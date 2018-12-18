using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMTMainWindow.View;

namespace SCMTMainWindow
{
    /// <summary>
    /// 带有附属控件的控件;
    /// </summary>
    public interface I_SCMT_BelongUserControl
    {
        /// <summary>
        /// 通知其附属控件更新内容;
        /// </summary>
        /// <param name="sender">控件源</param>
        /// <param name="e">内容</param>
        void UpdateBelongControl(object sender, SearchButtonEventArgs e);
    }
}
