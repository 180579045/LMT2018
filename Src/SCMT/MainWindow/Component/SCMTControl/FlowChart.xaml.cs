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
using System.Windows.Threading;
using System.Threading;
using SCMTOperationCore.Message.SNMP;
using System.Xml.Linq;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// 委托 : 设置流程图的颜色
    /// </summary>
    /// <param name="result">初始化成功,true;失败,false</param>
    public delegate void SetColorForFlowChart(Dictionary<string, string> modDic);
    /// <summary>
    /// FlowChart.xaml 的交互逻辑
    /// </summary>
    public partial class FlowChart : UserControl
    {
        //Timer tmr;
        FlowChartCommand fcNodeCmd; // 流程图的对应的命令类
        string fileXmlPath = @"..\..\..\Component\SCMTControl\FlowChart.xml";
        private System.Timers.Timer timer = new System.Timers.Timer(1 * 60 * 1000);//1 * 60 * 1000);// 1min*60s*1000ms
        protected Dictionary<string, XElement> mapCanvasEllipse { get; set; }//保存Canvas的Ellipse信息
        protected Dictionary<string, XElement> mapCanvasTextBlock { get; set; }//保存Canvas的TextBlock信息
        protected Dictionary<string, XElement> mapLine { get; set; }//保存Line信息
        protected Dictionary<string, XElement> mapExampleRectangle { get; set; }//保存示例图的矩形信息
        protected Dictionary<string, XElement> mapExampleText { get; set; }//保存示例图的文字信息
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

            initflowChartCmdGetFlowChartCommand();// 初始化 : 获取每个流程图的对应的命令

            flowChartCmdSetFlowChartColor();//设置
            ////
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }

        /// <summary>
        /// 初始化 : 获取每个流程图的对应的命令
        /// </summary>
        void initflowChartCmdGetFlowChartCommand()
        {
            // dataHandle.resultInitData = new ResultInitData(ResultInitData);
            fcNodeCmd = new FlowChartCommand(fileXmlPath);
            // 设置流程图颜色
            fcNodeCmd.setColor = new SetColorForFlowChart(flowChartCmdModCanvasEllipse);
        }

        /// <summary>
        /// 设置颜色
        /// </summary>
        void flowChartCmdSetFlowChartColor()
        {
            fcNodeCmd.LightFlowChartsNoThread();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modDic"></param>
        void flowChartCmdModCanvasEllipse(Dictionary<string, string> modDic)
        {
            modCanvasEllipse(modDic);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                paintPicture();
                //this.circle5108.Fill = (Brush)brushConverter.ConvertFromString(colorStr[ran.Next(0, 4)]);
            });
            //paintPicture();
        }

        /// <summary>
        /// 周期更新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("=====start timer set color.");
            flowChartCmdSetFlowChartColor();
            ///// 必须使用 this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () {}
            ///// 来实现，异线程调用
            //this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            //{
            //    //this.circle5108.Fill = (Brush)brushConverter.ConvertFromString(colorStr[ran.Next(0, 4)]);
            //});
        }
        /// <summary>
        /// 初始化界面信息
        /// </summary>
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
                string dd1 = item.Name.ToString();

                switch (dd1)
                {
                    case "Canvas":
                        //处理Canvas
                        string dd = item.Attribute("Name").Value;
                        keepCanvasElement(dd, item);
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
        /// <summary>
        /// 保存Canvas内的相关信息
        /// </summary>
        /// <param name="name">Canvas的Name</param>
        /// <param name="element">Canvas下的所有元素</param>
        private void keepCanvasElement(string name, XElement element)
        {
            foreach (XElement item in element.Elements())
            {
                string aa = item.Name.ToString();
                switch (aa)
                {
                    case "Ellipse":
                        string bb = item.Attribute("Name").Value;
                        keepCanvasEllipse(name, item);
                        break;
                    case "TextBlock":
                        keepCanvasTextBlock(name, item);
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
        /// <summary>
        /// 保存Canvas的Ellipse信息
        /// </summary>
        /// <param name="name">Canvas的Name</param>
        /// <param name="element">Ellipse的元素</param>
        private void keepCanvasEllipse(string name, XElement element)
        {
            mapCanvasEllipse.Add(name, element);
        }
        /// <summary>
        /// 保存Canvas的TextBlock信息
        /// </summary>
        /// <param name="name">Canvas的Name</param>
        /// <param name="element">TextBlock的元素</param>
        private void keepCanvasTextBlock(string name, XElement element)
        {
            if (mapCanvasTextBlock.ContainsKey(name))
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
        /// <summary>
        /// 保存Line的信息
        /// </summary>
        /// <param name="element">Line的信息</param>
        private void keepLineElement(XElement element)
        {
            string lineName = element.Attribute("Name").Value;
            mapLine.Add(lineName, element);
        }
        /// <summary>
        /// 保存示例图的信息
        /// </summary>
        /// <param name="element">Example的元素</param>
        private void keepExampleElement(XElement element)
        {
            foreach (XElement item in element.Elements())
            {
                string aa = item.Name.ToString();
                switch (aa)
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
        /// <summary>
        /// 保存示例图中矩形信息
        /// </summary>
        /// <param name="element">矩形元素</param>
        private void keepExampleRectangle(XElement element)
        {
            string strName = element.Attribute("Name").Value;
            mapExampleRectangle.Add(strName, element);
        }
        /// <summary>
        /// 保存示例图中文字信息
        /// </summary>
        /// <param name="element">文字元素</param>
        private void keepExampleTextBlock(XElement element)
        {
            string strName = element.Attribute("Name").Value;
            mapExampleText.Add(strName, element);
        }
        /// <summary>
        /// 画图
        /// </summary>
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
        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="basicCanv">底板Canvas</param>
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
        /// <summary>
        /// 写文字信息
        /// </summary>
        /// <param name="basicCanv">底板Canvas</param>
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
        /// <summary>
        /// 画连接圆之间的线
        /// </summary>
        /// <param name="basicCanv">底板Canvas</param>
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
        /// <summary>
        /// 画示例图的矩形
        /// </summary>
        /// <param name="basicCanv">底板Canvas</param>
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
        /// <summary>
        /// 画示例图的文字
        /// </summary>
        /// <param name="basicCanv">底板Canvas</param>
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
        /// <summary>
        /// 修改圆圈颜色
        /// </summary>
        /// <param name="modDic">要修改的圆的Canvas的Name和颜色的集合</param>
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

    class FlowChartCommand
    {
        /// <summary>
        /// [内存]每一个流程图中命令内容
        /// Dictionary<string, List<Dictionary<string, string>>> : Dictionary<流程图名, 对应的节点内容>
        /// List<Dictionary<string, string>> : List<叶子节点的所有属性>
        /// Dictionary<string, string> : key: leafName叶子名,leafValue叶子节点,leafProperty叶子属性,leafCompRules叶子比较规则
        /// </summary>
        private Dictionary<string, List<Dictionary<string, string>>> fcCmd = new Dictionary<string, List<Dictionary<string, string>>>();
        /// <summary>
        /// 引用外部函数
        /// </summary>
        public SetColorForFlowChart setColor;

        #region 0:解析xml信息.1.从xml解析每个流程图的cmdsInfo;->2.解析指定流程图的FlowChartOfCmdsInfo;->3.解析每个cmd的CmdOfLeafsInfo;->4.解析leafInfo属性.
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

            /// 3. 分析命令内容 : 遍历解析每个流程图命令内容
            foreach (XmlNode xn in nodeList)
            {
                /// 3.1. 容错判断 1. 必须是 Canvas 2. Canvas中必须有椭圆属性"Ellipse"
                if ((!String.Equals(xn.Name, "Canvas")) || (null == xn.SelectSingleNode("Ellipse")))
                {
                    continue;
                }
                /// 3.2. 获取命令相关内容
                /// 3.2.1. 必须有relatedcmd属性
                XmlNode cmd = xn.SelectSingleNode("relatedcmd");
                /// 3.2.2. 解析每个流程图的信息
                string flowChartName = xn.Attributes["Name"].Value; /// 用于操作图形的句柄
                fccFromXmlParseFlowChartOfCmdInfo(flowChartName, cmd); ///  
                // xml 结构变化 去掉 命令cmd相关内容
                //XmlNode cmds = xn.SelectSingleNode("relatedcmd");
                ///// 3.2.2. 解析每个流程图的信息
                //string flowChartName = xn.Attributes["Name"].Value; /// 用于操作图形的句柄
                //fccFromXmlParseFlowChartOfCmdsInfo(flowChartName, cmds); ///  
                //fccFromXmlParseFlowChartOfCmdsInfo(flowChartName, cmd); ///  
            }
        }

        /// <summary>
        /// 从 xml 中解析出每个流程图的相关命令 cmds(一个)
        /// </summary>
        /// <param name="flowChartName">流程图的名字</param>
        /// <param name="cmds">流程的命令内容</param>
        private void fccFromXmlParseFlowChartOfCmdsInfo(string flowChartName, XmlNode cmds)
        {
            Dictionary<string, List<Dictionary<string, string>>> cmdList;
            if (null != cmds)
            {
                /// 2. 获取相关命令信息
                cmdList = new Dictionary<string, List<Dictionary<string, string>>>();
                foreach (XmlNode cmd in cmds.ChildNodes)
                {
                    List<Dictionary<string, string>> leafList = fccFromCommandsParseCmdOfLeafsList(cmd);
                    ///
                    string cmdName = cmd.Attributes["name"].Value;
                    cmdList.Add(cmdName, leafList);
                }
                // fcCmd 数据类型变化
                // fcCmd.Add(flowChartName, cmdList);
            }

            return;
        }
        /// <summary>
        /// 从 xml 中解析出每个流程图的相关命令 cmds(一个)
        /// </summary>
        /// <param name="flowChartName">流程图的名字</param>
        /// <param name="cmds">流程的命令内容</param>
        private void fccFromXmlParseFlowChartOfCmdInfo(string flowChartName, XmlNode cmd)
        {
            if (null != cmd)
            {
                /// 2. 获取相关命令信息
                List<Dictionary<string, string>> leafList = fccFromCommandsParseCmdOfLeafsList(cmd);
                fcCmd.Add(flowChartName, leafList);
            }
            return;
        }

        /// <summary>
        /// 解析所有命令内容
        /// </summary>
        /// <param name="cmds"></param>
        /// <returns></returns>
        [Obsolete("Use Method List<Dictionary<string, string>> fccFromCommandsParseForLeafList ]")]
        private Dictionary<string, List<Dictionary<string, string>>> fccFromCommandsParseCommandForListOld(XmlNode cmds)
        {
            Dictionary<string, List<Dictionary<string, string>>> cmdList = new Dictionary<string, List<Dictionary<string, string>>>();
            ///解析每个命令
            foreach (XmlNode cmd in cmds.ChildNodes)
            {
                List<Dictionary<string, string>> leafList = new List<Dictionary<string, string>>();
                /// 解析每个节点
                foreach (XmlNode leaf in cmd.ChildNodes)
                {
                    leafList.Add(fccFromCmdParseLeaf(leaf));
                }
                ///
                string cmdName = cmd.Attributes["name"].Value;
                cmdList.Add(cmdName, leafList);
            }
            return cmdList;
        }

        /// <summary>
        /// 解析每个 cmd 中的 leaf 信息
        /// </summary>
        /// <param name="cmd">每个命令及其他叶子信息</param>
        /// <returns></returns>
        private List<Dictionary<string, string>> fccFromCommandsParseCmdOfLeafsList(XmlNode cmd)
        {
            List<Dictionary<string, string>> leafList = new List<Dictionary<string, string>>();
            /// 解析每个节点
            foreach (XmlNode leaf in cmd.ChildNodes)
            {
                leafList.Add(fccFromCmdParseLeaf(leaf));
            }
            return leafList;
        }

        /// <summary>
        /// 解析每个 leaf 信息的属性
        /// </summary>
        /// <param name="leaf">cmd中的leaf</param>
        /// <returns></returns>
        private Dictionary<string, string> fccFromCmdParseLeaf(XmlNode leaf)
        {
            return new Dictionary<string, string>()
            {
                { "leafName", leaf.Attributes["name"].Value },
                { "leafValue", leaf.Attributes["value"].Value },
                { "leafProperty", leaf.Attributes["property"].Value },
                { "leafCompRules", leaf.Attributes["compRules"].Value},
            };
        }
        #endregion

        #region 点亮每个流程图颜色
        /// <summary>
        /// 循环解析每个流程图应该的颜色
        ///  "#FFB5B5B5" ,//未知;"#FF00FF00" ,//可用
        ///  "#FFFF0000" ,//故障;"#FFFFFF00" ,//跳过
        /// </summary>
        public Dictionary<string, string> LightFlowCharts()
        {
            string whichWay = "wayOne";// "wayTwo"
            if (null == fcCmd)
                return null;

            Dictionary<string, string> flowChartColor = new Dictionary<string, string>();

            //// 方案一 ： 顺序点亮
            if (String.Equals(whichWay, "wayOne"))
            {
                foreach (var flowChartName in fcCmd.Keys)
                {
                    string setColor = parseCmdInfo(flowChartName.ToString());
                    Console.WriteLine("flowChartName=({0}), setColor=({1})", flowChartName, setColor);
                    flowChartColor.Add(flowChartName, setColor);
                }
            }
            else
            {
                /// 方案二 ： 同时点亮, 但没有返回值
                Dictionary<Thread, object> threads = new Dictionary<Thread, object>();
                foreach (var flowChartName in fcCmd.Keys)
                {
                    ParameterizedThreadStart ParStart = new ParameterizedThreadStart(parseCmdInfo);
                    Thread myThread = new Thread(ParStart);
                    object name = flowChartName;
                    threads.Add(myThread, name);
                }
                foreach (Thread t in threads.Keys)
                {
                    t.Start(threads[t]);
                }
            }
            return flowChartColor;
        }

        /// <summary>
        /// 同时分析每个流程图应该的颜色 thread
        ///  "#FFB5B5B5" ,//未知;"#FF00FF00" ,//可用
        ///  "#FFFF0000" ,//故障;"#FFFFFF00" ,//跳过
        /// </summary>
        public void LightFlowChartsThread()
        {
            if (null == fcCmd)
                return ;
            Dictionary<Thread, object> threads = new Dictionary<Thread, object>();
            foreach (var flowChartName in fcCmd.Keys)
            {
                ParameterizedThreadStart ParStart = new ParameterizedThreadStart(parseCmdInfo);
                Thread myThread = new Thread(ParStart);
                object name = flowChartName;
                threads.Add(myThread, name);
            }

            foreach (Thread t in threads.Keys)
            {
                t.Start(threads[t]);
            }

            //foreach (Thread t in threads.Keys)
            //{
            //    if (t.IsAlive)
            //        t.Abort();
            //}
            return ;
        }
        /// <summary>
        /// 依次循环点亮
        /// </summary>
        public void LightFlowChartsNoThread()
        {
            Dictionary<Thread, object> threads = new Dictionary<Thread, object>();
            foreach (var flowChartName in fcCmd.Keys)
            {
                object name = flowChartName;
                parseCmdInfo(name);
            }
        }

        /// <summary>
        /// [new] 线程可用
        /// </summary>
        /// <param name="ParObject"></param>
        public void parseCmdInfo(object ParObject)
        {
            string flowChartName = ParObject.ToString();
            //Console.WriteLine("======== {0} ======start====", flowChartName);
            List<Dictionary<string, string>> leafList = fcCmd[flowChartName];
            if (null == leafList)
                return ;
            string lastColor = DecisionAnalysisgetOneCmdColor("", leafList);
            Dictionary<string, string> colr = new Dictionary<string, string>() { { flowChartName, lastColor } };
            setColor(colr);
            //Console.WriteLine("======== {0} ======end====", flowChartName);
            return ;
        }

        /// <summary>
        /// [old] 
        /// </summary>
        /// <param name="flowChartName"></param>
        /// <returns></returns>
        public string parseCmdInfo(string flowChartName)
        {
            return "";
        }
        /// <summary>
        /// 判断分析 一个命令应该设置的颜色
        /// </summary>
        /// <param name="leafDictList"></param>
        /// <returns></returns>
        private string DecisionAnalysisgetOneCmdColor(string cmdName, List<Dictionary<string, string>> leafDictList)
        {
            string color = "";
            List<bool> leafValueResult = new List<bool>();

            foreach (var leafDict in leafDictList)
            {
                bool isLastLeaf = compLeafValueWithXmlValue(cmdName, leafDict);
                leafValueResult.Add(isLastLeaf);

                /// 逻辑 : 只要有一个叶子节点符合 跳过条件，就不继续判断
                string leafProperty = leafDict["leafProperty"];
                if (String.Equals("*", leafProperty) && isLastLeaf)
                {
                    color = "#FFFFFF00";//跳过
                    break;
                }
            }
            if (String.Empty == color)
            {
                color = "#FF00FF00";//可用
                foreach (var result in leafValueResult)
                {
                    /// 只要有一个错误就错误
                    if (!result)
                    {
                        color = "#FFFF0000";//故障
                        break;
                    }
                }
            }
            return color;
        }

        /// <summary>
        /// 获取单个叶子节点的所有snmp中有效的值
        /// </summary>
        /// <param name="leafOid">需要查询的节点的oid</param>
        /// <returns>有效值的队列</returns>
        private List<string> GetsAllSnmpValuesForALeafNode(string leafOid)
        {
            string oidPrefix = "1.3.6.1.4.1.5105.100.";                        // oid 前缀
            string boardAddr = "172.27.245.92";                              // 板块地址
            LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(boardAddr);// snmp 操作句柄
            List<CDTLmtbVb> lmtVbs = new List<CDTLmtbVb>() {                 // snmp 查询的输入oid的容器
                new CDTLmtbVb() { Oid = (oidPrefix + leafOid) }};            //("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11");
            List<string> resultsList = new List<string>();                   // 所有的有效值
            Dictionary<string, string> tmpResult;                            // 单次查询结果
            List <string> logMsg = new List<string>();                       // 定位记录
            // 遍历查询所有有效的数据
            while (true)
            {
                if (lmtbSnmpEx.GetNextRequest(boardAddr, lmtVbs, out tmpResult, 0))
                {
                    lmtVbs.Clear();
                    foreach (KeyValuePair<string, string> item in tmpResult)
                    {
                        // 结果处理
                        logMsg.Add( $"oid={item.Key}, value={item.Value}");// 保存记录，便于定位 
                        resultsList.Add(item.Value);                       // 保存结果
                        // 为下次snmp取值做准备
                        CDTLmtbVb lmtVbTmp = new CDTLmtbVb { Oid = (item.Key) };
                        lmtVbs.Add(lmtVbTmp);
                    }
                }
                else
                {
                    break;
                }
            } // end while
            return resultsList;
        }

        /// <summary>
        /// (多节点)获取每一个叶子节点的所有snmp数据
        /// </summary>
        void GetAllSnmpDataForEachLeafNode()
        {
            string boardAddr = "172.27.245.92";
            LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(boardAddr);     /// 查询snmp的句柄
            List<CDTLmtbVb> lmtVbs = new List<CDTLmtbVb>();                       /// 多个叶子节点组成的队列
            /// 一起查询多个叶子节点内容
            lmtVbs.Add(new CDTLmtbVb() { Oid = ("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.11") });
            lmtVbs.Add(new CDTLmtbVb() { Oid = ("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.12") });
            lmtVbs.Add(new CDTLmtbVb() { Oid = ("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.13") });
            lmtVbs.Add(new CDTLmtbVb() { Oid = ("1.3.6.1.4.1.5105.100.2.4.1.1.2.1.1.14") });
            Dictionary<string, string> result = new Dictionary<string, string>(); /// snmp返回的结果汇总
            Dictionary<string, string> tmpResult;                                 /// 单次结果
            string logMsg;
            while (true)
            {
                if (lmtbSnmpEx.GetNextRequest(boardAddr, lmtVbs, out tmpResult, 0))
                {
                    lmtVbs.Clear();
                    foreach (KeyValuePair<string, string> item in tmpResult)
                    {
                        logMsg = $"oid={item.Key}, value={item.Value}";
                        //                       Log.Info(logMsg);

                        // 保存结果
                        result.Add(item.Key, item.Value);

                        CDTLmtbVb lmtVbTmp = new CDTLmtbVb { Oid = (item.Key) };
                        lmtVbs.Add(lmtVbTmp);
                    }
                }
                else
                {
                    break;
                }
            } // end while
        }

        /// <summary>
        /// 获取这个节点基站内存值与xml参考值得比较结果
        /// </summary>
        /// <param name="leafDict"></param>
        /// <returns>符合true,不符合false</returns>
        private bool compLeafValueWithXmlValue(string cmdName, Dictionary<string, string> leafDict)
        {
            bool isLastLeaf = false;
            string mibName = leafDict["leafName"];
            string boardAddr = "172.27.245.92";
            string errorInfo;

            var mapNameData = new Dictionary<string, IReDataByEnglishName> {
                { mibName, null }
            };

            if (!Database.GetInstance().getDataByEnglishName(mapNameData, boardAddr, out errorInfo))
            {
                return false;
            }
            // 获取所有有效的(行状态有效)数值
            List<string> effectiveVal = GetsAllSnmpValuesForALeafNode(mapNameData[mibName].oid);
            /// 逻辑 : 只要有一个索引的值符合条件，就可以
            foreach (var value in effectiveVal)
            {
                Console.WriteLine("====** mibname=({0}) value = ({1})", mibName, value);
                bool compResult = DecisionAnalysisgetFlowChartColor(leafDict, value);
                if (compResult)
                {
                    isLastLeaf = true;
                    break;
                }
            }
            return isLastLeaf;
        }

        #region [废弃]原因 : 使用 snmp 的 getNext 功能 : 可以连续只获取有效行状态的实例的节点值，不用进行无效循环.
        /// <summary>
        /// 获取单个叶子节点的所有的索引
        /// </summary>
        /// <param name="leafDict"></param>
        /// <param name="indexsList">向内add所有的索引信息</param>
        /// <returns>false没有索引</returns>
        private bool getLeafIndexList(Dictionary<string, string> leafDict, List<string> indexsList)
        {
            if (null == leafDict || null == indexsList)
                return false;
            if (0 == int.Parse(leafDict["indexNum"]))
            {
                return false;
            }
            combineRecursionIndex(1, leafDict, "", indexsList);
            return true;
        }

        /// <summary>
        /// 递归 : 组合所有可能的 index ,".x.y.z.**"
        /// </summary>
        /// <param name="indexNum"></param>
        /// <param name="leafDict"></param>
        /// <param name="prefixIndex"></param>
        /// <param name="moreIndexs"></param>
        private void combineRecursionIndex(int indexNum, Dictionary<string, string> leafDict, string prefixIndex, List<string> moreIndexs)
        {
            int indexMax = int.Parse(leafDict["indexNum"]);
            if (indexNum == 0 || indexNum > indexMax)
            {
                return;
            }

            string indexNo = "index" + indexNum.ToString();
            Dictionary<string, int> index = splitIndexValueForStartEnd(indexNo, leafDict[indexNo]);
            for (int start = index["start"]; start <= index["end"]; start++)
            {
                string postfixIndex = prefixIndex + "." + start.ToString();

                int indexNext = indexNum + 1;
                if (indexNext <= indexMax)
                {
                    combineRecursionIndex(indexNext, leafDict, postfixIndex, moreIndexs);
                }
                else
                {
                    moreIndexs.Add(postfixIndex);
                }
            }
        }

        /// <summary>
        /// 拆分出 start 和 end
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="indexValue"></param>
        /// <returns></returns>
        private Dictionary<string, int> splitIndexValueForStartEnd(string indexName, string indexValue)
        {
            int indexOf = indexValue.IndexOf('.');
            if (-1 == indexOf)
            {
                // meiyou 
                return new Dictionary<string, int>() { { "start", 0 }, { "end", 0 } }; ;
            }
            int intStart = int.Parse(indexValue.Substring(0, indexOf));
            int intEnd = int.Parse(indexValue.Substring(indexOf + 2));
            return new Dictionary<string, int>() { { "start", intStart }, { "end", intEnd } };
        }
#endregion

        #region 比较颜色相关的函数
        private delegate bool CompDataResult(int snmpReValue, int xmlValue);
        private bool CompRulesIsGreaterThan(int snmpReValue, int xmlValue)
        {
            if (snmpReValue > xmlValue)
            {
                return true;
            }
            return false;
        }
        private bool CompRulesIsLessThan(int snmpReValue, int xmlValue)
        {
            if (snmpReValue < xmlValue)
            {
                return true;
            }
            return false;
        }
        private bool CompRulesIsEquals(int snmpReValue, int xmlValue)
        {
            if (snmpReValue == xmlValue)
            {
                return true;
            }
            return false;
        }
        private bool CompRulesIsNotGreaterThan(int snmpReValue, int xmlValue)
        {
            if (snmpReValue <= xmlValue)
            {
                return true;
            }
            return false;
        }
        private bool CompRulesIsNotLessThan(int snmpReValue, int xmlValue)
        {
            if (snmpReValue >= xmlValue)
            {
                return true;
            }
            return false;
        }
        private bool CompRulesIsNotEquals(int snmpReValue, int xmlValue)
        {
            if (snmpReValue != xmlValue)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断分析颜色
        /// </summary>
        /// <param name="cmdName"></param>
        /// <param name="mibName"></param>
        /// <param name="leafInfo"></param>
        /// <returns>flase 停止</returns>
        private bool DecisionAnalysisgetFlowChartColor(Dictionary<string, string> leafInfo, string snmpReturnLeafValue)
        {
            int snmpLeafVal = -1;
            int xmlValue = -1;
            try
            {
                snmpLeafVal = int.Parse(snmpReturnLeafValue);
                xmlValue = int.Parse(leafInfo["leafValue"]);
            }
            catch
            {
                //Console.WriteLine("FlowChartSetColor : err. DecisionAnalysisgetFlowChartColor {0}", ex.Message);
                return false;//MessageBox.Show(ex.Message);//显示异常信息
            }

            // C# 委托: 函数指针
            //-GT(Greater than)表示> ;-LT(Less than)表示< ;-EQ(Equals)     表示 =;
            //-NGT(Not GT)     表示<=;-NLT(Not LT)  表示>=;-NEQ(Not Equals)表示!=;
            Dictionary<string, CompDataResult> compRules = new Dictionary<string, CompDataResult>() {
                { "GT", (CompRulesIsGreaterThan)},
                { "LT", (CompRulesIsLessThan)},
                { "EQ", (CompRulesIsEquals)},
                { "NGT", (CompRulesIsNotGreaterThan)},
                { "NLT", (CompRulesIsNotLessThan)},
                { "NEQ", (CompRulesIsNotEquals)},
            };
            string mibComp = leafInfo["leafCompRules"];
            bool compResult = compRules[mibComp](snmpLeafVal, xmlValue);

            return compResult;// new Dictionary<string, string>() {{ cmdName, colorStr[1]}};
        }
        #endregion 比较颜色相关的函数
        #endregion 点亮每个流程图颜色

    }
}
