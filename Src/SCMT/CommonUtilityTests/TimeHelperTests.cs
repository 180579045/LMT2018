using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility.Tests
{
	[TestClass()]
	public class TimeHelperTests
	{
		[TestMethod()]
		public void GetCurrentTimeTest()
		{
			//CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("zh_CN");
			string format = "yyyyMMddHHmmss00zz00";
			string timeText = DateTime.Now.ToString(format, CultureInfo.CurrentCulture);

			var binaryText = "07D90101010312002B0800";
			binaryText = binaryText.Replace("2B", "+");
			var subStrings = binaryText.Split('+');
			var hexString = subStrings[0];

			var bbb = TimeHelper.StrHex2Bytes(hexString);
			
			int offset = 0;
			var year = bbb[offset++] * 256 + bbb[offset++];

			var month = bbb[offset++];
			var day = bbb[offset++];
			var hour = bbb[offset++];
			var min = bbb[offset++];
			var sec = bbb[offset++];

			var dt = new DateTime(year, month, day, hour, min, sec, DateTimeKind.Utc);
			var dd = TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.Local);

			binaryText = "07D90101010312002B0800";
			var newdt = TimeHelper.ConvertUtcTimeTextToDateTime(binaryText).ConvertTimeZoneToLocal();
			Assert.AreEqual(dd, newdt);
		}
	}
}