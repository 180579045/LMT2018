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
	public class NPEAntHelperTests
	{
		[TestMethod()]
		public void GetAllAntTypeInfoTest()
		{
			var ret = NPEAntHelper.GetInstance().GetAllAntTypeInfo();
			Assert.IsNotNull(ret);
		}

		[TestMethod()]
		public void GetAntBfsDataByVendorTypeIdxTest()
		{
			var ret = NPEAntHelper.GetInstance().GetAntBfsDataByVendorTypeIdx("3", "40");
			Assert.IsTrue(ret.Count > 0);
		}

		[TestMethod()]
		public void GetAntBfsDataByVendorAndTypeIdxTest()
		{
			var ret = NPEAntHelper.GetInstance().GetAntBfsDataByVendorAndTypeIdx("3", "40");
			Assert.IsTrue(ret.Count > 0);
		}
	}
}