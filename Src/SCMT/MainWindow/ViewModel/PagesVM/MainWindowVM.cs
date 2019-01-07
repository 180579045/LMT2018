using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ChromeTabs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SCMTMainWindow.ViewModel
{
    /// <summary>
    /// 主界面VM层基类;
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        /// <summary>
        /// 所有标签页内容集合,ChromeTabs控件，可容纳自定义的类型;
        /// </summary>
        public ObservableCollection<TabBase> ItemCollection { get; set; }

        /// <summary>
        /// 重新排序Tab标签;
        /// </summary>
        public RelayCommand<TabReorder> ReorderTabsCommand { get; set; }

        /// <summary>
        /// 添加Tab页的处理命令;
        /// </summary>
        public RelayCommand AddTabCommand { get; set; }

        /// <summary>
        /// 关闭Tab页的处理命令;
        /// </summary>
        public RelayCommand<TabBase> CloseTabCommand { get; set; }
        
        /// <summary>
        /// 当前选择的Tab页;
        /// </summary>
        private TabBase _selectedTab;
        public TabBase SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (_selectedTab != value)
                {
                    Set(() => SelectedTab, ref _selectedTab, value);
                }
            }
        }

        /// <summary>
        /// 是否允许添加页签,当内存较小的时候，不允许添加基站，非基站页签除外;
        /// </summary>
        private bool _canAddTabs;
        public bool CanAddTabs
        {
            get => _canAddTabs;
            set
            {
                if (_canAddTabs == value) return;
                Set(() => CanAddTabs, ref _canAddTabs, value);
                AddTabCommand.RaiseCanExecuteChanged();
            }
        }

        private string _title = "DTMobile Station Combine Maintain Tool";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Set(()=> _title, ref _title, value);
            }
        }


        public MainWindowVM()
        {
            ItemCollection = new ObservableCollection<TabBase>();
            ItemCollection.CollectionChanged += ItemCollection_CollectionChanged;
            ReorderTabsCommand = new RelayCommand<TabReorder>(ReorderTabsCommandAction);
            AddTabCommand = new RelayCommand(AddTabCommandAction, () => CanAddTabs);
            CloseTabCommand = new RelayCommand<TabBase>(CloseTabCommandAction);
            CanAddTabs = true;
        }
    
        /// <summary>
        /// Reorder the tabs and refresh collection sorting.
        /// </summary>
        /// <param name="reorder"></param>
        protected virtual void ReorderTabsCommandAction(TabReorder reorder)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemCollection);
            int from = reorder.FromIndex;
            int to = reorder.ToIndex;
            var tabCollection = view.Cast<TabBase>().ToList();//Get the ordered collection of our tab control

            tabCollection[from].TabNumber = tabCollection[to].TabNumber; //Set the new index of our dragged tab

            if (to > from)
            {
                for (int i = from + 1; i <= to; i++)
                {
                    tabCollection[i].TabNumber--; //When we increment the tab index, we need to decrement all other tabs.
                }
            }
            else if (from > to)//when we decrement the tab index
            {
                for (int i = to; i < from; i++)
                {
                    tabCollection[i].TabNumber++;//When we decrement the tab index, we need to increment all other tabs.
                }
            }

            view.Refresh();//Refresh the view to force the sort description to do its work.
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TabBase tab in e.NewItems)
                {
                    if (ItemCollection.Count > 1)
                    {
                        //If the new tab don't have an existing number, we increment one to add it to the end.
                        if (tab.TabNumber == 0)
                            tab.TabNumber = ItemCollection.OrderBy(x => x.TabNumber).LastOrDefault().TabNumber + 1;
                    }
                }
            }
            else
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(ItemCollection);
                view.Refresh();
                var tabCollection = view.Cast<TabBase>().ToList();
                foreach (var item in tabCollection)
                    item.TabNumber = tabCollection.IndexOf(item);
            }
        }

        /// <summary>
        /// 关闭标签页的时候执行的命令;
        /// </summary>
        /// <param name="vm"></param>
        private void CloseTabCommandAction(TabBase vm)
        {
            ItemCollection.Remove(vm);
        }

        /// <summary>
        /// 添加标签页的时候执行的命令;
        /// 默认为添加基站;
        /// </summary>
        private void AddTabCommandAction()
        {
            Console.WriteLine("Add tab by USER");
            ItemCollection.Add(CreateENodeBTab());
        }

        protected TabBase CreateENodeBTab()
        {
            ENodeBTab tab = new ENodeBTab();
            return tab;
        }
    }
}
