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
    public class CmdExecuteMgrTests
    {
        [TestMethod()]
        public void GetInstanceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CmdGetSyncTest()
        {
            Debug.WriteLine("kkk");


            string cmdName = "aaa";
            long requestId = 0;
            string strIndex = ".0";
            string strIpAddr = "192.168.5.198";
            LmtPdu lmtPdu = new LmtPdu2c();

            CmdExecuteMgr.GetInstance().CmdGetSync(cmdName, out requestId, strIndex
                , strIpAddr, ref lmtPdu);

 //           Assert.Fail();
        }
    }
}