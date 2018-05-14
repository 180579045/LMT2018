using CDLBrowser.Parser.Document;
using CDLBrowser.Parser.Document.Event;
using MsgQueue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace CDLBrowser.Parser
{
    /// <summary>
    /// ParseMessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ParseMessageWindow : Window
    {
        ObservableCollection<EventNew> hlMessageUE = new ObservableCollection<EventNew>();

        SubscribeClient subClient;
        public ParseMessageWindow()
        {
            InitializeComponent();
            InitSubscribeTopic();
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventNew en = dataGrid.SelectedItem as EventNew;
            HexMessage hexm = new HexMessage();
            hexm.HeMemoryViewContext = en.RawData;
            this.hexMemView.DataContext = hexm;

        }
        private void InitSubscribeTopic()
        {
            PubSubServer.GetInstance().InitServer(CommonPort.PubServerPort, CommonPort.SubServerPort);
            this.dataGrid.ItemsSource = hlMessageUE;
            subClient = new SubscribeClient(CommonPort.PubServerPort);
            subClient.AddSubscribeTopic("HlSignalMsg", updateHlSingalMessageInfo);
            subClient.Run();
        }
        private void updateHlSingalMessageInfo(SubscribeMsg msg)
        {

            EventNew UIMsg = new EventNew();
            UIMsg.RawData = msg.Data;
            int maxNumber = 0;
            //按照界面区分自然编号
            maxNumber = hlMessageUE.Count;
            UIMsg.DisplayIndex = maxNumber;
            this.dataGrid.Dispatcher.Invoke(new Action(() => {
                hlMessageUE.Add(UIMsg);
            }));
            return;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = { 0x21, 0x35, 0x22 };
            SubscribeMsg msg = new SubscribeMsg(bytes, "good");
            updateHlSingalMessageInfo(msg);
        }
    }
}
