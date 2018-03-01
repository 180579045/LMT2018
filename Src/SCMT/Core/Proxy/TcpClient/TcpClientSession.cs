using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    public delegate void ConnectedCallback(Socket socket, object state, SocketAsyncEventArgs e);

    class ConnectToken
    {
        public object State { get; set; }

        public ConnectedCallback Callback { get; set; }
    }

    public class DataEventArgs : EventArgs
    {
        public byte[] Data { get; set; }

        public int Offset { get; set; }

        public int Length { get; set; }
    }

    public class TcpClientSession : ClientSession
    {
        private EventHandler<DataEventArgs> m_DataReceived;
        private DataEventArgs m_DataArgs = new DataEventArgs();

        public event EventHandler<DataEventArgs> DataReceived
        {
            add { m_DataReceived += value; }
            remove { m_DataReceived -= value; }
        }

        public TcpClientSession(EndPoint remoteEndPoint) : base(remoteEndPoint)
        {
        }

        /// <summary>
        /// 建立连接;
        /// </summary>
        public override void Connect()
        {
            // 注册连接结果的回调函数;
            var SocketArg = CreateSocketAsyncEventArgs(m_RemoteEndPoint, m_Connectedcallback, 0);
            var socket = new Socket(m_RemoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            
            socket.ConnectAsync(SocketArg);
        }
        
        /// <summary>
        /// 关闭连接;
        /// </summary>
        public override void Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建Socket回调参数;
        /// </summary>
        /// <param name="remoteEndPoint"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        static SocketAsyncEventArgs CreateSocketAsyncEventArgs(EndPoint remoteEndPoint, ConnectedCallback callback, object state)
        {
            var e = new SocketAsyncEventArgs();

            e.UserToken = new ConnectToken
            {
                State = state,
                Callback = callback
            };

            e.RemoteEndPoint = remoteEndPoint;
            e.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEventCompleted);

            return e;
        }

        /// <summary>
        /// Socket建链后的处理;
        /// 1、开始接收消息;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SocketAsyncEventCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                return;
            }
            
            OnDataReceived(e.Buffer, e.Offset, e.BytesTransferred);
            
            var token = (ConnectToken)e.UserToken;
            e.UserToken = null;
            
            token.Callback(sender as Socket, token.State, e);

        }

        private void OnDataReceived(byte[] data, int offset, int length)
        {
            var handler = m_DataReceived;
            if (handler == null)
                return;

            m_DataArgs.Data = data;
            m_DataArgs.Offset = offset;
            m_DataArgs.Length = length;

            handler(this, m_DataArgs);
        }
    }
}

