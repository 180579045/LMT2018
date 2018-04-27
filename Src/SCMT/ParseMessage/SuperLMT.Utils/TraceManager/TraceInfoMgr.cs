using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnmpSharpNet;
using Common.Logging;
using System.Net;

namespace SuperLMT.Utils.TraceManager
{
    public class TraceInfoMgr
    {

        /// <summary>
        /// The shared <see cref="Common.Logging.ILog"/> instance for this class (and derived classes).
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(TraceInfoMgr));

        #region 单例实现
       
        private static readonly TraceInfoMgr g_instance;
        public static TraceInfoMgr Instance
        {
            get { return g_instance; }
        }

        private TraceInfoMgr() 
        {
            Init();
        }

        static TraceInfoMgr()
        {
            g_instance = new TraceInfoMgr();
        }
        
        #endregion

#region 公共函数
        /// <summary>
        /// 初始化 从XML文件中将增加、查询、删除命令所需的参数读取出来
        /// </summary>
        /// <returns></returns>
        private bool Init() 
        {
            TraceDocMgr docMgr = new TraceDocMgr();
            ListGetTrace = docMgr.CmdGetInfo();
            ListAddTrace = docMgr.CmdAddInfo();
            ListDelTrace = docMgr.CmdDelInfo();
            CommonOid = docMgr.CmdCommonOid();
            return true;
        }


        public SnmpAgentInfo GetSnmpAgent(string peerAddress, int retryNum, int iterval)
        {
            SnmpAgentInfo snmpAgent = new SnmpAgentInfo();
            snmpAgent.PeerAddress = IPAddress.Parse(peerAddress);
            snmpAgent.Community = "private";
            snmpAgent.RetryTime = retryNum;                                     /*失败重复次数*/
            snmpAgent.TimeOut = iterval;                                              /*重发间隔时间*/

            return snmpAgent;

        }


        /// <summary>
        /// 下发Get命令
        /// </summary>
        /// <param name="peerAddress">对端IP</param>
        /// <param name="nIndex">对应索引</param>
        /// <returns></returns>
        public IEnumerator<Vb> GetTraceTask(string peerAddress, int nIndex)
        {
            SnmpAgentInfo snmpAgent = GetSnmpAgent(peerAddress, 2, 2000);
            string index = string.Format(".{0}", nIndex);
            int count = ListGetTrace.Count;
            Vb[] vbs = new Vb[count];
            int curNum = 0;
            foreach (TraceItem item in ListGetTrace)
            {
                vbs[curNum] = new Vb(CommonOid + item.Oid + index);
                curNum++;
            }
            IEnumerator<Vb> getRes = null;
            try 
            {
                getRes = SnmpSessionHelper.Instance.Get(snmpAgent, vbs);
                return getRes;
            }
            catch (SnmpErrorStatusException snmpex)
            {
                log.Error(string.Format("查询网元{0}MIB版本号失败!, 原因为:{1}", snmpAgent.PeerAddress, snmpex.Message));
            }
            catch (System.Exception ex)
            {
                log.Error(string.Format("查询网元{0}MIB版本号失败!, 原因为:{1}", snmpAgent.PeerAddress, ex.Message));
            }
            return null;
            
            
        }

        /// <summary>
        /// 删除对应管理站的对应索引的命令
        /// </summary>
        /// <param name="peerAddress">对端IP</param>
        /// <param name="nIndex">对应索引</param>
        /// <returns></returns>
        public bool DelTraceTask(string peerAddress, int nIndex)
        {
            SnmpAgentInfo snmpAgent = GetSnmpAgent(peerAddress, 2, 2000);
            string index = string.Format(".{0}", nIndex);
            int count = ListDelTrace.Count;
            Vb[] vbs = new Vb[count];
            int curNum = 0;
            foreach (TraceItem item in ListDelTrace)
            {
                vbs[curNum] = new Vb(new Oid(CommonOid + item.Oid + index), new Integer32(6));
                curNum++;
            }
            IEnumerator<Vb> getRes = null;

            try
            {
                getRes = SnmpSessionHelper.Instance.Set(snmpAgent, vbs);

                return true;
            }
            catch (SnmpErrorStatusException snmpex)
            {
                log.Error(string.Format("启动网元{0}的删除命令失败!, 原因为:{1}", snmpAgent.PeerAddress, snmpex.Message));
            }
            catch (System.Exception ex)
            {
                log.Error(string.Format("启动网元{0}的删除命令失败!, 原因为:{1}", snmpAgent.PeerAddress, ex.Message));
            }

            return false;
        }


