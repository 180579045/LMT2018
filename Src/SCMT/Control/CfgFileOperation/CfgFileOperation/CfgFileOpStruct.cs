using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Compression;// zip
using System.IO;// File
using System.Runtime.InteropServices;
using System.Data;

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
        /// <summary>
        /// u8MiDeviceType : TD/LTE/5G的文件
        /// </summary>
        /// <param name="typeValue"></param>
        public void Setu8MiDeviceType(byte typeValue)
        {
            u8MiDeviceType = typeValue;// new MacroDefinition().LTE_DEVICE;
        }
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
        /// <summary>
        /// strMibVersion="5.65.3.6" => u32PublicMibVer = 5;u32MainMibVer = 65;u32SubMibVer = 3;u32ReserveVer = 6;
        /// </summary>
        /// <param name="strMibVersion"></param>
        public void FillVerInfo(string strMibVersion)
        {
            //"5_65_3_6";
            u32PublicMibVer = 5;
            u32MainMibVer = 65;
            u32SubMibVer = 3;
            u32ReserveVer = 6;

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

            string u32PublicMibVer1 = vecMibVer[0];
            string u32MainMibVer1 = vecMibVer[1];
            string u32SubMibVer1 = vecMibVer[2];
            string u32ReserveVer1 = vecMibVer[3];
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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        byte[] u8VerifyStr;                              // [4] 文件头的校验字段 "BEG\0"
        public uint u32DatType;                          // reserved , =1 
        public uint u32DatVer;                           // reserved , =1 
        public uint u32TableCnt;                         // 表的数目,指示索引表中的向目个数

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
        /*********************        功能函数(变量赋值)             ***************************/
        public void Setu8VerifyStr(string str= "BEG\0")
        {
            if (u8VerifyStr == null)
                u8VerifyStr = new byte[4];
            StringToByteArray(str, u8VerifyStr);
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
        byte[] u8TblName;                      // [32] 表名
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
        /// <summary>
        /// 把 Struct DataHead 中的参数转化成byte[]串
        /// </summary>
        /// <returns></returns>
        public byte[] StruToByteArray()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileTblInfo())];// DataHead 转 byte[]
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
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileTblInfo())];// DataHead 转 byte[]
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


    struct StruAlarmInfo
    {
        string alarmCauseNo;
        string alarmCauseRowStatus;
        string alarmCauseSeverity;
        string alarmCauseIsValid;
        string alarmCauseType;
        string alarmCauseClearStyle;
        string alarmCauseToAlarmBox;
        string alarmCauseItfNProtocolCauseNo;
        string alarmCauseIsStateful;
        string alarmCausePrimaryAlarmCauseNo;
        string alarmCauseStatefulClearDeditheringInterval;
        string alarmCauseStatefulCreateDeditheringInterval;
        string alarmCauseStatefulDelayTime;
        string alarmCauseCompressionInterval;
        string alarmCauseCompressionRepetitions;
        string alarmCauseAutoProcessPolicy;
        string alarmCauseValueStyle;
        string alarmCauseFaultObjectType;
        string alarmCauseReportBoardType;

        //2013-06-27 luoxin 告警原因表新增一列“告警不稳定态处理方式”
        string strAlarmUnstableDispose;
        string strAlarmCauseInsecureNo;  //不稳定态告警编号

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
            else// (String.Equals("否", alarmRow[("IsReportToOMCR")].ToString()))
            {
                alarmCauseIsValid = "1";
                alarmCauseToAlarmBox = "0";
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
                ReturnValue = alarmCauseFaultObjectType;
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
            rruTypeSupportCellWorkMode = excelRead(RRuInfo, "rruTypeSupportCellWorkMode");
            //行状态
            rruTypeRowStatus = "4";
            //2014-2-27 luoxin RRUType新增节点
            strRruTypeFiberLength = excelRead(RRuInfo, "rruTypeFiberLength");// new RRuTypeTabStru().GetExcelRruInfoRruTypeFiberLength(excelRead(RRuInfo, "rruTypeZoomProperty"));
            strRruTypeIrCompressMode = excelRead(RRuInfo, "rruTypeCompressionProperty");//rruTypeCompressionProperty

            //2016-08-29 guoyingjie add  rruTypeFamilyName
            strRruTypeFamilyName = excelRead(RRuInfo, "rruTypeFamilyName");
        }
        
        public GetRruTypeByNodeNameEn excelRead;
        string rruTypeManufacturerIndex;
        string rruTypeIndex;
        string rruTypeRowStatus;
        string rruTypeName;
        string rruTypeMaxAntPathNum;
        string rruTypeMaxTxPower;
        string rruTypeBandWidth;
        string rruTypeSupportCellWorkMode;
        string strRruTypeFiberLength;//2014-2-27 luoxin RRUType新增节点
        string strRruTypeIrCompressMode;//2014-2-27 luoxin RRUType新增节点
        string strRruTypeFamilyName;//2016-08-29 guoyingjie add  rruTypeFamilyName

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
        string rruTypePortManufacturerIndex;
        string rruTypePortIndex;
        string rruTypePortNo;
        string rruTypePortRowStatus;
        string rruTypePortSupportFreqBand;
        string rruTypePortSupportFreqBandWidth;
        string rruTypePortPathNo;

        //2013-04-10 luoxin DTMUC00153813
        string rruTypePortSupportAbandTdsCarrierNum;
        string rruTypePortSupportFBandTdsCarrierNum;
        string rruTypePortCalAIqRxNom;
        string rruTypePortCalAIqTxNom;
        string rruTypePortCalPinRxNom;
        string rruTypePortCalPoutTxNom;
        //2013-04-10 luoxin end 

        //2014-3-5 luoxin RRU通道类型表增加新节点
        string strRruTypePortAntMaxPower;

        public GetRruTypePortByNodeNameEn excelRead;

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
            rruTypePortSupportAbandTdsCarrierNum = "";
            rruTypePortSupportFBandTdsCarrierNum = "";
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
        //public RRuTypePortTabStru(Dictionary<string, string> RRuInfo)
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
            rruTypePortSupportAbandTdsCarrierNum = "";
            rruTypePortSupportFBandTdsCarrierNum = "";
        }
    }
}
