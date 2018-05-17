using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json.Linq;

namespace SCMT_json.JSONDataMgr
{
    class CmdTreeJsonData
    {
        private JObject cmdJObject;
        public string GetStringObjTreeJson()
        {
            return this.cmdJObject.ToString();
        }
        public void CmdParseDataSet(DataSet CmdDateSet)
        {
            JObject cmdInfoList = new JObject();
            int cmdNum = CmdDateSet.Tables[0].Rows.Count;
            for (int loop = 0; loop < cmdNum - 1; loop++)
            {
                DataRow row = CmdDateSet.Tables[0].Rows[loop];

                JObject cmdInfo = new JObject();
                cmdInfo.Add("TableName", row["HostTableName"].ToString());
                cmdInfo.Add("CmdType", row["CmdType"].ToString());
                cmdInfo.Add("CmdDesc", row["CmdDesc"].ToString());
                cmdInfo.Add("leafOIdList", ReLeafListInfo(row["MIBList"].ToString()));

                cmdInfoList.Add(row["CmdName"].ToString(), cmdInfo);
            }
            this.cmdJObject = cmdInfoList;
            return;
        }

        JArray ReLeafListInfo(string strleaflist)
        {
            JArray leafJArray = new JArray();
            string[] sArray = strleaflist.Split('|');
            foreach (var s in sArray)
                leafJArray.Add(s);
            return leafJArray;
        }
    }
}
