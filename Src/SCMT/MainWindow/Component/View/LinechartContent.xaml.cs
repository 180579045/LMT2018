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

namespace SCMTMainWindow.Component.View
{
    /// <summary>
    /// LinechartContent.xaml 的交互逻辑
    /// </summary>
    public partial class LinechartContent : UserControl
    {
        public LinechartContent()
        {
            InitializeComponent();
            this.address.Address = System.Environment.CurrentDirectory + @"\LineChart_JS\LinChart.html";
            CefSharp.CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            this.address.RegisterJsObject("JsObj", new CallbackObjectForJs());
            this.address.BeginInit();

        }
    }
}
