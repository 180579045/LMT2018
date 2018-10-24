using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

namespace CfgFileOperation
{
    class CfgReadAntennaExcel
    {
        /// <summary>
        /// 保存每条告警内容的内存
        /// </summary>
        private List<Dictionary<string, string>> AntennaIndexBS = null; //波束扫描原始值 的数据
        /// <summary>
        /// 72个列 sheet = "波束扫描原始值"
        /// </summary>
        private Dictionary<string, string> ColsInfoBS = null; // void init();
        /// <summary>
        /// 初始化
        /// </summary>
        public CfgReadAntennaExcel()
        {
            //
            AntennaIndexBS = new List<Dictionary<string, string>>();
            //
            ColsInfoBS = new Dictionary<string, string>() {//72个
            {"antIndex", "A"},	     // 天线编号:
            {"antVendorName", "B"},	 // 天线厂家名称:
            {"antMode", "C"},      // 天线型号:
            {"horBeamScanning", "D"}, // 水平方向波束个数:
            {"horDowntiltAngle", "E"},// 水平方向数字下倾角:(单位o)	
            {"verBeamScanning", "F"}, // 垂直方向波束个数:
            {"verDowntiltAngle", "G"},// 垂直方向数字下倾角:(单位o)	
            {"antLossFlag", "H"},     // 有损无损:（0：无损/1:有损）	
            {"antBfScanAmplitude0", "I"},// 天线1 幅度 V
            {"antBfScanAmplitude1", "J"},//
            {"antBfScanAmplitude2", "K"},//
            {"antBfScanAmplitude3", "L"},//
            {"antBfScanAmplitude4", "M"},//
            {"antBfScanAmplitude5", "N"},//
            {"antBfScanAmplitude6", "O"},//
            {"antBfScanAmplitude7", "P"},//
            {"antBfScanAmplitude8", "Q"},//
            {"antBfScanAmplitude9", "R"},//
            {"antBfScanAmplitude10", "S"},//
            {"antBfScanAmplitude11", "T"},//
            {"antBfScanAmplitude12", "U"},//
            {"antBfScanAmplitude13", "V"},//
            {"antBfScanAmplitude14", "W"},//
            {"antBfScanAmplitude15", "X"},//
            {"antBfScanAmplitude16", "Y"},//
            {"antBfScanAmplitude17", "Z"},//
            {"antBfScanAmplitude18", "AA"},//
            {"antBfScanAmplitude19", "AB"},//
            {"antBfScanAmplitude20", "AC"},//
            {"antBfScanAmplitude21", "AD"},//
            {"antBfScanAmplitude22", "AE"},//
            {"antBfScanAmplitude23", "AF"},//
            {"antBfScanAmplitude24", "AG"},//
            {"antBfScanAmplitude25", "AH"},//
            {"antBfScanAmplitude26", "AI"},//
            {"antBfScanAmplitude27", "AG"},//
            {"antBfScanAmplitude28", "AK"},//
            {"antBfScanAmplitude29", "AL"},//
            {"antBfScanAmplitude30", "AM"},//
            {"antBfScanAmplitude31", "AN"},//
            {"antBfScanPhase0",  "AO"},// 天线1 相位 deg
            {"antBfScanPhase1",  "AP"},//
            {"antBfScanPhase2",  "AQ"},//
            {"antBfScanPhase3",  "AR"},//
            {"antBfScanPhase4",  "AS"},//
            {"antBfScanPhase5",  "AT"},//
            {"antBfScanPhase6",  "AU"},//
            {"antBfScanPhase7",  "AV"},//
            {"antBfScanPhase8",  "AW"},//
            {"antBfScanPhase9",  "AX"},//
            {"antBfScanPhase10",  "AY"},//
            {"antBfScanPhase11",  "AZ"},//
            {"antBfScanPhase12",  "BA"},//
            {"antBfScanPhase13",  "BB"},//
            {"antBfScanPhase14",  "BC"},//
            {"antBfScanPhase15",  "BD"},//
            {"antBfScanPhase16",  "BE"},//
            {"antBfScanPhase17",  "BF"},//
            {"antBfScanPhase18",  "BG"},//
            {"antBfScanPhase19",  "BH"},//
            {"antBfScanPhase20",  "BI"},//
            {"antBfScanPhase21",  "BG"},//
            {"antBfScanPhase22",  "BK"},//
            {"antBfScanPhase23",  "BL"},//
            {"antBfScanPhase24",  "BM"},//
            {"antBfScanPhase25",  "BN"},//
            {"antBfScanPhase26",  "BO"},//
            {"antBfScanPhase27",  "BP"},//
            {"antBfScanPhase28",  "BQ"},//
            {"antBfScanPhase29",  "BR"},//
            {"antBfScanPhase30",  "BS"},//
            {"antBfScanPhase31",  "BT"},//
        };

        }

