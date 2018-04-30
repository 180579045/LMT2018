using System;
using System.Text;
using System.Collections.Generic;
using CommonUility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonUilityTest
{
	/// <summary>
	/// SerializeHelperTest 的摘要说明
	/// </summary>
	[TestClass]
	public class SerializeHelperTest
	{
		public SerializeHelperTest()
		{
			//
			//  在此处添加构造函数逻辑
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///获取或设置测试上下文，该上下文提供
		///有关当前测试运行及其功能的信息。
		///</summary>
		public TestContext TestContext
		{
			get { return testContextInstance; }
			set { testContextInstance = value; }
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
		public void TestSerializeByte()
		{
			byte[] data = new byte[5];
			int used = 0;
			used += SerializeHelper.SerializeByte(ref data, used, 0x1A);
			used += SerializeHelper.SerializeByte(ref data, used, 20);
			used += SerializeHelper.SerializeByte(ref data, used, 255);

			Assert.AreEqual(3, used);
			Assert.AreEqual(0x1A, data[0]);
			Assert.AreEqual(20, data[1]);
			Assert.AreEqual(255, data[2]);
			Assert.AreEqual(0, data[3]);
		}

		[TestMethod]
		public void TestDeSerializeByte()
		{
			byte[] data = {22, 55, 0xAB, 255, 0};
			int used = 0;
			byte actual = 0;
			used += SerializeHelper.DeserializeByte(data, used, ref actual);
			Assert.AreEqual(22, actual);

			used += SerializeHelper.DeserializeByte(data, used, ref actual);
			Assert.AreEqual(55, actual);

			used += SerializeHelper.DeserializeByte(data, used, ref actual);
			Assert.AreEqual(171, actual);

			used += SerializeHelper.DeserializeByte(data, used, ref actual);
			Assert.AreEqual(255, actual);

			used += SerializeHelper.DeserializeByte(data, used, ref actual);
			Assert.AreEqual(0, actual);
		}
	}
}
