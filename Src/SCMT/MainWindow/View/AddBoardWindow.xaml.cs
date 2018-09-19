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
using SCMTMainWindow.View.Document;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// AddBoardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddBoardWindow : Window
    {
        public bool isOk = false;
        public BoradDetailData detaiData = new BoradDetailData();

        public AddBoardWindow()
        {
            InitializeComponent();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            isOk = true;

            detaiData.SelectedBroadType = BoardType.SelectedValue.ToString();
            detaiData.Frame = 0;
            detaiData.Panel = 0;
            detaiData.SelectedSlotNum = int.Parse(textBox2.Text);
            detaiData.Bt = (BroadType)BoardType.SelectedIndex;
            detaiData.Wm = (WorkMode)workMode.SelectedIndex;
            detaiData.Fsm = (FrameStruMode)fsm.SelectedIndex;
            Close();
        }

        public void SetOperationData(BoradDetailData bd)
        {
            this.DataContext = null;
            this.DataContext = bd;
            //this.workMode.DataContext = bd.Wm;
            this.workMode.ItemsSource = System.Enum.GetValues(typeof(WorkMode));
            this.BoardType.ItemsSource = System.Enum.GetValues(typeof(BroadType));
            this.fsm.ItemsSource = System.Enum.GetValues(typeof(FrameStruMode));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}