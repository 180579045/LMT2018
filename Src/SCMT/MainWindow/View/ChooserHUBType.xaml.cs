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

using NetPlan;
using System.IO;
using Newtonsoft.Json;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// ChooserHUBType.xaml 的交互逻辑
    /// </summary>
    public partial class ChooserHUBType : Window
    {
        public int nRHUBType;                    //通道数
        public bool bOK = false;
        private int nMaxRHUBNo;
        public int nRHUBNo;                       //要添加的rHUB的数量

        private List<RHUBEquipment> listRHUB;

        public ChooserHUBType()
        {
            InitializeComponent();

            listRHUB = NPEBoardHelper.GetInstance().GetRhubEquipments();

            if(listRHUB != null && listRHUB.Count != 0)
            {
                foreach(RHUBEquipment item in listRHUB)
                {
                    this.cbrHUBType.Items.Add(item.friendlyUIName);
                }
            }

            this.cbrHUBType.SelectedIndex = 0;
            //从配置文件获取最大拖动网元数量
            string strInfo = File.ReadAllText(".\\Component\\Configration\\RRUPropertyConfig.json");
            Dictionary<string, int> obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(strInfo);
            nMaxRHUBNo = obj["rHUBMaxNumber"];

            this.txtrHUBNumber.Text = "1";
            this.txtrHUBNumber.Focus();
        }

        //输入框只能输入数字
        private void txtrHUBNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string strNumber = this.txtrHUBNumber.Text;

                int num = int.Parse(strNumber);

                if (num > nMaxRHUBNo || num < 0)
                {
                    string strText = string.Format("最大支持 {0} 个网元", nMaxRHUBNo);
                    MessageBox.Show(strText);
                }
            }
            catch
            {
                MessageBox.Show("请输入数字");
            }
        }

        //确认
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(this.cbrHUBType.SelectedItem != null)
            {
                string strRHUBType = this.cbrHUBType.SelectedItem.ToString();
                foreach(RHUBEquipment item in listRHUB)
                {
                    if(item.friendlyUIName == strRHUBType)
                    {
                        nRHUBType = item.ethPortRNum;
                    }
                }

                try
                {
                    nRHUBNo = int.Parse(this.txtrHUBNumber.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字");
                }

            }

            bOK = true;
            this.Close();
        }

        //取消
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            bOK = false;
            this.Close();
        }
    }
}
