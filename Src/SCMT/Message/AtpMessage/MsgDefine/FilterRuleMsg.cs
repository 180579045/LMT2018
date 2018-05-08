using System;
using System.Runtime.InteropServices;
using CommonUility;

namespace AtpMessage.MsgDefine
{
	/*过滤条件消息*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaFilterRuleReq : IASerialize
	{
		public GtsMsgHeader header;                         /*GTS消息头*/
		public byte u8FilterSign;                           /*RULE_FILTER表示过滤, RULE_HODE表示保留*/
		public byte u8FilterNumber;                         /*过滤条件个数*/
		public byte u8DstFrame;                             /*远程BBU板的机框号*/
		public byte u8DstSlot;                              /*远程BBU板的插槽号*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public uint[] u32FilterOpcode;						/*过滤条件*/

		public MsgGtsm2GtsaFilterRuleReq()
		{
			header = new GtsMsgHeader();
			u32FilterOpcode = new uint[100];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8FilterSign);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8FilterNumber);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstFrame);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstSlot);
			used += SerializeHelper.SerializeUintArray(ref ret, offset + used, u32FilterOpcode, 0, u32FilterOpcode.Length);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public ushort ContentLen => sizeof(byte) * 4 + sizeof(uint) * 100;

		public int Len => header.Len + ContentLen;
	};

	/*过滤条件响应*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsa2GtsmFilterRuleRsp : IASerialize
	{
		public GtsMsgHeader header;                         /*GTS消息头*/
		public byte u8Complete;                             /*GTS_SUCCESS表示成功，GTS_FAILURE表示失败*/
		public byte u8DstFrame;                             /*远程BBU板的机框号*/
		public byte u8DstSlot;                              /*远程BBU板的插槽号*/
		public byte u8Padding;

		public MsgGtsa2GtsmFilterRuleRsp()
		{
			header = new GtsMsgHeader();
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
			used += header.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Complete);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DstFrame);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DstSlot);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Padding);
			return used;
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * 4;
	};
}
