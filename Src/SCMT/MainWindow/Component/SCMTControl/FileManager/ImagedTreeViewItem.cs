using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using FileManager;
using CommonUtility;
using LogManager;
using SCMTOperationCore.Message.SI;
using System.Text;
using System.Runtime.InteropServices;

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
        ImageSource srcImgIcon;

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
        
        public ImageSource ImgIcon
        {
            get
            {
                return srcImgIcon;
            }
            set
            {
                srcImgIcon = value;
                img.Source = srcImgIcon;
            }
        }        
    }
    
    /// <summary>
     /// 基站侧文件显示类
     /// 内置一个StackPanel，包括一个TextBlock显示文本，图片
     /// </summary>
    public class ImagedTreeViewItemENB : TreeViewItem
    {
        TextBlock text;
        Image img;
        ImageSource srcSelected, srcUnselected, srcImgIcon;

        /// <summary>
        /// Constructor makes stack with image and text
        /// </summary>
        public ImagedTreeViewItemENB()
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

                if (IsSelected)
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
                if (!IsSelected)
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

            //SelectedImage = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/OPEN.BMP"));
            //UnselectedImage = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/CLOSED.BMP"));
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
                DirectoryTreeViewItem newItem = new DirectoryTreeViewItem(dirChild);
                newItem.ImgIcon = DirectoryTreeView.GetIcon(dirChild.FullName, true, true);
                Items.Add(newItem);
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
                if (chDrive != 'A' && chDrive != 'B' )
                {
                    uint uFlag = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES | SHGFI_DISPLAYNAME | SHGFI_SMALLICON | SHGFI_SYSICONINDEX;

                    uint uAttribute = FILE_ATTRIBUTE_NORMAL;

                    SHFILEINFO fileInfo = new SHFILEINFO();

                    if (0 != SHGetFileInfo(drive.Name, uAttribute, ref fileInfo, (uint)Marshal.SizeOf(typeof(SHFILEINFO)), uFlag))
                    {
                        item.Text = fileInfo.szDisplayName;
                        item.ImgIcon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(fileInfo.hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                }

                Items.Add(item);

                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady)
                {
                    item.Populate();
                }
            }

            EndInit();
        }

        /// <summary>
        /// 获取文件图标需要的结构体，作为出参，不需要初始化
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;                                        //文件的图标句柄
            public int iIcon;                                              //文件图标的系统索引号
            public uint dwAttributes;                                //文件的属性值
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;                           //文件的显示名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;                               //文件的类型名
        }

        /// <summary>
        /// 使用win32程序，查看文件信息，主要是获取图标，包括文件图标，文件夹图标，驱动器图标
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="dwFileAttributes">文件属性，一般区分文件和文件夹</param>
        /// <param name="lpFileInfo">出参，保存图标等信息的结构体</param>
        /// <param name="cbFileInfoSize">结构体大小</param>
        /// <param name="uFlags">核心变量，通过不同的标志获取不同的信息</param>
        /// <returns></returns>
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHGetFileInfo(string strFilePath, uint dwFileAttributes, ref SHFILEINFO lpFileInfo, uint cbFileInfoSize, uint uFlags);

        //获取图标
        private const uint SHGFI_ICON = 0x100;

        //大图标 32 x 32
        private const uint SHGFI_LARGEICON = 0x0;

        //小图标 16 x 16
        private const uint SHGFI_SMALLICON = 0x1;

        //使用use passed dwFileAttribute
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const uint SHGFI_DISPLAYNAME = 0x200;
        private const uint SHGFI_SYSICONINDEX = 0x400;

        /// <summary>
        /// 自定义函数，获取文件的图标，可以指定大小图标，或者文件夹图标
        /// </summary>
        /// <param name="strFilePath">文件名</param>
        /// <param name="bSmallOrLarge">true 小图标  false 大图标</param>
        /// <param name="bDirectory">true  文件夹  false 文件</param>
        /// <returns></returns>
        public static ImageSource GetIcon(string strFilePath, bool bSmallOrLarge, bool bDirectory)
        {
            uint uFlag = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES | SHGFI_DISPLAYNAME;
            if (bSmallOrLarge)
                uFlag |= SHGFI_SMALLICON;

            uint uAttribute = FILE_ATTRIBUTE_NORMAL;
            if (bDirectory)
                uAttribute |= FILE_ATTRIBUTE_DIRECTORY;

            SHFILEINFO fileInfo = new SHFILEINFO();

            if (0 != SHGetFileInfo(strFilePath, uAttribute, ref fileInfo, (uint)Marshal.SizeOf(typeof(SHFILEINFO)), uFlag))
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(fileInfo.hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }

            return null;
        }

    }


    /// <summary>
    /// 基站文件夹列表item，继承自ImagedTreeViewItem
    /// </summary>
    public class enbDirectoryTreeViewItem : ImagedTreeViewItemENB
    {
        string dir;

        //Constructor requires SI_STRU_FileInfo object
        public enbDirectoryTreeViewItem(string pathName, string parentPath)
        {
            if(string.Empty == parentPath)
            {
                this.dir = pathName;
            }
            else
            {
                this.dir = parentPath + "/" + pathName;
            }

            Text = pathName;

            SelectedImage = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/OPEN.BMP"));
            UnselectedImage = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/CLOSED.BMP"));
        }

        /// <summary>
        /// public property to obtain DirectoryInfo
        /// </summary>
        public string DirInfo
        {
            get { return dir; }
        }
    }
}
