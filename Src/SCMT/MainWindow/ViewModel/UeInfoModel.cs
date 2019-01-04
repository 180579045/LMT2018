using SCMTMainWindow.UeInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UEData;
using System.Data;
namespace SCMTMainWindow.ViewModel
{
    public class UeInfoModel : DependencyObject, INotifyPropertyChanged
    {
        private ObservableCollection<UeInformation> ueInfos = GlobalData.strUeInfo;
        private ObservableCollection<UeMeasCfInfo> ueMeasInfos = GlobalData.strUeMeasInfo;
        private ObservableCollection<UeipCellInfo> ueIpCellInfos = GlobalData.strUeIpCellInfo;
        private ObservableCollection<UeipInfo> ueIpInfos = GlobalData.strUeIpInfo;
        public UeInfoModel()
        {
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
        public ObservableCollection<UeMeasCfInfo> UeMeasInfos
        {
            get
            {
                return ueMeasInfos;
            }

            set
            {
                ueMeasInfos = value;
                this.NotifyPropertyChange("UeMeasInfos");
            }
        }
        public ObservableCollection<UeipCellInfo> UeIpCellInfos
        {
            get
            {
                return ueIpCellInfos;
            }

            set
            {
                ueIpCellInfos = value;
                this.NotifyPropertyChange("UeIpCellInfos");
            }
        }
        public ObservableCollection<UeipInfo> UeIpInfos
        {
            get
            {
                return ueIpInfos;
            }

            set
            {
                ueIpInfos = value;
                this.NotifyPropertyChange("UeIpInfos");
            }
        }
        private void Inital()
        {


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
