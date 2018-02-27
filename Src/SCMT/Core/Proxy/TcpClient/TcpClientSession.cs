using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient
{
    class TcpClientSession : ClientSession
    {
        public TcpClientSession(EndPoint remoteEndPoint) : base(remoteEndPoint)
        {
        }
    }
}
