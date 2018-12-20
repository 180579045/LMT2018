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
            //捕获应用程序域中发生的异常（包括UI线程和非UI线程）
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
		    //UI线程
		    //Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
		    //TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

        }
	    private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
	    {
	        Log.Error(unobservedTaskExceptionEventArgs.Exception.ToString());

	        Environment.Exit(0);
	    }

	    private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
	    {
	        Log.Error(dispatcherUnhandledExceptionEventArgs.Exception.ToString());
	        
	        Environment.Exit(0);
	    }

	    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
	    {
	        Log.Error(((Exception)unhandledExceptionEventArgs.ExceptionObject).ToString());
	        Environment.Exit(0);
	    }
        protected override void OnExit(ExitEventArgs e)
		{
			AtpInitial.Stop();
			MqInitial.Stop();
		}
	}
}
