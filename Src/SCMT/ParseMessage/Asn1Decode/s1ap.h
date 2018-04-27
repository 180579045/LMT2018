/*************************************************************/
/* Copyright (C) 2011 OSS Nokalva, Inc.  All rights reserved.*/
/*************************************************************/

/* THIS FILE IS PROPRIETARY MATERIAL OF OSS NOKALVA, INC.
 * AND MAY BE USED ONLY BY DIRECT LICENSEES OF OSS NOKALVA, INC.
 * THIS FILE MAY NOT BE DISTRIBUTED. */

/* Generated for: Datang Mobile Communications Equipment CO.,LTD, Beijing, China - Windows license 9319 (C) & 9652 (ASN-1Step) */
/* Abstract syntax: s1ap */
/* Created: Fri Mar 18 09:45:31 2011 */
/* ASN.1 compiler version: 8.4 */
/* Code generated for runtime version 8.4 or later */
/* Compiler operating system: Windows */
/* Compiler machine type: Intel x86 */
/* Target operating system: Windows */
/* Target machine type: Intel x86 */
/* C compiler options required: -Zp4 (Microsoft) */
/* ASN.1 compiler options and file names specified:
 * -controlfile s1ap.c -headerfile s1ap.h -errorfile asn_error.txt
 * -externalname s1ap -debug -per -root -autoencdec -compat nosharedtypes
 * asn1dflt.ms.zp4 36413vc60.asn
 */

#ifndef OSS_s1ap
#define OSS_s1ap

/* 9.3.7	Container Definitions */
/* ************************************************************** */
/**/
/* Container definitions */
/**/
/* ************************************************************** */

#include "ossasn1.h"

#define          S1AP_PDU_PDU 1
#define          HandoverRequired_PDU 2
#define          HandoverCommand_PDU 3
#define          E_RABSubjecttoDataForwardingList_PDU 4
#define          E_RABDataForwardingItem_PDU 5
#define          HandoverPreparationFailure_PDU 6
#define          HandoverRequest_PDU 7
#define          E_RABToBeSetupListHOReq_PDU 8
#define          E_RABToBeSetupItemHOReq_PDU 9
#define          HandoverRequestAcknowledge_PDU 10
#define          E_RABAdmittedList_PDU 11
#define          E_RABAdmittedItem_PDU 12
#define          E_RABFailedtoSetupListHOReqAck_PDU 13
#define          E_RABFailedToSetupItemHOReqAck_PDU 14
#define          HandoverFailure_PDU 15
#define          HandoverNotify_PDU 16
#define          PathSwitchRequest_PDU 17
#define          E_RABToBeSwitchedDLList_PDU 18
#define          E_RABToBeSwitchedDLItem_PDU 19
#define          PathSwitchRequestAcknowledge_PDU 20
#define          E_RABToBeSwitchedULList_PDU 21
#define          E_RABToBeSwitchedULItem_PDU 22
#define          PathSwitchRequestFailure_PDU 23
#define          HandoverCancel_PDU 24
#define          HandoverCancelAcknowledge_PDU 25
#define          E_RABSetupRequest_PDU 26
#define          E_RABToBeSetupListBearerSUReq_PDU 27
#define          E_RABToBeSetupItemBearerSUReq_PDU 28
#define          E_RABSetupResponse_PDU 29
#define          E_RABSetupListBearerSURes_PDU 30
#define          E_RABSetupItemBearerSURes_PDU 31
#define          E_RABModifyRequest_PDU 32
#define          E_RABToBeModifiedListBearerModReq_PDU 33
#define          E_RABToBeModifiedItemBearerModReq_PDU 34
#define          E_RABModifyResponse_PDU 35
#define          E_RABModifyListBearerModRes_PDU 36
#define          E_RABModifyItemBearerModRes_PDU 37
#define          E_RABReleaseCommand_PDU 38
#define          E_RABReleaseResponse_PDU 39
#define          E_RABReleaseListBearerRelComp_PDU 40
#define          E_RABReleaseItemBearerRelComp_PDU 41
#define          E_RABReleaseIndication_PDU 42
#define          InitialContextSetupRequest_PDU 43
#define          E_RABToBeSetupListCtxtSUReq_PDU 44
#define          E_RABToBeSetupItemCtxtSUReq_PDU 45
#define          InitialContextSetupResponse_PDU 46
#define          E_RABSetupListCtxtSURes_PDU 47
#define          E_RABSetupItemCtxtSURes_PDU 48
#define          InitialContextSetupFailure_PDU 49
#define          Paging_PDU 50
#define          TAIList_PDU 51
#define          TAIItem_PDU 52
#define          UEContextReleaseRequest_PDU 53
#define          UEContextReleaseCommand_PDU 54
#define          UEContextReleaseComplete_PDU 55
#define          UEContextModificationRequest_PDU 56
#define          UEContextModificationResponse_PDU 57
#define          UEContextModificationFailure_PDU 58
#define          UERadioCapabilityMatchRequest_PDU 59
#define          UERadioCapabilityMatchResponse_PDU 60
#define          DownlinkNASTransport_PDU 61
#define          InitialUEMessage_PDU 62
#define          UplinkNASTransport_PDU 63
#define          NASNonDeliveryIndication_PDU 64
#define          Reset_PDU 65
#define          ResetType_PDU 66
#define          ResetAcknowledge_PDU 67
#define          UE_associatedLogicalS1_ConnectionListResAck_PDU 68
#define          ErrorIndication_PDU 69
#define          S1SetupRequest_PDU 70
#define          S1SetupResponse_PDU 71
#define          S1SetupFailure_PDU 72
#define          ENBConfigurationUpdate_PDU 73
#define          ENBConfigurationUpdateAcknowledge_PDU 74
#define          ENBConfigurationUpdateFailure_PDU 75
#define          MMEConfigurationUpdate_PDU 76
#define          MMEConfigurationUpdateAcknowledge_PDU 77
#define          MMEConfigurationUpdateFailure_PDU 78
#define          DownlinkS1cdma2000tunnelling_PDU 79
#define          UplinkS1cdma2000tunnelling_PDU 80
#define          UECapabilityInfoIndication_PDU 81
#define          ENBStatusTransfer_PDU 82
#define          MMEStatusTransfer_PDU 83
#define          TraceStart_PDU 84
#define          TraceFailureIndication_PDU 85
#define          DeactivateTrace_PDU 86
#define          CellTrafficTrace_PDU 87
#define          LocationReportingControl_PDU 88
#define          LocationReportingFailureIndication_PDU 89
#define          LocationReport_PDU 90
#define          OverloadStart_PDU 91
#define          OverloadStop_PDU 92
#define          WriteReplaceWarningRequest_PDU 93
#define          WriteReplaceWarningResponse_PDU 94
#define          ENBDirectInformationTransfer_PDU 95
#define          Inter_SystemInformationTransferType_PDU 96
#define          MMEDirectInformationTransfer_PDU 97
#define          ENBConfigurationTransfer_PDU 98
#define          MMEConfigurationTransfer_PDU 99
#define          PrivateMessage_PDU 100
#define          KillRequest_PDU 101
#define          KillResponse_PDU 102
#define          PWSRestartIndication_PDU 103
#define          DownlinkUEAssociatedLPPaTransport_PDU 104
#define          UplinkUEAssociatedLPPaTransport_PDU 105
#define          DownlinkNonUEAssociatedLPPaTransport_PDU 106
#define          UplinkNonUEAssociatedLPPaTransport_PDU 107
#define          E_RABModificationIndication_PDU 108
#define          E_RABToBeModifiedListBearerModInd_PDU 109
#define          E_RABToBeModifiedItemBearerModInd_PDU 110
#define          E_RABNotToBeModifiedListBearerModInd_PDU 111
#define          E_RABNotToBeModifiedItemBearerModInd_PDU 112
#define          E_RABModificationConfirm_PDU 113
#define          E_RABModifyListBearerModConf_PDU 114
#define          E_RABModifyItemBearerModConf_PDU 115
#define          Bearers_SubjectToStatusTransfer_Item_PDU 116
#define          BroadcastCancelledAreaList_PDU 117
#define          BroadcastCompletedAreaList_PDU 118
#define          Cause_PDU 119
#define          CellAccessMode_PDU 120
#define          Cdma2000PDU_PDU 121
#define          Cdma2000RATType_PDU 122
#define          Cdma2000SectorID_PDU 123
#define          Cdma2000HOStatus_PDU 124
#define          Cdma2000HORequiredIndication_PDU 125
#define          Cdma2000OneXSRVCCInfo_PDU 126
#define          Cdma2000OneXRAND_PDU 127
#define          CNDomain_PDU 128
#define          ConcurrentWarningMessageIndicator_PDU 129
#define          Correlation_ID_PDU 130
#define          CSFallbackIndicator_PDU 131
#define          AdditionalCSFallbackIndicator_PDU 132
#define          CSG_Id_PDU 133
#define          CSG_IdList_PDU 134
#define          CSGMembershipStatus_PDU 135
#define          COUNTValueExtended_PDU 136
#define          CriticalityDiagnostics_PDU 137
#define          DataCodingScheme_PDU 138
#define          Direct_Forwarding_Path_Availability_PDU 139
#define          Data_Forwarding_Not_Possible_PDU 140
#define          EmergencyAreaIDListForRestart_PDU 141
#define          Global_ENB_ID_PDU 142
#define          GUMMEIList_PDU 143
#define          ENB_StatusTransfer_TransparentContainer_PDU 144
#define          ENB_UE_S1AP_ID_PDU 145
#define          ENBname_PDU 146
#define          E_RABInformationListItem_PDU 147
#define          E_RABList_PDU 148
#define          E_RABItem_PDU 149
#define          EUTRAN_CGI_PDU 150
#define          EUTRANRoundTripDelayEstimationInfo_PDU 151
#define          ExpectedUEBehaviour_PDU 152
#define          ExtendedRepetitionPeriod_PDU 153
#define          GUMMEI_PDU 154
#define          GUMMEIType_PDU 155
#define          GWContextReleaseIndication_PDU 156
#define          HandoverRestrictionList_PDU 157
#define          HandoverType_PDU 158
#define          Masked_IMEISV_PDU 159
#define          KillAllWarningMessages_PDU 160
#define          LAI_PDU 161
#define          L3_Information_PDU 162
#define          LPPa_PDU_PDU 163
#define          LHN_ID_PDU 164
#define          LoggedMBSFNMDT_PDU 165
#define          M3Configuration_PDU 166
#define          M4Configuration_PDU 167
#define          M5Configuration_PDU 168
#define          MDT_Location_Info_PDU 169
#define          MDT_Configuration_PDU 170
#define          ManagementBasedMDTAllowed_PDU 171
#define          MDTPLMNList_PDU 172
#define          PrivacyIndicator_PDU 173
#define          MessageIdentifier_PDU 174
#define          MobilityInformation_PDU 175
#define          MMEname_PDU 176
#define          MMERelaySupportIndicator_PDU 177
#define          MME_UE_S1AP_ID_PDU 178
#define          MSClassmark2_PDU 179
#define          MSClassmark3_PDU 180
#define          MutingAvailabilityIndication_PDU 181
#define          MutingPatternInformation_PDU 182
#define          NAS_PDU_PDU 183
#define          NASSecurityParametersfromE_UTRAN_PDU 184
#define          NASSecurityParameterstoE_UTRAN_PDU 185
#define          NumberofBroadcastRequest_PDU 186
#define          OldBSS_ToNewBSS_Information_PDU 187
#define          OverloadResponse_PDU 188
#define          PagingDRX_PDU 189
#define          PagingPriority_PDU 190
#define          ProSeAuthorized_PDU 191
#define          PS_ServiceNotAvailable_PDU 192
#define          ReceiveStatusOfULPDCPSDUsExtended_PDU 193
#define          RelativeMMECapacity_PDU 194
#define          RelayNode_Indicator_PDU 195
#define          RequestType_PDU 196
#define          RepetitionPeriod_PDU 197
#define          RRC_Establishment_Cause_PDU 198
#define          ECGIListForRestart_PDU 199
#define          Routing_ID_PDU 200
#define          SecurityKey_PDU 201
#define          SecurityContext_PDU 202
#define          SerialNumber_PDU 203
#define          SONInformationReport_PDU 204
#define          SONConfigurationTransfer_PDU 205
#define          SynchronisationInformation_PDU 206
#define          Source_ToTarget_TransparentContainer_PDU 207
#define          SourceBSS_ToTargetBSS_TransparentContainer_PDU 208
#define          SRVCCOperationPossible_PDU 209
#define          SRVCCHOIndication_PDU 210
#define          SourceeNB_ToTargeteNB_TransparentContainer_PDU 211
#define          SourceRNC_ToTargetRNC_TransparentContainer_PDU 212
#define          ServedGUMMEIs_PDU 213
#define          SubscriberProfileIDforRFP_PDU 214
#define          SupportedTAs_PDU 215
#define          TimeSynchronisationInfo_PDU 216
#define          S_TMSI_PDU 217
#define          TAI_PDU 218
#define          TargetID_PDU 219
#define          TargeteNB_ToSourceeNB_TransparentContainer_PDU 220
#define          Target_ToSource_TransparentContainer_PDU 221
#define          TargetRNC_ToSourceRNC_TransparentContainer_PDU 222
#define          TargetBSS_ToSourceBSS_TransparentContainer_PDU 223
#define          TimeToWait_PDU 224
#define          Time_UE_StayedInCell_EnhancedGranularity_PDU 225
#define          TransportInformation_PDU 226
#define          TransportLayerAddress_PDU 227
#define          TraceActivation_PDU 228
#define          E_UTRAN_Trace_ID_PDU 229
#define          TrafficLoadReductionIndication_PDU 230
#define          TunnelInformation_PDU 231
#define          TAIListForRestart_PDU 232
#define          UEAggregateMaximumBitrate_PDU 233
#define          UE_S1AP_IDs_PDU 234
#define          UE_associatedLogicalS1_ConnectionItem_PDU 235
#define          UEIdentityIndexValue_PDU 236
#define          UE_HistoryInformationFromTheUE_PDU 237
#define          UEPagingID_PDU 238
#define          UERadioCapability_PDU 239
#define          UERadioCapabilityForPaging_PDU 240
#define          UESecurityCapabilities_PDU 241
#define          UserLocationInformation_PDU 242
#define          VoiceSupportMatchIndicator_PDU 243
#define          WarningAreaList_PDU 244
#define          WarningType_PDU 245
#define          WarningSecurityInfo_PDU 246
#define          WarningMessageContents_PDU 247
#define          X2TNLConfigurationInfo_PDU 248
#define          ENBX2ExtTLAs_PDU 249
#define          ENBIndirectX2TransportLayerAddresses_PDU 250
#define          S1AP_ELEMENTARY_PROCEDURES_OSET 1 /* Class is S1AP-ELEMENTARY-PROCEDURE */
#define          S1AP_ELEMENTARY_PROCEDURES_CLASS_1_OSET 2 /* Class is S1AP-ELEMENTARY-PROCEDURE */
#define          S1AP_ELEMENTARY_PROCEDURES_CLASS_2_OSET 3 /* Class is S1AP-ELEMENTARY-PROCEDURE */
#define          HandoverRequiredIEs_OSET 4  /* Class is S1AP-PROTOCOL-IES */
#define          HandoverCommandIEs_OSET 5   /* Class is S1AP-PROTOCOL-IES */
#define          E_RABDataForwardingItemIEs_OSET 6 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABDataForwardingItem_ExtIEs_OSET 7 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          HandoverPreparationFailureIEs_OSET 8 /* Class is S1AP-PROTOCOL-IES */
#define          HandoverRequestIEs_OSET 9   /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSetupItemHOReqIEs_OSET 10 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSetupItemHOReq_ExtIEs_OSET 11 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          HandoverRequestAcknowledgeIEs_OSET 12 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABAdmittedItemIEs_OSET 13 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABAdmittedItem_ExtIEs_OSET 14 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABFailedtoSetupItemHOReqAckIEs_OSET 15 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABFailedToSetupItemHOReqAckExtIEs_OSET 16 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          HandoverFailureIEs_OSET 17  /* Class is S1AP-PROTOCOL-IES */
#define          HandoverNotifyIEs_OSET 18   /* Class is S1AP-PROTOCOL-IES */
#define          PathSwitchRequestIEs_OSET 19 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSwitchedDLItemIEs_OSET 20 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSwitchedDLItem_ExtIEs_OSET 21 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          PathSwitchRequestAcknowledgeIEs_OSET 22 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSwitchedULItemIEs_OSET 23 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSwitchedULItem_ExtIEs_OSET 24 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          PathSwitchRequestFailureIEs_OSET 25 /* Class is S1AP-PROTOCOL-IES */
#define          HandoverCancelIEs_OSET 26   /* Class is S1AP-PROTOCOL-IES */
#define          HandoverCancelAcknowledgeIEs_OSET 27 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABSetupRequestIEs_OSET 28 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSetupItemBearerSUReqIEs_OSET 29 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSetupItemBearerSUReqExtIEs_OSET 30 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABSetupResponseIEs_OSET 31 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABSetupItemBearerSUResIEs_OSET 32 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABSetupItemBearerSUResExtIEs_OSET 33 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABModifyRequestIEs_OSET 34 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeModifiedItemBearerModReqIEs_OSET 35 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeModifyItemBearerModReqExtIEs_OSET 36 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABModifyResponseIEs_OSET 37 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABModifyItemBearerModResIEs_OSET 38 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABModifyItemBearerModResExtIEs_OSET 39 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABReleaseCommandIEs_OSET 40 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABReleaseResponseIEs_OSET 41 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABReleaseItemBearerRelCompIEs_OSET 42 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABReleaseItemBearerRelCompExtIEs_OSET 43 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABReleaseIndicationIEs_OSET 44 /* Class is S1AP-PROTOCOL-IES */
#define          InitialContextSetupRequestIEs_OSET 45 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSetupItemCtxtSUReqIEs_OSET 46 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeSetupItemCtxtSUReqExtIEs_OSET 47 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          InitialContextSetupResponseIEs_OSET 48 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABSetupItemCtxtSUResIEs_OSET 49 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABSetupItemCtxtSUResExtIEs_OSET 50 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          InitialContextSetupFailureIEs_OSET 51 /* Class is S1AP-PROTOCOL-IES */
#define          PagingIEs_OSET 52           /* Class is S1AP-PROTOCOL-IES */
#define          TAIItemIEs_OSET 53          /* Class is S1AP-PROTOCOL-IES */
#define          TAIItemExtIEs_OSET 54       /* Class is S1AP-PROTOCOL-EXTENSION */
#define          UEContextReleaseRequest_IEs_OSET 55 /* Class is S1AP-PROTOCOL-IES */
#define          UEContextReleaseCommand_IEs_OSET 56 /* Class is S1AP-PROTOCOL-IES */
#define          UEContextReleaseComplete_IEs_OSET 57 /* Class is S1AP-PROTOCOL-IES */
#define          UEContextModificationRequestIEs_OSET 58 /* Class is S1AP-PROTOCOL-IES */
#define          UEContextModificationResponseIEs_OSET 59 /* Class is S1AP-PROTOCOL-IES */
#define          UEContextModificationFailureIEs_OSET 60 /* Class is S1AP-PROTOCOL-IES */
#define          UERadioCapabilityMatchRequestIEs_OSET 61 /* Class is S1AP-PROTOCOL-IES */
#define          UERadioCapabilityMatchResponseIEs_OSET 62 /* Class is S1AP-PROTOCOL-IES */
#define          DownlinkNASTransport_IEs_OSET 63 /* Class is S1AP-PROTOCOL-IES */
#define          InitialUEMessage_IEs_OSET 64 /* Class is S1AP-PROTOCOL-IES */
#define          UplinkNASTransport_IEs_OSET 65 /* Class is S1AP-PROTOCOL-IES */
#define          NASNonDeliveryIndication_IEs_OSET 66 /* Class is S1AP-PROTOCOL-IES */
#define          ResetIEs_OSET 67            /* Class is S1AP-PROTOCOL-IES */
#define          UE_associatedLogicalS1_ConnectionItemRes_OSET 68 /* Class is S1AP-PROTOCOL-IES */
#define          ResetAcknowledgeIEs_OSET 69 /* Class is S1AP-PROTOCOL-IES */
#define          UE_associatedLogicalS1_ConnectionItemResAck_OSET 70 /* Class is S1AP-PROTOCOL-IES */
#define          ErrorIndicationIEs_OSET 71  /* Class is S1AP-PROTOCOL-IES */
#define          S1SetupRequestIEs_OSET 72   /* Class is S1AP-PROTOCOL-IES */
#define          S1SetupResponseIEs_OSET 73  /* Class is S1AP-PROTOCOL-IES */
#define          S1SetupFailureIEs_OSET 74   /* Class is S1AP-PROTOCOL-IES */
#define          ENBConfigurationUpdateIEs_OSET 75 /* Class is S1AP-PROTOCOL-IES */
#define          ENBConfigurationUpdateAcknowledgeIEs_OSET 76 /* Class is S1AP-PROTOCOL-IES */
#define          ENBConfigurationUpdateFailureIEs_OSET 77 /* Class is S1AP-PROTOCOL-IES */
#define          MMEConfigurationUpdateIEs_OSET 78 /* Class is S1AP-PROTOCOL-IES */
#define          MMEConfigurationUpdateAcknowledgeIEs_OSET 79 /* Class is S1AP-PROTOCOL-IES */
#define          MMEConfigurationUpdateFailureIEs_OSET 80 /* Class is S1AP-PROTOCOL-IES */
#define          DownlinkS1cdma2000tunnellingIEs_OSET 81 /* Class is S1AP-PROTOCOL-IES */
#define          UplinkS1cdma2000tunnellingIEs_OSET 82 /* Class is S1AP-PROTOCOL-IES */
#define          UECapabilityInfoIndicationIEs_OSET 83 /* Class is S1AP-PROTOCOL-IES */
#define          ENBStatusTransferIEs_OSET 84 /* Class is S1AP-PROTOCOL-IES */
#define          MMEStatusTransferIEs_OSET 85 /* Class is S1AP-PROTOCOL-IES */
#define          TraceStartIEs_OSET 86       /* Class is S1AP-PROTOCOL-IES */
#define          TraceFailureIndicationIEs_OSET 87 /* Class is S1AP-PROTOCOL-IES */
#define          DeactivateTraceIEs_OSET 88  /* Class is S1AP-PROTOCOL-IES */
#define          CellTrafficTraceIEs_OSET 89 /* Class is S1AP-PROTOCOL-IES */
#define          LocationReportingControlIEs_OSET 90 /* Class is S1AP-PROTOCOL-IES */
#define          LocationReportingFailureIndicationIEs_OSET 91 /* Class is S1AP-PROTOCOL-IES */
#define          LocationReportIEs_OSET 92   /* Class is S1AP-PROTOCOL-IES */
#define          OverloadStartIEs_OSET 93    /* Class is S1AP-PROTOCOL-IES */
#define          OverloadStopIEs_OSET 94     /* Class is S1AP-PROTOCOL-IES */
#define          WriteReplaceWarningRequestIEs_OSET 95 /* Class is S1AP-PROTOCOL-IES */
#define          WriteReplaceWarningResponseIEs_OSET 96 /* Class is S1AP-PROTOCOL-IES */
#define          ENBDirectInformationTransferIEs_OSET 97 /* Class is S1AP-PROTOCOL-IES */
#define          MMEDirectInformationTransferIEs_OSET 98 /* Class is S1AP-PROTOCOL-IES */
#define          ENBConfigurationTransferIEs_OSET 99 /* Class is S1AP-PROTOCOL-IES */
#define          MMEConfigurationTransferIEs_OSET 100 /* Class is S1AP-PROTOCOL-IES */
#define          PrivateMessageIEs_OSET 101  /* Class is S1AP-PRIVATE-IES */
#define          KillRequestIEs_OSET 102     /* Class is S1AP-PROTOCOL-IES */
#define          KillResponseIEs_OSET 103    /* Class is S1AP-PROTOCOL-IES */
#define          PWSRestartIndicationIEs_OSET 104 /* Class is S1AP-PROTOCOL-IES */
#define          DownlinkUEAssociatedLPPaTransport_IEs_OSET 105 /* Class is S1AP-PROTOCOL-IES */
#define          UplinkUEAssociatedLPPaTransport_IEs_OSET 106 /* Class is S1AP-PROTOCOL-IES */
#define          DownlinkNonUEAssociatedLPPaTransport_IEs_OSET 107 /* Class is S1AP-PROTOCOL-IES */
#define          UplinkNonUEAssociatedLPPaTransport_IEs_OSET 108 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABModificationIndicationIEs_OSET 109 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeModifiedItemBearerModIndIEs_OSET 110 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABToBeModifiedItemBearerModInd_ExtIEs_OSET 111 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABNotToBeModifiedItemBearerModIndIEs_OSET 112 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABNotToBeModifiedItemBearerModInd_ExtIEs_OSET 113 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABModificationConfirmIEs_OSET 114 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABModifyItemBearerModConfIEs_OSET 115 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABModifyItemBearerModConfExtIEs_OSET 116 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          AllocationAndRetentionPriority_ExtIEs_OSET 117 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          Bearers_SubjectToStatusTransfer_ItemIEs_OSET 118 /* Class is S1AP-PROTOCOL-IES */
#define          Bearers_SubjectToStatusTransfer_ItemExtIEs_OSET 119 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CancelledCellinEAI_Item_ExtIEs_OSET 120 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CancelledCellinTAI_Item_ExtIEs_OSET 121 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CellID_Broadcast_Item_ExtIEs_OSET 122 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CellID_Cancelled_Item_ExtIEs_OSET 123 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CellBasedMDT_ExtIEs_OSET 124 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          Cdma2000OneXSRVCCInfo_ExtIEs_OSET 125 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CellType_ExtIEs_OSET 126    /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CGI_ExtIEs_OSET 127         /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CSG_IdList_Item_ExtIEs_OSET 128 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          COUNTvalue_ExtIEs_OSET 129  /* Class is S1AP-PROTOCOL-EXTENSION */
#define          COUNTValueExtended_ExtIEs_OSET 130 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CriticalityDiagnostics_ExtIEs_OSET 131 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CriticalityDiagnostics_IE_Item_ExtIEs_OSET 132 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          EmergencyAreaID_Broadcast_Item_ExtIEs_OSET 133 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          EmergencyAreaID_Cancelled_Item_ExtIEs_OSET 134 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CompletedCellinEAI_Item_ExtIEs_OSET 135 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          GERAN_Cell_ID_ExtIEs_OSET 136 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          GlobalENB_ID_ExtIEs_OSET 137 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ENB_StatusTransfer_TransparentContainer_ExtIEs_OSET 138 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABInformationListIEs_OSET 139 /* Class is S1AP-PROTOCOL-IES */
#define          E_RABInformationListItem_ExtIEs_OSET 140 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABItemIEs_OSET 141       /* Class is S1AP-PROTOCOL-IES */
#define          E_RABItem_ExtIEs_OSET 142   /* Class is S1AP-PROTOCOL-EXTENSION */
#define          E_RABQoSParameters_ExtIEs_OSET 143 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          EUTRAN_CGI_ExtIEs_OSET 144  /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ExpectedUEBehaviour_ExtIEs_OSET 145 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ExpectedUEActivityBehaviour_ExtIEs_OSET 146 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ForbiddenTAs_Item_ExtIEs_OSET 147 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ForbiddenLAs_Item_ExtIEs_OSET 148 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          GBR_QosInformation_ExtIEs_OSET 149 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          GUMMEI_ExtIEs_OSET 150      /* Class is S1AP-PROTOCOL-EXTENSION */
#define          HandoverRestrictionList_ExtIEs_OSET 151 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ImmediateMDT_ExtIEs_OSET 152 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          LAI_ExtIEs_OSET 153         /* Class is S1AP-PROTOCOL-EXTENSION */
#define          LastVisitedEUTRANCellInformation_ExtIEs_OSET 154 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ListeningSubframePattern_ExtIEs_OSET 155 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          LoggedMDT_ExtIEs_OSET 156   /* Class is S1AP-PROTOCOL-EXTENSION */
#define          LoggedMBSFNMDT_ExtIEs_OSET 157 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          M3Configuration_ExtIEs_OSET 158 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          M4Configuration_ExtIEs_OSET 159 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          M5Configuration_ExtIEs_OSET 160 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          MDT_Configuration_ExtIEs_OSET 161 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          MBSFN_ResultToLogInfo_ExtIEs_OSET 162 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          MutingPatternInformation_ExtIEs_OSET 163 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          M1PeriodicReporting_ExtIEs_OSET 164 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ProSeAuthorized_ExtIEs_OSET 165 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          RequestType_ExtIEs_OSET 166 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          RIMTransfer_ExtIEs_OSET 167 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          RLFReportInformation_ExtIEs_OSET 168 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          SecurityContext_ExtIEs_OSET 169 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          SONInformationReply_ExtIEs_OSET 170 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          SONConfigurationTransfer_ExtIEs_OSET 171 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          SynchronisationInformation_ExtIEs_OSET 172 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          SourceeNB_ID_ExtIEs_OSET 173 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs_OSET 174 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ServedGUMMEIsItem_ExtIEs_OSET 175 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          SupportedTAs_Item_ExtIEs_OSET 176 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TimeSynchronisationInfo_ExtIEs_OSET 177 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          S_TMSI_ExtIEs_OSET 178      /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TAIBasedMDT_ExtIEs_OSET 179 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TAI_ExtIEs_OSET 180         /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TAI_Broadcast_Item_ExtIEs_OSET 181 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TAI_Cancelled_Item_ExtIEs_OSET 182 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TABasedMDT_ExtIEs_OSET 183  /* Class is S1AP-PROTOCOL-EXTENSION */
#define          CompletedCellinTAI_Item_ExtIEs_OSET 184 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TargeteNB_ID_ExtIEs_OSET 185 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TargetRNC_ID_ExtIEs_OSET 186 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TargeteNB_ToSourceeNB_TransparentContainer_ExtIEs_OSET 187 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          M1ThresholdEventA2_ExtIEs_OSET 188 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          TraceActivation_ExtIEs_OSET 189 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          Tunnel_Information_ExtIEs_OSET 190 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          UEAggregate_MaximumBitrates_ExtIEs_OSET 191 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          UE_S1AP_ID_pair_ExtIEs_OSET 192 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          UE_associatedLogicalS1_ConnectionItemExtIEs_OSET 193 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          UESecurityCapabilities_ExtIEs_OSET 194 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          UserLocationInformation_ExtIEs_OSET 195 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          X2TNLConfigurationInfo_ExtIEs_OSET 196 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          ENBX2ExtTLA_ExtIEs_OSET 197 /* Class is S1AP-PROTOCOL-EXTENSION */
#define          MDTMode_ExtensionIE_OSET 198 /* Class is S1AP-PROTOCOL-IES */
#define          SONInformation_ExtensionIE_OSET 199 /* Class is S1AP-PROTOCOL-IES */

