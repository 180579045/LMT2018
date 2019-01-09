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
using GalaSoft.MvvmLight.Command;
using SCMTMainWindow.Component.SCMTControl;

namespace SCMTMainWindow.Pages
{
    /// <summary>
    /// ENBListManager.xaml 的交互逻辑
    /// </summary>
    public partial class ENBListManager : UserControl
    {
        public RelayCommand<ConnectNodeBPara> ConnectNodeBCommand;

        public ENBListManager()
        {
            InitializeComponent();
            ConnectNodeBCommand = new RelayCommand<ConnectNodeBPara>(ConnectNodeB);
            NodeBIcon.ConnectNodeBCommand = ConnectNodeBCommand;
        }

        /// <summary>
        /// 当一个基站节点被点击，触发连接基站处理函数;
        /// </summary>
        /// <param name="para"></param>
        private void ConnectNodeB(ConnectNodeBPara para)
        {
            Console.WriteLine("Connecting to NodeB" + para.FriendlyName);
        }
    }
}
