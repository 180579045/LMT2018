using System;
using System.Threading.Tasks;
using CommonUtility;
using ZeroMQ;

namespace MsgQueue
{
	/// <summary>
	/// 用一个Proxy实例，同时运行PublishServer和SubcribeServer
	/// 很多模块既要publish数据，也要subcribe其他模块的数据。这个服务器进程相当于中转站
	/// </summary>
	public class PubSubServer : Singleton<PubSubServer>, IDisposable
	{
		private PubSubServer()
		{
			
		}

		~PubSubServer()
		{
			Dispose(false);
		}

		public ZContext context { get; private set; }	// inproc协议要求所有的socket使用同一个一个context对象
		private ZSocket pubSocket;
		private ZSocket subSocket;

		public void InitServer()
		{
			if (HadInited) return;

			context = ZContext.Create();

			pubSocket = new ZSocket(context, ZSocketType.XPUB);
			subSocket = new ZSocket(context, ZSocketType.XSUB);

			pubSocket.Bind(CommonPort.PubBus);
			subSocket.Bind(CommonPort.SubBus);

			Task.Factory.StartNew(StartProxy);
		}

		private void StartProxy()
		{
			HadInited = true;
			ZContext.Proxy(subSocket, pubSocket);
		}

		public void Stop()
		{
			if (!HadInited) return;

			context.Shutdown();

			HadInited = false;
		}

		public bool HadInited { get; private set; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				pubSocket?.Dispose();
				subSocket?.Dispose();
				context?.Dispose();
			}
		}
	}
}
