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
        private Dictionary<string, dynamic> table_info_db = null;
        private Dictionary<string, dynamic> table_info_db_list = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 2 ：{key：nameEnglish, value: {"oid":vOid,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// nameEnTableInfo.Add("isLeaf", "0");
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> nameEn_info_db = null;
        private Dictionary<string, dynamic> nameEn_info_db_list = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 3 ：{key：oid, value: {"nameEn":vNameEn,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> oid_info_db = null;
        private Dictionary<string, dynamic> oid_info_db_list = new Dictionary<string, dynamic>();

        /*************************************     开辟内存，存数据      ********************************************/

        /// 初始化，申请存储
        private bool initDbMemory()
        {
            table_info_db = new Dictionary<string, dynamic>();
            nameEn_info_db = new Dictionary<string, Dictionary<string, string>>();
            oid_info_db = new Dictionary<string, Dictionary<string, string>>();
            return true;
        }
        /// 结束后，释放存储
        private bool DelDbMemory()
        {
            table_info_db = null;
            nameEn_info_db = null;
            oid_info_db = null;
            return true;
        }

        /// <summary>
        /// json 文件生产 3种数据库
        /// </summary>
        public bool GeneratedMibInfoList(string ConnectIp)
        {
            initDbMemory();
            ///
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(
                iniFile.getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

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
            //TestWriteListDbFile2();
            if (!AddDBList(ConnectIp))
            {
                Console.WriteLine("add Db list err.");
                return false;
            }
            TestWriteListDbFile2();
            DelDbMemory();
            return true;
        }

        /// <summary>
        /// 把数据库内容，写到文件中，便于查看
        /// </summary>
        void TestWriteListDbFile()
        {
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(iniFile.getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            json.WriteFile(jsonfilepath + "oid_info_db.json", JsonConvert.SerializeObject(oid_info_db));
            json.WriteFile(jsonfilepath + "nameEn_info_db.json", JsonConvert.SerializeObject(nameEn_info_db));
            json.WriteFile(jsonfilepath + "table_info_db.json", JsonConvert.SerializeObject(table_info_db));
        }
        void TestWriteListDbFile2()
        {
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(iniFile.getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            json.WriteFile(jsonfilepath + "oid_info_db2.json", JsonConvert.SerializeObject(oid_info_db_list));
            json.WriteFile(jsonfilepath + "nameEn_info_db2.json", JsonConvert.SerializeObject(nameEn_info_db_list));
            json.WriteFile(jsonfilepath + "table_info_db2.json", JsonConvert.SerializeObject(table_info_db_list));
        }

        /// <summary>
        /// 用表英文名，查询表内容
        /// </summary>
        /// <param name="key">表英文名</param>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public bool getTableInfo(string key,out dynamic tableInfo)
        {
            tableInfo = "";
            //判断键存在
            if (!table_info_db.ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("Table db with Key = ({0}) not exists.", key);
                return false;
            }
            tableInfo = table_info_db[key];
            return true;
        }
        public bool getTableInfo(string key, out dynamic tableInfo, string ConnectIp)
        {
            tableInfo = "";
            //判断键存在
            if (!table_info_db_list.ContainsKey(ConnectIp)) // exist == True 
            {
                Console.WriteLine("Table db with ConnectIp = ({0}) not exists.", ConnectIp);
                return false;
            }
            if (!table_info_db_list[ConnectIp].ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("Table db with Key = ({0}) not exists.", key);
                return false;
            }
            tableInfo = table_info_db_list[ConnectIp][key];
            return true;
        }
        /// <summary>
        /// 用节点的英文名，查询节点信息
        /// </summary>
        /// <param name="key">节点的英文名</param>
        /// <param name="nameInfo"></param>
        /// <returns></returns>
        public bool getNameEnInfo(string key, out dynamic nameInfo)
        {
            nameInfo = "";
            //判断键存在
            if (!nameEn_info_db.ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("NameEn db with Key = ({0}) not exists.", key);
                return false;
            }
            nameInfo = nameEn_info_db[key];
            return true;
        }
        public bool getNameEnInfo(string key, out dynamic nameInfo, string ConnectIp)
        {
            nameInfo = "";
            //判断键存在
            if (!nameEn_info_db_list.ContainsKey(ConnectIp)) // exist == True 
            {
                Console.WriteLine("NameEn db with ConnectIp = ({0}) not exists.", ConnectIp);
                return false;
            }
            if (!nameEn_info_db_list[ConnectIp].ContainsKey(key)) // exist == True 
            {
                Console.WriteLine("NameEn db with Key = ({0}) not exists.", key);
                return false;
            }
            nameInfo = nameEn_info_db_list[ConnectIp][key];
            return true;
        }
        /// <summary>
        /// 用节点的OID，查询节点信息
        /// </summary>
        /// <param name="key">节点OID</param>
        /// <param name="oidInfo"></param>
        /// <returns></returns>
        public bool getOidEnInfo(string key, out dynamic oidInfo)
        {
            oidInfo = "";
            string prefixStr = "1.3.6.1.4.1.5105.1.";

            // 处理1. 去前缀
            string keyNew = key.Replace(prefixStr,"");

            int indexNum = 0;
            string findKey = keyNew;
            while (findKey.Count(ch => ch == '.') > 4)
            {
                if (!oid_info_db.ContainsKey(findKey))
                {
                    findKey = findKey.Substring(0, findKey.LastIndexOf("."));
                    indexNum += 1;
                }
                else
                {
                    oidInfo = oid_info_db[findKey];
                    break;
                }
            }

            if (oidInfo.Equals(""))
                return false;
            else if (indexNum !=  int.Parse(oidInfo["indexNum"]))
                return false;
            return true;
        }
        public bool getOidEnInfo(string key, out dynamic oidInfo, string ConnectIp)
        {
            oidInfo = "";
            if (!oid_info_db_list.ContainsKey(ConnectIp)) // exist == True 
            {
                Console.WriteLine("NameEn db with ConnectIp = ({0}) not exists.", ConnectIp);
                return false;
            }
            var oid_info_db = oid_info_db_list[ConnectIp];

            // 处理1. 去前缀
            int indexNum = 0;
            string findKey = key.Replace("1.3.6.1.4.1.5105.1.", "");
            while (findKey.Count(ch => ch == '.') > 4)
            {
                if (!oid_info_db.ContainsKey(findKey))
                {
                    findKey = findKey.Substring(0, findKey.LastIndexOf("."));
                    indexNum += 1;
                }
                else
                {
                    oidInfo = oid_info_db[findKey];
                    break;
                }
            }

            if (oidInfo.Equals(""))
                return false;
            else if (indexNum != int.Parse(oidInfo["indexNum"]))
                return false;
            return true;
        }

        //创建数据库
        public bool CreateNameEnByTableInfo(JToken table)
        {
            Dictionary<string, string> nameEnTableInfo = new Dictionary<string, string>();

            nameEnTableInfo.Add("isLeaf", "0");
            nameEnTableInfo.Add("oid", table["oid"].ToString());
            nameEnTableInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnTableInfo.Add("nameCh", table["nameCh"].ToString());

            this.nameEn_info_db.Add(table["nameMib"].ToString(), nameEnTableInfo);
            return true;
        }
        public bool CreateNameEnByChildInfo(JToken child, JToken table)
        {
            Dictionary<string, string> nameEnChildInfo = new Dictionary<string, string>();

            nameEnChildInfo.Add("tableNameEn", table["nameMib"].ToString());
            nameEnChildInfo.Add("isLeaf", "1");
            nameEnChildInfo.Add("oid", child["childOid"].ToString());
            nameEnChildInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnChildInfo.Add("nameCh", child["childNameCh"].ToString());
            try
            {
                this.nameEn_info_db.Add(child["childNameMib"].ToString(), nameEnChildInfo);
            }
            catch (Exception ex)
            {
                this.nameEn_info_db[child["childNameMib"].ToString()]["oid"] =
                    this.nameEn_info_db[child["childNameMib"].ToString()]["oid"] +
                        "|" + child["childOid"].ToString();
                Console.WriteLine("生成json_db2时{0},{1}", child["childNameMib"].ToString(), ex.Message);
            }
            return true;
        }
        public bool CreateOidByTableInfo(JToken table)
        {
            Dictionary<string, string> oidTableInfo = new Dictionary<string, string>();

            oidTableInfo.Add("isLeaf", "0");
            oidTableInfo.Add("nameMib", table["nameMib"].ToString());
            oidTableInfo.Add("indexNum", table["indexNum"].ToString());
            oidTableInfo.Add("nameCh", table["nameCh"].ToString());
            this.oid_info_db.Add(table["oid"].ToString(), oidTableInfo);
            return true;
        }
        public bool CreateOidByChildInfo(JToken child, JToken table)
        {
            Dictionary<string, string> oidChildInfo = new Dictionary<string, string>();

            oidChildInfo.Add("isLeaf", "1");
            oidChildInfo.Add("nameMib", child["childNameMib"].ToString());
            oidChildInfo.Add("indexNum", table["indexNum"].ToString());
            oidChildInfo.Add("nameCh", child["childNameCh"].ToString());

            this.oid_info_db.Add(child["childOid"].ToString(), oidChildInfo);
            return true;
        }
        public bool CreateTableByTableInfo(JToken table)
        {
            this.table_info_db.Add(table["nameMib"].ToString(), table);
            return true;
        }

        //增加/删除数据库列表
        bool AddDBList(string ConnectIp)
        {
            try
            {
                table_info_db_list.Add(ConnectIp, table_info_db);
                nameEn_info_db_list.Add(ConnectIp, nameEn_info_db);
                oid_info_db_list.Add(ConnectIp, oid_info_db);

                table_info_db = null;
            }
            catch {
                return false;
            }
            return true;
        }
        bool DelDBList(string ConnectIp)
        {
            try
            {
                table_info_db_list.Remove(ConnectIp);
                nameEn_info_db_list.Remove(ConnectIp);
                oid_info_db_list.Remove(ConnectIp);
            }
            catch
            {
                return false;
            }
            return true;
        }


        /********************************    私有，原来的代码     ********************************/
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
            //TestWriteListDbFile2();
            return;
        }

        /// <summary>
        /// json 文件生产 3种数据库
        /// </summary>
        private void GeneratedMibInfoList(bool useOld)
        {
            initDbMemory();
            ///
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(
                iniFile.getIniFilePath("JsonDataMgr.ini"),
                "JsonFileInfo", "jsonfilepath");

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
                this.nameEn_info_db.Add(tableName, nameEnTableInfo);

                oidTableInfo.Add("isLeaf", "0");
                oidTableInfo.Add("nameMib", tableName);
                oidTableInfo.Add("indexNum", tableIndexNum);
                oidTableInfo.Add("nameCh", nameCh);
                this.oid_info_db.Add(tableOid, oidTableInfo);

                this.table_info_db.Add(tableName, table);
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
                        this.nameEn_info_db.Add(childName, nameEnChildInfo);
                    }
                    catch (Exception ex)
                    {
                        this.nameEn_info_db[childName]["oid"] = this.nameEn_info_db[childName]["oid"] + "|" + childOid;
                        Console.WriteLine("生成json_db时{0},{1}", childName, ex.Message);
                    }

                    oidChildInfo.Add("isLeaf", "1");
                    oidChildInfo.Add("nameMib", childName);
                    oidChildInfo.Add("indexNum", tableIndexNum);
                    this.oid_info_db.Add(childOid, oidChildInfo);
                }
            }
            TestWriteListDbFile();
            return;
        }
        /********************************    私有，原来的代码     ********************************/

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
        //    foreach (var dbkey in nameEn_info_db.Keys)
        //    {
        //        var db1 = nameEn_info_db[dbkey];
        //        var db2 = nameEn_info_db2[dbkey];
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
        //    foreach (var dbkey in oid_info_db.Keys)
        //    {
        //        var db1 = oid_info_db[dbkey];
        //        var db2 = oid_info_db2[dbkey];
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
        //    foreach (var dbkey in table_info_db.Keys)
        //    {
        //        var db1 = table_info_db[dbkey];
        //        var db2 = table_info_db2[dbkey];

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
    }
}
