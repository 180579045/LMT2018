using SCMTMainWindow.Property;
using System;
using System.Collections.Generic;
using System.IO;
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
using Xceed.Wpf.AvalonDock.Layout.Serialization;


using SCMTMainWindow.View.Document;
using System.Windows.Markup;
using System.Xml;
using Xceed.Wpf.Toolkit.PropertyGrid;
using NetPlan;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// NetPlan.xaml 的交互逻辑
    /// </summary>
    public partial class NetPlan : UserControl
    {
        //全局变量，板卡的画板
        Propertyies p1 = new Propertyies("botton1","good","luck");
        private Canvas boardCanvas;

        //全局变量  板卡的列数
        private int boardColumn;

        //全局变量  板卡的行数
        private int boardRow;

        //是否初始化网规
        private bool bInit = false;

        public NetPlan()
        {
            InitializeComponent();

            //画小区
            DrawNrRect();

            //propertyGrid.SelectedObject = p1;
            boardCanvas = new Canvas();

            //设置 2 列 4 行 的板卡框架
            boardColumn = 2;
            boardRow = 4;

            for(int i = 0; i < boardColumn; i++)
            {
                for(int j = 0; j < boardRow; j++)
                {
                    Rectangle rectItem = new Rectangle();
                    rectItem.Stroke = new SolidColorBrush(Colors.DarkBlue);
                    rectItem.Fill = new SolidColorBrush(Colors.LightGray);
                    rectItem.Width = 240;
                    rectItem.Height = 40;

                    Canvas.SetLeft(rectItem, 240 * i);
                    Canvas.SetBottom(rectItem, 40 * j);

                    rectItem.MouseLeftButtonDown += RectItem_MouseLeftButtonDown;

                    boardCanvas.Children.Add(rectItem);
                }
            }

            boardCanvas.Width = boardColumn * 240;
            boardCanvas.Height = boardRow * 40;
            boardCanvas.Background = new SolidColorBrush(Colors.Red);

            MyDesigner.Children.Add(boardCanvas);
            Canvas.SetLeft(boardCanvas, (MyDesigner.ActualWidth - boardCanvas.Width) / 2);
            Canvas.SetTop(boardCanvas, (MyDesigner.ActualHeight - boardCanvas.Height) / 2);

            //在初始化的时候为true ，初始化之后为 false
            bInit = true;
        }

        private void InitNetPlan()
        {
            //初始化是否成功
            if(NPSnmpOperator.InitNetPlanInfo())
            {
                var allNPInfo = MibInfoMgr.GetInstance().GetAllEnbInfo();

                //初始化板卡信息
                if(allNPInfo.Keys.Contains(EnumDevType.board))
                {
                    InitBoardInfo(allNPInfo[EnumDevType.board]);
                }
            }
        }

        /// <summary>
        /// 初始化板卡信息
        /// </summary>
        private void InitBoardInfo(List<DevAttributeInfo> listBoardInfo)
        {
            if(listBoardInfo != null && listBoardInfo.Count != 0)
            {
                foreach(DevAttributeInfo item in listBoardInfo)
                {
                    string[] strPorts = item.m_strOidIndex.Split('.');
                    int nPort = int.Parse(strPorts[strPorts.Count() - 1]);

                    int nType = int.Parse(item.m_mapAttributes["netBoardType"].m_strOriginValue);
                    BoardEquipment boardInfo = NPEBoardHelper.GetInstance().GetBoardInfoByType(nType);

                    if(boardInfo != null)
                    {
                        CreateBoardInfo(nPort, boardInfo.boardTypeName, boardInfo.irOfpNum);
                    }
                }
            }

            var realTimeBoardInfo = NPSnmpOperator.GetRealTimeBoardInfo();

            if(realTimeBoardInfo != null && realTimeBoardInfo.Count != 0)
            {
                foreach(var item in realTimeBoardInfo)
                {
                    int nPort = int.Parse(item.m_mapAttributes["boardSlotNo"].m_strOriginValue);

                    if((nPort > 0) && (nPort < 7))
                    {
                        Rectangle targetItem = (Rectangle)boardCanvas.Children[nPort];
                        targetItem.StrokeThickness = 5;
                        targetItem.Stroke = new SolidColorBrush(Colors.LightGreen);
                    }
                }
            }
        }

        private void CreateBoardInfo(int nPort, string strBoardName, int nIRNumber)
        {
            string boardName = strBoardName;

            int soltNum = nPort;
            Rectangle targetItem = (Rectangle)boardCanvas.Children[nPort];

            //根据 板卡所在 Canvas 的 索引，判断属于第几行，第几列
            int nColumn = soltNum / boardRow;
            int nRow = soltNum % boardRow;

            //添加一个板卡信息的描述
            Ellipse boardNameEllipse = new Ellipse();
            boardNameEllipse.Fill = new SolidColorBrush(Colors.Blue);
            boardNameEllipse.Width = 10;
            boardNameEllipse.Height = 10;
            boardCanvas.Children.Add(boardNameEllipse);

            Canvas.SetRight(boardNameEllipse, 200 + 240 * (boardColumn - 1 - nColumn));
            Canvas.SetBottom(boardNameEllipse, 20 + 40 * nRow);

            //添加一个文字的描述
            Label boardNameLabel = new Label();
            boardNameLabel.Width = 80;
            boardNameLabel.Height = 25;
            boardNameLabel.Content = boardName.Substring(boardName.IndexOf('|') + 1);
            boardCanvas.Children.Add(boardNameLabel);

            Canvas.SetRight(boardNameLabel, 155 + 240 * (boardColumn - 1 - nColumn));
            Canvas.SetBottom(boardNameLabel, 0 + 40 * nRow);

            targetItem.Fill = new SolidColorBrush(Colors.LightYellow);

            //填充光口，根据获取到的数量进行添加

            for (int i = 0; i < nIRNumber; i++)
            {
                DesignerItem designerItem = new DesignerItem();

                Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/XMLFile1.xml");
                Stream stream = Application.GetResourceStream(strUri).Stream;

                FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;
                Object content = el.FindName("g_IR") as Grid;

                string strXAML = XamlWriter.Save(content);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML)));
                designerItem.Content = testContent;

                //获取 Canvas 相对于 DesignerCanvas 的位置，方便进行光口的添加

                double CanvasLeft = DesignerCanvas.GetLeft(boardCanvas);
                double CanvasTop = DesignerCanvas.GetTop(boardCanvas);

                Canvas.SetLeft(designerItem, CanvasLeft + 200 + 240 * nColumn - i * 20);
                Canvas.SetTop(designerItem, CanvasTop + 12.5 + 40 * (boardRow - 1 - nRow));

                MyDesigner.Children.Add(designerItem);
                SetConnectorDecoratorTemplate(designerItem);
            }
        }



        /// <summary>
        /// 绘制小区
        /// </summary>
        private void DrawNrRect()
        {
            //绘制3 * 12 的小区网络

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 12; j++)
                {
                    Rectangle newRect = new Rectangle();
                    newRect.Width = 40;
                    newRect.Height = 40;
                    newRect.Fill = new SolidColorBrush(Colors.Red);
                    newRect.Stroke = new SolidColorBrush(Colors.Black);

                    this.nrRectCanvas.Children.Add(newRect);
                    Canvas.SetTop(newRect, 40 * i);
                    Canvas.SetLeft(newRect, 40 * j);
                }
            }
            this.nrRectCanvas.Visibility = Visibility.Hidden;
        }

        private string BroadName(BroadType bt)
        {
            switch (bt)
            {
                case BroadType.SCTE:
                    return "SCTE";
                case BroadType.SCTF:
                    return "SCTF";
                case BroadType.BPOH:
                    return "BPOH";
                case BroadType.BPOI:
                    return "BPOI";
                case BroadType.BPOK:
                    return "BPOK";
                default:
                    return null;


            }
        }

        private void SetConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                ControlTemplate template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Template = template;
            }
        }

        private void RectItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle targetItem = (Rectangle)sender as Rectangle;
            int soltNum = boardCanvas.Children.IndexOf(targetItem);

            //根据 板卡所在 Canvas 的 索引，判断属于第几行，第几列
            int nColumn = soltNum / boardRow;
            int nRow = soltNum % boardRow;

            //双击显示
            if (e.ClickCount == 2)
            {
                //从后台获取板卡信息
                List<BoardEquipment> listBoardInfo = NPEBoardHelper.GetInstance().GetSlotSupportBoardInfo(soltNum);

                ChooseBoardType dlg = new ChooseBoardType(listBoardInfo);
                dlg.ShowDialog();

                //BoradDetailData bd = new BoradDetailData();
                ////初始化弹框默认数据
                //bd.setDefaultDate(soltNum);
                ////实例化弹窗
                //AddBoardWindow broadDetailWindos = new AddBoardWindow();
                ////初始化弹窗
                //broadDetailWindos.SetOperationData(bd);
                ////展示弹窗
                //broadDetailWindos.ShowDialog();

                //if (!broadDetailWindos.isOk)
                //{
                //    return;
                //}

                //获取弹窗中设置的板卡名称
                //string boardName = BroadName(broadDetailWindos.detaiData.Bt);

                if (!dlg.bOK)
                {
                    return;
                }

                string boardName = dlg.strBoardName;

                //添加一个板卡信息的描述
                Ellipse boardNameEllipse = new Ellipse();
                boardNameEllipse.Fill = new SolidColorBrush(Colors.Blue);
                boardNameEllipse.Width = 10;
                boardNameEllipse.Height = 10;
                boardCanvas.Children.Add(boardNameEllipse);

                Canvas.SetRight(boardNameEllipse, 200 + 240 * (boardColumn - 1 - nColumn));
                Canvas.SetBottom(boardNameEllipse, 20 + 40 * nRow);

                //添加一个文字的描述
                Label boardNameLabel = new Label();
                boardNameLabel.Width = 80;
                boardNameLabel.Height = 25;
                boardNameLabel.Content = boardName.Substring(boardName.IndexOf('|')+1);
                boardCanvas.Children.Add(boardNameLabel);

                Canvas.SetRight(boardNameLabel, 155 + 240 * (boardColumn - 1 - nColumn));
                Canvas.SetBottom(boardNameLabel, 0 + 40 * nRow);

                targetItem.Fill = new SolidColorBrush(Colors.LightYellow);

                //填充光口，根据获取到的数量进行添加
                BoardEquipment itemBoard = new BoardEquipment();
                foreach(BoardEquipment item in listBoardInfo)
                {
                    if(item.boardTypeName == boardName)
                    {
                        itemBoard = item;
                    }
                }

                for(int i = 0; i < itemBoard.irOfpNum; i++)
                {
                    DesignerItem designerItem = new DesignerItem();

                    Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/XMLFile1.xml");
                    Stream stream = Application.GetResourceStream(strUri).Stream;

                    FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;
                    Object content = el.FindName("g_IR") as Grid;

                    string strXAML = XamlWriter.Save(content);
                    Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML)));
                    designerItem.Content = testContent;

                    //获取 Canvas 相对于 DesignerCanvas 的位置，方便进行光口的添加

                    double CanvasLeft = DesignerCanvas.GetLeft(boardCanvas);
                    double CanvasTop = DesignerCanvas.GetTop(boardCanvas);

                    Canvas.SetLeft(designerItem, CanvasLeft + 200 + 240 * nColumn - i*20);
                    Canvas.SetTop(designerItem, CanvasTop + 12.5 + 40 * (boardRow - 1 - nRow));

                    MyDesigner.Children.Add(designerItem);
                    SetConnectorDecoratorTemplate(designerItem);
                }

            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(dockingmanger);
            using (var stream = new StreamReader("layout.xml"))
            {
                serializer.Deserialize(stream);
            }

        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                LayoutAnchorable la = new LayoutAnchorable();
                la.Title = "断点";
                la.Content = new TextBox();
                LayoutDocument ld = new LayoutDocument();
                ld.Title = "good";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(dockingmanger);
            using (var stream = new StreamWriter("layout.xml"))
            {
                serializer.Serialize(stream);
            }
        }

        //网络规划面板改变大小的时候，需要重新绘制板卡的位置，使其一直保持居中
        private void MyDesigner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(boardCanvas, (MyDesigner.ActualWidth - boardCanvas.Width) / 2);
            Canvas.SetTop(boardCanvas, (MyDesigner.ActualHeight - boardCanvas.Height) / 2);

            //除了 Canvas 之外，重新绘制相对大小
            for(int i = 1; i < MyDesigner.Children.Count; i++)
            {
                UIElement uiItem = MyDesigner.Children[i];

                double uiLeft = DesignerCanvas.GetLeft(uiItem) + (e.NewSize.Width - e.PreviousSize.Width) / 2;
                double uiTop = DesignerCanvas.GetTop(uiItem) + (e.NewSize.Height - e.PreviousSize.Height) / 2;

                uiLeft = uiLeft < 0 ? 0 : uiLeft;
                uiTop = uiTop < 0 ? 0 : uiTop;

                DesignerCanvas.SetLeft(uiItem, uiLeft);
                DesignerCanvas.SetTop(uiItem, uiTop);
            }

            if(bInit)
            {

                bInit = false;
            }
            
        }

        private void PropertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
           
           var property= e.OriginalSource as PropertyItem;
            string a = property.PropertyName;
            object o = e.NewValue;

        }
        
        //private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        //{
        //    if(this.gridNetPlan.RowDefinitions[0].Height == GridLength.Auto)
        //    {
        //        this.gridNetPlan.RowDefinitions[0].Height = new GridLength(180);
        //        this.nrRectCanvas.Visibility = Visibility.Visible;
        //        //this.ExpanderNrRect.Header = "隐藏小区";
        //    }
        //    else
        //    {
        //        this.gridNetPlan.RowDefinitions[0].Height = GridLength.Auto;
        //        this.nrRectCanvas.Visibility = Visibility.Hidden;
        //        //this.ExpanderNrRect.Header = "展开小区";
        //    }

        //}


        //protected override Size MeasureOverride(Size constraint)
        //{

        //    MyDesigner.Measure(constraint);
        //    return base.MeasureOverride(constraint);
        //}
        /// <summary>
        /// 点击新建网规配置文件触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewHandler(object sender, RoutedEventArgs e)
        {
            //handle click event
            MessageBox.Show("ggggggggggggg");
        }
        /// <summary>
        /// 点击导入网规文件触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportHandler(object sender, RoutedEventArgs e)
        {
            //handle click event
            MessageBox.Show("ggggggggggggg");
        }
        /// <summary>
        /// 点击下发网规规划触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownHandler(object sender, RoutedEventArgs e)
        {
            //handle click event
            MessageBox.Show("ggggggggggggg");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshHandler(object sender, RoutedEventArgs e)
        {
            //handle click event
            MessageBox.Show("ggggggggggggg");
        }
        private void ClearHandler(object sender, RoutedEventArgs e)
        {
            //handle click event
            MessageBox.Show("ggggggggggggg");
        }
        private void LineHandler(object sender, RoutedEventArgs e) {
            MessageBox.Show("ggggggggggggg");
        }
        private void ZoomIncreaseHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ggggggggggggg");
        }

        private void ZoomDecreaseHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ggggggggggggg");
        }
        private void FullScreenHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ggggggggggggg");
        }
        private void HiddenCellHandler(object sender, RoutedEventArgs e)
        {
            if (this.gridNetPlan.RowDefinitions[1].Height == GridLength.Auto)
            {
                this.gridNetPlan.RowDefinitions[1].Height = new GridLength(180);
                this.nrRectCanvas.Visibility = Visibility.Visible;
                //this.ExpanderNrRect.Header = "隐藏小区";
                ToolTip tip = new System.Windows.Controls.ToolTip();
                tip.Content = "隐藏小区 Ctrl+Alt+H";
                this.btnHiddenCell.ToolTip = tip;
            }
            else
            {
                this.gridNetPlan.RowDefinitions[1].Height = GridLength.Auto;
                this.nrRectCanvas.Visibility = Visibility.Hidden;
                //this.ExpanderNrRect.Header = "展开小区";
                ToolTip tip = new System.Windows.Controls.ToolTip();
                tip.Content = "展开小区 Ctrl+Alt+H";
                this.btnHiddenCell.ToolTip = tip;
            }
        }
        private void DeleteElementHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ggggggggggggg");
        }

        /// <summary>
        /// 窗口加载的时候，初始化网规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化网规
            InitNetPlan();
        }
    }
}
