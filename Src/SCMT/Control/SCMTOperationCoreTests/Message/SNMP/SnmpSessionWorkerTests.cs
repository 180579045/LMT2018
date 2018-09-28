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
	public class SnmpSessionWorkerTests
	{
/*		[TestMethod()]
		public void SnmpGetSyncTest()
		{
			string strIpAddr = "172.27.245.92";

			//			string commniuty = "public";
			string commniuty = "dtm.1234";
			DTLinkPathMgr dTLinkPathMgr = DTLinkPathMgr.GetInstance();
			dTLinkPathMgr.StartSnmp(commniuty, strIpAddr);


			List<CDTLmtbVb> lmtVbs = new List<CDTLmtbVb>();

			CDTLmtbVb lmtVb2 = new CDTLmtbVb();
			lmtVb2.Oid = ("1.3.6.1.4.1.5105.100.1.9.1.1");
			lmtVbs.Add(lmtVb2);

			CDTLmtbVb lmtVb3 = new CDTLmtbVb();
			lmtVb3.Oid = ("1.3.6.1.4.1.5105.100.1.9.1.2");
			lmtVbs.Add(lmtVb3);


			Dictionary<string, string> results;

			SnmpSessionWorker.SnmpGetSync(strIpAddr, lmtVbs, out results, 0);


		}


		[TestMethod()]
		public void SnmpGetNextSyncTest()
		{
			string strIpAddr = "172.27.245.92";

			string commniuty = "public";
			DTLinkPathMgr dTLinkPathMgr = DTLinkPathMgr.GetInstance();
			dTLinkPathMgr.StartSnmp(commniuty, strIpAddr);


			List<CDTLmtbVb> lmtVbs = new List<CDTLmtbVb>();

			CDTLmtbVb lmtVb2 = new CDTLmtbVb();
			lmtVb2.Oid = ("1.3.6.1.4.1.5105.100.2.2.1.2.1.1.3");
			lmtVbs.Add(lmtVb2);

			CDTLmtbVb lmtVb3 = new CDTLmtbVb();
			lmtVb3.Oid = ("1.3.6.1.4.1.5105.100.2.2.1.2.1.1.2");
			lmtVbs.Add(lmtVb3);


			CDTLmtbVb lmtVb4 = new CDTLmtbVb();
			lmtVb4.Oid = ("1.3.6.1.4.1.5105.100.2.2.1.2.1.1.4");
			lmtVbs.Add(lmtVb4);


			Dictionary<string, string> results;

			SnmpSessionWorker.SnmpGetNextSync(strIpAddr, lmtVbs, out results, 0);
		}

		[TestMethod()]
		public void SnmpSetSyncTest()
		{
			string strIpAddr = "172.27.245.92";

			string commniuty = "public";
			DTLinkPathMgr dTLinkPathMgr = DTLinkPathMgr.GetInstance();
			dTLinkPathMgr.StartSnmp(commniuty, strIpAddr);


			List<CDTLmtbVb> lmtVbs = new List<CDTLmtbVb>();

			CDTLmtbVb lmtVb2 = new CDTLmtbVb();
			SnmpSessionWorker.PacketVb(ref lmtVb2, "1.3.6.1.4.1.5105.100.2.2.1.1.1.0", "0" ,SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32);
			lmtVbs.Add(lmtVb2);

			CDTLmtbVb lmtVb3 = new CDTLmtbVb();
			SnmpSessionWorker.PacketVb(ref lmtVb3, "1.3.6.1.4.1.5105.100.2.4.2.4.1.1.0", "0", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32);
			lmtVbs.Add(lmtVb3);

			SnmpSessionWorker.SnmpSetSync(strIpAddr, lmtVbs, 0);
		}

	*/
	}
}