

namespace CDLBrowser.Parser.Document.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class EventNew
    {  
        public EventNew(){            
        }
        [DataBaseIngoredAttribute(false)]
        public int Oid { get; set; }
        [DataBaseIngoredAttribute(false)]
        public int DisplayIndex { get; set; }
        [DataBaseIngoredAttribute(false)]
        public string TimeStamp { get; set; }
        [DataBaseIngoredAttribute(false)]
        public string EventName { get; set; }
        [DataBaseIngoredAttribute(false)]
        public string MessageDestination { get; set; }
        [DataBaseIngoredAttribute(false)]
        public string MessageSource { get; set; }
        [DataBaseIngoredAttribute(false)]
        public string data { get; set; }
        private byte[] rawData;
        [DataBaseIngoredAttribute(false)]
        public byte[] RawData
        {
            get
            {
                return this.rawData;
            }
            set
            {
                rawData = value;
            }

        }
    }
}
