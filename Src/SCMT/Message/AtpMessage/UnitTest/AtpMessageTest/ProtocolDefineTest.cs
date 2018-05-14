using System;
using System.Text;
using System.Collections.Generic;
using AtpMessage.GtsMsgParse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpMessageTest
{
	/// <summary>
	/// ProtocolDefineTest 的摘要说明
	/// </summary>
	[TestClass]
	public class ProtocolDefineTest
	{
		public ProtocolDefineTest()
		{

		}

		/// <summary>
		///获取或设置测试上下文，该上下文提供
		///有关当前测试运行及其功能的信息。
		///</summary>
		public TestContext TestContext { get; set; }

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
		public void TestEtherHeaderInit()
		{
			byte[] src_mac = new byte[6] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66};
			byte[] dst_mac = new byte[6] { 0x77, 0x88, 0x99, 0x00, 0xaa, 0xbb };
			byte[] p_type = new byte[2] {0x00, 0x11};

			ETHERNET_HEADER header = new ETHERNET_HEADER()
			{
				des_mac = dst_mac,
				src_mac = src_mac,
				type = p_type
			};

			Assert.AreEqual(14, header.Len);
		}

		[TestMethod]
		public void TestEthernetHeaderDe()
		{
			byte[] data = new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0x00, 0xaa, 0xbb, 0x00, 0x11 };
			ETHERNET_HEADER header = new ETHERNET_HEADER();
			int ret = header.DeserializeToStruct(data, 0);
			Assert.AreEqual(data.Length, ret);

			for (int i = 0; i < 6; i++)
			{
				Assert.AreEqual(data[i], header.des_mac[i]);
			}
		}
	}
}
