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

			if (!SetRruLinkAntInfo(newRecord, "1"))		// todo 注意：硬编码，可能导致连接删除失败
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

			// 不能直接删除，可能带有RRU与小区的关联数据
			var newRecord = raLink.DeepClone();
			ChangeDevRecordTypeToModify(newRecord);

			if (!SetRruLinkAntInfo(newRecord, "-1"))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			mapMibInfo[m_recordType].Remove(raLink);

			Log.Debug($"删除连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"删除类型为：rru_ant，索引为：{newRecord.m_strOidIndex}的记录成功");
			AddDevToMap(mapMibInfo, m_recordType, newRecord);

			MibInfoMgr.InfoTip("删除连接成功");

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
		/// 根据
		/// </summary>
		/// <returns></returns>
		public static List<DevAttributeInfo> GetRecordsByRruNo(string strRruNo, IReadOnlyList<DevAttributeInfo> listOriginData)
		{
			return listOriginData.Where(item => IsRelateSetting(strRruNo, item)).ToList();
		}

		#endregion 公共接口区

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