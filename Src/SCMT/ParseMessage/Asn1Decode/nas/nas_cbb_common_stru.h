/******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
*******************************************************************************
* �ļ�����: nas_cbb_common_stru.h
* ��    ��: ����MME��ϵͳ�����й����ṹ�Ķ��弰��������ϵͳ���õĽṹ����
* ˵    ��:
* �޸���ʷ:
* 01a 2008/09/04 ��ΰ ���������汾 ���ο�SGSN��ص�ͷ�ļ����壩
******************************************************************************/

/********************************* ͷ�ļ����� ********************************/
/******************************** ͷ�ļ�������ͷ *****************************/
#ifndef NAS_CBB_COMMON_STRU_H
#define NAS_CBB_COMMON_STRU_H


#include "nas_cbb_public.h"



//#pragma pack(1)
/******************************** ��ͳ������� *******************************/


/************ PS��ϵͳ�˳����ͺ궨�� BEGIN ************/
/************ PS��ϵͳ�˳����ͺ궨�� END ************/
/* IPЭ������ */
#define MSPS_IPVER_IPV4			((UINT8)(0))
#define MSPS_IPVER_IPV6			((UINT8)(1))
/* Begin wangxiaofeng modify 2010/11/22  for dual ip */
#define MSPS_IPVER_IPV4V6			((UINT8)(2))
#define MSPS_IPVER_IPV4ORV6			((UINT8)(3))

#define MSPS_IPVER_SEL_FROM_PRI		((UINT8)(0))
#define MSPS_IPVER_SEL_FROM_V4		((UINT8)(1))
#define MSPS_IPVER_SEL_FROM_V6		((UINT8)(2))
#define MSPS_IPVER_SEL_FROM_DEV		((UINT8)(3))
/* End wangxiaofeng modify 2010/11/22  for dual ip */

#define MSPS_IP_ADDR_V4			((UINT8)(0))
#define MSPS_IP_ADDR_V6			((UINT8)(1))
#define MSPS_IP_ADDR_V4V6    	((UINT8)(2))  /* zhaobo added 2011_01_17 */

#define MSPS_IPV4_VER_TYPE ((UINT16)(4))
#define MSPS_IPV6_VER_TYPE ((UINT16)(6))

/* IP���� */
#define MSPS_IP_ADDR_V4_LEN	    ((UINT8)(4))
#define MSPS_IP_ADDR_V6_LEN	    ((UINT8)(16))


/* IP��ַ��Ч�� */ 
#define RDBS_IPVER_IPV4					0	/* MSPS0.80 */
#define RDBS_IPVER_IPV6					1	/* MSPS0.80 */
/* Begin wangxiaofeng modify 2010/11/22  for dual ip */
#define RDBS_IPVER_IPV4V6					2	
/* End wangxiaofeng modify 2010/11/22  for dual ip */

#define RDBS_IP_VALID					0	/* MSPS0.80 */
#define RDBS_IP_INVALID					1	/* MSPS0.80 */
#define RDBS_IPV4_ADDR_LEN		((UINT8)(4))		/* MSPS0.80 */
#define RDBS_IPV6_ADDR_LEN		((UINT8)(16))		/* MSPS0.80 */

#define MSPS_IP_INVALID			((UINT8)(0))                                 
#define MSPS_IP_VALID			((UINT8)(1))

#define  MSPS_PUB_FAIL				0
#define  MSPS_PUB_SUCCESS			1
#define  MSPS_PUB_INVALID_VALUE	2

#define  MSPS_PUB_FALSE                0
#define  MSPS_PUB_TRUE                 1

#define  MSPS_PUB_NONEXIST		0
#define  MSPS_PUB_EXIST			1

/************ ���ݿ�GMM CONTEXT�������Ϣ�궨�� BEGIN ************/
#define MSPS_MAX_IMSI_LEN			8
#define MSPS_MAX_MSISDN_LEN		8
#define MSPS_MAX_IMEI_LEN			8
#define MSPS_MAX_IMEISV_LEN		8
#define MSPS_MAX_PTMSI_SIGNATURE_LEN	3
#define MSPS_MAX_MOBILE_ID_DIGIT_LEN	8
#define MSPS_MAPU_MAX_AGEIND_NUM		6

/* added by liuyang9550 2009-03-02 for write subdata below */
#define MSPS_MAX_STNSR_LEN             8
/* Begin wangxiaofeng modify 2010/11/26 for RSZC */
/* Begin wangxiaofeng modify 2011/1/4 for RSZC type error*/
#define MSPS_RSZC_LEN              2
/* End wangxiaofeng modify 2011/1/4 for RSZC type error*/
/* End wangxiaofeng modify 2010/11/26 for RSZC */

/* added by liuyang9550 2009-03-02 for write subdata above */

#define MSPS_MAX_RAND_LEN              16
#define MSPS_MAX_SRES_LEN              4
#define MSPS_MAX_KC_LEN                8
#define MSPS_MAX_XRES_LEN              16
#define MSPS_MAX_CK_LEN                16
#define MSPS_MAX_IK_LEN                16
#define MSPS_MAX_AUTN_LEN              16
#define MSPS_MAX_AUTS_LEN              14
#define MSPS_MAX_VECTOR_NUM            5

#define MSPS_MAX_ACCESS_CAPA_NUM       10  /* 24008��10.5.5.12a */
#define MSPS_MAX_RADIO_ACCCAPA_NUM     16

#define MSPS_MAX_NUM_POINTS            15 /* 25413: 9.2.3.11 */

/* GMM��������CAMEL��Ϣ�еĺ�ֵ */
#define MSPS_MAX_ADDR_LEN              20
#define MSPS_MAX_ISDN_STRING_LEN       9
#define MSPS_MAX_NUM_OF_CAMEL_TDPDATA  10
#define MSPS_MAX_NUM_OF_TPDUTYPES      5
#define MSPS_MAX_NUM_OF_MOBILITYTRIGGERS 10

/* ǩԼ������Я��������zonecode �ĸ��� */
#define MSPS_MAX_NUM_OF_ZONE_CODE      10

/* ǩԼ������Я��������LSA�ĸ��� */
#define MSPS_MAX_NUM_OF_LSA            20

/* LCS_INFO */
#define MSPS_MAX_NUM_GMLC      		  5
#define MSPS_MAX_NUM_SS_STATUS 	      5
#define MSPS_MAX_NUM_EX_CLIENT 	      5
#define MSPS_MAX_NUM_EX_EX_CLIENT      35
#define MSPS_MAX_NUM_CLIENT_INTERID    5

/* ·�����仯�б� */
#define MSPS_RAI_CHANGE_NUM_MAX        888

#define MSPS_SUBFLOW_MAX_NUM           7
#define MSPS_SUBFLOW_COMB_MAX_NUM      64
    
/* GMM �������й���PDP �����ĵĺ� */
#define MSPS_MAX_PDP_NUM_PER_IMSI      11    /* ÿ�û���༤��PDP��������Ŀ */
#define MSPS_MAX_SUB_PDP_NUM_PER_IMSI  50

/* GMM context���� */
#define MSPS_GMM_TYPE_GSM_TRIPLET             0
#define MSPS_GMM_TYPE_UMTS_QUINTUPLET         1
#define MSPS_GMM_TYPE_GSM_QUINTUPLET          2
#define MSPS_GMM_TYPE_UMTS_QUINTUPLET_CIPH    3
#define MSPS_MAX_TRIPLET_NUM           5
#define MSPS_MAX_QUINTUPLET_NUM        5

#define MSPS_GTPC_CONTAINER_MAX_LEN    100

/****** GMM�����������Խṹ�����漰�ĺ�ֵ BEGIN ******/
/* MOBILE ID TYPE */
#define  MSPS_MS_ID_TYPE_IMSI                 ((UINT8)1)
#define  MSPS_MS_ID_TYPE_IMEI                 ((UINT8)2)
#define  MSPS_MS_ID_TYPE_IMEISV               ((UINT8)3)
#define  MSPS_MS_ID_TYPE_PTMSI                ((UINT8)4)
#define  MSPS_MS_ID_TYPE_NO_ID                ((UINT8)0)

/* radio priority sms */
#define  PRIORITY_LEVEL_1                   ((UINT8)1)
#define  PRIORITY_LEVEL_2                   ((UINT8)2)
#define  PRIORITY_LEVEL_3                   ((UINT8)3)
#define  PRIORITY_LEVEL_4                   ((UINT8)4)

/* MSPS_ODB_DATA_T�� ucSubsStatus */
#define  MSPS_ODB_SERVICE_GRANTED     0x00   /* ɾ�����е�ODBǩԼ�� */
#define  MSPS_ODB_SUBS_STATUS_ODB     0x01   /* �޸�ODBǩԼ�� */

/* MSPS_GPRS_CSI_T��ucGprsTdp��GPRS Trigger Detection Point */
#define  ATTACH                            0x01
#define  ATTACH_CHANGE_OF_POSITION         0x02
#define  PDP_CTXT_ESTAB                    0x0b
#define  PDP_CTXT_ESTAB_ACK                0x0c
#define  PDP_CTXT_CHANGE_OF_POSITION       0x0e

/* MSPS_GPRS_CSI_T��MSPS_SMS_CSI_T��ucDefaultSessionHandling */
#define  MSPS_CAMEL_CONTINUE_TRANSACTION             0x00
#define  MSPS_CAMEL_RELEASE_TRANSACTION              0x01

/* MSPS_GPRS_CSI_T��MSPS_SMS_CSI_T��usCamelCapabilityHandling */
#define  MSPS_CAMEL_PHASE_1                      ((UINT8)1)
#define  MSPS_CAMEL_PHASE_2                      ((UINT8)2)
#define  MSPS_CAMEL_PHASE_3                      ((UINT8)3)
#define  MSPS_CAMEL_PHASE_4                      ((UINT8)4)

/* MSPS_SMS_CAMEL_TDP_DATA_T�е�ucSmsTdp */
#define  MSPS_SMS_TDP_COLLECTED_INFO             ((UINT8)1)
#define  MSPS_SMS_TDP_DELIVERY_REQUEST           ((UINT8)2)

/* MSPS_MG_CSI_T��aucMobilityTriggers Mobility Management Triggers */
#define  RAU_IN_SAME_SGSN                          0x80
#define  RAU_FROM_NEW_TO_OTHER_SGSN                0x80
#define  RAU_DISCONNECT_TO_OTHER_SGSN_BY_DETACH    0x82
#define  GPRS_ATTACH                               0x83
#define  MS_INIT_GPRS_DETACH_TRG                   0x84
#define  NET_INT_GPRS_DETACH                       0x85
#define  NET_INIT_TRANS_TO_MS_UNREACH_FOR_PAGING   0x86

/* MSPS_LSA_INFO_T��ucLsaOnlyAccessIndicator��LSA Only Access Indicator */
#define  ACCESS_OUTSIDE_LSAS_ALLOWED      0x00
#define  ACCESS_OUTSIDE_LSAS_RESTRICTED   0x01

/* MSPS_EXT_CLIENT_T��ucGmlcRestriction, used in Call/Session Related/Unrelated Privacy Class */
#define  MSPS_GMLC_LIST      0
#define  MSPS_HOME_COUNTRY   1

/* MSPS_EXT_CLIENT_T��MSPS_CALL_SESS_T��ucNotifiToMsUser ,used in Call/Session Related/Unrelated Privacy Class */
#define  MSPS_NOTIF_LOC_ALLOW                     0
#define  MSPS_NOTIF_VERIFY_LOC_ALLOW_NO_RSP       1
#define  MSPS_NOTIF_VERIFY_LOC_NOTALLOW_NO_RSP    2
#define  MSPS_LOC_NOT_ALLOW 					     3

/* MSPS_PLMN_OP_T��LCSClientInternalID ,used in PLMN Client List */
#define  MSPS_BC_SVR         0  /* broadcastService */
#define  MSPS_O_M_HPLMN      1
#define  MSPS_O_M_VPLMN      2
#define  MSPS_ANONY_LOC      3  /* anonymousLocation */
#define  MSPS_TARGET_MS_SVR  4

/****** GMM�����������Խṹ�����漰�ĺ�ֵ END ******/

/************ ���ݿ�GMM CONTEXT�������Ϣ�궨�� END ************/

/************ ���ݿ�PDP CONTEXT�������Ϣ�궨�� BEGIN ************/
#define MSPS_MAX_APN_NI_LEN        63     /* APN�����ʶ������󳤶� */
#define MSPS_MAX_APN_OI_LEN        37     /* APN��Ӫ�̱�ʶ������󳤶� */
#define MSPS_GTPC_MAX_PF_NUM       8      /* TFT IEЯ��packet filter������ */
#define MSPS_MAX_PCO_LEN     	  250    /* PCO IE��󳤶� */

#define MSPS_VALUE_MAX_LEN             255
#define MSPS_SGC_IMSI_PREFIX_MAX_LEN   8
#define MSPS_MAX_NUM_OF_HLRID          50
#define MSPS_MAX_PACKETFILTER_NUM      8

/****** PDP�����������Խṹ�����漰�ĺ�ֵ BEGIN ******/
/* MSPS_PDP_BASE_T,MSPS_SUB_PDP_CONTEXT_CONTENT_T��ucPdpType */
#define     MSPS_PDP_TYPE_PPP    0x01            /* According to 24.008 */
#define     MSPS_PDP_TYPE_IPV4   0x21            /* According to 24.008 */
#define     MSPS_PDP_TYPE_IPV6   0x57            /* According to 24.008 */
#define     MSPS_PDP_TYPE_NULL   0XFF            /* No PDP Type */

