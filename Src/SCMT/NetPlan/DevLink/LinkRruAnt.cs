using System;
using System.Collections.Generic;
using System.Linq;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.DevLink
{
	public class LinkRruAnt : NetPlanLinkBase
	{
		#region 公共接口区

		public override bool AddLink(WholeLink wholeLink, ref Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, null))     // 这里传入null，因为rru和本地小区的关联也会添加rru_ant类型的记录，此处直接跳过不检查
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			// 增加netRRUAntennaSettingEntry表一行记录
			DevAttributeInfo newRecord;
			var oldRecord = GetDevAttributeInfo(m_strRruAntIndex, m_recordType);
			if (null != oldRecord)
			{
				newRecord = oldRecord.DeepClone();
			}
			else
			{
				newRecord = new DevAttributeInfo(m_recordType, m_strRruAntIndex);
			}

			ChangeDevRecordTypeToModify(newRecord);

			if (!SetRruLinkAntInfo(newRecord, "1"))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			if (null != oldRecord)
			{
				mapMibInfo[m_recordType].Remove(oldRecord);
			}

			AddDevToMap(mapMibInfo, m_recordType, newRecord);

			Log.Debug($"添加连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"添加类型为：rru_ant，索引为：{newRecord.m_strOidIndex}的记录成功");

			MibInfoMgr.InfoTip("添加连接成功");

			return true;
		}

		public override bool DelLink(WholeLink wholeLink, ref Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			var raLink = GetDevAttributeInfo(m_strRruAntIndex, m_recordType);

			// 直接删除，如果布配了本地小区，会校验失败，流程走不到这里
			//var newRecord = raLink.DeepClone();
			MibInfoMgr.DelDevFromMap(mapMibInfo, EnumDevType.rru_ant, raLink);

			//if (!SetRruLinkAntInfo(newRecord, "-1"))
			//{
			//	MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
			//	return false;
			//}

			Log.Debug($"删除连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"删除类型为：rru_ant，索引为：{raLink.m_strOidIndex}的记录成功");
			//AddDevToMap(mapMibInfo, m_recordType, newRecord);

			//MibInfoMgr.InfoTip("删除连接成功");		// todo 如果是64通道，会调用64次操作，打印64个消息，需要UI模块处理

			return true;
		}

		/// 验证两端连接的设备是否存在
		public override bool CheckLinkIsValid(WholeLink wholeLink, Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo, IsRecordExist checkExist)
		{
			var strRruIndex = wholeLink.GetDevIndex(EnumDevType.rru);
			if (null == strRruIndex)
			{
				m_strLatestError = "查询连接的rru设备索引失败";
				return false;
			}

			m_rruDev = GetDevAttributeInfo(strRruIndex, EnumDevType.rru);
			if (null == m_rruDev)
			{
				m_strLatestError = $"索引为{strRruIndex}的rru设备不存在";
				return false;
			}

			m_nRruPort = wholeLink.GetDevIrPort(EnumDevType.rru, EnumPortType.rru_to_ant);
			if (-1 == m_nRruPort)
			{
				m_strLatestError = $"索引为{strRruIndex}的rru设备连接天线阵端口号错误";
				return false;
			}

			// 验证天线阵安装规划表中是否已经存在相同索引的实例
			m_strRruAntIndex = $"{strRruIndex}.{m_nRruPort}";
			if (null != checkExist)
			{
				if (!checkExist.Invoke(m_strRruAntIndex, m_recordType))
				{
					var tmp = checkExist == RecordExistInDel ? "不" : "已";
					m_strLatestError = $"索引为{m_strRruAntIndex}的天线阵安装规划表记录{tmp}存在";
					return false;
				}
			}

			var strAntIndex = wholeLink.GetDevIndex(EnumDevType.ant);
			if (null == strAntIndex)
			{
				m_strLatestError = "查询连接的天线阵设备失败";
				return false;
			}

			m_antDev = GetDevAttributeInfo(strAntIndex, EnumDevType.ant);
			if (null == m_antDev)
			{
				m_strLatestError = $"索引为{strAntIndex}的天线阵设备信息查询失败";
				return false;
			}

			m_nAntPort = wholeLink.GetDevIrPort(EnumDevType.ant, EnumPortType.ant_to_rru);
			if (-1 == m_nAntPort)
			{
				m_strLatestError = $"索引为{strAntIndex}的天线阵连接rru设备端口号错误";
				return false;
			}

			return true;
		}

		/// <summary>
		/// 查询天线阵安装规划表中的一条记录
		/// </summary>
		/// <param name="wholeLink"></param>
		/// <param name="mapMibInfo"></param>
		/// <returns></returns>
		public override DevAttributeInfo GetRecord(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return null;
			}

			return GetDevAttributeInfo(m_strRruAntIndex, m_recordType);
		}

		/// <summary>
		/// 判断天线阵安装规划表是否连接了天线阵，如果没有连接天线阵，则这个记录不下发
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		public static bool RruHasConnectToAnt(DevAttributeInfo record)
		{
			if (null == record)
			{
				throw new ArgumentNullException();
			}

			if (record.m_enumDevType != EnumDevType.rru_ant)
			{
				return true;
			}

			var antNo = record.GetNeedUpdateValue("netSetRRUPortAntArrayNo");
			var antPathNo = record.GetNeedUpdateValue("netSetRRUPortAntArrayPathNo");
			if (null == antNo || "-1" == antNo)
			{
				return false;
			}

			if (null == antPathNo || "-1" == antPathNo)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 根据rru编号查询天线阵安装规划表中的相关记录
		/// </summary>
		/// <returns></returns>
		public static List<DevAttributeInfo> GetRecordsByRruNo(string strRruNo, IReadOnlyList<DevAttributeInfo> listOriginData)
		{
			return listOriginData.Where(item => IsRelateSetting(strRruNo, item)).ToList();
		}

		/// <summary>
		/// 根据天线阵编号查询对应的天线阵安装规划表记录
		/// </summary>
		/// <param name="strAntNo"></param>
		/// <param name="listOriginData"></param>
		/// <returns></returns>
		public static List<DevAttributeBase> GetRecordsByAntNo(string strAntNo, IReadOnlyList<DevAttributeInfo> listOriginData)
		{
			return (from item in listOriginData
				let antNoInMib = item.GetNeedUpdateValue("netSetRRUPortAntArrayNo")
				where null != antNoInMib && strAntNo == antNoInMib select item)
				.Cast<DevAttributeBase>()
				.ToList();
		}

		#endregion 公共接口区

		#region 静态接口区

		/// <summary>
		/// 解析rru与天线的连接
		/// </summary>
		/// <param name="rruAntCfgList">天线阵安装规划表记录列表</param>
		/// <param name="rruDevList">rru设备列表</param>
		/// <param name="antDevList">天线阵设备列表</param>
		/// <returns></returns>
		public static List<WholeLink> ParseRruToAntLinks(IReadOnlyCollection<DevAttributeInfo> rruAntCfgList,
			IReadOnlyCollection<DevAttributeInfo> rruDevList,
			IReadOnlyCollection<DevAttributeInfo> antDevList)
		{
			var wlinkList = new List<WholeLink>();

			// 遍历天线阵安装规划表
			if (null == rruAntCfgList || 0 == rruAntCfgList.Count)
			{
				return wlinkList;
			}

			if (null == rruDevList || 0 == rruDevList.Count)
			{
				Log.Error("天线阵安装规划表信息不为空，但RRU信息为空");
				return wlinkList;
			}

			if (null == antDevList || 0 == antDevList.Count)
			{
				Log.Error("天线阵安装规划表信息不为空，但天线信息为空");
				return wlinkList;
			}

			foreach (var ra in rruAntCfgList)
			{
				var rruNo = ra.GetFieldOriginValue("netSetRRUNo");
				var rruIndex = $".{rruNo}";

				// 根据rru索引查找rru设备，如果未找到就认为存在错误
				var rru = rruDevList.FirstOrDefault(item => item.m_strOidIndex == rruIndex);
				if (null == rru)
				{
					Log.Error($"根据rru索引{rruIndex}未找到对应的rru设备");
					continue;
				}

				var rruPathNo = ra.GetFieldOriginValue("netSetRRUPortNo");

				var antNo = ra.GetFieldOriginValue("netSetRRUPortAntArrayNo");
				if ("-1" == antNo)
				{
					Log.Debug($"编号为{rruNo}的rru通道{rruPathNo}未连接天线");
					continue;
				}

				var antIndex = $".{antNo}";

				// 根据ant索引查找ant设备
				var ant = antDevList.FirstOrDefault(item => item.m_strOidIndex == antIndex);
				if (null == ant)
				{
					Log.Error($"根据ant索引{antIndex}未找到对应的ant设备");
					continue;
				}

				var antPathNo = ra.GetFieldOriginValue("netSetRRUPortAntArrayPathNo");
				if ("-1" == antPathNo)
				{
					Log.Error($"编号为{rruNo}的rru通道{rruPathNo}连接天线阵{antNo}的通道未设置");
					continue;
				}

				var tmpLink = GenerateRruToAntLink(rruIndex, int.Parse(rruPathNo), antIndex, int.Parse(antPathNo));
				wlinkList.Add(tmpLink);
				//Log.Debug($"生成索引为{rruIndex}的rru通道{rruPathNo}到索引为{antIndex}的ant通道{antPathNo}连接成功");
			}

			return wlinkList;        // 如果中间有break调用，这里就会返回false
		}

		/// <summary>
		/// 生成一个rru到天线阵的连接
		/// </summary>
		/// <param name="strRruIndex"></param>
		/// <param name="nRruPort"></param>
		/// <param name="strAntIndex"></param>
		/// <param name="nAntPathNo"></param>
		/// <returns></returns>
		private static WholeLink GenerateRruToAntLink(string strRruIndex, int nRruPort, string strAntIndex, int nAntPathNo)
		{
			var rruEndpoint = new LinkEndpoint
			{
				devType = EnumDevType.rru,
				strDevIndex = strRruIndex,
				nPortNo = nRruPort,
				portType = EnumPortType.rru_to_ant
			};

			var antEndPoint = new LinkEndpoint
			{
				devType = EnumDevType.ant,
				strDevIndex = strAntIndex,
				nPortNo = nAntPathNo,
				portType = EnumPortType.ant_to_rru
			};

			return new WholeLink(rruEndpoint, antEndPoint, EnumDevType.rru_ant);
		}

		/// <summary>
		/// 生成一条pico到天线阵的连接
		/// </summary>
		/// <param name="strPicoIndex"></param>
		/// <param name="nPicoPort"></param>
		/// <param name="strAntIndex"></param>
		/// <param name="nAntPort"></param>
		/// <returns></returns>
		private static WholeLink GeneratePicoToAntLink(string strPicoIndex, int nPicoPort, string strAntIndex, int nAntPort)
		{
			var pico = new LinkEndpoint
			{
				devType = EnumDevType.rru,
				strDevIndex = strPicoIndex,
				nPortNo = nPicoPort,
				portType = EnumPortType.pico_to_ant
			};

			var ant = new LinkEndpoint
			{
				devType = EnumDevType.ant,
				strDevIndex = strAntIndex,
				nPortNo = nAntPort,
				portType = EnumPortType.ant_to_pico
			};

			return new WholeLink(pico, ant, EnumDevType.prru_ant);
		}

		/// <summary>
		/// 解析pico到ant之间的连接
		/// </summary>
		/// <param name="rruList"></param>
		/// <param name="antList"></param>
		/// <param name="rruAntCfgList">天线阵安装规划表中的信息</param>
		/// <returns></returns>
		public static List<WholeLink> ParsePicoToAntLink(IEnumerable<DevAttributeInfo> rruList, List<DevAttributeInfo> antList,
			List<DevAttributeInfo> rruAntCfgList)
		{
			var raLinkList = new List<WholeLink>();
			if (null == rruList || null == antList || null == rruAntCfgList)
			{
				Log.Error("解析pico到天线阵的连接信息缺失");
				return raLinkList;
			}

			foreach (var rru in rruList)
			{
				var rruType = rru.GetFieldOriginValue("netRRUTypeIndex");
				if (!NPERruHelper.GetInstance().IsPicoDevice(int.Parse(rruType)))
				{
					// rru不是Pico，直接跳过
					continue;
				}

				foreach (var cfg in rruAntCfgList)
				{
					var rruNoInCfg = cfg.GetFieldOriginValue("netSetRRUNo");
					if (rruNoInCfg != rru.m_strOidIndex)
					{
						continue;
					}

					var picoToAntPort = cfg.GetFieldOriginValue("netSetRRUPortNo");
					var picoPort = int.Parse(picoToAntPort);

					var antNo = cfg.GetFieldOriginValue("netSetRRUPortAntArrayNo");
					if ("-1" == antNo)
					{
						Log.Debug($"索引为{rru.m_strOidIndex}pico设备{picoToAntPort}通道没有连接天线阵");
						continue;
					}

					var antPort = cfg.GetFieldOriginValue("netSetRRUPortAntArrayPathNo");
					if ("-1" == antPort)
					{
						Log.Error($"索引为.{antNo}的天线通道配置错误");
						continue;
					}

					var antIr = int.Parse(antPort);

					var antDev = antList.FirstOrDefault(ant => ant.m_strOidIndex == $".{antNo}");
					if (null == antDev)
					{
						Log.Error($"根据索引.{antNo}未找到天线设备");
						continue;
					}

					var tmpLink = GeneratePicoToAntLink(rru.m_strOidIndex, picoPort, antDev.m_strOidIndex, antIr);
					if (null != tmpLink)
					{
						raLinkList.Add(tmpLink);
					}
				}
			}

			return raLinkList;
		}

		#endregion


		#region 私有接口

		/// <summary>
		/// 设置天线阵安装规划表中信息
		/// </summary>
		/// <param name="record"></param>
		/// <param name="strValue">重置的值。如果为-1，表示重置</param>
		/// <returns></returns>
		private bool SetRruLinkAntInfo(DevAttributeInfo record, string strValue)
		{
			var strAntNo = m_antDev.m_strOidIndex.Trim('.'); ;
			var strPathNo = m_nAntPort.ToString();
			if ("-1" == strValue)
			{
				strAntNo = "-1";
				strPathNo = "-1";
			}

			var bSucceed = record.SetFieldLatestValue("netSetRRUPortAntArrayNo", strAntNo);
			if (!bSucceed)
			{
				m_strLatestError = $"设置索引为{record.m_strOidIndex}的天线阵安装规划表记录字段netSetRRUPortAntArrayNo值{strAntNo}失败";
				return false;
			}

			bSucceed = record.SetFieldLatestValue("netSetRRUPortAntArrayPathNo", strPathNo);
			if (!bSucceed)
			{
				m_strLatestError = $"设置索引为{record.m_strOidIndex}的天线阵安装规划表记录字段netSetRRUPortAntArrayPathNo值{strPathNo}失败";
				return false;
			}

			return true;
		}

		private static bool IsRelateSetting(string strRruNo, DevAttributeBase rruAntSetting)
		{
			var rruNoInSetting = rruAntSetting.GetNeedUpdateValue("netSetRRUNo");
			return strRruNo == rruNoInSetting;
		}

		#endregion 私有接口

		#region 私有数据区

		private DevAttributeInfo m_rruDev;
		private DevAttributeInfo m_antDev;
		private int m_nRruPort;
		private int m_nAntPort;
		private string m_strRruAntIndex;
		private const EnumDevType m_recordType = EnumDevType.rru_ant;

		#endregion 私有数据区
	}
}