using LogManager;
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
	public class LmtbSnmpExTests
	{

		[TestMethod()]
		public void GetNextRequestTest()
		{

			string commnuity = "public";
			string destIpAddr = "172.27.245.92";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);


			List<CDTLmtbVb> lmtVbs = new List<CDTLmtbVb>();
			CDTLmtbVb lmtVb = new CDTLmtbVb();
//            lmtVb.Oid = ("1.3.6.1.4.1.5105.100.2.2.1.1.1.0");
			lmtVb.Oid = ("1.3.6.1.4.1.5105.100.2.2.1.2.1.1.3");
			lmtVbs.Add(lmtVb);

			/*
			CDTLmtbVb lmtVb2 = new CDTLmtbVb();
			lmtVb2.Oid = ("1.3.6.1.4.1.5105.100.1.9.4.1");
			lmtVbs.Add(lmtVb2);

			CDTLmtbVb lmtVb3 = new CDTLmtbVb();
			lmtVb3.Oid = ("1.3.6.1.4.1.5105.100.1.9.4.2");
			lmtVbs.Add(lmtVb3);
			*/
			Dictionary<string, string> result;

			lmtbSnmpEx.GetNextRequest(destIpAddr, lmtVbs, out result, 0);

			foreach (KeyValuePair<string, string> item in result)
			{
				string logMsg = string.Format("oid={0}, value={1}", item.Key, item.Value);
				Log.Info(logMsg);
			}
		}

		[TestMethod()]
		public void GetNextRequestLoopTest()
		{
			string logMsg;
			string commnuity = "public";
			string destIpAddr = "172.27.245.92";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);


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
			/*
			CDTLmtbVb lmtVb5 = new CDTLmtbVb();
			lmtVb5.Oid = ("1.3.6.1.4.1.5105.100.2.2.1.2.1.1.5");
			lmtVbs.Add(lmtVb5);
			*/

			Dictionary<string, string> result = new Dictionary<string, string>();
			Dictionary<string, string> tmpResult;

			while (true)
			{
				if (lmtbSnmpEx.GetNextRequest(destIpAddr, lmtVbs, out tmpResult, 0))
				{
					lmtVbs.Clear();
					foreach (KeyValuePair<string, string> item in tmpResult)
					{
						logMsg = $"oid={item.Key}, value={item.Value}";
 //                       Log.Info(logMsg);

						// 保存结果
						result.Add(item.Key, item.Value);

						CDTLmtbVb lmtVbTmp = new CDTLmtbVb {Oid = (item.Key)};
						lmtVbs.Add(lmtVbTmp);
					}
				}
				else
				{
					break;
				}

				foreach (KeyValuePair<string, string> val in result)
				{
					logMsg = $"oid={val.Key}, value={val.Value}";
					//   Log.Info(logMsg);
					Debug.WriteLine(logMsg);
				}

			} // end while

		}




		[TestMethod()]
		public void GetRequestTest()
		{

			string commnuity = "public";
			string destIpAddr = "172.27.245.92";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);

			CDTLmtbVb lmtVb = new CDTLmtbVb {Oid = ("1.3.6.1.4.1.5105.100.1.9.4.11")};
//            lmtVb.Oid = ("1.3.6.1.4.1.5105.100.2.2");

			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			lmtPdu.AddVb(lmtVb);

			long requestId;

			lmtbSnmpEx.SnmpGetSync(lmtPdu, out requestId, destIpAddr, 0);
			string strVal = "";
//			lmtPdu.GetValueByOID("1.3.6.1.4.1.5105.100.1.9.4.11",out strVal);

			Debug.WriteLine("---------- strVal = {0}", strVal);
		}

	}
}