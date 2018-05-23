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

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// LinechartContent.xaml 的交互逻辑
    /// </summary>
    public partial class LinechartContent : UserControl
    {
        //Add By Mayi;
        //为了实现鼠标拖拽，创建全局变量，通过全局变量进行添加;
        public CallbackObjectForJs m_CbForJs = new CallbackObjectForJs();

        public LinechartContent()
        {
            InitializeComponent();
            this.address.Address = System.Environment.CurrentDirectory + @"\LineChart_JS\LineChart.html";
            CefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            
            string[] data = { "16:49:01", "16:49:02", "16:49:03", "16:49:04", "16:49:05", "16:49:06" };
            double[] da_Num = CallbackObjectForJs.randomArr(36);       // 构造option，以显示在js中，作为初始默认显示;

            this.address.RegisterJsObject("JsObj", m_CbForJs);         // 向浏览器注册JavaScript对象,对象名称是JsObj，在前端可以访问;
            this.address.BeginInit();                                  // 刷新页面;
            
            // Add By Mayi;
            // 创建  CefSharp  的  drop  事件，用来接收鼠标拖拽的对象;
            this.address.AllowDrop = true;
            this.address.Drop += Address_Drop;

            series mySeries = new series("testDefault", "line", false, "circle", "", da_Num);       // 向与前端交互的JsObj添加series,包含所有的折线数据;
            legend myLegend = new legend(m_CbForJs.listForLegend);                                  // 向与前端交互的JsObj添加legend;
            xAxis xaxis = new xAxis(data);                                                          // 向与前端交互的JsObj添加xAxis;

            m_CbForJs.listForLegend.Add("testDefault");
            m_CbForJs.listForSeries.Add(mySeries);

            Option myOption = new Option(myLegend, m_CbForJs.listForSeries, xaxis);
            m_CbForJs.Option = Option.ObjectToJson(myOption);                                       // 将数据转换为Json格式，让前端读取;
            
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
                //获取到  从鼠标拖拽过来的对象  转换为  Treeviewitem
                TreeViewItem myTreeViewItem = e.Data.GetData(typeof(TreeViewItem)) as TreeViewItem;

                if (m_CbForJs.listForLegend.Contains(myTreeViewItem.Header.ToString()))
                    return;

                //构造  option，以显示在js中
                double[] da_Num = CallbackObjectForJs.randomArr(72);

                series mySeries = new series(myTreeViewItem.Header.ToString(), "line", false, "circle", "", da_Num);
                m_CbForJs.listForLegend.Add(myTreeViewItem.Header.ToString());
                m_CbForJs.listForSeries.Add(mySeries);

                legend myLegend = new legend(m_CbForJs.listForLegend);

                string[] data = { "16:49:01", "16:49:02", "16:49:03", "16:49:04", "16:49:05", "16:49:06" };
                xAxis xaxis = new xAxis(data);

                Option myOption = new Option(myLegend, m_CbForJs.listForSeries, xaxis);
                
                m_CbForJs.Option = Option.ObjectToJson(myOption);
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.address.ShowDevTools();
        }
    }
}
