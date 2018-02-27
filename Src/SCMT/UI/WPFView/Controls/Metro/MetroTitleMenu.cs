using UICore.Utility.Element;
using System.Windows.Controls;

namespace UICore.Controls.Metro
{
    public class MetroTitleMenu : Menu
    {
        public MetroTitleMenu()
        {
            Utility.Refresh(this);
        }

        static MetroTitleMenu()
        {
            ElementBase.DefaultStyle<MetroTitleMenu>(DefaultStyleKeyProperty);
        }
    }
}