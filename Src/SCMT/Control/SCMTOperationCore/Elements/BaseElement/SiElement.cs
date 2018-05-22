using System;
using System.Diagnostics;
using System.Net;
using SCMTOperationCore.Connection;
using SCMTOperationCore.Connection.Tcp;
using SCMTOperationCore.Elements.BaseElement;
using DataReceivedEventArgs = SCMTOperationCore.Connection.DataReceivedEventArgs;

namespace SCMTOperationCore.Elements
{
	/// <summary>
	/// Si元素的基类,可拓展诸如NodeB这类的基于SI消息的网元;
	/// </summary>
	public class SiElement : Element, IElement
	{
		/// <summary>
		/// 构造函数。后面追加别的参数
		/// </summary>
		/// <param name="friendName">友好名。可查可改</param>
		/// <param name="neIp">网元IP，可查可改</param>
		/// <param name="nePort">网元端口，可查可改。默认为5000端口</param>
		public SiElement(string friendName, IPAddress neIp, ushort nePort = 5000)
		: base(friendName, neIp, nePort)
		{
			NetworkEndPoint ep = new NetworkEndPoint(NeAddress, nePort, IPMode.IPv4);
			connection = new TcpConnection(ep);
			dealer = new SiMsgDealer();
		}

		~SiElement()
		{
			DisConnect();
		}

		//连接网元。
		public override void Connect()
		{
			try
			{
				connection.DataReceived += OnDataReceived;
				connection.Disconnected += OnDisconnected;
				connection.Connected += OnConnected;

				connection.Connect();
			}
			catch (Exception e)		//TODO 连接失败，会有异常抛出，需要处理
			{
				Console.WriteLine(e);
				throw;
			}
		}

		//连接成功处理事件
		private void OnConnected(object sender, ConnectedEventArgs e)
		{
			Debug.WriteLine("connect server succeed");
		}

		//连接断开处理事件
		private void OnDisconnected(object sender, DisconnectedEventArgs e)
		{
			Debug.WriteLine("disconnect server succeed");
		}

		//收到数据处理事件
		private void OnDataReceived(object sender, DataReceivedEventArgs e)
		{
			dealer.DealSiMsg(e.Bytes);
		}

		//断开连接，在OnDisconnected中处理事件
		public override void DisConnect()
		{
			connection.Close();
		}

		//发送数据
		public bool SendSiMsg(byte[] msgBytes)
		{
			try
			{
				connection.SendBytes(msgBytes);
				return true;
			}
			catch (Exception e)			//TODO 发送失败会throw异常，需要处理
			{
				Console.WriteLine(e);
				//throw;
			}

			return false;
		}


		public bool HasConnected()
		{
			return (ConnectState == ConnectionState.Connected);
		}

		//---------------------------------属性区---------------------------------
		public ConnectionState ConnectState => connection.State;

		private readonly NetworkConnection connection;

		private SiMsgDealer dealer;
	}

	
}
