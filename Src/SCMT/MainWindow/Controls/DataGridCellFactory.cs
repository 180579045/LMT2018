using System.Collections.Generic;
using CommonUtility;
using LmtbSnmp;

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
			// var value = ContentValue.Replace(" ", ""); // 不能随意替换空格，如日期
			var value = ContentValue;
            var dataType = SnmpToDatabase.GetMibNodeDataType(MibName, targetIP);
			if (dataType == DataGrid_CellDataType.RegularType || 
				dataType == DataGrid_CellDataType.OID ||
				dataType == DataGrid_CellDataType.Array)
            {
                var dgm = new DataGrid_Cell_MIB()
                {
                    m_Content = SnmpToDatabase.ConvertSnmpValueToString(MibName, value, targetIP) as string,
                    oid = oid,
                    m_bIsReadOnly = SnmpToDatabase.GetReadAndWriteStatus(MibName,targetIP),
                    MibName_CN = MibNameCN,
                    MibName_EN = MibName
                };

                return dgm;
            }

            // 如果是枚举类型的单元格;
            else if (dataType == DataGrid_CellDataType.enumType)
            {
                Dictionary<int, string> all_list = new Dictionary<int, string>();     // 获取所有要显示的值的集合;

                all_list = SnmpToDatabase.ConvertSnmpValueToEnumContent(MibName, targetIP);

                var dgm = new DataGrid_Cell_MIB_ENUM()
                {
                    m_AllContent = all_list,
                    m_Content = SnmpToDatabase.ConvertSnmpValueToString(MibName, value, targetIP) as string,
                    m_CurrentValue = int.Parse(value),
                    oid = oid,
                    m_bIsReadOnly = SnmpToDatabase.GetReadAndWriteStatus(MibName, targetIP),
                    MibName_CN = MibNameCN,
                    MibName_EN = MibName
                };

                return dgm;
            }

            // 如果是要时间类型的单元格;
            else if(dataType == DataGrid_CellDataType.DateTime)
            {
                var dgm = new DataGrid_Cell_MIB()
                {
					// 获取SNMP参数时日期类型已经转换为标准格式
					//m_Content = SnmpToDatabase.ConvertSnmpValueToString(MibName, value, targetIP) as string,
					m_Content = value,
					oid = oid,
                    m_bIsReadOnly = SnmpToDatabase.GetReadAndWriteStatus(MibName, targetIP),
                    MibName_CN = MibNameCN,
                    MibName_EN = MibName
                };

                return dgm;
            }

            // 如果是BIT类型的单元格;
            else if(dataType == DataGrid_CellDataType.bitType)
            {
                Dictionary<int, string> all_list = new Dictionary<int, string>();     // 获取所有要显示的值的集合;

                all_list = SnmpToDatabase.ConvertSnmpValueToEnumContent(MibName, targetIP);
                var dgm = new DataGrid_Cell_MIB_BIT()
                {
                    m_AllBit = all_list,
                    m_Content = SnmpToDatabase.ConvertSnmpValueToString(MibName, value, targetIP) as string,
                    oid = oid,
                    m_bIsReadOnly = SnmpToDatabase.GetReadAndWriteStatus(MibName, targetIP),
                    MibName_CN = MibNameCN,
                    MibName_EN = MibName
                };

                return dgm;
            }

            return null;
            
        }
    }
}
