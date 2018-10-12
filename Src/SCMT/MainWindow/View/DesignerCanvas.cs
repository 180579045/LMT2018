using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using NetPlan;
using SCMTMainWindow.Property;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Reflection;
using System.Reflection.Emit;

using CommonUtility;

namespace SCMTMainWindow.View
{
    public partial class DesignerCanvas : Canvas
    {
        private Point? rubberbandSelectionStartPoint = null;

        //private Dictionary<string, int> dicRRU = new Dictionary<string, int>();
        private int nRRUMax = 0;

        private SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                SelectionService.ClearSelection();
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// 从xaml文件中获取对应的网元的xaml信息，名称和大小
        /// </summary>
        /// <param name="nMaxRRUPath"></param>
        /// <param name="strXAML"></param>
        /// <param name="RRUSize"></param>
        /// <returns></returns>
        private string GetElementFromXAML(int nMaxRRUPath, string strXAML, out Size RRUSize)
        {
            Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/XMLFile1.xml");
            Stream stream = Application.GetResourceStream(strUri).Stream;

            FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;

            string strName = string.Empty;
            if(nMaxRRUPath == 1)
            {
                strName = "g_OnePathRRU";
                RRUSize = new Size(80, 70);
            }
            else if(nMaxRRUPath == 2)
            {
                strName = "g_TwoPathRRU";
                RRUSize = new Size(130, 70);
            }
            else if (nMaxRRUPath == 4)
            {
                strName = "g_FourPathRRU";
                RRUSize = new Size(160, 70);
            }
            else if(nMaxRRUPath == 8)
            {
                strName = "g_EightPathRRU";
                RRUSize = new Size(260, 70);
            }
            else if (nMaxRRUPath == 16)
            {
                strName = "g_SixteenPathRRU";
                RRUSize = new Size(480, 70);
            }
            else
            {
                strName = "g_OnePathRRU";
                RRUSize = new Size(80, 50);
            }

            Object content = el.FindName(strName) as Grid;

            strXAML = XamlWriter.Save(content);

            return strXAML;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;

            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {
                Object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));

