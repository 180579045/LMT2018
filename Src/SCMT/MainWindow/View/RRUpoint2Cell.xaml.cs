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

        public RRUpoint2Cell(int nRRUPoint, List<int> listCell)
        {
            InitializeComponent();

            //根据 RRU 端口数量初始化界面，这里的 list 应该传入从后台获取的
            InitRRUPoint2Cell(nRRUPoint, listCell);
            g_listCell = listCell;
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="nRRUPoint"></param>
        /// <param name="listCell"></param>
        private void InitRRUPoint2Cell(int nRRUPoint, List<int> listCell)
        {
            if(nRRUPoint > 0)
            {
                //添加边框
                for(int i = 0; i < 5; i++)
                {
                    Border newBorder = new Border();
                    newBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
                    newBorder.BorderThickness = new Thickness(1);
                    MainGrid.Children.Add(newBorder);
                    Grid.SetColumn(newBorder, i);
                    Grid.SetRowSpan(newBorder, nRRUPoint + 1);
                }
                for(int i = 0; i < nRRUPoint; i++)
                {
                    RowDefinition rowItem = new RowDefinition();
                    rowItem.Height = GridLength.Auto;
                    MainGrid.RowDefinitions.Add(rowItem);

                    Border newBorder = new Border();
                    newBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
                    newBorder.BorderThickness = new Thickness(1);
                    MainGrid.Children.Add(newBorder);
                    Grid.SetRow(newBorder, i + 1);
                    Grid.SetColumnSpan(newBorder, 5);

                    TextBlock rruPoint = new TextBlock();
                    rruPoint.Text = i.ToString();
                    rruPoint.HorizontalAlignment = HorizontalAlignment.Center;
                    rruPoint.Margin = new Thickness(5);
                    MainGrid.Children.Add(rruPoint);
                    Grid.SetColumn(rruPoint, 0);
                    Grid.SetRow(rruPoint, i + 1);

                    TextBlock lteCellID = new TextBlock();
                    lteCellID.PreviewMouseLeftButtonUp += LteCellID_PreviewMouseLeftButtonUp;
                    lteCellID.HorizontalAlignment = HorizontalAlignment.Stretch;
                    lteCellID.TextAlignment = TextAlignment.Center;
                    string strCellId = string.Empty;
                    for(int j = 0; j < listCell.Count; j++)
                    {
                        if(j == 0)
                        {
                            strCellId = listCell[0].ToString();
                        }
                        else
                        {
                            strCellId = strCellId + "," + listCell[j].ToString();
                        }

                    }
                    lteCellID.Text = strCellId;
                    lteCellID.Margin = new Thickness(5);
                    MainGrid.Children.Add(lteCellID);
                    Grid.SetColumn(lteCellID, 1);
                    Grid.SetRow(lteCellID, i + 1);

                    TextBlock radioPathDirection = new TextBlock();
                    radioPathDirection.MouseLeftButtonDown += RadioPathDirection_MouseLeftButtonDown;
                    radioPathDirection.HorizontalAlignment = HorizontalAlignment.Center;
                    radioPathDirection.Text = "test";
                    radioPathDirection.Margin = new Thickness(5);
                    MainGrid.Children.Add(radioPathDirection);
                    Grid.SetColumn(radioPathDirection, 2);
                    Grid.SetRow(radioPathDirection, i + 1);

                    TextBlock supportFrequent = new TextBlock();
                    supportFrequent.Text = "支持的频段";
                    supportFrequent.HorizontalAlignment = HorizontalAlignment.Center;
                    supportFrequent.Margin = new Thickness(5);
                    MainGrid.Children.Add(supportFrequent);
                    Grid.SetColumn(supportFrequent, 3);
                    Grid.SetRow(supportFrequent, i + 1);
                }
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

            for (int i = 0; i < g_listCell.Count; i++)
            {
                RowDefinition newRow = new RowDefinition();
                newRow.Height = GridLength.Auto;
                newgrid.RowDefinitions.Add(newRow);

                CheckBox newItem = new CheckBox();
                newItem.Content = "NetLocalCell" + g_listCell[i].ToString();
                newgrid.Children.Add(newItem);
                Grid.SetRow(newItem, i + 1);
                newItem.Margin = new Thickness(5);
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
            Grid.SetRow(btnOK, g_listCell.Count + 1);
            btnOK.Margin = new Thickness(5);
        }

        private void RadioPathDirection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

            targetPop.IsOpen = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
