
/******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
*******************************************************************************
* File Name: nas_cbb_msg.h 
* Description:  GTPC模块的IE TAG 以及 消息TAG，消息定义等.
* Note: 
* History: 
*  EPS.MME 0.01 2008.09.04 吴鹏程  创建基础版本
******************************************************************************/

/******************************** HEAD FILES PROTECTION BEGIN*****************************/
#ifndef  NAS_CBB_MSG_H
#define  NAS_CBB_MSG_H
/******************************** HEAD FILES PROTECTION  BEGIN*****************************/
#include "nas_cbb_ie.h"
/*************************MSG Defination**********************************/

/************ EMM模块公共消息相关结构定义 BEGIN ************/
#define  NAS_PTMSI_SIGN_LEN 3
/****************ATTACH BEGIN***************/
/* Attach Request Message */
typedef struct
{   
    /*EMM消息头*/
    MSPS_NAS_EMM_HEADER_T          stEmmHeader;                         

    /* Mandatory IE*/
    BITS8                          btEpsAttachType:4;
    BITS8                          btNasKeySetId:4;
    MSPS_EPS_MOBILE_ID_T           stOldGutiOrImsi;
    MSPS_UE_NETWORK_CAPA_T         stUeNetworkCapa;
    MSPS_ESM_MSG_CONTAINER_T       stEsmMsgContainer;

    UINT8                              ucAdditinalGutiExist;
    MSPS_GUTI_T                        stAdditinalGuti;

    /*  Optional IE */
    /* add by zhangbin 2011-12-28 begin */
    UINT8                          ucOldPtmsiSignExist;
    UINT8                          aucOldPtmsiSign[NAS_PTMSI_SIGN_LEN];
    /* add by zhangbin 2011-12-28 end */
    UINT8                          ucTaiExist;
    MSPS_TAI_T                     stLastTai;
    UINT8                          ucDrxParaExist;
    MSPS_DRX_PARA_T                stDrxPara;

	/* 20111228 WUPENGCHENG ADD BEGIN */
    UINT8                          ucMsNetworkCapaExist;
    MSPS_MS_NETWORK_CAPA_T         stMsNetworkCapa;
     /* 20111228 WUPENGCHENG ADD END */

	UINT8                          ucOldLaiExist;
	MSPS_LAI_T                     stOldLai;

	UINT8                             ucMobileStationClassMark2Exist;
	MSPS_MOBILE_STATION_CLASSMARK2_T  stMobileStationClassMark2;

	UINT8                          ucAdditionalUdpTypeExist;
	MSPS_ADDITION_UDP_TYPE_T       stAdditionalUdpType;

	UINT8                             ucTmsiStatusExist;
	NAS_TMSI_STATUS_T                 stTmsiStatus;

}NAS_ATTACH_REQ_T;


/*ATTACH ACCEPT*/
typedef struct
{
    /* EMM消息头 */
    MSPS_NAS_EMM_HEADER_T          stEmmHeader;
    /* Mandatory IE*/
    BITS8                          btAttachResult   :4;
    BITS8                          btSpareHalfOctet :4;
    NAS_GPRS_TIMER_T               stT3412Timer;
    MSPS_TAI_LIST_T                stTaiList;
    MSPS_ESM_MSG_CONTAINER_T       stEsmMsgContainer;
    /*  Optional IE */
    UINT8                          ucGutiExist ;
    MSPS_GUTI_T                    stGuti;

    UINT8                          ucEPlmnExist;
    MSPS_PLMN_LIST_T               stEPlmn;


	UINT8                          ucOldLaiExist;
	MSPS_LAI_T                     stOldLai;

	UINT8                          ucMsIdExist;
	MSPS_MOBILE_ID_T               stMsId;

	UINT8                          ucAdditionalUdpRltExist;
	MSPS_ADDITION_UDP_RESULT_T     stAdditionalUdpRlt;

	/* 20101109 wupengcheng add */
	UINT8                          ucEmmCauseExist;
	UINT8                          ucEmmCause;

}NAS_ATTACH_ACCEPT_T;


/*Authentication Failure*/
#define NAS_AUTH_FAIL_AUTS_MAX_LEN    ((UINT8)0x0E)
typedef struct
{
    MSPS_NAS_EMM_HEADER_T           stEmmHdr;
    UINT8                           ucEMMCause;   /*Refered by 8.2.5.1*/
    UINT8                           ucAutsExist;
    UINT8                           aucAuts[NAS_AUTH_FAIL_AUTS_MAX_LEN];
}NAS_AUTH_FAILURE_T;

typedef struct
{
    MSPS_NAS_EMM_HEADER_T           stEmmHdr;
}NAS_AUTH_REJ_T;

typedef struct
{
    MSPS_NAS_EMM_HEADER_T           stEmmHdr;
    BITS8                           btNasKeySetId_Asme :4; /*NAS Key Set Identifier*/
    BITS8                           btSpare            :4;  
    MSPS_AUTH_PARA_RAND_T           stRANDPara;            /*Authentication parameter RAND:EPS Challenge*/
    MSPS_AUTH_PARA_AUTN_T           stAUTNPara;            /*Authentication parameter AUTN:EPS Challenge*/
}NAS_AUTH_REQ_T;

