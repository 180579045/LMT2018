using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;


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

    public class ReDataByTableEnglishNameChild : IReDataByTableEnglishNameChild
    {
        public string childNameMib { get; set; } // "alarmCauseNopublic string  ,
        public string childNo { get; set; } // 1,
        public string childOid { get; set; } // "1.1.1.1.1.1",
        public string childNameCh { get; set; } // "告警原因编号",
        public string isMib { get; set; } // 1,
        public string ASNType { get; set; } // "Integer32",
        public string OMType { get; set; } // "s32",
        public string UIType { get; set; } // 0,
        public string managerValueRange { get; set; } // "0-2147483647",
        public string defaultValue { get; set; } // "×",
        public string detailDesc { get; set; } // "告警原因编号， 取值  :0..2147483647。",
        public string leafProperty { get; set; } // 0,
        public string unit { get; set; } // ""
    }
    public class ReDataByTableEnglishName : IReDataByTableEnglishName
    {
        public string myOid { get; set; }
        public string myIndexNum { get; set; }
        public List<IReDataByTableEnglishNameChild> myChildList = new List<IReDataByTableEnglishNameChild>();

        //// TableInfo 实现
        public string oid { get { return myOid; } }
        public string indexNum { get { return myIndexNum; } }
        public List<IReDataByTableEnglishNameChild> childrenList { get { return myChildList; } }
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
        public  ResultInitData resultInitData; // 委托, 返回初始化的结果

        private MibInfoList mibL = null; // MIB 相关数据的操作句柄
        private CmdInfoList cmdL = null; // Cmd 相关数据的操作句柄

        private string curConnectIp = "";// 标记数据库的归属，是哪个基站的数据

        /// <summary>
        /// 初始化 线程: 调用 DBInitDateBaseByIpConnect 。
        /// </summary>
        /// <param name="connectIp">基站连接的ip</param>
        public void initDatabase(string connectIp)
        {
            try{
                new Thread(new ParameterizedThreadStart(DBInitDateBaseByIpConnect)).Start(connectIp);
            }
            catch{
                resultInitData(false);
            }
            return;
        }


        /// <summary>
        /// 真实执行初始化内容的函数。
        /// </summary>
        private void DBInitDateBaseByIpConnect(object connectIp)
        {
            //Console.WriteLine("Db init start ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            // 1. 解压lm.dtz
            if (!DBInitZip())
            {
                resultInitData(false);
                return;
            }

            // 2. 解析lm.dtz => json文件(增加，叶子节点的读写属性) 解析.mdb文件
            if (!DBInitParseMdbToWriteJson())
            {
                resultInitData(false);
                return;
            }

            // 3. 解析json 文件
            if (!DBInitParseJsonToMemory(connectIp.ToString()))
            {
                resultInitData(false);
                return;
            }
            
            // 4. 结果
            resultInitData(true);
            //Console.WriteLine("mib/cmd list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
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
        public bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData, string curConnectIp)
        {
            reData = null;
            ReDataByEnglishName reDataC = new ReDataByEnglishName();
            dynamic getNameInfo;

            if (nameEn.Length == 0 | mibL == null)
                return false;

            if (!mibL.getNameEnInfo(nameEn, out getNameInfo, curConnectIp))
                return false;
            reDataC._myOid = getNameInfo["oid"];
            reData = reDataC;
            return true;
        }

        public bool getDataByEnglishName(List<string> nameEnList, out List<IReDataByEnglishName> reDataList)
        {
            reDataList = new List<IReDataByEnglishName>();
            foreach (var nameEn in nameEnList)
            {
                IReDataByEnglishName nameInfo = new ReDataByEnglishName();
                if (!this.getDataByEnglishName(nameEn, out nameInfo))
                    return false;
                reDataList.Add(nameInfo);
            }
            return true;
        }
        public bool getDataByEnglishName(List<string> nameEnList, out List<IReDataByEnglishName> reDataList, string curConnectIp)
        {
            reDataList = new List<IReDataByEnglishName>();
            foreach (var nameEn in nameEnList)
            {
                IReDataByEnglishName nameInfo = new ReDataByEnglishName();
                if (!this.getDataByEnglishName(nameEn, out nameInfo, curConnectIp))
                {
                    reDataList = null;
                    return false;
                }
                reDataList.Add(nameInfo);
            }
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
        public bool getDataByOid(string oid, out IReDataByOid reData, string curConnectIp)
        {
            reData = null;
            ReDataByOid reOidInfo = new ReDataByOid();
            dynamic getOidInfo;

            if (oid.Length == 0 | mibL == null)
                return false;

            if (!mibL.getOidEnInfo(oid, out getOidInfo, curConnectIp))
                return false;
            reOidInfo.myNameEn = getOidInfo["nameMib"];
            reOidInfo.myIsLeaf = getOidInfo["isLeaf"];
            reOidInfo.myIndexNum = getOidInfo["indexNum"];
            reData = reOidInfo;
            return true;
        }
        
        public bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName reData)
        {
            ReDataByTableEnglishName reTable = new ReDataByTableEnglishName();
            reData = null;

            if (nameEn.Length == 0 | mibL == null)
                return false;

            dynamic getTable;
            if (!mibL.getTableInfo(nameEn.Replace("Table", "Entry"), out getTable))
                return false;
            reTable.myOid = getTable["oid"];
            reTable.myIndexNum = getTable["indexNum"];
            foreach (var child in getTable["childList"])
            {
                ReDataByTableEnglishNameChild childInfo = new ReDataByTableEnglishNameChild();

                childInfo.childNameMib = child["childNameMib"];
                childInfo.childNo = child["childNo"];
                childInfo.childOid = child["childOid"];
                childInfo.childNameCh = child["childNameCh"];
                childInfo.isMib = child["isMib"];
                childInfo.ASNType = child["ASNType"];
                childInfo.OMType = child["OMType"];
                childInfo.UIType = child["UIType"];
                childInfo.managerValueRange = child["managerValueRange"];
                childInfo.defaultValue = child["defaultValue"];
                childInfo.detailDesc = child["detailDesc"];
                childInfo.leafProperty = child["leafProperty"];
                childInfo.unit = child["unit"];
                //
                reTable.myChildList.Add(childInfo);
            }

            reData = reTable;
            return true;
        }

        public bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName reData, string curConnectIp)
        {
            ReDataByTableEnglishName reTable = new ReDataByTableEnglishName();
            reData = null;

            if (nameEn.Length == 0 | mibL == null)
                return false;

            dynamic getTable;
            if (!mibL.getTableInfo(nameEn.Replace("Table", "Entry"), out getTable, curConnectIp))
                return false;
            reTable.myOid = getTable["oid"];
            reTable.myIndexNum = getTable["indexNum"];
            foreach (var child in getTable["childList"])
            {
                ReDataByTableEnglishNameChild childInfo = new ReDataByTableEnglishNameChild();

                childInfo.childNameMib = child["childNameMib"];
                childInfo.childNo = child["childNo"];
                childInfo.childOid = child["childOid"];
                childInfo.childNameCh = child["childNameCh"];
                childInfo.isMib = child["isMib"];
                childInfo.ASNType = child["ASNType"];
                childInfo.OMType = child["OMType"];
                childInfo.UIType = child["UIType"];
                childInfo.managerValueRange = child["managerValueRange"];
                childInfo.defaultValue = child["defaultValue"];
                childInfo.detailDesc = child["detailDesc"];
                childInfo.leafProperty = child["leafProperty"];
                childInfo.unit = child["unit"];
                //
                reTable.myChildList.Add(childInfo);
            }

            reData = reTable;
            return true;
        }

        public bool getCmdDataByCmdEnglishName(string cmdEn, out IReCmdDataByCmdEnglishName reCmdData)
        {
            reCmdData = new ReCmdDataByCmdEnglishName();
            if (null == cmdL | cmdEn == String.Empty)
                return false;

            Dictionary<string, dynamic> cmdInfo;
            cmdL.getCmdInfoByCmdEnglishName(cmdEn, out cmdInfo);
            reCmdData.cmdNameEn = cmdEn; // 命令的英文名
            reCmdData.tableName = cmdInfo["TableName"]; // 命令的mib表英文名
            reCmdData.cmdType = cmdInfo["CmdType"]; //命令类型
            reCmdData.cmdDesc = cmdInfo["CmdDesc"]; //命令描述
            reCmdData.leaflist = cmdInfo["leafOIdList"]; // 命令节点名
            return true;
        }


        /**********   私有函数   **********/
        //初始化(1.解压lm.dtz;2.解析.mdb,生成json;3.解析json;)
        private void DBInitDateBase()
        {
            // 初始化
            // 1. 解压lm.dtz
            UnzippedLmDtz unZip = new UnzippedLmDtz();
            string err = "";
            if (!unZip.UnZipFile(out err))
            {
                Console.WriteLine("Err:Unzip fail, {0}", err);
                resultInitData(false);
                return;
            }
            Console.WriteLine("unzip ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            // 2. 解析lm.dtz => json文件(增加，叶子节点的读写属性)
            //解析.mdb文件
            JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM = new JsonDataManager("5.10.11");
            //JsonDataM.ConvertAccessDbToJson();
            JsonDataM.ConvertAccessDbToJsonForThread();
            Console.WriteLine("write json ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            // 3. 解析json 文件
            //MibInfoList mibL = new MibInfoList();
            mibL = new MibInfoList();// mib 节点
            cmdL = new CmdInfoList();// cmd 节点
            //mibL.GeneratedMibInfoList();
            cmdL.GeneratedCmdInfoList();
            Console.WriteLine("mib/cmd list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            //mibL.getOidEnInfo(@"1.3.6.1.4.1.5105.1.2.100.1.1.5.6.1.20.33",out oidInfo);

            //
            resultInitData(true);
            return;
        }
        /// 1. 解压lm.dtz
        private bool DBInitZip()
        {
            string err = "";
            UnzippedLmDtz unZip = new UnzippedLmDtz();
            if (!unZip.UnZipFile(out err))
            {
                resultInitData(false);
                Console.WriteLine("Err : DBInitZip fail, {0}", err);
                return false;
            }
            return true;
        }
        /// 2. 解析lm.mdb,写json文件; 解析lm.dtz => json文件(增加，叶子节点的读写属性) 解析.mdb文件
        private bool DBInitParseMdbToWriteJson()
        {
            JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            if (!JsonDataM.ConvertAccessDbToJsonForThread())
                return false;
            return true;
        }
        /// 3. 解析json文件到内存中
        private bool DBInitParseJsonToMemory(string connectIp)
        {
            mibL = new MibInfoList();// mib 节点
            cmdL = new CmdInfoList();// cmd 节点
            if (!mibL.GeneratedMibInfoList(connectIp))
                return false;
            cmdL.GeneratedCmdInfoList();
            return true;
        }
        /**********   私有函数   **********/


        //////////

        public bool testGetDataByTableEnglishName()
        {
            ReadIniFile iniFile = new ReadIniFile();
            string jsonfilepath = iniFile.IniReadValue(iniFile.getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

            JsonFile json = new JsonFile();
            JObject JObj = json.ReadJsonFileForJObject(jsonfilepath + "Tree_Reference.json");
            foreach (var table in JObj["NodeList"])
            {
                IReDataByTableEnglishName reData;
                string MibTableName = table["MibTableName"].ToString();
                if (String.Equals("/", MibTableName))
                    continue;
                if (false == getDataByTableEnglishName(MibTableName, out reData))
                {
                    Console.WriteLine("===={0} not exist.", MibTableName);
                }
            }
            return true;
        }
    }
}
