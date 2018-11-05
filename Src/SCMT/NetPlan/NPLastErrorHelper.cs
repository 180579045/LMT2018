using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan
{
	/// <summary>
	/// 网规最后一个操作的错误信息
	/// </summary>
	public static class NPLastErrorHelper
	{
		#region 公共接口

		public static void SetLastError(string strErrorDesc)
		{
			lock (m_syncObj)
			{
				m_strLastErrDesc = strErrorDesc;
			}
		}

		public static string GetLastError()
		{
			lock (m_syncObj)
			{
				return m_strLastErrDesc;
			}
		}

		#endregion

		private static string m_strLastErrDesc;
		private static readonly object m_syncObj = new object();
	}
}
