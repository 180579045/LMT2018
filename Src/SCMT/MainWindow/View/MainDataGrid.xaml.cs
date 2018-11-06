using SCMTMainWindow.Component.ViewModel;
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
                                <ComboBox ItemsSource='{Binding " + iter.Item1 + @".m_AllContent.Values}' SelectedIndex='0'/>
                             </DataTemplate>";

                        TextBlockTemplate = XamlReader.Parse(textblock_xaml) as DataTemplate;
                        ComboBoxTemplate = XamlReader.Parse(combobox_xaml) as DataTemplate;

                        column.Header = iter.Item2;                                      // 填写列名称;
                        column.CellTemplate = TextBlockTemplate;                         // 将单元格的显示形式赋值;
                        column.CellEditingTemplate = ComboBoxTemplate;                   // 将单元格的编辑形式赋值;
                        column.Width = 230;                                              // 设置显示宽度;

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

                        this.DynamicDataGrid.Columns.Add(column);
                    }
                    else
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 动态表构造函数;
        /// </summary>
        public MainDataGrid()
        {
            InitializeComponent();

            this.DynamicDataGrid.MouseMove += DynamicDataGrid_MouseMove;
            this.DynamicDataGrid.BeginningEdit += DynamicDataGrid_BeginningEdit;                  // 当表格发生正在编辑的状态;
            this.DynamicDataGrid.SelectionChanged += DynamicDataGrid_SelectionChanged;
            this.DynamicDataGrid.GotMouseCapture += DynamicDataGrid_GotMouseCapture;
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
            if (((e.OriginalSource as ComboBox).SelectedIndex == -1))
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
//                 ((sender as DataGrid).SelectedCells[0].Item as DyDataGrid_MIBModel).JudgePropertyName_ChangeSelection(
//                     (sender as DataGrid).SelectedCells[0].Column.Header.ToString());
            }
            catch
            {

            }

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
    }
}
