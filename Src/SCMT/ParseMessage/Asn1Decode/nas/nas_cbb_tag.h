/******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
*******************************************************************************
* 文件名称: nas_cbb_tag.h
* 功    能: NAS编解码
* 说    明:
* 修改历史:
* EPS 2008.10.07    吴鹏程 创建基础版本
******************************************************************************/

/******************************** 头文件保护开头 *****************************/
#ifndef NAS_CBB_TAG_H
#define NAS_CBB_TAG_H
/********************************* 头文件包含 ********************************/
#include "nas_cbb_common_stru.h"

extern void cpss_message(const INT8 *pcFormat, ...);

#define NAS_EMM_PROTOCOL  ((UINT8)0x07)
#define NAS_ESM_PROTOCOL  ((UINT8)0x02)

#ifndef NAS_IE_NO_EXIST 
#define NAS_IE_NO_EXIST   ((UINT8)0)
#endif

#ifndef NAS_IE_EXIST
#define NAS_IE_EXIST      ((UINT8)1)
#endif









#define MSPS_SIG_RETURN_SUCCESS                                 ((UINT32)0x00000000) 
#define MSPS_SIG_RETURN_FAILURE                                 ((UINT32)0xFFFFFFFF) 
#define MSPS_SIG_DISCARD                                        ((UINT32)0x00000001) 



#define NAS_CODEC_SUCCESS                                       ((UINT32)0x00000000) 
#define NAS_CODEC_FAILURE                                       ((UINT32)0xFFFFFFFF)
#define NAS_CODEC_CHECK_LEN_BREAK                               ((UINT32)0x00000001)





/************ PS子系统公共消息编号宏定义 BEGIN ************/
/* NAS EMM Messages: 0x41~0x61*/
#define NAS_ATTACH_REQ                                          ((UINT8)0x41)
#define NAS_ATTACH_ACCEPT                                       ((UINT8)0x42)
#define NAS_ATTACH_CMP                                          ((UINT8)0x43)
#define NAS_ATTACH_REJ                                          ((UINT8)0x44)
#define NAS_DETACH_REQ                                          ((UINT8)0x45)
#define NAS_DETACH_ACCEPT                                       ((UINT8)0x46)
#define NAS_TAU_REQ                                             ((UINT8)0x48)
#define NAS_TAU_ACCEPT                                          ((UINT8)0x49)
#define NAS_TAU_CMP                                             ((UINT8)0x4A)
#define NAS_TAU_REJ                                             ((UINT8)0x4B)

#define NAS_EXTENDED_SERVICE_REQ                                ((UINT8)0x4C)     /* 24301-111---->24301-810 添加 */
#define NAS_SERVICE_REJ                                         ((UINT8)0x4E)

#define NAS_SERVICE_REQ                                         ((UINT8)0x4F)     /* 添加SERVICE_REQ，仅用于MME */
#define NAS_GUTI_REALLOC_CMD                                    ((UINT8)0x50)
#define NAS_GUTI_REALLOC_CMP                                    ((UINT8)0x51)
#define NAS_AUTH_REQ                                            ((UINT8)0x52)
#define NAS_AUTH_RSP                                            ((UINT8)0x53)
#define NAS_AUTH_REJ                                            ((UINT8)0x54)
#define NAS_AUTH_FAILURE                                        ((UINT8)0x5C)
#define NAS_ID_REQ                                              ((UINT8)0x55)
#define NAS_ID_RSP                                              ((UINT8)0x56)
#define NAS_SEC_MODE_CMD                                        ((UINT8)0x5D)
#define NAS_SEC_MODE_CMP                                        ((UINT8)0x5E)
#define NAS_SEC_MODE_REJ                                        ((UINT8)0x5F)
#define NAS_EMM_STATUS                                          ((UINT8)0x60)
#define NAS_EMM_INFO                                            ((UINT8)0x61)
// 根据 24301-810 增加     
#define NAS_DL_NAS_TRANSPORT                                    ((UINT8)0x62)
#define NAS_UL_NAS_TRANSPORT                                    ((UINT8)0x63)
#define NAS_CS_SERVICE_NOTIFICATION                             ((UINT8)0x64)

/* ESM Messages: 0xC1~0xE8 */
#define NAS_ACT_DFT_BEARER_CTX_REQ                              ((UINT8)0xC1)
#define NAS_ACT_DFT_BEARER_CTX_ACCEPT                           ((UINT8)0xC2)
#define NAS_ACT_DFT_BEARER_CTX_REJ                              ((UINT8)0xC3)
#define NAS_ACT_DEC_BEARER_CTX_REQ                              ((UINT8)0xC5)
#define NAS_ACT_DEC_BEARER_CTX_ACCEPT                           ((UINT8)0xC6)
#define NAS_ACT_DEC_BEARER_CTX_REJ                              ((UINT8)0xC7)
#define NAS_MOD_BEARER_CTX_REQ                                  ((UINT8)0xC9)
#define NAS_MOD_BEARER_CTX_ACCEPT                               ((UINT8)0xCA)
#define NAS_MOD_BEARER_CTX_REJ                                  ((UINT8)0xCB)
#define NAS_DEACT_BEARER_CTX_REQ                                ((UINT8)0xCD)
#define NAS_DEACT_BEARER_CTX_ACCEPT                             ((UINT8)0xCE)
#define NAS_PDN_CONN_REQ                                        ((UINT8)0xD0)
#define NAS_PDN_CONN_REJ                                        ((UINT8)0xD1)
#define NAS_PDN_DISCONN_REQ                                     ((UINT8)0xD2)
#define NAS_PDN_DISCONN_REJ                                     ((UINT8)0xD3)
#define NAS_BEARER_RES_ALLOC_REQ                                ((UINT8)0xD4)
#define NAS_BEARER_RES_ALLOC_REJ                                ((UINT8)0xD5)
#define NAS_BEARER_RES_RLS_REQ                                  ((UINT8)0xD6)
#define NAS_BEARER_RES_RLS_REJ                                  ((UINT8)0xD7)
#define NAS_ESM_INFO_REQ                                        ((UINT8)0xD9)
#define NAS_ESM_INFO_RSP                                        ((UINT8)0xDA)

