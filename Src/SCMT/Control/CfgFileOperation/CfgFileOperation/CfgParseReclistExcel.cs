using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using MIBDataParser.JSONDataMgr;
using System.IO;

namespace CfgFileOperation
{
    /// <summary>
    /// 4G ： 解析 《RecList.xls》 文件
    /// 5G ： 解析 《参数标定手册》 文件
    /// 格式和逻辑相近
    /// </summary>
    class CfgParseReclistExcel
    {
        //List<string> m_vectIndexScope = null;// new List<string>();//2014-2-12 luoxin 索引取值范围

        /// <summary>
        /// 需要处理的表名
        /// </summary>
        List<string> m_vectPDGTabName = new List<string>();

        ////2014-2-12 luoxin 多实例配置的规划索引
        //string m_strPlanIndex = "";
        //2014-2-12 luoxin 当前索引
        string m_strCurTabAndIndex = "";

        public CfgParseReclistExcel()
        {
            // sheet Cell参数表
            string[] UeType = new string[] { "0:默认", "1:展讯", "2:e500", "3:华为" };
        }

        /// <summary>
        /// 4G:总体处理 reclist
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="strUeType"></param>
        public bool ProcessingExcel(BinaryWriter bw, string strExcelPath, string strFileToDirectory, string strUeType, CfgOp cfgOp)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strFileToDirectory) || (String.Empty == strUeType) || (null == cfgOp))
                return false;

            // 几种模式
            if (0 == String.Compare("0:默认", strUeType, true)) // 不区分大小写，相等
            {
                //strExcelPageArray[0] = "gNB参数表-7";
                //strExcelPageArray[1] = "Cell参数表-8";
                ProcessingExcelUeTypeDefault(strExcelPath, cfgOp);
            }
            //展讯
            else if (0 == String.Compare(strUeType,"1:展讯", true))
            {
                //strExcelPageArray[0] = "gNB参数表-9";
                //strExcelPageArray[1] = "Cell参数表-10";
            }
            //e500
            else if (0 == String.Compare(strUeType,"2:e500", true))
            {
                //strExcelPageArray[0] = "gNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-12";
                //strExcelPageArray[]={"gNB参数表-x2", "Cell参数表-y2"};
            }
            //华为
            else if (0 == String.Compare(strUeType, "3:华为", true))
            {
                // 华为 ： Cell参数表 N; gNB参数表 N;
                //strExcelPageArray[0] = "gNB参数表-13";
                //strExcelPageArray[1] = "Cell参数表-14";
                //strExcelPageArray[]={"gNB参数表-x3", "Cell参数表-y3"};
                //return ProcessingExcel_5GHuaWei(bw, strExcelPath, cfgOp);
            }
            else
            {
                //strExcelPageArray[0] = "eNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-17";
                //strExcelPageArray[] = {"eNB参数表-11", "Cell参数表-17"};
            }
            return true;
        }
        /// <summary>
        /// 5G 生成
        /// <处理规则>
        /// 1. 非索引节点 : 处理标识, 节点取值处理.0: 不处理;1：使用默认值;2：使用推荐值.
        /// 2. 索引节点时 : 处理标识只有0和1,有2就是错误。
        ///    2.1. 当标识为1时, 代表具体的实例的具体索引.例如:
        ///         MIB变量名         | 中文名称          | 取值范围  | 默认值 | 华为:推荐值 | 处理标识
        ///         nrCellUlIotIndex* | 小区上行底噪索引	 | 0..6	     |  0     |             |  1
        ///         含义: ”nrCellUlIotIndex” 在 生成"华为patch"时, 这个值只取 "0"。
        ///    2.2.当标识为0时, 并且存在(<条件1>处理标识为1或2)(<条件2>非索引)的节点才有意义, 这时表示对所有实例有效。
        ///         修改这个表的所有实例，每个实例中这个节点都要修改为"标识"的值.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="strUeType"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        public bool ProcessingExcel_5G(BinaryWriter bw, string strExcelPath, string strFileToDirectory, string strUeType, CfgOp cfgOp)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strFileToDirectory) || (String.Empty == strUeType) || (null == cfgOp))
                return false;

            // 几种模式
            if (0 == String.Compare("0:默认", strUeType, true)) // 不区分大小写，相等
            {
                //strExcelPageArray[0] = "gNB参数表-7";
                //strExcelPageArray[1] = "Cell参数表-8";
                ProcessingExcelUeTypeDefault(strExcelPath, cfgOp);
            }
            //展讯
            else if (0 == String.Compare(strUeType, "1:展讯", true))
            {
                //strExcelPageArray[0] = "gNB参数表-9";
                //strExcelPageArray[1] = "Cell参数表-10";
            }
            //e500
            else if (0 == String.Compare(strUeType, "2:e500", true))
            {
                //strExcelPageArray[0] = "gNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-12";
                //strExcelPageArray[]={"gNB参数表-x2", "Cell参数表-y2"};
            }
            //华为
            else if (0 == String.Compare(strUeType, "3:华为", true))
            {
                // 华为 ： Cell参数表 N; gNB参数表 N;
                //strExcelPageArray[0] = "gNB参数表-13";
                //strExcelPageArray[1] = "Cell参数表-14";
                //strExcelPageArray[]={"gNB参数表-x3", "Cell参数表-y3"};
                return ProcessingExcel_5GHuaWei(bw, strExcelPath, cfgOp);
            }
            else
            {
                //strExcelPageArray[0] = "eNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-17";
                //strExcelPageArray[] = {"eNB参数表-11", "Cell参数表-17"};
            }
            return true;
        }


        public int GetVectPDGTabNameNum()
        {
            return m_vectPDGTabName.Count;
        }
        public List<string> GetVectPDGTabName()
        {
            return m_vectPDGTabName;
        }
        
        /// <summary>
        /// 默认值
        /// </summary>
        /// <param name="wbook"></param>
        /// <param name="excelOp"></param>
        void ProcessingExcelUeTypeDefault(string strExcelPath,  CfgOp cfgOp)
        {
            // 如果是老的 reclist 
            // "Cell参数表"
            Dictionary<string, string> SheetCellColUe0 = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"Q" },  // 处理标识
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "recommendValue", "P"},    // 推荐值
                    { "End","Q"},                // 结束标志
                };

            // "gNB参数表" 展讯
            Dictionary<string, string> SheetGNBColUe0 = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"M" },  // 处理标识
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "recommendValue", "L"},    // 推荐值
                    { "End","Q"},                // 结束标志
                };

            //Excel.Worksheet wks = excelOp.ReadExcelSheet(strExcelPath, "Cell参数表");//
            //if (wks == null)
            //    return;

            DealReclistPageData(strExcelPath, SheetCellColUe0, cfgOp, "Cell参数表");
        }

        /// <summary>
        /// 华为相关数据处理
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        bool ProcessingExcel_5GHuaWei(BinaryWriter bw, string strExcelPath, CfgOp cfgOp)
        {
            bool re = true;
            // "Cell参数表"
            Dictionary<string, string> CellParaCol = new Dictionary<string, string>(){
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值                   
                    { "End","O"},                // 结束标志                   
                    { "recommendValue", "M"},    // 推荐值
                    { "ProcessIdentity" ,"N" },  // 处理标识
                };

            // "gNB参数表" 展讯
            Dictionary<string, string> gNBParaCol = new Dictionary<string, string>(){
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值                    
                    { "End","Q"},                // 结束标志
                    { "recommendValue", "L"},    // 推荐值
                    { "ProcessIdentity" ,"M" },  // 处理标识
                };

            re = DealReclist5GData(bw, strExcelPath, "Cell参数表", CellParaCol, cfgOp);

            return re;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="ColName"></param>
        /// <param name="cfgOp"></param>
        /// <param name="strPageName"></param>
        void DealReclistPageData(string strExcelPath, Dictionary<string, string> ColName, CfgOp cfgOp, string strPageName)
        {
            int rowCount = GetEndLineNum(strExcelPath, strPageName, ColName);// wks.UsedRange.Rows.Count;                  // 获取行数
            var exop = CfgExcelOp.GetInstance();
            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColName.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = exop.GetRangeVal(strExcelPath, strPageName, ColName[colName] + "1", ColName[colName] + rowCount);
                ColVals.Add(colName, arry);
            }

            // 4
            string strCurTableName = "";
            int  m_iCurTabIndexNum = -1;//2014-2-12 luoxin 当前表索引个数
            bool m_bIsMoreInsts = false;//2014-2-12 luoxin 是否多实例配置
            string m_strPlanIndex = ""; //2014-2-12 luoxin 多实例配置的规划索引
            List<string> m_vectIndexScope = null;// new List<string>();//2014-2-12 luoxin 索引取值范围
            for (int currentLine = 4; currentLine < rowCount + 1; currentLine++)
            {
                bool bIsIndexNode = false;          //是否是索引节点                
                if (isEndLine(ColVals, currentLine))//结束标记
                    break;
                string strFlag = GetExcelFlag(ColVals, currentLine);//根据patch标识进行处理
                if (!isEffectiveLine(strFlag))
                    continue;

                //节点名 NodeName, 是否为索引节点 bIsIndexNode
                string NodeName = GetIndexNodeName(GetCellString(ColVals["NodeName"][currentLine, 1]), out bIsIndexNode);               
                string DefaultValue = GetDefaultValue(ColVals["DefaultValue"][currentLine, 1]);//节点值

                //下面是挑选的过程，
                //1.把reclist 中存在 表名tableName 存起来；2.把reclist中每个table下的node存起来3.处理每个reclist中node的默认值。
                /// 1.通过node mibName 找 tableName，判断reclist中表名是否是在当前数据库中存在
                string strTableName = cfgOp.m_mibTreeMem.GetTableNameFromDBMibTree(NodeName);
                if (String.Empty == strTableName)
                    continue;

                ///如果表名在数据库中存在，看看reclist excel lineNo 的节点是否同一个表，防止多次处理同一张表
                if (0 != String.Compare(strCurTableName, strTableName, true))// 不区分大小，不相等时
                {
                    //再次查看是否有这个表
                    if (!cfgOp.m_mapTableInfo.ContainsKey(strTableName))
                        continue;

                    strCurTableName = strTableName;//m_strCurTabName = strTableName;
                    m_vectIndexScope = new List<string>();
                    m_iCurTabIndexNum = cfgOp.m_mibTreeMem.GetIndexNumFromDBMibTree(NodeName);
                    m_bIsMoreInsts = false;
                }

                //标量表
                if (0 == m_iCurTabIndexNum)
                {
                    if (!DealScalarTable(cfgOp, strCurTableName, NodeName, strFlag))
                        continue;
                }
                //表量表
                else
                {
                    if (bIsIndexNode)//处理索引
                    {
                        m_vectIndexScope.Add(DefaultValue);
                        if (0 == String.Compare(strFlag, "2", true))
                        {
                            m_bIsMoreInsts = true;
                            m_strPlanIndex = GetRecommendValue(ColVals, currentLine);//推荐值
                        }
                        continue;
                    }
                    //处理表量表中节点
                    DealTableWithIndex(cfgOp, m_bIsMoreInsts, NodeName, DefaultValue, strFlag,
                        strCurTableName, m_iCurTabIndexNum, strPageName, m_vectIndexScope, m_strPlanIndex);
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strPageName"></param>
        /// <param name="ColName"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        bool DealReclist5GData(BinaryWriter bw, string strExcelPath, string strPageName, 
            Dictionary<string, string> ColName, CfgOp cfgOp)
        {
            bool re = true;
            int rowValidCount = GetEndLineNum(strExcelPath, strPageName, ColName);// 获取行数  
            Dictionary<string, object[,]> ColVals = GetExcelRangeValue(strExcelPath, strPageName, rowValidCount, ColName);// 获取ColName相关的col的数据

            string m_strCurTableName = ""; // 当前处理的表名 
            int m_iCurTabIndexNum = -1;  // 2014-2-12 luoxin 当前表索引个数
            bool m_bIsMoreInsts = false; // 2014-2-12 luoxin 是否多实例配置
            string m_strPlanIndex = "";  // 2014-2-12 luoxin 多实例配置的规划索引
            List<string> m_vectIndexScope = null;//2014-2-12 luoxin 索引取值范围
            //for (int currentLine = 4; currentLine < rowValidCount + 1; currentLine++)
            for (int currentLine = rowValidCount-3; currentLine < rowValidCount + 1; currentLine++)
            {
                bool bIsIndexNode = false;//是否是索引节点

                if (isEndLine(ColVals, currentLine))
                    break;
                
                string strFlag = GetExcelFlag(ColVals, currentLine);//根据patch标识进行处理
                if (!isEffectiveLine(strFlag))
                    continue;

                string NodeName = GetIndexNodeName(
                    GetCellString(ColVals["NodeName"][currentLine, 1]), out bIsIndexNode);//节点名NodeName,是否为索引节点bIsIndexNode              
                string DefaultValue = GetDefaultValue(ColVals["DefaultValue"][currentLine, 1]);//节点值

                /// 下面是挑选的过程，
                /// 1.把reclist 中存在 表名tableName 存起来；2.把reclist中每个table下的node存起来3.处理每个reclist中node的默认值。
                /// 1.通过node mibName 找 tableName，判断reclist中表名是否是在当前数据库中存在
                if (false == IsNewTableAndDeal(cfgOp, NodeName,
                    m_strCurTableName, out m_strCurTableName, 
                    m_iCurTabIndexNum, out m_iCurTabIndexNum, 
                    m_bIsMoreInsts, out m_bIsMoreInsts, m_vectIndexScope))
                {
                    continue;
                }

                //标量表
                if (0 == m_iCurTabIndexNum)
                {
                    if (bIsIndexNode)
                        Console.WriteLine("Err: 自相矛盾。标量表不能有索引节点");
                    if (!DealScalarTable(cfgOp, m_strCurTableName, NodeName, strFlag))
                        continue;
                }
                //表量表
                else
                {
                    //索引节点
                    if (bIsIndexNode)
                    {
                        DealIndexNode(m_vectIndexScope, DefaultValue, strFlag, ColVals, currentLine,
                            m_bIsMoreInsts, out m_bIsMoreInsts, m_strPlanIndex, out m_strPlanIndex);
                        continue;//索引节点没有办法设定特殊值
                    }
                    //其他节点
                    DealTableWithIndex(cfgOp, m_bIsMoreInsts, NodeName, DefaultValue, strFlag,
                        m_strCurTableName, m_iCurTabIndexNum, strPageName, m_vectIndexScope, m_strPlanIndex);
                }
            }

            return re;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strPageName"></param>
        /// <param name="rowValidCount"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        Dictionary<string, object[,]> GetExcelRangeValue(string strExcelPath, string strPageName, int rowValidCount, 
            Dictionary<string, string> ColName)
        {
            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColName.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = CfgExcelOp.GetInstance().GetRangeVal(strExcelPath, strPageName, ColName[colName] + "1", ColName[colName] + rowValidCount);
                ColVals.Add(colName, arry);
            }
            return ColVals;
        }

        /// <summary>
        /// NodeName节点是否是属于新表。如果属于新表，更新相关内容。
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="NodeName"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="m_CurTableName"></param>
        /// <param name="iCurTabIndexNum">输入:表的索引个数</param>
        /// <param name="m_iCurTabIndexNum">输出:初始化表的索引个数</param>
        /// <param name="bIsMoreInsts">输入:是否是多实例</param>
        /// <param name="m_bIsMoreInsts">输出:初始化是否是多实例</param>
        /// <param name="m_vectIndexScope"></param>
        /// <returns>false:continue; true:继续流程(内部处理赋值问题0:不处理;1:处理)</returns>
        bool IsNewTableAndDeal(CfgOp cfgOp, string NodeName, 
            string strCurTableName,out string m_CurTableName,
            int iCurTabIndexNum, out int m_iCurTabIndexNum, 
            bool bIsMoreInsts, out bool m_bIsMoreInsts, List<string> m_vectIndexScope)
        {
            // 先保持数据不变
            m_CurTableName = strCurTableName;
            m_iCurTabIndexNum = iCurTabIndexNum;
            m_bIsMoreInsts = bIsMoreInsts;
            // 处理
            string strTableName = cfgOp.m_mibTreeMem.GetTableNameFromDBMibTree(NodeName);
            if (String.Empty == strTableName)
            {
                return false;//continue;
            }
            ///如果表名在数据库中存在，看看reclist excel lineNo 的节点是否同一个表，防止多次处理同一张表
            if (0 != String.Compare(strCurTableName, strTableName, true))// 不区分大小，不相等时
            {
                //再次查看是否有这个表
                if (!cfgOp.m_mapTableInfo.ContainsKey(strTableName))
                    return false; //continue;
                else
                {
                    m_CurTableName = strCurTableName;
                    m_iCurTabIndexNum = cfgOp.m_mibTreeMem.GetIndexNumFromDBMibTree(NodeName);
                    m_bIsMoreInsts = false;
                    if (m_vectIndexScope != null)
                        m_vectIndexScope.Clear();
                    m_vectIndexScope = new List<string>();
                    return true;
                }
            }
            else
                return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_vectIndexScope"></param>
        /// <param name="strCellVal"></param>
        /// <param name="strFlag"></param>
        /// <param name="ColVals"></param>
        /// <param name="currentLine"></param>
        /// <param name="bIsMoreInsts"></param>
        /// <param name="m_bIsMoreInsts"></param>
        /// <param name="strPlanIndex"></param>
        /// <param name="m_strPlanIndex"></param>
        void DealIndexNode(List<string> m_vectIndexScope, string strCellVal, string strFlag,
            Dictionary<string, object[,]> ColVals, int currentLine,
            bool bIsMoreInsts, out bool m_bIsMoreInsts,
            string strPlanIndex, out string m_strPlanIndex)
        {
            m_bIsMoreInsts = bIsMoreInsts;
            m_strPlanIndex = strPlanIndex;

            m_vectIndexScope.Add(strCellVal);
            if (0 == String.Compare(strFlag, "2", true))
            {
                m_bIsMoreInsts = true;
                m_strPlanIndex = GetRecommendValue(ColVals, currentLine);//推荐值
            }
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
        string GetIndexNodeName(string NodeName, out bool bIsIndexNode)
        {
            bIsIndexNode = IsIndexNode(NodeName);
            int pos = NodeName.IndexOf('*');
            if (-1 != pos)
                return NodeName.Substring(0, pos);
            return NodeName;
        }

        //2014-2-12 luoxin 处理标量表中节点
        bool DealScalarTable(CfgOp cfgOp, string strCurTableName, string strNodeName, string ProcessIdentity)//string strFlag)
        {
            //做补丁文件
            if (0 == String.Compare(ProcessIdentity, "1", true ))
            {
                // reclist 全局变量
                InsertPdgTab(strCurTableName);

                // 在 table 中 存 relict 内容
                if (cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.ContainsKey(".0"))
                    cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName[".0"].Add(strNodeName);
                else
                {
                    cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.Add(".0", new List<string>() { strNodeName });
                }
            }
            if (0 == String.Compare(ProcessIdentity, "2", true))
            {
                //do something copy by quyaxin
            }


            return true;
        }

        /// <summary>
        /// 对实例的处理
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="m_bIsMoreInsts"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealTableWithIndex(CfgOp cfgOp, bool m_bIsMoreInsts, string strNodeName, string strNodeValue, 
            string strFlag, string strCurTableName, int m_iCurTabIndexNum, string strPageName, List<string> m_vectIndexScope,
            string m_strPlanIndex)
        {
            //单实例配置
            if (!m_bIsMoreInsts)
            {
                if (!DealSingleConfigTable(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName))
                    return false;
            }
            //多实例配置
            else
            {
                if (!DealMoreConfigTable(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName, 
                    m_iCurTabIndexNum, strPageName, m_vectIndexScope, m_strPlanIndex))
                {
                    return false;
                }
            }

            return true;
        }


        //2014-2-12 luoxin 处理单实例配置表节点
        bool DealSingleConfigTable(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, string strCurTableName )
        {
            if (0 == String.Compare(strFlag, "1", true))
            {
                InsertPdgTab(strCurTableName);//做补丁文件 reclist 全局变量

                List<string> InstINumList = cfgOp.m_mapTableInfo[strCurTableName].GetCfgInstsInstantNum();
                foreach (var strIndex in InstINumList)
                    //foreach (var mib in cfgOp.m_mapTableInfo[strCurTableName].m_LeafNodes)//找到每个实例下的所有叶子
                    cfgOp.m_mapTableInfo[strCurTableName].InsertInstOrLeaf(strIndex, strNodeName);//做补丁文件
            }
            return true;
        }

        //2014-2-12 luoxin 处理多实例配置表节点
        bool DealMoreConfigTable(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, string strCurTableName, 
            int m_iCurTabIndexNum, string strPageName, List<string> m_vectIndexScope, string m_strPlanIndex)
        {
            string strIndex = "";

            //一维索引
            if (1 == m_iCurTabIndexNum)
            {
                if (!DealMoreConfigTableOneDimensional(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName, 
                    strPageName, m_strPlanIndex))
                    return false;
            }
            //二维索引
            if (2 == m_iCurTabIndexNum)
            {
                DealMoreConfigTableTwoDimensionalD(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName, 
                    strPageName, m_vectIndexScope, m_strPlanIndex);
            }

            return true;
        }

        /// <summary>
        /// 一维索引 处理
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTableOneDimensional(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, 
            string strCurTableName, string strPageName, string m_strPlanIndex)
        {
            string strIndex = "." + m_strPlanIndex;//一维索引

            //2016-06-27 luoxin 判断实例是否在配置文件中存在
            if (!InstanceIsExist(cfgOp, strIndex, strCurTableName))
                return false;
            //2016-06-27 luoxin end

            //"gNB参数表"页的一维索引多配置全部表需要增加行状态
            CfgTableOp curtable = cfgOp.m_mapTableInfo[strCurTableName];
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
                if (0 == String.Compare(strFlag, "1", true))
                {
                    InsertPdgTab(strCurTableName);
                    curtable.InsertInstOrLeaf(strIndex, strRowStatusName);
                }
                //2014-4-23 luoxin end
            }

            if (0 == String.Compare(strFlag, "1", true))
            {
                InsertPdgTab(strCurTableName);//做补丁文件
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
        /// <param name="cfgOp"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTableTwoDimensionalD(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, 
            string strCurTableName, string strPageName, List<string> m_vectIndexScope, string m_strPlanIndex)
        {
            string strFirstIndexScope = m_vectIndexScope[0];

            strFirstIndexScope = strFirstIndexScope.Replace("..", "-");
            int iFirstIndexMin = int.Parse(strFirstIndexScope.Substring(0, strFirstIndexScope.IndexOf('-')));
            int iFirstIndexMax = int.Parse(strFirstIndexScope.Substring(strFirstIndexScope.IndexOf('-') + 1));

            for (int iFirstIndex = iFirstIndexMin; iFirstIndex <= iFirstIndexMax; iFirstIndex++)
            {
                string strFirstIndex = String.Format(".{0}", iFirstIndex);
                string strIndex = strFirstIndex + "." + m_strPlanIndex;

                //2016-06-27 luoxin 判断实例是否在配置文件中存在
                if (!InstanceIsExist(cfgOp, strIndex, strCurTableName))
                    return false;
                //2016-06-27 luoxin end

                if (0 == String.Compare(strFlag, "1", true))
                {
                    InsertPdgTab(strCurTableName);//做补丁文件
                    cfgOp.m_mapTableInfo[strCurTableName].InsertInstOrLeaf(strIndex, strNodeName);
                }

                //更新节点值
                if (!WriteValueToBuffer(cfgOp.m_mapTableInfo[strCurTableName], strIndex, strNodeName, strNodeValue))
                    continue;
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
        bool WriteValueToBuffer(CfgTableOp curtable, string strCurIndex, string strNodeName,  string strNodeValue)
        {
            CfgTableInstanceInfos pInstInfo = null;  //实例信息
            CfgFileLeafNodeOp leafNodeOp = null;     //节点属性
            int InstsPos = 0;                        //实例位置

            // 获得表的某个(strIndex)实例信息
            if (!curtable.GetCfgInstsByIndex(strCurIndex, out pInstInfo, out InstsPos))//是否存在这个索引值的实例
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
        bool InstanceIsExist(CfgOp cfgOp, string strInstIndex, string strCurTableName)
        {
            List<string> vectInstSet = cfgOp.m_mapTableInfo[strCurTableName].GetCfgInstsInstantNum();
            if (vectInstSet.FindIndex(e => e.Equals(strInstIndex)) == -1) //不存在：返回-1，存在：返回位置。
                return false;
            else
                return true;
        }

        ///// <summary>
        ///// 从内存中查询节点的表名
        ///// </summary>
        ///// <param name="strNodeName"></param>
        ///// <returns></returns>
        //string GetTableNameFromDBMibTree(string strNodeName)
        //{
        //    if (null == mibTreeMem || string.Empty == strNodeName)
        //        return "";
        //    if (!mibTreeMem.pMapMibNodeByName.ContainsKey(strNodeName))
        //        return "";

        //    return mibTreeMem.pMapMibNodeByName[strNodeName].strTableName;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="strNodeName"></param>
        ///// <returns></returns>
        //int GetIndexNumFromDBMibTree(string strNodeName)
        //{
        //    if (null == mibTreeMem || string.Empty == strNodeName)
        //        return -1;
        //    if (!mibTreeMem.pMapMibNodeByName.ContainsKey(strNodeName))
        //        return -1;
        //    var ddd = mibTreeMem.pMapMibNodeByName[strNodeName];

        //    return mibTreeMem.pMapMibNodeByName[strNodeName].nIndexNum;
        //}
        /// <summary>
        /// 把
        /// </summary>
        /// <param name="strTabName"></param>
        public void InsertPdgTab(string strTabName)
        {
            if (m_vectPDGTabName.FindIndex(e => e.Equals(strTabName)) == -1) //不存在：返回-1，存在：返回位置。
                m_vectPDGTabName.Add(strTabName);
        }
        /// <summary>
        /// 判断是否结束行
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        int GetEndLineNum(string strExcelPath, string strPageName , Dictionary<string, string> ColName)
        {
            //int rowCount = wks.UsedRange.Rows.Count;
            var exop = CfgExcelOp.GetInstance();
            int rowCount = exop.GetRowCount(strExcelPath, strPageName);
            if (-1 == rowCount)
            {
                //bw.Write(String.Format("Err:ProcessingExcelRru ({0}):({1}), get row count err.", strExcelPath, strSheet));
                return -1;// false;
            }
            object[,] arry = exop.GetRangeVal(strExcelPath, strPageName, ColName["End"] + "1", ColName["End"] + (rowCount+1));
            for (int row = 1; row < rowCount + 1; row++)
            {
                if (arry[row, 1] == null)
                    continue;
                if (0==String.Compare("end", arry[row, 1].ToString(), true))
                    return row;
            }
            return rowCount;
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
        /// 推荐值, 
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        string GetRecommendValue(Dictionary<string, object[,]> ColVals, int lineNo)
        {
            object[,] arry = ColVals["recommendValue"];

            //根据patch标识进行处理
            if (arry[lineNo, 1] == null)
                return "";
            if (String.Empty == arry[lineNo, 1].ToString())
                return "";
            return arry[lineNo, 1].ToString();
        }

        /// <summary>
        /// 是否是有效行： 0,1,2 三种标识需要处理
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns>true 需要进行处理 </returns>
        bool isEffectiveLine(string ProcessIdentity)
        {
            if (String.Equals(ProcessIdentity, "0") || String.Equals(ProcessIdentity, "1") || String.Equals(ProcessIdentity, "2"))
                return true;
            else
                return false;
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
        /// 单元格信息
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        string GetCellString(object cellVal)
        {
            if (cellVal == null)
                return "";
            else
                return cellVal.ToString();
        }

        /// <summary>
        /// 节点值
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        string GetDefaultValue(object cellVal)
        {
            string defaultValue = GetCellString(cellVal);
            if (0 < defaultValue.IndexOf(':'))
            {
                defaultValue = defaultValue.Substring(0, defaultValue.IndexOf(':'));
            }
            return defaultValue;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="leafName"></param>
        /// <returns></returns>
        bool IsIndexNode(string strNodeName, out string leafName)
        {
            if (-1 != strNodeName.IndexOf('*'))
            {
                leafName = strNodeName.Remove(strNodeName.IndexOf('*'));//leafName = strNodeName.Substring(0, strNodeName.IndexOf('*'));
                return true;
            }
            else
            {
                leafName = strNodeName;
                return false;
            }
        }

        ///// <summary>
        ///// 把lm.mdb mibtree 安行存入内存，方便使用
        ///// </summary>
        ///// <param name="strFileToDirectory"></param>
        //void ProcessingMdbData(string strFileToDirectory)
        //{
        //    //string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
        //    mibTreeMem = new CfgParseDBMibTreeToMemory();
        //    mibTreeMem.ReadMibTreeToMemory(strFileToDirectory);

        //}

        
    }
}
