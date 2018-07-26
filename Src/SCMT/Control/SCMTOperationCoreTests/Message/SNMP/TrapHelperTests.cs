using MIBDataParser.JSONDataMgr;
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
			// 初始化数据库
			Database db = Database.GetInstance();
			db.initDatabase("172.27.245.92");

//			CDTSnmpMsgDispose tt = new CDTSnmpMsgDispose();
//			tt.OnTrap(null, null);

			Thread.Sleep(50000);
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