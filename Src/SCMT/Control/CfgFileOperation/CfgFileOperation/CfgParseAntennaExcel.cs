using CfgFileOpStruct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

namespace CfgFileOperation
{
    /// <summary>
    /// 解析天线《LTE_基站天线广播波束权值参数配置表_5G.xls》 ：antennaArrayTypeEntry
    /// </summary>
    public class CfgParseAntennaExcel
    {
        /// <summary>
        /// 保存每条天线权值内容的内存
        /// </summary>
        private List<Dictionary<string, string>> AntennaIndexBS = null; //波束扫描原始值 的数据
        /// <summary>
        /// 
        /// </summary>
        public List<AntArrayBfScanAntWeightTabStru> vectAntArrayBfScanInfo = null;
        public List<AntArrayBfScanAntWeightTabStru> vectAntArrayBfScanInfoMdb = null;
        /// <summary>
        /// 72个列 sheet = "波束扫描原始值"
        /// </summary>
        private Dictionary<string, string> ColsInfoBS = null; // void init();
        /// <summary>
        /// 初始化
        /// </summary>
        public CfgParseAntennaExcel()
        {
            // 类似 mdb 存 excel 内容
            AntennaIndexBS = new List<Dictionary<string, string>>();

            // 把AntennaIndexBS 内容解析成 AntArrayBfScanAntWeightTabStru 格式
            vectAntArrayBfScanInfo = new List<AntArrayBfScanAntWeightTabStru>();

            //
            ColsInfoBS = new Dictionary<string, string>() {//72个
            {"antIndex", "A"},	      // 天线编号:
            {"antVendorName", "B"},	  // 天线厂家名称:      antBfScanVendor //天线阵厂商索引 第一维索引值
            {"antMode", "C"},         // 天线型号:          antBfScanMode
            {"horBeamScanning", "D"}, // 水平方向波束个数:  antBfScanHorNum
            {"horDowntiltAngle", "E"},// 水平方向数字下倾角:(单位o)	
            {"verBeamScanning", "F"}, // 垂直方向波束个数:  antBfScanVerNum
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
            {"antBfScanAmplitude27", "AJ"},//
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
            {"antBfScanPhase21",  "BJ"},//
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

        public void ProcessingAlarmMdb(string strMdbPath)
        {
            vectAntArrayBfScanInfoMdb = new List<AntArrayBfScanAntWeightTabStru>();
            string strSQLAlarm = ("select  * from antennaBfScan");
            DataSet dateSet = new CfgAccessDBManager().GetRecord(strMdbPath, strSQLAlarm);

            int dataCount = dateSet.Tables[0].Rows.Count; // 例如一个版本 2178个告警信息 0~2177

            int nRecord = 0;
            string  strAntArrayVendor = "";//天线阵厂商索引 第一维索引值
            string strAntArrayType = "";  //天线阵型号索引 第二维索引值
            int nBfScanIndex = 0;          //波束扫描组合数 第三维索引
            string strHorNum = "";
            string strVerNum = "";

            string strVendorTypekey = "";
            string strBfKey = "";
            int nBfTotal = 0;
            int nTableNum = 1000;
            string m_strCurrentVendorTypekey = "";
            string m_strBfKey = "";
            //while (!recordset.IsEOF())
            for (int loop = 0; loop < dataCount - 1; loop++)
            {
                var recordset = dateSet.Tables[0].Rows[loop];
                if (nRecord == nTableNum)
                {
                    break;
                }

                //天线阵厂商索引 第一维索引值
                //alarmRow[("AlaNumber")].ToString();
                strAntArrayVendor = recordset["antBfScanVendor"].ToString();
                //天线阵型号索引 第二维索引值
                strAntArrayType = recordset["antBfScanMode"].ToString();
                //水平波束个数
                strHorNum = recordset["antBfScanHorNum"].ToString();
                //垂直波束个数
                strVerNum = recordset["antBfScanVerNum"].ToString();

                //厂家-类型key
                strVendorTypekey = strAntArrayVendor + "-" + strAntArrayType;
                if (true != String.Equals(strVendorTypekey, m_strCurrentVendorTypekey))//if (true != strVendorTypekey.Contains(m_strCurrentVendorTypekey))
                {
                    m_strCurrentVendorTypekey = strVendorTypekey;
                    nBfScanIndex = 0; //重置为0
                }

                //水平波束个数-垂直波束个数key
                strBfKey = strHorNum + "-" + strVerNum;
                if (true != String.Equals(strBfKey, m_strBfKey))//if (true != strBfKey.Contains(m_strBfKey))
                {
                    m_strBfKey = strBfKey;
                    nBfTotal = int.Parse(strHorNum) + int.Parse(strVerNum);

                    for (int nBfGroup = 0; nBfGroup < nBfTotal; nBfGroup++)
                    {
                        for (int nBfTimes = 0; nBfTimes < 4; nBfTimes++)
                        {
                            AntArrayBfScanAntWeightTabStru pAntArrayBfScanInfo = new AntArrayBfScanAntWeightTabStru();
                            string strAmplitude = "antBfScanAmplitude";
                            string strPhase = "antBfScanPhase";
                            int nAmpPhaNum = 0;
                            string strAmpPhaNum = "";
                            string strBfScanIndex = "";
                            string strBfGroup = "";
                            string strBfTimes = "";

                            //天线阵厂商索引 第一维索引值
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightVendorIndex = strAntArrayVendor;
                            //天线阵型号索引 第二维索引值
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightTypeIndex = strAntArrayType;
                            //波束扫描组合数 第三维索引
                            //strBfScanIndex.Format("%d", nBfScanIndex);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightIndex = nBfScanIndex.ToString();
                            //波束扫描每种组合的波束个数 第四维索引
                            //strBfGroup.Format("%d", nBfGroup);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightBFScanGrpNo = nBfGroup.ToString();
                            //波束扫描每个波束的倍数系数 第五维索引
                            //strBfTimes.Format("%d", nBfTimes);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAntGrpNo = nBfTimes.ToString();

                            pAntArrayBfScanInfo.antArrayBfScanAntWeightRowStatus = "4";
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightHorizonNum = strHorNum;
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightVerticalNum = strVerNum;

                            nAmpPhaNum = nBfTimes * 8 + 1;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude0 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase0 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nAmpPhaNum = nBfTimes * 8 + 2;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude1 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase1 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nAmpPhaNum = nBfTimes * 8 + 3;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude2 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase2 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nAmpPhaNum = nBfTimes * 8 + 4;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude3 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase3 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nAmpPhaNum = nBfTimes * 8 + 5;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude4 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase4 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nAmpPhaNum = nBfTimes * 8 + 6;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude5 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase5 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nAmpPhaNum = nBfTimes * 8 + 7;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude6 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase6 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nAmpPhaNum = nBfTimes * 8 + 8;
                            //nAmpPhaNum.ToString().Format("%d", nAmpPhaNum);
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude7 = recordset[strAmplitude + nAmpPhaNum.ToString()].ToString();
                            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase7 = recordset[strPhase + nAmpPhaNum.ToString()].ToString();

                            nRecord++;
                            vectAntArrayBfScanInfoMdb.Add(pAntArrayBfScanInfo);
                        }
                    }
                }



                /////////////////
                //下一种bf组合
                nBfScanIndex++;
            }

            new CfgAccessDBManager().Close(dateSet);
        }

        /// <summary>
        /// 处理各个sheet
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strSheet"></param>
        /// <param name="nTableNum">表容量</param>
        public void ProcessingAntennaExcel(string strExcelPath, string strSheet, int nTableNum)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strSheet))
                return;
            //CfgExcelOp excelOp = new CfgExcelOp();
            //var excelOp = CfgExcelOp.GetInstance();
            //if (excelOp == null)
            //    return;

