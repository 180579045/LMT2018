using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCMTOperationCore.Message.MsgDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsgQueue;

namespace SCMTOperationCore.Message.MsgDispatcher.Tests
{
	[TestClass()]
	public class MsgDispatcherTests
	{
		[TestMethod()]
		public void OnEnbPhaseMsgTest()
		{
			var data = new byte[80];
			var msg = new SubscribeMsg(data, "test");
			
		}
	}
}