        /// <summary>
        /// 下发添加命令
        /// </summary>
        /// <param name="peerAddresss"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public bool AddTraceTask(string peerAddresss, int nIndex)
        {
            SnmpAgentInfo snmpAgent = GetSnmpAgent(peerAddresss, 2, 2000);
            string index = string.Format(".{0}", nIndex);
            int count = ListAddTrace.Count;
            Vb[] vbs = new Vb[count];
            int curNum = 0;
            TraceItem inter = new TraceItem();
            foreach (TraceItem item in ListAddTrace)
            {
                if ("跟踪任务类型" == item.DisplayName) { inter = item; }
                if ("跟踪管理对象OID" == item.DisplayName)
                {
                    if (null != inter)
                    {
                        if ("" == item.Value)
                        {
                            item.Value = "0";
                        }
                        string curValue = CommonOid + SpcialDeal(inter.Value) + item.Value;
                        vbs[curNum] = new Vb(new Oid(CommonOid + item.Oid + index), new Oid(curValue));
                    }
                    else
                    {
 
                    }
                }
                else
                {
                    if ("行状态" == item.DisplayName)
                    {
                        vbs[curNum] = new Vb(new Oid(CommonOid + item.Oid + index), new Integer32(4));
                    }
                    else
                    {
                        vbs[curNum] = new Vb(new Oid(CommonOid + item.Oid + index), item.GetValue());
                    }
                    
                }
                
                curNum++;
            }
            IEnumerator<Vb> getRes = null;

            try
            {
                getRes = SnmpSessionHelper.Instance.Set(snmpAgent, vbs);

                return true;
            }
            catch (SnmpErrorStatusException snmpex)
            {
                log.Error(string.Format("启动网元{0}的删除命令失败!, 原因为:{1}", snmpAgent.PeerAddress, snmpex.Message));
            }
            catch (System.Exception ex)
            {
                log.Error(string.Format("启动网元{0}的删除命令失败!, 原因为:{1}", snmpAgent.PeerAddress, ex.Message));
            }

            return false;
        }

        /// <summary>
        /// 循环查询没有使用的index下发
        /// </summary>
        /// <param name="peerAddress"></param>
        /// <returns></returns>
        public int AddTaskWithOutIndex(string peerAddress)
        {
            Random random = new Random();
            
            IEnumerator<Vb> getRes = null;
            SnmpAgentInfo snmpAgent = GetSnmpAgent(peerAddress, 2, 2000);

            //Vb[] vbs = new Vb[count];
           
            int nIndex = random.Next(64);
            getRes = GetTraceTask(peerAddress, nIndex);
            while(null != getRes)
            {
                nIndex = random.Next(64);
                getRes = GetTraceTask(peerAddress, nIndex);
            }

            if (AddTraceTask(peerAddress, nIndex))
            {
                return nIndex;
            }

            return -1;
        }


        /// <summary>
        /// 特殊处理OID节点，这里先是 只对 ENB的处理，有可能影响到EPC
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string SpcialDeal(string value) 
        {
            string strPre;
            switch (value)
            {
                // S1, X2(接口)
                case "1":
                case "2":
                        strPre = "2.4.1.4.4.1.1.1.";
                        break;
                // 空口，小区
                case "3":
                case "4":
                        strPre = "2.4.4.1.1.1.";
                        break;
                case "5":
                        strPre = "";
                        break;
                default:
                    strPre = "";
                    break;
            }
            return strPre;
        }
#endregion


#region     属性
        private List<TraceItem> _listAddTrace;  //添加命令
        public List<TraceItem> ListAddTrace
        {
            get { return _listAddTrace; }
            set { _listAddTrace = value; }
        }

        private List<TraceItem> _listGetTrace; //查询命令
        public List<TraceItem> ListGetTrace
        {
            get { return _listGetTrace; }
            set
            {
                _listGetTrace = value;
            }
        }

        private List<TraceItem> _listDelTrace; //删除命令
        public List<TraceItem> ListDelTrace
        {
            get { return _listDelTrace; }
            set
            {
                _listDelTrace = value;
            }
        }

        private string _commonOid;
        public string CommonOid
        {
            get
            {
                return _commonOid;
            }
            set
            {
            	_commonOid = value;
            }
        }
#endregion

    }
}