#define NAS_ESM_STATUS                                          ((UINT8)0xE8)


/*
  从24301-111到24301-810的协议版本升级中
  Bearer resource release request 更改为 Bearer resource modification request
  Bearer resource release reject  更改为 Bearer resource modification reject
*/
#define NAS_BEARER_RES_MOD_REQ                                  ((UINT8)0xD6)           
#define NAS_BEARER_RES_MOD_REJ                                  ((UINT8)0xD7)


/************ PS子系统公共消息编号宏定义 END ************/


/* 修正 */
#define NAS_BEARER_RES_REL_REQ                                  ((UINT8)0xD6)
#define NAS_BEARER_RES_REL_REJ                                  ((UINT8)0xD7)




/************ PS子系统标准CAUSE编号宏定义 BEGIN *********/
/* EMM标准原因值: 0x02~0x6F */
#define EMM_IMSI_UNKNOWN_IN_HLR                                 ((UINT8)0x02)
#define EMM_ILLEGAL_MS                                          ((UINT8)0x03)
#define IMEI_NOT_ACCEPTED                                       ((UINT8)0x05)
#define EMM_ILLEGAL_ME                                          ((UINT8)0x06)
#define EMM_GPRS_SERVICES_NOT_ALLOWED                           ((UINT8)0x07)
#define EMM_GPRS_NON_GPRS_NOT_ALLOWED                           ((UINT8)0x08)
#define EMM_MS_ID_NOT_DERIVED_BY_NETWORK                        ((UINT8)0x09)
#define EMM_IMPLICITLY_DETACH                                   ((UINT8)0x0A)
#define EMM_PLMN_NOT_ALLOWED                                    ((UINT8)0x0B)
#define EMM_LA_NOT_ALLOWED                                      ((UINT8)0x0C)
#define EMM_ROAM_NOT_ALLOWED                                    ((UINT8)0x0D)
#define EMM_GPRS_SERVICES_NOT_ALLOWED_PLMN                      ((UINT8)0x0E)
#define EMM_NO_SUITABLE_CELL_IN_LA                              ((UINT8)0x0F)
#define EMM_MSC_TMEPORARILY_NOT_REACHABLE                       ((UINT8)0x10)
#define EMM_NETWORK_FAILURE                                     ((UINT8)0x11)
#define EMM_CS_DOMAIN_NOT_AVAILABLE                             ((UINT8)0x12)
#define EMM_ESM_FAILURE                                         ((UINT8)0x13)
#define EMM_MAC_FAILURE                                         ((UINT8)0x14)
#define EMM_SYNCH_FAILURE                                       ((UINT8)0x15)
#define EMM_CONGESTION                                          ((UINT8)0x16)
#define EMM_UE_SECU_CAPA_MISMATCH                               ((UINT8)0x17)
#define EMM_SECU_MOD_REJ_UNSPECIFIED                            ((UINT8)0x18)
#define EMM_NOT_AUTHORIZED_FOR_THIS_CSG                         ((UINT8)0x19)
#define EMM_NO_EPS_AUTH_UNACCEPTABLE                            ((UINT8)0x1A)
#define EMM_CS_DOMAIN_TEMPORARILT_NOT_AVAILABLE                 ((UINT8)0x27)
#define EMM_NO_EPS_BEARER_CTX_ACTIVATED                         ((UINT8)0x28)

#define EMM_SEMANTICALLY_INCORRECT_MSG                          ((UINT8)0x5F)
#define EMM_INVALID_MANDATORY_INFO                              ((UINT8)0x60)
#define EMM_MSG_TYPE_NONEXIST_NOT_IMPLEMENTED                   ((UINT8)0x61)
#define EMM_MSG_TYPE_NOT_COMPATIBLE                             ((UINT8)0x62)
#define EMM_IE_NONEXIST_NOT_IMPLEMENTED                         ((UINT8)0x63)
#define EMM_CONDITIONAL_IE_ERROR                                ((UINT8)0x64)
#define EMM_MSG_NOT_COMPATIBLE                                  ((UINT8)0x65)
#define EMM_PROTOCOL_ERROR_UNSPECIFIED                          ((UINT8)0x6F)

