using System;
using System.Runtime.CompilerServices;
using CommonUtility;
using LogManager;

namespace MsgQueue
{
	/// <summary>
	/// 公共的发布消息助手
	/// </summary>
	public class PublishHelper : Singleton<PublishHelper>, IDisposable
	{
		private readonly PublisherClient _pubClient;

		#region 构造、析构

		private PublishHelper()
		{
			_pubClient = new PublisherClient();
		}

		~PublishHelper()
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
				_pubClient?.Dispose();
			}
		}

		#endregion

		#region 公共接口

		public void Publish(string topic, string msg)
		{
			_pubClient.PublishMsg(topic, msg);
		}

		public void Publish(string topic, byte[] msgBytes)
		{
			_pubClient.PublishMsg(topic, msgBytes);
		}

		public void Publish(string topic, byte[] msgBytes, string option)
		{
			_pubClient.PublishMsg(topic, msgBytes, option);
		}

		public static void PublishMsg(string topic, string msg,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			Log.Debug($"{memeberName} call this func, msg topic: {topic}, body: {msg}");
			GetInstance().Publish(topic, msg);
		}

		public static void PublishMsg(string topic, byte[] msgBytes,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			Log.Debug($"{memeberName} call this func, msg topic: {topic}, body: {BitConverter.ToString(msgBytes)}");
			GetInstance().Publish(topic, msgBytes);
		}

		// 扩展，增加扩展的信息，一般是IP地址
		public static void PublishMSg(string topic, byte[] msgBytes, string option,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			Log.Debug($"{memeberName} call this func, msg topic: {topic}, body: {BitConverter.ToString(msgBytes)}, option: {option}");
			GetInstance().Publish(topic, msgBytes, option);
		}

		#endregion
	}
}
