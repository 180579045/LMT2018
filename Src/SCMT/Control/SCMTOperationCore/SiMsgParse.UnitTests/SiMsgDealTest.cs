using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCMTOperationCore.Elements.BaseElement;
using SCMTOperationCore.Message.SI;

namespace SiMsgParse.UnitTests
{
	[TestClass]
	public class SiMsgDealTest
	{
		[TestMethod]
		public void TestSiMsgParse()
		{
			SI_SILMTENB_GetFileInfoRspMsg rsp = new SI_SILMTENB_GetFileInfoRspMsg();
			rsp.head.u16MsgLength = 10240;
			rsp.head.u16MsgType = 0x41;

			byte[] temp = new byte[rsp.Len];
			rsp.SerializeToBytes(ref temp, 0);

			byte[] part1 = new byte[10200];
			Buffer.BlockCopy(temp, 0, part1, 0, temp.Length);

			SiMsgDealer dealer = new SiMsgDealer("");

			dealer.DealSiMsg(part1);		//应该是不够的，还需要40个字节才能解析数据包

			byte[] part2 = new byte[50];
			part2[40] = 0x00;
			part2[41] = 0x04;

			dealer.DealSiMsg(part2);		//第一个消息可以解完，第二个消息也可以解完，还剩几个字节
		}
	}
}
