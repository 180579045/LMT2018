using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using MsgQueue;


namespace AtpMessage.SessionMgr
{
	/// <summary>
	/// 负责Tcp连接的夹层业务流程的类型;
	/// </summary>
	internal class TcpSession : IASession
	{
		private TcpClient tcpClient { get; set; }               // Tcp连接会话;
		private IPEndPoint targetEp;

		private Thread recvThread;

		public TcpSession(Target target)
		{
			targetEp = new IPEndPoint(IPAddress.Parse(target.raddr), target.rport);
			tcpClient = new TcpClient(targetEp);
		}


		public void SendAsync(byte[] dataBytes)
		{
			
		}

		public bool Init(string lip)
		{
			bool bSucceed = true;
			try
			{
				tcpClient.Connect(targetEp);
				recvThread = new Thread(RecvDataFunc);
				recvThread.Start();

				//启动接收线程
			}
			catch (SocketException e)
			{
				//TODO 连接异常处理
				Console.WriteLine(e.SocketErrorCode);
				bSucceed = false;
			}

			return bSucceed;
		}

		public bool Stop()
		{
			tcpClient.Close();
			recvThread.Join();
			return true;
		}

		private void RecvDataFunc()
		{
			NetworkStream stream = null;
			while (true)
			{
				try
				{
					stream = tcpClient.GetStream();

				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}
	}
}
