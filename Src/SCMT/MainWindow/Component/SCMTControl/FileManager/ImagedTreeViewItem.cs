using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
    /// <summary>
    /// 简单的Item类，选中和不被选中用两种图标表示
    /// 内置一个StackPanel，包括一个TextBlock显示文本，图片
    /// </summary>
    public class ImagedTreeViewItem : TreeViewItem
    {
        TextBlock text;
        Image img;
        ImageSource srcSelected, srcUnselected;

        /// <summary>
        /// Constructor makes stack with image and text
        /// </summary>
        public ImagedTreeViewItem()
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Header = stack;

            img = new Image();
            img.VerticalAlignment = VerticalAlignment.Center;
            img.Margin = new Thickness(0, 0, 2, 0);
            stack.Children.Add(img);

            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        /// <summary>
        /// Public porperty for text and images
        /// </summary>
        public string Text
        {
            get { return text.Text; }
            set { text.Text = value; }
        }

        public ImageSource SelectedImage
        {
            get { return srcSelected; }
            set
            {
                srcSelected = value;

                if(IsSelected)
                {
                    img.Source = srcSelected;
                }
            }//end of set
        }//end of public Imagesource SelectedItem

        public ImageSource UnselectedImage
        {
            get { return srcUnselected; }
            set
            {
                srcUnselected = value;
                if(!IsSelected)
                {
                    img.Source = srcUnselected;
                }
            }//end of set
        }//end of public ImageSource UnselectedImage

        /// <summary>
        /// Event override to set image
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            img.Source = srcSelected;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            img.Source = srcUnselected;
        }
    }

    /// <summary>
    /// 文件夹列表item，继承自ImagedTreeViewItem
    /// </summary>
    public class DirectoryTreeViewItem : ImagedTreeViewItem
    {
        DirectoryInfo dir;

        //Constructor requires DirectoryInfo object
        public DirectoryTreeViewItem(DirectoryInfo pDir)
        {
            this.dir = pDir;
            Text = pDir.Name;

            SelectedImage = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/OPEN.BMP"));
            UnselectedImage = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/CLOSED.BMP"));
        }

        /// <summary>
        /// public property to obtain DirectoryInfo
        /// </summary>
        public DirectoryInfo DirInfo
        {
            get { return dir; }
        }

        /// <summary>
        /// public mathod to populate wtih items
        /// </summary>
        public void Populate()
        {
            DirectoryInfo[] dirs;

            try
            {
                dirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach (DirectoryInfo dirChild in dirs)
            {
                Items.Add(new DirectoryTreeViewItem(dirChild));
            }
        }

        /// <summary>
        /// event override to populate subitem
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExpanded(RoutedEventArgs e)
        {
            base.OnExpanded(e);

            foreach (object obj in Items)
            {
                DirectoryTreeViewItem item = obj as DirectoryTreeViewItem;
                item.Populate();
            }
        }
    }


    public class DirectoryTreeView : TreeView
    {
        /// <summary>
        /// Constructor builds
        /// </summary>
        public DirectoryTreeView()
        {
            RefreshTree();
        }

        public void RefreshTree()
        {
            BeginInit();
            Items.Clear();

            //Obtain the disk drivers
            DriveInfo[] drivers = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drivers)
            {
                char chDrive = drive.Name.ToUpper()[0];
                DirectoryTreeViewItem item = new DirectoryTreeViewItem(drive.RootDirectory);

                //display ...
                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady && drive.VolumeLabel.Length > 0)
                {
                    item.Text = string.Format("{0}  ({1})", drive.VolumeLabel, drive.Name);
                }
                else
                {
                    item.Text = string.Format("{0}  ({1})", drive.DriveType, drive.Name);
                }

                Items.Add(item);

                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady)
                {
                    item.Populate();
                }
            }

            EndInit();
        }
    }
}
