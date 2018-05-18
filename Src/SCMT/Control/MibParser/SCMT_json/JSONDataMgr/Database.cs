using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;


namespace MIBDataParser.JSONDataMgr
{
    ///
    public class ReDataByEnglishName : IReDataByEnglishName
    {
        public string _myOid { get; set; }

        // INameEnInfo1 实现
        public string oid
        {
            get { return this._myOid; }
        }
    }

    public class ReDataByOid : IReDataByOid
    {
        public string myNameEn { get; set; }
        public string myIsLeaf { get; set; }
        public string myIndexNum { get; set; }

        //// OidInfo 实现
        public string nameEn { get { return myNameEn; } }
        public string isLeaf { get { return myIsLeaf; } }
        public string indexNum { get { return myIndexNum; } }
    }

    public class ReDataByTableEnglishName : IReDataByTableEnglishName
    {
        public string myOid { get; set; }
        public string myIndexNum { get; set; }
        public List<Dictionary<string, object>> myChildList = new List<Dictionary<string, object>>();
        //// TableInfo 实现
        public string oid { get { return myOid; } }
        public string indexNum { get { return myIndexNum; } }
        public List<Dictionary<string, object>> childrenList { get { return myChildList; } }
    }

    public class ReCmdDataByCmdEnglishName : IReCmdDataByCmdEnglishName
    {
        public string cmdNameEn { get; set; } // 命令的英文名
        public string tableName { get; set; } // 命令的mib表英文名
        public string cmdType { get; set; } //命令类型
        public string cmdDesc { get; set; } //命令描述
        public List<string> leaflist { get; set; } // 命令节点名
    }

    public class Database : IDatabase
    {
        public ResultInitData resultInitData;
        private MibInfoList mibL = null;
        private CmdInfoList cmdL = null;

        // 初始化 线程: 调用 myInitDateBase
        public void initDatabase()
        {
            try
            {
                // 开线程: void myInitDateBase();
                Thread childThread = new Thread(myInitDateBase);
                childThread.Start();
                return;
            }
            catch
            {
                return;
            }
        }

        //初始化(1.解压lm.dtz;2.解析.mdb;3.解析json;)
        private void myInitDateBase()
        {
            // 初始化
            // 1. 解压lm.dtz
            UnzippedLmDtz unZip = new UnzippedLmDtz();
            string err = "";
            if (!unZip.UnZipFile(out err))
            {
                Console.WriteLine("Err:Unzip fail, {0}", err);
                resultInitData(false);
                return ;
            }
            Console.WriteLine("unzip ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            // 2. 解析lm.dtz => json文件(增加，叶子节点的读写属性)
            //解析.mdb文件
            JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM.ConvertAccessDbToJson();
            Console.WriteLine("write json ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            // 3. 解析json 文件
            //MibInfoList mibL = new MibInfoList();
            mibL = new MibInfoList();// mib 节点
            cmdL = new CmdInfoList();// cmd 节点
            mibL.GeneratedMibInfoList();
            cmdL.GeneratedCmdInfoList();
            Console.WriteLine("mib list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            //mibL.getOidEnInfo(@"1.3.6.1.4.1.5105.1.2.100.1.1.5.6.1.20.33",out oidInfo);

            //
            resultInitData(true);
            return;
        }


        public bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData)
        {
            reData = null;
            ReDataByEnglishName reDataC = new ReDataByEnglishName();
            dynamic getNameInfo;

            if (nameEn.Length == 0 | mibL == null)
                return false;

            if (!mibL.getNameEnInfo(nameEn, out getNameInfo))
                return false;
            reDataC._myOid = getNameInfo["oid"];
            reData = reDataC;
            return true;
        }

        public bool getDataByOid(string oid, out IReDataByOid reData)
        {
            reData = null;
            ReDataByOid reOidInfo = new ReDataByOid();
            dynamic getOidInfo;

            if (oid.Length == 0 | mibL == null)
                return false;

            if (!mibL.getOidEnInfo(oid, out getOidInfo))
                return false;
            reOidInfo.myNameEn = getOidInfo["nameMib"];
            reOidInfo.myIsLeaf = getOidInfo["isLeaf"];
            reOidInfo.myIndexNum = getOidInfo["indexNum"];
            reData = reOidInfo;
            return true;
        }

        public bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName reData)
        {
            reData = null;
            
            ReDataByTableEnglishName reTable = new ReDataByTableEnglishName();
            dynamic getTable;

            if (nameEn.Length == 0 | mibL == null)
                return false;

            //if (!mibL.getTableInfo(nameEn, out getTable))
            //    return false;
            string nameTableEn = nameEn.Replace("Table", "Entry");
            if (!mibL.getTableInfo(nameTableEn, out getTable))
                return false;

            reTable.myOid = getTable["oid"];
            reTable.myIndexNum = getTable["indexNum"];

            string entryNameEn = getTable["nameMib"];
            string entryOid = getTable["oid"];
            string entryNameCh = getTable["nameCh"];
            var entryChildList = getTable["childList"];

            List<Dictionary<string, object>> childlist = new List<Dictionary<string, object>>();
            foreach (var child in entryChildList)
            {
                Dictionary<string, object> childInfo = new Dictionary<string, object>();
                childInfo.Add("childNameMib", child["childNameMib"]);
                childInfo.Add("childNo", child["childNo"]);
                childInfo.Add("childOid", child["childOid"]);
                childInfo.Add("childNameCh", child["childNameCh"]);
                childInfo.Add("isMib", child["isMib"]);
                childInfo.Add("ASNType", child["ASNType"]);
                childInfo.Add("OMType", child["OMType"]);
                childInfo.Add("UIType", child["UIType"]);
                childInfo.Add("managerValueRange", child["managerValueRange"]);
                childInfo.Add("defaultValue", child["defaultValue"]);
                childInfo.Add("detailDesc", child["detailDesc"]);
                childInfo.Add("leafProperty", child["leafProperty"]);
                childInfo.Add("unit", child["unit"]);
                //
                childlist.Add(childInfo);
            }
            reTable.myChildList = childlist;

            reData = reTable;
            return true;
        }

        public bool testGetDataByTableEnglishName()
        {
            ReadIniFile iniFile = new ReadIniFile();
            string iniFilePath = iniFile.getIniFilePath("JsonDataMgr.ini");
            string jsonfilepath = iniFile.IniReadValue(iniFilePath, "JsonFileInfo", "jsonfilepath");
            string sFilePath = jsonfilepath + "Tree_Reference.json";
            
            StreamReader reader = File.OpenText(sFilePath);
            JObject JObj = new JObject();
            JObj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            foreach (var table in JObj["NodeList"])
            {
                IReDataByTableEnglishName reData;
                string MibTableName = table["MibTableName"].ToString();
                if (String.Equals("/", MibTableName))
                    continue;
                if (false == getDataByTableEnglishName(MibTableName, out reData))
                {
                    //Console.WriteLine("===={0} not exist.", MibTableName);
                }
            }

            return true;
        }

        public bool getCmdDataByCmdEnglishName(string cmdEn, out IReCmdDataByCmdEnglishName reCmdData)
        {
            reCmdData = new ReCmdDataByCmdEnglishName();
            if (null == cmdL | cmdEn == String.Empty)
                return false;

            Dictionary<string, dynamic> cmdInfo;
            cmdL.getCmdInfoByCmdEnglishName(cmdEn, out cmdInfo);
            return true;
        }
    }
}