typedef struct ObjectID {
    unsigned short  length;
    unsigned char   *value;
} ObjectID;

typedef unsigned short  ProcedureCode;

typedef enum Criticality {
    reject = 0,
    ignore = 1,
    notify = 2
} Criticality;

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
typedef struct S1AP_ELEMENTARY_PROCEDURE {
    unsigned char   bit_mask;
#       define      SuccessfulOutcome_present 0x80
#       define      UnsuccessfulOutcome_present 0x40
#       define      criticality_present 0x20
    unsigned short  InitiatingMessage;
    unsigned short  SuccessfulOutcome;  /* optional; set in bit_mask
                                         * SuccessfulOutcome_present if
                                         * present */
    unsigned short  UnsuccessfulOutcome;  /* optional; set in bit_mask
                                           * UnsuccessfulOutcome_present if
                                           * present */
    ProcedureCode   procedureCode;
    Criticality     criticality;  /* criticality_present not set in bit_mask
                                   * implies value is ignore */
} S1AP_ELEMENTARY_PROCEDURE;

typedef struct InitiatingMessage {
    ProcedureCode   procedureCode;
    Criticality     criticality;
    OpenType        value;
} InitiatingMessage;

typedef struct SuccessfulOutcome {
    ProcedureCode   procedureCode;
    Criticality     criticality;
    OpenType        value;
} SuccessfulOutcome;

typedef struct UnsuccessfulOutcome {
    ProcedureCode   procedureCode;
    Criticality     criticality;
    OpenType        value;
} UnsuccessfulOutcome;

/* ************************************************************** */
/**/
/* Interface PDU Definition */
/**/
/* ************************************************************** */
typedef struct S1AP_PDU {
    unsigned short  choice;
#       define      initiatingMessage_chosen 1
#       define      successfulOutcome_chosen 2
#       define      unsuccessfulOutcome_chosen 3
    union {
        InitiatingMessage initiatingMessage;  /* to choose, set choice to
                                               * initiatingMessage_chosen */
        SuccessfulOutcome successfulOutcome;  /* to choose, set choice to
                                               * successfulOutcome_chosen */
        UnsuccessfulOutcome unsuccessfulOutcome;  /* to choose, set choice to
                                                * unsuccessfulOutcome_chosen */
    } u;
} S1AP_PDU;

typedef unsigned short  ProtocolIE_ID;

typedef struct ProtocolIE_Field {
    ProtocolIE_ID   id;
    Criticality     criticality;
    OpenType        value;
} ProtocolIE_Field;

typedef struct ProtocolIE_Container {
    struct ProtocolIE_Container *next;
    ProtocolIE_Field value;
} *ProtocolIE_Container;

/* ************************************************************** */
/**/
/* HANDOVER PREPARATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Handover Required */
/**/
/* ************************************************************** */
typedef struct HandoverRequired {
    struct ProtocolIE_Container *protocolIEs;
} HandoverRequired;

/* ************************************************************** */
/**/
/* Handover Command */
/**/
/* ************************************************************** */
typedef struct HandoverCommand {
    struct ProtocolIE_Container *protocolIEs;
} HandoverCommand;

typedef struct ProtocolIE_SingleContainer {
    ProtocolIE_ID   id;
    Criticality     criticality;
    OpenType        value;
} ProtocolIE_SingleContainer;

typedef struct E_RAB_IE_ContainerList {
    struct E_RAB_IE_ContainerList *next;
    ProtocolIE_SingleContainer value;
} *E_RAB_IE_ContainerList;

typedef E_RAB_IE_ContainerList E_RABSubjecttoDataForwardingList;

typedef int             E_RAB_ID;

typedef struct TransportLayerAddress {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} TransportLayerAddress;

typedef struct GTP_TEID {
    unsigned short  length;
    unsigned char   value[4];
} GTP_TEID;

typedef unsigned short  ProtocolExtensionID;

typedef struct ProtocolExtensionField {
    ProtocolExtensionID id;
    Criticality     criticality;
    OpenType        extensionValue;
} ProtocolExtensionField;

typedef struct ProtocolExtensionContainer {
    struct ProtocolExtensionContainer *next;
    ProtocolExtensionField value;
} *ProtocolExtensionContainer;

typedef struct E_RABDataForwardingItem {
    unsigned char   bit_mask;
#       define      E_RABDataForwardingItem_dL_transportLayerAddress_present 0x80
#       define      E_RABDataForwardingItem_dL_gTP_TEID_present 0x40
#       define      E_RABDataForwardingItem_uL_TransportLayerAddress_present 0x20
#       define      E_RABDataForwardingItem_uL_GTP_TEID_present 0x10
#       define      E_RABDataForwardingItem_iE_Extensions_present 0x08
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress dL_transportLayerAddress;  /* optional; set in
                                   * bit_mask
                  * E_RABDataForwardingItem_dL_transportLayerAddress_present if
                  * present */
    GTP_TEID        dL_gTP_TEID;  /* optional; set in bit_mask
                                   * E_RABDataForwardingItem_dL_gTP_TEID_present
                                   * if present */
    TransportLayerAddress uL_TransportLayerAddress;  /* optional; set in
                                   * bit_mask
                  * E_RABDataForwardingItem_uL_TransportLayerAddress_present if
                  * present */
    GTP_TEID        uL_GTP_TEID;  /* optional; set in bit_mask
                                   * E_RABDataForwardingItem_uL_GTP_TEID_present
                                   * if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * E_RABDataForwardingItem_iE_Extensions_present if
                             * present */
} E_RABDataForwardingItem;

/* ************************************************************** */
/**/
/* Handover Preparation Failure */
/**/
/* ************************************************************** */
typedef struct HandoverPreparationFailure {
    struct ProtocolIE_Container *protocolIEs;
} HandoverPreparationFailure;

/* ************************************************************** */
/**/
/* HANDOVER RESOURCE ALLOCATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Handover Request */
/**/
/* ************************************************************** */
typedef struct HandoverRequest {
    struct ProtocolIE_Container *protocolIEs;
} HandoverRequest;

typedef E_RAB_IE_ContainerList E_RABToBeSetupListHOReq;

/* Q */
typedef unsigned short  QCI;

typedef unsigned short  PriorityLevel;
#define                     spare 0U
#define                     highest 1U
#define                     lowest 14U
#define                     no_priority 15U

typedef enum Pre_emptionCapability {
    shall_not_trigger_pre_emption = 0,
    may_trigger_pre_emption = 1
} Pre_emptionCapability;

typedef enum Pre_emptionVulnerability {
    not_pre_emptable = 0,
    pre_emptable = 1
} Pre_emptionVulnerability;

typedef struct AllocationAndRetentionPriority {
    unsigned char   bit_mask;
#       define      AllocationAndRetentionPriority_iE_Extensions_present 0x80
    PriorityLevel   priorityLevel;
    Pre_emptionCapability pre_emptionCapability;
    Pre_emptionVulnerability pre_emptionVulnerability;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                      * AllocationAndRetentionPriority_iE_Extensions_present if
                      * present */
} AllocationAndRetentionPriority;

typedef ULONG_LONG      BitRate;

/* G */
typedef struct GBR_QosInformation {
    unsigned char   bit_mask;
#       define      GBR_QosInformation_iE_Extensions_present 0x80
    BitRate         e_RAB_MaximumBitrateDL;
    BitRate         e_RAB_MaximumBitrateUL;
    BitRate         e_RAB_GuaranteedBitrateDL;
    BitRate         e_RAB_GuaranteedBitrateUL;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * GBR_QosInformation_iE_Extensions_present if
                                   * present */
} GBR_QosInformation;

typedef struct E_RABLevelQoSParameters {
    unsigned char   bit_mask;
#       define      gbrQosInformation_present 0x80
#       define      E_RABLevelQoSParameters_iE_Extensions_present 0x40
    QCI             qCI;
    AllocationAndRetentionPriority allocationRetentionPriority;
    GBR_QosInformation gbrQosInformation;  /* optional; set in bit_mask
                                            * gbrQosInformation_present if
                                            * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * E_RABLevelQoSParameters_iE_Extensions_present if
                             * present */
} E_RABLevelQoSParameters;

typedef struct E_RABToBeSetupItemHOReq {
    unsigned char   bit_mask;
#       define      E_RABToBeSetupItemHOReq_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    E_RABLevelQoSParameters e_RABlevelQosParameters;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * E_RABToBeSetupItemHOReq_iE_Extensions_present if
                             * present */
} E_RABToBeSetupItemHOReq;

/* ************************************************************** */
/**/
/* Handover Request Acknowledge */
/**/
/* ************************************************************** */
typedef struct HandoverRequestAcknowledge {
    struct ProtocolIE_Container *protocolIEs;
} HandoverRequestAcknowledge;

typedef E_RAB_IE_ContainerList E_RABAdmittedList;

typedef struct E_RABAdmittedItem {
    unsigned char   bit_mask;
#       define      E_RABAdmittedItem_dL_transportLayerAddress_present 0x80
#       define      E_RABAdmittedItem_dL_gTP_TEID_present 0x40
#       define      E_RABAdmittedItem_uL_TransportLayerAddress_present 0x20
#       define      E_RABAdmittedItem_uL_GTP_TEID_present 0x10
#       define      E_RABAdmittedItem_iE_Extensions_present 0x08
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    TransportLayerAddress dL_transportLayerAddress;  /* optional; set in
                                   * bit_mask
                        * E_RABAdmittedItem_dL_transportLayerAddress_present if
                        * present */
    GTP_TEID        dL_gTP_TEID;  /* optional; set in bit_mask
                                   * E_RABAdmittedItem_dL_gTP_TEID_present if
                                   * present */
    TransportLayerAddress uL_TransportLayerAddress;  /* optional; set in
                                   * bit_mask
                        * E_RABAdmittedItem_uL_TransportLayerAddress_present if
                        * present */
    GTP_TEID        uL_GTP_TEID;  /* optional; set in bit_mask
                                   * E_RABAdmittedItem_uL_GTP_TEID_present if
                                   * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * E_RABAdmittedItem_iE_Extensions_present if
                                   * present */
} E_RABAdmittedItem;

