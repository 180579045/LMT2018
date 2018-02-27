using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AtpMessage
{
    public delegate void UpdateMsg(AtpMessageInfo arg);
    

    public class AtpMessageInfo
    {
        public static Thread SendAtpMessage = new Thread(StartTimer);
        private static int Num = 0;
        public static UpdateMsg func;
        private static System.Threading.Timer timer;
        public string No
        {
            get;
            set;
        }
        public string Time
        {
            get;
            set;
        }
        public string SourceID
        {
            get;
            set;
        }
        public string OpCode
        {
            get;
            set;
        }
        public string DestID
        {
            get;
            set;
        }
        public string Length
        {
            get;
            set;
        }

        AtpMessageInfo(string No, string Time, string SourceID, string OpCode, string DestID, string Length)
        {
            this.No = No;
            this.Time = Time;
            this.SourceID = SourceID;
            this.OpCode = OpCode;
            this.DestID = DestID;
            this.Length = Length;
        }

        static private void StartTimer()
        {
            //启动1s定时器
            timer = new System.Threading.Timer(
                new System.Threading.TimerCallback(SimSendMessage), null, 0, 1000);
        }

        static private void SimSendMessage(object value)
        {
            //模拟1s发送一条消息
            //后期应该是监听基站的消息
            Random rd = new Random();
            
            AtpMessageInfo tempMessage = new AtpMessageInfo(Num.ToString(),
                DateTime.Now.ToString(), "0:0:1:0:70", rd.Next(1, 2000).ToString(), "0:0:4:0:72", rd.Next(16,100).ToString());
            Num++;
            func(tempMessage);
        }

        static public void StartThread()
        {
            //应该是需要判断跟踪开关打开则启动线程，暂时未做
            AtpMessageInfo.SendAtpMessage.Start();
        }

        static public void RequestStop()
        {
            timer.Dispose();
        }

    }
}
