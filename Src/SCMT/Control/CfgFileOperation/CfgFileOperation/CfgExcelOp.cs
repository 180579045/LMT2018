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
    /// <summary>
    /// excel 操作
    /// </summary>
    public sealed class CfgExcelOp:IDisposable
    {
        /// <summary>
        /// 唯一
        /// </summary>
        private static CfgExcelOp _instance = null;   // excel 操作实例
        /// <summary>
        /// 唯一
        /// </summary>
        private static readonly object _syncLock = new object();	// 同步锁
        /// <summary>
        /// 唯一 excel app handle
        /// </summary>
        private static Excel.Application excel = null;//new Excel.Application();

        ///Excel.Workbook wbook = null;// excel.Workbooks.Open(FilePath);
        /// <summary>
        /// key:FilePath， val:excel.Workbooks.Open(FilePath).
        /// excel - sheets app handle
        /// </summary>
        Dictionary<string, Excel.Workbook> wbookDic = new Dictionary<string, Workbook>();
        /// <summary>
        /// excel - sheets - one sheet app handle
        /// </summary>
        Excel.Worksheet excelSheet = null;//wks = (Excel.Worksheet)wbook.Worksheets[i];

        /// <summary>
        /// 保证单实例
        /// </summary>
        /// <returns></returns>
        public static CfgExcelOp GetInstance()
        {
            if (null == _instance)
            {
                lock (_syncLock)
                {
                    if (null == _instance)
                    {
                        _instance = new CfgExcelOp();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 只有一个句柄
        /// </summary>
        private CfgExcelOp()
        {
            //Excel.Application excel = new Excel.Application();
            excel = new Excel.Application();
            excel.Visible = false;//设置调用引用的 Excel文件是否可见
            excel.DisplayAlerts = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns>false:null;true:no null</returns>
        //public Excel.Workbook OpenExcel(string FilePath)
        private bool OpenExcel(string FilePath)
        {
            // 是否存在
            if (wbookDic.ContainsKey(FilePath))
            {
                if (wbookDic[FilePath] != null)
                {
                    try
                    {
                        //资源释放
                        System.Runtime.InteropServices.Marshal.ReleaseComObject((Object)wbookDic[FilePath]);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            try
            {
                wbookDic[FilePath] = excel.Workbooks.Open(FilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Open excel({0}) err:{1}", FilePath, ex.Message));
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据FilePath获取excel句柄
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private Excel.Workbook GetBookHandle(string FilePath)
        {
            if (wbookDic.ContainsKey(FilePath))
            {
                return wbookDic[FilePath];
            }
            else {
                if (OpenExcel(FilePath))
                    return wbookDic[FilePath];
                else
                    return null;
            }
        }

        /// <summary>
        /// 读取sheet
        /// </summary>
        /// <param name="FilePath"> excel 地址 </param>
        /// <param name="sheetName">sheet 名字 </param>
        private Excel.Worksheet ReadExcelSheet_old(Excel.Workbook wbook, string sheetName)
        {
            Excel.Worksheet wks = null;
            if (wbook==null)
                return wks;
            
            for (int i = 1; i < wbook.Worksheets.Count + 1; i++)
            {
                if (wbook.Worksheets[i].name.ToString() == sheetName)
                {
                    wks = (Excel.Worksheet)wbook.Worksheets[i];
                    break;
                }
            }
            return wks;
        }
        private Excel.Worksheet ReadExcelSheet(string FilePath, string sheetName)
        {
            Excel.Worksheet wks = null;
            Excel.Workbook wbook = GetBookHandle(FilePath);
            if (wbook == null)
                return null;

            for (int i = 1; i < wbook.Worksheets.Count + 1; i++)
            {
                if (wbook.Worksheets[i].name.ToString() == sheetName)
                {
                    wks = (Excel.Worksheet)wbook.Worksheets[i];
                    break;
                }
            }
            return wks;
        }

        /// <summary>
        /// 获取指定位置指定页的相关信息
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="From"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public object[,] GetRangeVal(string FilePath, string sheetName, string From, string End)
        {
            Excel.Worksheet wks = ReadExcelSheet(FilePath, sheetName);
            if (wks == null)
                return null;
            return (object[,])wks.Cells.get_Range(From, End).Value2;
        }
        /// <summary>
        /// 获取 地址(FilePath)excel中(sheetName)页的行数 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public int GetRowCount(string FilePath, string sheetName)
        {
            Excel.Worksheet wks = ReadExcelSheet(FilePath, sheetName);
            if (wks == null)
                return -1;
            return wks.UsedRange.Rows.Count;                  // 获取行数
        }

        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            //throw new NotImplementedException();
            foreach (var path in wbookDic.Keys)
            {
                if (wbookDic[path] != null)
                {
                    //资源释放
                    System.Runtime.InteropServices.Marshal.ReleaseComObject((Object)wbookDic[path]);
                }
            }
            if (excel != null)
            {
                excel.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject((Object)excel);

                System.GC.Collect();
            }

        }
    }
}
