using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;

namespace NetPlan.DevLink
{
	public class LinkRruAnt : NetPlanLinkBase
	{
		#region 公共接口区

		public override bool AddLink(WholeLink wholeLink, ref Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo))
			{
				return false;
			}

			// 增加netRRUAntennaSettingEntry表一行记录
			var newRecord = new DevAttributeInfo(EnumDevType.rru_ant, m_strRruAntIndex);
			if (!SetRruLinkAntInfo(newRecord))
			{
				return false;
			}

			return AddDevToMap(mapMibInfo, EnumDevType.rru_ant, newRecord);
		}

		public override bool DelLink(WholeLink wholeLink, ref Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo)
		{
			return base.DelLink(wholeLink, ref mapMibInfo);
		}

		/// 验证两端连接的设备是否存在
		public override bool CheckLinkIsValid(WholeLink wholeLink, Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo)
		{
			var strRruIndex = wholeLink.GetDevIndex(EnumDevType.rru);
			if (null == strRruIndex)
			{
				Log.Error("查询连接的RRU设备索引失败");
				return false;
			}

			m_rruDev = GetDevAttributeInfo(strRruIndex, EnumDevType.rru);
			if (null == m_rruDev)
			{
				Log.Error($"索引为{strRruIndex}的RRU设备不存在");
				return false;
			}

			m_nRruPort = wholeLink.GetDevIrPort(EnumDevType.rru, EnumPortType.rru_to_ant);
			if (-1 == m_nRruPort)
			{
				Log.Error($"索引为{strRruIndex}的RRU设备连接天线端口号错误");
				return false;
			}

			// 验证天线阵安装规划表中是否已经存在相同索引的实例
			m_strRruAntIndex = $"{strRruIndex}.{m_nRruPort}";
			var record = GetDevAttributeInfo(m_strRruAntIndex, EnumDevType.rru_ant);
			if (null != record)
			{
				Log.Error($"索引为{strRruIndex}RRU{m_nRruPort}通道已经有到天线阵的连接");
				return false;
			}

			var strAntIndex = wholeLink.GetDevIndex(EnumDevType.ant);
			if (null == strAntIndex)
			{
				Log.Error("查询连接的天线阵设备失败");
				return false;
			}

			m_antDev = GetDevAttributeInfo(strAntIndex, EnumDevType.ant);
			if (null == m_antDev)
			{
				Log.Error($"索引为{strAntIndex}的天线阵设备信息查询失败");
				return false;
			}

			m_nAntPort = wholeLink.GetDevIrPort(EnumDevType.ant, EnumPortType.ant_to_rru);
			if (-1 == m_nAntPort)
			{
				Log.Error($"索引为{strAntIndex}的ANT设备连接RRU端口号设置错误");
				return false;
			}

			return true;
		}

		#endregion

		#region 私有接口

		private bool SetRruLinkAntInfo(DevAttributeInfo record)
		{
			var strAntNo = m_antDev.m_strOidIndex.Trim('.');
			if (!record.SetFieldLatestValue("netSetRRUPortAntArrayNo", strAntNo))
			{
				return false;
			}

			if (!record.SetFieldLatestValue("netSetRRUPortAntArrayPathNo", m_nAntPort.ToString()))
			{
				return false;
			}

			return true;
		}

		#endregion


		#region 私有数据区

		private DevAttributeInfo m_rruDev;
		private DevAttributeInfo m_antDev;
		private int m_nRruPort;
		private int m_nAntPort;
		private string m_strRruAntIndex;

		#endregion
	}
}
