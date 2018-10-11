using LogManager;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LmtbSnmp;
using MIBDataParser.JSONDataMgr;

namespace LinkPath
{
	/// <summary>
	/// 以命令名称方式发送Snmp消息
	/// </summary>
	public sealed class CDTCmdExecuteMgr : Singleton<CDTCmdExecuteMgr>
	{
		private CDTCmdExecuteMgr()
		{
		}

		public void Initialize()
		{
			
		}


		/// <summary>
		/// 执行一条类型为Get的同步操作命令
		/// </summary>
		/// <param name="cmdName"></param>
		/// <param name="requestId"></param>
		/// <param name="strIndex"></param>
		/// <param name="strIpAddr"></param>
		/// <param name="lmtPdu">传出的PDU。可以用于获取oid对应的值</param>
		/// <returns></returns>
		public int CmdGetSync(string cmdName, out long requestId, string strIndex
							  , string strIpAddr, ref CDTLmtbPdu lmtPdu, bool isPrint = true
			, bool needCheck = false, long timeOut = 0)
		{
			requestId = 0;

			Log.Info("CmdGetSync() start");
			var msg = $"cmdName={cmdName},requestId={requestId},strIndex={strIndex}, strIpAddr={strIpAddr}";
			Log.Info(msg);

			if (string.IsNullOrEmpty(cmdName) || string.IsNullOrEmpty(strIpAddr))
			{
				throw new CustomException("传入参数错误");
			}

			PackGetCmdPdu(cmdName, strIndex, strIpAddr, needCheck, ref lmtPdu);
			lmtPdu.m_bIsNeedPrint = isPrint;
			lmtPdu.SetSyncId(true);

			// 根据ip获取当前基站的snmp实例
			LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(strIpAddr);
			var rs = lmtbSnmpEx.SnmpGetSync(lmtPdu, out requestId, strIpAddr, timeOut);
			if (rs != 0)
			{
				Log.Error("执行lmtbSnmpEx.SnmpGetSync()方法错误");
				return rs;
			}

			return 0;
		}

		/// <summary>
		/// 执行一条类型为Get的异步操作命令
		/// </summary>
		/// <param name="cmdName"></param>
		/// <param name="requestId"></param>
		/// <param name="strIndex"></param>
		/// <param name="strIpAddr"></param>
		/// <param name="lmtPdu"></param>
		/// <returns></returns>
		public int CmdGetAsync(string cmdName, out long requestId, string strIndex
							  , string strIpAddr, bool isPrint = true, bool needCheck = false)
		{
			requestId = 0;

			var msg = $"cmdName={cmdName},requestId={requestId},strIndex={strIndex}, strIpAddr={strIpAddr}";
			Log.Info(msg);

			if (string.IsNullOrEmpty(cmdName) || string.IsNullOrEmpty(strIpAddr))
			{
				return -1;
			}

			var lmtPdu = new CDTLmtbPdu();
			PackGetCmdPdu(cmdName, strIndex, strIpAddr, needCheck, ref lmtPdu);

			lmtPdu.m_bIsNeedPrint = isPrint;
			lmtPdu.SetSyncId(false);

			// 根据ip获取当前基站的snmp实例
			LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(strIpAddr);
			int rs = lmtbSnmpEx.SnmpGetAsync(lmtPdu, out requestId, strIpAddr);
			if (rs != 0)
			{
				Log.Error("执行lmtbSnmpEx.SnmpGetAsync()方法错误");
			}

			return 0;
		}

		/// <summary>
		/// 根据命令名称执行同步GetNext
		/// </summary>
		/// <param name="cmdName"></param>
		/// <param name="requestId"></param>
		/// <param name="indexList"></param>
		/// <param name="results"></param>
		/// <param name="strIpAddr"></param>
		/// <param name="isPrint"></param>
		/// <param name="needCheck"></param>
		/// <returns></returns>
		public int CmdGetNextSync(string cmdName, out long requestId, out List<string> indexList
							  , out Dictionary<string, string> results, string strIpAddr, bool isPrint = false
							  , bool needCheck = false)
		{
			// 初始化out变量
			requestId = 0;
			results = new Dictionary<string, string>();
			indexList = new List<string>();

			Log.Info("CmdGetNextSync() start");
			var logMsg = $"cmdName={cmdName},requestId={requestId}, strIpAddr={strIpAddr}";
			Log.Info(logMsg);

			if (string.IsNullOrEmpty(cmdName) || string.IsNullOrEmpty(strIpAddr))
			{
				return -1;
			}

			var cmdInfo = Database.GetInstance().GetCmdDataByName(cmdName, targetIp: strIpAddr);
			if (null == cmdInfo)
			{
				Log.Error($"未找到命令{cmdName}的信息");
				return -2;
			}

			var mibList = cmdInfo.m_leaflist;

			// TODO : bNeedCheck
			if (needCheck)
			{
			}

			// 获取oid的前缀
			var strPreFixOid = SnmpToDatabase.GetMibPrefix().TrimEnd('.');
			var sbOid = new StringBuilder();
			var lmtbVbs = new List<CDTLmtbVb>();

			// 组装lmtbVb参数
			foreach (var v in mibList)
			{
				var leaf = Database.GetInstance().GetMibDataByOid(v, strIpAddr);
				if (null == leaf)
				{
					Log.Error($"根据oid {v} 未找到关联的mib信息");
					continue;
				}

				if (0 == leaf.isMib)
				{
					// 过滤掉假mib
					continue;
				}

				sbOid.Clear();
				sbOid.AppendFormat("{0}.{1}", strPreFixOid, v);
				var lmtVb = new CDTLmtbVb {Oid = sbOid.ToString()};
				lmtbVbs.Add(lmtVb);
			}

			Dictionary<string, string> tmpResult;
			// 根据ip获取当前基站的snmp实例
			ILmtbSnmp lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(strIpAddr);
			while (true)
			{
				if (lmtbSnmpEx.GetNextRequest(strIpAddr, lmtbVbs, out tmpResult, 0))
				{
					// 清除查询参数
					lmtbVbs.Clear();
					foreach (var item in tmpResult)
					{
						logMsg = $"ObjectName={item.Key}, Value={item.Value}";
						//Log.Debug(logMsg);

						// 保存结果
						results.Add(item.Key, item.Value);

						// 组装新的查询oid
						var lmtVb = new CDTLmtbVb {Oid = item.Key};
						lmtbVbs.Add(lmtVb);
					}

				}
				else
				{
					break;
				}
			} //end while

			return 0;
		}