        /// <summary>
        /// 处理各个sheet
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strSheet"></param>
        public void ProcessingAntennaExcel(string strExcelPath, string strSheet)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strSheet))
                return;
            CfgFileExcelReadWrite excelOp = new CfgFileExcelReadWrite();
            if (excelOp == null)
                return;

            strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\LTE_基站天线广播波束权值参数配置表_5G.xls";

            Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            if (wbook == null)
                return;
            Excel.Worksheet wks = excelOp.ReadExcelSheet(wbook, strSheet);//使用"波束扫描原始值"填写数据库中表"antennaBfScan"的内容
            if (wks == null)
                return;

            //Console.WriteLine("ProcessingAntennaExcelBS : Start..., time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            if (String.Equals("原始值", strSheet))
            { }
            else if (String.Equals("耦合系数原始值", strSheet))
            { }
            else if (String.Equals("波束扫描原始值", strSheet))
            {
                ProcessingAntennaExcelBS(wks);
            }
            //Console.WriteLine("ProcessingAntennaExcelBS : END..., time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetBeamScanData()
        {
            return AntennaIndexBS;
        }

        /// <summary>
        /// 处理"波束扫描原始值"的内容
        /// </summary>
        /// <param name="FilePath"></param>
        private void ProcessingAntennaExcelBS(Excel.Worksheet wks)
        {
            if ((wks == null) || (AntennaIndexBS == null))
                return;

            int rowCount = wks.UsedRange.Rows.Count;                  // 获取行数

            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColsInfoBS.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = (object[,])wks.Cells.get_Range(ColsInfoBS[colName] + "1", ColsInfoBS[colName] + rowCount).Value2;
                ColVals.Add(colName, arry);
            }
            
            // 处理每行的内容
            // 先处理第一行(即从 line=2开始)
            Dictionary<string, string> PreInfo = new Dictionary<string, string>();//当下一行有cell为null时，用来获取上一行的数据填充
            int currentLine = 2;
            foreach (var colNameEn in ColsInfoBS.Keys)
            {
                object[,] arry = ColVals[colNameEn];
                string cellVal = GetCellValueToStringBeamScan(arry[currentLine, 1], ColsInfoBS[colNameEn], "");
                PreInfo.Add(colNameEn, cellVal);
            }
            AntennaIndexBS.Add(PreInfo);
            //
            for ( currentLine=3; currentLine < rowCount+1 ; currentLine++)
            {
                Dictionary<string, string> CurInfo = new Dictionary<string, string>();//处理当前数据
                foreach (var colNameEn in ColsInfoBS.Keys)
                {
                    object[,] arry = ColVals[colNameEn];
                    string cellVal = GetCellValueToStringBeamScan(arry[currentLine, 1], ColsInfoBS[colNameEn], PreInfo[colNameEn]);
                    CurInfo.Add(colNameEn, cellVal);
                }
                PreInfo = CurInfo;
                AntennaIndexBS.Add(CurInfo);
            }

        }
        /// <summary>
        /// 处理"波束扫描原始值"中 cell 的内容;
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string GetCellValueToStringBeamScan(object array, string colName, object preArray)
        {
            string reStr = "";
            List<string> rules_1 = new List<string>(){ "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V","W", "X", "Y", "Z",
                "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN" };
            List<string> rules_2 = new List<string>(){ "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
                "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT" };

            if (array == null)//
                reStr = preArray.ToString();
            else
            {
                if (rules_1.Exists(e => e.Equals(colName)))
                {
                    reStr = Convert.ToInt64((double)array * 100).ToString();// 四舍五入
                }
                else if (rules_2.Exists(e => e.Equals(colName)))
                {
                    reStr = array.ToString();// 
                }
                else
                {
                    reStr = array.ToString();
                }
            }
            
            return reStr;
        }

    }
}
