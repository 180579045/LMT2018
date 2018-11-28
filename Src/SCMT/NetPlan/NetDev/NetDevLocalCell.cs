//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommonUtility;
//using LinkPath;
//using LogManager;

//namespace NetPlan
//{
//	internal sealed class NetDevLocalCell : NetDevBase
//	{
//		NetDevLocalCell()
//		{
//			m_mapLcInfo = new Dictionary<int, LcContent>(36);
//		}

//		#region 对外接口

//		// 进行小区规划


//		// 删除小区规划


//		// 取消规划


//		// 删除本地小区


//		// 去激活小区


//		// 查询本地小区的状态（返回最后状态）
//		LcStatus GetLcStatus(int nLcId)
//		{
//			if (!IsValidLcId(nLcId))
//			{
//				Log.Error($"传入的本地小区ID： {nLcId} 无效，有效范围[0,35]");
//				return LcStatus.Disabled;
//			}

//			if (m_mapLcInfo.ContainsKey(nLcId))
//			{
//				var lcc = m_mapLcInfo[nLcId];
//				return lcc.GetLcStatus();
//			}

//			var nlcc = new LcContent();
//			var lcs = nlcc.GetLcStatus();
//			m_mapLcInfo.Add(nLcId, nlcc);
//			return lcs;
//		}

//		#endregion


//		#region 私有接口

//		private static bool IsValidLcId(int nLcId)
//		{
//			return nLcId >= 0 && nLcId <= 35;
//		}


//		private Dictionary<int, LcContent> m_mapLcInfo;

//		#endregion
//	}


//	internal sealed class LcContent
//	{
//		private LcStatus m_originStatus;			// 本地小区原始状态
//		private LcStatus m_latestStatus;			// 本地小区最新状态
//		private DevAttributeInfo m_devSwitch;		// 保存布配开关信息
//		private DevAttributeInfo m_devLc;
//		private List<string> m_listRruAnt;			// 关联的天线阵安装规划
//		private int m_nLcId;

//		internal LcContent()
//		{
//			m_devSwitch = null;
//			m_devLc = null;
//			m_listRruAnt = new List<string>();
//		}

//		#region 相关的方法

//		/// <summary>
//		/// 获取本地小区的状态
//		/// todo 需要实际测试验证
//		/// </summary>
//		/// <returns></returns>
//		internal LcStatus GetLcStatus()
//		{
//			// 首先看布配开关的状态
//			var switchValue = GetSwitchValue();
//			if (-1 == switchValue)
//			{
//				Log.Error($"查询本地小区{m_nLcId}状态失败，返回-1");
//				return LcStatus.Disabled;
//			}

//			if (1 == switchValue)
//			{
//				Log.Debug($"本地小区{m_nLcId}当前状态为：规划中");
//				return LcStatus.Planning;
//			}

//			// 先判断devLc是否存在，如果不存在，直接返回。不存在说明基站中没有这个实例，之前也没有规划过
//			if (null == m_devLc)
//			{
//				return LcStatus.UnPlan;
//			}

//			// 如果devLc已经存在，判断是否为newAdd.
//			// 原因：如果是newadd的lc，说明基站中肯定没有这个实例，肯定不会存在小区已建和本地小区已建、未建这个3种状态
//			// 那么只有两种可能：规划中，和未规划。前面判断布配开关状态已经去掉了规划中状态，就只剩下未规划状态
//			if (RecordDataType.NewAdd == m_devLc.m_recordType)
//			{
//				return LcStatus.UnPlan;
//			}

//			// 不是Newadd，说明基站中有此实例，需要实时查询
//			// 当布配开关关闭时，需要实时查询本地小区和小区信息。涉及到行状态，且删除小区和本地小区时都下发到基站，内存中没有对应的数据
//			// todo 可能有trap消息
//			var cellOperaStatus = CommLinkPath.GetMibValueFromCmdExeResult(cellIndex, "GetNRCellInfo", "nrCellOperationalState", targetIp);
//			if ("0" == cellOperaStatus)
//			{
//				Log.Debug($"本地小区{nCellId}当前状态为：小区已建");
//				return LcStatus.CellBuilded;
//			}

