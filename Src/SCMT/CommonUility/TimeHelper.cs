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

		public static string DateTimeToString(DateTime t, string format = "yyyy-MM-dd HH:mm:ss")
		{
			return t.ToString(format);
		}

		// 获取用于做文件夹名字的时间信息：yearmonday_hourminsec_millsec
		public static string GetFolderNameWithTime()
		{
			return DateTimeToString(DateTime.Now, "yyyyMMdd_HHmmss_fff");
		}
	}
}
