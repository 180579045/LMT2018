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
using CefSharp.Wpf;
using Xceed.Wpf.AvalonDock.Layout;
using SCMTMainWindow.Component.View;
using Microsoft.Win32;
using System.Data;
using CDLBrowser.Parser.Document.Event;
using SuperLMT.Utils;
using CDLBrowser.Parser.DatabaseMgr;
using CDLBrowser.Parser.Document;
using System.Data.SQLite;
using System.Windows.Input;

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
        bool bIsRepeat;
        
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;          // 默认全屏模式;
            this.MinWidth = 1024;                                             // 设置一个最小分辨率;
            this.MinHeight = 768;                                             // 设置一个最小分辨率;
            this.MainHorizenTab.SelectionChanged += 
                MainHorizenTab_SelectionChanged;                              // Tab选择改变后的事件;

            InitView();                                                       // 初始化界面;
            RegisterFunction();                                               // 注册功能;
            CefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //this.LineChar1.RegisterJsObject("JsObj", new CallbackObjectForJs());
            deleteTempFile();
        }

        private void MainHorizenTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
        private void RefreshObj(IList<ObjNode> ItemsSource)
        {
            // 将右侧叶节点容器容器加入到对象树子容器中;
            this.Obj_Root.SubExpender = this.FavLeaf_Lists;

            foreach (ObjNode items in ItemsSource)
            {
                items.TraverseChildren(this.Obj_Root, this.FavLeaf_Lists, 0);
            }
        }

        /// <summary>
        /// 窗口关闭;
        /// 关闭先前注册的服务;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        JObj.First.Next.First[TempCount].AddBeforeSelf(NewObjNodes);
                        break;
                    }
                }
                TempCount++;
            }
            reader.Close();
            FileStream fs = new FileStream(cfgFile, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(JObj);
            sw.Close();
            //File.WriteAllText(cfgFile, JsonConvert.SerializeObject(JObj));

        }

        private void IsRepeatNode(ObjNode iter, List<ObjNode> listIter)
        {
            foreach(ObjNode iterListSub in listIter)
            {
                if(iter.ObjName == iterListSub.ObjName)
                {
                    bIsRepeat = true;
                }
                else
                {
                    if(iterListSub.SubObj_Lsit != null)
                    {
                        IsRepeatNode(iter, iterListSub.SubObj_Lsit);
                    }
                }
            }
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
            reader.Close();
            ObjNodeControl Ctrl = new ObjNodeControl(node);
            // 遍历所有节点确认亲子关系;
            foreach (ObjNode iter in m_NodeList)
            {
                //Root.Add(iter);
                if (Root.SubObj_Lsit != null)
                {
                    bIsRepeat = false;
                    IsRepeatNode(iter, Root.SubObj_Lsit);
                    if (bIsRepeat)
                    {
                        continue;
                    }
                }

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
                items.TraverseCollectChildren(this.Obj_Collect, this.FavLeaf_Lists, 0);
            }
        }
        
        /// <summary>
        /// 添加基站;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNodeB_Click(object sender, RoutedEventArgs e)
        {
            AddNodeB.NewInstance(this).Closed += AddNB_Closed;
            AddNodeB.NewInstance(this).ShowDialog();
        }

        /// <summary>
        /// 当窗口关闭得时候进行得处理;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNB_Closed(object sender, EventArgs e)
        {
            // 如果参数为空，则表示没有基站;
            if(!(e is NodeBArgs))
            {
                return;
            }
            ObjNodeControl Ctrl = new ObjNodeControl((e as NodeBArgs).m_NodeB);  // 象树树信息;
            node = (e as NodeBArgs).m_NodeB;

            RefreshObj(Ctrl.m_RootNode);                                         // 1、更新对象树;
            AddNodeBPageToWindow();                                              // 2、将所有基站添加到窗口页签中;
            if (node != null)
            {
                node.Connect();                                                  // 3、连接基站(第一个版本，暂时只连接一个基站);
            }
        }

        private void Show_LineChart(object sender, EventArgs e)
        {
            // 后续需要有一个界面元素管理类;
            LayoutAnchorable sub = new LayoutAnchorable();
            LinechartContent content = new LinechartContent();

            // 当前的问题：这个Title显示不出来;
            sub.Title = "折线图";
            sub.FloatingHeight = 280;
            sub.FloatingWidth = 350;
            sub.Content = content;
            sub.FloatingLeft = 200;
            sub.FloatingTop = 200;
            sub.CanClose = true;
            sub.CanAutoHide = false;

            this.Pane.Children.Add(sub);
            sub.Float();

            //Add by Mayi
            //实现鼠标的拖拽，这里通过一个简单的TreeView 做个演示
            TreeView myTree = new TreeView();

            var originStyle = myTree.Style;
            var newStyle = new Style();
            newStyle.BasedOn = originStyle;

            //为鼠标移动添加事件
            newStyle.Setters.Add(new EventSetter(MouseMoveEvent, new MouseEventHandler(TreeViewItem_MouseMove)));
            myTree.ItemContainerStyle = newStyle;

            //构造一个简单的TreeView
            TreeViewItem RootItem1 = new TreeViewItem();
            RootItem1.Header = "RootItem1";
            myTree.Items.Add(RootItem1);

            TreeViewItem SubItem1 = new TreeViewItem();
            SubItem1.Header = "SubItem1";
            RootItem1.Items.Add(SubItem1);

            TreeViewItem SubSubItem1 = new TreeViewItem();
            SubSubItem1.Header = "SubSubItem";
            SubItem1.Items.Add(SubSubItem1);

            TreeViewItem RootItem2 = new TreeViewItem();
            RootItem2.Header = "RootItem2";
            myTree.Items.Add(RootItem2);

            //显示到主界面
            this.FavLeaf_Lists.Children.Add(myTree);
        }

        private void ShowMessage_Click(object sender, EventArgs e)
        {
            // 后续需要有一个界面元素管理类;
            LayoutAnchorable sub = new LayoutAnchorable();
            MesasgeRecv content = new MesasgeRecv();

            sub.Content = content;
            sub.FloatingHeight = 300;
            sub.FloatingWidth = 800;

            this.Pane.Children.Add(sub);
            sub.Float();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Lost_Nodeb_Focus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Base lost focus");
        }

        private void Load_Nodeb(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Base Load");
        }

        private void Get_Nodeb_Focus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Get Nodeb Focus");

        }

        private void AddeNB(object sender, EventArgs e)
        {
            AddNodeB.NewInstance(this).Closed += AddNB_Closed;
            AddNodeB.NewInstance(this).ShowDialog();
        }

        private void ShowFlowChart(object sender, EventArgs e)
        {
            LayoutAnchorable sub = new LayoutAnchorable();
            FlowChart content = new FlowChart();

            sub.Content = content;
            sub.FloatingHeight = 300;
            sub.FloatingWidth = 800;

            this.Pane.Children.Add(sub);
            sub.Float();
        }

        private void OpenFileButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "CDL Log|*.txt" + "|All Files|*.*", Multiselect = true};
            if ((bool)dialog.ShowDialog())
            {
                string[] fileNames = dialog.FileNames;
                this.textDirction.Text = fileNames[0];
            }
        }

        private void parseFile_Click(object sender, RoutedEventArgs e)
        {
            List<Event> le = new List<Event>();
            byte[] bytes = { 0x06 ,0xD6, 0x12, 0x09, 0x00, 0x20, 0xFF, 0xFF, 0xFF, 0x28,0xFF, 0xF0, 0x5A, 0xC4, 0x95, 0x6C, 0x1D, 0x36, 0xE3, 0xB4, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x00, 0x5C, 0x00 };
            IDbCommand sessionSqlCmd;
            DbOptions opts = new DbOptions();
            opts.ConnStr = DbConnSqlite.GetConnectString(DbConnProvider.DefaultSqliteDatabaseName); ;
            opts.ConnType = CDLBrowser.Parser.DatabaseMgr.DbType.SQLite;
            DbConn dbconn = new DbConn(opts);
            int ret = dbconn.CheckDatabaseTable(typeof(Event));
            if (ret < 0)
            {                
                return;
            }
            /*
            dbconn.ExcuteNonQuery("PRAGMA synchronous = OFF");
            dbconn.ExcuteNonQuery("PRAGMA journal_mode = MEMORY");
            */

            sessionSqlCmd = dbconn.BeginTransaction();
            string sql = string.Empty;
            EventParser parser = new EventParser();
            parser.Version = "1.3.06659";    

            EventParserManager.Instance.AddEventParser(parser.Version, parser);
            Event newe = ParseEvent(bytes,"1.3.06659");
            le.Add(newe);
           sql = dbconn.CreateInsertSqlFromObject(typeof(Event), newe, "Event", true);
          //  dbconn.ExcuteByTrans(sql, sessionSqlCmd);

            IDbDataParameter dbparameterRaw = null;
            IDbDataParameter dbparameterBody = null;
            dbparameterRaw = new SQLiteParameter("@RawData", newe.RawData);
            dbparameterBody = new SQLiteParameter("@MsgBody", newe.MsgBody);

            IDbDataParameter[] paramsArray = new IDbDataParameter[2];
            paramsArray[0] = dbparameterRaw;
            paramsArray[1] = dbparameterBody;
            dbconn.ExecuteWithParamtersByTrans(sql, paramsArray, sessionSqlCmd);


            /*
            for (int i = 0; i < 10; i++) {
                EventNew ne = new EventNew();
                ne.DisplayIndex = i;
                ne.TimeStamp = "gggggg";
                ne.EventName = Convert.ToString(i);
                ne.MessageDestination = "UE";
                ne.MessageSource = "enb";
                le.Add(ne);
                sql = dbconn.CreateInsertSqlFromObject(typeof(EventNew), ne, "EventNew", true);
                dbconn.ExcuteByTrans(sql, sessionSqlCmd);
            }*/

            dbconn.CommitChanges(sessionSqlCmd);
            dbconn.Close();
            this.dataGrid.ItemsSource = le;

        }

        private Event  ParseEvent(byte[] eventsBuffer,string version) {
            var memoryStream = new MemoryStream(eventsBuffer);
            string timeCircleReport = String.Empty;
            string strDateTime;
            DateTime dtStart;
            dtStart = DateTime.Now;
            strDateTime = dtStart.ToString("yyyy-MM-dd hh:mm:ss.fff");
            Event evt = null;
            try
            {
                evt = new Event()
                {
                    HostNeid = 1,
                    TimeStamp = "",
                    Version = "1.3.06659",
                    ParsingId = 1,
                    LogFileId = 1,

                    //DisplayIndex = this.eventIndex++,
                    IsMarked = false,
                    TickTime = ConvertTimeStamp.Singleton.ConvertTimeStampToUlong(strDateTime, 0),
                };
                bool binitRet = evt.InitializePersistentData(memoryStream, version);
                if (evt.EventName != null && (evt.EventName.Equals("SYS_HL_TIME_ADJUST") || evt.EventName.Equals("SYS_L2_TIME_ADJUST")))
                {
                    timeCircleReport = "";
                    //evt.Delete();
                    return null;
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("ParseEvent EXP:" + exp.StackTrace);
                //throw;
            }

            if (evt == null)
            {
                return null;
            }
            string evtNameForUeType = evt.EventName;
            /**/
            if (evtNameForUeType.Equals("S1 Initial Context Setup Request")||evtNameForUeType.Equals("UECapabilityInformation"))
            {

                var rootNode = SecondaryParser.Singleton.GetExpandNodeQuote(evt);
                if (rootNode != null)
                {
                    var paraNode = rootNode.GetChildNodeById("Data[]");
                    if (paraNode != null)
                    {
                        var displayContent = paraNode.DisplayContent;
                        try
                        {
                            string ueType = String.Empty;
                            if (evtNameForUeType.Equals("S1 Initial Context Setup Request"))
                                ueType = this.FindValueFromTarget(displayContent, "UERadioCapability");
                            else if (evtNameForUeType.Equals("UECapabilityInformation"))
                                ueType = this.FindValueFromTarget(displayContent, "ueCapabilityRAT-Container");
                            if (!string.IsNullOrEmpty(ueType))
                            {
                                int startPosition = ueType.LastIndexOf(')') + 1;
                                int endPosition = ueType.LastIndexOf('\'');
                                int ueTypeLength = endPosition - startPosition;
                                ueType = ueType.Substring(startPosition, ueTypeLength);
                                evt.UeType =
                                    CDLBrowser.Parser.Configuration.ConfigurationManager.Singleton.GetUeTypeConfiguration().
                                        GetUeTypeByCapabilityStr(ueType);
                            }

                        }
                        catch (Exception ex)
                        {                           
                        }
                    }
                }
            }




            /**/

          //  FilteredAddEvent(evt);
            evt.DisplayIndex++;
            IConfigNodeWrapper bodyNode = SecondaryParser.Singleton.GetExpandNodeQuote(evt);
            if (bodyNode != null)
            {
                string msgbody = "";
                ExportBodyToXml(bodyNode, ref msgbody, 0);
                evt.MsgBody = msgbody;
                evt.InitOtherParams();
            }

            return evt;

        }

        string FindValueFromTarget(string content, string keyWord)
        {
            var targetPosition = content.IndexOf(keyWord, StringComparison.Ordinal);
            if (targetPosition == -1)
            {
                return null;
            }

            var length = keyWord.Length;
            targetPosition = targetPosition + length - 1;

            var positionBegin = content.IndexOf(':', targetPosition);
            var positionEnd = content.IndexOf('\n', targetPosition);
            if (positionBegin == -1 || positionBegin > positionEnd)
            {
                positionBegin = content.IndexOf(' ', targetPosition);
            }

            var targetValue = content.Substring(positionBegin + 1, positionEnd - positionBegin - 1);
            targetValue = targetValue.Trim(',').Trim(' ');
            return targetValue;
        }

        private void ExportBodyToXml(IConfigNodeWrapper root, ref string result, int level)
        {

            if (root != null)
            {
                string spaces = "";
                for (int i = 0; i < level; i++)
                {
                    spaces += ' ';
                }
                result += spaces + string.Format("  {0}\n", root.DisplayContent);
                level++;
                foreach (var child in root.Children)
                {
                    ExportBodyToXml(child, ref result, level);
                }               
            }
        }

        private void deleteTempFile() {
            string fileCdl = AppPathUtiliy.Singleton.GetAppPath() + "cdl.db";

            if (File.Exists(fileCdl))
            {
     
                File.Delete(fileCdl);
            }
            else
            {
      
            }
        }
	
	//Add by mayi 
        //实现  鼠标的移动事件，执行拖拽
        private  void TreeViewItem_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //判断鼠标左键是否按下，否则，只要鼠标在范围内移动就会一直触发事件
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                try
                {
                    TreeViewItem myItem = sender as TreeViewItem;
                    TreeView myTree = myItem.Parent as TreeView;
                    //开始执行拖拽
                    DragDropEffects myDropEffect = DragDrop.DoDragDrop(myTree, myTree.SelectedValue, DragDropEffects.Copy);

                }
                catch (Exception)
                {
                }

            }//if button

        }

    }
}
