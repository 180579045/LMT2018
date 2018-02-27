using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore
{
    class TcpConnection : IConnection
    {
        public event EventHandler Connected;                       // 连接成功后处理;
        public event EventHandler DisConnected;                    // 连接断开后处理;

        public void CreateConnection(BaseElement ele, int port)
        {

        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
