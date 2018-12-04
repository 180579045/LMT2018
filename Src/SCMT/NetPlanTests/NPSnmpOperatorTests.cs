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
using ONE_DEV_ATTRI_INFO = System.Collections.Generic.List<NetPlan.MibLeafNodeInfo>;

namespace NetPlan.Tests
{
	[TestClass()]
	public class NPSnmpOperatorTests
	{
		[TestMethod()]
		public async Task QueryNetBoardTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);
			var boardInfoList = new List<DevAttributeInfo>();
			var ret = NPSnmpOperator.ExecuteNetPlanCmd("GetNetBoard", ref boardInfoList, EnumDevType.board);
			Assert.IsTrue(ret);
			Assert.AreEqual(boardInfoList.Count, 2);
		}

		[TestMethod()]
		public async Task InitNetPlanInfoTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var ret =await NPSnmpOperator.InitNetPlanInfo();
			Assert.IsTrue(ret);
		}

		[TestMethod()]
		public async Task GetRealTimeBoardInfoTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var ret = NPSnmpOperator.GetRealTimeBoardInfo();
			Assert.IsNotNull(ret);
		}
	}
}