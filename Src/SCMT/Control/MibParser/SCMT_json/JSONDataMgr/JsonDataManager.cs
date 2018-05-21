/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ JsonDataManager $
* 机器名称：       $ machinename $
* 命名空间：       $ MIBDataParser.JSONDataMgr $
* 文 件 名：       $ JsonDataManager.cs $
* 创建时间：       $ 2018.04.XX $
* 作    者：       $ TangYun $
* 说   明 ：
*     JsonDataManager。
* 修改时间     修 改 人    修改内容：
* 2018.04.xx   唐 芸       创建文件并实现类  JsonDataManager
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using Newtonsoft.Json.Linq;

namespace MIBDataParser.JSONDataMgr
{
    class JsonDataManager
    {
        string mibInfo;
        string objTreeInfo;
        string cmdTreeInfo;
        string mibVersion;
        
        string mdbFile;
        string jsonfilepath;

        static private bool isMibJsonOK = false;
        static private bool isObjJsonOK = false;
        static private bool isCmdJsonOK = false;

        public JsonDataManager(string mibVersion)
        {
            // 配置文件获取
            ReadIniFile iniFile = new ReadIniFile();
            string iniFilePath = iniFile.getIniFilePath("JsonDataMgr.ini");
            try
            {
                string mdbfilePath = iniFile.IniReadValue(iniFilePath, "ZipFileInfo", "mdbfilePath");
                mdbFile = mdbfilePath + "lm.mdb";
                jsonfilepath = iniFile.IniReadValue(iniFilePath, "JsonFileInfo", "jsonfilepath");

            }
            catch (Exception ex)
            {
                Console.WriteLine("读取ini文件({0})时,{1}", iniFilePath, ex.Message);
                return;//显示异常信息
            }
            this.mibVersion = mibVersion;
        }

