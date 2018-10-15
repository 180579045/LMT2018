using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace NetPlan
{
	// MIB信息管理类，单例
	public class MibInfoMgr : Singleton<MibInfoMgr>
	{
		#region 公共接口

		/// <summary>
		/// 保存一类设备的所欲属性
		/// </summary>
		/// <param name="type"></param>
		/// <param name="listDevInfo"></param>
		public void AddDevMibInfo(EnumDevType type, List<DevAttributeInfo> listDevInfo)
		{
			if (null == listDevInfo || listDevInfo.Count == 0)
			{
				return;
			}

			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(type))
				{
					m_mapAllMibData[type].AddRange(listDevInfo);	// 已经存在同类的设备信息，直接添加
				}
				else
				{
					m_mapAllMibData[type] = listDevInfo;			// 还不存在同类的设备信息，直接保存
				}
			}
		}

		/// <summary>
		/// 增加一个设备属性
		/// </summary>
		/// <param name="type"></param>
		/// <param name="devInfo"></param>
		public void AddDevMibInfo(EnumDevType type, DevAttributeInfo devInfo)
		{
			if (null == devInfo)
			{
				return;
			}

			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(type))
				{
					m_mapAllMibData[type].Add(devInfo);
				}
				else
				{
					var listDev = new List<DevAttributeInfo> {devInfo};
					m_mapAllMibData[type] = listDev;
				}
			}
		}




		#endregion

		#region 私有接口

		private MibInfoMgr()
		{
			m_mapAllMibData = new Dictionary< EnumDevType, List<DevAttributeInfo> >();
		}


		#endregion


		#region 私有成员、数据

		private Dictionary< EnumDevType, List<DevAttributeInfo> > m_mapAllMibData;	// 保存所有的网规MIB数据
		private readonly object _syncObj = new object();

		#endregion
	}
}