typedef struct
{
    MSPS_NAS_EMM_HEADER_T           stEmmHdr;
    MSPS_AUTH_RSP_PARA_T            stRspPara;            /*Authentication Response parameter*/
}NAS_AUTH_RSP_T;
/*============================Authentication Related end===============================*/
/*ATTACH COMPLETE*/
typedef struct
{
    MSPS_NAS_EMM_HEADER_T           stEmmHeader;                      /*EMM层三消息头*/
    MSPS_ESM_MSG_CONTAINER_T        stEsmMsgContainer;
}NAS_ATTACH_CMP_T;

/*ATTACH REJECT*/
typedef struct
{
    MSPS_NAS_EMM_HEADER_T           stEmmHeader;                     /*EMM消息头*/
    UINT8                           ucEmmCause;                      /*attach请求被拒绝的原因*/

    /* Optional IE */
    UINT8                           ucEsmMsgContainerExist;
    MSPS_ESM_MSG_CONTAINER_T         stEsmMsgContainer;
}NAS_ATTACH_REJ_T;

/********************ATTACH END********************/

/*DETACH REQUEST*/

/************************************************************************/
/* 注释：DETACH REQUEST分UE originating datach 和 UE terminated datach  */
/* 两个过程，二者的不同在于：                                           */
/* 在originating过程中，GUTI为必选，而terminated过程中不存在GUTI。      */  
/* 在terminated过程中，Cause为必选，而originating过程中不存在Cause。    */
/* 为简化期间，将二者合二为一。由GUTI的存在与否判断是那个过程。         */  
/************************************************************************/
typedef struct
{
/* Mandatory IE*/
    MSPS_NAS_EMM_HEADER_T              stEmmHeader;          /*EMM消息头*/
    BITS8                              btDetachType:4;       /*Detach类型*/
    BITS8                              btNasKsiAsme:4;   

    UINT8                              ucEmmCauseExist;
    UINT8                              ucEmmCause;
    /* Network to UE 编码的时候，无stOldGutiOrImsi这一项，可以不填 */
    MSPS_EPS_MOBILE_ID_T                stOldGutiOrImsi;
}NAS_DETACH_REQ_T;    


/*DETACH ACCEPT(UT)*/
typedef struct
{
    MSPS_NAS_EMM_HEADER_T              stEmmHeader;                     /*EMM消息头*/
}NAS_DETACH_ACCEPT_T;

/*TRACKING AREA UPDATE REQUEST*/
/* 24301 协议升级，从111 ---> 810 ,将 NAS key set IdentifierSGSN改为可选项，TV方式。 */

typedef struct
{
/* Mandatory IE*/
    MSPS_NAS_EMM_HEADER_T              stEmmHeader;                    /*EMM消息头*/
    BITS8                              btEpsupdateType:4;              /*路由区域更新类型*/
    BITS8                              btSpare:4;
    MSPS_EPS_MOBILE_ID_T               stOldGutiOrImsi;
    BITS8                              btNasKeySetId_Asme:4;
    BITS8                              btNasKeySetId_Sgsn:4;
/* Optional IE*/
    UINT8                              ucNasKeySetId_Sgsn_Exist;     // 111 ---->810 增加

	/**************************/
	UINT8                              ucAdditinalGutiExist;
    MSPS_GUTI_T                        stAdditinalGuti;

    UINT8                              ucGprsCipherKeySnExist;
	UINT8                              ucGprsCipherKeySn;

	UINT8                              ucOldPtmsiSignExist;
	UINT8                              aucOldPtmsiSign[NAS_PTMSI_SIGN_LEN];

	UINT8                              ucMsNeworkCapaExist;
    MSPS_MS_NETWORK_CAPA_T             stMsNeworkCapa;
	/**************************/

    UINT8                              ucUeNetCapaExist;
    MSPS_UE_NETWORK_CAPA_T             stUeNetworkCapa;

    UINT8                              ucLastVisitTaiExist;
    MSPS_TAI_T                         stLastVisitTai;

    UINT8                              ucstEpsBearerCtxStatusExist;
    MSPS_NAS_CCB_EBI_N_T               stEpsBearerCtxStatus;

    UINT8                              ucDrxParaExist;
    MSPS_DRX_PARA_T                    stDrxPara;

	UINT8                              ucNonceueExist;
	UINT32                             uiNonceue;

	UINT8                              ucUeRadiaCapaInfoUpdNeedFlagExist;
	MSPS_UE_RADIO_CAPA_INFO_UPD_NEEDED_FLAG_E   eUeRadiaCapaInfoUpdNeedFlag;

	UINT8                             ucOldLaiExist;
	MSPS_LAI_T                        stOldLai;

	UINT8                             ucMobileStationClassMark2Exist;
	MSPS_MOBILE_STATION_CLASSMARK2_T  stMobileStationClassMark2;
	
	UINT8                             ucAdditionalUdpTypeExist;
	MSPS_ADDITION_UDP_TYPE_T          stAdditionalUdpType;

	UINT8                             ucTmsiStatusExist;
	NAS_TMSI_STATUS_T                 stTmsiStatus;

}NAS_TAU_REQ_T;


