﻿using CommonUtility;
using DataBaseUtil;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SCMTMainWindow.Component.ViewModel;
using SCMTMainWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using UICore.Controls.Metro;

namespace SCMTMainWindow.View
{
	/// <summary>
	/// MainDataGrid.xaml 的交互逻辑
	/// </summary>
	public partial class MainDataGrid : UserControl
	{
		/// <summary>
		/// 动态表的所有列信息,该属性必须设置，否则无法正常显示;
		/// 设置该属性之后，动态表就会将所有列对应的模板全部生成;
		/// </summary>
		private DyDataGrid_MIBModel m_ColumnModel;

		public DyDataGrid_MIBModel ColumnModel
		{
			get
			{
				return m_ColumnModel;
			}
			set
			{
				m_ColumnModel = value;
				this.DynamicDataGrid.Columns.Clear();

				// 获取所有列信息，并将列信息填充到DataGrid当中;
				foreach (var iter in m_ColumnModel.PropertyList)
				{
					// 显示字符类型的数据结构;
					if (iter.Item3 is DataGrid_Cell_MIB)
					{
						// 当前添加的表格类型只有Text类型，应该使用工厂模式添加对应不同的数据类型;
						var column = new DataGridTextColumn
						{
							Header = iter.Item2,
							IsReadOnly = (iter.Item3 as DataGrid_Cell_MIB).m_bIsReadOnly,
							Binding = new Binding(iter.Item1 + ".m_Content")
						};

						this.DynamicDataGrid.Columns.Add(column);
					}
					// 显示枚举类型的数据结构，在单元格内呈现一个ComboBox下拉框;
					else if (iter.Item3 is DataGrid_Cell_MIB_ENUM)
					{
						DataGridTemplateColumn column = new DataGridTemplateColumn();
						DataTemplate TextBlockTemplate = new DataTemplate();
						DataTemplate ComboBoxTemplate = new DataTemplate();

						string textblock_xaml =
						   @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
											xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
											xmlns:model='clr-namespace:WPF.Model'>
								<TextBlock Text='{Binding " + iter.Item1 + @".m_Content}'/>
							</DataTemplate>";

						string combobox_xaml =
						   @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
											xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
											xmlns:model='clr-namespace:WPF.Model'>
								<ComboBox ItemsSource='{Binding " + iter.Item1 + @".m_AllContent}' SelectedIndex='0'/>
							 </DataTemplate>";

						TextBlockTemplate = XamlReader.Parse(textblock_xaml) as DataTemplate;
						ComboBoxTemplate = XamlReader.Parse(combobox_xaml) as DataTemplate;

						column.Header = iter.Item2;                                      // 填写列名称;
						column.CellTemplate = TextBlockTemplate;                         // 将单元格的显示形式赋值;
						column.CellEditingTemplate = ComboBoxTemplate;                   // 将单元格的编辑形式赋值;
																						 //column.Width = 230;                                              // 设置显示宽度;
						column.IsReadOnly = (iter.Item3 as DataGrid_Cell_MIB_ENUM).m_bIsReadOnly;

						this.DynamicDataGrid.Columns.Add(column);
					}
					else if (iter.Item3 is System.Collections.Generic.List<string>)
					{
						DataGridTemplateColumn column = new DataGridTemplateColumn();                   // 单元格是一个template;
						DataTemplate template = new DataTemplate();                                     // 用一个DataTemplate类型填充;
						ComboBox box = new ComboBox();

						string xaml1 =
							@"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
											xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
											xmlns:model='clr-namespace:WPF.Model'>
								<ComboBox ItemsSource='{Binding " + iter.Item1 + @"}' SelectedIndex='0'/>
							</DataTemplate>";

						template = XamlReader.Parse(xaml1) as DataTemplate;

						column.Header = iter.Item2;                               // 填写列名称;
						column.CellTemplate = template;                           // 将单元格的显示形式赋值;
																				  //column.Width = 230;                                       // 设置显示宽度;

						this.DynamicDataGrid.Columns.Add(column);
					}
					else if (iter.Item3 is DataGrid_Cell_MIB_BIT)
					{
						// 当前添加的表格类型只有Text类型，应该使用工厂模式添加对应不同的数据类型;
						var column = new DataGridTextColumn
						{
							Header = iter.Item2,
							IsReadOnly = (iter.Item3 as DataGrid_Cell_MIB_BIT).m_bIsReadOnly,
							Binding = new Binding(iter.Item1 + ".m_Content")
						};

						this.DynamicDataGrid.Columns.Add(column);
					}
					else
					{
					}
				}
			}
		}

		private DyDataGrid_MIBModel m_selectDataGrid;

		/// <summary>
		/// 0为查询，1为添加，2为删除，3为修改
		/// </summary>
		private int m_operType = 0;

		/// <summary>
		/// 动态表构造函数;
		/// </summary>
		public MainDataGrid()
		{
			InitializeComponent();

			this.DynamicDataGrid.MouseMove += DynamicDataGrid_MouseMove;                          // 鼠标移动到单元格位置上边的时候;
			this.DynamicDataGrid.BeginningEdit += DynamicDataGrid_BeginningEdit;                  // 当表格发生正在编辑的状态;
			this.DynamicDataGrid.SelectionChanged += DynamicDataGrid_SelectionChanged;            // 当用户的选择发生变化的时候(用在枚举、BIT类型修改完成后);
			this.DynamicDataGrid.GotMouseCapture += DynamicDataGrid_GotMouseCapture;              // 捕获鼠标事件，用于判断用户拖拽事件;
			this.DynamicDataGrid.LostFocus += DynamicDataGrid_LostFocus;                          // 单元格失去焦点事件;
			this.DynamicDataGrid.MouseRightButtonDown += DynamicDataGrid_MouseRightButtonDown;    //鼠标右键按下弹出右键菜单
		}

