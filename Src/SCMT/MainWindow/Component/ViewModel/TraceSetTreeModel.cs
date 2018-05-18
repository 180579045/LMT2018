using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SCMTMainWindow.Component
{
    public class TraceSetTreeModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string _name;

        /// <summary>
        /// 是否选中
        /// </summary>
        private bool _isChecked;

        /// <summary>
        /// 是否折叠
        /// </summary>
        private bool _isExpanded;

        /// <summary>
        /// 子项
        /// </summary>
        private IList<TraceSetTreeModel> _children;

        /// <summary>
        /// 父项
        /// </summary>
        private TraceSetTreeModel _parent;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TraceSetTreeModel()
        {
            _children = new List<TraceSetTreeModel>();
            _isChecked = false;
            _isExpanded = false;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 鼠标停留时显示名称
        /// </summary>
        public string ToolTip
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// 选中状态
        /// </summary>
        public bool ISChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (value != _isChecked)
                {
                    //触发  选中状态改变事件
                    _isChecked = value;
                    INotifyPropertyChanged("IsChecked");

                    //子项被选中时，父项也被选中
                    if (_isChecked)
                    {
                        if (Parent != null)
                        {
                            Parent.ISChecked = true;
                        }
                    }
                    else
                    {
                        //父项取消选中时，所有子项也取消选中
                        foreach (TraceSetTreeModel child in Children)
                        {
                            child.ISChecked = false;
                        }
                    }//else
                }//if(value != ...
            }//set
        }

        /// <summary>
        /// 折叠状态
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    INotifyPropertyChanged("IsExpanded");
                }
            }
        }

        /// <summary>
        /// 父项
        /// </summary>
        public TraceSetTreeModel Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// 子项
        /// </summary>
        public IList<TraceSetTreeModel> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        /// <summary>
        /// 选中所有子项
        /// </summary>
        /// <param name="bChecked"></param>
        public void SetAllChildrenChecked(bool bChecked)
        {
            foreach (TraceSetTreeModel child in Children)
            {
                child.ISChecked = bChecked;
                child.SetAllChildrenChecked(bChecked);
            }
        }

        /// <summary>
        /// 折叠所有子项
        /// </summary>
        /// <param name="bExpanded"></param>
        public void SetAllChildrenExpended(bool bExpanded)
        {
            foreach (TraceSetTreeModel child in Children)
            {
                child.IsExpanded = bExpanded;
                child.SetAllChildrenExpended(bExpanded);
            }
        }

        /// <summary>
        /// 属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void INotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
