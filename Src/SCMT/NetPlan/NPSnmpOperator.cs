using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using DIC_DOUBLE_STR = System.Collections.Generic.Dictionary<string, string>;
using ONE_DEV_ATTRI_INFO = System.Collections.Generic.List<NetPlan.MibLeafNodeInfo>;

namespace NetPlan
{
	/// <summary>
	/// 网规snmp相关的操作
	/// </summary>
	public class NPSnmpOperator
	{
		#region 公共接口

		/// <summary>
		/// 设置网规开始开关
		/// </summary>
		/// <param name="bOpen">true:打开开关，false:关闭开关</param>
		/// <param name="strIndex">索引</param>
		/// <param name="targetIp">目标基站地址</param>
		/// <returns>true:设置成功,false:设置失败</returns>
		public static bool SetNetPlanSwitch(bool bOpen, string strIndex, string targetIp)
		{
			if (string.IsNullOrEmpty(strIndex) || string.IsNullOrEmpty(targetIp))
			{
				Log.Error("设置网规开关功能传入参数错误");
				return false;
			}

			var strIndexTemp = strIndex.Trim('.');      // 去掉索引字符串前后的.
			strIndexTemp = $".{strIndexTemp}";

			var name2Value = new DIC_DOUBLE_STR();
			name2Value["netPlanControlLcConfigSwitch"] = (bOpen ? "1" : "0");

			const string cmd = "SetNetwokPlanControlSwitch";
			long reqId;
			var pdu = new CDTLmtbPdu();

			var ret = CDTCmdExecuteMgr.CmdSetSync(cmd, out reqId, name2Value, strIndexTemp, targetIp, ref pdu);

			return (0 == ret);
		}

		/// <summary>
		/// 传入命令名查询网规信息
		/// </summary>
		/// <param name="strCmdName">要执行的命令名</param>
		/// <param name="result">出参：查询结果，key:oid, value: real value </param>
		/// <returns>true:查询成功,false:查询失败</returns>
		public static bool QueryNetPlanInfo(string strCmdName, out DIC_DOUBLE_STR result, out List<string> indexList)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				Log.Error($"未选中基站");
				result = null;
				indexList = null;
				return false;
			}

			if (string.IsNullOrEmpty(strCmdName))
			{
				throw new CustomException("传入命令名为null");
			}

			long reqId;
			var ret = CDTCmdExecuteMgr.GetInstance().CmdGetNextSync(strCmdName, out reqId, out indexList, out result, targetIp);

			if (0 == ret)
				return true;

