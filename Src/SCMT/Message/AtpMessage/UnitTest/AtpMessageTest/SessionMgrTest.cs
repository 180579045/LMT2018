﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using AtpMessage;
using AtpMessage.SessionMgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpMessageTest
{
	/// <summary>
	/// SessionMgrTest 的摘要说明
	/// </summary>
	[TestClass]
	public class SessionMgrTest
	{
		public SessionMgrTest()
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
		public void TestGetProcotol()
		{
			string topic = "/SessionService/Create/UDP";
			string actual = SessionService.GetInstance().GetProcotolTypeFromTopic(topic);
			Assert.AreEqual("UDP", actual);

			topic = "/SessionService/Create/";
			actual = SessionService.GetInstance().GetProcotolTypeFromTopic(topic);
			Assert.AreEqual("", actual);
		}

		[TestMethod]
		public void TestAtpInitial()
		{
			Type t = typeof(AtpInitial);
			MethodInfo mtInfo = t.GetMethod("Init");
			Assert.IsNotNull(mtInfo);

			int ret = (int)mtInfo.Invoke(null, null);
			Assert.AreEqual(11, ret);
		}
	}
}
