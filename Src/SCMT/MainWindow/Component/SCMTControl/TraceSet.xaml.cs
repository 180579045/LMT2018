using CDLBrowser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using CDLBrowser.Parser;
using MsgQueue;
using SCMTMainWindow.Component;

namespace SCMTMainWindow.Component.SCMTControl
{
	/// <summary>
	/// TraceSet.xaml 的交互逻辑
	/// </summary>
	public partial class TraceSet : Window
	{
		public TraceSet()
		{
			InitializeComponent();
		}

		/// <summary>
		///  控件数据
		///  </summary>
		private IList<TraceSetTreeModel> _itemSourceData;

		public IList<TraceSetTreeModel> ItemSourceData
		{
			get { return _itemSourceData; }
			set
			{
				_itemSourceData = value;
				MainTree.ItemsSource = _itemSourceData;
			}
		}

		/// <summary>
		/// 单例实例
		/// </summary>
		private static TraceSet m_Instance;

		public static TraceSet CreateInstance()
		{
			return m_Instance ?? (m_Instance = new TraceSet());
		}


		/// <summary>
		/// 获取  未被选中的  项
		/// </summary>
		/// <returns></returns>
		public IList<TraceSetTreeModel> GetUncheckedItems()
		{
			return GetUncheckedItemsPrivate(_itemSourceData);
		}

		/// <summary>
		/// 私有函数  获取未被选中的  项  递归调用
		/// </summary>
		/// <param name="treeList"></param>
		/// <returns></returns>
		private IList<TraceSetTreeModel> GetUncheckedItemsPrivate(IList<TraceSetTreeModel> treeList)
		{
			IList<TraceSetTreeModel> unSelectedTreeList = new List<TraceSetTreeModel>();

			foreach (var item in treeList)
			{
				//如果被选中，遍历子项，查找未被选中的项
				if (item.ISChecked)
				{
					if (item.Children.Count != 0)
					{
						foreach (var child in GetUncheckedItemsPrivate(item.Children))
						{
							unSelectedTreeList.Add(child);
						}
					}
				}
				else
				{
					if (item.Children.Count != 0)
					{
						foreach (var child in GetUncheckedItemsPrivate(item.Children))
						{
							unSelectedTreeList.Add(child);
						}
					}
					else
					{
						unSelectedTreeList.Add(item);
					}
				}
			}

			return unSelectedTreeList;
		}

		/// <summary>
		/// 获取选中的项
		/// </summary>
		/// <returns></returns>
		public IList<TraceSetTreeModel> GetCheckedItems()
		{
			return GetCheckedItemsPrivate(_itemSourceData);
		}

		/// <summary>
		/// 私有函数，忽略层次关系，获取所有选中项
		/// </summary>
		/// <param name="treeList"></param>
		/// <returns></returns>
		private IList<TraceSetTreeModel> GetCheckedItemsPrivate(IList<TraceSetTreeModel> treeList)
		{
			IList<TraceSetTreeModel> selectedTreeList = new List<TraceSetTreeModel>();

			foreach (var item in treeList)
			{
				if (item.ISChecked)
				{
					//   selectedTreeList.Add(item);

					if (item.Children.Count == 0)
					{
						selectedTreeList.Add(item);
					}
					else
					{
						foreach (var child in GetCheckedItemsPrivate(item.Children))
						{
							selectedTreeList.Add(child);
						}
					}
				}
			}

			return selectedTreeList;
		}

		/// <summary>
		/// 展开所有子项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExpandAll_Click(object sender, RoutedEventArgs e)
		{
			foreach (TraceSetTreeModel tree in MainTree.ItemsSource)
			{
				tree.IsExpanded = true;
				tree.SetAllChildrenExpended(true);
			}
		}

		/// <summary>
		/// 折叠所有子项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UnExpandAll_Click(object sender, RoutedEventArgs e)
		{
			foreach (TraceSetTreeModel tree in MainTree.ItemsSource)
			{
				tree.IsExpanded = false;
				tree.SetAllChildrenExpended(false);
			}
		}

		/// <summary>
		/// 全部选中子项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckAll_Click(object sender, RoutedEventArgs e)
		{
			foreach (TraceSetTreeModel tree in MainTree.ItemsSource)
			{
				tree.ISChecked = true;
				tree.SetAllChildrenChecked(true);
			}
		}

		/// <summary>
		/// 全部注销选中子项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UnCheckAll_Click(object sender, RoutedEventArgs e)
		{
			foreach (TraceSetTreeModel tree in MainTree.ItemsSource)
			{
				tree.ISChecked = false;
				tree.SetAllChildrenChecked(false);
			}
		}

		/// <summary>
		/// 选中所有  被选中item的子项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuSelectAllChildren_Click(object sender, RoutedEventArgs e)
		{
			if (MainTree.SelectedItem != null)
			{
				TraceSetTreeModel tree = (TraceSetTreeModel)MainTree.SelectedItem;
				tree.ISChecked = true;
				tree.SetAllChildrenChecked(true);
			}
		}

