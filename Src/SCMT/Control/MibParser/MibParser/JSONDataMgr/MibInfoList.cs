using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;

namespace MIBDataParser.JSONDataMgr
{
    class NameEnInfo
    {
        public string m_oid;
        public bool m_isLeaf;
        public int m_indexNum;
        public string m_nameCh;
        public string m_tableNameEn;
        public List<NameEnInfo> m_sameNameEn;

        public NameEnInfo(){}
        public NameEnInfo(bool isLeaf, dynamic table, dynamic child)
        {
            this.m_isLeaf = isLeaf;
            this.m_indexNum = int.Parse(table["indexNum"].ToString());
            this.m_tableNameEn = table["nameMib"].ToString();
            if (isLeaf)//Leaf
            {
                this.m_oid    = child["childOid"].ToString();
                this.m_nameCh = child["childNameCh"].ToString();
            }
            else// table
            {
                this.m_oid    = table["oid"].ToString();
                this.m_nameCh = table["nameCh"].ToString();
            }
        }
        public void AddSameNameEnInfo(NameEnInfo nameInfo)
        {
            if (this.m_sameNameEn == null)
            {
                this.m_sameNameEn = new List<NameEnInfo>();
            }
            this.m_sameNameEn.Add(nameInfo);
        }
    }
    class OidInfo
    {
        public bool m_isLeaf;
        public int m_indexNum;
        public string m_nameEn;
        public string m_nameCh;
        public string m_tableNameEn;

