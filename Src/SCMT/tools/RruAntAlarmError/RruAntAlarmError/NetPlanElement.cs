using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    class NetPlanElement
    {
        public int number
        { get; set; }
        public string elementName
        { get; set; }
    }

    class ShelfEquipment
    {
        public int number
        { get; set; }
        public int equipNEType
        { get; set; }
        public string equipNETypeName
        { get; set; }
        public int totalSlotNum
        { get; set; }
        public int supportPlanSlotNum
        { get; set; }
        //可规划规划的槽位,以json数组保存每个槽位支持的板卡类型
        public string planSlotInfo
        { get;set;}
        public int columnsUI
        { get; set; }
    }

    class BoardEquipment
    {
        public int number
        { get; set; }
        public int boardType
        { get; set; }
        public string boardTypeName
        { get; set; }
        public int supportEquipType
        { get; set; }
        public string supportConnectElement
        { get; set; }
        public int irOfpNum
        { get; set; }
        //支持的光口数,以json数组保存每个光口支持的速率
        public string irOfpPortTransSpeed
        { get; set; }
    }
    class RHUBEquipment
    {
        public int number
        { get; set; }
        //对应MIB中netRHUBType
        public string rHubType
        { get; set; }
        //对应UI上呈现选择的名称
        public string friendlyUIName
        { get; set; }
        public int irOfpNum
        { get; set; }
        //所有的光口支持速率一样,以json数组保存光口支持的速率
        public string irOfpPortTransSpeed
        { get; set; }
        public int ethPortRNum
        { get; set; }
        //所有的以太口支持速率一样,以json数组保存以太口支持的速率
        public string ethPortTransSpeed
        { get; set; }
    }
}