/* MSPS_PDP_BASE_T��ucPdpTypeOrg */
#define     MSPS_PDP_TYPE_ORG_ETSI   0           /* According to 24.008 */
#define     MSPS_PDP_TYPE_ORG_IETF   1           /* According to 24.008 */
#define     MSPS_PDP_TYPE_ORG_EMPTY  15          /* According to 24.008 */

/* MSPS_EXTENDED_UMTS_QOS_T��btTrafficClass */
#define     MSPS_QOS_CONVERSATIONAL_TRAFFIC      1   /* According to 24.008 */
#define     MSPS_QOS_STREAMING_TRAFFIC           2   /* According to 24.008 */
#define     MSPS_QOS_INTERACTIVE_TRAFFIC         3   /* According to 24.008 */
#define     MSPS_QOS_BACKGROUND_TRAFFIC          4   /* According to 24.008 */
/****** PDP�����������Խṹ�����漰�ĺ�ֵ END ******/

/*************** ��Դ���ݹ���ģ��궨��BEGIN *************/
/* 2011-11-8 for С�ͻ� */
#define MSPS_MAX_CCB_N          (g_stMmeResNumCfg.uiSysCapacityUeNumPerMcpa + 1)/* MME �������֧�ֵ��û��� 			*/
#define MSPS_MAX_PDN_N          g_stMmeResNumCfg.uiSysCapacityPdnNumPerMcpa     /* MME �������֧�ֵ�PDN ������			*/
#define MSPS_MAX_EPS_BEARER_N   g_stMmeResNumCfg.uiSysCapacityBearerNumPerMcpa  /* MME �������֧�ֵ�ESP ������			*/

#define	MSPS_MAX_MTMSI_N			(MSPS_MAX_CCB_N*2)	/* ����ʹ�õ�M-TMSI ��Դ������	*/
#define	MSPS_MAX_S1AP_N				MSPS_MAX_CCB_N		/* ����ʹ�õ�S1AP ID��Դ������  */
#define	MSPS_MAX_TEID_N				MSPS_MAX_EPS_BEARER_N

#define MSPS_DISABLE_RESOURCE				0xFFFFFFFF
#define MSPS_FREE_RESOURCE					0x00000000
#define MSPS_BUSY_RESOURCE					0x80000000

#define	MSPS_DISAVAILABLE_CCB				MSPS_DISABLE_RESOURCE
#define	MSPS_DISAVAILABLE_PDN_INFO			MSPS_DISABLE_RESOURCE
#define	MSPS_DISAVAILABLE_EPS_BEAEER_INFO	MSPS_DISABLE_RESOURCE

#define	MSPS_RDM_ERROR_CODE_START		0x80000000
#define MSPS_RDM_ERROR_NO_CCB			(MSPS_RDM_ERROR_CODE_START + 21)
#define MSPS_RDM_ERROR_NO_MTMSI			(MSPS_RDM_ERROR_CODE_START + 22)
#define MSPS_RDM_ERROR_NO_PDN			(MSPS_RDM_ERROR_CODE_START + 23)
#define MSPS_RDM_ERROR_NO_BEARER			(MSPS_RDM_ERROR_CODE_START + 24)
#define MSPS_RDM_ERROR_NO_S1AP_ID		(MSPS_RDM_ERROR_CODE_START + 25)
#define MSPS_RDM_ERROR_NO_S1_TEID		(MSPS_RDM_ERROR_CODE_START + 26)
#define MSPS_RDM_ERROR_NO_S11_TEID		(MSPS_RDM_ERROR_CODE_START + 27)

#define MSPS_RDM_ERROR_CCB_RLS_FAIL		(MSPS_RDM_ERROR_CODE_START + 41)
#define MSPS_RDM_ERROR_MTMSI_RLS_FAIL	(MSPS_RDM_ERROR_CODE_START + 42)
#define MSPS_RDM_ERROR_PDN_RLS_FAIL		(MSPS_RDM_ERROR_CODE_START + 43)
#define MSPS_RDM_ERROR_BEARER_RLS_FAIL	(MSPS_RDM_ERROR_CODE_START + 44)
#define MSPS_RDM_ERROR_S1AP_ID_RLS_FAIL	(MSPS_RDM_ERROR_CODE_START + 45)
#define MSPS_RDM_ERROR_S1_TEID_RLS_FAIL	(MSPS_RDM_ERROR_CODE_START + 46)
#define MSPS_RDM_ERROR_S11_TEID_RLS_FAIL	(MSPS_RDM_ERROR_CODE_START + 47)
#define	MTMSI_FREE							0xC0000000
#define	MTMSI_BUSY							0xE0000000
#define	MSPS_DISAVAILABLE_MTMSI				MSPS_DISABLE_RESOURCE
#define	MSPS_MTMSI_MASK						0x001FFFFF	/* bit20~bit0(1 ~ 2,097,150) */
#define	MSPS_MTMSI_MASK1						0x1FFFFFFF
#define	MSPS_MTMSI_FREE_MASK					0xC01FFFFF

#define	MSPS_EQUAL							(UINT32)1	/* FFS	*/
#define	MSPS_NOTEQUAL						(UINT32)0	/* FFS	*/

#define	MSPS_PDN_INFO_FREE					MSPS_FREE_RESOURCE
#define	MSPS_PDN_INFO_BUSY					MSPS_BUSY_RESOURCE

#define	MSPS_EPS_BEARER_INFO_FREE			MSPS_FREE_RESOURCE
#define	MSPS_EPS_BEARER_INFO_BUSY			MSPS_BUSY_RESOURCE

#define	MSPS_MAX_APN_NAME			64	/* APN name length			*/
#define MSPS_MAX_SERVER_PATY_IP_ADDR_NUM  2/* added by liuyang9550 2009-08-23 */

#define	MSPS_MAX_HSS_APN_N			5	/* ǩԼ�����е�APN�����	*/
#define MAX_RSZ_ZONCODE_NUM         11  /* added by liuyang9550 2009-08-23 */
#define	MSPS_MAX_PDN					11	/* ÿ��CCB�е�PDN�����		*/
#define	MSPS_MAX_PDN_BEARER_N		16	/* ÿ��UE֧��11������		*/

#define	MSPS_DFT_BEARER_INDEX		0
#define	MSPS_MAX_BEARER_PER_PDN		11	/* ÿ��PDN�е�EPS���������	*/
#define MSPS_MAX_BEARER_PER_UE      11

#define	MSPS_PRE_PAID_FLG			0
#define	MSPS_NORMAL_PAID_FLG			1
#define	MSPS_FLAT_RATE_FLG			2
#define	MSPS_HOT_BILLING_FLG			3
/*************** ��Դ���ݹ���ģ��궨��END ***************/

/************ ���ݿ�PDP CONTEXT�������Ϣ�궨�� END ************/

/****** �Ʒ���غ궨��GEGIN ******/
#define MSPS_SSF_MAX_FCI_LENGTH              160  /*FCI���ݳ���*/

/******�Ʒ���غ궨�� END ******/

/******����Ϣ��غ궨�� BEGIN ******/
#define  MAX_ADDRESS_LENGTH   ((UINT8)20)  /* 29002 */
/******����Ϣ��غ궨�� END ******/

/******************************** ���Ͷ��� ***********************************/
/************ PS��ϵͳ��RDBS GMM CONTEXT ��ع����ṹ���� BEGIN ************/
/* IMSI 23003��2.2 */
typedef struct
{
    UINT8  ucLength;
    UINT8  aucImsi[MSPS_MAX_IMSI_LEN];  /* MAX=8 */
}MSPS_IMSI_T;

/* MSISDN */  /*23003��3.3*/
typedef struct
{
    UINT8  ucLen;
    UINT8  aucValue[MSPS_MAX_MSISDN_LEN];  /* MAX=8��OCTET��15 DIGITS��*/
}MSPS_MSISDN_T;

/* IMEI 23003��6.2.1 */
typedef struct
{
    UINT8  aucValue[MSPS_MAX_IMEI_LEN];  /* MAX=8 */
}MSPS_IMEI_T;

/* added by liuyang9550 2009-03-02 for write subdata below */
/* see 23.003 830 */
typedef struct
{
    UINT8  ucLen;
    UINT8  aucValue[MSPS_MAX_STNSR_LEN];  /* MAX=8��OCTET��15 DIGITS��*/
}MSPS_STNSR;

#if 0		/* 2010-01-08 by zhangxf */
typedef struct
{
    UINT8  ucLen;
    UINT8  aucValue[MSPS_MAX_RSZC_LEN];  /* MAX=50��OCTET��15 DIGITS��*/
}MSPS_RSZoneCode;
#endif

typedef struct
{
    UINT8  ucLen;
    UINT8  aucValue[MSPS_MAX_APN_OI_LEN];  /* MAX=37��OCTET��64 character��*/
}MSPS_APNOIReplacement;

/* added by liuyang9550 2009-03-02 for write subdata above */

/* add by wangzhao 2010-03-31 for DSR begin */
//Charging Characteristics
#define	MSPS_MAX_CHARG_CHARACT_LEN		16 
typedef struct 
{
    UINT32	uiLen;
    UINT8		aucValue[MSPS_MAX_CHARG_CHARACT_LEN];
}MSPS_ChargingCharact_T;
/* add by wangzhao 2010-03-31 for DSR end */
/* TMSI 23003��2.4 */
typedef	UINT32			MSPS_TMSI_T;  /* ��16��������ʾ��ʹ�ã�0x12345678 */

/* M-TMSI 2008-10-24*/
typedef	MSPS_TMSI_T		MSPS_MTMSI_T;

/* P_TMSI */
typedef	MSPS_TMSI_T		MSPS_PTMSI_T;

/*S TMSI 36413:9.2.3.6*/
typedef struct
{
    UINT8		ucMmeCode;
    MSPS_MTMSI_T	stMTmsi;
}MSPS_STMSI_T;

/* PLMN 23003��12.1 */
/*****************************************************************************
*		PLMN ID   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119)  
*   23			19			15			11			7			3			0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |	MCC2	|	MCC1	|	MNC3	|	MCC3	|	MNC2	|	MNC1	|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
typedef struct
{
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btMcc2:4;
        BITS8  btMcc1:4;
        BITS8  btMnc3:4;
        BITS8  btMcc3:4;
        BITS8  btMnc2:4;
        BITS8  btMnc1:4;

    #else
        BITS8  btMcc1:4;
        BITS8  btMcc2:4;
        BITS8  btMcc3:4;
        BITS8  btMnc3:4;
        BITS8  btMnc1:4;
        BITS8  btMnc2:4;   
    #endif
}MSPS_PLMN_T;

/*****************************************************************************
*		GUMMEI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119)  
*   47							23						7			0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |MCC2|MCC1|MNC3|MCC3|MNC2|MNC1|		mme grp id		|  mme code	|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
typedef struct{
	MSPS_PLMN_T				stPlmn;
	UINT16					usMmeGrpId;
	UINT8					ucMmeCode;
}MSPS_GUMMEI_T;

/*****************************************************************************
*		GUTI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119 )  
*  65							  42		38		  31			0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |MCC2|MCC1|MNC3|MCC3|MNC2|MNC1|mme grp id|mme code|		mtmsi	|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
typedef struct{	/* 2008/10/06	*/
	MSPS_GUMMEI_T		stGummei;
	MSPS_MTMSI_T			uiMtmsi;
}MSPS_GUTI_T;

/* IMEISV 23003��6.2.2 */
typedef struct
{
    UINT8  aucValue[MSPS_MAX_IMEISV_LEN];  /* MAX=8 */
}MSPS_IMEISV_T;

/*****************************************************************************
*	MSPS_IMEISV_T  ( 3GPP TS 23.003 v8.2.0(2008-09) Page.59 )  
*    63										 31					 7			0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |						TAC					|		SNR			|	SVN		|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*	-TAC:Type Allocation Code(8 digit)
*	-SNR:Serial Number(6 digit)
*	-SVN:Software Version Number(2 digit)
******************************************************************************/
typedef struct{				/* zhangxianfeng */
	MSPS_IMEI_T				stImei;
	UINT8                   ucImeiSoftVerExist;
	MSPS_IMEISV_T			stImeiSoftVer;
}MSPS_MEID_T;

/************** PS��ϵͳ��RDBS ��ع����ṹ���� BEGIN ***********/
#define SWP_GTPV_V2_APN_RST_RESERVE_LEN 10
typedef struct{

	UINT16 usLen;
	UINT8 ucRstType;
	UINT8 aucReserve[SWP_GTPV_V2_APN_RST_RESERVE_LEN];

}MSPS_APN_RESTRICTION_T;

typedef struct{

	UINT32 uiAccessRestrictionData;/* FFS */

}MSPS_ACCESS_RESTRICTION_T;


#define MSPS_SEC_KASME_MAX_LEN  32
#define MSPS_SEC_ENC_MAX_LEN    16
#define MSPS_SEC_INT_MAX_LEN    16
#define MSPS_SEC_ENB_MAX_LEN    32

typedef struct{	/* 3GPP TS 24.301 V.4.0(2008-07) */
	BITS8	btKsiasme	:3;/*ksi_asme*/
	BITS8	btNasCount	:5;/*
	k_asme
	k_nasint
	k_nassec
	nas_count
	*/
	/**********wpc 20090717 add************/
	UINT8  aucKasme[MSPS_SEC_KASME_MAX_LEN];
	UINT8  aucEnc[MSPS_SEC_ENC_MAX_LEN];
	UINT8  aucInt[MSPS_SEC_INT_MAX_LEN];
	UINT8  aucEnb[MSPS_SEC_ENB_MAX_LEN];
	UINT32 uiUpCount;
	UINT32 uiDlCount;
	UINT8  ucAuthSuccFlag;   /* ��Ȩ�ɹ���1����û��Ȩ ��0 */
	UINT8  ucSeletedNasSecAlgorithms;

	/**************************************/
}MSPS_NAS_INT_K_T;	/* FFS */

