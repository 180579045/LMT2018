using CDLBrowser.Parser.Document.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow.Component.ViewModel
{
    public class HLMessageViewModel
    {
        private volatile ObservableCollection<EventNew> UE_MsgList; 
        public ObservableCollection<EventNew> m_MsgList
        {
            get { return UE_MsgList; }
            set { UE_MsgList = value; }
        }

        private volatile ObservableCollection<EventNew> eNB_MsgList;
        public ObservableCollection<EventNew> m_eNB_MsgList
        {
            get { return eNB_MsgList; }
            set { eNB_MsgList = value; }
        }

        private volatile ObservableCollection<EventNew> gNB_MsgList;
        public ObservableCollection<EventNew> m_gNB_MsgList
        {
            get { return gNB_MsgList; }
            set { gNB_MsgList = value; }
        }

        public HLMessageViewModel()
        {
            UE_MsgList = new ObservableCollection<EventNew>();
            eNB_MsgList = new ObservableCollection<EventNew>();
            gNB_MsgList = new ObservableCollection<EventNew>();
        }
    }
}
