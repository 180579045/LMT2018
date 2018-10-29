using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using MIBDataParser.JSONDataMgr;

namespace CfgFileOperation
{
    /// <summary>
    /// 4G ： 解析 《RecList.xls》 文件
    /// 5G ： 解析 《参数标定手册》 文件
    /// 格式和逻辑相近
    /// </summary>
    class CfgReadReclistExcel
    {
        private Dictionary<string, string> ColsNameCell = null;//Cell参数表

        private Dictionary<string, string> ColsNamegNB = null; //Cell参数表

        Dictionary<string, string> SheetCellColUe0;
        Dictionary<string, string> SheetGNBColUe0;

        public CfgReadReclistExcel()
        {
            // sheet Cell参数表
            string[] UeType = new string[] { "0:默认", "1:展讯", "2:e500", "3:华为" };
        }

        public void ProcessingExcel(string strExcelPath, string strFileToDirectory, string strUeType)
        {

            if ((String.Empty == strExcelPath) || (String.Empty == strFileToDirectory) || (String.Empty == strUeType))
                return;

            CfgExcelOp excelOp = new CfgExcelOp();
            if (excelOp == null)
                return;

            //strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            if (wbook == null)
                return;

            //Excel.Worksheet wks = excelOp.ReadExcelSheet(wbook, strSheet);//
            //if (wks == null)
            //    return;

            // 获取 lm.mdb中数据
            ProcessingMdbData(strFileToDirectory);



            if (0 == String.Compare("0:默认", strUeType, true)) // 不区分大小写，相等
            {
                ProcessingExcelUeType0(wbook, excelOp);
            }

            //
            //DealReclistPageData(string strCfgName, _WorksheetPtr ptrSingleSheet, int iPatchFlagColumn, CString strPageName)
        }

        void ProcessingExcelUeType0(Excel.Workbook wbook, CfgExcelOp excelOp)
        {
            // "Cell参数表"
            SheetCellColUe0 = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"K" },  // 处理标识
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "recommendValue", "J"},    // 推荐值
                    { "End","Q"},                // 结束标志
                };

            // "gNB参数表" eNB
            SheetGNBColUe0 = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"K" },  // 处理标识
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "Q"},      // 默认值
                    { "recommendValue", "P"},    // 推荐值
                    { "End","K"},                // 结束标志
                };

            Excel.Worksheet wks = excelOp.ReadExcelSheet(wbook, "Cell参数表");//
            if (wks == null)
                return;

            DealReclistPageData(wks, SheetCellColUe0);
        }

        void DealReclistPageData(Excel.Worksheet wks, Dictionary<string, string> ColName)
        {
            int rowCount = GetEndLineNum(wks, ColName);// wks.UsedRange.Rows.Count;                  // 获取行数

            bool bIsIndexNode = false;//是否是索引节点


            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColName.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = (object[,])wks.Cells.get_Range(ColName[colName] + "1", ColName[colName] + rowCount).Value2;
                ColVals.Add(colName, arry);
            }
            
            // 4
            for (int currentLine = 4; currentLine < rowCount + 1; currentLine++)
            {
                //根据patch标识进行处理
                if (!isEffectiveLine(ColVals, currentLine))
                    continue;

                //节点名
                string NodeName = GetCellString(ColVals["NodeName"][currentLine, 1]);
                //节点值
                string DefaultValue = GetDefaultValue(ColVals["DefaultValue"][currentLine, 1]);
                //是否是索引节点,并且处理节点名
                bIsIndexNode = IsIndexNode(NodeName, out NodeName);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        int GetEndLineNum(Excel.Worksheet wks, Dictionary<string, string> ColName)
        {
            int rowCount = wks.UsedRange.Rows.Count;
            object[,] arry = (object[,])wks.Cells.get_Range(ColName["End"] + "1", ColName["End"] + rowCount).Value2;

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
        /// 
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        bool isEffectiveLine(Dictionary<string, object[,]> ColVals, int lineNo)
        {
            object[,] arry = ColVals["ProcessIdentity"];

            //根据patch标识进行处理
            if (arry[lineNo, 1] == null)
                return false;

            string ProcessIdentity = arry[lineNo, 1].ToString();
            if (String.Equals(ProcessIdentity, "0") || String.Equals(ProcessIdentity, "1") || String.Equals(ProcessIdentity, "2"))
                return true;
            else
                return false;
        }

        string GetCellString(object cellVal)
        {
            if (cellVal == null)
                return "";
            else
                return cellVal.ToString();
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFileToDirectory"></param>
        void ProcessingMdbData(string strFileToDirectory)
        {
            string strSQL = "select * from MibTree order by ExcelLine";// ("select * from MibTree where DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            DataSet MibdateSet = RecordByAccessDb(strFileToDirectory, strSQL);

            
            StruMibNode  pTempNode = new StruMibNode();

        }
        struct StruMibNodeReclist
        {
            StruMibNode baseNode;

            //string strOID = "";
            //string strMibName = "";
            //string strMibVal_AllList = "";
            //string strMibDesc = ""; /*描述*/
            //string strMibVal_List = "";
            //string strIsLeaf = ""; /*是否是叶子*/
            //string strParentOID = "";
            //string strInstanceNum = ""; //--add by cuidairui 2009-08-04
            //string strMibSyntax = "";
            //string strBitSegStartOffset = "";
            //string csDefValue = "";//于晓伟加
            //string csBasicDataType = "";//于晓伟 2009-11-09添加
            //string strMibUnit = "";
            //string strIndexOID = "";
            //string strMMLName = "";//add by cuidairui 2009-10-30


        }



        private DataSet RecordByAccessDb(string fileName, string sqlContent)
        {
            DataSet dateSet = new DataSet();
            AccessDBManager mdbData = new AccessDBManager(fileName);//fileName = "D:\\C#\\SCMT\\lm.mdb";
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
