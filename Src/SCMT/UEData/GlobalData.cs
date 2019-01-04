using SCMTOperationCore.Message.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace UEData
{
    using SIu8 = Byte;
    using SIs8 = Char;


    public static class GlobalData
    {
        /// <summary>
        /// 单小区最多可查询返回用户数
        /// </summary>
        public const int RRCOM_MAX_UE_NUM_SEARCH = 50;
        /// <summary>
        /// 每个UE支持的最大DRB数
        /// </summary>
        public const int RRCOM_MAX_DRB_UE = 11;
        /// <summary>
        /// 每个UE支持的最大PDUSESSION数
        /// </summary>
        public const int RRCOM_MAX_PDUSESSION_UE = 8;
        /// <summary>
        /// 每个PDUSESSION支持的最大QOSFLOW数
        /// </summary>
        public const int RRCOM_MAX_QOS_FLOW_PDUSESSION = 8;
        /// <summary>
        /// 最大的MCC字节数
        /// </summary>
        public const int RRCOM_MAX_MCC_BYTE_NUM = 3;
        /// <summary>
        /// 最大的MNC字节数
        /// </summary>
        public const int RRCOM_MAX_MNC_BYTE_NUM = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int OM_MAX_CELLNUM = 36;
        /// <summary>
        /// 
        /// </summary>
        public const int L2_MAX_UE_NUM_SEARCH = 400;
        /// <summary>
        /// 查询UE信息请求
        /// </summary>
        public static SIu16 O_LMTOM_SI_GET_UEINFO_REQ = 0xF2;
        /// <summary>
        /// 查询UE信息响应
        /// </summary>
        public static SIu16 O_OMLMT_SI_GET_UEINFO_RSP = 0xF3;
        /// <summary>
        /// UE测量信息显示请求
        /// </summary>
        public static SIu16 O_LMTOM_SI_GET_UEMEASINFO_REQ = 0xF4;
        /// <summary>
        /// UE测量信息显示响应
        /// </summary>
        public static SIu16 O_OMLMT_SI_GET_UEMEASINFO_RSP = 0xF5;
        /// <summary>
        /// UE IP信息查询请求
        /// </summary>
        public static ushort O_LMTOM_SI_GET_UEIPINFO_REQ = 0xF7;
        /// <summary>
        /// UE IP信息查询响应
        /// </summary>
        public static ushort O_OMLMT_SI_GET_UEIPINFO_RSP = 0xF8;
        /// <summary>
        /// 添加变量标志是否支持虚拟数据
        /// </summary>
        public static bool m_isSupportDummyData = false;
        /// <summary>
        /// 标志当前的查询是否要支持虚拟数据（单击支持True，双击显示真实Flase）
        /// </summary>
        public static bool m_ThisTimeisSupport = true;
        /// <summary>
        /// 基站网元ip
        /// </summary>
        public static string strIpAddr = null;
        /// <summary>
        /// 小区信息集合
        /// </summary>
        public static ObservableCollection<CellUeInformation> CellUeInfo = new ObservableCollection<CellUeInformation>();
        /// <summary>
        /// UE信息
        /// </summary>
        public static ObservableCollection<UeInformation> strUeInfo = new ObservableCollection<UeInformation>();
        /// <summary>
        /// UE测量配置信息
        /// </summary>
        public static ObservableCollection<UeMeasCfInfo> strUeMeasInfo = new ObservableCollection<UeMeasCfInfo>();
        /// <summary>
        /// UE业务查询下的UE用户信息
        /// </summary>
        public static ObservableCollection<UeipInfo> strUeIpInfo = new ObservableCollection<UeipInfo>();
        /// <summary>
        /// UE业务查询下的小区信息
        /// </summary>
        public static ObservableCollection<UeipCellInfo> strUeIpCellInfo = new ObservableCollection<UeipCellInfo>();

        public static string m_csUeLoValueList = "0:边缘/1:中心/2:无效";
        public static string m_csMacTAValueList = "0:未配置/1:超时/2:未超时";
        public static string m_FlowTypeValueList = "1:AMR-NB/2:AMR-WB";
        public static string m_spsactiveflagValueList = "0:未激活/1:激活";
        public static string m_caactiveflagValueList = "0:辅小区未配置/1:辅小区未激活/2:辅小区已激活";
        /// <summary>
        /// UE测量的ID最大个数
        /// </summary>
        public const int RRCOM_MAX_UE_MEAS_UE = 32;
        /// <summary>
        /// 
        /// </summary>
        public const int MAC_MAX_ANT_NUM_PER_CELL = 24;
        /// <summary>
        /// 记录查询按钮点击次数
        /// </summary>
        public static ushort BtnCount = 0;
        /// <summary>
        /// UE业务查询中的小区类型
        /// </summary>
        public static byte CellType;

    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_GbrQoSFlowInfo : IASerialize
    {
        public ulong u64MbrDownlink;                  //下行最大流速率，单位bps
        public ulong u64MbrUplink;                    //上行最大流速率，单位bps
        public ulong u64GbrDownlink;                  //下行保证流速率，单位bps
        public ulong u64GbrUplink;                  //上行保证流速率，单位bps

        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ulong) * 4;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeInt64(bytes, offset + used, ref u64MbrDownlink, false);
            used += SerializeHelper.DeserializeInt64(bytes, offset + used, ref u64MbrUplink, false);
            used += SerializeHelper.DeserializeInt64(bytes, offset + used, ref u64GbrDownlink, false);
            used += SerializeHelper.DeserializeInt64(bytes, offset + used, ref u64GbrUplink, false);

            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_QosFlowInfo : IASerialize
    {
        public ushort u16Qfi;                          //QFI信息
        public byte u8DrbId;                         //DRBID
        public byte u8Pad;
        public RRCOM_GbrQoSFlowInfo struGbrQosFlowInfo;           //GBR Qos Flow信息
        public RRCOM_QosFlowInfo()
        {
            struGbrQosFlowInfo = new RRCOM_GbrQoSFlowInfo();
        }
        public int Len => ContentLen;

        public ushort ContentLen => (ushort)(sizeof(ushort) + sizeof(byte) * 2 + Marshal.SizeOf<RRCOM_GbrQoSFlowInfo>());

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16Qfi, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DrbId);
            used += 1;
            used += struGbrQosFlowInfo.DeserializeToStruct(bytes, offset + used);
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_SecurityInd : IASerialize
    {
        public byte u8IntProtectInd;                 //完保指示，取值：ENUMERATED (required, preferred, not needed, …)
        public byte u8EncProtectInd;                 //加密指示，取值：ENUMERATED (required, preferred, not needed, …)

        public int Len => ContentLen;
        public ushort ContentLen => sizeof(byte) * 2;
        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8IntProtectInd);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8EncProtectInd);
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_S_NSSAI : IASerialize
    {
        public byte u8Sst;                           //SST信息
        public uint u32Sd;                           //SD信息

        public int Len => ContentLen;

        public ushort ContentLen => sizeof(byte) + sizeof(uint);

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Sst);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32Sd, false);
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_AmbrInfo : IASerialize
    {
        public ulong u64AmbrDownlink;                 //聚合下行最大速率，单位bps
        public ulong u64AmbrUplink;                   //聚合上行最大速率，单位bps

        public ushort ContentLen => sizeof(ulong) * 2;

        public int Len => ContentLen;
        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeInt64(bytes, offset + used, ref u64AmbrDownlink, false);
            used += SerializeHelper.DeserializeInt64(bytes, offset + used, ref u64AmbrUplink, false);
            return used;
        }

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }
    }

    /*PduSession Info*/
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_PduSessionInfo : IASerialize
    {
        public byte u8SecurityIndFlag;               //SecurityInd是否存在，取值0：不存在，1：存在
        public byte u8PdusessionId;                  //Pdusession ID
        public byte u8ValidNofQosFlow;    /*该Pdusession下的QosFlow个数，至少会存在一个*/
        public byte u8pad;
        public RRCOM_AmbrInfo struPduAmbr;                     //Pdusession级AMBR信息
        public RRCOM_S_NSSAI struSnssai;                      //S-nssai信息
        public RRCOM_SecurityInd struSecurityInd;                 //SecurityInd信息，当u8SecurityIndFlag取值为1时有效
        /// <summary>
        /// 数组元素个数指定为RRCOM_MAX_QOS_FLOW_PDUSESSION
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.RRCOM_MAX_QOS_FLOW_PDUSESSION)]
        public RRCOM_QosFlowInfo[] struQosFlowInfo;      //QosFlow信息
        public RRCOM_PduSessionInfo()
        {
            struPduAmbr = new UEData.RRCOM_AmbrInfo();
            struSnssai = new UEData.RRCOM_S_NSSAI();
            struSecurityInd = new UEData.RRCOM_SecurityInd();
            struQosFlowInfo = new RRCOM_QosFlowInfo[GlobalData.RRCOM_MAX_QOS_FLOW_PDUSESSION];
        }
        public int Len => ContentLen;

        public ushort ContentLen => (ushort)(sizeof(byte) * 2 + Marshal.SizeOf<RRCOM_AmbrInfo>() + Marshal.SizeOf<RRCOM_S_NSSAI>() + Marshal.SizeOf<RRCOM_SecurityInd>() + (Marshal.SizeOf<RRCOM_QosFlowInfo>()) * u8ValidNofQosFlow);

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {

            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8SecurityIndFlag);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8PdusessionId);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ValidNofQosFlow);
            used += 1;
            used += struPduAmbr.DeserializeToStruct(bytes, offset + used);
            used += struSnssai.DeserializeToStruct(bytes, offset + used);
            used += struSecurityInd.DeserializeToStruct(bytes, offset + used);
            for (int i = 0; i < u8ValidNofQosFlow && u8ValidNofQosFlow <= GlobalData.RRCOM_MAX_QOS_FLOW_PDUSESSION; i++)
            {
                struQosFlowInfo[i] = new RRCOM_QosFlowInfo();
                used += struQosFlowInfo[i].DeserializeToStruct(bytes, offset + used);
            }
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_PlmnId : IASerialize
    {
        /// <summary>
        /// PLMN移动国家码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.RRCOM_MAX_MCC_BYTE_NUM)]
        public byte[] u8Mcc;
        /// <summary>
        /// PLMN移动网络码,第三字节为0xff时表示该位无效
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.RRCOM_MAX_MNC_BYTE_NUM)]
        public byte[] u8Mnc;
        public RRCOM_PlmnId()
        {
            u8Mcc = new byte[GlobalData.RRCOM_MAX_MCC_BYTE_NUM];
            u8Mnc = new byte[GlobalData.RRCOM_MAX_MNC_BYTE_NUM];
        }
        public ushort ContentLen => GlobalData.RRCOM_MAX_MCC_BYTE_NUM + GlobalData.RRCOM_MAX_MNC_BYTE_NUM;

        public int Len => ContentLen;

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.SerializeBytes(ref u8Mcc, 0, bytes, offset + used, u8Mcc.Length);
            used += SerializeHelper.SerializeBytes(ref u8Mnc, 0, bytes, offset + used, u8Mnc.Length);
            return used;
        }

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_Gumai : IASerialize
    {
        public RRCOM_PlmnId struPlmnId;                 //PLMN ID
        public byte u8AmfRegionId;              //AMF Region ID, BIT STRING (SIZE(8))
        public byte u8AmfPoniter;               //AMF Pointer,BIT STRING (SIZE(6))
        public byte u16AmfSetId;                //AMF Set ID,BIT STRING (SIZE(10))
        public RRCOM_Gumai()
        {
            struPlmnId = new UEData.RRCOM_PlmnId();
        }
        public int Len => ContentLen;

        public ushort ContentLen => (ushort)(Marshal.SizeOf<RRCOM_PlmnId>() + sizeof(byte) * 3);
        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += struPlmnId.DeserializeToStruct(bytes, offset + used);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8AmfRegionId);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8AmfPoniter);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u16AmfSetId);
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class RRCOM_DrbInfo : IASerialize
    {
        public byte u8DrbId;                         /*DRBID*/
        public byte u8LoChId;                        /*LoChId*/
        public byte u8ModeFlag;                      /* DRB模式标志，显示为1:AM，2:UM_BI，3:UM_UNI_UL，4:UM_UNI_DL ,单向UM的本次可不实现，后续有需要再加*/

        public ushort ContentLen => sizeof(byte) * 3;

        public int Len => ContentLen;

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8DrbId);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8LoChId);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ModeFlag);
            return used;
        }

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }
    }


    public class RRCOM_UeInfo
    {
        public byte u8SpcellLocalCellId;                              /*主服务小区本地小区ID*/
        public byte u8ScellLocalCellId;                               /*辅小区本地ID,不存在时,填写无效值Invalid_u8(255)*/
        public ushort u16Crint;                                         /*用户Crnti,不存在时,填写无效值Invalid_u16(65535)*/
        public ushort u16UeIndexCell;                                   //用户小区内索引
        public uint u32UeIndexGnb;                                    /*用户基站内索引*/
        public uint u32RanNgapId;                                     /*用户Ran侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)*/
        public uint u32AmfNgapId;                                     /*用户Amf侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)*/
        public byte u8Pad;                                            /*预留后续inactive时用户状态使用*/
        public byte u8ValidNofDrb;                                    /*该用户拥有的DRB个数,不存在时,填写0*/
        /// <summary>
        /// 元素个数RRCOM_MAX_DRB_UE
        /// </summary>
        public RRCOM_DrbInfo[] struDrbInfo;                    /*QosFlow映射的DRB信息,需要分级显示,当u8ValidNofDrb为0时,不进行显示*/
        public RRCOM_AmbrInfo struUeAmbr;                                       /*用户级AMBR信息*/
        public byte u8ValidNofPdusession;                             /*该用户拥有的Pdusession个数,不存在时,填写0*/
        /// <summary>
        /// 元素个数RRCOM_MAX_PDUSESSION_UE
        /// </summary>
        public RRCOM_PduSessionInfo[] struPdusessionInfo;   /*每个Pdusession信息,需要分级显示,当u8ValidNofPdusession为0时,不进行显示*/
        public RRCOM_Gumai struGumai;                                        /*用户Gumai信息*/
    }
    #region 90SA load2 UE信息查询
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_OMLMT_SI_Nr_GetUeInfoRspMsg : IASerialize
    {
        public ushort u16MsgLength; /* 消息长度*/
        public ushort u16MsgType;   /* 消息类型*/
        public ushort u16RequestId; /*requestId*/
        public byte u8Result;    /*0-执行成功;1-执行失败*/
        public byte u8Version;   /*兼容标志，版本号*/
        public byte u8EndFlag; /*DTMUC00191633 拆分消息结束标志:0表示所有小区信息上报结束,1表示未结束*/
        public byte u8CellNum;  /*拆分消息上报：表示本条消息上报的小区数*/
        public ushort u16RealNofUeInEnb; /*基站内实际用户数*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.OM_MAX_CELLNUM)]
        public OM_STRU_SI_NrCellUeInformation[] struUeInfo; /*小区内UE信息*/
        public OMLMTA_OMLMT_SI_Nr_GetUeInfoRspMsg()
        {
            struUeInfo = new OM_STRU_SI_NrCellUeInformation[GlobalData.OM_MAX_CELLNUM];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 4 + sizeof(byte) * 4;
        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16RequestId, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Result);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Version);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8EndFlag);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8CellNum);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16RealNofUeInEnb, false);
            //used += struUeInfo.DeserializeToStruct(bytes, offset + used);
            for (int i = 0; i < u8CellNum && u8CellNum <= GlobalData.OM_MAX_CELLNUM; i++)
            {
                struUeInfo[i] = new OM_STRU_SI_NrCellUeInformation();
                used += struUeInfo[i].DeserializeToStruct(bytes, offset + used);
            }
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OM_STRU_SI_NrCellUeInformation : IASerialize
    {
        public uint u32CellSlotNo;                   /*本小区所在BBU板卡的的槽位号*/
        public int s32CellIndexBBU;                 /*BBU内小区索引*/
        public ushort u16PhyId;                        /*小区的PCI*/
        public byte u8PCellLocalCellId;                  /*主小区本地小区索引*/
        public byte u8Pad;
        public ushort u16UeNum;                         /*本小区的UE个数*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad2;
        /// <summary>
        /// 数组元素个数初值RRCOM_MAX_UE_NUM_SEARCH
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.RRCOM_MAX_UE_NUM_SEARCH)]
        public OM_SI_Nr_UeInformation[] struRrcOmUeInfo;   /*本小区UE的信息*/
        public OM_STRU_SI_NrCellUeInformation()
        {
            u8Pad2 = new byte[2];
            struRrcOmUeInfo = new OM_SI_Nr_UeInformation[GlobalData.RRCOM_MAX_UE_NUM_SEARCH];
        }
        public int Len => ContentLen;

        public ushort ContentLen => (ushort)(sizeof(uint) + sizeof(int) + sizeof(ushort) * 2 + sizeof(byte) * 4 + (Marshal.SizeOf<OM_SI_Nr_UeInformation>()) * u16UeNum);

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32CellSlotNo, false);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref s32CellIndexBBU, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16PhyId, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8PCellLocalCellId);
            used += 1;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16UeNum, false);
            used += 2;
            for (int i = 0; i < u16UeNum && u16UeNum <= GlobalData.RRCOM_MAX_UE_NUM_SEARCH; i++)
            {
                struRrcOmUeInfo[i] = new OM_SI_Nr_UeInformation();
                used += struRrcOmUeInfo[i].DeserializeToStruct(bytes, offset + used);
            }
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OM_SI_Nr_UeInformation : IASerialize
    {
        public byte u8SpcellLocalCellId;                              /*主服务小区本地小区ID*/
        public byte u8ScellLocalCellId;                               /*辅小区本地ID,不存在时,填写无效值Invalid_u8(255)*/
        public ushort u16Crint;                                         /*用户Crnti,不存在时,填写无效值Invalid_u16(65535)*/
        public ushort u16UeIndexCell;                                   /*用户小区内索引*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8pad1;
        public uint u32UeIndexGnb;                                    /*用户基站内索引*/
        public uint u32RanNgapId;                                     /*用户Ran侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)*/
        public uint u32AmfNgapId;                                     /*用户Amf侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)*/
        public byte u8Pad;                                            /*预留后续inactive时用户状态使用*/
        public byte u8ValidNofDrb;                                    /*该用户拥有的DRB个数,不存在时,填写0*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.RRCOM_MAX_DRB_UE)]
        public RRCOM_DrbInfo[] struDrbInfo;                    /*QosFlow映射的DRB信息,需要分级显示,当u8ValidNofDrb为0时,不进行显示*/
        public RRCOM_AmbrInfo struUeAmbr;                                       /*用户级AMBR信息*/
        public byte u8ValidNofPdusession;                             /*该用户拥有的Pdusession个数,不存在时,填写0*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] u8Pad3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.RRCOM_MAX_PDUSESSION_UE)]
        public RRCOM_PduSessionInfo[] struPdusessionInfo;   /*每个Pdusession信息,需要分级显示,当u8ValidNofPdusession为0时,不进行显示*/
        public RRCOM_Gumai struGumai;                                        /*用户Gumai信息*/
        public OM_SI_Nr_UeInformation()
        {
            u8pad1 = new byte[2];
            u8Pad2 = new SIu8[3];
            struDrbInfo = new RRCOM_DrbInfo[GlobalData.RRCOM_MAX_DRB_UE];
            struUeAmbr = new RRCOM_AmbrInfo();
            u8Pad3 = new SIu8[3];
            struPdusessionInfo = new RRCOM_PduSessionInfo[GlobalData.RRCOM_MAX_PDUSESSION_UE];
            struGumai = new RRCOM_Gumai();
        }
        public int Len => ContentLen;

        public ushort ContentLen => (ushort)(sizeof(byte) * 12 + sizeof(ushort) * 2 + sizeof(uint) * 3 + Marshal.SizeOf<RRCOM_DrbInfo>() * u8ValidNofDrb + Marshal.SizeOf<RRCOM_AmbrInfo>() + Marshal.SizeOf<RRCOM_PduSessionInfo>() * u8ValidNofPdusession + Marshal.SizeOf<RRCOM_Gumai>());

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8SpcellLocalCellId);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ScellLocalCellId);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16Crint, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16UeIndexCell, false);
            used += 2;
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32UeIndexGnb, false);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32RanNgapId, false);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32AmfNgapId, false);
            used += 1;
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ValidNofDrb);
            used += 2;
            for (int i = 0; i < u8ValidNofDrb && u8ValidNofDrb <= GlobalData.RRCOM_MAX_DRB_UE; i++)
            {
                struDrbInfo[i] = new UEData.RRCOM_DrbInfo();
                used += struDrbInfo[i].DeserializeToStruct(bytes, offset + used);
            }
            used += struUeAmbr.DeserializeToStruct(bytes, offset + used);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ValidNofPdusession);
            used += 3;
            for (int j = 0; j < u8ValidNofPdusession && u8ValidNofPdusession <= GlobalData.RRCOM_MAX_PDUSESSION_UE; j++)
            {
                struPdusessionInfo[j] = new RRCOM_PduSessionInfo();
                used += struPdusessionInfo[j].DeserializeToStruct(bytes, offset + used);
            }
            used += struGumai.DeserializeToStruct(bytes, offset + used);
            return used;
        }
    }
    #endregion
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_LMTOM_GetUeInfoReqMsgBigCap : IASerialize
    {
        public ushort u16MsgLength;                              /*消息长度*/
        public ushort u16MsgType;                               /*消息类型*/
        public byte u8LocalCellId;                              /*本地小区索引*/
        public ushort u16RequestId;                               /*requestId*/
        public byte u8Pad;                                    /*补位*/
        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            if (ret.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16MsgLength);
            used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16MsgType);
            used += SerializeHelper.SerializeByte(ref ret, offset + used, u8LocalCellId);
            used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16RequestId);
            used += 1;
            return used;
        }
        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            throw new NotImplementedException();
        }
        public static explicit operator SI_STRU_LMTENBMsgHead(OMLMTA_LMTOM_GetUeInfoReqMsgBigCap Cap)
        {
            SI_STRU_LMTENBMsgHead Head = new UEData.SI_STRU_LMTENBMsgHead();
            Head.u16MsgLength = Cap.u16MsgLength;
            Head.u16MsgType = Cap.u16MsgType;
            return Head;
        }
        public int Len => ContentLen;
        public ushort ContentLen => sizeof(ushort) * 3 + sizeof(byte) * 2;
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class SI_STRU_LMTENBMsgHead : IASerialize
    {
        public ushort u16MsgLength;                              /*消息长度*/
        public ushort u16MsgType;                               /*消息类型*/
        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            if (ret.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16MsgLength);
            used += SerializeHelper.SerializeUshort(ref ret, offset + used, u16MsgType);
            return used;
        }
        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            throw new NotImplementedException();
        }
        public int Len => ContentLen;
        public ushort ContentLen => sizeof(ushort) * 2;

    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_OMLMT_SI_GetUeInfoRspMsg_Head : IASerialize//OMLMTA_OMLMT_SI_Nr_GetUeInfoRspMsg
    {

        public ushort u16MsgLength; /* 消息长度*/
        public ushort u16MsgType;   /* 消息类型*/
        public byte u8Result;    /*0-执行成功;1-执行失败*/
        public byte u8Version;   /*兼容标志，版本号*/
        public byte u8EndFlag;  /*2014-4-17 songwenjing 使用此保留位，表示该包是否是最后一个包。0表示所有小区信息上报结束，1表示未结束*/
        public byte u8CellNum;  /*小区个数*/
        public ushort u16RealNofUeInEnb; /*基站内实际用户数量*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad;    /*补位*/
        public OMLMTA_OMLMT_SI_GetUeInfoRspMsg_Head()
        {
            u8Pad = new byte[2];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 3 + sizeof(byte) * 6;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }

            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Result);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Version);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8EndFlag);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8CellNum);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16RealNofUeInEnb, false);
            used += 2;
            return used;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_LMTOM_SI_GetUeMeasInfoReqMsg : IASerialize
    {
        public ushort u16MsgLength; /*消息长度 */
        public ushort u16MsgType;   /*消息类型*/
        public ushort u16UeIndexCell;   /*UE基站索引*/
        public byte u8LocalCellId; /*本地小区索引*/
        public byte u8pad;

        public ushort ContentLen => sizeof(ushort) * 3 + sizeof(byte) * 2;

        public int Len => ContentLen;

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16UeIndexCell, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8LocalCellId);
            used += 1;
            return used;
        }

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// OMLMTA_LMTOM_SI_GetUeMeasInfoReqMsg显示转换为SI_STRU_LMTENBMsgHead
        /// </summary>
        /// <param name="ReqMsg"></param>
        public static explicit operator SI_STRU_LMTENBMsgHead(OMLMTA_LMTOM_SI_GetUeMeasInfoReqMsg ReqMsg)
        {
            SI_STRU_LMTENBMsgHead head = new UEData.SI_STRU_LMTENBMsgHead();
            head.u16MsgLength = ReqMsg.u16MsgLength;
            head.u16MsgType = ReqMsg.u16MsgType;
            return head;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_OMLMT_SI_GetUeMeasInfoRspMsg1 : IASerialize
    {
        public ushort u16MsgLength; /* 消息长度*/
        public ushort u16MsgType;   /* 消息类型*/
        public byte u8Result;     /*0-执行成功;1-执行失败*/
        public byte u8VerType;    /*255:老版本 1:Carrier Frequency扩容为u32的版本 wangxiaoying 2018.8.9 u8Pad[1]补位改为版本号，配合高层扩容Carrier Frequency为u32 */
        public byte u8VerIndicator;//2013-11-12 songwenjing1 DTMUC00165486 配合HL修改，1：指示无效值为255的版本，0：指示无效值为非255的老版本
        public byte u8ValidNofId;  /*查询到的UE测量ID有效个数*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.RRCOM_MAX_UE_MEAS_UE)]
        public LMTOM_UeMeasIdInfo1[] struMeasIdInfo; /*测量配置信息*/
        public OMLMTA_OMLMT_SI_GetUeMeasInfoRspMsg1()
        {
            struMeasIdInfo = new LMTOM_UeMeasIdInfo1[GlobalData.RRCOM_MAX_UE_MEAS_UE];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 2 + sizeof(byte) * 4;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Result);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8VerType);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8VerIndicator);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ValidNofId);
            for (int i = 0; i < u8ValidNofId && i <= GlobalData.RRCOM_MAX_UE_MEAS_UE; i++)
            {
                struMeasIdInfo[i] = new UEData.LMTOM_UeMeasIdInfo1();
                used += struMeasIdInfo[i].DeserializeToStruct(bytes, offset + used);
            }
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class LMTOM_UeMeasIdInfo1 : IASerialize
    {
        public byte u8MeasId;              /*测量ID*/
        public byte u8MeasObjectId;     /*测量对象ID*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad1;
        public uint u32CarrierFreq;    /*Carrier Frequency*/
        public uint u32MeasObjectChoice;     /*MeasObjectChoice， RRCOM_MeasChoice*/
        public byte u8ReportConfigId;                                  /*测量报告配置ID*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] u8Pad;//3
        public uint u32ReportCfgChoice;    /*测量报告配置RAT  RRCOM_MeasChoice*/
        public uint u32ReportConfig;         /*测量报告配置 RRCOM_ReportConfig*/
        public uint u32MeasPurpose;        /*测量目的RRCOM_MeasPurpose*/
        public uint u32AlgorithmType;     /*触发算法RRCOM_MeasAlgoType*/
        public LMTOM_UeMeasIdInfo1()
        {
            u8Pad1 = new byte[2];
            u8Pad = new byte[3];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(uint) * 6 + sizeof(byte) * 8;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8MeasId);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8MeasObjectId);
            used += 2;
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32CarrierFreq, false);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32MeasObjectChoice, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ReportConfigId);
            used += 3;
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32ReportCfgChoice, false);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32ReportConfig, false);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32MeasPurpose, false);
            used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref u32AlgorithmType, false);
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_OMLMT_GetUeMeasInfoRspMsg_Head : IASerialize
    {

        public ushort u16MsgLength; /* 消息长度*/
        public ushort u16MsgType;   /* 消息类型*/
        public byte u8Result;     /*0-执行成功;1-执行失败*/
        public byte u8VerType;    /*255:老版本 1:Carrier Frequency扩容为u32的版本  wangxiaoying 2018.8.9 u8Pad[1]补位改为版本号，配合高层扩容Carrier Frequency为u32 */
        public byte u8VerIndicator;//2013-11-12 songwenjing1 DTMUC00165486 配合HL修改，1：指示无效值为255的版本，0：指示无效值为非255的老版本
        public byte u8ValidNofId;  /*查询到的UE测量ID有效个数*/

        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 2 + sizeof(byte) * 4;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Result);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8VerType);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8VerIndicator);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8ValidNofId);
            return used;
        }
    }
    #region UE业务面查询
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_LMTOM_GetUeIpInfoReqMsgBigCap : IASerialize
    {
        public ushort u16MsgLength; /*消息长度 */
        public ushort u16MsgType;   /*消息类型*/
        public ushort u16CellId;    /*查询的小区ID，取值范围0~11*/
        public ushort u16UeIndex;   /*指定UE的编号，范围0~1200，0XFFFF表示查询小区下所有UE*/

        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 4;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16CellId, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16UeIndex, false);
            return used;
        }
        public static explicit operator SI_STRU_LMTENBMsgHead(OMLMTA_LMTOM_GetUeIpInfoReqMsgBigCap Req)
        {
            SI_STRU_LMTENBMsgHead head = new SI_STRU_LMTENBMsgHead();
            head.u16MsgLength = Req.u16MsgLength;
            head.u16MsgType = Req.u16MsgType;
            return head;
        }
    }

    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_OMLMT_SI_GetUeIPInfoRspMsgHead : IASerialize
    {
        public ushort u16MsgLength; /* 消息长度*/
        public ushort u16MsgType;   /* 消息类型*/
        public byte u8Result;      /*0-执行成功;1-执行失败*/
        public byte u8Version;     /*2014-1-23 songwenjing 修改结构体，将保留字节提出一个作为版本标识*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad;

        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 2 + sizeof(byte) * 4;

        public OMLMTA_OMLMT_SI_GetUeIPInfoRspMsgHead()
        {
            u8Pad = new byte[2];
        }

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Result);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Version);
            used += 2;
            return used;
        }
    }

    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg_1 : IASerialize
    {

        public ushort u16MsgLength; /* 消息长度*/
        public ushort u16MsgType;   /* 消息类型*/
        public byte u8Result;      /*0-执行成功;1-执行失败*/
        public byte u8Version;     /*2014-1-23 songwenjing 修改结构体，将保留字节提出一个作为版本标识*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad;
        public OM_STRU_SI_CellUeIpInformation_1 struUeIpInfo; /*小区内UE信息*/
        public OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg_1()
        {
            u8Pad = new byte[2];
            struUeIpInfo = new UEData.OM_STRU_SI_CellUeIpInformation_1();
        }

        public int Len => ContentLen;

        public ushort ContentLen => (ushort)(sizeof(ushort) * 2 + sizeof(byte) * 4 + Marshal.SizeOf<OM_STRU_SI_CellUeIpInformation_1>());

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16MsgType, false);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Result);
            used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8Version);
            used += 2;
            used += struUeIpInfo.DeserializeToStruct(bytes, used + offset);
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OM_STRU_SI_CellUeIpInformation_1 : IASerialize
    {
        public byte u8BbuCellIndex;        /*bbu小区索引,LTE和NBIOT复用*/
        public byte u8CellIndexEnb;        /*基站内小区索引,LTE和NBIOT复用*/
        public byte u8SlotId;              /*小区所在bpog板对应的槽位号,LTE和NBIOT复用*/
        public byte u8ProcId;              /*小区所在DSP对应的L2模块对应的处理器号,LTE和NBIOT复用*/
        public byte u8Pad;
        public byte u8CellType;            /*小区类型，LTE = 0，NBIOT = 1*/
        public ushort u16ValidNofUeInEnb;    /*LMT上给出用户信息的用户数量，最多400个,LTE和NBIOT复用*/
        public ushort u16UlSpsActiveUeNum;   /*指示小区中上行激活sps的用户数，1，是；0，否，LTE专用*/
        public ushort u16DlSpsActiveUeNum;   /*指示小区中下行激活sps的用户数，1，是；0，否，LTE专用*/
        /// <summary>
        /// 每个用户的信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.L2_MAX_UE_NUM_SEARCH)]
        public L2_SI_UeInfo_1[] struUeInfo;
        public OM_STRU_SI_CellUeIpInformation_1()
        {
            struUeInfo = new L2_SI_UeInfo_1[GlobalData.L2_MAX_UE_NUM_SEARCH];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(byte) * 6 + sizeof(ushort) * 3;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8BbuCellIndex);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8CellIndexEnb);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8SlotId);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8ProcId);
            used += 1;
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8CellType);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16ValidNofUeInEnb, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16UlSpsActiveUeNum, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16DlSpsActiveUeNum, false);
            for (int i = 0; i < u16ValidNofUeInEnb && i < GlobalData.L2_MAX_UE_NUM_SEARCH; i++)
            {
                struUeInfo[i] = new L2_SI_UeInfo_1();
                used += struUeInfo[i].DeserializeToStruct(bytes, used + offset);
            }
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class L2_SI_UeInfo_1 : IASerialize
    {
        public ushort u16UeIndexEnb;         /*基站内UE索引，LTE和NBIOT复用*/
        public ushort u16UeIndexCell;        /*小区内UE索引，LTE和NBIOT复用*/
        public uint u32UeIpInfo;           /*用户IP地址,需要解析成xxx.xxx.xxx.xxx显示,0为无效值，LTE和NBIOT复用*/
        public byte u8HlMacUeLocation;     /*HL配置的UE位置信息 0-边缘；1-中心；2-无效，LTE专用*/
        public byte u8MacUeLocation;       /*MAC计算的UE位置信息 0-边缘；1-中心；2-无效，LTE专用*/
        public byte u8MacTA;               /*TA信息 0-未配置；1-超时；2-未超时，LTE和NBIOT复用*/
        public byte u8MacTmMode;           /*MAC下行传输模式 1-TM1;2-TM2;3-TM3;4-TM4;5-TM5;6-TM6;7-TM7;8-TM8;9-TM9，LTE专用*/
        public byte u8UeCapability;        /*UE等级能力，LTE和NBIOT复用*/
        public byte u8FlowType;            /*2014-1-23 songwenjing 占用一个保留字节：语音编码格式 1：AMR-NB；2：AMR-WB；other：undefine，LTE专用*/
                                           //SIu8                 u8Pad[3]; 
                                           //2014-4-8 songwenjing DTMUC00165030 新添加 begin
                                           //SIu8                 u8Pad[2];              //2014-1-23 保留字节少一个
        public byte u8UlSpsActiveFlag;      /*指示用户是否是上行sps激活用户，LTE专用*/
        public byte u8DlSpsActiveFlag;      /*指示用户是否是下行sps激活用户，LTE专用*/
                                            //2014-4-8 songwenjing DTMUC00165030 新添加 end
                                            //2014-6-30 wangxiaoying DTMUC00223182 LMT上增加查询L2载辅波激活与否功能 begin
        public byte u8CaActiveFlag;        /*载波聚合小区状态，LTE专用*/
                                           //2014-6-30 wangxiaoying DTMUC00223182 LMT上增加查询L2载辅波激活与否功能 end

        //2014-8-27 wangxiaoying 应HL高层修改LMT显示，CR号是DTMUC00231650 begin
        public byte u8ScellCellIndexEnb;     //辅小区ENB索引，LTE专用
        public ushort u16ScellUeIndex;         //辅小区用户索引，LTE专用
                                               //2014-8-27 wangxiaoying 应HL高层修改LMT显示，CR号是DTMUC00231650 end
                                               //wangxiaoying add DTMUC00336140 [研发自提]配合HL和L2修改UE信息查询接口和UE业务面查询接口 2016.12.22 begin
        public ushort s16UeUlMcl;              /*NBIOT专用，修正后上行MCL*/
        public ushort s16UeDlMcl;              /*NBIOT专用，修正后下行MCL*/
        public ushort s16UlSinr;               /*NBIOT专用，上行CQI修正后的Sinr*/
        public ushort s16DlSinr;               /*NBIOT专用，下行CQI修正后的Sinr*/
                                               //wangxiaoying add DTMUC00336140 [研发自提]配合HL和L2修改UE信息查询接口和UE业务面查询接口 2016.12.22 end

        //2045-3-25 wangxiaoying DTMUC00252570 应HL高层需要，增加UE所属的RRU编号和RRU通道编号 begin
        public byte u8UIPathNum;               //UE所占用的通道数，LTE专用
        public byte u8DIPathNum;               //UE所占用的通道数，LTE专用
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] au8Pad;               //补齐字节
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.MAC_MAX_ANT_NUM_PER_CELL)]
        public Ue_RruInfo[] struUIRruInfo; //UE上行选择的通道状态信息，LTE专用
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.MAC_MAX_ANT_NUM_PER_CELL)]
        public Ue_RruInfo[] struDIRruInfo; //UE下行选择的通道状态信息，LTE专用
        public L2_SI_UeInfo_1()
        {
            au8Pad = new SIu8[2];
            struUIRruInfo = new Ue_RruInfo[GlobalData.MAC_MAX_ANT_NUM_PER_CELL];
            struDIRruInfo = new Ue_RruInfo[GlobalData.MAC_MAX_ANT_NUM_PER_CELL];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 7 + sizeof(byte) * 14;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16UeIndexEnb, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16UeIndexCell, false);
            used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref u32UeIpInfo, false);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8HlMacUeLocation);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8MacUeLocation);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8MacTA);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8MacTmMode);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8UeCapability);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8FlowType);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8UlSpsActiveFlag);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8DlSpsActiveFlag);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8CaActiveFlag);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8ScellCellIndexEnb);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16ScellUeIndex, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref s16UeUlMcl, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref s16UeDlMcl, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref s16UlSinr, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref s16DlSinr, false);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8UIPathNum);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8DIPathNum);
            used += 2;
            for (int i = 0; i < u8UIPathNum && i < GlobalData.MAC_MAX_ANT_NUM_PER_CELL; i++)
            {
                struUIRruInfo[i] = new Ue_RruInfo();
                used += struUIRruInfo[i].DeserializeToStruct(bytes, used + offset);
            }
            for (int i = 0; i < u8DIPathNum && i < GlobalData.MAC_MAX_ANT_NUM_PER_CELL; i++)
            {
                struDIRruInfo[i] = new Ue_RruInfo();
                used += struDIRruInfo[i].DeserializeToStruct(bytes, used + offset);
            }
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class Ue_RruInfo : IASerialize
    {
        public byte u8RruNo;             //UE归属的RRU编号
        public byte u8RruPort;            //UE归属的RRU通道号

        public ushort ContentLen => sizeof(byte) * 2;

        public int Len => ContentLen;

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8RruNo);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8RruPort);
            return used;
        }

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg : IASerialize
    {

        public ushort u16MsgLength; /* 消息长度*/
        public ushort u16MsgType;   /* 消息类型*/
        public byte u8Result;      /*0-执行成功;1-执行失败*/
        public byte u8Version;     /*2014-1-23 songwenjing 修改结构体，将保留字节提出一个作为版本标识*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad;
        public OM_STRU_CellUeIpInformation_4 struUeIpInfo; /*小区内UE信息*/
        public OMLMTA_OMLMT_SI_GetUeIPInfoRspMsg()
        {
            u8Pad = new byte[2];
            struUeIpInfo = new OM_STRU_CellUeIpInformation_4();
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 2 + 4;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16MsgLength, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16MsgType, false);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8Result);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8Version);
            used += 2;
            used += struUeIpInfo.DeserializeToStruct(bytes, used + offset);
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class OM_STRU_CellUeIpInformation_4 : IASerialize
    {

        public byte u8BbuCellIndex;        /*bbu小区索引*/
        public byte u8CellIndexEnb;        /*基站内小区索引*/
        public byte u8SlotId;              /*小区所在bpog板对应的槽位号*/
        public byte u8ProcId;              /*小区所在DSP对应的L2模块对应的处理器号*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] u8Pad;
        public ushort u16ValidNofUeInEnb;    /*LMT上给出用户信息的用户数量，最多400个*/
        public ushort u16UlSpsActiveUeNum;   /*指示小区中上行激活sps的用户数，1，是；0，否*/
        public ushort u16DlSpsActiveUeNum;   /*指示小区中下行激活sps的用户数，1，是；0，否*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.L2_MAX_UE_NUM_SEARCH)]
        public L2_UeInfo4[] struUeInfo;/*每个用户的信息*/
        public OM_STRU_CellUeIpInformation_4()
        {
            u8Pad = new Byte[2];
            struUeInfo = new L2_UeInfo4[GlobalData.L2_MAX_UE_NUM_SEARCH];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 3 + 6;

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8BbuCellIndex);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8CellIndexEnb);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8SlotId);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8ProcId);
            used += 2;
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16ValidNofUeInEnb, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16UlSpsActiveUeNum, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16DlSpsActiveUeNum, false);
            for (int i = 0; i < u16ValidNofUeInEnb && i < GlobalData.L2_MAX_UE_NUM_SEARCH; i++)
            {
                struUeInfo[i] = new L2_UeInfo4();
                used += struUeInfo[i].DeserializeToStruct(bytes, used + offset);
            }
            return used;
        }
    }
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class L2_UeInfo4 : IASerialize
    {
        public ushort u16UeIndexEnb;         /*基站内UE索引*/
        public ushort u16UeIndexCell;        /*小区内UE索引*/
        public uint u32UeIpInfo;           /*用户IP地址,需要解析成xxx.xxx.xxx.xxx显示,0为无效值*/
        public byte u8HlMacUeLocation;     /*HL配置的UE位置信息 0-边缘；1-中心；2-无效*/
        public byte u8MacUeLocation;       /*MAC计算的UE位置信息 0-边缘；1-中心；2-无效*/
        public byte u8MacTA;               /*TA信息 0-未配置；1-超时；2-未超时*/
        public byte u8MacTmMode;           /*MAC下行传输模式 1-TM1;2-TM2;3-TM3;4-TM4;5-TM5;6-TM6;7-TM7;8-TM8;9-TM9*/
        public byte u8UeCapability;        /*UE等级能力*/
        public byte u8FlowType;            /*2014-1-23 songwenjing 占用一个保留字节：语音编码格式 1：AMR-NB；2：AMR-WB；other：undefine*/
                                           //SIu8                 u8Pad[3]; 
                                           //2014-4-8 songwenjing DTMUC00165030 新添加 begin
                                           //SIu8                 u8Pad[2];              //2014-1-23 保留字节少一个
        public byte u8UlSpsActiveFlag;      /*指示用户是否是上行sps激活用户*/
        public byte u8DlSpsActiveFlag;      /*指示用户是否是下行sps激活用户*/
                                            //2014-4-8 songwenjing DTMUC00165030 新添加 end
                                            //2014-6-30 wangxiaoying DTMUC00223182 LMT上增加查询L2载辅波激活与否功能 begin
        public byte u8CaActiveFlag;        /*载波聚合小区状态*/
                                           //2014-6-30 wangxiaoying DTMUC00223182 LMT上增加查询L2载辅波激活与否功能 end

        //2014-8-27 wangxiaoying 应HL高层修改LMT显示，CR号是DTMUC00231650 begin
        public byte u8ScellCellIndexEnb;     //辅小区ENB索引
        public ushort u16ScellUeIndex;         //辅小区用户索引
        //2045-3-25 wangxiaoying DTMUC00252570 应HL高层需要，增加UE所属的RRU编号和RRU通道编号 begin
        public byte u8UIPathNum;               //UE所占用的通道数
        public byte u8DIPathNum;               //UE所占用的通道数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] au8Pad;               //补齐字节
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.MAC_MAX_ANT_NUM_PER_CELL)]
        public Ue_RruInfo[] struUIRruInfo; //UE上行选择的通道状态信息
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GlobalData.MAC_MAX_ANT_NUM_PER_CELL)]
        public Ue_RruInfo[] struDIRruInfo; //UE下行选择的通道状态信息
        public L2_UeInfo4()
        {
            au8Pad = new byte[2];
            struUIRruInfo = new Ue_RruInfo[GlobalData.MAC_MAX_ANT_NUM_PER_CELL];
            struDIRruInfo = new Ue_RruInfo[GlobalData.MAC_MAX_ANT_NUM_PER_CELL];
        }
        public int Len => ContentLen;

        public ushort ContentLen => sizeof(ushort) * 3 + 14 + sizeof(uint);

        public int SerializeToBytes(ref byte[] ret, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public int DeserializeToStruct(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Len)
            {
                return -1;
            }
            int used = 0;
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16UeIndexEnb, false);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16UeIndexCell, false);
            used += SerializeHelper.DeserializeInt32(bytes, used + offset, ref u32UeIpInfo, false);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8HlMacUeLocation);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8MacUeLocation);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8MacTA);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8MacTmMode);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8UeCapability);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8FlowType);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8UlSpsActiveFlag);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8DlSpsActiveFlag);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8CaActiveFlag);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8ScellCellIndexEnb);
            used += SerializeHelper.DeserializeUshort(bytes, used + offset, ref u16ScellUeIndex, false);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8UIPathNum);
            used += SerializeHelper.DeserializeByte(bytes, used + offset, ref u8DIPathNum);
            used += 2;
            for (int i = 0; i < u8UIPathNum && i < GlobalData.MAC_MAX_ANT_NUM_PER_CELL; i++)
            {
                struUIRruInfo[i] = new Ue_RruInfo();
                used += struUIRruInfo[i].DeserializeToStruct(bytes, used + offset);
            }
            for (int i = 0; i < u8DIPathNum && i < GlobalData.MAC_MAX_ANT_NUM_PER_CELL; i++)
            {
                struDIRruInfo[i] = new Ue_RruInfo();
                used += struDIRruInfo[i].DeserializeToStruct(bytes, used + offset);
            }
            return used;
        }
    }
    #endregion
    /// <summary>
    /// 获取小区信息
    /// </summary>
    public class CellUeInformation
    {
        /// <summary>
        /// 小区名
        /// </summary>
        public byte PCellLocalCellId { get; set; }
        /// <summary>
        /// 本小区的UE个数
        /// </summary>
        public ushort UeNum { get; set; }
        /// <summary>
        /// 本小区所在BBU板卡的的槽位号
        /// </summary>
        public uint CellSlotNo { get; set; }
        /// <summary>
        /// BBU内小区索引
        /// </summary>
        public int CellIndexBBU { get; set; }
        /// <summary>
        /// 小区的PCI
        /// </summary>
        public ushort PhyId { get; set; }
    }
    public class UeInformation
    {
        /// <summary>
        /// 主服务小区本地小区ID
        /// </summary>
        public string SpcellLocalCellId { get; set; }
        /// <summary>
        /// 辅小区本地ID,不存在时,填写无效值Invalid_u8(255)
        /// </summary>
        public string ScellLocalCellId { get; set; }
        /// <summary>
        /// 用户Crnti,不存在时,填写无效值Invalid_u16(65535)
        /// </summary>
        public string u16Crint { get; set; }
        /// <summary>
        /// 用户小区内索引
        /// </summary>
        public string u16UeIndexCell { get; set; }
        /// <summary>
        /// 用户基站内索引
        /// </summary>
        public string u32UeIndexGnb { get; set; }
        /// <summary>
        /// 用户Ran侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)
        /// </summary>
        public string u32RanNgapId { get; set; }
        /// <summary>
        /// 用户Amf侧NgapId,不存在时,填写无效值Invalid_u32(4294967295)
        /// </summary>
        public string u32AmfNgapId { get; set; }
        /// <summary>
        /// 该用户拥有的DRB个数,不存在时,填写0
        /// </summary>
        public string u8ValidNofDrb { get; set; }
        /// <summary>
        /// QosFlow映射的DRB信息,需要分级显示,当u8ValidNofDrb为0时,不进行显示
        /// </summary>
        public ObservableCollection<ChildrenUeInfo> struDrbInfo { get; set; }
        /// <summary>
        /// 用户级AMBR信息-聚合下行最大速率，单位bps
        /// </summary>
        public string u64AmbrDownlink { get; set; }
        /// <summary>
        /// 用户级AMBR信息-聚合上行最大速率，单位bps
        /// </summary>
        public string u64AmbrUplink { get; set; }
        /// <summary>
        /// 用户拥有的Pdusession个数,不存在时,填写0
        /// </summary>
        public string u8ValidNofPdusession { get; set; }
        /// <summary>
        /// 每个Pdusession信息,需要分级显示,当u8ValidNofPdusession为0时,不进行显示
        /// </summary>
        public ObservableCollection<ChildrenUeInfo> struPdusessionInfo { get; set; }
        /// <summary>
        /// AMF Region ID
        /// </summary>
        public string u16AmfSetId { get; set; }
        /// <summary>
        /// AMF Pointer
        /// </summary>
        public string u8AmfPoniter { get; set; }
        /// <summary>
        /// AMF Set ID
        /// </summary>
        public string u8AmfRegionId { get; set; }
        /// <summary>
        /// plmn移动国家码
        /// </summary>
        public ObservableCollection<ChildrenUeInfo> u8Mcc { get; set; }
        /// <summary>
        /// Plmn移动网络码
        /// </summary>
        public ObservableCollection<ChildrenUeInfo> u8Mnc { get; set; }

    }
    /// <summary>
    /// UE测量配置信息
    /// </summary>
    public class UeMeasCfInfo
    {
        /// <summary>
        /// 测量ID
        /// </summary>
        public string MeasId { get; set; }
        /// <summary>
        /// 测量对象ID
        /// </summary>
        public string MeasObjectId { get; set; }
        /// <summary>
        /// Carrier Frequency
        /// </summary>
        public string CarrierFreq { get; set; }
        /// <summary>
        /// MeasObjectChoice
        /// </summary>
        public string MeasObjectChoice { get; set; }
        /// <summary>
        /// 测量报告配置ID
        /// </summary>
        public string ReportConfigId { get; set; }
        /// <summary>
        /// 测量报告配置RAT
        /// </summary>
        public string ReportCfgChoice { get; set; }
        /// <summary>
        /// 测量报告配置
        /// </summary>
        public string ReportConfig { get; set; }
        /// <summary>
        /// 测量目的
        /// </summary>
        public string MeasPurpose { get; set; }
        /// <summary>
        /// 触发算法RRCOM_MeasAlgoType
        /// </summary>
        public string AlgorithmType { get; set; }
    }
    /// <summary>
    /// 小区信息
    /// </summary>
    public class UeipCellInfo
    {
        /// <summary>
        /// BBU小区索引
        /// </summary>
        public string BbuCellIndex { get; set; }
        /// <summary>
        /// 基站内小区索引
        /// </summary>
        public string CellIndexEnb { get; set; }
        /// <summary>
        /// 小区所在bpog板对应的槽位号
        /// </summary>
        public string SlotId { get; set; }
        /// <summary>
        /// 小区所在DSP对应的L2模块对应的处理器号
        /// </summary>
        public string ProcId { get; set; }
        /// <summary>
        /// LMT上给出用户信息的用户数量，最多400个
        /// </summary>
        public string ValidNofUeInEnb { get; set; }
        /// <summary>
        /// 指示小区中上行激活sps的用户数
        /// </summary>
        public string UlSpsActiveUeNum { get; set; }
        /// <summary>
        /// 指示小区中下行激活sps的用户数
        /// </summary>
        public string DlSpsActiveUeNum { get; set; }
        /// <summary>
        /// AMR-NB
        /// </summary>
        public string nAmrNBNum { get; set; }
        /// <summary>
        /// AMR-WB
        /// </summary>
        public string AmrWBNum { get; set; }
        /// <summary>
        /// 小区类型
        /// </summary>
        public byte CellType { get; set; }
    }
    /// <summary>
    /// 业务查询到的UE信息
    /// </summary>
    public class UeipInfo
    {
        /// <summary>
        /// 基站内UE索引
        /// </summary>
        public string UeIndexCell { get; set; }
        public string UeIpInfo { get; set; }
        public string HlMacUeLocation { get; set; }
        public string MacUeLocation { get; set; }
        public string MacTA { get; set; }
        public string MacTmMode { get; set; }
        public string UeCapability { get; set; }
        public string FlowType { get; set; }
        public string UlSpsActiveFlag { get; set; }
        public string DlSpsActiveFlag { get; set; }
        public string CaActiveFlag { get; set; }
        public string ScellCellIndexEnb { get; set; }
        public string ScellUeIndex { get; set; }
        public string UIRruInfo { get; set; }
        public string DIRruInfo { get; set; }
        public string UeUlMcl { get; set; }
        public string UeDlMcl { get; set; }
        public string UlSinr { get; set; }
        public string DlSinr { get; set; }
    }
    public class ChildrenUeInfo
    {
        private string ueInfoChildren;
        public string UeInfoChildren
        {
            get
            {
                return ueInfoChildren;
            }

            set
            {
                ueInfoChildren = value;
            }
        }
        public List<ChildrenUeInfo> Children { get; set; }

        public ChildrenUeInfo(string Cname)
        {
            UeInfoChildren = Cname;
            Children = new List<ChildrenUeInfo>();
        }
    }





}
