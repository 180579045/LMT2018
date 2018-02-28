using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    /// <summary>
    /// 客户端连接抽象类;
    /// </summary>
    public abstract class ClientSession : IClientSession
    {
        protected Socket m_Client { get; set; }                    // Socket连接;
        protected EndPoint m_RemoteEndPoint { get; set; }          // 对端;
        protected int m_ReceiveBufferSize { get; set; }            // 接收缓冲区大小;

        public event EventHandler Closed;
        public event EventHandler Connected;

        public ClientSession(EndPoint remoteEndPoint)
        {
            if (remoteEndPoint == null)
                throw new ArgumentNullException("remoteEndPoint");

            m_RemoteEndPoint = remoteEndPoint;
        }

        public abstract void Close();

        public abstract void Connect();

        public void Send(IList<ArraySegment<byte>> segments)
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] data, int offset, int length)
        {
            throw new NotImplementedException();
        }
    }
}
