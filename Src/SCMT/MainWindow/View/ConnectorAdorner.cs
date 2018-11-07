using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System;

using NetPlan;

namespace SCMTMainWindow.View
{
    public class ConnectorAdorner : Adorner
    {
        private PathGeometry pathGeometry;
        private DesignerCanvas designerCanvas;
        private Connector sourceConnector;
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

        public ConnectorAdorner(DesignerCanvas designer, Connector sourceConnector)
            : base(designer)
        {
            this.designerCanvas = designer;
            this.sourceConnector = sourceConnector;
            drawingPen = new Pen(Brushes.LightSlateGray, 1);
            drawingPen.LineJoin = PenLineJoin.Round;
            this.Cursor = Cursors.Cross;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (HitConnector != null)
            {
                Connector sourceConnector = this.sourceConnector;
                Connector sinkConnector = this.HitConnector;
                Connection newConnection = new Connection(sourceConnector, sinkConnector);

                //这里是创建连接的地方，需要下发给基站

                LinkEndpoint srcPoint = new LinkEndpoint();
                srcPoint.devType = sourceConnector.DevType;
                srcPoint.strDevIndex = sourceConnector.DevIndex;
                srcPoint.nPortNo = sourceConnector.PortNo;

                LinkEndpoint dstPoint = new LinkEndpoint();
                dstPoint.devType = sinkConnector.DevType;
                dstPoint.strDevIndex = sinkConnector.DevIndex;
                dstPoint.nPortNo = sinkConnector.PortNo;
                if (sourceConnector.PortType == EnumPortType.bbu_to_other)
                {
                    if(sinkConnector.DevType == EnumDevType.rru)
                    {
                        srcPoint.portType = EnumPortType.bbu_to_rru;
                        dstPoint.portType = EnumPortType.rru_to_bbu;
                    }else if(sinkConnector.DevType == EnumDevType.rhub)
                    {
                        srcPoint.portType = EnumPortType.bbu_to_rhub;
                        dstPoint.portType = EnumPortType.rhub_to_bbu;
                    }
                }else if(sourceConnector.PortType == EnumPortType.rru_to_other)
                {
                    if(sinkConnector.DevType == EnumDevType.board)
                    {
                        srcPoint.portType = EnumPortType.rru_to_bbu;
                        dstPoint.portType = EnumPortType.bbu_to_rru;
                    }else if(sinkConnector.DevType == EnumDevType.rru)
                    {
                        srcPoint.portType = EnumPortType.rru_to_rru;
                        dstPoint.portType = EnumPortType.rru_to_rru;
                    }
                }else if(sourceConnector.PortType == EnumPortType.rhub_to_other)
                {
                    if(sinkConnector.DevType == EnumDevType.board)
                    {
                        dstPoint.portType = EnumPortType.bbu_to_rhub;
                        srcPoint.portType = EnumPortType.rhub_to_bbu;
                    }else if(sinkConnector.DevType == EnumDevType.rhub)
                    {
                        dstPoint.portType = EnumPortType.rhub_to_rhub;
                        srcPoint.portType = EnumPortType.rhub_to_rhub;
                    }
                }
                else
                {
                    srcPoint.portType = sourceConnector.PortType;
                    dstPoint.portType = sinkConnector.PortType;
                }

                if (MibInfoMgr.GetInstance().AddLink(srcPoint, dstPoint))
                {
                    Canvas.SetZIndex(newConnection, designerCanvas.Children.Count);
                    this.designerCanvas.Children.Add(newConnection);

                    //将 to_other 的连接点设置为 确定的连接
                    sourceConnector.PortType = srcPoint.portType;
                    sinkConnector.PortType = dstPoint.portType;
                }
                else
                {
                    MessageBox.Show("AddLink 失败");
                }
                
            }
            if (HitDesignerItem != null)
            {
                this.HitDesignerItem.IsDragConnectionOver = false;
            }

            if (this.IsMouseCaptured) this.ReleaseMouseCapture();

            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured) this.CaptureMouse();
                HitTesting(e.GetPosition(this));
                this.pathGeometry = GetPathGeometry(e.GetPosition(this));
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawGeometry(null, drawingPen, this.pathGeometry);

            // without a background the OnMouseMove event would not be fired
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
        }

        private PathGeometry GetPathGeometry(Point position)
        {
            PathGeometry geometry = new PathGeometry();

            ConnectorOrientation targetOrientation;
            if (HitConnector != null)
                targetOrientation = HitConnector.Orientation;
            else
                targetOrientation = ConnectorOrientation.None;

            List<Point> linePoints = PathFinder.GetConnectionLine(sourceConnector.GetInfo(), position, targetOrientation);

            if (linePoints.Count > 0)
            {
                PathFigure figure = new PathFigure();
                figure.StartPoint = linePoints[0];
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
            }

            return geometry;
        }

        private void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;

            DependencyObject hitObject = designerCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                   hitObject != sourceConnector.ParentDesignerItem &&
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
