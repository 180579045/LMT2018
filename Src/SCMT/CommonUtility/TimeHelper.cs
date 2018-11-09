using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

//时间相关
namespace CommonUtility
{
	public static class TimeHelper
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

		public static string DateTimeToStringEx(this DateTime t, string format = "yyyy-MM-dd HH:mm:ss")
		{
			return t.ToString(format);
		}

		// 获取用于做文件夹名字的时间信息：yearmonday_hourminsec_millsec
		public static string GetFolderNameWithTime()
		{
			return DateTimeToString(DateTime.Now, "yyyyMMdd_HHmmss_fff");
		}

		// 转换时区:UTC时间转换为当前时间。基站中出来的时间一般是UTC时间，最后带着2B0800字符串
		public static DateTime ConvertTimeZoneToLocal(this DateTime dt)
		{
			var newDt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, DateTimeKind.Utc);
			return TimeZoneInfo.ConvertTimeFromUtc(newDt, TimeZoneInfo.Local);
		}

		// 基站返回的时间字符串转为本地时间。基站返回的时间字符串形如：07D90101010312002B0800，2B要换成+
		// 转换时，只支持格式：yyyyMMddHHmmss00zz00。07D9这种16进制字符串需要先转换成十进制
		public static DateTime ConvertUtcTimeTextToDateTime(string utcTimeText)
		{
			if (string.IsNullOrEmpty(utcTimeText) || utcTimeText.Length < 14) // 14是从年到秒的字符串长度
			{
				return DateTime.UtcNow;
			}

			utcTimeText = utcTimeText.Replace(" ", "");
			//var binaryText = utcTimeText.Replace("2B", "+");	// 不能直接替换2B，分钟和秒数可能是43，也就是2B
			//var subStrings = binaryText.Split('+');
			var timeBytes = StrHex2Bytes(utcTimeText.Substring(0, 14));
			int offset = 0;
			var year = timeBytes[offset++] * 256 + timeBytes[offset++];
			var month = timeBytes[offset++];
			var day = timeBytes[offset++];
			var hour = timeBytes[offset++];
			var min = timeBytes[offset++];
			var sec = timeBytes[offset++];

			year = year > 9999 || year == 0 ? 2009 : year;
			month = (byte) (month > 12 || month == 0 ? 1 : month);
			day = (byte) (day > 31 || day == 0 ? 1 : day);
			hour = (byte) (hour > 24 ? 0 : hour);
			min = (byte)(min > 59 ? 0 : min);
			sec = (byte)(sec > 59 ? 0 : sec);

			var dt = new DateTime(year, month, day, hour, min, sec, DateTimeKind.Utc);
			return dt;
		}

		// 16进制字符串转换为字节数组
		public static byte[] StrHex2Bytes(string strHex)
		{
			if (string.IsNullOrEmpty(strHex))
			{
				return null;
			}
			strHex = strHex.Replace(":", "");
			strHex = strHex.Replace(" ", "");

			var length = strHex.Length;
			if ((length % 2) != 0)
			{
				strHex = strHex.Insert(length - 1, "0");	//字符串不是2的倍数，在最后一个字符前加0
			}

			length = strHex.Length;

			var bytes = new byte[length / 2];

			try
			{
				for (var i = 0; i < length; i += 2)
				{
					bytes[i / 2] = Convert.ToByte(strHex.Substring(i, 2), 16);
				}
			}
			catch (Exception e)
			{
				//Console.WriteLine(e);
			}

			return bytes;
		}


	}
}
