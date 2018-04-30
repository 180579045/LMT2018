using System;
using System.Runtime.InteropServices;
using CommonUility;

namespace AtpMessage.MsgDefine
{
	/*过滤复位请求*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal struct MsgGtsm2GtsaFilterResetReq : IASerialize
	{
		public GtsMsgHeader header;                         /*GTS消息头*/
		public byte u8FilterReset;                          /*复位标志*/
		public byte u8DstFrame;                             /*远程BBU板的机框号*/
		public byte u8DstSlot;                              /*远程BBU板的插槽号*/
		public byte u8Padding;
		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8FilterReset);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstFrame);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstSlot);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8Padding);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public ushort ContentLen => sizeof(byte) * 4;

		public int Len => header.Len + ContentLen;
	};

	/*过滤复位响应消息*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal struct MsgGtsa2GtsmFilterResetRsp : IASerialize
	{
		public GtsMsgHeader header;                         /*GTS消息头*/
		public byte u8Complete;                             /*GTS_SUCCESS表示成功，GTS_FAILURE表示失败*/
		public byte u8DstFrame;                             /*远程BBU板的机框号*/
		public byte u8DstSlot;                              /*远程BBU板的插槽号*/
		public byte u8Padding;
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
			used += header.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Complete);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DstFrame);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DstSlot);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Padding);
			return used;
		}

		public ushort ContentLen => sizeof(byte) * 4;

		public int Len => header.Len + ContentLen;
	};
}
