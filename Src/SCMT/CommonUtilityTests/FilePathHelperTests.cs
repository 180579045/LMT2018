using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility.Tests
{
	[TestClass()]
	public class FilePathHelperTests
	{
		[TestMethod()]
		public void GetFileNameFromFullPathTest()
		{
			var fullPath = "/ata2/VER/dpaa_eth_init";

			var fileName = FilePathHelper.GetFileNameFromFullPath(fullPath);
			Assert.AreEqual(fileName, "dpaa_eth_init");

			var dirName = FilePathHelper.GetFileParentFolder(fullPath);
			Assert.AreEqual(dirName, "/ata2/VER/");

			fullPath = "e:/cur.cfg";
			fileName = FilePathHelper.GetFileNameFromFullPath(fullPath);
			Assert.AreEqual(fileName, "cur.cfg");

			dirName = FilePathHelper.GetFileParentFolder(fullPath);
			Assert.AreEqual(dirName, "e:/");
		}
	}
}