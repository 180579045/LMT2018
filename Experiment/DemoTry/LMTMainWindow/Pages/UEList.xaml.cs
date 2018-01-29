using Arthas.Controls.Metro;
using System;
using System.Windows;
using System.Windows.Media;
using LineChart;
using System.Threading;

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
        private ChartStyleGridlines cs;
        private DataCollection dc;
        private DataSeries ds;
        private int x1 = 0;

        // 图二的模拟数据;
        private ChartStyleGridlines cs2;
        private DataCollection dc2;
        private DataSeries ds2;
        private int x2 = 0;

        public UEList()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        /// <summary>
        /// 模拟数据;
        /// </summary>
        private void AddChart()
        {
            DrawMainGraph1();                                 // 主界面图1;
            DrawMainGraph2();                                 // 主界面图1;

            // 启动一个线程，每隔2秒返回一个点;
            Thread DrawPic1 = new Thread(CreatePoint);
            DrawPic1.Start();
        }

        private void drawcallback(int y, int y2)
        {
            x1++;
            x2++;

            Dispatcher.BeginInvoke(new Action(delegate
            {
                ds.LineSeries.Points.Add(new Point(x1 * 3, y));
                dc.DataList.Add(ds);
                dc.AddPoint(cs, ds);

                ds2.LineSeries.Points.Add(new Point(x2 * 3, y2));
                dc2.DataList.Add(ds2);
                dc2.AddPoint(cs2, ds2);
            }));

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
            dc2 = new DataCollection();
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
        /// 子线程，每隔2s给主线程反馈一个数字;
        /// </summary>
        private void CreatePoint()
        {
            Random rd = new Random();
            Random rd2 = new Random();
            while (true)
            {
                drawcallback(rd.Next(0,50), rd2.Next(0,20));
                Thread.Sleep(500);
            }
        }

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

            AddChart();
        }

        private void ShowUEGraphic_Click(object sender, RoutedEventArgs e)
        {
            DataGraphic_Selected Gs = new DataGraphic_Selected();
            Gs.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Gs.WindowState = System.Windows.WindowState.Maximized;
            Gs.Show();
        }
    }
}
