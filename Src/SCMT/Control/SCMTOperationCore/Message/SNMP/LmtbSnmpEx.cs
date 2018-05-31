using LogManager;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Message.SNMP
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

		public SnmpHelper GetSnmpSync()
		{
			return m_SnmpSync;
		}

		public SnmpHelper GetSnmpAsync()
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

			Log.Debug("========== SnmpGetSync() Start ==========");
			var msg = $"pars: lmtPdu={lmtPdu}, requestId={requestId}, strIpAddr={strIpAddr}, timeOut={timeOut}";
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

			SnmpHelper snmp = m_SnmpSync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令");
				Log.Error(msg);
				return -1;
			}

			/*
			List<string> vbList = new List<string>();

			int vbCount = lmtPdu.VbCount();

			for (int i = 0; i < vbCount; i++)
			{
				CDTLmtbVb lmtVb =  lmtPdu.GetVbByIndexEx(i);
				string oid = lmtVb.get_Oid();
				vbList.Add(oid);
			}
			*/

			// SnmpV2Packet result = snmp.GetRequest(vbList);

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

			rs = SnmpPdu2LmtPdu(result, snmp.m_target, lmtPdu, 0, false);


			return 0;
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

			SnmpAsyncResponse callback = new SnmpAsyncResponse(this.SnmpCallbackFun);

			bool status = snmp.GetRequestAsync(pdu, callback);

			if (!status)
			{
				Log.Error("SNMP Get异步请求错误");
				return -1;
			}

			return 0;
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

			Log.Debug("========== SnmpGetSync() Start ==========");
			string msg = string.Format("pars: lmtPdu={0}, requestId={1}, strIpAddr={2}, timeOut={3}"
				, lmtPdu, requestId, strIpAddr, timeOut);
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

			SnmpHelper snmp = m_SnmpSync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令");
				Log.Error(msg);
				return -1;
			}

			// TODO
			 lmtPdu.setReqMsgType((int)PduType.Set);


			Pdu pdu = new Pdu();
			// TODO:
			string community = "private";
			bool rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);

			SnmpV2Packet result = snmp.SetRequest(pdu);

			if (result == null)
			{
				Log.Error("SNMP request error, response is null.");
				return -1;
			}

			rs = SnmpPdu2LmtPdu(result, snmp.m_target, lmtPdu, 0, false);

			return 0;
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
				return -1;
			}

			return 0;
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

			string strMibPreFix = "";
			string strOid;
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

				if (SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS == strSyntaxType)
				{
					/*对于像inetipAddress和DateandTime需要做一下特殊处理，把内存值转换为显示文本*/
					// CString strNodeType = GetNodeTypeByOIDInCache(csIpAddr, strOID, strMIBPrefix);
					string strNodeType = "";

					if ("DateandTime".Equals(strNodeType))
					{
						// TODO
						// strValue = strValue;
					}
					else if ("inetaddress".Equals(strNodeType))
					{

					}
					else if ("MacAddress".Equals(strNodeType))
					{

					}
					else if ("Unsigned32Array".Equals(strNodeType))
					{

					}
					else if ("Integer32Array".Equals(strNodeType) || "".Equals(strNodeType))
					{

					}
					else if ("MncMccType".Equals(strNodeType))
					{

					}

				}

				// TODO

				Vb vb = new Vb(new Oid(strTmpOid));

				SetVbValue(ref vb, strSyntaxType, strValue);

				pdu.VbList.Add(vb);

			} // end for


			return true;
		}

		/// <summary>
		/// 设置snmp vb的值
		/// </summary>
		/// <param name="vb"></param>
		/// <param name="syntaxType"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public int SetVbValue(ref Vb vb, SNMP_SYNTAX_TYPE syntaxType, string value)
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
							vb.Value = new OctetString("null");
						}
						else
						{
							vb.Value = new OctetString(value);
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
						vb.Value = new UInteger32(value);
						break;
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_GAUGE32:
						vb.Value = new UInteger32(value);
						break;
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_TIMETICKS:
						vb.Value = new TimeTicks(value);
						break;
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OPAQUE:
						vb.Value = new Opaque(value);
						break;
					case SNMP_SYNTAX_TYPE.SNMP_SYNTAX_CNTR64:
						// TODO
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

			} catch (Exception e)
			{
				Log.Error(e.Message.ToString());
				throw e;
			}

			return 0;
		}

		/// <summary>
		/// 将snmp类型的pdu转换为LmtSnmp的pdu
		/// </summary>
		/// <param name="snmpPackage"></param>
		/// <param name="target"></param>
		/// <param name="lmtPdu"></param>
		/// <param name="reason"></param>
		/// <param name="isAsync"></param>
		private bool SnmpPdu2LmtPdu(SnmpV2Packet snmpPackage, UdpTarget target
			, CDTLmtbPdu lmtPdu, int reason, bool isAsync)
		{
			string logMsg;
			if (lmtPdu == null)
			{
				Log.Error("参数[lmtPdu]为空");
				return false;
			}

			// TODO
			// stru_LmtbPduAppendInfo appendInfo;
			logMsg = string.Format("snmpPackage.Pdu.Type = {0}", snmpPackage.Pdu.Type);
			Log.Debug(logMsg);
			logMsg = string.Format("PduType.V2Trap={0}", PduType.V2Trap);
			// TODO
			if (snmpPackage.Pdu.Type != PduType.V2Trap) // Trap
			{

			}
			else
			{

			}

			lmtPdu.Clear();
			lmtPdu.m_LastErrorIndex = snmpPackage.Pdu.ErrorIndex;
			lmtPdu.m_LastErrorStatus = snmpPackage.Pdu.ErrorStatus;
			lmtPdu.m_requestId = snmpPackage.Pdu.RequestId;

			// ip and port
			IPAddress srcIpAddr = target.Address;
			int port = target.Port;
			lmtPdu.m_SourceIp = srcIpAddr.ToString();
			lmtPdu.m_SourcePort = port;

			lmtPdu.reason = reason;
			//lmtPdu.SetPduType(snmpPackage.Pdu.Type);


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

			// 转换vb

			// 对于Trap消息,我们自己额外构造两个Vb，用来装载时间戳和trap Id 
			if (snmpPackage.Pdu.Type == PduType.V2Trap) // Trap
			{
				// TODO:
			}

			foreach(Vb vb in snmpPackage.Pdu.VbList)
			{
				logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
					, vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
				Log.Debug(logMsg);

				CDTLmtbVb lmtVb = new CDTLmtbVb();

				lmtVb.Oid = vb.Oid.ToString();

				// SnmpConstants.GetSyntaxObject(AsnType.OCTETSTRING);
				// SnmpConstants.GetTypeName(vb.Value.Type);

				// TODO
				// lmtVb.set_Syntax(vb.Value.GetType());

				// TODO:不确定对不对？？？？？？？
				if (AsnType.OCTETSTRING == vb.Value.Type)
				{
					/*对于像inetipAddress和DateandTime需要做一下特殊处理，把内存值转换为显示文本*/
					// CString strNodeType = GetNodeTypeByOIDInCache(csIpAddr, strOID, strMIBPrefix);
					string strNodeType = "";

					if ("DateandTime".Equals(strNodeType))
					{
					}
					else if ("inetaddress".Equals(strNodeType))
					{

					}
					else if ("MacAddress".Equals(strNodeType))
					{

					}
					else if ("Unsigned32Array".Equals(strNodeType))
					{

					}
					else if ("Integer32Array".Equals(strNodeType) || "".Equals(strNodeType))
					{

					}
					else if ("MncMccType".Equals(strNodeType))
					{

					}
				}

				string value = vb.Value.ToString();
				lmtVb.Value = value;
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
				bool rs = SnmpPdu2LmtPdu(packetv2, m_SnmpAsync.m_target, lmtPdu, 0, false);

				// TODO
				// 发消息



			}

			return;
		}



	}
}
