using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Element
{
    class GtsaElement : BaseElement, IElement
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
    }
}
