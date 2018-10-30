using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SCMTMainWindow;

namespace SCMTMainWindow.Controls
{
    /// <summary>
    /// 根据不同的数据类型，返回不同的DataGridCell的WPF显示类型;
    /// </summary>
    public class DataGrid_CellFactory
    {
        public static DataGridColumn CreateDataGridCell(DataGrid_Cell_MIB cell)
        {
            switch(cell.cellDataType)
            {
                case DataGrid_CellDataType.RegularType:
                    DataGridTextColumn textColumn = new DataGridTextColumn();



                    return textColumn;
                default:
                    DataGridTextColumn nullColumn = new DataGridTextColumn();
                    return nullColumn;
            }
        }
    }
}
