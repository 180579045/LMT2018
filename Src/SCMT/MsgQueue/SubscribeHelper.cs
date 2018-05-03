using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;

namespace MsgQueue
{
    public class SubscribeHelper
    {
        private SubscribeClient subClient;

        public SubscribeHelper()
        {
            subClient = new SubscribeClient(CommonPort.PubServerPort);
            subClient.Run();
        }

        ~SubscribeHelper()
        {
            subClient.Dispose();
        }

        public static SubscribeHelper GetInstance()
        {
            return Singleton<SubscribeHelper>.GetInstance();
        }

        public static bool AddSubscribe(string topic, HandlerSubscribeMsg handler)
        {
            return GetInstance().SubscribeTopic(topic, handler);
        }

        public static bool CancelSubscribe(string topic)
        {
            return GetInstance().SubScribeCancel(topic);
        }

        //TODO 需要加topic流程
        private bool SubscribeTopic(string topic, HandlerSubscribeMsg handler)
        {
            subClient.AddSubscribeTopic(topic, handler);
            return true;
        }

        private bool SubScribeCancel(string topic)
        {
            subClient.CancelSubscribeTopic(topic);
            return true;
        }

    }
}
