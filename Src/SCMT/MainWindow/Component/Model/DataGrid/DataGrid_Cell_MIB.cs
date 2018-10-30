using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMTMainWindow.Component.ViewModel;

namespace SCMTMainWindow
{
    /// <summary>
    /// 单元格内可显示的所有数据类型;
    /// </summary>
    public enum DataGrid_CellDataType
    {
        enumType = 0,                                  // MIB中的枚举类型;
        bitType = 1,                                   // MIB中的BIT类型;
        DateTime = 2,                                  // MIB中的时间类型;
        RegularType = 3                                // MIB中的字符串、INT等类型;
    }
    
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
        public List<string> m_AllContent { get; set; }

        public DataGrid_Cell_MIB_ENUM()
        {
            m_AllContent = new List<string>();
        }

        public override void CellDragawayCallback()
        {
            throw new NotImplementedException();
        }

        public override void EditingCallback()
        {
            throw new NotImplementedException();
        }
    }

    public class DataGridCell_MIB_MouseEventArgs
    {
        public string HeaderName { get; set; }
        public DyDataGrid_MIBModel SelectedCell { get; set; }
    }
}
