/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ DTLinkPathMgr $
* 机器名称：       $ machinename $
* 命名空间：       $ LinkPath $
* 文 件 名：       $ DTLinkPathMgr.cs $
* 创建时间：       $ 2018.09.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     与eNB的通信链路管理器。
* 修改时间     修 改 人         修改内容：
* 2018.09.xx  XXXX            XXXXX
*************************************************************************************/

using System.Collections.Generic;
using CommonUtility;
using LmtbSnmp;

namespace LinkPath
{
	/// <summary>
	/// 与eNB的通信链路管理器
	/// </summary>
	public class DTLinkPathMgr : Singleton<DTLinkPathMgr>, ICDTLinkPathMgr
	{

		// 管理侧trap端口
		public long m_TrapPort { get; set; }

		// SNMP消息处理模块
		public CDTSnmpMsgDispose m_SnmpMsgDispose;


		private DTLinkPathMgr()
		{
		}

		#region 公有接口，需要调用GetInstance后使用

		/// <summary>
		/// 初始化模块信息
		/// </summary>
		public void Initialize()
		{
			// 实例化SNMP消息处理实例
			m_SnmpMsgDispose = new CDTSnmpMsgDispose(this);
			return;

		}


		public LmtbSnmpEx GetSnmpByIp(string destIp)
		{
			lock (SynObj)
			{
				if (!m_mapIpToSnmpObj.ContainsKey(destIp))
				{
					LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
					// 启动SNMP实例
					int status = lmtbSnmpEx.SnmpLibStartUp(SnmpToDatabase.GetCommunityString(), destIp);
					m_mapIpToSnmpObj.Add(destIp, lmtbSnmpEx);
				}

				return m_mapIpToSnmpObj[destIp];
			}
		}

		// TODO: 因为不能确认是否能够动态设置基站IP，此方法目前废弃不用
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
					// TODO 这个地方要调查一下是否要改成动态设置ip？与旧工具保持一致
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
