using System;
using System.Linq;
using System.Runtime.CompilerServices;
//using Common.Logging;
using log4net;

//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "config/LogCfg/Log4Net.xml", Watch = true)]
namespace LogManager
{
	public static class Log
	{
		//private static readonly ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static ILog log;

		#region static void WriteLog(Exception ex)

		//错误信息
		public static void WriteLogError(Exception ex,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			log.Error($"{memeberName} {lineNumber} Error", ex);
		}

		//严重错误
		public static void WriteLogFatal(Exception ex,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			log.Fatal($"{memeberName} {lineNumber} Fatal", ex);
		}

		#endregion static void WriteLog(Exception ex)

		/// <summary>
		/// 输出日志到Log4Net
		/// </summary>
		/// <param name="msg"></param>

		#region static void WriteLog(string msg)

		//调试信息
		public static void Debug(string msg,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			log.Debug($"{memeberName} {lineNumber} {msg}");
		}

		//一般信息
		public static void Info(string msg,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			log.Info($"{memeberName} {lineNumber} {msg}");
		}

		public static void Warn(string msg,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			log.Warn($"{memeberName} {lineNumber} {msg}");
		}

		public static void Error(string msg,
			[CallerFilePath] string filePath = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerMemberName] string memeberName = null)
		{
			log.Error($"{memeberName} {lineNumber} {msg}");
		}

		public static void SetLogFileName(string strFileName)
		{
			log = CustomRollingFileLogger.GetCustomLogger(strFileName);
		}

		#endregion static void WriteLog(string msg)
	}
}