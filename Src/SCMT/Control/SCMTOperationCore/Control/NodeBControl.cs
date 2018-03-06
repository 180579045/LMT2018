using System;
using SCMTOperationCore.Elements;
using System.Collections.Generic;

namespace SCMTOperationCore.Control
{
    public class NodeBControl : ElementControl
    {
        public static List<NodeB> m_NodeBList { get; set; } 
        public NodeBControl()
        {

        }

        public void AddElement(Element ele)
        {
            //m_NodeBList.Add(ele as NodeB);
        }
        
    }
}
