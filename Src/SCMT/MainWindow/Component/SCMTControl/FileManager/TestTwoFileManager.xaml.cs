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
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Threading;
using FileManager;
using CommonUtility;
using LogManager;
using SCMTOperationCore.Message.SI;


namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
    /// <summary>
    /// TestTwoFileManager.xaml 的交互逻辑
    /// </summary>
    public partial class TestTwoFileManager : UserControl
    {
        //全局变量
        private ListView lvMainListView = new ListView();
        private ProcessList myList = new ProcessList();

        /// <summary>
        /// 当前被选中的文件夹， 全局变量
        /// </summary>
        private DirectoryTreeViewItem localSelectedDTVI;

        /// <summary>
        /// 基站文件夹中被选中的项
        /// </summary>
        private enbDirectoryTreeViewItem enbSelectedItem;

        //定义文件夹树，显示基站文件夹信息，因为基站需要使用回调函数显示，所以设置为全局变量
        private TreeView enbMainTree = new TreeView();

        // 保存最后一次点击的右键菜单名称
        private string latestEnbMenuName;


        //右键菜单添加
        ContextMenu myContext = new ContextMenu();


        public TestTwoFileManager(string strIP)
        {
            InitializeComponent();
            //添加  ListView   显示文件传输进度
            InitListView();

            //初始化本地文件管理模块
            InitLocalFileManager();

            //初始化基站文件管理模块
            InitEnbFileManager();
        }


        /// <summary>
        /// 初始化  ListView    
        /// </summary>
        private void InitListView()
        {
            //首先添加到  主界面  Grid  中
            MainGrid.Children.Add(lvMainListView);
            Grid.SetRow(lvMainListView, 2);

            //添加  字段名称
            GridView gvListView = new GridView();
            lvMainListView.View = gvListView;

            GridViewColumn gvcColumn = new GridViewColumn();
            gvcColumn.Header = "文件名称";
            gvcColumn.Width = 100;
            gvcColumn.DisplayMemberBinding = new Binding("FileName");
            gvListView.Columns.Add(gvcColumn);

            gvcColumn = new GridViewColumn();
            gvcColumn.Header = "进度";
            gvcColumn.Width = 220;

            DataTemplate template = new DataTemplate();

            //进度条显示文字，目前想到的方案是，定义Grid，添加TextBlock显示，和进度条组合起来
            FrameworkElementFactory gridTxtAndProcess = new FrameworkElementFactory(typeof(Grid));
            FrameworkElementFactory processText = new FrameworkElementFactory(typeof(TextBlock));
            FrameworkElementFactory fileProgress = new FrameworkElementFactory(typeof(ProgressBar));
            //ProgressBar fileProgress = new ProgressBar();
            fileProgress.SetValue(ProgressBar.MaximumProperty, 100.0);
            fileProgress.SetValue(ProgressBar.WidthProperty, 200.0);
            fileProgress.SetValue(ProgressBar.HeightProperty, 15.0);
            fileProgress.SetBinding(ProgressBar.ValueProperty, new Binding("ProgressValue"));

            //设置TextBlock信息
            processText.SetValue(TextBlock.WidthProperty, 33.0);    //显示百分比够了
            processText.SetValue(TextBlock.HeightProperty, 15.0);     //大概是进度的宽度
            processText.SetBinding(TextBlock.TextProperty, new Binding("TextBloxkValue"));

            //Grid添加子项，包含TextBlock和Processbar
            gridTxtAndProcess.AppendChild(fileProgress);
            gridTxtAndProcess.AppendChild(processText);

            template.VisualTree = gridTxtAndProcess;
            gvcColumn.CellTemplate = template;
            gvListView.Columns.Add(gvcColumn);

            gvcColumn = new GridViewColumn();
            gvcColumn.Header = "文件状态";
            gvcColumn.Width = 100;
            gvcColumn.DisplayMemberBinding = new Binding("FileState");
            gvListView.Columns.Add(gvcColumn);

            gvcColumn = new GridViewColumn();
            gvcColumn.Header = "操作类型";
            gvcColumn.Width = 100;
            gvcColumn.DisplayMemberBinding = new Binding("OperateType");
            gvListView.Columns.Add(gvcColumn);
        }


        /// <summary>
        /// 初始化本地文件管理模块
        /// </summary>
        private void InitLocalFileManager()
        {
            //定义文件夹树
            DirectoryTreeView localMainTree = new DirectoryTreeView();
            localMainTree.SelectedItemChanged += LocalMainTree_SelectedItemChanged;
            fmLocal.Children.Add(localMainTree);
            Grid.SetColumn(localMainTree, 0);

            //分隔条
            GridSplitter splite = new GridSplitter();
            splite.Width = 2;
            splite.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            fmLocal.Children.Add(splite);
            Grid.SetColumn(splite, 1);

            //定义字段，绑定到文件信息类中
            GridView myview = new GridView();
            lvLocalFileInfo.View = myview;

            GridViewColumn mycolun = new GridViewColumn();
            mycolun.Header = "文件名";
            mycolun.Width = 120;

            //文件名是带图标的文件名，需要使用模板
            DataTemplate template = new DataTemplate();

            //模板是一个  包含  Image  控件  和  TextBlock 控件  的 StackPanel
            FrameworkElementFactory fileStackPanel = new FrameworkElementFactory(typeof(StackPanel));

            //定义图标，并绑定
            FrameworkElementFactory fileIcon = new FrameworkElementFactory(typeof(Image));
            fileIcon.SetValue(Image.WidthProperty, 16.0);
            fileIcon.SetValue(Image.HeightProperty, 16.0);
            fileIcon.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            fileIcon.SetValue(Image.MarginProperty, new Thickness(0, 0, 2, 0));
            fileIcon.SetBinding(Image.SourceProperty, new Binding("ImgSource"));

            //定义文件名并绑定
            FrameworkElementFactory fileText = new FrameworkElementFactory(typeof(TextBlock));
            fileText.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            fileText.SetBinding(TextBlock.TextProperty, new Binding("FileName"));
            fileStackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            //设置  stackpanel
            fileStackPanel.AppendChild(fileIcon);
            fileStackPanel.AppendChild(fileText);
            template.VisualTree = fileStackPanel;

            //该列的模板就是刚刚定义的stackpanel
            mycolun.CellTemplate = template;
            myview.Columns.Add(mycolun);

            mycolun = new GridViewColumn();
            mycolun.Header = "最后修改时间";
            mycolun.Width = 100;
            mycolun.DisplayMemberBinding = new Binding("LastModifyTime");
            myview.Columns.Add(mycolun);

            mycolun = new GridViewColumn();
            mycolun.Header = "大小";
            mycolun.Width = 80;
            mycolun.DisplayMemberBinding = new Binding("Size");
            myview.Columns.Add(mycolun);

            mycolun = new GridViewColumn();
            mycolun.Header = "类型";
            mycolun.Width = 50;
            mycolun.DisplayMemberBinding = new Binding("FileType");
            myview.Columns.Add(mycolun);


            MenuItem myMUItem = new MenuItem();
            myMUItem.Header = "下载至基站";
            myMUItem.Name = "Menu01";
            myMUItem.Click += downloadFileToBoard_Click;
            myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "查看";
            myMUItem.Name = "FileLook";
            myMUItem.Click += localFileLook_Click;
            myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "刷新";
            myMUItem.Name = "Refresh";
            myMUItem.Click += localRefresh_Click;
            myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "重命名";
            myMUItem.Name = "Rename";
            myMUItem.Click += localRename_Click;
            myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "新建文件夹";
            myMUItem.Name = "NewFolder";
            myMUItem.Click += localNewFolder_Click;
            myContext.Items.Add(myMUItem);

        //    lvLocalFileInfo.ContextMenu = myContext;

            //设置文件列表的  鼠标拖拽事件
            lvLocalFileInfo.AllowDrop = true;
            lvLocalFileInfo.Drop += LvLocalFileInfo_Drop; ;

            //设置文件 list 的 移动事件
            lvLocalFileInfo.QueryContinueDrag += LvLocalFileInfo_QueryContinueDrag;
            lvLocalFileInfo.MouseLeftButtonDown += LvLocalFileInfo_MouseLeftButtonDown;
            lvLocalFileInfo.PreviewMouseRightButtonDown += LvENBFileInfo_PreviewMouseRightButtonDown;            

            //鼠标双击事件
            lvLocalFileInfo.MouseDoubleClick += LvLocalFileInfo_MouseDoubleClick;
        }

        /// <summary>
        /// 右键事件，判断当前是否存在选中的ListViewItem，存在则弹出右键菜单，否则，不弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvENBFileInfo_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lvLocalFileInfo == null)
            {
                return;
            }

            if (lvLocalFileInfo.SelectedItems.Count <= 0)
            {
                lvLocalFileInfo.ContextMenu = null;
            }
            else
            {
                lvLocalFileInfo.ContextMenu = myContext;
            }
        }

        /// <summary>
        /// ListView 的鼠标左键点击事件，目的是为了判断当前点击的是空白还是 item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvLocalFileInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lvLocalFileInfo.IsHitTestVisible)
            {
                //MessageBox.Show("OK");
            }

            //根据当前鼠标在 ListView中的位置，获取鼠标下的控件
            IInputElement element = lvLocalFileInfo.InputHitTest(Mouse.GetPosition(lvLocalFileInfo));

            if(element != null  )
            {
                if(element is System.Windows.Controls.ScrollViewer)
                {
                    lvLocalFileInfo.SelectedItems.Clear();
                    return;
                }
            }

        }

        /// <summary>
        /// 查询文件拖放的状态，以决定是否继续拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvLocalFileInfo_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if(!e.KeyStates.HasFlag(DragDropKeyStates.LeftMouseButton))
            {
                e.Action = DragAction.Cancel;
            }
        }

        /// <summary>
        /// 本地文件双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvLocalFileInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (lvLocalFileInfo.SelectedItem != null)
            //{
            //    FileInfoDemo selectedFile = lvLocalFileInfo.SelectedItem as FileInfoDemo;

            //    if (selectedFile.FileType == "文件夹")
            //    {
            //        //MessageBox.Show(localSelectedDTVI.DirInfo.Name);
            //        localSelectedDTVI.IsExpanded = true;
            //        foreach(DirectoryTreeViewItem dirInfo in localSelectedDTVI.Items)
            //        {
            //            if(dirInfo.DirInfo.Name == selectedFile.FileName)
            //            {
            //                //DirectoryTreeViewItem newItem = new DirectoryTreeViewItem(dirInfo);
            //                localSelectedDTVI = dirInfo;
            //                localSelectedDTVI.IsExpanded = true;
            //                localRefreshFileList();
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 初始化基站文件管理模块
        /// </summary>
        private void InitEnbFileManager()
        {
            fmENB.Children.Add(enbMainTree);
            Grid.SetColumn(enbMainTree, 0);
            enbMainTree.SelectedItemChanged += EnbMainTree_SelectedItemChanged;

            //分隔条
            GridSplitter splite = new GridSplitter();
            splite.Width = 2;
            splite.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            fmENB.Children.Add(splite);
            Grid.SetColumn(splite, 1);

            //定义字段，绑定到文件信息类中
            GridView myview = new GridView();
            lvENBFileInfo.View = myview;

            GridViewColumn mycolun = new GridViewColumn();
            mycolun.Header = "文件名";
            mycolun.Width = 120;

            //文件名是带图标的文件名，需要使用模板
            DataTemplate template = new DataTemplate();

            //模板是一个  包含  Image  控件  和  TextBlock 控件  的 StackPanel
            FrameworkElementFactory fileStackPanel = new FrameworkElementFactory(typeof(StackPanel));

            //定义图标，并绑定
            FrameworkElementFactory fileIcon = new FrameworkElementFactory(typeof(Image));
            fileIcon.SetValue(Image.WidthProperty, 16.0);
            fileIcon.SetValue(Image.HeightProperty, 16.0);
            fileIcon.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            fileIcon.SetValue(Image.MarginProperty, new Thickness(0, 0, 2, 0));
            fileIcon.SetBinding(Image.SourceProperty, new Binding("ImgSource"));

            //定义文件名并绑定
            FrameworkElementFactory fileText = new FrameworkElementFactory(typeof(TextBlock));
            fileText.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            fileText.SetBinding(TextBlock.TextProperty, new Binding("FileName"));
            fileStackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            //设置  stackpanel
            fileStackPanel.AppendChild(fileIcon);
            fileStackPanel.AppendChild(fileText);
            template.VisualTree = fileStackPanel;

            //该列的模板就是刚刚定义的stackpanel
            mycolun.CellTemplate = template;
            myview.Columns.Add(mycolun);


            mycolun = new GridViewColumn();
            mycolun.Header = "修改日期";
            mycolun.Width = 120;
            mycolun.DisplayMemberBinding = new Binding("LastModifyTime");
            myview.Columns.Add(mycolun);

            mycolun = new GridViewColumn();
            mycolun.Header = "大小";
            mycolun.Width = 80;
            mycolun.DisplayMemberBinding = new Binding("Size");
            myview.Columns.Add(mycolun);

            mycolun = new GridViewColumn();
            mycolun.Header = "文件小版本";
            mycolun.Width = 120;
            mycolun.DisplayMemberBinding = new Binding("FileLittleVer");
            myview.Columns.Add(mycolun);

            mycolun = new GridViewColumn();
            mycolun.Header = "读写属性";
            mycolun.Width = 80;
            mycolun.DisplayMemberBinding = new Binding("RWAttr");
            myview.Columns.Add(mycolun);

            //右键菜单添加
            ContextMenu myContext = new ContextMenu();

            MenuItem myMUItem = new MenuItem();
            myMUItem.Header = "上传文件至本地";
            myMUItem.Name = "UploadToLocal";
            myMUItem.Click += UploadFileToMgr_Click;
            myContext.Items.Add(myMUItem);

            //myMUItem = new MenuItem();
            //myMUItem.Header = "激活基站软件包";
            //myMUItem.Name = "ActiveEnbSoftwarePackage";
            //myMUItem.Click += ActiveEnvSoft_Click;
            //myContext.Items.Add(myMUItem);

            //myMUItem = new MenuItem();
            //myMUItem.Header = "激活外设软件包";
            //myMUItem.Name = "ActivePeripheralSoftwarePackage";
            //myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "版本查询";
            myMUItem.Name = "SelectVer";

            var subItem = new MenuItem();
            subItem.Header = "基站软件包版本";
            subItem.Name = "QueryEnbSoftVersion";
            subItem.Click += GetEnbSoftVer_Click;
            myMUItem.Items.Add(subItem);

            subItem = new MenuItem();
            subItem.Header = "外设软件包版本";
            subItem.Name = "QueryPerSoftVersion";
            subItem.Click += GetPerSoftVer_Click;
            myMUItem.Items.Add(subItem);

            myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "查询系统容量";
            myMUItem.Name = "SelectSystemCapacity";
            myMUItem.Click += GetCapacity_Click;
            myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "上传快照配置文件";
            myMUItem.Name = "UploadConfigFile";
            myContext.Items.Add(myMUItem);

            myMUItem = new MenuItem();
            myMUItem.Header = "刷新";
            myMUItem.Name = "EnbRefresh";
            myMUItem.Click += enbRefresh;
            myContext.Items.Add(myMUItem);

            lvENBFileInfo.ContextMenu = myContext;

            //初始化和基站的连接
            InitMember();
            InitTargetFileTreeInfo();
        }

        /// <summary>
        /// enb 刷新功能，重新调用一次 fileHandler ，从回调函数刷新界面
        /// </summary>
        private void enbRefreshList()
        {
            //清除文件列表信息，重新加载
            lvENBFileInfo.Items.Clear();

            if (enbSelectedItem != null)
            {
                if (!_fileHandler.GetBoardFileInfo(enbSelectedItem.DirInfo))
                {
                    Log.Error($"获取板卡{_boardIp}路径信息失败");
                    // TODO 前台错误信息提示
                    return;
                }
            }
        }

        /// <summary>
        /// enb右键菜单  刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enbRefresh(object sender, RoutedEventArgs e)
        {
            enbRefreshList();
        }

        // 上传文件到本地功能
        private void UploadFileToMgr_Click(object sender, RoutedEventArgs e)
        {
            // 处理本地的文件路径
            var localDirName = localSelectedDTVI.DirInfo.FullName;

            // 处理基站侧文件路径
            if (null == enbSelectedItem)
            {
                Log.Error("基站侧尚未选中任何目录，应该设置一个默认值");
                return;
            }

            var enbPath = enbSelectedItem.DirInfo;
            var enbFi = lvENBFileInfo.SelectedItem as FileInfoEnb;
            if (null != enbFi)
            {
                enbPath += $"/{enbFi.FileName}";
            }

            var ret = ShowTip_Confirm($"确定上传文件{enbFi.FileName}到管理侧{localDirName}？");
            if (MessageBoxResult.Yes != ret)
            {
                return;
            }

            // 判断是否覆盖已有文件
            var fileFullPath = localDirName + "\\" + enbFi.FileName;
            if (File.Exists(fileFullPath))
            {
                if (MessageBoxResult.Yes != ShowTip_Confirm("是否覆盖已有文件？"))
                {
                    return;
                }
            }

            try
            {
                _fileHandler.UploadFileToLocal(localDirName, enbPath);
            }
            catch (Exception exception)
            {
                ShowTip_Error(exception.Message);
            }
        }

        //enb树的选择改变事件
        private void EnbMainTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            enbSelectedItem = e.NewValue as enbDirectoryTreeViewItem;

            if(enbSelectedItem != null)
            {
                if (!_fileHandler.GetBoardFileInfo(enbSelectedItem.DirInfo))
                {
                    Log.Error($"获取板卡{_boardIp}路径信息{enbSelectedItem.DirInfo}失败");
                    ShowTip_Error($"获取基站{_boardIp}文件信息失败，请检查基站是否已断开连接！");
                }
            }
        }


        #region 私有属性、成员

        private FileMgrFileHandler _fileHandler;
        private string _boardIp;
        private bool _bInRoot = true;

        #endregion
        // 私有成员初始化
        private void InitMember()
        {
            string boardIp = "172.27.245.92";       // TODO 这里需要使用实际的IP地址
            _fileHandler = new FileMgrFileHandler(boardIp);
            _fileHandler.GetFileInfoRspArrived += GetFileInfoCallBack;
            _fileHandler.UpdateProgressEvent += UpdateProgressCallBack;
            _fileHandler.NewProgressEvent += NewProgressCallBack;
            _fileHandler.EndProgressEvent += EndProgressCallBack;
            _fileHandler.MenuClickRspEvent += MenuClickCallBack;

            _boardIp = boardIp;
        }

        // 调用接口，查询板卡上的目录信息，并填入到控件中
        private void InitTargetFileTreeInfo()
        {
            if (!_fileHandler.GetBoardFileInfo(""))
            {
                Log.Error($"获取板卡{_boardIp}路径信息失败");
                ShowTip_Error($"获取基站{_boardIp}文件信息失败，请检查基站是否已断开连接！");
            }

            // 等待si消息回复
        }

        private void GetFileInfoCallBack(byte[] rspBytes)
        {
          //  MessageBox.Show("Here GetFileInfoCallBack");
            var rsp = new SI_SILMTENB_GetFileInfoRspMsg();
            if (-1 == rsp.DeserializeToStruct(rspBytes, 0))
            {
                Log.Error("查询文件信息结果转换失败");
                return;
            }

            if (1 == rsp.s8GetResult)   // 获取结果： 0：成功 1：失败
            {
                Log.Error("基站上报数据失败");
                return;
            }

            if (_bInRoot)
            {
                var strTest = Encoding.Default.GetString(rsp.s8SrcPath).Replace("\0", "");
                InitEnbTreeView(strTest, _boardIp);
                _bInRoot = false;
            }

            var fileNumber = rsp.u16FileNum;
            var fileVersion  = fileNumber & 0x00FF;
            var fileCount = (fileNumber & 0xFF00) >> 8;
            Log.Debug($"文件数量：{fileCount}，文件版本：{fileVersion}");

            if (fileVersion == 0)
            {
                ShowFileInfoToView(rsp);
            }
            else if (fileVersion == 1)
            {
                var rspv2 = new SI_SILMTENB_GetFileInfoRspMsg_v2();
                if (-1 == rspv2.DeserializeToStruct(rspBytes, 0))
                {
                    Log.Error("查询文件信息结果转换失败");
                    return;
                }

                ShowFileInfoToView(rspv2);
            }
        }

        // TODO 把文件数据写入到控件，分为两个版本
        private void ShowFileInfoToView(SI_SILMTENB_GetFileInfoRspMsg rsp)
        {
            if (rsp != null)
            {
                for (int i = 0; i < rsp.u16FileNum; i++)
                {
                    var strTest = Encoding.Default.GetString(rsp.struFileInfo[i].s8FileName).Replace("\0", "");
                 //   MessageBox.Show(strTest);
                }
            }

        }
        
        private void ShowFileInfoToView(SI_SILMTENB_GetFileInfoRspMsg_v2 rsp)
        {
            //每次获取文件信息之前，清空Listview
            lvENBFileInfo.Dispatcher.BeginInvoke(new Action(() =>
            {
                lvENBFileInfo.Items.Clear();
            }));
            if (rsp != null)
            {
                //获取父路径
                var strFather = Encoding.Default.GetString(rsp.s8SrcPath).Replace("\0", "").Replace("//", "/");

                //如果当前选中的 items  再次被点击，则需要先进行清理
                enbSelectedItem.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (enbSelectedItem.Items.Count != 0)
                    {
                        enbSelectedItem.Items.Clear();
                    }
                }));
                
                for (int i = 0; i < rsp.u8FileCount; i++)
                {
                    var strTest = Encoding.Default.GetString(rsp.struFileInfo[i].s8FileName).Replace("\0", "").Replace("//", "/");

                    //根据<>判断是否是文件夹，否则是文件
                    if ((strTest[0] == '<') && (strTest[strTest.Length-1] == '>'))
                    {
                        //去掉<>，获取文件夹名称
                        var strCurrent = strTest.Substring(1, strTest.Length - 2);

                        //在被选中的item中添加子项
                        enbSelectedItem.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            enbDirectoryTreeViewItem newItem = new enbDirectoryTreeViewItem(strCurrent, strFather);
                            enbSelectedItem.Items.Add(newItem);

                        }));

                        //添加文件夹信息到ListView中
                        FileInfoEnb newEnbFileInfo = new FileInfoEnb();

                        newEnbFileInfo.FileName = strCurrent;
                        newEnbFileInfo.IsDirectory = true;
                        newEnbFileInfo.Size = null;
