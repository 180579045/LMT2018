using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager.FileHandler
{
	public partial class CfgFileSelDlg : Form
	{
		public CfgFileSelDlg()
		{
			InitializeComponent();
		}

		#region 公共接口、属性

		public string FileTypeString { get; private set; }

		#endregion

		#region 窗体事件处理

		private void IDOK_Click(object sender, EventArgs e)
		{
			FileTypeString = comboBox1.SelectedItem.ToString();
			
		}

		private void IDCANCEL_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void form_load(object sender, EventArgs e)
		{
			comboBox1.Items.Clear();
			comboBox1.Items.Add("配置文件");
			comboBox1.Items.Add("一般文件");
			comboBox1.Items.Add("RNC容灾数据文件");
			comboBox1.SelectedIndex = 0;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			FileTypeString = comboBox1.SelectedText;
		}

		#endregion


	}
}
