using System;
using ZeroMQ;

namespace MsgQueue
{
	/// <summary>
	/// publish客户端。不想用全局的实例，或者要发布到其他端口，使用这个类
	/// </summary>

	public class PublisherClient : IDisposable
	{
		private ZSocket Client;

		#region 构造、析构

		public PublisherClient(string subServer = CommonPort.SubBus)
		{
			Client = new ZSocket(PubSubServer.GetInstance().context, ZSocketType.PUB);
			Client.Connect(subServer);
		}

		~PublisherClient()
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
			}
		}

		#endregion

		#region 公共接口

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="topic">发送消息对应的主题</param>
		/// <param name="msg">消息内容</param>
		public void PublishMsg(string topic, string msg)
		{
			Client.SendMore(new ZFrame(topic));
			Client.SendFrame(new ZFrame(msg));
		}

		/// <summary>
		/// 发布消息。重载函数
		/// </summary>
		/// <param name="topic">发送消息对应的主题</param>
		/// <param name="msg">消息内容</param>
		public void PublishMsg(string topic, byte[] msg)
		{
			Client.SendMore(new ZFrame(topic));
			Client.SendFrame(new ZFrame(msg));
		}

		public void PublishMsg(string topic, byte[] msg, string option)
		{
			PublishMsg(topic, msg);
			Client.SendFrame(new ZFrame(option));
		}

		#endregion


	}
}
