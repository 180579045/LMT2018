using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using CfgFileOpStruct;
using System.IO;

namespace CfgFileOperation
{
    /// <summary>
    /// 解析告警《eNB告警信息表.xls》 alarmCauseEntry
    /// </summary>
    class CfgParseAlarmExecl
    {
        /// <summary>
        /// 保存每条告警内容的内存
        /// </summary>
        List<Dictionary<string, string>> alamNo = new List<Dictionary<string, string>>();
        List<StruAlarmInfo> vectAlarmInfoMdb = new List<StruAlarmInfo>();
        public List<StruAlarmInfo> vectAlarmInfoExcel = new List<StruAlarmInfo>();
        public CfgParseAlarmExecl()
        { }

        public void CfgParseAlarmExeclAndMdb(string strExcelPath, string strMdbPath)
        {
            //var excelOp = CfgExcelOp.GetInstance();
            //string strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\eNB告警信息表.xls";
            string strSheetName = "eNB告警信息表";
            
            //Excel.Worksheet wks = excelOp.ReadExcelSheet(strExcelPath, strSheetName);

            ProcessingAlarmMdb(strMdbPath);
            ProcessingAlarmExcel(strExcelPath, strSheetName);

            FileStream fs = new FileStream("alarmBugWriteBuf.txt", FileMode.Create);
            //实例化BinaryWriter
            BinaryWriter bw = new BinaryWriter(fs);

            List<byte> bugbuff = new List<byte>();
            foreach (var mdb in vectAlarmInfoMdb)
            {
                string alarmNo = mdb.alarmCauseNo;
                int pos = vectAlarmInfoExcel.FindIndex(e => e.alarmCauseNo == alarmNo);
                if (-1 == pos)
                    continue;
                is_same_pama_str(mdb, vectAlarmInfoExcel[pos], bw);
            }
            //清空缓冲区
            bw.Flush();
            //关闭流
            bw.Close();
            fs.Close();
            //byte[] wr = bugbuff.ToArray();
            //FileStream fs = new FileStream("alarmBugWriteBuf.txt", FileMode.Create);
            //fs.Seek(0, SeekOrigin.End);// 偏移的位置
            //fs.Write(wr, 0, wr.Length);
            //fs.Flush();
            //fs.Close();

        }

