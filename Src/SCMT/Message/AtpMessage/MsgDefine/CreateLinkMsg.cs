using System;
using System.Runtime.InteropServices;
using CommonUtility;

namespace AtpMessage.MsgDefine
{
	/************************************
	消息宏：O_GTSMGTSA_ADDFLOW_REQ
	结构名：Msg_gtsmgtsa_AddFlow_Req
	源SFU：
	目的SFU：
	描述：建链请求
	**************************************/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaAddFlowReq : IASerialize
	{
		public GtsMsgHeader header;
		public uint u32IpType;					/*IP地址类型，IPV4/IPV6*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = GtsMsgType.IP_ADDRESS_LEN_V6)]
		public byte[] u8PcIpAdd;				/*PC侧的IP地址*/
		public ushort u16RackNo;				/*机架号，暂时不用，留待以后扩展*/
		public ushort u16FrameNo;				/*BPOE板卡机框号*/
		public ushort u16SlotNo;				/*BPOE板卡插槽号*/
		public ushort u16ProcId;				/*处理器号，暂时不用，以后扩展*/

		public MsgGtsm2GtsaAddFlowReq()
		{
			header = new GtsMsgHeader();
			u8PcIpAdd = new byte[GtsMsgType.IP_ADDRESS_LEN_V6];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeInt32(ref ret, offset + used, u32IpType);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, u8PcIpAdd, 0, GtsMsgType.IP_ADDRESS_LEN_V6);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16RackNo);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16FrameNo);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16SlotNo);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16ProcId);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(uint) + sizeof(byte) * GtsMsgType.IP_ADDRESS_LEN_V6 + sizeof(ushort) * 4;
	}

	/************************************
	消息宏：O_GTSAGTSM_ADDFLOW_RSP
	结构名：Msg_gtsagtsm_AddFlow_Rsp
	源SFU：
	目的SFU：
	描述：建链响应
	**************************************/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsa2GtsmAddFlowRsp : IASerialize
	{
		public GtsMsgHeader header;
		public ushort u16RackNo;			/*机架号，暂时不用，留待以后扩展*/
		public ushort u16FrameNo;			/*BPOE板卡机框号*/
		public ushort u16SlotNo;			/*BPOE板卡插槽号*/
		public ushort u16ProcId;			/*处理器号，暂时不用，以后扩展*/
		public int s32Result;				/*建链结果，0为成功，1为失败*/

		public MsgGtsa2GtsmAddFlowRsp()
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
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16RackNo);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16FrameNo);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16SlotNo);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16ProcId);
			uint u32Result = 0;
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32Result);
			s32Result = (int)u32Result;
			return used;
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(ushort) * 4 + sizeof(int);
	}
}
