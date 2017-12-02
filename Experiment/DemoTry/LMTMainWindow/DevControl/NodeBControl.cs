using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMTMainWindow
{
    class NodeB
    {
        private string IPAddr { get; }
        private string FriendName { get; }
        public string ObjTreeDataPath { get; }

        NodeB(string IPAddr)
        {
            this.IPAddr = IPAddr; 
        }
    }

    class NodeBControl
    {
        public List<NodeB> NodeBList = new List<NodeB>();
        public static void AccessNodeB()
        {

        }
    }
}
