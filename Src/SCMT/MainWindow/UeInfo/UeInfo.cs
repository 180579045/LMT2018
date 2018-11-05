using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow.UeInfo
{
    public class UeInformation
    {
        private int num;
        private string info1;

        private string info2;

        private string info3;
        private ObservableCollection<ChildrenUeInfo> info4;
        public int Num
        {
            get { return this.num; }
            set { this.num = value; }
        }

        public string Info1
        {
            get
            {
                return info1;
            }

            set
            {
                info1 = value;
            }
        }

        public string Info2
        {
            get
            {
                return info2;
            }

            set
            {
                info2 = value;
            }
        }

        public string Info3
        {
            get
            {
                return info3;
            }

            set
            {
                info3 = value;
            }
        }

        public ObservableCollection<ChildrenUeInfo> Info4
        {
            get
            {
                return info4;
            }

            set
            {
                info4 = value;
            }
        }
    }
}
