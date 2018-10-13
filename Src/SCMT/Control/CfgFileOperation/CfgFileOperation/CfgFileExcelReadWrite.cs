using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows;
using System.Data.OleDb;
using System.Threading;

using System.Reflection; // 引用这个才能使用Missing字段 
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace CfgFileOperation
{
    class CfgFileExcelReadWrite
    {
        List<Dictionary<string, string>> alamNo = new List<Dictionary<string, string>>();
        void AddNewAlamNo(List<Dictionary<string, string>> alamNoNewOne)
        {
            alamNo.AddRange(alamNoNewOne);
        }

        private Excel.Worksheet g_wks = null;

        public void ReadExcelBook(string FilePath)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = false;//设置调用引用的 Excel文件是否可见
            excel.DisplayAlerts = false;
            Excel.Workbook wb = excel.Workbooks.Open("D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\eNB告警信息表.xls");
            g_wks = (Excel.Worksheet)wb.Worksheets[1]; //索引从1开始 
        }

        public DataSet ReadExcelToDS(string FilePath)
        {
            var path = FilePath;
            string fileSuffix = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(fileSuffix))
                return null;

            //判断Excel文件是2003版本还是2007版本
            string connString = "";
            if (fileSuffix == ".xls")
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            else
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

            string strExcel = "select * from [eNB告警信息表$ ]";
            DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbDataAdapter cmd = new OleDbDataAdapter(strExcel, conn);
            
            conn.Open();
            //cmd.Fill(ds);//, "eNB告警信息表");
            //cmd.Fill(dt);
            
            //DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            //cmd.Fill(dt);
            //string tableName = schemaTable.Rows[0][2].ToString().Trim();
            //List<string> list = new List<string>();
            //for (int i = 0; i < schemaTable.Rows.Count; i++)
            //{
            //    var t1 = schemaTable.Rows[i][0].ToString();
            //    var t2 = schemaTable.Rows[i][2].ToString();
            //    list.Add(schemaTable.Rows[i][2].ToString());
            //}
            //var ta = schemaTable.DataSet;


            return ds;
        }

        public void test3(string FilePath)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                {"告警编号", "B"},//  [{"AlaNumber"}] 
                {"厂家告警级别", "J"},//  [{"AlaDegree"}]
                {"是否需要上报OMCR", "Y"},//  [{"IsReportToOMCR"}]
                {"告警类型", "I"},//  [{"AlaType"}]
                {"清除方式", "Q"},//  [{"ClearStyle"}]
                {"对应北向接口告警标准原因", "X"},//  [{"ItfNProtocolCauseNo"}]
                {"是否为故障类告警", "D"},//  [{"IsFault"}]
                {"主告警编号", "G"},//  [{"AlaSubtoPrimaryNumber"}]
                {"故障类告警清除去抖周期{单位：s}", "AC"},// [{"ClearDeditheringInterval"}]
                {"告警产生去抖周期{单位：s}", "AF"},// [{"CreateDeditheringInterval"}]
                {"告警频次去抖间隔（单位：min）", "AD"},// [{"CompressionInterval"}]
                {"告警频次去抖次数", "AE"},// [{"CompressionRepetitions"}]
                {"告警值含义描述", "AA"},// [{"ValueStyle"}]
                {"故障对象名称_EN", "AV"},// [{"FathernameOfObject"}]
                {"告警不稳定态处理方式", "AW"},// [{"AlaUnstableDispose"}]
                {"不稳定态告警编号", "AX"},// [{"UnstableAlaNum"}]
            };

            Excel.Application excel = new Excel.Application();
            excel.Visible = false;//设置调用引用的 Excel文件是否可见
            excel.DisplayAlerts = false;
            Excel.Workbook wb = excel.Workbooks.Open("D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\eNB告警信息表.xls");
            Excel.Worksheet wks = (Excel.Worksheet)wb.Worksheets[1]; //索引从1开始 
            int rowCount = wks.UsedRange.Rows.Count;//赋值有效行;//有效行，索引从1开始
            int colCount = wks.UsedRange.Column;
            //循环行
            List<Dictionary<string, string>> dd = new List<Dictionary<string, string>>();
            for (int i = 2; i <= rowCount; i++)//
            {
                var row = wks.Rows[i];
                if (wks.Cells[i, dic["告警编号"]].Value2 != null)
                {
                    Dictionary<string, string> ValueCol = new Dictionary<string, string>();
                    foreach (var key in dic.Keys)
                    {
                        var ccc = wks.Cells[i, dic[key]].Value2;
                        var Cellvalue = wks.Cells[i, dic[key]].Value2 == null ? "" : wks.Cells[i, dic[key]].Value2.ToString();
                        ValueCol.Add(key, Cellvalue.ToString());
                    }
                    dd.Add(ValueCol);
                    Console.WriteLine(" i={0}", i);
                }
                else
                {
                    Console.WriteLine("break i={0}", i);
                    break;
                }
            }//2159 af
        }

        public void test4(string FilePath)
        {
            ReadExcelBook( FilePath);
            //int rowCount = wks.UsedRange.Rows.Count;//赋值有效行;//有效行，索引从1开始
            //int colCount = wks.UsedRange.Column;
            //循环行
            //List<Dictionary<string, string>> dd = new List<Dictionary<string, string>>();

            int rowCount = g_wks.UsedRange.Rows.Count;

            test5(rowCount);
        }

        void test5(int rowCount)
        {
            Dictionary<string, string> ddd = new Dictionary<string, string>();
            List<int> iStartEnd_1 = new List<int>() { 0, rowCount / 2 };
            List<int> iStartEnd_2 = new List<int>() { rowCount / 2 +1, rowCount };

            // 继续二叉树分
            new Thread(doSomething0).Start(iStartEnd_1);
            new Thread(doSomething0).Start(iStartEnd_2);

        }
        void doSomething0(object iStartEnd)
        {
            int iStart = ((List<int>)iStartEnd)[0];
            int iEnd = ((List<int>)iStartEnd)[1];
            if (iEnd - iStart > 200)
            {
                List<int> iStartEnd_1 = new List<int>() { iStart, iEnd / 2 };
                List<int> iStartEnd_2 = new List<int>() { iEnd / 2 + 1, iEnd };
                doSomething0(iStartEnd_1);
                doSomething0(iStartEnd_2);
            }
            else
            {
                new Thread(doSomething).Start(iStartEnd);
            }
        }
        private void doSomething(object iStartEnd)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                {"告警编号", "B"},//  [{"AlaNumber"}] 
                {"厂家告警级别", "J"},//  [{"AlaDegree"}]
                {"是否需要上报OMCR", "Y"},//  [{"IsReportToOMCR"}]
                {"告警类型", "I"},//  [{"AlaType"}]
                {"清除方式", "Q"},//  [{"ClearStyle"}]
                {"对应北向接口告警标准原因", "X"},//  [{"ItfNProtocolCauseNo"}]
                {"是否为故障类告警", "D"},//  [{"IsFault"}]
                {"主告警编号", "G"},//  [{"AlaSubtoPrimaryNumber"}]
                {"故障类告警清除去抖周期{单位：s}", "AC"},// [{"ClearDeditheringInterval"}]
                {"告警产生去抖周期{单位：s}", "AF"},// [{"CreateDeditheringInterval"}]
                {"告警频次去抖间隔（单位：min）", "AD"},// [{"CompressionInterval"}]
                {"告警频次去抖次数", "AE"},// [{"CompressionRepetitions"}]
                {"告警值含义描述", "AA"},// [{"ValueStyle"}]
                {"故障对象名称_EN", "AV"},// [{"FathernameOfObject"}]
                {"告警不稳定态处理方式", "AW"},// [{"AlaUnstableDispose"}]
                {"不稳定态告警编号", "AX"},// [{"UnstableAlaNum"}]
            };

            List<Dictionary<string, string>> dd = new List<Dictionary<string, string>>();
            //ddd = new Dictionary<string, string>();
            int iStart = ((List<int>)iStartEnd)[0];
            int iEnd = ((List<int>)iStartEnd)[1];
            for (int i = iStart; i <= iEnd; i++)//
            {
                var row = g_wks.Rows[i];
                if (g_wks.Cells[i, dic["告警编号"]].Value2 != null)
                {
                    Dictionary<string, string> ValueCol = new Dictionary<string, string>();
                    foreach (var key in dic.Keys)
                    {
                        var ccc = g_wks.Cells[i, dic[key]].Value2;
                        var Cellvalue = g_wks.Cells[i, dic[key]].Value2 == null ? "" : g_wks.Cells[i, dic[key]].Value2.ToString();
                        ValueCol.Add(key, Cellvalue.ToString());
                    }
                    dd.Add(ValueCol);
                    Console.WriteLine(" i={0}", i);
                }
                else
                {
                    Console.WriteLine("break i={0}", i);
                    break;
                }
            }//2159 af

            AddNewAlamNo(dd);


        }


        public void test2(string FilePath)
        {
            object missing = System.Reflection.Missing.Value;
            Excel.Application excel = new  Excel.Application();// new Excel.ApplicationClass();//lauch excel application 
                                                               // 以只读的形式打开EXCEL文件 
            Excel.Workbook wb = excel.Application.Workbooks.Open(FilePath, missing, true, missing, missing, missing,
             missing, missing, missing, true, missing, missing, missing, missing, missing);

            //取得第一个工作薄 
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets.get_Item(1);
            //取得总记录行数    (包括标题列) 
            int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数 
                                                         //int columnsint = mySheet.UsedRange.Cells.Columns.Count;//得到列数 

            //取得数据范围区域   (不包括标题列)   
            Excel.Range rng1 = ws.Cells.get_Range("A2", "A" + rowsint);
            Excel.Range rng2 = ws.Cells.get_Range("B2", "B" + rowsint);
            Excel.Range rng3 = ws.Cells.get_Range("C2", "C" + rowsint);
            Excel.Range rng4 = ws.Cells.get_Range("D2", "D" + rowsint);
            object[,] arry1 = (object[,])rng1.Value2;   //get range's value 
            object[,] arry2 = (object[,])rng2.Value2;
            object[,] arry3 = (object[,])rng3.Value2;   //get range's value 
            object[,] arry4 = (object[,])rng4.Value2;

        }

        public void test(string FilePath)
        {
            test4(FilePath);
            //DataTable dt = ReadExcelToDS(FilePath).Tables[0];
            //int rruTypePortCount = dt.Rows.Count; // 数据库中的行有效数据的个数
        }


        ///// <summary>
        ///// 将DataTable数据导入到excel中
        ///// </summary>
        ///// <param name="data">要导入的数据</param>
        ///// <param name="sheetName">要导入的excel的sheet的名称</param>
        ///// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        ///// <param name="fileName">文件路径</param>
        ///// <returns>导入数据的总行数(包含列名那一行)</returns>
        //public static int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten, string fileName)
        //{
        //    if (data == null)
        //    {
        //        throw new ArgumentNullException("data");
        //    }
        //    if (string.IsNullOrEmpty(sheetName))
        //    {
        //        throw new ArgumentNullException(sheetName);
        //    }
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        throw new ArgumentNullException(fileName);
        //    }
            

        //    //////////////
        //    IWorkbook workbook = null;
        //    if (fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
        //    {
        //        workbook = new XSSFWorkbook(); //2007及以上版本
        //    }
        //    else if (fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
        //    {
        //        workbook = new HSSFWorkbook(); //2003版本
        //    }

        //    FileStream fs = null;
        //    try
        //    {
        //        fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //        ISheet sheet;
        //        if (workbook != null)
        //        {
        //            sheet = workbook.CreateSheet(sheetName);
        //        }
        //        else
        //        {
        //            return -1;
        //        }

        //        int j;
        //        int count;
        //        //写入DataTable的列名，写入单元格中
        //        if (isColumnWritten)
        //        {
        //            var row = sheet.CreateRow(0);
        //            for (j = 0; j < data.Columns.Count; ++j)
        //            {
        //                row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
        //            }
        //            count = 1;
        //        }
        //        else
        //        {
        //            count = 0;
        //        }
        //        //遍历循环datatable具体数据项
        //        int i;
        //        for (i = 0; i < data.Rows.Count; ++i)
        //        {
        //            var row = sheet.CreateRow(count);
        //            for (j = 0; j < data.Columns.Count; ++j)
        //            {
        //                row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
        //            }
        //            ++count;
        //        }
        //        //将文件流写入到excel
        //        workbook.Write(fs);
        //        return count;
        //    }
        //    catch (IOException ioex)
        //    {
        //        throw new IOException(ioex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //        }
        //    }
        //}

        //public static object GetCellValue(ICell cell)
        //{
        //    if (null == cell)
        //    {
        //        throw new ArgumentNullException("GetCellValue input para cell is null");
        //    }
        //    switch (cell.CellType)
        //    {
        //        case CellType.Blank:
        //            return "";
        //        case CellType.Boolean:
        //            return cell.BooleanCellValue;
        //        case CellType.Error:
        //            return cell.ErrorCellValue;
        //        //数值型
        //        case CellType.Numeric:
        //            short format = cell.CellStyle.DataFormat;
        //            //对时间格式的处理:(yyyy-MM-dd HH:mm:ss) - 22，日期(yyyy-MM-dd) - 14，时间(HH:mm:ss) 
        //            //- 21，年月(yyyy-MM) - 17，时分(HH:mm) - 20，月日(MM-dd) - 58
        //            if (format == 14 || format == 31 || format == 57 || format == 58 || format == 20)
        //            {
        //                return cell.DateCellValue.ToString(" HH:mm");
        //            }
        //            else
        //            {
        //                return cell.NumericCellValue;
        //            }
        //        case CellType.String:
        //            return cell.StringCellValue;
        //        //公式型
        //        case CellType.Formula:
        //        case CellType.Unknown:
        //        default:
        //            return "=" + cell.CellFormula;
        //    }

        //}

        //public static ISheet ExcelToExtractSheet(string sheetName, bool isFirstRowColumn, string fileName)
        //{
        //    if (string.IsNullOrEmpty(sheetName))
        //    {
        //        MessageBox.Show(sheetName + " null ");
        //        throw new ArgumentNullException(sheetName);
        //    }
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        MessageBox.Show(fileName + " is null ");
        //        throw new ArgumentNullException(fileName);
        //    }
        //    var data = new DataTable();
        //    IWorkbook workbook = null;
        //    FileStream fs = null;
        //    try
        //    {
        //        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //        if (fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
        //        {
        //            workbook = new XSSFWorkbook(fs);

        //        }
        //        else if (fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
        //        {
        //            workbook = new HSSFWorkbook(fs);
        //        }


        //        ISheet sheet = null;
        //        if (workbook != null)
        //        {
        //            sheet = workbook.GetSheet(sheetName);
        //        }
        //        return sheet;

        //    }
        //    catch (IOException ioex)
        //    {
        //        MessageBox.Show(ioex.Message);
        //        return null;

        //        //throw new IOException(ioex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //        return null;
        //    }

        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //        }
        //    }
        //}

        //public static bool GetCellMergedRange(ISheet sheet, int rowIndex, int colunmIndex, out CellRangeAddress range)
        //{
        //    CellRangeAddress inputRange = new CellRangeAddress(rowIndex, rowIndex, colunmIndex, colunmIndex);
        //    int numMergedRegions = sheet.NumMergedRegions;
        //    int loop = 0;

        //    if (false == sheet.IsMergedRegion(inputRange))
        //    {
        //        range = null;
        //        return false;
        //    }
        //    for (loop = 0; loop < numMergedRegions; loop++)
        //    {
        //        range = sheet.GetMergedRegion(loop);
        //        int firstColumn = range.FirstColumn;
        //        int lastColumn = range.LastColumn;
        //        int firstRow = range.FirstRow;
        //        int lastRow = range.LastRow;

        //        if (rowIndex >= firstRow && rowIndex <= lastRow)
        //        {
        //            if (colunmIndex >= firstColumn && colunmIndex <= lastColumn)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    //没有找到，返回true，但是range为空
        //    range = null;
        //    return true;
        //}

        //public static DataSet ExcelToDataSet(string sheetName, bool isFirstRowColumn, string fileName)
        //{
        //    DataSet ds = new DataSet();
        //    ISheet sheet = ExcelToExtractSheet(sheetName, isFirstRowColumn, fileName);
        //    if (null == sheet)
        //    {
        //        MessageBox.Show("请检查文件名 " + fileName + " 以及文件格式是否正确");
        //        return null;
        //    }
        //    //表头
        //    IRow header = sheet.GetRow(sheet.FirstRowNum);
        //    List<int> columns = new List<int>();
        //    DataTable dt = new DataTable();
        //    bool isMerged = false;
        //    for (int columnLoop = 0; columnLoop < header.LastCellNum; columnLoop++)
        //    {
        //        if (null == header.GetCell(columnLoop))
        //        {
        //            dt.Columns.Add(new DataColumn("Columns" + columnLoop.ToString()));
        //        }
        //        else
        //        {
        //            object obj = GetCellValue(header.GetCell(columnLoop));
        //            if (obj == null || obj.ToString() == string.Empty)
        //            {
        //                dt.Columns.Add(new DataColumn("Columns" + columnLoop.ToString()));
        //            }
        //            else
        //            {
        //                dt.Columns.Add(new DataColumn(obj.ToString()));
        //            }
        //        }
        //        columns.Add(columnLoop);
        //    }
        //    //数据
        //    IEnumerator rows = sheet.GetEnumerator();
        //    int rowLoop = sheet.FirstRowNum + 1;
        //    while (rows.MoveNext())
        //    {
        //        //如果没有数据则跳出循环
        //        if (null == sheet.GetRow(rowLoop))
        //        {
        //            break;
        //        }
        //        DataRow dr = dt.NewRow();
        //        bool hasValue = false;
        //        foreach (int columnTemp in columns)
        //        {
        //            if (null == sheet.GetRow(rowLoop).GetCell(columnTemp))
        //            {
        //                dr[columnTemp] = null;
        //            }
        //            else
        //            {
        //                //判断是否为合并单元格，取对应合并初值
        //                CellRangeAddress cellRange;
        //                isMerged = GetCellMergedRange(sheet, rowLoop, columnTemp, out cellRange);
        //                if (isMerged)
        //                {
        //                    dr[columnTemp] = GetCellValue(sheet.GetRow(cellRange.FirstRow).GetCell(cellRange.FirstColumn));
        //                }
        //                else
        //                {
        //                    dr[columnTemp] = GetCellValue(sheet.GetRow(rowLoop).GetCell(columnTemp));
        //                }
        //            }
        //            if (dr[columnTemp] != null && dr[columnTemp].ToString() != string.Empty)
        //            {
        //                hasValue = true;
        //            }
        //        }
        //        if (hasValue)
        //        {
        //            dt.Rows.Add(dr);
        //        }
        //        else
        //        {
        //            //一行里没有任何数据，则终止解析
        //            break;
        //        }
        //        rowLoop++;
        //    }
        //    ds.Tables.Add(dt);

        //    return ds;
        //}

    }
}
