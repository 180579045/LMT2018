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
using NetPlan;

using System.Windows.Controls.Primitives;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// RRUpoint2Cell.xaml 的交互逻辑
    /// </summary>
    public partial class RRUpoint2Cell : Window
    {
        //全局变量，保存 小区id
        private List<int> g_listCell;
        private int nMaxCellNumber = 0;
        private Dictionary<string, NPRruToCellInfo> g_allRRUToCellInfo;
        private string strRRUIndex = string.Empty;
        private int g_nRRUPort = 0;

        //快速配置时需要先保存 小区信息的TextBlock
        private Dictionary<int, TextBlock> g_allCellTextBlock = new Dictionary<int, TextBlock>();
        private List<int> g_listCellIDForFast = new List<int>();
        private int nFirstPort = 1;
        private int nLastPort = 2;

        public RRUpoint2Cell(int nRRUPoint, List<int> listCell, string strIndex)
        {
            InitializeComponent();

            //根据 RRU 端口数量初始化界面，这里的 list 应该传入从后台获取的
            InitRRUPoint2Cell(nRRUPoint, listCell, strIndex);
            g_listCell = listCell;
            strRRUIndex = strIndex;
            g_nRRUPort = nRRUPoint;
            nLastPort = nRRUPoint;
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="nRRUPoint"></param>
        /// <param name="listCell"></param>
        private void InitRRUPoint2Cell(int nRRUPoint, List<int> listCell, string strIndex)
        {
            if(nRRUPoint > 0)
            {
                //根据 rru 的index获取信息
                g_allRRUToCellInfo = MibInfoMgr.GetInstance().GetNetLcInfoByRruIndex(strIndex);

                if(g_allRRUToCellInfo.Count == 0 || g_allRRUToCellInfo == null)
                {
                    MessageBox.Show("没有获取到 rru 上小区的信息");
                    return;
                }

                //添加边框
                for (int i = 0; i < 5; i++)
                {
                    Border newBorder = new Border();
                    newBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
                    newBorder.BorderThickness = new Thickness(1);
                    MainGrid.Children.Add(newBorder);
                    Grid.SetColumn(newBorder, i);
                    Grid.SetRowSpan(newBorder, nRRUPoint);
                }
                for(int i = 1; i <= nRRUPoint; i++)
                {
                    if(!g_allRRUToCellInfo.ContainsKey(i.ToString()))
                    {
                        continue;
                    }
                    var currCellInfo = g_allRRUToCellInfo[i.ToString()];

                    RowDefinition rowItem = new RowDefinition();
                    rowItem.Height = GridLength.Auto;
                    MainGrid.RowDefinitions.Add(rowItem);

                    Border newBorder = new Border();
                    newBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
                    newBorder.BorderThickness = new Thickness(1);
                    MainGrid.Children.Add(newBorder);
                    Grid.SetRow(newBorder, i-1);
                    Grid.SetColumnSpan(newBorder, 5);

                    TextBlock rruPoint = new TextBlock();
                    rruPoint.Text = i.ToString();
                    rruPoint.HorizontalAlignment = HorizontalAlignment.Center;
                    rruPoint.Margin = new Thickness(5);
                    MainGrid.Children.Add(rruPoint);
                    Grid.SetColumn(rruPoint, 0);
                    Grid.SetRow(rruPoint, i-1);

                    TextBlock lteCellID = new TextBlock();
                    g_allCellTextBlock.Add(i, lteCellID);
                    lteCellID.PreviewMouseLeftButtonUp += LteCellID_PreviewMouseLeftButtonUp;
                    lteCellID.HorizontalAlignment = HorizontalAlignment.Stretch;
                    lteCellID.TextAlignment = TextAlignment.Center;
                    string strCellId = string.Empty;
                    if(currCellInfo.CellIdList.Count <= 0)
                    {
                        strCellId = "-";
                    }
                    else
                    {
                        for (int j = 0; j < currCellInfo.CellIdList.Count; j++)
                        {
                            strCellId += currCellInfo.CellIdList[j].cellId + ",";
                        }
                        strCellId = strCellId.TrimEnd(',');
                    }
                    lteCellID.Text = strCellId;
                    lteCellID.Margin = new Thickness(5);
                    MainGrid.Children.Add(lteCellID);
                    Grid.SetColumn(lteCellID, 1);
                    Grid.SetRow(lteCellID, i-1);

                    ComboBox radioPathDirection = new ComboBox();
                    radioPathDirection.HorizontalAlignment = HorizontalAlignment.Center;
                    //添加获取到的收发信息
                    foreach(var item in currCellInfo.SupportTxRxStatus)
                    {
                        radioPathDirection.Items.Add(item);
                    }

                    if(currCellInfo.RealTRx != null && currCellInfo.RealTRx != string.Empty)
                    {
                        if(radioPathDirection.Items.Contains(currCellInfo.RealTRx))
                        {
                            radioPathDirection.SelectedItem = currCellInfo.RealTRx;
                        }
                    }

                    radioPathDirection.Margin = new Thickness(5);
                    radioPathDirection.Width = 120;
                    radioPathDirection.SelectionChanged += RadioPathDirection_SelectionChanged;
                    MainGrid.Children.Add(radioPathDirection);
                    Grid.SetColumn(radioPathDirection, 2);
                    Grid.SetRow(radioPathDirection, i-1);

                    TextBlock supportFrequent = new TextBlock();
                    supportFrequent.Text = currCellInfo.SupportFreqBand;
                    supportFrequent.HorizontalAlignment = HorizontalAlignment.Center;
                    supportFrequent.Margin = new Thickness(5);
                    MainGrid.Children.Add(supportFrequent);
                    Grid.SetColumn(supportFrequent, 3);
                    Grid.SetRow(supportFrequent, i-1);
                }
            }
        }

        /// <summary>
        /// 收发信息选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioPathDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox targetCombo = sender as ComboBox;
            int nRow = Grid.GetRow(targetCombo) + 1;

            if(targetCombo.SelectedItem != null)
            {
                g_allRRUToCellInfo[nRow.ToString()].RealTRx = targetCombo.SelectedItem.ToString();
            }
        }

        /// <summary>
        /// 小区id 列 鼠标单击时弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LteCellID_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock targetItem = sender as TextBlock;
            nMaxCellNumber = 0;

            //尝试使用 popup 弹出
            Popup newpop = new Popup();
            newpop.PlacementTarget = targetItem;
            newpop.StaysOpen = false;
            newpop.IsOpen = true;
            newpop.Focus();

            //添加小区信息
            Border newborder = new Border();
            newborder.BorderBrush = new SolidColorBrush(Colors.Gray);
            newborder.BorderThickness = new Thickness(1);
            newpop.Child = newborder;

            Grid newgrid = new Grid();
            newborder.Child = newgrid;
            RowDefinition row1 = new RowDefinition();
            row1.Height = GridLength.Auto;
            newgrid.RowDefinitions.Add(row1);

            //标题，端口号
            TextBlock txtPoint = (TextBlock)MainGrid.Children[MainGrid.Children.IndexOf(targetItem) - 1];
            TextBlock strTitle = new TextBlock();
            strTitle.Text = "端口" + txtPoint.Text;
            newgrid.Children.Add(strTitle);
            strTitle.Margin = new Thickness(5);

            //添加已经配置到当前端口的小区
            string strHaveCellId = targetItem.Text;
            List<string> listStr = new List<string>();                         //保存当前已经存在的小区id
            if (strHaveCellId != "-")
            {
                if(strHaveCellId.Contains(","))
                {
                    string[] strList = strHaveCellId.Split(',');
                    listStr = strList.ToList<string>();

                    for (int i = 0; i < listStr.Count; i++)
                    {
                        if (g_listCell.Contains(int.Parse(listStr[i])))
                        {
                            continue;
                        }
                        RowDefinition newRow = new RowDefinition();
                        newRow.Height = GridLength.Auto;
                        newgrid.RowDefinitions.Add(newRow);

                        CheckBox newItem = new CheckBox();
                        newItem.Content = "NetLocalCell" + listStr[i];
                        newgrid.Children.Add(newItem);
                        Grid.SetRow(newItem, newgrid.RowDefinitions.Count-1);
                        newItem.Margin = new Thickness(5);
                        newItem.IsChecked = true;
                        newItem.IsEnabled = false;
                        nMaxCellNumber++;
                    }
                }
                else
                {
                    if (!g_listCell.Contains(int.Parse(strHaveCellId)))
                    {
                        RowDefinition newRow = new RowDefinition();
                        newRow.Height = GridLength.Auto;
                        newgrid.RowDefinitions.Add(newRow);

                        CheckBox newItem = new CheckBox();
                        newItem.Content = "NetLocalCell" + strHaveCellId;
                        newgrid.Children.Add(newItem);
                        Grid.SetRow(newItem, newgrid.RowDefinitions.Count - 1);
                        newItem.Margin = new Thickness(5);
                        newItem.IsChecked = true;
                        newItem.IsEnabled = false;
                        nMaxCellNumber++;
                    }
                    else
                    {
                        listStr.Add(strHaveCellId);
                    }
                }
            }

            for (int i = 0; i < g_listCell.Count; i++)
            {
                RowDefinition newRow = new RowDefinition();
                newRow.Height = GridLength.Auto;
                newgrid.RowDefinitions.Add(newRow);

                CheckBox newItem = new CheckBox();
                newItem.Content = "NetLocalCell" + g_listCell[i].ToString();
                newgrid.Children.Add(newItem);
                Grid.SetRow(newItem, newgrid.RowDefinitions.Count-1);
                newItem.Margin = new Thickness(5);

                if(listStr.Contains(g_listCell[i].ToString()))
                {
                    newItem.IsChecked = true;
                    nMaxCellNumber++;
                }
                newItem.Checked += NewItem_Checked;
                newItem.Unchecked += NewItem_Unchecked;
            }
            RowDefinition row2 = new RowDefinition();
            row2.Height = GridLength.Auto;
            newgrid.RowDefinitions.Add(row2);
            newgrid.Background = new SolidColorBrush(Colors.White);

            Button btnOK = new Button();
            btnOK.Content = "确定";
            btnOK.Width = 60;
            btnOK.Height = 25;
            btnOK.Click += BtnOK_Click;
            newgrid.Children.Add(btnOK);
            Grid.SetRow(btnOK, newgrid.RowDefinitions.Count-1);
            btnOK.Margin = new Thickness(5);
        }

        /// <summary>
        /// 取消选中某个小区，则从全局变量中删除该小区数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewItem_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox targetItem = sender as CheckBox;
            Grid targetGrid = targetItem.Parent as Grid;
            Border targetBorder = targetGrid.Parent as Border;
            Popup targetPop = targetBorder.Parent as Popup;
            TextBlock targetText = targetPop.PlacementTarget as TextBlock;

            string strCellID = targetItem.Content.ToString().Substring(12);
            int nPort = Grid.GetRow(targetText) + 1;

            foreach(var item in g_allRRUToCellInfo[nPort.ToString()].CellIdList)
            {
                if(item.cellId == strCellID)
                {
                    g_allRRUToCellInfo[nPort.ToString()].CellIdList.Remove(item);
                    nMaxCellNumber--;
                    targetItem.IsChecked = false;
                    return;
                }
            }            
        }

        /// <summary>
        /// 选中某个小区，给全局变量添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewItem_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox targetItem = sender as CheckBox;
            Grid targetGrid = targetItem.Parent as Grid;
            Border targetBorder = targetGrid.Parent as Border;
            Popup targetPop = targetBorder.Parent as Popup;
            TextBlock targetText = targetPop.PlacementTarget as TextBlock;

            if (nMaxCellNumber >= 3)
            {
                targetItem.IsChecked = false;
                nMaxCellNumber++;
                targetPop.StaysOpen = true;

                Popup newpop = new Popup();
                newpop.PlacementTarget = targetItem;
                Label newLB = new Label();
                newLB.Background = new SolidColorBrush(Colors.White);
                newLB.Content = "最大支持3个小区";
                newLB.Foreground = new SolidColorBrush(Colors.Red);
                newpop.Child = newLB;
                newpop.StaysOpen = false;
                newpop.IsOpen = true;
                targetPop.StaysOpen = false;
                return;
            }

            nMaxCellNumber++;
            targetItem.IsChecked = true;

            string strCellID = targetItem.Content.ToString().Substring(12);

            CellAndState newItem = new CellAndState();
            newItem.cellId = strCellID;
            newItem.bIsFixed = false;

            int nPort = Grid.GetRow(targetText) + 1;

            g_allRRUToCellInfo[nPort.ToString()].CellIdList.Add(newItem);
        }
        
        /// <summary>
        /// 确定选择小区id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Button targetBtn = sender as Button;
            Grid targetGrid = targetBtn.Parent as Grid;
            Border targetBorder = targetGrid.Parent as Border;
            Popup targetPop = targetBorder.Parent as Popup;

            TextBlock targetText = targetPop.PlacementTarget as TextBlock;

            int nPort = Grid.GetRow(targetText) + 1;

            string strText = string.Empty;
            foreach (var item in g_allRRUToCellInfo[nPort.ToString()].CellIdList)
            {
                strText += item.cellId + ",";
            }

            targetText.Text = strText.TrimEnd(',');

            targetPop.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!MibInfoMgr.GetInstance().SetNetLcInfo(strRRUIndex, g_allRRUToCellInfo))
            {
                MessageBox.Show("设置失败");
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 快速配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(g_listCellIDForFast != null && g_listCellIDForFast.Count > 0)
            {
                g_listCellIDForFast.Clear();
            }
            Button targetItem = sender as Button;

            //尝试使用 popup 弹出
            Popup newpop = new Popup();
            newpop.PlacementTarget = targetItem;
            newpop.StaysOpen = false;
            newpop.IsOpen = true;
            newpop.Focus();
            
            //添加内容
            Border newborder = new Border();
            newborder.BorderBrush = new SolidColorBrush(Colors.Gray);
            newborder.BorderThickness = new Thickness(1);
            newborder.Background = new SolidColorBrush(Colors.White);
            newpop.Child = newborder;

            GroupBox mainGroup = new GroupBox();
            mainGroup.Header = "NR快速配置端口属性";
            mainGroup.Background = new SolidColorBrush(Colors.White);
            newborder.Child = mainGroup;

            Grid newgrid = new Grid();
            mainGroup.Content = newgrid;

            //分为两列，一列显示端口信息，一列显示小区信息
            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(300);
            newgrid.ColumnDefinitions.Add(column1);
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(220);
            newgrid.ColumnDefinitions.Add(column2);

            //构造左侧的端口信息界面
            Grid grid_PortInfo = new Grid();
            newgrid.Children.Add(grid_PortInfo);
            Grid.SetColumn(grid_PortInfo, 0);

            RowDefinition row1 = new RowDefinition();
            row1.Height = GridLength.Auto;
            grid_PortInfo.RowDefinitions.Add(row1);

            RowDefinition row2 = new RowDefinition();
            row2.Height = GridLength.Auto;
            grid_PortInfo.RowDefinitions.Add(row2);

            RowDefinition row3 = new RowDefinition();
            row3.Height = GridLength.Auto;
            grid_PortInfo.RowDefinitions.Add(row3);

            //放置  端口信息的 stackpanel
            StackPanel stackPort = new StackPanel();
            grid_PortInfo.Children.Add(stackPort);
            stackPort.Orientation = Orientation.Horizontal;

            Label lbPortText = new Label();
            lbPortText.Content = "端口（例如：1-8）";
            lbPortText.Width = 120;
            lbPortText.Height = 25;
            lbPortText.HorizontalAlignment = HorizontalAlignment.Left;
            lbPortText.Margin = new Thickness(5);
            stackPort.Children.Add(lbPortText);

            TextBox txtFirstPort = new TextBox();
            txtFirstPort.Text = "1";
            txtFirstPort.Margin = new Thickness(5);
            txtFirstPort.Width = 30;
            txtFirstPort.Height = 25;
            txtFirstPort.TextChanged += TxtFirstPort_TextChanged;
            stackPort.Children.Add(txtFirstPort);

            Label lbLine = new Label();
            lbLine.Content = "-";
            lbLine.Width = 20;
            lbLine.Height = 25;
            lbLine.Margin = new Thickness(0);
            stackPort.Children.Add(lbLine);

            TextBox txtEndPort = new TextBox();
            txtEndPort.Text = g_nRRUPort.ToString();
            txtEndPort.Margin = new Thickness(5);
            txtEndPort.Width = 30;
            txtEndPort.Height = 25;
            txtEndPort.TextChanged += TxtEndPort_TextChanged;
            stackPort.Children.Add(txtEndPort);

            Label lbText = new Label();
            lbText.Width = 290;
            lbText.Height = 25;
            lbText.HorizontalAlignment = HorizontalAlignment.Left;
            lbText.Margin = new Thickness(5);
            lbText.Content = "说明：此功能用来批量配置多个通道归属一样的小区";
            grid_PortInfo.Children.Add(lbText);
            Grid.SetRow(lbText, 1);

            StackPanel stackBTN = new StackPanel();
            stackBTN.Orientation = Orientation.Horizontal;
            grid_PortInfo.Children.Add(stackBTN);
            Grid.SetRow(stackBTN, 2);

            Button btnCancel = new Button();
            btnCancel.Click += BtnCancel_Click; ;
            btnCancel.Content = "取消";
            btnCancel.Width = 80;
            btnCancel.Height = 25;
            btnCancel.HorizontalAlignment = HorizontalAlignment.Right;
            btnCancel.Margin = new Thickness(10);
            stackBTN.Children.Add(btnCancel);

            Button btnOK = new Button();
            btnOK.Click += BtnOK_Click1;
            btnOK.Content = "确定";
            btnOK.Width = 80;
            btnOK.Height = 25;
            btnOK.HorizontalAlignment = HorizontalAlignment.Right;
            btnOK.Margin = new Thickness(10);
            stackBTN.Children.Add(btnOK);

            //构造小区信息
            Grid grid_cellInfo = new Grid();
            newgrid.Children.Add(grid_cellInfo);
            Grid.SetColumn(grid_cellInfo, 1);

            ColumnDefinition column3 = new ColumnDefinition();
            column3.Width = new GridLength(70);
            grid_cellInfo.ColumnDefinitions.Add(column3);
            ColumnDefinition column4 = new ColumnDefinition();
            column4.Width = new GridLength(130);
            grid_cellInfo.ColumnDefinitions.Add(column4);

            Label lbCellText = new Label();
            lbCellText.Content = "归属小区";
            lbCellText.VerticalAlignment = VerticalAlignment.Center;
            grid_cellInfo.Children.Add(lbCellText);

            Grid cellGrid = new Grid();
            grid_cellInfo.Children.Add(cellGrid);
            Grid.SetColumn(cellGrid, 1);
            for(int i = 0; i < g_listCell.Count; i++)
            {
                RowDefinition newRow = new RowDefinition();
                newRow.Height = GridLength.Auto;
                cellGrid.RowDefinitions.Add(newRow);

                CheckBox newItem = new CheckBox();
                newItem.Content = "NetLocalCell" + g_listCell[i].ToString();
                cellGrid.Children.Add(newItem);
                newItem.Checked += NewItem_Checked1;
                newItem.Unchecked += NewItem_Unchecked1;
                Grid.SetRow(newItem, cellGrid.RowDefinitions.Count - 1);
                newItem.Margin = new Thickness(5);
            }
        }

        private void NewItem_Unchecked1(object sender, RoutedEventArgs e)
        {
            CheckBox targetItem = sender as CheckBox;
            g_listCellIDForFast.Remove(int.Parse(targetItem.Content.ToString().Substring(12)));
        }

        private void NewItem_Checked1(object sender, RoutedEventArgs e)
        {
            CheckBox targetItem = sender as CheckBox;
            if (g_listCellIDForFast.Count >= 3)
            {
                Grid cellGrid = targetItem.Parent as Grid;
                Grid cellInfo = cellGrid.Parent as Grid;
                Grid newGrid = cellInfo.Parent as Grid;
                GroupBox grp = newGrid.Parent as GroupBox;
                Border bord = grp.Parent as Border;
                Popup targetPop = bord.Parent as Popup;
                targetPop.StaysOpen = true;
                targetItem.IsChecked = false;

                Popup newpop = new Popup();
                newpop.PlacementTarget = targetItem;
                Label newLB = new Label();
                newLB.Background = new SolidColorBrush(Colors.White);
                newLB.Content = "最大支持3个小区";
                newLB.Foreground = new SolidColorBrush(Colors.Red);
                newpop.Child = newLB;
                newpop.StaysOpen = false;
                newpop.IsOpen = true;

                targetPop.StaysOpen = false;
                return;
            }
            g_listCellIDForFast.Add(int.Parse(targetItem.Content.ToString().Substring(12)));
        }

        /// <summary>
        /// 终止端口信息改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtEndPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox targetItem = sender as TextBox;

            try
            {
                nLastPort = int.Parse(targetItem.Text);

                if (nLastPort <= nFirstPort || nLastPort >= g_nRRUPort)
                {
                    StackPanel cellGrid = targetItem.Parent as StackPanel;
                    Grid cellInfo = cellGrid.Parent as Grid;
                    Grid newGrid = cellInfo.Parent as Grid;
                    GroupBox grp = newGrid.Parent as GroupBox;
                    Border bord = grp.Parent as Border;
                    Popup targetPop = bord.Parent as Popup;
                    targetPop.StaysOpen = true;
                    Popup newpop = new Popup();
                    newpop.PlacementTarget = targetItem;
                    Label newLB = new Label();
                    newLB.Background = new SolidColorBrush(Colors.White);
                    newLB.Content = "请输入合法数字";
                    newLB.Foreground = new SolidColorBrush(Colors.Red);
                    newpop.Child = newLB;
                    newpop.StaysOpen = false;
                    newpop.IsOpen = true;
                    targetPop.StaysOpen = false;
                    targetItem.Text = g_nRRUPort.ToString();
                }
            }
            catch
            {
                StackPanel cellGrid = targetItem.Parent as StackPanel;
                Grid cellInfo = cellGrid.Parent as Grid;
                Grid newGrid = cellInfo.Parent as Grid;
                GroupBox grp = newGrid.Parent as GroupBox;
                Border bord = grp.Parent as Border;
                Popup targetPop = bord.Parent as Popup;
                targetPop.StaysOpen = true;
                Popup newpop = new Popup();
                newpop.PlacementTarget = targetItem;
                Label newLB = new Label();
                newLB.Background = new SolidColorBrush(Colors.White);
                newLB.Content = "请输入合法数字";
                newLB.Foreground = new SolidColorBrush(Colors.Red);
                newpop.Child = newLB;
                newpop.StaysOpen = false;
                newpop.IsOpen = true;
                targetPop.StaysOpen = false;
                targetItem.Text = g_nRRUPort.ToString();
            }
        }

        /// <summary>
        ///起始端口信息改变事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFirstPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox targetItem = sender as TextBox;

            try
            {
                nFirstPort = int.Parse(targetItem.Text);

                if(nFirstPort <= 0 || nFirstPort >= nLastPort)
                {
                    StackPanel cellGrid = targetItem.Parent as StackPanel;
                    Grid cellInfo = cellGrid.Parent as Grid;
                    Grid newGrid = cellInfo.Parent as Grid;
                    GroupBox grp = newGrid.Parent as GroupBox;
                    Border bord = grp.Parent as Border;
                    Popup targetPop = bord.Parent as Popup;
                    targetPop.StaysOpen = true;
                    Popup newpop = new Popup();
                    newpop.PlacementTarget = targetItem;
                    Label newLB = new Label();
                    newLB.Background = new SolidColorBrush(Colors.White);
                    newLB.Content = "请输入合法数字";
                    newLB.Foreground = new SolidColorBrush(Colors.Red);
                    newpop.Child = newLB;
                    newpop.StaysOpen = false;
                    newpop.IsOpen = true;
                    targetPop.StaysOpen = false;
                    targetItem.Text = "1";
                }
            }catch
            {
                StackPanel cellGrid = targetItem.Parent as StackPanel;
                Grid cellInfo = cellGrid.Parent as Grid;
                Grid newGrid = cellInfo.Parent as Grid;
                GroupBox grp = newGrid.Parent as GroupBox;
                Border bord = grp.Parent as Border;
                Popup targetPop = bord.Parent as Popup;
                targetPop.StaysOpen = true;
                Popup newpop = new Popup();
                newpop.PlacementTarget = targetItem;
                Label newLB = new Label();
                newLB.Background = new SolidColorBrush(Colors.White);
                newLB.Content = "请输入合法数字";
                newLB.Foreground = new SolidColorBrush(Colors.Red);
                newpop.Child = newLB;
                newpop.StaysOpen = false;
                newpop.IsOpen = true;
                targetPop.StaysOpen = false;
                targetItem.Text = "1";
            }
        }

        /// <summary>
        /// 快速配置取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Button targetBtn = sender as Button;
            StackPanel cellGrid = targetBtn.Parent as StackPanel;
            Grid cellInfo = cellGrid.Parent as Grid;
            Grid newGrid = cellInfo.Parent as Grid;
            GroupBox grp = newGrid.Parent as GroupBox;
            Border bord = grp.Parent as Border;
            Popup targetPop = bord.Parent as Popup;
            targetPop.IsOpen = false;
        }

        /// <summary>
        /// 快速配置界面的确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click1(object sender, RoutedEventArgs e)
        {
            Button targetBtn = sender as Button;
            StackPanel cellGrid = targetBtn.Parent as StackPanel;
            Grid cellInfo = cellGrid.Parent as Grid;
            Grid newGrid = cellInfo.Parent as Grid;
            GroupBox grp = newGrid.Parent as GroupBox;
            Border bord = grp.Parent as Border;
            Popup targetPop = bord.Parent as Popup;

            if (g_listCellIDForFast == null )
            {
                targetPop.IsOpen = false;
                return;
            }

            for (int i = nFirstPort; i <= nLastPort; i++)
            {
                TextBlock targetItem = g_allCellTextBlock[i];

                List<CellAndState> listToRemove = new List<CellAndState>();
                List<CellAndState> listToAdd = new List<CellAndState>();

                //先判断是否有需要移除的选项
                foreach(var item in g_allRRUToCellInfo[i.ToString()].CellIdList)
                {
                    if(!item.bIsFixed)
                    {
                        listToRemove.Add(item);
                    }
                }

                if(listToRemove.Count > 0)
                {
                    foreach (var item in listToRemove)
                        g_allRRUToCellInfo[i.ToString()].CellIdList.Remove(item);
                }

                //移除之后，进行添加
                foreach(var item in g_listCellIDForFast)
                {
                    CellAndState newItem = new CellAndState();
                    newItem.bIsFixed = false;
                    newItem.cellId = item.ToString();

                    g_allRRUToCellInfo[i.ToString()].CellIdList.Add(newItem);
                }

                if(g_allRRUToCellInfo[i.ToString()].CellIdList.Count > 3)
                {
                    int nToRemove = g_allRRUToCellInfo[i.ToString()].CellIdList.Count - 3;
                    g_allRRUToCellInfo[i.ToString()].CellIdList.RemoveRange(3, nToRemove);
                }

                string strText = string.Empty;
                foreach (var item in g_allRRUToCellInfo[i.ToString()].CellIdList)
                {
                    strText += item.cellId + ",";
                }

                if(strText == "")
                {
                    targetItem.Text = "-";
                }
                else
                {
                    targetItem.Text = strText.TrimEnd(',');
                }
            }
            targetPop.IsOpen = false;
        }

        /// <summary>
        /// 重置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= g_nRRUPort; i++)
            {
                TextBlock targetItem = g_allCellTextBlock[i];

                List<CellAndState> listToRemove = new List<CellAndState>();

                //先判断是否有需要移除的选项
                foreach (var item in g_allRRUToCellInfo[i.ToString()].CellIdList)
                {
                    if (!item.bIsFixed)
                    {
                        listToRemove.Add(item);
                    }
                }

                if (listToRemove.Count > 0)
                {
                    foreach (var item in listToRemove)
                        g_allRRUToCellInfo[i.ToString()].CellIdList.Remove(item);
                }

                string strText = string.Empty;
                foreach (var item in g_allRRUToCellInfo[i.ToString()].CellIdList)
                {
                    strText += item.cellId + ",";
                }

                if (strText == "")
                {
                    targetItem.Text = "-";
                }
                else
                {
                    targetItem.Text = strText.TrimEnd(',');
                }
            }
        }
    }
}
