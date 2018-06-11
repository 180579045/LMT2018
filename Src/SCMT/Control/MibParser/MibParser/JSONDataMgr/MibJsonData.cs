using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json.Linq;


namespace MIBDataParser.JSONDataMgr
{
    class MibJsonData
    {
        //string stringMibJson;
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
            return this.mibJObject.ToString();
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
            JArray childJArray = null;
            int tableNum = MibdateSet.Tables[0].Rows.Count;
            for (int loop = 0; loop < tableNum - 1; loop++)
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
                }
                switch (tableLeafType)
                {
                    case EnumTableLeafType.table:
                        if (!TableToJsonData(row, nextRow, out objRec))
                        {
                            Console.WriteLine("Err:DB err.TableToJsonData,TableRow({0}),but leaf({1}).", row["MIBName"].ToString(), nextRow["MIBName"].ToString());
                        }
                        this.tableNum++;
                        break;
                    case EnumTableLeafType.leaf:
                        if (!LeafToJsonData(objRec, childJArray, row))
                        {
                            //Console.WriteLine("Err:DB err.LeafToJsonData,TableRow({0}),but leaf({1}).", objRec["MIBName"].ToString(), row["MIBName"].ToString());
                        }
                        break;
                    default:
                        break;
                }
            }
            if (!LeafToJsonData(objRec, childJArray, MibdateSet.Tables[0].Rows[tableNum-1])){
                Console.WriteLine("Err:DB err.LeafToJsonData,TableRow({0}),but leaf({1}).", objRec["MIBName"].ToString(), MibdateSet.Tables[0].Rows[tableNum]["MIBName"].ToString());
            }
            objRec.Add("childList", childJArray);
            mibJArray.Add(objRec);

            this.mibJObject.Add("mibVersion", this.mibVersion);
            this.mibJObject.Add("tableNum", this.tableNum);
            this.mibJObject.Add("tableList", mibJArray);

            //this.stringMibJson = this.mibJObject.ToString();
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
        private bool TableToJsonData(DataRow rowRec, DataRow rowRecNext, out JObject table)
        {
            //int isStatic = 1;
            // 两个节点是否有父子关系
            if( !String.Equals( rowRecNext["ParentOID"].ToString(), rowRec["OID"].ToString()))
            {
                table = this.TableToJsonDataResolveTable(rowRec, null);
                return false;
            }
            
            ////判断静动态表
            //if ("" != rowRec["TableContent"].ToString())
            //    isStatic = 0;//0表示动态表，1表示静态表

            table = this.TableToJsonDataResolveTable(rowRec, rowRecNext);
            return true;
        }
        /// <summary>
        /// 解析 table,在TableToJsonData中。
        /// </summary>
        /// <param name="rowRec"></param>
        /// <param name="indexNum"></param>
        /// <returns></returns>
        private JObject TableToJsonDataResolveTable(DataRow rowRec, DataRow rowRecNext)
        {
            return new JObject {
                { "nameMib", rowRec["MIBName"].ToString()},
                { "oid", rowRec["OID"].ToString()},
                { "nameCh", rowRec["ChFriendName"].ToString()},
                { "indexNum", this.TableToJsonDataResolveIndexNum(rowRecNext)},
                { "mibSyntax", rowRec["MIB_Syntax"].ToString()},
                { "mibDesc", rowRec["MIBDesc"].ToString()},
            };
        }
        /// <summary>
        /// 解析索引个数，在TableToJsonData中。
        /// </summary>
        /// <param name="rowRecNext"></param>
        /// <returns></returns>
        private int TableToJsonDataResolveIndexNum(DataRow rowRecNext)
        {
            int indexNum = 0;
            if (null != rowRecNext)
            {
                foreach (var index in new List<string> { "Index1OID", "Index2OID", "Index3OID", "Index4OID", "Index5OID", "Index6OID" })
                {
                    if ("" != rowRecNext[index].ToString())
                    {
                        indexNum++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return indexNum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childJArray"></param>
        /// <param name="rowRec"></param>
        /// <param name="leafIndex"></param>
        private bool LeafToJsonData(JObject table, JArray childJArray, DataRow rowRec)
        {
            int isMib = 0;
            int UIType = 0;

            // 判断父子关系
            if (!rowRec["ParentOID"].ToString().Equals(table["oid"].ToString()))
                return false;

            if (0 == string.Compare("√",rowRec["IsMIB"].ToString()))//假MIB不写入
                isMib = 1;

            if (0 == string.Compare("enum", rowRec["OMType"].ToString()))
                UIType = 1;//单选下拉框
            else if (0 == string.Compare("DateAndTime", rowRec["OMType"].ToString()))
                UIType = 2;//日期
            else if (0 == string.Compare("BITS", rowRec["OMType"].ToString()))
                UIType = 3;//复选框

            //
            int leafIndex = int.Parse(rowRec["OID"].ToString().Replace(rowRec["ParentOID"].ToString()+".", ""));
            JObject childJObject = new JObject {
                { "childNameMib", rowRec["MIBName"].ToString()},
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
                { "leafProperty", 0},//0x0001,查;0x0010,增;0x0100,改;0x1000,删;
                { "unit", rowRec["MIBVal_Unit"].ToString()},
                { "IsIndex", rowRec["IsIndex"].ToString()},
                { "mibSyntax", rowRec["MIB_Syntax"].ToString()},
                { "mibDesc", rowRec["MIBDesc"].ToString()},
            };
            childJArray.Add(childJObject);
            return true;
        }///
        
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

