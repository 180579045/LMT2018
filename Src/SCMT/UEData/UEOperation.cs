using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using CommonUtility;
using LmtbSnmp;
using MIBDataParser.JSONDataMgr;
using MIBDataParser;
using LinkPath;
using SCMTOperationCore.Elements;
using System.Net;
using SCMTOperationCore.Control;
using MsgQueue;
using System.Collections.ObjectModel;
using MsgDispatcher;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
namespace UEData
{
    public class UEOperation
    {
        public byte[] UEData { get; private set; }
        public UEOperation()
        {
            GlobalData.BtnCount = 0;
        }
        /// <summary>
        /// 查询UE信息
        /// </summary>
        public void QueryUeinfo()
        {
            if (GlobalData.m_isSupportDummyData)
            {
                GlobalData.m_ThisTimeisSupport = true;//需要支持显示虚拟数据时，单击显示虚拟数据，双击不显示
                Log.Info("单击显示虚拟数据");
            }
            else
            {
                GlobalData.m_ThisTimeisSupport = false;
            }
            MqInitial.Init();
            ConnectWorker.GetInstance();
            DoMsgDispatcher.GetInstance();
            //获取订阅UE反馈消息
            SubscribeHelper.AddSubscribe(TopicHelper.QueryUeinfoRsp, UeInfoData);
            GetIPOnceAgain();
            QueryData_BigCap();
            //QueryData_BigCapTest();

            /*236, 136, 143, 234, 145, 139, 0, 1, 2, 0, 1, 192, 8, 0, 69, 0, 1, 112, 248, 154, 64, 0, 64, 6, 253, 244, 172, 27, 245, 92, 172, 27, 245, 100, 19, 136, 208, 249, 41, 182, 45, 20, 108, 91, 12, 10, 80, 24, 0, 211, 95, 158, 0, 0, */
            //byte[] b = { 0, 8, 0, 1, 0, 3, 0, 7, 0, 2, 0, 2, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 17, 0, 1, 0, 17, 1, 1, 0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 17, 1, 1, 17, 1, 1, 1 , 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 17, 0, 1, 0, 17, 1, 1, 0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 17, 1, 1, 17, 1, 1, 1 };
            //OnQueryUeInfoRsp_BigCapTest(b);
        }
        /// <summary>
        /// UE测量配置查询
        /// </summary>
        /// <param name="nUeIndex">基站索引</param>
        public bool QueryMcfinfo(int nUeIndex)
        {
            try
            {
                MqInitial.Init();
                ConnectWorker.GetInstance();
                DoMsgDispatcher.GetInstance();
                //获取订阅UE反馈消息
                SubscribeHelper.AddSubscribe(TopicHelper.QueryUemeasRsp, UeMeanCfInfoData);
                GetIPOnceAgain();
                OMLMTA_LMTOM_SI_GetUeMeasInfoReqMsg Msg = new OMLMTA_LMTOM_SI_GetUeMeasInfoReqMsg();
                Msg.u16MsgLength = (ushort)Msg.Len;
                Msg.u16MsgType = GlobalData.O_LMTOM_SI_GET_UEMEASINFO_REQ;
                Msg.u16UeIndexCell = (ushort)nUeIndex;
                SI_STRU_LMTENBMsgHead Head = (SI_STRU_LMTENBMsgHead)Msg;
                byte[] ret = new byte[Head.ContentLen];
                Head.SerializeToBytes(ref ret, 0);
                if (!NodeBControl.SendSiMsg(GlobalData.strIpAddr, ret))
                {
                    Log.Error(string.Format("基站{0}发送查询测量配置的SI消息失败.", nUeIndex));
                    return false;
                }
                Log.Info(string.Format("基站{0}发送查询测量配置的SI消息成功.", nUeIndex));
            }
            catch (Exception)
            {
                Log.Error(string.Format("基站{0}测量配置查询失败.", nUeIndex));
                return false;
            }

            return true;
        }
        /// <summary>
        /// 查询UE业务信息
        /// </summary>
        /// <param name="csCellIndex">小区索引</param>
        /// <param name="csUEIndex">用户索引</param>
        /// <returns></returns>
        public bool QueryIpinfo(string csCellIndex, string csUEIndex)
        {
            MqInitial.Init();
            ConnectWorker.GetInstance();
            DoMsgDispatcher.GetInstance();
            //获取订阅UE反馈消息
            SubscribeHelper.AddSubscribe(TopicHelper.QueryUemeasRsp, UeIpInfoData);
            GetIPOnceAgain();
            OMLMTA_LMTOM_GetUeIpInfoReqMsgBigCap queryMsg = new UEData.OMLMTA_LMTOM_GetUeIpInfoReqMsgBigCap();
            queryMsg.u16CellId = Convert.ToUInt16(csCellIndex);
            queryMsg.u16MsgLength = queryMsg.ContentLen;
            queryMsg.u16MsgType = GlobalData.O_LMTOM_SI_GET_UEIPINFO_REQ;
            if ("" == csUEIndex)
            {
                queryMsg.u16UeIndex = 0xffff;
            }
            else
            {
                queryMsg.u16UeIndex = Convert.ToUInt16(csUEIndex);
            }
            Log.Info(string.Format("小区ID{0}，UEID{1}", csCellIndex, csUEIndex));
            SI_STRU_LMTENBMsgHead Head = (SI_STRU_LMTENBMsgHead)queryMsg;
            Byte[] bytes = new byte[Head.ContentLen];
            Head.SerializeToBytes(ref bytes, 0);
            if (!NodeBControl.SendSiMsg(GlobalData.strIpAddr, bytes))
            {
                Log.Error(string.Format("查询UEIP信息命令下发失败, SendSIMsg函数返回失败, IP地址:{0}\n", GlobalData.strIpAddr));
                return false;
            }
            else
            {
                Log.Info("命令下发成功");
            }
            return true;
        }
        /// <summary>
        /// 返回UE业务信息
        /// </summary>
        /// <param name="msg"></param>
        private void UeIpInfoData(SubscribeMsg msg)
        {
            if (msg.Data.Length == 0)
            {
                Log.Error("获取UE业务信息的byte数组失败");
                return;
            }
            else
            {
                OnQueryUeIPInfoRsp_BigCap(msg.Data);
            }
        }
        /// <summary>
        /// 返回UE信息字节组
        /// </summary>
        /// <param name="msg"></param>
        private void UeInfoData(SubscribeMsg msg)
        {
            if (msg.Data.Length == 0)
            {
                Log.Error("获取UE信息的byte数组失败");
                return;
            }
            else
            {
                OnQueryUeInfoRsp_BigCap(msg.Data);
            }
            UEData = msg.Data;

        }
        /// <summary>
        /// 返回UE测量信息字节组
        /// </summary>
        /// <param name="msg"></param>
        private void UeMeanCfInfoData(SubscribeMsg msg)
        {
            if (msg.Data.Length == 0)
            {
                Log.Error("获取UE测量配置信息的byte数组失败");
                return;
            }
            else
            {
                OnQueryUeMeasCfgRsp_BigCap(msg.Data);
            }
        }
        /// <summary>
        /// 连接多个基站时判断是大容量版本还是非大容量版
        /// </summary>
        private void GetIPOnceAgain()
        {
            //5G暂时不需判断是否为大容量
            GlobalData.strIpAddr = CSEnbHelper.GetCurEnbAddr();
            if (GlobalData.strIpAddr == null)
            {
                Log.Error("获取网元IP失败,请检查当前操作基站是否连接正常");
                throw new CustomException("无法获取网元IP,请检查当前操作基站是否连接正常");
            }
            else
            {
                Log.Info(string.Format("当前网元IP：{0}", GlobalData.strIpAddr));
            }
        }
        /// <summary>
        /// test
        /// </summary>
        public void QueryData_BigCapTest()
        {
            byte[] ret = { 0, 8, 0, 242, 0, 0 };
            GlobalData.strIpAddr = "192.168.5.167";
            //SubscribeHelper.AddSubscribe(TopicHelper.QueryUeinfoRsp, UeInfoData);
            if (!NodeBControl.SendSiMsg(GlobalData.strIpAddr, ret))
            {
                //Log.Error(string.Format("查询UE信息命令下发失败, SendSIMsg函数返回失败, IP地址:{0}\n", GlobalData.strIpAddr));
                /* L_ERROR("查询UE信息命令下发失败, SendSIMsg函数返回失败, IP地址:%s\n", m_strIpAddr);
                //        m_btnQuery.SetWindowText("查询命令下发失败");
                m_btnQuery.SetWindowText(LoadStringById(IDS_QUERY_COMMAND_FAILED));*/
                Console.Write("No");
            }
        }
        /// <summary>
        /// 查询数据(BigCap)
        /// </summary>
        public void QueryData_BigCap()
        {
            string Error = string.Empty;
            string strCellOperationalState;
            string strMibName = "nrcellentry";
            string strMibOid;
            string strpreOid = SnmpToDatabase.GetMibPrefix();
            GlobalData.BtnCount++;
            Database Db = Database.GetInstance();
            MibTable Mibt = new MibTable();
            MibLeaf Mib = new MibLeaf(Mibt);
            if (!Db.getDataByEnglishName(strMibName, out Mib, GlobalData.strIpAddr, out Error))
            {
                Log.Error(string.Format("查询节点{0}的OID失败", strMibName));
                return;
            }
            strMibOid = string.Format("{0}.{1}", strpreOid, Mib.childOid);
            string strMibIns = "";
            List<CDTLmtbVb> querVbs = new List<CDTLmtbVb>();
            querVbs.Clear();
            CDTLmtbVb CellStatus = new CDTLmtbVb();
            CellStatus.Oid = strMibOid + strMibIns;
            CellStatus.ParentOidLength = strMibOid.Length;
            querVbs.Add(CellStatus);
            Dictionary<string, string> results;
            do
            {
                /*获取索引 */
                if (!SnmpSessionWorker.SnmpGetNextSync(GlobalData.strIpAddr, querVbs, out results, 2000))
                {
                    break;
                }
                if (results.Count != 0)
                {
                    strMibIns = results.Keys.First();
                    strCellOperationalState = results.Values.First();
                    strMibIns.Replace(strMibOid, "");
                    string strCellId = strMibIns;
                    strCellId = strCellId.Trim('.');
                    if (strCellOperationalState == "0")
                    {
                        OMLMTA_LMTOM_GetUeInfoReqMsgBigCap queryMsg = new UEData.OMLMTA_LMTOM_GetUeInfoReqMsgBigCap();
                        queryMsg.u16MsgLength = queryMsg.ContentLen;
                        queryMsg.u16MsgType = GlobalData.O_LMTOM_SI_GET_UEINFO_REQ;
                        queryMsg.u8LocalCellId = (byte)Convert.ToInt32(strCellId);
                        queryMsg.u16RequestId = GlobalData.BtnCount;
                        SI_STRU_LMTENBMsgHead Head = (SI_STRU_LMTENBMsgHead)queryMsg;
                        byte[] ret = new byte[Head.ContentLen];
                        Head.SerializeToBytes(ref ret, 0);
                        if (!NodeBControl.SendSiMsg(GlobalData.strIpAddr, ret))
                        {
                            Log.Error(string.Format("查询UE信息命令下发失败, SendSIMsg函数返回失败, IP地址:{0}\n", GlobalData.strIpAddr));
                        }
                    }
                }
                querVbs.Clear();
                results.Clear();
                CellStatus.Oid = strMibOid + strMibIns;
                CellStatus.ParentOidLength = strMibOid.Length;
                querVbs.Add(CellStatus);
            } while (true);
        }
        private void OnQueryUeInfoRsp_BigCapTest(byte[] UEdata)
        {
            OMLMTA_OMLMT_SI_Nr_GetUeInfoRspMsg pMsg = new OMLMTA_OMLMT_SI_Nr_GetUeInfoRspMsg();
            pMsg.DeserializeToStruct(UEdata);
            //if (pMsg.u16RequestId != GlobalData.BtnCount)
            //{
            //    Log.Error("该消息体不是所需消息体");
            //    return;
            //}
            int nversion = pMsg.u8Version;
            int nendflag = pMsg.u8EndFlag;
            Log.Info(string.Format("nversion = {0} ,nendflag = {1}", nversion, nendflag));
            if (7 == nversion)
            {
                Log.Info(string.Format("pMsgHead->u8Version = {0}", nversion));
                if (pMsg.u8Result != 0)
                {
                    Log.Error("消息查询失败");
                    return;
                }
                Log.Info(string.Format("小区个数{0}", pMsg.u8CellNum));
                Log.Info(string.Format("基站内实际用户数量{0}", pMsg.u16RealNofUeInEnb));

                LoadData_BigCap(pMsg.struUeInfo, pMsg.u8CellNum, pMsg.u16RealNofUeInEnb);
            }
            else
            {
                Log.Error("UE信息查询版本错误,不能识别的结构体");
                return;
            }
        }

