using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;


namespace CfgFileOperation
{
    class CfgReadAlarmExecl
    {
        /// <summary>
        /// 保存每条告警内容的内存
        /// </summary>
        List<Dictionary<string, string>> alamNo = new List<Dictionary<string, string>>();


        public CfgReadAlarmExecl()
        {
            CfgFileExcelReadWrite excelOp = new CfgFileExcelReadWrite();
            string strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\eNB告警信息表.xls";
            string strSheetName = "eNB告警信息表";
            Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            Excel.Worksheet wks = excelOp.ReadExcelSheet(wbook, strSheetName);

            ProcessingAlarmExcel(wks);
        }

        /// <summary>
        /// 处理 excel sheet 的内容
        /// </summary>
        /// <param name="FilePath"></param>
        void ProcessingAlarmExcel(Excel.Worksheet wks)
        {
            if (wks == null)
                return;
            int rowCount = wks.UsedRange.Rows.Count;                  // 获取行数
            object[,] arryB = (object[,])wks.Cells.get_Range("B2", "B" + rowCount).Value2;//{"告警编号", "B"},//  [//{"AlaNumber"}] 
            object[,] arryD = (object[,])wks.Cells.get_Range("D2", "D" + rowCount).Value2;//{"是否为故障类告警", "D"},//  [//{"IsFault"}]
            object[,] arryG = (object[,])wks.Cells.get_Range("G2", "G" + rowCount).Value2;//{"主告警编号", "G"},//  [//{"AlaSubtoPrimaryNumber"}]
            object[,] arryI = (object[,])wks.Cells.get_Range("I2", "I" + rowCount).Value2;//{"告警类型", "I"},//  [//{"AlaType"}]
            object[,] arryJ = (object[,])wks.Cells.get_Range("J2", "J" + rowCount).Value2;//{"厂家告警级别", "J"},//  [//{"AlaDegree"}]
            object[,] arryQ = (object[,])wks.Cells.get_Range("Q2", "Q" + rowCount).Value2;//{"清除方式", "Q"},//  [//{"ClearStyle"}]
            object[,] arryX = (object[,])wks.Cells.get_Range("X2", "X" + rowCount).Value2;//{"对应北向接口告警标准原因", "X"},//  [//{"ItfNProtocolCauseNo"}]
            object[,] arryY = (object[,])wks.Cells.get_Range("Y2", "Y" + rowCount).Value2;//{"是否需要上报OMCR", "Y"},//  [//{"IsReportToOMCR"}]

            object[,] arryAA = (object[,])wks.Cells.get_Range("AA2", "AA" + rowCount).Value2;//{"告警值含义描述", "AA"},// [//{"ValueStyle"}]
            object[,] arryAC = (object[,])wks.Cells.get_Range("AC2", "AC" + rowCount).Value2;//{"故障类告警清除去抖周期{单位：s}", "AC"},// [//{"ClearDeditheringInterval"}]
            object[,] arryAD = (object[,])wks.Cells.get_Range("AD2", "AD" + rowCount).Value2;//{"告警频次去抖间隔（单位：min）", "AD"},// [//{"CompressionInterval"}]
            object[,] arryAE = (object[,])wks.Cells.get_Range("AE2", "AE" + rowCount).Value2;//{"告警频次去抖次数", "AE"},// [//{"CompressionRepetitions"}]
            object[,] arryAF = (object[,])wks.Cells.get_Range("AF2", "AF" + rowCount).Value2;//{"告警产生去抖周期{单位：s}", "AF"},// [//{"CreateDeditheringInterval"}]
            object[,] arryAV = (object[,])wks.Cells.get_Range("AV2", "AV" + rowCount).Value2;//{"故障对象名称_EN", "AV"},// [//{"FathernameOfObject"}]
            object[,] arryAW = (object[,])wks.Cells.get_Range("AW2", "AW" + rowCount).Value2;//{"告警不稳定态处理方式", "AW"},// [//{"AlaUnstableDispose"}]
            object[,] arryAX = (object[,])wks.Cells.get_Range("AX2", "AX" + rowCount).Value2;//{"不稳定态告警编号", "AX"},// [//{"UnstableAlaNum"}]

            // 处理 每行的告警内容
            for (int i = 1; i <= arryB.Length; i++)//
            {
                var almNo = arryB[i, 1];
                if (almNo == null)//没有告警号，则结束
                    break;
                if (almNo.ToString().Contains("*"))//带* 是tds使用的
                    continue;

                Dictionary<string, string> alarm = new Dictionary<string, string>(){
                    {"告警编号", GetCellValueToString(arryB[i, 1])},//  [{"AlaNumber"}] 
                    {"是否为故障类告警", GetCellValueToString(arryD[i, 1])},//  [//{"IsFault"}]
                    {"主告警编号", GetCellValueToString(arryG[i, 1])},//  [//{"AlaSubtoPrimaryNumber"}]
                    {"告警类型", GetCellValueToString(arryI[i, 1])},//  [//{"AlaType"}]
                    {"厂家告警级别", GetCellValueToString(arryJ[i, 1])},//  [//{"AlaDegree"}]
                    {"清除方式", GetCellValueToString(arryQ[i, 1])},//  [//{"ClearStyle"}]
                    {"对应北向接口告警标准原因", GetCellValueToString(arryX[i, 1])},//  [//{"ItfNProtocolCauseNo"}]
                    {"是否需要上报OMCR", GetCellValueToString(arryY[i, 1])},//  [//{"IsReportToOMCR"}]

                    {"告警值含义描述",GetCellValueToString(arryAA[i, 1])},// [//{"ValueStyle"}]
                    {"故障类告警清除去抖周期{单位：s}",GetCellValueToString(arryAC[i, 1])},// [//{"ClearDeditheringInterval"}]
                    {"告警频次去抖间隔（单位：min）",GetCellValueToString(arryAD[i, 1])},// [//{"CompressionInterval"}]
                    {"告警频次去抖次数",GetCellValueToString(arryAE[i, 1])},// [//{"CompressionRepetitions"}]
                    {"告警产生去抖周期{单位：s}",GetCellValueToString(arryAF[i, 1])},// [//{"CreateDeditheringInterval"}]
                    {"故障对象名称_EN",GetCellValueToString(arryAV[i, 1])},// [//{"FathernameOfObject"}]
                    {"告警不稳定态处理方式",GetCellValueToString(arryAW[i, 1])},// [//{"AlaUnstableDispose"}]
                    {"不稳定态告警编号",GetCellValueToString(arryAX[i, 1])},// [//{"UnstableAlaNum"}]
                };
                alamNo.Add(alarm);// 加入大排行
            }
        }

        /// <summary>
        /// 处理excel 中 null cell 的内容
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public string GetCellValueToString(object array)
        {
            if (array == null)//没有告警号，则结束
                return "";
            else
                return array.ToString();
        }
    }
}
