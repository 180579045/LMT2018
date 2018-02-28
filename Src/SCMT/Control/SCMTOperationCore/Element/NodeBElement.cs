using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SCMTOperationCore
{
    public class NodeBElement : Element, IElement
    {
        public int m_NodeBTcpPort { get; set; }                         // 对端TCP端口号;
        public int m_SnmpPort { get; set; }                             // 对端Snmp端口号;
        public IPEndPoint m_Point { get; set; }                         // 对端实体;
        private TcpConnection m_connect { get; set; }                   // Tcp连接;

        public NodeBElement()
        {
            m_NodeBTcpPort = 5000;
            m_SnmpPort = 161;
        }

        public void Connect()
        {
            m_connect = new TcpConnection();
            // 接下来要搞这个,调用TcpConnection，而TcpConnection依赖于Core的Client;
            m_connect.CreateConnection(this);
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }

        public void ConnectEvent(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void DisConnectEvent(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
