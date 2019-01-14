/*----------------------------------------------------------------
// Copyright (C) 2019 大唐移动通信设备有限公司 版权所有;
//
// 文件名：MainWindowVM.cs
// 文件功能描述：主界面VM类,主要负责管理页签;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2019-1-6
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MsgQueue;
using SCMTMainWindow.Component.SCMTControl;
using SCMTOperationCore.Control;
using SCMTOperationCore.Elements;

namespace SCMTMainWindow.ViewModel
{
    public class NodeBListManagerTabVM : TabBase
    {
        /// <summary>
        /// 保存基站列表;
        /// </summary>
        public ObservableCollection<NodeB_ICON> NodeBList { get; set; }

        /// <summary>
        /// 点击连站的时候，需要通知主界面进行处理;
        /// </summary>
        public event EventHandler<AddNodeBEvtArgs> onConnectNodeBEvt = delegate { };

        /// <summary>
        /// 添加基站的依赖命令;
        /// </summary>
        public RelayCommand<ConnectNodeBPara> ConnectNodeBCommand { get; set; }

        public NodeBListManagerTabVM()
        {
            ConnectNodeBCommand = new RelayCommand<ConnectNodeBPara>(ConnectNodeB);    // 将依赖命令Binding;

            // 初始化的时候，读取用户已经保存的基站列表，存入NodeBList;
            NodeB_ICON icon = new NodeB_ICON();
            icon.ConnectNodeBCommand = ConnectNodeBCommand;
        }

        /// <summary>
        /// 当一个基站节点被点击，触发连接基站处理函数;
        /// </summary>
        /// <param name="para"></param>
        private void ConnectNodeB(ConnectNodeBPara para)
        {
            Console.WriteLine("Connecting to NodeB" + para.FriendlyName);

            // 第一步：新建一个基站主页面类，并开始连接基站;
            NodeBMainTabVM tab = new NodeBMainTabVM();
            tab.TabName = para.FriendlyName;
            
            var tnode = NodeBControl.GetInstance().GetNodeByFName(para.FriendlyName) as NodeB;

            ShowLogHelper.Show($"开始连接基站：{tnode.FriendlyName}-{tnode.NeAddress}", "SCMT");
            tnode.ConnectAsync();
            
            // 第二步：通知主界面新增一个基站页签;
            AddNodeBEvtArgs evtArgs = new AddNodeBEvtArgs();
            evtArgs.tab = tab;
            evtArgs.node = tnode;
            onConnectNodeBEvt(this, evtArgs);
        }

    }

    public class AddNodeBEvtArgs : EventArgs
    {
        public NodeBMainTabVM tab;
        public NodeB node;
    }
}
