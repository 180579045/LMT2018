using SCMTMainWindow.Property;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace SCMTMainWindow.ViewModel
{
    public class NetPlanModel : DependencyObject, INotifyPropertyChanged
    {
        private ObservableCollection<Propertyies> alP = new ObservableCollection<Propertyies>();
        public NetPlanModel()
        {
        }
        public ObservableCollection<Propertyies> alPropertyies
        {
            get { return this.alP; }
            set
            {
                this.alP = value;
                this.NotifyPropertyChange("alPropertyies");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            alP.Clear();
        }
        private void NotifyPropertyChange(string propertyName)
        {
            if (null != this.PropertyChanged)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
