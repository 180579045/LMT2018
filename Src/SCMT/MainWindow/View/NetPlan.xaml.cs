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

        public NetPlan()
        {
            InitializeComponent();

            propertyGrid.SelectedObject = p1;
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
            boardCanvas.Background = new SolidColorBrush(Colors.Red);

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
                //Rectangle rect = new Rectangle();
                //rect.Fill = new SolidColorBrush(Colors.Red);
                //rect.Width = 15;
                //rect.Height = 10;


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

                Canvas.SetLeft(designerItem, CanvasLeft + 160 + 240 * nColumn);
                Canvas.SetTop(designerItem, CanvasTop + 12.5 + 40 * (boardRow - 1 - nRow));

                MyDesigner.Children.Add(designerItem);
                SetConnectorDecoratorTemplate(designerItem);
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


        //protected override Size MeasureOverride(Size constraint)
        //{

        //    MyDesigner.Measure(constraint);
        //    return base.MeasureOverride(constraint);
        //}




    }
}
