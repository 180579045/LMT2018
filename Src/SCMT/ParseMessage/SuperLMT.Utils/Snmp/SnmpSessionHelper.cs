// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnmpSessionHelper.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The snmp session helper.
// </summary>
// Author:  pengqiang
// History: 2012/2/14 created by pengqiang
// --------------------------------------------------------------------------------------------------------------------
namespace SuperLMT.Utils.Snmp
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Common.Logging;

    using SnmpSharpNet;

    /// <summary>
    /// The snmp session helper.
    /// </summary>
    public class SnmpSessionHelper : ISnmpSession
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SnmpSessionHelper));

        #region 单例实现

        /// <summary>
        /// The g_instance.
        /// </summary>
        private static readonly SnmpSessionHelper Instance = new SnmpSessionHelper();

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static SnmpSessionHelper Singleton
        {
            get
            {
                return Instance;
            }
        }
        #endregion

        #region ISnmpSession实现

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="peer">
        /// The peer.
        /// </param>
        /// <param name="vbs">
        /// The vbs.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Vb> Get(SnmpAgentInfo peer, IEnumerable<Vb> vbs)
        {
            return this.Excute(peer, PduType.Get, vbs);
        }

        /// <summary>
        /// The get next.
        /// SNMP GET-NEXT request
        /// </summary>
        /// <param name="peer">
        /// The peer.
        /// </param>
        /// <param name="vbs">
        /// The vbs.
        /// </param>
        /// <returns>
        /// Result of the SNMP request in a dictionary format with Oid => AsnType values/>.
        /// </returns>
        public IEnumerable<Vb> GetNext(SnmpAgentInfo peer, Vb[] vbs)
        {
            return this.Excute(peer, PduType.GetNext, vbs);
        }

        /// <summary>
        /// The get bulk.
        /// SNMP GET-BULK request
        /// </summary>
        /// <remarks>
        /// Performs a GetBulk SNMP v2 operation on a list of OIDs. This is a convenience function that
        /// calls GetBulk(Pdu) method.
        /// </remarks>
        /// <param name="peer">
        /// The peer.
        /// </param>
        /// <param name="vbs">
        /// The vbs.
        /// </param>
        /// <returns>
        /// Result of the SNMP request in a dictionary format with Oid => AsnType values/>.
        /// </returns>
        public IEnumerable<Vb> GetBulk(SnmpAgentInfo peer, Vb[] vbs)
        {
            return this.Excute(peer, PduType.GetBulk, vbs);
        }

        /// <summary>
        /// SNMP SET request
        /// </summary>
        /// <example>
        /// Set operation in SNMP version 1:
        /// <code>
        /// String snmpAgent = "10.10.10.1";
        /// String snmpCommunity = "private";
        /// SimpleSnmp snmp = new SimpleSnmp(snmpAgent, snmpCommunity);
        /// // Create a request Pdu
        /// List&lt;Vb&gt; vbList = new List&lt;Vb&gt;();
        /// Oid setOid = new Oid("1.3.6.1.2.1.1.1.0"); // sysDescr.0
        /// OctetString setValue = new OctetString("My personal toy");
        /// vbList.Add(new Vb(setOid, setValue));
        /// Dictionary&lt;Oid, AsnType&gt; result = snmp.Set(SnmpVersion.Ver1, list.ToArray());
        /// if( result == null ) {
        ///   Console.WriteLine("Request failed.");
        /// } else {
        ///   Console.WriteLine("Success!");
        /// }
        /// </code>
        ///  To use SNMP version 2, change snmp.Set() method call first parameter to SnmpVersion.Ver2.
        /// </example>
        /// <param name="peer">
        /// The peer.
        /// </param>
        /// <param name="vbs">
        /// Vb array containing Oid/AsnValue pairs for the SET operation
        /// </param>
        /// <returns>
        /// Result of the SNMP request in a dictionary format with Oid =&gt; AsnType values
        /// </returns>
        public IEnumerable<Vb> Set(SnmpAgentInfo peer, IEnumerable<Vb> vbs)
        {
            return this.Excute(peer, PduType.Set, vbs);
        }

        #endregion

        #region ITrapReceiveCall接口实现

        /// <summary>
        /// The trap receive.
        /// </summary>
        /// <param name="objBinary">
        /// The obj binary.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void TrapReceive(byte[] objBinary)
        {
            object obj = null;
            try
            {
                obj = ObjectSerializer.DeSerializeBinary(objBinary);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("把二进制流序列化为Pdu对象失败!, 原因为{0}", ex.Message));
                return;
            }

            var trapPdu = obj as AsnType;
            if (null == trapPdu)
            {
                return;
            }

            if (PduType.V2Trap == (PduType)trapPdu.Type)
            {
                this.SnmpV2TrapRelay(trapPdu as Pdu);
            }
            else
            {
                throw new NotImplementedException("收到了非Snmp V2版本的Trap PDU, 还未实现相应的处理!");
            }
        }

        #endregion

        #region 公共对外成员

        /// <summary>
        /// The initialize.
        /// </summary>
        public void Initialize()
        {
            /*建立和Trap转发服务的通道*/
            string _endpoint = @"net.tcp://localhost:5883/Sub"; /*服务地址*/
        }

        #endregion

        #region 内部成员

        /// <summary>
        /// 处理Snmp V2的PDU
        /// </summary>
        /// <param name="trapPdu"></param>
        protected void SnmpV2TrapRelay(Pdu trapPdu)
        {
            //string strTrapTarget = trapPdu.
            //ITrapPduParseCall trapParseCall = TrapPduTargetFilter.GetSubscribers(strTrapTarget);

            //if( null != trapParseCall )
            //{
            //    trapParseCall.TrapPduParse(trapPdu);
            //}
            //else
            //{
            //    log.Info(string.Format("网元{0}没有注册TrapPduParser，该网元的TrapPDU被丢弃!", strTrapTarget));
            //}
        }
        
        /// <summary>
        /// SNMP Operation Execute
        /// </summary>
        /// <example>Set operation in SNMP version 1:
        /// <code>
        /// </code>
        /// 
        /// 
        /// </example>
        /// <param name="version">SNMPAgentInfo. 对端SNMP Agent相关配置信息</param>
        /// <param name="pdu">PduType. 要执行的操作类型：Get/Set/GetNext/GetBulk</param>
        /// <param name="pdu">Vb[]. 操作参数</param>
        /// <returns>Result of the SNMP request in a IEnumerator<vb> </returns>
        public IEnumerable<Vb> Excute(SnmpAgentInfo peer, PduType pduType, IEnumerable<Vb> vbs)
        {
            //验证对端地址信息是否正确
            if (!this.ValidPeer(peer))
            {
                Log.Error("SnmpAgentInfo校验失败!");
                return null;
            }

            //组装PDU
            Pdu pdu = this.PacketPdu(pduType, vbs);

            //GetBulk包需要设置NonRepeaters和MaxRepetitions两个字段
            if (PduType.GetBulk == pdu.Type)
            {
                pdu.NonRepeaters = peer.NonRepeaters;
                pdu.MaxRepetitions = peer.MaxRepetitions;
            }

            using (var _target = new UdpTarget(peer.PeerAddress, peer.Port, peer.TimeOut, peer.RetryTime))
            {
                SnmpPacket result = null;
                try
                {
                    var param = new AgentParameters(peer.Version, new OctetString(peer.Community));
                    result = _target.Request(pdu, param); 
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    throw;
                }

                if (result != null)
                {
                    if (result.Pdu.ErrorStatus == 0)
                    {
                        return result.Pdu;
                    }

                    throw new SnmpErrorStatusException("Agent responded with an error", result.Pdu.ErrorStatus, result.Pdu.ErrorIndex);
                }
            }

            return null;
        }


        /// <summary>
        /// 把OidList打包成PDU
        /// </summary>
        /// <param name="oidList"></param>
        /// <returns></returns>
        private Pdu PacketPdu(PduType pduType, IEnumerable<Vb> vbs)
        {
            var pdu = new Pdu(pduType);
            foreach (Vb vb in vbs)
            {
                pdu.VbList.Add(vb);
            }

            return pdu;
        }

        /// <summary>
        /// 验证远端的信息是否正确
        /// </summary>
        /// <param name="peerIP"></param>
        /// <param name="strCommunity"></param>
        /// <returns></returns>
        protected bool ValidPeer(SnmpAgentInfo peer)
        {
            if (peer.PeerAddress == IPAddress.None || peer.PeerAddress == IPAddress.Any)
            {
                throw new SnmpException("IPAddress is not valid.");
            }
            if (peer.Community.Length < 1 || peer.Community.Length > 50)
            {
                throw new SnmpException("Community is not valid.");
            }
            if (peer.Version != SnmpVersion.Ver1 && peer.Version != SnmpVersion.Ver2)
            {
                throw new SnmpInvalidVersionException("Support SNMP version 1 and 2 only.");
            }

            return true;
        }

        #endregion

        #region 内部成员

        /*和Trap转发服务的通信通道*/
        //protected ITrapReceiveSubscription _trapReceiveProxy;

        #endregion
    }
}
