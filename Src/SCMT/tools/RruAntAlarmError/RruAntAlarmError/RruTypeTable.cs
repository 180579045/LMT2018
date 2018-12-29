using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    class RruTypeTable
    {
        public int rruTypeManufacturerIndex
        { get; set; }
        public string rruTypeNotMibManufacturerName
        { get; set; }
        public int rruTypeIndex
        { get; set; }
        public string rruTypeName
        { get; set; }
        public int rruTypeMaxAntPathNum
        { get; set; }
        public int rruTypeMaxTxPower
        { get; set; }
        public int rruTypeBandWidth
        { get; set; }
        public string rruTypeFiberLength
        { get; set; }
        public string rruTypeIrCompressMode
        { get; set; }
        public string rruTypeSupportCellWorkMode
        { get; set; }
        public string rruTypeFamilyName
        { get; set; }
        //excel表中的射频通道数
        public int rruTypeNotMibMaxePortNo
        { get; set; }
        //excel表中支持的工作模式
        public string rruTypeNotMibSupportNetWorkMode
        { get; set; }
        //支持的IR口速率
        public string rruTypeNotMibIrRate
        { get; set; }
        //压缩属性与带宽约束关系
        public string rruTypeNotMibIrBand
        { get; set; }
        public string rruTypeNotMibIsPico
        { get; set; }
    }
}
