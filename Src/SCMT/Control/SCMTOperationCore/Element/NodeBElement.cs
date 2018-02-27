using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Element
{
    class NodeBElement : BaseElement, IElement
    {
        public int m_NodeBTcpPort { get; set; }                         // 对端TCP端口号;

        public void Connect()
        {
            // 接下来要搞这个,调用TcpConnection，而TcpConnection依赖于Core的Client;
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }
    }
}
