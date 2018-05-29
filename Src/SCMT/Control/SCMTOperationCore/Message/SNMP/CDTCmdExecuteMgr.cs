using LogManager;
using SnmpSharpNet;
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

        public void Initialize()
        {
            
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
                              , string strIpAddr, ref CDTLmtbPdu lmtPdu, bool isPrint = false
            , bool needCheck = false, long timeOut = 0)
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
            string strMibList = "";
            // test
            if (cmdName.Equals("aaa"))
            {
                strMibList = "100.1.9.1.1|100.1.9.1.2";
            }
            else
            {
                strMibList = "100.1.2.1.1.1.5.3";
            }

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
            if (needCheck)
            {
            }

            // TODO:
            // 获取oid的前缀
            string strPreFixOid = "1.3.6.1.4.1.5105";
            StringBuilder sbOid = new StringBuilder();

            foreach(string v in mibList)
            {
                sbOid.Clear();
                sbOid.AppendFormat("{0}.{1}{2}", strPreFixOid, v, strIndex);
                CDTLmtbVb vb = new CDTLmtbVb();
                vb.set_Oid(sbOid.ToString());
                lmtPdu.AddVb(vb);
            }

            lmtPdu.setCmdName(cmdName);
            lmtPdu.setPrintId(isPrint);
            lmtPdu.setSyncId(true);

            // 根据ip获取当前基站的snmp实例
            LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetInstance().getSnmpByIp(strIpAddr);
            int rs = lmtbSnmpEx.SnmpGetSync(lmtPdu, out requestId, strIpAddr, timeOut);
            if (rs != 0)
            {
                Log.Error("执行lmtbSnmpEx.SnmpGetSync()方法错误");
            }

            return 0;
        }

        /// <summary>
        /// 执行一条类型为Set的同步操作命令
        /// </summary>
        /// <param name="cmdName"></param>
        /// <param name="requestId"></param>
        /// <param name="name2Value"></param>
        /// <param name="strIndex"></param>
        /// <param name="strIpAddr"></param>
        /// <param name="lmtPdu"></param>
        /// <param name="isPrint"></param>
        /// <param name="needCheck"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public int CmdSetSync(String cmdName, out long requestId, Dictionary<string,string> name2Value
            , string strIndex, string strIpAddr, ref CDTLmtbPdu lmtPdu, bool isPrint = false
            , bool needCheck = false, long timeOut = 0)
        {
            requestId = 0;
            int rs = 0;

            if (lmtPdu == null)
            {
                return -1;
            }
            lmtPdu.Clear();

            // TODO: 从数据库获取命令对应的oid

            string strMibList = "100.1.2.2.2.1.2|100.1.2.2.2.1.3|100.1.2.2.2.1.4|100.1.2.2.2.1.5|100.1.2.2.2.1.6|100.1.2.2.2.1.7";
            if (string.IsNullOrEmpty(strMibList))
            {
                return -1;
            }

            // 将字符串转换为oid数组
            string[] mibList = StringToArray(strMibList);
            if (mibList == null)
            {
                return -1;
            }

            string strIndexFmt = string.Format(".{0}", strIndex.Trim('.'));

            // TODO
            if (needCheck)
            {
            }

            // 获取oid的前缀
            string strPreFixOid = "1.3.6.1.4.1.5105";
            StringBuilder sbOid = new StringBuilder();
            string strMibValue;
            string strTyep;
            string strMibName;

            foreach (string v in mibList)
            {
                sbOid.Clear();
                sbOid.AppendFormat("{0}.{1}{2}", strPreFixOid, v, strIndex);
                CDTLmtbVb vb = new CDTLmtbVb();
                vb.set_Oid(sbOid.ToString());


                strTyep = "";
                strMibName = "";
                strMibValue = "";

                // TODO: 从数据库中获取oid的名称和数据类型

                MibNodeInfoTest mibNodeInfoTest = GetMibNodeInfoByOID(strIpAddr, v);
                strTyep = mibNodeInfoTest.strType;
                strMibName = mibNodeInfoTest.strMibName;

                if (name2Value.ContainsKey(strMibName))
                {
                    strMibValue = name2Value[strMibName];
                }
                else
                {
                    continue;
                }


                vb.set_Value(strMibValue);
                // TODO:
                vb.set_Syntax(GetAsnTypeByMibType(strTyep));

                lmtPdu.AddVb(vb);
            }

            lmtPdu.setCmdName(cmdName);
            lmtPdu.setPrintId(isPrint);
            lmtPdu.setSyncId(true);



            LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetInstance().getSnmpByIp(strIpAddr);
            rs = lmtbSnmpEx.SnmpSetSync(lmtPdu, out requestId, strIpAddr, timeOut);
            if (rs != 0)
            {
                Log.Error("执行lmtbSnmpEx.SnmpGetSync()方法错误");
            }



            return rs;

        }


        // test
        private MibNodeInfoTest GetMibNodeInfoByOID(string strIpAddr, string strMibOid)
        {
            Dictionary<string, MibNodeInfoTest> mibNodeInfoList = new Dictionary<string, MibNodeInfoTest>();

            string oid = "100.1.2.2.2.1.2";
            string strType = "LONG";
            string strMibName = "fileTransRowStatus";
            MibNodeInfoTest a = new MibNodeInfoTest(oid, strType, strMibName);
            mibNodeInfoList.Add(oid, a);

            oid = "100.1.2.2.2.1.3";
            strType = "LONG";
            strMibName = "fileTransType";
            MibNodeInfoTest b = new MibNodeInfoTest(oid, strType, strMibName);
            mibNodeInfoList.Add(oid, b);


            oid = "100.1.2.2.2.1.4";
            strType = "LONG";
            strMibName = "fileTransIndicator";
            MibNodeInfoTest c = new MibNodeInfoTest(oid, strType, strMibName);
            mibNodeInfoList.Add(oid, c);

            oid = "100.1.2.2.2.1.5";
            strType = "OCTETS";
            strMibName = "fileTransNEDirectory";
            MibNodeInfoTest d = new MibNodeInfoTest(oid, strType, strMibName);
            mibNodeInfoList.Add(oid, d);

            oid = "100.1.2.2.2.1.6";
            strType = "OCTETS";
            strMibName = "fileTransFTPDirectory";
            MibNodeInfoTest e = new MibNodeInfoTest(oid, strType, strMibName);
            mibNodeInfoList.Add(oid, e);

            oid = "100.1.2.2.2.1.7";
            strType = "OCTETS";
            strMibName = "fileTransFileName";
            MibNodeInfoTest f = new MibNodeInfoTest(oid, strType, strMibName);
            mibNodeInfoList.Add(oid, f);

            return mibNodeInfoList[strMibOid];

        }

        public SNMP_SYNTAX_TYPE GetAsnTypeByMibType(string strMibType)
        {
            if ("OCTETS".Equals(strMibType))
            {
                return (SNMP_SYNTAX_TYPE)AsnType.OCTETSTRING;
            } else if ("LONG".Equals(strMibType))
            {
                return (SNMP_SYNTAX_TYPE)AsnType.INTEGER ;
            }

            return (SNMP_SYNTAX_TYPE)AsnType.OCTETSTRING;
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

    class MibNodeInfoTest
    {
        public string oid { get; set; }
        public string strType { get; set; }
        public string strMibName { get; set; }

        public MibNodeInfoTest(string oid, string strType, string strMibName)
        {
            this.oid = oid;
            this.strType = strType;
            this.strMibName = strMibName;

        }
    }
}
