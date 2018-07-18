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
using CefSharp;
using CefSharp.Wpf;
using Xceed.Wpf.AvalonDock.Layout;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// HeatMapChartContent.xaml 的交互逻辑
    /// </summary>
    public partial class HeatMapChartContent : UserControl
    {
        private bool GettingValue = false;

        public HeatMapChartContent()
        {
            InitializeComponent();
            this.heatMapAddress.Address = System.Environment.CurrentDirectory + @"\HeatMapChart_JS\heatmap-cartesian.html";
            this.heatMapAddress.BeginInit();                                  // 刷新页面;
        }
        public void Sub_Closed(object sender, EventArgs e)
        {
            GettingValue = false;
        }

        private void Button_ClickHeatMap(object sender, RoutedEventArgs e)
        {
            this.heatMapAddress.ShowDevTools();
        }
    }
}
