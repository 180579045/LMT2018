/******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
*******************************************************************************
* File Name: nas_cbb_ie.h 
* Description:  GTPC模块的IE TAG 以及 消息TAG，消息定义等.
* Note: 
* History: 
*  EPS.MME 0.01 2008.09.04 吴鹏程 创建基础版本
******************************************************************************/

/******************************** HEAD FILES PROTECTION BEGIN*****************/
#ifndef  NAS_CBB_IE_H
#define  NAS_CBB_IE_H
/******************************** HEAD FILES PROTECTION  BEGIN****************/
#include "nas_cbb_tag.h"


/*************************IE Defination***************************************/
/************ 协议标准消息头结构定义 BEGIN ************/

/* 系统标准EMM消息头 24.301 */
/* not security protected NAS Messages */
typedef struct
{
    BITS8            btPd             :4;       /* 协议描述符 */
    BITS8            btSecHeaderType  :4;
    UINT8            ucMsgType;                 /* NAS消息类型,如:NAS_ATTACH_REQ_MSG */
    UINT32           uiDevNo;
}MSPS_NAS_EMM_HEADER_T;



/*系统标准ESM消息头 24.301 */
typedef struct
{
    BITS8           btPd            :4; 
    BITS8           btEpsBearerId   :4;  
    UINT8           ucMsgType;
    UINT32          uiDevNo;
    UINT8           ucProcedureTxnId;  
}MSPS_NAS_ESM_HEADER_T;


/* IMSI 23003：2.2 */
/*
#define MME_MAX_IMSI_LEN              8
typedef struct
{
    UINT8  usLength;
    UINT8  aucImsi[MME_MAX_IMSI_LEN];  
}MME_IMSI_T;


typedef struct
{
    UINT16      usLen ;                         
    BITS8       btMccDigit1 :4;
    BITS8       btMccDigit2 :4;
    BITS8       btMccDigit3 :4;
    BITS8       btMncDigit3 :4;
    BITS8       btMncDigit1 :4;
    BITS8       btMncDigit2 :4;
    UINT8       ucMmeCode;
    UINT16      usMmeGroupID;
    MME_MTMSI_T stMTmsi;
}MME_GUTI_T;

*/

#define  FLAG_OLD_GUTI    0x06
#define  FLAG_IMSI        0x01
#define  FLAG_IMEI        0x03
/* zhaobo add TMSI below 2013-03-07 */
#define  FLAG_TMSI        0x04
/* zhaobo add TMSI above 2013-03-07 */

typedef union
{
          MSPS_IMEI_T stImei ;
          MSPS_IMSI_T stImsi ;
          MSPS_GUTI_T stGuti ;
}MSPS_OLDGUTI_IMSI_T;


typedef struct
{
/*
    enum 
    {
        FLAG_OLD_GUTI = 0x06,
        FLAG_IMSI     = 0x01        
    }ucFlag_OldGutiOrImsi;
*/

    UINT8 ucFlag_OldGutiOrImsi;
    MSPS_OLDGUTI_IMSI_T  unGutiOrImsi;

}MSPS_EPS_MOBILE_ID_T;
/* MS Network Capability 24008：10.5.5.12 */
#define MSPS_MAX_NETWORK_CAPA_LEN              2



/*
typedef struct
{
UINT8 ucLength;
#ifdef BIG_ENDIAN
BITS8  btIdDigit1:4;
BITS8  btOddEvenIndicator:1;
BITS8  btIdentityType:3;
#else
BITS8  btIdentityType:3;
BITS8  btOddEvenIndicator:1;
BITS8  btIdDigit1:4;
#endif
UINT8 aucIdDigit[MSPS_MAX_MOBILE_ID_DIGIT_LEN];    // MAX=8 
}MSPS_MOBILE_ID_T;
*/


