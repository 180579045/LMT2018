using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    class AntennaArrayTypeTable
    {
        //天线阵大排行，对应excel表中的第一列
        public int antArrayNotMibNumber
        { get; set; }
        //天线型号厂家索引
        public int antArrayVendor
        { get; set; }
        //天线型号厂家名称
        public string antArrayNotMibVendorName
        { get; set; }
        //天线型号索引
        public int antArrayIndex
        { get; set; }
        //天线型号名称
        public string antArrayModelName
        { get; set; }
        //天线阵天线根数
        public int antArrayNum
        { get; set; }
        //天线间距
        public int antArrayDistance
        { get; set; }
        //天线阵形状
        public string antArrayType
        { get; set; }
        //有损无损
        public string antArrayNotMibAntLossFlag
        { get; set; }
        //天线阵规划波束宽度，60/75
        public string netAntArrayNotMibHalfPowerBeamWidth
        { get; set; }
    }
}
