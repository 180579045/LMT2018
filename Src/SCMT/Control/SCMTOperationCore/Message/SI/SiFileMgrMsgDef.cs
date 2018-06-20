using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;


//文件管理模块需要用到的SI消息结构体定义文件

namespace SCMTOperationCore.Message.SI
{
	using System.Diagnostics;
	using SIs8 = Char;
	using SIu8 = Byte;

	//助手函数。TODO 可以和其他的几个合并在一起，做一个模板
	public class SiMsgHelper
	{
		public static SI_LMTENBSI_MsgHead GetSiMsgHead(byte[] data, int offset = 0)
		{
			SI_LMTENBSI_MsgHead head = new SI_LMTENBSI_MsgHead();
			if (-1 == head.DeserializeToStruct(data, offset))
				return null;

			return head;
		}
	}

	//SI消息头
	public class SI_LMTENBSI_MsgHead : IASerialize
	{
		public ushort u16MsgLength;     /* 消息长度。整个报文的长度。 */
		public ushort u16MsgType;       /* 消息类型 */

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			int u = SerializeHelper.SerializeUshort(ref ret, offset + used, u16MsgLength);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.SerializeUshort(ref ret, offset + used, u16MsgType);
			if (-1 == u) return -1;
			used += u;

			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			int u = SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType);
			if (-1 == u) return -1;
			used += u;

			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => sizeof(ushort) * 2;
	}

	/***********************************************
	消息宏:  O_LMTENBSI_GETFILEINFO_REQ
	结构名: SI_LMTENBSI_GetFileInfoReqMsg
	描述:  文件信息查询请求消息
	***********************************************/
	public class SI_LMTENBSI_GetFileInfoReqMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8SrcPath;		/* 查询路径 */

		public SI_LMTENBSI_GetFileInfoReqMsg()
		{
			s8SrcPath = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
			head = new SI_LMTENBSI_MsgHead();
			head.u16MsgLength = (ushort)(head.Len + SiMacroDef.SI_FILEPATH_MAX_LEN);
			head.u16MsgType = SiMacroDef.O_LMTENBSI_GETFILEINFO_REQ;
		}

		public SI_LMTENBSI_GetFileInfoReqMsg(string path)
		{
			s8SrcPath = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
			head = new SI_LMTENBSI_MsgHead();
			head.u16MsgLength = (ushort)(head.Len + SiMacroDef.SI_FILEPATH_MAX_LEN);
			head.u16MsgType = SiMacroDef.O_LMTENBSI_GETFILEINFO_REQ;

			SetPath(path);
		}

		public bool SetPath(byte[] pathBytes, int offset = 0)
		{
			if (null == pathBytes || offset < 0 || pathBytes.Length < offset)
			{
				return false;
			}

			int dataLen = pathBytes.Length - offset;
			int min = Math.Min(SiMacroDef.SI_FILEPATH_MAX_LEN, dataLen);

			Buffer.BlockCopy(pathBytes, offset, s8SrcPath, 0, min);
			return true;
		}

		public void SetPath(string path)
		{
			if (null == path)
			{
				return;
			}

			int minPath = Math.Min(SiMacroDef.SI_FILEPATH_MAX_LEN, path.Length);

			var pathBytes = Encoding.Default.GetBytes(path);

			Buffer.BlockCopy(pathBytes, 0, s8SrcPath, 0, minPath);
		}

		public static implicit operator SI_LMTENBSI_GetFileInfoReqMsg(byte[] pathBytes)
		{
			SI_LMTENBSI_GetFileInfoReqMsg temp = new SI_LMTENBSI_GetFileInfoReqMsg();
			temp.SetPath(pathBytes, 0);
			return temp;
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			int u = head.SerializeToBytes(ref ret, offset + used);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.SerializeBytes(ref ret, offset + used, s8SrcPath, 0, SiMacroDef.SI_FILEPATH_MAX_LEN);
			if (-1 == u) return -1;
			used += u;

			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			int u = head.DeserializeToStruct(bytes, offset + used);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.SerializeBytes(ref s8SrcPath, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			if (-1 == u) return -1;
			used += u;

			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => SiMacroDef.SI_FILEPATH_MAX_LEN;
	}

	/**************************************************
	消息宏: O_SILMTENB_GETFILEINFO_RES
	结构名: SI_SILMTENB_GetFileInfoRspMsg
	描述:   文件信息查询响应消息
	**************************************************/
	public class SI_SILMTENB_GetFileInfoRspMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8SrcPath;		/* 查询目录 */
		public byte s8GetResult;		/* 0:成功;1:失败 */
		public ushort u16FileNum;		/* 待查询目录下的文件数 */
		public SI_STRU_FileInfo[] struFileInfo;		/* 文件信息结构数组 */

		public SI_SILMTENB_GetFileInfoRspMsg()
		{
			s8SrcPath = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
			struFileInfo = new SI_STRU_FileInfo[SiMacroDef.SI_DIR_MAX_FILENUM];
			head = new SI_LMTENBSI_MsgHead();
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			//throw new NotImplementedException();
			int used = 0;
			used += head.SerializeToBytes(ref ret, used + offset);

			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			int u = head.DeserializeToStruct(bytes, offset + used);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.SerializeBytes(ref s8SrcPath, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeByte(bytes, offset + used, ref s8GetResult);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16FileNum);
			if (-1 == u) return -1;
			used += u;

			//解出来所有的数据
			for (int i = 0; i < SiMacroDef.SI_DIR_MAX_FILENUM; i++)
			{
				var fileInfo = new SI_STRU_FileInfo();
				u = fileInfo.DeserializeToStruct(bytes, offset + used);
				if (-1 == u) return -1;
				used += u;

				struFileInfo[i] = fileInfo;

				//var fname = Encoding.Default.GetString(fileInfo.s8FileName).Replace("\0", "");
				//var fver = Encoding.Default.GetString(fileInfo.s8FileMicroVer).Replace("\0", "");
				//var ftime = fileInfo.struFileTime.GetStrTime();
				//Debug.WriteLine($"{i}  {fname} {fver} {ftime} {fileInfo.u32FileLength}");
			}

			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => (ushort)(SiMacroDef.SI_FILEPATH_MAX_LEN + SiMacroDef.SI_DIR_MAX_FILENUM * Marshal.SizeOf<SI_STRU_FileInfo>());
	}

	/**************************************************
	消息宏: O_SILMTENB_GETFILEINFO_RES
	结构名: SI_SILMTENB_GetFileInfoRspMsg
	描述:   文件信息查询响应消息
	**************************************************/
	public class SI_SILMTENB_GetFileInfoRspMsg_v2 : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8SrcPath;        /* 查询目录 */
		public byte s8GetResult;        /* 0:成功;1:失败 */
		public byte u8Pad;
		public byte u8Ver;
		public byte u8FileCount;
		public SI_STRU_FileInfo_V2[] struFileInfo;     /* 文件信息结构数组，V2 96个*/

		public SI_SILMTENB_GetFileInfoRspMsg_v2()
		{
			s8SrcPath = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
			struFileInfo = new SI_STRU_FileInfo_V2[96];
			head = new SI_LMTENBSI_MsgHead();
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
			int u = head.DeserializeToStruct(bytes, offset + used);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.SerializeBytes(ref s8SrcPath, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeByte(bytes, offset + used, ref s8GetResult);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Pad);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Ver);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeByte(bytes, offset + used, ref u8FileCount);
			if (-1 == u) return -1;
			used += u;

			//Debug.WriteLine($"index filename fileversion time size");

			//解出来所有的数据
			for (int i = 0; i < 96; i++)
			{
				var fi = new SI_STRU_FileInfo_V2();
				u = fi.DeserializeToStruct(bytes, offset + used);
				if (-1 == u) return -1;
				used += u;

				struFileInfo[i] = fi;

				//var fname = Encoding.Default.GetString(fi.s8FileName).Replace("\0", "");
				//var fver = Encoding.Default.GetString(fi.s8FileMicroVer).Replace("\0", "");
				//var ftime = fi.struFileTime.GetStrTime();
				//Debug.WriteLine($"{i}  {fname} {fver} {ftime} {fi.u32FileLength}");
			}

			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => (ushort) (200 + sizeof(byte) * 4 + 96 * Marshal.SizeOf<SI_STRU_FileInfo_V2>());
	}

	/*文件信息结构*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class SI_STRU_FileInfo : IASerialize
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = SiMacroDef.SI_FILENAME_MAX_LEN)]
		public byte[] s8FileName;						/* 文件名 */

		public uint u32FileLength;						/* 文件字节长度 */
		public SIDOS_DATE_TIME struFileTime;			/* 文件时间 */

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = SiMacroDef.SI_FILEVER_MAX_LEN)]
		public byte[] s8FileMicroVer;          /* 文件小版本 */

		public byte u8RdWrAttribute;			/* 文件读写属性 */

		public SI_STRU_FileInfo()
		{
			s8FileMicroVer = new byte[SiMacroDef.SI_FILEVER_MAX_LEN];
			s8FileName = new byte[SiMacroDef.SI_FILENAME_MAX_LEN];
			struFileTime = new SIDOS_DATE_TIME();
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
			int u = SerializeHelper.SerializeBytes(ref s8FileName, 0, bytes, offset + used, SiMacroDef.SI_FILENAME_MAX_LEN);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32FileLength);
			if (-1 == u) return -1;
			used += u;

			u = struFileTime.DeserializeToStruct(bytes, offset + used);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.SerializeBytes(ref s8FileMicroVer, 0, bytes, offset + used, SiMacroDef.SI_FILEVER_MAX_LEN);
			if (-1 == u) return -1;
			used += u;

			u = SerializeHelper.DeserializeByte(bytes, offset + used, ref u8RdWrAttribute);
			if (-1 == u) return -1;
			used += u;

			used += 3;		// 补齐3个字节

			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => 
			(ushort) (SiMacroDef.SI_FILENAME_MAX_LEN + sizeof(uint) + Marshal.SizeOf<SIDOS_DATE_TIME>() + 
			          SiMacroDef.SI_FILEVER_MAX_LEN + sizeof(byte));
	}

	/*文件信息结构*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class SI_STRU_FileInfo_V2 : IASerialize
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 70)]
		public byte[] s8FileName;    /*40--->70文件名 2014.12.11 */

		public uint u32FileLength;					/* 文件字节长度 */
		public SIDOS_DATE_TIME struFileTime;		/* 文件时间 */

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = SiMacroDef.SI_FILEVER_MAX_LEN)]
		public byte[] s8FileMicroVer;				/* 文件小版本 */

		public byte u8RdWrAttribute;				/* 文件读写属性 */

		public SI_STRU_FileInfo_V2()
		{
			s8FileMicroVer = new byte[SiMacroDef.SI_FILEVER_MAX_LEN];
			s8FileName = new byte[70];
			struFileTime = new SIDOS_DATE_TIME();
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
			used += SerializeHelper.SerializeBytes(ref s8FileName, 0, bytes, offset + used, 70);

			used += 2;		// 2个字节补齐

			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32FileLength);
			used += struFileTime.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.SerializeBytes(ref s8FileMicroVer, 0, bytes, offset + used, SiMacroDef.SI_FILEVER_MAX_LEN);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8RdWrAttribute);

			used += 3;		// 3个字节补齐

			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => (ushort)(70 + sizeof(uint) + 24 + 40 + sizeof(byte) + 5);
	}

	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class SIDOS_DATE_TIME : IASerialize
	{
		public uint dosdt_year;       /* 年 */
		public uint dosdt_month;      /* 月 */
		public uint dosdt_day;        /* 日 */
		public uint dosdt_hour;       /* 时 */
		public uint dosdt_minute;     /* 分 */
		public uint dosdt_second;     /* 秒 */

		public string GetStrTime()
		{
			var year = dosdt_year.ToString("D4");
			var month = dosdt_month.ToString("D2");
			var day = dosdt_day.ToString("D2");
			var hour = dosdt_hour.ToString("D2");
			var min = dosdt_minute.ToString("D2");
			var second = dosdt_second.ToString("D2");
			var time = $"{year}-{month}-{day} {hour}:{min}:{second}";
			return time;
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
			used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref dosdt_year);
			used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref dosdt_month);
			used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref dosdt_day);
			used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref dosdt_hour);
			used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref dosdt_minute);
			used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref dosdt_second);
			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => sizeof(uint) * 6;
	}


	/****************************************************
		消息宏:  O_LMTENBSI_GETFILEATTRIB_REQ
		结构名: SI_LMTENBSI_GetFileAttribReqMsg
		描述:  查询文件属性请求消息
	****************************************************/
	//[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class SI_LMTENBSI_GetFileAttribReqMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;

		//[MarshalAs(UnmanagedType.ByValArray, SizeConst = SiMacroDef.SI_FILEPATH_MAX_LEN)]
		public byte[] s8FilePath; /* 待查询的文件路径 */

		//[MarshalAs(UnmanagedType.ByValArray, SizeConst = SiMacroDef.SI_FILENAME_MAX_LEN)]
		public byte[] s8FileName; /* 待查询的文件名 */

		public SI_LMTENBSI_GetFileAttribReqMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			head.u16MsgLength = (ushort)(SiMacroDef.SI_FILEPATH_MAX_LEN + SiMacroDef.SI_FILENAME_MAX_LEN + head.Len);
			head.u16MsgType = SiMacroDef.O_LMTENBSI_GETFILEATTRIB_REQ;

			s8FilePath = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
			s8FileName = new byte[SiMacroDef.SI_FILENAME_MAX_LEN];
		}

		public void SetPathAndName(string path, string name)
		{
			if (null == path || null == name)
			{
				return;
			}

			int minPath = Math.Min(SiMacroDef.SI_FILEPATH_MAX_LEN, path.Length);
			int minName = Math.Min(SiMacroDef.SI_FILENAME_MAX_LEN, name.Length);

			var pathBytes = Encoding.Default.GetBytes(path);
			var nameBytes = Encoding.Default.GetBytes(name);

			Buffer.BlockCopy(pathBytes, 0, s8FilePath, 0, minPath);
			Buffer.BlockCopy(nameBytes, 0, s8FileName, 0, minName);
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += head.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, s8FilePath, 0, SiMacroDef.SI_FILEPATH_MAX_LEN);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, s8FileName, 0, SiMacroDef.SI_FILENAME_MAX_LEN);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => SiMacroDef.SI_FILEPATH_MAX_LEN + SiMacroDef.SI_FILENAME_MAX_LEN;
	}

	/****************************************************
		消息宏:  O_SILMTENB_GETFILEATTRIB_RES
		结构名: SI_SILMTENB_GetFileAttribRspMsg
		描述:  查询文件属性响应消息
	****************************************************/
	public class SI_SILMTENB_GetFileAttribRspMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8FilePath;				/* 获取文件的路径 */
		public SI_STRU_FileInfo struFileInfo;	/* 该文件的属性 */
		public byte s8GetResult;				/* 0:成功;1:失败 */

		public SI_SILMTENB_GetFileAttribRspMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			s8FilePath = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
			struFileInfo = new SI_STRU_FileInfo();
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
			used += SerializeHelper.SerializeBytes(ref s8FilePath, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			used += struFileInfo.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.DeserializeByte(bytes, used + offset, ref s8GetResult);
			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => (ushort)(SiMacroDef.SI_FILEPATH_MAX_LEN + struFileInfo.Len + sizeof(byte));
	}

	/****************************************************
		消息宏:  O_LMTENBSI_DELFILE_REQ
		结构名: SI_LMTENBSI_DelFileReqMsg
		描述:  文件删除请求消息
	*************************************************/
	public class SI_LMTENBSI_DelFileReqMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte u8DelOpType;		/* 0:普通删除;1:强制删除 */
		public byte[] s8FileName;		/* 待删除文件名 */

		public SI_LMTENBSI_DelFileReqMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			head.u16MsgType = SiMacroDef.O_LMTENBSI_DELFILE_REQ;
			head.u16MsgLength = (ushort)(head.Len + sizeof(byte) + SiMacroDef.SI_FILEPATH_MAX_LEN);
			s8FileName = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += head.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8DelOpType);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, s8FileName, 0, SiMacroDef.SI_FILEPATH_MAX_LEN);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public void SetName(string name)
		{
			if (null == name)
			{
				return;
			}

			int minPath = Math.Min(SiMacroDef.SI_FILEPATH_MAX_LEN, name.Length);

			var pathBytes = Encoding.Default.GetBytes(name);

			Buffer.BlockCopy(pathBytes, 0, s8FileName, 0, minPath);
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => SiMacroDef.SI_FILEPATH_MAX_LEN + sizeof(byte);
	}

	/*************************************************
		消息宏:  O_SILMTENB_DELFILE_RES
		结构名: STRU_SILMTENB_DELFILE_RES
		描述:  文件删除响应消息
	*************************************************/
	public class SI_SILMTENB_DelFileRspMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte u8DelOpType;			/* 0:普通删除；1:强制删除 */
		public byte[] s8FileName;			/* 删除文件名 */
		public byte s8DelResult;			/* 0:成功;1;失败;2:参数错误;3:只读文件禁止删除;4:目前阶段禁止删除 */

		public SI_SILMTENB_DelFileRspMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			s8FileName = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
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
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DelOpType);
			used += SerializeHelper.SerializeBytes(ref s8FileName, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref s8DelResult);
			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * 2 + SiMacroDef.SI_FILEPATH_MAX_LEN;
	}

	/**********************************************
		消息宏:  O_LMTENBSI_GETCAPACITY_REQ
		结构名: SI_LMTENBSI_GetCapacityReqMsg
		描述:  查询设备容量请求消息
	***********************************************/
	public class SI_LMTENBSI_GetCapacityReqMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8DevName; /* 设备名 */

		public SI_LMTENBSI_GetCapacityReqMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			head.u16MsgLength = (ushort)(head.Len + SiMacroDef.SI_FILEPATH_MAX_LEN);
			head.u16MsgType = SiMacroDef.O_LMTENBSI_GETCAPACITY_REQ;
			s8DevName = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
		}

		public SI_LMTENBSI_GetCapacityReqMsg(string path)
		{
			head = new SI_LMTENBSI_MsgHead();
			head.u16MsgLength = (ushort)(head.Len + SiMacroDef.SI_FILEPATH_MAX_LEN);
			head.u16MsgType = SiMacroDef.O_LMTENBSI_GETCAPACITY_REQ;
			s8DevName = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];

			int minPath = Math.Min(SiMacroDef.SI_FILEPATH_MAX_LEN, path.Length);
			var pathBytes = Encoding.Default.GetBytes(path);
			Buffer.BlockCopy(pathBytes, 0, s8DevName, 0, minPath);
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += head.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, s8DevName, 0, SiMacroDef.SI_FILEPATH_MAX_LEN);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => head.Len + ContentLen;
		public ushort ContentLen => SiMacroDef.SI_FILEPATH_MAX_LEN;
	}

	/****************************************************
		消息宏:  O_SILMTENB_GETCAPACITY_RES
		结构名: SI_SILMTENB_GetCapacityRspMsg
		描述:  查询设备容量响应消息
	****************************************************/
	public class SI_SILMTENB_GetCapacityRspMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8DevName; /* 设备名 */
		public uint u32TotalCapability;		/* 该设备总容量（字节数） */
		public uint u32AvailCapability;		/* 该设备空闲容量（字节数） */
		public byte s8GetResult;			/* 0:成功;1:失败 */

		public SI_SILMTENB_GetCapacityRspMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			s8DevName = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
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
			used += SerializeHelper.SerializeBytes(ref s8DevName, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32TotalCapability);
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32AvailCapability);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref s8GetResult);
			return used;
		}

		public int Len => head.Len + ContentLen;
		public ushort ContentLen => SiMacroDef.SI_FILEPATH_MAX_LEN + sizeof(byte) + sizeof(uint) * 2;
	}

	/*****************************************************
		消息宏:  O_LMTENBSI_SETRDWRATTRIB_REQ
		结构名: SI_LMTENBSI_SetRdWrAttribReqMsg
		描述:  设置文件读写属性请求消息
	******************************************************/
	public class SI_LMTENBSI_SetRdWrAttribReqMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8FileName;    /* 待设置的文件名 */
		public byte u8RdWrAttribute;					/* 1:只读;0:可读可写 */

		public SI_LMTENBSI_SetRdWrAttribReqMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			head.u16MsgLength = (ushort) (head.Len + SiMacroDef.SI_FILEPATH_MAX_LEN + sizeof(byte));
			head.u16MsgType = SiMacroDef.O_LMTENBSI_SETRDWRATTRIB_REQ;

			s8FileName = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += head.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, s8FileName, 0, SiMacroDef.SI_FILEPATH_MAX_LEN);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8RdWrAttribute);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += head.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.SerializeBytes(ref s8FileName, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8RdWrAttribute);
			return used;
		}

		public int Len => head.Len + ContentLen;
		public ushort ContentLen => SiMacroDef.SI_FILEPATH_MAX_LEN + sizeof(byte);
	}

	/*****************************************************
		消息宏:  O_SILMTENB_SETRDWRATTRIB_RES
		结构名: SI_SILMTENB_SetRdWrAttribRspMsg
		描述:  设置文件读写属性响应消息
	*****************************************************/
	public class SI_SILMTENB_SetRdWrAttribRspMsg : IASerialize
	{
		public SI_LMTENBSI_MsgHead head;
		public byte[] s8FileName;		/* 所设置的文件名 */
		public byte u8RdWrAttribute;	/* 1:只读;0:可读可写 */
		public byte s8SetResult;		/* 0:成功;1:失败;2:参数错误 */

		public SI_SILMTENB_SetRdWrAttribRspMsg()
		{
			head = new SI_LMTENBSI_MsgHead();
			s8FileName = new byte[SiMacroDef.SI_FILEPATH_MAX_LEN];
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
			used += SerializeHelper.SerializeBytes(ref s8FileName, 0, bytes, offset + used, SiMacroDef.SI_FILEPATH_MAX_LEN);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8RdWrAttribute);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref s8SetResult);
			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => SiMacroDef.SI_FILEPATH_MAX_LEN + sizeof(byte) * 2;
	}
}
