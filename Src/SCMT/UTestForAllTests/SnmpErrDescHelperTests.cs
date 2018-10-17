using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTestForAll.Tests
{
	[TestClass()]
	public class SnmpErrDescHelperTests
	{
		[TestMethod()]
		public void GetErrDescByIdTest()
		{
			SnmpErrDesc snmpDrrDesc = SnmpErrDescHelper.GetErrDescById("100");

			SnmpErrDesc snmpDrrDesc2 = SnmpErrDescHelper.GetErrDescById("100");

			//Assert.Fail("===");
		}
	}
}