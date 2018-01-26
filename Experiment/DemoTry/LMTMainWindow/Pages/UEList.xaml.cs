using Arthas.Controls.Metro;
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
using LineChart;

namespace SCMTMainWindow
{
    /// <summary>
    /// UEList.xaml 的交互逻辑
    /// </summary>
    public partial class UEList : MetroWindow
    {
        private ChartStyleGridlines cs;
        private DataCollection dc;
        private DataSeries ds;

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
            DrawMainGraph1();            // 主界面图1;
        }

        private void DrawMainGraph1()
        {
            cs = new ChartStyleGridlines();
            dc = new DataCollection();
            ds = new DataSeries();

            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "BLER";
            cs.Xmin = 0;
            cs.Xmax = 50;
            cs.Ymin = -1.5;
            cs.Ymax = 1.5;
            cs.YTick = 0.5;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = Brushes.Black;
            cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

            // Draw Sine curve:
            ds.LineColor = Brushes.Blue;
            ds.LineThickness = 2;
            for (int i = 0; i < 200; i++)
            {
                double x = i / 9.0;
                double y = Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            // Draw cosine curve:
            ds = new DataSeries();
            ds.LineColor = Brushes.Red;
            ds.LinePattern = DataSeries.LinePatternEnum.DashDot;
            ds.LineThickness = 2;

            for (int i = 0; i < 70; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);
            dc.AddLines(cs);
        }

        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart();
        }

        private void ShowUEGraphic_Click(object sender, RoutedEventArgs e)
        {
            DataGraphic_Selected Gs = new DataGraphic_Selected();
            Gs.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Gs.Show();
        }
    }
}
