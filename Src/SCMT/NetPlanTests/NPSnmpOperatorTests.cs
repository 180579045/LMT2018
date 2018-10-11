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
	public class NPSnmpOperatorTests
	{
		[TestMethod()]
		public void QueryNetBoardTest()
		{
			bool stop = false;
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			db.initDatabase("172.27.245.92");
			db.resultInitData = result =>
			{
				if (result)
				{
					var boardInfoList = new List<List<MibLeafNodeInfo>>();
					var ret = NPSnmpOperator.QueryNetBoard("GetNetBoard", ref boardInfoList);
					Assert.IsTrue(ret);
					Assert.AreEqual(boardInfoList.Count, 2);
					stop = true;
				}
			};

			while (!stop)
			{
				Thread.Sleep(1000);
			}
		}
	}
}