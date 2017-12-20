using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow
{
    class NodeB
    {
        private string m_IPAddr { get; }                 // 基站IP地址
        private string m_FriendName { get; }             // 基站友好名;
        public string m_ObjTreeDataPath { get; }         // 基站对象树数据库数据源;

        public NodeB(string IPAddr)
        {
            this.m_IPAddr = IPAddr;
            this.m_ObjTreeDataPath = @"Data\Tree_Reference2.json";

            // 此后的动作;
            // 1、创建数据库保存路径,并保存到对应的属性成员中;
            // 2、更新用户界面，在界面中添加节点按钮;
            // 3、以管理站接入基站的流程;
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
