using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SCMT_json.JSONDataMgr
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
            string iniFilePath = iniFile.getIniFilePath("JsonDataMgr.ini");
            string jsonfilepath = iniFile.IniReadValue(iniFilePath, "JsonFileInfo", "jsonfilepath");
            string sFilePath = jsonfilepath+ "mib.json";
            FileStream fs = new FileStream(sFilePath, FileMode.Open);//初始化文件流
            byte[] array = new byte[fs.Length];//初始化字节数组
            fs.Read(array, 0, array.Length);//读取流中数据到字节数组中
            fs.Close();//关闭流
            string str = Encoding.Default.GetString(array);//将字节数组转化为字符串

            ///
            dynamic json = JObject.Parse(str);
            foreach (var table in json["tableList"])
            {
                Dictionary<string, string> nameEnTableInfo = new Dictionary<string, string>();
                Dictionary<string, string> oidTableInfo = new Dictionary<string, string>();
                string tableName = table["nameMib"];
                string tableOid = table["oid"];
                string tableIndexNum = table["indexNum"];
                string nameCh = table["nameCh"];
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
                        nameEn_info_db[childName]["oid"] = nameEn_info_db[childName]["oid"] +"|"+ childOid;
                        Console.WriteLine("生成json_db时{0},{1}",childName,ex.Message);
                    }

                    oidChildInfo.Add("isLeaf", "1");
                    oidChildInfo.Add("nameMib", childName);
                    oidChildInfo.Add("indexNum", tableIndexNum);
                    oid_info_db.Add(childOid, oidChildInfo);
                }
            }

            //var test = oid_info_db.ToString();
            //var jsonstroid = JsonConvert.SerializeObject(oid_info_db);
            //var jsonstrnameEn = JsonConvert.SerializeObject(nameEn_info_db);
            //var jsonstrtable = JsonConvert.SerializeObject(table_info_db);


            //string fswritePathoid = @"D:\C#\SCMT_json\SCMT_json\jsonfile\oid_info_db.json";
            //string fswritePathnameEn = @"D:\C#\SCMT_json\SCMT_json\jsonfile\nameEn_info_db.json";
            //string fswritePattableh = @"D:\C#\SCMT_json\SCMT_json\jsonfile\table_info_db.json"; ;
            //FileStream fswoid = new FileStream(fswritePathoid, FileMode.Create, FileAccess.Write);//找到文件如果文件不存在则创建文件如果存在则覆盖文件                                                                            //清空文件
            //                                                                                   //清空文件
            //FileStream fswname = new FileStream(fswritePathnameEn, FileMode.Create, FileAccess.Write);
            //FileStream fswtable = new FileStream(fswritePattableh, FileMode.Create, FileAccess.Write);

            //fswoid.SetLength(0);
            //fswname.SetLength(0);
            //fswtable.SetLength(0);

            //StreamWriter sw1 = new StreamWriter(fswoid, Encoding.Default);
            //StreamWriter sw2 = new StreamWriter(fswname, Encoding.Default);
            //StreamWriter sw3 = new StreamWriter(fswtable, Encoding.Default);

            //sw1.Write(jsonstroid);
            //sw1.Flush();
            //sw1.Close();

            //sw2.Write(jsonstrnameEn);
            //sw2.Flush();
            //sw2.Close();

            //sw3.Write(jsonstrtable);
            //sw3.Flush();
            //sw3.Close();
            return;
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
