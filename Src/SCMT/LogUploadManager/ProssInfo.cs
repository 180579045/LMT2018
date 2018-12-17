using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogUploadManager
{
    public class ProssInfo : INotifyPropertyChanged
    {
        //文件信息
        private string fileInfo;
        //日志id
        private int id;
        private long taskId;
        //上传进度
        private int completePross;
        private string completeProssStr;
        //状态
        private string status;
        //操作类型
        private string operationType;
        //进度条背景色
        private Brush backGroundColor;
        //进度条前景色
        private Brush forGroundColor;
        public event PropertyChangedEventHandler PropertyChanged;

        public string FileInfo
        {
            get
            {
                return fileInfo;
            }

            set
            {
                fileInfo = value;
                this.NotifyPropertyChange("FileInfo");
            }
        }

        public int CompletePross
        {
            get
            {
                return completePross;
            }

            set
            {
                completePross = value;
                this.NotifyPropertyChange("CompletePross");
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                this.NotifyPropertyChange("Status");
            }
        }

        public string OperationType
        {
            get
            {
                return operationType;
            }

            set
            {
                operationType = value;
            }
        }

        public Brush BackGroundColor
        {
            get
            {
                return backGroundColor;
            }

            set
            {
                backGroundColor = value;
            }
        }

        public Brush ForGroundColor
        {
            get
            {
                return forGroundColor;
            }

            set
            {
                forGroundColor = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public long TaskId
        {
            get
            {
                return taskId;
            }

            set
            {
                taskId = value;
            }
        }

        public string CompleteProssStr
        {
            get
            {
                return completeProssStr;
            }

            set
            {
                completeProssStr = value;
                this.NotifyPropertyChange("CompleteProssStr");
            }
        }

        public void NotifyPropertyChange(string propertyName)
        {
            if (null != this.PropertyChanged)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
