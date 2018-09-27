using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SnmpSharpNet;
using MAP_STRING = System.Collections.Generic.Dictionary<string, string>;

// snmp 到 database 的所需接口封装
namespace SCMTOperationCore.Message.SNMP
{
	public static class SnmpToDatabase
	{
		#region 公共静态接口

		// 从 snmp 的 Octet string 类型转换为 string
		public static string GetDateTimeStringFromOct(OctetString origin)
		{
			string dtime = "";

			if (DATEANDTIMELENV2TC == origin.Length)
			{
				if (0 == origin[0])
				{
					return "0000-00-00 00:00:00";
				}

				var data = origin.ToArray();
				ushort year = BitConverter.ToUInt16(data, 0);
				year = SerializeHelper.ReverseUshort(year);
				DateTime t = new DateTime(year, data[2], data[3], data[4], data[5], data[6]);
				dtime = TimeHelper.DateTimeToString(t);
			}
			else if (DATEANDTIMEDTCUSTOM == origin.Length)
			{
				Vb tempVb = new Vb {Value = origin};
				// TODO 还不知道怎么做
			}
			else
			{
				Log.Error("长度错误");
			}

			return dtime;
		}

		// 根据命令名找到命令对应的所有信息
		public static CmdMibInfo GetCmdInfoByCmdName(string cmdName, string ip)
		{
			return Database.GetInstance().GetCmdDataByName(cmdName, ip);
		}

		// 获取公共MIB前缀。最后带.字符
		public static string GetMibPrefix()
		{
			return "1.3.6.1.4.1.5105.100.";
		}

		// 获取snmp的团体名
		public static string GetCommunityString()
		{
			return "public";
		}

		// 同步方式获取cmd执行结果后从pdu中根据mib name获取对应的值
		public static string GetMibValueFromCmdExeResult(string index, string cmdName, string mibName, string boardAddr)
		{
			long lrequestId = 0;
			string csCmdValueTemp = null;
			CDTLmtbPdu inOutPdu = new CDTLmtbPdu();

			var ret = CDTCmdExecuteMgr.GetInstance().CmdGetSync(cmdName, out lrequestId, index, boardAddr, ref inOutPdu);
			if (0 == ret)
			{
				inOutPdu.GetValueByMibName(boardAddr, mibName, out csCmdValueTemp, index);
			}

			return csCmdValueTemp;
		}

		// 同步方式获取cmd执行结果，从pdu中取出mib name对应的值
		public static bool GetMibValueFromCmdExeResult(string index, string cmdName, ref MAP_STRING mibNames,
			string boardAddr)
		{
			long lrequestId = 0;
			CDTLmtbPdu inOutPdu = new CDTLmtbPdu();

			var ret = CDTCmdExecuteMgr.GetInstance().CmdGetSync(cmdName, out lrequestId, index, boardAddr, ref inOutPdu);
			if (0 != ret)
			{
				return false;
			}

			string csCmdValueTemp = index;
			for (int i = 0; i < mibNames.Count; i++)
			{
				var mibItem = mibNames.ElementAt(i);
				if (inOutPdu.GetValueByMibName(boardAddr, mibItem.Key, out csCmdValueTemp, index))
				{
					mibNames[mibItem.Key] = csCmdValueTemp;
				}
			}

			return true;
		}

		// oid列表转换为vb列表。oid无前缀；失败返回null
		public static List<CDTLmtbVb> ConvertOidToVb(List<string> oidList, string oidPostFix)
		{
			if (null == oidList)
			{
				return null;
			}

			var prefix = GetMibPrefix();
			var postfix = "";
			if (null != oidPostFix)
			{
				postfix = oidPostFix.Trim('.'); //删除oidPostFix的开始处的.
			}
			var vbList = new List<CDTLmtbVb>();
			foreach (var oid in oidList)
			{
				var soid = oid.Trim('.');   // 不知道oid前后是否带有.，此处使用保守的方式，先尝试删掉，后面再追加
				var fullOid = $"{prefix}{soid}";
				if (!string.IsNullOrEmpty(postfix) && !string.IsNullOrWhiteSpace(postfix))
				{
					fullOid = $"{fullOid}.{postfix}";
				}

				var vb = new CDTLmtbVb { Oid = fullOid };
				vbList.Add(vb);
			}

			return vbList;
		}

		// 把节点名列表转换为节点名对应的MIB列表
		public static IEnumerable<MibLeaf> ConvertNameListToMibInfoList(List<string> leafNameList, string ip)
		{
			if (null == leafNameList || string.IsNullOrEmpty(ip))
			{
				throw new CustomException("传入参数错误");
			}

			return leafNameList.Select(name => GetMibNodeInfoByName(name, ip)).ToList();
		}

		// 把节点名列表转换为oid列表。需要连接到数据模块查找
		public static List<string> ConvertNameToOid(List<string> nameList, string ip)
		{
			if (null == nameList || string.IsNullOrEmpty(ip))
			{
				throw new CustomException("传入参数错误");
			}

			var olidList = new List<string>();

			foreach (var name in nameList)
			{
				MibLeaf temp;
				var errorMsg = "";
				if (!Database.GetInstance().getDataByEnglishName(name, out temp, ip, out errorMsg))
				{
					throw new CustomException($"待查找的节点名{name}不存在，请确认MIB版本后重试");
				}

				olidList.Add(temp.childOid);
			}

			return olidList;
		}

