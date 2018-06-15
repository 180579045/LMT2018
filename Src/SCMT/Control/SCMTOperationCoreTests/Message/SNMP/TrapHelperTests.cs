using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCMTOperationCore.Message.SNMP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Message.SNMP.Tests
{
	[TestClass()]
	public class TrapHelperTests
	{
		[TestMethod()]
		public void InitReceiverTest()
		{
			TrapHelper trapHelper = new TrapHelper();
			trapHelper.InitReceiver();

			Debug.WriteLine("end ------------");
		}
	}
}