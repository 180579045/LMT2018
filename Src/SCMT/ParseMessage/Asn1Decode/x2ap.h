/*************************************************************/
/* Copyright (C) 2011 OSS Nokalva, Inc.  All rights reserved.*/
/*************************************************************/

/* THIS FILE IS PROPRIETARY MATERIAL OF OSS NOKALVA, INC.
 * AND MAY BE USED ONLY BY DIRECT LICENSEES OF OSS NOKALVA, INC.
 * THIS FILE MAY NOT BE DISTRIBUTED. */

/* Generated for: Datang Mobile Communications Equipment CO.,LTD, Beijing, China - Windows license 9319 (C) & 9652 (ASN-1Step) */
/* Abstract syntax: x2ap */
/* Created: Fri Apr 15 10:00:04 2011 */
/* ASN.1 compiler version: 8.4 */
/* Code generated for runtime version 8.4 or later */
/* Compiler operating system: Windows */
/* Compiler machine type: Intel x86 */
/* Target operating system: Windows */
/* Target machine type: Intel x86 */
/* C compiler options required: -Zp4 (Microsoft) */
/* ASN.1 compiler options and file names specified:
 * -controlfile x2ap.c -headerfile x2ap.h -errorfile asn_error.txt -prefix
 * X2AP_ -externalname x2ap -debug -per -root -autoencdec -compat nosharedtypes
 * asn1dflt.ms.zp4 36423vc80.asn
 */

#ifndef OSS_x2ap
#define OSS_x2ap

/* 9.3.8	Container definitions */
/* ************************************************************** */
/**/
/* Container definitions */
/**/
/* ************************************************************** */

#include "ossasn1.h"

#define          X2AP_X2AP_PDU_PDU 1
#define          X2AP_HandoverRequest_PDU 2
#define          X2AP_UE_ContextInformation_PDU 3
#define          X2AP_E_RABs_ToBeSetup_Item_PDU 4
#define          X2AP_MobilityInformation_PDU 5
#define          X2AP_HandoverRequestAcknowledge_PDU 6
#define          X2AP_E_RABs_Admitted_List_PDU 7
#define          X2AP_E_RABs_Admitted_Item_PDU 8
#define          X2AP_HandoverPreparationFailure_PDU 9
#define          X2AP_HandoverReport_PDU 10
#define          X2AP_SNStatusTransfer_PDU 11
#define          X2AP_E_RABs_SubjectToStatusTransfer_List_PDU 12
#define          X2AP_E_RABs_SubjectToStatusTransfer_Item_PDU 13
#define          X2AP_UEContextRelease_PDU 14
#define          X2AP_HandoverCancel_PDU 15
#define          X2AP_ErrorIndication_PDU 16
#define          X2AP_ResetRequest_PDU 17
#define          X2AP_ResetResponse_PDU 18
#define          X2AP_X2SetupRequest_PDU 19
#define          X2AP_X2SetupResponse_PDU 20
#define          X2AP_X2SetupFailure_PDU 21
#define          X2AP_LoadInformation_PDU 22
#define          X2AP_CellInformation_List_PDU 23
#define          X2AP_CellInformation_Item_PDU 24
#define          X2AP_ENBConfigurationUpdate_PDU 25
#define          X2AP_ServedCellsToModify_PDU 26
#define          X2AP_Old_ECGIs_PDU 27
#define          X2AP_ENBConfigurationUpdateAcknowledge_PDU 28
#define          X2AP_ENBConfigurationUpdateFailure_PDU 29
#define          X2AP_ResourceStatusRequest_PDU 30
#define          X2AP_CellToReport_List_PDU 31
#define          X2AP_CellToReport_Item_PDU 32
#define          X2AP_ReportingPeriodicity_PDU 33
#define          X2AP_PartialSuccessIndicator_PDU 34
#define          X2AP_ResourceStatusResponse_PDU 35
#define          X2AP_MeasurementInitiationResult_List_PDU 36
#define          X2AP_MeasurementInitiationResult_Item_PDU 37
#define          X2AP_MeasurementFailureCause_Item_PDU 38
#define          X2AP_ResourceStatusFailure_PDU 39
#define          X2AP_CompleteFailureCauseInformation_List_PDU 40
#define          X2AP_CompleteFailureCauseInformation_Item_PDU 41
#define          X2AP_ResourceStatusUpdate_PDU 42
#define          X2AP_CellMeasurementResult_List_PDU 43
#define          X2AP_CellMeasurementResult_Item_PDU 44
#define          X2AP_PrivateMessage_PDU 45
#define          X2AP_MobilityChangeRequest_PDU 46
#define          X2AP_MobilityChangeAcknowledge_PDU 47
#define          X2AP_MobilityChangeFailure_PDU 48
#define          X2AP_RLFIndication_PDU 49
#define          X2AP_CellActivationRequest_PDU 50
#define          X2AP_ServedCellsToActivate_PDU 51
#define          X2AP_CellActivationResponse_PDU 52
#define          X2AP_ActivatedCellList_PDU 53
#define          X2AP_CellActivationFailure_PDU 54
#define          X2AP_X2Release_PDU 55
#define          X2AP_X2APMessageTransfer_PDU 56
#define          X2AP_RNL_Header_PDU 57
#define          X2AP_X2AP_Message_PDU 58
#define          X2AP_SeNBAdditionRequest_PDU 59
#define          X2AP_E_RABs_ToBeAdded_List_PDU 60
#define          X2AP_E_RABs_ToBeAdded_Item_PDU 61
#define          X2AP_SeNBAdditionRequestAcknowledge_PDU 62
#define          X2AP_E_RABs_Admitted_ToBeAdded_List_PDU 63
#define          X2AP_E_RABs_Admitted_ToBeAdded_Item_PDU 64
#define          X2AP_SeNBAdditionRequestReject_PDU 65
#define          X2AP_SeNBReconfigurationComplete_PDU 66
#define          X2AP_ResponseInformationSeNBReconfComp_PDU 67
#define          X2AP_SeNBModificationRequest_PDU 68
#define          X2AP_UE_ContextInformationSeNBModReq_PDU 69
#define          X2AP_E_RABs_ToBeAdded_ModReqItem_PDU 70
#define          X2AP_E_RABs_ToBeModified_ModReqItem_PDU 71
#define          X2AP_E_RABs_ToBeReleased_ModReqItem_PDU 72
#define          X2AP_SeNBModificationRequestAcknowledge_PDU 73
#define          X2AP_E_RABs_Admitted_ToBeAdded_ModAckList_PDU 74
#define          X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_PDU 75
#define          X2AP_E_RABs_Admitted_ToBeModified_ModAckList_PDU 76
#define          X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_PDU 77
#define          X2AP_E_RABs_Admitted_ToBeReleased_ModAckList_PDU 78
#define          X2AP_E_RABs_Admitted_ToReleased_ModAckItem_PDU 79
#define          X2AP_SeNBModificationRequestReject_PDU 80
#define          X2AP_SeNBModificationRequired_PDU 81
#define          X2AP_E_RABs_ToBeReleased_ModReqd_PDU 82
#define          X2AP_E_RABs_ToBeReleased_ModReqdItem_PDU 83
#define          X2AP_SeNBModificationConfirm_PDU 84
#define          X2AP_SeNBModificationRefuse_PDU 85
#define          X2AP_SeNBReleaseRequest_PDU 86
#define          X2AP_E_RABs_ToBeReleased_List_RelReq_PDU 87
#define          X2AP_E_RABs_ToBeReleased_RelReqItem_PDU 88
#define          X2AP_SeNBReleaseRequired_PDU 89
#define          X2AP_SeNBReleaseConfirm_PDU 90
#define          X2AP_E_RABs_ToBeReleased_List_RelConf_PDU 91
#define          X2AP_E_RABs_ToBeReleased_RelConfItem_PDU 92
#define          X2AP_SeNBCounterCheckRequest_PDU 93
#define          X2AP_E_RABs_SubjectToCounterCheck_List_PDU 94
#define          X2AP_E_RABs_SubjectToCounterCheckItem_PDU 95
#define          X2AP_X2RemovalRequest_PDU 96
#define          X2AP_X2RemovalResponse_PDU 97
#define          X2AP_X2RemovalFailure_PDU 98
#define          X2AP_ABSInformation_PDU 99
#define          X2AP_ABS_Status_PDU 100
#define          X2AP_AdditionalSpecialSubframe_Info_PDU 101
#define          X2AP_Cause_PDU 102
#define          X2AP_CoMPInformation_PDU 103
#define          X2AP_CompositeAvailableCapacityGroup_PDU 104
#define          X2AP_COUNTValueExtended_PDU 105
#define          X2AP_CriticalityDiagnostics_PDU 106
#define          X2AP_CRNTI_PDU 107
#define          X2AP_CSGMembershipStatus_PDU 108
#define          X2AP_CSG_Id_PDU 109
#define          X2AP_DeactivationIndication_PDU 110
#define          X2AP_DynamicDLTransmissionInformation_PDU 111
#define          X2AP_EARFCNExtension_PDU 112
#define          X2AP_ECGI_PDU 113
#define          X2AP_E_RAB_List_PDU 114
#define          X2AP_E_RAB_Item_PDU 115
#define          X2AP_ExpectedUEBehaviour_PDU 116
#define          X2AP_ExtendedULInterferenceOverloadInfo_PDU 117
#define          X2AP_FreqBandIndicatorPriority_PDU 118
#define          X2AP_GlobalENB_ID_PDU 119
#define          X2AP_GUGroupIDList_PDU 120
#define          X2AP_GUMMEI_PDU 121
#define          X2AP_HandoverReportType_PDU 122
#define          X2AP_Masked_IMEISV_PDU 123
#define          X2AP_InvokeIndication_PDU 124
#define          X2AP_M3Configuration_PDU 125
#define          X2AP_M4Configuration_PDU 126
#define          X2AP_M5Configuration_PDU 127
#define          X2AP_MDT_Configuration_PDU 128
#define          X2AP_MDTPLMNList_PDU 129
#define          X2AP_MDT_Location_Info_PDU 130
#define          X2AP_MeNBtoSeNBContainer_PDU 131
#define          X2AP_Measurement_ID_PDU 132
#define          X2AP_MBMS_Service_Area_Identity_List_PDU 133
#define          X2AP_MBSFN_Subframe_Infolist_PDU 134
#define          X2AP_ManagementBasedMDTallowed_PDU 135
#define          X2AP_MobilityParametersModificationRange_PDU 136
#define          X2AP_MobilityParametersInformation_PDU 137
#define          X2AP_MultibandInfoList_PDU 138
#define          X2AP_Number_of_Antennaports_PDU 139
#define          X2AP_PCI_PDU 140
#define          X2AP_PLMN_Identity_PDU 141
#define          X2AP_PRACH_Configuration_PDU 142
#define          X2AP_ProSeAuthorized_PDU 143
#define          X2AP_ReceiveStatusOfULPDCPSDUsExtended_PDU 144
#define          X2AP_Registration_Request_PDU 145
#define          X2AP_ReportingPeriodicityRSRPMR_PDU 146
#define          X2AP_ReportCharacteristics_PDU 147
#define          X2AP_RRCConnReestabIndicator_PDU 148
#define          X2AP_RRCConnSetupIndicator_PDU 149
#define          X2AP_RSRPMRList_PDU 150
#define          X2AP_SCGChangeIndication_PDU 151
#define          X2AP_SeNBSecurityKey_PDU 152
#define          X2AP_SeNBtoMeNBContainer_PDU 153
#define          X2AP_ServedCells_PDU 154
#define          X2AP_ShortMAC_I_PDU 155
#define          X2AP_SRVCCOperationPossible_PDU 156
#define          X2AP_SubframeAssignment_PDU 157
#define          X2AP_TAC_PDU 158
#define          X2AP_TargetCellInUTRAN_PDU 159
#define          X2AP_TargeteNBtoSource_eNBTransparentContainer_PDU 160
#define          X2AP_TimeToWait_PDU 161
#define          X2AP_Time_UE_StayedInCell_EnhancedGranularity_PDU 162
#define          X2AP_TraceActivation_PDU 163
#define          X2AP_UE_HistoryInformation_PDU 164
#define          X2AP_UE_HistoryInformationFromTheUE_PDU 165
#define          X2AP_UE_X2AP_ID_PDU 166
#define          X2AP_UEAggregateMaximumBitRate_PDU 167
#define          X2AP_UESecurityCapabilities_PDU 168
#define          X2AP_UE_RLF_Report_Container_PDU 169
#define          X2AP_UE_RLF_Report_Container_for_extended_bands_PDU 170
#define          X2AP_X2AP_ELEMENTARY_PROCEDURES_OSET 1 /* Class is X2AP-ELEMENTARY-PROCEDURE */
#define          X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1_OSET 2 /* Class is X2AP-ELEMENTARY-PROCEDURE */
#define          X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2_OSET 3 /* Class is X2AP-ELEMENTARY-PROCEDURE */
#define          X2AP_HandoverRequest_IEs_OSET 4 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_UE_ContextInformation_ExtIEs_OSET 5 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeSetup_ItemIEs_OSET 6 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeSetup_ItemExtIEs_OSET 7 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_HandoverRequestAcknowledge_IEs_OSET 8 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_Admitted_Item_ExtIEs_OSET 9 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_HandoverPreparationFailure_IEs_OSET 10 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_HandoverReport_IEs_OSET 11 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_SNStatusTransfer_IEs_OSET 12 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs_OSET 13 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_UEContextRelease_IEs_OSET 14 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_HandoverCancel_IEs_OSET 15 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ErrorIndication_IEs_OSET 16 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ResetRequest_IEs_OSET 17 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ResetResponse_IEs_OSET 18 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_X2SetupRequest_IEs_OSET 19 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_X2SetupResponse_IEs_OSET 20 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_X2SetupFailure_IEs_OSET 21 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_LoadInformation_IEs_OSET 22 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CellInformation_Item_ExtIEs_OSET 23 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ENBConfigurationUpdate_IEs_OSET 24 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ServedCellsToModify_Item_ExtIEs_OSET 25 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ENBConfigurationUpdateAcknowledge_IEs_OSET 26 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ENBConfigurationUpdateFailure_IEs_OSET 27 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ResourceStatusRequest_IEs_OSET 28 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CellToReport_Item_ExtIEs_OSET 29 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ResourceStatusResponse_IEs_OSET 30 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_MeasurementInitiationResult_Item_ExtIEs_OSET 31 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_MeasurementFailureCause_Item_ExtIEs_OSET 32 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ResourceStatusFailure_IEs_OSET 33 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CompleteFailureCauseInformation_Item_ExtIEs_OSET 34 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ResourceStatusUpdate_IEs_OSET 35 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CellMeasurementResult_Item_ExtIEs_OSET 36 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_PrivateMessage_IEs_OSET 37 /* Class is X2AP-PRIVATE-IES */
#define          X2AP_MobilityChangeRequest_IEs_OSET 38 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_MobilityChangeAcknowledge_IEs_OSET 39 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_MobilityChangeFailure_IEs_OSET 40 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_RLFIndication_IEs_OSET 41 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CellActivationRequest_IEs_OSET 42 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ServedCellsToActivate_Item_ExtIEs_OSET 43 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CellActivationResponse_IEs_OSET 44 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ActivatedCellList_Item_ExtIEs_OSET 45 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CellActivationFailure_IEs_OSET 46 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_X2Release_IEs_OSET 47  /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_X2APMessageTransfer_IEs_OSET 48 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_RNL_Header_Item_ExtIEs_OSET 49 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBAdditionRequest_IEs_OSET 50 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeAdded_ItemIEs_OSET 51 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeAdded_Item_SCG_BearerExtIEs_OSET 52 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeAdded_Item_Split_BearerExtIEs_OSET 53 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBAdditionRequestAcknowledge_IEs_OSET 54 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_BearerExtIEs_OSET 55 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_BearerExtIEs_OSET 56 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBAdditionRequestReject_IEs_OSET 57 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_SeNBReconfigurationComplete_IEs_OSET 58 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ResponseInformationSeNBReconfComp_SuccessItemExtIEs_OSET 59 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItemExtIEs_OSET 60 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBModificationRequest_IEs_OSET 61 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_UE_ContextInformationSeNBModReqExtIEs_OSET 62 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeAdded_ModReqItemIEs_OSET 63 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_BearerExtIEs_OSET 64 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeAdded_ModReqItem_Split_BearerExtIEs_OSET 65 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeModified_ModReqItemIEs_OSET 66 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeModified_ModReqItem_SCG_BearerExtIEs_OSET 67 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeModified_ModReqItem_Split_BearerExtIEs_OSET 68 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeReleased_ModReqItemIEs_OSET 69 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_BearerExtIEs_OSET 70 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeReleased_ModReqItem_Split_BearerExtIEs_OSET 71 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBModificationRequestAcknowledge_IEs_OSET 72 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_BearerExtIEs_OSET 73 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_BearerExtIEs_OSET 74 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_BearerExtIEs_OSET 75 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_BearerExtIEs_OSET 76 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_BearerExtIEs_OSET 77 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_BearerExtIEs_OSET 78 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBModificationRequestReject_IEs_OSET 79 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_SeNBModificationRequired_IEs_OSET 80 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeReleased_ModReqdItemIEs_OSET 81 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeReleased_ModReqdItemExtIEs_OSET 82 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBModificationConfirm_IEs_OSET 83 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_SeNBModificationRefuse_IEs_OSET 84 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_SeNBReleaseRequest_IEs_OSET 85 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeReleased_RelReqItemIEs_OSET 86 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_BearerExtIEs_OSET 87 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeReleased_RelReqItem_Split_BearerExtIEs_OSET 88 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBReleaseRequired_IEs_OSET 89 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_SeNBReleaseConfirm_IEs_OSET 90 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeReleased_RelConfItemIEs_OSET 91 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_BearerExtIEs_OSET 92 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_ToBeReleased_RelConfItem_Split_BearerExtIEs_OSET 93 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SeNBCounterCheckRequest_IEs_OSET 94 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_SubjectToCounterCheckItemIEs_OSET 95 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_SubjectToCounterCheckItemExtIEs_OSET 96 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_X2RemovalRequest_IEs_OSET 97 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_X2RemovalResponse_IEs_OSET 98 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_X2RemovalFailure_IEs_OSET 99 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_ABSInformationFDD_ExtIEs_OSET 100 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ABSInformationTDD_ExtIEs_OSET 101 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ABS_Status_ExtIEs_OSET 102 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_AdditionalSpecialSubframe_Info_ExtIEs_OSET 103 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_AS_SecurityInformation_ExtIEs_OSET 104 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_AllocationAndRetentionPriority_ExtIEs_OSET 105 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CellBasedMDT_ExtIEs_OSET 106 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CellType_ExtIEs_OSET 107 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CoMPHypothesisSetItem_ExtIEs_OSET 108 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CoMPInformation_ExtIEs_OSET 109 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CoMPInformationItem_ExtIEs_OSET 110 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CoMPInformationStartTime_ExtIEs_OSET 111 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CompositeAvailableCapacityGroup_ExtIEs_OSET 112 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CompositeAvailableCapacity_ExtIEs_OSET 113 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_COUNTvalue_ExtIEs_OSET 114 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_COUNTValueExtended_ExtIEs_OSET 115 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CriticalityDiagnostics_ExtIEs_OSET 116 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_CriticalityDiagnostics_IE_List_ExtIEs_OSET 117 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_DynamicNAICSInformation_ExtIEs_OSET 118 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_FDD_Info_ExtIEs_OSET 119 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_TDD_Info_ExtIEs_OSET 120 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ECGI_ExtIEs_OSET 121   /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RAB_Level_QoS_Parameters_ExtIEs_OSET 122 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RAB_ItemIEs_OSET 123 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RAB_Item_ExtIEs_OSET 124 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ExpectedUEBehaviour_ExtIEs_OSET 125 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ExpectedUEActivityBehaviour_ExtIEs_OSET 126 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ExtendedULInterferenceOverloadInfo_ExtIEs_OSET 127 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ForbiddenTAs_Item_ExtIEs_OSET 128 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ForbiddenLAs_Item_ExtIEs_OSET 129 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_GBR_QosInformation_ExtIEs_OSET 130 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_GlobalENB_ID_ExtIEs_OSET 131 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_GTPtunnelEndpoint_ExtIEs_OSET 132 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_GU_Group_ID_ExtIEs_OSET 133 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_GUMMEI_ExtIEs_OSET 134 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_HandoverRestrictionList_ExtIEs_OSET 135 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_HWLoadIndicator_ExtIEs_OSET 136 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_LastVisitedEUTRANCellInformation_ExtIEs_OSET 137 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_LocationReportingInformation_ExtIEs_OSET 138 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_M3Configuration_ExtIEs_OSET 139 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_M4Configuration_ExtIEs_OSET 140 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_M5Configuration_ExtIEs_OSET 141 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_MDT_Configuration_ExtIEs_OSET 142 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_MBSFN_Subframe_Info_ExtIEs_OSET 143 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_BandInfo_ExtIEs_OSET 144 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_Neighbour_Information_ExtIEs_OSET 145 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_M1PeriodicReporting_ExtIEs_OSET 146 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_PRACH_Configuration_ExtIEs_OSET 147 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ProSeAuthorized_ExtIEs_OSET 148 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_RadioResourceStatus_ExtIEs_OSET 149 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_RelativeNarrowbandTxPower_ExtIEs_OSET 150 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_RSRPMeasurementResult_ExtIEs_OSET 151 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_RSRPMRList_ExtIEs_OSET 152 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_S1TNLLoadIndicator_ExtIEs_OSET 153 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ServedCell_ExtIEs_OSET 154 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_ServedCell_Information_ExtIEs_OSET 155 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_SpecialSubframe_Info_ExtIEs_OSET 156 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_TABasedMDT_ExtIEs_OSET 157 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_TAIBasedMDT_ExtIEs_OSET 158 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_TAI_Item_ExtIEs_OSET 159 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_M1ThresholdEventA2_ExtIEs_OSET 160 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_TraceActivation_ExtIEs_OSET 161 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_UEAggregate_MaximumBitrate_ExtIEs_OSET 162 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_UESecurityCapabilities_ExtIEs_OSET 163 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_UL_HighInterferenceIndicationInfo_Item_ExtIEs_OSET 164 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_UsableABSInformationFDD_ExtIEs_OSET 165 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_UsableABSInformationTDD_ExtIEs_OSET 166 /* Class is X2AP-PROTOCOL-EXTENSION */
#define          X2AP_E_RABs_Admitted_ItemIEs_OSET 167 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CompleteFailureCauseInformation_ItemIEs_OSET 168 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_Admitted_ToBeModified_ModAckItemIEs_OSET 169 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_SubjectToStatusTransfer_ItemIEs_OSET 170 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_MeasurementInitiationResult_ItemIEs_OSET 171 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_Admitted_ToBeReleased_ModAckItemIEs_OSET 172 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CellMeasurementResult_ItemIEs_OSET 173 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CellInformation_ItemIEs_OSET 174 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_MeasurementFailureCause_ItemIEs_OSET 175 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_CellToReport_ItemIEs_OSET 176 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_Admitted_ToBeAdded_ItemIEs_OSET 177 /* Class is X2AP-PROTOCOL-IES */
#define          X2AP_E_RABs_Admitted_ToBeAdded_ModAckItemIEs_OSET 178 /* Class is X2AP-PROTOCOL-IES */