		/// <summary>
		/// 执行一条类型为Set的同步操作命令
		/// </summary>
		/// <param name="cmdName">命令名</param>
		/// <param name="requestId"></param>
		/// <param name="name2Value">命令对应的MIB及其值</param>
		/// <param name="strIndex">索引</param>
		/// <param name="strIpAddr">目标基站IP地址</param>
		/// <param name="lmtPdu"></param>
		/// <param name="isPrint"></param>
		/// <param name="needCheck"></param>
		/// <param name="timeOut"></param>
		/// <returns></returns>
		public static int CmdSetSync(string cmdName, out long requestId, Dictionary<string,string> name2Value
			, string strIndex, string strIpAddr, ref CDTLmtbPdu lmtPdu, bool isPrint = true
			, bool needCheck = false, long timeOut = 0)
		{
			requestId = 0;

			if (null == lmtPdu)
			{
				lmtPdu = new CDTLmtbPdu();
			}

			lmtPdu.Clear();

			PackSetCmdPdu(cmdName, strIndex, strIpAddr, needCheck, name2Value, ref lmtPdu);
			lmtPdu.m_bIsNeedPrint = isPrint;
			lmtPdu.SetSyncId(true);

			LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetInstance().GetSnmpByIp(strIpAddr);
			var rs = lmtbSnmpEx.SnmpSetSync(lmtPdu, out requestId, strIpAddr, timeOut);
			if (rs != 0)
			{
				Log.Error("执行lmtbSnmpEx.SnmpGetSync()方法错误");
			}
			
			return rs;
		}


		/// <summary>
		/// 执行一条类型为Set的异步操作命令
		/// </summary>
		/// <param name="cmdName"></param>
		/// <param name="requestId"></param>
		/// <param name="name2Value"></param>
		/// <param name="strIndex"></param>
		/// <param name="strIpAddr"></param>
		/// <param name="lmtPdu"></param>
		/// <param name="isPrint"></param>
		/// <param name="needCheck"></param>
		/// <param name="timeOut"></param>
		/// <returns></returns>
		public static int CmdSetAsync(string cmdName, out long requestId, Dictionary<string, string> name2Value
			, string strIndex, string strIpAddr, bool isPrint = true, bool needCheck = false)
		{
			requestId = 0;

			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();

			PackSetCmdPdu(cmdName, strIndex, strIpAddr, needCheck, name2Value, ref lmtPdu);
			lmtPdu.m_bIsNeedPrint = isPrint;
			lmtPdu.SetSyncId(false);

			LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetInstance().GetSnmpByIp(strIpAddr);
			var rs = lmtbSnmpEx.SnmpSetAsync(lmtPdu, out requestId, strIpAddr);
			if (rs != 0)
			{
				Log.Error("执行lmtbSnmpEx.SnmpSetAsync()方法错误");
			}

			return rs;
		}

		/// <summary>
		/// Add By Mayi  通过 oid 获取 父节点
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="strChildMibOid"></param>
		/// <returns></returns>
		//private MibNodeInfoTest GetParentMibNodeByChildOID(string strIpAddr, string strChildMibOid)
		//{
		//    string matchOid = strChildMibOid;

		//    int oidLastSectionPos = matchOid.LastIndexOf('.');

		//    while (-1 != oidLastSectionPos)
		//    {
		//        matchOid = matchOid.Substring(oidLastSectionPos);

		//        MibNodeInfoTest parentNode = GetMibNodeInfoByOID(strIpAddr, matchOid);
		//        if (null != parentNode)
		//        {
		//            return parentNode;
		//        }

