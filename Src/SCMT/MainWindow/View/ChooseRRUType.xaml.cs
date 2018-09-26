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

namespace SCMTMainWindow.View
{
    /// <summary>
    /// ChooseRRUType.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseRRUType : Window
    {
        public string strRRUtype;
        public bool bOK = false;

        public ChooseRRUType()
        {
            InitializeComponent(); this.cbRRUtype.Items.Add("单通道");
            this.cbRRUtype.Items.Add("双通道");
            this.cbRRUtype.Items.Add("四通道");
            this.cbRRUtype.Items.Add("八通道");
            this.cbRRUtype.Items.Add("十六通道");

            this.txtRRUNumber.Text = "1";
            this.txtRRUNumber.Focus();

            this.cbRRUtype.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbRRUtype.SelectedItem != null)
            {
                strRRUtype = this.cbRRUtype.SelectedItem.ToString();
                bOK = true;
                this.Close();
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            strRRUtype = null;
            bOK = false;
            this.Close();
        }
    }
}
