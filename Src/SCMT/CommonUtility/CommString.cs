/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ CommString $
* 机器名称：       $ machinename $
* 命名空间：       $ DataBaseUtil $
* 文 件 名：       $ CommString.cs $
* 创建时间：       $ 2018.10.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     公共字符串变量定义。
* 修改时间     修 改 人         修改内容：
* 2018.xx.xx  XXXX            XXXXX
*************************************************************************************/
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
		public const string IDS_SETPDU_ERROR = "SET命令响应错误";
		public const string IDS_GETPDU_ERROR = "GET命令响应错误";
		public const string IDS_PDU_ERROR = "命令响应错误";

		public const string IDS_STR_MSGDISPOSE_FMT1 = "命令{0}响应等待超时";

		public const string IDS_INSTANCE = "实例";

		public const string IDS_OPERLOG_SUCCESS = "成功";

		public const string IDS_NEID = "网元标识:";
		public const string IDS_NETYPE = "网元类型:";
		public const string IDS_SYNCFILETYPE = "需要同步的文件类型:";
		public const string IDS_ADDITIONALINFO = "附加信息:";
		public const string IDS_OCCURTIME = "产生时间:";
		public const string IDS_RECEIVE = "收到";
		public const string IDS_INFOMSGSTYLE = "被管对象: {0},值为: {1}";
		public const string IDS_UNITNAME = "单位/精度: ";
		public const string IDS_ANR_EVENT_TYPE = "ANR事件类型: ";
		public const string IDS_ANR_NOTILCIDX = "本小区索引: ";
		public const string IDS_ANR_NOTI_ADJ_RELATION_IDX = "邻区关系索引: ";
		public const string IDS_ANR_NOTI_ADJ_CELL_NET_TYPE = "邻区网络类型: ";
		public const string IDS_ANR_NOTI_ADJ_CELL_PLMN_MCC = "邻区移动国家码: ";
		public const string IDS_ANR_NOTI_ADJ_CELL_PLMN_MNC = "邻区移动网络码: ";
		public const string IDS_ANR_NOTI_ADJ_CELL_ID = "邻区索引: ";
		public const string DIS_EVNET_RESULT = "事件结果: ";
		public const string DIS_EVENT_FAIL_RSULT = "事件失败原因: ";
		public const string DIS_EVENT_TIME = "事件产生时间: ";
		public const string DIS_MRO_EVENT_TYPE = "MRO事件类型: ";
		public const string DIS_MRO_NOTI_CELL_ID = "小区索引: ";
		public const string DIS_FC_EVENT_TYPE = "FC事件类型: ";

	}
}
