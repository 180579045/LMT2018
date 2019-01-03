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
using CfgFileOperation;//配置文件操作类
using System.IO;
using LogManager;

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
            //dlg.Filter = "数据库文件 | *.dtz|*.mdb";
            dlg.Filter = "数据库文件 | *.mdb";
            if (dlg.ShowDialog() == true)
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
            bool re = true;
            Dictionary<string, string> path = new Dictionary<string, string>();
            string strErr = "";
            re = parseInputPath(path, out strErr);//验证
            if (!re)
            {
                Log.Debug("UI-CreateInitPatch : Input para err.");
                return;
            }

            string strLogName = path["OutDir"] + "\\" + "LogOperInitPatch.txt";
            FileStream fs = new FileStream(strLogName, FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, true);
            bw.Write(String.Format("*** UI-CreateInitPatch Start... Time is {0}.***\n", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒")).ToArray());
            Log.Debug("UI-CreateInitPatch Start.");
            try
            {
                CfgOp cfgOperation = new CfgOp();
                re = new CfgOp().CreatePatchAndInitCfg5G(bw, path);
                
            }
            catch
            {
                re = false;
                bw.Write("Err exe Death...\n".ToArray());
                Log.Debug("UI-CreateInitPatch Err exe Death.");
            }
            finally
            {
                CfgExcelOp.GetInstance().Dispose();
            }
            bw.Write(String.Format("*** UI-CreateInitPatch End. Time is {0}.***\n", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒")).ToArray());
            Log.Debug("UI-CreateInitPatch End.");
            //清空缓冲区
            bw.Flush();
            //关闭流
            bw.Close();
            fs.Close();

            if(re)
                MessageBox.Show("   解析成功！");
            else
                MessageBox.Show("   解析失败！查看保存文件下的log.");
            return;
        }
        private bool parseInputPath(Dictionary<string, string> path, out string err)
        {
            //
            path.Add("DataMdb", this.tbDataBase.Text);//数据库
            path.Add("Antenna", this.tbAntWeight.Text);//天 线 权 值
            path.Add("Alarm", this.tbWarning.Text);//告 警 信 息
            path.Add("RruInfo", this.tbRRUInfo.Text);//RRU  信 息
            path.Add("Reclist", this.tbRecList.Text);//RecList文件
            path.Add("SelfDef", this.tbSelfDefine.Text);//自定义文件
            path.Add("OutDir", this.tbPathToSave.Text+"\\");//生 成 路 径

            return IsAllPathValid(path, out err);
        }
        /// <summary>
        /// 验证每个文件是否存在
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        private bool IsAllPathValid(Dictionary<string, string> paths, out string err)
        {
            err = "";
            //是否为空
            foreach (var key in paths.Keys)
            {
                if (String.Empty == paths[key])
                {
                    err = String.Format("Err : {0} is null.\n", key);
                    return false;
                }
            }

            //string filePath = paths["DataMdb"];
            if (!File.Exists(paths["DataMdb"]))
            {
                err = "DataMdb: " + paths["DataMdb"] + " 文件不存在！\n";
                return false;
            }
            if (!File.Exists(paths["Antenna"]))
            {
                err = "Antenna: " + paths["Antenna"] + " 文件不存在！\n";
                return false;
            }
            if (!File.Exists(paths["Alarm"]))
            {
                err = "Alarm: " + paths["Alarm"] + " 文件不存在！\n";
                return false;
            }
            if (!File.Exists(paths["RruInfo"]))
            {
                err = "RruInfo: " + paths["RruInfo"] + " 文件不存在！\n";
                return false;
            }
            if (!File.Exists(paths["Reclist"]))
            {
                err = "Reclist: " + paths["Reclist"] + " 文件不存在！\n";
                return false;
            }
            if (!File.Exists(paths["SelfDef"]))
            {
                err = "SelfDef: " + paths["SelfDef"] + " 文件不存在！\n";
                return false;
            }
            if (!Directory.Exists(paths["OutDir"]))//文件夹
            {
                //Directory.Delete(paths["OutDir"], true);//删除文件夹以及文件夹中的子目录，文件    
                //Directory.CreateDirectory(paths["OutDir"]);//如果不存在就创建file文件夹
                err = "OutDir: " + paths["OutDir"] + " 文件夹不存在！\n";
                return false;
            }
            return true;
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
