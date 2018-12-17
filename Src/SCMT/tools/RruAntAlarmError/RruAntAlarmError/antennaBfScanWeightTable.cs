using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    class AntennaBfScanWeightTable
    {
        //天线阵大排行，对应excel表中的第一列,与sheet 原始值中是一致的
        public string antArrayNotMibNumber { get; set; }
        /// 天线阵厂家索引
        /// </summary>
        public string antArrayBfScanAntWeightVendorIndex { get; set; }
        /// <summary>
        /// 天线阵型号索引
        /// </summary>
        public string antArrayBfScanAntWeightTypeIndex { get; set; }
        /// <summary>
        /// 天线阵索引
        /// </summary>
        public string antArrayBfScanAntWeightIndex { get; set; }
        public string antennaBfScanWeightBFScanGrpNo { get; set; }
        public string antArrayBfScanAntWeightAntGrpNo { get; set; }
        public string antennaBfScanWeightAmplitude0 { get; set; }
        public string antennaBfScanWeightPhase0 { get; set; }
        public string antennaBfScanWeightAmplitude1 { get; set; }
        public string antennaBfScanWeightPhase1 { get; set; }
        public string antennaBfScanWeightAmplitude2 { get; set; }
        public string antennaBfScanWeightPhase2 { get; set; }
        public string antennaBfScanWeightAmplitude3 { get; set; }
        public string antennaBfScanWeightPhase3 { get; set; }
        public string antennaBfScanWeightAmplitude4 { get; set; }
        public string antennaBfScanWeightPhase4 { get; set; }
        public string antennaBfScanWeightAmplitude5 { get; set; }
        public string antennaBfScanWeightPhase5 { get; set; }
        public string antennaBfScanWeightAmplitude6 { get; set; }
        public string antennaBfScanWeightPhase6 { get; set; }
        public string antennaBfScanWeightAmplitude7 { get; set; }
        public string antennaBfScanWeightPhase7 { get; set; }
        public string antennaBfScanWeightHorizonNum { get; set; }
        public string antennaBfScanWeightVerticalNum { get; set; }
    }
}
