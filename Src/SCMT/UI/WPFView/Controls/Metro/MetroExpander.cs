using Arthas.Utility.Element;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;

namespace Arthas.Controls.Metro
{
    public class MetroExpander : ContentControl
    {
        public static readonly DependencyProperty IsExpandedProperty = ElementBase.Property<MetroExpander, bool>(nameof(IsExpandedProperty));
        public static readonly DependencyProperty CanHideProperty = ElementBase.Property<MetroExpander, bool>(nameof(CanHideProperty));
        public static readonly DependencyProperty HeaderProperty = ElementBase.Property<MetroExpander, string>(nameof(HeaderProperty));
        public static readonly DependencyProperty MarginProperty = ElementBase.Property<MetroExpander, Thickness>(nameof(MarginProperty));
        public static readonly DependencyProperty ARMarginProperty = ElementBase.Property<MetroExpander, Thickness>(nameof(ARMarginProperty));
        public static readonly DependencyProperty HintProperty = ElementBase.Property<MetroExpander, string>(nameof(HintProperty));
        public static readonly DependencyProperty HintBackgroundProperty = ElementBase.Property<MetroExpander, Brush>(nameof(HintBackgroundProperty));
        public static readonly DependencyProperty HintForegroundProperty = ElementBase.Property<MetroExpander, Brush>(nameof(HintForegroundProperty));
        public static readonly DependencyProperty IconProperty = ElementBase.Property<MetroExpander, ImageSource>(nameof(IconProperty), null);

        public static RoutedUICommand ButtonClickCommand = ElementBase.Command<MetroExpander>(nameof(ButtonClickCommand));
        public static RoutedUICommand ButtonClickCommand2 = ElementBase.Command<MetroExpander>(nameof(ButtonClickCommand2));

        public bool IsExpanded { get { return (bool)GetValue(IsExpandedProperty); } set { SetValue(IsExpandedProperty, value); ElementBase.GoToState(this, IsExpanded ? "Expand" : "Normal"); } }
        public bool CanHide { get { return (bool)GetValue(CanHideProperty); } set { SetValue(CanHideProperty, value); } }
        public string Header { get { return (string)GetValue(HeaderProperty); } set { SetValue(HeaderProperty, value); } }
        public Thickness Margin { get { return (Thickness)GetValue(MarginProperty); } set { SetValue(MarginProperty, value); } }
        public Thickness ARMargin { get { return (Thickness)GetValue(ARMarginProperty); } set { SetValue(ARMarginProperty, value); } }
        public string Hint { get { return (string)GetValue(HintProperty); } set { SetValue(HintProperty, value); } }
        public Brush HintBackground { get { return (Brush)GetValue(HintBackgroundProperty); } set { SetValue(HintBackgroundProperty, value); } }
        public Brush HintForeground { get { return (Brush)GetValue(HintForegroundProperty); } set { SetValue(HintForegroundProperty, value); } }
        public ImageSource Icon { get { return (ImageSource)GetValue(IconProperty); } set { SetValue(IconProperty, value); } }
        
        public StackPanel SubExpender { get; set; }                // 右侧叶子节点容器;
        public object obj_type { get; set; }                       // 保存对象;
        public Grid NBContent_Grid { get; set; }                   // 显示内容的容器;
        public MetroScrollViewer NBBase_Grid { get; set; }         // 显示基本信息的容器;
        
        public event EventHandler Click;

        public MetroExpander()
        {
            Utility.Refresh(this);

            Loaded += delegate
            {
                if (Content == null)
                {
                    IsExpanded = false;
                }
                else if (!CanHide)
                {
                    IsExpanded = true;
                }
                ElementBase.GoToState(this, IsExpanded ? "StartExpand" : "StartNormal");
            };

            CommandBindings.Add(new CommandBinding(ButtonClickCommand, delegate
            {
                if (CanHide && Content != null)
                {
                    IsExpanded = !IsExpanded;
                }
            }));

            CommandBindings.Add(new CommandBinding(ButtonClickCommand2, delegate
            {
                if (Click != null)
                {
                    Click(this, null);
                }
            }));
        }

        public void Clear()
        {
            Content = new StackPanel();
        }

        /// <summary>
        /// 所包含的孩子节点;
        /// </summary>
        public UIElementCollection Children
        {
            get
            {
                if ((Content is StackPanel))
                {
                    return (Content as StackPanel).Children;
                }
                else if ((Content is Grid))
                {
                    return (Content as Grid).Children;
                }
                return null;
            }
        }
        
        public void Add(FrameworkElement element)
        {
            if (!(Content is StackPanel))
            {
                Content = new StackPanel();
            }
            (Content as StackPanel).Children.Add(element);
        }
        
        static MetroExpander()
        {
            ElementBase.DefaultStyle<MetroExpander>(DefaultStyleKeyProperty);
        }
    }
}