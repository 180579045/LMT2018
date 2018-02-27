// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DrawingCanvasView.xaml.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   MainWindow.xaml 的交互逻辑
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DT.Tools.FlowChart
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Common.Logging;

    using Brushes = System.Windows.Media.Brushes;
    using Pen = System.Windows.Media.Pen;
    using Point = System.Windows.Point;

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DrawingCanvasView
    {
        /// <summary>
        /// The default space x.
        /// </summary>
        private const double DefaultSpaceX = 250;

        /// <summary>
        /// The default space y.
        /// </summary>
        private const double DefaultSpaceY = 30;

        /// <summary>
        /// The left space.
        /// </summary>
        private const double LeftSpace = 35;

        /// <summary>
        /// The top space.
        /// </summary>
        private const double TopSpace = 15;

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(DrawingCanvasView));

        /// <summary>
        /// The select rectangle visual.
        /// </summary>
        private readonly FlowChartDrawVisual selectRectangleVisual = new FlowChartDrawVisual();

        /// <summary>
        /// The focus ellipse visual.
        /// </summary>
        private readonly FlowChartDrawVisual focusEllipseVisual = new FlowChartDrawVisual();

        /// <summary>
        /// The net element point x.
        /// </summary>
        private readonly Dictionary<string, double> netElementPointX = new Dictionary<string, double>();

        /// <summary>
        /// The messages start point.
        /// </summary>
        private readonly List<Point> messagesStartPoint = new List<Point>();

        /// <summary>
        /// The messages stop point.
        /// </summary>
        private readonly List<Point> messagesStopPoint = new List<Point>();

        /// <summary>
        /// The select draw visual.
        /// </summary>
        private FlowChartDrawVisual selectDrawVisual;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawingCanvasView"/> class.
        /// </summary>
        public DrawingCanvasView()
        {
            this.InitializeComponent();
            this.CleanAll();
        }

        /// <summary>
        /// The object type.
        /// </summary>
        private enum ObjectType
        {
            /// <summary>
            /// The net element rectangle line.
            /// </summary>
            NetElementRectangleLine,

            /// <summary>
            /// The net element rectangle.
            /// </summary>
            NetElementRectangle,

            /// <summary>
            /// The net element line.
            /// </summary>
            NetElementLine,

            /// <summary>
            /// The net element title.
            /// </summary>
            NetElementTitle,

            /// <summary>
            /// The message vector right.
            /// </summary>
            MessageVectorRight,

            /// <summary>
            /// The message vector left.
            /// </summary>
            MessageVectorLeft,

            /// <summary>
            /// The message text.
            /// </summary>
            MessageText,

            /// <summary>
            /// The focus ellipse line.
            /// </summary>
            FocusEllipseLine,

            /// <summary>
            /// The focus ellipse.
            /// </summary>
            FocusEllipse,

            /// <summary>
            /// The selected rectangle.
            /// </summary>
            SelectedRectangle
        }

        /// <summary>
        /// Gets the select draw visual.
        /// </summary>
        public FlowChartDrawVisual SelectDrawVisual
        {
            get
            {
                return this.selectDrawVisual;
            }
        }

        /// <summary>
        /// The drawing flow chart.
        /// </summary>
        /// <param name="netElementNames">
        /// The net element names.
        /// </param>
        /// <param name="messages">
        /// The messages.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool DrawingFlowChart(List<string> netElementNames, List<BasicMessage> messages)
        {
            this.drawingSurface.Width = (netElementNames.Count * DefaultSpaceX) + LeftSpace;
            this.drawingSurface.Height = (messages.Count * DefaultSpaceY) + (TopSpace * 2);

            this.titleSurface.Width = (netElementNames.Count * DefaultSpaceX) + LeftSpace;
            this.messageScrollView.ScrollToBottom();
           // this.titleSurface.Height = 35;
            this.Clean();
            if (!this.InitializeSearchList(messages))
            {
                Log.Error("InitializeSearchList error");
                return false;
            }

            if (!this.InitializeCanvas(netElementNames))
            {
                Log.Error("InitializeCanvas error");
                return false;
            }

            this.DrawingMessages(messages);
            return true;
        }

        /// <summary>
        /// The initialize canvas.
        /// </summary>
        /// <param name="netElementNames">
        /// The net element names.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool InitializeCanvas(List<string> netElementNames)
        {
            if (netElementNames.Count < 2)
            {
                Log.Error("less than 2 net elements");
                return false;
            }

            try
            {
                var drawingVisual = new DrawingVisual();
                var drawingVisual1 = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    for (int i = 0; i < netElementNames.Count; i++)
                    {
                       // var netElementName = netElementNames[i];
                        double pointX = (DefaultSpaceX * i) + LeftSpace;
                       // this.netElementPointX.Add(netElementName, pointX);

                        var point = new Point(pointX, TopSpace);
                        this.AppendNetElement(drawingContext, null, point);
                    }
                }
                using (DrawingContext drawingContext1 = drawingVisual1.RenderOpen())
                {
                    for (int i = 0; i < netElementNames.Count; i++)
                    {
                        var netElementName = netElementNames[i];
                        double pointX = (DefaultSpaceX * i) + LeftSpace;
                        this.netElementPointX.Add(netElementName, pointX);

                        var point = new Point(pointX, TopSpace);
                        this.AppendNetElement1(drawingContext1, netElementName, point);
                    }
                }
                this.titleSurface.AddVisual(drawingVisual1);
                this.drawingSurface.AddVisual(drawingVisual);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// The initialize search list.
        /// </summary>
        /// <param name="messages">
        /// The messages.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool InitializeSearchList(IEnumerable<BasicMessage> messages)
        {
            if (messages == null)
            {
                return false;
            }

            var messageList = new List<string>();
            foreach (var basicMessage in messages)
            {
                messageList.Add(basicMessage.Name);
            }

            this.SearchMessageList.ItemsSource = messageList;
            return true;
        }

        /// <summary>
        /// The drawing messages.
        /// </summary>
        /// <param name="messages">
        /// The messages.
        /// </param>
        private void DrawingMessages(List<BasicMessage> messages)
        {
            for (var i = 0; i < messages.Count; i++)
            {
                var basicMessage = messages[i];
                var flowChartDrawVisual = new FlowChartDrawVisual();
                using (DrawingContext drawingContext = flowChartDrawVisual.RenderOpen())
                {
                    string messageContent = "[" + basicMessage.TimeStamp + "]\r\n" + basicMessage.Name;
                    var startPointX = this.netElementPointX[basicMessage.SourceElement];
                    var endPointX = this.netElementPointX[basicMessage.DestinationElement];
                    //var pointY = (TopSpace * 3) + (DefaultSpaceY * i);
                    var pointY = TopSpace + (DefaultSpaceY * i);
                    var startPoint = new Point(startPointX, pointY);
                    var endPoint = new Point(endPointX, pointY);
                    this.AppendMessage(drawingContext, messageContent, startPoint, endPoint);
                }

                flowChartDrawVisual.StartPoint = this.messagesStartPoint[i];
                flowChartDrawVisual.StopPoint = this.messagesStopPoint[i];
                flowChartDrawVisual.ParserId = basicMessage.ParserId;
                flowChartDrawVisual.OID = basicMessage.OID;

                this.drawingSurface.AddVisual(flowChartDrawVisual);
            }
        }

        /// <summary>
        /// The append net element.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing context.
        /// </param>
        /// <param name="netElementName">
        /// The net element name.
        /// </param>
        /// <param name="point">
        /// The point.
        /// </param>
        private void AppendNetElement(DrawingContext drawingContext, string netElementName, Point point)
        {
            //this.AddRectangle(drawingContext, point);
            this.AddLine(drawingContext, point);
           // this.AddNetElementTitle(drawingContext, netElementName, point);
        }

        private void AppendNetElement1(DrawingContext drawingContext, string netElementName, Point point)
        {
            this.AddRectangle(drawingContext, point);
           // this.AddLine(drawingContext, point);
            this.AddNetElementTitle(drawingContext, netElementName, point);
        }

        /// <summary>
        /// The append message.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing context.
        /// </param>
        /// <param name="messageName">
        /// The message name.
        /// </param>
        /// <param name="startPoint">
        /// The start point.
        /// </param>
        /// <param name="endPoint">
        /// The end point.
        /// </param>
        private void AppendMessage(DrawingContext drawingContext, string messageName, Point startPoint, Point endPoint)
        {
            this.AddVector(drawingContext, startPoint, endPoint);
            var point = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            this.AddMessageText(drawingContext, messageName, point);
        }

        /// <summary>
        /// The add line.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing context.
        /// </param>
        /// <param name="point">
        /// The point.
        /// </param>
        private void AddLine(DrawingContext drawingContext, Point point)
        {
            double rectangleHeight = 30;
            double lineHeight = this.drawingSurface.Height;

            var startPoint = new Point(point.X, point.Y - (rectangleHeight / 2));
            var endPoint = new Point(point.X, point.Y - (rectangleHeight / 2) + lineHeight);

            var selectedPen = this.SelectedColor(ObjectType.NetElementLine);
            drawingContext.DrawLine(selectedPen, startPoint, endPoint);
        }

        /// <summary>
        /// The add rectangle.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing context.
        /// </param>
        /// <param name="point">
        /// The point.
        /// </param>
        private void AddRectangle(DrawingContext drawingContext, Point point)
        {
            double rectangleHeight = 30;
            double rectandleWidth = 50;

            var startPoint = new Point(point.X - (rectandleWidth / 2), point.Y - (rectangleHeight / 2));
            var endPoint = new Point(point.X + (rectandleWidth / 2), point.Y + (rectangleHeight / 2));

            var selectedPen = this.SelectedColor(ObjectType.NetElementRectangleLine);
            var filledColor = this.SelectedColor(ObjectType.NetElementRectangle);
            var rectangle = new Rect(startPoint, endPoint);
            drawingContext.DrawRectangle(filledColor.Brush, selectedPen, rectangle);
        }

        /// <summary>
        /// The add vector.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing context.
        /// </param>
        /// <param name="startPoint">
        /// The start point.
        /// </param>
        /// <param name="endPoint">
        /// The end point.
        /// </param>
        private void AddVector(DrawingContext drawingContext, Point startPoint, Point endPoint)
        {
            Pen selectedPen;
            double pointX;
            double length = 5;
            double height = 5;
            double pointYDown = endPoint.Y + length;
            double pointYUp = endPoint.Y - length;
            if (endPoint.X < startPoint.X)
            {
                pointX = endPoint.X + height;
                selectedPen = this.SelectedColor(ObjectType.MessageVectorLeft);
            }
            else if (endPoint.X > startPoint.X)
            {
                pointX = endPoint.X - length;
                selectedPen = this.SelectedColor(ObjectType.MessageVectorRight);
            }
            else
            {
                return;
            }

            var pointDown = new Point(pointX, pointYDown);
            var pointUp = new Point(pointX, pointYUp);
            drawingContext.DrawLine(selectedPen, pointUp, endPoint);
            drawingContext.DrawLine(selectedPen, pointDown, endPoint);

            drawingContext.DrawLine(selectedPen, startPoint, endPoint);
        }

        /// <summary>
        /// The add net element title.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing context.
        /// </param>
        /// <param name="textContent">
        /// The text content.
        /// </param>
        /// <param name="point">
        /// The point.
        /// </param>
        private void AddNetElementTitle(DrawingContext drawingContext, string textContent, Point point)
        {
            var selectedPen = this.SelectedColor(ObjectType.NetElementTitle);
            var formattedText = new FormattedText(
                textContent,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("微软雅黑"),
                12,
                selectedPen.Brush);

            var leftTopPoint = new Point(point.X - (formattedText.Width / 2), point.Y - (formattedText.Height / 2));
            drawingContext.DrawText(formattedText, leftTopPoint);
        }

        /// <summary>
        /// The add text.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing context.
        /// </param>
        /// <param name="textContent">
        /// The text content.
        /// </param>
        /// <param name="point">
        /// The point.
        /// </param>
        private void AddMessageText(DrawingContext drawingContext, string textContent, Point point)
        {
            var selectedPen = this.SelectedColor(ObjectType.MessageText);
            var formattedText = new FormattedText(
                textContent,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("微软雅黑"),
                12,
                selectedPen.Brush);

            var leftTopPoint = new Point(point.X - (formattedText.Width / 2), point.Y - (formattedText.Height / 2));
            var rightBottomPoint = new Point(point.X + (formattedText.Width / 2), point.Y + (formattedText.Height / 2));
            drawingContext.DrawText(formattedText, leftTopPoint);

            this.messagesStartPoint.Add(leftTopPoint);
            this.messagesStopPoint.Add(rightBottomPoint);
        }

        /// <summary>
        /// The clean all.
        /// </summary>
        private void CleanAll()
        {
            this.netElementPointX.Clear();
            drawingSurface.ClearAllVisuals();
            this.SearchMessageList.ItemsSource = null;
            this.SearchMessageList.Text = string.Empty;
            this.searchButton.IsEnabled = false;
        }

        public void Clean() {

            CleanAll();
        }

        /// <summary>
        /// The draw focus ellipse visual.
        /// </summary>
        /// <param name="messagePosition">
        /// The message position.
        /// </param>
        private void DrawFocusEllipseVisual(Point messagePosition)
        {
            if ((int)messagePosition.X == 0)
            {
                return;
            }

            using (DrawingContext drawingContext = this.focusEllipseVisual.RenderOpen())
            {
                /*绘制矩形*/
                var widthAnimation = new DoubleAnimation(30, 0, new Duration(TimeSpan.FromSeconds(1))) { FillBehavior = FillBehavior.Stop };
                var selectedPen = this.SelectedColor(ObjectType.FocusEllipseLine);
                var filledColor = this.SelectedColor(ObjectType.FocusEllipse);
                widthAnimation.Completed += this.FocusEllicpseAnimationCompleted;
                drawingContext.DrawEllipse(
                    filledColor.Brush,
                    selectedPen,
                    messagePosition,
                    null,
                    0,
                    widthAnimation.CreateClock(),
                    0,
                    widthAnimation.CreateClock());
            }

            this.drawingSurface.AddVisual(this.focusEllipseVisual);
            searchButton.IsEnabled = false;

            drawingSurface.ResetPosition();

            messageScrollView.ScrollToHorizontalOffset(messagePosition.X - LeftSpace);
            messageScrollView.ScrollToVerticalOffset(messagePosition.Y - 100);
        }

        /// <summary>
        /// The width animation_ completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void FocusEllicpseAnimationCompleted(object sender, EventArgs e)
        {
            this.drawingSurface.DeleteVisual(this.focusEllipseVisual);
            searchButton.IsEnabled = true;
        }

        /// <summary>
        /// The drawing surface_ mouse left button down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void VisualMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointClicked = e.GetPosition(this.drawingSurface);
            var visual = this.drawingSurface.GetVisual(pointClicked) as FlowChartDrawVisual;
            this.selectDrawVisual = visual;

            if (visual == null)
            {
                this.drawingSurface.DeleteVisual(this.selectRectangleVisual);
            }
            else
            {
                this.drawingSurface.DeleteVisual(this.selectRectangleVisual);
                this.SelectMessage(visual);
            }
        }

        /// <summary>
        /// The reset button on click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ResetButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.drawingSurface.ResetPosition();
        }

        /// <summary>
        /// The select message.
        /// </summary>
        /// <param name="flowChartDrawVisual">
        /// The flow chart draw visual.
        /// </param>
        private void SelectMessage(FlowChartDrawVisual flowChartDrawVisual)
        {
            if (flowChartDrawVisual == null)
            {
                return;
            }

            this.drawingSurface.DeleteVisual(this.selectRectangleVisual);

            using (DrawingContext dc = this.selectRectangleVisual.RenderOpen())
            {
                /*绘制矩形*/
                var selectedPen = this.SelectedColor(ObjectType.SelectedRectangle);
                var selectedRectangle = new Rect(flowChartDrawVisual.StartPoint, flowChartDrawVisual.StopPoint);
                dc.DrawRectangle(Brushes.Transparent, selectedPen, selectedRectangle);
            }

            this.drawingSurface.AddVisual(this.selectRectangleVisual);

            this.selectRectangleVisual.StartPoint = flowChartDrawVisual.StartPoint;
            this.selectRectangleVisual.StopPoint = flowChartDrawVisual.StopPoint;
            this.selectRectangleVisual.ParserId = flowChartDrawVisual.ParserId;
            this.selectRectangleVisual.OID = flowChartDrawVisual.OID;
        }

        /// <summary>
        /// The on search button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnSearchButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.messagesStartPoint == null)
            {
                return;
            }

            var index = SearchMessageList.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            this.DrawFocusEllipseVisual(this.messagesStartPoint[index]);
        }

        /// <summary>
        /// The selected color.
        /// </summary>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <returns>
        /// The <see cref="Pen"/>.
        /// </returns>
        private Pen SelectedColor(ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.NetElementRectangleLine:
                    return new Pen(Brushes.CornflowerBlue, 3);
                case ObjectType.NetElementRectangle:
                    return new Pen(Brushes.BurlyWood, 3);
                case ObjectType.NetElementLine:
                    return new Pen(Brushes.CornflowerBlue, 3);
                case ObjectType.NetElementTitle:
                    return new Pen(Brushes.Black, 3);

                case ObjectType.MessageVectorRight:
                    return new Pen(Brushes.Green, 3);
                case ObjectType.MessageVectorLeft:
                    return new Pen(Brushes.DeepSkyBlue, 3);
                case ObjectType.MessageText:
                    return new Pen(Brushes.Black, 3);

                case ObjectType.FocusEllipseLine:
                    return new Pen(Brushes.Fuchsia, 3);
                case ObjectType.FocusEllipse:
                    return new Pen(Brushes.PaleGoldenrod, 3);

                case ObjectType.SelectedRectangle:
                    return new Pen(Brushes.Black, 1) { DashStyle = DashStyles.Dash };

                default:
                    return new Pen(Brushes.Gray, 3);
            }
        }

        /// <summary>
        /// The when box closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void WhenBoxClosed(object sender, EventArgs e)
        {
            var textContent = this.SearchMessageList.Text;
            if (textContent == string.Empty)
            {
                this.searchButton.IsEnabled = false;
            }
            else
            {
                this.searchButton.IsEnabled = true;
            }
        }
    }
}