        public OidInfo() { }
        public OidInfo(bool isLeaf, dynamic table, dynamic child)
        {
            this.m_isLeaf = isLeaf;
            this.m_indexNum = int.Parse(table["indexNum"].ToString());
            this.m_tableNameEn = table["nameMib"].ToString();
            if (isLeaf){
                this.m_nameEn = child["childNameMib"].ToString();
                this.m_nameCh = child["childNameCh"].ToString();
            }
            else {
                this.m_nameEn = table["nameMib"].ToString();
                this.m_nameCh = table["nameCh"].ToString();
            }
        }
    }
    class LeafInfo
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
        public int leafProperty;
        public string unit;
        public string IsIndex;
        public string mibSyntax;
        public string mibDesc;
    }
    class TableInfo
    {
        public string nameMib;
        public string oid;
        public string nameCh;
        public int indexNum;
        public string mibSyntax;
        public string mibDesc;
        public List<LeafInfo> childList = new List<LeafInfo>();
    }

    class MibInfoList
    {
        bool isTableDbOK = false;
        bool isNameEnDbOK = false;
        bool isOidDbOK = false;
        Dictionary<string, dynamic> input;
        /*************************************     开辟内存，存数据      ********************************************/
        /// <summary>
        /// 数据库 1 ：{key：tableName, value: 表的所有信息(包括叶子节点)}
        /// </summary>
        private Dictionary<string, dynamic> tableInfoDbList = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 2 ：{key：nameEnglish, value: {"oid":vOid,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// nameEnTableInfo.Add("isLeaf", "0");
        /// </summary>
        private Dictionary<string, dynamic> nameEnInfoDbList = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 3 ：{key：oid, value: {"nameEn":vNameEn,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// </summary>
        private Dictionary<string, dynamic> oidInfoDbList = new Dictionary<string, dynamic>();
        /*************************************     开辟内存，存数据      ********************************************/


        /*************************************        公共接口实现       ********************************************/

        /// <summary>
        /// json 文件生产 3种数据库
        /// </summary>
        /// <param name="ConnectIp">标识数据归属</param>
        /// <returns></returns>        
        public bool GeneratedMibInfoListOld(string ConnectIp)
        {
            ///初始化
            Dictionary<string, dynamic> tableInfoDb = new Dictionary<string, dynamic>();
            Dictionary<string, Dictionary<string, string>> nameEnInfoDb = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, Dictionary<string, string>> oidInfoDb = new Dictionary<string, Dictionary<string, string>>();

            ///
            string jsonfilepath = new ReadIniFile().IniReadValue(
                new ReadIniFile().getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");
            JObject JObj = new JsonFile().ReadJsonFileForJObject(jsonfilepath + "mib.json");
            foreach (var table in JObj["tableList"])
            {
                CreateNameEnByTableInfo(nameEnInfoDb, table);
                CreateOidByTableInfo(oidInfoDb, table);
                CreateTableByTableInfo(tableInfoDb, table);
                foreach (var child in table["childList"])
                {
                    CreateNameEnByChildInfo(nameEnInfoDb, child, table);
                    CreateOidByChildInfo(oidInfoDb, child, table);
                }
            }

            if ((false == AddDBList(ConnectIp, tableInfoDbList, tableInfoDb) )
                || (false == AddDBList(ConnectIp, nameEnInfoDbList, nameEnInfoDb))
                || (false == AddDBList(ConnectIp, oidInfoDbList, oidInfoDb)))
            {
                return false;
            }
            MibWriteDBListToJsonForRead("test");
            return true;
        }

        public bool GeneratedMibInfoList(string ConnectIp)
        {
            ///初始化
            Dictionary<string, TableInfo> tableInfoDb = new Dictionary<string, TableInfo>();
            Dictionary<string, NameEnInfo> nameEnInfoDb = new Dictionary<string, NameEnInfo>();
            Dictionary<string, OidInfo> oidInfoDb = new Dictionary<string, OidInfo>();

            ///
            string jsonfilepath = new ReadIniFile().IniReadValue(
                new ReadIniFile().getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");
            JObject JObj = new JsonFile().ReadJsonFileForJObject(jsonfilepath + "mib.json");
            foreach (var table in JObj["tableList"])
            {
                CreateNameEnByTableInfoNew(nameEnInfoDb, table);
                CreateOidInfoNew( false, oidInfoDb, null, table);
                CreateTableByTableInfoNew(tableInfoDb, table);
                foreach (var child in table["childList"])
                {
                    CreateNameEnByChildInfoNew(nameEnInfoDb, child, table);
                    CreateOidInfoNew( true, oidInfoDb, child, table);
                }
            }

            if ((false == AddDBList(ConnectIp, tableInfoDbList, tableInfoDb))
                || (false == AddDBList(ConnectIp, nameEnInfoDbList, nameEnInfoDb))
                || (false == AddDBList(ConnectIp, oidInfoDbList, oidInfoDb)))
            {
                return false;
            }
            //MibWriteDBListToJsonForRead("0608");
            //MibWriteDBListToJsonForRead("nameEnInfoDbNew0608", nameEnInfoDb);
            //MibWriteDBListToJsonForRead("oldInfoDbNew0608", oidInfoDb);
            //MibWriteDBListToJsonForRead("tableInfoDbNew0608", tableInfoDb);

            return true;
        }

        public bool GeneratedMibInfoListThread(string ConnectIp)
        {
            isTableDbOK = false;
            isNameEnDbOK = false;
            isOidDbOK = false;

            string jsonfilepath = new ReadIniFile().IniReadValue(
                new ReadIniFile().getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");
            JObject JObj = new JsonFile().ReadJsonFileForJObject(jsonfilepath + "mib.json");
            input = new Dictionary<string, dynamic>() {
                {"Ip", ConnectIp },
                { "value", JObj},
            };

            Thread[] threads = new Thread[] {
                 new Thread(new ThreadStart(CreateTableDbThread)),
                 new Thread(new ThreadStart(CreateNameEnDbThread)),
                 new Thread(new ThreadStart(CreateOidDbThread)),
            };

            foreach (Thread t in threads)
                t.Start();

            while (true)
            {
                if (true == isTableDbOK
                    && true == isNameEnDbOK
                    && true == isOidDbOK)
                {
                    break;
                }
            }
            return true;
        }

        public void CreateTableDbThread()//Dictionary<string, dynamic> input)
        {
            JObject JObj = input["value"];
            string ip = input["Ip"];
            Dictionary<string, TableInfo> tableInfoDb = new Dictionary<string, TableInfo>();
            foreach (var table in JObj["tableList"])
            {
                CreateTableByTableInfoNew(tableInfoDb, table);
            }
            tableInfoDbList.Add(ip, tableInfoDb);
            isTableDbOK = true;
        }
        private void CreateNameEnDbThread()//Dictionary<string, dynamic> input)
        {
            JObject JObj = input["value"];
            string ip = input["Ip"];
            Dictionary<string, NameEnInfo> nameEnInfoDb = new Dictionary<string, NameEnInfo>();
            foreach (var table in JObj["tableList"])
            {
                CreateNameEnByTableInfoNew(nameEnInfoDb, table);
                
                foreach (var child in table["childList"])
                {
                    CreateNameEnByChildInfoNew(nameEnInfoDb, child, table);
                }
            }
            nameEnInfoDbList.Add(ip, nameEnInfoDb);
            isNameEnDbOK = true;
        }
        private void CreateOidDbThread()//Dictionary<string, dynamic> input)
        {
            JObject JObj = input["value"];
            string ip = input["Ip"];
            Dictionary<string, OidInfo> oidInfoDb = new Dictionary<string, OidInfo>();
            foreach (var table in JObj["tableList"])
            {
                CreateOidInfoNew(false, oidInfoDb, null, table);
                foreach (var child in table["childList"])
                {
                    CreateOidInfoNew(true, oidInfoDb, child, table);
                }
            }
            oidInfoDbList.Add(ip, oidInfoDb);
            isOidDbOK = true;
        }

        /// <summary>
        /// 用表英文名，查询表内容
        /// </summary>  
        /// <param name="key">表英文名</param>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public bool getTableInfo(string key, out TableInfo tableInfo, string ConnectIp)
        {
            tableInfo = null;
            //判断键存在
            if (!tableInfoDbList.ContainsKey(ConnectIp)) // exist == True 
            {
                Console.WriteLine("Table db with ConnectIp = ({0}) not exists.", ConnectIp);
                return false;
            }
            if (!tableInfoDbList[ConnectIp].ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("Table db with Key = ({0}) not exists.", key);
                return false;
            }
            tableInfo = tableInfoDbList[ConnectIp][key];
            return true;
        }

        /// <summary>
        /// 用节点的英文名，查询节点信息
        /// </summary>
        /// <param name="key">节点的英文名</param>
        /// <param name="nameInfo"></param>
        /// <returns></returns>
        [Obsolete("Use Method bool getNameEnInfo(string key, out NameEnInfo nameInfo, string ConnectIp); instead", true)]
        public bool getNameEnInfo(string key, out dynamic nameInfo)
        {
            nameInfo = "";
            ////判断键存在
            //if (!nameEnInfoDb.ContainsKey(key)) // exist == True 
            //{
            //    Console.WriteLine("NameEn db with Key = ({0}) not exists.", key);
            //    return false;
            //}
            //nameInfo = nameEnInfoDb[key];
            return true;
        }

        /// <summary>
        /// 用节点的英文名，查询节点信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="nameInfo"></param>
        /// <param name="ConnectIp"></param>
        /// <returns></returns>
        public bool getNameEnInfo(string key, out NameEnInfo nameInfo, string ConnectIp)
        {
            nameInfo = null;
            //判断键存在
            if (!nameEnInfoDbList.ContainsKey(ConnectIp)) // exist == True 
            {
                Console.WriteLine("NameEn db with ConnectIp = ({0}) not exists.", ConnectIp);
                return false;
            }
            if (!nameEnInfoDbList[ConnectIp].ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("NameEn db with Key = ({0}) not exists.", key);
                return false;
            }
            nameInfo = nameEnInfoDbList[ConnectIp][key];
            return true;
        }
        /// <summary>
        /// 用节点的OID，查询节点信息
        /// </summary>
        /// <param name="key">节点OID</param>
        /// <param name="oidInfo"></param>
        /// <returns></returns>
        //private bool getOidEnInfo(string key, out dynamic oidInfo)
        //{
        //    oidInfo = "";
        //    //string prefixStr = "1.3.6.1.4.1.5105.1.";

        //    //// 处理1. 去前缀
        //    //string keyNew = key.Replace(prefixStr, "");

        //    //int indexNum = 0;
        //    //string findKey = keyNew;
        //    //while (findKey.Count(ch => ch == '.') > 4)
        //    //{
        //    //    if (!oidInfoDb.ContainsKey(findKey))
        //    //    {
        //    //        findKey = findKey.Substring(0, findKey.LastIndexOf("."));
        //    //        indexNum += 1;
        //    //    }
        //    //    else
        //    //    {
        //    //        oidInfo = oidInfoDb[findKey];
        //    //        break;
        //    //    }
        //    //}

        //    //if (oidInfo.Equals(""))
        //    //    return false;
        //    //else if (indexNum != int.Parse(oidInfo["indexNum"]))
        //    //    return false;
        //    return false;
        //}
        public bool getOidEnInfo(string key, out OidInfo oidInfo, string ConnectIp)
        {
            oidInfo = null;
            if (!oidInfoDbList.ContainsKey(ConnectIp)) // exist == True 
            {
                Console.WriteLine("NameEn db with ConnectIp = ({0}) not exists.", ConnectIp);
                return false;
            }
            Dictionary<string, OidInfo> oidInfoDb = oidInfoDbList[ConnectIp];

            // 处理1. 去前缀
            int indexNum = 0;
            string findKey = key.Replace("1.3.6.1.4.1.5105.1.", "");
            while (findKey.Count(ch => ch == '.') > 4)
            {
                if (!oidInfoDb.ContainsKey(findKey))
                {
                    findKey = findKey.Substring(0, findKey.LastIndexOf("."));
                    indexNum += 1;
                }
                else
                {
                    oidInfo = oidInfoDb[findKey];
                    if (indexNum == oidInfo.m_indexNum)
                        return true;
                }
            }
            return false;
        }

        /*************************************        公共接口实现       ********************************************/

        /*************************************        私有实现代码       ********************************************/
        //创建数据库
        private bool CreateNameEnByTableInfo(Dictionary<string, Dictionary<string, string>>  nameEnInfoDb , JToken table)
        {
            Dictionary<string, string> nameEnTableInfo = new Dictionary<string, string>();
            nameEnTableInfo.Add("tableNameEn", table["nameMib"].ToString());
            nameEnTableInfo.Add("isLeaf", "0");
            nameEnTableInfo.Add("oid", table["oid"].ToString());
            nameEnTableInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnTableInfo.Add("nameCh", table["nameCh"].ToString());

            nameEnInfoDb.Add(table["nameMib"].ToString(), nameEnTableInfo);
            return true;
        }
        private bool CreateNameEnByChildInfo(Dictionary<string, Dictionary<string, string>> nameEnInfoDb, JToken child, JToken table)
        {
            Dictionary<string, string> nameEnChildInfo = new Dictionary<string, string>();

            nameEnChildInfo.Add("tableNameEn", table["nameMib"].ToString());
            nameEnChildInfo.Add("isLeaf", "1");
            nameEnChildInfo.Add("oid", child["childOid"].ToString());
            nameEnChildInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnChildInfo.Add("nameCh", child["childNameCh"].ToString());
            try
            {
                nameEnInfoDb.Add(child["childNameMib"].ToString(), nameEnChildInfo);
            }
            catch (Exception ex)
            {
                nameEnInfoDb[child["childNameMib"].ToString()]["oid"] =
                    nameEnInfoDb[child["childNameMib"].ToString()]["oid"] +
                        "|" + child["childOid"].ToString();
                Console.WriteLine("生成json_db2时{0},{1}", child["childNameMib"].ToString(), ex.Message);
            }
            return true;
        }
        private bool CreateNameEnByTableInfoNew(Dictionary<string, NameEnInfo> nameEnInfoDb, JToken table)
        {
            nameEnInfoDb.Add(table["nameMib"].ToString(), new NameEnInfo(false, table, null));
            return true;
        }
        private bool CreateNameEnByChildInfoNew(Dictionary<string, NameEnInfo> nameEnInfoDb, JToken child, JToken table)
        {
            if (nameEnInfoDb.ContainsKey(child["childNameMib"].ToString()))
            {
                nameEnInfoDb[child["childNameMib"].ToString()].AddSameNameEnInfo(new NameEnInfo(true, table, child));
            }
            else
            {
                nameEnInfoDb.Add(child["childNameMib"].ToString(), new NameEnInfo(true, table, child));
            }
            return true;
        }

        private bool CreateOidByTableInfo(Dictionary<string, Dictionary<string, string>> oidInfoDb, JToken table)
        {
            oidInfoDb.Add(table["oid"].ToString(), new Dictionary<string, string>() {
                { "isLeaf", "0"},
                { "nameMib", table["nameMib"].ToString()},
                { "indexNum", table["indexNum"].ToString()},
                { "nameCh", table["nameCh"].ToString() },
            });
            return true;
        }
        private bool CreateOidByChildInfo(Dictionary<string, Dictionary<string, string>> oidInfoDb, JToken child, JToken table)
        {
            Dictionary<string, string> oidChildInfo = new Dictionary<string, string>();

            oidChildInfo.Add("isLeaf", "1");
            oidChildInfo.Add("nameMib", child["childNameMib"].ToString());
            oidChildInfo.Add("indexNum", table["indexNum"].ToString());
            oidChildInfo.Add("nameCh", child["childNameCh"].ToString());

            oidInfoDb.Add(child["childOid"].ToString(), oidChildInfo);
            return true;
        }
        private bool CreateOidInfoNew(bool isLeaf ,Dictionary<string, OidInfo> oidInfoDb, JToken child, JToken table)
        {
            if(isLeaf)
                oidInfoDb.Add(child["childOid"].ToString(), new OidInfo(isLeaf, table, child) );
            else
                oidInfoDb.Add(table["oid"].ToString(), new OidInfo(isLeaf, table, child));
            return true;
        }

        private bool CreateTableByTableInfo(Dictionary<string, dynamic> tableInfoDb, JToken table)
        {
            tableInfoDb.Add(table["nameMib"].ToString(), table);
            return true;
        }
        private bool CreateTableByTableInfoNew(Dictionary<string, TableInfo> tableInfoDb, JToken table)
        {
            tableInfoDb.Add(table["nameMib"].ToString(), JsonConvert.DeserializeObject<TableInfo>(table.ToString()));
            return true;
        }

        //增加/删除数据库列表
        private bool AddDBList(string ConnectIp, dynamic DbList, dynamic Db)
        {
            try
            {
                DbList.Add(ConnectIp, Db);
            }
            catch
            {
                Console.WriteLine("add {0} Db list err.", ConnectIp);
                return false;
            }
            return true;
        }
        private bool DelDBList(string ConnectIp)
        {
            try
            {
                tableInfoDbList.Remove(ConnectIp);
                nameEnInfoDbList.Remove(ConnectIp);
                oidInfoDbList.Remove(ConnectIp);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 把数据库内容，写到文件中，便于查看
        /// </summary>
        private void MibWriteDBListToJsonForRead(string postfix)
        {
            string jsonFilePath = new ReadIniFile().IniReadValue(
                new ReadIniFile().getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            json.WriteFile(jsonFilePath + string.Format("oidInfoDb_{0}.json", postfix),
                JsonConvert.SerializeObject(oidInfoDbList, Formatting.Indented));
            json.WriteFile(jsonFilePath + string.Format("nameEnInfoDb_{0}.json", postfix),
                JsonConvert.SerializeObject(nameEnInfoDbList, Formatting.Indented));
            json.WriteFile(jsonFilePath + string.Format("tableInfoDb_{0}.json", postfix),
                JsonConvert.SerializeObject(tableInfoDbList, Formatting.Indented));
        }

        private void MibWriteDBListToJsonForRead(string jsonName, dynamic data)
        {
            string jsonFilePath = new ReadIniFile().IniReadValue(
                new ReadIniFile().getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            json.WriteFile(jsonFilePath + string.Format("{0}.json", jsonName),
                JsonConvert.SerializeObject(data, Formatting.Indented));
        }
        /*************************************        私有实现代码       ********************************************/

    }
}
