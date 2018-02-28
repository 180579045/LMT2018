using System;
using System.Net;
using Client;
using System.Net.Sockets;

namespace SCMTOperationCore
{
    /// <summary>
    /// 负责Tcp连接的夹层业务流程;
    /// </summary>
    class TcpConnection : IConnection
    {
        public event EventHandler Connected;                       // 连接成功后处理;
        public event EventHandler DisConnected;                    // 连接断开后处理;

        private TcpClientSession m_TcpSession { get; set; }        // Tcp连接会话;
        private const string m_RequestTemplate = "CONNECT {0}:{1} HTTP/1.1\r\nHost: {0}:{1}\r\nProxy-Connection: Keep-Alive\r\n\r\n";

        public TcpConnection()
        {
        }

        public void CreateConnection(Element ele, int port)
        {

        }

        /// <summary>
        /// 创建连接;
        /// </summary>
        /// <param name="ele">需要连接的对端网元</param>
        public void CreateConnection(Element ele)
        {
            ele.m_ConnectionState = ConnectionState.Connecting;
            (ele as NodeBElement).m_Point = new IPEndPoint(ele.m_IPAddress, (ele as NodeBElement).m_NodeBTcpPort);
            m_TcpSession = new TcpClientSession((ele as NodeBElement).m_Point);
            m_TcpSession.Connected += M_TcpSession_Connected;
            m_TcpSession.m_Connectedcallback += ProcessConnect;
            m_TcpSession.Connect();
        }

        private void M_TcpSession_Connected(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void ConnectEvent(object sender, EventArgs arg)
        {

        }

        private void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e)
        {
            string request = "";
            
            if (e == null)
            {
                Console.WriteLine("e is null");
                return;
            }

            if (socket == null)
            {
                Console.WriteLine("socket is null");
                return;
            }
            
            Console.WriteLine("Receive Socket content" +request);


        }
    }
}
