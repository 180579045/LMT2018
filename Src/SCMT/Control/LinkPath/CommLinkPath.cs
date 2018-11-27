/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ CommLinkPath $
* 机器名称：       $ machinename $
* 命名空间：       $ LinkPath $
* 文 件 名：       $ CommLinkPath.cs $
* 创建时间：       $ 2018.09.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     LinkPath项目的公共服务类。
* 修改时间     修 改 人         修改内容：
* 2018.09.xx  XXXX            XXXXX
*************************************************************************************/
using LmtbSnmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using DataBaseUtil;
using LogManager;
using MAP_STRING = System.Collections.Generic.Dictionary<string, string>;

namespace LinkPath
{
	/// <summary>
	/// LinkPath项目的公共服务类
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

		/// <summary>
		/// 判断一个实例是否存在
		/// </summary>
		/// <param name="strCmdName"></param>
		/// <param name="strIndex"></param>
		/// <returns></returns>
		public static bool IsExistRecord(string strCmdName, string strIndex)
		{
			long requestId;
			var ioPdu = new CDTLmtbPdu();
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("尚未选中基站");
			}

			var ret = CDTCmdExecuteMgr.GetInstance().CmdGetSync(strCmdName, out requestId, strIndex, targetIp, ref ioPdu);
			if (0 != ret)
			{
				return false;
			}

			var ec = ioPdu.m_LastErrorStatus;
			if (0 != ec)
			{
				Log.Error($"命令：{strCmdName}执行失败，错误码：{ec}，描述：{SnmpErrDescHelper.GetErrDescById(ec)}");
				return false;
			}

			return true;
		}

	}
}
