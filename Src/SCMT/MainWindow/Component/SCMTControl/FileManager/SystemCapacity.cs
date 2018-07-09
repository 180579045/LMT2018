using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
	public partial class SystemCapacity : Form
	{
		public SystemCapacity(string devName, string restSpace, string allSpace)
		{
			InitializeComponent();

			devNameText.Text = devName;
			restCapacityText.Text = restSpace;
			allCapacityText.Text = allSpace;
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}
	}
}
