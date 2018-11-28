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
        /// <summary>
        /// cmd.json中每个命令的数据结构
        /// </summary>
        /// <param name="CmdDateSet"></param>
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
        /// <summary>
        /// cmd.json 中增加新的字段:"leafOIdSupplementList"的数据结构
        /// </summary>
        /// <param name="CmdDateSet"></param>
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
                cmdInfo.Add("leafOIdList", ReLeafListInfo(row["MIBList"].ToString()));
                cmdInfo.Add("leafOIdListDefault", ReLeafListInfoVersion2(row));
                //cmdInfo.Add("leafOIdList", ReLeafListInfoVersion2(row));

                cmdInfoList.Add(row["CmdName"].ToString(), cmdInfo);
            }
            this.cmdJObject = cmdInfoList;
            return;
        }
        /// <summary>
        /// 节点的oid列表
        /// </summary>
        /// <param name="strleaflist"></param>
        /// <returns></returns>
        JArray ReLeafListInfo(string strleaflist)
        {
            JArray leafJArray = new JArray();
            string[] sArray = strleaflist.Split('|');
            foreach (var s in sArray)
                leafJArray.Add(s);
            return leafJArray;
        }
        /// <summary>
        /// 节点的oid和其默认值的键值对
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        JObject ReLeafListInfoVersion2(DataRow row)
        {
            JObject leafInfo = null;
            string MIBList_supplement = row["MIBList_supplement"].ToString();
            if (string.Empty != MIBList_supplement)
            {
                leafInfo = new JObject();
                string[] sASupVal = MIBList_supplement.Split('=');//默认值
                leafInfo.Add("oid", sASupVal[0]);
                leafInfo.Add("value", sASupVal[1]);
            }
            return leafInfo;
        }
    }
}
