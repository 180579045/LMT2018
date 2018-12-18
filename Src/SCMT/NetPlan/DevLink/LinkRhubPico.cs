using System;
using System.Collections.Generic;
using System.Linq;
using LogManager;

namespace NetPlan
{
	internal sealed class LinkRhubPico : NetPlanLinkBase
	{
		public LinkRhubPico()
		{
			m_ethRecordType = EnumDevType.rhub_prru;
		}

		#region 虚函数区

		public override bool AddLink(WholeLink wholeLink, NPDictionary mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordNotExistInAdd))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			var picoClone = m_picoDev.DeepClone();

			if (RecordDataType.NewAdd != picoClone.m_recordType)
			{
				picoClone.SetDevRecordType(RecordDataType.Modified);
			}

			// 设置pico中连接rhub的信息
			var hubNo = int.Parse(m_rhubDev.m_strOidIndex.Trim('.'));
			if (!SetRhubInfoInPico(picoClone, m_nRhubEthPort, hubNo))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			// 判断rhub是否已经连接到板卡，如果已经连接到板卡，就设置pico中板卡的属性信息
			if (HasConnectedToBoard(m_rhubDev))
			{
				var bbi = GetBoardInfoFromRhub(RecordNotExistInAdd);
				if (null == bbi)
				{
					MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
					return false;
				}

				if (!SetBoardBaseInfoInRru(bbi, picoClone, m_nPicoPort))
				{
					MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
					return false;
				}

				if (!SetIrPortInfoInPico(m_rhubDev, picoClone, m_nPicoPort))
				{
					Log.Error("设置pico的接入板光口号和接入级数失败");
					return false;
				}

				// m_strEthRecordIndex只有在rhub已经连接过板卡后才会生成
				if (null == m_strEthRecordIndex)
				{
					MibInfoMgr.ErrorTip("添加连接失败，原因：以太网口规划表记录索引为null");
					return false;
				}

				var newRecord = new DevAttributeInfo(EnumDevType.rhub_prru, m_strEthRecordIndex);
				AddDevToMap(mapMibInfo, EnumDevType.rhub_prru, newRecord);

				Log.Debug($"添加类型为：{m_ethRecordType.ToString()}，索引为：{newRecord.m_strOidIndex}的记录成功");
			}

			mapMibInfo[EnumDevType.rru].Remove(m_picoDev);
			mapMibInfo[EnumDevType.rru].Add(picoClone);

			Log.Debug($"添加连接成功，连接详细信息：{wholeLink}");

			MibInfoMgr.InfoTip("添加连接成功");

