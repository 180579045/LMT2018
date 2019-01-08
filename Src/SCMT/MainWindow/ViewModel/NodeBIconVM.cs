using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SCMTMainWindow.ViewModel
{
    public class NodeBIconVM : ViewModelBase
    {
        /// <summary>
        /// 显示基站Icon的图标;
        /// </summary>
        public Path NodeBIcon { get; set; }

        /// <summary>
        /// 显示基站Icon的IP地址;
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 显示基站Icon的友好名;
        /// </summary>
        public string FriendlyName { get; set; }
    }
}