#define EPS_UPD_RESULT_TA_UPD                     0x00
#define EPS_UPD_RESULT_COMBINED_TA_LA_UPD         0x01
#define EPS_UPD_RESULT_TA_UPD_ISR_ACT             0x04
#define EPS_UPD_RESULT_COMBINED_TA_LA_UPD_ISR_ACT 0x05

/*TRACKING AREA UPDATE ACCEPT MESSAGE*/
typedef struct
{
/* Mandatory IE*/
    MSPS_NAS_EMM_HEADER_T               stEmmHeader;                  /*EMM消息头*/
    BITS8                               btSpare:4;
    BITS8                               btEpsUpdateResult:4;
/* Optional IE*/

    UINT8                               ucGutiExist;
    MSPS_GUTI_T                         stGuti;

    UINT8                               ucTaiListExist;
    MSPS_TAI_LIST_T                     stTaiList  ;


    UINT8                               ucstEpsBearerCtxStatusExist;
    MSPS_NAS_CCB_EBI_N_T                stEpsBearerCtxStatus;

    UINT8                               ucT3412Exist;
    NAS_GPRS_TIMER_T                    stT3412;

    UINT8                               ucEPlmnExist;
    MSPS_PLMN_LIST_T                    stEPlmn;

	UINT8                               ucLaiExist;
	MSPS_LAI_T                          stLai;
	
	UINT8                               ucMsIdExist;
	MSPS_MOBILE_ID_T                    stMsId;
	
	UINT8                               ucEmmCauseExist;
	UINT8                               ucEmmCause;

	UINT8                               ucAdditionalUdpRltExist;
	MSPS_ADDITION_UDP_RESULT_T          stAdditionalUdpRlt;

}NAS_TAU_ACCEPT_T;

/*TRACKING AREA UPDATE COMPLETE MESSAGE*/
typedef struct
{
    MSPS_NAS_EMM_HEADER_T              stEmmHeader;                 /*EMM消息头*/
}NAS_TAU_CMP_T;

/*TRACKING AREA UPDATE REJECT*/
typedef struct
{
    MSPS_NAS_EMM_HEADER_T              stEmmHeader;                  /*EMM消息头*/
    UINT8                              ucEmmCause;
}NAS_TAU_REJ_T;


/*SERVICE REJECT MESSAGE*/
typedef struct
{
    MSPS_NAS_EMM_HEADER_T              stEmmHeader;          /*EMM消息头*/
    UINT8                              ucEmmCause;
}NAS_SERVICE_REJ_T;


/*GUTI REALLOCATION COMMAND*/
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_EMM_HEADER_T                 stEmmHeader;                    /*EMM消息头*/
    MSPS_GUTI_T                           stGuti;
    /* Optional IE*/
    UINT8                                 ucTaiListExist;
    MSPS_TAI_LIST_T                       stTaiList;
}NAS_GUTI_REALLOC_CMD_T;

/*GUTI REALLOCATION COMPLETE*/
typedef struct
{
    MSPS_NAS_EMM_HEADER_T                      stEmmHeader;                    /*EMM消息头*/
}NAS_GUTI_REALLOC_CMP_T;




/*  UE---->MME  */
#define NAS_NOT_SECURITY_PROTECTED            ((UINT8)0x00)
#define NAS_SECURITY_PROTECTED                ((UINT8)0x01)
#define NAS_SECURITY_HEADER_FOR_SERVICE_REQ   ((UINT8)0x0C)



/*
typedef struct  
{
    BITS8            btPd:4;       
    BITS8            btSecHeaderType:4;
    UINT8            ucKsiAndSequenceNum;
    UINT16           usMsgAuthenticationCode;
}NAS_SERVICE_REQ_T;

*/
#define NAS_SERVICE_REQ_SHORT_MAC_LEN 2
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T      stEmmHeader;                    /*EMM消息头*/
    UINT8                      ucKsiAndSequenceNum;
/*    UINT16                     usMsgAuthenticationCode;*/
    UINT8                      aucShortMac[NAS_SERVICE_REQ_SHORT_MAC_LEN];
}NAS_SERVICE_REQ_T;




/* Identity Request */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T           stEmmHeader;                    /*EMM消息头*/
    BITS8                           btSpare :4;
    BITS8                           btIdentityType :4;
}NAS_ID_REQ_T;

/* Identity Response */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T           stEmmHeader;                    /*EMM消息头*/
    MSPS_MOBILE_ID_T                stMobileID;
}NAS_ID_RSP_T;



/************************************************************************/
/*                       IMEISV   value                                 */
/************************************************************************/
/*
The purpose of the IMEISV request information element is to indicate that the IMEISV shall be included by
the MS in the authentication and ciphering response message .  
The IMSISV REQUEST is a type 1 information element .
---------------------------------------------------
|8                     5 | 4                    1 |
---------------------------------------------------
| IMEISV request IEI     |0 |IMEISV request value |
---------------------------------------------------
*/
/* IMEISV request value */
#define MSPS_NAS_IMEISV_NOT_REQUESTED    0
#define MSPS_NAS_IMEISV_REQUESTED        1
/* All other values are interpreted as IMEISV not requested by this vervion of the protocol */



