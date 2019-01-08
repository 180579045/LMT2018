

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
    /// NodeB_ICON.xaml 的交互逻辑
    /// </summary>
    public partial class NodeB_ICON : UserControl
    {
        public string IPAddress { get; set; }
        public string FName { get; set; }

        /// <summary>
        /// 连接基站的依赖命令属性;
        /// </summary>
        public static readonly DependencyProperty ConnectNodeBCommandProperty =
            DependencyProperty.Register(
            "ConnectNodeBCommand",
            typeof(ICommand),
            typeof(NodeB_ICON));


        public ICommand ConnectNodeBCommand
        {
            get => (ICommand)GetValue(ConnectNodeBCommandProperty);
            set => SetValue(ConnectNodeBCommandProperty, value);
        }


        public NodeB_ICON()
        {
            InitializeComponent();
            this.NodeB_Icon.MouseDown += NodeB_Icon_MouseDown;
            this.NodeBIP.Text = "172.27.245.92";
            this.NodeBFriendlyName.Text = "友好名";
        }

        /// <summary>
        /// 连接基站;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeB_Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("NodeB ICON Clicked" + e);
            

        }
    }

    public class ConnectNodeBPara
    {
        public string Ipaddress { get; set; }
        public string FriendlyName { get; set; }
    }
}
