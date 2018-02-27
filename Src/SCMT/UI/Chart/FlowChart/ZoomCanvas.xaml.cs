// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZoomCanvas.xaml.cs" company="dtmobile">
//   dtmobile
// </copyright>
// <summary>
//   UserControl1.xaml 的交互逻辑
// </summary>
// --------------------------------------------------------------------------------------------------------------------

/************************************************************************/
/* 欢迎指正与交流
 *作者：卓然糊涂
 *日期：2011-12-23
 *邮箱qzhuoran@gmail.com   
 * 
/************************************************************************/
namespace DT.Tools.FlowChart
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media;

    using Control = System.Windows.Forms.Control;

    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class ZoomCanvas
    {
        /// <summary>
        /// The visuals.
        /// </summary>
        private readonly List<Visual> visuals = new List<Visual>();

        /// <summary>
        /// The mymat.
        /// </summary>
        private Matrix mymat;

        /// <summary>
        /// The startpoint.
        /// </summary>
        private Point startpoint;

        /// <summary>
        /// The currentpoint.
        /// </summary>
        private Point currentpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomCanvas"/> class.
        /// </summary>
        public ZoomCanvas()
        {
            this.InitializeComponent();
            this.MouseLeftButtonDown += this.CanvasMouseLeftButtonDown1;
            this.MouseWheel += this.CanvasMouseWheel;
            this.MouseLeftButtonDown += this.CanvasMouseMove;
            this.mymat = new Matrix(1, 0, 0, 1, 0, 0); // 存储当前控件位移和比例
        }

        /// <summary>
        /// Gets the visual children count.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        /// <summary>
        /// The reset position.
        /// </summary>
        public void ResetPosition()
        {
            this.MatrixChange(0, 0, 1);
        }

        /// <summary>
        /// The get visual.
        /// </summary>
        /// <param name="point">
        /// The point.
        /// </param>
        /// <returns>
        /// The <see cref="DrawingVisual"/>.
        /// </returns>
        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            return hitResult.VisualHit as DrawingVisual;
        }

        /// <summary>
        /// The add visual.
        /// </summary>
        /// <param name="visual">
        /// The visual.
        /// </param>
        public void AddVisual(Visual visual)
        {
            this.visuals.Add(visual);

            this.AddVisualChild(visual);
            this.AddLogicalChild(visual);
        }

        /// <summary>
        /// The delete visual.
        /// </summary>
        /// <param name="visual">
        /// The visual.
        /// </param>
        public void DeleteVisual(Visual visual)
        {
            this.visuals.Remove(visual);
            this.RemoveVisualChild(visual);
            this.RemoveLogicalChild(visual);
        }

        /// <summary>
        /// The clear all visuals.
        /// </summary>
        public void ClearAllVisuals()
        {
            foreach (var visual in this.visuals)
            {
                this.RemoveVisualChild(visual);
                this.RemoveLogicalChild(visual);
            }

            this.visuals.Clear();
        }

        /// <summary>
        /// The get visual child.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="Visual"/>.
        /// </returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }

        /// <summary>
        /// The canvas_ mouse left button down_1.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CanvasMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            this.startpoint = e.GetPosition((FrameworkElement)this.Parent); // 记录开始位置
            this.currentpoint.X = this.mymat.OffsetX; // 记录Canvas当前位移
            this.currentpoint.Y = this.mymat.OffsetY;
        }

        /// <summary>
        /// The matrix change.
        /// </summary>
        /// <param name="dx">
        /// The dx.
        /// </param>
        /// <param name="dy">
        /// The dy.
        /// </param>
        /// <param name="scale">
        /// The scale.
        /// </param>
        private void MatrixChange(double dx, double dy, double scale)
        {
            this.mymat.M11 = scale;
            this.mymat.M22 = scale;
            this.mymat.OffsetX = dx;
            this.mymat.OffsetY = dy;
            this.RenderTransform = new MatrixTransform(this.mymat);
        }

        /// <summary>
        /// The matrix change.
        /// </summary>
        /// <param name="dx">
        /// The dx.
        /// </param>
        /// <param name="dy">
        /// The dy.
        /// </param>
        private void MatrixChange(double dx, double dy)
        {
            this.mymat.OffsetX = dx;
            this.mymat.OffsetY = dy;
            this.RenderTransform = new MatrixTransform(this.mymat);
        }

        /// <summary>
        /// The canvas_ mouse wheel.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CanvasMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                return;
            }

            Point p1 = e.GetPosition(this); // 得当鼠标相对于控件的坐标
            double dx, dy;
            double scale = this.mymat.M11;
            if (e.Delta > 0)
            {
                scale += 0.2;
                if (scale > 4)
                {
                    return;
                }

                dx = (p1.X * (scale - 0.2)) - (scale * p1.X) + this.mymat.OffsetX;
                dy = (p1.Y * (scale - 0.2)) - (scale * p1.Y) + this.mymat.OffsetY; // 放大本质是 移动和缩放两个步骤 

                this.MatrixChange(dx, dy, scale);
            }
            else if (e.Delta <= 0)
            {
                /*缩小*/
                scale -= 0.2;
                if (scale < 0.5)
                {
                    return;
                }

                dx = (p1.X * (scale + 0.2)) - (scale * p1.X) + this.mymat.OffsetX;
                dy = (p1.Y * (scale + 0.2)) - (scale * p1.Y) + this.mymat.OffsetY;
                this.MatrixChange(dx, dy, scale);
            }
        }

        /// <summary>
        /// The canvas mouse move.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CanvasMouseMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currp = e.GetPosition((FrameworkElement)this.Parent);
                double dx = currp.X - this.startpoint.X + this.currentpoint.X;
                double dy = currp.Y - this.startpoint.Y + this.currentpoint.Y; // 总位移等于当前的位移加上已有的位移
                this.MatrixChange(dx, dy); // 移动控件，并更新总位移
            }
        }
    }
}
