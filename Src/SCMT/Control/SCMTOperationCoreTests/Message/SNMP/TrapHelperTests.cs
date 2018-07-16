using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCMTOperationCore.Message.SNMP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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


		[TestMethod]
		public void OnTrapTest()
		{
			CDTSnmpMsgDispose tt = new CDTSnmpMsgDispose();
			tt.OnTrap(null, null);

			Debug.WriteLine("===========");
		}






		[TestMethod]
		public void TrapTest()
		{

			Thread thdTrap = new Thread(TrapThd);
			thdTrap.IsBackground = true;
			thdTrap.Start();

			Debug.WriteLine("==============");
		}


		private void TrapThd()
		{
			TrapHelper trapHelper = new TrapHelper();
			trapHelper.InitReceiver();
			
		}
	}
}