typedef struct X2AP__ObjectID {
    unsigned short  length;
    unsigned char   *value;
} X2AP__ObjectID;

typedef unsigned short  X2AP_ProcedureCode;

/* ************************************************************** */
/**/
/* Common Data Types */
/**/
/* ************************************************************** */
typedef enum X2AP_Criticality {
    X2AP_reject = 0,
    X2AP_ignore = 1,
    X2AP_notify = 2
} X2AP_Criticality;

/* ************************************************************** */
/**/
/* IE parameter types from other modules. */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* Interface Elementary Procedure Class */
/**/
/* ************************************************************** */
typedef struct X2AP_X2AP_ELEMENTARY_PROCEDURE {
    unsigned char   bit_mask;
#       define      X2AP_SuccessfulOutcome_present 0x80
#       define      X2AP_UnsuccessfulOutcome_present 0x40
#       define      X2AP_criticality_present 0x20
    unsigned short  InitiatingMessage;
    unsigned short  SuccessfulOutcome;  /* optional; set in bit_mask
                                         * X2AP_SuccessfulOutcome_present if
                                         * present */
    unsigned short  UnsuccessfulOutcome;  /* optional; set in bit_mask
                                           * X2AP_UnsuccessfulOutcome_present if
                                           * present */
    X2AP_ProcedureCode procedureCode;
    X2AP_Criticality criticality;  /* X2AP_criticality_present not set in
                                    * bit_mask implies value is ignore */
} X2AP_X2AP_ELEMENTARY_PROCEDURE;

typedef struct X2AP_InitiatingMessage {
    X2AP_ProcedureCode procedureCode;
    X2AP_Criticality criticality;
    OpenType        value;
} X2AP_InitiatingMessage;

typedef struct X2AP_SuccessfulOutcome {
    X2AP_ProcedureCode procedureCode;
    X2AP_Criticality criticality;
    OpenType        value;
} X2AP_SuccessfulOutcome;

typedef struct X2AP_UnsuccessfulOutcome {
    X2AP_ProcedureCode procedureCode;
    X2AP_Criticality criticality;
    OpenType        value;
} X2AP_UnsuccessfulOutcome;

/* ************************************************************** */
/**/
/* Interface PDU Definition */
/**/
/* ************************************************************** */
typedef struct X2AP_X2AP_PDU {
    unsigned short  choice;
#       define      X2AP_initiatingMessage_chosen 1
#       define      X2AP_successfulOutcome_chosen 2
#       define      X2AP_unsuccessfulOutcome_chosen 3
    union {
        X2AP_InitiatingMessage initiatingMessage;  /* to choose, set choice to
                                             * X2AP_initiatingMessage_chosen */
        X2AP_SuccessfulOutcome successfulOutcome;  /* to choose, set choice to
                                             * X2AP_successfulOutcome_chosen */
        X2AP_UnsuccessfulOutcome unsuccessfulOutcome;  /* to choose, set choice
                                        * to X2AP_unsuccessfulOutcome_chosen */
    } u;
} X2AP_X2AP_PDU;

typedef unsigned short  X2AP_ProtocolIE_ID;

typedef struct X2AP_ProtocolIE_Field {
    X2AP_ProtocolIE_ID id;
    X2AP_Criticality criticality;
    OpenType        value;
} X2AP_ProtocolIE_Field;

typedef struct X2AP_ProtocolIE_Container {
    struct X2AP_ProtocolIE_Container *next;
    X2AP_ProtocolIE_Field value;
} *X2AP_ProtocolIE_Container;

/* ************************************************************** */
/**/
/* IE parameter types from other modules. */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* HANDOVER REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_HandoverRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_HandoverRequest;

/* This IE is a transparent container and shall be encoded as the VisitedCellInfoList field contained in the UEInformationResponse message as defined in TS 36.331 [9] */
typedef unsigned int    X2AP_UE_S1AP_ID;

