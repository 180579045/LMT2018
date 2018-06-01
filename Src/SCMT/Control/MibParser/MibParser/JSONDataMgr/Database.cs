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

        public ReDataByTableEnglishNameChild()
        {
        }

        public ReDataByTableEnglishNameChild(dynamic child)
        {
            this.childNameMib = child["childNameMib"];
            this.childNo = child["childNo"];
            this.childOid = child["childOid"];
            this.childNameCh = child["childNameCh"];
            this.isMib = child["isMib"];
            this.ASNType = child["ASNType"];
            this.OMType = child["OMType"];
            this.UIType = child["UIType"];
            this.managerValueRange = child["managerValueRange"];
            this.defaultValue = child["defaultValue"];
            this.detailDesc = child["detailDesc"];
            this.leafProperty = child["leafProperty"];
            this.unit = child["unit"];
        }
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

    public sealed class Database : IDatabase
    {
        public  ResultInitData resultInitData; // 委托, 返回初始化的结果

        private MibInfoList mibL = null; // MIB 相关数据的操作句柄
        private CmdInfoList cmdL = null; // Cmd 相关数据的操作句柄

        private static Database _instance = null;

        private static readonly object SynObj = new object();

        [Obsolete("Use Method public static Database GetInstance(). For example:Database dtHandle = Database.GetInstance(); instead", true)]
        public Database()
        {
        }

        private Database(string my)
        {
        }

        public static Database GetInstance()
        {
            if (null == _instance)
            {
                _instance = new Database("");
                //lock (SynObj)
                //{
                //    if (null == _instance)
                //    {
                //        _instance = new Database();
                //    }
                //}
            }

            return _instance;
        }

        /// <summary>
        /// 初始化 线程: 调用 DBInitDateBaseByIpConnect 。
        /// </summary>
        /// <param name="connectIp">基站连接的ip，标记数据库的归属，是哪个基站的数据</param>
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

        [Obsolete("Use Method bool getDataByEnglishName(Dictionary<string, IReDataByEnglishName> reData, string connectIp, out string err); instead", true)]
        public bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData)
        {
            reData = null;
            ReDataByEnglishName reDataC = new ReDataByEnglishName();
            //dynamic getNameInfo;

            if (nameEn.Length == 0 | mibL == null)
                return false;

            //if (!mibL.getNameEnInfo(nameEn, out getNameInfo))
            //    return false;
            //reDataC._myOid = getNameInfo["oid"];
            reData = reDataC;
            return true;
        }
        [Obsolete("Use Method bool getDataByEnglishName(Dictionary<string, IReDataByEnglishName> reData, string connectIp, out string err); instead", true)]
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
        [Obsolete("Use Method bool getDataByEnglishName(Dictionary<string, IReDataByEnglishName> reData, string connectIp, out string err); instead", true)]
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
        /// <summary>
        /// [查询]通过节点英文名字，查询节点信息。支持多节点查找。
        /// </summary>
        /// <param name="reData">其中key为查询的英文名(需要输入)，value为对应的节点信息。</param>
        /// <param name="connectIp">信息归属的标识</param>
        /// <param name="err">查询失败的原因</param>
        /// <returns></returns>
        public bool getDataByEnglishName(Dictionary<string, IReDataByEnglishName> reData, string connectIp, out string err)
        {
            err = "";

            // 参数判断
            if ((reData == null) || (reData.Keys.Count == 0) || (String.Empty == connectIp))
            {
                err = "reData is null , reData keys count is 0 or connectIp is null.";
                return false;
            }
            if (mibL == null)
            {
                err = "数据库没有初始化";
                return false;
            }

            // 获取keys
            string[] dtKeys = new string[reData.Keys.Count];
            reData.Keys.CopyTo(dtKeys, 0);

            // 查询
            dynamic getNameInfo;
            foreach (var key in dtKeys)
            {
                ReDataByEnglishName nameInfo = new ReDataByEnglishName();
                if (!mibL.getNameEnInfo(key, out getNameInfo, connectIp))
                {
                    err = String.Format("get nameEn({0}) err.",key);
                    return false;
                }
                nameInfo._myOid = getNameInfo["oid"];
                reData[key] = nameInfo;
            }
            return true;
        }

        [Obsolete("Use Method bool getDataByOid(Dictionary<string, IReDataByOid> reData, string connectIp, out string err); instead", true)]
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
        public bool getDataByOid(Dictionary<string, IReDataByOid> reData, string connectIp, out string err)
        {
            err = "";

            // 参数判断
            if ((reData == null) || (reData.Keys.Count == 0) || (String.Empty == connectIp))
            {
                err = "reData is null , reData keys count is 0 or connectIp is null.";
                return false;
            }
            if (mibL == null)
            {
                err = "数据库没有初始化";
                return false;
            }

            // 获取keys
            string[] dtKeys = new string[reData.Keys.Count];
            reData.Keys.CopyTo(dtKeys, 0);

            //
            dynamic getOidInfo;
            foreach (var key in dtKeys)
            {
                ReDataByOid reOidInfo = new ReDataByOid();
                if (!mibL.getOidEnInfo(key, out getOidInfo, connectIp))
                {
                    err = String.Format("get nameEn({0}) err.", key);
                    return false;
                }
                reOidInfo.myNameEn = getOidInfo["nameMib"];
                reOidInfo.myIsLeaf = getOidInfo["isLeaf"];
                reOidInfo.myIndexNum = getOidInfo["indexNum"];
                //
                reData[key] = reOidInfo;
            }
            return true;
        }

        [Obsolete("Use Method bool getDataByTableEnglishName(Dictionary<string, IReDataByTableEnglishName> reData, string connectIp, out string err); instead", true)]
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
        public bool getDataByTableEnglishName(Dictionary<string, IReDataByTableEnglishName> reData, string connectIp, out string err)
        {
            err = "";

            // 参数判断
            if ((reData == null) || (reData.Keys.Count == 0) ||(String.Empty == connectIp))
            {
                err = "reData is null , reData keys count is 0 or connectIp is null.";
                return false;
            }
            if (mibL == null)
            {
                err = "数据库没有初始化";
                return false;
            }

            // 获取keys
            string[] dtKeys = new string[reData.Keys.Count];
            reData.Keys.CopyTo(dtKeys, 0);

            // 查询
            dynamic getTable;
            foreach (var key in dtKeys)
            {
                if (!mibL.getTableInfo(key.Replace("Table", "Entry"), out getTable, connectIp))
                {
                    err = String.Format("get nameEn({0}) err.", key);
                    return false;
                }

                ReDataByTableEnglishName reTable = new ReDataByTableEnglishName();
                reTable.myOid = getTable["oid"];
                reTable.myIndexNum = getTable["indexNum"];
                foreach (var child in getTable["childList"])
                {
                    reTable.myChildList.Add(new ReDataByTableEnglishNameChild(child));
                }
                //
                reData[key] = reTable;
            }
            
            return true;
        }

        /// <summary>
        /// 查询命令的相关内容
        /// </summary>
        /// <param name="cmdEn">输入的查询命令英文名</param>
        /// <param name="reCmdData">返回相关的命令内容</param>
        /// <param name="curConnectIp">输入命令的归属基站标识</param>
        /// <returns></returns>
        [Obsolete("Use Method bool getCmdDataByCmdEnglishName(Dictionary<string, IReCmdDataByCmdEnglishName> reData, string connectIp, out string err); instead", true)]
        public bool getCmdDataByCmdEnglishName(string cmdEn, out IReCmdDataByCmdEnglishName reCmdData, string curConnectIp)
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
        public bool getCmdDataByCmdEnglishName(Dictionary<string, IReCmdDataByCmdEnglishName> reData, string connectIp, out string err)
        {
            err = "";

            // 参数判断
            if ((reData == null) || (reData.Keys.Count == 0) || (String.Empty == connectIp))
            {
                err = "reData is null , reData keys count is 0 or connectIp is null.";
                return false;
            }
            if (cmdL == null)
            {
                err = "数据库没有初始化";
                return false;
            }

            // 获取keys
            string[] dtKeys = new string[reData.Keys.Count];
            reData.Keys.CopyTo(dtKeys, 0);

            // 查询
            Dictionary<string, dynamic> cmdInfo;
            foreach (var key in dtKeys)
            {
                if (!cmdL.getCmdInfoByCmdEnglishName(key, out cmdInfo))
                {
                    err = String.Format("get cmd({0}) err.", key);
                    return false;
                }

                ReCmdDataByCmdEnglishName reCmdData = new ReCmdDataByCmdEnglishName();
                reCmdData.cmdNameEn = key; // 命令的英文名
                reCmdData.tableName = cmdInfo["TableName"]; // 命令的mib表英文名
                reCmdData.cmdType = cmdInfo["CmdType"]; //命令类型
                reCmdData.cmdDesc = cmdInfo["CmdDesc"]; //命令描述
                reCmdData.leaflist = cmdInfo["leafOIdList"]; // 命令节点名
                //
                reData[key] = reCmdData;
            }

            return true;
        }


        public bool testDictExample(Dictionary<string, IReDataByEnglishName> reData)
        {
            string[] keys = new string [reData.Keys.Count];
            reData.Keys.CopyTo(keys, 0);

            foreach (var key in keys)
            {
                ReDataByEnglishName dat = new ReDataByEnglishName();
                reData[key] = dat;
                break;
            }

            //reData = reDataN;

            return true;
        }

        ///**********   私有函数   **********/
        /// <summary>
        /// 真实执行初始化内容的函数。
        /// </summary>
        /// <param name="connectIp"> 标识数据的归属，查询要用 </param>
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

        //public bool testGetDataByTableEnglishName()
        //{
        //    ReadIniFile iniFile = new ReadIniFile();
        //    string jsonfilepath = iniFile.IniReadValue(iniFile.getIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

        //    JsonFile json = new JsonFile();
        //    JObject JObj = json.ReadJsonFileForJObject(jsonfilepath + "Tree_Reference.json");
        //    foreach (var table in JObj["NodeList"])
        //    {
        //        IReDataByTableEnglishName reData;
        //        string MibTableName = table["MibTableName"].ToString();
        //        if (String.Equals("/", MibTableName))
        //            continue;
        //        if (false == getDataByTableEnglishName(MibTableName, out reData))
        //        {
        //            Console.WriteLine("===={0} not exist.", MibTableName);
        //        }
        //    }
        //    return true;
        //}
    }
}
