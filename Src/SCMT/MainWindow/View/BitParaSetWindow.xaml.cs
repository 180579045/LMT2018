using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// BitParaSetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BitParaSetWindow : Window, INotifyPropertyChanged
    {
        public bool bOK = false;
        public string strBITShow = "";
        public BitParaSetWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<BITSPara> ll = (ObservableCollection<BITSPara>)this.dataGrid.DataContext;
            var checkBox = (sender as CheckBox);
            if (checkBox.IsChecked == true)
            {
                foreach (BITSPara para in ll)
                {
                    para.IsSelected = true;
                }
            }
            else
            {
                foreach (BITSPara para in ll)
                {
                    para.IsSelected = false;
                }
            }

            RefreshCheckBox();
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            bOK = false;
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            bOK = true;
            string value = "";
            ObservableCollection<BITSPara> ll = (ObservableCollection<BITSPara>)this.dataGrid.DataContext;
            foreach (BITSPara para in ll)
            {
                if (para.IsSelected)
                    value += para.BitValue + "/";
            }
            strBITShow = value.Remove(value.Length - 1);

            this.Close();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<BITSPara> ll = (ObservableCollection<BITSPara>)this.dataGrid.DataContext;
            if (ll.All(p => p.IsSelected))
                IsSelectAll = true;
            else
            {
                if (ll.All(p => p.IsSelected == false))
                    IsSelectAll = false;
            }

        }
        private void RefreshCheckBox()
        {
            ObservableCollection<BITSPara> ll = (ObservableCollection<BITSPara>)this.dataGrid.DataContext;
            if (ll == null || ll.Count == 0 || ll.All(p => p.IsSelected == false))
            {
                IsSelectAll = false;
                return;
            }

            if (ll.All(p => p.IsSelected))
            {
                IsSelectAll = true;
                return;
            }
        }

        public void InitBITShowContent(Dictionary<int, string> content)
        {
            foreach(int key in content.Keys)
            {
                BITSPara para = new BITSPara();
                para.IsSelected = false;
                para.ID = key;
                para.BitValue = content[key];
                m_listBit.Add(para);
            }
           
            this.dataGrid.DataContext = BITSValue;
            RefreshCheckBox();
        }

        private ObservableCollection<BITSPara> m_listBit = new ObservableCollection<BITSPara>();

        private bool isSelectAll = false;
        public bool IsSelectAll
        {
            get { return isSelectAll; }
            set { isSelectAll = value; RaisePropertyChanged("IsSelectAll"); }
        }

        public ObservableCollection<BITSPara> BITSValue
        {
            get { return m_listBit; }
            set { m_listBit = value; RaisePropertyChanged("BITSValue"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string strPropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }
    }

    public class BITSPara : INotifyPropertyChanged
    {
        private bool isSelected;
        private int id;
        private string bitValue;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged("IsSelected"); }
        }
        public string BitValue
        {
            get { return bitValue; }
            set { bitValue = value; RaisePropertyChanged("BitValue"); }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string strPropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }
    }
}