//                        string strTime = rsp.struFileInfo[i].struFileTime.dosdt_year.ToString() + "/" + rsp.struFileInfo[i].struFileTime.dosdt_month.ToString()
//                            + rsp.struFileInfo[i].struFileTime.dosdt_day.ToString() + " " + rsp.struFileInfo[i].struFileTime.dosdt_hour.ToString() + ":" + rsp.struFileInfo[i].struFileTime.dosdt_minute.ToString();
                        newEnbFileInfo.LastModifyTime = rsp.struFileInfo[i].struFileTime.GetStrTime();
                        newEnbFileInfo.RWAttr = rsp.struFileInfo[i].u8RdWrAttribute == 0 ? "可读可写" : "只读";

                        lvENBFileInfo.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            lvENBFileInfo.Items.Add(newEnbFileInfo);
                        }));
                    }
                    else
                    {
                        //添加文件信息到ListView中
                        FileInfoEnb newEnbFileInfo = new FileInfoEnb();

                        newEnbFileInfo.FileName = Encoding.Default.GetString(rsp.struFileInfo[i].s8FileName).Replace("\0", "");
                        newEnbFileInfo.IsDirectory = false;
                        newEnbFileInfo.Size = string.Format("{0:N0}KB", (rsp.struFileInfo[i].u32FileLength + 1023) / 1024);
//                        string strTime = rsp.struFileInfo[i].struFileTime.dosdt_year.ToString() + "/" + rsp.struFileInfo[i].struFileTime.dosdt_month.ToString()
//                            + rsp.struFileInfo[i].struFileTime.dosdt_day.ToString() + " " + rsp.struFileInfo[i].struFileTime.dosdt_hour.ToString() + ":" + rsp.struFileInfo[i].struFileTime.dosdt_minute.ToString();
                        newEnbFileInfo.LastModifyTime = rsp.struFileInfo[i].struFileTime.GetStrTime();
                        newEnbFileInfo.FileLittleVer = Encoding.Default.GetString(rsp.struFileInfo[i].s8FileMicroVer).Replace("\0", "");
                        newEnbFileInfo.RWAttr = rsp.struFileInfo[i].u8RdWrAttribute == 0 ? "可读可写" : "只读";

                        lvENBFileInfo.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            lvENBFileInfo.Items.Add(newEnbFileInfo);
                        }));
                    }

                }
            }
        }

        private void InitEnbTreeView(string strRootPath, string strENBIP)
        {
            int i = 0;
            while ((i = strRootPath.IndexOf('|')) > 0)
            {
                string newStr = strRootPath.Substring(0,i);

                enbMainTree.Dispatcher.BeginInvoke(new Action(() =>
                {
                    enbDirectoryTreeViewItem newItem = new enbDirectoryTreeViewItem(newStr, "");
                    enbMainTree.Items.Add(newItem);

                }));

                strRootPath = strRootPath.Substring(i + 1);
            }

            enbMainTree.Dispatcher.BeginInvoke(new Action(() =>
            {
                enbDirectoryTreeViewItem newItem = new enbDirectoryTreeViewItem(strRootPath, "");
                enbMainTree.Items.Add(newItem);
            }));

        }

        /// <summary>
        /// 本地文件拖拽功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocalListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            //以本地文件管理的  ListViewItem  作为拖拽源进行拖拽
            ListViewItem item = sender as ListViewItem;

            //判断是否是 item  发出 MouseMove 事件，并且鼠标左键保持按下状态
            if((item != null) && (e.LeftButton == MouseButtonState.Pressed ))
            {
                FileInfoDemo dragFileInfo = (FileInfoDemo)lvLocalFileInfo.SelectedItem;

                try
                {
                    DragDropEffects dragInfo = DragDrop.DoDragDrop(lvLocalFileInfo, dragFileInfo, DragDropEffects.Copy);
                }
                catch (Exception)
                {

                }
            } 
        }
        
        //线程等待
        public static void Waite(int nTime)
        {
            ExecuteWaite(() => Thread.Sleep(nTime));
        }

        public static void ExecuteWaite(Action act)
        {
            var waiteFrame = new DispatcherFrame();


            IAsyncResult ret = act.BeginInvoke(dummy => waiteFrame.Continue = false, null);

            Dispatcher.PushFrame(waiteFrame);

            act.EndInvoke(ret);
        }

        /// <summary>
        /// 开始进行文件的复制，通过流方式进行拷贝，并且显示拷贝进度
        /// </summary>
        /// <param name="strSourceFileName">源文件</param>
        /// <param name="strDestFileName">目标文件</param>
        private void DoFileCopy(string strSourceFileName, string strDestFileName)
        {
            try
            {
                //创建  源文件的  文件流  以只读方式打开，其他程序也可以同时以只读打开
                FileStream fsFileSource = new FileStream(strSourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                //创建  目标文件 的流，以创建新文件打开，如果已存在，抛出异常，其他程序可以只读打开
                FileStream fsFileDest = new FileStream(strDestFileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read);

                FileInfo fSourceInfo = new FileInfo(strSourceFileName);
                long nTotalSize = fSourceInfo.Length;                                       //文件的总长度
                byte[] bArry = new byte[4096];                                                  //读写文件的缓冲数组
                long nCurrentSize = 0;                                                                //每次读取的大小

                //添加进度条显示信息
                myList.FileName = fSourceInfo.Name;
                myList.ProgressValue = 0;
                //lvMainListView.Items.Add(myList);

                while (nCurrentSize < nTotalSize)
                {
                    int n = fsFileSource.Read(bArry, 0, 4096);                              //每次读取1024个字节

                    nCurrentSize += n;

                    fsFileDest.Write(bArry, 0, n);

                    long nProcessValue = (nCurrentSize * 100) / nTotalSize;

                    //更新进度条
                    myList.ProgressValue = nProcessValue;
                    myList.TextBloxkValue = nProcessValue.ToString() + "%";

                    Waite(1);
                }

                if (nCurrentSize == nTotalSize)
                {
                    lvMainListView.Items.Clear();
                }

                fsFileDest.Close();
                fsFileSource.Close();

                localRefreshFileList();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("文件不存在，请确认");
            }
            catch (IOException ex)
            {
                MessageBox.Show("文件已存在");
            }

        }

        /// <summary>
        /// 文件列表  的拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvLocalFileInfo_Drop(object sender, DragEventArgs e)
        {
            if (localSelectedDTVI != null)
            {
                //获取被拖拽过来的文件信息
                FileInfoDemo dropFileInfo = e.Data.GetData(typeof(FileInfoDemo)) as FileInfoDemo;

                //根据文件路径进行文件的复制
                if (dropFileInfo != null)
                {
                    try
                    {
                        //执行文件的复制
                        DoFileCopy(dropFileInfo.FilePath + "\\" + dropFileInfo.FileName, localSelectedDTVI.DirInfo.FullName + "\\" + dropFileInfo.FileName);
                    }
                    catch (Exception)
                    {
                    }

                }//end if(null)

            }//end if(selected = null)

        }


        /// <summary>
        /// 文件夹树改变时，查找文件夹下是否存在文件，如果存在，则显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocalMainTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            localSelectedDTVI = e.NewValue as DirectoryTreeViewItem;

            localRefreshFileList();
        }


        /// <summary>
        /// 右键菜单  新建文件夹  事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void localNewFolder_Click(object sender, RoutedEventArgs e)
        {
            if (localSelectedDTVI != null)
            {
                FileRename renameFolder = new FileRename("新建文件夹", null);
                renameFolder.ShowDialog();

                //如果  点击  确定，则创建文件夹，否则取消
                if (renameFolder.bOK)
                {
                    string strPath = localSelectedDTVI.DirInfo.FullName + "//" + renameFolder.strNewName;

                    int i = 0;
                    string strExitePath = strPath;

                    //最多创建到文件夹1000
                    while (Directory.Exists(strExitePath) && i < 1000)
                    {
                        i++;
                        strExitePath = strPath + i.ToString();
                    }

                    try
                    {
                        Directory.CreateDirectory(strExitePath);
                        localRefreshFileList();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("创建文件夹失败");
                    }
                }
                else
                {
                    return;
                }//end bOK

            }
            else
            {
                MessageBox.Show("请选择需要创建文件夹的父目录");
            }
        }

        /// <summary>
        /// 右键菜单  重命名  点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void localRename_Click(object sender, RoutedEventArgs e)
        {
            if (lvLocalFileInfo.SelectedItem != null)
            {
                FileInfoDemo selectedFile = (FileInfoDemo)lvLocalFileInfo.SelectedItem;
                string fileFullName = selectedFile.FilePath + "\\" + selectedFile.FileName;
                FileInfo thisFile = new FileInfo(fileFullName);

                //打开  重命名  界面，获取输入的新的文件名
                FileRename renameDlg = new FileRename(selectedFile.FileName, selectedFile.FileType);
                renameDlg.ShowDialog();
                string strNewName = renameDlg.strNewName;
                strNewName = selectedFile.FilePath + "\\" + strNewName;

                //如果 按下  确认  按键，则执行修改
                if (renameDlg.bOK)
                {
                    try
                    {
                        thisFile.MoveTo(strNewName);
                        localRefreshFileList();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("重命名失败");
                    }
                }

                //fileFullName = selectedFile.FilePath + "\\newName" + selectedFile.FileType;

                //thisFile.MoveTo(fileFullName);

                //RefreshFileList();           

            }
        }

        /// <summary>
        /// 刷新当前选中的路径下的文件信息，可以被多个函数调用的，避免添加到刷新事件中无法被其他函数调用
        /// </summary>
        private void localRefreshFileList()
        {
            //清除文件列表信息，重新加载
            lvLocalFileInfo.Items.Clear();

            FileInfo[] fileInfos;
            DirectoryInfo[] dirInfos;

            try
            {
                dirInfos = localSelectedDTVI.DirInfo.GetDirectories();
                fileInfos = localSelectedDTVI.DirInfo.GetFiles();
            }
            catch
            {
                return;
            }

            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                FileInfoDemo myDir = new FileInfoDemo();
                myDir.FileName = dirInfo.Name;
                myDir.Size = null;
                myDir.LastModifyTime = dirInfo.LastAccessTime.ToString();
                myDir.FilePath = dirInfo.Parent.Name;
                myDir.FileType = "文件夹";
                myDir.ImgSource = FileInfoGet.GetIcon(dirInfo.Name, true, true);

                lvLocalFileInfo.Items.Add(myDir);
            }

            foreach (FileInfo info in fileInfos)
            {
                FileInfoDemo myFile = new FileInfoDemo();
                myFile.FileName = info.Name;
                //将 long 类型的长度  转换为  千位分隔符表示
                //具体的做法是，如果文件为空，长度为 0KB，否则，就算不够1024也算1KB
                myFile.Size = string.Format("{0:N0}KB", (info.Length + 1023) / 1024);
                myFile.LastModifyTime = info.LastAccessTime.ToString();
                myFile.FilePath = info.DirectoryName;
                myFile.FileType = info.Extension;
                myFile.ImgSource = FileInfoGet.GetIcon(info.Name, true, false);

                lvLocalFileInfo.Items.Add(myFile);
            }
        }

        /// <summary>
        /// 右键菜单  刷新按钮  点击事件  刷新当前文件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void localRefresh_Click(object sender, RoutedEventArgs e)
        {
            localRefreshFileList();
        }

        /// <summary>
        /// 右键  查看  菜单  点击事件，查看文件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void localFileLook_Click(object sender, RoutedEventArgs e)
        {
            if (lvLocalFileInfo.SelectedItem != null)
            {
                FileInfoDemo selectedFile = (FileInfoDemo)lvLocalFileInfo.SelectedItem;

                string FileFullPath = selectedFile.FilePath + "\\" + selectedFile.FileName;

                //结构体信息填充，打开文件属性对话框
                FileInfoGet.SHELLEXECUTEINFO FileInfo = new FileInfoGet.SHELLEXECUTEINFO();
                FileInfo.cbSize = Marshal.SizeOf(FileInfo);
                FileInfo.lpVerb = "properties";
                FileInfo.lpFile = FileFullPath;
                FileInfo.nShow = FileInfoGet.SW_SHOW;
                FileInfo.fMask = FileInfoGet.SW_MASK_INVOKEIDLIST;

                FileInfo.lpDirectory = null;
                FileInfo.lpParameters = null;
                FileInfo.lpIDList = IntPtr.Zero;

                FileInfoGet.ShellExecuteEx(ref FileInfo);

            }
        }

        /// <summary>
        /// 右键菜单 下载至基站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadFileToBoard_Click(object sender, RoutedEventArgs e)
        {
            // 处理本地的文件路径
            var localFile = lvLocalFileInfo.SelectedItem as FileInfoDemo;
            if (null == localFile)
            {
                Log.Error("获取本地选中文件信息失败");
                return;
            }

            var localPath = localFile.FilePath + localFile.FileName;

            // 处理基站侧文件路径
            if (null == enbSelectedItem)
            {
                Log.Error("基站侧尚未选中任何目录，应该设置一个默认值");
                return;
            }

            var dstPath = enbSelectedItem.DirInfo;
            var enbPath = lvENBFileInfo.SelectedItem as FileInfoEnb;
            if (null != enbPath)
            {
                dstPath += $"/{enbPath.FileName}";
            }

            if (_fileHandler.SendFileToRemote(localPath, dstPath))
            {
                //MessageBox.Show("不知道发生了什么，反正没有问题，成功了吧？");
            }
        }

        /// <summary>
        /// 更新文件传输的进度
        /// </summary>
        /// <param name="pbInfo"></param>
        private void UpdateProgressCallBack(TProgressBarInfo pbInfo)
        {
            //更新进度条
            lvMainListView.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (ProcessList item in lvMainListView.Items)
                {
                    if (item.TaskID == pbInfo.m_lTaskID)
                    {
                        item.ProgressValue = pbInfo.m_nPercent;
                        item.TextBloxkValue = pbInfo.m_nPercent.ToString() + "%";
                        item.FileState = pbInfo.m_strStatus;
                    }
                }
                localRefreshFileList();
            }));
        }

        /// <summary>
        /// 创建一个文件传输进度  显示信息
        /// </summary>
        /// <param name="pbInfo"></param>
        private void NewProgressCallBack(TProgressBarInfo pbInfo)
        {
            lvMainListView.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var newProcessBarInfo = new ProcessList
                    {
                        FileName = pbInfo.m_csFileName,
                        TaskID = pbInfo.m_lTaskID,
                        ProgressValue = 0,
                        FileState = pbInfo.m_strStatus,
                        OperateType = pbInfo.strOperationType
                    };
                    lvMainListView.Items.Add(newProcessBarInfo);
                    localRefreshFileList();
                }));
        }

        /// <summary>
        /// 删除某条文件传输进度信息
        /// </summary>
        /// <param name="pbInfo"></param>
        private void EndProgressCallBack(TProgressBarInfo pbInfo)
        {
            //删除某条进度信息
            lvMainListView.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (ProcessList item in lvMainListView.Items)
                {
                    if (item.TaskID == pbInfo.m_lTaskID)
                    {
                        lvMainListView.Items.Remove(item);
                        return;
                    }
                }
                localRefreshFileList();
            }));
        }

        // 查询系统容量
        private void GetCapacity_Click(object sender, RoutedEventArgs e)
        {
            latestEnbMenuName = "SelectSystemCapacity";

            try
            {
                if (!FileMgrSendSiMsg.SendGetCapacityReq(_boardIp, "/ata2"))
                {
                    ShowTip_Error("查询系统容量失败！");
                }
            }
            catch (Exception)
            {
                ShowTip_Error("查询系统容量失败！");
            }
        }

        // ENB侧右键操作通知
        private void MenuClickCallBack(IASerialize rsp)
        {
            if (latestEnbMenuName.Equals("SelectSystemCapacity"))		// 查询系统容量
            {
                var gcRsp = rsp as SI_SILMTENB_GetCapacityRspMsg;
                if (gcRsp.s8GetResult == 1)
                {
                    ShowTip_Error("查询系统容量失败！");
                    return;
                }

                var devName = Encoding.UTF8.GetString(gcRsp.s8DevName).TrimEnd('\0');
                var restSpace = string.Format("{0:N0}", (gcRsp.u32AvailCapability + 1023) / 1024);
                var allSpace = string.Format("{0:N0}", (gcRsp.u32TotalCapability + 1023) / 1024);
                var capacityDlg = new SystemCapacity(devName, restSpace, allSpace);
                capacityDlg.ShowDialog();
            }
        }

        // 查询基站软件包版本菜单响应
        private void GetEnbSoftVer_Click(object sender, RoutedEventArgs e)
        {
            var enbVer = new EnbSoftVer(_boardIp);
            enbVer.ShowDialog();
        }

        // 查询外设软件包版本菜单响应
        private void GetPerSoftVer_Click(object sender, RoutedEventArgs e)
        {
            var perVer = new PerSoftwareVer(_boardIp);
            perVer.ShowDialog();
        }

        // 激活基站软件包
        private void ActiveEnvSoft_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new EnbSoftwareActive(_boardIp);

            if (System.Windows.Forms.DialogResult.OK != dlg.ShowDialog())
            {
                return;
            }

            
        }



        private MessageBoxResult ShowTip_Error(string msg)
        {
            return MessageBox.Show(msg, "文件管理", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private MessageBoxResult ShowTip_Confirm(string msg, MessageBoxButton btn = MessageBoxButton.YesNo)
        {
            return MessageBox.Show(msg, "文件管理", btn, MessageBoxImage.Question);
        }
    }
}
