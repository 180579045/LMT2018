using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using MsgQueue;

/// <summary>
/// session管理类，单例，提供初始化函数。在这里session和link是区分开的，session模块只负责通信
/// link模块还包含其他的操作。link和session之间使用publish-subcribe模式
/// 订阅Topics：
/// TODO 应该有一个topic管理中心，每个模块都要注册
/// </summary>
namespace AtpMessage.SessionMgr
{
	public class UdpSessionData
	{
	    public byte[] data;
	    public Target target;
	}

	public class SessionService
	{
		public static SessionService GetInstance()
		{
			return Singleton<SessionService>.GetInstance();
		}

		/// <summary>
		/// 初始化函数。初始化map，订阅主题
		/// </summary>
		public void Init()
		{
			_udpSessions = new Dictionary<string, UdpSession>();

			_subClient = new SubscribeClient(CommonPort.PubServerPort);
			_subClient.AddSubscribeTopic("MakeUdpSession", OnMakeUdpSession);
			_subClient.AddSubscribeTopic("BreakUdpSession", OnBreakUdpSession);
			_subClient.Run();
		}

		public void OnMakeUdpSession(SubscribeMsg msg)
		{
			var sessionData = JsonHelper.SerializeJsonToObject<UdpSessionData>(msg.Data);
			var key = $"{sessionData.target.addr}-{sessionData.target.port}";
			if (_udpSessions.ContainsKey(key))
			{
				_udpSessions[key].SendAsync(sessionData.data);	//如果连接已经建立了，就只发送数据
				return;
			}

			var session = new UdpSession(sessionData.target);
			session.Run();
			session.SendAsync(sessionData.data);
			_udpSessions.Add(key, session);
		}

		public void OnBreakUdpSession(SubscribeMsg msg)
		{
			var data = JsonHelper.SerializeJsonToObject<UdpSessionData>(msg.Data);
			var key = $"{data.target.addr}-{data.target.port}";
			if (!_udpSessions.ContainsKey(key))
			{
				return;
			}

			_udpSessions[key].SendAsync(data.data);
			_udpSessions[key].Dispose();
			_udpSessions.Remove(key);
		}

		private SubscribeClient _subClient;

		//保存udpSession。key:"目的IP-目的端口"
		private Dictionary<string, UdpSession> _udpSessions;
	}
}
