/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：SnmpMessage.cs
// 文件功能描述：Snmp报文类;
// 创建人：郭亮;
// 版本：V1.0
// 创建标识：创建文件;
// 时间：2017-11-2
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using SnmpSharpNet;

namespace SCMTOperationCore.Message.SNMP
{
    /// <summary>
    /// 抽象SNMP报文，以便后续扩展SNMPV3;
    /// </summary>
    abstract public class SnmpMessage
    {
        public Dictionary<string, string> m_Response { get; set; }        // Get返回的结果;
        public string m_IPAddr { get; set; }                              // 代理目标IP地址
        public string m_Community { get; set; }                           // 代理目标的Community
        public string m_ErrorStatus { get; set; }                         // 错误码;
        protected SnmpV2Packet m_Result { get; set; }                     // 返回结果;
        public SnmpVersion m_Version { get { return SnmpVersion.Ver2; } } // SNMP版本;
        public List<string> PduList { get; set; }                         // Snmp报文的Pdu列表

        public SnmpMessage(string Commnuity, string ipaddr)
        {
            this.m_IPAddr = ipaddr;
            this.m_Community = Commnuity;
        }

        public SnmpMessage()
        {

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
        public abstract Dictionary<string,string> GetRequest(List<string> PduList);

        /// <summary>
        /// SetRequest的对外接口
        /// </summary>
        /// <param name="PduList">需要查询的Pdu列表</param>
        /// <param name="Community">需要设置的Community</param>
        /// <param name="IpAddress">需要设置的IP地址</param>
        public abstract void SetRequest(Dictionary<string, string> PduList, string Community, string IpAddress);


        /// <summary>
        /// GetNext的对外接口;
        /// </summary>
        public abstract Dictionary<string, string> GetNext(string oid);

        /// <summary>
        /// 连接代理;
        /// </summary>
        /// <param name="Community"></param>
        /// <param name="IpAddr"></param>
        /// <returns></returns>
        protected UdpTarget ConnectToAgent(string Community, string IpAddr)
        {
            OctetString community = new OctetString(Community);
            AgentParameters param = new AgentParameters(community);
            param.Version = SnmpVersion.Ver2;
            IpAddress agent = new IpAddress(IpAddr);
            Dictionary<string, string> rest = new Dictionary<string, string>();

            // 创建代理(基站);
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
            return target;
        }

    }
    /// <summary>
    /// 异步获取SNMP结果参数;
    /// </summary>
    public class SnmpMessageResult : IAsyncResult
    {
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
    public class SnmpMessageV2c : SnmpMessage
    {
        public SnmpMessageV2c(string Commnuity, string ipaddr) : base(Commnuity, ipaddr)
        {
        }

        public SnmpMessageV2c()
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
            SnmpMessageResult res = new SnmpMessageResult();
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
                    callback(res);
                }
                else
                {
                    for (int i = 0; i < m_Result.Pdu.VbCount; i++)
                    {
                        rest.Add(m_Result.Pdu.VbList[i].Oid.ToString(), m_Result.Pdu.VbList[i].Value.ToString());
                        res.SetSNMPReslut(rest);
                        callback(res);
                    }

                }
            }
            else
            {
                Console.WriteLine("No response received from SNMP agent.");
            }

            target.Close();
        }

        /// <summary>
        /// 只需要填入Pdulist的GetResponse;
        /// </summary>
        /// <param name="PduList"></param>
        /// <returns></returns>
        public override Dictionary<string, string> GetRequest(List<string> PduList)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// GetNext方法;
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public override Dictionary<string, string> GetNext(string oid)
        {
            SimpleSnmp SnmpMsg = new SimpleSnmp(this.m_IPAddr, this.m_Community);
            Dictionary<Oid,AsnType> TempRes = SnmpMsg.GetNext(this.m_Version, new string[] { oid });
            Dictionary<string, string> Res = new Dictionary<string, string>();

            if(TempRes != null)
            {
                foreach (KeyValuePair<Oid, AsnType> entry in TempRes)
                {
                    Console.WriteLine("{0} = {1}: {2}", entry.Key.ToString(), SnmpConstants.GetTypeName(entry.Value.Type),
                                        entry.Value.ToString());
                    Res.Add(entry.Key.ToString(), entry.Value.ToString());
                }
            }
            else
            {
                // null
            }
            

            return Res;
        }

    }

}