typedef struct {	/* 3GPP TS 24.301 V.4.0(2008-07) */
	BITS8	btReserved7			:1;
	BITS8	btTypeCiphAlgor		:3;
	BITS8	btReserved3			:1;
	BITS8	TypeInteProAlgor	:3;
}MSPS_NAS_SECURITY_ALGOR_T;	/* FFS */

typedef struct{

	UINT32 uiGreKey;			/* 3GPP TS29.274 8.25 F-TEID Format Define */

}MSPS_GRE_KEY_T;

/* LAI 23003��4.1 */
/*****************************************************************************
*		LAI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119)  
*   39							 15									0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |MCC2|MCC1|MNC3|MCC3|MNC2|MNC1|			lac(BCD)				|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
typedef struct
{
    MSPS_PLMN_T    stPlmn;
    UINT16        usLac;
}MSPS_LAI_T;

/* CGI 23003��4.1 */
/*****************************************************************************
*		CGI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119)  
*   55							  31				15				0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |MCC2|MCC1|MNC3|MCC3|MNC2|MNC1|		lac(BCD)	|	ci(BCD)		|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
typedef struct
{
    MSPS_LAI_T	stLai;
    UINT16		usCi;
}MSPS_CGI_T;

//36.413 V851 ECGI 2009-06-29
typedef struct  
{
	MSPS_PLMN_T			stPlmn;
	BITS32				btResv:4;
	BITS32				btCi:28;
}MSPS_ECGI_T; 

/*****************************************************************************
*		TAI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119)  
*   39								 	15							0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |MCC2 |MCC1 |MNC3 |MCC3 |MNC2 |MNC1 |			tac				|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
typedef struct
{
    MSPS_PLMN_T	stPlmn;
    UINT16		usTac;	
}MSPS_TAI_T;
/* Begin wangxiaofeng modify 2010/11/26 for RSZC */
typedef struct 
{
    UINT16  usZoneCode;
    UINT16  usResves;
}MSPS_ZONECODE_T;
typedef MSPS_ZONECODE_T	MSPS_RSZoneCode;	/* 2010-01-08 by zhangxf */

#define MSPS_TAI_PERZONECODE_NUM 480
#define MSPS_ZONECODE_PERDEV_NUM 480
typedef struct
{
    UINT16 usZoneCode;
    UINT16 usTaiNum;
    MSPS_TAI_T astTaiList[MSPS_TAI_PERZONECODE_NUM];

}MSPS_ZONECODETOTAI_T;
typedef struct
{
    UINT32 uiZoneCodeToTaiNum;
    MSPS_ZONECODETOTAI_T astZoneCodeToTailist[MSPS_ZONECODE_PERDEV_NUM];
}MSPS_ZONECODELIST_T;
/* End wangxiaofeng modify 2010/11/26 for RSZC */

/* RAI 24008��10.5.5.15 */
/*****************************************************************************
*		RAI   ( 3GPP TS 23.401 v8.1.0(2008-03) Page.119)  
*   47							 23					7				0
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
*   |MCC2|MCC1|MNC3|MCC3|MNC2|MNC1|		lac			|		rac		|
*   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
******************************************************************************/
typedef struct
{
    MSPS_LAI_T  stLai;
    UINT8      ucRac;
}MSPS_RAI_T;

typedef struct
{
    UINT8   aucValue[MSPS_MAPU_MAX_AGEIND_NUM];
}MSPS_AGE_INDICATOR_T;

/* PTMSI_SIGNATURE 24008��10.5.5.8, 23003��2.7 */
typedef struct
{
    UINT8  aucValue[MSPS_MAX_PTMSI_SIGNATURE_LEN];  /* MAX=3 */
}MSPS_PTMSI_SIGNATURE_T;

/* Mobile Identity 24008��10.5.1.4 */
typedef struct
{
    UINT8 ucLength;
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btIdDigit1:4;
        BITS8  btOddEvenIndicator:1;
        BITS8  btIdentityType:3;
    #else
        BITS8  btIdentityType:3;
        BITS8  btOddEvenIndicator:1;
        BITS8  btIdDigit1:4;
    #endif
    UINT8 aucIdDigit[MSPS_MAX_MOBILE_ID_DIGIT_LEN];    /* MAX=8 */
}MSPS_MOBILE_ID_T;

/* SAI 23003��12.5, 25413��9.2.3.9 */
typedef struct
{
    MSPS_LAI_T  stLai;
    UINT16     usSac;
}MSPS_SAI_T;

/* Time Zone and Time 24008��10.5.3.9 */
typedef struct
{
    UINT8  ucYear;
    UINT8  ucMonth;
    UINT8  ucDay;
    UINT8  ucHour;
    UINT8  ucMinute;
    UINT8  ucSecond;
    UINT8  ucTimeZone;
}MSPS_SAI_AGE_T;

/* NE number */
typedef  MSPS_MSISDN_T  MSPS_SGSN_NUMBER_T;
typedef  MSPS_MSISDN_T  MSPS_GGSN_NUMBER_T;
typedef  MSPS_MSISDN_T  MSPS_HLR_NUMBER_T;
typedef  MSPS_MSISDN_T  MSPS_VLR_NUMBER_T;
typedef  MSPS_MSISDN_T  MSPS_GSN_NUMBER_T;

/****** ��Ȩ��ؽṹ���� BEGIN ******/

/* 29002��ASN.1 description��PAGE 335..336 */

/* RAND */
typedef struct
{
    UINT8  ucValue[MSPS_MAX_RAND_LEN];  /* MAX=16 */
}MSPS_RAND_T;

/* SRES */
typedef struct
{
    UINT8  ucValue[MSPS_MAX_SRES_LEN];  /* MAX=4 */
}MSPS_SRES_T;

/* KC */
typedef struct
{
    UINT8  ucValue[MSPS_MAX_KC_LEN];  /* MAX=8 */
}MSPS_KC_T;

/* XRES */
typedef struct
{
    UINT8  ucLen;                      /* 4..16 */
    UINT8  ucValue[MSPS_MAX_XRES_LEN];  /* MAX=16 */
}MSPS_XRES_T;

/* CK */
typedef struct
{
    UINT8  ucValue[MSPS_MAX_CK_LEN];  /* MAX=16 */
} MSPS_CK_T;

/* IK */
typedef struct
{
    UINT8  ucValue[MSPS_MAX_IK_LEN];  /* MAX=16 */
}MSPS_IK_T;

/* AUTN */
typedef struct
{
    UINT8  ucLength;
    UINT8  ucValue[MSPS_MAX_AUTN_LEN];  /* MAX=16 */
}MSPS_AUTN_T;

/* AUTS */
typedef struct
{
    UINT8  ucValue[MSPS_MAX_AUTS_LEN];  /* MAX=14 */
}MSPS_AUTS_T;

/* CKSN */
typedef  UINT8  MSPS_CKSN_T;

/* KSI */
typedef  UINT8  MSPS_KSI_T;

#define MSPS_KASME_MAX_LEN 0x20

typedef struct
{
	UINT8        aucKasme[MSPS_KASME_MAX_LEN];
}MSPS_KASME_T;
/* Authentication Triplet */
typedef struct
{
    MSPS_RAND_T  stRand;
    MSPS_SRES_T  stSres;
    MSPS_KC_T  stKc;
}MSPS_AUTH_TRIPLET_T;
typedef struct
{
    MSPS_RAND_T  stRand;
    MSPS_XRES_T  stXres;
    MSPS_AUTN_T  stAutn;

	MSPS_KASME_T stKasme;

}MSPS_AUTH_QUADRUPLET_T;

/* Authentication Quintuplet 23008��2.3.2 */
typedef struct
{
    MSPS_RAND_T  stRand;
    MSPS_XRES_T  stXres;
    MSPS_CK_T    stCk;
    MSPS_IK_T    stIk;
    MSPS_AUTN_T  stAutn;
}MSPS_AUTH_QUINTUPLET_T;

/******************************/
/* ucAuthVectorType */
//#define MSPS_AUTH_VECTOR_TYPE_TRIPLET  1
typedef enum  
{
	MSPS_AUTH_VECTOR_TYPE_TRIPLET,
    MSPS_AUTH_VECTOR_TYPE_QUADRUPLET,
    MSPS_AUTH_VECTOR_TYPE_QUINTUPLET
}MSPS_AV_TYPE_E;

/* Authentication Vector */
typedef struct
{
    MSPS_KSI_T  stKsi;

    MSPS_AV_TYPE_E   ucAuthVectorType;
    UINT8   ucNum;
    union
    {
        MSPS_AUTH_TRIPLET_T    stTripletList[MSPS_MAX_VECTOR_NUM]; /* MAX=5 */
		MSPS_AUTH_QUADRUPLET_T stQuadrupletList[MSPS_MAX_VECTOR_NUM];
        MSPS_AUTH_QUINTUPLET_T stQuintupletList[MSPS_MAX_VECTOR_NUM];
    }unAuthVector;
}MSPS_AUTH_VECTOR_T;

/****** ��Ȩ��ؽṹ���� END ******/

/****** MS Radio Access Capability ��ؽṹ���� BEGIN ******/
typedef struct
{
    BITS8   btAccessTechType :4; /* 4 */
    BITS8   btGmskPowerCls: 3;/* 3 */
    BITS8   bt8PskPowerCls: 2; /* 2 */
}MSPS_ADDI_ACCESS_CAPA_T;

typedef struct
{
    BITS8   btHscsdMsCls:5; /* 5 */
    BITS8   btDtmGprsMsCls:2; /* 2 */
    BITS8   btSingleSlotDtm:1; /* 1 */
    BITS8   btGprsMsCls:5; /* 5 */
    BITS8   btGprsExtDAllocCapa:1 ; /* 1 */
    BITS8   btDtmEgprsMsCls:2; /* 2 */
    BITS8   btSmsValue :4; /* 4 */
    BITS8   btSmValue:4; /* 4 */
    UINT8   ucEcsdMsCls:5; /* 5 */
    BITS8   btEgprsMsCls:5; /* 5 */
    BITS8   btEgprsExtDAllocCapa:1; /* 1 */
}MSPS_MULTISLOT_CAPA_T;

typedef struct
{
    BITS8 btRfPowerCapa:3; /* 3 */
    BITS8 btA51:1; /* 1 */
    BITS8 btA52:1; /* 1 */
    BITS8 btA53:1; /* 1 */
    BITS8 btA54:1; /* 1 */
    BITS8 btA55:1; /* 1 */
    BITS8 btA56:1; /* 1 */
    BITS8 btA57:1; /* 1 */
    BITS8   btEsInd:1; /* 1 */
    BITS8   btPs:1; /* 1 */
    BITS8   btVgcs:1; /* 1 */
    BITS8   btVbs:1; /* 1 */
    MSPS_MULTISLOT_CAPA_T    stMultislotCapa;
    BITS8   btPskPowerCapa:2; /* 2 */
    BITS8   btCptIntMeaCapa:1; /* 1 */
    BITS8   btRevLevelInd:1; /* 1 */
    BITS8   btUmtsFddRaTechCapa:1; /* 1 */
    BITS8   btU384McpsTddRaTechCapa:1; /* 1 */
    BITS8   btC2000RaTechCapa:1; /* 1 */
    BITS8   btU128McpsTddRaTechCapa:1; /* 1 */
    BITS8   btGeranFeaturePt:1; /* 1 */
    BITS8   btExtDtmGprsMsCls:1; /* 2 */
    BITS8   btExtDtmEgprsMsCls:1; /* 2 */
    BITS8   btModuBasedMsSpt:1; /* 1 */
}MSPS_ACCESS_CAPA_T;

typedef struct
{
    /* UINT8  ucLength; */ /* codec needed */
    UINT8  ucAccessTechType;
    union
    {
        /* if btAccessTechType !=1111 */
        MSPS_ACCESS_CAPA_T  stAccessCapa;

        /* if btAccessTechType =1111 */
        struct
        {
            /* BITS8 btLengh:7; */ /* codec needed */
            /* number of MSPS_ADDI_ACCESS_CAPA array, not codec */
            UINT8 ucNum;
            MSPS_ADDI_ACCESS_CAPA_T  astAddiAccessCapa[MSPS_MAX_ACCESS_CAPA_NUM];
        }stAddiAccessCap;
    }unAccessTech;
}MSPS_MS_RADIO_ACCESS_CAPA_VALUE_T;

typedef struct
{
    UINT8 ucRadioAccCapaNum;
    MSPS_MS_RADIO_ACCESS_CAPA_VALUE_T  astMsRadioAccess[MSPS_MAX_RADIO_ACCCAPA_NUM];
}MSPS_MS_RADIO_ACCESS_CAPA_T;

/****** MS Radio Access Capability ��ؽṹ���� END ******/

/* MS Network Capability 24008��10.5.5.12 */
#define  MSPS_MS_NETWORK_CAPA   8
typedef struct
{
		UINT8    ucLength;     //���� 2~15
		UINT8    aucVal[MSPS_MS_NETWORK_CAPA];

}MSPS_MS_NETWORK_CAPA_T;

/* DRX Parameter 24008��10.5.5.6 */
typedef struct
{
    UINT8  ucSplitPgCycleCode;
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btDrxCycleLenCoef:4;
        BITS8  btSplitOnCcch:1;
        BITS8  btNonDrxTimer:3;
    #else
        BITS8  btNonDrxTimer:3;
        BITS8  btSplitOnCcch:1;
        BITS8  btDrxCycleLenCoef:4;
    #endif
}MSPS_DRX_PARA_T;

