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
using System.Xml.Linq;
using SCMTOperationCore.Message.SNMP;

namespace SCMTMainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static string StrNodeName;
        private List<string> CollectList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            InitView();                                                       // 初始化界面;
            RegisterFunction();                                               // 注册功能;
        }

        /// <summary>
        /// 初始化用户界面;
        /// Demo阶段，先假设只连接一个基站;
        /// </summary>
        private void InitView()
        {
            this.WindowState = System.Windows.WindowState.Maximized;          // 默认全屏模式;
            this.MinWidth = 1366;                                             // 设置一个最小分辨率;
            this.MinHeight = 768;                                             // 设置一个最小分辨率;
            NodeB node = new NodeB("172.27.245.92");                          // 初始化一个基站节点(第一个版本,暂时只连接一个基站);
            ObjNodeControl Ctrl = new ObjNodeControl(node);                   // 从JSON文件中初始化一个对象树;
            this.RefreshObj(Ctrl.m_RootNode, this.Content_Comm);              // 将对象树加入到Expender容器中

        }

        /// <summary>
        /// 注册所有所需要的基础功能;
        /// </summary>
        private void RegisterFunction()
        {
            //TrapMessage.SetNodify(this.PrintTrap);                            // 注册Trap监听;
        }

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

        //                                                                                     以下为Demo处理;
        // ---------------------------------------------------------------------------------------------------
        
        private void AddToCollect_Click(object sender, RoutedEventArgs e)
        {
            string StrName = StrNodeName;
            //string StrNameExit = CollectList.Find(o => o == StrName);
            //if (StrNameExit == null)
            //{
            //    CollectList.Add(StrName);
            //}

            //bool bcollect = false;
            NodeB node = new NodeB("172.27.245.92");
            string cfgFile = node.m_ObjTreeDataPath;
            //JsonSerializer serialiser = new JsonSerializer();
            //string newContent = string.Empty;
            //string qwe = File.ReadAllText(cfgFile);
            //using (StreamReader reader = new StreamReader(cfgFile))
            //{
            //    string json = reader.ReadToEnd();

            //    dynamic jsonObj = JsonConvert.DeserializeObject(json);
            //    int nn = (int)jsonObj["NodeList"][11]["ChildRenCount"];
            //    jsonObj["NodeList"][11]["ChildRenCount"] = "3";
            //    nn = (int)jsonObj["NodeList"][11]["ChildRenCount"];
            //    //File.WriteAllText(cfgFile, JsonConvert.SerializeObject(jsonObj));
            //    reader.Close();
            //    string fdsf = JsonConvert.SerializeObject(jsonObj);
            //    File.WriteAllText(cfgFile, fdsf, Encoding.UTF8);
            //}
            
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
                        //JObject child = new JObject("ObjCollect", 1);
                        //var collectJason = JObject.Parse(@"""ObjCollect"": 1");
                        //var collectToken = collectJason as JToken;
                        JObj.First.Next.First[TempCount].AddAfterSelf(new JObject(new JProperty("ObjCollect", 1)));
                        //int nn = (int)JObj.First.Next.First[TempCount]["ObjCollect"];
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
            //this.FavLeaf_Lists.Children.Clear();
            //foreach (string iter in CollectList)
            //{
            //    MetroExpander Easy = new MetroExpander();
            //    Easy.Header = iter;
            //    this.FavLeaf_Lists.Children.Add(Easy);
            //}
            //this.FavLeaf_Lists.Children.Clear();

            ObjNode Objnode;
            List<ObjNode> m_NodeList = new List<ObjNode>();
            List<ObjNode> RootNodeShow = new List<ObjNode>();
            ObjNode Root = new ObjTreeNode(0, 0, "1.0", "收藏节点");
            NodeB node = new NodeB("172.27.245.92");
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
                //if ((int)JObj.First.Next.First[TempCount]["ChildRenCount"] != 0)
                //{
                //    Objnode = new ObjTreeNode(iter, ObjParentNodes, version, name);
                //}
                //else
                //{
                //    Objnode = new ObjLeafNode(iter, ObjParentNodes, version, name);
                //}
                if (ObjCollect == 1)
                {
                    int index = m_NodeList.IndexOf(Objnode);
                    if (index < 0)
                    //int opper = m_NodeList.BinarySearch(Objnode);
                    //ObjNode iuy = m_NodeList.Find(t => t.Equals(Objnode));
                    //if (opper != 0)
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

    }
}
