using System;
using CommonUtility;
using DataBaseUtil;
using LinkPath;
using LmtbSnmp;
using LogManager;
using SCMTOperationCore.Elements;
using DIC_DOUBLE_STR = System.Collections.Generic.Dictionary<string, string>;

// 网规小区相关的操作
namespace NetPlan
{
	public static class NPCellOperator
	{
		/// <summary>
		/// 设置网元布配控制开关状态，不下发到基站，没有什么意义
		/// </summary>
		/// <param name="bOpen">true:打开开关，false:关闭开关</param>
		/// <param name="nLcNo">本地小区编号</param>
		/// <param name="targetIp">目标基站地址</param>
		/// <returns>true:设置成功,false:设置失败</returns>
		public static bool SetNetPlanSwitch(bool bOpen, int nLcNo)
		{
			var strIndexTemp = $".{nLcNo}";

			const string mibName = "nrNetLocalCellCtrlConfigSwitch";

			var dValue = (bOpen ? "1" : "0");
			const EnumDevType devType = EnumDevType.nrNetLcCtr;

			// 修改内存中的数据，如果不存在，就添加
			var dev = MibInfoMgr.GetInstance().GetDevAttributeInfo(strIndexTemp, devType);
			if (null == dev)
			{
				dev = new DevAttributeInfo(devType, strIndexTemp);
				dev.SetFieldLatestValue(mibName, dValue);
				dev.SetFieldOriginValue(mibName, dValue, true);
				dev.m_recordType = RecordDataType.Original;
				MibInfoMgr.GetInstance().AddDevMibInfo(devType, dev);

				Log.Debug($"增加本地小区{nLcNo}布配开关为：{{bOpen ? \"打开\" : \"关闭\"}}");
			}
			else
			{
				MibInfoMgr.GetInstance().SetDevAttributeValue($".{nLcNo}", mibName, dValue, devType);
				Log.Debug($"修改本地小区{nLcNo}布配开关为：{{bOpen ? \"打开\" : \"关闭\"}}");
			}

			return true;
		}

		public static bool SetNetPlanSwitch(bool bOpen, string strLcIndex)
		{
			if (string.IsNullOrEmpty(strLcIndex))
			{
				Log.Error("传入的本地小区索引为null或空值");
				return false;
			}

			int nLcNo;
			if (!int.TryParse(strLcIndex.Trim('.'), out nLcNo))
			{
				Log.Error($"传入的本地小区索引{strLcIndex}无效");
				return false;
			}

			return SetNetPlanSwitch(bOpen, nLcNo);
		}

		/// <summary>
		/// 发送布配开关到基站中
		/// </summary>
		/// <param name="bOpen"></param>
		/// <param name="strLcIndex"></param>
		/// <param name="targetIp"></param>
		/// <returns></returns>
		public static bool SendNetPlanSwitchToEnb(bool bOpen, string strLcIndex, string targetIp)
		{
			const string cmd = "SetNRNetwokPlanControlSwitch";
			const string mibName = "nrNetLocalCellCtrlConfigSwitch";

			var dValue = (bOpen ? "1" : "0");
			var name2Value = new DIC_DOUBLE_STR { [mibName] = dValue };

			long reqId;
			var pdu = new CDTLmtbPdu();
			var ret = CDTCmdExecuteMgr.CmdSetSync(cmd, out reqId, name2Value, strLcIndex, targetIp, ref pdu);
			if (0 != ret)
			{
				Log.Error($"下发本地小区布配开关命令{cmd}失败，原因：{SnmpErrDescHelper.GetErrDescById(ret)}");
				return false;
			}

			return SetNetPlanSwitch(bOpen, strLcIndex);
		}

