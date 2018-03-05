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
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;

using Snmp_dll;
using UICore.Controls.Metro;
using Specialized3DChart;
using System.Windows.Threading;
using DT.Tools.FlowChart;
using AtpMessage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SCMTMainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ChartStyle2D cs;
        private DataSeriesSurface ds;
        private Draw3DChart d3c;

        private List<BasicMessage> lb = new List<BasicMessage>();
        private List<String> ne = new List<String>();
        private DispatcherTimer timer = new DispatcherTimer();
        private int i = 0;

        public static string StrNodeName;
        private List<string> CollectList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            InitView();                                                       // 初始化界面;
            RegisterFunction();                                               // 注册功能;
            InitDemoData();                                                   // 更新主界面Demo数据;
            AtpMessageInfo.func = recvMsg;
            AtpMessageInfo.SendAtpMessage.Start();

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
            NodeB node = new NodeB("172.27.245.92");                          // 初始化一个基站节点(Demo程序,暂时只连接一个基站);
            ObjNodeControl Ctrl = new ObjNodeControl(node);                   // 从JSON文件中初始化一个对象树;
            this.RefreshObj(Ctrl.m_RootNode,                                  // 将对象树加入到Expender容器中
                            this.Content_Comm, this.Content_Base);
        }

        /// <summary>
        /// 注册所有所需要的基础功能;
        /// </summary>
        private void RegisterFunction()
        {
            TrapMessage.SetNodify(this.PrintTrap);                            // 注册Trap监听;
        }

        /// <summary>
        /// 更新对象树模型以及叶节点模型;
        /// </summary>
        /// <param name="ItemsSource">对象树列表</param>
        private void RefreshObj(IList<ObjNode> ItemsSource, Grid NB_Content, MetroScrollViewer NB_Base)
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
            TrapMessage.RequestStop();
            AtpMessageInfo.RequestStop();
        }

        //                                                                                     以下为Demo处理;
        // ---------------------------------------------------------------------------------------------------

        private void InitDemoData()
        {
            // 添加Demo数据;
            ObservableCollection<AlarmGrid> custdata = AlarmGrid.GetData();   // 初始化基本信息中的告警信息;
            DG1.DataContext = custdata;                                       // 将告警信息加入控件;

            ObservableCollection<DataGrid> custdata2 = DataGrid.GetData();   // 初始化基本信息中的告警信息;
            Content_NB.DataContext = custdata2;
        }

        private void PrintTrap(List<string> TrapMsg)
        {
            Dispatcher.Invoke(new Action(
                delegate
                {
                    foreach (string content in TrapMsg)
                        Console.WriteLine("Trap Content is" + content);
                }));
        }

        private void MetroExpander_Click_BaseInfo(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Visible;
            Content_Comm.Visibility = Visibility.Hidden;
        }

        private void UEList_Click(object sender, RoutedEventArgs e)
        {
            UEList UEWindow = new UEList();
            UEWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            UEWindow.Show();
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartCanvas.Width = chartGrid.ActualWidth;
            chartCanvas.Height = chartGrid.ActualHeight;
            AddChart();
        }

        private void AddChart()
        {
            chartCanvas.Children.Clear();
            cs = new ChartStyle2D();
            cs.ChartCanvas = this.chartCanvas;
            cs.GridlinePattern = Specialized3DChart.ChartStyle.GridlinePatternEnum.Solid;
            cs.IsColorBar = true;
            cs.Title = "No Title";
            ds = new DataSeriesSurface();
            ds.LineColor = Brushes.Black;
            Utility.Peak3D(cs, ds);

            d3c = new Draw3DChart();
            d3c.Colormap.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Jet;
            d3c.ChartType = Draw3DChart.ChartTypeEnum.SurfaceFillContour3D;
            d3c.IsLineColorMatch = false;
            d3c.NumberContours = 15;
            d3c.AddChart(cs, ds);
        }

        private void SimFile1_Click(object sender, RoutedEventArgs e)
        {
            Simulation_Download Sd1 = new Simulation_Download();
            Sd1.Show();
        }

        private void DemoSCTF_Checked(object sender, RoutedEventArgs e)
        {
            Message_Setter Ms = new Message_Setter();
            Ms.Show();
        }

        private void Easy_NB_Click(object sender, EventArgs e)
        {
            this.FavLeaf_Lists.Children.Clear();
            MetroExpander Easy1 = new MetroExpander();
            MetroExpander Easy2 = new MetroExpander();
            MetroExpander Easy3 = new MetroExpander();
            MetroExpander Easy4 = new MetroExpander();
            MetroExpander Easy5 = new MetroExpander();
            MetroExpander Easy6 = new MetroExpander();

            Easy1.Header = "链路查询";
            Easy2.Header = "时钟查询";
            Easy3.Header = "LTE基带资源";
            Easy4.Header = "射频资源";
            Easy5.Header = "光模块";
            Easy6.Header = "LTE小区信息";

            this.FavLeaf_Lists.Children.Add(Easy1);
            this.FavLeaf_Lists.Children.Add(Easy2);
            this.FavLeaf_Lists.Children.Add(Easy3);
            this.FavLeaf_Lists.Children.Add(Easy4);
            this.FavLeaf_Lists.Children.Add(Easy5);
            this.FavLeaf_Lists.Children.Add(Easy6);

        }

        private void MainWindow_Cell_Click(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Hidden;
            Content_Comm.Visibility = Visibility.Visible;
        }

        private void PrintTrapInConsole(List<string> Trapinfo)
        {
            foreach (string prt in Trapinfo)
            {
                Console.WriteLine(prt);
            }
        }

        private void MetroTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Click_Flow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ne.Add("UE");
                ne.Add("eNB");
                ne.Add("eNBjd");
                ne.Add("MME");
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(AnimatedPlot);
                timer.IsEnabled = true;
            }
        }

        private void AnimatedPlot(object sender, EventArgs e)
        {
            BasicMessage bm = new BasicMessage();
            bm.Name = "GOOF" + Convert.ToString(i);
            bm.OID = i;
            bm.ParserId = i;
            bm.DestinationElement = "UE";
            bm.SourceElement = "eNB";
            bm.TimeStamp = "0000000";
            if (lb.Count >= 50)
            {
                lb.RemoveAt(0);
            }
            lb.Add(bm);
            this.drawingCanvas.Clean();

            this.drawingCanvas.DrawingFlowChart(ne, lb);
            i++;
        }

        private void MetroTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            ne.Add("UE");
            ne.Add("eNB");
            ne.Add("eNBjd");
            ne.Add("MME");
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.IsEnabled = true;
        }

        private void MetroTabItem_LostFocus(object sender, RoutedEventArgs e)
        {
            ne.Clear();
            lb.Clear();
            timer.IsEnabled = false;
            i = 0;
            this.drawingCanvas.Clean();
        }

        private void MetroTabItem_GotFocus_1(object sender, RoutedEventArgs e)
        {
            this.dynamicChar.drawingDynamicChart();
        }

        public void recvMsg(AtpMessageInfo arg)
        {
            this.ATP_MSG_GRID.Dispatcher.Invoke(
                new Action(
                    delegate
                    {
                        if (this.ATP_MSG_GRID.Items.Count > 25)
                        {
                            //限制显示行数，后期作优化:最新的显示在上面;
                            this.ATP_MSG_GRID.Items.RemoveAt(0);
                        }
                        // 回填到控件中;
                        this.ATP_MSG_GRID.Items.Add(arg);

                    }
                    )
                );
        }

        private void EventShowAtpMsgContent(object sender, RoutedEventArgs e)
        {

            this.TextBlockAtpContent.Text = DateTime.Now.ToString();

        }
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
