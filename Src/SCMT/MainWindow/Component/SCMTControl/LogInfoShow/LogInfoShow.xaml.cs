using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MsgQueue;
using System.Windows.Controls.Primitives;

namespace SCMTMainWindow.Component.SCMTControl.LogInfoShow
{
    /// <summary>
    /// LogInfoShow.xaml 的交互逻辑
    /// </summary>
    public partial class LogInfoShow : UserControl
    {
        //全局变量，保存所有的Log日志信息
        public Dictionary<string, Dictionary<InfoTypeEnum, List<LogInfoTitle>>> g_AllLog = new Dictionary<string, Dictionary<InfoTypeEnum, List<LogInfoTitle>>>();

        private int nMaxInfoTypeEnum = 15;

        public LogInfoShow()
        {
            InitializeComponent();

            LoadJsonFile();

            this.UiLogInfo.Items.SortDescriptions.Add(new SortDescription("LogTime", ListSortDirection.Ascending));

        }
        private void LoadJsonFile()
        {

        }

        /// <summary>
        /// 初始化Dictionary中的Dictionary   每个IP对应一个Dictionary，初始化这个
        /// </summary>
        /// <param name="dirLogInfo"></param>
        private void InitOneDirectionLog(Dictionary<InfoTypeEnum, List<LogInfoTitle>> dirLogInfo)
        {
            for (int i = 0; i < 15; i++)
            {
                dirLogInfo[(InfoTypeEnum)i] = new List<LogInfoTitle>();
            }
        }
        public static void AddLogInfo(LogInfoTitle newLogInfo, LogInfoShow LogInfo)
        {
            DateTime nowTime = DateTime.Now;
            newLogInfo.LogTime = nowTime.ToString("yyyy-MM-dd hh:mm:ss");
            
            Color color = Color.FromRgb(250, 250, 250);

            if(newLogInfo.TargetIP == string.Empty)
            {
                MessageBox.Show("IP 地址为空的日志，无法判断");
                return;
            }

            //判断combo是否包含对应的IP地址
            if (!LogInfo.combTargetIP.Items.Contains(newLogInfo.TargetIP))
            {
                LogInfo.combTargetIP.Items.Add(newLogInfo.TargetIP);

                //如果是第一次传递IP地址，则将此地址设置为默认地址
                if (LogInfo.combTargetIP.SelectedItem == null)
                {
                    LogInfo.combTargetIP.SelectedItem = newLogInfo.TargetIP;
                }

                //判断全局变量是否包含该IP，并进行初始化
                if (!LogInfo.g_AllLog.ContainsKey(newLogInfo.TargetIP))
                {
                    LogInfo.g_AllLog.Add(newLogInfo.TargetIP, new Dictionary<InfoTypeEnum, List<LogInfoTitle>>());
                    LogInfo.InitOneDirectionLog(LogInfo.g_AllLog[newLogInfo.TargetIP]);
                }
            }

            switch (newLogInfo.Type)
            {
                case InfoTypeEnum.ENB_INFO:
                    newLogInfo.LogType = "LMT信息";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_TASK_DEAL_INFO:
                    newLogInfo.LogType = "LMT任务处理";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_TASK_DEAL_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_TASK_DEAL_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.SI_STR_INFO:
                    newLogInfo.LogType = "直接显示字符串";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.SI_STR_INFO].Add(newLogInfo);
                    if (LogInfo.cbSI_STR_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.SI_ALARM_INFO:
                    newLogInfo.LogType = "启动告警";
                    color = Color.FromRgb(176, 176, 176);
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.SI_ALARM_INFO].Add(newLogInfo);
                    if (LogInfo.cbSI_ALARM_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.OM_BRKDWN_ALARM_INFO:
                    newLogInfo.LogType = "故障类告警提示";
                    color = Color.FromRgb(255, 0, 0);
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.OM_BRKDWN_ALARM_INFO].Add(newLogInfo);
                    if (LogInfo.cbOM_BRKDWN_ALARM_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.OM_EVENT_ALARM_INFO:
                    newLogInfo.LogType = "事件类告警提示";
                    color = Color.FromRgb(233, 149, 22);
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.OM_EVENT_ALARM_INFO].Add(newLogInfo);
                    if (LogInfo.cbOM_EVENT_ALARM_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.OM_ALARM_CLEAR_INFO:
                    newLogInfo.LogType = "告警清除提示";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.OM_ALARM_CLEAR_INFO].Add(newLogInfo);
                    if (LogInfo.cbOM_ALARM_CLEAR_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.OM_EVENT_NOTIFY_INFO:
                    newLogInfo.LogType = "事件通知";
                    color = Color.FromRgb(36, 36, 255);
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.OM_EVENT_NOTIFY_INFO].Add(newLogInfo);
                    if (LogInfo.cbOM_EVENT_NOTIFY_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_GETOP_INFO:
                    newLogInfo.LogType = "GET命令响应";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_GETOP_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_GETOP_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_SETOP_INFO:
                    newLogInfo.LogType = "SET命令响应";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_SETOP_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_SETOP_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_SETOP_ERR_INFO:
                    newLogInfo.LogType = "SET命令响应错误";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_SETOP_ERR_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_SETOP_ERR_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_GETOP_ERR_INFO:
                    newLogInfo.LogType = "GET命令响应错误";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_GETOP_ERR_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_GETOP_ERR_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_VARY_INFO:
                    newLogInfo.LogType = "变更通知";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_VARY_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_VARY_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_OTHER_INFO:
                    newLogInfo.LogType = "其他信息";
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_OTHER_INFO].Add(newLogInfo);
                    if (LogInfo.cbENB_OTHER_INFO.IsChecked == false)
                    {
                        return;
                    }
                    break;
                case InfoTypeEnum.ENB_OTHER_INFO_IMPORT:
                    newLogInfo.LogType = "其他信息(重要)";
                    color = Color.FromRgb(221, 125, 232);
                    LogInfo.g_AllLog[newLogInfo.TargetIP][InfoTypeEnum.ENB_OTHER_INFO_IMPORT].Add(newLogInfo);
                    if (LogInfo.cbENB_OTHER_INFO_IMPORT.IsChecked == false)
                    {
                        return;
                    }
                    break;
                default:
                    newLogInfo.LogType = "UNKNOW";
                    break;
            }
            newLogInfo.LogColor = new SolidColorBrush(color);

