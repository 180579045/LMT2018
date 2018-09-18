using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SCMTMainWindow.View
{
    public class ConnectionAdorner : Adorner
    {
        private DesignerCanvas designerCanvas;
        private Canvas adornerCanvas;
        private Connection connection;
        private PathGeometry pathGeometry;
        private Connector fixConnector, dragConnector;
        private Thumb sourceDragThumb, sinkDragThumb;
        private Pen drawingPen;

        private DesignerItem hitDesignerItem;
        private DesignerItem HitDesignerItem
        {
            get { return hitDesignerItem; }
            set
            {
                if (hitDesignerItem != value)
                {
                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = false;

                    hitDesignerItem = value;

                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = true;
                }
            }
        }

        private Connector hitConnector;
        private Connector HitConnector
        {
            get { return hitConnector; }
            set
            {
                if (hitConnector != value)
                {
                    hitConnector = value;
                }
            }
        }

        private VisualCollection visualChildren;
        protected override int VisualChildrenCount
        {
            get
            {
                return this.visualChildren.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.visualChildren[index];
        }

        public ConnectionAdorner(DesignerCanvas designer, Connection connection)
            : base(designer)
        {
            this.designerCanvas = designer;
            adornerCanvas = new Canvas();
            this.visualChildren = new VisualCollection(this);
            this.visualChildren.Add(adornerCanvas);

            this.connection = connection;
            this.connection.PropertyChanged += new PropertyChangedEventHandler(AnchorPositionChanged);

            InitializeDragThumbs();

            drawingPen = new Pen(Brushes.LightSlateGray, 1);
            drawingPen.LineJoin = PenLineJoin.Round;

            base.Unloaded += new RoutedEventHandler(ConnectionAdorner_Unloaded);
        }
                

        void AnchorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("AnchorPositionSource"))
            {
                Canvas.SetLeft(sourceDragThumb, connection.AnchorPositionSource.X);
                Canvas.SetTop(sourceDragThumb, connection.AnchorPositionSource.Y);
            }

            if (e.PropertyName.Equals("AnchorPositionSink"))
            {
                Canvas.SetLeft(sinkDragThumb, connection.AnchorPositionSink.X);
                Canvas.SetTop(sinkDragThumb, connection.AnchorPositionSink.Y);
            }
        }

        void thumbDragThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (HitConnector != null)
            {
                if (connection != null)
                {
                    if (connection.Source == fixConnector)
                        connection.Sink = this.HitConnector;
                    else
                        connection.Source = this.HitConnector;
                }
            }

            this.HitDesignerItem = null;
            this.HitConnector = null;
            this.pathGeometry = null;
            this.connection.StrokeDashArray = null;
            this.InvalidateVisual();
        }

        void thumbDragThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.HitDesignerItem = null;
            this.HitConnector = null;
            this.pathGeometry = null;
            this.Cursor = Cursors.Cross;
            this.connection.StrokeDashArray = new DoubleCollection(new double[] { 1, 2 });

            if (sender == sourceDragThumb)
            {
                fixConnector = connection.Sink;
                dragConnector = connection.Source;
            }
            else if (sender == sinkDragThumb)
            {
                dragConnector = connection.Sink;
                fixConnector = connection.Source;
            }
        }

        void thumbDragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point currentPosition = Mouse.GetPosition(this);
            this.HitTesting(currentPosition);
            this.pathGeometry = UpdatePathGeometry(currentPosition);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawGeometry(null, drawingPen, this.pathGeometry);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            adornerCanvas.Arrange(new Rect(0, 0, this.designerCanvas.ActualWidth, this.designerCanvas.ActualHeight));
            return finalSize;
        }

        private void ConnectionAdorner_Unloaded(object sender, RoutedEventArgs e)
        {
            sourceDragThumb.DragDelta -= new DragDeltaEventHandler(thumbDragThumb_DragDelta);
            sourceDragThumb.DragStarted -= new DragStartedEventHandler(thumbDragThumb_DragStarted);
            sourceDragThumb.DragCompleted -= new DragCompletedEventHandler(thumbDragThumb_DragCompleted);

            sinkDragThumb.DragDelta -= new DragDeltaEventHandler(thumbDragThumb_DragDelta);
            sinkDragThumb.DragStarted -= new DragStartedEventHandler(thumbDragThumb_DragStarted);
            sinkDragThumb.DragCompleted -= new DragCompletedEventHandler(thumbDragThumb_DragCompleted);
        }

        private void InitializeDragThumbs()
        {
            Style dragThumbStyle = connection.FindResource("ConnectionAdornerThumbStyle") as Style;

            //source drag thumb
            sourceDragThumb = new Thumb();
            Canvas.SetLeft(sourceDragThumb, connection.AnchorPositionSource.X);
            Canvas.SetTop(sourceDragThumb, connection.AnchorPositionSource.Y);
            this.adornerCanvas.Children.Add(sourceDragThumb);
            if (dragThumbStyle != null)
                sourceDragThumb.Style = dragThumbStyle;

            sourceDragThumb.DragDelta += new DragDeltaEventHandler(thumbDragThumb_DragDelta);
            sourceDragThumb.DragStarted += new DragStartedEventHandler(thumbDragThumb_DragStarted);
            sourceDragThumb.DragCompleted += new DragCompletedEventHandler(thumbDragThumb_DragCompleted);

            // sink drag thumb
            sinkDragThumb = new Thumb();
            Canvas.SetLeft(sinkDragThumb, connection.AnchorPositionSink.X);
            Canvas.SetTop(sinkDragThumb, connection.AnchorPositionSink.Y);
            this.adornerCanvas.Children.Add(sinkDragThumb);
            if (dragThumbStyle != null)
                sinkDragThumb.Style = dragThumbStyle;

            sinkDragThumb.DragDelta += new DragDeltaEventHandler(thumbDragThumb_DragDelta);
            sinkDragThumb.DragStarted += new DragStartedEventHandler(thumbDragThumb_DragStarted);
            sinkDragThumb.DragCompleted += new DragCompletedEventHandler(thumbDragThumb_DragCompleted);
        }

        private PathGeometry UpdatePathGeometry(Point position)
        {
            PathGeometry geometry = new PathGeometry();

            ConnectorOrientation targetOrientation;
            if (HitConnector != null)
                targetOrientation = HitConnector.Orientation;
            else
                targetOrientation = dragConnector.Orientation;

            List<Point> linePoints = PathFinder.GetConnectionLine(fixConnector.GetInfo(), position, targetOrientation);

            if (linePoints.Count > 0)
            {
                PathFigure figure = new PathFigure(); figure.StartPoint = linePoints[0];
                //linePoints.Remove(linePoints[0]);

                int nCount = linePoints.Count;

                if (linePoints.Count >= 1)
                {

                    //贝塞尔曲线的两个控制点，需要根据不同的情况构造不同的控制点
                    Point ptCtrl1 = new Point();
                    Point ptCtrl2 = new Point();

                    //如果直线是水平的，贝塞尔曲线的控制点的选择
                    if (linePoints[0].Y == linePoints[linePoints.Count - 1].Y)
                    {
                        ptCtrl1.X = (linePoints[linePoints.Count - 1].X + linePoints[0].X) / 2;
                        ptCtrl1.Y = linePoints[0].Y - (Math.Abs(linePoints[linePoints.Count - 1].X - linePoints[0].X) / 6);

                        ptCtrl2.X = (linePoints[linePoints.Count - 1].X + linePoints[0].X) / 2;
                        ptCtrl2.Y = linePoints[0].Y + (Math.Abs(linePoints[linePoints.Count - 1].X - linePoints[0].X) / 6);

                        figure.Segments.Add(new BezierSegment(ptCtrl1, ptCtrl2, linePoints[linePoints.Count - 1], true));
                    }
                    else if (linePoints[0].X == linePoints[nCount - 1].X)
                    {
                        //如果连线是垂直的话，贝塞尔曲线的选择
                        ptCtrl1.X = linePoints[0].X + (Math.Abs(linePoints[nCount - 1].Y - linePoints[0].Y) / 6);
                        ptCtrl1.Y = (linePoints[0].Y + linePoints[nCount - 1].Y) / 2;

                        ptCtrl2.X = linePoints[0].X - (Math.Abs(linePoints[nCount - 1].Y - linePoints[0].Y) / 6);
                        ptCtrl2.Y = (linePoints[0].Y + linePoints[nCount - 1].Y) / 2;

                        figure.Segments.Add(new BezierSegment(ptCtrl1, ptCtrl2, linePoints[linePoints.Count - 1], true));

                    }
                    else
                    {
                        double yAbs = Math.Abs(linePoints[nCount - 1].Y - linePoints[0].Y);        //纵坐标的距离
                        double xAbs = Math.Abs(linePoints[nCount - 1].X - linePoints[0].X);        //横坐标的距离

                        //求斜率，或者 tan 的值，再根据 tan 求 正弦值
                        double arctanAerfa = Math.Atan(yAbs / xAbs);
                        double sinAerfa = Math.Sin(arctanAerfa);
                        double cosAerfa = Math.Cos(arctanAerfa);
                        //double atanaf = Math.Atan((linePoints[nCount - 1].Y - linePoints[0].Y) / (linePoints[nCount - 1].X - linePoints[0].X));
                        //double sinaf = Math.Sin(Math.Atan((linePoints[nCount - 1].Y - linePoints[0].Y) / (linePoints[nCount - 1].X - linePoints[0].X)));
                        //double cosaf = Math.Cos(Math.Asin(sinaf));

                        Point middlePoint = new Point((linePoints[nCount - 1].X + linePoints[0].X) / 2, (linePoints[nCount - 1].Y + linePoints[0].Y) / 2);

                        double d = Math.Pow((Math.Pow((linePoints[nCount - 1].Y - linePoints[0].Y), 2) + Math.Pow((linePoints[nCount - 1].X - linePoints[0].X), 2)), 0.5);

                        ptCtrl1.X = middlePoint.X + d * sinAerfa / 6;
                        ptCtrl1.Y = middlePoint.Y - d * cosAerfa / 6;

                        ptCtrl2.X = middlePoint.X - d * sinAerfa / 6;
                        ptCtrl2.Y = middlePoint.Y + d * cosAerfa / 6;

                        figure.Segments.Add(new BezierSegment(ptCtrl1, ptCtrl2, linePoints[linePoints.Count - 1], true));

                        //figure.Segments.Add(new BezierSegment(linePoints[1], linePoints[linePoints.Count - 2], linePoints[linePoints.Count - 1], true));

                    }//else if(linePoints[0].X == linePoints[nCount-1].X)
                     //{

                    //    ptCtrl1.X = linePoints[0].X - (Math.Abs(linePoints[linePoints.Count - 1].X - linePoints[0].X) / 6);
                    //    ptCtrl1.Y = (linePoints[linePoints.Count - 1].X + linePoints[0].X) / 2;

                    //    ptCtrl2.X = (linePoints[linePoints.Count - 1].X + linePoints[0].X) / 2;
                    //    ptCtrl2.Y = linePoints[0].Y + (Math.Abs(linePoints[linePoints.Count - 1].X - linePoints[0].X) / 6);

                    //    figure.Segments.Add(new BezierSegment(ptCtrl1, ptCtrl2, linePoints[linePoints.Count - 1], true));
                    //}
                    //else
                    //{
                    //    figure.Segments.Add(new BezierSegment(linePoints[1], linePoints[linePoints.Count - 2], linePoints[linePoints.Count - 1], true));

                    //}

                    //.......未完待续。。。还有3个象限和坐标轴，以及第一个坐标轴还未完成

                }
                //figure.Segments.Add(new PolyLineSegment(linePoints, true));
                geometry.Figures.Add(figure);
                //figure.StartPoint = linePoints[0];
                //linePoints.Remove(linePoints[0]);
                //figure.Segments.Add(new PolyLineSegment(linePoints, true));
                //geometry.Figures.Add(figure);
            }

            return geometry;
        }

        private void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;

            DependencyObject hitObject = designerCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                   hitObject != fixConnector.ParentDesignerItem &&
                   hitObject.GetType() != typeof(DesignerCanvas))
            {
                if (hitObject is Connector)
                {
                    HitConnector = hitObject as Connector;
                    hitConnectorFlag = true;
                }

                if (hitObject is DesignerItem)
                {
                    HitDesignerItem = hitObject as DesignerItem;
                    if (!hitConnectorFlag)
                        HitConnector = null;
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            HitConnector = null;
            HitDesignerItem = null;
        }

    }
}
