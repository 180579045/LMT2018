/*******************************************************************************
*  COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD                    
********************************************************************************
* �ļ�����:                                                
* ��������: 
* ʹ��˵��:    
* �ļ�����:                                                      
* ��д����:                                           
* �޸���ʷ: 
* �޸�����    �޸���  BugID/CRID      �޸�����
* ------------------------------------------------------------------------------
* 
*******************************************************************************/

/*******************************  ͷ�ļ�����  *********************************/
#include "cdl_common.h"
#include "x2_handover_decode.h"
#include "x2ap.h"

/*******************************  �ֲ��궨��  *********************************/

/***************************  ȫ�ֱ�������/��ʼ��  ****************************/

/*****************************  ���غ���ԭ������  *****************************/

/********************************  ����ʵ��  **********************************/
/*******************************************************************************
* ��������: X2AP_CauseDecode
* ��������: ����Cause ��Ϣ
* ����ĵ�: <eNB AP�����ϸ���>
* ��������:
*   ������              ����            ����/���     ����
* pCause            X2AP_Cause *    ����          Э���е�Cause�ṹͷָ��
* pu8CauseType          u8 *                 ���     Cause����
* pu8CauseValue         u8 *                 ���     Causeֵ
* ����ֵ:   ��
* ��������:
* ����˵��:
* �޸�����    �汾��   �޸���  �޸�����
* ------------------------------------------------------------------------------
*******************************************************************************/
void X2AP_CauseDecode(const X2AP_Cause *pCause, u8 *pu8CauseType, u8 *pu8CauseValue)
{
    if (pCause->choice == X2AP_radioNetwork_chosen)
    {
        *pu8CauseType = X2AP_radioNetwork_chosen;
        *pu8CauseValue = (u8)(pCause->u.radioNetwork);
    }
    else if (pCause->choice == X2AP_transport_chosen)
    {
        *pu8CauseType = X2AP_transport_chosen;
        *pu8CauseValue = (u8)pCause->u.transport;
    }   
    else if (pCause->choice == X2AP_protocol_chosen)
    {
        *pu8CauseType = X2AP_protocol_chosen;
        *pu8CauseValue = (u8)pCause->u.protocol;
    }
    else if (pCause->choice == X2AP_misc_chosen)
    {
        *pu8CauseType = X2AP_misc_chosen;
        *pu8CauseValue = (u8)pCause->u.misc;
    }
    else
    {
        *pu8CauseType = X2AP_misc_chosen;
        *pu8CauseValue = (u8)X2AP_CauseMisc_unspecified;
    }

    return;
}

/*******************************************************************************
* ��������: X2AP_DecodeTargetCellId
* ��������: ����Target Cell ID��Ϣ        
* ����ĵ�: <eNB AP�����ϸ���>
* ��������:
* ��������:   ����      ����/���     ����
* pstruTargetCellId    X2AP_ECGI *     ����    ����ǰTarget Cell ID����ָ��
* pstruPlmnId          HL_PlmnId *     ���    PDU�����PLMN
* pu32TargetCellId     u32 *           ���    PDU�����Cell Identity
* ����ֵ: 
*        SUCCESS                           �ɹ�
*        AP_MACRO_MSG_IE_NOT_UNDERSTOOD    ����ʶ���IE
* ��������:
* ����˵��:
* 1.���������ı��ȫ�ֱ����б�
* 2.���������ı�ľ�̬�����б�
* 3.���õ�û�б��ı��ȫ�ֱ����б�
* 4.���õ�û�б��ı�ľ�̬�����б�
* �޸�����    �汾��   �޸���  �޸�����
* -----------------------------------------------------------------
*******************************************************************************/
void X2AP_DecodeTargetCellId(X2AP_ECGI *pstruTargetCellId, u32 *pu32TargetCellId)
{
    u8     u8CellId[X2AP_MACRO_MAX_EUTRAN_CELL_ID_BYTE_LEN];
    u32    u32TargetCellId;

    if (X2AP_MACRO_MAX_EUTRAN_CELL_ID_BIT_LEN != pstruTargetCellId->eUTRANcellIdentifier.length)
    {
        u32TargetCellId = 0xffffffff;
        *pu32TargetCellId = u32TargetCellId;
        return;
    }

    (void)memcpy(u8CellId, pstruTargetCellId->eUTRANcellIdentifier.value,
                                     X2AP_MACRO_MAX_EUTRAN_CELL_ID_BYTE_LEN);

    u32TargetCellId = (u8CellId[0] << 24) & 0xFF000000;/*��Ϊ32-25λ*/
    u32TargetCellId |= (u8CellId[1] << 16) & 0xFF0000;/*��Ϊ24-17λ*/
    u32TargetCellId |= (u8CellId[2] << 8) & 0xFF00;/*��Ϊ16-9λ*/
    u32TargetCellId |= u8CellId[3];/*��Ϊ8-1λ*/
    u32TargetCellId = (u32TargetCellId >> 4) & 0x0FFFFFFF;/*����4λ*/
    *pu32TargetCellId = u32TargetCellId;

    return;
}

