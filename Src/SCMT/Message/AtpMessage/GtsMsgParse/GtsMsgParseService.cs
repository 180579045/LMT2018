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

            _subscribeClient = new SubscribeClient(CommonPort.PubServerPort);
            _subscribeClient.AddSubscribeTopic("/GtsMsgParseService", OnMsgParse);
            _subscribeClient.Run();
        }

        private void OnMsgParse(SubscribeMsg msg)
        {
            //TODO 使用推拉模式解析数据，但可能有顺序性的要求，先使用worker进行解析
            _worker.DoWork(msg.Data);
        }

        private SubscribeClient _subscribeClient;
        private GtsMsgParseWorker _worker;
    }
}
