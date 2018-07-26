using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommonUtility;
using MsgQueue;
using SCMTOperationCore.Message.SI;
using LogManager;
using SCMTOperationCore.Control;

// 消息中转处理

namespace SCMTOperationCore.Message.MsgDispatcher
{
	public class MsgDispatcher : Singleton<MsgDispatcher>
	{
		#region 构造、析构

		private MsgDispatcher()
		{
			SubscribeHelper.AddSubscribe(TopicHelper.EnbPhaseMsg, OnEnbPhaseMsg);
			SubscribeHelper.AddSubscribe(TopicHelper.QuerySiPortVerRsp, OnQuerySiVerRsp);
		}

		#endregion

		#region 消息处理

		public void OnEnbPhaseMsg(SubscribeMsg msg)
		{
			// DataWithIp类型与SubscribeMsg类型的定义一样，但是SerializeJsonToObject转换时
			// 设置TargetIp为null，还没有找到问题的原因，暂时用SubscribeMsg类型
			//var rspMsg = JsonHelper.SerializeJsonToObject<DataWithIp>(msg.Data);
			var rspMsg = JsonHelper.SerializeJsonToObject<SubscribeMsg>(msg.Data);
			if (null == rspMsg)
			{
				Log.Error("转换消息为SubscribeMsg失败");
				return;
			}

			// 查询SI接口版本 TODO 后续移动到UE相关的模块，不在此进行查询
			//var reqHead = SiPortVerHelper.GetReqBytes();
			//NodeBControl.SendSiMsg(rspMsg.Topic, reqHead);

			AnalyseSIPhaseMsg(rspMsg.Data, rspMsg.Topic);
		}

		private void OnQuerySiVerRsp(SubscribeMsg msg)
		{
			//var rspMsg = JsonHelper.SerializeJsonToObject<DataWithIp>(msg.Data);
			var rspMsg = JsonHelper.SerializeJsonToObject<SubscribeMsg>(msg.Data);
			var rspHead = SiPortVerHelper.GetRspHead(rspMsg.Data);
			if (null == rspHead)
			{
				Log.Error("转换数据失败");
				return;
			}

			if (rspHead.u8Result != 0)
			{
				Log.Error("查询SI接口版本号失败");
				return;
			}

			ShowLogHelper.Show("查询si接口版本成功!", rspMsg.Topic, InfoTypeEnum.ENB_OTHER_INFO);

			if (1 == rspHead.u8Version)
			{
				var rspV1 = SiPortVerHelper.GetRspV1(rspMsg.Data);
				if (null == rspV1)
				{
					Log.Error("转换数据失败");
					return;
				}

				LoadData(rspV1);
			}
			else
			{
				Log.Error($"SI接口版本回复的版本号 {rspHead.u8Version} 未定义，目前支持 1");
			}
		}

		#endregion

		#region 私有接口

		// enb阶段消息
		private void AnalyseSIPhaseMsg(byte[] msgBytes, string targetIp)
		{
			if (string.IsNullOrWhiteSpace(targetIp) ||
				string.IsNullOrEmpty(targetIp))
			{
				Log.Error("解析出targetIp为null");
				return;
			}

			var rsp = new SI_NBPHASE_REP_MSG();
			if (-1 == rsp.DeserializeToStruct(msgBytes, 0))
			{
				Log.Error("enb阶段消息转换失败");
				return;
			}

			var phase = (ENODEB_PHASE)rsp.u16NodeBPhase;
			LMTORSYSTYPE type = (LMTORSYSTYPE) rsp.u8NodeBType;

			var tipInfo = "";
			var bBreakConnect = true;
			switch (phase)
			{
				case ENODEB_PHASE.ENODEBPHASE_SI_OVERTIME:
				case ENODEB_PHASE.ENODEBPHASE_SI_INTIME:
				case ENODEB_PHASE.ENODEBPHASE_SI_FINISH:
					if (type == LMTORSYSTYPE.NODEBTYPE_LTE ||
						type == LMTORSYSTYPE.NODEBTYPE_Macro)
					{
						// TODO 如果需要硬件版本号，在此处添加
						// c#中大括号的转义：需要连续两个{{或}}才会生成一个{或}
						PublishHelper.PublishMsg(TopicHelper.EnbConnectedMsg, $"{{\"TargetIp\" : \"{targetIp}\"}}");
					}
					else
					{
						tipInfo = $"IP为{targetIp}的基站类型不能识别！";
					}

					bBreakConnect = false;
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

			if (!string.IsNullOrEmpty(tipInfo))
			{
				MessageBox.Show(tipInfo, "连接", MessageBoxButton.OK);
			}

			// TODO 断开连接
			if (bBreakConnect)
			{
				
			}
		}


		private void LoadData(object rspObj)
		{
			var rsp = rspObj as GetSiPortVersionRspMsg;
			// TODO UE相关的信息放到UE模块进行查询和处理，不需要连接上就查询吧？
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

	// 通过SI接口版本查到的Ue相关信息
	public class UeInfo
	{
		
	}
}
