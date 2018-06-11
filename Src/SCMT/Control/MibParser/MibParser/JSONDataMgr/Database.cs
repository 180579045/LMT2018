using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;


namespace MIBDataParser.JSONDataMgr
{
    ///
    public class ReDataByEnglishName : IReDataByEnglishName
    {
        public string oid { get;}
        public string mibSyntax { get;}//MIB_Syntax取值类型
        public string mangerValue { get;}//取值范围
        public string mibDesc { get;}//描述信息 MIBDesc
        public string parentOid { get;}//父oid，table=""
        public ReDataByEnglishName(){}
        public ReDataByEnglishName(string oid, string mibSyntax, string mangerValue, string mibDesc, string parentOid)
        {
            this.oid = oid;
            this.mibSyntax = mibSyntax;//MIB_Syntax
            this.mangerValue = mangerValue;//取值范围,管理站取值范围
            this.mibDesc = mibDesc;//描述信息 MIBDesc
            this.parentOid = parentOid;//父oid，table=null
        }
    }

    public class ReDataByOid : IReDataByOid
    {
        //// OidInfo 实现
        public string nameEn { get; }
        public string isLeaf { get; }
        public string indexNum { get; }
        public ReDataByOid() { }
        public ReDataByOid(string nameEn, string isLeaf, string indexNum)
        {
            this.nameEn = nameEn;
            this.isLeaf = isLeaf;
            this.indexNum = indexNum;
        }
    }

    public class ReDataByTableEnglishNameChild : IReDataByTableEnglishNameChild
    {
        public string childNameMib {get; set;} // "alarmCauseNopublic string  ,
        public string childNo {get; set;} // 1,
        public string childOid {get; set;} // "1.1.1.1.1.1",
        public string childNameCh {get; set;} // "告警原因编号",
        public string isMib {get; set;} // 1,
        public string ASNType {get; set;} // "Integer32",
        public string OMType {get; set;} // "s32",
        public string UIType {get; set;} // 0,
        public string managerValueRange {get; set;} // "0-2147483647",
        public string defaultValue {get; set;} // "×",
        public string detailDesc {get; set;} // "告警原因编号， 取值  :0..2147483647。",
        public string leafProperty {get; set;} // 0,
        public string unit {get; set;} // ""
        public bool idIndex { get; set; }
    }
    public class ReDataByTableEnglishName : IReDataByTableEnglishName
    {
        //// TableInfo 实现
        public string oid { get; set; }
        public string indexNum { get; set; }
        public List<IReDataByTableEnglishNameChild> childrenList { get; set; }
    }

    public class ReCmdDataByCmdEnglishName : IReCmdDataByCmdEnglishName
    {
        public string m_cmdNameEn { get;} // 命令的英文名
        public string m_tableName { get;  } // 命令的mib表英文名
        public string m_cmdType { get; } //命令类型
        public string m_cmdDesc { get; } //命令描述
        public List<string> m_leaflist { get; } // 命令节点名

        public ReCmdDataByCmdEnglishName(JObject value, string cmdNameEn)
        {
            this.m_cmdNameEn = cmdNameEn;
            this.m_tableName = value["TableName"].ToString();
            this.m_cmdType = value["CmdType"].ToString();
            this.m_cmdDesc = value["CmdDesc"].ToString();

            this.m_leaflist = new List<string>();
            foreach (var leaf in value["leafOIdList"])
            {
                m_leaflist.Add(leaf.ToString());
            }
        }
    }

    public sealed class Database : IDatabase
    {
        public  ResultInitData resultInitData; // 委托, 返回初始化的结果

        private MibInfoList mibL = null; // MIB 相关数据的操作句柄
        private CmdInfoList cmdL = null; // Cmd 相关数据的操作句柄

        private static Database _instance = null;//private static readonly object SynObj = new object();

