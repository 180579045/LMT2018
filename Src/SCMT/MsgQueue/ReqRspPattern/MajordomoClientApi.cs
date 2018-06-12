using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace MsgQueue
{
	// client api
	public class MajordomoClient : IDisposable
	{
		// Our context
		readonly ZContext _context;

		// Majordomo broker
		public string Broker { get; protected set; }

		//  Socket to broker
		public ZSocket Client { get; protected set; }

		//  Print activity to console
		public bool Verbose { get; protected set; }

		//  Request timeout
		public TimeSpan Timeout { get; protected set; }

		//  Request retries
		public int Retries { get; protected set; }

		// 连接到 broker
		public void ConnectToBroker()
		{
			Client = new ZSocket(_context, ZSocketType.REQ);
			Client.Connect(Broker);

			if (Verbose)
			{
				"I: connecting to broker at '{0}'…".DumpString(Broker);
			}
		}

		public MajordomoClient(string broker, bool verbose)
		{
			if (broker == null)
			{
				throw new InvalidOperationException();
			}
			_context = new ZContext();
			Broker = broker;
			Verbose = verbose;
			Timeout = TimeSpan.FromMilliseconds(2500);
			Retries = 3;

			ConnectToBroker();
		}

		~MajordomoClient()
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

				if (Client != null)
				{
					Client.Dispose();
					Client = null;
				}
				////Do not Dispose Context: cuz of weird shutdown behavior, stucks in using calls //
			}
		}

		// 设置重试间隔
		public void Set_Timeout(int timeoutInMs)
		{
			Timeout = TimeSpan.FromMilliseconds(timeoutInMs);
		}

		// 设置重试次数
		public void Set_Retries(int retries)
		{
			Retries = retries;
		}

		//  Here is the send method. It sends a request to the broker and gets
		//  a reply even if it has to retry several times. It takes ownership of
		//  the request message, and destroys it when sent. It returns the reply
		//  message, or NULL if there was no reply after multiple attempts://
		public ZMessage Send(string service, ZMessage request, CancellationTokenSource cancellor)
		{
			if (request == null)
			{
				throw new InvalidOperationException();
			}

			//  Prefix request with protocol frames
			//  Frame 1: "MDPCxy" (six bytes, MDP/Client x.y)
			//  Frame 2: Service name (printable string)
			request.Prepend(new ZFrame(service));
			request.Prepend(new ZFrame(MdpCommon.MDPC_CLIENT));

			if (Verbose)
			{
				request.DumpZmsg("I: send request to '{0}' service:", service);
			}

			int retriesLeft = Retries;
			while (retriesLeft > 0 && !cancellor.IsCancellationRequested)
			{
				if (cancellor.IsCancellationRequested || 
				    (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
					_context.Shutdown();

				// Copy the Request and send on Client
				ZMessage msgreq = request.Duplicate();

				ZError error;
				if (!Client.Send(msgreq, out error))
				{
					if (Equals(error, ZError.ETERM))
					{
						cancellor.Cancel();
						break;
					}
				}

				var p = ZPollItem.CreateReceiver();
				ZMessage msg;

				//  On any blocking call, libzmq will return -1 if there was
				//  an error; we could in theory check for different error codes,
				//  but in practice it's OK to assume it was EINTR (Ctrl-C):

				// Poll the client Message
				if (Client.PollIn(p, out msg, out error, Timeout))
				{
					//  If we got a reply, process it
					if (Verbose)
					{
						msg.DumpZmsg("I: received reply");
					}

					if (msg.Count < 3)
					{
						throw new InvalidOperationException();
					}

					using (ZFrame header = msg.Pop())
					{
						if (!header.ToString().Equals(MdpCommon.MDPC_CLIENT))
						{
							throw new InvalidOperationException();
						}
					}

					using (ZFrame replyService = msg.Pop())
					{
						if (!replyService.ToString().Equals(service))
						{
							throw new InvalidOperationException();
						}
					}

					request.Dispose();
					return msg;
				}
				else if (Equals(error, ZError.ETERM))
				{
					cancellor.Cancel();
					break;
				}
				else if (Equals(error, ZError.EAGAIN))
				{
					if (--retriesLeft > 0)
					{
						if (Verbose)
						{
							"W: no reply, reconnecting…".DumpString();
						}

						ConnectToBroker();
					}
					else
					{
						if (Verbose)
						{
							"W: permanent error, abandoning".DumpString();
						}

						// TODO 这种情况要通知client知道

						break;
					}
				}
			}

			if (cancellor.IsCancellationRequested)
			{
				"W: interrupt received, killing client…\n".DumpString();
			}

			request.Dispose();

			return null;
		}
	}

	// client api 用法
	public class ClientApiTest
	{
		public void Test()
		{
			CancellationTokenSource cancellor = new CancellationTokenSource();
			Console.CancelKeyPress += (s, ea) =>
			{
				ea.Cancel = true;
				cancellor.Cancel();
			};

			using (MajordomoClient session = new MajordomoClient("tcp://127.0.0.1:5555", true))
			{
				ZMessage request = new ZMessage();
				request.Add(new ZFrame("getTreeNameByOid"));
				request.Add(new ZFrame("1.1.1.1.1"));

				ZMessage reply = session.Send("redis", request, cancellor);
				if (reply != null)
				{
					var replycode = reply[0].ToString();
					"Loopup echo service: {0}\n".DumpString(replycode);
					reply.Dispose();
				}
				else
					"E: no response from broker, make sure it's running\n".DumpString();
			}
		}
	}
}
