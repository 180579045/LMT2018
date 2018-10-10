using LogManager;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace RruAntAlarmError
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_RruClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                this.textBox_Rru.Text = openFileDialog.FileName;
            }
            return;
        }

        private void textBox_RruTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_AntWeightClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                this.textBox_AntWeight.Text = openFileDialog.FileName;
            }
            return;
        }

        private void textBox_AntWeightTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_AlarmClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                this.textBox_Alarm.Text = openFileDialog.FileName;
            }
            return;
        }

        private void textBox_AlarmTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_ErrorCodeClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                this.textBox_ErrorCode.Text = openFileDialog.FileName;
            }
            return;
        }

        private void textBox_ErrorCodeTextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void button_ClickOK(object sender, RoutedEventArgs e)
        {
            bool parseResult = true;
            string outputPath = @".\output";
            //创建output目录
            if(false == Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            //解析rru信息表
            if (0 != string.Compare("", this.textBox_Rru.Text))
            {
                Log.Info("======begin to parse " + this.textBox_Rru.Text);
                RruInfoParser rruInfoParser = new RruInfoParser();
                if(false == rruInfoParser.parseRruInfoToJsonFile("RRU基本信息表", true, this.textBox_Rru.Text))
                {
                    parseResult = false;
                    Log.Error("=====parse Rru " + this.textBox_Rru.Text + " fail");
                    MessageBox.Show(this.textBox_Rru.Text + " parse fail");
                    return;
                }
            }
            if (0 != string.Compare("", this.textBox_AntWeight.Text))
            {
                Log.Info("======begin to parse AntWeight " + this.textBox_AntWeight.Text);
                AntennaInfoParser antennaInfoParser = new AntennaInfoParser();
                if (false == antennaInfoParser.parseAntennaInfoToJsonFile(true, this.textBox_AntWeight.Text))
                {
                    parseResult = false;
                    Log.Error("=====parse AntWeight " + this.textBox_AntWeight.Text + " fail");
                    MessageBox.Show(this.textBox_AntWeight.Text + " parse fail");
                    return;
                }
            }
            if (0 != string.Compare("", this.textBox_Alarm.Text))
            {
                Log.Info("======begin to parse alarm " + this.textBox_ErrorCode.Text);
                AlarmInfoParser alarmInfoParser = new AlarmInfoParser();
                if (false == alarmInfoParser.parseAlarmInfoToJsonFile("eNB告警信息表", true, this.textBox_Alarm.Text))
                {
                    parseResult = false;
                    Log.Error("=====parse Alarm " + this.textBox_Rru.Text + " fail");
                    MessageBox.Show(this.textBox_Rru.Text + " parse fail");
                    return;
                }

            }
            if (0 != string.Compare("", this.textBox_ErrorCode.Text))
            {
                Log.Info("======begin to parse ErrorCode " + this.textBox_ErrorCode.Text);
                ErrorCodeParser errorCodeParser = new ErrorCodeParser();
                if (false == errorCodeParser.parseErrorCodeToJsonFile("错误代码", true, this.textBox_ErrorCode.Text))
                {
                    parseResult = false;
                    Log.Error("=====parse ErrorCode " + this.textBox_ErrorCode.Text + " fail");
                    MessageBox.Show(this.textBox_ErrorCode.Text + " parse fail");
                    return;
                }
            }
            if (0 != string.Compare("", this.textBox_NetPlanElements.Text))
            {
                Log.Info("======begin to parse NetPlanElements " + this.textBox_Rru.Text);
                NetPlanElementsParser elementsParser = new NetPlanElementsParser();
                if (false == elementsParser.parseNetPlanElementToJsonFile(true, this.textBox_NetPlanElements.Text))
                {
                    parseResult = false;
                    Log.Error("=====parse NetPlanElements" + this.textBox_Rru.Text + " fail");
                    MessageBox.Show(this.textBox_Rru.Text + " parse fail");
                    return;
                }
            }
            
            if (parseResult)
            {
                MessageBox.Show("恭喜你，解析成功，耶~~~~~~~");
                Log.Info("=====恭喜你，解析成功，耶~~~~~~~");
                this.Close();
            }
        }

        private void button_ClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox_NetPlanElements_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_NetPlanElementsClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel Files(*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                this.textBox_NetPlanElements.Text = openFileDialog.FileName;
            }
            return;
        }
    }
}
