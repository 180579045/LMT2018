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

namespace SCMTMainWindow.View
{
    public partial class DesignerCanvas : Canvas
    {
        private Point? rubberbandSelectionStartPoint = null;

        private Dictionary<string, int> dicRRU = new Dictionary<string, int>();

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
                    for(int i = 0; i < nRRUNumber; i++)
                    {
                        DesignerItem newItem = new DesignerItem();

                        string strXAML1 = strXAML;
                        string strRRUFullName = string.Empty;

                        if(dicRRU.Count == 0)
                        {
                            strRRUFullName = string.Format("{0}-{1}", strRRUName, dicRRU.Count);
                            dicRRU.Add(strRRUFullName, 0);
                        }
                        else
                        {
                            Dictionary<string, int>.KeyCollection keys = dicRRU.Keys;
                            string strMaxString = keys.ElementAt(dicRRU.Count-1);
                            int nIndex = dicRRU[strMaxString] + 1;
                            strRRUFullName = string.Format("{0}-{1}", strRRUName, nIndex);
                            dicRRU.Add(strRRUFullName, nIndex);
                        }
                        strRRUFullName = string.Format("Text=\"{0}\"", strRRUFullName);
                        strXAML1 = strXAML1.Replace("Text=\"RRU\"", strRRUFullName);
                        Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                        newItem.Content = testContent;

                        Point position = e.GetPosition(this);

                        if (dragObject.DesiredSize.HasValue)
                        {
                            Size desiredSize = dragObject.DesiredSize.Value;
                            newItem.Width = desiredSize.Width;
                            newItem.Height = desiredSize.Height;

                            DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2) + i * 20);
                            DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2) + i * 20);
                        }
                        //else
                        //{
                        //    DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X));
                        //    DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y));
                        //}
                        Canvas.SetZIndex(newItem, this.Children.Count);
                        this.Children.Add(newItem);
                        SetConnectorDecoratorTemplate(newItem);
                        
                        this.SelectionService.SelectItem(newItem);
                        newItem.Focus();
                    }
                }

                e.Handled = true;
            }
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
