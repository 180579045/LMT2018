using UICore.Utility.Element;
using System.Windows.Controls;

namespace UICore.Controls.Metro
{
    public class MetroContextMenu : ContextMenu
    {
        public MetroContextMenu()
        {
            Utility.Refresh(this);
        }

        static MetroContextMenu()
        {
            ElementBase.DefaultStyle<MetroContextMenu>(DefaultStyleKeyProperty);
        }
    }
}