//			var mapMibToValue = new DIC_DOUBLE_STR
//			{
//				{"nrLocalCellOperationalState", null}, {"nrLocalCellRowStatus", null}
//			};
//			var bSuc =
//				CommLinkPath.GetMibValueFromCmdExeResult(cellIndex, "GetLocalNRCellInfo", ref mapMibToValue, targetIp);
//			if (bSuc)   // 如果查询本地小区信息成功
//			{
//				if ("0" == mapMibToValue["nrLocalCellOperationalState"])
//				{
//					Log.Debug($"本地小区{nCellId}当前状态为：本地小区已建");
//					return LcStatus.LcBuilded;
//				}

//				if ("4" == mapMibToValue["nrLocalCellRowStatus"])
//				{
//					Log.Debug($"本地小区{nCellId}当前状态为：本地小区未建");
//					return LcStatus.LcUnBuilded;
//				}
//			}
//		}

//		/// <summary>
//		/// 获取布配开关的状态
//		/// </summary>
//		/// <returns></returns>
//		internal int GetSwitchValue()
//		{
//			string state;

//			if (null == m_devSwitch)
//			{
//				// 在内存中没有找到数据，就从基站中实时查询
//				const string cmd = "GetNRNetwokPlanControlSwitch";
//				const string mibName = "nrNetLocalCellCtrlConfigSwitch";

//				Log.Debug($"本地小区{m_nLcId}的布配开关在内存中不存在，开始从基站中查询...");

//				state = CommLinkPath.GetMibValueFromCmdExeResult($".{m_nLcId}", cmd, mibName, CSEnbHelper.GetCurEnbAddr());
//				if (null == state)
//				{
//					Log.Error($"命令 {cmd} 执行失败，查询 {mibName} 失败");
//					return -1;
//				}

//				// 从基站中查询成功，保存到内存中
//				if (!GenerateNewCtrlDev(state))
//				{
//					Log.Error($"保存本地小区{m_nLcId}布配开关到内存失败");
//					return -1;
//				}

//				Log.Debug($"从基站中查询到本地小区{m_nLcId}的布配开关为{state}");
//			}
//			else
//			{
//				state = MibInfoMgr.GetNeedUpdateValue(m_devSwitch, "nrNetLocalCellCtrlConfigSwitch");
//				if (null == state)
//				{
//					Log.Error("查询字段nrNetLocalCellCtrlConfigSwitch的值失败，需要确认MIB版本是否正确");
//					return -1;
//				}
//			}

//			int ost;
//			if (!int.TryParse(state, out ost))
//			{
//				Log.Error($"本地小区{m_nLcId}的布配开关值{state}错误，只能是(0:off|关闭/1:on|打开)");
//				return -1;
//			}

//			return ost;
//		}


//		internal bool GenerateNewCtrlDev(string strSwitchValue)
//		{
//			var dev = new DevAttributeInfo(EnumDevType.nrNetLcCtr, $".{m_nLcId}");

//			if (!dev.SetFieldOriginValue("nrNetLocalCellCtrlConfigSwitch", strSwitchValue, true))
//			{
//				Log.Error($"设置字段 nrNetLocalCellCtrlConfigSwitch 的 OriginValue 为{strSwitchValue}失败");
//				return false;
//			}

//			dev.m_recordType = RecordDataType.Original;
//			m_devSwitch = dev;

//			return true;
//		}


//		/// <summary>
//		/// 关掉布配开关，不会立即下发
//		/// </summary>
//		/// <returns></returns>
//		internal bool CloseSwitch()
//		{
			
//		}

//		/// <summary>
//		/// 打开布配开关，不会立即下发
//		/// </summary>
//		/// <returns></returns>
//		internal bool OpenSwitch()
//		{
			
//		}

//		/// <summary>
//		/// 下发布配开关到基站
//		/// </summary>
//		/// <returns></returns>
//		internal bool SendSwitchToEnb()
//		{
			
//		}

//		/// <summary>
//		/// 判断本地小区是否已经关联到RRU的通道
//		/// </summary>
//		/// <returns></returns>
//		bool HasLinkedToRru()
//		{
			
//		}

//		/// <summary>
//		/// 重置天线阵安装规划表中和该本地小区相关的所有配置
//		/// </summary>
//		/// <returns></returns>
//		bool ResetLinkedRruPort()
//		{
			
//		}

//		/// <summary>
//		/// 增加该小区关联的rru通道信息
//		/// </summary>
//		/// <returns></returns>
//		bool AddLinkedRruInfo()
//		{
			
//		}




//		#endregion
//	}
//}
