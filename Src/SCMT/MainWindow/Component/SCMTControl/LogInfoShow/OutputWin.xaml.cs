using Microsoft.Office.Interop.Excel;
using MsgQueue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace SCMTMainWindow.Component.SCMTControl.LogInfoShow
{
	/// <summary>
	/// OutputWin.xaml 的交互逻辑
	/// </summary>
	public partial class OutputWin : System.Windows.Window
	{
		//全局变量，保存所有的日志信息
		private Dictionary<string, Dictionary<InfoTypeEnum, List<LogInfoTitle>>> g_outputLogInfo;

		public OutputWin(IEnumerable<string> listIP, Dictionary<string, Dictionary<InfoTypeEnum, List<LogInfoTitle>>> g_AllLog)
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			Topmost = true;

			//根据参数得到全部日志信息
			g_outputLogInfo = g_AllLog;

			var len = Enum.GetValues(typeof(InfoTypeEnum)).Length;

			//根据参数IP地址，初始化ListView，并获取每个IP地址的Log条数
			foreach (var item in listIP)
			{
				var newItem = new SelectedIP {TextIP = item};

				for (var i = 0; i < len; i++)
				{
					if (g_outputLogInfo[item].ContainsKey((InfoTypeEnum)i))
						newItem.LogCount += g_outputLogInfo[item][(InfoTypeEnum)i].Count;
				}
				lvIPSelect.Items.Add(newItem);
			}

			//初始化Type  ListView
			InitTypeCheckBox();
		}

		private void InitTypeCheckBox()
		{
			var len = (InfoTypeEnum[])Enum.GetValues(typeof(InfoTypeEnum));
			foreach (var t in len)
			{
				var levelText = InfoTypeConvert.GetDescByType(t);
				if (string.IsNullOrEmpty(levelText))
				{
					continue;
				}

				var newItem = new SelectedType { TextType = levelText };
				lvTypeSelect.Items.Add(newItem);
			}
		}

		/// <summary>
		/// 路径选择按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new SaveFileDialog();
			dlg.InitialDirectory = @"C:\";
			dlg.Filter = "Excel文件|*.xls";
			dlg.ShowDialog();

			pathToOutput.Text = dlg.FileName;
		}

		/// <summary>
		/// 导出按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			//先判断保存路径是否存在
			if (pathToOutput.Text == string.Empty)
			{
				System.Windows.MessageBox.Show("请先选择保存路径");
				return;
			}

			string strFileName = pathToOutput.Text;

			List<string> strIP = new List<string>();
			List<InfoTypeEnum> listType = new List<InfoTypeEnum>();

			//获取被选中的IP地址
			foreach (SelectedIP item in lvIPSelect.Items)
			{
				if (item.IsSelectedIP)
				{
					strIP.Add(item.TextIP);
				}
			}

			if (strIP.Count == 0)
			{
				System.Windows.MessageBox.Show("没有选中任何IP地址");
				return;
			}

			//获取被选中的消息级别
			foreach (SelectedType item in lvTypeSelect.Items)
			{
				if (item.IsSelectedType)
				{
					string strText = item.TextType;
					listType.Add(GetEnumByString(strText));
				}
			}

			if (listType.Count == 0)
			{
				System.Windows.MessageBox.Show("没有选中任何消息级别");
				return;
			}

			//开始导出excel
			Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
			Workbook excelWB = excelApp.Workbooks.Add(Type.Missing);

			//每个IP地址是一个  sheet 表单
			for (int i = 1; i <= strIP.Count; i++)
			{
				Worksheet excelWS = (Worksheet)excelWB.Worksheets.Add(Type.Missing);
				excelWS.Name = strIP[i - 1];

				//行数
				int nRows = 0;

				foreach (var t in listType)
				{
					foreach (LogInfoTitle newLogInfo in g_outputLogInfo[strIP[i - 1]][t])
					{
						excelWS.Cells[nRows + 1, 1] = newLogInfo.LogTime;
						excelWS.Cells[nRows + 1, 2] = newLogInfo.LogType;
						excelWS.Cells[nRows + 1, 3] = newLogInfo.LogInfo;

						nRows++;
					}
				}
			}

			try
			{
				excelWB.SaveAs(strFileName);
			}
			catch (Exception exception)
			{
				string strMsg = exception.ToString();
				System.Windows.MessageBox.Show(strMsg);

				excelWB.Close();
				excelApp.Quit();
			}

			excelWB.Close();
			excelApp.Quit();

			System.Windows.MessageBox.Show("导出成功");
		}

		/// <summary>
		/// 通过字符串信息获取InfoTypeEnum的值
		/// </summary>
		/// <param name="levelText"></param>
		/// <returns></returns>
		private InfoTypeEnum GetEnumByString(string levelText)
		{
			return InfoTypeConvert.GetTypeByDesc(levelText);
		}

		/// <summary>
		/// IP地址全选
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			foreach (SelectedIP item in lvIPSelect.Items)
			{
				item.IsSelectedIP = true;
			}
		}

		/// <summary>
		/// IP地址取消全选
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuItem_Click_1(object sender, RoutedEventArgs e)
		{
			foreach (SelectedIP item in lvIPSelect.Items)
			{
				item.IsSelectedIP = false;
			}
		}

		/// <summary>
		/// 类型  全选
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuItem_Click_2(object sender, RoutedEventArgs e)
		{
			foreach (SelectedType item in lvTypeSelect.Items)
			{
				item.IsSelectedType = true;
			}
		}

		/// <summary>
		/// 类型取消全选
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuItem_Click_3(object sender, RoutedEventArgs e)
		{
			foreach (SelectedType item in lvTypeSelect.Items)
			{
				item.IsSelectedType = false;
			}
		}
	}

	/// <summary>
	/// listview中待选择的IP地址类
	/// </summary>
	public class SelectedIP : INotifyPropertyChanged
	{
		//属性改变事件
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string strPropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
		}

		//IP地址
		public string TextIP
		{
			get; set;
		}

		//是否被选中的属性
		private bool bIsSelectedIP;

		public bool IsSelectedIP
		{
			get
            {
                return bIsSelectedIP;
            } 
			set
			{
				bIsSelectedIP = value;
				RaisePropertyChanged("IsSelectedIP");
			}
		}

		//当前IP地址拥有的日志信息条数
		public int LogCount
		{
			get; set;
		}
	}

	/// <summary>
	/// ListView 中待选择的  Type  类型
	/// </summary>
	public class SelectedType : INotifyPropertyChanged
	{
		//属性改变事件
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string strPropertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyName));
		}

		public string TextType
		{
			get; set;
		}

		private bool bIsSelectedType;

		public bool IsSelectedType
		{
			get
            {
                return bIsSelectedType;
            }
			set
			{
				bIsSelectedType = value;
				RaisePropertyChanged("IsSelectedType");
			}
		}
	}
}