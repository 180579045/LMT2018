using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 文件传输助手

namespace SCMTOperationCore.Message.MsgDispatcher
{
	public static class FileTransHelper
	{
		public static bool UploadAlarmLogFile(string targetIp)
		{
			//var pAutoSynAlarmDlg = new CDTSynAlarmDlg(targetIp, ALARMTYPE.ACTIVE_ALARM_ENB5216);
			//pAutoSynAlarmDlg.Start(INTERFACETYPE.INTERFACE_HIDE);
			return true;
		}

	}

	public enum ALARMTYPE : byte
	{
		HISTORY_ALARM = 5,
		ACTIVE_ALARM = 13,
		HISTORY_ALARM_ENB5216 = 4,
		ACTIVE_ALARM_ENB5216 = 27,
		EVENT_ALARM_ENB5216 = 8
	}

	public enum INTERFACETYPE
	{
		INTERFACE_UNKNOWN = -1,
		INTERFACE_MODEL,		//模态对话框执行
		INTERFACE_HIDE			//隐藏窗口执行
	}
}
