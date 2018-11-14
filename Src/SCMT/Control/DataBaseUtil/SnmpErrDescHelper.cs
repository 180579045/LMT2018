/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ SnmpErrDescHelper $
* 机器名称：       $ machinename $
* 命名空间：       $ DataBaseUtil $
* 文 件 名：       $ SnmpErrDescHelper.cs $
* 创建时间：       $ 2018.10.16 $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     Snmp错误信息描述操作类。
* 修改时间     修 改 人         修改内容：
* 2018.10.xx  XXXX            XXXXX
*************************************************************************************/

using System;
using System.Data;
using System.Linq;
using System.Text;
using CommonUtility;
using LogManager;
using Newtonsoft.Json.Linq;

namespace DataBaseUtil
{
	public static class SnmpErrDescHelper
	{
		// Snmp错误描述表，存储所有错误信息
		private static readonly DataTable m_ErrDescTable = new DataTable();

		private static int m_nLastErrorStatus;

		/// <summary>
		/// 静态构造方法
		/// </summary>
		static SnmpErrDescHelper()
		{
			// 初始化静态变量
			m_ErrDescTable.Columns.Add("errorID", Type.GetType("System.String"));
			m_ErrDescTable.Columns.Add("errorChDesc", Type.GetType("System.String"));
			m_ErrDescTable.Columns.Add("errorEnDesc", Type.GetType("System.String"));
		}

		/// <summary>
		/// 根据错误Id获取错误信息
		/// </summary>
		/// <param name="strErrId"></param>
		/// <returns></returns>
		public static SnmpErrDesc GetErrDescById(string strErrId)
		{
			// 说明：先从内存中检索，不存在时再从文件中检索

			// 返回值
			SnmpErrDesc snmpErrDesc;
			// 检索条件
			var strSearch = $"errorID = '{strErrId}'";
			var dr = m_ErrDescTable.Select(strSearch);

			if (dr.LongCount() > 0)
			{
				snmpErrDesc = new SnmpErrDesc
				{
					errorID = (string)dr[0]["errorID"],
					errorChDesc = (string)dr[0]["errorChDesc"],
					errorEnDesc = (string)dr[0]["errorEnDesc"]
				};

				return snmpErrDesc;
			}

			// 内存中不存在，从文件中获取
			snmpErrDesc = GetErrDescFromFile(strErrId);
			// 将错误消息添加到内存中
			if (snmpErrDesc != null)
			{
				m_ErrDescTable.Rows.Add(snmpErrDesc.errorID, snmpErrDesc.errorChDesc, snmpErrDesc.errorEnDesc);
			}

			return snmpErrDesc;
		}

		/// <summary>
		/// 设置最后一个SNMP错误码
		/// </summary>
		/// <param name="nCode"></param>
		public static void SetLastErrorCode(int nCode)
		{
			lock (m_syncObj)
			{
				m_nLastErrorStatus = nCode;
			}
		}

		/// <summary>
		/// 根据错误码获取描述信息
		/// </summary>
		/// <returns></returns>
		public static string GetLastErrorDesc()
		{
			lock (m_syncObj)
			{
				var objErr = GetErrDescById(m_nLastErrorStatus.ToString());
				return objErr.errorChDesc;
			}
		}

		public static int GetLastErrorCode()
		{
			lock (m_syncObj)
			{
				return m_nLastErrorStatus;
			}
		}

		/// <summary>
		/// 根据错误Id从文件中获取错Snmp误信息
		/// </summary>
		/// <param name="strErrId"></param>
		/// <returns></returns>
		private static SnmpErrDesc GetErrDescFromFile(string strErrId)
		{
			// 返回值
			SnmpErrDesc snmpErrDesc = null;

			// 文件全路径
			var strFullPath = FilePathHelper.GetConfigPath() + "ErrorCodeInfo.json";
			// 读取文件内容
			var strJson = FileRdWrHelper.GetFileContent(strFullPath, Encoding.Default);

			if (string.IsNullOrEmpty(strJson))
			{
				Log.Error($"读取Snmp错误描述文件错误，文件路径={strFullPath}");
				return null;
			}

			var jObject = JObject.Parse(strJson);
			// 检索条件
			var strSearch = $"$.errorCodeInfo[?(@.errorID=={strErrId})]";
			var errDesc = jObject.SelectToken(strSearch);

			if (errDesc != null)
			{
				snmpErrDesc = errDesc.ToObject<SnmpErrDesc>();
			}

			return snmpErrDesc;
		}

		private static readonly object m_syncObj = new object();
	}
}