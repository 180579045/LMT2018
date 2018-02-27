using UICore.Utility.Element;
using System.Windows.Controls;

namespace UICore.Controls.Metro
{
    public class MetroTextButton : Button
    {
        static MetroTextButton()
        {
            ElementBase.DefaultStyle<MetroTextButton>(DefaultStyleKeyProperty);
        }
    }
}