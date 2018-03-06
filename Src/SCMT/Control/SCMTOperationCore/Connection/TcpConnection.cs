using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using SCMTOperationCore.Elements;

namespace SCMTOperationCore
{
    /// <summary>
    /// 负责Tcp连接的夹层业务流程的类型;
    /// </summary>
    class TcpConnection : IConnection
    {
        public event EventHandler Connected;                       // 连接成功后处理;
        public event EventHandler DisConnected;                    // 连接断开后处理;
        public ConnectionState m_ConnectionState { get; set; }     // 当前连接状态; 

        private TcpClient m_TcpSession { get; set; }               // Tcp连接会话;

        public TcpConnection()
        {
        }

        /// <summary>
        /// 根据一个网元实例创建连接;
        /// </summary>
        /// <param name="ele">需要连接的对端网元</param>
        public void CreateConnection(Element ele)
        {
            SiElement TcpEle = ele as SiElement;
            
            m_TcpSession = new TcpClient();
            m_TcpSession.BeginConnect(TcpEle.m_IPAddress, TcpEle.m_NodeBTcpPort, ConnectEvent, TcpEle);

            // 此处涉及链接业务需求分析,比如建立Tcp连接不成功，尝试重连几次等等;
        }
        
        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        private void ConnectEvent(IAsyncResult ar)
        {
            SiElement RecState = ar.AsyncState as SiElement;
            Console.WriteLine("Receive Data From:" + RecState.m_IPAddress.ToString());
            
            SiArgs args = new SiArgs();
            args.a = 10;
            RecState.m_ConnectionState = ConnectionState.Connencted;
            this.Connected(this, args);
        }

        

    }
}
