/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：ObjTreeNode.cs
// 文件功能描述：对象树节点类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LmtbSnmp;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SCMTMainWindow.Component.SCMTControl;
using SCMTMainWindow.Component.ViewModel;
using SCMTOperationCore.Elements;
using UICore.Controls.Metro;
using LinkPath;
using CommonUtility;
using LogManager;
using System.Windows.Media;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SCMTMainWindow
{
	/// <summary>
	/// 对象树节点抽象类;
	/// 说明:由此可衍生出各种类型的节点，诸如普通查询\小区\传输\分制式显示;
	/// </summary>
	public abstract class ObjNode
	{
		public MetroExpander selectedItem;

		public string version { get; set; }                            // 对象树版本号;

		public int ObjID { get; set; }                                 // 节点ID;

		public int ObjParentID { get; set; }                           // 父节点ID;

		public string ObjName { get; set; }                            // 节点名称;

		public List<string> OIDList { get; set; }                      // 节点包含OID列表;

		public string ObjTableName { get; set; }                       // 节点对应的表名;

		public List<ObjNode> SubObj_Lsit { get; set; }                 // 孩子节点列表;

		public Grid m_NB_ContentGrid { get; set; }                     // 对应的MIB内容容器;

		public MetroScrollViewer m_NB_Base_Contain { get; set; }       // 对应的基站基本信息容器;

		public DataGrid m_NB_Content { get; set; }                     // 对应的MIB内容;

		protected Dictionary<string, string> name_cn { get; set; }     // 属性名与中文名对应关系;(后续独立挪到DataGridControl中)

		protected Dictionary<string, string> oid_cn { get; set; }      // oid与中文名对应关系;

		protected Dictionary<string, string> oid_en { get; set; }      // oid与英文名对应关系;

		protected ObservableCollection<DyDataGrid_MIBModel> contentlist { get; set; }            // 用来保存内容;

		public static MainWindow main { get; set; }                    // 保存与之对应的主窗口;

		public static NodeB nodeb { get; set; }                        // 对应的基站;

		public static DTDataGrid datagrid { get; set; }                // 对应的界面表格;

		public abstract void Add(ObjNode obj);                         // 增加孩子节点;

		public abstract void Remove(ObjNode obj);                      // 删除孩子节点;

		public event EventHandler IsExpandedChanged;                   // 树形结构展开时;

		public event EventHandler IsSelectedChanged;                   // 树形结构节点被选择时;

		public event MouseButtonEventHandler IsRightMouseDown;         // 右键选择节点时;

		protected static string prev_oid = "1.3.6.1.4.1.5105.100.";    // DataBase模块保存的是部分OID，这个是前半部分;
		protected static Dictionary<string, string> GetNextResList;    // GetNext结果;
		protected static int LastColumn = 0;                           // 整行最后一个节点;

		public static string ObjParentOID { get; set; }                // 父节点OID;

		public static int IndexCount { get; set; }                     // 索引个数;

		public static int ChildCount { get; set; }                     // 孩子节点的个数;

		public static MibTable nodeMibTable { get; set; }        //每个节点对应的Mib信息

		public static bool isGetParaTaskRunning = false;				//异步获取参数任务是否正在进行

		/// <summary>
		/// 对象树节点点击事件;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public abstract void ClickObjNode(object sender, EventArgs e);

		/// <summary>
		/// 递归某个节点的所有孩子节点，并填入对象树容器中;
		/// </summary>
		public void TraverseChildren(MetroExpander Obj_Tree, StackPanel Lists, int depths)
		{
			var margin = new Thickness();
			// 遍历所有对象树节点;
			foreach (var Obj_Node in SubObj_Lsit)
			{
				// 只有枝节点才能够加入对象树;
				if (Obj_Node is ObjTreeNode)
				{
					// 新建一个对象树节点容器控件;
					var item = new MetroExpander();

					// 判断孩子中是否还包含枝节点;
					var NotContainTree = true;
					foreach (var isTree in Obj_Node.SubObj_Lsit)
					{
						if (isTree is ObjTreeNode)
						{
							NotContainTree = false;
						}
					}

					// 将孩子没有枝节点的节点进行缩进处理;
					if (NotContainTree)
					{
						margin.Left = 35 + depths;
						margin.Top = 8;
						margin.Bottom = 8;
						item.Margin = margin;
					}
					else
					{
						margin.Left = 20 + depths;
						margin.Top = 8;
						margin.Bottom = 8;
						item.ARMargin = margin;
					}

					item.Header = Obj_Node.ObjName;
					item.SubExpender = Lists;                           // 增加子容器,保存叶子节点;
					item.obj_type = Obj_Node;                           // 将节点添加到容器控件中;
					item.Click += IsSelectedChanged;                    // 点击事件;
					item.MouseRightButtonDown += IsRightMouseDown;

					Obj_Tree.Add(item);

					// 递归孩子节点;
					Obj_Node.TraverseChildren(item, Lists, depths + 15);
				}
			}
		}

		/// <summary>
		/// 遍历收藏夹节点;
		/// </summary>
		/// <param name="Obj_Tree"></param>
		/// <param name="Lists"></param>
		/// <param name="depths"></param>
		public void TraverseCollectChildren(MetroExpander Obj_Tree, StackPanel Lists, int depths)
		{
			var margin = new Thickness();
			// 遍历所有对象树节点;
			foreach (var Obj_Node in SubObj_Lsit)
			{
				// 只有枝节点才能够加入对象树;
				if (Obj_Node is ObjTreeNode)
				{
					// 新建一个对象树节点容器控件;
					var item = new MetroExpander();

					// 判断孩子中是否还包含枝节点;
					var NotContainTree = true;
					if (Obj_Node.SubObj_Lsit != null)
					{
						foreach (var isTree in Obj_Node.SubObj_Lsit)
						{
							if (isTree is ObjTreeNode)
							{
								NotContainTree = false;
							}
						}
					}
					// 将孩子没有枝节点的节点进行缩进处理;
					if (NotContainTree)
					{
						margin.Left = 35 + depths;
						margin.Top = 8;
						margin.Bottom = 8;
						item.Margin = margin;
					}
					else
					{
						margin.Left = 20 + depths;
						margin.Top = 8;
						margin.Bottom = 8;
						item.ARMargin = margin;
					}

					item.Header = Obj_Node.ObjName;
					item.SubExpender = Lists;                           // 增加子容器,保存叶子节点;
					item.obj_type = Obj_Node;                           // 将节点添加到容器控件中;
					item.Click += IsSelectedChanged;                    // 点击事件;
																		//item.MouseRightButtonDown += IsRightMouseDown;

					Obj_Tree.Add(item);
					// 递归孩子节点;
					Obj_Node.TraverseChildren(item, Lists, depths + 15);
				}
				else
				{
					// 新建一个对象树节点容器控件;
					var item = new MetroExpander();

					margin.Left = 35 + depths;
					margin.Top = 8;
					margin.Bottom = 8;
					item.Margin = margin;

					item.Header = Obj_Node.ObjName;
					item.SubExpender = Lists;                           // 增加子容器,保存叶子节点;
					item.obj_type = Obj_Node;                           // 将节点添加到容器控件中;
					item.Click += IsSelectedChanged;                    // 点击事件;
																		//item.MouseRightButtonDown += IsRightMouseDown;

					Obj_Tree.Add(item);
				}
			}
		}

		/// <summary>
		/// 初始化参数列表;
		/// </summary>
		public ObjNode(int id, int pid, string version, string name, string tablename)
		{
			this.version = version;
			this.ObjID = id;
			this.ObjParentID = pid;
			this.ObjName = name;
			this.ObjTableName = tablename;
			IsSelectedChanged += ClickObjNode;
			IsRightMouseDown += ObjNode_IsRightMouseDown;
			name_cn = new Dictionary<string, string>();
			oid_cn = new Dictionary<string, string>();
			oid_en = new Dictionary<string, string>();
			contentlist = new ObservableCollection<DyDataGrid_MIBModel>();
			GetNextResList = new Dictionary<string, string>();
			nodeMibTable = new MibTable();
		}

		private void ObjNode_IsRightMouseDown(object sender, MouseButtonEventArgs e)
		{
			var abc = e.Source as MetroExpander;
			Console.WriteLine("111" + (abc.obj_type as ObjNode).ObjName);
			MainWindow.m_strNodeName = (abc.obj_type as ObjNode).ObjName;
		}

        /// <summary>
        /// 将查询到的数据组成界面显示的整行数据
        /// </summary>
        /// <param name="lineData"></param>
        /// <param name="nextResult"></param>
        public void SetMainDataGridLineData(ref Dictionary<string, Dictionary<string, string>> lineData, List<Dictionary<string, string>> nextResult)
        {
            if (nextResult.Count == 0)
                return;

            if (lineData == null)
                lineData = new Dictionary<string, Dictionary<string, string>>();

            if (IndexCount > 0)
            {
                // 循环每行
                foreach (Dictionary<string, string> oidVal in nextResult)
                {
                    string TempIndex = "";
                    int count = 0;
                    // 循环每列
                    foreach (KeyValuePair<string, string> kv in oidVal)
                    {
                        if (count == 0)//由于每行的索引相同，执行一次获取到索引
                        {
                            var temp = kv.Key.Split('.');

                            for (int i = temp.Length - IndexCount; i < temp.Length; i++)
                            {
                                TempIndex += "." + temp[i];
                            }
                        }

                        if (!lineData.ContainsKey(TempIndex))
                        {
                            lineData.Add(TempIndex, new Dictionary<string, string>());
                            lineData[TempIndex].Add(kv.Key, kv.Value);
                        }
                        else
                        {
                            if (!lineData[TempIndex].ContainsKey(kv.Key))
                                lineData[TempIndex].Add(kv.Key, kv.Value);
                        }
                        count++;
                    }
                }
            }
            else if (IndexCount == 0)//无索引时，后缀索引为.0
            {
                // 循环每行
                foreach (Dictionary<string, string> oidVal in nextResult)
                {
                    string TempIndex = ".0";
                    // 循环每列
                    foreach (KeyValuePair<string, string> kv in oidVal)
                    {
                        if (!lineData.ContainsKey(TempIndex))
                        {
                            lineData.Add(TempIndex, new Dictionary<string, string>());
                            lineData[TempIndex].Add(kv.Key, kv.Value);
                        }
                        else
                        {
                            if (!lineData[TempIndex].ContainsKey(kv.Key))
                                lineData[TempIndex].Add(kv.Key, kv.Value);
                        }
                    }
                }
            }
        }
    }

	/// <summary>
	/// 对象树*普通树枝*节点;
	/// </summary>
	internal class ObjTreeNode : ObjNode
	{
		public ObjTreeNode(int id, int pid, string version, string name, string tablename)
			: base(id, pid, version, name, tablename)
		{
			SubObj_Lsit = new List<ObjNode>();
		}

		private void SetExpanderBackGround(MetroExpander expander)
		{
			expander.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			if (expander != null && expander.Children != null && expander.Children.Count > 0)
			{
				foreach (var item in expander.Children)
				{
					if (item.GetType() == typeof(MetroExpander))
					{
						var targetItem = item as MetroExpander;
						SetExpanderBackGround(targetItem);
					}
				}
			}
		}

		private void GetRoot(MetroExpander expander)
		{
			//expander.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
			if (expander.Header == "双模基站")
			{
				//return;
				SetExpanderBackGround(expander);
			}
			else
			{
				var parentItem = expander.Parent;
				if(parentItem.GetType() == typeof(StackPanel))
				{
					StackPanel item = parentItem as StackPanel;
					MetroExpander ppItem = item.Parent as MetroExpander;
					GetRoot(ppItem);

				}else if(parentItem.GetType() == typeof(MetroExpander))
				{
					MetroExpander item = parentItem as MetroExpander;
					GetRoot(item);
                }
			}
		}

		/// <summary>
		/// 点击枝节点时;
		/// 1、在右侧列表中更新叶子节点;
		/// 2、触发SNMP的GetNext查询;
		/// </summary>
		/// <param name="sender">对应的容器</param>
		/// <param name="e"></param>
		public override void ClickObjNode(object sender, EventArgs e)
		{
			// 上一个请求任务是否正在进行
			if (isGetParaTaskRunning == true)
			{
				Log.Info("上一个Get命令正在进行，本次操作无法进行，已退出...");
				Console.WriteLine("上一个Get命令正在进行，本次操作无法进行，已退出...");
				return;
			}

			// 清除DataGrid表格数据
			main.Main_Dynamic_DataGrid.DynamicDataGrid.Columns.Clear();
			main.Main_Dynamic_DataGrid.DynamicDataGrid.DataContext = null;

			var items = sender as MetroExpander;

			// 清理掉之前填入的Children节点;
			if (items != null)
			{
				items.SubExpender.Children.Clear();

				//Add By Mayi  修改选中节点的背景色
				//if(selectedItem != null)
					GetRoot(items);
				items.Background = new SolidColorBrush(Color.FromRgb(208, 227, 252));
				

				// 将叶子节点加入右侧容器;
				var node = items.obj_type as ObjNode;
				if (null == node)
					return;

				if (node.SubObj_Lsit != null)
				{
					foreach (var iter in ((ObjNode)items.obj_type).SubObj_Lsit)
					{
						// 子节点如果是枝节点跳过;
						if (iter is ObjTreeNode)
						{
							continue;
						}

						// 初始化对应的内容,并加入到容器中;
						var subitems = new MetroExpander
						{
							Header = iter.ObjName,
							obj_type = iter
						};

						subitems.Click += iter.ClickObjNode;
						items.SubExpender.Children.Add(subitems);
					}

					//// 该节点有对应的表可查;
					//if (node.ObjTableName != @"/")
					//{
					//    ObjTreeNode_Click(node);
					//}
				}
				//else
				//{
				//    // 该节点有对应的表可查;
				//    if (node.ObjTableName != @"/")
				//    {
				//        ObjTreeNode_Click(node);
				//    }
				//}

				// 该节点有对应的表可查;
				if (node.ObjTableName != @"/")
				{
					ObjTreeNode_Click(node);
				}
			}
		}

		/// <summary>
		/// 添加孩子节点;
		/// </summary>
		/// <param name="obj">孩子节点</param>
		public override void Add(ObjNode obj)
		{
			var index = SubObj_Lsit.IndexOf(obj);
			if (index < 0)
			{
				SubObj_Lsit.Add(obj);
				//Console.WriteLine("222" + obj.ObjName);
			}
			//else
			//{
			//    Console.WriteLine("添加重复节点;");
			//}
			//SubObj_Lsit.Add(obj);
		}

		/// <summary>
		/// 删除孩子节点;
		/// </summary>
		/// <param name="obj">孩子节点</param>
		public override void Remove(ObjNode obj)
		{
			SubObj_Lsit.Remove(obj);
		}

		/// <summary>
		/// 点击树枝节点时的处理方法;
		/// </summary>
		private void ObjTreeNode_Click_bak(ObjNode node)
		{
			//Console.WriteLine("TreeNode Clicked to show info!");
			var ret = new MibTable();
			var GetNextRet = new Dictionary<string, string>();
			var IndexNum = 0;
			contentlist.Clear();
			GetNextResList.Clear();
			ObjParentOID = string.Empty;
			nodeMibTable = new MibTable();

			// 目前可以获取到节点对应的中文名以及对应的表名;
			//Console.WriteLine("LeafNode Clicked!" + node.ObjName + " and TableName " + node.ObjTableName);

			var errorInfo = "";
			//根据表名获取该表内所有MIB节点;
			nodeb.db = Database.GetInstance();
			nodeb.db.GetMibDataByTableName(node.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);
			nodeMibTable = ret;

			var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
			name_cn.Clear();
			oid_cn.Clear();
			oid_en.Clear();         // 每个节点都有自己的表数据结构;

			try
			{
				int.TryParse(ret.indexNum.ToString(), out IndexNum);              // 获取这张表索引的个数;
				IndexCount = ret.indexNum;
				LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
				ChildCount = ret.childList.Count - IndexNum;
				ObjParentOID = ret.oid;                                // 将父节点OID赋值;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// 循环MIb表
			foreach (var iter in ret.childList)
			{
				// 过滤非Mib节点
				if (iter.isMib != 1)
				{
					ChildCount--;
				}

			}

			// 遍历所有子节点，组SNMP的GetNext命令的一行OID集合;
			foreach (var iter in ret.childList)
			{
				oidlist.Clear();
				// 索引不参与查询,将所有其他孩子节点进行GetNext查询操作;
				if (iter.childNo > IndexNum)
				{
					// 如果不是真MIB，不参与查询;
					if (iter.isMib != 1)
					{
						//ChildCount--;
						continue;
					}

					var temp = SnmpToDatabase.GetMibPrefix() + iter.childOid;
					name_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
					oid_en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
					oid_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
					oidlist.Add(temp);

					// 通过GetNext查询单个节点数据;
					var msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());

					// todo 此处需要设置为异步查询，否则查询某些节点时，UI卡死

					msg.GetNextRequestWhenStop(ReceiveResBySingleNode, NotifyMainUpdateDataGrid, oidlist);
				}
				else
				{
					//ty:增加索引的信息
					name_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
					oid_en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
					oid_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
				}

				// 如果是单个节点遍历，就只能在此处组DataGrid的VM类;
			}

			// 通过GetNext获取整表数据，后来发现基站不支持,如果基站支持后，在此处GetNext即可;
			//SnmpMessageV2c msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());
			//msg.GetNextRequest(new AsyncCallback(ReceiveRes), oidlist);
		}

		/// <summary>
		/// 点击树枝节点时的处理方法;
		/// </summary>
		/// <param name="node"></param>
		private void ObjTreeNode_Click(ObjNode node)
		{
			contentlist.Clear();

			// 异步获取数据，避免UI卡死
			Task tk = new Task(() =>
			{
				Action<ObjNode> ac = (nodeInfo) => GetParentNodeDatasThd(nodeInfo);
				GetParentNodeDatas(ac, node);
            });
			// 启动任务
			tk.Start();

			// 任务列表，用于检查任务是否结束
			List<Task> taskList = new List<Task>();
			taskList.Add(tk);

			// 设置任务标识为进行中
			isGetParaTaskRunning = true;

			// 异步等待任务完成
			Task.Factory.StartNew(x => {
				// 等待任务完成
				Task.WaitAll(taskList.ToArray());
				// 任务完成，设置任务完成标识
				isGetParaTaskRunning = false;

				Log.Info("Snmp Get任务完成...");
				Console.WriteLine("Get任务完成...");
			}, null);

			Console.WriteLine("YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");

		}

		/// <summary>
		/// 获取树枝节点的DataGrid数据
		/// </summary>
		/// <param name="_ac">Action</param>
		/// <param name="objNode">ObjNode节点实例</param>
		private void GetParentNodeDatas(Action<ObjNode> _ac, ObjNode objNode)
		{
			_ac(objNode);
		}

		/// <summary>
		/// 点击树枝节点时的异步处理方法;
		/// </summary>
		private void GetParentNodeDatasThd(ObjNode node)
		{
			//Console.WriteLine("TreeNode Clicked to show info!");
			var ret = new MibTable();
			var GetNextRet = new Dictionary<string, string>();
			var IndexNum = 0;
			Dictionary<string, string> getNextResList = new Dictionary<string, string>();
			string objParentOID = string.Empty;
			MibTable nodeMibTable = new MibTable();
            Dictionary<string, Dictionary<string, string>> getNextResultLineData = new Dictionary<string, Dictionary<string, string>>();

        // 目前可以获取到节点对应的中文名以及对应的表名;
        //Console.WriteLine("LeafNode Clicked!" + node.ObjName + " and TableName " + node.ObjTableName);

        var errorInfo = "";
			//根据表名获取该表内所有MIB节点;
			nodeb.db = Database.GetInstance();
			nodeb.db.GetMibDataByTableName(node.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);
			nodeMibTable = ret;

			var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
			//name_cn.Clear(); // TODO: 此方法在线程中调用，尽量避免使用全局变量
			Dictionary<string, string> name2cn = new Dictionary<string, string>();
			//oid_cn.Clear();
			Dictionary<string, string> oid2cn = new Dictionary<string, string>();
			//oid_en.Clear();         // 每个节点都有自己的表数据结构;
			Dictionary<string, string> oid2en = new Dictionary<string, string>();

			try
			{
				int.TryParse(ret.indexNum.ToString(), out IndexNum);              // 获取这张表索引的个数;
				IndexCount = ret.indexNum;
				LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
				ChildCount = ret.childList.Count - IndexNum;
				objParentOID = ret.oid;                                // 将父节点OID赋值;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// 循环MIb表，组装列名称
			foreach (var iter in ret.childList)
			{
				// 过滤非Mib节点
				if (iter.isMib != 1)
				{
					ChildCount--;
					continue;
				}

                if (iter.ASNType.Equals("RowStatus"))
                {
                    continue;
                }

                // 保存中文名称等信息
                name2cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
				oid2en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
				oid2cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
			}

			// 数据的最终结果
			List<Dictionary<string, string>> oidAndValTable = new List<Dictionary<string, string>>();
			// 一行数据，因为一行数据可能是由多次GeNext的结果拼成的
			Dictionary<string, string> oidAndValLine = new Dictionary<string, string>();

			// 根据表名获取命令信息
			CmdInfoList cmdList = new CmdInfoList();
			if (!cmdList.GeneratedCmdInfoList())
			{
				return;
			}

			List<CmdMibInfo> cmdMibInfoList = cmdList.GetCmdsByTblName(ret.nameMib);
			if (cmdMibInfoList.Count == 0)
			{
				return;
			}

			// 获取DataGrid数据
			LmtbSnmpEx lmtSnmpEx = DTLinkPathMgr.GetSnmpInstance(CSEnbHelper.GetCurEnbAddr());
			// 组装GentNext 的Oid列表
			List<string> getNextOidList = new List<string>();

			// 无论是标量还是矢量表，每个GetCmd执行一次GetNext
			foreach (CmdMibInfo cmdItem in cmdMibInfoList)
			{
				if ("0".Equals(cmdItem.m_cmdType)) // Get命令
				{
					// 每个GetCmd执行一次GetNext
					try
					{
						// 获取一个GetCmd命令中的Oid
						getNextOidList.Clear();
						foreach (string oid in cmdItem.m_leaflist)
						{
							getNextOidList.Add(SnmpToDatabase.GetMibPrefix() + oid);
						}
						// GetNext结果
						List<Dictionary<string, string>> oidAndValTableTmp = null;
						if (false == lmtSnmpEx.SnmpGetNextLoop(CSEnbHelper.GetCurEnbAddr(), getNextOidList, out oidAndValTableTmp))
						{
							Log.Error("执行SnmpGetNextLoop()方法错误！");
							return;
						}

                        SetMainDataGridLineData(ref getNextResultLineData, oidAndValTableTmp);
                        
                        /*foreach (Dictionary<string, string> oidVal in oidAndValTableTmp)
						{
							// var oidAndValLine2 = oidAndValLine.Concat(oidVal);
							// 循环每列
							foreach (KeyValuePair<string, string> kv in oidVal)
							{
								// 保存多次GetNext的结果
								getNextResList.Add(kv.Key, kv.Value);
							}
						}*/

					}
					catch (Exception ex)
					{
						throw ex;
					}

				}
			}

			// 更新DataGrid数据
			UpdataDataGrid(getNextResultLineData, oid2cn, oid2en, objParentOID, nodeMibTable);
		}

		/// <summary>
		/// 点击树枝节点时的处理方法;
		/// </summary>
		private void ObjTreeNode_Click_bak2(ObjNode node)
		{
			//Console.WriteLine("TreeNode Clicked to show info!");
			var ret = new MibTable();
			var GetNextRet = new Dictionary<string, string>();
			var IndexNum = 0;
			contentlist.Clear();
			GetNextResList.Clear();
			ObjParentOID = string.Empty;
			nodeMibTable = new MibTable();

			// 目前可以获取到节点对应的中文名以及对应的表名;
			//Console.WriteLine("LeafNode Clicked!" + node.ObjName + " and TableName " + node.ObjTableName);

			var errorInfo = "";
			//根据表名获取该表内所有MIB节点;
			nodeb.db = Database.GetInstance();
			nodeb.db.GetMibDataByTableName(node.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);
			nodeMibTable = ret;

			var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
			name_cn.Clear();
			oid_cn.Clear();
			oid_en.Clear();         // 每个节点都有自己的表数据结构;

			try
			{
				int.TryParse(ret.indexNum.ToString(), out IndexNum);              // 获取这张表索引的个数;
				IndexCount = ret.indexNum;
				LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
				ChildCount = ret.childList.Count - IndexNum;
				ObjParentOID = ret.oid;                                // 将父节点OID赋值;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// 循环MIb表，组装列名称
			foreach (var iter in ret.childList)
			{
				// 过滤非Mib节点
				if (iter.isMib != 1)
				{
					ChildCount--;
					continue;
				}

                if(iter.ASNType.Equals("RowStatus"))
                {
                    continue;
                }

				// 保存中文名称等信息
				name_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
				oid_en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
				oid_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
			}

			// 数据的最终结果
			List<Dictionary<string, string>> oidAndValTable = new List<Dictionary<string, string>>();
			// 一行数据，因为一行数据可能是由多次GeNext的结果拼成的
			Dictionary<string, string> oidAndValLine = new Dictionary<string, string>();

			// 根据表名获取命令信息
			CmdInfoList cmdList = new CmdInfoList();
			if (!cmdList.GeneratedCmdInfoList())
			{
				return;
			}

			List<CmdMibInfo> cmdMibInfoList = cmdList.GetCmdsByTblName(ret.nameMib);
			if (cmdMibInfoList.Count == 0)
			{
				return;
			}

			// 获取DataGrid数据
			LmtbSnmpEx lmtSnmpEx = DTLinkPathMgr.GetSnmpInstance(CSEnbHelper.GetCurEnbAddr());
			// 组装GentNext 的Oid列表
			List<string> getNextOidList = new List<string>();

			// 无论是标量还是矢量表，每个GetCmd执行一次GetNext
			foreach (CmdMibInfo cmdItem in cmdMibInfoList)
			{
				if ("0".Equals(cmdItem.m_cmdType)) // Get命令
				{
					// 每个GetCmd执行一次GetNext
					try
					{
						// 获取一个GetCmd命令中的Oid
						getNextOidList.Clear();
						foreach (string oid in cmdItem.m_leaflist)
						{
							getNextOidList.Add(SnmpToDatabase.GetMibPrefix() + oid);
						}
						// GetNext结果
						List<Dictionary<string, string>> oidAndValTableTmp = null;
						if (false == lmtSnmpEx.SnmpGetNextLoop(CSEnbHelper.GetCurEnbAddr(), getNextOidList, out oidAndValTableTmp))
						{
							Log.Error("执行SnmpGetNextLoop()方法错误！");
							return;
						}

						// 循环每行
						foreach (Dictionary<string, string> oidVal in oidAndValTableTmp)
						{
							// var oidAndValLine2 = oidAndValLine.Concat(oidVal);
							// 循环每列
							foreach (KeyValuePair<string, string> kv in oidVal)
							{
								// 保存多次GetNext的结果
								GetNextResList.Add(kv.Key, kv.Value);
							}
						}

					}
					catch (Exception ex)
					{
						throw ex;
					}

				}
			}

			// 更新DataGrid数据
			//UpdataDataGrid(GetNextResList);
		}

		/// <summary>
		/// 按照单个节点进行GetNext;
		/// 该函数将所有数据收集完成后再通知主界面DataGrid更新;
		/// </summary>
		/// <param name="ar"></param>
		private void ReceiveResBySingleNode(IAsyncResult ar)
		{
			var res = ar as SnmpMessageResult;

			// 遍历GetNext结果，添加到对应容器当中,GetNextResList容器中保存着全量集;
			foreach (var iter in res.AsyncState as Dictionary<string, string>)
			{
				GetNextResList.Add(iter.Key, iter.Value);
			}
		}

		/// <summary>
		/// 调用主界面更新DataGrid
		/// </summary>
		/// <param name="getNextList"></param>
		/// <param name="oid_cn"></param>
		/// <param name="oid_en"></param>
		/// <param name="objParentOID"></param>
		/// <param name="nodeMibTable"></param>
		private void UpdataDataGrid(Dictionary<string, Dictionary<string, string>> getNextList, Dictionary<string, string> oid2cn
			, Dictionary<string, string>  oid2en, string objParentOID, MibTable nodeMibTable)
		{
			main.UpdateAllMibDataGrid(getNextList, oid2cn, oid2en, contentlist, objParentOID, IndexCount, nodeMibTable);
		}

		/// <summary>
		/// ReceiveResBySingleNode的GetNext函数收集完成之后，调用主界面更新DataGrid
		/// </summary>
		/// <param name="ar"></param>
		private void NotifyMainUpdateDataGrid(IAsyncResult ar)
		{
			LastColumn++;

			// 全部节点都已经收集完毕;
			if (LastColumn == ChildCount)
			{
				//main.UpdateAllMibDataGrid(GetNextResList, oid_cn, oid_en, contentlist, ObjParentOID, IndexCount, nodeMibTable);
			}
		}
	}

	/// <summary>
	/// 对象树*普通叶子*节点;
	/// </summary>
	internal class ObjLeafNode : ObjNode
	{
		public ObjLeafNode(int id, int pid, string version, string name, string tablename)
			: base(id, pid, version, name, tablename)
		{
		}

		/// <summary>
		/// 当点击叶子节点时，会触发GetNext操作;
		/// 注意：基站GetNext不支持全节点查询，最大粒度为Get命令当中的节点数量;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void ClickObjNode(object sender, EventArgs e)
		{
			// 上一个请求任务是否正在进行
			if (isGetParaTaskRunning == true)
			{
				Log.Info("上一个Get命令正在进行，本次操作无法进行，已退出...");
				Console.WriteLine("上一个Get命令正在进行，本次操作无法进行，已退出...");
				return;
			}

			contentlist.Clear();
			// 清理DataGrid表格数据
			main.Main_Dynamic_DataGrid.DynamicDataGrid.DataContext = null;
			main.Main_Dynamic_DataGrid.DynamicDataGrid.Columns.Clear();

			var item = sender as MetroExpander;
			var node = item.obj_type as ObjNode;

			if (item.Parent.GetType() == typeof(StackPanel))
			{
				StackPanel parentItem = item.Parent as StackPanel;

				foreach (var subItem in parentItem.Children)
				{
					if (subItem.GetType() == typeof(MetroExpander))
					{
						var targetItem = subItem as MetroExpander;
						targetItem.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
					}
				}
			}
			item.Background = new SolidColorBrush(Color.FromRgb(208, 227, 252));

			// 目前可以获取到节点对应的中文名以及对应的表名;
			Console.WriteLine("LeafNode Clicked!" + node.ObjName + "and TableName " + this.ObjTableName);

			// 启用多任务查询数据，避免UI卡死
			Task tk = new Task(() =>
			{
				Action<ObjNode> ac = (nodeInfo) => GetLeafNodeDatasThd(nodeInfo);
				GetLeafNodeDatas(ac, node);
            });
			// 启动线程
			tk.Start();

			// 设置任务标识为进行中
			isGetParaTaskRunning = true;

			// 任务列表，用于检查任务是否结束
			List<Task> taskList = new List<Task>();
			taskList.Add(tk);

			// 异步检查任务是否结束，避免UI卡死
			Task.Factory.StartNew(x => 
			{
				// 等待任务结束
				Task.WaitAll(taskList.ToArray());
				// 任务结束，设置任务标识为完成
				isGetParaTaskRunning = false;

				Log.Info("Snmp Get任务完成");
				Console.WriteLine("Snmp Get任务完成");

			}, null);

			Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

		}

		/// <summary>
		/// 获取叶子节点的DataGrid数据
		/// </summary>
		/// <param name="_ac"></param>
		/// <param name="node"></param>
		private void GetLeafNodeDatas(Action<ObjNode> _ac, ObjNode node)
		{
			_ac(node);
		}

		/// <summary>
		/// 获取叶子节点的DataGrid数据;
		/// </summary>
		/// <param name="node">ObjNode对象</param>
		public void GetLeafNodeDatasThd(ObjNode node)
		{
			string strMsg = "";
			var ret = new MibTable();
			var IndexNum = 0;
			Dictionary<string, string> getNextResList = new Dictionary<string, string>();
			string objParentOID = string.Empty;
			MibTable nodeMibTable = new MibTable();
            Dictionary<string, Dictionary<string, string>> getNextResultLineData = new Dictionary<string, Dictionary<string, string>>();


            var errorInfo = "";
			//根据表名获取该表内所有MIB节点;
			nodeb.db = Database.GetInstance();
			nodeb.db.GetMibDataByTableName(this.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);

            if (ret == null)
            {
                Log.Error("获取不到该节点的表信息");
                return;
            }               

			nodeMibTable = ret;
			if (ret == null)
			{
				strMsg = string.Format("使用Mib表名获取到的Mib表信息为空（Mib表名：{0}）", ObjTableName);
                Log.Error(strMsg);
				MessageBox.Show(strMsg);
				return;
			}

			var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
			//name_cn.Clear(); // TODO: 方法在多线程中执行，尽量避免使用全局变量
			//oid_cn.Clear();
			//oid_en.Clear();         // 每个节点都有自己的表数据结构;
			Dictionary<string, string> name2cn = new Dictionary<string, string>();
			Dictionary<string, string> oid2cn = new Dictionary<string, string>();
			Dictionary<string, string> oid2en = new Dictionary<string, string>();

			try
			{
				int.TryParse(ret.indexNum.ToString(), out IndexNum);              // 获取这张表索引的个数;
				IndexCount = ret.indexNum;
				LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
				ChildCount = ret.childList.Count - IndexNum;
				objParentOID = ret.oid;                                // 将父节点OID赋值;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// 循环MIb表，组装列名称
			foreach (var iter in ret.childList)
			{
				// 过滤非Mib节点
				if (iter.isMib != 1)
				{
					ChildCount--;
					continue;
				}

                if (iter.ASNType.Equals("RowStatus"))
                {
                    continue;
                }

                // 保存中文名称等信息
                name2cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
				oid2en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
				oid2cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
			}

			// 数据的最终结果
			List<Dictionary<string, string>> oidAndValTable = new List<Dictionary<string, string>>();
			// 一行数据，因为一行数据可能是由多次GeNext的结果拼成的
			Dictionary<string, string> oidAndValLine = new Dictionary<string, string>();

			// 根据表名获取命令信息
			CmdInfoList cmdList = new CmdInfoList();
			if (!cmdList.GeneratedCmdInfoList())
			{
				return;
			}

			List<CmdMibInfo> cmdMibInfoList = cmdList.GetCmdsByTblName(ret.nameMib);
			if (cmdMibInfoList.Count == 0)
			{
				return;
			}

			// 获取DataGrid数据
			LmtbSnmpEx lmtSnmpEx = DTLinkPathMgr.GetSnmpInstance(CSEnbHelper.GetCurEnbAddr());
			// 组装GentNext 的Oid列表
			List<string> getNextOidList = new List<string>();

			// 无论是标量还是矢量表，每个GetCmd执行一次GetNext
			foreach (CmdMibInfo cmdItem in cmdMibInfoList)
			{
				if ("0".Equals(cmdItem.m_cmdType)) // Get命令
				{
					// 每个GetCmd执行一次GetNext
					try
					{
						// 获取一个GetCmd命令中的Oid
						getNextOidList.Clear();
						foreach (string oid in cmdItem.m_leaflist)
						{
							getNextOidList.Add(SnmpToDatabase.GetMibPrefix() + oid);
						}
						// GetNext结果
						List<Dictionary<string, string>> oidAndValTableTmp = null;
						if (false == lmtSnmpEx.SnmpGetNextLoop(CSEnbHelper.GetCurEnbAddr(), getNextOidList, out oidAndValTableTmp))
						{
							Log.Error("执行SnmpGetNextLoop()方法错误！");
							return;
						}

                        SetMainDataGridLineData(ref getNextResultLineData, oidAndValTableTmp);
                        // 循环每行
                        /*foreach (Dictionary<string, string> oidVal in oidAndValTableTmp)
						{
							// var oidAndValLine2 = oidAndValLine.Concat(oidVal);
							// 循环每列
							foreach (KeyValuePair<string, string> kv in oidVal)
							{
								// 保存多次GetNext的结果
								if (!getNextResList.ContainsKey(kv.Key))
								{
									getNextResList.Add(kv.Key, kv.Value);
								}
							}
						}*/

                    }
					catch (Exception ex)
					{
						throw ex;
					}

				}
			}

			// 更新DataGrid数据
			UpdataDataGrid(getNextResultLineData, oid2cn, oid2en, objParentOID, nodeMibTable);

		}


		/// <summary>
		/// 当点击叶子节点时，会触发GetNext操作;
		/// 注意：基站GetNext不支持全节点查询，最大粒度为Get命令当中的节点数量;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ClickObjNode_bak2(object sender, EventArgs e)
		{
			string strMsg = "";
			var item = sender as MetroExpander;
			var node = item.obj_type as ObjNode;
			var ret = new MibTable();
			var GetNextRet = new Dictionary<string, string>();
			var IndexNum = 0;
			contentlist.Clear();
			GetNextResList.Clear();
			ObjParentOID = String.Empty;
			nodeMibTable = new MibTable();

			if (item.Parent.GetType() == typeof(StackPanel))
			{
				StackPanel parentItem = item.Parent as StackPanel;

				foreach (var subItem in parentItem.Children)
				{
					if (subItem.GetType() == typeof(MetroExpander))
					{
						var targetItem = subItem as MetroExpander;
						targetItem.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
					}
				}
			}
			item.Background = new SolidColorBrush(Color.FromRgb(208, 227, 252));

			// 目前可以获取到节点对应的中文名以及对应的表名;
			Console.WriteLine("LeafNode Clicked!" + node.ObjName + "and TableName " + this.ObjTableName);

			var errorInfo = "";
			//根据表名获取该表内所有MIB节点;
			nodeb.db = Database.GetInstance();
			nodeb.db.GetMibDataByTableName(this.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);

			if (ret == null)
			{
				Log.Error("获取不到该节点的表信息");
				return;
			}

			nodeMibTable = ret;
			if (ret == null)
			{
				strMsg = string.Format("使用Mib表名获取到的Mib表信息为空（Mib表名：{0}）", ObjTableName);
				Log.Error(strMsg);
				MessageBox.Show(strMsg);
				return;
			}

			var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
			name_cn.Clear();
			oid_cn.Clear();
			oid_en.Clear();         // 每个节点都有自己的表数据结构;

			try
			{
				int.TryParse(ret.indexNum.ToString(), out IndexNum);              // 获取这张表索引的个数;
				IndexCount = ret.indexNum;
				LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
				ChildCount = ret.childList.Count - IndexNum;
				ObjParentOID = ret.oid;                                // 将父节点OID赋值;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// 循环MIb表，组装列名称
			foreach (var iter in ret.childList)
			{
				// 过滤非Mib节点
				if (iter.isMib != 1)
				{
					ChildCount--;
					continue;
				}

                if (iter.ASNType.Equals("RowStatus"))
                {
                    continue;
                }

                // 保存中文名称等信息
                name_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
				oid_en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
				oid_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
			}


			// 数据的最终结果
			List<Dictionary<string, string>> oidAndValTable = new List<Dictionary<string, string>>();
			// 一行数据，因为一行数据可能是由多次GeNext的结果拼成的
			Dictionary<string, string> oidAndValLine = new Dictionary<string, string>();

			// 根据表名获取命令信息
			CmdInfoList cmdList = new CmdInfoList();
			if (!cmdList.GeneratedCmdInfoList())
			{
				return;
			}

			List<CmdMibInfo> cmdMibInfoList = cmdList.GetCmdsByTblName(ret.nameMib);
			if (cmdMibInfoList.Count == 0)
			{
				return;
			}

			// 获取DataGrid数据
			LmtbSnmpEx lmtSnmpEx = DTLinkPathMgr.GetSnmpInstance(CSEnbHelper.GetCurEnbAddr());
			// 组装GentNext 的Oid列表
			List<string> getNextOidList = new List<string>();

			// 无论是标量还是矢量表，每个GetCmd执行一次GetNext
			foreach (CmdMibInfo cmdItem in cmdMibInfoList)
			{
				if ("0".Equals(cmdItem.m_cmdType)) // Get命令
				{
					// 每个GetCmd执行一次GetNext
					try
					{
						// 获取一个GetCmd命令中的Oid
						getNextOidList.Clear();
						foreach (string oid in cmdItem.m_leaflist)
						{
							getNextOidList.Add(SnmpToDatabase.GetMibPrefix() + oid);
						}
						// GetNext结果
						List<Dictionary<string, string>> oidAndValTableTmp = null;
						if (false == lmtSnmpEx.SnmpGetNextLoop(CSEnbHelper.GetCurEnbAddr(), getNextOidList, out oidAndValTableTmp))
						{
							Log.Error("执行SnmpGetNextLoop()方法错误！");
							return;
						}

						// 循环每行
						foreach (Dictionary<string, string> oidVal in oidAndValTableTmp)
						{
							// var oidAndValLine2 = oidAndValLine.Concat(oidVal);
							// 循环每列
							foreach (KeyValuePair<string, string> kv in oidVal)
							{
								// 保存多次GetNext的结果
								if (!GetNextResList.ContainsKey(kv.Key))
								{
									GetNextResList.Add(kv.Key, kv.Value);
								}
							}
						}

					}
					catch (Exception ex)
					{
						throw ex;
					}

				}
			}

			// 更新DataGrid数据
			//UpdataDataGrid(GetNextResList);

		}

		/// <summary>
		/// 当点击叶子节点时，会触发GetNext操作;
		/// 注意：基站GetNext不支持全节点查询，最大粒度为Get命令当中的节点数量;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ClickObjNode_bak(object sender, EventArgs e)
		{
			var item = sender as MetroExpander;
			var node = item.obj_type as ObjNode;
			var ret = new MibTable();
			var GetNextRet = new Dictionary<string, string>();
			var IndexNum = 0;
			contentlist.Clear();
			GetNextResList.Clear();
			ObjParentOID = String.Empty;
			nodeMibTable = new MibTable();

			// 目前可以获取到节点对应的中文名以及对应的表名;
			Console.WriteLine("LeafNode Clicked!" + node.ObjName + "and TableName " + this.ObjTableName);

			var errorInfo = "";
			//根据表名获取该表内所有MIB节点;
			nodeb.db = Database.GetInstance();
			nodeb.db.GetMibDataByTableName(this.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);
			nodeMibTable = ret;

			var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
			name_cn.Clear();
			oid_cn.Clear();
			oid_en.Clear();         // 每个节点都有自己的表数据结构;

			try
			{
				int.TryParse(ret.indexNum.ToString(), out IndexNum);              // 获取这张表索引的个数;
				IndexCount = ret.indexNum;
				LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
				ChildCount = ret.childList.Count - IndexNum;
				ObjParentOID = ret.oid;                                // 将父节点OID赋值;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// 需要提前去掉假MIB的数量，否则如果一张表的后几项是假MIB，这张表将无法呈现数据
			foreach (var iter in ret.childList)
			{
				if (iter.isMib != 1)
				{
					ChildCount--;
				}
			}

			// 遍历所有子节点，组SNMP的GetNext命令的一行OID集合;
			foreach (var iter in ret.childList)
			{
				oidlist.Clear();
				// 索引不参与查询,将所有其他孩子节点进行GetNext查询操作;
				if (iter.childNo > IndexNum)
				{
					// 如果不是真MIB，不参与查询;
					if (iter.isMib != 1)
					{
						//ChildCount--;
						continue;
					}

					var temp = SnmpToDatabase.GetMibPrefix() + iter.childOid;
					name_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
					oid_en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
					oid_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
					oidlist.Add(temp);

					// 通过GetNext查询单个节点数据;
					var msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());
					msg.GetNextRequestWhenStop(ReceiveResBySingleNode, NotifyMainUpdateDataGrid, oidlist);
				}
				else
				{
					//ty:增加索引的信息
					name_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childNameMib, iter.childNameCh);
					oid_en.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameMib);
					oid_cn.Add(SnmpToDatabase.GetMibPrefix() + iter.childOid, iter.childNameCh);
				}

				// 如果是单个节点遍历，就只能在此处组DataGrid的VM类;
			}

			// 通过GetNext获取整表数据，后来发现基站不支持,如果基站支持后，在此处GetNext即可;
			//SnmpMessageV2c msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());
			//msg.GetNextRequest(new AsyncCallback(ReceiveRes), oidlist);
		}

		/// <summary>
		/// 每当收集完一行数据后，更新主界面中的DataGrid;
		/// </summary>
		/// <param name="ar"></param>
		// 		private void ReceiveRes(IAsyncResult ar)
		// 		{
		// 			main.UpdateMibDataGrid(ar, oid_cn, oid_en, contentlist);
		// 		}

		/// <summary>
		/// 按照单个节点进行GetNext;
		/// 该函数将所有数据收集完成后再通知主界面DataGrid更新;
		/// </summary>
		/// <param name="ar"></param>
		private void ReceiveResBySingleNode(IAsyncResult ar)
		{
			var res = ar as SnmpMessageResult;

			// 遍历GetNext结果，添加到对应容器当中,GetNextResList容器中保存着全量集;
			foreach (var iter in res.AsyncState as Dictionary<string, string>)
			{
				GetNextResList.Add(iter.Key, iter.Value);
			}
		}

		/// <summary>
		/// 更新DataGrid数据
		/// </summary>
		/// <param name="getNextList"></param>
		/// <param name="oid_cn"></param>
		/// <param name="oid_en"></param>
		/// <param name="objParentOID"></param>
		/// <param name="nodeMibTable"></param>
		private void UpdataDataGrid(Dictionary<string, Dictionary<string, string>> getNextList, Dictionary<string, string> oid2cn
			, Dictionary<string, string> oid2en, string objParentOID, MibTable nodeMibTable)
		{
			main.UpdateAllMibDataGrid(getNextList, oid2cn, oid2en, contentlist, objParentOID, IndexCount, nodeMibTable);
		}

		/// <summary>
		/// ReceiveResBySingleNode的GetNext函数收集完成之后，调用主界面更新DataGrid
		/// </summary>
		/// <param name="ar"></param>
		private void NotifyMainUpdateDataGrid(IAsyncResult ar)
		{
			LastColumn++;

			// 全部节点都已经收集完毕;
			if (LastColumn == ChildCount)
			{
				//main.UpdateAllMibDataGrid(GetNextResList, oid_cn, oid_en, contentlist, ObjParentOID, IndexCount, nodeMibTable);
			}
		}

		public override void Add(ObjNode obj)
		{
			throw new NotImplementedException();
		}

		public override void Remove(ObjNode obj)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// 对象树*小区建立*节点;
	/// </summary>
	internal class ObjCellSetupNode : ObjNode
	{
		public ObjCellSetupNode(int id, int pid, string version, string name, string tablename)
			: base(id, pid, version, name, tablename)
		{
		}

		public override void ClickObjNode(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		public override void Add(ObjNode obj)
		{
			throw new NotImplementedException();
		}

		public override void Remove(ObjNode obj)
		{
			throw new NotImplementedException();
		}
	}
}