/****** TRACE_DATA ��ؽṹ���� BEGIN ******/
/* 23060��13.2 */
typedef struct
{
    UINT16  usLength;
    UINT8   ucValue[2];
}MSPS_TRACE_REFERENCE_T;

/* ISDN Address String */
typedef struct
{
    UINT16  usLength;
    UINT8   aucValue[MSPS_MAX_ADDR_LEN];  /* MAX=20 */
}MSPS_ADDR_STRING_T;

/* TRACE DATA */
typedef struct
{
    /* Identifies a record or a collection of records for a particular trace. */
    MSPS_TRACE_REFERENCE_T  stTraceReference;

    /* Indicates the type of trace. */
    UINT16  ucTraceType;

    /* Identifies the entity that initiated the trace. omc-id*/
    MSPS_ADDR_STRING_T  stTriggerId;  /*20*/
}MSPS_TRACE_DATA_T;
/****** TRACE_DATA ��ؽṹ���� END ******/

/* ODB DATA,����29.002������м�����ṹ */
typedef struct
{
    /*MSPS_MAPU_SERVICE_GRANTED or MSPS_MAPU_SUBS_STATUS_ODB*/
    UINT8     ucSubsStatus;

    /* ����ODB��bit�� MSPS_PUB_TRUEΪ��Ч,MSPS_PUB_FALSEΪ��Ч*/
    BITS8     btSpecialOdbData1:1;
    BITS8     btSpecialOdbData2:1;
    BITS8     btSpecialOdbData3:1;
    BITS8     btSpecialOdbData4:1;
    BITS8     btReserved:4;

    /* ����ODB��bit��*/
    BITS32    btAllOgCalls:1;
    BITS32    btInerOgCalls:1;
    BITS32    btInterOgCallsNotToHplmnCtry:1;
    BITS32    btInterZonalOgCalls:  1;
    BITS32    btInterZonalOgCallsNotToHplmnCtry:1;
    BITS32    btInterZonalInterOgCallsNotToHplmnCtry:1;
    BITS32    btAllPs:1;
    BITS32    btRoamerAccessHplmnAp:1;
    BITS32    btRoamerAccessVplmnAp  :1;
    BITS32    btReserved2:23;
}MSPS_ODB_DATA_T;

/****** GPRS_CSI ��ؽṹ���� BEGIN ******/

typedef struct
{
    UINT16  length;
    UINT8   value[MSPS_MAX_ISDN_STRING_LEN];
}MSPS_ISDN_AddressString;

typedef struct
{
    UINT8    ucGprsTdp;  /* macro defined */
    UINT32   ulServiceKey;
    MSPS_ISDN_AddressString    stGsmScfAddress;
    UINT8    ucDefaultSessionHandling;  /* macro  defined */
}MSPS_GPRS_CAMEL_TDP_DATA_T;

typedef struct
{
    MSPS_GPRS_CAMEL_TDP_DATA_T  stGprsCamelTDPDataList[MSPS_MAX_NUM_OF_CAMEL_TDPDATA]; /* optional */
    UINT16    usCamelCapabilityHandling;  /*  optional; */
}MSPS_GPRS_CSI_T ;
/****** GPRS_CSI ��ؽṹ���� END ******/

/****** SMS_CSI ��ؽṹ���� BEGIN ******/
/* TDP LIST�е�ÿ��TDP���� */
typedef struct
{
    UINT8                   ucSmsTdp;             /* ����Ϣ�е�TDP */
    MSPS_ISDN_AddressString  stGsmsScfAddress;      /* gsmSCF address */
    UINT32      ulServiceKey;      /* service key INTEGER (0..2147483647) */
    UINT8       ucDefaultSmsHandling;             /* DefaultSmsHandling, */
} MSPS_SMS_CAMEL_TDP_DATA_T;

typedef struct
{
    MSPS_SMS_CAMEL_TDP_DATA_T astSmsTdpList[MSPS_MAX_NUM_OF_CAMEL_TDPDATA];  /* TDP LIST */
    UINT16   usCamelCapabilityHandling;      /* CAMEL�Ľ׶κ� */
}MSPS_SMS_CSI_T;

/* ÿ��DP������Ӧ��DP Criterion */
typedef struct
{
    UINT8       ucSmsTdp;         /* MT�е�TDP */
    UINT8       ucDpCriterionNum;   /* ֧�ֵ�DP��׼������ */

    /* �����ÿ����Ԫ����һ��DP��׼����һ��TPDU */
    UINT8       aucDpCriterion[MSPS_MAX_NUM_OF_TPDUTYPES];  /* max=5 */
}MSPS_SMS_MT_TDP_CRITERION_LIST_T;

typedef MSPS_SMS_CSI_T   MSPS_MO_SMS_CSI_T;

typedef struct
{
    MSPS_SMS_CSI_T   stMtSmsCsi;
    MSPS_SMS_MT_TDP_CRITERION_LIST_T  astMtTdpCriterionList[MSPS_MAX_NUM_OF_CAMEL_TDPDATA];
}MSPS_MT_SMS_CSI_T;
/****** SMS_CSI ��ؽṹ���� END ******/

/* MG CSI */
typedef struct
{
    /* MM-code����,macro defined */
    UINT8    ucMobilityTriggerNum;  /* ��������ĳ��� */    
    UINT8    aucMobilityTriggers[MSPS_MAX_NUM_OF_MOBILITYTRIGGERS];   /* MAX=10 */
    UINT32   ulServiceKey;    /* service key INTEGER (0..2147483647) */
    MSPS_ISDN_AddressString    stGsmScfAddress;             /* gsmSCF address */
}MSPS_MG_CSI_T ;

/* Teleservice Code ,����29.002������м�����ṹ */
typedef struct
{
    /* ����bit����,MSPS_PUB_TRUEΪ��Ч,MSPS_PUB_FALSEΪ��Ч*/
    /* ------------����ҵ��------------------ */
    BITS32    btAllTeleservices : 1 ;
    /* ------------����Ϣ-------------------- */
    BITS32    btAllSms: 1 ;
    BITS32    btSmsMtPp: 1 ;
    BITS32    btSmsMoPp: 1 ;
    BITS32    btSmsCbs: 1;  /* Cell Broadcast  Service */
    /* ------------����----------------------- */
    BITS32    btAllFacsimileTransmServices: 1 ;
    BITS32    btFacsimileGroup3AndAlterSpeech: 1 ;
    BITS32    btAutomaticFacsimileGroup3: 1 ;
    BITS32    btFacsimileGroup4: 1 ;
    /* ------------����ҵ��------------------- */
    BITS32    btAllDataTeleservices: 1 ;
    BITS32    btAllTeleservices_ExeptSMS: 1 ;
    /* ------------Reserved----------------- */
    BITS32    btReserved:21;


    /* ------------��չ����---------------------- */
    BITS16    btAllPlmnSpecificTs:1;
    BITS16    btPlmnSpecificTs1:1;
    BITS16    btPlmnSpecificTs2:1;
    BITS16    btPlmnSpecificTs3:1;
    BITS16    btPlmnSpecificTs4:1;
    BITS16    btPlmnSpecificTs5:1;
    BITS16    btPlmnSpecificTs6:1;
    BITS16    btPlmnSpecificTs7:1;
    BITS16    btPlmnSpecificTs8:1;
    BITS16    btPlmnSpecificTs9:1;
    BITS16    btPlmnSpecificTsA:1;
    BITS16    btPlmnSpecificTsB:1;
    BITS16    btPlmnSpecificTsC:1;
    BITS16    btPlmnSpecificTSD:1;
    BITS16    btPlmnSpecificTSE:1;
    BITS16    btPlmnSpecificTSF:1;
}MSPS_EXT_TELE_SERVICE_T;

/* ZoneCode List */
typedef struct
{
    UINT8    ucZcNum;   /* number of ZC list */
    UINT16   ausZcList[MSPS_MAX_NUM_OF_ZONE_CODE] ;
}MSPS_ZONECODE_LIST_T;

/****** LSA INFO ��ؽṹ���� BEGIN ******/
typedef struct
{
    /* 1byte LSA attribute and 3bytes LSA Identity */
    UINT32    ulLsaIdentiryAtribute;

    /* �Ƿ��û����ڵ�cell���ڵ�LSA��ָʾΪactive mode */
    UINT8     ucLsaActiveModeIndicator;
}MSPS_LSA_DATA_INFO_T;

typedef struct
{
    /* 1byte LSA attribute and 3bytes LSA Identity */
    UINT8    ucLsaListLength;    /* ��LSA_Info Ϊ��λ����ʾ�м���LSA_Info */
    MSPS_LSA_DATA_INFO_T astLsaData[MSPS_MAX_NUM_OF_LSA];
}MSPS_LSA_DATA_LIST_T;

typedef struct
{
    /* �û��Ƿ�ֻ������ǩԼ��LSA�ڻ;���bit��Ч
     * 1:only access to the LSAs that are defined by the LSA Infor element */
    UINT8       ucLsaOnlyAccessIndicator;
    MSPS_LSA_DATA_LIST_T    stLsaDataList;
}MSPS_LSA_INFO_T;
/****** LSA INFO ��ؽṹ���� END ******/

/****** LCS INFO ��ؽṹ���� BEGIN ******/
typedef UINT8 MSPS_SS_CODE_T;

typedef struct
{
    /* 0�ֽ�5��8BITΪ0000��0�ֽڵ�BIT1���� A,  BIT2���� R,  BIT3���� P,
     * BIT4 ����Q,1-4�ֽ�Ԥ��, */
    UINT8    ucSsStatus[MSPS_MAX_NUM_SS_STATUS];  /* MAX=5 */
}MSPS_SS_STATUS_T;

typedef struct
{
    MSPS_ADDR_STRING_T   stClientIdentity;
    /* macro defined, 0:gmlc_List, 1:home_Country */
    UINT8   ucGmlcRestriction;
    /* If notificationToMSUser is not received, the default value according to */
    /* 3GPP TS 23.271 shall be assumed. */
    UINT8    ucNotifiToMsUser; /* macro defined,0-3 */
}MSPS_EXT_CLIENT_T;

typedef struct
{
    MSPS_SS_STATUS_T   stCallSessionRelate;
    UINT8             ucNotifToMsUser;  /* macro defined,0-3 */
    UINT8             ucExtClientNum;   /* ���������Ԫ�ظ��� */
    MSPS_EXT_CLIENT_T  stExtClientList[MSPS_MAX_NUM_EX_CLIENT];   /* MAX=5 external client */
    UINT8             ucExtExternalClientNum;   /* ���������Ԫ�ظ���  */    
    MSPS_EXT_CLIENT_T  stExtenExternalClientList[MSPS_MAX_NUM_EX_EX_CLIENT];  /* MAX=35 extension external client */
}MSPS_CALL_SESS_T;

typedef struct
{
    MSPS_SS_STATUS_T    stPlmnOperator;
    UINT8    ucClientIdNum;    /* ���������Ԫ�ظ���  */
    UINT8    ucLcsClientInterId[MSPS_MAX_NUM_CLIENT_INTERID];    /*MAX=5 macro defined */
}MSPS_PLMN_OP_T;

typedef struct
{
    MSPS_SS_CODE_T     stUniversalSsCode;
    MSPS_SS_STATUS_T  stUniversal;
    MSPS_SS_CODE_T     stCallSessionReltSsCode;
    MSPS_CALL_SESS_T stCallSessionRelt; /* Call/Session Related Privacy Class */
    MSPS_SS_CODE_T     stCallSessionUnreltSsCode;
    MSPS_CALL_SESS_T stCallSessionUnrelt; /* Call/Session  unrelated */
    MSPS_SS_CODE_T     stPlmnOperatorSsCode;
    MSPS_PLMN_OP_T  stPlmnOperator;
}MSPS_PRIVACY_EXCEP_T;

typedef struct
{
    MSPS_SS_CODE_T     stBasicLocSsCode;
    MSPS_SS_STATUS_T stBasicLoc; /* Basic Self Location Class */
    MSPS_SS_CODE_T     stAutoLocSsCode;
    MSPS_SS_STATUS_T  stAutoLoc;    /* Autonomous Self Location Class */
    MSPS_SS_CODE_T     stTran3rdPartySsCode;
    MSPS_SS_STATUS_T stTran3rdParty;   /* Transfer to Third Party Class */
}MSPS_MO_LR_LIST_T;

typedef struct
{
    UINT8   ucGmlcAddNum;
    MSPS_ISDN_AddressString  stGmlcAddr[MSPS_MAX_NUM_GMLC]; /* MAX=5 */
    MSPS_PRIVACY_EXCEP_T     stPrivExcepClass;
    MSPS_MO_LR_LIST_T        stMolrList;
}MSPS_LCS_INFO_T;
/******LCS INFO ��ؽṹ���� END ******/

/***** GTPC GMM CONTEXT ��ؽṹ���� BEGIN ******/
typedef struct
{
    UINT32       ulLength;
    UINT8        aucFill[MSPS_GTPC_CONTAINER_MAX_LEN];
}MSPS_GTPC_CONTAINER_T;

/* GMM_CTXT_GSM_TRIPLET */
typedef struct
{
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btSpare:5;
        BITS8  btCksn:3;
        BITS8  btSecurityMode:2;
        BITS8  btVectorNum:3;
        BITS8  btUsedCiph:3;
    #else
        BITS8  btCksn:3;
        BITS8  btSpare:5;
        BITS8  btUsedCiph:3;
        BITS8  btVectorNum:3;
        BITS8  btSecurityMode:2;
    #endif
    MSPS_KC_T  stKc;
    MSPS_AUTH_TRIPLET_T  stTriplet[MSPS_MAX_TRIPLET_NUM];  /*MAX=5*/
    MSPS_DRX_PARA_T  stDrxPara;
    UINT8  ucLength;
    MSPS_MS_NETWORK_CAPA_T  stMsNetworkCapa;
    UINT16  usContainerLength;
    MSPS_GTPC_CONTAINER_T  stContainer;
}MSPS_GMM_CTXT_GSM_TRIPLET_T;