typedef E_RAB_IE_ContainerList E_RABFailedtoSetupListHOReqAck;

typedef enum CauseRadioNetwork {
    CauseRadioNetwork_unspecified = 0,
    tx2relocoverall_expiry = 1,
    successful_handover = 2,
    release_due_to_eutran_generated_reason = 3,
    handover_cancelled = 4,
    partial_handover = 5,
    ho_failure_in_target_EPC_eNB_or_target_system = 6,
    ho_target_not_allowed = 7,
    tS1relocoverall_expiry = 8,
    tS1relocprep_expiry = 9,
    cell_not_available = 10,
    unknown_targetID = 11,
    no_radio_resources_available_in_target_cell = 12,
    unknown_mme_ue_s1ap_id = 13,
    unknown_enb_ue_s1ap_id = 14,
    unknown_pair_ue_s1ap_id = 15,
    handover_desirable_for_radio_reason = 16,
    time_critical_handover = 17,
    resource_optimisation_handover = 18,
    reduce_load_in_serving_cell = 19,
    user_inactivity = 20,
    radio_connection_with_ue_lost = 21,
    load_balancing_tau_required = 22,
    cs_fallback_triggered = 23,
    ue_not_available_for_ps_service = 24,
    radio_resources_not_available = 25,
    failure_in_radio_interface_procedure = 26,
    invalid_qos_combination = 27,
    interrat_redirection = 28,
    interaction_with_other_procedure = 29,
    unknown_E_RAB_ID = 30,
    multiple_E_RAB_ID_instances = 31,
    encryption_and_or_integrity_protection_algorithms_not_supported = 32,
    s1_intra_system_handover_triggered = 33,
    s1_inter_system_handover_triggered = 34,
    x2_handover_triggered = 35,
    redirection_towards_1xRTT = 36,
    not_supported_QCI_value = 37,
    invalid_CSG_Id = 38
} CauseRadioNetwork;

typedef enum CauseTransport {
    transport_resource_unavailable = 0,
    CauseTransport_unspecified = 1
} CauseTransport;

typedef enum CauseNas {
    normal_release = 0,
    authentication_failure = 1,
    detach = 2,
    CauseNas_unspecified = 3,
    csg_subscription_expiry = 4
} CauseNas;

typedef enum CauseProtocol {
    transfer_syntax_error = 0,
    abstract_syntax_error_reject = 1,
    abstract_syntax_error_ignore_and_notify = 2,
    message_not_compatible_with_receiver_state = 3,
    semantic_error = 4,
    abstract_syntax_error_falsely_constructed_message = 5,
    CauseProtocol_unspecified = 6
} CauseProtocol;

typedef enum CauseMisc {
    control_processing_overload = 0,
    not_enough_user_plane_processing_resources = 1,
    hardware_failure = 2,
    om_intervention = 3,
    CauseMisc_unspecified = 4,
    unknown_PLMN = 5
} CauseMisc;

typedef struct Cause {
    unsigned short  choice;
#       define      radioNetwork_chosen 1
#       define      transport_chosen 2
#       define      nas_chosen 3
#       define      protocol_chosen 4
#       define      misc_chosen 5
    union {
        CauseRadioNetwork radioNetwork;  /* to choose, set choice to
                                          * radioNetwork_chosen */
        CauseTransport  transport;  /* to choose, set choice to
                                     * transport_chosen */
        CauseNas        nas;  /* to choose, set choice to nas_chosen */
        CauseProtocol   protocol;  /* to choose, set choice to
                                    * protocol_chosen */
        CauseMisc       misc;  /* to choose, set choice to misc_chosen */
    } u;
} Cause;

typedef struct E_RABFailedToSetupItemHOReqAck {
    unsigned char   bit_mask;
#       define      E_RABFailedToSetupItemHOReqAck_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    Cause           cause;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                      * E_RABFailedToSetupItemHOReqAck_iE_Extensions_present if
                      * present */
} E_RABFailedToSetupItemHOReqAck;

/* ************************************************************** */
/**/
/* Handover Failure */
/**/
/* ************************************************************** */
typedef struct HandoverFailure {
    struct ProtocolIE_Container *protocolIEs;
} HandoverFailure;

/* ************************************************************** */
/**/
/* HANDOVER NOTIFICATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Handover Notify */
/**/
/* ************************************************************** */
typedef struct HandoverNotify {
    struct ProtocolIE_Container *protocolIEs;
} HandoverNotify;

/* ************************************************************** */
/**/
/* PATH SWITCH REQUEST ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Path Switch Request */
/**/
/* ************************************************************** */
typedef struct PathSwitchRequest {
    struct ProtocolIE_Container *protocolIEs;
} PathSwitchRequest;

typedef E_RAB_IE_ContainerList E_RABToBeSwitchedDLList;

typedef struct E_RABToBeSwitchedDLItem {
    unsigned char   bit_mask;
#       define      E_RABToBeSwitchedDLItem_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * E_RABToBeSwitchedDLItem_iE_Extensions_present if
                             * present */
} E_RABToBeSwitchedDLItem;

/* ************************************************************** */
/**/
/* Path Switch Request Acknowledge */
/**/
/* ************************************************************** */
typedef struct PathSwitchRequestAcknowledge {
    struct ProtocolIE_Container *protocolIEs;
} PathSwitchRequestAcknowledge;

typedef E_RAB_IE_ContainerList E_RABToBeSwitchedULList;

typedef struct E_RABToBeSwitchedULItem {
    unsigned char   bit_mask;
#       define      E_RABToBeSwitchedULItem_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * E_RABToBeSwitchedULItem_iE_Extensions_present if
                             * present */
} E_RABToBeSwitchedULItem;

/* ************************************************************** */
/**/
/* Path Switch Request Failure */
/**/
/* ************************************************************** */
typedef struct PathSwitchRequestFailure {
    struct ProtocolIE_Container *protocolIEs;
} PathSwitchRequestFailure;

/* ************************************************************** */
/**/
/* HANDOVER CANCEL ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Handover Cancel */
/**/
/* ************************************************************** */
typedef struct HandoverCancel {
    struct ProtocolIE_Container *protocolIEs;
} HandoverCancel;

/* ************************************************************** */
/**/
/* Handover Cancel Request Acknowledge */
/**/
/* ************************************************************** */
typedef struct HandoverCancelAcknowledge {
    struct ProtocolIE_Container *protocolIEs;
} HandoverCancelAcknowledge;

/* ************************************************************** */
/**/
/* E-RAB SETUP ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* E-RAB Setup Request */
/**/
/* ************************************************************** */
typedef struct E_RABSetupRequest {
    struct ProtocolIE_Container *protocolIEs;
} E_RABSetupRequest;

typedef struct E_RABToBeSetupListBearerSUReq {
    struct E_RABToBeSetupListBearerSUReq *next;
    ProtocolIE_SingleContainer value;
} *E_RABToBeSetupListBearerSUReq;

/* N */
typedef struct NAS_PDU {
    unsigned int    length;
    unsigned char   *value;
} NAS_PDU;

typedef struct E_RABToBeSetupItemBearerSUReq {
    unsigned char   bit_mask;
#       define      E_RABToBeSetupItemBearerSUReq_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    E_RABLevelQoSParameters e_RABlevelQoSParameters;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    NAS_PDU         nAS_PDU;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                       * E_RABToBeSetupItemBearerSUReq_iE_Extensions_present if
                       * present */
} E_RABToBeSetupItemBearerSUReq;

/* ************************************************************** */
/**/
/* E-RAB Setup Response */
/**/
/* ************************************************************** */
typedef struct E_RABSetupResponse {
    struct ProtocolIE_Container *protocolIEs;
} E_RABSetupResponse;

typedef struct E_RABSetupListBearerSURes {
    struct E_RABSetupListBearerSURes *next;
    ProtocolIE_SingleContainer value;
} *E_RABSetupListBearerSURes;

typedef struct E_RABSetupItemBearerSURes {
    unsigned char   bit_mask;
#       define      E_RABSetupItemBearerSURes_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                           * E_RABSetupItemBearerSURes_iE_Extensions_present if
                           * present */
} E_RABSetupItemBearerSURes;

/* ************************************************************** */
/**/
/* E-RAB MODIFY ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* E-RAB Modify Request */
/**/
/* ************************************************************** */
typedef struct E_RABModifyRequest {
    struct ProtocolIE_Container *protocolIEs;
} E_RABModifyRequest;

typedef struct E_RABToBeModifiedListBearerModReq {
    struct E_RABToBeModifiedListBearerModReq *next;
    ProtocolIE_SingleContainer value;
} *E_RABToBeModifiedListBearerModReq;

typedef struct E_RABToBeModifiedItemBearerModReq {
    unsigned char   bit_mask;
#       define      E_RABToBeModifiedItemBearerModReq_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    E_RABLevelQoSParameters e_RABLevelQoSParameters;
    NAS_PDU         nAS_PDU;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                   * E_RABToBeModifiedItemBearerModReq_iE_Extensions_present if
                   * present */
} E_RABToBeModifiedItemBearerModReq;

/* ************************************************************** */
/**/
/* E-RAB Modify Response */
/**/
/* ************************************************************** */
typedef struct E_RABModifyResponse {
    struct ProtocolIE_Container *protocolIEs;
} E_RABModifyResponse;

typedef struct E_RABModifyListBearerModRes {
    struct E_RABModifyListBearerModRes *next;
    ProtocolIE_SingleContainer value;
} *E_RABModifyListBearerModRes;

typedef struct E_RABModifyItemBearerModRes {
    unsigned char   bit_mask;
#       define      E_RABModifyItemBearerModRes_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * E_RABModifyItemBearerModRes_iE_Extensions_present if
                         * present */
} E_RABModifyItemBearerModRes;

/* ************************************************************** */
/**/
/* E-RAB RELEASE ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* E-RAB Release Command */
/**/
/* ************************************************************** */
typedef struct E_RABReleaseCommand {
    struct ProtocolIE_Container *protocolIEs;
} E_RABReleaseCommand;

/* ************************************************************** */
/**/
/* E-RAB Release Response */
/**/
/* ************************************************************** */
typedef struct E_RABReleaseResponse {
    struct ProtocolIE_Container *protocolIEs;
} E_RABReleaseResponse;

typedef struct E_RABReleaseListBearerRelComp {
    struct E_RABReleaseListBearerRelComp *next;
    ProtocolIE_SingleContainer value;
} *E_RABReleaseListBearerRelComp;

typedef struct E_RABReleaseItemBearerRelComp {
    unsigned char   bit_mask;
#       define      E_RABReleaseItemBearerRelComp_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                       * E_RABReleaseItemBearerRelComp_iE_Extensions_present if
                       * present */
} E_RABReleaseItemBearerRelComp;

/* ************************************************************** */
/**/
/* E-RAB RELEASE INDICATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* E-RAB Release Indication */
/**/
/* ************************************************************** */
typedef struct E_RABReleaseIndication {
    struct ProtocolIE_Container *protocolIEs;
} E_RABReleaseIndication;

/* ************************************************************** */
/**/
/* INITIAL CONTEXT SETUP ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* Initial Context Setup Request */
/**/
/* ************************************************************** */
typedef struct InitialContextSetupRequest {
    struct ProtocolIE_Container *protocolIEs;
} InitialContextSetupRequest;

typedef struct E_RABToBeSetupListCtxtSUReq {
    struct E_RABToBeSetupListCtxtSUReq *next;
    ProtocolIE_SingleContainer value;
} *E_RABToBeSetupListCtxtSUReq;

typedef struct E_RABToBeSetupItemCtxtSUReq {
    unsigned char   bit_mask;
#       define      nAS_PDU_present 0x80
#       define      E_RABToBeSetupItemCtxtSUReq_iE_Extensions_present 0x40
    E_RAB_ID        e_RAB_ID;
    E_RABLevelQoSParameters e_RABlevelQoSParameters;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    NAS_PDU         nAS_PDU;  /* optional; set in bit_mask nAS_PDU_present if
                               * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * E_RABToBeSetupItemCtxtSUReq_iE_Extensions_present if
                         * present */
} E_RABToBeSetupItemCtxtSUReq;

/* ************************************************************** */
/**/
/* Initial Context Setup Response */
/**/
/* ************************************************************** */
typedef struct InitialContextSetupResponse {
    struct ProtocolIE_Container *protocolIEs;
} InitialContextSetupResponse;

typedef struct E_RABSetupListCtxtSURes {
    struct E_RABSetupListCtxtSURes *next;
    ProtocolIE_SingleContainer value;
} *E_RABSetupListCtxtSURes;

typedef struct E_RABSetupItemCtxtSURes {
    unsigned char   bit_mask;
#       define      E_RABSetupItemCtxtSURes_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        gTP_TEID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * E_RABSetupItemCtxtSURes_iE_Extensions_present if
                             * present */
} E_RABSetupItemCtxtSURes;

/* ************************************************************** */
/**/
/* Initial Context Setup Failure */
/**/
/* ************************************************************** */
typedef struct InitialContextSetupFailure {
    struct ProtocolIE_Container *protocolIEs;
} InitialContextSetupFailure;

/* ************************************************************** */
/**/
/* PAGING ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Paging */
/**/
/* ************************************************************** */
typedef struct Paging {
    struct ProtocolIE_Container *protocolIEs;
} Paging;

typedef struct TAIList {
    struct TAIList  *next;
    ProtocolIE_SingleContainer value;
} *TAIList;

typedef struct TBCD_STRING {
    unsigned short  length;
    unsigned char   value[3];
} TBCD_STRING;

typedef TBCD_STRING     PLMNidentity;

/* T */
typedef struct TAC {
    unsigned short  length;
    unsigned char   value[2];
} TAC;

typedef struct TAI {
    unsigned char   bit_mask;
#       define      TAI_iE_Extensions_present 0x80
    PLMNidentity    pLMNidentity;
    TAC             tAC;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask TAI_iE_Extensions_present if
                                   * present */
} TAI;

typedef struct TAIItem {
    unsigned char   bit_mask;
#       define      TAIItem_iE_Extensions_present 0x80
    TAI             tAI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask TAIItem_iE_Extensions_present if
                                   * present */
} TAIItem;

/* ************************************************************** */
/**/
/* UE CONTEXT RELEASE ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* UE Context Release Request */
/**/
/* ************************************************************** */
typedef struct UEContextReleaseRequest {
    struct ProtocolIE_Container *protocolIEs;
} UEContextReleaseRequest;

/* ************************************************************** */
/**/
/* UE Context Release Command */
/**/
/* ************************************************************** */
typedef struct UEContextReleaseCommand {
    struct ProtocolIE_Container *protocolIEs;
} UEContextReleaseCommand;

/* ************************************************************** */
/**/
/* UE Context Release Complete */
/**/
/* ************************************************************** */
typedef struct UEContextReleaseComplete {
    struct ProtocolIE_Container *protocolIEs;
} UEContextReleaseComplete;

/* ************************************************************** */
/**/
/* UE CONTEXT MODIFICATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* UE Context Modification Request */
/**/
/* ************************************************************** */
typedef struct UEContextModificationRequest {
    struct ProtocolIE_Container *protocolIEs;
} UEContextModificationRequest;

/* ************************************************************** */
/**/
/* UE Context Modification Response */
/**/
/* ************************************************************** */
typedef struct UEContextModificationResponse {
    struct ProtocolIE_Container *protocolIEs;
} UEContextModificationResponse;

/* ************************************************************** */
/**/
/* UE Context Modification Failure */
/**/
/* ************************************************************** */
typedef struct UEContextModificationFailure {
    struct ProtocolIE_Container *protocolIEs;
} UEContextModificationFailure;

/* ************************************************************** */
/**/
/* UE RADIO CAPABILITY MATCH ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* UE Radio Capability Match Request */
/**/
/* ************************************************************** */
typedef struct UERadioCapabilityMatchRequest {
    struct ProtocolIE_Container *protocolIEs;
} UERadioCapabilityMatchRequest;

/* ************************************************************** */
/**/
/* UE Radio Capability Match Response */
/**/
/* ************************************************************** */
typedef struct UERadioCapabilityMatchResponse {
    struct ProtocolIE_Container *protocolIEs;
} UERadioCapabilityMatchResponse;

/* ************************************************************** */
/**/
/* NAS TRANSPORT ELEMENTARY PROCEDURES */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* DOWNLINK NAS TRANSPORT */
/**/
/* ************************************************************** */
typedef struct DownlinkNASTransport {
    struct ProtocolIE_Container *protocolIEs;
} DownlinkNASTransport;

/* ************************************************************** */
/**/
/* INITIAL UE MESSAGE */
/**/
/* ************************************************************** */
typedef struct InitialUEMessage {
    struct ProtocolIE_Container *protocolIEs;
} InitialUEMessage;

/* ************************************************************** */
/**/
/* UPLINK NAS TRANSPORT */
/**/
/* ************************************************************** */
typedef struct UplinkNASTransport {
    struct ProtocolIE_Container *protocolIEs;
} UplinkNASTransport;

/* ************************************************************** */
/**/
/* NAS NON DELIVERY INDICATION */
/**/
/* ************************************************************** */
typedef struct NASNonDeliveryIndication {
    struct ProtocolIE_Container *protocolIEs;
} NASNonDeliveryIndication;

/* ************************************************************** */
/**/
/* RESET ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Reset */
/**/
/* ************************************************************** */
typedef struct Reset {
    struct ProtocolIE_Container *protocolIEs;
} Reset;

typedef enum ResetAll {
    reset_all = 0
} ResetAll;

typedef struct ResetType {
    unsigned short  choice;
#       define      s1_Interface_chosen 1
#       define      partOfS1_Interface_chosen 2
    union {
        ResetAll        s1_Interface;  /* to choose, set choice to
                                        * s1_Interface_chosen */
        struct UE_associatedLogicalS1_ConnectionListRes *partOfS1_Interface;  
                                        /* to choose, set choice to
                                         * partOfS1_Interface_chosen */
    } u;
} ResetType;

typedef struct UE_associatedLogicalS1_ConnectionListRes {
    struct UE_associatedLogicalS1_ConnectionListRes *next;
    ProtocolIE_SingleContainer value;
} *UE_associatedLogicalS1_ConnectionListRes;

/* ************************************************************** */
/**/
/* Reset Acknowledge */
/**/
/* ************************************************************** */
typedef struct ResetAcknowledge {
    struct ProtocolIE_Container *protocolIEs;
} ResetAcknowledge;

typedef struct UE_associatedLogicalS1_ConnectionListResAck {
    struct UE_associatedLogicalS1_ConnectionListResAck *next;
    ProtocolIE_SingleContainer value;
} *UE_associatedLogicalS1_ConnectionListResAck;

/* ************************************************************** */
/**/
/* ERROR INDICATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Error Indication */
/**/
/* ************************************************************** */
typedef struct ErrorIndication {
    struct ProtocolIE_Container *protocolIEs;
} ErrorIndication;

/* ************************************************************** */
/**/
/* S1 SETUP ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* S1 Setup Request */
/**/
/* ************************************************************** */
typedef struct S1SetupRequest {
    struct ProtocolIE_Container *protocolIEs;
} S1SetupRequest;

/* ************************************************************** */
/**/
/* S1 Setup Response */
/**/
/* ************************************************************** */
typedef struct S1SetupResponse {
    struct ProtocolIE_Container *protocolIEs;
} S1SetupResponse;

/* ************************************************************** */
/**/
/* S1 Setup Failure */
/**/
/* ************************************************************** */
typedef struct S1SetupFailure {
    struct ProtocolIE_Container *protocolIEs;
} S1SetupFailure;

/* ************************************************************** */
/**/
/* ENB CONFIGURATION UPDATE ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* eNB Configuration Update */
/**/
/* ************************************************************** */
typedef struct ENBConfigurationUpdate {
    struct ProtocolIE_Container *protocolIEs;
} ENBConfigurationUpdate;

/* ************************************************************** */
/**/
/* eNB Configuration Update Acknowledge */
/**/
/* ************************************************************** */
typedef struct ENBConfigurationUpdateAcknowledge {
    struct ProtocolIE_Container *protocolIEs;
} ENBConfigurationUpdateAcknowledge;

/* ************************************************************** */
/**/
/* eNB Configuration Update Failure */
/**/
/* ************************************************************** */
typedef struct ENBConfigurationUpdateFailure {
    struct ProtocolIE_Container *protocolIEs;
} ENBConfigurationUpdateFailure;