		/// <summary>
		/// 单元格失去焦点事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DynamicDataGrid_LostFocus(object sender, RoutedEventArgs e)
		{
			// 目前只处理ComboBox和TextBox
			if (typeof(ComboBox) != e.OriginalSource.GetType()
				&& typeof(TextBox) != e.OriginalSource.GetType())
			{
				return;
			}

            // 变更后的值
            string strVal = null;
			// 操作信息
			string strMsg = "";
			// Mib英文名称
			string mibNameEn = null;
			// oid
			string oid = null;
			// 英文与值的对应关系
			Dictionary<string, string> enName2Value = new Dictionary<string, string>();

			var bIsCombobox = false;
			// ComboBox
			if (typeof(ComboBox) == e.OriginalSource.GetType())
			{
				ComboBox cbx = (ComboBox)e.OriginalSource;
				if (cbx.IsDropDownOpen) // 如果是打开状态不处理
				{
					return;
				}

				// 获取ComboBox的值
				KeyValuePair<int, string> keyVal = (KeyValuePair<int, string>)cbx.SelectedItem;
				strVal = keyVal.Key.ToString();
				bIsCombobox = true;
			}

			// TextBox
			if (typeof(TextBox) == e.OriginalSource.GetType())
			{
				// 获取TextBox的值
				TextBox tbx = (TextBox)e.OriginalSource;
				strVal = tbx.Text;
			}

			DataGrid dataGrid = (DataGrid)sender;
			// 行Model
			if (!(dataGrid.CurrentCell.Item is DyDataGrid_MIBModel))
			{
				return;
			}

			DyDataGrid_MIBModel mibModel = (DyDataGrid_MIBModel)dataGrid.CurrentCell.Item;

			// 只判断textbox的值是否修改。 todo combobox控件存在问题，没有存储原始值，无法判断值是否被修改了
			if (!DataGridUtils.GetOidAndEnName(dataGrid.CurrentCell, out oid, out mibNameEn))
			{
				strMsg = string.Format("无法获取Mib英文名称错误，oid:{0}", oid);
				Log.Error(strMsg);
				return;
			}
			if (string.IsNullOrEmpty(mibNameEn))
			{
				strMsg = string.Format("无法获取Mib英文名称，oid:{0}", oid);
				Log.Error(strMsg);
				return;
			}

			// 获取Mib节点属性
			MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByName(mibNameEn, CSEnbHelper.GetCurEnbAddr());
			if (null == mibLeaf)
			{
				strMsg = "无法获取Mib节点信息！";
				Log.Error(strMsg);
				MessageBox.Show(strMsg);
				return;
			}

			// 校验是否包含特殊字符
			if (false == SnmpMibUtil.CheckMibValidChar(strVal, out strMsg))
			{
				Log.Error(string.Format("{0},节点名称：{1}，值：{2}", strMsg, mibNameEn, strVal));
				MessageBox.Show(string.Format("{0}: {1}", mibLeaf.childNameCh, strMsg));
				return;
			}
			// 校验Mib值
			if (false == SnmpMibUtil.CheckMibValueByMibLeaf(mibLeaf, strVal))
			{
				strMsg = string.Format("{0}: 您所设置的值格式错误或不在取值范围内", mibLeaf.childNameCh);
				Log.Error(strMsg);
				MessageBox.Show(strMsg);
				return;
			}

			//获取修改的单元格数据，而不是下发整行数据。
			Dictionary<string, object> lineDataPro = new Dictionary<string, object>();
			if (mibModel.Properties.ContainsKey(mibNameEn))
			{
				if (!lineDataPro.ContainsKey(mibNameEn))
					lineDataPro.Add(mibNameEn, mibModel.Properties[mibNameEn]);
			}

			// 判断值是否有变化
			if (!bIsCombobox && !DataGridUtils.IsValueChanged(lineDataPro, mibNameEn, strVal))
			{
				return;
			}

			enName2Value.Add(mibNameEn, strVal);
			// 组装Vb列表
			List<CDTLmtbVb> setVbs = new List<CDTLmtbVb>();
			if (false == DataGridUtils.MakeSnmpVbs(lineDataPro, enName2Value, ref setVbs, out strMsg, m_operType))
			{
				Log.Error(strMsg);
				return;
			}

			// SNMP Set
			long requestId;
			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			// 发送SNMP Set命令
			int res = CDTCmdExecuteMgr.VbsSetSync(setVbs, out requestId, CSEnbHelper.GetCurEnbAddr(), ref lmtPdu, true);
			if (res != 0)
			{
				strMsg = string.Format("CDTCmdExecuteMgr.VbsSetSync()方法调用失败，EnbIp:{0}", CSEnbHelper.GetCurEnbAddr());
				Log.Error(strMsg);
				return;
			}

			var es = lmtPdu.m_LastErrorStatus;
			if (0 != es)
			{
				MessageBox.Show($"参数修改失败，错误码：{es}，错误描述：{SnmpErrDescHelper.GetErrDescById(es)}");
				return;
			}

			// 使用返回的Pdu更新DataGrid
			/*if (false == DataGridUtils.UpdateGridByPdu(lineDataPro, lmtPdu))
			{
				strMsg = "使用CDTLmtbPdu更新DataGrid时出错!";
				Log.Error(strMsg);
				return;
			}*/

			MessageBox.Show("参数修改成功！");

            m_operType = 3;
            RefreshDataGrid(lmtPdu, (m_ColumnModel.TableProperty as MibTable).indexNum);
        }

		/// <summary>
		/// 单元格开始编辑时;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DynamicDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			dynamic temp = e.Column.GetCellContent(e.Row).DataContext as DyDataGrid_MIBModel;
			// 根据不同的列（既数据类型）改变不同的处理策略;
			try
			{
				temp.JudgePropertyName_StartEditing(e.Column.Header);
			}
			catch (Exception ex)
			{
			}
		}

		private void DynamicDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// 只处理ComboBox

			// 如果SelectedIndex是-1，则表明是初始化过程中调用的;
			// 如果RemovedItems.Count是0的话，则表明是第一次发生变化的时候被调用的;
			if (typeof(ComboBox) == e.OriginalSource.GetType())
			{
				if ((e.OriginalSource as ComboBox).SelectedIndex == -1)
				{
					return;
				}
			}