		// 转换节点名列表为vb列表，针对每一个oid都有一个value值
		// nameList每个元素是oid，且不带前缀，name2value的key是节点名，不是oid
		// 需要把节点名转换为oid再进行转换
		public static List<CDTLmtbVb> ConvertNameToVbList(List<string> nameList, string ip, string index,
			bool bCheck, Dictionary<string, string> name2value)
		{
			if (null == nameList || string.IsNullOrEmpty(ip))
			{
				throw new CustomException("传入参数错误");
			}

			//获取name2value 的 key 信息，即 EnglishName，再查询 mib 节点信息
			var reData = new Dictionary<string, MibLeaf>();
			foreach (var item in name2value)
			{
				reData[item.Key] = null;
			}

			string errorInfo = "";
			if (!Database.GetInstance().getDataByEnglishName(reData, ip, out errorInfo))
			{
				throw new CustomException("查询信息失败");
			}

			//将 OId 和 EnglishName 进行 一 一 对应
			var relation = new Dictionary<string, string>();
			foreach (var item in reData)
			{
				relation[item.Value.childOid] = item.Key;
			}

			//IReDataByEnglishName nameToOid = new ReDataByEnglishName();
			var vbList = new List<CDTLmtbVb>();

			var prefix = GetMibPrefix().Trim('.');
			var postfix = index.Trim('.');    //删除oidPostFix的前后的.

			// 在此处校验索引是否有效
			if (bCheck)
			{
				throw new NotImplementedException("校验索引有效性功能尚未实现");
			}

			foreach (var name in nameList)
			{
				//if (!Database.GetInstance().getDataByEnglishName(name, out nameToOid, ip, out errorInfo))
				//{
				//	throw new CustomException($"待查找的节点名{name}不存在，请确认MIB版本后重试");
				//}
				
				if (!relation.ContainsKey(name))
				{
					Log.Debug($"没有设置{name}的值，跳过");
					continue;
				}

				var mibNode = reData[relation[name]];

				var vb = new CDTLmtbVb
				{
					Oid = $"{prefix}.{name.Trim('.')}.{postfix}",
					Value = name2value[ relation[name] ],
					SnmpSyntax = ConvertDataType(mibNode.mibSyntax)
				};

				vbList.Add(vb);
			}

			return vbList;
		}

		/// <summary>
		/// 根据mibname获取该项对应的取值范围，并分割
		/// </summary>
		/// <param name="mibName">mib的叶子节点</param>
		/// <param name="targetIp">目标IP</param>
		/// <returns>null:查询该MIB信息失败</returns>
		public static Dictionary<int, string> GetValueRangeByMibName(string mibName, string targetIp)
		{
			string error;
			var retData = new Dictionary<string, MibLeaf> { [mibName] = null };
			if (!Database.GetInstance().getDataByEnglishName(retData, targetIp, out error))
			{
				return null;
			}

			string mvr = retData[mibName].managerValueRange;
			if (string.IsNullOrWhiteSpace(mvr)) return null;

			return MibStringHelper.SplitManageValue(mvr);
		}

		// 根据mib名称获取节点信息
		public static MibLeaf GetMibNodeInfoByName(string mibName, string targetIp)
		{
			var mapName2Data = new Dictionary<string, MibLeaf> { [mibName] = null };
			string errorInfo;

			if (Database.GetInstance().getDataByEnglishName(mapName2Data, targetIp, out errorInfo))
			{
				return mapName2Data[mibName];
			}

			return null;
		}

		// 根据传入的行状态值，获取描述字符串
		public static string GetRowStatusText(string rowStatus)
		{
			var rsText = "";

			switch (rowStatus)
			{
				case "1":
					rsText = "使用中";
					break;
				case "2":
					rsText = "退服";
					break;
				case "3":
					rsText = "未准备好";
					break;
				case "4":
					rsText = "创建并运行";
					break;
				case "5":
					rsText = "创建并等待";
					break;
				case "6":
					rsText = "删除";
					break;
			}

			return rsText;
		}

		#endregion

		#region 私有方法

		// 把字符串的数据类型转换为snmp能够直接使用的数据类型
		private static SNMP_SYNTAX_TYPE ConvertDataType(string syntaxString)
		{
			if (string.IsNullOrEmpty(syntaxString))
			{
				throw new CustomException("文本数据类型不能是null或者empty");
			}

			SNMP_SYNTAX_TYPE type;
			switch (syntaxString)
			{
				case "LONG":
				case "INETADDRESSTYPE":
				case "ROWSTATUS":
				case "BOOL":
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32;
					break;
				case "UINT32":
				case "BITS":
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_UINT32;
					break;
				case "NULL":
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NULL;
					break;
				case "OID":
				case "VARIABLEPOINTER":
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID;
					break;
				case "IPADDR":
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_IPADDR;
					break;
				case "TIMETICKS":
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_TIMETICKS;
					break;
				case "DATETIME":
				case "INETADDRESS":
				case "DATEANDTIME":
				case "OCTET STRING":
				case "MACADDRESS":
				case "UNSIGNED32ARRAY":
				case "INTEGER32ARRAY":
				case "MNCMCCTYPE":
				case "OCTETS":
				case "PHYADDR":
				case "TEMP_ID":
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS;
					break;
				default:
					type = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INIT;
					break;
			}

			return type;
		}

		#endregion

		#region 自定义的宏

		private static uint DATEANDTIMELENV2TC = 11;    /*SNMPV2 TC中定义的时间长度*/
		private static uint DATEANDTIMEDTCUSTOM = 19;   /*公司自定义的时间长度*/

		#endregion
	}
}
