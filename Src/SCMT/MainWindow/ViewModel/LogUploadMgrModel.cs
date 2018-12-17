using LogUploadManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;
using static LogUploadManager.CommonFunction;

namespace SCMTMainWindow.ViewModel
{
    public class LogUploadMgrModel : INotifyPropertyChanged
    {
       // private static readonly LogUploadMgrModel Instance = new LogUploadMgrModel();
        /// <summary>
        /// 全选标志位
        /// </summary>
        private bool selectedAll = false;
        private int taskFinished = 0;
        private LogUploadHelper logUploadHelper = new LogUploadHelper();
        //public static LogUploadMgrModel Singleton {

        //    get {

        //        return Instance;
        //    }
        //}
        /// <summary>
        /// 全选后执行方法
        /// </summary>
        /// <param name="isSelected"></param>
        public void SetAllPubllicFileSeletedStatus(bool isSelected) {

            for (int i = 0; i < PublicInfoList.Count; i++) {
                PublicInfoList[i].IsMarked = isSelected;
            }
            if (isSelected)
            {
                SelectedAll = true;
            }
            else {
                SelectedAll = false;
            }
        }
        /// <summary>
        /// 选中一条日志后触发方法
        /// </summary>
        public void SelectedSingleFile() {
            for (int i = 0; i < PublicInfoList.Count; i++)
            {
                if (!PublicInfoList[i].IsMarked) {
                    return;
                }
            }
            SelectedAll = true;
        }
        /// <summary>
        /// 当取消选择时
        /// </summary>
        public void UnSelectedSingleFile() {
            if (SelectedAll) {
                SelectedAll = false;
            }

        }
        /// <summary>
        /// 反选
        /// </summary>
        public void SelectedOther() {
            for (int i = 0; i < PublicInfoList.Count; i++)
            {
                PublicInfoList[i].IsMarked = !PublicInfoList[i].IsMarked;
            }
        }
        /// <summary>
        /// 公共日志传输进程信息
        /// </summary>
        private ObservableCollection<ProssInfo> prosList = new ObservableCollection<ProssInfo>();
        /// <summary>
        /// 页面上公共日志列表信息
        /// </summary>
        private ObservableCollection<PublicLogInfo> publicInfoList = new ObservableCollection<PublicLogInfo>();
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 构造器
        /// </summary>
        public LogUploadMgrModel() {
            this.Inital();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Inital() {
            ParseLogXml();
        }
        private void ParseLogXml()
        {
            XmlDocument rootElement = new XmlDocument();
            rootElement.Load(System.AppDomain.CurrentDomain.BaseDirectory + "//Component//Configration//LMTBoardLogType5216.xml");
            XmlNodeList children = rootElement.SelectNodes("//LOGSHOW//PUBLICFILE//LOGSHOW");
            foreach (var child in children)
            {
                var xmlElement = child as XmlElement;
                if (xmlElement != null)
                {
                    string type = xmlElement.GetAttribute("TYPE");
                    int typeid = Convert.ToInt16(type);
                    string caption = xmlElement.GetAttribute("CAPTION");
                    publicInfoList.Add(new PublicLogInfo(typeid, caption));
                }

            }
        }
        public ObservableCollection<ProssInfo> ProsList
        {
            get
            {
                return prosList;
            }

            set
            {
                prosList = value;
                this.NotifyPropertyChange("ProsList");
            }
        }

        public ObservableCollection<PublicLogInfo> PublicInfoList
        {
            get
            {
                return publicInfoList;
            }

            set
            {
                publicInfoList = value;
                this.NotifyPropertyChange("PublicInfoList");
            }
        }

        public bool SelectedAll
        {
            get
            {
                return selectedAll;
            }

            set
            {
                selectedAll = value;
                this.NotifyPropertyChange("SelectedAll");
            }
        }

        public void NotifyPropertyChange(string propertyName)
        {
            if (null != this.PropertyChanged)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 上传日志总入口
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="targPath"></param>
        public void UploadLogList(LOGTYPEENUM logType, string targPath) {
            //LogUploadHelper.Singleton.
            logUploadHelper.publicLogTransResult.Clear();
            logUploadHelper.StartAcceptTapMessage();
            switch (logType) {
                case LOGTYPEENUM.ENUM_PUBLICLOG:
                    UploadPublicLogList(targPath);
                    break;

            }
        }
        public void UploadPublicLogList(string targPath) {
            int rowId = -1;
            long  taskId = -1;
            bool bTaskSend = true;
            bool bProceBarIns = true;
            bool bAllSuccess = true;
            this.prosList.Clear();
            taskFinished = 0;
            //更新进度条

            //上传日志
            for (int index = 0; index < PublicInfoList.Count; index++) {
                if (PublicInfoList[index].IsMarked) {
                    int downLoadIndex = PublicInfoList[index].Id;
                    // LogUploadHelper.Singleton.UploadPublicLog(downLoadIndex, targPath,ref taskId);
                   if( 1==logUploadHelper.UploadPublicLog(downLoadIndex, targPath, ref taskId)){ 
                    //更新进度条
                    ProssInfo prossInfo = new ProssInfo();
                    prossInfo.FileInfo = "[" + PublicInfoList[index].Describe + "]";
                    prossInfo.Id = PublicInfoList[index].Id;
                    prossInfo.TaskId = taskId;
                    prossInfo.CompletePross = 0;
                    prossInfo.Status = "文件上传等待中";
                    prossInfo.OperationType = "上传";
                    prossInfo.BackGroundColor = Brushes.LightBlue;
                    prossInfo.ForGroundColor = Brushes.Green;
                    prosList.Add(prossInfo);
                    }
                }
            }
            //总进度条信息插入
            string tatolInfo = string.Format("日志上传总进度：0/{0}", prosList.Count);
            ProssInfo prossInfototal = new ProssInfo();
            prossInfototal.FileInfo = tatolInfo;
            prosList.Insert(0, prossInfototal);
        }
        /// <summary>
        /// 更新进度条
        /// </summary>
        private void UpdateProssBar(){ 




        }

        /// <summary>
        /// 获取整体的上传进度 全部上传为true 否则为false
        /// </summary>
        /// <returns></returns>
        public bool GetAllUploadProgress(LOGTYPEENUM logType) {
            int nProcBarCount = prosList.Count();
            //是否全部上传完毕标志位
            bool bAllFinished = true;
            //返回进度0-100 
            long iRet = 0;
            //遍历各文件上传进度
            Console.WriteLine("1111111111111111111111111111111111111111111");
            for (int i=1;i< nProcBarCount;i++) {
                string status = prosList[i].Status;
                //查找正在上传或等待上传或删除失败的任务
                if ((status=="上传正在进行")|| (status == "任务删除失败")|| (status == "文件上传等待中")) {
                    switch (logType) {
                        //公共日志处理
                        case LOGTYPEENUM.ENUM_PUBLICLOG:
                            iRet = GetUploadProgress(prosList[i].TaskId, prosList[i].Id);
                            Console.WriteLine(iRet);
                            Console.WriteLine(i);
                            Console.WriteLine("22222222222222222222222222222222222222");
                            break;

                    }
                    //控制进度条显示
                    //上传失败,下发命令失败
                    if (-1 == iRet)
                    {
                        prosList[i].Status = "上传失败";
                        prosList[i].ForGroundColor = Brushes.Red;
                        taskFinished++;
                        var tatolInfo= string.Format("日志上传总进度：{0}/{1}", taskFinished, prosList.Count-1);
                        prosList[0].Status = tatolInfo;
                        Console.WriteLine("333333333333333333333333333333");
                    }
                    else if (-2 == iRet)
                    {
                        bAllFinished = false;
                        Console.WriteLine("44444444444444444444444444444");
                    }
                    else if (-3 == iRet)
                    {
                        prosList[i].Status = "上传失败";
                        prosList[i].ForGroundColor = Brushes.Red;
                        taskFinished++;
                        var tatolInfo = string.Format("日志上传总进度：{0}/{1}", taskFinished, prosList.Count - 1);
                        prosList[0].FileInfo = tatolInfo;
                        Console.WriteLine("5555555555555555555555555555555");
                    }
                    else if (iRet >= 0) {
                        if (iRet >= 100)
                        {
                            //上传结束
                            prosList[i].Status = "上传已完成";
                            prosList[i].CompletePross = 100;
                            prosList[i].CompleteProssStr = prosList[i].CompletePross.ToString() + "%";
                            prosList[i].ForGroundColor = Brushes.Green;
                            taskFinished++;
                            Console.WriteLine("*****************************************");
                            var tatolInfo = string.Format("日志上传总进度：{0}/{1}", taskFinished, prosList.Count - 1);
                            prosList[0].FileInfo = tatolInfo;
                            

                        }
                        else {
                            //上传未结束
                            prosList[i].CompletePross =(int) iRet;
                            prosList[i].CompleteProssStr = prosList[i].CompletePross.ToString() + "%";
                            prosList[i].Status = "上传正在进行";                            
                            prosList[i].ForGroundColor = Brushes.Green;                           
                            var tatolInfo = string.Format("日志上传总进度：{0}/{1}", taskFinished, prosList.Count - 1);
                            prosList[0].FileInfo = tatolInfo;
                            Console.WriteLine("66666666666666666666666666666666");
                            bAllFinished = false;
                        }
                    }
                }
            }
            return bAllFinished;
        }
        public long GetUploadProgress(long TaskId,int LogId) {
            int nProgress=0;
            // if (LogUploadHelper.Singleton.GetUploadPublicLogProgress(TaskId, LogId, ref nProgress))
            if (logUploadHelper.GetUploadPublicLogProgress(TaskId, LogId, ref nProgress))
            {

                return nProgress;
            }
            else {
                return -2;
            }            
        }


    }   
}
