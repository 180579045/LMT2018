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
	internal enum ProcotolType
	{
		Invalid = -1,
		UDP = 0,
		TCP,
		IP,
		FTP,
	}

	public class SessionService : Singleton<SessionService>, IDisposable
	{

		private SessionService()
		{
			_Sessions = new Dictionary<string, IASession>();

			string topic = "/SessionService/Create/UDP";
			string desc = "创建连接。后面跟协议：UDP、TCP、IP、FTP。";
			Type dataType = typeof(SessionData);
			TopicManager.GetInstance().AddTopic(new TopicInfo(topic, desc, dataType));
			SubscribeHelper.AddSubscribe(topic, OnMakeSession);
			SubscribeHelper.AddSubscribe("/SessionService/Create/IP", OnMakeSession);

			topic = "/SessionService/Delete/UDP";
			desc = "断开连接。后面跟协议：UDP、TCP、IP、FTP。";
			TopicManager.GetInstance().AddTopic(new TopicInfo(topic, desc, dataType));
			SubscribeHelper.AddSubscribe(topic, OnBreakSession);
			SubscribeHelper.AddSubscribe("/SessionService/Delete/IP", OnBreakSession);
		}

		//创建链接
		public void OnMakeSession(SubscribeMsg msg)
		{
			string procotol = GetProcotolTypeFromTopic(msg.Topic);
			ProcotolType pt = GetProcotolType(procotol);
			if (ProcotolType.Invalid == pt)
			{
				// unsupported procotol
				return;
			}

			var sessionData = JsonHelper.SerializeJsonToObject<SessionData>(msg.Data);
			var key = $"{procotol}://{sessionData.target.raddr}:{sessionData.target.rport}";
			if (_Sessions.ContainsKey(key))
			{
				_Sessions[key].SendAsync(sessionData.data);	//如果连接已经建立了，就只发送数据
				return;
			}

			IASession session = null;
			if (ProcotolType.UDP == pt)
			{
				session = new UdpSession(sessionData.target);   //TODO 先只处理udp协议
				session.SendAsync(sessionData.data);
			}
			else if (ProcotolType.IP == pt)
			{
				session = new IpSession(sessionData.target);
				if (!session.Init(sessionData.target.laddr))
				{
					return;
				}
			}
			_Sessions.Add(key, session);
		}

		//断开连接
		public void OnBreakSession(SubscribeMsg msg)
		{
			string procotol = GetProcotolTypeFromTopic(msg.Topic);
			ProcotolType pt = GetProcotolType(procotol);
			if (ProcotolType.Invalid == pt)
			{
				// unsupported procotol
				return;
			}

			var data = JsonHelper.SerializeJsonToObject<SessionData>(msg.Data);
			var key = $"{procotol}://{data.target.raddr}:{data.target.rport}";
			if (!_Sessions.ContainsKey(key))
			{
				return;
			}

			_Sessions[key].SendAsync(data.data);		//发送数据
			_Sessions[key].Stop();						//释放数据
			_Sessions.Remove(key);
		}

		//从topic中取出协议类型。大概有UDP，IP，TCP，SNMP，FTP
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
		private Dictionary<string, IASession> _Sessions;
		public void Dispose()
		{
			foreach (var session in _Sessions)
			{
				session.Value.Stop();
			}

			SubscribeHelper.CancelSubscribe("/SessionService/Create/UDP");
			SubscribeHelper.CancelSubscribe("/SessionService/Create/IP");
			SubscribeHelper.CancelSubscribe("/SessionService/Delete/IP");
			SubscribeHelper.CancelSubscribe("/SessionService/Delete/UDP");
		}
	}
}
