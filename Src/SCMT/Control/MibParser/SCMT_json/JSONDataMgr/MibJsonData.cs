using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json.Linq;


namespace SCMT_json.JSONDataMgr
{
    class MibJsonData
    {
        string stringMibJson;
        JObject mibJObject; // mib节点，表
        //JArray mibJArray;
        int tableNum;
        string mibVersion;

        public MibJsonData()
        {
            this.mibJObject = new JObject();
            this.tableNum = 0;
        }
        public MibJsonData(string mibVersion)
        {
            this.mibJObject = new JObject();
            this.tableNum = 0;
            this.mibVersion = mibVersion;
        }
        public JObject GetMibJObject()
        {
            return this.mibJObject;
        }
        public string GetStringMibJson()
        {
            return this.stringMibJson;
        }
        public void SetMibVersion(string version)
        {
            this.mibVersion = version;
            return;
        }
        public void MibParseDataSet(DataSet MibdateSet)
        {
            JArray mibJArray = new JArray();
            JObject objRec = null;
            int leafIndex = 0;
            JArray childJArray = null;
            for (int loop = 0; loop < MibdateSet.Tables[0].Rows.Count - 1; loop++)
            {
                DataRow row = MibdateSet.Tables[0].Rows[loop];
                DataRow nextRow = MibdateSet.Tables[0].Rows[loop + 1];
                EnumTableLeafType tableLeafType = IsTable(row["IsLeaf"].ToString(), nextRow["IsLeaf"].ToString());
                if (EnumTableLeafType.table == tableLeafType)
                {
                    if (0 != this.tableNum)
                    {
                        objRec.Add("childList", childJArray);
                        mibJArray.Add(objRec);
                    }
                    objRec = new JObject();
                    childJArray = new JArray();
                    leafIndex = 0;
                }
                switch (tableLeafType)
                {
                    case EnumTableLeafType.table:
                        objRec = TableToJsonData(row, nextRow);
                        this.tableNum++;
                        break;
                    case EnumTableLeafType.leaf:
                        LeafToJsonData(childJArray, row, leafIndex);
                        leafIndex++;
                        break;
                    default:
                        break;
                }

            }
            this.mibJObject.Add("mibVersion", this.mibVersion);
            this.mibJObject.Add("tableNum", this.tableNum);
            this.mibJObject.Add("tableList", mibJArray);
            this.stringMibJson = this.mibJObject.ToString();
            return;
        }

        private EnumTableLeafType IsTable(string currentRow, string nextRow)
        {
            if (0 == string.Compare(currentRow, "True"))
            {
                return EnumTableLeafType.leaf;
            }
            else if ((0 == string.Compare(currentRow, "False")) &&
                (0 == string.Compare(nextRow, "True")))
            {
                return EnumTableLeafType.table;
            }
            else
            {
                return EnumTableLeafType.other;
            }
        }
        //input:string propertyName, string varValue
        private JObject TableToJsonData(DataRow rowRec, DataRow rowRecNext)
        {
            int indexNum = 0;
            int isStatic = 1;
            if ("" != rowRecNext["Index1OID"].ToString())
            {
                indexNum++;
            }
            else if ("" != rowRecNext["Index1OID"].ToString())
            {
                indexNum++;
            }
            else if ("" != rowRecNext["Index2OID"].ToString())
            {
                indexNum++;
            }
            else if ("" != rowRecNext["Index3OID"].ToString())
            {
                indexNum++;
            }
            else if ("" != rowRecNext["Index4OID"].ToString())
            {
                indexNum++;
            }
            else if ("" != rowRecNext["Index5OID"].ToString())
            {
                indexNum++;
            }
            else if ("" != rowRecNext["Index6OID"].ToString())
            {
                indexNum++;
            }
            if ("" != rowRec["TableContent"].ToString())
            {
                //0表示动态表，1表示静态表
                isStatic = 0;
            }
            var table1 = new JObject {{ "nameMib", rowRec["MIBName"].ToString()},
                { "oid", rowRec["OID"].ToString()},
                { "nameCh", rowRec["ChFriendName"].ToString()},
                { "indexNum", indexNum}
            };
            return table1;
        }
        private void LeafToJsonData(JArray childJArray, DataRow rowRec, int leafIndex)
        {
            int isMib = 0;
            int UIType = 0;
            //假MIB不写入
            if(0 == string.Compare("√",rowRec["IsMIB"].ToString()))
            {
                isMib = 1;
            }
            if (0 == string.Compare("enum", rowRec["OMType"].ToString()))
            {
                //单选下拉框
                UIType = 1;
            }
            else if (0 == string.Compare("DateAndTime", rowRec["OMType"].ToString()))
            {
                //日期
                UIType = 2;
            }
            else if (0 == string.Compare("BITS", rowRec["OMType"].ToString()))
            {
                //复选框
                UIType = 3;
            }
            JObject childJObject = new JObject { { "childNameMib", rowRec["MIBName"].ToString()},
                { "childNo", leafIndex},
                { "childOid",rowRec["OID"].ToString()},
                { "childNameCh", rowRec["ChFriendName"].ToString()},
                { "isMib", isMib},
                { "ASNType", rowRec["ASNType"].ToString()},
                { "OMType", rowRec["OMType"].ToString()},
                { "UIType", UIType},
                { "managerValueRange", rowRec["MIBVal_list"].ToString()},
                { "defaultValue", rowRec["DefaultValue"].ToString()},
                { "detailDesc", rowRec["chDetailDesc"].ToString()},
                { "unit", rowRec["MIBVal_Unit"].ToString()}
            };
            childJArray.Add(childJObject);
            return;
        }
        
        static public string RemoveCommOID(string OID)
        {
        	int lastIndex = OID.LastIndexOf("1.3.6.1.4.1.5105.100");
            string result = OID.Substring(lastIndex + "1.3.6.1.4.1.5105.100".Length);
            Console.WriteLine("RemoveCommOID result is {0}", result);
            return result;
        }
        
        static public string AddCommOID(string OID)
        {
        	string result = "1.3.6.1.4.1.5105.100" + OID;
            Console.WriteLine("AddCommOID result is {0}", result);
            return result;
        }
        static public string GetTotalOID(string OID, int index)
        {
            string temp = AddCommOID(OID);
            string result = temp + "." + index.ToString();
            Console.WriteLine("GetTotalOID result is {0}", result);
            return result;
        }
    }
    public enum EnumTableLeafType
    {
        leaf = -1,
        table = 0,
        other = 1
    }
    
    /*
    class LeafNode
    {
    	public string childNameMib;
    	public int childNo;
    	public string childOid;
    	public string childNameCh;
    	public int isMib;
    	public string ASNType;
    	public string OMType;
    	public int UIType;
    	public string managerValueRange;
    	public string defaultValue;
    	public string detailDesc;
    	public string unit;
    } */
}