        /// <summary>
        /// lm.dtz解析生成json文件，保存当前基站的对象树及MIB信息
        /// </summary>
        /// <param name="fileName"></param>
        public void ConvertAccessDbToJson()//string fileName,string mibJsonPath, string ojbJsonPath)
        {
            Console.WriteLine("begin to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            ConvertAccessDbToJsonMibTree();
            ConvertAccessDbToJsonObjTree();
            ConvertAccessDbToJsonCmdTree();

            //// 查询MibTree database
            //string sqlContent = "select * from MibTree order by OID asc";
            //DataSet dataSet = GetRecordByAccessDb(this.mdbFile, sqlContent);
            //MibJsonData mibJsonDatat = new MibJsonData(mibVersion);
            //mibJsonDatat.MibParseDataSet(dataSet);

            //JsonFile jsonMibFile = new JsonFile();
            //jsonMibFile.WriteFile(jsonfilepath + "mib.json", mibJsonDatat.GetStringMibJson());
            //this.mibInfo = mibJsonDatat.GetStringMibJson();

            ////对象树转换生成json文件
            //dataSet.Reset();
            //sqlContent = "select * from ObjTree order by ObjExcelLine";
            //dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
            //ObjTressJsonData objTreeJson = new ObjTressJsonData();
            //objTreeJson.ObjParseDataSet(dataSet);
            //JsonFile jsonObjFile = new JsonFile();
            ////jsonObjFile.WriteFile("D:\\C#\\SCMT\\obj.json", objTreeJson.GetStringObjTreeJson());
            //jsonObjFile.WriteFile(jsonfilepath + "obj.json", objTreeJson.GetStringObjTreeJson());
            //this.objTreeInfo = objTreeJson.GetStringObjTreeJson();

            ////cmdTree命令树 转换生成json文件
            //ConvertAccessDbToJsonCmdTree();
            Console.WriteLine("end to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            return;
        }
        public void ConvertAccessDbToJsonForThread()//string fileName,string mibJsonPath, string ojbJsonPath)
        {
            isMibJsonOK = false;
            isObjJsonOK = false;
            isCmdJsonOK = false;
            
            //Console.WriteLine("begin to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            Thread[] threads = new Thread[3];
            threads[0] = new Thread(new ThreadStart(ConvertAccessDbToJsonMibTree));
            threads[0].Name = "MibTree";
            threads[1] = new Thread(new ThreadStart(ConvertAccessDbToJsonObjTree));
            threads[1].Name = "ObjTree";
            threads[2] = new Thread(new ThreadStart(ConvertAccessDbToJsonCmdTree));
            threads[2].Name = "CmdTree";
            
            foreach (Thread t in threads)
            {
                t.Start();
            }

            while (true){
                if (true == isMibJsonOK && true == isObjJsonOK && true == isCmdJsonOK) {
                    break;
                }
            }
            //Console.WriteLine("end   to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            //Console.Read();
            return;
        }

        /// <summary>
        /// 解析lm.dtz 写 mib.json
        /// </summary>
        public void ConvertAccessDbToJsonMibTree()
        {
            //Console.WriteLine("DbToJsonMibTree start " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            string sqlContent = "select * from MibTree order by OID asc";
            DataSet dataSet = GetRecordByAccessDb(this.mdbFile, sqlContent);
            MibJsonData mibJsonDatat = new MibJsonData(mibVersion);
            mibJsonDatat.MibParseDataSet(dataSet);

            JsonFile jsonMibFile = new JsonFile();
            jsonMibFile.WriteFile(jsonfilepath + "mib.json", mibJsonDatat.GetStringMibJson());
            this.mibInfo = mibJsonDatat.GetStringMibJson();

            isMibJsonOK = true;
            //Console.WriteLine("DbToJsonMibTree end " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
        }
        /// <summary>
        /// 解析lm.dtz 写 obj.json
        /// </summary>
        public void ConvertAccessDbToJsonObjTree()
        {
            //Console.WriteLine("DbToJsonObjTree start " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            string sqlContent = "select * from ObjTree order by ObjExcelLine";
            DataSet dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
            ObjTressJsonData objTreeJson = new ObjTressJsonData();
            objTreeJson.ObjParseDataSet(dataSet);

            JsonFile jsonObjFile = new JsonFile();
            //jsonObjFile.WriteFile("D:\\C#\\SCMT\\obj.json", objTreeJson.GetStringObjTreeJson());
            jsonObjFile.WriteFile(jsonfilepath + "obj.json", objTreeJson.GetStringObjTreeJson());
            this.objTreeInfo = objTreeJson.GetStringObjTreeJson();

            isObjJsonOK = true;
            //Console.WriteLine("DbToJsonObjTree end " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
        }
        /// <summary>
        /// 解析lm.dtz 写 cmd.json
        /// </summary>
        public void ConvertAccessDbToJsonCmdTree()
        {
            //Console.WriteLine("DbToJsonCmdTree start " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            //生产 cmdTree 命令树文件
            string sqlContent = "select * from CmdTree order by CmdID";
            DataSet dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
            CmdTreeJsonData cmdJsonDatat = new CmdTreeJsonData();
            cmdJsonDatat.CmdParseDataSet(dataSet);

            JsonFile jsonObjFile = new JsonFile();
            //jsonObjFile.WriteFile("D:\\C#\\SCMT\\obj.json", objTreeJson.GetStringObjTreeJson());
            jsonObjFile.WriteFile(jsonfilepath + "cmd.json", cmdJsonDatat.GetStringObjTreeJson());
            this.cmdTreeInfo = cmdJsonDatat.GetStringObjTreeJson();

            isCmdJsonOK = true;
            //Console.WriteLine("DbToJsonCmdTree end " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            return;
        }

        /// <summary>
        /// 打开数据库,进行查询
        /// </summary>
        /// <param name="fileName">完整文件名</param>
        /// <param name="sqlContent">sql查询语句</param>
        /// <returns>DataSet结果</returns>
        private DataSet GetRecordByAccessDb(string fileName, string sqlContent)
        {
            //fileName = "D:\\C#\\SCMT\\lm.mdb";
            DataSet dateSet = new DataSet();
            //To do:将lm.dtz改名lm.rar,再解压缩,再改名成为mdb
            AccessDBManager mdbData = new AccessDBManager(fileName);
            try
            {
                mdbData.Open();
                dateSet = mdbData.GetDataSet(sqlContent);
                mdbData.Close();
            }
            finally
            {
                mdbData = null;
            }
            return dateSet;
        }
    }
}
