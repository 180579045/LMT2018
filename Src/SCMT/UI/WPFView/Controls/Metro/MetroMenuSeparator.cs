using UICore.Utility.Element;
using System.Windows.Controls;

namespace UICore.Controls.Metro
{
    public class MetroMenuSeparator : Separator
    {
        static MetroMenuSeparator()
        {
            ElementBase.DefaultStyle<MetroMenuSeparator>(DefaultStyleKeyProperty);
        }
    }
}