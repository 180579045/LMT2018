using CommonUtility;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser.JSONDataMgr;
using MsgQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUploadManager
{
    public class LogUploadHelper
    {
       // private  static readonly LogUploadHelper Instance= new LogUploadHelper();
        //公共日志上传结果记录(File Trans Type--TransResult)
        public Dictionary<int, int> publicLogTransResult = new Dictionary<int, int>();
        private string strEquipIp;
        //public static LogUploadHelper Singleton {
        //    get {
        //        return Instance;
        //    }

        //}
        public LogUploadHelper(){
        }
        public void StartAcceptTapMessage() {            
            SubscribeHelper.AddSubscribe(TopicHelper.SnmpMsgDispose_OnTrap, CallOnTrap);
        }
        private void CallOnTrap(SubscribeMsg msg)
        {
            // 消息类型转换
            //var strTopic = msg.Topic;
            Log.Info($"msg.Topic = {msg.Topic}");

            var lmtPdu = SerializeHelper.DeserializeWithBinary<CDTLmtbPdu>(msg.Data);
            OnFileTransTrap(lmtPdu);
        }
        private int OnFileTransTrap(CDTLmtbPdu lmtPdu) {
            if (null == lmtPdu) {
                Log.Error("传入的PDU指针为空, 操作失败\n");
                return -1;
            }
            var strNodeIp = lmtPdu.m_SourceIp;
            Log.Info($"收到网元Trap, 网元ip:{strNodeIp}");

            // 验证包的合法性
            string strErrorMsg;
            
            if (!CheckPDUValidity(lmtPdu, out strErrorMsg))
            {
                if (strErrorMsg != "")
                {
                    // 打印消息
                    ShowLogHelper.Show(strErrorMsg, lmtPdu.m_SourceIp, InfoTypeEnum.ENB_OTHER_INFO_IMPORT);
                }
                Log.Error("Trap Pdu验证失败!");
                return -1;
            }

            // 获得该网元所对应的MIB OID前缀
            var strOidPrefix = SnmpToDatabase.GetMibPrefix();
            if (string.IsNullOrEmpty(strOidPrefix))
            {
                Log.Error("获取MIB前缀失败!");
                return -1;
            }

            // 验证是否是认识的Trap类型
            var lmtVb = new CDTLmtbVb();
            lmtPdu.GetVbByIndex(1, ref lmtVb); // 第0个为时间戳，第1个为Trap包的OID
            int intTrapType;
            if (!CheckTrapOIDValidity(strNodeIp, lmtVb.Value, strOidPrefix, out intTrapType))
            {
                Log.Error($"验证Trap类型失败,未知Trap类型,OID为{lmtVb.Value}！");
                return -1;
            }

            // TODO: 方便观察消息，生产环境时需去掉
            ShowLogHelper.Show($"Trap消息，TrapType:{intTrapType}", lmtPdu.m_SourceIp
                , InfoTypeEnum.ENB_OTHER_INFO_IMPORT);

            // 按不同类型处理Trap
            if (intTrapType==23) {
                string strMibName = "fileTransNotiFileType";
                string strFileTransType;
                if (!lmtPdu.GetValueByMibName(lmtPdu.m_SourceIp, strMibName, out strFileTransType)) {
                    Log.Error("PDU中获取文件类型节点{0}失败, GetValueByMibName函数返回失败\n", strMibName);
                    return -1;
                }
                int nTransType = Convert.ToInt16(strFileTransType);
                strMibName = "fileTransNotiResult";
                string strFileTransResult;
                if (!lmtPdu.GetValueByMibName(lmtPdu.m_SourceIp, strMibName, out strFileTransResult)) {
                    Log.Error("PDU中获取文件类型节点{0}失败, GetValueByMibName函数返回失败\n", strMibName);
                    return -1;
                }
                //0:success|成功/3:failure|失败
                int nTransResult;
                if ("0" == strFileTransResult)
                {
                    nTransResult = 0;
                }
                else {
                    nTransResult = -1;
                }
                if(!publicLogTransResult.ContainsKey(nTransType))
                publicLogTransResult.Add(nTransType, nTransResult);
                return 0;
            }

            return 0;
        }
        /// <summary>
		/// 判断PDU包的合法性
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strErrorMsg"></param>
		/// <returns></returns>
		public bool CheckPDUValidity(CDTLmtbPdu lmtPdu, out string strErrorMsg)
        {
            strErrorMsg = "";

            // 不能为空
            if (lmtPdu == null)
            {
                strErrorMsg = "接收到的包为空包";
                Log.Error("接收到的包为空包");
                return false;
            }

            // 判断包是否超时
            if (lmtPdu.reason == -5) // SNMP_CLASS_TIMEOUT
            {
                strErrorMsg = "响应超时";
                Log.Error("响应超时");
                return true;
            }

            // 判断包是否出错
            if (lmtPdu.m_LastErrorStatus != 0) // SNMP_ERROR_SUCCESS
            {
                strErrorMsg = $"ErrorStatus: {lmtPdu.m_LastErrorStatus} , ErrorIndex: {lmtPdu.m_LastErrorIndex} .";
                Log.Error($"接收到的包不正确--{strErrorMsg}");
                return true;
            }

            // 判断VB个数
            var vbCount = lmtPdu.VbCount();
            if (vbCount <= 0)
            {
                Log.Error($"接收到的包VB个数不正确:{vbCount}");
                return false;
            }

            //挨个检查VB的OID字段
            for (var i = 0; i < vbCount; i++)
            {
                var lmtVb = new CDTLmtbVb();
                lmtPdu.GetVbByIndex(i, ref lmtVb);
                if (string.IsNullOrEmpty(lmtVb.Oid))
                {
                    Log.Error($"接收到的包VB的OID写法为空,不正确:{i}");
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 查验Trap OID是否有效，有效的情况下，返回LMT-eNB自己定义的Trap类型
        /// </summary>
        /// <param name="strIpAddr">网元IP</param>
        /// <param name="strTrapOid">Trap类型Oid</param>
        /// <param name="strOidPrefix">Oid前缀</param>
        /// <param name="intTrapType">Trap类型对应的数值</param>
        /// <returns></returns>
        public bool CheckTrapOIDValidity(string strIpAddr, string strTrapOid, string strOidPrefix, out int intTrapType)
        {
            intTrapType = 0;

            if (string.IsNullOrEmpty(strTrapOid))
            {
                Log.Error("参数strTrapOid为空！");
                return false;
            }
            // 去掉Mib前缀
            var strSubTrapOid = strTrapOid.Replace(strOidPrefix, "");

            if (string.IsNullOrEmpty(strSubTrapOid))
            {
                Log.Error("Trap Oid 截取错误！");
                return false;
            }

            // 根据oid获取Mib节点信息
            var mibLeaf = SnmpMibUtil.GetMibNodeInfoByOID(strIpAddr, strSubTrapOid);
            if (null == mibLeaf)
            {
                Log.Error($"无法获取Mib节点信息，Oid:{strSubTrapOid}");
                return false;
            }
            // Mib名称
            var strMibName = mibLeaf.childNameMib;

            // 查询是否有该类型的Trap
            var nTrapId = Database.GetInstance().GetTrapInfoByMibName(strMibName);
            if (-1 == nTrapId)
            {
                Log.Error("数据库中没有找到相应的Trap类型信息!");
                return false;
            }

            intTrapType = nTrapId;

            return true;
        }

        public long UploadPublicLog(int downIndex,string targetPath,ref long taskId) {
            var enbIp = CSEnbHelper.GetCurEnbAddr();
            long reqId = 0;
           // long taskId = 0;
            var transFileObj = FileTransTaskMgr.FormatTransInfo(targetPath, "", (Transfiletype5216)downIndex, TRANSDIRECTION.TRANS_UPLOAD);
            transFileObj.IpAddr = enbIp;
            strEquipIp = enbIp;
            var result = FileTransTaskMgr.SendTransFileTask(enbIp, transFileObj, ref taskId, ref reqId);
            if (result != SENDFILETASKRES.TRANSFILE_TASK_SUCCEED) {
                Log.Error("下发文件传输任务失败, 目的路径:%s, 文件类型:%d, 网元IP:%s", targetPath, downIndex, enbIp);
                return -1;
            }
            return 1;
        }

        public bool GetUploadPublicLogProgress(long TaskId,int LogType, ref int nProgress) {
            Log.Debug(String.Format("需要获取的日志类型:{0}, TaskId:{1}", TaskId, LogType));
            if (publicLogTransResult.ContainsKey(LogType))
            {
                Log.Debug("该公共日志已经有结果");
                if (0 == publicLogTransResult[LogType])
                {
                    Log.Debug("该日志已经传输成功，返回进程100");
                    nProgress = 100;
                }
                else if (1 == publicLogTransResult[LogType])
                {

                    Log.Debug("该日志已经传输失败，返回进程-1");
                    nProgress = -1;
                }

            }
            else {
                Log.Debug("该日志还未有结果");
                string strCmdName;
                long lRequestId;
                CDTLmtbPdu pdu=new CDTLmtbPdu();
                string strTaskId= TaskId.ToString();
                string strIndex = strTaskId;
                strCmdName = "GetFileTransPercent";
                if ('.' != strIndex[0]) {
                    strIndex = "." + strIndex;
                }
                int cmdRet = CDTCmdExecuteMgr.GetInstance().CmdGetSync(strCmdName, out lRequestId, strIndex, strEquipIp, ref pdu);
                if (0 == cmdRet)
                {
                    string strMibName = "fileTransPercent";
                    string strPercent;
                    if (!pdu.GetValueByMibName(strEquipIp, strMibName, out strPercent))
                    {
                        Log.Error(string.Format("查询百分比字段失败, 网元IP:{0}, MibName:{1}", strEquipIp, strMibName));
                        return false;
                    }
                    nProgress = Convert.ToInt16(strPercent);
                    return true;
                }
                else {
                    Log.Error(string.Format("下发调试日志查询命令失败, 下发命令:{0}, 索引:{1}, 网元IP:{2}", strCmdName, strIndex, strEquipIp));
                    return false;
                }                

            }

            return true;
        }
    }
}
