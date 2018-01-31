using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow
{
    class UEMessage
    {
        public string MsgTime { get; set; }
        public string MsgContent { get; set; }
        public UEMessage(string time, string content)
        {
            MsgTime = time;
            MsgContent = content;
        }
    }
}
