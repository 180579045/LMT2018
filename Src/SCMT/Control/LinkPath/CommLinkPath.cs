using LmtbSnmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAP_STRING = System.Collections.Generic.Dictionary<string, string>;

namespace LinkPath
{
	/// <summary>
	/// LinkPath项目的通用文件
	/// </summary>
	public class CommLinkPath
	{

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

	}
}
