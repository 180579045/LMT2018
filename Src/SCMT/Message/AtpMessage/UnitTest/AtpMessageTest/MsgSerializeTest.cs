using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtpMessage.MsgDefine;
using CommonUility;

namespace AtpMessageTest
{
	[TestClass]
	public class MsgSerializeTest
	{
		[TestMethod]
		public void TestBytesToStruct()
		{
			//此处构造一个登陆响应报文
			byte[] rsp = { 00, 03, 00, 00, 03, 0x22, 00, 04, 0x0a, 00, 01, 0xc0, 00, 01, 0xf0, 0xe4 };
			GtsMsgHeader header = GetHeaderFromBytes.GetHeader(rsp);
			Assert.AreEqual(802, header.u16Opcode);

			MsgGtsa2GtsmLogonRsp rspObj = new MsgGtsa2GtsmLogonRsp();
			rspObj.DeserializeToStruct(rsp, 0);
			Assert.AreEqual(header.u16Opcode, rspObj.header.u16Opcode);
		}
	}
}
