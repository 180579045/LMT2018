using System;
using System.Runtime.InteropServices;
using CommonUility;

namespace AtpMessage.MsgDefine
{
	/************************************
	消息宏：O_GTSMGTSA_ALIVE_RPT
	描述：ATP发给GTSA的存活消息
	**************************************/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaAliveRpt : IASerialize
	{
		public GtsMsgHeader header;              /*GTSA头*/
		public byte u8FrameNo;
		public byte u8SlotNo;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] u8Pad;

		public MsgGtsm2GtsaAliveRpt()
		{
			header = new GtsMsgHeader();
			u8Pad = new byte[2];
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
			used += 2;
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public ushort ContentLen => sizeof(byte) * 4;

		public int Len => header.Len + ContentLen;
	}
}
