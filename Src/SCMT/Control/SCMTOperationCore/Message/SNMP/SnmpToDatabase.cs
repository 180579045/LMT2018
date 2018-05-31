using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SnmpSharpNet;

// snmp 到 database 的所需接口封装
namespace SCMTOperationCore.Message.SNMP
{
	public class SnmpToDatabase
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
	    public static IReCmdDataByCmdEnglishName GetCmdInfoByCmdName(string cmdName, string ip)
	    {
			throw new NotImplementedException();
	    }

		// 获取公共MIB前缀。最后带.字符
		public static string GetMibPrefix()
		{
			return "1.3.6.1.4.1.5105.";
		}

		// oid列表转换为vb列表。oid无前缀；失败返回null
		public static List<CDTLmtbVb> ConvertOidToVb(List<string> oidList, string oidPostFix)
		{
			if (null == oidList)
			{
				return null;
			}

			var prefix = GetMibPrefix();
			var postfix = oidPostFix.TrimStart('.');	//删除oidPostFix的开始处的.
			var vbList = new List<CDTLmtbVb>();
			foreach (var oid in oidList)
			{
				var soid = oid.Trim('.');	// 不知道oid前后是否带有.，此处使用保守的方式，先尝试删掉，后面再追加
				var vb = new CDTLmtbVb {Oid = $"{prefix}{soid}.{oidPostFix}"};
				vbList.Add(vb);
			}

			return vbList;
		}

		

		// 把节点名列表转换为oid列表。需要连接到数据模块查找
		public static List<string> ConvertNameToOid(List<string> nameList, string ip)
		{
			if (null == nameList || string.IsNullOrEmpty(ip))
			{
				throw new CustomException("传入参数错误");
			}

			var olidList = new List<string>();
			IReDataByEnglishName temp = new ReDataByEnglishName();

			foreach (var name in nameList)
			{
				if (!Database.GetInstance().getDataByEnglishName(name, out temp, ip))
				{
					throw new CustomException("待查找的节点名{name}不存在，请确认MIB版本后重试");
				}

				olidList.Add(temp.oid);
			}

			return olidList;
		}

		// 转换节点名列表为vb列表，针对每一个oid都有一个value值
		public static List<CDTLmtbVb> ConvertNameToVbList(List<string> nameList, string ip, string index,
			bool bCheck, Dictionary<string, string> name2value)
		{
			if (null == nameList || string.IsNullOrEmpty(ip))
			{
				throw new CustomException("传入参数错误");
			}

			IReDataByEnglishName nameToOid = new ReDataByEnglishName();
			var vbList = new List<CDTLmtbVb>();

			var prefix = GetMibPrefix().Trim('.');
			var postfix = index.Trim('.');    //删除oidPostFix的开始处的.

			// 在此处校验索引是否有效
			if (bCheck)
			{
				throw new NotImplementedException("校验索引有效性功能尚未实现");
			}

			foreach (var name in nameList)
			{
				if (!Database.GetInstance().getDataByEnglishName(name, out nameToOid, ip))
				{
					throw new CustomException($"待查找的节点名{name}不存在，请确认MIB版本后重试");
				}

				if (!name2value.ContainsKey(name))
				{
					Log.Debug($"没有设置{name}的值，跳过");
					continue;
				}

				var vb = new CDTLmtbVb
				{
					Oid = $"{prefix}.{nameToOid.oid.Trim('.')}.{postfix}",
					Value = name2value[name],
					SnmpSyntax = ConvertDataType(nameToOid.syntax)
				};

				vbList.Add(vb);
			}

			return vbList;
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
