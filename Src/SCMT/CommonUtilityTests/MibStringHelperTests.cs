using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility.Tests
{
	[TestClass()]
	public class MibStringHelperTests
	{
		[TestMethod()]
		public void GetIndexValueByGradeTest()
		{
			var oid = "1.2.3.4.5.6.7.8.100.2.3.4.5.6.0.0.1";
			var grade = 3;
			var indexstr = ".0.0.1";
			var actual = MibStringHelper.GetIndexValueByGrade(oid, grade);
			Assert.AreEqual(indexstr, actual);
		}
	}
}