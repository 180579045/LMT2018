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

namespace SCMTMainWindow.Component.View
{
    /// <summary>
    /// MesasgeRecv.xaml 的交互逻辑
    /// </summary>
    public partial class MesasgeRecv : UserControl
    {
        // 显示Grid控件对应的内容;
        private List<string> GridContent;
        public List<string> m_GridContent
        {
            get;
            set;
        }

        public MesasgeRecv()
        {
            InitializeComponent();
            List<Customer> custdata = GetData();
            //Bind the DataGrid to the customer data
            this.DG1.DataContext = custdata;
        }

        private List<Customer> GetData()
        {
            List<Customer> ret = new List<Customer>();
            Customer a = new Customer();
            a.FirstName = "10";
            a.LastName = "123";
            a.IsMember = false;
            ret.Add(a);

            return ret;
        }

    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri Email { get; set; }
        public bool IsMember { get; set; }
    }
}
