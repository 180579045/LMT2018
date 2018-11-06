using SCMTMainWindow.Component.ViewModel;
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
                    // 显示列表类型的数据结构;
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
                    // 如果单元格内类型是ComboBox类型;
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
                    else if (iter.Item3 is String)
                    {

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

            this.DynamicDataGrid.BeginningEdit += DynamicDataGrid_BeginningEdit;                  // 当表格发生正在编辑的状态;
            this.DynamicDataGrid.LostFocus += DynamicDataGrid_LostFocus;                          // 当表格失去焦点的时候;
            this.DynamicDataGrid.SelectionChanged += DynamicDataGrid_SelectionChanged;
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

        /// <summary>
        /// 单元格失去焦点之后;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
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
    }
}