			return true;
		}

		public override bool DelLink(WholeLink wholeLink, NPDictionary mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			var picoClone = m_picoDev.DeepClone();
			var bbi = new BoardBaseInfo();
			if (!SetBoardBaseInfoInRru(bbi, picoClone, m_nPicoPort))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			if (!SetRhubInfoInPico(picoClone, -1, -1))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			// 只有rhub已经连接上板卡才会删除以太网口规划记录，因为只有rhub连到板卡，才会添加以太网口规划记录
			if (HasConnectedToBoard(m_rhubDev))
			{
				bbi = GetBoardInfoFromRhub(RecordExistInDel);
				if (null == bbi)
				{
					MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
					return false;
				}

				var record = GetDevAttributeInfo(m_strEthRecordIndex, EnumDevType.rhub_prru);
				MibInfoMgr.DelDevFromMap(mapMibInfo, EnumDevType.rhub_prru, record);
				Log.Debug($"删除类型为：{m_ethRecordType.ToString()}，索引为：{record.m_strOidIndex}的记录成功");
			}

			ChangeDevRecordTypeToModify(picoClone);

			mapMibInfo[EnumDevType.rru].Remove(m_picoDev);
			mapMibInfo[EnumDevType.rru].Add(picoClone);

			Log.Debug($"删除连接成功，连接详细信息：{wholeLink}");

			MibInfoMgr.InfoTip("删除连接成功");

			return true;
		}

		public override bool CheckLinkIsValid(WholeLink wholeLink, NPDictionary mapMibInfo, IsRecordExist checkExist)
		{
			var rhubIndex = wholeLink.GetDevIndex(EnumDevType.rhub);
			if (null == rhubIndex)
			{
				m_strLatestError = "获取rhub设备的索引失败";
				return false;
			}

			m_nRhubEthPort = wholeLink.GetDevIrPort(EnumDevType.rhub, EnumPortType.rhub_to_pico);
			if (-1 == m_nRhubEthPort)
			{
				m_strLatestError = "获取rhub设备连接pico的端口失败";
				return false;
			}

			m_rhubDev = GetDevAttributeInfo(rhubIndex, EnumDevType.rhub);
			if (null == m_rhubDev)
			{
				m_strLatestError = $"根据rhub设备索引{rhubIndex}获取设备属性信息失败";
				return false;
			}

			var picoIndex = wholeLink.GetDevIndex(EnumDevType.rru);
			if (null == picoIndex)
			{
				m_strLatestError = "获取pico设备的索引失败";
				return false;
			}

			m_nPicoPort = wholeLink.GetDevIrPort(EnumDevType.rru, EnumPortType.pico_to_rhub);
			if (-1 == m_nPicoPort)
			{
				m_strLatestError = "获取pico设备连接rhub的端口失败";
				return false;
			}

			m_picoDev = GetDevAttributeInfo(picoIndex, EnumDevType.rru);
			if (null == m_picoDev)
			{
				m_strLatestError = $"根据pico索引{picoIndex}获取设备属性信息失败";
				return false;
			}

			return true;
		}

		public override DevAttributeInfo GetRecord(WholeLink wholeLink, NPDictionary mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return null;
			}

			if (HasConnectedToBoard(m_rhubDev))
			{
				var bbi = GetBoardInfoFromRhub(RecordExistInDel);
				if (null == bbi)
				{
					return null;
				}

				// m_strEthRecordIndex只有在rhub已经连接过板卡后才会生成
				if (null == m_strEthRecordIndex)
				{
					Log.Error("未能解析出eth record index");
					return null;
				}

				return GetDevAttributeInfo(m_strEthRecordIndex, m_ethRecordType);
			}

			return null;
		}

		/// <summary>
		/// 增加rhub到pico之间的连接以太网连接
		/// </summary>
		public bool AddEthPlanRecord(DevAttributeInfo rhubDev, DevAttributeInfo picoDev, NPDictionary mapAllData)
		{
			if (!HasConnectedToBoard(rhubDev))
			{
				Log.Error($"从索引为{rhubDev.m_strOidIndex}的rhub设备中查询rhub未连接到bbu");
				return false;
			}

			var mapPicoToRhub = NetDevRru.GetLinkedRhubInfoFromPico(picoDev);
			if (null == mapPicoToRhub || mapPicoToRhub.Count == 0)
			{
				Log.Error($"查询索引为{picoDev.m_strOidIndex}的pico设备连接rhub的连接信息为空");
				return false;
			}

			var boardSlotList = NetDevRhub.GetRhubLinkToBoardSlotNo(rhubDev);
			if (boardSlotList.Count == 0)
			{
				Log.Error($"从索引为{rhubDev.m_strOidIndex}的rhub设备中查询连接bbu端口号失败");
				return false;
			}

			foreach (var boardSlot in boardSlotList)
			{
				var bbuIdx = $".0.0.{boardSlot}";
				mapOriginData = mapAllData;

				foreach (var item in mapPicoToRhub)
				{
					var ridx = $"{bbuIdx}.{item.Value.strDevIndex.Trim('.')}.{item.Value.nPortNo}";
					if (RecordExistInDel(ridx, EnumDevType.rhub_prru))
					{
						Log.Error($"索引为{ridx}类型为rhub_prru的记录已经存在");
						continue;
					}

					var newRecord = new DevAttributeInfo(EnumDevType.rhub_prru, ridx);
					AddDevToMap(mapAllData, EnumDevType.rhub_prru, newRecord);

					SetIrPortInfoInPico(rhubDev, picoDev, item.Key);
				}
			}

			return true;
		}

		#endregion 虚函数区

		#region 静态接口

		/// <summary>
		/// 解析rhub到pico的连接
		/// </summary>
		/// <param name="rhubList"></param>
		/// <param name="rruList"></param>
		/// <param name="ethCfgList"></param>
		/// <returns></returns>
		public static List<WholeLink> ParseRhubToPicoLink(List<DevAttributeInfo> rhubList, IEnumerable<DevAttributeInfo> rruList, List<DevAttributeInfo> ethCfgList)
		{
			var ethLinkList = new List<WholeLink>();

			if (null == rhubList || null == rruList || null == ethCfgList)
			{
				Log.Error("解析rhub到pico的连接，信息缺失");
				return ethLinkList;
			}

			foreach (var rru in rruList)
			{
				var rruType = rru.GetFieldOriginValue("netRRUTypeIndex");
				if (!NPERruHelper.GetInstance().IsPicoDevice(int.Parse(rruType)))
				{
					continue;
				}

				var picoPort = 1;
				var hubPort = rru.GetFieldOriginValue("netRRUOfp1AccessEthernetPort");
				if ("-1" == hubPort)
				{
					hubPort = rru.GetFieldOriginValue("netRRUOfp2AccessEthernetPort");
					if ("-1" == hubPort)
					{
						Log.Error($"索引为{rru.m_strOidIndex}的pico设置未连接到rhub设备");
						continue;
					}
					picoPort = 2;
				}

				var hubNo = rru.GetFieldOriginValue("netRRUHubNo");
				if ("-1" == hubNo || "-1" == hubPort)
				{
					Log.Error("pico设备没有连接到任何hub设备");
					continue;
				}

				var picoToHubIndex = $".{hubNo}";
				var hubDev = rhubList.FirstOrDefault(rhub => rhub.m_strOidIndex == picoToHubIndex);
				if (null == hubDev)
				{
					Log.Error($"根据索引{picoToHubIndex}未找到rhub设备");
					continue;
				}

				var rackNo = hubDev.GetFieldOriginValue("netRHUBAccessRackNo");
				var shelfNo = hubDev.GetFieldOriginValue("netRHUBAccessShelfNo");

				var slotNoList = NetDevRhub.GetRhubLinkToBoardSlotNo(hubDev);
				if (slotNoList.Count == 0)
				{
					Log.Debug($"索引为{picoToHubIndex}的RHUB设备没有连接到板卡");
					continue;
				}

				// 此处不验证rhub连接的板卡是否存在，在上一步解析boardtohublink时已经验证过

				foreach (var slotNo in slotNoList)
				{
					var ethRecordIndex = $".{rackNo}.{shelfNo}.{slotNo}.{hubNo}.{hubPort}";
					if (null == ethCfgList.FirstOrDefault(ec => ec.m_strOidIndex == ethRecordIndex))
					{
						Log.Error($"根据索引{ethRecordIndex}未找到对应的以太网口配置信息");
						continue;
					}

					var nhubPort = int.Parse(hubPort);

					// 创建连接
					var link = GenerateRhubToPicoLink(picoToHubIndex, nhubPort, rru.m_strOidIndex, picoPort);
					ethLinkList.Add(link);
				}
			}

			return ethLinkList;
		}

		/// <summary>
		/// 生成一个hub到pico的连接
		/// </summary>
		/// <param name="strHubIndex"></param>
		/// <param name="nHubEthPort"></param>
		/// <param name="strPicoIndex"></param>
		/// <param name="nPicoPort"></param>
		/// <returns></returns>
		private static WholeLink GenerateRhubToPicoLink(string strHubIndex, int nHubEthPort, string strPicoIndex, int nPicoPort)
		{
			var hub = new LinkEndpoint
			{
				devType = EnumDevType.rhub,
				strDevIndex = strHubIndex,
				nPortNo = nHubEthPort,
				portType = EnumPortType.rhub_to_pico
			};

			var pico = new LinkEndpoint
			{
				devType = EnumDevType.rru,
				strDevIndex = strPicoIndex,
				nPortNo = nPicoPort,
				portType = EnumPortType.pico_to_rhub
			};

			var link = new WholeLink
			{
				m_srcEndPoint = hub,
				m_dstEndPoint = pico,
				m_linkType = EnumDevType.rhub_prru
			};

			return link;
		}


		public static string GetRhubToPicoPort(DevAttributeBase ethRecord)
		{
			return ethRecord?.GetFieldOriginValue("netEthPortIndexOnHub");
		}

		#endregion


		#region 私有函数区

		private BoardBaseInfo GetBoardInfoFromRhub(IsRecordExist checkExist)
		{
			var boardSlotList = NetDevRhub.GetRhubLinkToBoardSlotNo(m_rhubDev);
			if (boardSlotList.Count == 0)
			{
				m_strLatestError = $"获取索引为{m_rhubDev.m_strOidIndex}的rhub设备连接的板卡插槽号失败";
				return null;
			}

			foreach (var boardSlot in boardSlotList)
			{
				// 查询板卡相关的信息
				var boardIndex = $".0.0.{boardSlot}";
				var board = GetDevAttributeInfo(boardIndex, EnumDevType.board);
				if (null == board)
				{
					m_strLatestError = $"根据索引{boardIndex}未找到对应的板卡信息";
					return null;
				}

				var boardType = board.GetNeedUpdateValue("netBoardType");
				if (null == boardType || "-1" == boardType)
				{
					m_strLatestError = $"获取索引为{boardIndex}的板卡字段netBoardType值信息失败";
					return null;
				}

				// 查询以太网连接是否存在
				m_strEthRecordIndex = $"{boardIndex}{m_rhubDev.m_strOidIndex}.{m_nRhubEthPort}";
				if (!checkExist.Invoke(m_strEthRecordIndex, EnumDevType.rhub_prru))
				{
					var tmp = checkExist == RecordExistInDel ? "不" : "已";
					m_strLatestError = $"索引为{m_strEthRecordIndex}的以太网口规划表记录{tmp}存在";
					return null;
				}

				var bbi = new BoardBaseInfo("0", "0", boardSlot, boardType);	// todo 此处应该返回多个bbi
				return bbi;
			}

			return null;
		}

		private static bool HasConnectedToBoard(DevAttributeInfo rhub)
		{
			if (null == rhub)
			{
				throw new ArgumentNullException();
			}

			// 查询rhub设备是否已经建立到board的连接
			var boardSlotList = NetDevRhub.GetRhubLinkToBoardSlotNo(rhub);
			return boardSlotList.Count > 0;
		}

		// todo 放到NetDevRru中
		private bool SetRhubInfoInPico(DevAttributeInfo dev, int nEthPort, int nHubNo)
		{
			var mibName = $"netRRUOfp{m_nPicoPort}AccessEthernetPort";
			var bSucceed = dev.SetDevAttributeValue(mibName, nEthPort.ToString());
			if (!bSucceed)
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的pico设备{mibName}字段值{nEthPort}失败";
				return false;
			}

			bSucceed = dev.SetDevAttributeValue("netRRUHubNo", nHubNo.ToString());
			if (!bSucceed)
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的pico设备netRRUHubNo字段值{nHubNo}失败";
				return false;
			}

			return true;
		}

		// todo 需要细化
		private static bool SetIrPortInfoInPico(DevAttributeInfo hubdev, DevAttributeInfo picoDev, int nPicoIrPort)
		{
			// 从hub设备中找到任何一个光口连接的板卡光口号
			for (var i = 1; i < MagicNum.RRU_TO_BBU_PORT_CNT; i++)
			{
				var mibName = $"netRHUBOfp{i}AccessOfpPortNo";
				var value = hubdev.GetNeedUpdateValue(mibName);
				if (null == value || "-1" == value)
				{
					continue;
				}

				mibName = $"netRHUBOfp{i}AccessLinePosition";
				var apos = hubdev.GetNeedUpdateValue(mibName);
				if (null == apos || "-1" == apos)
				{
					continue;
				}

				// todo 下面是什么鬼操作
				var picoSlotMib = $"netRRUOfp{nPicoIrPort}AccessOfpPortNo";
				var picoAposMib = $"netRRUOfp{nPicoIrPort}AccessLinePosition";
				picoDev.SetFieldLatestValue(picoSlotMib, value);
				picoDev.SetFieldLatestValue(picoAposMib, apos);
				break;
			}

			return true;
		}

		#endregion


		#region 私有数据区

		private int m_nPicoPort;
		private int m_nRhubEthPort;
		private DevAttributeInfo m_picoDev;
		private DevAttributeInfo m_rhubDev;
		private string m_strEthRecordIndex;

		private readonly EnumDevType m_ethRecordType;

		#endregion 私有数据区
	}
}