/*******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
********************************************************************************
* �ļ�����: 
* ��������: 
* ����˵��: 
* ��д����: 
* �޸���ʷ: 
* �޸�����    �޸���  BugID/CRID      �޸�����
* ------------------------------------------------------------------------------
*
*******************************************************************************/

/******************************** ͷ�ļ�������ͷ ******************************/
#ifndef X2AP_PER_DECODE_H
#define X2AP_PER_DECODE_H

/*******************************  ͷ�ļ�����  *********************************/
#include "cdl_common.h"
/******************************** ��ͳ������� ********************************/
#define L3_MAX_RRC_CONTEXT              1000    /*RRC�����ĳ���             */
#define X2AP_MACRO_MAX_EUTRAN_CELL_ID_BIT_LEN   28  /*Eutran Cell ID���س���*/
#define X2AP_MACRO_MAX_EUTRAN_CELL_ID_BYTE_LEN  4   /*Eutran Cell ID�ֽڳ���*/

/* X2AP ���̺�*/
#define X2AP_PROC_ID_handoverPreparation  0
#define X2AP_PROC_ID_handoverCancel  1
#define X2AP_PROC_ID_loadIndication  2
#define X2AP_PROC_ID_errorIndication  3
#define X2AP_PROC_ID_snStatusTransfer  4
#define X2AP_PROC_ID_uEContextRelease  5
#define X2AP_PROC_ID_x2Setup  6
#define X2AP_PROC_ID_reset  7
#define X2AP_PROC_ID_eNBConfigurationUpdate  8
#define X2AP_PROC_ID_resourceStatusReportingInitiation  9
#define X2AP_PROC_ID_resourceStatusReporting  10
#define X2AP_PROC_ID_privateMessage  11
#define X2AP_PROC_ID_mobilitySettingsChange  12
#define X2AP_PROC_ID_rLFIndication  13
#define X2AP_PROC_ID_handoverReport  14
#define X2AP_PROC_ID_cellActivation  15
/* X2APЭ��IE*/
#define X2AP_ID_E_RABs_Admitted_Item  0
#define X2AP_ID_E_RABs_Admitted_List  1
#define X2AP_ID_E_RAB_Item  2
#define X2AP_ID_E_RABs_NotAdmitted_List  3
#define X2AP_ID_E_RABs_ToBeSetup_Item  4
#define X2AP_ID_Cause  5
#define X2AP_ID_CellInformation  6
#define X2AP_ID_CellInformation_Item  7
#define X2AP_ID_New_eNB_UE_X2AP_ID  9
#define X2AP_ID_Old_eNB_UE_X2AP_ID  10
#define X2AP_ID_TargetCell_ID  11
#define X2AP_ID_TargeteNBtoSource_eNBTransparentContainer  12
#define X2AP_ID_TraceActivation  13
#define X2AP_ID_UE_ContextInformation  14
#define X2AP_ID_UE_HistoryInformation  15
#define X2AP_ID_UE_X2AP_ID  16
#define X2AP_ID_CriticalityDiagnostics  17
#define X2AP_ID_E_RABs_SubjectToStatusTransfer_List  18
#define X2AP_ID_E_RABs_SubjectToStatusTransfer_Item  19
#define X2AP_ID_ServedCells  20
#define X2AP_ID_GlobalENB_ID  21
#define X2AP_ID_TimeToWait  22
#define X2AP_ID_GUMMEI_ID  23
#define X2AP_ID_GUGroupIDList  24
#define X2AP_ID_ServedCellsToAdd  25
#define X2AP_ID_ServedCellsToModify  26
#define X2AP_ID_ServedCellsToDelete  27
#define X2AP_ID_Registration_Request  28
#define X2AP_ID_CellToReport  29
#define X2AP_ID_ReportingPeriodicity  30
#define X2AP_ID_CellToReport_Item  31
#define X2AP_ID_CellMeasurementResult  32
#define X2AP_ID_CellMeasurementResult_Item  33
#define X2AP_ID_GUGroupIDToAddList  34
#define X2AP_ID_GUGroupIDToDeleteList  35
#define X2AP_ID_SRVCCOperationPossible  36
#define X2AP_ID_Measurement_ID  37
#define X2AP_ID_ReportCharacteristics  38
#define X2AP_ID_ENB1_Measurement_ID  39
#define X2AP_ID_ENB2_Measurement_ID  40
#define X2AP_ID_Number_of_Antennaports  41
#define X2AP_ID_CompositeAvailableCapacityGroup  42
#define X2AP_ID_ENB1_Cell_ID  43
#define X2AP_ID_ENB2_Cell_ID  44
#define X2AP_ID_ENB2_Proposed_Mobility_Parameters  45
#define X2AP_ID_ENB1_Mobility_Parameters  46
#define X2AP_ID_ENB2_Mobility_Parameters_Modification_Range  47
#define X2AP_ID_FailureCellPCI  48
#define X2AP_ID_Re_establishmentCellECGI  49
#define X2AP_ID_FailureCellCRNTI  50
#define X2AP_ID_ShortMAC_I  51
#define X2AP_ID_SourceCellECGI  52
#define X2AP_ID_FailureCellECGI  53
#define X2AP_ID_HandoverReportType  54
#define X2AP_ID_PRACH_Configuration  55
#define X2AP_ID_MBSFN_Subframe_Info  56
#define X2AP_ID_ServedCellsToActivate  57
#define X2AP_ID_ActivatedCellList  58
#define X2AP_ID_DeactivationIndication  59
#define X2AP_ID_UE_RLF_Report_Container  60

/********************************  ���Ͷ���  **********************************/
/*������Ϣ����ṹ*/
typedef struct
{
    u32             u32SourceMmeUeId;           /* Source MME UE ID */ 
    u16             u16OldX2apUeId;             /* Old UE X2AP ID */ 
    u32             u32TargetEnbId;             /* Target Enb ID */ 
    u8              u8TargetCellId;             /* Target cell ID */
    u8              u8CauseType;                /* �л�ԭ������*/
    u8              u8CauseValue;               /* �л�ԭ��ֵ*/
    u16             u16NofRrcContext;           /* RRC�����ĳ��� */
    u8              u8RrcContext[L3_MAX_RRC_CONTEXT];   /* RRC������ */    
}X2AP_HO_PrepReqDecode;

/****************************  ����ԭ������  ******************************/
void X2AP_HO_PrepReqDecodeFunc(void *pMsg, X2AP_HO_PrepReqDecode *pstruDecode);
/******************************** ͷ�ļ�������β ******************************/
#endif
/******************************  ͷ�ļ�����  **********************************/