/*******************************************************************************
* ��������: X2AP_HO_PrepReqDecodeFunc  
* ��������: ��������eNB ���л�������Ϣ
* ����ĵ�: <eNB AP�����ϸ���>
* ��������:
* ��������:   ����      ����/���     ����
* pMsg        void *                      ����     ������Ϣָ��
* pstruDecode    X2AP_HandoverReqDecode *    ���     ������Ϣ�ṹ
* ����ֵ:   
*        SUCCESS                           �ɹ�
*        AP_MACRO_MSG_IE_WRONG_ORDER       IE������˳����ֻ���ִ�������
*        AP_MACRO_MSG_IE_NOT_UNDERSTOOD    ����ʶ���IE
*        AP_MACRO_MSG_IE_MISSING           ȱ�� IE
* ��������:
* ����˵��:
* 1.���������ı��ȫ�ֱ����б�
* 2.���������ı�ľ�̬�����б�
* 3.���õ�û�б��ı��ȫ�ֱ����б�
* 4.���õ�û�б��ı�ľ�̬�����б�
* �޸�����    �汾��   �޸���  �޸�����
* -----------------------------------------------------------------
*******************************************************************************/
void X2AP_HO_PrepReqDecodeFunc(void *pMsg, X2AP_HO_PrepReqDecode *pstruDecode)
{
    X2AP_X2AP_PDU             *pstruDecodePDU = NULLPTR;   /*�������Ϣͷָ��*/
    X2AP_InitiatingMessage    *pstruInitMsg = NULLPTR;     /*��ʼ����Ϣ*/    
    X2AP_HandoverRequest      *pstruHandoverReq = NULLPTR; /*�л�������������ָ��*/
    X2AP_Cause                *pstruCause = NULLPTR;       /*Cause*/
    X2AP_ECGI                 *pstruTargetCellId = NULLPTR;/*Target Cell ID*/
    X2AP_UE_ContextInformation          *pstruContextInfo = NULLPTR; /*UE ContextInformation*/
    struct X2AP_ProtocolIE_Container    *pIE = NULLPTR;              /*ProtocolIE_Container*/
    u32                        u32TargetCellId;


    pstruDecode->u16OldX2apUeId = 0xFFFF;

    /*������ʼ��*/
    pstruDecodePDU = (X2AP_X2AP_PDU *)pMsg;    
    pstruInitMsg = &(pstruDecodePDU->u.initiatingMessage);

    /*��ʼ����Ϣ*/
    pstruHandoverReq = (X2AP_HandoverRequest *)(pstruInitMsg->value.decoded);
    pIE = pstruHandoverReq->protocolIEs;

    while (NULLPTR != pIE)
    {
        switch (pIE->value.id)  /*����IE-idʶ�����IE*/
        {
            case  X2AP_ID_Old_eNB_UE_X2AP_ID: /*1-M, id-Old-eNB-UE-X2AP-ID*/              
                /*X2AP_ID_Old_eNB_UE_X2AP_ID*/
                pstruDecode->u16OldX2apUeId = *(X2AP_UE_X2AP_ID *)(pIE->value.value.decoded);
                break;

            case  X2AP_ID_Cause: /*2-M, id-Cause*/              
                /*cause*/
                pstruCause = (X2AP_Cause *)(pIE->value.value.decoded);
                X2AP_CauseDecode(pstruCause, &(pstruDecode->u8CauseType), &(pstruDecode->u8CauseValue));
                break;

            case  X2AP_ID_TargetCell_ID: /*3-M, X2AP_ID_TargetCell_ID*/              
                pstruTargetCellId = (X2AP_ECGI *)(pIE->value.value.decoded);
                X2AP_DecodeTargetCellId(pstruTargetCellId, &u32TargetCellId);
                /*��ֵ���*/
                pstruDecode->u8TargetCellId = (u8)(u32TargetCellId & 0xFF);
                pstruDecode->u32TargetEnbId = (u32TargetCellId >> 8) & 0xFFFFF;
                break;

            case  X2AP_ID_UE_ContextInformation:/*5-M, UE-ContextInformation*/                  
                /*����UE_ContextInformation��Ϣ*/
                pstruContextInfo = (X2AP_UE_ContextInformation *)pIE->value.value.decoded;
                /*MME S1AP ID*/
                pstruDecode->u32SourceMmeUeId = (u32)(pstruContextInfo->mME_UE_S1AP_ID);
                /*RRC Containter*/
                pstruDecode->u16NofRrcContext = (u16)pstruContextInfo->rRC_Context.length;
                (void)memcpy(pstruDecode->u8RrcContext, pstruContextInfo->rRC_Context.value, 
                    pstruDecode->u16NofRrcContext);
                break;

            default:/*������û���г���IE*/
                break;
            }/*end of switch (pIE->value.id)*/
        pIE = pIE->next;  /*��ȡ�����ڵ���һ��IE��ָ��*/
    }/*end of while (NULLPTR != pIE)*/
    return;
}
/*******************************  Դ�ļ�����  *********************************/

