// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Event.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the Event type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.Event
{
    using System;
    using System.IO;
	using System.Windows.Documents;
    using CDLBrowser.Parser.Configuration;
    using System.Xml.Serialization;
    using DevExpress.Xpo;
    using SuperLMT.Utils;
    using System.Text.RegularExpressions;

    using System.Text;


    /// <summary>
    /// The event.
    /// </summary>
    //public class Event : XPObject, IEvent
    public class Event : IEvent
   {
        /// <summary>
        /// The half sub frame no.
        /// </summary>
        private int halfSubFrameNo;

        /// <summary>
        /// The protocol.
        /// </summary>
        private string protocol;

        //private bool _isMarked;
        /// <summary>
        /// The raw data.
        /// </summary>
        private byte[] rawData;


        private uint enbueid = 0xffffffff;
        private uint crnti = 65535;
        private uint padarry = 0;
        /// <summary>
        /// The ueType.
        /// </summary>
        private string ueType = "未知类型";
        //解析过程锁定对象，防止多任务重写结果
        private static object parseLock = new object();
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        public Event(Session s)
        {
        }

        public Event() : base()
        {
            EventName = "UNKNOWN TYPE";

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        //internal Event()
        //{
        //}
        [DataBaseIngoredAttribute(false)]
        public  int Oid { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether is marked.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public bool IsMarked
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the raw data.
        /// </summary>
        [Delayed]
        [DataBaseIngoredAttribute(false)]
        public byte[] RawData
        {
            get
            {
                if (null != this.rawData)
                {
                    return this.rawData;
                }
                return this.rawData;
                //return this.GetDelayedPropertyValue<byte[]>("RawData");
            }

            set
            {
                //this.SetPropertyValue("RawData", ref this.rawData, value);
                this.rawData = value;
            }
        }

        /// <summary>
        /// Gets or sets the host Parse id.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public int ParsingId { get; set; }

        /// <summary>
        /// Gets or sets the log file id.
        /// </summary>
        [DataBaseIngoredAttribute(true)]
        public int LogFileId { get; set; }

        /// <summary>
        /// Gets or sets the event id.
        /// </summary>
        [DataBaseIngoredAttribute(true)]
        public int EventIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public int DisplayIndex { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [DataBaseIngoredAttribute(true)]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public string TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the protocol.(interface name)
        /// </summary>
       [DataBaseIngoredAttribute(false)]
        public string Protocol
        {
            get
            {
                return this.protocol;
            }

            set
            {
                //this.SetPropertyValue("Protocol", ref this.protocol, value);
                this.protocol = value;
            }
        }
        /// <summary>
        /// Gets or sets the name.(Event Name)
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the message source.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public string MessageSource { get; set; }

        /// <summary>
        /// Gets or sets the message destination.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public string MessageDestination { get; set; }

        /// <summary>
        /// Gets or sets the half sub frame No.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public int HalfSubFrameNo
        {
            get
            {
                return this.halfSubFrameNo;
            }

            set
            {
                //this.SetPropertyValue<int>("HalfSubFrameNo", ref this.halfSubFrameNo, value);
                this.halfSubFrameNo = value;
            }
        }

        /// <summary>
        /// Gets or sets the ne id.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public uint HostNeid { get; set; }

        /// <summary>
        /// Gets or sets the local cell id.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public ushort LocalCellId { get; set; }

        /// <summary>
        /// Gets or sets the cell id.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public ushort CellId { get; set; }

        /// <summary>
        /// Gets or sets the cell user equipment id.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public ushort CellUeId { get; set; }

                

        /// <summary>
        /// Gets or sets the tick time.
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public ulong TickTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is marked.
        /// </summary>
        [DataBaseIngoredAttribute(true)]
        public string LogName { get; set; }

        /// <summary>
        ///  Gets or sets the uetype.
        /// </summary>
        [DataBaseIngoredAttribute(true)]
        public string UeType 
        { 
            get
            {
                return this.ueType;
            }
            set
            {
                this.ueType = value;
            }
        }
        [DataBaseIngoredAttribute(false)]
        public bool IsDeleted { set; get; }

        [DataBaseIngoredAttribute(false)]
        public string MsgBody { set; get; }
        /// <summary>
        /// Gets or sets the protocol.(interface name)
        /// </summary>

        [DataBaseIngoredAttribute(false)]
        public uint EnbUeId 
        {
            get
            {
               return enbueid;

            }
            set
            {
               enbueid = value;
            }
     
        }
        [DataBaseIngoredAttribute(false)]
        public uint CRNTI
        {
            get
            {
               return crnti ;

            }
            set
            {
               crnti = value;
            }
        
        }

        string mmeues1apid = "-";

        [DataBaseIngoredAttribute(false)]
        public string MMEUES1APID
        {
            get { return mmeues1apid; }
            set { mmeues1apid = value; }
        }


/// <summary>
/// 
/// </summary>

        string ue_x2Ap_Id = "-";

        [DataBaseIngoredAttribute(false)]
        public string UEX2APID
        {
            get { return ue_x2Ap_Id; }
            set { ue_x2Ap_Id = value; }
        }
        /*
        string teId = "-";
        [DataBaseIngoredAttribute(false)]
        public string TEID
        {
            get { return teId; }
            set { teId = value; }
        }*/

        string _imsi = "-";
        [DataBaseIngoredAttribute(false)]
        public string IMSI
        {
            get { return _imsi; }
            set { _imsi = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataBaseIngoredAttribute(false)]
        public uint PADARRY
        {
            get
            {
                return padarry;

            }
            set
            {
                padarry = value;
            }

        }
		
        private string GetMMEUES1APID(string content, string keyWord)
        {
            if (content != null)
            {
                var targetPosition = content.IndexOf(keyWord, StringComparison.Ordinal);
                if (targetPosition == -1)
                {
                    return "-";
                }

                var length = keyWord.Length;
                targetPosition = targetPosition + length - 1;

                var positionBegin = content.IndexOf(':', targetPosition);
                var positionEnd = content.IndexOf('\n', targetPosition);
                if (positionBegin == -1 || positionBegin > positionEnd)
                {
                    positionBegin = content.IndexOf(' ', targetPosition);
                }

                var targetValue = content.Substring(positionBegin + 1, positionEnd - positionBegin - 1);
                targetValue = targetValue.Trim(',').Trim(' ');
                return targetValue;
            }

            return "-";
        }
        /// <summary>
        /// The initialize persistent data.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public void InitializePersistentData(byte[] buffer, EventParser eventParser)
        {
            eventParser.Parse(buffer);

            // if message using binding,need parse all
            this.HalfSubFrameNo = Convert.ToInt32(eventParser.GetEventTreeHeadNode("Time").Value);

            this.EventIdentifier = Convert.ToInt32(eventParser.GetEventTreeHeadNode("EventType").Value);

            string messageSource;
            string messageDestination;
            string evtname;
            eventParser.GetMessageDirection(this, out messageSource, out messageDestination,out evtname);

            this.MessageSource = messageSource;
            this.MessageDestination = messageDestination;
            this.EventName = evtname;

            this.Protocol = OffLineProtocolInfoManager.GetSingleton().GetProtocolName(Convert.ToUInt32(eventParser.GetEventTreeHeadNode("InterfaceType").Value));

            this.LocalCellId = Convert.ToUInt16(eventParser.GetEventTreeHeadNode("LocalCellID").Value);

            this.CellUeId = Convert.ToUInt16(eventParser.GetEventTreeHeadNode("CellUEIndex").Value);

            this.CellId = Convert.ToUInt16(eventParser.GetEventTreeHeadNode("CellID").Value);

            this.EventIdentifier = Convert.ToInt32(eventParser.GetEventTreeHeadNode("EventType").Value);

            //this.EventName = eventParser.eventConfigurationMgr.GetEventConfiguration(this.Version).GetEventBodyNodeById(this.EventIdentifier).GetAttribute("Describe");

            this.RawData = buffer;
        }
        private string GetEventConfigFilePath(string version)
        {
            return AppPathUtiliy.Singleton.GetAppPath() + string.Format(@"\Configuration\Files\eNBCDL_{0}.xml", version);
        }
        /// <summary>
        /// The initialize persistent data.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        public bool InitializePersistentData(MemoryStream stream, string fileVersion)
        {
            lock (parseLock)
            {
                long startPosition = stream.Position;
                this.Version = fileVersion;
 
                EventParser eventParser = EventParserManager.Instance.GetParser(fileVersion);
                eventParser.Parse(stream);

                IConfigNodeWrapper boardTypeNode = eventParser.GetEventTreeHeadNode("Rsv");
                if (boardTypeNode != null)
                {
                     string strBoardType = boardTypeNode.Value.ToString();
                     int boardType = 0;
                    
                     if (Int32.TryParse(strBoardType, out boardType))
                     {
                     	   // sctd/scte板卡都按scte处理
                     	   if(boardType == 0x8d || boardType == 0xb2)
                     	   {
                     	   	   boardType = 0x8d;
                     	   }
                    	   
                         string finalVersion = string.Format("{0}.{1}", fileVersion, boardType);
                         if (boardType > 0 && System.IO.File.Exists(GetEventConfigFilePath(finalVersion)))
                         {
                             if (EventParserManager.Instance.GetParser(finalVersion) == null)
                             {
                                 EventParser parser = new EventParser();
                                 parser.Version = finalVersion;
                                 EventParserManager.Instance.AddEventParser(parser.Version, parser);
                             }
                             //覆盖Version 否则解析消息体会出问题
                             this.Version = finalVersion;
                         }
                     }
                }

                string strdtlength = eventParser.GetEventTreeHeadNode("DataLength").Value.ToString();
                int dtlength;
                if (Int32.TryParse(strdtlength, out dtlength) == false)
                {
                    System.Diagnostics.Debug.WriteLine("datalength is error.");
                
                }

                if (dtlength == 0)
                {
                    long tmplength = stream.Position;
                    stream.Position = startPosition;
                    dtlength = (int)(tmplength - startPosition);
                    var raws = new byte[dtlength];
                    stream.Read(raws, 0, dtlength);
                    this.RawData = raws;
                    return false;
                }
                // finally read row data........in case exception stream may never goto end.
                stream.Position = startPosition;
                dtlength = (int)(dtlength < stream.Length - startPosition ? dtlength : stream.Length - startPosition);
                var rowdata = new byte[dtlength];
                stream.Read(rowdata, 0, dtlength);
                this.RawData = rowdata;

                // if message using binding,need parse all
                string strtime = (string) eventParser.GetEventTreeHeadNode("Time").Value.ToString();
                int tmval;
                if (Int32.TryParse(strtime, out tmval) == false)
                {
                    System.Diagnostics.Debug.WriteLine("time is invalid.");
                    this.HalfSubFrameNo = 0;
                   
                }
                this.HalfSubFrameNo = tmval;
                //added by zhuwentian begin
                if (BTSVersionsManager.HasVersion(Version) == true) {
                    if (eventParser.GetEventTreeHeadNode("Pad[2]") != null && eventParser.GetEventTreeHeadNode("Pad[2]").Value!= null)
                    {
                        string strpad = eventParser.GetEventTreeHeadNode("Pad[2]").Value.ToString();
                        uint pad = 0;
                        if (UInt32.TryParse(strpad, out pad) == false)
                        {
                            System.Diagnostics.Debug.WriteLine("pad[2] is invalid.");
                        }
                        this.padarry = pad;
                    }
                }
                //added by zhuwentian end
                string strtype = eventParser.GetEventTreeHeadNode("EventType").Value.ToString();
                int evtype;
                if (Int32.TryParse(strtype, out evtype) == false)
                {
                    System.Diagnostics.Debug.WriteLine("EventType is invalid.");
                   
                }
                this.EventIdentifier = evtype;
                

                string messageSource;
                string messageDestination;
                string evtname;
                eventParser.GetMessageDirection(this, out messageSource, out messageDestination, out evtname);

                this.MessageSource = messageSource;
                this.MessageDestination = messageDestination;

                if (!string.IsNullOrEmpty(evtname))
                {
                    this.EventName = evtname;
                }


                // 2015-05-07  lixiang start
                if (null != eventParser.GetEventTreeHeadNode("Time2") && null != eventParser.GetEventTreeHeadNode("Time3"))
                {
                    string strTime2 = eventParser.GetEventTreeHeadNode("Time2").Value.ToString();
                    int evTime2 = 0;
                    if (Int32.TryParse(strTime2, out evTime2) == false)
                    {
                        System.Diagnostics.Debug.WriteLine("Time2 is invalid.");
                       
                    }

                    string strTime3 = eventParser.GetEventTreeHeadNode("Time3").Value.ToString();
                    uint evTime3 = 0;
                    if (UInt32.TryParse(strTime3, out evTime3) == false)
                    {
                        System.Diagnostics.Debug.WriteLine("Time3 is invalid.");
                      
                    }
                    string tempTimeStamp;
                    ulong ticks;
                    if (changeTime(evTime2, evTime3, out tempTimeStamp, out ticks))
                    {
                        this.TimeStamp = tempTimeStamp;
                        //不设置会导致统计有问题
                        this.TickTime = ticks;
                    }
                }           
                string strprot = eventParser.GetEventTreeHeadNode("InterfaceType").Value.ToString();
                uint uiprot;
                if (UInt32.TryParse(strprot, out uiprot) == false)
                {
                    System.Diagnostics.Debug.WriteLine("interfacetype is invalid.");
                   
                }
                this.Protocol = OffLineProtocolInfoManager.GetSingleton().GetProtocolName(uiprot);

                ushort ilcellid;
                if (UInt16.TryParse(eventParser.GetEventTreeHeadNode("LocalCellID").Value.ToString(), out ilcellid) == false)
                {
                    System.Diagnostics.Debug.WriteLine("localcellid is invalid.");
                    this.LocalCellId = 0;
                   
                }
                this.LocalCellId = ilcellid;

                ushort icellueind;
                if (UInt16.TryParse(eventParser.GetEventTreeHeadNode("CellUEIndex").Value.ToString(), out icellueind) ==
                    false)
                {
                    System.Diagnostics.Debug.WriteLine("cellueindex is invalid.");
                    this.CellUeId = 255;
                }
                this.CellUeId = icellueind;

                ushort cellid;
                if (UInt16.TryParse(eventParser.GetEventTreeHeadNode("CellID").Value.ToString(), out cellid) == false)
                {
                    System.Diagnostics.Debug.WriteLine("cellid is invalid.");
                    this.CellId = 255;
                }
                this.CellId = cellid;
                
                return true;
            }
        }
        /// <summary>
        /// 合并消息体
        /// </summary>
        /// <param name="root"></param>
        /// <param name="result"></param>
        /// <param name="level"></param>
        private void ExportBodyToXml(IConfigNodeWrapper root, ref string result, int level)
        {

            if (root != null)
            {
                string spaces = "";
                for (int i = 0; i < level; i++)
                {
                    spaces += ' ';
                }
                /*
                string key;
                if (string.IsNullOrEmpty(root.Id))
                {
                    key = "defaultKey";
                }
                else
                {
                    key = root.Id;
                }*/

               // result += spaces + string.Format("<{0}>\r\n", key);
                result += spaces + string.Format("  {0}\r\n", root.DisplayContent);
                level++;
                foreach (var child in root.Children)
                {
                    ExportBodyToXml(child, ref result, level);             
                }
               // result += spaces +  string.Format("</{0}>\r\n", key);
            }
        }

        public void InitOtherParams() 
        {
            try
            {
                if (RawData == null)
                {
                    return;
                }
                if (MsgBody != null)
                {
                    string strUeId = GetNodeFromMsgBody(MsgBody, "eNBUEID");
                    if (strUeId != null)
                    {
                        EnbUeId = Convert.ToUInt32(strUeId);
                    }

                    string strCrnti = GetNodeFromMsgBody(MsgBody, "Crnti");
                    uint crntixx;
                    if (BTSVersionsManager.HasVersion(Version) == true)
                    {
                        CRNTI = this.padarry;
                    }
                    else if (strCrnti != null && uint.TryParse(strCrnti, out crntixx))
                    {
                        CRNTI = Convert.ToUInt32(strCrnti);
                    }

                    mmeues1apid = GetMMEUES1APID(MsgBody, "MME-UE-S1AP-ID");

                    string tmp_x2apId = GetNodeFromMsgBody(MsgBody, "UE-X2AP-ID");
                    if (tmp_x2apId != null)
                    {
                        UEX2APID = tmp_x2apId;
                    }
                    string tmp = FindValueFromTarget(MsgBody, "               |----Imsi   :");
                    if (tmp != null)
                    {
                        IMSI = tmp;
                    }
                    
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine("InitOtherParams:" + exp.Message);
            }
        
        }
        /// <summary>
        /// The find value from target.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="keyWord">
        /// The key word.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected  string FindValueFromTarget(string content, string keyWord)
        {
            var targetPosition = content.IndexOf(keyWord, StringComparison.Ordinal);
            if (targetPosition == -1)
            {
                return null;
            }

            var length = keyWord.Length;
            targetPosition = targetPosition + length - 1;

            var positionBegin = content.IndexOf(':', targetPosition);
            var positionEnd = content.IndexOf("\r\n", targetPosition);
            if (positionBegin == -1 || positionBegin > positionEnd)
            {
                positionBegin = content.IndexOf(' ', targetPosition);
            }

            var targetValue = content.Substring(positionBegin + 1, positionEnd - positionBegin - 1);
            targetValue = targetValue.Trim(',').Trim(' ');
            return targetValue;
        }
        /// <summary>
        /// 查找指定节点
        /// </summary>
        /// <param name="msgbody"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetNodeFromMsgBody(string msgbody, string key) 
        {
            try
            {
                if (string.IsNullOrEmpty(key) == false)
                {
                    string pattern = string.Format("\\b(?<name>{0})\\s+[:]?\\s*(?<value>\\w+)",key);
                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                    Match m = r.Match(msgbody);
                    if (m.Success)
                    {
                        return m.Groups["value"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
             return null;
        }
       /// <summary>
       /// 时间转换
       /// </summary>
       /// <param name="time2">1970年1月1日1时0秒 后秒部分</param>
       /// <param name="time3">ns部分</param>
       /// <param name="myTime">输出时间</param>
       /// <param name="ticks">ticks</param>
       /// <returns></returns>

        bool changeTime(int time2, uint time3, out string strTimeStamp, out ulong ticks)
        {
            strTimeStamp = "";
            ticks = 0;
            if (0 == time2 || 0 == time3 || this.EventName == "UNKNOWN TYPE")
            {
                return false;
            }
            else
            {
                DateTime tStart = new DateTime(1970, 1, 1, 0, 0, 0);
                DateTime currTime = tStart + TimeSpan.FromSeconds(time2);
                
                //time3 表示时间的ns部分，对超出1s的部分进行截断
                time3 = time3 % 1000000000;
               
                // convert to ticks
                long nsticks = (long)time3 / 100;
                currTime = currTime + TimeSpan.FromTicks(nsticks);

                //对小基站版本不进行时区转换
                if (BTSVersionsManager.HasVersion(Version) == false)
                {
                    //yanjiewa 2015-6-13 转换为本地时间
                    currTime = currTime.ToLocalTime();
                }

                // yanjiewa 2015-6-15 修改时间格式化为24小时制-DTMUC00269445
                strTimeStamp = (currTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                // end yanjiewa 2015-6-15 修改时间格式化为24小时制-DTMUC00269445

                ticks = Convert.ToUInt64(currTime.Ticks);

                return true;
            }
        }
        public override string ToString()
        {
            return string.Format("OID={0},timestamp={1},name={2}",Oid,TimeStamp,EventName);
        }
    }
}
