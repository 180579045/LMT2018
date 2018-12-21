using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Compression;// zip
using System.IO;// File
using System.Runtime.InteropServices;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace CfgFileOpStruct
{
    /// <summary>
    /// 宏定义
    /// </summary>
    class MacroDefinition
    {
        /* 数据文件所属网元类型 */
        public byte NETTYPE = 3;                                          //2014-7-21 luoxin OMC校验字段，改为ENB取值3 
        /* u8HiDeviceType : Nodeb基站类型（1 版、2版、3版超级基站）       */
        public byte NB_DEVICE  = 0;                                       // u8HiDeviceType 使用
        public byte EPS_DEVICE = 1;                                       // u8HiDeviceType 使用
        public byte IMS_DEVICE = 2;                                       // u8HiDeviceType 使用
        /* u8MiDeviceType : Td或LTE 设备, 5G 还没设定 */
        public byte TD_DEVICE  = 0;                                       // TD  基站文件 
        public byte LTE_DEVICE = 1;                                       // LTE 基站文件
        public byte G5_DEVICE  = 2;                                       // 5G  基站文件
        /* : 大小端标识                               */
        public byte PC_VERSION = 0;                                       // 小端序，适用于EPS，IMS的初配文件
        public byte VXWORK_VERSION = 1;                                   // 大端序，适用于ENB的初配文件
        /* u32IcFileVer : Cfg 版本                    */
        /*   以下两个宏都可标示当前Cfg版本为1         */
        public byte CFG_VERSION_1 = 1;                                    // 可标示当前Cfg版本为1
        public byte CFG_VERSION_1_2 = 2;                                  // 可标示当前Cfg版本为1
        /*   以下宏标示当前Cfg版本为2                 */
        public byte CFG_VERSION_2 = 10;                                   // 可标示当前Cfg版本为2
        /* :  */
        enum LMTORTYPE
        {
	        LMTORTYPE_UNKNOWN = -1,
	        LMTORTYPE_ENB = 1,
	        LMTORTYPE_IMS = 2,
	        LMTORTYPE_EPS = 3,
	        LMTORTYPE_RNC3000 = 4,
	        LMTORTYPE_CUSTOM			//自定义模式
	
        };
        /* : lmt 工作模式 */
        enum WORKMODE
        {
            WORKMODE_UNKNOWN = -1,
            WORKMODE_ONLINE,      /*在线工作模式*/
            WORKMODE_OFFLINE      /*离线工作模式*/
        };
        /* u32IcFile_HeaderVer : 文件头版本 */
        public byte TD_ICF_HEADER  = 1;                                   // TD  文件头
        public byte LTE_ICF_HEADER = 2;                                   // LTE 文件头
        /* u8FileType : 配置文件类别(1:cfg或dfg,2:pdg) */
        public byte OM_ENB_CFGORPDF_FILE = 1;                             // cfg或dfg 配置文件
        public byte OM_ENB_PDG_FILE      = 2;                             // pdg      配置文件

        /*  */
        public string ALARMCAUSEENTRY = ("alarmCauseEntry");
        public string ANTENNATYPEENTRY = ("antennaArrayTypeEntry");
        public string ANNTENNAWEITHTENTRY = ("antennaWeightEntry");
        public string RRUTYPEENTRY = ("rruTypeEntry");
        public string RRUTYPEPORTENTRY = ("rruTypePortEntry");

        /**/
        public string ADJCELLEUTRAENTRY = "cellAdjEutraCellEntry";
        public string ADJCELLUTRAFDDENTRY = "cellAdjUtraFddCellEntry";
        public string ADJCELLUTRATDDENTRY = "cellAdjUtraTddCellEntry";
        public string ADJCELLGERANENTRY = "cellAdjGeranCellEntry";
        public string ADJCELLCDMA2000ENTRY = "cellAdjCdma2000CellEntry";

        /*  */
        /// <summary>
        /// u8            u8ConfigFileOrNot
        /// </summary>
        public enum CONFIGFILEORNOT
        {
            OM_IS_NOT_CONFIG_FILE = 0,                                    /*不需要:0(false)*/
            OM_IS_CONFIG_FILE = 1,                                        /*需要:1(true)*/
            OM_IS_PDGFILE = 2,                                            /*需要:2(true)*/
        };
        /// <summary>
        /// u8 u8DataType;/*数据类型*/
        /// </summary>
        public enum DATATYPE
        {
            OM_INVALID_VALUE = 0,                                         /*数据类型:0(非法类型)*/
            OM_S8_VALUE = 1,                                              /*数据类型:1(s8类型)*/
            OM_U8_VALUE = 2,                                              /*数据类型:2(u8类型)*/
            OM_S16_VALUE = 3,                                             /*数据类型:3(s16类型)*/
            OM_U16_VALUE = 4,                                             /*数据类型:4(u16类型)*/
            OM_S32_VALUE = 5,                                             /*数据类型:5(s32类型)*/
            OM_U32_VALUE = 6,                                             /*数据类型:6(u32类型)*/
            OM_FLOAT_VALUE = 7,                                           /*数据类型:7(Float类型)*/
            OM_IP_ADDRESS_VALUE = 8,                                      /*数据类型:8(IP地址类型)*/
            OM_EBUFFER_VALUE = 9,                                         /*数据类型:9(字符串类型)*/
            OM_OID_VALUE = 10,                                            /*数据类型:10(OID类型)*/
            OM_MAC_ADDRESS_VALUE = 11,                                    /*数据类型:11(MAC地址)*/
            OM_DATATIME_VALUE = 12,                                       /*数据类型:12 u8[]类型:DataTime类型*/
            OM_INETADDRESS_VALUE = 13,                                    /*数据类型:13 u8[]类型:IPv4,ipv6*/
            OM_U32ARRAY_VALUE = 14,                                       /*数据类型:u32[]类型*/
            OM_MNCMCC_VALUE = 15,                                         /*数据类型:MncMccType类型*/
            OM_INETADDRESSTYPE_VALUE = 16,                                /*数据类型:InetAddressType类型*/
        };

        public enum ENUM_MIBVALUETYPE
        {
            MIBVALUETYPE_UNKNOWN = -1,
            MIBVALUETYPE_LONG,
            MIBVALUETYPE_UINT32,
            MIBVALUETYPE_STRING,
            MIBVALUETYPE_IPADDR,
            MIBVALUETYPE_MACADDR,
            MIBVALUETYPE_ARRAY,
            MIBVALUETYPE_MOI,
            MIBVALUETYPE_MOC,
            MIBVALUETYPE_ENUM,
            MIBVALUETYPE_DATETIME,
            MIBVALUETYPE_IPV4,
            MIBVALUETYPE_IPV6,
            MIBVALUETYPE_SOFTVER,
            MIBVALUETYPE_RETURNCODE,
            MIBVALUETYPE_SEQID,
            MIBVALUETYPE_SHCMDPARA,
            MIBVALUETYPE_BITS,
            MIBVALUETYPE_IMSI,
            MIBVALUETYPE_TMSI,
            MIBVALUETYPE_IMEI,
            MIBVALUETYPE_IMSISTRING,
            MIBVALUETYPE_MNCSTRING,
            MIBVALUETYPE_MCCSTRING,
            //wangshengfu 2010.06.21
            MIBVALUETYPE_DATE,
            MIBVALUETYPE_TIME
            //end of wangshengfu
        };
        public enum MibNodeType
        {
            IndexNode,
            RowStatusNode,
            NormalNode,
        };
        //用户权限, 与LMTPrivilege表的'PrivilegeID'字段一一对应
        public enum USERPRIVILEGE
        {
            USERPRIVILEGE_MODULE_DEVELOPER = 1,//模块权限上的开发人员权限
            USERPRIVILEGE_MODULE_TEST = 2,//模块权限上的测试权限
            USERPRIVILEGE_MODULE_ENGINEER = 3,//模块权限上的工程人员权限
            USERPRIVILEGE_MODULE_RESTRICT = 4, //受限权限
            USERPRIVILEGE_ELEM_ENB = 5,//网元权限上的ENB操作人员权限
            USERPRIVILEGE_ELEM_EPS = 6,//网元权限上的EPS操作人员权限
            USERPRIVILEGE_ELEM_IMS = 7,//网元权限上的IMS操作人员权限
            USERPRIVILEGE_USER_NORMAL = 8,//用户权限上的普通用户
            USERPRIVILEGE_USER_USERMGR = 9,//用户管理
            USERPRIVILEGE_UNKNOWN = 100,//值越大, 权限越小
        };
        /// <summary>
        /// 
        /// </summary>
        public int MAX_INDEXCOUNT = 6;  //MIB的最大维数


        /* dsfsdfsdf sf */
        public void InvalidateCfgHeader()
        {
            bool m_bEmptyCfg;
            byte g_VerFlag;
            byte[] u32NodebVer = new byte[4];
            LMTORTYPE eLmtorType;
            WORKMODE eWorkMode;
            //iReadNum = fread(&m_cfgFile_Header, 1, sizeof(OMIC_STRU_ICFile_Header), m_pcfgOut);
            //long highDeviceType = m_cfgFile_Header.u32NodebVer[0];  //
            long highDeviceType = u32NodebVer[0];  //u8HiDeviceType
            if (highDeviceType == NB_DEVICE)
            {
                m_bEmptyCfg = false;
                g_VerFlag = VXWORK_VERSION;
                string strDbName = "LMTDBENODEB70.mdb";
                //memcpy(pLmtorInfo->strDBName, strDbName, strDbName.GetLength());
                //pLmtorInfo->eLmtorType = LMTORTYPE_ENB;
                eLmtorType = LMTORTYPE.LMTORTYPE_ENB;
                //pLmtorInfo->eWorkMode = WORKMODE_ONLINE;
                eWorkMode = WORKMODE.WORKMODE_ONLINE;
            }
            else if (highDeviceType == IMS_DEVICE)
            {
                m_bEmptyCfg = true;
                g_VerFlag = PC_VERSION;
                string strDbName = "LMTDBIMS.mdb";
                //memcpy(pLmtorInfo->strDBName, strDbName, strDbName.GetLength());
                //pLmtorInfo->eLmtorType = LMTORTYPE_IMS;
                eLmtorType = LMTORTYPE.LMTORTYPE_IMS;
                //pLmtorInfo->eWorkMode = WORKMODE_ONLINE;
                eWorkMode = WORKMODE.WORKMODE_ONLINE;
                //break;
            }
            else if (highDeviceType == EPS_DEVICE)
            {
                m_bEmptyCfg = true;
                g_VerFlag = PC_VERSION;
                string strDbName = "LMTDBEPS.mdb";
                //memcpy(pLmtorInfo->strDBName, strDbName, strDbName.GetLength());
                //pLmtorInfo->eLmtorType = LMTORTYPE_EPS;
                eLmtorType = LMTORTYPE.LMTORTYPE_EPS;
                //pLmtorInfo->eWorkMode = WORKMODE_ONLINE;
                eWorkMode = WORKMODE.WORKMODE_ONLINE;
                //break;
            }
            else
            {
                string strMsg = "5021";
                //strMsg.LoadString(IDS_STRCFGTYPEFAIL);
                //AfxMessageBox(strMsg);
                //return FALSE;
            }



        }
    }

    ///  .cfg 文件头
    ///  zizeof : 956 
    ///  LayoutKind.Sequential属性让结构体在导出到非托管内存时按出现的顺序依次布局,
    ///  pack 4 字节按4对齐
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct StruCfgFileHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        byte[] u8VerifyStr;                              //  [4]文件头的校验字段 "ICF"  

        byte u8HiDeviceType;                             //  Nodeb基站类型（1 版(LMTORTYPE_ENB)、2版(LMTORTYPE_IMS)、3版(LMTORTYPE_EPS)超级基站）
        byte u8MiDeviceType;                             //  TD/LTE/5G的文件
        ushort u16LoDevType;                             //  计算大小端
        public uint u32IcFileVer;                        //  初配文件版本：用来标志当前文件的大版本(版本为1,2)
        public uint u32ReserveVer;                       //  初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本
                                                         //                : 从数据版本号中截取 ("5_65_3_6",截取6);
        public uint u32DataBlk_Location;                 //  数据块起始位置 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        byte[] u8LastMotifyDate;                         //  [20]文件最新修改的日期, 按字符串存放 

        public uint u32IcFile_HeaderVer;                 //  初配文件头版本，用于记录不同的文件头格式、版本 
        public uint u32PublicMibVer;                     //  公共Mib版本号 : 从数据版本号中截取("5_65_3_6",截取5)
        public uint u32MainMibVer;                       //  Mib主版本号   : 从数据版本号中截取("5_65_3_6",截取65)
        public uint u32SubMibVer;                        //  Mib辅助版本号 : 从数据版本号中截取("5_65_3_6",截取3)
        public uint u32IcFile_HeaderLength;              //  初配文件头部长度 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        byte[] u8IcFileDesc;                            //   [256]文件描述 

        public uint u32RevDatType;                       //  保留段数据类别 (1: 文件描述) 
        public uint u32IcfFileType;                      //  初配文件类别（1:NB,2:RRS） 2005-12-22 
        public uint u32IcfFileProperty;                  //  初配文件属性（0:正式文件;1:补充文件）
        public uint u32DevType;                          //  设备类型(1:超级基站;2:紧凑型小基站)

        public ushort u16NeType;                         //  数据文件所属网元类型
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        byte[] u8Pading;                                 //  [2]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        sbyte[] s8DataFmtVer;                           //  [12] 数据文件版本（与对应的MIB版本相同）  

        public byte u8TblNum;                            //  数据文件中表的个数  
        public byte u8FileType;                          //  配置文件类别(1:cfg或dfg,2:pdg)  
        public byte u8Pad1;                              //  保留 
        public byte u8ReserveAreaType;                   //  保留空间的含义 =0  
        //==================================================================//
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 150)]
        public uint[] u32TblOffset;                      // [150] 每个表的数据在文件中的起始位置（相对文件头）  

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        byte[] u8reserved;                                // [4] 保留字段 

        //////////////////////////////////////////////////////////////////////////
        /*********************        功能函数(结构转byte序等)      ***************************/
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="s"></param>
        public StruCfgFileHeader(string s = "")
        {
            u8VerifyStr = new byte[4];                    //  [4]文件头的校验字段 "ICF" 
            u8HiDeviceType = 0x00;                        //
            u8MiDeviceType = 0x00;                        //
            u16LoDevType = 0;                             //
            u32IcFileVer = 0;                             //  初配文件版本：用来标志当前文件的大版本
            u32ReserveVer = 0;                            //  初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本
            u32DataBlk_Location = 0;                      //  数据块起始位置 
            u8LastMotifyDate = new byte[20];              //  [20]文件最新修改的日期, 按字符串存放 
            u32IcFile_HeaderVer = 0;                      //  初配文件头版本，用于记录不同的文件头格式、版本 
            u32PublicMibVer = 0;                          //  公共Mib版本号
            u32MainMibVer = 0;                            //  Mib主版本号
            u32SubMibVer = 0;                             //  Mib辅助版本号
            u32IcFile_HeaderLength = 0;                   //  初配文件头部长度 
            u8IcFileDesc = new byte[256];                 //   [256]文件描述 
            u32RevDatType = 0;                            //  保留段数据类别 (1: 文件描述) 
            u32IcfFileType = 0;                           //  初配文件类别（1:NB,2:RRS） 2005-12-22 
            u32IcfFileProperty = 0;                       //  初配文件属性（0:正式文件;1:补充文件）
            u32DevType = 0;                               //  设备类型(1:超级基站;2:紧凑型小基站)
            u16NeType = 0;                                //  数据文件所属网元类型
            u8Pading = new byte[2];                       //  [2]
            s8DataFmtVer = new sbyte[12];                 //  [12] 数据文件版本（与对应的MIB版本相同）  
            u8TblNum = 0x00;                              //  数据文件中表的个数  
            u8FileType = 0x00;                            //  配置文件类别(1:cfg或dfg,2:pdg)  
            u8Pad1 = 0x00;                                //  保留 
            u8ReserveAreaType = 0x00;                     //  保留空间的含义 =0  
            u32TblOffset = new uint[150];                 //  [150] 每个表的数据在文件中的起始位置（相对文件头）  
            u8reserved = new byte[4];                     //  [4] 保留字段 
        }
        /// <summary>
        /// 把 Struct CfgFile_Header 中的参数转化成byte[]串
        /// </summary>
        /// <returns></returns>
        public byte[] StruToByteArray()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileHeader())];//CfgFile_Header 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, u8VerifyStr);            //  [4]文件头的校验字段 "ICF" 
            SetValueToByteArray(byteAL, bytePosL, u8HiDeviceType);         //
            SetValueToByteArray(byteAL, bytePosL, u8MiDeviceType);         //
            SetValueToByteArray(byteAL, bytePosL, u16LoDevType);           //
            SetValueToByteArray(byteAL, bytePosL, u32IcFileVer);           //  初配文件版本：用来标志当前文件的大版本
            SetValueToByteArray(byteAL, bytePosL, u32ReserveVer);          //  初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本
            SetValueToByteArray(byteAL, bytePosL, u32DataBlk_Location);    //  数据块起始位置 
            SetValueToByteArray(byteAL, bytePosL, u8LastMotifyDate);       //  [20]文件最新修改的日期, 按字符串存放 
            SetValueToByteArray(byteAL, bytePosL, u32IcFile_HeaderVer);    //  初配文件头版本，用于记录不同的文件头格式、版本 
            SetValueToByteArray(byteAL, bytePosL, u32PublicMibVer);        //  公共Mib版本号
            SetValueToByteArray(byteAL, bytePosL, u32MainMibVer);          //  Mib主版本号
            SetValueToByteArray(byteAL, bytePosL, u32SubMibVer);           //  Mib辅助版本号
            SetValueToByteArray(byteAL, bytePosL, u32IcFile_HeaderLength); // 初配文件头部长度
            SetValueToByteArray(byteAL, bytePosL, u8IcFileDesc);           //   [256]文件描述 
            SetValueToByteArray(byteAL, bytePosL, u32RevDatType);          //  保留段数据类别 (1: 文件描述) 
            SetValueToByteArray(byteAL, bytePosL, u32IcfFileType);         //  初配文件类别（1:NB,2:RRS） 2005-12-22 
            SetValueToByteArray(byteAL, bytePosL, u32IcfFileProperty);     //  初配文件属性（0:正式文件;1:补充文件）
            SetValueToByteArray(byteAL, bytePosL, u32DevType);             //  设备类型(1:超级基站;2:紧凑型小基站)
            SetValueToByteArray(byteAL, bytePosL, u16NeType);              //  数据文件所属网元类型
            SetValueToByteArray(byteAL, bytePosL, u8Pading);               //  [2]
            SetValueToByteArray(byteAL, bytePosL, s8DataFmtVer);           //  [12] 数据文件版本（与对应的MIB版本相同）  
            SetValueToByteArray(byteAL, bytePosL, u8TblNum);               //  数据文件中表的个数  
            SetValueToByteArray(byteAL, bytePosL, u8FileType);             //  配置文件类别(1:cfg或dfg,2:pdg)  
            SetValueToByteArray(byteAL, bytePosL, u8Pad1);                 //  保留 
            SetValueToByteArray(byteAL, bytePosL, u8ReserveAreaType);      //  保留空间的含义);//0  
            SetValueToByteArray(byteAL, bytePosL, u32TblOffset);           //  [150] 每个表的数据在文件中的起始位置（相对文件头）  
            SetValueToByteArray(byteAL, bytePosL, u8reserved);             //  [4] 保留字段 
            return byteArray;
        }
        /// <summary>
        /// 颠倒每个数据
        /// </summary>
        /// <returns></returns>
        public byte[] StruToByteArrayReverse()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileHeader())];//CfgFile_Header 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };

            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, u8VerifyStr);                   //  [4]文件头的校验字段 "ICF" 
            SetValueToByteArrayReverse(byteAL, bytePosL, u8HiDeviceType);         //
            SetValueToByteArrayReverse(byteAL, bytePosL, u8MiDeviceType);         //
            SetValueToByteArrayReverse(byteAL, bytePosL, u16LoDevType);           //
            SetValueToByteArrayReverse(byteAL, bytePosL, u32IcFileVer);           //  初配文件版本：用来标志当前文件的大版本
            SetValueToByteArrayReverse(byteAL, bytePosL, u32ReserveVer);          //  初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本
            SetValueToByteArrayReverse(byteAL, bytePosL, u32DataBlk_Location);    //  数据块起始位置 
            SetValueToByteArray(byteAL, bytePosL, u8LastMotifyDate);              //  [20]文件最新修改的日期, 按字符串存放 
            SetValueToByteArrayReverse(byteAL, bytePosL, u32IcFile_HeaderVer);    //  初配文件头版本，用于记录不同的文件头格式、版本 
            SetValueToByteArrayReverse(byteAL, bytePosL, u32PublicMibVer);        //  公共Mib版本号
            SetValueToByteArrayReverse(byteAL, bytePosL, u32MainMibVer);          //  Mib主版本号
            SetValueToByteArrayReverse(byteAL, bytePosL, u32SubMibVer);           //  Mib辅助版本号
            SetValueToByteArrayReverse(byteAL, bytePosL, u32IcFile_HeaderLength); //  初配文件头部长度
            SetValueToByteArray(byteAL, bytePosL, u8IcFileDesc);                  //   [256]文件描述 
            SetValueToByteArrayReverse(byteAL, bytePosL, u32RevDatType);          //  保留段数据类别 (1: 文件描述) 
            SetValueToByteArrayReverse(byteAL, bytePosL, u32IcfFileType);         //  初配文件类别（1:NB,2:RRS） 2005-12-22 
            SetValueToByteArrayReverse(byteAL, bytePosL, u32IcfFileProperty);     //  初配文件属性（0:正式文件;1:补充文件）
            SetValueToByteArrayReverse(byteAL, bytePosL, u32DevType);             //  设备类型(1:超级基站;2:紧凑型小基站)
            SetValueToByteArrayReverse(byteAL, bytePosL, u16NeType);              //  数据文件所属网元类型
            SetValueToByteArrayReverse(byteAL, bytePosL, u8Pading);               //  [2]
            SetValueToByteArray(byteAL, bytePosL, s8DataFmtVer);                  //  [12] 数据文件版本（与对应的MIB版本相同）  
            SetValueToByteArrayReverse(byteAL, bytePosL, u8TblNum);               //  数据文件中表的个数  
            SetValueToByteArrayReverse(byteAL, bytePosL, u8FileType);             //  配置文件类别(1:cfg或dfg,2:pdg)  
            SetValueToByteArrayReverse(byteAL, bytePosL, u8Pad1);                 //  保留 
            SetValueToByteArrayReverse(byteAL, bytePosL, u8ReserveAreaType);      //  保留空间的含义);//0  
            SetValueToByteArrayReverse(byteAL, bytePosL, u32TblOffset);           //  [150] 每个表的数据在文件中的起始位置（相对文件头）  
            SetValueToByteArrayReverse(byteAL, bytePosL, u8reserved);             //  [4] 保留字段 
            return byteArray;
        }
        /*********************        功能函数(变量赋值)             ***************************/
        /// <summary>
        /// 设置 u8VerifyStr
        /// </summary>
        /// <param name="str"></param>
        public void Setu8VerifyStr(string str)
        {
            if (u8VerifyStr == null)
                u8VerifyStr = new byte[4];
            StringToByteArray(str, u8VerifyStr);
        }
        /// <summary>
        /// 获取 u8VerifyStr
        /// </summary>
        /// <returns></returns>
        public byte[] Getu8VerifyStr()
        {
            return u8VerifyStr;
        }
        /// <summary>
        /// u8HiDeviceType : 根据Nodeb基站类型确定
        /// </summary>
        public void Setu8HiDeviceType(byte typeValue)
        {
            // 根据Nodeb基站类型
            u8HiDeviceType = typeValue;
            //LMTORTYPE orType = pInfo->eLmtorType;
            //if (orType == LMTORTYPE_ENB)
            //{
            //    m_eNB_cfgFile_Header.u8HiDeviceType = NB_DEVICE;
            //    m_bEmptyCfg = FALSE;
            //}
            //else if (orType == LMTORTYPE_EPS)
            //{
            //    m_eNB_cfgFile_Header.u8HiDeviceType = EPS_DEVICE;
            //}
            //else if (orType == LMTORTYPE_IMS)
            //{
            //    m_eNB_cfgFile_Header.u8HiDeviceType = IMS_DEVICE;
            //}
            //else
            //{
            //    CString sStrTypeError;
            //    sStrTypeError.LoadString(IDS_STRCFGTYPEFAIL);
            //    AfxMessageBox(sStrTypeError);
            //    return FALSE;
            //}
        }
        public byte Getu8HiDeviceType(){ return u8HiDeviceType; }
        /// <summary>
        /// u8MiDeviceType : TD/LTE/5G的文件
        /// </summary>
        /// <param name="typeValue"></param>
        public void Setu8MiDeviceType(byte typeValue)
        {
            u8MiDeviceType = typeValue;// new MacroDefinition().LTE_DEVICE;
        }
        public byte Getu8MiDeviceType() { return u8MiDeviceType; }
        /// <summary>
        /// u16LoDevType : 计算大小端
        /// </summary>
        public void Setu16LoDevType()
        {
            ushort LoDeviceType = 700;
            byte[] tempType = new byte[2];
            tempType[0] = (byte)((0xff00 & LoDeviceType) >> 8);
            tempType[1] = (byte)(0xff & LoDeviceType);
            //* ((char*)(&tempType)) = LoDeviceType / 256;
            //*(((char*)(&tempType)) + 1) = LoDeviceType % 256;
            u16LoDevType = BitConverter.ToUInt16(tempType, 0);
        }
        public ushort Getu16LoDevType(){return u16LoDevType;}
        /// <summary>
        /// u32IcFileVer : 初配文件版本：用来标志当前文件的大版本(版本为1,2)
        /// </summary>
        /// <param name="m_cfgEditVersion">默认"cfg_Version_2"</param>
        public void Setu32IcFileVer(string m_cfgEditVersion= "cfg_Version_2")
        {
            // 原来 LMT 界面选项 "cfg_Version_1"，"cfg_Version_2"
            // 新工具 scmt 默认为 版本2
            if (m_cfgEditVersion == "cfg_Version_1")
            {
                u32IcFileVer = new MacroDefinition().CFG_VERSION_1;
            }
            else
            {
                u32IcFileVer = new MacroDefinition().CFG_VERSION_2;
            }
        }
        public uint Getu32IcFileVer() { return u32IcFileVer; }
        /// <summary>
        /// strMibVersion="5.65.3.6" => u32PublicMibVer = 5;u32MainMibVer = 65;u32SubMibVer = 3;u32ReserveVer = 6;
        /// </summary>
        /// <param name="strMibVersion"></param>
        public void FillVerInfo(string strMibVersion)
        {
            //"5_65_3_6";
            //u32PublicMibVer = 5;
            //u32MainMibVer = 65;
            //u32SubMibVer = 3;
            //u32ReserveVer = 6;
            string strTempMibVersion = strMibVersion;
            int nPos = strTempMibVersion.IndexOf('_');
            List<string> vecMibVer = new List<string>();
            while (-1 != nPos)
            {
                string strMibVer = strTempMibVersion.Substring(0, nPos);
                vecMibVer.Add(strMibVer);
                strTempMibVersion = strTempMibVersion.Substring(nPos + 1);
                nPos = strTempMibVersion.IndexOf('_');
            }
            vecMibVer.Add(strTempMibVersion);
            //string u32PublicMibVer1 = vecMibVer[0];
            //string u32MainMibVer1 = vecMibVer[1];
            //string u32SubMibVer1 = vecMibVer[2];
            //string u32ReserveVer1 = vecMibVer[3];
            u32PublicMibVer = uint.Parse(vecMibVer[0]);
            u32MainMibVer = uint.Parse(vecMibVer[1]);
            u32SubMibVer = uint.Parse(vecMibVer[2]);
            u32ReserveVer = uint.Parse(vecMibVer[3]);

            vecMibVer = null;
        }
        /// <summary>
        /// u8LastMotifyDate : 文件最新修改的日期, 按字符串存放
        /// </summary>
        /// <param name="str"></param>
        public void Setu8LastMotifyDate(string str)
        {
            if (u8LastMotifyDate == null)
                u8LastMotifyDate = new byte[20];
            StringToByteArray(str, u8LastMotifyDate);
        }
        public byte[] Getu8LastMotifyDate() { return u8LastMotifyDate; }

        /// <summary>
        /// u8IcFileDesc : [256]文件描述 （"初配文件"）
        /// </summary>
        /// <param name="str">中文字符串</param>
        public void Setu8IcFileDesc(string str)
        {
            if (u8IcFileDesc == null)
                u8IcFileDesc = new byte[256];
            // str 是 中文 特殊处理
            //byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
            //Buffer.BlockCopy(byteArray, 0, u8IcFileDesc, 0, byteArray.Length);
            StringChToByteArray(str, u8IcFileDesc);
        }
        public byte[] Getu8IcFileDesc() { return u8IcFileDesc; }
        /// <summary>
        /// s8DataFmtVer : [12] 数据文件版本（与对应的MIB版本相同）
        /// </summary>
        /// <param name="str">一般为"ICF"</param>
        public void Sets8DataFmtVer(string str)
        {
            if (u8LastMotifyDate == null)
                s8DataFmtVer = new sbyte[12];
            StringToSByteArray(str, s8DataFmtVer);
        }
        public sbyte[] Gets8DataFmtVer() { return s8DataFmtVer; }
        /*********************        功能函数(解析文件读取的字符串)***************************/
        public void ParseFileReadBytes(byte[] byteArray)
        {
            int startPos = 0;// 开始pos
            int lenSize = 0;
            byte[] bData;
            //-> u8VerifyStr  1 * 4:Byte[4][4]文件头的校验字段 "ICF"
            startPos = 0;
            lenSize = 4;
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u8VerifyStr = GetBytesValueToParmBytes(bData);
            string stru8VerifyStr = System.Text.Encoding.Default.GetString(u8VerifyStr).TrimEnd('\0');
            // ->u8HiDeviceType   1 * 1:byte Nodeb基站类型（1 版 = 0、2版 = 2、3版超级基站 = 1）
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u8HiDeviceType);
            u8HiDeviceType = byteArray[startPos];
            //-> u8MiDeviceType; 1 * 1:byte TD = 0 / LTE = 1 / 5G = 2的文件
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u8MiDeviceType);
            u8MiDeviceType = byteArray[startPos];
            //->u16LoDevType; 2 * 1:ushort 计算大小端 700拆分成 0xbc和0x02组合
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u16LoDevType);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u16LoDevType = GetBytesValToUshort(bData);
            //->u32IcFileVer 4 * 1:uint 初配文件版本：用来标志当前文件的大版本 = 1,2
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32IcFileVer);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32IcFileVer = GetBytesValToUint(bData);// 版本1:=1;版本2:=10
            //->u32ReserveVer; 4 * 1:uint 初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本(版本号"5_65_3_6", 截取6)
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32ReserveVer);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32ReserveVer = GetBytesValToUint(bData);
            //->u32DataBlk_Location; 4 * 1:uint 数据块起始位置 默认956
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32DataBlk_Location);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32DataBlk_Location = GetBytesValToUint(bData);
            //->u8LastMotifyDate[20]; 1 * 20:byte[20][20] 文件最新修改的日期, 按字符串存放 
            startPos += lenSize;
            lenSize = 20;
            u8LastMotifyDate = byteArray.Skip(startPos).Take(lenSize).ToArray();
            string stru8LastMotifyDate = System.Text.Encoding.Default.GetString(u8LastMotifyDate).TrimEnd('\0');
            //u8LastMotifyDate = OxbytesToString(bData);

            //-> u32IcFile_HeaderVer; 4 * 1:uint 初配文件头版本，用于记录不同的文件头格式、版本
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32IcFile_HeaderVer);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32IcFile_HeaderVer = GetBytesValToUint(bData);
            //->u32PublicMibVer; 4 * 1:uint 公共Mib版本号(版本号"5_65_3_6", 截取5)
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32PublicMibVer);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32PublicMibVer = GetBytesValToUint(bData);
            //-> u32MainMibVer; 4 * 1:uint Mib主版本号(版本号"5_65_3_6", 截取65)
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32MainMibVer);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32MainMibVer = GetBytesValToUint(bData);
            //-> u32SubMibVer; 4 * 1:uint Mib辅助版本号(版本号"5_65_3_6", 截取3)
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32SubMibVer);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32SubMibVer = GetBytesValToUint(bData);
            //-> u32IcFile_HeaderLength; 4 * 1:uint 初配文件头部长度 
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32IcFile_HeaderLength);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32IcFile_HeaderLength = GetBytesValToUint(bData);
            //-> u8IcFileDesc[256]    1 * 256:byte[256][256] 文件描述 “初配文件”
            startPos += lenSize;
            lenSize = 256;
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u8IcFileDesc = bData;
            string stru8IcFileDesc = System.Text.Encoding.Default.GetString(u8IcFileDesc.Skip(0).Take(9).ToArray());
            //-> u32RevDatType; 4 * 1:uint 保留段数据类别 (1: 文件描述) 
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32RevDatType);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32RevDatType = GetBytesValToUint(bData);
            //-> u32IcfFileType; 4 * 1:uint 初配文件类别（1:NB,2:RRS） 2005 - 12 - 22
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32IcfFileType);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32IcfFileType = GetBytesValToUint(bData);
            //->u32IcfFileProperty; 4 * 1:uint 初配文件属性（0:正式文件; 1:补充文件）
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32IcfFileProperty);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32IcfFileProperty = GetBytesValToUint(bData);
            //-> u32DevType; 4 * 1:uint 设备类型(1:超级基站; 2:紧凑型小基站)
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u32DevType);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u32DevType = GetBytesValToUint(bData);
            //-> u16NeType    2 * 1:ushort 数据文件所属网元类型
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u16NeType);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u16NeType = GetBytesValToUshort(bData);
            //-> u8Pading[2]  1 * 2:byte[2][2]
            startPos += lenSize;
            lenSize = 2;
            //-> s8DataFmtVer[12] 1 * 2:sbyte[12][12] 数据文件版本（与对应的MIB版本相同）  
            startPos += lenSize;
            lenSize = 12;
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            s8DataFmtVer = GetBytesValToSBytes(bData);
            string strs8DataFmtVer = Encoding.Default.GetString(bData).TrimEnd('\0');
            //-> u8TblNum 1 * 1:byte 数据文件中表的个数  
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u8TblNum);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u8TblNum = bData[0];
            //-> u8FileType   1 * 1:byte 配置文件类别(1, init.cfg:cfg,或dfg,2 patch_ex.cfg:pdg,)
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new StruCfgFileHeader().u8FileType);
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            u8FileType = bData[0];
            //-> u8Pad1   1 * 1:byte 保留 
            startPos += lenSize;
            lenSize = 1;
            //-> u8ReserveAreaType    1 * 1:byte 保留空间的含义 = 0
            startPos += lenSize;
            lenSize = 1;
            //  ->u32TblOffset[150]    4 * 150:uint[150][150] 每个表的数据在文件中的起始位置（相对文件头）  
            startPos += lenSize;
            lenSize = Marshal.SizeOf(new int())*150;
            bData = byteArray.Skip(startPos).Take(lenSize).ToArray();
            //u32TblOffset = GetBytesValToSBytes(bData);
            //-> Reserved[4]  1 * 4:byte[4][4] 保留字段

        }
        string OxbytesToString(byte[] bytes)
        {
            string hexString = string.Empty;
            Array.Reverse((byte[])bytes);
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        uint GetBytesValToUint(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }

        byte[] GetBytesValueToParmBytes(byte[] bytes)
        {
            byte[] re = new byte[] { };
            Array.Reverse((byte[])bytes);
            //Buffer.BlockCopy((byte[])bytes, 0, (byte[])re, 0, ((byte[])bytes).Length);
            return bytes;
        }

        ushort GetBytesValToUshort(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt16(OxbytesToString(bytes), 16);
        }

        sbyte[] GetBytesValToSBytes(byte[] bytes)
        {
            sbyte[] objParm = new sbyte[bytes.Length];
            Buffer.BlockCopy(bytes, 0, objParm, 0, bytes.Length);
            return objParm;
        }
        /*********************        功能函数(私有)             ***************************/
        /// <summary>
        /// 把 各种type参数 转化成字节串byte[]
        /// </summary>
        /// <param name="byteAL">目的字节串的列表</param>
        /// <param name="bytePosL">目的字节串保存的位置</param>
        /// <param name="objParm">要保存的参数</param>
        private void SetValueToByteArray(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
        private void SetValueToByteArrayReverse(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Array.Reverse((byte[])objParm);
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Array.Reverse((sbyte[])objParm);
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Array.Reverse((byte[])TypeToByteArr);
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
        /// <summary>
        /// 把 string(Enlish, not have Chinese) 依次ASCII化后填写到byte[]中
        /// </summary>
        /// <param name="strEn">需要转化的字符串(字符串要求内容要求是英文、字母、数字，不能有中文)</param>
        /// <param name="byteParm">被赋值的变量参数</param>
        private void StringToByteArray(string strEn, byte[] byteParm)
        {
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(strEn);// 转化成byte[]
            int strLen = (byteArray.Length < byteParm.Length) ? (byteArray.Length) : (byteParm.Length);// 转化长度以短的为主
            Array.Copy(byteArray, byteParm, strLen);// 转化
        }
        /// <summary>
        /// 把 中文字符串 依次填写到byte[]中
        /// </summary>
        /// <param name="strCh">中文字符串</param>
        /// <param name="byteParm">被赋值的变量参数</param>
        private void StringChToByteArray(string strCh, byte[] byteParm)
        {
            // str 是 中文 特殊处理
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(strCh);
            int strLen = (byteArray.Length < byteParm.Length) ? (byteArray.Length) : (byteParm.Length);
            Array.Copy(byteArray, byteParm, strLen);
            //Buffer.BlockCopy(byteArray, 0, byteParm, 0, byteArray.Length);
        }
        /// <summary>
        /// 把 string 依次填写到 sbyte[]中
        /// </summary>
        /// <param name="str">需要转化的字符串</param>
        /// <param name="sbyteParm">被赋值的变量参数</param>
        private void StringToSByteArray(string str, sbyte[] sbyteParm)
        {
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
            int strLen = (byteArray.Length < sbyteParm.Length) ? (byteArray.Length) : (sbyteParm.Length);// 转化长度以短的为主
            for (int i = 0; i < strLen; i++)
            {
                if (byteArray[i] > 127)
                    sbyteParm[i] = (sbyte)(byteArray[i] - 256);
                else
                    sbyteParm[i] = (sbyte)byteArray[i];
            }
        }
    }

    /// <summary>
    ///  数据块的头 ，为将来堆叠准备
    ///  zizeof : 24 
    ///  LayoutKind.Sequential属性让结构体在导出到非托管内存时按出现的顺序依次布局,
    ///  pack 4 字节按4对齐
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    struct StruDataHead
    {
        /// <summary>
        /// 文件头的校验字段
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] u8VerifyStr;                       // [4] 文件头的校验字段 "BEG\0"
        /// <summary>
        /// reserved , =1 
        /// </summary>
        public uint u32DatType;                          // reserved , =1 
        /// <summary>
        /// reserved , =1 
        /// </summary>
        public uint u32DatVer;                           // reserved , =1 
        /// <summary>
        /// 表的数目,指示索引表中的向目个数
        /// </summary>
        public uint u32TableCnt;                         // 表的数目,指示索引表中的向目个数

        /// <summary>
        /// 保留
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] u32Reserved;                       // [2] 保留

        ////////////////////////////////////////////////////////////////
        /*********************        功能函数(结构转byte序等)      ***************************/
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="s"></param>
        public StruDataHead(string s)
        {
            u8VerifyStr = new byte[4];
            u32DatType  = 0;
            u32DatVer   = 0;
            u32TableCnt = 0;
            u32Reserved = new uint[2];
        }
        /// <summary>
        /// 把 Struct DataHead 中的参数转化成byte[]串
        /// </summary>
        /// <returns></returns>
        public byte[] StruToByteArray()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruDataHead())];// DataHead 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, u8VerifyStr);            // [4]文件头的校验字段 "ICF" 
            SetValueToByteArray(byteAL, bytePosL, u32DatType);             //
            SetValueToByteArray(byteAL, bytePosL, u32DatVer);              //
            SetValueToByteArray(byteAL, bytePosL, u32TableCnt);            //
            SetValueToByteArray(byteAL, bytePosL, u32Reserved);            //  
            return byteArray;
        }
        public byte[] StruToByteArrayReverse()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruDataHead())];// DataHead 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, u8VerifyStr);            // [4]文件头的校验字段 "ICF" 
            SetValueToByteArrayReverse(byteAL, bytePosL, u32DatType);             //
            SetValueToByteArrayReverse(byteAL, bytePosL, u32DatVer);              //
            SetValueToByteArrayReverse(byteAL, bytePosL, u32TableCnt);            //
            SetValueToByteArrayReverse(byteAL, bytePosL, u32Reserved);            //  
            return byteArray;
        }
        /*********************        功能函数(变量赋值)         ***************************/
        public void Setu8VerifyStr(string str= "BEG\0")
        {
            if (u8VerifyStr == null)
                u8VerifyStr = new byte[4];
            StringToByteArray(str, u8VerifyStr);
        }
        /*********************        功能函数(把字符串解析成成员变量)**********************/
        /// <summary>
        /// 根据字符串依次解析头
        /// </summary>
        /// <param name="data"></param>
        public void SetValueByBytes(byte[] data)
        {
            int fromOf = 0;
            int lenSize = 0;
            //
            fromOf = 0;
            lenSize = Marshal.SizeOf(new byte()) * 4;
            byte[] bytes123 = data.Skip(fromOf).Take(lenSize).ToArray();
            u8VerifyStr = GetBytesValueToParmBytes(bytes123);
            //
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new StruDataHead().u32DatType);
            byte[] bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u32DatType = GetBytesValToUint(bytes);
            //
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new StruDataHead().u32DatVer);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u32DatVer = GetBytesValToUint(bytes);

            //
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new StruDataHead().u32TableCnt);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u32TableCnt = GetBytesValToUint(bytes);
        }
        /*********************        功能函数(私有)             ***************************/
        /// <summary>
        /// 把 string(Enlish, not have Chinese) 依次ASCII化后填写到byte[]中
        /// </summary>
        /// <param name="strEn">需要转化的字符串(字符串要求内容要求是英文、字母、数字，不能有中文)</param>
        /// <param name="byteParm">被赋值的变量参数</param>
        private void StringToByteArray(string strEn, byte[] byteParm)
        {
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(strEn);// 转化成byte[]
            int strLen = (byteArray.Length < byteParm.Length) ? (byteArray.Length) : (byteParm.Length);// 转化长度以短的为主
            Array.Copy(byteArray, byteParm, strLen);// 转化
        }
        /// <summary>
        /// 把 各种type参数 转化成字节串byte[]
        /// </summary>
        /// <param name="byteAL">目的字节串的列表</param>
        /// <param name="bytePosL">目的字节串保存的位置</param>
        /// <param name="objParm">要保存的参数</param>
        private void SetValueToByteArray(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
        private void SetValueToByteArrayReverse(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Array.Reverse((byte[])objParm);
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Array.Reverse((sbyte[])objParm);
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Array.Reverse((byte[])TypeToByteArr);
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }

        private string OxbytesToString(byte[] bytes)
        {
            string hexString = string.Empty;
            Array.Reverse((byte[])bytes);
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        private uint GetBytesValToUint(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }

        private byte[] GetBytesValueToParmBytes(byte[] bytes)
        {
            byte[] re = new byte[] { };
            Array.Reverse((byte[])bytes);
            //Buffer.BlockCopy((byte[])bytes, 0, (byte[])re, 0, ((byte[])bytes).Length);
            return bytes;
        }
    }

    /// <summary>
    /// 表头信息
    /// zizeof : 44 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    struct StruCfgFileTblInfo
    {
        /// <summary>
        /// 数据文件版本（即数据更新次数）
        /// </summary>
        public ushort u16DataFmtVer;           // 数据文件版本（即数据更新次数）
        /// <summary>
        /// 保留字段
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        byte[] u8pad;                          // [2] 保留字段
        /// <summary>
        /// 表名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] u8TblName;                      // [32] 表名
        /// <summary>
        /// 本表的单个记录包含的字段数
        /// </summary>
        public ushort u16FieldNum;             // 本表的单个记录包含的字段数,叶子节点数
        /// <summary>
        /// 单个记录的有效长度（单位：字节）
        /// </summary>
        public ushort u16RecLen;               // 单个记录的有效长度（单位：字节）
        /// <summary>
        /// 数据文件中包含的记录数量--实例数
        /// </summary>
        public uint u32RecNum;                 // 数据文件中包含的记录数量--实例数
        /*********************        功能函数(结构转byte序等)      ***************************/
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="s"></param>
        public StruCfgFileTblInfo(string s)
        {
            u16DataFmtVer = 0;
            u8pad = new byte[2];
            u8TblName = new byte[32];
            u16FieldNum = 0;
            u16RecLen = 0;
            u32RecNum = 0;
        }
        public object DeepCopy()
        {
            StruCfgFileTblInfo s = new StruCfgFileTblInfo("init");
            s.u16DataFmtVer = this.u16DataFmtVer;
            Array.Copy(this.u8TblName, s.u8TblName, this.u8TblName.Length);
            //Array.Copy(s.u8TblName, this.u8TblName, this.u8TblName.Length); 
            s.u16FieldNum = this.u16FieldNum;
            s.u16RecLen = this.u16RecLen;
            s.u32RecNum = this.u32RecNum;
            return s;
        }
        /// <summary>
        /// 把 Struct DataHead 中的参数转化成byte[]串
        /// </summary>
        /// <returns></returns>
        public byte[] StruToByteArray()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileTblInfo("init"))];// DataHead 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, u16DataFmtVer);
            SetValueToByteArray(byteAL, bytePosL, u8pad);
            SetValueToByteArray(byteAL, bytePosL, u8TblName);
            SetValueToByteArray(byteAL, bytePosL, u16FieldNum);
            SetValueToByteArray(byteAL, bytePosL, u16RecLen); 
            SetValueToByteArray(byteAL, bytePosL, u32RecNum);
            return byteArray;
        }
        public byte[] StruToByteArrayReverse()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileTblInfo("init"))];// DataHead 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, u16DataFmtVer);
            SetValueToByteArray(byteAL, bytePosL, u8pad);
            SetValueToByteArray(byteAL, bytePosL, u8TblName);
            SetValueToByteArrayReverse(byteAL, bytePosL, u16FieldNum);
            SetValueToByteArrayReverse(byteAL, bytePosL, u16RecLen);
            SetValueToByteArrayReverse(byteAL, bytePosL, u32RecNum);
            return byteArray;
        }
        /*********************        功能函数(变量赋值)             ***************************/
        /// <summary>
        /// byte[] u8TblName;// [32] 表名
        /// </summary>
        /// <param name="str"></param>
        public void Setu8TblName(string str)
        {
            if (u8TblName == null)
                u8TblName = new byte[32];
            StringToByteArray(str, u8TblName);
        }
        /*********************        功能函数(私有)             ***************************/
        /// <summary>
        /// 把 string(Enlish, not have Chinese) 依次ASCII化后填写到byte[]中
        /// </summary>
        /// <param name="strEn">需要转化的字符串(字符串要求内容要求是英文、字母、数字，不能有中文)</param>
        /// <param name="byteParm">被赋值的变量参数</param>
        private void StringToByteArray(string strEn, byte[] byteParm)
        {
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(strEn);// 转化成byte[]
            int strLen = (byteArray.Length < byteParm.Length) ? (byteArray.Length) : (byteParm.Length);// 转化长度以短的为主
            Array.Copy(byteArray, byteParm, strLen);// 转化
        }
        /// <summary>
        /// 把 各种type参数 转化成字节串byte[]
        /// </summary>
        /// <param name="byteAL">目的字节串的列表</param>
        /// <param name="bytePosL">目的字节串保存的位置</param>
        /// <param name="objParm">要保存的参数</param>
        private void SetValueToByteArray(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
        private void SetValueToByteArrayReverse(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Array.Reverse((byte[])objParm);
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Array.Reverse((sbyte[])objParm);
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Array.Reverse((byte[])TypeToByteArr);
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }

        public void SetBytesValue(byte[] data)
        {
            int fromOf = 0;
            int lenSize = 0;
            byte[] bytes;
            //
            fromOf = 0;
            lenSize = Marshal.SizeOf(new ushort());
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u16DataFmtVer = GetBytesValToU16(bytes);
            //
            //
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte())*2;
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u8pad = GetBytesValueToParmBytes(bytes);
            //
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte()) * 32;
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u8TblName = GetBytesValueToParmBytes2(bytes);// new byte[32];
            //
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(u16FieldNum);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u16FieldNum = GetBytesValToU16(bytes);

            fromOf += lenSize;
            lenSize = Marshal.SizeOf(u16RecLen);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u16RecLen = GetBytesValToU16(bytes);

            fromOf += lenSize;
            lenSize = Marshal.SizeOf(u32RecNum);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u32RecNum = GetBytesValToUint(bytes);
        }
        uint GetBytesValToUint(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }
        ushort GetBytesValToU16(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt16(OxbytesToString(bytes), 16);
        }
        string OxbytesToString(byte[] bytes)
        {
            string hexString = string.Empty;
            Array.Reverse((byte[])bytes);
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        byte[] GetBytesValueToParmBytes(byte[] bytes)
        {
            byte[] re = new byte[] { };
            Array.Reverse((byte[])bytes);
            //Buffer.BlockCopy((byte[])bytes, 0, (byte[])re, 0, ((byte[])bytes).Length);
            return bytes;
        }
        byte[] GetBytesValueToParmBytes2(byte[] bytes)
        {
            byte[] re = new byte[] { };
            ////var pos = bytes.DefaultIfEmpty();
            //for (int i = bytes.Length; i > 0; i--)
            //{
            //    if (bytes[i] == '0')
            //        bytes[i] = '/0';
            //}
            //Array.Reverse((byte[])bytes);
            //Buffer.BlockCopy((byte[])bytes, 0, (byte[])re, 0, ((byte[])bytes).Length);
            return bytes;
        }
        bool isEmptyByte(byte val)
        {
            if (val != '0')
                return true;
            return false;
        }
    }

    /// <summary>
    /// OID的结构定义
    /// </summary>
    struct OM_OBJ_ID_T
    {
        int num_components;                               /*OID的个数                      */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        int[] component_list;                             /*[30:OID的最大长度] 存放该OID的数组                */
        /*********************        功能函数(结构转byte序等)      ***************************/
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="s"></param>
        public void int_OM_OBJ_ID_T(string s="init")
        {
            num_components = 0;
            int OM_OID_MAXLENTH = 30;             /*OID的最大长度                  */
            component_list = new int[OM_OID_MAXLENTH];
        }
        public OM_OBJ_ID_T(string inputStr)
        {
            num_components = 0;
            int OM_OID_MAXLENTH = 30;             /*OID的最大长度                  */
            component_list = new int[OM_OID_MAXLENTH];

            string StrTmp;
            string csInputTmp = inputStr;
            /*
            *	循环截取“.”相隔的每部分数据，存放在OID结构内的数据数组中
            */
            int index = 0;
            while ((index = csInputTmp.IndexOf(".")) > 0)
            {
                //截取单节数据
                StrTmp = csInputTmp.Substring(0, index);// csInputTmp.Mid(0, index);
                //存放在OID结构中
                component_list[num_components] = int.Parse(StrTmp);// atoi(StrTmp);
                //元素个数加一
                num_components++;//newOID.num_components = newOID.num_components + 1;

                csInputTmp = csInputTmp.Substring(index + 1);
            }

            //单值部分处理
            component_list[num_components] = int.Parse(csInputTmp);//newOID.component_list[newOID.num_components] = atoi(csInputTmp);
            num_components++;//newOID.num_components = newOID.num_components + 1;
        }
        /// <summary>
        /// 把 Struct DataHead 中的参数转化成byte[]串
        /// </summary>
        /// <returns></returns>
        public byte[] StruToByteArray()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new OM_OBJ_ID_T())];// DataHead 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, (ushort)num_components);
            SetValueToByteArray(byteAL, bytePosL, component_list);
            return byteAL[0];
        }
        /*********************        功能函数(私有)             ***************************/
        /// <summary>
        /// 把 各种type参数 转化成字节串byte[]
        /// </summary>
        /// <param name="byteAL">目的字节串的列表</param>
        /// <param name="bytePosL">目的字节串保存的位置</param>
        /// <param name="objParm">要保存的参数</param>
        private void SetValueToByteArray(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else if (objParm is int[])
            {
                foreach (var ui in (int[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((int)ui); //  数据块起始位置 
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    struct OM_STRU_VALUE
    {
        public string valType;
        public string value;
        public long strValLen;
        public long TableDimen;  //该节点对应的表是几维的
        public string asnType;
        public string ValAllList;//取值范围
        public string Mib_Syntax;
        public string leafName;
        public int typeSize;
        public OM_STRU_VALUE(DataRow leafRow, int u16FieldLen, long longGetMaxStrLen)
        {
            typeSize = u16FieldLen;
            valType = leafRow["OMType"].ToString();
            value = leafRow["DefaultValue"].ToString();
            strValLen = longGetMaxStrLen;
            asnType = leafRow["ASNType"].ToString(); 
            ValAllList = leafRow["MIBVal_AllList"].ToString();
            Mib_Syntax = leafRow["MIB_Syntax"].ToString();
            leafName = leafRow["MIBName"].ToString();
            TableDimen = 0;//tableIndexNum;//索引的个数 
        }
        public void SetTableDimen(long tableIndexNum){
            TableDimen = tableIndexNum;//tableIndexNum;//索引的个数 
        }
    }

    /// <summary>
    /// 告警相关结构体题
    /// </summary>
    struct StruAlarmInfo
    {
        public string alarmCauseNo;
        public string alarmCauseRowStatus;
        public string alarmCauseSeverity;
        public string alarmCauseIsValid;
        public string alarmCauseType;//5
        public string alarmCauseClearStyle;
        public string alarmCauseToAlarmBox;
        public string alarmCauseItfNProtocolCauseNo;
        public string alarmCauseIsStateful;
        public string alarmCausePrimaryAlarmCauseNo;//10
        public string alarmCauseStatefulClearDeditheringInterval;
        public string alarmCauseStatefulCreateDeditheringInterval;
        public string alarmCauseStatefulDelayTime;
        public string alarmCauseCompressionInterval;
        public string alarmCauseCompressionRepetitions;//15
        public string alarmCauseAutoProcessPolicy;
        public string alarmCauseValueStyle;
        public string alarmCauseFaultObjectType;
        public string alarmCauseReportBoardType;//19

        //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
        public string strAlarmUnstableDispose;
        public string strAlarmCauseInsecureNo;  //不稳定态告警编号

        public StruAlarmInfo(DataRow alarmRow)
        {
            //告警编号		
            alarmCauseNo = alarmRow[("AlaNumber")].ToString();
            alarmCauseSeverity = alarmRow[("AlaDegree")].ToString(); //告警级别
            alarmCauseRowStatus = "4";  //告警行状态

            // "5037" sStrValueYes
            if (String.Equals("是", alarmRow[("IsReportToOMCR")].ToString()))//strAlarmCauseValid == sStrValueYes)
            {
                alarmCauseIsValid = "0";
                alarmCauseToAlarmBox = "1";
            }
            // "5038" sStrValueNO
            else if (String.Equals("否", alarmRow[("IsReportToOMCR")].ToString()))
            {
                alarmCauseIsValid = "1";
                alarmCauseToAlarmBox = "0";
            }
            else
            {
                alarmCauseIsValid = "0";
                alarmCauseToAlarmBox = "1";
            }
            string TmpValue = alarmRow[("AlaType")].ToString();
            alarmCauseType = TmpValue.Substring(0, TmpValue.IndexOf(" -"));
            alarmCauseClearStyle = alarmRow[("ClearStyle")].ToString();
            alarmCauseItfNProtocolCauseNo = alarmRow[("ItfNProtocolCauseNo")].ToString();
            //string strAlarmStateFul = alarmRow[("IsFault")].ToString();
            if (String.Equals("是", alarmRow[("IsFault")].ToString()))//(strAlarmStateFul == sStrValueYes)
            {
                alarmCauseIsStateful = "0";
            }
            else
            {
                alarmCauseIsStateful = "1";
            }
            alarmCausePrimaryAlarmCauseNo = alarmRow[("AlaSubtoPrimaryNumber")].ToString();
            alarmCauseStatefulClearDeditheringInterval = alarmRow[("ClearDeditheringInterval")].ToString();
            alarmCauseStatefulCreateDeditheringInterval = alarmRow[("CreateDeditheringInterval")].ToString();
            alarmCauseCompressionInterval = alarmRow[("CompressionInterval")].ToString();
            alarmCauseCompressionRepetitions = alarmRow[("CompressionRepetitions")].ToString();
            alarmCauseValueStyle = alarmRow[("ValueStyle")].ToString();
            alarmCauseFaultObjectType = alarmRow[("FathernameOfObject")].ToString();

            //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
            strAlarmUnstableDispose = alarmRow[("AlaUnstableDispose")].ToString();
            strAlarmCauseInsecureNo = alarmRow[("UnstableAlaNum")].ToString();  //不稳定态告警编号

            alarmCauseStatefulDelayTime = "0";
            alarmCauseAutoProcessPolicy = "0";
            alarmCauseReportBoardType = "0";
        }
        public string GetContrastValue(string ParaName)
        {
            string ReturnValue = "";
            if (ParaName.Contains("alarmCauseNo"))
                ReturnValue = alarmCauseNo;
            else if (ParaName.Contains("alarmCauseSeverity"))
                ReturnValue = alarmCauseSeverity;
            else if (ParaName.Contains("alarmCauseRowStatus"))
                ReturnValue = alarmCauseRowStatus;
            else if (ParaName.Contains("alarmCauseType"))
                ReturnValue = alarmCauseType;
            else if (ParaName.Contains("alarmCauseIsValid"))
                ReturnValue = alarmCauseIsValid;
            else if (ParaName.Contains("alarmCauseToAlarmBox"))
                ReturnValue = alarmCauseToAlarmBox;
            else if (ParaName.Contains("alarmCausePrimaryAlarmCauseNo"))
                ReturnValue = alarmCausePrimaryAlarmCauseNo;
            else if (ParaName.Contains("alarmCauseClearStyle"))
                ReturnValue = alarmCauseClearStyle;
            else if (ParaName.Contains("alarmCauseItfNProtocolCauseNo"))
                ReturnValue = alarmCauseItfNProtocolCauseNo;
            else if (ParaName.Contains("alarmCauseIsStateful"))
                ReturnValue = alarmCauseIsStateful;
            else if (ParaName.Contains("alarmCausePrimaryAlarmCauseNo"))
                ReturnValue = alarmCausePrimaryAlarmCauseNo;
            else if (ParaName.Contains("alarmCauseStatefulClearDeditheringInterval"))
                ReturnValue = alarmCauseStatefulClearDeditheringInterval;
            else if (ParaName.Contains("alarmCauseStatefulCreateDeditheringInterval"))
            {
                ReturnValue = alarmCauseStatefulCreateDeditheringInterval;
            }
            else if (ParaName.Contains("alarmCauseStatefulDelayTime"))
            {
                ReturnValue = alarmCauseStatefulDelayTime;
            }
            else if (ParaName.Contains("alarmCauseCompressionInterval"))
            {
                ReturnValue = alarmCauseCompressionInterval;
            }
            else if (ParaName.Contains("alarmCauseCompressionRepetitions"))
            {
                ReturnValue = alarmCauseCompressionRepetitions;
            }
            else if (ParaName.Contains("alarmCauseValueStyle"))
            {
                ReturnValue = alarmCauseValueStyle;
            }
            else if (ParaName.Contains("alarmCauseFaultObjectType"))
            {
                ReturnValue = alarmCauseFaultObjectType;//故障源对象类型
            }
            else if (ParaName.Contains("alarmCauseReportBoardType"))
            {
                ReturnValue = alarmCauseReportBoardType;
            }
            //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
            else if (ParaName.Contains("alarmCauseProcessFlag"))
            {
                ReturnValue = strAlarmUnstableDispose;
            }
            else if (ParaName.Contains("alarmCauseInsecureAlarmCauseNo"))  //不稳定态告警编号
            {
                ReturnValue = strAlarmCauseInsecureNo;
            }
            return ReturnValue;
        }
        /// <summary>
        /// 解析excel解析
        /// </summary>
        /// <param name="wks"></param>
        public StruAlarmInfo(Dictionary<string, string> alarmExVal)
        {
            //if (wks == null)
            //    return null;
            //告警编号		
            alarmCauseNo = alarmExVal[("AlaNumber")].ToString();
            
            string strAlaDegree = alarmExVal[("AlaDegree")].ToString(); //告警级别
            if (-1 != strAlaDegree.IndexOf("."))
                alarmCauseSeverity = strAlaDegree.Substring(0, strAlaDegree.IndexOf("."));
            else
                alarmCauseSeverity = strAlaDegree; //告警级别
            alarmCauseRowStatus = "4";  //告警行状态

            // "5037" sStrValueYes
            if (String.Equals("是", alarmExVal[("IsReportToOMCR")].ToString()))//strAlarmCauseValid == sStrValueYes)
            {
                alarmCauseIsValid = "0";
                alarmCauseToAlarmBox = "1";
            }
            // "5038" sStrValueNO
            else if (String.Equals("否", alarmExVal[("IsReportToOMCR")].ToString()))
            {
                alarmCauseIsValid = "1";
                alarmCauseToAlarmBox = "0";
            }
            else
            {
                alarmCauseIsValid = "0";
                alarmCauseToAlarmBox = "1";
            }

            string TmpValue = alarmExVal[("AlaType")].ToString();
            int pos = TmpValue.IndexOf(" -")==-1?TmpValue.Length:TmpValue.IndexOf(" -");
            alarmCauseType = TmpValue.Substring(0, pos);
            string ClearStyle = alarmExVal[("ClearStyle")].ToString();
            if (String.Equals("无", ClearStyle) )
                alarmCauseClearStyle = "";
            else if (string.Empty == ClearStyle.Replace(" ", ""))
                alarmCauseClearStyle = "";// 255.ToString();
            else if(String.Equals("恢复后主动清除", ClearStyle))
                alarmCauseClearStyle = ""; //1.ToString();
            else
            {
                alarmCauseClearStyle = alarmExVal[("ClearStyle")].ToString().Replace(" ", "");
            }
            //alarmCauseClearStyle = alarmExVal[("ClearStyle")].ToString();
            alarmCauseItfNProtocolCauseNo = alarmExVal[("ItfNProtocolCauseNo")].ToString();
            //string strAlarmStateFul = alarmRow[("IsFault")].ToString();
            if (String.Equals("Y", alarmExVal[("IsFault")].ToString()))//(strAlarmStateFul == sStrValueYes)
            {
                alarmCauseIsStateful = "0";
            }
            else
            {
                alarmCauseIsStateful = "1";
            }
            alarmCausePrimaryAlarmCauseNo = alarmExVal[("AlaSubtoPrimaryNumber")].ToString();
            string ClearDeditheringInterval = alarmExVal[("ClearDeditheringInterval")].ToString();
            if (String.Equals(ClearDeditheringInterval, "×"))
                alarmCauseStatefulClearDeditheringInterval = "";
            else
                alarmCauseStatefulClearDeditheringInterval = ClearDeditheringInterval;
            alarmCauseStatefulCreateDeditheringInterval = alarmExVal[("CreateDeditheringInterval")].ToString();
            alarmCauseCompressionInterval = alarmExVal[("CompressionInterval")].ToString();
            alarmCauseCompressionRepetitions = alarmExVal[("CompressionRepetitions")].ToString();
            alarmCauseValueStyle = alarmExVal[("ValueStyle")].ToString();

            Dictionary<string, string> FaultObjType = new Dictionary<string, string>() {
                { "0","all|所有对象都包含"},
                {"1","nodeBEntry|基站"},
                { "2", "boardEntry|板卡" },
                { "3", "processorEntry|处理器" },
                { "4", "fanEntry|风扇" },
                { "5", "envEntry|环境监控" },
                { "6", "oabEntry|OAB" },
                { "7", "dryContactEntry|BBU干接点" },
                { "8", "airConditionerEntry|空调" },
                { "9", "rruEntry|RRU" },
                { "10", "rcuEntry|电调天线单元" },
                { "11", "cellEntry|LTE小区" },
                { "12", "localcellEntry|LTE本地小区" },
                { "13", "antArrayEntry|天线阵" },
                { "14", "antPathEntry|天线通道" },
                { "15", "clockEntry|时钟" },
                { "16", "emTemperatureSensiorEntry|温度传感器" },
                { "17", "emHumiditySensiorEntry|湿度传感器" },
                { "18", "emSmokeSensiorEntry|烟雾传感器" },
                { "19", "emWaterSensiorEntry|水浸传感器" },
                { "20", "emTheftSensiorEntry|门禁传感器" },
                { "21", "emThunderboltSensiorEntry|雷击传感器" },
                { "22", "powerEntry|电源" },
                { "23", "netBoardEntry|规划板卡" },
                { "24", "netRruEntry|规划RRU" },
                { "25", "ofPortEntry|光模块" },
                { "26", "sctpEntry|sctp" },
                { "27", "topoRRUAntSettingEntry|射频通道" },
                { "28", "heatExEntry|热交换器" },
                { "29", "processorCoreEntry|处理器核" },
                { "30", "ethernetOAMEntry|以太OAM链路" },
                { "31", "remoteClockModuleEntry|拉远时钟" },
                { "32", "tdsLocalCellEntry|TD-SCDMA本地小区" },
                { "33", "rruOfpPortEntry|RRU光模块" },
                { "34", "netRHUBEntry|规划RHUB" },
                { "35", "topologyRHUBEntry|RHUB" },
                { "36", "netRRURootAlarmEntry|RRU干接点" }
            };
            string FathernameOfObject = alarmExVal[("FathernameOfObject")].ToString();//z
            if (String.Empty == FathernameOfObject || String.Equals("无",FathernameOfObject))
            {
                alarmCauseFaultObjectType = "";
            }
            else
            {
                int findPos = FathernameOfObject.IndexOf(":");
                if (findPos != -1)
                {
                    string key = FathernameOfObject.Substring(0, findPos);
                    alarmCauseFaultObjectType = key + ":" + FaultObjType[key];
                }
                else
                    alarmCauseFaultObjectType = "";
            }

            //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
            strAlarmUnstableDispose = alarmExVal[("AlaUnstableDispose")].ToString();
            string UnstableAlaNum = alarmExVal[("UnstableAlaNum")].ToString();//不稳定态告警编号
            if (string.Empty == UnstableAlaNum)
            {
                strAlarmCauseInsecureNo = 0.ToString();
            }
            else
                strAlarmCauseInsecureNo = alarmExVal[("UnstableAlaNum")].ToString();  //不稳定态告警编号

            alarmCauseStatefulDelayTime = "0";
            alarmCauseAutoProcessPolicy = "0";
            alarmCauseReportBoardType = "0";

        }

        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RRuInfo"></param>
    /// <param name="nodeNameEn"></param>
    /// <returns></returns>
    public delegate string GetRruTypeByNodeNameEn(Dictionary<string, string> RRuInfo, string nodeNameEn);
    /// <summary>
    /// 
    /// </summary>
    struct RRuTypeTabStru
    {
        public RRuTypeTabStru(string s)
        {
            rruTypeManufacturerIndex = "0";
            rruTypeIndex = "0";
            rruTypeRowStatus = "6";
            rruTypeName = "0";
            rruTypeMaxAntPathNum = "0";
            rruTypeMaxTxPower = "0";
            rruTypeBandWidth = "0";
            rruTypeSupportCellWorkMode = "0";
            //2014-2-27 luoxin RRUType新增节点
            strRruTypeFiberLength = "0";
            strRruTypeIrCompressMode = "0";
            //2016-08-29 guoyingjie add  rruTypeFamilyName
            strRruTypeFamilyName = "0";
            excelRead = null;
        }
        /// <summary>
        /// 处理mdb数据的数据
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="rruTypedateSet"></param>
        public RRuTypeTabStru(DataRow Row, DataSet rruTypedateSet)
        {
            excelRead = null;
            //RRU生产厂家索引
            rruTypeManufacturerIndex = Row["rruTypeManufacturerIndex"].ToString();
            //RRU设备类型索引
            rruTypeIndex = Row[("rruTypeIndex")].ToString();
            //RRU类型名称
            rruTypeName = Row[("rruTypeName")].ToString();
            //RRU支持的天线数
            rruTypeMaxAntPathNum = Row[("rruTypeMaxAntPathNum")].ToString();
            //RRU通道最大发射功率
            rruTypeMaxTxPower = Row[("rruTypeMaxTxPower")].ToString();
            //RRU支持的频带宽度
            rruTypeBandWidth = Row[("rruTypeBandWidth")].ToString();
            //RRU支持的小区工作模式
            rruTypeSupportCellWorkMode = Row[("rruTypeSupportCellWorkMode")].ToString();
            //行状态
            rruTypeRowStatus = "4";

            //2014-2-27 luoxin RRUType新增节点
            strRruTypeFiberLength = Row[("rruTypeFiberLength")].ToString();
            strRruTypeIrCompressMode = Row[("rruTypeIrCompressMode")].ToString();

            //2016-08-29 guoyingjie add  rruTypeFamilyName
            if (rruTypedateSet.Tables[0].Columns.Contains("rruTypeFamilyName"))//判断是否有这一列
                strRruTypeFamilyName = Row[("rruTypeFamilyName")].ToString();
            else
                strRruTypeFamilyName = "";
        }

        public void RRuTypeTabStruInit(Dictionary<string, string> RRuInfo)
        {
            //excelRead = GetNodeInfoByNameEn;
            //RRU生产厂家索引
            rruTypeManufacturerIndex = excelRead(RRuInfo, "rruTypeManufacturerIndex");
            //RRU设备类型索引
            rruTypeIndex = excelRead(RRuInfo, "rruTypeIndex");
            rruTypeName = excelRead(RRuInfo, "rruTypeName");
            //RRU支持的天线数
            rruTypeMaxAntPathNum = excelRead(RRuInfo, "rruTypeMaxAntPathNum");
            //RRU通道最大发射功率
            rruTypeMaxTxPower = excelRead(RRuInfo, "rruTypeMaxTxPower");
            //RRU支持的频带宽度
            rruTypeBandWidth = excelRead(RRuInfo, "rruTypeBandWidth");
            //RRU支持的小区工作模式
            string strRruTypeSupportCellWorkMode = excelRead(RRuInfo, "rruTypeSupportCellWorkMode");
            int pos = 0;
            Dictionary<string, string> CellWorkMode = new Dictionary<string, string>() {
                { "LTE","LTE TDD"},{ "TD","TD-SCDMA"},{ "NR","NR"}, { "FDD","LTE FDD"}
            };
            while (true)
            {
                pos = strRruTypeSupportCellWorkMode.IndexOf("/");
                if (-1 != pos) // LTE/TD
                {
                    string proStr = strRruTypeSupportCellWorkMode.Substring(0, pos);
                    rruTypeSupportCellWorkMode += CellWorkMode[proStr] + "/";
                    strRruTypeSupportCellWorkMode = strRruTypeSupportCellWorkMode.Substring(pos + 1, strRruTypeSupportCellWorkMode.Length - pos - 1);
                }
                else
                {
                    string proStr = strRruTypeSupportCellWorkMode;
                    rruTypeSupportCellWorkMode += CellWorkMode[proStr] ;
                    break;
                }

            }
            if (String.Empty == rruTypeSupportCellWorkMode)
                rruTypeSupportCellWorkMode = "";
            //rruTypeSupportCellWorkMode = excelRead(RRuInfo, "rruTypeSupportCellWorkMode");
            //行状态
            rruTypeRowStatus = "4";
            //2014-2-27 luoxin RRUType新增节点
            strRruTypeFiberLength = excelRead(RRuInfo, "rruTypeFiberLength");// new RRuTypeTabStru().GetExcelRruInfoRruTypeFiberLength(excelRead(RRuInfo, "rruTypeZoomProperty"));
            strRruTypeIrCompressMode = excelRead(RRuInfo, "rruTypeCompressionProperty");//rruTypeCompressionProperty

            //2016-08-29 guoyingjie add  rruTypeFamilyName
            strRruTypeFamilyName = excelRead(RRuInfo, "rruTypeFamilyName");
        }
        
        public GetRruTypeByNodeNameEn excelRead;
        public string rruTypeManufacturerIndex;
        public string rruTypeIndex;
        public string rruTypeRowStatus;
        public string rruTypeName;
        public string rruTypeMaxAntPathNum;
        public string rruTypeMaxTxPower;
        public string rruTypeBandWidth;
        public string rruTypeSupportCellWorkMode;
        public string strRruTypeFiberLength;//2014-2-27 luoxin RRUType新增节点
        public string strRruTypeIrCompressMode;//2014-2-27 luoxin RRUType新增节点
        public string strRruTypeFamilyName;//2016-08-29 guoyingjie add  rruTypeFamilyName

        public string  GetRRuLeafValue(string FieldName)
        {
            string ReturnValue = "";
            if (FieldName.Contains("rruTypeManufacturerIndex"))
            {
                ReturnValue = rruTypeManufacturerIndex;
            }
            else if (FieldName.Contains("rruTypeIndex"))
            {
                ReturnValue = rruTypeIndex;
            }
            else if (FieldName.Contains("rruTypeName"))
            {
                ReturnValue = rruTypeName;
            }
            else if (FieldName.Contains("rruTypeMaxAntPathNum"))
            {
                ReturnValue = rruTypeMaxAntPathNum;
            }
            else if (FieldName.Contains("rruTypeMaxTxPower"))
            {
                ReturnValue = rruTypeMaxTxPower;
            }
            else if (FieldName.Contains("rruTypeBandWidth"))
            {
                ReturnValue = rruTypeBandWidth;
            }
            else if (FieldName.Contains("rruTypeSupportCellWorkMode"))
            {
                ReturnValue = rruTypeSupportCellWorkMode;
            }
            else if (FieldName.Contains("rruTypeRowStatus"))
            {
                ReturnValue = rruTypeRowStatus;
            }
            //2014-2-27 luoxin RRUType新增节点
            else if (0==String.Compare("rruTypeFiberLength", FieldName, true))
            {
                ReturnValue = strRruTypeFiberLength;
            }
            else if (0 == String.Compare("rruTypeIrCompressMode", FieldName, true))
            {
                ReturnValue = strRruTypeIrCompressMode;
            }
            //add by guoyingjie 2016-08-29 add rruTypeFamilyName
            else if (0 == String.Compare("rruTypeFamilyName", FieldName, true))
            {
                ReturnValue = strRruTypeFamilyName;
            }

            return ReturnValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RRuInfo"></param>
    /// <param name="nodeNameEn"></param>
    /// <returns></returns>
    public delegate string GetRruTypePortByNodeNameEn(Dictionary<string, string> RRuInfo, string nodeNameEn);
    /// <summary>
    /// 
    /// </summary>
    struct RRuTypePortTabStru
    {
        public string rruTypePortManufacturerIndex;
        public string rruTypePortIndex;
        public string rruTypePortNo;
        public string rruTypePortRowStatus;
        public string rruTypePortSupportFreqBand;
        public string rruTypePortSupportFreqBandWidth;
        public string rruTypePortPathNo;

        //2013-04-10 luoxin DTMUC00153813
        public string rruTypePortSupportAbandTdsCarrierNum;
        public string rruTypePortSupportFBandTdsCarrierNum;
        public string rruTypePortCalAIqRxNom;
        public string rruTypePortCalAIqTxNom;
        public string rruTypePortCalPinRxNom;
        public string rruTypePortCalPoutTxNom;
        //2013-04-10 luoxin end 

        //2014-3-5 luoxin RRU通道类型表增加新节点
        public string strRruTypePortAntMaxPower;

        public GetRruTypePortByNodeNameEn excelRead;

        /// <summary>
        /// 查询数据库mdb时，使用的初始化
        /// </summary>
        /// <param name="Row"></param>
        public RRuTypePortTabStru(DataRow Row)
        {
            excelRead = null;
            //RRU生产厂家索引
            rruTypePortManufacturerIndex = Row["rruTypeManufacturerIndex"].ToString(); 
            //RRU设备类型索引
            rruTypePortIndex = Row["rruTypeIndex"].ToString(); 
            //远端射频单元上端口编号
            rruTypePortNo = Row["rruTypePortNo"].ToString(); 
            //天线通道支持频段
            rruTypePortSupportFreqBand = Row["rruTypePortSupportFreqBand"].ToString(); 
            //天线通道支持频段宽度 
            rruTypePortSupportFreqBandWidth = Row["rruTypePortSupportFreqBandWidth"].ToString(); 
            //通道天线编号
            rruTypePortPathNo = Row["rruTypePortPathNo"].ToString(); ;
            //行状态
            rruTypePortRowStatus = "4";

            //2013-04-10 luoxin DTMUC00153813
            rruTypePortCalAIqRxNom = Row["rruTypePortCalAIqRxNom"].ToString(); 
            rruTypePortCalAIqTxNom = Row["rruTypePortCalAIqTxNom"].ToString(); 
            rruTypePortCalPinRxNom = Row["rruTypePortCalPinRxNom"].ToString(); 
            rruTypePortCalPoutTxNom = Row["rruTypePortCalPoutTxNom"].ToString();
            strRruTypePortAntMaxPower = Row["rruTypePortAntMaxPower"].ToString();
            //根据频段获取载波数（目前只支持A频段和F频段）
            rruTypePortSupportAbandTdsCarrierNum = "0";
            rruTypePortSupportFBandTdsCarrierNum = "0";
            // tds 相关内容不在支持
            GetCarrierNumByFreqBand(Row);// 填写 rruTypePortSupportAbandTdsCarrierNum 、 rruTypePortSupportFBandTdsCarrierNum
            //vectRRuTypePort.push_back(pRRuTypePort);
        }
        /// <summary>
        /// 填写 rruTypePortSupportAbandTdsCarrierNum 、 rruTypePortSupportFBandTdsCarrierNum
        /// </summary>
        /// <param name="Row"></param>
        void GetCarrierNumByFreqBand(DataRow Row)
        {
            string strFBand = "";
            string strABand = "";
            string strBand = "";
            string str_FreqBand = Row["rruTypePortSupportFreqBand"].ToString();
            string str_CarrierNum = Row["rruTypeFrequencyRangCarrier"].ToString();
            string str_ACarrierNum = "0";
            string str_FCarrierNum = "0";

            int iPos = str_FreqBand.IndexOf("/");
            if (iPos > 0)
            {
                //左截取：str.Substring(0, i) 返回，返回左边的i个字符
                //右截取：str.Substring(str.Length - i, i) 返回，返回右边的i个字符
                strFBand = str_FreqBand.Substring(iPos);
                strABand = str_FreqBand.Substring(str_FreqBand.Length - iPos - 1, iPos + 1);//Right(str_FreqBand.GetLength() - iPos - 1);

                strFBand = strFBand.Substring(strFBand.IndexOf("("));//Left(strFBand.Find("("));
                strABand = strABand.Substring(strABand.IndexOf("("));// Left(strABand.Find("("));

                if (0 == String.Compare("F频段", strFBand, true))
                {
                    str_FCarrierNum = str_CarrierNum.Substring(str_CarrierNum.IndexOf("/")); ;// str_CarrierNum.Left(str_CarrierNum.Find("/"));
                }
                if (0 == String.Compare("A频段", strABand, true))
                {
                    str_ACarrierNum = str_CarrierNum.Substring(str_CarrierNum.Length - str_CarrierNum.IndexOf("/") - 1, str_CarrierNum.IndexOf("/") + 1);// str_CarrierNum.Right(str_CarrierNum.GetLength() - str_CarrierNum.Find("/") - 1);
                }
            }
            else
            {
                strBand = str_FreqBand.Substring(str_FreqBand.IndexOf("("));

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

        public string GetRRuTypePortValue(string FieldName)
        {
            string ReturnValue = "";
            //2012-06-27 luoxin Find用于发现子字符串 应该换做Compare from zyj 
            //if (FieldName.Find("rruTypePortManufacturerIndex")>=0)
            if (FieldName.Contains("rruTypePortManufacturerIndex"))
            {
                ReturnValue = rruTypePortManufacturerIndex;
            }
            //else if (FieldName.Find("rruTypePortIndex")>=0)
            else if (FieldName.Contains("rruTypePortIndex"))
            {
                ReturnValue = rruTypePortIndex;
            }
            //else if (FieldName.Find("rruTypePortNo")>=0)
            else if (FieldName.Contains("rruTypePortNo"))
            {
                ReturnValue = rruTypePortNo;
            }
            //else if (FieldName.Find("rruTypePortSupportFreqBandWidth")>=0)
            else if (FieldName.Contains("rruTypePortSupportFreqBandWidth"))
            {
                ReturnValue = rruTypePortSupportFreqBandWidth;
            }
            //else if (FieldName.Find("rruTypePortSupportFreqBand")>=0)
            else if (FieldName.Contains("rruTypePortSupportFreqBand"))
            {
                ReturnValue = rruTypePortSupportFreqBand;
            }
            //else if (FieldName.Find("rruTypePortPathNo")>=0)
            else if (FieldName.Contains("rruTypePortPathNo"))
            {
                ReturnValue = rruTypePortPathNo;
            }
            //else if (FieldName.Find("rruTypePortRowStatus")>=0)
            else if (FieldName.Contains("rruTypePortRowStatus"))
            {
                ReturnValue = rruTypePortRowStatus;
            }
            //2012-06-27 luoxin Find用于发现子字符串 应该换做Compare from zyj end
            //2013-04-10 luoxin DTMUC00153813
            else if (FieldName.Contains("rruTypePortCalAIqRxNom"))
            {
                ReturnValue = rruTypePortCalAIqRxNom;
            }
            else if (FieldName.Contains("rruTypePortCalAIqTxNom"))
            {
                ReturnValue = rruTypePortCalAIqTxNom;
            }
            else if (FieldName.Contains("rruTypePortCalPinRxNom"))
            {
                ReturnValue = rruTypePortCalPinRxNom;
            }
            else if (FieldName.Contains("rruTypePortCalPoutTxNom"))
            {
                ReturnValue = rruTypePortCalPoutTxNom;
            }
            // tds
            else if (FieldName.Contains("rruTypePortSupportAbandTdsCarrierNum"))
            {
                ReturnValue = rruTypePortSupportAbandTdsCarrierNum;
            }
            else if (FieldName.Contains("rruTypePortSupportFBandTdsCarrierNum"))
            {
                ReturnValue = rruTypePortSupportFBandTdsCarrierNum;
            }
            //2013-04-10 luoxin end

            //2014-3-5 luoxin RRU通道类型表增加新节点
            else if (0 == String.Compare("rruTypePortAntMaxPower", FieldName, true))//(FieldName.CompareNoCase("rruTypePortAntMaxPower") == 0)
            {
                ReturnValue = strRruTypePortAntMaxPower;
            }

            return ReturnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RRuInfo"></param>
        public void RRuTypePortTabStruInit(Dictionary<string, string> RRuInfo)
        {
            //RRU生产厂家索引
            rruTypePortManufacturerIndex = excelRead(RRuInfo, "rruTypeManufacturerIndex");
            //RRU设备类型索引
            rruTypePortIndex = excelRead(RRuInfo, "rruTypeIndex");
            //远端射频单元上端口编号
            rruTypePortNo = excelRead(RRuInfo, "rruTypePortNo");
            //天线通道支持频段
            rruTypePortSupportFreqBand = excelRead(RRuInfo, "rruTypePortSupportFreqBand");
            //天线通道支持频段宽度 
            rruTypePortSupportFreqBandWidth = excelRead(RRuInfo, "rruTypePortSupportFreqBandWidth");
            //通道天线编号
            rruTypePortPathNo = excelRead(RRuInfo, "rruTypePortPathNo"); ;
            //行状态
            rruTypePortRowStatus = "4";

            //2013-04-10 luoxin DTMUC00153813
            rruTypePortCalAIqRxNom = excelRead(RRuInfo, "rruTypePortCalAIqRxNom");
            rruTypePortCalAIqTxNom = excelRead(RRuInfo, "rruTypePortCalAIqTxNom");
            rruTypePortCalPinRxNom = excelRead(RRuInfo, "rruTypePortCalPinRxNom");
            rruTypePortCalPoutTxNom = excelRead(RRuInfo, "rruTypePortCalPoutTxNom");
            strRruTypePortAntMaxPower = excelRead(RRuInfo, "rruTypePortAntMaxPower");
            //Tds不在支持  根据频段获取载波数（目前只支持A频段和F频段）

            //根据频段获取载波数（目前只支持A频段和F频段）
            rruTypePortSupportAbandTdsCarrierNum = excelRead(RRuInfo, "rruTypePortSupportAbandTdsCarrierNum"); 
            rruTypePortSupportFBandTdsCarrierNum = excelRead(RRuInfo, "rruTypePortSupportFbandTdsCarrierNum"); 
        }
    }

    struct AntArrayBfScanAntWeightTabStru
    {
        //public AntArrayBfScanAntWeightTabStru(string s)
        //{
        //    antArrayBfScanAntWeightVendorIndex = "0";
        //    antArrayBfScanAntWeightTypeIndex = "0";
        //    antArrayBfScanAntWeightIndex = "0";
        //    antArrayBfScanAntWeightBFScanGrpNo = "0";
        //    antArrayBfScanAntWeightAntGrpNo = "0";
        //    antArrayBfScanAntWeightRowStatus = "6";
        //    antArrayBfScanAntWeightAmplitude0 = "0";
        //    antArrayBfScanAntWeightPhase0 = "0";
        //    antArrayBfScanAntWeightAmplitude1 = "0";
        //    antArrayBfScanAntWeightPhase1 = "0";
        //    antArrayBfScanAntWeightAmplitude2 = "0";
        //    antArrayBfScanAntWeightPhase2 = "0";
        //    antArrayBfScanAntWeightAmplitude3 = "0";
        //    antArrayBfScanAntWeightPhase3 = "0";
        //    antArrayBfScanAntWeightAmplitude4 = "0";
        //    antArrayBfScanAntWeightPhase4 = "0";
        //    antArrayBfScanAntWeightAmplitude5 = "0";
        //    antArrayBfScanAntWeightPhase5 = "0";
        //    antArrayBfScanAntWeightAmplitude6 = "0";
        //    antArrayBfScanAntWeightPhase6 = "0";
        //    antArrayBfScanAntWeightAmplitude7 = "0";
        //    antArrayBfScanAntWeightPhase7 = "0";
        //    antArrayBfScanAntWeightHorizonNum = "0";
        //    antArrayBfScanAntWeightVerticalNum = "0";
        //}
        /// <summary>
        /// 天线阵厂家索引
        /// </summary>
        public string antArrayBfScanAntWeightVendorIndex;
        /// <summary>
        /// 天线阵型号索引
        /// </summary>
        public string antArrayBfScanAntWeightTypeIndex;
        /// <summary>
        /// 天线阵索引
        /// </summary>
        public string antArrayBfScanAntWeightIndex;
        /// <summary>
        /// 波束扫描组号
        /// </summary>
        public string antArrayBfScanAntWeightBFScanGrpNo;
        /// <summary>
        /// 天线组号
        /// </summary>
        public string antArrayBfScanAntWeightAntGrpNo;
        /// <summary>
        /// 天线阵波束扫描天线权值行状态
        /// </summary>
        public string antArrayBfScanAntWeightRowStatus;
        /// <summary>
        /// 天线1幅度
        /// </summary>
        public string antArrayBfScanAntWeightAmplitude0;
        /// <summary>
        /// 天线1相位
        /// </summary>
        public string antArrayBfScanAntWeightPhase0;
        public string antArrayBfScanAntWeightAmplitude1;
        public string antArrayBfScanAntWeightPhase1;
        public string antArrayBfScanAntWeightAmplitude2;
        public string antArrayBfScanAntWeightPhase2;
        public string antArrayBfScanAntWeightAmplitude3;
        public string antArrayBfScanAntWeightPhase3;
        public string antArrayBfScanAntWeightAmplitude4;
        public string antArrayBfScanAntWeightPhase4;
        public string antArrayBfScanAntWeightAmplitude5;
        public string antArrayBfScanAntWeightPhase5;
        public string antArrayBfScanAntWeightAmplitude6;
        public string antArrayBfScanAntWeightPhase6;
        public string antArrayBfScanAntWeightAmplitude7;
        public string antArrayBfScanAntWeightPhase7;
        /// <summary>
        /// 水平波束个数
        /// </summary>
        public string antArrayBfScanAntWeightHorizonNum;
        /// <summary>
        /// 垂直波束个数
        /// </summary>
        public string antArrayBfScanAntWeightVerticalNum;
        /// <summary>
        /// 水平方向数字下倾角
        /// </summary>
        public string antArrayBfScanAntWeightHorizonDowntiltAngle;
        /// <summary>
        /// 垂直方向数字下倾角
        /// </summary>
        public string antArrayBfScanAntWeightVerticalDowntiltAngle;
        /// <summary>
        /// 有损无损
        /// </summary>
        public string antArrayBfScanWeightIsLossFlag;

        public string GetAntArrayBfScanLeafValue(string FieldName)
        {
            string ReturnValue = "";

            if (FieldName.Contains("antennaBfScanWeightVendorIndex"))
            {
                ReturnValue = antArrayBfScanAntWeightVendorIndex;
            }
            else if (FieldName.Contains("antennaBfScanWeightTypeIndex"))
            {
                ReturnValue = antArrayBfScanAntWeightTypeIndex;
            }
            else if (FieldName.Contains("antennaBfScanWeightIndex"))
            {
                ReturnValue = antArrayBfScanAntWeightIndex;
            }
            else if (FieldName.Contains("antennaBfScanWeightBFScanGrpNo"))
            {
                ReturnValue = antArrayBfScanAntWeightBFScanGrpNo;
            }
            else if (FieldName.Contains("antennaBfScanWeightAntGrpNo"))
            {
                ReturnValue = antArrayBfScanAntWeightAntGrpNo;
            }
            else if (FieldName.Contains("antennaBfScanWeightRowStatus"))
            {
                ReturnValue = antArrayBfScanAntWeightRowStatus;
            }
            //天线1幅度 1~8
            else if (FieldName.Contains("antennaBfScanWeightAmplitude0"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude0;
            }
            //天线1相位 1~8
            else if (FieldName.Contains("antennaBfScanWeightPhase0"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase0;
            }
            else if (FieldName.Contains("antennaBfScanWeightAmplitude1"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude1;
            }
            else if (FieldName.Contains("antennaBfScanWeightPhase1"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase1;
            }
            else if (FieldName.Contains("antennaBfScanWeightAmplitude2"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude2;
            }
            else if (FieldName.Contains("antennaBfScanWeightPhase2"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase2;
            }
            else if (FieldName.Contains("antennaBfScanWeightAmplitude3"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude3;
            }
            else if (FieldName.Contains("antennaBfScanWeightPhase3"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase3;
            }
            else if (FieldName.Contains("antennaBfScanWeightAmplitude4"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude4;
            }
            else if (FieldName.Contains("antennaBfScanWeightPhase4"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase4;
            }
            else if (FieldName.Contains("antennaBfScanWeightAmplitude5"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude5;
            }
            else if (FieldName.Contains("antennaBfScanWeightPhase5"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase5;
            }
            else if (FieldName.Contains("antennaBfScanWeightAmplitude6"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude6;
            }
            else if (FieldName.Contains("antennaBfScanWeightPhase6"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase6;
            }
            else if (FieldName.Contains("antennaBfScanWeightAmplitude7"))
            {
                ReturnValue = antArrayBfScanAntWeightAmplitude7;
            }
            else if (FieldName.Contains("antennaBfScanWeightPhase7"))
            {
                ReturnValue = antArrayBfScanAntWeightPhase7;
            }
            //水平波束个数
            else if (FieldName.Contains("antennaBfScanWeightHorizonNum"))
            {
                ReturnValue = antArrayBfScanAntWeightHorizonNum;
            }
            //垂直波束个数 
            else if (FieldName.Contains("antennaBfScanWeightVerticalNum"))
            {
                ReturnValue = antArrayBfScanAntWeightVerticalNum;
            }
            //支持上下倾角及是否有损坏
            else if (FieldName.Contains("antennaBfScanWeightHorizonDowntiltAngle"))
            {
                ReturnValue = antArrayBfScanAntWeightHorizonDowntiltAngle;
            }
            else if (FieldName.Contains("antennaBfScanWeightVerticalDowntiltAngle"))
            {
                ReturnValue = antArrayBfScanAntWeightVerticalDowntiltAngle;
            }
            else if (FieldName.Contains("antennaBfScanWeightIsLossFlag"))
            {
                ReturnValue = antArrayBfScanWeightIsLossFlag;
            }
            return ReturnValue;
        }
    }

}
