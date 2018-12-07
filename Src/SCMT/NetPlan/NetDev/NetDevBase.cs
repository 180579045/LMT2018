using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using DataBaseUtil;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using SCMTOperationCore.Elements;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
	internal class NetDevBase
	{
		#region 构造函数

		internal NetDevBase(string strTargetIp, MAP_DEVTYPE_DEVATTRI mapOriginData)
		{
			m_strTargetIp = strTargetIp;
			m_mapOriginData = mapOriginData;
		}

		#endregion

		#region 虚函数区

		/// <summary>
		/// 下发网规信息
		/// </summary>
		/// <param name="dev">待下发的设备</param>
		/// <param name="mapOriginData">原始数据</param>
		/// <param name="bDlAntWcb">是否下载天线阵权重信息</param>
		/// <returns></returns>
		internal virtual bool DistributeToEnb(DevAttributeBase dev, bool bDlAntWcb = false)
		{
			var cmdType = dev.m_recordType.ConvertToSct();

			if (EnumSnmpCmdType.Invalid == cmdType)
			{
				Log.Error($"下发网规信息功能传入SNMP命令类型错误，无法下发索引为{dev.m_strOidIndex}的设备信息");
				return false;
			}

			if (EnumSnmpCmdType.Get == cmdType)
			{
				Log.Debug($"无需下发类型为{dev.m_recordType.ToString()}索引为{dev.m_strOidIndex}设备信息");
				return true;
			}

			// todo 多次获取命令的信息，流程待优化
			var cmdList = NPECmdHelper.GetInstance().GetCmdList(dev.m_strEntryName, cmdType);

			if (null == cmdList || 0 == cmdList.Count)
			{
				Log.Error($"未找到表入口名为{dev.m_strEntryName}的{cmdType.ToString()}相关命令");
				return false;
			}

			var cmdToMibLeafMap = NPECmdHelper.GetInstance().GetSameTypeCmdMibLeaf(cmdList);
			if (null == cmdToMibLeafMap || 0 == cmdToMibLeafMap.Count)
			{
				Log.Error($"未找到表入口名为{dev.m_strEntryName}的{cmdType.ToString()}相关命令详细信息");
				return false;
			}

			var strRs = "4";
			switch (cmdType)
			{
				case EnumSnmpCmdType.Set:
				case EnumSnmpCmdType.Add:
					break;

				case EnumSnmpCmdType.Del:
					strRs = "6";
					break;
			}

			// 有些设备可能会有多个set命令
			foreach (var kv in cmdToMibLeafMap)
			{
				var cmdName = kv.Key;
				var mibLeafList = kv.Value;

				var name2Value = dev.GenerateName2ValueMap(mibLeafList, strRs);
				if (null == name2Value || name2Value.Count == 0)
				{
					Log.Error($"索引为{dev.m_strOidIndex}的设备生成name2value失败");
					return false;
				}

				var ret = CDTCmdExecuteMgr.CmdSetSync(cmdName, name2Value, dev.m_strOidIndex, m_strTargetIp);
				if (0 != ret)
				{
					Log.Error($"下发命令{cmdName}失败，原因：{SnmpErrDescHelper.GetErrDescById(ret)}");
					return false;
				}
			}

			return true;
		}

		internal bool DistributeToEnb(DevAttributeBase dev, string strCmdName)
		{
			var cmdInfo = SnmpToDatabase.GetCmdInfoByCmdName(strCmdName, m_strTargetIp);
			if (null == cmdInfo)
			{
				return false;
			}

			var oidList = cmdInfo.m_leaflist;
			var mibLeafInfoList = SnmpToDatabase.ConvertOidListToMibInfoList(oidList, m_strTargetIp);
			var leafInfoList = mibLeafInfoList as IList<MibLeaf> ?? mibLeafInfoList.ToList();
			if (!leafInfoList.Any())
			{
				return false;
			}

			var name2Value = dev.GenerateName2ValueMap(leafInfoList.ToList(), "4");
			if (null == name2Value || name2Value.Count == 0)
			{
				Log.Error($"索引为{dev.m_strOidIndex}的设备生成name2value失败");
				return false;
			}

			var ret = CDTCmdExecuteMgr.CmdSetSync(strCmdName, name2Value, dev.m_strOidIndex, m_strTargetIp);
			if (0 != ret)
			{
				Log.Error($"下发命令{strCmdName}失败，原因：{SnmpErrDescHelper.GetErrDescById(ret)}");
				return false;
			}

			return true;
		}

		// 后处理
		internal virtual bool PostDeal(DevAttributeBase dev)
		{
			return true;
		}

		#endregion

		#region 非虚函数

		internal EnumSnmpCmdType GetSnmCmdTypeFromWcbOpType(AntWcbOpType opType)
		{
			var cmdType = EnumSnmpCmdType.Invalid;
			switch (opType)
			{
				case AntWcbOpType.skip:
					break;
				case AntWcbOpType.only_add:
					cmdType = EnumSnmpCmdType.Add;
					break;
				case AntWcbOpType.only_del:
					cmdType = EnumSnmpCmdType.Del;
					break;
				case AntWcbOpType.only_set:
					cmdType = EnumSnmpCmdType.Set;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(opType), opType, null);
			}

			return cmdType;
		}

		/// <summary>
		/// 计算数据类型为BITS的值。
		/// 在解析出来的json文件中，已经把excel中bit对应的值转换为10进制值
		/// 如rruTypeFiberLength这个节点的取值范围是：0:ten|10km/1:twenty|20km/2:forty|40km，MIB类型为BITS，转换为10进制后就是：1-10km,2-20km,4-40km
		/// 如果一个设备支持多种取值，需要进行加法运算。如果一个rru支持20km和40km拉远，设置对应的数据节点值就应该是2+4=6
		/// </summary>
		/// <param name="listOriginVd"></param>
		/// <returns></returns>
		protected static int CalculateBitsValue(IEnumerable<VD> listOriginVd)
		{
			var tmp = 0;
			return listOriginVd.Where(item => int.TryParse(item.value, out tmp)).Sum(item => tmp);
		}

		#endregion

		#region 内部数据

		protected MAP_DEVTYPE_DEVATTRI m_mapOriginData;
		protected string m_strTargetIp;

		#endregion
	}
}
