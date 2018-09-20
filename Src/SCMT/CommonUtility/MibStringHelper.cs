using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtility
{
	// 从MIB取出的字符串处理助手

	public class MibStringHelper
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

			return retData;
		}

	}
}