        public void ParseExcel(string strExcelPath)
        {
            //CfgExcelOp excelOp = new CfgExcelOp();
            
            string strSheetName = "eNB告警信息表";
            //Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            //Excel.Worksheet wks = excelOp.ReadExcelSheet(strExcelPath, strSheetName);
            //if (wks != null)
            ProcessingAlarmExcel(strExcelPath, strSheetName);
            //else
            //{
            //    return;
            //}
        }
        bool is_same_pama(StruAlarmInfo a, StruAlarmInfo b, List<byte> bugbuff)
        {
            string bugb = "";
            if (!String.Equals(a.alarmCauseNo, b.alarmCauseNo))
            {
                bugb += (String.Format("alarmCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseNo, b.alarmCauseNo));
            }
            if (!String.Equals(a.alarmCauseRowStatus, b.alarmCauseRowStatus))
            {
                bugb += (String.Format("alarmCauseRowStatus : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseRowStatus, b.alarmCauseRowStatus));
            }
            if (!String.Equals(a.alarmCauseSeverity, b.alarmCauseSeverity))
            {
                bugb += (String.Format("alarmCauseSeverity : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseSeverity, b.alarmCauseSeverity));
            }
            if (!String.Equals(a.alarmCauseIsValid, b.alarmCauseIsValid))
            {
                bugb += (String.Format("alarmCauseIsValid : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseIsValid, b.alarmCauseIsValid));
            }
            if (!String.Equals(a.alarmCauseType, b.alarmCauseType))
            {
                bugb += (String.Format("alarmCauseType : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseType, b.alarmCauseType));
            }//5
            if (!String.Equals(a.alarmCauseClearStyle, b.alarmCauseClearStyle))
            {
                bugb += (String.Format("alarmCauseClearStyle : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseClearStyle, b.alarmCauseClearStyle));
            }
            if (!String.Equals(a.alarmCauseToAlarmBox, b.alarmCauseToAlarmBox))
            {
                bugb += (String.Format("alarmCauseToAlarmBox : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseToAlarmBox, b.alarmCauseToAlarmBox));
            }
            if (!String.Equals(a.alarmCauseItfNProtocolCauseNo.Replace(" ", ""), b.alarmCauseItfNProtocolCauseNo.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseItfNProtocolCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseItfNProtocolCauseNo, b.alarmCauseItfNProtocolCauseNo));
            }
            if (!String.Equals(a.alarmCauseIsStateful, b.alarmCauseIsStateful))
            {
                bugb += (String.Format("alarmCauseIsStateful : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseIsStateful, b.alarmCauseIsStateful));
            }
            if (!String.Equals(a.alarmCausePrimaryAlarmCauseNo, b.alarmCausePrimaryAlarmCauseNo))
            {
                bugb += (String.Format("alarmCausePrimaryAlarmCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCausePrimaryAlarmCauseNo, b.alarmCausePrimaryAlarmCauseNo));
            }//10
            if (!String.Equals(a.alarmCauseStatefulClearDeditheringInterval, b.alarmCauseStatefulClearDeditheringInterval))
            {
                bugb += (String.Format("alarmCauseStatefulClearDeditheringInterval : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulClearDeditheringInterval, b.alarmCauseStatefulClearDeditheringInterval));
            }
            if (!String.Equals(a.alarmCauseStatefulCreateDeditheringInterval.Replace(" ", ""), b.alarmCauseStatefulCreateDeditheringInterval.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseStatefulCreateDeditheringInterval : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulCreateDeditheringInterval, b.alarmCauseStatefulCreateDeditheringInterval));
            }
            if (!String.Equals(a.alarmCauseStatefulDelayTime, b.alarmCauseStatefulDelayTime))
            {
                bugb += (String.Format("alarmCauseStatefulDelayTime : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulDelayTime, b.alarmCauseStatefulDelayTime));
            }
            if (!String.Equals(a.alarmCauseCompressionInterval.Replace(" ", ""), b.alarmCauseCompressionInterval.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseCompressionInterval : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseCompressionInterval, b.alarmCauseCompressionInterval));
            }
            if (!String.Equals(a.alarmCauseCompressionRepetitions.Replace(" ", ""), b.alarmCauseCompressionRepetitions.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseCompressionRepetitions : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseCompressionRepetitions, b.alarmCauseCompressionRepetitions));
            }//15
            if (!String.Equals(a.alarmCauseAutoProcessPolicy, b.alarmCauseAutoProcessPolicy))
            {
                bugb += (String.Format("alarmCauseAutoProcessPolicy : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseAutoProcessPolicy, b.alarmCauseAutoProcessPolicy));
            }
            if (!String.Equals(a.alarmCauseValueStyle.Replace(" ", ""), b.alarmCauseValueStyle.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseValueStyle : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseValueStyle, b.alarmCauseValueStyle));
            }
            //if (!String.Equals(a.alarmCauseFaultObjectType, b.alarmCauseFaultObjectType))
            //{
            //    bugb += (String.Format("alarmCauseFaultObjectType : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseFaultObjectType, b.alarmCauseFaultObjectType));
            //}
            if (!String.Equals(a.alarmCauseReportBoardType, b.alarmCauseReportBoardType))
            {
                bugb += (String.Format("alarmCauseReportBoardType : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseReportBoardType, b.alarmCauseReportBoardType));
            }//19

            //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
            if (!String.Equals(a.strAlarmUnstableDispose.Replace(" ", ""), b.strAlarmUnstableDispose.Replace(" ", "")))
            {
                bugb += (String.Format("strAlarmUnstableDispose : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.strAlarmUnstableDispose, b.strAlarmUnstableDispose));
            }
            if (!String.Equals(a.strAlarmCauseInsecureNo, b.strAlarmCauseInsecureNo))
            {
                bugb += (String.Format("strAlarmCauseInsecureNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.strAlarmCauseInsecureNo, b.strAlarmCauseInsecureNo));
            }  //不稳定态告警编号
            if (!String.Equals(bugb, ""))
            {
                Console.WriteLine(bugb);

                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(bugb);
                bugbuff.AddRange(byteArray);
            }
            return true;
        }

        bool is_same_pama_str(StruAlarmInfo a, StruAlarmInfo b, BinaryWriter bw)
        {
            string bugb = "";
            if (!String.Equals(a.alarmCauseNo, b.alarmCauseNo))
            {
                bugb += (String.Format("alarmCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseNo, b.alarmCauseNo));
            }
            if (!String.Equals(a.alarmCauseRowStatus, b.alarmCauseRowStatus))
            {
                bugb += (String.Format("alarmCauseRowStatus : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseRowStatus, b.alarmCauseRowStatus));
            }
            if (!String.Equals(a.alarmCauseSeverity, b.alarmCauseSeverity))
            {
                bugb += (String.Format("alarmCauseSeverity : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseSeverity, b.alarmCauseSeverity));
            }
            if (!String.Equals(a.alarmCauseIsValid, b.alarmCauseIsValid))
            {
                bugb += (String.Format("alarmCauseIsValid : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseIsValid, b.alarmCauseIsValid));
            }
            if (!String.Equals(a.alarmCauseType, b.alarmCauseType))
            {
                bugb += (String.Format("alarmCauseType : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseType, b.alarmCauseType));
            }//5
            if (!String.Equals(a.alarmCauseClearStyle, b.alarmCauseClearStyle))
            {
                bugb += (String.Format("alarmCauseClearStyle : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseClearStyle, b.alarmCauseClearStyle));
            }
            if (!String.Equals(a.alarmCauseToAlarmBox, b.alarmCauseToAlarmBox))
            {
                bugb += (String.Format("alarmCauseToAlarmBox : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseToAlarmBox, b.alarmCauseToAlarmBox));
            }
            if (!String.Equals(a.alarmCauseItfNProtocolCauseNo.Replace(" ", ""), b.alarmCauseItfNProtocolCauseNo.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseItfNProtocolCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseItfNProtocolCauseNo, b.alarmCauseItfNProtocolCauseNo));
            }
            if (!String.Equals(a.alarmCauseIsStateful, b.alarmCauseIsStateful))
            {
                bugb += (String.Format("alarmCauseIsStateful : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseIsStateful, b.alarmCauseIsStateful));
            }
            if (!String.Equals(a.alarmCausePrimaryAlarmCauseNo, b.alarmCausePrimaryAlarmCauseNo))
            {
                bugb += (String.Format("alarmCausePrimaryAlarmCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCausePrimaryAlarmCauseNo, b.alarmCausePrimaryAlarmCauseNo));
            }//10
            if (!String.Equals(a.alarmCauseStatefulClearDeditheringInterval, b.alarmCauseStatefulClearDeditheringInterval))
            {
                bugb += (String.Format("alarmCauseStatefulClearDeditheringInterval : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulClearDeditheringInterval, b.alarmCauseStatefulClearDeditheringInterval));
            }
            if (!String.Equals(a.alarmCauseStatefulCreateDeditheringInterval.Replace(" ", ""), b.alarmCauseStatefulCreateDeditheringInterval.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseStatefulCreateDeditheringInterval : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulCreateDeditheringInterval, b.alarmCauseStatefulCreateDeditheringInterval));
            }
            if (!String.Equals(a.alarmCauseStatefulDelayTime, b.alarmCauseStatefulDelayTime))
            {
                bugb += (String.Format("alarmCauseStatefulDelayTime : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulDelayTime, b.alarmCauseStatefulDelayTime));
            }
            if (!String.Equals(a.alarmCauseCompressionInterval.Replace(" ", ""), b.alarmCauseCompressionInterval.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseCompressionInterval : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseCompressionInterval, b.alarmCauseCompressionInterval));
            }
            if (!String.Equals(a.alarmCauseCompressionRepetitions.Replace(" ", ""), b.alarmCauseCompressionRepetitions.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseCompressionRepetitions : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseCompressionRepetitions, b.alarmCauseCompressionRepetitions));
            }//15
            if (!String.Equals(a.alarmCauseAutoProcessPolicy, b.alarmCauseAutoProcessPolicy))
            {
                bugb += (String.Format("alarmCauseAutoProcessPolicy : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseAutoProcessPolicy, b.alarmCauseAutoProcessPolicy));
            }
            if (!String.Equals(a.alarmCauseValueStyle.Replace(" ", ""), b.alarmCauseValueStyle.Replace(" ", "")))
            {
                bugb += (String.Format("alarmCauseValueStyle : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseValueStyle, b.alarmCauseValueStyle));
            }
            //if (!String.Equals(a.alarmCauseFaultObjectType, b.alarmCauseFaultObjectType))
            //{
            //    bugb += (String.Format("alarmCauseFaultObjectType : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseFaultObjectType, b.alarmCauseFaultObjectType));
            //}
            if (!String.Equals(a.alarmCauseReportBoardType, b.alarmCauseReportBoardType))
            {
                bugb += (String.Format("alarmCauseReportBoardType : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseReportBoardType, b.alarmCauseReportBoardType));
            }//19

            //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
            if (!String.Equals(a.strAlarmUnstableDispose.Replace(" ", ""), b.strAlarmUnstableDispose.Replace(" ", "")))
            {
                bugb += (String.Format("strAlarmUnstableDispose : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.strAlarmUnstableDispose, b.strAlarmUnstableDispose));
            }
            if (!String.Equals(a.strAlarmCauseInsecureNo, b.strAlarmCauseInsecureNo))
            {
                bugb += (String.Format("strAlarmCauseInsecureNo : a.no={0},b.no={1}, a.v={2}, b.v={3} \n", a.alarmCauseNo, b.alarmCauseNo, a.strAlarmCauseInsecureNo, b.strAlarmCauseInsecureNo));
            }  //不稳定态告警编号
            if (!String.Equals(bugb, ""))
            {
                bw.Write(bugb);
            }
            return true;
        }

        bool is_same(StruAlarmInfo a, StruAlarmInfo b)
        {
            //if(String.Compare(a.))
            if (!String.Equals(a.alarmCauseNo, b.alarmCauseNo))
            { Console.WriteLine(String.Format("alarmCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseNo, b.alarmCauseNo)); }
            if (!String.Equals(a.alarmCauseRowStatus, b.alarmCauseRowStatus))
            { Console.WriteLine(String.Format("alarmCauseRowStatus : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseRowStatus, b.alarmCauseRowStatus)); }
            if (!String.Equals(a.alarmCauseSeverity, b.alarmCauseSeverity))
            { Console.WriteLine(String.Format("alarmCauseSeverity : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseSeverity, b.alarmCauseSeverity)); }
            if (!String.Equals(a.alarmCauseIsValid, b.alarmCauseIsValid))
            { Console.WriteLine(String.Format("alarmCauseIsValid : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseIsValid, b.alarmCauseIsValid)); }
            if (!String.Equals(a.alarmCauseType, b.alarmCauseType))
            { Console.WriteLine(String.Format("alarmCauseType : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseType, b.alarmCauseType)); }//5
            if (!String.Equals(a.alarmCauseClearStyle, b.alarmCauseClearStyle))
            { Console.WriteLine(String.Format("alarmCauseClearStyle : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseClearStyle, b.alarmCauseClearStyle)); }
            if (!String.Equals(a.alarmCauseToAlarmBox, b.alarmCauseToAlarmBox))
            { Console.WriteLine(String.Format("alarmCauseToAlarmBox : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseToAlarmBox, b.alarmCauseToAlarmBox)); }
            if (!String.Equals(a.alarmCauseItfNProtocolCauseNo.Replace(" ", ""), b.alarmCauseItfNProtocolCauseNo.Replace(" ", "")))
            { Console.WriteLine(String.Format("alarmCauseItfNProtocolCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseItfNProtocolCauseNo, b.alarmCauseItfNProtocolCauseNo)); }
            if (!String.Equals(a.alarmCauseIsStateful, b.alarmCauseIsStateful))
            { Console.WriteLine(String.Format("alarmCauseIsStateful : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseIsStateful, b.alarmCauseIsStateful)); }
            if (!String.Equals(a.alarmCausePrimaryAlarmCauseNo, b.alarmCausePrimaryAlarmCauseNo))
            { Console.WriteLine(String.Format("alarmCausePrimaryAlarmCauseNo : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCausePrimaryAlarmCauseNo, b.alarmCausePrimaryAlarmCauseNo)); }//10
            if (!String.Equals(a.alarmCauseStatefulClearDeditheringInterval, b.alarmCauseStatefulClearDeditheringInterval))
            { Console.WriteLine(String.Format("alarmCauseStatefulClearDeditheringInterval : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulClearDeditheringInterval, b.alarmCauseStatefulClearDeditheringInterval)); }
            if (!String.Equals(a.alarmCauseStatefulCreateDeditheringInterval.Replace(" ", ""), b.alarmCauseStatefulCreateDeditheringInterval.Replace(" ", "")))
            { Console.WriteLine(String.Format("alarmCauseStatefulCreateDeditheringInterval : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulCreateDeditheringInterval, b.alarmCauseStatefulCreateDeditheringInterval)); }
            if (!String.Equals(a.alarmCauseStatefulDelayTime, b.alarmCauseStatefulDelayTime))
            { Console.WriteLine(String.Format("alarmCauseStatefulDelayTime : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseStatefulDelayTime, b.alarmCauseStatefulDelayTime)); }
            if (!String.Equals(a.alarmCauseCompressionInterval.Replace(" ", ""), b.alarmCauseCompressionInterval.Replace(" ", "")))
            { Console.WriteLine(String.Format("alarmCauseCompressionInterval : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseCompressionInterval, b.alarmCauseCompressionInterval)); }
            if (!String.Equals(a.alarmCauseCompressionRepetitions.Replace(" ", ""), b.alarmCauseCompressionRepetitions.Replace(" ", "")))
            { Console.WriteLine(String.Format("alarmCauseCompressionRepetitions : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseCompressionRepetitions, b.alarmCauseCompressionRepetitions)); }//15
            if (!String.Equals(a.alarmCauseAutoProcessPolicy, b.alarmCauseAutoProcessPolicy))
            { Console.WriteLine(String.Format("alarmCauseAutoProcessPolicy : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseAutoProcessPolicy, b.alarmCauseAutoProcessPolicy)); }
            if (!String.Equals(a.alarmCauseValueStyle.Replace(" ", ""), b.alarmCauseValueStyle.Replace(" ", "")))
            { Console.WriteLine(String.Format("alarmCauseValueStyle : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseValueStyle, b.alarmCauseValueStyle)); }
            //if (!String.Equals(a.alarmCauseFaultObjectType, b.alarmCauseFaultObjectType))
            //{ Console.WriteLine(String.Format("alarmCauseFaultObjectType : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseFaultObjectType, b.alarmCauseFaultObjectType)); }
            if (!String.Equals(a.alarmCauseReportBoardType, b.alarmCauseReportBoardType))
            { Console.WriteLine(String.Format("alarmCauseReportBoardType : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.alarmCauseReportBoardType, b.alarmCauseReportBoardType)); }//19

            //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
            if (!String.Equals(a.strAlarmUnstableDispose.Replace(" ",""), b.strAlarmUnstableDispose.Replace(" ", "")))
            { Console.WriteLine(String.Format("strAlarmUnstableDispose : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.strAlarmUnstableDispose, b.strAlarmUnstableDispose)); }
            if (!String.Equals(a.strAlarmCauseInsecureNo, b.strAlarmCauseInsecureNo))
            { Console.WriteLine(String.Format("strAlarmCauseInsecureNo : a.no={0},b.no={1}, a.v={2}, b.v={3}", a.alarmCauseNo, b.alarmCauseNo, a.strAlarmCauseInsecureNo, b.strAlarmCauseInsecureNo)); }  //不稳定态告警编号

            return true; 
        }

        /// <summary>
        /// 处理 excel sheet 的内容
        /// </summary>
        /// <param name="FilePath"></param>
        void ProcessingAlarmExcel(string strExcelPath, string strSheetName)
        {
            var cfgExOp = CfgExcelOp.GetInstance();
            // 获取行数
            int rowCount = cfgExOp.GetRowCount(strExcelPath, strSheetName);

            object[,] arryB = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "B2", "B" + rowCount);//{"告警编号", "B"},//  [//{"AlaNumber"}] 
            object[,] arryD = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "D2", "D" + rowCount);//{"是否为故障类告警", "D"},//  [//{"IsFault"}]
            object[,] arryG = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "G2", "G" + rowCount);//{"主告警编号", "G"},//  [//{"AlaSubtoPrimaryNumber"}]
            object[,] arryI = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "I2", "I" + rowCount);//{"告警类型", "I"},//  [//{"AlaType"}]
            object[,] arryJ = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "J2", "J" + rowCount);//{"厂家告警级别", "J"},//  [//{"AlaDegree"}]
            object[,] arryQ = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "Q2", "Q" + rowCount);//{"清除方式", "Q"},//  [//{"ClearStyle"}]
            object[,] arryX = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "X2", "X" + rowCount);//{"对应北向接口告警标准原因", "X"},//  [//{"ItfNProtocolCauseNo"}]
            object[,] arryY = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "Y2", "Y" + rowCount);//{"是否需要上报OMCR", "Y"},//  [//{"IsReportToOMCR"}]
            object[,] arryZ = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "Z2", "Z" + rowCount);//{"故障对象名称", "Z"},//  [//{"FathernameOfObject"}]

            object[,] arryAA = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AA2", "AA" + rowCount);//{"告警值含义描述", "AA"},// [//{"ValueStyle"}]
            object[,] arryAC = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AC2", "AC" + rowCount);//{"故障类告警清除去抖周期{单位：s}", "AC"},// [//{"ClearDeditheringInterval"}]
            object[,] arryAD = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AD2", "AD" + rowCount);//{"告警频次去抖间隔（单位：min）", "AD"},// [//{"CompressionInterval"}]
            object[,] arryAE = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AE2", "AE" + rowCount);//{"告警频次去抖次数", "AE"},// [//{"CompressionRepetitions"}]
            object[,] arryAF = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AF2", "AF" + rowCount);//{"告警产生去抖周期{单位：s}", "AF"},// [//{"CreateDeditheringInterval"}]
            object[,] arryAV = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AV2", "AV" + rowCount);//{"故障对象名称_EN", "AV"},// [//{"FathernameOfObject_En"}]
            object[,] arryAW = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AW2", "AW" + rowCount);//{"告警不稳定态处理方式", "AW"},// [//{"AlaUnstableDispose"}]
            object[,] arryAX = cfgExOp.GetRangeVal(strExcelPath, strSheetName, "AX2", "AX" + rowCount);//{"不稳定态告警编号", "AX"},// [//{"UnstableAlaNum"}]

            //List<StruAlarmInfo> vectAlarmInfo = new List<StruAlarmInfo>();
            // 处理 每行的告警内容
            for (int i = 1; i <= arryB.Length; i++)//
            {
                string stralmNo = "";
                var almNo = arryB[i, 1];
                if (almNo == null)//没有告警号，则结束
                    break;
                stralmNo = almNo.ToString();
                if (stralmNo.Contains("*"))//带* 是tds使用的
                {
                    stralmNo = almNo.ToString().TrimEnd('*');
                    if (stralmNo == "")
                    {
                        continue;
                    }
                }


                Dictionary<string, string> alarmExVal = new Dictionary<string, string>();
                alarmExVal.Add("AlaNumber", stralmNo);// GetCellValueToString(arryB[i, 1]));//alarmCauseNo = alarmRow[("AlaNumber")].ToString();//告警编号
                alarmExVal.Add("IsReportToOMCR", GetCellValueToString(arryY[i, 1]));
                alarmExVal.Add("AlaDegree", GetCellValueToString(arryJ[i, 1]));
                alarmExVal.Add("AlaType", GetCellValueToString(arryI[i, 1]));
                alarmExVal.Add("ClearStyle", GetCellValueToString(arryQ[i, 1]));
                alarmExVal.Add("ItfNProtocolCauseNo", GetCellValueToString(arryX[i, 1]));
                alarmExVal.Add("IsFault", GetCellValueToString(arryD[i, 1]));
                alarmExVal.Add("AlaSubtoPrimaryNumber", GetCellValueToString(arryG[i, 1]));
                alarmExVal.Add("ClearDeditheringInterval", GetCellValueToString(arryAC[i, 1]));
                alarmExVal.Add("CreateDeditheringInterval", GetCellValueToString(arryAF[i, 1]));
                alarmExVal.Add("CompressionInterval", GetCellValueToString(arryAD[i, 1]));
                alarmExVal.Add("CompressionRepetitions", GetCellValueToString(arryAE[i, 1]));
                alarmExVal.Add("ValueStyle", GetCellValueToString(arryAA[i, 1]));
                alarmExVal.Add("FathernameOfObject", GetCellValueToString(arryZ[i, 1]));
                //alarmExVal.Add("FathernameOfObject_En", GetCellValueToString(arryAV[i, 1]));//IsReportToOMCR
                alarmExVal.Add("AlaUnstableDispose", GetCellValueToString(arryAW[i, 1]));
                alarmExVal.Add("UnstableAlaNum", GetCellValueToString(arryAX[i, 1]));
                //alarmExVal.Add("AlaNumber", GetCellValueToString(arryB[i, 1]));

                //alamNo.Add(alarm);// 加入大排行
                vectAlarmInfoExcel.Add(new StruAlarmInfo(alarmExVal));
                
            }
        }

        void ProcessingAlarmMdb(string strMdbPath)
        {
            string strSQLAlarm = ("select  * from AlarmInform_5216");

            DataSet AlarmdateSet = new CfgOp().CfgGetRecordByAccessDb(strMdbPath, strSQLAlarm);

            int alarmCount = AlarmdateSet.Tables[0].Rows.Count; // 例如一个版本 2178个告警信息 0~2177

            //List<StruAlarmInfo> vectAlarmInfo = new List<StruAlarmInfo>();
            for (int loop = 0; loop < alarmCount - 1; loop++)
            {  // 在表之间循环
                vectAlarmInfoMdb.Add(new StruAlarmInfo(AlarmdateSet.Tables[0].Rows[loop]));
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
