using System;
using System.Text;
using LogManager;
using Newtonsoft.Json;

namespace CommonUtility
{
	/// <summary>
	/// Json和对象转换助手
	/// </summary>
	public static class JsonHelper
	{
		/// <summary>
		/// 把object对象转换成json
		/// </summary>
		/// <param name="obj">待转换的对象</param>
		/// <returns>转换后的字符串</returns>
		public static string SerializeObjectToString(object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		/// <summary>
		/// 把string类型的json转换为T类型的对象
		/// </summary>
		/// <typeparam name="T">泛型类型T</typeparam>
		/// <param name="json">json格式的字符串</param>
		/// <returns>T类型对象实例</returns>
		public static T SerializeJsonToObject<T>(string json)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(json);
			}
			catch (Exception e)
			{
				Log.Error($"[JSONCOVHELPER] json convert failed, excrption : {e}");
				return default(T);
			}
		}

		/// <summary>
		/// 把bytes[]数组中内容转为T类型的对象
		/// </summary>
		/// <typeparam name="T">泛型类型T</typeparam>
		/// <param name="jsonBytes">bytes数组</param>
		/// <returns></returns>
		public static T SerializeJsonToObject<T>(byte[] jsonBytes)
		{
			var s = Encoding.UTF8.GetString(jsonBytes);		// 使用utf8编码，避免中文转换后乱码
			return JsonHelper.SerializeJsonToObject<T>(s);
		}
	}
}
