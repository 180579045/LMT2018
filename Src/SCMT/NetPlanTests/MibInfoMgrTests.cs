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
		public async void AddNewAntTest()
		{
			// 初始化数据库
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var ant = MibInfoMgr.GetInstance().AddNewAnt(0, "大唐", "TDAE-F02");
			Assert.IsNotNull(ant);

			ant = MibInfoMgr.GetInstance().AddNewAnt(0, "大唐", "TDAE-F02");
			Assert.IsNull(ant);
		}

		[TestMethod()]
		public async Task AddNewBoardTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var dev = MibInfoMgr.GetInstance().AddNewBoard(1, "SCTE", "LTE FDD", "CPRI");
			Assert.IsNotNull(dev);
			var delResult = MibInfoMgr.GetInstance().DelDev(dev.m_strOidIndex, EnumDevType.board);
			Assert.IsTrue(delResult);
		}

		[TestMethod()]
		public async Task SetDevAttributeValueTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var ret = await NPSnmpOperator.InitNetPlanInfo();
			if (ret)
			{
				var strIndex = ".0.0.1";
				MibInfoMgr.GetInstance().SetDevAttributeValue(strIndex, "netBoardIrFrameType", "CPRI-HDLC", EnumDevType.board);
			}
		}

		[TestMethod()]
		public async Task DistributeBoardInfoToEnbTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var dev = MibInfoMgr.GetInstance().AddNewBoard(1, "SCTF板", "LTE FDD", "CPRI");
			Assert.IsNotNull(dev);

			var ret = MibInfoMgr.GetInstance().DistributeNetPlanInfoToEnb(EnumDevType.board);
			Assert.IsTrue(ret);
		}

		[TestMethod()]
		public async Task SetNetLcInfoTest()
		{
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var strRruNo = "10";
			var lcInfoMap = new Dictionary<string, NPRruToCellInfo>();
			var lcInfo = new NPRruToCellInfo();
			lcInfo.CellIdList.Add(new CellAndState { cellId = "0", bIsFixed = false });
			lcInfo.CellIdList.Add(new CellAndState { cellId = "1", bIsFixed = false });
			lcInfo.CellIdList.Add(new CellAndState { cellId = "2", bIsFixed = false });
			lcInfo.CellIdList.Add(new CellAndState { cellId = "4", bIsFixed = false });
			lcInfoMap.Add("1", lcInfo);

			var ret = MibInfoMgr.GetInstance().SetNetLcInfo(strRruNo, lcInfoMap);
			Assert.IsTrue(ret);

			lcInfo = new NPRruToCellInfo();
			lcInfo.CellIdList.Add(new CellAndState { cellId = "6", bIsFixed = false });
			lcInfo.CellIdList.Add(new CellAndState { cellId = "7", bIsFixed = false });
			lcInfo.CellIdList.Add(new CellAndState { cellId = "8", bIsFixed = false });
			lcInfoMap["1"] = lcInfo;

			ret = MibInfoMgr.GetInstance().SetNetLcInfo(strRruNo, lcInfoMap);
			Assert.IsTrue(ret);
		}
	}
}