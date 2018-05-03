using System;
using System.Collections.Generic;
using AtpMessage.MsgDefine;
using CommonUility;
using MsgQueue;

namespace AtpMessage.LinkMgr
{
	/// <summary>
	/// 连接管理。考虑到后面可能同时连接多个基站抄消息，所以需要保存对应的连接。
	/// TODO 前台是否限制了一个IP只能对应一个网元？
	/// TODO ipSession可能无法建立对应的map，需要更多的信息
	/// </summary>
	public class LinkMgrActor
	{
		/// <summary>
		/// 获取LinkMgrActor的实例。单例。
		/// </summary>
		/// <returns></returns>
		public static LinkMgrActor GetInstance()
		{
			return Singleton<LinkMgrActor>.GetInstance();
		}

		public LinkMgrActor()
		{
			_mapNetElementLinks = new Dictionary<string, INetElementLink>();

			_subscribeClient = new SubscribeClient(CommonPort.PubServerPort);
			_subscribeClient.AddSubscribeTopic("/LinkMgr", OnLinkMgr);
			_subscribeClient.Run();
		}

		/// <summary>
		/// 建立网元连接。TODO 如果是其他连接，topic还需要修改
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="neConfig"></param>
		public bool ConnectNetElement(string ip, NetElementConfig neConfig)
		{
			if (string.IsNullOrWhiteSpace(ip) ||
				string.IsNullOrEmpty(ip) ||
				null == neConfig)
			{
				throw new ArgumentNullException("ip is null or empty, or neConfig is null");
			}

			if (HasLinkWithSameIp(ip))
			{
				return false;
			}

			INetElementLink link = LinkFactory.CreateLink(neConfig.conType);
			_mapNetElementLinks[ip] = link;

			//此处只有udp协议的数据处理，后面增加其他类型的协议
			_subscribeClient.AddSubscribeTopic($"udp-recv://{ip}:{CommonPort.AtpLinkPort}", OnLinkMsgFromBoard);       //订阅消息
			link.Connect(neConfig);

			return true;
			//TODO 启动定时器，超时后启动取消还是让前台手动取消？或者设定时间间隔，一直重试？
		}

		/// <summary>
		/// 断开网元连接
		/// </summary>
		/// <param name="ip"></param>
		public bool DisconnectNe(string ip)
		{
			if (string.IsNullOrWhiteSpace(ip) ||
				string.IsNullOrEmpty(ip))
			{
				throw new ArgumentNullException("ip is null or empty");
			}

			if (!HasLinkWithSameIp(ip))
			{
				return false;
			}

			INetElementLink link = _mapNetElementLinks[ip];
			link.Disconnect();
			_subscribeClient.CancelSubscribeTopic($"from:{ip}:{CommonPort.AtpLinkPort}");
			_mapNetElementLinks.Remove(ip);

			return true;
		}

		/// <summary>
		/// 连接管理一级订阅处理函数
		/// </summary>
		/// <param name="msg">所需的数据消息</param>
		private void OnLinkMgr(SubscribeMsg msg)
		{
			//多层topic解析，当前版本为了简单，先使用直接调用的方式进行处理
		}

		private void OnLinkMsgFromBoard(SubscribeMsg msg)
		{
			byte[] msgBytes = msg.Data;
			GtsMsgHeader header = GetHeaderFromBytes.GetHeader(msgBytes);
			string ip = GetIpFromTopic(msg.Topic);

			switch (header.u16Opcode)
			{
				case GtsMsgType.O_GTSAGTSM_LOGON_RSP:
					DealConnectRsp(ip);
					break;
				case GtsMsgType.O_GTSAGTSM_TRACE_CTRL_RSP:
					break;
				case GtsMsgType.O_GTSAGTSM_FDL_RSP:
					break;
				case GtsMsgType.O_GTSAGTSM_FDL_COMPLETE_IND:
					break;
				case GtsMsgType.O_GTSAGTSM_FILTER_RULE_RSP:
					break;
				case GtsMsgType.O_GTSAGTSM_FILTER_RESET_RSP:
					break;
				case GtsMsgType.O_GTSAGTSM_TRACE_MSG:
					DealTraceMsg(msg.Data);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 处理设备发回的登录响应报文
		/// </summary>
		/// <param name="ip"></param>
		private void DealConnectRsp(string ip)
		{
			if (string.IsNullOrWhiteSpace(ip) ||
				string.IsNullOrEmpty(ip))
			{
				throw new ArgumentNullException();
			}

			if (_mapNetElementLinks.ContainsKey(ip))
			{
				INetElementLink link = _mapNetElementLinks[ip];
				link.OnLogonResult(true);
			}
		}

		/// <summary>
		/// gtsa发送过来的跟踪消息，直接publish
		/// </summary>
		/// <param name="msgData"></param>
		private void DealTraceMsg(byte[] msgData)
		{
			PublishHelper.PublishMsg("/GtsMsgParseService", msgData);
		}

		/// <summary>
		/// 从from:ip:port字段中取出ip字符串
		/// </summary>
		/// <param name="topic">订阅消息字符串</param>
		/// <returns>null:格式不对；ip:格式正确</returns>
		public string GetIpFromTopic(string topic)
		{
			string ip = null;
			string prefix = "udp-recv://";
			int pos = topic.IndexOf(prefix, StringComparison.Ordinal);
			if (pos >= 0)
			{
				string temp = topic.Substring(pos + prefix.Length);
				int pos2 = temp.IndexOf(":", StringComparison.Ordinal);
				if (pos2 > 0)
				{
					ip = temp.Substring(0, pos2);
				}
			}

			return ip;
		}

		/// <summary>
		/// 判断是否已经建立ip对应的连接
		/// TODO 后期需要修改。OSP一个IP可以建立多个连接，只要port不同即可
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public bool HasLinkWithSameIp(string ip)
		{
			if (string.IsNullOrWhiteSpace(ip) ||
				string.IsNullOrEmpty(ip))
			{
				throw new ArgumentNullException();
			}

			return _mapNetElementLinks.ContainsKey(ip);
		}

		private SubscribeClient _subscribeClient;

		//保存所有添加的网元信息。Key：ne addr，Value：ne reference
		private Dictionary<string, INetElementLink> _mapNetElementLinks;
	}
}
