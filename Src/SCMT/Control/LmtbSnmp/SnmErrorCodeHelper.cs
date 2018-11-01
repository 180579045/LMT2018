using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

// 错误码帮助
namespace LmtbSnmp
{
	public class SnmErrorCodeHelper : Singleton<SnmErrorCodeHelper>
	{

		#region 公共接口区

		/// <summary>
		/// 根据错误码获取描述信息
		/// </summary>
		/// <param name="nErrorCode"></param>
		/// <returns></returns>
		public string GetErrorDescByCode(int nErrorCode)
		{
			if (!m_mapErrCodeToDesc.ContainsKey(nErrorCode))
			{
				return null;
			}

			return m_mapErrCodeToDesc[nErrorCode];
		}

		/// <summary>
		/// 设置最后一个Snmp操作错误码
		/// </summary>
		/// <param name="nCode"></param>
		public void SetLastErrorCode(int nCode)
		{
			m_nLastSnmpErrorStatus = nCode;
		}

		public string GetLastErrorDesc()
		{
			return GetErrorDescByCode(m_nLastSnmpErrorStatus);
		}

		#endregion

		#region 私有接口区

		private SnmErrorCodeHelper()
		{
			m_mapErrCodeToDesc = new Dictionary<int, string>
			{
				[176] = "该板卡已经布配RRU",
				[177] = "该板卡已经建立本地小区"
			};
		}

		#endregion


		#region 私有数据区

		private Dictionary<int, string> m_mapErrCodeToDesc;

		private int m_nLastSnmpErrorStatus;

		#endregion
	}
}
