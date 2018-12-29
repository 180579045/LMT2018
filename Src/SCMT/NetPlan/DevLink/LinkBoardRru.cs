using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetPlan
{
	// 专门处理board与rru之间的连接
	public class LinkBoardRru : NetPlanLinkBase
	{
		public LinkBoardRru()
		{
			m_irRecordType = EnumDevType.board_rru;
		}

		#region 虚函数区

		public override bool DelLink(WholeLink wholeLink, NPDictionary mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			var oldRecord = GetDevAttributeInfo(m_irRecordIndex, EnumDevType.board_rru);

			// 设置RRU连接的板卡的属性
			var rruClone = m_rruDev.DeepClone();
			if (!ResetRruToBoardInfo(rruClone, m_nRruPort))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			ChangeDevRecordTypeToModify(rruClone);

			// 删除旧的连接记录
			MibInfoMgr.DelDevFromMap(mapMibInfo, EnumDevType.board_rru, oldRecord);

			mapMibInfo[EnumDevType.rru].Remove(m_rruDev);
			mapMibInfo[EnumDevType.rru].Add(rruClone);

			// todo rru与board的最后一条连接删除时，才设置板类型等信息，先不做

			Log.Debug($"删除连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"删除类型为：{m_irRecordType.ToString()}，索引为：{oldRecord.m_strOidIndex}的记录成功");

			MibInfoMgr.InfoTip("删除连接成功");
			return true;
		}

		public override bool AddLink(WholeLink wholeLink, NPDictionary mapMibInfo)
		{
			mapOriginData = mapMibInfo;
			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordNotExistInAdd))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			// 设置netRRUEntry表和netIROptPlanEntry表
			var irRecord = new DevAttributeInfo(EnumDevType.board_rru, m_irRecordIndex);
			irRecord.SetDevRecordType(RecordDataType.NewAdd);

			// 所有的信息全部填在rruClone中，如果失败就直接return
			var rruClone = m_rruDev.DeepClone();
			if (!SetRruToBoardInfo(rruClone, m_nRruPort, m_boardDev, m_nBoardPort))
			{
				MibInfoMgr.ErrorTip("添加连接失败，原因：设置rru记录中板卡相关信息失败");
				return false;
			}

			if (RecordDataType.NewAdd != rruClone.m_recordType)
			{
				rruClone.SetDevRecordType(RecordDataType.Modified);
			}

			// 使用rruClone替代原来的rru，然后增加irRecord记录
			mapMibInfo[EnumDevType.rru].Remove(m_rruDev);
			mapMibInfo[EnumDevType.rru].Add(rruClone);

			AddDevToMap(mapMibInfo, EnumDevType.board_rru, irRecord);

			// todo RRU级联的情况
			Log.Debug($"添加连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"添加类型为：{m_irRecordType.ToString()}，索引为：{irRecord.m_strOidIndex}的记录成功");

			MibInfoMgr.InfoTip("添加连接成功");
			return true;
		}

		public override DevAttributeInfo GetRecord(WholeLink wholeLink, NPDictionary mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return null;
			}

			return GetDevAttributeInfo(m_irRecordIndex, m_irRecordType);
		}

		public override bool CheckLinkIsValid(WholeLink wholeLink, NPDictionary mapMibInfo, IsRecordExist checkExist)
		{
			if (null == wholeLink || null == mapMibInfo || mapMibInfo.Count == 0)
			{
				throw new ArgumentNullException();
			}

			var boardIndex = wholeLink.GetDevIndex(EnumDevType.board);
			if (null == boardIndex)
			{
				m_strLatestError = "获取板卡索引失败";
				return false;
			}

			m_nBoardPort = wholeLink.GetDevIrPort(EnumDevType.board, EnumPortType.bbu_to_rru);
			if (-1 == m_nBoardPort)
			{
				m_strLatestError = "获取板卡连接rru设备光口号失败";
				return false;
			}

			m_boardDev = GetDevAttributeInfo(boardIndex, EnumDevType.board);
			if (null == m_boardDev)
			{
				m_strLatestError = $"根据索引{boardIndex}未找到板卡信息";
				return false;
			}

			m_irRecordIndex = $"{boardIndex}.{m_nBoardPort}";
			var bExist = checkExist.Invoke(m_irRecordIndex, EnumDevType.board_rru);
			if (!bExist)
			{
				var tmp = checkExist == RecordExistInDel ? "不" : "已";
				m_strLatestError = $"索引为{m_irRecordIndex}的光口规划表记录{tmp}存在";
				return false;
			}

			// rru信息相关
			var rruIndex = wholeLink.GetDevIndex(EnumDevType.rru);
			if (null == rruIndex)
			{
				m_strLatestError = "获取RRU设备索引失败";
				return false;
			}

			m_nRruPort = wholeLink.GetDevIrPort(EnumDevType.rru, EnumPortType.rru_to_bbu);
			if (-1 == m_nRruPort)
			{
				m_strLatestError = "获取rru设备连接板卡光口号失败";
				return false;
			}

			m_rruDev = GetDevAttributeInfo(rruIndex, EnumDevType.rru);
			if (null == m_rruDev)
			{
				m_strLatestError = $"根据索引{rruIndex}未找到rru信息";
				return false;
			}

			return true;
		}

		#endregion 虚函数区

		#region 静态接口

		/// <summary>
		/// 解析board与rru之间的连接、rru与rru之间的连接
		/// </summary>
		/// <param name="rruDevList">rru设备列表</param>
		/// <param name="boardDevList">板卡设备列表</param>
		/// <param name="irOptList"></param>
		/// <returns></returns>
		public static List<WholeLink> ParseBoardToRruLink(IEnumerable<DevAttributeInfo> rruDevList, List<DevAttributeInfo> boardDevList,
			IReadOnlyCollection<DevAttributeInfo> irOptList)
		{
			var brLinkList = new List<WholeLink>();
			if (null == rruDevList || null == boardDevList || null == irOptList)
			{
				Log.Error("解析板卡到rru的连接，信息缺失");
				return brLinkList;
			}

			foreach (var rru in rruDevList)
			{
				if (null == rru)
				{
					continue;
				}

				var rruTypeIndex = rru.GetNeedUpdateValue("netRRUTypeIndex");
				if (null == rruTypeIndex)
				{
					return brLinkList;
				}

				if (NPERruHelper.GetInstance().IsPicoDevice(int.Parse(rruTypeIndex)))
				{
					Log.Debug($"索引为{rru.m_strOidIndex}的RRU是pico，bbu与之没有连接关系");
					continue;
				}

				// RRU最多有个4个光口连接板卡获取RRU
				for (var i = 1; i <= MagicNum.RRU_TO_BBU_PORT_CNT; i++)
				{
					var tmpLink = HandleRruOfpLinkInfo(i, rru, boardDevList, irOptList);
					if (null != tmpLink)
						brLinkList.Add(tmpLink);
				}
			}

			var tmpLinks = HandleWaitPrevRruList();
			brLinkList.AddRange(tmpLinks);

			return brLinkList;
		}

		/// 处理rru光口连接的信息
		private static WholeLink HandleRruOfpLinkInfo(int nOfpIndex, DevAttributeInfo rru,
			IEnumerable<DevAttributeInfo> boardDevList,
			IEnumerable<DevAttributeInfo> irOptList)
		{
			// 取出rru编号、接入板卡的信息
			var rruNo = rru.GetFieldOriginValue("netRRUNo");

			// 级联模式的rru，连接的板卡的信息和第1级rru连接板卡信息一样
			var rackNo = rru.GetFieldOriginValue("netRRUAccessRackNo");
			var shelfNo = rru.GetFieldOriginValue("netRRUAccessShelfNo");
			var boardTypeInRru = rru.GetFieldOriginValue("netRRUAccessBoardType");
			var rruIndex = $".{rruNo}";

			// 级联模式才会有接入级数>1的情况
			var workMode = rru.GetFieldOriginValue("netRRUOfpWorkMode");

			var slotNoMib = "netRRUAccessSlotNo";

			if (nOfpIndex > 1)
			{
				slotNoMib = $"netRRUOfp{nOfpIndex}SlotNo";
			}

			// 获取光口n连接的槽位号
			var slotNo = rru.GetFieldOriginValue(slotNoMib);
			if ("-1" == slotNo)
			{
				Log.Debug($"索引为{rruIndex}的RRU光口{nOfpIndex}未连接设备");
				return null;
			}

			var boardIndex = $".{rackNo}.{shelfNo}.{slotNo}";

			// 根据索引获取板卡信息
			var boardInfo = boardDevList.FirstOrDefault(item => item.m_strOidIndex == boardIndex);
			if (null == boardInfo)
			{
				Log.Error($"索引为{rruIndex}的RRU连接板卡索引{boardIndex}，未找到对应的板卡");
				return null;
			}

			// 判断板卡的类型是否相同
			var boardTypeInBoard = boardInfo.GetFieldOriginValue("netBoardType");
			if (boardTypeInBoard != boardTypeInRru)
			{
				Log.Error($"索引为{rruIndex}的RRU连接板卡类型{boardTypeInRru}，而相同索引{boardIndex}的板卡类型为{boardTypeInBoard}，二者不一致");
				return null;
			}

			// 获取每个光口连接的对端光口号和接入级数
			var ofpPortMibName = $"netRRUOfp{nOfpIndex}AccessOfpPortNo";
			var accessPosMibName = $"netRRUOfp{nOfpIndex}AccessLinePosition";

			var remoteOfPort = rru.GetFieldOriginValue(ofpPortMibName);
			if ("-1" == remoteOfPort)
			{
				Log.Debug($"索引为{rruIndex}的RRU光口{nOfpIndex}未连接任何设备");
				return null;
			}

			var irIndex = $"{boardIndex}.{remoteOfPort}";
			var record = irOptList.FirstOrDefault(tmp => tmp.m_strOidIndex == irIndex);
			if (null == record)
			{
				Log.Error($"根据索引{irIndex}未找到netIROptPlanEntry表实例");
				return null;
			}

			var accessPos = rru.GetFieldOriginValue(accessPosMibName);
			if ("-1" == accessPos)  // 非级联，接入级数为1
			{
				Log.Error($"索引为{rruIndex}的RRU光口{nOfpIndex}连接对端光口{remoteOfPort}，但接入级数为-1，无效信息");
				return null;
			}

			var parsedRru = new ParsedRruInfo
			{
				nBoardIrPort = int.Parse(remoteOfPort),
				nCurLinePos = int.Parse(accessPos),
				nRruIrPort = nOfpIndex,
				strBoardIndex = boardIndex,
				strRruIndex = rruIndex
			};

			// rru连接板卡时，接入级数为1；正常模式，接入级数为1
			if ("1" == accessPos)
			{
				var link = GenerateBoardToRruLink(boardIndex, int.Parse(remoteOfPort), rruIndex, nOfpIndex);

				if ("2" == workMode || "5" == workMode)
				{
					AddElementToParsedRruList(parsedRru);
				}

				return link;
			}

			if ("2" != workMode && "5" != workMode)
			{
				Log.Error($"索引为{rruIndex}的RRU工作模式为{workMode}，光口级联级数错误");
				return null;
			}

			//// 接入级数大于1，则要找接入级数小1级的设备组成连接信息
			//var prevRru = GetPrevRruInfo(parsedRru);
			//if (null == prevRru)
			//{
			//	// 没有找到上级rru，放入等待队列
			//	AddElementToWaitPrevList(parsedRru);
			//	return true;
			//}

			//if (!GenerateRruToRruLink(prevRru, parsedRru))
			//{
			//	Log.Error("生成rru到rru的连接失败");
			//	return false;
			//}

			AddElementToParsedRruList(parsedRru);       // 先保存起来，最后统一处理

			return null;
		}

		/// 生成一个rru到rru的连接
		private static WholeLink GenerateRruToRruLink(ParsedRruInfo prevRruInfo, ParsedRruInfo curRruInfo)
		{
			if (null == prevRruInfo || null == curRruInfo)
			{
				Log.Error("传入无效的参数");
				return null;
			}

			// 上一级rru的连接板卡的光口号与rru之间的级联的光口号的关系没有确定的关系
			var prevRruDownLinePort = -1;   // 上级rru的下联口
			if (1 == prevRruInfo.nRruIrPort)
			{
				prevRruDownLinePort = 2;    // rru在级联模式下，必然区分上联口和下联口。上联口只能是1。TODO rru是否有4个光口的？
											// todo 当前5GIII的AAU设备是4个光口，1主3辅，尚不支持级联模式，后来人要注意(2018.11.2,houshangling)
			}

			var nextRruUpLinePort = curRruInfo.nRruIrPort;      // 上联口
																// rru级联必然是上级rru的下联口连接到下级rru的上联口

			var prevEndpoint = new LinkEndpoint
			{
				devType = EnumDevType.rru,
				strDevIndex = prevRruInfo.strRruIndex,
				nPortNo = prevRruDownLinePort,
				portType = EnumPortType.rru_to_rru
			};

			var curEndPoint = new LinkEndpoint
			{
				devType = EnumDevType.rru,
				strDevIndex = curRruInfo.strRruIndex,
				nPortNo = nextRruUpLinePort,
				portType = EnumPortType.rru_to_rru
			};

			return new WholeLink(prevEndpoint, curEndPoint, EnumDevType.rru_rru);
		}

		/// <summary>
		/// 生成一个板卡到rru的连接信息
		/// </summary>
		/// <param name="strBoardIndex"></param>
		/// <param name="nBoardIrPort"></param>
		/// <param name="strRruIndex"></param>
		/// <param name="nRruIrPort"></param>
		/// <returns></returns>
		private static WholeLink GenerateBoardToRruLink(string strBoardIndex, int nBoardIrPort, string strRruIndex, int nRruIrPort)
		{
			var boardEndpoint = new LinkEndpoint
			{
				devType = EnumDevType.board,
				strDevIndex = strBoardIndex,
				nPortNo = nBoardIrPort,
				portType = EnumPortType.bbu_to_rru
			};

			var rruEndPoint = new LinkEndpoint
			{
				devType = EnumDevType.rru,
				strDevIndex = strRruIndex,
				nPortNo = nRruIrPort,
				portType = EnumPortType.rru_to_bbu
			};

			return new WholeLink(boardEndpoint, rruEndPoint, EnumDevType.board_rru);
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

			if (null == m_mapParsedRru)
			{
				m_mapParsedRru = new Dictionary<string, List<ParsedRruInfo>>();
			}

			var key = $"{element.strBoardIndex}.{element.nBoardIrPort}";
			if (m_mapParsedRru.ContainsKey(key))
			{
				if (null == m_mapParsedRru[key])
				{
					var listRru = new List<ParsedRruInfo> {element};
					m_mapParsedRru[key] = listRru;
				}
				else
				{
					m_mapParsedRru[key].Add(element);
				}
			}
		}

		private static List<WholeLink> HandleWaitPrevRruList()
		{
			var lw = new List<WholeLink>();
			if (null == m_mapParsedRru)
			{
				return lw;
			}

			foreach (var kv in m_mapParsedRru)
			{
				var rruList = kv.Value;
				rruList.Sort(new PriComparer());

				for (int i = 1; i < rruList.Count; i++)
				{
					var prev = rruList.ElementAt(i - 1);
					var post = rruList.ElementAt(i);
					var tmpLink = GenerateRruToRruLink(prev, post);
					if (null != tmpLink)
					{
						lw.Add(tmpLink);
					}
				}
			}

			return lw;
		}

		private static Dictionary<string, List<ParsedRruInfo>> m_mapParsedRru;

		#endregion 静态接口

		#region 私有函数

		/// <summary>
		/// 设置和板卡相连的RRU的属性信息
		/// </summary>
		/// <returns></returns>
		private bool SetRruToBoardInfo(DevAttributeInfo rruDev, int nRruPort, DevAttributeInfo boardDev, int nBoardPort)
		{
			if (null == rruDev || nRruPort < 0 || nRruPort >= MagicNum.RRU_TO_BBU_PORT_CNT ||
				null == boardDev || nBoardPort < 0 || nBoardPort >= MagicNum.BBU_IR_PORT_CNT)
			{
				return false;
			}

			var bbi = GetBoardBaseInfo(boardDev);
			if (null == bbi)
			{
				return false;
			}

			return SetBoardBaseInfoInRru(bbi, rruDev, nRruPort) && SetRruOfpInfo(rruDev, nRruPort, nBoardPort);
		}

		/// <summary>
		/// 重置RRU属性中和板卡相关的所有信息
		/// </summary>
		/// <param name="rruDev"></param>
		/// <param name="nRruPort"></param>
		/// <returns></returns>
		private bool ResetRruToBoardInfo(DevAttributeInfo rruDev, int nRruPort)
		{
			var bbi = new BoardBaseInfo();
			return SetBoardBaseInfoInRru(bbi, rruDev, nRruPort) && SetRruOfpInfo(rruDev, nRruPort, -1);
		}

		#endregion 私有函数

		#region 私有数据区

		private string m_irRecordIndex;
		private DevAttributeInfo m_boardDev;
		private DevAttributeInfo m_rruDev;
		private int m_nBoardPort;
		private int m_nRruPort;
		private readonly EnumDevType m_irRecordType;

		#endregion 私有数据区
	}
}