using System;
using System.Net;
using Client;
using System.Net.Sockets;
using System.IO;

namespace SCMTOperationCore
{
    /// <summary>
    /// 负责Tcp连接的夹层业务流程;
    /// </summary>
    class TcpConnection : IConnection
    {
        public event EventHandler Connected;                       // 连接成功后处理;
        public event EventHandler DisConnected;                    // 连接断开后处理;
        public ConnectionState m_ConnectionState { get; set; }     // 当前连接状态; 

        private TcpClientSession m_TcpSession { get; set; }        // Tcp连接会话;
        private const string m_RequestTemplate = "Connection";
        private Stream m_Stream { get; set; }

        public TcpConnection()
        {
        }

        /// <summary>
        /// 创建连接;
        /// </summary>
        /// <param name="ele">需要连接的对端网元</param>
        public void CreateConnection(Element ele)
        {
            ele.m_ConnectionState = ConnectionState.Connecting;
            (ele as SiElement).m_Point = new IPEndPoint(ele.m_IPAddress, (ele as SiElement).m_NodeBTcpPort);

            m_TcpSession = new TcpClientSession((ele as SiElement).m_Point);
            m_TcpSession.m_Connectedcallback += ProcessConnect;
            m_TcpSession.DataReceived += M_TcpSession_DataReceived;
            m_TcpSession.Connect();
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

            m_ConnectionState = ConnectionState.Connencted;
            m_Stream = new NetworkStream(socket); 

            Console.WriteLine("Receive Socket content" +request);
            
        }

        private void M_TcpSession_DataReceived(object sender, DataEventArgs e)
        {
            Console.WriteLine("Receive Data");
        }


    }
}