/* GMM_CTXT_UMTS_QUINTUPLET */
typedef struct
{
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btSpare1:5;
        BITS8  btKsi:3;
        BITS8  btSecurityMode:2;
        BITS8  btVectorNum:3;
        BITS8  btSpare2:3;
    #else
        BITS8  btKsi:3;
        BITS8  btSpare1:5;
        BITS8  btSpare2:3;
        BITS8  btVectorNum:3;
        BITS8  btSecurityMode:2;
    #endif
    MSPS_CK_T  stCk;
    MSPS_IK_T  stIk;
    UINT16  usQuintupLength;
    MSPS_AUTH_QUINTUPLET_T  stQuintuplet[MSPS_MAX_QUINTUPLET_NUM]; /*MAX=5*/
    MSPS_DRX_PARA_T  stDrxPara;
    UINT8  ucLength;
    MSPS_MS_NETWORK_CAPA_T  stMsNetworkCapa;
    UINT16  usContainerLength;
    MSPS_GTPC_CONTAINER_T  stContainer;
}MSPS_GMM_CTXT_UMTS_QUINTUPLET_T;

/* GMM_CTXT_GSM_QUINTUPLET  */
typedef struct
{
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btSpare:5;
        BITS8  btCksn:3;
        BITS8  btSecurityMode:2;
        BITS8  btVectorNum:3;
        BITS8  btUsedCiph:3;
    #else
        BITS8  btCksn:3;
        BITS8  btSpare:5;
        BITS8  btUsedCiph:3;
        BITS8  btVectorNum:3;
        BITS8  btSecurityMode:2;
    #endif
    MSPS_KC_T  stKc;
    UINT16  usQuintupLength;
    MSPS_AUTH_QUINTUPLET_T  stQuintuplet[MSPS_MAX_QUINTUPLET_NUM]; /*MAX=5*/
    MSPS_DRX_PARA_T  stDrxPara;
    UINT8  ucLength;
    MSPS_MS_NETWORK_CAPA_T  stMsNetworkCapa;
    UINT16  usContainerLength;
    MSPS_GTPC_CONTAINER_T  stContainer;
}MSPS_GMM_CTXT_GSM_QUINTUPLET_T;

/* GMM_CTXT_UMTS_QUINTUPLET_U_CIPH */
typedef struct
{
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btSpare:5;
        BITS8  btKsiOrCksn:3;
        BITS8  btSecurityMode:2;
        BITS8  btVectorNum:3;
        BITS8  btUsedCiph:3;
    #else
        BITS8  btKsiOrCksn:3;
        BITS8  btSpare:5;
        BITS8  btUsedCiph:3;
        BITS8  btVectorNum:3;
        BITS8  btSecurityMode:2;
    #endif
    MSPS_CK_T  stCk;
    MSPS_IK_T  stIk;
    UINT16  usQuintupLength;
    /*MAX=5*/
    MSPS_AUTH_QUINTUPLET_T  stQuintuplet[MSPS_MAX_QUINTUPLET_NUM];
    MSPS_DRX_PARA_T  stDrxPara;
    UINT8  ucLength;
    MSPS_MS_NETWORK_CAPA_T  stMsNetworkCapa;
    UINT16  usContainerLength;
    MSPS_GTPC_CONTAINER_T  stContainer;
}MSPS_GMM_CTXT_UMTS_QUINTUPLET_U_CIPH_T;

/* GMM_CONTEXT 29060��7.7.28 */
typedef struct
{
    UINT8  ucGmmCtxtType;
    union
    {
        MSPS_GMM_CTXT_GSM_TRIPLET_T  stGsmTripMmCtxt;
        MSPS_GMM_CTXT_UMTS_QUINTUPLET_T  stUmtsQuintupMmCtxt;
        MSPS_GMM_CTXT_GSM_QUINTUPLET_T  stGsmQuintupMmCtxt;
        MSPS_GMM_CTXT_UMTS_QUINTUPLET_U_CIPH_T  stUmtsQuintupUciphCtxt;
    }unMmCtxt;
}MSPS_CODEC_GTPC_GMM_CXT_T;
/****** GTPC GMM CONTEXT ��ؽṹ���� END ******/

/* Radio Priority */
typedef struct
{
    #ifdef  MSPS_BIG_ENDIAN
        BITS8 btNsapi:4;
        BITS8 btSpare:1;
        BITS8 btRadionPriority:3;
    #else
        BITS8 btRadionPriority:3;
        BITS8 btSpare:1;
        BITS8 btNsapi:4;
    #endif
}MSPS_RADIO_PRIORITY_T;

typedef struct
{
    MSPS_AUTH_VECTOR_T  stAuthVector; /* ��SGSN�б���ļ�Ȩ����  */
    UINT8              ucCksnKsi;      /* ��ǰ��cskn��ksi           */
    UINT8              ucUsedCiphExist;
    UINT8              ucUsedCiph;     /* ��ǰʹ�õ�GSM ciph algorithm */
    UINT8              ucKcExist;
    MSPS_KC_T           stKc;
    UINT8              ucIkExist;
    MSPS_IK_T           stIk;
    UINT8              ucCkExist;
    MSPS_CK_T           stCk;
    MSPS_DRX_PARA_T         stDrxPara;
    MSPS_MS_NETWORK_CAPA_T  stMsNetworkCapa;
    UINT8                     ucContainerExist;
    MSPS_GTPC_CONTAINER_T   stContainer;  /*  ��չ�� */
}MSPS_BASE_GMM_CONTEXT_T;

/************ PS��ϵͳ��RDBS GMM CONTEXT ��ع����ṹ���� END ************/

/************ PS��ϵͳ��RDBS PDP CONTEXT ��ع����ṹ���� BEGIN ************/
typedef union
{
    UINT8   ucIpAddr4[MSPS_IP_ADDR_V4_LEN];       /* IPv4   */
    UINT8   ucIpAddr16[MSPS_IP_ADDR_V6_LEN];     /* IPv6   */
}MSPS_GTPC_ADDR_U;

/* IP Address struct */
/* Begin wangxiaofeng modify 2010/11/22  for dual ip */
typedef struct 
{
    UINT8    ucType;         /* 0,IPV4; 1,IPV6;2,IPV4&V6 */
    UINT8    ucValid;         /* 0, invalid; 1, valid*/
    UINT8    ucIpPrefix;     /* IPǰ׺�����û�У���0,  */
        /* modify by qiyalong 2013-0305 for IPV6 begin*/
    BITS8    btActIpType:4;    /* ��¼��ǰ�豸����ʹ�õ�IP���� 0,�������ȼ�ѡ;1,IPV4; 2,IPV6��
                              ��ucTypeΪIPV4&V6��Ч */
    UINT8    ucRvs;
    /* modify by qiyalong 2013-0305 for IPV6 end */
    
    /* single(v4 or v6 ) ip addr OR  ipv6 addr under dual ip */  
    UINT8    aucIp[MSPS_IP_ADDR_V6_LEN];
    /* for dual ip only ,v4 addr  */
    UINT8    aucIpV4[MSPS_IP_ADDR_V4_LEN];
}MSPS_IP_ADDR_T;

/* End wangxiaofeng modify 2010/11/22  for dual ip */


typedef MSPS_IP_ADDR_T   MSPS_PDP_ADDR_T;
typedef MSPS_IP_ADDR_T   MSPS_GSN_ADDR_T;	/* GSN address */
typedef MSPS_IP_ADDR_T   MSPS_CG_ADDR_T;	/* Charging Gateway Address */
typedef MSPS_IP_ADDR_T   MSPS_RNC_ADDR_T;	/* RNC Address */

/****** QOS profile ��ؽṹ���� BEGIN ******/
/* Base Qos  */
typedef struct
{
	#ifdef MSPS_BIG_ENDIAN
	    BITS8   btSpare1:2;
	    BITS8   btDelayClass:3;     /* ��ʱ����   */
	    BITS8   btReliablility:3;   /* �ɿ��Լ��� */

	    BITS8   btPeakThroughput:4;   /* ��ֵ������ */
	    BITS8   btSpare2:1;
	    BITS8   btPrecedenceClass:3;   /* ���ȼ�   */

	    BITS8   btSpare3:3;
	    BITS8   btMeanThroughput:5;   /* ƽ�������� */
	#else

	    BITS8   btReliablility:3;   /* �ɿ��Լ��� */
	    BITS8   btDelayClass:3;     /* ��ʱ����   */
	    BITS8   btSpare1:2;

	    BITS8   btPrecedenceClass:3; /* ���ȼ�   */
	    BITS8   btSpare2:1;
	    BITS8   btPeakThroughput:4;   /* ��ֵ������ */

	    BITS8   btMeanThroughput:5;   /* ƽ�������� */
	    BITS8   btSpare3:3;
	#endif
}MSPS_BASE_QOS_T;

/* Extended Qos */
#define MSPS_MAX_BITRATE_LEN 5
typedef struct
{
    #ifdef MSPS_BIG_ENDIAN
        BITS8    btTrafficClass:3;          /* ע�� 1        */
        BITS8    btDeliveryOrder:2;         /* ����˳��       */
        BITS8    btDeliveryOfErrSdu:3;      /* �Ƿ�������SDU    */

        UINT8   ucMaxSduSize;               /* ���SDU����      */
        UINT8   ucMaxUlBitrate[MSPS_MAX_BITRATE_LEN];             /* �����������     */
        UINT8   ucMaxDlBitrate[MSPS_MAX_BITRATE_LEN];             /* �����������     */

        BITS8   btResidualBer:4;            /*Residual BER ����������*/
        BITS8   btSduErrRatio:4;            /*SDU Error ratio SDU������ */

        BITS8   btTransferDelay:6;          /* ����ʱ��       */
        BITS8   btTrafficPriority:2;        /* �������ȼ�     */

        UINT8   ucGuarantUlBitrate[MSPS_MAX_BITRATE_LEN];         /* ���б�֤����     */
        UINT8   ucGuarantDlBitrate[MSPS_MAX_BITRATE_LEN];         /* ���б�֤����     */
    #else
        BITS8  btDeliveryOfErrSdu:3;        /* �Ƿ�������SDU    */
        BITS8  btDeliveryOrder:2;           /* ����˳��       */
        BITS8  btTrafficClass:3;            /* ��������       */

        UINT8   ucMaxSduSize;               /* ���SDU����      */
        UINT8   ucMaxUlBitrate[MSPS_MAX_BITRATE_LEN];             /* �����������     */
        UINT8   ucMaxDlBitrate[MSPS_MAX_BITRATE_LEN];             /* �����������     */

        BITS8   btSduErrRatio:4;            /*SDU Error ratioSDU������  */
        BITS8   btResidualBer:4;            /*Residual BER����������  */

        BITS8   btTrafficPriority:2;        /* �������ȼ�     */
        BITS8   btTransferDelay:6;          /* ����ʱ��       */

        UINT8   ucGuarantUlBitrate[MSPS_MAX_BITRATE_LEN];         /* ���б�֤����     */
        UINT8   ucGuarantDlBitrate[MSPS_MAX_BITRATE_LEN];         /* ���б�֤����     */
    #endif

/**
ע��1 :��������
#define     MSPS_QOS_CONVERSATIONAL_TRAFFIC      1
#define     MSPS_QOS_STREAMING_TRAFFIC           2
#define     MSPS_QOS_INTERACTIVE_TRAFFIC         3
#define     MSPS_QOS_BACKGROUND_TRAFFIC          4
*/
}MSPS_EXTENDED_UMTS_QOS_T;
/* 2011-12-13 */
/* 29212-b20 5.3.46 */
typedef enum 
{
    PRE_EMPTION_CAPABILITY_ENABLED = 0,     /* ������ռ */
    PRE_EMPTION_CAPABILITY_DISABLED         /* ��������ռ */
}MSPS_PRE_EMP_CAP_E;
/* 29212-b20 5.3.47 */
typedef enum 
{
    PRE_EMPTION_VULNERABILITY_ENABLED = 0,  /* ������ռ */
    PRE_EMPTION_VULNERABILITY_DISABLED      /* ��������ռ */
}MSPS_PRE_EMP_VUL_E;
/* added by liuyang 2009-08-23 below */
typedef struct
{
	UINT32                    uiPriorityLevel;   /* MSPS_PUB_EXIST or MSPS_PUB_NONEXIST */
	INT32                     iPreEmpCap;
	INT32                     iPreEmpVul;
}EPS_ARP;
/* added by liuyang 2009-08-23 above */
/* NAS QoS Profile */
typedef struct
{
	UINT8                    ucExtendedQosFg;   /* MSPS_PUB_EXIST or MSPS_PUB_NONEXIST */
	UINT8                    ucArpExist;
	UINT8                    ucQCI;
	UINT8                    ucArp;
	/*add by wangzhao 2009-09-18 begin*/
	EPS_ARP                   stArp;
	/*add by wangzhao 2009-09-18 end*/	
    MSPS_BASE_QOS_T           stBaseQos;
    MSPS_EXTENDED_UMTS_QOS_T  stExtendedQos;
}MSPS_NAS_QOS_PROFILE_T;

/* 2008-10-24 */
typedef	MSPS_NAS_QOS_PROFILE_T	MSPS_EPS_BEARER_QOS_T;	/* FFS */
typedef	MSPS_NAS_QOS_PROFILE_T	MSPS_HSS_QOS_PROFILE_T;	/* FFS */
typedef	MSPS_NAS_QOS_PROFILE_T	MSPS_SUBSCRIBED_QOS_T;	/* FFS */
/* QoS Profile */

