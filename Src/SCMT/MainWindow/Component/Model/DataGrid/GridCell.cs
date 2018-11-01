/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：ObjTreeNode.cs
// 文件功能描述：DataGrid单元格类型;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2018-2-20
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LmtbSnmp;

namespace SCMTMainWindow
{
    
    /// <summary>
    /// 单元格内要显示内容类型;
    /// </summary>
    public abstract class GridCell
    {
        public string oid;                             // 该单元格内对象的oid;
        public string MibName_EN;                      // 该单元格内对象的MIB英文名称;
        public string MibName_CN;                      // 该单元格内对象的MIB中文名称;
        public DataGrid_CellDataType cellDataType;     // 该单元格内显示内容的数据类型;
        public bool TableType { get; set; }            // 该单元格内保存数据的表类型,True表示带有索引的表，False表示不带索引的表;
        public string Indexs { get; set; }             // 该单元格的索引;

        public abstract void EditingCallback();        // 单元格编辑事件;
        public abstract void CellDragawayCallback();   // 单元格被拖拽事件;
        //public abstract void MouseMoveOnCell();        // 鼠标悬停在单元格;
    }
}