/* ************************************************************** */
/**/
/* MME CONFIGURATION UPDATE ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* MME Configuration Update */
/**/
/* ************************************************************** */
typedef struct MMEConfigurationUpdate {
    struct ProtocolIE_Container *protocolIEs;
} MMEConfigurationUpdate;

/* ************************************************************** */
/**/
/* MME Configuration Update Acknowledge */
/**/
/* ************************************************************** */
typedef struct MMEConfigurationUpdateAcknowledge {
    struct ProtocolIE_Container *protocolIEs;
} MMEConfigurationUpdateAcknowledge;

/* ************************************************************** */
/**/
/* MME Configuration Update Failure */
/**/
/* ************************************************************** */
typedef struct MMEConfigurationUpdateFailure {
    struct ProtocolIE_Container *protocolIEs;
} MMEConfigurationUpdateFailure;

/* ************************************************************** */
/**/
/* DOWNLINK S1 CDMA2000 TUNNELLING ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Downlink S1 CDMA2000 Tunnelling */
/**/
/* ************************************************************** */
typedef struct DownlinkS1cdma2000tunnelling {
    struct ProtocolIE_Container *protocolIEs;
} DownlinkS1cdma2000tunnelling;

/* ************************************************************** */
/**/
/* UPLINK S1 CDMA2000 TUNNELLING ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Uplink S1 CDMA2000 Tunnelling */
/**/
/* ************************************************************** */
typedef struct UplinkS1cdma2000tunnelling {
    struct ProtocolIE_Container *protocolIEs;
} UplinkS1cdma2000tunnelling;

/* ************************************************************** */
/**/
/* UE CAPABILITY INFO INDICATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* UE Capability Info Indication */
/**/
/* ************************************************************** */
typedef struct UECapabilityInfoIndication {
    struct ProtocolIE_Container *protocolIEs;
} UECapabilityInfoIndication;

/* ************************************************************** */
/**/
/* eNB STATUS TRANSFER ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* eNB Status Transfer */
/**/
/* ************************************************************** */
typedef struct ENBStatusTransfer {
    struct ProtocolIE_Container *protocolIEs;
} ENBStatusTransfer;

/* ************************************************************** */
/**/
/* MME STATUS TRANSFER ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* MME Status Transfer */
/**/
/* ************************************************************** */
typedef struct MMEStatusTransfer {
    struct ProtocolIE_Container *protocolIEs;
} MMEStatusTransfer;

/* ************************************************************** */
/**/
/* TRACE ELEMENTARY PROCEDURES */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* Trace Start */
/**/
/* ************************************************************** */
typedef struct TraceStart {
    struct ProtocolIE_Container *protocolIEs;
} TraceStart;

/* ************************************************************** */
/**/
/* Trace Failure Indication */
/**/
/* ************************************************************** */
typedef struct TraceFailureIndication {
    struct ProtocolIE_Container *protocolIEs;
} TraceFailureIndication;

/* ************************************************************** */
/**/
/* DEACTIVATE TRACE ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Deactivate Trace */
/**/
/* ************************************************************** */
typedef struct DeactivateTrace {
    struct ProtocolIE_Container *protocolIEs;
} DeactivateTrace;

/* ************************************************************** */
/**/
/* CELL TRAFFIC TRACE ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Cell Traffic Trace */
/**/
/* ************************************************************** */
typedef struct CellTrafficTrace {
    struct ProtocolIE_Container *protocolIEs;
} CellTrafficTrace;

/* ************************************************************** */
/**/
/* LOCATION ELEMENTARY PROCEDURES */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Location Reporting Control */
/**/
/* ************************************************************** */
typedef struct LocationReportingControl {
    struct ProtocolIE_Container *protocolIEs;
} LocationReportingControl;

/* ************************************************************** */
/**/
/* Location Report Failure Indication */
/**/
/* ************************************************************** */
typedef struct LocationReportingFailureIndication {
    struct ProtocolIE_Container *protocolIEs;
} LocationReportingFailureIndication;

/* ************************************************************** */
/**/
/* Location Report */
/**/
/* ************************************************************** */
typedef struct LocationReport {
    struct ProtocolIE_Container *protocolIEs;
} LocationReport;

/* ************************************************************** */
/**/
/* OVERLOAD ELEMENTARY PROCEDURES */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Overload Start */
/**/
/* ************************************************************** */
typedef struct OverloadStart {
    struct ProtocolIE_Container *protocolIEs;
} OverloadStart;

/* ************************************************************** */
/**/
/* Overload Stop */
/**/
/* ************************************************************** */
typedef struct OverloadStop {
    struct ProtocolIE_Container *protocolIEs;
} OverloadStop;

/* ************************************************************** */
/**/
/* WRITE-REPLACE WARNING ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* Write-Replace Warning Request */
/**/
/* ************************************************************** */
typedef struct WriteReplaceWarningRequest {
    struct ProtocolIE_Container *protocolIEs;
} WriteReplaceWarningRequest;

/* ************************************************************** */
/**/
/* Write-Replace Warning Response */
/**/
/* ************************************************************** */
typedef struct WriteReplaceWarningResponse {
    struct ProtocolIE_Container *protocolIEs;
} WriteReplaceWarningResponse;

/* ************************************************************** */
/**/
/* eNB DIRECT INFORMATION TRANSFER ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* eNB Direct Information Transfer */
/**/
/* ************************************************************** */
typedef struct ENBDirectInformationTransfer {
    struct ProtocolIE_Container *protocolIEs;
} ENBDirectInformationTransfer;

typedef struct RIMInformation {
    unsigned int    length;
    unsigned char   *value;
} RIMInformation;

/* L */
typedef struct LAC {
    unsigned short  length;
    unsigned char   value[2];
} LAC;

typedef struct LAI {
    unsigned char   bit_mask;
#       define      LAI_iE_Extensions_present 0x80
    PLMNidentity    pLMNidentity;
    LAC             lAC;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask LAI_iE_Extensions_present if
                                   * present */
} LAI;

typedef struct RAC {
    unsigned short  length;
    unsigned char   value[1];
} RAC;

typedef struct CI {
    unsigned short  length;
    unsigned char   value[2];
} CI;

typedef struct GERAN_Cell_ID {
    unsigned char   bit_mask;
#       define      GERAN_Cell_ID_iE_Extensions_present 0x80
    LAI             lAI;
    RAC             rAC;
    CI              cI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * GERAN_Cell_ID_iE_Extensions_present if
                                   * present */
} GERAN_Cell_ID;

typedef unsigned short  RNC_ID;

typedef unsigned short  ExtendedRNC_ID;

typedef struct TargetRNC_ID {
    unsigned char   bit_mask;
#       define      TargetRNC_ID_rAC_present 0x80
#       define      extendedRNC_ID_present 0x40
#       define      TargetRNC_ID_iE_Extensions_present 0x20
    LAI             lAI;
    RAC             rAC;  /* optional; set in bit_mask TargetRNC_ID_rAC_present
                           * if present */
    RNC_ID          rNC_ID;
    ExtendedRNC_ID  extendedRNC_ID;  /* optional; set in bit_mask
                                      * extendedRNC_ID_present if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask TargetRNC_ID_iE_Extensions_present
                                   * if present */
} TargetRNC_ID;

typedef struct RIMRoutingAddress {
    unsigned short  choice;
#       define      gERAN_Cell_ID_chosen 1
#       define      RIMRoutingAddress_targetRNC_ID_chosen 2
#       define      eHRPD_Sector_ID_chosen 3
    union {
        GERAN_Cell_ID   gERAN_Cell_ID;  /* to choose, set choice to
                                         * gERAN_Cell_ID_chosen */
        TargetRNC_ID    targetRNC_ID;  /* extension #1; to choose, set choice to
                                     * RIMRoutingAddress_targetRNC_ID_chosen */
        struct _octet1 {
            unsigned short  length;
            unsigned char   value[16];
        } eHRPD_Sector_ID;  /* extension #2; to choose, set choice to
                             * eHRPD_Sector_ID_chosen */
    } u;
} RIMRoutingAddress;

typedef struct RIMTransfer {
    unsigned char   bit_mask;
#       define      rIMRoutingAddress_present 0x80
#       define      RIMTransfer_iE_Extensions_present 0x40
    RIMInformation  rIMInformation;
    RIMRoutingAddress rIMRoutingAddress;  /* optional; set in bit_mask
                                           * rIMRoutingAddress_present if
                                           * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask RIMTransfer_iE_Extensions_present
                                   * if present */
} RIMTransfer;

typedef struct Inter_SystemInformationTransferType {
    unsigned short  choice;
#       define      rIMTransfer_chosen 1
    union {
        RIMTransfer     rIMTransfer;  /* to choose, set choice to
                                       * rIMTransfer_chosen */
    } u;
} Inter_SystemInformationTransferType;

/* ************************************************************** */
/**/
/* MME DIRECT INFORMATION TRANSFER ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* MME Direct Information Transfer */
/**/
/* ************************************************************** */
typedef struct MMEDirectInformationTransfer {
    struct ProtocolIE_Container *protocolIEs;
} MMEDirectInformationTransfer;

/* ************************************************************** */
/**/
/* eNB CONFIGURATION TRANSFER ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* eNB Configuration Transfer */
/**/
/* ************************************************************** */
typedef struct ENBConfigurationTransfer {
    struct ProtocolIE_Container *protocolIEs;
} ENBConfigurationTransfer;

/* ************************************************************** */
/**/
/* MME CONFIGURATION TRANSFER ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* MME Configuration Transfer */
/**/
/* ************************************************************** */
typedef struct MMEConfigurationTransfer {
    struct ProtocolIE_Container *protocolIEs;
} MMEConfigurationTransfer;

typedef struct PrivateIE_ID {
    unsigned short  choice;
#       define      local_chosen 1
#       define      global_chosen 2
    union {
        unsigned short  local;  /* to choose, set choice to local_chosen */
        ObjectID        global;  /* to choose, set choice to global_chosen */
    } u;
} PrivateIE_ID;

typedef struct PrivateIE_Field {
    PrivateIE_ID    id;
    Criticality     criticality;
    OpenType        value;
} PrivateIE_Field;

typedef struct PrivateIE_Container {
    struct PrivateIE_Container *next;
    PrivateIE_Field value;
} *PrivateIE_Container;

/* ************************************************************** */
/**/
/* PRIVATE MESSAGE ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Private Message */
/**/
/* ************************************************************** */
typedef struct PrivateMessage {
    struct PrivateIE_Container *privateIEs;
} PrivateMessage;

/* ************************************************************** */
/**/
/* KILL PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* Kill Request */
/**/
/* ************************************************************** */
typedef struct KillRequest {
    struct ProtocolIE_Container *protocolIEs;
} KillRequest;

/* ************************************************************** */
/**/
/* Kill Response */
/**/
/* ************************************************************** */
typedef struct KillResponse {
    struct ProtocolIE_Container *protocolIEs;
} KillResponse;

/* ************************************************************** */
/**/
/* PWS RESTART INDICATION PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* PWS Restart Indication */
/**/
/* ************************************************************** */
typedef struct PWSRestartIndication {
    struct ProtocolIE_Container *protocolIEs;
} PWSRestartIndication;

/* ************************************************************** */
/**/
/* LPPA TRANSPORT ELEMENTARY PROCEDURES */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* DOWNLINK UE ASSOCIATED LPPA TRANSPORT */
/**/
/* ************************************************************** */
typedef struct DownlinkUEAssociatedLPPaTransport {
    struct ProtocolIE_Container *protocolIEs;
} DownlinkUEAssociatedLPPaTransport;

/* ************************************************************** */
/**/
/* UPLINK UE ASSOCIATED LPPA TRANSPORT */
/**/
/* ************************************************************** */
typedef struct UplinkUEAssociatedLPPaTransport {
    struct ProtocolIE_Container *protocolIEs;
} UplinkUEAssociatedLPPaTransport;

/* ************************************************************** */
/**/
/* DOWNLINK NON UE ASSOCIATED LPPA TRANSPORT */
/**/
/* ************************************************************** */
typedef struct DownlinkNonUEAssociatedLPPaTransport {
    struct ProtocolIE_Container *protocolIEs;
} DownlinkNonUEAssociatedLPPaTransport;

/* ************************************************************** */
/**/
/* UPLINK NON UE ASSOCIATED LPPA TRANSPORT */
/**/
/* ************************************************************** */
typedef struct UplinkNonUEAssociatedLPPaTransport {
    struct ProtocolIE_Container *protocolIEs;
} UplinkNonUEAssociatedLPPaTransport;

/* ************************************************************** */
/**/
/* E-RAB MODIFICATION INDICATION ELEMENTARY PROCEDURE */
/**/
/* ************************************************************** */

/* ************************************************************** */
/**/
/* E-RAB Modification Indication */
/**/
/* ************************************************************** */
typedef struct E_RABModificationIndication {
    struct ProtocolIE_Container *protocolIEs;
} E_RABModificationIndication;

typedef E_RAB_IE_ContainerList E_RABToBeModifiedListBearerModInd;

typedef struct E_RABToBeModifiedItemBearerModInd {
    unsigned char   bit_mask;
#       define      E_RABToBeModifiedItemBearerModInd_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        dL_GTP_TEID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                   * E_RABToBeModifiedItemBearerModInd_iE_Extensions_present if
                   * present */
} E_RABToBeModifiedItemBearerModInd;

typedef E_RAB_IE_ContainerList E_RABNotToBeModifiedListBearerModInd;

typedef struct E_RABNotToBeModifiedItemBearerModInd {
    unsigned char   bit_mask;
#       define      E_RABNotToBeModifiedItemBearerModInd_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        dL_GTP_TEID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                * E_RABNotToBeModifiedItemBearerModInd_iE_Extensions_present if
                * present */
} E_RABNotToBeModifiedItemBearerModInd;

/* ************************************************************** */
/**/
/* E-RAB Modification Confirm */
/**/
/* ************************************************************** */
typedef struct E_RABModificationConfirm {
    struct ProtocolIE_Container *protocolIEs;
} E_RABModificationConfirm;

typedef struct E_RABModifyListBearerModConf {
    struct E_RABModifyListBearerModConf *next;
    ProtocolIE_SingleContainer value;
} *E_RABModifyListBearerModConf;

typedef struct E_RABModifyItemBearerModConf {
    unsigned char   bit_mask;
#       define      E_RABModifyItemBearerModConf_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                        * E_RABModifyItemBearerModConf_iE_Extensions_present if
                        * present */
} E_RABModifyItemBearerModConf;

typedef struct CellBasedMDT {
    unsigned char   bit_mask;
#       define      CellBasedMDT_iE_Extensions_present 0x80
    struct CellIdListforMDT *cellIdListforMDT;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask CellBasedMDT_iE_Extensions_present
                                   * if present */
} CellBasedMDT;

typedef struct TABasedMDT {
    unsigned char   bit_mask;
#       define      TABasedMDT_iE_Extensions_present 0x80
    struct TAListforMDT *tAListforMDT;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask TABasedMDT_iE_Extensions_present
                                   * if present */
} TABasedMDT;

typedef struct TAIBasedMDT {
    unsigned char   bit_mask;
#       define      TAIBasedMDT_iE_Extensions_present 0x80
    struct TAIListforMDT *tAIListforMDT;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask TAIBasedMDT_iE_Extensions_present
                                   * if present */
} TAIBasedMDT;

/* A */
typedef struct AreaScopeOfMDT {
    unsigned short  choice;
#       define      cellBased_chosen 1
#       define      tABased_chosen 2
#       define      pLMNWide_chosen 3
#       define      tAIBased_chosen 4
    union {
        CellBasedMDT    cellBased;  /* to choose, set choice to
                                     * cellBased_chosen */
        TABasedMDT      tABased;  /* to choose, set choice to tABased_chosen */
        Nulltype        pLMNWide;  /* to choose, set choice to
                                    * pLMNWide_chosen */
        TAIBasedMDT     tAIBased;  /* extension #1; to choose, set choice to
                                    * tAIBased_chosen */
    } u;
} AreaScopeOfMDT;

/* B */
typedef struct Bearers_SubjectToStatusTransferList {
    struct Bearers_SubjectToStatusTransferList *next;
    ProtocolIE_SingleContainer value;
} *Bearers_SubjectToStatusTransferList;

typedef unsigned short  PDCP_SN;

typedef unsigned int    HFN;

typedef struct COUNTvalue {
    unsigned char   bit_mask;
#       define      COUNTvalue_iE_Extensions_present 0x80
    PDCP_SN         pDCP_SN;
    HFN             hFN;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask COUNTvalue_iE_Extensions_present
                                   * if present */
} COUNTvalue;

/* R */
typedef struct ReceiveStatusofULPDCPSDUs {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} ReceiveStatusofULPDCPSDUs;

typedef struct Bearers_SubjectToStatusTransfer_Item {
    unsigned char   bit_mask;
#       define      receiveStatusofULPDCPSDUs_present 0x80
#       define      Bearers_SubjectToStatusTransfer_Item_iE_Extensions_present 0x40
    E_RAB_ID        e_RAB_ID;
    COUNTvalue      uL_COUNTvalue;
    COUNTvalue      dL_COUNTvalue;
    ReceiveStatusofULPDCPSDUs receiveStatusofULPDCPSDUs;  /* optional; set in
                                   * bit_mask receiveStatusofULPDCPSDUs_present
                                   * if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                * Bearers_SubjectToStatusTransfer_Item_iE_Extensions_present if
                * present */
} Bearers_SubjectToStatusTransfer_Item;

typedef struct BPLMNs {
    struct BPLMNs   *next;
    PLMNidentity    value;
} *BPLMNs;

typedef struct BroadcastCancelledAreaList {
    unsigned short  choice;
#       define      cellID_Cancelled_chosen 1
#       define      tAI_Cancelled_chosen 2
#       define      emergencyAreaID_Cancelled_chosen 3
    union {
        struct CellID_Cancelled *cellID_Cancelled;  /* to choose, set choice to
                                                   * cellID_Cancelled_chosen */
        struct TAI_Cancelled *tAI_Cancelled;  /* to choose, set choice to
                                               * tAI_Cancelled_chosen */
        struct EmergencyAreaID_Cancelled *emergencyAreaID_Cancelled;  /* to
                                   * choose, set choice to
                                   * emergencyAreaID_Cancelled_chosen */
    } u;
} BroadcastCancelledAreaList;

typedef struct BroadcastCompletedAreaList {
    unsigned short  choice;
#       define      cellID_Broadcast_chosen 1
#       define      tAI_Broadcast_chosen 2
#       define      emergencyAreaID_Broadcast_chosen 3
    union {
        struct CellID_Broadcast *cellID_Broadcast;  /* to choose, set choice to
                                                   * cellID_Broadcast_chosen */
        struct TAI_Broadcast *tAI_Broadcast;  /* to choose, set choice to
                                               * tAI_Broadcast_chosen */
        struct EmergencyAreaID_Broadcast *emergencyAreaID_Broadcast;  /* to
                                   * choose, set choice to
                                   * emergencyAreaID_Broadcast_chosen */
    } u;
} BroadcastCompletedAreaList;

typedef struct CellIdentity {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} CellIdentity;

typedef struct EUTRAN_CGI {
    unsigned char   bit_mask;
#       define      EUTRAN_CGI_iE_Extensions_present 0x80
    PLMNidentity    pLMNidentity;
    CellIdentity    cell_ID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask EUTRAN_CGI_iE_Extensions_present
                                   * if present */
} EUTRAN_CGI;

typedef unsigned short  NumberOfBroadcasts;

typedef struct CancelledCellinEAI_Item {
    unsigned char   bit_mask;
#       define      CancelledCellinEAI_Item_iE_Extensions_present 0x80
    EUTRAN_CGI      eCGI;
    NumberOfBroadcasts numberOfBroadcasts;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * CancelledCellinEAI_Item_iE_Extensions_present if
                             * present */
} CancelledCellinEAI_Item;

/* C */
typedef struct CancelledCellinEAI {
    struct CancelledCellinEAI *next;
    CancelledCellinEAI_Item value;
} *CancelledCellinEAI;

typedef struct CancelledCellinTAI_Item {
    unsigned char   bit_mask;
#       define      CancelledCellinTAI_Item_iE_Extensions_present 0x80
    EUTRAN_CGI      eCGI;
    NumberOfBroadcasts numberOfBroadcasts;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * CancelledCellinTAI_Item_iE_Extensions_present if
                             * present */
} CancelledCellinTAI_Item;

