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
            TrapMessage.SetNodify(this.UpdateTrapText);
            TrapMessage.WaitforTrap.Start();
        }

        /// <summary>
        /// TrapMessage线程收到Trap后调用,更新TrapText;
        /// </summary>
        /// <param name="TrapContent">TrapMessage的Trap监听线程返回的结果</param>
        public void UpdateTrapText(List<string> TrapContent)
        {
            this.TrapText.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        foreach (string content in TrapContent)
                        {
                            TrapText.Text += content;
                        }
                    }
               )
            );
        }

        /// <summary>
        /// 获取基站数值按钮事件;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetValue_Click(object sender, RoutedEventArgs e)
        {
            string UserInputList1 = oid1.Text;
            string UserInputList2 = oid2.Text;
            string UserInputList3 = oid3.Text;

            List<string> inputoid1 = new List<string>();
            List<string> inputoid2 = new List<string>();
            List<string> inputoid3 = new List<string>();
            string[] OidList1, OidList2, OidList3;
            Dictionary<string, string> Ret1, Ret2, Ret3;
            
            // 获取用户输入的OIDList1;
            OidList1 = UserInputList1.Split(';');
            foreach(string temp in OidList1)
            {
                inputoid1.Add(temp);
            }

            // 获取用户输入的OIDList2;
            OidList2 = UserInputList2.Split(';');
            foreach (string temp in OidList2)
            {
                inputoid2.Add(temp);
            }

            // 获取用户输入的OIDList2;
            OidList3 = UserInputList3.Split(';');
            foreach (string temp in OidList3)
            {
                inputoid3.Add(temp);
            }

            // 获取基站中对应数值;
            SnmpMessageV2c snmpmsg1 = new SnmpMessageV2c();
            Ret1 = snmpmsg1.GetRequest(inputoid1, "public", "172.27.245.92");

            // 获取基站中对应数值;
            SnmpMessageV2c snmpmsg2 = new SnmpMessageV2c();
            Ret2 = snmpmsg2.GetRequest(inputoid2, "public", "172.27.245.92");

            // 获取基站中对应数值;
            SnmpMessageV2c snmpmsg3 = new SnmpMessageV2c();
            Ret3 = snmpmsg3.GetRequest(inputoid3, "public", "172.27.245.92");

            var RetTemp = Ret1.Union(Ret2);
            var AllRet = RetTemp.Union(Ret3);

            foreach (var RetShow in AllRet)
            {
                RetText.Text += RetShow.Value +",";
            }
        }

    }
}