        [Obsolete("Use Method public static Database GetInstance(). For example:Database dtHandle = Database.GetInstance(); instead", true)]
        public  Database(){}
        private Database(string my){}
        public static Database GetInstance()
        {
            if (null == _instance){
                _instance = new Database("");
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
                new Thread(
                    new ParameterizedThreadStart(DBInitDateBaseByIpConnect)).Start(connectIp);
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
            //ReDataByEnglishName reDataC = new ReDataByEnglishName();
            ////dynamic getNameInfo;

            //if (nameEn.Length == 0 | mibL == null)
            //    return false;

            ////if (!mibL.getNameEnInfo(nameEn, out getNameInfo))
            ////    return false;
            ////reDataC._myOid = getNameInfo["oid"];
            //reData = reDataC;
            return true;
        }
        [Obsolete("Use Method bool getDataByEnglishName(Dictionary<string, IReDataByEnglishName> reData, string connectIp, out string err); instead", true)]
        public bool getDataByEnglishName(List<string> nameEnList, out List<IReDataByEnglishName> reDataList, string curConnectIp)
        {
            string err = "";
            reDataList = new List<IReDataByEnglishName>();
            //foreach (var nameEn in nameEnList)
            //{
            //    IReDataByEnglishName nameInfo = new ReDataByEnglishName();
            //    if (!this.getDataByEnglishName(nameEn, out nameInfo, curConnectIp,out err))
            //    {
            //        reDataList = null;
            //        return false;
            //    }
            //    reDataList.Add(nameInfo);
            //}
            return true;
        }
        public bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData, string curConnectIp, out string err)
        {
            err = "";
            reData = null;
            // 参数判断
            if ((String.Empty == nameEn) || (String.Empty == curConnectIp) || mibL == null)
            {
                err = "nameEn is null , or connectIp is null or 数据库没有初始化.";
                return false;
            }
            NameEnInfo getNameInfo;
            TableInfo getTableInfo ;
            if ((false == mibL.getNameEnInfo(nameEn, out getNameInfo, curConnectIp))
                || (false == mibL.getTableInfo(getNameInfo.m_tableNameEn, out getTableInfo, curConnectIp)))
            {
                err = String.Format("get nameEn({0}) err.", nameEn);
                return false;
            }

            if (getNameInfo.m_isLeaf)//1,为叶子节点
            {
                LeafInfo child = getTableInfo.childList.Find(x => String.Equals(x.childNameMib, nameEn));
                reData = new ReDataByEnglishName(getNameInfo.m_oid, child.mibSyntax, child.managerValueRange,
                    child.mibDesc, getTableInfo.oid);
            }
            else//1,为叶子节点//0,为父节点
            {
                reData = new ReDataByEnglishName(getNameInfo.m_oid, getTableInfo.mibSyntax, "", getTableInfo.mibDesc, "");
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
            if ((reData == null) || (reData.Keys.Count == 0) || (String.Empty == connectIp) || (mibL == null)){
                err = "reData, connectIp or mibDb is err.";
                return false;
            }
            // 获取keys
            string[] dtKeys = new string[reData.Keys.Count];//.Copy(reData.Keys, dtKeys, 0);
            reData.Keys.CopyTo(dtKeys, 0);

            // 查询
            IReDataByEnglishName getNameInfo = null;
            foreach (var key in dtKeys)
            {
                if (!getDataByEnglishName(key, out getNameInfo, connectIp,out  err)) {
                    return false;
                }
                reData[key] = getNameInfo;
            }
            return true;
        }

        [Obsolete("Use Method bool getDataByOid(Dictionary<string, IReDataByOid> reData, string connectIp, out string err); instead", true)]
        public bool getDataByOid(string oid, out IReDataByOid reData, string curConnectIp)
        {
            reData = null;
            //ReDataByOid reOidInfo = new ReDataByOid();
            //dynamic getOidInfo;

            //if (oid.Length == 0 | mibL == null)
            //    return false;

            //if (!mibL.getOidEnInfo(oid, out getOidInfo, curConnectIp))
            //    return false;
            //reOidInfo.myNameEn = getOidInfo["nameMib"];
            //reOidInfo.myIsLeaf = getOidInfo["isLeaf"];
            //reOidInfo.myIndexNum = getOidInfo["indexNum"];
            //reData = reOidInfo;
            return false;
        }
        public bool getDataByOid(string oid, out IReDataByOid reData, string curConnectIp, out string err)
        {
            reData = null;
            err = "";

            if ((String.Empty == oid) || (String.Empty == curConnectIp) || (mibL == null)){
                err = "oid , mibDb or connectIp is err.";
                return false;
            }

            OidInfo oidInfo;
            if (!mibL.getOidEnInfo(oid,out oidInfo, curConnectIp)){
                err = String.Format("get db by {0},err.", oid);
                return false;
            }

            reData = new ReDataByOid(oidInfo.m_nameEn, oidInfo.m_isLeaf.ToString(), oidInfo.m_indexNum.ToString());
            return true;
        }
        public bool getDataByOid(Dictionary<string, IReDataByOid> reData, string connectIp, out string err)
        {
            err = "";
            // 参数判断
            if ((reData == null) || (reData.Keys.Count == 0) || (String.Empty == connectIp) || (mibL == null))
            {
                err = "reData , mibDb or connectIp is err.";
                return false;
            }
            // 获取keys
            string[] dtKeys = new string[reData.Keys.Count];
            reData.Keys.CopyTo(dtKeys, 0);

            //
            IReDataByOid getOidInfo;
            foreach (var key in dtKeys)
            {
                if (!getDataByOid(key, out getOidInfo, connectIp, out err))
                    return false;
                reData[key] = getOidInfo;
            }
            return true;
        }

        [Obsolete("Use Method bool getDataByTableEnglishName(Dictionary<string, IReDataByTableEnglishName> reData, string connectIp, out string err); instead", true)]
        public bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName reData, string curConnectIp)
        {
            reData = null;
            return false;
        }
        public bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName reData, string curConnectIp, out string err)
        {
            reData = null;
            err = "";

            if ((nameEn == String.Empty) | (curConnectIp == String.Empty) | mibL == null)
            {
                err = "input err or mibDb is null";
                return false;
            }

            TableInfo getTable ;
            if (!mibL.getTableInfo(nameEn.Replace("Table", "Entry"), out getTable, curConnectIp))
            {
                err = String.Format("get nameEn({0}) err.", nameEn);
                return false;
            }
            ReDataByTableEnglishName table = new ReDataByTableEnglishName();
            table.oid = getTable.oid;
            table.indexNum = getTable.indexNum.ToString();
            table.childrenList = new List<IReDataByTableEnglishNameChild>();
            foreach (LeafInfo getChild in getTable.childList)
            {
                table.childrenList.Add(new ReDataByTableEnglishNameChild(){
                    childNameMib = getChild.childNameMib, // "alarmCauseNopublic string  ,
                    childNo = getChild.childNo.ToString(), // 1,
                    childOid = getChild.childOid, // "1.1.1.1.1.1",
                    childNameCh = getChild.childNameCh, // "告警原因编号",
                    isMib = getChild.isMib.ToString(), // 1,
                    ASNType = getChild.ASNType, // "Integer32",
                    OMType = getChild.OMType, // "s32",
                    UIType = getChild.UIType.ToString(), // 0,
                    managerValueRange = getChild.managerValueRange, // "0-2147483647",
                    defaultValue = getChild.defaultValue, // "×",
                    detailDesc = getChild.defaultValue, // "告警原因编号， 取值  :0..2147483647。",
                    leafProperty = getChild.leafProperty.ToString(), // 0,
                    unit = getChild.unit, // ""
                });
            }
            reData = table;
            return true;
        }
        public bool getDataByTableEnglishName(Dictionary<string, IReDataByTableEnglishName> reData, string connectIp, out string err)
        {
            err = "";
            // 参数判断
            if ((reData == null) || (reData.Keys.Count == 0) ||(String.Empty == connectIp)|| (mibL == null))
            {
                err = "reData ,mibDb,  or connectIp is err.";
                return false;
            }
            // 获取keys
            string[] dtKeys = new string[reData.Keys.Count];
            reData.Keys.CopyTo(dtKeys, 0);

            // 查询
            IReDataByTableEnglishName getTable;
            foreach (var key in dtKeys)
            {
                if (!getDataByTableEnglishName(key, out getTable, connectIp, out err))
                    return false;
                reData[key] = getTable;
            }
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
            return cmdL.getCmdInfoByCmdEnglishName(reData, connectIp, out err);
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
            Console.WriteLine("Db init start ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            // 1. 解压lm.dtz
            if (!DBInitZip())
            {
                resultInitData(false);
                return;
            }
            Console.WriteLine("Db init zip ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            // 2. 解析lm.dtz => 写json文件(增加，叶子节点的读写属性) 解析.mdb文件
            if (!DBInitParseMdbToWriteJson())
            {
                resultInitData(false);
                return;
            }
            Console.WriteLine("Db init parse mdb ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            // 3. 解析json 文件
            if (!DBInitParseJsonToMemory(connectIp.ToString()))
            {
                resultInitData(false);
                return;
            }
            Console.WriteLine("mib/cmd list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

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
            //cmdL.GeneratedCmdInfoList();
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
            return new JsonDataManager("5.10.11").ConvertAccessDbToJsonForThread();
        }
        /// 3. 解析json文件到内存中
        private bool DBInitParseJsonToMemory(string connectIp)
        {
            mibL = new MibInfoList();// mib 节点
            cmdL = new CmdInfoList();// cmd 节点
            if (!mibL.GeneratedMibInfoList(connectIp))
                return false;
            cmdL.GeneratedCmdInfoList(connectIp);

            //Console.WriteLine("Db init list1 start ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            //MibInfoList tets1 = new MibInfoList();
            //tets1.GeneratedMibInfoList(connectIp);

            //Console.WriteLine("Db init list1 ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            //Console.WriteLine("Db init list2 start ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            //MibInfoList tets2 = new MibInfoList();
            //tets2.GeneratedMibInfoListThread(connectIp);
            //Console.WriteLine("Db init list2 ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));


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
