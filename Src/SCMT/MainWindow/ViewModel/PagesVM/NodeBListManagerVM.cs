/*----------------------------------------------------------------
// Copyright (C) 2019 大唐移动通信设备有限公司 版权所有;
//
// 文件名：NodeBListManagerVM.cs
// 文件功能描述：当界面启动时,以及添加一个页签时，都会进入这个基站管理页;
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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SCMTMainWindow.Component.SCMTControl;

namespace SCMTMainWindow.ViewModel
{
    /// <summary>
    /// 基站管理列表界面VM;
    /// ??在VM中没有把实例化对象赋值给依赖属性，为什么啊??
    /// </summary>
    public class NodeBListManagerVM : MainWindowVM
    {
        /// <summary>
        /// 添加基站的依赖命令;
        /// </summary>
        public RelayCommand<ConnectNodeBPara> ConnectNodeBCommand;

        public NodeBListManagerVM()
        {
            ConnectNodeBCommand = new RelayCommand<ConnectNodeBPara>(ConnectNodeB);
        }

        /// <summary>
        /// 保存基站列表;
        /// </summary>
        public ObservableCollection<NodeB_ICON> NodeBList { get; set; }

        /// <summary>
        /// 当一个基站节点被点击，触发连接基站处理函数;
        /// </summary>
        /// <param name="para"></param>
        private void ConnectNodeB(ConnectNodeBPara para)
        {
            Console.WriteLine("Connecting to NodeB" + para.FriendlyName);
        }
    }
}
