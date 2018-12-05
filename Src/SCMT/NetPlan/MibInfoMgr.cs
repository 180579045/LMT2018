using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUtility;
using DataBaseUtil;
using LinkPath;
using LogManager;
using NetPlan.DevLink;
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
			lock (_syncObj)
			{
				return m_mapAllMibData;
			}
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

			NPLastErrorHelper.SetLastError($"未找到索引为{strIndex}的{type.ToString()}设备");
			return null;
		}

		public DevAttributeInfo GetLinkAttri(LinkEndpoint srcEndpoint, LinkEndpoint dstEndpoint)
		{
			var lt = EnumDevType.unknown;
			if (!GetLinkTypeBySrcDstEnd(srcEndpoint, dstEndpoint, ref lt))
			{
				Log.Error("获取连接类型失败");
				return null;
			}

			var wlink = new WholeLink(srcEndpoint, dstEndpoint);
			var handler = LinkFactory.CreateLinkHandler(lt);
			if (null == handler)
			{
				Log.Error($"连接类型{lt.ToString()}尚未提供支持");
				return null;
			}

			lock (_syncObj)
			{
				return handler.GetRecord(wlink, m_mapAllMibData);
			}
		}

		/// <summary>
		/// 解析连接
		/// </summary>
		/// <returns></returns>
		public bool ParseLinks()
		{
			lock (_syncObj)
			{
				return m_linkMgr.ParseLinksFromMibInfo(m_mapAllMibData);
			}
		}

		/// 获取所有的连接信息
		public Dictionary<EnumDevType, List<WholeLink>> GetLinks()
		{
			return m_linkMgr.GetAllLink();
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
					m_mapAllMibData[type].AddRange(listDevInfo);    // 已经存在同类的设备信息，直接添加
				}
				else
				{
					m_mapAllMibData[type] = listDevInfo;            // 还不存在同类的设备信息，直接保存
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
					var listDev = new List<DevAttributeInfo> { devInfo };
					m_mapAllMibData[type] = listDev;
				}
			}
		}

		/// <summary>
		/// 增加一个新的天线设备
		/// </summary>
		/// <param name="nIndex">设备序号</param>
		/// <param name="strVerdorName">厂家名称</param>
		/// <param name="strAntTypeName">天线阵类型名</param>
		/// <returns>null:添加失败</returns>
		public DevAttributeInfo AddNewAnt(int nIndex, string strVerdorName, string strAntTypeName)
		{
			const EnumDevType type = EnumDevType.ant;
			var ant = GenerateNewDev(type, nIndex);
			if (null == ant)
			{
				return null;
			}

			var strVendorNo = NPEAntHelper.GetInstance().GetVendorIndexByName(strVerdorName);
			if (null == strVendorNo)
			{
				Log.Error($"根据厂家名{strVerdorName}获取厂家索引失败");
				return null;
			}

			var strAntTypeNo = NPEAntHelper.GetInstance().GetTypeIndexByModelName(strAntTypeName);
			if (null == strAntTypeNo)
			{
				Log.Error($"根据类型名{strAntTypeName}获取类型编号失败");
				return null;
			}

			if (!ant.SetFieldOriginValue("netAntArrayVendorIndex", strVendorNo))
			{
				Log.Error($"设置字段netAntArrayVendorIndex的值{strVendorNo}失败");
				return null;
			}

			if (!ant.SetFieldOriginValue("netAntArrayTypeIndex", strAntTypeNo))
			{
				Log.Error($"设置字段netAntArrayTypeIndex的值{strAntTypeNo}失败");
				return null;
			}

			//var antType = NPEAntHelper.GetInstance().GetAntTypeByVendorAndTypeIdx(strVendorNo, strAntTypeNo);
			//if (null == antType)
			//{
			//	Log.Error($"根据天线阵的厂家索引{strVendorNo}和类型索引{strAntTypeNo}查询天线阵器件信息失败");
			//	return null;
			//}

			//NetDevAnt.SetAntTypeInfo(ant, antType);

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
			if (string.IsNullOrEmpty(strBoardType) || string.IsNullOrEmpty(strWorkMode) ||
				string.IsNullOrEmpty(strIrFrameType))
			{
				throw new ArgumentNullException();
			}

			const EnumDevType type = EnumDevType.board;
			var dev = GenerateNewDev(type, slot);
			if (null == dev)
			{
				Log.Error("生成新设备属性失败，可能已经存在相同索引相同类型的设备");
				return null;
			}

			if (!dev.SetFieldOriginValue("netBoardType", strBoardType) ||
				!dev.SetFieldOriginValue("netBoardWorkMode", strWorkMode) ||
				!dev.SetFieldOriginValue("netBoardIrFrameType", strIrFrameType))
			{
				Log.Error("设置新板卡属性失败");
				NPLastErrorHelper.SetLastError("设备新板卡属性失败");
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
				Log.Error("传入RRU索引列表为null；工作模式为null或空");
				NPLastErrorHelper.SetLastError("新增RRU失败，传入RRU索引列表或工作模式为空");
				return null;
			}

			const EnumDevType type = EnumDevType.rru;
			var rruList = new List<DevAttributeInfo>();
			foreach (var seqIndex in seqIndexList)
			{
				var newRru = GenerateNewDev(type, seqIndex);
				if (null == newRru)
				{
					Log.Error($"根据RRU编号{seqIndex}生成新设备失败");
					return null;
				}

				if (!newRru.SetFieldOriginValue("netRRUTypeIndex", nRruType.ToString()) ||
					!newRru.SetFieldOriginValue("netRRUOfpWorkMode", strWorkMode))
				{
					Log.Error("设置RRU参数netRRUTypeIndex、netRRUOfpWorkMode失败");
					NPLastErrorHelper.SetLastError("设置RRU工作模式失败");
					return null;
				}

				if (!MoveDevFromWaitDelToModifyMap(type, newRru, newRru.m_strOidIndex))
				{
					return null;
				}

				Log.Debug($"编号为{seqIndex}的RRU设备添加成功");
				rruList.Add(newRru);
			}

			return rruList;
		}

		/// <summary>
		/// 增加新的RHUB设备
		/// 1.5G当前版本rhub的4个光口要全部连接到基带板，否则小区无法建立
		/// 2.rhub光口编号1~4,eth口编号1~8
		/// 3.前期5G不支持rhub级联
		/// 4.rhub的4个光口对应hbpod板卡的2、3、4、5光口，顺序无要求，底层写死
		/// 5.只支持正常模式
		/// 6.上联口编号1，2，连接接入板；下联口编号3，4，级联使用
		/// </summary>
		/// <param name="nRhubNo">要添加设备的编号</param>
		/// <param name="strDevVer">设备版本。rhub分1.0和2.0两个版本，用于UI绘图</param>
		/// <param name="strWorkMode"></param>
		/// <returns>null:添加rhub设备失败；</returns>
		public RHubDevAttri AddNewRhub(int nRhubNo, string strDevVer, string strWorkMode)
		{
			if (string.IsNullOrEmpty(strWorkMode) || string.IsNullOrEmpty(strDevVer))
			{
				Log.Error("传入rhub工作模式为null或空");
				return null;
			}

			const EnumDevType type = EnumDevType.rhub;

			var dev = new RHubDevAttri(nRhubNo, strDevVer);
			if (dev.m_mapAttributes.Count == 0)
			{
				Log.Error($"编号为{nRhubNo}的rhub设备属性数量为0");
				return null;
			}

			var devIndex = dev.m_strOidIndex;
			if (HasSameIndexDev(m_mapAllMibData, type, devIndex))
			{
				Log.Error($"已经存在编号为{nRhubNo}的rhub设备，添加失败");
				return null;
			}

			dev.SetDevRecordType(RecordDataType.NewAdd);

			if (!dev.SetFieldOriginValue("netRHUBOfpWorkMode", strWorkMode))
			{
				Log.Error("设置rhub参数netRHUBOfpWorkMode失败");
				return null;
			}

			if (!dev.SetFieldOriginValue("netRHUBType", strDevVer))
			{
				Log.Error("设置rhub参数netRHUBType失败");
				return null;
			}

			if (!MoveDevFromWaitDelToModifyMap(type, dev, devIndex))
			{
				return null;
			}

			Log.Debug($"编号为{nRhubNo}的rhub设备添加成功");

			return dev;
		}

		/// <summary>
		/// 新增本地小区
		/// </summary>
		/// <param name="nLocalCellId"></param>
		/// <returns></returns>
		public DevAttributeInfo AddNewLocalCell(int nLocalCellId)
		{
			const EnumDevType type = EnumDevType.nrNetLc;

			var dev = GetDevAttributeInfo($".{nLocalCellId}", type);
			if (null == dev)
			{
				dev = GenerateNewDev(type, nLocalCellId);
				if (null == dev)
				{
					Log.Error($"生成本地小区{nLocalCellId}的属性失败");
					return null;
				}
				Log.Debug($"生成本地小区{nLocalCellId}属性信息成功");

				AddDevToMap(m_mapAllMibData, type, dev);
			}
			else
			{
				Log.Debug($"本地小区{nLocalCellId}已经存在不需要重新生成");
				dev.SetDevRecordType(RecordDataType.Modified);
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

			if (!dev.SetFieldOriginValue("nrNetLocalCellCtrlConfigSwitch", strSwitchValue, true))
			{
				Log.Error($"设置字段 nrNetLocalCellCtrlConfigSwitch 的 OriginValue 为{strSwitchValue}失败");
				return null;
			}

			dev.SetDevRecordType(RecordDataType.Original);
			lock (_syncObj)
			{
				AddDevToMap(m_mapAllMibData, type, dev);
			}
			return dev;
		}

		/// <summary>
		/// 增加连接
		/// </summary>
		/// <param name="srcEndpoint"></param>
		/// <param name="dstEndpoint"></param>
		/// <returns></returns>
		public bool AddLink(LinkEndpoint srcEndpoint, LinkEndpoint dstEndpoint)
		{
			var linkType = EnumDevType.unknown;
			if (!GetLinkTypeBySrcDstEnd(srcEndpoint, dstEndpoint, ref linkType))
			{
				Log.Error("获取连接类型失败");
				return false;
			}

			var wlink = new WholeLink(srcEndpoint, dstEndpoint);

			var handler = LinkFactory.CreateLinkHandler(linkType);
			if (null == handler)
			{
				Log.Error($"连接类型{linkType.ToString()}尚未提供支持");
				return false;
			}

			lock (_syncObj)
			{
				return handler.AddLink(wlink, ref m_mapAllMibData);
			}
		}

		/// <summary>
		/// 把类型为devType的Dev添加到给定的map中
		/// </summary>
		/// <param name="mapData"></param>
		/// <param name="devType"></param>
		/// <param name="dev"></param>
		public static bool AddDevToMap(MAP_DEVTYPE_DEVATTRI mapData, EnumDevType devType, DevAttributeInfo dev)
		{
			if (null == mapData || devType == EnumDevType.unknown || null == dev)
			{
				return false;
			}

			if (mapData.ContainsKey(devType))
			{
				mapData[devType].Add(dev);
			}
			else
			{
				var devList = new List<DevAttributeInfo> { dev };
				mapData[devType] = devList;
			}

			return true;
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

			lock (_syncObj)
			{
				// 由于rru设备的特殊性，同时关系到本地小区映射关系和天线阵的连接，删除RRU设备时，需要删除天线阵安装规划表中的所有相关联的数据
				if (EnumDevType.rru == devType)
				{
					if (!DelRecordFromRruAntSettingTblByRruIndex(strIndex))
					{
						return false;
					}
				}

				DelDevFromMap(m_mapAllMibData, devType, dev);
			}
			return true;
		}

		/// <summary>
		/// 根据rru索引删除天线安装规划表中的所有记录
		/// </summary>
		/// <param name="strRruIndex"></param>
		private bool DelRecordFromRruAntSettingTblByRruIndex(string strRruIndex)
		{
			var rruPathInfoList = GetRruPortInfoByIndex(strRruIndex);
			if (null == rruPathInfoList)
			{
				return false;
			}

			foreach (var pathInfo in rruPathInfoList)
			{
				var portNo = pathInfo.rruTypePortNo;
				var ridx = $"{strRruIndex}.{portNo}";
				var record = GetDevAttributeInfo(ridx, EnumDevType.rru_ant);
				DelDevFromMap(m_mapAllMibData, EnumDevType.rru_ant, record);
			}

			return true;
		}

		/// <summary>
		/// 删除连接
		/// </summary>
		/// <param name="srcEndpoint"></param>
		/// <param name="dstEndpoint"></param>
		/// <returns></returns>
		public bool DelLink(LinkEndpoint srcEndpoint, LinkEndpoint dstEndpoint)
		{
			var linkType = EnumDevType.unknown;
			if (!GetLinkTypeBySrcDstEnd(srcEndpoint, dstEndpoint, ref linkType))
			{
				Log.Error("获取连接类型失败，删除连接失败");
				return false;
			}

			var wlink = new WholeLink(srcEndpoint, dstEndpoint);
			var handler = LinkFactory.CreateLinkHandler(linkType);
			if (null == handler)
			{
				Log.Error($"尚未支持类型为{linkType.ToString()}的连接处理");
				return false;
			}

			lock (_syncObj)
			{
				return handler.DelLink(wlink, ref m_mapAllMibData);
			}
		}

		/// <summary>
		/// 直接从内存中删除符合条件的
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="devType"></param>
		/// <returns></returns>
		public bool DelDevFromMemory(string strIndex, EnumDevType devType)
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
				DelDevFromMap(m_mapAllMibData, devType, dev, true);
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
				if (!m_mapAllMibData.ContainsKey(devType)) return false;

				var devList = m_mapAllMibData[devType];
				var dev = GetSameIndexDev(devList, strIndex);
				if (null == dev) return false;

				dev.SetFieldLatestValue(strFieldName, strValue);
				if (RecordDataType.NewAdd != dev.m_recordType)
				{
					dev.SetDevRecordType(RecordDataType.Modified);
				}
				return true;
			}
		}

		/// <summary>
		/// 下发网规信息
		/// </summary>
		/// <returns></returns>
		private bool DistributeData(EnumDevType devType, MAP_DEVTYPE_DEVATTRI mapDataSource, bool bDlAntWcb = false)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("下发网规参数失败，尚未选中基站");
			}

			if (!mapDataSource.ContainsKey(devType) || mapDataSource[devType].Count <= 0) return true;

			var devList = mapDataSource[devType];
			var waitRmList = new List<DevAttributeInfo>();

			var bSucceed = true;
			NetDevBase handler = null;
			foreach (var item in devList)
			{
				if (RecordDataType.Original == item.m_recordType)
				{
					continue;
				}

				// 天线安装规划表的特殊处理
				if (devType == EnumDevType.rru_ant)
				{
					if (!LinkRruAnt.RruHasConnectToAnt(item))
					{
						Log.Error($"索引为{item.m_strOidIndex}的天线安装规划记录没有配置天线阵的信息，忽略不再下发");
						continue;
					}
				}

				if (null == handler)
				{
					handler = NetDevFactory.GetDevHandler(targetIp, devType, mapDataSource);
					if (null == handler)
					{
						Log.Error($"获取类型{devType}处理对象失败");
						return false;
					}
				}

				// 如果下发失败，就break，后面的流程还要执行，清理掉已经下发成功的待删除的设备
				if (!(bSucceed = handler.DistributeToEnb(item, bDlAntWcb)))
				{
					break;
				}

				if (item.m_recordType == RecordDataType.WaitDel)
				{
					waitRmList.Add(item);       // 如果是要删除的设备，参数下发后，直接删除内存中的数据
				}
				else
				{
					item.SetDevRecordType(RecordDataType.Original);		// 下发成功的都设置为原始数据
				}

				Log.Debug($"类型为{devType.ToString()}，索引为{item.m_strOidIndex}的网规信息下发成功");
			}

			foreach (var wrmDev in waitRmList)
			{
				devList.Remove(wrmDev);
			}

			return bSucceed;
		}

		/// <summary>
		/// 下发网规的信息
		/// </summary>
		/// <param name="devType"></param>
		/// <returns></returns>
		public bool DistributeNetPlanInfoToEnb(EnumDevType devType, bool bDlAntWcb = false)
		{
			Log.Debug($"开始下发类型 {devType.ToString()} 的规划信息");

			var result = DistributeData(devType, m_mapAllMibData, bDlAntWcb);

			Log.Debug($"类型 {devType.ToString()} 的规划信息下发完成，结果为：{result}");
			return result;
		}

		/// <summary>
		/// 根据rru设备的索引获取该RRU每个通道上配置的本地小区配置信息
		/// </summary>
		/// <param name="strIndex"></param>
		/// <returns>字典，key:通道号，value:该通道上本地小区的信息</returns>
		public Dictionary<string, NPRruToCellInfo> GetNetLcInfoByRruIndex(string strIndex)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp || string.IsNullOrEmpty(strIndex))
			{
				throw new ArgumentNullException();
			}

			var retMap = new Dictionary<string, NPRruToCellInfo>();
			var rruPathInfoList = GetRruPortInfoByIndex(strIndex);
			if (null == rruPathInfoList)
			{
				return retMap;
			}

			// 保存此次遍历中小区的状态，防止多次重复查找
			var mapLcStatus = new Dictionary<string, bool>();

			// 遍历rru通道信息
			foreach (var pathInfo in rruPathInfoList)
			{
				var pathNo = pathInfo.rruTypePortNo;        // rru通道编号
				var tmpIdx = $"{strIndex}.{pathNo}";

				// 查找是否存在天线阵安装规划表信息
				var rai = GetDevAttributeInfo(tmpIdx, EnumDevType.rru_ant);
				if (null == rai)
				{
					rai = new DevAttributeInfo(EnumDevType.rru_ant, tmpIdx);    // 删除天线阵时，才删除所有的天线阵安装规划表信息
					AddDevToMap(m_mapAllMibData, EnumDevType.rru_ant, rai);
				}

				GetRruPortToCellInfo(rai, pathInfo, ref retMap, ref mapLcStatus);
			}

			return retMap;
		}

		/// <summary>
		/// 配置RRU通道与本地小区的关联关系
		/// </summary>
		/// <param name="strRruIndex">rru索引</param>
		/// <param name="portToCellInfoList">端口对应的本地小区信息。key:rru通道号，value:本地小区信息</param>
		/// <returns></returns>
		public bool SetNetLcInfo(string strRruIndex, Dictionary<string, NPRruToCellInfo> portToCellInfoList)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("尚未选中基站");
			}

			const EnumDevType devType = EnumDevType.rru_ant;
			var rruNo = strRruIndex.Trim('.');

			Func<KeyValuePair<string, NPRruToCellInfo>, bool> la = (kv) =>
			{
				var newDev = AddNewRruAntDev(rruNo, kv.Key, kv.Value);
				if (null == newDev)
				{
					Log.Error("新加天线阵安装规划表实例失败");
					return false;
				}
				AddDevToMap(m_mapAllMibData, EnumDevType.rru_ant, newDev);
				return true;
			};

			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(devType))
				{
					var devList = m_mapAllMibData[devType];
					foreach (var kv in portToCellInfoList) // 遍历传入的所有值
					{
						var strIndex = $".{rruNo}.{kv.Key}"; // 用rru编号和port编号组成索引
						var dev = GetSameIndexDev(devList, strIndex);

						if (null == dev)
						{
							if (!la(kv))
							{
								return false;
							}
						}
						else
						{
							SetRruAntSettingTableInfo(dev, kv.Value);

							if (dev.m_recordType != RecordDataType.NewAdd)
							{
								dev.SetDevRecordType(RecordDataType.Modified);
							}
						}
					}
				}
				else
				{
					if (portToCellInfoList.Any(kv => !la(kv)))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 增加一行天线阵安装规划表记录
		/// </summary>
		/// <param name="strRruNo"></param>
		/// <param name="strPort"></param>
		/// <param name="lcInfo"></param>
		/// <returns></returns>
		private static DevAttributeInfo AddNewRruAntDev(string strRruNo, string strPort, NPRruToCellInfo lcInfo)
		{
			var strIndex = $".{strRruNo.Trim('.')}.{strPort}";
			var newDev = new DevAttributeInfo(EnumDevType.rru_ant, strIndex);
			return !SetRruAntSettingTableInfo(newDev, lcInfo) ? null : newDev;
		}

		/// <summary>
		/// 根据RRU设备索引获取所有通道信息
		/// </summary>
		/// <param name="strIndex"></param>
		/// <returns></returns>
		private IEnumerable<RruPortInfo> GetRruPortInfoByIndex(string strIndex)
		{
			int nIndex;
			if (!int.TryParse(strIndex.Trim('.'), out nIndex))
			{
				Log.Error($"传入的{strIndex}存在非法字符");
				return null;
			}

			var rru = GetDevAttributeInfo(strIndex, EnumDevType.rru);
			if (null == rru)
			{
				Log.Error($"不存在索引为{strIndex}的RRU设备");
				return null;
			}

			// 得到RRU类型
			var rruTypeIndex = rru.GetNeedUpdateValue("netRRUTypeIndex");
			if (null == rruTypeIndex)
			{
				Log.Error($"查询索引为{strIndex}RRU的类型索引值失败");
				return null;
			}

			// 得到厂家索引
			var rruVendorIndex = rru.GetNeedUpdateValue("netRRUManufacturerIndex");
			if (null == rruVendorIndex)
			{
				Log.Error($"查询索引为{strIndex}RRU的厂家索引值失败");
				return null;
			}

			// 根据RRU类型和厂家索引，去器件库中查询RRU信息
			var rruPathInfoList = NPERruHelper.GetInstance()
				.GetRruPathInfoByTypeAndVendor(int.Parse(rruTypeIndex), int.Parse(rruVendorIndex));
			if (null == rruPathInfoList)
			{
				Log.Error($"根据RRU类型{rruTypeIndex}和厂家编号{rruVendorIndex}获取RRU通道信息失败");
				return null;
			}

			return rruPathInfoList;
		}

		/// <summary>
		/// 根据本地小区ID在天线阵安装规划表中找到CellId相同的信息，把CellId对应的属性设置为-1
		/// </summary>
		/// <param name="nLcId"></param>
		/// <returns></returns>
		public bool ResetRelateLcIdInNetRruAntSettingTblByLcId(int nLcId)
		{
			const EnumDevType devType = EnumDevType.rru_ant;
			var strLcId = nLcId.ToString();
			lock (_syncObj)
			{
				if (!m_mapAllMibData.ContainsKey(devType))
				{
					Log.Error($"未找到类型为{devType.ToString()}的规划信息");
					return false;
				}

				var devList = m_mapAllMibData[devType];
				if (null == devList)
				{
					Log.Error($"类型为{devType.ToString()}的信息为null");
					return true;
				}

				// 需要遍历所有的天线阵安装规划表
				if (devList.Any(dev => !NetDevLc.ResetNetLcConfig(dev, strLcId)))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 清理所有的数据
		/// </summary>
		public void Clear()
		{
			lock (_syncObj)
			{
				m_mapAllMibData.Clear();
				m_linkMgr.Clear();
			}
		}

		#endregion 公共接口

		#region 私有接口

		private MibInfoMgr()
		{
			m_mapAllMibData = new MAP_DEVTYPE_DEVATTRI();
			m_linkMgr = new NetPlanLinkMgr();
		}

		/// <summary>
		/// 判断已修改、所有、新添加的设备列表中是否存在与给定的索引相同设备
		/// </summary>
		/// <param name="mapMibInfo"></param>
		/// <param name="devType"></param>
		/// <param name="strIndex"></param>
		/// <returns></returns>
		private static bool HasSameIndexDev(MAP_DEVTYPE_DEVATTRI mapMibInfo, EnumDevType devType, string strIndex)
		{
			if (!mapMibInfo.ContainsKey(devType)) return false;

			var devList = mapMibInfo[devType];
			return HasSameIndexDev(devList, strIndex);
		}

		// 判断给定的列表中是否存在与strIndex相同索引的设备
		private static bool HasSameIndexDev(List<DevAttributeInfo> devList, string strIndex)
		{
			return null != devList && devList.Any(dev => dev.m_strOidIndex == strIndex);
		}

		private DevAttributeInfo GetSameIndexDev(List<DevAttributeInfo> devList, string strIndex)
		{
			return devList.FirstOrDefault(dev => dev.m_strOidIndex == strIndex);
		}

		/// <summary>
		/// 从map中删除数据
		/// </summary>
		/// <param name="mapData">待操作的map</param>
		/// <param name="devType">设备类型</param>
		/// <param name="dai">设备属性</param>
		/// <param name="bDelReal">是否立即删除，不仅是设置记录类型</param>
		public static void DelDevFromMap(MAP_DEVTYPE_DEVATTRI mapData, EnumDevType devType, DevAttributeInfo dai, bool bDelReal = false)
		{
			if (null == mapData || null == dai)
			{
				return;
			}

			if (!mapData.ContainsKey(devType))
			{
				return;
			}

			if (dai.m_recordType == RecordDataType.NewAdd || bDelReal)
			{
				mapData[devType].Remove(dai);
			}
			else
			{
				dai.SetDevRecordType(RecordDataType.WaitDel);
			}
		}

		/// <summary>
		/// 调整设备属性
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
					return true;
				}

				var devList = m_mapAllMibData[type];
				foreach (var dev in devList)
				{
					// 如果在待删除的列表中，就直接把新创建的设备放到已修改的列表中
					if (dev.m_strOidIndex != devIndex) continue;

					if (RecordDataType.WaitDel == dev.m_recordType)
					{
						devList.Remove(dev);

						// 需要比对dev和newDev，把dev的值和newDev的信息合并到一起，并修改设备record类型为modify
						newDev.AdjustOtherDevOriginValueToMyOrigin(dev);
						AddDevToMap(m_mapAllMibData, type, newDev);
					}

					return true;
				}

				// 流程走到这里，肯定是在devList中没有找到索引相同的设备
				AddDevToMap(m_mapAllMibData, type, newDev);
			}

			return true;
		}

		/// <summary>
		/// 生成一个指定类型和索引最后一级值的设备
		/// </summary>
		/// <param name="type"></param>
		/// <param name="lastIndexValue"></param>
		/// <returns></returns>
		private DevAttributeInfo GenerateNewDev(EnumDevType type, int lastIndexValue)
		{
			var dev = new DevAttributeInfo(type, lastIndexValue);
			if (dev.m_mapAttributes.Count == 0)
			{
				Log.Error($"创建类型为{type.ToString()}最后一个索引值为{lastIndexValue}的新dev失败");
				return null;
			}

			var devIndex = dev.m_strOidIndex;

			List<DevAttributeInfo> devList = null;
			lock (_syncObj)
			{
				if (m_mapAllMibData.ContainsKey(type))
				{
					devList = m_mapAllMibData[type];
				}
			}

			if (null != devList)
			{
				var oldDev = GetSameIndexDev(devList, devIndex);
				if (null != oldDev && RecordDataType.WaitDel != oldDev.m_recordType)
				{
					Log.Error($"存在相同类型{type.ToString()}相同索引{devIndex}的设备");
					NPLastErrorHelper.SetLastError($"已存在编号为{lastIndexValue}的{type.ToString()}设备");
					return null;
				}
			}

			dev.SetDevRecordType(RecordDataType.NewAdd);

			return dev;
		}

		/// <summary>
		/// 获取需要更新的值
		/// </summary>
		/// <param name="strOriginValue"></param>
		/// <param name="strLatestValue"></param>
		/// <returns></returns>
		private static string GetNeedUpdateValue(string strOriginValue, string strLatestValue)
		{
			if (string.IsNullOrEmpty(strOriginValue))
			{
				return null;
			}

			if (null == strLatestValue || strLatestValue == strOriginValue)
			{
				return strOriginValue;
			}

			return strLatestValue;
		}

		/// <summary>
		/// 从DevAttributeInfo的m_mapAttributes数据结构中取出strMibName对应的值
		/// 如果是枚举值就转换为字符串
		/// </summary>
		/// <param name="mapInfos"></param>
		/// <param name="strMibName"></param>
		/// <returns></returns>
		private static string GetEnumStringByMibName(IReadOnlyDictionary<string, MibLeafNodeInfo> mapInfos, string strMibName)
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

			// 现在m_strOriginValue和m_strLatestValue存的是数字对应的字符串，无需转换
			var ret = GetNeedUpdateValue(attri.m_strOriginValue, attri.m_strLatestValue);

			return ret;
		}

		/// <summary>
		/// 获取RRU通道对应的小区信息
		/// </summary>
		/// <param name="dev"></param>
		/// <param name="rpi"></param>
		/// <param name="mapResult"></param>
		/// <param name="mapLcStauts"></param>
		private static bool GetRruPortToCellInfo(DevAttributeInfo dev, RruPortInfo rpi,
			ref Dictionary<string, NPRruToCellInfo> mapResult,
			ref Dictionary<string, bool> mapLcStauts)
		{
			var rtc = new NPRruToCellInfo();
			var supportTx = rpi.rruTypePortNotMibRxTxStatus;
			foreach (var stx in supportTx)
			{
				var trxMap = MibStringHelper.SplitMibEnumString(stx.desc);
				if (null != trxMap)
				{
					rtc.SupportTxRxStatus.AddRange(trxMap.Values);
				}
			}

			var mapAttri = dev.m_mapAttributes;

			var rtxv = GetEnumStringByMibName(mapAttri, "netSetRRUPortTxRxStatus");
			if (null == rtxv)
			{
				if (rtc.SupportTxRxStatus.Count > 0)
				{
					rtc.RealTRx = rtc.SupportTxRxStatus.Last();
				}
			}
			else
			{
				rtc.RealTRx = rtxv;
			}

			var cellId1 = GetEnumStringByMibName(mapAttri, "netSetRRUPortSubtoLocalCellId");
			if (null != cellId1 && -1 != int.Parse(cellId1))
			{
				bool bCellFix;
				if (mapLcStauts.ContainsKey(cellId1))
				{
					bCellFix = mapLcStauts[cellId1];
				}
				else
				{
					bCellFix = NPCellOperator.IsFixedLc(cellId1);
					mapLcStauts.Add(cellId1, bCellFix);
				}
				rtc.CellIdList.Add(new CellAndState { cellId = cellId1, bIsFixed = bCellFix });
			}

			// 通道频道信息是根据小区的id从netLc表中找到netLcFreqBand字段的值
			// RRU 的ID相同，取出所有通道对应的信息
			for (var i = 2; i <= 4; i++)
			{
				var mibName = $"netSetRRUPortSubtoLocalCellId{i}";
				var cellId = GetEnumStringByMibName(mapAttri, mibName);
				if (null != cellId && "-1" != cellId)
				{
					bool bCellFix;
					if (mapLcStauts.ContainsKey(cellId))
					{
						bCellFix = mapLcStauts[cellId];
					}
					else
					{
						bCellFix = NPCellOperator.IsFixedLc(cellId);
						mapLcStauts.Add(cellId, bCellFix);
					}
					rtc.CellIdList.Add(new CellAndState { cellId = cellId, bIsFixed = bCellFix });
				}
			}

			var portNo = GetEnumStringByMibName(mapAttri, "netSetRRUPortNo");
			if (null == portNo)
			{
				return false;
			}

			var sb = new StringBuilder();

			var sfbList = rpi.rruTypePortSupportFreqBand;
			foreach (var sfb in sfbList)
			{
				var kvMap = MibStringHelper.SplitMibEnumString(sfb.desc);
				if (null == kvMap) continue;

				foreach (var kv in kvMap)
				{
					sb.Append($"{kv.Value}/");
				}
			}

			rtc.SupportFreqBand = sb.ToString().TrimEnd('/');
			mapResult.Add(portNo, rtc);

			return true;
		}

		/// <summary>
		/// 设置天线阵安装规划表的信息。修改时使用
		/// </summary>
		/// <param name="dev"></param>
		/// <param name="lcInfo"></param>
		/// <returns></returns>
		private static bool SetRruAntSettingTableInfo(DevAttributeInfo dev, NPRruToCellInfo lcInfo)
		{
			if (dev.m_mapAttributes.ContainsKey("netSetRRUPortTxRxStatus"))
			{
				var tempAtrri = dev.m_mapAttributes["netSetRRUPortTxRxStatus"];
				tempAtrri.SetLatestValue(lcInfo.RealTRx);
			}

			var lcIdList = lcInfo.CellIdList;
			var lcAttr1 = dev.m_mapAttributes["netSetRRUPortSubtoLocalCellId"];
			var lcAttr2 = dev.m_mapAttributes["netSetRRUPortSubtoLocalCellId2"];
			var lcAttr3 = dev.m_mapAttributes["netSetRRUPortSubtoLocalCellId3"];
			var lcAttr4 = dev.m_mapAttributes["netSetRRUPortSubtoLocalCellId4"];
			lcAttr1.SetLatestValue("-1");
			lcAttr2.SetLatestValue("-1");
			lcAttr3.SetLatestValue("-1");
			lcAttr4.SetLatestValue("-1");

			// 先设置为-1。todo 处于本地小区未建状态的LC是否可以挪动，待确定是否存在问题
			Func<MibLeafNodeInfo, string, bool> SetLcIdCfg = (attribute, newLcId) =>
			{
				//if (attribute.m_attRecordType == RecordDataType.NewAdd)		// 新增的小区配置属性才修改
				//{
				//	attribute.SetLatestValue(newLcId);
				//}
				attribute?.SetLatestValue(newLcId);
				return true;
			};

			// todo 目前每个通道只支持3个本地小区，后面可能会扩展
			for (var i = 1; i <= lcIdList.Count; i++)
			{
				var newValue = lcIdList[i - 1].cellId;
				switch (i)
				{
					case 1:
						SetLcIdCfg(lcAttr1, newValue);
						break;

					case 2:
						SetLcIdCfg(lcAttr2, newValue);
						break;

					case 3:
						SetLcIdCfg(lcAttr3, newValue);
						break;

					case 4:
						SetLcIdCfg(lcAttr4, newValue);
						break;

					default:
						break;
				}
			}

			return true;
		}

		/// <summary>
		/// 根据连接两端信息，获取连接的类型
		/// </summary>
		/// <param name="srcEndpoint">源端信息</param>
		/// <param name="dstEndpoint">目的端信息</param>
		/// <param name="linkType">ref 连接类型，传出参数</param>
		/// <returns>true:源和目的信息有效，获取到有效类型；false:其他情况</returns>
		private static bool GetLinkTypeBySrcDstEnd(LinkEndpoint srcEndpoint, LinkEndpoint dstEndpoint, ref EnumDevType linkType)
		{
			var link = new WholeLink(srcEndpoint, dstEndpoint);
			linkType = link.GetLinkType();
			return (linkType != EnumDevType.unknown);
		}

		#endregion 私有接口

		#region 私有成员、数据

		// 保存所有的网规MIB数据。开始保存的是从设备中查询回来的信息，如果对这些信息进行了修改、删除，就从这个数据结构中移动到对应的结构中
		// 最后下发网规信息时，就不再下发这个数据结构中的信息
		private MAP_DEVTYPE_DEVATTRI m_mapAllMibData;

		private readonly NetPlanLinkMgr m_linkMgr;

		private readonly object _syncObj = new object();

		#endregion 私有成员、数据
	}
}