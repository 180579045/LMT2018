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
using System.Windows.Shapes;
using Arthas.Controls.Metro;

namespace SCMTMainWindow
{
    /// <summary>
    /// Message_Setter.xaml 的交互逻辑
    /// </summary>
    public partial class Message_Setter : MetroWindow
    {
        public Message_Setter()
        {
            InitializeComponent();
        }

        private void Message_Setter_Confrim(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Message_Setter_Concel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
