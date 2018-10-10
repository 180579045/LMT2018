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
using System.Threading;
using System.ComponentModel;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock;
using LmtbSnmp;

namespace SnmpWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool GetValueStatus = false;
        public MainWindow()
        {
            InitializeComponent();
            TrapMessage.SetNodify(this.UpdateTrapText);
//             Thread T2 = new Thread(NumPlus);
//             T2.Start();
        }

        public void NumPlus()
        {
            int Num = 0;
            while(true)
            {
                Thread.Sleep(1000);
                Action action = () => TrapText.Text += "另外一个线程;" + (Num++).ToString();
                TrapText.Dispatcher.BeginInvoke(action);
            }
        }

        /// <summary>
        /// TrapMessage线程收到Trap后调用,更新TrapText;
        /// </summary>
        /// <param name="TrapContent">TrapMessage的Trap监听线程返回的结果</param>
        public void UpdateTrapText(List<string> TrapContent)
        {
            // 如果当前运行的线程不是UI线程才写入到控件中;
            if(System.Threading.Thread.CurrentThread != this.TrapText.Dispatcher.Thread)
            {
                this.TrapText.Dispatcher.Invoke(
                   new Action(
                        delegate
                        {
                            foreach (string content in TrapContent)
                            {
                                TrapText.Text += content + "\r\n";
                            }
                        }
                   )
                );
            }
            
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
            Ret1 = snmpmsg1.GetRequest(inputoid1, "public", "172.27.245.92");  // TODO 需要确定真正的enb地址

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

        /// <summary>
        /// 持续获取基站数值压力测试按钮;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetValue_Continue_Click(object sender, RoutedEventArgs e)
        {
            string UserInputList1 = oid1.Text;
            string UserInputList2 = oid2.Text;
            string UserInputList3 = oid3.Text;

            List<string> inputoid1 = new List<string>();
            string[] OidList1;

            // 获取用户输入的OIDList1;
            OidList1 = UserInputList1.Split(';');
            foreach (string temp in OidList1)
            {
                inputoid1.Add(temp);
            }


            // 获取基站中对应数值;
            SnmpMessageV2c snmpmsg1 = new SnmpMessageV2c();
            snmpmsg1.GetRequest(AsyncGetSNMP, inputoid1, "public", "172.27.245.92"); // TODO 需要确定真正的enb地址

		}

        private void AsyncGetSNMP(IAsyncResult ar)
        {
            SnmpMessageResult res = ar as SnmpMessageResult;
            Dictionary<string, string> temp = res.AsyncState as Dictionary<string,string>;
        }

        /// <summary>
        /// 持续获取基站数值;
        /// </summary>
        private void StartGetValueContinue()
        {
            SnmpMessageV2c snmpmsg = new SnmpMessageV2c();
            Dictionary<string, string> Ret;
            List<string> inputpdu = new List<string>();
            // 这个是板卡表;
            inputpdu.Add("1.3.6.1.4.1.5105.100.1.9.5.1.1.4.0.0.1");
            inputpdu.Add("1.3.6.1.4.1.5105.100.1.9.5.1.1.5.0.0.1");
            inputpdu.Add("1.3.6.1.4.1.5105.100.1.9.5.1.1.6.0.0.1");
            inputpdu.Add("1.3.6.1.4.1.5105.100.1.9.5.1.1.7.0.0.1");
            inputpdu.Add("1.3.6.1.4.1.5105.100.1.9.5.1.1.8.0.0.1");

            while (GetValueStatus)
            {
                Ret = snmpmsg.GetRequest(inputpdu, "public", "172.27.245.92");  // TODO 需要确定真正的enb地址
				string temp = "";
                foreach (var RetShow in Ret)
                {
                    temp += RetShow.Value + ",";
                }
                Console.WriteLine(temp);
            }
        }

        /// <summary>
        /// 设置基站数值按钮事件;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetValue_Click(object sender, RoutedEventArgs e)
        {
            string oid1 = this.SetOId1.Text;
            string oid2 = this.SetOId2.Text;
            string oid3 = this.SetOId3.Text;

            string value1 = this.SetValue1.Text;
            string value2 = this.SetValue2.Text;
            string value3 = this.SetValue3.Text;

            Dictionary<string, string> Pdulist1 = new Dictionary<string, string>();
            Pdulist1.Add(oid1, value1);
            Pdulist1.Add(oid2, value2);
            Pdulist1.Add(oid3, value3);

            SnmpMessageV2c SetValue = new SnmpMessageV2c();
            SetValue.SetRequest(Pdulist1, "public", "172.27.245.92");  // TODO 需要确定真正的enb地址
		}

        private void GetNext_Click(object sender, RoutedEventArgs e)
        {
            string UserInputList1 = oid1.Text;

            List<string> inputoid1 = new List<string>();
            
            SnmpMessageV2c msg = new SnmpMessageV2c("public", "172.27.245.92");  // TODO 需要确定真正的enb地址
            //Dictionary<string,string> ret = msg.GetNext(UserInputList1);
        }

        private void GetNextAsync_Click(object sender, RoutedEventArgs e)
        {
            string oid = oid1.Text;
            string[] oids = oid.Split(';');
            List<string> OidsArgs = new List<string>();

            foreach(string iter in oids)
            {
                OidsArgs.Add(iter);
            }

            SnmpMessageV2c msg = new SnmpMessageV2c("public", "172.27.245.92");  // TODO 需要确定真正的enb地址
			msg.GetNextRequest(ReceiveRes, OidsArgs);
        }

        void ReceiveRes(IAsyncResult ar)
        {
            SnmpMessageResult res = ar as SnmpMessageResult;

            foreach(KeyValuePair<string, string> iter in res.AsyncState as Dictionary<string, string> )
            {
                Console.WriteLine("NextIndex" + iter.Key.ToString()+ "Value:" + iter.Value.ToString());
            }
        }

        private void shownewpanel(object sender, RoutedEventArgs e)
        {
            LayoutAnchorable sub = new LayoutAnchorable();
            this.pane1.Children.Add(sub);
            sub.Title = "aa";
            sub.IsVisible = true;
            sub.Float();
        }
    }

}
