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
using SCMTOperationCore.Elements;
using SCMTOperationCore.Control;
using Xceed.Wpf.AvalonDock.Layout;
using SCMTMainWindow.Component.View;
using System.Data;
using CDLBrowser.Parser.Document.Event;
using SuperLMT.Utils;
using CDLBrowser.Parser.DatabaseMgr;
using CDLBrowser.Parser.Document;
using System.Data.SQLite;
using System.Windows.Input;
using CDLBrowser.Parser.BPLAN;
using MsgQueue;
using CommonUtility;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Interop;
using CDLBrowser.Parser;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SCMTMainWindow.Component.SCMTControl;
using SCMTMainWindow.Component.ViewModel;
using System.Windows.Data;
using SCMTMainWindow.Component.SCMTControl.LogInfoShow;
using System.Windows.Media;
using System.Threading;
using System.Text;
using dict_d_string = System.Collections.Generic.Dictionary<string, string>;
using MsgDispatcher;
using LmtbSnmp;
using LinkPath;
using System.Net.Sockets;

namespace SCMTMainWindow
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑;
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		#region 私有属性

		private bool m_bIsSingleMachineDebug = false; // add by lyb 增加单机调试时，连接备用数据库
		private bool m_bIsRepeat;
		private IntPtr m_Hwnd;                            // 当前主窗口句柄;
		private LayoutAnchorable subForMessageRecv;       //信令消息界面
		private readonly MessageRecv messageRecv = new MessageRecv();

		// 全局快捷键字典，注册的时候作为出参，根据该信息可以判断热键消息
		private Dictionary<eHotKey, int> m_HotKeyDic = new Dictionary<eHotKey, int>();
		private List<LayoutAnchorable> listAvalon = new List<LayoutAnchorable>();

		#endregion

		#region 公有属性

		public static string m_strNodeName;
		public NodeB node;                         // 当前项目暂时先只连接一个基站;

		#endregion

		//private List<string> CollectList = new List<string>();
		//ObservableCollection<DyDataGrid_MIBModel> content_list                // 用来存储MIBDataGrid中存放的值;
		//	= new ObservableCollection<DyDataGrid_MIBModel>();
		

		#region 构造、析构

		public MainWindow()
		{
			InitializeComponent();
			WindowState = WindowState.Maximized;          // 默认全屏模式;
			MinWidth = 1024;                                             // 设置一个最小分辨率;
			MinHeight = 768;                                             // 设置一个最小分辨率;

			InitView();                                                       // 初始化界面;
			RegisterFunction();                                               // 注册功能;

            TabControlEnable(false);

            // 启动线程，后台处理一些初始化功能
            Task.Factory.StartNew(new Action(() =>
			{

				deleteTempFile();
				SubscribeMsgs();

				// 启动必须程序
				StartMustTools();

				// TODO 读取网元配置文件
			}));

			// 在异常由应用程序引发但未进行处理时发生。主要指的是UI线程。
			Application.Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
			//  当某个异常未被捕获时出现。主要指的是非UI线程
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		#endregion


		#region 程序初始化
		/// <summary>
		/// 初始化用户界面;
		/// </summary>
		private void InitView()
		{
			MibDataGrid.MouseMove += MibDataGrid_MouseMove;
			MibDataGrid.PreviewMouseMove += MibDataGrid_PreviewMouseMove;
			MibDataGrid.GotMouseCapture += MibDataGrid_GotMouseCapture;

			// 解析保存的基站节点信息
			var nodeList = NodeBControl.GetInstance().GetNodebInfo();
			foreach (var node in nodeList)
			{
				AddNodeLabel(node.Key, node.Value);
			}
		}

		/// <summary>
		/// 窗口启动，注册所有所需要的基础功能;
		/// </summary>
		private void RegisterFunction()
		{
			try
			{
				// 注册Trap监听
				if (LmtTrapMgr.GetInstance().StartLmtTrap() == false)
				{
					Log.Error(string.Format("Trap监听启动错误！"));
				}

			} 
			catch(SocketException e)
			{
				Log.Error(string.Format("Trap监听启动错误！"));
				MessageBox.Show("Trap监听启动错误，无法接收Trap消息，请确认162端口是否已被占用！");
			}

			// 初始化通信管理器
			DTLinkPathMgr.GetInstance().Initialize();



		}

		// 启动FTP工具，如果不存在就提示用户
		private void StartMustTools()
		{
			var ftpPath = ConfigFileHelper.GetFtpPath();
			var ftpFullPath = FilePathHelper.GetAppPath() + ftpPath;

			if (!ProcessHelper.StartProcess(ftpFullPath))
			{
				ShowLogHelper.Show("FTP工具启动失败，文件传输任务将会无法进行", "SCMT");
			}
		}

		#endregion

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
		/// 将基站添加到对象树以及详细页的页签当中;
		/// </summary>
		private void AddNodeBPageToWindow()
		{
			// TODO 添加控件
		}

		private void Flow_Click(object sender, RoutedEventArgs e)
		{
			//FlowChart f1 = new FlowChart();
			//f1.Show();

			LayoutAnchorable sub = new LayoutAnchorable();
			FlowChart content = new FlowChart();

			sub.Content = content;
			//sub.FloatingHeight = 300;
			//sub.FloatingWidth = 800;

			Pane.Children.Add(sub);
			sub.Float();
		}

		/// <summary>
		/// 更新数据库;
		/// 调用时机：单机调试；连接基站成功；
		/// </summary>
		/// TODO 连接多个基站时，这个方案需要改
		private void InitDataBase()
		{
			node.db = Database.GetInstance();

			// TODO 需要同步等待数据库初始化完成后才能进行其他操作
			node.db.initDatabase(node.NeAddress.ToString());

			node.db.resultInitData = (bool ret) =>
			{
				if (ret == false)
				{
                    ShowLogHelper.Show("数据库初始化失败，无法创建对象树", "SCMT");
                    ExpanderBaseInfo.IsExpanded = false;
                    TabControlEnable(false);
                }
				else
				{
					Dispatcher.Invoke(() =>
					{
						var Ctrl = new ObjNodeControl(node);        // 初始化象树树信息;
						RefreshObj(Ctrl.m_RootNode);                // 向控件更新对象树;

                        TabControlEnable(true);
                        ExpanderBaseInfo.IsExpanded = true;
                    });
				}
			};
		}

		/// <summary>
		/// 向对象树控件更新对象树模型以及叶节点模型;
		/// </summary>
		/// <param name="ItemsSource">对象树列表</param>
		private void RefreshObj(IList<ObjNode> ItemsSource)
		{
			// 将右侧叶节点容器容器加入到对象树子容器中;
			Obj_Root.SubExpender = FavLeaf_Lists;

			foreach (var items in ItemsSource)
			{
				items.TraverseChildren(Obj_Root, FavLeaf_Lists, 0);
			}
		}

		//______________________________________________________________________主界面动态刷新____
		#region 添加对象树收藏
		/// <summary>
		/// 将对象树添加到收藏;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddToCollect_Click(object sender, RoutedEventArgs e)
		{
			string StrName = m_strNodeName;
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("尚未选择要操作的基站");
			}

			NodeB node = new NodeB(targetIp, "NodeB");
			string cfgFile = FilePathHelper.GetDataPath() + "Tree_Collect.json";

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
						var ObjMibTableName = (string)JObj.First.Next.First[TempCount]["MibTableName"];
						var ObjMibList = (string)JObj.First.Next.First[TempCount]["MIBList"];
						JObject NewObjNodes = new JObject(new JProperty("ObjID", ObjNodesId),
							new JProperty("ObjParentID", ObjParentNodesId),
							new JProperty("ChildRenCount", ObjChildRenCount),
							new JProperty("ObjName", name),
							new JProperty("ObjNameEn", ObjNameEn),
							new JProperty("MibTableName", ObjMibTableName),
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
			StreamWriter sw = new StreamWriter(fs,Encoding.UTF8);
			sw.Write(JObj);
			sw.Flush();
			sw.Close();
			fs.Close();
			//File.WriteAllText(cfgFile, JsonConvert.SerializeObject(JObj));
		}

		private void IsRepeatNode(ObjNode iter, List<ObjNode> listIter)
		{
			foreach(ObjNode iterListSub in listIter)
			{
				if(iter.ObjName == iterListSub.ObjName)
				{
					m_bIsRepeat = true;
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

		// 点击“收藏节点”菜单响应函数
		private void Collect_Node_Click(object sender, EventArgs e)
		{
			object objCollect = Obj_Collect.Content;
			if(objCollect != null)
			{
				Obj_Collect.Content = null;
			}

			objCollect = Obj_Collect.Content;
			List<ObjNode> m_NodeList = new List<ObjNode>();
			List<ObjNode> RootNodeShow = new List<ObjNode>();
			ObjNode Root = new ObjTreeNode(0, 0, "1.0", "收藏节点", @"/");

			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				throw new CustomException("尚未选中要操作的基站");
			}

			NodeB node = new NodeB(targetIp, "NodeB");
			string cfgFile = FilePathHelper.GetDataPath() + "Tree_Collect.json";

			// TODO 不判断文件是否存在
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
				var TableName = (string)JObj.First.Next.First[TempCount]["MibTableName"];
				var version = (string)JObj.First.First;
				if (JObj.First.Next.First[TempCount]["ObjCollect"] == null)
				{
					TempCount++;
					continue;
				}
				int ObjCollect = (int)JObj.First.Next.First[TempCount]["ObjCollect"];

				ObjNode objnode = new ObjTreeNode(iter, ObjParentNodes, version, name, TableName);
				if (ObjCollect == 1)
				{
					int index = m_NodeList.IndexOf(objnode);
					if (index < 0)
					{
						m_NodeList.Add(objnode);
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
					m_bIsRepeat = false;
					IsRepeatNode(iter, Root.SubObj_Lsit);
					if (m_bIsRepeat)
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
			Obj_Collect.Clear();
			Obj_Collect.SubExpender = FavLeaf_Lists;

			foreach (ObjNode items in RootNodeShow)
			{
				items.TraverseCollectChildren(Obj_Collect, FavLeaf_Lists, 0);
			}
		}
		#endregion

		#region 显示折线图事件
		private void Show_LineChart(object sender, EventArgs e)
		{
			// TODO 后续需要有一个界面元素管理类;
			LayoutAnchorable sub = new LayoutAnchorable();
			LinechartContent content = new LinechartContent();

			// 当前的问题：这个Title显示不出来;
			sub.Title = "折线图";
			sub.FloatingHeight = 400;
			sub.FloatingWidth = 800;
			sub.Content = content;
			sub.FloatingLeft = 200;
			sub.FloatingTop = 200;
			sub.CanClose = true;
			sub.CanAutoHide = false;

			Pane.Children.Add(sub);
			sub.Float();

			// 当窗口发生变化时;
			sub.PropertyChanged += content.WindowProperty_Changed;
			sub.Closed += content.Sub_Closed;
			
			/* 后续使用真实数据;
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
			*/
		}

		private void Sub_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Show_HeatMapChart(object sender, EventArgs e)
		{
			// TODO 后续需要有一个界面元素管理类;
			LayoutAnchorable sub = new LayoutAnchorable();
			HeatMapChartContent content = new HeatMapChartContent();

			// 当前的问题：这个Title显示不出来;
			sub.Title = "PRB使用情况";
			sub.FloatingHeight = 400;
			sub.FloatingWidth = 800;
			sub.Content = content;
			sub.FloatingLeft = 200;
			sub.FloatingTop = 200;
			sub.CanClose = true;
			sub.CanAutoHide = false;

			Pane.Children.Add(sub);
			sub.Float();

			// 当窗口发生变化时;
			sub.Closed += content.Sub_Closed;
		}

		#endregion

		#region 显示B方案Message列表控件
		private void ShowMessage_Click(object sender, EventArgs e)
		{
			///后续需要有一个界面元素管理类;
			//  LayoutAnchorable sub = new LayoutAnchorable();
			//     MesasgeRecv content = new MesasgeRecv();

			subForMessageRecv = new LayoutAnchorable
			{
				Content = messageRecv,
				Title = "信令消息",
				FloatingHeight = 500,
				FloatingWidth = 800,
				CanHide = true,
				CanClose = false,
				CanAutoHide = false
			};

			subForMessageRecv.Hiding += subForMessageRecv_Hiding;

			Pane.Children.Add(subForMessageRecv);

			subForMessageRecv.Show();
			subForMessageRecv.Float();

		}
		#endregion

		#region 添加基站

		private void AddeNB(object sender, EventArgs e)
		{
			var nodebDlg = AddNodeB.NewInstance(this);
			nodebDlg.Closed += AddNB_Closed;
			nodebDlg.Owner = this;
			nodebDlg.ShowDialog();
		}

		/// <summary>
		/// 当增加基站的窗口关闭的时候进行的处理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddNB_Closed(object sender, EventArgs e)
		{
			// 如果参数为空，则表示用户没有添加基站;
			if (!(e is NodeBArgs))
			{
				Log.Error("add nodeb failed");
				return;
			}

			node = ((NodeBArgs)e).m_NodeB;
			ObjNode.main = this;
			//ObjNode.datagrid = this.MibDataGrid;

			// 向基站前端控件填入对应信息;
			AddNodeBPageToWindow();                    // 将基站添加到窗口页签中;
			AddNodeLabel(node.FriendlyName, node.NeAddress.ToString());
		}

		private void AddNodeLabel(string friendlyName, string Ip)
		{
			var nodeLabel = new MetroExpander
			{
				Header = friendlyName,
				IsExpanded = true
			};

			nodeLabel.Click += NodeLabel_Click;
			//nodeLabel.Icon = new Uri("Resources / NetPLanB.png");

			var menu = CreateNodebMenu();	// 每个基站创建一个右键菜单，都有自己的状态

			nodeLabel.ContextMenu = menu;
			ExistedNodebList.Children.Add(nodeLabel);
		}

		// 创建基站右键菜单
		private MetroContextMenu CreateNodebMenu()
		{
			// 右键菜单的添加
			var menu = new MetroContextMenu();
			var menuItem = new MetroMenuItem { Header = "连接基站" };
			menuItem.Click += ConnectStationMenu_Click;
			menu.Items.Add(menuItem);

			menuItem = new MetroMenuItem { Header = "断开连接" };
			menuItem.Click += DisconStationMenu_Click;
			menuItem.IsEnabled = false;
			menu.Items.Add(menuItem);

			menuItem = new MetroMenuItem {Header = "数据同步" };
			menuItem.Click += DataSync_Click;
			menuItem.IsEnabled = false;
			menu.Items.Add(menuItem);

			menuItem = new MetroMenuItem { Header = "删除" };
			menuItem.Click += DeleteStationMenu_Click;
			menuItem.IsEnabled = true;
			menu.Items.Add(menuItem);

			return menu;
		}

		/// <summary>
		/// 基站节点  点击事件，获取被点击的IP地址，保存到全局变量
		/// </summary>
		private void NodeLabel_Click(object sender, EventArgs e)
		{
			var target = sender as MetroExpander;
			if (null != target)
			{
				node = NodeBControl.GetInstance().GetNodeByFName(target.Header) as NodeB;
				CSEnbHelper.SetCurEnbAddr(node.NeAddress.ToString());

				//改变被点击的 node，还原之前的 node
				var Children = ExistedNodebList.Children;
				for(int i = 0; i < ExistedNodebList.Children.Count; i++)
				{
					var Item = ExistedNodebList.Children[i] as MetroExpander;
					Item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
				}
				target.Background = new SolidColorBrush(Color.FromRgb(208, 227, 252));
				//target.Background.Opacity = 50;
				//target.Opacity = 50;
				if (m_bIsSingleMachineDebug)
				{
					InitDataBase();
				}
			}
		}

		private void ConnectStationMenu_Click(object sender, RoutedEventArgs e)
		{
			var mui = sender as MenuItem;
			if (null != mui)
			{
				var parent = (ContextMenu)mui.Parent;
				if (null == parent)
				{
					return;
				}

				var target = parent.PlacementTarget as MetroExpander;
				if (target != null)
				{
					node = NodeBControl.GetInstance().GetNodeByFName(target.Header) as NodeB;
					var menuItem = GetMenuItemByIp(node.NeAddress.ToString(), "连接基站");
					if (null != menuItem)
					{
					//NodeBControl.GetInstance().ConnectNodeb(target.Header);
					ShowLogHelper.Show($"开始连接基站：{node.FriendlyName}-{node.NeAddress}", "SCMT");
					node.Connect();
					ObjNode.main = this;
						ChangeMenuHeader(node.NeAddress.ToString(), "连接基站", "取消连接");
					}
					else
					{
						node.DisConnect();
						ChangeMenuHeader(node.NeAddress.ToString(), "取消连接", "连接基站");
					}
				}
			}
		}

		private void DisconStationMenu_Click(object sender, RoutedEventArgs e)
		{
			var tip = $"基站将断开连接，并且该基站打开的功能窗口也将关闭。是否继续操作？";

			var mui = sender as MenuItem;
			if (null == mui) return;

				var parent = (ContextMenu)mui.Parent;
			var target = parent?.PlacementTarget as MetroExpander;
			if (target == null)
				{
					return;
				}


				// 如果MessageBox放在上一句的前面，parent.PlacementTarget将会变成null，拿不到信息
				var dr = MessageBox.Show(tip, "断开连接", MessageBoxButton.YesNo, MessageBoxImage.Question | MessageBoxImage.Warning);
			if (MessageBoxResult.Yes == dr)
				{

				NodeBControl.GetInstance().DisConnectNodeb(target.Header);
			}
		}

		// 基站节点右键菜单：删除，响应函数
		private void DeleteStationMenu_Click(object sender, RoutedEventArgs e)
		{
			var mui = sender as MenuItem;
			if (null != mui)
			{
				var parent = (ContextMenu) mui.Parent;
				if (null == parent)
				{
					return;
				}

				var target = parent.PlacementTarget as MetroExpander;
				if (null == target)
				{
					return;
				}

				var tip = $"确定要删除该网元及其所有子网元？这将关闭该网元对应的所有窗口。";
				var dr = MessageBox.Show(tip, "删除网元", MessageBoxButton.YesNo, MessageBoxImage.Question | MessageBoxImage.Warning);
				if (MessageBoxResult.Yes != dr)
				{
					return;
				}

				var nodeName = target.Header;
				NodeBControl.GetInstance().DisConnectNodeb(nodeName);
				NodeBControl.GetInstance().DelElementByFriendlyName(nodeName);

				ExistedNodebList.Children.Remove(target);
			}
		}

		// 发起数据同步菜单响应函数
		private void DataSync_Click(object sender, RoutedEventArgs e)
		{
			var mui = sender as MenuItem;
			if (null != mui)
			{
				var parent = (ContextMenu)mui.Parent;
				if (null == parent)
				{
					return;
				}

				var target = parent.PlacementTarget as MetroExpander;
				if (null == target)
				{
					return;
				}

				var nodeName = target.Header;
				var targetIp = NodeBControl.GetInstance().GetNodeIpByFriendlyName(nodeName);
				if (null == targetIp)
				{
					return;
				}

				var tip = $"即将发起与设备的数据同步过程，耗时较长，请确认是否继续？";
				var dr = MessageBox.Show(tip, "数据同步", MessageBoxButton.YesNo, MessageBoxImage.Question | MessageBoxImage.Warning);
				if (MessageBoxResult.Yes != dr)
				{
					return;
				}

                // 发送消息，开始数据同步
                long taskId = 0;
				long reqId = 0;
				var dstPath = FilePathHelper.GetConsistencyFilePath();
				FilePathHelper.CreateFolder(dstPath);
				var fto = FileTransTaskMgr.FormatTransInfo(dstPath, "", Transfiletype5216.TRANSFILE_dataConsistency,
					TRANSDIRECTION.TRANS_UPLOAD);
				fto.IpAddr = targetIp;
				FileTransTaskMgr.SendTransFileTask(targetIp, fto, ref taskId, ref reqId);

            }
		}

		private void EnableMenu(ContextMenu menuRoot, string header, bool bEnable = true)
		{
			var menu = GetMenuItemByHeader(menuRoot, header);
			if (null != menu)
			{
				menu.IsEnabled = bEnable;
			}
		}

		// 根据ip找到对应的控件，然后根据menuDesc也就是header，找到menu，然后设置menu的状态
		private void EnableMenu(string targetIp, string menuDesc, bool bEnable = true)
		{
			if (string.IsNullOrEmpty(targetIp) || string.IsNullOrEmpty(menuDesc))
				return;

			var header = NodeBControl.GetInstance().GetFriendlyNameByIp(targetIp);

			ExistedNodebList.Dispatcher.BeginInvoke(new Action(() =>
			{
				var children = ExistedNodebList.Children;
				if (null == children) return;

				var count = children.Count;
				for (var index = 0; index < count; index++)
				{
					var target = children[index] as MetroExpander;
					if (target.Header.Equals(header))
					{
						EnableMenu(target.ContextMenu, menuDesc, bEnable);
						break;
					}
				}
			}));
		}

		private MenuItem GetMenuItemByHeader(ContextMenu menuRoot, string header)
		{
			if (null == menuRoot || string.IsNullOrEmpty(header))
			{
				return null;
			}

			var menuItems = menuRoot.Items;
			foreach (var submenu in menuItems)
			{
				var item = submenu as MenuItem;
				if (item != null && header.Equals(item.Header))
				{
					return item;
				}
			}
			return null;
		}
		private MenuItem GetMenuItemByIp(string ip, string menuText)
		{
			if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(menuText))
				return null;
			var header = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);
			MenuItem retMenu = null;
			var children = ExistedNodebList.Children;
			if (null == children) return null;

			ContextMenu contextMenu = null;
			var count = children.Count;
			for (var index = 0; index < count; index++)
			{
				var target = children[index] as MetroExpander;
				if (target != null && target.Header.Equals(header))
				{
					contextMenu = target.ContextMenu;
					break;
				}
			}
			retMenu = GetMenuItemByHeader(contextMenu, menuText);
			return retMenu;
		}
		private bool ChangeMenuHeaderAsync(string ip, string oldText, string newText)
		{
			if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(oldText) || string.IsNullOrEmpty(newText))
				return false;
			var header = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);
			ExistedNodebList.Dispatcher.BeginInvoke(new Action(() =>
			{
				var children = ExistedNodebList.Children;
				ContextMenu contextMenu = null;
				var count = children.Count;
				for (var index = 0; index < count; index++)
				{
					var target = children[index] as MetroExpander;
					if (target != null && target.Header.Equals(header))
					{
						contextMenu = target.ContextMenu;
						break;
					}
				}
				if (contextMenu == null)
					return;
				var menuItems = contextMenu.Items;
				foreach (var submenu in menuItems)
				{
					var item = submenu as MenuItem;
					if (item != null && oldText.Equals(item.Header))
					{
						item.Header = newText;
					}
				}
			}));
			return true;
		}
		private bool ChangeMenuHeader(string ip, string oldText, string newText)
		{
			var item = GetMenuItemByIp(ip, oldText);
			if (null == item)
				return false;
			item.Header = newText;
			return true;
		}

		#endregion

		#region 添加泳道图事件
		private void ShowFlowChart(object sender, EventArgs e)
		{
			//LayoutAnchorable sub = new LayoutAnchorable();
			//FlowChart content = new FlowChart();

			//sub.Content = content;
			//sub.FloatingHeight = 300;
			//sub.FloatingWidth = 800;

			//this.Pane.Children.Add(sub);
			//sub.Float();
		}
		#endregion
		
		#region CDL的一大堆……
		// 开始解析;
		private void parseFile_Click(object sender, RoutedEventArgs e)
		{
			List<Event> le = new List<Event>();
			byte[] bytes = { 0x06 ,0xD6, 0x12, 0x09, 0x00, 0x20, 0xFF, 0xFF, 0xFF, 0x28,0xFF, 0xF0, 0x5A, 0xC4, 0x95, 0x6C, 0x1D, 0x36, 0xE3, 0xB4, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x00, 0x5C, 0x00 };
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

			var sessionSqlCmd = dbconn.BeginTransaction();
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
			

			dbconn.CommitChanges(sessionSqlCmd);
			dbconn.Close();

		}

		private Event  ParseEvent(byte[] eventsBuffer,string version) {
			var memoryStream = new MemoryStream(eventsBuffer);
			string timeCircleReport = String.Empty;
			string strDateTime = TimeHelper.DateTimeToString(DateTime.Now, "yyyy-MM-dd hh:mm:ss.fff");

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
								ueType = FindValueFromTarget(displayContent, "UERadioCapability");
							else if (evtNameForUeType.Equals("UECapabilityInformation"))
								ueType = FindValueFromTarget(displayContent, "ueCapabilityRAT-Container");
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
						catch (Exception)
						{
						}
					}
				}
			}

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
		#endregion

		#region 快捷键在此
		//Add by mayi 
		//实现  鼠标的移动事件，执行拖拽
		private void TreeViewItem_MouseMove(object sender, MouseEventArgs e)
		{
			//判断鼠标左键是否按下，否则，只要鼠标在范围内移动就会一直触发事件
			if (e.LeftButton == MouseButtonState.Pressed)
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

		/// <summary>
		/// 窗体资源准备完成之后，获取当前窗口句柄，并添加消息处理程序;
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			//获取窗体句柄;
			m_Hwnd = new WindowInteropHelper(this).Handle;
			HwndSource m_HwndSource = HwndSource.FromHwnd(m_Hwnd);

			//添加消息处理程序;
			if (m_HwndSource != null)
			{
				m_HwndSource.AddHook(WndProc);
			}
		}

		/// <summary>
		/// 窗体消息回调函数，负责处理热键消息;
		/// </summary>
		/// <param name="hWnd">窗口句柄</param>
		/// <param name="msg">消息</param>
		/// <param name="wParam">附加参数1</param>
		/// <param name="lParam">附加参数2</param>
		/// <param name="handled">是否处理</param>
		/// <returns></returns>
		private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			switch (msg)
			{
				//消息是热键消息;
				case HotKeyManager.WM_HOTKEY:

					int atomID = wParam.ToInt32();

					//此处无法使用switch，因为case不是常量而是变量;
					if (atomID == m_HotKeyDic[eHotKey.UserCase1])
					{
						SignalBConfig.SetScriptTxt(1);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase2])
					{
						SignalBConfig.SetScriptTxt(2);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase3])
					{
						SignalBConfig.SetScriptTxt(3);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase4])
					{
						SignalBConfig.SetScriptTxt(4);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase5])
					{
						SignalBConfig.SetScriptTxt(5);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase6])
					{
						SignalBConfig.SetScriptTxt(6);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase7])
					{
						SignalBConfig.SetScriptTxt(7);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase8])
					{
						SignalBConfig.SetScriptTxt(8);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					else if (atomID == m_HotKeyDic[eHotKey.UserCase9])
					{
						SignalBConfig.SetScriptTxt(9);
						PublishHelper.PublishMsg("StartTraceHlSignal", "");
					}
					handled = true;

					break;
			}
			return IntPtr.Zero;
		}

		/// <summary>
		/// 所有控件初始化完成之后，注册快捷键
		/// </summary>
		/// <param name="e"></param>
		protected override void OnContentRendered(EventArgs e)
		{
			base.OnContentRendered(e);

			InitHotKey();
		}

		/// <summary>
		/// 初始化并注册快捷键
		/// </summary>
		/// <param name="listHotKeyModel">待注册的快捷键集合，初始为空，再次注册时则不为空</param>
		/// <returns>成功 true 并保存全局变量的值  失败 false 弹出重新注册的界面</returns>
		private bool InitHotKey(ObservableCollection<HotKeyModel> listHotKeyModel = null)
		{
			//参数为空，加载配置文件中的数据，否则，按照界面设置的数据进行设置
			var listHKM = listHotKeyModel ?? HotKeyInit.Instance.LoadJsonFileInfo();

			if (listHKM == null)
			{
				MessageBox.Show("加载配置文件失败");
				return false;
			}

			string strFali = HotKeyManager.RegisterAllHotKey(listHKM, m_Hwnd, out m_HotKeyDic);

			//返回的字符串为空则全局注册成功
			if (string.IsNullOrEmpty(strFali))
			{
				//更新配置文件数据
				File.WriteAllText("HotKey.json", JsonConvert.SerializeObject(listHKM));
				return true;
			}

			//注册失败，弹出选择框，是否重新注册快捷键
			MessageBoxResult mbResult = MessageBox.Show(string.Format("无法注册下列快捷键\n\r{0}是否重新注册？", strFali), "提示", MessageBoxButton.YesNo);

			if (mbResult == MessageBoxResult.Yes)
			{
				var win = HotKeySet.CreateInstance();

				if (!win.IsVisible)
				{
					win.ShowDialog();
				}
				else
				{
					win.Activate();
				}

				return false;
			}

			return false;
		}

		/// <summary>
		/// 窗口加载完成之后，添加注册事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
		{
			HotKeyInit.Instance.RegisterGlobalHotKeyEvent += Instance_RegisterGlobalHotKeyEvent;
		}


		/// <summary>
		/// 事件处理函数
		/// </summary>
		/// <param name="listHotKeyModel"></param>
		/// <returns></returns>
		private bool Instance_RegisterGlobalHotKeyEvent(ObservableCollection<HotKeyModel> listHotKeyModel)
		{
			return InitHotKey(listHotKeyModel);
		}


		/// <summary>
		/// 显示  注册界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MetroMenuItem_Click(object sender, RoutedEventArgs e)
		{
			//显示  设置窗体
			var win = HotKeySet.CreateInstance();

			if (!win.IsVisible)
			{
				win.ShowDialog();
			}
			else
			{
				win.Activate();
			}

		}
		#endregion

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
		}

		private void Lost_Nodeb_Focus(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Base page lost focus");
		}

		private void Load_Nodeb(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Base Load");
		}

		private void Get_Nodeb_Focus(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Get Nodeb Focus");
		}


		private void OpenClick(object sender, RoutedEventArgs e)
		{
			ParseMessageWindow Pw = new ParseMessageWindow();
			Pw.Show();
		}

		#region DataGrid相关处理

		/// <summary>
		/// 针对DataGrid的鼠标操作;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MibDataGrid_MouseMove(object sender, MouseEventArgs e)
		{
			//(e.OriginalSource as DataGridCell).DataContext is MS.Internal.NamedObject;
			try
			{
				if (e.OriginalSource is DataGridCell)
				{
					Debug.WriteLine("MouseMove;函数参数e反馈的实体是单元格内数据内容:" +
						((e.OriginalSource as DataGridCell).Column).Header);

					DyDataGrid_MIBModel SelectedIter = new DyDataGrid_MIBModel();

					foreach (var iter in (e.Source as DataGrid).SelectedCells)
					{
						Console.WriteLine("User Selected:" + iter.Item.GetType());
						SelectedIter = iter.Item as DyDataGrid_MIBModel;
					}

					DataGridCell item = e.OriginalSource as DataGridCell;

					// 在MouseMove事件当中可以添加鼠标拖拽事件;
					if (e.MiddleButton == MouseButtonState.Pressed)
					{
						DragDropEffects myDropEffect = DragDrop.DoDragDrop(item, new DataGridCell_MIB_MouseEventArgs()
						{
							HeaderName = (e.OriginalSource as DataGridCell).Column.Header.ToString(),
							SelectedCell = SelectedIter
						}, DragDropEffects.Copy);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("MouseMove Exception" + ex);
			}
		}

		private void MibDataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (e.OriginalSource is DataGridCell)
			{
				Console.WriteLine("PreviewMouseMove;函数参数e反馈的实体是单元格内数据内容:" +
					((e.OriginalSource as DataGridCell).Column).Header);
			}
		}

		private void MibDataGrid_GotMouseCapture(object sender, MouseEventArgs e)
		{
			try
			{
				if ((e.OriginalSource as DataGrid).Items.CurrentItem is DyDataGrid_MIBModel)
				{
					DyDataGrid_MIBModel SelectedIter = new DyDataGrid_MIBModel();

					foreach (var iter in (e.OriginalSource as DataGrid).SelectedCells)
					{
						Console.WriteLine("User Selected:" + iter.Item.GetType() + "and Header is" + iter.Column.Header);
						SelectedIter = iter.Item as DyDataGrid_MIBModel;

						DataGrid item = e.OriginalSource as DataGrid;

						// 在MouseMove事件当中可以添加鼠标拖拽事件;
						if (e.LeftButton == MouseButtonState.Pressed)
						{
							DragDropEffects myDropEffect = DragDrop.DoDragDrop(item, new DataGridCell_MIB_MouseEventArgs()
							{
								HeaderName = iter.Column.Header.ToString(),
								SelectedCell = SelectedIter
							}, DragDropEffects.Copy);
						}

					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

		}

		/// <summary>
		/// 打开跟踪设置界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MetroMenuItem_Click_1(object sender, RoutedEventArgs e)
		{
			//显示  设置窗体
			var win = TraceSet.CreateInstance();

			if (!win.IsVisible)
			{
				win.ShowDialog();
			}
			else
			{
				win.Activate();
			}

		}

		/// <summary>
		/// 每有一条新的MIB数据，都会调用该函数;
		/// </summary>
		/// <param name="ar"></param>
		/// <param name="oid_cn"></param>
		/// <param name="oid_en"></param>
		/// <param name="contentlist"></param>
		public void UpdateMibDataGrid(IAsyncResult ar, Dictionary<string, string> oid_cn, Dictionary<string, string> oid_en, ObservableCollection<DyDataGrid_MIBModel> contentlist)
		{
			SnmpMessageResult res = ar as SnmpMessageResult;

			// 将信息回填到DataGrid当中;
			MibDataGrid.Dispatcher.Invoke(new Action(() =>
			{
				MibDataGrid.Columns.Clear();                          //以最后一次为准即可;
				dynamic model = new DyDataGrid_MIBModel();

				// 遍历GetNext的结果;
				foreach (KeyValuePair<string, string> iter in res.AsyncState as Dictionary<string, string>)
				{
					Console.WriteLine("NextIndex" + iter.Key + " Value:" + iter.Value);

					// 通过基站反馈回来的一行结果，动态生成一个类型，用来与DataGrid对应;
					foreach (var iter2 in oid_cn)
					{
						// 如果存在对应关系;
						if (iter.Key.Contains(iter2.Key))
						{
							Console.WriteLine("Add Property:" + oid_en[iter2.Key] + " Value:" + iter.Value + " and Header is:" + iter2.Value);
							model.AddProperty(oid_en[iter2.Key], new DataGrid_Cell_MIB()
							{
								m_Content = iter.Value,
								oid = iter.Key,
								MibName_CN = iter2.Value,
								MibName_EN = oid_en[iter2.Key]
							}, iter2.Value);
						}
					}
				}

				// 将这个整行数据填入List;
				if (model.Properties.Count != 0)
				{
					// 向单元格内添加内容;
					contentlist.Add(model);
				}

				foreach (var iter3 in oid_en)
				{
					Console.WriteLine("new binding is:" + iter3.Value + ".m_Content");
					DataGridTextColumn column = new DataGridTextColumn();
					column.Header = oid_cn[iter3.Key];
					column.Binding = new Binding(iter3.Value + ".m_Content");

					MibDataGrid.Columns.Add(column);

				}

				MibDataGrid.DataContext = contentlist;

			}));
		}

		/// <summary>
		/// 当SNMP模块收集全整表数据后，调用该函数;
		/// </summary>
		/// <param name="ar">GetNext之后，得到的整表数据;</param>
		/// <param name="oid_cn"></param>
		/// <param name="oid_en"></param>
		/// <param name="contentlist"></param>
		public void UpdateAllMibDataGrid(dict_d_string ar, dict_d_string oid_cn, dict_d_string oid_en,
			ObservableCollection<DyDataGrid_MIBModel> contentlist, string ParentOID, int IndexCount)
		{
			var action = new Action<dict_d_string, dict_d_string, dict_d_string, ObservableCollection<DyDataGrid_MIBModel>, 
				string, int>(UpdateMibDataGridCallback);
			MibDataGrid.Dispatcher.Invoke(action, new object[] {ar, oid_cn , oid_en, contentlist, ParentOID, IndexCount});
		}

		private void UpdateMibDataGridCallback(dict_d_string ar, dict_d_string oid_cn, dict_d_string oid_en,
			ObservableCollection<DyDataGrid_MIBModel> contentlist, string ParentOID, int IndexCount)
		{
			int RealIndexCount = IndexCount; // 真实的索引个数
											 // 将信息回填到DataGrid当中;

			MibDataGrid.Columns.Clear();                             // 清除上一次的结果;

			if (IndexCount == 0)                                          // 如果索引个数为0，按照1来处理;
				IndexCount = 1;

			var AlreadyRead = new List<string>();

			// TODO 调试打印,正式版本记得删除;
			//foreach (var iter in ar)
			//{
			//	string[] temp = iter.Key.Split('.');
			//	string NowIndex = "";
			//	string NowNodeOID = "";

			//	for (int i = temp.Length - IndexCount; i < temp.Length; i++)
			//	{
			//		NowIndex += "." + temp[i];
			//	}
			//	for (int i = 0; i < temp.Length - IndexCount; i++)
			//	{
			//		NowNodeOID += "." + temp[i];
			//	}

			//	Console.WriteLine("NextIndex " + iter.Key + " and Value is " + iter.Value + " OID Index is " + NowIndex +
			//		" Node OID is " + NowNodeOID.Substring(1, NowNodeOID.Length - NowIndex.Length + 1));
			//}

			// 遍历GetNext结果后，将结果填入到DataGrid控件当中;
			foreach (var iter in ar)
			{
				var fulloid = iter.Key;

				// 获取当前遍历到的节点的索引值(即取最后N位数字);
				var temp = fulloid.Split('.');
				var NowIndex = "";
				for (var i = temp.Length - IndexCount; i < temp.Length; i++)
				{
					NowIndex += "." + temp[i];
				}

				Debug.WriteLine("NextIndex " + fulloid + " and Value is " + iter.Value + " OID Index is " + NowIndex);

				// 如果存在索引,且索引没有被添加到表中;
				if (!fulloid.Contains(NowIndex) || AlreadyRead.Contains(NowIndex))
					continue;


				dynamic model = new DyDataGrid_MIBModel();
				//尝试填写表量表填写实例描述

				if (RealIndexCount > 0)
				{
					var IndexOIDPre = "";
					for (int i = 0; i < temp.Length - IndexCount - 1; i++)
					{
						IndexOIDPre += "." + temp[i];
					}

					var IndexContent = "";
					for (int i = 0; i < RealIndexCount; i++)
					{
						string IndexOIDTemp = IndexOIDPre + "." + (i + 1);
						string IndexOID = IndexOIDTemp.Substring(1);
						IndexContent += oid_cn[IndexOID] + temp[temp.Length - RealIndexCount + i];
					}

					//如下DataGrid_Cell_MIB中的 oid暂时填写成这样
					model.AddProperty("indexlist", new DataGrid_Cell_MIB()
					{
						m_Content = IndexContent,
						oid = IndexOIDPre + ".",
						MibName_CN = "实例描述",
						MibName_EN = "indexlist"
					}, "实例描述");
				}

				// 将ar当中所有匹配的结果取出,最后会取出了一行数据;
				foreach (var iter3 in ar)
				{
					// 将所有相同索引取出;
					temp = iter3.Key.Split('.');
					string TempIndex = "";

					// TODO 这里和上面有什么区别？
					for (int i = temp.Length - IndexCount; i < temp.Length; i++)
					{
						TempIndex += "." + temp[i];
					}

					//以前的写法有问题，比如0.0.10包含了0.0.1，会有误判的情况，此处修改by tangyun
					if (TempIndex == NowIndex)
					{
						// 将GetNext整表的OID数值取出到temp_compare;
						string[] temp_nowoid = iter3.Key.Split('.');
						string NowNodeOID = "";
						for (int i = 0; i < temp_nowoid.Length - IndexCount; i++)
						{
							NowNodeOID += "." + temp_nowoid[i];
						}
						string temp_compare = NowNodeOID.Substring(1);

						// 如果OID匹配;
						if (oid_cn.ContainsKey(temp_compare))
						{
							Debug.WriteLine("Add Property:" + oid_en[temp_compare] + " Value:" + iter3.Value + " and Header is:" + oid_cn[temp_compare]);

							var dgm = new DataGrid_Cell_MIB()
							{
								m_Content = SnmpToDatabase.ConvertSnmpValueToString(oid_en[temp_compare], iter3.Value, "172.27.245.92") as string,  // TODO 需要确定真正的板卡地址
								oid = iter3.Key,
								MibName_CN = oid_cn[temp_compare],
								MibName_EN = oid_en[temp_compare]
							};

							model.AddProperty(oid_en[temp_compare], dgm, oid_cn[temp_compare]);

							// 已经查询过该索引,后续不再参与查询;
							if (!AlreadyRead.Contains(NowIndex))
							{
								AlreadyRead.Add(NowIndex);
							}
						}
					}
				}

				// 将这个整行数据填入List;
				if (model.Properties.Count != 0)
				{
					// 向单元格内添加内容;
					contentlist.Add(model);
				}

			}
			//增加表量表索引的列名
			if (RealIndexCount > 0)
			{
				var column = new DataGridTextColumn
				{
					Header = "实例描述",
					Binding = new Binding("indexlist.m_Content")
				};
				MibDataGrid.Columns.Add(column);
			}

			foreach (var iter3 in oid_en)
			{
				string[] temp = iter3.Key.Split('.');
				if ((RealIndexCount > 0) && (int.Parse(temp[temp.Length - 1]) <= RealIndexCount))
				{
					//索引不对应列名
					continue;
				}

				var column = new DataGridTextColumn
				{
					Header = oid_cn[iter3.Key],
					Binding = new Binding(iter3.Value + ".m_Content")
				};

				MibDataGrid.Columns.Add(column);
			}

			MibDataGrid.DataContext = contentlist;
		}

		#endregion



		/// <summary>
		/// 隐藏  信令消息界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void subForMessageRecv_Hiding(object sender, System.ComponentModel.CancelEventArgs e)
		{
			messageRecv.ClearAll();
		}


		private void MetroExpander_Click(object sender, EventArgs e)
		{
			var enbIp = CSEnbHelper.GetCurEnbAddr();
			if(enbIp == null)
			{
				MessageBox.Show("未选择基站，请单击需要显示的基站");
				return;
			}

			string strFriendName =  NodeBControl.GetInstance().GetFriendlyNameByIp(enbIp);

			foreach(LayoutAnchorable item in listAvalon)
			{
				if(item.Title == strFriendName)
				{
					item.Show();
					return;
				}
			}

			var content = new Component.SCMTControl.FileManager.TestTwoFileManager(enbIp);

			var sub = new LayoutAnchorable
			{
				Content = content,
				Title = strFriendName,
				FloatingHeight = 800,
				FloatingWidth = 600,
				CanHide = true,
				CanClose = false,
				CanAutoHide = false
			};

			listAvalon.Add(sub);
			FileManagerLAP.Children.Add(sub);
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			//可以记录日志并转向错误bug窗口友好提示用户
			if (e.ExceptionObject is Exception)
			{
				Exception ex = (Exception)e.ExceptionObject;
				Log.WriteLogFatal(ex);
				MessageBox.Show(ex.Message);
			}
		}
		void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			//可以记录日志并转向错误bug窗口友好提示用户
			e.Handled = true;
			Log.WriteLogFatal(e.Exception);
			MessageBox.Show("消息:" + e.Exception.Message + "\r\n" + e.Exception.StackTrace);

		}

		#region 订阅消息及处理

		// 订阅消息
		private void SubscribeMsgs()
		{
			SubscribeHelper.AddSubscribe(TopicHelper.SHOW_LOG, OnShowLog);
			SubscribeHelper.AddSubscribe(TopicHelper.EnbConnectedMsg, OnConnect);
			SubscribeHelper.AddSubscribe(TopicHelper.EnbOfflineMsg, OnDisconnect);
            SubscribeHelper.AddSubscribe(TopicHelper.LoadLmdtzToVersionDb, OnLoadLmdtzToVersionDb);
        }

		// 打印日志
		private void OnShowLog(SubscribeMsg msg)
		{
			var logInfo = ShowLogHelper.GetLogInfo(msg.Data);
			//var neName = NodeBControl.GetInstance().GetFriendlyNameByIp(logInfo.TargetIp);

			var newLogInfo = new LogInfoTitle
			{
				Type = logInfo.Type,
				LogInfo = logInfo.Msg,
				TargetIP = logInfo.TargetIp
			};

			Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background, new Action(() =>
				{
					//UiLogShow.AppendText(msgText);
					LogInfoShow.AddLogInfo(newLogInfo, MainLogInfoShow);
				}
				)
			);
		}

		// 连接成功
		private void OnConnect(SubscribeMsg msg)
		{
			var netAddr = JsonHelper.SerializeJsonToObject<NetAddr>(msg.Data);
			var ip = netAddr.TargetIp;

			var fname = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);
			ShowLogHelper.Show($"成功连接基站：{fname}-{ip}", $"{ip}");
			InitDataBase();
			ChangeMenuHeaderAsync(ip, "取消连接", "连接基站");
			EnableMenu(ip, "连接基站", false);
			EnableMenu(ip, "断开连接");
			EnableMenu(ip, "数据同步");
		}
		// 断开连接
		private void OnDisconnect(SubscribeMsg msg)
		{
			var netAddr = JsonHelper.SerializeJsonToObject<NetAddr>(msg.Data);
			var ip = netAddr.TargetIp;

			var fname = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);
			ShowLogHelper.Show($"基站连接断开：{fname}-{ip}", $"{ip}");
			ChangeMenuHeaderAsync(ip, "取消连接", "连接基站");
			EnableMenu(ip, "连接基站");
			EnableMenu(ip, "断开连接", false);
			EnableMenu(ip, "数据同步", false);

			// 文件管理按钮禁用，文件管理窗口关闭
			CloseFileMgrDlg(fname);

			Dispatcher.Invoke(() => Obj_Root.Children?.Clear());
		}

		#endregion
		// 关闭文件管理的窗口
		private bool CloseFileMgrDlg(string friendlyName)
		{
			//foreach (var itemAnchorable in listAvalon)
			//{
			//	if (itemAnchorable.Title == friendlyName)
			//	{
			//		itemAnchorable.Close();
			//	}
			//}
			return true;
		}

		/// <summary>
		/// 柱状图
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Show_BarChart(object sender, EventArgs e)
		{
			LayoutAnchorable sub = new LayoutAnchorable();
			BarChart content = new BarChart();

			// TODO 当前的问题：这个Title显示不出来;
			sub.Title = "柱状图";
			sub.FloatingHeight = 400;
			sub.FloatingWidth = 800;
			sub.Content = content;
			sub.FloatingLeft = 200;
			sub.FloatingTop = 200;
			sub.CanClose = true;
			sub.CanAutoHide = false;

			Pane.Children.Add(sub);
			sub.Float();

			// 当窗口发生变化时;
			//sub.PropertyChanged += content.WindowProperty_Changed;
			//sub.Closed += content.Sub_Closed;
		}

        //解析lm.dtz文件
        private void OnLoadLmdtzToVersionDb(SubscribeMsg msg)
        {
            var netAddr = JsonHelper.SerializeJsonToObject<NetAddr>(msg.Data);
            var ip = netAddr.TargetIp;

            var fname = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);
            
            InitDataBase();

            ShowLogHelper.Show($"解析lm.dtz数据文件成功：{fname}-{ip}", $"{ip}");
        }

        /// <summary>
        /// 用于设置tab页面是否可用
        /// </summary>
        /// <param name="isEnable"></param>
        private void TabControlEnable(bool isEnable)
        {
            ItemCollection collection = MainHorizenTab.Items;
            for(int i = 0; i < collection.Count; i++)
                (collection[i] as TabItem).IsEnabled = isEnable;

        }
    }

}
