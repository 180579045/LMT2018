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
    public class CDTCmdExecuteMgrTests
    {
        [TestMethod()]
        public void CmdGetSyncTest()
        {
            //_unity.Dispose();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            string cmdName = "aaa";
            long requestId = 0;
            string strIndex = ".0";
            string strIpAddr = "172.27.245.92";
            CDTLmtbPdu lmtbPdu = new CDTLmtbPdu();
 //           Debug.WriteLine("========= CmdGetSyncTest() 123 ==========");


            string commniuty = "public";
            DTLinkPathMgr dTLinkPathMgr = DTLinkPathMgr.GetInstance();
            dTLinkPathMgr.StartSnmp(commniuty, strIpAddr);


            CDTCmdExecuteMgr.GetInstance().CmdGetSync(cmdName, out requestId, strIndex, strIpAddr, ref lmtbPdu);


            cmdName = "bbb";
            CDTLmtbPdu lmtbPdu2 = new CDTLmtbPdu();
            CDTCmdExecuteMgr.GetInstance().CmdGetSync(cmdName, out requestId, strIndex, strIpAddr, ref lmtbPdu2);


            Debug.WriteLine("========= CmdGetSyncTest() End ==========");


        }


        [TestMethod()]
        public void CmdSetSyncTest()
        {
            string cmdName = "aaa";
            long requestId = 0;
            string strIndex = ".19";
            string strIpAddr = "172.27.245.92";
            CDTLmtbPdu lmtbPdu = new CDTLmtbPdu();
            //           Debug.WriteLine("========= CmdGetSyncTest() 123 ==========");


            string commniuty = "public";
            DTLinkPathMgr dTLinkPathMgr = DTLinkPathMgr.GetInstance();
            dTLinkPathMgr.StartSnmp(commniuty, strIpAddr);

            Dictionary<string, string> name2Value = new Dictionary<string, string>();
            name2Value.Add("fileTransRowStatus", Convert.ToString(4));
            name2Value.Add("fileTransType", Convert.ToString(27));
            name2Value.Add("fileTransIndicator", Convert.ToString(1));
            name2Value.Add("fileTransNEDirectory", null);
            name2Value.Add("fileTransFTPDirectory", "e:\\afyf\\src\\Lmt\\out\\release\\bin\\data\\AlarmFile\\TempFiles\\");
            name2Value.Add("fileTransFileName", null);



            int rs = CDTCmdExecuteMgr.GetInstance().CmdSetSync(cmdName, out requestId, name2Value, strIndex, strIpAddr, ref lmtbPdu);


          

            Debug.WriteLine("========= CmdGetSyncTest() End ==========");


        }
    }
}