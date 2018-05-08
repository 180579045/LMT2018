using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using MsgQueue;

namespace AtpMessage.GtsMsgParse
{
	public class GtsMsgParseService
	{
		public static GtsMsgParseService GetInstance()
		{
			return Singleton<GtsMsgParseService>.GetInstance();
		}

		public void InitService()
		{
			_worker = new GtsMsgParseWorker();
			SubscribeHelper.AddSubscribe("/GtsMsgParseService/WinPcap", OnIpFrameMsgParse);
			SubscribeHelper.AddSubscribe("/GtsMsgParseService/GtsaSend", OnGtsaMsgParse);
		}

		private void OnIpFrameMsgParse(SubscribeMsg msg)
		{
			//TODO 使用推拉模式解析数据，但可能有顺序性的要求，先使用worker进行解析
			_worker.ParseIpFrameData(msg.Data);
		}

		private void OnGtsaMsgParse(SubscribeMsg msg)
		{
			_worker.ParseUdpData(msg.Data);
		}

		private GtsMsgParseWorker _worker;
	}
}
