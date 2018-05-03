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
/// 订阅Topics：/SessionService/Create/,/SessionService/Delete/
/// </summary>
namespace AtpMessage.SessionMgr
{
	public enum ProcotolType
	{
		Invalid = -1,
		UDP = 0,
		TCP,
		IP,
		FTP,
	}

	public class SessionService
	{
		public static SessionService GetInstance()
		{
			return Singleton<SessionService>.GetInstance();
		}

		public SessionService()
		{
			_udpSessions = new Dictionary<string, UdpSession>();

			string topic = "/SessionService/Create/";
			string desc = "创建连接。后面跟协议：UDP、TCP、IP、FTP。";
			Type dataType = typeof(SessionData);
			TopicManager.GetInstance().AddTopic(new TopicInfo(topic, desc, dataType));
			SubscribeHelper.AddSubscribe(topic, OnMakeUdpSession);

			topic = "/SessionService/Delete/";
			desc = "断开连接。后面跟协议：UDP、TCP、IP、FTP。";
			TopicManager.GetInstance().AddTopic(new TopicInfo(topic, desc, dataType));
			SubscribeHelper.AddSubscribe(topic, OnBreakUdpSession);
		}

		/// <summary>
		/// 初始化函数。初始化map，订阅主题
		/// </summary>
		public void Init() { }

		/// <summary>
		/// 创建链接
		/// </summary>
		/// <param name="msg"></param>
		public void OnMakeUdpSession(SubscribeMsg msg)
		{
			string procotol = GetProcotolTypeFromTopic(msg.Topic);
			ProcotolType pt = GetProcotolType(procotol);
			if (ProcotolType.Invalid == pt)
			{
				// unsupported procotol
				return;
			}

			var sessionData = JsonHelper.SerializeJsonToObject<SessionData>(msg.Data);
			var key = $"{procotol}://{sessionData.target.addr}:{sessionData.target.port}";
			if (_udpSessions.ContainsKey(key))
			{
				_udpSessions[key].SendAsync(sessionData.data);	//如果连接已经建立了，就只发送数据
				return;
			}

			var session = new UdpSession(sessionData.target);	//TODO 先只处理udp协议
			session.Run();
			session.SendAsync(sessionData.data);
			_udpSessions.Add(key, session);
		}

		/// <summary>
		/// 断开连接。
		/// </summary>
		/// <param name="msg"></param>
		public void OnBreakUdpSession(SubscribeMsg msg)
		{
			string procotol = GetProcotolTypeFromTopic(msg.Topic);
			ProcotolType pt = GetProcotolType(procotol);
			if (ProcotolType.Invalid == pt)
			{
				// unsupported procotol
				return;
			}

			var data = JsonHelper.SerializeJsonToObject<SessionData>(msg.Data);
			var key = $"{procotol}://{data.target.addr}:{data.target.port}";
			if (!_udpSessions.ContainsKey(key))
			{
				return;
			}

			_udpSessions[key].SendAsync(data.data);		//发送数据
			_udpSessions[key].Dispose();				//释放数据
			_udpSessions.Remove(key);
		}

		/// <summary>
		/// 从topic中取出协议类型。大概有UDP，IP，TCP，SNMP，FTP
		/// </summary>
		/// <param name="topic"></param>
		/// <returns></returns>
		public string GetProcotolTypeFromTopic(string topic)
		{
			if (String.IsNullOrWhiteSpace(topic))
			{
				throw new ArgumentNullException();
			}

			string prefix = "/SessionService/Create/";
			return topic.Substring(prefix.Length);
		}

		private ProcotolType GetProcotolType(string procotol)
		{
			ProcotolType type = ProcotolType.Invalid;

			procotol = procotol.ToUpper();
			if (procotol.Equals("UDP"))
			{
				type = ProcotolType.UDP;
			}
			else if (procotol.Equals("TCP"))
			{
				type = ProcotolType.TCP;
			}
			else if (procotol.Equals("IP"))
			{
				type = ProcotolType.IP;
			}
			else if (procotol.Equals("FTP"))
			{
				type = ProcotolType.FTP;
			}

			return type;
		}

		private bool IsSupportProtocol(string topic)
		{
			string procotol = GetProcotolTypeFromTopic(topic);
			ProcotolType pt = GetProcotolType(procotol);
			return (ProcotolType.Invalid == pt);
		}

		//保存udpSession。key:"目的IP-目的端口"
		private Dictionary<string, UdpSession> _udpSessions;
	}
}
