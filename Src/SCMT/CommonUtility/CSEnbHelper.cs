using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 保存当前选择的基站信息
namespace CommonUtility
{
	public class CSEnbHelper
	{
		#region 公共接口

		public static string GetCurEnbAddr()
		{
			lock (_lock)
			{
				return _enbAddr;
			}
		}

		public static void SetCurEnbAddr(string strEnbAddr)
		{
			lock (_lock)
			{
				if (!string.IsNullOrEmpty(strEnbAddr))
				{
					_enbAddr = strEnbAddr;
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
