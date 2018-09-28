using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPath
{
    /// <summary>
    /// LMT-eNB与eNB的通信链路的管理器
    /// </summary>
    public interface ICDTLinkPathMgr
    {
        /*启动SNMP模块*/
        bool StartSnmp(string commnuity, string destIpAddr);
    }
}
