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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CDLBrowser.Parser.BPLAN;
using MsgQueue;
using SCMTMainWindow.Controls.PlanBParser;
using Newtonsoft.Json;
using CommonUility;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// MessageRecvMayi.xaml 的交互逻辑
    /// </summary>
    public partial class MessageRecvMayi : UserControl
    {
        //全局变量，定义3个集合，分别表示  UE，ENB， GNB 的信息
        public ObservableCollection<ScriptMessage> UE_List = new ObservableCollection<ScriptMessage>();
        public ObservableCollection<ScriptMessage> ENB_List = new ObservableCollection<ScriptMessage>();
        public ObservableCollection<ScriptMessage> GNB_List = new ObservableCollection<ScriptMessage>();

        public SignalBPlan signalB;

        public MessageRecvMayi()
        {
            InitializeComponent();

            //初始化  三个  ListView
            InitListView();

            //设置  ListView  的数据绑定，  整体都绑定到一个集合
            lvUE.ItemsSource = UE_List;
            lveNB.ItemsSource = ENB_List;
            lvgNB.ItemsSource = GNB_List;

            //实例化  ，并注册回调函数  （ C#中应该叫做  委托  好一些）
            signalB = new SignalBPlan();
            SubscribeHelper.AddSubscribe("HlSignalMsg", StartTraceByUI);
        }

        /// <summary>
        /// 开始跟踪  按钮  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTrace_Click(object sender, RoutedEventArgs e)
        {
            SignalBConfig.StartByScriptXml();
            PublishHelper.PublishMsg("StartTraceHlSignal", "");
        }

        /// <summary>
        /// 回调函数，或者叫做委托函数，在初始化中注册，接收消息并显示到界面上
        /// </summary>
        /// <param name="msg"></param>
        private void StartTraceByUI(SubscribeMsg msg)
        {
            ScriptMessage scriptMessage = JsonHelper.SerializeJsonToObject<ScriptMessage>(msg.Data);

            if (-1 != scriptMessage.UI.IndexOf("UE"))
            {
                this.lvUE.Dispatcher.Invoke(new Action(() => {
                    UE_List.Add(scriptMessage);
                }));
            }
            if (-1 != scriptMessage.UI.IndexOf("eNB"))
            {
                this.lveNB.Dispatcher.Invoke(new Action(() => {
                    ENB_List.Add(scriptMessage);
                }));
            }
            if (-1 != scriptMessage.UI.IndexOf("gNB"))
            {
                this.lvgNB.Dispatcher.Invoke(new Action(() => {
                    GNB_List.Add(scriptMessage);
                }));
            }
        }

        /// <summary>
        /// ListView  的  选择改变事件，选择不同的 item 时，显示各自的 data 节点信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvUE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvUE.SelectedItem == null)
            {
                return;
            }
            ScriptMessage thisMsg = (ScriptMessage)lvUE.SelectedItem;

            lbUE.Items.Clear();
            lbUE.Items.Add(thisMsg.data);
        }
        /// <summary>
        /// ListView  的  选择改变事件，选择不同的 item 时，显示各自的 data 节点信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lveNB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lveNB.SelectedItem == null)
            {
                return;
            }
            ScriptMessage thisMsg = (ScriptMessage)lveNB.SelectedItem;

            lbeNB.Items.Clear();
            lbeNB.Items.Add(thisMsg.data);
        }
        /// <summary>
        /// ListView  的  选择改变事件，选择不同的 item 时，显示各自的 data 节点信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvgNB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvgNB.SelectedItem == null)
            {
                return;
            }
            ScriptMessage thisMsg = (ScriptMessage)lvgNB.SelectedItem;

            lbgNB.Items.Clear();
            lbgNB.Items.Add(thisMsg.data);
        }
        /// <summary>
        /// 初始化  三个  ListView   并绑定数据
        /// </summary>
        private void InitListView()
        {
            //设置  ListView  的  GridView
            GridView gvUE = new GridView();
            GridView gvENB = new GridView();
            GridView gvGNB = new GridView();

            //设置具体的每一列  UE
            GridViewColumn colum = new GridViewColumn();
            colum.Header = "No";
            colum.Width = 30;
            colum.DisplayMemberBinding = new Binding("NO");
            gvUE.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "ID";
            colum.Width = 60;
            colum.DisplayMemberBinding = new Binding("ENBUEID");
            gvUE.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "MsgSource";
            colum.Width = 80;
            colum.DisplayMemberBinding = new Binding("MessageSource");
            gvUE.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "MsgDestination";
            colum.Width = 85;
            colum.DisplayMemberBinding = new Binding("MessageDestination");
            gvUE.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "Msg";
            colum.Width = 125;
            colum.DisplayMemberBinding = new Binding("message");
            gvUE.Columns.Add(colum);

            lvUE.View = gvUE;

            //设置具体的每一列  enb
            colum = new GridViewColumn();
            colum.Header = "No";
            colum.Width = 30;
            colum.DisplayMemberBinding = new Binding("NO");
            gvENB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "ID";
            colum.Width = 60;
            colum.DisplayMemberBinding = new Binding("ENBUEID");
            gvENB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "MsgSource";
            colum.Width = 80;
            colum.DisplayMemberBinding = new Binding("MessageSource");
            gvENB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "MsgDestination";
            colum.Width = 85;
            colum.DisplayMemberBinding = new Binding("MessageDestination");
            gvENB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "Msg";
            colum.Width = 125;
            colum.DisplayMemberBinding = new Binding("message");
            gvENB.Columns.Add(colum);

            lveNB.View = gvENB;

            //设置具体的每一列  gnb
            colum = new GridViewColumn();
            colum.Header = "No";
            colum.Width = 30;
            colum.DisplayMemberBinding = new Binding("NO");
            gvGNB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "ID";
            colum.Width = 60;
            colum.DisplayMemberBinding = new Binding("ENBUEID");
            gvGNB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "MsgSource";
            colum.Width = 80;
            colum.DisplayMemberBinding = new Binding("MessageSource");
            gvGNB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "MsgDestination";
            colum.Width = 85;
            colum.DisplayMemberBinding = new Binding("MessageDestination");
            gvGNB.Columns.Add(colum);

            colum = new GridViewColumn();
            colum.Header = "Msg";
            colum.Width = 125;
            colum.DisplayMemberBinding = new Binding("message");
            gvGNB.Columns.Add(colum);

            lvgNB.View = gvGNB;
        }

        /// <summary>
        /// 主界面进行隐藏时调用，清空所有信息，并发送停止消息
        /// </summary>
        public void ClearAll()
        {
            UE_List.Clear();
            ENB_List.Clear();
            GNB_List.Clear();
            lbUE.Items.Clear();
            lbeNB.Items.Clear();
            lbgNB.Items.Clear();

            PublishHelper.PublishMsg("StopTraceHlSignal", "");
        }
    }
}
