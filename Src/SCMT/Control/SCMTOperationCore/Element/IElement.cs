using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore
{
    interface IElement
    {
        void Connect();
        void DisConnect();

        void ConnectEvent(object sender, EventArgs args);
        void DisConnectEvent(object sender, EventArgs args);
    }
}