		/// <summary>
		/// 查询网规布配开关
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="targetIp"></param>
		/// <returns>-1:查询失败</returns>
		public static int GetNetPlanCtrlSwitch(string strIndex, string targetIp)
		{
			if (string.IsNullOrEmpty(strIndex) || string.IsNullOrEmpty(targetIp))
			{
				Log.Error("设置网规开关功能传入参数错误");
				return 0;
			}

			var strIndexTemp = strIndex.Trim('.');      // 去掉索引字符串前后的.
			strIndexTemp = $".{strIndexTemp}";

			// 先从内存中获取，如果没有找到再下发命令进行查询
			var ctrlDev = MibInfoMgr.GetInstance().GetDevAttributeInfo(strIndexTemp, EnumDevType.nrNetLcCtr);
			var ctrlValue = ctrlDev?.GetNeedUpdateValue("nrNetLocalCellCtrlConfigSwitch");
			if (ctrlValue != null)
			{
				return int.Parse(ctrlValue);
			}

			// 在内存中没有找到数据，就从基站中实时查询
			const string cmd = "GetNRNetwokPlanControlSwitch";
			const string mibName = "nrNetLocalCellCtrlConfigSwitch";

			Log.Debug($"本地小区{strIndexTemp}的布配开关在内存中不存在，开始从基站中查询...");

			var value = CommLinkPath.GetMibValueFromCmdExeResult(strIndex, cmd, mibName, targetIp);
			if (null == value)
			{
				Log.Error($"命令 {cmd} 执行失败，查询 {mibName} 失败");
				return -1;
			}

			// 从基站中查询成功，保存到内存中
			var newCtrlDev = MibInfoMgr.GetInstance().AddNewNetLcCtrlSwitch(strIndexTemp, value);
			if (null == newCtrlDev)
			{
				Log.Error($"保存本地小区{strIndex.Trim('.')}布配开关到内存失败");
				return -1;
			}

			Log.Debug($"从基站中查询到本地小区{strIndexTemp}的布配开关为{value}");

			return int.Parse(value);
		}

		/// <summary>
		/// 激活、去激活小区。操作之后要查询本地小区状态，设置颜色
		/// </summary>
		/// <param name="targetIp"></param>
		/// <param name="operType">操作类型</param>
		/// <param name="nDuration">延迟去激活的时长</param>
		/// <param name="nCellId"></param>
		/// <returns></returns>
		public static bool SetCellActiveTrigger(int nCellId, string targetIp, CellOperType operType, int nDuration = 1)
		{
			if (string.IsNullOrEmpty(targetIp) || nCellId < 0 || nCellId > 35)
			{
				Log.Error("激活/去激活功能传入参数错误");
				return false;
			}

			var strIndexTemp = $".{nCellId}";

			//var enbType = NodeBControl.GetInstance().GetEnbTypeByIp(targetIp);
			const EnbTypeEnum enbType = EnbTypeEnum.ENB_EMB6116;
			var cmdName = "SetCellActiveTrigger";
			var name2Value = new DIC_DOUBLE_STR();
			if (EnbTypeEnum.ENB_EMB6116 == enbType)
			{
				cmdName = "SetNRCellActiveTrigger";
				name2Value.Add("nrCellActiveTrigger", operType.ToString());
				name2Value.Add("nrCellDeactDelayTime", nDuration.ToString());
			}
			else
			{
				name2Value.Add("cellActiveTrigger", ((int)operType).ToString());
				name2Value.Add("cellDeactDelayTime", nDuration.ToString());
			}
			var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, strIndexTemp, targetIp);
			Log.Debug($"下发命令{cmdName}结果：{ret}（0：成功，其他值：失败）");
			if (0 != ret)
			{
				return false;
			}

