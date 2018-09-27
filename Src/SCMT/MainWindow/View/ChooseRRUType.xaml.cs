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
    /// ChooseRRUType.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseRRUType : Window
    {
        public int nMaxRRUPath;                       //最大通道数
        public bool bOK = false;
        private int nMaxRRUNumber;                 //配置文件中获取的最大支持的网元数量
        public int nRRUNumber;                        //输入的当前要拖拽的网元的数量
        public string strRRUName;                     //获取RRU的类型名称

        //全局变量，保存RRU的属性
        private Dictionary<string, InitialRruInfo> dirRRU;

        public ChooseRRUType()
        {
            InitializeComponent();

            //获取RRU的所有属性
            dirRRU = NPERruHelper.GetInstance().GetRruInfoMap();

            if(dirRRU.Count > 0)
            {
                //填充RRU的类型
                foreach (string strRRUName in dirRRU.Keys)
                {
                    this.cbRRUtype.Items.Add(strRRUName);
                }

                this.cbRRUtype.SelectedIndex = 0;
            }
            //从配置文件获取最大拖动网元数量
            string strInfo = File.ReadAllText(".\\Component\\Configration\\RRUPropertyConfig.json");
            Dictionary<string, int> obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(strInfo);
            nMaxRRUNumber = obj["RRUMaxNumber"];

            this.txtRRUNumber.Text = "1";
            this.txtRRUNumber.Focus();

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbRRUtype.SelectedItem != null)
            {
                strRRUName = this.cbRRUtype.SelectedItem.ToString();
                nMaxRRUPath = dirRRU[strRRUName].rruTypeMaxAntPathNum;

                try
                {
                    nRRUNumber = int.Parse(this.txtRRUNumber.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字");
                }

                bOK = true;
                this.Close();
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            nMaxRRUPath = 0;
            bOK = false;
            this.Close();
        }

        private void cbRRUWorkModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        //RRU的类型选择改变的时候，需要重新加载  描述和工作模式
        private void cbRRUtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string strSelectedRRUName = this.cbRRUtype.SelectedItem.ToString();

            if (strSelectedRRUName != "")
            {
                InitialRruInfo info = dirRRU[strSelectedRRUName];

                this.txtRRUdescription.Content = info.rruInfoDesc;

                this.cbRRUWorkModel.Items.Clear();
                List<string> listWorkModel = info.rruWorkMode;
                foreach (string strWorkModel in listWorkModel)
                {
                    this.cbRRUWorkModel.Items.Add(strWorkModel);
                }

                this.cbRRUWorkModel.SelectedIndex = 0;
            }
        }

        private void txtRRUNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string strNumber = this.txtRRUNumber.Text;

                int num = int.Parse(strNumber);

                if(num > nMaxRRUNumber || num < 0)
                {
                    string strText = string.Format("最大支持 {0} 个网元", nMaxRRUNumber);
                    MessageBox.Show(strText);
                }
            }
            catch
            {
                MessageBox.Show("请输入数字");
            }
        }
    }
}
