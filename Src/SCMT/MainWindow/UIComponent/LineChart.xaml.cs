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
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock;

namespace SCMTMainWindow.UIComponent
{
    /// <summary>
    /// LineChart.xaml 的交互逻辑
    /// </summary>
    public partial class LineChart : UserControl
    {
        public LayoutAnchorable linechart { get; set; }           // 折线图容器;

        public LineChart()
        {
            InitializeComponent();
            linechart = new LayoutAnchorable();
            linechart.CanAutoHide = false;
            linechart.CanTogglePin = false;
            linechart.CanHide = true;
            linechart.Title = "折线图";
            linechart.FloatingWidth = 300;
            linechart.FloatingHeight = 300;
            linechart.Content = new LineChartContent();
        }

        public void Float()
        {
            linechart.Float();
        }
    }
}
