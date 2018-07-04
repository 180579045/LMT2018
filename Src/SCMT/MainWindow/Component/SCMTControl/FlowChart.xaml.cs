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
using System.Xml;
using System.Timers;
//using System.WindowsBase;
using System.Windows.Threading;
using System.Threading;
using SCMTOperationCore.Message.SNMP;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// FlowChart.xaml 的交互逻辑
    /// </summary>
    public partial class FlowChart : UserControl
    {
        //Timer tmr;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private List<FlowChartNode> FlowChartNL = new List<FlowChartNode>();

        public FlowChart()
        {
            InitializeComponent();

            set123();

            ////
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }

        void set123()
        {
            FlowChartNode node = new FlowChartNode();

            FlowChartNL.Add(node);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //模拟的做一些的操作
            List<string> colorStr = new List<string>() {
                "#FFB5B5B5" ,//未知;
                "#FF00FF00" ,//可用
                "#FFFF0000" ,//故障
                "#FFFFFF00" ,//跳过
            };

            Random ran = new Random(); ;
            BrushConverter brushConverter = new BrushConverter();

            /// 必须使用 this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () {}
            /// 来实现，异线程调用
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () {
                this.circle5108.Fill = (Brush)brushConverter.ConvertFromString(colorStr[ran.Next(0, 4)]);
            });

            string boardAddr = "172.27.245.92";       // TODO 这里需要使用实际的IP地址
            var cmdName = "GetOmLinkInfo";
            var mibName = "swPackRunningVersion"; //SCMTOperationCore.Message.SNMP
            string index = ".2";

            string csCmdValueTemp = SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
            int aba = 1;
        }

        void test()
        {

//            < relatedcmd cmdName = "GetOmLinkInfo:2" />
 
//             < cmdleaf leafName = "*equipStartupStage-NLT" leafValue = "3" />
    
//                < cmdleaf leafName = "*omLinkSetupStatus-NEQ" leafValue = "2" />
       
//                   < cmdleaf leafName = "equipStartupStage-NLT" leafValue = "3" />
          
//                      < cmdleaf leafName = "omLinkSetupStatus" leafValue = "2" />
//             wangxiaoying 10:40:44
//OM链路建立

        }


    }

    /// <summary>
    /// 每一个流程图节点中的内容
    /// </summary>
    class FlowChartNode
    {
        /// <summary>
        /// 每个节点的编号
        /// </summary>
        int nodeNo;                           // 第几个流程图

        /// <summary>
        /// 索引相关内容
        /// </summary>
        List<                                 // List.No     : 索引的顺序
            Dictionary<                       // List.value  : 索引的内容
                int,                          // Dict.key    : 索引编号
                string>>                      // Dict.value  : 索引信息 1.2.5105.x.y.....
            index;                            // 索引数量<Dict<索引编号, 索引值>>

        /// <summary>
        /// 命令相关内容
        /// </summary>
        List<                                 // List.No     : 可能有n个命令，每个命令都有下发的顺序.
            Dictionary<                       // List.value  : 每个命令相关的内容，节点信息，节点阈值.
            string,                           // Dict1.key   : 命令名字
            Dictionary<                       // Dict1.value : 命令中叶子节点信息(Dict2)
                string,                       // Dict2.key   : 叶子节点名字
                string>>>                     // Dict2.value : 叶子节点阈值
            cmd;                              // 

        public FlowChartNode()
        {
            this.nodeNo = -1;
            //this.cmdList = new Dictionary<int, string>();
            //this.cmdNode = new Dictionary<string, Dictionary<string, string>>();
        }
        public FlowChartNode(int nodeNo)
        {
            this.nodeNo = nodeNo;
            //this.cmdList = new Dictionary<int, string>();
            //this.cmdNode = new Dictionary<string, Dictionary<string, string>>();
        }

        void setFlowChartNodeColor()
        {

        }

        /// <summary>
        /// 判断条件
        /// </summary>
        /// <returns>-1:程序错误;0:未知;1;绿色;2:红色</returns>
        int setWhichColor()
        {
            
            int reColor = 0;



            return reColor;
        }

    }
   
}
