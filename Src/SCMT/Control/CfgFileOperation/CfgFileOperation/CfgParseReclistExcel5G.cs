using CfgFileOpStruct;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    /// <summary>
    /// 1. 目标是什么？
    /// 5G 要求根据参数的选择可以生成多个 patch
    /// 2. 解析规则是相同的
    /// 3. patch的类型是多种的(5种)
    /// 默认(放入bbu打包中)、恢复默认配置、(各个终端:)展讯、e500、华为。
    /// 4. 数据存在问题？
    /// 因为只处理标识为0，1，2的，保存表和要处理的节点的相关信息
    /// </summary>
    class CfgParseReclistExcel5G
    {
        /// <summary>
        /// 写文件句柄
        /// </summary>
        BinaryWriter g_bw = null;
        /// <summary>
        /// reclist 文件地址
        /// </summary>
        string g_strExcelPath = "";
        string g_mdbPath = "";
        //2014-2-12 luoxin 当前索引
        string m_strCurTabAndIndex = "";
        /// <summary>
        /// 某种终端要处理的表，及其表的内容。
        /// </summary>
        Dictionary<string, ReclistTable> m_reclTable = new Dictionary<string, ReclistTable>();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strMdb"></param>
        public CfgParseReclistExcel5G(BinaryWriter bw, string strExcelPath, string strMdb)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strMdb) || (null == bw))
                return ;
            if (!File.Exists(strExcelPath))
            {
                bw.Write(String.Format("CfgParseReclistExcel5G : ({0}) 文件不存在！", strExcelPath).ToArray());
                return ;
            }
            if (!File.Exists(strMdb))
            {
                bw.Write(String.Format("CfgParseReclistExcel5G : mdb ({0}) 文件！", strMdb).ToArray());
                return;
            }
            g_bw = bw;
            g_strExcelPath = strExcelPath;
            g_mdbPath = strMdb;
        }


        public void InsertPdgTab(string strTabName, string strFlag, CfgOp cfgOp)
        {
            if ("1" == strFlag || "2" == strFlag)
            {
                if (false != m_reclTable.ContainsKey(strTabName))
                {
                    return;
                }
                if (false == cfgOp.m_mapTableInfo.ContainsKey(strTabName))
                {
                    return;
                }
                ReclistTable newTable = new ReclistTable(cfgOp, strTabName);
                m_reclTable.Add(strTabName, newTable);
            }
        }
        /// <summary>
        /// 获取某种终端要处理的表的个数
        /// </summary>
        /// <returns></returns>
        public int GetVectPDGTabNameNum()
        {
            return m_reclTable.Count;
        }
        /// <summary>
        /// 获取某种终端要处理的表的集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetVectPDGTabName()
        {
            return m_reclTable.Keys.ToList();// m_vectPDGTabName;
        }
        public ReclistTable GetReclistTableByName(string strTabName)
        {
            if (!m_reclTable.ContainsKey(strTabName))
                return null;
            return m_reclTable[strTabName];
        }
        public CfgTableOp GetCfgTableOpByName(string strTabName)
        {
            if (!m_reclTable.ContainsKey(strTabName))
                return null;
            return m_reclTable[strTabName].tableInfo;
        }
        /// <summary>
        /// 文件头
        /// </summary>
        StruCfgFileHeader m_eNB_pdgFile_Header;        // path Cfg文件头结构
        /// <summary>
        /// 填写文件头
        /// </summary>
        /// <param name="strDBName">数据库地址</param>
        public void WriteHeaderVersionInfoPDG(string strDBName)//CAdoConnection* pAdoCon,const CString &strFileDesc)
        {
            string strSQL = "select * from SystemParameter where SysParameter = 'MibPublicVersion'";
            DataSet MibdateSet = new CfgAccessDBManager().GetRecord(strDBName, strSQL);
            DataRow row = MibdateSet.Tables[0].Rows[0];
            string strMibVersion = row.ItemArray[1].ToString();//row["MibPublicVersion"].ToString();
            new CfgAccessDBManager().Close(MibdateSet);
            //
            m_eNB_pdgFile_Header = new StruCfgFileHeader("patch");
            m_eNB_pdgFile_Header.Setu8VerifyStr("ICF");
            m_eNB_pdgFile_Header.Setu8HiDeviceType(new MacroDefinition().NB_DEVICE);// 暂时
            m_eNB_pdgFile_Header.Setu8MiDeviceType(new MacroDefinition().LTE_DEVICE);// = LTE_DEVICE;
            m_eNB_pdgFile_Header.Setu16LoDevType();
            m_eNB_pdgFile_Header.Setu32IcFileVer("cfg_Version_2");
            m_eNB_pdgFile_Header.FillVerInfo(strMibVersion);//4 "5_65_3_6"
            m_eNB_pdgFile_Header.u32DataBlk_Location = (uint)Marshal.SizeOf(new StruCfgFileHeader());//956
            m_eNB_pdgFile_Header.Setu8LastMotifyDate(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            m_eNB_pdgFile_Header.u32IcFile_HeaderVer = new MacroDefinition().LTE_ICF_HEADER;
            m_eNB_pdgFile_Header.u32IcFile_HeaderLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(new StruCfgFileHeader());// new StruCfgFileHeader());
            m_eNB_pdgFile_Header.Setu8IcFileDesc("初配文件");
            m_eNB_pdgFile_Header.u16NeType = new MacroDefinition().NETTYPE;
            m_eNB_pdgFile_Header.Sets8DataFmtVer("ICF");
            m_eNB_pdgFile_Header.u8FileType = new MacroDefinition().OM_ENB_PDG_FILE;
            //m_bEmptyCfg = false;
            return;
        }
        /// <summary>
        /// 文件头序列化
        /// </summary>
        /// <returns></returns>
        public byte[] GetFileHeaderToByteArray()
        {
            return m_eNB_pdgFile_Header.StruToByteArray();
        }
        /// <summary>
        /// 数据块头
        /// </summary>
        StruDataHead m_dataheadInfo_PDG;
        /// <summary>
        /// 添加数据头内容
        /// </summary>
        public void WriteDataHeadInfoPDG()
        {
            uint uintTableNum = (uint)GetVectPDGTabNameNum();
            m_dataheadInfo_PDG = new StruDataHead("init");
            m_dataheadInfo_PDG.u32DatType = 1;
            m_dataheadInfo_PDG.u32DatVer = 1;
            m_dataheadInfo_PDG.Setu8VerifyStr("BEG\0");
            m_dataheadInfo_PDG.u32TableCnt = uintTableNum;
            return;
        }
        /// <summary>
        /// 数据块头序列化
        /// </summary>
        /// <returns></returns>
        public byte[] GetDataHeadToByteArray()
        {
            return m_dataheadInfo_PDG.StruToByteArray();
        }

        /// <summary>
        /// 5G sheet:Cell参数表、gNB参数表 通用
        /// </summary>
        Dictionary<string, string> SameCol = new Dictionary<string, string>(){
            { "NodeName", "A"},          // 节点名
            { "DefaultValue", "E"},      // 默认值     标识为1使用
            { "End","O"},                // 结束标志 
        };

        /// <summary>
        /// 5G 生成
        /// <处理规则>
        /// 1. 非索引节点 : 处理标识, 节点取值处理.0: 不处理(不写入文件中);1：使用默认值(写入文件);2：使用推荐值(写入文件).
        /// 2. 索引节点时 : 处理标识只有0和1,有2就是错误。
        ///    2.1. 当有索引节点标识为1时, 代表具体的实例的具体索引.例如:
        ///         MIB变量名         | 中文名称          | 取值范围  | 默认值 | 华为:推荐值 | 处理标识
        ///         nrCellUlIotIndex* | 小区上行底噪索引	 | 0..6	     |  0     |             |  1
        ///         含义: ”nrCellUlIotIndex” 在 生成"华为patch"时, 这个值只取 "0"。
        ///    2.2.当所有索引节点标识为0时, 并且存在(<条件1>处理标识为1或2)(<条件2>非索引)的节点才有意义。
        ///         这时表示对所有实例有效，修改这个表的所有实例中这个(标识1~2)节点，节点值修改为"标识"的值.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strUeType"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        public bool ProcessingExcel(string strUeType, CfgOp cfgOp)
        {
            bool re = true;
            if ((String.Empty == strUeType) || (null == cfgOp) )
                return false;
            Dictionary<string, string> CellCol = new Dictionary<string, string>();// "Cell参数表"

            // 几种模式
            if (0 == String.Compare("0:升级发布", strUeType, true)) // 不区分大小写，相等
            {
                CellCol.Add("ProcessIdentity", "H");  // 处理标识
                CellCol.Add("recommendValue", "E");    // 推荐值    标识为2使用
            }
            else if (0 == String.Compare("4:恢复默认配置", strUeType, true)) // 不区分大小写，相等
            {
                CellCol.Add("ProcessIdentity", "G");  // 处理标识
                CellCol.Add("recommendValue", "E");    // 推荐值    标识为2使用
            }
            //展讯
            else if (0 == String.Compare(strUeType, "1:展讯", true))
            {
                CellCol.Add("ProcessIdentity", "J");  // 处理标识
                CellCol.Add("recommendValue", "I");    // 推荐值    标识为2使用
            }
            //e500
            else if (0 == String.Compare(strUeType, "2:e500", true))
            {
                CellCol.Add("ProcessIdentity", "K");  // 处理标识
                CellCol.Add("recommendValue", "L");    // 推荐值    标识为2使用
            }
            //华为
            else if (0 == String.Compare(strUeType, "3:华为", true))
            {
                CellCol.Add("ProcessIdentity", "N");  // 处理标识
                CellCol.Add("recommendValue", "M");    // 推荐值    标识为2使用
            }
            else {
                g_bw.Write(String.Format("Err CfgParseReclistExcel5G ({0}) is Err.\n", strUeType).ToArray());
                CellCol.Clear();
                return false;
            }
            // 合并两个字典
            CellCol = CellCol.Concat(SameCol).ToDictionary(k => k.Key, v => v.Value);
            // 解析
            foreach (string type in new string[] { "Cell参数表", "gNB参数表" })
            {
                g_bw.Write(String.Format("CfgParseReclistExcel5G ({0}) ({1}) DealReclist start...\n", strUeType, type).ToArray());
                Console.WriteLine("CfgParseReclistExcel5G ({0}) ({1}) DealReclist start...\n", strUeType, type);
                if (!DealReclist(type, CellCol, cfgOp))
                {
                    re = false;
                    g_bw.Write(String.Format("Err CfgParseReclistExcel5G ({0}) ({1}) DealReclist Err.\n", strUeType, type).ToArray());
                    Console.WriteLine("Err CfgParseReclistExcel5G ({0}) ({1}) DealReclist Err.\n", strUeType, type);
                    break;
                }
                g_bw.Write(String.Format("CfgParseReclistExcel5G ({0}) ({1}) DealReclist end.\n", strUeType, type).ToArray());
                Console.WriteLine("CfgParseReclistExcel5G ({0}) ({1}) DealReclist end.\n", strUeType, type);
            }
            CellCol.Clear();
            return re;
        }
        /// <summary>
        /// 自定义 (patch)
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="strCondition"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        public bool ProcessingSelfPatch(string strExcelPath, CfgOp cfgOp, string strUeType)
        {
            CfgParseSelfExcel selfExcel = new CfgParseSelfExcel();
            return selfExcel.ProcessingExcel5G(g_bw, strExcelPath, g_mdbPath, "patch", cfgOp, this, strUeType);
        }
        /// <summary>
        /// 统一处理规则
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strPageName"></param>
        /// <param name="ColName"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        bool DealReclist(string strPageName, Dictionary<string, string> ColName, CfgOp cfgOp)
        {
            int rEndNo = GetEndLineNum(strPageName, ColName);// 获取结束的行数
            if (rEndNo < 0)
                return false;
            Dictionary<string, object[,]> ColVals = GetExcelRangeValue(strPageName, rEndNo, ColName);// 获取ColName相关的col的数据
            if (null == ColVals)
                return false;

            // 1.把reclist 中存在 表名tableName 存起来；2.把reclist中每个table下的node存起来3.处理每个reclist中node的默认值。
            //每个表的用到的
            string m_strCurTableName = ""; // 当前处理的表名 
            int m_iCurTabIndexNum = -1;  // 2014-2-12 luoxin 当前表索引个数
            bool m_bIsMoreInsts = false; // 2014-2-12 luoxin 是否多实例配置
            List<Dictionary<string, string>> m_indexInfo = new List<Dictionary<string, string>>();
            for (int iLine = 4; iLine < rEndNo + 1; iLine++)
            {
                bool bIsIndex = false;    //是否是索引节点
                string strFlag = "";      //根据patch标识进行处理
                string nodeName = "";     //节点名NodeName, 
                string nodeValue = "";    //根据 Flag 取不同的值, 0,1取默认值，2取推荐值
                string strTableName = ""; //节点所属于的table
                //if (iLine == 718)
                //    Console.WriteLine("===");
                if (isEndLine(ColVals, iLine))                     // 是否结束行
                    break;
                if (!isEffectiveLine(ColVals, iLine, out strFlag)) // 是否有效行
                    continue;
                if (!GetNodeName(ColVals, iLine, strFlag, out nodeName, out bIsIndex))// 是否为索引节点bIsIndex, 如果是索引节点是否为flag为2。
                    return false;
                //if ("nrCsiRsimLcId" == nodeName)
                //    Console.WriteLine("");
                if (!GetNodeValueByFlag(ColVals, iLine, strFlag, out nodeValue))     //根据 Flag 取不同的值, 0,1取默认值，2取推荐值
                    continue;
                if (!GetTableName(cfgOp, nodeName, out strTableName)) //节点所属于的table
                    continue;


                if (0 != String.Compare(m_strCurTableName, strTableName, true)) // 是否是新表
                {
                    m_strCurTableName = strTableName;  // 更新表名
                    m_iCurTabIndexNum = GetIndexNumFromDBMibTree(cfgOp, nodeName);// 获取索引个数
                    m_bIsMoreInsts = false;            // 初始化为单实例
                    //m_vectIndexScope.Clear();          // 清空
                    //m_vectIndexName.Clear();
                    m_indexInfo.Clear();
                    if (true == bIsIndex && 0 == m_iCurTabIndexNum)
                    {
                        g_bw.Write(String.Format("Err: 自相矛盾。标量表不能有索引节点.\n").ToArray());
                        Console.WriteLine("Err: 自相矛盾。标量表不能有索引节点");
                    }
                }
                
                //if ("nrCsiRsImEntry" == m_strCurTableName)
                //    Console.WriteLine("");

                InsertPdgTab(m_strCurTableName, strFlag, cfgOp);// 保存表

                if (0 == m_iCurTabIndexNum)//标量表
                {
                    DealScalarTable(m_strCurTableName, nodeName, nodeValue, strFlag);
                }
                else//表量表
                {
                    //索引节点
                    if (bIsIndex)
                    {
                        AddIndexInfo(m_indexInfo, nodeName, nodeValue);
                        //m_vectIndexName.Add(nodeName);
                        //m_vectIndexScope.Add(nodeValue);    //真正取值，当形式不是 start .. end时，start和end等于一个值
                        // 隐含约定，如果索引值有 flag 为 1，那么第一维索引为范围，其他维索引必须为具体值。
                        if (0 == String.Compare(strFlag, "1", true))//2018-11-21,quyaxin 多实例场景取值含义变化
                        {
                            m_bIsMoreInsts = true;
                        }
                        continue;//索引节点没有办法设定特殊值
                    }
                    //其他节点//单实例配置
                    if (!m_bIsMoreInsts )
                    {
                        if (strFlag == "0")
                            continue;
                        if (!DealSingleConfigTable(nodeName, nodeValue, strFlag, m_strCurTableName))
                        {
                            g_bw.Write(String.Format("Err DealSingle: nodeName({0}),val({1}),flag({2}),table({3}).\n",
                                nodeName, nodeValue, strFlag, m_strCurTableName).ToArray());
                            return false;
                        }
                    }
                    //多实例配置
                    else
                    {
                        if (!DealMoreConfigTable(nodeName, nodeValue, strFlag, m_strCurTableName,
                            m_iCurTabIndexNum, strPageName, m_indexInfo))
                        {
                            g_bw.Write(String.Format("Err DealMore : nodeName({0}),val({1}),flag({2}),table({3}).\n",
                                nodeName, nodeValue, strFlag, m_strCurTableName).ToArray());
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 按照顺序保存索引的名称和取值
        /// </summary>
        /// <param name="m_indexInfo"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void AddIndexInfo(List<Dictionary<string, string>> m_indexInfo, string name, string value)
        {
            var info = m_indexInfo.Find(e => e.ContainsKey(name) == true);
            if (info != null)
            {
                info[name] = value;
            }
            else
            {
                m_indexInfo.Add(new Dictionary<string, string>() { { name, value } });// ()
            }
        }

        //2014-2-12 luoxin 处理标量表中节点
        bool DealScalarTable(string strCurTableName, string strNodeName, string strNodeValue, string ProcessIdentity)//string strFlag)
        {
            //做补丁文件
            if (0 == String.Compare(ProcessIdentity, "1", true))
            {
                // reclist 全局变量
                if (!SaveTableNodeByIndex(strCurTableName, strNodeName, ".0"))
                {
                    return false;
                }
            }
            else if (0 == String.Compare(ProcessIdentity, "2", true))
            {
                //do something copy by quyaxin
                if (!SaveTableNodeByIndex(strCurTableName, strNodeName, ".0"))
                {
                    return false;
                }
                //更新节点值
                if (!WriteValueToBuffer(m_reclTable[strCurTableName].tableInfo, ".0", strNodeName, strNodeValue))
                {
                    return false;
                }
            }


            return true;
        }
        /// <summary>
        /// 保存reclist 中表对应的实例(strIndex)中修改的节点
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strIndex"></param>
        /// <returns></returns>
        bool SaveTableNodeByIndex(string strTableName, string strNodeName, string strIndex)
        {
            // reclist 全局变量
            if (m_reclTable.ContainsKey(strTableName))
            {
                if (!m_reclTable[strTableName].AddLeafNameByIndex(strIndex, strNodeName))
                {
                    g_bw.Write(String.Format("Err AddLeafNameByIndex index=(.0), nodeName({0})", strNodeName).ToArray());
                    return false;
                }
            }
            else
            {
                g_bw.Write(String.Format("Err SaveTableNodeByIndex tableName({0}), nodeName({1}), m_reclTable[{0}] is null.", strTableName, strNodeName).ToArray());
                return false;// 在前面处理
            }
            return true;
        }
        //2014-2-12 luoxin 处理单实例配置表节点
        bool DealSingleConfigTable(string strNodeName, string strNodeValue, string strFlag, string strCurTableName)
        {
            if (0 == String.Compare(strFlag, "1", true))
            {
                List<string> InstINumList = m_reclTable[strCurTableName].tableInfo.GetCfgInstsInstantNum();
                foreach (var strIndex in InstINumList)
                {
                    if (!SaveTableNodeByIndex(strCurTableName, strNodeName, strIndex))//找到每个实例下的所有叶子,做补丁文件
                    {
                        return false;
                    }
                }
            }
            //2018-09-08 quyaxin 导入终端类型
            else if (0 == String.Compare(strFlag, "2", true))
            {
                List<string> InstINumList = m_reclTable[strCurTableName].tableInfo.GetCfgInstsInstantNum();
                foreach (var strIndex in InstINumList)
                {
                    //cfgOp.m_mapTableInfo[strCurTableName].InsertInstOrLeaf(strIndex, strNodeName);//做补丁文件
                    if (!SaveTableNodeByIndex(strCurTableName, strNodeName, strIndex))
                    {
                        return false;
                    }
                    //更新节点值
                    if (!WriteValueToBuffer(m_reclTable[strCurTableName].tableInfo, strIndex, strNodeName, strNodeValue))
                        continue;
                }
            }
            else
            {
                //do nothing
            }
            return true;
        }
        //2014-2-12 luoxin 处理多实例配置表节点
        bool DealMoreConfigTable(string strNodeName, string strNodeValue, string strFlag, string strCurTableName,
            int m_iCurTabIndexNum, string strPageName, List<string> m_vectIndexScope, List<string> m_vectIndexName)//, string m_strPlanIndex)
        {
            //string strIndex = "";
            if (m_vectIndexScope.Count != m_iCurTabIndexNum)
            {
                string bug = String.Format("DealMore Err: nodeName({0}) index vectnum({1}) != TabIndexNum({2}).index(",
                    strNodeName, m_vectIndexScope.Count, m_iCurTabIndexNum);
                foreach (var name in m_vectIndexName)
                    bug += String.Format("{0},", name);
                bug = bug.TrimEnd(',') + ").\n";
                g_bw.Write(bug.ToArray());
                return false;
            }
            //一维索引
            if (1 == m_iCurTabIndexNum)
            {
                if (!DealMoreConfigTable1(strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_vectIndexScope))
                    return false;
            }
            //二维索引
            if (2 == m_iCurTabIndexNum)
            {
                DealMoreConfigTable2(strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_vectIndexScope);
            }
            //二维索引
            if (3 == m_iCurTabIndexNum)
            {
                DealMoreConfigTable3(strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_vectIndexScope);
            }

            return true;
        }
        //2014-2-12 luoxin 处理多实例配置表节点
        bool DealMoreConfigTable(string strNodeName, string strNodeValue, string strFlag, string strCurTableName,
            int m_iCurTabIndexNum, string strPageName,
            List<Dictionary<string, string>> m_indexInfo)//, string m_strPlanIndex)
        {
            //string strIndex = "";
            if (m_indexInfo.Count != m_iCurTabIndexNum)
            {
                string bug = String.Format("Err DealMore : nodeName({0}) index vectnum({1}) != TabIndexNum({2}).index(",
                    strNodeName, m_indexInfo.Count, m_iCurTabIndexNum);
                foreach (var index in m_indexInfo)
                    bug += String.Format("{0},", index.Keys.ToList()[0]);
                bug = bug.TrimEnd(',') + ").\n";
                g_bw.Write(bug.ToArray());
                Console.WriteLine(bug);
                return false;
            }
            //一维索引
            if (1 == m_iCurTabIndexNum)
            {
                if (!DealMoreConfigTable1(strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_indexInfo))
                    return false;
            }
            //二维索引
            if (2 == m_iCurTabIndexNum)
            {
                DealMoreConfigTable2(strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_indexInfo);
            }
            //二维索引
            if (3 == m_iCurTabIndexNum)
            {
                DealMoreConfigTable3(strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_indexInfo);
            }

            return true;
        }
        /// <summary>
        /// 一维索引 处理
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTable1(string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName, List<string> m_vectIndexScope)//, string m_strPlanIndex)
        {
            string strIndex = "." + m_vectIndexScope.Last();//一维索引

            //2016-06-27 luoxin 判断实例是否在配置文件中存在
            if (!InstanceIsExist(strIndex, strCurTableName))
                return false;
            //2016-06-27 luoxin end

            //"gNB参数表"页的一维索引多配置全部表需要增加行状态
            CfgTableOp curtable = m_reclTable[strCurTableName].tableInfo;
            if (0 == String.Compare(strPageName, "gNB参数表", true))
            {
                string strTabAndIndex = strCurTableName + strIndex;
                string strRowStatusName = curtable.GetRowStatusName();

                //每条实例只增加一次行状态
                if (0 != String.Compare(strTabAndIndex, m_strCurTabAndIndex, true))
                {
                    if (!WriteValueToBuffer(curtable, strIndex, strRowStatusName, "4"))//设置行有效
                        return false;
                    m_strCurTabAndIndex = strTabAndIndex;//记录当前的索引
                }

                //2014-4-23 luoxin DTMUC00212701 行状态需要增加到补丁文件中
                if (strFlag == "1" || strFlag == "2")
                {
                    curtable.InsertInstOrLeaf(strIndex, strRowStatusName);
                }
                //2014-4-23 luoxin end
            }

            if (strFlag == "1" || strFlag == "2")
            {
                curtable.InsertInstOrLeaf(strIndex, strNodeName);
            }

            //更新节点值
            if (!WriteValueToBuffer(curtable, strIndex, strNodeName, strNodeValue))
                return false;

            return true;
        }
        /// <summary>
        /// 一维索引 处理
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTable1(string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName, 
            List<Dictionary<string, string>> m_indexInfo)//, string m_strPlanIndex)
        {
            string strIndex = "." + m_indexInfo[0].Values.ToList()[0];//一维索引

            //2016-06-27 luoxin 判断实例是否在配置文件中存在
            if (!InstanceIsExist(strIndex, strCurTableName))
                return false;
            //2016-06-27 luoxin end

            //"gNB参数表"页的一维索引多配置全部表需要增加行状态
            CfgTableOp curtable = m_reclTable[strCurTableName].tableInfo;
            if (0 == String.Compare(strPageName, "gNB参数表", true))
            {
                string strTabAndIndex = strCurTableName + strIndex;
                string strRowStatusName = curtable.GetRowStatusName();

                //每条实例只增加一次行状态
                if (0 != String.Compare(strTabAndIndex, m_strCurTabAndIndex, true))
                {
                    if (!WriteValueToBuffer(curtable, strIndex, strRowStatusName, "4"))//设置行有效
                        return false;
                    m_strCurTabAndIndex = strTabAndIndex;//记录当前的索引
                }

                //2014-4-23 luoxin DTMUC00212701 行状态需要增加到补丁文件中
                if (strFlag == "1" || strFlag == "2")
                {
                    curtable.InsertInstOrLeaf(strIndex, strRowStatusName);
                }
                //2014-4-23 luoxin end
            }

            if (strFlag == "1" || strFlag == "2")
            {
                curtable.InsertInstOrLeaf(strIndex, strNodeName);
            }

            //更新节点值
            if (!WriteValueToBuffer(curtable, strIndex, strNodeName, strNodeValue))
                return false;

            return true;
        }
        /// <summary>
        /// 二维索引 处理
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTable2(string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName, List<string> m_vectIndexScope)//, string m_strPlanIndex)
        {
            string index1str = m_vectIndexScope[0];
            string index2str = m_vectIndexScope[1];

            Dictionary<int, List<int>> ValStrToInt = new Dictionary<int, List<int>>();
            ParseIndexStrToInt(m_vectIndexScope, ValStrToInt);
            for (int index1 = ValStrToInt[0][0]; index1 <= ValStrToInt[0][1]; index1++)
            {
                for (int index2 = ValStrToInt[1][0]; index2 <= ValStrToInt[1][1]; index2++)
                {
                    string strIndex = "." + index1 + "." + index2;
                    //2016-06-27 luoxin 判断实例是否在配置文件中存在
                    if (!InstanceIsExist(strIndex, strCurTableName))
                        return false;
                    //2016-06-27 luoxin end
                    if (strFlag == "1" || strFlag == "2")
                    {
                        m_reclTable[strCurTableName].tableInfo.InsertInstOrLeaf(strIndex, strNodeName);
                    }

                    //更新节点值
                    if (!WriteValueToBuffer(m_reclTable[strCurTableName].tableInfo, strIndex, strNodeName, strNodeValue))
                        continue;
                }
            }
            return true;
        }
        /// <summary>
        /// 三维索引
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <param name="m_vectIndexScope"></param>
        /// <returns></returns>
        bool DealMoreConfigTable3(string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName, List<string> m_vectIndexScope)//, string m_strPlanIndex)
        {
            Dictionary<int, List<int>> ValStrToInt = new Dictionary<int, List<int>>();
            ParseIndexStrToInt(m_vectIndexScope, ValStrToInt);

            for (int index1 = ValStrToInt[0][0]; index1 <= ValStrToInt[0][1]; index1++)
            {
                for (int index2 = ValStrToInt[1][0]; index2 <= ValStrToInt[1][1]; index2++)
                {
                    for (int index3 = ValStrToInt[2][0]; index3 <= ValStrToInt[2][1]; index3++)
                    {
                        string strIndex = "." + index1 + "." + index2 + "." + index3;

                        //2016-06-27 luoxin 判断实例是否在配置文件中存在
                        if (!InstanceIsExist(strIndex, strCurTableName))
                            return false;
                        //2016-06-27 luoxin end

                        if (strFlag == "1" || strFlag == "2")
                        {
                            m_reclTable[strCurTableName].tableInfo.InsertInstOrLeaf(strIndex, strNodeName);
                        }

                        //更新节点值
                        if (!WriteValueToBuffer(m_reclTable[strCurTableName].tableInfo, strIndex, strNodeName, strNodeValue))
                            continue;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 二维索引 处理
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTable2(string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName,
            List<Dictionary<string, string>> m_indexInfo)//, string m_strPlanIndex)
        {
            Dictionary<int, List<int>> ValStrToInt = new Dictionary<int, List<int>>();
            ParseIndexInfoStrToInt(m_indexInfo, ValStrToInt);
            for (int index1 = ValStrToInt[0][0]; index1 <= ValStrToInt[0][1]; index1++)
            {
                for (int index2 = ValStrToInt[1][0]; index2 <= ValStrToInt[1][1]; index2++)
                {
                    string strIndex = "." + index1 + "." + index2;
                    //2016-06-27 luoxin 判断实例是否在配置文件中存在
                    if (!InstanceIsExist(strIndex, strCurTableName))
                        return false;
                    //2016-06-27 luoxin end
                    if (strFlag == "1" || strFlag == "2")
                    {
                        m_reclTable[strCurTableName].tableInfo.InsertInstOrLeaf(strIndex, strNodeName);
                    }

                    //更新节点值
                    if (!WriteValueToBuffer(m_reclTable[strCurTableName].tableInfo, strIndex, strNodeName, strNodeValue))
                        continue;
                }
            }
            return true;
        }
        /// <summary>
        /// 三维索引
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <param name="m_vectIndexScope"></param>
        /// <returns></returns>
        bool DealMoreConfigTable3(string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName,
            List<Dictionary<string, string>> m_indexInfo)//, string m_strPlanIndex)
        {
            Dictionary<int, List<int>> ValStrToInt = new Dictionary<int, List<int>>();
            ParseIndexInfoStrToInt(m_indexInfo, ValStrToInt);

            for (int index1 = ValStrToInt[0][0]; index1 <= ValStrToInt[0][1]; index1++)
            {
                for (int index2 = ValStrToInt[1][0]; index2 <= ValStrToInt[1][1]; index2++)
                {
                    for (int index3 = ValStrToInt[2][0]; index3 <= ValStrToInt[2][1]; index3++)
                    {
                        string strIndex = "." + index1 + "." + index2 + "." + index3;

                        //2016-06-27 luoxin 判断实例是否在配置文件中存在
                        if (!InstanceIsExist(strIndex, strCurTableName))
                            return false;
                        //2016-06-27 luoxin end

                        if (strFlag == "1" || strFlag == "2")
                        {
                            m_reclTable[strCurTableName].tableInfo.InsertInstOrLeaf(strIndex, strNodeName);
                        }

                        //更新节点值
                        if (!WriteValueToBuffer(m_reclTable[strCurTableName].tableInfo, strIndex, strNodeName, strNodeValue))
                            continue;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 把索引值拆分
        /// </summary>
        /// <param name="m_vectIndexScope"></param>
        /// <param name="indexValStrToInt"></param>
        /// <returns></returns>
        bool ParseIndexStrToInt(List<string> m_vectIndexScope, Dictionary<int, List<int>> indexValStrToInt)
        {
            int indexNum = 0;
            foreach (string indexVal in m_vectIndexScope)
            {
                string [] spStr = indexVal.Split("..".ToArray());
                int iMin = int.Parse(spStr.First());
                int iMax = int.Parse(spStr.Last());// 如果没有 ".." 时，猜想和iMin的值一样
                indexValStrToInt.Add(indexNum, new List<int> { iMin, iMax });
                indexNum++;
            }
            return true;
        }
        /// <summary>
        /// 把索引值拆分
        /// </summary>
        /// <param name="m_vectIndexScope"></param>
        /// <param name="indexValStrToInt"></param>
        /// <returns></returns>
        bool ParseIndexInfoStrToInt(List<Dictionary<string, string>> m_indexInfo, Dictionary<int, List<int>> indexValStrToInt)
        {
            int indexNum = 0;
            
            foreach (var index in m_indexInfo)
            {
                string indexVal = index.Values.ToList()[0];
                string[] spStr = indexVal.Split("..".ToArray());
                int iMin = int.Parse(spStr.First());
                int iMax = int.Parse(spStr.Last());// 如果没有 ".." 时，猜想和iMin的值一样
                indexValStrToInt.Add(indexNum, new List<int> { iMin, iMax });
                indexNum++;
            }
            return true;
        }
        /// <summary>
        /// 修改某(strIndex)实例中某(strNodeName)节点在内存中的值(value)数据
        /// 与 CfgParseSelfExcel 中的同名函数，功能相同
        /// </summary>
        /// <param name="curtable">表实例</param>
        /// <param name="strIndex">修改的实例的索引值</param>
        /// <param name="strNodeName">修改的节点的英文名</param>
        /// <param name="value">修改节点的值</param>
        /// <returns></returns>
        bool WriteValueToBuffer(CfgTableOp curtable, string strCurIndex, string strNodeName, string strNodeValue)
        {
            CfgTableInstanceInfos pInstInfo = null;  //实例信息
            CfgFileLeafNodeOp leafNodeOp = null;     //节点属性
            int InstsPos = 0;                        //实例位置

            // 获得表的某个(strIndex)实例信息
            if (!curtable.GetCfgInstsByIndexDeepCopy(strCurIndex, out pInstInfo, out InstsPos))//是否存在这个索引值的实例
                return false;


            // 获得表的某个节点(strNodeName)信息
            if (!curtable.GetLeafNodesByNodeName(strNodeName, out leafNodeOp))
                return false;

            // 组合节点的信息
            byte[] InstMem = pInstInfo.GetInstMem();                           //1.这个表实例的内容的内存;实例化后表中节点依次排列的内容
            ushort u16FieldLen = leafNodeOp.m_struFieldInfo.u16FieldLen;       //2.这个节点字段的长度
            ushort u16FieldOffset = leafNodeOp.m_struFieldInfo.u16FieldOffset; //3.这个节点在实例内存中相对的位置 ;字段相对记录头偏移量
            string strOMType = leafNodeOp.m_struMibNode.strOMType;
            string asnType = leafNodeOp.m_struMibNode.strMibSyntax;            //asnType
            string strDefaultValue = strNodeValue;                             // 修改点, 在InstMem中把strNodeName的值修改为strNodeValue

            // 修改内存数据
            List<byte[]> byteArray = new List<byte[]>() { InstMem };
            List<int> bytePosL = new List<int>() { u16FieldOffset };
            new CfgOp().WriteToBuffer(byteArray, strDefaultValue, bytePosL, strOMType, u16FieldLen, "", asnType);

            // 重新写回去,实验证明数据已经修改，而且不会修改其他字段
            //curtable.SetInstsInfoValueByIndex(InstsPos, byteArray[0]);

            return true;
        }
        //2016-06-27 luoxin 把实例加入到补丁文件之前先判断该实例在配置文件中是否存在
        bool InstanceIsExist(string strInstIndex, string strCurTableName)
        {
            List<string> vectInstSet = m_reclTable[strCurTableName].tableInfo.GetCfgInstsInstantNum();
            if (vectInstSet.FindIndex(e => e.Equals(strInstIndex)) == -1) //不存在：返回-1，存在：返回位置。
                return false;
            else
                return true;
        }
        /// <summary>
        /// 获取nodeName对应的table表名
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="nodeName"></param>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        bool GetTableName(CfgOp cfgOp, string nodeName, out string strTableName)
        {
            strTableName = cfgOp.m_mibTreeMem.GetTableNameFromDBMibTree(nodeName);
            if (String.Empty == strTableName || "" == strTableName)
            {
                Console.WriteLine(String.Format(
                    "Err ({0}) m_mibTreeMem GetTableName({1}) from m_mibTreeMem is null, continue.(Mib中是否存在或其他原因).\n",
                    nodeName, strTableName));
                g_bw.Write(String.Format("Err ({0}) m_mibTreeMem GetTableName({1}) from m_mibTreeMem is null, continue.(Mib中是否存在或其他原因).\n", nodeName, strTableName).ToArray());
                return false;//continue;
            }
            if (!cfgOp.m_mapTableInfo.ContainsKey(strTableName))
            {
                Console.WriteLine(String.Format("Err ({0}) m_mapTableInfo GetTableName({1}) null, continue.(Mib中'配置文件'是否有修改权限或其他原因).\n", nodeName, strTableName));
                g_bw.Write(String.Format("Err ({0}) m_mapTableInfo GetTableName({1}) null, continue.(Mib中'配置文件'是否有修改权限或其他原因).\n", nodeName, strTableName).ToArray());
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strPageName"></param>
        /// <param name="rowValidCount"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        Dictionary<string, object[,]> GetExcelRangeValue(string strPageName, int rowValidCount, Dictionary<string, string> ColName)
        {
            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColName.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = CfgExcelOp.GetInstance().GetRangeVal(g_strExcelPath, strPageName, ColName[colName] + "1", ColName[colName] + rowValidCount);
                if (null == arry)
                {
                    g_bw.Write(String.Format("Err GetExcelRangeValue ({0}) ({1}) get col({2}) arry null.", 
                        g_strExcelPath, strPageName, colName).ToArray());

                    ColVals = null;
                    return null;
                }
                ColVals.Add(colName, arry);
            }
            return ColVals;
        }
        /// <summary>
        /// 是否是索引节点
        /// </summary>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        bool IsIndexNode(string NodeName)
        {
            if (-1 != NodeName.IndexOf('*'))
                return true;//有'*'是索引节点
            return false;
        }
        /// <summary>
        /// 去掉 * 之后的名字
        /// </summary>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        bool GetNodeName(Dictionary<string, object[,]> ColVals, int lineNo, string strFlag, out string NodeName, out bool bIsIndexNode)
        {
            NodeName = GetCellString(ColVals["NodeName"][lineNo, 1]);
            bIsIndexNode = IsIndexNode(NodeName);           
            if (bIsIndexNode == true && strFlag == "2")
            {
                Console.WriteLine(String.Format("Err lineNo({0}) GetNodeName({1}) is index,but flag({2}) is '2' err.", lineNo, NodeName, strFlag));
                g_bw.Write(String.Format("Err lineNo({0}) GetNodeName({1}) is index,but flag({2}) is '2' err.", lineNo, NodeName, strFlag).ToArray());
                return false;
            }
            int pos = NodeName.IndexOf('*'); // 索引
            int pos2 = NodeName.IndexOf('#');// 行状态
            if (-1 != pos)
            {
                NodeName = NodeName.Substring(0, pos);
            }
            if (-1 != pos2)
            {
                NodeName = NodeName.Substring(0, pos2);
            }

            NodeName = NodeName.TrimEnd(' ');
            return true;
        }
        /// <summary>
        /// 根据flag取节点值
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        bool GetNodeValueByFlag(Dictionary<string, object[,]> ColVals, int lineNo, string strFlag, out string nodeValue )
        {
            if ("1" == strFlag || "0" == strFlag)
                nodeValue = GetCellString(ColVals["DefaultValue"][lineNo, 1]);
            else if ("2" == strFlag)
                nodeValue = GetCellString(ColVals["recommendValue"][lineNo, 1]);
            else
            {
                g_bw.Write(String.Format("GetNodeValueByFlag lineNo({0}) flag({1}) not is 0,1,2.\n",
                    lineNo, strFlag).ToArray());
                nodeValue = "";
                return false;
            }

            if (nodeValue == "")
            {
                g_bw.Write(String.Format("GetNodeValueByFlag lineNo({0}) nodeval is null, flag({1}).\n", 
                    lineNo, strFlag).ToArray());
                return false;
            }
            if (0 < nodeValue.IndexOf(':'))
                nodeValue = nodeValue.Substring(0, nodeValue.IndexOf(':'));
            return true;

        }
        /// <summary>
        /// 获取索引节点的默认值
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <param name="strFlag"></param>
        /// <param name="nodeValue"></param>
        /// <returns></returns>
        bool GetIndexNodeVal(Dictionary<string, object[,]> ColVals, int lineNo, string strFlag, out string nodeValue)
        {
            nodeValue = "";
            if ("2" == strFlag)//5g 新规则， index node ,flag不等为2。
                return false;
            nodeValue = GetCellString(ColVals["DefaultValue"][lineNo, 1]);
            if (nodeValue == "")
                return false;
            if (0 < nodeValue.IndexOf(':'))
                nodeValue = nodeValue.Substring(0, nodeValue.IndexOf(':'));
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        int GetIndexNumFromDBMibTree(CfgOp cfgOp, string strNodeName)
        {
            if (null == cfgOp || "" == strNodeName || null == cfgOp.m_mibTreeMem)
                return -1;
            return cfgOp.m_mibTreeMem.GetIndexNumFromDBMibTree(strNodeName); ;
        }
        /// <summary>
        /// 单元格信息
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        string GetCellString(object cellVal)
        {
            return (cellVal == null) ? "": cellVal.ToString();
        }
        /// <summary>
        /// 找"end" 行,必须存在
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        int GetEndLineNum(string strPageName, Dictionary<string, string> ColName)
        {
            //int rowCount = wks.UsedRange.Rows.Count;
            var exop = CfgExcelOp.GetInstance();
            int rowCount = exop.GetRowCount(g_strExcelPath, strPageName);
            if (-1 == rowCount)
            {
                g_bw.Write(String.Format("Err GetEndLineNum:({0}):({1}), get row count err.", g_strExcelPath, strPageName).ToArray());
                return -1;// false;
            }

            object[,] arry = exop.GetRangeVal(g_strExcelPath, strPageName, ColName["End"] + "1", ColName["End"] + (rowCount + 1));
            for (int row = 1; row < rowCount + 1; row++)
            {
                if (arry[row, 1] == null)
                    continue;
                if (0 == String.Compare("end", arry[row, 1].ToString(), true))
                    return row;
            }

            g_bw.Write(String.Format("Err GetEndLineNum:({0}):({1}), not exist 'end'.", g_strExcelPath, strPageName).ToArray());
            return -2;// false;
        }
        /// <summary>
        /// 是否是有效行： 0,1,2 三种标识需要处理
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns>true 需要进行处理 </returns>
        bool isEffectiveLine(Dictionary<string, object[,]> ColVals, int lineNo, out string strFlag)
        {
            strFlag = GetExcelFlag(ColVals, lineNo);//根据patch标识进行处理
            if (String.Equals(strFlag, "0") || String.Equals(strFlag, "1") || String.Equals(strFlag, "2"))
                return true;
            else
            {
                //g_bw.Write(String.Format("isEffectiveLine : lineNo({0}) flag is not in (0,1,2).\n", lineNo).ToArray());
                return false;
            }
        }
        /// <summary>
        /// 标识  ： ProcessIdentity
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        string GetExcelFlag(Dictionary<string, object[,]> ColVals, int lineNo)
        {
            object[,] arry = ColVals["ProcessIdentity"];
            //根据patch标识进行处理
            if (arry[lineNo, 1] == null)
                return "";
            return arry[lineNo, 1].ToString();
        }
        /// <summary>
        /// "end" 标识 结束
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns>true 结束</returns>
        bool isEndLine(Dictionary<string, object[,]> ColVals, int lineNo)
        {
            object[,] arry = ColVals["End"];

            //根据patch标识进行处理
            if (arry[lineNo, 1] == null)
                return false;

            string strEndVal = arry[lineNo, 1].ToString();
            if (0 == String.Compare("END", strEndVal, true))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取 table 需要修改的实例的个数
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int GetTableInstsNum(string tableName)
        {
            if (!m_reclTable.ContainsKey(tableName))
            {
                return -1;
            }
            return m_reclTable[tableName].GetInstNum();
        }

        /// <summary>
        /// 写文件前的一些处理
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="newFilePath"></param>
        /// <param name="strDBPath"></param>
        /// <returns></returns>
        public bool CreateFilePdg_eNB(CfgOp cfgOp, string newFilePath, string strDBPath)
        {
            WriteHeaderVersionInfoPDG(strDBPath);//Pdg文件头结构
            WriteDataHeadInfoPDG();              // 数据块头
            // 偏移头(1.固定偏移;2.表实例偏移)
            int m_tableNum = GetVectPDGTabNameNum();
            // 1.固定偏移
            uint iFixedHeadOffset = 0;//固定头偏移 Fixed Head
            iFixedHeadOffset += (uint)Marshal.SizeOf(new StruCfgFileHeader(""));      //文件头：956 字节，
            iFixedHeadOffset += (ushort)Marshal.SizeOf(new StruDataHead("init"));     //数据头：24  字节， sizeof(DataHead);
            iFixedHeadOffset += (uint)m_tableNum * (uint)Marshal.SizeOf(new UInt32());//偏移头：4 * 表个数 字节，每个表的偏移量 
            // 2.表实例偏移
            foreach (var tabName in GetVectPDGTabName())
            {
                iFixedHeadOffset = SetTableOffset_PDG(GetCfgTableOpByName(tabName), iFixedHeadOffset);//计算表的偏移量
            }
            return true;
        }
        /// <summary>
        /// 计算表的偏移位置 patch
        /// </summary>
        /// <param name="tableOp"></param>
        /// <param name="isDyTable"></param>
        /// <param name="TableOffset"></param>
        /// <returns></returns>
        public uint SetTableOffset_PDG(CfgTableOp tableOp, uint TableOffset)
        {
            // 表的起始位置：是上一个表的结束位置
            tableOp.SetTableOffsetPDG(TableOffset);
            // 表块：包含3部分
            // 1. 表头
            TableOffset += (uint)Marshal.SizeOf(new StruCfgFileTblInfo("init"));
            // 2. 所有叶子节点的头
            TableOffset += (uint)((uint)Marshal.SizeOf(new StruCfgFileFieldInfo()) * (uint)tableOp.m_LeafNodes.Count);
            // 3. 实例内容 = 每个实例(所有叶子节点信息的长度) * 实例个数
            uint buflen = (uint)tableOp.m_cfgFile_TblInfo.u16RecLen;// 单个记录的有效长度（单位：字节）
            uint InstNum = (uint)GetTableInstsNum(tableOp.m_strTableName);// (uint)tableOp.m_InstIndex2LeafName.Count;
            TableOffset += buflen * InstNum;
            return TableOffset;
        }
        /// <summary>
        /// 写 patch_ex 文件
        /// </summary>
        /// <param name="newFilePath"></param>
        public bool SaveFilePdg_eNB(string newFilePath)
        {
            if (String.Empty == newFilePath)
                return false;

            // 申请内容
            List<byte> allBuff = new List<byte>();

            // 文件头   : 序列化
            allBuff.AddRange(GetFileHeaderToByteArray());// 写入头文件

            // 数据块头 : 序列化
            allBuff.AddRange(GetDataHeadToByteArray());  // 数据块的头

            // 偏移块头 : 序列化
            foreach (var tabName in GetVectPDGTabName())
            {
                byte[] tableOffset = BitConverter.GetBytes((uint)GetCfgTableOpByName(tabName).GetTableOffsetPDG());
                //Array.Reverse(tableOffset);
                allBuff.AddRange(tableOffset);
            }

            //  表块 实例 : 序列化
            foreach (var tabName in GetVectPDGTabName())
            {
                //if (String.Equals("acCfgEntry", tabName))
                //{
                //    Console.WriteLine("\n");
                //}
                //allBuff.AddRange(tableOp.WriteTofilePDG());
                allBuff.AddRange(GetReclistTableByName(tabName).WriteTofilePDG());
            }
            new CfgOp().CfgWriteFile(newFilePath, allBuff.ToArray(), 0);
            return true;
        }
    }

    class ReclistTable
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tableName = "";
        /// <summary>
        /// 从cfgOp中深拷贝的表的内容
        /// </summary>
        public CfgTableOp tableInfo;
        /// <summary>
        /// 初始化：主要是从cfgOp中深拷贝的表的内容。
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="strTabName"></param>
        public ReclistTable(CfgOp cfgOp ,string strTabName)
        {
            if (null == cfgOp || string.Empty == strTabName)
            {
                return;
            }
            tableName = strTabName;
            tableInfo = (CfgTableOp)cfgOp.m_mapTableInfo[strTabName].DeepCopy();
            return;
        }

        /// <summary>
        /// 用于记录补丁文件 ,key :索引，vul:叶子节点列表
        /// </summary>
        Dictionary<string, List<string>> m_InstIndexLeafName = new Dictionary<string, List<string>>();
        /// <summary>
        /// 根据索引值(用来查找实例)，记录需要修改的节点。
        /// </summary>
        /// <param name="strIndex"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public bool AddLeafNameByIndex(string strIndex, string nodeName)
        {
            if (string.Empty == strIndex || string.Empty == nodeName)
            {
                return false;
            }
            // 
            if (m_InstIndexLeafName.ContainsKey(strIndex))
            {
                m_InstIndexLeafName[strIndex].Add(nodeName);
            }
            else
            {
                m_InstIndexLeafName.Add(strIndex, new List<string>() { nodeName });
            }
            return true;
        }
        /// <summary>
        /// 获取实例的个数
        /// </summary>
        /// <returns></returns>
        public int GetInstNum()
        {
            return m_InstIndexLeafName.Count();
        }

        /// <summary>
        /// 表块 的序列化: patch_ex
        /// </summary>
        /// <returns></returns>
        public List<byte> WriteTofilePDG()
        {
            List<byte> tableBytes = new List<byte>();

            //1.表头  StruCfgFileTblInfo
            tableInfo.m_cfgFile_TblInfo.u32RecNum = (uint)GetInstNum();
            if ("nodeBInfo" == tableName)
            {
                tableInfo.m_cfgFile_TblInfo.u16FieldNum = 1;
                tableInfo.m_cfgFile_TblInfo.u16RecLen = 4;
            }
            tableBytes.AddRange(tableInfo.m_cfgFile_TblInfo.StruToByteArray());

            //2.每个叶子的头
            foreach (var leaf in tableInfo.m_LeafNodes)
            {
                // 标记 节点是否可配置
                SetLeafFieldConfigFlagPDG(leaf.m_struFieldInfo, leaf.m_struMibNode.strMibName);
                // 每个叶子节点的头
                tableBytes.AddRange(leaf.m_struFieldInfo.StruToByteArray());
            }

            //3. 表实例 : 表的每个叶子的内容 * 每个表实例
            foreach (var InstleafName in m_InstIndexLeafName)
            {
                string strInstIndex = InstleafName.Key;
                CfgTableInstanceInfos inst = tableInfo.m_cfgInsts.Find(e => String.Compare(e.GetInstantNum(), strInstIndex, true) == 0);
                if (null != inst)
                    tableBytes.AddRange(inst.GetInstMem());
            }
            return tableBytes;
        }
        void SetLeafFieldConfigFlagPDG(StruCfgFileFieldInfo FieldInfo, string strNodeName)
        {
            bool bFind = false;
            foreach (var leafNames in m_InstIndexLeafName.Values)
            {
                if (-1 != leafNames.FindIndex(e => String.Compare(e, strNodeName, true) == 0))
                {
                    bFind = true;
                    break;
                }
            }

            //如果节点本身是不可配置的即使被选中也是不可配置的，如果是可配置的节点在没有被选中的情况下
            //也是不可配置的。索引字段全部为可配置的。add by yangyuming
            if (true == bFind)
            {
                if (FieldInfo.u8ConfigFlag != (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE)
                    FieldInfo.u8ConfigFlag = (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE;
            }
            else
            {
                if (FieldInfo.u8ConfigFlag == (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE)
                    FieldInfo.u8ConfigFlag = (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE;
                else if (FieldInfo.u8ConfigFlag == (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE && FieldInfo.u8FieldTag == 'Y')
                {
                    FieldInfo.u8ConfigFlag = (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE;
                }
            }

        }
    }
    //class ReclistIndex
    //{
    //}
}
