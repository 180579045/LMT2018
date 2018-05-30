/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：SnmpHelper.cs
// 文件功能描述：Snmp报文类;
// 创建人：;
// 版本：V1.0
// 创建标识：创建文件;
// 时间：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using SnmpSharpNet;
using System.Threading.Tasks;
using LogManager;

namespace SCMTOperationCore.Message.SNMP
{
    /// <summary>
    /// 抽象SNMP报文，以便后续扩展SNMPV3;
    /// </summary>
    public abstract class SnmpHelper
    {
        public Dictionary<string, string> m_Response { get; set; }        // Get返回的结果;
        public string m_DestIPAddr { get; set; }                              // 代理目标IP地址
        public long m_DestPort { get { return 161; } set { m_DestPort = value; } }  //目的端口
        public string m_Community { get; set; }                           // 代理目标的Community
        public long m_TrapPort { get { return 162; } set { m_TrapPort = value; } }  //管理端Trap监听端口
        public long m_TimeOut { get { return 1000; } set { m_TimeOut = value; } }   //设置的超时时间(10s超时，以10ms为单位)
        public string m_ErrorStatus { get; set; }                         // 错误码;
        protected SnmpV2Packet m_Result { get; set; }                     // 返回结果;
        public SnmpVersion m_Version { get { return SnmpVersion.Ver2; } set { m_Version = value; } } // SNMP版本,当前基站使用SNMP固定为Ver.2;
        public List<string> PduList { get; set; }                         // Snmp报文的Pdu列表

        public UdpTarget m_target { get; set; }

        public AgentParameters m_Param { get; set; }

        public SnmpHelper(string commnuity, string destIpAddr)
        {
            this.m_DestIPAddr = destIpAddr;
            this.m_Community = commnuity;

            ConnectToAgent(m_Community, m_DestIPAddr);
        }

        public SnmpHelper(string commnuity, string destIpAddr, long destPort, long trapPort
            , SnmpVersion version, long timeOut)
        {
            this.m_Community = commnuity;
            this.m_DestIPAddr = destIpAddr;
            this.m_DestPort = destPort;
            this.m_TrapPort = trapPort;
            this.m_Version = version;
            this.m_TimeOut = timeOut;

            ConnectToAgent(m_Community, m_DestIPAddr);
        }

   

        /// <summary>
        /// GetRequest的对外接口
        /// </summary>
        /// <param name="PduList">需要查询的Pdu列表</param>
        /// <param name="Community">需要设置的Community</param>
        /// <param name="IpAddress">需要设置的IP地址</param>
        /// <returns></returns>
        public abstract Dictionary<string, string> GetRequest(List<string> PduList, string Community, string IpAddress);

        /// <summary>
        /// GetRequest的对外接口，客户端通过异步回调获取数据;
        /// </summary>
        /// <param name="callback">异步回调方法</param>
        /// <param name="PduList">需要查询的Pdu列表</param>
        /// <param name="Community">需要设置的Community</param>
        /// <param name="IpAddress">需要设置的IP地址</param>
        public abstract void GetRequest(AsyncCallback callback, List<string>PduList, string Community, string IpAddress);

        /// <summary>
        /// GetRequest的对外接口，入参只有Pdulist
        /// </summary>
        /// <param name="PduList"></param>
        /// <returns></returns>
        public abstract SnmpV2Packet GetRequest(List<string> PduList);

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="pdu"></param>
        /// <returns></returns>
        public abstract SnmpV2Packet GetRequest(Pdu pdu);

        public abstract bool GetRequestAsync(Pdu pdu, SnmpAsyncResponse callback);

        /// <summary>
        /// SetRequest的对外接口
        /// </summary>
        /// <param name="PduList">需要查询的Pdu列表</param>
        /// <param name="Community">需要设置的Community</param>
        /// <param name="IpAddress">需要设置的IP地址</param>
        public abstract void SetRequest(Dictionary<string, string> PduList, string Community, string IpAddress);

        /// <summary>
        /// Set请求
        /// </summary>
        /// <param name="pdu"></param>
        /// <returns></returns>
        public abstract SnmpV2Packet SetRequest(Pdu pdu);

        /// <summary>
        /// SetRequest的对外接口;
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="PduList"></param>
        public abstract void SetRequest(AsyncCallback callback, List<string>PduList);

        /// <summary>
        /// GetRequest的对外接口;
        /// </summary>
        /// <param name="callback">获得结果后的异步回调</param>
        /// <param name="PduList">最开始的入参</param>
        public abstract void GetNextRequest(AsyncCallback callback, List<string> PduList);