        /// <summary>
        /// 解析UE数据
        /// </summary>
        private void OnQueryUeInfoRsp_BigCap(byte[] UEdata)
        {
            OMLMTA_OMLMT_SI_GetUeInfoRspMsg_Head pMsgHead = new OMLMTA_OMLMT_SI_GetUeInfoRspMsg_Head();
            pMsgHead.DeserializeToStruct(UEdata);
            if (pMsgHead.u8Result != 0)
            {
                Log.Error("UE信息查询失败");
                return;
            }
            OMLMTA_OMLMT_SI_Nr_GetUeInfoRspMsg pMsg = new OMLMTA_OMLMT_SI_Nr_GetUeInfoRspMsg();
            pMsg.DeserializeToStruct(UEdata);
            if (pMsg.u16RequestId != GlobalData.BtnCount)
            {
                Log.Error("该消息体不是所需消息体");
                return;
            }
            int nversion = pMsg.u8Version;
            int nendflag = pMsg.u8EndFlag;
            Log.Info(string.Format("nversion = {0} ,nendflag = {1}", nversion, nendflag));
            if (7 == nversion)
            {
                Log.Info(string.Format("pMsgHead->u8Version = {0}", nversion));
                if (pMsg.u8Result != 0)
                {
                    Log.Error("消息查询失败");
                    return;
                }
                Log.Info(string.Format("小区个数{}", pMsg.u8CellNum));
                Log.Info(string.Format("基站内实际用户数量{0}", pMsg.u16RealNofUeInEnb));

                LoadData_BigCap(pMsg.struUeInfo, pMsg.u8CellNum, pMsg.u16RealNofUeInEnb);
            }
            else
            {
                Log.Error("UE信息查询版本错误,不能识别的结构体");
                return;
            }

        }
        /// <summary>
        /// 解析UE测量配置信息
        /// </summary>
        /// <param name="UEdata"></param>
        private void OnQueryUeMeasCfgRsp_BigCap(byte[] UEdata)
        {
            OMLMTA_OMLMT_GetUeMeasInfoRspMsg_Head head = new OMLMTA_OMLMT_GetUeMeasInfoRspMsg_Head();
            head.DeserializeToStruct(UEdata, 0);
            if (head.u8Result != 0)
            {
                Log.Error("UE测量配置查询失败.");
                throw new Exception("UE测量配置信息查询失败");
            }
            int iMsgLen = head.u16MsgLength;
            Log.Info(string.Format("接收到的消息长度为{0}", iMsgLen));
            int nversion = head.u8VerType;
            if (1 == nversion)
            {

                OMLMTA_OMLMT_SI_GetUeMeasInfoRspMsg1 RspMsg = new OMLMTA_OMLMT_SI_GetUeMeasInfoRspMsg1();
                if (iMsgLen > Marshal.SizeOf<OMLMTA_OMLMT_SI_GetUeMeasInfoRspMsg1>())
                {
                    Log.Error("返回的消息长度超过了结构体的长度.");
                    throw new Exception("UE测量配置信息查询失败");
                }
                RspMsg.DeserializeToStruct(UEdata, 0);
                if (RspMsg.u8Result != 0)
                {
                    Log.Error("UE测量配置查询失败.");
                    throw new Exception("UE测量配置信息查询失败");
                }
                LoadData_MeasCfBigCap(RspMsg.struMeasIdInfo, RspMsg.u8ValidNofId, RspMsg.u8VerIndicator);
            }
            else
            {
                Log.Error("不能识别的结构体，不能识别的结构体");
                throw new Exception("UE测量配置信息查询失败");
            }
        }
        private void OnQueryUeIPInfoRsp_BigCap(byte[] UEdata)
        {
            OMLMTA_OMLMT_SI_GetUeIPInfoRspMsgHead pMsgHead = new OMLMTA_OMLMT_SI_GetUeIPInfoRspMsgHead();
            pMsgHead.DeserializeToStruct(UEData, 0);
            Log.Info(string.Format("接收到的消息长度为{0}", pMsgHead.u16MsgLength));
            if (0 != pMsgHead.u8Result)
            {
                Log.Error("UE业务面信息查询执行失败");
                throw new Exception("UE业务面信息查询失败");
            }

            byte m_ResVersion = pMsgHead.u8Version;
            Log.Info(string.Format("查询成功,版本号{0}", m_ResVersion));
            if (0 == m_ResVersion)
            {
                OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg pMsg = new OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg();
                pMsg.DeserializeToStruct(UEData, 0);
                LoadData_BigCap(pMsg.struUeIpInfo);
            }
            else if (1 == m_ResVersion)
            {
                OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg_1 pMsg = new OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg_1();
                pMsg.DeserializeToStruct(UEData, 0);
                LoadData_BigCap1(pMsg.struUeIpInfo);
            }
            //增加不能识别结构体的判断保护 //DTMUC00264880 查询TDL用户业务信息LMT报错退出 wangxiaoying 2015.05.11
            else
            {
                Log.Error("不能识别的结构体");
                throw new Exception("不能是识别的结构体,查询失败.");
            }
        }

