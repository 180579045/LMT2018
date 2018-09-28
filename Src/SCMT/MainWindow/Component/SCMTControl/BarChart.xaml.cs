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
using CefSharp;
using CefSharp.Wpf;
using Xceed.Wpf.AvalonDock.Layout;
using System.Threading;
using LmtbSnmp;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// BarChart.xaml 的交互逻辑
    /// </summary>
    public partial class BarChart : UserControl
    {
        //为了实现鼠标拖拽，创建全局变量，通过全局变量进行添加;
        public CallbackObjectForJs m_CbForJs = new CallbackObjectForJs();
        private bool GettingValue = false;

        public BarChart()
        {
            InitializeComponent();

            this.address.Address = System.Environment.CurrentDirectory + @"\LineChart_JS\LineChart.html";
            CefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            //string[] data = { "16:49:01", "16:49:02", "16:49:03", "16:49:04", "16:49:05", "16:49:06" };
            //double[] da_Num = CallbackObjectForJs.randomArr(36);       // 构造option，以显示在js中，作为初始默认显示;
            m_CbForJs.canvas_height = "300";
            m_CbForJs.canvas_width = "800";

            this.address.RegisterJsObject("JsObj", m_CbForJs);         // 向浏览器注册JavaScript对象,对象名称是JsObj，在前端可以访问;
            this.address.BeginInit();                                  // 刷新页面;

            // Add By Mayi;
            // 创建  CefSharp  的  drop  事件，用来接收鼠标拖拽的对象;
            this.address.AllowDrop = true;
            this.address.Drop += Address_Drop;
        }

        /// <summary>
        /// 当窗口发生变化时，改变前端JS和HTML的宽高;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WindowProperty_Changed(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            LayoutAnchorable linechart_window = sender as LayoutAnchorable;
            Console.WriteLine("WindowProperty Changed!" + linechart_window.FloatingHeight + "," + linechart_window.FloatingWidth);
            m_CbForJs.canvas_height = (linechart_window.FloatingHeight - 100).ToString();
            m_CbForJs.canvas_width = linechart_window.FloatingWidth.ToString();
            this.address.Reload();
        }

        /// <summary>
        /// 当窗口关闭后，停止获取数据;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Sub_Closed(object sender, EventArgs e)
        {
            GettingValue = false;
        }

        /// <summary>
        /// Add By Mayi;
        /// 实现接收拖拽对象的函数;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Address_Drop(object sender, DragEventArgs e)
        {
            try
            {
                DataGridCell_MIB_MouseEventArgs cell = e.Data.GetData(typeof(DataGridCell_MIB_MouseEventArgs)) as DataGridCell_MIB_MouseEventArgs;

                foreach (var iter in cell.SelectedCell.Properties)
                {
                    // 找与其对应的节点;
                    if (cell.HeaderName == (iter.Value as DataGrid_Cell_MIB).MibName_CN)
                    {
                        Console.WriteLine("Selected Cell Keys is " + iter.Key + "and value is " + (iter.Value as DataGrid_Cell_MIB).MibName_CN +
                                          " This Node oid is " + (iter.Value as DataGrid_Cell_MIB).oid);

                        List<string> inputoid = new List<string>();
                        inputoid.Add((iter.Value as DataGrid_Cell_MIB).oid);

                        GettingValue = true;
                        string[] data = { };
                        // 启动一个线程不断读取该节点的数值并回填到EChart的option中;
                        Task ReadValue_FromSNMP = new Task(() =>
                        {
                            string[] da_Num = { };       // 构造option，以显示在js中，作为初始默认显示;
                            Dictionary<string, string> Ret = new Dictionary<string, string>();
                            List<string> da_Num_content = new List<string>();
                            List<string> data_content = new List<string>();

                            string[] db_temp = new string[m_CbForJs.listForXAxis.Length];
                            da_Num_content = da_Num_content.Concat(db_temp).ToList();

                            // 持续获取数据;
                            while (GettingValue)
                            {
                                // ！！！后续需要扩展功能，保存日志文件;
                                SnmpMessageV2c snmpmsg1 = new SnmpMessageV2c();
                                Ret = snmpmsg1.GetRequest(inputoid, "public", "172.27.245.92");
                                double temp = 0;

                                foreach (var iter2 in Ret)
                                {
                                    Console.WriteLine("Get MibValue is " + iter2.Value);
                                    Double.TryParse(iter2.Value, out temp);
                                    da_Num_content.Add(temp.ToString());
                                    da_Num = da_Num_content.ToArray();                     // 将数据集再转换回数组;

                                    data_content.Add(DateTime.Now.ToString("T"));
                                    data = data_content.ToArray();
                                }
                                
                                Thread.Sleep(2000);

                                series mySeries = new series((iter.Value as DataGrid_Cell_MIB).MibName_CN, "bar", false, "", "", da_Num);

                                if (!m_CbForJs.listForLegend.Contains((iter.Value as DataGrid_Cell_MIB).MibName_CN))
                                {
                                    m_CbForJs.listForLegend.Add((iter.Value as DataGrid_Cell_MIB).MibName_CN);
                                    m_CbForJs.listForSeries.Add(mySeries);

                                    if (m_CbForJs.listForSeries.Count == 0)
                                    {
                                        m_CbForJs.listForXAxis = data;
                                    }
                                    else
                                    {
                                        m_CbForJs.listForXAxis = m_CbForJs.listForXAxis.Length > data.Length ? m_CbForJs.listForXAxis : data;
                                    }
                                }
                                else
                                {
                                    int index = m_CbForJs.listForLegend.IndexOf((iter.Value as DataGrid_Cell_MIB).MibName_CN);
                                    m_CbForJs.listForSeries[index] = mySeries; if (m_CbForJs.listForSeries.Count == 0)
                                    {
                                        m_CbForJs.listForXAxis = data;
                                    }
                                    else
                                    {
                                        m_CbForJs.listForXAxis = m_CbForJs.listForXAxis.Length > data.Length ? m_CbForJs.listForXAxis : data;
                                    }
                                }

                                legend myLegend = new legend(m_CbForJs.listForLegend);

                                xAxis xaxis = new xAxis(m_CbForJs.listForXAxis);
                                Option myOption = new Option(myLegend, m_CbForJs.listForSeries, xaxis);

                                m_CbForJs.Option = Option.ObjectToJson(myOption);
                            }



                        });

                        ReadValue_FromSNMP.Start();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("LineChart Receive Mouse Drop Exception " + ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.address.ShowDevTools();
        }
    }
}