// typedef struct
// {
//     UINT8  ucLength;
// 
//  BITS8  btGEA1:1;
//  BITS8  btSmCapaDch:1;
//  BITS8  btSmCapaGCh:1;
//  BITS8  btUCS2:1;
//  BITS8  btSsScreenInd:2;
//  BITS8  btSoLsaCapa:1;
//  BITS8  btRevLevelInd:1;
//  BITS8  btPfcFeatureMode:1;
//  BITS8  btGea2:1;
//  BITS8  btGea3:1;
//  BITS8  btGea4:1;
//  BITS8  btGea5:1;
//  BITS8  btGea6:1;
//  BITS8  btGea7:1;
//  BITS8  btLcsVaCapa:1;
// 
// 
// 
// }MSPS_UE_NETWORK_CAPA_T;



#define  MSPS_NAS_UE_NETWORK_CAPA   15
typedef struct  
{
    UINT8    ucLength;     //长度 2~15
    UINT8    aucVal[MSPS_NAS_UE_NETWORK_CAPA];
}MSPS_UE_NETWORK_CAPA_T;






/* 24301-810 ,p212 , The UE security capability is a type 4 ie . */
#define MSPS_NAS_UE_SECU_CAPABILITY_MAX_LEN   5
typedef struct
{
    UINT8  ucLen;
    UINT8  aucValue[MSPS_NAS_UE_SECU_CAPABILITY_MAX_LEN];
}MSPS_UE_SEC_CAPA_T;


/* DRX Parameter 24008：10.5.5.6 */
// typedef struct
// {
//     UINT8  ucSplitPgCycleCode;
// #ifdef BIG_ENDIAN
//  BITS8  btDrxCycleLenCoef:4;
//  BITS8  btSplitOnCcch:1;
//  BITS8  btNonDrxTimer:3;
// #else
//  BITS8  btNonDrxTimer:3;
//  BITS8  btSplitOnCcch:1;
//  BITS8  btDrxCycleLenCoef:4;
// #endif
// }MME_DRX_PARA_T;
/************ EMM模块公共消息信元相关结构定义 END ************/
 //#define MME_MAX_TAI_LEN 5
 //typedef struct
 //{
 //    UINT8  ucTaiTag;
 // UINT8  aucValue[MME_MAX_TAI_LEN];
// }MME_TAI_T;         /* TV 编码 */


#define MSPS_MAX_ESM_MSG_CONTAINER_LEN (UINT16)1024
typedef struct
{
    UINT32 uiLength;
    UINT8  aucValue[MSPS_MAX_ESM_MSG_CONTAINER_LEN];
}MSPS_ESM_MSG_CONTAINER_T;

/* GPRS Timer 24008：10.5.7.3 */
/* GPRS timer information element is to specify GPRS specific timer values,
e.g. for the READY timer. */
typedef struct
{

    BITS8             btUnit:3;
    BITS8             btValue:5;

}NAS_GPRS_TIMER_T;


/*TAI的最大数目(reference 3GPPTS24.301 9.9.326)*/
// #define MME_MAX_TAI_NUM                        9
// typedef struct
// {
// #ifdef BIG_ENDIAN
//     BITS8          btSpare:1;
//  BITS8          btListType:2;
//  BITS8          btTaiNum:5;
// #else
//  BITS8          btTaiNum:5;
//  BITS8          btListType:2;
//  BITS8          btSpare:1;
// #endif
//     MME_TAI_T      astTai[MME_MAX_TAI_NUM];
// }MME_TAI_LIST_T;         /* LV编码 */


/****************GUTI Begin **********************************/

/* referenced from TS24008-820 P82  */
/**************************Packet TMSI(P-TMSI) Begin**********/
/* TMSI 23003：2.4 */
//typedef  UINT32      MME_TMSI_T;  /* 用16进制数表示和使用，0x12345678 */
/* P_TMSI */
//typedef  MME_TMSI_T  MME_PTMSI_T;
/**************************Packet TMSI(P-TMSI) End ***********/
/* M_TMSI */
 typedef  MSPS_TMSI_T  MME_MTMSI_T;