            string strTargetIP = LogInfo.combTargetIP.SelectedItem.ToString();
            if (string.Compare(strTargetIP, newLogInfo.TargetIP) == 0)
            {
                LogInfo.UiLogInfo.Items.Add(newLogInfo);
            }
        }

        /// <summary>
        /// 点击listview 标题栏，实现排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void ListViewTitile_Clicked(object sender, RoutedEventArgs e)
        //{
        //    if(e.OriginalSource is GridViewColumnHeader)
        //    {
        //        GridViewColumn clickedColumn = (e.OriginalSource as GridViewColumnHeader).Column;

        //        if(clickedColumn != null)
        //        {
        //            string bindingProperty = (clickedColumn.DisplayMemberBinding as Binding).Path.Path;

        //            SortDescriptionCollection sdc = UiLogInfo.Items.SortDescriptions;
        //            ListSortDirection sortDirection = ListSortDirection.Ascending;

        //            if(sdc.Count > 0)
        //            {
        //                SortDescription sd = sdc[0];
        //                sortDirection = (ListSortDirection)((((int)sd.Direction) + 1) % 2);
        //                sdc.Clear();
        //            }

        //            sdc.Add(new SortDescription(bindingProperty, sortDirection));
        //        }

        //    }
        //}

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.UiLogInfo.SelectAll();
        }

        /// <summary>
        /// 清除全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string strIP = this.combTargetIP.SelectedItem.ToString();

            //清除的时候不可以直接对IP地址清空，必须对每个Type清空，否则造成第二个字典(Dictionary)中没有关键字(Key)
            for (int i = 0; i < nMaxInfoTypeEnum; i++)
            {
                g_AllLog[strIP][(InfoTypeEnum)i].Clear();
            }
            this.UiLogInfo.Items.Clear();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (this.UiLogInfo.SelectedItems.Count == 0)
            {
                MessageBox.Show("没有选中任何item，无法清除");
                return;
            }
            List<LogInfoTitle> listStr = new List<LogInfoTitle>();
            foreach (LogInfoTitle item in this.UiLogInfo.SelectedItems)
            {
                listStr.Add(item);
            }

            string strIP = this.combTargetIP.SelectedItem.ToString();

            foreach (LogInfoTitle item in listStr)
            {
                //需要删除全局变量中对应的item，比较麻烦
                for (int i = 0; i < nMaxInfoTypeEnum; i++)
                {
                    if (g_AllLog[strIP][(InfoTypeEnum)i].Contains(item))
                    {
                        g_AllLog[strIP][(InfoTypeEnum)i].Remove(item);
                    }

                }
                this.UiLogInfo.Items.Remove(item);
            }
        }

        /// <summary>
        /// 复制全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (this.UiLogInfo.Items.Count != 0)
            {
                string strText = "";
                foreach (LogInfoTitle item in this.UiLogInfo.Items)
                {
                    strText += item.LogTime + "\t" + item.LogType + "\t" + item.LogInfo + "\n";
                }
                Clipboard.SetDataObject(strText);
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (this.UiLogInfo.SelectedItems.Count != 0)
            {
                string strText = "";
                foreach (LogInfoTitle item in this.UiLogInfo.SelectedItems)
                {
                    strText += item.LogTime + "\t" + item.LogType + "\t" + item.LogInfo + "\n";
                }
                Clipboard.SetDataObject(strText);
            }
        }


        /// <summary>
        /// 通用函数 实现对ListView的移除和添加，根据不同的 过滤选项
        /// </summary>
        /// <param name=""></param>
        /// <param name="b_AddOrRemove">true 表示添加  false 表示移除</param>
        private void AddOrRemoveLogInfo(InfoTypeEnum LogFilterIndex, bool b_AddOrRemove)
        {
            if (g_AllLog.Count == 0)
            {
                return;
            }

            string strIP = this.combTargetIP.SelectedItem.ToString();

            if (g_AllLog[strIP][LogFilterIndex] == null || g_AllLog[strIP][LogFilterIndex].Count == 0)
            {
                return;
            }

            if (b_AddOrRemove)
            {
                foreach (LogInfoTitle item in g_AllLog[strIP][LogFilterIndex])
                {
                    this.UiLogInfo.Items.Add(item);
                }
            }
            else
            {
                foreach (LogInfoTitle item in g_AllLog[strIP][LogFilterIndex])
                {
                    this.UiLogInfo.Items.Remove(item);
                }
            }

            this.UiLogInfo.Items.SortDescriptions.Add(new SortDescription("LogTime", ListSortDirection.Ascending));
        }

        /// <summary>
        /// LMT信息  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_INFO, true);
        }

        /// <summary>
        /// LMT信息  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_INFO, false);
        }

        /// <summary>
        /// LMT任务处理  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_TASK_DEAL_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_TASK_DEAL_INFO, true);
        }

        /// <summary>
        /// LMT任务处理  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_TASK_DEAL_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_TASK_DEAL_INFO, false);
        }

        /// <summary>
        /// 直接显示字符串  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSI_STR_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.SI_STR_INFO, true);
        }

        /// <summary>
        /// 直接显示字符串  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSI_STR_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.SI_STR_INFO, false);
        }

        /// <summary>
        /// 启动告警   显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSI_ALARM_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.SI_ALARM_INFO, true);
        }

        /// <summary>
        /// 启动告警  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSI_ALARM_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.SI_ALARM_INFO, false);
        }

        /// <summary>
        /// 故障类告警提示  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_BRKDWN_ALARM_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_BRKDWN_ALARM_INFO, true);
        }

        /// <summary>
        /// 故障类告警提示  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_BRKDWN_ALARM_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_BRKDWN_ALARM_INFO, false);
        }

        /// <summary>
        /// 事件类告警提示  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_EVENT_ALARM_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_EVENT_ALARM_INFO, true);
        }

        /// <summary>
        /// 事件类告警提示  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_EVENT_ALARM_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_EVENT_ALARM_INFO, false);
        }

        /// <summary>
        /// 告警清除提示  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_ALARM_CLEAR_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_ALARM_CLEAR_INFO, true);
        }

        /// <summary>
        /// 告警清除提示  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_ALARM_CLEAR_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_ALARM_CLEAR_INFO, false);
        }

        /// <summary>
        /// 事件通知  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_EVENT_NOTIFY_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_EVENT_NOTIFY_INFO, true);
        }

        /// <summary>
        /// 事件通知  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOM_EVENT_NOTIFY_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.OM_EVENT_NOTIFY_INFO, false);
        }

        /// <summary>
        /// GET命令响应  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_GETOP_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_GETOP_INFO, true);
        }

        /// <summary>
        /// GET命令响应  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_GETOP_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_GETOP_INFO, false);
        }

        /// <summary>
        /// 命令响应  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_SETOP_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_SETOP_INFO, true);
        }

        /// <summary>
        /// 命令响应  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_SETOP_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_SETOP_INFO, false);
        }

        /// <summary>
        /// GET命令响应错误  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_GETOP_ERR_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_GETOP_ERR_INFO, true);
        }

        /// <summary>
        /// GET命令响应错误  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_GETOP_ERR_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_GETOP_ERR_INFO, false);
        }

        /// <summary>
        /// SET命令响应错误  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_SETOP_ERR_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_SETOP_ERR_INFO, true);
        }

        /// <summary>
        /// SET命令响应错误  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_SETOP_ERR_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_SETOP_ERR_INFO, false);
        }

        /// <summary>
        /// 变更通知  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_VARY_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_VARY_INFO, true);
        }

        /// <summary>
        /// 变更通知  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_VARY_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_VARY_INFO, false);
        }

        /// <summary>
        /// 其他信息  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_OTHER_INFO_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_OTHER_INFO, true);
        }

        /// <summary>
        /// 其他信息  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_OTHER_INFO_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_OTHER_INFO, false);
        }

        /// <summary>
        /// 其他信息(重要)  显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_OTHER_INFO_IMPORT_Checked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_OTHER_INFO_IMPORT, true);
        }

        /// <summary>
        /// 其他信息(重要)  不显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbENB_OTHER_INFO_IMPORT_Unchecked(object sender, RoutedEventArgs e)
        {
            AddOrRemoveLogInfo(InfoTypeEnum.ENB_OTHER_INFO_IMPORT, false);
        }


        /// <summary>
        /// 选择不同的  comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combTargetIP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object obj = this.combTargetIP.SelectedItem;
            string strTargetIP = obj.ToString();

            if (!g_AllLog.ContainsKey(strTargetIP))
            {
                return;
            }

            this.UiLogInfo.Items.Clear();

            for (int i = 0; i < 15; i++)
            {
                foreach (LogInfoTitle item in g_AllLog[strTargetIP][(InfoTypeEnum)i])
                {
                    this.UiLogInfo.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            List<string> listIP = new List<string>();
            foreach (object obj in this.combTargetIP.Items)
            {
                listIP.Add(obj.ToString());
            }

            if (listIP.Count == 0)
            {
                MessageBox.Show("没有可以导出的内容，IP地址为空");
                return;
            }

            OutputWin win = new OutputWin(listIP, g_AllLog);
            win.ShowDialog();
        }
    }


    public class LogInfoTitle : INotifyPropertyChanged
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
        string strLogTime;
        string strLogType;
        string strLogInfo;
        Brush clrLogColor;
        InfoTypeEnum enumType;
        string strTargetIP;

        public string TargetIP
        {
            get { return strTargetIP; }
            set { strTargetIP = value; }
        }

        public InfoTypeEnum Type
        {
            get { return enumType; }
            set { enumType = value; }
        }

        public string LogTime
        {
            get { return strLogTime; }
            set { strLogTime = value; }
        }

        public string LogType
        {
            get { return strLogType; }
            set { strLogType = value; }
        }

        public string LogInfo
        {
            get { return strLogInfo; }
            set { strLogInfo = value; }
        }

        public Brush LogColor
        {
            get { return clrLogColor; }
            set
            {
                clrLogColor = value;
                RaisePropertyChanged("clrLogColor");
            }
        }
    }
    
}
