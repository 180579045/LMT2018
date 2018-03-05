using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Elements
{
    class GtsaElement : Element, IElement
    {
        public int m_GtsaPort { get; set; }

        public void Connect()
        {
            throw new NotImplementedException();
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
