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
	public partial class CfgFileCheckErrorDlg : Form
	{
		public CfgFileCheckErrorDlg(string errorInfo)
		{
			InitializeComponent();

			ErrorString = errorInfo;
		}

		private void IDOK_Click(object sender, EventArgs e)
		{

		}

		private void IDCANCEL_Click(object sender, EventArgs e)
		{

		}

		private void form_load(object sender, EventArgs e)
		{
			textBox1.Text = ErrorString;
			textBox1.Update();
		}

		private string ErrorString { get; set; }
	}
}
