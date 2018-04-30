using System;
using System.Runtime.InteropServices;
using CommonUility;

namespace AtpMessage.MsgDefine
{
	/*文件上传请求*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal struct MsgGtsm2GtsaFulReq : IASerialize
	{
		public GtsMsgHeader header;				/*GTS消息头*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = GtsMsgType.MAX_FILE_PATH_NAME_LENGTH)]
		public byte[] u8FileName;				/*文件名*/

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeBytes(ref ret, offset + used, u8FileName, 0, GtsMsgType.MAX_FILE_PATH_NAME_LENGTH);
			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			throw new NotImplementedException();
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * GtsMsgType.MAX_FILE_PATH_NAME_LENGTH;
	};

	/*文件上传响应*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal struct MsgGtsm2GtsaFulRsp : IASerialize
	{
		public GtsMsgHeader header;                         /*GTS消息头*/
		public byte u8Complete;                             /*GTS_SUCCESS表示成功， GTS_FAILURE表示失败*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] u8Padding;

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += header.SerializeToBytes(ref ret, offset + used);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8Complete);
			used += 3;
			return used;
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
			used += 3;
			return used;
		}

		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * 4;
	};

	/*文件上传完成消息*/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	internal struct MsgGtsa2GtsmFulCompleteInd : IASerialize
	{
		public GtsMsgHeader header;			/*GTS消息头*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = GtsMsgType.MAX_FILE_PATH_NAME_LENGTH)]
		public byte[] u8FileName;			/*文件名*/
		public byte u8Completed;			/*完成标志*/
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
			used += SerializeHelper.SerializeBytes(ref u8FileName, 0, bytes, offset + used, GtsMsgType.MAX_FILE_PATH_NAME_LENGTH);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Completed);
			return used;
		}


		public int Len => header.Len + ContentLen;

		public ushort ContentLen => sizeof(byte) * GtsMsgType.MAX_FILE_PATH_NAME_LENGTH + sizeof(byte);
	};
}