/* ESM标准原因值 */
#define ESM_CAUSE_OPERATOR_DETERMINED_BARRING                   ((UINT8)0x08)
#define ESM_CAUSE_INSUFFICIENT_RESOURCES                        ((UINT8)0x1A)
#define ESM_CAUSE_UNKNOWN_OR_MISSING_APN                        ((UINT8)0x1B)
#define ESM_CAUSE_UNKNOWN_PDN_TYPE                              ((UINT8)0x1C)
#define ESM_CAUSE_USER_AUTH_FAILED                              ((UINT8)0x1D)
#define ESM_CAUSE_ACTIVATION_REJ_BY_SGW_OR_PGW                  ((UINT8)0x1E)
#define ESM_CAUSE_ACTIVATION_REJ_UNSPECIFIED                    ((UINT8)0x1F)
#define ESM_CAUSE_SERVICE_OPTION_NOT_SUPPORTED                  ((UINT8)0x20)
#define ESM_CAUSE_REQUESTED_SERVICE_OPTION_NOT_SUBSCRIBED       ((UINT8)0x21)
#define ESM_CAUSE_SERVICE_OPTION_TEMPORARILY_OUT_OF_ORDER       ((UINT8)0x22)
#define ESM_CAUSE_PTI_ALREADY_IN_USE                            ((UINT8)0x23)
#define ESM_CAUSE_REGULAR_DEACTIVATION                          ((UINT8)0x24)
#define ESM_CAUSE_EPS_QOS_NOT_ACCEPTED                          ((UINT8)0x25)
#define ESM_CAUSE_NETWORK_FAIL                                  ((UINT8)0x26)
#define ESM_CAUSE_REACTIVATION_REQUESTED                        ((UINT8)0x27)
#define ESM_CAUSE_FEATURE_NOT_SUPPORTED                         ((UINT8)0x28)
#define ESM_CAUSE_SEMANTIC_ERROR_IN_THE_TFT_OPERATION           ((UINT8)0x29)
#define ESM_CAUSE_SYNTACTICAL_ERROR_IN_THE_TFT_OPERATION        ((UINT8)0x2A)
#define ESM_CAUSE_UNKNOWN_EPS_BEARER_CTX                        ((UINT8)0x2B)
#define ESM_CAUSE_SEMANTIC_ERRORS_IN_PACKET_FILTER              ((UINT8)0x2C)
#define ESM_CAUSE_SYNTACTICAL_ERRORS_IN_PACKET_FILTER           ((UINT8)0x2D)
#define ESM_CAUSE_EPS_BEARER_CTX_WITHOUT_TFT_ALREADY_ACTIVATED  ((UINT8)0x2E)
#define ESM_CAUSE_PTI_MISMATCH                                  ((UINT8)0x2F)
#define ESM_CAUSE_LAST_PDN_DISCONNECTION_NOT_ALLOWED            ((UINT8)0x31)
#define ESM_CAUSE_PDN_TYPE_IPV4_ONLY_ALLOWED                    ((UINT8)0x32)
#define ESM_CAUSE_PDN_TYPE_IPV6_ONLY_ALLOWED                    ((UINT8)0x33)
#define ESM_CAUSE_SINGLE_ADDR_BEARERS_ONLY_ALLOWED              ((UINT8)0x34)
#define ESM_CAUSE_ESM_INFO_NOT_RECEIVED                         ((UINT8)0x35)
#define ESM_CAUSE_PDN_CONNECTION_DOES_NOT_EXIST                 ((UINT8)0x36)
#define ESM_CAUSE_MULTIPLE_PDN_CONN_FOR_A_GIVEN_APN_NOT_ALLOWED ((UINT8)0x37)
#define ESM_CAUSE_COLLISION_WITH_NETWORK_INITIATED_REQ          ((UINT8)0x38)
#define ESM_CAUSE_INVALID_PTI_VALUE                             ((UINT8)0x51)
#define ESM_CAUSE_SEMANTICALLY_INCORRECT_MSG                    ((UINT8)0x5F)
#define ESM_CAUSE_INVALID_MANDATORY_INFO                        ((UINT8)0x60)
#define ESM_CAUSE_MSG_TYPE_NON_EXISTENT_OR_NOT_IMPLEMENTED      ((UINT8)0x61)
#define ESM_CAUSE_MSG_TYPE_NOT_COMPATIBLE_WITH_PROTOCOL_STATE   ((UINT8)0x62)
#define ESM_CAUSE_IE_NON_EXISTENT_OR_NOT_IMPLEMENTED            ((UINT8)0x63)
#define ESM_CAUSE_CONDITIONAL_IE_ERROR                          ((UINT8)0x64)
#define ESM_CAUSE_MSG_NOT_COMPATIBLE_WITH_THE_PROTOCOL_STATE    ((UINT8)0x65)
#define ESM_CAUSE_PROTOCOL_ERROR_UNSPECIFIED                    ((UINT8)0x6F)
#define ESM_CAUSE_APN_RESTRICTION_VALUE_INCOMPATIBLE_WITH_ACTIVE_EPS_BEARER_CTX    ((UINT8)0x70)
/* 
Any other value received by the UE shall be treated as 0010 0010,"Service option temporarily out of order".
Any other value received by the network shall be treated as 0110 1111,"Protocol error,unspecified".
*/


