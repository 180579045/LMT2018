namespace MsgQueue
{
	/// <summary>
	/// 消息队列使用的端口号
	/// </summary>
	public class CommonPort
	{
		public const int PubServerPort = 1234;
		public const int SubServerPort = 5678;

		public const string PubBus = "inproc://publish-bus";	//使用inproc协议，加速消息的收发处理
		public const string SubBus = "inproc://subscribe-bus";

		public const int AtpLinkPort = 5002;
	}
}
