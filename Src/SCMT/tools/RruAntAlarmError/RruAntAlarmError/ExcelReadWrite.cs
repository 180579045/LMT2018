using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RruAntAlarmError
{
    class ExcelReadWrite
    {
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>导入数据的总行数(包含列名那一行)</returns>
        public static int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten, string fileName)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (string.IsNullOrEmpty(sheetName))
            {
                throw new ArgumentNullException(sheetName);
            }
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(fileName);
            }
            IWorkbook workbook = null;
            if (fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
            {
                workbook = new XSSFWorkbook(); //2007及以上版本
            }
            else if (fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
            {
                workbook = new HSSFWorkbook(); //2003版本
            }

            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                ISheet sheet;
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                int j;
                int count;
                //写入DataTable的列名，写入单元格中
                if (isColumnWritten)
                {
                    var row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }
                //遍历循环datatable具体数据项
                int i;
                for (i = 0; i < data.Rows.Count; ++i)
                {
                    var row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                //将文件流写入到excel
                workbook.Write(fs);
                return count;
            }
            catch (IOException ioex)
            {
                throw new IOException(ioex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        public static object GetCellValue(ICell cell)
        {
            if (null == cell)
            {
                throw new ArgumentNullException("GetCellValue input para cell is null");
            }
            switch(cell.CellType)
            {
                case CellType.Blank:
                    return "";
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;  
                    //数值型
                case CellType.Numeric:
                    short format = cell.CellStyle.DataFormat;
                    //对时间格式的处理:(yyyy-MM-dd HH:mm:ss) - 22，日期(yyyy-MM-dd) - 14，时间(HH:mm:ss) 
                    //- 21，年月(yyyy-MM) - 17，时分(HH:mm) - 20，月日(MM-dd) - 58
                    if (format == 14 || format == 31 || format == 57 || format == 58 || format == 20)
                    {
                        return cell.DateCellValue.ToString(" HH:mm");
                    }
                    else
                    {
                        return cell.NumericCellValue;
                    }
                case CellType.String:
                    return  cell.StringCellValue;
                //公式型
                case CellType.Formula:
                case CellType.Unknown:
                default:
                     return "=" + cell.CellFormula;
            }

        }

        public static ISheet ExcelToExtractSheet(string sheetName, bool isFirstRowColumn, string fileName)
        {
            if (string.IsNullOrEmpty(sheetName))
            {
                MessageBox.Show(sheetName + " null ");
                throw new ArgumentNullException(sheetName);
            }
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(fileName + " is null ");
                throw new ArgumentNullException(fileName);
            }
            var data = new DataTable();
            IWorkbook workbook = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                {
                    workbook = new XSSFWorkbook(fs);

                }
                else if (fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }


                ISheet sheet = null;
                if (workbook != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                return sheet;

            }
            catch (IOException ioex)
            {
                MessageBox.Show(ioex.Message);
                return null;

                //throw new IOException(ioex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return null;
            }

            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        public static ICell GetMergeCellValue(ISheet sheet, CellRangeAddress range)
        {
            for (int row = range.FirstRow; row < range.LastRow; row++)
            {
                for(int column = range.FirstColumn; column < range.LastRow; column++)
                {
                    ICell cell = sheet.GetRow(row).GetCell(column);
                    if (cell != null)
                    {
                        return cell;
                    }
                }
            }
            return null;
        }

        public static bool GetCellMergedRange(ISheet sheet, int rowIndex, int colunmIndex, out CellRangeAddress range)
        {
            CellRangeAddress inputRange = new CellRangeAddress(rowIndex, rowIndex, colunmIndex, colunmIndex);
            int numMergedRegions = sheet.NumMergedRegions;
            int loop = 0;

            if (false == sheet.IsMergedRegion(inputRange))
            {
                range = null;
                return false;
            }
            for (loop = 0; loop < numMergedRegions; loop++)
            {
                range = sheet.GetMergedRegion(loop);
                int firstColumn = range.FirstColumn;
                int lastColumn = range.LastColumn;
                int firstRow = range.FirstRow;
                int lastRow = range.LastRow;

                if (rowIndex >= firstRow && rowIndex <= lastRow)
                {
                    if (colunmIndex >= firstColumn && colunmIndex <= lastColumn)
                    {
                        return true;
                    }
                }
            }
            //没有找到，返回true，但是range为空
            range = null;
            return true;
        }

        public static DataSet ExcelToDataSet(string sheetName, bool isFirstRowColumn, string fileName)
        {
            DataSet ds = new DataSet();
            ISheet sheet = ExcelToExtractSheet(sheetName, isFirstRowColumn, fileName);
            if(null == sheet)
            {
                MessageBox.Show("请检查文件名 "+ fileName +" 以及文件格式是否正确");
                return null;
            }
            //表头
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            DataTable dt = new DataTable();
            bool isMerged = false;
            for (int columnLoop = 0; columnLoop < header.LastCellNum; columnLoop++)
            {
                if(null == header.GetCell(columnLoop))
                {
                    dt.Columns.Add(new DataColumn("Columns" + columnLoop.ToString()));
                }
                else
                {
                    object obj = GetCellValue(header.GetCell(columnLoop));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + columnLoop.ToString()));
                    }
                    else
                    {
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    }
                }
                columns.Add(columnLoop);
            }
            //数据
            IEnumerator rows = sheet.GetEnumerator();
            int rowLoop = sheet.FirstRowNum + 1;
            while (rows.MoveNext())
            {
                //如果没有数据则跳出循环
                if(null == sheet.GetRow(rowLoop))
                {
                    break; 
                }
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int columnTemp in columns)
                {
                    if(null == sheet.GetRow(rowLoop).GetCell(columnTemp))
                    {
                        dr[columnTemp] = null;
                    }
                    else
                    {
                        //判断是否为合并单元格，取对应合并初值
                        CellRangeAddress cellRange;
                        isMerged = GetCellMergedRange(sheet, rowLoop, columnTemp, out cellRange);
                        if(isMerged)
                        {
                            //dr[columnTemp] =  GetCellValue(sheet.GetRow(cellRange.FirstRow).GetCell(cellRange.FirstColumn));
                            dr[columnTemp] = GetCellValue(GetMergeCellValue(sheet, cellRange));
                        }
                        else
                        {
                            dr[columnTemp] = GetCellValue(sheet.GetRow(rowLoop).GetCell(columnTemp));
                        }
                    }
                    if (dr[columnTemp] != null && dr[columnTemp].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
                else
                {
                    //一行里没有任何数据，则终止解析
                    break;
                }
                rowLoop++;
            }
            ds.Tables.Add(dt);

            return ds;
        }

    }
}
