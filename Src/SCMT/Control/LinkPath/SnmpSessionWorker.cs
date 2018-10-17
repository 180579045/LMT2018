/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ SnmpSessionWorker $
* 机器名称：       $ machinename $
* 命名空间：       $ LinkPath $
* 文 件 名：       $ SnmpSessionWorker.cs $
* 创建时间：       $ 2018.09.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     提供Snmp通信功能（提供无须向上层抛送消息的查询和设置操作）。
* 修改时间     修 改 人         修改内容：
* 2018.09.xx  XXXX            XXXXX
*************************************************************************************/
using LmtbSnmp;
using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LinkPath
{
	/// <summary>
	/// 提供Snmp通信功能（提供无须向上层抛送消息的查询和设置操作）
	/// </summary>
	public class SnmpSessionWorker
	{

		/// <summary>
		/// 组装CDTLmtbVb信息
		/// </summary>
		/// <param name="packetVb"></param>
		/// <param name="oid"></param>
		/// <param name="value"></param>
		/// <param name="syntaxType"></param>
		public static void PacketVb(ref CDTLmtbVb packetVb, string oid, string value, SNMP_SYNTAX_TYPE syntaxType)
		{
			if (null == packetVb)
			{
				packetVb = new CDTLmtbVb();
			}

			packetVb.Oid = oid;
			packetVb.Value = value;
			packetVb.SnmpSyntax = syntaxType;
		}

		/// <summary>
		/// 组装CDTLmtbVb信息
		/// </summary>
		/// <param name="packetVb"></param>
		/// <param name="oid"></param>
		/// <param name="value"></param>
		/// <param name="asnType"></param>
		public static void PacketVb(ref CDTLmtbVb packetVb, string oid, string value, string asnType)
		{
			if (null == packetVb)
			{
				packetVb = new CDTLmtbVb();
			}

			packetVb.Oid = oid;
			packetVb.Value = value;
			packetVb.SnmpSyntax = LmtbSnmpEx.GetSyntax(asnType);
		}

		/// <summary>
		/// SNMP Get同步方法
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="vbs"></param>
		/// <param name="queryResults"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public static bool SnmpGetSync(string strIpAddr, List<CDTLmtbVb> vbs, out Dictionary<string, string> queryResults, long timeout)
		{
			queryResults = new Dictionary<string, string>();

			ILmtbSnmp lmtbSnmp = DTLinkPathMgr.GetSnmpInstance(strIpAddr);
			if (lmtbSnmp == null)
			{
				Log.Error("获取lmtbSnmp实例错误");
				return false;
			}

			return lmtbSnmp.SnmpGetSync(strIpAddr, vbs, out queryResults, timeout);
		}

		/// <summary>
		/// SNMP Set同步方法
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="setVbs"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public static bool SnmpSetSync(string strIpAddr, List<CDTLmtbVb> setVbs, long timeout)
		{
			ILmtbSnmp lmtbSnmp = DTLinkPathMgr.GetSnmpInstance(strIpAddr);
			if (null == lmtbSnmp)
			{
				Log.Error("获取lmtbSnmp实例错误");
				return false;
			}

			return lmtbSnmp.SnmpSetSync(strIpAddr, setVbs, timeout);
		}


		/// <summary>
		/// SNMP GetNext同步操作
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="vbs"></param>
		/// <param name="queryResults"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public static bool SnmpGetNextSync(string strIpAddr, List<CDTLmtbVb> vbs, out Dictionary<string, string> queryResults, long timeout)
		{
			queryResults = new Dictionary<string, string>();

			ILmtbSnmp lmtbSnmp = DTLinkPathMgr.GetSnmpInstance(strIpAddr);
			if (null == lmtbSnmp)
			{
				Log.Error("获取lmtbSnmp实例错误");
				return false;
			}

			return lmtbSnmp.GetNextRequest(strIpAddr, vbs, out queryResults, timeout);
		}

		/// <summary>
		/// 根据oid获取其值
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public static string GetQueryValueByOid(string oid, Dictionary<string, string> result)
		{
			if (result == null)
			{
				return null;
			}

			return result[oid];
		}

	}
}