			try
			{
				if ((sender as DataGrid).SelectedCells[0].Item.GetType() != typeof(DyDataGrid_MIBModel))
					return;
			}
			catch (Exception ex)
			{
				return;
			}

			try
			{
				((sender as DataGrid).SelectedCells[0].Item as DyDataGrid_MIBModel).JudgePropertyName_ChangeSelection(
					(sender as DataGrid).SelectedCells[0].Column.Header.ToString(), (e.OriginalSource as ComboBox).SelectedItem);
			}
			catch
			{
			}

			//m_selectDataGrid = (DyDataGrid_MIBModel)(sender as DataGrid).SelectedCells[0].Item;
		}

		/// <summary>
		/// 鼠标移动事件;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DynamicDataGrid_MouseMove(object sender, MouseEventArgs e)
		{
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

		/// <summary>
		/// 捕获鼠标时;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DynamicDataGrid_GotMouseCapture(object sender, MouseEventArgs e)
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

		private MetroContextMenu dataGridMenu;

		/// <summary>
		/// 保存属性节点信息
		/// </summary>
		private List<CmdMibInfo> listCmdMibInfo;

		/// <summary>
		/// 根据选择的节点树，显示相应的右键菜单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DynamicDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			//获取选择的行数据,目前只能单选，多选后续添加
			if ((sender as DataGrid).CurrentItem is DyDataGrid_MIBModel)
				m_selectDataGrid = (DyDataGrid_MIBModel)(sender as DataGrid).CurrentItem;

			if (m_ColumnModel == null)
				return;

			MibTable table = (m_ColumnModel.TableProperty as MibTable);
			if (table == null)
				return;

			string menuName = table.nameCh;

			listCmdMibInfo?.Clear();
			listCmdMibInfo = Database.GetInstance().GetCmdsInfoByEntryName(table.nameMib, CSEnbHelper.GetCurEnbAddr());
			if (listCmdMibInfo.Count == 0)
				return;

			// 右键菜单的添加
			dataGridMenu = new MetroContextMenu();

			var menuItemAdd = new MetroMenuItem();
			menuItemAdd.Header = "添加命令";
			menuItemAdd.IsEnabled = true;
			dataGridMenu.Items.Add(menuItemAdd);

			var menuItemModify = new MetroMenuItem();
			menuItemModify.Header = "修改 " + menuName;
			menuItemModify.IsEnabled = true;
			dataGridMenu.Items.Add(menuItemModify);

			var menuItemQuery = new MetroMenuItem();
			menuItemQuery.Header = "查询 " + menuName;
			menuItemQuery.IsEnabled = true;
			dataGridMenu.Items.Add(menuItemQuery);

			foreach (CmdMibInfo mibinfo in listCmdMibInfo)
			{
				if (mibinfo.m_cmdType.Equals("1"))//增加
				{
					var menuChildItem = new MetroMenuItem();
					menuChildItem.Header = mibinfo.m_cmdDesc;
					menuChildItem.Click += MenuAddItem_Click; ;
					menuChildItem.IsEnabled = true;

					menuItemAdd.Items.Add(menuChildItem);
				}
				else if (mibinfo.m_cmdType.Equals("3") || mibinfo.m_cmdType.Equals("4"))//修改
				{
					var menuChildItem = new MetroMenuItem();
					menuChildItem.Header = mibinfo.m_cmdDesc;
					menuChildItem.Click += MenuModifyItem_Click;
					if (m_selectDataGrid == null)
						menuChildItem.IsEnabled = false;
					else
						menuChildItem.IsEnabled = true;
					menuItemModify.Items.Add(menuChildItem);
				}
				else if (mibinfo.m_cmdType.Equals("0"))//查询
				{
					var menuChildItem = new MetroMenuItem();
					menuChildItem.Header = mibinfo.m_cmdDesc;
					menuChildItem.Click += MenuQueryItem_Click;
					menuChildItem.IsEnabled = true;
					menuItemQuery.Items.Add(menuChildItem);
				}
				else if (mibinfo.m_cmdType.Equals("2"))//删除
				{
					var menuChildItem = new MetroMenuItem();
					menuChildItem.Header = mibinfo.m_cmdDesc;
					menuChildItem.Click += MenuDeleteItem_Click;
					menuChildItem.IsEnabled = true;
					dataGridMenu.Items.Add(menuChildItem);
				}
			}

			this.ContextMenu = dataGridMenu;
		}

		/// <summary>
		/// 保存当前命令的属性节点信息
		/// </summary>
		private CmdMibInfo cmdMibInfo = new CmdMibInfo();

		/// <summary>
		/// 保存索引节点信息
		/// </summary>
		private List<MibLeaf> listIndexInfo = new List<MibLeaf>();

		private void MenuAddItem_Click(object sender, RoutedEventArgs e)
		{
			var menu = sender as MenuItem;
			if (null == menu)
				return;

			CmdMibInfo info = listCmdMibInfo.Find(p => p.m_cmdDesc.Equals(menu.Header));

			if (info != null)
			{
				m_operType = int.Parse(info.m_cmdType);
				//判断是否含有默认值，如果含有的默认值与列表个数相同，不显示窗口，直接根据默认值下发指令
				if (info.m_leafDefault != null)
				{
					if (info.m_leaflist.Count == info.m_leafDefault.Count)
					{
						return;
					}
				}

				MainDataParaSetGrid paraGrid = new MainDataParaSetGrid(this);
				paraGrid.InitAddParaSetGrid(info, (m_ColumnModel.TableProperty as MibTable), m_operType);
				paraGrid.ShowDialog();

				if (!paraGrid.bOK)
				{
					return;
				}
			}
		}

		private void MenuModifyItem_Click(object sender, RoutedEventArgs e)
		{
			var menu = sender as MenuItem;
			if (null == menu)
				return;

			CmdMibInfo info = listCmdMibInfo.Find(p => p.m_cmdDesc.Equals(menu.Header));
			if (info != null)
			{
				m_operType = int.Parse(info.m_cmdType);

				//判断是否含有默认值，如果含有的默认值与列表个数相同，不显示窗口，直接根据默认值下发指令
				if (info.m_leafDefault != null)
				{
					if (info.m_leaflist.Count == info.m_leafDefault.Count)
					{
						ModifySpecialProcess(info);
						return;
					}
				}

				MainDataParaSetGrid paraGrid = new MainDataParaSetGrid(this);
				paraGrid.InitModifyParaSetGrid(info, m_selectDataGrid, (m_ColumnModel.TableProperty as MibTable), m_operType);
				paraGrid.ShowDialog();

				if (!paraGrid.bOK)
				{
					return;
				}
			}
		}

		private void MenuQueryItem_Click(object sender, RoutedEventArgs e)
		{
			var menu = sender as MenuItem;
			if (null == menu)
				return;

			MibTable tbl = (m_ColumnModel.TableProperty as MibTable);

			CmdMibInfo info = listCmdMibInfo.Find(p => p.m_cmdDesc.Equals(menu.Header));
			if (info != null && tbl != null)
			{
				m_operType = int.Parse(info.m_cmdType);

				Dictionary<string, string> dicMibToValue = new Dictionary<string, string>();
				Dictionary<string, string> dicMibToOid = new Dictionary<string, string>();
				GetChildMibInfo(info, ref dicMibToValue, ref dicMibToOid);

				//无索引节点，不弹出窗口，直接下发查询指令
				if (tbl.indexNum == 0)
				{
					if (CommLinkPath.GetMibValueFromCmdExeResult(".0", info.m_cmdNameEn, ref dicMibToValue, CSEnbHelper.GetCurEnbAddr()))
					{
						QuerySuccessRefreshDataGrid(dicMibToValue, dicMibToOid, 0, "");
					}
				}
				else if (tbl.indexNum > 0)//弹出包含索引节点的窗口
				{
					MainDataParaSetGrid paraGrid = new MainDataParaSetGrid(this);
					paraGrid.InitQueryParaSetGrid(info, tbl, m_operType);
					paraGrid.ShowDialog();

					if (!paraGrid.bOK)
					{
						return;
					}
				}
			}
		}

		private void MenuDeleteItem_Click(object sender, RoutedEventArgs e)
		{
			var menu = sender as MenuItem;
			if (null == menu)
				return;

			CmdMibInfo info = listCmdMibInfo.Find(p => p.m_cmdDesc.Equals(menu.Header));
			if (info != null)
			{
				m_operType = int.Parse(info.m_cmdType);
				if (m_selectDataGrid == null)
					return;

				MibLeaf leaf = GetRowStatusInfo(info);
				if (leaf == null)
					return;

				DeleteCmd(info, leaf, "6");
			}
		}

		/// <summary>
		/// 查询时，获取查询包含的命令
		/// </summary>
		/// <param name="info"></param>
		/// <param name="dicMibToValue"></param>
		/// <param name="dicMibToOid"></param>
		public void GetChildMibInfo(CmdMibInfo info, ref Dictionary<string, string> dicMibToValue, ref Dictionary<string, string> dicMibToOid)
		{
			MibTable tbl = (m_ColumnModel.TableProperty as MibTable);
			var childlist = tbl.childList;

			if (dicMibToValue == null)
				dicMibToValue = new Dictionary<string, string>();
			if (dicMibToOid == null)
				dicMibToOid = new Dictionary<string, string>();

			foreach (string oid in info.m_leaflist)
			{
				MibLeaf leaf = childlist.Find(p => p.childOid.Equals(oid));
				if (leaf != null)
				{
					dicMibToValue.Add(leaf.childNameMib, null);
					dicMibToOid.Add(leaf.childNameMib, leaf.childOid);
				}
			}
		}

		/// <summary>
		/// 查询成功刷新列表
		/// </summary>
		/// <param name="dicMibToValue"></param>
		/// <param name="indexNum"></param>
		public void QuerySuccessRefreshDataGrid(Dictionary<string, string> dicMibToValue, Dictionary<string, string> dicMibToOid, int indexNum, string strdes)
		{
			//获取列表内容
			ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
			datalist = (ObservableCollection<DyDataGrid_MIBModel>)this.DynamicDataGrid.DataContext;
			int count = 0;

			foreach (string key in dicMibToValue.Keys)
			{
				string value = dicMibToValue[key];
				if (value == null)
					continue;

				if (indexNum == 0)
				{
					string oid = SnmpToDatabase.GetMibPrefix() + dicMibToOid[key] + ".0";

					foreach (DyDataGrid_MIBModel mm in datalist)
					{
						if (mm.Properties.ContainsKey(key))
						{
							if (mm.Properties[key] is DataGrid_Cell_MIB)
							{
								var iter = mm.Properties[key] as DataGrid_Cell_MIB;
								if (iter.oid.Equals(oid))
									iter.m_Content = SnmpToDatabase.ConvertSnmpValueToString(key, value, CSEnbHelper.GetCurEnbAddr()) as string;
							}
							else if (mm.Properties[key] is DataGrid_Cell_MIB_ENUM)
							{
								var iter = mm.Properties[key] as DataGrid_Cell_MIB_ENUM;
								if (iter.oid.Equals(oid))
								{
									iter.m_CurrentValue = int.Parse(value);
									iter.m_Content = SnmpToDatabase.ConvertSnmpValueToString(key, value, CSEnbHelper.GetCurEnbAddr()) as string;
								}
							}
						}
					}
				}
				else if (indexNum > 0)
				{
					foreach (DyDataGrid_MIBModel mm in datalist)
					{
						//根据查询的索引内容查询列表中是否存在，存在直接修改，不存在则添加
						if (mm.Properties.ContainsKey("indexlist"))
						{
							if (!(mm.Properties["indexlist"] as DataGrid_Cell_MIB).m_Content.Equals(strdes))
								continue;
						}

						count++;

						if (mm.Properties.ContainsKey(key))
						{
							if (mm.Properties[key] is DataGrid_Cell_MIB)
							{
								var iter = mm.Properties[key] as DataGrid_Cell_MIB;
								iter.m_Content = SnmpToDatabase.ConvertSnmpValueToString(key, value, CSEnbHelper.GetCurEnbAddr()) as string;
							}
							else if (mm.Properties[key] is DataGrid_Cell_MIB_ENUM)
							{
								var iter = mm.Properties[key] as DataGrid_Cell_MIB_ENUM;
								iter.m_CurrentValue = int.Parse(value);
								iter.m_Content = SnmpToDatabase.ConvertSnmpValueToString(key, value, CSEnbHelper.GetCurEnbAddr()) as string;
							}
						}
					}
				}
			}

			this.DynamicDataGrid.DataContext = null;
			//if(count == 0)

			this.DynamicDataGrid.DataContext = datalist;
		}

		/// <summary>
		/// 获取行状态信息，用于右键删除指令
		/// </summary>
		/// <returns></returns>
		private MibLeaf GetRowStatusInfo(CmdMibInfo info)
		{
			MibLeaf mibLeaf = null;
			foreach (string oid in info.m_leaflist)
			{
				mibLeaf = Database.GetInstance().GetMibDataByOid(oid, CSEnbHelper.GetCurEnbAddr());
				if (mibLeaf.ASNType.Equals("RowStatus"))
				{
					return mibLeaf;
				}
			}

			return mibLeaf;
		}

		public void RefreshDataGrid(CDTLmtbPdu lmtPdu, int nIndexGrade)
		{
			//获取列表内容
			ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
			datalist = (ObservableCollection<DyDataGrid_MIBModel>)this.DynamicDataGrid.DataContext;

			if (lmtPdu == null || lmtPdu.VbCount() == 0)
				return;

			dynamic model = new DyDataGrid_MIBModel();
			var strOidPrefix = SnmpToDatabase.GetMibPrefix();
            string strIndex = "";

            var lmtVbCount = lmtPdu.VbCount();

			for (var i = 0; i < lmtVbCount; i++)
			{
				var lmtVb = lmtPdu.GetVbByIndexEx(i);
				var strVbOid = lmtVb?.Oid;
				if (string.IsNullOrEmpty(strVbOid))
				{
					continue;
				}

				// 根据Vb 中的OID 获取 MibOid
				// 去掉前缀				
				if (nIndexGrade > 0)
				{
					strIndex = MibStringHelper.GetIndexValueByGrade(strVbOid, nIndexGrade);
					if (null == strIndex)
					{
						Log.Error($"根据fulloid:{strVbOid}截取{nIndexGrade}维索引失败");
						continue;
					}
				}
				else if (nIndexGrade == 0)
				{
					strIndex = ".0";
				}

				var strMibOid = strVbOid.Replace(strOidPrefix, "");
				strMibOid = strMibOid.Substring(0, strMibOid.Length - strIndex.Length);
				if (string.IsNullOrEmpty(strMibOid))
				{
					continue;
				}

				var mibLeaf = SnmpMibUtil.GetMibNodeInfoByOID(lmtPdu.m_SourceIp, strMibOid);
				if (null == mibLeaf)
				{
					continue;
				}

				if (mibLeaf.IsIndex.Equals("False"))
				{
					if (m_operType == 1)//添加新行数据
					{
						var dgm = DataGridCellFactory.CreateGridCell(mibLeaf.childNameMib, mibLeaf.childNameCh, lmtVb.Value, lmtVb.Oid, CSEnbHelper.GetCurEnbAddr());
						model.AddProperty(mibLeaf.childNameMib, dgm, mibLeaf.childNameCh);

                        //分页处理，增加的数据也需要添加到内存中，针对数据存在多页的情况，单页数据没有影响
                        if (totalPageNum > 1)
                        {
                            if (!LineDataList.ContainsKey(strIndex))
                            {
                                LineDataList.Add(strIndex, new Dictionary<string, string>());
                                LineDataList[strIndex].Add(lmtVb.Oid, lmtVb.Value);
                            }
                            else
                            {
                                if (!LineDataList[strIndex].ContainsKey(lmtVb.Oid))
                                    LineDataList[strIndex].Add(lmtVb.Oid, lmtVb.Value);
                            }
                        }
                    }
					else if (m_operType == 3 || m_operType == 4)//修改直接更新值
					{
						foreach (DyDataGrid_MIBModel mm in datalist)
						{
							if (mm.Properties.ContainsKey(mibLeaf.childNameMib))
							{
								if (mm.Properties[mibLeaf.childNameMib] is DataGrid_Cell_MIB)
								{
									var iter = mm.Properties[mibLeaf.childNameMib] as DataGrid_Cell_MIB;
									if (iter.oid.Equals(lmtVb.Oid))
										iter.m_Content = SnmpToDatabase.ConvertSnmpValueToString(mibLeaf.childNameMib, lmtVb.Value, CSEnbHelper.GetCurEnbAddr()) as string;
								}
								else if (mm.Properties[mibLeaf.childNameMib] is DataGrid_Cell_MIB_ENUM)
								{
									var iter = mm.Properties[mibLeaf.childNameMib] as DataGrid_Cell_MIB_ENUM;
									if (iter.oid.Equals(lmtVb.Oid))
									{
										iter.m_CurrentValue = int.Parse(lmtVb.Value);
										iter.m_Content = SnmpToDatabase.ConvertSnmpValueToString(mibLeaf.childNameMib, lmtVb.Value, CSEnbHelper.GetCurEnbAddr()) as string;
									}
								}
							}
						}

                        //分页处理，修改的数据更新到内存中
                        if (totalPageNum > 1 && LineDataList.ContainsKey(strIndex))
                        {
                            foreach (KeyValuePair<string, string> kv in LineDataList[strIndex])
                            {
                                if(kv.Key.Equals(lmtVb.Oid))
                                {
                                    if(!kv.Value.Equals(lmtVb.Value))
                                    {
                                        LineDataList[strIndex][kv.Key] = lmtVb.Value;
                                        break;
                                    }
                                }
                            }
                        }
                    }
				}
			}

			string strOid = "";
			string showinfo = "";
			if (nIndexGrade > 0)
			{
				if (DataGridUtils.GetIndexNodeInfoFromLmtbPdu(lmtPdu, (m_ColumnModel.TableProperty as MibTable), ref strOid, ref showinfo))
				{
					model.AddProperty("indexlist", new DataGrid_Cell_MIB()
					{
						m_Content = showinfo,
						oid = strOid + ".",
						m_bIsReadOnly = true,
						MibName_CN = "实例描述",
						MibName_EN = "indexlist"
					}, "实例描述");
				}
			}

			if (model != null)
			{
				if (m_operType == 2)//删除
				{
					foreach (DyDataGrid_MIBModel mm in datalist)
					{
						if (nIndexGrade == 0 && datalist.Count == 1)
						{
							datalist.Remove(mm);
							break;
						}
						else
						{
							var cell = mm.Properties["indexlist"] as DataGrid_Cell_MIB;
							if (cell.m_Content.Equals((model.Properties["indexlist"] as DataGrid_Cell_MIB).m_Content))
							{
								datalist.Remove(mm);
								break;
							}
						}
					}

                    //分页处理，删除的数据更新到内存中
                    if (totalPageNum > 1 && LineDataList.ContainsKey(strIndex))
                    {
                        LineDataList.Remove(strIndex);
                    }
                }

				if (m_operType == 1)
					datalist.Add(model);

				this.DynamicDataGrid.DataContext = null;
				this.DynamicDataGrid.DataContext = datalist;
			}
		}

        /// <summary>
        /// 右键菜单列表数据转换成下发指令所需要的数据格式
        /// </summary>
        /// <param name="datalist">列表数据</param>
        /// <param name="indexGrade">索引个数</param>
        /// <param name="strIndex">索引值</param>
        /// <param name="cellData">转换后的数据</param>
        public void ModelConvertToDic(ObservableCollection<DyDataGrid_MIBModel> datalist, ref int indexGrade, ref string strIndex, ref Dictionary<string, object> cellData,bool bisIndex)
        {
            string strMsg;
            string strPreOid = SnmpToDatabase.GetMibPrefix();
            // 索引
            string strFullOid = "";

            if (cellData == null)
                cellData = new Dictionary<string, object>();

            // Mib值校验
            foreach (DyDataGrid_MIBModel mm in datalist)
            {
                string strMibVal = null;
                var cell = mm.Properties["ParaValue"] as GridCell;
                if (cell.cellDataType == LmtbSnmp.DataGrid_CellDataType.enumType)
                {
                    var emnuCell = cell as DataGrid_Cell_MIB_ENUM;
                    if (emnuCell != null)
                        strMibVal = emnuCell.m_CurrentValue.ToString();
                    else
                        strMibVal = cell.m_Content;
                }
                else
                    strMibVal = cell.m_Content;

                // 获取Mib节点属性
                MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByName(cell.MibName_EN, CSEnbHelper.GetCurEnbAddr());
                if (null == mibLeaf)
                {
                    strMsg = "无法获取Mib节点信息！";
                    Log.Error(strMsg);
                    MessageBox.Show(strMsg);
                    return;
                }

                // 获取索引节点
                if ("True".Equals(mibLeaf.IsIndex) && m_operType == 1 && bisIndex) // 只有添加时才获取索引
                {
                    strIndex += "." + strMibVal;
                    indexGrade++;
                    continue;
                }

                if (!cell.oid.Contains(strPreOid))
                {
                    strFullOid = strPreOid + cell.oid + strIndex;
                }
                else
                {
                    strFullOid = cell.oid + strIndex;
                }

                cell.oid = strFullOid;        

                if (!cellData.ContainsKey(mibLeaf.childNameMib))
                {
                    cellData.Add(mibLeaf.childNameMib, mm.Properties["ParaValue"]);
                }
            }
        }

        public bool AddAndModifyCmd(Dictionary<string,object> lineData,ref CDTLmtbPdu lmtPdu)
        {
            string strMsg;
            // 向基站下发添加指令
            // Mib英文名称与值的对应关系
            Dictionary<string, string> enName2Value = new Dictionary<string, string>();
            // 根据DataGrid行数据组装Mib英文名称与值的对应关系
            if (false == DataGridUtils.MakeEnName2Value(lineData, ref enName2Value))
            {
                strMsg = "DataGridUtils.MakeEnName2Value()方法执行错误！";
                Log.Error(strMsg);
                MessageBox.Show("添加参数失败！");
                return false;
            }

            // 组装Vb列表
            List<CDTLmtbVb> setVbs = new List<CDTLmtbVb>();
            if (false == DataGridUtils.MakeSnmpVbs(lineData, enName2Value, ref setVbs, out strMsg,m_operType))
            {
                Log.Error(strMsg);
                return false;
            }

            // SNMP Set
            long requestId;

            if(lmtPdu == null)
                lmtPdu = new CDTLmtbPdu();
            // 发送SNMP Set命令
            int res = CDTCmdExecuteMgr.VbsSetSync(setVbs, out requestId, CSEnbHelper.GetCurEnbAddr(), ref lmtPdu, true);
            if (res != 0)
            {
                strMsg = string.Format("参数配置失败，EnbIP:{0}", CSEnbHelper.GetCurEnbAddr());
                Log.Error(strMsg);
                MessageBox.Show(strMsg);
                return false;
            }

            return true;
        }

        private void DeleteCmd(CmdMibInfo info, MibLeaf leaf, string value)
        {
            // 找到tbl信息
            MibTable tblInfo;
            string errMsg;
            if (!Database.GetInstance().GetMibDataByTableName(info.m_tableName, out tblInfo,
                CSEnbHelper.GetCurEnbAddr(), out errMsg))
            {
                MessageBox.Show($"根据表名{info.m_tableName}查找MIB表信息失败，请确认MIB版本是否匹配");
                return;
            }

            var nIdxCount = tblInfo.indexNum;

            //添加删除指令
            string strMsg = null;
            string strOidPrefix = SnmpToDatabase.GetMibPrefix();
            // 获取索引
            string strIndex = null;
            DataGridUtils.GetMibIndex(m_selectDataGrid.Properties, nIdxCount, out strIndex);

            // 组装Vb列表
            List<CDTLmtbVb> setVbs = new List<CDTLmtbVb>();
            // 组装Vb
            CDTLmtbVb lmtVb = new CDTLmtbVb();
            lmtVb.Oid = strOidPrefix + leaf.childOid + strIndex;
            lmtVb.Value = value;
            lmtVb.SnmpSyntax = LmtbSnmpEx.GetSyntax(leaf.mibSyntax);
            setVbs.Add(lmtVb);

            // SNMP Set
            long requestId;
            CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
            // 发送SNMP Set命令
            int res = CDTCmdExecuteMgr.VbsSetSync(setVbs, out requestId, CSEnbHelper.GetCurEnbAddr(), ref lmtPdu, true);
            if (res != 0)
            {
                strMsg = string.Format("参数删除失败，EnbIP:{0}", CSEnbHelper.GetCurEnbAddr());
                Log.Error(strMsg);
                MessageBox.Show(strMsg);
                return;
            }
            // 判读SNMP响应结果
            if (lmtPdu.m_LastErrorStatus != 0)
            {
                strMsg = string.Format("参数删除失败，错误信息:{0}", lmtPdu.m_LastErrorStatus);
                Log.Error(strMsg);
                MessageBox.Show(strMsg);
                return;
            }

            MessageBox.Show("参数删除成功！");

            RefreshDataGrid(lmtPdu, nIdxCount);
        }

        /// <summary>
        /// 右键菜单包含一些cmdType=4的一些操作
        /// </summary>
        /// <param name="info"></param>
        private void ModifySpecialProcess(CmdMibInfo info)
        {
            MibLeaf mibLeaf = null;
            ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
            Dictionary<string, object> lineData = new Dictionary<string, object>();
            foreach (string oid in info.m_leafDefault.Keys)
            {
                mibLeaf = Database.GetInstance().GetMibDataByOid(oid, CSEnbHelper.GetCurEnbAddr());
                if (mibLeaf != null)
                {
                    if (mibLeaf.ASNType.Equals("RowStatus") && info.m_leafDefault[oid].Equals("6"))
                    {
                        DeleteCmd(info, mibLeaf, info.m_leafDefault[oid]);
                    }
                    else
                    {
                        dynamic model = new DyDataGrid_MIBModel();

                        string devalue = info.m_leafDefault[oid];

                        var dgm = DataGridCellFactory.CreateGridCell(mibLeaf.childNameMib, mibLeaf.childNameCh, devalue, mibLeaf.childOid, CSEnbHelper.GetCurEnbAddr());

                        model.AddParaProperty("ParaValue", dgm, "参数值");

                        // 将这个整行数据填入List;
                        if (model.Properties.Count != 0)
                        {
                            // 向单元格内添加内容;
                            datalist.Add(model);
                        }
                    }
                }
            }

            int indexGrade = (m_ColumnModel.TableProperty as MibTable).indexNum;
            dynamic mibmodel = new DyDataGrid_MIBModel();
            CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
            string strMsg;
            string strIndex = "";
            if (datalist.Count == 0)
                return;

            if (string.IsNullOrWhiteSpace(strIndex))
                GetCurrentSelectIndex(ref strIndex);

            ModelConvertToDic(datalist, ref indexGrade, ref strIndex, ref lineData,false);
            if (!AddAndModifyCmd(lineData, ref lmtPdu))
                return;

            // 判读SNMP响应结果
            if (lmtPdu.m_LastErrorStatus != 0)
            {
                strMsg = $"参数配置失败，错误信息:{SnmpErrDescHelper.GetErrDescById(lmtPdu.m_LastErrorStatus)}";
                Log.Error(strMsg);
                MessageBox.Show(strMsg);
                return;
            }

            if (m_operType == 3 || m_operType == 4)
            {
                MessageBox.Show("参数修改成功！");
            }
            else if (m_operType == 1)
            {
                MessageBox.Show("参数添加成功！");
            }

            //下发指令成功后更新基本信息列表
            if (m_operType == 3 || m_operType == 4)
                indexGrade = (m_ColumnModel.TableProperty as MibTable).indexNum;
            RefreshDataGrid(lmtPdu, indexGrade);
        }

        private void GetCurrentSelectIndex(ref string strIndex)
        {
            if (m_selectDataGrid == null)
                return;

            foreach (var iter in m_selectDataGrid.Properties)
            {
                dynamic model = new DyDataGrid_MIBModel();
                if (iter.Key.Equals("indexlist"))
                    continue;

                MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByName(iter.Key, CSEnbHelper.GetCurEnbAddr());

                if (mibLeaf == null)
                    continue;

                if (iter.Value is DataGrid_Cell_MIB)
                {
                    var cellGrid = iter.Value as DataGrid_Cell_MIB;

                    if ((m_ColumnModel.TableProperty as MibTable).indexNum > 0)
                        strIndex = MibStringHelper.GetIndexValueByGrade(cellGrid.oid, (m_ColumnModel.TableProperty as MibTable).indexNum);
                }
                else if (iter.Value is DataGrid_Cell_MIB_ENUM)
                {
                    var cellGrid = iter.Value as DataGrid_Cell_MIB_ENUM;

                    if ((m_ColumnModel.TableProperty as MibTable).indexNum > 0)
                        strIndex = MibStringHelper.GetIndexValueByGrade(cellGrid.oid, (m_ColumnModel.TableProperty as MibTable).indexNum);
                }

                if (!string.IsNullOrWhiteSpace(strIndex))
                    break;
            }

            if ((m_ColumnModel.TableProperty as MibTable).indexNum == 0)
                strIndex = ".0";
        }

        #region 基本信息列表分页处理
        private int perPageLineNum = 50;//每页显示行数
        private int totalLineNum;//总行数
        private int totalPageNum = 1;//总页数
        private Dictionary<string, string> oid_cn;
        private Dictionary<string, string> oid_en;
        private MibTable mibTable;
        private int IndexCount = 0;
        /// <summary>
        /// 行数据，key为索引,value为oid与值得键值对
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> LineDataList = new Dictionary<string, Dictionary<string, string>>();

        public void SetDataGridInfo(Dictionary<string, string> oidcn, Dictionary<string, string> oiden, MibTable table, int indexCount)
        {
            if (LineDataList.Count == 0)
                return;

            oid_cn = oidcn;
            oid_en = oiden;
            mibTable = table;
            IndexCount = indexCount;

            tbkShowLineNum.Text = perPageLineNum.ToString();
            totalLineNum = LineDataList.Count;
            SetPageInfo();           
            tbkCurrentPage.Text = "1";
        }
        /// <summary>
        /// 每页显示的行数改变时，需要重新设置总页数
        /// </summary>
        private void SetPageInfo()
        {                    
            if (totalLineNum % perPageLineNum == 0)
                totalPageNum = totalLineNum / perPageLineNum;
            else
                totalPageNum = totalLineNum / perPageLineNum + 1;

            tbkTotal.Text = totalPageNum.ToString();

            if (totalPageNum == 1)
                PageInfo.Visibility = Visibility.Collapsed;
            else
                PageInfo.Visibility = Visibility.Visible;    
        }
        /// <summary>
        /// 根据设置的页数刷新界面的显示
        /// </summary>
        /// <param name="curentPage"></param>
        public void RefreshDataGridPage(int curentPage)
        {
            if (LineDataList.Count == 0)
                return;
            tbkCurrentPage.Text = curentPage.ToString();

            DynamicDataGrid.DataContext = null;
            var itemCount = 0;
            int count = 0;
            ObservableCollection<DyDataGrid_MIBModel> contentlist = new ObservableCollection<DyDataGrid_MIBModel>();

            foreach (string key in LineDataList.Keys)
            {
                count++;
                if(curentPage == 1)
                {
                    if (curentPage * perPageLineNum < count)
                        return;
                }
                else
                {
                    if(!((count > (curentPage - 1) * perPageLineNum) && (count < curentPage * perPageLineNum)))
                    {
                        continue;
                    }

                    if (count > curentPage * perPageLineNum)
                        break;
                }
                
                // 创建一个能够填充到DataGrid控件的动态类型，这个类型的所有属性来自于读取的所有MIB节点;
                dynamic model = new DyDataGrid_MIBModel();
                string keyTem = key.Trim('.');
                string[] temp = keyTem.Split('.');

                // 当多因维度大于0，即该表为矢量表的时候为该列添加表头;
                if (IndexCount > 0)
                {
                    var IndexContent = "";
                    for (int i = 0; i < IndexCount; i++)
                    {
                        string IndexOIDTemp = SnmpToDatabase.GetMibPrefix() + mibTable.oid + "." + (i + 1);
                        IndexContent += oid_cn[IndexOIDTemp] + temp[i];
                    }

                    // 如下DataGrid_Cell_MIB中的 oid暂时填写成这样;
                    // 参数一：属性名称;
                    // 参数二：单元格实例;
                    // 参数三：单元格列中文名称;
                    model.AddProperty("indexlist", new DataGrid_Cell_MIB()
                    {
                        m_Content = IndexContent,
                        oid = SnmpToDatabase.GetMibPrefix() + mibTable.oid + ".",
                        m_bIsReadOnly = true,
                        MibName_CN = "实例描述",
                        MibName_EN = "indexlist"
                    }, "实例描述");
                }

                if (mibTable != null)
                {
                    model.AddTableProperty(mibTable);
                }

                foreach (var iter in LineDataList[key])
                {
                    string temp_compare = iter.Key.Remove(iter.Key.Length - key.Length);

                    // 如果OID匹配;
                    if (oid_cn.ContainsKey(temp_compare))
                    {
                        Debug.WriteLine("Add Property:" + oid_en[temp_compare] + " Value:" + iter.Value + " and Header is:" + oid_cn[temp_compare]);

                        // 在这里要区分DataGrid要显示的数据类型;
                        var dgm = DataGridCellFactory.CreateGridCell(oid_en[temp_compare], oid_cn[temp_compare]
                            , iter.Value, iter.Key, CSEnbHelper.GetCurEnbAddr());

                        // 第一个参数：属性的名称——节点英文名称;
                        // 第二个参数：属性的实例——DataGrid_Cell_MIB实例;
                        // 第三个参数：列要显示的中文名——节点的中文友好名;
                        model.AddProperty(oid_en[temp_compare], dgm, oid_cn[temp_compare]);
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
                    ColumnModel = model;
                    DynamicDataGrid.DataContext = contentlist;
                }
            }

        }
        /// <summary>
        /// 跳转到指定页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if (totalPageNum == 1)
                return;

            int pageNum = int.Parse(tbxPageNum.Text);
            int total = int.Parse(tbkTotal.Text);//总页数
            if(pageNum >= 1 && pageNum <= total)
            {
                RefreshDataGridPage(pageNum);
            }
        }
        /// <summary>
        /// 跳转到上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (totalPageNum == 1)
                return;

            int currentPage = int.Parse(tbkCurrentPage.Text);//获取当前页数
            if(currentPage > 1)
            {
                RefreshDataGridPage(currentPage - 1);
            }
        }
        /// <summary>
        /// 跳转到下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (totalPageNum == 1)
                return;

            int total = int.Parse(tbkTotal.Text);//总页数
            int currentPage = int.Parse(tbkCurrentPage.Text);//获取当前页数
            if (currentPage < total)
            {
                RefreshDataGridPage(currentPage + 1);
            }
        }
        /// <summary>
        /// 为了兼容不同显示器，可改变界面显示的行数，默认一页显示30行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbkShowLineNum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!perPageLineNum.Equals(int.Parse(tbkShowLineNum.Text)))
            {
                perPageLineNum = int.Parse(tbkShowLineNum.Text);
                SetPageInfo();
                RefreshDataGridPage(int.Parse(tbkCurrentPage.Text));
            }
        }
        #endregion
    }
}