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
using System.Windows.Controls.Primitives;


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
        private Polygon rectForCell = new Polygon();

        //初始化连接
        private int nSizeChangedNo = 0;
        private bool bInit = false;

        //判断是否取消连接点
        public bool bHiddenLineConnector = true;

        //创建一个 dictionary 保存每个板卡对应连接的设备
        Dictionary<int, int> allBoardToDev = new Dictionary<int, int>();

        /// <summary>
        /// 初始化函数
        /// </summary>
        public NetPlan()
        {
            InitializeComponent();

            this.leftList.SelectedContentIndex = 1;

            //画小区
            DrawNrRect();

            //构造板卡画板
            CreateMainBoard();

            CreateTemplateList();

            for (int i = 0; i < boardRow * boardColumn; i++)
            {
                allBoardToDev.Add(i, 0);
            }

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

            rectForCell.Stroke = new LinearGradientBrush(Colors.Black, Colors.LightSkyBlue, 30.0);
            rectForCell.StrokeThickness = 3;
            rectForCell.Points.Add(new Point(2, 2));
            rectForCell.Points.Add(new Point(38, 2));
            rectForCell.Points.Add(new Point(38, 38));
            rectForCell.Points.Add(new Point(2, 38));
            this.nrRectCanvas.Visibility = Visibility.Hidden;
            this.nrRectCanvas.MouseLeftButtonDown += NrRectCanvas_MouseLeftButtonDown;
            this.nrRectCanvas.MouseRightButtonDown += NrRectCanvas_MouseRightButtonDown;
        }

        private void NrRectCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (nrRectCanvas != null && nrRectCanvas.Children.Count > 0)
            {
                Point pt = e.GetPosition(nrRectCanvas);

                if (!nrRectCanvas.Children.Contains(rectForCell))
                {
                    nrRectCanvas.Children.Add(rectForCell);
                }

                //鼠标的点击位置在板卡范围内
                if (pt.X > 0 && pt.X < 12 * 40 && pt.Y > 0 && pt.Y < 3 * 40)
                {
                    int nRectLeft = (int)pt.X / 40;
                    int nRectTop = (int)pt.Y / 40;

                    Canvas.SetLeft(rectForCell, nRectLeft * 40);
                    Canvas.SetTop(rectForCell, nRectTop * 40);

                    int nCellID = nRectTop * 12 + nRectLeft;
                    SetCellProperty(nCellID);
                }
                else
                {
                    nrRectCanvas.Children.Remove(rectForCell);
                }
            }
        }

        private void NrRectCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (nrRectCanvas != null && nrRectCanvas.Children.Count > 0)
            {
                Point pt = e.GetPosition(nrRectCanvas);

                if (!nrRectCanvas.Children.Contains(rectForCell))
                {
                    nrRectCanvas.Children.Add(rectForCell);
                }

                //鼠标的点击位置在板卡范围内
                if (pt.X > 0 && pt.X < 12 * 40 && pt.Y > 0 && pt.Y < 3 * 40)
                {
                    int nRectLeft = (int)pt.X / 40;
                    int nRectTop = (int)pt.Y / 40;

                    Canvas.SetLeft(rectForCell, nRectLeft * 40);
                    Canvas.SetTop(rectForCell, nRectTop * 40);
                    int nCellID = nRectTop * 12 + nRectLeft;
                    SetCellProperty(nCellID);
                }
                else
                {
                    nrRectCanvas.Children.Remove(rectForCell);
                }
            }
        }

        private void SetCellProperty(int nCellID)
        {            
            var devAttr = MibInfoMgr.GetInstance().GetDevAttributeInfo($".{ nCellID }", EnumDevType.nrNetLc);
            MyDesigner.CreateGirdForNetInfo("小区"+nCellID, devAttr);
            MyDesigner.g_nowDevAttr = devAttr;
        }

        /// <summary>
        /// 小区右键点击时，弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string strIP = CSEnbHelper.GetCurEnbAddr();
            if(strIP == null || strIP == "")
            {
                MessageBox.Show("未选择基站");
                return;
            }

            Grid targetRect = sender as Grid;
            int nCellId = this.nrRectCanvas.Children.IndexOf(targetRect);
            var cellStatus = NPCellOperator.GetLcStatus(nCellId, strIP);

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

            //MenuItem modifyRRU = new MenuItem();
            //modifyRRU.Header = "RRU功率调整";
            //modifyRRU.Name = "modifyRRU";
            //modifyRRU.Click += ModifyRRU_Click;
            //cellRightMenu.Items.Add(modifyRRU);

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

            switch(cellStatus)
            {
                case LcStatus.UnPlan:
                    doCellPlan.IsEnabled = true;
                    deleteCellPlan.IsEnabled = false;
                    cancel.IsEnabled = false;
                    activeCell.IsEnabled = false;
                    deleteLocalCell.IsEnabled = false;
                    break;
                case LcStatus.Planning:
                    doCellPlan.IsEnabled = false;
                    deleteCellPlan.IsEnabled = false;
                    cancel.IsEnabled = true;
                    activeCell.IsEnabled = false;
                    deleteLocalCell.IsEnabled = false;
                    break;
                case LcStatus.LcUnBuilded:
                    doCellPlan.IsEnabled = true;
                    deleteCellPlan.IsEnabled = true;
                    cancel.IsEnabled = false;
                    activeCell.IsEnabled = false;
                    deleteLocalCell.IsEnabled = false;
                    break;
                case LcStatus.LcBuilded:
                    doCellPlan.IsEnabled = false;
                    deleteCellPlan.IsEnabled = false;
                    cancel.IsEnabled = false;
                    activeCell.IsEnabled = false;
                    deleteLocalCell.IsEnabled = true;
                    break;
                case LcStatus.CellBuilded:
                    doCellPlan.IsEnabled = false;
                    deleteCellPlan.IsEnabled = false;
                    cancel.IsEnabled = false;
                    activeCell.IsEnabled = true;
                    deleteLocalCell.IsEnabled = false;
                    break;
                default:
                    break;
            }

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
	        if (obj != null)
	        {
		        ContextMenu test = obj.Parent as ContextMenu;
		        Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
		        int nCellNumber = nrRectCanvas.Children.IndexOf(targetRect);

		        var targetIp = CSEnbHelper.GetCurEnbAddr();
		        if (null == targetIp)
		        {
			        MessageBox.Show("尚未选中基站", "网络规划", MessageBoxButton.OK, MessageBoxImage.Error);
		        }

		        if (!NPCellOperator.DelLocalCell(nCellNumber, targetIp))
		        {
			        MessageBox.Show("删除本地小区失败", "网络规划", MessageBoxButton.OK, MessageBoxImage.Error);
			        return;
		        }

                // 成功，查询本地小区的状态，然后设置对应的颜色
                Rectangle rect = targetRect.Children[0] as Rectangle;
                var varStatus = NPCellOperator.GetLcStatus(nCellNumber, targetIp);
                SetCellColor(rect, varStatus);
            }
		}

        /// <summary>
        /// 去激活小区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveCell_Click(object sender, RoutedEventArgs e)
        {
            MenuItem obj = sender as MenuItem;
	        if (obj != null)
	        {
		        ContextMenu test = obj.Parent as ContextMenu;
		        Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
		        int nCellNumber = nrRectCanvas.Children.IndexOf(targetRect);
				var targetIp = CSEnbHelper.GetCurEnbAddr();
				if (null == targetIp)
				{
					MessageBox.Show("尚未选中基站", "网络规划", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
				}

		        if (!NPCellOperator.SetCellActiveTrigger(nCellNumber, targetIp, CellOperType.deactive))
                {
                    MessageBox.Show("取消失败");
                    return;
                }

                Rectangle rect = targetRect.Children[0] as Rectangle;
                var varStatus = NPCellOperator.GetLcStatus(nCellNumber, targetIp);
                SetCellColor(rect, varStatus);
                this.MyDesigner.g_cellPlaning.Remove(nCellNumber);
            }
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
	        if (obj != null)
	        {
		        ContextMenu test = obj.Parent as ContextMenu;
		        Grid targetRect = ContextMenuService.GetPlacementTarget(test) as Grid;
		        Rectangle rect = targetRect.Children[0] as Rectangle;

		        int nCellNumber = this.nrRectCanvas.Children.IndexOf(targetRect);
		        if (!NPCellOperator.CancelLcPlanOp(nCellNumber))
		        {
                    MessageBox.Show("取消失败");
                    return;
		        }

                // 取消成功，查询本地小区的状态，并设置颜色
                string strIP = CSEnbHelper.GetCurEnbAddr();
                if (strIP == null || strIP == "")
                {
                    MessageBox.Show("未选择基站");
                    return;
                }
                var varStatus = NPCellOperator.GetLcStatus(nCellNumber, strIP);
                SetCellColor(rect, varStatus);
                this.MyDesigner.g_cellPlaning.Remove(nCellNumber);
	        }
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

			int nCellNumber = this.nrRectCanvas.Children.IndexOf(targetRect);

	        if (!NPCellOperator.DelLcNetPlan(nCellNumber, CSEnbHelper.GetCurEnbAddr()))
	        {
		        return;
	        }

			Rectangle rect = targetRect.Children[0] as Rectangle;
			rect.Fill = new SolidColorBrush(Colors.Red);

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
                if (NPCellOperator.AddNewNrLc(nCellNumber, CSEnbHelper.GetCurEnbAddr()))
                {
                    this.MyDesigner.g_cellPlaning.Add(nCellNumber);
                }
            }
        }

        private void SetCellColor(Rectangle rect, LcStatus cellStatus)
        {
            switch (cellStatus)
            {
                case LcStatus.UnPlan:
                    rect.Fill = new SolidColorBrush(Colors.Red);
                    break;
                case LcStatus.Planning:
                    rect.Fill = new SolidColorBrush(Colors.LightGreen);
                    break;
                case LcStatus.LcUnBuilded:
                    rect.Fill = new SolidColorBrush(Colors.Yellow);
                    break;
                case LcStatus.LcBuilded:
                    rect.Fill = new SolidColorBrush(Colors.Blue);
                    break;
                case LcStatus.CellBuilded:
                    rect.Fill = new SolidColorBrush(Colors.LightBlue);
                    break;
                default:
                    break;
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
                if(targetCanvas.Children.Count > 1)
                {
                    return;
                }

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

                if(MyDesigner.g_AllDevInfo.ContainsKey(EnumDevType.board))
                {
                    MyDesigner.g_AllDevInfo[EnumDevType.board].Add(strIRName, newBoardInfo.m_strOidIndex);
                }
                else
                {
                    MyDesigner.g_AllDevInfo.Add(EnumDevType.board, new Dictionary<string, string>());
                    MyDesigner.g_AllDevInfo[EnumDevType.board].Add(strIRName, newBoardInfo.m_strOidIndex);
                }

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
                    designerItem.ItemName = strIRName + "-" + i;
                    designerItem.DevIndex = newBoardInfo.m_strOidIndex;

                    Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/NetElement.xml");
                    Stream stream = Application.GetResourceStream(strUri).Stream;

                    FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;
                    Object content = el.FindName("g_IR") as Grid;

                    string strXAML = XamlWriter.Save(content);

                    string strText = string.Format("Text=\"{0}\"", i);
                    strXAML = strXAML.Replace("Text=\"IR\"", strText);
                    Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML)));
                    designerItem.Content = testContent;

                    //获取 Canvas 相对于 DesignerCanvas 的位置，方便进行光口的添加

                    double CanvasLeft = DesignerCanvas.GetLeft(boardCanvas);
                    double CanvasTop = DesignerCanvas.GetTop(boardCanvas);

                    Canvas.SetLeft(designerItem, CanvasLeft + 60 + 240 * nColumn + i * 20);
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

                var labelTarget = targetCanvas.Children[1] as Label;
                //为每个光口构造一个名称，根据板卡插槽号和 boardName，也是板卡的名称
                string strIRName = string.Format("{0}-{1}", labelTarget.Content.ToString(), soltNum);

                var boardAttribute = MibInfoMgr.GetInstance().GetDevAttributeInfo(MyDesigner.g_AllDevInfo[EnumDevType.board][strIRName], EnumDevType.board);
                if (boardAttribute != null)
                {
                    MyDesigner.CreateGirdForNetInfo(strIRName, boardAttribute);
                    MyDesigner.g_nowDevAttr = boardAttribute;
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

            if(targetCanvas.Children.Count < 2)
            {
                return;
            }

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
                if (!MyDesigner.g_AllDevInfo[EnumDevType.board].ContainsKey(strIRName))
                {
                    MessageBox.Show("没有从设备字典中查找到该设备，请注意");
                    return;
                }
                if (!MibInfoMgr.GetInstance().DelDev(MyDesigner.g_AllDevInfo[EnumDevType.board][strIRName], EnumDevType.board))
                {
                    MessageBox.Show("从基站侧删除设备失败，请注意");
                    return;
                }

                for (int i = 1; i < MyDesigner.Children.Count; i++)
                {
                    if (MyDesigner.Children[i].GetType().Name == "DesignerItem")
                    {
                        DesignerItem item = MyDesigner.Children[i] as DesignerItem;
                        if ((item.DevIndex ?? "") == MyDesigner.g_AllDevInfo[EnumDevType.board][strIRName])
                        {
                            DeleteBoardIR(item);
                            i--;
                        }
                    }
                }

                MyDesigner.g_AllDevInfo[EnumDevType.board].Remove(strIRName);

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
		private bool InitNetPlan()
		{
			//初始化是否成功
			MibInfoMgr.GetInstance().Clear();

			var initResult = NPSnmpOperator.InitNetPlanInfo();

			// 剩下的工作全部推到UI线程中执行
			if (initResult)
			{
				var allNPInfo = MibInfoMgr.GetInstance().GetAllEnbInfo();

				//初始化板卡信息
				if (allNPInfo.Keys.Contains(EnumDevType.board))
				{
					InitBoardInfo(allNPInfo[EnumDevType.board]);
				}
				if (allNPInfo.Keys.Contains(EnumDevType.rru))
				{
					InitRruInfo(allNPInfo[EnumDevType.rru]);
				}
				if (allNPInfo.ContainsKey(EnumDevType.ant))
				{
					InitAntennaInfo(allNPInfo[EnumDevType.ant]);
				}
				if (allNPInfo.ContainsKey(EnumDevType.rhub))
				{
					InitRHUBInfo(allNPInfo[EnumDevType.rhub]);
				}

                InitCellStatus();

				return true;
			}

			return false;
		}

        #region 初始化小区状态

        private void InitCellStatus()
        {
            //获取当前基站的IP地址
            string strIP = CSEnbHelper.GetCurEnbAddr();

            if(strIP == null || strIP == "")
            {
                MessageBox.Show("未选择基站  InitCellStatus");
                return;
            }

            for(int i = 0; i < this.nrRectCanvas.Children.Count; i++)
            {
                Grid targetGrid = this.nrRectCanvas.Children[i] as Grid;
                Rectangle rect = targetGrid.Children[0] as Rectangle;

                var cellStatus = NPCellOperator.GetLcStatus(i, strIP);

                switch(cellStatus)
                {
                    case LcStatus.UnPlan:
                        rect.Fill = new SolidColorBrush(Colors.Red);
                        break;
                    case LcStatus.Planning:
                        rect.Fill = new SolidColorBrush(Colors.LightGreen);
      //                  if (!this.MyDesigner.g_cellPlaning.Contains(i))
						//{
						//	if (NPCellOperator.SetNetPlanSwitch(true, i, CSEnbHelper.GetCurEnbAddr()))
						//	{
						//		this.MyDesigner.g_cellPlaning.Add(i);
						//	}
						//}
						break;
                    case LcStatus.LcUnBuilded:
                        rect.Fill = new SolidColorBrush(Colors.Yellow);
                        break;
                    case LcStatus.LcBuilded:
                        rect.Fill = new SolidColorBrush(Colors.Blue);
                        break;
                    case LcStatus.CellBuilded:
                        rect.Fill = new SolidColorBrush(Colors.LightBlue);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region 初始化板卡

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

                    if((nPort >= 0) && (nPort < boardColumn * boardRow))
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

            if(MyDesigner.g_AllDevInfo.ContainsKey(EnumDevType.board))
            {
                if (!MyDesigner.g_AllDevInfo[EnumDevType.board].ContainsKey(strIRName))
                    MyDesigner.g_AllDevInfo[EnumDevType.board].Add(strIRName, strDevIndex);
            }
            else
            {
                MyDesigner.g_AllDevInfo.Add(EnumDevType.board, new Dictionary<string, string>());
                MyDesigner.g_AllDevInfo[EnumDevType.board].Add(strIRName, strDevIndex);
            }

            //填充光口，根据获取到的数量进行添加

            for (int i = 0; i < nIRNumber; i++)
            {
                DesignerItem designerItem = new DesignerItem();
                designerItem.ItemName = strIRName + "-" + i;
                designerItem.DevIndex = strDevIndex;
                designerItem.PortNo = i;

                Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/NetElement.xml");
                Stream stream = Application.GetResourceStream(strUri).Stream;

                FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;
                Object content = el.FindName("g_IR") as Grid;

                string strXAML = XamlWriter.Save(content);

                string strText = string.Format("Text=\"{0}\"", i);
                strXAML = strXAML.Replace("Text=\"IR\"", strText);

                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML)));
                designerItem.Content = testContent;

                //获取 Canvas 相对于 DesignerCanvas 的位置，方便进行光口的添加

                double CanvasLeft = DesignerCanvas.GetLeft(boardCanvas);
                double CanvasTop = DesignerCanvas.GetTop(boardCanvas);

                Canvas.SetLeft(designerItem, CanvasLeft + 60 + 240 * nColumn + i * 20);
                Canvas.SetTop(designerItem, CanvasTop + 12.5 + 40 * (boardRow - 1 - nRow));

                MyDesigner.Children.Add(designerItem);
                SetConnectorDecoratorTemplate(designerItem);
            }
        }

        /// <summary>
        /// 设置板卡光口号的属性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="nPortNo"></param>
        /// <returns></returns>
        private bool SetBoardConnector(DesignerItem item, int nPortNo)
        {
            if(item == null)
            {
                return false;
            }
            Control cd = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
            List<Connector> connectors = new List<Connector>();
            GetConnectors(cd, connectors);

            if(connectors != null && connectors.Count != 0)
            {
                connectors[0].PortNo = nPortNo;

                return true;
            }

            return false;
        }


        #endregion

        #region 初始化RRU

        /// <summary>
        /// 初始化rru
        /// </summary>
        /// <param name="listRRUInfo"></param>
        private void InitRruInfo(List<DevAttributeInfo> listRRUInfo)
        {
            if(listRRUInfo == null || listRRUInfo.Count == 0)
            {
                return;
            }

            foreach(DevAttributeInfo item in listRRUInfo)
            {
                //获取索引信息
                string strIndex = item.m_strOidIndex.Substring(1);
                int nRRUid;
                try
                {
                    nRRUid = int.Parse(strIndex);
                    MyDesigner.nRRUNo = nRRUid;
                }
                catch
                {
                    MessageBox.Show("索引解析失败");
                    return;
                }

                //获取类型信息
                string strRRUType = item.m_mapAttributes["netRRUTypeIndex"].m_strOriginValue;
                int nRRUType;
                try
                {
                    nRRUType = int.Parse(strRRUType);
                }
                catch
                {
                    MessageBox.Show("类型解析失败");
                    return;
                }

                //根据类型获取 rru 的属性
                var rruItemInfo = NPERruHelper.GetInstance().GetRruInfoByType(nRRUType);

                if(rruItemInfo == null)
                {
                    MessageBox.Show("获取rru信息失败");
                    return;
                }

                DesignerItem newItem = new DesignerItem();

                //从xml 中获取 rru 元素并创建
                string strXAML = string.Empty;
                Size newSize;
                string strName = rruItemInfo.rruTypeName + "-" + nRRUid.ToString();
                strXAML = MyDesigner.GetElementFromXAML(rruItemInfo.rruTypeNotMibMaxePortNo, strXAML, out newSize);
                string strXAML1 = string.Format("Text=\"{0}\"", strName);

                if(rruItemInfo.rruTypeNotMibMaxePortNo > 16)
                {
                    string strPortNo = string.Format("Text=\"1..{0}\"", rruItemInfo.rruTypeNotMibMaxePortNo);
                    strXAML = strXAML.Replace("Text=\"1\"", strPortNo);
                }
                strXAML = strXAML.Replace("Text=\"RRU\"", strXAML1);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML)));
                newItem.Content = testContent;
                newItem.ItemName = strName;
                newItem.NPathNumber = rruItemInfo.rruTypeNotMibMaxePortNo;
                newItem.DevType = EnumDevType.rru;
                newItem.DevIndex = item.m_strOidIndex;

                newItem.Width = newSize.Width;
                newItem.Height = newSize.Height;

                //全局设备信息添加
                if(MyDesigner.g_AllDevInfo.ContainsKey(EnumDevType.rru))
                {
                    if(MyDesigner.g_AllDevInfo[EnumDevType.rru].ContainsKey(strName))
                    {
                        MessageBox.Show("全局字典中已经包含该设备 rru，请注意  InitRRUInfo");
                        return;
                    }
                    MyDesigner.g_AllDevInfo[EnumDevType.rru].Add(strName, item.m_strOidIndex);
                }
                else
                {
                    MyDesigner.g_AllDevInfo.Add(EnumDevType.rru, new Dictionary<string, string>());
                    MyDesigner.g_AllDevInfo[EnumDevType.rru].Add(strName, item.m_strOidIndex);
                }

                Canvas.SetZIndex(newItem, MyDesigner.Children.Count);
                MyDesigner.Children.Add(newItem);
                SetConnectorDecoratorTemplate(newItem);

                MyDesigner.SelectionService.SelectItem(newItem);
                newItem.Focus();
                newItem.MouseDoubleClick += MyDesigner.NewItem_MouseDoubleClick;
                //创建属性
                MyDesigner.CreateGirdForNetInfo(strName, item);

                MyDesigner.gridProperty = this.gridProperty;
            }
        }
        #endregion

        #region    初始化天线阵

        /// <summary>
        /// 初始化天线阵
        /// </summary>
        /// <param name="listAntInfo"></param>
        private void InitAntennaInfo(List<DevAttributeInfo> listAntInfo)
        {
            if (listAntInfo == null || listAntInfo.Count == 0)
            {
                return;
            }

            //创建一个 dictionary 保存每个 rru 对应连接的设备
            Dictionary<int, int> allRRUToAnt = new Dictionary<int, int>();

            foreach (DevAttributeInfo item in listAntInfo)
            {
                //获取索引信息
                string strIndex = item.m_strOidIndex.Substring(1);
                int nAntID;
                try
                {
                    nAntID = int.Parse(strIndex);
                    MyDesigner.nAntennaNo = nAntID;
                }
                catch
                {
                    MessageBox.Show("索引解析失败");
                    return;
                }

                //获取天线阵的通道数
                int nPort;
                try
                {
                    nPort = int.Parse(item.m_mapAttributes["netAntArrayNum"].m_strOriginValue);
                }catch
                {
                    MessageBox.Show("获取天线阵通道数失败InitAntennaInfo");
                    return;
                }

                DesignerItem newItem = new DesignerItem();

                //从xml 中获取 ant 元素并创建
                string strXAML = string.Empty;
                Size newSize;
                string strName = "No:-" + nAntID;
                strXAML = MyDesigner.GetAntennaromXML(nPort, strXAML, out newSize);
                if (nPort > 8)
                {
                    string strPortNo = string.Format("Text=\"1..{0}\"", nPort);
                    strXAML = strXAML.Replace("Text=\"1\"", strPortNo);
                }
                string strXAML1 = string.Format("Text=\"{0}\"", strName);
                strXAML = strXAML.Replace("Text=\"Antenna\"", strXAML1);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML)));
                newItem.Content = testContent;
                newItem.ItemName = strName;
                newItem.NPathNumber = nPort;
                newItem.DevType = EnumDevType.ant;
                newItem.DevIndex = item.m_strOidIndex;

                newItem.Width = newSize.Width;
                newItem.Height = newSize.Height;

                Canvas.SetZIndex(newItem, MyDesigner.Children.Count);
                MyDesigner.Children.Add(newItem);
                SetConnectorDecoratorTemplate(newItem);

                MyDesigner.SelectionService.SelectItem(newItem);
                newItem.Focus();

                ////全局设备信息添加
                if (MyDesigner.g_AllDevInfo.ContainsKey(EnumDevType.ant))
                {
                    if(MyDesigner.g_AllDevInfo[EnumDevType.ant].ContainsKey(strName))
                    {
                        MessageBox.Show("全局字典中已经存在该 antenna，请注意 InitAntInfo");
                        return;
                    }
                    else
                    {
                        MyDesigner.g_AllDevInfo[EnumDevType.ant].Add(strName, item.m_strOidIndex);
                    }
                }
                else
                {
                    MyDesigner.g_AllDevInfo.Add(EnumDevType.ant, new Dictionary<string, string>());
                    MyDesigner.g_AllDevInfo[EnumDevType.ant].Add(strName, item.m_strOidIndex);
                }
                ////创建属性
                MyDesigner.CreateGirdForNetInfo(strName, item);

                MyDesigner.gridProperty = this.gridProperty;
            }
        }

        #endregion

        #region    初始化rHUB

        /// <summary>
        /// 初始化 rhub 设备信息
        /// </summary>
        /// <param name="listrHUBInfo"></param>
        private void InitRHUBInfo(List<DevAttributeInfo> listrHUBInfo)
        {
            if (listrHUBInfo == null || listrHUBInfo.Count == 0)
            {
                return;
            }

            //创建一个 dictionary 保存每个板卡对应连接的设备
            Dictionary<int, int> allBoardToDev = new Dictionary<int, int>();

            for (int i = 0; i < boardRow * boardColumn; i++)
            {
                allBoardToDev.Add(i, 0);
            }

            foreach (DevAttributeInfo item in listrHUBInfo)
            {
                //获取索引信息
                string strIndex = item.m_strOidIndex.Substring(1);
                int nRHUBid;
                try
                {
                    nRHUBid = int.Parse(strIndex);
                    MyDesigner.nrHUBNo = nRHUBid;
                }
                catch
                {
                    MessageBox.Show("索引解析失败");
                    return;
                }

                //获取类型信息
                string strrHUBType = item.m_mapAttributes["netRHUBType"].m_strOriginValue;

                if(strrHUBType == "null")
                {
                    strrHUBType = "rhub1.0";
                }

                int nMaxRHUBPath = strrHUBType == "rhub1.0" ? 2 : 4;

                DesignerItem newItem = new DesignerItem();

                //从xml 中获取 rru 元素并创建
                string strXAML = string.Empty;
                Size newSize;
                string strName = strrHUBType + "-" + nRHUBid.ToString();
                strXAML = MyDesigner.GetrHUBFromXML(nMaxRHUBPath, strXAML, out newSize);
                string strXAML1 = string.Format("Text=\"{0}\"", strName);
                strXAML = strXAML.Replace("Text=\"rHUB\"", strXAML1);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML)));
                newItem.Content = testContent;
                newItem.ItemName = strName;
                newItem.DevType = EnumDevType.rhub;
                newItem.DevIndex = item.m_strOidIndex;

                newItem.Width = newSize.Width;
                newItem.Height = newSize.Height;

                Canvas.SetZIndex(newItem, MyDesigner.Children.Count);
                MyDesigner.Children.Add(newItem);
                SetConnectorDecoratorTemplate(newItem);

                MyDesigner.SelectionService.SelectItem(newItem);
                newItem.Focus();

                //全局设备信息添加
                if (MyDesigner.g_AllDevInfo.ContainsKey(EnumDevType.rhub))
                {
                    if (MyDesigner.g_AllDevInfo[EnumDevType.rhub].ContainsKey(strName))
                    {
                        MessageBox.Show("全局字典中已经包含该设备 rhub，请注意  InitRHUBInfo");
                        return;
                    }
                    MyDesigner.g_AllDevInfo[EnumDevType.rhub].Add(strName, item.m_strOidIndex);
                }
                else
                {
                    MyDesigner.g_AllDevInfo.Add(EnumDevType.rhub, new Dictionary<string, string>());
                    MyDesigner.g_AllDevInfo[EnumDevType.rhub].Add(strName, item.m_strOidIndex);
                }
                //创建属性
                MyDesigner.CreateGirdForNetInfo(strName, item);

                MyDesigner.gridProperty = this.gridProperty;
            }
        }

        #endregion

        #region 初始化 连接

        private void InitAllConnection()
        {
            //获取全部连接信息
            var allLink = MibInfoMgr.GetInstance().GetLinks();

            //先初始化 板卡 到 rru 的连接
            if(allLink.ContainsKey(EnumDevType.board_rru))
            {
                foreach(WholeLink item in allLink[EnumDevType.board_rru])
                {
                    InitBoardToDev(item);
                }
            }
            if(allLink.ContainsKey(EnumDevType.board_rhub))
            {
                foreach(WholeLink item in allLink[EnumDevType.board_rhub])
                {
                    InitBoardToDev(item);
                }
            }
            if(allLink.ContainsKey(EnumDevType.rru_ant))
            {
                for(int i = 0; i < allLink[EnumDevType.rru_ant].Count; i++)
                {
                    if(i < allLink[EnumDevType.rru_ant].Count-1)
                    {
                        if (allLink[EnumDevType.rru_ant][i].m_dstEndPoint.strDevIndex.Equals(allLink[EnumDevType.rru_ant][i + 1].m_dstEndPoint.strDevIndex))
                        {
                            continue;
                        }
                    }
                    InitRRUToAnt(allLink[EnumDevType.rru_ant][i]);
                }
            }
            if(allLink.ContainsKey(EnumDevType.rhub_prru))
            {
                foreach(var item in allLink[EnumDevType.rhub_prru])
                {
                    InitRHUBToPRRU(item);
                }
            }
        }

        /// <summary>
        /// 初始化 板卡到 RRU 或者 RHUB 的连接
        /// </summary>
        /// <param name="linkBoardToDev"></param>
        private void InitBoardToDev(WholeLink linkBoardToDev)
        {
            //如果源设备是 board ，则目的设备就是 RRU 或者 RHUB，否则，如果源不是 board， 则目的设备就是 board
            if(linkBoardToDev.m_srcEndPoint.devType == EnumDevType.board)
            {
                Connector dstConnector = new Connector();
                Connector srcConnector = new Connector();

                string strDevName = string.Empty;
                //如果目的设备是 rru
                if (linkBoardToDev.m_dstEndPoint.devType == EnumDevType.rru)
                {
                    //对每一个连接，都要遍历在设备信息中是否存在该设备
                    foreach (string strName in MyDesigner.g_AllDevInfo[EnumDevType.rru].Keys)
                    {
                        //先找到 目标连接点
                        if (MyDesigner.g_AllDevInfo[EnumDevType.rru][strName].Equals(linkBoardToDev.m_dstEndPoint.strDevIndex))
                        {
                            dstConnector = FindDevConnector(strName, linkBoardToDev.m_dstEndPoint.nPortNo, EnumPortType.rru_to_other);
                            if (dstConnector == null)
                            {
                                MessageBox.Show("没有找到 m_dstEndPoint InitBoardToDev");
                                return;
                            }
                            strDevName = strName;
                            break;
                        }
                    }
                }else if(linkBoardToDev.m_dstEndPoint.devType == EnumDevType.rhub)
                {
                    //对每一个连接，都要遍历在设备信息中是否存在该设备
                    foreach (string strName in MyDesigner.g_AllDevInfo[EnumDevType.rhub].Keys)
                    {
                        //先找到 目标连接点
                        if (MyDesigner.g_AllDevInfo[EnumDevType.rhub][strName].Equals(linkBoardToDev.m_dstEndPoint.strDevIndex))
                        {
                            dstConnector = FindDevConnector(strName, linkBoardToDev.m_dstEndPoint.nPortNo, EnumPortType.rhub_to_other);
                            if (dstConnector == null)
                            {
                                MessageBox.Show("没有找到 m_dstEndPoint InitBoardToDev");
                                return;
                            }
                            strDevName = strName;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("该连接不支持连接在板卡上  InitBoardToDev");
                    return;
                }

                //查找板卡上的光口连接点信息，然后进行连线
                foreach (string strName in MyDesigner.g_AllDevInfo[EnumDevType.board].Keys)
                {
                    if (MyDesigner.g_AllDevInfo[EnumDevType.board][strName].Equals(linkBoardToDev.m_srcEndPoint.strDevIndex))
                    {
                        //遍历 MyDesigner 的所有孩子，找到 strName 所对应的设备
                        for (int i = 1; i < MyDesigner.Children.Count; i++)
                        {
                            if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                            {
                                DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                                if (targetItem.ItemName.Equals(strName + "-" + linkBoardToDev.m_srcEndPoint.nPortNo))
                                {
                                    Control cd = targetItem.Template.FindName("PART_ConnectorDecorator", targetItem) as Control;

                                    List<Connector> connectors = new List<Connector>();
                                    GetConnectors(cd, connectors);

                                    if (connectors.Count > 0)
                                    {
                                        srcConnector = connectors[0];

                                        //构造连接
                                        Connection newConnection = new Connection(srcConnector, dstConnector);
                                        Canvas.SetZIndex(newConnection, MyDesigner.Children.Count);
                                        MyDesigner.Children.Add(newConnection);
                                        ResetItemPosition(strDevName, linkBoardToDev.m_srcEndPoint.strDevIndex);
                                        newConnection.Source.PortType = linkBoardToDev.m_srcEndPoint.portType;
                                        newConnection.Sink.PortType = linkBoardToDev.m_dstEndPoint.portType;
                                    }
                                    else
                                    {
                                        MessageBox.Show("没有找到板卡信息");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ResetItemPosition(string strDevName, string strBoardIndex)
        {
            int nSlotNo = 0;

            try
            {
                nSlotNo = int.Parse(MibStringHelper.GetRealValueFromIndex(strBoardIndex, 3));
            }
            catch
            {
                MessageBox.Show("从板卡索引获取插槽号失败");
                return;
            }

            //遍历 MyDesigner 的所有孩子，找到 strDevName 所对应的设备
            for (int i = 1; i < MyDesigner.Children.Count; i++)
            {
                if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                {
                    DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                    if (targetItem.ItemName.Equals(strDevName))
                    {
                        int nColumn = nSlotNo / boardRow;
                        int nRow = nSlotNo % boardRow;

                        ////根据板卡的位置不同，向不同的方向添加 rHUB，分为 top, top-right, top-left, bottom, bottom-right, bottom-left
                        double uiTop = 0;
                        double uiLeft = 0;
                        if (nColumn == (boardColumn - 1))              //右侧
                        {
                            if (nRow == (boardRow - 1))          //上侧
                            {
                                double nTop = Canvas.GetTop(boardCanvas) - 20 - allBoardToDev[nSlotNo] * 20 - targetItem.Height;
                                uiTop = nTop;
                                uiLeft = Canvas.GetLeft(boardCanvas) + 240 + 20;
                            }
                            else if (nRow > (boardRow / 2))      //右上侧
                            {
                                double nTop = Canvas.GetTop(boardCanvas) - 20 - allBoardToDev[nSlotNo] * 20 + (40 * (boardRow - nRow));
                                uiTop = nTop;
                                uiLeft = Canvas.GetLeft(boardCanvas) + 480 + allBoardToDev[nSlotNo] * 20 + 20;
                            }
                            else if (nRow == 0)       //下侧
                            {
                                uiTop = Canvas.GetTop(boardCanvas) + 40 * boardRow + allBoardToDev[nSlotNo] * 20;
                                uiLeft = Canvas.GetLeft(boardCanvas) + 480 + allBoardToDev[nSlotNo] * 20 + 20;
                            }
                            else          //右下侧
                            {
                                double nTop = Canvas.GetTop(boardCanvas) + (40 * (boardRow - nRow - 1)) + allBoardToDev[nSlotNo] * 20;
                                uiTop = nTop;
                                uiLeft = Canvas.GetLeft(boardCanvas) + 480 + allBoardToDev[nSlotNo] * 20 + 20;
                            }
                        }
                        else           //左侧
                        {
                            if (nRow == 0)         //下侧
                            {
                                uiTop = Canvas.GetTop(boardCanvas) + 40 * boardRow + allBoardToDev[nSlotNo] * 20;
                                double nLeft = Canvas.GetLeft(boardCanvas) - 20 - allBoardToDev[nSlotNo] * 20;
                                uiLeft = nLeft;
                            }
                            else if (nRow > (boardRow / 2))          //左上侧
                            {
                                double nTop = Canvas.GetTop(boardCanvas) - 20 - allBoardToDev[nSlotNo] * 20;
                                uiTop = nTop;
                                double nLeft = Canvas.GetLeft(boardCanvas) - 20 - allBoardToDev[nSlotNo] * 20;
                                uiLeft = nLeft;
                            }
                            else if (nRow == (boardRow - 1))             //上侧
                            {
                                double nTop = Canvas.GetTop(boardCanvas) - 20 - allBoardToDev[nSlotNo] * 20;
                                uiTop = nTop;
                                uiLeft = Canvas.GetLeft(boardCanvas);
                            }
                            else                 //左下侧
                            {
                                double nTop = Canvas.GetTop(boardCanvas) + (40 * (boardRow - nRow - 1)) + allBoardToDev[nSlotNo] * 20;
                                uiTop = nTop;
                                double nLeft = Canvas.GetLeft(boardCanvas) - 20 - allBoardToDev[nSlotNo] * 20;
                                uiLeft = nLeft;
                            }
                        }

                        DesignerCanvas.SetTop(targetItem, uiTop);
                        DesignerCanvas.SetLeft(targetItem, uiLeft);

                        //对应的板卡设备数量 +1
                        allBoardToDev[nSlotNo]++;

                        break;
                    }
                }
            }
        }

        private void InitRRUToAnt(WholeLink linkRRUToAnt)

        {
            //如果目的设备是 rru
            if (linkRRUToAnt.m_srcEndPoint.devType == EnumDevType.rru)
            {
                Connector dstConnector = new Connector();
                Connector srcConnector = new Connector();

                DesignerItem rruItem = new DesignerItem();
                //对每一个连接，都要遍历在设备信息中是否存在该设备
                foreach (string strName in MyDesigner.g_AllDevInfo[EnumDevType.rru].Keys)
                {
                    //先找到 源连接点 即 RRU
                    if (MyDesigner.g_AllDevInfo[EnumDevType.rru][strName].Equals(linkRRUToAnt.m_srcEndPoint.strDevIndex))
                    {
                        int nPortNo = linkRRUToAnt.m_srcEndPoint.nPortNo > 16 ? 1 : linkRRUToAnt.m_srcEndPoint.nPortNo;
                        srcConnector = FindDevConnector(strName, nPortNo, EnumPortType.rru_to_ant);
                        if (srcConnector == null)
                        {
                            MessageBox.Show("没有找到rru InitRRUToAnt");
                            return;
                        }
                        for (int i = 1; i < MyDesigner.Children.Count; i++)
                        {
                            if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                            {
                                DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                                if (targetItem.ItemName.Equals(strName))
                                {
                                    rruItem = targetItem;
                                    break;
                                }
                            }
                        }
                        //跳出循环
                        break;
                    }
                }

                foreach (string strName in MyDesigner.g_AllDevInfo[EnumDevType.ant].Keys)
                {
                    //寻找  目标连接点  ant
                    if (MyDesigner.g_AllDevInfo[EnumDevType.ant][strName].Equals(linkRRUToAnt.m_dstEndPoint.strDevIndex))
                    {
                        int nPortNo = linkRRUToAnt.m_dstEndPoint.nPortNo > 16 ? 1 : linkRRUToAnt.m_dstEndPoint.nPortNo;
                        dstConnector = FindDevConnector(strName, nPortNo, EnumPortType.ant_to_rru);
                        if (dstConnector == null)
                        {
                            MessageBox.Show("没有找到ant InitRRUToAnt");
                            return;
                        }
                        //构造连接
                        Connection newConnection = new Connection(srcConnector, dstConnector);
                        Canvas.SetZIndex(newConnection, MyDesigner.Children.Count);
                        MyDesigner.Children.Add(newConnection);

                        for (int i = 1; i < MyDesigner.Children.Count; i++)
                        {
                            if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                            {
                                DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                                if (targetItem.ItemName.Equals(strName))
                                {
                                    DesignerCanvas.SetLeft(targetItem, DesignerCanvas.GetLeft(rruItem) + 120);
                                    DesignerCanvas.SetTop(targetItem, DesignerCanvas.GetTop(rruItem) - 20);
                                    break;
                                }
                            }
                        }

                        break;
                    }
                }
            }
            else
            {

            }

        }

        private void InitRHUBToPRRU(WholeLink linkrHUBToprru)
        {
            //如果目的设备是 rHUB
            if (linkrHUBToprru.m_srcEndPoint.devType == EnumDevType.rhub)
            {
                Connector dstConnector = new Connector();
                Connector srcConnector = new Connector();

                DesignerItem rhubItem = new DesignerItem();
                //对每一个连接，都要遍历在设备信息中是否存在该设备
                foreach (string strName in MyDesigner.g_AllDevInfo[EnumDevType.rhub].Keys)
                {
                    //先找到 源连接点 即 rHUB
                    if (MyDesigner.g_AllDevInfo[EnumDevType.rhub][strName].Equals(linkrHUBToprru.m_srcEndPoint.strDevIndex))
                    {
                        int nPortNo = linkrHUBToprru.m_srcEndPoint.nPortNo > 16 ? 1 : linkrHUBToprru.m_srcEndPoint.nPortNo;
                        srcConnector = FindDevConnector(strName, nPortNo, EnumPortType.rhub_to_pico);
                        if (srcConnector == null)
                        {
                            MessageBox.Show("没有找到 rhub InitRHUBToPRRU");
                            return;
                        }
                        for (int i = 1; i < MyDesigner.Children.Count; i++)
                        {
                            if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                            {
                                DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                                if (targetItem.ItemName.Equals(strName))
                                {
                                    rhubItem = targetItem;
                                    break;
                                }
                            }
                        }
                        //跳出循环
                        break;
                    }
                }

                foreach (string strName in MyDesigner.g_AllDevInfo[EnumDevType.rru].Keys)
                {
                    //寻找  目标连接点  rru
                    if (MyDesigner.g_AllDevInfo[EnumDevType.rru][strName].Equals(linkrHUBToprru.m_dstEndPoint.strDevIndex))
                    {
                        int nPortNo = linkrHUBToprru.m_dstEndPoint.nPortNo > 16 ? 1 : linkrHUBToprru.m_dstEndPoint.nPortNo;
                        dstConnector = FindDevConnector(strName, nPortNo, EnumPortType.rru_to_other);
                        if (dstConnector == null)
                        {
                            MessageBox.Show("没有找到 pico InitRHUBToPRRU");
                            return;
                        }
                        //构造连接
                        Connection newConnection = new Connection(srcConnector, dstConnector);
                        Canvas.SetZIndex(newConnection, MyDesigner.Children.Count);
                        MyDesigner.Children.Add(newConnection);

                        for (int i = 1; i < MyDesigner.Children.Count; i++)
                        {
                            if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                            {
                                DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                                if (targetItem.ItemName.Equals(strName))
                                {
                                    DesignerCanvas.SetLeft(targetItem, DesignerCanvas.GetLeft(rhubItem) + 220);
                                    DesignerCanvas.SetTop(targetItem, DesignerCanvas.GetTop(rhubItem) - 20);
                                    break;
                                }
                            }
                        }

                        break;
                    }
                }
            }
            else
            {

            }


        }

        private Connector FindDevConnector(string strName, int nPort, EnumPortType enumType)
        {
            //遍历 MyDesigner 的所有孩子，找到 strName 所对应的设备
            for (int i = 1; i < MyDesigner.Children.Count; i++)
            {
                if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                {
                    DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                    if (targetItem.ItemName.Equals(strName))
                    {
                        Control cd = targetItem.Template.FindName("PART_ConnectorDecorator", targetItem) as Control;

                        List<Connector> connectors = new List<Connector>();
                        GetConnectors(cd, connectors);

                        foreach (Connector connector in connectors)
                        {
                            if (connector.PortType == enumType && connector.PortNo == nPort)
                            {
                                return connector;
                            }
                            //else
                            //{
                            //    if (connector.ID.Equals(connectorType + 1))
                            //    {
                            //        return connector;
                            //    }
                            //}
                        }
                    }
                }
            }

            return null;
        }

        #endregion

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

        /// <summary>
        /// 构建连接点
        /// </summary>
        /// <param name="item"></param>
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

        /// <summary>
        /// 取消连接点
        /// </summary>
        /// <param name="item"></param>
        public void HiddenConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.Content is UIElement)
            {
                ControlTemplate template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 恢复连接
        /// </summary>
        /// <param name="item"></param>
        public void VisibilityConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.Content is UIElement)
            {
                ControlTemplate template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Visibility = Visibility;
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

            if(nSizeChangedNo < 3)
            {
                nSizeChangedNo++;
            }
            if (nSizeChangedNo == 1)
            {

                //MyDesigner.Width = MyDesigner.ActualWidth + 10;
                //MyDesigner.Height = MyDesigner.ActualHeight + 10;
            }
            if (nSizeChangedNo == 2)
            {
                InitAllConnection();
                DeleteAllItemConnector();
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

	        var choice = MessageBox.Show("是否同步下发天线阵权重信息？\r\n注意：同步下发天线阵权重信息耗时较长。", "网络规划", MessageBoxButton.YesNoCancel,
		        MessageBoxImage.Warning);
	        if (choice == MessageBoxResult.Cancel)
	        {
		        return;
	        }

	        var bDlWcb = (choice == MessageBoxResult.Yes);
	        if (!NPSnmpOperator.DistributeNetPlanData(bDlWcb))
	        {
		        MessageBox.Show("下发网络规划信息失败", "网络规划", MessageBoxButton.OK, MessageBoxImage.Error);
	        }

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



        private void DeleteAllItemConnector()
        {
            //遍历 MyDesigner 的所有孩子，找到 strName 所对应的设备
            for (int i = 1; i < MyDesigner.Children.Count; i++)
            {
                if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                {
                    DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                    HiddenConnectorDecoratorTemplate(targetItem);
                }
            }
        }

        private void VisibilityAllConnector()
        {
            //遍历 MyDesigner 的所有孩子，找到 strName 所对应的设备
            for (int i = 1; i < MyDesigner.Children.Count; i++)
            {
                if ((MyDesigner.Children[i].GetType() == typeof(DesignerItem)))
                {
                    DesignerItem targetItem = MyDesigner.Children[i] as DesignerItem;

                    VisibilityConnectorDecoratorTemplate(targetItem);
                }
            }
        }
        private void LineHandler(object sender, RoutedEventArgs e)
        {
            if(bHiddenLineConnector)     //如果连接点已经被取消，则重新打开
            {
                VisibilityAllConnector();
                bHiddenLineConnector = false;
            }
            else
            {
                DeleteAllItemConnector();
                bHiddenLineConnector = true;
            }
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
            if(!bInit)
            {
                bInit = true;
                InitNetPlan();
            }
        }

        /// <summary>
        /// 关闭网络规划按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutAnchorable_Closed(object sender, EventArgs e)
        {
            MyDesigner.g_AllDevInfo.Clear();
            MyDesigner.Children.Clear();
        }

        public void NetPlanClean()
        {
            MibInfoMgr.GetInstance().Clear();
        }

        #region 网规模板列表相关操作
        /// <summary>
        /// 保存网规模板文件路径和文件名
        /// </summary>
        private Dictionary<string, string> dicTemplateFile;
        //判断是否取消模板连接点
        private bool bTemHiddenLineConnector = true;

        /// <summary>
        ///创建网规模板列表
        /// </summary>
        private void CreateTemplateList()
        {
            //初始化从默认路径获取
            var path = FilePathHelper.GetNetPlanTempaltePath();
            FilePathHelper.CreateFolder(path);
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            TemplateFileList.Items.Clear();
            dicTemplateFile = new Dictionary<string, string>();

            foreach (FileInfo fileinfo in dirInfo.GetFiles())
            {
                TemplateFileList.Items.Add(fileinfo.Name);
                if(!dicTemplateFile.ContainsKey(fileinfo.Name))
                    dicTemplateFile.Add(fileinfo.Name,fileinfo.FullName);
            }                                   
        }

        private void TemplateListViewItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;

            string selectedFile = TemplateFileList.SelectedItem as string;
            string path = null;

            if (!string.IsNullOrEmpty(selectedFile))
            {
                if(dicTemplateFile.ContainsKey(selectedFile))
                    path = FilePathHelper.GetNetPlanTempaltePath() + selectedFile;

                if (path == null)
                    return;

                NPTemplate npTempalte = NetPlanTemplateInfo.GetInstance().GetTemplateInfoFromFile(path);

                if(npTempalte != null)
                {
                    //在网规页面：根据文件网元信息填充到当前网规中，后期处理

                    //在模板页面：清空当前模板窗口
                    MyDesignerTemplate.ClearTemInfo();                  
                    //绘制当前选中模板信息
                    MyDesignerTemplate.DrawTemplate(npTempalte);
                }              
            }
        }

        /// <summary>
        /// 导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemImportHandler(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                //Filter = "Json Files(*.json)|*.json"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var filename = openFileDialog.FileName;
                int index = filename.LastIndexOf("\\");
                filename = filename.Substring(index + 1);
                if (!dicTemplateFile.ContainsKey(filename))
                    dicTemplateFile.Add(filename, openFileDialog.FileName);

                //判断列表中是否存在
                bool isexist = false;
                foreach (string fname in TemplateFileList.Items)
                {
                    if (fname.Equals(filename))
                        isexist = true;
                }

                if (!isexist)
                    TemplateFileList.Items.Add(filename);
            }

            return;
        }
        /// <summary>
        /// 导出模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemExportHandler(object sender, RoutedEventArgs e)
        {
            MyDesignerTemplate.CreateTempalteToFile();
            CreateTemplateList();
        }

        private void VisibilityTemAllConnector()
        {
            for (int i = 1; i < MyDesignerTemplate.Children.Count; i++)
            {
                if ((MyDesignerTemplate.Children[i].GetType() == typeof(DesignerItem)))
                {
                    DesignerItem targetItem = MyDesignerTemplate.Children[i] as DesignerItem;

                    VisibilityConnectorDecoratorTemplate(targetItem);
                }
            }
        }

        private void DeleteTemAllItemConnector()
        {
            for (int i = 1; i < MyDesignerTemplate.Children.Count; i++)
            {
                if ((MyDesignerTemplate.Children[i].GetType() == typeof(DesignerItem)))
                {
                    DesignerItem targetItem = MyDesignerTemplate.Children[i] as DesignerItem;

                    HiddenConnectorDecoratorTemplate(targetItem);
                }
            }
        }
        private void TemLineHandler(object sender, RoutedEventArgs e)
        {
            if (bTemHiddenLineConnector)     //如果连接点已经被取消，则重新打开
            {
                VisibilityTemAllConnector();
                bTemHiddenLineConnector = false;
            }
            else
            {
                DeleteTemAllItemConnector();
                bTemHiddenLineConnector = true;
            }
        }

        private void TemClearHandler(object sender, RoutedEventArgs e)
        {
            MyDesignerTemplate.ClearTemInfo();
        }

        private void MyDesignerTemplate_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// 小区状态显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button targetBtn = sender as Button;

            Popup pop = new Popup();
            pop.PlacementTarget = targetBtn;
            pop.StaysOpen = false;
            pop.IsOpen = true;

            Border popBorder = new Border();
            popBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
            popBorder.BorderThickness = new Thickness(2);
            pop.Child = popBorder;

            Grid cellStatusGrid = new Grid();
            popBorder.Child = cellStatusGrid;
            cellStatusGrid.Background = new SolidColorBrush(Colors.White);

            //创建8行2列，添加小区状态描述
            ColumnDefinition strStatus = new ColumnDefinition();
            strStatus.Width = new GridLength(100);
            cellStatusGrid.ColumnDefinitions.Add(strStatus);
            ColumnDefinition colorStatus = new ColumnDefinition();
            colorStatus.Width = new GridLength(60);
            cellStatusGrid.ColumnDefinitions.Add(colorStatus);

            for (int i = 0; i < 15; i++)
            {
                RowDefinition newrow = new RowDefinition();

                if (i % 2 == 0)
                {
                    newrow.Height = new GridLength(30);
                    cellStatusGrid.RowDefinitions.Add(newrow);
                }
                else
                {
                    newrow.Height = GridLength.Auto;
                    cellStatusGrid.RowDefinitions.Add(newrow);

                    Grid newgrid = new Grid();
                    newgrid.Background = new SolidColorBrush(Colors.Gray);
                    newgrid.Height = 1;
                    cellStatusGrid.Children.Add(newgrid);
                    Grid.SetRow(newgrid, i);
                    Grid.SetColumnSpan(newgrid, 2);
                }
            }
            
            TextBlock strCellStatus = new TextBlock();
            strCellStatus.Text = "小区状态";
            strCellStatus.HorizontalAlignment = HorizontalAlignment.Center;
            strCellStatus.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(strCellStatus);
            Grid.SetColumnSpan(strCellStatus, 2);

            TextBlock notPlan = new TextBlock();
            notPlan.Text = "未规划";
            notPlan.HorizontalAlignment = HorizontalAlignment.Center;
            notPlan.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(notPlan);
            Grid.SetRow(notPlan, 2);
            Grid.SetColumn(notPlan, 0);

            Rectangle notPlanRect = new Rectangle();
            notPlanRect.Fill = new SolidColorBrush(Colors.Red);
            notPlanRect.Height = 30;
            cellStatusGrid.Children.Add(notPlanRect);
            Grid.SetRow(notPlanRect, 2);
            Grid.SetColumn(notPlanRect, 1);

            TextBlock planing = new TextBlock();
            planing.Text = "规划中";
            planing.HorizontalAlignment = HorizontalAlignment.Center;
            planing.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(planing);
            Grid.SetRow(planing, 4);
            Grid.SetColumn(planing, 0);

            Rectangle planingRect = new Rectangle();
            planingRect.Fill = new SolidColorBrush(Colors.LightGreen);
            planingRect.Height = 30;
            cellStatusGrid.Children.Add(planingRect);
            Grid.SetRow(planingRect, 4);
            Grid.SetColumn(planingRect, 1);

            TextBlock localCellNot = new TextBlock();
            localCellNot.Text = "本地小区未建";
            localCellNot.HorizontalAlignment = HorizontalAlignment.Center;
            localCellNot.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(localCellNot);
            Grid.SetRow(localCellNot, 6);
            Grid.SetColumn(localCellNot, 0);

            Rectangle localCellNotRect = new Rectangle();
            localCellNotRect.Fill = new SolidColorBrush(Colors.Yellow);
            localCellNotRect.Height = 30;
            cellStatusGrid.Children.Add(localCellNotRect);
            Grid.SetRow(localCellNotRect, 6);
            Grid.SetColumn(localCellNotRect, 1);

            TextBlock localCellOK = new TextBlock();
            localCellOK.Text = "本地小区已建";
            localCellOK.HorizontalAlignment = HorizontalAlignment.Center;
            localCellOK.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(localCellOK);
            Grid.SetRow(localCellOK, 8);
            Grid.SetColumn(localCellOK, 0);

            Rectangle localCellOKRect = new Rectangle();
            localCellOKRect.Fill = new SolidColorBrush(Colors.Blue);
            localCellOKRect.Height = 30;
            cellStatusGrid.Children.Add(localCellOKRect);
            Grid.SetRow(localCellOKRect, 8);
            Grid.SetColumn(localCellOKRect, 1);

            TextBlock CellOK = new TextBlock();
            CellOK.Text = "小区已建";
            CellOK.HorizontalAlignment = HorizontalAlignment.Center;
            CellOK.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(CellOK);
            Grid.SetRow(CellOK, 10);
            Grid.SetColumn(CellOK, 0);

            Rectangle CellOKRect = new Rectangle();
            CellOKRect.Fill = new SolidColorBrush(Colors.LightBlue);
            CellOKRect.Height = 30;
            cellStatusGrid.Children.Add(CellOKRect);
            Grid.SetRow(CellOKRect, 10);
            Grid.SetColumn(CellOKRect, 1);

            TextBlock strRRUAngAnt = new TextBlock();
            strRRUAngAnt.Text = "RRU与天线阵状态";
            strRRUAngAnt.HorizontalAlignment = HorizontalAlignment.Center;
            strRRUAngAnt.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(strRRUAngAnt);
            Grid.SetRow(strRRUAngAnt, 12);
            Grid.SetColumnSpan(strRRUAngAnt, 2);

            TextBlock notBelongCell = new TextBlock();
            notBelongCell.Text = "未归属小区";
            notBelongCell.HorizontalAlignment = HorizontalAlignment.Center;
            notBelongCell.VerticalAlignment = VerticalAlignment.Center;
            cellStatusGrid.Children.Add(notBelongCell);
            Grid.SetRow(notBelongCell, 14);
            Grid.SetColumn(notBelongCell, 0);

            Rectangle notBelongCellRect = new Rectangle();
            notBelongCellRect.Fill = new SolidColorBrush(Colors.DarkGray);
            notBelongCellRect.Height = 30;
            cellStatusGrid.Children.Add(notBelongCellRect);
            Grid.SetRow(notBelongCellRect, 14);
            Grid.SetColumn(notBelongCellRect, 1);

        }
    }
}
