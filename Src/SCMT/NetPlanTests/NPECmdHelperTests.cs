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
	public class NPECmdHelperTests
	{
		[TestMethod()]
		public void GetAllCmdByTypeTest()
		{
			NPECmdHelper.GetInstance().GetAllCmdByType("board");
		}
	}
}