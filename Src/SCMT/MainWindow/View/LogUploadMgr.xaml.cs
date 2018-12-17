using LogUploadManager;
using SCMTMainWindow.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using static LogUploadManager.CommonFunction;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// LogUploadMgr.xaml 的交互逻辑
    /// </summary>
    public partial class LogUploadMgr : UserControl
    {
        private bool SelectedAllFlag;
        private DispatcherTimer timer = new DispatcherTimer();
        private LogUploadMgrModel viewModel = new LogUploadMgrModel();
        public LogUploadMgr()
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }
        /// <summary>
        /// 当选择全选时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //LogUploadMgrModel.Singleton.SetAllPubllicFileSeletedStatus(true);
            viewModel.SetAllPubllicFileSeletedStatus(true);
            SelectedAllFlag = true;
        }
        /// <summary>
        /// 取消全选时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SelectedAllFlag == true)
            {
                //LogUploadMgrModel.Singleton.SetAllPubllicFileSeletedStatus(false);
                viewModel.SetAllPubllicFileSeletedStatus(false);
                SelectedAllFlag = false;
            }
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //LogUploadMgrModel.Singleton.SelectedSingleFile();
            viewModel.SelectedSingleFile();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectedAllFlag = false;
            //LogUploadMgrModel.Singleton.UnSelectedSingleFile();
            viewModel.UnSelectedSingleFile();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            //LogUploadMgrModel.Singleton.SelectedOther();
            viewModel.SelectedOther();
        }

        private void FindTheSaveFilePath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            this.SaveFilePath.Text = dialog.SelectedPath;
        }

        private void ExportLogFile_Click(object sender, RoutedEventArgs e)
        {
            //LogUploadMgrModel.Singleton.UploadLogList(LOGTYPEENUM.ENUM_PUBLICLOG, this.SaveFilePath.Text);     
            viewModel.UploadLogList(LOGTYPEENUM.ENUM_PUBLICLOG, this.SaveFilePath.Text);
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {            
            //判断是否全部上传结束
            //如果没有上传结束，继续触发定时器方法 刷新进度
            //if (false== LogUploadMgrModel.Singleton.GetAllUploadProgress((LOGTYPEENUM )this.LogTabControl.SelectedIndex)) {
            if (true == viewModel.GetAllUploadProgress((LOGTYPEENUM)this.LogTabControl.SelectedIndex))
            {
                timer.Stop();
            }//如果全部上传完成，1刷新页面 全部上传完成,2.停止定时器
            else {
                
                   
            }
        }
    }
}
