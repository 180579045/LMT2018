using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileManager.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileHandler.Tests
{
	[TestClass()]
	public class CfgFileHandlerTests
	{
		[TestMethod()]
		public void DoPutFileTest()
		{
			var cfgHandler = new CfgFileHandler("172.27.245.92");
			string strErrMsg;
			cfgHandler.DoPutFile("e:/cur.cfg", "/ata2/VER/CFG/cur.cfg", out strErrMsg);
		}

		[TestMethod()]
		public void CheckCfgTest()
		{
			var cfgHandler = new CfgFileHandler("172.27.245.92");

			string errInfo = "";
			cfgHandler.CheckCfgFile("e:/cur.cfg", ref errInfo);
		}
	}
}