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
using SCMTOperationCore.Message.SNMP;
using MAP_STRING = System.Collections.Generic.Dictionary<string, string>;

namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
	public partial class PerSoftwareVer : Form
	{
		public PerSoftwareVer(string boardIp)
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
						lvItem.SubItems.Add(sv.manufacturer);
						lvItem.SubItems.Add(sv.softwareVer);
						lvItem.SubItems.Add(sv.dlTime);
						lvItem.SubItems.Add(sv.softwareStorageFolder);
						lvItem.SubItems.Add(sv.relayVer);
						listView1.Items.Add(lvItem);
						lvIndex++;
					}
				}
			}
		}

		private void Init5216SoftWareInfo()
		{
			comboBox1.Items.Add("RRU软件");
			comboBox1.Items.Add("电调天线");
			comboBox1.Items.Add("环境监控软件");
			comboBox1.Items.Add("GPS软件");
			comboBox1.Items.Add("1588软件");
			comboBox1.Items.Add("北斗软件");
			comboBox1.SelectedIndex = 0;

			listView1.Columns.Add("编号", 50, HorizontalAlignment.Center);
			listView1.Columns.Add("行状态", -2, HorizontalAlignment.Center);
			listView1.Columns.Add("厂商", -2, HorizontalAlignment.Center);
			listView1.Columns.Add("软件包版本", 120, HorizontalAlignment.Center);
			listView1.Columns.Add("软件包下载时间", 120, HorizontalAlignment.Center);
			listView1.Columns.Add("软件包存储路径", 120, HorizontalAlignment.Center);
			listView1.Columns.Add("补丁包依赖版本", 120, HorizontalAlignment.Center);
			listView1.View = System.Windows.Forms.View.Details;

			for (int index1 = 1; index1 < 7; index1++)      // 软件包类型
			{
				for (int index2 = 1; index2 < 3; index2++)	// 厂商
				{
					var manu = (index2 == 1) ? "大唐移动" : "其他厂家";
					for (int index3 = 1; index3 < 3; index3++)	// 软件版本索引
					{
						var index = $".{index2}.{index1}.{index3}.10";
						// 先清空所有的值
						var mibValues = new MAP_STRING
						{
							["swPackRowStatus"] = "",
							["swPackVersion"] = "",
							["swPackDownloadTime"] = "",
							["swPackDirectory"] = "",
							["swPackRelyVesion"] = ""
						};

						if (SnmpToDatabase.GetMibValueFromCmdExeResult(index, "GetSWPack", ref mibValues, _boardIp))
						{
							var sv = new SoftwareVersion
							{
								rowStatus = SnmpToDatabase.GetRowStatusText(mibValues["swPackRowStatus"]),
								softwareVer = mibValues["swPackVersion"].Trim('\0'),
								dlTime = ConvertTimeFormat(mibValues["swPackDownloadTime"]),
								softwareStorageFolder = mibValues["swPackDirectory"],
								relayVer = mibValues["swPackRelyVesion"],
								manufacturer = manu
							};

							if (_mapSoftwareVer.ContainsKey(index1 - 1))
							{
								var svList = _mapSoftwareVer[index1 - 1];
								svList.Add(sv);
							}
							else
							{
								var svList = new List<SoftwareVersion> { sv };
								_mapSoftwareVer[index1 - 1] = svList;
							}
						}
					}
				}
			}
			comboBox1_SelectedIndexChanged(this, null);
		}

		private string ConvertTimeFormat(string binaryText)
		{
			var localTime = TimeHelper.ConvertUtcTimeTextToDateTime(binaryText).ConvertTimeZoneToLocal();
			return TimeHelper.DateTimeToString(localTime);
		}

		private string _boardIp { get; }

		private Dictionary<int, List<SoftwareVersion>> _mapSoftwareVer;       // key: 一维索引值-1，对应到combobox上item的index
	}

}
