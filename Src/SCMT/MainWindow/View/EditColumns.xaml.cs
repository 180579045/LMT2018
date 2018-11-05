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
using System.Windows.Shapes;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// EditColumns.xaml 的交互逻辑
    /// </summary>
    public partial class EditColumns : Window
    {
        public EditColumns()
        {
            InitializeComponent();
        }
        private ObservableCollection<DataGridColumn> displayHeader = new ObservableCollection<DataGridColumn>();
        private ObservableCollection<DataGridColumn> HiddenHeader = new ObservableCollection<DataGridColumn>();
        public bool IsOK = false;
        public EditColumns(ObservableCollection<DataGridColumn> columns)
        {
            foreach (var colun in columns)
            {
                if (colun.Visibility == Visibility.Visible)
                {
                    DisplayHeader.Add(colun);
                }
                else
                {
                    HiddenHeader1.Add(colun);
                }
            }

            InitializeComponent();
            this.hiddenls.ItemsSource = HiddenHeader;
            this.displayls.ItemsSource = DisplayHeader;
        }

        public ObservableCollection<DataGridColumn> DisplayHeader
        {
            get
            {
                return displayHeader;
            }

            set
            {
                displayHeader = value;
            }
        }

        public ObservableCollection<DataGridColumn> HiddenHeader1
        {
            get
            {
                return HiddenHeader;
            }

            set
            {
                HiddenHeader = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = this.hiddenls.SelectedItem as DataGridColumn;
            HiddenHeader.Remove(a);
            displayHeader.Add(a);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selecteItem = this.displayls.SelectedItem as DataGridColumn;
            displayHeader.Remove(selecteItem);
            HiddenHeader.Add(selecteItem);
        }

        private void button_Ok_Click(object sender, RoutedEventArgs e)
        {
            IsOK = true;
            Close();
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsOK = false;
            Close();
        }
    }
}
