using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AtpMessage;
using AtpMessage.LinkMgr;
using CommonUtility;
using LogManager;
using MsgQueue;
using MsgDispatcher;
using System.Windows.Threading;
using System.Text;

namespace SCMTMainWindow
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			// 设置日志名
			var timeString = TimeHelper.DateTimeToString(DateTime.Now, "yyyyMMdd-HHmmss");
			var logFilePath = $"SCMT_log_{timeString}.log";
			Log.SetLogFileName(logFilePath);

			MqInitial.Init();
			ConnectWorker.GetInstance();
			DoMsgDispatcher.GetInstance();
            //AtpInitial.Init();

            //NetElementConfig config = new NetElementConfig()
            //{
            //	TraceIp = "172.27.245.82",
            //	AgentSlot = 2,
            //	Index = 3,
            //	FrameNo = 0,
            //	SlotNo = 1,
            //	TargetIp = "172.27.245.92",
            //	conType = ConnectType.ATP_DIRECT_LINK
            //};

            //LinkMgrActor.GetInstance().ConnectNetElement("172.27.245.92", config);
            //异常捕获记录 
		    //UI线程
		    Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
            //捕获应用程序域中发生的异常（包括非UI线程）
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            //Async await 异常
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }
        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
	    {
	        string errInfo = unobservedTaskExceptionEventArgs.Exception.ToString();
            Log.Error(errInfo);
	        MessageBox.Show("当前应用程序遇到一些问题,请提取相应日志文件联系研发" + Environment.NewLine + errInfo);
            //Environment.Exit(0);
        }

	    private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
	    {
	        string errInfo = dispatcherUnhandledExceptionEventArgs.Exception.ToString();
            Log.Error(errInfo);
	        MessageBox.Show("当前应用程序遇到一些问题,请提取相应日志文件联系研发" + Environment.NewLine + errInfo);
	        dispatcherUnhandledExceptionEventArgs.Handled = true;//告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。 
            //Environment.Exit(0);
        }

	    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
	    {
	        bool existFlag = false;
	        StringBuilder sbEx = new StringBuilder();
	        if (unhandledExceptionEventArgs.IsTerminating)
	        {
	            sbEx.Append("程序发生致命错误，将终止，请提取相应日志文件联系研发！\n");
                existFlag = true;
            }
	        sbEx.Append("捕获未处理异常：");
	        if (unhandledExceptionEventArgs.ExceptionObject is Exception)
	        {
	            sbEx.Append(((Exception)unhandledExceptionEventArgs.ExceptionObject).Message);
	        }
	        else
	        {
	            sbEx.Append(unhandledExceptionEventArgs.ExceptionObject);
	        }
	        MessageBox.Show(sbEx.ToString());
            string errInfo = ((Exception) unhandledExceptionEventArgs.ExceptionObject).ToString();
            Log.Error(errInfo);
	        if (existFlag)
	        {
	            Environment.Exit(0);
	        }
	    }
        protected override void OnExit(ExitEventArgs e)
		{
			AtpInitial.Stop();
			MqInitial.Stop();
		}
	}
}
