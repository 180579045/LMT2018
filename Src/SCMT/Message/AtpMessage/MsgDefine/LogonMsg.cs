using System;
using System.Runtime.InteropServices;
using CommonUtility;

namespace AtpMessage.MsgDefine
{
	/*登录请求O_GTSMGTSA_LOGON_REQ*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class MsgGtsm2GtsaLogonReq : IASerialize
	{
		public GtsMsgHeader header;							/*GTS消息头*/
		public ushort u16DataLength;						/*数据长度*/
		public ushort u16Port;								/*Eth数据侦听端口号*/
		public int s32RequestId;
		public uint u32TraceDspIpAddr;						/*跟踪DSP的IP地址*/
		public ushort u16LinkId;							/*链路号*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] u8MacAddr;							/*PC的MAC*/
		public byte u8DstFrame;								/*远程BBU板的机框号*/
		public byte u8DstSlot;								/*远程BBU板的插槽号*/
		public byte u8AgentSlot;							/*代理CCU板槽位号*/
		public byte u8Padding;

		public MsgGtsm2GtsaLogonReq()
		{
			u8MacAddr = new byte[6];
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
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16DataLength);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16Port);
			used += SerializeHelper.SerializeInt32(ref ret, offset + used, (uint)s32RequestId);
			used += SerializeHelper.SerializeInt32(ref ret, offset + used, u32TraceDspIpAddr);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16LinkId);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, u8MacAddr, 0, 6);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstFrame);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstSlot);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8AgentSlot);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8Padding);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(ushort) * 3 + sizeof(byte) * 10 + sizeof(uint) * 2;
	}

	/*登录响应O_GTSAGTSM_LOGON_RSP*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class MsgGtsa2GtsmLogonRsp : IASerialize
	{
		public GtsMsgHeader header;                         /*GTS消息头*/
		public byte u8DstFrame;                             /*远程BBU板的机框号*/
		public byte u8DstSlot;                              /*远程BBU板的插槽号*/
		public byte u8BoardType;                            /*板卡类型*/
		public byte u8Pad;

		public MsgGtsa2GtsmLogonRsp()
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
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DstFrame);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DstSlot);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8BoardType);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Pad);
			return used;
		}

		public ushort ContentLen => sizeof(byte) * 4;

		public int Len => header.Len + ContentLen;
	}


	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaQuitInd : IASerialize
	{
		public GtsMsgHeader header;                        /*GTS消息头*/
		public ushort u16DataLength;                        /*数据长度*/
		public byte u8DstFrame;                             /*远程BBU板的机框号*/
		public byte u8DstSlot;                              /*远程BBU板的插槽号*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = GtsMsgType.MAX_DIAG_MSG_LEN)]
		public byte[] u8Data;                               /*NBT使用的DIAG断开信息*/

		public MsgGtsm2GtsaQuitInd()
		{
			u8Data = new byte[GtsMsgType.MAX_DIAG_MSG_LEN];
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
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16DataLength);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstFrame);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DstSlot);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, u8Data, 0, 1024);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(ushort) + sizeof(byte) * (1024 + 2);
	};
}
