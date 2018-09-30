using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    class AlarmTable
    {
        public int alarmCauseNo
        { set; get; }
        public string alarmModel
        { set; get; }
        public int alarmIsStateful
        { set; get; }
        //中文描述
        public string alarmChineseDesc
        { set; get; }
        public string alarmSubCauseNo
        { set; get; }
        public string alarmType
        { set; get; }
        public string alarmSeverity
        { set; get; }
        //告警解释
        public string alarmExplain
        { set; get; }
        //故障影响
        public string alarmConsequence
        { set; get; }
        public string alarmRepairAdvise
        { set; get; }
        public string alarmFaultObjectType
        { set; get; }
    }
}
