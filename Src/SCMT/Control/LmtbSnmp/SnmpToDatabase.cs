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
namespace LmtbSnmp
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
		public static IEnumerable<MibLeaf> ConvertOidListToMibInfoList(List<string> leafOidList, string ip)
		{
			if (null == leafOidList || string.IsNullOrEmpty(ip))
			{
				throw new CustomException("传入参数错误");
			}

			return leafOidList.Select(oid => GetMibNodeInfoByOid(oid, ip)).Where(mibnode => null != mibnode).ToList();
		}

		// 根据oid获取MIB信息
		public static MibLeaf GetMibNodeInfoByOid(string oid, string ip)
		{
			if (string.IsNullOrEmpty(oid) || string.IsNullOrEmpty(ip))
			{
				return null;
			}

			MibLeaf mibLeaf = null;
			string errInfo;
			if (Database.GetInstance().GetMibDataByOid(oid, out mibLeaf, ip, out errInfo))
			{
				return mibLeaf;
			}
			return null;
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
				var temp = GetMibNodeInfoByName(name, ip);
				if (null == temp)
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
		public static List<CDTLmtbVb> ConvertOidListToVbList(List<string> nameList, string ip, string index,
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
			var mibLeaf = GetMibNodeInfoByName(mibName, targetIp);
			if (null == mibLeaf)
			{
				return null;
			}

			string mvr = mibLeaf.managerValueRange;
			if (string.IsNullOrWhiteSpace(mvr)) return null;

			return MibStringHelper.SplitManageValue(mvr);
		}

		/// <summary>
		/// 给定mibName，从数据库中找到该节点名对应的数据类型，把strValue转换为相关的格式
		/// 1.如果是DateAndTime，就转换为可读的时间字符串
		/// 2.如果是枚举值，就返回strValue对应的取值
		/// 3.
		/// </summary>
		/// <param name="mibName">MIB节点名</param>
		/// <param name="strValue">从Snmp协议中获取的具体数值</param>
		/// <param name="targetIp">目标IP</param>
		/// <returns></returns>
		public static dynamic ConvertSnmpValueToString(string mibName, string strValue, string targetIp)
		{
			if (string.IsNullOrEmpty(mibName) || string.IsNullOrEmpty(targetIp) || string.IsNullOrEmpty(strValue))
			{
				return null;
			}

			var retData = GetMibNodeInfoByName(mibName, targetIp);

			if (null == retData)
			{
				return null;
			}

			if (retData.IsTable)	// 给定的Mib名是表入口
			{
				return null;
			}

			// TODO 莫忘记取值范围校验
			return ConvertValueToString(retData, strValue);
		}

		/// <summary>
		/// 将Snmp协议获取的数值转为枚举类型模型;
		/// </summary>
		/// <param name="mibName">MIB节点名</param>
		/// <param name="strValue">从Snmp协议中获取的具体数值</param>
		/// <param name="targetIp">目标IP</param>
		/// <returns></returns>
		public static Dictionary<int, string> ConvertSnmpValueToEnumContent(string mibName, string targetIp)
		{
			if (string.IsNullOrEmpty(mibName) || string.IsNullOrEmpty(targetIp))
			{
				return null;
			}

			var retData = GetMibNodeInfoByName(mibName, targetIp);
			
			var mvr = retData.managerValueRange;                    // 1.取出该节点的取值范围;
			var mapKv = MibStringHelper.SplitManageValue(mvr);      // 2.分解取值范围;
			
			return mapKv;
		}

		/// <summary>
		/// 将从SNMP协议读取到的数值转换为string类型;
		/// </summary>
		/// <param name="mibLeaf">Mib节点</param>
		/// <param name="strValue">Snmp返回的具体数值</param>
		/// <returns></returns>
		public static dynamic ConvertValueToString(MibLeaf mibLeaf, string strValue)
		{
			var omType = mibLeaf.OMType;
			var asnType = mibLeaf.ASNType;

			if (omType.Equals("u32") || omType.Equals("s32"))
			{
				if (asnType.Equals("bits", StringComparison.OrdinalIgnoreCase))
				{
					// BITS类型的，需要转换
					return ConvertBitsToString(mibLeaf.managerValueRange, strValue);
				}
			}
			else if (omType.Equals("u32[]"))
			{

			}
			else if (omType.Equals("s32[]"))
			{
				// oid类型
				if (asnType.Equals("notification-type", StringComparison.OrdinalIgnoreCase) ||
					asnType.Equals("OBJECT IDENTIFIER", StringComparison.OrdinalIgnoreCase))
				{

				}
			}
			else if (omType.Equals("enum"))
			{
				// 1.取出该节点的取值范围
				var mvr = mibLeaf.managerValueRange;

				// 2.分解取值范围
				var mapKv = MibStringHelper.SplitManageValue(mvr);
				var value = int.Parse(strValue);

				// 3.比对是否存在对应的枚举值
				return mapKv.ContainsKey(value) ? mapKv[value] : null;
			}
			//else if (omType.Equals("s8[]"))
			//{

			//}
			else if (omType.Equals("u8[]"))
			{
				if (asnType.Equals("DateAndTime"))
				{
					return TimeHelper.ConvertUtcTimeTextToDateTime(strValue).ConvertTimeZoneToLocal().DateTimeToStringEx();
				}
				else if (asnType.Equals("InetAddress"))
				{
					return ConvertInetToString(strValue);
				}
				else if (asnType.Equals("MacAddress"))
				{
					return ConvertMacAddrToString(strValue);
				}
				else if (asnType.Equals("MncMccType"))
				{

				}
			}
			//else if (omType.Equals("u16") || omType.Equals("s16"))
			//{
			//	return strValue;
			//}
			//else
			//{
			//	return strValue;
			//}
			return strValue;
		}

		public static dynamic ConvertStringToMibValue(MibLeaf mibLeaf, string strValue)
		{
			var omType = mibLeaf.OMType;
			var asnType = mibLeaf.ASNType;

			if (omType.Equals("u32") || omType.Equals("s32"))
			{
				if (asnType.Equals("bits", StringComparison.OrdinalIgnoreCase))
				{
					// BITS类型的，需要转换
					//return ConvertBitsToString(mibLeaf.managerValueRange, strValue);
					// TODO 反转StringToBits。与CommSnmpFuncs文件中的相关函数合并
				}
			}
			else if (omType.Equals("u32[]"))
			{

			}
			else if (omType.Equals("s32[]"))
			{
				// oid类型
				if (asnType.Equals("notification-type", StringComparison.OrdinalIgnoreCase) ||
					asnType.Equals("OBJECT IDENTIFIER", StringComparison.OrdinalIgnoreCase))
				{

				}
			}
			else if (omType.Equals("enum"))
			{
				// 1.取出该节点的取值范围
				var mvr = mibLeaf.managerValueRange;

				// 2.分解取值范围
				var mapKv = MibStringHelper.SplitManageValue(mvr);

				foreach (var kv in mapKv)
				{
					if (kv.Value == strValue)
					{
						return kv.Key.ToString();
					}
				}
			}
			//else if (omType.Equals("s8[]"))
			//{

			//}
			else if (omType.Equals("u8[]"))
			{
				if (asnType.Equals("DateAndTime"))
				{
					
				}
				else if (asnType.Equals("InetAddress"))
				{
					
				}
				else if (asnType.Equals("MacAddress"))
				{
					
				}
				else if (asnType.Equals("MncMccType"))
				{

				}
			}
			//else if (omType.Equals("u16") || omType.Equals("s16"))
			//{
			//	return strValue;
			//}
			//else
			//{
			//	return strValue;
			//}
			return strValue;
		}


		/// <summary>
		/// 转换BITS类型的数据为描述信息
		/// </summary>
		/// <param name="strValueRange">MIB中的取值范围</param>
		/// <param name="strValue">实际取值</param>
		/// <returns></returns>
		public static dynamic ConvertBitsToString(string strValueRange, string strValue)
		{
			if (string.IsNullOrEmpty(strValueRange) || string.IsNullOrEmpty(strValue))
			{
				return null;
			}

			// 1.分隔取值范围
			var mapKv = MibStringHelper.SplitManageValue(strValueRange);

			// 2.TODO 简单处理，实际取值只有一个
			var value = int.Parse(strValue);

			// 3.返回对应的描述信息
			return mapKv.ContainsKey(value) ? mapKv[value] : null;
		}

		/// <summary>
		/// 转换IP地址到string
		/// 在本函数内部处理了IPV4和IPV6
		/// </summary>
		/// <param name="strInetAddr"></param>
		/// <returns></returns>
		public static dynamic ConvertInetToString(string strInetAddr)
		{
			if (string.IsNullOrEmpty(strInetAddr))
			{
				return null;
			}

			// 剔除字符串中的空格
			var strTemp = strInetAddr.Replace(" ", "");

			// 根据字符串的长度判断是ipv4还是ipv6
			var len = strTemp.Length;
			if (len == 8)
			{
				// ipv4地址字符串的处理
				var ipv4 = TimeHelper.StrHex2Bytes(strTemp);
				return $"{ipv4[0]}.{ipv4[1]}.{ipv4[2]}.{ipv4[3]}";
			}
			else if (len == 32)
			{
				string ipv6 = "";
				// ipv6地址字符串的处理
				for (var i = 0; i < len;)
				{
					ipv6 = $"{ipv6}:{strTemp.Substring(i, 4)}";
					i += 4;
				}
				return ipv6.TrimStart(':');
			}
			return strInetAddr;
		}

		public static dynamic ConvertMacAddrToString(string strMac)
		{
			if (string.IsNullOrEmpty(strMac))
			{
				return null;
			}

			return strMac.Replace(" ", ":").ToUpper();
		}

		/// <summary>
		/// 获取Mib节点的数据类型，以便在前端区分显示;
		/// 一共分为四种类型，分别是：字符类型、enum枚举类型、Bit类型、时间类型;
		/// </summary>
		/// <param name="mibName">Mib节点名称</param>
		/// <param name="targetIp">目标IP</param>
		/// <returns></returns>
		public static DataGrid_CellDataType GetMibNodeDataType(string mibName, string targetIp)
		{
			if (string.IsNullOrEmpty(mibName) || string.IsNullOrEmpty(targetIp))
			{
				return DataGrid_CellDataType.RegularType;
			}

			var retData = GetMibNodeInfoByName(mibName, targetIp);
			if (null == retData)
			{
				Log.Error($"查询基站{targetIp}的{mibName}信息失败");
				return DataGrid_CellDataType.invalidType;
			}

			if (retData.IsTable)
			{
				return DataGrid_CellDataType.RegularType;
			}

			var omType = retData.OMType;
			var asnType = retData.ASNType;

			// 字符类型;
			if(asnType.Equals("DisplayString"))
			{
				return DataGrid_CellDataType.RegularType;
			}
			// 比特类型;
			else if (omType.Equals("u32") && asnType.Equals("BITS"))
			{
				return DataGrid_CellDataType.bitType;
			}
			// 对数字有取值范围的枚举类型;
			else if (omType.Equals("u32[]") || omType.Equals("s32[]"))
			{
				return DataGrid_CellDataType.enumType;
			}
			// 枚举类型;
			else if (omType.Equals("enum"))
			{
				return DataGrid_CellDataType.enumType;
			}
			// 时间类型;
			else if(asnType.Equals("DateAndTime"))
			{
				return DataGrid_CellDataType.DateTime;
			}
            
//             else if (omType.Equals("u8[]"))
//             {
//                 if (asnType.Equals("DateAndTime"))
//                 {
// 
//                 }
//                 else if (asnType.Equals("InetAddress"))
//                 {
// 
//                 }
//                 else if (asnType.Equals("MacAddress"))
//                 {
// 
//                 }
//                 else if (asnType.Equals("MncMccType"))
//                 {
// 
//                 }
//             }

			return DataGrid_CellDataType.RegularType;
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

	/// <summary>
	/// 单元格内可显示的所有数据类型;
	/// </summary>
	public enum DataGrid_CellDataType
	{
		invalidType = -1,
		enumType = 0,                                  // MIB中的枚举类型;
		bitType = 1,                                   // MIB中的BIT类型;
		DateTime = 2,                                  // MIB中的时间类型;
		RegularType = 3                                // MIB中的字符串、INT等类型;
	}
}