/************************************************************************/
/*                        Nonce value                                   */
/************************************************************************/
/*
The purpose of the Nonce information element is to transfer a 32-bit nonce value to support deriving 
a new mapped EPS security context.
The Nonce is type 3 information element with a length of 5 octet .

*/



/************************************************************************/
/*                      UE security capability                          */
/************************************************************************/
/*
The UE security capability information element is used by the network to indicate which 
security algorithms are supported by the UE in S1 mode, Iu mode and Gb mode . Security 
algorithms are supported both for NAS and for AS security . If the UE supports S101 mode,
then these security algorithms are also supported for NAS security in S101 mode .

The UE security capability is a type 4 information element with a minimum length 
of 4 octets and a maximum length of 7 octets .
*/


/* security mode command */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */

    UINT8                       ucSeletedNasSecAlgorithms;     /* Selected NAS security algorithms */
    BITS8                       btNasKSIasme :4 ;              /* NAS key set identifier(ASME) */
    BITS8                       btSpare :4;                    /* Spare half octet */
    MSPS_UE_SEC_CAPA_T          stRepalyedUeSecCapa;

    /* optional IE */
    UINT8                       ucImeisvReqExist;              /* only a IE type exist */
    UINT8                       ucImeisvReq;

    UINT8                       ucReplayedNonceUeExist; 
    UINT32                      uiReplayedNonceUe;
    
    UINT8                       ucNonceMmeExist;
    UINT32                      uiNonceMme;

}NAS_SEC_MODE_CMD_T;


/* security mode complete */
typedef struct
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */
    UINT8                       ucImeisvExist;
    MSPS_MOBILE_ID_T            stImeisv;
}NAS_SEC_MODE_CMP_T;    



/*********************************************************************************/
/*                          2009-04-28  根据24301-810添加                        */
/*********************************************************************************/

/* EMM */

/* NAS_SEC_MODE_REJ */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */
    UINT8                       ucEmmCause;
}NAS_SEC_MODE_REJ_T;

/* NAS_EMM_STATUS */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */
    UINT8                       ucEmmCause;
}NAS_EMM_STATUS_T;

/* NAS_EMM_INFO */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */

    /* optional IE */

}NAS_EMM_INFO_T;



/* 协议24301-111升级到24301-810，增加以下三个消息结构体 */

/* This message is sent by the network to the UE in order to carry an SMS message in encapsulated format. */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */
    MSPS_NAS_MSG_CONTAINER_T    stNasMsgContainer;
}NAS_DL_NAS_TRANSPORT_T;


/* This message is sent bu the UE to the network in order to carry an SMS message in encapsulated format . */
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */
    MSPS_NAS_MSG_CONTAINER_T    stNasMsgContainer;
}NAS_UL_NAS_TRANSPORT_T;

/* 
   This message is sent by the network when a paging request with CS call indicator was received via SGs for 
   a UE, and a NAS signaling connection is already established for the UE . 
*/
typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */

	MSPS_PAGING_ID_E            ePagingId;
    /*UINT8                       ucPagingId;*/
    /* optional IE  */
    /* CLI */
    /* SS Code */
    /* LCS indicator */
    /* LCS client identity */
}NAS_CS_SERVICE_NOTIFICATION_T;



/*
   Extended service request is sent by the UE to the network to initiate a CS fallback call or 
   respond to a mobile terminated CS fallback request from the network .
*/
/* ucCSFBRsp 取值范围 */
#define NAS_CSFB_RSP_CS_FALLBACK_REJECTED_BY_THE_UE          0x00
#define NAS_CSFB_RSP_CS_FALLBACK_ACCEPTED_BY_THE_UE          0x01
/* btServiceType取值范围 */
#define NAS_MOBILE_ORGINATING_CS_FALLBACK_OR_1XCS_FALLBACK   0x00
#define NAS_MOBILE_TERMINATING_CS_FALLBACK_OR_1XCS_FALLBACK  0x01
#define NAS_MOBILE_ORIGINATING_CS_FALLBACK_EMERGENCY_CALL_OR_1XCS_FALLBACK_EMERGENCY_CALL  0x02

typedef struct  
{
    MSPS_NAS_EMM_HEADER_T       stEmmHeader;                   /* EMM消息头 */
    BITS8                       btServiceType : 4 ;
    BITS8                       btNSAksi : 4;
    UINT32                      uiMTmsi;
    UINT8                       ucCSFBRspExist; /* The UE shall include this IE only if the Service type IE indicatates "mobile terminating CS fallback". */               
    UINT8                       ucCSFBRsp;
}NAS_EXTENDED_SERVICE_REQ_T;




/************ EMM模块公共消息相关结构定义 END ************/











