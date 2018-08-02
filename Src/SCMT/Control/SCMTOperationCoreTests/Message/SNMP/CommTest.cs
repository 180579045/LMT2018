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
			string targetIp = "192.168.5.198";
			string str1 = $"{{\"TargetIp\" : \"{targetIp}\", \"UpdatePath\" : \"{targetIp}\"}}";

			Debug.WriteLine(str1);
		}
	}
}
