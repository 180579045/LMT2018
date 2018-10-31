using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMTMainWindow.Component.ViewModel;

namespace SCMTMainWindow
{
    
    /// <summary>
    /// 显示常规字符串内容的单元格类型;
    /// </summary>
    public class DataGrid_Cell_MIB : GridCell
    {
        public string m_Content { get; set; }          // 该单元格内要显示的内容;
        
        // 单元格中的对象被拖拽到另一个对象上;
        public override void CellDragawayCallback()
        {
            throw new NotImplementedException();
        }

        // 编辑该对象时的事件回调函数;
        public override void EditingCallback()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 显示枚举类型的单元格类型;
    /// </summary>
    public class DataGrid_Cell_MIB_ENUM : GridCell
    {
        public Dictionary<int, string> m_AllContent { get; set; }     // 要显示的数据集合;
        public string m_ShowContent { get; set; }                     // 当前要显示的内容;

        public DataGrid_Cell_MIB_ENUM()
        {
            m_AllContent = new Dictionary<int, string>();
        }

        /// <summary>
        /// 单元格被拖拽后的事件触发;
        /// </summary>
        public override void CellDragawayCallback()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 枚举类型单元格被编辑时的事件触发;
        /// </summary>
        public override void EditingCallback()
        {
            // 需要留意必须在鼠标双击之后，才会回调该函数;
            throw new NotImplementedException();
        }
    }

    public class DataGridCell_MIB_MouseEventArgs
    {
        public string HeaderName { get; set; }
        public DyDataGrid_MIBModel SelectedCell { get; set; }
    }
}
