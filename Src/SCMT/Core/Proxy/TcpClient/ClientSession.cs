using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TcpClient
{
    public abstract class ClientSession : IClientSession
    {
        protected Socket m_Client { get; set; }
        protected EndPoint m_RemoteEndPoint { get; set; }
        protected int m_ReceiveBufferSize { get; set; }

        public event EventHandler Closed;
        public event EventHandler Connected;

        public ClientSession(EndPoint remoteEndPoint)
        {
            if (remoteEndPoint == null)
                throw new ArgumentNullException("remoteEndPoint");

            m_RemoteEndPoint = remoteEndPoint;
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

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
