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

    public class TcpClientSession : ClientSession
    {
        public ConnectedCallback m_Connectedcallback;

        public TcpClientSession(EndPoint remoteEndPoint) : base(remoteEndPoint)
        {
        }

        /// <summary>
        /// 建立连接;
        /// </summary>
        public override void Connect()
        {
            // 注册连接结果的回调函数;
            var e = CreateSocketAsyncEventArgs(m_RemoteEndPoint, m_Connectedcallback, 0);
            var socket = new Socket(m_RemoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            socket.ConnectAsync(e);
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

        private static void SocketAsyncEventCompleted(object sender, SocketAsyncEventArgs e)
        {
            e.Completed -= SocketAsyncEventCompleted;
            var token = (ConnectToken)e.UserToken;
            e.UserToken = null;
            token.Callback(sender as Socket, token.State, e);
        }
    }
}

