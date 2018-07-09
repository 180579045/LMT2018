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
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.ComponentModel;
using MsgQueue;
using Microsoft.Win32;

namespace SCMTMainWindow.Component.SCMTControl.LogInfoShow
{
    /// <summary>
    /// OutputWin.xaml 的交互逻辑
    /// </summary>
    public partial class OutputWin : System.Windows.Window
    {
        //全局变量，保存所有的日志信息
        Dictionary<string, Dictionary<InfoTypeEnum, List<LogInfoTitle>>> g_outputLogInfo;
        public OutputWin(List<string> listIP, Dictionary<string, Dictionary<InfoTypeEnum, List<LogInfoTitle>>> g_AllLog)
        {
            InitializeComponent();

            //根据参数得到全部日志信息
            g_outputLogInfo = g_AllLog;

            //根据参数IP地址，初始化ListView，并获取每个IP地址的Log条数
            foreach (string item in listIP)
            {
                SelectedIP newItem = new SelectedIP();
                newItem.TextIP = item;

                for (int i = 0; i < 15; i++)
                {
                    newItem.LogCount += g_outputLogInfo[item][(InfoTypeEnum)i].Count;
                }
                this.lvIPSelect.Items.Add(newItem);
            }

            //初始化Type  ListView
            InitTypeCheckBox();
        }

        private void InitTypeCheckBox()
        {
            string levelText = "";

            for (int i = 0; i < 15; i++)
            {
                switch ((InfoTypeEnum)i)
                {
                    case InfoTypeEnum.ENB_INFO:
                        levelText = "LMT信息";
                        break;
                    case InfoTypeEnum.ENB_TASK_DEAL_INFO:
                        levelText = "LMT-ENB任务处理";
                        break;
                    case InfoTypeEnum.SI_STR_INFO:
                        levelText = "启动阶段信息上报";
                        break;
                    case InfoTypeEnum.SI_ALARM_INFO:
                        levelText = "启动告警";
                        break;
                    case InfoTypeEnum.OM_BRKDWN_ALARM_INFO:
                        levelText = "故障类告警提示";
                        break;
                    case InfoTypeEnum.OM_EVENT_ALARM_INFO:
                        levelText = "事件类告警提示";
                        break;
                    case InfoTypeEnum.OM_ALARM_CLEAR_INFO:
                        levelText = "告警清除提示";
                        break;
                    case InfoTypeEnum.OM_EVENT_NOTIFY_INFO:
                        levelText = "事件通知";
                        break;
                    case InfoTypeEnum.ENB_GETOP_INFO:
                        levelText = "GET命令响应";
                        break;
                    case InfoTypeEnum.ENB_SETOP_INFO:
                        levelText = "SET命令响应";
                        break;
                    case InfoTypeEnum.ENB_GETOP_ERR_INFO:
                        levelText = "GET命令响应错误";
                        break;
                    case InfoTypeEnum.ENB_SETOP_ERR_INFO:
                        levelText = "SET命令响应错误";
                        break;
                    case InfoTypeEnum.ENB_VARY_INFO:
                        levelText = "变更通知";
                        break;
                    case InfoTypeEnum.ENB_OTHER_INFO:
                        levelText = "其他信息";
                        break;
                    case InfoTypeEnum.ENB_OTHER_INFO_IMPORT:
                        levelText = "其他信息(重要)";
                        break;
                    case InfoTypeEnum.CUSTOM_INFO:
                        break;
                    default:
                        break;
                }

                SelectedType newItem = new SelectedType();
                newItem.TextType = levelText;
                this.lvTypeSelect.Items.Add(newItem);
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
            dlg.InitialDirectory = @"E:\";
            dlg.Filter = "Excel文件|*.xls";
            dlg.ShowDialog();

            this.pathToOutput.Text = dlg.FileName;
        }

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //先判断保存路径是否存在
            if (this.pathToOutput.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("请先选择保存路径");
                return;
            }

            string strFileName = this.pathToOutput.Text;

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
            Workbook excelWB = excelApp.Workbooks.Add(System.Type.Missing);

            for (int i = 1; i <= strIP.Count; i++)
            {
                Worksheet excelWS = (Worksheet)excelWB.Worksheets.Add(System.Type.Missing);
                excelWS.Name = strIP[i - 1];

                int nRows = 0;

                for (int j = 0; j < listType.Count; j++)
                {
                    foreach (LogInfoTitle newLogInfo in g_outputLogInfo[strIP[i - 1]][listType[j]])
                    {
                        excelWS.Cells[nRows + 1, 1] = newLogInfo.LogTime;
                        excelWS.Cells[nRows + 1, 2] = newLogInfo.LogType;
                        excelWS.Cells[nRows + 1, 3] = newLogInfo.LogInfo;

                        nRows++;
                    }
                }

            }

            excelWB.SaveAs(strFileName);
            excelWB.Close();
            excelApp.Quit();

            System.Windows.MessageBox.Show("导出成功");
        }

        private InfoTypeEnum GetEnumByString(string levelText)
        {
            InfoTypeEnum enumInfo = new InfoTypeEnum();

            switch (levelText)
            {
                case "LMT信息":
                    enumInfo = InfoTypeEnum.ENB_INFO;
                    break;
                case "LMT-ENB任务处理":
                    enumInfo = InfoTypeEnum.ENB_TASK_DEAL_INFO;
                    break;
                case "启动阶段信息上报":
                    enumInfo = InfoTypeEnum.SI_STR_INFO;
                    break;
                case "启动告警":
                    enumInfo = InfoTypeEnum.SI_ALARM_INFO;
                    break;
                case "故障类告警提示":
                    enumInfo = InfoTypeEnum.OM_BRKDWN_ALARM_INFO;
                    break;
                case "事件类告警提示":
                    enumInfo = InfoTypeEnum.OM_EVENT_ALARM_INFO;
                    break;
                case "告警清除提示":
                    enumInfo = InfoTypeEnum.OM_ALARM_CLEAR_INFO;
                    break;
                case "事件通知":
                    enumInfo = InfoTypeEnum.OM_EVENT_NOTIFY_INFO;
                    break;
                case "GET命令响应":
                    enumInfo = InfoTypeEnum.ENB_GETOP_INFO;
                    break;
                case "SET命令响应":
                    enumInfo = InfoTypeEnum.ENB_SETOP_INFO;
                    break;
                case "GET命令响应错误":
                    enumInfo = InfoTypeEnum.ENB_GETOP_ERR_INFO;
                    break;
                case "SET命令响应错误":
                    enumInfo = InfoTypeEnum.ENB_SETOP_ERR_INFO;
                    break;
                case "变更通知":
                    enumInfo = InfoTypeEnum.ENB_VARY_INFO;
                    break;
                case "其他信息":
                    enumInfo = InfoTypeEnum.ENB_OTHER_INFO;
                    break;
                case "其他信息(重要)":
                    enumInfo = InfoTypeEnum.ENB_OTHER_INFO_IMPORT;
                    break;
                default:
                    break;
            }

            return enumInfo;
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


    public class SelectedIP : INotifyPropertyChanged
    {
        //属性改变事件
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string strPropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }

        public string TextIP
        {
            get; set;
        }

        bool bIsSelectedIP;
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

        public int LogCount
        {
            get; set;
        }
    }

    public class SelectedType : INotifyPropertyChanged
    {
        //属性改变事件
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string strPropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }

        public string TextType
        {
            get; set;
        }

        bool bIsSelectedType;
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
