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
		/// 设置网元布配控制开关状态
		/// 布配开关的特殊性：在最后点击下发网规参数时才关闭，所以此处立即下发到基站
		/// 最后下发网规信息时，可以直接下发本地小区相关的参数，不需要先打开布配开关，再下发参数
		/// 调用时机：1.右键菜单：进行小区规划 点击事件；2.下发小区参数后调用，关闭布配开关
		/// </summary>
		/// <param name="bOpen">true:打开开关，false:关闭开关</param>
		/// <param name="nLcNo">本地小区编号</param>
		/// <param name="targetIp">目标基站地址</param>
		/// <returns>true:设置成功,false:设置失败</returns>
		public static bool SetNetPlanSwitch(bool bOpen, int nLcNo, string targetIp)
		{
			if (string.IsNullOrEmpty(targetIp))
			{
				throw new CustomException("设置网规开关功能传入参数错误");
			}

			var strIndexTemp = $".{nLcNo}";

			const string cmd = "SetNRNetwokPlanControlSwitch";
			const string mibName = "nrNetLocalCellCtrlConfigSwitch";

			var dValue = (bOpen ? "1" : "0");
			var name2Value = new DIC_DOUBLE_STR { [mibName] = dValue };

			long reqId;
			var pdu = new CDTLmtbPdu();
			var ret = CDTCmdExecuteMgr.CmdSetSync(cmd, out reqId, name2Value, strIndexTemp, targetIp, ref pdu);
			if (0 != ret)
			{
				if (2 == ret)
				{
					var desc = SnmpErrDescHelper.GetLastErrorDesc();
					Log.Error($"下发本地小区布配开关命令{cmd}失败，原因：{desc}");
				}
				else
				{
					Log.Error($"下发本地小区布配开关命令{cmd}失败");
				}

				return false;
			}

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

				Log.Debug($"增加本地小区{nLcNo}布配开关为{{bOpen ? \"打开\" : \"关闭\"}}");
			}
			else
			{
				MibInfoMgr.GetInstance().SetDevAttributeValue($".{nLcNo}", mibName, dValue, devType);
				Log.Debug($"修改本地小区{nLcNo}布配开关为{{bOpen ? \"打开\" : \"关闭\"}}");
			}

			return true;
		}

		public static bool SetNetPlanSwitch(bool bOpen, string strLcIndex, string targetIp)
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

			return SetNetPlanSwitch(bOpen, nLcNo, targetIp);
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
			if (null != ctrlDev)
			{
				var ctrlValue = MibInfoMgr.GetDevAttributeValue(ctrlDev, "nrNetLocalCellCtrlConfigSwitch");
				if (null != ctrlValue)
				{
					return int.Parse(ctrlValue);
				}
			}

			// 在内存中没有找到数据，就从基站中实时查询
			const string cmd = "GetNRNetwokPlanControlSwitch";
			const string mibName = "nrNetLocalCellCtrlConfigSwitch";

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

			// 当布配开关关闭时，需要实时查询本地小区和小区信息。涉及到行状态，且删除小区和本地小区时都下发到基站，内存中没有对应的数据
			var enbType = EnbTypeEnum.ENB_EMB6116;
			if (EnbTypeEnum.ENB_EMB6116 == enbType)
			{
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
				if (bSuc)	// 如果查询本地小区信息成功
				{
					//Log.Error($"命令 GetLocalNRCellInfo 执行失败，原因：{SnmpErrDescHelper.GetLastErrorDesc()}。");
					//if (342 == SnmpErrDescHelper.GetLastErrorCode())
					//{
					//	return LcStatus.UnPlan;
					//}
					//return LcStatus.Disabled;

					if ("0" == mapMibToValue["nrLocalCellOperationalState"])
					{
						Log.Debug($"本地小区{nCellId}当前状态为：本地小区已建");
						return LcStatus.LcBuilded;
					}

					if ("4" == mapMibToValue["nrLocalCellRowStatus"])
					{
						Log.Debug($"本地小区{nCellId}当前状态为：本地小区未建");
						return LcStatus.LcUnBuilded;
					}
				}

				// 查询网规的本地小区信息
				var nnLc = MibInfoMgr.GetInstance().GetNrNetLcInfoByID(cellIndex);
				if (null != nnLc)
				{
					var nlcrs = MibInfoMgr.GetNeedUpdateValue(nnLc, "nrNetLocalCellRowStatus");
					if ("4" != nlcrs)
					{
						return LcStatus.UnPlan;
					}
					return LcStatus.LcUnBuilded;
				}

				return LcStatus.UnPlan;
			}

			return LcStatus.Disabled;
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
				Log.Error($"删除本地小区{nLocalCellId}网规信息失败，本地小区状态：{lcState.ToString()}");
				return false;
			}

			// 统一处理方式，从enb中的查到的信息也不立即下发
			// 删除本地小区网规信息
			if (!MibInfoMgr.GetInstance().DelDev($".{nLocalCellId}", EnumDevType.nrNetLc))
			{
				Log.Error($"删除本地小区{nLocalCellId}的规划信息失败");
				return false;
			}

			// 设置本地小区相关的天线安装规划表信息
			if (!MibInfoMgr.GetInstance().ResetRelateLcIdInNetRruAntSettingTblByLcId(nLocalCellId))
			{
				Log.Error($"设置本地小区{nLocalCellId}相关的天线安装规划表信息失败");
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