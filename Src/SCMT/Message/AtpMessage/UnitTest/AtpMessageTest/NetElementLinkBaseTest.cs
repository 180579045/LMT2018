using System;
using System.Text;
using System.Collections.Generic;
using AtpMessage.LinkMgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpMessageTest
{
    /// <summary>
    /// NetElementLinkBaseTest 的摘要说明
    /// </summary>
    [TestClass]
    public class NetElementLinkBaseTest
    {
        public NetElementLinkBaseTest()
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
        public void TestConnect()
        {
            NetElementLinkBase link = LinkFactory.CreateLink(ConnectType.ATP_DIRECT_LINK) as NetElementLinkBase;
            Assert.AreEqual(NetElementLinkBase.LinkState.Disconnected, link.State);
            Assert.IsTrue(link.IsBreak);

            try
            {
                link.Connect(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("netElementAddress is null", e.ParamName);
            }

            NetElementConfig config = new NetElementConfig()
            {
                TraceIp = "172.27.245.82",
                AgentSlot = 2,
                Index = 3,
                FrameNo = 0,
                SlotNo = 0,
                conType = ConnectType.ATP_DIRECT_LINK
            };

            link.Connect(config);
            Assert.AreEqual(NetElementLinkBase.LinkState.Connecting, link.State);
            Assert.IsFalse(link.IsBreak);

            link.OnLogonResult(true);
            Assert.AreEqual(NetElementLinkBase.LinkState.Connencted, link.State);
            Assert.IsFalse(link.IsBreak);

            link.Disconnect();
            Assert.AreEqual(NetElementLinkBase.LinkState.Disconnected, link.State);
            Assert.IsTrue(link.IsBreak);
        }
    }
}
