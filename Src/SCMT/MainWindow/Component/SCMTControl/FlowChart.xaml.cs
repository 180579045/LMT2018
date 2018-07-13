using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Xml.Linq;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// FlowChart.xaml 的交互逻辑
    /// </summary>
    public partial class FlowChart : UserControl
    {
        //Timer tmr;
        FlowChartCommand fcNodeCmd; // 流程图的对应的命令类
        string fileXmlPath = @"..\..\..\Component\SCMTControl\FlowChart.xml";
        private System.Timers.Timer timer = new System.Timers.Timer();//1 * 60 * 1000);// 1min*60s*1000ms
        protected Dictionary<string, XElement> mapCanvasEllipse { get; set; }
        protected Dictionary<string, XElement> mapCanvasTextBlock { get; set; }
        protected Dictionary<string,XElement> mapLine { get; set; }
        protected Dictionary<string,XElement> mapExampleRectangle { get; set; }
        protected Dictionary<string,XElement> mapExampleText { get; set; }

        public FlowChart()
        {
            mapCanvasEllipse = new Dictionary<string, XElement>();
            mapCanvasTextBlock = new Dictionary<string, XElement>();
            mapLine = new Dictionary<string, XElement>();
            mapExampleRectangle = new Dictionary<string, XElement>();
            mapExampleText = new Dictionary<string, XElement>();
            InitializeComponent();
            initInterface();
            paintPicture();

            initGetFlowChartCommand();// 初始化 : 获取每个流程图的对应的命令

            fcNodeCmd.parseFlowChartCmd();
            ////
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }

        /// <summary>
        /// 初始化 : 获取每个流程图的对应的命令
        /// </summary>
        void initGetFlowChartCommand()
        {
            fcNodeCmd = new FlowChartCommand(fileXmlPath);
        }

        /// <summary>
        /// 周期更新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            fcNodeCmd.parseFlowChartCmd();

            //模拟的做一些的操作
            List<string> colorStr = new List<string>() {
                "#FFB5B5B5" ,//未知;
                "#FF00FF00" ,//可用
                "#FFFF0000" ,//故障
                "#FFFFFF00" ,//跳过
            };

            Random ran = new Random();
            BrushConverter brushConverter = new BrushConverter();

            /// 必须使用 this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () {}
            /// 来实现，异线程调用
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                //this.circle5108.Fill = (Brush)brushConverter.ConvertFromString(colorStr[ran.Next(0, 4)]);
            });

            string boardAddr = "172.27.245.92";       // TODO 这里需要使用实际的IP地址
            var cmdName = "GetOmLinkInfo";
            var mibName = "swPackRunningVersion"; //SCMTOperationCore.Message.SNMP
            string index = ".2";

            string csCmdValueTemp = SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
            int aba = 1;
        }

        private void initInterface()
        {
            mapCanvasEllipse.Clear();
            mapCanvasTextBlock.Clear();
            mapLine.Clear();
            mapExampleRectangle.Clear();
            mapExampleText.Clear();


            XDocument document = XDocument.Load(fileXmlPath);
            XElement root = document.Root;
            XElement firttnode1 = (XElement)root.FirstNode;
            XElement firstnode2 = (XElement)firttnode1.FirstNode;
            string bb = firstnode2.Attribute("Stretch").Value;

            IEnumerable<XElement> enumerable = firstnode2.Elements();
            foreach (XElement item in enumerable)
            {
                string dd1 =  item.Name.ToString();
                
                switch(dd1)
                {
                    case "Canvas":
                        //处理Canvas
                        string dd = item.Attribute("Name").Value;
                        keepCanvasElement(dd,item);
                        break;
                    case "Line":
                        //处理line
                        keepLineElement(item);
                        break;
                    case "example":
                        //处理示例图
                        keepExampleElement(item);
                        break;

                    default:
                        break;
                }
            }

        }
        private void keepCanvasElement(string name,XElement element)
        {
            foreach(XElement item in element.Elements())
            {
                string aa = item.Name.ToString();
                switch(aa)
                {
                    case "Ellipse":
                        string bb = item.Attribute("Name").Value;
                        keepCanvasEllipse(name,item);
                        break;
                    case "TextBlock":
                        keepCanvasTextBlock(name,item);
                        break;
                    case "relatedcmd":
                        keepCanvasRelatedcmd(item);
                        break;
                    case "cmdleaf":
                        keepCanvasCmdleaf(item);
                        break;
                    default:
                        break;
                }
            }
        }
        private void keepCanvasEllipse(string name,XElement element)
        {
            mapCanvasEllipse.Add(name, element);
        }
        private void keepCanvasTextBlock(string name,XElement element)
        {
            if(mapCanvasTextBlock.ContainsKey(name))
            {
                name = name + "another";
            }

            mapCanvasTextBlock.Add(name, element);
        }
        private void keepCanvasRelatedcmd(XElement element)
        {
            //string strCmdName = element.Attribute("cmdName").Value;
            //int nPos = strCmdName.IndexOf('|');
            //if (nPos > 0)
            //{
            //    string strCmd1 = strCmdName.Substring(0, nPos);
            //}

        }
        private void keepCanvasCmdleaf(XElement element)
        {
            string leafName = element.Attribute("leafName").Value;
        }
        private void keepLineElement(XElement element)
        {
            string lineName = element.Attribute("Name").Value;
            mapLine.Add(lineName, element);
        }
        private void keepExampleElement(XElement element)
        {
            foreach(XElement item in element.Elements())
            {
                string aa = item.Name.ToString();
                switch(aa)
                {
                    case "Rectangle":
                        keepExampleRectangle(item);
                        break;
                    case "TextBlock":
                        keepExampleTextBlock(item);
                        break;
                    default:
                        break;
                }
            }
        }
        private void keepExampleRectangle(XElement element)
        {
            string strName = element.Attribute("Name").Value;
            mapExampleRectangle.Add(strName, element);
        }
        private void keepExampleTextBlock(XElement element)
        {
            string strName = element.Attribute("Name").Value;
            mapExampleText.Add(strName, element);
        }
        private void paintPicture()
        {
            mygrid.Children.Clear();
            Viewbox vie = new Viewbox();
            
            mygrid.Children.Add(vie);

            Canvas bigCanv = new Canvas();
            bigCanv.Width = 700;
            bigCanv.Height = 800;
            vie.Child = bigCanv;


            //画圆
            paintCanvasEllipse(bigCanv);
            //写圆中的文字
            paintCanvasTextBlock(bigCanv);
            //画连接圆的线
            paintLine(bigCanv);
            //画示例图的矩形
            paintExampleRectangle(bigCanv);
            //写示例图的文字
            paintExampleText(bigCanv);

        }
        private void paintCanvasEllipse(Canvas basicCanv)
        {
            foreach (var item in mapCanvasEllipse)
            {
                string name = item.Key;
                XElement element = item.Value;
                string lefttoEllipse = element.Attribute("Canvas.Left").Value;
                string toptoEllipse = element.Attribute("Canvas.Top").Value;
                string widthEllipse = element.Attribute("Width").Value;
                string hightEllipse = element.Attribute("Height").Value;
                string nameEllipse = element.Attribute("Name").Value;
                string colorFillEllipse = element.Attribute("Fill").Value;
                string colorStrokeEllipse = element.Attribute("Stroke").Value;
                string matrixEllipse = "";
                foreach (XElement item1 in element.Elements())
                {
                    foreach (XElement item2 in item1.Elements())
                    {
                        matrixEllipse = item2.Attribute("Matrix").Value;
                    }
                }

                Canvas canv = new Canvas();
                canv.Name = name;

                Ellipse mynew = new Ellipse();
                mynew.Width = Convert.ToDouble(widthEllipse);
                mynew.Height = Convert.ToDouble(hightEllipse);
                mynew.Name = nameEllipse;
                BrushConverter brushConverter = new BrushConverter();
                mynew.Fill = (System.Windows.Media.Brush)brushConverter.ConvertFromString(colorFillEllipse);
                mynew.Stroke = (System.Windows.Media.Brush)brushConverter.ConvertFromString(colorStrokeEllipse);
                mynew.SetValue(Canvas.LeftProperty, Convert.ToDouble(lefttoEllipse));
                mynew.SetValue(Canvas.TopProperty, Convert.ToDouble(toptoEllipse));

                canv.Children.Add(mynew);
                basicCanv.Children.Add(canv);
            }
        }
        private void paintCanvasTextBlock(Canvas basicCanv)
        {
            foreach (var item in mapCanvasTextBlock)
            {
                string name = item.Key;
                XElement element = item.Value;

                string stringText = element.Value;
                string textFontSize = element.Attribute("FontSize").Value;
                string textFontFamily = element.Attribute("FontFamily").Value;
                string textForeground = element.Attribute("Foreground").Value;
                string textCanvasLeft = element.Attribute("Canvas.Left").Value;
                string textCanvasTop = element.Attribute("Canvas.Top").Value;
                string textName = element.Attribute("Name").Value;

                Canvas canv = new Canvas();
                TextBlock mytext = new TextBlock();
                mytext.Text = stringText;
                mytext.FontSize = Convert.ToDouble(textFontSize);
                FontFamilyConverter fontFamilyConverter = new FontFamilyConverter();
                mytext.FontFamily = (System.Windows.Media.FontFamily)fontFamilyConverter.ConvertFromString(textFontFamily);
                BrushConverter brushConverter = new BrushConverter();
                mytext.Foreground = (System.Windows.Media.Brush)brushConverter.ConvertFromString(textForeground);
                mytext.SetValue(Canvas.LeftProperty, Convert.ToDouble(textCanvasLeft));
                mytext.SetValue(Canvas.TopProperty, Convert.ToDouble(textCanvasTop));
                mytext.Name = textName;

                canv.Children.Add(mytext);
                basicCanv.Children.Add(canv);
            }
        }
        private void paintLine(Canvas basicCanv)
        {
            foreach (var item in mapLine)
            {
                string name = item.Key;
                XElement element = item.Value;

                string lineX1 = element.Attribute("X1").Value;
                string lineY1 = element.Attribute("Y1").Value;
                string lineX2 = element.Attribute("X2").Value;
                string lineY2 = element.Attribute("Y2").Value;
                string lineName = element.Attribute("Name").Value;
                string lineFill = element.Attribute("Fill").Value;
                string lineStroke = element.Attribute("Stroke").Value;

                Canvas canv = new Canvas();
                Line myline = new Line();
                myline.X1 = Convert.ToDouble(lineX1);
                myline.Y1 = Convert.ToDouble(lineY1);
                myline.X2 = Convert.ToDouble(lineX2);
                myline.Y2 = Convert.ToDouble(lineY2);
                myline.Name = lineName;
                BrushConverter brushConverter = new BrushConverter();
                myline.Fill = (System.Windows.Media.Brush)brushConverter.ConvertFromString(lineFill);
                myline.Stroke = (System.Windows.Media.Brush)brushConverter.ConvertFromString(lineStroke);

                canv.Children.Add(myline);
                basicCanv.Children.Add(canv);
            }
        }
        private void paintExampleRectangle(Canvas basicCanv)
        {
            foreach (var item in mapExampleRectangle)
            {
                string name = item.Key;
                XElement element = item.Value;

                string strCanvasLeft = element.Attribute("Canvas.Left").Value;
                string strCanvasTop = element.Attribute("Canvas.Top").Value;
                string strWidth = element.Attribute("Width").Value;
                string strHeight = element.Attribute("Height").Value;
                string strName = element.Attribute("Name").Value;
                string strFill = element.Attribute("Fill").Value;
                string strStroke = element.Attribute("Stroke").Value;

                Canvas canv = new Canvas();
                System.Windows.Shapes.Rectangle myrectangle = new System.Windows.Shapes.Rectangle();
                myrectangle.SetValue(Canvas.LeftProperty, Convert.ToDouble(strCanvasLeft));
                myrectangle.SetValue(Canvas.TopProperty, Convert.ToDouble(strCanvasTop));
                myrectangle.Width = Convert.ToDouble(strWidth);
                myrectangle.Height = Convert.ToDouble(strHeight);
                myrectangle.Name = strName;
                BrushConverter brushConverter = new BrushConverter();
                myrectangle.Fill = (System.Windows.Media.Brush)brushConverter.ConvertFromString(strFill);
                myrectangle.Stroke = (System.Windows.Media.Brush)brushConverter.ConvertFromString(strStroke);

                canv.Children.Add(myrectangle);
                basicCanv.Children.Add(canv);
            }
        }
        private void paintExampleText(Canvas basicCanv)
        {
            foreach (var item in mapExampleText)
            {
                string name = item.Key;
                XElement element = item.Value;

                string strFontSize = element.Attribute("FontSize").Value;
                string strFontFamily = element.Attribute("FontFamily").Value;
                string strForeground = element.Attribute("Foreground").Value;
                string strCanvasLeft = element.Attribute("Canvas.Left").Value;
                string strCanvasTop = element.Attribute("Canvas.Top").Value;
                string strName = element.Attribute("Name").Value;
                string strText = element.Value;

                Canvas canv = new Canvas();
                TextBlock myExampleText = new TextBlock();
                myExampleText.FontSize = Convert.ToDouble(strFontSize);
                FontFamilyConverter fontFamilyConverter = new FontFamilyConverter();
                myExampleText.FontFamily = (System.Windows.Media.FontFamily)fontFamilyConverter.ConvertFromString(strFontFamily);
                BrushConverter brushConverter = new BrushConverter();
                myExampleText.Foreground = (System.Windows.Media.Brush)brushConverter.ConvertFromString(strForeground);
                myExampleText.SetValue(Canvas.LeftProperty, Convert.ToDouble(strCanvasLeft));
                myExampleText.SetValue(Canvas.TopProperty, Convert.ToDouble(strCanvasTop));
                myExampleText.Name = strName;
                myExampleText.Text = strText;

                canv.Children.Add(myExampleText);
                basicCanv.Children.Add(canv);
            }
        }
        //修改圆圈颜色
        private void modCanvasEllipse(Dictionary<string,string> modDic)
        {
            foreach(var item in modDic)
            {
                string name = item.Key;
                string strFill = item.Value;
                if (mapCanvasEllipse.ContainsKey(name))
                {
                    mapCanvasEllipse[name].SetAttributeValue("Fill", strFill);
                }
            }

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

    class FlowChartCommand
    {
        /// <summary>
        /// 每一个流程图中命令内容
        /// Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>> : Dictionary<流程图名, 对应的命令内容(可有多个命令)>
        /// Dictionary<string, List<Dictionary<string, string>>> ; Dictionary<命令名字，命令的所有叶子>
        /// List<Dictionary<string, string>> : List<叶子节点的所有属性>
        /// Dictionary<string, string> : key: leafName叶子名,leafValue叶子节点,leafProperty叶子属性,leafCompRules叶子比较规则
        /// </summary>
        private Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>> fcCmd = new Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>>();

        /// <summary>
        /// 初始化 : 从 xml中解析想要的信息
        /// </summary>
        /// <param name="nodeList"></param>
        public FlowChartCommand(string xmlFilePath)
        {
            /// 1.
            if (String.Equals("", xmlFilePath))
                return;

            /// 2. 查找<Viewbox>下面的内容   
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);//@"..\..\..\Component\SCMTControl\FlowChart.xml".
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("UserControl/Grid/Viewbox").ChildNodes;
            if (null == nodeList)
                return;

            /// 3. 分析命令内容
            foreach (XmlNode xn in nodeList)
            {
                /// 3.1. 容错判断 1. 必须是 Canvas 2. Canvas中必须有椭圆属性"Ellipse"
                if ((!String.Equals(xn.Name, "Canvas")) || (null == xn.SelectSingleNode("Ellipse")))
                {
                    continue;
                }
                /// 3.2. 获取命令相关内容
                /// 3.2.1. 必须有relatedcmd属性
                XmlNode cmds = xn.SelectSingleNode("relatedcmd");
                /// 3.2.2. 获取相关命令信息
                string flowChartName = xn.Attributes["Name"].Value; /// 用于操作图形的句柄
                parseXml(flowChartName, cmds); 
            }
        }
        
        /// <summary>
        /// 从 xml中解析出每个流程图的相关命令
        /// </summary>
        /// <param name="flowChartName">流程图的名字</param>
        /// <param name="cmds">流程的命令内容</param>
        private void parseXml(string flowChartName, XmlNode cmds)
        {
            Dictionary<string, List<Dictionary<string, string>>> cmdList;
            if (null == cmds)
            {
                cmdList = null;
                return;
            }
            else
            {
                /// 2. 获取相关命令信息
                cmdList = parseCommandList(cmds);
            }
            fcCmd.Add(flowChartName, cmdList);
        }

        /// <summary>
        /// 解析所有命令内容
        /// </summary>
        /// <param name="cmds"></param>
        /// <returns></returns>
        private Dictionary<string, List<Dictionary<string, string>>> parseCommandList(XmlNode cmds)
        {
            Dictionary<string, List<Dictionary<string, string>>> cmdList = new Dictionary<string, List<Dictionary<string, string>>>();
            ///解析每个命令
            foreach (XmlNode cmd in cmds.ChildNodes)
            {
                List<Dictionary<string, string>> leafList = new List<Dictionary<string, string>>();
                /// 解析每个节点
                foreach (XmlNode leaf in cmd.ChildNodes)
                {
                    leafList.Add(parseIndexNum(leaf));
                }                
                ///
                string cmdName = cmd.Attributes["name"].Value;
                cmdList.Add(cmdName, leafList);
            }
            return cmdList;
        }

        private Dictionary<string, string> parseIndexNum(XmlNode leaf)
        {
            Dictionary<string, string> leafInfo = new Dictionary<string, string>()
            {
                { "leafName", leaf.Attributes["name"].Value },
                { "leafValue", leaf.Attributes["value"].Value },
                { "leafProperty", leaf.Attributes["property"].Value },
                { "leafCompRules", leaf.Attributes["compRules"].Value},
                { "indexNum", leaf.Attributes["indexNum"].Value }
            };
            /// index1~x
            for (int no = 0; no < int.Parse(leaf.Attributes["indexNum"].Value); no++)
            {
                string index = String.Format("index{0}", no + 1);
                leafInfo.Add(index, leaf.Attributes[index].Value);
            }
            return leafInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdContainer"></param>
        /// <returns></returns>
        public int parseCmdInfo(Dictionary<string, List<Dictionary<string, string>>> cmdContainer)
        {
            if (null == cmdContainer)
                return -1;

            List<string> cmds = new List<string>();
            foreach (var cmd in cmdContainer.Keys)
            {
                cmds.Add(cmd);
            }

            foreach (var cmd in cmds)
            {
                string cmdName = cmd;
                List<Dictionary<string, string>> leafInfoList = cmdContainer[cmd];
                foreach (var leafDict in leafInfoList)
                {
                    /// leafDict: key: leafName 叶子名,leafValue 叶子节点,leafProperty 叶子属性,leafCompRules 叶子比较规则
                    string boardAddr = "172.27.245.92";       // TODO 这里需要使用实际的IP地址
                    //var cmdName = "GetOmLinkInfo";
                    //var mibName = "swPackRunningVersion"; //SCMTOperationCore.Message.SNMP
                    string mibName = leafDict["leafName"];

                    int indexNum = int.Parse(leafDict["indexNum"]);
                    for (int no = 0; no < indexNum; no++)
                    {
                        string strIndexNo = String.Format("index{0}", no + 1);
                        string strIndexValue = leafDict[strIndexNo];

                        /// 好多的好多。。。。
                        string index = getIndexString(strIndexNo, strIndexValue);

                        string csCmdValueTemp = SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
                        if (String.Empty == csCmdValueTemp)
                        {
                            
                        }
                    }
                    //string index = ".2";

                    //string csCmdValueTemp = SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);

                }
            }

            

            return 0;
        }
        private string getIndexString(string indexNo, string indexValue)
        {
            return "";
        }

        public void parseFlowChartCmd()
        {
            if (null == fcCmd)
                return;

            var keys = fcCmd.Keys;

            foreach (var key in fcCmd.Keys)
            {
                int a = parseCmdInfo(fcCmd[key]);
            }
        }

    }


}
