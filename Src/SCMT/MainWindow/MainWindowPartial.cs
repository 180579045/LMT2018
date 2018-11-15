using System.Windows;
using System.Windows.Controls;
using CommonUtility;
using LinkPath;
using MsgQueue;
using SCMTMainWindow.Pages;
using SCMTOperationCore.Control;
using SCMTOperationCore.Elements;
using UICore.Controls.Metro;

namespace SCMTMainWindow
{
	// MainWindows类分体

	public partial class MainWindow
	{
		#region 右键菜单响应函数区

		/// <summary>
		/// 修改基站友好名菜单响应函数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ModifyFriendlyName_Click(object sender, RoutedEventArgs e)
		{
			var me = GetTargetMenu(sender);
			if (null == me)
			{
				return;
			}

            string strIP = NodeBControl.GetInstance().GetNodeIpByFriendlyName(me.Header);
            if(null == strIP || string.Empty == strIP)
            {
                MessageBox.Show("无法通过友好名获取IP地址");
                return;
            }

			// 对话框
			var nodebDlg = new ModifyFriendlyName(me.Header);
			nodebDlg.ShowDialog();

            if(!nodebDlg.bOK)
            {
                return;
            }

            if(NodeBControl.GetInstance().ModifyElement(strIP, nodebDlg.strNewFriendlyName))
                me.Header = nodebDlg.strNewFriendlyName;
		}

		/// <summary>
		/// 修改基站IP地址菜单响应函数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ModifyEnbAddr_Click(object sender, RoutedEventArgs e)
		{
			var me = GetTargetMenu(sender);
			if (null == me)
			{
				return;
			}

			// 对话框
		}

		/// 基站节点右键菜单：删除，响应函数
		private void DeleteStationMenu_Click(object sender, RoutedEventArgs e)
		{
			var target = GetTargetMenu(sender);
			if (target == null)
			{
				return;
			}

			const string tip = "确定要删除该网元及其所有子网元？这将关闭该网元对应的所有窗口。";
			var dr = MessageBox.Show(tip, "删除网元", MessageBoxButton.YesNo, MessageBoxImage.Question | MessageBoxImage.Warning);
			if (MessageBoxResult.Yes != dr)
			{
				return;
			}

			var nodeName = target.Header;
			NodeBControl.GetInstance().DisConnectNodeb(nodeName);
			NodeBControl.GetInstance().DelElementByFriendlyName(nodeName);

			ExistedNodebList.Children.Remove(target);
		}

		/// <summary>
		/// 连接基站右键菜单响应
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConnectStationMenu_Click(object sender, RoutedEventArgs e)
		{
			var target = GetTargetMenu(sender);
			if (target == null) return;

			node = NodeBControl.GetInstance().GetNodeByFName(target.Header) as NodeB;
			var menuItem = GetMenuItemByIp(node.NeAddress.ToString(), "连接基站");
			if (null != menuItem)
			{
				//NodeBControl.GetInstance().ConnectNodeb(target.Header);
				ShowLogHelper.Show($"开始连接基站：{node.FriendlyName}-{node.NeAddress}", "SCMT");
				node.Connect();
				ObjNode.main = this;
				ChangeMenuHeader(node.NeAddress.ToString(), "连接基站", "取消连接");
			}
			else
			{
				node.DisConnect();
				ChangeMenuHeader(node.NeAddress.ToString(), "取消连接", "连接基站");
			}
		}

		/// <summary>
		/// 断开连接菜单响应函数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DisconStationMenu_Click(object sender, RoutedEventArgs e)
		{
			const string tip = "基站将断开连接，并且该基站打开的功能窗口也将关闭。是否继续操作？";

			var target = GetTargetMenu(sender);
			if (target == null)
			{
				return;
			}

			// 如果MessageBox放在上一句的前面，parent.PlacementTarget将会变成null，拿不到信息
			var dr = MessageBox.Show(tip, "断开连接", MessageBoxButton.YesNo, MessageBoxImage.Question | MessageBoxImage.Warning);
			if (MessageBoxResult.Yes == dr)
			{
				NodeBControl.GetInstance().DisConnectNodeb(target.Header);
			}
		}

		/// 发起数据同步菜单响应函数
		private void DataSync_Click(object sender, RoutedEventArgs e)
		{
			var target = GetTargetMenu(sender);
			if (null == target)
			{
				return;
			}

			var nodeName = target.Header;
			var targetIp = NodeBControl.GetInstance().GetNodeIpByFriendlyName(nodeName);
			if (null == targetIp)
			{
				return;
			}

			const string tip = "即将发起与设备的数据同步过程，耗时较长，请确认是否继续？";
			var dr = MessageBox.Show(tip, "数据同步", MessageBoxButton.YesNo, MessageBoxImage.Question | MessageBoxImage.Warning);
			if (MessageBoxResult.Yes != dr)
			{
				return;
			}

			// 发送消息，开始数据同步
			long taskId = 0;
			long reqId = 0;
			var dstPath = FilePathHelper.GetConsistencyFilePath();
			FilePathHelper.CreateFolder(dstPath);
			var fto = FileTransTaskMgr.FormatTransInfo(dstPath, "", Transfiletype5216.TRANSFILE_dataConsistency,
				TRANSDIRECTION.TRANS_UPLOAD);
			fto.IpAddr = targetIp;
			FileTransTaskMgr.SendTransFileTask(targetIp, fto, ref taskId, ref reqId);
		}


		private static MetroExpander GetTargetMenu(object sender)
		{
			var mui = sender as MenuItem;
			if (null == mui) return null;

			var parent = (ContextMenu) mui.Parent;

			var target = parent?.PlacementTarget as MetroExpander;
			return target;
		}

		#endregion
	}
}
