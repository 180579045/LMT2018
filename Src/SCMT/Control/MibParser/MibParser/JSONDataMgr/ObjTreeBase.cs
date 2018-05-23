using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMT_json.JSONDataMgr
{
    class ObjTreeBase
    {
        public string objNameCh;
        public string objNameChParent;
        public int level;
        public string objNameMibTable;
    }
    class ObjTreeInfo
    {
        public string objNameCh;
        public string objNameChParent;
        public int level;
        public string objNameMibTable;
        public ObjTreeInfo[] childObj;
        
            
    }
}
