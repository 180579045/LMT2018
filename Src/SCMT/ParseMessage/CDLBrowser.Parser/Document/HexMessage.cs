using SuperLMT.Utils.CustomUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLBrowser.Parser.Document
{
    public class HexMessage
    {
        private SelectedRange heMemoryViewSelectedRage;
        private byte[] heMemoryViewContext;
        public SelectedRange HeMemoryViewSelectedRage
        {
            get
            {
                return heMemoryViewSelectedRage;
            }
            set
            {
                this.heMemoryViewSelectedRage = value;
            }
        }
        public byte[] HeMemoryViewContext
        {
            get
            {
                return heMemoryViewContext;
            }
            set
            {
                this.heMemoryViewContext = value;

            }
        }
    }
}
