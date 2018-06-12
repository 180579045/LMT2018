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
using System.Windows.Shapes;
using System.Threading;

namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
    /// <summary>
    /// TestTwoFileManager.xaml 的交互逻辑
    /// </summary>
    public partial class TestTwoFileManager : UserControl
    {
        //静态成员 ListView  方便外部进行访问修改
        public static ListView lvMainListView = new ListView();
        public static ProcessList myList = new ProcessList();

        public TestTwoFileManager()
        {
            InitializeComponent();
            //添加  ListView   作为静态成员，只能使用后台代码添加，原谅我不会用xaml添加静态。。。^_^
            InitListView();
        }


        /// <summary>
        /// 初始化  ListView    
        /// </summary>
        private void InitListView()
        {
            //首先添加到  主界面  Grid  中
            MainGrid.Children.Add(lvMainListView);
            Grid.SetRow(lvMainListView, 2);

            //添加  字段名称
            GridView gvListView = new GridView();
            lvMainListView.View = gvListView;

            GridViewColumn gvcColumn = new GridViewColumn();
            gvcColumn.Header = "文件名称";
            gvcColumn.Width = 100;
            gvcColumn.DisplayMemberBinding = new Binding("FileName");
            gvListView.Columns.Add(gvcColumn);

            gvcColumn = new GridViewColumn();
            gvcColumn.Header = "进度";
            gvcColumn.Width = 120;

            DataTemplate template = new DataTemplate();

            //进度条显示文字，目前想到的方案是，定义Grid，添加TextBlock显示，和进度条组合起来
            FrameworkElementFactory gridTxtAndProcess = new FrameworkElementFactory(typeof(Grid));
            FrameworkElementFactory processText = new FrameworkElementFactory(typeof(TextBlock));
            FrameworkElementFactory fileProgress = new FrameworkElementFactory(typeof(ProgressBar));
            //ProgressBar fileProgress = new ProgressBar();
            fileProgress.SetValue(ProgressBar.MaximumProperty, 100.0);
            fileProgress.SetValue(ProgressBar.WidthProperty, 100.0);
            fileProgress.SetValue(ProgressBar.HeightProperty, 15.0);
            fileProgress.SetBinding(ProgressBar.ValueProperty, new Binding("ProgressValue"));

            //设置TextBlock信息
            processText.SetValue(TextBlock.WidthProperty, 33.0);    //显示百分比够了
            processText.SetValue(TextBlock.HeightProperty, 15.0);     //大概是进度的宽度
            processText.SetBinding(TextBlock.TextProperty, new Binding("TextBloxkValue"));

            //Grid添加子项，包含TextBlock和Processbar
            gridTxtAndProcess.AppendChild(fileProgress);
            gridTxtAndProcess.AppendChild(processText);

            template.VisualTree = gridTxtAndProcess;
            gvcColumn.CellTemplate = template;
            gvListView.Columns.Add(gvcColumn);

            gvcColumn = new GridViewColumn();
            gvcColumn.Header = "文件状态";
            gvcColumn.Width = 100;
            gvListView.Columns.Add(gvcColumn);

            gvcColumn = new GridViewColumn();
            gvcColumn.Header = "操作类型";
            gvcColumn.Width = 100;
            gvListView.Columns.Add(gvcColumn);
        }

    }
}
