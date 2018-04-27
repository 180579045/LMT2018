using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDLBrowser.Parser.Configuration
{
    public class EventsStatisticRecord
    {
        public EventsStatisticRecord(string eventname,string argumenttype,string argumentname)
        {
            EventName = eventname;
            ArgumentType = argumenttype;
            ArgumentName = argumentname;
        }
        /// <summary>
        /// 消息名称
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string ArgumentType { get; set; }
        /// <summary>
        /// 具体参数名称
        /// </summary>
        public string ArgumentName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ArgumentValue { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 建立消息的半帧号
        /// </summary>
        public string HalfSubFrameNo { get;set;}

        /// <summary>
        /// 释放时间
        /// </summary>
        public string ReleaseTime { get; set; }

        /// <summary>
        /// 释放消息的半帧号
        /// </summary>
        public string ReleaseSFN { get; set; }

        /// <summary>
        /// ENBUeId
        /// </summary>
        public string ENBUeId { get; set; }

        /// <summary>
        /// CellUeIndex
        /// </summary>
        public string CellUeIndex { get; set; }

        /// <summary>
        /// CRNTI
        /// </summary>
        public string CRNTI { get; set; }

        /// <summary>
        /// CellId
        /// </summary>
        public string CellId { get; set; }

        /// <summary>
        /// DisplyIndex
        /// </summary>
        public int DisplyIndex { get; set; }
    }
}
