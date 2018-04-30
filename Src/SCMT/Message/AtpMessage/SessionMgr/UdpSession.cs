using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AtpMessage.MsgDefine;
using MsgQueue;

namespace AtpMessage.SessionMgr
{
	public class Target : IComparable
	{
		public string addr;
		public int port;

		public int CompareTo(object obj)
		{
			var temp = obj as Target;
			if (null == temp)
			{
				return -1;
			}

			if (temp.addr == addr && temp.port == port)
			{
				return 0;
			}

			return 1;
		}
	}

	/// <summary>
	/// atp和gtsa模式通信使用的是udp协议
	/// </summary>
	internal class UdpSession
	{
		private readonly UdpClient _udpClient;
		private IPEndPoint _ipTargetEp;

		private readonly SubscribeClient _subClient;
		private readonly string _prefix;

		private Thread _recvThread;

		private bool MsgSendCompleted { get; set; }

		public UdpSession(Target target)
		{
			_udpClient = new UdpClient();
			_ipTargetEp = new IPEndPoint(IPAddress.Parse(target.addr), target.port);
			_udpClient.Connect(_ipTargetEp);

			_prefix = $"{target.addr}-{target.port}";
			_subClient = new SubscribeClient(CommonPort.PubServerPort);
			_subClient.AddSubscribeTopic(_prefix, OnSubscribe);
		}

		public void SendCallback(IAsyncResult ar)
		{
			MsgSendCompleted = ar.IsCompleted;
		}

		public void SendAsync(byte[] dataBytes)
		{
			MsgSendCompleted = false;
			try
			{
				_udpClient.BeginSend(dataBytes, dataBytes.Length, SendCallback, _udpClient);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			while (!MsgSendCompleted)
			{
				Thread.Sleep(100);
			}
		}

		public void Run()
		{
			_recvThread = new Thread(RecvFromBoard);
			_recvThread.Start();
			_subClient.Run();
		}

		private void RecvFromBoard(object obj)
		{
			while (true)
			{
				try
				{
					var revBytes = _udpClient.Receive(ref _ipTargetEp);
					var header = GetHeaderFromBytes.GetHeader(revBytes);
					//PublishHelper.PublishMsg($"{_prefix}-{header.u16Opcode}", revBytes);
				}
				catch (SocketException e)
				{
					if (SocketError.Interrupted == e.SocketErrorCode)
					{
						break;
					}
				}
			}
		}

		private void OnSubscribe(SubscribeMsg msg)
		{
			SendAsync(msg.Data);
		}

		public void Dispose()
		{
			_udpClient?.Close();
			_recvThread?.Join(100);
			_subClient?.Stop();
			_subClient?.Dispose();
		}
	}
}
