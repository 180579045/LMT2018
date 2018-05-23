using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMTMainWindow.Component.SCMTControl;

namespace SCMTMainWindow
{
    public class DataGrid_MIBDataControl
    {
        public DTDataGrid datagrid { get; set; }

        public DataGrid_MIBDataControl(DTDataGrid dg)
        {
            this.datagrid = dg;
        }

        public void ParseMibToDataGrid(Dictionary<string,string> pname_content, Dictionary<string,string> pname_cn)
        {


        }
    }
}
