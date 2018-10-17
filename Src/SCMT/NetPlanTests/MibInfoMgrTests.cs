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

		[TestMethod()]
		public void AddNewBoardTest()
		{
			bool stop = false;
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			db.initDatabase("172.27.245.92");
			db.resultInitData = result =>
			{
				if (result)
				{
					var dev = MibInfoMgr.GetInstance().AddNewBoard(1, "SCTE", "LTE FDD", "CPRI");
					Assert.IsNotNull(dev);
					var delResult = MibInfoMgr.GetInstance().DelDev(dev.m_strOidIndex, EnumDevType.board);
					Assert.IsTrue(delResult);

					stop = true;
				}
			};

			while (!stop)
			{
				Thread.Sleep(1000);
			}
		}

		[TestMethod()]
		public void SetDevAttributeValueTest()
		{
			bool stop = false;
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			db.initDatabase("172.27.245.92");
			db.resultInitData = result =>
			{
				if (result)
				{
					if (NPSnmpOperator.InitNetPlanInfo())
					{
						var strIndex = ".0.0.1";
						MibInfoMgr.GetInstance().SetDevAttributeValue(strIndex, "netBoardIrFrameType", "CPRI-HDLC", EnumDevType.board);
					}

					stop = true;
				}
			};

			while (!stop)
			{
				Thread.Sleep(1000);
			}
		}

		[TestMethod()]
		public void DistributeBoardInfoToEnbTest()
		{
			bool stop = false;
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			db.initDatabase("172.27.245.92");
			db.resultInitData = result =>
			{
				if (result)
				{
					var dev = MibInfoMgr.GetInstance().AddNewBoard(1, "SCTF板", "LTE FDD", "CPRI");
					Assert.IsNotNull(dev);

					var ret = MibInfoMgr.GetInstance().DistributeBoardInfoToEnb();
					Assert.IsTrue(ret);

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