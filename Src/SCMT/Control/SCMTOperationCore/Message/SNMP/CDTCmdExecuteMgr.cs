using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Message.SNMP
{
    /// <summary>
    /// 以命令名称方式发送Snmp消息
    /// </summary>
    public sealed class CDTCmdExecuteMgr
    {
        private static CDTCmdExecuteMgr _instance = null;

        private static readonly object SynObj = new object();

        private CDTCmdExecuteMgr()
        {
        }

        public static CDTCmdExecuteMgr GetInstance()
        {
            if (null == _instance)
            {
                lock(SynObj)
                {
                    if (null == _instance)
                    {
                        _instance = new CDTCmdExecuteMgr();
                    }
                }
            }

            return _instance;
        }


        /// <summary>
        /// 执行一条类型为Get的同步操作命令
        /// </summary>
        /// <param name="cmdName"></param>
        /// <param name="requestId"></param>
        /// <param name="strIndex"></param>
        /// <param name="strIpAddr"></param>
        /// <param name="lmtPdu"></param>
        /// <returns></returns>
        public int CmdGetSync(string cmdName, out long requestId, string strIndex
                              , string strIpAddr, ref CDTLmtbPdu lmtPdu)
        {
            requestId = 0;

            Log.Info("CmdGetSync() start");
            string msg = string.Format("cmdName={0},requestId={1},strIndex={2}, strIpAddr={3}"
                , cmdName, requestId, strIndex, strIpAddr);
            Log.Info(msg);

            if (string.IsNullOrEmpty(cmdName) || string.IsNullOrEmpty(strIpAddr))
            {
                return -1;
            }

            // TODO: 数据库先打桩
            string strMibList = "1.2.3|1.2.4|1.2.5";

            if (string.IsNullOrEmpty(strMibList))
            {
                return -1;
            }

            // 转换为oid数组
            string[] mibList = StringToArray(strMibList);
            if (mibList == null)
            {
                return -1;
            }


            // TODO : bNeedCheck

            // TODO:
            // 获取oid的前缀
            string strPreFixOid = "10";
            string strOid;
            StringBuilder sbOid = new StringBuilder();

            foreach(string v in mibList)
            {
                sbOid.Append(strPreFixOid);
                sbOid.AppendFormat("{0}.{1}.{2}", strPreFixOid, v, strIndex);
                CDTLmtbVb vb = new CDTLmtbVb();
//                lmtPdu.AddVb();
            }

            return 0;
        }

        /// <summary>
        /// 将以"|"分割的字符串转换为数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string[] StringToArray(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            return str.Split('|');
        }
        
    }
}
