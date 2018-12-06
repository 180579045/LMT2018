using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeroMQ;
using LogManager;

namespace MsgQueue
{
	public delegate void HandlerSubscribeMsg(SubscribeMsg msg);

	public class SubscribeMsg
	{
		public string Topic { get; }

		public byte[] Data { get; }

		public SubscribeMsg(byte[] data, string topic)
		{
			Topic = topic;
			Data = data;
		}
	}

	/// <summary>
	/// 使用ZeroMQ实现的订阅者客户端
	/// </summary>
	public class SubscribeClient : IDisposable
	{
		public ZSocket Client { get; protected set; }

		public string PublishServer { get; protected set; }

		public SubscribeClient(string pubServer = CommonPort.PubBus)
		{
			_dictionaryTopicHandlers = new Dictionary<string, List<HandlerSubscribeMsg> >();
			PublishServer = pubServer;

			ConnectToPubServer();
		}

		~SubscribeClient()
		{
			Dispose(false);
		}

		public void ConnectToPubServer()
		{
			Client = new ZSocket(PubSubServer.GetInstance().context, ZSocketType.SUB);
			Client.Connect(PublishServer);
		}

		/// <summary>
		/// 设置订阅消息topic，还要有回调函数，在此处触发一个事件
		/// </summary>
		/// <param name="topic">订阅的主题</param>
		/// <param name="handler">消息处理函数</param>
		/// <returns>true:增加订阅成功；false:订阅失败</returns>
		public bool AddSubscribeTopic(string topic, HandlerSubscribeMsg handler)
		{
			if (_dictionaryTopicHandlers.ContainsKey(topic))
			{
				var handlerList = _dictionaryTopicHandlers[topic];
				handlerList.Add(handler);
			}
			else
			{
				var handlerList = new List<HandlerSubscribeMsg>();
				handlerList.Add(handler);
				_dictionaryTopicHandlers.Add(topic, handlerList);
				Client.Subscribe(topic);
			}

			return true;
		}

		/// <summary>
		/// 取消订阅的topic
		/// </summary>
		/// <param name="topic">待取消的主题</param>
		/// <returns></returns>
		public bool CancelSubscribeTopic(string topic)
		{
			if (!_dictionaryTopicHandlers.ContainsKey(topic)) return false;

			// TODO 这里还需要其他参数，唯一确定一个对象的handler
			_dictionaryTopicHandlers.Remove(topic);
			Client.Unsubscribe(topic);
			return true;
		}

		/// <summary>
		/// 启动任务开始监听订
		/// </summary>
		public void Run()
		{
			if (_running) return;

			_stop = false;
			_running = true;
			Task.Factory.StartNew(RecvMessage);
		}

		public void Stop()
		{
			_stop = true;
		}

		/// <summary>
		/// 接收消息函数
		/// msg[0] is topic, msg[1] is message content
		/// </summary>
		private void RecvMessage()
		{
			ZMessage msg;
			ZError error;
			while (!_stop)
			{
				try
				{
					if (null == (msg = Client.ReceiveMessage(out error)))
					{
						if (error == ZError.ETERM)
							break; // Interrupted

						Log.Error($"[MQ] {error}");
						break;
					}
				}
				catch (ObjectDisposedException e)
				{
					Log.Error($"[MQ] ObjectDisposed Execption {e}");
					break;
				}
				catch(Exception e)
				{
					Log.Error($"[MQ] {e}");
					break;
				}

				using (msg)
				{
					var topic = msg[0].ReadString();
					var msgBody = msg[1].Read();

					var handlers = GetTopicHandler(topic);
					if (null == handlers)
						continue;

					foreach (var handler in handlers)
					{
						handler?.Invoke(new SubscribeMsg(msgBody, topic));
					}
				}
			}
		}

		private List<HandlerSubscribeMsg> GetTopicHandler(string topic)
		{
			return _dictionaryTopicHandlers.ContainsKey(topic) ? _dictionaryTopicHandlers[topic] : null;
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
				if (Client != null)
				{
					Client.Dispose();
					Client = null;
				}
				_dictionaryTopicHandlers?.Clear();
			}
		}

		// 同一个topic，可能会有多个订阅者
		private readonly Dictionary<string, List<HandlerSubscribeMsg> > _dictionaryTopicHandlers;
		private bool _running;
		private bool _stop;
	}
}
