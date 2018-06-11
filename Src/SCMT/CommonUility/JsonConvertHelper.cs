//using NetMQ;

using System.Text;
using Newtonsoft.Json;

namespace CommonUility
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
			return JsonConvert.DeserializeObject<T>(json);
		}

		/// <summary>
		/// 把bytes[]数组中内容转为T类型的对象
		/// </summary>
		/// <typeparam name="T">泛型类型T</typeparam>
		/// <param name="jsonBytes">bytes数组</param>
		/// <returns></returns>
		public static T SerializeJsonToObject<T>(byte[] jsonBytes)
		{
			//var s = SendReceiveConstants.DefaultEncoding.GetString(jsonBytes);
			var s = Encoding.Default.GetString(jsonBytes);
			return JsonHelper.SerializeJsonToObject<T>(s);
		}
	}
}
