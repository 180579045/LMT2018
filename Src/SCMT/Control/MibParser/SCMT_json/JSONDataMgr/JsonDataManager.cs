/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ JsonDataManager $
* 机器名称：       $ machinename $
* 命名空间：       $ SCMT_json.JSONDataMgr $
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
using SCMT_json.Interface.UISnmp;

namespace SCMT_json.JSONDataMgr
{
    class JsonDataManager
    {
        //单独一个线程处理消息
        /*public static Thread jsonThread = new Thread(json_ReceiveMsg);

        static public void Json_ReceiveMsg()
        {
            //分发消息
            return;
        }*/
        string mibInfo;
        string objTreeInfo;
        string mibVersion;
        
        string mdbFile;
        string jsonfilepath;

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
                return ;//显示异常信息
            }
            this.mibVersion = mibVersion;
        }

        /// <summary>
        /// lm.dtz解析生成json文件，保存当前基站的对象树及MIB信息
        /// </summary>
        /// <param name="fileName"></param>
        public void ConvertAccessDbToJson()//string fileName,string mibJsonPath, string ojbJsonPath)
        {
            //"D:\\C#\\SCMT\\output\\lm.mdb", "D:\\C#\\SCMT\\mib.json", "D:\\C#\\SCMT\\obj.json"
            Console.WriteLine("begin to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            //查询MibTree database
            string sqlContent = "select * from MibTree order by ExcelLine asc";
            DataSet dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
            MibJsonData mibJsonDatat = new MibJsonData(mibVersion);
            mibJsonDatat.MibParseDataSet(dataSet);
            JsonFile jsonMibFile = new JsonFile();
            ///jsonMibFile.WriteFile("D:\\C#\\SCMT\\mib.json", mibJsonDatat.GetStringMibJson());
            jsonMibFile.WriteFile(jsonfilepath+ "mib.json", mibJsonDatat.GetStringMibJson());
            this.mibInfo = mibJsonDatat.GetStringMibJson();

            //对象树转换生成json文件
            dataSet.Reset();
            sqlContent = "select * from ObjTree order by ObjExcelLine";
            dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
            ObjTressJsonData objTreeJson = new ObjTressJsonData();
            objTreeJson.ObjParseDataSet(dataSet);
            JsonFile jsonObjFile = new JsonFile();
            //jsonObjFile.WriteFile("D:\\C#\\SCMT\\obj.json", objTreeJson.GetStringObjTreeJson());
            jsonObjFile.WriteFile(jsonfilepath + "obj.json", objTreeJson.GetStringObjTreeJson());
            this.objTreeInfo = objTreeJson.GetStringObjTreeJson();
            Console.WriteLine("end to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
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

        /// <summary>
        /// 数据一致性文件解析生成json文件保存当前基站相关参数值
        /// </summary>
        /// <param name="filePath"></param>
        private void DataConsistencyConvertToJson(string filePath)
        {
            return;
        }
        /// <summary>
        /// SNMP模块主动上报的trap信息
        /// </summary>
        /// <param name="message"> structSnmp </param>
        /// <returns> structUI </returns>
        private string TrapMessageDealfromSNMP(string message)
        {
            JObject trapReport = new JObject();
            return trapReport.ToString();
        }

        /// <summary>
        /// UI模块发来的get,set,getnext消息
        /// <作者>luanyibo</作者>
        /// </summary>
        /// <param name="message">UIInfoInterface 消息格式</param>
        public SNMPInfoInterface DcMessageDealfromUI(UIInfoInterface message)
        {
            SNMPInfoInterface m_snmpInfo = new SNMPInfoInterface();
            if (message == null)
            {
                return m_snmpInfo;
            }
            string m_oid = "";
            //SNMPInfoInterface m_snmpInfo = new SNMPInfoInterface();
            List<UILeafInfo> m_leafLists = message.LeafLists;
            if (m_leafLists == null)
            {
                return m_snmpInfo;
            }
            foreach (var leaf in m_leafLists)
            {
                m_oid = SearchOidFromJsonByEnglishName(leaf.ChildNameMib);
                m_oid += message.IndexContent;
                m_snmpInfo.addNewOidLeafList(m_oid, leaf.Value);
            }
            m_snmpInfo.RequestId = message.RequestId;
            m_snmpInfo.MessageType = message.MessageType;
            m_snmpInfo.ErrorStatus = message.ErrorStatus;
            m_snmpInfo.ErrorIndex = message.ErrorIndex;

            //Console.WriteLine(message);
            return m_snmpInfo;
        }


        /// <summary>
        /// Snmp模块发来的消息
        /// </summary>
        /// <作者>luanyibo</作者>
        /// <param name="message"> SNMPInfoInterface snmp格式的传入信息</param>
        /// <returns> UIInfoInterface, UI格式的输出信息</returns>
        public UIInfoInterface DcMessageDealfromSnmp(SNMPInfoInterface message)
        {
            string m_oid = "";
            UIInfoInterface re_uiInfo = new UIInfoInterface();
            List<string> splitOid = new List<string>();
            List<SnmpOidLeafInfo> m_oidleafLists = message.OidLeafLists;
            foreach (var oidInfo in m_oidleafLists)
            {
                splitOid = splitSnmpOidForIndexContentAndOid(oidInfo.Oid);
                m_oid = SearchEnglishNameFromJsonByOid(splitOid[0]);
                re_uiInfo.addNewLeafList(m_oid, oidInfo.Value);
            }
            re_uiInfo.RequestId = message.RequestId;
            re_uiInfo.MessageType = message.MessageType;
            re_uiInfo.ErrorStatus = message.ErrorStatus;
            re_uiInfo.ErrorIndex = message.ErrorIndex;
            re_uiInfo.IndexContent = splitOid[1];

            //Console.WriteLine(message);
            return re_uiInfo;
        }



        /// <summary>
        /// 通过UI中的英文名称，查找对应的oid
        /// </summary>
        /// <作者>luanyibo</作者>
        /// <param name="searchEnlishName"> EnglishName </param>
        /// <returns> 查到,返回leafOid;查不到，返回NULL </returns>
        public string SearchOidFromJsonByEnglishName(string searchEnglishName)
        {
            //List<string> strReturnOid = new List<string>();
            //Console.WriteLine("begin to Search {0}, time is {1}" , searchContent, DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            string strReturnOid = "";
            JObject mibJObject = JObject.Parse(this.mibInfo);
            JArray  mibJArray  = JArray.Parse(mibJObject["tableList"].ToString());
            foreach (var table in mibJArray)
            {
                if (table["nameMib"].ToString().ToLower().Contains(searchEnglishName.ToLower()))
                {
                    strReturnOid = table["oid"].ToString().ToLower();
                    return strReturnOid;
                }
                else
                {
                    JArray leafJArray = JArray.Parse(table["childList"].ToString());
                    foreach (var leaf in leafJArray)
                    {
                        if (leaf["childNameMib"].ToString().ToLower().Contains(searchEnglishName.ToLower()))
                        {
                            strReturnOid = leaf["childOid"].ToString().ToLower();
                            return strReturnOid;
                        }
                    }
                }
            }
            //Console.WriteLine("end to Search {0}, time is {1}", searchContent, DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            return strReturnOid;
        }

        /// <summary>
        /// 通过snmp中的oid，查找对应的EnglishName
        /// </summary>
        /// <作者>luanyibo</作者>
        /// <param name="searchOid"> searchOid 需要去掉索引值</param>
        /// <returns> 查到,返回 EnglishName;查不到，返回NULL </returns>
        public string SearchEnglishNameFromJsonByOid(string searchOid)
        {
            //string newOid = "";
            //int lastPoint = searchOid.LastIndexOf(".");
            string nameEnglish = "";
            JObject mibJObject = JObject.Parse(this.mibInfo);
            JArray mibJArray = JArray.Parse(mibJObject["tableList"].ToString());
            foreach ( var table in mibJArray)
            {
                if ( 0 == String.Compare(table["oid"].ToString(), searchOid))
                {
                    nameEnglish = table["nameMib"].ToString();
                    break;
                }
                else
                {
                    JArray leafJArray = JArray.Parse(table["childList"].ToString());
                    foreach (var leaf in leafJArray)
                    {
                        if ( 0 == String.Compare(leaf["childOid"].ToString(), searchOid))
                        {
                            nameEnglish = leaf["childNameMib"].ToString();
                            break;
                        }
                    }
                }
            }

            return nameEnglish;
        }

        /// <summary>
        /// 拆分oid，把索引项拆出来
        /// </summary>
        /// <作者>luanyibo</作者>
        /// <param name="searchOid"> searchOid 需要去掉索引值</param>
        /// <returns> 查到,返回 list[0]:oid,list[1]:indexContent </returns>
        public List <string> splitSnmpOidForIndexContentAndOid(string searchOid)
        {
            List<string> re = new List<string>();
            int lastPoint = searchOid.LastIndexOf(".");
            string indexContent = searchOid.Substring(lastPoint);
            string oid = searchOid.Substring(0, lastPoint);
            re.Add(oid);
            re.Add(indexContent);
            //Console.WriteLine(searchOid);
            //Console.WriteLine(re[0]);
            //Console.WriteLine(re[1]);
            return re;
        }


        /// <summary>
        /// UI模块发来的模糊查询的消息返回对象树的表名
        /// </summary>
        /// <param name="searchContent"></param>
        /// <returns>JSON字符串示例
        /// {
        /// "result": [
        /// {
        /// "objNameMibTable": "netRRUEntry"
        /// },
        /// {
        /// "objNameMibTable": "netRRUAntennaSettingEntry"
        /// },
        /// {
        /// "objNameMibTable": "netRRURootAlarmEntry"
        /// }
        /// ]
        /// }
        /// </returns>
        public string SearchMessageDealfromUI(string searchContent)
        {
            JObject resultJObject = new JObject();
            JArray resultJArray = new JArray();
            List<string> listMibTables = new List<string>();
            //需要查找对象树中文名称，mib：oid, 中文名称，英文名称
            JObject mibJObject = JObject.Parse(this.mibInfo);
            JArray mibJArray = JArray.Parse(mibJObject["tableList"].ToString());
            foreach (var table in mibJArray)
            {
                bool matchFlag = false;
                string tableNameMib = table["nameMib"].ToString();
                if (tableNameMib.ToLower().Contains(searchContent.ToLower()))
                {
                    matchFlag = true;
                }
                else if (table["oid"].ToString().ToLower().Contains(searchContent.ToLower()))
                {
                    matchFlag = true;
                }
                else if (table["nameCh"].ToString().ToLower().Contains(searchContent.ToLower()))
                {
                    matchFlag = true;
                }
                else
                {
                    JArray leafJArray = JArray.Parse(table["childList"].ToString());
                    foreach (var leaf in leafJArray)
                    {
                        if (leaf["childNameMib"].ToString().ToLower().Contains(searchContent.ToLower()))
                        {
                            matchFlag = true;
                        }
                        else if (leaf["childOid"].ToString().ToLower().Contains(searchContent.ToLower()))
                        {
                            matchFlag = true;
                        }
                        else if (leaf["childNameCh"].ToString().ToLower().Contains(searchContent.ToLower()))
                        {
                            matchFlag = true;
                        }
                    }
                }
                if (matchFlag)
                {
                    listMibTables.Add(tableNameMib);
                }
            }
            //需要转换成对象树的顺序
            JObject objTreeJObject = JObject.Parse(this.objTreeInfo);
            IList<string> tokens = objTreeJObject.SelectTokens("$...objNameMibTable").Select(q => (string)q).ToList();
            List<string> listObjTables = new List<string>();
            foreach (var item in tokens)
            {
                if(0 != string.Compare("/",item.ToString()))
                {
                    listObjTables.Add(item);
                }
            }
            List<string> listResult = new List<string>();
            listResult = listObjTables.Intersect(listMibTables).ToList();
            foreach (var item in listResult)      // 各个值  
            {
                resultJArray.Add(new JObject { { "objNameMibTable", item } });
            }
            resultJObject.Add("result", resultJArray);
            return resultJObject.ToString();
        }

        //UI获取对象树信息
        public string GetObjTreefromUI()
        {
            return this.objTreeInfo;
        }

        //点击对象树某个节点，返回所有的叶子节点信息及类型，以显示标题栏及给UI保存数据类型
        public string GetTableTitleInfoByUI(string message)
        {
            JObject resultJObject = new JObject();
            JArray resultJArray = new JArray();
            JArray indexJArray = new JArray();
            JObject mibJObject = JObject.Parse(this.mibInfo);
            JToken mibtable = mibJObject.SelectToken("$.tableList[?(@.nameMib=='" + message + "')]");
            int indexNum = int.Parse(mibtable["indexNum"].ToString());
            int indexLoop = 0;

            JArray leafJArray = JArray.Parse(mibtable["childList"].ToString());
            foreach (var item in leafJArray)
            {
                if(indexLoop > indexNum)
                {
                    resultJArray.Add(addLeafProperty((JObject)item));
                }
                else if (indexLoop == indexNum)
                {
                    resultJObject.Add("实例描述", indexJArray);
                    indexLoop++;
                }
                else
                {
                    indexJArray.Add(addLeafProperty((JObject)item));
                    indexLoop++;
                }
            }
            resultJObject.Add("leafList", resultJArray);
            return resultJObject.ToString();
        }

        private JObject addLeafProperty(JObject propertyJO)
        {
            JObject result = new JObject { { "childNameCh", propertyJO["childNameCh"]},
                { "managerValueRange", propertyJO["managerValueRange"]},
                { "unit",propertyJO["unit"]},
                { "detailDesc",propertyJO["detailDesc"]},
                { "UIType",propertyJO["UIType"]}};
            return result;
        }

        //用来发消息的函数
        public int sendMessage(string message)
        {
            return 0;
        }
    }
}
