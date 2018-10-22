using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 保存当前选择的基站信息
// TODO 万恶之源，可能存在选择基站IP与正在操作页面IP不一致的问题

namespace CommonUtility
{
	public static class CSEnbHelper
	{
		#region 公共接口

		public static string GetCurEnbAddr()
		{
			lock (_lock)
			{
				return _enbAddr;
			}
		}

		/// <summary>
		/// 设置当前已选择基站的IP
		/// </summary>
		/// <param name="strEnbAddr"></param>
		public static void SetCurEnbAddr(string strEnbAddr)
		{
			if (string.IsNullOrEmpty(strEnbAddr))
			{
				return;
			}

			lock (_lock)
			{
				_enbAddr = strEnbAddr;
			}
		}

		/// <summary>
		/// 基站断开连接时，清理当前已选择相同地址基站
		/// </summary>
		/// <param name="strEnbAddr"></param>
		public static void ClearCurEnbAddr(string strEnbAddr)
		{
			if (string.IsNullOrEmpty(strEnbAddr))
			{
				return;
			}

			lock (_lock)
			{
				if (strEnbAddr.Equals(_enbAddr))
				{
					_enbAddr = null;
				}
			}
		}

		#endregion


		#region 私有成员

		private static readonly object _lock = new object();
		private static string _enbAddr;

		#endregion
	}
}
