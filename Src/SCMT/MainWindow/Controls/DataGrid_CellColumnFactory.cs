using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMTMainWindow;
using System.Windows.Controls;

namespace SCMTMainWindow.Controls
{

    public class DataGrid_CellColumnFactory
    {
        /// <summary>
        /// 根据不同的数据类型，返回不同的DataGrid中的列类型;
        /// </summary>
        public static DataGridColumn CreateDataGridCellColumn(GridCell cell)
        {
            // 如果只需要显示字符;
            if (cell is DataGrid_Cell_MIB)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                return column;
            }
            // 如果需要显示枚举类型的下拉框;
            else if (cell is DataGrid_Cell_MIB_ENUM)
            {
                DataGridTemplateColumn column = new DataGridTemplateColumn();
                return column;
            }

            return null;
        }
    }
}
