using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmtbSnmp
{
	/// <summary>
	/// LMT的Trap管理
	/// </summary>
	public class LmtTrapMgr
	{
		private static LmtTrapMgr _lmtTrapMgr = null;

		private static object _synObj = new object();

		// Trap监听实例
		public static TrapHelper m_TrapHelper = null;

		// LMT Trap端口
		private static int m_TrapPort = 162;

		private LmtTrapMgr()
		{ }

		/// <summary>
		/// 获取单实例方法
		/// </summary>
		/// <returns></returns>
		public static LmtTrapMgr GetInstance()
		{
			if (_lmtTrapMgr == null)
			{
				lock (_synObj)
				{
					if (_lmtTrapMgr == null)
					{
						_lmtTrapMgr = new LmtTrapMgr();

					}
				}
			}

			return _lmtTrapMgr;
		}

		/// <summary>
		/// 启动Trap监听
		/// </summary>
		/// <returns></returns>
		public bool StartLmtTrap()
		{
			bool rs = true;

			// 创建Trap监听实例
			if (m_TrapHelper != null)
			{
				m_TrapHelper.StopReceiver();
			}
			m_TrapHelper = new TrapHelper(m_TrapPort);

			// IPV4
			if (false == m_TrapHelper.InitReceiverIpv4())
			{
				Log.Error("IPV4 Trap监听实例创建失败！");
				rs = false;
			}
			// IPV6
			if (false == m_TrapHelper.InitReceiverIpv6())
			{
				Log.Error("IPV6 Trap监听实例创建失败！");
				rs = false;
			}

			return rs;
		}

	}
}
