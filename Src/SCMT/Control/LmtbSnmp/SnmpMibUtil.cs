
/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ SnmpMibUtil $
* 机器名称：       $ machinename $
* 命名空间：       $ LmtbSnmp $
* 文 件 名：       $ CDTCmdExecuteMgr.cs $
* 创建时间：       $ 2018.11.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     SNMP及Mib间类型转换工具类。
* 修改时间     修 改 人         修改内容：
* 2018.10.xx  XXXX            XXXXX
*************************************************************************************/
using CommonUtility;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LmtbSnmp
{
	public class SnmpMibUtil
	{
		/// <summary>
		/// 将SNMP返回的节点值，根据Vb类型及Mib定义的类型转换为可显示字符串
		/// 如： 日期类型->"2018-11-3 10:37:10"；  IP->"192.168.5.1"； 数字->"111"
		/// </summary>
		/// <param name="strIpAddr">基站IP</param>
		/// <param name="vb">SNMP命令返回的VB</param>
		/// <param name="strValue">转换后的可显示文本</param>
		/// <returns></returns>
		public static bool ConvertSnmpVal2MibStr(string strIpAddr, Vb vb, out string strValue)
		{
			strValue = null;
			string strValTmp = null;
			// Mib中的节点类型
			string strNodeType = null;

			if (string.IsNullOrEmpty(strIpAddr))
			{
				return false;
			}
			
			// 值转换
			switch (vb.Value.Type)
			{
				// DateandTime、inetaddress等在SNMP中都使用OctetString表示的，要特殊处理,把内存值转换为显示文本
				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS:
					// 获取Mib中的节点类型
					strNodeType = GetNodeTypeByOIDInCache(strIpAddr, vb.Oid.ToString());
					//strNodeType = "InetAddress";
					//strNodeType = "DateAndTime";
					//strNodeType = "Unsigned32Array";
					//strNodeType = "MacAddress";

					if (string.IsNullOrEmpty(strNodeType))
					{
						// TODO 获取不到Mib节点类型要不要返回？
					}

					if (string.Equals("DateandTime", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValTmp = SnmpDateTime2String((OctetString)vb.Value);
					}
					else if (string.Equals("InetAddress", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						var ipAddr = new IpAddress((OctetString)vb.Value);
						strValTmp = ipAddr.ToString();
					}
					else if (string.Equals("MacAddress", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValTmp = ((OctetString)vb.Value).ToMACAddressString();
					}
					else if (string.Equals("Unsigned32Array", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValTmp = OctetStrToU32Array((OctetString)vb.Value);
					}
					else if (string.Equals("Integer32Array", strNodeType, StringComparison.OrdinalIgnoreCase)
						|| "".Equals(strNodeType))
					{
						strValTmp = OctetStrToS32Array((OctetString)vb.Value);
					}
					else if (string.Equals("MncMccType", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValTmp = OctetStr2MncMccTypeStr((OctetString)vb.Value);
					}
					else
					{
						strValTmp = vb.Value.ToString();
						strValTmp = Utf8ToAnsi(strValTmp);
					}
					break;

				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_BITS:
					strValTmp = vb.Value.ToString();
					strValTmp = Utf8ToAnsi(strValTmp);
					break;

				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT:
					if (string.Equals("null", vb.Value.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						strValTmp = "0";
					}
					else
					{
						strValTmp = vb.Value.ToString();
					}
					break;

				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NULL: //状态码不为0时，vb.Value.Type为NULL
					// 获取Mib中的节点类型
					strNodeType = GetNodeTypeByOIDInCache(strIpAddr, vb.Oid.ToString());
					if (!string.IsNullOrEmpty(strNodeType))
					{
						if (string.Equals("INTEGER", strNodeType, StringComparison.OrdinalIgnoreCase)
							|| string.Equals("LONG", strNodeType, StringComparison.OrdinalIgnoreCase))
						{
							strValTmp = "0"; // 整型为NULL时取0

						}
						else
						{
							strValTmp = vb.Value.ToString();
						}
					}
					else {
						strValTmp = vb.Value.ToString();
					}
					break;
				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_IPADDR:
				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_TIMETICKS:
				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_CNTR64:
					strValTmp = vb.Value.ToString();
					break;

				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_CNTR32:
				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_GAUGE32:
				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OPAQUE:
					strValTmp = vb.Value.ToString();
					break;

				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NOSUCHOBJECT:
					strValTmp = "没有这个对象";
					break;

				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NOSUCHINSTANCE:
					strValTmp = "没有这个实例";
					break;

				case (byte)SNMP_SYNTAX_TYPE.SNMP_SYNTAX_ENDOFMIBVIEW:
					strValTmp = "到达MIB结尾";
					break;

				default:
					strValTmp = vb.Value.ToString();
					break;
			}

			strValue = strValTmp;

			return true;
		}

		/// <summary>
		/// 组装SNMP协议中的VB数据，将String类型的value值根据SYNTAX类型转化为SNMP协议中的VB Value值
		/// </summary>
		/// <param name="vb">SNMP VB</param>
		/// <param name="syntaxType">LmtPdu中的节点类型</param>
		/// <param name="value">节点值</param>
		/// <returns></returns>
		public static int SetVbValue(ref Vb vb, SNMP_SYNTAX_TYPE syntaxType, string value, string strDataType = "")
		{
			if (vb == null)
			{
				return -1;
			}

			try
			{
				switch (syntaxType)
				{
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT:
						vb.Value = new Integer32(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_BITS:
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS:
						if (value == null)
						{
							// TODO null的处理这样好像不对？？？？
							vb.Value = new OctetString("null"); 
						}
						else
						{
							vb.Value = PacketOctetStr(vb.Oid.ToString(), value, strDataType);
						}
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NULL:
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID:
						// TODO:
						vb.Value = new Oid(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_IPADDR:
						vb.Value = new IpAddress(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_CNTR32:
						vb.Value = new Counter32(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_GAUGE32:
						vb.Value = new Gauge32(value); // new UInteger32(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_TIMETICKS:
						vb.Value = new TimeTicks(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OPAQUE:
						vb.Value = new Opaque(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_CNTR64:
						// TODO 是否需要大小端转换？？？？
						vb.Value = new Counter64(value);
						break;

					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NOSUCHOBJECT:
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NOSUCHINSTANCE:
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_ENDOFMIBVIEW:
						// do nothing
						break;

					default:
						// do nothing
						break;
				}
			}
			catch (Exception e)
			{
				Log.Error($"{e.Message} syntax type:{syntaxType.ToString()} value:{value} vb info:{vb}");
				throw;
			}

			return 0;
		}

		/// <summary>
		/// 根据oid、值、数据类型组装OctetString实例
		/// </summary>
		/// <param name="strOid"></param>
		/// <param name="strValue"></param>
		/// <param name="strDataTyep"></param>
		/// <returns></returns>
		public static OctetString PacketOctetStr(string strOid, string strValue, string strDataType)
		{
			if (string.Equals("DateandTime", strDataType, StringComparison.OrdinalIgnoreCase)) // 日期类型
			{
				byte[] buf = new byte[11];
				byte[] buffer = SnmpStrDateTime2Bytes(strValue);
				for (int i = 0; i < 8; i++)
				{
					buf[i] = buffer[i];
				}
				buf[8] = (byte)'+';
				buf[9] = 0;
				buf[10] = 0;
				return new OctetString(buf);
			}
			else if (string.Equals("InetAddress", strDataType, StringComparison.OrdinalIgnoreCase)) // Ip地址
			{
				//if (strOid.IndexOf("1.3.6.1.4.1.5105.100.1.10.1.1.1.10") >= 0)
				//{
					// TODO Mib中此oid类型并不是IP
					
				//}
				//else
				//{
					return new OctetString(SnmpStrIpAddr2Bytes(strValue));
				//}
			}
			else if (string.Equals("MacAddress", strDataType, StringComparison.OrdinalIgnoreCase)) // MAC地址
			{
				return new OctetString(StrMacAddr2Bytes(strValue));
			}
			else if (string.Equals("Unsigned32Array", strDataType, StringComparison.OrdinalIgnoreCase)
				|| string.Equals("Integer32Array", strDataType, StringComparison.OrdinalIgnoreCase))
			{
				return new OctetString(Unsigned32Array2Bytes(strValue));
			}
			else if (string.Equals("MncMccType", strDataType, StringComparison.OrdinalIgnoreCase))
			{
				// TODO (与原来逻辑不一样，不知道是否正确？？？？)
				return new OctetString(MncMccType2Bytes(strValue));
			}
			else
			{
				// TODO 转换为UTF8编码(与原来逻辑不一样，不知道是否正确？？？？)
				return new OctetString(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(strValue)));

			}

		}


		/// <summary>
		/// 将IP地址转换为byte数组
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <returns></returns>
		public static byte[] SnmpStrIpAddr2Bytes(string strIpAddr)
		{
			byte[] bytes = new byte[4];
			if (!string.IsNullOrEmpty(strIpAddr))
			{
				IpAddress ipAddr = new IpAddress(strIpAddr);
				bytes = BitConverter.GetBytes(ipAddr.ToUInt32()); //ipAddr.ToArray();
			}

			return bytes;
		}

		/// <summary>
		/// Mac地址字符串转换为byte数组
		/// </summary>
		/// <param name="strMacAddr"></param>
		/// <returns></returns>
		public static byte[] StrMacAddr2Bytes(string strMacAddr)
		{
			return StrHex2Bytes(strMacAddr);
		}

		/// <summary>
		/// 十六进制字串转换为bytes数组
		/// </summary>
		/// <param name="strHex"></param>
		/// <returns></returns>
		public static byte[] StrHex2Bytes(string strHex)
		{
			if (string.IsNullOrEmpty(strHex))
			{
				return null;
			}
			strHex = strHex.Replace(":", "");
			strHex = strHex.Replace(" ", "");

			int length = strHex.Length;
			byte[] bytes = new byte[length / 2];

			for (int i = 0; i < length; i += 2)
			{
				bytes[i / 2] = Convert.ToByte(strHex.Substring(i, 2), 16);
			}

			return bytes;
		}



		/// <summary>
		/// MncMccType类型转换为byte数组
		/// </summary>
		/// <param name="strMncMccVal"></param>
		/// <returns></returns>
		public static byte[] MncMccType2Bytes(string strMncMccVal)
		{
			// TODO: 原来方法要减48，目前没减确定对不对？？？
			return System.Text.Encoding.ASCII.GetBytes(strMncMccVal);
		}

		/// <summary>
		/// 将OctetString转换为MncMccType字符串
		/// </summary>
		/// <param name="octStr"></param>
		/// <returns></returns>
		public static string OctetStr2MncMccTypeStr(OctetString octStr)
		{
			if (null == octStr || octStr.Length <= 0)
			{
				return "";
			}

			StringBuilder sb = new StringBuilder();
			byte[] bytes = octStr.ToArray();

			foreach (byte bt in bytes)
			{
				if (bt == 255)
				{
					// 无效字段不需要记录
					break;
				}

				sb.Append((int)bt);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Ansi编码字符串转换为Utf8编码字符串
		/// </summary>
		/// <param name="strAnsi"></param>
		/// <returns></returns>
		public static string AnsiToUtf8(string strAnsi)
		{
			Encoding utf8 = Encoding.UTF8;
			Encoding ansi = Encoding.Default;

			byte[] bytes = ansi.GetBytes(strAnsi);
			bytes = Encoding.Convert(ansi, utf8, bytes);

			return utf8.GetString(bytes);
		}


		/// <summary>
		/// 将SNMP的DateTime类型转换为字符串
		/// 如： 07:d9:01:01:00:00:23:00:00:00:00->"2009-01-01 00:00:35"
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static string SnmpDateTime2String(OctetString v)
		{
			// 返回值
			string strRes = "";

			if (11 == v.Length) // SNMPv2中定义的DateandTime
			{
				byte[] bts = v.ToArray();

				//获取时间;
				int year = bts[0] * 256 + bts[1]; // 前两个字节为年份
				int month = bts[2];  // 月
				int day = bts[3];    // 日
				int hour = bts[4];   // 时
				int minute = bts[5]; // 分
				int second = bts[6]; // 秒
									 //int msecond = bts[7];
				year = year > 9999 || year == 0 ? 2009 : year;
				month = (byte)(month > 12 || month == 0 ? 1 : month);
				day = (byte)(day > 31 || day == 0 ? 1 : day);
				hour = (byte)(hour > 24 ? 0 : hour);
				minute = (byte)(minute > 59 ? 0 : minute);
				second = (byte)(second > 59 ? 0 : second);

				DateTime dt = new DateTime(year, month, day, hour, minute, second);
				strRes = dt.ToString("yyyy-MM-dd HH:mm:ss");
			}
			else if (v.Length == 19) // 公司自定义的时间长度
			{
				// TODO 代验证
				strRes = Utf8ToAnsi(v.ToString());
			}
			else
			{
				strRes = "";
			}

			return strRes;
		}


		/// <summary>
		/// 从Octet String类型转为S32数组的字符串表示
		/// 如: 00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00 -> "{0,0,0,0,0}"
		/// </summary>
		/// <param name="octStr"></param>
		/// <returns></returns>
		public static string OctetStrToS32Array(OctetString octStr)
		{
			// 数字长度
			int valCount = octStr.Length / sizeof(int);
			if (valCount <= 0)
			{
				return "";
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("{");

			byte[] bytes = octStr.ToArray();

			// 每4个字节转换为一个Int32数字
			for (int i = 0; i < valCount;)
			{
				byte[] bsTmp = new byte[sizeof(int)];
				Buffer.BlockCopy(bsTmp, 0, bytes, i * 4, sizeof(int));
				// 大小端转换
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bsTmp);
				}

				int intTmp = BitConverter.ToInt32(bsTmp, 0);
				sb.Append(intTmp);

				i++;
				if (i < valCount)
				{
					sb.Append(",");
				}
			}

			sb.Append("}");

			return sb.ToString();
		}

		/// <summary>
		/// 从Octet String类型转为U32数组的字符串表示
		/// 如: 00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00 -> "{0,0,0,0,0}"
		/// </summary>
		/// <param name="octStr"></param>
		/// <returns></returns>
		public static string OctetStrToU32Array(OctetString octStr)
		{
			// 数组个数
			int valCount = octStr.Length / sizeof(uint);
			if (valCount <= 0)
			{
				return "";
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("{");

			byte[] bytes = octStr.ToArray();

			// 每四个字节转换为一个UInt32数字
			for (int i = 0; i < valCount;)
			{
				// 拷贝4个字节，转换为整数
				byte[] bsTmp = new byte[sizeof(uint)];
				Buffer.BlockCopy(bsTmp, 0, bytes, i * 4, sizeof(uint));
				// 大小端转换
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bsTmp);
				}

				uint intTmp = BitConverter.ToUInt32(bsTmp, 0);
				sb.Append(intTmp);

				i++;
				if (i < valCount)
				{
					sb.Append(",");
				}
			}

			sb.Append("}");

			return sb.ToString();
		}

		/// <summary>
		/// 将Unsigned32Array转换为byte数组
		/// </summary>
		/// <param name="strUnsigned32"></param>
		/// <returns></returns>
		public static byte[] Unsigned32Array2Bytes(string strUnsigned32)
		{
			string strTmp = strUnsigned32;
			// 删除左右大括号
			strTmp = strTmp.Replace("{", "");
			strTmp = strTmp.Replace("}", "");
			// 获取数值
			string[] strValArray = strTmp.Split(',');

			// 返回值
			byte[] bytes = new byte[strValArray.Length * 4];

			// 将数值转换为byte数组
			int index = 0;
			foreach (string strVal in strValArray)
			{
				if (string.IsNullOrEmpty(strVal))
				{
					break;
				}

				uint intVal = uint.Parse(strVal);
				byte[] bytesTmp = BitConverter.GetBytes(intVal);
				if (BitConverter.IsLittleEndian) // 转换为网络序
				{
					Array.Reverse(bytesTmp);
				}
				Buffer.BlockCopy(bytesTmp, 0, bytes, index * 4, sizeof(uint));
				index++;
			}

			return bytes;
		}

		/// <summary>
		/// UTF8字符串转ANSI编码字符串
		/// </summary>
		/// <param name="strUtf8"></param>
		/// <returns></returns>
		public static string Utf8ToAnsi(string strUtf8)
		{
			Encoding utf8 = Encoding.UTF8;
			Encoding ansi = Encoding.Default;

			byte[] bytes = utf8.GetBytes(strUtf8);
			bytes = Encoding.Convert(utf8, ansi, bytes);

			return ansi.GetString(bytes);
		}

		/// <summary>
		/// 将日期字符串转换为byte数组，并转换为网络字节序
		/// </summary>
		/// <param name="strDateTime">格式："2018-06-13 08:01:45"</param>
		/// <returns></returns>
		public static byte[] SnmpStrDateTime2Bytes(string strDateTime)
		{
			DateTime dt = String2DateTime(strDateTime);

			return DateTime2Bytes(dt);
		}


		/// <summary>
		/// 将DateTime转换为byte数组，并转换为网络字节序
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static byte[] DateTime2Bytes(DateTime dt)
		{
			byte[] bytes = new byte[8];
			if (dt != null)
			{
				// 年
				ushort year = (ushort)dt.Year;
				byte[] bytesYear = BitConverter.GetBytes(year);
				if (BitConverter.IsLittleEndian) // 转换为网络字节序
				{
					Array.Reverse(bytesYear);
				}
				Buffer.BlockCopy(bytesYear, 0, bytes, 0, 2);

				bytes[2] = Convert.ToByte(dt.Month.ToString(), 10);
				bytes[3] = Convert.ToByte(dt.Day.ToString(), 10);
				bytes[4] = Convert.ToByte(dt.Hour.ToString(), 10);
				bytes[5] = Convert.ToByte(dt.Minute.ToString(), 10);
				bytes[6] = Convert.ToByte(dt.Second.ToString(), 10);
			}

			return bytes;
		}

		/// <summary>
		/// 将日期字符串转换为DateTime类型
		/// </summary>
		/// <param name="strDateTime"></param>
		/// <param name="strFormat"></param>
		/// <returns>DateTime</returns>
		public static DateTime String2DateTime(string strDateTime, string strFormat = "yyyy-MM-dd HH:mm:ss")
		{
			DateTime dt;

			DateTimeFormatInfo dtForm = new DateTimeFormatInfo();
			dtForm.ShortDatePattern = strFormat;

			dt = Convert.ToDateTime(strDateTime, dtForm);

			return dt;
		}


		/// <summary>
		/// 将Bits类型的数值翻译成具体的位描述意义
		/// </summary>
		/// <param name="strBitsTypeValue">值</param>
		/// <param name="strValueList">值的取值范围</param>
		/// <param name="strOutput">bit位的描述意义</param>
		/// <returns></returns>
		/// ===========================================================================
		/// Bit值=>0< strValueList=>0:时钟故障/1:传输故障< 输出=>无效<
		/// Bit值=>1< strValueList=>0:时钟故障/1:传输故障< 输出=>时钟故障<
		/// Bit值=>2< strValueList=>0:时钟故障/1:传输故障< 输出=>传输故障<
		/// Bit值=>3< strValueList=>0:时钟故障/1:传输故障< 输出=>时钟故障/传输故障<
		/// Bit值=>4< strValueList=>0:时钟故障/1:传输故障< 输出=>4<
		/// ===========================================================================
		public static bool GenerateBitsTypeDesc(string strBitsTypeValue, string strValueList, out string strOutput)
		{
			// 初始化
			strOutput = "";
			strBitsTypeValue = strBitsTypeValue.Trim();
			// "0"值
			if ("0".Equals(strBitsTypeValue))
			{
				strOutput = "无效";
				return true;
			}

			// 解析“0:时钟故障/1:传输故障”格式的字符串
			//var dicBit2Desc = new Dictionary<int, string>();
			//var keyValList = strValueList.Split('/');
			//foreach (var item in keyValList)
			//{
			//	var keyVal = item.Split(':');
			//	dicBit2Desc.Add(Convert.ToInt32(keyVal[0]), keyVal[1]);
			//}

			var dicBit2Desc = MibStringHelper.SplitManageValue(strValueList);
			// 将bits值转换为unsigned long
			var u32BitsTypeValue = Convert.ToUInt32(strBitsTypeValue);
			for (var i = 0; i < 32; i++)
			{
				var u32BitValue = (uint)1 << i;
				var u32Tmp = u32BitsTypeValue & u32BitValue;
				if (u32Tmp != 0 & dicBit2Desc.ContainsKey(i))
				{
					strOutput += dicBit2Desc[i];
					strOutput += @"/";
				}
			}

			strOutput = strOutput.TrimEnd('/');

			// 如果没有解析结果, 把原值填上
			if (string.IsNullOrEmpty(strOutput))
			{
				strOutput = strBitsTypeValue;
			}

			return true;
		}


		/// <summary>
		/// 通过OID和对应的值，获得相关的信息（名字、描述、以及单位）
		/// </summary>
		/// <param name="strNeIp"></param>
		/// <param name="oid"></param>
		/// <param name="strValue"></param>
		/// <param name="strName"></param>
		/// <param name="strValueDesc"></param>
		/// <param name="strUnitName"></param>
		public static bool GetInfoByOID(string strNeIp, string oid, string strValue, out string strName
			, out string strValueDesc, out string strUnitName)
		{
			strName = "";
			strValueDesc = "";
			strUnitName = "";

			// Mib前缀
			var strMibPrefix = SnmpToDatabase.GetMibPrefix();

			var strOidTmp = oid.Replace(strMibPrefix, "");

			// 获取节点信息
			var mibObjInfo = GetParentMibNodeByChildOID(strNeIp, strOidTmp);
			if (mibObjInfo == null)
			{
				Log.Error($"GetInfoByOID() 中根据OID:{strOidTmp}获取网元:{strNeIp}的MIB节点失败");
				return false;
			}

			if (strOidTmp.IndexOf(mibObjInfo.childOid, StringComparison.Ordinal) != 0)
			{
				// 如果MIB节点OID不是原OID的首个位置，则返回失败。
				Log.Error($"MIB节点OID: {mibObjInfo.childOid}不是原OID: {strOidTmp}的首个位置");
				return false;
			}

			// 组字符串
			var strTempDesc = mibObjInfo.mibDesc;
			strName = mibObjInfo.childNameMib;
			var strMibName = strName;

			// 描述截取,格式：告警箱版本(字符串长度1~255(字节))(初配值："")
			var index = strTempDesc.IndexOf('(');
			if (index > 0)
			{
				strTempDesc = strTempDesc.Substring(0, index);
			}
			strName = $"{strTempDesc}({strName})";

			// 值的实际意义,使用TranslateMibValue函数来解析值的描述, 其中包含BITS类型的支持
			strValueDesc = strValue;

			// 值的中文描述
			string strReValue;
			if (!TranslateMibValue(strNeIp, strMibName, strValueDesc, out strReValue))
			{
				Log.Error($"TranslateMibValue函数返回失败, 参数: {strMibName}, {strValueDesc}");
			}
			// 获取到的值的中文描述
			strValueDesc = strReValue;

			// 获取单位
			strUnitName = CommFuns.ParseMibUnit(mibObjInfo.mibDesc);

			// 获取索引
			var strIndex = strOidTmp;
			strIndex = strIndex.Replace(mibObjInfo.childOid, "");
			if (mibObjInfo.IsTable) // 是表显示取索引
			{
				strIndex = strIndex.TrimStart('.');
				strName = $"{strName}{CommString.IDS_INSTANCE}{strIndex}";
			}

			return true;
		}

		/// <summary>
		/// 根据Oid获取实例的Mib信息
		/// </summary>
		/// <param name="strNeIp"></param>
		/// <param name="strMibOid"></param>
		/// <returns></returns>
		public static MibLeaf GetMibNodeInfoByOID(string strNeIp, string strMibOid)
		{
			MibLeaf reData;

			string strError;
			if (!Database.GetInstance().GetMibDataByOid(strMibOid, out reData, strNeIp, out strError))
			{
				reData = null;
			}

			return reData;
		}

		/// <summary>
		/// 通过子节点的OID来查找父节点
		/// </summary>
		/// <param name="strNeIp"></param>
		/// <param name="strChildOid"></param>
		/// <returns></returns>
		private static MibLeaf GetParentMibNodeByChildOID(string strNeIp, string strChildOid)
		{
			if (string.IsNullOrEmpty(strChildOid))
			{
				return null;
			}

			var strParentOid = strChildOid;
			var index = strParentOid.LastIndexOf('.');

			while (index > 0)
			{
				strParentOid = strParentOid.Substring(0, index);

				var reData = GetMibNodeInfoByOID(strNeIp, strParentOid);
				if (reData != null)
				{
					return reData;
				}

				index = strParentOid.LastIndexOf('.');
			}

			return null;
		}

		/// <summary>
		/// Add By Mayi
		/// </summary>
		/// <returns></returns>
		public static string GetNodeTypeByOIDInCache(string strNeIp, string strOid)
		{
			// Mib前缀
			var strMibPrefix = SnmpToDatabase.GetMibPrefix();
			// 去掉Oid的Mib前缀
			var strOidTmp = strOid.Replace(strMibPrefix, "");

			var mibLeaf = GetParentMibNodeByChildOID(strNeIp, strOidTmp);

			return mibLeaf?.mibSyntax;
		}

		/// <summary>
		/// 把Mib的数值翻译成描述，或者是把描述翻译为实际的值
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="strMibName">Mib名称</param>
		/// <param name="strMibValue">Mib的值或Mib的描述</param>
		/// <param name="strReValue">返回值，Mib描述或值</param>
		/// <param name="bValueToDesc">true:将值翻译为描述；false:将描述翻译为值</param>
		/// <returns></returns>
		public static bool TranslateMibValue(string strIpAddr, string strMibName, string strMibValue, out string strReValue, bool bValueToDesc = true)
		{
			strReValue = "";

			// 节点信息
			var mibNodeInfo = SnmpToDatabase.GetMibNodeInfoByName(strMibName, strIpAddr);
			if (mibNodeInfo == null)
			{
				Log.Error($"获取到的Mib信息为空，strMibName={strMibName}");
				return false;
			}

			// 节点取值范围
			var strMibValList = mibNodeInfo.managerValueRange;
			// 节点类型
			var strMibSyntax = mibNodeInfo.mibSyntax;

			var strInMibValue = strMibValue;

			// 处理BITS类型
			if (string.Equals("BITS", strMibSyntax, StringComparison.OrdinalIgnoreCase))
			{
				return SnmpMibUtil.GenerateBitsTypeDesc(strInMibValue, strMibValList, out strReValue);
			}

			// TODO 类型为Unsigned32Array时，下面的方法会异常
			//strReValue = SnmpToDatabase.ConvertValueToString(mibNodeInfo, strInMibValue);
			// todo 此处没有处理从描述转为值的情况

			// 根据mib值获取描述或根据描述获取mib值
			// TODO: 看不懂原来的逻辑。。。。先按照"0:本地文件/1:远端文件"格式解析
			if (!string.IsNullOrEmpty(strMibValList))
			{
				var keyValList = strMibValList.Split('/');
				foreach (var item in keyValList)
				{
					var keyVal = item.Split(':');
					if (keyVal.Length < 2) continue;

					if (bValueToDesc) //值翻译为描述
					{
						if (string.Equals(strMibValue, keyVal[0], StringComparison.OrdinalIgnoreCase))
						{
							strReValue = keyVal[1];
							break;
						}
					}
					else // 描述翻译为值
					{
						if (string.Equals(strMibValue, keyVal[1], StringComparison.OrdinalIgnoreCase))
						{
							strReValue = keyVal[0];
							break;
						}
					}
				}
			}

			// 如果没有获取到，将mib值返回
			if (string.IsNullOrEmpty(strReValue))
			{
				strReValue = strMibValue;
			}

			return true;
		}

		/// <summary>
		/// 根据取值列表把Mib的数值翻译成实际的意义，或把实际意义翻译成数值
		/// </summary>
		/// <param name="strMibValueList"></param>
		/// <param name="strMibValue"></param>
		/// <param name="strReValue"></param>
		/// <param name="isValueToDesc"></param>
		/// <returns></returns>
		public static bool TranslateMibValue(string strMibValueList, string strMibValue
												, out string strReValue, bool isValueToDesc = true)
		{
			strReValue = "";

			// 根据mib值获取描述或根据描述获取mib值
			//if (!string.IsNullOrEmpty(strMibValueList))
			//{
			//	var keyValList = strMibValueList.Split('/');
			//	foreach (var item in keyValList)
			//	{
			//		var keyVal = item.Split(':');
			//		if (keyVal.Length >= 2)
			//		{
			//			if (isValueToDesc) //值翻译为描述
			//			{
			//				if (string.Equals(strMibValue, keyVal[0], StringComparison.OrdinalIgnoreCase))
			//				{
			//					strReValue = keyVal[1];
			//					break;
			//				}
			//			}
			//			else // 描述翻译为值
			//			{
			//				if (string.Equals(strMibValue, keyVal[1], StringComparison.OrdinalIgnoreCase))
			//				{
			//					strReValue = keyVal[0];
			//					break;
			//				}
			//			}
			//		}
			//	}
			//}
			var kvMap = MibStringHelper.SplitManageValue(strMibValueList);
			if (isValueToDesc)
			{
				int nMibValue;
				if (int.TryParse(strMibValue, out nMibValue))
				{
					if (kvMap.ContainsKey(nMibValue))
					{
						strReValue = kvMap[nMibValue];
					}
				}
			}
			else
			{
				foreach (var item in kvMap)
				{
					if (item.Value.Equals(strMibValue))
					{
						strReValue = item.Key.ToString();
						break;
					}
				}
			}

			// 如果没有获取到，将mib值返回
			if (string.IsNullOrEmpty(strReValue))
			{
				strReValue = strMibValue;
			}
			return true;
		}

		/// <summary>
		/// 根据Mib的英文名称校验Mib的值
		/// </summary>
		/// <param name="strIpAddr">基站IP</param>
		/// <param name="mibNameEn">Mib英文名称</param>
		/// <param name="strValue">True：校验合法；False：校验非法</param>
		/// <returns></returns>
		public static bool CheckMibValueByNameEn(string strIpAddr, string mibNameEn, string strValue)
		{
			if (string.IsNullOrEmpty(strIpAddr) || string.IsNullOrEmpty(mibNameEn))
			{
				return false;
			}

			//获取Mib节点信息
			MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByName(mibNameEn, strIpAddr);
			if (null == mibLeaf)
			{
				return false;
			}

			return CheckMibValueByMibLeaf(mibLeaf, strValue);
		}

		/// <summary>
		/// 根据Oid校验Mib值
		/// </summary>
		/// <param name="strIpAddr">基站IP</param>
		/// <param name="oid">Mib的Oid</param>
		/// <param name="strValue">Mib值</param>
		/// <returns>True:校验合法；False:校验非法</returns>
		public static bool CheckMibValueByOid(string strIpAddr, string oid, string strValue)
		{
			if (string.IsNullOrEmpty(oid))
			{
				return false;
			}

			//获取Mib节点信息
			MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByOid(oid, strIpAddr);
			if (null == mibLeaf)
			{
				return false;
			}

			return CheckMibValueByMibLeaf(mibLeaf, strValue);
		}

		/// <summary>
		/// 根据MibLeaf对象校验Mib值
		/// </summary>
		/// <param name="mibLeaf">MibLeaf对象</param>
		/// <param name="strValue">Mib值</param>
		/// <returns>True:校验通过；False:校验未通过</returns>
		public static bool CheckMibValueByMibLeaf(MibLeaf mibLeaf, string strValue)
		{
			if (mibLeaf == null)
			{
				return false;
			}

			// LONG和UINT32类型校验
			if (string.Equals(mibLeaf.mibSyntax, "LONG", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(mibLeaf.mibSyntax, "UINT32", StringComparison.OrdinalIgnoreCase))
			{
				return CheckLongOrUint32(mibLeaf.managerValueRange, strValue);
			}
			else if (string.Equals(mibLeaf.mibSyntax, "OCTETS", StringComparison.OrdinalIgnoreCase))
			{
				// TODO:此处不需要再用 "ASNType": "DisplayString"限制吧？
				return CheckString(mibLeaf.managerValueRange, strValue);
			}
			else if (string.Equals(mibLeaf.mibSyntax, "BITS", StringComparison.OrdinalIgnoreCase))
			{
				// 此处只做校验，不返回计算后的值
				uint bitsVal = 0;
				return GetBitsTypeValueFromDesc(mibLeaf.managerValueRange, strValue, out bitsVal);
			}
			else if (string.Equals(mibLeaf.mibSyntax, "IPADDR", StringComparison.OrdinalIgnoreCase) 
				|| string.Equals(mibLeaf.mibSyntax, "INETADDRESS", StringComparison.OrdinalIgnoreCase))
			{
				return CheckInetAddress(strValue);
			}
			else if (string.Equals(mibLeaf.mibSyntax, "DATETIME", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(mibLeaf.mibSyntax, "DATEANDTIME", StringComparison.OrdinalIgnoreCase))
			{
				return CheckDateTime(strValue);
			}
			else if (string.Equals(mibLeaf.mibSyntax, "MACADDRESS", StringComparison.OrdinalIgnoreCase))
			{
				return CheckMacAddr(strValue);
			}
			else if (string.Equals(mibLeaf.mibSyntax, "MNCMCCTYPE", StringComparison.OrdinalIgnoreCase))
			{
				return CheckMncMccType(strValue);
			}


			return true;
		}

		/// <summary>
		/// MncMccType类型校验
		/// </summary>
		/// <param name="strMncMccVal">MncMccType类的值</param>
		/// <returns></returns>
		public static bool CheckMncMccType(string strMncMccVal)
		{
			if (string.IsNullOrEmpty(strMncMccVal))
			{
				return false;
			}
			if (strMncMccVal.Length != 2 && strMncMccVal.Length != 3)
			{
				return false;
			}

			uint val;
			bool rs = uint.TryParse(strMncMccVal, out val);

			return rs;
		}

		/// <summary>
		/// MacAddress类型校验
		/// </summary>
		/// <param name="strMac">MacAddress类型的值</param>
		/// <returns></returns>
		public static bool CheckMacAddr(string strMac)
		{
			string partt = @"^([0-9a-fA-F]{2})(([/\s::][0-9a-fA-F]{2}){5})$";
			Regex r = new Regex(partt);
			return r.IsMatch(strMac);
		}

		/// <summary>
		/// IP地址类型校验，包括IPV4和IPV6
		/// </summary>
		/// <param name="strIp">IP字符串</param>
		/// <returns></returns>
		public static bool CheckInetAddress(string strIp)
		{
			IPAddress ip;
			return IPAddress.TryParse(strIp, out ip);
		}

		/// <summary>
		/// 校验DateTime类型字符串，格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>
		/// <param name="strDataTime">日期类型字符串</param>
		/// <returns></returns>
		public static bool CheckDateTime(string strDataTime)
		{
			DateTime dt;
			return DateTime.TryParseExact(strDataTime, "yyyy-MM-dd HH:mm:ss",null, DateTimeStyles.None, out dt);
		}

		/// <summary>
		/// 检查字符串类型的Mib节点值是否在取值范围内，即检查字符串的长度是否合法
		/// 对应mib.json文件中的"mibSyntax": "OCTETS"和"ASNType": "DisplayString"类型的校验
		/// </summary>
		/// <param name="strValList">取值范围，如："1-150"</param>
		/// <param name="strValue">mib节点的当前值</param>
		/// <returns></returns>
		public static bool CheckString(string strValList, string strValue)
		{
			if (string.IsNullOrEmpty(strValList) || null == strValue)
			{
				return false;
			}

			// 取值范围，格式 : "1-150"
			int index = strValList.IndexOf('-');
			string[] valRange = strValList.Split('-');
			if (valRange.Length != 2)
			{
				return false;
			}

			int minLen = Convert.ToInt32(valRange[0]);
			int MaxLen = Convert.ToInt32(valRange[1]);
			// 检查字符串的长度
			if (strValue.Length < minLen || strValue.Length > MaxLen)
			{
				return false;
			}

			return true;
		}


		/// <summary>
		/// 检查LONG或UINT32类型的Mib节点值是否在取值范围内
		/// 其中LONG包含枚举和数字两种类型
		/// </summary>
		/// <param name="strValList">取值范围，如："-2147483648-2147483647" 或 "0:关闭/1:故障对象自动复位/2:网元自动复位"</param>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static bool CheckLongOrUint32(string strValList, string strValue)
		{
			if (string.IsNullOrEmpty(strValList) || strValue == null)
			{
				return false;
			}

			long longVal;
			if (Int64.TryParse(strValue, out longVal) == false)
			{
				return false;
			}

			// 判断取值范围是枚举类型还是数字类型
			if (strValList.IndexOf(':') > 0) // 有":"就认为是枚举
			{
				var valRange = MibStringHelper.SplitManageValue(strValList);
				if (!valRange.ContainsKey(Convert.ToInt32(longVal)))
				{
					return false;
				}
			}
			else if (strValList.LastIndexOf('-') > 0) // "-2147483648-2147483647"格式的取值范围
			{
				long minVal;
				long maxVal;
				int index = strValList.IndexOf("--");
				if (index > 0) // 最大值为负数的情况，如："-100--50"
				{
					minVal = Convert.ToInt64(strValList.Substring(0, index));
					maxVal = Convert.ToInt64(strValList.Substring(index + 2));
				} 
				else // "1-200"或"-100-200"格式 
				{
					index = strValList.LastIndexOf('-');
					minVal = Convert.ToInt64(strValList.Substring(0, index));
					maxVal = Convert.ToInt64(strValList.Substring(index + 1));
				}

				long val = Convert.ToInt64(strValue);
				if (val < minVal || val > maxVal)
				{
					return false;
				}
			}
			else // 取值范围格式无法识别
			{
				return false;
			}

			return true;
		}


		/// <summary>
		/// 根据Bits类型的描述信息校验值是否合法，并计算出整数值。此方法即可以用来做校验，也可以用来获取Bits计算后的值
		/// 如：
		/// strValList="0:通信类/1:服务质量类/2:处理类/3:设备类/4:环境类", strInputValue="通信类/服务质量类",  outValue=3
		/// strValList="0:通信类/1:服务质量类/2:处理类/3:设备类/4:环境类", strInputValue="主要告警/次要告警",  outValue=6
		/// </summary>
		/// <param name="strValList">取值范围，如："0:通信类/1:服务质量类/2:处理类/3:设备类/4:环境类"</param>
		/// <param name="strInputValue">描述信息，如："通信类/服务质量类"</param>
		/// <param name="outValue">根据Bits类型的描述值计算得到的uint值</param>
		/// <returns>True:校验值合法；False:校验值非法</returns>
		public static bool GetBitsTypeValueFromDesc(string strValList, string strInputValue, out uint outValue)
		{
			outValue = 0; // 无效

			// 无效
			if (string.IsNullOrEmpty(strInputValue) || "无效".Equals(strInputValue))
			{
				outValue = 0;
				return true;
			}
			if (string.IsNullOrEmpty(strValList))
			{
				return false;
			}

			// 存储Bits取值范围的描述和对应值
			Dictionary<string, int> desc2Value = new Dictionary<string, int>();
			// 解析取值范围
			string[] valRangeList = strValList.Split('/');
			foreach (string item in valRangeList)
			{
				string[] valDesc = item.Split(':');
				if (valDesc.Length != 2)
				{
					return false;
				}
				desc2Value.Add(valDesc[1], Convert.ToInt32(valDesc[0]));
			}

			// 存储入参描述对应的数字
			List<int> keyList = new List<int>();
			// 解析入参
			valRangeList = strInputValue.Split('/');
			foreach (string item in valRangeList)
			{
				string desc;
				if (item.IndexOf(':') >= 0) // 入参即有键又有描述的情况
				{
					desc = item.Substring(0, item.IndexOf(':'));
				}
				else
				{
					desc = item;
				}

				if (!desc2Value.ContainsKey(desc))
				{
					return false;
				}
				keyList.Add(desc2Value[desc]);
			}

			// 计算最终值
			foreach (int val in keyList)
			{
				uint addVal = (uint)1 << val;
				outValue |= addVal;
			}

			return true;
		}

		/// <summary>
		/// 校验Mib值是否含有非法字符
		/// </summary>
		/// <param name="strValue">Mib值</param>
		/// <param name="strMsg">提示信息</param>
		/// <returns>True:未含有非法字符；False:含有非法字符</returns>
		public static bool CheckMibValidChar(string strValue, out string strMsg)
		{
			strMsg = "";
			if (string.IsNullOrEmpty(strValue))
			{
				return true;
			}
			if (strValue.Contains("&") || strValue.Contains("<")
				|| strValue.Contains(">") || strValue.Contains("\r"))
			{
				strMsg = "请确保字符串中没有 &、 < 、 > 、回车符";
				return false;
			}
			
			return true;
		}

	}

}