        /// <summary>
        /// 连接代理;
        /// </summary>
        /// <param name="Community"></param>
        /// <param name="IpAddr"></param>
        /// <returns></returns>
        protected UdpTarget ConnectToAgent(string Community, string IpAddr)
        {
            OctetString community = new OctetString(Community);
            m_Param = new AgentParameters(community);
            m_Param.Version = SnmpVersion.Ver2;
            IpAddress agent = new IpAddress(IpAddr);

            // 创建代理(基站);
            m_target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

            return m_target;
        }

        /// <summary>
        /// GetNext;
        /// </summary>
        protected Dictionary<string, string> GetNext(ref List<string> oid)
        {
            SimpleSnmp SnmpMsg = new SimpleSnmp(this.m_DestIPAddr, this.m_Community);
            Dictionary<string, string> Res = new Dictionary<string, string>();
            List<string> NextOids = new List<string>();
            Dictionary<Oid, AsnType> TempRes = new Dictionary<Oid, AsnType>();
            string[] oidargs;

            if(oid.Count != 0)
            {
                oidargs = oid.ToArray();
                TempRes = SnmpMsg.GetNext(this.m_Version, oidargs);
                if (TempRes != null)
                {
                    oid.Clear();
                    foreach (KeyValuePair<Oid, AsnType> entry in TempRes)
                    {
                        Res.Add(entry.Key.ToString(), entry.Value.ToString());
                        oid.Add(entry.Key.ToString());                                // 将Next信息回填;
                    }
                }
                else
                {
                    oid.Clear();
                }
            }
            
            return Res;
        }

    }
    /// <summary>
    /// 异步获取SNMP结果参数;
    /// </summary>
    public class SnmpHelperResult : IAsyncResult
    {
        /// <summary>
        /// Key：oid;
        /// value：数值;
        /// </summary>
        private Dictionary<string, string> m_Result;

        public void SetSNMPReslut(Dictionary<string, string> res)
        {
            this.m_Result = res;
        }

        public object AsyncState
        {
            get
            {
                return m_Result;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsCompleted
        {
            get
            {
                throw new NotImplementedException();
            }
        }


    }

    /// <summary>
    /// Snmp报文V2c版本
    /// </summary>
    public class SnmpHelperV2c : SnmpHelper
    {
        public SnmpHelperV2c(string Commnuity, string ipaddr) : base(Commnuity, ipaddr)
        {
        }


        /// <summary>
        /// GetRequest
        /// </summary>
        /// <param name="PduList">需要查询的Pdu列表</param>
        /// <param name="Community">需要设置的Community</param>
        /// <param name="IpAddr">需要设置的IP地址</param>
        /// <returns>返回一个字典,键值为OID,值为MIB值;</returns>
        public override Dictionary<string, string> GetRequest(List<string> PduList, string Community, string IpAddr)
        {
            Dictionary<string, string> rest = new Dictionary<string, string>();
            OctetString community = new OctetString(Community);
            AgentParameters param = new AgentParameters(community);
            param.Version = SnmpVersion.Ver2;

            // 创建代理(基站);
            UdpTarget target = ConnectToAgent(Community, IpAddr);

            // 填写Pdu请求;
            Pdu pdu = new Pdu(PduType.Get);
            foreach (string pdulist in PduList)
            {
                pdu.VbList.Add(pdulist);
            }

            // 接收结果;
            m_Result = (SnmpV2Packet)target.Request(pdu, param);

            // 如果结果为空,则认为Agent没有回响应;
            if (m_Result != null)
            {
                // ErrorStatus other then 0 is an error returned by 
                // the Agent - see SnmpConstants for error definitions
                if (m_Result.Pdu.ErrorStatus != 0)
                {
                    // agent reported an error with the request
                    Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                        m_Result.Pdu.ErrorStatus,
                        m_Result.Pdu.ErrorIndex);

                    rest.Add(m_Result.Pdu.ErrorIndex.ToString(), m_Result.Pdu.ErrorStatus.ToString());
                }
                else
                {
                    for (int i = 0; i < m_Result.Pdu.VbCount; i++)
                    {
                        rest.Add(m_Result.Pdu.VbList[i].Oid.ToString(), m_Result.Pdu.VbList[i].Value.ToString());
                    }

                }
            }
            else
            {
                Console.WriteLine("No response received from SNMP agent.");
            }

            target.Close();
            return rest;
        }

