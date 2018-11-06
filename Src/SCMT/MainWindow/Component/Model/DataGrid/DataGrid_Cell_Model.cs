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
        // 单元格中的对象被拖拽到另一个对象上;
        public override void CellDragawayCallback()
        {
        }

        // 编辑该对象时的事件回调函数;
        public override void EditingCallback()
        {
            Console.WriteLine("Editing Callback");
        }

        public override void MouseMoveOnCell()
        {
        }

        public override void SelectionCellChanged()
        {
        }
    }

    /// <summary>
    /// 显示枚举类型的单元格类型;
    /// </summary>
    public class DataGrid_Cell_MIB_ENUM : GridCell
    {
        /// <summary>
        /// 要显示的数据集合;
        /// </summary>
        public Dictionary<int, string> m_AllContent { get; set; }
        
        /// <summary>
        /// 当前值;
        /// </summary>
        public int m_CurrentValue { get; set; }

        public DataGrid_Cell_MIB_ENUM()
        {
            m_AllContent = new Dictionary<int, string>();
        }

        /// <summary>
        /// 单元格被拖拽后的事件触发;
        /// </summary>
        public override void CellDragawayCallback()
        {
        }

        /// <summary>
        /// 枚举类型单元格被编辑时的事件触发;
        /// </summary>
        public override void EditingCallback()
        {
            Console.WriteLine("Editing Callback");
        }

        public override void MouseMoveOnCell()
        {
        }

        public override void SelectionCellChanged()
        {
        }
    }

    public class DataGridCell_MIB_MouseEventArgs
    {
        public string HeaderName { get; set; }
        public DyDataGrid_MIBModel SelectedCell { get; set; }
    }
}
