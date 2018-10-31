using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using LmtbSnmp;
using SCMTMainWindow;

namespace SCMTMainWindow
{
    /// <summary>
    /// 根据不同的数据类型，返回不同的DataGridCell的WPF显示类型;
    /// </summary>
    public class DataGridCellFactory
    {
        public static GridCell CreateGridCell(string MibName, string MibNameCN, string ContentValue, string oid, string targetIP)
        {
            // 如果是字符串类型的单元格;
            if (SnmpToDatabase.GetMibNodeDataType(MibName, targetIP) == DataGrid_CellDataType.RegularType)
            {
                var dgm = new DataGrid_Cell_MIB()
                {
                    m_Content = SnmpToDatabase.ConvertSnmpValueToString(MibName, ContentValue, targetIP) as string,
                    oid = oid,
                    MibName_CN = MibNameCN,
                    MibName_EN = MibName
                };
            }
            // 如果是枚举类型的单元格;
            else if (SnmpToDatabase.GetMibNodeDataType(MibName, targetIP) == DataGrid_CellDataType.enumType)
            {
                Dictionary<int, string> all_list = new Dictionary<int, string>();     // 获取所有要显示的值的集合;

                all_list = SnmpToDatabase.ConvertSnmpValueToEnumContent(MibName, targetIP);


                var dgm = new DataGrid_Cell_MIB_ENUM()
                {
                    m_AllContent = all_list,
                    m_ShowContent = SnmpToDatabase.ConvertSnmpValueToString(MibName, ContentValue, targetIP) as string,
                    oid = oid,
                    MibName_CN = MibNameCN,
                    MibName_EN = MibName
                };
            }
            // 如果是要时间类型的单元格;
            else if(SnmpToDatabase.GetMibNodeDataType(MibName, targetIP) == DataGrid_CellDataType.DateTime)
            {

            }
            // 如果是BIT类型的单元格;
            else if(SnmpToDatabase.GetMibNodeDataType(MibName, targetIP) == DataGrid_CellDataType.bitType)
            {

            }

            return null;
        }
    }
}
