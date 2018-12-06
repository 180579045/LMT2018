using Microsoft.VisualStudio.TestTools.UnitTesting;
using LmtbSnmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace UTestForAll.Tests
{
	[TestClass()]
	public class SnmpMibUtilTests
	{

		[TestMethod()]
		public static void CheckMncMccTypeTest()
		{

			//bool rs1 = test.CheckMncMccType("000");
			/*
						bool rs2 = SnmpMibUtil.CheckMncMccType("333.44");
						bool rs3 = SnmpMibUtil.CheckMncMccType("1.3");
						bool rs4 = SnmpMibUtil.CheckMncMccType("-88");
						bool rs5 = SnmpMibUtil.CheckMncMccType("460");
						bool rs6 = SnmpMibUtil.CheckMncMccType("00");
			*/
			Debug.WriteLine("====");
		}

		[TestMethod]
		public void CheckMacAddrTest()
		{

			bool rs = SnmpMibUtil.CheckMacAddr("dd");

			Debug.WriteLine("=======");
		}
	}
}