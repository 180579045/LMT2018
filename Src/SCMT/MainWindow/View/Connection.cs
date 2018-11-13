using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SCMTMainWindow.View
{
    public class Connection : Control, ISelectable, INotifyPropertyChanged
    {
        private Adorner connectionAdorner;

        #region Properties

        public Guid ID { get; set; }

        // source connector
        private Connector source;
        public Connector Source
        {
            get
            {
                return source;
            }
            set
            {
                if(value == null)
                {
                    return;
                }

                if (source != value)
                {
                    if (source != null)
                    {
                        source.PropertyChanged -= new PropertyChangedEventHandler(OnConnectorPositionChanged);
                        source.Connections.Remove(this);
                    }

                    source = value;

                    if (source != null)
                    {
                        source.Connections.Add(this);
                        source.PropertyChanged += new PropertyChangedEventHandler(OnConnectorPositionChanged);
                    }

                    UpdatePathGeometry();
                }
            }
        }

        // sink connector
        private Connector sink;
        public Connector Sink
        {
            get { return sink; }
            set
            {
                if(value == null)
                {
                    return;
                }

                if (sink != value)
                {
                    if (sink != null)
                    {
                        sink.PropertyChanged -= new PropertyChangedEventHandler(OnConnectorPositionChanged);
                        sink.Connections.Remove(this);
                    }

                    sink = value;

                    if (sink != null)
                    {
                        sink.Connections.Add(this);
                        sink.PropertyChanged += new PropertyChangedEventHandler(OnConnectorPositionChanged);
                    }
                    UpdatePathGeometry();
                }
            }
        }

        // connection path geometry
        private PathGeometry pathGeometry;
        public PathGeometry PathGeometry
        {
            get { return pathGeometry; }
            set
            {
                if (pathGeometry != value)
                {
                    pathGeometry = value;
                    UpdateAnchorPosition();
                    OnPropertyChanged("PathGeometry");
                }
            }
        }

        // between source connector position and the beginning 
        // of the path geometry we leave some space for visual reasons; 
        // so the anchor position source really marks the beginning 
        // of the path geometry on the source side
        private Point anchorPositionSource;
        public Point AnchorPositionSource
        {
            get { return anchorPositionSource; }
            set
            {
                if (anchorPositionSource != value)
                {
                    anchorPositionSource = value;
                    OnPropertyChanged("AnchorPositionSource");
                }
            }
        }

        // slope of the path at the anchor position
        // needed for the rotation angle of the arrow
        private double anchorAngleSource = 0;
        public double AnchorAngleSource
        {
            get { return anchorAngleSource; }
            set
            {
                if (anchorAngleSource != value)
                {
                    anchorAngleSource = value;
                    OnPropertyChanged("AnchorAngleSource");
                }
            }
        }

        // analogue to source side
        private Point anchorPositionSink;
        public Point AnchorPositionSink
        {
            get { return anchorPositionSink; }
            set
            {
                if (anchorPositionSink != value)
                {
                    anchorPositionSink = value;
                    OnPropertyChanged("AnchorPositionSink");
                }
            }
        }
        // analogue to source side
        private double anchorAngleSink = 0;
        public double AnchorAngleSink
        {
            get { return anchorAngleSink; }
            set
            {
                if (anchorAngleSink != value)
                {
                    anchorAngleSink = value;
                    OnPropertyChanged("AnchorAngleSink");
                }
            }
        }

        private ArrowSymbol sourceArrowSymbol = ArrowSymbol.None;
        public ArrowSymbol SourceArrowSymbol
        {
            get { return sourceArrowSymbol; }
            set
            {
                if (sourceArrowSymbol != value)
                {
                    sourceArrowSymbol = value;
                    OnPropertyChanged("SourceArrowSymbol");
                }
            }
        }

        public ArrowSymbol sinkArrowSymbol = ArrowSymbol.Arrow;
        public ArrowSymbol SinkArrowSymbol
        {
            get { return sinkArrowSymbol; }
            set
            {
                if (sinkArrowSymbol != value)
                {
                    sinkArrowSymbol = value;
                    OnPropertyChanged("SinkArrowSymbol");
                }
            }
        }

        // specifies a point at half path length
        private Point labelPosition;
        public Point LabelPosition
        {
            get { return labelPosition; }
            set
            {
                if (labelPosition != value)
                {
                    labelPosition = value;
                    OnPropertyChanged("LabelPosition");
                }
            }
        }

        // pattern of dashes and gaps that is used to outline the connection path
        private DoubleCollection strokeDashArray;
        public DoubleCollection StrokeDashArray
        {
            get
            {
                return strokeDashArray;
            }
            set
            {
                if (strokeDashArray != value)
                {
                    strokeDashArray = value;
                    OnPropertyChanged("StrokeDashArray");
                }
            }
        }
        // if connected, the ConnectionAdorner becomes visible
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                    if (isSelected)
                        ShowAdorner();
                    else
                        HideAdorner();
                }
            }
        }

        #endregion

        public Connection(Connector source, Connector sink)
        {
            this.ID = Guid.NewGuid();
            this.Source = source;
            this.Sink = sink;
            base.Unloaded += new RoutedEventHandler(Connection_Unloaded);
        }


        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // usual selection business
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                    }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                }

                //this.Focus();
            }
            e.Handled = false;
        }

        void OnConnectorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            // whenever the 'Position' property of the source or sink Connector 
            // changes we must update the connection path geometry
            if (e.PropertyName.Equals("Position"))
            {
                UpdatePathGeometry();
            }
        }

        private void UpdatePathGeometry()
        {
            if (Source != null && Sink != null)
            {
                PathGeometry geometry = new PathGeometry();

                //获取一些点，大概是  源和 目的 之间的点
                List<Point> linePoints = PathFinder.GetConnectionLine(Source.GetInfo(), Sink.GetInfo(), true);
                if (linePoints.Count > 0)
                {
                    PathFigure figure = new PathFigure();
                    figure.StartPoint = linePoints[0];
                    //linePoints.Remove(linePoints[0]);

                    int nCount = linePoints.Count;

                    if(linePoints.Count >= 1)
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
                        }else if(linePoints[0].X == linePoints[nCount-1].X)
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

                    this.PathGeometry = geometry;
                }
            }
        }

        private void UpdateAnchorPosition()
        {
            Point pathStartPoint, pathTangentAtStartPoint;
            Point pathEndPoint, pathTangentAtEndPoint;
            Point pathMidPoint, pathTangentAtMidPoint;

            // the PathGeometry.GetPointAtFractionLength method gets the point and a tangent vector 
            // on PathGeometry at the specified fraction of its length
            this.PathGeometry.GetPointAtFractionLength(0, out pathStartPoint, out pathTangentAtStartPoint);
            this.PathGeometry.GetPointAtFractionLength(1, out pathEndPoint, out pathTangentAtEndPoint);
            this.PathGeometry.GetPointAtFractionLength(0.5, out pathMidPoint, out pathTangentAtMidPoint);

            // get angle from tangent vector
            this.AnchorAngleSource = Math.Atan2(-pathTangentAtStartPoint.Y, -pathTangentAtStartPoint.X) * (180 / Math.PI);
            this.AnchorAngleSink = Math.Atan2(pathTangentAtEndPoint.Y, pathTangentAtEndPoint.X) * (180 / Math.PI);

            // add some margin on source and sink side for visual reasons only
            pathStartPoint.Offset(-pathTangentAtStartPoint.X * 5, -pathTangentAtStartPoint.Y * 5);
            pathEndPoint.Offset(pathTangentAtEndPoint.X * 5, pathTangentAtEndPoint.Y * 5);

            this.AnchorPositionSource = pathStartPoint;
            this.AnchorPositionSink = pathEndPoint;
            this.LabelPosition = pathMidPoint;
        }

        private void ShowAdorner()
        {
            // the ConnectionAdorner is created once for each Connection
            if (this.connectionAdorner == null)
            {
                DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    this.connectionAdorner = new ConnectionAdorner(designer, this);
                    adornerLayer.Add(this.connectionAdorner);
                }
            }
            this.connectionAdorner.Visibility = Visibility.Visible;
        }

        internal void HideAdorner()
        {
            if (this.connectionAdorner != null)
                this.connectionAdorner.Visibility = Visibility.Collapsed;
        }

        void Connection_Unloaded(object sender, RoutedEventArgs e)
        {
            // do some housekeeping when Connection is unloaded

            // remove event handler
            this.Source = null;
            this.Sink = null;

            // remove adorner
            if (this.connectionAdorner != null)
            {
                DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.connectionAdorner);
                    this.connectionAdorner = null;
                }
            }
        }

        #region INotifyPropertyChanged Members

        // we could use DependencyProperties as well to inform others of property changes
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }

    public enum ArrowSymbol
    {
        None,
        Arrow,
        Diamond
    }
}
