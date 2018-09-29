using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using SnmpSharpNet;

namespace LmtbSnmp
{
	/// <summary>
	/// SNMP Trap 和 Inform类
	/// </summary>
	public class TrapHelper
	{
		// Trap socket
		protected Socket _socket;
		// 接收消息数组
		protected byte[] _inBuffer;
		// 对端IP
		protected IPEndPoint _peerIp;
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
		/// 初始化Trap服务
		/// </summary>
		/// <returns></returns>
		public bool InitReceiver()
		{
			if (_socket != null)
			{
				StopReceiver();
			}

			try
			{
				// create UDP Socket
				_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message.ToString());
				// there is no need to close the socket because it was never correctly created
				_socket = null;
			}

			if (_socket == null)
			{
				return false;
			}

			try
			{
				// 绑定本地端口
				EndPoint localEp = new IPEndPoint(IPAddress.Any, m_port);
				_socket.Bind(localEp);
			}
			catch (SocketException ex)
			{
				Log.Error(ex.Message.ToString());
				_socket.Close();
				_socket = null;
				 throw ex;
			}

			if (_socket == null)
			{
				return false;
			}

			if (!RegisterReceiveOperation())
			{
				return false;
			}

			return true;
		}


		/// <summary>
		/// 接收消息操作
		/// </summary>
		/// <returns></returns>
		public bool RegisterReceiveOperation()
		{
			if (_socket == null)
			{
				return false;
			}

			try
			{
				_peerIp = new IPEndPoint(IPAddress.Any, 0);
				EndPoint ep = (EndPoint)_peerIp;
				_inBuffer = new byte[64 * 1024];
				// 接收消息
				_socket.BeginReceiveFrom(_inBuffer, 0, 64 * 1024, SocketFlags.None, ref ep, new AsyncCallback(ReceiveCallback), _socket);
			}
			catch ( Exception ex)
			{
				Log.Error(ex.Message.ToString());
				_socket.Close();
				_socket = null;
			}

			if (_socket == null)
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
			Socket sock = (Socket)result.AsyncState;

			_peerIp = new IPEndPoint(IPAddress.Any, 0);

			// 接收的数据大小
			int intLen;

			EndPoint ep = null;
			try
			{
				ep = (EndPoint)_peerIp;
				intLen = sock.EndReceiveFrom(result, ref ep);
				_peerIp = (IPEndPoint)ep;

			}
			catch (Exception ex)
			{
				Log.Error(ex.Message.ToString());
				intLen = -1;
			}

			if (_socket == null)
			{
				return;
			}

			// 数据未接收完，继续接收
			if (intLen <= 0)
			{
				RegisterReceiveOperation();
				return;
			}

			// 获取SNMP版本
			int packetVersion = SnmpPacket.GetProtocolVersion(_inBuffer, intLen);
			if (packetVersion == (int)SnmpVersion.Ver1)
			{

				 Log.Error("接收到的SNMP Trap消息版本为V1");
			}
			else if (packetVersion == (int)SnmpVersion.Ver2)
			{
				SnmpV2Packet pkt = new SnmpV2Packet();
				try
				{
					pkt.decode(_inBuffer, intLen);
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
						Log.Info(string.Format("** SNMPv2 Trap from {0}", _peerIp.ToString()));
					}
					else if (pkt.Pdu.Type == PduType.Inform) // inform
					{
						Log.Info(string.Format("** SNMPv2 Trap from {0}", _peerIp.ToString()));
					}
					else
					{
						Log.Error(string.Format("Invalid SNMPv2 packet from {0}", _peerIp.ToString()));
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
							_socket.SendTo(buf, (EndPoint)_peerIp);
						}

						// TODO: 需要修改调用方法，避免项目相互依赖
						// Trap消息处理
						//CDTSnmpMsgDispose cDTSnmpMsgDispose = new CDTSnmpMsgDispose();
						//cDTSnmpMsgDispose.OnTrap(pkt.Pdu, (IPEndPoint)ep);
					}

				}
			}

			RegisterReceiveOperation();
		}

		/// <summary>
		/// 停止Trap服务器
		/// </summary>
		public void StopReceiver()
		{
			if (_socket != null)
			{
				_socket.Close();
				_socket = null;
			}
		}

	}
}
