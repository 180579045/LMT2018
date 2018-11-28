using LmtbSnmp;
using LogManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
		public void MacAddressTest()
		{
			string logMsg;
			string commnuity = "public";
			string destIpAddr = "172.27.245.92";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);


			List<CDTLmtbVb> lmtVbs = new List<CDTLmtbVb>();
			// 1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11.0.0.1.0: 001ea859a85b

			CDTLmtbVb lmtVb2 = new CDTLmtbVb();
			lmtVb2.Oid = ("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11");
			lmtVbs.Add(lmtVb2);
			

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

						CDTLmtbVb lmtVbTmp = new CDTLmtbVb { Oid = (item.Key) };
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

			// 1.3.6.1.4.1.5105.100.1.9.4.11
			// 2.4.1.4.1.1.8
			CDTLmtbVb lmtVb = new CDTLmtbVb {Oid = ("1.3.6.1.4.1.5105.100.2.4.1.4.1.1.8.1") };
//            lmtVb.Oid = ("1.3.6.1.4.1.5105.100.2.2");

			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			lmtPdu.AddVb(lmtVb);

			long requestId;

			lmtbSnmpEx.SnmpGetSync(lmtPdu, out requestId, destIpAddr, 0);
			string strVal = "";

			Debug.WriteLine("---------- strVal = {0}", strVal);
		}


		[TestMethod()]
		public void GetRequestIpTest()
		{

			string commnuity = "public";
			string destIpAddr = "172.27.245.92";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);

			string strOid = "1.3.6.1.4.1.5105.100.1.2.1.1.1.5.3";

			CDTLmtbVb lmtVb = new CDTLmtbVb { Oid = (strOid) };
			//            lmtVb.Oid = ("1.3.6.1.4.1.5105.100.2.2");

			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			lmtPdu.AddVb(lmtVb);

			long requestId;

			lmtbSnmpEx.SnmpGetSync(lmtPdu, out requestId, destIpAddr, 0);
			string strVal = "";
			lmtPdu.GetValueByOID("1.3.6.1.4.1.5105.100.1.9.4.11", out strVal);

			Debug.WriteLine("---------- strVal = {0}", strVal);
		}

		[TestMethod()]
		public void GetRequestMacTest()
		{

			string commnuity = "public";
			string destIpAddr = "172.27.245.92";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);

			string strOid = "1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11.2";

			CDTLmtbVb lmtVb = new CDTLmtbVb { Oid = (strOid) };
			//            lmtVb.Oid = ("1.3.6.1.4.1.5105.100.2.2");

			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			lmtPdu.AddVb(lmtVb);

			long requestId;

			lmtbSnmpEx.SnmpGetSync(lmtPdu, out requestId, destIpAddr, 0);
			string strVal = "";
			lmtPdu.GetValueByOID(strOid, out strVal);

			Debug.WriteLine("---------- strVal = {0}", strVal);
		}

		[TestMethod]
		public void TimeTest()
		{
			string strDateTime = "2018-06-13 08:01:45";


			byte[] bytes = SnmpMibUtil.SnmpStrDateTime2Bytes(strDateTime);


			OctetString t = new OctetString(bytes);

			string strDt = SnmpMibUtil.SnmpDateTime2String(t);

			Debug.WriteLine("----");
		}

		[TestMethod]
		public void IpTest()
		{
			// 1.3.6.1.4.1.5105.100.1.2.1.1.1.5.3: ac1bf5c6  172.27.245.198
			string strIpAddr = "172.27.245.198";
			byte[] bytes = SnmpMibUtil.SnmpStrIpAddr2Bytes(strIpAddr);

			OctetString otString = new OctetString(bytes);

			IpAddress ipAddr2 = new IpAddress(otString);

			Debug.WriteLine("-----");
		}

		[TestMethod]
		public void MacTest()
		{
			// 001ea859a85b
			string strMacAddr = "001ea859a85b";
			byte[] bytes = SnmpMibUtil.StrHex2Bytes(strMacAddr);

			OctetString o1 = new OctetString(bytes);


			EthernetAddress ethAddr = new EthernetAddress(bytes);

			OctetString o2 = new OctetString(ethAddr);

			Debug.WriteLine("--------");

		}


		[TestMethod]
		public void Unsigned32ArrayTest()
		{
			string strU32Array = @"{123}";

			byte[] bytes = SnmpMibUtil.Unsigned32Array2Bytes(strU32Array);


			OctetString o = new OctetString(bytes);


			Debug.WriteLine("----------");
		}

		[TestMethod]
		public void PlnmTest()
		{
			string plmn = "00";

			byte[] bytes = SnmpMibUtil.MncMccType2Bytes(plmn);

			Debug.WriteLine("------------");
		}

		


		[TestMethod]
		public void SnmpSetIpTest()
		{
			string commnuity = "public";
			string destIpAddr = "172.27.245.92";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);

			string strOid = "1.3.6.1.4.1.5105.100.1.2.1.1.1.5.3";
			string strIpAddr = "172.27.245.198";

			List<CDTLmtbVb> vbList= new List<CDTLmtbVb>();

			CDTLmtbVb lmtVb = new CDTLmtbVb { Oid = (strOid),Value=(strIpAddr) };
			lmtVb.SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS;
			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			lmtPdu.AddVb(lmtVb);

			long requestId;


			lmtbSnmpEx.SnmpSetSync(lmtPdu, out requestId, destIpAddr, 0);

			Debug.WriteLine("------");


		}



	}
}