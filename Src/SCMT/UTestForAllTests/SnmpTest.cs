using LmtbSnmp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTestForAllTests
{
	[TestClass]
	public class SnmpTest
	{


		/// <summary>
		/// SnmpGetSync()方法使用例子
		/// </summary>
		[TestMethod]
		public void SnmpGetSyncTest()
		{
			string logMsg;
			string commnuity = "public";
			string destIpAddr = "172.27.245.91";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);

			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			CDTLmtbVb lmtVb = new CDTLmtbVb();
			// lmtVb.Oid = "1.3.6.1.4.1.5105.100.1.2.1.1.1.5.3"; // ftpServerInetAddr
//			lmtVb.Oid = "1.3.6.1.4.1.5105.100.2.7.2.2.6.1.15.200";
			lmtVb.Oid = "1.3.6.1.4.1.5105.100.1.9.5.1.1.9"; // boardUpTime
			lmtVb.Oid = "1.3.6.1.4.1.5105.100.2.4.7.2.1.22"; // sscUuExternBitmap {0,0,0,0}
			lmtVb.Oid = "1.3.6.1.4.1.5105.100.1.9.5.1.1.18"; // boardAlarmStatics {0,0,0,0}
			lmtVb.Oid = "1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11"; // ethLocalMacAddress  00:1e:a8:ff:ff:ff
			lmtVb.Oid = "1.3.6.1.4.1.5105.100.2.4.1.5.1.1.5"; // arpStatusDstMac
			lmtPdu.AddVb(lmtVb);

			long requestId;
			if (0 != lmtbSnmpEx.SnmpGetSync(lmtPdu, out requestId, destIpAddr, 10))
			{
				Debug.WriteLine("方法异常");
				return;
			}
			if (lmtPdu.m_LastErrorStatus != 0)
			{
				Debug.WriteLine("SNMP响应错误");
				// 根据响应状态码处理错误

				return;
			}
			// 获取正常
			for (int index = 0; index < lmtPdu.VbCount(); index++)
			{
				CDTLmtbVb lmtVbTmp = null;
				lmtPdu.GetVbByIndex(index, ref lmtVbTmp);
				logMsg = string.Format("Oid:{0}; Value:{1}", lmtVbTmp.Oid, lmtVbTmp.Value);
				Debug.WriteLine(logMsg);
			}
			

			Debug.WriteLine("===============");
		}

		[TestMethod()]
		public void GetNextTest()
		{
			string logMsg;
			string commnuity = "public";
//			string commnuity = "dtm.123";
			string destIpAddr = "172.27.245.91";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);


			// 1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11.0.0.1.0: 001ea859a85b
			// 2.7.13.1.1 hlGlobalTestSwitchSrsCfgSwitch SRS配置开关
			// 1.3.6.1.4.1.5105.100.1.2.1.1.1.5.3


			List<string> oidList = new List<string>();
			Dictionary<string, string> result = new Dictionary<string, string>();
			Dictionary<string, string> oidValueMap;
			List<string> lastOidList = new List<string>();

			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11");
			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.12");
			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.13");
			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.14");

			while (true)
			{
				lmtbSnmpEx.GetNextRequest(destIpAddr, oidList, out oidValueMap, out lastOidList);
				if (oidValueMap.Count() > 0) // 获取到数据
				{
					oidList.Clear();
					foreach (KeyValuePair<string, string> item in oidValueMap)
					{
						logMsg = $"oid={item.Key}, value={item.Value}";

						// 保存结果
						result.Add(item.Key, item.Value);
						// 回填新的oid，下次检索用
						oidList = new List<string>(lastOidList);

					}
				}
				else // 获取数据结束
				{
					break;
				}

			} // end while

			Debug.WriteLine("===============");

		}

		[TestMethod]
		public void GetNextLoopTest()
		{
			string logMsg;
			string commnuity = "public";
			//			string commnuity = "dtm.123";
			string destIpAddr = "172.27.245.91";

			LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
			// 启动SNMP通信
			int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);


			List<string> oidList = new List<string>();
			Dictionary<string, string> result = new Dictionary<string, string>();
			List<Dictionary<string, string>> oidValueTable;

			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11");//ethLocalMacAddress
			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.12");
			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.13");
			oidList.Add("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.14");


			if (lmtbSnmpEx.SnmpGetNextLoop(destIpAddr, oidList, out oidValueTable) == false)
			{
				Debug.WriteLine("执行方法错误");
			}
			// 循环行
			foreach(Dictionary<string, string> lineData in oidValueTable)
			{
				// 循环列
				foreach(KeyValuePair<string, string> oidValue in lineData)
				{
					logMsg = string.Format("oid:{0}, value:{1}", oidValue.Key, oidValue.Value);
					Debug.WriteLine(logMsg);
				}
			}

			Debug.WriteLine("===============");


		}

	}
}