/************ ESM模块公共消息相关结构定义 BEGIN ************/
/*ACTIVATE DEFAULT EPS BEARER CONTEXT REQUEST*/
typedef struct  
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T                      stEsmHeader;                           /*ESM消息头*/
    MSPS_SDF_QOS_T                             stSdfQos;
    MSPS_APN_T                                 stApn;
    UINT8                                      ucPdnAddrExist;                       /* 由于111 -> 810 升级版本，将此IE转为Mandatory选项,为避免程序变动，暂保留Exist标志，Exist = 1。 */                 
    MSPS_IP_ADDR_T                             stPdnAddr;
 	/* Begin wangxiaofeng modify 2012-1-7 */
	UINT8      ucTiExistExist;
	UINT8      ucTi;
	UINT8      ucTiflg;
	/* End wangxiaofeng modify 2012-1-7 */
    /* Begin wangxiaofeng modify 2012-1-8 */
    UINT8  ucNegQosExist;
    MSPS_NEG_QOS_T  stNegQos;

    UINT8  ucNegLLCSapiExist;
    UINT8  ucLLCSapi;

    UINT8  ucRadioPriExist;
    UINT8  ucRadioPri;
 
    UINT8  ucPackFlowIdExist;
    UINT8  ucPackFlowId;
    /* End wangxiaofeng modify 2012-1-8 */	
    UINT8                                      ucApnAmbrExist;
    MSPS_AMBR_T                                stApnAmbr;

    /* Begin wangxiaofeng modify 2010/11/22  for dual ip */
	UINT8                          ucEsmCauseExist;
	UINT8                          ucEsmCause;
    /* End wangxiaofeng modify 2010/11/22  for dual ip */

    UINT8                                      ucPcoExist;
    MSPS_PCO_T                                 stPco;                      /* PCO */ 

}NAS_ACT_DFT_BEARER_CTX_REQ_T;



/*ACTIVATE DEFAULT EPS BEARER CONTEXT ACCEPT*/
typedef struct
{
/* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T                          stEsmHeader;                           /*ESM消息头*/
/* Optional IE*/
    UINT8                                          ucPcoExist;
    MSPS_PCO_T                                     stPco;                      /* PCO */  

}NAS_ACT_DFT_BEARER_CTX_ACCEPT_T;

/*ACTIVATE DEFAULT EPS BEARER CONTEXT REJECT*/
typedef struct
{
    MSPS_NAS_ESM_HEADER_T               stEsmHeader;            /*ESM消息头*/
    UINT8                          ucEsmCause;
}NAS_ACT_DFT_BEARER_CTX_REJ_T;


/*ACTIVATE DEDICATED EPS BEARER CONTEXT REQUEST BEGIN*/

typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */
    BITS8                              btLinkedEpsBearerId :4;     
    BITS8                              btSpare :4; 
    MSPS_SDF_QOS_T                     stSdfQos;                   /* LV */
    MSPS_TFT_T                         stTft;                      /* LV */

    /* Begin wangxiaofeng modify 2012-1-13 */
	UINT8      ucTiExistExist;
	UINT8      ucTi;
	UINT8      ucTiflg;
	
    UINT8  ucNegQosExist;
    MSPS_NEG_QOS_T  stNegQos;

    UINT8  ucNegLLCSapiExist;
    UINT8  ucLLCSapi;

    UINT8  ucRadioPriExist;
    UINT8  ucRadioPri;

    UINT8  ucPackFlowIdExist;
    UINT8  ucPackFlowId;

    /* End wangxiaofeng modify 2012-1-13 */
    /* Optional IE*/
    UINT8                              ucPcoExist;                 /* 标识消息中是否存在stPco.1-存在;0-不存在 */
    MSPS_PCO_T                         stPco;                      /* PCO */
}NAS_ACT_DEC_BEARER_CTX_REQ_T;
/********ACTIVATE DEDICATED EPS BEARER CONTEXT REQUEST END**********/


/**************ACTIVATE DEDICATED EPS BEARER CONTEXT ACCEPT BEGIN***/
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */
    /* Optional IE*/
    UINT8                         ucPcoExist;                 /* 标识消息中是否存在stPco.1-存在;0-不存在 */
    MSPS_PCO_T                     stPco;                      /* PCO */
}NAS_ACT_DEC_BEARER_CTX_ACCEPT_T;
/***********ACTIVATE DEDICATED EPS BEARER CONTEXT ACCEPT END********/


/********ACTIVATE DEDICATED EPS BEARER CONTEXT REJECT BEGIN*********/
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */
    UINT8                         ucEsmCause;                 /* NAS_ESM_CAUSE_TAG */
    /* Optional IE*/
    UINT8                         ucPcoExist;                 /* 标识消息中是否存在stPco.1-存在;0-不存在 */
    MSPS_PCO_T                     stPco;                      /* PCO */
}NAS_ACT_DEC_BEARER_CTX_REJ_T;
/***************ACTIVATE DEDICATED EPS BEARER CONTEXT REJECT END****/


