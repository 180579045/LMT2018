using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCoreTests.Message.SNMP
{

	[TestClass()]
	public class CommTest
	{
		[TestMethod()]
		public void MyTest()
		{

			string fmt = "";
			fmt = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");

			Debug.WriteLine(fmt);
		}
	}
}
