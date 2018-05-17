using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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

    public class Database : IDatabase
    {
        public ResultInitData resultInitData;
        private MibInfoList mibL = null;
        //private CmdInfoList cmdL = null;

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
            mibL = new MibInfoList();
            mibL.GeneratedMibInfoList();
            Console.WriteLine("mib list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            //mibL.getOidEnInfo(@"1.3.6.1.4.1.5105.1.2.100.1.1.5.6.1.20.33",out oidInfo);

            //
//             cmdL = new CmdInfoList();
//             cmdL.GeneratedCmdInfoList();
            resultInitData(true);
            return;
        }

        public bool initDatabase()
        {
            try
            {
                Thread childThread = new Thread(myInitDateBase);
                childThread.Start();
                return true;
            }
            catch
            {
                return false;
            }
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

            if (!mibL.getTableInfo(nameEn, out getTable))
                return false;
 
            reTable.myOid = getTable["oid"];
            reTable.myIndexNum = getTable["indexNum"];
            reTable.myChildList = getTable["childList"];

            reData = reTable;
            return true;
        }
    }
}