/********************MODIFY EPS BEARER CONTEXT REQUEST BEGIN********/
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */


    UINT8                              ucNewSdfQosExist;
    MSPS_SDF_QOS_T                     stNewSdfQos;
    UINT8                              ucTftExist;
    MSPS_TFT_T                         stTft; 

    /* add by qiyalong 2013-0122 begin */
    UINT8  ucNegQosExist;
    MSPS_NEG_QOS_T  stNegQos;

    UINT8  ucNegLLCSapiExist;
    UINT8  ucLLCSapi;

    UINT8  ucRadioPriExist;
    UINT8  ucRadioPri;
 
    UINT8  ucPackFlowIdExist;
    UINT8  ucPackFlowId;
    /* add by qiyalong 2013-0122 end */

    UINT8                              ucApnAmbrExist;
    MSPS_AMBR_T                        stApnAmbr;

    UINT8                              ucPcoExist;
    MSPS_PCO_T                         stPco;                      /* PCO */ 

}NAS_MOD_BEARER_CTX_REQ_T;
/*******************MODIFY EPS BEARER CONTEXT REQUEST END************/



/****************MODIFY EPS BEARER CONTEXT ACCEPT BEGIN *************/
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */
    /* Optional IE*/
    UINT8                              ucPcoExist;                 /* 标识消息中是否存在stPco.1-存在;0-不存在 */
    MSPS_PCO_T                         stPco;                      /* PCO */
}NAS_MOD_EPS_BEARER_CTX_ACCEPT_T;
/*****************MODIFY EPS BEARER CONTEXT ACCEPT END **************/

/*********************MODIFY EPS BEARER CONTEXT REJECT BEGIN*********/
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */
    UINT8                              ucEsmCause;                 /* Tag:NAS_ESM_CAUSE_TAG */
    /* Optional IE*/
    UINT8                              ucPcoExist;                 /* 标识消息中是否存在stPco.1-存在;0-不存在 */
    MSPS_PCO_T                         stPco;                      /* PCO */
}NAS_MOD_BEARER_CTX_REJ_T;
/*********************MODIFY EPS BEARER CONTEXT REJECT END************/



/* Deactivate EPS bearer context request */
/* This message is send by the network to request deactivation of an active EPS bearer context. 
   PCO is included in the message when the network wishes to transmit (protocol) data (e.g. configuration 
   parameters, error codes or messages/events) to the UE .
*/
typedef struct  
{
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */

    UINT8                              ucEsmCause;                 /* ESM cause */

    UINT8                              ucPcoExist;
    MSPS_PCO_T                         stPco;                      /* PCO */    
}NAS_DEACT_BEARER_CTX_REQ_T;


/* Deactivate EPS bearer context accept */
/* This message is sent by the UE to acknowledge deactivation of the EPS bearer context requested in the 
   corresponding Deactivate EPS bearer context request message .  
   PCO is included in the message when the UE wishes to transmit (protocol) data (e.g. configuration parameters,
   error codes or message/events) to the network .
*/
typedef struct  
{
    MSPS_NAS_ESM_HEADER_T              stEsmHeader;                /* ESM消息头 */
        
    UINT8                              ucPcoExist;
    MSPS_PCO_T                         stPco;                      /* PCO */    
}NAS_DEACT_BEARER_CTX_ACCEPT_T;






/* PDN Connectivity Request */
typedef struct
{
    // Mandatory IE
    MSPS_NAS_ESM_HEADER_T         stEsmHeader;                // ESM消息头 
    BITS8                         btReqType :4 ;                
    BITS8                         btPDNType :4 ;                
    // Optional IE
    UINT8                         btApnExist;
    MSPS_APN_T                    stApn;
    
    UINT8                         ucEsmInfoTranFlagExist;
    UINT8                         ucEsmInfoTranFlag;

    UINT8                              ucPcoExist;
    MSPS_PCO_T                         stPco;                      /* PCO */   

}NAS_PDN_CONN_REQ_T;



/* Bearer Resource Allocation Request */
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T         stEsmHeader;                /* ESM消息头 */
    BITS8                         btSpare :4 ;   
    BITS8                         btLinkedEBI :4 ; 
    MSPS_SDF_QOS_T                stSdfQos;
    MSPS_TFT_T                    stTft;
    /* Optional IE*/
    UINT8                          ucPcoExist;
    MSPS_PCO_T                     stPco;                      /* PCO */    

}NAS_BEARER_RESOURCE_ALLOCATION_REQ_T;

/* ESM Cause ， ucEsmCause 取值范围 */
#define ESM_CAUSE_ESM_INFO_NOT_REV 0x35
#define ESM_CAUSE_PROTOCOL_ERROR   0x6F
/*other value shall be treated as 0x6F ;*/

/* Bearer Resource Allocation Reject */
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T         stEsmHeader;                /* ESM消息头 */
    UINT8                         ucEsmCause;              
    /* Optional IE*/
}NAS_BEARER_RESOURCE_ALLOCATION_REJ_T;





/* Bearer Resource release Request */
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T         stEsmHeader;                /* ESM消息头 */
    BITS8                         btSpare :4 ;   
    BITS8                         btLinkedEBI :4 ; 
    
    MSPS_TFT_T                    stTft;
    /* Optional IE*/
    UINT8                         ucQosExist;
    MSPS_SDF_QOS_T                stQos;

    UINT8                         ucEsmCauseExist;
    UINT8                         ucEsmCause;

    /* Optional IE*/
    UINT8                          ucPcoExist;
    MSPS_PCO_T                     stPco;                      /* PCO */ 

}NAS_BEARER_RESOURCE_RELEASE_REQ_T;


