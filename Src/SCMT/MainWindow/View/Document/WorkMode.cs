using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow.View.Document
{
    public enum WorkMode
    {
        LTE_TDD,
        LTE_FDD,
        NBIOT,
        LTE_FDD_NBIOT
    }
    public enum FrameStruMode
    {
        CPRI,
        TDD,
        TDS
    }
    public enum BroadType {
        SCTE,
        SCTF,
        BPOH,
        BPOI,
        BPOK
    }
}
