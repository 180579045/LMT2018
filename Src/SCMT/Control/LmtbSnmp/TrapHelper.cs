using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using SnmpSharpNet;
using MIBDataParser.JSONDataMgr;
using MIBDataParser;
using MsgQueue;
using CommonUtility;

namespace LmtbSnmp
{
	/// <summary>
	/// SNMP Trap 和 Inform类
	/// </summary>
	public class TrapHelper
	{
		// Trap socket for IPV4
		protected Socket _socketIpv4;
		// Trap socket for IPV6
		protected Socket _socketIpv6;
		// Trap端口号
		protected int m_port = 162;

		/// <summary>
		/// 构造方法
		/// </summary>
		public TrapHelper()
		{
		}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="port"></param>
		public TrapHelper(int port)
		{
			m_port = port;
		}

		/// <summary>
		/// 初始化IPV4 Trap服务
		/// </summary>
		/// <returns></returns>
		public bool InitReceiverIpv4()
		{
			if (_socketIpv4 != null)
			{
				StopReceiver();
			}

			try
			{
				// create UDP Socket
				_socketIpv4 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message.ToString());
				// there is no need to close the socket because it was never correctly created
				_socketIpv4 = null;
			}

			if (_socketIpv4 == null)
			{
				return false;
			}

			try
			{
				// 绑定本地端口
				EndPoint localEp = new IPEndPoint(IPAddress.Any, m_port);
				_socketIpv4.Bind(localEp);
			}
			catch (SocketException ex)
			{
				Log.Error(ex.Message.ToString());
				_socketIpv4.Close();
				_socketIpv4 = null;
				 throw ex;
			}

			if (_socketIpv4 == null)
			{
				return false;
			}

			if (!RegisterReceiveOperation(_socketIpv4))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 初始化IPV6 Trap服务
		/// </summary>
		/// <returns></returns>
		public bool InitReceiverIpv6()
		{
			if (_socketIpv6 != null)
			{
				StopReceiver();
			}

			try
			{
				// create UDP Socket
				_socketIpv6 = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message.ToString());
				// there is no need to close the socket because it was never correctly created
				_socketIpv6 = null;
			}

			if (_socketIpv6 == null)
			{
				return false;
			}

			try
			{
				// 绑定本地端口
				EndPoint localEp = new IPEndPoint(IPAddress.IPv6Any, m_port);
				_socketIpv6.Bind(localEp);
			}
			catch (SocketException ex)
			{
				Log.Error(ex.Message.ToString());
				_socketIpv6.Close();
				_socketIpv6 = null;
				throw ex;
			}

			if (_socketIpv6 == null)
			{
				return false;
			}

			if (!RegisterReceiveOperation(_socketIpv6))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 接收消息操作
		/// </summary>
		/// <returns></returns>
		public bool RegisterReceiveOperation(Socket socket)
		{
			if (socket == null)
			{
				return false;
			}

			try
			{
				EndPoint ep = null;
				if (socket.AddressFamily == AddressFamily.InterNetwork) // IPV4
				{
					ep = new IPEndPoint(IPAddress.Any, 0);

				}
				else if (socket.AddressFamily == AddressFamily.InterNetworkV6) // IPV6
				{
					ep = new IPEndPoint(IPAddress.IPv6Any, 0);
				}
				else
				{
					Log.Error(string.Format("不能识别的Socket地址族 socket.AddressFamily:{0}", socket.AddressFamily));
					return false;
				}
				StateObject stateObject = new StateObject();
				stateObject.workSocket = socket;
				// 接收消息
				socket.BeginReceiveFrom(stateObject.buffer, 0, 64 * 1024, SocketFlags.None
					, ref ep, new AsyncCallback(ReceiveCallback), stateObject);
			}
			catch ( Exception ex)
			{
				Log.Error(ex.Message.ToString());
				socket.Close();
				socket = null;
			}

			if (socket == null)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 接收Trap消息的回调函数
		/// </summary>
		/// <param name="result"></param>
		private void ReceiveCallback(IAsyncResult result)
		{
			StateObject stateObj = (StateObject)result.AsyncState;
			Socket sock = stateObj.workSocket;
			byte[] buffer = stateObj.buffer;

			EndPoint ep = null;
			if (sock.AddressFamily == AddressFamily.InterNetwork) // IPV4
			{
				ep = new IPEndPoint(IPAddress.Any, 0);
			}
			else if (sock.AddressFamily == AddressFamily.InterNetworkV6) // IPV6
			{
				ep = new IPEndPoint(IPAddress.IPv6Any, 0);
			}
			else
			{
				return;
			}

			// 接收的数据大小
			int intLen;

			try
			{
				intLen = sock.EndReceiveFrom(result, ref ep);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message.ToString());
				intLen = -1;
			}

			if (sock == null)
			{
				return;
			}

			// 数据未接收完，继续接收
			if (intLen <= 0)
			{
				RegisterReceiveOperation(sock);
				return;
			}

			// 获取SNMP版本
			int packetVersion = SnmpPacket.GetProtocolVersion(buffer, intLen);
			if (packetVersion == (int)SnmpVersion.Ver1)
			{

				 Log.Error("接收到的SNMP Trap消息版本为V1");
			}
			else if (packetVersion == (int)SnmpVersion.Ver2)
			{
				SnmpV2Packet pkt = new SnmpV2Packet();
				try
				{
					pkt.decode(buffer, intLen);
				}
				catch (Exception ex)
				{
					Log.Error(ex.Message.ToString());
					pkt = null;
				}

				if (pkt != null)
				{
					if (pkt.Pdu.Type == PduType.V2Trap) // trap
					{
						Log.Info(string.Format("** SNMPv2 Trap from {0}", ep.ToString()));
					}
					else if (pkt.Pdu.Type == PduType.Inform) // inform
					{
						Log.Info(string.Format("** SNMPv2 Trap from {0}", ep.ToString()));
					}
					else
					{
						Log.Error(string.Format("Invalid SNMPv2 packet from {0}", ep.ToString()));
						pkt = null;
					}

					if (pkt != null)
					{
						Log.Debug(string.Format("*** community: {0}, sysUpTime: {1}, trapObjectID: {2}"
							, pkt.Community, pkt.Pdu.TrapSysUpTime.ToString(), pkt.Pdu.TrapObjectID.ToString()));

						Log.Debug(string.Format("*** PDU count: {0}", pkt.Pdu.VbCount));

						foreach (Vb vb in pkt.Pdu.VbList)
						{
							Log.Debug(string.Format("**** Trap Vb oid: {0}, type: {1}, value: {2}"
								,vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString()));
						}

						if (pkt.Pdu.Type == PduType.V2Trap)
						{
							Log.Debug("** End of SNMPv2 Trap");
						}
						else
						{
							Log.Debug("** End of SNMPv2 INFORM");

							// 发送Inform响应消息
							SnmpV2Packet response = pkt.BuildInformResponse();
							byte[] buf = response.encode();
							sock.SendTo(buf, ep);
						}

						// Trap消息处理
						CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
						// 将Pdu转换为CDTLmtbPdu
						if (false == this.SnmpPdu2LmtPdu4Trap(pkt.Pdu, (IPEndPoint)ep, ref lmtPdu, 0, true))
						{
							Log.Error("SnmpPdu2LmtPdu4Trap()调用错误！");
						}
						else
						{
							// 发布Trap消息
							byte[] bytes = SerializeHelper.Serialize2Binary(lmtPdu);
							PublishHelper.PublishMsg(TopicHelper.SnmpMsgDispose_OnTrap, bytes);
						}
					}

				}
			}

			RegisterReceiveOperation(sock);
		}

