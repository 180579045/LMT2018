using SCMTMainWindow.UeInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCMTMainWindow.ViewModel
{
    public class UeInfoModel:DependencyObject, INotifyPropertyChanged
    {
        private ObservableCollection<UeInformation> ueInfos = new ObservableCollection<UeInformation>();

        public  UeInfoModel() {
            this.Inital();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<UeInformation> UeInfos
        {
            get
            {
                return ueInfos;
            }

            set
            {
                ueInfos = value;
                this.NotifyPropertyChange("UeInfos");
            }
        }
        private void Inital() {
            ObservableCollection<ChildrenUeInfo> al = new ObservableCollection<ChildrenUeInfo>();
            ChildrenUeInfo c1 = new ChildrenUeInfo();
            ChildrenUeInfo c2 = new ChildrenUeInfo();
            c2.UeInfoChildren = "3333333333";
            c1.UeInfoChildren = "22222222222";
            c1.Children.Add(c2);
            c1.Children.Add(c2);
            c1.Children.Add(c2);
            c1.Children.Add(c2);
            c1.Children.Add(c2);
            al.Add(c1);
            //al.Add(c1);
            //al.Add(c1);
            //al.Add(c1);
            //al.Add(c1);
            for (int i = 0; i < 10; i++)
            {
                UeInformation ue = new UeInformation();
                ue.Num = i;
                ue.Info1 = "rrrrrr";
                ue.Info2 = "tttttt";
                ue.Info3 = "yyyy";
                ue.Info4 = al;
                ueInfos.Add(ue);
            }

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
