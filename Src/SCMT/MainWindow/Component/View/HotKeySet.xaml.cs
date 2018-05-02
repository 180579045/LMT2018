using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SCMTMainWindow.Component.View
{
    /// <summary>
    /// HotKeySet.xaml 的交互逻辑
    /// </summary>
    public partial class HotKeySet : Window
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        private static HotKeySet m_Instance;

        public static HotKeySet CreateInstance()
        {
            return m_Instance ?? (m_Instance = new HotKeySet());
        }

        private ObservableCollection<HotKeyModel> m_ListHotKeyModel = new ObservableCollection<HotKeyModel>();
        /// <summary>
        /// 快捷键  设置项  集合  保存从配置文件读取的集合，并和界面绑定显示
        /// </summary>
        public ObservableCollection<HotKeyModel> ListHotKeyModel
        {
            get
            {
                return m_ListHotKeyModel;
            }
            set
            {
                m_ListHotKeyModel = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HotKeySet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载完成之后，从配置文件获取数据，绑定到界面显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HotKeySettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var list = HotKeyInit.Instance.LoadJsonFileInfo();

            if (list == null)
            {
                MessageBox.Show("加载配置文件失败");
                this.Close();
                return;
            }
            list.ToList().ForEach(x => ListHotKeyModel.Add(x));
        }

        /// <summary>
        /// 窗口关闭之后，单例清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HotKeySettingWindow_Closed(object sender, EventArgs e)
        {
            m_Instance = null;
        }

        /// <summary>
        /// 保存设置，调用  注册事件  进行注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSetting_Click(object sender, RoutedEventArgs e)
        {
            if (!HotKeyInit.Instance.RegisterGlobalHotKey(ListHotKeyModel))
            {
                return;
            }
            this.Close();
        }
    }
}