/************ PS子系统标准CAUSE编号宏定义 END ************/




/************************IE TYPE*********************/


#define NAS_PCO_TAG                                             ((UINT8)(0x27))
#define NAS_AUTH_FAIL_PARA_TAG                                  ((UINT8)(0x30))

#define NAS_TFT_TAG                                             ((UINT8)(0x36))
#define NAS_PLMN_LIST_TAG                                       ((UINT8)(0x4A))
#define NAS_T3412_TAG                                           ((UINT8)(0x5A))
#define NAS_GUTI_TAG                                            ((UINT8)(0x50))
#define NAS_TAI_TAG                                             ((UINT8)(0x52))
#define NAS_EMM_CAUSE_TAG                                       ((UINT8)(0x53))
#define NAS_TAI_LIST_TAG                                        ((UINT8)(0x54))
#define NAS_NONCE_UE_TAG                                        ((UINT8)(0x55))
#define NAS_NONCE_MME_TAG                                       ((UINT8)(0x56))
#define NAS_EPS_BEARER_CTX_STATUS_TAG                           ((UINT8)(0x57))
#define NAS_ESM_CAUSE_TAG                                       ((UINT8)(0x58))
#define NAS_SDF_QOS_TAG                                         ((UINT8)(0x5B))
#define NAS_PDN_ADDR_TAG                                        ((UINT8)(0x59))
#define NAS_APN_TAG                                             ((UINT8)(0x28))
/* Begin wangxiaofeng modify 2012-1-8 */
#define NAS_NEG_QOS_TAG                                         ((UINT8)(0x30))
#define NAS_NEG_llC_SAPI_TAG                                    ((UINT8)(0x32))
#define NAS_PACKET_FLOWID_TAG                                   ((UINT8)(0x34))
/* Begin wangxiaofeng modify 2012-1-8 */
#define NAS_ESM_CONTAINER_TAG                                   ((UINT8)(0x78))
#define NAS_ESM_INFO_TRAN_FLAG_TAG                              ((UINT8)(0xD0))   
#define NAS_DRX_PATA_TAG                                        ((UINT8)(0x5C))
#define NAS_UE_NETWORK_CAPA_TAG                                 ((UINT8)(0x58))
#define NAS_MOBILE_STATION_CLASSMARK2_TAG                       ((UINT8)(0x11)) 
#define NAS_LOCA_AREA_ID_TAG                                    ((UINT8)(0x13))
#define NAS_MOBILE_ID_TAG                                       ((UINT8)(0x23))
#define NAS_EMM_CAUSE_TAG                                       ((UINT8)(0x53))
#define NAS_T3402_TAG                                           ((UINT8)(0x17))
#define NAS_T3423_TAG                                           ((UINT8)(0x59))
/* Begin wangxiaofeng modify 2012-1-7 */
#define NAS_TI_TAG                                             ((UINT8)(0x5d))
/* End wangxiaofeng modify 2012-1-7 */
/* add by qiylong 2013-01-25 begin */
#define NAS_RD_TAG                                              ((UINT8)(0x80))
/* add by qiylong 2013-01-25 end */
#define NAS_APN_AMBR_TAG                                        ((UINT8)(0x5e))

#define NAS_IMEISV_TAG                                          ((UINT8)(0x23)) 
#define NAS_IMEISV_REQ_TAG                                      ((UINT8)(0xC0))
 
#define NAS_UE_RADIO_CAPA_INFO_UPD_NEEDED_FLAG                  ((UINT8)(0xA0))

#define NAS_ADDTIONAL_UPD_TYPE_TAG                              ((UINT8)(0xF0))
#define NAS_TMSI_STATUS_TAG                                     ((UINT8)(0x90))
#define NAS_KSI_TAG                                             ((UINT8)(0xB0))
#define NAS_CIPHERING_KEY_SN_TAG                                ((UINT8)(0x80))
#define NAS_PTMSI_SIGN_TAG                                      ((UINT8)(0x19))
#define NAS_EPS_MOBI_ID_TAG                                     ((UINT8)(0x50))   /* 为兼容以前的，暂不对前边的GUTI做修改 */
#define NAS_MS_NETWORK_CAPA_TAG                                 ((UINT8)(0x31))   /* 为兼容以前的，暂不对前边的GUTI做修改 */
#define NAS_UE_RADIO_CAPA_INFO_UPD_NEEDED_TAG                   ((UINT8)(0xA0))

/***************************************************/
#define NAS_IMEISV_REQUESTED                                    ((UINT8)(0x01))
#define NAS_IMEISV_NOT_REQUESTED                                ((UINT8)(0x00))
/***************EPS Attach Result**************************/
#define NAS_ATTA_RESULT_EPS_ONLY                                ((UINT8)(0x01))
#define NAS_ATTA_RESULT_COMBINED_EPS_IMSI_ATTA                  ((UINT8)(0x02))


