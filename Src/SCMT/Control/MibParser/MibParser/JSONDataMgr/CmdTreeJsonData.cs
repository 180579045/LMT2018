using System.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MIBDataParser.JSONDataMgr
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
        public void CmdParseDataSetVersion2(DataSet CmdDateSet)
        {
            JObject cmdInfoList = new JObject();
            int cmdNum = CmdDateSet.Tables[0].Rows.Count;
            for (int loop = 0; loop < cmdNum - 1; loop++)
            {
                DataRow row = CmdDateSet.Tables[0].Rows[loop];

                string MoName = row["MOName"].ToString();
                if (string.Equals("不显示", MoName))
                {
                    continue;
                }
                JObject cmdInfo = new JObject();
                cmdInfo.Add("TableName", row["HostTableName"].ToString());
                cmdInfo.Add("CmdType", row["CmdType"].ToString());
                cmdInfo.Add("CmdDesc", row["CmdDesc"].ToString());
                cmdInfo.Add("leafOIdList", ReLeafListInfoVersion2(row));

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

        JObject ReLeafListInfoVersion2(DataRow row)
        {
            string MIBList_supplement = row["MIBList_supplement"].ToString();
            string strleaflist = row["MIBList"].ToString();
            JObject leafInfo = new JObject();
            string[] sArray = strleaflist.Split('|').Distinct().ToArray();
            //sArray.Distinct().ToList()
            string[] sArrayVal = MIBList_supplement.Split('=');
            foreach (var s in sArray)
            {
                if (s.Equals(sArrayVal[0]))
                {
                    leafInfo.Add(s, sArrayVal[1]);
                }
                else
                {
                    try {
                        leafInfo.Add(s, "NULL");
                    }
                    catch
                    {
                        int abc = 1;
                    }
                    
                }
            }
            return leafInfo;
        }
    }
}
