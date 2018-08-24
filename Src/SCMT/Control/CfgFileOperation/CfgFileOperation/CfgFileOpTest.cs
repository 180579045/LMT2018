using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MIBDataParser.JSONDataMgr;


namespace CfgFileOperation
{
    
    /// <summary>
    /// .cfg 文件相关操作的测试代码
    /// </summary>
    class CfgFileOpTest
    {
        static void Main(string[] args)
        {

            //CfgFile_Header a = new CfgFile_Header();
            //var sizeA = System.Runtime.InteropServices.Marshal.SizeOf(a);
            //Console.WriteLine(System.Runtime.InteropServices.Marshal.SizeOf(a));
            //Console.ReadLine();

            CfgFileOp cfgHandle = new CfgFileOp();
            //
            string currentPath = System.Environment.CurrentDirectory;
            string fileToUnzip = currentPath+ "\\Data\\lmdtz\\lm.dtz";//
            string fileToDire = currentPath + "\\Data\\lmdtz";
            
            //cfgHandle.CreateCfgFile("", fileToUnzip, fileToDire);

            cfgHandle.test();
        }
    }
}
