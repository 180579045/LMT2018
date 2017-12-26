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
using Snmp_dll;
using Arthas.Controls.Metro;
using Arthas.Utility.Media;
using System.Reflection;

using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using LineChart;
using Specialized3DChart;

namespace SCMTMainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ChartStyle2D cs;
        private DataSeriesSurface ds;
        private Draw3DChart d3c;

        public MainWindow()
        {
            InitializeComponent();
            InitView();                                                       // 初始化界面;
            RegisterFunction();                                               // 注册功能;
            InitDemoData();
        }

        /// <summary>
        /// 初始化用户界面;
        /// Demo阶段，先假设只连接一个基站;
        /// </summary>
        private void InitView()
        {
            NodeB node = new NodeB("172.27.245.92");                          // 初始化一个基站节点(Demo程序,暂时只连接一个基站);
            ObjNodeControl Ctrl = new ObjNodeControl(node);                   // 初始化一个对象树;
            this.RefreshObj(Ctrl.m_RootNode);                                 // 将对象树加入到Expender容器中
            MetroExpander.NBContent_Grid = this.Content_Comm;                 // 将显示的容器加入到对象树属性中;
            MetroExpander.NBBase_Grid = this.Content_Base;                    // 将基站基本信息的容奇加入到对象树属性中;
        }

        /// <summary>
        /// 注册所有所需要的基础功能;
        /// </summary>
        private void RegisterFunction()
        {
            //TrapMessage.SetNodify(this.Update_NBInfoShow);                  // 注册Trap监听;
        }

        /// <summary>
        /// 更新对象树模型以及叶节点模型;
        /// </summary>
        /// <param name="ItemsSource">对象树列表</param>
        private void RefreshObj(IList<ObjNode> ItemsSource)
        {
            // 将右侧叶节点容器容器加入到对象树子容器中;
            this.Obj_Root.SubExpender = this.FavLeaf_Lists;

            foreach (ObjNode items in ItemsSource)
            {
                items.TraverseChildren(this.Obj_Root, this.FavLeaf_Lists, 0);
            }
        }
        
        //                                                    以下为Demo处理;
        // ------------------------------------------------------------------
        private void MetroExpander_Click_BaseInfo(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Visible;
            Content_Comm.Visibility = Visibility.Hidden;
        }

        private void UEList_Click(object sender, RoutedEventArgs e)
        {
            UEList UEWindow = new UEList();
            UEWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            UEWindow.Show();
        }

        private void InitDemoData()
        {
            // 添加Demo数据;
            ObservableCollection<AlarmGrid> custdata = AlarmGrid.GetData();   // 初始化基本信息中的告警信息;
            DG1.DataContext = custdata;                                       // 将告警信息加入控件;
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartCanvas.Width = chartGrid.ActualWidth;
            chartCanvas.Height = chartGrid.ActualHeight;
            AddChart();
        }

        private void AddChart()
        {
            chartCanvas.Children.Clear();
            cs = new ChartStyle2D();
            cs.ChartCanvas = this.chartCanvas;
            cs.GridlinePattern = Specialized3DChart.ChartStyle.GridlinePatternEnum.Solid;
            cs.IsColorBar = true;
            cs.Title = "No Title";
            ds = new DataSeriesSurface();
            ds.LineColor = Brushes.Black;
            Utility.Peak3D(cs, ds);

            d3c = new Draw3DChart();
            d3c.Colormap.ColormapBrushType = ColormapBrush.ColormapBrushEnum.Jet;
            d3c.ChartType = Draw3DChart.ChartTypeEnum.SurfaceFillContour3D;
            d3c.IsLineColorMatch = false;
            d3c.NumberContours = 15;
            d3c.AddChart(cs, ds);
        }

        private void SimFile1_Click(object sender, RoutedEventArgs e)
        {
            Simulation_Download Sd1 = new Simulation_Download();
            Sd1.Show();
        }

        private void DemoSCTF_Checked(object sender, RoutedEventArgs e)
        {
            Message_Setter Ms = new Message_Setter();
            Ms.Show();
        }

        private void Easy_NB_Click(object sender, EventArgs e)
        {
            this.FavLeaf_Lists.Children.Clear();
            MetroExpander Easy1 = new MetroExpander();
            MetroExpander Easy2 = new MetroExpander();
            MetroExpander Easy3 = new MetroExpander();
            MetroExpander Easy4 = new MetroExpander();
            MetroExpander Easy5 = new MetroExpander();
            MetroExpander Easy6 = new MetroExpander();

            Easy1.Header = "链路查询";
            Easy2.Header = "时钟查询";
            Easy3.Header = "LTE基带资源";
            Easy4.Header = "射频资源";
            Easy5.Header = "光模块";
            Easy6.Header = "LTE小区信息";

            this.FavLeaf_Lists.Children.Add(Easy1);
            this.FavLeaf_Lists.Children.Add(Easy2);
            this.FavLeaf_Lists.Children.Add(Easy3);
            this.FavLeaf_Lists.Children.Add(Easy4);
            this.FavLeaf_Lists.Children.Add(Easy5);
            this.FavLeaf_Lists.Children.Add(Easy6);

        }

        private void MainWindow_Cell_Click(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Hidden;
            Content_Comm.Visibility = Visibility.Visible;
        }
    }

   

}
