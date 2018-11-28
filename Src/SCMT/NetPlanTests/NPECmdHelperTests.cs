using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonUtility;
using MIBDataParser.JSONDataMgr;

namespace NetPlan.Tests
{
	[TestClass()]
	public class NPECmdHelperTests
	{

		[TestMethod()]
		public async Task GetDevAttributesFromMibTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var dev = NPECmdHelper.GetInstance().GetDevAttributesFromMib("ant");
			Assert.IsNull(dev);
		}
	}
}