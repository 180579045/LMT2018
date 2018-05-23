using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MIBDataParser.JSONDataMgr
{
    class CmdInfoList
    {
        /// <summary>
        /// key (string) : cmd English name
        /// value (Dictionary): {
        ///     "TableName":.
        ///     "CmdType":
        ///     "CmdDesc":命令描述
        ///     "leafOIdList" (List):[oid_1,oid_2,...,oid_x]}
        /// </summary>
        Dictionary<string, dynamic> cmd_info_db = new Dictionary<string, dynamic>();

        public void GeneratedCmdInfoList()
        {          ///
            ReadIniFile iniFile = new ReadIniFile();
            string iniFilePath = iniFile.getIniFilePath("JsonDataMgr.ini");
            string jsonfilepath = iniFile.IniReadValue(iniFilePath, "JsonFileInfo", "jsonfilepath");
            string sFilePath = jsonfilepath + "cmd.json";
            FileStream fs = new FileStream(sFilePath, FileMode.Open);//初始化文件流
            byte[] array = new byte[fs.Length];//初始化字节数组
            fs.Read(array, 0, array.Length);//读取流中数据到字节数组中
            fs.Close();//关闭流
            string str = Encoding.Default.GetString(array);//将字节数组转化为字符串

            ///
            dynamic json = JObject.Parse(str);
            //cmd_info_db = json;

            foreach (var table in json)
            {
                dynamic value = table.Value;
                Dictionary<string, dynamic> cmdInfo = new Dictionary<string, dynamic>();
                List<string> leaflist = new List<string>();

                cmdInfo.Add("TableName", value["TableName"]);
                cmdInfo.Add("CmdType", value["CmdType"]);
                cmdInfo.Add("CmdDesc", value["CmdDesc"]);
                foreach (var leaf in value["leafOIdList"])
                {
                    leaflist.Add(leaf.ToString());
                }
                cmdInfo.Add("leafOIdList", leaflist);

                cmd_info_db.Add(table.Name.ToString(), cmdInfo);
            }
            return;
        }

        public bool getCmdInfoByCmdEnglishName(string cmdNameEn, out Dictionary<string, dynamic> cmdInfo)
        {
            cmdInfo = new Dictionary<string, dynamic>();
            //判断键存在
            if (!cmd_info_db.ContainsKey(cmdNameEn)) // exist == True 
            {
                Console.WriteLine("Table db with Key = ({0}) not exists.", cmdNameEn);
                return false;
            }
            cmdInfo = cmd_info_db[cmdNameEn];
            return true;
        }
    }
}
