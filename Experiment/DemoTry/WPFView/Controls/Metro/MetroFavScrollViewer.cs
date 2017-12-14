using Arthas.Utility.Element;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Arthas.Controls.Metro
{
    public class MetroFavScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty FloatProperty = ElementBase.Property<MetroFavScrollViewer, bool>(nameof(FloatProperty));
        public static readonly DependencyProperty AutoLimitMouseProperty = ElementBase.Property<MetroFavScrollViewer, bool>(nameof(AutoLimitMouseProperty));
        public static readonly DependencyProperty VerticalMarginProperty = ElementBase.Property<MetroFavScrollViewer, Thickness>(nameof(VerticalMarginProperty));
        public static readonly DependencyProperty HorizontalMarginProperty = ElementBase.Property<MetroFavScrollViewer, Thickness>(nameof(HorizontalMarginProperty));

        public bool Float { get { return (bool)GetValue(FloatProperty); } set { SetValue(FloatProperty, value); } }
        public bool AutoLimitMouse { get { return (bool)GetValue(AutoLimitMouseProperty); } set { SetValue(AutoLimitMouseProperty, value); } }
        public Thickness VerticalMargin { get { return (Thickness)GetValue(VerticalMarginProperty); } set { SetValue(VerticalMarginProperty, value); } }
        public Thickness HorizontalMargin { get { return (Thickness)GetValue(HorizontalMarginProperty); } set { SetValue(HorizontalMarginProperty, value); } }

        public MetroFavScrollViewer()
        {
            Utility.Refresh(this);
        }

        static MetroFavScrollViewer()
        {
            ElementBase.DefaultStyle<MetroFavScrollViewer>(DefaultStyleKeyProperty);
        }
    }
}
