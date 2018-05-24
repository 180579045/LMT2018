using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMTMainWindow.Component.ViewModel;

namespace SCMTMainWindow
{
    public class DataGrid_Cell_MIB : GridCell
    {
        public string oid;                             // 该单元格内对象的oid;
        public string MibName_EN;                      // 该单元格内对象的MIB英文名称;
        public string MibName_CN;                      // 该单元格内对象的MIB中文名称;
        public string m_Content { get; set; }          // 该单元格内要显示的内容;
        //____________________________________以下是否使用待定;
        public bool TableType { get; set; }            // 该单元格内保存数据的表类型,True表示带有索引的表，False表示不带索引的表;
        public string Indexs { get; set; }             // 该单元格的索引;

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

    public class DataGridCell_MIB_MouseEventArgs
    {
        public string HeaderName { get; set; }
        public DyDataGrid_MIBModel SelectedCell { get; set; }
    }
}