		/// <summary>
		/// 停止Trap服务器
		/// </summary>
		public void StopReceiver()
		{
			if (_socketIpv4 != null)
			{
				_socketIpv4.Close();
				_socketIpv4 = null;
			}

			if (_socketIpv6 != null)
			{
				_socketIpv6.Close();
				_socketIpv6 = null;
			}
		}


		/// <summary>
		/// 将Trap的snmp类型的pdu转换为LmtSnmp的pdu
		/// </summary>
		/// <param name="pdu"></param>
		/// <param name="iPEndPort"></param>
		/// <param name="lmtPdu"></param>
		/// <param name="reason"></param>
		/// <param name="isAsync"></param>
		private bool SnmpPdu2LmtPdu4Trap(Pdu pdu, IPEndPoint iPEndPort, ref CDTLmtbPdu lmtPdu, int reason, bool isAsync)
		{
			if (lmtPdu == null)
			{
				Log.Error("参数[lmtPdu]为空");
				return false;
			}

			if (pdu.Type != PduType.V2Trap)
			{
				Log.Error("接收到的不是Trap消息或不是V2Trap消息！");
				return false;
			}

			stru_LmtbPduAppendInfo appendInfo = new stru_LmtbPduAppendInfo {m_bIsSync = !isAsync};

			var logMsg = $"snmpPackage.Pdu.Type = {pdu.Type}";
			//			Log.Debug(logMsg);

			appendInfo.m_bIsNeedPrint = true;

			lmtPdu.Clear();
			lmtPdu.m_LastErrorIndex = pdu.ErrorIndex;
			lmtPdu.m_LastErrorStatus = pdu.ErrorStatus;
			lmtPdu.m_requestId = pdu.RequestId;
			lmtPdu.assignAppendValue(appendInfo);

			// 设置IP和端口信息
			IPAddress srcIpAddr = iPEndPort.Address;
			int port = iPEndPort.Port;
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
				Log.Error("获取MIB前缀失败!");
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
					, vb.Oid, SnmpConstants.GetTypeName(vb.Value.Type), vb.Value);
				Log.Debug(logMsg);

				CDTLmtbVb lmtVb = new CDTLmtbVb {Oid = vb.Oid.ToString()};

				// 值是否需要SetVbValue()处理
				bool isNeedPostDispose = true;

				string strValue = vb.Value.ToString();

				// TODO
				lmtVb.SnmpSyntax = (SNMP_SYNTAX_TYPE)vb.Value.Type; //vb.GetType();

				// 如果是getbulk响应返回的SNMP_SYNTAX_ENDOFMIBVIEW，则不处理这个vb，继续
				if (lmtVb.SnmpSyntax == SNMP_SYNTAX_TYPE.SNMP_SYNTAX_ENDOFMIBVIEW)
				{
					lmtPdu.isEndOfMibView = true;
					continue;
				}

				// 将SNMP节点值转换为可显示的文本字符串
				SnmpMibUtil.ConvertSnmpVal2MibStr(iPEndPort.Address.ToString(), vb, out strValue);

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


	}
}
