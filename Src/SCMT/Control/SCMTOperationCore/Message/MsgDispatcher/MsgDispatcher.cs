using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using MsgQueue;
using SCMTOperationCore.Message.SI;

// 消息中转处理

namespace SCMTOperationCore.Message.MsgDispatcher
{
	public class MsgDispatcher : Singleton<MsgDispatcher>
	{
		#region 构造、析构

		private MsgDispatcher()
		{
			SubscribeHelper.AddSubscribe("WM_DEAL_ENBPHASE", OnEnbPhase);
		}

		#endregion

		#region 消息处理

		private void OnEnbPhase(SubscribeMsg msg)
		{
			var rspMsg = msg as SubMsgWithTargetIp;
			AnalyseSIPhaseMsg(rspMsg.Data, rspMsg.TargetIp);
		}

		#endregion

		#region 私有接口

		private void AnalyseSIPhaseMsg(byte[] msgBytes, string targetIp)
		{
			var rsp = SerializeHelper.BytesToStruct<SI_NBPHASE_REP_MSG>(msgBytes);
			var phase = (ENODEB_PHASE)rsp.u16NodeBPhase;
			LMTORSYSTYPE type = (LMTORSYSTYPE) rsp.u8NodeBType;

			var tipInfo = "";
			switch (phase)
			{
				case ENODEB_PHASE.ENODEBPHASE_SI_OVERTIME:
				case ENODEB_PHASE.ENODEBPHASE_SI_INTIME:
				case ENODEB_PHASE.ENODEBPHASE_SI_FINISH:
					if (type == LMTORSYSTYPE.NODEBTYPE_LTE ||
						type == LMTORSYSTYPE.NODEBTYPE_Macro)
					{
						// TODO 如果需要硬件版本号，在此处添加
					}
					else
					{
						tipInfo = $"IP为{targetIp}的基站类型不能识别！";
					}
					break;
				case ENODEB_PHASE.ENODEBPHASE_EXIST_CONNECT:
					tipInfo = $"已有本地维护管理站与IP为{targetIp}的基站相连，请稍后再访问此基站！";
					break;
				case ENODEB_PHASE.ENODEBPHASE_LOGIN_BACKBOARD:
					tipInfo = $"不可使用备板地址({targetIp})登录基站，请输入主板IP地址！";
					break;
				case ENODEB_PHASE.ENODEBPHASE_UNKNOWN:
					tipInfo = $"IP为{targetIp}的基站状态未知！";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// TODO 前台显示tipInfo
		}

		#endregion
	}

	public enum LMTORSYSTYPE
	{
		LMTORSYSTYPE_UNKNOWN = -1,
		NODEBTYPE_Macro,    /*宏基站*/
		NODEBTYPE_MINI,     /*微基站*/
		NODEBTYPE_SUPER,    /*超级基站*/
		NODEBTYPE_LTE,      /*eNB*/
		NODEBTYPE_CUSTOM    //自定义模式
	}
}
