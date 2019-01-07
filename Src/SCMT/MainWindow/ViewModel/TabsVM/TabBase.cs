using System;
using System.Collections.Generic;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace SCMTMainWindow.ViewModel
{
    /// <summary>
    /// 标签页**页签和页签内容**的VM层基类;
    /// TabBase包含了页签该有的基础内容;
    /// </summary>
    public abstract class TabBase : ViewModelBase
    {
        /// <summary>
        /// Tab页的编号;
        /// </summary>
        private int _tabNumber;
        public int TabNumber
        {
            get => _tabNumber;
            set
            {
                if (_tabNumber != value)
                {
                    Set(() => TabNumber, ref _tabNumber, value);
                }
            }
        }

        /// <summary>
        /// Tab页的名称(后续就是基站名称吧);
        /// </summary>
        private string _tabName;
        public string TabName
        {
            get => _tabName;
            set
            {
                if (_tabName != value)
                {
                    Set(() => TabName, ref _tabName, value);
                }
            }
        }

        /// <summary>
        /// 是否固定;
        /// </summary>
        private bool _isPinned;
        public bool IsPinned
        {
            get => _isPinned;
            set
            {
                if (_isPinned != value)
                {
                    Set(() => IsPinned, ref _isPinned, value);
                }
            }
        }

        /// <summary>
        /// Tab的标志;
        /// </summary>
        private ImageSource _tabIcon;
        public ImageSource TabIcon
        {
            get => _tabIcon;
            set
            {
                if (!Equals(_tabIcon, value))
                {
                    Set(() => TabIcon, ref _tabIcon, value);
                }
            }
        }
    }
}
