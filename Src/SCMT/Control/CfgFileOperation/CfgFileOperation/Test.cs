using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MIBDataParser.JSONDataMgr;
using CfgFileOpStruct;


namespace CfgFileOperation
{
    
    /// <summary>
    /// .cfg 文件相关操作的测试代码
    /// </summary>
    class Test
    {
        static void Main(string[] args)
        {
            Test test = new Test();

            //test.testForParseAlarmEx();

            test.testForCreatePatchAndInit();

            //test.testForReadSelfExcel();

            //test.testForReadRecList();//

            //test.testLoadMibTreeIntoMem();//

            //test.testForReadExcelRruType();

            //test.testForReadExcelAnnt();

            //test.testForOpReadExcelForCfg();

            //test.test4();

        }
        /// <summary>
        /// 生成 init 和 patch
        /// </summary>
        void testForCreatePatchAndInit()
        {
            string dataBasePath  = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\5GCfg\\";
            string dataMdbPath   = "lm.mdb";//1.数据库
            string antennaExPath = "LTE_基站天线广播波束权值参数配置表_5G.xls";//2.天线信息
            string alarmExPath   = "eNB告警信息表.xls";//3.告警信息
            string rruInfoExPath = "RRU基本信息表.xls";//4.RRU信息
            string reclistExPath = "RecList_V6.00.50.05.40.07.01.xls";//5.reclist
            string selfDefExPath = "自定义_初配数据文件_ENB_5G_00_00_05.xls";//6.自定义文件(init, patch)

            CfgOp cfgOp = new CfgOp();
            Dictionary<string, string> paths = new Dictionary<string, string>() {
                { "DataMdb", dataBasePath+dataMdbPath},
                { "Antenna", dataBasePath+antennaExPath},
                { "Alarm",   dataBasePath+alarmExPath},
                { "RruInfo" ,dataBasePath+rruInfoExPath},
                { "Reclist" ,dataBasePath+reclistExPath},
                { "SelfDef" ,dataBasePath+selfDefExPath},
            };
            cfgOp.OnCreatePatchAndInitCfg(paths);
        }

        void testForParseAlarmEx()
        {
            string alarmMdbPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\LMTAlarm.mdb";
            string alarmExPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\eNB告警信息表.xls";
            CfgParseAlarmExecl alarmEx = new CfgParseAlarmExecl();
            alarmEx.CfgParseAlarmExeclAndMdb(alarmExPath, alarmMdbPath);
        }


        void testForReadSelfExcel()
        {
            // 加载lm.mdb到内存
            CfgOp cfgOp = new CfgOp();
            string strCfgFileName = "";
            string FileToDirectory = "";
            string strDBPath = "";
            string strDBName = ".\\Data\\lmdtz\\lm.dtz";
            //cfgOp.CreateCfgFile(strCfgFileName, FileToDirectory, strDBPath, strDBName);

            // reclist
            cfgOp.m_reclistExcel = new CfgParseReclistExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            string UeType = "0:默认";
            //reclist.ProcessingExcel(excelPath, strFileToDirectory, UeType, cfgOp);
            cfgOp.m_reclistExcel.ProcessingExcel(excelPath, strFileToDirectory, UeType, cfgOp);

            // 自定义文件
            excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\自定义_初配数据文件_ENB_5G_00_00_05.xls";
            //CfgParseSelfExcel selfEx = new CfgParseSelfExcel();
            //selfEx.ProcessingExcel(excelPath, strFileToDirectory, "init", cfgOp);
            //selfEx.ProcessingExcel(excelPath, strFileToDirectory, "patch", cfgOp);
            cfgOp.m_selfExcel = new CfgParseSelfExcel();
            cfgOp.m_selfExcel.ProcessingExcel(excelPath, strFileToDirectory, "init", cfgOp);
            cfgOp.m_selfExcel.ProcessingExcel(excelPath, strFileToDirectory, "patch", cfgOp);
        }

        void testForReadRecList()
        {
            // 加载lm.mdb到内存
            CfgOp cfgOp = new CfgOp();
            string strCfgFileName = "";
            string FileToDirectory = "";
            string strDBPath = "";
            string strDBName = ".\\Data\\lmdtz\\lm.dtz";
            //cfgOp.CreateCfgFile(strCfgFileName, FileToDirectory, strDBPath, strDBName);

            // reclist
            //CfgParseReclistExcel reclist = new CfgParseReclistExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            string UeType = "0:默认";
            cfgOp.m_reclistExcel.ProcessingExcel(excelPath, strFileToDirectory, UeType, cfgOp);
        }

        void testLoadMibTreeIntoMem()
        {
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            CfgParseDBMibTreeToMemory mibTree = new CfgParseDBMibTreeToMemory();
            mibTree.ReadMibTreeToMemory(strFileToDirectory);
            int a = 1;
        }

        void testForReadExcelRruType()
        {
            CfgParseRruExcel rru = new CfgParseRruExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RRU基本信息表_ty.xls";
            //string sheetName = "RRU基本信息表";
            //rru.ProcessingExcel(excelPath, sheetName);


            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\LMTDBENODEB70.mdb";
            //List<RRuTypeTabStru> rruList = rru.GetRruTypeInfoData();
            rru.TestMdbAndExcel(excelPath, strFileToDirectory);
            //List<RRuTypePortTabStru> rruPortL = rru.GetRruTypePortInfoData();
        }

