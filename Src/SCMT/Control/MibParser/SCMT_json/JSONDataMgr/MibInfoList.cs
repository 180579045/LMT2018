using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MIBDataParser.JSONDataMgr
{
    class MibInfoList
    {
        /// <summary>
        /// 数据库 1 ：{key：tableName, value: 表的所有信息(包括叶子节点)}
        /// </summary>
        Dictionary<string, dynamic> table_info_db = new Dictionary<string, dynamic>();

        /// <summary>
        /// 数据库 2 ：{key：nameEnglish, value: {"oid":vOid,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// nameEnTableInfo.Add("isLeaf", "0");
        /// </summary>
        Dictionary<string, Dictionary<string, string>> nameEn_info_db = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 数据库 3 ：{key：oid, value: {"nameEn":vNameEn,"indexNum":vIndexNum,"nameCh":vNameCh }}
        /// </summary>
        Dictionary<string, Dictionary<string, string>> oid_info_db = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// json 文件生产 3种数据库
        /// </summary>
        public void GeneratedMibInfoList()
        {          ///
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(iniFile.getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

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
                nameEn_info_db.Add(tableName, nameEnTableInfo);

                oidTableInfo.Add("isLeaf", "0");
                oidTableInfo.Add("nameMib", tableName);
                oidTableInfo.Add("indexNum", tableIndexNum);
                oidTableInfo.Add("nameCh", nameCh);
                oid_info_db.Add(tableOid, oidTableInfo);

                table_info_db.Add(tableName, table);
                foreach (var child in childList)
                {
                    Dictionary<string, string> nameEnChildInfo = new Dictionary<string, string>();
                    Dictionary<string, string> oidChildInfo = new Dictionary<string, string>();
                    string childName = child["childNameMib"];
                    string childOid = child["childOid"];

                    nameEnChildInfo.Add("isLeaf", "1");
                    nameEnChildInfo.Add("oid", childOid);
                    nameEnChildInfo.Add("indexNum", tableIndexNum);
                    try
                    {
                        nameEn_info_db.Add(childName, nameEnChildInfo);
                    }
                    catch (Exception ex)
                    {
                        nameEn_info_db[childName]["oid"] = nameEn_info_db[childName]["oid"] + "|" + childOid;
                        Console.WriteLine("生成json_db时{0},{1}", childName, ex.Message);
                    }

                    oidChildInfo.Add("isLeaf", "1");
                    oidChildInfo.Add("nameMib", childName);
                    oidChildInfo.Add("indexNum", tableIndexNum);
                    oid_info_db.Add(childOid, oidChildInfo);
                }
            }
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
            json.WriteFile(JsonConvert.SerializeObject(oid_info_db), jsonfilepath + "oid_info_db.json");
            json.WriteFile(JsonConvert.SerializeObject(nameEn_info_db), jsonfilepath + "nameEn_info_db.json");
            json.WriteFile(JsonConvert.SerializeObject(table_info_db), jsonfilepath + "table_info_db.json");
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


    }
}
