using SCMTMainWindow.Component.ViewModel;
using SCMTMainWindow.Controls.PlanBParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// MesasgeRecv.xaml 的交互逻辑
    /// </summary>
    public partial class MesasgeRecv : UserControl
    {
        
        public MesasgeRecv()
        {
            InitializeComponent();

            // 当Message实例化的时候,直接
            HLMessageParser msg = new HLMessageParser();
            this.DG1.DataContext = msg.MessageList.m_MsgList;

        }
    }
}
