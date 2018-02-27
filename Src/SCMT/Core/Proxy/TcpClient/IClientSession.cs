using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient
{
    public interface IClientSession
    {
        void Connect();
        void Send(byte[] data, int offset, int length);
        void Send(IList<ArraySegment<byte>> segments);
        void Close();
        event EventHandler Connected;
        event EventHandler Closed;
    }
}