        /// <summary>
        /// 带有异步回调的GetResponse;
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="PduList"></param>
        /// <param name="Community"></param>
        /// <param name="IpAddr"></param>
        public override void GetRequest(AsyncCallback callback, List<string> PduList, string Community, string IpAddr)
        {
            // 当确认全部获取SNMP数据后，调用callback回调;
            SnmpHelperResult res = new SnmpHelperResult();
            OctetString community = new OctetString(Community);
            AgentParameters param = new AgentParameters(community);
            Dictionary<string, string> rest = new Dictionary<string, string>();
            param.Version = SnmpVersion.Ver2;

            // 创建代理(基站);
            UdpTarget target = ConnectToAgent(Community,IpAddr);

            // Pdu请求形式Get;
            Pdu pdu = new Pdu(PduType.Get);
            foreach (string pdulist in PduList)
            {
                pdu.VbList.Add(pdulist);
            }
            
            Task tsk = Task.Factory.StartNew(()=> {
                
                // 接收结果;
                m_Result = (SnmpV2Packet)target.Request(pdu, param);

                if (m_Result != null)
                {
                    // ErrorStatus other then 0 is an error returned by 
                    // the Agent - see SnmpConstants for error definitions
                    if (m_Result.Pdu.ErrorStatus != 0)
                    {
                        // agent reported an error with the request
                        Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                            m_Result.Pdu.ErrorStatus,
                            m_Result.Pdu.ErrorIndex);

                        rest.Add(m_Result.Pdu.ErrorIndex.ToString(), m_Result.Pdu.ErrorStatus.ToString());
                        res.SetSNMPReslut(rest);
                        Thread.Sleep(3111);
                        callback(res);
                    }
                    else
                    {
                        for (int i = 0; i < m_Result.Pdu.VbCount; i++)
                        {
                            rest.Add(m_Result.Pdu.VbList[i].Oid.ToString(), m_Result.Pdu.VbList[i].Value.ToString());
                            res.SetSNMPReslut(rest);
                            Thread.Sleep(3111);
                            callback(res);
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No response received from SNMP agent.");
                }

                target.Close();
            });
            
        }

        /// <summary>
        /// 只需要填入Pdulist的GetResponse;
        /// </summary>
        /// <param name="VbList"></param>
        /// <returns></returns>
        public override SnmpV2Packet GetRequest(List<string> VbList)
        {
            if (m_target == null)
            {
                Log.Error("SNMP m_target = null.");
                return null;
            }

            // Log msg
            string logMsg;
            SnmpV2Packet result = null;

            Pdu pdu = new Pdu(PduType.Get);

            foreach(string oid in VbList)
            {
                pdu.VbList.Add(oid);
            }

            try
            {
                result = (SnmpV2Packet)m_target.Request(pdu, m_Param);
                if (null != result)
                {
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        logMsg = string.Format("Error in SNMP reply. Error {0} index {1}"
                            , result.Pdu.ErrorStatus, result.Pdu.ErrorIndex);
                        Log.Error(logMsg);
                    }
                    else
                    {
                        foreach(Vb vb in result.Pdu.VbList)
                        {
                            logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
                                , vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
                            Log.Debug(logMsg);
                        }
                    }
                }
                else
                {
                    Log.Error("No response received from SNMP agent.");
                }
            }
            catch ( Exception e)
            {
                Log.Error(e.Message.ToString());
                throw e;
            }
           

            return result;

        }

        public override SnmpV2Packet GetRequest(Pdu pdu)
        {
            if (m_target == null)
            {
                Log.Error("SNMP m_target = null.");
                return null;
            }

            if (pdu == null)
            {
                Log.Error("SNMP请求参数pdu为空");
                return null;
            }

            // Log msg
            string logMsg;
            SnmpV2Packet result = null;

            pdu.Type = PduType.Get;
            try
            {
                result = (SnmpV2Packet)m_target.Request(pdu, m_Param);
                if (null != result)
                {
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        logMsg = string.Format("Error in SNMP reply. Error {0} index {1}"
                            , result.Pdu.ErrorStatus, result.Pdu.ErrorIndex);
                        Log.Error(logMsg);
                    }
                    else
                    {
                        foreach (Vb vb in result.Pdu.VbList)
                        {
                            logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
                                , vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
                            Log.Debug(logMsg);
                        }
                    }
                }
                else
                {
                    Log.Error("No response received from SNMP agent.");
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message.ToString());
                throw e;
            }


            return result;

        }

        public override bool GetRequestAsync(Pdu pdu, SnmpAsyncResponse callback)
        {
            bool rs = false;
            if (m_target == null)
            {
                Log.Error("SNMP m_target = null.");
                return false;
            }

            if (pdu == null)
            {
                Log.Error("SNMP请求参数pdu为空");
                return false;
            }

            try
            {
                rs = m_target.RequestAsync(pdu, m_Param, callback);
                if (!rs)
                {
                    Log.Error("SNMP异步请求错误");
                }
                
            }
            catch (Exception e)
            {
                Log.Error(e.Message.ToString());
                throw e;
            }

            return rs;
        }

