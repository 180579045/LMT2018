using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

// 连接基站成功后查询SI端口版本号

namespace SCMTOperationCore.Message.SI
{
	public static class SiPortVerHelper
	{
		public static byte[] GetReqBytes()
		{
			SiMsgHead head = new SiMsgHead();
			head.u16MsgType = SiMacroDef.O_LMTOM_GET_SIPORTVERSION_REQ;
			head.u16MsgLength = head.ContentLen;

			return SerializeHelper.SerializeStructToBytes(head);
		}

		public static GetSiPortVersionRspMsg_Head GetRspHead(byte[] rspBytes)
		{
			return GetHeaderHelper.GetHeader<GetSiPortVersionRspMsg_Head>(rspBytes);
		}

		public static GetSiPortVersionRspMsg GetRspV1(byte[] rspBytes)
		{
			return GetHeaderHelper.GetHeader<GetSiPortVersionRspMsg>(rspBytes);
		}
	}

	public class GetSiPortVersionRspMsg_Head : IASerialize
	{
		public SiMsgHead head;
		public byte u8Result;    /*0-执行成功;1-执行失败*/
		public byte u8Version;   /*版本号*/
		public byte u8EndFlag;  /*是否为最后一个包，0：最后一个包，1：不是最后一个包*/
		public byte u8Pad;      /*保留此位*/

		public GetSiPortVersionRspMsg_Head()
		{
			head = new SiMsgHead();
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			throw new NotImplementedException();
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += head.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Result);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Version);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8EndFlag);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Pad);
			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => (sizeof(byte) * 4);
	}

	public class GetSiPortVersionRspMsg : IASerialize
	{
		public GetSiPortVersionRspMsg_Head msgHead;
		public byte u8UeInfo;		/*UE信息查询*/
		public byte u8UeMeasCfg;	/*UE测量配置*/
		public byte u8UeFree;		/*释放UE用户*/
		public byte u8UeOpeInfo;	/*UeIp信息查询*/
		public byte[] u8Pad1;		/*保留此位*/

		public GetSiPortVersionRspMsg()
		{
			msgHead = new GetSiPortVersionRspMsg_Head();
			u8Pad1 = new byte[44];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			throw new NotImplementedException();
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}
			int used = 0;
			used += msgHead.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8UeInfo);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8UeMeasCfg);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8UeFree);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8UeOpeInfo);
			used += 44;

			return used;
		}

		public int Len => msgHead.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * 48;
	}
}
