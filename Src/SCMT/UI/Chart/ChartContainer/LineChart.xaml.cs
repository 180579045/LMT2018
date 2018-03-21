using System;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock;

namespace ChartContainer
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
            linechart.CanHide = false;
            linechart.Title = "折线图";
            linechart.IsVisible = true;
        }

        /// <summary>
        /// 弹出;
        /// </summary>
        public void Flot()
        {
            linechart.Float();
        }
    }
}
