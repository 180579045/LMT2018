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
    /// ChooseAntennaType.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseAntennaType : Window
    {
        public int nAntennaType;

        public ChooseAntennaType()
        {
            InitializeComponent();

            this.cbAntennaType.Items.Add("单通道");
            this.cbAntennaType.Items.Add("双通道");
            this.cbAntennaType.Items.Add("四通道");
            this.cbAntennaType.Items.Add("八通道");

            this.cbAntennaType.SelectedIndex = 0;
        }

        private void cbAntennaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbAntennaWorkModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtAntennaNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string strAntennaType = this.cbAntennaType.SelectedItem.ToString();

            switch (strAntennaType)
            {
                case ("单通道"):
                    nAntennaType = 1;
                    break;
                case ("双通道"):
                    nAntennaType = 2;
                    break;
                case ("四通道"):
                    nAntennaType = 4;
                    break;
                case ("八通道"):
                    nAntennaType = 8;
                    break;
                default:
                    nAntennaType = 1;
                    break;
            }

            this.Close();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
