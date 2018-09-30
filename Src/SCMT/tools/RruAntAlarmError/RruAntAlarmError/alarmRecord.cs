using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    class AlarmRecord
    {
        public int Sequence
        { set; get; }
        public string ClearState
        { set; get; }
        public string ClearTime
        { set; get; }
        public int AlaNumer
        { set; get; }
        public int FrameNo
        { set; get; }
        public int SlotNo
        { set; get; }
        public int AlaType
        { set; get; }
        public int AlaDegree
        { set; get; }
        public int AlaValue
        { set; get; }
        public string OccurTime
        { set; get; }
        public string Affirm
        { set; get; }
        public string Attach
        { set; get;}
        public string ReceiveTime
        { set; get; }
        public int RepeatTimes
        { set; get; }
        public int Attribute
        { set; get; }
        public string BoardName
        { set; get; }
        public string ProcName
        { set; get; }
        public int LocationInfo
        { set; get; }
    }
}
