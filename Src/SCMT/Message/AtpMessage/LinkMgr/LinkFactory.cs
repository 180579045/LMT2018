using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpMessage.LinkMgr
{
    public class LinkFactory
    {
        public static NetElementLinkBase CreateLink(ConnectType ct)
        {
            NetElementLinkBase link = null;
            switch (ct)
            {
                case ConnectType.ATP_DIRECT_LINK:
                    link = new AtpDirectLink();
                    break;
                case ConnectType.ATP_REMOTE_LOG:
                    link = new AtpRemoteLogLink();
                    break;
                case ConnectType.ATP_REMOTE_MSG:
                    link = new AtpRemoteMsgLink();
                    break;
                case ConnectType.OSP:
                    break;
                case ConnectType.LMT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ct), ct, null);
            }

            return link;
        }
    }
}