typedef enum
{
    MSPS_RANAP_SPEECH = 0,
    MSPS_RANAP_UNKNOW = 1
}MSPS_RANAP_SRC_STATC_DSCR_E;

typedef enum
{
    MSPS_RANAP_LOSSLESS = 0,
    MSPS_RANAP_NONE = 1,
    MSPS_RANAP_REALTIME = 2
}MSPS_RANAP_RLCREQUIR_E;

typedef enum
{
    MSPS_RANAP_SHALL_NOT_TRIGGER_PRE_EMPTION = 0,
    MSPS_RANAP_MAY_TRIGGER_PRE_EMPTION = 1
}MSPS_RANAP_PRE_EMP_CAPA_E;

typedef enum
{
    MSPS_RANAP_NOT_PRE_EMPTABLE = 0,
    MSPS_RANAP_PRE_EMPTABLE = 1
}MSPS_RANAP_PRE_EMP_VULN_E;

typedef enum
{
    MSPS_RANAP_QUEUEING_NOT_ALLOWED = 0,
    MSPS_RANAP_QUEUEING_ALLOWED = 1
}MSPS_RANAP_QUEU_ALLOW_E;
typedef struct
{
    UINT16          usPriorityLevel;
    MSPS_RANAP_PRE_EMP_CAPA_E  ePreEmpCapa;
    MSPS_RANAP_PRE_EMP_VULN_E  ePreEmpVuln;
    MSPS_RANAP_QUEU_ALLOW_E  eQueuAllow;
}MSPS_RANAP_ALLOC_OR_RETENT_PRIOR_T;

typedef enum
{
    MSPS_RANAP_SYM_BIDIR = 0,
    MSPS_RANAP_ASYM_UNIDIR_DL = 1,
    MSPS_RANAP_ASYM_UNIDIR_UPLINK = 2,
    MSPS_RANAP_ASYM_BIDIR = 3
}MSPS_RANAP_RAB_ASYM_IND_E;

typedef struct
{
    UINT16          usMantissa;
    UINT16          usExponent;
}MSPS_RBER_T;

typedef struct
{
    UINT16          usMantissa;
    UINT16          usExponent;
}MSPS_SER_T;

typedef enum {
    MSPS_YES = 0,
    MSPS_NO = 1,
    MSPS_NO_ERROR_DETECTION_CONSIDERATION = 2
}MSPS_DELIVERY_OF_ERRSDU_E;

typedef struct
{
    /* 0��ʾ������;1��ʾ���� */
    UINT8               ucSubflowSduSizeFg;
    UINT8               ucRabSubflowCombBrFg;
    UINT16              usSubflowSduSize;
    UINT32              ulRabSubflowCombBr;
}MSPS_SDU_FORMATINFOPARA_T;

typedef struct
{
    /* 0��ʾ������;1��ʾ���� */
    UINT8               ucSduErFg;/* �����SDU������ */
    /* ���������� */
    MSPS_RBER_T          stResidualBer;
    /* SDU������ */
    MSPS_SER_T           stSduEr;
    /* ����SDU�Ĵ���ʽ */
    MSPS_DELIVERY_OF_ERRSDU_E    eErrSdu;
    /* ������ϸ��� */
    UINT8                       ucSubflowCombNum;/* ucSubflowCombNumΪ������MSPS_SDU_FORMATINFOPARA_T������ */
    MSPS_SDU_FORMATINFOPARA_T    astSduFormatInfoPara[MSPS_SUBFLOW_COMB_MAX_NUM];
}MSPS_RANAP_SDUPARA_T;

/* TAN modified begin */
/* ICN30 modified for CS 20070413  begin */
typedef struct
{
    MSPS_RANAP_SDUPARA_T             astSduPara[MSPS_SUBFLOW_MAX_NUM];
}MSPS_RANAP_SDUPARA_ARRAY_T;  
/* ICN30 modified for CS 20070413  end  */
/* TAN modified end   */  
typedef struct
{
    UINT8 ucSrcStatcDescrptorFg;
    UINT8 ucRlcRequireFg;
    UINT8  ucAllocOrRetentPriorFg;
    /*SourceStatisticsDescriptor��������*/
    MSPS_RANAP_SRC_STATC_DSCR_E  eSrcStatcDecriptor;
    /*RAB_AsymmetryIndicator������Ϊ��ѡ*/
    MSPS_RANAP_RAB_ASYM_IND_E    stRabAsymInd;
    /*AllocationOrRetentionPriority*/
    MSPS_RANAP_ALLOC_OR_RETENT_PRIOR_T stAllocOrRetentPrior;
    /*RelocationRequirement*/
    MSPS_RANAP_RLCREQUIR_E     eRlcRequire;
    
    #ifdef MSPS_RANAPCODE_FOR_CS
        /* �˴���ӹ�������SDU����Ԫ */
        UINT8   ucSubFlowNum;/* ����������Ϊ������ʾMSPS_RANAP_SDUPARA_T������ */
        /* TAN modified begin */
        /* ICN30 modified for CS 20070413  begin */
        /*MSPS_RANAP_SDUPARA_T             astSduPara[MSPS_SUBFLOW_MAX_NUM];*/
        MSPS_RANAP_SDUPARA_ARRAY_T         *pstSduParaArray;
        /* ICN30 modified for CS 20070413  end   */
        /* TAN modified end   */
    #endif 
}MSPS_RAB_QOS_EXT_T;

typedef struct
{
    UINT8         ucAllocationPriority;
    MSPS_NAS_QOS_PROFILE_T   stNasQos;
    MSPS_RAB_QOS_EXT_T       stRabExtOos;
}MSPS_QOS_PROFILE_T;
/****** QOS profile ��ؽṹ���� END ******/

/****** APN ��ؽṹ���� BEGIN ******/
/* APN Network Identifier */
typedef struct
{
    UINT8   ucApnNiLen;               /* [0-63]  0 ��ʾû��NI ���� */
    UINT8   aucApnNi[MSPS_MAX_APN_NI_LEN];
}MSPS_APN_NI_T;

/* APN Operator Identifier */
typedef struct
{
    UINT8   ucApnOiLen;             /* [0-37 ]  0 ��ʾû��OI ����  OI����ʵ�ʳ��� */
    UINT8   aucApnOi[MSPS_MAX_APN_OI_LEN];
}MSPS_APN_OI_T;

/* APN */
typedef struct
{
    MSPS_APN_NI_T    stApnNi;  /* �����ʶ NI    */
    MSPS_APN_OI_T    stApnOi;  /* ��Ӫ�̱�ʶ OI  */
}MSPS_APN_T;
/****** APN ��ؽṹ���� END ******/

/* Begin wangxiaofeng modify 2012-1-8 */

typedef struct
{
    UINT16      usQosProfileDataLength;  /* QosProfileDataLength���ȵ���3��11��12����12���������according to TS29.060v5e0 77.34 */
    UINT8       ucAllocationRetentionPriority;  
    BITS8       btSpare1:2;
    BITS8       btDelayClass:3;
    BITS8       btReliabilityClass:3;

    BITS8       btPeakThroughPut:4;
    BITS8       btSpare2:1;
    BITS8       btPrecedenceClass:3;

    BITS8       btSpare3:3;
    BITS8       btMeanThroughPut:5;

    BITS8       btTrafficClass:3;
    BITS8       btDeliveryOrder:2;
    BITS8       btDeliveryOfErrSDU:3;

    UINT8       ucMaxSDUSize;
    UINT8       ucUlMBR;
    UINT8       ucDlMBR;

    BITS8       btResidualBER:4;
    BITS8       btSDUErrRatio:4;

    BITS8       btTransferDelay:6;
    BITS8       btTrafficHandingPrior:2;

    UINT8       ucUlGBR;
    UINT8       ucDlGBR;

    BITS8       btSpare4:3;
    BITS8       btSignalInd:1;
    BITS8       btSrcStatisDescrip:4;

    UINT8       ucDlMBRExtend;
    UINT8       ucDlGBRExtend;
    UINT8       ucUlMBRExtend;
    UINT8       ucUlGBRExtend;

}MSPS_NEG_QOS_T; /*  �ṹͬ��SWP_V1_QOS_T*/

/* End wangxiaofeng modify 2012-1-8 */


/* Charging Characteristics  */
typedef  UINT16 MSPS_CHARGING_CHARAC_T;

/* End User Address */
typedef struct
{
    UINT8     ucLength;     /* length      len = 2 ��ʾû��PDP Address */
    #ifdef  MSPS_BIG_ENDIAN
        BITS8     btSpare:4;      /* Spare 1111     */
        BITS8     btPdpTypeOrg:4;   /* PDP Type Org   MSPS_PDP_TYPE_ORG_ETSI... */
    #else
        BITS8     btPdpTypeOrg:4;   /* PDP Type Org   */
        BITS8     btSpare:4;      /* Spare 1111     */
    #endif
    UINT8         ucPdpTypeNum;   /* PDP Type Number  MSPS_PDP_TYPE_PPP...*/
    MSPS_PDP_ADDR_T      stPdpAddr;      /* PDP Address    */
}MSPS_END_USER_ADDR_T;

/* Protocol Configuration Options */
typedef struct
{
    UINT8   ucLen;          /* PCOʵ�ʳ���   */
    UINT8   aucPco[MSPS_MAX_PCO_LEN];
}MSPS_PCO_T;

/****** TFT ��ؽṹ���� BEGIN ******/
typedef union
{
    UINT8   ucAddrMask4[4];     /* IPv4����         */
    UINT8   ucAddrMask16[16];     /* IPv6����         */
}MSPS_GTPC_ADDRMASK_U;

/* IPv4 source address type �� IPv6 source address type */
typedef struct
{
    MSPS_GTPC_ADDR_U       unIpAddr;
    MSPS_GTPC_ADDRMASK_U   unAddrMask;
}MSPS_GTPC_SOURCE_ADDR_TYPE_T;

/* Destination port range type �� Source port range type */
typedef struct
{
    UINT16    usLowLimit;         /* �������       */
    UINT16    usHighLimit;        /* �������       */
}MSPS_GTPC_PORT_RANGE_TYPE_T;

/* Type of service/Traffic class type */
typedef struct
{
    UINT8   ucTosOrTCType;      /* ����           */
    UINT8   ucMask;         /* ����           */
   /*  UINT8   aucReserved[2];  */ /* �����ֽ�         */
}MSPS_GTPC_TOS_OR_TC_TYPE_T;

/* Packet filter contents */
typedef struct
{
    UINT32    ulSecParaIndex;       /* ��ȫ��������     */
    UINT16    usSourcePortType;     /* Դ�˿�����         */
    UINT16    usDestPortType;       /* Ŀ�Ķ˿�����         */
    UINT16    usComponentFlag;      /* Ԫ�ش��ڱ�ʶ */
    UINT8     aucFlowLabelType[3];  /* Flow Label Type      */
    UINT8     ucProtocolType;       /* IPv4��IPv6��չ����  */
    MSPS_GTPC_SOURCE_ADDR_TYPE_T   stSourceAddrType;
    MSPS_GTPC_PORT_RANGE_TYPE_T    stSourcePortRangeType;
    MSPS_GTPC_PORT_RANGE_TYPE_T    stDestPortRangeType;
    MSPS_GTPC_TOS_OR_TC_TYPE_T     stTOSorTCType;
}MSPS_PACKET_FILTER_CONTENTS_T;

typedef struct
{
    UINT8   ucPacketFilterId;   /* identifier       */
    UINT8   ucPrecedence;     /*  evaluation precedence   */
    UINT8   ucReserved;       /* �����ֽ�         */
    /* Packet filter contents */
    MSPS_PACKET_FILTER_CONTENTS_T  stPacketFilterContent;
}MSPS_PACKET_FILTER_T;

/* Traffic Flow Template (TFT) */
/* Parameter list */

#define MSPS_MAX_PARAMETER_NUM     4   /* TFT IEЯ��packet filter������ */
typedef struct
{
	UINT8   ucPacketFilterId;       /* identifier       */
	UINT8   ucLength;               /* Length           */	
	/* Parameter list contents */
	UINT8   aucParameterContent[MSPS_MAX_PARAMETER_NUM];
}MSPS_PARAMETER_T;
#define MSPS_MAX_PF_NUM     8   /* TFT IEЯ��packet filter������ */
#define MSPS_MAX_PR_NUM     8   /* TFT IEЯ��Parameter list������ */

typedef struct{
        BITS8   btOperateCode:3;    /* TFT operation code     */
        BITS8   btE :1;
        BITS8   btPacketFilterNum:4;  /* packet filters Num   */
 
    /* Packet filter list */
    MSPS_PACKET_FILTER_T     astPacketFilter[MSPS_MAX_PF_NUM];
	/* Parameter list */
	UINT8   ucParameterLen;
	MSPS_PARAMETER_T         astParameter[MSPS_MAX_PR_NUM];
}MSPS_TFT_T;
/****** TFT ��ؽṹ���� END ******/

/* PDP ID LIST */
typedef struct
{
     UINT8     ucPdpNum;                               /* ʵ��PDP���� */
     UINT32   aulPdpDbIndex[MSPS_MAX_PDP_NUM_PER_IMSI]; /* PDP DB Index */
}MSPS_PDP_ID_LIST_T;

/* Extension header */
typedef struct
{
    UINT8   ucLength;                     /* ֵ�򳤶� */
    UINT8   aucValue[MSPS_VALUE_MAX_LEN];  /* ֵ�� 255 */
}MSPS_EXT_HEADER_TYPE_LIST_T;

