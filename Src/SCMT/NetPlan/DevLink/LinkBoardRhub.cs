using System.Collections.Generic;
using System.Linq;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.DevLink
{
	public sealed class LinkBoardRhub : NetPlanLinkBase
	{
		public LinkBoardRhub() : base()
		{
			m_irRecordType = EnumDevType.board_rru;
		}

		#region 虚函数重载

		public override bool DelLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			var rhubClone = m_rhubDev.DeepClone();
			if (!ResetBoardInfoInRhub(rhubClone, m_nRhubPort))
			{
				MibInfoMgr.ErrorTip("删除连接失败，原因：重置rhub设备中板卡信息失败");
				return false;
			}

			ChangeDevRecordTypeToModify(rhubClone);

			// 处理与rhub连接的pico的信息
			if (mapMibInfo.ContainsKey(EnumDevType.rru))
			{
				// board与rhub连通的瞬间，先前rhub与pico的连接上pico设备属性也要设置
				List<DevAttributeInfo> oldPicoList;
				List<DevAttributeInfo> newPicoList;

				var bbi = new BoardBaseInfo();

				if (!UpdatePicoInfo(bbi, mapMibInfo[EnumDevType.rru], out oldPicoList, out newPicoList))
				{
					MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
					return false;
				}

				foreach (var pico in oldPicoList)
				{
					mapMibInfo[EnumDevType.rru].Remove(pico);
				}

				foreach (var pico in newPicoList)
				{
					mapMibInfo[EnumDevType.rru].Add(pico);
				}
			}

			// 删除board与rhub之间的连接
			var irRecord = GetDevAttributeInfo(m_strIrRecodeIndex, EnumDevType.board_rru);
			MibInfoMgr.DelDevFromMap(mapMibInfo, EnumDevType.board_rru, irRecord);

			mapMibInfo[EnumDevType.rhub].Remove(m_rhubDev);
			mapMibInfo[EnumDevType.rhub].Add(rhubClone);

			// todo rhub级联的情况暂不支持

			Log.Debug($"删除连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"删除类型为：{m_irRecordType.ToString()}，索引为：{irRecord.m_strOidIndex}的记录成功");

			MibInfoMgr.InfoTip("删除连接成功");

			return true;
		}

		public override bool AddLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordNotExistInAdd))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			var rhubClone = m_rhubDev.DeepClone();

			var bbi = GetBoardBaseInfo(m_boardDev);

			if (!SetBoardBaseInfoInRhub(bbi, rhubClone))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			if (!SetOfpLinkInfoInRhub(rhubClone, m_nBoardPort))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			if (RecordDataType.NewAdd != rhubClone.m_recordType)
			{
				rhubClone.SetDevRecordType(RecordDataType.Modified);
			}

			if (mapMibInfo.ContainsKey(EnumDevType.rru))
			{
				// board与rhub连通的瞬间，先前rhub与pico的连接上pico设备属性也要设置
				List<DevAttributeInfo> oldPicoList;
				List<DevAttributeInfo> newPicoList;
				if (!UpdatePicoInfo(bbi, mapMibInfo[EnumDevType.rru], out oldPicoList, out newPicoList))
				{
					MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
					return false;
				}

				foreach (var pico in oldPicoList)
				{
					mapMibInfo[EnumDevType.rru].Remove(pico);
				}

				var linkrp = new LinkRhubPico();
				foreach (var pico in newPicoList)
				{
					mapMibInfo[EnumDevType.rru].Add(pico);

					// 板卡和rhub连接成功后，才能增加netEthPlanEntry表记录，因为需要bbu的架框槽信息
					linkrp.AddEthPlanRecord(rhubClone, pico, mapMibInfo);
				}
			}

			// todo 当前尚未支持rhub级联的情况，rhub级联后需要计算后面rhub的接入级数、连接的板卡信息等

			// 添加一条记录
			var irRecord = new DevAttributeInfo(EnumDevType.board_rru, m_strIrRecodeIndex);     // 记录仍然是board_rru类型

			mapMibInfo[EnumDevType.rhub].Remove(m_rhubDev);
			mapMibInfo[EnumDevType.rhub].Add(rhubClone);

			AddDevToMap(mapMibInfo, EnumDevType.board_rru, irRecord);

			Log.Debug($"添加连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"添加类型为：{m_irRecordType.ToString()}，索引为：{irRecord.m_strOidIndex}的记录成功");

			MibInfoMgr.InfoTip("添加连接成功");

			return true;
		}

		public override bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo, IsRecordExist checkExist)
		{
			var boardIndex = wholeLink.GetDevIndex(EnumDevType.board);
			if (null == boardIndex)
			{
				m_strLatestError = "获取板卡索引失败";
				return false;
			}

			m_nBoardPort = wholeLink.GetDevIrPort(EnumDevType.board, EnumPortType.bbu_to_rhub);
			if (-1 == m_nBoardPort)
			{
				m_strLatestError = "获取板卡连接rhub设备光口号失败";
				return false;
			}

			m_boardDev = GetDevAttributeInfo(boardIndex, EnumDevType.board);
			if (null == m_boardDev)
			{
				m_strLatestError = $"根据索引{boardIndex}未找到板卡信息";
				return false;
			}

			var rhubIndex = wholeLink.GetDevIndex(EnumDevType.rhub);
			if (null == rhubIndex)
			{
				m_strLatestError = "获取rhub设备索引失败";
				return false;
			}

			m_nRhubPort = wholeLink.GetDevIrPort(EnumDevType.rhub, EnumPortType.rhub_to_bbu);
			if (-1 == m_nRhubPort)
			{
				m_strLatestError = "获取rhub设备连接板卡光口号失败";
				return false;
			}

			m_rhubDev = GetDevAttributeInfo(rhubIndex, EnumDevType.rhub);
			if (null == m_rhubDev)
			{
				m_strLatestError = $"根据索引{rhubIndex}未找到rhub设备信息";
				return false;
			}

			// 确定netIROptPlanEntry中是否已经存在对应的记录
			m_strIrRecodeIndex = $"{boardIndex}.{m_nBoardPort}";
			if (!checkExist.Invoke(m_strIrRecodeIndex, EnumDevType.board_rru))
			{
				var tmp = checkExist == RecordExistInDel ? "不" : "已";
				m_strLatestError = $"索引为{m_strIrRecodeIndex}的光口规划表记录{tmp}存在";
				return false;
			}

			var slotNo = m_boardDev.GetNeedUpdateValue("netBoardSlotNo");
			if (null == slotNo)
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备netBoardSlotNo字段值失败";
				return false;
			}

			var boardType = m_boardDev.GetNeedUpdateValue("netBoardType");
			if (null == boardType)
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备netBoardType字段值失败";
				return false;
			}

			m_strOfpMibName = $"netRHUBOfp{m_nRhubPort}AccessOfpPortNo";                // 板卡的光口号
			if (!m_rhubDev.IsExistField(m_strOfpMibName))
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备{m_strOfpMibName}字段值失败，可能MIB版本差异导致错误";
				return false;
			}

			m_strOfpLinePosMibName = $"netRHUBOfp{m_nRhubPort}AccessLinePosition";      // 接入级数
			if (!m_rhubDev.IsExistField(m_strOfpLinePosMibName))
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备{m_strOfpLinePosMibName}字段值失败，可能MIB版本差异导致错误";
				return false;
			}

			m_strSlotMibName = m_nRhubPort == 1 ? "netRHUBAccessSlotNo" : $"netRHUBOfp{m_nRhubPort}SlotNo";
			if (!m_rhubDev.IsExistField(m_strSlotMibName))
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备{m_strSlotMibName}字段值失败，可能MIB版本差异导致错误";
				return false;
			}

			return true;
		}

		public override DevAttributeInfo GetRecord(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return null;
			}

			return GetDevAttributeInfo(m_strIrRecodeIndex, m_irRecordType);
		}

		private bool ResetBoardInfoInRhub(DevAttributeInfo rhub, int nRhubPort)
		{
			var bbi = new BoardBaseInfo();
			return SetBoardBaseInfoInRhub(bbi, rhub) && SetOfpLinkInfoInRhub(rhub, -1);
		}

		/// <summary>
		/// 设置rhub设备和board相关的属性
		/// </summary>
		/// <param name="bbi"></param>
		/// <param name="dev"></param>
		/// <returns></returns>
		private bool SetBoardBaseInfoInRhub(BoardBaseInfo bbi, DevAttributeInfo dev)
		{
			var rowstatus = dev.GetNeedUpdateValue("netRHUBRowStatus");
			if (null == rowstatus)
			{
				m_strLatestError = $"获取索引为{dev.m_strOidIndex}rhub设备netRHUBRowStatus字段值失败";
				return false;
			}

			if ("6" == rowstatus && dev.m_recordType != RecordDataType.NewAdd)
			{
				Log.Error($"RHUB{dev.m_strOidIndex}行状态为：待删除");
				//return false;
			}

			if (!dev.SetDevAttributeValue("netRHUBAccessRackNo", bbi.strRackNo))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备netRHUBAccessRackNo字段值{bbi.strRackNo}失败";
				return false;
			}
			if (!dev.SetDevAttributeValue("netRHUBAccessShelfNo", bbi.strShelfNo))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备netRHUBAccessShelfNo字段值{bbi.strShelfNo}失败";
				return false;
			}
			if (!dev.SetDevAttributeValue(m_strSlotMibName, bbi.strSlotNo))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备{m_strSlotMibName}字段值{bbi.strSlotNo}失败";
				return false;
			}

			if (!dev.SetDevAttributeValue("netRHUBAccessBoardType", bbi.strBoardCode))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备netRHUBAccessBoardType字段值{bbi.strBoardCode}失败";
				return false;
			}

			return true;
		}

		private bool SetOfpLinkInfoInRhub(DevAttributeInfo rhub, int nBoardPort)
		{
			var workMode = rhub.GetNeedUpdateValue("netRHUBOfpWorkMode", false);
			if (null == workMode)
			{
				m_strLatestError = $"获取索引为{rhub.m_strOidIndex}rhub设备netRHUBOfpWorkMode字段值失败";
				return false;
			}

			if (!rhub.SetDevAttributeValue(m_strOfpMibName, nBoardPort.ToString()))
			{
				m_strLatestError = $"设置索引为{rhub.m_strOidIndex}的rhub设备{m_strOfpMibName}字段值{nBoardPort}失败";
				return false;
			}

			var lp = -1 == nBoardPort ? "-1" : "1";

			if (!rhub.SetDevAttributeValue(m_strOfpLinePosMibName, lp))
			{
				m_strLatestError = $"设置索引为{rhub.m_strOidIndex}的rhub设备{m_strOfpLinePosMibName}字段值{lp}失败";
				return false;
			}

			return true;
		}

		/// <summary>
		/// 更新和rhub相连的pico信息
		/// </summary>
		/// <param name="rruList"></param>
		/// <param name="picoList">出参，保存原来map中的pico设备</param>
		/// <param name="newPico">出参，保存clone的pico设备</param>
		/// <returns></returns>
		private bool UpdatePicoInfo(BoardBaseInfo bbi, List<DevAttributeInfo> rruList, out List<DevAttributeInfo> picoList, out List<DevAttributeInfo> newPico)
		{
			newPico = new List<DevAttributeInfo>();
			picoList = new List<DevAttributeInfo>();

			if (null == rruList)
			{
				return false;
			}

			var rhubNo = m_rhubDev.m_strOidIndex.Trim('.');

			foreach (var rru in rruList)
			{
				var rruTypeIndex = rru.GetNeedUpdateValue("netRRUTypeIndex");
				if (null == rruTypeIndex)
				{
					m_strLatestError = $"获取索引为{rru.m_strOidIndex}的rru设备netRRUTypeIndex字段值失败";
					return false;
				}

				if (!NPERruHelper.GetInstance().IsPicoDevice(int.Parse(rruTypeIndex)))
				{
					continue;
				}

				var rhubNoInPico = rru.GetNeedUpdateValue("netRRUHubNo");
				if (null == rhubNoInPico)
				{
					m_strLatestError = $"获取索引为{rru.m_strOidIndex}的rru设备netRRUHubNo字段值失败";
					return false;
				}

				// 判断Pico连接的rhub是否是当前正在建立连接的rhub
				if (rhubNoInPico != rhubNo)
				{
					continue;
				}

				var picoClone = rru.DeepClone();
				if (!SetBoardBaseInfoInRru(bbi, picoClone, 1))  // todo pico的端口硬编码
				{
					return false;
				}

				if (RecordDataType.NewAdd != rru.m_recordType)
				{
					picoClone.SetDevRecordType(RecordDataType.Modified);
				}

				newPico.Add(picoClone);
				picoList.Add(rru);
			}

			return true;
		}

		#endregion 虚函数重载


		#region 静态接口区

		/// <summary>
		/// 解析板卡到rhub之间的连接
		/// netIROptPlanEntry
		/// </summary>
		/// <param name="rhubList"></param>
		/// <param name="boardList"></param>
		/// <param name="irDevList"></param>
		/// <returns></returns>
		public static List<WholeLink> ParseBoardToRhubLink(IReadOnlyCollection<DevAttributeInfo> rhubList,
			IReadOnlyCollection<DevAttributeInfo> boardList, IReadOnlyCollection<DevAttributeInfo> irDevList)
		{
			var lw = new List<WholeLink>();
			if (null == rhubList || null == boardList || null == irDevList)
			{
				Log.Error("解析板卡到rhub的连接信息缺失");
				return lw;
			}

			// rhub设备可能是4个光口连接，也可能是2个光口连接
			foreach (var rhubDai in rhubList)
			{
				// 取出rhub设备，读取连接板卡的架框槽
				if (null == rhubDai)
				{
					continue;
				}

				for (var i = 1; i <= MagicNum.RRU_TO_BBU_PORT_CNT; i++)
				{
					var tmpLink = HandleRhubOfpLinkInfo(i, rhubDai, boardList, irDevList);
					if (null != tmpLink)
					{
						lw.Add(tmpLink);
					}
				}
			}

			// 5G的pico设备只有一个网口，通过网口与rhub的电口相连
			var rrLinkList = HandleWaitPrevRhubList();
			lw.AddRange(rrLinkList);

			return lw;
		}

		/// <summary>
		/// 处理rhub设备的光口号信息
		/// </summary>
		/// <param name="nOfpIndex">光口索引，rhub2.0才有4个光口</param>
		/// <param name="rhub">rhub设备属性信息</param>
		/// <param name="boardDevList">板卡信息列表</param>
		/// <param name="irOptRecordList">IR光口规划表</param>
		/// <returns></returns>
		private static WholeLink HandleRhubOfpLinkInfo(int nOfpIndex, DevAttributeInfo rhub, IEnumerable<DevAttributeInfo> boardDevList,
			IEnumerable<DevAttributeInfo> irOptRecordList)
		{
			// 取出rru编号、接入板卡的信息
			var rhubNo = rhub.GetFieldOriginValue("netRHUBNo");

			// 级联模式的rru，连接的板卡的信息和第1级rru连接板卡信息一样
			var rackNo = rhub.GetFieldOriginValue("netRHUBAccessRackNo");
			var shelfNo = rhub.GetFieldOriginValue("netRHUBAccessShelfNo");
			var boardTypeInRhub = rhub.GetFieldOriginValue("netRHUBAccessBoardType");
			var hubIndex = $".{rhubNo}";

			// 级联模式才会有接入级数>1的情况
			var workMode = rhub.GetFieldOriginValue("netRHUBOfpWorkMode");

			var slotNoMib = "netRHUBAccessSlotNo";      // 光口接入的槽位号
			if (nOfpIndex > 1)
			{
				slotNoMib = $"netRHUBOfp{nOfpIndex}SlotNo";
			}

			// 获取光口n连接的槽位号
			var slotNo = rhub.GetFieldOriginValue(slotNoMib);
			if (null == slotNo)
			{
				Log.Debug($"当前版本MIB中不包含{slotNoMib}字段");
				return null;
			}

			if ("-1" == slotNo)
			{
				Log.Debug($"索引为{hubIndex}的RHUB光口{nOfpIndex}未连接设备");
				return null;
			}

			var boardIndex = $".{rackNo}.{shelfNo}.{slotNo}";

			// 根据索引获取板卡信息
			var boardInfo = boardDevList.FirstOrDefault(item => item.m_strOidIndex == boardIndex);
			if (null == boardInfo)
			{
				Log.Error($"索引为{hubIndex}的RHUB连接板卡索引{boardIndex}，未找到对应的板卡");
				return null;
			}

			// 判断板卡的类型是否相同
			var boardTypeInBoard = boardInfo.GetFieldOriginValue("netBoardType");
			if (boardTypeInBoard != boardTypeInRhub)
			{
				Log.Error($"索引为{hubIndex}的RHUB连接板卡类型{boardTypeInRhub}，而相同索引{boardIndex}的板卡类型为{boardTypeInBoard}，二者不一致");
				return null;
			}

			// 获取每个光口连接的对端光口号和接入级数
			var ofpPortMibName = $"netRHUBOfp{nOfpIndex}AccessOfpPortNo";
			var accessPosMibName = $"netRHUBOfp{nOfpIndex}AccessLinePosition";

			var remoteOfPort = rhub.GetFieldOriginValue(ofpPortMibName);
			if (null == remoteOfPort)
			{
				Log.Debug($"当前版本MIB中不包含{ofpPortMibName}字段");
				return null;
			}

			if ("-1" == remoteOfPort)
			{
				Log.Debug($"索引为{hubIndex}的RHUB光口{nOfpIndex}未连接任何设备");
				return null;
			}

			// todo 在netIROptPlanEntry中查找是否存在这条连接。2级级联的设备是否存在于这张表中？
			var irIndex = $"{boardIndex}.{remoteOfPort}";
			var record = irOptRecordList.FirstOrDefault(tmp => tmp.m_strOidIndex == irIndex);
			if (null == record)
			{
				Log.Error($"根据索引{irIndex}未找到netIROptPlanEntry表实例");
				return null;
			}

			var accessPos = rhub.GetFieldOriginValue(accessPosMibName);
			if (null == accessPos)
			{
				Log.Debug($"当前版本MIB中不包含{accessPosMibName}字段");
				return null;
			}

			if ("-1" == accessPos)
			{
				Log.Error($"索引为{hubIndex}的RHUB光口{nOfpIndex}连接对端光口{remoteOfPort}，但接入级数为-1，无效信息");
				return null;
			}

			var parsedRhub = new ParsedRruInfo
			{
				nBoardIrPort = int.Parse(remoteOfPort),
				nCurLinePos = int.Parse(accessPos),
				nRruIrPort = nOfpIndex,
				strBoardIndex = boardIndex,
				strRruIndex = hubIndex
			};

			// rhub连接板卡时，接入级数为1；正常模式，接入级数为1
			if ("1" == accessPos)
			{
				var brLink = GenerateBoardToRhubLink(boardIndex, int.Parse(remoteOfPort), hubIndex, nOfpIndex);

				if ("2" == workMode || "5" == workMode)     // 级联；负荷分担 + 级联
				{
					AddElementToParsedRruList(parsedRhub);
				}
				return brLink;
			}

			if ("2" != workMode && "5" != workMode)     // 级联；负荷分担+级联
			{
				Log.Error($"索引为{hubIndex}的RHUB工作模式为{workMode}，光口级联级数错误");
				return null;
			}

			// 接入级数大于1，则要找接入级数小1级的设备组成连接信息
			//var prevRhub = GetPrevRruInfo(parsedRhub);
			//if (null == prevRhub)
			//{
			//	// 没有找到上级rru，放入等待队列
			//	AddElementToWaitPrevList(parsedRhub);
			//	return true;
			//}

			//if (!GenerateRhubToRhubLink(prevRhub, parsedRhub))
			//{
			//	Log.Error("生成rhub到rhub的连接失败");
			//	return false;
			//}

			AddElementToParsedRruList(parsedRhub);      // 解析完成的就加入到列表中

			return null;
		}

		/// <summary>
		/// 生成一条board到rhub设备的连接
		/// </summary>
		/// <param name="strBoardIndex"></param>
		/// <param name="nBoardIrPort"></param>
		/// <param name="strRhubIndex"></param>
		/// <param name="nRhubIrPort"></param>
		/// <returns></returns>
		private static WholeLink GenerateBoardToRhubLink(string strBoardIndex, int nBoardIrPort, string strRhubIndex, int nRhubIrPort)
		{
			var boardEndpoint = new LinkEndpoint
			{
				devType = EnumDevType.board,
				strDevIndex = strBoardIndex,
				nPortNo = nBoardIrPort,
				portType = EnumPortType.bbu_to_rhub
			};

			var rruEndPoint = new LinkEndpoint
			{
				devType = EnumDevType.rhub,
				strDevIndex = strRhubIndex,
				nPortNo = nRhubIrPort,
				portType = EnumPortType.rhub_to_bbu
			};

			return new WholeLink(boardEndpoint, rruEndPoint, EnumDevType.board_rhub);
		}

		/// <summary>
		/// 保存已经找到前一级的设备属性
		/// </summary>
		/// <param name="element"></param>
		private static void AddElementToParsedRruList(ParsedRruInfo element)
		{
			if (null == element)
			{
				return;
			}

			if (null == m_mapParsedRhub)
			{
				m_mapParsedRhub = new Dictionary<string, List<ParsedRruInfo>>();
			}

			var key = $"{element.strBoardIndex}.{element.nBoardIrPort}";
			if (m_mapParsedRhub.ContainsKey(key))
			{
				if (null == m_mapParsedRhub[key])
				{
					var listRru = new List<ParsedRruInfo>();
					listRru.Add(element);
					m_mapParsedRhub[key] = listRru;
				}
				else
				{
					m_mapParsedRhub[key].Add(element);
				}
			}
		}

		/// <summary>
		/// 生成一条rhub到rhub的连接
		/// </summary>
		/// <param name="prevRhubInfo"></param>
		/// <param name="curRhubInfo"></param>
		/// <returns></returns>
		private static WholeLink GenerateRhubToRhubLink(ParsedRruInfo prevRhubInfo, ParsedRruInfo curRhubInfo)
		{
			if (null == prevRhubInfo || null == curRhubInfo)
			{
				Log.Error("传入无效的参数");
				return null;
			}

			// 上一级rru的连接板卡的光口号与rru之间的级联的光口号的关系没有确定的关系
			var prevRruDownLinePort = -1;   // 上级rru的下联口
			if (1 == prevRhubInfo.nRruIrPort)
			{
				prevRruDownLinePort = 2;    // rru在级联模式下，必然区分上联口和下联口。上联口只能是1。TODO rru是否有4个光口的？
											// todo 当前5GIII的AAU设备是4个光口，1主3辅，尚不支持级联模式，后来人要注意(2018.11.2,houshangling)
			}

			var nextRruUpLinePort = curRhubInfo.nRruIrPort;      // 上联口
																// rru级联必然是上级rru的下联口连接到下级rru的上联口

			var prevEndpoint = new LinkEndpoint
			{
				devType = EnumDevType.rhub,
				strDevIndex = prevRhubInfo.strRruIndex,
				nPortNo = prevRruDownLinePort,
				portType = EnumPortType.rhub_to_rhub
			};

			var curEndPoint = new LinkEndpoint
			{
				devType = EnumDevType.rhub,
				strDevIndex = curRhubInfo.strRruIndex,
				nPortNo = nextRruUpLinePort,
				portType = EnumPortType.rhub_to_rhub
			};

			return new WholeLink(prevEndpoint, curEndPoint, EnumDevType.rhub_rhub);
		}

		private static IEnumerable<WholeLink> HandleWaitPrevRhubList()
		{
			var lw = new List<WholeLink>();
			foreach (var kv in m_mapParsedRhub)
			{
				var rruList = kv.Value;
				rruList.Sort(new PriComparer());

				for (int i = 1; i < rruList.Count; i++)
				{
					var prev = rruList.ElementAt(i - 1);
					var post = rruList.ElementAt(i);
					var tmpLink = GenerateRhubToRhubLink(prev, post);
					if (null != tmpLink)
					{
						lw.Add(tmpLink);
					}
				}
			}

			return lw;
		}

		private static Dictionary<string, List<ParsedRruInfo>> m_mapParsedRhub;

		#endregion

		#region 私有数据区

		private DevAttributeInfo m_rhubDev;
		private DevAttributeInfo m_boardDev;

		private string m_strOfpMibName;
		private string m_strOfpLinePosMibName;
		private string m_strSlotMibName;
		private string m_strIrRecodeIndex;

		private int m_nBoardPort;
		private int m_nRhubPort;

		private EnumDevType m_irRecordType;

		#endregion 私有数据区
	}
}