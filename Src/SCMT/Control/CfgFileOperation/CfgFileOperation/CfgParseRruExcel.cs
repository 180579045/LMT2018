using CfgFileOpStruct;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace CfgFileOperation
{
    /// <summary>
    /// 解析《RRU基本信息表.xls》 rruTypeEntry、rruTypePortEntry
    /// </summary>
    class CfgParseRruExcel
    {
        private List<Dictionary<string, string>> RruInfo = null;//总的东西
        /// <summary>
        /// 保存  内容的内存
        /// </summary>
        private List<Dictionary<string, string>> RruTypeInfo = null; //#define RRUTYPE_NAME           _T("rruType")             //rruType数据库
        //private List<Dictionary<string, string>> RruTypeInfo = null;//#define RRUTYPECONFIG_NAME     _T("rruTypeconfig")             //rruTypeconfig数据库
        private List<Dictionary<string, string>> RruTypePortInfo = null;//#define RRUTYPEPORT_NAME       _T("rruTypePort")         //rruTypePort数据库
        //private List<Dictionary<string, string>> RruTypeInfo = null;//#define RRUTYPEPORTCONFIG_NAME _T("rruTypePortConfig")   //rruTypePortConfig数据库

        /// <summary>
        /// 所有的 sheet 列的名字
        /// </summary>
        private Dictionary<string, string> ColsInfoRru = null;
        /// <summary>
        /// rrutype 用到的 列名
        /// </summary>
        private List<string> ColsNameRruType = null; // void init();

        /// <summary>
        /// rrutypeport 用到的 列名
        /// </summary>
        private List<string> ColsNameRruTypePort = null;

        public CfgParseRruExcel()
        {
            RruInfo = new List<Dictionary<string, string>>();
            ColsInfoRru = new Dictionary<string, string>() {
                {"rruNumber", "A"},                      //(excelColName;MibNodeName)RRU型号编号
                {"rruTypeManufacturerIndex", "B"},       //RRU厂家名称;射频单元生产厂家索引
                {"rruTypeIndex", "C"},                   //RRU硬件类型;射频单元设备类型索引
                {"rruTypeName", "D"},                    //RRU名称;RRU类型名称
                {"rruTypeMaxAntPathNum", "E"},           //支持的天线根数;RRU支持的天线数
                {"rruTypeMaxTxPower", "F"},              //通道的最大发射功率;通道最大发射功率
                {"rruTypeBandWidth", "G"},               //支持的频带总宽度（M）;RRU支持的频带宽度
                {"rruTypeSupportNetWorkMode", "H"},      //支持的工作模式;RRU支持的工作模式
                {"rruTypeSupportCellWorkMode", "I"},     //支持建立的小区类型;RRU支持建立的小区类型
                {"rruTypeMaxPortPathNum", "J"},          //射频通道数;射频通道数
                {"rruTypePortNo", "K"},                  //射频通道编号;远端射频单元上端口编号 射频通道编号
                {"rruTypePortSupportFreqBand", "L"},     //通道支持频段;天线通道支持频段
                {"rruTypeFrequencyRangCarrier", "M"},    //各频段支持的载波数;各频段支持的载波数
                {"rruTypePortCarrier", "N"},             //各射频通道支持的载波数;各射频通道支持的载波数
                {"rruTypePortSupportFreqBandWidth", "O"},//通道支持带宽（M）;天线通道支持频段宽度

                {"rruTypePortPathNo", "P"},              //所属天线编号;RRU通道天线编号
                {"rruTypePortAntMaxPower", "Q"},         //通道支持的最大发送功率;通道支持的最大发送功率
                {"rruTypePortCalAIqTxNom", "R"},         //通道TX基带信号的标定振幅;通道TX基带信号的标定振幅//added by wangpeng2 20121120
                {"rruTypePortCalAIqRxNom", "S"},         //通道RX基带信号的标定振幅;通道RX基带信号的标定振幅
                {"rruTypePortCalPoutTxNom", "T"},        //通道天线单元的标定输出(1/256dbm);通道天线单元的标定输出(1/256dbm)
                {"rruTypePortCalPinRxNom", "U"},         //通道天线单元的标定输入(1/256dbm);通道天线单元的标定输入(1/256dbm)
                {"rruTypeZoomProperty", "V"},            //拉远属性;拉远属性 
                {"rruTypeCompressionProperty", "W"},     //压缩属性;压缩属性 
                {"rruTypeIrRate", "X"},                  //支持的IR口速率;支持的IR口速率
                {"rruTypeIrBand", "Y"},                  //压缩属性与带宽约束关系;压缩属性与带宽约束关系
                {"rruTypePortSupportTxRxStatus", "Z"},   //通道支持收发属性
                {"rruTypeFamilyName", "AA"},//RRU类型族名称", ""},add by shaoqing
            };
        }

        /// <summary>
        /// 处理各个sheet
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strSheet"></param>
        public bool ProcessingExcel(BinaryWriter bw, string strExcelPath, string strSheet)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strSheet))
                return false;
            // 处理所有数据
            return ProcessingExcelRru(bw, strExcelPath, strSheet);
        }
        /// <summary>
        /// 处理"波束扫描原始值"的内容
        /// </summary>
        /// <param name="FilePath"></param>
        private bool ProcessingExcelRru(BinaryWriter bw, string strExcelPath, string strSheet)
        {
            if ((RruInfo == null))
                return false;
            var excelOp = CfgExcelOp.GetInstance();
            int rowCount = excelOp.GetRowCount(strExcelPath, strSheet);                  // 获取行数
            if (-1 == rowCount)
            {
                bw.Write(String.Format("Err:ProcessingExcelRru ({0}):({1}), get row count err.", strExcelPath, strSheet));
                return false;
            }
            int colsCount = excelOp.GetColCount(strExcelPath, strSheet);
            
            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColsInfoRru.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = excelOp.GetRangeVal(strExcelPath, strSheet, ColsInfoRru[colName] + "1", ColsInfoRru[colName] + rowCount);
                ColVals.Add(colName, arry);
            }
            //test(ColVals, rowCount);
            //
            int rowCountTrue = FindEndLineNum(ColVals, rowCount);// 增加保护

            // 处理每行的内容
            // 先处理第一行(即从 line=2开始)
            Console.WriteLine(String.Format("Parsing rru Pro info...\n"));
            Dictionary<string, string> PreInfo = new Dictionary<string, string>();//当下一行有cell为null时，用来获取上一行的数据填充
            int currentLine = 2;
            foreach (var colNameEn in ColsInfoRru.Keys)
            {
                object[,] arry = ColVals[colNameEn];
                string cellVal = GetCellValueToString(arry[currentLine, 1], ColsInfoRru[colNameEn], "");
                PreInfo.Add(colNameEn, cellVal);
            }
            RruInfo.Add(PreInfo);

            Console.WriteLine(String.Format("Parsing rru line3... info...\n"));
            for (currentLine = 3; currentLine < rowCountTrue + 1; currentLine++)
            {
                Dictionary<string, string> CurInfo = new Dictionary<string, string>();//处理当前数据
                foreach (var colNameEn in ColsInfoRru.Keys)
                {
                    object[,] arry = ColVals[colNameEn];
                    string cellVal = GetCellValueToString(arry[currentLine, 1], ColsInfoRru[colNameEn], PreInfo[colNameEn]);
                    CurInfo.Add(colNameEn, cellVal);
                }
                PreInfo = CurInfo;
                RruInfo.Add(CurInfo);
            }
            return true;
        }

        /// <summary>
        /// 获取 rruType 的数据
        /// </summary>
        /// <returns></returns>
        public List<RRuTypeTabStru> GetRruTypeInfoData()
        {
            if (RruInfo == null)
                return null;
            
            List<RRuTypeTabStru> vectRRUTypeInfo = new List<RRuTypeTabStru>();
            string firstRruNumber = "";
            foreach (var rru in RruInfo)
            {
                if (firstRruNumber == "") // 第一次赋值
                {
                    if (String.Equals("", rru["rruNumber"]))
                        continue;// 这个是异常 err
                    else
                        firstRruNumber = rru["rruNumber"];
                }
                else
                {
                    if (String.Equals(firstRruNumber, rru["rruNumber"]))
                        continue;
                    else
                        firstRruNumber = rru["rruNumber"];
                }
                RRuTypeTabStru rruType = new RRuTypeTabStru();
                rruType.excelRead = new GetRruTypeByNodeNameEn(GetRruTypeString);
                rruType.RRuTypeTabStruInit(rru);
                vectRRUTypeInfo.Add(rruType);
            }
            
            return vectRRUTypeInfo;
        }

        /// <summary>
        /// rruType
        /// </summary>
        /// <param name="RRuInfo"></param>
        /// <param name="nodeNameEn"></param>
        /// <returns></returns>
        string GetRruTypeString(Dictionary<string, string> RRuInfo, string nodeNameEn)
        {
            string reStr = "";
            if (RRuInfo == null)
                return reStr;
            if (String.Equals("rruTypeManufacturerIndex", nodeNameEn))//RRU生产厂家索引
                reStr = RRuInfo["rruTypeManufacturerIndex"].ToString();
            //RRU设备类型索引
            else if (String.Equals("rruTypeIndex", nodeNameEn))
                reStr = RRuInfo[("rruTypeIndex")].ToString();
            //RRU类型名称
            else if (String.Equals("rruTypeName", nodeNameEn))
                reStr = RRuInfo[("rruTypeName")].ToString();
            //RRU支持的天线数
            else if (String.Equals("rruTypeMaxAntPathNum", nodeNameEn))
                reStr = RRuInfo[("rruTypeMaxAntPathNum")].ToString();
            //RRU通道最大发射功率
            else if (String.Equals("rruTypeMaxTxPower", nodeNameEn))
                reStr = RRuInfo[("rruTypeMaxTxPower")].ToString();
            //RRU支持的频带宽度
            else if (String.Equals("rruTypeBandWidth", nodeNameEn))
                reStr = RRuInfo[("rruTypeBandWidth")].ToString();
            //RRU支持的小区工作模式
            else if (String.Equals("rruTypeSupportCellWorkMode", nodeNameEn))
                reStr = RRuInfo[("rruTypeSupportCellWorkMode")].ToString();
            //行状态
            else if (String.Equals("rruTypeRowStatus", nodeNameEn))
                reStr = "4";//rruTypeRowStatus = "4";
            // 2014-2-27 luoxin RRUType新增节点 射频单元支持的光纤拉远
            else if (String.Equals("rruTypeFiberLength", nodeNameEn))
                reStr = GetExcelRruInfoRruTypeFiberLength(RRuInfo[("rruTypeZoomProperty")].ToString());
            // rruTypeIrCompressMode
            else if (String.Equals("rruTypeCompressionProperty", nodeNameEn))
                reStr = RRuInfo[("rruTypeCompressionProperty")].ToString();//rruTypeCompressionProperty
            //2016-08-29 guoyingjie add  rruTypeFamilyName
            else if (String.Equals("rruTypeCompressionProperty", nodeNameEn))
                reStr = RRuInfo[("rruTypeFamilyName")].ToString();
            return reStr;
        }
        string GetExcelRruInfoRruTypeFiberLength(string rruTypeZoomProperty)
        {
            if (string.Equals("", rruTypeZoomProperty))
                return rruTypeZoomProperty;
            int indexPos = rruTypeZoomProperty.IndexOf("公里");
            if (-1 == indexPos)
                return rruTypeZoomProperty;
            else
                return rruTypeZoomProperty.Remove(indexPos) + "km";
        }

        /// <summary>
        /// 获取 rruTypePort 的数据
        /// </summary>
        /// <returns></returns>
        public List<RRuTypePortTabStru> GetRruTypePortInfoData()
        {
            if (RruInfo == null)
                return null;
            List<RRuTypePortTabStru> RruTypePort = new List<RRuTypePortTabStru>();

            foreach (var rru in RruInfo)
            {
                string rruTypePortSupportFreqBand = rru["rruTypePortSupportFreqBand"];
                //if (rruTypePortSupportFreqBand.Contains("F频段") || rruTypePortSupportFreqBand.Contains("A频段"))
                //{
                //    Console.WriteLine("F频段 or A频段");
                //}

                RRuTypePortTabStru rruType = new RRuTypePortTabStru();
                rruType.excelRead = new GetRruTypePortByNodeNameEn(GetRruTypePortString);
                rruType.RRuTypePortTabStruInit(rru);
                RruTypePort.Add(rruType);
            }

            return RruTypePort;
        }
        /// <summary>
        /// rruTypePort
        /// </summary>
        /// <param name="RRuInfo"></param>
        /// <param name="nodeNameEn"></param>
        /// <returns></returns>
        string GetRruTypePortString(Dictionary<string, string> RRuInfo, string nodeNameEn)
        {
            string reStr = "";
            if (RRuInfo == null)
                return reStr;
            //RRU生产厂家索引
            else if (String.Equals("rruTypeManufacturerIndex", nodeNameEn))
                reStr = RRuInfo["rruTypeManufacturerIndex"];//;
            //RRU设备类型索引
            else if (String.Equals("rruTypeIndex", nodeNameEn))
                reStr = RRuInfo["rruTypeIndex"];
            //远端射频单元上端口编号
            else if (String.Equals("rruTypePortNo", nodeNameEn))
                reStr = RRuInfo["rruTypePortNo"];
            //天线通道支持频段
            else if (String.Equals("rruTypePortSupportFreqBand", nodeNameEn))
                reStr = RRuInfo["rruTypePortSupportFreqBand"];
            //天线通道支持频段宽度 
            else if (String.Equals("rruTypePortSupportFreqBandWidth", nodeNameEn))
                reStr = RRuInfo["rruTypePortSupportFreqBandWidth"];
            //通道天线编号
            else if (String.Equals("rruTypePortPathNo", nodeNameEn))
                reStr = RRuInfo["rruTypePortPathNo"];
            //行状态
            else if (String.Equals("rruTypePortRowStatus", nodeNameEn))
                reStr = "4";

            //2013-04-10 luoxin DTMUC00153813
            else if (String.Equals("rruTypePortCalAIqRxNom", nodeNameEn))
                reStr = RRuInfo["rruTypePortCalAIqRxNom"];
            else if (String.Equals("rruTypePortCalAIqTxNom", nodeNameEn))
                reStr = RRuInfo["rruTypePortCalAIqTxNom"];
            else if (String.Equals("rruTypePortCalPinRxNom", nodeNameEn))
                reStr = RRuInfo["rruTypePortCalPinRxNom"];
            else if (String.Equals("rruTypePortCalPoutTxNom", nodeNameEn))
                reStr = RRuInfo["rruTypePortCalPoutTxNom"];
            else if (String.Equals("rruTypePortAntMaxPower", nodeNameEn))
                reStr = RRuInfo["rruTypePortAntMaxPower"];
            //根据频段获取载波数（目前只支持A频段和F频段）
            else if (String.Equals("rruTypePortSupportAbandTdsCarrierNum", nodeNameEn))
            {
                string strACarrierNum = "0";
                string strFCarrierNum = "0";
                string strCarrierNum = RRuInfo["rruTypeFrequencyRangCarrier"];
                string strFreqBand = RRuInfo["rruTypePortSupportFreqBand"];
                GetCarrierNumByFreqBandEx(strFreqBand, strCarrierNum, out strACarrierNum, out strFCarrierNum);
                reStr = strACarrierNum;
            }
            else if (String.Equals("rruTypePortSupportFbandTdsCarrierNum", nodeNameEn))
            {
                string strACarrierNum = "0";
                string strFCarrierNum = "0";
                string strCarrierNum = RRuInfo["rruTypeFrequencyRangCarrier"];
                string strFreqBand = RRuInfo["rruTypePortSupportFreqBand"];
                GetCarrierNumByFreqBandEx(strFreqBand, strCarrierNum, out strACarrierNum, out strFCarrierNum);
                reStr = strFCarrierNum;
            }
            return reStr;
        }
        void GetCarrierNumByFreqBandEx(string str_FreqBand, string str_CarrierNum, out string str_ACarrierNum, out string str_FCarrierNum)
        {
            string strFBand = "";
            string strABand = "";
            string strBand = "";

            str_ACarrierNum = "0";
            str_FCarrierNum = "0";

            int iPos = str_FreqBand.IndexOf("/");
            if (iPos > 0)
            {
                strFBand = str_FreqBand.Substring(0, iPos);
                strABand = str_FreqBand.Substring(iPos+1);

                strFBand = strFBand.Substring(0, strFBand.IndexOf("("));//Left(strFBand.Find("("));
                strABand = strABand.Substring(0, strABand.IndexOf("("));// Left(strABand.Find("("));
                //if (String.Equals(str_CarrierNum, "21/9"))
                //{
                //    Console.WriteLine("===");
                //}

                if (0 == String.Compare("F频段", strFBand, true))
                {
                    str_FCarrierNum = str_CarrierNum.Substring(0, str_CarrierNum.IndexOf("/"));// str_CarrierNum.Left(str_CarrierNum.Find("/"));
                }
                if (0 == String.Compare("A频段", strABand, true))
                {
                    str_ACarrierNum = str_CarrierNum.Substring(str_CarrierNum.IndexOf("/")+1);// str_CarrierNum.Right(str_CarrierNum.GetLength() - str_CarrierNum.Find("/") - 1);
                }
            }
            else
            {
                strBand = str_FreqBand.Substring(0, str_FreqBand.IndexOf("("));

                if (0 == String.Compare("F频段", strBand, true))//if (0 == strBand.CompareNoCase("F频段"))
                {
                    str_FCarrierNum = str_CarrierNum;
                }
                if (0 == String.Compare("A频段", strBand, true))//if (0 == strBand.CompareNoCase("A频段"))
                {
                    str_ACarrierNum = str_CarrierNum;
                }
            }
        }
        /// <summary>
        /// 处理 cell 的内容;
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string GetCellValueToString(object array, string colName, object preArray)
        {
            string reStr = "";
            if (array == null)//
                reStr = preArray.ToString();
            else
                reStr = array.ToString();
            return reStr;
        }

        private int FindEndLineNum(Dictionary<string, object[,]> ColVals, int rowCount)
        {
            object[,] arryCol_A = ColVals["rruNumber"];    //RRU型号编号
            object[,] arryCol_K = ColVals["rruTypePortNo"];//射频通道编号

            for (int lineNo = 2; lineNo < rowCount + 1; lineNo++)
            {
                var rruNumber = arryCol_A[lineNo, 1];
                var rruTypePortNo = arryCol_K[lineNo, 1];
                if (rruNumber == null && rruTypePortNo == null)
                    return lineNo;
            }
            return rowCount;
        }

        private void test(Dictionary<string, object[,]> ColVals, int rowCount)
        {
            object[,] arry1 = ColVals["rruNumber"];
            object[,] arry3 = ColVals["rruTypeIndex"];
            int abc = 1;
            int numa = 1;
            int numc = 1;
            for (int currentLine = 1; currentLine < rowCount + 1; currentLine++)
            {
                var a = arry1[currentLine, 1];
                var c = arry3[currentLine, 1];
                if (a == null && c != null)
                    abc = 2;
                if (a != null )
                    numa++;
                if ( c != null)
                    numc++;
            }
        }

        public void TestMdbAndExcel(string strExcelPath, string strMdbPath)
        {
            string sheetName = "RRU基本信息表";
            ProcessingExcel(null,strExcelPath, sheetName);
            List<RRuTypeTabStru> rruList = GetRruTypeInfoData();
            List<RRuTypePortTabStru> rruPortL = GetRruTypePortInfoData();

            // mdb 
            string strSQLRruType = ("select  * from rruType");
            DataSet rruTypedateSet = new CfgAccessDBManager().GetRecord(strMdbPath, strSQLRruType);
            int rruTypeCount = rruTypedateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数

            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            //string strSQL = ("select * from MibTree where MIBName='rruTypeEntry' DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            string strSQL = ("select * from MibTree where MIBName='rruTypeEntry'");

            DataSet MibdateSet = new CfgAccessDBManager().GetRecord(strFileToDirectory , strSQL);
            DataRow tableRow = MibdateSet.Tables[0].Rows[0];
            int nTableNum = int.Parse(tableRow["TableContent"].ToString());//表容量

            // mdb rrutype
            List<RRuTypeTabStru> vectRRUTypeInfo = new List<RRuTypeTabStru>();
            for (int loop = 0; loop < rruTypeCount - 1; loop++)
            {
                if (loop == nTableNum) break;
                vectRRUTypeInfo.Add(new RRuTypeTabStru(rruTypedateSet.Tables[0].Rows[loop], rruTypedateSet));
            }

            // mdb rrutypeport
            string strSQLRruTypePort = ("select  * from rruTypePort");
            DataSet rruTypePortDateSet = new CfgAccessDBManager().GetRecord(strMdbPath, strSQLRruTypePort);
            int rruTypePortCount = rruTypePortDateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数

            //strSQL = ("select * from MibTree where MIBName='rruTypePortEntry' DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            strSQL = ("select * from MibTree where MIBName='rruTypePortEntry' ");
            MibdateSet = new CfgAccessDBManager().GetRecord(strFileToDirectory, strSQL);
            tableRow = MibdateSet.Tables[0].Rows[0];
            nTableNum = int.Parse(tableRow["TableContent"].ToString());//表容量

            List<RRuTypePortTabStru> vectRRUTypePortInfo = new List<RRuTypePortTabStru>();
            for (int loop = 0; loop < rruTypePortCount - 1; loop++)
            {  // 在表之间循环
                if (loop == nTableNum)
                    break;
                vectRRUTypePortInfo.Add(new RRuTypePortTabStru(rruTypePortDateSet.Tables[0].Rows[loop]));
            }


            is_same_rru(rruList, vectRRUTypeInfo);

            new CfgAccessDBManager().Close(rruTypedateSet);
            new CfgAccessDBManager().Close(MibdateSet);
            new CfgAccessDBManager().Close(rruTypePortDateSet);
        }
        void is_same_rru(List<RRuTypeTabStru> ExData, List<RRuTypeTabStru> MdbData)
        {
            List<byte> bugbuff = new List<byte>();
            foreach (var mdb in MdbData)
            {
                string rruTypeIndex = mdb.rruTypeIndex;
                int pos = ExData.FindIndex(e => e.rruTypeIndex == rruTypeIndex);
                if (-1 == pos)
                {
                    string bugbf = String.Format("***rruTypeIndex no = {0}, not in ex.\n", rruTypeIndex);
                    byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(bugbf);
                    bugbuff.AddRange(byteArray);
                    Console.WriteLine(bugbf);
                    continue;
                }
                is_same_pama(mdb, ExData[pos], bugbuff);
            }
            foreach(var ex in ExData)
            {
                string rruTypeIndex = ex.rruTypeIndex;
                int pos = MdbData.FindIndex(e => e.rruTypeIndex == rruTypeIndex);
                if (-1 == pos)
                {
                    string bugbf = String.Format("***rruTypeIndex no = {0}, not in mdb.\n", rruTypeIndex);
                    byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(bugbf);
                    bugbuff.AddRange(byteArray);
                    Console.WriteLine(bugbf);
                    //continue;
                }
                //is_same_pama(mdb, ExData[pos], bugbuff);
            }
            
        }


        void is_same_pama(RRuTypeTabStru mdb, RRuTypeTabStru ex, List<byte> bugbuff)
        {
            string bufBug = "";
            //RRU生产厂家索引
            if (!String.Equals(mdb.rruTypeManufacturerIndex, ex.rruTypeManufacturerIndex)) {
                bufBug += String.Format("rruTypeManufacturerIndex: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.rruTypeManufacturerIndex, ex.rruTypeManufacturerIndex);
            }
            //RRU设备类型索引
            if (!String.Equals(mdb.rruTypeIndex, ex.rruTypeIndex))
            {
                bufBug += String.Format("rruTypeIndex: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.rruTypeIndex, ex.rruTypeIndex);
            }
            if (!String.Equals(mdb.rruTypeName, ex.rruTypeName)) {
                bufBug += String.Format("rruTypeName: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.rruTypeName, ex.rruTypeName);
            }
            //RRU支持的天线数
            if (!String.Equals(mdb.rruTypeMaxAntPathNum, ex.rruTypeMaxAntPathNum)) {
                bufBug += String.Format("rruTypeMaxAntPathNum: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.rruTypeMaxAntPathNum, ex.rruTypeMaxAntPathNum);
            }
            //RRU通道最大发射功率
            if (!String.Equals(mdb.rruTypeMaxTxPower, ex.rruTypeMaxTxPower)) {
                bufBug += String.Format("rruTypeMaxTxPower: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.rruTypeMaxTxPower, ex.rruTypeMaxTxPower);
            }
            //RRU支持的频带宽度
            if (!String.Equals(mdb.rruTypeBandWidth, ex.rruTypeBandWidth)) {
                bufBug += String.Format("rruTypeBandWidth: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.rruTypeBandWidth, ex.rruTypeBandWidth);
            }
            //RRU支持的小区工作模式
            if (!String.Equals(mdb.rruTypeSupportCellWorkMode, ex.rruTypeSupportCellWorkMode)) {
                bufBug += String.Format("rruTypeSupportCellWorkMode: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.rruTypeSupportCellWorkMode, ex.rruTypeSupportCellWorkMode);
            }
            //行状态
            //if (!String.Equals(mdb.rruTypeRowStatus = "4";
            //2014-2-27 luoxin RRUType新增节点
            if (!String.Equals(mdb.strRruTypeFiberLength, ex.strRruTypeFiberLength))
            {
                bufBug += String.Format("rruTypeFiberLength: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.strRruTypeFiberLength, ex.strRruTypeFiberLength);
            }// new RRuTypeTabStru().GetExcelRruInfoRruTypeFiberLength(bufBug += String.Format("rruTypeZoomProperty"));

            if (!String.Equals(mdb.strRruTypeIrCompressMode, ex.strRruTypeIrCompressMode))
            {
                bufBug += String.Format("rruTypeCompressionProperty: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.strRruTypeIrCompressMode, ex.strRruTypeIrCompressMode);
            }//rruTypeCompressionProperty

            //2016-08-29 guoyingjie add  rruTypeFamilyName
            if (!String.Equals(mdb.strRruTypeFamilyName, ex.strRruTypeFamilyName))
            {
                bufBug += String.Format("rruTypeFamilyName: mdb.no={0}, mdb.v={1}, ex.v={2} \n", mdb.rruTypeIndex, mdb.strRruTypeFamilyName, ex.strRruTypeFamilyName);
            }
            Console.WriteLine(bufBug);
        }
    }
}
