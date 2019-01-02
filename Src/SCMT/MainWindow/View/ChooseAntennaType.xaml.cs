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
    /// ChooseAntennaType.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseAntennaType : Window
    {
        private Dictionary<string, List<AntType>> allAntInfo = new Dictionary<string, List<AntType>>();
        private List<SimpleBfScanData> allSimpleData;
        public AntType currentSelectedAntType;
        private int nMaxAntNum = 0;
        public int nAntNo = 1;
        public bool bOK = false;

        public ChooseAntennaType()
        {
            InitializeComponent();

            allAntInfo = NPEAntHelper.GetInstance().GetAllAntTypeInfo();

            if(allAntInfo != null && allAntInfo.Count > 0)
            {
                foreach(var item in allAntInfo)
                {
                    this.cbAntennaType.Items.Add(item.Key);
                }
            }

            this.cbAntennaType.SelectedIndex = 0;

            //从配置文件获取最大拖动网元数量
            string strInfo = File.ReadAllText(".\\Component\\Configration\\RRUPropertyConfig.json");
            Dictionary<string, int> obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(strInfo);
            nMaxAntNum = obj["AntMaxNumber"];
            txtAntennaNumber.Text = "1";
        }

        /// <summary>
        /// 厂家类型改变选择的时候，需要重新填充该厂家的天线阵类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAntennaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbAntennaType.SelectedItem != null)
            {
                if(this.cbAntennaWorkModel.Items.Count > 0)
                {
                    this.cbAntennaWorkModel.Items.Clear();
                }
                string strVendor = cbAntennaType.SelectedItem.ToString();
                if(allAntInfo != null && allAntInfo.Count > 0 && allAntInfo.ContainsKey(strVendor))
                {
                    foreach(var item in allAntInfo[strVendor])
                    {
                        this.cbAntennaWorkModel.Items.Add(item.antArrayModelName);
                    }
                    if(this.cbAntennaWorkModel.Items.Count > 0)
                        this.cbAntennaWorkModel.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 天线阵改变的时候，需要改变它的描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAntennaWorkModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.cbAntennaWorkModel.SelectedItem != null)
            {
                string strVendor = this.cbAntennaType.SelectedItem.ToString();
                if (allAntInfo != null && allAntInfo.Count > 0 && allAntInfo.ContainsKey(strVendor))
                {
                    foreach (var item in allAntInfo[strVendor])
                    {
                        if(item.antArrayModelName == cbAntennaWorkModel.SelectedItem.ToString())
                        {
                            currentSelectedAntType = item;
                            txtRRUdescription.Content = item.antInfoDesc;

                            allSimpleData = NPEAntHelper.GetInstance().GetAntBfsDataByVendorAndTypeIdx(item.antArrayVendor.ToString(), item.antArrayIndex.ToString());

                            ClearSimpleData();
                            foreach(var simpleItem in allSimpleData)
                            {
                                if(!this.cbAntennaHorizontalNum.Items.Contains(simpleItem.horBeamScanCount))
                                    this.cbAntennaHorizontalNum.Items.Add(simpleItem.horBeamScanCount);
                                if(!this.cbAntennaHorizontalAngle.Items.Contains(simpleItem.horBeamScanAngle))
                                    this.cbAntennaHorizontalAngle.Items.Add(simpleItem.horBeamScanAngle);
                                if(!this.cbAntennaVerticalNum.Items.Contains(simpleItem.verBeamScanCount))
                                    this.cbAntennaVerticalNum.Items.Add(simpleItem.verBeamScanCount);
                                if(!this.cbAntennaVerticalAngle.Items.Contains(simpleItem.verBeamScanAngle))
                                    this.cbAntennaVerticalAngle.Items.Add(simpleItem.verBeamScanAngle);
                                if(!this.cbAntennaLossFlag.Items.Contains(simpleItem.lossFlag))
                                    this.cbAntennaLossFlag.Items.Add(simpleItem.lossFlag);
                            }

                            if (this.cbAntennaHorizontalNum.Items.Count > 0)
                                this.cbAntennaHorizontalNum.SelectedIndex = 0;

                            return;
                        }
                    }

                }
            }
        }

        private void txtAntennaNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string strNumber = this.txtAntennaNumber.Text;

                nAntNo = int.Parse(strNumber);

                if (nAntNo > nMaxAntNum || nAntNo < 0)
                {
                    string strText = string.Format("最大支持 {0} 个网元", nMaxAntNum);
                    txtAntennaNumber.Text = "1";
                    MessageBox.Show(strText);
                }
            }
            catch
            {
                txtAntennaNumber.Text = "1";
                MessageBox.Show("请输入数字");
            }
    }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(currentSelectedAntType!= null)
            {
                bOK = true;
            }
            else
            {
                bOK = false;
            }

            this.Close();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            bOK = false;
            this.Close();
        }

        /// <summary>
        /// 水平波束个数改变的时候，需要确定别的选项的可选值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAntennaHorizontalNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbAntennaHorizontalNum.SelectedItem == null)
                return;
            string strItem = this.cbAntennaHorizontalNum.SelectedItem.ToString();

            if (this.cbAntennaHorizontalAngle.Items.Count > 0)
                this.cbAntennaHorizontalAngle.Items.Clear();
            if (this.cbAntennaVerticalNum.Items.Count > 0)
                this.cbAntennaVerticalNum.Items.Clear();
            if (this.cbAntennaVerticalAngle.Items.Count > 0)
                this.cbAntennaVerticalAngle.Items.Clear();
            if (this.cbAntennaLossFlag.Items.Count > 0)
                this.cbAntennaLossFlag.Items.Clear();

            foreach (var simpleItem in allSimpleData)
            {
                if(simpleItem.horBeamScanCount == strItem)
                {
                    if (!this.cbAntennaHorizontalAngle.Items.Contains(simpleItem.horBeamScanAngle))
                        this.cbAntennaHorizontalAngle.Items.Add(simpleItem.horBeamScanAngle);
                    if (!this.cbAntennaVerticalNum.Items.Contains(simpleItem.verBeamScanCount))
                        this.cbAntennaVerticalNum.Items.Add(simpleItem.verBeamScanCount);
                    if (!this.cbAntennaVerticalAngle.Items.Contains(simpleItem.verBeamScanAngle))
                        this.cbAntennaVerticalAngle.Items.Add(simpleItem.verBeamScanAngle);
                    if (!this.cbAntennaLossFlag.Items.Contains(simpleItem.lossFlag))
                        this.cbAntennaLossFlag.Items.Add(simpleItem.lossFlag);
                }
            }
            
            if (this.cbAntennaHorizontalAngle.Items.Count > 0)
                this.cbAntennaHorizontalAngle.SelectedIndex = 0;
            if (this.cbAntennaVerticalNum.Items.Count > 0)
                this.cbAntennaVerticalNum.SelectedIndex = 0;
            if (this.cbAntennaVerticalAngle.Items.Count > 0)
                this.cbAntennaVerticalAngle.SelectedIndex = 0;
            if (this.cbAntennaLossFlag.Items.Count > 0)
                this.cbAntennaLossFlag.SelectedIndex = 0;
        }

        private void ClearSimpleData()
        {
            if (this.cbAntennaHorizontalNum.Items.Count > 0)
                this.cbAntennaHorizontalNum.Items.Clear();
            if (this.cbAntennaHorizontalAngle.Items.Count > 0)
                this.cbAntennaHorizontalAngle.Items.Clear();
            if (this.cbAntennaVerticalNum.Items.Count > 0)
                this.cbAntennaVerticalNum.Items.Clear();
            if (this.cbAntennaVerticalAngle.Items.Count > 0)
                this.cbAntennaVerticalAngle.Items.Clear();
            if (this.cbAntennaLossFlag.Items.Count > 0)
                this.cbAntennaLossFlag.Items.Clear();
        }

        private void cbAntennaHorizontalAngle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void cbAntennaVerticalNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbAntennaVerticalNum.SelectedItem == null)
                return;
            string strItem = this.cbAntennaVerticalNum.SelectedItem.ToString();

            if (this.cbAntennaHorizontalAngle.Items.Count > 0)
                this.cbAntennaHorizontalAngle.Items.Clear();
            if (this.cbAntennaVerticalAngle.Items.Count > 0)
                this.cbAntennaVerticalAngle.Items.Clear();
            if (this.cbAntennaLossFlag.Items.Count > 0)
                this.cbAntennaLossFlag.Items.Clear();

            foreach (var simpleItem in allSimpleData)
            {
                if (simpleItem.verBeamScanCount == strItem)
                {
                    if (!this.cbAntennaHorizontalAngle.Items.Contains(simpleItem.horBeamScanAngle))
                        this.cbAntennaHorizontalAngle.Items.Add(simpleItem.horBeamScanAngle);
                    if (!this.cbAntennaVerticalAngle.Items.Contains(simpleItem.verBeamScanAngle))
                        this.cbAntennaVerticalAngle.Items.Add(simpleItem.verBeamScanAngle);
                    if (!this.cbAntennaLossFlag.Items.Contains(simpleItem.lossFlag))
                        this.cbAntennaLossFlag.Items.Add(simpleItem.lossFlag);
                }
            }

            if (this.cbAntennaHorizontalAngle.Items.Count > 0)
                this.cbAntennaHorizontalAngle.SelectedIndex = 0;
            if (this.cbAntennaVerticalAngle.Items.Count > 0)
                this.cbAntennaVerticalAngle.SelectedIndex = 0;
            if (this.cbAntennaLossFlag.Items.Count > 0)
                this.cbAntennaLossFlag.SelectedIndex = 0;


        }

        private void cbAntennaVerticalAngle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbAntennaLossFlag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
