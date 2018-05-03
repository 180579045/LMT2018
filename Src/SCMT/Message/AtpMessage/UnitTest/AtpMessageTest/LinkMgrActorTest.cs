using System;
using System.Text;
using System.Collections.Generic;
using AtpMessage.LinkMgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpMessageTest
{
    /// <summary>
    /// LinkMgrActorTest 的摘要说明
    /// </summary>
    [TestClass]
    public class LinkMgrActorTest
    {
        public LinkMgrActorTest()
        {
            //
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetIpFromTopic()
        {
            string ip = LinkMgrActor.GetInstance().GetIpFromTopic("udp-recv://172.27.245.92:5002");
            Assert.AreEqual("172.27.245.92", ip);

            ip = LinkMgrActor.GetInstance().GetIpFromTopic("udp-recv://172.27.245.92");
            Assert.IsNull(ip);

            ip = LinkMgrActor.GetInstance().GetIpFromTopic("172.27.245.92:5002");
            Assert.IsNull(ip);

            ip = LinkMgrActor.GetInstance().GetIpFromTopic("to:172.27.245.92:5002");
            Assert.IsNull(ip);
        }

        [TestMethod]
        public void TestConnectNetElement()
        {
            string ip = "172.27.245.91";
            NetElementConfig config = new NetElementConfig()
            {
                TraceIp = "172.27.245.82",
                AgentSlot = 2,
                Index = 3,
                FrameNo = 0,
                SlotNo = 0,
                conType = ConnectType.ATP_DIRECT_LINK
            };

            bool bSucceed = LinkMgrActor.GetInstance().ConnectNetElement(ip, config);
            Assert.IsTrue(bSucceed);
            Assert.IsTrue(LinkMgrActor.GetInstance().HasLinkWithSameIp(ip));

            bSucceed = bSucceed = LinkMgrActor.GetInstance().ConnectNetElement(ip, config);
            Assert.IsFalse(bSucceed);

            try
            {
                bSucceed = bSucceed = LinkMgrActor.GetInstance().ConnectNetElement(ip, null);
                Assert.IsFalse(bSucceed);
            }
            catch (ArgumentNullException e)
            {
                string errMsg = e.ParamName;
                Assert.AreEqual("ip is null or empty, or neConfig is null", errMsg);
            }

            try
            {
                bSucceed = bSucceed = LinkMgrActor.GetInstance().DisconnectNe(null);
                Assert.IsTrue(bSucceed);
            }
            catch (ArgumentNullException e)
            {
                string errMsg = e.ParamName;
                Assert.AreEqual("ip is null or empty", errMsg);
            }

            bSucceed = LinkMgrActor.GetInstance().DisconnectNe(ip);
            Assert.IsTrue(bSucceed);

            bSucceed = LinkMgrActor.GetInstance().HasLinkWithSameIp(ip);
            Assert.IsFalse(bSucceed);
        }
    }
}
