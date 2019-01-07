/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：MainWindow.xaml.cs
// 文件功能描述：主界面控制类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/

using ChromeTabs;
using SCMTMainWindow.ViewModel;
using System.Windows;
using UICore.Controls.Metro;

namespace SCMTMainWindow
{
	/// <inheritdoc />
	/// <summary>
	/// MainWindow.xaml 的交互逻辑;
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        /// <summary>
        /// This event triggers when a tab is dragged outside the bonds of the tab control panel.
        /// We can use it to create a docking tab control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void TabControl_TabDraggedOutsideBonds(object sender, TabDragEventArgs e)
        //{
        //    TabBase draggedTab = e.Tab as TabBase;
        //    if (TryDragTabToWindow(e.CursorPosition, draggedTab))
        //    {
        //        //Set Handled to true to tell the tab control that we have dragged the tab to a window, and the tab should be closed.
        //        e.Handled = true;
        //    }
        //}

        //protected override bool TryDockWindow(Point position, TabBase dockedWindowVM)
        //{
        //    //Hit test against the tab control
        //    if (MyChromeTabControl.InputHitTest(position) is FrameworkElement element)
        //    {
        //        ////test if the mouse is over the tab panel or a tab item.
        //        if (CanInsertTabItem(element))
        //        {
        //            //TabBase dockedWindowVM = (TabBase)win.DataContext;
        //            ViewModelExampleBase vm = (ViewModelExampleBase)DataContext;
        //            vm.ItemCollection.Add(dockedWindowVM);
        //            vm.SelectedTab = dockedWindowVM;
        //            //We run this method on the tab control for it to grab the tab and position it at the mouse, ready to move again.
        //            MyChromeTabControl.GrabTab(dockedWindowVM);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private void BnOpenPinnedTabExample_Click(object sender, RoutedEventArgs e)
        //{
        //    new PinnedTabExampleWindow().Show();
        //}

        //private void BnOpenCustomStyleExample_Click(object sender, RoutedEventArgs e)
        //{
        //    new CustomStyleExampleWindow().Show();
        //}


    }
}