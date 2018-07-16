using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CommonUtility.Tests
{
	[TestClass()]
	public class CommonFunctionsTests
	{
		[TestMethod()]
		public void GenerateBitsTypeDescTest()
		{
			/*
			Bit值=>0< strValueList=>0:时钟故障/1:传输故障< 输出=>无效<
			Bit值=>1 < strValueList=>0:时钟故障 / 1:传输故障 < 输出=>时钟故障 <
			Bit值=>2 < strValueList=>0:时钟故障 / 1:传输故障 < 输出=>传输故障 <
			Bit值=>3 < strValueList=>0:时钟故障 / 1:传输故障 < 输出=>时钟故障 / 传输故障 <
			Bit值=>4 < strValueList=>0:时钟故障 / 1:传输故障 < 输出=>4 <
			*/


			string strBitVal = "4";
			string strValueList = "0:时钟故障/1:传输故障";
			string strValDesc = "";

			CommonFunctions.GenerateBitsTypeDesc(strBitVal, strValueList, out strValDesc);
		}

		[TestMethod]
		public void TranslateMibValueTest()
		{
			// 0:本地文件/1:远端文件

			string strIpaddr = "172.27.245.92";
			string strMibName = "configFileLocationType";
			string strMibValue = "2";
			string strReValue = "";


			CommonFunctions.TranslateMibValue(strIpaddr, strMibName, strMibValue, out strReValue, true);

			Debug.WriteLine("====");

		}

		[TestMethod]
		public void MyTest()
		{


			string strPath = "E:\\\\afyf\\src\\LMT2018\\Src\\SCMT\\FileManager\\test.txt";

			string str2 = strPath.Replace("\\","");

			Debug.WriteLine(str2);
		}
	}
}