/* Bearer Resource release Reject */
typedef struct
{
    /* Mandatory IE*/
    MSPS_NAS_ESM_HEADER_T         stEsmHeader;                /* ESM消息头 */
    UINT8                         ucEsmCause;     
    /* Optional IE*/

    /* Optional IE*/
    UINT8                          ucPcoExist;
    MSPS_PCO_T                     stPco;                      /* PCO */ 
}NAS_BEARER_RESOURCE_RELEASE_REJ_T;


typedef NAS_BEARER_RESOURCE_RELEASE_REQ_T NAS_BEARER_RESOURCE_MODIFICATION_REQ_T;
typedef NAS_BEARER_RESOURCE_RELEASE_REJ_T NAS_BEARER_RESOURCE_MODIFICATION_REJ_T;


/*********************************************************************************/
/*                          2009-04-28  根据24301-810添加                        */
/*********************************************************************************/
/* ESM */
/* NAS_PDN_CONN_REJ */
typedef struct  
{
    MSPS_NAS_ESM_HEADER_T          stEsmHeader;                /* ESM消息头 */
    UINT8                          ucEsmCause;
    UINT8                          ucPcoExist;
    MSPS_PCO_T                     stPco;                      /* PCO */        
}NAS_PDN_CONN_REJ_T;


/* NAS_PDN_DISCONN_REQ */
typedef struct  
{
    MSPS_NAS_ESM_HEADER_T          stEsmHeader;                /* ESM消息头 */

    BITS8                          btLbi   :4;
    BITS8                          btSpare :4;
    UINT8                          ucPcoExist;
    MSPS_PCO_T                     stPco;                      /* PCO */    
}NAS_PDN_DISCONN_REQ_T;


/* NAS_PDN_DISCONN_REJ */
typedef struct  
{
    MSPS_NAS_ESM_HEADER_T          stEsmHeader;                /* ESM消息头 */
    UINT8                          ucEsmCause;
    UINT8                          ucPcoExist;
    MSPS_PCO_T                     stPco;                      /* PCO */    
}NAS_PDN_DISCONN_REJ_T;


/* NAS_ESM_STATUS */
typedef struct
{
    MSPS_NAS_ESM_HEADER_T          stEsmHeader;                /* ESM消息头 */
    UINT8                          ucEsmCause;
}NAS_ESM_STATUS_T;



typedef struct
{
    MSPS_NAS_ESM_HEADER_T          stEsmHeader;                /* ESM消息头 */
}NAS_ESM_INFO_REQ_T;

typedef struct
{
    MSPS_NAS_ESM_HEADER_T          stEsmHeader;                /* ESM消息头 */

    UINT8                          btApnExist;
    UINT8                          ucPcoExist;

    MSPS_APN_T                     stApn;
    MSPS_PCO_T                     stPco;                      
}NAS_ESM_INFO_RSP_T;
/*************************function declare*******************************/




/* ucCode */
//#define BUFF_STRUCT 0
//#define BUFF_RAW    1

/*定义编解码函数指针*/
typedef UINT32 (*pNasEnCode)(void *,  UINT8 *, UINT32 *, UINT32 ) ;   
typedef UINT32 (*pNasDeCode)(UINT8 *,  UINT32 , void *, UINT32 );


#define NAS_CODEC_TABLE_ITEM_NUM   60
/*定义编解码函数表项*/
typedef struct  
{
    UINT8             ucMsgType;         /*编码前的消息ID*/ 
    
    pNasEnCode        pEnCodeFunc;       /*编码函数*/
    
    pNasDeCode        pDeCodeFunc;       /*解码函数*/
    
}NAS_CODEC_TABLE_ITEM_T;


/* APN纠错功能相关宏 */
#define MSPS_MBM_NON_APN_CHECK   (0x00)    /*  APN 不纠错 */
#define MSPS_MBM_APN_CHECK       (0x01)    /*  APN 纠错  */


