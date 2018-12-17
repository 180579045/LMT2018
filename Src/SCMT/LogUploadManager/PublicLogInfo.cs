using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUploadManager
{
    public class PublicLogInfo:INotifyPropertyChanged
    {        
        private bool isMarked=false;
        private int id;
        private string describe;

        public event PropertyChangedEventHandler PropertyChanged;

        public PublicLogInfo(int id,string describe ) {
            this.id = id;
            this.describe = describe;
        }

        public bool IsMarked
        {
            get
            {
                return isMarked;
            }

            set
            {
                isMarked = value;
                this.NotifyPropertyChange("IsMarked");
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Describe
        {
            get
            {
                return describe;
            }

            set
            {
                describe = value;
                this.NotifyPropertyChange("Describe");
            }
        }
        public void NotifyPropertyChange(string propertyName)
        {
            if (null != this.PropertyChanged)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
