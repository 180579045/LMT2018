using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace SuperLMT.Utils
{

    public delegate void UdpCallback(object sender, EventArgs e);


    public class TraceListenMgr
    {
        #region 单例实现
        private static readonly TraceListenMgr g_instance;
        public static TraceListenMgr Instance
        {
            get
            {
                return g_instance;
            }
        }

        private TraceListenMgr()
        {
            
        }
        static TraceListenMgr()
        {
            g_instance = new TraceListenMgr();
        }

        #endregion

        #region 公共函数

        public void AddCallBackInfo(UdpCallback connectReceived)
        {
            _udpCallback += connectReceived;
        }

        public void DelCallBackInfo(UdpCallback connectRecieved)
        {
            _udpCallback -= connectRecieved;
        }

        public void Init()
        {
            Thread thread1 = new Thread(new ThreadStart(ReceiveData));
            thread1.Start();

        }

        public void Close()
        {
            if (!ReferenceEquals(null, _udpClient))
            {
                _udpClient.Close();
            }
        }


        private void ReceiveData()
        {
            //在本机指定的端口接收
            _udpClient = new UdpClient(_port);
            IPEndPoint remote = null;
            //接收从远程主机发送过来的信息；
            while (true)
            {
                try
                {
                    //关闭udpClient时此句会产生异常
                    byte[] bytes = _udpClient.Receive(ref remote);
                    string sourceIp = remote.Address.ToString();
                    UdpInfo curUdp = new UdpInfo();
                    curUdp.Message = bytes;
                    curUdp.SourceIP = sourceIp;
                    //string str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    //TextBoxCallback tx = SetTextBox;
                    if (null != _udpCallback)
                    {
                        EventArgs e = new EventArgs();
                        _udpCallback(curUdp, e);
                    }
                    
                }
                catch
                {
                    //退出循环，结束线程
                    break;
                }
             
            }
        }


        #endregion



        #region 属性
        private event UdpCallback _udpCallback;


        private int _port = 10001;

        private UdpClient _udpClient;

        

        #endregion

    }
}
