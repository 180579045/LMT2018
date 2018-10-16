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

using NetPlan;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// ChooserHUBType.xaml 的交互逻辑
    /// </summary>
    public partial class ChooserHUBType : Window
    {
        public int nRHUBType;

        public ChooserHUBType()
        {
            InitializeComponent();

            this.cbrHUBType.Items.Add("单通道");
            this.cbrHUBType.Items.Add("双通道");

            this.cbrHUBType.SelectedIndex = 0;
        }

        private void cbrHUBType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtrHUBNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbrHUBWorkModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string strRHUBType = this.cbrHUBType.SelectedItem.ToString();

            switch (strRHUBType)
            {
                case ("单通道"):
                    nRHUBType = 1;
                    break;
                case ("双通道"):
                    nRHUBType = 2;
                    break;
                default:
                    nRHUBType = 1;
                    break;
            }

            this.Close();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
