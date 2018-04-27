/******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
*******************************************************************************
* �ļ�����: nas_cbb_nas.h
* ��    ��: NAS�����ӿں������������ṩ���ⲿ�����ӿڣ�����ͽ��롣
* ˵    ��:
* �޸���ʷ:
* EPS 2008.10.07  ���������汾
******************************************************************************/

/******************************** ͷ�ļ�������ͷ *****************************/
#ifndef NAS_CBB_H
#define NAS_CBB_H
/********************************* ͷ�ļ����� ********************************/
#include "nas_cbb_msg.h"

/************************************************************************/
/*     *pvInMsg��ulInMaxLen                                             */
/*     ����������                                                       */
/*     |        |                                                       */
/*     ����������                  *pucOutBufAddr��*pulMsgLen           */
/*     ����������        encode    ����������������������������         */
/*     |        |      ----------> |       |        |         |         */
/*     ����������                  ����������������������������         */
/*     ����������                                                       */
/*     |        |                                                       */
/*     ����������                                                       */
/************************************************************************/
extern UINT32 msps_nas_encode( void   *pvInMsg,  
                               UINT8  *pucOutBufAddr, 
                               UINT32 *pulMsgLen, 
                               UINT32 ulInMaxLen,
                               UINT32 uiInCcbn );



/************************************************************************/
/*                                                   *pvOutMsg          */
/*                                                   ����������         */
/*                                                   |        |         */
/*  *pucInBufAddr,ulInMsgLen,ulInMaxLen              ����������         */
/*     ��������������������������         decode     ����������         */
/*     |       |       |       |       ----------->  |        |         */
/*     ��������������������������                    ����������         */
/*                                                   ����������         */
/*                                                   |        |         */
/*                                                   ����������         */
/************************************************************************/
extern UINT32 msps_nas_decode( UINT8  *pucInBufAddr,  
                               UINT32 ulInMsgLen, 
                               void   *pvOutMsg, 
                               UINT32 ulInMaxLen,
                               UINT32 uiInCcbn);



/*******************************************************************************
* ��������: mme_s1ap_encode_init_ue_msg
* ��    ��: S1AP_INIT_UE_MSG_T ��Ϣ���뺯�� 
* ��������: 
* ��    ��: 
* ��������         ����           ����/���      ����
* ucCode           UINT8          ����           �ֽ������ͣ�Struct:0 ;Buff:1��
* pInBuff          UINT8*         ���           �ֽ���
* ��������: Message Type
* ˵    ��: ����Buff ,����MsgType
*******************************************************************************/
extern UINT32 msps_nas_check_send_msgtype(UINT8 ucMsgType);
extern UINT32 msps_nas_check_recv_msgtype(UINT8 ucMsgType);
extern UINT32 msps_nas_check_emm_msgtype(UINT8 ucMsgType);
extern UINT32 msps_nas_check_esm_msgtype(UINT8 ucMsgType);
extern UINT8 nas_msgtype_get(void *pvInBuff, UINT8 ucCode );

/* pvInBuff :Raw Buff

   if     pucEsmMsgType return 0 ------> esm container do not exist
   else   esm container exist
*/
extern UINT32 nas_msgtype_get_emm_and_esm(UINT8 *pucInBuff, UINT8 *pucEmmMsgType, UINT8 *pucEsmMsgType );

extern UINT32  msps_get_mobile_id_from_buff(UINT8 *pucBuff ,UINT32 uiLen, MSPS_EPS_MOBILE_ID_T *pstMobileId);

extern UINT8 nas_get_secu_type(const UINT8 *pucBuff, UINT32 uiLen);

extern UINT8 nas_get_pd(const UINT8 *pucBuff, UINT32 uiLen);

extern UINT8 nas_get_sequence_num_from_secu_buff(const UINT8 *pucBuff, UINT32 uiLen);

extern UINT32 nas_get_esm_cause_from_raw_buff(UINT8 *pucInBuff, UINT32 uiLen, UINT8 *pucEsmCause);

extern UINT32 nas_get_emm_cause_from_raw_buff(UINT8 *pucInBuff, UINT32 uiLen, UINT8 *pucEmmCause);

extern UINT32 msps_nas_decode_temp(UINT8 *pucInBufAddr,  UINT32 ulInMsgLen, void *pvOutMsg, UINT32 ulInMaxLen);

extern UINT32 msps_nas_encode_temp( void *pvInMsg,  UINT8 *pucOutBufAddr, UINT32 *puiMsgLen, UINT32 ulInMaxLen );


/*****************************************************/
typedef struct
{
	/*
	enum 
	{
	FLAG_OLD_GUTI = 0x06,
	FLAG_IMSI     = 0x01,   
	FLAG_IMEI     = 0x03       
	}ucFlag_OldGutiOrImsi;
	*/
	
    UINT8 ucFlag_OldGutiOrImsi;
    MSPS_OLDGUTI_IMSI_T  unGutiOrImsi;
    
    UINT8        ucAdditionalGutiExist;
    MSPS_GUTI_T  stAdditionalGuti;
	
}MSPS_EPS_MOBILE_ID_TEMP_T;

extern UINT32  msps_get_mobile_id_and_additional_guti_from_buff(UINT8 *pucBuff ,UINT32 uiLen, MSPS_EPS_MOBILE_ID_TEMP_T *pstMobileId);




#endif




