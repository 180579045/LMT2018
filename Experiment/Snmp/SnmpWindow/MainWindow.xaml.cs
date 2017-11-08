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
using System.ComponentModel;

namespace SnmpWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TrapMessage.SetNodify(this);
            TrapMessage.WaitforTrap.Start();

            MyTextshow mtextshow = new MyTextshow();
            mtextshow.show = TrapShow;
            TrapText.DataContext = mtextshow;//textBox为控件名

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string a = oid.Text;
            List<string> inputoid = new List<string>();
            string[] OidList;
            Dictionary<string, string> Ret;
            
            OidList = a.Split(';');
            foreach(string temp in OidList)
            {
                inputoid.Add(temp);
            }

            SnmpMessageV2c snmpmsg = new SnmpMessageV2c();
            Ret = snmpmsg.GetRequest(inputoid, "public", "172.27.245.92");

            foreach(var RetShow in Ret)
            {
                RetText.Text += RetShow.Value +",";
            }
        }

    }

    public class Observer 
    {
        public TextBox obj;
        public Observer(TextBox obj)
        {

        }
        public void UpdateTrap(string TrapContent)
        {
            TrapText.Text = TrapContent;
        }
    }
}
