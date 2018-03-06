/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：AddNodeB.xaml.cs
// 文件功能描述：添加基站窗体;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2018-03-06
//----------------------------------------------------------------*/

using System;
using System.Windows;
using UICore.Controls.Metro;
using SCMTOperationCore.Control;
using SCMTOperationCore.Elements;
using System.ComponentModel;

namespace SCMTMainWindow
{

    class NodeBArgs : EventArgs
    {
        public NodeB m_NodeB { get; set; }

        public NodeBArgs(NodeB nb)
        {
            m_NodeB = nb;
        }
    }
    /// <summary>
    /// AddNodeB.xaml 的交互逻辑
    /// </summary>
    public partial class AddNodeB : MetroWindow
    {
        private static AddNodeB m_AddNB = null;
        private static NodeBControl m_NBControl { get; set; }
        private static NodeB m_nb { get; set; }

        private AddNodeB()
        {
            InitializeComponent();
        }

        public static AddNodeB NewInstance(MainWindow obj)
        {
            if(m_AddNB == null)
            {
                m_AddNB = new AddNodeB();
                m_AddNB.Closed += M_AddNB_Closed;
                m_AddNB.ShowInTaskbar = false;
                m_AddNB.IsSubWindowShow = true;
                m_NBControl = obj.NBControler;
                
            }
            return m_AddNB;
        }

        private static void M_AddNB_Closed(object sender, EventArgs e)
        {
            m_AddNB = null;
        }

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Click_Confirm(object sender, RoutedEventArgs e)
        {
            // 判断是否是合理的IP地址;

            // 判断友好名是否为空;
            NodeB nodeb = new NodeB(this.IpAddress.Text.ToString(), this.FriendName.Text.ToString());
            // 后续需要用Control类管理，第一版只连接一个基站;
            this.Close();
            this.OnClosed(new NodeBArgs(nodeb));
        }
    }
}
