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
using System.Net;

namespace SCMTMainWindow
{
	/// <summary>
	/// 窗口关闭事件参数;
	/// </summary>
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

		/// <summary>
		///  单例，防止窗口被多次打开;
		/// </summary>
		private AddNodeB()
		{
			InitializeComponent();
		}

		public static AddNodeB NewInstance(MainWindow obj)
		{
			if(m_AddNB == null)
			{
				m_AddNB = new AddNodeB();
				m_AddNB.Closed += M_AddNB_Closed;            // 注册窗口关闭时得处理;
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
			var ipText = IpAddress.Text;
			var friendText = FriendName.Text;

			// 判断是否是合理的IP地址;
			if (string.IsNullOrEmpty(ipText) || string.IsNullOrWhiteSpace(ipText))
			{
				MessageBox.Show("IP地址不能为空", "添加基站", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			IPAddress addr;
			if (!IPAddress.TryParse(ipText, out addr))
			{
				MessageBox.Show($"输入的IP地址{ipText}非法", "添加基站", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// 判断友好名是否为空;
			if (string.IsNullOrEmpty(friendText) || string.IsNullOrWhiteSpace(friendText))
			{
				MessageBox.Show("友好名不能为空", "添加基站", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			try
			{
				var nodeb = (NodeB)m_NBControl.AddElement(IpAddress.Text, FriendName.Text);

				// 后续需要用Control类管理，第一版只连接一个基站;
				this.Close();
				this.OnClosed(new NodeBArgs(nodeb));
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "添加基站", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
