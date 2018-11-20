using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTestForAll.Tests
{
	[TestClass()]
	public class CommFunsTests
	{
		[TestMethod()]
		public void GenerateAdjCellIdInfoTest()
		{
			string rv = CommFuns.GenerateAdjCellIdInfo("0", "{1,2,3}");

			Console.WriteLine("------");
		}

		[TestMethod]
		public void TestTest()
		{
			string str2 = "";
			string str1 = "";

			if (string.Compare(str1, str2) == 0)
			{
				Console.WriteLine("====");
			}
			else
			{
				Console.WriteLine("<><><>");
			}
		}
	}
}