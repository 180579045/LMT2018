using LogManager;
using MsgQueue;
using SCMTOperationCore.Connection;
using SCMTOperationCore.Connection.Tcp;
using SCMTOperationCore.Elements.BaseElement;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DataReceivedEventArgs = SCMTOperationCore.Connection.DataReceivedEventArgs;

namespace SCMTOperationCore.Elements
{
	/// <summary>
	/// Si元素的基类,可拓展诸如NodeB这类的基于SI消息的网元;
	/// </summary>
	public class SiElement : Element, IElement
	{
		#region 构造、析构

		/// <summary>
		/// 构造函数。后面追加别的参数
		/// </summary>
		/// <param name="friendName">友好名。可查可改</param>
		/// <param name="neIp">网元IP，可查可改</param>
		/// <param name="nePort">网元端口，可查可改。默认为5000端口</param>
		public SiElement(string friendName, IPAddress neIp, ushort nePort = 5000)
		: base(friendName, neIp, nePort)
		{
			var ipMode = (neIp.AddressFamily == AddressFamily.InterNetwork) ? IPMode.IPv4 : IPMode.IPv6;
			_targetPoint = new NetworkEndPoint(NeAddress, nePort, ipMode);
			connection = new TcpConnection(_targetPoint);
			connection.DataReceived += OnDataReceived;
			connection.Disconnected += OnDisconnected;
			connection.Connected += OnConnected;

			dealer = new SiMsgDealer(neIp.ToString());
			_cancelConnectFlag = false;
		}

		~SiElement()
		{
			DisConnect();
		}

		#endregion 构造、析构

		#region 公共接口

		//连接网元。
		public override async Task ConnectAsync()
		{
			try
			{
				SetCancelConnectFlag(false);
				if (HasConnected())
				{
					var chose = MessageBox.Show("当前设备已连接，是否断开重连？", "重连确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
					if (MessageBoxResult.No == chose)
					{
						return;
					}

					await DisConnect();
				}

				await Task.Factory.StartNew(() =>
				{
					while (!IsCancelConnect() && !HasConnected())
					{
						try
						{
							if (!connection.Connect())
							{
								Log.Debug("连接基站失败，1s后重连...");
								Task.Delay(1000);
							}
						}
						catch
						{
							// ignored
							Task.Delay(1000);
						}
					}
				});
				//连接成功处理事件
			}
			catch (Exception e)     // 连接失败，会有异常抛出，需要处理连接断开处理事件
			{
				Log.Error(e.Message);
			}

			//收到数据处理事件
		}

		//断开连接，在OnDisconnected中处理事件
		public override async Task DisConnect()
		{
			SetCancelConnectFlag(true);
			await Task.Factory.StartNew(() => connection.Close());
		}

		//发送数据
		public bool SendSiMsg(byte[] msgBytes)
		{
			try
			{
				if (HasConnected())
				{
					connection.SendBytes(msgBytes);
					return true;
				}
			}
			catch (Exception e)         //发送失败会throw异常，需要处理
			{
				Console.WriteLine(e);
				throw;
			}

			return false;
		}

		public bool HasConnected()
		{
			return (ConnectState == ConnectionState.Connected);
		}

		#endregion 公共接口

		#region 私有方法

		private void OnConnected(object sender, ConnectedEventArgs e)
		{
		}

		private void OnDisconnected(object sender, DisconnectedEventArgs e)
		{
			PublishHelper.PublishMsg(TopicHelper.EnbOfflineMsg, NeAddress.ToString());

			// 断开连接了，但不是手动取消的，应该是非自然断开
			if (!IsCancelConnect())
			{
				PublishHelper.PublishMsg(TopicHelper.ReconnectGnb, NeAddress.ToString());
			}
		}

		private void OnDataReceived(object sender, DataReceivedEventArgs e)
		{
			dealer.DealSiMsg(e.Bytes);
		}

		private bool IsCancelConnect()
		{
			lock (_lockObj)
			{
				return _cancelConnectFlag;
			}
		}

		private void SetCancelConnectFlag(bool bCancel)
		{
			lock (_lockObj)
			{
				_cancelConnectFlag = bCancel;
			}
		}

		#endregion 私有方法

		#region 私有成员、属性

		//---------------------------------属性区---------------------------------
		public ConnectionState ConnectState => connection.State;

		private NetworkConnection connection;

		private readonly SiMsgDealer dealer;

		private NetworkEndPoint _targetPoint;
		private bool _cancelConnectFlag;
		private static object _lockObj = new object();

		#endregion 私有成员、属性
	}
}