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
    class CfgFileOpTest
    {
        static void Main(string[] args)
        {

            new CfgReadAntennaExcel();

            new CfgFileOpTest().testForOpReadExcelForCfg();


            new CfgFileOpTest().test4();

            //string strCfgFileName = "";
            //string FileToDirectory = "";
            //string strDBPath = "";
            //string strDBName = ".\\Data\\lmdtz\\lm.dtz";
            //byte[] byteArray = System.Text.Encoding.Default.GetBytes("123");
            //Array.Reverse(byteArray);
            //CfgOp cfgOp = new CfgOp();
            //cfgOp.CreateCfgFile(strCfgFileName, FileToDirectory, strDBPath, strDBName);
            //cfgOp.SaveFile_eNB("./path.cfg");
            //Console.ReadLine();
        }

        void testForOpReadExcelForCfg()
        {
            CfgFileExcelReadWrite exOp = new CfgFileExcelReadWrite();
            exOp.test(".\\123\\eNB告警信息表.xls");

        }

        void test1()
        {
            uint dreamduip = new CfgFileOpTest().getIPAddr("192.168.3.144");
            bool re = new CfgFileOpTest().TestForCfgFileOpStructMain();
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