typedef struct CancelledCellinTAI {
    struct CancelledCellinTAI *next;
    CancelledCellinTAI_Item value;
} *CancelledCellinTAI;

typedef enum CellAccessMode {
    hybrid = 0
} CellAccessMode;

typedef struct CellID_Broadcast_Item {
    unsigned char   bit_mask;
#       define      CellID_Broadcast_Item_iE_Extensions_present 0x80
    EUTRAN_CGI      eCGI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * CellID_Broadcast_Item_iE_Extensions_present
                                   * if present */
} CellID_Broadcast_Item;

typedef struct CellID_Broadcast {
    struct CellID_Broadcast *next;
    CellID_Broadcast_Item value;
} *CellID_Broadcast;

typedef struct CellID_Cancelled_Item {
    unsigned char   bit_mask;
#       define      CellID_Cancelled_Item_iE_Extensions_present 0x80
    EUTRAN_CGI      eCGI;
    NumberOfBroadcasts numberOfBroadcasts;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * CellID_Cancelled_Item_iE_Extensions_present
                                   * if present */
} CellID_Cancelled_Item;

typedef struct CellID_Cancelled {
    struct CellID_Cancelled *next;
    CellID_Cancelled_Item value;
} *CellID_Cancelled;

typedef struct CellIdListforMDT {
    struct CellIdListforMDT *next;
    EUTRAN_CGI      value;
} *CellIdListforMDT;

typedef struct Cdma2000PDU {
    unsigned int    length;
    unsigned char   *value;
} Cdma2000PDU;

typedef enum Cdma2000RATType {
    hRPD = 0,
    onexRTT = 1
} Cdma2000RATType;

typedef struct Cdma2000SectorID {
    unsigned int    length;
    unsigned char   *value;
} Cdma2000SectorID;

typedef enum Cdma2000HOStatus {
    hOSuccess = 0,
    hOFailure = 1
} Cdma2000HOStatus;

typedef enum Cdma2000HORequiredIndication {
    Cdma2000HORequiredIndication_true = 0
} Cdma2000HORequiredIndication;

typedef struct Cdma2000OneXMEID {
    unsigned int    length;
    unsigned char   *value;
} Cdma2000OneXMEID;

typedef struct Cdma2000OneXMSI {
    unsigned int    length;
    unsigned char   *value;
} Cdma2000OneXMSI;

typedef struct Cdma2000OneXPilot {
    unsigned int    length;
    unsigned char   *value;
} Cdma2000OneXPilot;

typedef struct Cdma2000OneXSRVCCInfo {
    unsigned char   bit_mask;
#       define      Cdma2000OneXSRVCCInfo_iE_Extensions_present 0x80
    Cdma2000OneXMEID cdma2000OneXMEID;
    Cdma2000OneXMSI cdma2000OneXMSI;
    Cdma2000OneXPilot cdma2000OneXPilot;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * Cdma2000OneXSRVCCInfo_iE_Extensions_present
                                   * if present */
} Cdma2000OneXSRVCCInfo;

typedef struct Cdma2000OneXRAND {
    unsigned int    length;
    unsigned char   *value;
} Cdma2000OneXRAND;

typedef enum Cell_Size {
    verysmall = 0,
    CS_small = 1,
    Cell_Size_medium = 2,
    large = 3
} Cell_Size;

typedef struct CellType {
    unsigned char   bit_mask;
#       define      CellType_iE_Extensions_present 0x80
    Cell_Size       cell_Size;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask CellType_iE_Extensions_present if
                                   * present */
} CellType;

typedef struct CGI {
    unsigned char   bit_mask;
#       define      CGI_rAC_present 0x80
#       define      CGI_iE_Extensions_present 0x40
    PLMNidentity    pLMNidentity;
    LAC             lAC;
    CI              cI;
    RAC             rAC;  /* optional; set in bit_mask CGI_rAC_present if
                           * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask CGI_iE_Extensions_present if
                                   * present */
} CGI;

typedef enum CNDomain {
    ps = 0,
    cs = 1
} CNDomain;

typedef enum ConcurrentWarningMessageIndicator {
    ConcurrentWarningMessageIndicator_true = 0
} ConcurrentWarningMessageIndicator;

typedef struct Correlation_ID {
    unsigned short  length;
    unsigned char   value[4];
} Correlation_ID;

typedef enum CSFallbackIndicator {
    cs_fallback_required = 0,
    cs_fallback_high_priority = 1
} CSFallbackIndicator;

typedef enum AdditionalCSFallbackIndicator {
    no_restriction = 0,
    restriction = 1
} AdditionalCSFallbackIndicator;

typedef struct CSG_Id {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} CSG_Id;

typedef struct CSG_IdList_Item {
    unsigned char   bit_mask;
#       define      CSG_IdList_Item_iE_Extensions_present 0x80
    CSG_Id          cSG_Id;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * CSG_IdList_Item_iE_Extensions_present if
                                   * present */
} CSG_IdList_Item;

typedef struct CSG_IdList {
    struct CSG_IdList *next;
    CSG_IdList_Item value;
} *CSG_IdList;

typedef enum CSGMembershipStatus {
    member = 0,
    not_member = 1
} CSGMembershipStatus;

typedef unsigned short  PDCP_SNExtended;

typedef unsigned int    HFNModified;

typedef struct COUNTValueExtended {
    unsigned char   bit_mask;
#       define      COUNTValueExtended_iE_Extensions_present 0x80
    PDCP_SNExtended pDCP_SNExtended;
    HFNModified     hFNModified;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * COUNTValueExtended_iE_Extensions_present if
                                   * present */
} COUNTValueExtended;

typedef enum TriggeringMessage {
    initiating_message = 0,
    successful_outcome = 1,
    unsuccessfull_outcome = 2
} TriggeringMessage;

typedef struct CriticalityDiagnostics {
    unsigned char   bit_mask;
#       define      procedureCode_present 0x80
#       define      triggeringMessage_present 0x40
#       define      procedureCriticality_present 0x20
#       define      iEsCriticalityDiagnostics_present 0x10
#       define      CriticalityDiagnostics_iE_Extensions_present 0x08
    ProcedureCode   procedureCode;  /* optional; set in bit_mask
                                     * procedureCode_present if present */
    TriggeringMessage triggeringMessage;  /* optional; set in bit_mask
                                           * triggeringMessage_present if
                                           * present */
    Criticality     procedureCriticality;  /* optional; set in bit_mask
                                            * procedureCriticality_present if
                                            * present */
    struct CriticalityDiagnostics_IE_List *iEsCriticalityDiagnostics;  
                                  /* optional; set in bit_mask
                                   * iEsCriticalityDiagnostics_present if
                                   * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * CriticalityDiagnostics_iE_Extensions_present if
                              * present */
} CriticalityDiagnostics;

typedef enum TypeOfError {
    not_understood = 0,
    missing = 1
} TypeOfError;

typedef struct CriticalityDiagnostics_IE_Item {
    unsigned char   bit_mask;
#       define      CriticalityDiagnostics_IE_Item_iE_Extensions_present 0x80
    Criticality     iECriticality;
    ProtocolIE_ID   iE_ID;
    TypeOfError     typeOfError;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                      * CriticalityDiagnostics_IE_Item_iE_Extensions_present if
                      * present */
} CriticalityDiagnostics_IE_Item;

typedef struct CriticalityDiagnostics_IE_List {
    struct CriticalityDiagnostics_IE_List *next;
    CriticalityDiagnostics_IE_Item value;
} *CriticalityDiagnostics_IE_List;

/* D */
typedef struct DataCodingScheme {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} DataCodingScheme;

typedef enum DL_Forwarding {
    dL_Forwarding_proposed = 0
} DL_Forwarding;

typedef enum Direct_Forwarding_Path_Availability {
    directPathAvailable = 0
} Direct_Forwarding_Path_Availability;

typedef enum Data_Forwarding_Not_Possible {
    data_Forwarding_not_Possible = 0
} Data_Forwarding_Not_Possible;

/* E */
typedef int             EARFCN;

typedef struct ECGIList {
    struct ECGIList *next;
    EUTRAN_CGI      value;
} *ECGIList;

typedef struct EmergencyAreaID {
    unsigned short  length;
    unsigned char   value[3];
} EmergencyAreaID;

typedef struct EmergencyAreaIDList {
    struct EmergencyAreaIDList *next;
    EmergencyAreaID value;
} *EmergencyAreaIDList;

typedef struct EmergencyAreaID_Broadcast_Item {
    unsigned char   bit_mask;
#       define      EmergencyAreaID_Broadcast_Item_iE_Extensions_present 0x80
    EmergencyAreaID emergencyAreaID;
    struct CompletedCellinEAI *completedCellinEAI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                      * EmergencyAreaID_Broadcast_Item_iE_Extensions_present if
                      * present */
} EmergencyAreaID_Broadcast_Item;

typedef struct EmergencyAreaID_Broadcast {
    struct EmergencyAreaID_Broadcast *next;
    EmergencyAreaID_Broadcast_Item value;
} *EmergencyAreaID_Broadcast;

typedef struct EmergencyAreaID_Cancelled_Item {
    unsigned char   bit_mask;
#       define      EmergencyAreaID_Cancelled_Item_iE_Extensions_present 0x80
    EmergencyAreaID emergencyAreaID;
    struct CancelledCellinEAI *cancelledCellinEAI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                      * EmergencyAreaID_Cancelled_Item_iE_Extensions_present if
                      * present */
} EmergencyAreaID_Cancelled_Item;

typedef struct EmergencyAreaID_Cancelled {
    struct EmergencyAreaID_Cancelled *next;
    EmergencyAreaID_Cancelled_Item value;
} *EmergencyAreaID_Cancelled;

typedef struct CompletedCellinEAI_Item {
    unsigned char   bit_mask;
#       define      CompletedCellinEAI_Item_iE_Extensions_present 0x80
    EUTRAN_CGI      eCGI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * CompletedCellinEAI_Item_iE_Extensions_present if
                             * present */
} CompletedCellinEAI_Item;

typedef struct CompletedCellinEAI {
    struct CompletedCellinEAI *next;
    CompletedCellinEAI_Item value;
} *CompletedCellinEAI;

typedef struct ECGI_List {
    struct ECGI_List *next;
    EUTRAN_CGI      value;
} *ECGI_List;

typedef struct EmergencyAreaIDListForRestart {
    struct EmergencyAreaIDListForRestart *next;
    EmergencyAreaID value;
} *EmergencyAreaIDListForRestart;

typedef struct _bit1 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} _bit1;

typedef struct ENB_ID {
    unsigned short  choice;
#       define      macroENB_ID_chosen 1
#       define      homeENB_ID_chosen 2
    union {
        _bit1           macroENB_ID;  /* to choose, set choice to
                                       * macroENB_ID_chosen */
        _bit1           homeENB_ID;  /* to choose, set choice to
                                      * homeENB_ID_chosen */
    } u;
} ENB_ID;

typedef struct Global_ENB_ID {
    unsigned char   bit_mask;
#       define      Global_ENB_ID_iE_Extensions_present 0x80
    PLMNidentity    pLMNidentity;
    ENB_ID          eNB_ID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * Global_ENB_ID_iE_Extensions_present if
                                   * present */
} Global_ENB_ID;

typedef struct MME_Group_ID {
    unsigned short  length;
    unsigned char   value[2];
} MME_Group_ID;

typedef struct MME_Code {
    unsigned short  length;
    unsigned char   value[1];
} MME_Code;

typedef struct GUMMEI {
    unsigned char   bit_mask;
#       define      GUMMEI_iE_Extensions_present 0x80
    PLMNidentity    pLMN_Identity;
    MME_Group_ID    mME_Group_ID;
    MME_Code        mME_Code;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask GUMMEI_iE_Extensions_present if
                                   * present */
} GUMMEI;

typedef struct GUMMEIList {
    struct GUMMEIList *next;
    GUMMEI          value;
} *GUMMEIList;

typedef struct ENB_StatusTransfer_TransparentContainer {
    unsigned char   bit_mask;
#       define      ENB_StatusTransfer_TransparentContainer_iE_Extensions_present 0x80
    struct Bearers_SubjectToStatusTransferList *bearers_SubjectToStatusTransferList;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
             * ENB_StatusTransfer_TransparentContainer_iE_Extensions_present if
             * present */
} ENB_StatusTransfer_TransparentContainer;

typedef unsigned int    ENB_UE_S1AP_ID;

typedef struct ENBname {
    unsigned short  length;
    char            *value;
} ENBname;

typedef struct ENBX2TLAs {
    struct ENBX2TLAs *next;
    TransportLayerAddress value;
} *ENBX2TLAs;

typedef struct EncryptionAlgorithms {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} EncryptionAlgorithms;

typedef struct EPLMNs {
    struct EPLMNs   *next;
    PLMNidentity    value;
} *EPLMNs;

typedef enum EventType {
    direct = 0,
    change_of_serve_cell = 1,
    stop_change_of_serve_cell = 2
} EventType;

typedef struct E_RABInformationList {
    struct E_RABInformationList *next;
    ProtocolIE_SingleContainer value;
} *E_RABInformationList;

typedef struct E_RABInformationListItem {
    unsigned char   bit_mask;
#       define      dL_Forwarding_present 0x80
#       define      E_RABInformationListItem_iE_Extensions_present 0x40
    E_RAB_ID        e_RAB_ID;
    DL_Forwarding   dL_Forwarding;  /* optional; set in bit_mask
                                     * dL_Forwarding_present if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * E_RABInformationListItem_iE_Extensions_present if
                            * present */
} E_RABInformationListItem;

typedef struct E_RABList {
    struct E_RABList *next;
    ProtocolIE_SingleContainer value;
} *E_RABList;

typedef struct E_RABItem {
    unsigned char   bit_mask;
#       define      E_RABItem_iE_Extensions_present 0x80
    E_RAB_ID        e_RAB_ID;
    Cause           cause;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask E_RABItem_iE_Extensions_present if
                                   * present */
} E_RABItem;

typedef unsigned short  EUTRANRoundTripDelayEstimationInfo;

typedef int             ExpectedActivityPeriod;

typedef int             ExpectedIdlePeriod;

typedef enum SourceOfUEActivityBehaviourInformation {
    subscription_information = 0,
    statistics = 1
} SourceOfUEActivityBehaviourInformation;

typedef struct ExpectedUEActivityBehaviour {
    unsigned char   bit_mask;
#       define      expectedActivityPeriod_present 0x80
#       define      expectedIdlePeriod_present 0x40
#       define      sourceofUEActivityBehaviourInformation_present 0x20
#       define      ExpectedUEActivityBehaviour_iE_Extensions_present 0x10
    ExpectedActivityPeriod expectedActivityPeriod;  /* optional; set in bit_mask
                                            * expectedActivityPeriod_present if
                                            * present */
    ExpectedIdlePeriod expectedIdlePeriod;  /* optional; set in bit_mask
                                             * expectedIdlePeriod_present if
                                             * present */
    SourceOfUEActivityBehaviourInformation sourceofUEActivityBehaviourInformation;                                      /* optional; set in bit_mask
                            * sourceofUEActivityBehaviourInformation_present if
                            * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                         * ExpectedUEActivityBehaviour_iE_Extensions_present if
                         * present */
} ExpectedUEActivityBehaviour;

typedef enum ExpectedHOInterval {
    sec15 = 0,
    sec30 = 1,
    sec60 = 2,
    sec90 = 3,
    sec120 = 4,
    sec180 = 5,
    long_time = 6
} ExpectedHOInterval;

typedef struct ExpectedUEBehaviour {
    unsigned char   bit_mask;
#       define      expectedActivity_present 0x80
#       define      expectedHOInterval_present 0x40
#       define      ExpectedUEBehaviour_iE_Extensions_present 0x20
    ExpectedUEActivityBehaviour expectedActivity;  /* optional; set in bit_mask
                                                    * expectedActivity_present
                                                    * if present */
    ExpectedHOInterval expectedHOInterval;  /* optional; set in bit_mask
                                             * expectedHOInterval_present if
                                             * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * ExpectedUEBehaviour_iE_Extensions_present
                                   * if present */
} ExpectedUEBehaviour;

typedef unsigned int    ExtendedRepetitionPeriod;

/* F */
typedef enum ForbiddenInterRATs {
    all = 0,
    geran = 1,
    utran = 2,
    cdma2000 = 3,
    geranandutran = 4,
    cdma2000andutran = 5
} ForbiddenInterRATs;

typedef struct ForbiddenTAs_Item {
    unsigned char   bit_mask;
#       define      ForbiddenTAs_Item_iE_Extensions_present 0x80
    PLMNidentity    pLMN_Identity;
    struct ForbiddenTACs *forbiddenTACs;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * ForbiddenTAs_Item_iE_Extensions_present if
                                   * present */
} ForbiddenTAs_Item;

typedef struct ForbiddenTAs {
    struct ForbiddenTAs *next;
    ForbiddenTAs_Item value;
} *ForbiddenTAs;

typedef struct ForbiddenTACs {
    struct ForbiddenTACs *next;
    TAC             value;
} *ForbiddenTACs;

typedef struct ForbiddenLAs_Item {
    unsigned char   bit_mask;
#       define      ForbiddenLAs_Item_iE_Extensions_present 0x80
    PLMNidentity    pLMN_Identity;
    struct ForbiddenLACs *forbiddenLACs;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * ForbiddenLAs_Item_iE_Extensions_present if
                                   * present */
} ForbiddenLAs_Item;

typedef struct ForbiddenLAs {
    struct ForbiddenLAs *next;
    ForbiddenLAs_Item value;
} *ForbiddenLAs;

typedef struct ForbiddenLACs {
    struct ForbiddenLACs *next;
    LAC             value;
} *ForbiddenLACs;

typedef enum GUMMEIType {
    native = 0,
    mapped = 1
} GUMMEIType;

typedef enum GWContextReleaseIndication {
    GWContextReleaseIndication_true = 0
} GWContextReleaseIndication;

/* H */
typedef struct HandoverRestrictionList {
    unsigned char   bit_mask;
#       define      equivalentPLMNs_present 0x80
#       define      forbiddenTAs_present 0x40
#       define      forbiddenLAs_present 0x20
#       define      forbiddenInterRATs_present 0x10
#       define      HandoverRestrictionList_iE_Extensions_present 0x08
    PLMNidentity    servingPLMN;
    struct EPLMNs   *equivalentPLMNs;  /* optional; set in bit_mask
                                        * equivalentPLMNs_present if present */
    struct ForbiddenTAs *forbiddenTAs;  /* optional; set in bit_mask
                                         * forbiddenTAs_present if present */
    struct ForbiddenLAs *forbiddenLAs;  /* optional; set in bit_mask
                                         * forbiddenLAs_present if present */
    ForbiddenInterRATs forbiddenInterRATs;  /* optional; set in bit_mask
                                             * forbiddenInterRATs_present if
                                             * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * HandoverRestrictionList_iE_Extensions_present if
                             * present */
} HandoverRestrictionList;

typedef enum HandoverType {
    intralte = 0,
    ltetoutran = 1,
    ltetogeran = 2,
    utrantolte = 3,
    gerantolte = 4
} HandoverType;

/* I */
typedef struct Masked_IMEISV {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} Masked_IMEISV;

typedef struct MeasurementsToActivate {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} MeasurementsToActivate;

typedef enum M1ReportingTrigger {
    periodic = 0,
    a2eventtriggered = 1,
    a2eventtriggered_periodic = 2
} M1ReportingTrigger;

typedef unsigned short  Threshold_RSRP;

typedef unsigned short  Threshold_RSRQ;

typedef struct MeasurementThresholdA2 {
    unsigned short  choice;
#       define      threshold_RSRP_chosen 1
#       define      threshold_RSRQ_chosen 2
    union {
        Threshold_RSRP  threshold_RSRP;  /* to choose, set choice to
                                          * threshold_RSRP_chosen */
        Threshold_RSRQ  threshold_RSRQ;  /* to choose, set choice to
                                          * threshold_RSRQ_chosen */
    } u;
} MeasurementThresholdA2;

/* This is a dummy IE used only as a reference to the actual definition in relevant specification. */
typedef struct M1ThresholdEventA2 {
    unsigned char   bit_mask;
#       define      M1ThresholdEventA2_iE_Extensions_present 0x80
    MeasurementThresholdA2 measurementThreshold;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * M1ThresholdEventA2_iE_Extensions_present if
                                   * present */
} M1ThresholdEventA2;

