using UICore.Utility.Element;
using System.Windows.Controls;

namespace UICore.Controls.Metro
{
    public class MetroGroupBox : GroupBox
    {
        static MetroGroupBox()
        {
            ElementBase.DefaultStyle<MetroGroupBox>(DefaultStyleKeyProperty);
        }
    }
}