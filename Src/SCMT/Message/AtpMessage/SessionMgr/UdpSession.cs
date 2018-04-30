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

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="target">目的信息，包括地址和端口号</param>
		public UdpSession(Target target)
		{
			_udpClient = new UdpClient();
			_ipTargetEp = new IPEndPoint(IPAddress.Parse(target.addr), target.port);
			_udpClient.Connect(_ipTargetEp);

			_prefix = $"{target.addr}:{target.port}";       //订阅这个消息是用于运行过程中给板卡发送信息
			_subClient = new SubscribeClient(CommonPort.PubServerPort);
			_subClient.AddSubscribeTopic($"to:{_prefix}", OnSubscribe);
		}

		/// <summary>
		/// 异步发送信息回调函数
		/// </summary>
		/// <param name="ar"></param>
		public void SendCallback(IAsyncResult ar)
		{
			MsgSendCompleted = ar.IsCompleted;
		}

		/// <summary>
		/// 异步发送数据
		/// </summary>
		/// <param name="dataBytes"></param>
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

		/// <summary>
		/// 启动线程接收数据
		/// </summary>
		public void Run()
		{
			_recvThread = new Thread(RecvFromBoard);
			_recvThread.Start();
			_subClient.Run();
		}

		/// <summary>
		/// 接收数据线程函数
		/// </summary>
		/// <param name="obj"></param>
		private void RecvFromBoard(object obj)
		{
			while(true)
			{
				try
				{
					var revBytes = _udpClient.Receive(ref _ipTargetEp);
					var header = GetHeaderFromBytes.GetHeader(revBytes);
					PublishHelper.PublishMsg($"from:{_prefix}", revBytes);
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
