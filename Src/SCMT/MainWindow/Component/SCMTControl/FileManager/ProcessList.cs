using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
    /// <summary>
    /// 显示在底部日志信息 list 中的  信息类，用于和  list  绑定
    /// </summary>
    public class ProcessList : INotifyPropertyChanged
    {
        //属性改变事件
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string strPropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }

        string strFileName;
        long m_lTaskID;
        long nProgressValue;
        string strTextBloxkValue;
        string  _FileState;
        string _OperateType;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }

        public long TaskID
        {
            get { return m_lTaskID; }
            set { m_lTaskID = value; }
        }

        /// <summary>
        /// 进度条文字显示
        /// </summary>
        public string TextBloxkValue
        {
            set
            {
                this.strTextBloxkValue = value;
                RaisePropertyChanged("TextBloxkValue");
            }
            get { return strTextBloxkValue; }

        }

        /// <summary>
        /// 传输进度
        /// </summary>
        public long ProgressValue
        {
            set
            {
               this.nProgressValue = value;
                RaisePropertyChanged("ProgressValue");
            }
            get { return nProgressValue; }
        }

        /// <summary>
        /// 文件状态
        /// </summary>
        public string FileState
        {
            set
            {
                _FileState = value;
                RaisePropertyChanged("FileState");
            }
            get { return _FileState; }
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperateType
        {
            set {
                _OperateType = value;
                RaisePropertyChanged("OperateType");
            }
            get { return _OperateType; }
        }

        public enum PROGRESSLISTHEAD
        {
            //进度条表头
            //PROGRESSLISTHEAD_NENAME = 0,		//网元信息
            PROGRESSLISTHEAD_FILENAME = 0,          //文件信息
            PROGRESSLISTHEAD_FINISHEDPERCENT,   //操作完成百分比
            PROGRESSLISTHEAD_STATE,             //状态
            PROGRESSLISTHEAD_OPERATIONTYPE      //操作类型
        }

        public enum OPERTYPE
        {
            //操作类型
            OPERTYPE_UNKNOWN = PROGRESSLISTHEAD.PROGRESSLISTHEAD_OPERATIONTYPE + 1,
            OPERTYPE_DOWNLOAD,
            OPERTYPE_UPLOAD,
            OPERTYPE_UNZIP,
            OPERTYPE_ACTIVE,
            OPERTYPE_SYN,
            OPERTYPE_FINISHED
        }

        public enum FILETRANSSTATE
        {
            TRANSSTATE_UNKNOWN = OPERTYPE.OPERTYPE_FINISHED + 1,
            //上传
            TRANSSTATE_UPLOADWAITING,       //文件上传等待中
            TRANSSTATE_UPLOADING,           //上传正在进行
            TRANSSTATE_UPLOADFINISHED,      //上传已完成
            TRANSSTATE_UPLOADFAILED,        //上传失败
                                            //下载
            TRANSSTATE_DOWNLOADWAITING,     //文件下载等待中
            TRANSSTATE_DOWNLOADING,         //下载正在进行
            TRANSSTATE_DOWNLOADFINISHED,    //下载已完成
            TRANSSTATE_DOWNLOADFAILED,      //下载失败
                                            //解压
            TRANSSTATE_UPZIPING,            //解压正在进行
            TRANSSTATE_UPZIPFINISHED,       //解压已完成
            TRANSSTATE_UPZIPFAILED,         //解压失败
                                            //激活
            TRANSSTATE_ACTIVEING,           //激活正在进行
            TRANSSTATE_ACTIVEFINISHED,      //激活已完成
            TRANSSTATE_ACTIVEFAILED,        //激活失败
                                            //同步
            OPERTYPE_SYNING,
            OPERTYPE_SYNFINISHED,
            OPERTYPE_SYNFAILED,

            //任务状态
            TRANSSTATE_TASKSENDFAILED,      //任务下发失败
            TRANSSTATE_TASKDELETESUCCESSED, //任务删除成功
            TRANSSTATE_TASKDELETEFAILED,    //任务删除失败
                                            //异常
            TRANSSTATE_DISCONNECT           //网元断开连接
        }
    }

    /// <summary>
    /// 自定义的文件信息类，用于绑定到ListViewItem中
    /// </summary>
    public class FileInfoDemo
    {
        /// <summary>
        /// 获取文件的图标信息
        /// </summary>
        public ImageSource ImgSource { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public string LastModifyTime { get; set; }

        /// <summary>
        /// 文件所在路径
        /// </summary>
        public string FilePath { get; set; }
    }

    /// <summary>
    /// 自定义的文件操作的类，主要获取文件信息，弹出文件属性
    /// </summary>
    public class FileInfoGet
    {
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

        /// <summary>
        /// 自定义函数，获取文件的图标，可以指定大小图标，或者文件夹图标
        /// </summary>
        /// <param name="strFilePath">文件名</param>
        /// <param name="bSmallOrLarge">true 小图标  false 大图标</param>
        /// <param name="bDirectory">true  文件夹  false 文件</param>
        /// <returns></returns>
        public static ImageSource GetIcon(string strFilePath, bool bSmallOrLarge, bool bDirectory)
        {
            uint uFlag = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;
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

        /********下面是弹出文件对话框的相关函数*****************************/

        /// <summary>
        /// 定义结构体  ShellExecuteInfo  用于显示文件属性对话框
        /// 微软的定义是 contains information used by ShellExecuteEx  包含函数 ShellExecuteEx 执行所需要的信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;               // in, required, sizeof of this structure  该结构体的大小
            public uint fMask;                // in, SEE_MASK_XXX values  指出结构体其他成员的内容或有效性的标志，可用的值有22个，具体参考MSDN
            public IntPtr hwnd;                  // in, optional
            public string lpVerb;            // in, optional when unspecified the default verb is choosen
            public string lpFile;            // in, either this value or lpIDList must be specified
            public string lpParameters;      // in, optional
            public string lpDirectory;       // in, optional
            public int nShow;                  // in, required
            public IntPtr hInstApp;         // out when SEE_MASK_NOCLOSEPROCESS is specified
            public IntPtr lpIDList;             // in, valid when SEE_MASK_IDLIST is specified, PCIDLIST_ABSOLUTE, for use with SEE_MASK_IDLIST & SEE_MASK_INVOKEIDLIST
            public string lpClass;           // in, valid when SEE_MASK_CLASSNAME is specified
            public IntPtr hkeyClass;             // in, valid when SEE_MASK_CLASSKEY is specified
            public uint dwHotKey;             // in, valid when SEE_MASK_HOTKEY is specified

            public IntPtr hIcon;           // not used
            public IntPtr hProcess;            // out, valid when SEE_MASK_NOCLOSEPROCESS specified
        }

        public const int SW_SHOW = 5;

        /// <summary>
        /// 微软的定义  use the IContextMenu interface of the selected item's shortcut menu handler
        /// 我的拙劣的翻译：使用  被选中子项的  快捷菜单句柄  的 IContextMenu 接口  
        /// </summary>
        public const uint SW_MASK_INVOKEIDLIST = 12;

        /// <summary>
        /// 使用win32 程序，查看文件属性
        /// </summary>
        /// <param name="lpExecInfo"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);


    }

    /// <summary>
    /// 自定义的文件信息类，用于绑定到ListViewItem中
    /// </summary>
    public class FileInfoEnb
    {
        /// <summary>
        /// 获取文件的图标信息
        /// </summary>
        ImageSource imgSource;
        public ImageSource ImgSource {
            get {
                if(isDirectory)
                {
                    imgSource = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/CLOSED.BMP"));
                    return imgSource;
                }
                else
                {
                    imgSource = new BitmapImage(new Uri("pack://application:,,/component/SCMTControl/FileManager/img/file.bmp"));
                    return imgSource;
                }
            }
            set {
                imgSource = value;
            }
        }

        /// <summary>
        /// 是否是文件夹，根据类型不同选择不同的图标显示
        /// </summary>
        bool isDirectory;
        public bool IsDirectory
        {
            get { return isDirectory; }
            set { isDirectory = value; }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileLittleVer { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public string LastModifyTime { get; set; }

        // 读写属性
        public string RWAttr { get; set; }

        /// <summary>
        /// 文件所在路径
        /// </summary>
        public string FilePath { get; set; }
    }
}
