using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using LogManager;
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
				
			}
			else
			{
				Log.Error("长度错误");
			}

			return dtime;
		}

		// 根据命令名找到命令对应的所有信息
		public 

		#endregion

		#region 自定义的宏

		private static uint DATEANDTIMELENV2TC = 11;    /*SNMPV2 TC中定义的时间长度*/
		private static uint DATEANDTIMEDTCUSTOM = 19;   /*公司自定义的时间长度*/

		#endregion
	}
}
