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

namespace SCMTMainWindow.View
{
	/// <summary>
	/// ConfigFileOperate.xaml 的交互逻辑
	/// </summary>
	public partial class ConfigFileOperate : Window
	{
		public ConfigFileOperate()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 选择数据库文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDataBase_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "数据库文件 | *.dtz";
			if(dlg.ShowDialog() == true)
			{
				this.tbDataBase.Text = dlg.FileName;
			}
		}

		/// <summary>
		/// 选择天线阵权重文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAntWeight_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "excel | *.xls";
			if (dlg.ShowDialog() == true)
			{
				this.tbAntWeight.Text = dlg.FileName;
			}
		}

		/// <summary>
		/// 告警信息文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnWarning_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "excel | *.xls";
			if (dlg.ShowDialog() == true)
			{
				this.tbWarning.Text = dlg.FileName;
			}
		}

		/// <summary>
		/// RRU信息文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRRUInfo_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "excel | *.xls";
			if (dlg.ShowDialog() == true)
			{
				this.tbRRUInfo.Text = dlg.FileName;
			}
		}

		/// <summary>
		/// recList
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRecList_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "excel | *.xls";
			if (dlg.ShowDialog() == true)
			{
				this.tbRecList.Text = dlg.FileName;
			}
		}

		/// <summary>
		/// 自定义文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelfDefine_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "excel | *.xls";
			if (dlg.ShowDialog() == true)
			{
				this.tbSelfDefine.Text = dlg.FileName;
			}
		}

		/// <summary>
		/// 确定按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{

		}

		/// <summary>
		/// 取消按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 生成路径选择
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPathToSave_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
			dlg.Description = "请选择配置文件的保存路径";
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if(string.IsNullOrEmpty(dlg.SelectedPath))
				{
					MessageBox.Show("路径不能为空");
					return;
				}
				this.tbPathToSave.Text = dlg.SelectedPath;
			}
		}
	}
}