            //strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\LTE_基站天线广播波束权值参数配置表_5G.xls";

            //Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            //if (wbook == null)
            //    return;
            //Excel.Worksheet wks = excelOp.ReadExcelSheet(strExcelPath, strSheet);//使用"波束扫描原始值"填写数据库中表"antennaBfScan"的内容
            //if (wks == null)
            //    return;

            //Console.WriteLine("ProcessingAntennaExcelBS : Start..., time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            if (String.Equals("原始值", strSheet))
            { }
            else if (String.Equals("耦合系数原始值", strSheet))
            { }
            else if (String.Equals("波束扫描原始值", strSheet))
            {
                ProcessingAntennaExcelBS(strExcelPath, strSheet);     // 类似 mdb 存储解析excel的内容
                ProcessingAntennaExcelToStrList2(nTableNum); // 把存储内容 再次解析成 struct 的格式
            }

            //excelOp = null;
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

        /// <param name="FilePath"></param>
        /// <summary>
        /// 处理"波束扫描原始值"的内容
        /// </summary>
        /// <param name="nTableNum">表容量 </param>
        private void ProcessingAntennaExcelToStrList(int nTableNum)
        {
            if ((AntennaIndexBS.Count == 0) || (vectAntArrayBfScanInfo == null))
                return;

            string strAntArrayVendor = ""; //天线阵厂商索引 第一维索引值
            string strAntArrayType = "";   //天线阵型号索引 第二维索引值
            int nBfScanIndex = 0;          //波束扫描组合数 第三维索引
            string strHorNum = "";         //水平波束个数
            string strVerNum = "";         //垂直波束个数

            // 1.同一 ("天线阵厂商索引" + "波束扫描组合数"), 排列组合("水平波束个数" + "垂直波束个数") 。
            // 1.1 同一 ("天线阵厂商索引" + "波束扫描组合数") 为key 的 波束扫描组合 nBfScanIndex=0,初始化。
            string strVendorTypekey = "";           // 本次的组合："天线阵厂商索引" + "波束扫描组合数" = key
            string m_strCurrentVendorTypekey = "";  // 上次的组合："天线阵厂商索引" + "波束扫描组合数" = key
            // 1.2 同一 ("天线阵厂商索引" + "波束扫描组合数") 下，计算("水平波束个数" + "垂直波束个数") 组合，nBfScanIndex++.
            string strBfKey = "";                   // 本次的组合："水平波束个数" + "垂直波束个数" = key
            string m_strBfKey = "";                 // 上次的组合：
            int nBfTotal = 0;                       // 循环次数  ：int "水平波束个数" + "垂直波束个数" = count

            List<string> indexStrList = new List<string>();
            AntArrayBfScanAntWeightTabStru pAntArrayBfScanInfo;
            foreach ( var recordset in AntennaIndexBS)
            {
                if (vectAntArrayBfScanInfo.Count == nTableNum)// mib 中的 表容量 ！！
                {
                    break;
                }
                
                strAntArrayVendor = recordset["antVendorName"];//天线阵厂商索引 第一维索引值                
                strAntArrayType = recordset["antMode"];//天线阵型号索引 第二维索引值                
                strHorNum = recordset["horBeamScanning"];//水平波束个数               
                strVerNum = recordset["verBeamScanning"];//垂直波束个数

                //厂家-类型key 不同替换
                strVendorTypekey = strAntArrayVendor + "-" + strAntArrayType;
                if (true != String.Equals(strVendorTypekey,m_strCurrentVendorTypekey))
                {
                    m_strCurrentVendorTypekey = strVendorTypekey;
                    nBfScanIndex = 0; //重置为0
                }

                //水平波束个数-垂直波束个数key
                strBfKey = strHorNum + "-" + strVerNum;
                if (true != String.Equals(strBfKey, m_strBfKey))//if (true != strBfKey.Equals(m_strBfKey))
                {
                    m_strBfKey = strBfKey;
                    nBfTotal = int.Parse(strHorNum) + int.Parse(strVerNum);
                    for (int nBfGroup = 0; nBfGroup < nBfTotal; nBfGroup++)
                    {
                        for (int nBfTimes = 0; nBfTimes < 4; nBfTimes++)
                        {
                            pAntArrayBfScanInfo = GetAntArrayBfStru(recordset, nBfScanIndex, nBfGroup, nBfTimes);

                            string index1 = strAntArrayVendor;
                            string index2 = strAntArrayType;
                            string index3 = nBfScanIndex.ToString();
                            string index4 = nBfGroup.ToString();
                            string index5 = nBfTimes.ToString();
                            //if (nBfScanIndex == 1 && nBfGroup == 3 && nBfTimes == 3)
                            //{
                            //    Console.WriteLine("");
                            //}
                            string indexStr = String.Format("{0}.{1}.{2}.{3}.{4}", index1, index2, index3, index4, index5);
                            indexStrList.Add(indexStr);
                            vectAntArrayBfScanInfo.Add(pAntArrayBfScanInfo);
                        }
                    }
                    //下一种bf组合
                    nBfScanIndex++;
                }
                
            }
        }

