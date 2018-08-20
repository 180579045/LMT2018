using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace CfgFileOperation
{
    [StructLayout(LayoutKind.Sequential)]
    class CfgFile_Header123
    {
        //int a = 1;                                // 4bit
        //byte[] u8VerifyStr = new byte[4];         // [4]文件头的校验字段 "ICF"  
        //byte u8HiDeviceType = new byte();                      //
        //byte u8MiDeviceType;                      //
        //ushort u16LoDevType;
        //uint u32IcFileVer;                        //  初配文件版本：用来标志当前文件的大版本
        //uint u32ReserveVer;                       //  初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本
        //uint u32DataBlk_Location;                 //  数据块起始位置 
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        //byte[] u8LastMotifyDate = new byte[20];   //  [20]文件最新修改的日期, 按字符串存放 
        //uint u32IcFile_HeaderVer;                 //  初配文件头版本，用于记录不同的文件头格式、版本 
        //uint u32PublicMibVer;                     //  公共Mib版本号
        //uint u32MainMibVer;                       //  Mib主版本号
        //uint u32SubMibVer;                        //  Mib辅助版本号
        //uint u32IcFile_HeaderLength;              //  初配文件头部长度 
        //byte[] u8IcFileDesc = new byte[256];      //   [256]文件描述 
        //uint u32RevDatType;                       //  保留段数据类别 (1: 文件描述) 
        //uint u32IcfFileType;                      //  初配文件类别（1:NB,2:RRS） 2005-12-22 
        //uint u32IcfFileProperty;                  //  初配文件属性（0:正式文件;1:补充文件）
        //uint u32DevType;                          //  设备类型(1:超级基站;2:紧凑型小基站)

        //ushort u16NeType;                         //  数据文件所属网元类型
        //byte[] u8Pading = new byte[2];            //  [2]
        //sbyte[] s8DataFmtVer = new sbyte[12];     //  [12] 数据文件版本（与对应的MIB版本相同）  
        //byte u8TblNum;                            //  数据文件中表的个数  
        //byte u8FileType;                          //  配置文件类别(1:cfg或dfg,2:pdg)  
        //byte u8Pad1;                              //  保留 
        //byte u8ReserveAreaType;                   //  保留空间的含义 =0  
        ////==================================================================//
        //uint[] u32TblOffset = new uint[150];      // [150] 每个表的数据在文件中的起始位置（相对文件头）  
        //byte[] reserved = new byte[4];            // [4] 保留字段 
    }
    /// <summary>
    /// .cfg 文件相关操作的测试代码
    /// </summary>
    class CfgFileOpTest
    {
        static void Main(string[] args)
        {

            CfgFile_Header123 a = new CfgFile_Header123();
            var sizeA = System.Runtime.InteropServices.Marshal.SizeOf(a);
            Console.WriteLine(System.Runtime.InteropServices.Marshal.SizeOf(a));



            Byte[] order = new byte[2];

            
            byte[] myBytes = new byte[5] { 1, 2, 3, 4, 5 };

            Console.WriteLine(System.Runtime.InteropServices.Marshal.SizeOf(myBytes));
            BitArray myBA = new BitArray(myBytes);
            Console.WriteLine(System.Runtime.InteropServices.Marshal.SizeOf(myBA));
            int c = myBA.Length;



            CfgFileOp cfgHandle = new CfgFileOp();
            //
            string currentPath = System.Environment.CurrentDirectory;
            string fileToUnzip = currentPath+ "\\Data\\lmdtz\\lm.dtz";//
            string fileToDire = currentPath + "\\Data\\lmdtz";
            
            cfgHandle.CreateCfgFile("", fileToUnzip, fileToDire);
        }
    }
}
