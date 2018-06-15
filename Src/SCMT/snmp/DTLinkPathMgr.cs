using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Message.SNMP
{
    /// <summary>
    /// 与eNB的通信链路管理器
    /// </summary>
    public class DTLinkPathMgr : ICDTLinkPathMgr
    {
        private static DTLinkPathMgr _instance = null;
        private static readonly object SynObj = new object();

        private static bool m_SnmpAlive = false;

        // 同步snmp连接信息(每个基站分别建立一个同步连接和一个异步连接)
        private static Dictionary<string, LmtbSnmpEx> m_SnmpList = new Dictionary<string, LmtbSnmpEx>();

        // 管理侧trap端口
        public long m_TrapPort { get; set; }


        private DTLinkPathMgr()
        {
        }

        public static DTLinkPathMgr GetInstance()
        {
            if (null == _instance)
            {
                lock(SynObj)
                {
                    if (null == _instance)
                    {
                        _instance = new DTLinkPathMgr();
                    }
                }
            }
            return _instance;
        }

        public LmtbSnmpEx getSnmpByIp(string destIp)
        {
            return m_SnmpList[destIp];
        }

        /// <summary>
        /// 启动SNMP通信模块，每个基站建立一次连接
        /// </summary>
        /// <returns></returns>
        public bool StartSnmp(string commnuity, string destIpAddr)
        {
 //           Log.Debug("========== StartSnmp() Start ==========");

            
            // 连接不存在
            if (!m_SnmpList.ContainsKey(destIpAddr) ||  m_SnmpList[destIpAddr] == null)
            {
                LmtbSnmpEx lmtbSnmpEx = new LmtbSnmpEx();
                // 启动SNMP通信
                int status = lmtbSnmpEx.SnmpLibStartUp(commnuity, destIpAddr);
                m_SnmpList.Add(destIpAddr, lmtbSnmpEx);

            }

            Log.Debug("========== StartSnmp() End ==========");
            return true;
        }
  
    }
}