                if (content != null)
                {
                    //弹出RRU属性对话框，选择RRU的相关类型以及要添加的数量
                    ChooseRRUType dlgChooseRRU = new ChooseRRUType();
                    dlgChooseRRU.ShowDialog();

                    if (!dlgChooseRRU.bOK)
                    {
                        return;    //选择取消之后，不进行拖拽
                    }
                    int nMaxRRUPath = dlgChooseRRU.nMaxRRUPath;         //RRU的最大通道数
                    int nRRUNumber = dlgChooseRRU.nRRUNumber;           //需要添加的RRU的数量
                    string strXAML = string.Empty;                                        //解析xml文件
                    Size newSize;                                                                  //根据不同的通道数，确定不同的RRU的大小
                    string strRRUName = dlgChooseRRU.strRRUName;
                    strXAML =  GetElementFromXAML(nMaxRRUPath, strXAML, out newSize);

                    dragObject.DesiredSize = newSize;            //这个是之前代码留下的，实际上可以修改一下，这里并没有太大的意义，以后载重构吧，ByMayi 2018-0927



                    //根据输入的个数，添加多个网元
                    for (int i = 0; i < nRRUNumber; i++)
                    {
                        DesignerItem newItem = new DesignerItem();

                        string strXAML1 = strXAML;
                        string strRRUFullName = string.Empty;

                        strRRUFullName = string.Format("{0}-{1}", strRRUName, nRRUMax++);
                        //if(dicRRU.Count == 0)
                        //{
                        //    strRRUFullName = string.Format("{0}-{1}", strRRUName, dicRRU.Count);
                        //    dicRRU.Add(strRRUFullName, 0);
                        //}
                        //else
                        //{
                        //    Dictionary<string, int>.KeyCollection keys = dicRRU.Keys;
                        //    string strMaxString = keys.ElementAt(keys.Count-1);
                        //    int nIndex = dicRRU[strMaxString] + 1;
                        //    strRRUFullName = string.Format("{0}-{1}", strRRUName, nIndex);
                        //    dicRRU.Add(strRRUFullName, nIndex);
                        //}
                        string strInstedName = string.Format("Text=\"{0}\"", strRRUFullName);
                        strXAML1 = strXAML1.Replace("Text=\"RRU\"", strInstedName);
                        Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                        newItem.Content = testContent;
                        newItem.ItemName = strRRUFullName;

                        var test = NPECmdHelper.GetInstance().GetDevAttributesFromMib("rru");
                        globalDic.Add(strRRUFullName, test);

                        //Type typeTest = DynamicObject.BuildTypeWithCustomAttributesOnMethod("rru", test);

                        Point position = e.GetPosition(this);

                        if (dragObject.DesiredSize.HasValue)
                        {
                            Size desiredSize = dragObject.DesiredSize.Value;
                            newItem.Width = desiredSize.Width;
                            newItem.Height = desiredSize.Height;

                            DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2) + i * 20);
                            DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2) + i * 20);
                        }
                        Canvas.SetZIndex(newItem, this.Children.Count);
                        this.Children.Add(newItem);
                        SetConnectorDecoratorTemplate(newItem);
                        
                        this.SelectionService.SelectItem(newItem);
                        newItem.Focus();

                        Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");

                        if(ucTest != null)
                        {
                            gridProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                            CreateGirdForNetInfo(strRRUFullName, test);
                            gridProperty.Children.Clear();
                            gridProperty.Children.Add(g_GridForNet[strRRUFullName]);
                        }
                    }
                }

                e.Handled = true;
            }
        }

        //尝试获取节点的根元素    根据网上的代码，查询一个元素的父节点的父节点。。。通过递归查找到 Name 为
        //某个值的元素
        private T GetRootElement<T>(DependencyObject item, string strName) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);

            while(parent != null)
            {
                if(parent is T && ((T)parent).Name == strName)
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        //尝试获取元素的子节点
        private T GetChildrenElement<T>(DependencyObject item, string strName) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            int n = VisualTreeHelper.GetChildrenCount(item);

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(item); i++)
            {
                child = VisualTreeHelper.GetChild(item, i);

                if(child is T && ((T)child).Name == strName)
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildrenElement<T>(child, strName);
                    if(grandChild != null)
                    {
                        return grandChild;
                    }
                }
            }

            return null;
        }

        //全局变量，将网元名称和网元信息结构体对应
        private Dictionary<string, List<MibLeafNodeInfo>> globalDic = new Dictionary<string, List<MibLeafNodeInfo>>();
        //全局变量，将网元名称和网元的属性表格对应
        public Dictionary<string, Grid> g_GridForNet = new Dictionary<string, Grid>();
        //保存界面上的属性表格
        public Grid gridProperty;

        /// <summary>
        /// 为网元创建属性表格
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="mibInfo"></param>
        private void CreateGirdForNetInfo(string strName, List<MibLeafNodeInfo> mibInfo)
        {
            Grid grid = new Grid();

            //创建两列，第一列显示属性名称Name，第二列显示属性值Value
            ColumnDefinition columnName = new ColumnDefinition();
            ColumnDefinition columnValue = new ColumnDefinition();
            ColumnDefinition columnSplit = new ColumnDefinition();
            columnSplit.Width = GridLength.Auto;
            columnName.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(columnName);
            grid.ColumnDefinitions.Add(columnSplit);                     //分隔条
            grid.ColumnDefinitions.Add(columnValue);

            GridSplitter gridSplit = new GridSplitter();
            gridSplit.Width = 5;
            gridSplit.VerticalAlignment = VerticalAlignment.Stretch;
            gridSplit.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            gridSplit.Background = new SolidColorBrush(Colors.LightGray);
            grid.Children.Add(gridSplit);
            Grid.SetColumn(gridSplit, 1);
            Grid.SetRow(gridSplit, 1);

            //创建一个标题行
            RowDefinition rowTitle = new RowDefinition();
            grid.RowDefinitions.Add(rowTitle);
            TextBlock txtTitile = new TextBlock();
            txtTitile.Text = strName;
            grid.Children.Add(txtTitile);
            Grid.SetRow(txtTitile, 0);
            Grid.SetColumnSpan(txtTitile, 3);

            //根据传过来的值，添加行，每一行代表一个属性，并根据不同的类型添加不同的控件
            for(int i = 0; i < mibInfo.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(27);
                grid.RowDefinitions.Add(row);

                //添加属性名称的控件
                TextBlock txtName = new TextBlock();
                txtName.Margin = new Thickness(1);
                txtName.Text = mibInfo[i].mibAttri.childNameCh;
                grid.Children.Add(txtName);
                Grid.SetColumn(txtName, 0);
                Grid.SetRow(txtName, i+1);

                //根据不同的类型添加属性值控件
                switch(mibInfo[i].mibAttri.OMType)
                {
                    case "enum":
                        ComboBox cbValue = new ComboBox();
                        cbValue.Margin = new Thickness(1);
                        cbValue.Height = 25;

                        var valueInfo = MibStringHelper.SplitManageValue(mibInfo[i].mibAttri.managerValueRange);

                        for(int j = 0; j < valueInfo.Count; j++)
                        {
                            cbValue.Items.Add(valueInfo.ElementAt(j).Value);
                        }

                        //string[] strValue = mibInfo[i].mibAttri.defaultValue.Split(':');
                        //string strDefaultInfo = valueInfo[int.Parse(strValue[0])];
                        //cbValue.SelectedItem = strDefaultInfo;

                        grid.Children.Add(cbValue);
                        Grid.SetColumn(cbValue, 2);
                        Grid.SetRow(cbValue, i+1);
                        break;
                    default:
                        TextBox txtValue = new TextBox();
                        txtName.Margin = new Thickness(1);
                        txtName.Height = 25;
                        txtValue.Text = mibInfo[i].mibAttri.defaultValue;
                        grid.Children.Add(txtValue);
                        Grid.SetColumn(txtValue, 2);
                        Grid.SetRow(txtValue, i+1);
                        break;
                }
            }

            Grid.SetRowSpan(gridSplit, mibInfo.Count);
            g_GridForNet.Add(strName, grid);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            size.Width += 10;
            size.Height += 10;
            return size;
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
    }
}
