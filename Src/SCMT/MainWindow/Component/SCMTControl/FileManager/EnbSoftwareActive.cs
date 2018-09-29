using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUtility;
using MAP_STRING = System.Collections.Generic.Dictionary<string, string>;
using LmtbSnmp;
using LinkPath;

namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
	public partial class EnbSoftwareActive : Form
	{
		public EnbSoftwareActive(string boardIp)
		{
			_boardIp = boardIp;
			_mapSoftwareVer = new Dictionary<int, List<SoftwareVersion>>();

			InitializeComponent();
			Init5216SoftWareInfo();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			listView1.Items.Clear();
			var index = comboBox1.SelectedIndex;
			if (_mapSoftwareVer.ContainsKey(index))
			{
				var lvIndex = 1;
				var svList = _mapSoftwareVer[index];
				foreach (var sv in svList)
				{
					if (sv.rowStatus != "")
					{
						var lvItem = new ListViewItem(lvIndex.ToString());
						lvItem.SubItems.Add(sv.rowStatus);
						lvItem.SubItems.Add(sv.softwareVer);
						lvItem.SubItems.Add(sv.relayVer);
						lvItem.SubItems.Add(sv.detailVer);
						listView1.Items.Add(lvItem);
						lvIndex++;
					}
				}
			}
		}

		private void Init5216SoftWareInfo()
		{
			comboBox1.Items.Add("主设备软件");
			comboBox1.Items.Add("主设备冷补丁");
			comboBox1.Items.Add("主设备热补丁");
			comboBox1.SelectedIndex = 0;

			listView1.Columns.Add("编号", -2, HorizontalAlignment.Center);
			listView1.Columns.Add("状态", -2, HorizontalAlignment.Center);
			listView1.Columns.Add("软件包版本", -2, HorizontalAlignment.Center);
			listView1.Columns.Add("补丁包依赖版本", -2, HorizontalAlignment.Center);
			listView1.Columns.Add("补丁包详细版本", -2, HorizontalAlignment.Center);
			listView1.View = System.Windows.Forms.View.Details;

			for (int index1 = 1; index1 < 4; index1++)
			{
				for (int index2 = 1; index2 < 5; index2++)
				{
					// 先清空所有的值
					var mibValues = new MAP_STRING
					{
						["swPackRowStatus"] = "",
						["swPackVersion"] = "",
						["swPackRelyVesion"] = ""
					};

					var index = $".{index1}.{index2}";

					var detailVersion = CommLinkPath.GetMibValueFromCmdExeResult(index, "GetSWPackDetailVer", "swPackDetailVersion", _boardIp);
					if (!string.IsNullOrEmpty(detailVersion))
					{

					}

					if (CommLinkPath.GetMibValueFromCmdExeResult(index, "GetSWPack", ref mibValues, _boardIp))
					{
						var sv = new SoftwareVersion
						{
							rowStatus = SnmpToDatabase.GetRowStatusText(mibValues["swPackRowStatus"]),
							softwareVer = mibValues["swPackVersion"].Trim('\0'),
							relayVer = mibValues["swPackRelyVesion"],
							detailVer = detailVersion
						};

						if (_mapSoftwareVer.ContainsKey(index1 - 1))
						{
							var svList = _mapSoftwareVer[index1 - 1];
							if (!svList.Contains(sv))
							{
								svList.Add(sv);
							}
						}
						else
						{
							var svList = new List<SoftwareVersion> { sv };
							_mapSoftwareVer[index1 - 1] = svList;
						}
					}
				}
			}

			comboBox1_SelectedIndexChanged(this, null);
		}

		private string _boardIp { get; }

		private Dictionary<int, List<SoftwareVersion>> _mapSoftwareVer;       // key: 一维索引值-1，对应到combobox上item的index
	}
}
