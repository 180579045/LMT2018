using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperLMT.Utils
{
    public class UdpInfo
    {
        #region 属性
        private byte[] _message;
        public byte[] Message
        {
            get { return _message; }
            set { _message = value; }
        }


        private string _sourceIp;
        public string SourceIP
        {
            get { return _sourceIp; }
            set { _sourceIp = value; }
        }

#endregion
    }
}
