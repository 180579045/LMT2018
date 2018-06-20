using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpMessage.MsgDefine;
using CommonUtility;

namespace AtpMessage.LinkMgr
{
	/// <summary>
	/// 远程消息方式连接板卡
	/// </summary>
	public class AtpRemoteMsgLink : NetElementLinkBase
	{
		public override void Logon(NetElementConfig netElementAddress)
		{
			//登录请求
			MsgGtsm2GtsaLogonReq logonReq = new MsgGtsm2GtsaLogonReq()
			{
				u16LinkId = NbLinkType.NB_DSAP_NO_OSP_DSP_PC_GTSA,
				u16Port = NbLinkType.NB_IP_PORT_NO_OSP_DSP_PC_GTSA,

			};

			//建链请求
			MsgGtsm2GtsaAddFlowReq addFlowReq = new MsgGtsm2GtsaAddFlowReq()
			{
				u32IpType = GtsMsgType.OM_GTS_IPV4,
				u16RackNo = 0xff,
				u16ProcId = 0xff,
				u16FrameNo = netElementAddress.FrameNo,
				u16SlotNo = netElementAddress.SlotNo,

				header =
				{
					u16Opcode = GtsMsgType.O_GTSMGTSA_ADDFLOW_REQ,

				}
			};

			//TODO 只有建链通过后才能发送登录请求。这里需要同步处理。
			//SendPackets(SerializeHelper.SerializeStructToBytes(addFlowReq));

		}
	}
}