			if (!MibInfoMgr.GetInstance().DelDevFromMemory(strIndexTemp, EnumDevType.nrCell))
			{
				Log.Error($"从内存中删除索引为{strIndexTemp}的小区信息失败");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 删除本地小区（注意，不是本地小区规划），操作之后要查询本地小区状态，设置颜色
		/// </summary>
		/// <param name="nLocalCellId"></param>
		/// <param name="targetIp"></param>
		/// <returns></returns>
		public static bool DelLocalCell(int nLocalCellId, string targetIp)
		{
			if (string.IsNullOrEmpty(targetIp) || nLocalCellId < 0 || nLocalCellId > 35)
			{
				Log.Error("激活/去激活功能传入参数错误");
				return false;
			}

			var strIndexTemp = $".{nLocalCellId}";

			var cmdName = "SetNRLcAddDelTrigger";
			var name2Value = new DIC_DOUBLE_STR { { "nrLocalCellConfigTrigger", "1" } };

			var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, strIndexTemp, targetIp);
			Log.Debug($"下发命令{cmdName}结果：{ret}（0：成功，其他值：失败）");
			if (0 != ret)
			{
				return false;
			}

			// 删除本地小区后，只有nrCellEntry表变化，本地小区规划信息仍然存在
			if (!MibInfoMgr.GetInstance().DelDevFromMemory(strIndexTemp, EnumDevType.nrLc))
			{
				Log.Error($"从内存中删除索引为{strIndexTemp}的本地小区失败");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 获取本地小区的状态
		/// 一些必然的知识：
		/// 1.小区激活时，无法对本地小区进行规划
		/// 2.规划开关打开时，本地小区和小区都无法建立、激活
		/// 3.本地小区已建立，不能进行网规
		/// 判断本地小区的状态：
		/// netLocalCellCtrlEntry表中netPlanControlLcConfigSwitch为打开状态时，该小区映射为规划中
		/// 以下几条的前置条件：netLocalCellCtrlEntry表中netPlanControlLcConfigSwitch为关闭状态
		///	cellOperationalState值为0:enabled|可用时，该小区映射为小区已建
		///	cellOperationalState值不是0:enabled|可用 && lcOperationalState值为0:enabled|可用，该小区映射为本地小区已建
		///	cellOperationalState值不是0:enabled|可用 && lcOperationalState值不是0:enabled|可用 && netLcRowStatus为4:createAndGo|行有效，且该小区映射为本地小区未建
		///	lcRowStatus不是4:createAndGo|行有效，该小区映射为未规划
		/// </summary>
		/// <returns></returns>
		public static LcStatus GetLcStatus(int nCellId, string targetIp)
		{
			var cellIndex = $".{nCellId}";
			var switchValue = GetNetPlanCtrlSwitch(cellIndex, targetIp);
			if (-1 == switchValue)
			{
				Log.Error($"查询本地小区{nCellId}状态失败，返回-1");
				return LcStatus.Disabled;
			}

			if (1 == switchValue)
			{
				Log.Debug($"本地小区{nCellId}当前状态为：规划中");
				return LcStatus.Planning;
			}

			// 查询nrNetLcDev是否存在，如果不存在说明基站中肯定没有这个实例，且没有规划过
			var lcDev = MibInfoMgr.GetInstance().GetDevAttributeInfo(cellIndex, EnumDevType.nrNetLc);
			if (null == lcDev)
			{
				Log.Debug($"索引为{nCellId}的本地小区实例不存在，状态为未规划");
				return LcStatus.UnPlan;
			}

			// 如果devLc已经存在，判断是否为newAdd.
			// 原因：如果是newadd的lc，说明基站中肯定没有这个实例，肯定不会存在小区已建和本地小区已建、未建这个3种状态
			// 那么只有两种可能：规划中，和未规划。前面判断布配开关状态已经去掉了规划中状态，就只剩下未规划状态
			if (RecordDataType.NewAdd == lcDev.m_recordType)
			{
				return LcStatus.UnPlan;
			}

			// 不是Newadd，说明基站中有此实例，需要实时查询
			// 当布配开关关闭时，需要实时查询本地小区和小区信息。涉及到行状态，且删除小区和本地小区时都下发到基站，内存中没有对应的数据
			var cellOperaStatus = CommLinkPath.GetMibValueFromCmdExeResult(cellIndex, "GetNRCellInfo", "nrCellOperationalState", targetIp);
			if ("0" == cellOperaStatus)
			{
				Log.Debug($"本地小区{nCellId}当前状态为：小区已建");
				return LcStatus.CellBuilded;
			}

			var mapMibToValue = new DIC_DOUBLE_STR
				{
					{"nrLocalCellOperationalState", null}, {"nrLocalCellRowStatus", null}
				};
			var bSuc =
				CommLinkPath.GetMibValueFromCmdExeResult(cellIndex, "GetLocalNRCellInfo", ref mapMibToValue, targetIp);
			if (bSuc)   // 如果查询本地小区信息成功
			{
				if ("2" != mapMibToValue["nrLocalCellOperationalState"] && "4" == mapMibToValue["nrLocalCellRowStatus"])
				{
					Log.Debug($"本地小区{nCellId}当前状态为：本地小区已建");
					return LcStatus.LcBuilded;
				}
			}

			return LcStatus.LcUnBuilded;
		}

		/// <summary>
		/// 删除本地小区规划信息。只有处于本地小区未建状态才能删除网规信息
		/// 如果删掉的是enb中的信息，就立即下发snmp命令
		/// 1）下发SetNetRRUAntennaLcID命令
		/// 2）netPlanControlLcConfigSwitch置为打开
		/// 3）DelLocalCellNetworkPlan命令删除LC信息
		/// 4）netPlanControlLcConfigSwitch置为关闭
		/// 5）刷新小区的状态
		/// 6）和本LC相关的天线安装规划表信息cellid相同设置为-1
		/// 7）删除本地小区信息
		/// 如果删掉的新添加的本地小区规划，则不立即下发snmp命令，流程类似设备相关
		/// 1）删除本地小区信息
		/// 2）和本LC相关的天线安装规划表信息cellid相同设置为-1
		/// 3）netPlanControlLcConfigSwitch置为关闭
		/// 4）刷新小区状态
		/// </summary>
		/// <param name="nLocalCellId">本地小区编号</param>
		/// <param name="targetIp"></param>
		/// <returns></returns>
		public static bool DelLcNetPlan(int nLocalCellId, string targetIp)
		{
			var lcState = GetLcStatus(nLocalCellId, targetIp);
			if (LcStatus.LcUnBuilded != lcState)
			{
				Log.Error($"删除本地小区{nLocalCellId}网规信息失败，本地小区状态：{lcState.ToString()},只有本地小区未建状态才能删除");
				return false;
			}

			// 设置本地小区相关的天线安装规划表信息
			if (!MibInfoMgr.GetInstance().ResetRelateLcIdInNetRruAntSettingTblByLcId(nLocalCellId))
			{
				Log.Error($"设置本地小区{nLocalCellId}相关的天线安装规划表信息失败");
				return false;
			}

			// 打开本地小区的布配开关
			//if (!SetNetPlanSwitch(true, nLocalCellId, targetIp))
			//{
			//	Log.Error($"删除本地小区{nLocalCellId}网规信息失败，原因：打开小区布配开关失败");
			//	return false;
			//}

			// 统一处理方式，从enb中的查到的信息也不立即下发
			// 删除本地小区网规信息
			if (!MibInfoMgr.GetInstance().DelDev($".{nLocalCellId}", EnumDevType.nrNetLc))
			{
				Log.Error($"删除本地小区{nLocalCellId}的规划信息失败");
				return false;
			}

			// 直接关闭本地小区布配开关
			if (!SetNetPlanSwitch(false, nLocalCellId))
			{
				Log.Error($"删除本地小区{nLocalCellId}网规信息失败，原因：关闭小区布配开关失败");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 判断本地小区是否可以在RRU端口配置中被修改
		/// </summary>
		/// <param name="strLcId"></param>
		/// <returns></returns>
		public static bool IsFixedLc(string strLcId)
		{
			return false;
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("尚未选中基站");
			}

			if (string.IsNullOrEmpty(strLcId))
			{
				throw new ArgumentNullException(strLcId);
			}

			var lcStatus = GetLcStatus(int.Parse(strLcId), targetIp);

			return (lcStatus != LcStatus.Planning);
		}

		/// <summary>
		/// 取消本地小区规划。本地小区状态变化：规划中-->未规划
		/// todo 后台需要做的操作：1.关闭布配开关；2.设置本地小区之前的状态
		/// todo 需要原子性保证？
		/// </summary>
		/// <param name="nLcId"></param>
		/// <returns></returns>
		public static bool CancelLcPlanOp(int nLcId)
		{
			var lcStatus = GetLcStatus(nLcId, CSEnbHelper.GetCurEnbAddr());
			if (lcStatus != LcStatus.Planning)
			{
				Log.Error($"本地小区{nLcId}的状态为{lcStatus.ToString()}，不能执行取消规划操作。");
				return false;
			}

			// 删掉本地小区信息
			if (!MibInfoMgr.GetInstance().DelDev($".{nLcId}", EnumDevType.nrNetLc))
			{
				Log.Error($"删除本地小区{nLcId}的属性信息失败");
				return false;
			}

			// 重置天线安装规划表中相关的记录
			if (!MibInfoMgr.GetInstance().ResetRelateLcIdInNetRruAntSettingTblByLcId(nLcId))
			{
				Log.Error($"删除本地小区{nLcId}后，重置天线阵安装规划表中的信息失败");
				return false;
			}

			// 关闭布配开关
			if (!SetNetPlanSwitch(false, nLcId))
			{
				Log.Error($"本地小区{nLcId}的布配开关关闭失败");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 右键菜单：进行小区规划响应函数
		/// </summary>
		/// <param name="nLcId"></param>
		/// <param name="strTargetIp"></param>
		/// <returns></returns>
		public static bool AddNewNrLc(int nLcId, string strTargetIp)
		{
			if (!SetNetPlanSwitch(true, nLcId))
			{
				return false;
			}

			return null != MibInfoMgr.GetInstance().AddNewLocalCell(nLcId);
		}
	}

	// 本地小区的状态
	public enum LcStatus
	{
		UnPlan,
		Planning,
		CellBuilded,
		LcBuilded,
		LcUnBuilded,
		Disabled
	}

	// 小区的操作类型
	public enum CellOperType
	{
		active = 0,
		deactive = 1,
		shuttingDown = 2
	}
}