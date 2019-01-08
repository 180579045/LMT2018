using System;
using System.Collections.Generic;
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

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// AddNodeB_ICON.xaml 的交互逻辑
    /// </summary>
    public partial class AddNodeB_ICON : UserControl
    {
        public AddNodeB_ICON()
        {
            InitializeComponent();
            this.Add_NodeB_Icon.MouseDown += Add_NodeB_Icon_MouseDown;
        }



        private void Add_NodeB_Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Add NodeB" + e);
        }
    }
}
