using LogManager;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LmtbSnmp
{
	/// <summary>
	/// 对Snmp相关信息的初始化和原语操作的实现
	/// </summary>
	public class LmtbSnmpEx : ILmtbSnmp
	{
		// 同步snmp实例
		private SnmpHelper m_SnmpSync = null;

		// 异步snmp实例
		private SnmpHelper m_SnmpAsync = null;

		// 缓存异步SNMP请求时，requestId 与 LmtPdu对应关系
		Dictionary<long, stru_LmtbPduAppendInfo> m_ReqIdPduInfo = new Dictionary<long, stru_LmtbPduAppendInfo>();

		// 数据库里的节点类型描述到Snmp Syntax的映射
		public static Dictionary<string, SNMP_SYNTAX_TYPE> m_ValType2SynTax 
			= new Dictionary<string, SNMP_SYNTAX_TYPE>
			{
				{ "OCTETS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "PHYADDR", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "TEMP_ID", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "LONG", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "BOOL", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "UINT32", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_UINT32},
				{ "ROWSTATUS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "NULL", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NULL},
				{ "OID", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID},
				{ "IPADDR", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_IPADDR},
				{ "TIMETICKS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_TIMETICKS},
				{ "DATETIME", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "INETADDRESS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "VARIABLEPOINTER", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID},
				{ "DATEANDTIME", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "BITS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_UINT32},
				{ "OCTET STRING", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "INETADDRESSTYPE", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "MACADDRESS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "UNSIGNED32ARRAY", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "INTEGER32ARRAY", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "MNCMCCTYPE", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS}
			};

		/// <summary>
		/// 获取snmp同步实例
		/// </summary>
		/// <returns></returns>
		public SnmpHelper GetSnmpSyncInst()
		{
			return m_SnmpSync;
		}

		/// <summary>
		/// 获取snmp异步实例
		/// </summary>
		/// <returns></returns>
		public SnmpHelper GetSnmpAsyncInst()
		{
			return m_SnmpAsync;
		}



		public LmtbSnmpEx()
		{
		}

		

		/// <summary>
		/// 功能描述：Snmp的初始化工作，包括：
		/// 1，socket初始化
		/// 2，设置目标Agent端IP地址
		/// 3，设置目标Agent端消息监听端口
		/// 4，设置超时值
		/// 5，设置Read，Write的Community
		/// 6，设置重传次数
		/// 7，设置Snmp的版本号
		/// 8，设置Inform和Trap消息监听端口
		/// 9，注册异步回调函数
		/// 10，创建事情处理线程
		/// </summary>
		/// <param name="trapPort"></param>
		/// <returns></returns>
		public int SnmpLibStartUp(string commnuity, string destIpAddr)
		{
	 //       Log.Debug("========== SnmpLibStartUp() Start ==========");

			// ipv4
			CreateSnmpSession(commnuity, destIpAddr, false);

			// TODO: ipv6




	 //       Log.Debug("========== SnmpLibStartUp() End ==========");

			return 0;
		}

		/// <summary>
		/// 创建snmp回话，包括同步、异步两这个snmp连接
		/// </summary>
		/// <param name="isIpV6"></param>
		/// <returns></returns>
		public int CreateSnmpSession(string commnuity, string destIpAddr, bool isIpV6)
		{
			int status = 0;
			// 同步snmp连接
			m_SnmpSync = new SnmpHelperV2c(commnuity, destIpAddr);
			// 异步snmp连接
			m_SnmpAsync = new SnmpHelperV2c(commnuity, destIpAddr);

			return status;
		}

		/// <summary>
		/// 同步Get操作
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="requestId"></param>
		/// <param name="strIpAddr"></param>
		/// <param name="timeOut"></param>
		/// <returns></returns>
		public int SnmpGetSync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr, long timeOut)
		{
			requestId = 0;

 //           Log.Debug("========== SnmpGetSync() Start ==========");
			var msg = $"pars: lmtPdu={lmtPdu}, requestId={requestId}, strIpAddr={strIpAddr}, timeOut={timeOut}";
 //           Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("strIpAddr is null");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}


			SnmpHelper snmp = m_SnmpSync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令");
				Log.Error(msg);
				return -1;
			}

			Pdu pdu;
			bool rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);
			if (!rs)
			{
				Log.Error("LmtPdu2SnmpPdu()转换错误");
				return -1;
			}

			pdu.Type = PduType.Get;

			SnmpV2Packet result = snmp.GetRequest(pdu);
			if (result == null)
			{
				Log.Error("SNMP request error, response is null.");
				return -1;
			}

			requestId = result.Pdu.RequestId;

			rs = SnmpPdu2LmtPdu(result.Pdu, snmp.m_target, ref lmtPdu, 0, false);


			return 0;
		}

		/// <summary>
		/// 同步Get操作(提供给SnmpSessionWorker调用)
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="queryVbs"></param>
		/// <param name="results"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public bool SnmpGetSync(string strIpAddr, List<CDTLmtbVb> queryVbs, out Dictionary<string, string> results, long timeout)
		{
			// 初始化out参数
			results = new Dictionary<string, string>();
			
			// log msg
			string logMsg;
			bool status;

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("strIpAddr is null");
				return false;
			}

			SnmpHelper snmp = m_SnmpSync;
			if (null == snmp)
			{
				logMsg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令", strIpAddr);
				Log.Error(logMsg);
				return false;
			}

			Pdu pdu;
			PacketQueryPdu(queryVbs, out pdu);

			SnmpV2Packet ReqResult = (SnmpV2Packet)m_SnmpSync.GetRequest(pdu);

			if (null != ReqResult)
			{
				if (ReqResult.Pdu.ErrorStatus != 0)
				{
					logMsg = string.Format("Error in SNMP reply. Error {0} index {1}"
						, ReqResult.Pdu.ErrorStatus, ReqResult.Pdu.ErrorIndex);
					Log.Error(logMsg);
					status = false;
				}
				else
				{
					foreach (Vb vb in ReqResult.Pdu.VbList)
					{
						logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
							, vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
						//                       Log.Debug(logMsg);
						results.Add(vb.Oid.ToString(), vb.Value.ToString());
					}
					status = true;
				}
			}
			else
			{
				Log.Error("SNMP GetNextRequest请求错误");
				return false;
			}

			return status;
		}

		/// <summary>
		/// 异步Get操作
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="requestId"></param>
		/// <param name="strIpAddr"></param>
		/// <returns></returns>
		public int SnmpGetAsync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr)
		{
			requestId = 0;

			Log.Debug("========== SnmpGetSync() Start ==========");
			string msg = string.Format("pars: lmtPdu={0}, requestId={1}, strIpAddr={2}"
				, lmtPdu, requestId, strIpAddr);
			Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("strIpAddr is null");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}

			// TODO: 根据基站ip获取Lmtor信息
			//LMTORINFO* pLmtorInfo = CDTAppStatusInfo::GetInstance()->GetLmtorInfo(remoteIpAddr);

			SnmpHelper snmp = m_SnmpAsync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令");
				Log.Error(msg);
				return -1;
			}

			Pdu pdu = new Pdu();
			requestId = pdu.RequestId;

			bool rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);
			if (!rs)
			{
				Log.Error("LmtPdu2SnmpPdu()转换错误");
				return -1;
			}

			pdu.Type = PduType.Get;

			SnmpAsyncResponse callback = new SnmpAsyncResponse(SnmpCallbackFun);

			bool status = snmp.GetRequestAsync(pdu, callback);

			if (!status)
			{
				Log.Error("SNMP Get异步请求错误");
			}

			// 缓存异步消息
			stru_LmtbPduAppendInfo appendInfo = new stru_LmtbPduAppendInfo();
			lmtPdu.getAppendValue(appendInfo);
			Push_appendInfo(requestId, appendInfo);

			return 0;
		}

		/// <summary>
		/// GetNextRequest
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="queryVbs"></param>
		/// <param name="result"></param>
		/// <param name="timeout"></param>
		/// <returns></returns>
		public bool GetNextRequest(string strIpAddr, List<CDTLmtbVb> queryVbs, out Dictionary<string,string> result, long timeout)
		{
			result = new Dictionary<string, string>();

			bool status = false;
			string logMsg;

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("strIpAddr is null");
				return false;
			}
			if (queryVbs == null)
			{
				Log.Error("参数queryVbs为空");
				return false;
			}

			Pdu pdu;
			PacketQueryPdu(queryVbs, out pdu);

			SnmpV2Packet ReqResult = (SnmpV2Packet)m_SnmpSync.GetNextRequest(pdu);

			if (null != ReqResult)
			{
				if (ReqResult.Pdu.ErrorStatus != 0)
				{
					logMsg = string.Format("Error in SNMP reply. Error {0} index {1}"
						, ReqResult.Pdu.ErrorStatus, ReqResult.Pdu.ErrorIndex);
					Log.Error(logMsg);
					status = false;

					// 
					if (ReqResult.Pdu.ErrorStatus == SnmpConstants.ErrResourceUnavailable)// endOfMibView
					{
						return false;
					}
				}
				else
				{
					foreach (Vb vb in ReqResult.Pdu.VbList)
					{
						//logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
						//	, vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
 //                       Log.Debug(logMsg);
						result.Add(vb.Oid.ToString(), vb.Value.ToString());
					}
					status = true;
				}
			}
			else
			{
				Log.Error("SNMP GetNextRequest请求错误");
				return false;
			}

			return status;
		}


		/// <summary>
		/// 同步Set操作
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="requestId"></param>
		/// <param name="strIpAddr"></param>
		/// <param name="timeOut"></param>
		/// <returns></returns>
		public int SnmpSetSync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr, long timeOut)
		{
			requestId = 0;

//			Log.Debug("========== SnmpGetSync() Start ==========");
			string msg = string.Format("pars: lmtPdu={0}, requestId={1}, strIpAddr={2}, timeOut={3}"
				, lmtPdu, requestId, strIpAddr, timeOut);
//			Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}

			// TODO: 根据基站ip获取Lmtor信息
			//LMTORINFO* pLmtorInfo = CDTAppStatusInfo::GetInstance()->GetLmtorInfo(remoteIpAddr);

			SnmpHelper snmp = m_SnmpSync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令", strIpAddr);
				Log.Error(msg);
				return -1;
			}

			// TODO
			 lmtPdu.setReqMsgType((int)PduType.Set);


			Pdu pdu = new Pdu();
			bool rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);

			SnmpV2Packet result = snmp.SetRequest(pdu);

			if (result == null)
			{
				Log.Error("SNMP request error, response is null.");
				return -1;
			}

			rs = SnmpPdu2LmtPdu(result.Pdu, snmp.m_target, ref lmtPdu, 0, false);

			return 0;
		}

		/// <summary>
		/// SNMP Set同步操作(提供给SnmpSessionWorker调用)
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="setVbs"></param>
		/// <param name="timeOut"></param>
		/// <returns></returns>
		public bool SnmpSetSync(string strIpAddr, List<CDTLmtbVb> setVbs, long timeOut)
		{
		//	Log.Debug("========== SnmpSetSync() Start ==========");
			string logMsg = string.Format("pars: strIpAddr={0}, timeOut={1}"
				,strIpAddr, timeOut);
//			Log.Debug(logMsg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return false;
			}

			SnmpHelper snmp = m_SnmpSync;
			if (null == snmp)
			{
				logMsg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令", strIpAddr);
				Log.Error(logMsg);
				return false;
			}

			Pdu pdu;
			PacketSetPdu(setVbs, out pdu);

			SnmpV2Packet result = snmp.SetRequest(pdu);

			if (result == null)
			{
				Log.Error("SNMP set request error, response is null.");
				return false;
			}
			else
			{
				if (result.Pdu.ErrorStatus != 0)
				{
					logMsg = string.Format("Error in SNMP reply. Error {0} index {1}"
						, result.Pdu.ErrorStatus, result.Pdu.ErrorIndex);
					//Log.Error(logMsg);
					return false;
				}
			}

			return true;
		}


		/// <summary>
		/// 异步Set操作
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="requestId"></param>
		/// <param name="strIpAddr"></param>
		/// <returns></returns>
		public int SnmpSetAsync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr)
		{
			requestId = 0;

			Log.Debug("========== SnmpGetSync() Start ==========");
			string msg = string.Format("pars: lmtPdu={0}, requestId={1}, strIpAddr={2}"
				, lmtPdu, requestId, strIpAddr);
			Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}

			// TODO: 根据基站ip获取Lmtor信息
			//LMTORINFO* pLmtorInfo = CDTAppStatusInfo::GetInstance()->GetLmtorInfo(remoteIpAddr);

			SnmpHelper snmp = m_SnmpAsync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令");
				Log.Error(msg);
				return -1;
			}

			// TODO
			lmtPdu.setReqMsgType((int)PduType.Set);


			Pdu pdu = new Pdu();
			requestId = pdu.RequestId;
			// TODO:
			bool rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);
			if (!rs)
			{
				Log.Error("LmtPdu2SnmpPdu()转换错误");
			}

			pdu.Type = PduType.Set;

			SnmpAsyncResponse snmpCallback = new SnmpAsyncResponse(this.SnmpCallbackFun);

			rs = snmp.SetRequestAsync(pdu, snmpCallback);

			if (!rs)
			{
				Log.Error("SNMP 异步请求错误");
			}

			// 缓存异步请求信息
			stru_LmtbPduAppendInfo appendInfo = new stru_LmtbPduAppendInfo();
			lmtPdu.getAppendValue(appendInfo);
			Push_appendInfo(requestId, appendInfo);


			return 0;
		}

		/// <summary>
		/// Add By Mayi  
		/// </summary>
		/// <param name="ipAddr"></param>
		/// <param name="oid"></param>
		/// <param name="mibProFix"></param>
		/// <returns></returns>
		private string GetNodeTypeByOIDInCache(string oid)
		{
			string csMIBPrefix = "1.3.6.1.4.1.5105.100.";

			string strTempOid = oid.Replace(csMIBPrefix, ""); 

			string strNodeType = "";

			if ((strTempOid == "1.3.1.1.1.7.1") || (strTempOid == "1.3.1.1.1.10.1"))
			{
				strNodeType = "DateandTime";
				return strNodeType;
			}
			else
			{
				return null;
			}            
		}

		/// <summary>
		/// 将LmtPdu转换为snmpPdu
		/// </summary>
		/// <param name="pdu"></param>
		/// <param name="lmtPdu"></param>
		/// <param name="strRemoteIp"></param>
		/// <returns></returns>
		private bool LmtPdu2SnmpPdu(out Pdu pdu, CDTLmtbPdu lmtPdu, string strRemoteIp)
		{
			pdu = new Pdu();
			string strTmpOid;
			string strValue;
			SNMP_SYNTAX_TYPE strSyntaxType;

			int lmtVbCount = lmtPdu.VbCount();
			for (int i = 0; i < lmtVbCount; i++)
			{
				CDTLmtbVb cDTLmtbVb = lmtPdu.GetVbByIndexEx(i);

				strTmpOid = cDTLmtbVb.Oid;
				strSyntaxType = cDTLmtbVb.SnmpSyntax;
				strValue = cDTLmtbVb.Value;

				Vb vb = new Vb(new Oid(strTmpOid));

				//String strNodeType = GetNodeTypeByOIDInCache(csIpAddr, strOID, strMIBPrefix);
				string strNodeType = GetNodeTypeByOIDInCache(strTmpOid);

				SnmpHelper.SetVbValue(ref vb, strSyntaxType, strValue, strNodeType);

				// TODO

				pdu.VbList.Add(vb);

			} // end for


			return true;
		}


		/// <summary>
		/// 将snmp类型的pdu转换为LmtSnmp的pdu
		/// </summary>
		/// <param name="pdu"></param>
		/// <param name="target"></param>
		/// <param name="lmtPdu"></param>
		/// <param name="reason"></param>
		/// <param name="isAsync"></param>
		private bool SnmpPdu2LmtPdu(Pdu pdu, UdpTarget target, ref CDTLmtbPdu lmtPdu, int reason, bool isAsync)
		{
			string logMsg;
			if (lmtPdu == null)
			{
				Log.Error("参数[lmtPdu]为空");
				return false;
			}


			stru_LmtbPduAppendInfo appendInfo = new stru_LmtbPduAppendInfo();
			appendInfo.m_bIsSync = !isAsync;

			logMsg = string.Format("snmpPackage.Pdu.Type = {0}", pdu.Type);
			Log.Debug(logMsg);

			// 判断响应消息类型
			if (pdu.Type != PduType.V2Trap) // 非Trap消息
			{
				
				if (isAsync)
				{
					//如果该操作是异步Snmp命令发起的，该函数必定在Rsp_CallBack里被调用，lmtPdu里没有任何信息，需要从map里取
					if (false == Pop_appendInfo(pdu.RequestId, ref appendInfo))
					{
						Log.Error(string.Format("{找不到requestId={0}的PduAppendInfo实例}", pdu.RequestId));
						return false;
					}
				}
				else //如果是同步命令，则lmtPdu里已经包含了Pdu信息
				{
					lmtPdu.getAppendValue(appendInfo);
				}

			}
			else // Trap消息
			{
				appendInfo.m_bIsNeedPrint = true;
			}


			lmtPdu.Clear();
			lmtPdu.m_LastErrorIndex = pdu.ErrorIndex;
			lmtPdu.m_LastErrorStatus = pdu.ErrorStatus;
			lmtPdu.m_requestId = pdu.RequestId;
			lmtPdu.assignAppendValue(appendInfo);

			// 设置IP和端口信息
			IPAddress srcIpAddr = target.Address;
			int port = target.Port;
			lmtPdu.m_SourceIp = srcIpAddr.ToString();
			lmtPdu.m_SourcePort = port;

			lmtPdu.reason = reason;
			lmtPdu.m_type = (ushort)pdu.Type;


			// TODO
			/*
			LMTORINFO* pLmtorInfo = CDTAppStatusInfo::GetInstance()->GetLmtorInfo(csIpAddr);
			if (pLmtorInfo != NULL && pLmtorInfo->m_isSimpleConnect && pdu.get_type() == sNMP_PDU_TRAP)
			{
				Oid id;
				pdu.get_notify_id(id);
				CString strTrapOid = id.get_printable();
				if (strTrapOid != "1.3.6.1.4.1.5105.100.1.2.2.3.1.1")
				{
					//如果是简单连接网元的非文件传输结果事件，就不要往上层抛送了
					return FALSE;
				}
			}
			*/

			//如果是错误的响应，则直接返回
			if (lmtPdu.m_LastErrorStatus != 0 || reason == -5)
			{
				return true;
			}

			// 获取MIB前缀
			string prefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			if (string.IsNullOrEmpty(prefix))
			{
				Log.Error(string.Format("获取MIB前缀失败!"));
				return false;
			}

			// 对于Trap消息,我们自己额外构造两个Vb，用来装载时间戳和trap Id 
			if (pdu.Type == PduType.V2Trap) // Trap
			{
				// 构造时间戳Vb
				CDTLmtbVb lmtVb = new CDTLmtbVb();
				lmtVb.Oid = "时间戳";
				// TODO 是这个时间戳吗？？？？
				lmtVb.Value = pdu.TrapSysUpTime.ToString();
				lmtVb.SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID;
				lmtPdu.AddVb(lmtVb);

				// 构造Trap Id Vb
				lmtVb = new CDTLmtbVb();
				lmtVb.Oid = "notifyid";
				// TODO 对吗？？？
				lmtVb.Value = pdu.TrapObjectID.ToString();
				lmtVb.SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID;
				lmtPdu.AddVb(lmtVb);
			}

			foreach (Vb vb in pdu.VbList)
			{
				logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
					, vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
				Log.Debug(logMsg);

				CDTLmtbVb lmtVb = new CDTLmtbVb();

				lmtVb.Oid = vb.Oid.ToString();

				// 值是否需要SetVbValue()处理
				bool isNeedPostDispose = true;

				string strValue = vb.Value.ToString();

				// TODO
				lmtVb.SnmpSyntax = (SNMP_SYNTAX_TYPE)vb.Type; //vb.GetType();

				// 如果是getbulk响应返回的SNMP_SYNTAX_ENDOFMIBVIEW，则不处理这个vb，继续
				if (lmtVb.SnmpSyntax == SNMP_SYNTAX_TYPE.SNMP_SYNTAX_ENDOFMIBVIEW)
				{
					lmtPdu.isEndOfMibView = true;
					continue;
				}

				if (SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS == lmtVb.SnmpSyntax)
				{
					/*对于像inetipAddress和DateandTime需要做一下特殊处理，把内存值转换为显示文本*/
					// CString strNodeType = GetNodeTypeByOIDInCache(csIpAddr, strOID, strMIBPrefix);
					string strNodeType = "";
					// strNodeType = "DateandTime";

					if (string.Equals("DateandTime", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = SnmpHelper.SnmpDateTime2String((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
					else if (string.Equals("inetaddress", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						IpAddress ipAddr = new IpAddress((OctetString)vb.Value);
						strValue = ipAddr.ToString();
						isNeedPostDispose = false;
					}
					else if (string.Equals("MacAddress", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = ((OctetString)vb.Value).ToMACAddressString();
						isNeedPostDispose = false;
					}
					else if (string.Equals("Unsigned32Array", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = SnmpHelper.OctetStrToU32Array((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
					else if (string.Equals("Integer32Array", strNodeType, StringComparison.OrdinalIgnoreCase) 
						|| "".Equals(strNodeType))
					{
						strValue = SnmpHelper.OctetStrToS32Array((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
					else if (string.Equals("MncMccType", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = SnmpHelper.OctetStr2MncMccTypeStr((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
				}

				if (isNeedPostDispose)// 需要再处理
				{
					SnmpHelper.GetVbValue(vb, ref strValue);
				}
				
				lmtVb.Value = strValue;
				lmtPdu.AddVb(lmtVb);
			} // end foreach


			//如果得到的LmtbPdu对象里的vb个数为0，说明是是getbulk响应，并且没有任何实例
			//为方便后面统一处理，将错误码设为资源不可得
			if (lmtPdu.VbCount() == 0)
			{
				// TODO: SNMP_ERROR_RESOURCE_UNAVAIL
				lmtPdu.m_LastErrorStatus = 13;
				lmtPdu.m_LastErrorIndex = 1;
			}

			return true;
		}

		/// <summary>
		/// SNMP请求回调方法
		/// </summary>
		/// <param name="result"></param>
		/// <param name="packet"></param>
		private void SnmpCallbackFun(AsyncRequestResult result, SnmpPacket packet)
		{
			string logMsg = string.Format("SNMP异步请求结果: AsyncRequestResult = {0}", result);
			Log.Info(logMsg);

			if (result == AsyncRequestResult.NoError)
			{
				SnmpV2Packet packetv2 = (SnmpV2Packet)packet;

				if (packetv2 == null)
				{
					Log.Error("SNMP request error, response is null.");
					return;
				}

				CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
				bool rs = SnmpPdu2LmtPdu(packetv2.Pdu, m_SnmpAsync.m_target, ref lmtPdu, 0, false);

				// TODO
				// 发消息
				if (packetv2.Pdu.Type == PduType.Inform)
				{
				}


			}

			return;
		}


		/// <summary>
		/// 将CDTLmtbVb数组转换为SNMP Pdu
		/// </summary>
		/// <param name="queryVbs"></param>
		/// <param name="pdu"></param>
		public void PacketQueryPdu(List<CDTLmtbVb> queryVbs, out Pdu pdu)
		{
			pdu = new Pdu(PduType.GetNext);

			if (queryVbs == null)
			{
				Log.Error("参数queryVbs为空");
				return;
	}

			foreach (CDTLmtbVb lmtVb in queryVbs)
			{
				Vb vb = new Vb(new Oid(lmtVb.Oid));
				pdu.VbList.Add(vb);
}
		}

		public void PacketQueryPdu(List<string> oidList, out Pdu pdu)
		{
			pdu = new Pdu(PduType.GetNext);
			if (null == oidList)
			{
				return;
			}

			foreach (var oid in oidList)
			{
				pdu.VbList.Add(new Vb(oid));
			}
		}

		/// <summary>
		/// 组装Set Pdu
		/// </summary>
		/// <param name="setVbs"></param>
		/// <param name="setPdu"></param>
		public void PacketSetPdu(List<CDTLmtbVb> setVbs, out Pdu setPdu)
		{
			setPdu = new Pdu(PduType.Set);

			foreach (CDTLmtbVb lmtbVb in setVbs)
			{

				Vb vb = new Vb(new Oid(lmtbVb.Oid));

				 SnmpHelper.SetVbValue(ref vb, lmtbVb.SnmpSyntax, lmtbVb.Value);

				setPdu.VbList.Add(vb);
			}
		}

		/// <summary>
		/// 数据库里的节点类型描述到Snmp Syntax的映射
		/// </summary>
		/// <param name="strType"></param>
		/// <returns></returns>
		public static SNMP_SYNTAX_TYPE GetSyntax(string strType)
		{
			return m_ValType2SynTax[strType];
		}


		/// <summary>
		/// 保存异步SNMP请求信息
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="appendInfo"></param>
		public void Push_appendInfo(long requestId, stru_LmtbPduAppendInfo appendInfo)
		{
			m_ReqIdPduInfo[requestId] = appendInfo;
		}

		/// <summary>
		/// 获取SNMP异步缓存信息
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="appendInfo"></param>
		/// <returns></returns>
		public bool Pop_appendInfo(long requestId, ref stru_LmtbPduAppendInfo appendInfo)
		{
			bool rs = false;
			foreach (KeyValuePair<long, stru_LmtbPduAppendInfo> item in m_ReqIdPduInfo)
			{
				if (item.Key == requestId)
				{
					appendInfo = item.Value;
					// 移除
					m_ReqIdPduInfo.Remove(requestId);
					rs = true;
				}
			}

			return rs;
		}


	}
}
