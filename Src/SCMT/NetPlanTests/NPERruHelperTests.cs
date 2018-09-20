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
	public class NPERruHelperTests
	{
		[TestMethod()]
		public void GetAllRruInfoTest()
		{
			NPERruHelper.GetInstance().GetAllRruInfo();
		}
	}
}