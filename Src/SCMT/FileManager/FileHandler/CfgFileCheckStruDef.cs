using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// CFG文件校验所需的结构体定义
namespace FileManager.FileHandler
{
	public class CfgCheckMacro
	{
		public const int OMIC_VERIFY_STRING_LEN = 4;
		public const int OMIC_FILEDESC_LEN_MAX = 256;
	}

	/* 初配文件头结构定义 */
	[StructLayout(LayoutKind.Sequential)]
	public struct OMIC_STRU_ICFile_Header
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = CfgCheckMacro.OMIC_VERIFY_STRING_LEN)]
		public byte[] u8Flag;					/* 类型标记；也是一个数据单元开始的标记  */

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] u32NodebVer;				/*Nodeb基站类型（1 版、2版、3版超级基站）*/
		public uint u32IcFileVer;				/* 初配文件版本：用来标志当前文件的大版本*/
		public uint u32ReserveVer;				/* 初配文件小版本：用来区分相同大版本下的因取值不同造成的差异*/
		public uint u32DataBlk_Location;		/* 数据块起始位置 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] u8LastMotifyDate;			/* 文件最新修改的日期, 按字符串存放 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public uint[] u32IcFile_HeaderVer;		/* 初配文件头版本，用于记录不同的文件头格式、版本 */
		public uint u32IcFile_HeaderLength;		/* 初配文件头部长度 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = CfgCheckMacro.OMIC_FILEDESC_LEN_MAX)]
		public byte[] u8IcFileDesc;				/*  文件描述 */
		public uint u32RevDatType;				/* 保留段数据类别 (1: 文件描述) */
		public uint u32IcfFileType;				/* 初配文件类别（1:NB,2:RRS）*/
		public uint u32IcfFileProperty;			/* 初配文件属性（0:正式文件;1:补充文 ; 2:pdg文件*/
		public uint u32NobeBType;				/*1:超站，2：小站   2007-03-28修改 */

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
		public uint[] reserved;					/* 保留字段    */

		public static int SizeOf()
		{
			var byteLen = sizeof(byte) * (CfgCheckMacro.OMIC_VERIFY_STRING_LEN + 4 + 20 +
			                              CfgCheckMacro.OMIC_FILEDESC_LEN_MAX);
			var intLen = sizeof(uint) * (8 + 13 + 4);
			return byteLen + intLen;
		}
	}

	/* 数据块头 */
	[StructLayout(LayoutKind.Sequential)]
	public struct OMIC_STRU_ICFile_DataBlk_Header
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = CfgCheckMacro.OMIC_VERIFY_STRING_LEN)]
		public byte[] u8VerifyString;				/* 段头校验标识。（为指定字符串）*/
		public uint u32InstanceDataBlk_Offset;		/* 实例数组起始位置（到段头的偏移量）*/
		public uint u32InstanceNum;					/* 存放的实例个数 */
		public uint u32StrDataBlk_Offset;			/* 字符串部分数据起始位置（到段头的偏移量）,存放实例数组中的字符串值,在实例数组中记录其索引 */
		public uint u32StrNum;						/* 存放的字符串个数 */
		public uint u32OIDDataBlk_Offset;			/* OID部分数据起始位置（到段头的偏移量）,存放实例数组中OID值 */
		public uint u32OIDNum;						/* 存放的OID个数 */
		public uint u32DataBlkLen;					/* 数据块数据部分长度（不包括段头）*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public uint[] reserved;						/* 保留字段*/

		public static int SizeOf()
		{
			var byteLen = sizeof(byte) * CfgCheckMacro.OMIC_VERIFY_STRING_LEN;
			var intLen = sizeof(uint) * 11;
			return byteLen + intLen;
		}
	}

	/* 数据块的头 ，为将来堆叠准备*/
	[StructLayout(LayoutKind.Sequential)]
	struct DataHead
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] u8VerifyStr;            /* 文件头的校验字段 "BEG\0"  */
		public uint u32DatType;               /* reserved , =1 */
		public uint u32DatVer;                /* reserved , =1 */
		public uint u32TableCnt;              /* 表的数目,指示索引表中的向目个数*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public uint[] u32Reserved;          /* 保留 */

		public static int SizeOf()
		{
			var byteLen = sizeof(byte) * 4;
			var intLen = sizeof(uint) * 5;
			return byteLen + intLen;
		}
	};

	/* 每个索引项*/
	public struct OM_STRU_IcfIdxTableItem
	{
		public uint u32CurTblOffset;			/* 每个表的数据在文件中的起始位置（相对文件头） */

		public static int SizeOf()
		{
			return sizeof(uint);
		}
	};

	/*数据文件表信息*/
	[StructLayout(LayoutKind.Sequential)]
	public struct OM_STRU_CfgFile_TblInfo
	{
		public ushort u16DataFmtVer;	/* 数据文件版本（即数据更新次数） */

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] u8pad;			/* 保留字段*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] s8TblName;		/* 表名 */

		public ushort u16FieldNum;		/* 本表的单个记录包含的字段数 */
		public ushort u16RecLen;		/* 单个记录的有效长度（单位：字节） */
		public uint u32RecNum;			/* 数据文件中包含的记录数量--实例数*/

		public static int SizeOf()
		{
			return sizeof(byte) * 34 + sizeof(ushort) * 3 + sizeof(uint);
		}
	};

	/*数据文件表字段信息*/
	[StructLayout(LayoutKind.Sequential)]
	public struct OM_STRU_CfgFile_FieldInfo
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
		public byte[] s8FieldName;			/* 字段名 */
		public ushort u16FieldOffset;		/* 字段相对记录头偏移量*/
		public ushort u16FieldLen;			/* 字段长度 单位：字节 */
		public byte u8FieldType;			/* 字段类型 */
		public byte u8FieldTag;				/* 字段是否为关键字 */
		public byte u8SaveTag;				/* 字段是否需要存盘 */
		public byte u8ConfigFlag;			/* 字段是否可(需要)配置,0:不可配，1：可配*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] u8Pad;                /* 保留*/

		public static int SizeOf()
		{
			return sizeof(byte) * 56 + sizeof(ushort) * 2;
		}
	}
}
