using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIBDataParser.JSONDataMgr;

namespace NetPlan.Tests
{
	[TestClass()]
	public class NPECmdHelperTests
	{
		[TestMethod()]
		public void GetAllCmdByTypeTest()
		{
			NPECmdHelper.GetInstance().GetAllCmdByType("board");
		}

		[TestMethod()]
		public void GetDevAttributesFromMibTest()
		{
			Database.GetInstance().initDatabase("172.27.245.92");
			NPECmdHelper.GetInstance().GetDevAttributesFromMib("board");
		}
	}
}