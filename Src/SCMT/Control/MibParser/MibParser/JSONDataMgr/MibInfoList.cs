using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MIBDataParser.JSONDataMgr
{
    class MibInfoList
    {
        Dictionary<string, Dictionary<string, dynamic>> test;
        /// <summary>
        /// 数据库 1 ：{key：tableName, value: 表的所有信息(包括叶子节点)}
        /// </summary>
        Dictionary<string, dynamic> table_info_db = new Dictionary<string, dynamic>();
        Dictionary<string, dynamic> table_info_db2 = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 2 ：{key：nameEnglish, value: {"oid":vOid,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// nameEnTableInfo.Add("isLeaf", "0");
        /// </summary>
        Dictionary<string, Dictionary<string, string>> nameEn_info_db = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, Dictionary<string, string>> nameEn_info_db2 = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 数据库 3 ：{key：oid, value: {"nameEn":vNameEn,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// </summary>
        Dictionary<string, Dictionary<string, string>> oid_info_db = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, Dictionary<string, string>> oid_info_db2 = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// json 文件生产 3种数据库
        /// </summary>
        public void GeneratedMibInfoListOld()
        {          ///
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

        public void GeneratedMibInfoList()
        {          ///
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(iniFile.getIniFilePath(
                "JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            JObject JObj = json.ReadJsonFileForJObject(jsonfilepath + "mib.json");
            foreach (var table in JObj["tableList"])
            {
                getNameEnByTableInfo(table);
                getOidByTableInfo(table);

                this.table_info_db2.Add(table["nameMib"].ToString(), table);
                foreach (var child in table["childList"])
                {
                    getNameEnByChildInfo(child, table);

                    getOidByChildInfo(child, table);
                }
            }
            //TestWriteListDbFile2();
            return;
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
            json.WriteFile(jsonfilepath + "oid_info_db2.json", JsonConvert.SerializeObject(oid_info_db));
            json.WriteFile(jsonfilepath + "nameEn_info_db2.json", JsonConvert.SerializeObject(nameEn_info_db));
            json.WriteFile(jsonfilepath + "table_info_db2.json", JsonConvert.SerializeObject(table_info_db));
        }

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

        //
        public bool getNameEnByTableInfo(JToken table)
        {
            Dictionary<string, string> nameEnTableInfo = new Dictionary<string, string>();

            nameEnTableInfo.Add("isLeaf", "0");
            nameEnTableInfo.Add("oid", table["oid"].ToString());
            nameEnTableInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnTableInfo.Add("nameCh", table["nameCh"].ToString());

            this.nameEn_info_db2.Add(table["nameMib"].ToString(), nameEnTableInfo);
            return true;
        }
        public bool getNameEnByChildInfo(JToken child, JToken table)
        {
            Dictionary<string, string> nameEnChildInfo = new Dictionary<string, string>();

            nameEnChildInfo.Add("tableNameEn", table["nameMib"].ToString());
            nameEnChildInfo.Add("isLeaf", "1");
            nameEnChildInfo.Add("oid", child["childOid"].ToString());
            nameEnChildInfo.Add("indexNum", table["indexNum"].ToString());
            nameEnChildInfo.Add("nameCh", child["childNameCh"].ToString());
            try
            {
                this.nameEn_info_db2.Add(child["childNameMib"].ToString(), nameEnChildInfo);
            }
            catch (Exception ex)
            {
                this.nameEn_info_db2[child["childNameMib"].ToString()]["oid"] =
                    this.nameEn_info_db2[child["childNameMib"].ToString()]["oid"] +
                        "|" + child["childOid"].ToString();
                Console.WriteLine("生成json_db2时{0},{1}", child["childNameMib"].ToString(), ex.Message);
            }
            return true;
        }
        public bool getOidByTableInfo(JToken table)
        {
            Dictionary<string, string> oidTableInfo = new Dictionary<string, string>();

            oidTableInfo.Add("isLeaf", "0");
            oidTableInfo.Add("nameMib", table["nameMib"].ToString());
            oidTableInfo.Add("indexNum", table["indexNum"].ToString());
            oidTableInfo.Add("nameCh", table["nameCh"].ToString());
            this.oid_info_db2.Add(table["oid"].ToString(), oidTableInfo);
            return true;
        }
        public bool getOidByChildInfo(JToken child, JToken table)
        {
            Dictionary<string, string> oidChildInfo = new Dictionary<string, string>();

            oidChildInfo.Add("isLeaf", "1");
            oidChildInfo.Add("nameMib", child["childNameMib"].ToString());
            oidChildInfo.Add("indexNum", table["indexNum"].ToString());
            oidChildInfo.Add("nameCh", child["childNameCh"].ToString());

            this.oid_info_db2.Add(child["childOid"].ToString(), oidChildInfo);
            return true;
        }

        public void TestDBInfoList()
        {
            if (!testDB_nameEn_info())
                Console.WriteLine("nameEn is not same.");

            if (!testDb_oid_info())
                Console.WriteLine("oid is not same.");

            if (!testDb_table_info())
                Console.WriteLine("table is not same.");
        }
        bool testDB_nameEn_info()
        {
            foreach (var dbkey in nameEn_info_db.Keys)
            {
                var db1 = nameEn_info_db[dbkey];
                var db2 = nameEn_info_db2[dbkey];
                foreach (var key in db1.Keys)
                {
                    if (db1[key] != db2[key])
                    {
                        Console.WriteLine("NameEnDb is not same.ikey=({0}),info1=({1}),info2=({2})", key, db1[key], db2[key]);
                        return false;
                    }
                }
            }
            return true;
        }
        bool testDb_oid_info()
        {
            foreach (var dbkey in oid_info_db.Keys)
            {
                var db1 = oid_info_db[dbkey];
                var db2 = oid_info_db2[dbkey];
                foreach (var key in db1.Keys)
                {
                    if (db1[key] != db2[key])
                    {
                        Console.WriteLine("OidDb is not same.ikey=({0}),info1=({1}),info2=({2}).", key, db1[key], db2[key]);
                        return false;
                    }
                }
            }
            return true;
        }
        bool testDb_table_info()
        {
            foreach (var dbkey in table_info_db.Keys)
            {
                var db1 = table_info_db[dbkey];
                var db2 = table_info_db2[dbkey];

                string str1 = db1.ToString();
                string str2 = db2.ToString();
                if (!String.Equals(str1, str2))
                {
                    Console.WriteLine("TableDb is not same.key=({0}).", dbkey);
                    return false;
                }

            }
            return true;
        }
    }
}
