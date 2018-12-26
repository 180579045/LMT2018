/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：MainWindow.xaml.cs
// 文件功能描述：主界面控制类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/

using CDLBrowser.Parser;
using CDLBrowser.Parser.BPLAN;
using CDLBrowser.Parser.Configuration;
using CDLBrowser.Parser.DatabaseMgr;
using CDLBrowser.Parser.Document;
using CDLBrowser.Parser.Document.Event;
using CommonUtility;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using MsgDispatcher;
using MsgQueue;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCMTMainWindow.Component.SCMTControl;
using SCMTMainWindow.Component.SCMTControl.FileManager;
using SCMTMainWindow.Component.SCMTControl.LogInfoShow;
using SCMTMainWindow.Component.ViewModel;
using SCMTOperationCore.Control;
using SCMTOperationCore.Elements;
using SCMTOperationCore.Message.SI;
using SuperLMT.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using UICore.Controls.Metro;
using Xceed.Wpf.AvalonDock.Layout;
using DbType = CDLBrowser.Parser.DatabaseMgr.DbType;
using dict_d_string = System.Collections.Generic.Dictionary<string, string>;

namespace SCMTMainWindow
{
	/// <inheritdoc />
	/// <summary>
	/// MainWindow.xaml 的交互逻辑;
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		#region 私有属性

		private bool m_bIsSingleMachineDebug = false; // add by lyb 增加单机调试时，连接备用数据库
		private bool m_bIsRepeat;
		private bool is4GConn = true; // 是否连接4G基站，测试多站连接用

		private List<LayoutAnchorable> listAvalon = new List<LayoutAnchorable>();

		#endregion 私有属性

		#region 公有属性

		public static string m_strNodeName;
		//public NodeB node;                         // 当前项目暂时先只连接一个基站;TODO 改为支持多基站，废除此变量

		#endregion 公有属性

		//private List<string> CollectList = new List<string>();
		//ObservableCollection<DyDataGrid_MIBModel> content_list                // 用来存储MIBDataGrid中存放的值;
		//	= new ObservableCollection<DyDataGrid_MIBModel>();

		#region 构造、析构

		public MainWindow()
		{
			InitializeComponent();
			var uri = new Uri("/PresentationFramework.AeroLite, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35;component/themes/AeroLite.NormalColor.xaml", UriKind.Relative);
			var resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
			this.Resources.MergedDictionaries.Add(resourceDictionary);
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

            // 设置Command源和目标;
            this.GlobalSearch.ButtonClick += GlobalSearch_ButtonClick;
		}

        private void GlobalSearch_ButtonClick(object sender, EventArgs e)
        {
            this.GlobalSearch.Text = "";
        }

        #endregion 构造、析构

        #region 程序初始化

