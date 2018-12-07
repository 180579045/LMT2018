using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SCMTMainWindow.View.Controls
{
    public class DragThumb : Thumb
    {
		private DesignerItem dragTarget;
        public DragThumb()
        {
			base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
        }

        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			DesignerItem designerItem = this.DataContext as DesignerItem;
			DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
			if (designerItem != null && designer != null && designerItem.IsSelected && dragTarget != null)
			{
				double minLeft = double.MaxValue;
				double minTop = double.MaxValue;

				double nleft = Canvas.GetLeft(designerItem);
				double ntop = Canvas.GetTop(designerItem);

				minLeft = double.IsNaN(nleft) ? 0 : Math.Min(nleft, minLeft);
				minTop = double.IsNaN(ntop) ? 0 : Math.Min(ntop, minTop);

				double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
				double deltaVertical = Math.Max(-minTop, e.VerticalChange);

				double left = Canvas.GetLeft(designerItem);
				double top = Canvas.GetTop(designerItem);

				if (double.IsNaN(left)) left = 0;
				if (double.IsNaN(top)) top = 0;

				Canvas.SetLeft(dragTarget, left + deltaHorizontal);
				Canvas.SetTop(dragTarget, top + deltaVertical);

                CaptureMouse();

                designer.InvalidateMeasure();
				e.Handled = true;
			}
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            if (e.ClickCount == 2)
            {
                return;
            }else if(e.ClickCount == 1)
            {
                DesignerItem designerItem = this.DataContext as DesignerItem;
                dragTarget = new DesignerItem();
                Grid gridContent = new Grid();
                gridContent.Background = new SolidColorBrush(Color.FromArgb(50, 105, 105, 105));
                dragTarget.Content = gridContent;
                dragTarget.Width = designerItem.Width;
                dragTarget.Height = designerItem.Height;
                DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
                if (designerItem != null && designer != null && designerItem.IsSelected)
                {
                    designer.Children.Add(dragTarget);
                    Canvas.SetLeft(dragTarget, Canvas.GetLeft(designerItem));
                    Canvas.SetTop(dragTarget, Canvas.GetTop(designerItem));
                }
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }
            DesignerItem designerItem = this.DataContext as DesignerItem;
			DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
			if (designerItem != null && designer != null && designerItem.IsSelected && dragTarget != null)
			{
				Canvas.SetLeft(designerItem, Canvas.GetLeft(dragTarget));
				Canvas.SetTop(designerItem, Canvas.GetTop(dragTarget));
				designer.Children.Remove(dragTarget);
			}
		}

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);

            DesignerItem designerItem = this.DataContext as DesignerItem;
            DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
            if (designerItem != null && designer != null && designerItem.IsSelected && dragTarget != null)
            {
                Canvas.SetLeft(designerItem, Canvas.GetLeft(dragTarget));
                Canvas.SetTop(designerItem, Canvas.GetTop(dragTarget));
                designer.Children.Remove(dragTarget);
            }
        }
    }
}
