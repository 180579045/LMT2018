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

namespace DT.Tools.FlowChart
{
    /// <summary>
    /// DynamicChartCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class DynamicChartCanvas : UserControl
    {
        private readonly List<Visual> visuals = new List<Visual>();
        private double canvasHeight =270;
        private double CanvasWith=750;
        private double mark = 15;

        public DynamicChartCanvas()
        {
            InitializeComponent();
        }
        public bool drawingDynamicChart() {
            //Y轴
            var drawingVisualY = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisualY.RenderOpen()) {
                var selectedPen = new Pen(Brushes.Black,2);
                var startPoint = new Point(40,5);
                var endPoint = new Point(40,270);
                drawingContext.DrawLine(selectedPen, startPoint, endPoint);
            }
            //X轴
            var drawingVisualX = new DrawingVisual();
            using (DrawingContext drawingContextX = drawingVisualX.RenderOpen()) {

                var selectedPen = new Pen(Brushes.Black, 2);
                var startPoint = new Point(0, 0);
                var endPoint = new Point(750, 0);
                drawingContextX.DrawLine(selectedPen, startPoint, endPoint);
            }
            //中心画布
            /*
            var drawingVisualCenter = new DrawingVisual();
            using (DrawingContext drawingContextCenter =drawingVisualCenter.RenderOpen()) {
                for (int i = 0; i < (CanvasWith / mark); i++) {

                }
            }*/
            this.xAxis.AddVisual(drawingVisualY);
            this.yAxis.AddVisual(drawingVisualX);
            return true;
        }

    }
}
