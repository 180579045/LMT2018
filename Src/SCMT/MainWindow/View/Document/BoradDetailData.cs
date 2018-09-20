using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow.View.Document
{
    public class BoradDetailData
    {
        private double frame;
        private double panel;
        private ObservableCollection<int> slot =new ObservableCollection<int>();        
        private int selectedSoltNum;
        public string selectedBroadType;
        private BroadType bt = BroadType.SCTF;
        private WorkMode wm = WorkMode.LTE_TDD;
        private FrameStruMode fsm = FrameStruMode.CPRI;
        public string SelectedBroadType {
            get {
                return this.selectedBroadType;
            }
            set {
                this.selectedBroadType = value;
            }
        }
        public ObservableCollection<int> Slot {
            get { return this.slot; }
            set { this.slot = value; }
        }
       
        public int SelectedSlotNum {
            get {
                return this.selectedSoltNum;
            }
            set {
                this.selectedSoltNum = value;
            }
        }
        
        public BroadType Bt {
            get {
                return this.bt;

            }
            set {
                this.bt = value;
            }

        }
        //板卡工作模式枚举值

        //板卡IR帧结构类型

        public WorkMode Wm {
            get {
                return this.wm;
            }
            set {
                this.wm = value;
            }
        }
        public double Frame {
            get {
                return this.frame;
            }
            set {
                this.frame = value;
            }
        }
        public double Panel {
            get {
                return this.panel;
            }
            set {
                this.panel = value;
            }
        }

        public FrameStruMode Fsm {
            get {
                return this.fsm;
            }
            set {
                this.fsm = value;
            }
        }
        public void setDefaultDate(int soltNum) {
            this.frame = 0;
            this.panel = 0;
            this.selectedSoltNum = soltNum;
            this.bt = BroadType.SCTF;
            this.wm = WorkMode.LTE_TDD;
            this.fsm = FrameStruMode.CPRI;
        }
    }
}