        private void LoadData_BigCap1(OM_STRU_SI_CellUeIpInformation_1 struUeIpInfo)
        {
            try
            {
                string strUIRruNo;
                string strUIRruPort;
                string strUIRruInfo;
                string strTemp;
                string strDIRruNo;
                string strDIRruPort;
                string strDIRruInfo;
                int nAmrNBNum = 0;
                int nAmrWBNum = 0;
                GlobalData.strUeIpCellInfo.Clear();
                GlobalData.strUeIpInfo.Clear();
                UeipInfo Ue;
                UeipCellInfo UeipCell = new UeipCellInfo();
                //bbu小区索引
                UeipCell.BbuCellIndex = struUeIpInfo.u8BbuCellIndex.ToString();
                //基站内小区索引
                UeipCell.CellIndexEnb = struUeIpInfo.u8CellIndexEnb.ToString();
                //小区所在的bpog板对应的槽位
                UeipCell.SlotId = struUeIpInfo.u8SlotId.ToString();
                //小区所在的DSP对应的L2模块对应的处理器号
                UeipCell.ProcId = struUeIpInfo.u8ProcId.ToString();
                //用户数量，最多400
                UeipCell.ValidNofUeInEnb = struUeIpInfo.u16ValidNofUeInEnb.ToString();
                //小区类型LTE = 0，NBIOT = 1
                GlobalData.CellType = struUeIpInfo.u8CellType;
                if (struUeIpInfo.u8CellType == 0)
                {
                    //小区上行激活sps的用户数
                    UeipCell.UlSpsActiveUeNum = struUeIpInfo.u16UlSpsActiveUeNum.ToString();
                    //小区下行激活sps的用户数
                    UeipCell.DlSpsActiveUeNum = struUeIpInfo.u16DlSpsActiveUeNum.ToString();
                }
                else if (struUeIpInfo.u8CellType == 1)
                {
                    //NBIOT小区时,不显示指示小区中上下行激活sps的用户数
                    //小区上行激活sps的用户数
                    UeipCell.UlSpsActiveUeNum = "/";
                    //小区下行激活sps的用户数
                    UeipCell.DlSpsActiveUeNum = "/";
                }

                #region 用户信息
                for (int iLoop = 0; iLoop < struUeIpInfo.u16ValidNofUeInEnb; iLoop++)
                {
                    Ue = new UEData.UeipInfo();
                    L2_SI_UeInfo_1 strTempUeInfo = struUeIpInfo.struUeInfo[iLoop];
                    //小区内UE索引
                    Ue.UeIndexCell = strTempUeInfo.u16UeIndexCell.ToString();
                    //用户IP地址
                    uint u32UEIP = strTempUeInfo.u32UeIpInfo;
                    if (u32UEIP == 0)
                    {
                        Ue.UeIpInfo = "无效";
                    }
                    else
                    {
                        uint u32IP1;
                        uint u32IP2;
                        uint u32IP3;
                        uint u32IP4;
                        u32IP1 = (u32UEIP & 0xff000000);
                        u32IP2 = (u32UEIP & 0x00ff0000) << 8;
                        u32IP3 = (u32UEIP & 0x0000ff00) << 16;
                        u32IP4 = (u32UEIP & 0x000000ff) << 24;
                        Ue.UeIpInfo = string.Format($"{u32IP1}.{u32IP2}.{u32IP3}.{u32IP1}");
                    }
                    if (struUeIpInfo.u8CellType == 0)
                    {
                        //HL配置的UE位置信息
                        Ue.HlMacUeLocation = TranslateMibValue(GlobalData.m_csUeLoValueList, strTempUeInfo.u8HlMacUeLocation.ToString());
                        //MAC计算的UE位置信息
                        Ue.MacUeLocation = TranslateMibValue(GlobalData.m_csMacTAValueList, strTempUeInfo.u8MacUeLocation.ToString());
                    }
                    else if (struUeIpInfo.u8CellType == 1)
                    {
                        //HL配置的UE位置信息
                        Ue.HlMacUeLocation = "/";
                        //MAC计算的UE位置信息
                        Ue.MacUeLocation = "/";
                    }

                    //TA信息
                    Ue.MacTA = TranslateMibValue(GlobalData.m_csMacTAValueList, strTempUeInfo.u8MacTA.ToString());
                    if (struUeIpInfo.u8CellType == 0)
                    {
                        //MAC下行传输模式
                        Ue.MacTmMode = "TM" + strTempUeInfo.u8MacTmMode;
                    }
                    else if (struUeIpInfo.u8CellType == 1)
                    {
                        //MAC下行传输模式
                        Ue.MacTmMode = "/";
                    }

                    //UE等级能力
                    Ue.UeCapability = strTempUeInfo.u8UeCapability.ToString();
                    if (struUeIpInfo.u8CellType == 0)
                    {
                        if (strTempUeInfo.u8FlowType == 1)
                        {
                            nAmrNBNum++;
                        }
                        if (strTempUeInfo.u8FlowType == 2)
                        {
                            nAmrWBNum++;
                        }
                        Ue.FlowType = TranslateMibValue(GlobalData.m_FlowTypeValueList, strTempUeInfo.u8FlowType.ToString());
                        //指示用户是否是上行sps激活用户
                        Ue.UlSpsActiveFlag = TranslateMibValue(GlobalData.m_spsactiveflagValueList, strTempUeInfo.u8UlSpsActiveFlag.ToString());
                        //指示用户是否是下行sps激活用户
                        Ue.DlSpsActiveFlag = TranslateMibValue(GlobalData.m_spsactiveflagValueList, strTempUeInfo.u8DlSpsActiveFlag.ToString());
                        //载波聚合小区状态
                        Ue.CaActiveFlag = TranslateMibValue(GlobalData.m_caactiveflagValueList, strTempUeInfo.u8CaActiveFlag.ToString());
                        //辅小区eNB索引
                        Ue.ScellCellIndexEnb = strTempUeInfo.u16ScellUeIndex.ToString();
                        //辅小区用户索引
                        Ue.ScellUeIndex = strTempUeInfo.u16ScellUeIndex.ToString();
                        //ue上行所占用的通道数
                        strUIRruNo = "";
                        strUIRruPort = "";
                        strUIRruInfo = "";
                        strTemp = "";
                        for (int nRruInfoIndex = 0; nRruInfoIndex < strTempUeInfo.u8UIPathNum; nRruInfoIndex++)
                        {
                            //UE归属的RRU编号
                            Ue_RruInfo strTempRruinfo = strTempUeInfo.struUIRruInfo[nRruInfoIndex];
                            strTemp = "{" + strTempRruinfo.u8RruNo + ",";
                            strUIRruNo = strTemp;

                            //UE归属的RRU通道号
                            strTemp = strTempRruinfo.u8RruPort + "}";
                            strUIRruPort = strTemp;
                            if (nRruInfoIndex != strTempUeInfo.u8UIPathNum)
                            {
                                strUIRruInfo += strUIRruNo;
                                strUIRruInfo += strUIRruPort;
                                strUIRruInfo += "\r\n";
                            }
                        }
                        Ue.UIRruInfo = strUIRruInfo;
                        //UE下行所占的通道数
                        strDIRruInfo = "";
                        strDIRruNo = "";
                        strDIRruPort = "";
                        for (int nRruInfoIndex = 0; nRruInfoIndex < strTempUeInfo.u8DIPathNum; nRruInfoIndex++)
                        {
                            //UE归属的RRU编号
                            Ue_RruInfo strTempRruinfo = strTempUeInfo.struDIRruInfo[nRruInfoIndex];
                            strTemp = "{" + strTempRruinfo.u8RruNo + ",";
                            strDIRruNo = strTemp;

                            //UE归属的RRU通道号
                            strTemp = strTempRruinfo.u8RruPort + "}";
                            strDIRruPort = strTemp;
                            if (nRruInfoIndex != strTempUeInfo.u8DIPathNum)
                            {
                                strDIRruInfo += strDIRruNo;
                                strDIRruInfo += strDIRruPort;
                                strDIRruInfo += "\r\n";
                            }
                        }
                        Ue.DIRruInfo = strDIRruInfo;
                        Ue.UeUlMcl = "/";
                        Ue.UeDlMcl = "/";
                        Ue.UlSinr = "/";
                        Ue.DlSinr = "/";
                    }
                    else if (struUeIpInfo.u8CellType == 1)
                    {
                        Ue.FlowType = "/";
                        Ue.UlSpsActiveFlag = "/";
                        Ue.DlSpsActiveFlag = "/";
                        Ue.CaActiveFlag = "/";
                        Ue.ScellCellIndexEnb = "/";
                        Ue.ScellUeIndex = "/";
                        Ue.UIRruInfo = "/";
                        Ue.DIRruInfo = "/";
                        Ue.UeUlMcl = strTempUeInfo.s16UeUlMcl.ToString();
                        Ue.UeDlMcl = strTempUeInfo.s16UeDlMcl.ToString();
                        Ue.UlSinr = strTempUeInfo.s16UlSinr.ToString();
                        Ue.DlSinr = strTempUeInfo.s16DlSinr.ToString();
                    }
                    GlobalData.strUeIpInfo.Add(Ue);
                }
                #endregion
                if (struUeIpInfo.u8CellType == 0)
                {
                    //AMR-NB
                    UeipCell.nAmrNBNum = nAmrNBNum.ToString();
                    //AMR-WB
                    UeipCell.AmrWBNum = nAmrWBNum.ToString();
                }
                else if (struUeIpInfo.u8CellType == 1)
                {
                    //AMR-NB
                    UeipCell.nAmrNBNum = "/";
                    //AMR-WB
                    UeipCell.AmrWBNum = "/";
                }

                GlobalData.strUeIpCellInfo.Add(UeipCell);
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                throw new Exception("数据解析出错！");
            }
        }

