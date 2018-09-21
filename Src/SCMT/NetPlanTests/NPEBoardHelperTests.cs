using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan.Tests
{
	[TestClass()]
	public class NPEBoardHelperTests
	{
		[TestMethod()]
		public void GetSlotSupportBoardTypeTest()
		{
			NPEBoardHelper.GetInstance().GetSlotSupportBoardNames(1);
			NPEBoardHelper.GetInstance().GetSlotSupportBoardInfo(1);
		}
	}
}