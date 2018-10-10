using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    class RruTypePortTble
    {
        public int rruTypePortManufacturerIndex
        { get; set; }
        public string rruTypePortNotMibManufacturerName
        { get; set; }
        public int rruTypePortIndex
        { get; set; }
        public int rruTypePortNo
        { get; set; }
        public int rruTypePortPathNo
        { get; set; }
        public string rruTypePortSupportFreqBand
        { get; set; }
        public int rruTypePortSupportFreqBandWidth
        { get; set; }
        public int rruTypePortSupportAbandTdsCarrierNum
        { get; set; }
        public int rruTypePortSupportFBandTdsCarrierNum
        { get; set; }
        public int rruTypePortCalAIqTxNom
        { get; set; }
        public int rruTypePortCalAIqRxNom
        { get; set; }
        public int rruTypePortCalPoutTxNom
        { get; set; }
        public int rruTypePortCalPinRxNom
        { get; set; }
        public int rruTypePortAntMaxPower
        { get; set; }
        public string rruTypePortNotMibRxTxStatus
        { get; set; }
    }
}