        void testForReadExcelAnnt()
        {
            CfgParseAntennaExcel dd = new CfgParseAntennaExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\LTE_基站天线广播波束权值参数配置表_5G.xls";
            string sheetName = "波束扫描原始值";
            dd.ProcessingAntennaExcel(excelPath, sheetName);
            List<Dictionary<string, string>> data = dd.GetBeamScanData();
        }

        void testForOpReadExcelForCfg()
        {
            CfgExcelOp exOp = new CfgExcelOp();
            exOp.test(".\\123\\eNB告警信息表.xls");

        }

        void testForCfgFileOp()
        {
            string strCfgFileName = "";
            string FileToDirectory = "";
            string strDBPath = "";
            string strDBName = ".\\Data\\lmdtz\\lm.dtz";

            CfgOp cfgOp = new CfgOp();
            //cfgOp.CreateCfgFile(strCfgFileName, FileToDirectory, strDBPath, strDBName);
            cfgOp.SaveFile_eNB("./path.cfg");
        }

        void test1()
        {
            uint dreamduip = new Test().getIPAddr("192.168.3.144");
            bool re = new Test().TestForCfgFileOpStructMain();
        }
        public void test2()
        {
            string str1 = "1987-04-20 23:05:59"; 
            byte[] strToBytes1 = System.Text.Encoding.UTF8.GetBytes(str1);//可以
            byte[] strToBytes2 = System.Text.Encoding.Default.GetBytes(str1);//可以
            byte[] strToBytes3 = System.Text.Encoding.Unicode.GetBytes(str1);//no
            byte[] strToBytes4 = System.Text.Encoding.ASCII.GetBytes(str1);//可以
            byte[] strToBytes5 = System.Text.Encoding.UTF32.GetBytes(str1);//no
            byte[] strToBytes6 = System.Text.Encoding.UTF7.GetBytes(str1);//可以
            if (str1.Length < 19)
                return ;
            byte b = strToBytes1[4];
            if ((strToBytes1[4] != '-') || (strToBytes1[7] != '-') || (strToBytes1[10] != ' ') || (strToBytes1[13] != ':') || (strToBytes1[16] != ':'))
                return ;
            if ((str1[4] != '-') || (str1[7] != '-') || (str1[10] != ' ') || (str1[13] != ':') || (str1[16] != ':'))
                return ;
        }
        public void test3()
        {
            bool a = IsNumeric("dd");
            bool a2 = IsNumeric2("dd");
            bool a3 = IsNumeric("1234");
            bool a4 = IsNumeric2("1234");
        }
        bool IsNumeric(string InStr)
        {
            //
            //    //当数字字符串的为是少于4时，以下三种都可以转换，任选一种
            //    //如果位数超过4的话，请选用Convert.ToInt32() 和int.Parse()
            //    //result = int.Parse(message);
            
            //cfgHandle.CreateCfgFile("", fileToUnzip, fileToDire);
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(InStr))
            {
                return true;
            }
            else
                return false;
        }
        bool IsNumeric2(string InStr)
        {
            int result = -1;   //result 定义为out 用来输出值
            try
            {
                result = Convert.ToInt32(InStr);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void test4()
        {
            bool a = IsValidateDate(2018,2,40);//日
            bool a2 = IsValidateDate(2018, 13, 31);//月
            bool a3 = IsValidateDate(1, 2, 2);//年
            bool a4 = IsValidateDate(2018, 2, 3);
        }
        bool IsValidateDate(int y, int m, int d)
        {
            int[] a = { 31, (y % 4 == 0 && y % 100 != 0 || y % 400 == 0) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return m >= 1 && m <= 12 && d >= 1 && d <= a[m - 1];
        }

        bool TestForCfgFileOpStructMain()
        {
            if (!TestForCfgFileOpStructTestOM_OBJ_ID_T())
                return false;
            return true;
        }
        bool TestForCfgFileOpStructTestOM_OBJ_ID_T()
        {
            int intSizeOf = 4 + 30 * 4; // 8+240
            int intSizeOf2 = System.Runtime.InteropServices.Marshal.SizeOf(new OM_OBJ_ID_T());
            if (intSizeOf != intSizeOf2)
            {
                Console.WriteLine(String.Format("OM_OBJ_ID_T sizeof should be {0},but now is {1}"), intSizeOf, intSizeOf2);
                return false;
            }
            return true;
        }
        void test_string_Substring_use()
        {
            string a, b;
            a = "123456789";
            int pos = a.IndexOf('3');
            b = a.Substring(a.Length - 4);//6789
            b = a.Substring(0, 4);         //值为:1234 (起点，长度)
            b = a.Substring(3);            //值为:456789
            b = a.Substring(2, 4);         //值为:3456
        }
        public uint getIPAddr(string ipAddr)
        {
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(ipAddr);
            long dreamduip = ipaddress.Address;//转换为 90 03 a8 c0
            long x = dreamduip;
            long aaa = ((((x) & 0x000000ff) << 24) | (((x) & 0x0000ff00) << 8) | (((x) & 0x00ff0000) >> 8) | (((x) & 0xff000000) >> 24));
            return (uint)aaa;
        }
    }
}
