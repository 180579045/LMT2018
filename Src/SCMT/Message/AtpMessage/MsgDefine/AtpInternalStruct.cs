using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using OSP_MSGSELECT = System.UInt16;	/*消息类型定义*/
using OSP_MSGSIZE = System.UInt16;      /*消息长度定义*/
using OSP_LINKID = System.UInt16;       /*链路ID定义*/
using OSP_MSGOPT = System.Byte;         /*消息选项参数定义*/
using OSP_MSGPRIO = System.Byte;        /*消息优先级定义*/
using OSP_TRACEID = System.Byte;        /*消息跟踪开关定义*/
using OSP_RESERV = System.Byte;         /*保留字段定义*/
using OSP_SFUID = System.UInt32;        /*ATP增加*/

using u16 = System.UInt16;
using u8 = System.Byte;
using u32 = System.UInt32;

/// <summary>
/// ATP内部解析帧数据时用的结构
/// </summary>
namespace AtpMessage.MsgDefine
{
	//日志头文件中的时间定义,年月日时分秒和毫秒组成
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct STRU_Time : IASerialize
	{
		public u16 u16Year;
		public u8 u8Month;
		public u8 u8Day;
		public u8 u8Hour;
		public u8 u8Minute;
		public u8 u8Second;
		public u8 u8Pad;
		public u16 u16MilSec;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public u8[] u8Pad2;

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
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MilSec);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Month);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Day);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Hour);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Minute);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Second);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Pad);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MilSec);
			used += SerializeHelper.SerializeBytes(ref u8Pad2, 0, bytes, used + offset, 2);
			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => sizeof(u16) * 2 + sizeof(u8) * 8;
	};

	//日志文件中存储消息头定义
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct GTS_LogHeader : IASerialize
	{
		public u32 u32MsgNum;                   /*消息序号*/
		public u16 u16MsgLen;                   /*消息长度*/
		public u16 u16SubSfn;                   /*SUB_SFN*/
		public u8 u8Frame;                      /*机框*/
		public u8 u8Slot;                       /*槽位*/
		public u8 u8BoardStyle;                 /*板卡类型*/
		public u8 u8WorkMode;                   /*工作模式*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public u8[] u8IP;                     /*板卡IP*/
		public STRU_Time struTime;                     /*系统时间*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public u8[] u8Pad2;                    /*保留字段*/

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
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32MsgNum);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLen);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16SubSfn);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Frame);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Slot);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8BoardStyle);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8WorkMode);
			used += SerializeHelper.SerializeBytes(ref u8IP, 0, bytes, used + offset, 20);
			used += struTime.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.SerializeBytes(ref u8Pad2, 0, bytes, used + offset, 4);

			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => (ushort)(sizeof(u32) + sizeof(u16) * 2 + sizeof(u8) * 28 + struTime.ContentLen);
	};

	//ATP重新定义OSP相关的结构定义，必须跟基站一致
	//地址ID的定义，包括机框、槽位、处理器、sfutype
	public class ATP_OSP_SFUID_STRUCT : IASerialize
	{
		public u32 value;

		//TODO 大小端不同，以下字段的顺序也不同，需要调整
		public u32 SfuType => value & 0xFF000000;

		public u32 ProcId => value & 0x00FF0000;

		public u32 SlotId => value & 0x0000FF00;

		public u32 SubrackId => value & 0x000000FF;

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.SerializeInt32(ref ret, offset + used, value);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref value);
			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => sizeof(u32);
	};


	//osp头定义
	public struct ATP_OSP_STRU_MSGHEAD : IASerialize
	{
		public OSP_MSGSELECT MsgType;                /* 消息类型 */
		public OSP_MSGSIZE MsgSize;                /* 消息体长度，以字节为单位 */
		public ATP_OSP_SFUID_STRUCT SrcSfuId;    /* 源SFUID */
		public ATP_OSP_SFUID_STRUCT DstSfuId;    /* 目的SFUID */
		public u32 value;

		//TODO 判断大小端后需要调整顺序
		public u32 TimeStamp2 => value & 0xF8000000;        /* 时间戳2，用于记录时隙号:5bit */
		public u32 Reserv => value & 0x07000000;            /* 保留，供后续功能扩充:3bit */
		public u32 UsrId => value & 0x00F80000;             /* 用户标识:5bit */
		public u32 MsgPrio => value & 0x0070000;            /* 消息优先级:3bit */
		public u32 TimeStamp1 => value & 0x0000FFF8;        /* 时间戳1，用于记录子帧号:13bit */
		public u32 MsgFlag => value & 0x00000007;           /* 最后的3bit数据 */

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
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref MsgType);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref MsgSize);
			used += SrcSfuId.DeserializeToStruct(bytes, offset + used);
			used += DstSfuId.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref value);
			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => (ushort)(sizeof(OSP_MSGSELECT) + sizeof(OSP_MSGSIZE) + SrcSfuId.ContentLen +
									DstSfuId.ContentLen + sizeof(u32));
	}

}
