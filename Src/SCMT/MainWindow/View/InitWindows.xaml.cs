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
using System.Windows.Shapes;
using MsgQueue;

namespace SCMTMainWindow.View
{
	/// <summary>
	/// InitWindows.xaml 的交互逻辑
	/// </summary>
	public partial class InitWindows : Window
	{
		public InitWindows()
		{
			InitializeComponent();

			SubscribeHelper.AddSubscribe(TopicHelper.NetPlanInit, OnShowInitMsg);
		}

		#region 消息响应处理函数

		// 消息体msg.Data是一个字符串，直接显示即可
		private void OnShowInitMsg(SubscribeMsg msg)
		{
			var doing = Encoding.UTF8.GetString(msg.Data);

            this.txtInfo.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.txtInfo.Text = doing;

            DispatcherHelper.DoEvents();

        }));
        }

		#endregion
	}
}
