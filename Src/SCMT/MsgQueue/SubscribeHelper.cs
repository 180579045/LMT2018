using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using LogManager;

/// <summary>
/// warnning: 可能会有性能限制，毕竟是在一个线程中
/// </summary>
namespace MsgQueue
{
	public class SubscribeHelper : IDisposable
	{
		private readonly SubscribeClient subClient;

		#region 构造、析构

		public SubscribeHelper()
		{
			subClient = new SubscribeClient();
			subClient.Run();
		}

		~SubscribeHelper()
		{
			subClient.Dispose();
		}

		public void Dispose()
		{

		}

		public static SubscribeHelper GetInstance()
		{
			return Singleton<SubscribeHelper>.GetInstance();
		}

		#endregion

		#region 公共接口

		public static bool AddSubscribe(string topic, HandlerSubscribeMsg handler,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			Log.Debug($"{memeberName} call this func, subscribe {topic}");

			return GetInstance().SubscribeTopic(topic, handler);
		}

		public static bool CancelSubscribe(string topic,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			Log.Debug($"{memeberName} call this func, unsubscribe {topic}");
			return GetInstance().SubScribeCancel(topic);
		}

		#endregion

		#region 私有接口

		private bool SubscribeTopic(string topic, HandlerSubscribeMsg handler)
		{
			subClient.AddSubscribeTopic(topic, handler);
			return true;
		}

		private bool SubScribeCancel(string topic)
		{
			subClient.CancelSubscribeTopic(topic);
			return true;
		}

		#endregion
	}
}
