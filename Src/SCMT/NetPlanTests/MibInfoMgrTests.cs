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
	public class MibInfoMgrTests
	{
		[TestMethod()]
		public void AddNewAntTest()
		{
			bool stop = false;
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			db.initDatabase("172.27.245.92");
			db.resultInitData = result =>
			{
				if (result)
				{
					var ant = MibInfoMgr.GetInstance().AddNewAnt(0);
					Assert.IsNotNull(ant);
					ant = MibInfoMgr.GetInstance().AddNewAnt(0);
					Assert.IsNull(ant);
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