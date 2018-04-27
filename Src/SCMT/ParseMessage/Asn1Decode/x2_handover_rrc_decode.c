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
#include "x2_handover_rrc_decode.h"
#include "rrc.h"

/*******************************  �ֲ��궨��  *********************************/

/***************************  ȫ�ֱ�������/��ʼ��  ****************************/

/*****************************  ���غ���ԭ������  *****************************/

/********************************  ����ʵ��  **********************************/
/*******************************************************************************
* ��������: RRC_HO_PrepInfoDecodeFunc  
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
void RRC_HO_PrepInfoDecodeFunc(void *pMsg, RRC_HO_PrepInfoDecode *pstruDecode)
{
    HandoverPreparationInformation        *pHandoverPreparationInfo = NULLPTR;           /*�ڲ��ڵ���Ϣָ��*/
    HandoverPreparationInformation_r8_IEs *phandoverPreparationInformation_r8 = NULLPTR; /*�л�׼����Ϣ    */
    u32                                    u32SourceCellId;


    /*ָ����������    */
    pHandoverPreparationInfo = (HandoverPreparationInformation *)pMsg;
    phandoverPreparationInformation_r8 
        = &pHandoverPreparationInfo->criticalExtensions.u.c1.u.handoverPreparationInformation_r8;
    /*sourceUE_Identity*/
    pstruDecode->u16SourceCrnti = (u16)phandoverPreparationInformation_r8->as_Config.sourceUE_Identity.value[0];
    pstruDecode->u16SourceCrnti <<= 8;
    pstruDecode->u16SourceCrnti |= (u16)phandoverPreparationInformation_r8->as_Config.sourceUE_Identity.value[1];
    /*sourceDl_CarrierFreq*/
    pstruDecode->u16SourceDlEarfcn = phandoverPreparationInformation_r8->as_Config.sourceDl_CarrierFreq;
    /*sourcePhysCellId*/
    pstruDecode->u16SourcePci = phandoverPreparationInformation_r8->as_Context.reestablishmentInfo.sourcePhysCellId;
    /*�м����*/
    u32SourceCellId = (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[0];
    u32SourceCellId <<= 8;
    u32SourceCellId |= (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[1];
    u32SourceCellId <<= 8;
    u32SourceCellId |= (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[2];
    u32SourceCellId <<= 8;
    u32SourceCellId |= (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[3];
    /*��ֵ���*/
    pstruDecode->u8SourceCellId = (u8)(u32SourceCellId & 0xFF);
    pstruDecode->u32SourceEnbId = (u32SourceCellId >> 8) & 0xFFFFF;
    return;
}
/*******************************  Դ�ļ�����  *********************************/

