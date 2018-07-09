using System;
using System.Text;
using System.Collections.Generic;
using CommonUility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonUilityTest
{
	/// <summary>
	/// FilePathHelperTest 的摘要说明
	/// </summary>
	[TestClass]
	public class FilePathHelperTest
	{

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
		public void TestDeleteFolder()
		{
			var path = "d:/test";
			FilePathHelper.DeleteFolder(path);
		}

		[TestMethod]
		public void TestCreateFolder()
		{
			var path = "d:/test/test";
			FilePathHelper.CreateFolder(path);

			path = "d:/test/test2";
			FilePathHelper.CreateFolder(path);
		}

		[TestMethod]
		public void TestCopyFile()
		{
			var src = "d:/test/test.txt";
			var dst = "e:/test/test2.log";

			bool ret = FilePathHelper.CopyFile(src, dst);
			Assert.IsTrue(ret);

			dst = "f:/test2.log";
			ret = FilePathHelper.CopyFile(src, dst);
			Assert.IsFalse(ret);
		}
	}
}
