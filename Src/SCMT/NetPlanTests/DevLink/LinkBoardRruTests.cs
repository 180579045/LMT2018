using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan.DevLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using MIBDataParser.JSONDataMgr;

namespace NetPlan.DevLink.Tests
{
	[TestClass()]
	public class LinkBoardRruTests
	{
		[TestMethod()]
		public async Task AddLinkTest()
		{
			// 初始化数据库
			CSEnbHelper.SetCurEnbAddr("172.27.245.92");
			var db = Database.GetInstance();
			var result = await db.initDatabase("172.27.245.92");
			Assert.IsTrue(result);

			var newBoard = MibInfoMgr.GetInstance().AddNewBoard(5, "22", "LTE FDD", "cpri");
			Assert.IsNotNull(newBoard);

			var newRruList = MibInfoMgr.GetInstance().AddNewRru(new List<int>() {2}, 229, "级联模式");
			Assert.IsNotNull(newRruList);
			Assert.AreEqual(1, newRruList.Count);

			var newRru = newRruList.First();
			Assert.IsNotNull(newRru);

			var btorLink = new LinkBoardRru();
			Assert.IsNotNull(btorLink);

			var boardEp = new LinkEndpoint
			{
				devType = EnumDevType.board,
				strDevIndex = newBoard.m_strOidIndex,
				portType = EnumPortType.bbu_to_rru,
				nPortNo = 3
			};

			var rruEp = new LinkEndpoint
			{
				devType = EnumDevType.rru,
				strDevIndex = newRru.m_strOidIndex,
				portType = EnumPortType.rru_to_bbu,
				nPortNo = 3
			};

			var wholeLink = new WholeLink(boardEp, rruEp);
			Assert.IsNotNull(wholeLink);

			var mapData = MibInfoMgr.GetInstance().GetAllEnbInfo();
			result = btorLink.AddLink(wholeLink, ref mapData);
			Assert.IsTrue(result);
		}
	}
}