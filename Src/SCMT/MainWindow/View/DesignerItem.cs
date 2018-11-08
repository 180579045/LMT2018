using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SCMTMainWindow.View.Controls;
using NetPlan;
using System.Collections.Generic;

namespace SCMTMainWindow.View
{
    //These attributes identify the types of the named parts that are used for templating
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(DragThumb))]
    //[TemplatePart(Name = "PART_ResizeDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ConnectorDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public class DesignerItem : ContentControl, ISelectable, IGroupable
    {
        //新加的 具体某个网元的名称，也就是 ID
        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        //新加一个通道数 属性，保存当前网元的通道数，区别大于16通道的一些网元
        private int nPathNumber;
        public int NPathNumber
        {
            get { return nPathNumber; }
            set { nPathNumber = value; }

        }
        //连接点的需要传给连接线的一些属性
        public EnumDevType DevType { get; set; }
        public string DevIndex { get; set; }
        public int PortNo { get; set; }  //对于板卡的光口来说，需要设置一个 Portno

        #region ID
        private Guid id;
        public Guid ID
        {
            get { return id; }
        }
        #endregion

        #region ParentID
        public Guid ParentID
        {
            get { return (Guid)GetValue(ParentIDProperty); }
            set { SetValue(ParentIDProperty, value); }
        }
        public static readonly DependencyProperty ParentIDProperty = DependencyProperty.Register("ParentID", typeof(Guid), typeof(DesignerItem));
        #endregion

        #region IsGroup
        public bool IsGroup
        {
            get { return (bool)GetValue(IsGroupProperty); }
            set { SetValue(IsGroupProperty, value); }
        }
        public static readonly DependencyProperty IsGroupProperty =
            DependencyProperty.Register("IsGroup", typeof(bool), typeof(DesignerItem));
        #endregion

        #region IsSelected Property

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(false));

        #endregion

        #region DragThumbTemplate Property

        // can be used to replace the default template for the DragThumb
        public static readonly DependencyProperty DragThumbTemplateProperty =
            DependencyProperty.RegisterAttached("DragThumbTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetDragThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        public static void SetDragThumbTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        #endregion

        #region ConnectorDecoratorTemplate Property

        // can be used to replace the default template for the ConnectorDecorator
        public static readonly DependencyProperty ConnectorDecoratorTemplateProperty =
            DependencyProperty.RegisterAttached("ConnectorDecoratorTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetConnectorDecoratorTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(ConnectorDecoratorTemplateProperty);
        }

        public static void SetConnectorDecoratorTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(ConnectorDecoratorTemplateProperty, value);
        }

        #endregion

        #region IsDragConnectionOver

        // while drag connection procedure is ongoing and the mouse moves over 
        // this item this value is true; if true the ConnectorDecorator is triggered
        // to be visible, see template
        public bool IsDragConnectionOver
        {
            get { return (bool)GetValue(IsDragConnectionOverProperty); }
            set { SetValue(IsDragConnectionOverProperty, value); }
        }
        public static readonly DependencyProperty IsDragConnectionOverProperty =
            DependencyProperty.Register("IsDragConnectionOver",
                                         typeof(bool),
                                         typeof(DesignerItem),
                                         new FrameworkPropertyMetadata(false));

        #endregion

        static DesignerItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }

        public DesignerItem(Guid id)
        {
            this.id = id;
            this.Loaded += new RoutedEventHandler(DesignerItem_Loaded);
        }

        public DesignerItem()
            : this(Guid.NewGuid())
        {
        }


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

            // update selection
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                        //选中的时候切换属性的显示
                        if (this.itemName != null && this.itemName != string.Empty)
                        {
                            if (designer.g_GridForNet.ContainsKey(this.itemName))
                            {
                                if (designer.gridProperty != null)
                                {
                                    designer.gridProperty.Children.Clear();
                                    designer.gridProperty.Children.Add(designer.g_GridForNet[this.ItemName]);
                                }
                            }
                        }
                    }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                    //选中的时候切换属性的显示
                    if(this.itemName != null && this.itemName != string.Empty)
                    {
                        if (designer.g_GridForNet.ContainsKey(this.itemName))
                        {
                            if (designer.gridProperty != null)
                            {
                                designer.gridProperty.Children.Clear();
                                designer.gridProperty.Children.Add(designer.g_GridForNet[this.ItemName]);
                            }
                        }
                    }
                }
                Focus();
            }

            e.Handled = false;
        }

        void DesignerItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.Template != null)
            {

                //对板卡的光口进行设置 portno  并且板卡光口不支持拖拽
                if (this.DevType == EnumDevType.board)
                {
                    Control cd = this.Template.FindName("PART_ConnectorDecorator", this) as Control;
                    List<Connector> connectors = new List<Connector>();
                    GetConnectors(cd, connectors);

                    if (connectors != null && connectors.Count != 0)
                    {
                        connectors[0].PortNo = this.PortNo;
                    }
                    DragThumb targetItem = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                    if (targetItem != null)
                    {
                        targetItem.Visibility = Visibility.Hidden;
                    }

                        return;
                }

                ContentPresenter contentPresenter =
                    this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;
                if (contentPresenter != null)
                {
                    UIElement contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;
                    if (contentVisual != null)
                    {
                        DragThumb thumb = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                        if (thumb != null)
                        {
                            ControlTemplate template =
                                DesignerItem.GetDragThumbTemplate(contentVisual) as ControlTemplate;
                            if (template != null)
                                thumb.Template = template;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查找 DesignerItem 中的连接点 Connector
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="connectors"></param>
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

    }
}
