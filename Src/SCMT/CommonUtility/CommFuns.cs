using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
	/// <summary>
	/// 提供一些公共功能的函数接口
	/// </summary>
	public class CommFuns
	{
		/// <summary>
		/// 根据Mib描述解析出单位来
		/// </summary>
		/// <param name="strMibDesc"></param>
		/// <returns></returns>
		public static string ParseMibUnit(string strMibDesc)
		{
			Log.Debug(string.Format("strMibDesc = {0}", strMibDesc));

			string strUnit = "";
			if (string.IsNullOrEmpty(strMibDesc))
			{
				return "";
			}

			// 一般格式为{单位\精度:second}, 再取出":"后的字符
			int indexLeft = strMibDesc.IndexOf('{');
			int indexRight = strMibDesc.LastIndexOf('}');
			if (indexLeft < 0 || indexRight < 0) // "{"和"}"不同时存在
			{
				return "";
			}

            strUnit = strMibDesc.Substring(indexLeft, indexRight - indexLeft);

			// 截取":"后面部分
			int indexM = strUnit.IndexOf(':');
			if (indexM >= 0)
			{
				strUnit = strUnit.Substring(indexM);
			}

			// 小数点需要补上0
			if (strUnit.StartsWith("."))
			{
				strUnit = "0" + strUnit;
			}

			return strUnit;
		}
    }
}