/************************************************************************/
/*                         Identity type 2                              */
/************************************************************************/
#define MSPS_NAS_ID_TYPE2_IMSI       ((UINT8)0x01)
#define MSPS_NAS_ID_TYPE2_IMEI       ((UINT8)0x02)
#define MSPS_NAS_ID_TYPE2_IMEISV     ((UINT8)0x03)
#define MSPS_NAS_ID_TYPE2_TMSI       ((UINT8)0x04)
/************************************************************************/
/*                                                                      */
/************************************************************************/

/************ EMM模块公共消息信元宏定义 BEGIN ************/

/* PLMN列表中的PLMN个数(See subclause 10.5.1.13 in 3GPP TS 24.008[6].) */
#define MSPS_MAX_PLMN_NUM                                       ((UINT8)(0x05))

/*MME_AUTH_RSP_RARA_EXT_T结构中使用(reference SGSN)*/
#define MSPS_MAX_RES_EXT_LEN                                    ((UINT8)12)

/* 层3消息的最大长度(reference SGSN) */
#define MSPS_MAX_NAS_MSG_LEN                                    ((UINT8)252)

/*ESM消息容器的最大值(reference 3GPP TS24.301 9.9.3.12)*/
#define MSPS_MAX_ESM_MSG_CONTAINER_NUM                          ((UINT32)32768)


/*NAS消息长度的最大值(自己定义的）*/
/*
#define MME_MAX_NAS_MSG_LEN                    512
*/
/*PCO IE最大长度(see subclause 10.5.6.3 in  3GPP TS24.008[6].)*/
//#define MME_MAX_PCO_LEN                        253 

/*TAI的最大数目(reference 3GPPTS24.301 9.9.326)*/
#define MSPS_MAX_TAI_NUM                                        ((UINT8)(0x09))

/************ EMM 模块公共消息信元宏定义 END ************/

/************ SM模块公共消息信元宏定义 BEGIN ************/
#define MSPS_MAX_TFT_LEN                                        ((UINT8)255)   /* TFT 内容最大长度 (reference3GPPTS24.008)*/
/************ SM模块公共消息信元宏定义 END ************/

/************ GSMS模块公共消息相关宏定义 BEGIN ************/

/* PD值与业务的对应 MME_NAS_HEADER_T中btPd */
#define  MOBILITY_MGMNT_MSG                                     ((UINT8)5)
#define  GPRS_MOBILITY_MGMNT_MSG                                ((UINT8)8)
#define  GPRS_SESSION_MGMNT_MSG                                 ((UINT8)10)
/************ GSMS模块公共消息相关宏定义 END ************/ 

/******************************** 类型定义 ******************************/

#define  MSPS_NAS_ATT_TYPE_INIT_EPS                    ((UINT8)0x1)        /*added by wangbin 2008/11/13*/
#define  MSPS_NAS_ATT_TYPE_EPS_IMSI                    ((UINT8)0x2)        /*added by wangbin 2008/11/13*/
#define  MSPS_NAS_ATT_TYPE_HO_EPS                      ((UINT8)0x3)        /*added by wangbin 2008/11/13*/
#define  MSPS_NAS_ATT_TYPE_HO_EPS_IMSI                 ((UINT8)0x4)        /*added by wangbin 2008/11/13*/

#define  MSPS_NAS_ATT_RET_EPS_ONLY                     ((UINT8)0x01)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_ATT_RET_EPS_IMSI                     ((UINT8)0x02)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_NETWORK_REATTACH             ((UINT8)0x1)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_NETWORK_NO_REATTACH          ((UINT8)0x2)        /*added by wangbin 2008/11/5*/

#define  MSPS_NAS_DTT_TRG_UE_NO_SWITCHOFF_EPS          ((UINT8)0x1)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_UE_NO_SWITCHOFF_IMSI         ((UINT8)0x2)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_UE_NO_SWICHOFF_EPS_IMSI      ((UINT8)0x3)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_UE_SWITCHOFF_START           ((UINT8)0x8)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_UE_SWITCHOFF_EPS             ((UINT8)0x9)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_UE_SWITCHOFF_IMSI            ((UINT8)0xa)        /*added by wangbin 2008/11/5*/
#define  MSPS_NAS_DTT_TRG_UE_SWITCHOFF_EPS_IMSI        ((UINT8)0xb)        /*added by wangbin 2008/11/5*/ 

#define NAS_CODEC_IPV4                                  ((UINT8)(1))
#define NAS_CODEC_IPV6                                  ((UINT8)(2))
#define NAS_CODEC_IPV4IPV6                              ((UINT8)(3))
#define NAS_MAX_PF_NUM                                  ((UINT8)0x08)   /* TFT IE携带packet filter最大个数 */




#define  MSPS_NAS_EXIST                                ((UINT8)0x01)
#define  MSPS_NAS_NO_EXIST                             ((UINT8)0x00)




/********************************************TFT BEGIN******************************************************/


