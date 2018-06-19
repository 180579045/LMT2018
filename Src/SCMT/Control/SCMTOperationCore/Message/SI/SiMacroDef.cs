using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
