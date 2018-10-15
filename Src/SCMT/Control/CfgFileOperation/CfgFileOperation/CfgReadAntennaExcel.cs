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
        List<Dictionary<string, string>> AntennaIndex = new List<Dictionary<string, string>>();

        Dictionary<string, string> ColsInfo = new Dictionary<string, string>()
        {
            { "emAr_antNumber", "A"},                 //天线阵型号编号
            { "emAr_antArrayVendor", "B"},            //天线阵厂家索引
            { "emAr_antArrayIndex", "C"},             //天线阵型号索引
            { "emAr_antArrayNum", "D"},               //天线根数:antNum
            { "emAr_antArrayDistance", "E"},          //天线间距:antDistance
            { "emAr_antArrayType", "F"},              //天线类型:antType
            { "emAr_antLossFlag", "G"},               //有损无损:antLossFlag
            { "emAr_antWeightFrequencyBand", "H"},    //频段信息:antFrequencyBand
            { "emAr_antWeightHalfPowerBeamWidth", "I"},//天线半功率角:antHalfPowerBeamWidth(单位o)
            { "emAr_antWeightStatusIndex", "G"},      //天线状态索引
            { "emAr_antWeightStatus", "K"},           //天线状态
            { "emAr_antWeightAmplitude0", "L"},       //天线1幅度 Excel 从0开始
            { "emAr_antWeightPhase0", "M"},           //天线1相位
            { "emAr_antWeightAmplitude1", "N"},       //天线2幅度
            { "emAr_antWeightPhase1", "O"},           //天线2相位
            { "emAr_antWeightAmplitude2", "P"},       //天线3幅度
            { "emAr_antWeightPhase2", "Q"},           //天线3相位
            { "emAr_antWeightAmplitude3", "R"},       //天线4幅度
            { "emAr_antWeightPhase3", "S"},           //天线4相位
            { "emAr_antWeightAmplitude4", "T"},       //天线5幅度
            { "emAr_antWeightPhase4", "U"},           //天线5相位
            { "emAr_antWeightAmplitude5", "V"},       //天线6幅度
            { "emAr_antWeightPhase5", "W"},           //天线6相位
            { "emAr_antWeightAmplitude6", "X"},       //天线7幅度
            { "emAr_antWeightPhase6", "Y"},           //天线7相位
            { "emAr_antWeightAmplitude7", "Z"},       //天线8幅度
            { "emAr_antWeightPhase7", "AA"},          //天线8相位
            { "emAr_antWeightAmplitude8", "AB"},      //天线9幅度
            { "emAr_antWeightPhase8", "AC"},          //天线9相位
            { "emAr_antWeightAmplitude9", "AD"},      //天线10幅度
            { "emAr_antWeightPhase9", "AE"},          //天线10相位
            { "emAr_antWeightAmplitude10", "AF"},     //天线11幅度
            { "emAr_antWeightPhase10", "AG"},         //天线11相位
            { "emAr_antWeightAmplitude11", "AH"},     //天线12幅度
            { "emAr_antWeightPhase11", "AI"},         //天线12相位
            { "emAr_antWeightAmplitude12", "AG"},     //天线13幅度
            { "emAr_antWeightPhase12", "AK"},         //天线13相位
            { "emAr_antWeightAmplitude13", "AL"},     //天线14幅度
            { "emAr_antWeightPhase13", "AM"},         //天线14相位
            { "emAr_antWeightAmplitude14", "AN"},     //天线15幅度
            { "emAr_antWeightPhase14", "AO"},         //天线15相位
            { "emAr_antWeightAmplitude15", "AP"},     //天线16幅度
            { "emAr_antWeightPhase15", "AQ"},         //天线16相位
            { "emAr_antWeightAmplitude16", "AR"},     //天线17幅度
            { "emAr_antWeightPhase16", "AS"},         //天线17相位
            { "emAr_antWeightAmplitude17", "AT"},     //天线18幅度
            { "emAr_antWeightPhase17", "AU"},         //天线18相位
            { "emAr_antWeightAmplitude18", "AV"},     //天线19幅度
            { "emAr_antWeightPhase18", "AW"},         //天线19相位
            { "emAr_antWeightAmplitude19", "AX"},     //天线20幅度
            { "emAr_antWeightPhase19", "AY"},         //天线20相位
            { "emAr_antWeightAmplitude20", "AZ"},     //天线21幅度
            { "emAr_antWeightPhase20", "BA"},         //天线21相位
            { "emAr_antWeightAmplitude21", "BB"},     //天线22幅度
            { "emAr_antWeightPhase21", "BC"},         //天线22相位
            { "emAr_antWeightAmplitude22", "BD"},     //天线23幅度
            { "emAr_antWeightPhase22", "BE"},         //天线23相位
            { "emAr_antWeightAmplitude23", "BF"},     //天线24幅度
            { "emAr_antWeightPhase23", "BG"},         //天线24相位
            { "emAr_antWeightAmplitude24", "BH"},     //天线25幅度
            { "emAr_antWeightPhase24", "BI"},         //天线25相位
            { "emAr_antWeightAmplitude25", "BG"},     //天线26幅度
            { "emAr_antWeightPhase25", "BK"},         //天线26相位
            { "emAr_antWeightAmplitude26", "BL"},     //天线27幅度
            { "emAr_antWeightPhase26", "BM"},         //天线27相位
            { "emAr_antWeightAmplitude27", "BM"},     //天线28幅度
            { "emAr_antWeightPhase27", "BO"},         //天线28相位
            { "emAr_antWeightAmplitude28", "BP"},     //天线29幅度
            { "emAr_antWeightPhase28", "BQ"},         //天线29相位
            { "emAr_antWeightAmplitude29", "BR"},     //天线30幅度
            { "emAr_antWeightPhase29", "BS"},         //天线30相位
            { "emAr_antWeightAmplitude30", "BT"},     //天线31幅度
            { "emAr_antWeightPhase30", "BU"},         //天线31相位
            { "emAr_antWeightAmplitude31", "BV"},     //天线32幅度
            { "emAr_antWeightPhase31", "BW"},         //天线32相位
        };
        int emAr_Last = 75;


        /// <summary>
        /// 
        /// </summary>
        public CfgReadAntennaExcel()
        {
            CfgFileExcelReadWrite excelOp = new CfgFileExcelReadWrite();
            string strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\LTE_基站天线广播波束权值参数配置表_5G.xls";
            string strSheetName1 = "原始值";
            string strSheetName2 = "耦合系数原始值";
            string strSheetName3 = "波束扫描原始值";

            Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            Excel.Worksheet wks1 = excelOp.ReadExcelSheet(wbook, strSheetName1);

            ProcessingAntennaExcel(wks1);
        }

        enum emEPArrColumn
        {
            emAr_antNumber,                //天线阵型号编号
            emAr_antArrayVendor,           //天线阵厂家索引
            emAr_antArrayIndex,            //天线阵型号索引
            emAr_antArrayNum,              //天线根数:antNum
            emAr_antArrayDistance,         //天线间距:antDistance
            emAr_antArrayType,             //天线类型:antType
            emAr_antLossFlag,              //有损无损:antLossFlag
            emAr_antWeightFrequencyBand,   //频段信息:antFrequencyBand
            emAr_antWeightHalfPowerBeamWidth,//天线半功率角:antHalfPowerBeamWidth(单位º)
            emAr_antWeightStatusIndex,     //天线状态索引
            emAr_antWeightStatus,          //天线状态
            emAr_antWeightAmplitude0,      //天线1幅度
            emAr_antWeightPhase0,          //天线1相位
            emAr_antWeightAmplitude1,      //天线2幅度
            emAr_antWeightPhase1,          //天线2相位
            emAr_antWeightAmplitude2,      //天线3幅度
            emAr_antWeightPhase2,          //天线3相位
            emAr_antWeightAmplitude3,      //天线4幅度
            emAr_antWeightPhase3,          //天线4相位
            emAr_antWeightAmplitude4,      //天线5幅度
            emAr_antWeightPhase4,          //天线5相位
            emAr_antWeightAmplitude5,      //天线6幅度
            emAr_antWeightPhase5,          //天线6相位
            emAr_antWeightAmplitude6,      //天线7幅度
            emAr_antWeightPhase6,          //天线7相位
            emAr_antWeightAmplitude7,      //天线8幅度
            emAr_antWeightPhase7,          //天线8相位
            emAr_antWeightAmplitude8,      //天线9幅度
            emAr_antWeightPhase8,          //天线9相位
            emAr_antWeightAmplitude9,      //天线10幅度
            emAr_antWeightPhase9,          //天线10相位
            emAr_antWeightAmplitude10,     //天线11幅度
            emAr_antWeightPhase10,         //天线11相位
            emAr_antWeightAmplitude11,     //天线12幅度
            emAr_antWeightPhase11,         //天线12相位
            emAr_antWeightAmplitude12,     //天线13幅度
            emAr_antWeightPhase12,         //天线13相位
            emAr_antWeightAmplitude13,     //天线14幅度
            emAr_antWeightPhase13,         //天线14相位
            emAr_antWeightAmplitude14,     //天线15幅度
            emAr_antWeightPhase14,         //天线15相位
            emAr_antWeightAmplitude15,     //天线16幅度
            emAr_antWeightPhase15,         //天线16相位
            emAr_antWeightAmplitude16,     //天线17幅度
            emAr_antWeightPhase16,         //天线17相位
            emAr_antWeightAmplitude17,     //天线18幅度
            emAr_antWeightPhase17,         //天线18相位
            emAr_antWeightAmplitude18,     //天线19幅度
            emAr_antWeightPhase18,         //天线19相位
            emAr_antWeightAmplitude19,     //天线20幅度
            emAr_antWeightPhase19,         //天线20相位
            emAr_antWeightAmplitude20,     //天线21幅度
            emAr_antWeightPhase20,         //天线21相位
            emAr_antWeightAmplitude21,     //天线22幅度
            emAr_antWeightPhase21,         //天线22相位
            emAr_antWeightAmplitude22,     //天线23幅度
            emAr_antWeightPhase22,         //天线23相位
            emAr_antWeightAmplitude23,     //天线24幅度
            emAr_antWeightPhase23,         //天线24相位
            emAr_antWeightAmplitude24,     //天线25幅度
            emAr_antWeightPhase24,         //天线25相位
            emAr_antWeightAmplitude25,     //天线26幅度
            emAr_antWeightPhase25,         //天线26相位
            emAr_antWeightAmplitude26,     //天线27幅度
            emAr_antWeightPhase26,         //天线27相位
            emAr_antWeightAmplitude27,     //天线28幅度
            emAr_antWeightPhase27,         //天线28相位
            emAr_antWeightAmplitude28,     //天线29幅度
            emAr_antWeightPhase28,         //天线29相位
            emAr_antWeightAmplitude29,     //天线30幅度
            emAr_antWeightPhase29,         //天线30相位
            emAr_antWeightAmplitude30,     //天线31幅度
            emAr_antWeightPhase30,         //天线31相位
            emAr_antWeightAmplitude31,     //天线32幅度
            emAr_antWeightPhase31,         //天线32相位
            emAr_Last
        };
        
        /// <summary>
        /// 处理 excel sheet 的内容
        /// </summary>
        /// <param name="FilePath"></param>
        void ProcessingAntennaExcel(Excel.Worksheet wks)
        {
            if (wks == null)
                return;
            int rowCount = wks.UsedRange.Rows.Count;                  // 获取行数

            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColsInfo.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = (object[,])wks.Cells.get_Range(ColsInfo[colName] + "2", ColsInfo[colName] + rowCount).Value2;
                ColVals.Add(colName, arry);
            }
            
            for (int currentLine=2; currentLine < rowCount ; currentLine++)
            {
                foreach (var colName in ColsInfo.Keys)
                {
                    object[,] arry = ColVals[colName];
                    string cellVal = OriginalGetCellValueToString(arry[currentLine, 1], ColsInfo[colName]);
                }
            }

        }

        /// <summary>
        /// 处理excel 中 null cell 的内容 "原始值";
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public string OriginalGetCellValueToString(object array, string colName)
        {
            string reStr = "";
            List<string> rules_1 = new List<string>(){"L","N","P","R","T","V","X","Z","AB","AD","AF","AH","AJ",
                "AL","AN","AP","AR","AT","AV","AX","AZ","BB","BD","BF","BH","BJ","BL","BN","BP","BR","BT","BV"};
            List<string> rules_2 = new List<string>(){ "M","O","Q","S","U","W","Y","AA","AC","AE","AG","AI","AK",
                "AM","AO","AQ","AS","AU","AW","AY","BA","BC","BE","BG","BI","BK","BM","BO","BQ","BS","BU","BW"};
            if (rules_1.Exists(e => e.Equals(colName)))
            {
                if (array == null)//
                    reStr =  65535.ToString();//0xffff = 65535
                else
                    reStr = Convert.ToInt64((double)array * 100).ToString();// 四舍五入
            }
            else if (rules_2.Exists(e => e.Equals(colName)))
            {
                if (array == null)//
                    return "";
                else
                    reStr = Convert.ToInt64((double)array).ToString();// 四舍五入
            }
            else if (String.Equals("K", colName))
            {
                if (array == null)//
                    return "";
                else
                {
                    string re = array.ToString().Remove(0, 2);
                    reStr = Convert.ToInt32(re, 16).ToString();//把十六进制转换为十进制
                }
            }
            else
            {
                if (array == null)//
                    reStr = "";
                else
                    reStr = array.ToString();
            }
                

            return reStr;
        }
    }
}