// typedef struct
// {
//  UINT16      usLen ;                         
//  BITS8       btMccDigit1 :4;
//  BITS8       btMccDigit2 :4;
//  BITS8       btMccDigit3 :4;
//  BITS8       btMncDigit3 :4;
//  BITS8       btMncDigit1 :4;
//  BITS8       btMncDigit2 :4;
//  UINT8       ucMmeCode;
//  UINT16      usMmeGroupID;
//  MME_MTMSI_T stMTmsi;
// }MME_GUTI_T;
// 

/************************************************************************/

/*****************************************************************************
*       GUMMEI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119)  
*   47                          23                      7           0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |MCC2|MCC1|MNC3|MCC3|MNC2|MNC1|     mme grp id      |  mme code |
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
// typedef struct{
//  MME_PLMN_T              stPlmn;
//  UINT16                  usMmeGrpId;
//  UINT8                   ucMmeCode;
// }MME_GUMMEI_T;   
/* From mme_common_stru.h   */

                /*****************************************************************************
                *       GUTI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119 )  
                *  65                             42        38        31            0
                *   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
                *   |MCC2|MCC1|MNC3|MCC3|MNC2|MNC1|mme grp id|mme code|     mtmsi   |
                *   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
// typedef struct{  /* 2008/10/06   */
//  MME_GUMMEI_T            stGummei;
//  MME_MTMSI_T         uiMtmsi;
// }MME_GUTI_T;


/************************************************************************/


/* PLMN 23003：12.1 */
// typedef struct
// {
// #ifdef BIG_ENDIAN
//  BITS8  btMcc2:4;
//  BITS8  btMcc1:4;
//  BITS8  btMnc3:4;
//  BITS8  btMcc3:4;
//  BITS8  btMnc2:4;
//  BITS8  btMnc1:4;
//  
// #else
//  BITS8  btMcc1:4;
//  BITS8  btMcc2:4;
//  BITS8  btMcc3:4;
//  BITS8  btMnc3:4;
//  BITS8  btMnc1:4;
//  BITS8  btMnc2:4;   
// #endif
// }MME_PLMN_T;
/* LAI 23003：4.1 */
// typedef struct
// {
//     MME_PLMN_T    stPlmn;
//     UINT16        usLac;
// }MME_LAI_T;


/* Mobile Identity 24008：10.5.1.4 */
// #define MME_MAX_MOBILE_ID_DIGIT_LEN   8
// typedef struct
// {
//     UINT8 ucLength;
// #ifdef BIG_ENDIAN
//  BITS8  btIdDigit1:4;
//  BITS8  btOddEvenIndicator:1;
//  BITS8  btIdentityType:3;
// #else
//  BITS8  btIdentityType:3;
//  BITS8  btOddEvenIndicator:1;
//  BITS8  btIdDigit1:4;
// #endif
//     UINT8 aucIdDigit[MME_MAX_MOBILE_ID_DIGIT_LEN];    /* MAX=8 */
// }MME_MOBILE_ID_T;


/* PLMN列表中的PLMN个数(See subclause 10.5.1.13 in 3GPP TS 24.008[6].) */
// #define MME_MAX_PLMN_NUM                      5
// typedef struct
// {
//     UINT8  ucPlmnNum;
//     MME_PLMN_T  stPlmn[MME_MAX_PLMN_NUM];
// }MME_PLMN_LIST_T;


/****** APN 相关结构定义 BEGIN ******/
// #define MME_MAX_APN_NI_LEN        63    /* APN网络标识部分最大长度 */
// #define MME_MAX_APN_OI_LEN        37    /* APN运营商标识部分最大长度 */
/* APN Network Identifier */
// typedef struct
// {
//     UINT8   ucApnNiLen;               /* [0-63]  0 表示没有NI 部分 */
//     UINT8   aucApnNi[MME_MAX_APN_NI_LEN];
// }MME_APN_NI_T;

