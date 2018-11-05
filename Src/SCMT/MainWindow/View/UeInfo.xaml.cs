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
    /// UeInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UeInfo : UserControl
    {
        public UeInfo()
        {
            InitializeComponent();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditColumns edclw = new EditColumns(this.dataGrid.Columns);
            edclw.ShowDialog();
            if (!edclw.IsOK)
            {
                return;
            }
            for (int k = 0; k < this.dataGrid.Columns.Count; k++)
            {
                this.dataGrid.Columns[k].Visibility = Visibility.Visible;
            }
            for (int i = 0; i < edclw.HiddenHeader1.Count; i++)
            {
                for (int j = 0; j < this.dataGrid.Columns.Count; j++)
                {
                    if (edclw.HiddenHeader1[i].DisplayIndex == this.dataGrid.Columns[j].DisplayIndex)
                    {
                        this.dataGrid.Columns[j].Visibility = Visibility.Hidden;
                        continue;
                    }
                }
            }
        }
    }
}
