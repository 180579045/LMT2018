using MsgQueue;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace CDLBrowser.Parser.BPLAN
{
    public class ScriptMessage
    {
        public int NO
        { get; set; }

        public string UI
        { get; set; }

        public string ENBUEID
        { get; set; }

        public string MessageSource
        { get; set; }

        public string MessageDestination
        { get; set; }

        public string message
        { get; set; }

        public string data
        { get; set; }
        //脚本中没有时间戳，不需要解析
      
        public string time
        { get; set; }

    }

    public class SignalBPlan
    {
        public SubscribeClient subClient;

        public SignalBPlan()
        {
            subClient = new SubscribeClient(CommonPort.PubServerPort);
            subClient.AddSubscribeTopic("StartTraceHlSignal", startTraceByUI);
            subClient.Run();
        }

        public void startTraceByUI(SubscribeMsg msg)
        {
            ParseScript();
        }

        public void ParseScript()
        {
            try
            {
                int currentId = SignalBConfig.currentID;
                //先解析json文件
                JsonFile jsonFile = new JsonFile();
                JArray textJArray = JArray.Parse(jsonFile.ReadFile(@".\script\" + currentId + @".txt"));
                Console.WriteLine("read {0} file ok",(@".\script\" + currentId + @".txt"));
                IList<JToken> results = textJArray.Children().ToList();
                IList<ScriptMessage> messageList = new List<ScriptMessage>();
                foreach (JToken temp in results)
                {
                    ScriptMessage scriptTemp = temp.ToObject<ScriptMessage>();
                    messageList.Add(scriptTemp);
                }

                foreach (ScriptMessage scripTemp in messageList)
                {
                    SendHlMessageToUI(scripTemp);
                }
            }
            catch
            {
            }
        }

        public void SendHlMessageToUI(ScriptMessage inputMessage)
        {
            //作一个随机延时20ms~50ms
            TimeDelay();
            //添加时间戳
            inputMessage.time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            string msg = JsonConvert.SerializeObject(inputMessage);
            PublishHelper.PublishMsg("HlSignalMsg", msg);
            Console.WriteLine("time is {0}", inputMessage.time);

        }

        private int TimeDelay()
        {
            int delayValue = 0;
            Random random = new Random();
            delayValue = random.Next(20, 50);        
            Thread.Sleep(delayValue);
            return delayValue;
        }
    }
}
