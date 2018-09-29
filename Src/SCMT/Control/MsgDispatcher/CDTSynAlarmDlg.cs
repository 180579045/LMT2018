using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUtility;
using Timer = System.Threading.Timer;

namespace MsgDispatcher
{
	public partial class CDTSynAlarmDlg : Form
	{
		public CDTSynAlarmDlg(string targetIp, ALARMTYPE eAlarmType, bool bIsHistory = false)
		{
			InitializeComponent();

			TransProgress.Minimum = 0;
			TransProgress.Maximum = 100;

			TargetIp = targetIp;
			AlarmType = eAlarmType;
			UlHistoryFlag = bIsHistory;
		}

		public bool Start(INTERFACETYPE eInterfaceType = INTERFACETYPE.INTERFACE_MODEL)
		{
			StartTimer();

			ShowType = eInterfaceType;

			switch (ShowType)
			{
				case INTERFACETYPE.INTERFACE_MODEL:
					DoModal();
					break;
				case INTERFACETYPE.INTERFACE_HIDE:
					return WorkInHideModal();
				default:
					return false;
			}

			return true;
		}

		// 以模态类型显示
		private void DoModal()
		{
			ShowDialog();
		}

		// 以隐藏状态显示
		private bool WorkInHideModal()
		{
			this.WindowState = FormWindowState.Minimized;
			this.ShowInTaskbar = false;
			SetVisibleCore(false);
			return true;
		}

		private void EndTransBtn_Click(object sender, EventArgs e)
		{

		}

		#region 定时器及处理

		private void StartTimer()
		{
			TimeExpireTimer = new Timer(TimerExpireHandler, null, 300000, -1);		// 整个日志上传任务只有5分钟的时间
			InitTimer = new Timer(StartUploadTask, this, 2000, -1);					// 调用后2秒启动日志上传

			this.Text = "发起上传告警日志文件任务...";
		}

		private void TimerExpireHandler(object obj)
		{
			if (ShowType == INTERFACETYPE.INTERFACE_HIDE)
			{
				TimeExpireTimer.Change(-1, -1);
				TimeExpireTimer.Dispose();

				this.Dispose();					// 释放资源
			}
		}

		private void StartUploadTask(object obj)
		{
			//InitTimer.Change(-1, -1);		// 启动任务后就停止
			//InitTimer.Dispose();

			//long reqId = 0;
			//long taskId = 0;
			//var strAlarmFilePath = FilePathHelper.GetAlarmFilePath(TargetIp);
			//var cft = FileTransTaskMgr.FormatTransInfo(strAlarmFilePath, "", (Transfiletype5216) AlarmType, TRANSDIRECTION.TRANS_UPLOAD);
			//var ret = FileTransTaskMgr.SendTransFileTask(TargetIp, cft, ref taskId, ref reqId);

			//if (ALARMTYPE.HISTORY_ALARM_ENB5216 == AlarmType && UlHistoryFlag)
			//{
			//	cft = FileTransTaskMgr.FormatTransInfo(strAlarmFilePath, "", AlarmType,
			//		(Transfiletype5216)ALARMTYPE.EVENT_ALARM_ENB5216, TRANSDIRECTION.TRANS_UPLOAD);
			//	ret = FileTransTaskMgr.SendTransFileTask(TargetIp, cft, ref taskId, ref reqId);
			//}

			//if (SENDFILETASKRES.TRANSFILE_TASK_SUCCEED == ret)
			//{
			//	this.Text = "正在上传告警日志文件...";
			//	TaskType = SYNALARMTASK.TASK_UPLOAD;
			//}
			//else
			//{
			//	this.Text = "发起上传告警日志文件任务失败...";
			//}
		}

		#endregion


		#region 私有成员

		private string TargetIp { get; }

		private ALARMTYPE AlarmType { get; }

		private INTERFACETYPE ShowType { get; set; }

		private bool UlHistoryFlag { get; }

		private SYNALARMTASK TaskType { get; set; }

		// 定时器类型定义
		private Timer TimeExpireTimer;			// 超时定时器

		private Timer InitTimer;				// 启动初始化任务



		#endregion
	}

	internal enum SYNALARMTASK
	{
		TASK_UNKNOWN = -1,
		TASK_UPLOAD,
		TASK_UNZIP
	}
}