/* APN Operator Identifier */
//typedef struct
//{
//    UINT8   ucApnOiLen;             /* [0-37 ]  0 表示没有OI 部分  OI部分实际长度 */
//    UINT8   aucApnOi[MME_MAX_APN_OI_LEN];
//}MME_APN_OI_T;

/* APN */
//typedef struct
//{
//   MME_APN_NI_T    stApnNi;  /* 网络标识 NI    */
//   MME_APN_OI_T    stApnOi;  /* 运营商标识 OI  */
//}MME_APN_T;
/****** APN 相关结构定义 END ******/

/****** TFT 相关结构定义 BEGIN ******/
//typedef union
//{
//    UINT8   ucIpAddr4[4];       /* IPv4   */
//    UINT8   ucIpAddr16[16];     /* IPv6   */
//}MME_GTPC_ADDR_U;

//typedef union
//{
//    UINT8   ucAddrMask4[4];     /* IPv4掩码         */
//    UINT8   ucAddrMask16[16];     /* IPv6掩码         */
//}MME_GTPC_ADDRMASK_U;

/* IPv4 source address type 或 IPv6 source address type */
// typedef struct
// {
//     MME_GTPC_ADDR_U       unIpAddr;
//     MME_GTPC_ADDRMASK_U   unAddrMask;
// }MME_GTPC_SOURCE_ADDR_TYPE_T;

/* Destination port range type 或 Source port range type */
//typedef struct
//{
//    UINT16    usLowLimit;         /* 最低限制       */
//    UINT16    usHighLimit;        /* 最高限制       */
//}MME_GTPC_PORT_RANGE_TYPE_T;

/* Type of service/Traffic class type */
//typedef struct
//{
 //   UINT8   ucTosOrTCType;      /* 类型           */
 //   UINT8   ucMask;         /* 掩码           */
 //   UINT8   aucReserved[2];   /* 保留字节         */
//}MME_GTPC_TOS_OR_TC_TYPE_T;

/* Packet filter contents */
//typedef struct
//{
 //   UINT32    ulSecParaIndex;       /* 安全参数索引     */
 //   UINT16    usSourcePortType;     /* 源端口类型         */
 //   UINT16    usDestPortType;       /* 目的端口类型         */
 //   UINT16    usComponentFlag;      /* 元素存在标识 */
 //   UINT8     aucFlowLabelType[3];  /* Flow Label Type      */
//    UINT8     ucProtocolType;       /* IPv4或IPv6扩展类型  */
//     MME_GTPC_SOURCE_ADDR_TYPE_T   stSourceAddrType;
//     MME_GTPC_PORT_RANGE_TYPE_T    stSourcePortRangeType;
//     MME_GTPC_PORT_RANGE_TYPE_T    stDestPortRangeType;
//     MME_GTPC_TOS_OR_TC_TYPE_T     stTOSorTCType;
// }MME_PACKET_FILTER_CONTENTS_T;

//typedef struct
//{
 //   UINT8   ucPacketFilterId;   /* identifier       */
 //   UINT8   ucPrecedence;     /*  evaluation precedence   */
//    UINT8   ucLength;         /* Length           */
//    UINT8   ucReserved;       /* 保留字节         */
    
    /* Packet filter contents */
 //   MME_PACKET_FILTER_CONTENTS_T  stPacketFilterContent;
//}MME_PACKET_FILTER_T;

/* Traffic Flow Template (TFT) */
//#define MME_GTPC_MAX_PF_NUM       8    /* TFT IE携带packet filter最大个数 */
//typedef struct{
//    UINT8   ucLength;         /* Length of TFT      */
//#ifdef  BIG_ENDIAN
//  BITS8   btOperateCode:3;    /* TFT operation code     */
//  BITS8   btSpare:2;        /* Spare 0          */
//  BITS8   btPacketFilterNum:3;  /* packet filters Num   */
//#else
//  BITS8   btPacketFilterNum:3;  /* packet filters Num   */
//  BITS8   btSpare:2;        /* Spare 0          */
//  BITS8   btOperateCode:3;    /* TFT operation code     */
//#endif
//    UINT8   aucReserved[2];     /* 保留字节         */
    /* Packet filter list */
