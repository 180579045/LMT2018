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
using System.Windows.Navigation;
using System.Windows.Shapes;

using LmtbSnmp;
using LinkPath;
using SCMTOperationCore.Control;
using SCMTOperationCore.Message.SI;
using CommonUtility;

namespace SCMTMainWindow.Component.SCMTControl
{
    /// <summary>
    /// InitConfigSetting.xaml 的交互逻辑
    /// </summary>
    public partial class InitConfigSetting : Window
    {
        private List<string> listIP = new List<string>();
        public InitConfigSetting()
        {
            InitializeComponent();

            var allConnectedGNB = NodeBControl.GetInstance().GetAllConnectedGNB();

            foreach(var item in allConnectedGNB)
            {
                CheckBox newChb = new CheckBox();
                newChb.Content = item.Value + "(" + item.Key + ")";

                newChb.Margin = new Thickness(20, 20, 0, 0);

                newChb.Checked += NewChb_Checked;
                newChb.Unchecked += NewChb_Unchecked;

                this.stackForIP.Children.Add(newChb);
            }
        }

        /// <summary>
        /// IP地址被选中，添加到全局变量中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewChb_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox targetCHB = sender as CheckBox;
            string strIP = targetCHB.Content.ToString().Substring(targetCHB.Content.ToString().IndexOf('(')+1).TrimEnd(')');
            if (listIP.Contains(strIP))
                listIP.Remove(strIP);
        }

        /// <summary>
        /// IP地址取消选中，从全局变量中删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewChb_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox targetCHB = sender as CheckBox;
            string strIP = targetCHB.Content.ToString().Substring(targetCHB.Content.ToString().IndexOf('(')+1).TrimEnd(')');
            if (!listIP.Contains(strIP))
                listIP.Add(strIP);
        }

        /// <summary>
        /// 命令下发按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(listIP.Count <=0 )
            {
                MessageBox.Show("请选择要发送的基站 IP ");
                return;
            }
            else
            {
                string strMSG = string.Empty;
                if (this.chbgNB.IsChecked == true)
                {
                    MessageBoxResult ret = MessageBox.Show("是否生成动态配置文件？", "提示", MessageBoxButton.YesNoCancel);
                    if (ret == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    else if (ret == MessageBoxResult.Yes)
                    {
                        string strCMDName = "ResetEquip";
                        Dictionary<string, string> m_dir = new Dictionary<string, string>();
                        m_dir.Add("equipResetTrigger", "1");

                        foreach (var item in listIP)
                        {
                            if(CDTCmdExecuteMgr.CmdSetSync(strCMDName, m_dir, ".0", item) == 0)
                            {
                                strMSG += item + "  复位成功\n";
                            }
                            else
                            {
                                strMSG += item + "  复位失败\n";
                            }
                        }

                        MessageBox.Show(strMSG);
                        return;
                    }
                    else
                    {
                        var header = new SiMsgHead();
                        header.u16MsgType = 0x50;
                        header.u16MsgLength = 4;
                        byte[] buffer = new byte[header.u16MsgLength];
                        header.SerializeToBytes(ref buffer, 0);

                        foreach (var item in listIP)
                        {
                            if (NodeBControl.SendSiMsg(item, buffer))
                            {
                                strMSG += item + "  复位成功\n";
                            }
                            else
                            {
                                strMSG += item + "  复位失败\n";
                            }
                        }

                        MessageBox.Show(strMSG);
                        return;
                    }
                }

                //如果不是选择复位，则进行其他命令下发
                foreach (var item in listIP)
                {
                    if (this.chbNoClock.IsChecked == true)
                    {
                        Dictionary<string, string> dicNolock = new Dictionary<string, string>();
                        dicNolock.Add("sysStartIsNoClkSrcMode", "1");
                        if(CDTCmdExecuteMgr.CmdSetSync("SetSysStartIsNoClkSrcSete", dicNolock, ".0", item) == 0)
                        {
                            strMSG += item + "  无时钟源启动 下发成功\n";
                        }
                        else
                        {
                            strMSG += item + "  无时钟源启动 下发失败\n";
                        }
                    }

                    if (this.chbNomme.IsChecked == true)
                    {
                        var header = new SiMsgHead();
                        header.u16MsgType = 0x70;
                        header.u16MsgLength = 4;
                        byte[] buffer = new byte[header.u16MsgLength];
                        header.SerializeToBytes(ref buffer, 0);

                        if(NodeBControl.SendSiMsg(item, buffer))
                        {
                            strMSG += item + "  无mme 下发成功\n";
                        }
                        else
                        {
                            strMSG += item + "  无mme 下发失败\n";
                        }
                    }
                }

                if (strMSG != string.Empty)
                {
                    MessageBox.Show(strMSG);
                }
            }            
        }

        /// <summary>
        /// 选中gNB时，其他不可选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(this.chbgNB.IsChecked == true)
            {
                this.chbNoClock.IsChecked = false;
                this.chbNomme.IsChecked = false;

                this.chbNomme.IsEnabled = false;
                this.chbNoClock.IsEnabled = false;
            }
        }

        private void chbgNB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.chbgNB.IsChecked == false)
            {
                this.chbNomme.IsEnabled = true;
                this.chbNoClock.IsEnabled = true;
            }
        }
    }
}
