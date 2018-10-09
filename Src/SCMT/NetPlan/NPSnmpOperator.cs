using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using LinkPath;
using LmtbSnmp;
using LogManager;
using DIC_DOUBLE_STR = System.Collections.Generic.Dictionary<string, string>;

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

		#endregion

		#region 私有接口

		

		#endregion
	}
}
