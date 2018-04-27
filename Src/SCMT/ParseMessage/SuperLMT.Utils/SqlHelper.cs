using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperLMT.Utils
{
    public class SqlHelper
    {
        private static SqlHelper instance = new SqlHelper();

        public static SqlHelper Instance
        {
            get { return instance; }
        }

        public string GetMainQueryStr()
        {
            string query = "Oid,IsMarked,DisplayIndex,TimeStamp,Protocol,EventName,MessageSource,MessageDestination,HostNeid,LocalCellId,CellId,CellUeId,HalfSubFrameNo,EnbUeId,CRNTI,MMEUES1APID,UEX2APID,IMSI";
            return query;
        }
    }
}