//    MME_PACKET_FILTER_T     astPacketFilter[MME_GTPC_MAX_PF_NUM];
//}MME_TFT_T;
/****** TFT 相关结构定义 END ******/


/* Protocol Configuration Options */
//typedef struct
//{
 //   UINT8   ucLen;          /* PCO实际长度   */
//    UINT8   aucPco[MME_MAX_PCO_LEN];
//}MME_PCO_T;



/***Bearer Level Quality of Service(Bearer QoS),TAG:80***/

typedef struct 
{
    UINT8    ucQci ;
    UINT8    ucMbrForUpLink ;
    UINT8    ucMbrForDwLink ;
    UINT8    ucGbrForUpLink ;
    UINT8    ucGbrForDwLink ;
    UINT8    ucMbrForUpLink_Extend ;
    UINT8    ucMbrForDwLink_Extend ;
    UINT8    ucGbrForUpLink_Extend ;
    UINT8    ucGbrForDwLink_Extend ;
}MSPS_SDF_QOS_T ;

/*PDN ADDRESS的最大数目*/
#define MSPS_MAX_PDN_ADDR                        12
typedef struct
{
    UINT8  ucLength;
    BITS8  btSpare:5;
    BITS8  btValue:3;
    UINT8  aucAddrInfo[MSPS_MAX_PDN_ADDR];
}MSPS_PDN_ADDR_T;

typedef struct
{
    UINT8  ucLength;
    BITS8  btSpare:1;
    BITS8  btValue:7;
}MSPS_PACKET_FLOW_ID_T;

#define MSPS_AUTH_PARA_RAND_VALUE_LENGTH 16
#define MSPS_AUTH_PARA_AUTN_CONTENTS_LENGTH 16

typedef struct
{
    UINT8 aucRANDValue[MSPS_AUTH_PARA_RAND_VALUE_LENGTH];
}MSPS_AUTH_PARA_RAND_T;/*TS 24.008 10.5.3.1*/

/*UMTS authentication challenge only*/
typedef struct
{
    UINT8 ucLength;
    UINT8 aucAUTNContents[MSPS_AUTH_PARA_AUTN_CONTENTS_LENGTH];
}MSPS_AUTH_PARA_AUTN_T;/*TS 24.008 10.5.3.1.1*/

/*
    the contents's length range is in the FFS,
    now the min is 4, and the max is 16
*/
#define MSPS_AUTH_RSP_PARA_MAX_LENGTH 16
#define MSPS_AUTH_RSP_PARA_MIN_LENGTH 4
typedef struct
{
    UINT8 ucLength;
    UINT8 aucRES[MSPS_AUTH_RSP_PARA_MAX_LENGTH];
}MSPS_AUTH_RSP_PARA_T;/*TS 24.301 9.9.3.4*/





#define MSPS_NAS_MAX_CCB_EBI_N       11
typedef struct{
    UINT8 ucCount;
    UINT8 aucEbi[MSPS_NAS_MAX_CCB_EBI_N];
}MSPS_NAS_CCB_EBI_N_T;




/* 20090506根据24301-111到24301-810添加 */
/* 
  This IE is used to encapsulate the SMS message transferred between the UE and the network . The NAS 
  message container is a type 4 information element with a minimum length of 4 octets and a maximum 
  length of 253 octets . 
  NAS message container contents : This IE can contain an SMS message (i.e. CP-DATA, CP-ACK or CP-ERROR)
  as defined in subclanse 7.2 in 3GPP TS 24.011.
*/
#define MSPS_NAS_MSG_CONTAINER_MAX_LEN   0xFD
typedef struct  
{
    UINT8    ucLen;
    UINT8    aucNasMsgContainer[MSPS_NAS_MSG_CONTAINER_MAX_LEN];
}MSPS_NAS_MSG_CONTAINER_T;


