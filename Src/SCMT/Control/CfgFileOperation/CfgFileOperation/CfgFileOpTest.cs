using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CfgFileOperation
{
    /// <summary>
    /// .cfg 文件相关操作的测试代码
    /// </summary>
    class CfgFileOpTest
    {
        static void Main(string[] args)
        {
            // u8   u8VerifyStr[4];
            //typedef unsigned char u8;
            byte u8VerifyStr = new byte() { };
            Byte[] order = new byte[2];
            

            //var dfd = order.GetType();

            Console.WriteLine(sizeof(byte));
            Console.WriteLine(System.Runtime.InteropServices.Marshal.SizeOf(u8VerifyStr));

            CfgFileOp cfgHandle = new CfgFileOp();
            //
            string currentPath = System.Environment.CurrentDirectory;
            string fileToUnzip = currentPath+ "\\Data\\lmdtz\\lm.dtz";//
            string fileToDire = currentPath + "\\Data\\lmdtz";
            
            cfgHandle.CreateCfgFile("", fileToUnzip, fileToDire);
        }
    }
}