/*
<btOperateCode> 的取值范围如下：
000 spare :
001 Creat new TFT
010 Delete existing TFT
011 Add packet filters to existing TFT
100 Replace packet filters in existing TFT 
101 Delete packet filters from existing TFT
110 No TFT operation
111 Reserved 



0x06  No TFT operation
--------------->shall be used if a parameters list is include but no packet filter list is included in 
the traffic flow template information element.


For the "delete existing TFT"operation and the "no TFT operation",the packet filter list shall be empty.

For the "delete packet filters from existing TFT" operation,the packet filter shall contain a variable 
of packet filter identifiers . This number shall be derived from the coding of the number of packet filters
field in octer 3.

For the "creat new TFT","add packet filters to existing TFT"and "replace packet filters in existing TFT" 
operations, the packet filter list shall contain a variable number of packet filters. This number shall 
be derived from coding of the number of packet filter field in octer 3 .

The packet filter identifier field is used to identify each packet filter in a TFT. 

The packet filter direction is used to indicate .,for what traffic direction the filter applies.
00 ---> pre Rel-7 TFT filter
01 ---> downlink only
10 ---> uplink only 
11 ---> bidirectional

  
Packet filter component type identifier
<usComponentFlag>:
0x10 ----> IPV4 remote address type
           The packet filter component value shall be encoded as a sequence of a four octet IPV4 
           address field and a four octet IPV4 address mask field . The IPV4 address field shall 
           be transmitted first .

0x20 ----> IPV6 remote address type
           The packet filter component value shall be encoded as a sequence of a four octet IPV6 
           address field and a four octet IPV6 address mask field . The IPV6 address field shall 
           be transmitted first .

0x30 ----> Protocol identifier/Next header type
           The packet filter component value field shall be encoded as one octet which specifies 
           the IPV4 protocol identifier or IPV6 next header

0x40 ----> Single local port type
0x50 ----> Single remote port type 
           The packet filter component value field shall be encoded as two octet which specifies a 
           port number 

0x41 ----> Local port range type
0x51 ----> Remote port range type
           The packet filter component value field shall be encoded as a sequence of a two octet
           port range low limit field and a two octet port range high limit field. The port range 
           low limit filed shall be transmitted first 


0x60 ----> Security parameter index type
           The packet filter component value field shall be encoded as four octet which specifies 
           the IPSec security parameter index .

0x70 ----> Type of service/Traffic class type
           The packet filter component value field shall be encoded as a one octet Type of 
           service/Traffic class field and a one octet Type of service/Traffic class mask field .
           The Type of service/Traffic class field shall be transmitted first .

0x80 ----> Flow label type
           The packet filter component value field shall be encoded as three octet which specifies 
           the IPV6 flow label . The bits 8 through 5 of the first octet shall contain the IPV6 
           flow lable .



*/

/****** TFT 相关结构定义 BEGIN ******/
/*
typedef union
{
    UINT8   ucIpAddr4[4];        
    UINT8   ucIpAddr16[16];       
}SWP_GTPC_ADDR_U;
typedef union
{
    UINT8   ucAddrMask4[4];       
    UINT8   ucAddrMask16[16];    
}SWP_GTPC_ADDRMASK_U;

typedef struct
{
    SWP_GTPC_ADDR_U       unIpAddr;
    SWP_GTPC_ADDRMASK_U   unAddrMask;
}SWP_GTPC_SOURCE_ADDR_TYPE_T;

typedef struct
{
    UINT16    usLowLimit;         
    UINT16    usHighLimit;        
}SWP_GTPC_PORT_RANGE_TYPE_T; 

typedef struct
{
    UINT8   ucTosOrTCType;         
    UINT8   ucMask;                
    UINT8   aucReserved[2];        
}SWP_GTPC_TOS_OR_TC_TYPE_T;

typedef struct
{
    UINT32    ulSecParaIndex;      
    UINT16    usSourcePortType;    
    UINT16    usDestPortType;      
    UINT16    usComponentFlag;      
    UINT8     aucFlowLabelType[3]; 
    UINT8     ucProtocolType;      
    SWP_GTPC_SOURCE_ADDR_TYPE_T   stSourceAddrType;
    SWP_GTPC_PORT_RANGE_TYPE_T    stSourcePortRangeType;
    SWP_GTPC_PORT_RANGE_TYPE_T    stDestPortRangeType;
    SWP_GTPC_TOS_OR_TC_TYPE_T     stTOSorTCType;
}SWP_PACKET_FILTER_CONTENTS_T;

typedef struct
{
    UINT8   ucPacketFilterId;      
    UINT8   ucPrecedence;           
    UINT8   ucLength;             
    UINT8   ucReserved;            
    
    SWP_PACKET_FILTER_CONTENTS_T  stPacketFilterContent;
}SWP_PACKET_FILTER_T;

#define SWP_GTPC_MAX_PF_NUM     8  
typedef struct{
    UINT8   ucLength;             
#ifdef  BIG_ENDIAN
    BITS8   btOperateCode:3;   
    BITS8   btSpare:2;         
    BITS8   btPacketFilterNum:3;
#else
    BITS8   btPacketFilterNum:3;
    BITS8   btSpare:2;          
    BITS8   btOperateCode:3;   
#endif
    UINT8   aucReserved[2];        
    SWP_PACKET_FILTER_T     astPacketFilter[SWP_GTPC_MAX_PF_NUM];
}MSPS_TFT_T;
*/
/****** TFT 相关结构定义 END ******/

/********************************************TFT BEGIN******************************************************/

