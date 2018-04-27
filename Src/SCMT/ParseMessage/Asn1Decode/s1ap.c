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

#define    OSS_SOED_PER
#include   "osstype.h"
#include   "s1ap.h"

S1AP_ELEMENTARY_PROCEDURE handoverPreparation = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    2,
    3,
    6,
    0,
    reject
};

S1AP_ELEMENTARY_PROCEDURE handoverResourceAllocation = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    7,
    10,
    15,
    1,
    reject
};

S1AP_ELEMENTARY_PROCEDURE handoverNotification = {
    criticality_present,
    16,
    0,
    0,
    2,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE pathSwitchRequest = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    17,
    20,
    23,
    3,
    reject
};

S1AP_ELEMENTARY_PROCEDURE e_RABSetup = {
    SuccessfulOutcome_present | criticality_present,
    26,
    29,
    0,
    5,
    reject
};

S1AP_ELEMENTARY_PROCEDURE e_RABModify = {
    SuccessfulOutcome_present | criticality_present,
    32,
    35,
    0,
    6,
    reject
};

S1AP_ELEMENTARY_PROCEDURE e_RABRelease = {
    SuccessfulOutcome_present | criticality_present,
    38,
    39,
    0,
    7,
    reject
};

S1AP_ELEMENTARY_PROCEDURE e_RABReleaseIndication = {
    criticality_present,
    42,
    0,
    0,
    8,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE initialContextSetup = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    43,
    46,
    49,
    9,
    reject
};

S1AP_ELEMENTARY_PROCEDURE uEContextReleaseRequest = {
    criticality_present,
    53,
    0,
    0,
    18,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE paging = {
    criticality_present,
    50,
    0,
    0,
    10,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE downlinkNASTransport = {
    criticality_present,
    61,
    0,
    0,
    11,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE initialUEMessage = {
    criticality_present,
    62,
    0,
    0,
    12,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE uplinkNASTransport = {
    criticality_present,
    63,
    0,
    0,
    13,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE nASNonDeliveryIndication = {
    criticality_present,
    64,
    0,
    0,
    16,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE handoverCancel = {
    SuccessfulOutcome_present | criticality_present,
    24,
    25,
    0,
    4,
    reject
};

S1AP_ELEMENTARY_PROCEDURE reset = {
    SuccessfulOutcome_present | criticality_present,
    65,
    67,
    0,
    14,
    reject
};

S1AP_ELEMENTARY_PROCEDURE errorIndication = {
    criticality_present,
    69,
    0,
    0,
    15,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE s1Setup = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    70,
    71,
    72,
    17,
    reject
};

S1AP_ELEMENTARY_PROCEDURE eNBConfigurationUpdate = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    73,
    74,
    75,
    29,
    reject
};

S1AP_ELEMENTARY_PROCEDURE mMEConfigurationUpdate = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    76,
    77,
    78,
    30,
    reject
};

S1AP_ELEMENTARY_PROCEDURE downlinkS1cdma2000tunnelling = {
    criticality_present,
    79,
    0,
    0,
    19,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE uplinkS1cdma2000tunnelling = {
    criticality_present,
    80,
    0,
    0,
    20,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE uEContextModification = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    56,
    57,
    58,
    21,
    reject
};

S1AP_ELEMENTARY_PROCEDURE uECapabilityInfoIndication = {
    criticality_present,
    81,
    0,
    0,
    22,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE uEContextRelease = {
    SuccessfulOutcome_present | criticality_present,
    54,
    55,
    0,
    23,
    reject
};

S1AP_ELEMENTARY_PROCEDURE eNBStatusTransfer = {
    criticality_present,
    82,
    0,
    0,
    24,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE mMEStatusTransfer = {
    criticality_present,
    83,
    0,
    0,
    25,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE deactivateTrace = {
    criticality_present,
    86,
    0,
    0,
    26,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE traceStart = {
    criticality_present,
    84,
    0,
    0,
    27,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE traceFailureIndication = {
    criticality_present,
    85,
    0,
    0,
    28,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE cellTrafficTrace = {
    criticality_present,
    87,
    0,
    0,
    42,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE locationReportingControl = {
    criticality_present,
    88,
    0,
    0,
    31,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE locationReportingFailureIndication = {
    criticality_present,
    89,
    0,
    0,
    32,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE locationReport = {
    criticality_present,
    90,
    0,
    0,
    33,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE overloadStart = {
    criticality_present,
    91,
    0,
    0,
    34,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE overloadStop = {
    criticality_present,
    92,
    0,
    0,
    35,
    reject
};

S1AP_ELEMENTARY_PROCEDURE writeReplaceWarning = {
    SuccessfulOutcome_present | criticality_present,
    93,
    94,
    0,
    36,
    reject
};

S1AP_ELEMENTARY_PROCEDURE eNBDirectInformationTransfer = {
    criticality_present,
    95,
    0,
    0,
    37,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE mMEDirectInformationTransfer = {
    criticality_present,
    97,
    0,
    0,
    38,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE eNBConfigurationTransfer = {
    criticality_present,
    98,
    0,
    0,
    40,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE mMEConfigurationTransfer = {
    criticality_present,
    99,
    0,
    0,
    41,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE privateMessage = {
    criticality_present,
    100,
    0,
    0,
    39,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE pWSRestartIndication = {
    criticality_present,
    103,
    0,
    0,
    49,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE kill = {
    SuccessfulOutcome_present | criticality_present,
    101,
    102,
    0,
    43,
    reject
};

S1AP_ELEMENTARY_PROCEDURE downlinkUEAssociatedLPPaTransport = {
    criticality_present,
    104,
    0,
    0,
    44,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE uplinkUEAssociatedLPPaTransport = {
    criticality_present,
    105,
    0,
    0,
    45,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE downlinkNonUEAssociatedLPPaTransport = {
    criticality_present,
    106,
    0,
    0,
    46,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE uplinkNonUEAssociatedLPPaTransport = {
    criticality_present,
    107,
    0,
    0,
    47,
    ignore
};

S1AP_ELEMENTARY_PROCEDURE uERadioCapabilityMatch = {
    SuccessfulOutcome_present | criticality_present,
    59,
    60,
    0,
    48,
    reject
};

S1AP_ELEMENTARY_PROCEDURE e_RABModificationIndication = {
    SuccessfulOutcome_present | criticality_present,
    108,
    113,
    0,
    50,
    reject
};

const ProcedureCode id_HandoverPreparation = 0;

const ProcedureCode id_HandoverResourceAllocation = 1;

const ProcedureCode id_HandoverNotification = 2;

const ProcedureCode id_PathSwitchRequest = 3;

const ProcedureCode id_HandoverCancel = 4;

const ProcedureCode id_E_RABSetup = 5;

const ProcedureCode id_E_RABModify = 6;

const ProcedureCode id_E_RABRelease = 7;

const ProcedureCode id_E_RABReleaseIndication = 8;

const ProcedureCode id_InitialContextSetup = 9;

const ProcedureCode id_Paging = 10;

const ProcedureCode id_downlinkNASTransport = 11;

const ProcedureCode id_initialUEMessage = 12;

const ProcedureCode id_uplinkNASTransport = 13;

const ProcedureCode id_Reset = 14;

const ProcedureCode id_ErrorIndication = 15;

const ProcedureCode id_NASNonDeliveryIndication = 16;

const ProcedureCode id_S1Setup = 17;

const ProcedureCode id_UEContextReleaseRequest = 18;

const ProcedureCode id_DownlinkS1cdma2000tunnelling = 19;

const ProcedureCode id_UplinkS1cdma2000tunnelling = 20;

const ProcedureCode id_UEContextModification = 21;

const ProcedureCode id_UECapabilityInfoIndication = 22;

const ProcedureCode id_UEContextRelease = 23;

const ProcedureCode id_eNBStatusTransfer = 24;

const ProcedureCode id_MMEStatusTransfer = 25;

const ProcedureCode id_DeactivateTrace = 26;

const ProcedureCode id_TraceStart = 27;

const ProcedureCode id_TraceFailureIndication = 28;

const ProcedureCode id_ENBConfigurationUpdate = 29;

const ProcedureCode id_MMEConfigurationUpdate = 30;

const ProcedureCode id_LocationReportingControl = 31;

const ProcedureCode id_LocationReportingFailureIndication = 32;

const ProcedureCode id_LocationReport = 33;

const ProcedureCode id_OverloadStart = 34;

const ProcedureCode id_OverloadStop = 35;

const ProcedureCode id_WriteReplaceWarning = 36;

const ProcedureCode id_eNBDirectInformationTransfer = 37;

const ProcedureCode id_MMEDirectInformationTransfer = 38;

const ProcedureCode id_PrivateMessage = 39;

const ProcedureCode id_eNBConfigurationTransfer = 40;

const ProcedureCode id_MMEConfigurationTransfer = 41;

const ProcedureCode id_CellTrafficTrace = 42;

const ProcedureCode id_Kill = 43;

const ProcedureCode id_downlinkUEAssociatedLPPaTransport = 44;

const ProcedureCode id_uplinkUEAssociatedLPPaTransport = 45;

const ProcedureCode id_downlinkNonUEAssociatedLPPaTransport = 46;

const ProcedureCode id_uplinkNonUEAssociatedLPPaTransport = 47;

const ProcedureCode id_UERadioCapabilityMatch = 48;

const ProcedureCode id_PWSRestartIndication = 49;

const ProcedureCode id_E_RABModificationIndication = 50;

const int maxPrivateIEs = USHRT_MAX;

const int maxProtocolExtensions = USHRT_MAX;

const int maxProtocolIEs = USHRT_MAX;

const int maxnoofCSGs = 256;

const int maxnoofE_RABs = 256;

const int maxnoofTAIs = 256;

const int maxnoofTACs = 256;

const int maxnoofErrors = 256;

const int maxnoofBPLMNs = 6;

const int maxnoofPLMNsPerMME = 32;

const int maxnoofEPLMNs = 15;

const int maxnoofEPLMNsPlusOne = 16;

const int maxnoofForbLACs = 4096;

const int maxnoofForbTACs = 4096;

const int maxnoofIndividualS1ConnectionsToReset = 256;

const int maxnoofCells = 16;

const int maxnoofCellsineNB = 256;

const int maxnoofTAIforWarning = USHRT_MAX;

const int maxnoofCellID = USHRT_MAX;

const int maxnoofEmergencyAreaID = USHRT_MAX;

const int maxnoofCellinTAI = USHRT_MAX;

const int maxnoofCellinEAI = USHRT_MAX;

const int maxnoofeNBX2TLAs = 2;

const int maxnoofeNBX2ExtTLAs = 16;

const int maxnoofeNBX2GTPTLAs = 16;

const int maxnoofRATs = 8;

const int maxnoofGroupIDs = USHRT_MAX;

const int maxnoofMMECs = 256;

const int maxnoofCellIDforMDT = 32;

const int maxnoofTAforMDT = 8;

const int maxnoofMDTPLMNs = 16;

const int maxnoofCellsforRestart = 256;

const int maxnoofRestartTAIs = 2048;

const int maxnoofRestartEmergencyAreaIDs = 256;

//const int maxEARFCN = 262143;

const int maxnoofMBSFNAreaMDT = 8;

const ProtocolIE_ID id_MME_UE_S1AP_ID = 0;

const ProtocolIE_ID id_HandoverType = 1;

const ProtocolIE_ID id_Cause = 2;

const ProtocolIE_ID id_SourceID = 3;

const ProtocolIE_ID id_TargetID = 4;

const ProtocolIE_ID id_eNB_UE_S1AP_ID = 8;

const ProtocolIE_ID id_E_RABSubjecttoDataForwardingList = 12;

const ProtocolIE_ID id_E_RABtoReleaseListHOCmd = 13;

const ProtocolIE_ID id_E_RABDataForwardingItem = 14;

const ProtocolIE_ID id_E_RABReleaseItemBearerRelComp = 15;

const ProtocolIE_ID id_E_RABToBeSetupListBearerSUReq = 16;

const ProtocolIE_ID id_E_RABToBeSetupItemBearerSUReq = 17;

const ProtocolIE_ID id_E_RABAdmittedList = 18;

const ProtocolIE_ID id_E_RABFailedToSetupListHOReqAck = 19;

const ProtocolIE_ID id_E_RABAdmittedItem = 20;

const ProtocolIE_ID id_E_RABFailedtoSetupItemHOReqAck = 21;

const ProtocolIE_ID id_E_RABToBeSwitchedDLList = 22;

const ProtocolIE_ID id_E_RABToBeSwitchedDLItem = 23;

const ProtocolIE_ID id_E_RABToBeSetupListCtxtSUReq = 24;

const ProtocolIE_ID id_TraceActivation = 25;

const ProtocolIE_ID id_NAS_PDU = 26;

const ProtocolIE_ID id_E_RABToBeSetupItemHOReq = 27;

const ProtocolIE_ID id_E_RABSetupListBearerSURes = 28;

const ProtocolIE_ID id_E_RABFailedToSetupListBearerSURes = 29;

const ProtocolIE_ID id_E_RABToBeModifiedListBearerModReq = 30;

const ProtocolIE_ID id_E_RABModifyListBearerModRes = 31;

const ProtocolIE_ID id_E_RABFailedToModifyList = 32;

const ProtocolIE_ID id_E_RABToBeReleasedList = 33;

const ProtocolIE_ID id_E_RABFailedToReleaseList = 34;

const ProtocolIE_ID id_E_RABItem = 35;

const ProtocolIE_ID id_E_RABToBeModifiedItemBearerModReq = 36;

const ProtocolIE_ID id_E_RABModifyItemBearerModRes = 37;

const ProtocolIE_ID id_E_RABReleaseItem = 38;

const ProtocolIE_ID id_E_RABSetupItemBearerSURes = 39;

const ProtocolIE_ID id_SecurityContext = 40;

const ProtocolIE_ID id_HandoverRestrictionList = 41;

const ProtocolIE_ID id_UEPagingID = 43;

const ProtocolIE_ID id_pagingDRX = 44;

const ProtocolIE_ID id_TAIList = 46;

const ProtocolIE_ID id_TAIItem = 47;

const ProtocolIE_ID id_E_RABFailedToSetupListCtxtSURes = 48;

const ProtocolIE_ID id_E_RABReleaseItemHOCmd = 49;

const ProtocolIE_ID id_E_RABSetupItemCtxtSURes = 50;

const ProtocolIE_ID id_E_RABSetupListCtxtSURes = 51;

const ProtocolIE_ID id_E_RABToBeSetupItemCtxtSUReq = 52;

const ProtocolIE_ID id_E_RABToBeSetupListHOReq = 53;

const ProtocolIE_ID id_GERANtoLTEHOInformationRes = 55;

const ProtocolIE_ID id_UTRANtoLTEHOInformationRes = 57;

const ProtocolIE_ID id_CriticalityDiagnostics = 58;

const ProtocolIE_ID id_Global_ENB_ID = 59;

const ProtocolIE_ID id_eNBname = 60;

const ProtocolIE_ID id_MMEname = 61;

const ProtocolIE_ID id_ServedPLMNs = 63;

const ProtocolIE_ID id_SupportedTAs = 64;

const ProtocolIE_ID id_TimeToWait = 65;

const ProtocolIE_ID id_uEaggregateMaximumBitrate = 66;

const ProtocolIE_ID id_TAI = 67;

const ProtocolIE_ID id_E_RABReleaseListBearerRelComp = 69;

const ProtocolIE_ID id_cdma2000PDU = 70;

const ProtocolIE_ID id_cdma2000RATType = 71;

const ProtocolIE_ID id_cdma2000SectorID = 72;

const ProtocolIE_ID id_SecurityKey = 73;

const ProtocolIE_ID id_UERadioCapability = 74;

const ProtocolIE_ID id_GUMMEI_ID = 75;

const ProtocolIE_ID id_E_RABInformationListItem = 78;

const ProtocolIE_ID id_Direct_Forwarding_Path_Availability = 79;

const ProtocolIE_ID id_UEIdentityIndexValue = 80;

const ProtocolIE_ID id_cdma2000HOStatus = 83;

const ProtocolIE_ID id_cdma2000HORequiredIndication = 84;

const ProtocolIE_ID id_E_UTRAN_Trace_ID = 86;

const ProtocolIE_ID id_RelativeMMECapacity = 87;

const ProtocolIE_ID id_SourceMME_UE_S1AP_ID = 88;

const ProtocolIE_ID id_Bearers_SubjectToStatusTransfer_Item = 89;

const ProtocolIE_ID id_eNB_StatusTransfer_TransparentContainer = 90;

const ProtocolIE_ID id_UE_associatedLogicalS1_ConnectionItem = 91;

const ProtocolIE_ID id_ResetType = 92;

const ProtocolIE_ID id_UE_associatedLogicalS1_ConnectionListResAck = 93;

const ProtocolIE_ID id_E_RABToBeSwitchedULItem = 94;

const ProtocolIE_ID id_E_RABToBeSwitchedULList = 95;

const ProtocolIE_ID id_S_TMSI = 96;

const ProtocolIE_ID id_cdma2000OneXRAND = 97;

const ProtocolIE_ID id_RequestType = 98;

const ProtocolIE_ID id_UE_S1AP_IDs = 99;

const ProtocolIE_ID id_EUTRAN_CGI = 100;

const ProtocolIE_ID id_OverloadResponse = 101;

const ProtocolIE_ID id_cdma2000OneXSRVCCInfo = 102;

const ProtocolIE_ID id_E_RABFailedToBeReleasedList = 103;

const ProtocolIE_ID id_Source_ToTarget_TransparentContainer = 104;

const ProtocolIE_ID id_ServedGUMMEIs = 105;

const ProtocolIE_ID id_SubscriberProfileIDforRFP = 106;

const ProtocolIE_ID id_UESecurityCapabilities = 107;

const ProtocolIE_ID id_CSFallbackIndicator = 108;

const ProtocolIE_ID id_CNDomain = 109;

const ProtocolIE_ID id_E_RABReleasedList = 110;

const ProtocolIE_ID id_MessageIdentifier = 111;

const ProtocolIE_ID id_SerialNumber = 112;

const ProtocolIE_ID id_WarningAreaList = 113;

const ProtocolIE_ID id_RepetitionPeriod = 114;

const ProtocolIE_ID id_NumberofBroadcastRequest = 115;

const ProtocolIE_ID id_WarningType = 116;

const ProtocolIE_ID id_WarningSecurityInfo = 117;

const ProtocolIE_ID id_DataCodingScheme = 118;

const ProtocolIE_ID id_WarningMessageContents = 119;

const ProtocolIE_ID id_BroadcastCompletedAreaList = 120;

const ProtocolIE_ID id_Inter_SystemInformationTransferTypeEDT = 121;

const ProtocolIE_ID id_Inter_SystemInformationTransferTypeMDT = 122;

const ProtocolIE_ID id_Target_ToSource_TransparentContainer = 123;

const ProtocolIE_ID id_SRVCCOperationPossible = 124;

const ProtocolIE_ID id_SRVCCHOIndication = 125;

const ProtocolIE_ID id_NAS_DownlinkCount = 126;

const ProtocolIE_ID id_CSG_Id = 127;

const ProtocolIE_ID id_CSG_IdList = 128;

const ProtocolIE_ID id_SONConfigurationTransferECT = 129;

const ProtocolIE_ID id_SONConfigurationTransferMCT = 130;

const ProtocolIE_ID id_TraceCollectionEntityIPAddress = 131;

const ProtocolIE_ID id_MSClassmark2 = 132;

const ProtocolIE_ID id_MSClassmark3 = 133;

const ProtocolIE_ID id_RRC_Establishment_Cause = 134;

const ProtocolIE_ID id_NASSecurityParametersfromE_UTRAN = 135;

const ProtocolIE_ID id_NASSecurityParameterstoE_UTRAN = 136;

const ProtocolIE_ID id_DefaultPagingDRX = 137;

const ProtocolIE_ID id_Source_ToTarget_TransparentContainer_Secondary = 138;

const ProtocolIE_ID id_Target_ToSource_TransparentContainer_Secondary = 139;

const ProtocolIE_ID id_EUTRANRoundTripDelayEstimationInfo = 140;

const ProtocolIE_ID id_BroadcastCancelledAreaList = 141;

const ProtocolIE_ID id_ConcurrentWarningMessageIndicator = 142;

const ProtocolIE_ID id_Data_Forwarding_Not_Possible = 143;

const ProtocolIE_ID id_ExtendedRepetitionPeriod = 144;

const ProtocolIE_ID id_CellAccessMode = 145;

const ProtocolIE_ID id_CSGMembershipStatus = 146;

const ProtocolIE_ID id_LPPa_PDU = 147;

const ProtocolIE_ID id_Routing_ID = 148;

const ProtocolIE_ID id_Time_Synchronisation_Info = 149;

const ProtocolIE_ID id_PS_ServiceNotAvailable = 150;

const ProtocolIE_ID id_PagingPriority = 151;

const ProtocolIE_ID id_x2TNLConfigurationInfo = 152;

const ProtocolIE_ID id_eNBX2ExtendedTransportLayerAddresses = 153;

const ProtocolIE_ID id_GUMMEIList = 154;

const ProtocolIE_ID id_GW_TransportLayerAddress = 155;

const ProtocolIE_ID id_Correlation_ID = 156;

const ProtocolIE_ID id_SourceMME_GUMMEI = 157;

const ProtocolIE_ID id_MME_UE_S1AP_ID_2 = 158;

const ProtocolIE_ID id_RegisteredLAI = 159;

const ProtocolIE_ID id_RelayNode_Indicator = 160;

const ProtocolIE_ID id_TrafficLoadReductionIndication = 161;

const ProtocolIE_ID id_MDTConfiguration = 162;

const ProtocolIE_ID id_MMERelaySupportIndicator = 163;

const ProtocolIE_ID id_GWContextReleaseIndication = 164;

const ProtocolIE_ID id_ManagementBasedMDTAllowed = 165;

const ProtocolIE_ID id_PrivacyIndicator = 166;

const ProtocolIE_ID id_Time_UE_StayedInCell_EnhancedGranularity = 167;

const ProtocolIE_ID id_HO_Cause = 168;

const ProtocolIE_ID id_VoiceSupportMatchIndicator = 169;

const ProtocolIE_ID id_GUMMEIType = 170;

const ProtocolIE_ID id_M3Configuration = 171;

const ProtocolIE_ID id_M4Configuration = 172;

const ProtocolIE_ID id_M5Configuration = 173;

const ProtocolIE_ID id_MDT_Location_Info = 174;

const ProtocolIE_ID id_MobilityInformation = 175;

const ProtocolIE_ID id_Tunnel_Information_for_BBF = 176;

const ProtocolIE_ID id_ManagementBasedMDTPLMNList = 177;

const ProtocolIE_ID id_SignallingBasedMDTPLMNList = 178;

const ProtocolIE_ID id_ULCOUNTValueExtended = 179;

const ProtocolIE_ID id_DLCOUNTValueExtended = 180;

const ProtocolIE_ID id_ReceiveStatusOfULPDCPSDUsExtended = 181;

const ProtocolIE_ID id_ECGIListForRestart = 182;

const ProtocolIE_ID id_SIPTO_Correlation_ID = 183;

const ProtocolIE_ID id_SIPTO_L_GW_TransportLayerAddress = 184;

const ProtocolIE_ID id_TransportInformation = 185;

const ProtocolIE_ID id_LHN_ID = 186;

const ProtocolIE_ID id_AdditionalCSFallbackIndicator = 187;

const ProtocolIE_ID id_TAIListForRestart = 188;

const ProtocolIE_ID id_UserLocationInformation = 189;

const ProtocolIE_ID id_EmergencyAreaIDListForRestart = 190;

const ProtocolIE_ID id_KillAllWarningMessages = 191;

const ProtocolIE_ID id_Masked_IMEISV = 192;

const ProtocolIE_ID id_eNBIndirectX2TransportLayerAddresses = 193;

const ProtocolIE_ID id_uE_HistoryInformationFromTheUE = 194;

const ProtocolIE_ID id_ProSeAuthorized = 195;

const ProtocolIE_ID id_ExpectedUEBehaviour = 196;

const ProtocolIE_ID id_LoggedMBSFNMDT = 197;

const ProtocolIE_ID id_UERadioCapabilityForPaging = 198;

const ProtocolIE_ID id_E_RABToBeModifiedListBearerModInd = 199;

const ProtocolIE_ID id_E_RABToBeModifiedItemBearerModInd = 200;

const ProtocolIE_ID id_E_RABNotToBeModifiedListBearerModInd = 201;

const ProtocolIE_ID id_E_RABNotToBeModifiedItemBearerModInd = 202;

const ProtocolIE_ID id_E_RABModifyListBearerModConf = 203;

const ProtocolIE_ID id_E_RABModifyItemBearerModConf = 204;

const ProtocolIE_ID id_E_RABFailedToModifyListBearerModConf = 205;

const ProtocolIE_ID id_SON_Information_Report = 206;

const ProtocolIE_ID id_Muting_Availability_Indication = 207;

const ProtocolIE_ID id_Muting_Pattern_Information = 208;

const ProtocolIE_ID id_Synchronisation_Information = 209;

const ProtocolIE_ID id_E_RABToBeReleasedListBearerModConf = 210;

static Criticality _v0 = ignore;

static S1AP_ELEMENTARY_PROCEDURE _v1 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    2,
    3,
    6,
    0,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v2 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    7,
    10,
    15,
    1,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v3 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    17,
    20,
    23,
    3,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v4 = {
    SuccessfulOutcome_present | criticality_present,
    26,
    29,
    0,
    5,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v5 = {
    SuccessfulOutcome_present | criticality_present,
    32,
    35,
    0,
    6,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v6 = {
    SuccessfulOutcome_present | criticality_present,
    38,
    39,
    0,
    7,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v7 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    43,
    46,
    49,
    9,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v8 = {
    SuccessfulOutcome_present | criticality_present,
    24,
    25,
    0,
    4,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v9 = {
    SuccessfulOutcome_present | criticality_present,
    101,
    102,
    0,
    43,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v10 = {
    SuccessfulOutcome_present | criticality_present,
    65,
    67,
    0,
    14,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v11 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    70,
    71,
    72,
    17,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v12 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    56,
    57,
    58,
    21,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v13 = {
    SuccessfulOutcome_present | criticality_present,
    54,
    55,
    0,
    23,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v14 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    73,
    74,
    75,
    29,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v15 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    76,
    77,
    78,
    30,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v16 = {
    SuccessfulOutcome_present | criticality_present,
    93,
    94,
    0,
    36,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v17 = {
    SuccessfulOutcome_present | criticality_present,
    59,
    60,
    0,
    48,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v18 = {
    SuccessfulOutcome_present | criticality_present,
    108,
    113,
    0,
    50,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v19 = {
    criticality_present,
    16,
    0,
    0,
    2,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v20 = {
    criticality_present,
    42,
    0,
    0,
    8,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v21 = {
    criticality_present,
    50,
    0,
    0,
    10,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v22 = {
    criticality_present,
    61,
    0,
    0,
    11,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v23 = {
    criticality_present,
    62,
    0,
    0,
    12,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v24 = {
    criticality_present,
    63,
    0,
    0,
    13,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v25 = {
    criticality_present,
    69,
    0,
    0,
    15,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v26 = {
    criticality_present,
    64,
    0,
    0,
    16,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v27 = {
    criticality_present,
    53,
    0,
    0,
    18,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v28 = {
    criticality_present,
    79,
    0,
    0,
    19,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v29 = {
    criticality_present,
    80,
    0,
    0,
    20,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v30 = {
    criticality_present,
    81,
    0,
    0,
    22,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v31 = {
    criticality_present,
    82,
    0,
    0,
    24,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v32 = {
    criticality_present,
    83,
    0,
    0,
    25,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v33 = {
    criticality_present,
    86,
    0,
    0,
    26,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v34 = {
    criticality_present,
    84,
    0,
    0,
    27,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v35 = {
    criticality_present,
    85,
    0,
    0,
    28,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v36 = {
    criticality_present,
    87,
    0,
    0,
    42,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v37 = {
    criticality_present,
    88,
    0,
    0,
    31,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v38 = {
    criticality_present,
    89,
    0,
    0,
    32,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v39 = {
    criticality_present,
    90,
    0,
    0,
    33,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v40 = {
    criticality_present,
    91,
    0,
    0,
    34,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v41 = {
    criticality_present,
    92,
    0,
    0,
    35,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v42 = {
    criticality_present,
    95,
    0,
    0,
    37,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v43 = {
    criticality_present,
    97,
    0,
    0,
    38,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v44 = {
    criticality_present,
    98,
    0,
    0,
    40,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v45 = {
    criticality_present,
    99,
    0,
    0,
    41,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v46 = {
    criticality_present,
    100,
    0,
    0,
    39,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v47 = {
    criticality_present,
    104,
    0,
    0,
    44,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v48 = {
    criticality_present,
    105,
    0,
    0,
    45,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v49 = {
    criticality_present,
    106,
    0,
    0,
    46,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v50 = {
    criticality_present,
    107,
    0,
    0,
    47,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v51 = {
    criticality_present,
    103,
    0,
    0,
    49,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v52 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    2,
    3,
    6,
    0,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v53 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    7,
    10,
    15,
    1,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v54 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    17,
    20,
    23,
    3,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v55 = {
    SuccessfulOutcome_present | criticality_present,
    26,
    29,
    0,
    5,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v56 = {
    SuccessfulOutcome_present | criticality_present,
    32,
    35,
    0,
    6,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v57 = {
    SuccessfulOutcome_present | criticality_present,
    38,
    39,
    0,
    7,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v58 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    43,
    46,
    49,
    9,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v59 = {
    SuccessfulOutcome_present | criticality_present,
    24,
    25,
    0,
    4,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v60 = {
    SuccessfulOutcome_present | criticality_present,
    101,
    102,
    0,
    43,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v61 = {
    SuccessfulOutcome_present | criticality_present,
    65,
    67,
    0,
    14,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v62 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    70,
    71,
    72,
    17,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v63 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    56,
    57,
    58,
    21,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v64 = {
    SuccessfulOutcome_present | criticality_present,
    54,
    55,
    0,
    23,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v65 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    73,
    74,
    75,
    29,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v66 = {
    SuccessfulOutcome_present | UnsuccessfulOutcome_present | criticality_present,
    76,
    77,
    78,
    30,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v67 = {
    SuccessfulOutcome_present | criticality_present,
    93,
    94,
    0,
    36,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v68 = {
    SuccessfulOutcome_present | criticality_present,
    59,
    60,
    0,
    48,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v69 = {
    SuccessfulOutcome_present | criticality_present,
    108,
    113,
    0,
    50,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v70 = {
    criticality_present,
    16,
    0,
    0,
    2,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v71 = {
    criticality_present,
    42,
    0,
    0,
    8,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v72 = {
    criticality_present,
    50,
    0,
    0,
    10,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v73 = {
    criticality_present,
    61,
    0,
    0,
    11,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v74 = {
    criticality_present,
    62,
    0,
    0,
    12,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v75 = {
    criticality_present,
    63,
    0,
    0,
    13,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v76 = {
    criticality_present,
    69,
    0,
    0,
    15,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v77 = {
    criticality_present,
    64,
    0,
    0,
    16,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v78 = {
    criticality_present,
    53,
    0,
    0,
    18,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v79 = {
    criticality_present,
    79,
    0,
    0,
    19,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v80 = {
    criticality_present,
    80,
    0,
    0,
    20,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v81 = {
    criticality_present,
    81,
    0,
    0,
    22,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v82 = {
    criticality_present,
    82,
    0,
    0,
    24,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v83 = {
    criticality_present,
    83,
    0,
    0,
    25,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v84 = {
    criticality_present,
    86,
    0,
    0,
    26,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v85 = {
    criticality_present,
    84,
    0,
    0,
    27,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v86 = {
    criticality_present,
    85,
    0,
    0,
    28,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v87 = {
    criticality_present,
    87,
    0,
    0,
    42,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v88 = {
    criticality_present,
    88,
    0,
    0,
    31,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v89 = {
    criticality_present,
    89,
    0,
    0,
    32,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v90 = {
    criticality_present,
    90,
    0,
    0,
    33,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v91 = {
    criticality_present,
    91,
    0,
    0,
    34,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v92 = {
    criticality_present,
    92,
    0,
    0,
    35,
    reject
};

static S1AP_ELEMENTARY_PROCEDURE _v93 = {
    criticality_present,
    95,
    0,
    0,
    37,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v94 = {
    criticality_present,
    97,
    0,
    0,
    38,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v95 = {
    criticality_present,
    98,
    0,
    0,
    40,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v96 = {
    criticality_present,
    99,
    0,
    0,
    41,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v97 = {
    criticality_present,
    100,
    0,
    0,
    39,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v98 = {
    criticality_present,
    104,
    0,
    0,
    44,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v99 = {
    criticality_present,
    105,
    0,
    0,
    45,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v100 = {
    criticality_present,
    106,
    0,
    0,
    46,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v101 = {
    criticality_present,
    107,
    0,
    0,
    47,
    ignore
};

static S1AP_ELEMENTARY_PROCEDURE _v102 = {
    criticality_present,
    103,
    0,
    0,
    49,
    ignore
};

static S1AP_PROTOCOL_IES _v103 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v104 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v105 = {
    1,
    reject,
    158,
    mandatory
};

static S1AP_PROTOCOL_IES _v106 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v107 = {
    4,
    reject,
    219,
    mandatory
};

static S1AP_PROTOCOL_IES _v108 = {
    79,
    ignore,
    139,
    optional
};

static S1AP_PROTOCOL_IES _v109 = {
    125,
    reject,
    210,
    optional
};

static S1AP_PROTOCOL_IES _v110 = {
    104,
    reject,
    207,
    mandatory
};

static S1AP_PROTOCOL_IES _v111 = {
    138,
    reject,
    207,
    optional
};

static S1AP_PROTOCOL_IES _v112 = {
    132,
    reject,
    179,
    conditional
};

static S1AP_PROTOCOL_IES _v113 = {
    133,
    ignore,
    180,
    conditional
};

static S1AP_PROTOCOL_IES _v114 = {
    127,
    reject,
    133,
    optional
};

static S1AP_PROTOCOL_IES _v115 = {
    145,
    reject,
    120,
    optional
};

static S1AP_PROTOCOL_IES _v116 = {
    150,
    ignore,
    192,
    optional
};

static S1AP_PROTOCOL_IES _v117 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v118 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v119 = {
    1,
    reject,
    158,
    mandatory
};

static S1AP_PROTOCOL_IES _v120 = {
    135,
    reject,
    184,
    conditional
};

static S1AP_PROTOCOL_IES _v121 = {
    12,
    ignore,
    4,
    optional
};

static S1AP_PROTOCOL_IES _v122 = {
    13,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v123 = {
    123,
    reject,
    221,
    mandatory
};

static S1AP_PROTOCOL_IES _v124 = {
    139,
    reject,
    221,
    optional
};

static S1AP_PROTOCOL_IES _v125 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v126 = {
    14,
    ignore,
    5,
    mandatory
};

static S1AP_PROTOCOL_IES _v127 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v128 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v129 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v130 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v131 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v132 = {
    1,
    reject,
    158,
    mandatory
};

static S1AP_PROTOCOL_IES _v133 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v134 = {
    66,
    reject,
    233,
    mandatory
};

static S1AP_PROTOCOL_IES _v135 = {
    53,
    reject,
    8,
    mandatory
};

static S1AP_PROTOCOL_IES _v136 = {
    104,
    reject,
    207,
    mandatory
};

static S1AP_PROTOCOL_IES _v137 = {
    107,
    reject,
    241,
    mandatory
};

static S1AP_PROTOCOL_IES _v138 = {
    41,
    ignore,
    157,
    optional
};

static S1AP_PROTOCOL_IES _v139 = {
    25,
    ignore,
    228,
    optional
};

static S1AP_PROTOCOL_IES _v140 = {
    98,
    ignore,
    196,
    optional
};

static S1AP_PROTOCOL_IES _v141 = {
    124,
    ignore,
    209,
    optional
};

static S1AP_PROTOCOL_IES _v142 = {
    40,
    reject,
    202,
    mandatory
};

static S1AP_PROTOCOL_IES _v143 = {
    136,
    reject,
    185,
    conditional
};

static S1AP_PROTOCOL_IES _v144 = {
    127,
    reject,
    133,
    optional
};

static S1AP_PROTOCOL_IES _v145 = {
    146,
    ignore,
    135,
    optional
};

static S1AP_PROTOCOL_IES _v146 = {
    75,
    ignore,
    154,
    optional
};

static S1AP_PROTOCOL_IES _v147 = {
    158,
    ignore,
    178,
    optional
};

static S1AP_PROTOCOL_IES _v148 = {
    165,
    ignore,
    171,
    optional
};

static S1AP_PROTOCOL_IES _v149 = {
    177,
    ignore,
    172,
    optional
};

static S1AP_PROTOCOL_IES _v150 = {
    192,
    ignore,
    159,
    optional
};

static S1AP_PROTOCOL_IES _v151 = {
    196,
    ignore,
    152,
    optional
};

static S1AP_PROTOCOL_IES _v152 = {
    195,
    ignore,
    191,
    optional
};

static S1AP_PROTOCOL_IES _v153 = {
    27,
    reject,
    9,
    mandatory
};

static S1AP_PROTOCOL_EXTENSION _v154 = {
    143,
    ignore,
    140,
    optional
};

static S1AP_PROTOCOL_IES _v155 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v156 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v157 = {
    18,
    ignore,
    11,
    mandatory
};

static S1AP_PROTOCOL_IES _v158 = {
    19,
    ignore,
    13,
    optional
};

static S1AP_PROTOCOL_IES _v159 = {
    123,
    reject,
    221,
    mandatory
};

static S1AP_PROTOCOL_IES _v160 = {
    127,
    ignore,
    133,
    optional
};

static S1AP_PROTOCOL_IES _v161 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v162 = {
    145,
    ignore,
    120,
    optional
};

static S1AP_PROTOCOL_IES _v163 = {
    20,
    ignore,
    12,
    mandatory
};

static S1AP_PROTOCOL_IES _v164 = {
    21,
    ignore,
    14,
    mandatory
};

static S1AP_PROTOCOL_IES _v165 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v166 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v167 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v168 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v169 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v170 = {
    100,
    ignore,
    150,
    mandatory
};

static S1AP_PROTOCOL_IES _v171 = {
    67,
    ignore,
    218,
    mandatory
};

static S1AP_PROTOCOL_IES _v172 = {
    176,
    ignore,
    231,
    optional
};

static S1AP_PROTOCOL_IES _v173 = {
    186,
    ignore,
    164,
    optional
};

static S1AP_PROTOCOL_IES _v174 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v175 = {
    22,
    reject,
    18,
    mandatory
};

static S1AP_PROTOCOL_IES _v176 = {
    88,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v177 = {
    100,
    ignore,
    150,
    mandatory
};

static S1AP_PROTOCOL_IES _v178 = {
    67,
    ignore,
    218,
    mandatory
};

static S1AP_PROTOCOL_IES _v179 = {
    107,
    ignore,
    241,
    mandatory
};

static S1AP_PROTOCOL_IES _v180 = {
    127,
    ignore,
    133,
    optional
};

static S1AP_PROTOCOL_IES _v181 = {
    145,
    ignore,
    120,
    optional
};

static S1AP_PROTOCOL_IES _v182 = {
    157,
    ignore,
    154,
    optional
};

static S1AP_PROTOCOL_IES _v183 = {
    146,
    ignore,
    135,
    optional
};

static S1AP_PROTOCOL_IES _v184 = {
    176,
    ignore,
    231,
    optional
};

static S1AP_PROTOCOL_IES _v185 = {
    186,
    ignore,
    164,
    optional
};

static S1AP_PROTOCOL_IES _v186 = {
    23,
    reject,
    19,
    mandatory
};

static S1AP_PROTOCOL_IES _v187 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v188 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v189 = {
    66,
    ignore,
    233,
    optional
};

static S1AP_PROTOCOL_IES _v190 = {
    95,
    ignore,
    21,
    optional
};

static S1AP_PROTOCOL_IES _v191 = {
    33,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v192 = {
    40,
    reject,
    202,
    mandatory
};

static S1AP_PROTOCOL_IES _v193 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v194 = {
    158,
    ignore,
    178,
    optional
};

static S1AP_PROTOCOL_IES _v195 = {
    146,
    ignore,
    135,
    optional
};

static S1AP_PROTOCOL_IES _v196 = {
    195,
    ignore,
    191,
    optional
};

static S1AP_PROTOCOL_IES _v197 = {
    94,
    ignore,
    22,
    mandatory
};

static S1AP_PROTOCOL_IES _v198 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v199 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v200 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v201 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v202 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v203 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v204 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v205 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v206 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v207 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v208 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v209 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v210 = {
    66,
    reject,
    233,
    optional
};

static S1AP_PROTOCOL_IES _v211 = {
    16,
    reject,
    27,
    mandatory
};

static S1AP_PROTOCOL_IES _v212 = {
    17,
    reject,
    28,
    mandatory
};

static S1AP_PROTOCOL_EXTENSION _v213 = {
    156,
    ignore,
    130,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v214 = {
    183,
    ignore,
    130,
    optional
};

static S1AP_PROTOCOL_IES _v215 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v216 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v217 = {
    28,
    ignore,
    30,
    optional
};

static S1AP_PROTOCOL_IES _v218 = {
    29,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v219 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v220 = {
    39,
    ignore,
    31,
    mandatory
};

static S1AP_PROTOCOL_IES _v221 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v222 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v223 = {
    66,
    reject,
    233,
    optional
};

static S1AP_PROTOCOL_IES _v224 = {
    30,
    reject,
    33,
    mandatory
};

static S1AP_PROTOCOL_IES _v225 = {
    36,
    reject,
    34,
    mandatory
};

static S1AP_PROTOCOL_EXTENSION _v226 = {
    185,
    reject,
    226,
    optional
};

static S1AP_PROTOCOL_IES _v227 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v228 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v229 = {
    31,
    ignore,
    36,
    optional
};

static S1AP_PROTOCOL_IES _v230 = {
    32,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v231 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v232 = {
    37,
    ignore,
    37,
    mandatory
};

static S1AP_PROTOCOL_IES _v233 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v234 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v235 = {
    66,
    reject,
    233,
    optional
};

static S1AP_PROTOCOL_IES _v236 = {
    33,
    ignore,
    148,
    mandatory
};

static S1AP_PROTOCOL_IES _v237 = {
    26,
    ignore,
    183,
    optional
};

static S1AP_PROTOCOL_IES _v238 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v239 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v240 = {
    69,
    ignore,
    40,
    optional
};

static S1AP_PROTOCOL_IES _v241 = {
    34,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v242 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v243 = {
    189,
    ignore,
    242,
    optional
};

static S1AP_PROTOCOL_IES _v244 = {
    15,
    ignore,
    41,
    mandatory
};

static S1AP_PROTOCOL_IES _v245 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v246 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v247 = {
    110,
    ignore,
    148,
    mandatory
};

static S1AP_PROTOCOL_IES _v248 = {
    189,
    ignore,
    242,
    optional
};

static S1AP_PROTOCOL_IES _v249 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v250 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v251 = {
    66,
    reject,
    233,
    mandatory
};

static S1AP_PROTOCOL_IES _v252 = {
    24,
    reject,
    44,
    mandatory
};

static S1AP_PROTOCOL_IES _v253 = {
    107,
    reject,
    241,
    mandatory
};

static S1AP_PROTOCOL_IES _v254 = {
    73,
    reject,
    201,
    mandatory
};

static S1AP_PROTOCOL_IES _v255 = {
    25,
    ignore,
    228,
    optional
};

static S1AP_PROTOCOL_IES _v256 = {
    41,
    ignore,
    157,
    optional
};

static S1AP_PROTOCOL_IES _v257 = {
    74,
    ignore,
    239,
    optional
};

static S1AP_PROTOCOL_IES _v258 = {
    106,
    ignore,
    214,
    optional
};

static S1AP_PROTOCOL_IES _v259 = {
    108,
    reject,
    131,
    optional
};

static S1AP_PROTOCOL_IES _v260 = {
    124,
    ignore,
    209,
    optional
};

static S1AP_PROTOCOL_IES _v261 = {
    146,
    ignore,
    135,
    optional
};

static S1AP_PROTOCOL_IES _v262 = {
    159,
    ignore,
    161,
    optional
};

static S1AP_PROTOCOL_IES _v263 = {
    75,
    ignore,
    154,
    optional
};

static S1AP_PROTOCOL_IES _v264 = {
    158,
    ignore,
    178,
    optional
};

static S1AP_PROTOCOL_IES _v265 = {
    165,
    ignore,
    171,
    optional
};

static S1AP_PROTOCOL_IES _v266 = {
    177,
    ignore,
    172,
    optional
};

static S1AP_PROTOCOL_IES _v267 = {
    187,
    ignore,
    132,
    conditional
};

static S1AP_PROTOCOL_IES _v268 = {
    192,
    ignore,
    159,
    optional
};

static S1AP_PROTOCOL_IES _v269 = {
    196,
    ignore,
    152,
    optional
};

static S1AP_PROTOCOL_IES _v270 = {
    195,
    ignore,
    191,
    optional
};

static S1AP_PROTOCOL_IES _v271 = {
    52,
    reject,
    45,
    mandatory
};

static S1AP_PROTOCOL_EXTENSION _v272 = {
    156,
    ignore,
    130,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v273 = {
    183,
    ignore,
    130,
    optional
};

static S1AP_PROTOCOL_IES _v274 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v275 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v276 = {
    51,
    ignore,
    47,
    mandatory
};

static S1AP_PROTOCOL_IES _v277 = {
    48,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v278 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v279 = {
    50,
    ignore,
    48,
    mandatory
};

static S1AP_PROTOCOL_IES _v280 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v281 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v282 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v283 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v284 = {
    80,
    ignore,
    236,
    mandatory
};

static S1AP_PROTOCOL_IES _v285 = {
    43,
    ignore,
    238,
    mandatory
};

static S1AP_PROTOCOL_IES _v286 = {
    44,
    ignore,
    189,
    optional
};

static S1AP_PROTOCOL_IES _v287 = {
    109,
    ignore,
    128,
    mandatory
};

static S1AP_PROTOCOL_IES _v288 = {
    46,
    ignore,
    51,
    mandatory
};

static S1AP_PROTOCOL_IES _v289 = {
    128,
    ignore,
    134,
    optional
};

static S1AP_PROTOCOL_IES _v290 = {
    151,
    ignore,
    190,
    optional
};

static S1AP_PROTOCOL_IES _v291 = {
    198,
    ignore,
    240,
    optional
};

static S1AP_PROTOCOL_IES _v292 = {
    47,
    ignore,
    52,
    mandatory
};

static S1AP_PROTOCOL_IES _v293 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v294 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v295 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v296 = {
    164,
    reject,
    156,
    optional
};

static S1AP_PROTOCOL_IES _v297 = {
    99,
    reject,
    234,
    mandatory
};

static S1AP_PROTOCOL_IES _v298 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v299 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v300 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v301 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v302 = {
    189,
    ignore,
    242,
    optional
};

static S1AP_PROTOCOL_IES _v303 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v304 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v305 = {
    73,
    reject,
    201,
    optional
};

static S1AP_PROTOCOL_IES _v306 = {
    106,
    ignore,
    214,
    optional
};

static S1AP_PROTOCOL_IES _v307 = {
    66,
    ignore,
    233,
    optional
};

static S1AP_PROTOCOL_IES _v308 = {
    108,
    reject,
    131,
    optional
};

static S1AP_PROTOCOL_IES _v309 = {
    107,
    reject,
    241,
    optional
};

static S1AP_PROTOCOL_IES _v310 = {
    146,
    ignore,
    135,
    optional
};

static S1AP_PROTOCOL_IES _v311 = {
    159,
    ignore,
    161,
    optional
};

static S1AP_PROTOCOL_IES _v312 = {
    187,
    ignore,
    132,
    conditional
};

static S1AP_PROTOCOL_IES _v313 = {
    195,
    ignore,
    191,
    optional
};

static S1AP_PROTOCOL_IES _v314 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v315 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v316 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v317 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v318 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v319 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v320 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v321 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v322 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v323 = {
    74,
    ignore,
    239,
    optional
};

static S1AP_PROTOCOL_IES _v324 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v325 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v326 = {
    169,
    reject,
    243,
    mandatory
};

static S1AP_PROTOCOL_IES _v327 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v328 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v329 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v330 = {
    26,
    reject,
    183,
    mandatory
};

static S1AP_PROTOCOL_IES _v331 = {
    41,
    ignore,
    157,
    optional
};

static S1AP_PROTOCOL_IES _v332 = {
    106,
    ignore,
    214,
    optional
};

static S1AP_PROTOCOL_IES _v333 = {
    124,
    ignore,
    209,
    optional
};

static S1AP_PROTOCOL_IES _v334 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v335 = {
    26,
    reject,
    183,
    mandatory
};

static S1AP_PROTOCOL_IES _v336 = {
    67,
    reject,
    218,
    mandatory
};

static S1AP_PROTOCOL_IES _v337 = {
    100,
    ignore,
    150,
    mandatory
};

static S1AP_PROTOCOL_IES _v338 = {
    134,
    ignore,
    198,
    mandatory
};

static S1AP_PROTOCOL_IES _v339 = {
    96,
    reject,
    217,
    optional
};

static S1AP_PROTOCOL_IES _v340 = {
    127,
    reject,
    133,
    optional
};

static S1AP_PROTOCOL_IES _v341 = {
    75,
    reject,
    154,
    optional
};

static S1AP_PROTOCOL_IES _v342 = {
    145,
    reject,
    120,
    optional
};

static S1AP_PROTOCOL_IES _v343 = {
    155,
    ignore,
    227,
    optional
};

static S1AP_PROTOCOL_IES _v344 = {
    160,
    reject,
    195,
    optional
};

static S1AP_PROTOCOL_IES _v345 = {
    170,
    ignore,
    155,
    optional
};

static S1AP_PROTOCOL_IES _v346 = {
    176,
    ignore,
    231,
    optional
};

static S1AP_PROTOCOL_IES _v347 = {
    184,
    ignore,
    227,
    optional
};

static S1AP_PROTOCOL_IES _v348 = {
    186,
    ignore,
    164,
    optional
};

static S1AP_PROTOCOL_IES _v349 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v350 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v351 = {
    26,
    reject,
    183,
    mandatory
};

static S1AP_PROTOCOL_IES _v352 = {
    100,
    ignore,
    150,
    mandatory
};

static S1AP_PROTOCOL_IES _v353 = {
    67,
    ignore,
    218,
    mandatory
};

static S1AP_PROTOCOL_IES _v354 = {
    155,
    ignore,
    227,
    optional
};

static S1AP_PROTOCOL_IES _v355 = {
    184,
    ignore,
    227,
    optional
};

static S1AP_PROTOCOL_IES _v356 = {
    186,
    ignore,
    164,
    optional
};

static S1AP_PROTOCOL_IES _v357 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v358 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v359 = {
    26,
    ignore,
    183,
    mandatory
};

static S1AP_PROTOCOL_IES _v360 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v361 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v362 = {
    92,
    reject,
    66,
    mandatory
};

static S1AP_PROTOCOL_IES _v363 = {
    91,
    reject,
    235,
    mandatory
};

static S1AP_PROTOCOL_IES _v364 = {
    93,
    ignore,
    68,
    optional
};

static S1AP_PROTOCOL_IES _v365 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v366 = {
    91,
    ignore,
    235,
    mandatory
};

static S1AP_PROTOCOL_IES _v367 = {
    0,
    ignore,
    178,
    optional
};

static S1AP_PROTOCOL_IES _v368 = {
    8,
    ignore,
    145,
    optional
};

static S1AP_PROTOCOL_IES _v369 = {
    2,
    ignore,
    119,
    optional
};

static S1AP_PROTOCOL_IES _v370 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v371 = {
    59,
    reject,
    142,
    mandatory
};

static S1AP_PROTOCOL_IES _v372 = {
    60,
    ignore,
    146,
    optional
};

static S1AP_PROTOCOL_IES _v373 = {
    64,
    reject,
    215,
    mandatory
};

static S1AP_PROTOCOL_IES _v374 = {
    137,
    ignore,
    189,
    mandatory
};

static S1AP_PROTOCOL_IES _v375 = {
    128,
    reject,
    134,
    optional
};

static S1AP_PROTOCOL_IES _v376 = {
    61,
    ignore,
    176,
    optional
};

static S1AP_PROTOCOL_IES _v377 = {
    105,
    reject,
    213,
    mandatory
};

static S1AP_PROTOCOL_IES _v378 = {
    87,
    ignore,
    194,
    mandatory
};

static S1AP_PROTOCOL_IES _v379 = {
    163,
    ignore,
    177,
    optional
};

static S1AP_PROTOCOL_IES _v380 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v381 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v382 = {
    65,
    ignore,
    224,
    optional
};

static S1AP_PROTOCOL_IES _v383 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v384 = {
    60,
    ignore,
    146,
    optional
};

static S1AP_PROTOCOL_IES _v385 = {
    64,
    reject,
    215,
    optional
};

static S1AP_PROTOCOL_IES _v386 = {
    128,
    reject,
    134,
    optional
};

static S1AP_PROTOCOL_IES _v387 = {
    137,
    ignore,
    189,
    optional
};

static S1AP_PROTOCOL_IES _v388 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v389 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v390 = {
    65,
    ignore,
    224,
    optional
};

static S1AP_PROTOCOL_IES _v391 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v392 = {
    61,
    ignore,
    176,
    optional
};

static S1AP_PROTOCOL_IES _v393 = {
    105,
    reject,
    213,
    optional
};

static S1AP_PROTOCOL_IES _v394 = {
    87,
    reject,
    194,
    optional
};

static S1AP_PROTOCOL_IES _v395 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v396 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v397 = {
    65,
    ignore,
    224,
    optional
};

static S1AP_PROTOCOL_IES _v398 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v399 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v400 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v401 = {
    12,
    ignore,
    4,
    optional
};

static S1AP_PROTOCOL_IES _v402 = {
    83,
    ignore,
    124,
    optional
};

static S1AP_PROTOCOL_IES _v403 = {
    71,
    reject,
    122,
    mandatory
};

static S1AP_PROTOCOL_IES _v404 = {
    70,
    reject,
    121,
    mandatory
};

static S1AP_PROTOCOL_IES _v405 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v406 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v407 = {
    71,
    reject,
    122,
    mandatory
};

static S1AP_PROTOCOL_IES _v408 = {
    72,
    reject,
    123,
    mandatory
};

static S1AP_PROTOCOL_IES _v409 = {
    84,
    ignore,
    125,
    optional
};

static S1AP_PROTOCOL_IES _v410 = {
    102,
    reject,
    126,
    optional
};

static S1AP_PROTOCOL_IES _v411 = {
    97,
    reject,
    127,
    optional
};

static S1AP_PROTOCOL_IES _v412 = {
    70,
    reject,
    121,
    mandatory
};

static S1AP_PROTOCOL_IES _v413 = {
    140,
    ignore,
    151,
    optional
};

static S1AP_PROTOCOL_IES _v414 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v415 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v416 = {
    74,
    ignore,
    239,
    mandatory
};

static S1AP_PROTOCOL_IES _v417 = {
    198,
    ignore,
    240,
    optional
};

static S1AP_PROTOCOL_IES _v418 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v419 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v420 = {
    90,
    reject,
    144,
    mandatory
};

static S1AP_PROTOCOL_IES _v421 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v422 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v423 = {
    90,
    reject,
    144,
    mandatory
};

static S1AP_PROTOCOL_IES _v424 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v425 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v426 = {
    25,
    ignore,
    228,
    mandatory
};

static S1AP_PROTOCOL_IES _v427 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v428 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v429 = {
    86,
    ignore,
    229,
    mandatory
};

static S1AP_PROTOCOL_IES _v430 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v431 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v432 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v433 = {
    86,
    ignore,
    229,
    mandatory
};

static S1AP_PROTOCOL_IES _v434 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v435 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v436 = {
    86,
    ignore,
    229,
    mandatory
};

static S1AP_PROTOCOL_IES _v437 = {
    100,
    ignore,
    150,
    mandatory
};

static S1AP_PROTOCOL_IES _v438 = {
    131,
    ignore,
    227,
    mandatory
};

static S1AP_PROTOCOL_IES _v439 = {
    166,
    ignore,
    173,
    optional
};

static S1AP_PROTOCOL_IES _v440 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v441 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v442 = {
    98,
    ignore,
    196,
    mandatory
};

static S1AP_PROTOCOL_IES _v443 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v444 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v445 = {
    2,
    ignore,
    119,
    mandatory
};

static S1AP_PROTOCOL_IES _v446 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v447 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v448 = {
    100,
    ignore,
    150,
    mandatory
};

static S1AP_PROTOCOL_IES _v449 = {
    67,
    ignore,
    218,
    mandatory
};

static S1AP_PROTOCOL_IES _v450 = {
    98,
    ignore,
    196,
    mandatory
};

static S1AP_PROTOCOL_IES _v451 = {
    101,
    reject,
    188,
    mandatory
};

static S1AP_PROTOCOL_IES _v452 = {
    154,
    ignore,
    143,
    optional
};

static S1AP_PROTOCOL_IES _v453 = {
    161,
    ignore,
    230,
    optional
};

static S1AP_PROTOCOL_IES _v454 = {
    154,
    ignore,
    143,
    optional
};

static S1AP_PROTOCOL_IES _v455 = {
    111,
    reject,
    174,
    mandatory
};

static S1AP_PROTOCOL_IES _v456 = {
    112,
    reject,
    203,
    mandatory
};

static S1AP_PROTOCOL_IES _v457 = {
    113,
    ignore,
    244,
    optional
};

static S1AP_PROTOCOL_IES _v458 = {
    114,
    reject,
    197,
    mandatory
};

static S1AP_PROTOCOL_IES _v459 = {
    144,
    reject,
    153,
    optional
};

static S1AP_PROTOCOL_IES _v460 = {
    115,
    reject,
    186,
    mandatory
};

static S1AP_PROTOCOL_IES _v461 = {
    116,
    ignore,
    245,
    optional
};

static S1AP_PROTOCOL_IES _v462 = {
    117,
    ignore,
    246,
    optional
};

static S1AP_PROTOCOL_IES _v463 = {
    118,
    ignore,
    138,
    optional
};

static S1AP_PROTOCOL_IES _v464 = {
    119,
    ignore,
    247,
    optional
};

static S1AP_PROTOCOL_IES _v465 = {
    142,
    reject,
    129,
    optional
};

static S1AP_PROTOCOL_IES _v466 = {
    111,
    reject,
    174,
    mandatory
};

static S1AP_PROTOCOL_IES _v467 = {
    112,
    reject,
    203,
    mandatory
};

static S1AP_PROTOCOL_IES _v468 = {
    120,
    ignore,
    118,
    optional
};

static S1AP_PROTOCOL_IES _v469 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v470 = {
    121,
    reject,
    96,
    mandatory
};

static S1AP_PROTOCOL_IES _v471 = {
    122,
    reject,
    96,
    mandatory
};

static S1AP_PROTOCOL_IES _v472 = {
    129,
    ignore,
    205,
    optional
};

static S1AP_PROTOCOL_IES _v473 = {
    130,
    ignore,
    205,
    optional
};

static S1AP_PROTOCOL_IES _v474 = {
    111,
    reject,
    174,
    mandatory
};

static S1AP_PROTOCOL_IES _v475 = {
    112,
    reject,
    203,
    mandatory
};

static S1AP_PROTOCOL_IES _v476 = {
    113,
    ignore,
    244,
    optional
};

static S1AP_PROTOCOL_IES _v477 = {
    191,
    reject,
    160,
    optional
};

static S1AP_PROTOCOL_IES _v478 = {
    111,
    reject,
    174,
    mandatory
};

static S1AP_PROTOCOL_IES _v479 = {
    112,
    reject,
    203,
    mandatory
};

static S1AP_PROTOCOL_IES _v480 = {
    141,
    ignore,
    117,
    optional
};

static S1AP_PROTOCOL_IES _v481 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v482 = {
    182,
    reject,
    199,
    mandatory
};

static S1AP_PROTOCOL_IES _v483 = {
    59,
    reject,
    142,
    mandatory
};

static S1AP_PROTOCOL_IES _v484 = {
    188,
    reject,
    232,
    mandatory
};

static S1AP_PROTOCOL_IES _v485 = {
    190,
    reject,
    141,
    optional
};

static S1AP_PROTOCOL_IES _v486 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v487 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v488 = {
    148,
    reject,
    200,
    mandatory
};

static S1AP_PROTOCOL_IES _v489 = {
    147,
    reject,
    163,
    mandatory
};

static S1AP_PROTOCOL_IES _v490 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v491 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v492 = {
    148,
    reject,
    200,
    mandatory
};

static S1AP_PROTOCOL_IES _v493 = {
    147,
    reject,
    163,
    mandatory
};

static S1AP_PROTOCOL_IES _v494 = {
    148,
    reject,
    200,
    mandatory
};

static S1AP_PROTOCOL_IES _v495 = {
    147,
    reject,
    163,
    mandatory
};

static S1AP_PROTOCOL_IES _v496 = {
    148,
    reject,
    200,
    mandatory
};

static S1AP_PROTOCOL_IES _v497 = {
    147,
    reject,
    163,
    mandatory
};

static S1AP_PROTOCOL_IES _v498 = {
    0,
    reject,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v499 = {
    8,
    reject,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v500 = {
    199,
    reject,
    109,
    mandatory
};

static S1AP_PROTOCOL_IES _v501 = {
    201,
    reject,
    111,
    optional
};

static S1AP_PROTOCOL_IES _v502 = {
    200,
    reject,
    110,
    mandatory
};

static S1AP_PROTOCOL_IES _v503 = {
    202,
    reject,
    112,
    mandatory
};

static S1AP_PROTOCOL_IES _v504 = {
    0,
    ignore,
    178,
    mandatory
};

static S1AP_PROTOCOL_IES _v505 = {
    8,
    ignore,
    145,
    mandatory
};

static S1AP_PROTOCOL_IES _v506 = {
    203,
    ignore,
    114,
    optional
};

static S1AP_PROTOCOL_IES _v507 = {
    205,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v508 = {
    210,
    ignore,
    148,
    optional
};

static S1AP_PROTOCOL_IES _v509 = {
    58,
    ignore,
    137,
    optional
};

static S1AP_PROTOCOL_IES _v510 = {
    204,
    ignore,
    115,
    mandatory
};

static S1AP_PROTOCOL_IES _v511 = {
    89,
    ignore,
    116,
    mandatory
};

static S1AP_PROTOCOL_EXTENSION _v512 = {
    179,
    ignore,
    136,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v513 = {
    180,
    ignore,
    136,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v514 = {
    181,
    ignore,
    193,
    optional
};

static S1AP_PROTOCOL_IES _v515 = {
    78,
    ignore,
    147,
    mandatory
};

static S1AP_PROTOCOL_IES _v516 = {
    35,
    ignore,
    149,
    mandatory
};

static S1AP_PROTOCOL_EXTENSION _v517 = {
    171,
    ignore,
    166,
    conditional
};

static S1AP_PROTOCOL_EXTENSION _v518 = {
    172,
    ignore,
    167,
    conditional
};

static S1AP_PROTOCOL_EXTENSION _v519 = {
    173,
    ignore,
    168,
    conditional
};

static S1AP_PROTOCOL_EXTENSION _v520 = {
    174,
    ignore,
    169,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v521 = {
    167,
    ignore,
    225,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v522 = {
    168,
    ignore,
    119,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v523 = {
    178,
    ignore,
    172,
    optional
};

static S1AP_PROTOCOL_IES _v524 = {
    197,
    ignore,
    165,
    mandatory
};

static S1AP_PROTOCOL_IES _v525 = {
    206,
    ignore,
    204,
    mandatory
};

static S1AP_PROTOCOL_EXTENSION _v526 = {
    149,
    ignore,
    216,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v527 = {
    208,
    ignore,
    182,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v528 = {
    152,
    ignore,
    248,
    conditional
};

static S1AP_PROTOCOL_EXTENSION _v529 = {
    209,
    ignore,
    206,
    conditional
};

static S1AP_PROTOCOL_EXTENSION _v530 = {
    175,
    ignore,
    175,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v531 = {
    194,
    ignore,
    237,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v532 = {
    207,
    ignore,
    181,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v533 = {
    162,
    ignore,
    170,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v534 = {
    153,
    ignore,
    249,
    optional
};

static S1AP_PROTOCOL_EXTENSION _v535 = {
    193,
    ignore,
    250,
    optional
};

static int _v554 = 40;

static int _v555 = 50;

static int _v556 = 60;

static int _v557 = 80;

static int _v558 = 100;

static int _v559 = 120;

static int _v560 = 150;

static int _v561 = 180;

static int _v562 = 181;

static int _v563 = 40;

static int _v564 = 50;

static int _v565 = 60;

static int _v566 = 80;

static int _v567 = 100;

static int _v568 = 120;

static int _v569 = 150;

static int _v570 = 180;

static int _v571 = 181;


#if !defined(OSS_SPARTAN_AWARE) || ((OSS_SPARTAN_AWARE + 0) < 2)
static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES[] = {
    {&S1AP_ELEMENTARY_PROCEDURES[1], &_v1},
    {&S1AP_ELEMENTARY_PROCEDURES[2], &_v2},
    {&S1AP_ELEMENTARY_PROCEDURES[3], &_v3},
    {&S1AP_ELEMENTARY_PROCEDURES[4], &_v4},
    {&S1AP_ELEMENTARY_PROCEDURES[5], &_v5},
    {&S1AP_ELEMENTARY_PROCEDURES[6], &_v6},
    {&S1AP_ELEMENTARY_PROCEDURES[7], &_v7},
    {&S1AP_ELEMENTARY_PROCEDURES[8], &_v8},
    {&S1AP_ELEMENTARY_PROCEDURES[9], &_v9},
    {&S1AP_ELEMENTARY_PROCEDURES[10], &_v10},
    {&S1AP_ELEMENTARY_PROCEDURES[11], &_v11},
    {&S1AP_ELEMENTARY_PROCEDURES[12], &_v12},
    {&S1AP_ELEMENTARY_PROCEDURES[13], &_v13},
    {&S1AP_ELEMENTARY_PROCEDURES[14], &_v14},
    {&S1AP_ELEMENTARY_PROCEDURES[15], &_v15},
    {&S1AP_ELEMENTARY_PROCEDURES[16], &_v16},
    {&S1AP_ELEMENTARY_PROCEDURES[17], &_v17},
    {&S1AP_ELEMENTARY_PROCEDURES[18], &_v18},
    {&S1AP_ELEMENTARY_PROCEDURES[19], &_v19},
    {&S1AP_ELEMENTARY_PROCEDURES[20], &_v20},
    {&S1AP_ELEMENTARY_PROCEDURES[21], &_v21},
    {&S1AP_ELEMENTARY_PROCEDURES[22], &_v22},
    {&S1AP_ELEMENTARY_PROCEDURES[23], &_v23},
    {&S1AP_ELEMENTARY_PROCEDURES[24], &_v24},
    {&S1AP_ELEMENTARY_PROCEDURES[25], &_v25},
    {&S1AP_ELEMENTARY_PROCEDURES[26], &_v26},
    {&S1AP_ELEMENTARY_PROCEDURES[27], &_v27},
    {&S1AP_ELEMENTARY_PROCEDURES[28], &_v28},
    {&S1AP_ELEMENTARY_PROCEDURES[29], &_v29},
    {&S1AP_ELEMENTARY_PROCEDURES[30], &_v30},
    {&S1AP_ELEMENTARY_PROCEDURES[31], &_v31},
    {&S1AP_ELEMENTARY_PROCEDURES[32], &_v32},
    {&S1AP_ELEMENTARY_PROCEDURES[33], &_v33},
    {&S1AP_ELEMENTARY_PROCEDURES[34], &_v34},
    {&S1AP_ELEMENTARY_PROCEDURES[35], &_v35},
    {&S1AP_ELEMENTARY_PROCEDURES[36], &_v36},
    {&S1AP_ELEMENTARY_PROCEDURES[37], &_v37},
    {&S1AP_ELEMENTARY_PROCEDURES[38], &_v38},
    {&S1AP_ELEMENTARY_PROCEDURES[39], &_v39},
    {&S1AP_ELEMENTARY_PROCEDURES[40], &_v40},
    {&S1AP_ELEMENTARY_PROCEDURES[41], &_v41},
    {&S1AP_ELEMENTARY_PROCEDURES[42], &_v42},
    {&S1AP_ELEMENTARY_PROCEDURES[43], &_v43},
    {&S1AP_ELEMENTARY_PROCEDURES[44], &_v44},
    {&S1AP_ELEMENTARY_PROCEDURES[45], &_v45},
    {&S1AP_ELEMENTARY_PROCEDURES[46], &_v46},
    {&S1AP_ELEMENTARY_PROCEDURES[47], &_v47},
    {&S1AP_ELEMENTARY_PROCEDURES[48], &_v48},
    {&S1AP_ELEMENTARY_PROCEDURES[49], &_v49},
    {&S1AP_ELEMENTARY_PROCEDURES[50], &_v50},
    {NULL, &_v51}
};

static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES_CLASS_1[] = {
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[1], &_v52},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[2], &_v53},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[3], &_v54},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[4], &_v55},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[5], &_v56},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[6], &_v57},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[7], &_v58},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[8], &_v59},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[9], &_v60},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[10], &_v61},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[11], &_v62},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[12], &_v63},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[13], &_v64},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[14], &_v65},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[15], &_v66},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[16], &_v67},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[17], &_v68},
    {NULL, &_v69}
};

static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES_CLASS_2[] = {
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[1], &_v70},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[2], &_v71},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[3], &_v72},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[4], &_v73},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[5], &_v74},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[6], &_v75},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[7], &_v76},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[8], &_v77},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[9], &_v78},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[10], &_v79},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[11], &_v80},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[12], &_v81},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[13], &_v82},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[14], &_v83},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[15], &_v84},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[16], &_v85},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[17], &_v86},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[18], &_v87},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[19], &_v88},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[20], &_v89},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[21], &_v90},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[22], &_v91},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[23], &_v92},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[24], &_v93},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[25], &_v94},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[26], &_v95},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[27], &_v96},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[28], &_v97},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[29], &_v98},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[30], &_v99},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[31], &_v100},
    {&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[32], &_v101},
    {NULL, &_v102}
};

static ObjectSetEntry const HandoverRequiredIEs[] = {
    {&HandoverRequiredIEs[1], &_v103},
    {&HandoverRequiredIEs[2], &_v104},
    {&HandoverRequiredIEs[3], &_v105},
    {&HandoverRequiredIEs[4], &_v106},
    {&HandoverRequiredIEs[5], &_v107},
    {&HandoverRequiredIEs[6], &_v108},
    {&HandoverRequiredIEs[7], &_v109},
    {&HandoverRequiredIEs[8], &_v110},
    {&HandoverRequiredIEs[9], &_v111},
    {&HandoverRequiredIEs[10], &_v112},
    {&HandoverRequiredIEs[11], &_v113},
    {&HandoverRequiredIEs[12], &_v114},
    {&HandoverRequiredIEs[13], &_v115},
    {NULL, &_v116}
};

static ObjectSetEntry const HandoverCommandIEs[] = {
    {&HandoverCommandIEs[1], &_v117},
    {&HandoverCommandIEs[2], &_v118},
    {&HandoverCommandIEs[3], &_v119},
    {&HandoverCommandIEs[4], &_v120},
    {&HandoverCommandIEs[5], &_v121},
    {&HandoverCommandIEs[6], &_v122},
    {&HandoverCommandIEs[7], &_v123},
    {&HandoverCommandIEs[8], &_v124},
    {NULL, &_v125}
};

static ObjectSetEntry const E_RABDataForwardingItemIEs[] = {
    {NULL, &_v126}
};

static ObjectSetEntry const E_RABDataForwardingItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const HandoverPreparationFailureIEs[] = {
    {&HandoverPreparationFailureIEs[1], &_v127},
    {&HandoverPreparationFailureIEs[2], &_v128},
    {&HandoverPreparationFailureIEs[3], &_v129},
    {NULL, &_v130}
};

static ObjectSetEntry const HandoverRequestIEs[] = {
    {&HandoverRequestIEs[1], &_v131},
    {&HandoverRequestIEs[2], &_v132},
    {&HandoverRequestIEs[3], &_v133},
    {&HandoverRequestIEs[4], &_v134},
    {&HandoverRequestIEs[5], &_v135},
    {&HandoverRequestIEs[6], &_v136},
    {&HandoverRequestIEs[7], &_v137},
    {&HandoverRequestIEs[8], &_v138},
    {&HandoverRequestIEs[9], &_v139},
    {&HandoverRequestIEs[10], &_v140},
    {&HandoverRequestIEs[11], &_v141},
    {&HandoverRequestIEs[12], &_v142},
    {&HandoverRequestIEs[13], &_v143},
    {&HandoverRequestIEs[14], &_v144},
    {&HandoverRequestIEs[15], &_v145},
    {&HandoverRequestIEs[16], &_v146},
    {&HandoverRequestIEs[17], &_v147},
    {&HandoverRequestIEs[18], &_v148},
    {&HandoverRequestIEs[19], &_v149},
    {&HandoverRequestIEs[20], &_v150},
    {&HandoverRequestIEs[21], &_v151},
    {NULL, &_v152}
};

static ObjectSetEntry const E_RABToBeSetupItemHOReqIEs[] = {
    {NULL, &_v153}
};

static ObjectSetEntry const E_RABToBeSetupItemHOReq_ExtIEs[] = {
    {NULL, &_v154}
};

static ObjectSetEntry const HandoverRequestAcknowledgeIEs[] = {
    {&HandoverRequestAcknowledgeIEs[1], &_v155},
    {&HandoverRequestAcknowledgeIEs[2], &_v156},
    {&HandoverRequestAcknowledgeIEs[3], &_v157},
    {&HandoverRequestAcknowledgeIEs[4], &_v158},
    {&HandoverRequestAcknowledgeIEs[5], &_v159},
    {&HandoverRequestAcknowledgeIEs[6], &_v160},
    {&HandoverRequestAcknowledgeIEs[7], &_v161},
    {NULL, &_v162}
};

static ObjectSetEntry const E_RABAdmittedItemIEs[] = {
    {NULL, &_v163}
};

static ObjectSetEntry const E_RABAdmittedItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABFailedtoSetupItemHOReqAckIEs[] = {
    {NULL, &_v164}
};

static ObjectSetEntry const E_RABFailedToSetupItemHOReqAckExtIEs[] = {
{0}};

static ObjectSetEntry const HandoverFailureIEs[] = {
    {&HandoverFailureIEs[1], &_v165},
    {&HandoverFailureIEs[2], &_v166},
    {NULL, &_v167}
};

static ObjectSetEntry const HandoverNotifyIEs[] = {
    {&HandoverNotifyIEs[1], &_v168},
    {&HandoverNotifyIEs[2], &_v169},
    {&HandoverNotifyIEs[3], &_v170},
    {&HandoverNotifyIEs[4], &_v171},
    {&HandoverNotifyIEs[5], &_v172},
    {NULL, &_v173}
};

static ObjectSetEntry const PathSwitchRequestIEs[] = {
    {&PathSwitchRequestIEs[1], &_v174},
    {&PathSwitchRequestIEs[2], &_v175},
    {&PathSwitchRequestIEs[3], &_v176},
    {&PathSwitchRequestIEs[4], &_v177},
    {&PathSwitchRequestIEs[5], &_v178},
    {&PathSwitchRequestIEs[6], &_v179},
    {&PathSwitchRequestIEs[7], &_v180},
    {&PathSwitchRequestIEs[8], &_v181},
    {&PathSwitchRequestIEs[9], &_v182},
    {&PathSwitchRequestIEs[10], &_v183},
    {&PathSwitchRequestIEs[11], &_v184},
    {NULL, &_v185}
};

static ObjectSetEntry const E_RABToBeSwitchedDLItemIEs[] = {
    {NULL, &_v186}
};

static ObjectSetEntry const E_RABToBeSwitchedDLItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const PathSwitchRequestAcknowledgeIEs[] = {
    {&PathSwitchRequestAcknowledgeIEs[1], &_v187},
    {&PathSwitchRequestAcknowledgeIEs[2], &_v188},
    {&PathSwitchRequestAcknowledgeIEs[3], &_v189},
    {&PathSwitchRequestAcknowledgeIEs[4], &_v190},
    {&PathSwitchRequestAcknowledgeIEs[5], &_v191},
    {&PathSwitchRequestAcknowledgeIEs[6], &_v192},
    {&PathSwitchRequestAcknowledgeIEs[7], &_v193},
    {&PathSwitchRequestAcknowledgeIEs[8], &_v194},
    {&PathSwitchRequestAcknowledgeIEs[9], &_v195},
    {NULL, &_v196}
};

static ObjectSetEntry const E_RABToBeSwitchedULItemIEs[] = {
    {NULL, &_v197}
};

static ObjectSetEntry const E_RABToBeSwitchedULItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const PathSwitchRequestFailureIEs[] = {
    {&PathSwitchRequestFailureIEs[1], &_v198},
    {&PathSwitchRequestFailureIEs[2], &_v199},
    {&PathSwitchRequestFailureIEs[3], &_v200},
    {NULL, &_v201}
};

static ObjectSetEntry const HandoverCancelIEs[] = {
    {&HandoverCancelIEs[1], &_v202},
    {&HandoverCancelIEs[2], &_v203},
    {NULL, &_v204}
};

static ObjectSetEntry const HandoverCancelAcknowledgeIEs[] = {
    {&HandoverCancelAcknowledgeIEs[1], &_v205},
    {&HandoverCancelAcknowledgeIEs[2], &_v206},
    {NULL, &_v207}
};

static ObjectSetEntry const E_RABSetupRequestIEs[] = {
    {&E_RABSetupRequestIEs[1], &_v208},
    {&E_RABSetupRequestIEs[2], &_v209},
    {&E_RABSetupRequestIEs[3], &_v210},
    {NULL, &_v211}
};

static ObjectSetEntry const E_RABToBeSetupItemBearerSUReqIEs[] = {
    {NULL, &_v212}
};

static ObjectSetEntry const E_RABToBeSetupItemBearerSUReqExtIEs[] = {
    {&E_RABToBeSetupItemBearerSUReqExtIEs[1], &_v213},
    {NULL, &_v214}
};

static ObjectSetEntry const E_RABSetupResponseIEs[] = {
    {&E_RABSetupResponseIEs[1], &_v215},
    {&E_RABSetupResponseIEs[2], &_v216},
    {&E_RABSetupResponseIEs[3], &_v217},
    {&E_RABSetupResponseIEs[4], &_v218},
    {NULL, &_v219}
};

static ObjectSetEntry const E_RABSetupItemBearerSUResIEs[] = {
    {NULL, &_v220}
};

static ObjectSetEntry const E_RABSetupItemBearerSUResExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABModifyRequestIEs[] = {
    {&E_RABModifyRequestIEs[1], &_v221},
    {&E_RABModifyRequestIEs[2], &_v222},
    {&E_RABModifyRequestIEs[3], &_v223},
    {NULL, &_v224}
};

static ObjectSetEntry const E_RABToBeModifiedItemBearerModReqIEs[] = {
    {NULL, &_v225}
};

static ObjectSetEntry const E_RABToBeModifyItemBearerModReqExtIEs[] = {
    {NULL, &_v226}
};

static ObjectSetEntry const E_RABModifyResponseIEs[] = {
    {&E_RABModifyResponseIEs[1], &_v227},
    {&E_RABModifyResponseIEs[2], &_v228},
    {&E_RABModifyResponseIEs[3], &_v229},
    {&E_RABModifyResponseIEs[4], &_v230},
    {NULL, &_v231}
};

static ObjectSetEntry const E_RABModifyItemBearerModResIEs[] = {
    {NULL, &_v232}
};

static ObjectSetEntry const E_RABModifyItemBearerModResExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABReleaseCommandIEs[] = {
    {&E_RABReleaseCommandIEs[1], &_v233},
    {&E_RABReleaseCommandIEs[2], &_v234},
    {&E_RABReleaseCommandIEs[3], &_v235},
    {&E_RABReleaseCommandIEs[4], &_v236},
    {NULL, &_v237}
};

static ObjectSetEntry const E_RABReleaseResponseIEs[] = {
    {&E_RABReleaseResponseIEs[1], &_v238},
    {&E_RABReleaseResponseIEs[2], &_v239},
    {&E_RABReleaseResponseIEs[3], &_v240},
    {&E_RABReleaseResponseIEs[4], &_v241},
    {&E_RABReleaseResponseIEs[5], &_v242},
    {NULL, &_v243}
};

static ObjectSetEntry const E_RABReleaseItemBearerRelCompIEs[] = {
    {NULL, &_v244}
};

static ObjectSetEntry const E_RABReleaseItemBearerRelCompExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABReleaseIndicationIEs[] = {
    {&E_RABReleaseIndicationIEs[1], &_v245},
    {&E_RABReleaseIndicationIEs[2], &_v246},
    {&E_RABReleaseIndicationIEs[3], &_v247},
    {NULL, &_v248}
};

static ObjectSetEntry const InitialContextSetupRequestIEs[] = {
    {&InitialContextSetupRequestIEs[1], &_v249},
    {&InitialContextSetupRequestIEs[2], &_v250},
    {&InitialContextSetupRequestIEs[3], &_v251},
    {&InitialContextSetupRequestIEs[4], &_v252},
    {&InitialContextSetupRequestIEs[5], &_v253},
    {&InitialContextSetupRequestIEs[6], &_v254},
    {&InitialContextSetupRequestIEs[7], &_v255},
    {&InitialContextSetupRequestIEs[8], &_v256},
    {&InitialContextSetupRequestIEs[9], &_v257},
    {&InitialContextSetupRequestIEs[10], &_v258},
    {&InitialContextSetupRequestIEs[11], &_v259},
    {&InitialContextSetupRequestIEs[12], &_v260},
    {&InitialContextSetupRequestIEs[13], &_v261},
    {&InitialContextSetupRequestIEs[14], &_v262},
    {&InitialContextSetupRequestIEs[15], &_v263},
    {&InitialContextSetupRequestIEs[16], &_v264},
    {&InitialContextSetupRequestIEs[17], &_v265},
    {&InitialContextSetupRequestIEs[18], &_v266},
    {&InitialContextSetupRequestIEs[19], &_v267},
    {&InitialContextSetupRequestIEs[20], &_v268},
    {&InitialContextSetupRequestIEs[21], &_v269},
    {NULL, &_v270}
};

static ObjectSetEntry const E_RABToBeSetupItemCtxtSUReqIEs[] = {
    {NULL, &_v271}
};

static ObjectSetEntry const E_RABToBeSetupItemCtxtSUReqExtIEs[] = {
    {&E_RABToBeSetupItemCtxtSUReqExtIEs[1], &_v272},
    {NULL, &_v273}
};

static ObjectSetEntry const InitialContextSetupResponseIEs[] = {
    {&InitialContextSetupResponseIEs[1], &_v274},
    {&InitialContextSetupResponseIEs[2], &_v275},
    {&InitialContextSetupResponseIEs[3], &_v276},
    {&InitialContextSetupResponseIEs[4], &_v277},
    {NULL, &_v278}
};

static ObjectSetEntry const E_RABSetupItemCtxtSUResIEs[] = {
    {NULL, &_v279}
};

static ObjectSetEntry const E_RABSetupItemCtxtSUResExtIEs[] = {
{0}};

static ObjectSetEntry const InitialContextSetupFailureIEs[] = {
    {&InitialContextSetupFailureIEs[1], &_v280},
    {&InitialContextSetupFailureIEs[2], &_v281},
    {&InitialContextSetupFailureIEs[3], &_v282},
    {NULL, &_v283}
};

static ObjectSetEntry const PagingIEs[] = {
    {&PagingIEs[1], &_v284},
    {&PagingIEs[2], &_v285},
    {&PagingIEs[3], &_v286},
    {&PagingIEs[4], &_v287},
    {&PagingIEs[5], &_v288},
    {&PagingIEs[6], &_v289},
    {&PagingIEs[7], &_v290},
    {NULL, &_v291}
};

static ObjectSetEntry const TAIItemIEs[] = {
    {NULL, &_v292}
};

static ObjectSetEntry const TAIItemExtIEs[] = {
{0}};

static ObjectSetEntry const UEContextReleaseRequest_IEs[] = {
    {&UEContextReleaseRequest_IEs[1], &_v293},
    {&UEContextReleaseRequest_IEs[2], &_v294},
    {&UEContextReleaseRequest_IEs[3], &_v295},
    {NULL, &_v296}
};

static ObjectSetEntry const UEContextReleaseCommand_IEs[] = {
    {&UEContextReleaseCommand_IEs[1], &_v297},
    {NULL, &_v298}
};

static ObjectSetEntry const UEContextReleaseComplete_IEs[] = {
    {&UEContextReleaseComplete_IEs[1], &_v299},
    {&UEContextReleaseComplete_IEs[2], &_v300},
    {&UEContextReleaseComplete_IEs[3], &_v301},
    {NULL, &_v302}
};

static ObjectSetEntry const UEContextModificationRequestIEs[] = {
    {&UEContextModificationRequestIEs[1], &_v303},
    {&UEContextModificationRequestIEs[2], &_v304},
    {&UEContextModificationRequestIEs[3], &_v305},
    {&UEContextModificationRequestIEs[4], &_v306},
    {&UEContextModificationRequestIEs[5], &_v307},
    {&UEContextModificationRequestIEs[6], &_v308},
    {&UEContextModificationRequestIEs[7], &_v309},
    {&UEContextModificationRequestIEs[8], &_v310},
    {&UEContextModificationRequestIEs[9], &_v311},
    {&UEContextModificationRequestIEs[10], &_v312},
    {NULL, &_v313}
};

static ObjectSetEntry const UEContextModificationResponseIEs[] = {
    {&UEContextModificationResponseIEs[1], &_v314},
    {&UEContextModificationResponseIEs[2], &_v315},
    {NULL, &_v316}
};

static ObjectSetEntry const UEContextModificationFailureIEs[] = {
    {&UEContextModificationFailureIEs[1], &_v317},
    {&UEContextModificationFailureIEs[2], &_v318},
    {&UEContextModificationFailureIEs[3], &_v319},
    {NULL, &_v320}
};

static ObjectSetEntry const UERadioCapabilityMatchRequestIEs[] = {
    {&UERadioCapabilityMatchRequestIEs[1], &_v321},
    {&UERadioCapabilityMatchRequestIEs[2], &_v322},
    {NULL, &_v323}
};

static ObjectSetEntry const UERadioCapabilityMatchResponseIEs[] = {
    {&UERadioCapabilityMatchResponseIEs[1], &_v324},
    {&UERadioCapabilityMatchResponseIEs[2], &_v325},
    {&UERadioCapabilityMatchResponseIEs[3], &_v326},
    {NULL, &_v327}
};

static ObjectSetEntry const DownlinkNASTransport_IEs[] = {
    {&DownlinkNASTransport_IEs[1], &_v328},
    {&DownlinkNASTransport_IEs[2], &_v329},
    {&DownlinkNASTransport_IEs[3], &_v330},
    {&DownlinkNASTransport_IEs[4], &_v331},
    {&DownlinkNASTransport_IEs[5], &_v332},
    {NULL, &_v333}
};

static ObjectSetEntry const InitialUEMessage_IEs[] = {
    {&InitialUEMessage_IEs[1], &_v334},
    {&InitialUEMessage_IEs[2], &_v335},
    {&InitialUEMessage_IEs[3], &_v336},
    {&InitialUEMessage_IEs[4], &_v337},
    {&InitialUEMessage_IEs[5], &_v338},
    {&InitialUEMessage_IEs[6], &_v339},
    {&InitialUEMessage_IEs[7], &_v340},
    {&InitialUEMessage_IEs[8], &_v341},
    {&InitialUEMessage_IEs[9], &_v342},
    {&InitialUEMessage_IEs[10], &_v343},
    {&InitialUEMessage_IEs[11], &_v344},
    {&InitialUEMessage_IEs[12], &_v345},
    {&InitialUEMessage_IEs[13], &_v346},
    {&InitialUEMessage_IEs[14], &_v347},
    {NULL, &_v348}
};

static ObjectSetEntry const UplinkNASTransport_IEs[] = {
    {&UplinkNASTransport_IEs[1], &_v349},
    {&UplinkNASTransport_IEs[2], &_v350},
    {&UplinkNASTransport_IEs[3], &_v351},
    {&UplinkNASTransport_IEs[4], &_v352},
    {&UplinkNASTransport_IEs[5], &_v353},
    {&UplinkNASTransport_IEs[6], &_v354},
    {&UplinkNASTransport_IEs[7], &_v355},
    {NULL, &_v356}
};

static ObjectSetEntry const NASNonDeliveryIndication_IEs[] = {
    {&NASNonDeliveryIndication_IEs[1], &_v357},
    {&NASNonDeliveryIndication_IEs[2], &_v358},
    {&NASNonDeliveryIndication_IEs[3], &_v359},
    {NULL, &_v360}
};

static ObjectSetEntry const ResetIEs[] = {
    {&ResetIEs[1], &_v361},
    {NULL, &_v362}
};

static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemRes[] = {
    {NULL, &_v363}
};

static ObjectSetEntry const ResetAcknowledgeIEs[] = {
    {&ResetAcknowledgeIEs[1], &_v364},
    {NULL, &_v365}
};

static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemResAck[] = {
    {NULL, &_v366}
};

static ObjectSetEntry const ErrorIndicationIEs[] = {
    {&ErrorIndicationIEs[1], &_v367},
    {&ErrorIndicationIEs[2], &_v368},
    {&ErrorIndicationIEs[3], &_v369},
    {NULL, &_v370}
};

static ObjectSetEntry const S1SetupRequestIEs[] = {
    {&S1SetupRequestIEs[1], &_v371},
    {&S1SetupRequestIEs[2], &_v372},
    {&S1SetupRequestIEs[3], &_v373},
    {&S1SetupRequestIEs[4], &_v374},
    {NULL, &_v375}
};

static ObjectSetEntry const S1SetupResponseIEs[] = {
    {&S1SetupResponseIEs[1], &_v376},
    {&S1SetupResponseIEs[2], &_v377},
    {&S1SetupResponseIEs[3], &_v378},
    {&S1SetupResponseIEs[4], &_v379},
    {NULL, &_v380}
};

static ObjectSetEntry const S1SetupFailureIEs[] = {
    {&S1SetupFailureIEs[1], &_v381},
    {&S1SetupFailureIEs[2], &_v382},
    {NULL, &_v383}
};

static ObjectSetEntry const ENBConfigurationUpdateIEs[] = {
    {&ENBConfigurationUpdateIEs[1], &_v384},
    {&ENBConfigurationUpdateIEs[2], &_v385},
    {&ENBConfigurationUpdateIEs[3], &_v386},
    {NULL, &_v387}
};

static ObjectSetEntry const ENBConfigurationUpdateAcknowledgeIEs[] = {
    {NULL, &_v388}
};

static ObjectSetEntry const ENBConfigurationUpdateFailureIEs[] = {
    {&ENBConfigurationUpdateFailureIEs[1], &_v389},
    {&ENBConfigurationUpdateFailureIEs[2], &_v390},
    {NULL, &_v391}
};

static ObjectSetEntry const MMEConfigurationUpdateIEs[] = {
    {&MMEConfigurationUpdateIEs[1], &_v392},
    {&MMEConfigurationUpdateIEs[2], &_v393},
    {NULL, &_v394}
};

static ObjectSetEntry const MMEConfigurationUpdateAcknowledgeIEs[] = {
    {NULL, &_v395}
};

static ObjectSetEntry const MMEConfigurationUpdateFailureIEs[] = {
    {&MMEConfigurationUpdateFailureIEs[1], &_v396},
    {&MMEConfigurationUpdateFailureIEs[2], &_v397},
    {NULL, &_v398}
};

static ObjectSetEntry const DownlinkS1cdma2000tunnellingIEs[] = {
    {&DownlinkS1cdma2000tunnellingIEs[1], &_v399},
    {&DownlinkS1cdma2000tunnellingIEs[2], &_v400},
    {&DownlinkS1cdma2000tunnellingIEs[3], &_v401},
    {&DownlinkS1cdma2000tunnellingIEs[4], &_v402},
    {&DownlinkS1cdma2000tunnellingIEs[5], &_v403},
    {NULL, &_v404}
};

static ObjectSetEntry const UplinkS1cdma2000tunnellingIEs[] = {
    {&UplinkS1cdma2000tunnellingIEs[1], &_v405},
    {&UplinkS1cdma2000tunnellingIEs[2], &_v406},
    {&UplinkS1cdma2000tunnellingIEs[3], &_v407},
    {&UplinkS1cdma2000tunnellingIEs[4], &_v408},
    {&UplinkS1cdma2000tunnellingIEs[5], &_v409},
    {&UplinkS1cdma2000tunnellingIEs[6], &_v410},
    {&UplinkS1cdma2000tunnellingIEs[7], &_v411},
    {&UplinkS1cdma2000tunnellingIEs[8], &_v412},
    {NULL, &_v413}
};

static ObjectSetEntry const UECapabilityInfoIndicationIEs[] = {
    {&UECapabilityInfoIndicationIEs[1], &_v414},
    {&UECapabilityInfoIndicationIEs[2], &_v415},
    {&UECapabilityInfoIndicationIEs[3], &_v416},
    {NULL, &_v417}
};

static ObjectSetEntry const ENBStatusTransferIEs[] = {
    {&ENBStatusTransferIEs[1], &_v418},
    {&ENBStatusTransferIEs[2], &_v419},
    {NULL, &_v420}
};

static ObjectSetEntry const MMEStatusTransferIEs[] = {
    {&MMEStatusTransferIEs[1], &_v421},
    {&MMEStatusTransferIEs[2], &_v422},
    {NULL, &_v423}
};

static ObjectSetEntry const TraceStartIEs[] = {
    {&TraceStartIEs[1], &_v424},
    {&TraceStartIEs[2], &_v425},
    {NULL, &_v426}
};

static ObjectSetEntry const TraceFailureIndicationIEs[] = {
    {&TraceFailureIndicationIEs[1], &_v427},
    {&TraceFailureIndicationIEs[2], &_v428},
    {&TraceFailureIndicationIEs[3], &_v429},
    {NULL, &_v430}
};

static ObjectSetEntry const DeactivateTraceIEs[] = {
    {&DeactivateTraceIEs[1], &_v431},
    {&DeactivateTraceIEs[2], &_v432},
    {NULL, &_v433}
};

static ObjectSetEntry const CellTrafficTraceIEs[] = {
    {&CellTrafficTraceIEs[1], &_v434},
    {&CellTrafficTraceIEs[2], &_v435},
    {&CellTrafficTraceIEs[3], &_v436},
    {&CellTrafficTraceIEs[4], &_v437},
    {&CellTrafficTraceIEs[5], &_v438},
    {NULL, &_v439}
};

static ObjectSetEntry const LocationReportingControlIEs[] = {
    {&LocationReportingControlIEs[1], &_v440},
    {&LocationReportingControlIEs[2], &_v441},
    {NULL, &_v442}
};

static ObjectSetEntry const LocationReportingFailureIndicationIEs[] = {
    {&LocationReportingFailureIndicationIEs[1], &_v443},
    {&LocationReportingFailureIndicationIEs[2], &_v444},
    {NULL, &_v445}
};

static ObjectSetEntry const LocationReportIEs[] = {
    {&LocationReportIEs[1], &_v446},
    {&LocationReportIEs[2], &_v447},
    {&LocationReportIEs[3], &_v448},
    {&LocationReportIEs[4], &_v449},
    {NULL, &_v450}
};

static ObjectSetEntry const OverloadStartIEs[] = {
    {&OverloadStartIEs[1], &_v451},
    {&OverloadStartIEs[2], &_v452},
    {NULL, &_v453}
};

static ObjectSetEntry const OverloadStopIEs[] = {
    {NULL, &_v454}
};

static ObjectSetEntry const WriteReplaceWarningRequestIEs[] = {
    {&WriteReplaceWarningRequestIEs[1], &_v455},
    {&WriteReplaceWarningRequestIEs[2], &_v456},
    {&WriteReplaceWarningRequestIEs[3], &_v457},
    {&WriteReplaceWarningRequestIEs[4], &_v458},
    {&WriteReplaceWarningRequestIEs[5], &_v459},
    {&WriteReplaceWarningRequestIEs[6], &_v460},
    {&WriteReplaceWarningRequestIEs[7], &_v461},
    {&WriteReplaceWarningRequestIEs[8], &_v462},
    {&WriteReplaceWarningRequestIEs[9], &_v463},
    {&WriteReplaceWarningRequestIEs[10], &_v464},
    {NULL, &_v465}
};

static ObjectSetEntry const WriteReplaceWarningResponseIEs[] = {
    {&WriteReplaceWarningResponseIEs[1], &_v466},
    {&WriteReplaceWarningResponseIEs[2], &_v467},
    {&WriteReplaceWarningResponseIEs[3], &_v468},
    {NULL, &_v469}
};

static ObjectSetEntry const ENBDirectInformationTransferIEs[] = {
    {NULL, &_v470}
};

static ObjectSetEntry const MMEDirectInformationTransferIEs[] = {
    {NULL, &_v471}
};

static ObjectSetEntry const ENBConfigurationTransferIEs[] = {
    {NULL, &_v472}
};

static ObjectSetEntry const MMEConfigurationTransferIEs[] = {
    {NULL, &_v473}
};

static ObjectSetEntry const PrivateMessageIEs[] = {
{0}};

static ObjectSetEntry const KillRequestIEs[] = {
    {&KillRequestIEs[1], &_v474},
    {&KillRequestIEs[2], &_v475},
    {&KillRequestIEs[3], &_v476},
    {NULL, &_v477}
};

static ObjectSetEntry const KillResponseIEs[] = {
    {&KillResponseIEs[1], &_v478},
    {&KillResponseIEs[2], &_v479},
    {&KillResponseIEs[3], &_v480},
    {NULL, &_v481}
};

static ObjectSetEntry const PWSRestartIndicationIEs[] = {
    {&PWSRestartIndicationIEs[1], &_v482},
    {&PWSRestartIndicationIEs[2], &_v483},
    {&PWSRestartIndicationIEs[3], &_v484},
    {NULL, &_v485}
};

static ObjectSetEntry const DownlinkUEAssociatedLPPaTransport_IEs[] = {
    {&DownlinkUEAssociatedLPPaTransport_IEs[1], &_v486},
    {&DownlinkUEAssociatedLPPaTransport_IEs[2], &_v487},
    {&DownlinkUEAssociatedLPPaTransport_IEs[3], &_v488},
    {NULL, &_v489}
};

static ObjectSetEntry const UplinkUEAssociatedLPPaTransport_IEs[] = {
    {&UplinkUEAssociatedLPPaTransport_IEs[1], &_v490},
    {&UplinkUEAssociatedLPPaTransport_IEs[2], &_v491},
    {&UplinkUEAssociatedLPPaTransport_IEs[3], &_v492},
    {NULL, &_v493}
};

static ObjectSetEntry const DownlinkNonUEAssociatedLPPaTransport_IEs[] = {
    {&DownlinkNonUEAssociatedLPPaTransport_IEs[1], &_v494},
    {NULL, &_v495}
};

static ObjectSetEntry const UplinkNonUEAssociatedLPPaTransport_IEs[] = {
    {&UplinkNonUEAssociatedLPPaTransport_IEs[1], &_v496},
    {NULL, &_v497}
};

static ObjectSetEntry const E_RABModificationIndicationIEs[] = {
    {&E_RABModificationIndicationIEs[1], &_v498},
    {&E_RABModificationIndicationIEs[2], &_v499},
    {&E_RABModificationIndicationIEs[3], &_v500},
    {NULL, &_v501}
};

static ObjectSetEntry const E_RABToBeModifiedItemBearerModIndIEs[] = {
    {NULL, &_v502}
};

static ObjectSetEntry const E_RABToBeModifiedItemBearerModInd_ExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABNotToBeModifiedItemBearerModIndIEs[] = {
    {NULL, &_v503}
};

static ObjectSetEntry const E_RABNotToBeModifiedItemBearerModInd_ExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABModificationConfirmIEs[] = {
    {&E_RABModificationConfirmIEs[1], &_v504},
    {&E_RABModificationConfirmIEs[2], &_v505},
    {&E_RABModificationConfirmIEs[3], &_v506},
    {&E_RABModificationConfirmIEs[4], &_v507},
    {&E_RABModificationConfirmIEs[5], &_v508},
    {NULL, &_v509}
};

static ObjectSetEntry const E_RABModifyItemBearerModConfIEs[] = {
    {NULL, &_v510}
};

static ObjectSetEntry const E_RABModifyItemBearerModConfExtIEs[] = {
{0}};

static ObjectSetEntry const AllocationAndRetentionPriority_ExtIEs[] = {
{0}};

static ObjectSetEntry const Bearers_SubjectToStatusTransfer_ItemIEs[] = {
    {NULL, &_v511}
};

static ObjectSetEntry const Bearers_SubjectToStatusTransfer_ItemExtIEs[] = {
    {&Bearers_SubjectToStatusTransfer_ItemExtIEs[1], &_v512},
    {&Bearers_SubjectToStatusTransfer_ItemExtIEs[2], &_v513},
    {NULL, &_v514}
};

static ObjectSetEntry const CancelledCellinEAI_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const CancelledCellinTAI_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const CellID_Broadcast_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const CellID_Cancelled_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const CellBasedMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const Cdma2000OneXSRVCCInfo_ExtIEs[] = {
{0}};

static ObjectSetEntry const CellType_ExtIEs[] = {
{0}};

static ObjectSetEntry const CGI_ExtIEs[] = {
{0}};

static ObjectSetEntry const CSG_IdList_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const COUNTvalue_ExtIEs[] = {
{0}};

static ObjectSetEntry const COUNTValueExtended_ExtIEs[] = {
{0}};

static ObjectSetEntry const CriticalityDiagnostics_ExtIEs[] = {
{0}};

static ObjectSetEntry const CriticalityDiagnostics_IE_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const EmergencyAreaID_Broadcast_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const EmergencyAreaID_Cancelled_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const CompletedCellinEAI_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const GERAN_Cell_ID_ExtIEs[] = {
{0}};

static ObjectSetEntry const GlobalENB_ID_ExtIEs[] = {
{0}};

static ObjectSetEntry const ENB_StatusTransfer_TransparentContainer_ExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABInformationListIEs[] = {
    {NULL, &_v515}
};

static ObjectSetEntry const E_RABInformationListItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABItemIEs[] = {
    {NULL, &_v516}
};

static ObjectSetEntry const E_RABItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const E_RABQoSParameters_ExtIEs[] = {
{0}};

static ObjectSetEntry const EUTRAN_CGI_ExtIEs[] = {
{0}};

static ObjectSetEntry const ExpectedUEBehaviour_ExtIEs[] = {
{0}};

static ObjectSetEntry const ExpectedUEActivityBehaviour_ExtIEs[] = {
{0}};

static ObjectSetEntry const ForbiddenTAs_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const ForbiddenLAs_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const GBR_QosInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const GUMMEI_ExtIEs[] = {
{0}};

static ObjectSetEntry const HandoverRestrictionList_ExtIEs[] = {
{0}};

static ObjectSetEntry const ImmediateMDT_ExtIEs[] = {
    {&ImmediateMDT_ExtIEs[1], &_v517},
    {&ImmediateMDT_ExtIEs[2], &_v518},
    {&ImmediateMDT_ExtIEs[3], &_v519},
    {NULL, &_v520}
};

static ObjectSetEntry const LAI_ExtIEs[] = {
{0}};

static ObjectSetEntry const LastVisitedEUTRANCellInformation_ExtIEs[] = {
    {&LastVisitedEUTRANCellInformation_ExtIEs[1], &_v521},
    {NULL, &_v522}
};

static ObjectSetEntry const ListeningSubframePattern_ExtIEs[] = {
{0}};

static ObjectSetEntry const LoggedMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const LoggedMBSFNMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const M3Configuration_ExtIEs[] = {
{0}};

static ObjectSetEntry const M4Configuration_ExtIEs[] = {
{0}};

static ObjectSetEntry const M5Configuration_ExtIEs[] = {
{0}};

static ObjectSetEntry const MDT_Configuration_ExtIEs[] = {
    {NULL, &_v523}
};

static ObjectSetEntry const MBSFN_ResultToLogInfo_ExtIEs[] = {
{0}};

static ObjectSetEntry const MutingPatternInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const M1PeriodicReporting_ExtIEs[] = {
{0}};

static ObjectSetEntry const ProSeAuthorized_ExtIEs[] = {
{0}};

static ObjectSetEntry const RequestType_ExtIEs[] = {
{0}};

static ObjectSetEntry const RIMTransfer_ExtIEs[] = {
{0}};

static ObjectSetEntry const RLFReportInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const SecurityContext_ExtIEs[] = {
{0}};

static ObjectSetEntry const SONInformationReply_ExtIEs[] = {
    {&SONInformationReply_ExtIEs[1], &_v526},
    {NULL, &_v527}
};

static ObjectSetEntry const SONConfigurationTransfer_ExtIEs[] = {
    {&SONConfigurationTransfer_ExtIEs[1], &_v528},
    {NULL, &_v529}
};

static ObjectSetEntry const SynchronisationInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const SourceeNB_ID_ExtIEs[] = {
{0}};

static ObjectSetEntry const SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[] = {
    {&SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[1], &_v530},
    {NULL, &_v531}
};

static ObjectSetEntry const ServedGUMMEIsItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const SupportedTAs_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const TimeSynchronisationInfo_ExtIEs[] = {
    {NULL, &_v532}
};

static ObjectSetEntry const S_TMSI_ExtIEs[] = {
{0}};

static ObjectSetEntry const TAIBasedMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const TAI_ExtIEs[] = {
{0}};

static ObjectSetEntry const TAI_Broadcast_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const TAI_Cancelled_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const TABasedMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const CompletedCellinTAI_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const TargeteNB_ID_ExtIEs[] = {
{0}};

static ObjectSetEntry const TargetRNC_ID_ExtIEs[] = {
{0}};

static ObjectSetEntry const TargeteNB_ToSourceeNB_TransparentContainer_ExtIEs[] = {
{0}};

static ObjectSetEntry const M1ThresholdEventA2_ExtIEs[] = {
{0}};

static ObjectSetEntry const TraceActivation_ExtIEs[] = {
    {NULL, &_v533}
};

static ObjectSetEntry const Tunnel_Information_ExtIEs[] = {
{0}};

static ObjectSetEntry const UEAggregate_MaximumBitrates_ExtIEs[] = {
{0}};

static ObjectSetEntry const UE_S1AP_ID_pair_ExtIEs[] = {
{0}};

static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemExtIEs[] = {
{0}};

static ObjectSetEntry const UESecurityCapabilities_ExtIEs[] = {
{0}};

static ObjectSetEntry const UserLocationInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2TNLConfigurationInfo_ExtIEs[] = {
    {&X2TNLConfigurationInfo_ExtIEs[1], &_v534},
    {NULL, &_v535}
};

static ObjectSetEntry const ENBX2ExtTLA_ExtIEs[] = {
{0}};

static ObjectSetEntry const MDTMode_ExtensionIE[] = {
    {NULL, &_v524}
};

static ObjectSetEntry const SONInformation_ExtensionIE[] = {
    {NULL, &_v525}
};

#else /* OSS_SPARTAN_AWARE >= 2 */

#if ((OSS_SPARTAN_AWARE + 0) > 12)
static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES[] = {
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[1], &_v1, NULL, NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[2], &_v2, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[0], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[3], &_v3, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[1], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[4], &_v4, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[2], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[5], &_v5, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[3], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[6], &_v6, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[4], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[7], &_v7, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[5], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[8], &_v8, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[6], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[9], &_v9, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[7], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[10], &_v10, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[8], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[11], &_v11, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[9], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[12], &_v12, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[10], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[13], &_v13, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[11], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[14], &_v14, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[12], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[15], &_v15, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[13], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[16], &_v16, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[14], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[17], &_v17, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[15], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[18], &_v18, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[16], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[19], &_v19, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[17], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[20], &_v20, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[18], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[21], &_v21, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[19], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[22], &_v22, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[20], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[23], &_v23, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[21], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[24], &_v24, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[22], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[25], &_v25, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[23], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[26], &_v26, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[24], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[27], &_v27, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[25], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[28], &_v28, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[26], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[29], &_v29, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[27], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[30], &_v30, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[28], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[31], &_v31, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[29], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[32], &_v32, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[30], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[33], &_v33, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[31], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[34], &_v34, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[32], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[35], &_v35, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[33], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[36], &_v36, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[34], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[37], &_v37, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[35], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[38], &_v38, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[36], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[39], &_v39, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[37], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[40], &_v40, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[38], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[41], &_v41, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[39], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[42], &_v42, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[40], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[43], &_v43, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[41], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[44], &_v44, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[42], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[45], &_v45, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[43], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[46], &_v46, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[44], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[47], &_v47, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[45], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[48], &_v48, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[46], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[49], &_v49, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[47], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[50], &_v50, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[48], NULL},
    {NULL, &_v51, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[49], NULL}
};
static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES_CLASS_1[] = {
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[1], &_v52, NULL, NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[2], &_v53, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[0], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[3], &_v54, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[1], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[4], &_v55, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[2], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[5], &_v56, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[3], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[6], &_v57, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[4], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[7], &_v58, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[5], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[8], &_v59, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[6], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[9], &_v60, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[7], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[10], &_v61, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[8], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[11], &_v62, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[9], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[12], &_v63, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[10], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[13], &_v64, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[11], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[14], &_v65, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[12], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[15], &_v66, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[13], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[16], &_v67, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[14], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[17], &_v68, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[15], NULL},
    {NULL, &_v69, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[16], NULL}
};
static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES_CLASS_2[] = {
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[1], &_v70, NULL, NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[2], &_v71, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[0], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[3], &_v72, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[1], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[4], &_v73, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[2], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[5], &_v74, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[3], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[6], &_v75, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[4], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[7], &_v76, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[5], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[8], &_v77, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[6], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[9], &_v78, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[7], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[10], &_v79, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[8], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[11], &_v80, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[9], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[12], &_v81, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[10], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[13], &_v82, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[11], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[14], &_v83, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[12], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[15], &_v84, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[13], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[16], &_v85, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[14], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[17], &_v86, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[15], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[18], &_v87, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[16], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[19], &_v88, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[17], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[20], &_v89, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[18], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[21], &_v90, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[19], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[22], &_v91, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[20], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[23], &_v92, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[21], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[24], &_v93, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[22], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[25], &_v94, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[23], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[26], &_v95, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[24], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[27], &_v96, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[25], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[28], &_v97, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[26], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[29], &_v98, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[27], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[30], &_v99, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[28], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[31], &_v100, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[29], NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[32], &_v101, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[30], NULL},
    {NULL, &_v102, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[31], NULL}
};
static ObjectSetEntry const HandoverRequiredIEs[] = {
    {(ObjectSetEntry *)&HandoverRequiredIEs[1], &_v103, NULL, NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[2], &_v104, (ObjectSetEntry *)&HandoverRequiredIEs[0], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[3], &_v105, (ObjectSetEntry *)&HandoverRequiredIEs[1], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[4], &_v106, (ObjectSetEntry *)&HandoverRequiredIEs[2], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[5], &_v107, (ObjectSetEntry *)&HandoverRequiredIEs[3], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[6], &_v108, (ObjectSetEntry *)&HandoverRequiredIEs[4], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[7], &_v109, (ObjectSetEntry *)&HandoverRequiredIEs[5], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[8], &_v110, (ObjectSetEntry *)&HandoverRequiredIEs[6], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[9], &_v111, (ObjectSetEntry *)&HandoverRequiredIEs[7], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[10], &_v112, (ObjectSetEntry *)&HandoverRequiredIEs[8], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[11], &_v113, (ObjectSetEntry *)&HandoverRequiredIEs[9], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[12], &_v114, (ObjectSetEntry *)&HandoverRequiredIEs[10], NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[13], &_v115, (ObjectSetEntry *)&HandoverRequiredIEs[11], NULL},
    {NULL, &_v116, (ObjectSetEntry *)&HandoverRequiredIEs[12], NULL}
};
static ObjectSetEntry const HandoverCommandIEs[] = {
    {(ObjectSetEntry *)&HandoverCommandIEs[1], &_v117, NULL, NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[2], &_v118, (ObjectSetEntry *)&HandoverCommandIEs[0], NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[3], &_v119, (ObjectSetEntry *)&HandoverCommandIEs[1], NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[4], &_v120, (ObjectSetEntry *)&HandoverCommandIEs[2], NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[5], &_v121, (ObjectSetEntry *)&HandoverCommandIEs[3], NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[6], &_v122, (ObjectSetEntry *)&HandoverCommandIEs[4], NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[7], &_v123, (ObjectSetEntry *)&HandoverCommandIEs[5], NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[8], &_v124, (ObjectSetEntry *)&HandoverCommandIEs[6], NULL},
    {NULL, &_v125, (ObjectSetEntry *)&HandoverCommandIEs[7], NULL}
};
static ObjectSetEntry const E_RABDataForwardingItemIEs[] = {
    {NULL, &_v126, NULL, NULL}
};
static ObjectSetEntry const E_RABDataForwardingItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const HandoverPreparationFailureIEs[] = {
    {(ObjectSetEntry *)&HandoverPreparationFailureIEs[1], &_v127, NULL, NULL},
    {(ObjectSetEntry *)&HandoverPreparationFailureIEs[2], &_v128, (ObjectSetEntry *)&HandoverPreparationFailureIEs[0], NULL},
    {(ObjectSetEntry *)&HandoverPreparationFailureIEs[3], &_v129, (ObjectSetEntry *)&HandoverPreparationFailureIEs[1], NULL},
    {NULL, &_v130, (ObjectSetEntry *)&HandoverPreparationFailureIEs[2], NULL}
};
static ObjectSetEntry const HandoverRequestIEs[] = {
    {(ObjectSetEntry *)&HandoverRequestIEs[1], &_v131, NULL, NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[2], &_v132, (ObjectSetEntry *)&HandoverRequestIEs[0], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[3], &_v133, (ObjectSetEntry *)&HandoverRequestIEs[1], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[4], &_v134, (ObjectSetEntry *)&HandoverRequestIEs[2], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[5], &_v135, (ObjectSetEntry *)&HandoverRequestIEs[3], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[6], &_v136, (ObjectSetEntry *)&HandoverRequestIEs[4], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[7], &_v137, (ObjectSetEntry *)&HandoverRequestIEs[5], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[8], &_v138, (ObjectSetEntry *)&HandoverRequestIEs[6], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[9], &_v139, (ObjectSetEntry *)&HandoverRequestIEs[7], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[10], &_v140, (ObjectSetEntry *)&HandoverRequestIEs[8], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[11], &_v141, (ObjectSetEntry *)&HandoverRequestIEs[9], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[12], &_v142, (ObjectSetEntry *)&HandoverRequestIEs[10], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[13], &_v143, (ObjectSetEntry *)&HandoverRequestIEs[11], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[14], &_v144, (ObjectSetEntry *)&HandoverRequestIEs[12], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[15], &_v145, (ObjectSetEntry *)&HandoverRequestIEs[13], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[16], &_v146, (ObjectSetEntry *)&HandoverRequestIEs[14], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[17], &_v147, (ObjectSetEntry *)&HandoverRequestIEs[15], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[18], &_v148, (ObjectSetEntry *)&HandoverRequestIEs[16], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[19], &_v149, (ObjectSetEntry *)&HandoverRequestIEs[17], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[20], &_v150, (ObjectSetEntry *)&HandoverRequestIEs[18], NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[21], &_v151, (ObjectSetEntry *)&HandoverRequestIEs[19], NULL},
    {NULL, &_v152, (ObjectSetEntry *)&HandoverRequestIEs[20], NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemHOReqIEs[] = {
    {NULL, &_v153, NULL, NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemHOReq_ExtIEs[] = {
    {NULL, &_v154, NULL, NULL}
};
static ObjectSetEntry const HandoverRequestAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[1], &_v155, NULL, NULL},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[2], &_v156, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[0], NULL},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[3], &_v157, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[1], NULL},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[4], &_v158, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[2], NULL},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[5], &_v159, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[3], NULL},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[6], &_v160, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[4], NULL},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[7], &_v161, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[5], NULL},
    {NULL, &_v162, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[6], NULL}
};
static ObjectSetEntry const E_RABAdmittedItemIEs[] = {
    {NULL, &_v163, NULL, NULL}
};
static ObjectSetEntry const E_RABAdmittedItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABFailedtoSetupItemHOReqAckIEs[] = {
    {NULL, &_v164, NULL, NULL}
};
static ObjectSetEntry const E_RABFailedToSetupItemHOReqAckExtIEs[] = {
{0}};
static ObjectSetEntry const HandoverFailureIEs[] = {
    {(ObjectSetEntry *)&HandoverFailureIEs[1], &_v165, NULL, NULL},
    {(ObjectSetEntry *)&HandoverFailureIEs[2], &_v166, (ObjectSetEntry *)&HandoverFailureIEs[0], NULL},
    {NULL, &_v167, (ObjectSetEntry *)&HandoverFailureIEs[1], NULL}
};
static ObjectSetEntry const HandoverNotifyIEs[] = {
    {(ObjectSetEntry *)&HandoverNotifyIEs[1], &_v168, NULL, NULL},
    {(ObjectSetEntry *)&HandoverNotifyIEs[2], &_v169, (ObjectSetEntry *)&HandoverNotifyIEs[0], NULL},
    {(ObjectSetEntry *)&HandoverNotifyIEs[3], &_v170, (ObjectSetEntry *)&HandoverNotifyIEs[1], NULL},
    {(ObjectSetEntry *)&HandoverNotifyIEs[4], &_v171, (ObjectSetEntry *)&HandoverNotifyIEs[2], NULL},
    {(ObjectSetEntry *)&HandoverNotifyIEs[5], &_v172, (ObjectSetEntry *)&HandoverNotifyIEs[3], NULL},
    {NULL, &_v173, (ObjectSetEntry *)&HandoverNotifyIEs[4], NULL}
};
static ObjectSetEntry const PathSwitchRequestIEs[] = {
    {(ObjectSetEntry *)&PathSwitchRequestIEs[1], &_v174, NULL, NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[2], &_v175, (ObjectSetEntry *)&PathSwitchRequestIEs[0], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[3], &_v176, (ObjectSetEntry *)&PathSwitchRequestIEs[1], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[4], &_v177, (ObjectSetEntry *)&PathSwitchRequestIEs[2], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[5], &_v178, (ObjectSetEntry *)&PathSwitchRequestIEs[3], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[6], &_v179, (ObjectSetEntry *)&PathSwitchRequestIEs[4], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[7], &_v180, (ObjectSetEntry *)&PathSwitchRequestIEs[5], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[8], &_v181, (ObjectSetEntry *)&PathSwitchRequestIEs[6], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[9], &_v182, (ObjectSetEntry *)&PathSwitchRequestIEs[7], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[10], &_v183, (ObjectSetEntry *)&PathSwitchRequestIEs[8], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[11], &_v184, (ObjectSetEntry *)&PathSwitchRequestIEs[9], NULL},
    {NULL, &_v185, (ObjectSetEntry *)&PathSwitchRequestIEs[10], NULL}
};
static ObjectSetEntry const E_RABToBeSwitchedDLItemIEs[] = {
    {NULL, &_v186, NULL, NULL}
};
static ObjectSetEntry const E_RABToBeSwitchedDLItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const PathSwitchRequestAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[1], &_v187, NULL, NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[2], &_v188, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[0], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[3], &_v189, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[1], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[4], &_v190, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[2], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[5], &_v191, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[3], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[6], &_v192, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[4], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[7], &_v193, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[5], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[8], &_v194, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[6], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[9], &_v195, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[7], NULL},
    {NULL, &_v196, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[8], NULL}
};
static ObjectSetEntry const E_RABToBeSwitchedULItemIEs[] = {
    {NULL, &_v197, NULL, NULL}
};
static ObjectSetEntry const E_RABToBeSwitchedULItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const PathSwitchRequestFailureIEs[] = {
    {(ObjectSetEntry *)&PathSwitchRequestFailureIEs[1], &_v198, NULL, NULL},
    {(ObjectSetEntry *)&PathSwitchRequestFailureIEs[2], &_v199, (ObjectSetEntry *)&PathSwitchRequestFailureIEs[0], NULL},
    {(ObjectSetEntry *)&PathSwitchRequestFailureIEs[3], &_v200, (ObjectSetEntry *)&PathSwitchRequestFailureIEs[1], NULL},
    {NULL, &_v201, (ObjectSetEntry *)&PathSwitchRequestFailureIEs[2], NULL}
};
static ObjectSetEntry const HandoverCancelIEs[] = {
    {(ObjectSetEntry *)&HandoverCancelIEs[1], &_v202, NULL, NULL},
    {(ObjectSetEntry *)&HandoverCancelIEs[2], &_v203, (ObjectSetEntry *)&HandoverCancelIEs[0], NULL},
    {NULL, &_v204, (ObjectSetEntry *)&HandoverCancelIEs[1], NULL}
};
static ObjectSetEntry const HandoverCancelAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[1], &_v205, NULL, NULL},
    {(ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[2], &_v206, (ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[0], NULL},
    {NULL, &_v207, (ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[1], NULL}
};
static ObjectSetEntry const E_RABSetupRequestIEs[] = {
    {(ObjectSetEntry *)&E_RABSetupRequestIEs[1], &_v208, NULL, NULL},
    {(ObjectSetEntry *)&E_RABSetupRequestIEs[2], &_v209, (ObjectSetEntry *)&E_RABSetupRequestIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABSetupRequestIEs[3], &_v210, (ObjectSetEntry *)&E_RABSetupRequestIEs[1], NULL},
    {NULL, &_v211, (ObjectSetEntry *)&E_RABSetupRequestIEs[2], NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemBearerSUReqIEs[] = {
    {NULL, &_v212, NULL, NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemBearerSUReqExtIEs[] = {
    {(ObjectSetEntry *)&E_RABToBeSetupItemBearerSUReqExtIEs[1], &_v213, NULL, NULL},
    {NULL, &_v214, (ObjectSetEntry *)&E_RABToBeSetupItemBearerSUReqExtIEs[0], NULL}
};
static ObjectSetEntry const E_RABSetupResponseIEs[] = {
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[1], &_v215, NULL, NULL},
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[2], &_v216, (ObjectSetEntry *)&E_RABSetupResponseIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[3], &_v217, (ObjectSetEntry *)&E_RABSetupResponseIEs[1], NULL},
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[4], &_v218, (ObjectSetEntry *)&E_RABSetupResponseIEs[2], NULL},
    {NULL, &_v219, (ObjectSetEntry *)&E_RABSetupResponseIEs[3], NULL}
};
static ObjectSetEntry const E_RABSetupItemBearerSUResIEs[] = {
    {NULL, &_v220, NULL, NULL}
};
static ObjectSetEntry const E_RABSetupItemBearerSUResExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABModifyRequestIEs[] = {
    {(ObjectSetEntry *)&E_RABModifyRequestIEs[1], &_v221, NULL, NULL},
    {(ObjectSetEntry *)&E_RABModifyRequestIEs[2], &_v222, (ObjectSetEntry *)&E_RABModifyRequestIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABModifyRequestIEs[3], &_v223, (ObjectSetEntry *)&E_RABModifyRequestIEs[1], NULL},
    {NULL, &_v224, (ObjectSetEntry *)&E_RABModifyRequestIEs[2], NULL}
};
static ObjectSetEntry const E_RABToBeModifiedItemBearerModReqIEs[] = {
    {NULL, &_v225, NULL, NULL}
};
static ObjectSetEntry const E_RABToBeModifyItemBearerModReqExtIEs[] = {
    {NULL, &_v226, NULL, NULL}
};
static ObjectSetEntry const E_RABModifyResponseIEs[] = {
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[1], &_v227, NULL, NULL},
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[2], &_v228, (ObjectSetEntry *)&E_RABModifyResponseIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[3], &_v229, (ObjectSetEntry *)&E_RABModifyResponseIEs[1], NULL},
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[4], &_v230, (ObjectSetEntry *)&E_RABModifyResponseIEs[2], NULL},
    {NULL, &_v231, (ObjectSetEntry *)&E_RABModifyResponseIEs[3], NULL}
};
static ObjectSetEntry const E_RABModifyItemBearerModResIEs[] = {
    {NULL, &_v232, NULL, NULL}
};
static ObjectSetEntry const E_RABModifyItemBearerModResExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABReleaseCommandIEs[] = {
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[1], &_v233, NULL, NULL},
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[2], &_v234, (ObjectSetEntry *)&E_RABReleaseCommandIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[3], &_v235, (ObjectSetEntry *)&E_RABReleaseCommandIEs[1], NULL},
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[4], &_v236, (ObjectSetEntry *)&E_RABReleaseCommandIEs[2], NULL},
    {NULL, &_v237, (ObjectSetEntry *)&E_RABReleaseCommandIEs[3], NULL}
};
static ObjectSetEntry const E_RABReleaseResponseIEs[] = {
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[1], &_v238, NULL, NULL},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[2], &_v239, (ObjectSetEntry *)&E_RABReleaseResponseIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[3], &_v240, (ObjectSetEntry *)&E_RABReleaseResponseIEs[1], NULL},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[4], &_v241, (ObjectSetEntry *)&E_RABReleaseResponseIEs[2], NULL},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[5], &_v242, (ObjectSetEntry *)&E_RABReleaseResponseIEs[3], NULL},
    {NULL, &_v243, (ObjectSetEntry *)&E_RABReleaseResponseIEs[4], NULL}
};
static ObjectSetEntry const E_RABReleaseItemBearerRelCompIEs[] = {
    {NULL, &_v244, NULL, NULL}
};
static ObjectSetEntry const E_RABReleaseItemBearerRelCompExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABReleaseIndicationIEs[] = {
    {(ObjectSetEntry *)&E_RABReleaseIndicationIEs[1], &_v245, NULL, NULL},
    {(ObjectSetEntry *)&E_RABReleaseIndicationIEs[2], &_v246, (ObjectSetEntry *)&E_RABReleaseIndicationIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABReleaseIndicationIEs[3], &_v247, (ObjectSetEntry *)&E_RABReleaseIndicationIEs[1], NULL},
    {NULL, &_v248, (ObjectSetEntry *)&E_RABReleaseIndicationIEs[2], NULL}
};
static ObjectSetEntry const InitialContextSetupRequestIEs[] = {
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[1], &_v249, NULL, NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[2], &_v250, (ObjectSetEntry *)&InitialContextSetupRequestIEs[0], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[3], &_v251, (ObjectSetEntry *)&InitialContextSetupRequestIEs[1], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[4], &_v252, (ObjectSetEntry *)&InitialContextSetupRequestIEs[2], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[5], &_v253, (ObjectSetEntry *)&InitialContextSetupRequestIEs[3], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[6], &_v254, (ObjectSetEntry *)&InitialContextSetupRequestIEs[4], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[7], &_v255, (ObjectSetEntry *)&InitialContextSetupRequestIEs[5], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[8], &_v256, (ObjectSetEntry *)&InitialContextSetupRequestIEs[6], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[9], &_v257, (ObjectSetEntry *)&InitialContextSetupRequestIEs[7], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[10], &_v258, (ObjectSetEntry *)&InitialContextSetupRequestIEs[8], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[11], &_v259, (ObjectSetEntry *)&InitialContextSetupRequestIEs[9], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[12], &_v260, (ObjectSetEntry *)&InitialContextSetupRequestIEs[10], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[13], &_v261, (ObjectSetEntry *)&InitialContextSetupRequestIEs[11], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[14], &_v262, (ObjectSetEntry *)&InitialContextSetupRequestIEs[12], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[15], &_v263, (ObjectSetEntry *)&InitialContextSetupRequestIEs[13], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[16], &_v264, (ObjectSetEntry *)&InitialContextSetupRequestIEs[14], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[17], &_v265, (ObjectSetEntry *)&InitialContextSetupRequestIEs[15], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[18], &_v266, (ObjectSetEntry *)&InitialContextSetupRequestIEs[16], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[19], &_v267, (ObjectSetEntry *)&InitialContextSetupRequestIEs[17], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[20], &_v268, (ObjectSetEntry *)&InitialContextSetupRequestIEs[18], NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[21], &_v269, (ObjectSetEntry *)&InitialContextSetupRequestIEs[19], NULL},
    {NULL, &_v270, (ObjectSetEntry *)&InitialContextSetupRequestIEs[20], NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemCtxtSUReqIEs[] = {
    {NULL, &_v271, NULL, NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemCtxtSUReqExtIEs[] = {
    {(ObjectSetEntry *)&E_RABToBeSetupItemCtxtSUReqExtIEs[1], &_v272, NULL, NULL},
    {NULL, &_v273, (ObjectSetEntry *)&E_RABToBeSetupItemCtxtSUReqExtIEs[0], NULL}
};
static ObjectSetEntry const InitialContextSetupResponseIEs[] = {
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[1], &_v274, NULL, NULL},
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[2], &_v275, (ObjectSetEntry *)&InitialContextSetupResponseIEs[0], NULL},
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[3], &_v276, (ObjectSetEntry *)&InitialContextSetupResponseIEs[1], NULL},
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[4], &_v277, (ObjectSetEntry *)&InitialContextSetupResponseIEs[2], NULL},
    {NULL, &_v278, (ObjectSetEntry *)&InitialContextSetupResponseIEs[3], NULL}
};
static ObjectSetEntry const E_RABSetupItemCtxtSUResIEs[] = {
    {NULL, &_v279, NULL, NULL}
};
static ObjectSetEntry const E_RABSetupItemCtxtSUResExtIEs[] = {
{0}};
static ObjectSetEntry const InitialContextSetupFailureIEs[] = {
    {(ObjectSetEntry *)&InitialContextSetupFailureIEs[1], &_v280, NULL, NULL},
    {(ObjectSetEntry *)&InitialContextSetupFailureIEs[2], &_v281, (ObjectSetEntry *)&InitialContextSetupFailureIEs[0], NULL},
    {(ObjectSetEntry *)&InitialContextSetupFailureIEs[3], &_v282, (ObjectSetEntry *)&InitialContextSetupFailureIEs[1], NULL},
    {NULL, &_v283, (ObjectSetEntry *)&InitialContextSetupFailureIEs[2], NULL}
};
static ObjectSetEntry const PagingIEs[] = {
    {(ObjectSetEntry *)&PagingIEs[1], &_v284, NULL, NULL},
    {(ObjectSetEntry *)&PagingIEs[2], &_v285, (ObjectSetEntry *)&PagingIEs[0], NULL},
    {(ObjectSetEntry *)&PagingIEs[3], &_v286, (ObjectSetEntry *)&PagingIEs[1], NULL},
    {(ObjectSetEntry *)&PagingIEs[4], &_v287, (ObjectSetEntry *)&PagingIEs[2], NULL},
    {(ObjectSetEntry *)&PagingIEs[5], &_v288, (ObjectSetEntry *)&PagingIEs[3], NULL},
    {(ObjectSetEntry *)&PagingIEs[6], &_v289, (ObjectSetEntry *)&PagingIEs[4], NULL},
    {(ObjectSetEntry *)&PagingIEs[7], &_v290, (ObjectSetEntry *)&PagingIEs[5], NULL},
    {NULL, &_v291, (ObjectSetEntry *)&PagingIEs[6], NULL}
};
static ObjectSetEntry const TAIItemIEs[] = {
    {NULL, &_v292, NULL, NULL}
};
static ObjectSetEntry const TAIItemExtIEs[] = {
{0}};
static ObjectSetEntry const UEContextReleaseRequest_IEs[] = {
    {(ObjectSetEntry *)&UEContextReleaseRequest_IEs[1], &_v293, NULL, NULL},
    {(ObjectSetEntry *)&UEContextReleaseRequest_IEs[2], &_v294, (ObjectSetEntry *)&UEContextReleaseRequest_IEs[0], NULL},
    {(ObjectSetEntry *)&UEContextReleaseRequest_IEs[3], &_v295, (ObjectSetEntry *)&UEContextReleaseRequest_IEs[1], NULL},
    {NULL, &_v296, (ObjectSetEntry *)&UEContextReleaseRequest_IEs[2], NULL}
};
static ObjectSetEntry const UEContextReleaseCommand_IEs[] = {
    {(ObjectSetEntry *)&UEContextReleaseCommand_IEs[1], &_v297, NULL, NULL},
    {NULL, &_v298, (ObjectSetEntry *)&UEContextReleaseCommand_IEs[0], NULL}
};
static ObjectSetEntry const UEContextReleaseComplete_IEs[] = {
    {(ObjectSetEntry *)&UEContextReleaseComplete_IEs[1], &_v299, NULL, NULL},
    {(ObjectSetEntry *)&UEContextReleaseComplete_IEs[2], &_v300, (ObjectSetEntry *)&UEContextReleaseComplete_IEs[0], NULL},
    {(ObjectSetEntry *)&UEContextReleaseComplete_IEs[3], &_v301, (ObjectSetEntry *)&UEContextReleaseComplete_IEs[1], NULL},
    {NULL, &_v302, (ObjectSetEntry *)&UEContextReleaseComplete_IEs[2], NULL}
};
static ObjectSetEntry const UEContextModificationRequestIEs[] = {
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[1], &_v303, NULL, NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[2], &_v304, (ObjectSetEntry *)&UEContextModificationRequestIEs[0], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[3], &_v305, (ObjectSetEntry *)&UEContextModificationRequestIEs[1], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[4], &_v306, (ObjectSetEntry *)&UEContextModificationRequestIEs[2], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[5], &_v307, (ObjectSetEntry *)&UEContextModificationRequestIEs[3], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[6], &_v308, (ObjectSetEntry *)&UEContextModificationRequestIEs[4], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[7], &_v309, (ObjectSetEntry *)&UEContextModificationRequestIEs[5], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[8], &_v310, (ObjectSetEntry *)&UEContextModificationRequestIEs[6], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[9], &_v311, (ObjectSetEntry *)&UEContextModificationRequestIEs[7], NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[10], &_v312, (ObjectSetEntry *)&UEContextModificationRequestIEs[8], NULL},
    {NULL, &_v313, (ObjectSetEntry *)&UEContextModificationRequestIEs[9], NULL}
};
static ObjectSetEntry const UEContextModificationResponseIEs[] = {
    {(ObjectSetEntry *)&UEContextModificationResponseIEs[1], &_v314, NULL, NULL},
    {(ObjectSetEntry *)&UEContextModificationResponseIEs[2], &_v315, (ObjectSetEntry *)&UEContextModificationResponseIEs[0], NULL},
    {NULL, &_v316, (ObjectSetEntry *)&UEContextModificationResponseIEs[1], NULL}
};
static ObjectSetEntry const UEContextModificationFailureIEs[] = {
    {(ObjectSetEntry *)&UEContextModificationFailureIEs[1], &_v317, NULL, NULL},
    {(ObjectSetEntry *)&UEContextModificationFailureIEs[2], &_v318, (ObjectSetEntry *)&UEContextModificationFailureIEs[0], NULL},
    {(ObjectSetEntry *)&UEContextModificationFailureIEs[3], &_v319, (ObjectSetEntry *)&UEContextModificationFailureIEs[1], NULL},
    {NULL, &_v320, (ObjectSetEntry *)&UEContextModificationFailureIEs[2], NULL}
};
static ObjectSetEntry const UERadioCapabilityMatchRequestIEs[] = {
    {(ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[1], &_v321, NULL, NULL},
    {(ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[2], &_v322, (ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[0], NULL},
    {NULL, &_v323, (ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[1], NULL}
};
static ObjectSetEntry const UERadioCapabilityMatchResponseIEs[] = {
    {(ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[1], &_v324, NULL, NULL},
    {(ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[2], &_v325, (ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[0], NULL},
    {(ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[3], &_v326, (ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[1], NULL},
    {NULL, &_v327, (ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[2], NULL}
};
static ObjectSetEntry const DownlinkNASTransport_IEs[] = {
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[1], &_v328, NULL, NULL},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[2], &_v329, (ObjectSetEntry *)&DownlinkNASTransport_IEs[0], NULL},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[3], &_v330, (ObjectSetEntry *)&DownlinkNASTransport_IEs[1], NULL},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[4], &_v331, (ObjectSetEntry *)&DownlinkNASTransport_IEs[2], NULL},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[5], &_v332, (ObjectSetEntry *)&DownlinkNASTransport_IEs[3], NULL},
    {NULL, &_v333, (ObjectSetEntry *)&DownlinkNASTransport_IEs[4], NULL}
};
static ObjectSetEntry const InitialUEMessage_IEs[] = {
    {(ObjectSetEntry *)&InitialUEMessage_IEs[1], &_v334, NULL, NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[2], &_v335, (ObjectSetEntry *)&InitialUEMessage_IEs[0], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[3], &_v336, (ObjectSetEntry *)&InitialUEMessage_IEs[1], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[4], &_v337, (ObjectSetEntry *)&InitialUEMessage_IEs[2], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[5], &_v338, (ObjectSetEntry *)&InitialUEMessage_IEs[3], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[6], &_v339, (ObjectSetEntry *)&InitialUEMessage_IEs[4], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[7], &_v340, (ObjectSetEntry *)&InitialUEMessage_IEs[5], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[8], &_v341, (ObjectSetEntry *)&InitialUEMessage_IEs[6], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[9], &_v342, (ObjectSetEntry *)&InitialUEMessage_IEs[7], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[10], &_v343, (ObjectSetEntry *)&InitialUEMessage_IEs[8], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[11], &_v344, (ObjectSetEntry *)&InitialUEMessage_IEs[9], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[12], &_v345, (ObjectSetEntry *)&InitialUEMessage_IEs[10], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[13], &_v346, (ObjectSetEntry *)&InitialUEMessage_IEs[11], NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[14], &_v347, (ObjectSetEntry *)&InitialUEMessage_IEs[12], NULL},
    {NULL, &_v348, (ObjectSetEntry *)&InitialUEMessage_IEs[13], NULL}
};
static ObjectSetEntry const UplinkNASTransport_IEs[] = {
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[1], &_v349, NULL, NULL},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[2], &_v350, (ObjectSetEntry *)&UplinkNASTransport_IEs[0], NULL},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[3], &_v351, (ObjectSetEntry *)&UplinkNASTransport_IEs[1], NULL},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[4], &_v352, (ObjectSetEntry *)&UplinkNASTransport_IEs[2], NULL},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[5], &_v353, (ObjectSetEntry *)&UplinkNASTransport_IEs[3], NULL},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[6], &_v354, (ObjectSetEntry *)&UplinkNASTransport_IEs[4], NULL},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[7], &_v355, (ObjectSetEntry *)&UplinkNASTransport_IEs[5], NULL},
    {NULL, &_v356, (ObjectSetEntry *)&UplinkNASTransport_IEs[6], NULL}
};
static ObjectSetEntry const NASNonDeliveryIndication_IEs[] = {
    {(ObjectSetEntry *)&NASNonDeliveryIndication_IEs[1], &_v357, NULL, NULL},
    {(ObjectSetEntry *)&NASNonDeliveryIndication_IEs[2], &_v358, (ObjectSetEntry *)&NASNonDeliveryIndication_IEs[0], NULL},
    {(ObjectSetEntry *)&NASNonDeliveryIndication_IEs[3], &_v359, (ObjectSetEntry *)&NASNonDeliveryIndication_IEs[1], NULL},
    {NULL, &_v360, (ObjectSetEntry *)&NASNonDeliveryIndication_IEs[2], NULL}
};
static ObjectSetEntry const ResetIEs[] = {
    {(ObjectSetEntry *)&ResetIEs[1], &_v361, NULL, NULL},
    {NULL, &_v362, (ObjectSetEntry *)&ResetIEs[0], NULL}
};
static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemRes[] = {
    {NULL, &_v363, NULL, NULL}
};
static ObjectSetEntry const ResetAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&ResetAcknowledgeIEs[1], &_v364, NULL, NULL},
    {NULL, &_v365, (ObjectSetEntry *)&ResetAcknowledgeIEs[0], NULL}
};
static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemResAck[] = {
    {NULL, &_v366, NULL, NULL}
};
static ObjectSetEntry const ErrorIndicationIEs[] = {
    {(ObjectSetEntry *)&ErrorIndicationIEs[1], &_v367, NULL, NULL},
    {(ObjectSetEntry *)&ErrorIndicationIEs[2], &_v368, (ObjectSetEntry *)&ErrorIndicationIEs[0], NULL},
    {(ObjectSetEntry *)&ErrorIndicationIEs[3], &_v369, (ObjectSetEntry *)&ErrorIndicationIEs[1], NULL},
    {NULL, &_v370, (ObjectSetEntry *)&ErrorIndicationIEs[2], NULL}
};
static ObjectSetEntry const S1SetupRequestIEs[] = {
    {(ObjectSetEntry *)&S1SetupRequestIEs[1], &_v371, NULL, NULL},
    {(ObjectSetEntry *)&S1SetupRequestIEs[2], &_v372, (ObjectSetEntry *)&S1SetupRequestIEs[0], NULL},
    {(ObjectSetEntry *)&S1SetupRequestIEs[3], &_v373, (ObjectSetEntry *)&S1SetupRequestIEs[1], NULL},
    {(ObjectSetEntry *)&S1SetupRequestIEs[4], &_v374, (ObjectSetEntry *)&S1SetupRequestIEs[2], NULL},
    {NULL, &_v375, (ObjectSetEntry *)&S1SetupRequestIEs[3], NULL}
};
static ObjectSetEntry const S1SetupResponseIEs[] = {
    {(ObjectSetEntry *)&S1SetupResponseIEs[1], &_v376, NULL, NULL},
    {(ObjectSetEntry *)&S1SetupResponseIEs[2], &_v377, (ObjectSetEntry *)&S1SetupResponseIEs[0], NULL},
    {(ObjectSetEntry *)&S1SetupResponseIEs[3], &_v378, (ObjectSetEntry *)&S1SetupResponseIEs[1], NULL},
    {(ObjectSetEntry *)&S1SetupResponseIEs[4], &_v379, (ObjectSetEntry *)&S1SetupResponseIEs[2], NULL},
    {NULL, &_v380, (ObjectSetEntry *)&S1SetupResponseIEs[3], NULL}
};
static ObjectSetEntry const S1SetupFailureIEs[] = {
    {(ObjectSetEntry *)&S1SetupFailureIEs[1], &_v381, NULL, NULL},
    {(ObjectSetEntry *)&S1SetupFailureIEs[2], &_v382, (ObjectSetEntry *)&S1SetupFailureIEs[0], NULL},
    {NULL, &_v383, (ObjectSetEntry *)&S1SetupFailureIEs[1], NULL}
};
static ObjectSetEntry const ENBConfigurationUpdateIEs[] = {
    {(ObjectSetEntry *)&ENBConfigurationUpdateIEs[1], &_v384, NULL, NULL},
    {(ObjectSetEntry *)&ENBConfigurationUpdateIEs[2], &_v385, (ObjectSetEntry *)&ENBConfigurationUpdateIEs[0], NULL},
    {(ObjectSetEntry *)&ENBConfigurationUpdateIEs[3], &_v386, (ObjectSetEntry *)&ENBConfigurationUpdateIEs[1], NULL},
    {NULL, &_v387, (ObjectSetEntry *)&ENBConfigurationUpdateIEs[2], NULL}
};
static ObjectSetEntry const ENBConfigurationUpdateAcknowledgeIEs[] = {
    {NULL, &_v388, NULL, NULL}
};
static ObjectSetEntry const ENBConfigurationUpdateFailureIEs[] = {
    {(ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[1], &_v389, NULL, NULL},
    {(ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[2], &_v390, (ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[0], NULL},
    {NULL, &_v391, (ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[1], NULL}
};
static ObjectSetEntry const MMEConfigurationUpdateIEs[] = {
    {(ObjectSetEntry *)&MMEConfigurationUpdateIEs[1], &_v392, NULL, NULL},
    {(ObjectSetEntry *)&MMEConfigurationUpdateIEs[2], &_v393, (ObjectSetEntry *)&MMEConfigurationUpdateIEs[0], NULL},
    {NULL, &_v394, (ObjectSetEntry *)&MMEConfigurationUpdateIEs[1], NULL}
};
static ObjectSetEntry const MMEConfigurationUpdateAcknowledgeIEs[] = {
    {NULL, &_v395, NULL, NULL}
};
static ObjectSetEntry const MMEConfigurationUpdateFailureIEs[] = {
    {(ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[1], &_v396, NULL, NULL},
    {(ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[2], &_v397, (ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[0], NULL},
    {NULL, &_v398, (ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[1], NULL}
};
static ObjectSetEntry const DownlinkS1cdma2000tunnellingIEs[] = {
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[1], &_v399, NULL, NULL},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[2], &_v400, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[0], NULL},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[3], &_v401, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[1], NULL},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[4], &_v402, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[2], NULL},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[5], &_v403, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[3], NULL},
    {NULL, &_v404, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[4], NULL}
};
static ObjectSetEntry const UplinkS1cdma2000tunnellingIEs[] = {
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[1], &_v405, NULL, NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[2], &_v406, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[0], NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[3], &_v407, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[1], NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[4], &_v408, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[2], NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[5], &_v409, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[3], NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[6], &_v410, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[4], NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[7], &_v411, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[5], NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[8], &_v412, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[6], NULL},
    {NULL, &_v413, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[7], NULL}
};
static ObjectSetEntry const UECapabilityInfoIndicationIEs[] = {
    {(ObjectSetEntry *)&UECapabilityInfoIndicationIEs[1], &_v414, NULL, NULL},
    {(ObjectSetEntry *)&UECapabilityInfoIndicationIEs[2], &_v415, (ObjectSetEntry *)&UECapabilityInfoIndicationIEs[0], NULL},
    {(ObjectSetEntry *)&UECapabilityInfoIndicationIEs[3], &_v416, (ObjectSetEntry *)&UECapabilityInfoIndicationIEs[1], NULL},
    {NULL, &_v417, (ObjectSetEntry *)&UECapabilityInfoIndicationIEs[2], NULL}
};
static ObjectSetEntry const ENBStatusTransferIEs[] = {
    {(ObjectSetEntry *)&ENBStatusTransferIEs[1], &_v418, NULL, NULL},
    {(ObjectSetEntry *)&ENBStatusTransferIEs[2], &_v419, (ObjectSetEntry *)&ENBStatusTransferIEs[0], NULL},
    {NULL, &_v420, (ObjectSetEntry *)&ENBStatusTransferIEs[1], NULL}
};
static ObjectSetEntry const MMEStatusTransferIEs[] = {
    {(ObjectSetEntry *)&MMEStatusTransferIEs[1], &_v421, NULL, NULL},
    {(ObjectSetEntry *)&MMEStatusTransferIEs[2], &_v422, (ObjectSetEntry *)&MMEStatusTransferIEs[0], NULL},
    {NULL, &_v423, (ObjectSetEntry *)&MMEStatusTransferIEs[1], NULL}
};
static ObjectSetEntry const TraceStartIEs[] = {
    {(ObjectSetEntry *)&TraceStartIEs[1], &_v424, NULL, NULL},
    {(ObjectSetEntry *)&TraceStartIEs[2], &_v425, (ObjectSetEntry *)&TraceStartIEs[0], NULL},
    {NULL, &_v426, (ObjectSetEntry *)&TraceStartIEs[1], NULL}
};
static ObjectSetEntry const TraceFailureIndicationIEs[] = {
    {(ObjectSetEntry *)&TraceFailureIndicationIEs[1], &_v427, NULL, NULL},
    {(ObjectSetEntry *)&TraceFailureIndicationIEs[2], &_v428, (ObjectSetEntry *)&TraceFailureIndicationIEs[0], NULL},
    {(ObjectSetEntry *)&TraceFailureIndicationIEs[3], &_v429, (ObjectSetEntry *)&TraceFailureIndicationIEs[1], NULL},
    {NULL, &_v430, (ObjectSetEntry *)&TraceFailureIndicationIEs[2], NULL}
};
static ObjectSetEntry const DeactivateTraceIEs[] = {
    {(ObjectSetEntry *)&DeactivateTraceIEs[1], &_v431, NULL, NULL},
    {(ObjectSetEntry *)&DeactivateTraceIEs[2], &_v432, (ObjectSetEntry *)&DeactivateTraceIEs[0], NULL},
    {NULL, &_v433, (ObjectSetEntry *)&DeactivateTraceIEs[1], NULL}
};
static ObjectSetEntry const CellTrafficTraceIEs[] = {
    {(ObjectSetEntry *)&CellTrafficTraceIEs[1], &_v434, NULL, NULL},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[2], &_v435, (ObjectSetEntry *)&CellTrafficTraceIEs[0], NULL},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[3], &_v436, (ObjectSetEntry *)&CellTrafficTraceIEs[1], NULL},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[4], &_v437, (ObjectSetEntry *)&CellTrafficTraceIEs[2], NULL},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[5], &_v438, (ObjectSetEntry *)&CellTrafficTraceIEs[3], NULL},
    {NULL, &_v439, (ObjectSetEntry *)&CellTrafficTraceIEs[4], NULL}
};
static ObjectSetEntry const LocationReportingControlIEs[] = {
    {(ObjectSetEntry *)&LocationReportingControlIEs[1], &_v440, NULL, NULL},
    {(ObjectSetEntry *)&LocationReportingControlIEs[2], &_v441, (ObjectSetEntry *)&LocationReportingControlIEs[0], NULL},
    {NULL, &_v442, (ObjectSetEntry *)&LocationReportingControlIEs[1], NULL}
};
static ObjectSetEntry const LocationReportingFailureIndicationIEs[] = {
    {(ObjectSetEntry *)&LocationReportingFailureIndicationIEs[1], &_v443, NULL, NULL},
    {(ObjectSetEntry *)&LocationReportingFailureIndicationIEs[2], &_v444, (ObjectSetEntry *)&LocationReportingFailureIndicationIEs[0], NULL},
    {NULL, &_v445, (ObjectSetEntry *)&LocationReportingFailureIndicationIEs[1], NULL}
};
static ObjectSetEntry const LocationReportIEs[] = {
    {(ObjectSetEntry *)&LocationReportIEs[1], &_v446, NULL, NULL},
    {(ObjectSetEntry *)&LocationReportIEs[2], &_v447, (ObjectSetEntry *)&LocationReportIEs[0], NULL},
    {(ObjectSetEntry *)&LocationReportIEs[3], &_v448, (ObjectSetEntry *)&LocationReportIEs[1], NULL},
    {(ObjectSetEntry *)&LocationReportIEs[4], &_v449, (ObjectSetEntry *)&LocationReportIEs[2], NULL},
    {NULL, &_v450, (ObjectSetEntry *)&LocationReportIEs[3], NULL}
};
static ObjectSetEntry const OverloadStartIEs[] = {
    {(ObjectSetEntry *)&OverloadStartIEs[1], &_v451, NULL, NULL},
    {(ObjectSetEntry *)&OverloadStartIEs[2], &_v452, (ObjectSetEntry *)&OverloadStartIEs[0], NULL},
    {NULL, &_v453, (ObjectSetEntry *)&OverloadStartIEs[1], NULL}
};
static ObjectSetEntry const OverloadStopIEs[] = {
    {NULL, &_v454, NULL, NULL}
};
static ObjectSetEntry const WriteReplaceWarningRequestIEs[] = {
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[1], &_v455, NULL, NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[2], &_v456, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[0], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[3], &_v457, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[1], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[4], &_v458, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[2], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[5], &_v459, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[3], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[6], &_v460, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[4], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[7], &_v461, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[5], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[8], &_v462, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[6], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[9], &_v463, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[7], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[10], &_v464, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[8], NULL},
    {NULL, &_v465, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[9], NULL}
};
static ObjectSetEntry const WriteReplaceWarningResponseIEs[] = {
    {(ObjectSetEntry *)&WriteReplaceWarningResponseIEs[1], &_v466, NULL, NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningResponseIEs[2], &_v467, (ObjectSetEntry *)&WriteReplaceWarningResponseIEs[0], NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningResponseIEs[3], &_v468, (ObjectSetEntry *)&WriteReplaceWarningResponseIEs[1], NULL},
    {NULL, &_v469, (ObjectSetEntry *)&WriteReplaceWarningResponseIEs[2], NULL}
};
static ObjectSetEntry const ENBDirectInformationTransferIEs[] = {
    {NULL, &_v470, NULL, NULL}
};
static ObjectSetEntry const MMEDirectInformationTransferIEs[] = {
    {NULL, &_v471, NULL, NULL}
};
static ObjectSetEntry const ENBConfigurationTransferIEs[] = {
    {NULL, &_v472, NULL, NULL}
};
static ObjectSetEntry const MMEConfigurationTransferIEs[] = {
    {NULL, &_v473, NULL, NULL}
};
static ObjectSetEntry const PrivateMessageIEs[] = {
{0}};
static ObjectSetEntry const KillRequestIEs[] = {
    {(ObjectSetEntry *)&KillRequestIEs[1], &_v474, NULL, NULL},
    {(ObjectSetEntry *)&KillRequestIEs[2], &_v475, (ObjectSetEntry *)&KillRequestIEs[0], NULL},
    {(ObjectSetEntry *)&KillRequestIEs[3], &_v476, (ObjectSetEntry *)&KillRequestIEs[1], NULL},
    {NULL, &_v477, (ObjectSetEntry *)&KillRequestIEs[2], NULL}
};
static ObjectSetEntry const KillResponseIEs[] = {
    {(ObjectSetEntry *)&KillResponseIEs[1], &_v478, NULL, NULL},
    {(ObjectSetEntry *)&KillResponseIEs[2], &_v479, (ObjectSetEntry *)&KillResponseIEs[0], NULL},
    {(ObjectSetEntry *)&KillResponseIEs[3], &_v480, (ObjectSetEntry *)&KillResponseIEs[1], NULL},
    {NULL, &_v481, (ObjectSetEntry *)&KillResponseIEs[2], NULL}
};
static ObjectSetEntry const PWSRestartIndicationIEs[] = {
    {(ObjectSetEntry *)&PWSRestartIndicationIEs[1], &_v482, NULL, NULL},
    {(ObjectSetEntry *)&PWSRestartIndicationIEs[2], &_v483, (ObjectSetEntry *)&PWSRestartIndicationIEs[0], NULL},
    {(ObjectSetEntry *)&PWSRestartIndicationIEs[3], &_v484, (ObjectSetEntry *)&PWSRestartIndicationIEs[1], NULL},
    {NULL, &_v485, (ObjectSetEntry *)&PWSRestartIndicationIEs[2], NULL}
};
static ObjectSetEntry const DownlinkUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[1], &_v486, NULL, NULL},
    {(ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[2], &_v487, (ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[0], NULL},
    {(ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[3], &_v488, (ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[1], NULL},
    {NULL, &_v489, (ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[2], NULL}
};
static ObjectSetEntry const UplinkUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[1], &_v490, NULL, NULL},
    {(ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[2], &_v491, (ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[0], NULL},
    {(ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[3], &_v492, (ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[1], NULL},
    {NULL, &_v493, (ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[2], NULL}
};
static ObjectSetEntry const DownlinkNonUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&DownlinkNonUEAssociatedLPPaTransport_IEs[1], &_v494, NULL, NULL},
    {NULL, &_v495, (ObjectSetEntry *)&DownlinkNonUEAssociatedLPPaTransport_IEs[0], NULL}
};
static ObjectSetEntry const UplinkNonUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&UplinkNonUEAssociatedLPPaTransport_IEs[1], &_v496, NULL, NULL},
    {NULL, &_v497, (ObjectSetEntry *)&UplinkNonUEAssociatedLPPaTransport_IEs[0], NULL}
};
static ObjectSetEntry const E_RABModificationIndicationIEs[] = {
    {(ObjectSetEntry *)&E_RABModificationIndicationIEs[1], &_v498, NULL, NULL},
    {(ObjectSetEntry *)&E_RABModificationIndicationIEs[2], &_v499, (ObjectSetEntry *)&E_RABModificationIndicationIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABModificationIndicationIEs[3], &_v500, (ObjectSetEntry *)&E_RABModificationIndicationIEs[1], NULL},
    {NULL, &_v501, (ObjectSetEntry *)&E_RABModificationIndicationIEs[2], NULL}
};
static ObjectSetEntry const E_RABToBeModifiedItemBearerModIndIEs[] = {
    {NULL, &_v502, NULL, NULL}
};
static ObjectSetEntry const E_RABToBeModifiedItemBearerModInd_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABNotToBeModifiedItemBearerModIndIEs[] = {
    {NULL, &_v503, NULL, NULL}
};
static ObjectSetEntry const E_RABNotToBeModifiedItemBearerModInd_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABModificationConfirmIEs[] = {
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[1], &_v504, NULL, NULL},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[2], &_v505, (ObjectSetEntry *)&E_RABModificationConfirmIEs[0], NULL},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[3], &_v506, (ObjectSetEntry *)&E_RABModificationConfirmIEs[1], NULL},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[4], &_v507, (ObjectSetEntry *)&E_RABModificationConfirmIEs[2], NULL},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[5], &_v508, (ObjectSetEntry *)&E_RABModificationConfirmIEs[3], NULL},
    {NULL, &_v509, (ObjectSetEntry *)&E_RABModificationConfirmIEs[4], NULL}
};
static ObjectSetEntry const E_RABModifyItemBearerModConfIEs[] = {
    {NULL, &_v510, NULL, NULL}
};
static ObjectSetEntry const E_RABModifyItemBearerModConfExtIEs[] = {
{0}};
static ObjectSetEntry const AllocationAndRetentionPriority_ExtIEs[] = {
{0}};
static ObjectSetEntry const Bearers_SubjectToStatusTransfer_ItemIEs[] = {
    {NULL, &_v511, NULL, NULL}
};
static ObjectSetEntry const Bearers_SubjectToStatusTransfer_ItemExtIEs[] = {
    {(ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[1], &_v512, NULL, NULL},
    {(ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[2], &_v513, (ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[0], NULL},
    {NULL, &_v514, (ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[1], NULL}
};
static ObjectSetEntry const CancelledCellinEAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CancelledCellinTAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellID_Broadcast_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellID_Cancelled_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const Cdma2000OneXSRVCCInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellType_ExtIEs[] = {
{0}};
static ObjectSetEntry const CGI_ExtIEs[] = {
{0}};
static ObjectSetEntry const CSG_IdList_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const COUNTvalue_ExtIEs[] = {
{0}};
static ObjectSetEntry const COUNTValueExtended_ExtIEs[] = {
{0}};
static ObjectSetEntry const CriticalityDiagnostics_ExtIEs[] = {
{0}};
static ObjectSetEntry const CriticalityDiagnostics_IE_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const EmergencyAreaID_Broadcast_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const EmergencyAreaID_Cancelled_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CompletedCellinEAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const GERAN_Cell_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const GlobalENB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const ENB_StatusTransfer_TransparentContainer_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABInformationListIEs[] = {
    {NULL, &_v515, NULL, NULL}
};
static ObjectSetEntry const E_RABInformationListItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABItemIEs[] = {
    {NULL, &_v516, NULL, NULL}
};
static ObjectSetEntry const E_RABItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABQoSParameters_ExtIEs[] = {
{0}};
static ObjectSetEntry const EUTRAN_CGI_ExtIEs[] = {
{0}};
static ObjectSetEntry const ExpectedUEBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const ExpectedUEActivityBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const ForbiddenTAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const ForbiddenLAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const GBR_QosInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const GUMMEI_ExtIEs[] = {
{0}};
static ObjectSetEntry const HandoverRestrictionList_ExtIEs[] = {
{0}};
static ObjectSetEntry const ImmediateMDT_ExtIEs[] = {
    {(ObjectSetEntry *)&ImmediateMDT_ExtIEs[1], &_v517, NULL, NULL},
    {(ObjectSetEntry *)&ImmediateMDT_ExtIEs[2], &_v518, (ObjectSetEntry *)&ImmediateMDT_ExtIEs[0], NULL},
    {(ObjectSetEntry *)&ImmediateMDT_ExtIEs[3], &_v519, (ObjectSetEntry *)&ImmediateMDT_ExtIEs[1], NULL},
    {NULL, &_v520, (ObjectSetEntry *)&ImmediateMDT_ExtIEs[2], NULL}
};
static ObjectSetEntry const LAI_ExtIEs[] = {
{0}};
static ObjectSetEntry const LastVisitedEUTRANCellInformation_ExtIEs[] = {
    {(ObjectSetEntry *)&LastVisitedEUTRANCellInformation_ExtIEs[1], &_v521, NULL, NULL},
    {NULL, &_v522, (ObjectSetEntry *)&LastVisitedEUTRANCellInformation_ExtIEs[0], NULL}
};
static ObjectSetEntry const ListeningSubframePattern_ExtIEs[] = {
{0}};
static ObjectSetEntry const LoggedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const LoggedMBSFNMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const M3Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const M4Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const M5Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const MDT_Configuration_ExtIEs[] = {
    {NULL, &_v523, NULL, NULL}
};
static ObjectSetEntry const MBSFN_ResultToLogInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const MutingPatternInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const M1PeriodicReporting_ExtIEs[] = {
{0}};
static ObjectSetEntry const ProSeAuthorized_ExtIEs[] = {
{0}};
static ObjectSetEntry const RequestType_ExtIEs[] = {
{0}};
static ObjectSetEntry const RIMTransfer_ExtIEs[] = {
{0}};
static ObjectSetEntry const RLFReportInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const SecurityContext_ExtIEs[] = {
{0}};
static ObjectSetEntry const SONInformationReply_ExtIEs[] = {
    {(ObjectSetEntry *)&SONInformationReply_ExtIEs[1], &_v526, NULL, NULL},
    {NULL, &_v527, (ObjectSetEntry *)&SONInformationReply_ExtIEs[0], NULL}
};
static ObjectSetEntry const SONConfigurationTransfer_ExtIEs[] = {
    {(ObjectSetEntry *)&SONConfigurationTransfer_ExtIEs[1], &_v528, NULL, NULL},
    {NULL, &_v529, (ObjectSetEntry *)&SONConfigurationTransfer_ExtIEs[0], NULL}
};
static ObjectSetEntry const SynchronisationInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const SourceeNB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[] = {
    {(ObjectSetEntry *)&SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[1], &_v530, NULL, NULL},
    {NULL, &_v531, (ObjectSetEntry *)&SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[0], NULL}
};
static ObjectSetEntry const ServedGUMMEIsItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const SupportedTAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TimeSynchronisationInfo_ExtIEs[] = {
    {NULL, &_v532, NULL, NULL}
};
static ObjectSetEntry const S_TMSI_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAIBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAI_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAI_Broadcast_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAI_Cancelled_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TABasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const CompletedCellinTAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TargeteNB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const TargetRNC_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const TargeteNB_ToSourceeNB_TransparentContainer_ExtIEs[] = {
{0}};
static ObjectSetEntry const M1ThresholdEventA2_ExtIEs[] = {
{0}};
static ObjectSetEntry const TraceActivation_ExtIEs[] = {
    {NULL, &_v533, NULL, NULL}
};
static ObjectSetEntry const Tunnel_Information_ExtIEs[] = {
{0}};
static ObjectSetEntry const UEAggregate_MaximumBitrates_ExtIEs[] = {
{0}};
static ObjectSetEntry const UE_S1AP_ID_pair_ExtIEs[] = {
{0}};
static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemExtIEs[] = {
{0}};
static ObjectSetEntry const UESecurityCapabilities_ExtIEs[] = {
{0}};
static ObjectSetEntry const UserLocationInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2TNLConfigurationInfo_ExtIEs[] = {
    {(ObjectSetEntry *)&X2TNLConfigurationInfo_ExtIEs[1], &_v534, NULL, NULL},
    {NULL, &_v535, (ObjectSetEntry *)&X2TNLConfigurationInfo_ExtIEs[0], NULL}
};
static ObjectSetEntry const ENBX2ExtTLA_ExtIEs[] = {
{0}};
static ObjectSetEntry const MDTMode_ExtensionIE[] = {
    {NULL, &_v524, NULL, NULL}
};
static ObjectSetEntry const SONInformation_ExtensionIE[] = {
    {NULL, &_v525, NULL, NULL}
};
#else /* OSS_SPARTAN_AWARE <= 12 */
static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES[] = {
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[1], &_v1, NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[2], &_v2, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[0]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[3], &_v3, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[1]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[4], &_v4, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[2]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[5], &_v5, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[3]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[6], &_v6, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[4]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[7], &_v7, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[5]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[8], &_v8, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[6]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[9], &_v9, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[7]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[10], &_v10, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[8]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[11], &_v11, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[9]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[12], &_v12, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[10]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[13], &_v13, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[11]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[14], &_v14, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[12]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[15], &_v15, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[13]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[16], &_v16, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[14]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[17], &_v17, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[15]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[18], &_v18, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[16]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[19], &_v19, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[17]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[20], &_v20, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[18]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[21], &_v21, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[19]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[22], &_v22, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[20]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[23], &_v23, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[21]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[24], &_v24, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[22]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[25], &_v25, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[23]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[26], &_v26, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[24]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[27], &_v27, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[25]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[28], &_v28, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[26]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[29], &_v29, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[27]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[30], &_v30, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[28]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[31], &_v31, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[29]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[32], &_v32, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[30]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[33], &_v33, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[31]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[34], &_v34, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[32]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[35], &_v35, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[33]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[36], &_v36, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[34]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[37], &_v37, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[35]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[38], &_v38, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[36]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[39], &_v39, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[37]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[40], &_v40, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[38]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[41], &_v41, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[39]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[42], &_v42, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[40]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[43], &_v43, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[41]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[44], &_v44, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[42]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[45], &_v45, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[43]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[46], &_v46, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[44]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[47], &_v47, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[45]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[48], &_v48, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[46]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[49], &_v49, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[47]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[50], &_v50, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[48]},
    {NULL, &_v51, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES[49]}
};
static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES_CLASS_1[] = {
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[1], &_v52, NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[2], &_v53, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[0]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[3], &_v54, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[1]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[4], &_v55, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[2]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[5], &_v56, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[3]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[6], &_v57, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[4]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[7], &_v58, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[5]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[8], &_v59, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[6]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[9], &_v60, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[7]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[10], &_v61, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[8]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[11], &_v62, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[9]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[12], &_v63, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[10]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[13], &_v64, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[11]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[14], &_v65, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[12]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[15], &_v66, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[13]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[16], &_v67, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[14]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[17], &_v68, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[15]},
    {NULL, &_v69, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_1[16]}
};
static ObjectSetEntry const S1AP_ELEMENTARY_PROCEDURES_CLASS_2[] = {
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[1], &_v70, NULL},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[2], &_v71, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[0]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[3], &_v72, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[1]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[4], &_v73, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[2]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[5], &_v74, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[3]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[6], &_v75, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[4]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[7], &_v76, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[5]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[8], &_v77, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[6]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[9], &_v78, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[7]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[10], &_v79, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[8]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[11], &_v80, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[9]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[12], &_v81, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[10]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[13], &_v82, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[11]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[14], &_v83, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[12]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[15], &_v84, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[13]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[16], &_v85, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[14]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[17], &_v86, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[15]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[18], &_v87, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[16]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[19], &_v88, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[17]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[20], &_v89, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[18]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[21], &_v90, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[19]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[22], &_v91, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[20]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[23], &_v92, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[21]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[24], &_v93, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[22]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[25], &_v94, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[23]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[26], &_v95, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[24]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[27], &_v96, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[25]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[28], &_v97, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[26]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[29], &_v98, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[27]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[30], &_v99, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[28]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[31], &_v100, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[29]},
    {(ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[32], &_v101, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[30]},
    {NULL, &_v102, (ObjectSetEntry *)&S1AP_ELEMENTARY_PROCEDURES_CLASS_2[31]}
};
static ObjectSetEntry const HandoverRequiredIEs[] = {
    {(ObjectSetEntry *)&HandoverRequiredIEs[1], &_v103, NULL},
    {(ObjectSetEntry *)&HandoverRequiredIEs[2], &_v104, (ObjectSetEntry *)&HandoverRequiredIEs[0]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[3], &_v105, (ObjectSetEntry *)&HandoverRequiredIEs[1]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[4], &_v106, (ObjectSetEntry *)&HandoverRequiredIEs[2]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[5], &_v107, (ObjectSetEntry *)&HandoverRequiredIEs[3]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[6], &_v108, (ObjectSetEntry *)&HandoverRequiredIEs[4]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[7], &_v109, (ObjectSetEntry *)&HandoverRequiredIEs[5]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[8], &_v110, (ObjectSetEntry *)&HandoverRequiredIEs[6]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[9], &_v111, (ObjectSetEntry *)&HandoverRequiredIEs[7]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[10], &_v112, (ObjectSetEntry *)&HandoverRequiredIEs[8]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[11], &_v113, (ObjectSetEntry *)&HandoverRequiredIEs[9]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[12], &_v114, (ObjectSetEntry *)&HandoverRequiredIEs[10]},
    {(ObjectSetEntry *)&HandoverRequiredIEs[13], &_v115, (ObjectSetEntry *)&HandoverRequiredIEs[11]},
    {NULL, &_v116, (ObjectSetEntry *)&HandoverRequiredIEs[12]}
};
static ObjectSetEntry const HandoverCommandIEs[] = {
    {(ObjectSetEntry *)&HandoverCommandIEs[1], &_v117, NULL},
    {(ObjectSetEntry *)&HandoverCommandIEs[2], &_v118, (ObjectSetEntry *)&HandoverCommandIEs[0]},
    {(ObjectSetEntry *)&HandoverCommandIEs[3], &_v119, (ObjectSetEntry *)&HandoverCommandIEs[1]},
    {(ObjectSetEntry *)&HandoverCommandIEs[4], &_v120, (ObjectSetEntry *)&HandoverCommandIEs[2]},
    {(ObjectSetEntry *)&HandoverCommandIEs[5], &_v121, (ObjectSetEntry *)&HandoverCommandIEs[3]},
    {(ObjectSetEntry *)&HandoverCommandIEs[6], &_v122, (ObjectSetEntry *)&HandoverCommandIEs[4]},
    {(ObjectSetEntry *)&HandoverCommandIEs[7], &_v123, (ObjectSetEntry *)&HandoverCommandIEs[5]},
    {(ObjectSetEntry *)&HandoverCommandIEs[8], &_v124, (ObjectSetEntry *)&HandoverCommandIEs[6]},
    {NULL, &_v125, (ObjectSetEntry *)&HandoverCommandIEs[7]}
};
static ObjectSetEntry const E_RABDataForwardingItemIEs[] = {
    {NULL, &_v126, NULL}
};
static ObjectSetEntry const E_RABDataForwardingItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const HandoverPreparationFailureIEs[] = {
    {(ObjectSetEntry *)&HandoverPreparationFailureIEs[1], &_v127, NULL},
    {(ObjectSetEntry *)&HandoverPreparationFailureIEs[2], &_v128, (ObjectSetEntry *)&HandoverPreparationFailureIEs[0]},
    {(ObjectSetEntry *)&HandoverPreparationFailureIEs[3], &_v129, (ObjectSetEntry *)&HandoverPreparationFailureIEs[1]},
    {NULL, &_v130, (ObjectSetEntry *)&HandoverPreparationFailureIEs[2]}
};
static ObjectSetEntry const HandoverRequestIEs[] = {
    {(ObjectSetEntry *)&HandoverRequestIEs[1], &_v131, NULL},
    {(ObjectSetEntry *)&HandoverRequestIEs[2], &_v132, (ObjectSetEntry *)&HandoverRequestIEs[0]},
    {(ObjectSetEntry *)&HandoverRequestIEs[3], &_v133, (ObjectSetEntry *)&HandoverRequestIEs[1]},
    {(ObjectSetEntry *)&HandoverRequestIEs[4], &_v134, (ObjectSetEntry *)&HandoverRequestIEs[2]},
    {(ObjectSetEntry *)&HandoverRequestIEs[5], &_v135, (ObjectSetEntry *)&HandoverRequestIEs[3]},
    {(ObjectSetEntry *)&HandoverRequestIEs[6], &_v136, (ObjectSetEntry *)&HandoverRequestIEs[4]},
    {(ObjectSetEntry *)&HandoverRequestIEs[7], &_v137, (ObjectSetEntry *)&HandoverRequestIEs[5]},
    {(ObjectSetEntry *)&HandoverRequestIEs[8], &_v138, (ObjectSetEntry *)&HandoverRequestIEs[6]},
    {(ObjectSetEntry *)&HandoverRequestIEs[9], &_v139, (ObjectSetEntry *)&HandoverRequestIEs[7]},
    {(ObjectSetEntry *)&HandoverRequestIEs[10], &_v140, (ObjectSetEntry *)&HandoverRequestIEs[8]},
    {(ObjectSetEntry *)&HandoverRequestIEs[11], &_v141, (ObjectSetEntry *)&HandoverRequestIEs[9]},
    {(ObjectSetEntry *)&HandoverRequestIEs[12], &_v142, (ObjectSetEntry *)&HandoverRequestIEs[10]},
    {(ObjectSetEntry *)&HandoverRequestIEs[13], &_v143, (ObjectSetEntry *)&HandoverRequestIEs[11]},
    {(ObjectSetEntry *)&HandoverRequestIEs[14], &_v144, (ObjectSetEntry *)&HandoverRequestIEs[12]},
    {(ObjectSetEntry *)&HandoverRequestIEs[15], &_v145, (ObjectSetEntry *)&HandoverRequestIEs[13]},
    {(ObjectSetEntry *)&HandoverRequestIEs[16], &_v146, (ObjectSetEntry *)&HandoverRequestIEs[14]},
    {(ObjectSetEntry *)&HandoverRequestIEs[17], &_v147, (ObjectSetEntry *)&HandoverRequestIEs[15]},
    {(ObjectSetEntry *)&HandoverRequestIEs[18], &_v148, (ObjectSetEntry *)&HandoverRequestIEs[16]},
    {(ObjectSetEntry *)&HandoverRequestIEs[19], &_v149, (ObjectSetEntry *)&HandoverRequestIEs[17]},
    {(ObjectSetEntry *)&HandoverRequestIEs[20], &_v150, (ObjectSetEntry *)&HandoverRequestIEs[18]},
    {(ObjectSetEntry *)&HandoverRequestIEs[21], &_v151, (ObjectSetEntry *)&HandoverRequestIEs[19]},
    {NULL, &_v152, (ObjectSetEntry *)&HandoverRequestIEs[20]}
};
static ObjectSetEntry const E_RABToBeSetupItemHOReqIEs[] = {
    {NULL, &_v153, NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemHOReq_ExtIEs[] = {
    {NULL, &_v154, NULL}
};
static ObjectSetEntry const HandoverRequestAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[1], &_v155, NULL},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[2], &_v156, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[0]},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[3], &_v157, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[1]},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[4], &_v158, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[2]},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[5], &_v159, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[3]},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[6], &_v160, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[4]},
    {(ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[7], &_v161, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[5]},
    {NULL, &_v162, (ObjectSetEntry *)&HandoverRequestAcknowledgeIEs[6]}
};
static ObjectSetEntry const E_RABAdmittedItemIEs[] = {
    {NULL, &_v163, NULL}
};
static ObjectSetEntry const E_RABAdmittedItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABFailedtoSetupItemHOReqAckIEs[] = {
    {NULL, &_v164, NULL}
};
static ObjectSetEntry const E_RABFailedToSetupItemHOReqAckExtIEs[] = {
{0}};
static ObjectSetEntry const HandoverFailureIEs[] = {
    {(ObjectSetEntry *)&HandoverFailureIEs[1], &_v165, NULL},
    {(ObjectSetEntry *)&HandoverFailureIEs[2], &_v166, (ObjectSetEntry *)&HandoverFailureIEs[0]},
    {NULL, &_v167, (ObjectSetEntry *)&HandoverFailureIEs[1]}
};
static ObjectSetEntry const HandoverNotifyIEs[] = {
    {(ObjectSetEntry *)&HandoverNotifyIEs[1], &_v168, NULL},
    {(ObjectSetEntry *)&HandoverNotifyIEs[2], &_v169, (ObjectSetEntry *)&HandoverNotifyIEs[0]},
    {(ObjectSetEntry *)&HandoverNotifyIEs[3], &_v170, (ObjectSetEntry *)&HandoverNotifyIEs[1]},
    {(ObjectSetEntry *)&HandoverNotifyIEs[4], &_v171, (ObjectSetEntry *)&HandoverNotifyIEs[2]},
    {(ObjectSetEntry *)&HandoverNotifyIEs[5], &_v172, (ObjectSetEntry *)&HandoverNotifyIEs[3]},
    {NULL, &_v173, (ObjectSetEntry *)&HandoverNotifyIEs[4]}
};
static ObjectSetEntry const PathSwitchRequestIEs[] = {
    {(ObjectSetEntry *)&PathSwitchRequestIEs[1], &_v174, NULL},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[2], &_v175, (ObjectSetEntry *)&PathSwitchRequestIEs[0]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[3], &_v176, (ObjectSetEntry *)&PathSwitchRequestIEs[1]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[4], &_v177, (ObjectSetEntry *)&PathSwitchRequestIEs[2]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[5], &_v178, (ObjectSetEntry *)&PathSwitchRequestIEs[3]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[6], &_v179, (ObjectSetEntry *)&PathSwitchRequestIEs[4]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[7], &_v180, (ObjectSetEntry *)&PathSwitchRequestIEs[5]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[8], &_v181, (ObjectSetEntry *)&PathSwitchRequestIEs[6]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[9], &_v182, (ObjectSetEntry *)&PathSwitchRequestIEs[7]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[10], &_v183, (ObjectSetEntry *)&PathSwitchRequestIEs[8]},
    {(ObjectSetEntry *)&PathSwitchRequestIEs[11], &_v184, (ObjectSetEntry *)&PathSwitchRequestIEs[9]},
    {NULL, &_v185, (ObjectSetEntry *)&PathSwitchRequestIEs[10]}
};
static ObjectSetEntry const E_RABToBeSwitchedDLItemIEs[] = {
    {NULL, &_v186, NULL}
};
static ObjectSetEntry const E_RABToBeSwitchedDLItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const PathSwitchRequestAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[1], &_v187, NULL},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[2], &_v188, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[0]},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[3], &_v189, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[1]},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[4], &_v190, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[2]},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[5], &_v191, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[3]},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[6], &_v192, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[4]},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[7], &_v193, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[5]},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[8], &_v194, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[6]},
    {(ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[9], &_v195, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[7]},
    {NULL, &_v196, (ObjectSetEntry *)&PathSwitchRequestAcknowledgeIEs[8]}
};
static ObjectSetEntry const E_RABToBeSwitchedULItemIEs[] = {
    {NULL, &_v197, NULL}
};
static ObjectSetEntry const E_RABToBeSwitchedULItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const PathSwitchRequestFailureIEs[] = {
    {(ObjectSetEntry *)&PathSwitchRequestFailureIEs[1], &_v198, NULL},
    {(ObjectSetEntry *)&PathSwitchRequestFailureIEs[2], &_v199, (ObjectSetEntry *)&PathSwitchRequestFailureIEs[0]},
    {(ObjectSetEntry *)&PathSwitchRequestFailureIEs[3], &_v200, (ObjectSetEntry *)&PathSwitchRequestFailureIEs[1]},
    {NULL, &_v201, (ObjectSetEntry *)&PathSwitchRequestFailureIEs[2]}
};
static ObjectSetEntry const HandoverCancelIEs[] = {
    {(ObjectSetEntry *)&HandoverCancelIEs[1], &_v202, NULL},
    {(ObjectSetEntry *)&HandoverCancelIEs[2], &_v203, (ObjectSetEntry *)&HandoverCancelIEs[0]},
    {NULL, &_v204, (ObjectSetEntry *)&HandoverCancelIEs[1]}
};
static ObjectSetEntry const HandoverCancelAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[1], &_v205, NULL},
    {(ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[2], &_v206, (ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[0]},
    {NULL, &_v207, (ObjectSetEntry *)&HandoverCancelAcknowledgeIEs[1]}
};
static ObjectSetEntry const E_RABSetupRequestIEs[] = {
    {(ObjectSetEntry *)&E_RABSetupRequestIEs[1], &_v208, NULL},
    {(ObjectSetEntry *)&E_RABSetupRequestIEs[2], &_v209, (ObjectSetEntry *)&E_RABSetupRequestIEs[0]},
    {(ObjectSetEntry *)&E_RABSetupRequestIEs[3], &_v210, (ObjectSetEntry *)&E_RABSetupRequestIEs[1]},
    {NULL, &_v211, (ObjectSetEntry *)&E_RABSetupRequestIEs[2]}
};
static ObjectSetEntry const E_RABToBeSetupItemBearerSUReqIEs[] = {
    {NULL, &_v212, NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemBearerSUReqExtIEs[] = {
    {(ObjectSetEntry *)&E_RABToBeSetupItemBearerSUReqExtIEs[1], &_v213, NULL},
    {NULL, &_v214, (ObjectSetEntry *)&E_RABToBeSetupItemBearerSUReqExtIEs[0]}
};
static ObjectSetEntry const E_RABSetupResponseIEs[] = {
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[1], &_v215, NULL},
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[2], &_v216, (ObjectSetEntry *)&E_RABSetupResponseIEs[0]},
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[3], &_v217, (ObjectSetEntry *)&E_RABSetupResponseIEs[1]},
    {(ObjectSetEntry *)&E_RABSetupResponseIEs[4], &_v218, (ObjectSetEntry *)&E_RABSetupResponseIEs[2]},
    {NULL, &_v219, (ObjectSetEntry *)&E_RABSetupResponseIEs[3]}
};
static ObjectSetEntry const E_RABSetupItemBearerSUResIEs[] = {
    {NULL, &_v220, NULL}
};
static ObjectSetEntry const E_RABSetupItemBearerSUResExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABModifyRequestIEs[] = {
    {(ObjectSetEntry *)&E_RABModifyRequestIEs[1], &_v221, NULL},
    {(ObjectSetEntry *)&E_RABModifyRequestIEs[2], &_v222, (ObjectSetEntry *)&E_RABModifyRequestIEs[0]},
    {(ObjectSetEntry *)&E_RABModifyRequestIEs[3], &_v223, (ObjectSetEntry *)&E_RABModifyRequestIEs[1]},
    {NULL, &_v224, (ObjectSetEntry *)&E_RABModifyRequestIEs[2]}
};
static ObjectSetEntry const E_RABToBeModifiedItemBearerModReqIEs[] = {
    {NULL, &_v225, NULL}
};
static ObjectSetEntry const E_RABToBeModifyItemBearerModReqExtIEs[] = {
    {NULL, &_v226, NULL}
};
static ObjectSetEntry const E_RABModifyResponseIEs[] = {
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[1], &_v227, NULL},
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[2], &_v228, (ObjectSetEntry *)&E_RABModifyResponseIEs[0]},
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[3], &_v229, (ObjectSetEntry *)&E_RABModifyResponseIEs[1]},
    {(ObjectSetEntry *)&E_RABModifyResponseIEs[4], &_v230, (ObjectSetEntry *)&E_RABModifyResponseIEs[2]},
    {NULL, &_v231, (ObjectSetEntry *)&E_RABModifyResponseIEs[3]}
};
static ObjectSetEntry const E_RABModifyItemBearerModResIEs[] = {
    {NULL, &_v232, NULL}
};
static ObjectSetEntry const E_RABModifyItemBearerModResExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABReleaseCommandIEs[] = {
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[1], &_v233, NULL},
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[2], &_v234, (ObjectSetEntry *)&E_RABReleaseCommandIEs[0]},
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[3], &_v235, (ObjectSetEntry *)&E_RABReleaseCommandIEs[1]},
    {(ObjectSetEntry *)&E_RABReleaseCommandIEs[4], &_v236, (ObjectSetEntry *)&E_RABReleaseCommandIEs[2]},
    {NULL, &_v237, (ObjectSetEntry *)&E_RABReleaseCommandIEs[3]}
};
static ObjectSetEntry const E_RABReleaseResponseIEs[] = {
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[1], &_v238, NULL},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[2], &_v239, (ObjectSetEntry *)&E_RABReleaseResponseIEs[0]},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[3], &_v240, (ObjectSetEntry *)&E_RABReleaseResponseIEs[1]},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[4], &_v241, (ObjectSetEntry *)&E_RABReleaseResponseIEs[2]},
    {(ObjectSetEntry *)&E_RABReleaseResponseIEs[5], &_v242, (ObjectSetEntry *)&E_RABReleaseResponseIEs[3]},
    {NULL, &_v243, (ObjectSetEntry *)&E_RABReleaseResponseIEs[4]}
};
static ObjectSetEntry const E_RABReleaseItemBearerRelCompIEs[] = {
    {NULL, &_v244, NULL}
};
static ObjectSetEntry const E_RABReleaseItemBearerRelCompExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABReleaseIndicationIEs[] = {
    {(ObjectSetEntry *)&E_RABReleaseIndicationIEs[1], &_v245, NULL},
    {(ObjectSetEntry *)&E_RABReleaseIndicationIEs[2], &_v246, (ObjectSetEntry *)&E_RABReleaseIndicationIEs[0]},
    {(ObjectSetEntry *)&E_RABReleaseIndicationIEs[3], &_v247, (ObjectSetEntry *)&E_RABReleaseIndicationIEs[1]},
    {NULL, &_v248, (ObjectSetEntry *)&E_RABReleaseIndicationIEs[2]}
};
static ObjectSetEntry const InitialContextSetupRequestIEs[] = {
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[1], &_v249, NULL},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[2], &_v250, (ObjectSetEntry *)&InitialContextSetupRequestIEs[0]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[3], &_v251, (ObjectSetEntry *)&InitialContextSetupRequestIEs[1]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[4], &_v252, (ObjectSetEntry *)&InitialContextSetupRequestIEs[2]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[5], &_v253, (ObjectSetEntry *)&InitialContextSetupRequestIEs[3]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[6], &_v254, (ObjectSetEntry *)&InitialContextSetupRequestIEs[4]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[7], &_v255, (ObjectSetEntry *)&InitialContextSetupRequestIEs[5]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[8], &_v256, (ObjectSetEntry *)&InitialContextSetupRequestIEs[6]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[9], &_v257, (ObjectSetEntry *)&InitialContextSetupRequestIEs[7]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[10], &_v258, (ObjectSetEntry *)&InitialContextSetupRequestIEs[8]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[11], &_v259, (ObjectSetEntry *)&InitialContextSetupRequestIEs[9]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[12], &_v260, (ObjectSetEntry *)&InitialContextSetupRequestIEs[10]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[13], &_v261, (ObjectSetEntry *)&InitialContextSetupRequestIEs[11]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[14], &_v262, (ObjectSetEntry *)&InitialContextSetupRequestIEs[12]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[15], &_v263, (ObjectSetEntry *)&InitialContextSetupRequestIEs[13]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[16], &_v264, (ObjectSetEntry *)&InitialContextSetupRequestIEs[14]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[17], &_v265, (ObjectSetEntry *)&InitialContextSetupRequestIEs[15]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[18], &_v266, (ObjectSetEntry *)&InitialContextSetupRequestIEs[16]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[19], &_v267, (ObjectSetEntry *)&InitialContextSetupRequestIEs[17]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[20], &_v268, (ObjectSetEntry *)&InitialContextSetupRequestIEs[18]},
    {(ObjectSetEntry *)&InitialContextSetupRequestIEs[21], &_v269, (ObjectSetEntry *)&InitialContextSetupRequestIEs[19]},
    {NULL, &_v270, (ObjectSetEntry *)&InitialContextSetupRequestIEs[20]}
};
static ObjectSetEntry const E_RABToBeSetupItemCtxtSUReqIEs[] = {
    {NULL, &_v271, NULL}
};
static ObjectSetEntry const E_RABToBeSetupItemCtxtSUReqExtIEs[] = {
    {(ObjectSetEntry *)&E_RABToBeSetupItemCtxtSUReqExtIEs[1], &_v272, NULL},
    {NULL, &_v273, (ObjectSetEntry *)&E_RABToBeSetupItemCtxtSUReqExtIEs[0]}
};
static ObjectSetEntry const InitialContextSetupResponseIEs[] = {
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[1], &_v274, NULL},
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[2], &_v275, (ObjectSetEntry *)&InitialContextSetupResponseIEs[0]},
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[3], &_v276, (ObjectSetEntry *)&InitialContextSetupResponseIEs[1]},
    {(ObjectSetEntry *)&InitialContextSetupResponseIEs[4], &_v277, (ObjectSetEntry *)&InitialContextSetupResponseIEs[2]},
    {NULL, &_v278, (ObjectSetEntry *)&InitialContextSetupResponseIEs[3]}
};
static ObjectSetEntry const E_RABSetupItemCtxtSUResIEs[] = {
    {NULL, &_v279, NULL}
};
static ObjectSetEntry const E_RABSetupItemCtxtSUResExtIEs[] = {
{0}};
static ObjectSetEntry const InitialContextSetupFailureIEs[] = {
    {(ObjectSetEntry *)&InitialContextSetupFailureIEs[1], &_v280, NULL},
    {(ObjectSetEntry *)&InitialContextSetupFailureIEs[2], &_v281, (ObjectSetEntry *)&InitialContextSetupFailureIEs[0]},
    {(ObjectSetEntry *)&InitialContextSetupFailureIEs[3], &_v282, (ObjectSetEntry *)&InitialContextSetupFailureIEs[1]},
    {NULL, &_v283, (ObjectSetEntry *)&InitialContextSetupFailureIEs[2]}
};
static ObjectSetEntry const PagingIEs[] = {
    {(ObjectSetEntry *)&PagingIEs[1], &_v284, NULL},
    {(ObjectSetEntry *)&PagingIEs[2], &_v285, (ObjectSetEntry *)&PagingIEs[0]},
    {(ObjectSetEntry *)&PagingIEs[3], &_v286, (ObjectSetEntry *)&PagingIEs[1]},
    {(ObjectSetEntry *)&PagingIEs[4], &_v287, (ObjectSetEntry *)&PagingIEs[2]},
    {(ObjectSetEntry *)&PagingIEs[5], &_v288, (ObjectSetEntry *)&PagingIEs[3]},
    {(ObjectSetEntry *)&PagingIEs[6], &_v289, (ObjectSetEntry *)&PagingIEs[4]},
    {(ObjectSetEntry *)&PagingIEs[7], &_v290, (ObjectSetEntry *)&PagingIEs[5]},
    {NULL, &_v291, (ObjectSetEntry *)&PagingIEs[6]}
};
static ObjectSetEntry const TAIItemIEs[] = {
    {NULL, &_v292, NULL}
};
static ObjectSetEntry const TAIItemExtIEs[] = {
{0}};
static ObjectSetEntry const UEContextReleaseRequest_IEs[] = {
    {(ObjectSetEntry *)&UEContextReleaseRequest_IEs[1], &_v293, NULL},
    {(ObjectSetEntry *)&UEContextReleaseRequest_IEs[2], &_v294, (ObjectSetEntry *)&UEContextReleaseRequest_IEs[0]},
    {(ObjectSetEntry *)&UEContextReleaseRequest_IEs[3], &_v295, (ObjectSetEntry *)&UEContextReleaseRequest_IEs[1]},
    {NULL, &_v296, (ObjectSetEntry *)&UEContextReleaseRequest_IEs[2]}
};
static ObjectSetEntry const UEContextReleaseCommand_IEs[] = {
    {(ObjectSetEntry *)&UEContextReleaseCommand_IEs[1], &_v297, NULL},
    {NULL, &_v298, (ObjectSetEntry *)&UEContextReleaseCommand_IEs[0]}
};
static ObjectSetEntry const UEContextReleaseComplete_IEs[] = {
    {(ObjectSetEntry *)&UEContextReleaseComplete_IEs[1], &_v299, NULL},
    {(ObjectSetEntry *)&UEContextReleaseComplete_IEs[2], &_v300, (ObjectSetEntry *)&UEContextReleaseComplete_IEs[0]},
    {(ObjectSetEntry *)&UEContextReleaseComplete_IEs[3], &_v301, (ObjectSetEntry *)&UEContextReleaseComplete_IEs[1]},
    {NULL, &_v302, (ObjectSetEntry *)&UEContextReleaseComplete_IEs[2]}
};
static ObjectSetEntry const UEContextModificationRequestIEs[] = {
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[1], &_v303, NULL},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[2], &_v304, (ObjectSetEntry *)&UEContextModificationRequestIEs[0]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[3], &_v305, (ObjectSetEntry *)&UEContextModificationRequestIEs[1]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[4], &_v306, (ObjectSetEntry *)&UEContextModificationRequestIEs[2]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[5], &_v307, (ObjectSetEntry *)&UEContextModificationRequestIEs[3]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[6], &_v308, (ObjectSetEntry *)&UEContextModificationRequestIEs[4]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[7], &_v309, (ObjectSetEntry *)&UEContextModificationRequestIEs[5]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[8], &_v310, (ObjectSetEntry *)&UEContextModificationRequestIEs[6]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[9], &_v311, (ObjectSetEntry *)&UEContextModificationRequestIEs[7]},
    {(ObjectSetEntry *)&UEContextModificationRequestIEs[10], &_v312, (ObjectSetEntry *)&UEContextModificationRequestIEs[8]},
    {NULL, &_v313, (ObjectSetEntry *)&UEContextModificationRequestIEs[9]}
};
static ObjectSetEntry const UEContextModificationResponseIEs[] = {
    {(ObjectSetEntry *)&UEContextModificationResponseIEs[1], &_v314, NULL},
    {(ObjectSetEntry *)&UEContextModificationResponseIEs[2], &_v315, (ObjectSetEntry *)&UEContextModificationResponseIEs[0]},
    {NULL, &_v316, (ObjectSetEntry *)&UEContextModificationResponseIEs[1]}
};
static ObjectSetEntry const UEContextModificationFailureIEs[] = {
    {(ObjectSetEntry *)&UEContextModificationFailureIEs[1], &_v317, NULL},
    {(ObjectSetEntry *)&UEContextModificationFailureIEs[2], &_v318, (ObjectSetEntry *)&UEContextModificationFailureIEs[0]},
    {(ObjectSetEntry *)&UEContextModificationFailureIEs[3], &_v319, (ObjectSetEntry *)&UEContextModificationFailureIEs[1]},
    {NULL, &_v320, (ObjectSetEntry *)&UEContextModificationFailureIEs[2]}
};
static ObjectSetEntry const UERadioCapabilityMatchRequestIEs[] = {
    {(ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[1], &_v321, NULL},
    {(ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[2], &_v322, (ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[0]},
    {NULL, &_v323, (ObjectSetEntry *)&UERadioCapabilityMatchRequestIEs[1]}
};
static ObjectSetEntry const UERadioCapabilityMatchResponseIEs[] = {
    {(ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[1], &_v324, NULL},
    {(ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[2], &_v325, (ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[0]},
    {(ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[3], &_v326, (ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[1]},
    {NULL, &_v327, (ObjectSetEntry *)&UERadioCapabilityMatchResponseIEs[2]}
};
static ObjectSetEntry const DownlinkNASTransport_IEs[] = {
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[1], &_v328, NULL},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[2], &_v329, (ObjectSetEntry *)&DownlinkNASTransport_IEs[0]},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[3], &_v330, (ObjectSetEntry *)&DownlinkNASTransport_IEs[1]},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[4], &_v331, (ObjectSetEntry *)&DownlinkNASTransport_IEs[2]},
    {(ObjectSetEntry *)&DownlinkNASTransport_IEs[5], &_v332, (ObjectSetEntry *)&DownlinkNASTransport_IEs[3]},
    {NULL, &_v333, (ObjectSetEntry *)&DownlinkNASTransport_IEs[4]}
};
static ObjectSetEntry const InitialUEMessage_IEs[] = {
    {(ObjectSetEntry *)&InitialUEMessage_IEs[1], &_v334, NULL},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[2], &_v335, (ObjectSetEntry *)&InitialUEMessage_IEs[0]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[3], &_v336, (ObjectSetEntry *)&InitialUEMessage_IEs[1]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[4], &_v337, (ObjectSetEntry *)&InitialUEMessage_IEs[2]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[5], &_v338, (ObjectSetEntry *)&InitialUEMessage_IEs[3]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[6], &_v339, (ObjectSetEntry *)&InitialUEMessage_IEs[4]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[7], &_v340, (ObjectSetEntry *)&InitialUEMessage_IEs[5]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[8], &_v341, (ObjectSetEntry *)&InitialUEMessage_IEs[6]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[9], &_v342, (ObjectSetEntry *)&InitialUEMessage_IEs[7]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[10], &_v343, (ObjectSetEntry *)&InitialUEMessage_IEs[8]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[11], &_v344, (ObjectSetEntry *)&InitialUEMessage_IEs[9]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[12], &_v345, (ObjectSetEntry *)&InitialUEMessage_IEs[10]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[13], &_v346, (ObjectSetEntry *)&InitialUEMessage_IEs[11]},
    {(ObjectSetEntry *)&InitialUEMessage_IEs[14], &_v347, (ObjectSetEntry *)&InitialUEMessage_IEs[12]},
    {NULL, &_v348, (ObjectSetEntry *)&InitialUEMessage_IEs[13]}
};
static ObjectSetEntry const UplinkNASTransport_IEs[] = {
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[1], &_v349, NULL},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[2], &_v350, (ObjectSetEntry *)&UplinkNASTransport_IEs[0]},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[3], &_v351, (ObjectSetEntry *)&UplinkNASTransport_IEs[1]},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[4], &_v352, (ObjectSetEntry *)&UplinkNASTransport_IEs[2]},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[5], &_v353, (ObjectSetEntry *)&UplinkNASTransport_IEs[3]},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[6], &_v354, (ObjectSetEntry *)&UplinkNASTransport_IEs[4]},
    {(ObjectSetEntry *)&UplinkNASTransport_IEs[7], &_v355, (ObjectSetEntry *)&UplinkNASTransport_IEs[5]},
    {NULL, &_v356, (ObjectSetEntry *)&UplinkNASTransport_IEs[6]}
};
static ObjectSetEntry const NASNonDeliveryIndication_IEs[] = {
    {(ObjectSetEntry *)&NASNonDeliveryIndication_IEs[1], &_v357, NULL},
    {(ObjectSetEntry *)&NASNonDeliveryIndication_IEs[2], &_v358, (ObjectSetEntry *)&NASNonDeliveryIndication_IEs[0]},
    {(ObjectSetEntry *)&NASNonDeliveryIndication_IEs[3], &_v359, (ObjectSetEntry *)&NASNonDeliveryIndication_IEs[1]},
    {NULL, &_v360, (ObjectSetEntry *)&NASNonDeliveryIndication_IEs[2]}
};
static ObjectSetEntry const ResetIEs[] = {
    {(ObjectSetEntry *)&ResetIEs[1], &_v361, NULL},
    {NULL, &_v362, (ObjectSetEntry *)&ResetIEs[0]}
};
static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemRes[] = {
    {NULL, &_v363, NULL}
};
static ObjectSetEntry const ResetAcknowledgeIEs[] = {
    {(ObjectSetEntry *)&ResetAcknowledgeIEs[1], &_v364, NULL},
    {NULL, &_v365, (ObjectSetEntry *)&ResetAcknowledgeIEs[0]}
};
static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemResAck[] = {
    {NULL, &_v366, NULL}
};
static ObjectSetEntry const ErrorIndicationIEs[] = {
    {(ObjectSetEntry *)&ErrorIndicationIEs[1], &_v367, NULL},
    {(ObjectSetEntry *)&ErrorIndicationIEs[2], &_v368, (ObjectSetEntry *)&ErrorIndicationIEs[0]},
    {(ObjectSetEntry *)&ErrorIndicationIEs[3], &_v369, (ObjectSetEntry *)&ErrorIndicationIEs[1]},
    {NULL, &_v370, (ObjectSetEntry *)&ErrorIndicationIEs[2]}
};
static ObjectSetEntry const S1SetupRequestIEs[] = {
    {(ObjectSetEntry *)&S1SetupRequestIEs[1], &_v371, NULL},
    {(ObjectSetEntry *)&S1SetupRequestIEs[2], &_v372, (ObjectSetEntry *)&S1SetupRequestIEs[0]},
    {(ObjectSetEntry *)&S1SetupRequestIEs[3], &_v373, (ObjectSetEntry *)&S1SetupRequestIEs[1]},
    {(ObjectSetEntry *)&S1SetupRequestIEs[4], &_v374, (ObjectSetEntry *)&S1SetupRequestIEs[2]},
    {NULL, &_v375, (ObjectSetEntry *)&S1SetupRequestIEs[3]}
};
static ObjectSetEntry const S1SetupResponseIEs[] = {
    {(ObjectSetEntry *)&S1SetupResponseIEs[1], &_v376, NULL},
    {(ObjectSetEntry *)&S1SetupResponseIEs[2], &_v377, (ObjectSetEntry *)&S1SetupResponseIEs[0]},
    {(ObjectSetEntry *)&S1SetupResponseIEs[3], &_v378, (ObjectSetEntry *)&S1SetupResponseIEs[1]},
    {(ObjectSetEntry *)&S1SetupResponseIEs[4], &_v379, (ObjectSetEntry *)&S1SetupResponseIEs[2]},
    {NULL, &_v380, (ObjectSetEntry *)&S1SetupResponseIEs[3]}
};
static ObjectSetEntry const S1SetupFailureIEs[] = {
    {(ObjectSetEntry *)&S1SetupFailureIEs[1], &_v381, NULL},
    {(ObjectSetEntry *)&S1SetupFailureIEs[2], &_v382, (ObjectSetEntry *)&S1SetupFailureIEs[0]},
    {NULL, &_v383, (ObjectSetEntry *)&S1SetupFailureIEs[1]}
};
static ObjectSetEntry const ENBConfigurationUpdateIEs[] = {
    {(ObjectSetEntry *)&ENBConfigurationUpdateIEs[1], &_v384, NULL},
    {(ObjectSetEntry *)&ENBConfigurationUpdateIEs[2], &_v385, (ObjectSetEntry *)&ENBConfigurationUpdateIEs[0]},
    {(ObjectSetEntry *)&ENBConfigurationUpdateIEs[3], &_v386, (ObjectSetEntry *)&ENBConfigurationUpdateIEs[1]},
    {NULL, &_v387, (ObjectSetEntry *)&ENBConfigurationUpdateIEs[2]}
};
static ObjectSetEntry const ENBConfigurationUpdateAcknowledgeIEs[] = {
    {NULL, &_v388, NULL}
};
static ObjectSetEntry const ENBConfigurationUpdateFailureIEs[] = {
    {(ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[1], &_v389, NULL},
    {(ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[2], &_v390, (ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[0]},
    {NULL, &_v391, (ObjectSetEntry *)&ENBConfigurationUpdateFailureIEs[1]}
};
static ObjectSetEntry const MMEConfigurationUpdateIEs[] = {
    {(ObjectSetEntry *)&MMEConfigurationUpdateIEs[1], &_v392, NULL},
    {(ObjectSetEntry *)&MMEConfigurationUpdateIEs[2], &_v393, (ObjectSetEntry *)&MMEConfigurationUpdateIEs[0]},
    {NULL, &_v394, (ObjectSetEntry *)&MMEConfigurationUpdateIEs[1]}
};
static ObjectSetEntry const MMEConfigurationUpdateAcknowledgeIEs[] = {
    {NULL, &_v395, NULL}
};
static ObjectSetEntry const MMEConfigurationUpdateFailureIEs[] = {
    {(ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[1], &_v396, NULL},
    {(ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[2], &_v397, (ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[0]},
    {NULL, &_v398, (ObjectSetEntry *)&MMEConfigurationUpdateFailureIEs[1]}
};
static ObjectSetEntry const DownlinkS1cdma2000tunnellingIEs[] = {
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[1], &_v399, NULL},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[2], &_v400, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[0]},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[3], &_v401, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[1]},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[4], &_v402, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[2]},
    {(ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[5], &_v403, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[3]},
    {NULL, &_v404, (ObjectSetEntry *)&DownlinkS1cdma2000tunnellingIEs[4]}
};
static ObjectSetEntry const UplinkS1cdma2000tunnellingIEs[] = {
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[1], &_v405, NULL},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[2], &_v406, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[0]},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[3], &_v407, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[1]},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[4], &_v408, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[2]},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[5], &_v409, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[3]},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[6], &_v410, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[4]},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[7], &_v411, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[5]},
    {(ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[8], &_v412, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[6]},
    {NULL, &_v413, (ObjectSetEntry *)&UplinkS1cdma2000tunnellingIEs[7]}
};
static ObjectSetEntry const UECapabilityInfoIndicationIEs[] = {
    {(ObjectSetEntry *)&UECapabilityInfoIndicationIEs[1], &_v414, NULL},
    {(ObjectSetEntry *)&UECapabilityInfoIndicationIEs[2], &_v415, (ObjectSetEntry *)&UECapabilityInfoIndicationIEs[0]},
    {(ObjectSetEntry *)&UECapabilityInfoIndicationIEs[3], &_v416, (ObjectSetEntry *)&UECapabilityInfoIndicationIEs[1]},
    {NULL, &_v417, (ObjectSetEntry *)&UECapabilityInfoIndicationIEs[2]}
};
static ObjectSetEntry const ENBStatusTransferIEs[] = {
    {(ObjectSetEntry *)&ENBStatusTransferIEs[1], &_v418, NULL},
    {(ObjectSetEntry *)&ENBStatusTransferIEs[2], &_v419, (ObjectSetEntry *)&ENBStatusTransferIEs[0]},
    {NULL, &_v420, (ObjectSetEntry *)&ENBStatusTransferIEs[1]}
};
static ObjectSetEntry const MMEStatusTransferIEs[] = {
    {(ObjectSetEntry *)&MMEStatusTransferIEs[1], &_v421, NULL},
    {(ObjectSetEntry *)&MMEStatusTransferIEs[2], &_v422, (ObjectSetEntry *)&MMEStatusTransferIEs[0]},
    {NULL, &_v423, (ObjectSetEntry *)&MMEStatusTransferIEs[1]}
};
static ObjectSetEntry const TraceStartIEs[] = {
    {(ObjectSetEntry *)&TraceStartIEs[1], &_v424, NULL},
    {(ObjectSetEntry *)&TraceStartIEs[2], &_v425, (ObjectSetEntry *)&TraceStartIEs[0]},
    {NULL, &_v426, (ObjectSetEntry *)&TraceStartIEs[1]}
};
static ObjectSetEntry const TraceFailureIndicationIEs[] = {
    {(ObjectSetEntry *)&TraceFailureIndicationIEs[1], &_v427, NULL},
    {(ObjectSetEntry *)&TraceFailureIndicationIEs[2], &_v428, (ObjectSetEntry *)&TraceFailureIndicationIEs[0]},
    {(ObjectSetEntry *)&TraceFailureIndicationIEs[3], &_v429, (ObjectSetEntry *)&TraceFailureIndicationIEs[1]},
    {NULL, &_v430, (ObjectSetEntry *)&TraceFailureIndicationIEs[2]}
};
static ObjectSetEntry const DeactivateTraceIEs[] = {
    {(ObjectSetEntry *)&DeactivateTraceIEs[1], &_v431, NULL},
    {(ObjectSetEntry *)&DeactivateTraceIEs[2], &_v432, (ObjectSetEntry *)&DeactivateTraceIEs[0]},
    {NULL, &_v433, (ObjectSetEntry *)&DeactivateTraceIEs[1]}
};
static ObjectSetEntry const CellTrafficTraceIEs[] = {
    {(ObjectSetEntry *)&CellTrafficTraceIEs[1], &_v434, NULL},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[2], &_v435, (ObjectSetEntry *)&CellTrafficTraceIEs[0]},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[3], &_v436, (ObjectSetEntry *)&CellTrafficTraceIEs[1]},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[4], &_v437, (ObjectSetEntry *)&CellTrafficTraceIEs[2]},
    {(ObjectSetEntry *)&CellTrafficTraceIEs[5], &_v438, (ObjectSetEntry *)&CellTrafficTraceIEs[3]},
    {NULL, &_v439, (ObjectSetEntry *)&CellTrafficTraceIEs[4]}
};
static ObjectSetEntry const LocationReportingControlIEs[] = {
    {(ObjectSetEntry *)&LocationReportingControlIEs[1], &_v440, NULL},
    {(ObjectSetEntry *)&LocationReportingControlIEs[2], &_v441, (ObjectSetEntry *)&LocationReportingControlIEs[0]},
    {NULL, &_v442, (ObjectSetEntry *)&LocationReportingControlIEs[1]}
};
static ObjectSetEntry const LocationReportingFailureIndicationIEs[] = {
    {(ObjectSetEntry *)&LocationReportingFailureIndicationIEs[1], &_v443, NULL},
    {(ObjectSetEntry *)&LocationReportingFailureIndicationIEs[2], &_v444, (ObjectSetEntry *)&LocationReportingFailureIndicationIEs[0]},
    {NULL, &_v445, (ObjectSetEntry *)&LocationReportingFailureIndicationIEs[1]}
};
static ObjectSetEntry const LocationReportIEs[] = {
    {(ObjectSetEntry *)&LocationReportIEs[1], &_v446, NULL},
    {(ObjectSetEntry *)&LocationReportIEs[2], &_v447, (ObjectSetEntry *)&LocationReportIEs[0]},
    {(ObjectSetEntry *)&LocationReportIEs[3], &_v448, (ObjectSetEntry *)&LocationReportIEs[1]},
    {(ObjectSetEntry *)&LocationReportIEs[4], &_v449, (ObjectSetEntry *)&LocationReportIEs[2]},
    {NULL, &_v450, (ObjectSetEntry *)&LocationReportIEs[3]}
};
static ObjectSetEntry const OverloadStartIEs[] = {
    {(ObjectSetEntry *)&OverloadStartIEs[1], &_v451, NULL},
    {(ObjectSetEntry *)&OverloadStartIEs[2], &_v452, (ObjectSetEntry *)&OverloadStartIEs[0]},
    {NULL, &_v453, (ObjectSetEntry *)&OverloadStartIEs[1]}
};
static ObjectSetEntry const OverloadStopIEs[] = {
    {NULL, &_v454, NULL}
};
static ObjectSetEntry const WriteReplaceWarningRequestIEs[] = {
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[1], &_v455, NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[2], &_v456, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[0]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[3], &_v457, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[1]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[4], &_v458, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[2]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[5], &_v459, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[3]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[6], &_v460, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[4]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[7], &_v461, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[5]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[8], &_v462, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[6]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[9], &_v463, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[7]},
    {(ObjectSetEntry *)&WriteReplaceWarningRequestIEs[10], &_v464, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[8]},
    {NULL, &_v465, (ObjectSetEntry *)&WriteReplaceWarningRequestIEs[9]}
};
static ObjectSetEntry const WriteReplaceWarningResponseIEs[] = {
    {(ObjectSetEntry *)&WriteReplaceWarningResponseIEs[1], &_v466, NULL},
    {(ObjectSetEntry *)&WriteReplaceWarningResponseIEs[2], &_v467, (ObjectSetEntry *)&WriteReplaceWarningResponseIEs[0]},
    {(ObjectSetEntry *)&WriteReplaceWarningResponseIEs[3], &_v468, (ObjectSetEntry *)&WriteReplaceWarningResponseIEs[1]},
    {NULL, &_v469, (ObjectSetEntry *)&WriteReplaceWarningResponseIEs[2]}
};
static ObjectSetEntry const ENBDirectInformationTransferIEs[] = {
    {NULL, &_v470, NULL}
};
static ObjectSetEntry const MMEDirectInformationTransferIEs[] = {
    {NULL, &_v471, NULL}
};
static ObjectSetEntry const ENBConfigurationTransferIEs[] = {
    {NULL, &_v472, NULL}
};
static ObjectSetEntry const MMEConfigurationTransferIEs[] = {
    {NULL, &_v473, NULL}
};
static ObjectSetEntry const PrivateMessageIEs[] = {
{0}};
static ObjectSetEntry const KillRequestIEs[] = {
    {(ObjectSetEntry *)&KillRequestIEs[1], &_v474, NULL},
    {(ObjectSetEntry *)&KillRequestIEs[2], &_v475, (ObjectSetEntry *)&KillRequestIEs[0]},
    {(ObjectSetEntry *)&KillRequestIEs[3], &_v476, (ObjectSetEntry *)&KillRequestIEs[1]},
    {NULL, &_v477, (ObjectSetEntry *)&KillRequestIEs[2]}
};
static ObjectSetEntry const KillResponseIEs[] = {
    {(ObjectSetEntry *)&KillResponseIEs[1], &_v478, NULL},
    {(ObjectSetEntry *)&KillResponseIEs[2], &_v479, (ObjectSetEntry *)&KillResponseIEs[0]},
    {(ObjectSetEntry *)&KillResponseIEs[3], &_v480, (ObjectSetEntry *)&KillResponseIEs[1]},
    {NULL, &_v481, (ObjectSetEntry *)&KillResponseIEs[2]}
};
static ObjectSetEntry const PWSRestartIndicationIEs[] = {
    {(ObjectSetEntry *)&PWSRestartIndicationIEs[1], &_v482, NULL},
    {(ObjectSetEntry *)&PWSRestartIndicationIEs[2], &_v483, (ObjectSetEntry *)&PWSRestartIndicationIEs[0]},
    {(ObjectSetEntry *)&PWSRestartIndicationIEs[3], &_v484, (ObjectSetEntry *)&PWSRestartIndicationIEs[1]},
    {NULL, &_v485, (ObjectSetEntry *)&PWSRestartIndicationIEs[2]}
};
static ObjectSetEntry const DownlinkUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[1], &_v486, NULL},
    {(ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[2], &_v487, (ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[0]},
    {(ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[3], &_v488, (ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[1]},
    {NULL, &_v489, (ObjectSetEntry *)&DownlinkUEAssociatedLPPaTransport_IEs[2]}
};
static ObjectSetEntry const UplinkUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[1], &_v490, NULL},
    {(ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[2], &_v491, (ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[0]},
    {(ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[3], &_v492, (ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[1]},
    {NULL, &_v493, (ObjectSetEntry *)&UplinkUEAssociatedLPPaTransport_IEs[2]}
};
static ObjectSetEntry const DownlinkNonUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&DownlinkNonUEAssociatedLPPaTransport_IEs[1], &_v494, NULL},
    {NULL, &_v495, (ObjectSetEntry *)&DownlinkNonUEAssociatedLPPaTransport_IEs[0]}
};
static ObjectSetEntry const UplinkNonUEAssociatedLPPaTransport_IEs[] = {
    {(ObjectSetEntry *)&UplinkNonUEAssociatedLPPaTransport_IEs[1], &_v496, NULL},
    {NULL, &_v497, (ObjectSetEntry *)&UplinkNonUEAssociatedLPPaTransport_IEs[0]}
};
static ObjectSetEntry const E_RABModificationIndicationIEs[] = {
    {(ObjectSetEntry *)&E_RABModificationIndicationIEs[1], &_v498, NULL},
    {(ObjectSetEntry *)&E_RABModificationIndicationIEs[2], &_v499, (ObjectSetEntry *)&E_RABModificationIndicationIEs[0]},
    {(ObjectSetEntry *)&E_RABModificationIndicationIEs[3], &_v500, (ObjectSetEntry *)&E_RABModificationIndicationIEs[1]},
    {NULL, &_v501, (ObjectSetEntry *)&E_RABModificationIndicationIEs[2]}
};
static ObjectSetEntry const E_RABToBeModifiedItemBearerModIndIEs[] = {
    {NULL, &_v502, NULL}
};
static ObjectSetEntry const E_RABToBeModifiedItemBearerModInd_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABNotToBeModifiedItemBearerModIndIEs[] = {
    {NULL, &_v503, NULL}
};
static ObjectSetEntry const E_RABNotToBeModifiedItemBearerModInd_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABModificationConfirmIEs[] = {
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[1], &_v504, NULL},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[2], &_v505, (ObjectSetEntry *)&E_RABModificationConfirmIEs[0]},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[3], &_v506, (ObjectSetEntry *)&E_RABModificationConfirmIEs[1]},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[4], &_v507, (ObjectSetEntry *)&E_RABModificationConfirmIEs[2]},
    {(ObjectSetEntry *)&E_RABModificationConfirmIEs[5], &_v508, (ObjectSetEntry *)&E_RABModificationConfirmIEs[3]},
    {NULL, &_v509, (ObjectSetEntry *)&E_RABModificationConfirmIEs[4]}
};
static ObjectSetEntry const E_RABModifyItemBearerModConfIEs[] = {
    {NULL, &_v510, NULL}
};
static ObjectSetEntry const E_RABModifyItemBearerModConfExtIEs[] = {
{0}};
static ObjectSetEntry const AllocationAndRetentionPriority_ExtIEs[] = {
{0}};
static ObjectSetEntry const Bearers_SubjectToStatusTransfer_ItemIEs[] = {
    {NULL, &_v511, NULL}
};
static ObjectSetEntry const Bearers_SubjectToStatusTransfer_ItemExtIEs[] = {
    {(ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[1], &_v512, NULL},
    {(ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[2], &_v513, (ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[0]},
    {NULL, &_v514, (ObjectSetEntry *)&Bearers_SubjectToStatusTransfer_ItemExtIEs[1]}
};
static ObjectSetEntry const CancelledCellinEAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CancelledCellinTAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellID_Broadcast_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellID_Cancelled_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const Cdma2000OneXSRVCCInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const CellType_ExtIEs[] = {
{0}};
static ObjectSetEntry const CGI_ExtIEs[] = {
{0}};
static ObjectSetEntry const CSG_IdList_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const COUNTvalue_ExtIEs[] = {
{0}};
static ObjectSetEntry const COUNTValueExtended_ExtIEs[] = {
{0}};
static ObjectSetEntry const CriticalityDiagnostics_ExtIEs[] = {
{0}};
static ObjectSetEntry const CriticalityDiagnostics_IE_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const EmergencyAreaID_Broadcast_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const EmergencyAreaID_Cancelled_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const CompletedCellinEAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const GERAN_Cell_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const GlobalENB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const ENB_StatusTransfer_TransparentContainer_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABInformationListIEs[] = {
    {NULL, &_v515, NULL}
};
static ObjectSetEntry const E_RABInformationListItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABItemIEs[] = {
    {NULL, &_v516, NULL}
};
static ObjectSetEntry const E_RABItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const E_RABQoSParameters_ExtIEs[] = {
{0}};
static ObjectSetEntry const EUTRAN_CGI_ExtIEs[] = {
{0}};
static ObjectSetEntry const ExpectedUEBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const ExpectedUEActivityBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const ForbiddenTAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const ForbiddenLAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const GBR_QosInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const GUMMEI_ExtIEs[] = {
{0}};
static ObjectSetEntry const HandoverRestrictionList_ExtIEs[] = {
{0}};
static ObjectSetEntry const ImmediateMDT_ExtIEs[] = {
    {(ObjectSetEntry *)&ImmediateMDT_ExtIEs[1], &_v517, NULL},
    {(ObjectSetEntry *)&ImmediateMDT_ExtIEs[2], &_v518, (ObjectSetEntry *)&ImmediateMDT_ExtIEs[0]},
    {(ObjectSetEntry *)&ImmediateMDT_ExtIEs[3], &_v519, (ObjectSetEntry *)&ImmediateMDT_ExtIEs[1]},
    {NULL, &_v520, (ObjectSetEntry *)&ImmediateMDT_ExtIEs[2]}
};
static ObjectSetEntry const LAI_ExtIEs[] = {
{0}};
static ObjectSetEntry const LastVisitedEUTRANCellInformation_ExtIEs[] = {
    {(ObjectSetEntry *)&LastVisitedEUTRANCellInformation_ExtIEs[1], &_v521, NULL},
    {NULL, &_v522, (ObjectSetEntry *)&LastVisitedEUTRANCellInformation_ExtIEs[0]}
};
static ObjectSetEntry const ListeningSubframePattern_ExtIEs[] = {
{0}};
static ObjectSetEntry const LoggedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const LoggedMBSFNMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const M3Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const M4Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const M5Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const MDT_Configuration_ExtIEs[] = {
    {NULL, &_v523, NULL}
};
static ObjectSetEntry const MBSFN_ResultToLogInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const MutingPatternInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const M1PeriodicReporting_ExtIEs[] = {
{0}};
static ObjectSetEntry const ProSeAuthorized_ExtIEs[] = {
{0}};
static ObjectSetEntry const RequestType_ExtIEs[] = {
{0}};
static ObjectSetEntry const RIMTransfer_ExtIEs[] = {
{0}};
static ObjectSetEntry const RLFReportInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const SecurityContext_ExtIEs[] = {
{0}};
static ObjectSetEntry const SONInformationReply_ExtIEs[] = {
    {(ObjectSetEntry *)&SONInformationReply_ExtIEs[1], &_v526, NULL},
    {NULL, &_v527, (ObjectSetEntry *)&SONInformationReply_ExtIEs[0]}
};
static ObjectSetEntry const SONConfigurationTransfer_ExtIEs[] = {
    {(ObjectSetEntry *)&SONConfigurationTransfer_ExtIEs[1], &_v528, NULL},
    {NULL, &_v529, (ObjectSetEntry *)&SONConfigurationTransfer_ExtIEs[0]}
};
static ObjectSetEntry const SynchronisationInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const SourceeNB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[] = {
    {(ObjectSetEntry *)&SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[1], &_v530, NULL},
    {NULL, &_v531, (ObjectSetEntry *)&SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs[0]}
};
static ObjectSetEntry const ServedGUMMEIsItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const SupportedTAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TimeSynchronisationInfo_ExtIEs[] = {
    {NULL, &_v532, NULL}
};
static ObjectSetEntry const S_TMSI_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAIBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAI_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAI_Broadcast_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TAI_Cancelled_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TABasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const CompletedCellinTAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const TargeteNB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const TargetRNC_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const TargeteNB_ToSourceeNB_TransparentContainer_ExtIEs[] = {
{0}};
static ObjectSetEntry const M1ThresholdEventA2_ExtIEs[] = {
{0}};
static ObjectSetEntry const TraceActivation_ExtIEs[] = {
    {NULL, &_v533, NULL}
};
static ObjectSetEntry const Tunnel_Information_ExtIEs[] = {
{0}};
static ObjectSetEntry const UEAggregate_MaximumBitrates_ExtIEs[] = {
{0}};
static ObjectSetEntry const UE_S1AP_ID_pair_ExtIEs[] = {
{0}};
static ObjectSetEntry const UE_associatedLogicalS1_ConnectionItemExtIEs[] = {
{0}};
static ObjectSetEntry const UESecurityCapabilities_ExtIEs[] = {
{0}};
static ObjectSetEntry const UserLocationInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2TNLConfigurationInfo_ExtIEs[] = {
    {(ObjectSetEntry *)&X2TNLConfigurationInfo_ExtIEs[1], &_v534, NULL},
    {NULL, &_v535, (ObjectSetEntry *)&X2TNLConfigurationInfo_ExtIEs[0]}
};
static ObjectSetEntry const ENBX2ExtTLA_ExtIEs[] = {
{0}};
static ObjectSetEntry const MDTMode_ExtensionIE[] = {
    {NULL, &_v524, NULL}
};
static ObjectSetEntry const SONInformation_ExtensionIE[] = {
    {NULL, &_v525, NULL}
};
#endif /* (OSS_SPARTAN_AWARE + 0) > 12 */

static unsigned short const _oss_OSet_info[] =  {
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
    4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0
};
#endif /* !OSS_SPARTAN_AWARE || OSS_SPARTAN_AWARE < 2 */

#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) >= 3)
extern void DLL_ENTRY _oss_run_SOED_on_INTEL_X86_WINDOWS_AND_WINNT(void);
static void _oss_post_init(struct ossGlobal *world);
#endif /* OSS_SPARTAN_AWARE >= 3 */
#endif /* OSS_SPARTAN_AWARE */

#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) >= 12)
static unsigned char _privateFlags[] = {0x02,0x00};
#endif /* OSS_SPARTAN_AWARE >= 12 */
#endif /* OSS_SPARTAN_AWARE */

void DLL_ENTRY_FDEF _ossinit_s1ap(struct ossGlobal *world) {
#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) >= 3)
    _oss_run_SOED_on_INTEL_X86_WINDOWS_AND_WINNT();
#endif /* OSS_SPARTAN_AWARE >= 3 */
#endif /* OSS_SPARTAN_AWARE */
#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) >= 12)
    ossPrivateSetMoreFlags(world, 12, _privateFlags);
#endif /* OSS_SPARTAN_AWARE >= 12 */
#endif /* OSS_SPARTAN_AWARE */
    ossLinkPer(world);
#if !defined(OSS_SPARTAN_AWARE) || ((OSS_SPARTAN_AWARE + 0) < 8)
    ossLinkConstraint(world);
#else  /* OSS_SPARTAN_AWARE < 8 */
    ossLinkConstraintSpartanAware8(world);
#endif /* OSS_SPARTAN_AWARE < 8 */
#ifdef OSS_SPARTAN_AWARE
    ossLinkCmpValue(world);
#if ((OSS_SPARTAN_AWARE + 0) >= 2)
    ossInitObjectSetInfo(world, (unsigned short *)_oss_OSet_info);
#endif /* OSS_SPARTAN_AWARE >= 2 */
#endif /* OSS_SPARTAN_AWARE */

#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) >= 3)
    _oss_post_init(world);
#endif /* OSS_SPARTAN_AWARE >= 3 */
#endif /* OSS_SPARTAN_AWARE */
}

static unsigned short const _v609 = 4;
static unsigned short const _v611 = 27;
static unsigned short const _v615 = 8;
static unsigned short const _v631 = 64;
static unsigned short const _v635 = 8;
static unsigned short const _v639 = 16;
static unsigned short const _v641 = 32;
static unsigned short const _v659 = 256;
static unsigned short const _v661 = 16;
static unsigned short const _v673 = 8;
static unsigned short const _v679 = 10;
static unsigned short const _v681 = 2;
static unsigned short const _v683 = 50;
static unsigned short const _v707 = 4;
static unsigned short const _v888 = 3;
static unsigned short const _v891 = 2;
static unsigned short const _v1029 = 2;
static unsigned short const _v1036 = 1;
static unsigned short const _v1038 = 2;
static unsigned short const _v1055 = 16;
static unsigned short const _v1146 = 4096;
static unsigned short const _v1167 = 28;
static unsigned short const _v1239 = 3;
static unsigned short const _v1262 = 20;
static unsigned short const _v1264 = 28;
static unsigned short const _v1270 = 2;
static unsigned short const _v1272 = 1;
static unsigned short const _v1286 = 16;
static unsigned short const _v1339 = 8;
static unsigned short const _v1359 = 16;
static unsigned short const _v1361 = 8;
static unsigned short const _v1402 = 4;
static unsigned short const _v1409 = 2;

static const char _v1574[] = " '()+,-./0123456789:=?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkl"
    "mnopqrstuvwxyz";
static const unsigned char _v1575[129] = {
    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
    0xff, 0xff, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x01,
    0x02, 0x03, 0xff, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a,
    0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0xff,
    0xff, 0x14, 0xff, 0x15, 0xff, 0x16, 0x17, 0x18, 0x19, 0x1a,
    0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24,
    0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e,
    0x2f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x30, 0x31, 0x32,
    0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c,
    0x3d, 0x3e, 0x3f, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
    0x47, 0x48, 0x49, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
};
static unsigned int const _v1613[36] = {
  0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 
  20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35
};
static unsigned int const _v1669[4] = {
  1, 2, 3, INT_MAX
};
static unsigned int const _v1614[4] = {
  36, 37, 38, INT_MAX
};
static unsigned short const _v1610[4] = {
  0, 1, 14, 15
};
static unsigned int const _v1661[3] = {3, 4, INT_MAX};
static unsigned int const _v1639[3] = {4, 5, INT_MAX};
static unsigned int const _v1657[2] = {3, INT_MAX};
static unsigned int const _v1641[2] = {2, INT_MAX};
static unsigned int const _v1618[2] = {4, INT_MAX};
static unsigned int const _v1600[2] = {5, INT_MAX};
static unsigned int const _v1566[2] = {1, INT_MAX};
static unsigned short const _v1554[2] = {1, 16};
static unsigned short const _v1553[2] = {1, USHRT_MAX};
static unsigned short const _v1551[2] = {0, USHRT_MAX};
static unsigned short const _v1548[2] = {1, 160};
static unsigned short const _v1538[2] = {16, 16};
static unsigned short const _v1536[2] = {3, 8};
static unsigned int const _v1531[2] = {0, 16777215};
static unsigned int const _v1530[2] = {0, UINT_MAX};
static ULONG_LONG const _v1520[2] = {0ui64, 10000000000ui64};
static unsigned short const _v1513[2] = {2, 2};
static unsigned short const _v1507[2] = {8, 8};
static unsigned short const _v1505[2] = {4, 4};
static unsigned short const _v1496[2] = {1, 8};
static unsigned short const _v1477[2] = {1, 1};
static unsigned int const _v1473[2] = {0, 3};
static unsigned short const _v1469[2] = {1, 6};
static unsigned short const _v1467[2] = {1, 32};
static unsigned short const _v1466[2] = {1, 256};
static unsigned short const _v1425[2] = {1, 2};
static unsigned short const _v1421[2] = {256, 256};
static unsigned short const _v1420[2] = {0, 7};
static unsigned int const _v1404[2] = {0, 10239};
static unsigned int const _v1396[2] = {0, 262143};
static unsigned short const _v1395[2] = {0, 255};
static unsigned short const _v1364[2] = {0, 4095};
static unsigned short const _v1345[2] = {0, 34};
static unsigned short const _v1344[2] = {0, 97};
static unsigned short const _v1332[2] = {1, 15};
static unsigned short const _v1331[2] = {3, 3};
static unsigned short const _v1330[2] = {1, 4096};
static unsigned int const _v1306[2] = {1, 181};
static unsigned int const _v1304[2] = {1, 30};
static unsigned int const _v1297[2] = {0, 15};
static unsigned short const _v1263[2] = {28, 28};
static unsigned short const _v1261[2] = {20, 20};
static unsigned int const _v1219[2] = {0, 131071};
static unsigned short const _v1218[2] = {0, SHRT_MAX};
static unsigned short const _v1210[2] = {27, 27};
static unsigned short const _v1148[2] = {4096, 4096};
static unsigned int const _v1141[2] = {0, 1048575};
static unsigned short const _v1050[2] = {4096, USHRT_MAX};
static unsigned short const _v729[2] = {0, 15};
static unsigned short const _v685[2] = {1, 9600};
static unsigned short const _v682[2] = {50, 50};
static unsigned short const _v678[2] = {10, 10};
static unsigned short const _v677[2] = {1, 2048};
static unsigned short const _v675[2] = {1, 99};
static unsigned short const _v669[2] = {0, 40950U};
static unsigned short const _v649[2] = {1, 16384};
static unsigned short const _v643[2] = {1, 150};
static unsigned short const _v640[2] = {32, 32};
static unsigned short const _v633[2] = {32, 256};
static unsigned short const _v630[2] = {64, 64};
static unsigned int const _v629[2] = {4096, 131071};
static unsigned short const _v627[2] = {0, 2047};
static unsigned int const _v1673[1] = {INT_MAX};

static const unsigned short _pduarray[] = {
    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
    11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
    21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
    31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
    41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
    51, 52, 53, 54, 55, 56, 57, 58, 59, 60,
    61, 62, 63, 64, 65, 66, 67, 68, 69, 70,
    71, 72, 73, 74, 75, 76, 77, 78, 79, 80,
    81, 82, 83, 84, 85, 86, 87, 88, 89, 90,
    91, 92, 93, 94, 95, 96, 97, 98, 99, 100,
    101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
    111, 112, 113, 114, 115, 116, 117, 118, 119, 120,
    121, 122, 123, 124, 125, 126, 127, 128, 129, 130,
    131, 132, 133, 134, 135, 136, 137, 138, 139, 140,
    141, 142, 143, 144, 145, 146, 147, 148, 149, 150,
    151, 152, 153, 154, 155, 156, 157, 158, 159, 160,
    161, 162, 163, 164, 165, 166, 167, 168, 169, 170,
    171, 172, 173, 174, 175, 176, 177, 178, 179, 180,
    181, 182, 183, 184, 185, 186, 187, 188, 189, 190,
    191, 192, 193, 194, 195, 196, 197, 198, 199, 200,
    201, 202, 203, 204, 205, 206, 207, 208, 209, 210,
    211, 212, 213, 214, 215, 216, 217, 218, 219, 220,
    221, 222, 223, 224, 225, 226, 227, 228, 229, 230,
    231, 232, 233, 234, 235, 236, 237, 238, 239, 240,
    241, 242, 243, 244, 245, 246, 247, 248, 249, 250,
    0
};

static const struct etype _etypearray[] = {
    {-1, 0, 0, "ObjectID", 8, 2, 2, 4, 40, 0, 63, 0},
    {-1, 2, 3, "S1AP-PDU", 28, 3, 2, 4, 12, 0, 13, 0},
    {-1, 12, 14, "HandoverRequired", 4, 1, 0, 0, 12, 3, 12, 0},
    {-1, 12, 14, "HandoverCommand", 4, 1, 0, 0, 12, 4, 12, 0},
    {256, 12, 0, "E-RABSubjecttoDataForwardingList", 4, 24, 2, 4, 216, 284, 18, 0},
    {-1, 12, 18, "E-RABDataForwardingItem", 44, 6, 1, 0, 12, 5, 12, 0},
    {-1, 12, 14, "HandoverPreparationFailure", 4, 1, 0, 0, 12, 11, 12, 0},
    {-1, 12, 14, "HandoverRequest", 4, 1, 0, 0, 12, 12, 12, 0},
    {256, 12, 0, "E-RABToBeSetupListHOReq", 4, 24, 2, 4, 216, 311, 18, 3},
    {-1, 12, 62, "E-RABToBeSetupItemHOReq", 92, 5, 1, 0, 12, 13, 12, 0},
    {-1, 12, 14, "HandoverRequestAcknowledge", 4, 1, 0, 0, 12, 18, 12, 0},
    {256, 12, 0, "E-RABAdmittedList", 4, 24, 2, 4, 216, 362, 18, 6},
    {-1, 12, 82, "E-RABAdmittedItem", 60, 8, 1, 0, 12, 19, 12, 0},
    {256, 12, 0, "E-RABFailedtoSetupListHOReqAck", 4, 24, 2, 4, 216, 378, 18, 9},
    {-1, 12, 134, "E-RABFailedToSetupItemHOReqAck", 20, 3, 1, 0, 12, 27, 12, 0},
    {-1, 12, 14, "HandoverFailure", 4, 1, 0, 0, 12, 30, 12, 0},
    {-1, 12, 14, "HandoverNotify", 4, 1, 0, 0, 12, 31, 12, 0},
    {-1, 12, 14, "PathSwitchRequest", 4, 1, 0, 0, 12, 32, 12, 0},
    {256, 12, 0, "E-RABToBeSwitchedDLList", 4, 24, 2, 4, 216, 414, 18, 12},
    {-1, 12, 146, "E-RABToBeSwitchedDLItem", 28, 4, 1, 0, 12, 33, 12, 0},
    {-1, 12, 14, "PathSwitchRequestAcknowledge", 4, 1, 0, 0, 12, 37, 12, 0},
    {256, 12, 0, "E-RABToBeSwitchedULList", 4, 24, 2, 4, 216, 431, 18, 15},
    {-1, 12, 146, "E-RABToBeSwitchedULItem", 28, 4, 1, 0, 12, 38, 12, 0},
    {-1, 12, 14, "PathSwitchRequestFailure", 4, 1, 0, 0, 12, 42, 12, 0},
    {-1, 12, 14, "HandoverCancel", 4, 1, 0, 0, 12, 43, 12, 0},
    {-1, 12, 14, "HandoverCancelAcknowledge", 4, 1, 0, 0, 12, 44, 12, 0},
    {-1, 12, 14, "E-RABSetupRequest", 4, 1, 0, 0, 12, 45, 12, 0},
    {256, 12, 0, "E-RABToBeSetupListBearerSUReq", 4, 24, 2, 4, 216, 463, 18, 18},
    {-1, 12, 162, "E-RABToBeSetupItemBearerSUReq", 100, 6, 1, 0, 12, 46, 12, 0},
    {-1, 12, 14, "E-RABSetupResponse", 4, 1, 0, 0, 12, 52, 12, 0},
    {256, 12, 0, "E-RABSetupListBearerSURes", 4, 24, 2, 4, 216, 482, 18, 21},
    {-1, 12, 146, "E-RABSetupItemBearerSURes", 28, 4, 1, 0, 12, 53, 12, 0},
    {-1, 12, 14, "E-RABModifyRequest", 4, 1, 0, 0, 12, 57, 12, 0},
    {256, 12, 0, "E-RABToBeModifiedListBearerModReq", 4, 24, 2, 4, 216, 499, 18, 24},
    {-1, 12, 146, "E-RABToBeModifiedItemBearerModReq", 84, 4, 1, 0, 12, 58, 12, 0},
    {-1, 12, 14, "E-RABModifyResponse", 4, 1, 0, 0, 12, 62, 12, 0},
    {256, 12, 0, "E-RABModifyListBearerModRes", 4, 24, 2, 4, 216, 516, 18, 27},
    {-1, 12, 186, "E-RABModifyItemBearerModRes", 12, 2, 1, 0, 12, 63, 12, 0},
    {-1, 12, 14, "E-RABReleaseCommand", 4, 1, 0, 0, 12, 65, 12, 0},
    {-1, 12, 14, "E-RABReleaseResponse", 4, 1, 0, 0, 12, 66, 12, 0},
    {256, 12, 0, "E-RABReleaseListBearerRelComp", 4, 24, 2, 4, 216, 536, 18, 30},
    {-1, 12, 186, "E-RABReleaseItemBearerRelComp", 12, 2, 1, 0, 12, 67, 12, 0},
    {-1, 12, 14, "E-RABReleaseIndication", 4, 1, 0, 0, 12, 69, 12, 0},
    {-1, 12, 14, "InitialContextSetupRequest", 4, 1, 0, 0, 12, 70, 12, 0},
    {256, 12, 0, "E-RABToBeSetupListCtxtSUReq", 4, 24, 2, 4, 216, 556, 18, 33},
    {-1, 12, 194, "E-RABToBeSetupItemCtxtSUReq", 100, 6, 1, 0, 12, 71, 12, 0},
    {-1, 12, 14, "InitialContextSetupResponse", 4, 1, 0, 0, 12, 77, 12, 0},
    {256, 12, 0, "E-RABSetupListCtxtSURes", 4, 24, 2, 4, 216, 575, 18, 36},
    {-1, 12, 146, "E-RABSetupItemCtxtSURes", 28, 4, 1, 0, 12, 78, 12, 0},
    {-1, 12, 14, "InitialContextSetupFailure", 4, 1, 0, 0, 12, 82, 12, 0},
    {-1, 12, 14, "Paging", 4, 1, 0, 0, 12, 83, 12, 0},
    {256, 12, 0, "TAIList", 4, 24, 2, 4, 216, 597, 18, 39},
    {-1, 12, 186, "TAIItem", 24, 2, 1, 0, 12, 84, 12, 0},
    {-1, 12, 14, "UEContextReleaseRequest", 4, 1, 0, 0, 12, 86, 12, 0},
    {-1, 12, 14, "UEContextReleaseCommand", 4, 1, 0, 0, 12, 87, 12, 0},
    {-1, 12, 14, "UEContextReleaseComplete", 4, 1, 0, 0, 12, 88, 12, 0},
    {-1, 12, 14, "UEContextModificationRequest", 4, 1, 0, 0, 12, 89, 12, 0},
    {-1, 12, 14, "UEContextModificationResponse", 4, 1, 0, 0, 12, 90, 12, 0},
    {-1, 12, 14, "UEContextModificationFailure", 4, 1, 0, 0, 12, 91, 12, 0},
    {-1, 12, 14, "UERadioCapabilityMatchRequest", 4, 1, 0, 0, 12, 92, 12, 0},
    {-1, 12, 14, "UERadioCapabilityMatchResponse", 4, 1, 0, 0, 12, 93, 12, 0},
    {-1, 12, 14, "DownlinkNASTransport", 4, 1, 0, 0, 12, 94, 12, 0},
    {-1, 12, 14, "InitialUEMessage", 4, 1, 0, 0, 12, 95, 12, 0},
    {-1, 12, 14, "UplinkNASTransport", 4, 1, 0, 0, 12, 96, 12, 0},
    {-1, 12, 14, "NASNonDeliveryIndication", 4, 1, 0, 0, 12, 97, 12, 0},
    {-1, 12, 14, "Reset", 4, 1, 0, 0, 12, 98, 12, 0},
    {-1, 2, 220, "ResetType", 8, 2, 2, 4, 12, 99, 13, 0},
    {-1, 12, 14, "ResetAcknowledge", 4, 1, 0, 0, 12, 101, 12, 0},
    {256, 12, 0, "UE-associatedLogicalS1-ConnectionListResAck", 4, 24, 2, 4, 216, 695, 18, 42},
    {-1, 12, 14, "ErrorIndication", 4, 1, 0, 0, 12, 102, 12, 0},
    {-1, 12, 14, "S1SetupRequest", 4, 1, 0, 0, 12, 103, 12, 0},
    {-1, 12, 14, "S1SetupResponse", 4, 1, 0, 0, 12, 104, 12, 0},
    {-1, 12, 14, "S1SetupFailure", 4, 1, 0, 0, 12, 105, 12, 0},
    {-1, 12, 14, "ENBConfigurationUpdate", 4, 1, 0, 0, 12, 106, 12, 0},
    {-1, 12, 14, "ENBConfigurationUpdateAcknowledge", 4, 1, 0, 0, 12, 107, 12, 0},
    {-1, 12, 14, "ENBConfigurationUpdateFailure", 4, 1, 0, 0, 12, 108, 12, 0},
    {-1, 12, 14, "MMEConfigurationUpdate", 4, 1, 0, 0, 12, 109, 12, 0},
    {-1, 12, 14, "MMEConfigurationUpdateAcknowledge", 4, 1, 0, 0, 12, 110, 12, 0},
    {-1, 12, 14, "MMEConfigurationUpdateFailure", 4, 1, 0, 0, 12, 111, 12, 0},
    {-1, 12, 14, "DownlinkS1cdma2000tunnelling", 4, 1, 0, 0, 12, 112, 12, 0},
    {-1, 12, 14, "UplinkS1cdma2000tunnelling", 4, 1, 0, 0, 12, 113, 12, 0},
    {-1, 12, 14, "UECapabilityInfoIndication", 4, 1, 0, 0, 12, 114, 12, 0},
    {-1, 12, 14, "ENBStatusTransfer", 4, 1, 0, 0, 12, 115, 12, 0},
    {-1, 12, 14, "MMEStatusTransfer", 4, 1, 0, 0, 12, 116, 12, 0},
    {-1, 12, 14, "TraceStart", 4, 1, 0, 0, 12, 117, 12, 0},
    {-1, 12, 14, "TraceFailureIndication", 4, 1, 0, 0, 12, 118, 12, 0},
    {-1, 12, 14, "DeactivateTrace", 4, 1, 0, 0, 12, 119, 12, 0},
    {-1, 12, 14, "CellTrafficTrace", 4, 1, 0, 0, 12, 120, 12, 0},
    {-1, 12, 14, "LocationReportingControl", 4, 1, 0, 0, 12, 121, 12, 0},
    {-1, 12, 14, "LocationReportingFailureIndication", 4, 1, 0, 0, 12, 122, 12, 0},
    {-1, 12, 14, "LocationReport", 4, 1, 0, 0, 12, 123, 12, 0},
    {-1, 12, 14, "OverloadStart", 4, 1, 0, 0, 12, 124, 12, 0},
    {-1, 12, 14, "OverloadStop", 4, 1, 0, 0, 12, 125, 12, 0},
    {-1, 12, 14, "WriteReplaceWarningRequest", 4, 1, 0, 0, 12, 126, 12, 0},
    {-1, 12, 14, "WriteReplaceWarningResponse", 4, 1, 0, 0, 12, 127, 12, 0},
    {-1, 12, 14, "ENBDirectInformationTransfer", 4, 1, 0, 0, 12, 128, 12, 0},
    {-1, 2, 227, "Inter-SystemInformationTransferType", 56, 1, 2, 4, 12, 129, 13, 0},
    {-1, 12, 14, "MMEDirectInformationTransfer", 4, 1, 0, 0, 12, 130, 12, 0},
    {-1, 12, 14, "ENBConfigurationTransfer", 4, 1, 0, 0, 12, 131, 12, 0},
    {-1, 12, 14, "MMEConfigurationTransfer", 4, 1, 0, 0, 12, 132, 12, 0},
    {-1, 12, 14, "PrivateMessage", 4, 1, 0, 0, 12, 133, 12, 0},
    {-1, 12, 14, "KillRequest", 4, 1, 0, 0, 12, 134, 12, 0},
    {-1, 12, 14, "KillResponse", 4, 1, 0, 0, 12, 135, 12, 0},
    {-1, 12, 14, "PWSRestartIndication", 4, 1, 0, 0, 12, 136, 12, 0},
    {-1, 12, 14, "DownlinkUEAssociatedLPPaTransport", 4, 1, 0, 0, 12, 137, 12, 0},
    {-1, 12, 14, "UplinkUEAssociatedLPPaTransport", 4, 1, 0, 0, 12, 138, 12, 0},
    {-1, 12, 14, "DownlinkNonUEAssociatedLPPaTransport", 4, 1, 0, 0, 12, 139, 12, 0},
    {-1, 12, 14, "UplinkNonUEAssociatedLPPaTransport", 4, 1, 0, 0, 12, 140, 12, 0},
    {-1, 12, 14, "E-RABModificationIndication", 4, 1, 0, 0, 12, 141, 12, 0},
    {256, 12, 0, "E-RABToBeModifiedListBearerModInd", 4, 24, 2, 4, 216, 942, 18, 45},
    {-1, 12, 146, "E-RABToBeModifiedItemBearerModInd", 28, 4, 1, 0, 12, 142, 12, 0},
    {256, 12, 0, "E-RABNotToBeModifiedListBearerModInd", 4, 24, 2, 4, 216, 954, 18, 48},
    {-1, 12, 146, "E-RABNotToBeModifiedItemBearerModInd", 28, 4, 1, 0, 12, 146, 12, 0},
    {-1, 12, 14, "E-RABModificationConfirm", 4, 1, 0, 0, 12, 150, 12, 0},
    {256, 12, 0, "E-RABModifyListBearerModConf", 4, 24, 2, 4, 216, 971, 18, 51},
    {-1, 12, 186, "E-RABModifyItemBearerModConf", 12, 2, 1, 0, 12, 151, 12, 0},
    {-1, 12, 232, "Bearers-SubjectToStatusTransfer-Item", 44, 5, 1, 0, 12, 153, 12, 0},
    {-1, 2, 3, "BroadcastCancelledAreaList", 8, 3, 2, 4, 12, 158, 13, 0},
    {-1, 2, 3, "BroadcastCompletedAreaList", 8, 3, 2, 4, 12, 161, 13, 0},
    {-1, 2, 254, "Cause", 8, 5, 2, 4, 12, 164, 13, 0},
    {INT_MAX, 267, 0, "CellAccessMode", 4, 0, 4, 0, 2076, 1, 58, 0},
    {-1, 269, 0, "Cdma2000PDU", 8, 0, 4, 4, 8, 0, 20, 0},
    {INT_MAX, 267, 0, "Cdma2000RATType", 4, 0, 4, 0, 2076, 7, 58, 0},
    {-1, 269, 0, "Cdma2000SectorID", 8, 0, 4, 4, 8, 0, 20, 0},
    {INT_MAX, 267, 0, "Cdma2000HOStatus", 4, 0, 4, 0, 2076, 14, 58, 0},
    {INT_MAX, 267, 0, "Cdma2000HORequiredIndication", 4, 0, 4, 0, 2076, 21, 58, 0},
    {-1, 12, 146, "Cdma2000OneXSRVCCInfo", 32, 4, 1, 0, 12, 169, 12, 0},
    {-1, 269, 0, "Cdma2000OneXRAND", 8, 0, 4, 4, 8, 0, 20, 0},
    {1, 267, 0, "CNDomain", 4, 0, 4, 0, 24, 27, 58, 0},
    {0, 267, 0, "ConcurrentWarningMessageIndicator", 4, 0, 4, 0, 24, 31, 58, 0},
    {4, 269, 0, "Correlation-ID", 2, 0, 2, 2, 216, 0, 21, 54},
    {INT_MAX, 267, 0, "CSFallbackIndicator", 4, 0, 4, 0, 2076, 34, 58, 0},
    {INT_MAX, 267, 0, "AdditionalCSFallbackIndicator", 4, 0, 4, 0, 2076, 41, 58, 0},
    {27, 271, 0, "CSG-Id", 8, 0, 2, 4, 216, 0, 3, 57},
    {256, 12, 0, "CSG-IdList", 4, 16, 2, 4, 216, 1116, 18, 60},
    {1, 267, 0, "CSGMembershipStatus", 4, 0, 4, 0, 24, 48, 58, 0},
    {-1, 12, 134, "COUNTValueExtended", 12, 3, 1, 0, 12, 173, 12, 0},
    {-1, 12, 273, "CriticalityDiagnostics", 20, 5, 1, 0, 12, 176, 12, 0},
    {8, 271, 0, "DataCodingScheme", 8, 0, 2, 4, 216, 0, 3, 63},
    {INT_MAX, 267, 0, "Direct-Forwarding-Path-Availability", 4, 0, 4, 0, 2076, 52, 58, 0},
    {INT_MAX, 267, 0, "Data-Forwarding-Not-Possible", 4, 0, 4, 0, 2076, 58, 58, 0},
    {256, 12, 0, "EmergencyAreaIDListForRestart", 4, 8, 2, 4, 216, 1150, 18, 66},
    {-1, 12, 134, "Global-ENB-ID", 24, 3, 1, 0, 12, 181, 12, 0},
    {256, 12, 0, "GUMMEIList", 4, 20, 2, 4, 216, 154, 18, 69},
    {-1, 12, 186, "ENB-StatusTransfer-TransparentContainer", 12, 2, 1, 0, 12, 184, 12, 0},
    {-1, 313, 0, "ENB-UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 72},
    {150, 315, 19, "ENBname", 8, 7, 2, 4, 220, 64, 25, 74},
    {-1, 12, 317, "E-RABInformationListItem", 16, 3, 1, 0, 12, 186, 12, 0},
    {256, 12, 0, "E-RABList", 4, 24, 2, 4, 216, 1224, 18, 78},
    {-1, 12, 134, "E-RABItem", 20, 3, 1, 0, 12, 189, 12, 0},
    {-1, 12, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 313, 0, "EUTRANRoundTripDelayEstimationInfo", 2, 0, 2, 0, 200, 0, 55, 81},
    {-1, 12, 331, "ExpectedUEBehaviour", 32, 3, 1, 0, 12, 195, 12, 0},
    {-1, 313, 0, "ExtendedRepetitionPeriod", 4, 0, 4, 0, 200, 0, 55, 83},
    {-1, 12, 146, "GUMMEI", 20, 4, 1, 0, 12, 198, 12, 0},
    {INT_MAX, 267, 0, "GUMMEIType", 4, 0, 4, 0, 2076, 67, 58, 0},
    {INT_MAX, 267, 0, "GWContextReleaseIndication", 4, 0, 4, 0, 2076, 21, 58, 0},
    {-1, 12, 18, "HandoverRestrictionList", 28, 6, 1, 0, 12, 202, 12, 0},
    {INT_MAX, 267, 0, "HandoverType", 4, 0, 4, 0, 2076, 74, 58, 0},
    {64, 271, 0, "Masked-IMEISV", 8, 0, 2, 4, 216, 0, 3, 85},
    {0, 267, 0, "KillAllWarningMessages", 4, 0, 4, 0, 24, 31, 58, 0},
    {-1, 12, 134, "LAI", 16, 3, 1, 0, 12, 208, 12, 0},
    {-1, 269, 0, "L3-Information", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "LPPa-PDU", 8, 0, 4, 4, 8, 0, 20, 0},
    {256, 269, 0, "LHN-ID", 2, 0, 2, 2, 216, 0, 21, 88},
    {-1, 12, 349, "LoggedMBSFNMDT", 20, 4, 1, 0, 12, 211, 12, 0},
    {-1, 12, 186, "M3Configuration", 12, 2, 1, 0, 12, 215, 12, 0},
    {-1, 12, 134, "M4Configuration", 16, 3, 1, 0, 12, 217, 12, 0},
    {-1, 12, 134, "M5Configuration", 16, 3, 1, 0, 12, 220, 12, 0},
    {8, 271, 0, "MDT-Location-Info", 8, 0, 2, 4, 216, 0, 3, 91},
    {-1, 12, 146, "MDT-Configuration", 80, 4, 1, 0, 12, 223, 12, 0},
    {INT_MAX, 267, 0, "ManagementBasedMDTAllowed", 4, 0, 4, 0, 2076, 84, 58, 0},
    {16, 12, 0, "MDTPLMNList", 4, 8, 2, 4, 216, 599, 18, 94},
    {INT_MAX, 267, 0, "PrivacyIndicator", 4, 0, 4, 0, 2076, 90, 58, 0},
    {16, 271, 0, "MessageIdentifier", 8, 0, 2, 4, 216, 0, 3, 97},
    {32, 271, 0, "MobilityInformation", 8, 0, 2, 4, 216, 0, 3, 100},
    {150, 315, 19, "MMEname", 8, 7, 2, 4, 220, 64, 25, 103},
    {INT_MAX, 267, 0, "MMERelaySupportIndicator", 4, 0, 4, 0, 2076, 21, 58, 0},
    {-1, 313, 0, "MME-UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 107},
    {-1, 269, 0, "MSClassmark2", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "MSClassmark3", 8, 0, 4, 4, 8, 0, 20, 0},
    {INT_MAX, 267, 0, "MutingAvailabilityIndication", 4, 0, 4, 0, 2076, 97, 58, 0},
    {-1, 12, 317, "MutingPatternInformation", 16, 3, 1, 0, 12, 227, 12, 0},
    {-1, 269, 0, "NAS-PDU", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "NASSecurityParametersfromE-UTRAN", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "NASSecurityParameterstoE-UTRAN", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 313, 0, "NumberofBroadcastRequest", 2, 0, 2, 0, 200, 0, 55, 109},
    {-1, 269, 0, "OldBSS-ToNewBSS-Information", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 2, 227, "OverloadResponse", 8, 1, 2, 4, 12, 230, 13, 0},
    {INT_MAX, 267, 0, "PagingDRX", 4, 0, 4, 0, 2076, 104, 58, 0},
    {INT_MAX, 267, 0, "PagingPriority", 4, 0, 4, 0, 2076, 113, 58, 0},
    {-1, 12, 331, "ProSeAuthorized", 16, 3, 1, 0, 12, 231, 12, 0},
    {INT_MAX, 267, 0, "PS-ServiceNotAvailable", 4, 0, 4, 0, 2076, 126, 58, 0},
    {16384, 271, 0, "ReceiveStatusOfULPDCPSDUsExtended", 8, 0, 2, 4, 216, 0, 3, 111},
    {-1, 313, 0, "RelativeMMECapacity", 2, 0, 2, 0, 200, 0, 55, 114},
    {INT_MAX, 267, 0, "RelayNode-Indicator", 4, 0, 4, 0, 2076, 21, 58, 0},
    {-1, 12, 134, "RequestType", 16, 3, 1, 0, 12, 234, 12, 0},
    {-1, 313, 0, "RepetitionPeriod", 2, 0, 2, 0, 200, 0, 55, 116},
    {INT_MAX, 267, 0, "RRC-Establishment-Cause", 4, 0, 4, 0, 2076, 132, 58, 0},
    {256, 12, 0, "ECGIListForRestart", 4, 20, 2, 4, 216, 150, 18, 118},
    {-1, 313, 0, "Routing-ID", 2, 0, 2, 0, 200, 0, 55, 121},
    {256, 271, 0, "SecurityKey", 8, 0, 2, 4, 216, 0, 3, 123},
    {-1, 12, 134, "SecurityContext", 16, 3, 1, 0, 12, 237, 12, 0},
    {16, 271, 0, "SerialNumber", 8, 0, 2, 4, 216, 0, 3, 126},
    {-1, 2, 227, "SONInformationReport", 28, 1, 2, 4, 12, 240, 13, 0},
    {-1, 12, 146, "SONConfigurationTransfer", 132, 4, 1, 0, 12, 241, 12, 0},
    {-1, 12, 367, "SynchronisationInformation", 32, 4, 1, 0, 12, 245, 12, 0},
    {-1, 269, 0, "Source-ToTarget-TransparentContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "SourceBSS-ToTargetBSS-TransparentContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {INT_MAX, 267, 0, "SRVCCOperationPossible", 4, 0, 4, 0, 2076, 143, 58, 0},
    {INT_MAX, 267, 0, "SRVCCHOIndication", 4, 0, 4, 0, 2076, 149, 58, 0},
    {-1, 12, 395, "SourceeNB-ToTargeteNB-TransparentContainer", 48, 6, 1, 0, 12, 249, 12, 0},
    {-1, 269, 0, "SourceRNC-ToTargetRNC-TransparentContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {8, 12, 0, "ServedGUMMEIs", 4, 20, 2, 4, 216, 1533, 18, 129},
    {-1, 313, 0, "SubscriberProfileIDforRFP", 2, 0, 2, 0, 200, 0, 55, 132},
    {256, 12, 0, "SupportedTAs", 4, 16, 2, 4, 216, 1544, 18, 134},
    {-1, 12, 134, "TimeSynchronisationInfo", 16, 3, 1, 0, 12, 255, 12, 0},
    {-1, 12, 134, "S-TMSI", 16, 3, 1, 0, 12, 258, 12, 0},
    {-1, 12, 134, "TAI", 16, 3, 1, 0, 12, 261, 12, 0},
    {-1, 2, 3, "TargetID", 52, 3, 2, 4, 12, 264, 13, 0},
    {-1, 12, 186, "TargeteNB-ToSourceeNB-TransparentContainer", 16, 2, 1, 0, 12, 267, 12, 0},
    {-1, 269, 0, "Target-ToSource-TransparentContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "TargetRNC-ToSourceRNC-TransparentContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "TargetBSS-ToSourceBSS-TransparentContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {INT_MAX, 267, 0, "TimeToWait", 4, 0, 4, 0, 2076, 156, 58, 0},
    {-1, 313, 0, "Time-UE-StayedInCell-EnhancedGranularity", 2, 0, 2, 0, 200, 0, 55, 137},
    {-1, 12, 186, "TransportInformation", 16, 2, 0, 0, 12, 269, 12, 0},
    {160, 271, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 139},
    {-1, 12, 62, "TraceActivation", 36, 5, 1, 0, 12, 271, 12, 0},
    {8, 269, 0, "E-UTRAN-Trace-ID", 2, 0, 2, 2, 216, 0, 21, 143},
    {-1, 313, 0, "TrafficLoadReductionIndication", 2, 0, 2, 0, 200, 0, 55, 146},
    {-1, 12, 317, "TunnelInformation", 20, 3, 1, 0, 12, 276, 12, 0},
    {2048, 12, 0, "TAIListForRestart", 4, 16, 2, 4, 216, 218, 18, 148},
    {-1, 12, 134, "UEAggregateMaximumBitrate", 24, 3, 1, 0, 12, 279, 12, 0},
    {-1, 2, 220, "UE-S1AP-IDs", 20, 2, 2, 4, 12, 282, 13, 0},
    {-1, 12, 331, "UE-associatedLogicalS1-ConnectionItem", 16, 3, 1, 0, 12, 284, 12, 0},
    {10, 271, 0, "UEIdentityIndexValue", 8, 0, 2, 4, 216, 0, 3, 151},
    {-1, 269, 0, "UE-HistoryInformationFromTheUE", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 2, 220, "UEPagingID", 20, 2, 2, 4, 12, 287, 13, 0},
    {-1, 269, 0, "UERadioCapability", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "UERadioCapabilityForPaging", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 12, 134, "UESecurityCapabilities", 24, 3, 1, 0, 12, 289, 12, 0},
    {-1, 12, 134, "UserLocationInformation", 44, 3, 1, 0, 12, 292, 12, 0},
    {INT_MAX, 267, 0, "VoiceSupportMatchIndicator", 4, 0, 4, 0, 2076, 167, 58, 0},
    {-1, 2, 3, "WarningAreaList", 8, 3, 2, 4, 12, 295, 13, 0},
    {2, 269, 0, "WarningType", 2, 0, 2, 2, 216, 0, 21, 154},
    {50, 269, 0, "WarningSecurityInfo", 2, 0, 2, 2, 216, 0, 21, 157},
    {9600, 269, 0, "WarningMessageContents", 8, 0, 2, 4, 216, 0, 20, 160},
    {-1, 12, 186, "X2TNLConfigurationInfo", 12, 2, 1, 0, 12, 298, 12, 0},
    {16, 12, 0, "ENBX2ExtTLAs", 4, 20, 2, 4, 216, 1668, 18, 163},
    {2, 12, 0, "ENBIndirectX2TransportLayerAddresses", 4, 8, 2, 4, 216, 227, 18, 166},
    {-1, 313, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 169},
    {2, 267, 0, "Criticality", 4, 0, 4, 0, 24, 174, 58, 0},
    {-1, 2, 0, NULL, 2, 0, 0, 0, 8, 0, 50, 0},
    {-1, 2, 0, "S1AP-ELEMENTARY-PROCEDURE", 16, 5, 1, 0, 8, 300, 49, 0},
    {-1, 423, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 171},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 174},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 177},
    {-1, 12, 134, "InitiatingMessage", 24, 3, 0, 0, 264, 305, 12, 0},
    {-1, 423, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 180},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 183},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 186},
    {-1, 12, 134, "SuccessfulOutcome", 24, 3, 0, 0, 264, 308, 12, 0},
    {-1, 423, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 189},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 192},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 195},
    {-1, 12, 134, "UnsuccessfulOutcome", 24, 3, 0, 0, 264, 311, 12, 0},
    {-1, 423, 134, "InitiatingMessage", 24, 3, 0, 0, 264, 305, 12, 0},
    {-1, 425, 134, "SuccessfulOutcome", 24, 3, 0, 0, 264, 308, 12, 0},
    {-1, 427, 134, "UnsuccessfulOutcome", 24, 3, 0, 0, 264, 311, 12, 0},
    {-1, 313, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 198},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 200},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 203},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 206},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 314, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 274, 18, 209},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 212},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 215},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 218},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 317, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 279, 18, 221},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 224},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 227},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 230},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 320, 12, 0},
    {-1, 313, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 233},
    {4, 269, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 236},
    {-1, 313, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 239},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 241},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 243},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 245},
    {160, 429, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 247},
    {4, 431, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 249},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 251},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 254},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 257},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 323, 12, 0},
    {65535, 433, 0, NULL, 4, 24, 2, 4, 216, 296, 18, 260},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 263},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 266},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 269},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 326, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 301, 18, 272},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 275},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 278},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 281},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 329, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 306, 18, 284},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 287},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 290},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 293},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 332, 12, 0},
    {-1, 313, 0, "QCI", 2, 0, 2, 0, 200, 0, 55, 296},
    {-1, 313, 0, "PriorityLevel", 2, 0, 2, 0, 200, 179, 55, 298},
    {1, 267, 0, "Pre-emptionCapability", 4, 0, 4, 0, 24, 185, 58, 0},
    {1, 267, 0, "Pre-emptionVulnerability", 4, 0, 4, 0, 24, 189, 58, 0},
    {-1, 423, 0, "PriorityLevel", 2, 0, 2, 0, 200, 179, 55, 300},
    {1, 425, 0, "Pre-emptionCapability", 4, 0, 4, 0, 24, 185, 58, 0},
    {1, 427, 0, "Pre-emptionVulnerability", 4, 0, 4, 0, 24, 189, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 302},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 305},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 308},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 335, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 322, 18, 311},
    {-1, 12, 146, "AllocationAndRetentionPriority", 16, 4, 1, 0, 12, 338, 12, 0},
    {-1, 313, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 314},
    {-1, 423, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 316},
    {-1, 425, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 318},
    {-1, 427, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 320},
    {-1, 429, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 322},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 324},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 327},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 330},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 342, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 333, 18, 333},
    {-1, 12, 62, "GBR-QosInformation", 40, 5, 1, 0, 12, 345, 12, 0},
    {-1, 423, 0, "QCI", 2, 0, 2, 0, 200, 0, 55, 336},
    {-1, 425, 146, "AllocationAndRetentionPriority", 16, 4, 1, 0, 12, 338, 12, 0},
    {-1, 427, 62, "GBR-QosInformation", 40, 5, 1, 0, 12, 345, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 338},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 341},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 344},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 350, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 342, 18, 347},
    {-1, 12, 349, "E-RABLevelQoSParameters", 64, 4, 1, 0, 12, 353, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 350},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 352},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 354},
    {-1, 429, 349, "E-RABLevelQoSParameters", 64, 4, 1, 0, 12, 353, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 356},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 359},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 362},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 357, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 352, 18, 365},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 368},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 371},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 374},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 360, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 357, 18, 377},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 380},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 383},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 386},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 363, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 389},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 391},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 393},
    {160, 429, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 395},
    {4, 431, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 397},
    {160, 433, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 399},
    {4, 435, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 401},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 403},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 406},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 409},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 366, 12, 0},
    {65535, 437, 0, NULL, 4, 24, 2, 4, 216, 373, 18, 412},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 415},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 418},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 421},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 369, 12, 0},
    {INT_MAX, 267, 0, "CauseRadioNetwork", 4, 0, 4, 0, 2076, 193, 58, 0},
    {INT_MAX, 267, 0, "CauseTransport", 4, 0, 4, 0, 2076, 237, 58, 0},
    {INT_MAX, 267, 0, "CauseNas", 4, 0, 4, 0, 2076, 244, 58, 0},
    {INT_MAX, 267, 0, "CauseProtocol", 4, 0, 4, 0, 2076, 254, 58, 0},
    {INT_MAX, 267, 0, "CauseMisc", 4, 0, 4, 0, 2076, 266, 58, 0},
    {INT_MAX, 423, 0, "CauseRadioNetwork", 4, 0, 4, 0, 2076, 193, 58, 0},
    {INT_MAX, 425, 0, "CauseTransport", 4, 0, 4, 0, 2076, 237, 58, 0},
    {INT_MAX, 427, 0, "CauseNas", 4, 0, 4, 0, 2076, 244, 58, 0},
    {INT_MAX, 429, 0, "CauseProtocol", 4, 0, 4, 0, 2076, 254, 58, 0},
    {INT_MAX, 431, 0, "CauseMisc", 4, 0, 4, 0, 2076, 266, 58, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 424},
    {-1, 425, 254, "Cause", 8, 5, 2, 4, 12, 164, 13, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 426},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 429},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 432},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 372, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 394, 18, 435},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 438},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 441},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 444},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 375, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 399, 18, 447},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 450},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 453},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 456},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 378, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 404, 18, 459},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 462},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 465},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 468},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 381, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 409, 18, 471},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 474},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 477},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 480},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 384, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 483},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 485},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 487},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 489},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 492},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 495},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 387, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 421, 18, 498},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 501},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 504},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 507},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 390, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 426, 18, 510},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 513},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 516},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 519},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 393, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 522},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 524},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 526},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 528},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 531},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 534},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 396, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 438, 18, 537},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 540},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 543},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 546},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 399, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 443, 18, 549},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 552},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 555},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 558},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 402, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 448, 18, 561},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 564},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 567},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 570},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 405, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 453, 18, 573},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 576},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 579},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 582},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 408, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 458, 18, 585},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 588},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 591},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 594},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 411, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 597},
    {-1, 425, 349, "E-RABLevelQoSParameters", 64, 4, 1, 0, 12, 353, 12, 0},
    {160, 427, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 599},
    {4, 429, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 601},
    {-1, 431, 0, "NAS-PDU", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 603},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 606},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 609},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 414, 12, 0},
    {65535, 433, 0, NULL, 4, 24, 2, 4, 216, 472, 18, 612},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 615},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 618},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 621},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 417, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 477, 18, 624},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 627},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 630},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 633},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 420, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 636},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 638},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 640},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 642},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 645},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 648},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 423, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 489, 18, 651},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 654},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 657},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 660},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 426, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 494, 18, 663},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 666},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 669},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 672},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 429, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 675},
    {-1, 425, 349, "E-RABLevelQoSParameters", 64, 4, 1, 0, 12, 353, 12, 0},
    {-1, 427, 0, "NAS-PDU", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 677},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 680},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 683},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 432, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 506, 18, 686},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 689},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 692},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 695},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 435, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 511, 18, 698},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 701},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 704},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 707},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 438, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 710},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 712},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 715},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 718},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 441, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 521, 18, 721},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 724},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 727},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 730},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 444, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 526, 18, 733},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 736},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 739},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 742},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 447, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 531, 18, 745},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 748},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 751},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 754},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 450, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 757},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 759},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 762},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 765},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 453, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 541, 18, 768},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 771},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 774},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 777},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 456, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 546, 18, 780},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 783},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 786},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 789},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 459, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 551, 18, 792},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 795},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 798},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 801},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 462, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 804},
    {-1, 425, 349, "E-RABLevelQoSParameters", 64, 4, 1, 0, 12, 353, 12, 0},
    {160, 427, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 806},
    {4, 429, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 808},
    {-1, 431, 0, "NAS-PDU", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 810},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 813},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 816},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 465, 12, 0},
    {65535, 433, 0, NULL, 4, 24, 2, 4, 216, 565, 18, 819},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 822},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 825},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 828},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 468, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 570, 18, 831},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 834},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 837},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 840},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 471, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 843},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 845},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 847},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 849},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 852},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 855},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 474, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 582, 18, 858},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 861},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 864},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 867},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 477, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 587, 18, 870},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 873},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 876},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 879},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 480, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 592, 18, 882},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 885},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 888},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 891},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 483, 12, 0},
    {3, 269, 0, "TBCD-STRING", 2, 0, 2, 2, 216, 0, 21, 894},
    {3, 269, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 897},
    {2, 269, 0, "TAC", 2, 0, 2, 2, 216, 0, 21, 899},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 902},
    {2, 425, 0, "TAC", 2, 0, 2, 2, 216, 0, 21, 904},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 906},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 909},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 912},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 486, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 606, 18, 915},
    {-1, 423, 134, "TAI", 16, 3, 1, 0, 12, 261, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 918},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 921},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 924},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 489, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 612, 18, 927},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 930},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 933},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 936},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 492, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 617, 18, 939},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 942},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 945},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 948},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 495, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 622, 18, 951},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 954},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 957},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 960},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 498, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 627, 18, 963},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 966},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 969},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 972},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 501, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 632, 18, 975},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 978},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 981},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 984},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 504, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 637, 18, 987},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 990},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 993},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 996},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 507, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 642, 18, 999},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1002},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1005},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1008},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 510, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 647, 18, 1011},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1014},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1017},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1020},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 513, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 652, 18, 1023},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1026},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1029},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1032},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 516, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 657, 18, 1035},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1038},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1041},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1044},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 519, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 662, 18, 1047},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1050},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1053},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1056},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 522, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 667, 18, 1059},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1062},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1065},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1068},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 525, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 672, 18, 1071},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1074},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1077},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1080},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 528, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 677, 18, 1083},
    {INT_MAX, 267, 0, "ResetAll", 4, 0, 4, 0, 2076, 277, 58, 0},
    {INT_MAX, 423, 0, "ResetAll", 4, 0, 4, 0, 2076, 277, 58, 0},
    {256, 425, 0, "UE-associatedLogicalS1-ConnectionListRes", 4, 24, 2, 4, 216, 685, 18, 1086},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1089},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1092},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1095},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 531, 12, 0},
    {256, 12, 0, "UE-associatedLogicalS1-ConnectionListRes", 4, 24, 2, 4, 216, 685, 18, 1098},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1100},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1103},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1106},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 534, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 690, 18, 1109},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1112},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1115},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1118},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 537, 12, 0},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1121},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1124},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1127},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 540, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 699, 18, 1130},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1133},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1136},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1139},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 543, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 704, 18, 1142},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1145},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1148},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1151},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 546, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 709, 18, 1154},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1157},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1160},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1163},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 549, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 714, 18, 1166},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1169},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1172},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1175},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 552, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 719, 18, 1178},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1181},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1184},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1187},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 555, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 724, 18, 1190},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1193},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1196},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1199},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 558, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 729, 18, 1202},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1205},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1208},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1211},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 561, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 734, 18, 1214},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1217},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1220},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1223},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 564, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 739, 18, 1226},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1229},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1232},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1235},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 567, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 744, 18, 1238},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1241},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1244},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1247},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 570, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 749, 18, 1250},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1253},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1256},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1259},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 573, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 754, 18, 1262},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1265},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1268},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1271},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 576, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 759, 18, 1274},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1277},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1280},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1283},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 579, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 764, 18, 1286},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1289},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1292},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1295},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 582, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 769, 18, 1298},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1301},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1304},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1307},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 585, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 774, 18, 1310},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1313},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1316},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1319},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 588, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 779, 18, 1322},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1325},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1328},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1331},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 591, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 784, 18, 1334},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1337},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1340},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1343},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 594, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 789, 18, 1346},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1349},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1352},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1355},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 597, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 794, 18, 1358},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1361},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1364},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1367},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 600, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 799, 18, 1370},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1373},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1376},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1379},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 603, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 804, 18, 1382},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1385},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1388},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1391},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 606, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 809, 18, 1394},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1397},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1400},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1403},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 609, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 814, 18, 1406},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1409},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1412},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1415},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 612, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 819, 18, 1418},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1421},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1424},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1427},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 615, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 824, 18, 1430},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1433},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1436},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1439},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 618, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 829, 18, 1442},
    {-1, 269, 0, "RIMInformation", 8, 0, 4, 4, 8, 0, 20, 0},
    {2, 269, 0, "LAC", 2, 0, 2, 2, 216, 0, 21, 1445},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 1448},
    {2, 425, 0, "LAC", 2, 0, 2, 2, 216, 0, 21, 1450},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1452},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1455},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1458},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 621, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 838, 18, 1461},
    {1, 269, 0, "RAC", 2, 0, 2, 2, 216, 0, 21, 1464},
    {2, 269, 0, "CI", 2, 0, 2, 2, 216, 0, 21, 1467},
    {-1, 423, 134, "LAI", 16, 3, 1, 0, 12, 208, 12, 0},
    {1, 425, 0, "RAC", 2, 0, 2, 2, 216, 0, 21, 1470},
    {2, 427, 0, "CI", 2, 0, 2, 2, 216, 0, 21, 1472},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1474},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1477},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1480},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 624, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 848, 18, 1483},
    {-1, 12, 146, "GERAN-Cell-ID", 32, 4, 1, 0, 12, 627, 12, 0},
    {-1, 313, 0, "RNC-ID", 2, 0, 2, 0, 200, 0, 55, 1486},
    {-1, 313, 0, "ExtendedRNC-ID", 2, 0, 2, 0, 200, 0, 55, 1488},
    {-1, 423, 134, "LAI", 16, 3, 1, 0, 12, 208, 12, 0},
    {1, 425, 0, "RAC", 2, 0, 2, 2, 216, 0, 21, 1490},
    {-1, 427, 0, "RNC-ID", 2, 0, 2, 0, 200, 0, 55, 1492},
    {-1, 429, 0, "ExtendedRNC-ID", 2, 0, 2, 0, 200, 0, 55, 1494},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1496},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1499},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1502},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 631, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 860, 18, 1505},
    {-1, 12, 439, "TargetRNC-ID", 32, 5, 1, 0, 12, 634, 12, 0},
    {-1, 423, 146, "GERAN-Cell-ID", 32, 4, 1, 0, 12, 627, 12, 0},
    {-1, 425, 439, "TargetRNC-ID", 32, 5, 1, 0, 12, 634, 12, 0},
    {16, 427, 0, "_octet1", 2, 0, 2, 2, 248, 0, 21, 1508},
    {2, 2, 3, "RIMRoutingAddress", 36, 3, 2, 4, 2076, 639, 13, 0},
    {-1, 423, 0, "RIMInformation", 8, 0, 4, 4, 8, 0, 20, 0},
    {2, 425, 3, "RIMRoutingAddress", 36, 3, 2, 4, 2076, 639, 13, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1511},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1514},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1517},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 642, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 872, 18, 1520},
    {-1, 12, 317, "RIMTransfer", 52, 3, 1, 0, 12, 645, 12, 0},
    {-1, 423, 317, "RIMTransfer", 52, 3, 1, 0, 12, 645, 12, 0},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1523},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1526},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1529},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 648, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 879, 18, 1532},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1535},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1538},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1541},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 651, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 884, 18, 1544},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1547},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1550},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1553},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 654, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 889, 18, 1556},
    {-1, 423, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 1559},
    {-1, 425, 0, "ObjectID", 8, 2, 2, 4, 40, 0, 63, 0},
    {-1, 2, 463, "PrivateIE-ID", 12, 2, 2, 4, 8, 657, 13, 0},
    {-1, 423, 463, "PrivateIE-ID", 12, 2, 2, 4, 72, 657, 13, 1561},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1562},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1565},
    {-1, 12, 134, "PrivateIE-Field", 32, 3, 0, 0, 296, 659, 12, 0},
    {65535, 423, 0, NULL, 4, 32, 2, 4, 216, 897, 18, 1568},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1571},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1574},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1577},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 662, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 902, 18, 1580},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1583},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1586},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1589},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 665, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 907, 18, 1592},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1595},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1598},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1601},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 668, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 912, 18, 1604},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1607},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1610},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1613},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 671, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 917, 18, 1616},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1619},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1622},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1625},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 674, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 922, 18, 1628},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1631},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1634},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1637},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 677, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 927, 18, 1640},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1643},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1646},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1649},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 680, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 932, 18, 1652},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1655},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1658},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1661},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 683, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 937, 18, 1664},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1667},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1670},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1673},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 686, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1676},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 1678},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 1680},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1682},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1685},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1688},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 689, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 949, 18, 1691},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1694},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1697},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1700},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 692, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1703},
    {160, 425, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 1705},
    {4, 427, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 1707},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1709},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1712},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1715},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 695, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 961, 18, 1718},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1721},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1724},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1727},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 698, 12, 0},
    {65535, 423, 0, NULL, 4, 24, 2, 4, 216, 966, 18, 1730},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1733},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1736},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1739},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 701, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1742},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1744},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1747},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1750},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 704, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 976, 18, 1753},
    {32, 423, 0, "CellIdListforMDT", 4, 20, 2, 4, 216, 150, 18, 1756},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1759},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1762},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1765},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 707, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 982, 18, 1768},
    {-1, 12, 186, "CellBasedMDT", 12, 2, 1, 0, 12, 710, 12, 0},
    {8, 423, 0, "TAListforMDT", 4, 4, 2, 4, 216, 600, 18, 1771},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1774},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1777},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1780},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 712, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 989, 18, 1783},
    {-1, 12, 186, "TABasedMDT", 12, 2, 1, 0, 12, 715, 12, 0},
    {8, 423, 0, "TAIListforMDT", 4, 16, 2, 4, 216, 218, 18, 1786},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1789},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1792},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1795},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 717, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 996, 18, 1798},
    {-1, 12, 186, "TAIBasedMDT", 12, 2, 1, 0, 12, 720, 12, 0},
    {-1, 423, 186, "CellBasedMDT", 12, 2, 1, 0, 12, 710, 12, 0},
    {-1, 425, 186, "TABasedMDT", 12, 2, 1, 0, 12, 715, 12, 0},
    {-1, 427, 0, NULL, 1, 0, 0, 0, 8, 0, 5, 0},
    {-1, 429, 186, "TAIBasedMDT", 12, 2, 1, 0, 12, 720, 12, 0},
    {1, 2, 468, "AreaScopeOfMDT", 16, 4, 2, 4, 2076, 722, 13, 0},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1801},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1804},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1807},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 726, 12, 0},
    {256, 12, 0, "Bearers-SubjectToStatusTransferList", 4, 24, 2, 4, 216, 1007, 18, 1810},
    {-1, 313, 0, "PDCP-SN", 2, 0, 2, 0, 200, 0, 55, 1813},
    {-1, 313, 0, "HFN", 4, 0, 4, 0, 200, 0, 55, 1815},
    {-1, 423, 0, "PDCP-SN", 2, 0, 2, 0, 200, 0, 55, 1817},
    {-1, 425, 0, "HFN", 4, 0, 4, 0, 200, 0, 55, 1819},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1821},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1824},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1827},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 729, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1016, 18, 1830},
    {-1, 12, 134, "COUNTvalue", 12, 3, 1, 0, 12, 732, 12, 0},
    {4096, 271, 0, "ReceiveStatusofULPDCPSDUs", 8, 0, 2, 4, 216, 0, 3, 1833},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1836},
    {-1, 425, 134, "COUNTvalue", 12, 3, 1, 0, 12, 732, 12, 0},
    {-1, 427, 134, "COUNTvalue", 12, 3, 1, 0, 12, 732, 12, 0},
    {4096, 429, 0, "ReceiveStatusofULPDCPSDUs", 8, 0, 2, 4, 216, 0, 3, 1838},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1840},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1843},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1846},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 735, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 1027, 18, 1849},
    {6, 12, 0, "BPLMNs", 4, 8, 2, 4, 216, 599, 18, 1852},
    {65535, 423, 0, "CellID-Cancelled", 4, 32, 2, 4, 216, 1078, 18, 1855},
    {65535, 425, 0, "TAI-Cancelled", 4, 28, 2, 4, 216, 1578, 18, 1858},
    {65535, 427, 0, "EmergencyAreaID-Cancelled", 4, 16, 2, 4, 216, 1168, 18, 1861},
    {65535, 423, 0, "CellID-Broadcast", 4, 28, 2, 4, 216, 1069, 18, 1864},
    {65535, 425, 0, "TAI-Broadcast", 4, 28, 2, 4, 216, 1569, 18, 1867},
    {65535, 427, 0, "EmergencyAreaID-Broadcast", 4, 16, 2, 4, 216, 1159, 18, 1870},
    {28, 271, 0, "CellIdentity", 8, 0, 2, 4, 216, 0, 3, 1873},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 1876},
    {28, 425, 0, "CellIdentity", 8, 0, 2, 4, 216, 0, 3, 1878},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1880},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1883},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1886},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 738, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1042, 18, 1889},
    {-1, 313, 0, "NumberOfBroadcasts", 2, 0, 2, 0, 200, 0, 55, 1892},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 425, 0, "NumberOfBroadcasts", 2, 0, 2, 0, 200, 0, 55, 1894},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1896},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1899},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1902},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 741, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1050, 18, 1905},
    {-1, 12, 134, "CancelledCellinEAI-Item", 32, 3, 1, 0, 12, 744, 12, 0},
    {65535, 12, 0, "CancelledCellinEAI", 4, 32, 2, 4, 216, 1052, 18, 1908},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 425, 0, "NumberOfBroadcasts", 2, 0, 2, 0, 200, 0, 55, 1911},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1913},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1916},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1919},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 747, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1059, 18, 1922},
    {-1, 12, 134, "CancelledCellinTAI-Item", 32, 3, 1, 0, 12, 750, 12, 0},
    {65535, 12, 0, "CancelledCellinTAI", 4, 32, 2, 4, 216, 1061, 18, 1925},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1928},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1931},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1934},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 753, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1067, 18, 1937},
    {-1, 12, 186, "CellID-Broadcast-Item", 28, 2, 1, 0, 12, 756, 12, 0},
    {65535, 12, 0, "CellID-Broadcast", 4, 28, 2, 4, 216, 1069, 18, 1940},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 425, 0, "NumberOfBroadcasts", 2, 0, 2, 0, 200, 0, 55, 1942},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1944},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1947},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1950},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 758, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1076, 18, 1953},
    {-1, 12, 134, "CellID-Cancelled-Item", 32, 3, 1, 0, 12, 761, 12, 0},
    {65535, 12, 0, "CellID-Cancelled", 4, 32, 2, 4, 216, 1078, 18, 1956},
    {32, 12, 0, "CellIdListforMDT", 4, 20, 2, 4, 216, 150, 18, 1958},
    {-1, 269, 0, "Cdma2000OneXMEID", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "Cdma2000OneXMSI", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "Cdma2000OneXPilot", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "Cdma2000OneXMEID", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 425, 0, "Cdma2000OneXMSI", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 427, 0, "Cdma2000OneXPilot", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1960},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1963},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1966},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 764, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1090, 18, 1969},
    {INT_MAX, 267, 0, "Cell-Size", 4, 0, 4, 0, 2076, 283, 58, 0},
    {INT_MAX, 423, 0, "Cell-Size", 4, 0, 4, 0, 2076, 283, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1972},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1975},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1978},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 767, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1097, 18, 1981},
    {-1, 12, 186, "CellType", 12, 2, 1, 0, 12, 770, 12, 0},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 1984},
    {2, 425, 0, "LAC", 2, 0, 2, 2, 216, 0, 21, 1986},
    {2, 427, 0, "CI", 2, 0, 2, 2, 216, 0, 21, 1988},
    {1, 429, 0, "RAC", 2, 0, 2, 2, 216, 0, 21, 1990},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 1992},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 1995},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1998},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 772, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 1107, 18, 2001},
    {-1, 12, 232, "CGI", 24, 5, 1, 0, 12, 775, 12, 0},
    {27, 423, 0, "CSG-Id", 8, 0, 2, 4, 216, 0, 3, 2004},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2006},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2009},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2012},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 780, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1114, 18, 2015},
    {-1, 12, 186, "CSG-IdList-Item", 16, 2, 1, 0, 12, 783, 12, 0},
    {-1, 313, 0, "PDCP-SNExtended", 2, 0, 2, 0, 200, 0, 55, 2018},
    {-1, 313, 0, "HFNModified", 4, 0, 4, 0, 200, 0, 55, 2020},
    {-1, 423, 0, "PDCP-SNExtended", 2, 0, 2, 0, 200, 0, 55, 2022},
    {-1, 425, 0, "HFNModified", 4, 0, 4, 0, 200, 0, 55, 2024},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2026},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2029},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2032},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 785, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1124, 18, 2035},
    {2, 267, 0, "TriggeringMessage", 4, 0, 4, 0, 24, 292, 58, 0},
    {-1, 423, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 2038},
    {2, 425, 0, "TriggeringMessage", 4, 0, 4, 0, 24, 292, 58, 0},
    {2, 427, 0, "Criticality", 4, 0, 4, 0, 24, 174, 58, 0},
    {256, 429, 0, "CriticalityDiagnostics-IE-List", 4, 20, 2, 4, 216, 1145, 18, 2040},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2043},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2046},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2049},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 788, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 1134, 18, 2052},
    {INT_MAX, 267, 0, "TypeOfError", 4, 0, 4, 0, 2076, 297, 58, 0},
    {2, 423, 0, "Criticality", 4, 0, 4, 0, 24, 174, 58, 0},
    {-1, 425, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2055},
    {INT_MAX, 427, 0, "TypeOfError", 4, 0, 4, 0, 2076, 297, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2057},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2060},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2063},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 791, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1143, 18, 2066},
    {-1, 12, 146, "CriticalityDiagnostics-IE-Item", 20, 4, 1, 0, 12, 794, 12, 0},
    {256, 12, 0, "CriticalityDiagnostics-IE-List", 4, 20, 2, 4, 216, 1145, 18, 2069},
    {INT_MAX, 267, 0, "DL-Forwarding", 4, 0, 4, 0, 2076, 304, 58, 0},
    {-1, 313, 0, "EARFCN", 4, 0, 4, 0, 204, 0, 0, 2071},
    {65535, 12, 0, "ECGIList", 4, 20, 2, 4, 216, 150, 18, 2074},
    {3, 269, 0, "EmergencyAreaID", 2, 0, 2, 2, 216, 0, 21, 2077},
    {65535, 12, 0, "EmergencyAreaIDList", 4, 8, 2, 4, 216, 1150, 18, 2080},
    {3, 423, 0, "EmergencyAreaID", 2, 0, 2, 2, 216, 0, 21, 2083},
    {65535, 425, 0, "CompletedCellinEAI", 4, 28, 2, 4, 216, 1176, 18, 2085},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2088},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2091},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2094},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 798, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1157, 18, 2097},
    {-1, 12, 134, "EmergencyAreaID-Broadcast-Item", 16, 3, 1, 0, 12, 801, 12, 0},
    {65535, 12, 0, "EmergencyAreaID-Broadcast", 4, 16, 2, 4, 216, 1159, 18, 2100},
    {3, 423, 0, "EmergencyAreaID", 2, 0, 2, 2, 216, 0, 21, 2102},
    {65535, 425, 0, "CancelledCellinEAI", 4, 32, 2, 4, 216, 1052, 18, 2104},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2106},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2109},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2112},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 804, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1166, 18, 2115},
    {-1, 12, 134, "EmergencyAreaID-Cancelled-Item", 16, 3, 1, 0, 12, 807, 12, 0},
    {65535, 12, 0, "EmergencyAreaID-Cancelled", 4, 16, 2, 4, 216, 1168, 18, 2118},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2120},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2123},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2126},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 810, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1174, 18, 2129},
    {-1, 12, 186, "CompletedCellinEAI-Item", 28, 2, 1, 0, 12, 813, 12, 0},
    {65535, 12, 0, "CompletedCellinEAI", 4, 28, 2, 4, 216, 1176, 18, 2132},
    {256, 12, 0, "ECGI-List", 4, 20, 2, 4, 216, 150, 18, 2134},
    {20, 423, 0, "_bit1", 8, 0, 2, 4, 248, 0, 3, 2137},
    {28, 425, 0, "", 8, 0, 2, 4, 248, 0, 3, 2140},
    {-1, 2, 220, "ENB-ID", 12, 2, 2, 4, 12, 815, 13, 0},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 2143},
    {-1, 425, 220, "ENB-ID", 12, 2, 2, 4, 12, 815, 13, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2145},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2148},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2151},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 817, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1187, 18, 2154},
    {2, 269, 0, "MME-Group-ID", 2, 0, 2, 2, 216, 0, 21, 2157},
    {1, 269, 0, "MME-Code", 2, 0, 2, 2, 216, 0, 21, 2160},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 2163},
    {2, 425, 0, "MME-Group-ID", 2, 0, 2, 2, 216, 0, 21, 2165},
    {1, 427, 0, "MME-Code", 2, 0, 2, 2, 216, 0, 21, 2167},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2169},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2172},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2175},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 820, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1197, 18, 2178},
    {256, 423, 0, "Bearers-SubjectToStatusTransferList", 4, 24, 2, 4, 216, 1007, 18, 2181},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2183},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2186},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2189},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 823, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1203, 18, 2192},
    {2, 12, 0, "ENBX2TLAs", 4, 8, 2, 4, 216, 227, 18, 2195},
    {16, 271, 0, "EncryptionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 2198},
    {15, 12, 0, "EPLMNs", 4, 8, 2, 4, 216, 599, 18, 2202},
    {INT_MAX, 267, 0, "EventType", 4, 0, 4, 0, 2076, 310, 58, 0},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2205},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2208},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2211},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 826, 12, 0},
    {256, 12, 0, "E-RABInformationList", 4, 24, 2, 4, 216, 1212, 18, 2214},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 2217},
    {INT_MAX, 425, 0, "DL-Forwarding", 4, 0, 4, 0, 2076, 304, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2219},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2222},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2225},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 829, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1219, 18, 2228},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2231},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2234},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2237},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 832, 12, 0},
    {-1, 423, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 2240},
    {-1, 425, 254, "Cause", 8, 5, 2, 4, 12, 164, 13, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2242},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2245},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2248},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 835, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1230, 18, 2251},
    {-1, 313, 0, "ExpectedActivityPeriod", 4, 0, 4, 0, 204, 0, 0, 2254},
    {-1, 313, 0, "ExpectedIdlePeriod", 4, 0, 4, 0, 204, 0, 0, 2266},
    {INT_MAX, 267, 0, "SourceOfUEActivityBehaviourInformation", 4, 0, 4, 0, 2076, 318, 58, 0},
    {-1, 423, 0, "ExpectedActivityPeriod", 4, 0, 4, 0, 204, 0, 0, 2278},
    {-1, 425, 0, "ExpectedIdlePeriod", 4, 0, 4, 0, 204, 0, 0, 2280},
    {INT_MAX, 427, 0, "SourceOfUEActivityBehaviourInformation", 4, 0, 4, 0, 2076, 318, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2282},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2285},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2288},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 838, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1241, 18, 2291},
    {-1, 12, 367, "ExpectedUEActivityBehaviour", 20, 4, 1, 0, 12, 841, 12, 0},
    {INT_MAX, 267, 0, "ExpectedHOInterval", 4, 0, 4, 0, 2076, 325, 58, 0},
    {-1, 423, 367, "ExpectedUEActivityBehaviour", 20, 4, 1, 0, 12, 841, 12, 0},
    {INT_MAX, 425, 0, "ExpectedHOInterval", 4, 0, 4, 0, 2076, 325, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2294},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2297},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2300},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 845, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1250, 18, 2303},
    {INT_MAX, 267, 0, "ForbiddenInterRATs", 4, 0, 4, 0, 2076, 337, 58, 0},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 2306},
    {4096, 425, 0, "ForbiddenTACs", 4, 4, 2, 4, 216, 600, 18, 2308},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2311},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2314},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2317},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 848, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1258, 18, 2320},
    {-1, 12, 134, "ForbiddenTAs-Item", 16, 3, 1, 0, 12, 851, 12, 0},
    {16, 12, 0, "ForbiddenTAs", 4, 16, 2, 4, 216, 1260, 18, 2323},
    {4096, 12, 0, "ForbiddenTACs", 4, 4, 2, 4, 216, 600, 18, 2326},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 2328},
    {4096, 425, 0, "ForbiddenLACs", 4, 4, 2, 4, 216, 832, 18, 2330},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2333},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2336},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2339},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 854, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1268, 18, 2342},
    {-1, 12, 134, "ForbiddenLAs-Item", 16, 3, 1, 0, 12, 857, 12, 0},
    {16, 12, 0, "ForbiddenLAs", 4, 16, 2, 4, 216, 1270, 18, 2345},
    {4096, 12, 0, "ForbiddenLACs", 4, 4, 2, 4, 216, 832, 18, 2348},
    {3, 423, 0, "PLMNidentity", 2, 0, 2, 2, 216, 0, 21, 2350},
    {15, 425, 0, "EPLMNs", 4, 8, 2, 4, 216, 599, 18, 2352},
    {16, 427, 0, "ForbiddenTAs", 4, 16, 2, 4, 216, 1260, 18, 2354},
    {16, 429, 0, "ForbiddenLAs", 4, 16, 2, 4, 216, 1270, 18, 2356},
    {INT_MAX, 431, 0, "ForbiddenInterRATs", 4, 0, 4, 0, 2076, 337, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2358},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2361},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2364},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 860, 12, 0},
    {65535, 433, 0, NULL, 4, 24, 2, 4, 216, 1281, 18, 2367},
    {8, 271, 0, "MeasurementsToActivate", 8, 0, 2, 4, 216, 0, 3, 2370},
    {INT_MAX, 267, 0, "M1ReportingTrigger", 4, 0, 4, 0, 2076, 348, 58, 0},
    {-1, 313, 0, "Threshold-RSRP", 2, 0, 2, 0, 200, 0, 55, 2373},
    {-1, 313, 0, "Threshold-RSRQ", 2, 0, 2, 0, 200, 0, 55, 2375},
    {-1, 423, 0, "Threshold-RSRP", 2, 0, 2, 0, 200, 0, 55, 2377},
    {-1, 425, 0, "Threshold-RSRQ", 2, 0, 2, 0, 200, 0, 55, 2379},
    {-1, 2, 220, "MeasurementThresholdA2", 4, 2, 2, 2, 12, 863, 13, 0},
    {-1, 423, 220, "MeasurementThresholdA2", 4, 2, 2, 2, 12, 863, 13, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2381},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2384},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2387},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 865, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1294, 18, 2390},
    {-1, 12, 186, "M1ThresholdEventA2", 12, 2, 1, 0, 12, 868, 12, 0},
    {12, 267, 0, "ReportIntervalMDT", 4, 0, 4, 0, 24, 356, 58, 0},
    {7, 267, 0, "ReportAmountMDT", 4, 0, 4, 0, 24, 371, 58, 0},
    {12, 423, 0, "ReportIntervalMDT", 4, 0, 4, 0, 24, 356, 58, 0},
    {7, 425, 0, "ReportAmountMDT", 4, 0, 4, 0, 24, 371, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2393},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2396},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2399},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 870, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1304, 18, 2402},
    {-1, 12, 134, "M1PeriodicReporting", 16, 3, 1, 0, 12, 873, 12, 0},
    {8, 423, 0, "MeasurementsToActivate", 8, 0, 2, 4, 216, 0, 3, 2405},
    {INT_MAX, 425, 0, "M1ReportingTrigger", 4, 0, 4, 0, 2076, 348, 58, 0},
    {-1, 427, 186, "M1ThresholdEventA2", 12, 2, 1, 0, 12, 868, 12, 0},
    {-1, 429, 134, "M1PeriodicReporting", 16, 3, 1, 0, 12, 873, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2407},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2410},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2413},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 876, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 1314, 18, 2416},
    {-1, 12, 479, "ImmediateMDT", 48, 5, 1, 0, 12, 879, 12, 0},
    {8, 269, 0, "IMSI", 2, 0, 2, 2, 216, 0, 21, 2419},
    {16, 271, 0, "IntegrityProtectionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 2422},
    {8, 271, 0, "InterfacesToTrace", 8, 0, 2, 4, 216, 0, 3, 2426},
    {-1, 313, 0, "Time-UE-StayedInCell", 2, 0, 2, 0, 200, 0, 55, 2429},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 425, 186, "CellType", 12, 2, 1, 0, 12, 770, 12, 0},
    {-1, 427, 0, "Time-UE-StayedInCell", 2, 0, 2, 0, 200, 0, 55, 2431},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2433},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2436},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2439},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 884, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1327, 18, 2442},
    {-1, 12, 146, "LastVisitedEUTRANCellInformation", 44, 4, 1, 0, 12, 887, 12, 0},
    {-1, 269, 0, "LastVisitedUTRANCellInformation", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, NULL, 1, 0, 0, 0, 8, 0, 5, 0},
    {-1, 2, 227, "LastVisitedGERANCellInformation", 4, 1, 2, 2, 12, 891, 13, 0},
    {-1, 423, 146, "LastVisitedEUTRANCellInformation", 44, 4, 1, 0, 12, 887, 12, 0},
    {-1, 425, 0, "LastVisitedUTRANCellInformation", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 427, 227, "LastVisitedGERANCellInformation", 4, 1, 2, 2, 12, 891, 13, 0},
    {-1, 2, 3, "LastVisitedCell-Item", 48, 3, 2, 4, 12, 892, 13, 0},
    {INT_MAX, 267, 0, "Links-to-log", 4, 0, 4, 0, 2076, 381, 58, 0},
    {INT_MAX, 423, 0, NULL, 4, 0, 4, 0, 2076, 389, 58, 0},
    {-1, 425, 0, NULL, 4, 0, 4, 0, 204, 0, 0, 2445},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2448},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2451},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2454},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 895, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1343, 18, 2457},
    {-1, 12, 134, "ListeningSubframePattern", 16, 3, 1, 0, 12, 898, 12, 0},
    {7, 267, 0, "LoggingInterval", 4, 0, 4, 0, 24, 398, 58, 0},
    {5, 267, 0, "LoggingDuration", 4, 0, 4, 0, 24, 408, 58, 0},
    {7, 423, 0, "LoggingInterval", 4, 0, 4, 0, 24, 398, 58, 0},
    {5, 425, 0, "LoggingDuration", 4, 0, 4, 0, 24, 408, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2460},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2463},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2466},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 901, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1353, 18, 2469},
    {-1, 12, 134, "LoggedMDT", 16, 3, 1, 0, 12, 904, 12, 0},
    {7, 423, 0, "LoggingInterval", 4, 0, 4, 0, 24, 398, 58, 0},
    {5, 425, 0, "LoggingDuration", 4, 0, 4, 0, 24, 408, 58, 0},
    {8, 427, 0, "MBSFN-ResultToLog", 4, 12, 2, 4, 216, 1411, 18, 2472},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2475},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2478},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2481},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 907, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1362, 18, 2484},
    {INT_MAX, 267, 0, "M3period", 4, 0, 4, 0, 2076, 416, 58, 0},
    {INT_MAX, 423, 0, "M3period", 4, 0, 4, 0, 2076, 416, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2487},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2490},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2493},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 910, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1369, 18, 2496},
    {INT_MAX, 267, 0, "M4period", 4, 0, 4, 0, 2076, 424, 58, 0},
    {INT_MAX, 423, 0, "M4period", 4, 0, 4, 0, 2076, 424, 58, 0},
    {INT_MAX, 425, 0, "Links-to-log", 4, 0, 4, 0, 2076, 381, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2499},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2502},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2505},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 913, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1377, 18, 2508},
    {INT_MAX, 267, 0, "M5period", 4, 0, 4, 0, 2076, 424, 58, 0},
    {INT_MAX, 423, 0, "M5period", 4, 0, 4, 0, 2076, 424, 58, 0},
    {INT_MAX, 425, 0, "Links-to-log", 4, 0, 4, 0, 2076, 381, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2511},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2514},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2517},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 916, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1385, 18, 2520},
    {INT_MAX, 267, 0, "MDT-Activation", 4, 0, 4, 0, 2076, 434, 58, 0},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2523},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2526},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2529},
    {-1, 12, 134, "MDTMode-Extension", 24, 3, 0, 0, 264, 919, 12, 0},
    {-1, 423, 479, "ImmediateMDT", 48, 5, 1, 0, 12, 879, 12, 0},
    {-1, 425, 134, "LoggedMDT", 16, 3, 1, 0, 12, 904, 12, 0},
    {-1, 427, 134, "MDTMode-Extension", 24, 3, 0, 0, 264, 919, 12, 0},
    {1, 2, 3, "MDTMode", 52, 3, 2, 4, 2076, 922, 13, 0},
    {INT_MAX, 423, 0, "MDT-Activation", 4, 0, 4, 0, 2076, 434, 58, 0},
    {1, 425, 468, "AreaScopeOfMDT", 16, 4, 2, 4, 2076, 722, 13, 0},
    {1, 427, 3, "MDTMode", 52, 3, 2, 4, 2076, 922, 13, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2532},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2535},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2538},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 925, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1402, 18, 2541},
    {-1, 423, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 2544},
    {-1, 425, 0, "EARFCN", 4, 0, 4, 0, 204, 0, 0, 2546},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2548},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2551},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2554},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 928, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1409, 18, 2557},
    {-1, 12, 505, "MBSFN-ResultToLogInfo", 12, 3, 1, 0, 12, 931, 12, 0},
    {8, 12, 0, "MBSFN-ResultToLog", 4, 12, 2, 4, 216, 1411, 18, 2560},
    {4, 269, 0, "M-TMSI", 2, 0, 2, 2, 216, 0, 21, 2562},
    {INT_MAX, 423, 0, NULL, 4, 0, 4, 0, 2076, 443, 58, 0},
    {-1, 425, 0, NULL, 4, 0, 4, 0, 204, 0, 0, 2565},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2568},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2571},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2574},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 934, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1419, 18, 2577},
    {INT_MAX, 267, 0, "OverloadAction", 4, 0, 4, 0, 2076, 453, 58, 0},
    {INT_MAX, 423, 0, "OverloadAction", 4, 0, 4, 0, 2076, 453, 58, 0},
    {2, 269, 0, "Port-Number", 2, 0, 2, 2, 216, 0, 21, 2580},
    {INT_MAX, 267, 0, "ProSeDirectDiscovery", 4, 0, 4, 0, 2076, 463, 58, 0},
    {INT_MAX, 267, 0, "ProSeDirectCommunication", 4, 0, 4, 0, 2076, 463, 58, 0},
    {INT_MAX, 423, 0, "ProSeDirectDiscovery", 4, 0, 4, 0, 2076, 463, 58, 0},
    {INT_MAX, 425, 0, "ProSeDirectCommunication", 4, 0, 4, 0, 2076, 463, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2583},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2586},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2589},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 937, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1431, 18, 2592},
    {INT_MAX, 267, 0, "ReportArea", 4, 0, 4, 0, 2076, 470, 58, 0},
    {INT_MAX, 423, 0, "EventType", 4, 0, 4, 0, 2076, 310, 58, 0},
    {INT_MAX, 425, 0, "ReportArea", 4, 0, 4, 0, 2076, 470, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2595},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2598},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2601},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 940, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1439, 18, 2604},
    {-1, 269, 0, "UE-RLF-Report-Container", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 269, 0, "UE-RLF-Report-Container-for-extended-bands", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "UE-RLF-Report-Container", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 425, 0, "UE-RLF-Report-Container-for-extended-bands", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2607},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2610},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2613},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 943, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1448, 18, 2616},
    {-1, 12, 317, "RLFReportInformation", 24, 3, 1, 0, 12, 946, 12, 0},
    {-1, 269, 0, "RRC-Container", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 2619},
    {256, 425, 0, "SecurityKey", 8, 0, 2, 4, 216, 0, 3, 2621},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2623},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2626},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2629},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 949, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1457, 18, 2632},
    {INT_MAX, 267, 0, "SONInformationRequest", 4, 0, 4, 0, 2076, 476, 58, 0},
    {2, 423, 0, "ENBX2TLAs", 4, 8, 2, 4, 216, 227, 18, 2635},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2637},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2640},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2643},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 952, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1464, 18, 2646},
    {-1, 423, 186, "X2TNLConfigurationInfo", 12, 2, 1, 0, 12, 298, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2649},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2652},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2655},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 955, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1470, 18, 2658},
    {-1, 12, 519, "SONInformationReply", 20, 2, 1, 0, 12, 958, 12, 0},
    {-1, 423, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2661},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2664},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2667},
    {-1, 12, 134, "SONInformation-Extension", 24, 3, 0, 0, 264, 960, 12, 0},
    {INT_MAX, 423, 0, "SONInformationRequest", 4, 0, 4, 0, 2076, 476, 58, 0},
    {-1, 425, 519, "SONInformationReply", 20, 2, 1, 0, 12, 958, 12, 0},
    {-1, 427, 134, "SONInformation-Extension", 24, 3, 0, 0, 264, 960, 12, 0},
    {1, 2, 3, "SONInformation", 28, 3, 2, 4, 2076, 963, 13, 0},
    {-1, 423, 317, "RLFReportInformation", 24, 3, 1, 0, 12, 946, 12, 0},
    {-1, 423, 134, "Global-ENB-ID", 24, 3, 1, 0, 12, 181, 12, 0},
    {-1, 425, 134, "TAI", 16, 3, 1, 0, 12, 261, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2670},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2673},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2676},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 966, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1487, 18, 2679},
    {-1, 12, 134, "TargeteNB-ID", 48, 3, 1, 0, 12, 969, 12, 0},
    {-1, 423, 134, "Global-ENB-ID", 24, 3, 1, 0, 12, 181, 12, 0},
    {-1, 425, 134, "TAI", 16, 3, 1, 0, 12, 261, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2682},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2685},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2688},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 972, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1495, 18, 2691},
    {-1, 12, 134, "SourceeNB-ID", 48, 3, 1, 0, 8, 975, 12, 0},
    {-1, 423, 134, "TargeteNB-ID", 48, 3, 1, 0, 12, 969, 12, 0},
    {-1, 425, 134, "SourceeNB-ID", 48, 3, 1, 0, 8, 975, 12, 0},
    {1, 427, 3, "SONInformation", 28, 3, 2, 4, 2076, 963, 13, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2694},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2697},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2700},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 978, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1504, 18, 2703},
    {-1, 313, 0, "StratumLevel", 4, 0, 4, 0, 204, 0, 0, 2706},
    {-1, 423, 0, "StratumLevel", 4, 0, 4, 0, 204, 0, 0, 2709},
    {-1, 425, 134, "ListeningSubframePattern", 16, 3, 1, 0, 12, 898, 12, 0},
    {256, 427, 0, "ECGI-List", 4, 20, 2, 4, 216, 150, 18, 2711},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2713},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2716},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2719},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 981, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1513, 18, 2722},
    {-1, 423, 0, "RRC-Container", 8, 0, 4, 4, 8, 0, 20, 0},
    {256, 425, 0, "E-RABInformationList", 4, 24, 2, 4, 216, 1212, 18, 2725},
    {-1, 427, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 429, 0, "SubscriberProfileIDforRFP", 2, 0, 2, 0, 200, 0, 55, 2727},
    {16, 431, 0, "UE-HistoryInformation", 4, 48, 2, 4, 216, 1336, 18, 2729},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2732},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2735},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2738},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 984, 12, 0},
    {65535, 433, 0, NULL, 4, 24, 2, 4, 216, 1523, 18, 2741},
    {32, 423, 0, "ServedPLMNs", 4, 8, 2, 4, 216, 599, 18, 2744},
    {65535, 425, 0, "ServedGroupIDs", 4, 4, 2, 4, 216, 1189, 18, 2747},
    {256, 427, 0, "ServedMMECs", 4, 4, 2, 4, 216, 1190, 18, 2750},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2753},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2756},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2759},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 987, 12, 0},
    {65535, 429, 0, NULL, 4, 24, 2, 4, 216, 1531, 18, 2762},
    {-1, 12, 146, "ServedGUMMEIsItem", 20, 4, 1, 0, 12, 990, 12, 0},
    {65535, 12, 0, "ServedGroupIDs", 4, 4, 2, 4, 216, 1189, 18, 2765},
    {256, 12, 0, "ServedMMECs", 4, 4, 2, 4, 216, 1190, 18, 2767},
    {32, 12, 0, "ServedPLMNs", 4, 8, 2, 4, 216, 599, 18, 2769},
    {2, 423, 0, "TAC", 2, 0, 2, 2, 216, 0, 21, 2771},
    {6, 425, 0, "BPLMNs", 4, 8, 2, 4, 216, 599, 18, 2773},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2775},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2778},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2781},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 994, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1542, 18, 2784},
    {-1, 12, 134, "SupportedTAs-Item", 16, 3, 1, 0, 12, 997, 12, 0},
    {INT_MAX, 267, 0, "SynchronisationStatus", 4, 0, 4, 0, 2076, 485, 58, 0},
    {-1, 423, 0, "StratumLevel", 4, 0, 4, 0, 204, 0, 0, 2787},
    {INT_MAX, 425, 0, "SynchronisationStatus", 4, 0, 4, 0, 2076, 485, 58, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2789},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2792},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2795},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1000, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1551, 18, 2798},
    {1, 423, 0, "MME-Code", 2, 0, 2, 2, 216, 0, 21, 2801},
    {4, 425, 0, "M-TMSI", 2, 0, 2, 2, 216, 0, 21, 2803},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2805},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2808},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2811},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1003, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1558, 18, 2814},
    {8, 12, 0, "TAIListforMDT", 4, 16, 2, 4, 216, 218, 18, 2817},
    {65535, 12, 0, "TAIListforWarning", 4, 16, 2, 4, 216, 218, 18, 2819},
    {-1, 423, 134, "TAI", 16, 3, 1, 0, 12, 261, 12, 0},
    {65535, 425, 0, "CompletedCellinTAI", 4, 28, 2, 4, 216, 1587, 18, 2822},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2825},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2828},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2831},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1006, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1567, 18, 2834},
    {-1, 12, 134, "TAI-Broadcast-Item", 28, 3, 1, 0, 12, 1009, 12, 0},
    {65535, 12, 0, "TAI-Broadcast", 4, 28, 2, 4, 216, 1569, 18, 2837},
    {-1, 423, 134, "TAI", 16, 3, 1, 0, 12, 261, 12, 0},
    {65535, 425, 0, "CancelledCellinTAI", 4, 32, 2, 4, 216, 1061, 18, 2839},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2841},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2844},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2847},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1012, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1576, 18, 2850},
    {-1, 12, 134, "TAI-Cancelled-Item", 28, 3, 1, 0, 12, 1015, 12, 0},
    {65535, 12, 0, "TAI-Cancelled", 4, 28, 2, 4, 216, 1578, 18, 2853},
    {8, 12, 0, "TAListforMDT", 4, 4, 2, 4, 216, 600, 18, 2855},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2857},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2860},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2863},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1018, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1585, 18, 2866},
    {-1, 12, 186, "CompletedCellinTAI-Item", 28, 2, 1, 0, 12, 1021, 12, 0},
    {65535, 12, 0, "CompletedCellinTAI", 4, 28, 2, 4, 216, 1587, 18, 2869},
    {-1, 423, 134, "TargeteNB-ID", 48, 3, 1, 0, 12, 969, 12, 0},
    {-1, 425, 439, "TargetRNC-ID", 32, 5, 1, 0, 12, 634, 12, 0},
    {-1, 427, 232, "CGI", 24, 5, 1, 0, 12, 775, 12, 0},
    {-1, 423, 0, "RRC-Container", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2871},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2874},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2877},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1023, 12, 0},
    {65535, 425, 0, NULL, 4, 24, 2, 4, 216, 1596, 18, 2880},
    {160, 423, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 2883},
    {4, 425, 0, "GTP-TEID", 2, 0, 2, 2, 216, 0, 21, 2885},
    {INT_MAX, 267, 0, "TraceDepth", 4, 0, 4, 0, 2076, 492, 58, 0},
    {8, 423, 0, "E-UTRAN-Trace-ID", 2, 0, 2, 2, 216, 0, 21, 2887},
    {8, 425, 0, "InterfacesToTrace", 8, 0, 2, 4, 216, 0, 3, 2889},
    {INT_MAX, 427, 0, "TraceDepth", 4, 0, 4, 0, 2076, 492, 58, 0},
    {160, 429, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 2891},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2893},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2896},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2899},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1026, 12, 0},
    {65535, 431, 0, NULL, 4, 24, 2, 4, 216, 1608, 18, 2902},
    {160, 423, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 2905},
    {2, 425, 0, "Port-Number", 2, 0, 2, 2, 216, 0, 21, 2907},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2909},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2912},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2915},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1029, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1615, 18, 2918},
    {-1, 423, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 2921},
    {-1, 425, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 2923},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2925},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2928},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2931},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1032, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1622, 18, 2934},
    {-1, 423, 0, "MME-UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 2937},
    {-1, 425, 0, "ENB-UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 2939},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2941},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2944},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2947},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1035, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1629, 18, 2950},
    {-1, 12, 134, "UE-S1AP-ID-pair", 16, 3, 1, 0, 12, 1038, 12, 0},
    {-1, 423, 134, "UE-S1AP-ID-pair", 16, 3, 1, 0, 12, 1038, 12, 0},
    {-1, 425, 0, "MME-UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 2953},
    {-1, 423, 0, "MME-UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 2955},
    {-1, 425, 0, "ENB-UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 2957},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2959},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2962},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2965},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1041, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1639, 18, 2968},
    {16, 12, 0, "UE-HistoryInformation", 4, 48, 2, 4, 216, 1336, 18, 2971},
    {-1, 423, 134, "S-TMSI", 16, 3, 1, 0, 12, 258, 12, 0},
    {8, 425, 0, "IMSI", 2, 0, 2, 2, 216, 0, 21, 2973},
    {16, 423, 0, "EncryptionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 2975},
    {16, 425, 0, "IntegrityProtectionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 2977},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2979},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2982},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2985},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1044, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1649, 18, 2988},
    {-1, 423, 134, "EUTRAN-CGI", 20, 3, 1, 0, 12, 192, 12, 0},
    {-1, 425, 134, "TAI", 16, 3, 1, 0, 12, 261, 12, 0},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 2991},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 2994},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2997},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1047, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1656, 18, 3000},
    {65535, 423, 0, "ECGIList", 4, 20, 2, 4, 216, 150, 18, 3003},
    {65535, 425, 0, "TAIListforWarning", 4, 16, 2, 4, 216, 218, 18, 3005},
    {65535, 427, 0, "EmergencyAreaIDList", 4, 8, 2, 4, 216, 1150, 18, 3007},
    {160, 423, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 3009},
    {16, 425, 0, "ENBX2GTPTLAs", 4, 8, 2, 4, 216, 227, 18, 3011},
    {-1, 423, 0, "ProtocolExtensionID", 2, 0, 2, 0, 200, 0, 55, 3014},
    {2, 425, 0, "Criticality", 4, 0, 4, 0, 88, 174, 58, 3017},
    {-1, 427, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 3020},
    {-1, 12, 134, NULL, 24, 3, 0, 0, 264, 1050, 12, 0},
    {65535, 427, 0, NULL, 4, 24, 2, 4, 216, 1666, 18, 3023},
    {-1, 12, 331, "ENBX2ExtTLA", 20, 3, 1, 0, 12, 1053, 12, 0},
    {16, 12, 0, "ENBX2GTPTLAs", 4, 8, 2, 4, 216, 227, 18, 3026},
    {2, 267, 0, "Presence", 4, 0, 4, 0, 24, 503, 58, 0},
    {-1, 2, 0, "S1AP-PROTOCOL-IES", 16, 4, 0, 0, 8, 1056, 49, 0},
    {-1, 2, 0, "S1AP-PROTOCOL-EXTENSION", 16, 4, 0, 0, 8, 1060, 49, 0},
    {-1, 313, 0, NULL, 4, 0, 4, 0, 8, 0, 0, 0},
    {-1, 2, 0, "S1AP-PRIVATE-IES", 28, 5, 0, 0, 8, 1064, 49, 0}
};

static const struct ConstraintEntry _econstraintarray[] = {
    {5, 14, (void *)_v1466},
    {0, 6, (void *)2},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)5},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)8},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)11},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)14},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)17},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)20},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)23},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)26},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)29},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)32},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)35},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)38},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)41},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)44},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)47},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)50},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)53},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1505},
    {0, 6, (void *)56},
    {0, 1, (void *)&_v609},
    {5, 14, (void *)_v1210},
    {0, 6, (void *)59},
    {0, 1, (void *)&_v611},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)62},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1507},
    {0, 6, (void *)65},
    {0, 1, (void *)&_v615},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)68},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)71},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1531},
    {0, 3, (void *)_v1531},
    {5, 14, (void *)_v643},
    {0, 6, (void *)76},
    {0, 20, (void *)0x4d},
    {0, 3, (void *)_v643},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)80},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v627},
    {0, 3, (void *)_v627},
    {5, 14, (void *)_v629},
    {0, 3, (void *)_v629},
    {5, 14, (void *)_v630},
    {0, 6, (void *)87},
    {0, 1, (void *)&_v631},
    {5, 14, (void *)_v633},
    {0, 6, (void *)90},
    {0, 3, (void *)_v633},
    {5, 14, (void *)_v1507},
    {0, 6, (void *)93},
    {0, 1, (void *)&_v635},
    {5, 14, (void *)_v1554},
    {0, 6, (void *)96},
    {0, 3, (void *)_v1554},
    {5, 14, (void *)_v1538},
    {0, 6, (void *)99},
    {0, 1, (void *)&_v639},
    {5, 14, (void *)_v640},
    {0, 6, (void *)102},
    {0, 1, (void *)&_v641},
    {5, 14, (void *)_v643},
    {0, 6, (void *)105},
    {0, 20, (void *)0x6a},
    {0, 3, (void *)_v643},
    {5, 14, (void *)_v1530},
    {0, 3, (void *)_v1530},
    {5, 14, (void *)_v1551},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v649},
    {0, 6, (void *)113},
    {0, 3, (void *)_v649},
    {5, 14, (void *)_v1395},
    {0, 3, (void *)_v1395},
    {5, 14, (void *)_v1364},
    {0, 3, (void *)_v1364},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)120},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1395},
    {0, 3, (void *)_v1395},
    {5, 14, (void *)_v1421},
    {0, 6, (void *)125},
    {0, 1, (void *)&_v659},
    {5, 14, (void *)_v1538},
    {0, 6, (void *)128},
    {0, 1, (void *)&_v661},
    {5, 14, (void *)_v1496},
    {0, 6, (void *)131},
    {0, 3, (void *)_v1496},
    {5, 14, (void *)_v1466},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)136},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v669},
    {0, 3, (void *)_v669},
    {5, 14, (void *)_v1548},
    {0, 6, (void *)141},
    {0, 20, (void *)0x8e},
    {0, 3, (void *)_v1548},
    {5, 14, (void *)_v1507},
    {0, 6, (void *)145},
    {0, 1, (void *)&_v673},
    {5, 14, (void *)_v675},
    {0, 3, (void *)_v675},
    {5, 14, (void *)_v677},
    {0, 6, (void *)150},
    {0, 3, (void *)_v677},
    {5, 14, (void *)_v678},
    {0, 6, (void *)153},
    {0, 1, (void *)&_v679},
    {5, 14, (void *)_v1513},
    {0, 6, (void *)156},
    {0, 1, (void *)&_v681},
    {5, 14, (void *)_v682},
    {0, 6, (void *)159},
    {0, 1, (void *)&_v683},
    {5, 14, (void *)_v685},
    {0, 6, (void *)162},
    {0, 3, (void *)_v685},
    {5, 14, (void *)_v1554},
    {0, 6, (void *)165},
    {0, 3, (void *)_v1554},
    {5, 14, (void *)_v1425},
    {0, 6, (void *)168},
    {0, 3, (void *)_v1425},
    {5, 14, (void *)_v1395},
    {0, 3, (void *)_v1395},
    {5, 14, (void *)_v1395},
    {1, 11, (void *)170},
    {0, 16, (void *)0x12f},
    {0, 17, (void *)0xb000af},
    {0, 18, (void *)0x12f0131},
    {0, 16, (void *)0x130},
    {0, 17, (void *)0xb300b2},
    {0, 18, (void *)0x12f0131},
    {0, 16, (void *)0x12c},
    {5, 14, (void *)_v1395},
    {1, 11, (void *)170},
    {0, 16, (void *)0x12f},
    {0, 17, (void *)0xb900b8},
    {0, 18, (void *)0x12f0134},
    {0, 16, (void *)0x130},
    {0, 17, (void *)0xbc00bb},
    {0, 18, (void *)0x12f0134},
    {0, 16, (void *)0x12d},
    {5, 14, (void *)_v1395},
    {1, 11, (void *)170},
    {0, 16, (void *)0x12f},
    {0, 17, (void *)0xc200c1},
    {0, 18, (void *)0x12f0137},
    {0, 16, (void *)0x130},
    {0, 17, (void *)0xc500c4},
    {0, 18, (void *)0x12f0137},
    {0, 16, (void *)0x12e},
    {5, 14, (void *)_v1551},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x30420},
    {0, 17, (void *)0xcd00cc},
    {0, 19, (void *)0x420013a},
    {0, 16, (void *)0x30421},
    {0, 17, (void *)0xd000cf},
    {0, 19, (void *)0x420013a},
    {0, 16, (void *)0x30422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)211},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x40420},
    {0, 17, (void *)0xd900d8},
    {0, 19, (void *)0x420013d},
    {0, 16, (void *)0x40421},
    {0, 17, (void *)0xdc00db},
    {0, 19, (void *)0x420013d},
    {0, 16, (void *)0x40422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)223},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x50420},
    {0, 17, (void *)0xe500e4},
    {0, 19, (void *)0x4200140},
    {0, 16, (void *)0x50421},
    {0, 17, (void *)0xe800e7},
    {0, 19, (void *)0x4200140},
    {0, 16, (void *)0x50422},
    {5, 14, (void *)_v1297},
    {0, 20, (void *)0xeb},
    {0, 3, (void *)_v1297},
    {5, 14, (void *)_v1505},
    {0, 6, (void *)238},
    {0, 1, (void *)&_v707},
    {5, 14, (void *)_v1551},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x60424},
    {0, 17, (void *)0x10000ff},
    {0, 19, (void *)0x4240143},
    {0, 16, (void *)0x60425},
    {0, 17, (void *)0x1030102},
    {0, 19, (void *)0x4240143},
    {0, 16, (void *)0x60426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)262},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x70420},
    {0, 17, (void *)0x10c010b},
    {0, 19, (void *)0x4200146},
    {0, 16, (void *)0x70421},
    {0, 17, (void *)0x10f010e},
    {0, 19, (void *)0x4200146},
    {0, 16, (void *)0x70422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)274},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x80420},
    {0, 17, (void *)0x1180117},
    {0, 19, (void *)0x4200149},
    {0, 16, (void *)0x80421},
    {0, 17, (void *)0x11b011a},
    {0, 19, (void *)0x4200149},
    {0, 16, (void *)0x80422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)286},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x90420},
    {0, 17, (void *)0x1240123},
    {0, 19, (void *)0x420014c},
    {0, 16, (void *)0x90421},
    {0, 17, (void *)0x1270126},
    {0, 19, (void *)0x420014c},
    {0, 16, (void *)0x90422},
    {5, 14, (void *)_v1395},
    {0, 3, (void *)_v1395},
    {5, 14, (void *)_v729},
    {0, 3, (void *)_v729},
    {5, 14, (void *)_v729},
    {0, 11, (void *)299},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x740424},
    {0, 17, (void *)0x1330132},
    {0, 19, (void *)0x424014f},
    {0, 16, (void *)0x740425},
    {0, 17, (void *)0x1360135},
    {0, 19, (void *)0x424014f},
    {0, 16, (void *)0x740426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)313},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1520},
    {0, 3, (void *)_v1520},
    {5, 14, (void *)_v1520},
    {0, 3, (void *)_v1520},
    {5, 14, (void *)_v1520},
    {0, 3, (void *)_v1520},
    {5, 14, (void *)_v1520},
    {0, 3, (void *)_v1520},
    {5, 14, (void *)_v1520},
    {0, 3, (void *)_v1520},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x940424},
    {0, 17, (void *)0x1490148},
    {0, 19, (void *)0x4240156},
    {0, 16, (void *)0x940425},
    {0, 17, (void *)0x14c014b},
    {0, 19, (void *)0x4240156},
    {0, 16, (void *)0x940426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)335},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1395},
    {0, 11, (void *)297},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x8e0424},
    {0, 17, (void *)0x1570156},
    {0, 19, (void *)0x424015e},
    {0, 16, (void *)0x8e0425},
    {0, 17, (void *)0x15a0159},
    {0, 19, (void *)0x424015e},
    {0, 16, (void *)0x8e0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)349},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa0424},
    {0, 17, (void *)0x1690168},
    {0, 19, (void *)0x4240165},
    {0, 16, (void *)0xa0425},
    {0, 17, (void *)0x16c016b},
    {0, 19, (void *)0x4240165},
    {0, 16, (void *)0xa0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)367},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0xb0420},
    {0, 17, (void *)0x1750174},
    {0, 19, (void *)0x4200168},
    {0, 16, (void *)0xb0421},
    {0, 17, (void *)0x1780177},
    {0, 19, (void *)0x4200168},
    {0, 16, (void *)0xb0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)379},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0xc0420},
    {0, 17, (void *)0x1810180},
    {0, 19, (void *)0x420016b},
    {0, 16, (void *)0xc0421},
    {0, 17, (void *)0x1840183},
    {0, 19, (void *)0x420016b},
    {0, 16, (void *)0xc0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xd0424},
    {0, 17, (void *)0x1980197},
    {0, 19, (void *)0x424016e},
    {0, 16, (void *)0xd0425},
    {0, 17, (void *)0x19b019a},
    {0, 19, (void *)0x424016e},
    {0, 16, (void *)0xd0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)414},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0xe0420},
    {0, 17, (void *)0x1a401a3},
    {0, 19, (void *)0x4200171},
    {0, 16, (void *)0xe0421},
    {0, 17, (void *)0x1a701a6},
    {0, 19, (void *)0x4200171},
    {0, 16, (void *)0xe0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xf0424},
    {0, 17, (void *)0x1af01ae},
    {0, 19, (void *)0x4240174},
    {0, 16, (void *)0xf0425},
    {0, 17, (void *)0x1b201b1},
    {0, 19, (void *)0x4240174},
    {0, 16, (void *)0xf0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)437},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x100420},
    {0, 17, (void *)0x1bb01ba},
    {0, 19, (void *)0x4200177},
    {0, 16, (void *)0x100421},
    {0, 17, (void *)0x1be01bd},
    {0, 19, (void *)0x4200177},
    {0, 16, (void *)0x100422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)449},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x110420},
    {0, 17, (void *)0x1c701c6},
    {0, 19, (void *)0x420017a},
    {0, 16, (void *)0x110421},
    {0, 17, (void *)0x1ca01c9},
    {0, 19, (void *)0x420017a},
    {0, 16, (void *)0x110422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)461},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x120420},
    {0, 17, (void *)0x1d301d2},
    {0, 19, (void *)0x420017d},
    {0, 16, (void *)0x120421},
    {0, 17, (void *)0x1d601d5},
    {0, 19, (void *)0x420017d},
    {0, 16, (void *)0x120422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)473},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x130420},
    {0, 17, (void *)0x1df01de},
    {0, 19, (void *)0x4200180},
    {0, 16, (void *)0x130421},
    {0, 17, (void *)0x1e201e1},
    {0, 19, (void *)0x4200180},
    {0, 16, (void *)0x130422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x140424},
    {0, 17, (void *)0x1ee01ed},
    {0, 19, (void *)0x4240183},
    {0, 16, (void *)0x140425},
    {0, 17, (void *)0x1f101f0},
    {0, 19, (void *)0x4240183},
    {0, 16, (void *)0x140426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)500},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x150420},
    {0, 17, (void *)0x1fa01f9},
    {0, 19, (void *)0x4200186},
    {0, 16, (void *)0x150421},
    {0, 17, (void *)0x1fd01fc},
    {0, 19, (void *)0x4200186},
    {0, 16, (void *)0x150422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)512},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x160420},
    {0, 17, (void *)0x2060205},
    {0, 19, (void *)0x4200189},
    {0, 16, (void *)0x160421},
    {0, 17, (void *)0x2090208},
    {0, 19, (void *)0x4200189},
    {0, 16, (void *)0x160422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x170424},
    {0, 17, (void *)0x2150214},
    {0, 19, (void *)0x424018c},
    {0, 16, (void *)0x170425},
    {0, 17, (void *)0x2180217},
    {0, 19, (void *)0x424018c},
    {0, 16, (void *)0x170426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)539},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x180420},
    {0, 17, (void *)0x2210220},
    {0, 19, (void *)0x420018f},
    {0, 16, (void *)0x180421},
    {0, 17, (void *)0x2240223},
    {0, 19, (void *)0x420018f},
    {0, 16, (void *)0x180422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)551},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x190420},
    {0, 17, (void *)0x22d022c},
    {0, 19, (void *)0x4200192},
    {0, 16, (void *)0x190421},
    {0, 17, (void *)0x230022f},
    {0, 19, (void *)0x4200192},
    {0, 16, (void *)0x190422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)563},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x1a0420},
    {0, 17, (void *)0x2390238},
    {0, 19, (void *)0x4200195},
    {0, 16, (void *)0x1a0421},
    {0, 17, (void *)0x23c023b},
    {0, 19, (void *)0x4200195},
    {0, 16, (void *)0x1a0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)575},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x1b0420},
    {0, 17, (void *)0x2450244},
    {0, 19, (void *)0x4200198},
    {0, 16, (void *)0x1b0421},
    {0, 17, (void *)0x2480247},
    {0, 19, (void *)0x4200198},
    {0, 16, (void *)0x1b0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)587},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x1c0420},
    {0, 17, (void *)0x2510250},
    {0, 19, (void *)0x420019b},
    {0, 16, (void *)0x1c0421},
    {0, 17, (void *)0x2540253},
    {0, 19, (void *)0x420019b},
    {0, 16, (void *)0x1c0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x1d0424},
    {0, 17, (void *)0x260025f},
    {0, 19, (void *)0x424019e},
    {0, 16, (void *)0x1d0425},
    {0, 17, (void *)0x2630262},
    {0, 19, (void *)0x424019e},
    {0, 16, (void *)0x1d0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)614},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x1e0420},
    {0, 17, (void *)0x26c026b},
    {0, 19, (void *)0x42001a1},
    {0, 16, (void *)0x1e0421},
    {0, 17, (void *)0x26f026e},
    {0, 19, (void *)0x42001a1},
    {0, 16, (void *)0x1e0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)626},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x1f0420},
    {0, 17, (void *)0x2780277},
    {0, 19, (void *)0x42001a4},
    {0, 16, (void *)0x1f0421},
    {0, 17, (void *)0x27b027a},
    {0, 19, (void *)0x42001a4},
    {0, 16, (void *)0x1f0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x200424},
    {0, 17, (void *)0x2870286},
    {0, 19, (void *)0x42401a7},
    {0, 16, (void *)0x200425},
    {0, 17, (void *)0x28a0289},
    {0, 19, (void *)0x42401a7},
    {0, 16, (void *)0x200426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)653},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x210420},
    {0, 17, (void *)0x2930292},
    {0, 19, (void *)0x42001aa},
    {0, 16, (void *)0x210421},
    {0, 17, (void *)0x2960295},
    {0, 19, (void *)0x42001aa},
    {0, 16, (void *)0x210422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)665},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x220420},
    {0, 17, (void *)0x29f029e},
    {0, 19, (void *)0x42001ad},
    {0, 16, (void *)0x220421},
    {0, 17, (void *)0x2a202a1},
    {0, 19, (void *)0x42001ad},
    {0, 16, (void *)0x220422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x230424},
    {0, 17, (void *)0x2aa02a9},
    {0, 19, (void *)0x42401b0},
    {0, 16, (void *)0x230425},
    {0, 17, (void *)0x2ad02ac},
    {0, 19, (void *)0x42401b0},
    {0, 16, (void *)0x230426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)688},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x240420},
    {0, 17, (void *)0x2b602b5},
    {0, 19, (void *)0x42001b3},
    {0, 16, (void *)0x240421},
    {0, 17, (void *)0x2b902b8},
    {0, 19, (void *)0x42001b3},
    {0, 16, (void *)0x240422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)700},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x250420},
    {0, 17, (void *)0x2c202c1},
    {0, 19, (void *)0x42001b6},
    {0, 16, (void *)0x250421},
    {0, 17, (void *)0x2c502c4},
    {0, 19, (void *)0x42001b6},
    {0, 16, (void *)0x250422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x260424},
    {0, 17, (void *)0x2cd02cc},
    {0, 19, (void *)0x42401b9},
    {0, 16, (void *)0x260425},
    {0, 17, (void *)0x2d002cf},
    {0, 19, (void *)0x42401b9},
    {0, 16, (void *)0x260426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)723},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x270420},
    {0, 17, (void *)0x2d902d8},
    {0, 19, (void *)0x42001bc},
    {0, 16, (void *)0x270421},
    {0, 17, (void *)0x2dc02db},
    {0, 19, (void *)0x42001bc},
    {0, 16, (void *)0x270422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)735},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x280420},
    {0, 17, (void *)0x2e502e4},
    {0, 19, (void *)0x42001bf},
    {0, 16, (void *)0x280421},
    {0, 17, (void *)0x2e802e7},
    {0, 19, (void *)0x42001bf},
    {0, 16, (void *)0x280422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)747},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x290420},
    {0, 17, (void *)0x2f102f0},
    {0, 19, (void *)0x42001c2},
    {0, 16, (void *)0x290421},
    {0, 17, (void *)0x2f402f3},
    {0, 19, (void *)0x42001c2},
    {0, 16, (void *)0x290422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x2a0424},
    {0, 17, (void *)0x2fc02fb},
    {0, 19, (void *)0x42401c5},
    {0, 16, (void *)0x2a0425},
    {0, 17, (void *)0x2ff02fe},
    {0, 19, (void *)0x42401c5},
    {0, 16, (void *)0x2a0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)770},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x2b0420},
    {0, 17, (void *)0x3080307},
    {0, 19, (void *)0x42001c8},
    {0, 16, (void *)0x2b0421},
    {0, 17, (void *)0x30b030a},
    {0, 19, (void *)0x42001c8},
    {0, 16, (void *)0x2b0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)782},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x2c0420},
    {0, 17, (void *)0x3140313},
    {0, 19, (void *)0x42001cb},
    {0, 16, (void *)0x2c0421},
    {0, 17, (void *)0x3170316},
    {0, 19, (void *)0x42001cb},
    {0, 16, (void *)0x2c0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)794},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x2d0420},
    {0, 17, (void *)0x320031f},
    {0, 19, (void *)0x42001ce},
    {0, 16, (void *)0x2d0421},
    {0, 17, (void *)0x3230322},
    {0, 19, (void *)0x42001ce},
    {0, 16, (void *)0x2d0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x2e0424},
    {0, 17, (void *)0x32f032e},
    {0, 19, (void *)0x42401d1},
    {0, 16, (void *)0x2e0425},
    {0, 17, (void *)0x3320331},
    {0, 19, (void *)0x42401d1},
    {0, 16, (void *)0x2e0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)821},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x2f0420},
    {0, 17, (void *)0x33b033a},
    {0, 19, (void *)0x42001d4},
    {0, 16, (void *)0x2f0421},
    {0, 17, (void *)0x33e033d},
    {0, 19, (void *)0x42001d4},
    {0, 16, (void *)0x2f0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)833},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x300420},
    {0, 17, (void *)0x3470346},
    {0, 19, (void *)0x42001d7},
    {0, 16, (void *)0x300421},
    {0, 17, (void *)0x34a0349},
    {0, 19, (void *)0x42001d7},
    {0, 16, (void *)0x300422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x310424},
    {0, 17, (void *)0x3560355},
    {0, 19, (void *)0x42401da},
    {0, 16, (void *)0x310425},
    {0, 17, (void *)0x3590358},
    {0, 19, (void *)0x42401da},
    {0, 16, (void *)0x310426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)860},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x320420},
    {0, 17, (void *)0x3620361},
    {0, 19, (void *)0x42001dd},
    {0, 16, (void *)0x320421},
    {0, 17, (void *)0x3650364},
    {0, 19, (void *)0x42001dd},
    {0, 16, (void *)0x320422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)872},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x330420},
    {0, 17, (void *)0x36e036d},
    {0, 19, (void *)0x42001e0},
    {0, 16, (void *)0x330421},
    {0, 17, (void *)0x3710370},
    {0, 19, (void *)0x42001e0},
    {0, 16, (void *)0x330422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)884},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x340420},
    {0, 17, (void *)0x37a0379},
    {0, 19, (void *)0x42001e3},
    {0, 16, (void *)0x340421},
    {0, 17, (void *)0x37d037c},
    {0, 19, (void *)0x42001e3},
    {0, 16, (void *)0x340422},
    {5, 14, (void *)_v1331},
    {0, 6, (void *)896},
    {0, 1, (void *)&_v888},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1513},
    {0, 6, (void *)901},
    {0, 1, (void *)&_v891},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)900},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb30424},
    {0, 17, (void *)0x38f038e},
    {0, 19, (void *)0x42401e6},
    {0, 16, (void *)0xb30425},
    {0, 17, (void *)0x3920391},
    {0, 19, (void *)0x42401e6},
    {0, 16, (void *)0xb30426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)917},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x350424},
    {0, 17, (void *)0x39b039a},
    {0, 19, (void *)0x42401e9},
    {0, 16, (void *)0x350425},
    {0, 17, (void *)0x39e039d},
    {0, 19, (void *)0x42401e9},
    {0, 16, (void *)0x350426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)929},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x360420},
    {0, 17, (void *)0x3a703a6},
    {0, 19, (void *)0x42001ec},
    {0, 16, (void *)0x360421},
    {0, 17, (void *)0x3aa03a9},
    {0, 19, (void *)0x42001ec},
    {0, 16, (void *)0x360422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)941},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x370420},
    {0, 17, (void *)0x3b303b2},
    {0, 19, (void *)0x42001ef},
    {0, 16, (void *)0x370421},
    {0, 17, (void *)0x3b603b5},
    {0, 19, (void *)0x42001ef},
    {0, 16, (void *)0x370422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)953},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x380420},
    {0, 17, (void *)0x3bf03be},
    {0, 19, (void *)0x42001f2},
    {0, 16, (void *)0x380421},
    {0, 17, (void *)0x3c203c1},
    {0, 19, (void *)0x42001f2},
    {0, 16, (void *)0x380422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)965},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x390420},
    {0, 17, (void *)0x3cb03ca},
    {0, 19, (void *)0x42001f5},
    {0, 16, (void *)0x390421},
    {0, 17, (void *)0x3ce03cd},
    {0, 19, (void *)0x42001f5},
    {0, 16, (void *)0x390422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)977},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x3a0420},
    {0, 17, (void *)0x3d703d6},
    {0, 19, (void *)0x42001f8},
    {0, 16, (void *)0x3a0421},
    {0, 17, (void *)0x3da03d9},
    {0, 19, (void *)0x42001f8},
    {0, 16, (void *)0x3a0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)989},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x3b0420},
    {0, 17, (void *)0x3e303e2},
    {0, 19, (void *)0x42001fb},
    {0, 16, (void *)0x3b0421},
    {0, 17, (void *)0x3e603e5},
    {0, 19, (void *)0x42001fb},
    {0, 16, (void *)0x3b0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1001},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x3c0420},
    {0, 17, (void *)0x3ef03ee},
    {0, 19, (void *)0x42001fe},
    {0, 16, (void *)0x3c0421},
    {0, 17, (void *)0x3f203f1},
    {0, 19, (void *)0x42001fe},
    {0, 16, (void *)0x3c0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1013},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x3d0420},
    {0, 17, (void *)0x3fb03fa},
    {0, 19, (void *)0x4200201},
    {0, 16, (void *)0x3d0421},
    {0, 17, (void *)0x3fe03fd},
    {0, 19, (void *)0x4200201},
    {0, 16, (void *)0x3d0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1025},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x3e0420},
    {0, 17, (void *)0x4070406},
    {0, 19, (void *)0x4200204},
    {0, 16, (void *)0x3e0421},
    {0, 17, (void *)0x40a0409},
    {0, 19, (void *)0x4200204},
    {0, 16, (void *)0x3e0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1037},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x3f0420},
    {0, 17, (void *)0x4130412},
    {0, 19, (void *)0x4200207},
    {0, 16, (void *)0x3f0421},
    {0, 17, (void *)0x4160415},
    {0, 19, (void *)0x4200207},
    {0, 16, (void *)0x3f0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1049},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x400420},
    {0, 17, (void *)0x41f041e},
    {0, 19, (void *)0x420020a},
    {0, 16, (void *)0x400421},
    {0, 17, (void *)0x4220421},
    {0, 19, (void *)0x420020a},
    {0, 16, (void *)0x400422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1061},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x410420},
    {0, 17, (void *)0x42b042a},
    {0, 19, (void *)0x420020d},
    {0, 16, (void *)0x410421},
    {0, 17, (void *)0x42e042d},
    {0, 19, (void *)0x420020d},
    {0, 16, (void *)0x410422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1073},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x420420},
    {0, 17, (void *)0x4370436},
    {0, 19, (void *)0x4200210},
    {0, 16, (void *)0x420421},
    {0, 17, (void *)0x43a0439},
    {0, 19, (void *)0x4200210},
    {0, 16, (void *)0x420422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1085},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)1088},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x430420},
    {0, 17, (void *)0x4460445},
    {0, 19, (void *)0x4200213},
    {0, 16, (void *)0x430421},
    {0, 17, (void *)0x4490448},
    {0, 19, (void *)0x4200213},
    {0, 16, (void *)0x430422},
    {5, 14, (void *)_v1466},
    {0, 11, (void *)1087},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x440420},
    {0, 17, (void *)0x4510450},
    {0, 19, (void *)0x4200216},
    {0, 16, (void *)0x440421},
    {0, 17, (void *)0x4540453},
    {0, 19, (void *)0x4200216},
    {0, 16, (void *)0x440422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1111},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x450420},
    {0, 17, (void *)0x45d045c},
    {0, 19, (void *)0x4200219},
    {0, 16, (void *)0x450421},
    {0, 17, (void *)0x460045f},
    {0, 19, (void *)0x4200219},
    {0, 16, (void *)0x450422},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x460420},
    {0, 17, (void *)0x4660465},
    {0, 19, (void *)0x420021c},
    {0, 16, (void *)0x460421},
    {0, 17, (void *)0x4690468},
    {0, 19, (void *)0x420021c},
    {0, 16, (void *)0x460422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1132},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x470420},
    {0, 17, (void *)0x4720471},
    {0, 19, (void *)0x420021f},
    {0, 16, (void *)0x470421},
    {0, 17, (void *)0x4750474},
    {0, 19, (void *)0x420021f},
    {0, 16, (void *)0x470422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1144},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x480420},
    {0, 17, (void *)0x47e047d},
    {0, 19, (void *)0x4200222},
    {0, 16, (void *)0x480421},
    {0, 17, (void *)0x4810480},
    {0, 19, (void *)0x4200222},
    {0, 16, (void *)0x480422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1156},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x490420},
    {0, 17, (void *)0x48a0489},
    {0, 19, (void *)0x4200225},
    {0, 16, (void *)0x490421},
    {0, 17, (void *)0x48d048c},
    {0, 19, (void *)0x4200225},
    {0, 16, (void *)0x490422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1168},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x4a0420},
    {0, 17, (void *)0x4960495},
    {0, 19, (void *)0x4200228},
    {0, 16, (void *)0x4a0421},
    {0, 17, (void *)0x4990498},
    {0, 19, (void *)0x4200228},
    {0, 16, (void *)0x4a0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1180},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x4b0420},
    {0, 17, (void *)0x4a204a1},
    {0, 19, (void *)0x420022b},
    {0, 16, (void *)0x4b0421},
    {0, 17, (void *)0x4a504a4},
    {0, 19, (void *)0x420022b},
    {0, 16, (void *)0x4b0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1192},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x4c0420},
    {0, 17, (void *)0x4ae04ad},
    {0, 19, (void *)0x420022e},
    {0, 16, (void *)0x4c0421},
    {0, 17, (void *)0x4b104b0},
    {0, 19, (void *)0x420022e},
    {0, 16, (void *)0x4c0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1204},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x4d0420},
    {0, 17, (void *)0x4ba04b9},
    {0, 19, (void *)0x4200231},
    {0, 16, (void *)0x4d0421},
    {0, 17, (void *)0x4bd04bc},
    {0, 19, (void *)0x4200231},
    {0, 16, (void *)0x4d0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1216},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x4e0420},
    {0, 17, (void *)0x4c604c5},
    {0, 19, (void *)0x4200234},
    {0, 16, (void *)0x4e0421},
    {0, 17, (void *)0x4c904c8},
    {0, 19, (void *)0x4200234},
    {0, 16, (void *)0x4e0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1228},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x4f0420},
    {0, 17, (void *)0x4d204d1},
    {0, 19, (void *)0x4200237},
    {0, 16, (void *)0x4f0421},
    {0, 17, (void *)0x4d504d4},
    {0, 19, (void *)0x4200237},
    {0, 16, (void *)0x4f0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1240},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x500420},
    {0, 17, (void *)0x4de04dd},
    {0, 19, (void *)0x420023a},
    {0, 16, (void *)0x500421},
    {0, 17, (void *)0x4e104e0},
    {0, 19, (void *)0x420023a},
    {0, 16, (void *)0x500422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1252},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x510420},
    {0, 17, (void *)0x4ea04e9},
    {0, 19, (void *)0x420023d},
    {0, 16, (void *)0x510421},
    {0, 17, (void *)0x4ed04ec},
    {0, 19, (void *)0x420023d},
    {0, 16, (void *)0x510422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1264},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x520420},
    {0, 17, (void *)0x4f604f5},
    {0, 19, (void *)0x4200240},
    {0, 16, (void *)0x520421},
    {0, 17, (void *)0x4f904f8},
    {0, 19, (void *)0x4200240},
    {0, 16, (void *)0x520422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1276},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x530420},
    {0, 17, (void *)0x5020501},
    {0, 19, (void *)0x4200243},
    {0, 16, (void *)0x530421},
    {0, 17, (void *)0x5050504},
    {0, 19, (void *)0x4200243},
    {0, 16, (void *)0x530422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1288},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x540420},
    {0, 17, (void *)0x50e050d},
    {0, 19, (void *)0x4200246},
    {0, 16, (void *)0x540421},
    {0, 17, (void *)0x5110510},
    {0, 19, (void *)0x4200246},
    {0, 16, (void *)0x540422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1300},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x550420},
    {0, 17, (void *)0x51a0519},
    {0, 19, (void *)0x4200249},
    {0, 16, (void *)0x550421},
    {0, 17, (void *)0x51d051c},
    {0, 19, (void *)0x4200249},
    {0, 16, (void *)0x550422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1312},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x560420},
    {0, 17, (void *)0x5260525},
    {0, 19, (void *)0x420024c},
    {0, 16, (void *)0x560421},
    {0, 17, (void *)0x5290528},
    {0, 19, (void *)0x420024c},
    {0, 16, (void *)0x560422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1324},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x570420},
    {0, 17, (void *)0x5320531},
    {0, 19, (void *)0x420024f},
    {0, 16, (void *)0x570421},
    {0, 17, (void *)0x5350534},
    {0, 19, (void *)0x420024f},
    {0, 16, (void *)0x570422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1336},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x580420},
    {0, 17, (void *)0x53e053d},
    {0, 19, (void *)0x4200252},
    {0, 16, (void *)0x580421},
    {0, 17, (void *)0x5410540},
    {0, 19, (void *)0x4200252},
    {0, 16, (void *)0x580422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1348},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x590420},
    {0, 17, (void *)0x54a0549},
    {0, 19, (void *)0x4200255},
    {0, 16, (void *)0x590421},
    {0, 17, (void *)0x54d054c},
    {0, 19, (void *)0x4200255},
    {0, 16, (void *)0x590422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1360},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x5a0420},
    {0, 17, (void *)0x5560555},
    {0, 19, (void *)0x4200258},
    {0, 16, (void *)0x5a0421},
    {0, 17, (void *)0x5590558},
    {0, 19, (void *)0x4200258},
    {0, 16, (void *)0x5a0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1372},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x5b0420},
    {0, 17, (void *)0x5620561},
    {0, 19, (void *)0x420025b},
    {0, 16, (void *)0x5b0421},
    {0, 17, (void *)0x5650564},
    {0, 19, (void *)0x420025b},
    {0, 16, (void *)0x5b0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1384},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x5c0420},
    {0, 17, (void *)0x56e056d},
    {0, 19, (void *)0x420025e},
    {0, 16, (void *)0x5c0421},
    {0, 17, (void *)0x5710570},
    {0, 19, (void *)0x420025e},
    {0, 16, (void *)0x5c0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1396},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x5d0420},
    {0, 17, (void *)0x57a0579},
    {0, 19, (void *)0x4200261},
    {0, 16, (void *)0x5d0421},
    {0, 17, (void *)0x57d057c},
    {0, 19, (void *)0x4200261},
    {0, 16, (void *)0x5d0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1408},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x5e0420},
    {0, 17, (void *)0x5860585},
    {0, 19, (void *)0x4200264},
    {0, 16, (void *)0x5e0421},
    {0, 17, (void *)0x5890588},
    {0, 19, (void *)0x4200264},
    {0, 16, (void *)0x5e0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1420},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x5f0420},
    {0, 17, (void *)0x5920591},
    {0, 19, (void *)0x4200267},
    {0, 16, (void *)0x5f0421},
    {0, 17, (void *)0x5950594},
    {0, 19, (void *)0x4200267},
    {0, 16, (void *)0x5f0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1432},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x600420},
    {0, 17, (void *)0x59e059d},
    {0, 19, (void *)0x420026a},
    {0, 16, (void *)0x600421},
    {0, 17, (void *)0x5a105a0},
    {0, 19, (void *)0x420026a},
    {0, 16, (void *)0x600422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1444},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1513},
    {0, 6, (void *)1447},
    {0, 1, (void *)&_v1029},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)1446},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x980424},
    {0, 17, (void *)0x5b105b0},
    {0, 19, (void *)0x424026d},
    {0, 16, (void *)0x980425},
    {0, 17, (void *)0x5b405b3},
    {0, 19, (void *)0x424026d},
    {0, 16, (void *)0x980426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1463},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1477},
    {0, 6, (void *)1466},
    {0, 1, (void *)&_v1036},
    {5, 14, (void *)_v1513},
    {0, 6, (void *)1469},
    {0, 1, (void *)&_v1038},
    {5, 14, (void *)_v1477},
    {0, 11, (void *)1465},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)1468},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x870424},
    {0, 17, (void *)0x5c705c6},
    {0, 19, (void *)0x4240270},
    {0, 16, (void *)0x870425},
    {0, 17, (void *)0x5ca05c9},
    {0, 19, (void *)0x4240270},
    {0, 16, (void *)0x870426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1485},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1364},
    {0, 3, (void *)_v1364},
    {5, 14, (void *)_v1050},
    {0, 3, (void *)_v1050},
    {5, 14, (void *)_v1477},
    {0, 11, (void *)1465},
    {5, 14, (void *)_v1364},
    {0, 11, (void *)1487},
    {5, 14, (void *)_v1050},
    {0, 11, (void *)1489},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb90424},
    {0, 17, (void *)0x5dd05dc},
    {0, 19, (void *)0x4240277},
    {0, 16, (void *)0xb90425},
    {0, 17, (void *)0x5e005df},
    {0, 19, (void *)0x4240277},
    {0, 16, (void *)0xb90426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1507},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1538},
    {0, 6, (void *)1510},
    {0, 1, (void *)&_v1055},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa60424},
    {0, 17, (void *)0x5ec05eb},
    {0, 19, (void *)0x4240282},
    {0, 16, (void *)0xa60425},
    {0, 17, (void *)0x5ef05ee},
    {0, 19, (void *)0x4240282},
    {0, 16, (void *)0xa60426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1522},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x610420},
    {0, 17, (void *)0x5f805f7},
    {0, 19, (void *)0x4200288},
    {0, 16, (void *)0x610421},
    {0, 17, (void *)0x5fb05fa},
    {0, 19, (void *)0x4200288},
    {0, 16, (void *)0x610422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1534},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x620420},
    {0, 17, (void *)0x6040603},
    {0, 19, (void *)0x420028b},
    {0, 16, (void *)0x620421},
    {0, 17, (void *)0x6070606},
    {0, 19, (void *)0x420028b},
    {0, 16, (void *)0x620422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1546},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x630420},
    {0, 17, (void *)0x610060f},
    {0, 19, (void *)0x420028e},
    {0, 16, (void *)0x630421},
    {0, 17, (void *)0x6130612},
    {0, 19, (void *)0x420028e},
    {0, 16, (void *)0x630422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1558},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {0, 3, (void *)_v1551},
    {0, 16, (void *)0x640428},
    {0, 17, (void *)0x61c061b},
    {0, 19, (void *)0x4280293},
    {0, 16, (void *)0x640429},
    {0, 17, (void *)0x61f061e},
    {0, 19, (void *)0x4280293},
    {0, 16, (void *)0x64042a},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1570},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x650420},
    {0, 17, (void *)0x6280627},
    {0, 19, (void *)0x4200296},
    {0, 16, (void *)0x650421},
    {0, 17, (void *)0x62b062a},
    {0, 19, (void *)0x4200296},
    {0, 16, (void *)0x650422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1582},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x660420},
    {0, 17, (void *)0x6340633},
    {0, 19, (void *)0x4200299},
    {0, 16, (void *)0x660421},
    {0, 17, (void *)0x6370636},
    {0, 19, (void *)0x4200299},
    {0, 16, (void *)0x660422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1594},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x670420},
    {0, 17, (void *)0x640063f},
    {0, 19, (void *)0x420029c},
    {0, 16, (void *)0x670421},
    {0, 17, (void *)0x6430642},
    {0, 19, (void *)0x420029c},
    {0, 16, (void *)0x670422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1606},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x680420},
    {0, 17, (void *)0x64c064b},
    {0, 19, (void *)0x420029f},
    {0, 16, (void *)0x680421},
    {0, 17, (void *)0x64f064e},
    {0, 19, (void *)0x420029f},
    {0, 16, (void *)0x680422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1618},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x690420},
    {0, 17, (void *)0x6580657},
    {0, 19, (void *)0x42002a2},
    {0, 16, (void *)0x690421},
    {0, 17, (void *)0x65b065a},
    {0, 19, (void *)0x42002a2},
    {0, 16, (void *)0x690422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1630},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x6a0420},
    {0, 17, (void *)0x6640663},
    {0, 19, (void *)0x42002a5},
    {0, 16, (void *)0x6a0421},
    {0, 17, (void *)0x6670666},
    {0, 19, (void *)0x42002a5},
    {0, 16, (void *)0x6a0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1642},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x6b0420},
    {0, 17, (void *)0x670066f},
    {0, 19, (void *)0x42002a8},
    {0, 16, (void *)0x6b0421},
    {0, 17, (void *)0x6730672},
    {0, 19, (void *)0x42002a8},
    {0, 16, (void *)0x6b0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1654},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x6c0420},
    {0, 17, (void *)0x67c067b},
    {0, 19, (void *)0x42002ab},
    {0, 16, (void *)0x6c0421},
    {0, 17, (void *)0x67f067e},
    {0, 19, (void *)0x42002ab},
    {0, 16, (void *)0x6c0422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1666},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x6d0420},
    {0, 17, (void *)0x6880687},
    {0, 19, (void *)0x42002ae},
    {0, 16, (void *)0x6d0421},
    {0, 17, (void *)0x68b068a},
    {0, 19, (void *)0x42002ae},
    {0, 16, (void *)0x6d0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x6e0424},
    {0, 17, (void *)0x6970696},
    {0, 19, (void *)0x42402b1},
    {0, 16, (void *)0x6e0425},
    {0, 17, (void *)0x69a0699},
    {0, 19, (void *)0x42402b1},
    {0, 16, (void *)0x6e0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1693},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x6f0420},
    {0, 17, (void *)0x6a306a2},
    {0, 19, (void *)0x42002b4},
    {0, 16, (void *)0x6f0421},
    {0, 17, (void *)0x6a606a5},
    {0, 19, (void *)0x42002b4},
    {0, 16, (void *)0x6f0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x700424},
    {0, 17, (void *)0x6b206b1},
    {0, 19, (void *)0x42402b7},
    {0, 16, (void *)0x700425},
    {0, 17, (void *)0x6b506b4},
    {0, 19, (void *)0x42402b7},
    {0, 16, (void *)0x700426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1720},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x710420},
    {0, 17, (void *)0x6be06bd},
    {0, 19, (void *)0x42002ba},
    {0, 16, (void *)0x710421},
    {0, 17, (void *)0x6c106c0},
    {0, 19, (void *)0x42002ba},
    {0, 16, (void *)0x710422},
    {5, 14, (void *)_v1551},
    {0, 6, (void *)1732},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x720420},
    {0, 17, (void *)0x6ca06c9},
    {0, 19, (void *)0x42002bd},
    {0, 16, (void *)0x720421},
    {0, 17, (void *)0x6cd06cc},
    {0, 19, (void *)0x42002bd},
    {0, 16, (void *)0x720422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x730424},
    {0, 17, (void *)0x6d506d4},
    {0, 19, (void *)0x42402c0},
    {0, 16, (void *)0x730425},
    {0, 17, (void *)0x6d806d7},
    {0, 19, (void *)0x42402c0},
    {0, 16, (void *)0x730426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1755},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1467},
    {0, 6, (void *)1758},
    {0, 3, (void *)_v1467},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x7b0424},
    {0, 17, (void *)0x6e406e3},
    {0, 19, (void *)0x42402c3},
    {0, 16, (void *)0x7b0425},
    {0, 17, (void *)0x6e706e6},
    {0, 19, (void *)0x42402c3},
    {0, 16, (void *)0x7b0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1770},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1496},
    {0, 6, (void *)1773},
    {0, 3, (void *)_v1496},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb60424},
    {0, 17, (void *)0x6f306f2},
    {0, 19, (void *)0x42402c8},
    {0, 16, (void *)0xb60425},
    {0, 17, (void *)0x6f606f5},
    {0, 19, (void *)0x42402c8},
    {0, 16, (void *)0xb60426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1785},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1496},
    {0, 6, (void *)1788},
    {0, 3, (void *)_v1496},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb20424},
    {0, 17, (void *)0x7020701},
    {0, 19, (void *)0x42402cd},
    {0, 16, (void *)0xb20425},
    {0, 17, (void *)0x7050704},
    {0, 19, (void *)0x42402cd},
    {0, 16, (void *)0xb20426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1800},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x750420},
    {0, 17, (void *)0x70e070d},
    {0, 19, (void *)0x42002d6},
    {0, 16, (void *)0x750421},
    {0, 17, (void *)0x7110710},
    {0, 19, (void *)0x42002d6},
    {0, 16, (void *)0x750422},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)1812},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1364},
    {0, 3, (void *)_v1364},
    {5, 14, (void *)_v1141},
    {0, 3, (void *)_v1141},
    {5, 14, (void *)_v1364},
    {0, 11, (void *)1814},
    {5, 14, (void *)_v1141},
    {0, 11, (void *)1816},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x800424},
    {0, 17, (void *)0x7220721},
    {0, 19, (void *)0x42402d9},
    {0, 16, (void *)0x800425},
    {0, 17, (void *)0x7250724},
    {0, 19, (void *)0x42402d9},
    {0, 16, (void *)0x800426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1832},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1148},
    {0, 6, (void *)1835},
    {0, 1, (void *)&_v1146},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1148},
    {0, 11, (void *)1834},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x760424},
    {0, 17, (void *)0x7350734},
    {0, 19, (void *)0x42402df},
    {0, 16, (void *)0x760425},
    {0, 17, (void *)0x7380737},
    {0, 19, (void *)0x42402df},
    {0, 16, (void *)0x760426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1851},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1469},
    {0, 6, (void *)1854},
    {0, 3, (void *)_v1469},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1857},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1860},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1863},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1866},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1869},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1872},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1263},
    {0, 6, (void *)1875},
    {0, 1, (void *)&_v1167},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1263},
    {0, 11, (void *)1874},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x8f0424},
    {0, 17, (void *)0x75d075c},
    {0, 19, (void *)0x42402e2},
    {0, 16, (void *)0x8f0425},
    {0, 17, (void *)0x760075f},
    {0, 19, (void *)0x42402e2},
    {0, 16, (void *)0x8f0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1891},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {0, 3, (void *)_v1551},
    {5, 14, (void *)_v1551},
    {0, 11, (void *)1893},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x770424},
    {0, 17, (void *)0x76d076c},
    {0, 19, (void *)0x42402e5},
    {0, 16, (void *)0x770425},
    {0, 17, (void *)0x770076f},
    {0, 19, (void *)0x42402e5},
    {0, 16, (void *)0x770426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1907},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1910},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {0, 11, (void *)1893},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x780424},
    {0, 17, (void *)0x77e077d},
    {0, 19, (void *)0x42402eb},
    {0, 16, (void *)0x780425},
    {0, 17, (void *)0x7810780},
    {0, 19, (void *)0x42402eb},
    {0, 16, (void *)0x780426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1924},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1927},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x790424},
    {0, 17, (void *)0x78d078c},
    {0, 19, (void *)0x42402f1},
    {0, 16, (void *)0x790425},
    {0, 17, (void *)0x790078f},
    {0, 19, (void *)0x42402f1},
    {0, 16, (void *)0x790426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1939},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1865},
    {5, 14, (void *)_v1551},
    {0, 11, (void *)1893},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x7a0424},
    {0, 17, (void *)0x79d079c},
    {0, 19, (void *)0x42402f6},
    {0, 16, (void *)0x7a0425},
    {0, 17, (void *)0x7a0079f},
    {0, 19, (void *)0x42402f6},
    {0, 16, (void *)0x7a0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1955},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1856},
    {5, 14, (void *)_v1467},
    {0, 11, (void *)1757},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x7c0424},
    {0, 17, (void *)0x7ad07ac},
    {0, 19, (void *)0x42402fc},
    {0, 16, (void *)0x7c0425},
    {0, 17, (void *)0x7b007af},
    {0, 19, (void *)0x42402fc},
    {0, 16, (void *)0x7c0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1971},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x7d0424},
    {0, 17, (void *)0x7b907b8},
    {0, 19, (void *)0x42402ff},
    {0, 16, (void *)0x7d0425},
    {0, 17, (void *)0x7bc07bb},
    {0, 19, (void *)0x42402ff},
    {0, 16, (void *)0x7d0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)1983},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)1446},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)1468},
    {5, 14, (void *)_v1477},
    {0, 11, (void *)1465},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x7e0424},
    {0, 17, (void *)0x7cd07cc},
    {0, 19, (void *)0x4240304},
    {0, 16, (void *)0x7e0425},
    {0, 17, (void *)0x7d007cf},
    {0, 19, (void *)0x4240304},
    {0, 16, (void *)0x7e0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2003},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1210},
    {0, 11, (void *)58},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x7f0424},
    {0, 17, (void *)0x7db07da},
    {0, 19, (void *)0x424030c},
    {0, 16, (void *)0x7f0425},
    {0, 17, (void *)0x7de07dd},
    {0, 19, (void *)0x424030c},
    {0, 16, (void *)0x7f0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2017},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1218},
    {0, 3, (void *)_v1218},
    {5, 14, (void *)_v1219},
    {0, 3, (void *)_v1219},
    {5, 14, (void *)_v1218},
    {0, 11, (void *)2019},
    {5, 14, (void *)_v1219},
    {0, 11, (void *)2021},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x810424},
    {0, 17, (void *)0x7ef07ee},
    {0, 19, (void *)0x4240311},
    {0, 16, (void *)0x810425},
    {0, 17, (void *)0x7f207f1},
    {0, 19, (void *)0x4240311},
    {0, 16, (void *)0x810426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2037},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1395},
    {0, 11, (void *)170},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)2042},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x820424},
    {0, 17, (void *)0x80007ff},
    {0, 19, (void *)0x4240314},
    {0, 16, (void *)0x820425},
    {0, 17, (void *)0x8030802},
    {0, 19, (void *)0x4240314},
    {0, 16, (void *)0x820426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2054},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {0, 11, (void *)199},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x830424},
    {0, 17, (void *)0x80e080d},
    {0, 19, (void *)0x4240317},
    {0, 16, (void *)0x830425},
    {0, 17, (void *)0x8110810},
    {0, 19, (void *)0x4240317},
    {0, 16, (void *)0x830426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2068},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1466},
    {0, 11, (void *)2041},
    {5, 14, (void *)_v1396},
    {0, 20, (void *)0x819},
    {0, 3, (void *)_v1396},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2076},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1331},
    {0, 6, (void *)2079},
    {0, 1, (void *)&_v1239},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2082},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)2078},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2087},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x840424},
    {0, 17, (void *)0x82d082c},
    {0, 19, (void *)0x424031e},
    {0, 16, (void *)0x840425},
    {0, 17, (void *)0x830082f},
    {0, 19, (void *)0x424031e},
    {0, 16, (void *)0x840426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2099},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1871},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)2078},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1909},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x850424},
    {0, 17, (void *)0x83f083e},
    {0, 19, (void *)0x4240324},
    {0, 16, (void *)0x850425},
    {0, 17, (void *)0x8420841},
    {0, 19, (void *)0x4240324},
    {0, 16, (void *)0x850426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2117},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1862},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x860424},
    {0, 17, (void *)0x84d084c},
    {0, 19, (void *)0x424032a},
    {0, 16, (void *)0x860425},
    {0, 17, (void *)0x850084f},
    {0, 19, (void *)0x424032a},
    {0, 16, (void *)0x860426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2131},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)2086},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)2136},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1261},
    {0, 6, (void *)2139},
    {0, 1, (void *)&_v1262},
    {5, 14, (void *)_v1263},
    {0, 6, (void *)2142},
    {0, 1, (void *)&_v1264},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x880424},
    {0, 17, (void *)0x8660865},
    {0, 19, (void *)0x4240331},
    {0, 16, (void *)0x880425},
    {0, 17, (void *)0x8690868},
    {0, 19, (void *)0x4240331},
    {0, 16, (void *)0x880426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2156},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1513},
    {0, 6, (void *)2159},
    {0, 1, (void *)&_v1270},
    {5, 14, (void *)_v1477},
    {0, 6, (void *)2162},
    {0, 1, (void *)&_v1272},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)2158},
    {5, 14, (void *)_v1477},
    {0, 11, (void *)2161},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x950424},
    {0, 17, (void *)0x87e087d},
    {0, 19, (void *)0x4240334},
    {0, 16, (void *)0x950425},
    {0, 17, (void *)0x8810880},
    {0, 19, (void *)0x4240334},
    {0, 16, (void *)0x950426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2180},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1466},
    {0, 11, (void *)1811},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x890424},
    {0, 17, (void *)0x88c088b},
    {0, 19, (void *)0x4240337},
    {0, 16, (void *)0x890425},
    {0, 17, (void *)0x88f088e},
    {0, 19, (void *)0x4240337},
    {0, 16, (void *)0x890426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2194},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1425},
    {0, 6, (void *)2197},
    {0, 3, (void *)_v1425},
    {5, 14, (void *)_v1538},
    {0, 6, (void *)2200},
    {0, 20, (void *)0x899},
    {0, 1, (void *)&_v1286},
    {5, 14, (void *)_v1332},
    {0, 6, (void *)2204},
    {0, 3, (void *)_v1332},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x8a0420},
    {0, 17, (void *)0x8a208a1},
    {0, 19, (void *)0x420033a},
    {0, 16, (void *)0x8a0421},
    {0, 17, (void *)0x8a508a4},
    {0, 19, (void *)0x420033a},
    {0, 16, (void *)0x8a0422},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)2216},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x8b0424},
    {0, 17, (void *)0x8b008af},
    {0, 19, (void *)0x424033d},
    {0, 16, (void *)0x8b0425},
    {0, 17, (void *)0x8b308b2},
    {0, 19, (void *)0x424033d},
    {0, 16, (void *)0x8b0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2230},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0x8c0420},
    {0, 17, (void *)0x8bc08bb},
    {0, 19, (void *)0x4200340},
    {0, 16, (void *)0x8c0421},
    {0, 17, (void *)0x8bf08be},
    {0, 19, (void *)0x4200340},
    {0, 16, (void *)0x8c0422},
    {5, 14, (void *)_v1297},
    {0, 11, (void *)234},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x8d0424},
    {0, 17, (void *)0x8c708c6},
    {0, 19, (void *)0x4240343},
    {0, 16, (void *)0x8d0425},
    {0, 17, (void *)0x8ca08c9},
    {0, 19, (void *)0x4240343},
    {0, 16, (void *)0x8d0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2253},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1306},
    {0, 20, (void *)0x8d0},
    {2, 3, (void *)_v1304},
    {2, 1, (void *)&_v563},
    {2, 1, (void *)&_v564},
    {2, 1, (void *)&_v565},
    {2, 1, (void *)&_v566},
    {2, 1, (void *)&_v567},
    {2, 1, (void *)&_v568},
    {2, 1, (void *)&_v569},
    {2, 1, (void *)&_v570},
    {0, 1, (void *)&_v571},
    {5, 14, (void *)_v1306},
    {0, 20, (void *)0x8dc},
    {2, 3, (void *)_v1304},
    {2, 1, (void *)&_v554},
    {2, 1, (void *)&_v555},
    {2, 1, (void *)&_v556},
    {2, 1, (void *)&_v557},
    {2, 1, (void *)&_v558},
    {2, 1, (void *)&_v559},
    {2, 1, (void *)&_v560},
    {2, 1, (void *)&_v561},
    {0, 1, (void *)&_v562},
    {5, 14, (void *)_v1306},
    {0, 11, (void *)2255},
    {5, 14, (void *)_v1306},
    {0, 11, (void *)2267},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x910424},
    {0, 17, (void *)0x8ef08ee},
    {0, 19, (void *)0x4240346},
    {0, 16, (void *)0x910425},
    {0, 17, (void *)0x8f208f1},
    {0, 19, (void *)0x4240346},
    {0, 16, (void *)0x910426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2293},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x900424},
    {0, 17, (void *)0x8fb08fa},
    {0, 19, (void *)0x424034d},
    {0, 16, (void *)0x900425},
    {0, 17, (void *)0x8fe08fd},
    {0, 19, (void *)0x424034d},
    {0, 16, (void *)0x900426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2305},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1330},
    {0, 6, (void *)2310},
    {0, 3, (void *)_v1330},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x920424},
    {0, 17, (void *)0x90c090b},
    {0, 19, (void *)0x4240350},
    {0, 16, (void *)0x920425},
    {0, 17, (void *)0x90f090e},
    {0, 19, (void *)0x4240350},
    {0, 16, (void *)0x920426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2322},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1554},
    {0, 6, (void *)2325},
    {0, 3, (void *)_v1554},
    {5, 14, (void *)_v1330},
    {0, 11, (void *)2309},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1330},
    {0, 6, (void *)2332},
    {0, 3, (void *)_v1330},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x930424},
    {0, 17, (void *)0x9220921},
    {0, 19, (void *)0x4240356},
    {0, 16, (void *)0x930425},
    {0, 17, (void *)0x9250924},
    {0, 19, (void *)0x4240356},
    {0, 16, (void *)0x930426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2344},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1554},
    {0, 6, (void *)2347},
    {0, 3, (void *)_v1554},
    {5, 14, (void *)_v1330},
    {0, 11, (void *)2331},
    {5, 14, (void *)_v1331},
    {0, 11, (void *)895},
    {5, 14, (void *)_v1332},
    {0, 11, (void *)2203},
    {5, 14, (void *)_v1554},
    {0, 11, (void *)2324},
    {5, 14, (void *)_v1554},
    {0, 11, (void *)2346},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x960424},
    {0, 17, (void *)0x93b093a},
    {0, 19, (void *)0x424035c},
    {0, 16, (void *)0x960425},
    {0, 17, (void *)0x93e093d},
    {0, 19, (void *)0x424035c},
    {0, 16, (void *)0x960426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2369},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1507},
    {0, 6, (void *)2372},
    {0, 1, (void *)&_v1339},
    {5, 14, (void *)_v1344},
    {0, 3, (void *)_v1344},
    {5, 14, (void *)_v1345},
    {0, 3, (void *)_v1345},
    {5, 14, (void *)_v1344},
    {0, 11, (void *)2374},
    {5, 14, (void *)_v1345},
    {0, 11, (void *)2376},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xbb0424},
    {0, 17, (void *)0x9520951},
    {0, 19, (void *)0x4240361},
    {0, 16, (void *)0xbb0425},
    {0, 17, (void *)0x9550954},
    {0, 19, (void *)0x4240361},
    {0, 16, (void *)0xbb0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2392},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa30424},
    {0, 17, (void *)0x95e095d},
    {0, 19, (void *)0x4240366},
    {0, 16, (void *)0xa30425},
    {0, 17, (void *)0x9610960},
    {0, 19, (void *)0x4240366},
    {0, 16, (void *)0xa30426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2404},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1507},
    {0, 11, (void *)2371},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x970424},
    {0, 17, (void *)0x96c096b},
    {0, 19, (void *)0x424036c},
    {0, 16, (void *)0x970425},
    {0, 17, (void *)0x96f096e},
    {0, 19, (void *)0x424036c},
    {0, 16, (void *)0x970426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2418},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1536},
    {0, 6, (void *)2421},
    {0, 3, (void *)_v1536},
    {5, 14, (void *)_v1538},
    {0, 6, (void *)2424},
    {0, 20, (void *)0x979},
    {0, 1, (void *)&_v1359},
    {5, 14, (void *)_v1507},
    {0, 6, (void *)2428},
    {0, 1, (void *)&_v1361},
    {5, 14, (void *)_v1364},
    {0, 3, (void *)_v1364},
    {5, 14, (void *)_v1364},
    {0, 11, (void *)2430},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x990424},
    {0, 17, (void *)0x9860985},
    {0, 19, (void *)0x4240374},
    {0, 16, (void *)0x990425},
    {0, 17, (void *)0x9890988},
    {0, 19, (void *)0x4240374},
    {0, 16, (void *)0x990426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2444},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1404},
    {0, 20, (void *)0x98f},
    {0, 3, (void *)_v1404},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x9a0424},
    {0, 17, (void *)0x9950994},
    {0, 19, (void *)0x424037f},
    {0, 16, (void *)0x9a0425},
    {0, 17, (void *)0x9980997},
    {0, 19, (void *)0x424037f},
    {0, 16, (void *)0x9a0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2459},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x9b0424},
    {0, 17, (void *)0x9a109a0},
    {0, 19, (void *)0x4240385},
    {0, 16, (void *)0x9b0425},
    {0, 17, (void *)0x9a409a3},
    {0, 19, (void *)0x4240385},
    {0, 16, (void *)0x9b0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2471},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1496},
    {0, 6, (void *)2474},
    {0, 3, (void *)_v1496},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x9c0424},
    {0, 17, (void *)0x9b009af},
    {0, 19, (void *)0x424038b},
    {0, 16, (void *)0x9c0425},
    {0, 17, (void *)0x9b309b2},
    {0, 19, (void *)0x424038b},
    {0, 16, (void *)0x9c0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2486},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x9d0424},
    {0, 17, (void *)0x9bc09bb},
    {0, 19, (void *)0x424038e},
    {0, 16, (void *)0x9d0425},
    {0, 17, (void *)0x9bf09be},
    {0, 19, (void *)0x424038e},
    {0, 16, (void *)0x9d0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2498},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x9e0424},
    {0, 17, (void *)0x9c809c7},
    {0, 19, (void *)0x4240391},
    {0, 16, (void *)0x9e0425},
    {0, 17, (void *)0x9cb09ca},
    {0, 19, (void *)0x4240391},
    {0, 16, (void *)0x9e0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2510},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0x9f0424},
    {0, 17, (void *)0x9d409d3},
    {0, 19, (void *)0x4240394},
    {0, 16, (void *)0x9f0425},
    {0, 17, (void *)0x9d709d6},
    {0, 19, (void *)0x4240394},
    {0, 16, (void *)0x9f0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2522},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0xc50420},
    {0, 17, (void *)0x9e009df},
    {0, 19, (void *)0x4200397},
    {0, 16, (void *)0xc50421},
    {0, 17, (void *)0x9e309e2},
    {0, 19, (void *)0x4200397},
    {0, 16, (void *)0xc50422},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa00424},
    {0, 17, (void *)0x9e909e8},
    {0, 19, (void *)0x424039d},
    {0, 16, (void *)0xa00425},
    {0, 17, (void *)0x9ec09eb},
    {0, 19, (void *)0x424039d},
    {0, 16, (void *)0xa00426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2543},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1395},
    {0, 3, (void *)_v1395},
    {5, 14, (void *)_v1396},
    {0, 11, (void *)2072},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa10424},
    {0, 17, (void *)0x9f909f8},
    {0, 19, (void *)0x42403a0},
    {0, 16, (void *)0xa10425},
    {0, 17, (void *)0x9fc09fb},
    {0, 19, (void *)0x42403a0},
    {0, 16, (void *)0xa10426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2559},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1496},
    {0, 11, (void *)2473},
    {5, 14, (void *)_v1505},
    {0, 6, (void *)2564},
    {0, 1, (void *)&_v1402},
    {5, 14, (void *)_v1404},
    {0, 20, (void *)0xa07},
    {0, 3, (void *)_v1404},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa20424},
    {0, 17, (void *)0xa0d0a0c},
    {0, 19, (void *)0x42403a6},
    {0, 16, (void *)0xa20425},
    {0, 17, (void *)0xa100a0f},
    {0, 19, (void *)0x42403a6},
    {0, 16, (void *)0xa20426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2579},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1513},
    {0, 6, (void *)2582},
    {0, 1, (void *)&_v1409},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa40424},
    {0, 17, (void *)0xa1c0a1b},
    {0, 19, (void *)0x42403a9},
    {0, 16, (void *)0xa40425},
    {0, 17, (void *)0xa1f0a1e},
    {0, 19, (void *)0x42403a9},
    {0, 16, (void *)0xa40426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2594},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa50424},
    {0, 17, (void *)0xa280a27},
    {0, 19, (void *)0x42403ac},
    {0, 16, (void *)0xa50425},
    {0, 17, (void *)0xa2b0a2a},
    {0, 19, (void *)0x42403ac},
    {0, 16, (void *)0xa50426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2606},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa70424},
    {0, 17, (void *)0xa340a33},
    {0, 19, (void *)0x42403af},
    {0, 16, (void *)0xa70425},
    {0, 17, (void *)0xa370a36},
    {0, 19, (void *)0x42403af},
    {0, 16, (void *)0xa70426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2618},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1420},
    {0, 3, (void *)_v1420},
    {5, 14, (void *)_v1421},
    {0, 11, (void *)124},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa80424},
    {0, 17, (void *)0xa440a43},
    {0, 19, (void *)0x42403b5},
    {0, 16, (void *)0xa80425},
    {0, 17, (void *)0xa470a46},
    {0, 19, (void *)0x42403b5},
    {0, 16, (void *)0xa80426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2634},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1425},
    {0, 11, (void *)2196},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xc30424},
    {0, 17, (void *)0xa520a51},
    {0, 19, (void *)0x42403b8},
    {0, 16, (void *)0xc30425},
    {0, 17, (void *)0xa550a54},
    {0, 19, (void *)0x42403b8},
    {0, 16, (void *)0xc30426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2648},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xa90424},
    {0, 17, (void *)0xa5e0a5d},
    {0, 19, (void *)0x42403bb},
    {0, 16, (void *)0xa90425},
    {0, 17, (void *)0xa610a60},
    {0, 19, (void *)0x42403bb},
    {0, 16, (void *)0xa90426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2660},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)199},
    {0, 16, (void *)0xc60420},
    {0, 17, (void *)0xa6a0a69},
    {0, 19, (void *)0x42003c0},
    {0, 16, (void *)0xc60421},
    {0, 17, (void *)0xa6d0a6c},
    {0, 19, (void *)0x42003c0},
    {0, 16, (void *)0xc60422},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb80424},
    {0, 17, (void *)0xa730a72},
    {0, 19, (void *)0x42403c6},
    {0, 16, (void *)0xb80425},
    {0, 17, (void *)0xa760a75},
    {0, 19, (void *)0x42403c6},
    {0, 16, (void *)0xb80426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2681},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xac0424},
    {0, 17, (void *)0xa7f0a7e},
    {0, 19, (void *)0x42403cc},
    {0, 16, (void *)0xac0425},
    {0, 17, (void *)0xa820a81},
    {0, 19, (void *)0x42403cc},
    {0, 16, (void *)0xac0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2693},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xaa0424},
    {0, 17, (void *)0xa8b0a8a},
    {0, 19, (void *)0x42403d2},
    {0, 16, (void *)0xaa0425},
    {0, 17, (void *)0xa8e0a8d},
    {0, 19, (void *)0x42403d2},
    {0, 16, (void *)0xaa0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2705},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1473},
    {0, 20, (void *)0xa94},
    {0, 3, (void *)_v1473},
    {5, 14, (void *)_v1473},
    {0, 11, (void *)2707},
    {5, 14, (void *)_v1466},
    {0, 11, (void *)2135},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xab0424},
    {0, 17, (void *)0xa9e0a9d},
    {0, 19, (void *)0x42403d5},
    {0, 16, (void *)0xab0425},
    {0, 17, (void *)0xaa10aa0},
    {0, 19, (void *)0x42403d5},
    {0, 16, (void *)0xab0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2724},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1466},
    {0, 11, (void *)2215},
    {5, 14, (void *)_v1466},
    {0, 11, (void *)133},
    {5, 14, (void *)_v1554},
    {0, 6, (void *)2731},
    {0, 3, (void *)_v1554},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xad0424},
    {0, 17, (void *)0xab10ab0},
    {0, 19, (void *)0x42403d8},
    {0, 16, (void *)0xad0425},
    {0, 17, (void *)0xab40ab3},
    {0, 19, (void *)0x42403d8},
    {0, 16, (void *)0xad0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2743},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1467},
    {0, 6, (void *)2746},
    {0, 3, (void *)_v1467},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2749},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1466},
    {0, 6, (void *)2752},
    {0, 3, (void *)_v1466},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xae0424},
    {0, 17, (void *)0xac60ac5},
    {0, 19, (void *)0x42403db},
    {0, 16, (void *)0xae0425},
    {0, 17, (void *)0xac90ac8},
    {0, 19, (void *)0x42403db},
    {0, 16, (void *)0xae0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2764},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)2748},
    {5, 14, (void *)_v1466},
    {0, 11, (void *)2751},
    {5, 14, (void *)_v1467},
    {0, 11, (void *)2745},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)900},
    {5, 14, (void *)_v1469},
    {0, 11, (void *)1853},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xaf0424},
    {0, 17, (void *)0xadc0adb},
    {0, 19, (void *)0x42403e2},
    {0, 16, (void *)0xaf0425},
    {0, 17, (void *)0xadf0ade},
    {0, 19, (void *)0x42403e2},
    {0, 16, (void *)0xaf0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2786},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1473},
    {0, 11, (void *)2707},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb00424},
    {0, 17, (void *)0xaea0ae9},
    {0, 19, (void *)0x42403e8},
    {0, 16, (void *)0xb00425},
    {0, 17, (void *)0xaed0aec},
    {0, 19, (void *)0x42403e8},
    {0, 16, (void *)0xb00426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2800},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1477},
    {0, 11, (void *)2161},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)2563},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb10424},
    {0, 17, (void *)0xafa0af9},
    {0, 19, (void *)0x42403eb},
    {0, 16, (void *)0xb10425},
    {0, 17, (void *)0xafd0afc},
    {0, 19, (void *)0x42403eb},
    {0, 16, (void *)0xb10426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2816},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1496},
    {0, 11, (void *)1787},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2821},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2824},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb40424},
    {0, 17, (void *)0xb0e0b0d},
    {0, 19, (void *)0x42403ee},
    {0, 16, (void *)0xb40425},
    {0, 17, (void *)0xb110b10},
    {0, 19, (void *)0x42403ee},
    {0, 16, (void *)0xb40426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2836},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1868},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1926},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb50424},
    {0, 17, (void *)0xb1e0b1d},
    {0, 19, (void *)0x42403f4},
    {0, 16, (void *)0xb50425},
    {0, 17, (void *)0xb210b20},
    {0, 19, (void *)0x42403f4},
    {0, 16, (void *)0xb50426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2852},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)1859},
    {5, 14, (void *)_v1496},
    {0, 11, (void *)1772},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xb70424},
    {0, 17, (void *)0xb2e0b2d},
    {0, 19, (void *)0x42403fa},
    {0, 16, (void *)0xb70425},
    {0, 17, (void *)0xb310b30},
    {0, 19, (void *)0x42403fa},
    {0, 16, (void *)0xb70426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2868},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)2823},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xba0424},
    {0, 17, (void *)0xb3c0b3b},
    {0, 19, (void *)0x42403ff},
    {0, 16, (void *)0xba0425},
    {0, 17, (void *)0xb3f0b3e},
    {0, 19, (void *)0x42403ff},
    {0, 16, (void *)0xba0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2882},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1505},
    {0, 11, (void *)237},
    {5, 14, (void *)_v1507},
    {0, 11, (void *)144},
    {5, 14, (void *)_v1507},
    {0, 11, (void *)2427},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xbc0424},
    {0, 17, (void *)0xb520b51},
    {0, 19, (void *)0x4240402},
    {0, 16, (void *)0xbc0425},
    {0, 17, (void *)0xb550b54},
    {0, 19, (void *)0x4240402},
    {0, 16, (void *)0xbc0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2904},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1513},
    {0, 11, (void *)2581},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xbd0424},
    {0, 17, (void *)0xb620b61},
    {0, 19, (void *)0x4240405},
    {0, 16, (void *)0xbd0425},
    {0, 17, (void *)0xb650b64},
    {0, 19, (void *)0x4240405},
    {0, 16, (void *)0xbd0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2920},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1520},
    {0, 3, (void *)_v1520},
    {5, 14, (void *)_v1520},
    {0, 3, (void *)_v1520},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xbe0424},
    {0, 17, (void *)0xb720b71},
    {0, 19, (void *)0x4240408},
    {0, 16, (void *)0xbe0425},
    {0, 17, (void *)0xb750b74},
    {0, 19, (void *)0x4240408},
    {0, 16, (void *)0xbe0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2936},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1530},
    {0, 11, (void *)108},
    {5, 14, (void *)_v1531},
    {0, 11, (void *)73},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xbf0424},
    {0, 17, (void *)0xb820b81},
    {0, 19, (void *)0x424040b},
    {0, 16, (void *)0xbf0425},
    {0, 17, (void *)0xb850b84},
    {0, 19, (void *)0x424040b},
    {0, 16, (void *)0xbf0426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2952},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1530},
    {0, 11, (void *)108},
    {5, 14, (void *)_v1530},
    {0, 11, (void *)108},
    {5, 14, (void *)_v1531},
    {0, 11, (void *)73},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xc00424},
    {0, 17, (void *)0xb940b93},
    {0, 19, (void *)0x4240411},
    {0, 16, (void *)0xc00425},
    {0, 17, (void *)0xb970b96},
    {0, 19, (void *)0x4240411},
    {0, 16, (void *)0xc00426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2970},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1554},
    {0, 11, (void *)2730},
    {5, 14, (void *)_v1536},
    {0, 11, (void *)2420},
    {5, 14, (void *)_v1538},
    {0, 11, (void *)2199},
    {5, 14, (void *)_v1538},
    {0, 11, (void *)2423},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xc10424},
    {0, 17, (void *)0xba80ba7},
    {0, 19, (void *)0x4240414},
    {0, 16, (void *)0xc10425},
    {0, 17, (void *)0xbab0baa},
    {0, 19, (void *)0x4240414},
    {0, 16, (void *)0xc10426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)2990},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xc20424},
    {0, 17, (void *)0xbb40bb3},
    {0, 19, (void *)0x4240417},
    {0, 16, (void *)0xc20425},
    {0, 17, (void *)0xbb70bb6},
    {0, 19, (void *)0x4240417},
    {0, 16, (void *)0xc20426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)3002},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)2075},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)2820},
    {5, 14, (void *)_v1553},
    {0, 11, (void *)2081},
    {5, 14, (void *)_v1548},
    {0, 11, (void *)140},
    {5, 14, (void *)_v1554},
    {0, 6, (void *)3013},
    {0, 3, (void *)_v1554},
    {5, 14, (void *)_v1551},
    {1, 11, (void *)240},
    {0, 16, (void *)0xc40424},
    {0, 17, (void *)0xbcb0bca},
    {0, 19, (void *)0x424041a},
    {0, 16, (void *)0xc40425},
    {0, 17, (void *)0xbce0bcd},
    {0, 19, (void *)0x424041a},
    {0, 16, (void *)0xc40426},
    {5, 14, (void *)_v1553},
    {0, 6, (void *)3025},
    {0, 3, (void *)_v1553},
    {5, 14, (void *)_v1554},
    {0, 11, (void *)3012}
};

static const struct efield _efieldarray[] = {
    {4, 267, -1, 508, 2},
    {4, 268, -1, 509, 2},
    {4, 269, -1, 510, 2},
    {0, 275, -1, 511, 2},
    {0, 280, -1, 511, 2},
    {4, 288, -1, 512, 2},
    {8, 289, 0, 513, 3},
    {16, 290, 1, 514, 3},
    {24, 291, 2, 515, 3},
    {32, 292, 3, 516, 3},
    {40, 297, 4, 517, 3},
    {0, 302, -1, 511, 2},
    {0, 307, -1, 511, 2},
    {4, 345, -1, 512, 2},
    {8, 346, -1, 518, 2},
    {16, 347, -1, 519, 2},
    {24, 348, -1, 520, 2},
    {88, 353, 0, 517, 3},
    {0, 358, -1, 511, 2},
    {4, 363, -1, 512, 2},
    {8, 364, -1, 518, 2},
    {16, 365, -1, 519, 2},
    {24, 366, 0, 513, 3},
    {32, 367, 1, 514, 3},
    {40, 368, 2, 515, 3},
    {48, 369, 3, 516, 3},
    {56, 374, 4, 517, 3},
    {4, 389, -1, 512, 2},
    {8, 390, -1, 521, 2},
    {16, 395, 0, 517, 3},
    {0, 400, -1, 511, 2},
    {0, 405, -1, 511, 2},
    {0, 410, -1, 511, 2},
    {4, 415, -1, 512, 2},
    {8, 416, -1, 518, 2},
    {16, 417, -1, 519, 2},
    {24, 422, 0, 517, 3},
    {0, 427, -1, 511, 2},
    {4, 432, -1, 512, 2},
    {8, 433, -1, 518, 2},
    {16, 434, -1, 519, 2},
    {24, 439, 0, 517, 3},
    {0, 444, -1, 511, 2},
    {0, 449, -1, 511, 2},
    {0, 454, -1, 511, 2},
    {0, 459, -1, 511, 2},
    {4, 464, -1, 512, 2},
    {8, 465, -1, 522, 2},
    {72, 466, -1, 518, 2},
    {80, 467, -1, 519, 2},
    {88, 468, -1, 523, 2},
    {96, 473, 0, 517, 3},
    {0, 478, -1, 511, 2},
    {4, 483, -1, 512, 2},
    {8, 484, -1, 518, 2},
    {16, 485, -1, 519, 2},
    {24, 490, 0, 517, 3},
    {0, 495, -1, 511, 2},
    {4, 500, -1, 512, 2},
    {8, 501, -1, 524, 2},
    {72, 502, -1, 523, 2},
    {80, 507, 0, 517, 3},
    {0, 512, -1, 511, 2},
    {4, 517, -1, 512, 2},
    {8, 522, 0, 517, 3},
    {0, 527, -1, 511, 2},
    {0, 532, -1, 511, 2},
    {4, 537, -1, 512, 2},
    {8, 542, 0, 517, 3},
    {0, 547, -1, 511, 2},
    {0, 552, -1, 511, 2},
    {4, 557, -1, 512, 2},
    {8, 558, -1, 522, 2},
    {72, 559, -1, 518, 2},
    {80, 560, -1, 519, 2},
    {88, 561, 0, 523, 3},
    {96, 566, 1, 517, 3},
    {0, 571, -1, 511, 2},
    {4, 576, -1, 512, 2},
    {8, 577, -1, 518, 2},
    {16, 578, -1, 519, 2},
    {24, 583, 0, 517, 3},
    {0, 588, -1, 511, 2},
    {0, 593, -1, 511, 2},
    {4, 608, -1, 525, 2},
    {20, 613, 0, 517, 3},
    {0, 618, -1, 511, 2},
    {0, 623, -1, 511, 2},
    {0, 628, -1, 511, 2},
    {0, 633, -1, 511, 2},
    {0, 638, -1, 511, 2},
    {0, 643, -1, 511, 2},
    {0, 648, -1, 511, 2},
    {0, 653, -1, 511, 2},
    {0, 658, -1, 511, 2},
    {0, 663, -1, 511, 2},
    {0, 668, -1, 511, 2},
    {0, 673, -1, 511, 2},
    {0, 678, -1, 511, 2},
    {4, 680, -1, 526, 2},
    {4, 681, -1, 527, 2},
    {0, 691, -1, 511, 2},
    {0, 700, -1, 511, 2},
    {0, 705, -1, 511, 2},
    {0, 710, -1, 511, 2},
    {0, 715, -1, 511, 2},
    {0, 720, -1, 511, 2},
    {0, 725, -1, 511, 2},
    {0, 730, -1, 511, 2},
    {0, 735, -1, 511, 2},
    {0, 740, -1, 511, 2},
    {0, 745, -1, 511, 2},
    {0, 750, -1, 511, 2},
    {0, 755, -1, 511, 2},
    {0, 760, -1, 511, 2},
    {0, 765, -1, 511, 2},
    {0, 770, -1, 511, 2},
    {0, 775, -1, 511, 2},
    {0, 780, -1, 511, 2},
    {0, 785, -1, 511, 2},
    {0, 790, -1, 511, 2},
    {0, 795, -1, 511, 2},
    {0, 800, -1, 511, 2},
    {0, 805, -1, 511, 2},
    {0, 810, -1, 511, 2},
    {0, 815, -1, 511, 2},
    {0, 820, -1, 511, 2},
    {0, 825, -1, 511, 2},
    {0, 830, -1, 511, 2},
    {4, 875, -1, 528, 2},
    {0, 880, -1, 511, 2},
    {0, 885, -1, 511, 2},
    {0, 890, -1, 511, 2},
    {0, 898, -1, 529, 2},
    {0, 903, -1, 511, 2},
    {0, 908, -1, 511, 2},
    {0, 913, -1, 511, 2},
    {0, 918, -1, 511, 2},
    {0, 923, -1, 511, 2},
    {0, 928, -1, 511, 2},
    {0, 933, -1, 511, 2},
    {0, 938, -1, 511, 2},
    {4, 943, -1, 512, 2},
    {8, 944, -1, 518, 2},
    {16, 945, -1, 530, 2},
    {24, 950, 0, 517, 3},
    {4, 955, -1, 512, 2},
    {8, 956, -1, 518, 2},
    {16, 957, -1, 530, 2},
    {24, 962, 0, 517, 3},
    {0, 967, -1, 511, 2},
    {4, 972, -1, 512, 2},
    {8, 977, 0, 517, 3},
    {4, 1020, -1, 512, 2},
    {8, 1021, -1, 531, 2},
    {20, 1022, -1, 532, 2},
    {32, 1023, 0, 533, 3},
    {40, 1028, 1, 517, 3},
    {4, 1030, -1, 534, 2},
    {4, 1031, -1, 535, 2},
    {4, 1032, -1, 536, 2},
    {4, 1033, -1, 537, 2},
    {4, 1034, -1, 538, 2},
    {4, 1035, -1, 539, 2},
    {4, 384, -1, 540, 2},
    {4, 385, -1, 541, 2},
    {4, 386, -1, 542, 2},
    {4, 387, -1, 543, 2},
    {4, 388, -1, 544, 2},
    {4, 1084, -1, 545, 2},
    {12, 1085, -1, 546, 2},
    {20, 1086, -1, 547, 2},
    {28, 1091, 0, 517, 3},
    {2, 1119, -1, 548, 2},
    {4, 1120, -1, 549, 2},
    {8, 1125, 0, 517, 3},
    {2, 1127, 0, 550, 3},
    {4, 1128, 1, 551, 3},
    {8, 1129, 2, 552, 3},
    {12, 1130, 3, 553, 3},
    {16, 1135, 4, 517, 3},
    {2, 1182, -1, 554, 2},
    {8, 1183, -1, 555, 2},
    {20, 1188, 0, 517, 3},
    {4, 1199, -1, 556, 2},
    {8, 1204, 0, 517, 3},
    {4, 1214, -1, 512, 2},
    {8, 1215, 0, 557, 3},
    {12, 1220, 1, 517, 3},
    {4, 1225, -1, 512, 2},
    {8, 1226, -1, 521, 2},
    {16, 1231, 0, 517, 3},
    {2, 1037, -1, 554, 2},
    {8, 1038, -1, 558, 2},
    {16, 1043, 0, 517, 3},
    {4, 1245, 0, 559, 3},
    {24, 1246, 1, 560, 3},
    {28, 1251, 2, 517, 3},
    {2, 1191, -1, 561, 2},
    {8, 1192, -1, 562, 2},
    {12, 1193, -1, 563, 2},
    {16, 1198, 0, 517, 3},
    {2, 1273, -1, 564, 2},
    {8, 1274, 0, 565, 3},
    {12, 1275, 1, 566, 3},
    {16, 1276, 2, 567, 3},
    {20, 1277, 3, 568, 3},
    {24, 1282, 4, 517, 3},
    {2, 833, -1, 554, 2},
    {8, 834, -1, 569, 2},
    {12, 839, 0, 517, 3},
    {4, 1356, -1, 570, 2},
    {8, 1357, -1, 571, 2},
    {12, 1358, 0, 572, 3},
    {16, 1363, 1, 517, 3},
    {4, 1365, -1, 573, 2},
    {8, 1370, 0, 517, 3},
    {4, 1372, -1, 574, 2},
    {8, 1373, -1, 575, 2},
    {12, 1378, 0, 517, 3},
    {4, 1380, -1, 576, 2},
    {8, 1381, -1, 577, 2},
    {12, 1386, 0, 517, 3},
    {4, 1396, -1, 578, 2},
    {8, 1397, -1, 579, 2},
    {24, 1398, -1, 580, 2},
    {76, 1403, 0, 517, 3},
    {4, 1414, -1, 581, 2},
    {8, 1415, 0, 582, 3},
    {12, 1420, 1, 517, 3},
    {4, 1422, -1, 583, 2},
    {4, 1426, 0, 584, 3},
    {8, 1427, 1, 585, 3},
    {12, 1432, 2, 517, 3},
    {4, 1434, -1, 586, 2},
    {8, 1435, -1, 587, 2},
    {12, 1440, 0, 517, 3},
    {2, 1452, -1, 588, 2},
    {4, 1453, -1, 589, 2},
    {12, 1458, 0, 517, 3},
    {4, 1481, -1, 590, 2},
    {4, 1498, -1, 591, 2},
    {52, 1499, -1, 592, 2},
    {100, 1500, -1, 593, 2},
    {128, 1505, 0, 517, 3},
    {4, 1507, 0, 594, 3},
    {8, 1508, 1, 595, 3},
    {24, 1509, 2, 596, 3},
    {28, 1514, 3, 517, 3},
    {4, 1515, -1, 597, 2},
    {12, 1516, 0, 598, 3},
    {16, 1517, -1, 599, 2},
    {36, 1518, 1, 600, 3},
    {40, 1519, -1, 601, 2},
    {44, 1524, 2, 517, 3},
    {4, 1546, -1, 602, 2},
    {8, 1547, -1, 603, 2},
    {12, 1552, 0, 517, 3},
    {2, 1553, -1, 604, 2},
    {6, 1554, -1, 605, 2},
    {12, 1559, 0, 517, 3},
    {2, 601, -1, 554, 2},
    {8, 602, -1, 606, 2},
    {12, 607, 0, 517, 3},
    {4, 1589, -1, 591, 2},
    {4, 1590, -1, 607, 2},
    {4, 1591, -1, 608, 2},
    {4, 1592, -1, 597, 2},
    {12, 1597, 0, 517, 3},
    {0, 1598, -1, 518, 2},
    {8, 1599, -1, 516, 2},
    {2, 1601, -1, 609, 2},
    {12, 1602, -1, 610, 2},
    {20, 1603, -1, 611, 2},
    {24, 1604, -1, 612, 2},
    {32, 1609, 0, 517, 3},
    {4, 1610, -1, 518, 2},
    {12, 1611, 0, 613, 3},
    {16, 1616, 1, 517, 3},
    {4, 1617, -1, 614, 2},
    {12, 1618, -1, 615, 2},
    {20, 1623, 0, 517, 3},
    {4, 1632, -1, 616, 2},
    {4, 1633, -1, 617, 2},
    {4, 1634, 0, 617, 3},
    {8, 1635, 1, 618, 3},
    {12, 1640, 2, 517, 3},
    {4, 1642, -1, 619, 2},
    {4, 1643, -1, 620, 2},
    {4, 1644, -1, 621, 2},
    {12, 1645, -1, 622, 2},
    {20, 1650, 0, 517, 3},
    {4, 1651, -1, 623, 2},
    {24, 1652, -1, 624, 2},
    {40, 1657, 0, 517, 3},
    {4, 1658, -1, 625, 2},
    {4, 1659, -1, 626, 2},
    {4, 1660, -1, 627, 2},
    {4, 1460, -1, 628, 2},
    {8, 1465, 0, 517, 3},
    {2, 253, -1, 629, 2},
    {4, 253, 0, 630, 3},
    {6, 253, 1, 631, 3},
    {8, 251, -1, 550, 66},
    {12, 252, 2, 632, 7},
    {0, 255, -1, 550, 18},
    {4, 256, -1, 634, 2},
    {8, 257, -1, 635, 2},
    {0, 259, -1, 550, 18},
    {4, 260, -1, 634, 2},
    {8, 261, -1, 635, 2},
    {0, 263, -1, 550, 18},
    {4, 264, -1, 634, 2},
    {8, 265, -1, 635, 2},
    {0, 271, -1, 636, 18},
    {4, 272, -1, 634, 2},
    {8, 273, -1, 635, 2},
    {0, 276, -1, 636, 18},
    {4, 277, -1, 634, 2},
    {8, 278, -1, 635, 2},
    {0, 281, -1, 636, 18},
    {4, 282, -1, 634, 2},
    {8, 283, -1, 635, 2},
    {0, 293, -1, 636, 18},
    {4, 294, -1, 634, 2},
    {8, 295, -1, 637, 2},
    {0, 298, -1, 636, 18},
    {4, 299, -1, 634, 2},
    {8, 300, -1, 635, 2},
    {0, 303, -1, 636, 18},
    {4, 304, -1, 634, 2},
    {8, 305, -1, 635, 2},
    {0, 308, -1, 636, 18},
    {4, 309, -1, 634, 2},
    {8, 310, -1, 635, 2},
    {0, 319, -1, 636, 18},
    {4, 320, -1, 634, 2},
    {8, 321, -1, 637, 2},
    {2, 316, -1, 638, 2},
    {4, 317, -1, 639, 2},
    {8, 318, -1, 640, 2},
    {12, 323, 0, 517, 3},
    {0, 330, -1, 636, 18},
    {4, 331, -1, 634, 2},
    {8, 332, -1, 637, 2},
    {4, 326, -1, 641, 2},
    {12, 327, -1, 642, 2},
    {20, 328, -1, 643, 2},
    {28, 329, -1, 644, 2},
    {36, 334, 0, 517, 3},
    {0, 339, -1, 636, 18},
    {4, 340, -1, 634, 2},
    {8, 341, -1, 637, 2},
    {2, 336, -1, 645, 2},
    {4, 337, -1, 646, 2},
    {20, 338, 0, 647, 3},
    {60, 343, 1, 517, 3},
    {0, 349, -1, 636, 18},
    {4, 350, -1, 634, 2},
    {8, 351, -1, 637, 2},
    {0, 354, -1, 636, 18},
    {4, 355, -1, 634, 2},
    {8, 356, -1, 635, 2},
    {0, 359, -1, 636, 18},
    {4, 360, -1, 634, 2},
    {8, 361, -1, 635, 2},
    {0, 370, -1, 636, 18},
    {4, 371, -1, 634, 2},
    {8, 372, -1, 637, 2},
    {0, 375, -1, 636, 18},
    {4, 376, -1, 634, 2},
    {8, 377, -1, 635, 2},
    {0, 391, -1, 636, 18},
    {4, 392, -1, 634, 2},
    {8, 393, -1, 637, 2},
    {0, 396, -1, 636, 18},
    {4, 397, -1, 634, 2},
    {8, 398, -1, 635, 2},
    {0, 401, -1, 636, 18},
    {4, 402, -1, 634, 2},
    {8, 403, -1, 635, 2},
    {0, 406, -1, 636, 18},
    {4, 407, -1, 634, 2},
    {8, 408, -1, 635, 2},
    {0, 411, -1, 636, 18},
    {4, 412, -1, 634, 2},
    {8, 413, -1, 635, 2},
    {0, 418, -1, 636, 18},
    {4, 419, -1, 634, 2},
    {8, 420, -1, 637, 2},
    {0, 423, -1, 636, 18},
    {4, 424, -1, 634, 2},
    {8, 425, -1, 635, 2},
    {0, 428, -1, 636, 18},
    {4, 429, -1, 634, 2},
    {8, 430, -1, 635, 2},
    {0, 435, -1, 636, 18},
    {4, 436, -1, 634, 2},
    {8, 437, -1, 637, 2},
    {0, 440, -1, 636, 18},
    {4, 441, -1, 634, 2},
    {8, 442, -1, 635, 2},
    {0, 445, -1, 636, 18},
    {4, 446, -1, 634, 2},
    {8, 447, -1, 635, 2},
    {0, 450, -1, 636, 18},
    {4, 451, -1, 634, 2},
    {8, 452, -1, 635, 2},
    {0, 455, -1, 636, 18},
    {4, 456, -1, 634, 2},
    {8, 457, -1, 635, 2},
    {0, 460, -1, 636, 18},
    {4, 461, -1, 634, 2},
    {8, 462, -1, 635, 2},
    {0, 469, -1, 636, 18},
    {4, 470, -1, 634, 2},
    {8, 471, -1, 637, 2},
    {0, 474, -1, 636, 18},
    {4, 475, -1, 634, 2},
    {8, 476, -1, 635, 2},
    {0, 479, -1, 636, 18},
    {4, 480, -1, 634, 2},
    {8, 481, -1, 635, 2},
    {0, 486, -1, 636, 18},
    {4, 487, -1, 634, 2},
    {8, 488, -1, 637, 2},
    {0, 491, -1, 636, 18},
    {4, 492, -1, 634, 2},
    {8, 493, -1, 635, 2},
    {0, 496, -1, 636, 18},
    {4, 497, -1, 634, 2},
    {8, 498, -1, 635, 2},
    {0, 503, -1, 636, 18},
    {4, 504, -1, 634, 2},
    {8, 505, -1, 637, 2},
    {0, 508, -1, 636, 18},
    {4, 509, -1, 634, 2},
    {8, 510, -1, 635, 2},
    {0, 513, -1, 636, 18},
    {4, 514, -1, 634, 2},
    {8, 515, -1, 635, 2},
    {0, 518, -1, 636, 18},
    {4, 519, -1, 634, 2},
    {8, 520, -1, 637, 2},
    {0, 523, -1, 636, 18},
    {4, 524, -1, 634, 2},
    {8, 525, -1, 635, 2},
    {0, 528, -1, 636, 18},
    {4, 529, -1, 634, 2},
    {8, 530, -1, 635, 2},
    {0, 533, -1, 636, 18},
    {4, 534, -1, 634, 2},
    {8, 535, -1, 635, 2},
    {0, 538, -1, 636, 18},
    {4, 539, -1, 634, 2},
    {8, 540, -1, 637, 2},
    {0, 543, -1, 636, 18},
    {4, 544, -1, 634, 2},
    {8, 545, -1, 635, 2},
    {0, 548, -1, 636, 18},
    {4, 549, -1, 634, 2},
    {8, 550, -1, 635, 2},
    {0, 553, -1, 636, 18},
    {4, 554, -1, 634, 2},
    {8, 555, -1, 635, 2},
    {0, 562, -1, 636, 18},
    {4, 563, -1, 634, 2},
    {8, 564, -1, 637, 2},
    {0, 567, -1, 636, 18},
    {4, 568, -1, 634, 2},
    {8, 569, -1, 635, 2},
    {0, 572, -1, 636, 18},
    {4, 573, -1, 634, 2},
    {8, 574, -1, 635, 2},
    {0, 579, -1, 636, 18},
    {4, 580, -1, 634, 2},
    {8, 581, -1, 637, 2},
    {0, 584, -1, 636, 18},
    {4, 585, -1, 634, 2},
    {8, 586, -1, 635, 2},
    {0, 589, -1, 636, 18},
    {4, 590, -1, 634, 2},
    {8, 591, -1, 635, 2},
    {0, 594, -1, 636, 18},
    {4, 595, -1, 634, 2},
    {8, 596, -1, 635, 2},
    {0, 603, -1, 636, 18},
    {4, 604, -1, 634, 2},
    {8, 605, -1, 637, 2},
    {0, 609, -1, 636, 18},
    {4, 610, -1, 634, 2},
    {8, 611, -1, 637, 2},
    {0, 614, -1, 636, 18},
    {4, 615, -1, 634, 2},
    {8, 616, -1, 635, 2},
    {0, 619, -1, 636, 18},
    {4, 620, -1, 634, 2},
    {8, 621, -1, 635, 2},
    {0, 624, -1, 636, 18},
    {4, 625, -1, 634, 2},
    {8, 626, -1, 635, 2},
    {0, 629, -1, 636, 18},
    {4, 630, -1, 634, 2},
    {8, 631, -1, 635, 2},
    {0, 634, -1, 636, 18},
    {4, 635, -1, 634, 2},
    {8, 636, -1, 635, 2},
    {0, 639, -1, 636, 18},
    {4, 640, -1, 634, 2},
    {8, 641, -1, 635, 2},
    {0, 644, -1, 636, 18},
    {4, 645, -1, 634, 2},
    {8, 646, -1, 635, 2},
    {0, 649, -1, 636, 18},
    {4, 650, -1, 634, 2},
    {8, 651, -1, 635, 2},
    {0, 654, -1, 636, 18},
    {4, 655, -1, 634, 2},
    {8, 656, -1, 635, 2},
    {0, 659, -1, 636, 18},
    {4, 660, -1, 634, 2},
    {8, 661, -1, 635, 2},
    {0, 664, -1, 636, 18},
    {4, 665, -1, 634, 2},
    {8, 666, -1, 635, 2},
    {0, 669, -1, 636, 18},
    {4, 670, -1, 634, 2},
    {8, 671, -1, 635, 2},
    {0, 674, -1, 636, 18},
    {4, 675, -1, 634, 2},
    {8, 676, -1, 635, 2},
    {0, 682, -1, 636, 18},
    {4, 683, -1, 634, 2},
    {8, 684, -1, 635, 2},
    {0, 687, -1, 636, 18},
    {4, 688, -1, 634, 2},
    {8, 689, -1, 635, 2},
    {0, 692, -1, 636, 18},
    {4, 693, -1, 634, 2},
    {8, 694, -1, 635, 2},
    {0, 696, -1, 636, 18},
    {4, 697, -1, 634, 2},
    {8, 698, -1, 635, 2},
    {0, 701, -1, 636, 18},
    {4, 702, -1, 634, 2},
    {8, 703, -1, 635, 2},
    {0, 706, -1, 636, 18},
    {4, 707, -1, 634, 2},
    {8, 708, -1, 635, 2},
    {0, 711, -1, 636, 18},
    {4, 712, -1, 634, 2},
    {8, 713, -1, 635, 2},
    {0, 716, -1, 636, 18},
    {4, 717, -1, 634, 2},
    {8, 718, -1, 635, 2},
    {0, 721, -1, 636, 18},
    {4, 722, -1, 634, 2},
    {8, 723, -1, 635, 2},
    {0, 726, -1, 636, 18},
    {4, 727, -1, 634, 2},
    {8, 728, -1, 635, 2},
    {0, 731, -1, 636, 18},
    {4, 732, -1, 634, 2},
    {8, 733, -1, 635, 2},
    {0, 736, -1, 636, 18},
    {4, 737, -1, 634, 2},
    {8, 738, -1, 635, 2},
    {0, 741, -1, 636, 18},
    {4, 742, -1, 634, 2},
    {8, 743, -1, 635, 2},
    {0, 746, -1, 636, 18},
    {4, 747, -1, 634, 2},
    {8, 748, -1, 635, 2},
    {0, 751, -1, 636, 18},
    {4, 752, -1, 634, 2},
    {8, 753, -1, 635, 2},
    {0, 756, -1, 636, 18},
    {4, 757, -1, 634, 2},
    {8, 758, -1, 635, 2},
    {0, 761, -1, 636, 18},
    {4, 762, -1, 634, 2},
    {8, 763, -1, 635, 2},
    {0, 766, -1, 636, 18},
    {4, 767, -1, 634, 2},
    {8, 768, -1, 635, 2},
    {0, 771, -1, 636, 18},
    {4, 772, -1, 634, 2},
    {8, 773, -1, 635, 2},
    {0, 776, -1, 636, 18},
    {4, 777, -1, 634, 2},
    {8, 778, -1, 635, 2},
    {0, 781, -1, 636, 18},
    {4, 782, -1, 634, 2},
    {8, 783, -1, 635, 2},
    {0, 786, -1, 636, 18},
    {4, 787, -1, 634, 2},
    {8, 788, -1, 635, 2},
    {0, 791, -1, 636, 18},
    {4, 792, -1, 634, 2},
    {8, 793, -1, 635, 2},
    {0, 796, -1, 636, 18},
    {4, 797, -1, 634, 2},
    {8, 798, -1, 635, 2},
    {0, 801, -1, 636, 18},
    {4, 802, -1, 634, 2},
    {8, 803, -1, 635, 2},
    {0, 806, -1, 636, 18},
    {4, 807, -1, 634, 2},
    {8, 808, -1, 635, 2},
    {0, 811, -1, 636, 18},
    {4, 812, -1, 634, 2},
    {8, 813, -1, 635, 2},
    {0, 816, -1, 636, 18},
    {4, 817, -1, 634, 2},
    {8, 818, -1, 635, 2},
    {0, 821, -1, 636, 18},
    {4, 822, -1, 634, 2},
    {8, 823, -1, 635, 2},
    {0, 826, -1, 636, 18},
    {4, 827, -1, 634, 2},
    {8, 828, -1, 635, 2},
    {0, 835, -1, 636, 18},
    {4, 836, -1, 634, 2},
    {8, 837, -1, 637, 2},
    {0, 845, -1, 636, 18},
    {4, 846, -1, 634, 2},
    {8, 847, -1, 637, 2},
    {4, 842, -1, 648, 2},
    {20, 843, -1, 649, 2},
    {24, 844, -1, 650, 2},
    {28, 849, 0, 517, 3},
    {0, 857, -1, 636, 18},
    {4, 858, -1, 634, 2},
    {8, 859, -1, 637, 2},
    {4, 853, -1, 648, 2},
    {20, 854, 0, 649, 3},
    {24, 855, -1, 651, 2},
    {26, 856, 1, 652, 3},
    {28, 861, 2, 517, 3},
    {4, 863, -1, 653, 2},
    {4, 864, 0, 607, 10},
    {4, 865, 1, 654, 10},
    {0, 869, -1, 636, 18},
    {4, 870, -1, 634, 2},
    {8, 871, -1, 637, 2},
    {4, 867, -1, 655, 2},
    {12, 868, 0, 656, 3},
    {48, 873, 1, 517, 3},
    {0, 876, -1, 636, 18},
    {4, 877, -1, 634, 2},
    {8, 878, -1, 635, 2},
    {0, 881, -1, 636, 18},
    {4, 882, -1, 634, 2},
    {8, 883, -1, 635, 2},
    {0, 886, -1, 636, 18},
    {4, 887, -1, 634, 2},
    {8, 888, -1, 635, 2},
    {4, 891, -1, 657, 2},
    {4, 892, -1, 658, 2},
    {0, 894, -1, 636, 18},
    {12, 895, -1, 634, 2},
    {16, 896, -1, 635, 2},
    {0, 899, -1, 636, 18},
    {4, 900, -1, 634, 2},
    {8, 901, -1, 635, 2},
    {0, 904, -1, 636, 18},
    {4, 905, -1, 634, 2},
    {8, 906, -1, 635, 2},
    {0, 909, -1, 636, 18},
    {4, 910, -1, 634, 2},
    {8, 911, -1, 635, 2},
    {0, 914, -1, 636, 18},
    {4, 915, -1, 634, 2},
    {8, 916, -1, 635, 2},
    {0, 919, -1, 636, 18},
    {4, 920, -1, 634, 2},
    {8, 921, -1, 635, 2},
    {0, 924, -1, 636, 18},
    {4, 925, -1, 634, 2},
    {8, 926, -1, 635, 2},
    {0, 929, -1, 636, 18},
    {4, 930, -1, 634, 2},
    {8, 931, -1, 635, 2},
    {0, 934, -1, 636, 18},
    {4, 935, -1, 634, 2},
    {8, 936, -1, 635, 2},
    {0, 939, -1, 636, 18},
    {4, 940, -1, 634, 2},
    {8, 941, -1, 635, 2},
    {0, 946, -1, 636, 18},
    {4, 947, -1, 634, 2},
    {8, 948, -1, 637, 2},
    {0, 951, -1, 636, 18},
    {4, 952, -1, 634, 2},
    {8, 953, -1, 635, 2},
    {0, 958, -1, 636, 18},
    {4, 959, -1, 634, 2},
    {8, 960, -1, 637, 2},
    {0, 963, -1, 636, 18},
    {4, 964, -1, 634, 2},
    {8, 965, -1, 635, 2},
    {0, 968, -1, 636, 18},
    {4, 969, -1, 634, 2},
    {8, 970, -1, 635, 2},
    {0, 973, -1, 636, 18},
    {4, 974, -1, 634, 2},
    {8, 975, -1, 637, 2},
    {0, 979, -1, 636, 18},
    {4, 980, -1, 634, 2},
    {8, 981, -1, 637, 2},
    {4, 978, -1, 659, 2},
    {8, 983, 0, 517, 3},
    {0, 986, -1, 636, 18},
    {4, 987, -1, 634, 2},
    {8, 988, -1, 637, 2},
    {4, 985, -1, 660, 2},
    {8, 990, 0, 517, 3},
    {0, 993, -1, 636, 18},
    {4, 994, -1, 634, 2},
    {8, 995, -1, 637, 2},
    {4, 992, -1, 661, 2},
    {8, 997, 0, 517, 3},
    {4, 999, -1, 662, 2},
    {4, 1000, -1, 663, 2},
    {4, 1001, -1, 664, 2},
    {4, 1002, 0, 665, 10},
    {0, 1004, -1, 636, 18},
    {4, 1005, -1, 634, 2},
    {8, 1006, -1, 635, 2},
    {0, 1013, -1, 636, 18},
    {4, 1014, -1, 634, 2},
    {8, 1015, -1, 637, 2},
    {2, 1011, -1, 666, 2},
    {4, 1012, -1, 667, 2},
    {8, 1017, 0, 517, 3},
    {0, 1024, -1, 636, 18},
    {4, 1025, -1, 634, 2},
    {8, 1026, -1, 637, 2},
    {0, 1039, -1, 636, 18},
    {4, 1040, -1, 634, 2},
    {8, 1041, -1, 637, 2},
    {0, 1047, -1, 636, 18},
    {4, 1048, -1, 634, 2},
    {8, 1049, -1, 637, 2},
    {4, 1045, -1, 668, 2},
    {24, 1046, -1, 669, 2},
    {28, 1051, 0, 517, 3},
    {0, 1056, -1, 636, 18},
    {4, 1057, -1, 634, 2},
    {8, 1058, -1, 637, 2},
    {4, 1054, -1, 668, 2},
    {24, 1055, -1, 669, 2},
    {28, 1060, 0, 517, 3},
    {0, 1064, -1, 636, 18},
    {4, 1065, -1, 634, 2},
    {8, 1066, -1, 637, 2},
    {4, 1063, -1, 668, 2},
    {24, 1068, 0, 517, 3},
    {0, 1073, -1, 636, 18},
    {4, 1074, -1, 634, 2},
    {8, 1075, -1, 637, 2},
    {4, 1071, -1, 668, 2},
    {24, 1072, -1, 669, 2},
    {28, 1077, 0, 517, 3},
    {0, 1087, -1, 636, 18},
    {4, 1088, -1, 634, 2},
    {8, 1089, -1, 637, 2},
    {0, 1094, -1, 636, 18},
    {4, 1095, -1, 634, 2},
    {8, 1096, -1, 637, 2},
    {4, 1093, -1, 670, 2},
    {8, 1098, 0, 517, 3},
    {0, 1104, -1, 636, 18},
    {4, 1105, -1, 634, 2},
    {8, 1106, -1, 637, 2},
    {2, 1100, -1, 554, 2},
    {8, 1101, -1, 569, 2},
    {12, 1102, -1, 650, 2},
    {16, 1103, 0, 649, 3},
    {20, 1108, 1, 517, 3},
    {0, 1111, -1, 636, 18},
    {4, 1112, -1, 634, 2},
    {8, 1113, -1, 637, 2},
    {4, 1110, -1, 671, 2},
    {12, 1115, 0, 517, 3},
    {0, 1121, -1, 636, 18},
    {4, 1122, -1, 634, 2},
    {8, 1123, -1, 637, 2},
    {0, 1131, -1, 636, 18},
    {4, 1132, -1, 634, 2},
    {8, 1133, -1, 637, 2},
    {0, 1140, -1, 636, 18},
    {4, 1141, -1, 634, 2},
    {8, 1142, -1, 637, 2},
    {4, 1137, -1, 672, 2},
    {8, 1138, -1, 673, 2},
    {12, 1139, -1, 674, 2},
    {16, 1144, 0, 517, 3},
    {0, 1154, -1, 636, 18},
    {4, 1155, -1, 634, 2},
    {8, 1156, -1, 637, 2},
    {2, 1152, -1, 675, 2},
    {8, 1153, -1, 676, 2},
    {12, 1158, 0, 517, 3},
    {0, 1163, -1, 636, 18},
    {4, 1164, -1, 634, 2},
    {8, 1165, -1, 637, 2},
    {2, 1161, -1, 675, 2},
    {8, 1162, -1, 677, 2},
    {12, 1167, 0, 517, 3},
    {0, 1171, -1, 636, 18},
    {4, 1172, -1, 634, 2},
    {8, 1173, -1, 637, 2},
    {4, 1170, -1, 668, 2},
    {24, 1175, 0, 517, 3},
    {4, 1179, -1, 678, 2},
    {4, 1180, -1, 679, 2},
    {0, 1184, -1, 636, 18},
    {4, 1185, -1, 634, 2},
    {8, 1186, -1, 637, 2},
    {0, 1194, -1, 636, 18},
    {4, 1195, -1, 634, 2},
    {8, 1196, -1, 637, 2},
    {0, 1200, -1, 636, 18},
    {4, 1201, -1, 634, 2},
    {8, 1202, -1, 637, 2},
    {0, 1209, -1, 636, 18},
    {4, 1210, -1, 634, 2},
    {8, 1211, -1, 635, 2},
    {0, 1216, -1, 636, 18},
    {4, 1217, -1, 634, 2},
    {8, 1218, -1, 637, 2},
    {0, 1221, -1, 636, 18},
    {4, 1222, -1, 634, 2},
    {8, 1223, -1, 635, 2},
    {0, 1227, -1, 636, 18},
    {4, 1228, -1, 634, 2},
    {8, 1229, -1, 637, 2},
    {0, 1238, -1, 636, 18},
    {4, 1239, -1, 634, 2},
    {8, 1240, -1, 637, 2},
    {4, 1235, 0, 680, 3},
    {8, 1236, 1, 681, 3},
    {12, 1237, 2, 682, 3},
    {16, 1242, 3, 517, 3},
    {0, 1247, -1, 636, 18},
    {4, 1248, -1, 634, 2},
    {8, 1249, -1, 637, 2},
    {0, 1255, -1, 636, 18},
    {4, 1256, -1, 634, 2},
    {8, 1257, -1, 637, 2},
    {2, 1253, -1, 561, 2},
    {8, 1254, -1, 683, 2},
    {12, 1259, 0, 517, 3},
    {0, 1265, -1, 636, 18},
    {4, 1266, -1, 634, 2},
    {8, 1267, -1, 637, 2},
    {2, 1263, -1, 561, 2},
    {8, 1264, -1, 684, 2},
    {12, 1269, 0, 517, 3},
    {0, 1278, -1, 636, 18},
    {4, 1279, -1, 634, 2},
    {8, 1280, -1, 637, 2},
    {2, 1287, -1, 685, 2},
    {2, 1288, -1, 686, 2},
    {0, 1291, -1, 636, 18},
    {4, 1292, -1, 634, 2},
    {8, 1293, -1, 637, 2},
    {2, 1290, -1, 687, 2},
    {8, 1295, 0, 517, 3},
    {0, 1301, -1, 636, 18},
    {4, 1302, -1, 634, 2},
    {8, 1303, -1, 637, 2},
    {4, 1299, -1, 688, 2},
    {8, 1300, -1, 689, 2},
    {12, 1305, 0, 517, 3},
    {0, 1311, -1, 636, 18},
    {4, 1312, -1, 634, 2},
    {8, 1313, -1, 637, 2},
    {4, 1307, -1, 690, 2},
    {12, 1308, -1, 691, 2},
    {16, 1309, 0, 692, 3},
    {28, 1310, 1, 693, 3},
    {44, 1315, 2, 517, 3},
    {0, 1324, -1, 636, 18},
    {4, 1325, -1, 634, 2},
    {8, 1326, -1, 637, 2},
    {4, 1321, -1, 694, 2},
    {24, 1322, -1, 695, 2},
    {36, 1323, -1, 696, 2},
    {40, 1328, 0, 517, 3},
    {2, 1331, -1, 697, 2},
    {4, 1333, -1, 698, 2},
    {4, 1334, -1, 699, 2},
    {4, 1335, -1, 700, 2},
    {0, 1340, -1, 636, 18},
    {4, 1341, -1, 634, 2},
    {8, 1342, -1, 637, 2},
    {4, 1338, -1, 701, 2},
    {8, 1339, -1, 702, 2},
    {12, 1344, 0, 517, 3},
    {0, 1350, -1, 636, 18},
    {4, 1351, -1, 634, 2},
    {8, 1352, -1, 637, 2},
    {4, 1348, -1, 570, 2},
    {8, 1349, -1, 571, 2},
    {12, 1354, 0, 517, 3},
    {0, 1359, -1, 636, 18},
    {4, 1360, -1, 634, 2},
    {8, 1361, -1, 637, 2},
    {0, 1366, -1, 636, 18},
    {4, 1367, -1, 634, 2},
    {8, 1368, -1, 637, 2},
    {0, 1374, -1, 636, 18},
    {4, 1375, -1, 634, 2},
    {8, 1376, -1, 637, 2},
    {0, 1382, -1, 636, 18},
    {4, 1383, -1, 634, 2},
    {8, 1384, -1, 637, 2},
    {0, 1388, -1, 636, 18},
    {4, 1389, -1, 634, 2},
    {8, 1390, -1, 635, 2},
    {4, 1392, -1, 703, 2},
    {4, 1393, -1, 704, 2},
    {4, 1394, 0, 705, 10},
    {0, 1399, -1, 636, 18},
    {4, 1400, -1, 634, 2},
    {8, 1401, -1, 637, 2},
    {0, 1406, -1, 636, 18},
    {4, 1407, -1, 634, 2},
    {8, 1408, -1, 637, 2},
    {2, 1404, 0, 706, 3},
    {4, 1405, -1, 707, 2},
    {8, 1410, 1, 517, 3},
    {0, 1416, -1, 636, 18},
    {4, 1417, -1, 634, 2},
    {8, 1418, -1, 637, 2},
    {0, 1428, -1, 636, 18},
    {4, 1429, -1, 634, 2},
    {8, 1430, -1, 637, 2},
    {0, 1436, -1, 636, 18},
    {4, 1437, -1, 634, 2},
    {8, 1438, -1, 637, 2},
    {0, 1445, -1, 636, 18},
    {4, 1446, -1, 634, 2},
    {8, 1447, -1, 637, 2},
    {4, 1443, -1, 708, 2},
    {12, 1444, 0, 709, 3},
    {20, 1449, 1, 517, 3},
    {0, 1454, -1, 636, 18},
    {4, 1455, -1, 634, 2},
    {8, 1456, -1, 637, 2},
    {0, 1461, -1, 636, 18},
    {4, 1462, -1, 634, 2},
    {8, 1463, -1, 637, 2},
    {0, 1467, -1, 636, 18},
    {4, 1468, -1, 634, 2},
    {8, 1469, -1, 637, 2},
    {4, 1466, 0, 710, 3},
    {16, 1471, 1, 517, 3},
    {0, 1473, -1, 636, 18},
    {4, 1474, -1, 634, 2},
    {8, 1475, -1, 635, 2},
    {4, 1477, -1, 711, 2},
    {4, 1478, -1, 712, 2},
    {4, 1479, 0, 713, 10},
    {0, 1484, -1, 636, 18},
    {4, 1485, -1, 634, 2},
    {8, 1486, -1, 637, 2},
    {4, 1482, -1, 714, 2},
    {28, 1483, -1, 715, 2},
    {44, 1488, 0, 517, 3},
    {0, 1492, -1, 636, 18},
    {4, 1493, -1, 634, 2},
    {8, 1494, -1, 637, 2},
    {4, 1490, -1, 714, 2},
    {28, 1491, -1, 715, 2},
    {44, 1496, 0, 517, 3},
    {0, 1501, -1, 636, 18},
    {4, 1502, -1, 634, 2},
    {8, 1503, -1, 637, 2},
    {0, 1510, -1, 636, 18},
    {4, 1511, -1, 634, 2},
    {8, 1512, -1, 637, 2},
    {0, 1520, -1, 636, 18},
    {4, 1521, -1, 634, 2},
    {8, 1522, -1, 637, 2},
    {0, 1528, -1, 636, 18},
    {4, 1529, -1, 634, 2},
    {8, 1530, -1, 637, 2},
    {4, 1525, -1, 716, 2},
    {8, 1526, -1, 717, 2},
    {12, 1527, -1, 718, 2},
    {16, 1532, 0, 517, 3},
    {0, 1539, -1, 636, 18},
    {4, 1540, -1, 634, 2},
    {8, 1541, -1, 637, 2},
    {2, 1537, -1, 606, 2},
    {8, 1538, -1, 719, 2},
    {12, 1543, 0, 517, 3},
    {0, 1548, -1, 636, 18},
    {4, 1549, -1, 634, 2},
    {8, 1550, -1, 637, 2},
    {0, 1555, -1, 636, 18},
    {4, 1556, -1, 634, 2},
    {8, 1557, -1, 637, 2},
    {0, 1564, -1, 636, 18},
    {4, 1565, -1, 634, 2},
    {8, 1566, -1, 637, 2},
    {4, 1562, -1, 525, 2},
    {20, 1563, -1, 720, 2},
    {24, 1568, 0, 517, 3},
    {0, 1573, -1, 636, 18},
    {4, 1574, -1, 634, 2},
    {8, 1575, -1, 637, 2},
    {4, 1571, -1, 525, 2},
    {20, 1572, -1, 721, 2},
    {24, 1577, 0, 517, 3},
    {0, 1582, -1, 636, 18},
    {4, 1583, -1, 634, 2},
    {8, 1584, -1, 637, 2},
    {4, 1581, -1, 668, 2},
    {24, 1586, 0, 517, 3},
    {0, 1593, -1, 636, 18},
    {4, 1594, -1, 634, 2},
    {8, 1595, -1, 637, 2},
    {0, 1605, -1, 636, 18},
    {4, 1606, -1, 634, 2},
    {8, 1607, -1, 637, 2},
    {0, 1612, -1, 636, 18},
    {4, 1613, -1, 634, 2},
    {8, 1614, -1, 637, 2},
    {0, 1619, -1, 636, 18},
    {4, 1620, -1, 634, 2},
    {8, 1621, -1, 637, 2},
    {0, 1626, -1, 636, 18},
    {4, 1627, -1, 634, 2},
    {8, 1628, -1, 637, 2},
    {4, 1624, -1, 617, 2},
    {8, 1625, -1, 618, 2},
    {12, 1630, 0, 517, 3},
    {0, 1636, -1, 636, 18},
    {4, 1637, -1, 634, 2},
    {8, 1638, -1, 637, 2},
    {0, 1646, -1, 636, 18},
    {4, 1647, -1, 634, 2},
    {8, 1648, -1, 637, 2},
    {0, 1653, -1, 636, 18},
    {4, 1654, -1, 634, 2},
    {8, 1655, -1, 637, 2},
    {0, 1663, -1, 636, 18},
    {4, 1664, -1, 634, 2},
    {8, 1665, -1, 637, 2},
    {4, 1661, 0, 722, 3},
    {12, 1662, 1, 723, 3},
    {16, 1667, 2, 517, 3},
    {0, 270, -1, 636, 66},
    {4, 252, -1, 634, 2},
    {8, 253, -1, 724, 2},
    {12, 1670, -1, 725, 2},
    {0, 287, -1, 636, 66},
    {4, 252, -1, 634, 2},
    {8, 253, -1, 726, 2},
    {12, 1670, -1, 725, 2},
    {0, 893, -1, 636, 2},
    {12, 252, -1, 634, 2},
    {16, 253, -1, 724, 2},
    {20, 1670, -1, 725, 2},
    {24, 1673, -1, 727, 66}
};

#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) > 12)
typedef struct ExtParms {
    int           versId;
    const void    *xparm[7];
} ExtParms;
static ExtParms const extParms = {
    25,
    {NULL, NULL, NULL, NULL, (void *)0, (void *)0, (void *)4}
};
#endif /* OSS_SPARTAN_AWARE  > 12 */
#endif /* OSS_SPARTAN_AWARE */

static void * const _enamearray[] = {
    (void *)0,
    (void *)0x1, (void *)_v1613,
    (void *)"hybrid",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"hRPD", (void *)"onexRTT",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"hOSuccess", (void *)"hOFailure",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"true",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"ps", (void *)"cs",
    (void *)0x1, (void *)_v1613,
    (void *)"true",
    (void *)0x1, (void *)_v1613,
    (void *)"cs-fallback-required",
    (void *)0x2, (void *)_v1566, (void *)"cs-fallback-high-priority", (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"no-restriction", (void *)"restriction",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"member", (void *)"not-member",
    (void *)0x1, (void *)_v1613,
    (void *)"directPathAvailable",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"data-Forwarding-not-Possible",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x4a, (void *)_v1574, (void *)_v1575,
    (void *)0x2, (void *)_v1613,
    (void *)"native", (void *)"mapped",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x5, (void *)_v1613,
    (void *)"intralte", (void *)"ltetoutran", (void *)"ltetogeran", (void *)"utrantolte", (void *)"gerantolte",
    (void *)0x1, (void *)_v1673,
    (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"allowed",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"immediate-MDT", (void *)"logged-MDT",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"available", (void *)"unavailable",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1613,
    (void *)"v32", (void *)"v64", (void *)"v128", (void *)"v256",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x8, (void *)_v1613,
    (void *)"priolevel1", (void *)"priolevel2", (void *)"priolevel3", (void *)"priolevel4", (void *)"priolevel5",
    (void *)"priolevel6", (void *)"priolevel7", (void *)"priolevel8",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"ps-service-not-available",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x5, (void *)_v1613,
    (void *)"emergency", (void *)"highPriorityAccess", (void *)"mt-Access", (void *)"mo-Signalling", (void *)"mo-Data",
    (void *)0x2, (void *)_v1600,
    (void *)"delay-TolerantAccess", (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"possible",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"pSandCS", (void *)"cSonly",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x6, (void *)_v1613,
    (void *)"v1s", (void *)"v2s", (void *)"v5s", (void *)"v10s", (void *)"v20s",
    (void *)"v60s",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"supported", (void *)"not-supported",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1613,
    (void *)"reject", (void *)"ignore", (void *)"notify",
    (void *)0x4, (void *)_v1610,
    (void *)"spare", (void *)"highest", (void *)"lowest", (void *)"no-priority",
    (void *)0x2, (void *)_v1613,
    (void *)"shall-not-trigger-pre-emption", (void *)"may-trigger-pre-emption",
    (void *)0x2, (void *)_v1613,
    (void *)"not-pre-emptable", (void *)"pre-emptable",
    (void *)0x24, (void *)_v1613,
    (void *)"unspecified", (void *)"tx2relocoverall-expiry", (void *)"successful-handover", (void *)"release-due-to-eutran-generated-reason", (void *)"handover-cancelled",
    (void *)"partial-handover", (void *)"ho-failure-in-target-EPC-eNB-or-target-system", (void *)"ho-target-not-allowed", (void *)"tS1relocoverall-expiry", (void *)"tS1relocprep-expiry",
    (void *)"cell-not-available", (void *)"unknown-targetID", (void *)"no-radio-resources-available-in-target-cell", (void *)"unknown-mme-ue-s1ap-id", (void *)"unknown-enb-ue-s1ap-id",
    (void *)"unknown-pair-ue-s1ap-id", (void *)"handover-desirable-for-radio-reason", (void *)"time-critical-handover", (void *)"resource-optimisation-handover", (void *)"reduce-load-in-serving-cell",
    (void *)"user-inactivity", (void *)"radio-connection-with-ue-lost", (void *)"load-balancing-tau-required", (void *)"cs-fallback-triggered", (void *)"ue-not-available-for-ps-service",
    (void *)"radio-resources-not-available", (void *)"failure-in-radio-interface-procedure", (void *)"invalid-qos-combination", (void *)"interrat-redirection", (void *)"interaction-with-other-procedure",
    (void *)"unknown-E-RAB-ID", (void *)"multiple-E-RAB-ID-instances", (void *)"encryption-and-or-integrity-protection-algorithms-not-supported", (void *)"s1-intra-system-handover-triggered", (void *)"s1-inter-system-handover-triggered",
    (void *)"x2-handover-triggered",
    (void *)0x4, (void *)_v1614, (void *)"redirection-towards-1xRTT", (void *)"not-supported-QCI-value", (void *)"invalid-CSG-Id", (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"transport-resource-unavailable", (void *)"unspecified",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1613,
    (void *)"normal-release", (void *)"authentication-failure", (void *)"detach", (void *)"unspecified",
    (void *)0x2, (void *)_v1618, (void *)"csg-subscription-expiry",
    (void *)"oss-unknown-enumerator",
    (void *)0x7, (void *)_v1613,
    (void *)"transfer-syntax-error", (void *)"abstract-syntax-error-reject", (void *)"abstract-syntax-error-ignore-and-notify", (void *)"message-not-compatible-with-receiver-state", (void *)"semantic-error",
    (void *)"abstract-syntax-error-falsely-constructed-message", (void *)"unspecified",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x6, (void *)_v1613,
    (void *)"control-processing-overload", (void *)"not-enough-user-plane-processing-resources", (void *)"hardware-failure", (void *)"om-intervention", (void *)"unspecified",
    (void *)"unknown-PLMN",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"reset-all",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1613,
    (void *)"verysmall", (void *)"small", (void *)"medium", (void *)"large",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1613,
    (void *)"initiating-message", (void *)"successful-outcome", (void *)"unsuccessfull-outcome",
    (void *)0x2, (void *)_v1613,
    (void *)"not-understood", (void *)"missing",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"dL-Forwarding-proposed",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1613,
    (void *)"direct", (void *)"change-of-serve-cell", (void *)"stop-change-of-serve-cell",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"subscription-information", (void *)"statistics",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x7, (void *)_v1613,
    (void *)"sec15", (void *)"sec30", (void *)"sec60", (void *)"sec90", (void *)"sec120",
    (void *)"sec180", (void *)"long-time",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1613,
    (void *)"all", (void *)"geran", (void *)"utran", (void *)"cdma2000",
    (void *)0x3, (void *)_v1639, (void *)"geranandutran",
    (void *)"cdma2000andutran", (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"periodic", (void *)"a2eventtriggered",
    (void *)0x2, (void *)_v1641, (void *)"a2eventtriggered-periodic", (void *)"oss-unknown-enumerator",
    (void *)0xd, (void *)_v1613,
    (void *)"ms120", (void *)"ms240", (void *)"ms480", (void *)"ms640", (void *)"ms1024",
    (void *)"ms2048", (void *)"ms5120", (void *)"ms10240", (void *)"min1", (void *)"min6",
    (void *)"min12", (void *)"min30", (void *)"min60",
    (void *)0x8, (void *)_v1613,
    (void *)"r1", (void *)"r2", (void *)"r4", (void *)"r8", (void *)"r16",
    (void *)"r32", (void *)"r64", (void *)"rinfinity",
    (void *)0x3, (void *)_v1613,
    (void *)"uplink", (void *)"downlink", (void *)"both-uplink-and-downlink",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1613,
    (void *)"ms1280", (void *)"ms2560", (void *)"ms5120", (void *)"ms10240",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x8, (void *)_v1613,
    (void *)"ms128", (void *)"ms256", (void *)"ms512", (void *)"ms1024", (void *)"ms2048",
    (void *)"ms3072", (void *)"ms4096", (void *)"ms6144",
    (void *)0x6, (void *)_v1613,
    (void *)"m10", (void *)"m20", (void *)"m40", (void *)"m60", (void *)"m90",
    (void *)"m120",
    (void *)0x3, (void *)_v1613,
    (void *)"ms100", (void *)"ms1000", (void *)"ms10000",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x5, (void *)_v1613,
    (void *)"ms1024", (void *)"ms2048", (void *)"ms5120", (void *)"ms10240", (void *)"min1",
    (void *)0x1, (void *)_v1673,
    (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1613,
    (void *)"immediate-MDT-only", (void *)"immediate-MDT-and-Trace", (void *)"logged-MDT-only",
    (void *)0x2, (void *)_v1657, (void *)"logged-MBSFN-MDT", (void *)"oss-unknown-enumerator",
    (void *)0x5, (void *)_v1613,
    (void *)"ms0", (void *)"ms1280", (void *)"ms2560", (void *)"ms5120", (void *)"ms10240",
    (void *)0x1, (void *)_v1673,
    (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1613,
    (void *)"reject-non-emergency-mo-dt", (void *)"reject-rrc-cr-signalling", (void *)"permit-emergency-sessions-and-mobile-terminated-services-only",
    (void *)0x3, (void *)_v1661, (void *)"permit-high-priority-sessions-and-mobile-terminated-services-only", (void *)"reject-delay-tolerant-access",
    (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"authorized", (void *)"not-authorized",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"ecgi",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1613,
    (void *)"x2TNL-Configuration-Info",
    (void *)0x4, (void *)_v1669, (void *)"time-Synchronisation-Info", (void *)"activate-Muting", (void *)"deactivate-Muting", (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1613,
    (void *)"synchronous", (void *)"asynchronous",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x6, (void *)_v1613,
    (void *)"minimum", (void *)"medium", (void *)"maximum", (void *)"minimumWithoutVendorSpecificExtension", (void *)"mediumWithoutVendorSpecificExtension",
    (void *)"maximumWithoutVendorSpecificExtension",
    (void *)0x1, (void *)_v1673, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1613,
    (void *)"optional", (void *)"conditional", (void *)"mandatory",
    (void *)"initiatingMessage",
    (void *)"successfulOutcome",
    (void *)"unsuccessfulOutcome",
    (void *)"protocolIEs",
    (void *)"e-RAB-ID",
    (void *)"dL-transportLayerAddress",
    (void *)"dL-gTP-TEID",
    (void *)"uL-TransportLayerAddress",
    (void *)"uL-GTP-TEID",
    (void *)"iE-Extensions",
    (void *)"transportLayerAddress",
    (void *)"gTP-TEID",
    (void *)"e-RABlevelQosParameters",
    (void *)"cause",
    (void *)"e-RABlevelQoSParameters",
    (void *)"nAS-PDU",
    (void *)"e-RABLevelQoSParameters",
    (void *)"tAI",
    (void *)"s1-Interface",
    (void *)"partOfS1-Interface",
    (void *)"rIMTransfer",
    (void *)"privateIEs",
    (void *)"dL-GTP-TEID",
    (void *)"uL-COUNTvalue",
    (void *)"dL-COUNTvalue",
    (void *)"receiveStatusofULPDCPSDUs",
    (void *)"cellID-Cancelled",
    (void *)"tAI-Cancelled",
    (void *)"emergencyAreaID-Cancelled",
    (void *)"cellID-Broadcast",
    (void *)"tAI-Broadcast",
    (void *)"emergencyAreaID-Broadcast",
    (void *)"radioNetwork",
    (void *)"transport",
    (void *)"nas",
    (void *)"protocol",
    (void *)"misc",
    (void *)"cdma2000OneXMEID",
    (void *)"cdma2000OneXMSI",
    (void *)"cdma2000OneXPilot",
    (void *)"pDCP-SNExtended",
    (void *)"hFNModified",
    (void *)"procedureCode",
    (void *)"triggeringMessage",
    (void *)"procedureCriticality",
    (void *)"iEsCriticalityDiagnostics",
    (void *)"pLMNidentity",
    (void *)"eNB-ID",
    (void *)"bearers-SubjectToStatusTransferList",
    (void *)"dL-Forwarding",
    (void *)"cell-ID",
    (void *)"expectedActivity",
    (void *)"expectedHOInterval",
    (void *)"pLMN-Identity",
    (void *)"mME-Group-ID",
    (void *)"mME-Code",
    (void *)"servingPLMN",
    (void *)"equivalentPLMNs",
    (void *)"forbiddenTAs",
    (void *)"forbiddenLAs",
    (void *)"forbiddenInterRATs",
    (void *)"lAC",
    (void *)"loggingInterval",
    (void *)"loggingDuration",
    (void *)"mBSFN-ResultToLog",
    (void *)"m3period",
    (void *)"m4period",
    (void *)"m4-links-to-log",
    (void *)"m5period",
    (void *)"m5-links-to-log",
    (void *)"mdt-Activation",
    (void *)"areaScopeOfMDT",
    (void *)"mDTMode",
    (void *)"muting-pattern-period",
    (void *)"muting-pattern-offset",
    (void *)"overloadAction",
    (void *)"proSeDirectDiscovery",
    (void *)"proSeDirectCommunication",
    (void *)"eventType",
    (void *)"reportArea",
    (void *)"nextHopChainingCount",
    (void *)"nextHopParameter",
    (void *)"rLFReportInformation",
    (void *)"targeteNB-ID",
    (void *)"sourceeNB-ID",
    (void *)"sONInformation",
    (void *)"sourceStratumLevel",
    (void *)"listeningSubframePattern",
    (void *)"aggressoreCGI-List",
    (void *)"rRC-Container",
    (void *)"e-RABInformationList",
    (void *)"targetCell-ID",
    (void *)"subscriberProfileIDforRFP",
    (void *)"uE-HistoryInformation",
    (void *)"stratumLevel",
    (void *)"synchronisationStatus",
    (void *)"mMEC",
    (void *)"m-TMSI",
    (void *)"tAC",
    (void *)"targetRNC-ID",
    (void *)"cGI",
    (void *)"e-UTRAN-Trace-ID",
    (void *)"interfacesToTrace",
    (void *)"traceDepth",
    (void *)"traceCollectionEntityIPAddress",
    (void *)"uDP-Port-Number",
    (void *)"uEaggregateMaximumBitRateDL",
    (void *)"uEaggregateMaximumBitRateUL",
    (void *)"uE-S1AP-ID-pair",
    (void *)"mME-UE-S1AP-ID",
    (void *)"eNB-UE-S1AP-ID",
    (void *)"s-TMSI",
    (void *)"iMSI",
    (void *)"encryptionAlgorithms",
    (void *)"integrityProtectionAlgorithms",
    (void *)"eutran-cgi",
    (void *)"tai",
    (void *)"cellIDList",
    (void *)"trackingAreaListforWarning",
    (void *)"emergencyAreaIDList",
    (void *)"eNBX2TransportLayerAddresses",
    (void *)"InitiatingMessage",
    (void *)"SuccessfulOutcome",
    (void *)"UnsuccessfulOutcome",
    (void *)"criticality", (void *)&_v0,
    (void *)"criticality",
    (void *)"value",
    (void *)"id",
    (void *)"extensionValue",
    (void *)"priorityLevel",
    (void *)"pre-emptionCapability",
    (void *)"pre-emptionVulnerability",
    (void *)"e-RAB-MaximumBitrateDL",
    (void *)"e-RAB-MaximumBitrateUL",
    (void *)"e-RAB-GuaranteedBitrateDL",
    (void *)"e-RAB-GuaranteedBitrateUL",
    (void *)"qCI",
    (void *)"allocationRetentionPriority",
    (void *)"gbrQosInformation",
    (void *)"lAI",
    (void *)"rAC",
    (void *)"cI",
    (void *)"rNC-ID",
    (void *)"extendedRNC-ID",
    (void *)"gERAN-Cell-ID",
    (void *)"eHRPD-Sector-ID",
    (void *)"rIMInformation",
    (void *)"rIMRoutingAddress",
    (void *)"local",
    (void *)"global",
    (void *)"cellIdListforMDT",
    (void *)"tAListforMDT",
    (void *)"tAIListforMDT",
    (void *)"cellBased",
    (void *)"tABased",
    (void *)"pLMNWide",
    (void *)"tAIBased",
    (void *)"pDCP-SN",
    (void *)"hFN",
    (void *)"eCGI",
    (void *)"numberOfBroadcasts",
    (void *)"cell-Size",
    (void *)"cSG-Id",
    (void *)"iECriticality",
    (void *)"iE-ID",
    (void *)"typeOfError",
    (void *)"emergencyAreaID",
    (void *)"completedCellinEAI",
    (void *)"cancelledCellinEAI",
    (void *)"macroENB-ID",
    (void *)"homeENB-ID",
    (void *)"expectedActivityPeriod",
    (void *)"expectedIdlePeriod",
    (void *)"sourceofUEActivityBehaviourInformation",
    (void *)"forbiddenTACs",
    (void *)"forbiddenLACs",
    (void *)"threshold-RSRP",
    (void *)"threshold-RSRQ",
    (void *)"measurementThreshold",
    (void *)"reportInterval",
    (void *)"reportAmount",
    (void *)"measurementsToActivate",
    (void *)"m1reportingTrigger",
    (void *)"m1thresholdeventA2",
    (void *)"m1periodicReporting",
    (void *)"global-Cell-ID",
    (void *)"cellType",
    (void *)"time-UE-StayedInCell",
    (void *)"undefined",
    (void *)"e-UTRAN-Cell",
    (void *)"uTRAN-Cell",
    (void *)"gERAN-Cell",
    (void *)"pattern-period",
    (void *)"pattern-offset",
    (void *)"immediateMDT",
    (void *)"loggedMDT",
    (void *)"mDTMode-Extension",
    (void *)"mBSFN-AreaId",
    (void *)"carrierFreq",
    (void *)"uE-RLF-Report-Container",
    (void *)"uE-RLF-Report-Container-for-extended-bands",
    (void *)"x2TNLConfigurationInfo",
    (void *)"sONInformationRequest",
    (void *)"sONInformationReply",
    (void *)"sONInformation-Extension",
    (void *)"global-ENB-ID",
    (void *)"selected-TAI",
    (void *)"servedPLMNs",
    (void *)"servedGroupIDs",
    (void *)"servedMMECs",
    (void *)"broadcastPLMNs",
    (void *)"completedCellinTAI",
    (void *)"cancelledCellinTAI",
    (void *)"iPsecTLA",
    (void *)"gTPTLAa",
    (void *)"Value",
    (void *)"presence",
    (void *)"Extension",
    (void *)"_oss_unique_index"
};

#if !defined(OSS_SPARTAN_AWARE) || ((OSS_SPARTAN_AWARE + 0) <= 5)
typedef struct OSetTableEntry {
    void *a;
    unsigned short b;
    void *c;
    unsigned short d;
} OSetTableEntry;
#endif /* OSS_SPARTAN_AWARE  <= 5 */

static struct OSetTableEntry const _objectsettable[] = {
#if defined(OSS_SPARTAN_AWARE) && ((OSS_SPARTAN_AWARE + 0) > 12)
    {(void *)S1AP_ELEMENTARY_PROCEDURES, 254, NULL, 0, NULL, -1, -1},
    {(void *)S1AP_ELEMENTARY_PROCEDURES_CLASS_1, 254, NULL, 0, NULL, -1, -1},
    {(void *)S1AP_ELEMENTARY_PROCEDURES_CLASS_2, 254, NULL, 0, NULL, -1, -1},
    {(void *)HandoverRequiredIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)HandoverCommandIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABDataForwardingItemIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABDataForwardingItem_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)HandoverPreparationFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)HandoverRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSetupItemHOReqIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSetupItemHOReq_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)HandoverRequestAcknowledgeIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABAdmittedItemIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABAdmittedItem_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABFailedtoSetupItemHOReqAckIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABFailedToSetupItemHOReqAckExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)HandoverFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)HandoverNotifyIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)PathSwitchRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSwitchedDLItemIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSwitchedDLItem_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)PathSwitchRequestAcknowledgeIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSwitchedULItemIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSwitchedULItem_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)PathSwitchRequestFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)HandoverCancelIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)HandoverCancelAcknowledgeIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABSetupRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSetupItemBearerSUReqIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSetupItemBearerSUReqExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABSetupResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABSetupItemBearerSUResIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABSetupItemBearerSUResExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModifyRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeModifiedItemBearerModReqIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeModifyItemBearerModReqExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModifyResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModifyItemBearerModResIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModifyItemBearerModResExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABReleaseCommandIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABReleaseResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABReleaseItemBearerRelCompIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABReleaseItemBearerRelCompExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABReleaseIndicationIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)InitialContextSetupRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSetupItemCtxtSUReqIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeSetupItemCtxtSUReqExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)InitialContextSetupResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABSetupItemCtxtSUResIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABSetupItemCtxtSUResExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)InitialContextSetupFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)PagingIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)TAIItemIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)TAIItemExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)UEContextReleaseRequest_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UEContextReleaseCommand_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UEContextReleaseComplete_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UEContextModificationRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UEContextModificationResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UEContextModificationFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UERadioCapabilityMatchRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UERadioCapabilityMatchResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)DownlinkNASTransport_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)InitialUEMessage_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UplinkNASTransport_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)NASNonDeliveryIndication_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ResetIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UE_associatedLogicalS1_ConnectionItemRes, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ResetAcknowledgeIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UE_associatedLogicalS1_ConnectionItemResAck, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ErrorIndicationIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)S1SetupRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)S1SetupResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)S1SetupFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ENBConfigurationUpdateIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ENBConfigurationUpdateAcknowledgeIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ENBConfigurationUpdateFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)MMEConfigurationUpdateIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)MMEConfigurationUpdateAcknowledgeIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)MMEConfigurationUpdateFailureIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)DownlinkS1cdma2000tunnellingIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UplinkS1cdma2000tunnellingIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UECapabilityInfoIndicationIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ENBStatusTransferIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)MMEStatusTransferIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)TraceStartIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)TraceFailureIndicationIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)DeactivateTraceIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)CellTrafficTraceIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)LocationReportingControlIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)LocationReportingFailureIndicationIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)LocationReportIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)OverloadStartIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)OverloadStopIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)WriteReplaceWarningRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)WriteReplaceWarningResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ENBDirectInformationTransferIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)MMEDirectInformationTransferIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)ENBConfigurationTransferIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)MMEConfigurationTransferIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)PrivateMessageIEs, 1674, NULL, 0, NULL, -1, -1},
    {(void *)KillRequestIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)KillResponseIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)PWSRestartIndicationIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)DownlinkUEAssociatedLPPaTransport_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UplinkUEAssociatedLPPaTransport_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)DownlinkNonUEAssociatedLPPaTransport_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)UplinkNonUEAssociatedLPPaTransport_IEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModificationIndicationIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeModifiedItemBearerModIndIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABToBeModifiedItemBearerModInd_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABNotToBeModifiedItemBearerModIndIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABNotToBeModifiedItemBearerModInd_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModificationConfirmIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModifyItemBearerModConfIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABModifyItemBearerModConfExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)AllocationAndRetentionPriority_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)Bearers_SubjectToStatusTransfer_ItemIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)Bearers_SubjectToStatusTransfer_ItemExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CancelledCellinEAI_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CancelledCellinTAI_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CellID_Broadcast_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CellID_Cancelled_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CellBasedMDT_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)Cdma2000OneXSRVCCInfo_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CellType_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CGI_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CSG_IdList_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)COUNTvalue_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)COUNTValueExtended_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CriticalityDiagnostics_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CriticalityDiagnostics_IE_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)EmergencyAreaID_Broadcast_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)EmergencyAreaID_Cancelled_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CompletedCellinEAI_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)GERAN_Cell_ID_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)GlobalENB_ID_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ENB_StatusTransfer_TransparentContainer_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABInformationListIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABInformationListItem_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABItemIEs, 1671, NULL, 0, NULL, -1, -1},
    {(void *)E_RABItem_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)E_RABQoSParameters_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)EUTRAN_CGI_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ExpectedUEBehaviour_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ExpectedUEActivityBehaviour_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ForbiddenTAs_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ForbiddenLAs_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)GBR_QosInformation_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)GUMMEI_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)HandoverRestrictionList_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ImmediateMDT_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)LAI_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)LastVisitedEUTRANCellInformation_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ListeningSubframePattern_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)LoggedMDT_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)LoggedMBSFNMDT_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)M3Configuration_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)M4Configuration_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)M5Configuration_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)MDT_Configuration_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)MBSFN_ResultToLogInfo_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)MutingPatternInformation_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)M1PeriodicReporting_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ProSeAuthorized_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)RequestType_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)RIMTransfer_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)RLFReportInformation_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)SecurityContext_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)SONInformationReply_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)SONConfigurationTransfer_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)SynchronisationInformation_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)SourceeNB_ID_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ServedGUMMEIsItem_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)SupportedTAs_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TimeSynchronisationInfo_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)S_TMSI_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TAIBasedMDT_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TAI_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TAI_Broadcast_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TAI_Cancelled_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TABasedMDT_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)CompletedCellinTAI_Item_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TargeteNB_ID_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TargetRNC_ID_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TargeteNB_ToSourceeNB_TransparentContainer_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)M1ThresholdEventA2_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)TraceActivation_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)Tunnel_Information_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)UEAggregate_MaximumBitrates_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)UE_S1AP_ID_pair_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)UE_associatedLogicalS1_ConnectionItemExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)UESecurityCapabilities_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)UserLocationInformation_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)X2TNLConfigurationInfo_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)ENBX2ExtTLA_ExtIEs, 1672, NULL, 0, NULL, -1, -1},
    {(void *)MDTMode_ExtensionIE, 1671, NULL, 0, NULL, -1, -1},
    {(void *)SONInformation_ExtensionIE, 1671, NULL, 0, NULL, -1, -1}
#else
    {(void *)S1AP_ELEMENTARY_PROCEDURES, 254, NULL, 0},
    {(void *)S1AP_ELEMENTARY_PROCEDURES_CLASS_1, 254, NULL, 0},
    {(void *)S1AP_ELEMENTARY_PROCEDURES_CLASS_2, 254, NULL, 0},
    {(void *)HandoverRequiredIEs, 1671, NULL, 0},
    {(void *)HandoverCommandIEs, 1671, NULL, 0},
    {(void *)E_RABDataForwardingItemIEs, 1671, NULL, 0},
    {(void *)E_RABDataForwardingItem_ExtIEs, 1672, NULL, 0},
    {(void *)HandoverPreparationFailureIEs, 1671, NULL, 0},
    {(void *)HandoverRequestIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSetupItemHOReqIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSetupItemHOReq_ExtIEs, 1672, NULL, 0},
    {(void *)HandoverRequestAcknowledgeIEs, 1671, NULL, 0},
    {(void *)E_RABAdmittedItemIEs, 1671, NULL, 0},
    {(void *)E_RABAdmittedItem_ExtIEs, 1672, NULL, 0},
    {(void *)E_RABFailedtoSetupItemHOReqAckIEs, 1671, NULL, 0},
    {(void *)E_RABFailedToSetupItemHOReqAckExtIEs, 1672, NULL, 0},
    {(void *)HandoverFailureIEs, 1671, NULL, 0},
    {(void *)HandoverNotifyIEs, 1671, NULL, 0},
    {(void *)PathSwitchRequestIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSwitchedDLItemIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSwitchedDLItem_ExtIEs, 1672, NULL, 0},
    {(void *)PathSwitchRequestAcknowledgeIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSwitchedULItemIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSwitchedULItem_ExtIEs, 1672, NULL, 0},
    {(void *)PathSwitchRequestFailureIEs, 1671, NULL, 0},
    {(void *)HandoverCancelIEs, 1671, NULL, 0},
    {(void *)HandoverCancelAcknowledgeIEs, 1671, NULL, 0},
    {(void *)E_RABSetupRequestIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSetupItemBearerSUReqIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSetupItemBearerSUReqExtIEs, 1672, NULL, 0},
    {(void *)E_RABSetupResponseIEs, 1671, NULL, 0},
    {(void *)E_RABSetupItemBearerSUResIEs, 1671, NULL, 0},
    {(void *)E_RABSetupItemBearerSUResExtIEs, 1672, NULL, 0},
    {(void *)E_RABModifyRequestIEs, 1671, NULL, 0},
    {(void *)E_RABToBeModifiedItemBearerModReqIEs, 1671, NULL, 0},
    {(void *)E_RABToBeModifyItemBearerModReqExtIEs, 1672, NULL, 0},
    {(void *)E_RABModifyResponseIEs, 1671, NULL, 0},
    {(void *)E_RABModifyItemBearerModResIEs, 1671, NULL, 0},
    {(void *)E_RABModifyItemBearerModResExtIEs, 1672, NULL, 0},
    {(void *)E_RABReleaseCommandIEs, 1671, NULL, 0},
    {(void *)E_RABReleaseResponseIEs, 1671, NULL, 0},
    {(void *)E_RABReleaseItemBearerRelCompIEs, 1671, NULL, 0},
    {(void *)E_RABReleaseItemBearerRelCompExtIEs, 1672, NULL, 0},
    {(void *)E_RABReleaseIndicationIEs, 1671, NULL, 0},
    {(void *)InitialContextSetupRequestIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSetupItemCtxtSUReqIEs, 1671, NULL, 0},
    {(void *)E_RABToBeSetupItemCtxtSUReqExtIEs, 1672, NULL, 0},
    {(void *)InitialContextSetupResponseIEs, 1671, NULL, 0},
    {(void *)E_RABSetupItemCtxtSUResIEs, 1671, NULL, 0},
    {(void *)E_RABSetupItemCtxtSUResExtIEs, 1672, NULL, 0},
    {(void *)InitialContextSetupFailureIEs, 1671, NULL, 0},
    {(void *)PagingIEs, 1671, NULL, 0},
    {(void *)TAIItemIEs, 1671, NULL, 0},
    {(void *)TAIItemExtIEs, 1672, NULL, 0},
    {(void *)UEContextReleaseRequest_IEs, 1671, NULL, 0},
    {(void *)UEContextReleaseCommand_IEs, 1671, NULL, 0},
    {(void *)UEContextReleaseComplete_IEs, 1671, NULL, 0},
    {(void *)UEContextModificationRequestIEs, 1671, NULL, 0},
    {(void *)UEContextModificationResponseIEs, 1671, NULL, 0},
    {(void *)UEContextModificationFailureIEs, 1671, NULL, 0},
    {(void *)UERadioCapabilityMatchRequestIEs, 1671, NULL, 0},
    {(void *)UERadioCapabilityMatchResponseIEs, 1671, NULL, 0},
    {(void *)DownlinkNASTransport_IEs, 1671, NULL, 0},
    {(void *)InitialUEMessage_IEs, 1671, NULL, 0},
    {(void *)UplinkNASTransport_IEs, 1671, NULL, 0},
    {(void *)NASNonDeliveryIndication_IEs, 1671, NULL, 0},
    {(void *)ResetIEs, 1671, NULL, 0},
    {(void *)UE_associatedLogicalS1_ConnectionItemRes, 1671, NULL, 0},
    {(void *)ResetAcknowledgeIEs, 1671, NULL, 0},
    {(void *)UE_associatedLogicalS1_ConnectionItemResAck, 1671, NULL, 0},
    {(void *)ErrorIndicationIEs, 1671, NULL, 0},
    {(void *)S1SetupRequestIEs, 1671, NULL, 0},
    {(void *)S1SetupResponseIEs, 1671, NULL, 0},
    {(void *)S1SetupFailureIEs, 1671, NULL, 0},
    {(void *)ENBConfigurationUpdateIEs, 1671, NULL, 0},
    {(void *)ENBConfigurationUpdateAcknowledgeIEs, 1671, NULL, 0},
    {(void *)ENBConfigurationUpdateFailureIEs, 1671, NULL, 0},
    {(void *)MMEConfigurationUpdateIEs, 1671, NULL, 0},
    {(void *)MMEConfigurationUpdateAcknowledgeIEs, 1671, NULL, 0},
    {(void *)MMEConfigurationUpdateFailureIEs, 1671, NULL, 0},
    {(void *)DownlinkS1cdma2000tunnellingIEs, 1671, NULL, 0},
    {(void *)UplinkS1cdma2000tunnellingIEs, 1671, NULL, 0},
    {(void *)UECapabilityInfoIndicationIEs, 1671, NULL, 0},
    {(void *)ENBStatusTransferIEs, 1671, NULL, 0},
    {(void *)MMEStatusTransferIEs, 1671, NULL, 0},
    {(void *)TraceStartIEs, 1671, NULL, 0},
    {(void *)TraceFailureIndicationIEs, 1671, NULL, 0},
    {(void *)DeactivateTraceIEs, 1671, NULL, 0},
    {(void *)CellTrafficTraceIEs, 1671, NULL, 0},
    {(void *)LocationReportingControlIEs, 1671, NULL, 0},
    {(void *)LocationReportingFailureIndicationIEs, 1671, NULL, 0},
    {(void *)LocationReportIEs, 1671, NULL, 0},
    {(void *)OverloadStartIEs, 1671, NULL, 0},
    {(void *)OverloadStopIEs, 1671, NULL, 0},
    {(void *)WriteReplaceWarningRequestIEs, 1671, NULL, 0},
    {(void *)WriteReplaceWarningResponseIEs, 1671, NULL, 0},
    {(void *)ENBDirectInformationTransferIEs, 1671, NULL, 0},
    {(void *)MMEDirectInformationTransferIEs, 1671, NULL, 0},
    {(void *)ENBConfigurationTransferIEs, 1671, NULL, 0},
    {(void *)MMEConfigurationTransferIEs, 1671, NULL, 0},
    {(void *)PrivateMessageIEs, 1674, NULL, 0},
    {(void *)KillRequestIEs, 1671, NULL, 0},
    {(void *)KillResponseIEs, 1671, NULL, 0},
    {(void *)PWSRestartIndicationIEs, 1671, NULL, 0},
    {(void *)DownlinkUEAssociatedLPPaTransport_IEs, 1671, NULL, 0},
    {(void *)UplinkUEAssociatedLPPaTransport_IEs, 1671, NULL, 0},
    {(void *)DownlinkNonUEAssociatedLPPaTransport_IEs, 1671, NULL, 0},
    {(void *)UplinkNonUEAssociatedLPPaTransport_IEs, 1671, NULL, 0},
    {(void *)E_RABModificationIndicationIEs, 1671, NULL, 0},
    {(void *)E_RABToBeModifiedItemBearerModIndIEs, 1671, NULL, 0},
    {(void *)E_RABToBeModifiedItemBearerModInd_ExtIEs, 1672, NULL, 0},
    {(void *)E_RABNotToBeModifiedItemBearerModIndIEs, 1671, NULL, 0},
    {(void *)E_RABNotToBeModifiedItemBearerModInd_ExtIEs, 1672, NULL, 0},
    {(void *)E_RABModificationConfirmIEs, 1671, NULL, 0},
    {(void *)E_RABModifyItemBearerModConfIEs, 1671, NULL, 0},
    {(void *)E_RABModifyItemBearerModConfExtIEs, 1672, NULL, 0},
    {(void *)AllocationAndRetentionPriority_ExtIEs, 1672, NULL, 0},
    {(void *)Bearers_SubjectToStatusTransfer_ItemIEs, 1671, NULL, 0},
    {(void *)Bearers_SubjectToStatusTransfer_ItemExtIEs, 1672, NULL, 0},
    {(void *)CancelledCellinEAI_Item_ExtIEs, 1672, NULL, 0},
    {(void *)CancelledCellinTAI_Item_ExtIEs, 1672, NULL, 0},
    {(void *)CellID_Broadcast_Item_ExtIEs, 1672, NULL, 0},
    {(void *)CellID_Cancelled_Item_ExtIEs, 1672, NULL, 0},
    {(void *)CellBasedMDT_ExtIEs, 1672, NULL, 0},
    {(void *)Cdma2000OneXSRVCCInfo_ExtIEs, 1672, NULL, 0},
    {(void *)CellType_ExtIEs, 1672, NULL, 0},
    {(void *)CGI_ExtIEs, 1672, NULL, 0},
    {(void *)CSG_IdList_Item_ExtIEs, 1672, NULL, 0},
    {(void *)COUNTvalue_ExtIEs, 1672, NULL, 0},
    {(void *)COUNTValueExtended_ExtIEs, 1672, NULL, 0},
    {(void *)CriticalityDiagnostics_ExtIEs, 1672, NULL, 0},
    {(void *)CriticalityDiagnostics_IE_Item_ExtIEs, 1672, NULL, 0},
    {(void *)EmergencyAreaID_Broadcast_Item_ExtIEs, 1672, NULL, 0},
    {(void *)EmergencyAreaID_Cancelled_Item_ExtIEs, 1672, NULL, 0},
    {(void *)CompletedCellinEAI_Item_ExtIEs, 1672, NULL, 0},
    {(void *)GERAN_Cell_ID_ExtIEs, 1672, NULL, 0},
    {(void *)GlobalENB_ID_ExtIEs, 1672, NULL, 0},
    {(void *)ENB_StatusTransfer_TransparentContainer_ExtIEs, 1672, NULL, 0},
    {(void *)E_RABInformationListIEs, 1671, NULL, 0},
    {(void *)E_RABInformationListItem_ExtIEs, 1672, NULL, 0},
    {(void *)E_RABItemIEs, 1671, NULL, 0},
    {(void *)E_RABItem_ExtIEs, 1672, NULL, 0},
    {(void *)E_RABQoSParameters_ExtIEs, 1672, NULL, 0},
    {(void *)EUTRAN_CGI_ExtIEs, 1672, NULL, 0},
    {(void *)ExpectedUEBehaviour_ExtIEs, 1672, NULL, 0},
    {(void *)ExpectedUEActivityBehaviour_ExtIEs, 1672, NULL, 0},
    {(void *)ForbiddenTAs_Item_ExtIEs, 1672, NULL, 0},
    {(void *)ForbiddenLAs_Item_ExtIEs, 1672, NULL, 0},
    {(void *)GBR_QosInformation_ExtIEs, 1672, NULL, 0},
    {(void *)GUMMEI_ExtIEs, 1672, NULL, 0},
    {(void *)HandoverRestrictionList_ExtIEs, 1672, NULL, 0},
    {(void *)ImmediateMDT_ExtIEs, 1672, NULL, 0},
    {(void *)LAI_ExtIEs, 1672, NULL, 0},
    {(void *)LastVisitedEUTRANCellInformation_ExtIEs, 1672, NULL, 0},
    {(void *)ListeningSubframePattern_ExtIEs, 1672, NULL, 0},
    {(void *)LoggedMDT_ExtIEs, 1672, NULL, 0},
    {(void *)LoggedMBSFNMDT_ExtIEs, 1672, NULL, 0},
    {(void *)M3Configuration_ExtIEs, 1672, NULL, 0},
    {(void *)M4Configuration_ExtIEs, 1672, NULL, 0},
    {(void *)M5Configuration_ExtIEs, 1672, NULL, 0},
    {(void *)MDT_Configuration_ExtIEs, 1672, NULL, 0},
    {(void *)MBSFN_ResultToLogInfo_ExtIEs, 1672, NULL, 0},
    {(void *)MutingPatternInformation_ExtIEs, 1672, NULL, 0},
    {(void *)M1PeriodicReporting_ExtIEs, 1672, NULL, 0},
    {(void *)ProSeAuthorized_ExtIEs, 1672, NULL, 0},
    {(void *)RequestType_ExtIEs, 1672, NULL, 0},
    {(void *)RIMTransfer_ExtIEs, 1672, NULL, 0},
    {(void *)RLFReportInformation_ExtIEs, 1672, NULL, 0},
    {(void *)SecurityContext_ExtIEs, 1672, NULL, 0},
    {(void *)SONInformationReply_ExtIEs, 1672, NULL, 0},
    {(void *)SONConfigurationTransfer_ExtIEs, 1672, NULL, 0},
    {(void *)SynchronisationInformation_ExtIEs, 1672, NULL, 0},
    {(void *)SourceeNB_ID_ExtIEs, 1672, NULL, 0},
    {(void *)SourceeNB_ToTargeteNB_TransparentContainer_ExtIEs, 1672, NULL, 0},
    {(void *)ServedGUMMEIsItem_ExtIEs, 1672, NULL, 0},
    {(void *)SupportedTAs_Item_ExtIEs, 1672, NULL, 0},
    {(void *)TimeSynchronisationInfo_ExtIEs, 1672, NULL, 0},
    {(void *)S_TMSI_ExtIEs, 1672, NULL, 0},
    {(void *)TAIBasedMDT_ExtIEs, 1672, NULL, 0},
    {(void *)TAI_ExtIEs, 1672, NULL, 0},
    {(void *)TAI_Broadcast_Item_ExtIEs, 1672, NULL, 0},
    {(void *)TAI_Cancelled_Item_ExtIEs, 1672, NULL, 0},
    {(void *)TABasedMDT_ExtIEs, 1672, NULL, 0},
    {(void *)CompletedCellinTAI_Item_ExtIEs, 1672, NULL, 0},
    {(void *)TargeteNB_ID_ExtIEs, 1672, NULL, 0},
    {(void *)TargetRNC_ID_ExtIEs, 1672, NULL, 0},
    {(void *)TargeteNB_ToSourceeNB_TransparentContainer_ExtIEs, 1672, NULL, 0},
    {(void *)M1ThresholdEventA2_ExtIEs, 1672, NULL, 0},
    {(void *)TraceActivation_ExtIEs, 1672, NULL, 0},
    {(void *)Tunnel_Information_ExtIEs, 1672, NULL, 0},
    {(void *)UEAggregate_MaximumBitrates_ExtIEs, 1672, NULL, 0},
    {(void *)UE_S1AP_ID_pair_ExtIEs, 1672, NULL, 0},
    {(void *)UE_associatedLogicalS1_ConnectionItemExtIEs, 1672, NULL, 0},
    {(void *)UESecurityCapabilities_ExtIEs, 1672, NULL, 0},
    {(void *)UserLocationInformation_ExtIEs, 1672, NULL, 0},
    {(void *)X2TNLConfigurationInfo_ExtIEs, 1672, NULL, 0},
    {(void *)ENBX2ExtTLA_ExtIEs, 1672, NULL, 0},
    {(void *)MDTMode_ExtensionIE, 1671, NULL, 0},
    {(void *)SONInformation_ExtensionIE, 1671, NULL, 0}
#endif /* OSS_SPARTAN_AWARE  > 12 */
};


#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) >= 3)
static void _oss_post_init(struct ossGlobal *world) {
    static const unsigned char _oss_typeinfo[] = {
        0x00, 0xa2, 0x33, 0x2e, 0x61, 0x27, 0xbf, 0xd8, 0x23, 0xc7,
        0x3a, 0xda, 0x23, 0xc4, 0x3a, 0x0e, 0x11, 0x75, 0xc3, 0xa8,
        0xef, 0x6d, 0x35, 0x66, 0xd6, 0x50, 0x26, 0xf7, 0x99, 0xcd,
        0x48, 0x74, 0x44, 0xb9, 0x1e, 0xff, 0xfb, 0xc1, 0x6f, 0xa4,
        0x94, 0xab, 0xd7, 0xde, 0xb3, 0xc0, 0x20, 0xd6, 0x92, 0x21,
        0xdb, 0xed, 0xb1, 0x4b, 0x06, 0x12, 0xf5, 0x82, 0xe9, 0xc2,
        0xe6, 0xe9, 0x17, 0x94, 0x14, 0x9c, 0x04, 0x60, 0x32, 0xc2,
        0x36, 0xd5, 0x0f, 0xaa, 0x2b, 0x7d, 0xb7, 0xfc, 0x42, 0x52,
        0x1a, 0x96, 0x70, 0x0d, 0xb3, 0xa9, 0x99, 0x76, 0xe7, 0x8e,
        0xd9, 0xcb, 0x4f, 0x28, 0x16, 0x24, 0x57, 0xc6, 0x17, 0x15,
        0xfc, 0xab, 0xb6, 0x51, 0x74, 0x6d, 0x46, 0x75, 0xdf, 0x8c,
        0x00, 0x1a, 0x40, 0x0c, 0x9c, 0xc0, 0x3b, 0x3e, 0x58, 0x15,
        0x9d, 0xa4, 0x0b, 0x88, 0xcd, 0xac, 0xf7, 0xb1, 0xf1, 0x50,
        0x42, 0xe4, 0x5b, 0xe9, 0xc9, 0x3e, 0x0b, 0x9c, 0x8e, 0x90,
        0x4b, 0x3f, 0x7f, 0x9d, 0x31, 0x38, 0x50, 0x0e, 0x1d, 0x68,
        0xe7, 0x37, 0xa5, 0x48, 0x6a, 0x6f, 0x35, 0x00, 0xd5, 0x50,
        0x61, 0x8d, 0xfb, 0x1b
    };
    ossInitRootContext(world, (unsigned char *)_oss_typeinfo);
#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) > 12)
    ossSetExtParms(world, (void *)&extParms);
#endif /* OSS_SPARTAN_AWARE  > 12 */
#endif /* OSS_SPARTAN_AWARE */
}
#endif /* OSS_SPARTAN_AWARE >= 3 */
#endif /* OSS_SPARTAN_AWARE */

static const struct eheader _head = {_ossinit_s1ap, 0, 15, 19257, 250, 1675,
    (unsigned short *)_pduarray, (struct etype *)_etypearray,
    (struct efield *)_efieldarray, (void **)_enamearray, NULL,
    (struct ConstraintEntry *)_econstraintarray, NULL, NULL, 0, (void *)_objectsettable, 199};

#ifdef _OSSGETHEADER
void *DLL_ENTRY_FDEF ossGetHeader()
{
    return (void *)&_head;
}
#endif /* _OSSGETHEADER */

void * const s1ap = (void *)&_head;