        /// <summary>
        /// 初始化用户界面;
        /// </summary>
        private void InitView()
		{
			// 			MibDataGrid.MouseMove += MibDataGrid_MouseMove;
			// 			MibDataGrid.PreviewMouseMove += MibDataGrid_PreviewMouseMove;
			// 			MibDataGrid.GotMouseCapture += MibDataGrid_GotMouseCapture;
			this.FileManagerTabItem.GotFocus += FileManagerTabItem_GotFocus;

			// 解析保存的基站节点信息
			var nodeList = NodeBControl.GetInstance().GetNodebInfo();
			foreach (var item in nodeList)
			{
				AddNodeLabel(item.Key, item.Value);
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
					Log.Error("Trap监听启动错误！");
				}
			}
			catch (SocketException e)
			{
				Log.Error("Trap监听启动错误！");
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

		#endregion 程序初始化

		/// <summary>
		/// 窗口关闭;
		/// 关闭先前注册的服务;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MetroWindow_Closed(object sender, EventArgs e)
		{
			// TODO:已作废
			//TrapMessage.RequestStop();                                         // 停止注册的Trap监听;
		}

		/// <summary>
		/// 将基站添加到对象树以及详细页的页签当中;
		/// </summary>
		private void AddNodeBPageToWindow()
		{
			// TODO 添加控件
		}

		//private void Flow_Click(object sender, RoutedEventArgs e)
		//{
		//	//FlowChart f1 = new FlowChart();
		//	//f1.Show();

		//	LayoutAnchorable sub = new LayoutAnchorable();
		//	FlowChart content = new FlowChart();

		//	sub.Content = content;
		//	//sub.FloatingHeight = 300;
		//	//sub.FloatingWidth = 800;

		//	Pane.Children.Add(sub);
		//	sub.Float();
		//}

		/// <summary>
		/// 更新数据库;
		/// 调用时机：单机调试；连接基站成功；
		/// </summary>
		/// TODO 连接多个基站时，这个方案需要改
		/// TODO 为了支持多站，添加基站ip参数
		private async Task<bool> InitDataBase(string strIp)
		{
			// 获取nodeB信息
			NodeB nodeB = NodeBControl.GetInstance().GetNodeByIp(strIp) as NodeB;
			CSEnbHelper.SetCurEnbAddr(strIp);

			// TODO 需要同步等待数据库初始化完成后才能进行其他操作
			var result = await Database.GetInstance().initDatabase(strIp);

			if (result)
			{
				Dispatcher.Invoke(() =>
				{
					var Ctrl = new ObjNodeControl(nodeB);        // 初始化象树树信息,Ctrl.m_RootNode即全部对象树信息;
					RefreshObj(Ctrl.m_RootNode);                // 向控件更新对象树;
                    //this.Obj_Root.m_RootNode = Ctrl.m_RootNode;

					TabControlEnable(true);
					ExpanderBaseInfo.IsEnabled = true;
					ExpanderBaseInfo.IsExpanded = true;
				});

				return true;
			}

			ShowLogHelper.Show("数据库初始化失败，无法创建对象树", "SCMT");

			ExpanderBaseInfo.IsEnabled = false;
			ExpanderBaseInfo.IsExpanded = false;
			TabControlEnable(false);
			return false;
		}

		/// <summary>
		/// 向对象树控件更新对象树模型以及叶节点模型;
		/// </summary>
		/// <param name="ItemsSource">对象树列表</param>
		private void RefreshObj(IList<ObjNode> ItemsSource)
		{
			// 将右侧叶节点容器容器加入到对象树子容器中;
			Obj_Root.SubExpender = FavLeaf_Lists;
            GlobalSearch.Target_element = Obj_Root;
            
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
						new JProperty("ObjCollect", 1));
					JObj.First.Next.First[TempCount].Remove();
					JObj.First.Next.First[TempCount].AddBeforeSelf(NewObjNodes);
					break;
				}
				TempCount++;
			}
			reader.Close();
			FileStream fs = new FileStream(cfgFile, FileMode.OpenOrCreate);
			StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
			sw.Write(JObj);
			sw.Flush();
			sw.Close();
			fs.Close();
			//File.WriteAllText(cfgFile, JsonConvert.SerializeObject(JObj));
		}

		private void IsRepeatNode(ObjNode iter, List<ObjNode> listIter)
		{
			foreach (ObjNode iterListSub in listIter)
			{
				if (iter.ObjName == iterListSub.ObjName)
				{
					m_bIsRepeat = true;
				}
				else
				{
					if (iterListSub.SubObj_Lsit != null)
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
			if (objCollect != null)
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

		#endregion 添加对象树收藏

		#region 显示折线图事件

		private void Show_LineChart(object sender, EventArgs e)
		{
			// TODO 后续需要有一个界面元素管理类;
			LayoutAnchorable sub = new LayoutAnchorable();
			LinechartContent content = new LinechartContent();

			// 当前的问题：这个Title显示不出来;
			sub.Title = "折线图";
			sub.FloatingHeight = 460;
			sub.FloatingWidth = 640;
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
		}

		private void Sub_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

		#endregion 显示折线图事件
        
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

			var nodeB = ((NodeBArgs)e).m_NodeB;
			ObjNode.main = this;
			//ObjNode.datagrid = this.MibDataGrid;

			// 向基站前端控件填入对应信息;
			AddNodeBPageToWindow();                    // 将基站添加到窗口页签中;
			AddNodeLabel(nodeB.FriendlyName, nodeB.NeAddress.ToString());
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

			var menu = CreateNodebMenu();   // 每个基站创建一个右键菜单，都有自己的状态

			nodeLabel.ContextMenu = menu;
			ExistedNodebList.Children.Add(nodeLabel);
		}

		// 创建基站右键菜单
		private MetroContextMenu CreateNodebMenu()
		{
			// 右键菜单的添加
			var menu = new MetroContextMenu();

			var menuItem = new MetroMenuItem { Header = "修改友好名" };
			menuItem.Click += ModifyFriendlyName_Click;
			menuItem.IsEnabled = true;
			menu.Items.Add(menuItem);

			menuItem = new MetroMenuItem { Header = "修改IP地址" };
			menuItem.Click += ModifyEnbAddr_Click;
			menuItem.IsEnabled = true;
			menu.Items.Add(menuItem);

			menuItem = new MetroMenuItem { Header = "删除" };
			menuItem.Click += DeleteStationMenu_Click;
			menuItem.IsEnabled = true;
			menu.Items.Add(menuItem);

			menu.Items.Add(new Separator());

			menuItem = new MetroMenuItem { Header = "连接基站" };
			menuItem.Click += ConnectStationMenu_Click;
			menu.Items.Add(menuItem);

			menuItem = new MetroMenuItem { Header = "断开连接" };
			menuItem.Click += DisconStationMenu_Click;
			menuItem.IsEnabled = false;
			menu.Items.Add(menuItem);

			menuItem = new MetroMenuItem { Header = "复位基站" };
			menuItem.Click += GNBRest_Click; ;
			menuItem.IsEnabled = false;
			menu.Items.Add(menuItem);

			menu.Items.Add(new Separator());

			menuItem = new MetroMenuItem { Header = "数据同步" };
			menuItem.Click += DataSync_Click;
			menuItem.IsEnabled = false;
			menu.Items.Add(menuItem);

			return menu;
		}

		/// <summary>
		/// gnb复位按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GNBRest_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult ret = MessageBox.Show("是否生成动态配置文件？", "提示", MessageBoxButton.YesNoCancel);
			if (ret == MessageBoxResult.Cancel)
			{
				return;
			}
			else if (ret == MessageBoxResult.Yes)
			{
				string strCMDName = "ResetEquip";
				Dictionary<string, string> m_dir = new dict_d_string();
				m_dir.Add("equipResetTrigger", "1");

				string strIP = CSEnbHelper.GetCurEnbAddr();
				if (strIP != null && strIP != string.Empty)
				{
					int nRet = CDTCmdExecuteMgr.CmdSetSync(strCMDName, m_dir, ".0", strIP);
					if (nRet == 0)
					{
						MessageBox.Show("复位成功");
					}
					else
					{
						MessageBox.Show("复位失败");
					}
				}
			}
			else
			{
				var header = new SiMsgHead();
				header.u16MsgType = 0x50;
				header.u16MsgLength = 4;

				string strIP = CSEnbHelper.GetCurEnbAddr();
				if (strIP != null && strIP != string.Empty)
				{
					byte[] buffer = new byte[header.u16MsgLength];
					header.SerializeToBytes(ref buffer, 0);

					if (NodeBControl.SendSiMsg(strIP, buffer))
					{
						MessageBox.Show("复位成功");
					}
					else
					{
						MessageBox.Show("复位失败");
					}
				}
			}
		}

		/// <summary>
		/// 基站节点  点击事件，获取被点击的IP地址，保存到全局变量
		/// </summary>
		private void NodeLabel_Click(object sender, EventArgs e)
		{
			var target = sender as MetroExpander;
			if (null != target)
			{
				var nodeB = NodeBControl.GetInstance().GetNodeByFName(target.Header) as NodeB;

				// 只有已经连接的基站进行点击切换时，才修改当前基站的IP
				if (nodeB.HasConnected())
				{
					CSEnbHelper.SetCurEnbAddr(nodeB.NeAddress.ToString());
				}

				//改变被点击的 node，还原之前的 node
				var Children = ExistedNodebList.Children;
				for (int i = 0; i < ExistedNodebList.Children.Count; i++)
				{
					var Item = ExistedNodebList.Children[i] as MetroExpander;
					Item.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
				}
				target.Background = new SolidColorBrush(Color.FromRgb(208, 227, 252));
				//target.Background.Opacity = 50;
				//target.Opacity = 50;
				if (m_bIsSingleMachineDebug)
				{
					InitDataBase(nodeB.NeAddress.ToString());
				}
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
			if (String.IsNullOrEmpty(targetIp) || String.IsNullOrEmpty(menuDesc))
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
			if (null == menuRoot || String.IsNullOrEmpty(header))
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
			if (String.IsNullOrEmpty(ip) || String.IsNullOrEmpty(oldText) || String.IsNullOrEmpty(newText))
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

		#endregion 添加基站

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

		#endregion 添加泳道图事件

		#region CDL的一大堆……

		// 开始解析;
		private void parseFile_Click(object sender, RoutedEventArgs e)
		{
			List<Event> le = new List<Event>();
			byte[] bytes = { 0x06, 0xD6, 0x12, 0x09, 0x00, 0x20, 0xFF, 0xFF, 0xFF, 0x28, 0xFF, 0xF0, 0x5A, 0xC4, 0x95, 0x6C, 0x1D, 0x36, 0xE3, 0xB4, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x00, 0x5C, 0x00 };
			DbOptions opts = new DbOptions();
			opts.ConnStr = DbConnSqlite.GetConnectString(DbConnProvider.DefaultSqliteDatabaseName); ;
			opts.ConnType = DbType.SQLite;
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
			string sql = String.Empty;
			EventParser parser = new EventParser();
			parser.Version = "1.3.06659";

			EventParserManager.Instance.AddEventParser(parser.Version, parser);
			Event newe = ParseEvent(bytes, "1.3.06659");
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

		private Event ParseEvent(byte[] eventsBuffer, string version)
		{
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

			if (evtNameForUeType.Equals("S1 Initial Context Setup Request") || evtNameForUeType.Equals("UECapabilityInformation"))
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
							if (!String.IsNullOrEmpty(ueType))
							{
								int startPosition = ueType.LastIndexOf(')') + 1;
								int endPosition = ueType.LastIndexOf('\'');
								int ueTypeLength = endPosition - startPosition;
								ueType = ueType.Substring(startPosition, ueTypeLength);
								evt.UeType =
									ConfigurationManager.Singleton.GetUeTypeConfiguration().
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

		private string FindValueFromTarget(string content, string keyWord)
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
				result += spaces + String.Format("  {0}\n", root.DisplayContent);
				level++;
				foreach (var child in root.Children)
				{
					ExportBodyToXml(child, ref result, level);
				}
			}
		}

		private void deleteTempFile()
		{
			string fileCdl = AppPathUtiliy.Singleton.GetAppPath() + "cdl.db";

			if (File.Exists(fileCdl))
			{
				File.Delete(fileCdl);
			}
			else
			{
			}
		}

		#endregion CDL的一大堆……

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

		#endregion 快捷键在此

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
		// 		public void UpdateMibDataGrid(IAsyncResult ar, dict_d_string oid_cn, dict_d_string oid_en, ObservableCollection<DyDataGrid_MIBModel> contentlist)
		// 		{
		// 			SnmpMessageResult res = ar as SnmpMessageResult;
		//
		// 			// 将信息回填到DataGrid当中;
		// 			MibDataGrid.Dispatcher.Invoke(new Action(() =>
		// 			{
		// 				MibDataGrid.Columns.Clear();                          //以最后一次为准即可;
		// 				dynamic model = new DyDataGrid_MIBModel();
		//
		// 				// 遍历GetNext的结果;
		// 				foreach (KeyValuePair<string, string> iter in res.AsyncState as dict_d_string)
		// 				{
		// 					Console.WriteLine("NextIndex" + iter.Key + " Value:" + iter.Value);
		//
		// 					// 通过基站反馈回来的一行结果，动态生成一个类型，用来与DataGrid对应;
		// 					foreach (var iter2 in oid_cn)
		// 					{
		// 						// 如果存在对应关系;
		// 						if (iter.Key.Contains(iter2.Key))
		// 						{
		// 							Console.WriteLine("Add Property:" + oid_en[iter2.Key] + " Value:" + iter.Value + " and Header is:" + iter2.Value);
		// 							model.AddProperty(oid_en[iter2.Key], new DataGrid_Cell_MIB()
		// 							{
		// 								m_Content = iter.Value,
		// 								oid = iter.Key,
		// 								MibName_CN = iter2.Value,
		// 								MibName_EN = oid_en[iter2.Key]
		// 							}, iter2.Value);
		// 						}
		// 					}
		// 				}
		//
		// 				// 将这个整行数据填入List;
		// 				if (model.Properties.Count != 0)
		// 				{
		// 					// 向单元格内添加内容;
		// 					contentlist.Add(model);
		// 				}
		//
		// 				foreach (var iter3 in oid_en)
		// 				{
		// 					Console.WriteLine("new binding is:" + iter3.Value + ".m_Content");
		// 					DataGridTextColumn column = new DataGridTextColumn();
		// 					column.Header = oid_cn[iter3.Key];
		// 					column.Binding = new Binding(iter3.Value + ".m_Content");
		//
		// 					MibDataGrid.Columns.Add(column);
		//
		// 				}
		//
		// 				MibDataGrid.DataContext = contentlist;
		//
		// 			}));
		// 		}

		/// 把tbl中的每一列填到model中
		private void AddPropertyForEmptyTbl(ref dynamic model, MibTable tbl)
		{
			var mod = model as DyDataGrid_MIBModel;
			if (null == mod)
			{
				return;
			}

			Func<List<MibLeaf>, int, MibLeaf> la = (leafList, index) =>
			{
				return leafList.FirstOrDefault(item => item.childNo == index);
			};

			var childList = tbl.childList;
			for (var i = 1; i <= childList.Count; i++)
			{
				var leaf = la(childList, i);
				if (null == leaf)
				{
					continue;
				}

				if (leaf.isMib != 1 || leaf.IsRowStatus())
				{
					continue;
				}

				if (leaf.IsIndex == "True" && leaf.childNo == 1)
				{
					model.AddProperty("indexlist", new DataGrid_Cell_MIB()
					{
						m_Content = null,
						oid = null,
						m_bIsReadOnly = SnmpToDatabase.GetReadAndWriteStatus(leaf.childNameMib, CSEnbHelper.GetCurEnbAddr()),
						MibName_CN = "实例描述",
						MibName_EN = "indexlist"
					}, "实例描述");
				}
				else
				{
					model.AddProperty(leaf.childNameMib, new DataGrid_Cell_MIB()
					{
						m_Content = null,
						oid = null,
						m_bIsReadOnly = SnmpToDatabase.GetReadAndWriteStatus(leaf.childNameMib, CSEnbHelper.GetCurEnbAddr()),
						MibName_CN = leaf.childNameCh,
						MibName_EN = leaf.childNameMib
					}, leaf.childNameCh);
				}
			}
		}

		/// <summary>
		/// 当SNMP模块收集全整表数据后，调用该函数;
		/// </summary>
		/// <param name="ar">GetNext之后，得到的整表数据;</param>
		/// <param name="oid_cn"></param>
		/// <param name="oid_en"></param>
		/// <param name="contentlist"></param>
		public void UpdateAllMibDataGrid(Dictionary<string, Dictionary<string, string>> ar, dict_d_string oid_cn, dict_d_string oid_en,
			ObservableCollection<DyDataGrid_MIBModel> contentlist, string ParentOID, int IndexCount, MibTable mibTable)
		{
			var action = new Action<Dictionary<string, Dictionary<string, string>>, dict_d_string, dict_d_string, ObservableCollection<DyDataGrid_MIBModel>, string, int, MibTable>(UpdateMibDataGridCallback);

			//MibDataGrid.Dispatcher.Invoke(action, new object[] {ar, oid_cn , oid_en, contentlist, ParentOID, IndexCount});
			this.Main_Dynamic_DataGrid.Dispatcher.Invoke(action, new object[] { ar, oid_cn, oid_en, contentlist, ParentOID, IndexCount, mibTable });
		}

        /// <summary>
        ///
        /// </summary>
        /// <param name="ar"></param>
        /// <param name="oid_cn">OID与其中文友好名的对照字典</param>
        /// <param name="oid_en">OID与其英文友好名，即字段名称的对应字典</param>
        /// <param name="contentlist">包含了所有要显示内容的集合</param>
        /// <param name="ParentOID"></param>
        /// <param name="IndexCount">真实的索引个数</param>
        private void UpdateMibDataGridCallback(Dictionary<string,Dictionary<string,string>> ar, dict_d_string oid_cn, dict_d_string oid_en,
            ObservableCollection<DyDataGrid_MIBModel> contentlist, string ParentOID, int IndexCount, MibTable mibTable)
        {
            Main_Dynamic_DataGrid.DynamicDataGrid.DataContext = null;

            // 需要为记录为空的表生成表头，否则无法执行添加实例操作
            if (ar.Count == 0)
            {
                Main_Dynamic_DataGrid.PageInfo.Visibility = Visibility.Collapsed;//无数据时不显示分页信息
                ShowProgressBar(0, Visibility.Collapsed);
                if (mibTable == null)
                {
                    return;
                }

                dynamic model = new DyDataGrid_MIBModel();
                model.AddTableProperty(mibTable);
                AddPropertyForEmptyTbl(ref model, mibTable);

                Main_Dynamic_DataGrid.ColumnModel = model;
                Main_Dynamic_DataGrid.DynamicDataGrid.DataContext = contentlist;
                return;
            }
            Main_Dynamic_DataGrid.LineDataList.Clear();
            Main_Dynamic_DataGrid.LineDataList = ar;

            if (Main_Dynamic_DataGrid.LineDataList.Count == 0)
                return;
            Main_Dynamic_DataGrid.SetDataGridInfo(oid_cn,oid_en,mibTable,IndexCount);
            Main_Dynamic_DataGrid.RefreshDataGridPage(1);

            ShowProgressBar(0, Visibility.Collapsed);
        }
        /*private void UpdateMibDataGridCallback(dict_d_string ar, dict_d_string oid_cn, dict_d_string oid_en,
			ObservableCollection<DyDataGrid_MIBModel> contentlist, string ParentOID, int IndexCount, MibTable mibTable)
		{
			Main_Dynamic_DataGrid.DynamicDataGrid.DataContext = null;

			int RealIndexCount = IndexCount;                         // 真实的索引维度;

			if (IndexCount == 0)                                     // 如果索引个数为0，按照1来处理;
				IndexCount = 1;

			var AlreadyRead = new List<string>();
			var itemCount = 0;

			// 需要为记录为空的表生成表头，否则无法执行添加实例操作
			if (ar.Count == 0)
			{
				if (mibTable == null)
				{
					return;
				}

				dynamic model = new DyDataGrid_MIBModel();
				model.AddTableProperty(mibTable);
				AddPropertyForEmptyTbl(ref model, mibTable);

				Main_Dynamic_DataGrid.ColumnModel = model;
				Main_Dynamic_DataGrid.DynamicDataGrid.DataContext = contentlist;
				return;
			}

			// ar是遍历GetNext的结果，遍历后，将其转化为DataGrid能够显示的类型;
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

				// 创建一个能够填充到DataGrid控件的动态类型，这个类型的所有属性来自于读取的所有MIB节点;
				dynamic model = new DyDataGrid_MIBModel();

				// 当多因维度大于0，即该表为矢量表的时候为该列添加表头;
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

					// 如下DataGrid_Cell_MIB中的 oid暂时填写成这样;
					// 参数一：属性名称;
					// 参数二：单元格实例;
					// 参数三：单元格列中文名称;
					model.AddProperty("indexlist", new DataGrid_Cell_MIB()
					{
						m_Content = IndexContent,
						oid = IndexOIDPre + ".",
						m_bIsReadOnly = true,
						MibName_CN = "实例描述",
						MibName_EN = "indexlist"
					}, "实例描述");
				}

				if (mibTable != null)
				{
					model.AddTableProperty(mibTable);
				}

				// 将ar当中所有匹配的结果取出,最后会取出了一行数据;
				foreach (var iter3 in ar)
				{
					// 将所有相同索引取出;
					temp = iter3.Key.Split('.');
					string TempIndex = "";

					for (int i = temp.Length - IndexCount; i < temp.Length; i++)
					{
						TempIndex += "." + temp[i];
					}

					// 该步骤为抽取同样索引的内容，组成一行数据，然后添加至model中;
					// 以前的写法有问题，比如0.0.10包含了0.0.1，会有误判的情况，此处修改by tangyun;
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

							// 在这里要区分DataGrid要显示的数据类型;
							var dgm = DataGridCellFactory.CreateGridCell(oid_en[temp_compare], oid_cn[temp_compare]
								, iter3.Value, iter3.Key, CSEnbHelper.GetCurEnbAddr());

							// 第一个参数：属性的名称——节点英文名称;
							// 第二个参数：属性的实例——DataGrid_Cell_MIB实例;
							// 第三个参数：列要显示的中文名——节点的中文友好名;
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
					itemCount++;
				}

				// 最终全部收集完成后，为控件赋值;
				if (itemCount == contentlist.Count)
				{
					Main_Dynamic_DataGrid.ColumnModel = model;
					Main_Dynamic_DataGrid.DynamicDataGrid.DataContext = contentlist;
				}
			}
			// 增加表量表索引的列名;
			//if (RealIndexCount > 0)
			//{
			//	var column = new DataGridTextColumn
			//	{
			//		Header = "实例描述",
			//		Binding = new Binding("indexlist.m_Content")
			//	};
			//	//MibDataGrid.Columns.Add(column);
			//}

			// 所有需要显示的列名都确认好了，在DataGrid中添加列;
			//foreach (var iter3 in oid_en)
			//{
			//	string[] temp = iter3.Key.Split('.');
			//	if ((RealIndexCount > 0) && (Int32.Parse(temp[temp.Length - 1]) <= RealIndexCount))
			//	{
			//		// 索引不对应列名;
			//		continue;
			//	}

			//	// 当前添加的表格类型只有Text类型，应该使用工厂模式添加对应不同的数据类型;
			//	var column = new DataGridTextColumn
			//	{
			//		Header = oid_cn[iter3.Key],
			//		Binding = new Binding(iter3.Value + ".m_Content")
			//	};

			//	//MibDataGrid.Columns.Add(column);
			//}

			//MibDataGrid.DataContext = contentlist;
		}*/

        #endregion DataGrid相关处理

		private void FileManagerTabItem_GotFocus(object sender, RoutedEventArgs e)
		{
			MetroExpander_Click(sender, e);
		}

		/// <summary>
		/// 弹出文件管理;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MetroExpander_Click(object sender, EventArgs e)
		{
			var enbIp = CSEnbHelper.GetCurEnbAddr();
			if (enbIp == null)
			{
				MessageBox.Show("未选择基站，请单击需要显示的基站");
				return;
			}

			string strFriendName = NodeBControl.GetInstance().GetFriendlyNameByIp(enbIp);

			foreach (LayoutAnchorable item in listAvalon)
			{
				//为了在文件管理中增加说明本地or基站。。
				if (item.Title.Contains(strFriendName + "  "))
				{
					item.Show();
					return;
				}
			}

			var content = new TestTwoFileManager(enbIp);

			var sub = new LayoutAnchorable
			{
				Content = content,
				Title = strFriendName + "     本地(左侧)  <-->  基站(右侧)",
				FloatingHeight = 800,
				FloatingWidth = 600,
				CanHide = true,
				CanClose = false,
				CanAutoHide = false
			};

			listAvalon.Add(sub);
			FileManagerLAP.Children.Add(sub);
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			//可以记录日志并转向错误bug窗口友好提示用户
			if (e.ExceptionObject is Exception)
			{
				Exception ex = (Exception)e.ExceptionObject;
				Log.WriteLogFatal(ex);
				MessageBox.Show(ex.Message);
			}
		}

		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
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
			SubscribeHelper.AddSubscribe(TopicHelper.ReconnectGnb, OnReconnGnb);
		}

		// 打印日志
		private void OnShowLog(SubscribeMsg msg)
		{
			var logInfo = ShowLogHelper.GetLogInfo(msg.Data);
			if (null == logInfo)
			{
				return;
			}
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
		private async void OnConnect(SubscribeMsg msg)
		{
			var ip = Encoding.UTF8.GetString(msg.Data);
			if (string.IsNullOrEmpty(ip))
			{
				throw new ArgumentNullException();
			}

			var fname = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);
			ShowLogHelper.Show($"成功连接基站：{fname}-{ip}", $"{ip}");
			var result = await InitDataBase(ip);      // todo lm.dtz文件不存在时，这里抛出异常

			ChangeMenuHeaderAsync(ip, "取消连接", "连接基站");
			EnableMenu(ip, "连接基站", false);
			EnableMenu(ip, "断开连接");
			EnableMenu(ip, "数据同步");
			EnableMenu(ip, "复位基站");
			EnableMenu(ip, "修改友好名", false);
			EnableMenu(ip, "修改IP地址", false);

			if (!result)
			{
				Log.Error("数据库初始化失败，不再查询基站的设备信息");
				return;
			}

			// 查询基站类型是4G还是5G基站
			var st = EnbTypeEnum.ENB_EMB5116;
			st = GetEquipType(ip);
			if (is4GConn == false)
			{
				if (st != EnbTypeEnum.ENB_EMB6116)
				{
					ShowLogHelper.Show($"当前不支持除5G基站外的基站，将断开连接：{fname}-{ip}", $"{ip}");
					NodeBControl.GetInstance().DisConnectNodeb(fname);
					return;
				}
			}

			NodeBControl.GetInstance().SetNodebGridByIp(ip, st);
		}

		// 断开连接
		private void OnDisconnect(SubscribeMsg msg)
		{
			var ip = Encoding.UTF8.GetString(msg.Data);
			if (string.IsNullOrEmpty(ip))
			{
				throw new ArgumentNullException();
			}

			var fname = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);
			ShowLogHelper.Show($"基站连接断开：{fname}-{ip}", $"{ip}");
			ChangeMenuHeaderAsync(ip, "取消连接", "连接基站");
			EnableMenu(ip, "连接基站");
			EnableMenu(ip, "断开连接", false);
			EnableMenu(ip, "数据同步", false);
			EnableMenu(ip, "复位基站", false);
			EnableMenu(ip, "修改友好名");
			EnableMenu(ip, "修改IP地址");

			// 文件管理按钮禁用，文件管理窗口关闭
			CloseFileMgrDlg(fname);
			//关闭网规界面
			NetPlanClose();

			Dispatcher.Invoke(() =>
			{
				ExpanderBaseInfo.IsEnabled = false;
				ExpanderBaseInfo.IsExpanded = false;
				TabControlEnable(false);
				Obj_Root.Children?.Clear();
				//MibDataGrid.Columns.Clear();
				MainHorizenTab.SelectedIndex = 0;
                ShowProgressBar(0,Visibility.Collapsed);
			});

			// 断开后，当前已经连接的基站信息清空
			CSEnbHelper.ClearCurEnbAddr(ip);
		}

		/// <summary>
		/// 查询基站的类型
		/// </summary>
		/// <param name="targetIp"></param>
		/// <returns></returns>
		public EnbTypeEnum GetEquipType(string targetIp)
		{
			const string cmdName = "GetEquipmentCommonInfo";
			long reqId;
			var pdu = new CDTLmtbPdu(cmdName);
			int ret = CDTCmdExecuteMgr.GetInstance().CmdGetSync(cmdName, out reqId, "0", targetIp, ref pdu);
			if (ret != 0 || pdu.m_LastErrorStatus != 0)
			{
				ShowLogHelper.Show("查询设备信息失败，无法判断基站型号", targetIp, InfoTypeEnum.ENB_GETOP_ERR_INFO);
				return EnbTypeEnum.ENB_EMB6116;
			}
			string equipType;
			if (!pdu.GetValueByMibName(targetIp, "equipNEType", out equipType))
			{
				ShowLogHelper.Show("查询基站设备信息失败，无法判断基站型号", targetIp, InfoTypeEnum.ENB_GETOP_ERR_INFO);
				return EnbTypeEnum.ENB_EMB6116;
			}

			Log.Info($"基站设备类型是{equipType}");
			return (EnbTypeEnum)Convert.ToInt32(equipType);
		}

		private void OnReconnGnb(SubscribeMsg msg)
		{
			var targetIp = Encoding.UTF8.GetString(msg.Data);
			if (string.IsNullOrEmpty(targetIp))
			{
				throw new ArgumentNullException();
			}

			var gnb = NodeBControl.GetInstance().GetNodeByIp(targetIp) as NodeB;
			if (null != gnb)
			{
				Dispatcher.BeginInvoke(new Action(() => ConnectAction(gnb)));
			}
		}

		#endregion 订阅消息及处理

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
		private async void OnLoadLmdtzToVersionDb(SubscribeMsg msg)
		{

			return;     // todo 下面的流程不执行

			// 不能用netaddr反解析，不包含路径信息
			var netAddr = JsonHelper.SerializeJsonToObject<NetAddr>(msg.Data);

			var ip = netAddr.TargetIp;

			var fname = NodeBControl.GetInstance().GetFriendlyNameByIp(ip);

			var result = await InitDataBase(ip);

			if (result)
			{
				ShowLogHelper.Show($"解析lm.dtz数据文件成功：{fname}-{ip}", $"{ip}");
			}
			else
			{
				ShowLogHelper.Show($"解析lm.dtz数据文件失败：{fname}-{ip}", $"{ip}");
			}
		}

		/// <summary>
		/// 用于设置tab页面是否可用
		/// </summary>
		/// <param name="isEnable"></param>
		private void TabControlEnable(bool isEnable)
		{
			ItemCollection collection = MainHorizenTab.Items;
			for (int i = 0; i < collection.Count; i++)
				(collection[i] as TabItem).IsEnabled = isEnable;
		}

		private void MetroImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			LayoutAnchorable sub = new LayoutAnchorable();
			FlowChart content = new FlowChart();

			sub.Content = content;
			//sub.FloatingHeight = 300;
			//sub.FloatingWidth = 800;

			Pane.Children.Add(sub);
			sub.Float();
		}

		private bool bInitNetPlan = false;
		private View.NetPlan g_NetPlan;

		/// <summary>
		/// 切换到网规界面的时候
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_GotFocus(object sender, RoutedEventArgs e)
		{
			//netPlanScrollView
			if (!bInitNetPlan)
			{
				bInitNetPlan = true;
				g_NetPlan = new View.NetPlan();
				netPlanScrollView.Content = g_NetPlan;
			}
			else
			{
				g_NetPlan.ShowAvalonPanel();
			}
		}

		private void NetPlanClose()
		{
			if (bInitNetPlan)
			{
				bInitNetPlan = false;
				g_NetPlan?.NetPlanClean();
				g_NetPlan = null;
				GC.Collect();
			}
		}

		/// <summary>
		/// 配置文件解析工具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnConfigFileOperate_Click(object sender, RoutedEventArgs e)
		{
			View.ConfigFileOperate dlgConfigFile = new View.ConfigFileOperate();
			dlgConfigFile.ShowDialog();
		}

        /// <summary>
        /// 点击树节点显示列表时，在状态栏显示进度条进度
        /// </summary>
        /// <param name="value">进度</param>
        /// <param name="isVisible">是否显示进度条</param>
        public void ShowProgressBar(double value, Visibility showstatus)
        {
            Dispatcher.Invoke(() =>
            {
                if (progressBar == null)
                    return;

                progressBar.Value = value;
                progressBar.Visibility = showstatus;
            });
        }
    }
}