/************TFT相关宏定义*************/
/* GTPC对TFT编解码时用到的宏 */
#define MSPS_CODEC_FLT_NUM                                      (0x03)
/* Packet filter compoment flag */
#define MSPS_CODEC_IPV4_SRC_ADDRT                               (0x01)
#define MSPS_CODEC_IPV6_SRC_ADDRT                               (0x02)
#define MSPS_CODEC_PID_HDRT                                     (0x04)
#define MSPS_CODEC_DEST_PORTT                                   (0x08) 
#define MSPS_CODEC_DEST_PORT_RANGET                             (0x10)
#define MSPS_CODEC_SRC_PORTT                                    (0x20)
#define MSPS_CODEC_SRC_PORT_RANGET                              (0x40)
#define MSPS_CODEC_SECU_INDEXT                                  (0x80)
#define MSPS_CODEC_SRVT                                         (0x0100)
#define MSPS_CODEC_FLOW_LABLT                                   (0x0200) 

/* Packet filter compoment flag 取反。*/
#define MSPS_CODEC_IPV4_SRC_ADDRT_NON                           (0xFFFE)
#define MSPS_CODEC_IPV6_SRC_ADDRT_NON                           (0xFFFD)
#define MSPS_CODEC_PID_HDRT_NON                                 (0xFFFB)
#define MSPS_CODEC_DEST_PORTT_NON                               (0xFFF7) 
#define MSPS_CODEC_DEST_PORT_RANGET_NON                         (0xFFEF)
#define MSPS_CODEC_SRC_PORTT_NON                                (0xFFDF)
#define MSPS_CODEC_SRC_PORT_RANGET_NON                          (0xFFBF)
#define MSPS_CODEC_SECU_INDEXT_NON                              (0xFF7F)
#define MSPS_CODEC_SRVT_NON                                     (0xFEFF)
#define MSPS_CODEC_FLOW_LABLT_NON                               (0xFDFF)

/* Packet filter compoment id */
#define MSPS_CODEC_IPV4_SRC_ADDRT_ID                            (0x10)    
#define MSPS_CODEC_IPV6_SRC_ADDRT_ID                            (0x20)
#define MSPS_CODEC_PID_HDRT_ID                                  (0x30)    
#define MSPS_CODEC_DEST_PORTT_ID                                (0x40)        
#define MSPS_CODEC_DEST_PORT_RANGET_ID                          (0x41)    
#define MSPS_CODEC_SRC_PORTT_ID                                 (0x50)
#define MSPS_CODEC_SRC_PORT_RANGET_ID                           (0x51)    
#define MSPS_CODEC_SECU_INDEXT_ID                               (0x60)    
#define MSPS_CODEC_SRVT_ID                                      (0x70)
#define MSPS_CODEC_FLOW_LABLT_ID                                (0x80)

/* User End Address */
#define MSPS_CODEC_IPV4_LEN                                     (0x04)
#define MSPS_CODEC_IPV6_LEN                                     (0x10)
#define MSPS_CODEC_PDP_LEN                                      (0x01)

/* 特殊字段的长度 */
#define MSPS_CODEC_IE_ITEM_LEN16                                (0x02)
#define MSPS_CODEC_IE_ITEM_LEN32                                (0x04)

#define MSPS_CODEC_TLV_LEN_I                                    (0x01)
#define MSPS_CODEC_TWO_OCTET                                    (0x02)

/* TFT Operations */
#define MSPS_CODEC_SPARE                                         0
#define MSPS_CODEC_CREATE_TFT                                    1
#define MSPS_CODEC_DELETE_TFT                                    2
#define MSPS_CODEC_ADD_PFS                                       3
#define MSPS_CODEC_RELACE_PFS                                    4
#define MSPS_CODEC_DELETE_PFS                                    5
#define MSPS_CODEC_NO_TFT                                        6
#define MSPS_CODEC_RESERVE_TFT                                   7

#define MSPS_CODEC_TRUE                                         (0x01)
#define MSPS_CODEC_FALSE                                        (0x00)

#define MSPS_CODEC_TLV_LEN_II                                   ((UINT8)0x02)
#define MSPS_CODEC_ZERO                                         ((UINT8)0x00)
#define MSPS_CODEC_ONE_OCTET                                    ((UINT8)0x01)




/*  NAS security algorithms  */
/*************************************************************************************/
/* spare | type of ciphering algorithm | spare |type of integrity protection algorithm                                                                  */
/*************************************************************************************/
/* type of integrity protection algorithm */
#define EPS_INTEGRITY_ALGORITHM_128_EIA0                        ((UINT8)0x00)     
#define EPS_INTEGRITY_ALGORITHM_128_EIA1                        ((UINT8)0x01)       /*SNOW 3G */
#define EPS_INTEGRITY_ALGORITHM_128_EIA2                        ((UINT8)0x02)       /*AES*/
#define EPS_INTEGRITY_ALGORITHM_EIA3                            ((UINT8)0x03) 
#define EPS_INTEGRITY_ALGORITHM_EIA4                            ((UINT8)0x04) 
#define EPS_INTEGRITY_ALGORITHM_EIA5                            ((UINT8)0x05) 
#define EPS_INTEGRITY_ALGORITHM_EIA6                            ((UINT8)0x06) 
#define EPS_INTEGRITY_ALGORITHM_EIA7                            ((UINT8)0x07) 

