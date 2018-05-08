using System.Runtime.InteropServices;
using CommonUility;

namespace AtpMessage.MsgDefine
{
	/// <summary>
	/// gts消息头，实现了序列化和反序列化
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class GtsMsgHeader : IASerialize
	{
		public ushort u16SourceID;                            /*消息源信息域*/
		public ushort u16DestID;                              /*消息目的地信息域*/
		public ushort u16Opcode;                              /*消息类型*/
		public ushort u16Length;                              /*消息长度域*/
		public byte u8RemoteFlag;                             /*远程登录标志*/
		public byte u8RemoteMode;                             /*远程模式*/
		public ushort u16SubSfn;                              /*SUB_SFN*/

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			if (ret.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16SourceID);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16DestID);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16Opcode);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16Length);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8RemoteFlag);
			used += SerializeHelper.SerializeByte(ref ret, offset + used, u8RemoteMode);
			used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16SubSfn);

			return used;
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16SourceID);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16DestID);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16Opcode);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16Length);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8RemoteFlag);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8RemoteMode);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16SubSfn);
			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => sizeof(ushort) * 5 + sizeof(byte) * 2;
	}
}