		/// <summary>
		/// 鼠标右键点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			TreeViewItem item = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
			if (item != null)
			{
				item.Focus();
				e.Handled = true;
			}

		}
		static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
		{
			while (source != null && source.GetType() != typeof(T))
				source = VisualTreeHelper.GetParent(source);

			return source;
		}

		/// <summary>
		/// 显示  Name  的  TextBlock  控件，鼠标左键按下时触发，查找 xml 文件中相关的选项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			TextBlock tbName = e.Source as TextBlock;
			string strNodeName = tbName.Text;
			List<string> lstrChildrenName = new List<string>();

			// TraceSwitch 中名称和 opcode 中的名称不同，需要转换，前缀替换为 trace
			if (strNodeName.StartsWith("LTE"))
			{
				strNodeName = strNodeName.Replace("LTE", "TRACE");
			}
			else if (strNodeName.StartsWith("TD"))
			{
				strNodeName = strNodeName.Replace("TD_", "");
			}

			TextList.Items.Clear();

			//获取配置文件中当前点击的节点的配置信息
			GetopcodeName(lstrChildrenName, strNodeName);

			//如果当前节点  存在相关配置项，添加到 listbox 中
			if (lstrChildrenName.Count > 0)
			{
				TextList.Items.Add("Add all");

				for (int i = 0; i < lstrChildrenName.Count; i++)
				{
					TextList.Items.Add(lstrChildrenName[i]);
				}
			}
		}

		/// <summary>
		/// 获取具体 SwitchName 对应的  Name 列表
		/// </summary>
		/// <param name="lstrNameList">出参，返回读取到的名称列表</param>
		/// <param name="SwitchName">待查询的 SwitchName</param>
		private void GetopcodeName(List<string> lstrNameList, string SwitchName)
		{
			XmlDocument doc = new XmlDocument();
			lstrNameList.Clear();
			bool bFlag = false;

			try
			{
				doc.Load(".\\Component\\Configration\\opcode.xml");

				XmlNodeList nodeList = doc.SelectNodes("/Root/OperationCode[SwitchName='" + SwitchName + "']");

				foreach (XmlNode node in nodeList)
				{
					XmlNode nodeItem = node.SelectSingleNode("Name");

					for (int i = 0; i < lstrNameList.Count; i++)
					{
						if (nodeItem.InnerText == lstrNameList[i])
						{
							bFlag = true;
						}
						else
						{
							bFlag = false;
						}
					}//for(int i...)
					if (!bFlag)
					{
						lstrNameList.Add(nodeItem.InnerText);
					}
				}//foreach
			}
			catch (Exception)
			{
				MessageBox.Show("加载opcode.xml配置文件失败");
			}

		}


		/// <summary>
		/// 全局变量，主 Tree
		/// </summary>
		public IList<TraceSetTreeModel> tmList = new List<TraceSetTreeModel>();

		/// <summary>
		/// 窗口加载时触发，读取配置文件，加载到窗口显示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			tvDataBinding();
		}

		/// <summary>
		/// 数据绑定 解析XML文件的数据并绑定
		/// </summary>
		private void tvDataBinding()
		{
			List<string> myList = new List<string>();

			/// 从配置文件TraceSwitch.xml中读取 BoardStyleName
			GetBoardStyleName(myList);

			///根据每个BoardStyleName，获取其中具体的子项
			for (int i = 0; i < myList.Count; i++)
			{
				TraceSetTreeModel subTree = new TraceSetTreeModel();
				subTree.Name = myList[i].ToString();
				subTree.IsExpanded = false;

				//获取 BoardStyleName 中，具体的  Name 列表
				List<string> nameList = new List<string>();
				GetName(nameList, myList[i]);

				if (myList[i] == "SCP")
				{
					//分别存储 LTE 和 TD
					List<string> strLTEList = new List<string>();
					List<string> strTDList = new List<string>();
					GetLTEAndTD(strLTEList, strTDList, nameList);

					TraceSetTreeModel treeLTE = new TraceSetTreeModel();
					treeLTE.Name = "LTE";
					treeLTE.Parent = subTree;
					foreach (string strLTE in strLTEList)
					{
						TraceSetTreeModel childLTETree = new TraceSetTreeModel();
						childLTETree.Name = strLTE;
						childLTETree.Parent = treeLTE;

						treeLTE.Children.Add(childLTETree);
					}
					subTree.Children.Add(treeLTE);

					TraceSetTreeModel treeTD = new TraceSetTreeModel();
					treeTD.Name = "TD";
					treeTD.Parent = subTree;
					foreach (string strTD in strTDList)
					{
						TraceSetTreeModel childTDTree = new TraceSetTreeModel();
						childTDTree.Name = strTD;
						childTDTree.Parent = treeTD;

						treeTD.Children.Add(childTDTree);
					}
					subTree.Children.Add(treeTD);
				}
				else
				{
					for (int j = 0; j < nameList.Count; j++)
					{
						TraceSetTreeModel childTree = new TraceSetTreeModel();
						childTree.Name = nameList[j].ToString();
						childTree.Parent = subTree;

						subTree.Children.Add(childTree);
					}
				}

				tmList.Add(subTree);
			}

			ItemSourceData = tmList;
		}

		/// <summary>
		/// 从配置文件TraceSwitch.xml中读取BoardStyleName
		/// </summary>
		/// <param name="lstrBoardStyleName">出参，返回读取到的名称列表</param>
		private void GetBoardStyleName(List<string> lstrBoardStyleName)
		{
			XmlDocument bsnDoc = new XmlDocument();
			lstrBoardStyleName.Clear();
			bool bFlag = false;    //查重

			try
			{
				//加载配置文件，从配置文件中查找节点 TraceSwitch，保存到xmlNodeList中
				bsnDoc.Load(".\\Component\\Configration\\traceswitch.xml");
				XmlNodeList bsnList = bsnDoc.SelectNodes("/Root/TraceSwitch");

				//从每个 TraceSwitch 中获取 BoardStyleName
				foreach (XmlNode bsnNode in bsnList)
				{
					XmlNode bsnName = bsnNode.SelectSingleNode("BoardStyleName");

					//查看是否重复
					for (int i = 0; i < lstrBoardStyleName.Count; i++)
					{
						if (bsnName.InnerText == lstrBoardStyleName[i])
						{
							bFlag = true;
						}
						else
						{
							bFlag = false;
						}
					}
					if (!bFlag)
					{
						lstrBoardStyleName.Add(bsnName.InnerText);
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("加载traceswitch.xml配置文件失败");
			}
		}

		/// <summary>
		/// 获取具体 BoardStyleName 中，
		/// </summary>
		/// <param name="lstrNameList">出参，返回读取到的名称列表</param>
		/// <param name="BoardStyleName">待查询的 BoardStyleName</param>
		private void GetName(List<string> lstrNameList, string BoardStyleName)
		{
			XmlDocument doc = new XmlDocument();
			lstrNameList.Clear();
			bool bFlag = false;

			try
			{
				doc.Load(".\\Component\\Configration\\traceswitch.xml");

				XmlNodeList nodeList = doc.SelectNodes("/Root/TraceSwitch[BoardStyleName='" + BoardStyleName + "']");

				foreach (XmlNode node in nodeList)
				{
					XmlNode nodeItem = node.SelectSingleNode("Name");

					for (int i = 0; i < lstrNameList.Count; i++)
					{
						if (nodeItem.InnerText == lstrNameList[i])
						{
							bFlag = true;
						}
						else
						{
							bFlag = false;
						}
					}//for(int i...)
					if (!bFlag)
					{
						lstrNameList.Add(nodeItem.InnerText);
					}
				}//foreach
			}
			catch (Exception)
			{
				MessageBox.Show("加载traceswitch.xml配置文件失败");
			}

		}

		/// <summary>
		/// 区分  SCP 中的 LTE 开头和 TD 开头的数据，分别保存
		/// </summary>
		/// <param name="strLTEList">待保存的 LTE 数据</param>
		/// <param name="strTDlist">待保存的 TD 数据</param>
		/// <param name="sourceList">需要查询的源数据列表</param>
		private void GetLTEAndTD(List<string> strLTEList, List<string> strTDlist, List<string> sourceList)
		{
			var queryLTE = from LTE in sourceList
						   where LTE.StartsWith("LTE")
						   select LTE;
			foreach (var LTE in queryLTE)
			{
				strLTEList.Add(LTE);
			}

			var queryTD = from TD in sourceList
						  where TD.StartsWith("TD")
						  select TD;
			foreach (var TD in queryTD)
			{
				strTDlist.Add(TD);
			}
		}

		/// <summary>
		/// 发送开关按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			IList<TraceSetTreeModel> selectList = GetCheckedItems();
			List<string> lstrSelectName = new List<string>();

			string strTest = string.Empty;

			byte[] arrySwitch = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				arrySwitch[i] = 0;
			}


			for (int i = 0; i < selectList.Count; i++)
			{
				//lstrSelectName.Add(selectList[i].Name);
				int nID = GetIDByName(selectList[i].Name);

				arrySwitch[nID] = 1;

				strTest += nID.ToString() + ": " + arrySwitch[nID].ToString() + "\n";

			}

			PublishHelper.PublishMsg("/AtpBack/TraceConfig/StartTrace", arrySwitch);
		ParseMessageWindow Pw = new ParseMessageWindow();
			Pw.Show();
		}

		private int GetIDByName(string strName)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(".\\Component\\Configration\\traceswitch.xml");

			XmlNodeList docNodeList = doc.SelectNodes("/Root/TraceSwitch[Name='" + strName + "']");

			XmlNode node = docNodeList[0];

			XmlNode idNode = node.SelectSingleNode("ID");

			return int.Parse(idNode.InnerText);

		}

		/// <summary>
		/// 窗口关闭之后清除对象
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Closed(object sender, EventArgs e)
		{
			m_Instance = null;
		}
	}
}
