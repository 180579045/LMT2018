using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow
{
    public abstract class GridCell
    {
        public string content { get; set; }            // 表格内容;
        public abstract void EditingCallback();        // 单元格编辑事件;
        public abstract void CellDragawayCallback();   // 单元格被拖拽事件;
    }
}
