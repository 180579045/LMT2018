// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemInfos.cs" company="dtmobile">
//   dtmobile
// </copyright>
// <summary>
//   trace type ,include off line and on line
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    using System.Collections.Generic;

    /// <summary>
    /// trace type ,include off line and on line
    /// </summary>
    public enum EmTraceType
    {
        /// <summary>
        /// The signal trace.
        /// </summary>
        SignalTrace,

        /// <summary>
        /// The offline.
        /// </summary>
        Offline
    }

    /// <summary>
    /// infos of system
    /// </summary>
    public class SystemInfos  
    {
        private static readonly SystemInfos Instance = new SystemInfos();

        private string defaultTheme = "DeepBlue";

        private SystemInfos()
        {
            // 默认模式
            this.TraceType = EmTraceType.Offline;
        }

        /// <summary>
        /// get all trace types
        /// </summary>
        public IList<string> TraceTypes
        {
            /*get { return Enum.GetNames(typeof(emTraceType)); }*/
           
            get 
            { 
                IList<string> temp = new List<string>();
                temp.Add("离线模式"); /*默认离线*/
                temp.Add("在线模式");
                return temp;
            }
        }

        /// <summary>
        /// trace type selected by user 
        /// </summary>
        public EmTraceType TraceType { get; set; }

        public string ThemeName
        {
            get { return this.defaultTheme; }
            set { this.defaultTheme = value; }
        }

        /// <summary>
        /// The get singleton.
        /// </summary>
        /// <returns>
        /// The <see>
        ///       <cref>SystemInfos</cref>
        ///     </see> .
        /// </returns>
        public static SystemInfos GetSingleton()
        {
            return Instance;
        }
    }
}
