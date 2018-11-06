using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

// 判断哪些MIB表项不允许修改，用于网规

namespace MIBDataParser
{
	public static class DisableEditMibHelper
	{

		#region 公共接口

		public static bool IsDisabledEditMib(string strMibName)
		{
			if (string.IsNullOrEmpty(strMibName))
			{
				throw new ArgumentNullException(strMibName);
			}

			// 判断是否初始化过，如果没有初始化则进行初始化
			lock(m_syncObj)
			{
				// 无法判断的都认为是不禁用
				if (!m_bHasInited && !InitData())
				{
					return false;
				}

				return m_listDisbabledEditMibName.Contains(strMibName);
			}
		}

		#endregion


		#region 私有数据、方法

		private static List<string> m_listDisbabledEditMibName;

		private static bool m_bHasInited = false;

		private static object m_syncObj = new object();

		private static bool InitData()
		{
			try
			{
				var path = FilePathHelper.GetAppPath() + ConfigFileHelper.DisabledEditMibJson;
				var jsonContent = FileRdWrHelper.GetFileContent(path);
				var dem = JsonHelper.SerializeJsonToObject<DisabledMibName>(jsonContent);
				m_listDisbabledEditMibName = dem.DisabledMib;
				m_bHasInited = true;
			}
			catch(Exception)
			{
				
			}
			return m_bHasInited;
		}

		#endregion
	}


	public class DisabledMibName
	{
		public List<string> DisabledMib;

		public DisabledMibName()
		{
			DisabledMib = new List<string>();
		}
	}

}
