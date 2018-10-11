using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
	/// <summary>
	/// 信息字符串定义
	/// </summary>
	public class CommString
	{
		public const string IDS_ERROR_TOO_BIG = "报文太大";
		public const string IDS_ERROR_NO_SUCH_NAME = "不存在";
		public const string IDS_ERROR_BAD_VALUE = "错误的值";
		public const string IDS_ERROR_READ_ONLY = "只读";
		public const string IDS_ERROR_GENERAL_VB_ERR = "产生其它错误";
		public const string IDS_ERROR_NO_ACCESS = "不可访问";
		public const string IDS_ERROR_WRONG_TYPE = "设置类型与要求类型不一致";
		public const string IDS_ERROR_WRONG_LENGTH = "设置长度与要求长度不一致";
		public const string IDS_ERROR_WRONG_ENCODING = "ASN.1标签编码错误";
		public const string IDS_ERROR_WRONG_VALUE = "不可赋为设置值";
		public const string IDS_ERROR_NO_CREATION = "不存在";
		public const string IDS_ERROR_INCONSIST_VAL = "不适合当前环境";
		public const string IDS_ERROR_RESOURCE_UNAVAIL = "赋值所需资源当前不可得到";
		public const string IDS_ERROR_COMITFAIL = "提交失败";
		public const string IDS_ERROR_UNDO_FAIL = "撤销失败";
		public const string IDS_ERROR_AUTH_ERR = "授权错误";
		public const string IDS_ERROR_NOT_WRITEABLE = "不可修改";
		public const string IDS_ERROR_INCONSIS_NAME = "不存在,且在当前环境下不能生成";
		public const string IDS_ERROR_LOGIN = "LMT-eNB首次登录为错误报文";
		public const string IDS_ERROR_ACTIONSHIELD = "该操作被屏蔽";
		public const string IDS_ERROR_ACTIONFAILD = "操作失败";
		public const string IDS_ERROR_OMBUSY = "由于OM忙不能操作实现";
		public const string IDS_ERROR_OBSOLETE = "该操作目前已经废弃";
		public const string IDS_ERROR_OVERTIME = "OM出现超时错误";

		public const string IDS_SETPDU_ERROR = "SET命令响应错误";
		public const string IDS_GETPDU_ERROR = "GET命令响应错误";
		public const string IDS_PDU_ERROR = "命令响应错误";

		public const string IDS_STR_MSGDISPOSE_FMT1 = "命令{0}响应等待超时";

		public const string IDS_INSTANCE = "实例";

		public const string IDS_OPERLOG_SUCCESS = "成功";

		// 消息订阅
		public const string MSG_KEY_CDTSnmpMsgDispose_OnResponse = "CDTSnmpMsgDispose_OnResponse";


	}
}
