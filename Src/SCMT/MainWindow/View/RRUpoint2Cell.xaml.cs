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

        public RRUpoint2Cell(int nRRUPoint, List<int> listCell, string strIndex)
        {
            InitializeComponent();

            //根据 RRU 端口数量初始化界面，这里的 list 应该传入从后台获取的
            InitRRUPoint2Cell(nRRUPoint, listCell, strIndex);
            g_listCell = listCell;
            strRRUIndex = strIndex;
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
                    Grid.SetRowSpan(newBorder, nRRUPoint + 1);
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
                    Grid.SetRow(newBorder, i);
                    Grid.SetColumnSpan(newBorder, 5);

                    TextBlock rruPoint = new TextBlock();
                    rruPoint.Text = i.ToString();
                    rruPoint.HorizontalAlignment = HorizontalAlignment.Center;
                    rruPoint.Margin = new Thickness(5);
                    MainGrid.Children.Add(rruPoint);
                    Grid.SetColumn(rruPoint, 0);
                    Grid.SetRow(rruPoint, i);

                    TextBlock lteCellID = new TextBlock();
                    lteCellID.PreviewMouseLeftButtonUp += LteCellID_PreviewMouseLeftButtonUp;
                    lteCellID.HorizontalAlignment = HorizontalAlignment.Stretch;
                    lteCellID.TextAlignment = TextAlignment.Center;
                    string strCellId = "-";
                    for(int j = 0; j < currCellInfo.CellIdList.Count; j++)
                    {
                        if(currCellInfo.CellIdList[j].bIsFixed)
                        {
                            if (j == 0)
                            {
                                strCellId = currCellInfo.CellIdList[j].cellId;
                            }
                            else
                            {
                                strCellId = strCellId + "," + currCellInfo.CellIdList[j].cellId;
                            }
                        }
                    }
                    lteCellID.Text = strCellId;
                    lteCellID.Margin = new Thickness(5);
                    MainGrid.Children.Add(lteCellID);
                    Grid.SetColumn(lteCellID, 1);
                    Grid.SetRow(lteCellID, i);

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
                    Grid.SetRow(radioPathDirection, i);

                    TextBlock supportFrequent = new TextBlock();
                    supportFrequent.Text = currCellInfo.SupportFreqBand;
                    supportFrequent.HorizontalAlignment = HorizontalAlignment.Center;
                    supportFrequent.Margin = new Thickness(5);
                    MainGrid.Children.Add(supportFrequent);
                    Grid.SetColumn(supportFrequent, 3);
                    Grid.SetRow(supportFrequent, i);
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
            int nRow = Grid.GetRow(targetCombo);

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
            //newpop.Width = 150;
            //newpop.Height = 200;

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

        private void NewItem_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox targetItem = sender as CheckBox;
            nMaxCellNumber--;
            targetItem.IsChecked = false;
        }

        private void NewItem_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox targetItem = sender as CheckBox;
            if(nMaxCellNumber >= 3)
            {
                targetItem.IsChecked = false;
                nMaxCellNumber++;
                CheckBox targetBtn = sender as CheckBox;
                Grid targetGrid = targetBtn.Parent as Grid;
                Border targetBorder = targetGrid.Parent as Border;
                Popup targetPop = targetBorder.Parent as Popup;
                targetPop.StaysOpen = true;
                MessageBox.Show("最大支持3个小区", "提示", MessageBoxButton.OK);
                targetPop.Focus();
                targetPop.StaysOpen = false;
                return;
            }

            nMaxCellNumber++;
            targetItem.IsChecked = true;
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

            int nRow = Grid.GetRow(targetText);
            List<CellAndState> listCellNew = new List<CellAndState>();

            string strText = string.Empty;
            foreach(var item in targetGrid.Children)
            {
                if(item.GetType() == typeof(CheckBox))
                {
                    CheckBox targetCB = item as CheckBox;
                    if(targetCB.IsChecked == true)
                    {
                        string strCellId = targetCB.Content.ToString().Substring(12);
                        strText = strText + strCellId + ",";
                        for(int i = 0; i < g_allRRUToCellInfo[nRow.ToString()].CellIdList.Count; i++)
                        {
                            var cellItem = g_allRRUToCellInfo[nRow.ToString()].CellIdList[i];
                            if (cellItem.cellId != strCellId)
                            {
                                CellAndState newItem = new CellAndState();
                                newItem.cellId = strCellId;
                                newItem.bIsFixed = false;
                                listCellNew.Add(newItem);
                            }
                        }
                    }
                }
            }

            targetText.Text = strText.TrimEnd(',');

            if(listCellNew != null && listCellNew.Count != 0)
            {
                foreach(var item in listCellNew)
                {
                    g_allRRUToCellInfo[nRow.ToString()].CellIdList.Add(item);
                }
            }

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

        }
    }
}
