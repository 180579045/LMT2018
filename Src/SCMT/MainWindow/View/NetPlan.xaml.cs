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

        public NetPlan()
        {
            InitializeComponent();


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
                    rectItem.Fill = new SolidColorBrush(Colors.LightGreen);
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

            MyDesigner.Children.Add(boardCanvas);
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
                BoradDetailData bd = new BoradDetailData();
                //初始化弹框默认数据
                bd.setDefaultDate(soltNum);
                //实例化弹窗
                AddBoardWindow broadDetailWindos = new AddBoardWindow();
                //初始化弹窗
                broadDetailWindos.SetOperationData(bd);
                //展示弹窗
                broadDetailWindos.ShowDialog();

                if (!broadDetailWindos.isOk)
                {
                    return;
                }
                //获取弹窗中设置的板卡名称
                string broadName = BroadName(broadDetailWindos.detaiData.Bt);

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
                boardNameLabel.Width = 45;
                boardNameLabel.Height = 25;
                boardNameLabel.Content = broadName;
                boardCanvas.Children.Add(boardNameLabel);

                Canvas.SetRight(boardNameLabel, 185 + 240 * (boardColumn - 1 - nColumn));
                Canvas.SetBottom(boardNameLabel, 0 + 40 * nRow);

                targetItem.Fill = new SolidColorBrush(Colors.LightYellow);

                DesignerItem designerItem = new DesignerItem();
                Rectangle rect = new Rectangle();
                rect.Fill = new SolidColorBrush(Colors.Red);
                rect.Width = 15;
                rect.Height = 10;
                designerItem.Content = rect;


                Canvas.SetRight(designerItem, 80 + 240 * (boardColumn -1 - nColumn));
                Canvas.SetBottom(designerItem, 10 + 40 * nRow);

                boardCanvas.Children.Add(designerItem);
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
        }
    }
}
