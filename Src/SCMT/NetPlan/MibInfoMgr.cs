using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

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

		/// <summary>
		/// 增加一个新的天线设备
		/// </summary>
		/// <param name="nIndex">设备序号</param>
		/// <param name="type"></param>
		/// <returns>null:添加失败</returns>
		public DevAttributeInfo AddNewAnt(int nIndex, EnumDevType type = EnumDevType.ant)
		{
			var ant = new DevAttributeInfo(type, nIndex);
			if (ant.m_mapAttributes.Count == 0)
			{
				return null;
			}

			var antIndex = ant.m_strOidIndex;
			if (HasSameIndexDev(type, antIndex))
			{
				return null;
			}

			// 新加的设备，要判断索引和待删除列表中的是否一致
			// 目的：防止先删除所有的同类设备，然后重新添加，但是所有的属性设置的又相同的情况
			lock (_syncObj)
			{
				if (!m_mapWaitDelDev.ContainsKey(type))
				{
					AddDevToMap(m_mapNewAddDev, type, ant);
					return ant;
				}

				var devList = m_mapWaitDelDev[type];
				foreach (var dev in devList)
				{
					// 如果在待删除的列表中，就直接把新创建的设备放到已修改的列表中
					if (dev.m_strOidIndex != antIndex) continue;

					devList.Remove(dev);
					AddDevToMap(m_mapModifyDev, type, ant);
					return ant;
				}

				// 如果在待删除列表中没有找到，就加入到新添加设备列表
				AddDevToMap(m_mapNewAddDev, type, ant);
			}

			return ant;
		}

		#endregion

		#region 私有接口

		private MibInfoMgr()
		{
			m_mapAllMibData = new MAP_DEVTYPE_DEVATTRI();
			m_mapModifyDev = new MAP_DEVTYPE_DEVATTRI();
			m_mapNewAddDev = new MAP_DEVTYPE_DEVATTRI();
			m_mapWaitDelDev = new MAP_DEVTYPE_DEVATTRI();
		}

		/// <summary>
		/// 判断已修改、所有、新添加的设备列表中是否存在与给定的索引相同设备
		/// </summary>
		/// <param name="devType"></param>
		/// <param name="strIndex"></param>
		/// <returns></returns>
		private bool HasSameIndexDev(EnumDevType devType, string strIndex)
		{
			lock (_syncObj)
			{
				if (m_mapModifyDev.ContainsKey(devType))
				{
					var modDevList = m_mapModifyDev[devType];
					if (HasSameIndexDev(modDevList, strIndex))
					{
						return true;
					}
				}

				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					if (HasSameIndexDev(devList, strIndex))
					{
						return true;
					}
				}

				if (m_mapNewAddDev.ContainsKey(devType))
				{
					var devList = m_mapNewAddDev[devType];
					if (HasSameIndexDev(devList, strIndex))
					{
						return true;
					}
				}
			}

			return false;
		}

		// 判断给定的列表中是否存在与strIndex相同索引的设备
		private bool HasSameIndexDev(List<DevAttributeInfo> devList, string strIndex)
		{
			return null != devList && devList.Any(dev => dev.m_strOidIndex == strIndex);
		}

		/// <summary>
		/// 从给定的map删除索引为strIndex、类型为devType的记录
		/// </summary>
		/// <param name="mapData"></param>
		/// <param name="devType"></param>
		/// <param name="strIndex"></param>
		private void DelDevFromMap(MAP_DEVTYPE_DEVATTRI mapData, EnumDevType devType, string strIndex)
		{
			if (null == mapData)
			{
				return;
			}

			if (!mapData.ContainsKey(devType))
			{
				return;
			}

			var devList = mapData[devType];
			foreach (var dev in devList)
			{
				if (dev.m_strOidIndex != strIndex) continue;

				devList.Remove(dev);
				mapData[devType] = devList;
				break;
			}
		}

		/// <summary>
		/// 把类型为devType的Dev添加到给定的map中
		/// </summary>
		/// <param name="mapData"></param>
		/// <param name="devType"></param>
		/// <param name="dev"></param>
		private void AddDevToMap(MAP_DEVTYPE_DEVATTRI mapData, EnumDevType devType, DevAttributeInfo dev)
		{
			if (null == mapData)
			{
				return;
			}

			if (mapData.ContainsKey(devType))
			{
				mapData[devType].Add(dev);
			}
			else
			{
				var devList = new List<DevAttributeInfo> {dev};
				mapData[devType] = devList;
			}
		}

		#endregion


		#region 私有成员、数据

		// 保存所有的网规MIB数据。开始保存的是从设备中查询回来的信息，如果对这些信息进行了修改、删除，就从这个数据结构中移动到对应的结构中
		// 最后下发网规信息时，就不再下发这个数据结构中的信息
		private MAP_DEVTYPE_DEVATTRI m_mapAllMibData;

		private readonly object _syncObj = new object();

		// 下发网规信息时，这个数据结构中的设备只下发 RowStatus = 6 这一行数据
		private MAP_DEVTYPE_DEVATTRI m_mapWaitDelDev;	// 待删除的设备列表

		// 下发网规信息时，这个数据结构中的设备 RowStatus = 4,然后调用 Set 命令进行下发
		private MAP_DEVTYPE_DEVATTRI m_mapNewAddDev;     // 新增的设备列表

		// 下发网规信息时，这个数据结构中的信息只下发 DevAttributeInfo 结构中的 m_listModifyField 列表中的字段
		private MAP_DEVTYPE_DEVATTRI m_mapModifyDev;		// 已修改属性的设备列表

		#endregion
	}
}
