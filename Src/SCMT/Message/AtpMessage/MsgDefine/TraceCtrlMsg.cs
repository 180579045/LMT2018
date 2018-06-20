using System;
using System.Runtime.InteropServices;
using CommonUtility;

namespace AtpMessage.MsgDefine
{
	/*控制DSP消息抄送开关，先于跟踪控制消息发送*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaDspTraceCtrlReq : IASerialize
	{
		public GtsMsgHeader header;
		public byte u8FrameNo;
		public byte u8SlotNo;
		public byte u8Switch;						/* 0:关闭抄送   1:开启抄送*/
		public byte u8Pad;

		public MsgGtsm2GtsaDspTraceCtrlReq()
		{
			header = new GtsMsgHeader();
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8FrameNo);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8SlotNo);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8Switch);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8Pad);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * 4;
	}

	/*跟踪控制消息*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaTraceCtrlReq : IASerialize
	{
		public GtsMsgHeader header;						/*GTS消息头*/
		public byte u8DstFrame;								/*远程BBU板的机框号*/
		public byte u8DstSlot;								/*远程BBU板的插槽号*/
		public byte u8TraceMode;							/*是否按用户跟踪标志，值为0表示TRACE_ALL_USER，值为1表示TRACE_SPECIAL_USER */
		public byte u8Padding;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = GtsMsgType.MAX_TRACE_NUM)]
		public byte[] u8TraceControl;						/*跟踪开关*/

		public MsgGtsm2GtsaTraceCtrlReq()
		{
			header = new GtsMsgHeader();
			u8TraceControl = new byte[GtsMsgType.MAX_TRACE_NUM];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstFrame);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstSlot);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8TraceMode);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8Padding);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, u8TraceControl, 0, GtsMsgType.MAX_TRACE_NUM);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * (4 + GtsMsgType.MAX_TRACE_NUM);
	};

	/*跟踪控制响应*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsa2GtsmTraceCtrlRsp : IASerialize
	{
		public GtsMsgHeader header;                         /*GTS消息头*/
		public byte u8Complete;                             /*GTS_SUCCESS表示成功，GTS_FAILURE表示失败*/
		public byte u8DstFrame;                             /*远程BBU板的机框号*/
		public byte u8DstSlot;                              /*远程BBU板的插槽号*/
		public byte u8Padding;

		public MsgGtsa2GtsmTraceCtrlRsp()
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
