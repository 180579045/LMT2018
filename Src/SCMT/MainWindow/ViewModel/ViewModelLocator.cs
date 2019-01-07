using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;

namespace SCMTMainWindow.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainWindowVM>();          // 主界面VM层;
            SimpleIoc.Default.Register<NodeBListManagerVM>();    // 基站列表管理Page的VM层;
            SimpleIoc.Default.Register<ENodeBMainVM>();          // 基站详细信息Page的VM层;
        }

        public MainWindowVM ViewModelMainWindow
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVM>();
            }
        }

        public NodeBListManagerVM NodeBListManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NodeBListManagerVM>();
            }
        }

        public ENodeBMainVM ViewModelENodeBMainPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ENodeBMainVM>();
            }
        }
    }
}
