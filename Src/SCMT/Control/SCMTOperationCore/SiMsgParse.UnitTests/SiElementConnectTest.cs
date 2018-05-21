using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using CommonUility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCMTOperationCore.Connection;
using SCMTOperationCore.Elements;
using SCMTOperationCore.Message.SI;

namespace SiMsgParse.UnitTests
{
	/// <summary>
	/// SiElementConnectTest 的摘要说明
	/// </summary>
	[TestClass]
	public class SiElementConnectTest
	{
		public SiElementConnectTest()
		{
			neElement = new SiElement("test", IPAddress.Parse("127.0.0.1"));
		}

		private SiElement neElement;

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
			neElement.Connect();
			Assert.AreEqual(ConnectionState.Connected, neElement.ConnectState);
			Assert.IsTrue(neElement.HasConnected());
			neElement.DisConnect();
		}

		[TestMethod]
		public void TestSendMsg()
		{
			neElement.Connect();

			SI_LMTENBSI_GetFileInfoReqMsg req = new SI_LMTENBSI_GetFileInfoReqMsg();
			req.SetPath("c:\\windows");
			Assert.IsNotNull(req);

			byte[] reqBytes = SerializeHelper.SerializeStructToBytes(req);
			Assert.IsNotNull(reqBytes);
			Assert.AreEqual(req.Len, reqBytes.Length);

			bool sendResult = neElement.SendSiMsg(reqBytes);
			Assert.IsTrue(sendResult);

			neElement.DisConnect();
		}
	}
}
