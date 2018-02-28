using System;
using System.Net;
using Client;

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

        public TcpConnection()
        {
        }

        public void CreateConnection(Element ele, int port)
        {

        }

        public void CreateConnection(Element ele)
        {
            ele.m_ConnectionState = ConnectionState.Connecting;
            m_TcpSession = new TcpClientSession((ele as NodeBElement).m_Point);
            m_TcpSession.Connect();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
