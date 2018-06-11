using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace MsgQueue
{
	//
	//  管家模式 worker api 
	//  Implements the MDP/Worker spec at http://rfc.zeromq.org/spec:7.

	public class MajordomoWorker : IDisposable
	{
		private readonly ZContext _context;

		// broker的地址。比如：tcp://*:5555
		public string Broker { get; protected set; }

		// 对外提供的服务名
		public string Service { get; protected set; }

		// Socket to broker
		public ZSocket Worker { get; protected set; }

		// 是否打印调试信息
		public bool Verbose { get; protected set; }

		#region 心跳管理

		// When to send HEARTBEAT
		public DateTime HeartbeatAt { get; protected set; }

		// How many attempts left
		public UInt64 Liveness { get; protected set; }

		// Heartbeat delay, msecs
		public TimeSpan Heartbeat { get; protected set; }

		// Reconnect delay, msecs
		public TimeSpan Reconnect { get; protected set; }

		#endregion

		#region Unknown to port

		// Zero only at start
		private bool _expectReply;

		// Return identity, if any
		private ZFrame _replyTo;

		#endregion

		//发送消息到broker
		public void SendToBroker(string command, string option, ZMessage msg)
		{
			using (msg = msg != null ? msg.Duplicate() : new ZMessage())
			{
				if (!string.IsNullOrEmpty(option))
				{
					msg.Prepend(new ZFrame(option));
				}

				msg.Prepend(new ZFrame(command));
				msg.Prepend(new ZFrame(MdpCommon.MDPW_WORKER));
				msg.Prepend(new ZFrame(string.Empty));

				if (Verbose)
				{
					msg.DumpZmsg("I: sending '{0:X}|{0}' to broker", command.ToMdCmd());
				}

				Worker.Send(msg);
			}
		}

		//连接到broker
		public void ConnectToBroker()
		{
			Worker = new ZSocket(_context, ZSocketType.DEALER);
			Worker.Connect(Broker);

			if (Verbose)
			{
				"I: connecting to broker at {0}…".DumpString(Broker);
			}

			// 注册服务到broker
			SendToBroker(MdpCommon.MdpwCmd.READY.ToHexString(), Service, null);

			// Liveness = 0 时，将会断开到broker的连接
			Liveness = MdpCommon.HEARTBEAT_LIVENESS;
			HeartbeatAt = DateTime.UtcNow + Heartbeat;
		}

		/// <summary>
		/// worker的构造函数。一般是谁提供服务，谁构造持有该对象
		/// </summary>
		/// <param name="broker">broker的地址</param>
		/// <param name="service">提供的服务名</param>
		/// <param name="verbose">是否打印调试信息。在控制台可以用</param>
		public MajordomoWorker(string broker, string service, bool verbose)
		{
			if (broker == null)
				throw new InvalidOperationException();

			if (service == null)
				throw new InvalidOperationException();

			_context = new ZContext();
			Broker = broker;
			Service = service;
			Verbose = verbose;
			Heartbeat = MdpCommon.HEARTBEAT_DELAY;
			Reconnect = MdpCommon.RECONNECT_DELAY;

			ConnectToBroker();
		}

		// 析构函数
		~MajordomoWorker()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Destructor

				if (Worker != null)
				{
					Worker.Dispose();
					Worker = null;
				}
				//Do not Dispose Context: cuz of weird shutdown behavior, stucks in using calls
			}
		}

		// 以下两个方法是用于心跳控制的，可以根据网络状况进行调整

		// 设置心跳间隔
		public void Set_Heartbeat(int heartbeatInMs)
		{
			Heartbeat = TimeSpan.FromMilliseconds(heartbeatInMs);
		}

		// 设置重连间隔
		public void Set_Reconnect(int reconnectInMs)
		{
			Reconnect = TimeSpan.FromMilliseconds(reconnectInMs);
		}

		// 接收broker发送的消息
		public ZMessage Recv(ZMessage reply , CancellationTokenSource cancellor)
		{
			if (reply == null && _expectReply)
				throw new InvalidOperationException();

			if (reply != null)
			{
				if (_replyTo == null)
					throw new InvalidOperationException();
				reply.Wrap(_replyTo);
				SendToBroker(MdpCommon.MdpwCmd.REPLY.ToHexString(), string.Empty, reply);
			}

			_expectReply = true;

			while (true)
			{
				if (cancellor.IsCancellationRequested
					|| (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
					_context.Shutdown();

				var p = ZPollItem.CreateReceiver();
				ZMessage msg;
				ZError error;

				if (Worker.PollIn(p, out msg, out error, Heartbeat))
				{
					using (msg)
					{
						if (Verbose)
							msg.DumpZmsg("I: received message from broker:");

						Liveness = MdpCommon.HEARTBEAT_LIVENESS;

						// Don't try to handle errors, just assert noisily
						if (msg.Count < 3)
						{
							throw new InvalidOperationException();	//TODO 自己崩溃真的合适吗？
						}

						using (ZFrame empty = msg.Pop())
						{
							if (!empty.ToString().Equals(""))
							{
								throw new InvalidOperationException();
							}
						}

						using (ZFrame header = msg.Pop())
						{
							if (!header.ToString().Equals(MdpCommon.MDPW_WORKER))
							{
								throw new InvalidOperationException();
							}
						}

						//操作
						using (ZFrame command = msg.Pop())
						{
							if (command.StrHexEq(MdpCommon.MdpwCmd.REQUEST))
							{
								//  We should pop and save as many addresses as there are
								//  up to a null part, but for now, just save one…
								_replyTo = msg.Unwrap();

								// 把收到的消息返回给调用者，解析消息进行处理，完成后返回给
								return msg.Duplicate();
							}
							else if (command.StrHexEq(MdpCommon.MdpwCmd.HEARTBEAT))
							{
								// 收到心跳包，不需要处理
							}
							else if (command.StrHexEq(MdpCommon.MdpwCmd.DISCONNECT))
							{
								ConnectToBroker();
							}
							else
								"E: invalid input message: '{0}'".DumpString(command.ToString());
						}
					}
				}
				else if (Equals(error, ZError.ETERM))
				{
					cancellor.Cancel();
					break;
				}
				else if (Equals(error, ZError.EAGAIN) && --Liveness == 0)
				{
					if (Verbose)
						"W: disconnected from broker - retrying…".DumpString();

					Thread.Sleep(Reconnect);	//等待几秒钟后重试
					ConnectToBroker();
				}

				// 发送心跳包
				if (DateTime.UtcNow > HeartbeatAt)
				{
					SendToBroker(MdpCommon.MdpwCmd.HEARTBEAT.ToHexString(), null, null);
					HeartbeatAt = DateTime.UtcNow + Heartbeat;
				}
			}

			if (cancellor.IsCancellationRequested)
				"W: interrupt received, killing worker…\n".DumpString();

			return null;
		}
	}

	//以下是worker api的用法
	public class WorkerApiTest
	{
		public void Test()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			Console.CancelKeyPress += (s, ea) =>
			{
				ea.Cancel = true;
				cts.Cancel();
			};

			using (MajordomoWorker session = new MajordomoWorker("tcp://127.0.0.1:5555", "redis", true))
			{
				ZMessage reply = null;
				while (true)
				{
					ZMessage request = session.Recv(reply, cts);
					if (request == null)
						break;              // worker was interrupted

					// request 就是 client 传递的要做的事情
					var first = request.Pop().ToString();       //cmd
					var second = request.Pop().ToString();      //paramter

					reply = new ZMessage { new ZFrame("this is tree name") };

					//reply = request;		// 实际使用中，reply 需要填写
				}
			}
		}
	}
}
