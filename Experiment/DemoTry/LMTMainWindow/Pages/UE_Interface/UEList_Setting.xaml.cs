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
using Arthas.Controls.Metro;

namespace SCMTMainWindow
{
    public delegate void PassValuesHandler(object sender, GraphSelectorPassValEventArg e);
    /// <summary>
    /// UEList_Setting.xaml 的交互逻辑
    /// </summary>
    public partial class UEList_Setting : MetroWindow
    {

        public event PassValuesHandler m_PassToMain;

        public UEList_Setting()
        {
            InitializeComponent();
        }
        
        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("1111111111");
        }

        private void Button2_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("2222222222");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = new Button();
            button.BorderThickness = new Thickness(0);
            button.Background = new ImageBrush(new BitmapImage(new Uri(System.Environment.CurrentDirectory + @"..\..\..\Resources\A_Move.png")));
            button.Height = 50;
            button.Width = 50;
            GridRow2.Children.Add(button);
            Grid.SetColumn(button, 1);
            Grid.SetColumn(Addbutton, 2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MetroMenuTabItem abc = new MetroMenuTabItem();
            abc.Header = TabName.Text;
            abc.Height = 40;
            abc.Icon = new BitmapImage(new Uri(System.Environment.CurrentDirectory + @"..\..\..\Resources\A.png"));
            abc.IconMove = new BitmapImage(new Uri(System.Environment.CurrentDirectory + @"..\..\..\Resources\A_Move.png"));
            abc.VerticalAlignment = VerticalAlignment.Top;

            Frame newframe = new Frame();
            

            abc.Content = newframe;
            

            GraphSelectorPassValEventArg arg = new GraphSelectorPassValEventArg(abc);
            m_PassToMain(this, arg);

            this.Close();
        }
    }

    public class GraphSelectorPassValEventArg : EventArgs
    {
        public MetroMenuTabItem m_tab { get; set; }

        public GraphSelectorPassValEventArg(MetroMenuTabItem tab)
        {
            m_tab = tab;
        }
    }
}