typedef enum ReportIntervalMDT {
    ms120 = 0,
    ms240 = 1,
    ms480 = 2,
    ms640 = 3,
    ReportIntervalMDT_ms1024 = 4,
    ReportIntervalMDT_ms2048 = 5,
    ReportIntervalMDT_ms5120 = 6,
    ReportIntervalMDT_ms10240 = 7,
    ReportIntervalMDT_min1 = 8,
    min6 = 9,
    min12 = 10,
    min30 = 11,
    min60 = 12
} ReportIntervalMDT;

typedef enum ReportAmountMDT {
    r1 = 0,
    r2 = 1,
    r4 = 2,
    r8 = 3,
    r16 = 4,
    r32 = 5,
    r64 = 6,
    rinfinity = 7
} ReportAmountMDT;

typedef struct M1PeriodicReporting {
    unsigned char   bit_mask;
#       define      M1PeriodicReporting_iE_Extensions_present 0x80
    ReportIntervalMDT reportInterval;
    ReportAmountMDT reportAmount;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * M1PeriodicReporting_iE_Extensions_present
                                   * if present */
} M1PeriodicReporting;

typedef struct ImmediateMDT {
    unsigned char   bit_mask;
#       define      m1thresholdeventA2_present 0x80
#       define      m1periodicReporting_present 0x40
#       define      ImmediateMDT_iE_Extensions_present 0x20
    MeasurementsToActivate measurementsToActivate;
    M1ReportingTrigger m1reportingTrigger;
    M1ThresholdEventA2 m1thresholdeventA2;  /* optional; set in bit_mask
                                             * m1thresholdeventA2_present if
                                             * present */
/* Included in case of event-triggered, or event-triggered periodic reporting for measurement M1 */
    M1PeriodicReporting m1periodicReporting;  /* optional; set in bit_mask
                                               * m1periodicReporting_present if
                                               * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask ImmediateMDT_iE_Extensions_present
                                   * if present */
/* Included in case of periodic or event-triggered periodic reporting */
} ImmediateMDT;

typedef struct IMSI {
    unsigned short  length;
    unsigned char   value[8];
} IMSI;

typedef struct IntegrityProtectionAlgorithms {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} IntegrityProtectionAlgorithms;

typedef struct InterfacesToTrace {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} InterfacesToTrace;

/* J */
/* K */
typedef enum KillAllWarningMessages {
    KillAllWarningMessages_true = 0
} KillAllWarningMessages;

typedef unsigned short  Time_UE_StayedInCell;

typedef struct LastVisitedEUTRANCellInformation {
    unsigned char   bit_mask;
#       define      LastVisitedEUTRANCellInformation_iE_Extensions_present 0x80
    EUTRAN_CGI      global_Cell_ID;
    CellType        cellType;
    Time_UE_StayedInCell time_UE_StayedInCell;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                    * LastVisitedEUTRANCellInformation_iE_Extensions_present if
                    * present */
} LastVisitedEUTRANCellInformation;

typedef struct LastVisitedUTRANCellInformation {
    unsigned int    length;
    unsigned char   *value;
} LastVisitedUTRANCellInformation;

typedef struct LastVisitedGERANCellInformation {
    unsigned short  choice;
#       define      undefined_chosen 1
    union {
        Nulltype        undefined;  /* to choose, set choice to
                                     * undefined_chosen */
    } u;
} LastVisitedGERANCellInformation;

typedef struct LastVisitedCell_Item {
    unsigned short  choice;
#       define      e_UTRAN_Cell_chosen 1
#       define      uTRAN_Cell_chosen 2
#       define      gERAN_Cell_chosen 3
    union {
        LastVisitedEUTRANCellInformation e_UTRAN_Cell;  /* to choose, set choice
                                                    * to e_UTRAN_Cell_chosen */
        LastVisitedUTRANCellInformation uTRAN_Cell;  /* to choose, set choice to
                                                      * uTRAN_Cell_chosen */
        LastVisitedGERANCellInformation gERAN_Cell;  /* to choose, set choice to
                                                      * gERAN_Cell_chosen */
    } u;
} LastVisitedCell_Item;

typedef struct L3_Information {
    unsigned int    length;
    unsigned char   *value;
} L3_Information;

/* This is a dummy IE used only as a reference to the actual definition in relevant specification. */
typedef struct LPPa_PDU {
    unsigned int    length;
    unsigned char   *value;
} LPPa_PDU;

typedef struct LHN_ID {
    unsigned short  length;
    unsigned char   value[256];
} LHN_ID;

typedef enum Links_to_log {
    uplink = 0,
    downlink = 1,
    both_uplink_and_downlink = 2
} Links_to_log;

typedef struct ListeningSubframePattern {
    unsigned char   bit_mask;
#       define      ListeningSubframePattern_iE_Extensions_present 0x80
    enum {
        pattern_period_ms1280 = 0,
        pattern_period_ms2560 = 1,
        pattern_period_ms5120 = 2,
        pattern_period_ms10240 = 3
    } pattern_period;
    int             pattern_offset;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * ListeningSubframePattern_iE_Extensions_present if
                            * present */
} ListeningSubframePattern;

typedef enum LoggingInterval {
    ms128 = 0,
    ms256 = 1,
    ms512 = 2,
    LoggingInterval_ms1024 = 3,
    LoggingInterval_ms2048 = 4,
    ms3072 = 5,
    ms4096 = 6,
    ms6144 = 7
} LoggingInterval;

typedef enum LoggingDuration {
    m10 = 0,
    m20 = 1,
    m40 = 2,
    m60 = 3,
    m90 = 4,
    m120 = 5
} LoggingDuration;

typedef struct LoggedMDT {
    unsigned char   bit_mask;
#       define      LoggedMDT_iE_Extensions_present 0x80
    LoggingInterval loggingInterval;
    LoggingDuration loggingDuration;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask LoggedMDT_iE_Extensions_present if
                                   * present */
} LoggedMDT;

typedef struct LoggedMBSFNMDT {
    unsigned char   bit_mask;
#       define      mBSFN_ResultToLog_present 0x80
#       define      LoggedMBSFNMDT_iE_Extensions_present 0x40
    LoggingInterval loggingInterval;
    LoggingDuration loggingDuration;
    struct MBSFN_ResultToLog *mBSFN_ResultToLog;  /* optional; set in bit_mask
                                                   * mBSFN_ResultToLog_present
                                                   * if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * LoggedMBSFNMDT_iE_Extensions_present if
                                   * present */
} LoggedMBSFNMDT;

typedef enum M3period {
    ms100 = 0,
    ms1000 = 1,
    ms10000 = 2
} M3period;

/* M */
typedef struct M3Configuration {
    unsigned char   bit_mask;
#       define      M3Configuration_iE_Extensions_present 0x80
    M3period        m3period;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * M3Configuration_iE_Extensions_present if
                                   * present */
} M3Configuration;

typedef enum M4period {
    M4period_ms1024 = 0,
    M4period_ms2048 = 1,
    M4period_ms5120 = 2,
    M4period_ms10240 = 3,
    M4period_min1 = 4
} M4period;

typedef struct M4Configuration {
    unsigned char   bit_mask;
#       define      M4Configuration_iE_Extensions_present 0x80
    M4period        m4period;
    Links_to_log    m4_links_to_log;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * M4Configuration_iE_Extensions_present if
                                   * present */
} M4Configuration;

typedef enum M5period {
    M5period_ms1024 = 0,
    M5period_ms2048 = 1,
    M5period_ms5120 = 2,
    M5period_ms10240 = 3,
    M5period_min1 = 4
} M5period;

typedef struct M5Configuration {
    unsigned char   bit_mask;
#       define      M5Configuration_iE_Extensions_present 0x80
    M5period        m5period;
    Links_to_log    m5_links_to_log;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * M5Configuration_iE_Extensions_present if
                                   * present */
} M5Configuration;

typedef enum MDT_Activation {
    immediate_MDT_only = 0,
    immediate_MDT_and_Trace = 1,
    logged_MDT_only = 2,
    logged_MBSFN_MDT = 3
} MDT_Activation;

typedef struct MDT_Location_Info {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} MDT_Location_Info;

typedef ProtocolIE_SingleContainer MDTMode_Extension;

typedef struct MDTMode {
    unsigned short  choice;
#       define      immediateMDT_chosen 1
#       define      loggedMDT_chosen 2
#       define      mDTMode_Extension_chosen 3
    union {
        ImmediateMDT    immediateMDT;  /* to choose, set choice to
                                        * immediateMDT_chosen */
        LoggedMDT       loggedMDT;  /* to choose, set choice to
                                     * loggedMDT_chosen */
        MDTMode_Extension mDTMode_Extension;  /* extension #1; to choose, set
                                               * choice to
                                               * mDTMode_Extension_chosen */
    } u;
} MDTMode;

typedef struct MDT_Configuration {
    unsigned char   bit_mask;
#       define      MDT_Configuration_iE_Extensions_present 0x80
    MDT_Activation  mdt_Activation;
    AreaScopeOfMDT  areaScopeOfMDT;
    MDTMode         mDTMode;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * MDT_Configuration_iE_Extensions_present if
                                   * present */
} MDT_Configuration;

typedef enum ManagementBasedMDTAllowed {
    allowed = 0
} ManagementBasedMDTAllowed;

typedef struct MBSFN_ResultToLogInfo {
    unsigned char   bit_mask;
#       define      mBSFN_AreaId_present 0x80
#       define      MBSFN_ResultToLogInfo_iE_Extensions_present 0x40
    unsigned short  mBSFN_AreaId;  /* optional; set in bit_mask
                                    * mBSFN_AreaId_present if present */
    EARFCN          carrierFreq;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * MBSFN_ResultToLogInfo_iE_Extensions_present
                                   * if present */
} MBSFN_ResultToLogInfo;

typedef struct MBSFN_ResultToLog {
    struct MBSFN_ResultToLog *next;
    MBSFN_ResultToLogInfo value;
} *MBSFN_ResultToLog;

typedef struct MDTPLMNList {
    struct MDTPLMNList *next;
    PLMNidentity    value;
} *MDTPLMNList;

typedef enum PrivacyIndicator {
    immediate_MDT = 0,
    logged_MDT = 1
} PrivacyIndicator;

typedef struct MessageIdentifier {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} MessageIdentifier;

typedef struct MobilityInformation {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} MobilityInformation;

typedef struct MMEname {
    unsigned short  length;
    char            *value;
} MMEname;

typedef enum MMERelaySupportIndicator {
    MMERelaySupportIndicator_true = 0
} MMERelaySupportIndicator;

typedef unsigned int    MME_UE_S1AP_ID;

typedef struct M_TMSI {
    unsigned short  length;
    unsigned char   value[4];
} M_TMSI;

typedef struct MSClassmark2 {
    unsigned int    length;
    unsigned char   *value;
} MSClassmark2;

typedef struct MSClassmark3 {
    unsigned int    length;
    unsigned char   *value;
} MSClassmark3;

typedef enum MutingAvailabilityIndication {
    available = 0,
    unavailable = 1
} MutingAvailabilityIndication;

typedef struct MutingPatternInformation {
    unsigned char   bit_mask;
#       define      muting_pattern_offset_present 0x80
#       define      MutingPatternInformation_iE_Extensions_present 0x40
    enum {
        ms0 = 0,
        muting_pattern_period_ms1280 = 1,
        muting_pattern_period_ms2560 = 2,
        muting_pattern_period_ms5120 = 3,
        muting_pattern_period_ms10240 = 4
    } muting_pattern_period;
    int             muting_pattern_offset;  /* optional; set in bit_mask
                                             * muting_pattern_offset_present if
                                             * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * MutingPatternInformation_iE_Extensions_present if
                            * present */
} MutingPatternInformation;

typedef struct NASSecurityParametersfromE_UTRAN {
    unsigned int    length;
    unsigned char   *value;
} NASSecurityParametersfromE_UTRAN;

typedef struct NASSecurityParameterstoE_UTRAN {
    unsigned int    length;
    unsigned char   *value;
} NASSecurityParameterstoE_UTRAN;

typedef unsigned short  NumberofBroadcastRequest;

/* O */
typedef struct OldBSS_ToNewBSS_Information {
    unsigned int    length;
    unsigned char   *value;
} OldBSS_ToNewBSS_Information;

/* This is a dummy IE used only as a reference to the actual definition in relevant specification. */
typedef enum OverloadAction {
    reject_non_emergency_mo_dt = 0,
    reject_rrc_cr_signalling = 1,
    permit_emergency_sessions_and_mobile_terminated_services_only = 2,
    permit_high_priority_sessions_and_mobile_terminated_services_only = 3,
    reject_delay_tolerant_access = 4
} OverloadAction;

typedef struct OverloadResponse {
    unsigned short  choice;
#       define      overloadAction_chosen 1
    union {
        OverloadAction  overloadAction;  /* to choose, set choice to
                                          * overloadAction_chosen */
    } u;
} OverloadResponse;

/* P */
typedef enum PagingDRX {
    v32 = 0,
    v64 = 1,
    v128 = 2,
    v256 = 3
} PagingDRX;

typedef enum PagingPriority {
    priolevel1 = 0,
    priolevel2 = 1,
    priolevel3 = 2,
    priolevel4 = 3,
    priolevel5 = 4,
    priolevel6 = 5,
    priolevel7 = 6,
    priolevel8 = 7
} PagingPriority;

typedef struct Port_Number {
    unsigned short  length;
    unsigned char   value[2];
} Port_Number;

typedef enum ProSeDirectDiscovery {
    ProSeDirectDiscovery_authorized = 0,
    ProSeDirectDiscovery_not_authorized = 1
} ProSeDirectDiscovery;

typedef enum ProSeDirectCommunication {
    ProSeDirectCommunication_authorized = 0,
    ProSeDirectCommunication_not_authorized = 1
} ProSeDirectCommunication;

typedef struct ProSeAuthorized {
    unsigned char   bit_mask;
#       define      proSeDirectDiscovery_present 0x80
#       define      proSeDirectCommunication_present 0x40
#       define      ProSeAuthorized_iE_Extensions_present 0x20
    ProSeDirectDiscovery proSeDirectDiscovery;  /* optional; set in bit_mask
                                                 * proSeDirectDiscovery_present
                                                 * if present */
    ProSeDirectCommunication proSeDirectCommunication;  /* optional; set in
                                   * bit_mask proSeDirectCommunication_present
                                   * if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * ProSeAuthorized_iE_Extensions_present if
                                   * present */
} ProSeAuthorized;

typedef enum PS_ServiceNotAvailable {
    ps_service_not_available = 0
} PS_ServiceNotAvailable;

typedef struct ReceiveStatusOfULPDCPSDUsExtended {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} ReceiveStatusOfULPDCPSDUsExtended;

typedef unsigned short  RelativeMMECapacity;

typedef enum RelayNode_Indicator {
    RelayNode_Indicator_true = 0
} RelayNode_Indicator;

typedef enum ReportArea {
    ecgi = 0
} ReportArea;

typedef struct RequestType {
    unsigned char   bit_mask;
#       define      RequestType_iE_Extensions_present 0x80
    EventType       eventType;
    ReportArea      reportArea;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask RequestType_iE_Extensions_present
                                   * if present */
} RequestType;

typedef unsigned short  RepetitionPeriod;

typedef struct UE_RLF_Report_Container {
    unsigned int    length;
    unsigned char   *value;
} UE_RLF_Report_Container;

/* This IE is a transparent container and shall be encoded as the rlf-Report-r9 field contained in the UEInformationResponse message as defined in TS 36.331 [16] */
typedef struct UE_RLF_Report_Container_for_extended_bands {
    unsigned int    length;
    unsigned char   *value;
} UE_RLF_Report_Container_for_extended_bands;

typedef struct RLFReportInformation {
    unsigned char   bit_mask;
#       define      uE_RLF_Report_Container_for_extended_bands_present 0x80
#       define      RLFReportInformation_iE_Extensions_present 0x40
    UE_RLF_Report_Container uE_RLF_Report_Container;
    UE_RLF_Report_Container_for_extended_bands uE_RLF_Report_Container_for_extended_bands;                              /* optional; set in bit_mask
                        * uE_RLF_Report_Container_for_extended_bands_present if
                        * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * RLFReportInformation_iE_Extensions_present
                                   * if present */
} RLFReportInformation;

typedef struct RRC_Container {
    unsigned int    length;
    unsigned char   *value;
} RRC_Container;

typedef enum RRC_Establishment_Cause {
    emergency = 0,
    highPriorityAccess = 1,
    mt_Access = 2,
    mo_Signalling = 3,
    mo_Data = 4,
    delay_TolerantAccess = 5
} RRC_Establishment_Cause;

typedef struct ECGIListForRestart {
    struct ECGIListForRestart *next;
    EUTRAN_CGI      value;
} *ECGIListForRestart;

typedef unsigned short  Routing_ID;

/* S */
typedef struct SecurityKey {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} SecurityKey;

typedef struct SecurityContext {
    unsigned char   bit_mask;
#       define      SecurityContext_iE_Extensions_present 0x80
    unsigned short  nextHopChainingCount;
    SecurityKey     nextHopParameter;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * SecurityContext_iE_Extensions_present if
                                   * present */
} SecurityContext;

typedef struct SerialNumber {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} SerialNumber;

typedef enum SONInformationRequest {
    x2TNL_Configuration_Info = 0,
    time_Synchronisation_Info = 1,
    activate_Muting = 2,
    deactivate_Muting = 3
} SONInformationRequest;

/* X */
typedef struct X2TNLConfigurationInfo {
    unsigned char   bit_mask;
#       define      X2TNLConfigurationInfo_iE_Extensions_present 0x80
    struct ENBX2TLAs *eNBX2TransportLayerAddresses;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * X2TNLConfigurationInfo_iE_Extensions_present if
                              * present */
} X2TNLConfigurationInfo;

typedef struct SONInformationReply {
    unsigned char   bit_mask;
#       define      x2TNLConfigurationInfo_present 0x80
#       define      SONInformationReply_iE_Extensions_present 0x40
    X2TNLConfigurationInfo x2TNLConfigurationInfo;  /* optional; set in bit_mask
                                            * x2TNLConfigurationInfo_present if
                                            * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * SONInformationReply_iE_Extensions_present
                                   * if present */
} SONInformationReply;

typedef ProtocolIE_SingleContainer SONInformation_Extension;

typedef struct SONInformation {
    unsigned short  choice;
#       define      sONInformationRequest_chosen 1
#       define      sONInformationReply_chosen 2
#       define      sONInformation_Extension_chosen 3
    union {
        SONInformationRequest sONInformationRequest;  /* to choose, set choice
                                           * to sONInformationRequest_chosen */
        SONInformationReply sONInformationReply;  /* to choose, set choice to
                                                * sONInformationReply_chosen */
        SONInformation_Extension sONInformation_Extension;  /* extension #1; to
                                   * choose, set choice to
                                   * sONInformation_Extension_chosen */
    } u;
} SONInformation;

typedef struct SONInformationReport {
    unsigned short  choice;
#       define      rLFReportInformation_chosen 1
    union {
        RLFReportInformation rLFReportInformation;  /* to choose, set choice to
                                               * rLFReportInformation_chosen */
    } u;
} SONInformationReport;

typedef struct TargeteNB_ID {
    unsigned char   bit_mask;
#       define      TargeteNB_ID_iE_Extensions_present 0x80
    Global_ENB_ID   global_ENB_ID;
    TAI             selected_TAI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask TargeteNB_ID_iE_Extensions_present
                                   * if present */
} TargeteNB_ID;

/* This is a dummy IE used only as a reference to the actual definition in relevant specification. */
typedef struct SourceeNB_ID {
    unsigned char   bit_mask;
#       define      SourceeNB_ID_iE_Extensions_present 0x80
    Global_ENB_ID   global_ENB_ID;
    TAI             selected_TAI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask SourceeNB_ID_iE_Extensions_present
                                   * if present */
} SourceeNB_ID;

typedef struct SONConfigurationTransfer {
    unsigned char   bit_mask;
#       define      SONConfigurationTransfer_iE_Extensions_present 0x80
    TargeteNB_ID    targeteNB_ID;
    SourceeNB_ID    sourceeNB_ID;
    SONInformation  sONInformation;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                            * SONConfigurationTransfer_iE_Extensions_present if
                            * present */
} SONConfigurationTransfer;

typedef int             StratumLevel;

