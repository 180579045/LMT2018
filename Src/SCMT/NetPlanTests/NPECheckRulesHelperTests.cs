using CommonUtility;
using LogManager;
using MIBDataParser.JSONDataMgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.Tests
{
    [TestClass()]
    public class NPECheckRulesHelperTests
    {
        public NPECheckRulesHelperTests()
        {
            Log.SetLogFileName("NPECheckRulesHelperTests.log");
        }
        private async Task simConnectEnb()
        {
            CSEnbHelper.SetCurEnbAddr("172.27.245.92");
            var db = Database.GetInstance();
            var result = await db.initDatabase("172.27.245.92");
        }

        [TestMethod()]
        public async Task CheckNetPlanDataTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = NPECheckRulesDealTests.SimGetNetPlanEnbMib();
            string tip;
            bool res = NPECheckRulesHelper.CheckNetPlanData(mapMib_this, 10, out tip);
            Assert.IsTrue(res == true);
        }
    }
}