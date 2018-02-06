using Arthas.Controls.Metro;
using System;
using System.Windows;
using System.Windows.Media;
using LineChart;
using System.Threading;
using System.Collections.Generic;
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace SCMTMainWindow
{
    delegate void GenerateNum(int y);
    /// <summary>
    /// UEList.xaml 的交互逻辑;
    /// 此处均为Demo数据专门编写
    /// </summary>
    public partial class UEList : MetroWindow
    {
        // 图一的模拟数据;
        private ChartStyleGridlines cs;            // 表格;
        private DataCollection dc;                 // 折线图数据集;
        private DataSeries ds;                     // 折线图;
        private int x1 = 0;

        // 图二的模拟数据;
        private ChartStyleGridlines cs2;
        private DataSeries ds2;
        private int x2 = 0;

        // 图三的模拟数据;
        private ChartStyleGridlines cs3;
        private DataSeries ds3;
        

        public UEList()
        {
            InitializeComponent();
            AddCellBaseinfo();

            ObservableCollection<NodeBUser> custdata = GetData();
            //Bind the DataGrid to the customer data
            UEListGrid.DataContext = custdata;

            this.grid2.AllowDrop = true;
            grid2.PreviewMouseMove += new MouseEventHandler(grid2Move);
        }

        private void grid2Move(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Console.WriteLine("123321123321");
            }
        }

        private ObservableCollection<NodeBUser> GetData()
        {
            ObservableCollection<NodeBUser> ret = new ObservableCollection<NodeBUser>();
            ret.Add(new NodeBUser("1", "2", "3"));
            ret.Add(new NodeBUser("1", "2", "3"));
            ret.Add(new NodeBUser("1", "2", "3"));
            ret.Add(new NodeBUser("1", "2", "3"));
            ret.Add(new NodeBUser("1", "2", "3"));
            ret.Add(new NodeBUser("1", "2", "3"));


            return ret; 
        }

        /// <summary>
        /// 绘制折线图并开启模拟数据线程;
        /// </summary>
        private void AddChart()
        {
            DrawMainGraph1();                                 // 主界面图1;
            DrawMainGraph2();                                 // 主界面图2;
            DrawMainGraph3();                                 // 主界面图3;

            Thread DrawPic1 = new Thread(CreatePoint);        // 启动一个线程，每隔2秒返回一个点;
            DrawPic1.Start();

            Thread WriteMessage = new Thread(CreateMsg);
            WriteMessage.Start();
        }

        /// <summary>
        /// 图一初始化;
        /// </summary>
        private void DrawMainGraph1()
        {
            cs = new ChartStyleGridlines();
            dc = new DataCollection();
            ds = new DataSeries();

            // 设置画布;
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "BLER"; 
            cs.Xmin = 0;
            cs.Xmax = 100;
            cs.Ymin = 0;
            cs.Ymax = 5;
            cs.YTick = 1;

            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // 绘制图形;
            ds.LineColor = Brushes.Red;
            ds.LineThickness = 1;

        }

        /// <summary>
        /// 图二初始化;
        /// </summary>
        private void DrawMainGraph2()
        {
            cs2 = new ChartStyleGridlines();
            ds2 = new DataSeries();

            // 设置画布;
            cs2.ChartCanvas = chartCanvas1;
            cs2.TextCanvas = textCanvas1;
            cs2.Title = "BLER2";
            cs2.Xmin = 0;
            cs2.Xmax = 50;
            cs2.Ymin = 0;
            cs2.Ymax = 5;
            cs2.YTick = 1;

            cs2.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs2.GridlineColor = Brushes.Black;
            cs2.AddChartStyle(tbTitle1, tbXLabel1, tbYLabel1);

            // 绘制图形;
            ds2.LineColor = Brushes.Blue;
            ds2.LineThickness = 1;

        }

        /// <summary>
        /// 图三初始化;
        /// </summary>
        private void DrawMainGraph3()
        {
            cs3 = new ChartStyleGridlines();
            ds3 = new DataSeries();

            // 设置画布;
            cs3.ChartCanvas = chartCanvas2;
            cs3.TextCanvas = textCanvas2;
            cs3.Title = "BLER";
            cs3.Xmin = 0;
            cs3.Xmax = 100;
            cs3.Ymin = 0;
            cs3.Ymax = 5;
            cs3.YTick = 1;

            cs3.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs3.GridlineColor = Brushes.Black;
            cs3.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // 绘制图形;
            ds3.LineColor = Brushes.Black;
            ds3.LinePattern = DataSeries.LinePatternEnum.Solid;
            ds3.LineThickness = 3;

        }

        /// <summary>
        /// 添加小区基本信息的模拟数据;
        /// </summary>
        private void AddCellBaseinfo()
        {
            List<UECellBaseContent> CellBaseCnt = new List<UECellBaseContent>();

            CellBaseCnt.Add(new UECellBaseContent("小区制式", "5G II"));
            CellBaseCnt.Add(new UECellBaseContent("小区频点", "18400"));
            CellBaseCnt.Add(new UECellBaseContent("小区用户数", "3"));
            CellBaseCnt.Add(new UECellBaseContent("小区激活状态", "激活"));

            UECellBaseContent abc = new UECellBaseContent("111", "222");

            this.CellBaseInfo.ItemsSource = CellBaseCnt;
            //this.CellBaseInfo.Items.Add(abc);
        }

        /// <summary>
        /// 向消息列表控件添加滚动消息;
        /// </summary>
        private void AddMsgRoll()
        {
            this.UEMessageGrid.Dispatcher.Invoke(new Action(
                delegate
                {
                    this.UEMessageGrid.Items.Add(new UEMessage(DateTime.Now.ToString(), "Message" + x1));
                }
                ));
        }
        
        /// <summary>
        /// 将返回的数值回填到折线图空间中;
        /// </summary>
        /// <param name="y"></param>
        /// <param name="y2"></param>
        private void drawcallback(int y, int y2)
        {
            x1++;
            x2++;

            Dispatcher.BeginInvoke(new Action(delegate
            {
                ds.LineSeries.Points.Add(new Point(x1 * 3, y));
                dc.DataList.Clear();
                dc.DataList.Add(ds);
                dc.AddPoint(cs, ds);

                ds2.LineSeries.Points.Add(new Point(x2 * 3, y2));
                dc.DataList.Clear();
                dc.DataList.Add(ds2);
                dc.AddPoint(cs2, ds2);

                ds3.LineSeries.Points.Add(new Point(x2, y2));
                dc.DataList.Clear();
                dc.DataList.Add(ds3);
                dc.AddPoint(cs3, ds3);
            }));

        }

        /// <summary>
        /// 子线程，每隔2s给主线程反馈一个数字;
        /// </summary>
        private void CreatePoint()
        {
            Random rd = new Random();
            Random rd2 = new Random();
            while (true)
            {
                drawcallback(rd.Next(0,50), rd2.Next(0,20));
                Thread.Sleep(5000);
            }
        }

        private void CreateMsg()
        {
            while(true)
            {
                AddMsgRoll();
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// 折线图初始化函数;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);

            textCanvas1.Width = chartGrid.ActualWidth;
            textCanvas1.Height = chartGrid.ActualHeight;
            chartCanvas1.Children.Clear();
            textCanvas1.Children.RemoveRange(1, textCanvas1.Children.Count - 1);

            textCanvas2.Width = chartGrid.ActualWidth;
            textCanvas2.Height = chartGrid.ActualHeight;
            chartCanvas2.Children.Clear();
            textCanvas2.Children.RemoveRange(1, textCanvas2.Children.Count - 1);

            AddChart();
        }

        private void ShowUEGraphic_Click(object sender, RoutedEventArgs e)
        {
            DataGraphic_Selected Gs = new DataGraphic_Selected();
            Gs.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Gs.WindowState = System.Windows.WindowState.Maximized;
            Gs.Show();
        }
        

        private void UEGraphSetting_Click(object sender, RoutedEventArgs e)
        {
            UEList_Setting a = new UEList_Setting();
            a.m_PassToMain += A_m_PassToMain;
            a.Show();
        }

        private void A_m_PassToMain(object sender, GraphSelectorPassValEventArg e)
        {
            this.UEtab.Items.Add(e.m_tab);
        }

        private void MetroMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Excel Files (*.sql)|*.sql"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                
            }
        }

        private void grid2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {

            }
        }
    }


    class UECellBaseContent
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public UECellBaseContent(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }

    public class NodeBUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsMember { get; set; }

        public NodeBUser(string name, string name2, string name3)
        {
            FirstName = name;
            LastName = name2;
            IsMember = name3;
        }

    }

}
