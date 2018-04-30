using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
//namespace AtpMessage.MsgDefine
//{
//	//日志头文件中的时间定义,年月日时分秒和毫秒组成
//	public struct STRU_Time
//	{
//		u16 u16Year;
//		u8 u8Month;
//		u8 u8Day;
//		u8 u8Hour;
//		u8 u8Minute;
//		u8 u8Second;
//		u8 u8Pad;
//		u16 u16MilSec;
//		u8 u8Pad2[2];
//	};

//	//日志文件中存储消息头定义
//	public struct GTS_LogHeader
//	{
//		u32 u32MsgNum;                    /*消息序号*/
//		u16 u16MsgLen;                    /*消息长度*/
//		u16 u16SubSfn;                    /*SUB_SFN*/
//		u8 u8Frame;                      /*机框*/
//		u8 u8Slot;                       /*槽位*/
//		u8 u8BoardStyle;                 /*板卡类型*/
//		u8 u8WorkMode;                   /*工作模式*/
//		u8 u8IP[20];                     /*板卡IP*/
//		STRU_Time struTime;                     /*系统时间*/
//		u8 u8Pad2[4];                    /*保留字段*/
//	} ;

////ATP重新定义OSP相关的结构定义，必须跟基站一致
////地址ID的定义，包括机框、槽位、处理器、sfutype
//	public struct ATP_OSP_SFUID_STRUCT
//	{
//		u32 SfuType : 8;
//		u32 ProcId : 8;
//		u32 SlotId : 8;
//		u32 SubrackId : 8;
//	};


////osp头定义
//	public struct ATP_OSP_STRU_MSGHEAD
//	{
//		OSP_MSGSELECT MsgType;                /* 消息类型                           */
//		OSP_MSGSIZE MsgSize;                /* 消息体长度，以字节为单位           */
//		ATP_OSP_SFUID_STRUCT SrcSfuId;    /* 源SFUID                            */
//		ATP_OSP_SFUID_STRUCT DstSfuId;    /* 目的SFUID                          */
//		u32 TimeStamp2:5;           /* 时间戳2，用于记录时隙号           */
//		u32 Reserv:3;               /* 保留，供后续功能扩充              */
//		u32 UsrId:5;                /* 用户标识                          */
//		u32 MsgPrio:3;              /* 消息优先级                        */
//		u32 TimeStamp1:13;          /* 时间戳1，用于记录子帧号           */
//		u32 MsgFlag:3;
//	}

//}
