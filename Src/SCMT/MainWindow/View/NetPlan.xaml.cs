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
using CommonUtility;
using Xceed.Wpf.Toolkit.PropertyGrid;
using NetPlan;
using SCMTOperationCore.Control;
using SCMTOperationCore;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// NetPlan.xaml 的交互逻辑
    /// </summary>
    public partial class NetPlan : UserControl
    {
        //全局变量，板卡的画板
        private Canvas boardCanvas;

        //全局变量  板卡的列数
        private int boardColumn;

        //全局变量  板卡的行数
        private int boardRow;

        //选中时的边框显示
        private Polygon rect = new Polygon();

        /// <summary>
        /// 初始化函数
        /// </summary>
        public NetPlan()
        {
            InitializeComponent();

            //画小区
            DrawNrRect();

            //构造板卡画板
            CreateMainBoard();           
        }


        #region 绘制小区以及添加功能

        /// <summary>
        /// 绘制小区
        /// </summary>
        private void DrawNrRect()
        {
            //绘制3 * 12 的小区网络

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    //需要给小区添加 id ，所以需要将 rect 和 text 放到一个grid 中
                    Grid newGrid = new Grid();
                    newGrid.Width = 40;
                    newGrid.Height = 40;

                    Rectangle newRect = new Rectangle();
                    newRect.Width = 40;
                    newRect.Height = 40;
                    newRect.Fill = new SolidColorBrush(Colors.Red);
                    newRect.Stroke = new SolidColorBrush(Colors.Black);

                    newGrid.Children.Add(newRect);

                    TextBlock newText = new TextBlock();
                    int nId = i * 12 + j;
                    newText.Text = nId.ToString();
                    newText.HorizontalAlignment = HorizontalAlignment.Center;
                    newText.VerticalAlignment = VerticalAlignment.Center;
                    newGrid.Children.Add(newText);

                    this.nrRectCanvas.Children.Add(newGrid);
                    Canvas.SetTop(newGrid, 40 * i);
                    Canvas.SetLeft(newGrid, 40 * j);

                    newGrid.MouseRightButtonDown += NewGrid_MouseRightButtonDown;
                }
            }
            this.nrRectCanvas.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 小区右键点击时，弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid targetRect = sender as Grid;
            ContextMenu cellRightMenu = new ContextMenu();

            MenuItem doCellPlan = new MenuItem();
            doCellPlan.Header = "进行小区规划";
            doCellPlan.Name = "doCellPlan";
            doCellPlan.Click += DoCellPlan_Click;
            cellRightMenu.Items.Add(doCellPlan);

            MenuItem deleteCellPlan = new MenuItem();
            deleteCellPlan.Header = "删除小区规划";
            deleteCellPlan.Name = "deleteCellPlan";
            deleteCellPlan.Click += DeleteCellPlan_Click;
            cellRightMenu.Items.Add(deleteCellPlan);

            MenuItem cancel = new MenuItem();
            cancel.Header = "取消";
            cancel.Name = "cancel";
            cancel.Click += Cancel_Click;
            cellRightMenu.Items.Add(cancel);

            MenuItem modifyRRU = new MenuItem();
            modifyRRU.Header = "RRU功率调整";
            modifyRRU.Name = "modifyRRU";
            modifyRRU.Click += ModifyRRU_Click;
            cellRightMenu.Items.Add(modifyRRU);

            MenuItem activeCell = new MenuItem();
            activeCell.Header = "去激活小区";
            activeCell.Name = "activeCell";
            activeCell.Click += ActiveCell_Click;
            cellRightMenu.Items.Add(activeCell);

            MenuItem deleteLocalCell = new MenuItem();
            deleteLocalCell.Header = "删除本地小区";
            deleteLocalCell.Name = "deleteLocalCell";
            deleteLocalCell.Click += DeleteLocalCell_Click;
            cellRightMenu.Items.Add(deleteLocalCell);

            targetRect.ContextMenu = cellRightMenu;
        }

        /// <summary>
        /// 删除本地小区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLocalCell_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obj = sender as MenuItem;
            ContextMenu test = obj.Parent as ContextMenu;
            Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
        }

        /// <summary>
        /// 激活小区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveCell_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obj = sender as MenuItem;
            ContextMenu test = obj.Parent as ContextMenu;
            Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
        }

        /// <summary>
        /// RRU 功率修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyRRU_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obj = sender as MenuItem;
            ContextMenu test = obj.Parent as ContextMenu;
            Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
            MessageBox.Show("修改RRU。。。");
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obj = sender as MenuItem;
            ContextMenu test = obj.Parent as ContextMenu;
            Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
        }

        /// <summary>
        /// 删除本地小区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCellPlan_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obj = sender as MenuItem;
            ContextMenu test = obj.Parent as ContextMenu;
            Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
            Rectangle rect = targetRect.Children[0] as Rectangle;
            rect.Fill = new SolidColorBrush(Colors.Red);

            int nCellNumber = this.nrRectCanvas.Children.IndexOf(targetRect);
            this.MyDesigner.g_cellPlaning.Remove(nCellNumber);
        }

        /// <summary>
        /// 右键菜单，进行小区规划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCellPlan_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obj = sender as MenuItem;
            ContextMenu test = obj.Parent as ContextMenu;
            Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
            Rectangle rect = targetRect.Children[0] as Rectangle;
            rect.Fill = new SolidColorBrush(Colors.LightGreen);

            int nCellNumber = this.nrRectCanvas.Children.IndexOf(targetRect);

            if (!this.MyDesigner.g_cellPlaning.Contains(nCellNumber))
            {
                this.MyDesigner.g_cellPlaning.Add(nCellNumber);
            }
        }

        #endregion

        #region 绘制板卡以及添加功能

        /// <summary>
        /// 构造板卡画板信息
        /// </summary>
        private void CreateMainBoard()
        {
            boardCanvas = new Canvas();

            var boardInfo = NPEBoardHelper.GetInstance().GetShelfByEnbType(SCMTOperationCore.Elements.EnbTypeEnum.ENB_EMB6116);

            if(boardInfo == null)
            {
                MessageBox.Show("读取配置文件失败");
                return;
            }

            //设置 2 列 4 行 的板卡框架
            boardColumn = boardInfo.columnsUI;
            boardRow = boardInfo.supportPlanSlotNum / boardColumn;

            for (int i = 0; i < boardColumn; i++)
            {
                for (int j = 0; j < boardRow; j++)
                {
                    //需要给每个板卡添加一个 Canvas ，这样就可以在板卡的 Canvas 上添加内容，删除的时候方便进行判断
                    Canvas boardCanv = new Canvas();

                    //创建板卡矩形区域
                    Rectangle rectItem = new Rectangle();
                    rectItem.Stroke = new SolidColorBrush(Colors.DarkBlue);
                    rectItem.Fill = new SolidColorBrush(Colors.LightGray);
                    rectItem.Width = 240;
                    rectItem.Height = 40;

                    boardCanv.Children.Add(rectItem);
                    boardCanv.Width = 240;
                    boardCanv.Height = 40;

                    Canvas.SetLeft(boardCanv, 240 * i);
                    Canvas.SetBottom(boardCanv, 40 * j);
                    
                    //板卡左键按下，需要判断双击还是单击，双击的时候，进行板卡的添加，单击的时候切换属性和选择
                    boardCanv.MouseLeftButtonDown += BoardCanv_MouseLeftButtonDown;

                    //板卡的右键删除
                    boardCanv.MouseRightButtonDown += BoardCanv_MouseRightButtonDown;

                    boardCanvas.Children.Add(boardCanv);
                }
            }

            boardCanvas.Width = boardColumn * 240;
            boardCanvas.Height = boardRow * 40;
            boardCanvas.Background = new SolidColorBrush(Colors.Red);

            MyDesigner.Children.Add(boardCanvas);
            Canvas.SetLeft(boardCanvas, (MyDesigner.ActualWidth - boardCanvas.Width) / 2);
            Canvas.SetTop(boardCanvas, (MyDesigner.ActualHeight - boardCanvas.Height) / 2);

            //板卡界面点击左键的时候，需要根据不同的选择，用红色的框，框选中被选择的板卡
            boardCanvas.MouseLeftButtonDown += BoardCanvas_MouseLeftButtonDown;

            //初始化选择框
            rect.Stroke = new SolidColorBrush(Colors.Red);
            rect.StrokeThickness = 3;
            rect.Points.Add(new Point(2, 2));
            rect.Points.Add(new Point(238, 2));
            rect.Points.Add(new Point(238, 38));
            rect.Points.Add(new Point(2, 38));
        }

        /// <summary>
        /// 板卡左键按下，需要判断双击还是单击，双击的时候，进行板卡的添加，单击的时候切换属性和选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardCanv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas targetCanvas = (Canvas)sender as Canvas;
            Rectangle targetItem = targetCanvas.Children[0] as Rectangle;
            int soltNum = boardCanvas.Children.IndexOf(targetCanvas);

            //4号槽位不允许进行设置
            if(soltNum == 4 || soltNum == 5)
            {
                return;
            }

            //根据 板卡所在 Canvas 的 索引，判断属于第几行，第几列
            int nColumn = soltNum / boardRow;
            int nRow = soltNum % boardRow;

            //双击显示
            if (e.ClickCount == 2)
            {
                //从后台获取板卡信息
                List<BoardEquipment> listBoardInfo = NPEBoardHelper.GetInstance().GetSlotSupportBoardInfo(soltNum, SCMTOperationCore.Elements.EnbTypeEnum.ENB_EMB6116);

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

                //添加一个文字的描述
                Label boardNameLabel = new Label();
                boardNameLabel.Width = 80;
                boardNameLabel.Height = 25;
                boardNameLabel.FontSize = 8;
                boardNameLabel.Content = boardName.Substring(boardName.IndexOf('|') + 1);
                //为每个光口构造一个名称，根据板卡插槽号和 boardName，也是板卡的名称
                string strIRName = string.Format("{0}-{1}", boardNameLabel.Content.ToString(), soltNum);

                //下发给基站，添加一个索引
                var newBoardInfo = MibInfoMgr.GetInstance().AddNewBoard(soltNum, dlg.strBoardName, dlg.strWorkModel, dlg.strFSM);
                if (newBoardInfo == null)
                {
                    MessageBox.Show("从基站获取数据失败");
                    return;
                }

                MyDesigner.g_AllDevInfo.Add(strIRName, newBoardInfo.m_strOidIndex);
                targetCanvas.Children.Add(boardNameLabel);

                Canvas.SetRight(boardNameLabel, 155);
                Canvas.SetBottom(boardNameLabel, 0);

                targetItem.Fill = new SolidColorBrush(Colors.LightYellow);


                //添加一个板卡信息的描述
                Ellipse boardNameEllipse = new Ellipse();
                boardNameEllipse.Fill = new SolidColorBrush(Colors.Blue);
                boardNameEllipse.Width = 10;
                boardNameEllipse.Height = 10;
                targetCanvas.Children.Add(boardNameEllipse);

                Canvas.SetRight(boardNameEllipse, 210);
                Canvas.SetBottom(boardNameEllipse, 25);


                //填充光口，根据获取到的数量进行添加
                BoardEquipment itemBoard = new BoardEquipment();
                foreach (BoardEquipment item in listBoardInfo)
                {
                    if (item.boardTypeName == boardName)
                    {
                        itemBoard = item;
                    }
                }

                for (int i = 0; i < itemBoard.irOfpNum; i++)
                {
                    DesignerItem designerItem = new DesignerItem();
                    designerItem.ItemName = strIRName;

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
            else if (e.ClickCount == 1)                   //单击显示属性，每次切换的时候重新获取
            {
                if (targetCanvas.Children.Count < 2)               //当前板卡信息未被填充
                {
                    return;
                }

                var labelTarget = targetCanvas.Children[2] as Label;
                //为每个光口构造一个名称，根据板卡插槽号和 boardName，也是板卡的名称
                string strIRName = string.Format("{0}-{1}", labelTarget.Content.ToString(), soltNum);

                var boardAttribute = MibInfoMgr.GetInstance().GetDevAttributeInfo(MyDesigner.g_AllDevInfo[strIRName], EnumDevType.board);
                if (boardAttribute != null)
                {
                    MyDesigner.CreateGirdForNetInfo(strIRName, boardAttribute);
                    gridProperty.Children.Clear();
                    gridProperty.Children.Add(MyDesigner.g_GridForNet[strIRName]);
                }
            }
        }

        /// <summary>
        /// 右键删除板卡信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardCanv_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas targetCanvas = sender as Canvas;

            ContextMenu deleteMenu = new ContextMenu();

            MenuItem deleteBoard = new MenuItem();
            deleteBoard.Header = "删除";
            deleteBoard.Click += DeleteBoard_Click;

            deleteMenu.Items.Add(deleteBoard);

            targetCanvas.ContextMenu = deleteMenu;
        }

        /// <summary>
        /// 右键删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBoard_Click(object sender, RoutedEventArgs e)
        {
            MenuItem targetItem = sender as MenuItem;
            ContextMenu targetMenu = targetItem.Parent as ContextMenu;
            Canvas targetCanvas = ContextMenuService.GetPlacementTarget(targetMenu) as Canvas;

            MessageBoxResult result = MessageBox.Show("确定删除吗？", "提示", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.No)
            {
                return;
            }

            if (targetCanvas.Children != null && targetCanvas.Children.Count > 0)
            {
                //删除之后， Canvas 只有一个 rect 元素，不可以再次删除
                if (targetCanvas.Children.Count < 2)
                {
                    return;
                }

                //首先根据插槽号和板卡名称移除光口信息
                int nPort = boardCanvas.Children.IndexOf(targetCanvas);
                Label lbName = targetCanvas.Children[1] as Label;
                string strIRName = string.Format("{0}-{1}", lbName.Content.ToString(), nPort);

                //首先从基站删除
                if (!MyDesigner.g_AllDevInfo.Keys.Contains(strIRName))
                {
                    MessageBox.Show("没有从设备字典中查找到该设备，请注意");
                    return;
                }
                if (!MibInfoMgr.GetInstance().DelDev(MyDesigner.g_AllDevInfo[strIRName], EnumDevType.board))
                {
                    MessageBox.Show("从基站侧删除设备失败，请注意");
                    return;
                }

                for (int i = 1; i < MyDesigner.Children.Count; i++)
                {
                    if (MyDesigner.Children[i].GetType().Name == "DesignerItem")
                    {
                        DesignerItem item = MyDesigner.Children[i] as DesignerItem;
                        if ((item.ItemName ?? "") == strIRName)
                        {
                            DeleteBoardIR(item);
                            i--;
                        }
                    }
                }

                for (int i = 1; i < targetCanvas.Children.Count; i++)
                {
                    targetCanvas.Children.RemoveAt(i);
                    i--;
                }

                Rectangle rect = targetCanvas.Children[0] as Rectangle;
                rect.Fill = new SolidColorBrush(Colors.LightGray);
            }
        }

        /// <summary>
        /// 删除光口信息
        /// </summary>
        /// <param name="targetItem"></param>
        private void DeleteBoardIR(DesignerItem targetItem)
        {
            MyDesigner.SelectionService.ClearSelection();
            MyDesigner.SelectionService.AddToSelection(targetItem);

            foreach (Connection connection in MyDesigner.SelectionService.CurrentSelection.OfType<Connection>())
            {
                MyDesigner.Children.Remove(connection);
            }

            foreach (DesignerItem item in MyDesigner.SelectionService.CurrentSelection.OfType<DesignerItem>())
            {
                Control cd = item.Template.FindName("PART_ConnectorDecorator", item) as Control;

                List<Connector> connectors = new List<Connector>();
                GetConnectors(cd, connectors);

                foreach (Connector connector in connectors)
                {
                    foreach (Connection con in connector.Connections)
                    {
                        MyDesigner.Children.Remove(con);
                    }
                }
                MyDesigner.Children.Remove(item);                
            }

            MyDesigner.SelectionService.ClearSelection();
            UpdateZIndex();
        }

        private void UpdateZIndex()
        {
            List<UIElement> ordered = (from UIElement item in MyDesigner.Children
                                       orderby Canvas.GetZIndex(item as UIElement)
                                       select item as UIElement).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                Canvas.SetZIndex(ordered[i], i);
            }
        }
        private void GetConnectors(DependencyObject parent, List<Connector> connectors)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is Connector)
                {
                    connectors.Add(child as Connector);
                }
                else
                    GetConnectors(child, connectors);
            }
        }

        /// <summary>
        /// 板卡选择的时候，显示红色边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(boardCanvas != null && boardCanvas.Children.Count > 0)
            {
                Point pt = e.GetPosition(boardCanvas);

                if (!boardCanvas.Children.Contains(rect))
                {
                    boardCanvas.Children.Add(rect);
                }

                //鼠标的点击位置在板卡范围内
                if(pt.X > 0 && pt.X < boardColumn * 240 && pt.Y > 0 && pt.Y < boardRow * 40)
                {
                    int nRectLeft = (int)pt.X / 240;
                    int nRectTop = (int)pt.Y / 40;

                    Canvas.SetLeft(rect, nRectLeft * 240);
                    Canvas.SetTop(rect, nRectTop * 40);
                }
                else
                {
                    boardCanvas.Children.Remove(rect);
                }
            }
        }

        #endregion

        /// <summary>
        /// 初始化设备信息，从基站获取相关配置属性，显示到主界面上
        /// </summary>
        private void InitNetPlan()
        {
            //初始化是否成功
            MibInfoMgr.GetInstance().GetAllEnbInfo().Clear();            

            if (NPSnmpOperator.InitNetPlanInfo())
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
                    //插槽号
                    int nPort = int.Parse(item.m_mapAttributes["netBoardSlotNo"].m_strOriginValue);

                    string strBoardName = item.m_mapAttributes["netBoardType"].m_strOriginValue;
                    var boardInfo = NPEBoardHelper.GetInstance().GetBoardInfoByName(strBoardName);

                    if(boardInfo != null)
                    {
                        CreateBoardInfo(nPort, boardInfo.boardTypeName, boardInfo.irOfpNum, item.m_strOidIndex);
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
                        Canvas targetItem = (Canvas)boardCanvas.Children[nPort];
                        Rectangle rect = (Rectangle)targetItem.Children[0];
                        rect.StrokeThickness = 5;
                        rect.Stroke = new SolidColorBrush(Color.FromRgb(47,255,47));
                    }
                }
            }
        }

        private void CreateBoardInfo(int nPort, string strBoardName, int nIRNumber, string strDevIndex)
        {
            string boardName = strBoardName;

            int soltNum = nPort;
            Canvas targetCanvas = (Canvas)boardCanvas.Children[nPort];
            Rectangle targetItem = (Rectangle)targetCanvas.Children[0];

            //根据 板卡所在 Canvas 的 索引，判断属于第几行，第几列
            int nColumn = soltNum / boardRow;
            int nRow = soltNum % boardRow;

            //添加一个文字的描述
            Label boardNameLabel = new Label();
            boardNameLabel.Width = 80;
            boardNameLabel.Height = 25;
            boardNameLabel.FontSize = 8;
            boardNameLabel.Content = boardName.Substring(boardName.IndexOf('|') + 1);
            targetCanvas.Children.Add(boardNameLabel);

            Canvas.SetRight(boardNameLabel, 155);
            Canvas.SetBottom(boardNameLabel, 0);

            //添加一个板卡信息的描述
            Ellipse boardNameEllipse = new Ellipse();
            boardNameEllipse.Fill = new SolidColorBrush(Colors.Blue);
            boardNameEllipse.Width = 10;
            boardNameEllipse.Height = 10;
            targetCanvas.Children.Add(boardNameEllipse);

            Canvas.SetRight(boardNameEllipse, 210);
            Canvas.SetBottom(boardNameEllipse, 25);

            targetItem.Fill = new SolidColorBrush(Colors.LightYellow);

            //为每个光口构造一个名称，根据板卡插槽号和 boardName
            string strIRName = string.Format("{0}-{1}", boardNameLabel.Content.ToString(), soltNum);

            if(!MyDesigner.g_AllDevInfo.ContainsKey(strIRName))
                MyDesigner.g_AllDevInfo.Add(strIRName, strDevIndex);

            //填充光口，根据获取到的数量进行添加

            for (int i = 0; i < nIRNumber; i++)
            {
                DesignerItem designerItem = new DesignerItem();
                designerItem.ItemName = strIRName;

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
            
        }

        private void PropertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
           
           var property= e.OriginalSource as PropertyItem;
            string a = property.PropertyName;
            object o = e.NewValue;

        }
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

            if(!NPSnmpOperator.DistributeNetPlanData())
            {
                MessageBox.Show("Faild");
            }
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

        /// <summary>
        /// 关闭网络规划按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutAnchorable_Closed(object sender, EventArgs e)
        {
            MyDesigner.g_AllDevInfo.Clear();
            MyDesigner.g_GridForNet.Clear();

            MyDesigner.Children.Clear();
        }
    }
}
