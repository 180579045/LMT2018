using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MIBDataParser.JSONDataMgr
{
    class MibInfoList
    {
        /*************************************     开辟内存，存数据      ********************************************/
        /// <summary>
        /// 数据库 1 ：{key：tableName, value: 表的所有信息(包括叶子节点)}
        /// </summary>
        private Dictionary<string, dynamic> tableInfoDb = null;
        private Dictionary<string, dynamic> tableInfoDbList = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 2 ：{key：nameEnglish, value: {"oid":vOid,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// nameEnTableInfo.Add("isLeaf", "0");
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> nameEnInfoDb = null;
        private Dictionary<string, dynamic> nameEnInfoDbList = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 3 ：{key：oid, value: {"nameEn":vNameEn,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> oidInfoDb = null;
        private Dictionary<string, dynamic> oidInfoDbList = new Dictionary<string, dynamic>();

        /*************************************     开辟内存，存数据      ********************************************/


        /*************************************        公共接口实现       ********************************************/

        /// <summary>
        /// json 文件生产 3种数据库
        /// </summary>
        /// <param name="ConnectIp">标识数据归属</param>
        /// <returns></returns>        
        public bool GeneratedMibInfoList(string ConnectIp)
        {
            ///初始化
            initDbMemory();
            ///
            string jsonfilepath = new ReadIniFile().IniReadValue(
                new ReadIniFile().getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");
            JObject JObj = new JsonFile().ReadJsonFileForJObject(jsonfilepath + "mib.json");
            foreach (var table in JObj["tableList"])
            {
                CreateNameEnByTableInfo(table);
                CreateOidByTableInfo(table);
                CreateTableByTableInfo(table);
                foreach (var child in table["childList"])
                {
                    CreateNameEnByChildInfo(child, table);
                    CreateOidByChildInfo(child, table);
                }
            }

            if (!AddDBList(ConnectIp))
            {
                return false;
            }
            MibWriteDBListToJsonForRead("test");
            DelDbMemory();
            return true;
        }

        /// <summary>
        /// 用表英文名，查询表内容
        /// </summary>  
        /// <param name="key">表英文名</param>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public bool getTableInfo(string key, out dynamic tableInfo, string ConnectIp)
        {
            tableInfo = "";
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
        [Obsolete("Use Method bool getNameEnInfo(string key, out dynamic nameInfo, string ConnectIp); instead", true)]
        public bool getNameEnInfo(string key, out dynamic nameInfo)
        {
            nameInfo = "";
            //判断键存在
            if (!nameEnInfoDb.ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("NameEn db with Key = ({0}) not exists.", key);
                return false;
            }
            nameInfo = nameEnInfoDb[key];
            return true;
        }

        /// <summary>
        /// 用节点的英文名，查询节点信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="nameInfo"></param>
        /// <param name="ConnectIp"></param>
        /// <returns></returns>
        public bool getNameEnInfo(string key, out dynamic nameInfo, string ConnectIp)
        {
            nameInfo = "";
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
        private bool getOidEnInfo(string key, out dynamic oidInfo)
        {
            oidInfo = "";
            string prefixStr = "1.3.6.1.4.1.5105.1.";

            // 处理1. 去前缀
            string keyNew = key.Replace(prefixStr, "");

            int indexNum = 0;
            string findKey = keyNew;
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
                    break;
                }
            }

            if (oidInfo.Equals(""))
                return false;
            else if (indexNum != int.Parse(oidInfo["indexNum"]))
                return false;
            return true;
        }
        public bool getOidEnInfo(string key, out dynamic oidInfo, string ConnectIp)
        {
            oidInfo = "";
            if (!oidInfoDbList.ContainsKey(ConnectIp)) // exist == True 
            {
                Console.WriteLine("NameEn db with ConnectIp = ({0}) not exists.", ConnectIp);
                return false;
            }
            var oidInfoDb = oidInfoDbList[ConnectIp];

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
                    break;
                }
            }

            if (oidInfo.Equals(""))
                return false;
            else if (indexNum != int.Parse(oidInfo["indexNum"]))
                return false;
            return true;
        }

        /*************************************        公共接口实现       ********************************************/

        /*************************************        私有实现代码       ********************************************/
        //创建数据库
        private bool CreateNameEnByTableInfo(JToken table)
        {
            Dictionary<string, string> nameEnTableInfo = new Dictionary<string, string>();

            nameEnTableInfo.Add("isLeaf", "0");
            nameEnTableInfo.Add("oid", table["oid"].ToString());
            nameEnTableInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnTableInfo.Add("nameCh", table["nameCh"].ToString());

            this.nameEnInfoDb.Add(table["nameMib"].ToString(), nameEnTableInfo);
            return true;
        }
        private bool CreateNameEnByChildInfo(JToken child, JToken table)
        {
            Dictionary<string, string> nameEnChildInfo = new Dictionary<string, string>();

            nameEnChildInfo.Add("tableNameEn", table["nameMib"].ToString());
            nameEnChildInfo.Add("isLeaf", "1");
            nameEnChildInfo.Add("oid", child["childOid"].ToString());
            nameEnChildInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnChildInfo.Add("nameCh", child["childNameCh"].ToString());
            try
            {
                this.nameEnInfoDb.Add(child["childNameMib"].ToString(), nameEnChildInfo);
            }
            catch (Exception ex)
            {
                this.nameEnInfoDb[child["childNameMib"].ToString()]["oid"] =
                    this.nameEnInfoDb[child["childNameMib"].ToString()]["oid"] +
                        "|" + child["childOid"].ToString();
                Console.WriteLine("生成json_db2时{0},{1}", child["childNameMib"].ToString(), ex.Message);
            }
            return true;
        }
        private bool CreateOidByTableInfo(JToken table)
        {
            Dictionary<string, string> oidTableInfo = new Dictionary<string, string>();

            oidTableInfo.Add("isLeaf", "0");
            oidTableInfo.Add("nameMib", table["nameMib"].ToString());
            oidTableInfo.Add("indexNum", table["indexNum"].ToString());
            oidTableInfo.Add("nameCh", table["nameCh"].ToString());
            this.oidInfoDb.Add(table["oid"].ToString(), oidTableInfo);
            return true;
        }
        private bool CreateOidByChildInfo(JToken child, JToken table)
        {
            Dictionary<string, string> oidChildInfo = new Dictionary<string, string>();

            oidChildInfo.Add("isLeaf", "1");
            oidChildInfo.Add("nameMib", child["childNameMib"].ToString());
            oidChildInfo.Add("indexNum", table["indexNum"].ToString());
            oidChildInfo.Add("nameCh", child["childNameCh"].ToString());

            this.oidInfoDb.Add(child["childOid"].ToString(), oidChildInfo);
            return true;
        }
        private bool CreateTableByTableInfo(JToken table)
        {
            this.tableInfoDb.Add(table["nameMib"].ToString(), table);
            return true;
        }

        /// 初始化，申请存储
        private bool initDbMemory()
        {
            tableInfoDb = new Dictionary<string, dynamic>();
            nameEnInfoDb = new Dictionary<string, Dictionary<string, string>>();
            oidInfoDb = new Dictionary<string, Dictionary<string, string>>();
            return true;
        }
        /// 结束后，释放存储
        private bool DelDbMemory()
        {
            tableInfoDb = null;
            nameEnInfoDb = null;
            oidInfoDb = null;
            return true;
        }
        //增加/删除数据库列表
        private bool AddDBList(string ConnectIp)
        {
            try
            {
                tableInfoDbList.Add(ConnectIp, tableInfoDb);
                nameEnInfoDbList.Add(ConnectIp, nameEnInfoDb);
                oidInfoDbList.Add(ConnectIp, oidInfoDb);

                tableInfoDb = null;
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
        /// json 文件生产 3种数据库
        /// </summary>
        private void GeneratedMibInfoList(bool useOld)
        {
            initDbMemory();
            ///
            string jsonfilepath = new ReadIniFile().IniReadValue(
                new ReadIniFile().getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            JObject JObj = json.ReadJsonFileForJObject(jsonfilepath + "mib.json");
            foreach (var table in JObj["tableList"])
            {
                Dictionary<string, string> nameEnTableInfo = new Dictionary<string, string>();
                Dictionary<string, string> oidTableInfo = new Dictionary<string, string>();
                string tableName = table["nameMib"].ToString();
                string tableOid = table["oid"].ToString();
                string tableIndexNum = table["indexNum"].ToString();
                string nameCh = table["nameCh"].ToString();
                dynamic childList = table["childList"];

                nameEnTableInfo.Add("isLeaf", "0");
                nameEnTableInfo.Add("oid", tableOid);
                nameEnTableInfo.Add("indexNum", tableIndexNum);
                nameEnTableInfo.Add("nameCh", nameCh);
                this.nameEnInfoDb.Add(tableName, nameEnTableInfo);

                oidTableInfo.Add("isLeaf", "0");
                oidTableInfo.Add("nameMib", tableName);
                oidTableInfo.Add("indexNum", tableIndexNum);
                oidTableInfo.Add("nameCh", nameCh);
                this.oidInfoDb.Add(tableOid, oidTableInfo);

                this.tableInfoDb.Add(tableName, table);
                foreach (var child in childList)
                {
                    Dictionary<string, string> nameEnChildInfo = new Dictionary<string, string>();
                    Dictionary<string, string> oidChildInfo = new Dictionary<string, string>();
                    string childName = child["childNameMib"];
                    string childOid = child["childOid"];
                    string childNameCh = child["childNameCh"];

                    nameEnChildInfo.Add("tableNameEn", tableName);
                    nameEnChildInfo.Add("isLeaf", "1");
                    nameEnChildInfo.Add("oid", childOid);
                    nameEnChildInfo.Add("indexNum", tableIndexNum);
                    nameEnChildInfo.Add("nameCh", childNameCh);

                    try
                    {
                        this.nameEnInfoDb.Add(childName, nameEnChildInfo);
                    }
                    catch (Exception ex)
                    {
                        this.nameEnInfoDb[childName]["oid"] = this.nameEnInfoDb[childName]["oid"] + "|" + childOid;
                        Console.WriteLine("生成json_db时{0},{1}", childName, ex.Message);
                    }

                    oidChildInfo.Add("isLeaf", "1");
                    oidChildInfo.Add("nameMib", childName);
                    oidChildInfo.Add("indexNum", tableIndexNum);
                    this.oidInfoDb.Add(childOid, oidChildInfo);
                }
            }
            MibWriteDBListToJsonForRead("");
            return;
        }


        /// <summary>
        /// 用表英文名，查询表内容
        /// </summary>
        /// <param name="key">表英文名</param>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        private bool getTableInfo(string key, out dynamic tableInfo)
        {
            tableInfo = "";
            //判断键存在
            if (!tableInfoDb.ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("Table db with Key = ({0}) not exists.", key);
                return false;
            }
            tableInfo = tableInfoDb[key];
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
        /*************************************        私有实现代码       ********************************************/



        /*************************************      私有，原来的代码     ********************************************/

        /// <summary>
        /// json 文件生产 3种数据库
        /// </summary>
        private void GeneratedMibInfoList()
        {
            initDbMemory();
            ///
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(iniFile.getIniFilePath(
                "JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            JObject JObj = json.ReadJsonFileForJObject(jsonfilepath + "mib.json");
            foreach (var table in JObj["tableList"])
            {
                CreateNameEnByTableInfo(table);
                CreateOidByTableInfo(table);
                CreateTableByTableInfo(table);

                foreach (var child in table["childList"])
                {
                    CreateNameEnByChildInfo(child, table);
                    CreateOidByChildInfo(child, table);
                }
            }
            //MibWriteDBListToJsonForRead2();
            return;
        }


        //public void TestDBInfoList()
        //{
        //    if (!testDB_nameEn_info())
        //        Console.WriteLine("nameEn is not same.");

        //    if (!testDb_oid_info())
        //        Console.WriteLine("oid is not same.");

        //    if (!testDb_table_info())
        //        Console.WriteLine("table is not same.");
        //}
        //bool testDB_nameEn_info()
        //{
        //    foreach (var dbkey in nameEnInfoDb.Keys)
        //    {
        //        var db1 = nameEnInfoDb[dbkey];
        //        var db2 = nameEnInfoDb2[dbkey];
        //        foreach (var key in db1.Keys)
        //        {
        //            if (db1[key] != db2[key])
        //            {
        //                Console.WriteLine("NameEnDb is not same.ikey=({0}),info1=({1}),info2=({2})", key, db1[key], db2[key]);
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        //bool testDb_oid_info()
        //{
        //    foreach (var dbkey in oidInfoDb.Keys)
        //    {
        //        var db1 = oidInfoDb[dbkey];
        //        var db2 = oidInfoDb2[dbkey];
        //        foreach (var key in db1.Keys)
        //        {
        //            if (db1[key] != db2[key])
        //            {
        //                Console.WriteLine("OidDb is not same.ikey=({0}),info1=({1}),info2=({2}).", key, db1[key], db2[key]);
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        //bool testDb_table_info()
        //{
        //    foreach (var dbkey in tableInfoDb.Keys)
        //    {
        //        var db1 = tableInfoDb[dbkey];
        //        var db2 = tableInfoDb2[dbkey];

        //        string str1 = db1.ToString();
        //        string str2 = db2.ToString();
        //        if (!String.Equals(str1, str2))
        //        {
        //            Console.WriteLine("TableDb is not same.key=({0}).", dbkey);
        //            return false;
        //        }

        //    }
        //    return true;
        //}
        /*************************************      私有，原来的代码     ********************************************/
    }
}
