using CommonUtility;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SCMTMainWindow.Component.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using LmtbSnmp;
using SCMTMainWindow.Utils;
using LogManager;
using LinkPath;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// 基本信息右键菜单参数设置
    /// </summary>
    public partial class MainDataParaSetGrid : Window
    {
        /// <summary>
        /// 保存当前命令的属性节点信息
        /// </summary>
        private CmdMibInfo cmdMibInfo = new CmdMibInfo();
        /// <summary>
        /// 保存索引节点信息
        /// </summary>
        private List<MibLeaf> listIndexInfo = new List<MibLeaf>();

        public bool bOK = false;
        /// <summary>
        /// 动态表的所有列信息,该属性必须设置，否则无法正常显示;
        /// 设置该属性之后，动态表就会将所有列对应的模板全部生成;
        /// </summary>
        private DyDataGrid_MIBModel m_ParaModel;

        private MainDataGrid m_MainDataGrid;
        public MainDataParaSetGrid(MainDataGrid dataGrid)
        {
            InitializeComponent();

            m_MainDataGrid = dataGrid;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DynamicParaSetGrid.SelectionChanged += DynamicParaSetGrid_SelectionChanged;
            this.DynamicParaSetGrid.BeginningEdit += DynamicParaSetGrid_BeginningEdit;
            this.DynamicParaSetGrid.MouseMove += DynamicParaSetGrid_MouseMove;
            this.DynamicParaSetGrid.GotMouseCapture += DynamicParaSetGrid_GotMouseCapture;
        }

        private void DynamicParaSetGrid_GotMouseCapture(object sender, MouseEventArgs e)
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

        private void DynamicParaSetGrid_MouseMove(object sender, MouseEventArgs e)
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

        private void DynamicParaSetGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dynamic temp = e.Column.GetCellContent(e.Row).DataContext as DyDataGrid_MIBModel;
            // 根据不同的列（既数据类型）改变不同的处理策略;
            try
            {
                temp.JudgeParaPropertyName_StartEditing(e.Column.Header);
            }
            catch (Exception ex)
            {

            }
        }

        private void DynamicParaSetGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 如果SelectedIndex是-1，则表明是初始化过程中调用的;
            // 如果RemovedItems.Count是0的话，则表明是第一次发生变化的时候被调用的;
            if (((e.OriginalSource as ComboBox).SelectedIndex == -1) || (e.RemovedItems.Count == 0))
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

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
			string strMsg;
            bOK = true;
            //获取右键菜单列表内容
            ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
            datalist = (ObservableCollection < DyDataGrid_MIBModel >)this.DynamicParaSetGrid.DataContext;

            //将右键菜单列表内容转换成与基本信息列表格式相同结构
            dynamic model = new DyDataGrid_MIBModel();
            string value;
            string strPreOid = SnmpToDatabase.GetMibPrefix();
            foreach (DyDataGrid_MIBModel mm in datalist)
            {
                var cell = mm.Properties["ParaValue"] as GridCell;
                if (cell.cellDataType == LmtbSnmp.DataGrid_CellDataType.enumType)
                {
                    var emnuCell = cell as DataGrid_Cell_MIB_ENUM;
                    if(emnuCell != null)
                        value = emnuCell.m_CurrentValue.ToString();
                    else
                        value = cell.m_Content;
                }
                else
                    value = cell.m_Content;

                var dgm = DataGridCellFactory.CreateGridCell(cell.MibName_EN, cell.MibName_CN, value, strPreOid + cell.oid, CSEnbHelper.GetCurEnbAddr());

                model.AddProperty(cell.MibName_EN, dgm, cell.MibName_CN);
            }

			// 像基站下发添加指令
			// Mib英文名称与值的对应关系
			Dictionary<string, string> enName2Value = new Dictionary<string, string>();
			// 根据DataGrid行数据组装Mib英文名称与值的对应关系
			if (false == DataGridUtils.MakeEnName2Value(((DyDataGrid_MIBModel)model).Properties, ref enName2Value))
			{
				strMsg = "DataGridUtils.MakeEnName2Value()方法执行错误！";
				Log.Error(strMsg);
				MessageBox.Show("添加参数失败！");
				return;
			}

			// 组装Vb列表
			List<CDTLmtbVb> setVbs = new List<CDTLmtbVb>();
			if (false == DataGridUtils.MakeSnmpVbs(model, enName2Value, ref setVbs, out strMsg))
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

			MessageBox.Show("参数添加成功！");

			// 修改/查询指令


			//下发指令成功后更新基本信息列表
			//m_MainDataGrid


			this.Close();
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            bOK = false;
            this.Close();
        }

        private void CheckSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        public void InitParaSetGrid(CmdMibInfo mibInfo, MibTable table)
        {
            cmdMibInfo = mibInfo;

            listIndexInfo.Clear();
            foreach (MibLeaf leaf in table.childList)
            {
                if (leaf.IsIndex.Equals("True"))
                    listIndexInfo.Add(leaf);
            }

            this.Title = mibInfo.m_cmdDesc;
            int i = 0;
            ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
            if (cmdMibInfo == null)
                return;
                   
            if (cmdMibInfo.m_cmdDesc.Equals(this.Title))
            {
                if (listIndexInfo.Count > 0)
                {
                    //索引节点
                    foreach (MibLeaf mibLeaf in listIndexInfo)
                    {
                        dynamic model = new DyDataGrid_MIBModel();
                        model.AddParaProperty("ParaName", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.childNameCh,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "参数名称");

                        model.AddParaProperty("ParaValue", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.defaultValue,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "参数值");

                        model.AddParaProperty("ParaValueRange", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.managerValueRange,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "取值范围");

                        model.AddParaProperty("Unit", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.unit,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "单位");

                        // 将这个整行数据填入List;
                        if (model.Properties.Count != 0)
                        {
                            // 向单元格内添加内容;
                            datalist.Add(model);
                            i++;
                        }
                        // 最终全部收集完成后，为控件赋值;
                        if (i == datalist.Count)
                        {
                            this.ParaDataModel = model;
                            this.DynamicParaSetGrid.DataContext = datalist;
                        }
                    }
                }

                if (cmdMibInfo.m_leaflist.Count > 0)
                {
                    //属性节点
                    foreach (string oid in cmdMibInfo.m_leaflist)
                    {
                        MibLeaf mibLeaf = Database.GetInstance().GetMibDataByOid(oid, CSEnbHelper.GetCurEnbAddr());
                        dynamic model = new DyDataGrid_MIBModel();

                        string devalue = ConvertValidValue(mibLeaf);

                        model.AddParaProperty("ParaName", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.childNameCh,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "参数名称");

                        // 在这里要区分DataGrid要显示的数据类型;
                        var dgm = DataGridCellFactory.CreateGridCell(mibLeaf.childNameMib, mibLeaf.childNameCh, devalue, mibLeaf.childOid, CSEnbHelper.GetCurEnbAddr());

                        model.AddParaProperty("ParaValue", dgm, "参数值");

                        model.AddParaProperty("ParaValueRange", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.managerValueRange,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "取值范围");

                        model.AddParaProperty("ParaUnit", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.unit,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "单位");


                        // 将这个整行数据填入List;
                        if (model.Properties.Count != 0)
                        {
                            // 向单元格内添加内容;
                            datalist.Add(model);
                            i++;
                        }

                        // 最终全部收集完成后，为控件赋值;
                        if (i == datalist.Count)
                        {
                            this.ParaDataModel = model;
                            this.DynamicParaSetGrid.DataContext = datalist;
                        }
                    }
                }
            }        
        }
        /// <summary>
        /// 对无效的值"X",根据取值范围进行转换
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>

        private string  ConvertValidValue(MibLeaf leaf)
        {
            string value = leaf.defaultValue;
            if (leaf.defaultValue.Equals("×"))
            {
                if (leaf.OMType.Equals("u32") || leaf.OMType.Equals("s32"))
                {
                    string[] str = leaf.managerValueRange.Split('-');
                    value = str[0];
                }

                if (leaf.OMType.Equals("enum"))
                {
                    // 1.取出该节点的取值范围
                    var mvr = leaf.managerValueRange;

                    // 2.分解取值范围
                    var mapKv = MibStringHelper.SplitManageValue(mvr);
                    if (!mapKv.ContainsValue(value))
                        value = mapKv.FirstOrDefault().Key.ToString();
                }
            }
            else
            {
                string[] devalue = leaf.defaultValue.Split(':');
                value = devalue[0];
            }

            return value;
        }
        
        public DyDataGrid_MIBModel ParaDataModel
        {
            get
            {
                return m_ParaModel;
            }
            set
            {
                m_ParaModel = value;
                this.DynamicParaSetGrid.Columns.Clear();

                // 获取所有列信息，并将列信息填充到DataGrid当中;
                foreach (var iter in m_ParaModel.PropertyList)
                {
                    // 显示字符类型的数据结构;
                    if (iter.Item3 is DataGrid_Cell_MIB)
                    {
                        // 当前添加的表格类型只有Text类型，应该使用工厂模式添加对应不同的数据类型;
                        var column = new DataGridTextColumn
                        {
                            Header = iter.Item2,
                            Binding = new Binding(iter.Item1 + ".m_Content")
                        };

                        this.DynamicParaSetGrid.Columns.Add(column);
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

                        this.DynamicParaSetGrid.Columns.Add(column);
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

                        this.DynamicParaSetGrid.Columns.Add(column);
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
