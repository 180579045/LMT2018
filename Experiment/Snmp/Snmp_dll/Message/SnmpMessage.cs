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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SnmpSharpNet;

namespace Snmp_dll
{
    /// <summary>
    /// 抽象SNMP报文，以便后续扩展SNMPV3;
    /// </summary>
    abstract public class SnmpMessage
    {
        public Dictionary<string, string> Response { get; set; }        // Get返回的结果;
        public string IPAddr { get; set; }                              // 代理目标IP地址
        public string Community { get; set; }                           // 代理目标的Community
        public List<string> PduList { get; set; }                       // Snmp报文的Pdu列表
        public SnmpV2Packet Result { get; set; }                        // 返回结果;
        public string ErrorStatus { get; set; }                         // 错误码;

        /// <summary>
        /// GetRequest的对外接口，前三个参数为Snmp报文所必须的
        /// </summary>
        public abstract Dictionary<string, string> GetRequest(List<string> PduList, string Community, string IpAddress);

    }

    /// <summary>
    /// Snmp报文V2c版本
    /// </summary>
    public class SnmpMessageV2c : SnmpMessage
    {
        /// <summary>
        /// V2c版本的GetRequest
        /// </summary>
        public override Dictionary<string, string> GetRequest(List<string> PduList, string Community, string IpAddr)
        {
            OctetString community = new OctetString(Community);
            AgentParameters param = new AgentParameters(community);
            param.Version = SnmpVersion.Ver2;
            IpAddress agent = new IpAddress(IpAddr);
            Dictionary<string, string> rest = new Dictionary<string, string>();

            // 创建代理(基站);
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

            // Pdu请求形式Get;
            Pdu pdu = new Pdu(PduType.Get);
            foreach (string pdulist in PduList)
            {
                pdu.VbList.Add(pdulist);
            }

            // 接收结果;
            Result = (SnmpV2Packet)target.Request(pdu, param);

            // 如果结果为空,则认为Agent没有回响应;
            if (Result != null)
            {
                // ErrorStatus other then 0 is an error returned by 
                // the Agent - see SnmpConstants for error definitions
                if (Result.Pdu.ErrorStatus != 0)
                {
                    // agent reported an error with the request
                    Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                        Result.Pdu.ErrorStatus,
                        Result.Pdu.ErrorIndex);

                    rest.Add(Result.Pdu.ErrorIndex.ToString(), Result.Pdu.ErrorStatus.ToString());
                }
                else
                {
                    // Reply variables are returned in the same order as they were added
                    //  to the VbList
                    Console.WriteLine("sysDescr({0}) ({1}): {2}",
                        Result.Pdu.VbList[0].Oid.ToString(),
                        SnmpConstants.GetTypeName(Result.Pdu.VbList[0].Value.Type),
                        Result.Pdu.VbList[0].Value.ToString());

                    for (int i = 0; i < Result.Pdu.VbCount; i++)
                    {
                        rest.Add(Result.Pdu.VbList[i].Oid.ToString(), Result.Pdu.VbList[i].Value.ToString());
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
    }

}
