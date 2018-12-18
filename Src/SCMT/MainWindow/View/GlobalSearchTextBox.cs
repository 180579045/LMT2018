using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UICore.Controls.Metro;
using SCMTMainWindow;

namespace SCMTMainWindow.View
{
    /// <summary>
    /// 全局索引功能类;
    /// </summary>
    class GlobalSearchTextBox : MetroTextBox
    {
        public I_SCMT_BelongUserControl Target_element;

        // 当搜索框输入发生改变时;
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            // 能获取到的信息;
            Console.WriteLine("用户输入内容发生改变:" + (e.OriginalSource as TextBox).Text.ToString());
            Console.WriteLine("用户行为:" + e.UndoAction);
            foreach (TextChange change in e.Changes)
            {
                Console.WriteLine("Offset:" + change.Offset + " AddedLength:" + change.AddedLength);
            }

            // 通知附属控件更新;
            if (Target_element != null)
            {
                Target_element.UpdateBelongControl(this, new SearchButtonEventArgs()
                {
                    SearchContent = (e.OriginalSource as TextBox).Text.ToString(),
                    SearchUndoAction = e.UndoAction,
                    SearchTextChange = e.Changes
                });
            }
        }
        
    }

    public class SearchButtonEventArgs : EventArgs
    {
        /// <summary>
        /// 用户搜索的字符;
        /// </summary>
        public string SearchContent;

        /// <summary>
        /// 用户的行为;
        /// </summary>
        public UndoAction SearchUndoAction;

        /// <summary>
        /// 字符改变情况;
        /// </summary>
        public ICollection<TextChange> SearchTextChange;
    }
}