        private void ProcessingAntennaExcelToStrList2(int nTableNum)
        {
            if ((AntennaIndexBS.Count == 0) || (vectAntArrayBfScanInfo == null))
                return;

            string strAntArrayVendor = ""; //天线阵厂商索引 第一维索引值
            string strAntArrayType = "";   //天线阵型号索引 第二维索引值
            int nBfScanIndex = 0;          //波束扫描组合数 第三维索引:
            int nBfTotal = 0;              //波束扫描组合数 第四维索引: Max=count("水平波束个数" + "垂直波束个数" )
            string strHorNum = "";         //水平波束个数
            string strVerNum = "";         //垂直波束个数

            // 1.同一 ("天线阵厂商索引" + "波束扫描组合数"), 排列组合("水平波束个数" + "垂直波束个数") 。
            // 1.1 同一 ("天线阵厂商索引" + "波束扫描组合数") 为key 的 波束扫描组合 nBfScanIndex=0,初始化。
            string m_strCurrentVendorTypekey = "";  // 上次的组合："天线阵厂商索引" + "波束扫描组合数" = key
            // 1.2 同一 ("天线阵厂商索引" + "波束扫描组合数") 下，计算("水平波束个数" + "垂直波束个数") 组合，nBfScanIndex++.
            string m_strBfKey = "";                 // 上次的组合：
            foreach (var recordset in AntennaIndexBS)
            {
                if (vectAntArrayBfScanInfo.Count == nTableNum)// mib 中的 表容量 ！！
                {
                    break;
                }

                strAntArrayVendor = recordset["antVendorName"];//天线阵厂商索引 第一维索引值                
                strAntArrayType = recordset["antMode"];//天线阵型号索引 第二维索引值                
                strHorNum = recordset["horBeamScanning"];//水平波束个数               
                strVerNum = recordset["verBeamScanning"];//垂直波束个数

                ////厂家-类型key 不同替换
                //if (true != String.Equals((strAntArrayVendor + "-" + strAntArrayType), m_strCurrentVendorTypekey))
                //{
                //    m_strCurrentVendorTypekey = (strAntArrayVendor + "-" + strAntArrayType);
                //    nBfScanIndex = 0; //重置为0初始化第3维索引
                //}

                ////计算第3维和第4维索引
                //if (true != String.Equals((strHorNum + "-" + strVerNum), m_strBfKey))
                //{
                //    m_strBfKey = strHorNum + "-" + strVerNum;//水平波束个数-垂直波束个数key
                //    nBfTotal = 0;// 初始化第4维索引
                //}
                //else
                //{
                //    nBfTotal += 1;
                //    if (nBfTotal > (int.Parse(strHorNum) + int.Parse(strVerNum)))
                //    {
                //        nBfScanIndex++;//下一种bf组合
                //        continue;
                //    }
                //}
                if (!GetIndex3AndIndex4Value(recordset, nBfScanIndex, nBfTotal, m_strCurrentVendorTypekey, m_strBfKey, out nBfScanIndex, out nBfTotal))
                {
                    m_strCurrentVendorTypekey = (strAntArrayVendor + "-" + strAntArrayType);
                    m_strBfKey = strHorNum + "-" + strVerNum;//水平波束个数-垂直波束个数key
                    continue;
                }
                m_strCurrentVendorTypekey = (strAntArrayVendor + "-" + strAntArrayType);
                m_strBfKey = strHorNum + "-" + strVerNum;//水平波束个数-垂直波束个数key

                // 每行变成 4个结构
                GetIndex5AntArrayBfStru(recordset, strAntArrayVendor, strAntArrayType, nBfScanIndex, nBfTotal);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordset"></param>
        /// <param name="nBfScanIndex">index3</param>
        /// <param name="nBfTotal">index4</param>
        /// <param name="m_strCurrentVendorTypekey"></param>
        /// <param name="m_strBfKey"></param>
        /// <param name="index3"></param>
        /// <param name="index4"></param>
        /// <returns></returns>
        bool GetIndex3AndIndex4Value(Dictionary<string, string> recordset, int nBfScanIndex, int nBfTotal, string m_strCurrentVendorTypekey, string m_strBfKey, out int index3, out int index4 )
        {
            //index3 = 0;
            //index4 = 0;
            bool re = true;
            string strAntArrayVendor = recordset["antVendorName"]; //天线阵厂商索引 第一维索引值
            string strAntArrayType = recordset["antMode"];   //天线阵型号索引 第二维索引值
            string strHorNum = recordset["horBeamScanning"];         //水平波束个数
            string strVerNum = recordset["verBeamScanning"];         //垂直波束个数

            if (true == String.Equals(m_strBfKey, "") && true == String.Equals(m_strCurrentVendorTypekey, ""))
            {
                nBfScanIndex = 0;
                nBfTotal = 0;
            }

            //厂家-类型key 不同替换
            if (true != String.Equals((strAntArrayVendor + "-" + strAntArrayType), m_strCurrentVendorTypekey) && !String.Equals(m_strCurrentVendorTypekey, ""))
            {
                //m_strCurrentVendorTypekey = (strAntArrayVendor + "-" + strAntArrayType);
                nBfScanIndex = 0; //重置为0初始化第3维索引
                m_strBfKey = "";
                nBfTotal = 0;
            }
            else
            {
                //计算第3维和第4维索引
                if (true != String.Equals((strHorNum + "-" + strVerNum), m_strBfKey) && !String.Equals(m_strBfKey, ""))
                {
                    nBfScanIndex++;
                    //m_strBfKey = strHorNum + "-" + strVerNum;//水平波束个数-垂直波束个数key
                    nBfTotal = 0;// 初始化第4维索引
                }
                else if (!String.Equals(m_strBfKey, ""))
                {
                    nBfTotal += 1;
                }
                if (nBfTotal >= (int.Parse(strHorNum) + int.Parse(strVerNum)))
                {
                    nBfScanIndex++;//下一种bf组合
                    re = false;
                }
            }
            index3 = nBfScanIndex;
            index4 = nBfTotal;
            return re;
        }

        void GetIndex5AntArrayBfStru(Dictionary<string, string> recordset, string index1, string index2, int index3, int index4 )
        {
            AntArrayBfScanAntWeightTabStru pAntArrayBfScanInfo;
            for (int index5 = 0; index5 < 4; index5++)
            {
                pAntArrayBfScanInfo = GetAntArrayBfStru(recordset, index3, index4, index5);
                vectAntArrayBfScanInfo.Add(pAntArrayBfScanInfo);
            }
        }

        /// <summary>
        /// 组合 AntArrayBfScanAntWeightTabStru
        /// </summary>
        /// <param name="recordset"></param>
        /// <param name="nBfScanIndex">波束扫描组合数 第三维索引</param>
        /// <param name="nBfGroup">波束扫描每种组合的波束个数 第四维索引</param>
        /// <param name="nBfTimes">波束扫描每个波束的倍数系数 第五维索引</param>
        /// <returns></returns>
        AntArrayBfScanAntWeightTabStru GetAntArrayBfStru(Dictionary<string, string> recordset, int nBfScanIndex, int nBfGroup, int nBfTimes)
        {
            AntArrayBfScanAntWeightTabStru pAntArrayBfScanInfo = new AntArrayBfScanAntWeightTabStru();

            string strAmplitude = "antBfScanAmplitude";
            string strPhase = "antBfScanPhase";
            
            //天线阵厂商索引 第一维索引值
            pAntArrayBfScanInfo.antArrayBfScanAntWeightVendorIndex = recordset["antVendorName"];
            //天线阵型号索引 第二维索引值
            pAntArrayBfScanInfo.antArrayBfScanAntWeightTypeIndex = recordset["antMode"];
            //波束扫描组合数 第三维索引
            pAntArrayBfScanInfo.antArrayBfScanAntWeightIndex = nBfScanIndex.ToString();
            //波束扫描每种组合的波束个数 第四维索引
            pAntArrayBfScanInfo.antArrayBfScanAntWeightBFScanGrpNo = nBfGroup.ToString();
            //波束扫描每个波束的倍数系数 第五维索引
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAntGrpNo = nBfTimes.ToString();

            pAntArrayBfScanInfo.antArrayBfScanAntWeightRowStatus = "4";

            //天线1~8幅度Amplitude;天线1~8相位Phase.
            string strAmpPhaNum = (nBfTimes * 8 + 0).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude0 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase0 = recordset[(strPhase + strAmpPhaNum)];

            strAmpPhaNum = (nBfTimes * 8 + 1).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude1 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase1 = recordset[(strPhase + strAmpPhaNum)];

            strAmpPhaNum = (nBfTimes * 8 + 2).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude2 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase2 = recordset[(strPhase + strAmpPhaNum)];

            strAmpPhaNum = (nBfTimes * 8 + 3).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude3 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase3 = recordset[(strPhase + strAmpPhaNum)];

            strAmpPhaNum = (nBfTimes * 8 + 4).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude4 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase4 = recordset[(strPhase + strAmpPhaNum)];

            strAmpPhaNum = (nBfTimes * 8 + 5).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude5 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase5 = recordset[(strPhase + strAmpPhaNum)];

            strAmpPhaNum = (nBfTimes * 8 + 6).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude6 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase6 = recordset[(strPhase + strAmpPhaNum)];

            strAmpPhaNum = (nBfTimes * 8 + 7).ToString();
            pAntArrayBfScanInfo.antArrayBfScanAntWeightAmplitude7 = recordset[(strAmplitude + strAmpPhaNum)];
            pAntArrayBfScanInfo.antArrayBfScanAntWeightPhase7 = recordset[(strPhase + strAmpPhaNum)];

            pAntArrayBfScanInfo.antArrayBfScanAntWeightHorizonNum = recordset["horBeamScanning"];//水平波束个数 
            pAntArrayBfScanInfo.antArrayBfScanAntWeightVerticalNum = recordset["verBeamScanning"];//垂直波束个数

            pAntArrayBfScanInfo.antArrayBfScanAntWeightHorizonDowntiltAngle = recordset["horDowntiltAngle"]; // 水平方向数字下倾角:(单位o)
            pAntArrayBfScanInfo.antArrayBfScanAntWeightVerticalDowntiltAngle = recordset["verDowntiltAngle"];// 垂直方向数字下倾角:(单位o)
            pAntArrayBfScanInfo.antArrayBfScanWeightIsLossFlag = recordset["antLossFlag"];                   // 有损无损:（0：无损/1:有损）
            return pAntArrayBfScanInfo;
        }
        /// <summary>
        /// 处理"波束扫描原始值"的内容
        /// </summary>
        /// <param name="FilePath"></param>
        private void ProcessingAntennaExcelBS(string strExcelPath, string strSheet)
        {
            //if ((wks == null) || (AntennaIndexBS == null))
            //    return;
            if (AntennaIndexBS == null)
                return;

            var excelOp = CfgExcelOp.GetInstance();
            int rowCount = excelOp.GetRowCount(strExcelPath, strSheet);                  // 获取行数
            if (-1 == rowCount)
            {
                //bw.Write(String.Format("Err:ProcessingExcelRru ({0}):({1}), get row count err.", strExcelPath, strSheet));
                return;// false;
            }
            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColsInfoBS.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                //if (String.Equals(colName, "antBfScanPhase21"))
                //{
                //    Console.WriteLine("===");
                //}
                object[,] arry = excelOp.GetRangeVal(strExcelPath, strSheet, ColsInfoBS[colName] + "1", ColsInfoBS[colName] + rowCount);
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
            // 幅度
            List<string> amplitude = new List<string>(){ "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V","W", "X", "Y", "Z",
                "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN" };
            // 相位
            List<string> phase = new List<string>(){ "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
                "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT" };
            // 去掉中文
            List<string> delCh = new List<string>() { "B", "C"};

            if (array == null)//
                reStr = preArray.ToString();
            else
            {
                // 幅度的处理
                if (amplitude.Exists(e => e.Equals(colName)))
                {
                    reStr = Convert.ToInt64((double)array * 100).ToString();// 四舍五入
                }
                // 相位的处理
                else if (phase.Exists(e => e.Equals(colName)))
                {
                    reStr = array.ToString();// 
                }
                // 去掉中文
                else if (delCh.Exists(e => e.Equals(colName)))
                {
                    reStr = array.ToString();// 
                    int pos = reStr.IndexOf(":");
                    if (-1 != pos)
                    {
                        reStr = reStr.Substring(0, pos);
                    }
                }
                else
                {
                    reStr = array.ToString();
                }
            }
            
            return reStr;
        }

        public List<string> indexLEx = null;
        public List<string> indexLMdb = null;
        public bool BeyondCompMdbAndExcel()
        {
            bool re = true;
            List<string> leafs = new List<string> { "antennaBfScanWeightVendorIndex", "antennaBfScanWeightTypeIndex", "antennaBfScanWeightIndex", "antennaBfScanWeightBFScanGrpNo", "antennaBfScanWeightAntGrpNo" };

            indexLEx = new List<string>();
            foreach (var BfScanInfo in vectAntArrayBfScanInfo)
            {
                string indexStr = "";
                for (int ileafNum = 0; ileafNum < 5; ileafNum++)
                {
                    string strCurrentValue = BfScanInfo.GetAntArrayBfScanLeafValue(leafs[ileafNum]);
                    indexStr += strCurrentValue + ".";
                }
                indexStr = indexStr.TrimEnd('.');
                indexLEx.Add(indexStr);
            }

            indexLMdb = new List<string>();
            foreach (var BfScanInfo in vectAntArrayBfScanInfoMdb)
            {
                string indexStr = "";
                for (int ileafNum = 0; ileafNum < 5; ileafNum++)
                {
                    string strCurrentValue = BfScanInfo.GetAntArrayBfScanLeafValue(leafs[ileafNum]);
                    indexStr += strCurrentValue + ".";
                }
                indexStr = indexStr.TrimEnd('.');
                indexLMdb.Add(indexStr);
            }

            IEnumerable<string> intersect = indexLMdb.Except(indexLEx);
            if (intersect.LongCount() != 0)
            {
                return false;
            }

            return re;
        }
    }
}
