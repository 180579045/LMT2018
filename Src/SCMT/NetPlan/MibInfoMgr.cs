using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using SCMTOperationCore.Control;
using SCMTOperationCore.Elements;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
	// MIB信息管理类，单例
	public class MibInfoMgr : Singleton<MibInfoMgr>
	{
		public delegate string GetMibValue(string strOriginValue, string strLatestValue);

		#region 公共接口

		/// <summary>
		/// 查询所有enb中获取的网规信息
		/// 调用时机：打开网规页面初始化成功后
		/// </summary>
		/// <returns></returns>
		public MAP_DEVTYPE_DEVATTRI GetAllEnbInfo()
		{
			return m_mapAllMibData;
		}

		/// <summary>
		/// 获取一个指定类型和索引的设备信息，里面保存了所有的设备属性
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public DevAttributeInfo GetDevAttributeInfo(string strIndex, EnumDevType type)
		{
			if (string.IsNullOrEmpty(strIndex) || EnumDevType.unknown == type)
			{
				return null;
			}

			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(type))
				{
					var devList = m_mapAllMibData[type];
					var dev = GetSameIndexDev(devList, strIndex);
					if (null != dev)
					{
						return dev;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// 根据设备类型、索引、属性名查找该属性的值
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="type"></param>
		/// <param name="strAttriName"></param>
		/// <returns></returns>
		public string GetDevAttributeValue(string strIndex, EnumDevType type, string strAttriName)
		{
			if (string.IsNullOrEmpty(strAttriName))
			{
				throw new CustomException($"传入参数无效");
			}

			var devWithSameIndex = GetDevAttributeInfo(strIndex, type);
			if (null == devWithSameIndex)
			{
				Log.Error($"未找到索引为 {strIndex} 类型为 {type.ToString()} 的设备");
				return null;
			}

			var mapAttri = devWithSameIndex.m_mapAttributes;
			if (null == mapAttri || 0 == mapAttri.Count)
			{
				Log.Error($"索引为 {strIndex} 类型为 {type.ToString()} 的设备属性信息为空");
				return null;
			}

			var strAttriValue = GetEnumStringByMibName(mapAttri, strAttriName);
			if (null == strAttriValue)
			{
				Log.Error($"索引为 {strIndex} 类型为 {type.ToString()} 的设备属性 {strAttriName} 值为null");
				return null;
			}

			return strAttriValue;
		}

		public string GetDevAttributeValue(DevAttributeInfo dev, string strAttriName)
		{
			if (null == dev || string.IsNullOrEmpty(strAttriName))
			{
				throw new CustomException("传入参数无效");
			}

			var strAttriValue = GetEnumStringByMibName(dev.m_mapAttributes, strAttriName);
			return strAttriValue;
		}

		/// <summary>
		/// 保存一类设备的所有属性
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
			var ant = GerenalNewDev(type, nIndex);
			if (null == ant)
			{
				return null;
			}

			if (!MoveDevFromWaitDelToModifyMap(type, ant, ant.m_strOidIndex))
			{
				return null;
			}

			return ant;
		}

		/// <summary>
		/// 新增一个板卡
		/// </summary>
		/// <param name="slot">插槽号</param>
		/// <param name="strBoardType">板卡类型</param>
		/// <param name="strWorkMode">工作模式</param>
		/// <param name="strIrFrameType">帧结构</param>
		/// <returns></returns>
		public DevAttributeInfo AddNewBoard(int slot, string strBoardType, string strWorkMode, string strIrFrameType)
		{
			const EnumDevType type = EnumDevType.board;
			var dev = GerenalNewDev(type, slot);
			if (null == dev)
			{
				return null;
			}

			if (!dev.SetFieldOriginValue("netBoardType", strBoardType, false) ||
			    !dev.SetFieldOriginValue("netBoardWorkMode", strWorkMode, false) ||
			    !dev.SetFieldOriginValue("netBoardIrFrameType", strIrFrameType, false))
			{
				return null;
			}

			if (!MoveDevFromWaitDelToModifyMap(type, dev, dev.m_strOidIndex))
			{
				return null;
			}

			return dev;
		}

		/// <summary>
		/// 增加新的RRU设备
		/// </summary>
		/// <param name="seqIndexList">RRU编号列表</param>
		/// <param name="nRruType">RRU设备类型索引</param>
		/// <param name="strWorkMode">工作模式</param>
		/// <returns></returns>
		public List<DevAttributeInfo> AddNewRru(List<int> seqIndexList, int nRruType, string strWorkMode)
		{
			if (null == seqIndexList || string.IsNullOrEmpty(strWorkMode))
			{
				return null;
			}

			var type = EnumDevType.rru;
			var rruList = new List<DevAttributeInfo>();
			foreach (var seqIndex in seqIndexList)
			{
				var newRru = GerenalNewDev(type, seqIndex);
				if (null == newRru)
				{
					return null;
				}

				if (!newRru.SetFieldOriginValue("netRRUTypeIndex", nRruType.ToString(), false) ||
					!newRru.SetFieldOriginValue("netRRUOfpWorkMode", strWorkMode, false))
				{
					return null;
				}

				if (!MoveDevFromWaitDelToModifyMap(type, newRru, newRru.m_strOidIndex))
				{
					return null;
				}

				rruList.Add(newRru);
			}

			return rruList;
		}

		/// <summary>
		/// 增加新的RHUB设备
		/// </summary>
		/// <param name="seqIndexList"></param>
		/// <returns></returns>
		public List<DevAttributeInfo> AddNewRhub(List<int> seqIndexList)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 新增本地小区
		/// </summary>
		/// <param name="nLocalCellId"></param>
		/// <returns></returns>
		public DevAttributeInfo AddNewLocalCell(int nLocalCellId)
		{
			const EnumDevType type = EnumDevType.nrNetLc;
			var dev = GerenalNewDev(type, nLocalCellId);
			if (null == dev)
			{
				return null;
			}

			if (!MoveDevFromWaitDelToModifyMap(type, dev, dev.m_strOidIndex))
			{
				return null;
			}

			return dev;
		}

		/// <summary>
		/// 增加本地小区布配开关类型的信息
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="strSwitchValue"></param>
		/// <returns></returns>
		public DevAttributeInfo AddNewNetLcCtrlSwitch(string strIndex, string strSwitchValue)
		{
			var type = EnumDevType.nrNetLcCtr;
			var dev = new DevAttributeInfo(type, strIndex);

			if (!dev.SetFieldOriginValue("nrNetLocalCellCtrlConfigSwitch", strSwitchValue, false))
			{
				Log.Error($"设置字段 nrNetLocalCellCtrlConfigSwitch 的 OriginValue 为{strSwitchValue}失败");
				return null;
			}

			dev.m_recordType = RecordDataType.Original;
			lock (_syncObj)
			{
				AddDevToMap(m_mapAllMibData, type, dev);
			}
			return dev;
		}

		/// <summary>
		/// 删除类型为devType，索引为strIndex的设备
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="devType"></param>
		public bool DelDev(string strIndex, EnumDevType devType)
		{
			if (string.IsNullOrEmpty(strIndex) || EnumDevType.unknown == devType)
			{
				Log.Error($"传入设备索引{strIndex}或类型{devType.ToString()}错误");
				return false;
			}

			var dev = GetDevAttributeInfo(strIndex, devType);
			if (null == dev)
			{
				Log.Error($"未找到类型为{devType.ToString()}索引为{strIndex}的记录");
				return false;
			}

			if (RecordDataType.NewAdd != dev.m_recordType)  // 不是新加的设备就设置为WaitDev
			{
				dev.m_recordType = RecordDataType.WaitDel;
				Log.Debug($"找到类型为{devType.ToString()}索引为{strIndex}的记录，设置记录数据状态为：{dev.m_recordType}");
				return true;
			}

			Log.Debug($"找到类型为{devType.ToString()}索引为{strIndex}的记录，旧的记录类型为NewAdd，直接删除");
			lock (_syncObj)
			{
				DelDevFromMap(m_mapAllMibData, devType, dev);
			}
			return true;
		}

		/// <summary>
		/// 直接从内存中删除符合条件的
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="devType"></param>
		/// <returns></returns>
		public bool DevDevFromMemory(string strIndex, EnumDevType devType)
		{
			if (string.IsNullOrEmpty(strIndex) || EnumDevType.unknown == devType)
			{
				throw new CustomException($"传入设备索引{strIndex}或类型{devType.ToString()}错误");
			}

			var dev = GetDevAttributeInfo(strIndex, devType);
			if (null == dev)
			{
				Log.Error($"未找到类型为{devType.ToString()}索引为{strIndex}的记录");
				return false;
			}

			lock (_syncObj)
			{
				DelDevFromMap(m_mapAllMibData, devType, dev);
			}
			return true;
		}

		/// <summary>
		/// 设置指定设备的属性值
		/// </summary>
		/// <param name="strIndex">设备索引号</param>
		/// <param name="strFieldName">字段英文名</param>
		/// <param name="strValue">字段值</param>
		/// <param name="devType">设备类型</param>
		/// <returns></returns>
		public bool SetDevAttributeValue(string strIndex, string strFieldName, string strValue, EnumDevType devType)
		{
			if (string.IsNullOrEmpty(strIndex) || EnumDevType.unknown == devType)
			{
				return false;
			}

			lock (_syncObj)
			{
				// 从基站中查询到设备修改属性，就需要移动到modify队列，等待参数下发
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					var dev = GetSameIndexDev(devList, strIndex);
					if (null != dev)
					{
						dev.SetFieldValue(strFieldName, strValue);
						if (RecordDataType.NewAdd != dev.m_recordType)
						{
							dev.m_recordType = RecordDataType.Modified;
						}
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// 下发板卡信息到基站
		/// TODO 基站类型需要获取
		/// </summary>
		/// <returns></returns>
		public bool DistributeBoardInfoToEnb(EnumDevType devType)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("下发网规参数失败，尚未选中基站");
			}

			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(devType) && m_mapAllMibData[devType].Count > 0)
				{
					var mibList = m_mapAllMibData[devType];
					foreach (var item in mibList)
					{
						if (RecordDataType.Original == item.m_recordType)
						{
							continue;
						}

						var cmdType = EnumSnmpCmdType.Invalid;
						if (RecordDataType.NewAdd == item.m_recordType)
						{
							cmdType = EnumSnmpCmdType.Add;
						}
						else if (RecordDataType.Modified == item.m_recordType)
						{
							cmdType = EnumSnmpCmdType.Set;
						}
						else if (RecordDataType.WaitDel == item.m_recordType)
						{
							cmdType = EnumSnmpCmdType.Del;
						}

						if (!DistributeSnmpData(item, cmdType, targetIp))
						{
							Log.Error($"类型为{devType.ToString()}，索引为{item.m_strOidIndex}的网规信息下发{cmdType.ToString()}失败");
						}
						else
						{
							item.m_recordType = RecordDataType.Original;	// 下发成功的都设置为原始数据
							Log.Debug($"类型为{devType.ToString()}，索引为{item.m_strOidIndex}的网规信息下发{cmdType.ToString()}成功");
						}
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 根据rru设备的索引获取该RRU每个通道上配置的本地小区配置信息
		/// </summary>
		/// <param name="strIndex"></param>
		public Dictionary<string, NPRruToCellInfo> GetNetLcInfoByRruIndex(string strIndex)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("尚未选中基站");
			}

			var retMap = new Dictionary<string, NPRruToCellInfo>();
			var devType = EnumDevType.rru_ant;
			var rruNo = int.Parse(strIndex.Trim('.'));
			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					foreach (var item in devList)
					{
						GetRruPortToCellInfo(item, rruNo, ref retMap, (RecordDataType.Original == item.m_recordType));
					}
				}
			}

			return retMap;
		}

		/// <summary>
		/// 配置RRU通道与本地小区的关联关系
		/// </summary>
		/// <returns></returns>
		public bool SetNetLcInfo(string strRruIndex, Dictionary<string, NPRruToCellInfo> portToCellInfoList)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("尚未选中基站");
			}

			var devType = EnumDevType.rru_ant;
			var rruNo = strRruIndex.Trim('.');
			var waitRemoveList = new List<string>();

			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					foreach (var kv in portToCellInfoList) // 遍历传入的所有值
					{
						var strIndex = $".{rruNo}.{kv.Key}"; // 用rru编号和port编号组成索引
						var dev = GetSameIndexDev(devList, strIndex);
						if (null != dev)
						{
							var mapAttri = dev.m_mapAttributes;

							SetRruAntSettingTableInfo(mapAttri, kv.Value);
							if (dev.m_recordType != RecordDataType.NewAdd)
							{
								dev.m_recordType = RecordDataType.Modified;
							}
						}
					}
				}
			}

			// 如果还有信息没有用完，就new一个rru_ant对象，存入m_mapNewAdd
			if (portToCellInfoList.Count > 0)
			{
				foreach (var item in portToCellInfoList)
				{
					var newDev = AddNewRruAntDev(rruNo, item.Key, item.Value);
					if (null == newDev)
					{
						Log.Error("新加天线阵安装规划表实例失败");
						return false;
					}
					lock (_syncObj)
					{
						AddDevToMap(m_mapAllMibData, EnumDevType.rru_ant, newDev);
					}
				}
			}

			return true;
		}

		public DevAttributeInfo AddNewRruAntDev(string strRruNo, string strPort, NPRruToCellInfo lcInfo)
		{
			var strIndex = $".{strRruNo.Trim('.')}.{strPort}";
			var newDev = new DevAttributeInfo(EnumDevType.rru_ant, strIndex);
			if (!SetRruAntSettingTableInfo(newDev.m_mapAttributes, lcInfo))
			{
				return null;
			}
			newDev.m_recordType = RecordDataType.NewAdd;
			return newDev;
		}

		/// <summary>
		/// 根据本地小区ID从天线安装规划表中查询对应的行
		/// </summary>
		/// <param name="nLcId"></param>
		/// <returns></returns>
		public List<DevAttributeInfo> GetRowFromRruAntSetTableByLcId(int nLcId)
		{
			if (0 < nLcId || nLcId > 35)	// TODO 小区的数量先写死为36个
			{
				return null;
			}

			var retList = new List<DevAttributeInfo>();
			var strLcId = nLcId.ToString();
			var devType = EnumDevType.rru_ant;
			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					foreach (var dai in devList)
					{
						var lcId1 = GetEnumStringByMibName(dai.m_mapAttributes, "netSetRRUPortSubtoLocalCellId");
						var lcId2 = GetEnumStringByMibName(dai.m_mapAttributes, "netSetRRUPortSubtoLocalCellId2");
						var lcId3 = GetEnumStringByMibName(dai.m_mapAttributes, "netSetRRUPortSubtoLocalCellId3");
						var lcId4 = GetEnumStringByMibName(dai.m_mapAttributes, "netSetRRUPortSubtoLocalCellId4");
						if (strLcId == lcId4 || strLcId == lcId1 ||
							strLcId == lcId3 || strLcId == lcId2)
						{
							retList.Add(dai);
						}
					}
				}
			}

			return retList;
		}

		/// <summary>
		/// 根据本地小区ID在天线阵安装规划表中找到CellId相同的信息，把CellId对应的属性设置为-1
		/// </summary>
		/// <param name="nLcId"></param>
		/// <returns></returns>
		public bool ResetRelateLcIdInNetRruAntSettingTblByLcId(int nLcId)
		{
			var devType = EnumDevType.rru_ant;
			var strLcId = nLcId.ToString();
			lock (_syncObj)
			{
				if (!m_mapAllMibData.ContainsKey(devType))
				{
					Log.Error($"未找到类型为rru_ant的规划信息");
					return false;
				}

				var devList = m_mapAllMibData[devType];

				// 需要遍历所有的天线阵安装规划表
				Parallel.ForEach(devList, item =>
				{
					var mapAttributes = item.m_mapAttributes;

					for (var j = 1; j <= 4; j++)
					{
						var mibName = "netSetRRUPortSubtoLocalCellId";
						if (j > 1)
						{
							mibName += $"{j}";
						}

						if (!mapAttributes.ContainsKey(mibName))
						{
							Log.Error($"在天线阵安装规划表中没有找到名为{mibName}的节点");
							return;
						}

						var lcValue = GetEnumStringByMibName(mapAttributes, mibName);
						if (lcValue != strLcId) continue;

						mapAttributes[mibName].SetValue("-1");

						if (RecordDataType.NewAdd != item.m_recordType)
						{
							item.m_recordType = RecordDataType.Modified;
						}
					}
				});

				//for (var i = 0; i < devList.Count; i++)
				//{
				//	var dev = devList.ElementAt(i);
				//	var mapAttributes = dev.m_mapAttributes;

				//	for (var j = 1; j <= 4; j++)
				//	{
				//		var mibName = "netSetRRUPortSubtoLocalCellId";
				//		if (j > 1)
				//		{
				//			mibName += $"{j}";
				//		}

				//		if (!mapAttributes.ContainsKey(mibName))
				//		{
				//			Log.Error($"在天线阵安装规划表中没有找到名为{mibName}的节点");
				//			return false;
				//		}

				//		var lcValue = GetEnumStringByMibName(mapAttributes, mibName);
				//		if (lcValue != strLcId) continue;

				//		mapAttributes[mibName].SetValue("-1");

				//		if (RecordDataType.NewAdd != dev.m_recordType)
				//		{
				//			dev.m_recordType = RecordDataType.Modified;
				//		}
				//	}
				//}
			}

			return true;
		}

		#endregion

		#region 私有接口

		private MibInfoMgr()
		{
			m_mapAllMibData = new MAP_DEVTYPE_DEVATTRI();
		}

		/// <summary>
		/// 判断已修改、所有、新添加的设备列表中是否存在与给定的索引相同设备
		/// </summary>
		/// <param name="devType"></param>
		/// <param name="strIndex"></param>
		/// <returns></returns>
		private MAP_DEVTYPE_DEVATTRI HasSameIndexDev(EnumDevType devType, string strIndex)
		{
			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					if (HasSameIndexDev(devList, strIndex))
					{
						return m_mapAllMibData;
					}
				}
			}

			return null;
		}

		// 判断给定的列表中是否存在与strIndex相同索引的设备
		private bool HasSameIndexDev(List<DevAttributeInfo> devList, string strIndex)
		{
			return null != devList && devList.Any(dev => dev.m_strOidIndex == strIndex);
		}

		private DevAttributeInfo GetSameIndexDev(List<DevAttributeInfo> devList, string strIndex)
		{
			return devList.FirstOrDefault(dev => dev.m_strOidIndex == strIndex);
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

		private void DelDevFromMap(MAP_DEVTYPE_DEVATTRI mapData, EnumDevType devType, DevAttributeInfo dai)
		{
			if (null == mapData || null == dai)
			{
				return;
			}

			if (!mapData.ContainsKey(devType))
			{
				return;
			}

			mapData[devType].Remove(dai);
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

		/// <summary>
		/// 根据devIndex在waitDel队列中查找，是否存在相同索引相同类型的设备，如果存在就从waitDel中删除，把新生成的设备加入到modify队列中
		/// </summary>
		/// <param name="type"></param>
		/// <param name="newDev"></param>
		/// <param name="devIndex"></param>
		/// <returns></returns>
		private bool MoveDevFromWaitDelToModifyMap(EnumDevType type, DevAttributeInfo newDev, string devIndex)
		{
			// 新加的设备，要判断索引和待删除列表中的是否一致
			// 目的：防止先删除所有的同类设备，然后重新添加，但是所有的属性设置的又相同的情况
			lock (_syncObj)
			{
				if (!m_mapAllMibData.ContainsKey(type))
				{
					AddDevToMap(m_mapAllMibData, type, newDev);
				}
				else
				{
					var devList = m_mapAllMibData[type];
					foreach (var dev in devList)
					{
						// 如果在待删除的列表中，就直接把新创建的设备放到已修改的列表中
						if (dev.m_strOidIndex != devIndex) continue;

						if (RecordDataType.WaitDel == dev.m_recordType)
						{
							devList.Remove(dev);

							// 需要比对dev和newDev，把dev的值和newDev的信息合并到一起再加入到修改队列
							newDev.AdjustOtherDevOriginValueToMyOrigin(dev);
							AddDevToMap(m_mapAllMibData, type, newDev);
							break;
						}

						return true;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 生成一个指定类型和索引最后一级值的设备
		/// </summary>
		/// <param name="type"></param>
		/// <param name="lastIndexValue"></param>
		/// <returns></returns>
		private DevAttributeInfo GerenalNewDev(EnumDevType type, int lastIndexValue)
		{
			var dev = new DevAttributeInfo(type, lastIndexValue);
			if (dev.m_mapAttributes.Count == 0)
			{
				return null;
			}

			var devIndex = dev.m_strOidIndex;
			if (null != HasSameIndexDev(type, devIndex))
			{
				return null;
			}
			dev.m_recordType = RecordDataType.NewAdd;

			return dev;
		}

		/// <summary>
		/// 根据listColumns中的每个元素，在devInfo中找到对应的值，组装成字典，用于下发
		/// </summary>
		/// <param name="devInfo"></param>
		/// <param name="listColumns"></param>
		/// <param name="strRs">行状态的值：4，6</param>
		/// <returns></returns>
		private Dictionary<string, string> GeneralName2ValueMap(DevAttributeInfo devInfo, List<MibLeaf> listColumns, GetMibValue gmv, string strRs = "4")
		{
			if (null == devInfo || null == listColumns)
			{
				return null;
			}

			var n2v = new Dictionary<string, string>();
			var absMap = devInfo.m_mapAttributes;

			foreach (var leafInfo in listColumns)
			{
				var leafName = leafInfo.childNameMib;

				// 行状态的值特殊处理
				if (leafInfo.ASNType.Equals("RowStatus", StringComparison.OrdinalIgnoreCase))
				{
					n2v.Add(leafName, strRs);
				}
				else
				{
					if (!absMap.ContainsKey(leafName))
					{
						continue;
					}

					var mi = absMap[leafName];
					var value = gmv?.Invoke(mi.m_strOriginValue, mi.m_strLatestValue);
					if (null == value)
					{
						continue;
					}

					// value 有可能是枚举值等描述信息，需要翻转为snmp类型
					var ret = SnmpToDatabase.ConvertStringToMibValue(leafInfo, value);
					n2v.Add(leafName, ret);
				}
			}

			return n2v;
		}

		/// <summary>
		/// 获取最新值
		/// </summary>
		/// <param name="strOriginValue"></param>
		/// <param name="strLatestValue"></param>
		/// <returns></returns>
		private string GetLatestValue(string strOriginValue, string strLatestValue)
		{
			if (string.IsNullOrEmpty(strOriginValue))
			{
				return null;
			}

			var value = strOriginValue;

			if (null != strLatestValue)
			{
				value = strLatestValue;
			}
			return value;
		}

		/// <summary>
		/// 获取需要更新的值
		/// </summary>
		/// <param name="strOriginValue"></param>
		/// <param name="strLatestValue"></param>
		/// <returns></returns>
		private string GetNeedUpdateValue(string strOriginValue, string strLatestValue)
		{
			if (string.IsNullOrEmpty(strOriginValue))
			{
				return null;
			}

			if (null == strLatestValue || strLatestValue == strOriginValue)
			{
				return null;
			}

			return strLatestValue;
		}

		/// <summary>
		/// 布配网规信息
		/// </summary>
		/// <param name="devType"></param>
		/// <param name="cmdType"></param>
		/// <param name="targetIp"></param>
		/// <returns></returns>
		private bool DistributeSnmpData(DevAttributeInfo devAttribute, EnumSnmpCmdType cmdType, string targetIp)
		{
			if (string.IsNullOrEmpty(targetIp))
			{
				throw new CustomException("下发网规信息功能传入目标IP参数错误");
			}

			if (EnumSnmpCmdType.Invalid == cmdType)
			{
				throw new CustomException("下发网规信息功能传入SNMP命令类型错误");
			}

			var enbType = NodeBControl.GetInstance().GetEnbTypeByIp(targetIp);
			var cmdList = NPECmdHelper.GetInstance().GetCmdList(devAttribute.m_enumDevType, cmdType, enbType);
			if (null == cmdList || 0 == cmdList.Count)
			{
				throw new CustomException($"未找到类型为{devAttribute.m_enumDevType.ToString()}的{cmdType.ToString()}相关命令");
			}

			var cmdToMibLeafMap = NPECmdHelper.GetInstance().GetSameTypeCmdMibLeaf(cmdList);
			if (null == cmdToMibLeafMap || 0 == cmdToMibLeafMap.Count)
			{
				throw new CustomException($"未找到类型为{devAttribute.m_enumDevType.ToString()}的{cmdType.ToString()}相关命令详细信息");
			}

			var strRs = "4";
			GetMibValue gmv = GetLatestValue;
			switch (cmdType)
			{
				case EnumSnmpCmdType.Set:
					gmv = GetNeedUpdateValue;
					break;
				case EnumSnmpCmdType.Add:
					break;
				case EnumSnmpCmdType.Del:
					strRs = "6";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(cmdType), cmdType, null);
			}

			foreach (var kv in cmdToMibLeafMap)
			{
				var cmdName = kv.Key;
				var mibLeafList = kv.Value;

				var name2Value = GeneralName2ValueMap(devAttribute, mibLeafList, gmv, strRs);
				var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, devAttribute.m_strOidIndex, targetIp);
				if (0 != ret)
				{
					Log.Error($"下发命令{cmdName}失败");
					return false;       // TODO 一个设备信息下发失败是要结束整个过程吗？
				}
			}

			return true;
		}

		/// <summary>
		/// 从DevAttributeInfo的m_mapAttributes数据结构中取出strMibName对应的值
		/// 如果是枚举值就转换为字符串
		/// </summary>
		/// <param name="mapInfos"></param>
		/// <param name="strMibName"></param>
		/// <returns></returns>
		private string GetEnumStringByMibName(Dictionary<string, MibLeafNodeInfo> mapInfos, string strMibName)
		{
			if (null == mapInfos || string.IsNullOrEmpty(strMibName))
			{
				return null;
			}

			if (!mapInfos.ContainsKey(strMibName))
			{
				return null;
			}

			var attri = mapInfos[strMibName];
			if (null == attri)
			{
				return null;
			}

			var ret = (null == attri.m_strLatestValue)
				? SnmpToDatabase.ConvertValueToString(attri.mibAttri, attri.m_strOriginValue)
				: SnmpToDatabase.ConvertValueToString(attri.mibAttri, attri.m_strLatestValue);

			return ret;
		}


		/// <summary>
		/// 获取RRU通道对应的小区信息
		/// </summary>
		/// <param name="dai"></param>
		/// <param name="nRruNo"></param>
		/// <param name="mapResult"></param>
		private bool GetRruPortToCellInfo(DevAttributeInfo dai, int nRruNo, ref Dictionary<string, NPRruToCellInfo> mapResult, bool bCellFix = false)
		{
			var mapAttri = dai.m_mapAttributes;
			if (!mapAttri.ContainsKey("netSetRRUNo"))
				return false;

			var mibl = mapAttri["netSetRRUNo"];
			var setNo = int.Parse(mibl.m_strLatestValue);   // modify队列中肯定都是修改的
			if (nRruNo != setNo)
				return false;

			var trxStatus = GetEnumStringByMibName(mapAttri, "netSetRRUPortTxRxStatus");

			// 通道频道信息是根据小区的id从netLc表中找到netLcFreqBand字段的值
			// RRU 的ID相同，取出所有通道对应的信息
			var rtc = new NPRruToCellInfo(trStatus: trxStatus);

			var sb = new StringBuilder();
			for (var i = 2; i <= 4; i++)
			{
				var mibName = $"netSetRRUPortSubtoLocalCellId{i}";
				var cellId = GetEnumStringByMibName(mapAttri, mibName);
				if (null != cellId && "-1" != cellId)
				{
					var freqBand = GetValueFromNetLcTableByLcIdAndMibName(int.Parse(cellId), "nrNetLocalCellFreqBand");
					if (!string.IsNullOrEmpty(freqBand))
					{
						sb.AppendFormat(freqBand + "/");
					}

					rtc.CellIdList.Add(new CellAndState {cellId = cellId, bIsFixed = bCellFix});
				}
			}

			var cellId1 = GetEnumStringByMibName(mapAttri, "netSetRRUPortSubtoLocalCellId");
			if (null != cellId1 && -1 != int.Parse(cellId1))
			{
				var freqBand = GetValueFromNetLcTableByLcIdAndMibName(int.Parse(cellId1), "nrNetLocalCellFreqBand");
				if (!string.IsNullOrEmpty(freqBand))
				{
					sb.AppendFormat(freqBand + "/");
				}
				rtc.CellIdList.Add(new CellAndState { cellId = cellId1, bIsFixed = bCellFix });
			}

			var portNo = GetEnumStringByMibName(mapAttri, "netSetRRUPortNo");
			if (null == portNo)
			{
				return false;
			}
			rtc.FreqBand = sb.ToString();
			mapResult.Add(portNo, rtc);

			return true;
		}

		/// <summary>
		/// 根据本地小区ID和MIB节点名从netLocalCellTable表中查找对应的值
		/// </summary>
		/// <param name="strLcId">本地小区ID</param>
		/// <param name="strMibName">mib名称</param>
		/// <returns></returns>
		private string GetValueFromNetLcTableByLcIdAndMibName(int nLcId, string strMibName)
		{
			if (0 < nLcId || nLcId > 35)
			{
				Log.Error($"传入的本地小区ID超出[0,35]范围");
				return null;
			}

			var devType = EnumDevType.nrNetLc;

			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					var dev = GetSameIndexDev(devList, $".{nLcId}");
					if (null != dev)
					{
						return GetEnumStringByMibName(dev.m_mapAttributes, strMibName);
					}
				}
			}

			return null;
		}

		/// <summary>
		/// 设置天线阵安装规划表的信息。修改时使用
		/// </summary>
		/// <param name="mapAttributes"></param>
		/// <param name="lcInfo"></param>
		/// <returns></returns>
		private bool SetRruAntSettingTableInfo(Dictionary<string, MibLeafNodeInfo> mapAttributes, NPRruToCellInfo lcInfo)
		{
			if (mapAttributes.ContainsKey("netSetRRUPortTxRxStatus"))
			{
				var tempAtrri = mapAttributes["netSetRRUPortTxRxStatus"];
				tempAtrri.SetValue(lcInfo.TxRxStatus);
			}

			var lcIdList = lcInfo.CellIdList;
			var lcAttr1 = mapAttributes["netSetRRUPortSubtoLocalCellId"];
			var lcAttr2 = mapAttributes["netSetRRUPortSubtoLocalCellId2"];
			var lcAttr3 = mapAttributes["netSetRRUPortSubtoLocalCellId3"];
			var lcAttr4 = mapAttributes["netSetRRUPortSubtoLocalCellId4"];

			// 先设置为-1。可能会删除通道关联的小区
			lcAttr1.SetValue("-1");
			lcAttr2.SetValue("-1");
			lcAttr3.SetValue("-1");
			lcAttr4.SetValue("-1");
			for (int i = 1; i <= lcIdList.Count; i++)
			{
				if (1 == i)
				{
					lcAttr1.SetValue(lcIdList[i - 1].ToString());
				}
				if (2 == i)
				{
					lcAttr2.SetValue(lcIdList[i - 1].ToString());
				}
				if (3 == i)
				{
					lcAttr3.SetValue(lcIdList[i - 1].ToString());
				}
				if (4 == i)
				{
					lcAttr4.SetValue(lcIdList[i - 1].ToString());
					break;
				}
			}

			return true;
		}

		#endregion


		#region 私有成员、数据

		// 保存所有的网规MIB数据。开始保存的是从设备中查询回来的信息，如果对这些信息进行了修改、删除，就从这个数据结构中移动到对应的结构中
		// 最后下发网规信息时，就不再下发这个数据结构中的信息
		private MAP_DEVTYPE_DEVATTRI m_mapAllMibData;

		private readonly object _syncObj = new object();

		#endregion
	}
}
