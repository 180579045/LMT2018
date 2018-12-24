using LogManager;
using System.Collections.Generic;

namespace NetPlan
{
	/// <summary>
	/// 网规连接解析
	/// </summary>
	internal class NetPlanLinkParser
	{
		#region 公共接口

		internal void Clear()
		{
			lock (m_syncObj)
			{
				m_mapLinks.Clear();
			}
		}

		/// <summary>
		/// 从MIB信息中解析出所有的连接信息，保存在本地
		/// </summary>
		/// <param name="mapMibInfo"></param>
		/// <returns></returns>
		internal bool ParseLinksFromMibInfo(NPDictionary mapMibInfo)
		{
			// 解析新的信息，删除旧的信息
			Clear();

			if (null == mapMibInfo || 0 == mapMibInfo.Count)
			{
				Log.Error("传入MIB信息为空");
				return false;
			}

			if (!mapMibInfo.ContainsKey(EnumDevType.board))
			{
				Log.Error("传入的MIB信息中不包含board信息");
				return false;
			}

			var boardList = mapMibInfo[EnumDevType.board];
			if (null == boardList || boardList.Count == 0)
			{
				Log.Error("传入的MIB信息中board信息缺失");
				return false;
			}

			if (!mapMibInfo.ContainsKey(EnumDevType.board_rru))
			{
				Log.Error("没有板卡连接到RRU和RHUB的信息");
				return false;
			}

			var irOptList = mapMibInfo[EnumDevType.board_rru];
			if (null == irOptList || 0 == irOptList.Count)
			{
				Log.Error("没有板卡连接到RRU和RHUB的信息");
				return false;
			}

			List<DevAttributeInfo> rruList = null;
			if (mapMibInfo.ContainsKey(EnumDevType.rru))
			{
				rruList = mapMibInfo[EnumDevType.rru];
				if (null == rruList || 0 == rruList.Count)
				{
					goto ParseRruToAnt;
				}

				// 解析board与rru之间的连接
				var brLinkList = LinkBoardRru.ParseBoardToRruLink(rruList, boardList, irOptList);
				foreach (var brLink in brLinkList)
				{
					AddLinkToList(brLink.m_linkType, brLink);
				}
			}

			ParseRruToAnt:

			List<DevAttributeInfo> antList = null;
			List<DevAttributeInfo> rruAntCfgList = null;

			if (null == rruList || 0 == rruList.Count)
			{
				goto ParseBoardToRhub;
			}

			if (!mapMibInfo.ContainsKey(EnumDevType.rru_ant))
			{
				goto ParseBoardToRhub;
			}

			rruAntCfgList = mapMibInfo[EnumDevType.rru_ant];
			if (0 == rruAntCfgList.Count)
			{
				goto ParseBoardToRhub;
			}

			if (0 != rruAntCfgList.Count && !mapMibInfo.ContainsKey(EnumDevType.ant))
			{
				Log.Error("天线安装配置信息不为空，但天线信息为空");
				return false;
			}

			antList = mapMibInfo[EnumDevType.ant];

			// 解析rru与ant之间的连接
			var raLinkList = LinkRruAnt.ParseRruToAntLinks(rruAntCfgList, rruList, antList);
			AddLinkToList(EnumDevType.rru_ant, raLinkList);

			ParseBoardToRhub:

			// 解析board与rhub之间的连接
			if (!mapMibInfo.ContainsKey(EnumDevType.rhub))
			{
				Log.Debug("未找到RHUB设备类型信息");
				return true;
			}

			var rhubList = mapMibInfo[EnumDevType.rhub];
			if (rhubList.Count == 0)
			{
				Log.Debug("未找到RHUB设备类型信息");
				return true;
			}

			var brhubLinkList = LinkBoardRhub.ParseBoardToRhubLink(rhubList, boardList, irOptList);
			foreach (var item in brhubLinkList)
			{
				AddLinkToList(item.m_linkType, item);
			}

			if (!mapMibInfo.ContainsKey(EnumDevType.rhub_prru))
			{
				Log.Debug("未找到rhub到pico设备的连接信息");
				return true;
			}

			var ethList = mapMibInfo[EnumDevType.rhub_prru];

			// 解析rhub与pico之间的连接
			// 在mib中，netRRUOfp1AccessEthernetPort表示连接的是对端Hub eth口
			var hpLinkList = LinkRhubPico.ParseRhubToPicoLink(rhubList, rruList, ethList);
			AddLinkToList(EnumDevType.rhub_prru, hpLinkList);

			// 解析pico和ant之间的连接
			var paLinkList = LinkRruAnt.ParsePicoToAntLink(rruList, antList, rruAntCfgList);
			AddLinkToList(EnumDevType.prru_ant, paLinkList);

			return true;
		}

		/// <summary>
		/// 获取所有的连接
		/// </summary>
		/// <returns></returns>
		internal Dictionary<EnumDevType, List<WholeLink>> GetAllLink()
		{
			lock (m_syncObj)
			{
				return m_mapLinks;
			}
		}

		internal NetPlanLinkParser()
		{
			m_mapLinks = new Dictionary<EnumDevType, List<WholeLink>>();
		}

		internal void AddLinkToList(EnumDevType linkType, WholeLink link)
		{
			lock (m_syncObj)
			{
				if (m_mapLinks.ContainsKey(linkType))
				{
					m_mapLinks[linkType].Add(link);
				}
				else
				{
					var listLink = new List<WholeLink> { link };
					m_mapLinks[linkType] = listLink;
				}
			}
		}

		#endregion 公共接口

		#region 私有数据区

		/// <summary>
		/// 增加连接到列表中
		/// </summary>
		/// <param name="linkType"></param>
		/// <param name="linkList"></param>
		private void AddLinkToList(EnumDevType linkType, List<WholeLink> linkList)
		{
			lock (m_syncObj)
			{
				if (m_mapLinks.ContainsKey(linkType))
				{
					m_mapLinks[linkType].AddRange(linkList);
				}
				else
				{
					var listLink = new List<WholeLink>();
					listLink.AddRange(linkList);
					m_mapLinks[linkType] = listLink;
				}
			}
		}

		private readonly Dictionary<EnumDevType, List<WholeLink>> m_mapLinks;

		private readonly object m_syncObj = new object();

		#endregion 私有数据区
	}

	public class ParsedRruInfo
	{
		public string strBoardIndex;
		public int nBoardIrPort;
		public string strRruIndex;
		public int nRruIrPort;
		public int nCurLinePos;           // 这个RRU级联级数
	}

	public class PriComparer : IComparer<ParsedRruInfo>
	{
		public int Compare(ParsedRruInfo left, ParsedRruInfo right)
		{
			if (null == left && null == right)
			{
				return 0;
			}

			if (null == left)
			{
				return -1;
			}

			if (null == right)
			{
				return 1;
			}

			if (left.nCurLinePos == right.nCurLinePos)
			{
				return 0;
			}

			if (left.nCurLinePos < right.nCurLinePos)
			{
				return -1;
			}

			return 1;
		}
	}
}