typedef struct X2AP_EncryptionAlgorithms {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_EncryptionAlgorithms;

typedef struct X2AP_IntegrityProtectionAlgorithms {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_IntegrityProtectionAlgorithms;

typedef struct X2AP_ProtocolExtensionField {
    X2AP_ProtocolIE_ID id;
    X2AP_Criticality criticality;
    OpenType        extensionValue;
} X2AP_ProtocolExtensionField;

typedef struct X2AP_ProtocolExtensionContainer {
    struct X2AP_ProtocolExtensionContainer *next;
    X2AP_ProtocolExtensionField value;
} *X2AP_ProtocolExtensionContainer;

typedef struct X2AP_UESecurityCapabilities {
    unsigned char   bit_mask;
#       define      X2AP_UESecurityCapabilities_iE_Extensions_present 0x80
    X2AP_EncryptionAlgorithms encryptionAlgorithms;
    X2AP_IntegrityProtectionAlgorithms integrityProtectionAlgorithms;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * X2AP_UESecurityCapabilities_iE_Extensions_present if
                         * present */
} X2AP_UESecurityCapabilities;

/* J */
/* K */
typedef struct X2AP_Key_eNodeB_Star {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_Key_eNodeB_Star;

typedef unsigned short  X2AP_NextHopChainingCount;

typedef struct X2AP_AS_SecurityInformation {
    unsigned char   bit_mask;
#       define      X2AP_AS_SecurityInformation_iE_Extensions_present 0x80
    X2AP_Key_eNodeB_Star key_eNodeB_star;
    X2AP_NextHopChainingCount nextHopChainingCount;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * X2AP_AS_SecurityInformation_iE_Extensions_present if
                         * present */
} X2AP_AS_SecurityInformation;

typedef ULONG_LONG      X2AP_BitRate;

typedef struct X2AP_UEAggregateMaximumBitRate {
    unsigned char   bit_mask;
#       define      X2AP_UEAggregateMaximumBitRate_iE_Extensions_present 0x80
    X2AP_BitRate    uEaggregateMaximumBitRateDownlink;
    X2AP_BitRate    uEaggregateMaximumBitRateUplink;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                      * X2AP_UEAggregateMaximumBitRate_iE_Extensions_present if
                      * present */
} X2AP_UEAggregateMaximumBitRate;

typedef unsigned short  X2AP_SubscriberProfileIDforRFP;

typedef struct X2AP_RRC_Context {
    unsigned int    length;
    unsigned char   *value;
} X2AP_RRC_Context;

typedef struct X2AP_PLMN_Identity {
    unsigned short  length;
    unsigned char   value[3];
} X2AP_PLMN_Identity;

/* F */
typedef enum X2AP_ForbiddenInterRATs {
    X2AP_all = 0,
    X2AP_geran = 1,
    X2AP_utran = 2,
    X2AP_cdma2000 = 3,
    X2AP_geranandutran = 4,
    X2AP_cdma2000andutran = 5
} X2AP_ForbiddenInterRATs;

typedef struct X2AP_HandoverRestrictionList {
    unsigned char   bit_mask;
#       define      X2AP_equivalentPLMNs_present 0x80
#       define      X2AP_forbiddenTAs_present 0x40
#       define      X2AP_forbiddenLAs_present 0x20
#       define      X2AP_forbiddenInterRATs_present 0x10
#       define      X2AP_HandoverRestrictionList_iE_Extensions_present 0x08
    X2AP_PLMN_Identity servingPLMN;
    struct X2AP_EPLMNs *equivalentPLMNs;  /* optional; set in bit_mask
                                           * X2AP_equivalentPLMNs_present if
                                           * present */
    struct X2AP_ForbiddenTAs *forbiddenTAs;  /* optional; set in bit_mask
                                              * X2AP_forbiddenTAs_present if
                                              * present */
    struct X2AP_ForbiddenLAs *forbiddenLAs;  /* optional; set in bit_mask
                                              * X2AP_forbiddenLAs_present if
                                              * present */
    X2AP_ForbiddenInterRATs forbiddenInterRATs;  /* optional; set in bit_mask
                                           * X2AP_forbiddenInterRATs_present if
                                           * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                        * X2AP_HandoverRestrictionList_iE_Extensions_present if
                        * present */
} X2AP_HandoverRestrictionList;

typedef enum X2AP_EventType {
    X2AP_change_of_serving_cell = 0
} X2AP_EventType;

typedef enum X2AP_ReportArea {
    X2AP_ecgi = 0
} X2AP_ReportArea;

typedef struct X2AP_LocationReportingInformation {
    unsigned char   bit_mask;
#       define      X2AP_LocationReportingInformation_iE_Extensions_present 0x80
    X2AP_EventType  eventType;
    X2AP_ReportArea reportArea;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                   * X2AP_LocationReportingInformation_iE_Extensions_present if
                   * present */
} X2AP_LocationReportingInformation;

typedef struct X2AP_UE_ContextInformation {
    unsigned char   bit_mask;
#       define      X2AP_subscriberProfileIDforRFP_present 0x80
#       define      X2AP_handoverRestrictionList_present 0x40
#       define      X2AP_locationReportingInformation_present 0x20
#       define      X2AP_UE_ContextInformation_iE_Extensions_present 0x10
    X2AP_UE_S1AP_ID mME_UE_S1AP_ID;
    X2AP_UESecurityCapabilities uESecurityCapabilities;
    X2AP_AS_SecurityInformation aS_SecurityInformation;
    X2AP_UEAggregateMaximumBitRate uEaggregateMaximumBitRate;
    X2AP_SubscriberProfileIDforRFP subscriberProfileIDforRFP;  /* optional; set
                                   * in bit_mask
                                   * X2AP_subscriberProfileIDforRFP_present if
                                   * present */
    struct X2AP_E_RABs_ToBeSetup_List *e_RABs_ToBeSetup_List;
    X2AP_RRC_Context rRC_Context;
    X2AP_HandoverRestrictionList handoverRestrictionList;  /* optional; set in
                                   * bit_mask
                                   * X2AP_handoverRestrictionList_present if
                                   * present */
    X2AP_LocationReportingInformation locationReportingInformation;  
                                  /* optional; set in bit_mask
                                   * X2AP_locationReportingInformation_present
                                   * if present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                          * X2AP_UE_ContextInformation_iE_Extensions_present if
                          * present */
} X2AP_UE_ContextInformation;

typedef struct X2AP_ProtocolIE_Single_Container {
    X2AP_ProtocolIE_ID id;
    X2AP_Criticality criticality;
    OpenType        value;
} X2AP_ProtocolIE_Single_Container;

typedef struct X2AP_E_RABs_ToBeSetup_List {
    struct X2AP_E_RABs_ToBeSetup_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeSetup_List;

typedef int             X2AP_E_RAB_ID;

/* Q */
typedef unsigned short  X2AP_QCI;

typedef unsigned short  X2AP_PriorityLevel;
#define                     X2AP_spare 0U
#define                     X2AP_highest 1U
#define                     X2AP_lowest 14U
#define                     X2AP_no_priority 15U

typedef enum X2AP_Pre_emptionCapability {
    X2AP_shall_not_trigger_pre_emption = 0,
    X2AP_may_trigger_pre_emption = 1
} X2AP_Pre_emptionCapability;

typedef enum X2AP_Pre_emptionVulnerability {
    X2AP_not_pre_emptable = 0,
    X2AP_pre_emptable = 1
} X2AP_Pre_emptionVulnerability;

typedef struct X2AP_AllocationAndRetentionPriority {
    unsigned char   bit_mask;
#       define      X2AP_AllocationAndRetentionPriority_iE_Extensions_present 0x80
    X2AP_PriorityLevel priorityLevel;
    X2AP_Pre_emptionCapability pre_emptionCapability;
    X2AP_Pre_emptionVulnerability pre_emptionVulnerability;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                 * X2AP_AllocationAndRetentionPriority_iE_Extensions_present if
                 * present */
} X2AP_AllocationAndRetentionPriority;

/* G */
typedef struct X2AP_GBR_QosInformation {
    unsigned char   bit_mask;
#       define      X2AP_GBR_QosInformation_iE_Extensions_present 0x80
    X2AP_BitRate    e_RAB_MaximumBitrateDL;
    X2AP_BitRate    e_RAB_MaximumBitrateUL;
    X2AP_BitRate    e_RAB_GuaranteedBitrateDL;
    X2AP_BitRate    e_RAB_GuaranteedBitrateUL;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * X2AP_GBR_QosInformation_iE_Extensions_present if
                             * present */
} X2AP_GBR_QosInformation;

typedef struct X2AP_E_RAB_Level_QoS_Parameters {
    unsigned char   bit_mask;
#       define      X2AP_gbrQosInformation_present 0x80
#       define      X2AP_E_RAB_Level_QoS_Parameters_iE_Extensions_present 0x40
    X2AP_QCI        qCI;
    X2AP_AllocationAndRetentionPriority allocationAndRetentionPriority;
    X2AP_GBR_QosInformation gbrQosInformation;  /* optional; set in bit_mask
                                            * X2AP_gbrQosInformation_present if
                                            * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                     * X2AP_E_RAB_Level_QoS_Parameters_iE_Extensions_present if
                     * present */
} X2AP_E_RAB_Level_QoS_Parameters;

typedef enum X2AP_DL_Forwarding {
    X2AP_dL_forwardingProposed = 0
} X2AP_DL_Forwarding;

typedef struct X2AP_TransportLayerAddress {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_TransportLayerAddress;

typedef struct X2AP_GTP_TEI {
    unsigned short  length;
    unsigned char   value[4];
} X2AP_GTP_TEI;

typedef struct X2AP_GTPtunnelEndpoint {
    unsigned char   bit_mask;
#       define      X2AP_GTPtunnelEndpoint_iE_Extensions_present 0x80
    X2AP_TransportLayerAddress transportLayerAddress;
    X2AP_GTP_TEI    gTP_TEID;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2AP_GTPtunnelEndpoint_iE_Extensions_present if
                              * present */
} X2AP_GTPtunnelEndpoint;

typedef struct X2AP_E_RABs_ToBeSetup_Item {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeSetup_Item_dL_Forwarding_present 0x80
#       define      X2AP_E_RABs_ToBeSetup_Item_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters;
    X2AP_DL_Forwarding dL_Forwarding;  /* optional; set in bit_mask
                          * X2AP_E_RABs_ToBeSetup_Item_dL_Forwarding_present if
                          * present */
    X2AP_GTPtunnelEndpoint uL_GTPtunnelEndpoint;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                          * X2AP_E_RABs_ToBeSetup_Item_iE_Extensions_present if
                          * present */
} X2AP_E_RABs_ToBeSetup_Item;

typedef struct X2AP_MobilityInformation {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_MobilityInformation;

/* ************************************************************** */
/**/
/* HANDOVER REQUEST ACKNOWLEDGE */
/**/
/* ************************************************************** */
typedef struct X2AP_HandoverRequestAcknowledge {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_HandoverRequestAcknowledge;

typedef struct X2AP_E_RABs_Admitted_List {
    struct X2AP_E_RABs_Admitted_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_Admitted_List;

typedef struct X2AP_E_RABs_Admitted_Item {
    unsigned char   bit_mask;
#       define      X2AP_uL_GTP_TunnelEndpoint_present 0x80
#       define      X2AP_dL_GTP_TunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_Admitted_Item_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint uL_GTP_TunnelEndpoint;  /* optional; set in bit_mask
                                        * X2AP_uL_GTP_TunnelEndpoint_present if
                                        * present */
    X2AP_GTPtunnelEndpoint dL_GTP_TunnelEndpoint;  /* optional; set in bit_mask
                                        * X2AP_dL_GTP_TunnelEndpoint_present if
                                        * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                           * X2AP_E_RABs_Admitted_Item_iE_Extensions_present if
                           * present */
} X2AP_E_RABs_Admitted_Item;

/* ************************************************************** */
/**/
/* HANDOVER PREPARATION FAILURE */
/**/
/* ************************************************************** */
typedef struct X2AP_HandoverPreparationFailure {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_HandoverPreparationFailure;

/* ************************************************************** */
/**/
/* Handover Report */
/**/
/* ************************************************************** */
typedef struct X2AP_HandoverReport {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_HandoverReport;

/* ************************************************************** */
/**/
/* SN Status Transfer */
/**/
/* ************************************************************** */
typedef struct X2AP_SNStatusTransfer {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SNStatusTransfer;

typedef struct X2AP_E_RABs_SubjectToStatusTransfer_List {
    struct X2AP_E_RABs_SubjectToStatusTransfer_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_SubjectToStatusTransfer_List;

typedef struct X2AP_ReceiveStatusofULPDCPSDUs {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_ReceiveStatusofULPDCPSDUs;

typedef unsigned short  X2AP_PDCP_SN;

typedef unsigned int    X2AP_HFN;

typedef struct X2AP_COUNTvalue {
    unsigned char   bit_mask;
#       define      X2AP_COUNTvalue_iE_Extensions_present 0x80
    X2AP_PDCP_SN    pDCP_SN;
    X2AP_HFN        hFN;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_COUNTvalue_iE_Extensions_present if
                                   * present */
} X2AP_COUNTvalue;

typedef struct X2AP_E_RABs_SubjectToStatusTransfer_Item {
    unsigned char   bit_mask;
#       define      X2AP_receiveStatusofULPDCPSDUs_present 0x80
#       define      X2AP_E_RABs_SubjectToStatusTransfer_Item_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_ReceiveStatusofULPDCPSDUs receiveStatusofULPDCPSDUs;  /* optional; set
                                   * in bit_mask
                                   * X2AP_receiveStatusofULPDCPSDUs_present if
                                   * present */
    X2AP_COUNTvalue uL_COUNTvalue;
    X2AP_COUNTvalue dL_COUNTvalue;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
            * X2AP_E_RABs_SubjectToStatusTransfer_Item_iE_Extensions_present if
            * present */
} X2AP_E_RABs_SubjectToStatusTransfer_Item;

/* ************************************************************** */
/**/
/* UE Context Release */
/**/
/* ************************************************************** */
typedef struct X2AP_UEContextRelease {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_UEContextRelease;

/* ************************************************************** */
/**/
/* HANDOVER CANCEL */
/**/
/* ************************************************************** */
typedef struct X2AP_HandoverCancel {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_HandoverCancel;

/* ************************************************************** */
/**/
/* ERROR INDICATION */
/**/
/* ************************************************************** */
typedef struct X2AP_ErrorIndication {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ErrorIndication;

/* ************************************************************** */
/**/
/* Reset Request */
/**/
/* ************************************************************** */
typedef struct X2AP_ResetRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ResetRequest;

/* ************************************************************** */
/**/
/* Reset Response */
/**/
/* ************************************************************** */
typedef struct X2AP_ResetResponse {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ResetResponse;

/* ************************************************************** */
/**/
/* X2 SETUP REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_X2SetupRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2SetupRequest;

/* ************************************************************** */
/**/
/* X2 SETUP RESPONSE */
/**/
/* ************************************************************** */
typedef struct X2AP_X2SetupResponse {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2SetupResponse;

/* ************************************************************** */
/**/
/* X2 SETUP FAILURE */
/**/
/* ************************************************************** */
typedef struct X2AP_X2SetupFailure {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2SetupFailure;

/* ************************************************************** */
/**/
/* LOAD INFORMATION */
/**/
/* ************************************************************** */
typedef struct X2AP_LoadInformation {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_LoadInformation;

typedef struct X2AP_CellInformation_List {
    struct X2AP_CellInformation_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_CellInformation_List;

typedef struct X2AP_EUTRANCellIdentifier {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_EUTRANCellIdentifier;

typedef struct X2AP_ECGI {
    unsigned char   bit_mask;
#       define      X2AP_ECGI_iE_Extensions_present 0x80
    X2AP_PLMN_Identity pLMN_Identity;
    X2AP_EUTRANCellIdentifier eUTRANcellIdentifier;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask X2AP_ECGI_iE_Extensions_present if
                                   * present */
} X2AP_ECGI;

typedef enum X2AP_RNTP_Threshold {
    X2AP_minusInfinity = 0,
    X2AP_minusEleven = 1,
    X2AP_minusTen = 2,
    X2AP_minusNine = 3,
    X2AP_minusEight = 4,
    X2AP_minusSeven = 5,
    X2AP_minusSix = 6,
    X2AP_minusFive = 7,
    X2AP_minusFour = 8,
    X2AP_minusThree = 9,
    X2AP_minusTwo = 10,
    X2AP_minusOne = 11,
    X2AP_zero = 12,
    X2AP_RNTP_Threshold_one = 13,
    X2AP_RNTP_Threshold_two = 14,
    X2AP_three = 15
} X2AP_RNTP_Threshold;

typedef struct X2AP__bit1 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP__bit1;

typedef enum X2AP__enum1 {
    X2AP_numberOfCellSpecificAntennaPorts_one = 0,
    X2AP_numberOfCellSpecificAntennaPorts_two = 1,
    X2AP_four = 2
} X2AP__enum1;

typedef struct X2AP_RelativeNarrowbandTxPower {
    unsigned char   bit_mask;
#       define      X2AP_RelativeNarrowbandTxPower_iE_Extensions_present 0x80
    X2AP__bit1      rNTP_PerPRB;
    X2AP_RNTP_Threshold rNTP_Threshold;
    X2AP__enum1     numberOfCellSpecificAntennaPorts;
    int             p_B;
    int             pDCCH_InterferenceImpact;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                      * X2AP_RelativeNarrowbandTxPower_iE_Extensions_present if
                      * present */
} X2AP_RelativeNarrowbandTxPower;

typedef struct X2AP_CellInformation_Item {
    unsigned char   bit_mask;
#       define      X2AP_ul_InterferenceOverloadIndication_present 0x80
#       define      X2AP_ul_HighInterferenceIndicationInfo_present 0x40
#       define      X2AP_relativeNarrowbandTxPower_present 0x20
#       define      X2AP_CellInformation_Item_iE_Extensions_present 0x10
    X2AP_ECGI       cell_ID;
    struct X2AP_UL_InterferenceOverloadIndication *ul_InterferenceOverloadIndication;                                   /* optional; set in bit_mask
                            * X2AP_ul_InterferenceOverloadIndication_present if
                            * present */
    struct X2AP_UL_HighInterferenceIndicationInfo *ul_HighInterferenceIndicationInfo;                                   /* optional; set in bit_mask
                            * X2AP_ul_HighInterferenceIndicationInfo_present if
                            * present */
    X2AP_RelativeNarrowbandTxPower relativeNarrowbandTxPower;  /* optional; set
                                   * in bit_mask
                                   * X2AP_relativeNarrowbandTxPower_present if
                                   * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                           * X2AP_CellInformation_Item_iE_Extensions_present if
                           * present */
} X2AP_CellInformation_Item;

/* ************************************************************** */
/**/
/* ENB CONFIGURATION UPDATE */
/**/
/* ************************************************************** */
typedef struct X2AP_ENBConfigurationUpdate {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ENBConfigurationUpdate;

typedef int             X2AP_PCI;

/* T */
typedef struct X2AP_TAC {
    unsigned short  length;
    unsigned char   value[2];
} X2AP_TAC;

/* E */
typedef unsigned short  X2AP_EARFCN;

typedef enum X2AP_Transmission_Bandwidth {
    X2AP_bw6 = 0,
    X2AP_bw15 = 1,
    X2AP_bw25 = 2,
    X2AP_bw50 = 3,
    X2AP_bw75 = 4,
    X2AP_bw100 = 5
} X2AP_Transmission_Bandwidth;

typedef struct X2AP_FDD_Info {
    unsigned char   bit_mask;
#       define      X2AP_FDD_Info_iE_Extensions_present 0x80
    X2AP_EARFCN     uL_EARFCN;
    X2AP_EARFCN     dL_EARFCN;
    X2AP_Transmission_Bandwidth uL_Transmission_Bandwidth;
    X2AP_Transmission_Bandwidth dL_Transmission_Bandwidth;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_FDD_Info_iE_Extensions_present if
                                   * present */
} X2AP_FDD_Info;

typedef enum X2AP_SubframeAssignment {
    X2AP_sa0 = 0,
    X2AP_sa1 = 1,
    X2AP_sa2 = 2,
    X2AP_sa3 = 3,
    X2AP_sa4 = 4,
    X2AP_sa5 = 5,
    X2AP_sa6 = 6
} X2AP_SubframeAssignment;

typedef enum X2AP_SpecialSubframePatterns {
    X2AP_SpecialSubframePatterns_ssp0 = 0,
    X2AP_SpecialSubframePatterns_ssp1 = 1,
    X2AP_SpecialSubframePatterns_ssp2 = 2,
    X2AP_SpecialSubframePatterns_ssp3 = 3,
    X2AP_SpecialSubframePatterns_ssp4 = 4,
    X2AP_SpecialSubframePatterns_ssp5 = 5,
    X2AP_SpecialSubframePatterns_ssp6 = 6,
    X2AP_SpecialSubframePatterns_ssp7 = 7,
    X2AP_SpecialSubframePatterns_ssp8 = 8
} X2AP_SpecialSubframePatterns;

typedef enum X2AP_CyclicPrefixDL {
    X2AP_CyclicPrefixDL_normal = 0,
    X2AP_CyclicPrefixDL_extended = 1
} X2AP_CyclicPrefixDL;

typedef enum X2AP_CyclicPrefixUL {
    X2AP_CyclicPrefixUL_normal = 0,
    X2AP_CyclicPrefixUL_extended = 1
} X2AP_CyclicPrefixUL;

typedef struct X2AP_SpecialSubframe_Info {
    unsigned char   bit_mask;
#       define      X2AP_SpecialSubframe_Info_iE_Extensions_present 0x80
    X2AP_SpecialSubframePatterns specialSubframePatterns;
    X2AP_CyclicPrefixDL cyclicPrefixDL;
    X2AP_CyclicPrefixUL cyclicPrefixUL;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                           * X2AP_SpecialSubframe_Info_iE_Extensions_present if
                           * present */
} X2AP_SpecialSubframe_Info;

typedef struct X2AP_TDD_Info {
    unsigned char   bit_mask;
#       define      X2AP_TDD_Info_iE_Extensions_present 0x80
    X2AP_EARFCN     eARFCN;
    X2AP_Transmission_Bandwidth transmission_Bandwidth;
    X2AP_SubframeAssignment subframeAssignment;
    X2AP_SpecialSubframe_Info specialSubframe_Info;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_TDD_Info_iE_Extensions_present if
                                   * present */
} X2AP_TDD_Info;

typedef struct X2AP_EUTRA_Mode_Info {
    unsigned short  choice;
#       define      X2AP_fDD_chosen 1
#       define      X2AP_tDD_chosen 2
    union {
        X2AP_FDD_Info   fDD;  /* to choose, set choice to X2AP_fDD_chosen */
        X2AP_TDD_Info   tDD;  /* to choose, set choice to X2AP_tDD_chosen */
    } u;
} X2AP_EUTRA_Mode_Info;

typedef struct X2AP_ServedCell_Information {
    unsigned char   bit_mask;
#       define      X2AP_ServedCell_Information_iE_Extensions_present 0x80
    X2AP_PCI        pCI;
    X2AP_ECGI       cellId;
    X2AP_TAC        tAC;
    struct X2AP_BroadcastPLMNs_Item *broadcastPLMNs;
    X2AP_EUTRA_Mode_Info eUTRA_Mode_Info;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * X2AP_ServedCell_Information_iE_Extensions_present if
                         * present */
} X2AP_ServedCell_Information;

typedef struct X2AP_ServedCellsToModify_Item {
    unsigned char   bit_mask;
#       define      X2AP_ServedCellsToModify_Item_neighbour_Info_present 0x80
#       define      X2AP_ServedCellsToModify_Item_iE_Extensions_present 0x40
    X2AP_ECGI       old_ecgi;
    X2AP_ServedCell_Information servedCellInfo;
    struct X2AP_Neighbour_Information *neighbour_Info;  /* optional; set in
                                   * bit_mask
                      * X2AP_ServedCellsToModify_Item_neighbour_Info_present if
                      * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                       * X2AP_ServedCellsToModify_Item_iE_Extensions_present if
                       * present */
} X2AP_ServedCellsToModify_Item;

typedef struct X2AP_ServedCellsToModify {
    struct X2AP_ServedCellsToModify *next;
    X2AP_ServedCellsToModify_Item value;
} *X2AP_ServedCellsToModify;

typedef struct X2AP_Old_ECGIs {
    struct X2AP_Old_ECGIs *next;
    X2AP_ECGI       value;
} *X2AP_Old_ECGIs;

/* ************************************************************** */
/**/
/* ENB CONFIGURATION UPDATE ACKNOWLEDGE */
/**/
/* ************************************************************** */
typedef struct X2AP_ENBConfigurationUpdateAcknowledge {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ENBConfigurationUpdateAcknowledge;

/* ************************************************************** */
/**/
/* ENB CONFIGURATION UPDATE FAIURE */
/**/
/* ************************************************************** */
typedef struct X2AP_ENBConfigurationUpdateFailure {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ENBConfigurationUpdateFailure;

/* ************************************************************** */
/**/
/* Resource Status Request */
/**/
/* ************************************************************** */
typedef struct X2AP_ResourceStatusRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ResourceStatusRequest;

typedef struct X2AP_CellToReport_List {
    struct X2AP_CellToReport_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_CellToReport_List;

typedef struct X2AP_CellToReport_Item {
    unsigned char   bit_mask;
#       define      X2AP_CellToReport_Item_iE_Extensions_present 0x80
    X2AP_ECGI       cell_ID;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2AP_CellToReport_Item_iE_Extensions_present if
                              * present */
} X2AP_CellToReport_Item;

typedef enum X2AP_ReportingPeriodicity {
    X2AP_one_thousand_ms = 0,
    X2AP_two_thousand_ms = 1,
    X2AP_five_thousand_ms = 2,
    X2AP_ten_thousand_ms = 3
} X2AP_ReportingPeriodicity;

typedef enum X2AP_PartialSuccessIndicator {
    X2AP_partial_success_allowed = 0
} X2AP_PartialSuccessIndicator;

/* ************************************************************** */
/**/
/* Resource Status Response */
/**/
/* ************************************************************** */
typedef struct X2AP_ResourceStatusResponse {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ResourceStatusResponse;

typedef struct X2AP_MeasurementInitiationResult_List {
    struct X2AP_MeasurementInitiationResult_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_MeasurementInitiationResult_List;

typedef struct X2AP_MeasurementInitiationResult_Item {
    unsigned char   bit_mask;
#       define      X2AP_measurementFailureCause_List_present 0x80
#       define      X2AP_MeasurementInitiationResult_Item_iE_Extensions_present 0x40
    X2AP_ECGI       cell_ID;
    struct X2AP_MeasurementFailureCause_List *measurementFailureCause_List;  
                                        /* optional; set in bit_mask
                                 * X2AP_measurementFailureCause_List_present if
                                 * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
               * X2AP_MeasurementInitiationResult_Item_iE_Extensions_present if
               * present */
} X2AP_MeasurementInitiationResult_Item;

typedef struct X2AP_MeasurementFailureCause_List {
    struct X2AP_MeasurementFailureCause_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_MeasurementFailureCause_List;

typedef struct X2AP_ReportCharacteristics {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_ReportCharacteristics;

typedef enum X2AP_CauseRadioNetwork {
    X2AP_handover_desirable_for_radio_reasons = 0,
    X2AP_time_critical_handover = 1,
    X2AP_resource_optimisation_handover = 2,
    X2AP_reduce_load_in_serving_cell = 3,
    X2AP_partial_handover = 4,
    X2AP_unknown_new_eNB_UE_X2AP_ID = 5,
    X2AP_unknown_old_eNB_UE_X2AP_ID = 6,
    X2AP_unknown_pair_of_UE_X2AP_ID = 7,
    X2AP_ho_target_not_allowed = 8,
    X2AP_tx2relocoverall_expiry = 9,
    X2AP_trelocprep_expiry = 10,
    X2AP_cell_not_available = 11,
    X2AP_no_radio_resources_available_in_target_cell = 12,
    X2AP_invalid_MME_GroupID = 13,
    X2AP_unknown_MME_Code = 14,
    X2AP_encryption_and_or_integrity_protection_algorithms_not_supported = 15,
    X2AP_reportCharacteristicsEmpty = 16,
    X2AP_noReportPeriodicity = 17,
    X2AP_existingMeasurementID = 18,
    X2AP_unknown_eNB_Measurement_ID = 19,
    X2AP_measurement_temporarily_not_available = 20,
    X2AP_CauseRadioNetwork_unspecified = 21,
    X2AP_load_balancing = 22,
    X2AP_handover_optimisation = 23,
    X2AP_value_out_of_allowed_range = 24,
    X2AP_multiple_E_RAB_ID_instances = 25,
    X2AP_switch_off_ongoing = 26,
    X2AP_not_supported_QCI_value = 27,
    X2AP_measurement_not_supported_for_the_object = 28,
    X2AP_tDCoverall_expiry = 29,
    X2AP_tDCprep_expiry = 30,
    X2AP_action_desirable_for_radio_reasons = 31,
    X2AP_reduce_load = 32,
    X2AP_resource_optimisation = 33,
    X2AP_time_critical_action = 34,
    X2AP_target_not_allowed = 35,
    X2AP_no_radio_resources_available = 36,
    X2AP_invalid_QoS_combination = 37,
    X2AP_encryption_algorithms_not_aupported = 38,
    X2AP_procedure_cancelled = 39,
    X2AP_rRM_purpose = 40,
    X2AP_improve_user_bit_rate = 41,
    X2AP_user_inactivity = 42,
    X2AP_radio_connection_with_UE_lost = 43,
    X2AP_failure_in_the_radio_interface_procedure = 44,
    X2AP_bearer_option_not_supported = 45
} X2AP_CauseRadioNetwork;

typedef enum X2AP_CauseTransport {
    X2AP_transport_resource_unavailable = 0,
    X2AP_CauseTransport_unspecified = 1
} X2AP_CauseTransport;

typedef enum X2AP_CauseProtocol {
    X2AP_transfer_syntax_error = 0,
    X2AP_abstract_syntax_error_reject = 1,
    X2AP_abstract_syntax_error_ignore_and_notify = 2,
    X2AP_message_not_compatible_with_receiver_state = 3,
    X2AP_semantic_error = 4,
    X2AP_CauseProtocol_unspecified = 5,
    X2AP_abstract_syntax_error_falsely_constructed_message = 6
} X2AP_CauseProtocol;

typedef enum X2AP_CauseMisc {
    X2AP_control_processing_overload = 0,
    X2AP_hardware_failure = 1,
    X2AP_om_intervention = 2,
    X2AP_not_enough_user_plane_processing_resources = 3,
    X2AP_CauseMisc_unspecified = 4
} X2AP_CauseMisc;

typedef struct X2AP_Cause {
    unsigned short  choice;
#       define      X2AP_radioNetwork_chosen 1
#       define      X2AP_transport_chosen 2
#       define      X2AP_protocol_chosen 3
#       define      X2AP_misc_chosen 4
    union {
        X2AP_CauseRadioNetwork radioNetwork;  /* to choose, set choice to
                                               * X2AP_radioNetwork_chosen */
        X2AP_CauseTransport transport;  /* to choose, set choice to
                                         * X2AP_transport_chosen */
        X2AP_CauseProtocol protocol;  /* to choose, set choice to
                                       * X2AP_protocol_chosen */
        X2AP_CauseMisc  misc;  /* to choose, set choice to X2AP_misc_chosen */
    } u;
} X2AP_Cause;

typedef struct X2AP_MeasurementFailureCause_Item {
    unsigned char   bit_mask;
#       define      X2AP_MeasurementFailureCause_Item_iE_Extensions_present 0x80
    X2AP_ReportCharacteristics measurementFailedReportCharacteristics;
    X2AP_Cause      cause;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                   * X2AP_MeasurementFailureCause_Item_iE_Extensions_present if
                   * present */
} X2AP_MeasurementFailureCause_Item;

/* ************************************************************** */
/**/
/* Resource Status Failure */
/**/
/* ************************************************************** */
typedef struct X2AP_ResourceStatusFailure {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ResourceStatusFailure;

typedef struct X2AP_CompleteFailureCauseInformation_List {
    struct X2AP_CompleteFailureCauseInformation_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_CompleteFailureCauseInformation_List;

typedef struct X2AP_CompleteFailureCauseInformation_Item {
    unsigned char   bit_mask;
#       define      X2AP_CompleteFailureCauseInformation_Item_iE_Extensions_present 0x80
    X2AP_ECGI       cell_ID;
    struct X2AP_MeasurementFailureCause_List *measurementFailureCause_List;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
           * X2AP_CompleteFailureCauseInformation_Item_iE_Extensions_present if
           * present */
} X2AP_CompleteFailureCauseInformation_Item;

/* ************************************************************** */
/**/
/* Resource Status Update */
/**/
/* ************************************************************** */
typedef struct X2AP_ResourceStatusUpdate {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_ResourceStatusUpdate;

typedef struct X2AP_CellMeasurementResult_List {
    struct X2AP_CellMeasurementResult_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_CellMeasurementResult_List;

typedef enum X2AP_LoadIndicator {
    X2AP_lowLoad = 0,
    X2AP_mediumLoad = 1,
    X2AP_highLoad = 2,
    X2AP_overLoad = 3
} X2AP_LoadIndicator;

typedef struct X2AP_HWLoadIndicator {
    unsigned char   bit_mask;
#       define      X2AP_HWLoadIndicator_iE_Extensions_present 0x80
    X2AP_LoadIndicator dLHWLoadIndicator;
    X2AP_LoadIndicator uLHWLoadIndicator;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_HWLoadIndicator_iE_Extensions_present
                                   * if present */
} X2AP_HWLoadIndicator;

/* S */
typedef struct X2AP_S1TNLLoadIndicator {
    unsigned char   bit_mask;
#       define      X2AP_S1TNLLoadIndicator_iE_Extensions_present 0x80
    X2AP_LoadIndicator dLS1TNLLoadIndicator;
    X2AP_LoadIndicator uLS1TNLLoadIndicator;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * X2AP_S1TNLLoadIndicator_iE_Extensions_present if
                             * present */
} X2AP_S1TNLLoadIndicator;

typedef unsigned short  X2AP_DL_GBR_PRB_usage;

typedef unsigned short  X2AP_UL_GBR_PRB_usage;

typedef unsigned short  X2AP_DL_non_GBR_PRB_usage;

typedef unsigned short  X2AP_UL_non_GBR_PRB_usage;

typedef unsigned short  X2AP_DL_Total_PRB_usage;

typedef unsigned short  X2AP_UL_Total_PRB_usage;

typedef struct X2AP_RadioResourceStatus {
    unsigned char   bit_mask;
#       define      X2AP_RadioResourceStatus_iE_Extensions_present 0x80
    X2AP_DL_GBR_PRB_usage dL_GBR_PRB_usage;
    X2AP_UL_GBR_PRB_usage uL_GBR_PRB_usage;
    X2AP_DL_non_GBR_PRB_usage dL_non_GBR_PRB_usage;
    X2AP_UL_non_GBR_PRB_usage uL_non_GBR_PRB_usage;
    X2AP_DL_Total_PRB_usage dL_Total_PRB_usage;
    X2AP_UL_Total_PRB_usage uL_Total_PRB_usage;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * X2AP_RadioResourceStatus_iE_Extensions_present if
                            * present */
} X2AP_RadioResourceStatus;

typedef struct X2AP_CellMeasurementResult_Item {
    unsigned char   bit_mask;
#       define      X2AP_hWLoadIndicator_present 0x80
#       define      X2AP_s1TNLLoadIndicator_present 0x40
#       define      X2AP_radioResourceStatus_present 0x20
#       define      X2AP_CellMeasurementResult_Item_iE_Extensions_present 0x10
    X2AP_ECGI       cell_ID;
    X2AP_HWLoadIndicator hWLoadIndicator;  /* optional; set in bit_mask
                                            * X2AP_hWLoadIndicator_present if
                                            * present */
    X2AP_S1TNLLoadIndicator s1TNLLoadIndicator;  /* optional; set in bit_mask
                                           * X2AP_s1TNLLoadIndicator_present if
                                           * present */
    X2AP_RadioResourceStatus radioResourceStatus;  /* optional; set in bit_mask
                                          * X2AP_radioResourceStatus_present if
                                          * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                     * X2AP_CellMeasurementResult_Item_iE_Extensions_present if
                     * present */
} X2AP_CellMeasurementResult_Item;

typedef struct X2AP_PrivateIE_ID {
    unsigned short  choice;
#       define      X2AP_local_chosen 1
#       define      X2AP_global_chosen 2
    union {
        unsigned short  local;  /* to choose, set choice to X2AP_local_chosen */
        X2AP__ObjectID  global;  /* to choose, set choice to
                                  * X2AP_global_chosen */
    } u;
} X2AP_PrivateIE_ID;

typedef struct X2AP_PrivateIE_Field {
    X2AP_PrivateIE_ID id;
    X2AP_Criticality criticality;
    OpenType        value;
} X2AP_PrivateIE_Field;

typedef struct X2AP_PrivateIE_Container {
    struct X2AP_PrivateIE_Container *next;
    X2AP_PrivateIE_Field value;
} *X2AP_PrivateIE_Container;

/* ************************************************************** */
/**/
/* PRIVATE MESSAGE */
/**/
/* ************************************************************** */
typedef struct X2AP_PrivateMessage {
    struct X2AP_PrivateIE_Container *privateIEs;
} X2AP_PrivateMessage;

/* ************************************************************** */
/**/
/* MOBILITY CHANGE REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_MobilityChangeRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_MobilityChangeRequest;

/* ************************************************************** */
/**/
/* MOBILITY CHANGE ACKNOWLEDGE */
/**/
/* ************************************************************** */
typedef struct X2AP_MobilityChangeAcknowledge {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_MobilityChangeAcknowledge;

/* ************************************************************** */
/**/
/* MOBILITY CHANGE FAILURE */
/**/
/* ************************************************************** */
typedef struct X2AP_MobilityChangeFailure {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_MobilityChangeFailure;

/* ************************************************************** */
/**/
/* Radio Link Failure Indication */
/**/
/* ************************************************************** */
typedef struct X2AP_RLFIndication {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_RLFIndication;

/* ************************************************************** */
/**/
/* Cell Activation Request */
/**/
/* ************************************************************** */
typedef struct X2AP_CellActivationRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_CellActivationRequest;

typedef struct X2AP_ServedCellsToActivate_Item {
    unsigned char   bit_mask;
#       define      X2AP_ServedCellsToActivate_Item_iE_Extensions_present 0x80
    X2AP_ECGI       ecgi;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                     * X2AP_ServedCellsToActivate_Item_iE_Extensions_present if
                     * present */
} X2AP_ServedCellsToActivate_Item;

typedef struct X2AP_ServedCellsToActivate {
    struct X2AP_ServedCellsToActivate *next;
    X2AP_ServedCellsToActivate_Item value;
} *X2AP_ServedCellsToActivate;

/* ************************************************************** */
/**/
/* Cell Activation Response */
/**/
/* ************************************************************** */
typedef struct X2AP_CellActivationResponse {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_CellActivationResponse;

typedef struct X2AP_ActivatedCellList_Item {
    unsigned char   bit_mask;
#       define      X2AP_ActivatedCellList_Item_iE_Extensions_present 0x80
    X2AP_ECGI       ecgi;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * X2AP_ActivatedCellList_Item_iE_Extensions_present if
                         * present */
} X2AP_ActivatedCellList_Item;

typedef struct X2AP_ActivatedCellList {
    struct X2AP_ActivatedCellList *next;
    X2AP_ActivatedCellList_Item value;
} *X2AP_ActivatedCellList;

/*************************************************************** */
/**/
/* CELL ACTIVATION FAILURE */
/**/
/* ************************************************************** */
typedef struct X2AP_CellActivationFailure {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_CellActivationFailure;

/* ************************************************************** */
/**/
/* X2 RELEASE */
/**/
/* ************************************************************** */
typedef struct X2AP_X2Release {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2Release;

/* ************************************************************** */
/**/
/* X2AP Message Transfer */
/**/
/* ************************************************************** */
typedef struct X2AP_X2APMessageTransfer {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2APMessageTransfer;

typedef struct X2AP_ENB_ID {
    unsigned short  choice;
#       define      X2AP_macro_eNB_ID_chosen 1
#       define      X2AP_home_eNB_ID_chosen 2
    union {
        X2AP__bit1      macro_eNB_ID;  /* to choose, set choice to
                                        * X2AP_macro_eNB_ID_chosen */
        X2AP__bit1      home_eNB_ID;  /* to choose, set choice to
                                       * X2AP_home_eNB_ID_chosen */
    } u;
} X2AP_ENB_ID;

typedef struct X2AP_GlobalENB_ID {
    unsigned char   bit_mask;
#       define      X2AP_GlobalENB_ID_iE_Extensions_present 0x80
    X2AP_PLMN_Identity pLMN_Identity;
    X2AP_ENB_ID     eNB_ID;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_GlobalENB_ID_iE_Extensions_present if
                                   * present */
} X2AP_GlobalENB_ID;

typedef struct X2AP_RNL_Header {
    unsigned char   bit_mask;
#       define      X2AP_target_GlobalENB_ID_present 0x80
#       define      X2AP_RNL_Header_iE_Extensions_present 0x40
    X2AP_GlobalENB_ID source_GlobalENB_ID;
    X2AP_GlobalENB_ID target_GlobalENB_ID;  /* optional; set in bit_mask
                                             * X2AP_target_GlobalENB_ID_present
                                             * if present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_RNL_Header_iE_Extensions_present if
                                   * present */
} X2AP_RNL_Header;

typedef struct X2AP_X2AP_Message {
    unsigned int    length;
    unsigned char   *value;
} X2AP_X2AP_Message;

/* ************************************************************** */
/**/
/* SENB ADDITION REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBAdditionRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBAdditionRequest;

typedef struct X2AP_E_RABs_ToBeAdded_List {
    struct X2AP_E_RABs_ToBeAdded_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeAdded_List;

typedef struct X2AP_E_RABs_ToBeAdded_Item_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeAdded_Item_SCG_Bearer_dL_Forwarding_present 0x80
#       define      X2AP_E_RABs_ToBeAdded_Item_SCG_Bearer_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters;
    X2AP_DL_Forwarding dL_Forwarding;  /* optional; set in bit_mask
               * X2AP_E_RABs_ToBeAdded_Item_SCG_Bearer_dL_Forwarding_present if
               * present */
    X2AP_GTPtunnelEndpoint s1_UL_GTPtunnelEndpoint;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
               * X2AP_E_RABs_ToBeAdded_Item_SCG_Bearer_iE_Extensions_present if
               * present */
} X2AP_E_RABs_ToBeAdded_Item_SCG_Bearer;

typedef struct X2AP_E_RABs_ToBeAdded_Item_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeAdded_Item_Split_Bearer_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters;
    X2AP_GTPtunnelEndpoint meNB_GTPtunnelEndpoint;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
             * X2AP_E_RABs_ToBeAdded_Item_Split_Bearer_iE_Extensions_present if
             * present */
} X2AP_E_RABs_ToBeAdded_Item_Split_Bearer;

typedef struct X2AP_E_RABs_ToBeAdded_Item {
    unsigned short  choice;
#       define      X2AP_E_RABs_ToBeAdded_Item_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_ToBeAdded_Item_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_ToBeAdded_Item_SCG_Bearer sCG_Bearer;  /* to choose, set
                                   * choice to
                              * X2AP_E_RABs_ToBeAdded_Item_sCG_Bearer_chosen */
        X2AP_E_RABs_ToBeAdded_Item_Split_Bearer split_Bearer;  /* to choose, set
                                   * choice to
                            * X2AP_E_RABs_ToBeAdded_Item_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_ToBeAdded_Item;

/* ************************************************************** */
/**/
/* SENB ADDITION REQUEST ACKNOWLEDGE */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBAdditionRequestAcknowledge {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBAdditionRequestAcknowledge;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_List {
    struct X2AP_E_RABs_Admitted_ToBeAdded_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_Admitted_ToBeAdded_List;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer_dL_Forwarding_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer_uL_Forwarding_GTPtunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint s1_DL_GTPtunnelEndpoint;
    X2AP_GTPtunnelEndpoint dL_Forwarding_GTPtunnelEndpoint;  /* optional; set in
                                   * bit_mask
              * X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer_dL_Forwarding_GTPtunnelEndpoint_present if
              * present */
    X2AP_GTPtunnelEndpoint uL_Forwarding_GTPtunnelEndpoint;  /* optional; set in
                                   * bit_mask
              * X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer_uL_Forwarding_GTPtunnelEndpoint_present if
              * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
      * X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer_iE_Extensions_present if
      * present */
} X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_Bearer_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint seNB_GTPtunnelEndpoint;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
    * X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_Bearer_iE_Extensions_present if
    * present */
} X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_Item {
    unsigned short  choice;
#       define      X2AP_E_RABs_Admitted_ToBeAdded_Item_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_Admitted_ToBeAdded_Item_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_Bearer sCG_Bearer;  /* to
                                   * choose, set choice to
                     * X2AP_E_RABs_Admitted_ToBeAdded_Item_sCG_Bearer_chosen */
        X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_Bearer split_Bearer;  /* to
                                   * choose, set choice to
                   * X2AP_E_RABs_Admitted_ToBeAdded_Item_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_Admitted_ToBeAdded_Item;

/* ************************************************************** */
/**/
/* SENB ADDITION REQUEST REJECT */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBAdditionRequestReject {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBAdditionRequestReject;

/* ************************************************************** */
/**/
/* SENB RECONFIGURATION COMPLETE */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBReconfigurationComplete {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBReconfigurationComplete;

typedef struct X2AP_MeNBtoSeNBContainer {
    unsigned int    length;
    unsigned char   *value;
} X2AP_MeNBtoSeNBContainer;

typedef struct X2AP_ResponseInformationSeNBReconfComp_SuccessItem {
    unsigned char   bit_mask;
#       define      X2AP_ResponseInformationSeNBReconfComp_SuccessItem_meNBtoSeNBContainer_present 0x80
#       define      X2AP_ResponseInformationSeNBReconfComp_SuccessItem_iE_Extensions_present 0x40
    X2AP_MeNBtoSeNBContainer meNBtoSeNBContainer;  /* optional; set in bit_mask
      * X2AP_ResponseInformationSeNBReconfComp_SuccessItem_meNBtoSeNBContainer_present if
      * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
  * X2AP_ResponseInformationSeNBReconfComp_SuccessItem_iE_Extensions_present if
  * present */
} X2AP_ResponseInformationSeNBReconfComp_SuccessItem;

typedef struct X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItem {
    unsigned char   bit_mask;
#       define      X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItem_meNBtoSeNBContainer_present 0x80
#       define      X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItem_iE_Extensions_present 0x40
    X2AP_Cause      cause;
    X2AP_MeNBtoSeNBContainer meNBtoSeNBContainer;  /* optional; set in bit_mask
           * X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItem_meNBtoSeNBContainer_present if
           * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
     * X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItem_iE_Extensions_present if
     * present */
} X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItem;

typedef struct X2AP_ResponseInformationSeNBReconfComp {
    unsigned short  choice;
#       define      X2AP_success_chosen 1
#       define      X2AP_reject_by_MeNB_chosen 2
    union {
        X2AP_ResponseInformationSeNBReconfComp_SuccessItem success;  /* to
                                   * choose, set choice to
                                   * X2AP_success_chosen */
        X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItem reject_by_MeNB;                                         /* to choose, set choice to
                                         * X2AP_reject_by_MeNB_chosen */
    } u;
} X2AP_ResponseInformationSeNBReconfComp;

/* ************************************************************** */
/**/
/* SENB MODIFICATION REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBModificationRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBModificationRequest;

typedef struct X2AP_SeNBSecurityKey {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_SeNBSecurityKey;

typedef struct X2AP_UE_ContextInformationSeNBModReq {
    unsigned char   bit_mask;
#       define      X2AP_uE_SecurityCapabilities_present 0x80
#       define      X2AP_seNB_SecurityKey_present 0x40
#       define      X2AP_seNBUEAggregateMaximumBitRate_present 0x20
#       define      X2AP_e_RABs_ToBeAdded_present 0x10
#       define      X2AP_e_RABs_ToBeModified_present 0x08
#       define      X2AP_e_RABs_ToBeReleased_present 0x04
#       define      X2AP_UE_ContextInformationSeNBModReq_iE_Extensions_present 0x02
    X2AP_UESecurityCapabilities uE_SecurityCapabilities;  /* optional; set in
                                   * bit_mask
                                   * X2AP_uE_SecurityCapabilities_present if
                                   * present */
    X2AP_SeNBSecurityKey seNB_SecurityKey;  /* optional; set in bit_mask
                                             * X2AP_seNB_SecurityKey_present if
                                             * present */
    X2AP_UEAggregateMaximumBitRate seNBUEAggregateMaximumBitRate;  /* optional;
                                   * set in bit_mask
                                   * X2AP_seNBUEAggregateMaximumBitRate_present
                                   * if present */
    struct X2AP_E_RABs_ToBeAdded_List_ModReq *e_RABs_ToBeAdded;  /* optional;
                                   * set in bit_mask
                                   * X2AP_e_RABs_ToBeAdded_present if present */
    struct X2AP_E_RABs_ToBeModified_List_ModReq *e_RABs_ToBeModified;  
                                  /* optional; set in bit_mask
                                   * X2AP_e_RABs_ToBeModified_present if
                                   * present */
    struct X2AP_E_RABs_ToBeReleased_List_ModReq *e_RABs_ToBeReleased;  
                                  /* optional; set in bit_mask
                                   * X2AP_e_RABs_ToBeReleased_present if
                                   * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                * X2AP_UE_ContextInformationSeNBModReq_iE_Extensions_present if
                * present */
} X2AP_UE_ContextInformationSeNBModReq;

typedef struct X2AP_E_RABs_ToBeAdded_List_ModReq {
    struct X2AP_E_RABs_ToBeAdded_List_ModReq *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeAdded_List_ModReq;

typedef struct X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_Bearer_dL_Forwarding_present 0x80
#       define      X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_Bearer_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters;
    X2AP_DL_Forwarding dL_Forwarding;  /* optional; set in bit_mask
         * X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_Bearer_dL_Forwarding_present if
         * present */
    X2AP_GTPtunnelEndpoint s1_UL_GTPtunnelEndpoint;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
         * X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_Bearer_iE_Extensions_present if
         * present */
} X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_Bearer;

typedef struct X2AP_E_RABs_ToBeAdded_ModReqItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeAdded_ModReqItem_Split_Bearer_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters;
    X2AP_GTPtunnelEndpoint meNB_GTPtunnelEndpoint;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
       * X2AP_E_RABs_ToBeAdded_ModReqItem_Split_Bearer_iE_Extensions_present if
       * present */
} X2AP_E_RABs_ToBeAdded_ModReqItem_Split_Bearer;

typedef struct X2AP_E_RABs_ToBeAdded_ModReqItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_ToBeAdded_ModReqItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_ToBeAdded_ModReqItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_Bearer sCG_Bearer;  /* to choose,
                                   * set choice to
                        * X2AP_E_RABs_ToBeAdded_ModReqItem_sCG_Bearer_chosen */
        X2AP_E_RABs_ToBeAdded_ModReqItem_Split_Bearer split_Bearer;  /* to
                                   * choose, set choice to
                      * X2AP_E_RABs_ToBeAdded_ModReqItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_ToBeAdded_ModReqItem;

typedef struct X2AP_E_RABs_ToBeModified_List_ModReq {
    struct X2AP_E_RABs_ToBeModified_List_ModReq *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeModified_List_ModReq;

typedef struct X2AP_E_RABs_ToBeModified_ModReqItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeModified_ModReqItem_SCG_Bearer_e_RAB_Level_QoS_Parameters_present 0x80
#       define      X2AP_s1_UL_GTPtunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_ToBeModified_ModReqItem_SCG_Bearer_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters;  /* optional;
                                   * set in bit_mask
         * X2AP_E_RABs_ToBeModified_ModReqItem_SCG_Bearer_e_RAB_Level_QoS_Parameters_present if
         * present */
    X2AP_GTPtunnelEndpoint s1_UL_GTPtunnelEndpoint;  /* optional; set in
                                   * bit_mask
                                   * X2AP_s1_UL_GTPtunnelEndpoint_present if
                                   * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
      * X2AP_E_RABs_ToBeModified_ModReqItem_SCG_Bearer_iE_Extensions_present if
      * present */
} X2AP_E_RABs_ToBeModified_ModReqItem_SCG_Bearer;

typedef struct X2AP_E_RABs_ToBeModified_ModReqItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeModified_ModReqItem_Split_Bearer_e_RAB_Level_QoS_Parameters_present 0x80
#       define      X2AP_meNB_GTPtunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_ToBeModified_ModReqItem_Split_Bearer_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_E_RAB_Level_QoS_Parameters e_RAB_Level_QoS_Parameters;  /* optional;
                                   * set in bit_mask
           * X2AP_E_RABs_ToBeModified_ModReqItem_Split_Bearer_e_RAB_Level_QoS_Parameters_present if
           * present */
    X2AP_GTPtunnelEndpoint meNB_GTPtunnelEndpoint;  /* optional; set in bit_mask
                                       * X2AP_meNB_GTPtunnelEndpoint_present if
                                       * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
    * X2AP_E_RABs_ToBeModified_ModReqItem_Split_Bearer_iE_Extensions_present if
    * present */
} X2AP_E_RABs_ToBeModified_ModReqItem_Split_Bearer;

typedef struct X2AP_E_RABs_ToBeModified_ModReqItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_ToBeModified_ModReqItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_ToBeModified_ModReqItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_ToBeModified_ModReqItem_SCG_Bearer sCG_Bearer;  /* to
                                   * choose, set choice to
                     * X2AP_E_RABs_ToBeModified_ModReqItem_sCG_Bearer_chosen */
        X2AP_E_RABs_ToBeModified_ModReqItem_Split_Bearer split_Bearer;  /* to
                                   * choose, set choice to
                   * X2AP_E_RABs_ToBeModified_ModReqItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_ToBeModified_ModReqItem;

typedef struct X2AP_E_RABs_ToBeReleased_List_ModReq {
    struct X2AP_E_RABs_ToBeReleased_List_ModReq *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeReleased_List_ModReq;

typedef struct X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer_dL_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer_uL_GTPtunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint dL_GTPtunnelEndpoint;  /* optional; set in bit_mask
   * X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer_dL_GTPtunnelEndpoint_present if
   * present */
    X2AP_GTPtunnelEndpoint uL_GTPtunnelEndpoint;  /* optional; set in bit_mask
   * X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer_uL_GTPtunnelEndpoint_present if
   * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
      * X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer_iE_Extensions_present if
      * present */
} X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer;

typedef struct X2AP_E_RABs_ToBeReleased_ModReqItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeReleased_ModReqItem_Split_Bearer_dL_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_ToBeReleased_ModReqItem_Split_Bearer_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint dL_GTPtunnelEndpoint;  /* optional; set in bit_mask
     * X2AP_E_RABs_ToBeReleased_ModReqItem_Split_Bearer_dL_GTPtunnelEndpoint_present if
     * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
    * X2AP_E_RABs_ToBeReleased_ModReqItem_Split_Bearer_iE_Extensions_present if
    * present */
} X2AP_E_RABs_ToBeReleased_ModReqItem_Split_Bearer;

typedef struct X2AP_E_RABs_ToBeReleased_ModReqItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_ToBeReleased_ModReqItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_ToBeReleased_ModReqItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_Bearer sCG_Bearer;  /* to
                                   * choose, set choice to
                     * X2AP_E_RABs_ToBeReleased_ModReqItem_sCG_Bearer_chosen */
        X2AP_E_RABs_ToBeReleased_ModReqItem_Split_Bearer split_Bearer;  /* to
                                   * choose, set choice to
                   * X2AP_E_RABs_ToBeReleased_ModReqItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_ToBeReleased_ModReqItem;

/* ************************************************************** */
/**/
/* SENB MODIFICATION REQUEST ACKNOWLEDGE */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBModificationRequestAcknowledge {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBModificationRequestAcknowledge;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_ModAckList {
    struct X2AP_E_RABs_Admitted_ToBeAdded_ModAckList *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_Admitted_ToBeAdded_ModAckList;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer_dL_Forwarding_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer_uL_Forwarding_GTPtunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint s1_DL_GTPtunnelEndpoint;
    X2AP_GTPtunnelEndpoint dL_Forwarding_GTPtunnelEndpoint;  /* optional; set in
                                   * bit_mask
                    * X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer_dL_Forwarding_GTPtunnelEndpoint_present if
                    * present */
    X2AP_GTPtunnelEndpoint uL_Forwarding_GTPtunnelEndpoint;  /* optional; set in
                                   * bit_mask
                    * X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer_uL_Forwarding_GTPtunnelEndpoint_present if
                    * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
  * X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer_iE_Extensions_present if
  * present */
} X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_Bearer_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint seNB_GTPtunnelEndpoint;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
    * X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_Bearer_iE_Extensions_present if
    * present */
} X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_Bearer sCG_Bearer;  
                                        /* to choose, set choice to
               * X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_sCG_Bearer_chosen */
        X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_Bearer split_Bearer;  
                                        /* to choose, set choice to
             * X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem;

typedef struct X2AP_E_RABs_Admitted_ToBeModified_ModAckList {
    struct X2AP_E_RABs_Admitted_ToBeModified_ModAckList *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_Admitted_ToBeModified_ModAckList;

typedef struct X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_s1_DL_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_Bearer_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint s1_DL_GTPtunnelEndpoint;  /* optional; set in
                                   * bit_mask
                                   * X2AP_s1_DL_GTPtunnelEndpoint_present if
                                   * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
     * X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_Bearer_iE_Extensions_present if
     * present */
} X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_seNB_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_Bearer_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint seNB_GTPtunnelEndpoint;  /* optional; set in bit_mask
                                       * X2AP_seNB_GTPtunnelEndpoint_present if
                                       * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
       * X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_Bearer_iE_Extensions_present if
       * present */
} X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToBeModified_ModAckItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_Bearer sCG_Bearer;  
                                        /* to choose, set choice to
            * X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_sCG_Bearer_chosen */
        X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_Bearer split_Bearer;                                         /* to choose, set choice to
          * X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_Admitted_ToBeModified_ModAckItem;

typedef struct X2AP_E_RABs_Admitted_ToBeReleased_ModAckList {
    struct X2AP_E_RABs_Admitted_ToBeReleased_ModAckList *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_Admitted_ToBeReleased_ModAckList;

typedef struct X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_Bearer_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
     * X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_Bearer_iE_Extensions_present if
     * present */
} X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_Bearer_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
       * X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_Bearer_iE_Extensions_present if
       * present */
} X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_Bearer;

typedef struct X2AP_E_RABs_Admitted_ToReleased_ModAckItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_Admitted_ToReleased_ModAckItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_Admitted_ToReleased_ModAckItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_Bearer sCG_Bearer;  
                                        /* to choose, set choice to
              * X2AP_E_RABs_Admitted_ToReleased_ModAckItem_sCG_Bearer_chosen */
        X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_Bearer split_Bearer;                                         /* to choose, set choice to
            * X2AP_E_RABs_Admitted_ToReleased_ModAckItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_Admitted_ToReleased_ModAckItem;

/* ************************************************************** */
/**/
/* SENB MODIFICATION REQUEST REJECT */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBModificationRequestReject {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBModificationRequestReject;

/* ************************************************************** */
/**/
/* SENB MODIFICATION REQUIRED */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBModificationRequired {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBModificationRequired;

typedef struct X2AP_E_RABs_ToBeReleased_ModReqd {
    struct X2AP_E_RABs_ToBeReleased_ModReqd *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeReleased_ModReqd;

typedef struct X2AP_E_RABs_ToBeReleased_ModReqdItem {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeReleased_ModReqdItem_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_Cause      cause;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                * X2AP_E_RABs_ToBeReleased_ModReqdItem_iE_Extensions_present if
                * present */
} X2AP_E_RABs_ToBeReleased_ModReqdItem;

/* ************************************************************** */
/**/
/* SENB MODIFICATION CONFIRM */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBModificationConfirm {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBModificationConfirm;

/* ************************************************************** */
/**/
/* SENB MODIFICATION REFUSE */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBModificationRefuse {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBModificationRefuse;

/* ************************************************************** */
/**/
/* SENB RELEASE REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBReleaseRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBReleaseRequest;

typedef struct X2AP_E_RABs_ToBeReleased_List_RelReq {
    struct X2AP_E_RABs_ToBeReleased_List_RelReq *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeReleased_List_RelReq;

typedef struct X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer_uL_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer_dL_GTPtunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint uL_GTPtunnelEndpoint;  /* optional; set in bit_mask
   * X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer_uL_GTPtunnelEndpoint_present if
   * present */
    X2AP_GTPtunnelEndpoint dL_GTPtunnelEndpoint;  /* optional; set in bit_mask
   * X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer_dL_GTPtunnelEndpoint_present if
   * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
      * X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer_iE_Extensions_present if
      * present */
} X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer;

typedef struct X2AP_E_RABs_ToBeReleased_RelReqItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeReleased_RelReqItem_Split_Bearer_dL_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_ToBeReleased_RelReqItem_Split_Bearer_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint dL_GTPtunnelEndpoint;  /* optional; set in bit_mask
     * X2AP_E_RABs_ToBeReleased_RelReqItem_Split_Bearer_dL_GTPtunnelEndpoint_present if
     * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
    * X2AP_E_RABs_ToBeReleased_RelReqItem_Split_Bearer_iE_Extensions_present if
    * present */
} X2AP_E_RABs_ToBeReleased_RelReqItem_Split_Bearer;

typedef struct X2AP_E_RABs_ToBeReleased_RelReqItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_ToBeReleased_RelReqItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_ToBeReleased_RelReqItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_Bearer sCG_Bearer;  /* to
                                   * choose, set choice to
                     * X2AP_E_RABs_ToBeReleased_RelReqItem_sCG_Bearer_chosen */
        X2AP_E_RABs_ToBeReleased_RelReqItem_Split_Bearer split_Bearer;  /* to
                                   * choose, set choice to
                   * X2AP_E_RABs_ToBeReleased_RelReqItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_ToBeReleased_RelReqItem;

/* ************************************************************** */
/**/
/* SENB RELEASE REQUIRED */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBReleaseRequired {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBReleaseRequired;

/* ************************************************************** */
/**/
/* SENB RELEASE CONFIRM */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBReleaseConfirm {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBReleaseConfirm;

typedef struct X2AP_E_RABs_ToBeReleased_List_RelConf {
    struct X2AP_E_RABs_ToBeReleased_List_RelConf *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_ToBeReleased_List_RelConf;

typedef struct X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer_uL_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer_dL_GTPtunnelEndpoint_present 0x40
#       define      X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer_iE_Extensions_present 0x20
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint uL_GTPtunnelEndpoint;  /* optional; set in bit_mask
    * X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer_uL_GTPtunnelEndpoint_present if
    * present */
    X2AP_GTPtunnelEndpoint dL_GTPtunnelEndpoint;  /* optional; set in bit_mask
    * X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer_dL_GTPtunnelEndpoint_present if
    * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
     * X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer_iE_Extensions_present if
     * present */
} X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer;

typedef struct X2AP_E_RABs_ToBeReleased_RelConfItem_Split_Bearer {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_ToBeReleased_RelConfItem_Split_Bearer_dL_GTPtunnelEndpoint_present 0x80
#       define      X2AP_E_RABs_ToBeReleased_RelConfItem_Split_Bearer_iE_Extensions_present 0x40
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_GTPtunnelEndpoint dL_GTPtunnelEndpoint;  /* optional; set in bit_mask
      * X2AP_E_RABs_ToBeReleased_RelConfItem_Split_Bearer_dL_GTPtunnelEndpoint_present if
      * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
   * X2AP_E_RABs_ToBeReleased_RelConfItem_Split_Bearer_iE_Extensions_present if
   * present */
} X2AP_E_RABs_ToBeReleased_RelConfItem_Split_Bearer;

typedef struct X2AP_E_RABs_ToBeReleased_RelConfItem {
    unsigned short  choice;
#       define      X2AP_E_RABs_ToBeReleased_RelConfItem_sCG_Bearer_chosen 1
#       define      X2AP_E_RABs_ToBeReleased_RelConfItem_split_Bearer_chosen 2
    union {
        X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_Bearer sCG_Bearer;  /* to
                                   * choose, set choice to
                    * X2AP_E_RABs_ToBeReleased_RelConfItem_sCG_Bearer_chosen */
        X2AP_E_RABs_ToBeReleased_RelConfItem_Split_Bearer split_Bearer;  /* to
                                   * choose, set choice to
                  * X2AP_E_RABs_ToBeReleased_RelConfItem_split_Bearer_chosen */
    } u;
} X2AP_E_RABs_ToBeReleased_RelConfItem;

/* ************************************************************** */
/**/
/* SENB COUNTER CHECK REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_SeNBCounterCheckRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_SeNBCounterCheckRequest;

typedef struct X2AP_E_RABs_SubjectToCounterCheck_List {
    struct X2AP_E_RABs_SubjectToCounterCheck_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RABs_SubjectToCounterCheck_List;

typedef struct X2AP_E_RABs_SubjectToCounterCheckItem {
    unsigned char   bit_mask;
#       define      X2AP_E_RABs_SubjectToCounterCheckItem_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    unsigned int    uL_Count;
    unsigned int    dL_Count;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
               * X2AP_E_RABs_SubjectToCounterCheckItem_iE_Extensions_present if
               * present */
} X2AP_E_RABs_SubjectToCounterCheckItem;

/* ************************************************************** */
/**/
/* X2 REMOVAL REQUEST */
/**/
/* ************************************************************** */
typedef struct X2AP_X2RemovalRequest {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2RemovalRequest;

/* ************************************************************** */
/**/
/* X2 REMOVAL RESPONSE */
/**/
/* ************************************************************** */
typedef struct X2AP_X2RemovalResponse {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2RemovalResponse;

/* ************************************************************** */
/**/
/* X2 REMOVAL FAILURE */
/**/
/* ************************************************************** */
typedef struct X2AP_X2RemovalFailure {
    struct X2AP_ProtocolIE_Container *protocolIEs;
} X2AP_X2RemovalFailure;

typedef struct X2AP_ABSInformationFDD {
    unsigned char   bit_mask;
#       define      X2AP_ABSInformationFDD_iE_Extensions_present 0x80
    X2AP__bit1      abs_pattern_info;
    X2AP__enum1     numberOfCellSpecificAntennaPorts;
    X2AP__bit1      measurement_subset;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2AP_ABSInformationFDD_iE_Extensions_present if
                              * present */
} X2AP_ABSInformationFDD;

typedef struct X2AP_ABSInformationTDD {
    unsigned char   bit_mask;
#       define      X2AP_ABSInformationTDD_iE_Extensions_present 0x80
    X2AP__bit1      abs_pattern_info;
    X2AP__enum1     numberOfCellSpecificAntennaPorts;
    X2AP__bit1      measurement_subset;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2AP_ABSInformationTDD_iE_Extensions_present if
                              * present */
} X2AP_ABSInformationTDD;

/* A */
typedef struct X2AP_ABSInformation {
    unsigned short  choice;
#       define      X2AP_ABSInformation_fdd_chosen 1
#       define      X2AP_ABSInformation_tdd_chosen 2
#       define      X2AP_abs_inactive_chosen 3
    union {
        X2AP_ABSInformationFDD fdd;  /* to choose, set choice to
                                      * X2AP_ABSInformation_fdd_chosen */
        X2AP_ABSInformationTDD tdd;  /* to choose, set choice to
                                      * X2AP_ABSInformation_tdd_chosen */
        Nulltype        abs_inactive;  /* to choose, set choice to
                                        * X2AP_abs_inactive_chosen */
    } u;
} X2AP_ABSInformation;

typedef unsigned short  X2AP_DL_ABS_status;

typedef struct X2AP_UsableABSInformationFDD {
    unsigned char   bit_mask;
#       define      X2AP_UsableABSInformationFDD_iE_Extensions_present 0x80
    X2AP__bit1      usable_abs_pattern_info;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                        * X2AP_UsableABSInformationFDD_iE_Extensions_present if
                        * present */
} X2AP_UsableABSInformationFDD;

typedef struct X2AP_UsableABSInformationTDD {
    unsigned char   bit_mask;
#       define      X2AP_UsableABSInformationTDD_iE_Extensions_present 0x80
    X2AP__bit1      usaable_abs_pattern_info;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                        * X2AP_UsableABSInformationTDD_iE_Extensions_present if
                        * present */
} X2AP_UsableABSInformationTDD;

/* This IE is a transparent container and shall be encoded as the RLF-Report-v9e0 field contained in the UEInformationResponse message as defined in TS 36.331 [9] */
typedef struct X2AP_UsableABSInformation {
    unsigned short  choice;
#       define      X2AP_UsableABSInformation_fdd_chosen 1
#       define      X2AP_UsableABSInformation_tdd_chosen 2
    union {
        X2AP_UsableABSInformationFDD fdd;  /* to choose, set choice to
                                      * X2AP_UsableABSInformation_fdd_chosen */
        X2AP_UsableABSInformationTDD tdd;  /* to choose, set choice to
                                      * X2AP_UsableABSInformation_tdd_chosen */
    } u;
} X2AP_UsableABSInformation;

typedef struct X2AP_ABS_Status {
    unsigned char   bit_mask;
#       define      X2AP_ABS_Status_iE_Extensions_present 0x80
    X2AP_DL_ABS_status dL_ABS_status;
    X2AP_UsableABSInformation usableABSInformation;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_ABS_Status_iE_Extensions_present if
                                   * present */
} X2AP_ABS_Status;

typedef enum X2AP_AdditionalSpecialSubframePatterns {
    X2AP_AdditionalSpecialSubframePatterns_ssp0 = 0,
    X2AP_AdditionalSpecialSubframePatterns_ssp1 = 1,
    X2AP_AdditionalSpecialSubframePatterns_ssp2 = 2,
    X2AP_AdditionalSpecialSubframePatterns_ssp3 = 3,
    X2AP_AdditionalSpecialSubframePatterns_ssp4 = 4,
    X2AP_AdditionalSpecialSubframePatterns_ssp5 = 5,
    X2AP_AdditionalSpecialSubframePatterns_ssp6 = 6,
    X2AP_AdditionalSpecialSubframePatterns_ssp7 = 7,
    X2AP_AdditionalSpecialSubframePatterns_ssp8 = 8,
    X2AP_ssp9 = 9
} X2AP_AdditionalSpecialSubframePatterns;

typedef struct X2AP_AdditionalSpecialSubframe_Info {
    unsigned char   bit_mask;
#       define      X2AP_AdditionalSpecialSubframe_Info_iE_Extensions_present 0x80
    X2AP_AdditionalSpecialSubframePatterns additionalspecialSubframePatterns;
    X2AP_CyclicPrefixDL cyclicPrefixDL;
    X2AP_CyclicPrefixUL cyclicPrefixUL;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                 * X2AP_AdditionalSpecialSubframe_Info_iE_Extensions_present if
                 * present */
} X2AP_AdditionalSpecialSubframe_Info;

typedef struct X2AP_CellBasedMDT {
    unsigned char   bit_mask;
#       define      X2AP_CellBasedMDT_iE_Extensions_present 0x80
    struct X2AP_CellIdListforMDT *cellIdListforMDT;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_CellBasedMDT_iE_Extensions_present if
                                   * present */
} X2AP_CellBasedMDT;

typedef struct X2AP_TABasedMDT {
    unsigned char   bit_mask;
#       define      X2AP_TABasedMDT_iE_Extensions_present 0x80
    struct X2AP_TAListforMDT *tAListforMDT;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_TABasedMDT_iE_Extensions_present if
                                   * present */
} X2AP_TABasedMDT;

typedef struct X2AP_TAIBasedMDT {
    unsigned char   bit_mask;
#       define      X2AP_TAIBasedMDT_iE_Extensions_present 0x80
    struct X2AP_TAIListforMDT *tAIListforMDT;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_TAIBasedMDT_iE_Extensions_present if
                                   * present */
} X2AP_TAIBasedMDT;

typedef struct X2AP_AreaScopeOfMDT {
    unsigned short  choice;
#       define      X2AP_cellBased_chosen 1
#       define      X2AP_tABased_chosen 2
#       define      X2AP_pLMNWide_chosen 3
#       define      X2AP_tAIBased_chosen 4
    union {
        X2AP_CellBasedMDT cellBased;  /* to choose, set choice to
                                       * X2AP_cellBased_chosen */
        X2AP_TABasedMDT tABased;  /* to choose, set choice to
                                   * X2AP_tABased_chosen */
        Nulltype        pLMNWide;  /* to choose, set choice to
                                    * X2AP_pLMNWide_chosen */
        X2AP_TAIBasedMDT tAIBased;  /* extension #1; to choose, set choice to
                                     * X2AP_tAIBased_chosen */
    } u;
} X2AP_AreaScopeOfMDT;

/* B */
typedef int             X2AP_BenefitMetric;

typedef struct X2AP_BroadcastPLMNs_Item {
    struct X2AP_BroadcastPLMNs_Item *next;
    X2AP_PLMN_Identity value;
} *X2AP_BroadcastPLMNs_Item;

/* C */
typedef unsigned short  X2AP_CapacityValue;

typedef int             X2AP_CellCapacityClassValue;

typedef struct X2AP_CellIdListforMDT {
    struct X2AP_CellIdListforMDT *next;
    X2AP_ECGI       value;
} *X2AP_CellIdListforMDT;

typedef enum X2AP_Cell_Size {
    X2AP_verysmall = 0,
    X2AP_small = 1,
    X2AP_Cell_Size_medium = 2,
    X2AP_large = 3
} X2AP_Cell_Size;

typedef struct X2AP_CellType {
    unsigned char   bit_mask;
#       define      X2AP_CellType_iE_Extensions_present 0x80
    X2AP_Cell_Size  cell_Size;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_CellType_iE_Extensions_present if
                                   * present */
} X2AP_CellType;

typedef struct X2AP_CoMPHypothesisSetItem {
    unsigned char   bit_mask;
#       define      X2AP_CoMPHypothesisSetItem_iE_Extensions_present 0x80
    X2AP_ECGI       coMPCellID;
    X2AP__bit1      coMPHypothesis;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                          * X2AP_CoMPHypothesisSetItem_iE_Extensions_present if
                          * present */
} X2AP_CoMPHypothesisSetItem;

typedef struct X2AP_CoMPHypothesisSet {
    struct X2AP_CoMPHypothesisSet *next;
    X2AP_CoMPHypothesisSetItem value;
} *X2AP_CoMPHypothesisSet;

typedef struct X2AP_CoMPInformation {
    unsigned char   bit_mask;
#       define      X2AP_CoMPInformation_iE_Extensions_present 0x80
    struct X2AP_CoMPInformationItem *coMPInformationItem;
    struct X2AP_CoMPInformationStartTime *coMPInformationStartTime;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_CoMPInformation_iE_Extensions_present
                                   * if present */
} X2AP_CoMPInformation;

typedef struct X2AP_CoMPInformationItem {
    struct X2AP_CoMPInformationItem *next;
    struct {
        unsigned char   bit_mask;
#           define      X2AP_CoMPInformationItem_iE_Extensions_present 0x80
        struct X2AP_CoMPHypothesisSet *coMPHypothesisSet;
        X2AP_BenefitMetric benefitMetric;
        struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set
                                   * in bit_mask
                            * X2AP_CoMPInformationItem_iE_Extensions_present if
                            * present */
    } value;
} *X2AP_CoMPInformationItem;

typedef struct X2AP_CoMPInformationStartTime {
    struct X2AP_CoMPInformationStartTime *next;
    struct {
        unsigned char   bit_mask;
#           define      X2AP_CoMPInformationStartTime_iE_Extensions_present 0x80
        int             startSFN;
        int             startSubframeNumber;
        struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set
                                   * in bit_mask
                       * X2AP_CoMPInformationStartTime_iE_Extensions_present if
                       * present */
    } value;
} *X2AP_CoMPInformationStartTime;

typedef struct X2AP_CompositeAvailableCapacity {
    unsigned char   bit_mask;
#       define      X2AP_cellCapacityClassValue_present 0x80
#       define      X2AP_CompositeAvailableCapacity_iE_Extensions_present 0x40
    X2AP_CellCapacityClassValue cellCapacityClassValue;  /* optional; set in
                                   * bit_mask
                                   * X2AP_cellCapacityClassValue_present if
                                   * present */
    X2AP_CapacityValue capacityValue;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                     * X2AP_CompositeAvailableCapacity_iE_Extensions_present if
                     * present */
} X2AP_CompositeAvailableCapacity;

typedef struct X2AP_CompositeAvailableCapacityGroup {
    unsigned char   bit_mask;
#       define      X2AP_CompositeAvailableCapacityGroup_iE_Extensions_present 0x80
    X2AP_CompositeAvailableCapacity dL_CompositeAvailableCapacity;
    X2AP_CompositeAvailableCapacity uL_CompositeAvailableCapacity;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                * X2AP_CompositeAvailableCapacityGroup_iE_Extensions_present if
                * present */
} X2AP_CompositeAvailableCapacityGroup;

typedef unsigned short  X2AP_PDCP_SNExtended;

typedef unsigned int    X2AP_HFNModified;

typedef struct X2AP_COUNTValueExtended {
    unsigned char   bit_mask;
#       define      X2AP_COUNTValueExtended_iE_Extensions_present 0x80
    X2AP_PDCP_SNExtended pDCP_SNExtended;
    X2AP_HFNModified hFNModified;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * X2AP_COUNTValueExtended_iE_Extensions_present if
                             * present */
} X2AP_COUNTValueExtended;

typedef enum X2AP_TriggeringMessage {
    X2AP_initiating_message = 0,
    X2AP_successful_outcome = 1,
    X2AP_unsuccessful_outcome = 2
} X2AP_TriggeringMessage;

typedef struct X2AP_CriticalityDiagnostics {
    unsigned char   bit_mask;
#       define      X2AP_procedureCode_present 0x80
#       define      X2AP_triggeringMessage_present 0x40
#       define      X2AP_procedureCriticality_present 0x20
#       define      X2AP_iEsCriticalityDiagnostics_present 0x10
#       define      X2AP_CriticalityDiagnostics_iE_Extensions_present 0x08
    X2AP_ProcedureCode procedureCode;  /* optional; set in bit_mask
                                        * X2AP_procedureCode_present if
                                        * present */
    X2AP_TriggeringMessage triggeringMessage;  /* optional; set in bit_mask
                                                * X2AP_triggeringMessage_present
                                                * if present */
    X2AP_Criticality procedureCriticality;  /* optional; set in bit_mask
                                             * X2AP_procedureCriticality_present
                                             * if present */
    struct X2AP_CriticalityDiagnostics_IE_List *iEsCriticalityDiagnostics;  
                                        /* optional; set in bit_mask
                                    * X2AP_iEsCriticalityDiagnostics_present if
                                    * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * X2AP_CriticalityDiagnostics_iE_Extensions_present if
                         * present */
} X2AP_CriticalityDiagnostics;

typedef enum X2AP_TypeOfError {
    X2AP_not_understood = 0,
    X2AP_missing = 1
} X2AP_TypeOfError;

typedef struct X2AP_CriticalityDiagnostics_IE_List {
    struct X2AP_CriticalityDiagnostics_IE_List *next;
    struct {
        unsigned char   bit_mask;
#           define      X2AP_CriticalityDiagnostics_IE_List_iE_Extensions_present 0x80
        X2AP_Criticality iECriticality;
        X2AP_ProtocolIE_ID iE_ID;
        X2AP_TypeOfError typeOfError;
        struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set
                                   * in bit_mask
                 * X2AP_CriticalityDiagnostics_IE_List_iE_Extensions_present if
                 * present */
    } value;
} *X2AP_CriticalityDiagnostics_IE_List;

typedef struct X2AP_CRNTI {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_CRNTI;

typedef enum X2AP_CSGMembershipStatus {
    X2AP_member = 0,
    X2AP_not_member = 1
} X2AP_CSGMembershipStatus;

typedef struct X2AP_CSG_Id {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_CSG_Id;

/* D */
typedef enum X2AP_DeactivationIndication {
    X2AP_deactivated = 0
} X2AP_DeactivationIndication;

/* P */
typedef enum X2AP_PA_Values {
    X2AP_dB_6 = 0,
    X2AP_dB_4dot77 = 1,
    X2AP_dB_3 = 2,
    X2AP_dB_1dot77 = 3,
    X2AP_dB0 = 4,
    X2AP_dB1 = 5,
    X2AP_dB2 = 6,
    X2AP_dB3 = 7
} X2AP_PA_Values;

typedef struct X2AP_DynamicNAICSInformation {
    unsigned char   bit_mask;
#       define      X2AP_transmissionModes_present 0x80
#       define      X2AP_pB_information_present 0x40
#       define      X2AP_DynamicNAICSInformation_iE_Extensions_present 0x20
    X2AP__bit1      transmissionModes;  /* optional; set in bit_mask
                                         * X2AP_transmissionModes_present if
                                         * present */
    unsigned short  pB_information;  /* optional; set in bit_mask
                                      * X2AP_pB_information_present if
                                      * present */
    struct X2AP__seqof127 {
        struct X2AP__seqof127 *next;
        X2AP_PA_Values  value;
    } *pA_list;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                        * X2AP_DynamicNAICSInformation_iE_Extensions_present if
                        * present */
} X2AP_DynamicNAICSInformation;

typedef struct X2AP_DynamicDLTransmissionInformation {
    unsigned short  choice;
#       define      X2AP_naics_active_chosen 1
#       define      X2AP_naics_inactive_chosen 2
    union {
        X2AP_DynamicNAICSInformation naics_active;  /* to choose, set choice to
                                                  * X2AP_naics_active_chosen */
        Nulltype        naics_inactive;  /* to choose, set choice to
                                          * X2AP_naics_inactive_chosen */
    } u;
} X2AP_DynamicDLTransmissionInformation;

typedef int             X2AP_EARFCNExtension;

typedef struct X2AP_EPLMNs {
    struct X2AP_EPLMNs *next;
    X2AP_PLMN_Identity value;
} *X2AP_EPLMNs;

typedef struct X2AP_E_RAB_List {
    struct X2AP_E_RAB_List *next;
    X2AP_ProtocolIE_Single_Container value;
} *X2AP_E_RAB_List;

typedef struct X2AP_E_RAB_Item {
    unsigned char   bit_mask;
#       define      X2AP_E_RAB_Item_iE_Extensions_present 0x80
    X2AP_E_RAB_ID   e_RAB_ID;
    X2AP_Cause      cause;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_E_RAB_Item_iE_Extensions_present if
                                   * present */
} X2AP_E_RAB_Item;

typedef struct X2AP_EUTRANTraceID {
    unsigned short  length;
    unsigned char   value[8];
} X2AP_EUTRANTraceID;

typedef int             X2AP_ExpectedActivityPeriod;

typedef int             X2AP_ExpectedIdlePeriod;

typedef enum X2AP_SourceOfUEActivityBehaviourInformation {
    X2AP_subscription_information = 0,
    X2AP_statistics = 1
} X2AP_SourceOfUEActivityBehaviourInformation;

typedef struct X2AP_ExpectedUEActivityBehaviour {
    unsigned char   bit_mask;
#       define      X2AP_expectedActivityPeriod_present 0x80
#       define      X2AP_expectedIdlePeriod_present 0x40
#       define      X2AP_sourceofUEActivityBehaviourInformation_present 0x20
#       define      X2AP_ExpectedUEActivityBehaviour_iE_Extensions_present 0x10
    X2AP_ExpectedActivityPeriod expectedActivityPeriod;  /* optional; set in
                                   * bit_mask
                                   * X2AP_expectedActivityPeriod_present if
                                   * present */
    X2AP_ExpectedIdlePeriod expectedIdlePeriod;  /* optional; set in bit_mask
                                           * X2AP_expectedIdlePeriod_present if
                                           * present */
    X2AP_SourceOfUEActivityBehaviourInformation sourceofUEActivityBehaviourInformation;                                 /* optional; set in bit_mask
                       * X2AP_sourceofUEActivityBehaviourInformation_present if
                       * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                    * X2AP_ExpectedUEActivityBehaviour_iE_Extensions_present if
                    * present */
} X2AP_ExpectedUEActivityBehaviour;

typedef enum X2AP_ExpectedHOInterval {
    X2AP_sec15 = 0,
    X2AP_sec30 = 1,
    X2AP_sec60 = 2,
    X2AP_sec90 = 3,
    X2AP_sec120 = 4,
    X2AP_sec180 = 5,
    X2AP_long_time = 6
} X2AP_ExpectedHOInterval;

typedef struct X2AP_ExpectedUEBehaviour {
    unsigned char   bit_mask;
#       define      X2AP_expectedActivity_present 0x80
#       define      X2AP_expectedHOInterval_present 0x40
#       define      X2AP_ExpectedUEBehaviour_iE_Extensions_present 0x20
    X2AP_ExpectedUEActivityBehaviour expectedActivity;  /* optional; set in
                                   * bit_mask X2AP_expectedActivity_present if
                                   * present */
    X2AP_ExpectedHOInterval expectedHOInterval;  /* optional; set in bit_mask
                                           * X2AP_expectedHOInterval_present if
                                           * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * X2AP_ExpectedUEBehaviour_iE_Extensions_present if
                            * present */
} X2AP_ExpectedUEBehaviour;

typedef struct X2AP_ExtendedULInterferenceOverloadInfo {
    unsigned char   bit_mask;
#       define      X2AP_ExtendedULInterferenceOverloadInfo_iE_Extensions_present 0x80
    X2AP__bit1      associatedSubframes;
    struct X2AP_UL_InterferenceOverloadIndication *extended_ul_InterferenceOverloadIndication;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
             * X2AP_ExtendedULInterferenceOverloadInfo_iE_Extensions_present if
             * present */
} X2AP_ExtendedULInterferenceOverloadInfo;

typedef struct X2AP_ForbiddenTAs_Item {
    unsigned char   bit_mask;
#       define      X2AP_ForbiddenTAs_Item_iE_Extensions_present 0x80
    X2AP_PLMN_Identity pLMN_Identity;
    struct X2AP_ForbiddenTACs *forbiddenTACs;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2AP_ForbiddenTAs_Item_iE_Extensions_present if
                              * present */
} X2AP_ForbiddenTAs_Item;

typedef struct X2AP_ForbiddenTAs {
    struct X2AP_ForbiddenTAs *next;
    X2AP_ForbiddenTAs_Item value;
} *X2AP_ForbiddenTAs;

typedef struct X2AP_ForbiddenTACs {
    struct X2AP_ForbiddenTACs *next;
    X2AP_TAC        value;
} *X2AP_ForbiddenTACs;

typedef struct X2AP_ForbiddenLAs_Item {
    unsigned char   bit_mask;
#       define      X2AP_ForbiddenLAs_Item_iE_Extensions_present 0x80
    X2AP_PLMN_Identity pLMN_Identity;
    struct X2AP_ForbiddenLACs *forbiddenLACs;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2AP_ForbiddenLAs_Item_iE_Extensions_present if
                              * present */
} X2AP_ForbiddenLAs_Item;

typedef struct X2AP_ForbiddenLAs {
    struct X2AP_ForbiddenLAs *next;
    X2AP_ForbiddenLAs_Item value;
} *X2AP_ForbiddenLAs;

/* L */
typedef struct X2AP_LAC {
    unsigned short  length;
    unsigned char   value[2];
} X2AP_LAC; /*(EXCEPT ('0000'H|'FFFE'H)) */

typedef struct X2AP_ForbiddenLACs {
    struct X2AP_ForbiddenLACs *next;
    X2AP_LAC        value;
} *X2AP_ForbiddenLACs;

typedef struct X2AP_Fourframes {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_Fourframes;

typedef int             X2AP_FreqBandIndicator;

typedef enum X2AP_FreqBandIndicatorPriority {
    X2AP_not_broadcasted = 0,
    X2AP_broadcasted = 1
} X2AP_FreqBandIndicatorPriority;

typedef struct X2AP_MME_Group_ID {
    unsigned short  length;
    unsigned char   value[2];
} X2AP_MME_Group_ID;

typedef struct X2AP_GU_Group_ID {
    unsigned char   bit_mask;
#       define      X2AP_GU_Group_ID_iE_Extensions_present 0x80
    X2AP_PLMN_Identity pLMN_Identity;
    X2AP_MME_Group_ID mME_Group_ID;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_GU_Group_ID_iE_Extensions_present if
                                   * present */
} X2AP_GU_Group_ID;

typedef struct X2AP_GUGroupIDList {
    struct X2AP_GUGroupIDList *next;
    X2AP_GU_Group_ID value;
} *X2AP_GUGroupIDList;

typedef struct X2AP_MME_Code {
    unsigned short  length;
    unsigned char   value[1];
} X2AP_MME_Code;

typedef struct X2AP_GUMMEI {
    unsigned char   bit_mask;
#       define      X2AP_GUMMEI_iE_Extensions_present 0x80
    X2AP_GU_Group_ID gU_Group_ID;
    X2AP_MME_Code   mME_Code;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask X2AP_GUMMEI_iE_Extensions_present
                                   * if present */
} X2AP_GUMMEI;

/* H */
typedef enum X2AP_HandoverReportType {
    X2AP_hoTooEarly = 0,
    X2AP_hoToWrongCell = 1,
    X2AP_interRATpingpong = 2
} X2AP_HandoverReportType;

/* I */
typedef struct X2AP_Masked_IMEISV {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_Masked_IMEISV;

typedef enum X2AP_InvokeIndication {
    X2AP_abs_information = 0,
    X2AP_naics_information_start = 1,
    X2AP_naics_information_stop = 2
} X2AP_InvokeIndication;

typedef struct X2AP_InterfacesToTrace {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_InterfacesToTrace;

typedef unsigned short  X2AP_Time_UE_StayedInCell;

typedef struct X2AP_LastVisitedEUTRANCellInformation {
    unsigned char   bit_mask;
#       define      X2AP_LastVisitedEUTRANCellInformation_iE_Extensions_present 0x80
    X2AP_ECGI       global_Cell_ID;
    X2AP_CellType   cellType;
    X2AP_Time_UE_StayedInCell time_UE_StayedInCell;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
               * X2AP_LastVisitedEUTRANCellInformation_iE_Extensions_present if
               * present */
} X2AP_LastVisitedEUTRANCellInformation;

typedef struct X2AP_LastVisitedUTRANCellInformation {
    unsigned int    length;
    unsigned char   *value;
} X2AP_LastVisitedUTRANCellInformation;

typedef struct X2AP_LastVisitedGERANCellInformation {
    unsigned short  choice;
#       define      X2AP_undefined_chosen 1
    union {
        Nulltype        undefined;  /* to choose, set choice to
                                     * X2AP_undefined_chosen */
    } u;
} X2AP_LastVisitedGERANCellInformation;

typedef struct X2AP_LastVisitedCell_Item {
    unsigned short  choice;
#       define      X2AP_e_UTRAN_Cell_chosen 1
#       define      X2AP_uTRAN_Cell_chosen 2
#       define      X2AP_gERAN_Cell_chosen 3
    union {
        X2AP_LastVisitedEUTRANCellInformation e_UTRAN_Cell;  /* to choose, set
                                        * choice to X2AP_e_UTRAN_Cell_chosen */
        X2AP_LastVisitedUTRANCellInformation uTRAN_Cell;  /* to choose, set
                                          * choice to X2AP_uTRAN_Cell_chosen */
        X2AP_LastVisitedGERANCellInformation gERAN_Cell;  /* to choose, set
                                          * choice to X2AP_gERAN_Cell_chosen */
    } u;
} X2AP_LastVisitedCell_Item;

typedef enum X2AP_Links_to_log {
    X2AP_uplink = 0,
    X2AP_downlink = 1,
    X2AP_both_uplink_and_downlink = 2
} X2AP_Links_to_log;

typedef enum X2AP_M3period {
    X2AP_ms100 = 0,
    X2AP_ms1000 = 1,
    X2AP_ms10000 = 2
} X2AP_M3period;

/* M */
typedef struct X2AP_M3Configuration {
    unsigned char   bit_mask;
#       define      X2AP_M3Configuration_iE_Extensions_present 0x80
    X2AP_M3period   m3period;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_M3Configuration_iE_Extensions_present
                                   * if present */
} X2AP_M3Configuration;

typedef enum X2AP_M4period {
    X2AP_M4period_ms1024 = 0,
    X2AP_M4period_ms2048 = 1,
    X2AP_M4period_ms5120 = 2,
    X2AP_M4period_ms10240 = 3,
    X2AP_M4period_min1 = 4
} X2AP_M4period;

typedef struct X2AP_M4Configuration {
    unsigned char   bit_mask;
#       define      X2AP_M4Configuration_iE_Extensions_present 0x80
    X2AP_M4period   m4period;
    X2AP_Links_to_log m4_links_to_log;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_M4Configuration_iE_Extensions_present
                                   * if present */
} X2AP_M4Configuration;

typedef enum X2AP_M5period {
    X2AP_M5period_ms1024 = 0,
    X2AP_M5period_ms2048 = 1,
    X2AP_M5period_ms5120 = 2,
    X2AP_M5period_ms10240 = 3,
    X2AP_M5period_min1 = 4
} X2AP_M5period;

typedef struct X2AP_M5Configuration {
    unsigned char   bit_mask;
#       define      X2AP_M5Configuration_iE_Extensions_present 0x80
    X2AP_M5period   m5period;
    X2AP_Links_to_log m5_links_to_log;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_M5Configuration_iE_Extensions_present
                                   * if present */
} X2AP_M5Configuration;

typedef enum X2AP_MDT_Activation {
    X2AP_immediate_MDT_only = 0,
    X2AP_immediate_MDT_and_Trace = 1
} X2AP_MDT_Activation;

typedef struct X2AP_MeasurementsToActivate {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_MeasurementsToActivate;

typedef enum X2AP_M1ReportingTrigger {
    X2AP_periodic = 0,
    X2AP_a2eventtriggered = 1,
    X2AP_a2eventtriggered_periodic = 2
} X2AP_M1ReportingTrigger;

typedef unsigned short  X2AP_Threshold_RSRP;

typedef unsigned short  X2AP_Threshold_RSRQ;

typedef struct X2AP_MeasurementThresholdA2 {
    unsigned short  choice;
#       define      X2AP_threshold_RSRP_chosen 1
#       define      X2AP_threshold_RSRQ_chosen 2
    union {
        X2AP_Threshold_RSRP threshold_RSRP;  /* to choose, set choice to
                                              * X2AP_threshold_RSRP_chosen */
        X2AP_Threshold_RSRQ threshold_RSRQ;  /* to choose, set choice to
                                              * X2AP_threshold_RSRQ_chosen */
    } u;
} X2AP_MeasurementThresholdA2;

typedef struct X2AP_M1ThresholdEventA2 {
    unsigned char   bit_mask;
#       define      X2AP_M1ThresholdEventA2_iE_Extensions_present 0x80
    X2AP_MeasurementThresholdA2 measurementThreshold;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * X2AP_M1ThresholdEventA2_iE_Extensions_present if
                             * present */
} X2AP_M1ThresholdEventA2;

typedef enum X2AP_ReportIntervalMDT {
    X2AP_ms120 = 0,
    X2AP_ms240 = 1,
    X2AP_ms480 = 2,
    X2AP_ms640 = 3,
    X2AP_ReportIntervalMDT_ms1024 = 4,
    X2AP_ReportIntervalMDT_ms2048 = 5,
    X2AP_ReportIntervalMDT_ms5120 = 6,
    X2AP_ReportIntervalMDT_ms10240 = 7,
    X2AP_ReportIntervalMDT_min1 = 8,
    X2AP_min6 = 9,
    X2AP_min12 = 10,
    X2AP_min30 = 11,
    X2AP_min60 = 12
} X2AP_ReportIntervalMDT;

typedef enum X2AP_ReportAmountMDT {
    X2AP_r1 = 0,
    X2AP_r2 = 1,
    X2AP_r4 = 2,
    X2AP_r8 = 3,
    X2AP_r16 = 4,
    X2AP_r32 = 5,
    X2AP_r64 = 6,
    X2AP_rinfinity = 7
} X2AP_ReportAmountMDT;

typedef struct X2AP_M1PeriodicReporting {
    unsigned char   bit_mask;
#       define      X2AP_M1PeriodicReporting_iE_Extensions_present 0x80
    X2AP_ReportIntervalMDT reportInterval;
    X2AP_ReportAmountMDT reportAmount;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * X2AP_M1PeriodicReporting_iE_Extensions_present if
                            * present */
} X2AP_M1PeriodicReporting;

typedef struct X2AP_MDT_Configuration {
    unsigned char   bit_mask;
#       define      X2AP_m1thresholdeventA2_present 0x80
#       define      X2AP_m1periodicReporting_present 0x40
#       define      X2AP_MDT_Configuration_iE_Extensions_present 0x20
    X2AP_MDT_Activation mdt_Activation;
    X2AP_AreaScopeOfMDT areaScopeOfMDT;
    X2AP_MeasurementsToActivate measurementsToActivate;
    X2AP_M1ReportingTrigger m1reportingTrigger;
    X2AP_M1ThresholdEventA2 m1thresholdeventA2;  /* optional; set in bit_mask
                                           * X2AP_m1thresholdeventA2_present if
                                           * present */
/* Included in case of event-triggered, or event-triggered periodic reporting for measurement M1 */
    X2AP_M1PeriodicReporting m1periodicReporting;  /* optional; set in bit_mask
                                          * X2AP_m1periodicReporting_present if
                                          * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2AP_MDT_Configuration_iE_Extensions_present if
                              * present */
/* Included in case of periodic, or event-triggered periodic reporting for measurement M1 */
} X2AP_MDT_Configuration;

typedef struct X2AP_MDTPLMNList {
    struct X2AP_MDTPLMNList *next;
    X2AP_PLMN_Identity value;
} *X2AP_MDTPLMNList;

typedef struct X2AP_MDT_Location_Info {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_MDT_Location_Info;

typedef int             X2AP_Measurement_ID;

typedef struct X2AP_MBMS_Service_Area_Identity {
    unsigned short  length;
    unsigned char   value[2];
} X2AP_MBMS_Service_Area_Identity;

typedef struct X2AP_MBMS_Service_Area_Identity_List {
    struct X2AP_MBMS_Service_Area_Identity_List *next;
    X2AP_MBMS_Service_Area_Identity value;
} *X2AP_MBMS_Service_Area_Identity_List;

typedef enum X2AP_RadioframeAllocationPeriod {
    X2AP_n1 = 0,
    X2AP_n2 = 1,
    X2AP_n4 = 2,
    X2AP_n8 = 3,
    X2AP_n16 = 4,
    X2AP_n32 = 5
} X2AP_RadioframeAllocationPeriod;

/* R */
typedef int             X2AP_RadioframeAllocationOffset;

/* O */
typedef struct X2AP_Oneframe {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_Oneframe;

typedef struct X2AP_SubframeAllocation {
    unsigned short  choice;
#       define      X2AP_oneframe_chosen 1
#       define      X2AP_fourframes_chosen 2
    union {
        X2AP_Oneframe   oneframe;  /* to choose, set choice to
                                    * X2AP_oneframe_chosen */
        X2AP_Fourframes fourframes;  /* to choose, set choice to
                                      * X2AP_fourframes_chosen */
    } u;
} X2AP_SubframeAllocation;

typedef struct X2AP_MBSFN_Subframe_Info {
    unsigned char   bit_mask;
#       define      X2AP_MBSFN_Subframe_Info_iE_Extensions_present 0x80
    X2AP_RadioframeAllocationPeriod radioframeAllocationPeriod;
    X2AP_RadioframeAllocationOffset radioframeAllocationOffset;
    X2AP_SubframeAllocation subframeAllocation;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * X2AP_MBSFN_Subframe_Info_iE_Extensions_present if
                            * present */
} X2AP_MBSFN_Subframe_Info;

typedef struct X2AP_MBSFN_Subframe_Infolist {
    struct X2AP_MBSFN_Subframe_Infolist *next;
    X2AP_MBSFN_Subframe_Info value;
} *X2AP_MBSFN_Subframe_Infolist;

typedef enum X2AP_ManagementBasedMDTallowed {
    X2AP_allowed = 0
} X2AP_ManagementBasedMDTallowed;

typedef struct X2AP_MobilityParametersModificationRange {
    short           handoverTriggerChangeLowerLimit;
    short           handoverTriggerChangeUpperLimit;
} X2AP_MobilityParametersModificationRange;

typedef struct X2AP_MobilityParametersInformation {
    short           handoverTriggerChange;
} X2AP_MobilityParametersInformation;

typedef struct X2AP_BandInfo {
    unsigned char   bit_mask;
#       define      X2AP_BandInfo_iE_Extensions_present 0x80
    X2AP_FreqBandIndicator freqBandIndicator;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_BandInfo_iE_Extensions_present if
                                   * present */
} X2AP_BandInfo;

typedef struct X2AP_MultibandInfoList {
    struct X2AP_MultibandInfoList *next;
    X2AP_BandInfo   value;
} *X2AP_MultibandInfoList;

/* N */
typedef struct X2AP_Neighbour_Information {
    struct X2AP_Neighbour_Information *next;
    struct {
        unsigned char   bit_mask;
#           define      X2AP_Neighbour_Information_iE_Extensions_present 0x80
        X2AP_ECGI       eCGI;
        X2AP_PCI        pCI;
        X2AP_EARFCN     eARFCN;
        struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set
                                   * in bit_mask
                          * X2AP_Neighbour_Information_iE_Extensions_present if
                          * present */
    } value;
} *X2AP_Neighbour_Information;

typedef enum X2AP_Number_of_Antennaports {
    X2AP_an1 = 0,
    X2AP_an2 = 1,
    X2AP_an4 = 2
} X2AP_Number_of_Antennaports;

typedef struct X2AP_PRACH_Configuration {
    unsigned char   bit_mask;
#       define      X2AP_prach_ConfigIndex_present 0x80
#       define      X2AP_PRACH_Configuration_iE_Extensions_present 0x40
    unsigned short  rootSequenceIndex;
    unsigned short  zeroCorrelationIndex;
    ossBoolean      highSpeedFlag;
    unsigned short  prach_FreqOffset;
    unsigned short  prach_ConfigIndex;  /* optional; set in bit_mask
                                         * X2AP_prach_ConfigIndex_present if
                                         * present */
                                        /* present for TDD */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * X2AP_PRACH_Configuration_iE_Extensions_present if
                            * present */
} X2AP_PRACH_Configuration;

typedef enum X2AP_ProSeDirectDiscovery {
    X2AP_ProSeDirectDiscovery_authorized = 0,
    X2AP_ProSeDirectDiscovery_not_authorized = 1
} X2AP_ProSeDirectDiscovery;

typedef enum X2AP_ProSeDirectCommunication {
    X2AP_ProSeDirectCommunication_authorized = 0,
    X2AP_ProSeDirectCommunication_not_authorized = 1
} X2AP_ProSeDirectCommunication;

typedef struct X2AP_ProSeAuthorized {
    unsigned char   bit_mask;
#       define      X2AP_proSeDirectDiscovery_present 0x80
#       define      X2AP_proSeDirectCommunication_present 0x40
#       define      X2AP_ProSeAuthorized_iE_Extensions_present 0x20
    X2AP_ProSeDirectDiscovery proSeDirectDiscovery;  /* optional; set in
                                   * bit_mask X2AP_proSeDirectDiscovery_present
                                   * if present */
    X2AP_ProSeDirectCommunication proSeDirectCommunication;  /* optional; set in
                                   * bit_mask
                                   * X2AP_proSeDirectCommunication_present if
                                   * present */
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_ProSeAuthorized_iE_Extensions_present
                                   * if present */
} X2AP_ProSeAuthorized;

typedef struct X2AP_ReceiveStatusOfULPDCPSDUsExtended {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_ReceiveStatusOfULPDCPSDUsExtended;

typedef enum X2AP_Registration_Request {
    X2AP_start = 0,
    X2AP_stop = 1
} X2AP_Registration_Request;

typedef enum X2AP_ReportingPeriodicityRSRPMR {
    X2AP_one_hundred_20_ms = 0,
    X2AP_two_hundred_40_ms = 1,
    X2AP_four_hundred_80_ms = 2,
    X2AP_six_hundred_40_ms = 3
} X2AP_ReportingPeriodicityRSRPMR;

typedef enum X2AP_RRCConnReestabIndicator {
    X2AP_reconfigurationFailure = 0,
    X2AP_handoverFailure = 1,
    X2AP_otherFailure = 2
} X2AP_RRCConnReestabIndicator;

/* The values correspond to the values of ReestablishmentCause reported from the UE in the RRCConnectionReestablishmentRequest, as defined in TS 36.331 [9] */
typedef enum X2AP_RRCConnSetupIndicator {
    X2AP_rrcConnSetup = 0
} X2AP_RRCConnSetupIndicator;

typedef struct X2AP_RSRPMeasurementResult {
    struct X2AP_RSRPMeasurementResult *next;
    struct {
        unsigned char   bit_mask;
#           define      X2AP_RSRPMeasurementResult_iE_Extensions_present 0x80
        X2AP_ECGI       rSRPCellID;
        int             rSRPMeasured;
        struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set
                                   * in bit_mask
                          * X2AP_RSRPMeasurementResult_iE_Extensions_present if
                          * present */
    } value;
} *X2AP_RSRPMeasurementResult;

typedef struct X2AP_RSRPMRList {
    struct X2AP_RSRPMRList *next;
    struct {
        unsigned char   bit_mask;
#           define      X2AP_RSRPMRList_iE_Extensions_present 0x80
        struct X2AP_RSRPMeasurementResult *rSRPMeasurementResult;
        struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set
                                   * in bit_mask
                                   * X2AP_RSRPMRList_iE_Extensions_present if
                                   * present */
    } value;
} *X2AP_RSRPMRList;

typedef enum X2AP_SCGChangeIndication {
    X2AP_pDCPCountWrapAround = 0,
    X2AP_pSCellChange = 1,
    X2AP_other = 2
} X2AP_SCGChangeIndication;

typedef struct X2AP_SeNBtoMeNBContainer {
    unsigned int    length;
    unsigned char   *value;
} X2AP_SeNBtoMeNBContainer;

typedef struct X2AP_ServedCells {
    struct X2AP_ServedCells *next;
    struct {
        unsigned char   bit_mask;
#           define      X2AP_ServedCells_neighbour_Info_present 0x80
#           define      X2AP_ServedCells_iE_Extensions_present 0x40
        X2AP_ServedCell_Information servedCellInfo;
        struct X2AP_Neighbour_Information *neighbour_Info;  /* optional; set in
                                   * bit_mask
                                   * X2AP_ServedCells_neighbour_Info_present if
                                   * present */
        struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set
                                   * in bit_mask
                                   * X2AP_ServedCells_iE_Extensions_present if
                                   * present */
    } value;
} *X2AP_ServedCells;

typedef struct X2AP_ShortMAC_I {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_ShortMAC_I;

typedef enum X2AP_SRVCCOperationPossible {
    X2AP_possible = 0
} X2AP_SRVCCOperationPossible;

typedef struct X2AP_TAListforMDT {
    struct X2AP_TAListforMDT *next;
    X2AP_TAC        value;
} *X2AP_TAListforMDT;

typedef struct X2AP_TAI_Item {
    unsigned char   bit_mask;
#       define      X2AP_TAI_Item_iE_Extensions_present 0x80
    X2AP_TAC        tAC;
    X2AP_PLMN_Identity pLMN_Identity;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_TAI_Item_iE_Extensions_present if
                                   * present */
} X2AP_TAI_Item;

typedef struct X2AP_TAIListforMDT {
    struct X2AP_TAIListforMDT *next;
    X2AP_TAI_Item   value;
} *X2AP_TAIListforMDT;

typedef struct X2AP_TargetCellInUTRAN {
    unsigned int    length;
    unsigned char   *value;
} X2AP_TargetCellInUTRAN; /* This IE is to be encoded according to the UTRAN Cell ID in the Last Visited UTRAN Cell Information IE in TS 25.413 [24] */

typedef struct X2AP_TargeteNBtoSource_eNBTransparentContainer {
    unsigned int    length;
    unsigned char   *value;
} X2AP_TargeteNBtoSource_eNBTransparentContainer;

typedef enum X2AP_TimeToWait {
    X2AP_v1s = 0,
    X2AP_v2s = 1,
    X2AP_v5s = 2,
    X2AP_v10s = 3,
    X2AP_v20s = 4,
    X2AP_v60s = 5
} X2AP_TimeToWait;

typedef unsigned short  X2AP_Time_UE_StayedInCell_EnhancedGranularity;

typedef enum X2AP_TraceDepth {
    X2AP_minimum = 0,
    X2AP_TraceDepth_medium = 1,
    X2AP_maximum = 2,
    X2AP_minimumWithoutVendorSpecificExtension = 3,
    X2AP_mediumWithoutVendorSpecificExtension = 4,
    X2AP_maximumWithoutVendorSpecificExtension = 5
} X2AP_TraceDepth;

typedef struct X2AP_TraceCollectionEntityIPAddress {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_TraceCollectionEntityIPAddress;

typedef struct X2AP_TraceActivation {
    unsigned char   bit_mask;
#       define      X2AP_TraceActivation_iE_Extensions_present 0x80
    X2AP_EUTRANTraceID eUTRANTraceID;
    X2AP_InterfacesToTrace interfacesToTrace;
    X2AP_TraceDepth traceDepth;
    X2AP_TraceCollectionEntityIPAddress traceCollectionEntityIPAddress;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * X2AP_TraceActivation_iE_Extensions_present
                                   * if present */
} X2AP_TraceActivation;

/* U */
typedef struct X2AP_UE_HistoryInformation {
    struct X2AP_UE_HistoryInformation *next;
    X2AP_LastVisitedCell_Item value;
} *X2AP_UE_HistoryInformation;

typedef struct X2AP_UE_HistoryInformationFromTheUE {
    unsigned int    length;
    unsigned char   *value;
} X2AP_UE_HistoryInformationFromTheUE;

typedef unsigned short  X2AP_UE_X2AP_ID;

typedef enum X2AP_UL_InterferenceOverloadIndication_Item {
    X2AP_high_interference = 0,
    X2AP_medium_interference = 1,
    X2AP_low_interference = 2
} X2AP_UL_InterferenceOverloadIndication_Item;

typedef struct X2AP_UL_InterferenceOverloadIndication {
    struct X2AP_UL_InterferenceOverloadIndication *next;
    X2AP_UL_InterferenceOverloadIndication_Item value;
} *X2AP_UL_InterferenceOverloadIndication;

typedef struct X2AP_UL_HighInterferenceIndication {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} X2AP_UL_HighInterferenceIndication;

typedef struct X2AP_UL_HighInterferenceIndicationInfo_Item {
    unsigned char   bit_mask;
#       define      X2AP_UL_HighInterferenceIndicationInfo_Item_iE_Extensions_present 0x80
    X2AP_ECGI       target_Cell_ID;
    X2AP_UL_HighInterferenceIndication ul_interferenceindication;
    struct X2AP_ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
         * X2AP_UL_HighInterferenceIndicationInfo_Item_iE_Extensions_present if
         * present */
} X2AP_UL_HighInterferenceIndicationInfo_Item;

typedef struct X2AP_UL_HighInterferenceIndicationInfo {
    struct X2AP_UL_HighInterferenceIndicationInfo *next;
    X2AP_UL_HighInterferenceIndicationInfo_Item value;
} *X2AP_UL_HighInterferenceIndicationInfo;

typedef struct X2AP_UE_RLF_Report_Container {
    unsigned int    length;
    unsigned char   *value;
} X2AP_UE_RLF_Report_Container;

/* This IE is a transparent container and shall be encoded as the RLF-Report-r9 field contained in the UEInformationResponse message as defined in TS 36.331 [9] */
typedef struct X2AP_UE_RLF_Report_Container_for_extended_bands {
    unsigned int    length;
    unsigned char   *value;
} X2AP_UE_RLF_Report_Container_for_extended_bands;

typedef enum X2AP_Presence {
    X2AP_optional = 0,
    X2AP_conditional = 1,
    X2AP_mandatory = 2
} X2AP_Presence;

/* ************************************************************** */
/**/
/* IE parameter types from other modules. */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* Class Definition for Protocol IEs */
/**/
/* ************************************************************** */
typedef struct X2AP_X2AP_PROTOCOL_IES {
    X2AP_ProtocolIE_ID id;
    X2AP_Criticality criticality;
    unsigned short  Value;
    X2AP_Presence   presence;
} X2AP_X2AP_PROTOCOL_IES;

/* ************************************************************** */
/**/
/* Class Definition for Protocol Extensions */
/**/
/* ************************************************************** */
typedef struct X2AP_X2AP_PROTOCOL_EXTENSION {
    X2AP_ProtocolIE_ID id;
    X2AP_Criticality criticality;
    unsigned short  Extension;
    X2AP_Presence   presence;
} X2AP_X2AP_PROTOCOL_EXTENSION;

/* ************************************************************** */
/**/
/* Class Definition for Private IEs */
/**/
/* ************************************************************** */
typedef struct X2AP_X2AP_PRIVATE_IES {
    X2AP_PrivateIE_ID id;
    X2AP_Criticality criticality;
    unsigned short  Value;
    X2AP_Presence   presence;
    long            _oss_unique_index;
} X2AP_X2AP_PRIVATE_IES;

#ifndef _OSSNOVALUES

/* ************************************************************** */
/**/
/* Interface Elementary Procedures */
/**/
/* ************************************************************** */
extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_handoverPreparation;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_snStatusTransfer;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_uEContextRelease;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_handoverCancel;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_handoverReport;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_errorIndication;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_reset;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2Setup;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_loadIndication;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_eNBConfigurationUpdate;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_resourceStatusReportingInitiation;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_resourceStatusReporting;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_rLFIndication;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_privateMessage;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_mobilitySettingsChange;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_cellActivation;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2Release;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2APMessageTransfer;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBAdditionPreparation;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBReconfigurationCompletion;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_meNBinitiatedSeNBModificationPreparation;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBinitiatedSeNBModification;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_meNBinitiatedSeNBRelease;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBinitiatedSeNBRelease;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBCounterCheck;

extern X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2Removal;

/* ************************************************************** */
/**/
/* Extension constants */
/**/
/* ************************************************************** */
extern const int X2AP_maxPrivateIEs;

extern const int X2AP_maxProtocolExtensions;

extern const int X2AP_maxProtocolIEs;

/* ************************************************************** */
/**/
/* Elementary Procedures */
/**/
/* ************************************************************** */
extern const X2AP_ProcedureCode X2AP_id_handoverPreparation;

extern const X2AP_ProcedureCode X2AP_id_handoverCancel;

extern const X2AP_ProcedureCode X2AP_id_loadIndication;

extern const X2AP_ProcedureCode X2AP_id_errorIndication;

extern const X2AP_ProcedureCode X2AP_id_snStatusTransfer;

extern const X2AP_ProcedureCode X2AP_id_uEContextRelease;

extern const X2AP_ProcedureCode X2AP_id_x2Setup;

extern const X2AP_ProcedureCode X2AP_id_reset;

extern const X2AP_ProcedureCode X2AP_id_eNBConfigurationUpdate;

extern const X2AP_ProcedureCode X2AP_id_resourceStatusReportingInitiation;

extern const X2AP_ProcedureCode X2AP_id_resourceStatusReporting;

extern const X2AP_ProcedureCode X2AP_id_privateMessage;

extern const X2AP_ProcedureCode X2AP_id_mobilitySettingsChange;

extern const X2AP_ProcedureCode X2AP_id_rLFIndication;

extern const X2AP_ProcedureCode X2AP_id_handoverReport;

extern const X2AP_ProcedureCode X2AP_id_cellActivation;

extern const X2AP_ProcedureCode X2AP_id_x2Release;

extern const X2AP_ProcedureCode X2AP_id_x2APMessageTransfer;

extern const X2AP_ProcedureCode X2AP_id_x2Removal;

extern const X2AP_ProcedureCode X2AP_id_seNBAdditionPreparation;

extern const X2AP_ProcedureCode X2AP_id_seNBReconfigurationCompletion;

extern const X2AP_ProcedureCode X2AP_id_meNBinitiatedSeNBModificationPreparation;

extern const X2AP_ProcedureCode X2AP_id_seNBinitiatedSeNBModification;

extern const X2AP_ProcedureCode X2AP_id_meNBinitiatedSeNBRelease;

extern const X2AP_ProcedureCode X2AP_id_seNBinitiatedSeNBRelease;

extern const X2AP_ProcedureCode X2AP_id_seNBCounterCheck;

/* ************************************************************** */
/**/
/* Lists */
/**/
/* ************************************************************** */
extern const int X2AP_maxEARFCN;

extern const int X2AP_maxEARFCNPlusOne;

extern const int X2AP_newmaxEARFCN;

extern const int X2AP_maxInterfaces;

extern const int X2AP_maxCellineNB;

extern const int X2AP_maxnoofBands;

extern const int X2AP_maxnoofBearers;

extern const int X2AP_maxNrOfErrors;

extern const int X2AP_maxnoofPDCP_SN;

extern const int X2AP_maxnoofEPLMNs;

extern const int X2AP_maxnoofEPLMNsPlusOne;

extern const int X2AP_maxnoofForbLACs;

extern const int X2AP_maxnoofForbTACs;

extern const int X2AP_maxnoofBPLMNs;

extern const int X2AP_maxnoofNeighbours;

extern const int X2AP_maxnoofPRBs;

extern const int X2AP_maxPools;

extern const int X2AP_maxnoofCells;

extern const int X2AP_maxnoofMBSFN;

extern const int X2AP_maxFailedMeasObjects;

extern const int X2AP_maxnoofCellIDforMDT;

extern const int X2AP_maxnoofTAforMDT;

extern const int X2AP_maxnoofMBMSServiceAreaIdentities;

extern const int X2AP_maxnoofMDTPLMNs;

extern const int X2AP_maxnoofCoMPHypothesisSet;

extern const int X2AP_maxnoofCoMPCells;

extern const int X2AP_maxUEReport;

extern const int X2AP_maxCellReport;

extern const int X2AP_maxnoofPA;

/* ************************************************************** */
/**/
/* IEs */
/**/
/* ************************************************************** */
extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_List;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RAB_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_NotAdmitted_List;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeSetup_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_Cause;

extern const X2AP_ProtocolIE_ID X2AP_id_CellInformation;

extern const X2AP_ProtocolIE_ID X2AP_id_CellInformation_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_New_eNB_UE_X2AP_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_Old_eNB_UE_X2AP_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_TargetCell_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_TargeteNBtoSource_eNBTransparentContainer;

extern const X2AP_ProtocolIE_ID X2AP_id_TraceActivation;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_ContextInformation;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_HistoryInformation;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_X2AP_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_CriticalityDiagnostics;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToStatusTransfer_List;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToStatusTransfer_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_ServedCells;

extern const X2AP_ProtocolIE_ID X2AP_id_GlobalENB_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_TimeToWait;

extern const X2AP_ProtocolIE_ID X2AP_id_GUMMEI_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_GUGroupIDList;

extern const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToAdd;

extern const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToModify;

extern const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToDelete;

extern const X2AP_ProtocolIE_ID X2AP_id_Registration_Request;

extern const X2AP_ProtocolIE_ID X2AP_id_CellToReport;

extern const X2AP_ProtocolIE_ID X2AP_id_ReportingPeriodicity;

extern const X2AP_ProtocolIE_ID X2AP_id_CellToReport_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_CellMeasurementResult;

extern const X2AP_ProtocolIE_ID X2AP_id_CellMeasurementResult_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_GUGroupIDToAddList;

extern const X2AP_ProtocolIE_ID X2AP_id_GUGroupIDToDeleteList;

extern const X2AP_ProtocolIE_ID X2AP_id_SRVCCOperationPossible;

extern const X2AP_ProtocolIE_ID X2AP_id_Measurement_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_ReportCharacteristics;

extern const X2AP_ProtocolIE_ID X2AP_id_ENB1_Measurement_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_ENB2_Measurement_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_Number_of_Antennaports;

extern const X2AP_ProtocolIE_ID X2AP_id_CompositeAvailableCapacityGroup;

extern const X2AP_ProtocolIE_ID X2AP_id_ENB1_Cell_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_ENB2_Cell_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_ENB2_Proposed_Mobility_Parameters;

extern const X2AP_ProtocolIE_ID X2AP_id_ENB1_Mobility_Parameters;

extern const X2AP_ProtocolIE_ID X2AP_id_ENB2_Mobility_Parameters_Modification_Range;

extern const X2AP_ProtocolIE_ID X2AP_id_FailureCellPCI;

extern const X2AP_ProtocolIE_ID X2AP_id_Re_establishmentCellECGI;

extern const X2AP_ProtocolIE_ID X2AP_id_FailureCellCRNTI;

extern const X2AP_ProtocolIE_ID X2AP_id_ShortMAC_I;

extern const X2AP_ProtocolIE_ID X2AP_id_SourceCellECGI;

extern const X2AP_ProtocolIE_ID X2AP_id_FailureCellECGI;

extern const X2AP_ProtocolIE_ID X2AP_id_HandoverReportType;

extern const X2AP_ProtocolIE_ID X2AP_id_PRACH_Configuration;

extern const X2AP_ProtocolIE_ID X2AP_id_MBSFN_Subframe_Info;

extern const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToActivate;

extern const X2AP_ProtocolIE_ID X2AP_id_ActivatedCellList;

extern const X2AP_ProtocolIE_ID X2AP_id_DeactivationIndication;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_RLF_Report_Container;

extern const X2AP_ProtocolIE_ID X2AP_id_ABSInformation;

extern const X2AP_ProtocolIE_ID X2AP_id_InvokeIndication;

extern const X2AP_ProtocolIE_ID X2AP_id_ABS_Status;

extern const X2AP_ProtocolIE_ID X2AP_id_PartialSuccessIndicator;

extern const X2AP_ProtocolIE_ID X2AP_id_MeasurementInitiationResult_List;

extern const X2AP_ProtocolIE_ID X2AP_id_MeasurementInitiationResult_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_MeasurementFailureCause_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_CompleteFailureCauseInformation_List;

extern const X2AP_ProtocolIE_ID X2AP_id_CompleteFailureCauseInformation_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_CSG_Id;

extern const X2AP_ProtocolIE_ID X2AP_id_CSGMembershipStatus;

extern const X2AP_ProtocolIE_ID X2AP_id_MDTConfiguration;

extern const X2AP_ProtocolIE_ID X2AP_id_ManagementBasedMDTallowed;

extern const X2AP_ProtocolIE_ID X2AP_id_RRCConnSetupIndicator;

extern const X2AP_ProtocolIE_ID X2AP_id_NeighbourTAC;

extern const X2AP_ProtocolIE_ID X2AP_id_Time_UE_StayedInCell_EnhancedGranularity;

extern const X2AP_ProtocolIE_ID X2AP_id_RRCConnReestabIndicator;

extern const X2AP_ProtocolIE_ID X2AP_id_MBMS_Service_Area_List;

extern const X2AP_ProtocolIE_ID X2AP_id_HO_cause;

extern const X2AP_ProtocolIE_ID X2AP_id_TargetCellInUTRAN;

extern const X2AP_ProtocolIE_ID X2AP_id_MobilityInformation;

extern const X2AP_ProtocolIE_ID X2AP_id_SourceCellCRNTI;

extern const X2AP_ProtocolIE_ID X2AP_id_MultibandInfoList;

extern const X2AP_ProtocolIE_ID X2AP_id_M3Configuration;

extern const X2AP_ProtocolIE_ID X2AP_id_M4Configuration;

extern const X2AP_ProtocolIE_ID X2AP_id_M5Configuration;

extern const X2AP_ProtocolIE_ID X2AP_id_MDT_Location_Info;

extern const X2AP_ProtocolIE_ID X2AP_id_ManagementBasedMDTPLMNList;

extern const X2AP_ProtocolIE_ID X2AP_id_SignallingBasedMDTPLMNList;

extern const X2AP_ProtocolIE_ID X2AP_id_ReceiveStatusOfULPDCPSDUsExtended;

extern const X2AP_ProtocolIE_ID X2AP_id_ULCOUNTValueExtended;

extern const X2AP_ProtocolIE_ID X2AP_id_DLCOUNTValueExtended;

extern const X2AP_ProtocolIE_ID X2AP_id_eARFCNExtension;

extern const X2AP_ProtocolIE_ID X2AP_id_UL_EARFCNExtension;

extern const X2AP_ProtocolIE_ID X2AP_id_DL_EARFCNExtension;

extern const X2AP_ProtocolIE_ID X2AP_id_AdditionalSpecialSubframe_Info;

extern const X2AP_ProtocolIE_ID X2AP_id_Masked_IMEISV;

extern const X2AP_ProtocolIE_ID X2AP_id_IntendedULDLConfiguration;

extern const X2AP_ProtocolIE_ID X2AP_id_ExtendedULInterferenceOverloadInfo;

extern const X2AP_ProtocolIE_ID X2AP_id_RNL_Header;

extern const X2AP_ProtocolIE_ID X2AP_id_x2APMessage;

extern const X2AP_ProtocolIE_ID X2AP_id_ProSeAuthorized;

extern const X2AP_ProtocolIE_ID X2AP_id_ExpectedUEBehaviour;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_HistoryInformationFromTheUE;

extern const X2AP_ProtocolIE_ID X2AP_id_DynamicDLTransmissionInformation;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_RLF_Report_Container_for_extended_bands;

extern const X2AP_ProtocolIE_ID X2AP_id_CoMPInformation;

extern const X2AP_ProtocolIE_ID X2AP_id_ReportingPeriodicityRSRPMR;

extern const X2AP_ProtocolIE_ID X2AP_id_RSRPMRList;

extern const X2AP_ProtocolIE_ID X2AP_id_MeNB_UE_X2AP_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_SeNB_UE_X2AP_ID;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_SecurityCapabilities;

extern const X2AP_ProtocolIE_ID X2AP_id_SeNBSecurityKey;

extern const X2AP_ProtocolIE_ID X2AP_id_SeNBUEAggregateMaximumBitRate;

extern const X2AP_ProtocolIE_ID X2AP_id_ServingPLMN;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeAdded_List;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeAdded_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_MeNBtoSeNBContainer;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_List;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_Item;

extern const X2AP_ProtocolIE_ID X2AP_id_SeNBtoMeNBContainer;

extern const X2AP_ProtocolIE_ID X2AP_id_ResponseInformationSeNBReconfComp;

extern const X2AP_ProtocolIE_ID X2AP_id_UE_ContextInformationSeNBModReq;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeAdded_ModReqItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeModified_ModReqItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_ModReqItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_ModAckList;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeModified_ModAckList;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeReleased_ModAckList;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_ModAckItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeModified_ModAckItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeReleased_ModAckItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_ModReqd;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_ModReqdItem;

extern const X2AP_ProtocolIE_ID X2AP_id_SCGChangeIndication;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_List_RelReq;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_RelReqItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_List_RelConf;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_RelConfItem;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToCounterCheck_List;

extern const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToCounterCheckItem;

extern const X2AP_ProtocolIE_ID X2AP_id_FreqBandIndicatorPriority;

#endif  /* #ifndef _OSSNOVALUES */


extern void * const x2ap;    /* encoder-decoder control table */
#endif /* OSS_x2ap */