/* type of ciphering algorithm */   /* 目前EPS协议中确定实现支持的加密算法：0，1，2 */
#define EPS_ENCRYPTION_ALGORITHM_128_EEA0                       ((UINT8)0x00)     /* ciphering not used */
#define EPS_ENCRYPTION_ALGORITHM_128_EEA1                       ((UINT8)0x01)     /* snow 3G */
#define EPS_ENCRYPTION_ALGORITHM_128_EEA2                       ((UINT8)0x02)     /* AES */
#define EPS_ENCRYPTION_ALGORITHM_EEA3                           ((UINT8)0x03) 
#define EPS_ENCRYPTION_ALGORITHM_EEA4                           ((UINT8)0x04) 
#define EPS_ENCRYPTION_ALGORITHM_EEA5                           ((UINT8)0x05) 
#define EPS_ENCRYPTION_ALGORITHM_EEA6                           ((UINT8)0x06) 
#define EPS_ENCRYPTION_ALGORITHM_EEA7                           ((UINT8)0x07)


/* security type */
#define NAS_NO_SECU_PROTECTED                                       ((UINT8)(0x00))
#define NAS_INTEGRITY_PROTECTED                                     ((UINT8)(0x01))
#define NAS_INTEGRITY_PROTECTED_CIPHERED                            ((UINT8)(0x02))
#define NAS_INTEGRITY_PROTECTED_WITH_NEW_EPS_SECU_CTX               ((UINT8)(0x03))
#define NAS_INTEGRITY_PROTECTED_AND_CIPHERED_WITH_NEW_EPS_SECU_CTX  ((UINT8)(0x04))  
#define NAS_SECU_FOR_SERVICE_REQ                                    ((UINT8)(0x0C))


#define NAS_CODEC_ZERO                                           (0x00)
/* APN标签中的分隔符 */
#define NAS_CODEC_SIG_STOP                                       (0x2E)
#define NAS_CODEC_ONE_OCTET                                      (0x01)
/* APN OI的结束符的位置 */
#define NAS_CODEC_APN_OI_BACK_LEN                                (0x04)
#define NAS_CODEC_APN_NIOI_MIN_LEN                               (0x15) 
#define NAS_CODEC_APN_OI_LEN                                     (0x13)

/* FLAG的标志 */
#define NAS_CODEC_TRUE                                           (0x01)
#define NAS_CODEC_FALSE                                          (0x00)


#define NAS_CODEC_ONT_OCTET                                      ((UINT8)0x01)
#define NAS_CODEC_TWO_OCTET                                      ((UINT8)0x02)
#define NAS_CODEC_THREE_OCTET                                    ((UINT8)0x03)
#define NAS_CODEC_FOUR_OCTET                                     ((UINT8)0x04)
#define NAS_CODEC_FIVE_OCTET                                     ((UINT8)0x05)
#define NAS_CODEC_SIX_OCTET                                      ((UINT8)0x06)
#define NAS_CODEC_BUFF_MAX_LEN                                   (10*1024)

#define NAS_COUNT_3F                                             (0x00000FFF)
#define NAS_COUNT_2F                                             (0x000000FF)


#define NAS_IP_ADDR_IPV4                                         (0x01)
#define NAS_IP_ADDR_IPV6                                         (0x02)
#define NAS_IP_ADDR_IPV4V6                                       (0x03)

#define NAS_NO_NEED_INTEGRITY                                    (0x00) 
#define NAS_NEED_INTEGRITY                                       (0x01) 
#define NAS_NO_NEED_CIPHER                                       (0x00)
#define NAS_NEED_CIPHER                                          (0x01) 

#define NAS_CODEC_TEMP_BUFF_MAX_LEN                              (1024*10)
#define NAS_MSG_TYPE_C0                                          (0xC0)


#define NAS_EPS_MOBILE_ID_ODD                                    (0x01)  /*奇数*/
#define NAS_EPS_MOBILE_ID_EVEN                                   (0x00)  /*偶数*/

#define NAS_CODEC_COUNTER_OFFSET                                 5



/**********************************错误返回值******************************/
#define MSPS_NAS_MOBILE_ID_TYPE_NULL                             ((UINT8)0x00)
#define MSPS_NAS_MOBILE_ID_TYPE_IMSI                             ((UINT8)0x01)
#define MSPS_NAS_MOBILE_ID_TYPE_IMEI                             ((UINT8)0x02)
#define MSPS_NAS_MOBILE_ID_TYPE_IMEISV                           ((UINT8)0x03)
#define MSPS_NAS_MOBILE_ID_TYPE_TMSI_PTMSI_MTMSI                 ((UINT8)0x04)
#define MSPS_NAS_MOBILE_ID_TYPE_TMGI_MBMS_SESSION_ID             ((UINT8)0x05)
/**************************************************************************/
#define BUFF_STRUCT 0
#define BUFF_RAW    1

/**************************************************************************/

/******************************** HEAD FILES PROTECTION Begin ***************************/
#endif
/******************************** HEAD FILES PROTECTION END *****************************/
/******************************** .H FILE ENDS HERE *************************************/



