/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：ObjTree_Control.cs
// 文件功能描述：对象树节点控制类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using SCMTOperationCore.Elements;
using System.Threading.Tasks;
using System.Windows;

namespace SCMTMainWindow
{
    /// <summary>
    /// 对象树管理类;
    /// </summary>
    class ObjNodeControl
    {
        public NodeB m_NodeB { get; set; }                          // 对应的基站;
        public List<ObjNode> m_NodeList { get; set; }               // 对用的对象树列表(后续会改为以基站为索引的dictionary);
        public List<ObjNode> m_RootNode { get; set; }               // Demo只有一个基站，暂时用来保存根节点;
        private string m_ObjFilePath;                               // 暂时用一下，保存JSON文件路径;

        /// <summary>
        /// 实验程序先假定一个LMT只连接一个基站，在构造函数中直接读取默认JSON文件;
        /// </summary>
        public ObjNodeControl(NodeB node)
        {
            m_ObjFilePath = node.m_ObjTreeDataPath;
            JObject JObj = new JObject();
            try
            {
                using (StreamReader reader = File.OpenText(m_ObjFilePath))
                {
                    JObj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                }

                ObjNode.nodeb = node;
                ParseJObject(JObj);
            }
            catch(Exception e)
            {
                MessageBox.Show("1加载数据库失败;" + e.ToString());
            }
        }

        /// <summary>
        /// 解析JObject到一个容器当中;
        /// </summary>
        /// <param name="obj">从文件中解析出来的JSON对象;</param>
        /// <returns>返回一个保存了所有节点的容器;</returns>
        private void ParseJObject(JObject obj)
        {
            // 筛选出所有节点的ID;
            IEnumerable<int> AllNodes = from nodes in obj.First.Next.First select (int)nodes["ObjID"];
            m_NodeList = new List<ObjNode>();
            int TempCount = 0;

            
            // 遍历所有JSON文件中的节点,并添加进列表中;
            foreach (var iter in AllNodes)
            {
                ObjNode node;
                var ObjParentNodes = (int)obj.First.Next.First[TempCount]["ObjParentID"];
                var name = (string)obj.First.Next.First[TempCount]["ObjName"];
                var TableName = (string)obj.First.Next.First[TempCount]["MibTableName"];
                var version = (string)obj.First.First;

                if ((int)obj.First.Next.First[TempCount]["ChildRenCount"] != 0)
                {
                    node = new ObjTreeNode(iter, ObjParentNodes, version, name, TableName);
                }
                else
                {
                    node = new ObjLeafNode(iter, ObjParentNodes, version, name, TableName);
                }

                m_NodeList.Add(node);
                TempCount++;
            }
            m_RootNode = ArrangeParentage(m_NodeList);

            
            
        }

        /// <summary>
        /// 确认亲子关系;
        /// </summary>
        /// <param name="NodeList"></param>
        /// <returns></returns>
        public static List<ObjNode> ArrangeParentage(List<ObjNode> NodeList)
        {
            List<ObjNode> RootNodeShow = new List<ObjNode>();
            ObjNode Root = new ObjTreeNode(0, 0, "1.0", "基站节点列表",@"/");

            // 遍历所有节点确认亲子关系;
            foreach (ObjNode iter in NodeList)
            {
                if (iter.ObjParentID == 0)
                {
                    Root.Add(iter);
                }
                else
                {
                    foreach (ObjNode iterParent in NodeList)
                    {
                        if (iterParent.ObjID == iter.ObjParentID)
                        {
                            iterParent.Add(iter);
                        }
                    }
                }
            }

            RootNodeShow.Add(Root);

            return RootNodeShow;
        }
    }
}
