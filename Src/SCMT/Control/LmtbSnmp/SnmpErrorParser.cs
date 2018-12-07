using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseUtil;
using LogManager;
using SnmpSharpNet;

// snmp错误解析
namespace LmtbSnmp
{
	public static class SnmpErrorParser
	{
		#region 静态接口

		public static void PrintPduError(Pdu errPdu)
		{
			if (null == errPdu)
			{
				return;
			}

			var pduDesc = errPdu.ToString();
			var errorDesc = SnmpErrDescHelper.GetErrDescById(errPdu.ErrorStatus);
			Log.Info($"{pduDesc}Error status describution : {errorDesc}");
		}


		#endregion
	}
}
