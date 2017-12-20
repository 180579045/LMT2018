﻿/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：MainWindow.xaml.cs
// 文件功能描述：主界面控制类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Snmp_dll;
using Arthas.Controls.Metro;
using Arthas.Utility.Media;
using System.Reflection;

using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SCMTMainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑;
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitView();                                                       // 初始化界面;
        }

        /// <summary>
        /// 初始化用户界面;
        /// Demo阶段，先假设只连接一个基站;
        /// </summary>
        public void InitView()
        {
            NodeB node = new NodeB("172.27.245.92");                          // 初始化一个基站节点(Demo程序,暂时只连接一个基站);
            ObjNodeControl Ctrl = new ObjNodeControl(node);                   // 初始化一个对象树管理;
            this.RefreshObj(Ctrl.m_RootNode);

            TrapMessage.SetNodify(this.Update_NBInfoShow);                    // 注册Trap监听;

            ObservableCollection<AlarmGrid> custdata = AlarmGrid.GetData();   // 初始化基本信息中的告警信息;
            DG1.DataContext = custdata;                                       // 将告警信息加入控件;
            
        }

        /// <summary>
        /// 树形结构被选择后触发的处理;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjTree_Tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ObjNode SelectedItem = this.ObjTree_Tv.SelectedItem as ObjNode;
        }

        /// <summary>
        /// 在控制台窗口打印Trap信息;
        /// </summary>
        /// <param name="TrapContent"></param>
        private void Update_NBInfoShow(List<string> TrapContent)
        {
            // 如果当前运行的线程不是UI线程才写入到控件中;
            if (System.Threading.Thread.CurrentThread != NBInfoShow.Dispatcher.Thread)
            {
                NBInfoShow.Dispatcher.Invoke(
                   new Action(
                        delegate
                        {
                            foreach (string content in TrapContent)
                            {
                                NBInfoShow.Text += DateTime.Now + " " + content + "\r\n";
                                NBInfoShow.ScrollToEnd();
                            }
                        }
                   )
                );
            }
        }

        private void RefreshObj(IList<ObjNode> ItemsSource)
        {
            StackPanel FavLeafList = new StackPanel();
            this.Obj_Root.SubExpender = this.FavLeaf_Lists;
            FavLeafList = FavLeaf_Lists;
            foreach (ObjNode items in ItemsSource)
            {
                items.TraverseChildren(this.Obj_Root, this.FavLeaf_Lists, 0);
            }
        }

        /// <summary>
        /// Demo阶段得点击事件，先不考虑解耦合;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommMME_Click(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Hidden;
            Content_Comm.Visibility = Visibility.Visible;
        }

        private void MetroExpander_Click_1(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Visible;
            Content_Comm.Visibility = Visibility.Hidden;
        }

        private void MetroExpander_Click(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Hidden;
            Content_Comm.Visibility = Visibility.Visible;
        }

        private void MetroExpander_Click_2(object sender, EventArgs e)
        {
            Content_Base.Visibility = Visibility.Hidden;
            Content_Comm.Visibility = Visibility.Visible;
        }
    }

   

}