typedef struct
{
    UINT8               ucPdpType;    /*PDP����*/
    UINT8               ucNsapi;      /* NSAPI ��RABID��*/
    UINT32              ulTeiduForIu; /* SGSN�����RNCʹ��TEID-U */
    MSPS_GSN_ADDR_T      stSgsnUpAddrForIu;  /* SGSN Iu��� ���ص�ַ */
    MSPS_QOS_PROFILE_T   stQosNeg;  /* �û������QoS*/
}MSPS_RELOC_PDPCONTEXT_UPINFO_T;

/* CN Common GSM-MAP NAS system information 24008��10.5.1.12.1 */
typedef  UINT16  MSPS_CN_COMMON_NAS_SYS_INFO_T;  /* usLac */

/* PS domain specific system information 24008��10.5.1.12.3 */
typedef struct
{
    UINT8  ucRac;
    #ifdef MSPS_BIG_ENDIAN
        BITS8  btSpare:7;
        BITS8  btNmo:1;
    #else
        BITS8  btNmo:1;
        BITS8  btSpare:7;
    #endif
}MSPS_PS_DOMAIN_SPECIFIC_SYS_INFO_T;

/************ PS��ϵͳ��RDBS PDP CONTEXT ��ع����ṹ���� END ************/

/************ SPA ���ݿ�����Ϣ�ṹ BEGIN ************/
/* GSMS Reference Number�ṹStart */
typedef struct
{   
   UINT32   ulGsmsReferNumH;        
   UINT32   ulGsmsReferNumL;
}MSPS_SPA_GSMS_REFERNUM_T;
/* GSMS Reference Number�ṹEnd */

typedef struct
{
    UINT8   ucSpaDpState;                   /* SPAʵ��DP״̬ */
    UINT8   ucSpaOdbState;                  /* SPAʵ��ODB״̬ */
    UINT8   ucSpaAcrState;                  /* SPA�����ϱ�״̬ */
    UINT8   ucSpaTswState;                  /* SPA Tsw��ʱ����״̬  */
    UINT8   ucSpaGetVolumeRspState;         /* SPA��ȡ����Ӧ��״̬ */
    UINT8   ucSpaStopVolumeAckState;        /* SPAֹͣ����Ӧ��״̬ */
    UINT8   ucSpaAcVolumeAckState;          /* SPA�������Ӧ��״̬ */
}MSPS_SPA_INS_STATE_T;

typedef struct
{
    UINT32  ulWaitSsfGprsInvokeTd;           /* WaitforSSFGPRS Invoke��ʱ�������� */
    UINT32  ulWaitForDpRspTd;                /* WaitforDpRsp��ʱ�������� */
    UINT32  ulWaitOdbRspTd;                  /* WaitforODB Rsp��ʱ�������� */
    UINT32  ulGprsAtTd;                      /* GPRS AT��ʱ�������� */
    UINT32  ulTswTd;                         /* Tsw��ʱ�������� */
    UINT32  ulDcpTd;                         /* Dcp��ʱ�������� */
    UINT32  ulTcpTd;                         /* Tcp��ʱ�������� */
    UINT32  ulAcRspTd;                       /* AC Rsp��ʱ�������� */
    UINT32  ulStopRspTd;                     /* Stop Rsp��ʱ�������� */
    UINT32  ulGetRspTd;                      /* Get Rsp��ʱ�������� */
    UINT32  ulAcrGetRspTd;                   /* Acr Get Rsp��ʱ�������� */
    UINT32  ulTswGetRspTd;                   /* Tsw Get Rsp��ʱ�������� */
}MSPS_SPA_INS_TD_T;

typedef struct
{
    UINT32 ulMaxTransVolumeH;               /* �ۻ���������ط�ֵ */
    UINT32 ulMaxTransVolumeL;               /* �ۻ���������ط�ֵ */
    UINT32 ulVolumeResultH;                 /* ���ϱ������ܺ� */
    UINT32 ulVolumeResultL;                 /* ���ϱ������ܺ� */
    UINT32 ulVolumeIfNoTsw;                 /* δ����Tsw��ʱ������ */
    UINT16 usrO_VolumeIfNoTsw;              /* δ����Tsw��ʱ������RollOverֵ */
    UINT32 ulVolumeTswInterval;             /* ����Tsw��ʱǰ������ */
    UINT16 usrO_VolumeTswInterval;          /* ����Tsw��ʱǰ������RollOverֵ */
    UINT32 ulVolumeSinceLastTsw;            /* ����Tsw��ʱ������ */
    UINT16 usrO_VolumeSinceLastTsw;         /* ����Tsw��ʱ������RollOverֵ */
}MSPS_SPA_INS_VOLUME_T;

typedef struct
{
    UINT32 ulAcFirstStartTime;              /* ��һ��Ac��ʼʱ�� */
    UINT32 ulDcpStartTime;                  /* Dcp��ʼʱ�� */
    UINT32 ulMaxTransTime;                  /* ʱ����ط�ֵ */   
    UINT32 ulTimeResult;                    /* �ϱ�ʱ���ܺ� */
    UINT32 ulTimeIfNoTsw;                   /* δ����Tsw��ʱ��ʱ�� */    
    UINT16 usrO_TimeIfNoTsw;                /* δ����Tsw��ʱ��ʱ��RollOverֵ */    
    UINT32 ulTimeTswInterval;               /* ����Tsw��ʱǰ��ʱ�� */    
    UINT16 usrO_TimeTswInterval;            /* ����Tsw��ʱǰ��ʱ��RollOverֵ */    
    UINT32 ulTimeSinceLastTsw;              /* ����Tsw��ʱ��ʱ�� */    
    UINT16 usrO_TimeSinceLastTsw;           /* ����Tsw��ʱ��ʱ��RollOverֵ */
}MSPS_SPA_INS_TIME_T;

/*SPA���ݿ�����Ϣ�ṹStart*/
typedef struct
{    
    UINT8   ucTcpPendingFg;             /* Tcp Pending��� */
    UINT8   ucVcPendingFg;              /* Vc Pending��� */
    UINT8   ucWaitForAcTimeFg;          /* Wait For AC Time��� */
    UINT8   ucWaitForAcVolumeFg;        /* Wait For AC Volume��� */    
    UINT8   ucQosPendingFg;             /* Qos Pending��� */
    UINT8   ucContextActiveFg;          /* PDP Context Active״̬��� */
    UINT8   ucAcFirstFg;                /* ��һ��AC������ */
    UINT8   ucTswFg;                    /* Tsw�Ƿ�ʱ��� */
    UINT8   ucSmAtCounter;              /* SM AT���� */
    UINT32  ulBufferQosId;              /* QoS������ϢID */
    UINT32  ulBufferAcTimeId;           /* AC Time������ϢID */
    UINT32  ulBufferAcVolumeId;         /* AC Volume������ϢID */
    UINT32  ulSsfInsId;                 /* SSFʵ��ID */
    MSPS_SPA_INS_TIME_T stSpaInsTime;    /* ҵ��ʱ�� */
    MSPS_SPA_INS_VOLUME_T stSpaInsVolume;/* ҵ������ */
    MSPS_SPA_INS_STATE_T stSpaInsState;  /* SPAʵ��״̬ */
    MSPS_SPA_INS_TD_T    stSpaInsTd;     /* SPAʵ����ʱ�������� */
} MSPS_SPA_DB_PARAS_T;
/************ SPA ���ݿ�����Ϣ�ṹ END ************/

/************ LCS ���ݿ�����Ϣ�ṹ BEGIN ************/
typedef enum
{
    North = 0,
    South = 1
}MSPS_LATSIGN_E;

typedef struct
{
    MSPS_LATSIGN_E   enLatSign;
    UINT32          ulLatitude;
    INT32           lLongitude;
}MSPS_GEOGRAPHICAL_COORD_T;

typedef struct
{
    MSPS_GEOGRAPHICAL_COORD_T stGeograCood;
}MSPS_GA_POINT_T;

typedef struct
{
    MSPS_GEOGRAPHICAL_COORD_T stGeograCood;
    UINT8 ucUncertCode;
}MSPS_GA_POINTWITHUNCERT_T;

typedef struct
{
    UINT8 ucPolyGonPointNum;
    MSPS_GEOGRAPHICAL_COORD_T stGeograCood[MSPS_MAX_NUM_POINTS]; /* MAX = 15 */    
}MSPS_GA_POLYGON_T;

typedef struct
{
    UINT8 ucUncertSemi_Major;
    UINT8 ucUncertSemi_Minor;
    UINT8 ucOrientOfMajorAxis;
}MSPS_UNCERTELLIP_T;

typedef struct
{
    MSPS_GEOGRAPHICAL_COORD_T    stGeograCood;
    MSPS_UNCERTELLIP_T           stUncertEllip;
    UINT8                       ucConfidence;
}MSPS_GA_POINTWITHUNCERTELLIP_T;

typedef enum
{
    Height = 0,
    Depth = 1
}MSPS_DIRECTOFALT_E;

typedef struct
{
    MSPS_DIRECTOFALT_E   enDirectOfAlt;
    UINT16              usAlt;
}MSPS_ALTANDDIRECT_T;

typedef struct
{
    MSPS_GEOGRAPHICAL_COORD_T    stGeograCood;
    MSPS_ALTANDDIRECT_T          stAltAndDirect;
}MSPS_GA_POINTWITHALT_T;

typedef struct
{
    MSPS_GEOGRAPHICAL_COORD_T    stGeograCood;
    MSPS_ALTANDDIRECT_T          stAltAndDirect;
    MSPS_UNCERTELLIP_T           stUncertEllip;
    UINT8                       ucUnCertAlt;
    UINT8                       ucConfidence;
}MSPS_GA_POINTWITHALTANDUNCERTELLIP_T;

typedef struct
{
    MSPS_GEOGRAPHICAL_COORD_T stGeograCood;
    UINT16 usInnerRadius;
    UINT8 ucUnCertRadius;
    UINT8 ucOffsetAngle;
    UINT8 ucIncludeAngle;
    UINT8 ucConfidence;
}MSPS_GA_ELLIPARC_T;

typedef union
{
    MSPS_GA_POINT_T                       stGaPoint;
    MSPS_GA_POINTWITHUNCERT_T             stGaPointWithUncert;
    MSPS_GA_POLYGON_T                     stPolygon;
    MSPS_GA_POINTWITHUNCERTELLIP_T        stPointWithUncertEllip;
    MSPS_GA_POINTWITHALT_T                stPointWithAlt;
    MSPS_GA_POINTWITHALTANDUNCERTELLIP_T  stPointWithAltAndUncertEllip;
    MSPS_GA_ELLIPARC_T                    stEllipArc;
}MSPS_GEOGRAPHICAL_AREA_U;

typedef struct
{
    UINT8 ucGeograAreaChoice;/* 0-GA_Point; 1-GA_PointwithUncertainty;
                                2-GA_Polygon; 3-GA_PointWithUncertaintyEllipse;
                                4-GA_PointWithAltitude;
                                5- GA_PointWithAltitudeAndUncertaintyEllipsoid;
                                6- GA_EllipsoidArc */
    MSPS_GEOGRAPHICAL_AREA_U unGeograArea;
}MSPS_GEOGRAPHICAL_AREA_T;

typedef union
{
    MSPS_SAI_T stSai;
    MSPS_GEOGRAPHICAL_AREA_T stGeograArea;
}MSPS_LOCATION_AREA_ID_U;

typedef struct
{
    UINT8 ucAreaIdChoice; /* 0-SAI; 1-Geographical Area */
    MSPS_LOCATION_AREA_ID_U unAreaId;
}MSPS_LOCATION_AREA_ID_T;

/************ LCS ���ݿ�����Ϣ�ṹ END ************/



typedef struct
{
    UINT32 ulLen;
    UINT8  aucValue[MSPS_SSF_MAX_FCI_LENGTH];
} MSPS_SSF_FCI_INFO_T;

typedef MSPS_SSF_FCI_INFO_T  MSPS_CDR_FCI_INFO_T ; /* MSPS_CDR_FCI_INFO_T�����ݿ�ʹ�á�*/


/************ �Ʒ���ؽṹ���� END ************/

/************ ����Ϣ��ؽṹ���� END ************/
/* service center address */
typedef struct
{
    UINT8   ucLength;
    /* ���е�һ���ֽ� bit 8 SPARE , BIT 7-5 TYPE OF NUMBER, BIT 1-4 NUMBER PLAN*/
    UINT8   aucValue[MAX_ADDRESS_LENGTH]; /* 20 */
}MSPS_SC_ADDRESS_T;

/*public resource statistic Added by pym*/
typedef struct
{
    UINT8  ucMoudleType;
	UINT32 ulTotalNum;
    UINT32 ulPubResourceIdx;
}MSPS_PUB_RESOURCE_STAT_T;
/*add end*/

/*modifications added by  Wu peng cheng for  GTPC on MME V0.01 2008/10/10*/

/*******************************CAUSE****************************/
#define MSPS_GTPV_V2_MAX_CAUSE_RESERVED_LEN						8
typedef struct
{
    UINT16   usLength;
	UINT8    ucCause;
    UINT8    ucReserved[MSPS_GTPV_V2_MAX_CAUSE_RESERVED_LEN];  
}MSPS_CAUSE_T;

#define MSPS_MAX_RECOVERY_LEN            8
#define MSPS_GTPV_V2_MAX_RECOVER_LEN		4                          
typedef struct
{
    UINT16   usLength;                      
    UINT8    ucValue[MSPS_GTPV_V2_MAX_RECOVER_LEN];  
}MSPS_RECOVERY_T;

