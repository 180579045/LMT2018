using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpMessage.MsgDefine;
using CommonUility;

namespace AtpMessage.LinkMgr
{
    /// <summary>
    /// ATP的直连模式
    /// </summary>
    public class AtpDirectLink : NetElementLinkBase
    {
        public override void Logon(NetElementConfig netElementAddress)
        {
            MsgGtsm2GtsaLogonReq logonReq = new MsgGtsm2GtsaLogonReq()
            {
                u16LinkId = NbLinkType.NB_DSAP_NO_OSP_DSP_PC_GTSA,
                u16Port = NbLinkType.NB_IP_PORT_NO_OSP_DSP_PC_GTSA,
                u32TraceDspIpAddr = NetActHelper.inet_addr(netElementAddress.TraceIp),
                u8MacAddr = NetActHelper.GetMacAddrByIp(netElementAddress.TraceIp),
                u8AgentSlot = netElementAddress.AgentSlot,

                header =
                {
                    u8RemoteMode = GtsMsgType.CONNECT_DIRECT_MSG,
                    u16SourceID = GtsMsgType.DEST_GTSM,
                    u16Opcode = GtsMsgType.O_GTSMGTSA_LOGON_REQ,
                    u16DestID = netElementAddress.Index
                }
            };
            logonReq.header.u16Length = logonReq.ContentLen;

            SendPackets(SerializeHelper.SerializeStructToBytes(logonReq), true);

            base.Logon(netElementAddress);
        }

        public override void Logoff(NetElementConfig netElementAddress)
        {
            MsgGtsm2GtsaQuitInd logoffReq = new MsgGtsm2GtsaQuitInd()
            {
                header =
                {
                    u8RemoteMode = GtsMsgType.CONNECT_DIRECT_MSG,
                    u16SourceID = GtsMsgType.DEST_GTSM,
                    u16Opcode = GtsMsgType.O_GTSMGTSA_QUIT_IND,
                    u16DestID = netElementAddress.Index
                }
            };
            logoffReq.header.u16Length = logoffReq.ContentLen;

            SendPackets(SerializeHelper.SerializeStructToBytes(logoffReq), false);
            base.Logoff(netElementAddress);
        }

        public override bool SendTSwitchs(byte[] switchBytes, NetElementConfig netElementAddress)
        {
            MsgGtsm2GtsaDspTraceCtrlReq dspReq = new MsgGtsm2GtsaDspTraceCtrlReq()
            {
                header =
                {
                    u8RemoteMode = GtsMsgType.CONNECT_DIRECT_MSG,
                    u16SourceID = GtsMsgType.DEST_GTSM,
                    u16Opcode = GtsMsgType.O_GTSMGTSA_DSPTRACE_CTRL_REQ,
                    u16DestID = netElementAddress.Index
                },
                u8Switch = 1
            };
            dspReq.header.u16Length = dspReq.ContentLen;
            SendPackets(SerializeHelper.SerializeStructToBytes(dspReq));

            MsgGtsm2GtsaTraceCtrlReq req = new MsgGtsm2GtsaTraceCtrlReq()
            {
                header =
                {
                    u8RemoteMode = GtsMsgType.CONNECT_DIRECT_MSG,
                    u16SourceID = GtsMsgType.DEST_GTSM,
                    u16Opcode = GtsMsgType.O_GTSMGTSA_TRACE_CTRL_REQ,
                    u16DestID = netElementAddress.Index
                },
                u8TraceControl = switchBytes
            };
            req.header.u16Length = req.ContentLen;
            SendPackets(SerializeHelper.SerializeStructToBytes(req));

            return true;
        }
    }
}
