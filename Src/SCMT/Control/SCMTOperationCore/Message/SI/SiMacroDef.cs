using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace SCMTOperationCore.Message.SI
{
	//SI消息中宏定义
	public static class SiMacroDef
	{
		public const int SI_FILEPATH_MAX_LEN = 200;		/* 文件路径最大长度 */
		public const int SI_DIR_MAX_FILENUM = 70;       /* 一个目录最大文件数 */
		public const int SI_FILENAME_MAX_LEN = 40;      /* 文件名最大长度 */
		public const int SI_FILEVER_MAX_LEN = 40;       /* 文件版本最大长度*/

		public const ushort O_LMTENBSI_GETFILEINFO_REQ = 0x40; //文件信息查询请求消息
		public const ushort O_SILMTENB_GETFILEINFO_RES = 0x41; //文件信息查询响应消息

		public const ushort O_LMTENBSI_GETFILEATTRIB_REQ = 0x44;    //查询文件属性请求消息
		public const ushort O_SILMTENB_GETFILEATTRIB_RES = 0x45;    //查询文件属性响应消息

		public const ushort O_LMTENBSI_SETRDWRATTRIB_REQ = 0x46;    //设置文件读写属性请求消息
		public const ushort O_SILMTENB_SETRDWRATTRIB_RES = 0x47;    //设置文件读写属性响应消息

		public const ushort O_LMTENBSI_DELFILE_REQ = 0x48;      //文件删除请求消息
		public const ushort O_SILMTENB_DELFILE_RES = 0x49;      //文件删除响应消息

		public const ushort O_LMTENBSI_GETCAPACITY_REQ = 0x4C;            //查询设备容量请求消息
		public const ushort O_SILMTENB_GETCAPACITY_RES = 0x4D;            //查询设备容量响应消息

		public const ushort O_LMTOM_GET_SIPORTVERSION_REQ = 0xF0;	/*基站版本请求*/
		public const ushort O_OMLMT_GET_SIPORTVERSION_RSP = 0xF1;	/*基站版本响应*/

		public const ushort O_SILMTENB_NBPHASE_REP = 0x80;		/*NODE B所处阶段上报消息*/
	}

	//SI消息头
	public class SiMsgHead : IASerialize
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

	//助手函数。TODO 可以和其他的几个合并在一起，做一个模板
	public static class SiMsgHelper
	{
		public static SiMsgHead GetSiMsgHead(byte[] data, int offset = 0)
		{
			SiMsgHead head = new SiMsgHead();
			if (-1 == head.DeserializeToStruct(data, offset))
				return null;

			return head;
		}
	}
}
