using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    //考虑到该表只在SCMT中作SNMP命令下发,不作弹框显示,
    //故此处表设计按照antennaWeightMultAntEntry来设计,以便下发snmp命令
    class AntennaWeightTable
    {
        //天线阵大排行，对应excel表中的第一列
        public int antArrayNotMibNumber
        { get; set; }
        public int antennaWeightMultFrequencyBand
        { get; set; }
        public int antennaWeightMultAntGrpIndex
        { get; set; }
        public int antennaWeightMultAntStatusIndex
        { get; set; }
        public string antennaWeightMultNotMibAntStatus
        { get; set; }
        public int antennaWeightMultAntHalfPowerBeamWidth
        { get; set; }
        public int antennaWeightMultAntVerHalfPowerBeamWidth
        { get; set; }
        public int antennaWeightMultAntAmplitude0
        { get; set; }
        public int antennaWeightMultAntPhase0
        { get; set; }
        public int antennaWeightMultAntAmplitude1
        { get; set; }
        public int antennaWeightMultAntPhase1
        { get; set; }
        public int antennaWeightMultAntAmplitude2
        { get; set; }
        public int antennaWeightMultAntPhase2
        { get; set; }
        public int antennaWeightMultAntAmplitude3
        { get; set; }
        public int antennaWeightMultAntPhase3
        { get; set; }
        public int antennaWeightMultAntAmplitude4
        { get; set; }
        public int antennaWeightMultAntPhase4
        { get; set; }
        public int antennaWeightMultAntAmplitude5
        { get; set; }
        public int antennaWeightMultAntPhase5
        { get; set; }
        public int antennaWeightMultAntAmplitude6
        { get; set; }
        public int antennaWeightMultAntPhase6
        { get; set; }
        public int antennaWeightMultAntAmplitude7
        { get; set; }
        public int antennaWeightMultAntPhase7
        { get; set; }
    }
}