			result = null;
			return false;
		}

		// 查询布配的板卡信息
		public static bool QueryNetBoard(string cmdName, ref List<ONE_DEV_ATTRI_INFO> sameTypeDevInfoList)
		{
			if (string.IsNullOrEmpty(cmdName))
			{
				return false;
			}

			DIC_DOUBLE_STR result;	// key:完全oid，包括前缀和索引，value:该项对应的真实值
			List<string> indexList;
			if (!QueryNetPlanInfo(cmdName, out result, out indexList))
			{
				return false;
			}

			// TODO 下面的操作有重复计算，需要处理
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			var cmdInfo = Database.GetInstance().GetCmdDataByName(cmdName, targetIp);
			if (null == cmdInfo)
			{
				return false;
			}

			var parentName = cmdInfo.m_tableName;
			var tbl = Database.GetInstance().GetMibDataByTableName(parentName, targetIp);
			if (null == tbl)
			{
				return false;
			}

			var indexGrade = tbl.indexNum;		// 索引级数
			var childList = tbl.childList;

			// 存储同一类设备的所有信息
			if (null == sameTypeDevInfoList)
			{
				sameTypeDevInfoList = new List<ONE_DEV_ATTRI_INFO>();
			}

			// 区分标量和表量
			if (indexGrade == 0)	// 索引级数为0，认为是标量表
			{
				// 存储一个设备的所有属性信息
				var oneDevAttributes = GetScalarMibInfo(childList, result);
				if (oneDevAttributes.Count > 0)
				{
					sameTypeDevInfoList.Add(oneDevAttributes);
				}

				return true;
			}

			// 表量表
			// 此处在indexList中循环，一个indexList元素就是一行数据，也就是一个完整的属性。
			foreach (var index in indexList)
			{
				var oneDevAttributes = GetTableMibInfo(index, childList, result);

				// 一个设备的所有信息查询完成，保存数据
				if (oneDevAttributes.Count > 0)
				{
					sameTypeDevInfoList.Add(oneDevAttributes);
				}
			}

			return true;
		}

		#endregion

		#region 私有接口

		/// <summary>
		/// 根据Oid从mapOidAndValue中获取真实值后创建一个MibLeafNodeInfo对象
		/// </summary>
		/// <param name="strOid"></param>
		/// <param name="mapOidAndValue"></param>
		private static MibLeafNodeInfo GetMibLeafNodeWithRealValue(string strOid, DIC_DOUBLE_STR mapOidAndValue, MibLeaf mibLeaf)
		{
			if (string.IsNullOrEmpty(strOid))
			{
				return null;
			}

			// 判断是否存在于result中
			if (!mapOidAndValue.ContainsKey(strOid))
			{
				return null;
			}

			// 该表项有值，就处理
			var info = new MibLeafNodeInfo
			{
				m_strRealValue = mapOidAndValue[strOid],
				mibAttri = mibLeaf
			};

			return info;
		}

		/// <summary>
		/// 获取标量MIB信息
		/// </summary>
		/// <returns></returns>
		private static ONE_DEV_ATTRI_INFO GetScalarMibInfo(List<MibLeaf> childList, DIC_DOUBLE_STR result)
		{
			var devAttributes = new ONE_DEV_ATTRI_INFO();

			if (null == childList || null == result)
			{
				return devAttributes;
			}

			var mibPrefix = SnmpToDatabase.GetMibPrefix().Trim('.');

			// 对于标量的处理，在oid后追加.0
			foreach (var childLeaf in childList)
			{
				if (0 == childLeaf.isMib)
				{
					// 过滤掉假MIB
					continue;
				}

				var childFullOid = $"{mibPrefix}.{childLeaf.childOid.Trim('.')}.0";
				var info = GetMibLeafNodeWithRealValue(childFullOid, result, childLeaf);
				if (null != info)
				{
					devAttributes.Add(info);
				}
			}

			return devAttributes;
		}

		/// <summary>
		/// 获取标量表代表的一行信息
		/// </summary>
		/// <param name="strIndex"></param>
		/// <param name="childList"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		private static ONE_DEV_ATTRI_INFO GetTableMibInfo(string strIndex, List<MibLeaf> childList, DIC_DOUBLE_STR result)
		{
			var devAttributes = new ONE_DEV_ATTRI_INFO();

			if (string.IsNullOrEmpty(strIndex) || null == childList || null == result)
			{
				return devAttributes;
			}

			var mibPrefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			foreach (var childLeaf in childList)
			{
				if (0 == childLeaf.isMib)
				{
					// 跳过假mib
					continue;
				}

				// 判断是否是索引，如果是索引，就不会出现在result中
				if (childLeaf.IsIndex.Equals("True", StringComparison.OrdinalIgnoreCase))
				{
					// 根据子节点的顺序截取索引中的一段作为该项的值
					var realValue = MibStringHelper.GetRealValueFromIndex(strIndex, childLeaf.childNo);
					if (null == realValue)
					{
						continue;
					}

					var info = new MibLeafNodeInfo
					{
						m_strRealValue = realValue,
						mibAttri = childLeaf,
						m_bReadOnly = true          // 索引，只读
					};

					devAttributes.Add(info);
				}
				else
				{
					var childFullOid = $"{mibPrefix}.{childLeaf.childOid.Trim('.')}.{strIndex.Trim('.')}";
					var info = GetMibLeafNodeWithRealValue(childFullOid, result, childLeaf);
					if (null != info)
					{
						devAttributes.Add(info);
					}
				}
			}
			// TODO 注意：此处没有考虑result中是否会有剩余数据

			return devAttributes;
		}
		#endregion
	}
}
