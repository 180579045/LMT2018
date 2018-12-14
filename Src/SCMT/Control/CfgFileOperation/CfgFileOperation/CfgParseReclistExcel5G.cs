using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <summary>
        /// 某种终端要处理的表
        /// </summary>
        List<ReclistTable> RecTable;
        /// <summary>
        /// 某种终端要处理的表名
        /// </summary>
        List<string> m_vectPDGTabName = new List<string>();

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
        public bool ProcessingExcel(BinaryWriter bw, string strExcelPath, string strUeType, CfgOp cfgOp)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strUeType) || (null == cfgOp) || (null == bw))
                return false;
            if (!File.Exists(strExcelPath))
            {
                bw.Write(String.Format("CfgParseReclistExcel5G : ({0}) 文件不存在！", strExcelPath).ToArray());
                return false;
            }

            g_bw = bw;
            g_strExcelPath = strExcelPath;


            // 几种模式
            if (0 == String.Compare("0:升级发布", strUeType, true)) // 不区分大小写，相等
            {
                ProcessingExcelBbu(cfgOp);
            }
            else if (0 == String.Compare("4:恢复默认配置", strUeType, true)) // 不区分大小写，相等
            {
                ProcessingExcelDefault(cfgOp);
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
        /// 模式 : 0:升级发布
        /// </summary>
        /// <param name="wbook"></param>
        /// <param name="excelOp"></param>
        void ProcessingExcelBbu(CfgOp cfgOp)
        {
            // "Cell参数表"
            Dictionary<string, string> CellCol = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"H" },  // 处理标识
                    { "recommendValue", "E"},    // 推荐值    标识为2使用
                };
            // 合并两个字典
            CellCol = CellCol.Concat(SameCol).ToDictionary(k => k.Key, v => v.Value);
            DealReclist("Cell参数表", CellCol, cfgOp);

            //// "gNB参数表" 
            //Dictionary<string, string> gNBCol = new Dictionary<string, string>(){
            //        { "ProcessIdentity" ,"H" },  // 处理标识
            //        { "recommendValue", "E"},    // 推荐值    标识为2使用
            //    };
            //DealReclist("gNB参数表", gNBCol, cfgOp);
        }

        /// <summary>
        /// 模式 : 默认值
        /// </summary>
        /// <param name="wbook"></param>
        /// <param name="excelOp"></param>
        void ProcessingExcelDefault(CfgOp cfgOp)
        {
            // "Cell参数表"
            Dictionary<string, string> CellCol = new Dictionary<string, string>(){
                    
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "End","O"},                // 结束标志

                    { "ProcessIdentity" ,"Q" },  // 处理标识
                    { "recommendValue", "E"},    // 推荐值默认
                };

            // "gNB参数表" 展讯
            Dictionary<string, string> gNBCol = new Dictionary<string, string>(){                    
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "End","Q"},                // 结束标志

                    { "ProcessIdentity" ,"M" },  // 处理标识
                    { "recommendValue", "L"},    // 推荐值
                };

            //Excel.Worksheet wks = excelOp.ReadExcelSheet(strExcelPath, "Cell参数表");//
            //if (wks == null)
            //    return;

            //DealReclist("Cell参数表", SheetCellColUe0, cfgOp);
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
        bool DealReclist(string strPageName, Dictionary<string, string> ColName, CfgOp cfgOp)
        {
            int rEndNo = GetEndLineNum(strPageName, ColName);// 获取结束的行数
            if (rEndNo < 0)
                return false;
            Dictionary<string, object[,]> ColVals = GetExcelRangeValue(strPageName, rEndNo, ColName);// 获取ColName相关的col的数据
            if (null == ColVals)
                return false;

            //每个表的用到的
            string m_strCurTableName = ""; // 当前处理的表名 
            int m_iCurTabIndexNum = -1;  // 2014-2-12 luoxin 当前表索引个数
            bool m_bIsMoreInsts = false; // 2014-2-12 luoxin 是否多实例配置
            string m_strPlanIndex = "";  // 2014-2-12 luoxin 多实例配置的规划索引
            List<string> m_vectIndexScope = null;//2014-2-12 luoxin 索引取值范围
            for (int currentLine = 4; currentLine < rEndNo + 1; currentLine++)
            {
                bool bIsIndexNode = false;//是否是索引节点
                string strFlag = "";      //根据patch标识进行处理
                string nodeName = "";     //节点名NodeName, 
                string nodeValue = "";    //根据 Flag 取不同的值, 0,1取默认值，2取推荐值
                string strTableName = ""; //节点所属于的table
                if (isEndLine(ColVals, currentLine))
                    break;
                // 是否有效行
                if (!isEffectiveLine(ColVals, currentLine, out strFlag))
                    continue;
                // 是否为索引节点bIsIndexNode, 如果是索引节点是否为flag为2。
                if (!GetNodeName(ColVals, currentLine, strFlag, out nodeName, out bIsIndexNode))
                    return false;
                //根据 Flag 取不同的值, 0,1取默认值，2取推荐值
                if (!GetNodeValueByFlag(ColVals, currentLine, strFlag, out nodeValue))//节点值
                    continue;
                //节点所属于的table
                if (GetTableName(cfgOp, nodeName, out strTableName))
                    continue;

                /// 下面是挑选的过程，
                /// 1.把reclist 中存在 表名tableName 存起来；2.把reclist中每个table下的node存起来3.处理每个reclist中node的默认值。
                /// 1.通过node mibName 找 tableName，判断reclist中表名是否是在当前数据库中存在
                if (0 != String.Compare(m_strCurTableName, strTableName, true))
                {
                    m_strCurTableName = strTableName;
                    m_iCurTabIndexNum = cfgOp.m_mibTreeMem.GetIndexNumFromDBMibTree(nodeName);
                    if (true == bIsIndexNode && 0 == m_iCurTabIndexNum)
                        Console.WriteLine("Err: 自相矛盾。标量表不能有索引节点");
                    m_bIsMoreInsts = false;
                }

                InsertPdgTab(m_strCurTableName, strFlag);// 更新表

                //标量表
                if (0 == m_iCurTabIndexNum)
                {
                    DealScalarTable(cfgOp, m_strCurTableName, nodeName, strFlag);
                }
                //表量表
                else
                {
                    ////索引节点
                    //if (bIsIndexNode)
                    //{
                    //    DealIndexNode(m_vectIndexScope, DefaultValue, strFlag, ColVals, currentLine,
                    //        m_bIsMoreInsts, out m_bIsMoreInsts, m_strPlanIndex, out m_strPlanIndex);
                    //    continue;//索引节点没有办法设定特殊值
                    //}
                    ////其他节点
                    //DealTableWithIndex(cfgOp, m_bIsMoreInsts, NodeName, DefaultValue, strFlag,
                    //    m_strCurTableName, m_iCurTabIndexNum, strPageName, m_vectIndexScope, m_strPlanIndex);
                }
            }

            return true;
        }
        //2014-2-12 luoxin 处理标量表中节点
        bool DealScalarTable(CfgOp cfgOp, string strCurTableName, string strNodeName, string ProcessIdentity)//string strFlag)
        {
            //做补丁文件
            if (0 == String.Compare(ProcessIdentity, "1", true))
            {
                // reclist 全局变量
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
        /// strFlag 为1或2时，更新 strTabName 表
        /// </summary>
        /// <param name="strTabName"></param>
        public void InsertPdgTab(string strTabName, string strFlag)
        {
            if ("1" == strFlag || "2" == strFlag)
            {
                if (m_vectPDGTabName.FindIndex(e => e.Equals(strTabName)) == -1) //不存在：返回-1，存在：返回位置。
                {
                    m_vectPDGTabName.Add(strTabName);
                }
            }
            return;
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
                Console.WriteLine(String.Format("Err ({0}) GetTableName({1}) null, continue.", nodeName, strTableName));
                g_bw.Write(String.Format("Err ({0}) GetTableName({1}) null, continue.", nodeName, strTableName).ToArray());
                return false;//continue;
            }
            if (!cfgOp.m_mapTableInfo.ContainsKey(strTableName))
                return false;
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
                    g_bw.Write(String.Format("Err GetExcelRangeValue ({0}) ({1}) get col({2}) arry null.", g_strExcelPath, strPageName, colName));

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
            int pos = NodeName.IndexOf('*');
            if (-1 != pos)
            {
                NodeName = NodeName.Substring(0, pos);
            }
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
                nodeValue = "";

            if (nodeValue == "")
                return false;
            if (0 < nodeValue.IndexOf(':'))
                nodeValue = nodeValue.Substring(0, nodeValue.IndexOf(':'));
            return true;

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
                g_bw.Write(String.Format("Err GetEndLineNum:({0}):({1}), get row count err.", g_strExcelPath, strPageName));
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

            g_bw.Write(String.Format("Err GetEndLineNum:({0}):({1}), not exist 'end'.", g_strExcelPath, strPageName));
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
    }

    class ReclistTable
    {
        string tableName;

        /// <summary>
        /// 可能要保存相关的索引
        /// CfgFileLeafNodeIndexInfoOp 中有相关的取值
        /// </summary>
        List<string> index;
        /// <summary>
        /// 主要保存
        /// 1. 节点名
        /// 2. 最后取值
        /// 3. 
        /// </summary>
        List<CfgFileLeafNodeOp> leafList;
    }
    class ReclistIndex
    {

    }
}
