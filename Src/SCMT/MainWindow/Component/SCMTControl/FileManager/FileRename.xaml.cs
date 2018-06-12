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

namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
    /// <summary>
    /// FileRename.xaml 的交互逻辑
    /// </summary>
    public partial class FileRename : Window
    {
        public string strNewName;
        public bool bOK = false;
        public FileRename(string strOldName, string strExtension)
        {
            InitializeComponent();

            Width = 300;
            Height = 150;

            this.tbNewName.Text = strOldName;
            this.tbNewName.Focus();

            //如果不存在后缀名，则全选，否则只选中  文件名
            if (strExtension == null)
            {
                this.tbNewName.Select(0, strOldName.Length);
            }
            else
            {
                this.tbNewName.Select(0, strOldName.LastIndexOf(strExtension));
            }
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            strNewName = tbNewName.Text;
            bOK = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            bOK = false;
            this.Close();
        }
    }
}
