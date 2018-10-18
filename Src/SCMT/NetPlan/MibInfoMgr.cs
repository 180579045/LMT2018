using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
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
				if (m_mapModifyDev.ContainsKey(type))
				{
					var modDevList = m_mapModifyDev[type];
					var dev = GetSameIndexDev(modDevList, strIndex);
					if (null != dev)
					{
						return dev;
					}
				}

				if (m_mapAllMibData.ContainsKey(type))
				{
					var devList = m_mapAllMibData[type];
					var dev = GetSameIndexDev(devList, strIndex);
					if (null != dev)
					{
						return dev;
					}
				}

				if (m_mapNewAddDev.ContainsKey(type))
				{
					var devList = m_mapNewAddDev[type];
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
		/// 删除类型为devType，索引为strIndex的设备
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="devType"></param>
		public bool DelDev(string strIndex, EnumDevType devType)
		{
			if (string.IsNullOrEmpty(strIndex) || EnumDevType.unknown == devType)
			{
				return false;
			}

			lock (_syncObj)
			{
				if (m_mapModifyDev.ContainsKey(devType))
				{
					var modDevList = m_mapModifyDev[devType];
					var dev = GetSameIndexDev(modDevList, strIndex);
					if (null != dev)
					{
						MoveDevFromModifyToWaitDelMap(devType, dev);
						return true;
					}
				}

				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					var dev = GetSameIndexDev(devList, strIndex);
					if (null != dev)
					{
						MoveDevFromAllToWaitDelMap(devType, dev);
						return true;
					}
				}

				if (m_mapNewAddDev.ContainsKey(devType))
				{
					var devList = m_mapNewAddDev[devType];
					var dev = GetSameIndexDev(devList, strIndex);
					if (null != dev)
					{
						m_mapNewAddDev[devType].Remove(dev);
						return true;
					}
				}
			}

			return false;
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
				// 已经修改过属性的设备还没有下发到基站，所以修改设备属性后仍然存在这个数据结构中
				if (m_mapModifyDev.ContainsKey(devType))
				{
					var modDevList = m_mapModifyDev[devType];
					var dev = GetSameIndexDev(modDevList, strIndex);
					if (null != dev)
					{
						return dev.SetFieldValue(strFieldName, strValue);
					}
				}

				// 从基站中查询到设备修改属性，就需要移动到modify队列，等待参数下发
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					var dev = GetSameIndexDev(devList, strIndex);
					if (null != dev)
					{
						dev.SetFieldValue(strFieldName, strValue);
						MoveDevFromAllToModifyMap(devType, dev);
						return true;
					}
				}

				// 新增的设备还没有下发到基站，所以修改设备属性后仍然存在这个数据结构中
				if (m_mapNewAddDev.ContainsKey(devType))
				{
					var devList = m_mapNewAddDev[devType];
					var dev = GetSameIndexDev(devList, strIndex);
					if (null != dev)
					{
						return dev.SetFieldValue(strFieldName, strValue);
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
				// 有新增的板卡才下发Add命令
				if (m_mapNewAddDev.ContainsKey(devType) && m_mapNewAddDev[devType].Count > 0)
				{
					return DistributeSnmpData(devType, EnumSnmpCmdType.Add, targetIp);
					var cmdList = NPECmdHelper.GetInstance().GetCmdList(devType, EnumSnmpCmdType.Add);
					if (null == cmdList || 0 == cmdList.Count)
					{
						throw new CustomException($"未找到类型为{devType.ToString()}的Add相关命令");
					}

					var cmdToMibLeafMap = NPECmdHelper.GetInstance().GetSameTypeCmdMibLeaf(cmdList);
					if (null == cmdToMibLeafMap || 0 == cmdToMibLeafMap.Count)
					{
						throw new CustomException($"未找到类型为{devType.ToString()}的Add相关命令详细信息");
					}

					// 板卡只有一个Add命令：AddNetBoard，但是RRU有3个Add命令，每个命令下发一部分数据。
					var daiList = m_mapNewAddDev[devType];

					foreach (var kv in cmdToMibLeafMap)
					{
						var cmdName = kv.Key;
						var mibLeafList = kv.Value;
						foreach (var dai in daiList)
						{
							var name2Value = GeneralName2ValueMap(dai, mibLeafList, GetLatestValue);
							var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, dai.m_strOidIndex, targetIp);
							if (0 != ret)
							{
								Log.Error($"下发命令{cmdName}失败");
								return false;
							}
						}
					}
				}

				// 下发待删除的板卡信息
				if (m_mapWaitDelDev.ContainsKey(devType) && m_mapWaitDelDev[devType].Count > 0)
				{
					return DistributeSnmpData(devType, EnumSnmpCmdType.Del, targetIp);
					var cmdList = NPECmdHelper.GetInstance().GetCmdList(devType, EnumSnmpCmdType.Del);
					if (null == cmdList || 0 == cmdList.Count)
					{
						throw new CustomException($"未找到类型为{devType.ToString()}的Del相关命令");
					}

					var cmdToMibLeafMap = NPECmdHelper.GetInstance().GetSameTypeCmdMibLeaf(cmdList);
					if (null == cmdToMibLeafMap || 0 == cmdToMibLeafMap.Count)
					{
						throw new CustomException($"未找到类型为{devType.ToString()}的Del相关命令详细信息");
					}

					// 板卡只有一个Add命令：AddNetBoard，但是RRU有3个Add命令，每个命令下发一部分数据。
					foreach (var kv in cmdToMibLeafMap)
					{
						var cmdName = kv.Key;
						var mibLeafList = kv.Value;
						var daiList = m_mapWaitDelDev[devType];
						foreach (var dai in daiList)
						{
							var name2Value = GeneralName2ValueMap(dai, mibLeafList, GetLatestValue, "6");
							var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, dai.m_strOidIndex, targetIp);
							if (0 != ret)
							{
								Log.Error($"下发命令{cmdName}失败");
								return false;
							}
						}
					}
				}

				// 下发修改参数的板卡信息
				if (m_mapModifyDev.ContainsKey(devType) && m_mapModifyDev[devType].Count > 0)
				{
					return DistributeSnmpData(devType, EnumSnmpCmdType.Set, targetIp);
					var cmdList = NPECmdHelper.GetInstance().GetCmdList(devType, EnumSnmpCmdType.Set);
					if (null == cmdList || 0 == cmdList.Count)
					{
						throw new CustomException($"未找到类型为{devType.ToString()}的Set相关命令");
					}

					var cmdToMibLeafMap = NPECmdHelper.GetInstance().GetSameTypeCmdMibLeaf(cmdList);
					if (null == cmdToMibLeafMap || 0 == cmdToMibLeafMap.Count)
					{
						throw new CustomException($"未找到类型为{devType.ToString()}的Set相关命令详细信息");
					}

					foreach (var kv in cmdToMibLeafMap)
					{
						var cmdName = kv.Key;
						var mibLeafList = kv.Value;
						var daiList = m_mapWaitDelDev[devType];
						foreach (var dai in daiList)
						{
							var name2Value = GeneralName2ValueMap(dai, mibLeafList, GetNeedUpdateValue);
							var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, dai.m_strOidIndex, targetIp);
							if (0 != ret)
							{
								Log.Error($"下发命令{cmdName}失败");
								return false;
							}
						}
					}
				}
			}

			return true;
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
		private MAP_DEVTYPE_DEVATTRI HasSameIndexDev(EnumDevType devType, string strIndex)
		{
			lock (_syncObj)
			{
				if (m_mapModifyDev.ContainsKey(devType))
				{
					var modDevList = m_mapModifyDev[devType];
					if (HasSameIndexDev(modDevList, strIndex))
					{
						return m_mapModifyDev;
					}
				}

				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					if (HasSameIndexDev(devList, strIndex))
					{
						return m_mapAllMibData;
					}
				}

				if (m_mapNewAddDev.ContainsKey(devType))
				{
					var devList = m_mapNewAddDev[devType];
					if (HasSameIndexDev(devList, strIndex))
					{
						return m_mapNewAddDev;
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
				if (!m_mapWaitDelDev.ContainsKey(type))
				{
					AddDevToMap(m_mapNewAddDev, type, newDev);
					return true;
				}

				var devList = m_mapWaitDelDev[type];
				foreach (var dev in devList)
				{
					// 如果在待删除的列表中，就直接把新创建的设备放到已修改的列表中
					if (dev.m_strOidIndex != devIndex) continue;

					devList.Remove(dev);

					// 需要比对dev和newDev，把dev的值和newDev的信息合并到一起再加入到修改队列
					newDev.AdjustOtherDevOriginValueToMyOrigin(dev);
					AddDevToMap(m_mapModifyDev, type, newDev);
					return true;
				}

				// 如果在待删除列表中没有找到索引相同的设备，就把newDev加入到新添加设备列表
				AddDevToMap(m_mapNewAddDev, type, newDev);
			}

			return true;
		}

		/// <summary>
		/// 执行Del操作时，把oldDev从modify队列移动到waitDel队列
		/// </summary>
		/// <param name="type"></param>
		/// <param name="oldDev"></param>
		/// <returns></returns>
		private bool MoveDevFromModifyToWaitDelMap(EnumDevType type, DevAttributeInfo oldDev)
		{
			//lock (_syncObj)
			//{
				m_mapModifyDev[type].Remove(oldDev);
				AddDevToMap(m_mapWaitDelDev, type, oldDev);
			//}
			return true;
		}

		/// <summary>
		/// 执行Del操作时，把oldDev从all队列移动到waitDel队列
		/// </summary>
		/// <param name="type"></param>
		/// <param name="oldDev"></param>
		/// <returns></returns>
		private bool MoveDevFromAllToWaitDelMap(EnumDevType type, DevAttributeInfo oldDev)
		{
			m_mapAllMibData[type].Remove(oldDev);
			AddDevToMap(m_mapWaitDelDev, type, oldDev);

			return true;
		}

		// 移动指定的设备到修改队列
		private bool MoveDevFromAllToModifyMap(EnumDevType type, DevAttributeInfo oldDev)
		{
			m_mapAllMibData[type].Remove(oldDev);
			AddDevToMap(m_mapModifyDev, type, oldDev);
			return true;
		}

		private void AddDevToWaitDelMap(EnumDevType type, DevAttributeInfo dev)
		{
			if (m_mapWaitDelDev.ContainsKey(type))
			{
				m_mapWaitDelDev[type].Add(dev);
			}
			else
			{
				var devList = new List<DevAttributeInfo> { dev };
				m_mapWaitDelDev[type] = devList;
			}
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

		private bool DistributeSnmpData(EnumDevType devType, EnumSnmpCmdType cmdType, string targetIp)
		{
			var cmdList = NPECmdHelper.GetInstance().GetCmdList(devType, EnumSnmpCmdType.Add);
			if (null == cmdList || 0 == cmdList.Count)
			{
				throw new CustomException($"未找到类型为{devType.ToString()}的Add相关命令");
			}

			var cmdToMibLeafMap = NPECmdHelper.GetInstance().GetSameTypeCmdMibLeaf(cmdList);
			if (null == cmdToMibLeafMap || 0 == cmdToMibLeafMap.Count)
			{
				throw new CustomException($"未找到类型为{devType.ToString()}的Add相关命令详细信息");
			}

			var strRs = "4";
			MAP_DEVTYPE_DEVATTRI mapSource = null;
			GetMibValue gmv = GetLatestValue;
			switch (cmdType)
			{
				case EnumSnmpCmdType.Set:
					mapSource = m_mapModifyDev;
					gmv = GetNeedUpdateValue;
					break;
				case EnumSnmpCmdType.Add:
					mapSource = m_mapNewAddDev;
					break;
				case EnumSnmpCmdType.Del:
					strRs = "6";
					mapSource = m_mapWaitDelDev;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(cmdType), cmdType, null);
			}

			// 板卡只有一个Add命令：AddNetBoard，但是RRU有3个Add命令，每个命令下发一部分数据。
			var daiList = mapSource[devType];

			foreach (var kv in cmdToMibLeafMap)
			{
				var cmdName = kv.Key;
				var mibLeafList = kv.Value;
				foreach (var dai in daiList)
				{
					var name2Value = GeneralName2ValueMap(dai, mibLeafList, gmv, strRs);
					var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, dai.m_strOidIndex, targetIp);
					if (0 != ret)
					{
						Log.Error($"下发命令{cmdName}失败");
						return false;
					}
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

		// 下发网规信息时，这个数据结构中的设备只下发 RowStatus = 6 这一行数据
		// 这个字典的数据来源：从enb查回的数据删除掉
		private MAP_DEVTYPE_DEVATTRI m_mapWaitDelDev;		// 待删除的设备列表

		// 下发网规信息时，这个数据结构中的设备 RowStatus = 4,然后调用 Set 命令进行下发
		// 数据来源：新添加的设备（索引区别与已从enb查询到的数据删除元素，也就是enb-->del-->add(索引相同)-->最后在modify字典中，不在这个字典）
		private MAP_DEVTYPE_DEVATTRI m_mapNewAddDev;		// 新增的设备列表

		// 下发网规信息时，这个数据结构中的信息只下发 DevAttributeInfo 结构中的 m_listModifyField 列表中的字段
		// 数据来源：从enb中查询到的数据进行修改
		private MAP_DEVTYPE_DEVATTRI m_mapModifyDev;		// 已修改属性的设备列表

		#endregion
	}
}
