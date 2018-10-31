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
    /// 处理 excel 告警表
    /// </summary>
    class CfgExcelOp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns>false:null;true:no null</returns>
        public Excel.Workbook OpenExcel(string FilePath)
        {
            Excel.Workbook wbook = null;
            try
            {
                //FilePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\eNB告警信息表.xls";
                Excel.Application excel = new Excel.Application();
                excel.Visible = false;//设置调用引用的 Excel文件是否可见
                excel.DisplayAlerts = false;
                //Excel.Workbook wb = excel.Workbooks.Open(FilePath);
                wbook = excel.Workbooks.Open(FilePath);
            }
            catch
            {
                wbook = null;
            }
            return wbook;
        }

        /// <summary>
        /// 读取 sheet 告警
        /// </summary>
        /// <param name="FilePath"> excel 地址 </param>
        /// <param name="sheetName">sheet 名字 </param>
        public Excel.Worksheet ReadExcelSheet(Excel.Workbook wbook, string sheetName)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="From"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public object[,] GetWKSColRangeVal(Excel.Worksheet wks, string From, string End)
        {
            if (wks == null)
                return null;
            //object[,] arryB = (object[,])wks.Cells.get_Range(From, End).Value2;//{"告警编号", "B"},//  [//{"AlaNumber"}] 
            return (object[,])wks.Cells.get_Range(From, End).Value2; ;
        }


        public void test(string FilePath)
        {
            //ProcessingAlarmExcel(FilePath, "");
        }

        
    }
}