        private void LoadData_BigCap(OM_STRU_CellUeIpInformation_4 struUeIpInfo)
        {
            try
            {
                string strUIRruNo;
                string strUIRruPort;
                string strUIRruInfo;
                string strTemp;
                string strDIRruNo;
                string strDIRruPort;
                string strDIRruInfo;
                int nAmrNBNum = 0;
                int nAmrWBNum = 0;
                GlobalData.strUeIpCellInfo.Clear();
                GlobalData.strUeIpInfo.Clear();
                UeipInfo Ue;
                UeipCellInfo UeipCell = new UeipCellInfo();
                //bbu小区索引
                UeipCell.BbuCellIndex = struUeIpInfo.u8BbuCellIndex.ToString();
                //基站内小区索引
                UeipCell.CellIndexEnb = struUeIpInfo.u8CellIndexEnb.ToString();
                //小区所在的bpog板对应的槽位
                UeipCell.SlotId = struUeIpInfo.u8SlotId.ToString();
                //小区所在的DSP对应的L2模块对应的处理器号
                UeipCell.ProcId = struUeIpInfo.u8ProcId.ToString();
                //用户数量，最多400
                UeipCell.DlSpsActiveUeNum = struUeIpInfo.u16ValidNofUeInEnb.ToString();
                //小区上行激活sps的用户数
                UeipCell.UlSpsActiveUeNum = struUeIpInfo.u16UlSpsActiveUeNum.ToString();
                //小区下行激活sps的用户数
                UeipCell.DlSpsActiveUeNum = struUeIpInfo.u16DlSpsActiveUeNum.ToString();
                #region 用户信息
                for (int iLoop = 0; iLoop < struUeIpInfo.u16ValidNofUeInEnb; iLoop++)
                {
                    Ue = new UEData.UeipInfo();
                    L2_UeInfo4 strTempUeInfo = struUeIpInfo.struUeInfo[iLoop];
                    //小区内UE索引
                    Ue.UeIndexCell = strTempUeInfo.u16UeIndexCell.ToString();
                    //用户IP地址
                    uint u32UEIP = strTempUeInfo.u32UeIpInfo;
                    if (u32UEIP == 0)
                    {
                        Ue.UeIpInfo = "无效";
                    }
                    else
                    {
                        uint u32IP1;
                        uint u32IP2;
                        uint u32IP3;
                        uint u32IP4;
                        u32IP1 = (u32UEIP & 0xff000000);
                        u32IP2 = (u32UEIP & 0x00ff0000) << 8;
                        u32IP3 = (u32UEIP & 0x0000ff00) << 16;
                        u32IP4 = (u32UEIP & 0x000000ff) << 24;
                        Ue.UeIpInfo = string.Format($"{u32IP1}.{u32IP2}.{u32IP3}.{u32IP1}");
                    }
                    //HL配置的UE位置信息
                    Ue.HlMacUeLocation = TranslateMibValue(GlobalData.m_csUeLoValueList, strTempUeInfo.u8HlMacUeLocation.ToString());
                    //MAC计算的UE位置信息
                    Ue.MacUeLocation = TranslateMibValue(GlobalData.m_csMacTAValueList, strTempUeInfo.u8MacUeLocation.ToString());
                    //TA信息
                    Ue.MacTA = TranslateMibValue(GlobalData.m_csMacTAValueList, strTempUeInfo.u8MacTA.ToString());
                    //MAC下行传输模式
                    Ue.MacTmMode = "TM" + strTempUeInfo.u8MacTmMode;
                    //UE等级能力
                    Ue.UeCapability = strTempUeInfo.u8UeCapability.ToString();
                    if (strTempUeInfo.u8FlowType == 1)
                    {
                        nAmrNBNum++;
                    }
                    if (strTempUeInfo.u8FlowType == 2)
                    {
                        nAmrWBNum++;
                    }
                    Ue.FlowType = TranslateMibValue(GlobalData.m_FlowTypeValueList, strTempUeInfo.u8FlowType.ToString());
                    //指示用户是否是上行sps激活用户
                    Ue.UlSpsActiveFlag = TranslateMibValue(GlobalData.m_spsactiveflagValueList, strTempUeInfo.u8UlSpsActiveFlag.ToString());
                    //指示用户是否是下行sps激活用户
                    Ue.DlSpsActiveFlag = TranslateMibValue(GlobalData.m_spsactiveflagValueList, strTempUeInfo.u8DlSpsActiveFlag.ToString());
                    //载波聚合小区状态
                    Ue.CaActiveFlag = TranslateMibValue(GlobalData.m_caactiveflagValueList, strTempUeInfo.u8CaActiveFlag.ToString());
                    //辅小区eNB索引
                    Ue.ScellCellIndexEnb = strTempUeInfo.u16ScellUeIndex.ToString();
                    //辅小区用户索引
                    Ue.ScellUeIndex = strTempUeInfo.u16ScellUeIndex.ToString();
                    //ue上行所占用的通道数
                    strUIRruNo = "";
                    strUIRruPort = "";
                    strUIRruInfo = "";
                    strTemp = "";
                    for (int nRruInfoIndex = 0; nRruInfoIndex < strTempUeInfo.u8UIPathNum; nRruInfoIndex++)
                    {
                        //UE归属的RRU编号
                        Ue_RruInfo strTempRruinfo = strTempUeInfo.struUIRruInfo[nRruInfoIndex];
                        strTemp = "{" + strTempRruinfo.u8RruNo + ",";
                        strUIRruNo = strTemp;

                        //UE归属的RRU通道号
                        strTemp = strTempRruinfo.u8RruPort + "}";
                        strUIRruPort = strTemp;
                        if (nRruInfoIndex != strTempUeInfo.u8UIPathNum)
                        {
                            strUIRruInfo += strUIRruNo;
                            strUIRruInfo += strUIRruPort;
                            strUIRruInfo += "\r\n";
                        }
                    }
                    Ue.UIRruInfo = strUIRruInfo;
                    //UE下行所占的通道数
                    strDIRruInfo = "";
                    strDIRruNo = "";
                    strDIRruPort = "";
                    for (int nRruInfoIndex = 0; nRruInfoIndex < strTempUeInfo.u8DIPathNum; nRruInfoIndex++)
                    {
                        //UE归属的RRU编号
                        Ue_RruInfo strTempRruinfo = strTempUeInfo.struDIRruInfo[nRruInfoIndex];
                        strTemp = "{" + strTempRruinfo.u8RruNo + ",";
                        strDIRruNo = strTemp;

                        //UE归属的RRU通道号
                        strTemp = strTempRruinfo.u8RruPort + "}";
                        strDIRruPort = strTemp;
                        if (nRruInfoIndex != strTempUeInfo.u8DIPathNum)
                        {
                            strDIRruInfo += strDIRruNo;
                            strDIRruInfo += strDIRruPort;
                            strDIRruInfo += "\r\n";
                        }
                    }
                    Ue.DIRruInfo = strDIRruInfo;
                    GlobalData.strUeIpInfo.Add(Ue);
                }
                #endregion
                //AMR-NB
                UeipCell.nAmrNBNum = nAmrNBNum.ToString();
                //AMR-WB
                UeipCell.AmrWBNum = nAmrWBNum.ToString();
                GlobalData.strUeIpCellInfo.Add(UeipCell);
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                throw new Exception("数据解析出错！");
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="struUeInfo"></param>
        /// <param name="u8CellNum"></param>
        /// <param name="u16RealNofUeInEnb"></param>
        private void LoadData_BigCap(OM_STRU_SI_NrCellUeInformation[] struUeInfo, byte u8CellNum, ushort u16RealNofUeInEnb)
        {
            int UeNum = 0;
            CellUeInformation Ueinfo;
            for (int i = 0; i < u8CellNum; i++)
            {
                Ueinfo = new CellUeInformation();
                //小区名
                Ueinfo.PCellLocalCellId = struUeInfo[i].u8PCellLocalCellId;
                //BUU内小区索引
                Ueinfo.CellIndexBBU = struUeInfo[i].s32CellIndexBBU;
                //BUU板卡槽位号
                Ueinfo.CellSlotNo = struUeInfo[i].u32CellSlotNo;
                //小区的物理地址
                Ueinfo.PhyId = struUeInfo[i].u16PhyId;
                //本小区的UE个数
                Ueinfo.UeNum = struUeInfo[i].u16UeNum;
                //添加到全局动态数组
                GlobalData.CellUeInfo.Add(Ueinfo);
                //Ueinfo.uRrcOmUeInfo = struUeInfo[i].struRrcOmUeInfo;

            }
            //获取小区UE信息
            UeInformation Ue;
            for (int iLoop = 0; iLoop < u8CellNum; iLoop++)
            {
                UeNum = struUeInfo[iLoop].u16UeNum;
                OM_SI_Nr_UeInformation[] strUeInfo = struUeInfo[iLoop].struRrcOmUeInfo;
                for (int jLoop = 0; jLoop < UeNum; jLoop++)
                {
                    Ue = new UeInformation();
                    //主服务小区本地小区ID
                    Ue.SpcellLocalCellId = strUeInfo[jLoop].u8SpcellLocalCellId.ToString();
                    //辅小区本地ID,不存在时,填写无效值Invalid_u8(255)*
                    if (strUeInfo[jLoop].u8ScellLocalCellId == 255)
                    {
                        Ue.ScellLocalCellId = "不存在";
                    }
                    else
                    {
                        Ue.ScellLocalCellId = strUeInfo[jLoop].u8ScellLocalCellId.ToString();
                    }
                    //用户Crnti,不存在时,填写无效值Invalid_u16(65535)
                    if (65535 == strUeInfo[jLoop].u16Crint)
                    {
                        Ue.u16Crint = "不存在";
                    }
                    else
                    {
                        Ue.u16Crint = strUeInfo[jLoop].u16Crint.ToString();
                    }
                    //用户小区内索引
                    Ue.u16UeIndexCell = strUeInfo[jLoop].u16UeIndexCell.ToString();
                    //用户基站内索引
                    Ue.u32UeIndexGnb = strUeInfo[jLoop].u32UeIndexGnb.ToString();
                    //用户Ran侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)
                    if (4294967295 == strUeInfo[jLoop].u32RanNgapId)
                    {
                        Ue.u32RanNgapId = "不存在";
                    }
                    else
                    {
                        Ue.u32RanNgapId = strUeInfo[jLoop].u32RanNgapId.ToString();
                    }

                    //用户Amf侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)
                    if (4294967295 == strUeInfo[jLoop].u32AmfNgapId)
                    {
                        Ue.u32AmfNgapId = "不存在";
                    }
                    else
                    {
                        Ue.u32AmfNgapId = strUeInfo[jLoop].u32AmfNgapId.ToString();
                    }
                    //该用户拥有的DRB个数,不存在时,填写0
                    Ue.u8ValidNofDrb = strUeInfo[jLoop].u8ValidNofDrb.ToString();
                    //QosFlow映射的DRB信息,需要分级显示,当u8ValidNofDrb为0时,不进行显示
                    Ue.struDrbInfo = ToTree(strUeInfo[jLoop].struDrbInfo, strUeInfo[jLoop].u8ValidNofDrb);
                    //用户级AMBR信息-聚合下行最大速率，单位bps
                    Ue.u64AmbrDownlink = strUeInfo[jLoop].struUeAmbr.u64AmbrDownlink.ToString();
                    //用户级AMBR信息-聚合上行最大速率，单位bps
                    Ue.u64AmbrUplink = strUeInfo[jLoop].struUeAmbr.u64AmbrUplink.ToString();
                    //该用户拥有的Pdusession个数,不存在时,填写0
                    Ue.u8ValidNofPdusession = strUeInfo[jLoop].u8ValidNofPdusession.ToString();
                    //*每个Pdusession信息,需要分级显示,当u8ValidNofPdusession为0时,不进行显示*/
                    Ue.struPdusessionInfo = ToTree(strUeInfo[jLoop].struPdusessionInfo, strUeInfo[jLoop].u8ValidNofPdusession);
                    //AMF Region ID
                    Ue.u16AmfSetId = strUeInfo[jLoop].struGumai.u16AmfSetId.ToString();
                    //AMF Pointer
                    Ue.u8AmfPoniter = strUeInfo[jLoop].struGumai.u8AmfPoniter.ToString();
                    //AMF Set ID
                    Ue.u8AmfRegionId = strUeInfo[jLoop].struGumai.u8AmfRegionId.ToString();
                    //plmn移动国家码
                    Ue.u8Mcc = ToTree(strUeInfo[jLoop].struGumai.struPlmnId.u8Mcc, "MCC");
                    //Plmn移动网络码
                    Ue.u8Mnc = ToTree(strUeInfo[jLoop].struGumai.struPlmnId.u8Mnc, "MNC");
                    GlobalData.strUeInfo.Add(Ue);
                }
            }
        }
        private void LoadData_MeasCfBigCap(LMTOM_UeMeasIdInfo1[] UeMeasIdInfo, int iLoop, byte VerIndicator)
        {
            XmlDocument XML = new XmlDocument();
            XmlNode node;
            XmlNode Childnode;
            string path = "";
            path = System.AppDomain.CurrentDomain.BaseDirectory + "//Component//Configration//Resource.xml";
            XML.Load(path);
            XmlElement ele = XML.DocumentElement;
            if (VerIndicator == 1)
            {
                node = ele["MapValueToDescII"];
            }
            else
            {
                node = ele["MapValueToDescI"];
            }

            Log.Info(string.Format("ValidNofId = {0}", iLoop));
            UeMeasCfInfo Measinfo;
            for (int i = 0; i < iLoop; i++)
            {
                Measinfo = new UeMeasCfInfo();
                Measinfo.MeasId = UeMeasIdInfo[i].u8MeasId.ToString();
                Measinfo.MeasObjectId = UeMeasIdInfo[i].u8MeasObjectId.ToString();
                Childnode = node["MeasChoice"];
                Measinfo.MeasObjectChoice = SelectXML(Childnode, UeMeasIdInfo[i].u32MeasObjectChoice);
                Measinfo.CarrierFreq = UeMeasIdInfo[i].u32CarrierFreq.ToString();
                Measinfo.ReportConfigId = UeMeasIdInfo[i].u8ReportConfigId.ToString();
                Measinfo.ReportCfgChoice = SelectXML(Childnode, UeMeasIdInfo[i].u32ReportCfgChoice);
                Childnode = node["ReportConfig"];
                Measinfo.ReportConfig = SelectXML(Childnode, UeMeasIdInfo[i].u32ReportConfig);
                Childnode = node["MeasPurpose"];
                Measinfo.MeasPurpose = SelectXML(Childnode, UeMeasIdInfo[i].u32MeasPurpose);
                Childnode = node["MeasAlgoType"];
                Measinfo.AlgorithmType = SelectXML(Childnode, UeMeasIdInfo[i].u32AlgorithmType);
                //添加一组数据到数据组中
                GlobalData.strUeMeasInfo.Add(Measinfo);
            }
        }
        /// <summary>
        /// 生成RRCOM_DrbInfo树结构的数据
        /// </summary>
        /// <param name="DrbInfo">RRCOM_DrbInfo结构体数组</param>
        /// <param name="iLoop">数组大小</param>
        /// <returns></returns>
        private ObservableCollection<ChildrenUeInfo> ToTree(RRCOM_DrbInfo[] DrbInfo, byte iLoop)
        {
            ObservableCollection<ChildrenUeInfo> Collect = new ObservableCollection<ChildrenUeInfo>();
            if (0 == iLoop)
            {
                ChildrenUeInfo C = new UEData.ChildrenUeInfo("-");
                Collect.Add(C);
            }
            else
            {

                ChildrenUeInfo C = new UEData.ChildrenUeInfo("DrbInfo");
                for (int i = 0; i < iLoop; i++)
                {
                    C.Children.Add(new ChildrenUeInfo(string.Format("DrbInfo{0}:", i + 1)));
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("DrbId: {0}", DrbInfo[i].u8DrbId)));
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("LoChId: {0}", DrbInfo[i].u8LoChId)));
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("ModeFlag: {0}", DrbInfo[i].u8ModeFlag)));
                }
                Collect.Add(C);
            }
            return Collect;
        }
        /// <summary>
        /// 生成RRCOM_PduSessionInfo树结构的数据
        /// </summary>
        /// <param name="DrbInfo">RRCOM_PduSessionInfo结构体数组</param>
        /// <param name="iLoop">数组大小</param>
        /// <returns></returns>
        private ObservableCollection<ChildrenUeInfo> ToTree(RRCOM_PduSessionInfo[] PduInfo, byte iLoop)
        {
            ObservableCollection<ChildrenUeInfo> Collect = new ObservableCollection<ChildrenUeInfo>();
            if (0 == iLoop)
            {
                ChildrenUeInfo C = new ChildrenUeInfo("-");
                Collect.Add(C);
            }
            else
            {
                ChildrenUeInfo C = new ChildrenUeInfo("Pdu-SessionInfo");
                string[] name = { "SecurityIndFlag", "PdusessionId", "AmbrInfo", "RRCOM_S_NSSAI", "SecurityInd", "QosFlowInfo" };
                for (int i = 0; i < iLoop; i++)
                {
                    C.Children.Add(new ChildrenUeInfo(string.Format("Pdu-SessionInfo{0}:", i + 1)));
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("SecurityIndFlag: {0}", PduInfo[i].u8SecurityIndFlag)));
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("PdusessionId: {0}", PduInfo[i].u8PdusessionId)));
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("AmbrInfo:")));
                    C.Children[i].Children[2].Children.Add(new ChildrenUeInfo(string.Format("AmbrDownlink: {0}", PduInfo[i].struPduAmbr.u64AmbrDownlink)));
                    C.Children[i].Children[2].Children.Add(new ChildrenUeInfo(string.Format("AmbrUplink: {0}", PduInfo[i].struPduAmbr.u64AmbrUplink)));
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("RRCOM_S_NSSAI:")));
                    C.Children[i].Children[3].Children.Add(new ChildrenUeInfo(string.Format("Sst: {0}", PduInfo[i].struSnssai.u8Sst)));
                    C.Children[i].Children[3].Children.Add(new ChildrenUeInfo(string.Format("Sd: {0}", PduInfo[i].struSnssai.u32Sd)));
                    if (PduInfo[i].u8SecurityIndFlag != 0)
                    {
                        C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("SecurityInd:")));
                        C.Children[i].Children[4].Children.Add(new ChildrenUeInfo(string.Format("IntProtectInd: {0}", PduInfo[i].struSecurityInd.u8IntProtectInd)));
                        C.Children[i].Children[4].Children.Add(new ChildrenUeInfo(string.Format("EncProtectInd: {0}", PduInfo[i].struSecurityInd.u8EncProtectInd)));
                    }
                    C.Children[i].Children.Add(new ChildrenUeInfo(string.Format("QosFlowInfo:")));
                    for (int k = 0; k < PduInfo[i].u8ValidNofQosFlow; k++)
                    {
                        C.Children[i].Children[5].Children.Add(new ChildrenUeInfo(string.Format("QosFlowInfo{0}", k + 1)));
                        C.Children[i].Children[5].Children[k].Children.Add(new ChildrenUeInfo(string.Format("Qfi: {0}", PduInfo[i].struQosFlowInfo[k].u16Qfi)));
                        C.Children[i].Children[5].Children[k].Children.Add(new ChildrenUeInfo(string.Format("DrbId: {0}", PduInfo[i].struQosFlowInfo[k].u8DrbId)));
                        C.Children[i].Children[5].Children[k].Children.Add(new ChildrenUeInfo(string.Format("GbrDownlink: {0}", PduInfo[i].struQosFlowInfo[k].struGbrQosFlowInfo.u64GbrDownlink)));
                        C.Children[i].Children[5].Children[k].Children.Add(new ChildrenUeInfo(string.Format("GbrUplink: {0}", PduInfo[i].struQosFlowInfo[k].struGbrQosFlowInfo.u64GbrUplink)));
                        C.Children[i].Children[5].Children[k].Children.Add(new ChildrenUeInfo(string.Format("MbrDownlink: {0}", PduInfo[i].struQosFlowInfo[k].struGbrQosFlowInfo.u64MbrDownlink)));
                        C.Children[i].Children[5].Children[k].Children.Add(new ChildrenUeInfo(string.Format("MbrUplink: {0}", PduInfo[i].struQosFlowInfo[k].struGbrQosFlowInfo.u64MbrUplink)));
                    }
                }
                Collect.Add(C);
            }
            return Collect;
        }
        private ObservableCollection<ChildrenUeInfo> ToTree(byte[] bt, string name)
        {
            ObservableCollection<ChildrenUeInfo> Collect = new ObservableCollection<UEData.ChildrenUeInfo>();
            ChildrenUeInfo C = new UEData.ChildrenUeInfo(name);
            for (int i = 0; i < 3; i++)
            {
                C.Children.Add(new ChildrenUeInfo(string.Format(name + ":{0}", bt[i])));
            }
            Collect.Add(C);
            return Collect;
        }
        /// <summary>
        /// 获取UE测量配置资源信息
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string SelectXML(XmlNode Node, uint id)
        {
            string xml = Node.SelectSingleNode("./item[@ID='" + id + "']").InnerXml;
            return xml;
        }
        private string TranslateMibValue(string strValueList, string Id)
        {
            string value = "";
            string[] strList = strValueList.Split('/');
            if (Id != "")
            {
                foreach (var item in strList)
                {
                    if (item.Contains(Id))
                    {
                        value = item.Split(':')[1];
                        break;
                    }
                }
                if (value == "")
                    value = "未查到描述";
            }
            else
            {
                value = "未查到描述";
            }
            return value;
        }
    }
}