typedef struct SynchronisationInformation {
    unsigned char   bit_mask;
#       define      sourceStratumLevel_present 0x80
#       define      listeningSubframePattern_present 0x40
#       define      aggressoreCGI_List_present 0x20
#       define      SynchronisationInformation_iE_Extensions_present 0x10
    StratumLevel    sourceStratumLevel;  /* optional; set in bit_mask
                                          * sourceStratumLevel_present if
                                          * present */
    ListeningSubframePattern listeningSubframePattern;  /* optional; set in
                                   * bit_mask listeningSubframePattern_present
                                   * if present */
    struct ECGI_List *aggressoreCGI_List;  /* optional; set in bit_mask
                                            * aggressoreCGI_List_present if
                                            * present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                          * SynchronisationInformation_iE_Extensions_present if
                          * present */
} SynchronisationInformation;

typedef struct Source_ToTarget_TransparentContainer {
    unsigned int    length;
    unsigned char   *value;
} Source_ToTarget_TransparentContainer;

/* This IE includes a transparent container from the source RAN node to the target RAN node. */
/* The octets of the OCTET STRING are encoded according to the specifications of the target system. */
typedef struct SourceBSS_ToTargetBSS_TransparentContainer {
    unsigned int    length;
    unsigned char   *value;
} SourceBSS_ToTargetBSS_TransparentContainer;

typedef enum SRVCCOperationPossible {
    possible = 0
} SRVCCOperationPossible;

typedef enum SRVCCHOIndication {
    pSandCS = 0,
    cSonly = 1
} SRVCCHOIndication;

typedef unsigned short  SubscriberProfileIDforRFP;

typedef struct SourceeNB_ToTargeteNB_TransparentContainer {
    unsigned char   bit_mask;
#       define      e_RABInformationList_present 0x80
#       define      subscriberProfileIDforRFP_present 0x40
#       define      SourceeNB_ToTargeteNB_TransparentContainer_iE_Extensions_present 0x20
    RRC_Container   rRC_Container;
    struct E_RABInformationList *e_RABInformationList;  /* optional; set in
                                   * bit_mask e_RABInformationList_present if
                                   * present */
    EUTRAN_CGI      targetCell_ID;
    SubscriberProfileIDforRFP subscriberProfileIDforRFP;  /* optional; set in
                                   * bit_mask subscriberProfileIDforRFP_present
                                   * if present */
    struct UE_HistoryInformation *uE_HistoryInformation;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
          * SourceeNB_ToTargeteNB_TransparentContainer_iE_Extensions_present if
          * present */
} SourceeNB_ToTargeteNB_TransparentContainer;

typedef struct SourceRNC_ToTargetRNC_TransparentContainer {
    unsigned int    length;
    unsigned char   *value;
} SourceRNC_ToTargetRNC_TransparentContainer;

typedef struct ServedGUMMEIsItem {
    unsigned char   bit_mask;
#       define      ServedGUMMEIsItem_iE_Extensions_present 0x80
    struct ServedPLMNs *servedPLMNs;
    struct ServedGroupIDs *servedGroupIDs;
    struct ServedMMECs *servedMMECs;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * ServedGUMMEIsItem_iE_Extensions_present if
                                   * present */
} ServedGUMMEIsItem;

/* This is a dummy IE used only as a reference to the actual definition in relevant specification. */
typedef struct ServedGUMMEIs {
    struct ServedGUMMEIs *next;
    ServedGUMMEIsItem value;
} *ServedGUMMEIs;

typedef struct ServedGroupIDs {
    struct ServedGroupIDs *next;
    MME_Group_ID    value;
} *ServedGroupIDs;

typedef struct ServedMMECs {
    struct ServedMMECs *next;
    MME_Code        value;
} *ServedMMECs;

typedef struct ServedPLMNs {
    struct ServedPLMNs *next;
    PLMNidentity    value;
} *ServedPLMNs;

typedef struct SupportedTAs_Item {
    unsigned char   bit_mask;
#       define      SupportedTAs_Item_iE_Extensions_present 0x80
    TAC             tAC;
    struct BPLMNs   *broadcastPLMNs;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * SupportedTAs_Item_iE_Extensions_present if
                                   * present */
} SupportedTAs_Item;

typedef struct SupportedTAs {
    struct SupportedTAs *next;
    SupportedTAs_Item value;
} *SupportedTAs;

typedef enum SynchronisationStatus {
    synchronous = 0,
    asynchronous = 1
} SynchronisationStatus;

typedef struct TimeSynchronisationInfo {
    unsigned char   bit_mask;
#       define      TimeSynchronisationInfo_iE_Extensions_present 0x80
    StratumLevel    stratumLevel;
    SynchronisationStatus synchronisationStatus;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * TimeSynchronisationInfo_iE_Extensions_present if
                             * present */
} TimeSynchronisationInfo;

typedef struct S_TMSI {
    unsigned char   bit_mask;
#       define      S_TMSI_iE_Extensions_present 0x80
    MME_Code        mMEC;
    M_TMSI          m_TMSI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask S_TMSI_iE_Extensions_present if
                                   * present */
} S_TMSI;

typedef struct TAIListforMDT {
    struct TAIListforMDT *next;
    TAI             value;
} *TAIListforMDT;

typedef struct TAIListforWarning {
    struct TAIListforWarning *next;
    TAI             value;
} *TAIListforWarning;

typedef struct TAI_Broadcast_Item {
    unsigned char   bit_mask;
#       define      TAI_Broadcast_Item_iE_Extensions_present 0x80
    TAI             tAI;
    struct CompletedCellinTAI *completedCellinTAI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * TAI_Broadcast_Item_iE_Extensions_present if
                                   * present */
} TAI_Broadcast_Item;

typedef struct TAI_Broadcast {
    struct TAI_Broadcast *next;
    TAI_Broadcast_Item value;
} *TAI_Broadcast;

typedef struct TAI_Cancelled_Item {
    unsigned char   bit_mask;
#       define      TAI_Cancelled_Item_iE_Extensions_present 0x80
    TAI             tAI;
    struct CancelledCellinTAI *cancelledCellinTAI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * TAI_Cancelled_Item_iE_Extensions_present if
                                   * present */
} TAI_Cancelled_Item;

typedef struct TAI_Cancelled {
    struct TAI_Cancelled *next;
    TAI_Cancelled_Item value;
} *TAI_Cancelled;

typedef struct TAListforMDT {
    struct TAListforMDT *next;
    TAC             value;
} *TAListforMDT;

typedef struct CompletedCellinTAI_Item {
    unsigned char   bit_mask;
#       define      CompletedCellinTAI_Item_iE_Extensions_present 0x80
    EUTRAN_CGI      eCGI;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * CompletedCellinTAI_Item_iE_Extensions_present if
                             * present */
} CompletedCellinTAI_Item;

typedef struct CompletedCellinTAI {
    struct CompletedCellinTAI *next;
    CompletedCellinTAI_Item value;
} *CompletedCellinTAI;

typedef struct TargetID {
    unsigned short  choice;
#       define      targeteNB_ID_chosen 1
#       define      TargetID_targetRNC_ID_chosen 2
#       define      cGI_chosen 3
    union {
        TargeteNB_ID    targeteNB_ID;  /* to choose, set choice to
                                        * targeteNB_ID_chosen */
        TargetRNC_ID    targetRNC_ID;  /* to choose, set choice to
                                        * TargetID_targetRNC_ID_chosen */
        CGI             cGI;  /* to choose, set choice to cGI_chosen */
    } u;
} TargetID;

typedef struct TargeteNB_ToSourceeNB_TransparentContainer {
    unsigned char   bit_mask;
#       define      TargeteNB_ToSourceeNB_TransparentContainer_iE_Extensions_present 0x80
    RRC_Container   rRC_Container;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
          * TargeteNB_ToSourceeNB_TransparentContainer_iE_Extensions_present if
          * present */
} TargeteNB_ToSourceeNB_TransparentContainer;

typedef struct Target_ToSource_TransparentContainer {
    unsigned int    length;
    unsigned char   *value;
} Target_ToSource_TransparentContainer;

/* This IE includes a transparent container from the target RAN node to the source RAN node. */
/* The octets of the OCTET STRING are coded according to the specifications of the target system. */
typedef struct TargetRNC_ToSourceRNC_TransparentContainer {
    unsigned int    length;
    unsigned char   *value;
} TargetRNC_ToSourceRNC_TransparentContainer;

/* This is a dummy IE used only as a reference to the actual definition in relevant specification. */
typedef struct TargetBSS_ToSourceBSS_TransparentContainer {
    unsigned int    length;
    unsigned char   *value;
} TargetBSS_ToSourceBSS_TransparentContainer;

typedef enum TimeToWait {
    v1s = 0,
    v2s = 1,
    v5s = 2,
    v10s = 3,
    v20s = 4,
    v60s = 5
} TimeToWait;

typedef unsigned short  Time_UE_StayedInCell_EnhancedGranularity;

typedef struct TransportInformation {
    TransportLayerAddress transportLayerAddress;
    GTP_TEID        uL_GTP_TEID;
} TransportInformation;

typedef struct E_UTRAN_Trace_ID {
    unsigned short  length;
    unsigned char   value[8];
} E_UTRAN_Trace_ID;

typedef enum TraceDepth {
    minimum = 0,
    TraceDepth_medium = 1,
    maximum = 2,
    minimumWithoutVendorSpecificExtension = 3,
    mediumWithoutVendorSpecificExtension = 4,
    maximumWithoutVendorSpecificExtension = 5
} TraceDepth;

typedef struct TraceActivation {
    unsigned char   bit_mask;
#       define      TraceActivation_iE_Extensions_present 0x80
    E_UTRAN_Trace_ID e_UTRAN_Trace_ID;
    InterfacesToTrace interfacesToTrace;
    TraceDepth      traceDepth;
    TransportLayerAddress traceCollectionEntityIPAddress;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * TraceActivation_iE_Extensions_present if
                                   * present */
} TraceActivation;

typedef unsigned short  TrafficLoadReductionIndication;

typedef struct TunnelInformation {
    unsigned char   bit_mask;
#       define      uDP_Port_Number_present 0x80
#       define      TunnelInformation_iE_Extensions_present 0x40
    TransportLayerAddress transportLayerAddress;
    Port_Number     uDP_Port_Number;  /* optional; set in bit_mask
                                       * uDP_Port_Number_present if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * TunnelInformation_iE_Extensions_present if
                                   * present */
} TunnelInformation;

typedef struct TAIListForRestart {
    struct TAIListForRestart *next;
    TAI             value;
} *TAIListForRestart;

/* U */
typedef struct UEAggregateMaximumBitrate {
    unsigned char   bit_mask;
#       define      UEAggregateMaximumBitrate_iE_Extensions_present 0x80
    BitRate         uEaggregateMaximumBitRateDL;
    BitRate         uEaggregateMaximumBitRateUL;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                           * UEAggregateMaximumBitrate_iE_Extensions_present if
                           * present */
} UEAggregateMaximumBitrate;

typedef struct UE_S1AP_ID_pair {
    unsigned char   bit_mask;
#       define      UE_S1AP_ID_pair_iE_Extensions_present 0x80
    MME_UE_S1AP_ID  mME_UE_S1AP_ID;
    ENB_UE_S1AP_ID  eNB_UE_S1AP_ID;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                                   * UE_S1AP_ID_pair_iE_Extensions_present if
                                   * present */
} UE_S1AP_ID_pair;

typedef struct UE_S1AP_IDs {
    unsigned short  choice;
#       define      uE_S1AP_ID_pair_chosen 1
#       define      mME_UE_S1AP_ID_chosen 2
    union {
        UE_S1AP_ID_pair uE_S1AP_ID_pair;  /* to choose, set choice to
                                           * uE_S1AP_ID_pair_chosen */
        MME_UE_S1AP_ID  mME_UE_S1AP_ID;  /* to choose, set choice to
                                          * mME_UE_S1AP_ID_chosen */
    } u;
} UE_S1AP_IDs;

typedef struct UE_associatedLogicalS1_ConnectionItem {
    unsigned char   bit_mask;
#       define      mME_UE_S1AP_ID_present 0x80
#       define      eNB_UE_S1AP_ID_present 0x40
#       define      UE_associatedLogicalS1_ConnectionItem_iE_Extensions_present 0x20
    MME_UE_S1AP_ID  mME_UE_S1AP_ID;  /* optional; set in bit_mask
                                      * mME_UE_S1AP_ID_present if present */
    ENB_UE_S1AP_ID  eNB_UE_S1AP_ID;  /* optional; set in bit_mask
                                      * eNB_UE_S1AP_ID_present if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
               * UE_associatedLogicalS1_ConnectionItem_iE_Extensions_present if
               * present */
} UE_associatedLogicalS1_ConnectionItem;

typedef struct UEIdentityIndexValue {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} UEIdentityIndexValue;

typedef struct UE_HistoryInformation {
    struct UE_HistoryInformation *next;
    LastVisitedCell_Item value;
} *UE_HistoryInformation;

typedef struct UE_HistoryInformationFromTheUE {
    unsigned int    length;
    unsigned char   *value;
} UE_HistoryInformationFromTheUE;

/* This IE is a transparent container and shall be encoded as the VisitedCellInfoList field contained in the UEInformationResponse message as defined in TS 36.331 [16] */
typedef struct UEPagingID {
    unsigned short  choice;
#       define      s_TMSI_chosen 1
#       define      iMSI_chosen 2
    union {
        S_TMSI          s_TMSI;  /* to choose, set choice to s_TMSI_chosen */
        IMSI            iMSI;  /* to choose, set choice to iMSI_chosen */
    } u;
} UEPagingID;

typedef struct UERadioCapability {
    unsigned int    length;
    unsigned char   *value;
} UERadioCapability;

typedef struct UERadioCapabilityForPaging {
    unsigned int    length;
    unsigned char   *value;
} UERadioCapabilityForPaging;

/* This IE is a transparent container and shall be encoded as the rlf-Report-v9e0 contained in the UEInformationResponse message as defined in TS 36.331 [16] */
typedef struct UESecurityCapabilities {
    unsigned char   bit_mask;
#       define      UESecurityCapabilities_iE_Extensions_present 0x80
    EncryptionAlgorithms encryptionAlgorithms;
    IntegrityProtectionAlgorithms integrityProtectionAlgorithms;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                              * UESecurityCapabilities_iE_Extensions_present if
                              * present */
} UESecurityCapabilities;

typedef struct UserLocationInformation {
    unsigned char   bit_mask;
#       define      UserLocationInformation_iE_Extensions_present 0x80
    EUTRAN_CGI      eutran_cgi;
    TAI             tai;
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask
                             * UserLocationInformation_iE_Extensions_present if
                             * present */
} UserLocationInformation;

/* V */
typedef enum VoiceSupportMatchIndicator {
    supported = 0,
    not_supported = 1
} VoiceSupportMatchIndicator;

/* W */
typedef struct WarningAreaList {
    unsigned short  choice;
#       define      cellIDList_chosen 1
#       define      trackingAreaListforWarning_chosen 2
#       define      emergencyAreaIDList_chosen 3
    union {
        struct ECGIList *cellIDList;  /* to choose, set choice to
                                       * cellIDList_chosen */
        struct TAIListforWarning *trackingAreaListforWarning;  /* to choose, set
                                   * choice to
                                   * trackingAreaListforWarning_chosen */
        struct EmergencyAreaIDList *emergencyAreaIDList;  /* to choose, set
                                      * choice to emergencyAreaIDList_chosen */
    } u;
} WarningAreaList;

typedef struct WarningType {
    unsigned short  length;
    unsigned char   value[2];
} WarningType;

typedef struct WarningSecurityInfo {
    unsigned short  length;
    unsigned char   value[50];
} WarningSecurityInfo;

typedef struct WarningMessageContents {
    unsigned short  length;
    unsigned char   *value;
} WarningMessageContents;

typedef struct ENBX2ExtTLA {
    unsigned char   bit_mask;
#       define      iPsecTLA_present 0x80
#       define      gTPTLAa_present 0x40
#       define      ENBX2ExtTLA_iE_Extensions_present 0x20
    TransportLayerAddress iPsecTLA;  /* optional; set in bit_mask
                                      * iPsecTLA_present if present */
    struct ENBX2GTPTLAs *gTPTLAa;  /* optional; set in bit_mask gTPTLAa_present
                                    * if present */
    struct ProtocolExtensionContainer *iE_Extensions;  /* optional; set in
                                   * bit_mask ENBX2ExtTLA_iE_Extensions_present
                                   * if present */
} ENBX2ExtTLA;

typedef struct ENBX2ExtTLAs {
    struct ENBX2ExtTLAs *next;
    ENBX2ExtTLA     value;
} *ENBX2ExtTLAs;

typedef struct ENBX2GTPTLAs {
    struct ENBX2GTPTLAs *next;
    TransportLayerAddress value;
} *ENBX2GTPTLAs;

typedef struct ENBIndirectX2TransportLayerAddresses {
    struct ENBIndirectX2TransportLayerAddresses *next;
    TransportLayerAddress value;
} *ENBIndirectX2TransportLayerAddresses;
/* Y */
/* Z */

typedef enum Presence {
    optional = 0,
    conditional = 1,
    mandatory = 2
} Presence;

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
typedef struct S1AP_PROTOCOL_IES {
    ProtocolIE_ID   id;
    Criticality     criticality;
    unsigned short  Value;
    Presence        presence;
} S1AP_PROTOCOL_IES;

/* ************************************************************** */
/**/
/* Class Definition for Protocol Extensions */
/**/
/* ************************************************************** */
typedef struct S1AP_PROTOCOL_EXTENSION {
    ProtocolExtensionID id;
    Criticality     criticality;
    unsigned short  Extension;
    Presence        presence;
} S1AP_PROTOCOL_EXTENSION;

/* ************************************************************** */
/**/
/* Class Definition for Private IEs */
/**/
/* ************************************************************** */
typedef struct S1AP_PRIVATE_IES {
    PrivateIE_ID    id;
    Criticality     criticality;
    unsigned short  Value;
    Presence        presence;
    long            _oss_unique_index;
} S1AP_PRIVATE_IES;

#ifndef _OSSNOVALUES

/* ************************************************************** */
/**/
/* Interface Elementary Procedures */
/**/
/* ************************************************************** */
extern S1AP_ELEMENTARY_PROCEDURE handoverPreparation;

extern S1AP_ELEMENTARY_PROCEDURE handoverResourceAllocation;

extern S1AP_ELEMENTARY_PROCEDURE handoverNotification;

extern S1AP_ELEMENTARY_PROCEDURE pathSwitchRequest;

extern S1AP_ELEMENTARY_PROCEDURE e_RABSetup;

extern S1AP_ELEMENTARY_PROCEDURE e_RABModify;

extern S1AP_ELEMENTARY_PROCEDURE e_RABRelease;

extern S1AP_ELEMENTARY_PROCEDURE e_RABReleaseIndication;

extern S1AP_ELEMENTARY_PROCEDURE initialContextSetup;

extern S1AP_ELEMENTARY_PROCEDURE uEContextReleaseRequest;

extern S1AP_ELEMENTARY_PROCEDURE paging;

extern S1AP_ELEMENTARY_PROCEDURE downlinkNASTransport;

extern S1AP_ELEMENTARY_PROCEDURE initialUEMessage;

extern S1AP_ELEMENTARY_PROCEDURE uplinkNASTransport;

extern S1AP_ELEMENTARY_PROCEDURE nASNonDeliveryIndication;

extern S1AP_ELEMENTARY_PROCEDURE handoverCancel;

extern S1AP_ELEMENTARY_PROCEDURE reset;

extern S1AP_ELEMENTARY_PROCEDURE errorIndication;

extern S1AP_ELEMENTARY_PROCEDURE s1Setup;

extern S1AP_ELEMENTARY_PROCEDURE eNBConfigurationUpdate;

extern S1AP_ELEMENTARY_PROCEDURE mMEConfigurationUpdate;

extern S1AP_ELEMENTARY_PROCEDURE downlinkS1cdma2000tunnelling;

extern S1AP_ELEMENTARY_PROCEDURE uplinkS1cdma2000tunnelling;

extern S1AP_ELEMENTARY_PROCEDURE uEContextModification;

extern S1AP_ELEMENTARY_PROCEDURE uECapabilityInfoIndication;

extern S1AP_ELEMENTARY_PROCEDURE uEContextRelease;

extern S1AP_ELEMENTARY_PROCEDURE eNBStatusTransfer;

extern S1AP_ELEMENTARY_PROCEDURE mMEStatusTransfer;

extern S1AP_ELEMENTARY_PROCEDURE deactivateTrace;

extern S1AP_ELEMENTARY_PROCEDURE traceStart;

extern S1AP_ELEMENTARY_PROCEDURE traceFailureIndication;

extern S1AP_ELEMENTARY_PROCEDURE cellTrafficTrace;

