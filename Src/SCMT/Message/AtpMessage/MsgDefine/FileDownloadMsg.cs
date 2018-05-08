using System;
using System.Runtime.InteropServices;
using CommonUility;

namespace AtpMessage.MsgDefine
{
	/*文件下载请求*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class StruFdlData : IASerialize
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = GtsMsgType.MAX_FILE_PATH_NAME_LENGTH)]
		public byte[] u8FileName;					/*文件名*/
		public ushort u16DestID;					/*目标*/
		public uint u32FileSize;					/*文件大小*/

		public StruFdlData()
		{
			u8FileName = new byte[GtsMsgType.MAX_FILE_PATH_NAME_LENGTH];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.SerializeBytes(ref ret, used + offset, u8FileName, 0, GtsMsgType.MAX_FILE_PATH_NAME_LENGTH);
			used += SerializeHelper.SerializeUshort(ref ret, used + offset, u16DestID);
			used += SerializeHelper.SerializeInt32(ref ret, used + offset, u32FileSize);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => ContentLen;

		public ushort ContentLen => sizeof(byte) * GtsMsgType.MAX_FILE_PATH_NAME_LENGTH + sizeof(ushort) + sizeof(uint);
	};

	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaFdlReq : IASerialize
	{
		public GtsMsgHeader header;						/*GTS消息头*/
		public byte u8OpType;								/*文件操作类型*/
		public byte u8FileType;								/*文件类型*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] u8Padding;
		public StruFdlData struFileData;					/*数据部分*/

		public MsgGtsm2GtsaFdlReq()
		{
			header = new GtsMsgHeader();
			u8Padding = new byte[2];
			struFileData = new StruFdlData();
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8OpType);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8FileType);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, u8Padding, 0, 2);
			used += struFileData.SerializeToBytes(ref ret, offset + used);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => (ushort)(sizeof(byte) * 4 + struFileData.Len);
	};

	/*文件下载响应*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsm2GtsaFdlRsp : IASerialize
	{
		public GtsMsgHeader header;						/*GTS消息头*/
		public byte u8Complete;								/*GTS_SUCCESS表示成功，GTS_FAILURE表示失败*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] u8Padding;

		public MsgGtsm2GtsaFdlRsp()
		{
			header = new GtsMsgHeader();
			u8Padding = new byte[3];
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
			used += SerializeHelper.SerializeBytes(ref u8Padding, 0, bytes, offset + used, 3);
			return used;
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * 4;
	};

	/*文件下载完成消息*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class StruFdlEndInfo : IASerialize
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = GtsMsgType.MAX_FILE_PATH_NAME_LENGTH)]
		public byte[] u8FileName;							/*文件名*/
		public ushort u16DestID;							/*目标*/
		public byte u8Completed;							/*完成标志*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] u8Padding;

		public StruFdlEndInfo()
		{
			u8FileName = new byte[GtsMsgType.MAX_FILE_PATH_NAME_LENGTH];
			u8Padding = new byte[3];
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
			used += SerializeHelper.SerializeBytes(ref u8FileName, 0, bytes, offset + used, GtsMsgType.MAX_FILE_PATH_NAME_LENGTH);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16DestID);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Completed);
			used += SerializeHelper.SerializeBytes(ref u8Padding, 0, bytes, offset + used, 3);
			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => sizeof(byte) * (GtsMsgType.MAX_FILE_PATH_NAME_LENGTH + 4) + sizeof(ushort);
	};

	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal class MsgGtsa2GtsmFdlCompleteInd : IASerialize
	{
		public GtsMsgHeader header;							/*GTS消息头*/
		public byte u8OpType;								/*文件操作类型*/
		public byte u8FileType;								/*文件类型*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] u8Padding;
		public StruFdlEndInfo struFileData;					/*数据部分*/

		public MsgGtsa2GtsmFdlCompleteInd()
		{
			header = new GtsMsgHeader();
			u8Padding = new byte[2];
			struFileData = new StruFdlEndInfo();
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
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8OpType);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8FileType);
			used += 2;
			used += struFileData.DeserializeToStruct(bytes, offset + used);
			return used;
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => (ushort) (sizeof(byte) * 4 + struFileData.Len);
	};
}
