using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//指定log4net使用的config文件来读取配置信息
[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"config/log4net.config", Watch = true)]
namespace LogManager
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public class LogHelper
    {
        // ILog 实例集合
        private static readonly ConcurrentDictionary<Type, ILog> _loggers = new ConcurrentDictionary<Type, ILog>();

        /// <summary>
        /// 获取记录器
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static ILog GetLogger(Type source)
        {
            if (_loggers.ContainsKey(source))
            {
                return _loggers[source];
            }
            else
            {
                ILog logger = log4net.LogManager.GetLogger(source);
                _loggers.TryAdd(source, logger);
                return logger;
            }
        }

        /// <summary>  
        /// Debug委托  
        /// </summary>  
        /// <param name="message">日志信息</param>  
        public delegate void DDebug(object message);
        public delegate void DDebugFormat(string format, params object[] args);

        /// <summary>  
        /// Info委托  
        /// </summary>  
        /// <param name="message">日志信息</param>  
        public delegate void DInfo(object message);
        public delegate void DInfoFormat(string format, params object[] args);

        /// <summary>  
        /// Warn委托  
        /// </summary>  
        /// <param name="message">日志信息</param>  
        public delegate void DWarn(object message);
        public delegate void DWarnFormat(string foramt, params object[] args);


        /// <summary>  
        /// Error委托  
        /// </summary>  
        /// <param name="message">日志信息</param>  
        public delegate void DError(object message);
        public delegate void DErrorFormat(string foramt, params object[] args);

        /// <summary>  
        /// Fatal委托  
        /// </summary>  
        /// <param name="message">日志信息</param>  
        public delegate void DFatal(object message);
        public delegate void DFatalFormat(string foramt, params object[] args);

        /// <summary>  
        /// Debug  
        /// </summary>  
        public static DDebug Debug
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).Debug; }
        }
        public static DDebugFormat DebugFormat
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).DebugFormat; }
        }

        /// <summary>  
        /// Info  
        /// </summary>  
        public static DInfo Info
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).Info; }
        }
        public static DInfoFormat InfoFormat
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).InfoFormat; }
        }

        /// <summary>  
        /// Warn  
        /// </summary>  
        public static DWarn Warn
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).Warn; }
        }
        public static DWarnFormat WarnFormat
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).WarnFormat; }
        }

        /// <summary>  
        /// Error  
        /// </summary>  
        public static DError Error
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).Error; }
        }
        public static DErrorFormat ErrorFormat
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).ErrorFormat; }
        }

        /// <summary>  
        /// Fatal  
        /// </summary>  
        public static DFatal Fatal
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).Fatal; }
        }
        public static DFatalFormat FatalFormat
        {
            get { return GetLogger((new StackTrace()).GetFrame(1).GetMethod().DeclaringType).FatalFormat; }
        }

    }
}
