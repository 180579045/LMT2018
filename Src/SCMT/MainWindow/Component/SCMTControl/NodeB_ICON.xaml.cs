

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
            
            // TODO:暂时先在这写;
            this.NodeBIP.Text = "172.27.245.92";
            this.NodeBFriendlyName.Text = "友好名";

            this.IPAddress = "172.27.245.92";
            this.FName = "友好名";
            
        }

        /// <summary>
        /// 上报连接基站事件;
        /// （如果让Icon自己处理连接基站的功能，问题是无法直接操作主页面跳转功能）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NodeB_Icon_MouseDown()
        {
            Console.WriteLine("NodeB ICON Clicked");
            ConnectNodeBPara para = new ConnectNodeBPara();
            para.FriendlyName = FName;
            para.Ipaddress = IPAddress;
            
            if (ConnectNodeBCommand != null && ConnectNodeBCommand.CanExecute(para))
            {
                ConnectNodeBCommand.Execute(para);
            }
        }
    }

    public class ConnectNodeBPara
    {
        public string Ipaddress { get; set; }
        public string FriendlyName { get; set; }
    }


    public class NodeBIcnBorder : Border
    {
        private NodeB_ICON _parent;
        private NodeB_ICON NbIcon
        {
            get
            {
                if (_parent == null)
                {
                    DependencyObject parent = this;
                    while (parent != null && !(parent is NodeB_ICON))
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                    }
                    _parent = parent as NodeB_ICON;
                }
                return _parent;
            }
        }

        public NodeBIcnBorder()
        {
            this.MouseDown += NodeBIcnBorder_MouseDown;
        }

        private void NodeBIcnBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NbIcon.NodeB_Icon_MouseDown();
        }
    }
}
