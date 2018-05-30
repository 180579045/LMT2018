using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

//时间相关
namespace CommonUility
{
	public class TimeHelper
	{
		// 获取当前的时间，返回字符串：year-mon-day hour:min:second
		public static string GetCurrentTime()
		{
			return DateTimeToString(DateTime.Now);
		}

		public static string DateTimeToString(DateTime t)
		{
			var year = t.Year.ToString("D4");
			var month = t.Month.ToString("D2");
			var day = t.Day.ToString("D2");
			var hour = t.Hour.ToString("D2");
			var min = t.Minute.ToString("D2");
			var second = t.Second.ToString("D2");
			var time = $"{year}-{month}-{day} {hour}:{min}:{second}";
			return time;
		}
	}
}
