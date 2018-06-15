using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;

namespace SCMTOperationCore.Message.SNMP
{
	/// <summary>
	/// 与eNB的通信链路管理器
	/// </summary>
	public class DTLinkPathMgr : Singleton<DTLinkPathMgr>, ICDTLinkPathMgr
	{

		// 管理侧trap端口
		public long m_TrapPort { get; set; }


		private DTLinkPathMgr()
		{
		}

		#region 公有接口，需要调用GetInstance后使用

		public LmtbSnmpEx GetSnmpByIp(string destIp)
		{
			lock (SynObj)
			{
				if (!m_mapIpToSnmpObj.ContainsKey(destIp))
				{
					throw new CustomException($"地址为{destIp}的Snmp实例尚未初始化");
				}

				return m_mapIpToSnmpObj[destIp];
			}
		}

		/// <summary>
		/// 启动SNMP通信模块，每个基站建立一次连接
		/// </summary>
		/// <returns></returns>
		public bool StartSnmp(string commnuity, string destIpAddr)
		{
			lock (SynObj)
			{
				// 对应ip的snmp实例不存在才会创建
				if (!m_mapIpToSnmpObj.ContainsKey(destIpAddr))
				{
					LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
					// 启动SNMP实例
					int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);
					m_mapIpToSnmpObj.Add(destIpAddr, lmtbSnmpEx);
				}
			}

			return true;
		}

		// 断开与设备的连接时需要调用这个接口把snmp通信实例关掉
		public void CloseSnmpInstance(string ip)
		{
			lock (SynObj)
			{
				if (m_mapIpToSnmpObj.ContainsKey(ip))
				{
					m_mapIpToSnmpObj.Remove(ip);
				}
			}
		}

		#endregion
		

		#region 公有静态接口

		// 获取和IP对应snmp实例，未处理异常
		public static LmtbSnmpEx GetSnmpInstance(string ip)
		{
			return GetInstance().GetSnmpByIp(ip);
		}

		#endregion

		#region 私有属性

		private static readonly object SynObj = new object();

		private static bool m_SnmpAlive = false;

		// 同步snmp连接信息(每个基站分别建立一个同步连接和一个异步连接)
		private static Dictionary<string, LmtbSnmpEx> m_mapIpToSnmpObj = new Dictionary<string, LmtbSnmpEx>();


		#endregion
	}
}
