using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LMTMainWindow
{
    /// <summary>
    /// 对象树管理类;
    /// </summary>
    class ObjNodeControl
    {
        public NodeB NodeB_ID { get; set; }
        public List<ObjNode> NodeList { get; set; }
        private JObject JObj = new JObject();
        private string ObjFilePath;

        ObjNodeControl()
        {
            ObjFilePath = @"Data\Tree_Reference2.json";
        }

        /// <summary>
        /// 读取JSON文件;
        /// </summary>
        public void LoadFile()
        {
            using (StreamReader reader = File.OpenText(ObjFilePath))
            {
                JObj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
            NodeList = ParseJObject(JObj);
        }

        /// <summary>
        /// 解析JObject到一个容器当中;
        /// </summary>
        /// <param name="obj">从文件中解析出来的JSON对象;</param>
        /// <returns>返回一个保存了所有节点的容器;</returns>
        public List<ObjNode> ParseJObject(JObject obj)
        {
            NodeList = JsonConvert.DeserializeObject<ObjNodeControl>(File.ReadAllText(ObjFilePath));

            using (StreamReader file = File.OpenText(ObjFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                NodeList = (ObjNodeControl)serializer.Deserialize(file, typeof(NodeList));
            }
            return NodeList;
        }
    }

    /// <summary>
    /// 对象树节点;
    /// </summary>
    class ObjNode
    {
        public string version { get; set; }                    // 对象树版本号;
        public int ObjID { get; set; }                         // 节点ID;
        public int ObjParentID { get; set; }                   // 父节点ID;
        public string ObjName { get; set; }                    // 节点名称;
        //public List<string> OIDList { get; set; }            // 节点包含OID列表;
        //public List<MibCommand> MibCmdLsit { get; set; }     // 节点包含命令列表;
    }
    /// <summary>
    /// 对象树枝节点;
    /// </summary>
    class ObjTreeNode : ObjNode, Component
    {
        ObjTreeNode(int id, int pid)
        {
            this.ObjID = id;
            this.ObjParentID = pid;
        }

        public List<Component> SubObj_Lsit = new List<Component>();

        public void Add(Component obj)
        {
            SubObj_Lsit.Add(obj);
        }

        public void Remove(Component obj)
        {
            SubObj_Lsit.Remove(obj);
        }

        public void TraversetoList(int depth)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 对象树叶子节点;
    /// </summary>
    class ObjLeafNode : ObjNode, Component
    {
        ObjLeafNode(int id, int pid)
        {
            this.ObjID = id;
            this.ObjParentID = pid;
        }

        public void Add(Component obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(Component obj)
        {
            throw new NotImplementedException();
        }

        public void TraversetoList(int depth)
        {
            throw new NotImplementedException();
        }
    }
}
    
}
