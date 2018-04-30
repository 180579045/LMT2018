using System.ComponentModel;

namespace AtpMessage
{
	public class GtsMsgType
	{
		public const ushort GTS_OPCODE_GTSM_GTSA_BASE = 800;							/*GTSM-GTSA消息类型基值*/
		public const ushort O_GTSAGTSM_TRACE_MSG = (GTS_OPCODE_GTSM_GTSA_BASE + 0);		/*跟踪消息*/
		public const ushort O_GTSMGTSA_LOGON_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 1);		/*GTSM登录消息*/
		public const ushort O_GTSAGTSM_LOGON_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 2);		/*GTSM登录响应消息*/
		public const ushort O_GTSMGTSA_QUIT_IND = (GTS_OPCODE_GTSM_GTSA_BASE + 5);		/*GTSM退出请求消息*/
		public const ushort O_GTSMGTSA_FILTER_RESET_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 7);		/*过滤准则复位消息*/
		public const ushort O_GTSAGTSM_FILTER_RESET_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 8);		/*过滤准则复位响应消息*/
		public const ushort O_GTSMGTSA_FILTER_RULE_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 9);		/*过滤准则设置消息*/
		public const ushort O_GTSAGTSM_FILTER_RULE_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 10);		/*过滤准则设置响应消息*/
		public const ushort O_GTSMGTSA_TRACE_CTRL_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 11);   /*跟踪控制消息*/
		public const ushort O_GTSAGTSM_TRACE_CTRL_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 12);   /*跟踪控制响应消息*/
		public const ushort O_GTSMGTSA_FDL_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 13);   /*文件下载请求消息*/
		public const ushort O_GTSAGTSM_FDL_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 14);   /*文件下载响应消息*/
		public const ushort O_GTSAGTSM_FDL_COMPLETE_IND = (GTS_OPCODE_GTSM_GTSA_BASE + 15);   /*文件下载完成指示消息*/
		public const ushort O_GTSMGTSA_FUL_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 37);   /*文件上传请求消息*/
		public const ushort O_GTSAGTSM_FUL_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 38);   /*文件上传响应消息*/
		public const ushort O_GTSAGTSM_FUL_COMPLETE_IND = (GTS_OPCODE_GTSM_GTSA_BASE + 40);   /*文件上传完成消息*/
		public const ushort O_GTSMGTSA_RESPONSE_STOP_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 19);   /*响应终止指示消息*/
		public const ushort O_GTSAGTSM_RESPONSE_STOP_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 20);   /*响应终止指示响应消息*/
		public const ushort O_GTSMGTSA_SIMULATE_STOP_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 21);   /*模拟终止指示消息*/
		public const ushort O_GTSAGTSM_SIMULATE_STOP_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 22);   /*模拟终止指示响应消息*/
		public const ushort O_GTSMGTSA_DEBUG_CTRL_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 42);   /*调试信息控制消息*/
		public const ushort O_GTSAGTSM_DEBUG_CTRL_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 43);   /*调试信息控制响应消息*/
		public const ushort O_GTSAGTSM_DEBUG_INFO_IND = (GTS_OPCODE_GTSM_GTSA_BASE + 45);   /*调试信息*/
		public const ushort O_GTSAGTSM_REDIRECTION_MSG = (GTS_OPCODE_GTSM_GTSA_BASE + 16);   /*重定向消息*/
		public const ushort O_GTSAGTSM_ERRINFO_IND = (GTS_OPCODE_GTSM_GTSA_BASE + 18);   /*GTSA操作错误消息*/
		public const ushort O_GTSMGTSA_PPC_MAX_TRACENUM_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 24);   /*主处理器跟踪流量设置请求，包括SCP/BCP*/
		public const ushort O_GTSAGTSM_PPC_MAX_TRACENUM_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 25);   /*主处理器跟踪流量设置响应，包括SCP/BCP*/
		public const ushort O_GTSMGTSA_DSP_MAX_TRACENUM_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 26);   /*DSP处理器跟踪流量控制请求*/
		public const ushort O_GTSAGTSM_DSP_MAX_TRACENUM_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 27);   /*DSP处理器跟踪流量控制响应*/
		public const ushort O_GTSMGTSA_STOPTRACE_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 53); /*ATP发给GTSA的停止跟踪请求消息*/
		public const ushort O_GTSAGTSM_SIMULATE_COMPLETE_IND = (GTS_OPCODE_GTSM_GTSA_BASE + 24);   /*模拟完成指示消息*/
		public const ushort O_GTSMGTSA_QUERY_MSG_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 25);   /*查询消息请求消息*/
		public const ushort O_GTSAGTSM_QUERY_MSG_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 26);   /*查询消息结果消息*/
		public const ushort O_GTSMGTSA_CONFIG_MSG_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 27);   /*配置消息请求*/
		public const ushort O_GTSAGTSM_CONFIG_MSG_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 28);   /*配置消息响应*/
		public const ushort O_GTSMGTSA_DIAG_MSG = (GTS_OPCODE_GTSM_GTSA_BASE + 29);   /*DIAG配置消息*/
		public const ushort O_GTSAGTSM_ALARM_IND = (GTS_OPCODE_GTSM_GTSA_BASE + 32);   /*告警指示消息*/
		public const ushort O_GTSMGTSA_QUERY_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 33);   /*查询请求*/
		public const ushort O_GTSAGTSM_QUERY_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 34);   /*查询响应*/
		public const ushort O_GTSMGTSA_CONFIG_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 35);   /*配置请求*/
		public const ushort o_GTSMGTSA_CONFIG_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 36); /*配置响应*/
		public const ushort O_GTSMGTSA_ADDFLOW_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 50); /*ATP发给GTSA的建链请求*/
		public const ushort O_GTSAGTSM_ADDFLOW_RSP = (GTS_OPCODE_GTSM_GTSA_BASE + 51); /*GTSA发给ATP的建链操作响应*/
		public const ushort O_GTSMGTSA_ALIVE_RPT = (GTS_OPCODE_GTSM_GTSA_BASE + 52); /*ATP发给GTSA的存活消息请求*/
		public const ushort O_GTSMGTSA_DSPTRACE_CTRL_REQ = (GTS_OPCODE_GTSM_GTSA_BASE + 53); /*ATP发给GTSA的DSP抄送控制开关请求*/

		public const ushort DEST_GTSM = 0;			/*用户GTS消息头中表示消息的发送方接收方是GTSM*/

		/*登陆方式*/
		public const byte CONNECT_INVALID = 0;					/*无效*/
		public const byte CONNECT_DIRECT_MSG = 1;				/*直连消息模式*/
		public const byte CONNECT_REMOTE_MSG = 2;				/*远程消息模式*/
		public const byte CONNECT_REMOTE_LOG = 3;				/*远程日志模式*/

		public const ushort MAX_TRACE_NUM = 256;				/*跟踪开关个数*/
		public const byte MAX_FILE_PATH_NAME_LENGTH = 70;		/*文件传输中文件名长度*/
		public const byte IP_ADDRESS_LEN_V6 = 16;				/*IP_v6地址最大字节数*/
		public const ushort MAX_DIAG_MSG_LEN = 1024;			/*DIAG消息长度*/


		public const uint OM_GTS_IPV4 = 1;
		public const uint OM_GTS_IPV6 = 2;
	}
}
