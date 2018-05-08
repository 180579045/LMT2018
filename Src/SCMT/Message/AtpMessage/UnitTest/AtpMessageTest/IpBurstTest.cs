using System;
using System.Text;
using System.Collections.Generic;
using AtpMessage.GtsMsgParse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpMessageTest
{
	/// <summary>
	/// IP组包单元测试
	/// </summary>
	[TestClass]
	public class IpBurstTest
	{
		public IpBurstTest()
		{
			worker = new GtsMsgParseWorker();
		}

		private GtsMsgParseWorker worker;
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
		public void TestAddNewIpPacket()
		{
			//模拟IP分片无序到达的情况
			ushort ipId = 100;
			ushort frameOffset = 2;
			byte[] frameData = new byte[] {1, 1, 1, 1};

			worker.AddNewIpPacket(ipId, frameOffset, frameData);

			frameOffset = 1;
			frameData = new byte[] { 0, 0, 0, 0};
			worker.AddNewIpPacket(ipId, frameOffset, frameData);

			frameOffset = 3;
			frameData = new byte[] { 0, 1, 0, 1 };
			worker.AddNewIpPacket(ipId, frameOffset, frameData);

			byte[] expected = new byte[] {0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1};
			byte[] actual = worker.IpBurst(ipId);

			Assert.AreEqual(expected.Length, actual.Length);

			for (int i = 0; i < expected.Length; i++)
			{
				Assert.AreEqual(expected[i], actual[i]);
			}
		}
	}
}
