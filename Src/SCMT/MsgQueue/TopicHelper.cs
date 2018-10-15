using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 保存所有的topic，类似MFC中的消息定义

namespace MsgQueue
{
	public class TopicHelper
	{
		#region B方案中使用的topic


		#endregion

		#region 通知UI界面相关消息
		public const string SHOW_LOG = "show_log";


		#endregion

		#region 后台进程间消息

		public const string EnbConnectedMsg = "handle_enb_connected";
		public const string SnmpAsyncMsg = "handl_snmp_async_msg";
		public const string EnbOfflineMsg = "handle_enb_offline";

		#endregion

		#region SI消息对应的topic
		public const string EnbPhaseMsg = "handle_enbphase_msg";
		public const string QuerySiPortVerRsp = "handle_si_port_ver_rsp";
		public const string QueryEnbCapacityRsp = "handle_enb_capacity_rsp";

		#endregion

		#region SNMP相关消息
		// 解析数据一致性文件
		public const string ParseDataConFile = "parse_data_con_file";
		// 通知加载lm.dtz数据到MIBVersionDB中的消息
		public const string LoadLmdtzToVersionDb = "Load_Lmdtz_to_versiondb";
		// snmp响应消息
		public const string SnmpMsgDispose_OnResponse = "SnmpMsgDispose_OnResponse";
		// Trap消息
		public const string SnmpMsgDispose_OnTrap = "SnmpMsgDispose_OnTrap";
		// 配置变更Trap消息
		public const string SnmpMsgDispose_CfgChgTrap = "SnmpMsgDispose_CfgChgTrap";
		// Trap消息分发
		public const string SnmpMsgDispose_AppEventTrap = "SnmpMsgDispose_AppEventTrap";


		#endregion

	}
}
