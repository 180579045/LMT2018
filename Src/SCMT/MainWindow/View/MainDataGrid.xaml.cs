using CommonUtility;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using MsgQueue;
using SCMTMainWindow.Component.ViewModel;
using SCMTMainWindow.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UICore.Controls.Metro;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using System.Collections.ObjectModel;

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
                            IsReadOnly = true,
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
                        column.Width = 230;                                              // 设置显示宽度;
                        column.IsReadOnly = true;

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
                        column.Width = 230;                                       // 设置显示宽度;
                        column.IsReadOnly = true;

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
			DyDataGrid_MIBModel mibModel = (DyDataGrid_MIBModel)dataGrid.CurrentCell.Item;
			// 行数据
			Dictionary<string, object>  lineDataPro = mibModel.Properties;

			// 获取当前表格的Mib英文名和oid
			if (false == DataGridUtils.GetOidAndEnName(dataGrid.CurrentCell, out oid, out mibNameEn))
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

			// 判断值是否有变化
			if (false == DataGridUtils.IsValueChanged(lineDataPro, mibNameEn, strVal))
			{
				return;
			}

			enName2Value.Add(mibNameEn, strVal);
			// 组装Vb列表
			List<CDTLmtbVb> setVbs = new List<CDTLmtbVb>();
			if (false == DataGridUtils.MakeSnmpVbs(lineDataPro, enName2Value, ref setVbs, out strMsg))
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

			// 使用返回的Pdu更新DataGrid
			if (false == DataGridUtils.UpdateGridByPdu(lineDataPro, lmtPdu))
			{
				strMsg = "使用CDTLmtbPdu更新DataGrid时出错!";
				Log.Error(strMsg);
				return;
			}

			MessageBox.Show("参数修改成功！");

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
            catch(Exception ex)
            {

            }
            
        }

        private void DynamicDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //注释掉原来对基本信息列表的编辑操作
            /*// 只处理ComboBox

           // 如果SelectedIndex是-1，则表明是初始化过程中调用的;
            // 如果RemovedItems.Count是0的话，则表明是第一次发生变化的时候被调用的;
            if (((e.OriginalSource as ComboBox).SelectedIndex == -1) || (e.RemovedItems.Count == 0))
            {
                return;
            }
			if (typeof(ComboBox) != e.OriginalSource.GetType())

			{
				return;
			}
			else
			{
				try
				{
					(sender as DataGrid).SelectedCells[0].Item.GetType();
				}
				catch (Exception ex)
				{
					return;
				}
			}
			try
			{
				((sender as DataGrid).SelectedCells[0].Item as DyDataGrid_MIBModel).JudgePropertyName_ChangeSelection(
					(sender as DataGrid).SelectedCells[0].Column.Header.ToString(), (e.OriginalSource as ComboBox).SelectedItem);
			}
			catch
			{


			}

		}
        */

            //获取选择的行数据,目前只能单选，多选后续添加
            m_selectDataGrid = (DyDataGrid_MIBModel)this.DynamicDataGrid.SelectedItem;
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
            if (m_ColumnModel == null)
                return;

            MibTable table = (m_ColumnModel.TableProperty as MibTable);
            if (table == null)
                return;

            string menuName = table.nameCh;
            CmdInfoList cmdList = new CmdInfoList();
            if (!cmdList.GeneratedCmdInfoList())
                return;

            if (listCmdMibInfo != null && listCmdMibInfo.Count > 0)
                listCmdMibInfo.Clear();
            listCmdMibInfo = cmdList.GetCmdsByTblName(table.nameMib);
            if (listCmdMibInfo.Count == 0)
                return;

            // 右键菜单的添加
            dataGridMenu = new MetroContextMenu();

            var menuItemAdd= new MetroMenuItem();
            menuItemAdd.Header = "添加命令";
            menuItemAdd.Click += MenuItem_Click;
            menuItemAdd.IsEnabled = true;
            dataGridMenu.Items.Add(menuItemAdd);

            var menuItemModify = new MetroMenuItem();
            menuItemModify.Header = "修改 " + menuName;
            menuItemModify.Click += MenuItem_Click; ;
            menuItemModify.IsEnabled = true;
            dataGridMenu.Items.Add(menuItemModify);

            var menuItemQuery = new MetroMenuItem();
            menuItemQuery.Header = "查询 " + menuName;
            menuItemQuery.Click += MenuItem_Click; ;
            menuItemQuery.IsEnabled = true;
            dataGridMenu.Items.Add(menuItemQuery);

            foreach (CmdMibInfo mibinfo in listCmdMibInfo)
            {
                if(mibinfo.m_cmdDesc.Contains("增加"))
                {
                    var menuChildItem = new MetroMenuItem();
                    menuChildItem.Header = mibinfo.m_cmdDesc;
                    menuChildItem.Click += MenuItem_Click;
                    menuChildItem.IsEnabled = true;

                    menuItemAdd.Items.Add(menuChildItem);                 
                }

                if(mibinfo.m_cmdDesc.Contains("修改"))
                {
                    var menuChildItem = new MetroMenuItem();
                    menuChildItem.Header = mibinfo.m_cmdDesc;
                    menuChildItem.Click += MenuItem_Click;
                    menuChildItem.IsEnabled = true;

                    menuItemModify.Items.Add(menuChildItem);                    
                }

                if(mibinfo.m_cmdDesc.Contains("查询"))
                {
                    var menuChildItem = new MetroMenuItem();
                    menuChildItem.Header = mibinfo.m_cmdDesc;
                    menuChildItem.Click += MenuItem_Click;
                    menuChildItem.IsEnabled = true;

                    menuItemQuery.Items.Add(menuChildItem);                  
                }

                if(mibinfo.m_cmdDesc.Contains("删除"))
                {
                    var menuItemDelete = new MetroMenuItem();
                    menuItemDelete.Header = mibinfo.m_cmdDesc;
                    menuItemDelete.Click += MenuItem_Click; ;
                    menuItemDelete.IsEnabled = true;
                    dataGridMenu.Items.Add(menuItemDelete);
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (null == dataGridMenu)
                return;

            var menu = sender as MenuItem;
            if (null == menu)
                return;

			string strMsg = null;
            if (menu.Header.ToString().Contains("增加"))
            {
                foreach (CmdMibInfo info in listCmdMibInfo)
                {
                    if (info.m_cmdDesc.Equals(menu.Header))
                    {
                        MainDataParaSetGrid paraGrid = new MainDataParaSetGrid(this);
                        paraGrid.InitAddParaSetGrid(info, (m_ColumnModel.TableProperty as MibTable));
                        paraGrid.ShowDialog();

                        if (!paraGrid.bOK)
                        {
                            return;
                        }
                        break;
                    }
                }
            }
            else if (menu.Header.ToString().Contains("修改") && !menu.Header.ToString().Contains("修改 " + (m_ColumnModel.TableProperty as MibTable).nameCh))
            {
                MainDataParaSetGrid paraGrid = new MainDataParaSetGrid(this);
                paraGrid.InitModifyParaSetGrid(m_selectDataGrid);
                paraGrid.ShowDialog();

                if (!paraGrid.bOK)
                {
                    return;
                }
            }
            else if(menu.Header.ToString().Contains("删除"))
            {
                foreach (CmdMibInfo info in listCmdMibInfo)
                {
                    if (info.m_cmdDesc.Equals(menu.Header))
                    {
                        if (m_selectDataGrid == null)
                            return;
                        MibLeaf leaf = GetRowStatusInfo(info);
                        if (leaf == null)
                            return;
						//添加删除指令
						// TODO 临时方案
						string strOidPrefix = SnmpToDatabase.GetMibPrefix();
						// 获取索引
						string strIndex = null;
						DataGridUtils.GetMibIndex(m_selectDataGrid.Properties, out strIndex);
						

						// 组装Vb列表
						List<CDTLmtbVb> setVbs = new List<CDTLmtbVb>();
						// 组装Vb
						CDTLmtbVb lmtVb = new CDTLmtbVb();
						lmtVb.Oid = strOidPrefix + leaf.childOid + strIndex;
						lmtVb.Value = "6";
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

						break;
                    }
                }
            }
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
                if(mibLeaf.ASNType.Equals("RowStatus"))
                {
                    return mibLeaf;
                }
            }

            return mibLeaf;
        }
    }
}