        /// <summary>
        /// SNMP Set
        /// </summary>
        /// <param name="pdu"></param>
        /// <param name="community"></param>
        /// <returns></returns>
        public override SnmpV2Packet SetRequest(Pdu pdu)
        {
            if (m_target == null)
            {
                Log.Error("SNMP m_target = null.");
                return null;
            }

            if (pdu == null)
            {
                Log.Error("Parameter pdu=null.");
                return null;
            }
            pdu.Type = PduType.Set;

            // log msg
            string logMsg;

            SnmpV2Packet response;

            try
            {
                response = (SnmpV2Packet)m_target.Request(pdu, m_Param);
            }
            catch (Exception e)
            {
                Log.Error(e.Message.ToString());
                throw e;
            }

            if (response == null)
            {
                Log.Error("SNMP Set 命令错误");
            }
            else
            {
                if (response.Pdu.ErrorStatus != 0)
                {
                    logMsg = string.Format("SNMP agent returned ErrorStatus {0} on index {1}"
                        , response.Pdu.ErrorStatus, response.Pdu.ErrorIndex);
                }
                else
                {
                    foreach(Vb vb in response.Pdu.VbList)
                    {
                        logMsg = string.Format("ObectName={0}, Type={1}, Value={2}"
                            , vb.Oid.ToString(), vb.Value.Type.ToString(), vb.Value.ToString());
                    }
                }
            }

            return response;
        }
  

        /// <summary>
        /// SetRequest的SnmpV2c版本
        /// </summary>
        /// <param name="PduList">需要查询的Pdu列表</param>
        /// <param name="Community">需要设置的Community</param>
        /// <param name="IpAddress">需要设置的IP地址</param>
        public override void SetRequest(Dictionary<string, string> PduList, string Community, string IpAddress)
        {
            // Prepare target
            UdpTarget target = new UdpTarget((IPAddress)new IpAddress(IpAddress));
            // Create a SET PDU
            Pdu pdu = new Pdu(PduType.Set);
            foreach (var list in PduList)
            {
                pdu.VbList.Add(new Oid(list.Key), new OctetString(list.Value));
            }

            // Set Agent security parameters
            AgentParameters aparam = new AgentParameters(SnmpVersion.Ver2, new OctetString(Community));
            // Response packet
            SnmpV2Packet response;
            try
            {
                // Send request and wait for response
                response = target.Request(pdu, aparam) as SnmpV2Packet;
            }
            catch (Exception ex)
            {
                // If exception happens, it will be returned here
                Console.WriteLine(String.Format("Request failed with exception: {0}", ex.Message));
                target.Close();
                return;
            }
            // Make sure we received a response
            if (response == null)
            {
                Console.WriteLine("Error in sending SNMP request.");
            }
            else
            {
                // Check if we received an SNMP error from the agent
                if (response.Pdu.ErrorStatus != 0)
                {
                    Console.WriteLine(String.Format("SNMP agent returned ErrorStatus {0} on index {1}",
                        response.Pdu.ErrorStatus, response.Pdu.ErrorIndex));
                }
                else
                {
                    // Everything is ok. Agent will return the new value for the OID we changed
                    Console.WriteLine(String.Format("Agent response {0}: {1}",
                        response.Pdu[0].Oid.ToString(), response.Pdu[0].Value.ToString()));
                }
            }
        }

        public override void SetRequest(AsyncCallback callback, List<string> PduList)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// GetNext的对外接口
        /// 该函数会在第一次收到客户端请求后;
        /// 在每次收到基站的GetResponse之后，都会调用客户端注册的回调函数
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="PduList"></param>
        public override void GetNextRequest(AsyncCallback callback, List<string> PduList)
        {
            SnmpHelperResult res = new SnmpHelperResult();
            if ((PduList.Count == 0) && (PduList == null))      // 如果PduList内容为空，则不进行处理;
            {
                return;
            }
            Task tsk = Task.Factory.StartNew(() =>
            {
                Dictionary<string, string> NextRest = new Dictionary<string, string>();
                List<List<string>> AllList = new List<List<string>>();
                bool GetNextorNot = true;

                AllList.Add(PduList);
                while(GetNextorNot)
                {

                    NextRest = this.GetNext(ref PduList);           // 得到返回结果;
                    if (PduList.Count == 0)
                    {
                        GetNextorNot = false;
                    }
                    res.SetSNMPReslut(NextRest);
                    callback(res);
                }
                return;
            });
        }
        
    }

}
