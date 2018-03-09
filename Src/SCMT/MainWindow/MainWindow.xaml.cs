/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：MainWindow.xaml.cs
// 文件功能描述：主界面控制类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UICore.Controls.Metro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using SCMTOperationCore.Message.SNMP;
using SCMTOperationCore.Elements;
using SCMTOperationCore.Control;

namespace SCMTMainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static string StrNodeName;
        private List<string> CollectList = new List<string>();
        public NodeBControl NBControler;
        public NodeB node;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;          // 默认全屏模式;
            this.MinWidth = 1366;                                             // 设置一个最小分辨率;
            this.MinHeight = 768;                                             // 设置一个最小分辨率;
            
            InitView();                                                       // 初始化界面;
            RegisterFunction();                                               // 注册功能;
        }

        /// <summary>
        /// 初始化用户界面;
        /// </summary>
        private void InitView()
        {
            NBControler = new NodeBControl();
            
        }

        /// <summary>
        /// 注册所有所需要的基础功能;
        /// </summary>
        private void RegisterFunction()
        {
            //TrapMessage.SetNodify(this.PrintTrap);                            // 注册Trap监听;
        }

        private void AddNodeBPageToWindow()
        {
            
        }


        //______________________________________________________________________主界面动态刷新____

        /// <summary>
        /// 更新对象树模型以及叶节点模型;
        /// </summary>
        /// <param name="ItemsSource">对象树列表</param>
        private void RefreshObj(IList<ObjNode> ItemsSource, Grid NB_Content)
        {
            // 将右侧叶节点容器容器加入到对象树子容器中;
            this.Obj_Root.SubExpender = this.FavLeaf_Lists;

            foreach (ObjNode items in ItemsSource)
            {
                items.TraverseChildren(this.Obj_Root, this.FavLeaf_Lists, 0, this.Content_Comm, this.Content_Base);
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            TrapMessage.RequestStop();                                         // 停止注册的Trap监听;
        }
        
        /// <summary>
        /// 将对象树添加到收藏;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToCollect_Click(object sender, RoutedEventArgs e)
        {
            string StrName = StrNodeName;
            NodeB node = new NodeB("172.27.245.92", "NodeB");
            string cfgFile = node.m_ObjTreeDataPath;
            
            StreamReader reader = File.OpenText(cfgFile);
            JObject JObj = new JObject();
            JObj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            IEnumerable<int> AllNodes = from nodes in JObj.First.Next.First
                                        select (int)nodes["ObjID"];
            int TempCount = 0;
            foreach (var iter in AllNodes)
            {
                var name = (string)JObj.First.Next.First[TempCount]["ObjName"];
                if ((name == StrName))
                {
                    if (JObj.First.Next.First[TempCount]["ObjCollect"] != null)
                    {
                        int ObjIsCollect = (int)JObj.First.Next.First[TempCount]["ObjCollect"];


                        int nn = (int)JObj.First.Next.First[TempCount]["ObjCollect"];
                        JObj.First.Next.First[TempCount]["ObjCollect"] = "1";
                        nn = (int)JObj.First.Next.First[TempCount]["ObjCollect"];
                        break;

                    }
                    else
                    {
                        var ObjNodesId = (int)JObj.First.Next.First[TempCount]["ObjID"];
                        var ObjParentNodesId = (int)JObj.First.Next.First[TempCount]["ObjParentID"];
                        var ObjChildRenCount = (int)JObj.First.Next.First[TempCount]["ChildRenCount"];
                        var ObjNameEn = (string)JObj.First.Next.First[TempCount]["ObjNameEn"];
                        var ObjMibList = (string)JObj.First.Next.First[TempCount]["MIBList"];
                        JObject NewObjNodes = new JObject(new JProperty("ObjID", ObjNodesId),
                            new JProperty("ObjParentID", ObjParentNodesId),
                            new JProperty("ChildRenCount", ObjChildRenCount),
                            new JProperty("ObjName", name),
                            new JProperty("ObjNameEn", ObjNameEn),
                            new JProperty("MIBList", ObjMibList),
                            new JProperty("ObjCollect",1));
                        JObj.First.Next.First[TempCount].Remove();
                        //JObject child = new JObject("ObjCollect", 1);
                        //var collectJason = JObject.Parse(@"""ObjCollect"": 1");
                        //var collectToken = collectJason as JToken;
                        //JObj.First.Next.First[TempCount].AddAfterSelf(new JObject(new JProperty("ObjCollect", 1)));
                        JObj.First.Next.First[TempCount].AddBeforeSelf(NewObjNodes);
                        break;
                    }
                }
                TempCount++;
            }
            reader.Close();
            File.WriteAllText(cfgFile, JsonConvert.SerializeObject(JObj));

        }

        private void Collect_Node_Click(object sender, EventArgs e)
        {
            ObjNode Objnode;
            List<ObjNode> m_NodeList = new List<ObjNode>();
            List<ObjNode> RootNodeShow = new List<ObjNode>();
            ObjNode Root = new ObjTreeNode(0, 0, "1.0", "收藏节点");
            NodeB node = new NodeB("172.27.245.92", "NodeB");
            string cfgFile = node.m_ObjTreeDataPath;
            StreamReader reader = File.OpenText(cfgFile);
            JObject JObj = new JObject();
            JObj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            IEnumerable<int> AllNodes = from nodes in JObj.First.Next.First
                                        select (int)nodes["ObjID"];
            int TempCount = 0;
            foreach (var iter in AllNodes)
            {
                var ObjParentNodes = (int)JObj.First.Next.First[TempCount]["ObjParentID"];
                var name = (string)JObj.First.Next.First[TempCount]["ObjName"];
                var version = (string)JObj.First.First;
                if (JObj.First.Next.First[TempCount]["ObjCollect"] == null)
                {
                    TempCount++;
                    continue;
                }
                int ObjCollect = (int)JObj.First.Next.First[TempCount]["ObjCollect"];


                Objnode = new ObjTreeNode(iter, ObjParentNodes, version, name);
                if (ObjCollect == 1)
                {
                    int index = m_NodeList.IndexOf(Objnode);
                    if (index < 0)
                    {
                        m_NodeList.Add(Objnode);
                    }

                }

                TempCount++;
            }
            ObjNodeControl Ctrl = new ObjNodeControl(node);
            // 遍历所有节点确认亲子关系;
            foreach (ObjNode iter in m_NodeList)
            {
                //Root.Add(iter);
                // 遍历所有节点确认亲子关系;
                foreach (ObjNode iter1 in Ctrl.m_NodeList)
                {
                    if (iter1.ObjID == iter.ObjID)
                    {
                        Root.Add(iter1);
                    }
                    else if (iter1.ObjID > iter.ObjID)
                    {
                        foreach (ObjNode iterParent in Ctrl.m_NodeList)
                        {
                            if (iterParent.ObjID == iter1.ObjParentID)
                            {
                                iterParent.Add(iter1);
                            }
                        }
                    }
                }
            }
            RootNodeShow.Add(Root);

            // 将右侧叶节点容器容器加入到对象树子容器中;
            this.Obj_Collect.Clear();
            this.Obj_Collect.SubExpender = this.FavLeaf_Lists;

            foreach (ObjNode items in RootNodeShow)
            {
                items.TraverseCollectChildren(this.Obj_Collect, this.FavLeaf_Lists, 0, this.Content_Comm, this.Content_Base);
            }
        }

        private void MetroMenuSeparator_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        
        private void AddNodeB_Click(object sender, RoutedEventArgs e)
        {
            AddNodeB.NewInstance(this).Closed += AddNB_Closed;
            AddNodeB.NewInstance(this).ShowDialog();
        }

        private void AddNB_Closed(object sender, EventArgs e)
        {
            if(!(e is NodeBArgs))
            {
                return;
            }
            ObjNodeControl Ctrl = new ObjNodeControl((e as NodeBArgs).m_NodeB);  // 象树树信息;
            node = (e as NodeBArgs).m_NodeB;

            RefreshObj(Ctrl.m_RootNode, this.Content_Comm);                      // 1、更新对象树;
            AddNodeBPageToWindow();                                              // 2、将所有基站添加到窗口页签中;
            if (node != null)
            {
                node.Connect();                                                  // 3、连接基站(第一个版本，暂时只连接一个基站);
            }
        }
    }
}