		//        oidLastSectionPos = matchOid.LastIndexOf('.');
		//    }

		//    return null;
		//}


		// test
		private MibNodeInfoTest GetMibNodeInfoByOID(string strIpAddr, string strMibOid)
		{
			Dictionary<string, MibNodeInfoTest> mibNodeInfoList = new Dictionary<string, MibNodeInfoTest>();

			string oid = "100.1.2.2.2.1.2";
			string strType = "LONG";
			string strMibName = "fileTransRowStatus";
			MibNodeInfoTest a = new MibNodeInfoTest(oid, strType, strMibName);
			mibNodeInfoList.Add(oid, a);

			oid = "100.1.2.2.2.1.3";
			strType = "LONG";
			strMibName = "fileTransType";
			MibNodeInfoTest b = new MibNodeInfoTest(oid, strType, strMibName);
			mibNodeInfoList.Add(oid, b);


			oid = "100.1.2.2.2.1.4";
			strType = "LONG";
			strMibName = "fileTransIndicator";
			MibNodeInfoTest c = new MibNodeInfoTest(oid, strType, strMibName);
			mibNodeInfoList.Add(oid, c);

			oid = "100.1.2.2.2.1.5";
			strType = "OCTETS";
			strMibName = "fileTransNEDirectory";
			MibNodeInfoTest d = new MibNodeInfoTest(oid, strType, strMibName);
			mibNodeInfoList.Add(oid, d);

			oid = "100.1.2.2.2.1.6";
			strType = "OCTETS";
			strMibName = "fileTransFTPDirectory";
			MibNodeInfoTest e = new MibNodeInfoTest(oid, strType, strMibName);
			mibNodeInfoList.Add(oid, e);

			oid = "100.1.2.2.2.1.7";
			strType = "OCTETS";
			strMibName = "fileTransFileName";
			MibNodeInfoTest f = new MibNodeInfoTest(oid, strType, strMibName);
			mibNodeInfoList.Add(oid, f);

			return mibNodeInfoList[strMibOid];

		}

		public SNMP_SYNTAX_TYPE GetAsnTypeByMibType(string strMibType)
		{
			if ("OCTETS".Equals(strMibType))
			{
				return (SNMP_SYNTAX_TYPE)AsnType.OCTETSTRING;
			} else if ("LONG".Equals(strMibType))
			{
				return (SNMP_SYNTAX_TYPE)AsnType.INTEGER ;
			}

			return (SNMP_SYNTAX_TYPE)AsnType.OCTETSTRING;
		}

		/// <summary>
		/// 将以"|"分割的字符串转换为数组
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private string[] StringToArray(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}

			return str.Split('|');
		}

		#region 私有方法

		// 打包执行Get命令所需的pdu
		private bool PackGetCmdPdu(string cmdName, string index, string ip, bool bNeedCheck, ref CDTLmtbPdu pdu)
		{
			// TODO 返回的结果中leaflist是一个叶节点oid的集合
			var cmdInfo = SnmpToDatabase.GetCmdInfoByCmdName(cmdName, ip);
			if (null == cmdInfo)
			{
				throw new CustomException("get cmd info failed");
			}

			// 把查到的命令行对应的leaflist转换为vblist
			//var oidList = SnmpToDatabase.ConvertNameToOid(cmdInfo.m_leaflist, ip);
			var oidList = cmdInfo.m_leaflist;

			// 在此处校验索引是否有效
			if (bNeedCheck)
			{
				throw new NotImplementedException("校验索引有效性功能尚未实现");
			}

			if (null == pdu)
			{
				pdu = new CDTLmtbPdu();
			}

			var vbList = SnmpToDatabase.ConvertOidToVb(oidList, index);
			pdu.AddVb(vbList);
			pdu.SetCmdName(cmdName);

			return true;
		}

		// 打包执行set命令所需的pdu
		private static bool PackSetCmdPdu(string cmdName, string index, string ip, bool bNeedCheck,
			Dictionary<string, string> name2Value, ref CDTLmtbPdu pdu)
		{
			// 返回的结果中leaflist是一个叶节点名的集合，不是oid的集合
			var cmdInfo = SnmpToDatabase.GetCmdInfoByCmdName(cmdName, ip);
			if (null == cmdInfo)
			{
				throw new CustomException("get cmd info failed");
			}

			// 把name转为vblist，已经把数据值传入到vb中
			var vbList = SnmpToDatabase.ConvertNameToVbList(cmdInfo.m_leaflist, ip, index, bNeedCheck, name2Value);

			if (null == pdu)
			{
				pdu = new CDTLmtbPdu();
			}

			pdu.SetCmdName(cmdName);
			pdu.AddVb(vbList);

			return true;
		}

		#endregion
	}

	class MibNodeInfoTest
	{
		public string oid { get; set; }
		public string strType { get; set; }
		public string strMibName { get; set; }

		public MibNodeInfoTest(string oid, string strType, string strMibName)
		{
			this.oid = oid;
			this.strType = strType;
			this.strMibName = strMibName;

		}
	}
}