/* NULL function */
extern UINT32 msps_nas_encode_invalid_function(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_decode_invalid_function(UINT8 *,  UINT32 , void *, UINT32 );

/* EMM protocol */
extern UINT32 msps_nas_attach_accept_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_attach_accept_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_attach_complete_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_attach_complete_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_attach_reject_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_attach_reject_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_attach_request_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_attach_request_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_detach_accept_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_detach_accept_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_detach_request_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_detach_request_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_auth_req_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_auth_req_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_auth_rsp_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_auth_rsp_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_auth_fail_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_auth_fail_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_auth_rej_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_auth_rej_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_id_req_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_id_req_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_id_rsp_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_id_rsp_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_track_area_upd_req_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_track_area_upd_req_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_track_area_upd_accept_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_track_area_upd_accept_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_track_area_upd_cmp_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_track_area_upd_cmp_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_track_area_upd_reject_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_track_area_upd_reject_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_service_reject_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_service_reject_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_service_req_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_service_req_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_guti_realloc_cmd_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_guti_realloc_cmd_dec(UINT8 *,  UINT32 , void *, UINT32 );

extern UINT32 msps_nas_guti_realloc_cmp_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_guti_realloc_cmp_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_emm_info_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_emm_info_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_emm_status_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_emm_status_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_sec_mode_rej_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_sec_mode_rej_dec(UINT8 *,  UINT32 , void *, UINT32 );

extern UINT32 msps_nas_dl_nas_transport_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_dl_nas_transport_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_ul_nas_transport_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_ul_nas_transport_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_cs_service_notification_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_cs_service_notification_dec(UINT8 *,  UINT32 , void *, UINT32 );



/* ESM protocol */
extern UINT32 msps_nas_activate_default_eps_bearer_ctx_req_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_activate_default_eps_bearer_ctx_req_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_activate_default_eps_bearer_ctx_accept_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_activate_default_eps_bearer_ctx_accept_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_activate_default_eps_bearer_ctx_reject_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_activate_default_eps_bearer_ctx_reject_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_activate_dedicated_eps_bearer_ctx_req_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_activate_dedicated_eps_bearer_ctx_req_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_activate_dedicated_eps_bearer_ctx_accept_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_activate_dedicated_eps_bearer_ctx_accept_dec(UINT8 *, UINT32 , void *, UINT32 );
extern UINT32 msps_nas_activate_dedicated_eps_bearer_ctx_reject_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_activate_dedicated_eps_bearer_ctx_reject_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_mod_eps_bearer_ctx_req_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_mod_eps_bearer_ctx_req_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_mod_eps_bearer_ctx_accept_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_mod_eps_bearer_ctx_accept_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_mod_eps_bearer_ctx_reject_enc(void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_mod_eps_bearer_ctx_reject_dec(UINT8 *,  UINT32 , void *, UINT32 );
extern UINT32 msps_nas_pdn_conn_req_enc( void   *, UINT8  *, UINT32 *, UINT32  );
extern UINT32 msps_nas_pdn_conn_req_dec( UINT8 *,  UINT32 , void   *, UINT32   );
extern UINT32 msps_bearer_resource_allocation_req_enc( void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_bearer_resource_allocation_req_dec( UINT8 *, UINT32 , void *, UINT32 );
extern UINT32 msps_bearer_resource_allocation_rej_enc( void *, UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_bearer_resource_allocation_rej_dec( UINT8 *, UINT32, void *, UINT32 );
extern UINT32 msps_bearer_resource_release_req_enc( void *, UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_bearer_resource_release_req_dec( UINT8 *, UINT32, void *, UINT32 );
extern UINT32 msps_bearer_resource_release_rej_enc( void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_bearer_resource_release_rej_dec( UINT8 *, UINT32, void *, UINT32 );
extern UINT32 msps_deactivate_eps_bearer_ctx_req_enc( void *, UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_deactivate_eps_bearer_ctx_req_dec( UINT8  *, UINT32, void *, UINT32 );
extern UINT32 msps_deactivate_eps_bearer_ctx_accept_enc( void *, UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_deactivate_eps_bearer_ctx_accept_dec( UINT8  *, UINT32, void *, UINT32 );
extern UINT32 msps_nas_pdn_conn_rej_enc( void *,  UINT8 *, UINT32 *, UINT32 );
extern UINT32 msps_nas_pdn_conn_rej_dec( UINT8 *, UINT32 , void *, UINT32 );
extern UINT32 msps_nas_pdn_disconn_req_enc( void *, UINT8  *, UINT32 *, UINT32 );
extern UINT32 msps_nas_pdn_disconn_req_dec( UINT8 *, UINT32, void *, UINT32 );
extern UINT32 msps_nas_pdn_disconn_rej_enc( void *, UINT8 *, UINT32*, UINT32 );                         
extern UINT32 msps_nas_pdn_disconn_rej_dec( UINT8 *, UINT32, void*, UINT32 );


extern UINT32 msps_nas_esm_info_rsp_enc( void*, UINT8*, UINT32*, UINT32 );
extern UINT32 msps_nas_esm_info_rsp_dec( UINT8 *, UINT32, void*, UINT32 );

extern UINT32 msps_nas_esm_status_enc( void*, UINT8*, UINT32*, UINT32 );
extern UINT32 msps_nas_esm_status_dec( UINT8 *, UINT32, void*, UINT32 );

extern UINT32 msps_nas_security_mode_cmd_enc( void*, UINT8*, UINT32*, UINT32 );
extern UINT32 msps_nas_security_mode_cmd_dec( UINT8 *, UINT32, void*, UINT32 );  //暂时，提供给模拟UE使用

extern UINT32 msps_nas_security_mode_cmp_enc( void*, UINT8*, UINT32*, UINT32 );
extern UINT32 msps_nas_security_mode_cmp_dec( UINT8 *, UINT32, void*, UINT32 );
extern UINT32 msps_sig_get_len_ptop(UINT8 *pucTemp1, UINT8 *pucTemp2);
/******************************** HEAD FILES PROTECTION Begin ***************************/
#endif
/******************************** HEAD FILES PROTECTION END *****************************/
/******************************** .H FILE ENDS HERE *************************************/