extern S1AP_ELEMENTARY_PROCEDURE locationReportingControl;

extern S1AP_ELEMENTARY_PROCEDURE locationReportingFailureIndication;

extern S1AP_ELEMENTARY_PROCEDURE locationReport;

extern S1AP_ELEMENTARY_PROCEDURE overloadStart;

extern S1AP_ELEMENTARY_PROCEDURE overloadStop;

extern S1AP_ELEMENTARY_PROCEDURE writeReplaceWarning;

extern S1AP_ELEMENTARY_PROCEDURE eNBDirectInformationTransfer;

extern S1AP_ELEMENTARY_PROCEDURE mMEDirectInformationTransfer;

extern S1AP_ELEMENTARY_PROCEDURE eNBConfigurationTransfer;

extern S1AP_ELEMENTARY_PROCEDURE mMEConfigurationTransfer;

extern S1AP_ELEMENTARY_PROCEDURE privateMessage;

extern S1AP_ELEMENTARY_PROCEDURE pWSRestartIndication;

extern S1AP_ELEMENTARY_PROCEDURE kill;

extern S1AP_ELEMENTARY_PROCEDURE downlinkUEAssociatedLPPaTransport;

extern S1AP_ELEMENTARY_PROCEDURE uplinkUEAssociatedLPPaTransport;

extern S1AP_ELEMENTARY_PROCEDURE downlinkNonUEAssociatedLPPaTransport;

extern S1AP_ELEMENTARY_PROCEDURE uplinkNonUEAssociatedLPPaTransport;

extern S1AP_ELEMENTARY_PROCEDURE uERadioCapabilityMatch;

extern S1AP_ELEMENTARY_PROCEDURE e_RABModificationIndication;

/* ************************************************************** */
/**/
/* IE parameter types from other modules. */
/**/
/* ************************************************************** */
/* ************************************************************** */
/**/
/* Elementary Procedures */
/**/
/* ************************************************************** */
extern const ProcedureCode id_HandoverPreparation;

extern const ProcedureCode id_HandoverResourceAllocation;

extern const ProcedureCode id_HandoverNotification;

extern const ProcedureCode id_PathSwitchRequest;

extern const ProcedureCode id_HandoverCancel;

extern const ProcedureCode id_E_RABSetup;

extern const ProcedureCode id_E_RABModify;

extern const ProcedureCode id_E_RABRelease;

extern const ProcedureCode id_E_RABReleaseIndication;

extern const ProcedureCode id_InitialContextSetup;

extern const ProcedureCode id_Paging;

extern const ProcedureCode id_downlinkNASTransport;

extern const ProcedureCode id_initialUEMessage;

extern const ProcedureCode id_uplinkNASTransport;

extern const ProcedureCode id_Reset;

extern const ProcedureCode id_ErrorIndication;

extern const ProcedureCode id_NASNonDeliveryIndication;

extern const ProcedureCode id_S1Setup;

extern const ProcedureCode id_UEContextReleaseRequest;

extern const ProcedureCode id_DownlinkS1cdma2000tunnelling;

extern const ProcedureCode id_UplinkS1cdma2000tunnelling;

extern const ProcedureCode id_UEContextModification;

extern const ProcedureCode id_UECapabilityInfoIndication;

extern const ProcedureCode id_UEContextRelease;

extern const ProcedureCode id_eNBStatusTransfer;

extern const ProcedureCode id_MMEStatusTransfer;

extern const ProcedureCode id_DeactivateTrace;

extern const ProcedureCode id_TraceStart;

extern const ProcedureCode id_TraceFailureIndication;

extern const ProcedureCode id_ENBConfigurationUpdate;

extern const ProcedureCode id_MMEConfigurationUpdate;

extern const ProcedureCode id_LocationReportingControl;

extern const ProcedureCode id_LocationReportingFailureIndication;

extern const ProcedureCode id_LocationReport;

extern const ProcedureCode id_OverloadStart;

extern const ProcedureCode id_OverloadStop;

extern const ProcedureCode id_WriteReplaceWarning;

extern const ProcedureCode id_eNBDirectInformationTransfer;

extern const ProcedureCode id_MMEDirectInformationTransfer;

extern const ProcedureCode id_PrivateMessage;

extern const ProcedureCode id_eNBConfigurationTransfer;

extern const ProcedureCode id_MMEConfigurationTransfer;

extern const ProcedureCode id_CellTrafficTrace;

extern const ProcedureCode id_Kill;

extern const ProcedureCode id_downlinkUEAssociatedLPPaTransport;

extern const ProcedureCode id_uplinkUEAssociatedLPPaTransport;

extern const ProcedureCode id_downlinkNonUEAssociatedLPPaTransport;

extern const ProcedureCode id_uplinkNonUEAssociatedLPPaTransport;

extern const ProcedureCode id_UERadioCapabilityMatch;

extern const ProcedureCode id_PWSRestartIndication;

extern const ProcedureCode id_E_RABModificationIndication;

/* ************************************************************** */
/**/
/* Extension constants */
/**/
/* ************************************************************** */
extern const int maxPrivateIEs;

extern const int maxProtocolExtensions;

extern const int maxProtocolIEs;

/* ************************************************************** */
/**/
/* Lists */
/**/
/* ************************************************************** */
extern const int maxnoofCSGs;

extern const int maxnoofE_RABs;

extern const int maxnoofTAIs;

extern const int maxnoofTACs;

extern const int maxnoofErrors;

extern const int maxnoofBPLMNs;

extern const int maxnoofPLMNsPerMME;

extern const int maxnoofEPLMNs;

extern const int maxnoofEPLMNsPlusOne;

extern const int maxnoofForbLACs;

extern const int maxnoofForbTACs;

extern const int maxnoofIndividualS1ConnectionsToReset;

extern const int maxnoofCells;

extern const int maxnoofCellsineNB;

extern const int maxnoofTAIforWarning;

extern const int maxnoofCellID;

extern const int maxnoofEmergencyAreaID;

extern const int maxnoofCellinTAI;

extern const int maxnoofCellinEAI;

extern const int maxnoofeNBX2TLAs;

extern const int maxnoofeNBX2ExtTLAs;

extern const int maxnoofeNBX2GTPTLAs;

extern const int maxnoofRATs;

extern const int maxnoofGroupIDs;

extern const int maxnoofMMECs;

extern const int maxnoofCellIDforMDT;

extern const int maxnoofTAforMDT;

extern const int maxnoofMDTPLMNs;

extern const int maxnoofCellsforRestart;

extern const int maxnoofRestartTAIs;

extern const int maxnoofRestartEmergencyAreaIDs;

extern const int maxEARFCN;

extern const int maxnoofMBSFNAreaMDT;

/* ************************************************************** */
/**/
/* IEs */
/**/
/* ************************************************************** */
extern const ProtocolIE_ID id_MME_UE_S1AP_ID;

extern const ProtocolIE_ID id_HandoverType;

extern const ProtocolIE_ID id_Cause;

extern const ProtocolIE_ID id_SourceID;

extern const ProtocolIE_ID id_TargetID;

extern const ProtocolIE_ID id_eNB_UE_S1AP_ID;

extern const ProtocolIE_ID id_E_RABSubjecttoDataForwardingList;

extern const ProtocolIE_ID id_E_RABtoReleaseListHOCmd;

extern const ProtocolIE_ID id_E_RABDataForwardingItem;

extern const ProtocolIE_ID id_E_RABReleaseItemBearerRelComp;

extern const ProtocolIE_ID id_E_RABToBeSetupListBearerSUReq;

extern const ProtocolIE_ID id_E_RABToBeSetupItemBearerSUReq;

extern const ProtocolIE_ID id_E_RABAdmittedList;

extern const ProtocolIE_ID id_E_RABFailedToSetupListHOReqAck;

extern const ProtocolIE_ID id_E_RABAdmittedItem;

extern const ProtocolIE_ID id_E_RABFailedtoSetupItemHOReqAck;

extern const ProtocolIE_ID id_E_RABToBeSwitchedDLList;

extern const ProtocolIE_ID id_E_RABToBeSwitchedDLItem;

extern const ProtocolIE_ID id_E_RABToBeSetupListCtxtSUReq;

extern const ProtocolIE_ID id_TraceActivation;

extern const ProtocolIE_ID id_NAS_PDU;

extern const ProtocolIE_ID id_E_RABToBeSetupItemHOReq;

extern const ProtocolIE_ID id_E_RABSetupListBearerSURes;

extern const ProtocolIE_ID id_E_RABFailedToSetupListBearerSURes;

extern const ProtocolIE_ID id_E_RABToBeModifiedListBearerModReq;

extern const ProtocolIE_ID id_E_RABModifyListBearerModRes;

extern const ProtocolIE_ID id_E_RABFailedToModifyList;

extern const ProtocolIE_ID id_E_RABToBeReleasedList;

extern const ProtocolIE_ID id_E_RABFailedToReleaseList;

extern const ProtocolIE_ID id_E_RABItem;

extern const ProtocolIE_ID id_E_RABToBeModifiedItemBearerModReq;

extern const ProtocolIE_ID id_E_RABModifyItemBearerModRes;

extern const ProtocolIE_ID id_E_RABReleaseItem;

extern const ProtocolIE_ID id_E_RABSetupItemBearerSURes;

extern const ProtocolIE_ID id_SecurityContext;

extern const ProtocolIE_ID id_HandoverRestrictionList;

extern const ProtocolIE_ID id_UEPagingID;

extern const ProtocolIE_ID id_pagingDRX;

extern const ProtocolIE_ID id_TAIList;

extern const ProtocolIE_ID id_TAIItem;

extern const ProtocolIE_ID id_E_RABFailedToSetupListCtxtSURes;

extern const ProtocolIE_ID id_E_RABReleaseItemHOCmd;

extern const ProtocolIE_ID id_E_RABSetupItemCtxtSURes;

extern const ProtocolIE_ID id_E_RABSetupListCtxtSURes;

extern const ProtocolIE_ID id_E_RABToBeSetupItemCtxtSUReq;

extern const ProtocolIE_ID id_E_RABToBeSetupListHOReq;

extern const ProtocolIE_ID id_GERANtoLTEHOInformationRes;

extern const ProtocolIE_ID id_UTRANtoLTEHOInformationRes;

extern const ProtocolIE_ID id_CriticalityDiagnostics;

extern const ProtocolIE_ID id_Global_ENB_ID;

extern const ProtocolIE_ID id_eNBname;

extern const ProtocolIE_ID id_MMEname;

extern const ProtocolIE_ID id_ServedPLMNs;

extern const ProtocolIE_ID id_SupportedTAs;

extern const ProtocolIE_ID id_TimeToWait;

extern const ProtocolIE_ID id_uEaggregateMaximumBitrate;

extern const ProtocolIE_ID id_TAI;

extern const ProtocolIE_ID id_E_RABReleaseListBearerRelComp;

extern const ProtocolIE_ID id_cdma2000PDU;

extern const ProtocolIE_ID id_cdma2000RATType;

extern const ProtocolIE_ID id_cdma2000SectorID;

extern const ProtocolIE_ID id_SecurityKey;

extern const ProtocolIE_ID id_UERadioCapability;

extern const ProtocolIE_ID id_GUMMEI_ID;

extern const ProtocolIE_ID id_E_RABInformationListItem;

extern const ProtocolIE_ID id_Direct_Forwarding_Path_Availability;

extern const ProtocolIE_ID id_UEIdentityIndexValue;

extern const ProtocolIE_ID id_cdma2000HOStatus;

extern const ProtocolIE_ID id_cdma2000HORequiredIndication;

extern const ProtocolIE_ID id_E_UTRAN_Trace_ID;

extern const ProtocolIE_ID id_RelativeMMECapacity;

extern const ProtocolIE_ID id_SourceMME_UE_S1AP_ID;

extern const ProtocolIE_ID id_Bearers_SubjectToStatusTransfer_Item;

extern const ProtocolIE_ID id_eNB_StatusTransfer_TransparentContainer;

extern const ProtocolIE_ID id_UE_associatedLogicalS1_ConnectionItem;

extern const ProtocolIE_ID id_ResetType;

extern const ProtocolIE_ID id_UE_associatedLogicalS1_ConnectionListResAck;

extern const ProtocolIE_ID id_E_RABToBeSwitchedULItem;

extern const ProtocolIE_ID id_E_RABToBeSwitchedULList;

extern const ProtocolIE_ID id_S_TMSI;

extern const ProtocolIE_ID id_cdma2000OneXRAND;

extern const ProtocolIE_ID id_RequestType;

extern const ProtocolIE_ID id_UE_S1AP_IDs;

extern const ProtocolIE_ID id_EUTRAN_CGI;

extern const ProtocolIE_ID id_OverloadResponse;

extern const ProtocolIE_ID id_cdma2000OneXSRVCCInfo;

extern const ProtocolIE_ID id_E_RABFailedToBeReleasedList;

extern const ProtocolIE_ID id_Source_ToTarget_TransparentContainer;

extern const ProtocolIE_ID id_ServedGUMMEIs;

extern const ProtocolIE_ID id_SubscriberProfileIDforRFP;

extern const ProtocolIE_ID id_UESecurityCapabilities;

extern const ProtocolIE_ID id_CSFallbackIndicator;

extern const ProtocolIE_ID id_CNDomain;

extern const ProtocolIE_ID id_E_RABReleasedList;

extern const ProtocolIE_ID id_MessageIdentifier;

extern const ProtocolIE_ID id_SerialNumber;

extern const ProtocolIE_ID id_WarningAreaList;

extern const ProtocolIE_ID id_RepetitionPeriod;

extern const ProtocolIE_ID id_NumberofBroadcastRequest;

extern const ProtocolIE_ID id_WarningType;

extern const ProtocolIE_ID id_WarningSecurityInfo;

extern const ProtocolIE_ID id_DataCodingScheme;

extern const ProtocolIE_ID id_WarningMessageContents;

extern const ProtocolIE_ID id_BroadcastCompletedAreaList;

extern const ProtocolIE_ID id_Inter_SystemInformationTransferTypeEDT;

extern const ProtocolIE_ID id_Inter_SystemInformationTransferTypeMDT;

extern const ProtocolIE_ID id_Target_ToSource_TransparentContainer;

extern const ProtocolIE_ID id_SRVCCOperationPossible;

extern const ProtocolIE_ID id_SRVCCHOIndication;

extern const ProtocolIE_ID id_NAS_DownlinkCount;

extern const ProtocolIE_ID id_CSG_Id;

extern const ProtocolIE_ID id_CSG_IdList;

extern const ProtocolIE_ID id_SONConfigurationTransferECT;

extern const ProtocolIE_ID id_SONConfigurationTransferMCT;

extern const ProtocolIE_ID id_TraceCollectionEntityIPAddress;

extern const ProtocolIE_ID id_MSClassmark2;

extern const ProtocolIE_ID id_MSClassmark3;

extern const ProtocolIE_ID id_RRC_Establishment_Cause;

extern const ProtocolIE_ID id_NASSecurityParametersfromE_UTRAN;

extern const ProtocolIE_ID id_NASSecurityParameterstoE_UTRAN;

extern const ProtocolIE_ID id_DefaultPagingDRX;

extern const ProtocolIE_ID id_Source_ToTarget_TransparentContainer_Secondary;

extern const ProtocolIE_ID id_Target_ToSource_TransparentContainer_Secondary;

extern const ProtocolIE_ID id_EUTRANRoundTripDelayEstimationInfo;

extern const ProtocolIE_ID id_BroadcastCancelledAreaList;

extern const ProtocolIE_ID id_ConcurrentWarningMessageIndicator;

extern const ProtocolIE_ID id_Data_Forwarding_Not_Possible;

extern const ProtocolIE_ID id_ExtendedRepetitionPeriod;

extern const ProtocolIE_ID id_CellAccessMode;

extern const ProtocolIE_ID id_CSGMembershipStatus;

extern const ProtocolIE_ID id_LPPa_PDU;

extern const ProtocolIE_ID id_Routing_ID;

extern const ProtocolIE_ID id_Time_Synchronisation_Info;

extern const ProtocolIE_ID id_PS_ServiceNotAvailable;

extern const ProtocolIE_ID id_PagingPriority;

extern const ProtocolIE_ID id_x2TNLConfigurationInfo;

extern const ProtocolIE_ID id_eNBX2ExtendedTransportLayerAddresses;

extern const ProtocolIE_ID id_GUMMEIList;

extern const ProtocolIE_ID id_GW_TransportLayerAddress;

extern const ProtocolIE_ID id_Correlation_ID;

extern const ProtocolIE_ID id_SourceMME_GUMMEI;

extern const ProtocolIE_ID id_MME_UE_S1AP_ID_2;

extern const ProtocolIE_ID id_RegisteredLAI;

extern const ProtocolIE_ID id_RelayNode_Indicator;

extern const ProtocolIE_ID id_TrafficLoadReductionIndication;

extern const ProtocolIE_ID id_MDTConfiguration;

extern const ProtocolIE_ID id_MMERelaySupportIndicator;

extern const ProtocolIE_ID id_GWContextReleaseIndication;

extern const ProtocolIE_ID id_ManagementBasedMDTAllowed;

extern const ProtocolIE_ID id_PrivacyIndicator;

extern const ProtocolIE_ID id_Time_UE_StayedInCell_EnhancedGranularity;

extern const ProtocolIE_ID id_HO_Cause;

extern const ProtocolIE_ID id_VoiceSupportMatchIndicator;

extern const ProtocolIE_ID id_GUMMEIType;

extern const ProtocolIE_ID id_M3Configuration;

extern const ProtocolIE_ID id_M4Configuration;

extern const ProtocolIE_ID id_M5Configuration;

extern const ProtocolIE_ID id_MDT_Location_Info;

extern const ProtocolIE_ID id_MobilityInformation;

extern const ProtocolIE_ID id_Tunnel_Information_for_BBF;

extern const ProtocolIE_ID id_ManagementBasedMDTPLMNList;

extern const ProtocolIE_ID id_SignallingBasedMDTPLMNList;

extern const ProtocolIE_ID id_ULCOUNTValueExtended;

extern const ProtocolIE_ID id_DLCOUNTValueExtended;

extern const ProtocolIE_ID id_ReceiveStatusOfULPDCPSDUsExtended;

extern const ProtocolIE_ID id_ECGIListForRestart;

extern const ProtocolIE_ID id_SIPTO_Correlation_ID;

extern const ProtocolIE_ID id_SIPTO_L_GW_TransportLayerAddress;

extern const ProtocolIE_ID id_TransportInformation;

extern const ProtocolIE_ID id_LHN_ID;

extern const ProtocolIE_ID id_AdditionalCSFallbackIndicator;

extern const ProtocolIE_ID id_TAIListForRestart;

extern const ProtocolIE_ID id_UserLocationInformation;

extern const ProtocolIE_ID id_EmergencyAreaIDListForRestart;

extern const ProtocolIE_ID id_KillAllWarningMessages;

extern const ProtocolIE_ID id_Masked_IMEISV;

extern const ProtocolIE_ID id_eNBIndirectX2TransportLayerAddresses;

extern const ProtocolIE_ID id_uE_HistoryInformationFromTheUE;

extern const ProtocolIE_ID id_ProSeAuthorized;

extern const ProtocolIE_ID id_ExpectedUEBehaviour;

extern const ProtocolIE_ID id_LoggedMBSFNMDT;

extern const ProtocolIE_ID id_UERadioCapabilityForPaging;

extern const ProtocolIE_ID id_E_RABToBeModifiedListBearerModInd;

extern const ProtocolIE_ID id_E_RABToBeModifiedItemBearerModInd;

extern const ProtocolIE_ID id_E_RABNotToBeModifiedListBearerModInd;

extern const ProtocolIE_ID id_E_RABNotToBeModifiedItemBearerModInd;

extern const ProtocolIE_ID id_E_RABModifyListBearerModConf;

extern const ProtocolIE_ID id_E_RABModifyItemBearerModConf;

extern const ProtocolIE_ID id_E_RABFailedToModifyListBearerModConf;

extern const ProtocolIE_ID id_SON_Information_Report;

extern const ProtocolIE_ID id_Muting_Availability_Indication;

extern const ProtocolIE_ID id_Muting_Pattern_Information;

extern const ProtocolIE_ID id_Synchronisation_Information;

extern const ProtocolIE_ID id_E_RABToBeReleasedListBearerModConf;

#endif  /* #ifndef _OSSNOVALUES */


extern void * const s1ap;    /* encoder-decoder control table */
#endif /* OSS_s1ap */
