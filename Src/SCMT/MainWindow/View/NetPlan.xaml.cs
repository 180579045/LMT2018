using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// NetPlan.xaml 的交互逻辑
    /// </summary>
    public partial class NetPlan : UserControl
    {
        public NetPlan()
        {
            InitializeComponent();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(dockingmanger);
            using (var stream = new StreamReader("layout.xml"))
            {
                serializer.Deserialize(stream);
            }

        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                LayoutAnchorable la = new LayoutAnchorable();
                la.Title = "断点";
                la.Content = new TextBox();
                LayoutDocument ld = new LayoutDocument();
                ld.Title = "good";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(dockingmanger);
            using (var stream = new StreamWriter("layout.xml"))
            {
                serializer.Serialize(stream);
            }
        }
    }
}
