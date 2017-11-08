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
using Snmp_dll;
using System.Threading;

namespace SnmpWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var ThreadTrap = new Thread(WaitTrap);
            ThreadTrap.Start();
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string a = oid.Text;
            List<string> inputoid = new List<string>();
            string[] OidList;
            Dictionary<string, string> Ret;

            RetText.Text = "\\n";
            OidList = a.Split(';');
            foreach(string temp in OidList)
            {
                inputoid.Add(temp);
            }

            SnmpMessage snmpmsg = new SnmpMessage();
            Ret = snmpmsg.Type_GetRequest_V2c(inputoid, "public", "172.27.245.92");

            foreach(var RetShow in Ret)
            {
                RetText.Text += RetShow.Value +",";
            }
        }

        static void WaitTrap()
        {
            TrapMessage.WaitTrap();
        }
    }
}
