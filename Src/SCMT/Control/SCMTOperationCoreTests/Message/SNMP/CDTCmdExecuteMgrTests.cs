using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCMTOperationCore.Message.SNMP;
using System;
using System.Collections.Generic;
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
            string cmdName = "aaa";
            long requestId = 0;
            string strIndex = ".0";
            string strIpAddr = "192.168.5.198";
            CDTLmtbPdu lmtbPdu = null;
            CDTCmdExecuteMgr.GetInstance().CmdGetSync(cmdName, out requestId, strIndex, strIpAddr, ref lmtbPdu);


        }
    }
}