/* Private Extension */
#define MSPS_VALUE_MAX_LEN             255
typedef struct
{
    UINT16   usLength;                       /* ֵ�򳤶� */
    UINT8   ucExtId;                         /* ��չͷID */
    UINT8   ucValue[MSPS_VALUE_MAX_LEN-1];    /* ��չͷ 255-1 */
}MSPS_PRIVATE_EXT_T;

/***********MEI*************/
#define MSPS_GTPC_V2_MAX_MEI_LEN									8
typedef struct
{
    UINT16  usLen;
    UINT8   ucValue[MSPS_GTPC_V2_MAX_MEI_LEN];  
}MSPS_MEI_T;  

/* ULI ,����ULI for CGI��ULI for SAI��ULI for RAI��ULI for TAI*/
typedef struct
{
    UINT16			usLength;
    UINT8			ucLocationType;
    BITS8		       btMcc1;
    BITS8		       btMcc2;
    BITS8		       btMcc3;
    BITS8		       btMnc1;
    BITS8		       btMnc2;
    BITS8		       btMnc3;	
    UINT16		usLac;
    UINT16		usOptionalId;	  
}MSPS_ULI_T;

/* Serving Network */
#define MSPS_MAX_SERVING_NETWORK_LEN             3
typedef struct 
{
	UINT16  usLength ;							   /* ֵ�򳤶� */
	BITS8  btMcc1;
	BITS8  btMcc2;
	BITS8  btMcc3;
	BITS8  btMnc1;
	BITS8  btMnc2;
	BITS8  btMnc3;
}MSPS_SN_T ;

/*** Fully TEID TS29.274 v1.2.0 P69****/
typedef struct 
{
    UINT16           usLength;
    BITS8            bteNB;
    BITS8            btPgw;
    BITS8            btSgw;
    BITS8            btMme;
    BITS8            btCup;
    BITS8            btEbi;
    BITS8            btV6;
    BITS8            btV4;
    UINT32          uiTeid;
    MSPS_IP_ADDR_T   stIpAddr;            /* MSPS_IPV6_ADDR_LEN=16 */
    UINT8            ucEbi;
}MSPS_F_TEID_T;

/****PDN Address Allocation (PAA)************************************/
typedef struct 
{
    UINT16		    usLength ;			/* ֵ�򳤶� */
    UINT8		    ucPDNtype;
    MSPS_IP_ADDR_T	stPDNAddress;       /* PDN Address and prefix */   
}MSPS_PAA_T ;

/* Aggregate Maxinum Bit Rate(AMBR) */
#define MSPS_GTPC_V2_MAX_AMBR_UPLINK_LEN     8
#define MSPS_GTPC_V2_MAX_AMBR_DLINK_LEN      8
#define MSPS_GTPC_V2_MAX_AMBR_RESERVE_LEN    4

#if 1	/* 2008-10-29 FFS */
typedef struct
{
	UINT32		       usLength ;			                        /* ֵ�򳤶� */
	UINT32             ucuplink;
	UINT32             ucdownlink;
}MSPS_AMBR_T;
#else
typedef struct
{
	UINT16		       usLength ;			                        /* ֵ�򳤶� */
	UINT8               ucuplink[MSPS_GTPC_V2_MAX_AMBR_UPLINK_LEN];
	UINT8               ucdownlink[MSPS_GTPC_V2_MAX_AMBR_DLINK_LEN];
	MSPS_APN_T           stApnFlag;              /**��EPC�пɼ���������Ϊ��ѡ��*/
	UINT8               ucReserved[MSPS_GTPC_V2_MAX_AMBR_RESERVE_LEN];
}MSPS_AMBR_T;
#endif	
/****************** Indication ,29274-120,P63*************************/
typedef struct
{
    UINT16			usLen;          
	BITS8			btSpares:	3 ;
	BITS8			btHI:		1 ;
	BITS8			btDFI:		1 ;
	BITS8			btOI:		1 ;
	BITS8			btISRAI:	1 ;
	BITS8			btSGWCI:	1 ;
}MSPS_IND_T;

/******************QoS���*********************/
#define MSPS_GTPC_V2_MAX_LEGARY_QOS_LEN  32
typedef struct 
{
    UINT16          usLength;
    UINT8           ucLegacyQos[MSPS_GTPC_V2_MAX_LEGARY_QOS_LEN];                                     
}MSPS_LEGACY_QOS_T;

/***Bearer Level Quality of Service(Bearer QoS),TAG:80***/
#define MSPS_GTPC_V2_MAX_BEARER_QOS_LEN  32
typedef struct 
{
    UINT16          usLength;
    UINT8           ucBearerQos[MSPS_GTPC_V2_MAX_BEARER_QOS_LEN];   
}MSPS_BEARER_QOS_T ;


typedef struct 
{
	UINT16              usLength;
	UINT8				ucEbiExist;
    UINT8		        ucEbi;
    UINT8				ucCauseExist;        /*  Exist Flag */   
	MSPS_CAUSE_T		    stCause;				/*ԭ��ֵ  FFS*/
    UINT8				ucUTftExist;        /*  Exist Flag */      
	MSPS_TFT_T           stUTft;				/*O*/
    UINT8				ucDTftExist;        /*  Exist Flag */   
    MSPS_TFT_T	        stDTFT;				/*O*/
    UINT8				ucS1EnBExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS1EnBFTeid;
    UINT8				ucS1SgwExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS1SgwFTeid;
	UINT8				ucS4USgsnExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS4USgsnFTeid;	
	UINT8				ucS4USgwExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS4USgwFTeid;
	UINT8				ucS5USgwExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS5USgwFTeid;
    UINT8				ucS5UPgwExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS5UPgwFTeid;
	UINT8				ucS12RnCExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS12RncFTeid;
	UINT8				ucS12SgwExist;        /*  Exist Flag */  
    MSPS_F_TEID_T	    stS12SgwFTeid;
    UINT8				ucBearerQosExist;        /*  Exist Flag */
	MSPS_BEARER_QOS_T   stBearerQos;
    UINT8				ucLegacyQosExist;        /*  Exist Flag */
	MSPS_LEGACY_QOS_T	stLegacyQos;      
    UINT8				ucChargingCharExist;     /*  Exist Flag */  
    UINT16				ChargingCharacter;	 /* �Ʒ����� f*/ 
	UINT8				ucCharingIdExist;        /*  Exist Flag */  
    UINT32			    uiCharingId;
	UINT8               ProhibitPayloadCpr;	
}MSPS_BEARER_CTX_LIST_T;


/*end of modifications on MME V0.01 2008/10/10*/
/********************************************************************
added for s1ap 
by wangbin  2008/10/23
*********************************************************************/
#define MSPS_MAX_TAI_IN_LIST_NUM             ((UINT8)16)  /* [2010-04-12] ���� */
#define MSPS_MAX_PLMN_IN_LIST_NUM            ((UINT8)15)
#define MSPS_MTMSI_LEN                       ((UINT8)4)
/******************************** ���Ͷ��� *********/



/************************************************/
/*TAI LIST 24301:9.9.3.26*/
typedef struct
{
	UINT8 ucLength;
#ifndef MSPS_BIG_ENDIAN
    BITS8 btSpare:1;
    BITS8 btListType:2;
    BITS8 btTaiNum:5;
#else
    BITS8 btTaiNum:5;
    BITS8 btListType:2;
    BITS8 btSpare:1;
#endif
    MSPS_TAI_T  astTai[MSPS_MAX_TAI_IN_LIST_NUM];
}MSPS_TAI_LIST_T;

#define MSPS_MAX_TA_IN_LIST0_NUM 64
#define MSPS_MAX_TA_IN_LIST1_NUM 64
#define MSPS_MAX_TA_IN_LIST2_NUM 64
/* Tac����������� */
typedef struct
{
    MSPS_PLMN_T	stPlmn;
	UINT8		ucTacNum;
    UINT16		ausTac[MSPS_MAX_TA_IN_LIST0_NUM];
}MSPS_TA_LIST0_T;		/* 2010-02-03  */

/* Tac��������� */
typedef struct
{
    MSPS_PLMN_T	stPlmn;
	UINT8		ucTacNum;
    UINT16		usTac;	
}MSPS_TA_LIST1_T;		/* 2010-02-03  */

/* Plmn��Tac������������� */
typedef struct
{
	UINT8		ucTacNum;
	MSPS_TAI_T  astTai[MSPS_MAX_TA_IN_LIST2_NUM];
}MSPS_TA_LIST2_T;		/* 2010-02-03  */

typedef union {
	MSPS_TA_LIST0_T		stList0;
	MSPS_TA_LIST1_T 	stList1;
	MSPS_TA_LIST2_T 	stList2;
}MSPS_TA_LIST_U;

#define MSPS_TA_LIST_TYPE0		0
#define MSPS_TA_LIST_TYPE1		1
#define MSPS_TA_LIST_TYPE2		2
typedef struct
{
	UINT8			ucListType;
	MSPS_TA_LIST_U	unTaiList;
}MSPS_ATOM_TA_LIST_T;

#define MSPS_TA_LIST_N			8
typedef struct
{
	UINT8				ucListNum;
	MSPS_ATOM_TA_LIST_T	astTaList[MSPS_TA_LIST_N];
}MSPS_TA_LIST_T;		/* 2010-02-03  */


typedef struct
{
    UINT8 ucPlmnNum;
    MSPS_PLMN_T stPlmn[MSPS_MAX_PLMN_IN_LIST_NUM];
}MSPS_PLMN_LIST_T;


/*TIMER INFO 24008:10.5.7.3*/
typedef struct
{
#ifndef MSPS_BIG_ENDIAN
    BITS8  btUint:3;
    BITS8  btTimerValue:5;
#else
    BITS8  btTimerValue:5;
    BITS8  btUint:3;
#endif
}MSPS_TIMER_INFO_T;
/* GPRS Timer2 24008��10.5.7.4 */
/* The purpose of the GPRS timer 2 information element is to specify GPRS specific timer values,
e.g. for the timer T3302. */
typedef struct
{
    UINT8  ucLength;
    UINT8  ucValue;
}MSPS_TIMER2_INFO_T;


typedef struct{
	BITS8             btTaiChange :1;   /*���յ�ĳЩ��Ϣʱ���Ƚ���Ϣ�е�TAI��CCb�д洢��TAI�Ƿ���ͬ�������ͬ�����ΪTRUE������ΪFALSE */
	BITS8             btEcgiChange :1;  /*���յ�ĳЩ��Ϣʱ���Ƚ���Ϣ�е�ECGI��CCb�д洢��ECGI�Ƿ���ͬ�������ͬ�����ΪTRUE������ΪFALSE */
	BITS8             btSpare:6;
	MSPS_TAI_LIST_T			stTaiList;			/* Current Tracking Area List	*//* change API */
	MSPS_TAI_T				stTaiofTau;			/* TAI of Last TAU				*//* add API */
	MSPS_CGI_T				stLastKnownCell;	/* Last Know E-UTRAN Cell		*/
	MSPS_ECGI_T				stLastKnownEutranCell;
	UINT32					uiLastKnownCiAge;	/* Time elapsed since the Lase E-UTRAN CGI was acquired	*/
}MSPS_UE_LOC_INFO_T;


/************ ����Ϣ��ؽṹ���� END ************/


typedef EPS_ARP PDP_ARP;
typedef struct
{
    UINT8    ucMbrDl;
    UINT8    ucGbrDl;
    UINT8    ucMbrUl;
    UINT8    ucGbrUl;
}MSPS_PDP_EXTEND_QOS_T;

typedef enum
{
    CONVERSATIONAL = 1,
    STREAMING = 2,
    INTERACTIVE = 3,
    BACKGROUND = 4
}MSPS_TRAFFIC_CLASS_E;

typedef enum
{
    NOT_OPT_FOR_SIG_TRA = 0,
    OPT_FOR_SIG_TRA = 1
}MSPS_SIG_IND_E;

typedef enum
{
    UNKNOWN = 0,
    SPEECH = 1
}MSPS_SRC_STS_DES_E;

typedef struct
{
    MSPS_BASE_QOS_T stBaseQos;
    BITS8    btTrafficClass:3;          /* ע�� 1        */
    BITS8    btDeliveryOrder:2;         /* ����˳��       */
    BITS8    btDeliveryOfErrSdu:3;      /* �Ƿ�������SDU    */
    UINT8    ucMaxSduSize;              /* ���SDU����      */
    UINT8    ucMbrForUl;
    UINT8    ucMbrForDl;
    BITS8    btResidualBer:4;            /*Residual BER ����������*/
    BITS8    btSduErrRatio:4;            /*SDU Error ratio SDU������ */    
    BITS8    btTransferDelay:6;          /* ����ʱ��       */
    BITS8    btTrafficPriority:2;        /* �������ȼ�     */
    UINT8    ucGbrForUl;
    UINT8    ucGbrForDl;
    BITS8    btSpare:3;
    BITS8    btSigInd:1;
    BITS8    btSrcStsDes:4;
    MSPS_PDP_EXTEND_QOS_T stExtQos;
}MSPS_PDP_QOS_PROFILE_T;

typedef struct
{
    PDP_ARP stArp;
    MSPS_QOS_PROFILE_T stProfile;
}MSPS_PDP_QOS_T;
/* TLLI */
typedef struct
{                          
    UINT32     uiTlli;
}MSPS_TLLI_T;
    
/******************************************************************************/


/******************************** ȫ�ֱ������� *******************************/

/******************************** �ⲿ����ԭ������ ***************************/

/******************************** ͷ�ļ�������β *****************************/
//#pragma pack()
#endif /* MSPS_PUB_COMSTRUCT_H */
/******************************** ͷ�ļ����� *********************************/
/*modifications added by Wu peng cheng for GTPC on MME V0.01 2008/10/10*/

/*******************************CAUSE****************************/

