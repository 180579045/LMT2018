using System;
using MsgQueue;

namespace AtpMessage.LinkMgr
{
	public abstract class NeLinkBase : INetElementLink
	{
		public enum LinkState
		{
			Connecting,
			Connencted,
			Disconnected,
		}

		public LinkState State { get; protected set; } = LinkState.Disconnected;

		public bool IsBreak => LinkState.Disconnected == State;

		private NetElementConfig _netElementConfig;

		/// <summary>
		/// 连接网元。此处仅考虑ATP的连接
		/// </summary>
		/// <param name="netElementAddress">网元配置信息</param>
		/// <param name="isRepeatConnect">是否重复连接</param>
		public void Connect(NetElementConfig netElementAddress, bool isRepeatConnect = true)
		{
			if (null == netElementAddress)
			{
				throw new ArgumentNullException("netElementAddress is null");
			}

			//如果已经连接或者连接中，就不再处理
			if (IsBreak)
			{
				Logon(netElementAddress);
			}
		}

		public void Disconnect()
		{
			Logoff(_netElementConfig);
		}

		public bool IsConnected()
		{
			return LinkState.Connencted == State;
		}

		/// <summary>
		/// 登录板卡。分为直连板卡登录和非直连板卡登录
		/// 直连板卡很简单，只要构造报文即可
		/// 非直连还需要通知建链等操作
		/// </summary>
		public virtual void Logon(NetElementConfig netElementAddress)
		{
			State = LinkState.Connecting;
			_netElementConfig = netElementAddress;
		}

		/// <summary>
		/// 断开ATP的连接。虚函数，直连和非直连操作不一样
		/// </summary>
		public virtual void Logoff(NetElementConfig netElementAddress)
		{
			State = LinkState.Disconnected;
		}

		public int SendPackets(byte[] dataByteses)
		{
			PublishHelper.PublishMsg("topic", dataByteses);
			return dataByteses.Length;
		}
	}
}
