using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtility
{
	// 从MIB取出的字符串处理助手

	public static class MibStringHelper
	{
		/// <summary>
		/// 分解MIB项的取值范围字符串。
		/// 格式如：0:不激活/1:立即激活/2:定时激活/5:强制激活/6:手动激活
		/// </summary>
		/// <param name="origin">原始字符串</param>
		/// <returns>分解后的数据。Key为:前的数字，Value为:后的字符串</returns>
		public static Dictionary<int, string> SplitManageValue(string origin)
		{
			if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrEmpty(origin))
			{
				throw new  CustomException("传入的取值返回字符串为空");
			}

			var retData = new Dictionary<int, string>();
			var mva = origin.Split('/');
			foreach (var mv in mva)
			{
				if (string.IsNullOrWhiteSpace(mv) || string.IsNullOrEmpty(mv))
				{
					continue;
				}

				// 用分隔符/分割后再用：进行分割
				var temp = mv.Split(':');
				if (temp.Length != 2)
				{
					//Log.Error($"值：{mv}格式错误");
					continue;
				}

				var key = int.Parse(temp[0]);
				retData[key] = temp[1];
			}

			return retData;
		}

		/// <summary>
		/// 分解MIB中的字符串，形如：emb5116|EMB5116/emb6116|EMB6116
		/// </summary>
		/// <param name="originString"></param>
		/// <returns></returns>
		public static Dictionary<string, string> SplitMibEnumString(string originString)
		{
			if (string.IsNullOrWhiteSpace(originString) || string.IsNullOrEmpty(originString))
			{
				throw new CustomException("传入的取值返回字符串为空");
			}

			var retData = new Dictionary<string, string>();
			var mva = originString.Split('/');
			foreach (var mv in mva)
			{
				if (string.IsNullOrWhiteSpace(mv) || string.IsNullOrEmpty(mv))
				{
					continue;
				}

				var pos = mv.IndexOf('|');
				if (pos < 0)
				{
					retData[mv] = mv;
				}
				else
				{
					// 用分隔符/分割后再用：进行分割
					var temp = mv.Split('|');
					if (temp.Length != 2)
					{
						//Log.Error($"值：{mv}格式错误");
						continue;
					}

					var key = temp[0];
					retData[key] = temp[1];
				}
			}

			return retData;
		}

		/// <summary>
		/// 根据索引级数拆出索引值。注意，不判断拆掉索引值后的oid是否有效
		/// </summary>
		/// <param name="fulloid"></param>
		/// <param name="grade">索引级数</param>
		/// <returns>null:截取失败</returns>
		public static string GetIndexValueByGrade(string fulloid, int grade)
		{
			if (string.IsNullOrEmpty(fulloid))
			{
				return null;
			}

			// 如果是标量，grade = 0。索引级数范围(0,6]
			if (grade <= 0 || grade > 6)
			{
				return null;
			}

			// 如果fulloid中.字符的数量小于级数，直接返回null
			if (fulloid.Count(ch => ch == '.') < grade)
				return null;

			var startIndex = fulloid.Length;

			for (var i = 0; i < grade; i++)
			{
				startIndex = fulloid.LastIndexOf('.', startIndex - 1);
				if (-1 == startIndex)
				{
					return null;
				}
			}

			var sss = fulloid.Substring(startIndex, fulloid.Length - startIndex);
			return sss;
		}

		/// <summary>
		/// 从索引值字符串中取出指定序号的一段
		/// e.g. 从.0.0.1中取出第2级索引的真实值为0，第3级索引的真实值为1
		/// </summary>
		/// <param name="strIndex">索引值字符串</param>
		/// <param name="partNo">要取出段的序号，从0开始计数</param>
		/// <returns></returns>
		public static string GetRealValueFromIndex(string strIndex, int partNo)
		{
			if (string.IsNullOrEmpty(strIndex) || partNo < 0)
			{
				return null;
			}

			// 处理索引值字符串，只保留最前的.字符
			var temp = strIndex.Trim('.');
			temp = $".{temp}";

			var partIndex = strIndex.Split('.');

			if (partIndex.Length < partNo)  // 如果索引级数小于要查询的序号，就返回null
			{
				return null;
			}

			return partIndex[partNo];
		}

		/// <summary>
		/// 处理MIB中的默认值
		/// 比如：2:lined返回2；\"null\"返回空字符；0..95返回0；×返回0；-1.0返回-1
		/// </summary>
		/// <param name="strDefaultValue"></param>
		/// <returns></returns>
		public static string GetMibDefaultValue(string strDefaultValue)
		{
			var ret = "";
			if (string.IsNullOrEmpty(strDefaultValue))
			{
				return ret;
			}
			else if (strDefaultValue.Equals("×"))
			{
				ret = "0";
			}
			else if(strDefaultValue.Equals("-1.0"))
			{
				ret = "-1";
			}
			else if (strDefaultValue.Equals("\"null\""))
			{
				ret = "null";
			}
			else if (-1 != strDefaultValue.IndexOf(".."))
			{
				ret = "0";
			}
			else if (-1 != strDefaultValue.IndexOf(':'))
			{
				var subString = strDefaultValue.Split(':');
				ret = subString[0];
			}
			else
			{
				ret = strDefaultValue;
			}

			return ret;
		}
	}
}
