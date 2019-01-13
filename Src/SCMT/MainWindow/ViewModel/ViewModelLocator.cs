/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：ViewModelLocator.cs
// 文件功能描述：所有VM层的索引目录;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2019-1-2
//----------------------------------------------------------------*/
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

            SimpleIoc.Default.Register<MainWindowVM>();               // 主界面所有页签管理的VM层;
            SimpleIoc.Default.Register<NodeBListManagerTabVM>();      // 基站列表管理Page的VM层;
        }


        public NodeBListManagerTabVM NodeBListManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NodeBListManagerTabVM>();
            }
        }

        /// <summary>
        /// 主界面VM;
        /// </summary>
        public MainWindowVM ViewModelMainWindow
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVM>();
            }
        }

    }
}
