using CDLBrowser.Parser.BPLAN;
using CDLBrowser.Parser.Document.Event;
using CommonUility;
using MsgQueue;
using SCMTMainWindow.Component.ViewModel;
using System;

namespace SCMTMainWindow.Controls.PlanBParser
{
    public class HLMessageParser
    {
        //工信部测试B方案显示HL信令消息
        public SignalBPlan signalBPlanThread;
        public SubscribeClient subClient;
        public SignalBPlan signalB;
        public object UEItem;
        public object eNBItem;
        public object gNBItem;
        public HLMessageViewModel MessageList;

        // B方案呈现HL信令消息;
        private void updateHlSingalMessageInfo(SubscribeMsg msg)
        {
            ScriptMessage scriptMessage = JsonHelper.SerializeJsonToObject<ScriptMessage>(msg.Data);
            EventNew UIMsg = new EventNew();

            UIMsg.DisplayIndex = scriptMessage.NO;
            UIMsg.TimeStamp = scriptMessage.time;
            UIMsg.EventName = scriptMessage.message;
            UIMsg.MessageDestination = scriptMessage.MessageDestination;
            UIMsg.MessageSource = scriptMessage.MessageSource;
            UIMsg.data = scriptMessage.data;

            if (-1 != scriptMessage.UI.IndexOf("UE"))
            {
                MessageList.m_MsgList.Add(UIMsg);
            }
            if (-1 != scriptMessage.UI.IndexOf("eNB"))
            {
                MessageList.m_eNB_MsgList.Add(UIMsg);
            }
            if (-1 != scriptMessage.UI.IndexOf("gNB"))
            {
                MessageList.m_gNB_MsgList.Add(UIMsg);
            }
            return;
        }

        // 开始解析;
        public void beginParse()
        {
            //PubSubServer.GetInstance().InitServer();
            SignalBConfig.StartByScriptXml();
            PublishHelper.PublishMsg("StartTraceHlSignal", "");
        }

        // 构建HLMessage的解析器;
        public HLMessageParser()
        {
            signalB = new SignalBPlan();
            subClient = new SubscribeClient(CommonPort.PubServerPort);
            SubscribeHelper.AddSubscribe("HlSignalMsg", updateHlSingalMessageInfo);
        }
    }
}