/* Access Point Name */
/* refer to 24008-840 10.5.6.1
   The purpose of the Access point name is to identify the packet data network to which the GPRS user 
   wishes to connect and to notify the access point of the packet data network that wishes to the MS .
   The Accesss point Name is a lable or a fully qualified domain name according to DNS naming convertions .
*/
#define MSPS_NAS_ACCESS_POINT_NAME_MAX_LEN  100
typedef struct  
{
    UINT8   ucLen;
    UINT8   aucAccessPointName[MSPS_NAS_ACCESS_POINT_NAME_MAX_LEN];
}MSPS_ACCESS_POINT_NAME_T;


typedef enum
{
	NAS_UE_RADIO_CAPA_INFO_UPD_NOT_NEED,

	NAS_UE_RADIO_CAPA_INFO_UPD_NEED
}MSPS_UE_RADIO_CAPA_INFO_UPD_NEEDED_FLAG_E;


#define  MSPS_MOBILE_STATION_CLASSMARK2_CODEC_LEN  3
typedef struct  
{
	BITS8    btRfPowerCapa :3;
	BITS8    btA51         :1;
	BITS8    btEsInd       :1;
	BITS8    btRevisionLev :2;
	BITS8    btSpare1      :1;

	BITS8    btFc          :1;
	BITS8    btVgcs        :1;
	BITS8    btVbs         :1;
	BITS8    btSmCapa      :1;
	BITS8    btSsScreenInd :2;
	BITS8    btPsCapa      :1;
	BITS8    btSpare2      :1;

	BITS8    btA52         :1;
	BITS8    btA53         :1;
	BITS8    btCmsp        :1;
	BITS8    btSolsa       :1;
	BITS8    btUcs2        :1;
	BITS8    btLcsvaCap    :1;
	BITS8    btSpare3      :1;
    BITS8    btCm3         :1;

}MSPS_MOBILE_STATION_CLASSMARK2_T;
/*
typedef enum
{
	MSPS_ADDITION_UDP_TYPE_NO_ADDITIONAL_INFO,
	MSPS_ADDITION_UDP_TYPE_SMS_ONLY

}MSPS_ADDITION_UDP_TYPE_E;
*/
#define MSPS_ADDITION_UDP_TYPE_NO_ADDITIONAL_INFO 0
#define MSPS_ADDITION_UDP_TYPE_SMS_ONLY           1
typedef struct  
{
	BITS8  btSpare:7;
	BITS8  btVal:1;
}MSPS_ADDITION_UDP_TYPE_T;

/*
typedef enum
{
MSPS_ADDITION_UDP_RESULT_NO_ADDITIONAL_INFO,
MSPS_ADDITION_UDP_RESULT_CD_FALLBACK_NOT_PREFERRED,
MSPS_ADDITION_UDP_RESULT__SMS_ONLY,
MSPS_ADDITION_UDP_RESULT_RESERVED
}MSPS_ADDITION_UDP_RESULT_E;
*/
#define MSPS_ADDITION_UDP_RESULT_NO_ADDITIONAL_INFO         0
#define MSPS_ADDITION_UDP_RESULT_CD_FALLBACK_NOT_PREFERRED  1
#define MSPS_ADDITION_UDP_RESULT__SMS_ONLY                  2
#define MSPS_ADDITION_UDP_RESULT_RESERVED                   3

typedef struct  
{
	BITS8 btSpare :6;
	BITS8 btVal :2;
}MSPS_ADDITION_UDP_RESULT_T;

typedef enum
{
	MSPS_NAS_PAGING_ID_IMSI,
	MSPS_NAS_PAGING_ID_TMSI

}MSPS_PAGING_ID_E;


typedef struct
{
    BITS8    btSpare:7;
    BITS8    btTmsiFlag:1;  
}NAS_TMSI_STATUS_T;  
/************************************************************************/


/******************************** HEAD FILES PROTECTION Begin ***************************/
#endif
/******************************** HEAD FILES PROTECTION END *****************************/
/******************************** .H FILE ENDS HERE *************************************/
 




