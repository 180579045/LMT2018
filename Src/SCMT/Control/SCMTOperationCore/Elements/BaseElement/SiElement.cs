using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SCMTOperationCore.Elements
{
    /// <summary>
    /// Si元素的基类,可拓展诸如NodeB这类的基于SI消息的网元;
    /// </summary>
    public class SiElement : Element, IElement
    {
        public int m_NodeBTcpPort { get; set; }                         // 对端TCP端口号;
        public int m_SnmpPort { get; set; }                             // 对端Snmp端口号;
        public IPEndPoint m_Point { get; set; }                         // 对端实体;
        public ConnectionState m_IsConnected                            // 获取连接状态;
        {
            get { return m_connect.m_ConnectionState; }
        }
        private TcpConnection m_connect { get; set; }                   // Tcp连接;

        public SiElement()
        {
            m_NodeBTcpPort = 5000;
            m_SnmpPort = 161;
        }

        public void Connect()
        {
            m_connect = new TcpConnection();
            m_connect.CreateConnection(this);                           // 建立连接;
            m_connect.Connected += M_connect_Connected;
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }
        
        private void M_connect_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Element Receive" + (e as SiArgs).a);
        }
    }

    public class SiArgs : EventArgs
    {
        public int a;
        int b;
    }
}
