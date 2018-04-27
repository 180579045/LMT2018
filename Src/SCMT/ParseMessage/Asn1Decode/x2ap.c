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

#define    OSS_SOED_PER
#include   "osstype.h"
#include   "x2ap.h"

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_handoverPreparation = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    2,
    6,
    9,
    0,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_snStatusTransfer = {
    X2AP_criticality_present,
    11,
    0,
    0,
    4,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_uEContextRelease = {
    X2AP_criticality_present,
    14,
    0,
    0,
    5,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_handoverCancel = {
    X2AP_criticality_present,
    15,
    0,
    0,
    1,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_handoverReport = {
    X2AP_criticality_present,
    10,
    0,
    0,
    14,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_errorIndication = {
    X2AP_criticality_present,
    16,
    0,
    0,
    3,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_reset = {
    X2AP_SuccessfulOutcome_present | X2AP_criticality_present,
    17,
    18,
    0,
    7,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2Setup = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    19,
    20,
    21,
    6,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_loadIndication = {
    X2AP_criticality_present,
    22,
    0,
    0,
    2,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_eNBConfigurationUpdate = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    25,
    28,
    29,
    8,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_resourceStatusReportingInitiation = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    30,
    35,
    39,
    9,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_resourceStatusReporting = {
    X2AP_criticality_present,
    42,
    0,
    0,
    10,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_rLFIndication = {
    X2AP_criticality_present,
    49,
    0,
    0,
    13,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_privateMessage = {
    X2AP_criticality_present,
    45,
    0,
    0,
    11,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_mobilitySettingsChange = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    46,
    47,
    48,
    12,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_cellActivation = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    50,
    52,
    54,
    15,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2Release = {
    X2AP_criticality_present,
    55,
    0,
    0,
    16,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2APMessageTransfer = {
    X2AP_criticality_present,
    56,
    0,
    0,
    17,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBAdditionPreparation = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    59,
    62,
    65,
    19,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBReconfigurationCompletion = {
    X2AP_criticality_present,
    66,
    0,
    0,
    20,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_meNBinitiatedSeNBModificationPreparation = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    68,
    73,
    80,
    21,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBinitiatedSeNBModification = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    81,
    84,
    85,
    22,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_meNBinitiatedSeNBRelease = {
    X2AP_criticality_present,
    86,
    0,
    0,
    23,
    X2AP_ignore
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBinitiatedSeNBRelease = {
    X2AP_SuccessfulOutcome_present | X2AP_criticality_present,
    89,
    90,
    0,
    24,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_seNBCounterCheck = {
    X2AP_criticality_present,
    93,
    0,
    0,
    25,
    X2AP_reject
};

X2AP_X2AP_ELEMENTARY_PROCEDURE X2AP_x2Removal = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    96,
    97,
    98,
    18,
    X2AP_reject
};

const int X2AP_maxPrivateIEs = USHRT_MAX;

const int X2AP_maxProtocolExtensions = USHRT_MAX;

const int X2AP_maxProtocolIEs = USHRT_MAX;

const X2AP_ProcedureCode X2AP_id_handoverPreparation = 0;

const X2AP_ProcedureCode X2AP_id_handoverCancel = 1;

const X2AP_ProcedureCode X2AP_id_loadIndication = 2;

const X2AP_ProcedureCode X2AP_id_errorIndication = 3;

const X2AP_ProcedureCode X2AP_id_snStatusTransfer = 4;

const X2AP_ProcedureCode X2AP_id_uEContextRelease = 5;

const X2AP_ProcedureCode X2AP_id_x2Setup = 6;

const X2AP_ProcedureCode X2AP_id_reset = 7;

const X2AP_ProcedureCode X2AP_id_eNBConfigurationUpdate = 8;

const X2AP_ProcedureCode X2AP_id_resourceStatusReportingInitiation = 9;

const X2AP_ProcedureCode X2AP_id_resourceStatusReporting = 10;

const X2AP_ProcedureCode X2AP_id_privateMessage = 11;

const X2AP_ProcedureCode X2AP_id_mobilitySettingsChange = 12;

const X2AP_ProcedureCode X2AP_id_rLFIndication = 13;

const X2AP_ProcedureCode X2AP_id_handoverReport = 14;

const X2AP_ProcedureCode X2AP_id_cellActivation = 15;

const X2AP_ProcedureCode X2AP_id_x2Release = 16;

const X2AP_ProcedureCode X2AP_id_x2APMessageTransfer = 17;

const X2AP_ProcedureCode X2AP_id_x2Removal = 18;

const X2AP_ProcedureCode X2AP_id_seNBAdditionPreparation = 19;

const X2AP_ProcedureCode X2AP_id_seNBReconfigurationCompletion = 20;

const X2AP_ProcedureCode X2AP_id_meNBinitiatedSeNBModificationPreparation = 21;

const X2AP_ProcedureCode X2AP_id_seNBinitiatedSeNBModification = 22;

const X2AP_ProcedureCode X2AP_id_meNBinitiatedSeNBRelease = 23;

const X2AP_ProcedureCode X2AP_id_seNBinitiatedSeNBRelease = 24;

const X2AP_ProcedureCode X2AP_id_seNBCounterCheck = 25;

const int X2AP_maxEARFCN = USHRT_MAX;

const int X2AP_maxEARFCNPlusOne = 65536;

const int X2AP_newmaxEARFCN = 262143;

const int X2AP_maxInterfaces = 16;

const int X2AP_maxCellineNB = 256;

const int X2AP_maxnoofBands = 16;

const int X2AP_maxnoofBearers = 256;

const int X2AP_maxNrOfErrors = 256;

const int X2AP_maxnoofPDCP_SN = 16;

const int X2AP_maxnoofEPLMNs = 15;

const int X2AP_maxnoofEPLMNsPlusOne = 16;

const int X2AP_maxnoofForbLACs = 4096;

const int X2AP_maxnoofForbTACs = 4096;

const int X2AP_maxnoofBPLMNs = 6;

const int X2AP_maxnoofNeighbours = 512;

const int X2AP_maxnoofPRBs = 110;

const int X2AP_maxPools = 16;

const int X2AP_maxnoofCells = 16;

const int X2AP_maxnoofMBSFN = 8;

const int X2AP_maxFailedMeasObjects = 32;

const int X2AP_maxnoofCellIDforMDT = 32;

const int X2AP_maxnoofTAforMDT = 8;

const int X2AP_maxnoofMBMSServiceAreaIdentities = 256;

const int X2AP_maxnoofMDTPLMNs = 16;

const int X2AP_maxnoofCoMPHypothesisSet = 256;

const int X2AP_maxnoofCoMPCells = 32;

const int X2AP_maxUEReport = 128;

const int X2AP_maxCellReport = 9;

const int X2AP_maxnoofPA = 3;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_Item = 0;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_List = 1;

const X2AP_ProtocolIE_ID X2AP_id_E_RAB_Item = 2;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_NotAdmitted_List = 3;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeSetup_Item = 4;

const X2AP_ProtocolIE_ID X2AP_id_Cause = 5;

const X2AP_ProtocolIE_ID X2AP_id_CellInformation = 6;

const X2AP_ProtocolIE_ID X2AP_id_CellInformation_Item = 7;

const X2AP_ProtocolIE_ID X2AP_id_New_eNB_UE_X2AP_ID = 9;

const X2AP_ProtocolIE_ID X2AP_id_Old_eNB_UE_X2AP_ID = 10;

const X2AP_ProtocolIE_ID X2AP_id_TargetCell_ID = 11;

const X2AP_ProtocolIE_ID X2AP_id_TargeteNBtoSource_eNBTransparentContainer = 12;

const X2AP_ProtocolIE_ID X2AP_id_TraceActivation = 13;

const X2AP_ProtocolIE_ID X2AP_id_UE_ContextInformation = 14;

const X2AP_ProtocolIE_ID X2AP_id_UE_HistoryInformation = 15;

const X2AP_ProtocolIE_ID X2AP_id_UE_X2AP_ID = 16;

const X2AP_ProtocolIE_ID X2AP_id_CriticalityDiagnostics = 17;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToStatusTransfer_List = 18;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToStatusTransfer_Item = 19;

const X2AP_ProtocolIE_ID X2AP_id_ServedCells = 20;

const X2AP_ProtocolIE_ID X2AP_id_GlobalENB_ID = 21;

const X2AP_ProtocolIE_ID X2AP_id_TimeToWait = 22;

const X2AP_ProtocolIE_ID X2AP_id_GUMMEI_ID = 23;

const X2AP_ProtocolIE_ID X2AP_id_GUGroupIDList = 24;

const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToAdd = 25;

const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToModify = 26;

const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToDelete = 27;

const X2AP_ProtocolIE_ID X2AP_id_Registration_Request = 28;

const X2AP_ProtocolIE_ID X2AP_id_CellToReport = 29;

const X2AP_ProtocolIE_ID X2AP_id_ReportingPeriodicity = 30;

const X2AP_ProtocolIE_ID X2AP_id_CellToReport_Item = 31;

const X2AP_ProtocolIE_ID X2AP_id_CellMeasurementResult = 32;

const X2AP_ProtocolIE_ID X2AP_id_CellMeasurementResult_Item = 33;

const X2AP_ProtocolIE_ID X2AP_id_GUGroupIDToAddList = 34;

const X2AP_ProtocolIE_ID X2AP_id_GUGroupIDToDeleteList = 35;

const X2AP_ProtocolIE_ID X2AP_id_SRVCCOperationPossible = 36;

const X2AP_ProtocolIE_ID X2AP_id_Measurement_ID = 37;

const X2AP_ProtocolIE_ID X2AP_id_ReportCharacteristics = 38;

const X2AP_ProtocolIE_ID X2AP_id_ENB1_Measurement_ID = 39;

const X2AP_ProtocolIE_ID X2AP_id_ENB2_Measurement_ID = 40;

const X2AP_ProtocolIE_ID X2AP_id_Number_of_Antennaports = 41;

const X2AP_ProtocolIE_ID X2AP_id_CompositeAvailableCapacityGroup = 42;

const X2AP_ProtocolIE_ID X2AP_id_ENB1_Cell_ID = 43;

const X2AP_ProtocolIE_ID X2AP_id_ENB2_Cell_ID = 44;

const X2AP_ProtocolIE_ID X2AP_id_ENB2_Proposed_Mobility_Parameters = 45;

const X2AP_ProtocolIE_ID X2AP_id_ENB1_Mobility_Parameters = 46;

const X2AP_ProtocolIE_ID X2AP_id_ENB2_Mobility_Parameters_Modification_Range = 47;

const X2AP_ProtocolIE_ID X2AP_id_FailureCellPCI = 48;

const X2AP_ProtocolIE_ID X2AP_id_Re_establishmentCellECGI = 49;

const X2AP_ProtocolIE_ID X2AP_id_FailureCellCRNTI = 50;

const X2AP_ProtocolIE_ID X2AP_id_ShortMAC_I = 51;

const X2AP_ProtocolIE_ID X2AP_id_SourceCellECGI = 52;

const X2AP_ProtocolIE_ID X2AP_id_FailureCellECGI = 53;

const X2AP_ProtocolIE_ID X2AP_id_HandoverReportType = 54;

const X2AP_ProtocolIE_ID X2AP_id_PRACH_Configuration = 55;

const X2AP_ProtocolIE_ID X2AP_id_MBSFN_Subframe_Info = 56;

const X2AP_ProtocolIE_ID X2AP_id_ServedCellsToActivate = 57;

const X2AP_ProtocolIE_ID X2AP_id_ActivatedCellList = 58;

const X2AP_ProtocolIE_ID X2AP_id_DeactivationIndication = 59;

const X2AP_ProtocolIE_ID X2AP_id_UE_RLF_Report_Container = 60;

const X2AP_ProtocolIE_ID X2AP_id_ABSInformation = 61;

const X2AP_ProtocolIE_ID X2AP_id_InvokeIndication = 62;

const X2AP_ProtocolIE_ID X2AP_id_ABS_Status = 63;

const X2AP_ProtocolIE_ID X2AP_id_PartialSuccessIndicator = 64;

const X2AP_ProtocolIE_ID X2AP_id_MeasurementInitiationResult_List = 65;

const X2AP_ProtocolIE_ID X2AP_id_MeasurementInitiationResult_Item = 66;

const X2AP_ProtocolIE_ID X2AP_id_MeasurementFailureCause_Item = 67;

const X2AP_ProtocolIE_ID X2AP_id_CompleteFailureCauseInformation_List = 68;

const X2AP_ProtocolIE_ID X2AP_id_CompleteFailureCauseInformation_Item = 69;

const X2AP_ProtocolIE_ID X2AP_id_CSG_Id = 70;

const X2AP_ProtocolIE_ID X2AP_id_CSGMembershipStatus = 71;

const X2AP_ProtocolIE_ID X2AP_id_MDTConfiguration = 72;

const X2AP_ProtocolIE_ID X2AP_id_ManagementBasedMDTallowed = 74;

const X2AP_ProtocolIE_ID X2AP_id_RRCConnSetupIndicator = 75;

const X2AP_ProtocolIE_ID X2AP_id_NeighbourTAC = 76;

const X2AP_ProtocolIE_ID X2AP_id_Time_UE_StayedInCell_EnhancedGranularity = 77;

const X2AP_ProtocolIE_ID X2AP_id_RRCConnReestabIndicator = 78;

const X2AP_ProtocolIE_ID X2AP_id_MBMS_Service_Area_List = 79;

const X2AP_ProtocolIE_ID X2AP_id_HO_cause = 80;

const X2AP_ProtocolIE_ID X2AP_id_TargetCellInUTRAN = 81;

const X2AP_ProtocolIE_ID X2AP_id_MobilityInformation = 82;

const X2AP_ProtocolIE_ID X2AP_id_SourceCellCRNTI = 83;

const X2AP_ProtocolIE_ID X2AP_id_MultibandInfoList = 84;

const X2AP_ProtocolIE_ID X2AP_id_M3Configuration = 85;

const X2AP_ProtocolIE_ID X2AP_id_M4Configuration = 86;

const X2AP_ProtocolIE_ID X2AP_id_M5Configuration = 87;

const X2AP_ProtocolIE_ID X2AP_id_MDT_Location_Info = 88;

const X2AP_ProtocolIE_ID X2AP_id_ManagementBasedMDTPLMNList = 89;

const X2AP_ProtocolIE_ID X2AP_id_SignallingBasedMDTPLMNList = 90;

const X2AP_ProtocolIE_ID X2AP_id_ReceiveStatusOfULPDCPSDUsExtended = 91;

const X2AP_ProtocolIE_ID X2AP_id_ULCOUNTValueExtended = 92;

const X2AP_ProtocolIE_ID X2AP_id_DLCOUNTValueExtended = 93;

const X2AP_ProtocolIE_ID X2AP_id_eARFCNExtension = 94;

const X2AP_ProtocolIE_ID X2AP_id_UL_EARFCNExtension = 95;

const X2AP_ProtocolIE_ID X2AP_id_DL_EARFCNExtension = 96;

const X2AP_ProtocolIE_ID X2AP_id_AdditionalSpecialSubframe_Info = 97;

const X2AP_ProtocolIE_ID X2AP_id_Masked_IMEISV = 98;

const X2AP_ProtocolIE_ID X2AP_id_IntendedULDLConfiguration = 99;

const X2AP_ProtocolIE_ID X2AP_id_ExtendedULInterferenceOverloadInfo = 100;

const X2AP_ProtocolIE_ID X2AP_id_RNL_Header = 101;

const X2AP_ProtocolIE_ID X2AP_id_x2APMessage = 102;

const X2AP_ProtocolIE_ID X2AP_id_ProSeAuthorized = 103;

const X2AP_ProtocolIE_ID X2AP_id_ExpectedUEBehaviour = 104;

const X2AP_ProtocolIE_ID X2AP_id_UE_HistoryInformationFromTheUE = 105;

const X2AP_ProtocolIE_ID X2AP_id_DynamicDLTransmissionInformation = 106;

const X2AP_ProtocolIE_ID X2AP_id_UE_RLF_Report_Container_for_extended_bands = 107;

const X2AP_ProtocolIE_ID X2AP_id_CoMPInformation = 108;

const X2AP_ProtocolIE_ID X2AP_id_ReportingPeriodicityRSRPMR = 109;

const X2AP_ProtocolIE_ID X2AP_id_RSRPMRList = 110;

const X2AP_ProtocolIE_ID X2AP_id_MeNB_UE_X2AP_ID = 111;

const X2AP_ProtocolIE_ID X2AP_id_SeNB_UE_X2AP_ID = 112;

const X2AP_ProtocolIE_ID X2AP_id_UE_SecurityCapabilities = 113;

const X2AP_ProtocolIE_ID X2AP_id_SeNBSecurityKey = 114;

const X2AP_ProtocolIE_ID X2AP_id_SeNBUEAggregateMaximumBitRate = 115;

const X2AP_ProtocolIE_ID X2AP_id_ServingPLMN = 116;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeAdded_List = 117;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeAdded_Item = 118;

const X2AP_ProtocolIE_ID X2AP_id_MeNBtoSeNBContainer = 119;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_List = 120;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_Item = 121;

const X2AP_ProtocolIE_ID X2AP_id_SeNBtoMeNBContainer = 122;

const X2AP_ProtocolIE_ID X2AP_id_ResponseInformationSeNBReconfComp = 123;

const X2AP_ProtocolIE_ID X2AP_id_UE_ContextInformationSeNBModReq = 124;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeAdded_ModReqItem = 125;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeModified_ModReqItem = 126;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_ModReqItem = 127;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_ModAckList = 128;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeModified_ModAckList = 129;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeReleased_ModAckList = 130;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeAdded_ModAckItem = 131;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeModified_ModAckItem = 132;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_Admitted_ToBeReleased_ModAckItem = 133;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_ModReqd = 134;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_ModReqdItem = 135;

const X2AP_ProtocolIE_ID X2AP_id_SCGChangeIndication = 136;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_List_RelReq = 137;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_RelReqItem = 138;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_List_RelConf = 139;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_ToBeReleased_RelConfItem = 140;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToCounterCheck_List = 141;

const X2AP_ProtocolIE_ID X2AP_id_E_RABs_SubjectToCounterCheckItem = 142;

const X2AP_ProtocolIE_ID X2AP_id_FreqBandIndicatorPriority = 160;

static X2AP_Criticality _v0 = X2AP_ignore;

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v1 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    2,
    6,
    9,
    0,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v2 = {
    X2AP_SuccessfulOutcome_present | X2AP_criticality_present,
    17,
    18,
    0,
    7,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v3 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    19,
    20,
    21,
    6,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v4 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    30,
    35,
    39,
    9,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v5 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    25,
    28,
    29,
    8,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v6 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    46,
    47,
    48,
    12,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v7 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    50,
    52,
    54,
    15,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v8 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    59,
    62,
    65,
    19,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v9 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    68,
    73,
    80,
    21,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v10 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    81,
    84,
    85,
    22,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v11 = {
    X2AP_SuccessfulOutcome_present | X2AP_criticality_present,
    89,
    90,
    0,
    24,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v12 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    96,
    97,
    98,
    18,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v13 = {
    X2AP_criticality_present,
    11,
    0,
    0,
    4,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v14 = {
    X2AP_criticality_present,
    14,
    0,
    0,
    5,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v15 = {
    X2AP_criticality_present,
    15,
    0,
    0,
    1,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v16 = {
    X2AP_criticality_present,
    16,
    0,
    0,
    3,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v17 = {
    X2AP_criticality_present,
    42,
    0,
    0,
    10,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v18 = {
    X2AP_criticality_present,
    22,
    0,
    0,
    2,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v19 = {
    X2AP_criticality_present,
    45,
    0,
    0,
    11,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v20 = {
    X2AP_criticality_present,
    49,
    0,
    0,
    13,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v21 = {
    X2AP_criticality_present,
    10,
    0,
    0,
    14,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v22 = {
    X2AP_criticality_present,
    55,
    0,
    0,
    16,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v23 = {
    X2AP_criticality_present,
    56,
    0,
    0,
    17,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v24 = {
    X2AP_criticality_present,
    66,
    0,
    0,
    20,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v25 = {
    X2AP_criticality_present,
    86,
    0,
    0,
    23,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v26 = {
    X2AP_criticality_present,
    93,
    0,
    0,
    25,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v27 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    2,
    6,
    9,
    0,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v28 = {
    X2AP_SuccessfulOutcome_present | X2AP_criticality_present,
    17,
    18,
    0,
    7,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v29 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    19,
    20,
    21,
    6,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v30 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    30,
    35,
    39,
    9,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v31 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    25,
    28,
    29,
    8,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v32 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    46,
    47,
    48,
    12,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v33 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    50,
    52,
    54,
    15,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v34 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    59,
    62,
    65,
    19,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v35 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    68,
    73,
    80,
    21,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v36 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    81,
    84,
    85,
    22,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v37 = {
    X2AP_SuccessfulOutcome_present | X2AP_criticality_present,
    89,
    90,
    0,
    24,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v38 = {
    X2AP_SuccessfulOutcome_present | X2AP_UnsuccessfulOutcome_present
         | X2AP_criticality_present,
    96,
    97,
    98,
    18,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v39 = {
    X2AP_criticality_present,
    11,
    0,
    0,
    4,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v40 = {
    X2AP_criticality_present,
    14,
    0,
    0,
    5,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v41 = {
    X2AP_criticality_present,
    15,
    0,
    0,
    1,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v42 = {
    X2AP_criticality_present,
    16,
    0,
    0,
    3,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v43 = {
    X2AP_criticality_present,
    42,
    0,
    0,
    10,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v44 = {
    X2AP_criticality_present,
    22,
    0,
    0,
    2,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v45 = {
    X2AP_criticality_present,
    45,
    0,
    0,
    11,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v46 = {
    X2AP_criticality_present,
    49,
    0,
    0,
    13,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v47 = {
    X2AP_criticality_present,
    10,
    0,
    0,
    14,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v48 = {
    X2AP_criticality_present,
    55,
    0,
    0,
    16,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v49 = {
    X2AP_criticality_present,
    56,
    0,
    0,
    17,
    X2AP_reject
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v50 = {
    X2AP_criticality_present,
    66,
    0,
    0,
    20,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v51 = {
    X2AP_criticality_present,
    86,
    0,
    0,
    23,
    X2AP_ignore
};

static X2AP_X2AP_ELEMENTARY_PROCEDURE _v52 = {
    X2AP_criticality_present,
    93,
    0,
    0,
    25,
    X2AP_reject
};

static X2AP_X2AP_PROTOCOL_IES _v53 = {
    10,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v54 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v55 = {
    11,
    X2AP_reject,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v56 = {
    23,
    X2AP_reject,
    121,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v57 = {
    14,
    X2AP_reject,
    3,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v58 = {
    15,
    X2AP_ignore,
    164,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v59 = {
    13,
    X2AP_ignore,
    163,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v60 = {
    36,
    X2AP_ignore,
    156,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v61 = {
    71,
    X2AP_reject,
    108,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v62 = {
    82,
    X2AP_ignore,
    5,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v63 = {
    98,
    X2AP_ignore,
    123,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v64 = {
    105,
    X2AP_ignore,
    165,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v65 = {
    104,
    X2AP_ignore,
    116,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v66 = {
    103,
    X2AP_ignore,
    143,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v67 = {
    74,
    X2AP_ignore,
    135,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v68 = {
    89,
    X2AP_ignore,
    129,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v69 = {
    4,
    X2AP_ignore,
    4,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v70 = {
    10,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v71 = {
    9,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v72 = {
    1,
    X2AP_ignore,
    7,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v73 = {
    3,
    X2AP_ignore,
    114,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v74 = {
    12,
    X2AP_ignore,
    160,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v75 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v76 = {
    0,
    X2AP_ignore,
    8,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v77 = {
    10,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v78 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v79 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v80 = {
    54,
    X2AP_ignore,
    122,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v81 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v82 = {
    52,
    X2AP_ignore,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v83 = {
    53,
    X2AP_ignore,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v84 = {
    49,
    X2AP_ignore,
    113,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_IES _v85 = {
    81,
    X2AP_ignore,
    159,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_IES _v86 = {
    83,
    X2AP_ignore,
    107,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v87 = {
    82,
    X2AP_ignore,
    5,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v88 = {
    60,
    X2AP_ignore,
    169,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v89 = {
    107,
    X2AP_ignore,
    170,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v90 = {
    10,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v91 = {
    9,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v92 = {
    18,
    X2AP_ignore,
    12,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v93 = {
    19,
    X2AP_ignore,
    13,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v94 = {
    91,
    X2AP_ignore,
    144,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v95 = {
    92,
    X2AP_ignore,
    105,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v96 = {
    93,
    X2AP_ignore,
    105,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v97 = {
    10,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v98 = {
    9,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v99 = {
    10,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v100 = {
    9,
    X2AP_ignore,
    166,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v101 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v102 = {
    10,
    X2AP_ignore,
    166,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v103 = {
    9,
    X2AP_ignore,
    166,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v104 = {
    5,
    X2AP_ignore,
    102,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v105 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v106 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v107 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v108 = {
    21,
    X2AP_reject,
    119,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v109 = {
    20,
    X2AP_reject,
    154,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v110 = {
    24,
    X2AP_reject,
    120,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v111 = {
    21,
    X2AP_reject,
    119,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v112 = {
    20,
    X2AP_reject,
    154,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v113 = {
    24,
    X2AP_reject,
    120,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v114 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v115 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v116 = {
    22,
    X2AP_ignore,
    161,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v117 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v118 = {
    6,
    X2AP_ignore,
    23,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v119 = {
    7,
    X2AP_ignore,
    24,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v120 = {
    61,
    X2AP_ignore,
    99,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v121 = {
    62,
    X2AP_ignore,
    124,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v122 = {
    99,
    X2AP_ignore,
    157,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v123 = {
    100,
    X2AP_ignore,
    117,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v124 = {
    108,
    X2AP_ignore,
    103,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v125 = {
    106,
    X2AP_ignore,
    111,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v126 = {
    25,
    X2AP_reject,
    154,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v127 = {
    26,
    X2AP_reject,
    26,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v128 = {
    27,
    X2AP_reject,
    27,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v129 = {
    34,
    X2AP_reject,
    120,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v130 = {
    35,
    X2AP_reject,
    120,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v131 = {
    59,
    X2AP_ignore,
    110,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v132 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v133 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v134 = {
    22,
    X2AP_ignore,
    161,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v135 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v136 = {
    39,
    X2AP_reject,
    132,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v137 = {
    40,
    X2AP_ignore,
    132,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_IES _v138 = {
    28,
    X2AP_reject,
    145,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v139 = {
    38,
    X2AP_reject,
    147,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v140 = {
    29,
    X2AP_ignore,
    31,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v141 = {
    30,
    X2AP_ignore,
    33,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v142 = {
    64,
    X2AP_ignore,
    34,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v143 = {
    109,
    X2AP_ignore,
    146,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v144 = {
    31,
    X2AP_ignore,
    32,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v145 = {
    39,
    X2AP_reject,
    132,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v146 = {
    40,
    X2AP_reject,
    132,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v147 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v148 = {
    65,
    X2AP_ignore,
    36,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v149 = {
    66,
    X2AP_ignore,
    37,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v150 = {
    67,
    X2AP_ignore,
    38,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v151 = {
    39,
    X2AP_reject,
    132,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v152 = {
    40,
    X2AP_reject,
    132,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v153 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v154 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v155 = {
    68,
    X2AP_ignore,
    40,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v156 = {
    69,
    X2AP_ignore,
    41,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v157 = {
    39,
    X2AP_reject,
    132,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v158 = {
    40,
    X2AP_reject,
    132,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v159 = {
    32,
    X2AP_ignore,
    43,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v160 = {
    33,
    X2AP_ignore,
    44,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v161 = {
    42,
    X2AP_ignore,
    104,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v162 = {
    63,
    X2AP_ignore,
    100,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v163 = {
    110,
    X2AP_ignore,
    150,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v164 = {
    43,
    X2AP_reject,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v165 = {
    44,
    X2AP_reject,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v166 = {
    46,
    X2AP_ignore,
    137,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v167 = {
    45,
    X2AP_reject,
    137,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v168 = {
    5,
    X2AP_reject,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v169 = {
    43,
    X2AP_reject,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v170 = {
    44,
    X2AP_reject,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v171 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v172 = {
    43,
    X2AP_ignore,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v173 = {
    44,
    X2AP_ignore,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v174 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v175 = {
    47,
    X2AP_ignore,
    136,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v176 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v177 = {
    48,
    X2AP_ignore,
    140,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v178 = {
    49,
    X2AP_ignore,
    113,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v179 = {
    50,
    X2AP_ignore,
    107,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v180 = {
    51,
    X2AP_ignore,
    155,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v181 = {
    60,
    X2AP_ignore,
    169,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v182 = {
    75,
    X2AP_reject,
    149,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v183 = {
    78,
    X2AP_ignore,
    148,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v184 = {
    107,
    X2AP_ignore,
    170,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v185 = {
    57,
    X2AP_reject,
    51,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v186 = {
    58,
    X2AP_ignore,
    53,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v187 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v188 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v189 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v190 = {
    21,
    X2AP_reject,
    119,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v191 = {
    101,
    X2AP_reject,
    57,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v192 = {
    102,
    X2AP_reject,
    58,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v193 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v194 = {
    113,
    X2AP_reject,
    168,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_IES _v195 = {
    114,
    X2AP_reject,
    152,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_IES _v196 = {
    115,
    X2AP_reject,
    167,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v197 = {
    116,
    X2AP_ignore,
    141,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v198 = {
    117,
    X2AP_reject,
    60,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v199 = {
    119,
    X2AP_reject,
    131,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v200 = {
    118,
    X2AP_reject,
    61,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v201 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v202 = {
    112,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v203 = {
    120,
    X2AP_ignore,
    63,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v204 = {
    3,
    X2AP_ignore,
    114,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v205 = {
    122,
    X2AP_reject,
    153,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v206 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v207 = {
    121,
    X2AP_ignore,
    64,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v208 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v209 = {
    112,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v210 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v211 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v212 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v213 = {
    112,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v214 = {
    123,
    X2AP_ignore,
    67,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v215 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v216 = {
    112,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v217 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v218 = {
    136,
    X2AP_ignore,
    151,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v219 = {
    116,
    X2AP_ignore,
    141,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v220 = {
    124,
    X2AP_reject,
    69,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v221 = {
    119,
    X2AP_ignore,
    131,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v222 = {
    125,
    X2AP_ignore,
    70,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v223 = {
    126,
    X2AP_ignore,
    71,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v224 = {
    127,
    X2AP_ignore,
    72,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v225 = {
    111,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v226 = {
    112,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v227 = {
    128,
    X2AP_ignore,
    74,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v228 = {
    129,
    X2AP_ignore,
    76,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v229 = {
    130,
    X2AP_ignore,
    78,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v230 = {
    3,
    X2AP_ignore,
    114,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v231 = {
    122,
    X2AP_ignore,
    153,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v232 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v233 = {
    131,
    X2AP_ignore,
    75,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v234 = {
    132,
    X2AP_ignore,
    77,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v235 = {
    133,
    X2AP_ignore,
    79,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v236 = {
    111,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v237 = {
    112,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v238 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v239 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v240 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v241 = {
    112,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v242 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v243 = {
    136,
    X2AP_ignore,
    151,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v244 = {
    134,
    X2AP_ignore,
    82,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v245 = {
    122,
    X2AP_ignore,
    153,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v246 = {
    135,
    X2AP_ignore,
    83,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v247 = {
    111,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v248 = {
    112,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v249 = {
    119,
    X2AP_ignore,
    131,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v250 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v251 = {
    111,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v252 = {
    112,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v253 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v254 = {
    119,
    X2AP_ignore,
    131,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v255 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v256 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v257 = {
    112,
    X2AP_reject,
    166,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v258 = {
    5,
    X2AP_ignore,
    102,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v259 = {
    137,
    X2AP_ignore,
    87,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v260 = {
    138,
    X2AP_ignore,
    88,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v261 = {
    111,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v262 = {
    112,
    X2AP_reject,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v263 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v264 = {
    111,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v265 = {
    112,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v266 = {
    139,
    X2AP_ignore,
    91,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v267 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v268 = {
    140,
    X2AP_ignore,
    92,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v269 = {
    111,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v270 = {
    112,
    X2AP_ignore,
    166,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v271 = {
    141,
    X2AP_ignore,
    94,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v272 = {
    142,
    X2AP_ignore,
    95,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v273 = {
    21,
    X2AP_reject,
    119,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v274 = {
    21,
    X2AP_reject,
    119,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v275 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v276 = {
    5,
    X2AP_ignore,
    102,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_IES _v277 = {
    17,
    X2AP_ignore,
    106,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v278 = {
    95,
    X2AP_reject,
    112,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v279 = {
    96,
    X2AP_reject,
    112,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v280 = {
    97,
    X2AP_ignore,
    101,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v281 = {
    94,
    X2AP_reject,
    112,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_IES _v282 = {
    2,
    X2AP_ignore,
    115,
    X2AP_mandatory
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v283 = {
    77,
    X2AP_ignore,
    162,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v284 = {
    80,
    X2AP_ignore,
    102,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v285 = {
    85,
    X2AP_ignore,
    125,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v286 = {
    86,
    X2AP_ignore,
    126,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v287 = {
    87,
    X2AP_ignore,
    127,
    X2AP_conditional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v288 = {
    88,
    X2AP_ignore,
    130,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v289 = {
    90,
    X2AP_ignore,
    129,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v290 = {
    76,
    X2AP_ignore,
    158,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v291 = {
    94,
    X2AP_reject,
    112,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v292 = {
    41,
    X2AP_ignore,
    139,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v293 = {
    55,
    X2AP_ignore,
    142,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v294 = {
    56,
    X2AP_ignore,
    134,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v295 = {
    70,
    X2AP_ignore,
    109,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v296 = {
    79,
    X2AP_ignore,
    133,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v297 = {
    84,
    X2AP_ignore,
    138,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v298 = {
    160,
    X2AP_ignore,
    118,
    X2AP_optional
};

static X2AP_X2AP_PROTOCOL_EXTENSION _v299 = {
    72,
    X2AP_ignore,
    128,
    X2AP_optional
};

static int _v318 = 40;

static int _v319 = 50;

static int _v320 = 60;

static int _v321 = 80;

static int _v322 = 100;

static int _v323 = 120;

static int _v324 = 150;

static int _v325 = 180;

static int _v326 = 181;

static int _v327 = 40;

static int _v328 = 50;

static int _v329 = 60;

static int _v330 = 80;

static int _v331 = 100;

static int _v332 = 120;

static int _v333 = 150;

static int _v334 = 180;

static int _v335 = 181;


#if !defined(OSS_SPARTAN_AWARE) || ((OSS_SPARTAN_AWARE + 0) < 2)
static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES[] = {
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[1], &_v1},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[2], &_v2},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[3], &_v3},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[4], &_v4},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[5], &_v5},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[6], &_v6},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[7], &_v7},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[8], &_v8},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[9], &_v9},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[10], &_v10},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[11], &_v11},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[12], &_v12},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[13], &_v13},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[14], &_v14},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[15], &_v15},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[16], &_v16},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[17], &_v17},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[18], &_v18},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[19], &_v19},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[20], &_v20},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[21], &_v21},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[22], &_v22},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[23], &_v23},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[24], &_v24},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES[25], &_v25},
    {NULL, &_v26}
};

static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[] = {
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[1], &_v27},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[2], &_v28},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[3], &_v29},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[4], &_v30},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[5], &_v31},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[6], &_v32},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[7], &_v33},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[8], &_v34},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[9], &_v35},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[10], &_v36},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[11], &_v37},
    {NULL, &_v38}
};

static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[] = {
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[1], &_v39},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[2], &_v40},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[3], &_v41},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[4], &_v42},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[5], &_v43},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[6], &_v44},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[7], &_v45},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[8], &_v46},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[9], &_v47},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[10], &_v48},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[11], &_v49},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[12], &_v50},
    {&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[13], &_v51},
    {NULL, &_v52}
};

static ObjectSetEntry const X2AP_HandoverRequest_IEs[] = {
    {&X2AP_HandoverRequest_IEs[1], &_v53},
    {&X2AP_HandoverRequest_IEs[2], &_v54},
    {&X2AP_HandoverRequest_IEs[3], &_v55},
    {&X2AP_HandoverRequest_IEs[4], &_v56},
    {&X2AP_HandoverRequest_IEs[5], &_v57},
    {&X2AP_HandoverRequest_IEs[6], &_v58},
    {&X2AP_HandoverRequest_IEs[7], &_v59},
    {&X2AP_HandoverRequest_IEs[8], &_v60},
    {&X2AP_HandoverRequest_IEs[9], &_v61},
    {&X2AP_HandoverRequest_IEs[10], &_v62},
    {&X2AP_HandoverRequest_IEs[11], &_v63},
    {&X2AP_HandoverRequest_IEs[12], &_v64},
    {&X2AP_HandoverRequest_IEs[13], &_v65},
    {NULL, &_v66}
};

static ObjectSetEntry const X2AP_UE_ContextInformation_ExtIEs[] = {
    {&X2AP_UE_ContextInformation_ExtIEs[1], &_v67},
    {NULL, &_v68}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeSetup_ItemIEs[] = {
    {NULL, &_v69}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeSetup_ItemExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_HandoverRequestAcknowledge_IEs[] = {
    {&X2AP_HandoverRequestAcknowledge_IEs[1], &_v70},
    {&X2AP_HandoverRequestAcknowledge_IEs[2], &_v71},
    {&X2AP_HandoverRequestAcknowledge_IEs[3], &_v72},
    {&X2AP_HandoverRequestAcknowledge_IEs[4], &_v73},
    {&X2AP_HandoverRequestAcknowledge_IEs[5], &_v74},
    {NULL, &_v75}
};

static ObjectSetEntry const X2AP_E_RABs_Admitted_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_HandoverPreparationFailure_IEs[] = {
    {&X2AP_HandoverPreparationFailure_IEs[1], &_v77},
    {&X2AP_HandoverPreparationFailure_IEs[2], &_v78},
    {NULL, &_v79}
};

static ObjectSetEntry const X2AP_HandoverReport_IEs[] = {
    {&X2AP_HandoverReport_IEs[1], &_v80},
    {&X2AP_HandoverReport_IEs[2], &_v81},
    {&X2AP_HandoverReport_IEs[3], &_v82},
    {&X2AP_HandoverReport_IEs[4], &_v83},
    {&X2AP_HandoverReport_IEs[5], &_v84},
    {&X2AP_HandoverReport_IEs[6], &_v85},
    {&X2AP_HandoverReport_IEs[7], &_v86},
    {&X2AP_HandoverReport_IEs[8], &_v87},
    {&X2AP_HandoverReport_IEs[9], &_v88},
    {NULL, &_v89}
};

static ObjectSetEntry const X2AP_SNStatusTransfer_IEs[] = {
    {&X2AP_SNStatusTransfer_IEs[1], &_v90},
    {&X2AP_SNStatusTransfer_IEs[2], &_v91},
    {NULL, &_v92}
};

static ObjectSetEntry const X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[] = {
    {&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[1], &_v94},
    {&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[2], &_v95},
    {NULL, &_v96}
};

static ObjectSetEntry const X2AP_UEContextRelease_IEs[] = {
    {&X2AP_UEContextRelease_IEs[1], &_v97},
    {NULL, &_v98}
};

static ObjectSetEntry const X2AP_HandoverCancel_IEs[] = {
    {&X2AP_HandoverCancel_IEs[1], &_v99},
    {&X2AP_HandoverCancel_IEs[2], &_v100},
    {NULL, &_v101}
};

static ObjectSetEntry const X2AP_ErrorIndication_IEs[] = {
    {&X2AP_ErrorIndication_IEs[1], &_v102},
    {&X2AP_ErrorIndication_IEs[2], &_v103},
    {&X2AP_ErrorIndication_IEs[3], &_v104},
    {NULL, &_v105}
};

static ObjectSetEntry const X2AP_ResetRequest_IEs[] = {
    {NULL, &_v106}
};

static ObjectSetEntry const X2AP_ResetResponse_IEs[] = {
    {NULL, &_v107}
};

static ObjectSetEntry const X2AP_X2SetupRequest_IEs[] = {
    {&X2AP_X2SetupRequest_IEs[1], &_v108},
    {&X2AP_X2SetupRequest_IEs[2], &_v109},
    {NULL, &_v110}
};

static ObjectSetEntry const X2AP_X2SetupResponse_IEs[] = {
    {&X2AP_X2SetupResponse_IEs[1], &_v111},
    {&X2AP_X2SetupResponse_IEs[2], &_v112},
    {&X2AP_X2SetupResponse_IEs[3], &_v113},
    {NULL, &_v114}
};

static ObjectSetEntry const X2AP_X2SetupFailure_IEs[] = {
    {&X2AP_X2SetupFailure_IEs[1], &_v115},
    {&X2AP_X2SetupFailure_IEs[2], &_v116},
    {NULL, &_v117}
};

static ObjectSetEntry const X2AP_LoadInformation_IEs[] = {
    {NULL, &_v118}
};

static ObjectSetEntry const X2AP_CellInformation_Item_ExtIEs[] = {
    {&X2AP_CellInformation_Item_ExtIEs[1], &_v120},
    {&X2AP_CellInformation_Item_ExtIEs[2], &_v121},
    {&X2AP_CellInformation_Item_ExtIEs[3], &_v122},
    {&X2AP_CellInformation_Item_ExtIEs[4], &_v123},
    {&X2AP_CellInformation_Item_ExtIEs[5], &_v124},
    {NULL, &_v125}
};

static ObjectSetEntry const X2AP_ENBConfigurationUpdate_IEs[] = {
    {&X2AP_ENBConfigurationUpdate_IEs[1], &_v126},
    {&X2AP_ENBConfigurationUpdate_IEs[2], &_v127},
    {&X2AP_ENBConfigurationUpdate_IEs[3], &_v128},
    {&X2AP_ENBConfigurationUpdate_IEs[4], &_v129},
    {NULL, &_v130}
};

static ObjectSetEntry const X2AP_ServedCellsToModify_Item_ExtIEs[] = {
    {NULL, &_v131}
};

static ObjectSetEntry const X2AP_ENBConfigurationUpdateAcknowledge_IEs[] = {
    {NULL, &_v132}
};

static ObjectSetEntry const X2AP_ENBConfigurationUpdateFailure_IEs[] = {
    {&X2AP_ENBConfigurationUpdateFailure_IEs[1], &_v133},
    {&X2AP_ENBConfigurationUpdateFailure_IEs[2], &_v134},
    {NULL, &_v135}
};

static ObjectSetEntry const X2AP_ResourceStatusRequest_IEs[] = {
    {&X2AP_ResourceStatusRequest_IEs[1], &_v136},
    {&X2AP_ResourceStatusRequest_IEs[2], &_v137},
    {&X2AP_ResourceStatusRequest_IEs[3], &_v138},
    {&X2AP_ResourceStatusRequest_IEs[4], &_v139},
    {&X2AP_ResourceStatusRequest_IEs[5], &_v140},
    {&X2AP_ResourceStatusRequest_IEs[6], &_v141},
    {&X2AP_ResourceStatusRequest_IEs[7], &_v142},
    {NULL, &_v143}
};

static ObjectSetEntry const X2AP_CellToReport_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ResourceStatusResponse_IEs[] = {
    {&X2AP_ResourceStatusResponse_IEs[1], &_v145},
    {&X2AP_ResourceStatusResponse_IEs[2], &_v146},
    {&X2AP_ResourceStatusResponse_IEs[3], &_v147},
    {NULL, &_v148}
};

static ObjectSetEntry const X2AP_MeasurementInitiationResult_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_MeasurementFailureCause_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ResourceStatusFailure_IEs[] = {
    {&X2AP_ResourceStatusFailure_IEs[1], &_v151},
    {&X2AP_ResourceStatusFailure_IEs[2], &_v152},
    {&X2AP_ResourceStatusFailure_IEs[3], &_v153},
    {&X2AP_ResourceStatusFailure_IEs[4], &_v154},
    {NULL, &_v155}
};

static ObjectSetEntry const X2AP_CompleteFailureCauseInformation_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ResourceStatusUpdate_IEs[] = {
    {&X2AP_ResourceStatusUpdate_IEs[1], &_v157},
    {&X2AP_ResourceStatusUpdate_IEs[2], &_v158},
    {NULL, &_v159}
};

static ObjectSetEntry const X2AP_CellMeasurementResult_Item_ExtIEs[] = {
    {&X2AP_CellMeasurementResult_Item_ExtIEs[1], &_v161},
    {&X2AP_CellMeasurementResult_Item_ExtIEs[2], &_v162},
    {NULL, &_v163}
};

static ObjectSetEntry const X2AP_PrivateMessage_IEs[] = {
{0}};

static ObjectSetEntry const X2AP_MobilityChangeRequest_IEs[] = {
    {&X2AP_MobilityChangeRequest_IEs[1], &_v164},
    {&X2AP_MobilityChangeRequest_IEs[2], &_v165},
    {&X2AP_MobilityChangeRequest_IEs[3], &_v166},
    {&X2AP_MobilityChangeRequest_IEs[4], &_v167},
    {NULL, &_v168}
};

static ObjectSetEntry const X2AP_MobilityChangeAcknowledge_IEs[] = {
    {&X2AP_MobilityChangeAcknowledge_IEs[1], &_v169},
    {&X2AP_MobilityChangeAcknowledge_IEs[2], &_v170},
    {NULL, &_v171}
};

static ObjectSetEntry const X2AP_MobilityChangeFailure_IEs[] = {
    {&X2AP_MobilityChangeFailure_IEs[1], &_v172},
    {&X2AP_MobilityChangeFailure_IEs[2], &_v173},
    {&X2AP_MobilityChangeFailure_IEs[3], &_v174},
    {&X2AP_MobilityChangeFailure_IEs[4], &_v175},
    {NULL, &_v176}
};

static ObjectSetEntry const X2AP_RLFIndication_IEs[] = {
    {&X2AP_RLFIndication_IEs[1], &_v177},
    {&X2AP_RLFIndication_IEs[2], &_v178},
    {&X2AP_RLFIndication_IEs[3], &_v179},
    {&X2AP_RLFIndication_IEs[4], &_v180},
    {&X2AP_RLFIndication_IEs[5], &_v181},
    {&X2AP_RLFIndication_IEs[6], &_v182},
    {&X2AP_RLFIndication_IEs[7], &_v183},
    {NULL, &_v184}
};

static ObjectSetEntry const X2AP_CellActivationRequest_IEs[] = {
    {NULL, &_v185}
};

static ObjectSetEntry const X2AP_ServedCellsToActivate_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CellActivationResponse_IEs[] = {
    {&X2AP_CellActivationResponse_IEs[1], &_v186},
    {NULL, &_v187}
};

static ObjectSetEntry const X2AP_ActivatedCellList_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CellActivationFailure_IEs[] = {
    {&X2AP_CellActivationFailure_IEs[1], &_v188},
    {NULL, &_v189}
};

static ObjectSetEntry const X2AP_X2Release_IEs[] = {
    {NULL, &_v190}
};

static ObjectSetEntry const X2AP_X2APMessageTransfer_IEs[] = {
    {&X2AP_X2APMessageTransfer_IEs[1], &_v191},
    {NULL, &_v192}
};

static ObjectSetEntry const X2AP_RNL_Header_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBAdditionRequest_IEs[] = {
    {&X2AP_SeNBAdditionRequest_IEs[1], &_v193},
    {&X2AP_SeNBAdditionRequest_IEs[2], &_v194},
    {&X2AP_SeNBAdditionRequest_IEs[3], &_v195},
    {&X2AP_SeNBAdditionRequest_IEs[4], &_v196},
    {&X2AP_SeNBAdditionRequest_IEs[5], &_v197},
    {&X2AP_SeNBAdditionRequest_IEs[6], &_v198},
    {NULL, &_v199}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ItemIEs[] = {
    {NULL, &_v200}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_Item_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_Item_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBAdditionRequestAcknowledge_IEs[] = {
    {&X2AP_SeNBAdditionRequestAcknowledge_IEs[1], &_v201},
    {&X2AP_SeNBAdditionRequestAcknowledge_IEs[2], &_v202},
    {&X2AP_SeNBAdditionRequestAcknowledge_IEs[3], &_v203},
    {&X2AP_SeNBAdditionRequestAcknowledge_IEs[4], &_v204},
    {&X2AP_SeNBAdditionRequestAcknowledge_IEs[5], &_v205},
    {NULL, &_v206}
};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBAdditionRequestReject_IEs[] = {
    {&X2AP_SeNBAdditionRequestReject_IEs[1], &_v208},
    {&X2AP_SeNBAdditionRequestReject_IEs[2], &_v209},
    {&X2AP_SeNBAdditionRequestReject_IEs[3], &_v210},
    {NULL, &_v211}
};

static ObjectSetEntry const X2AP_SeNBReconfigurationComplete_IEs[] = {
    {&X2AP_SeNBReconfigurationComplete_IEs[1], &_v212},
    {&X2AP_SeNBReconfigurationComplete_IEs[2], &_v213},
    {NULL, &_v214}
};

static ObjectSetEntry const X2AP_ResponseInformationSeNBReconfComp_SuccessItemExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItemExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBModificationRequest_IEs[] = {
    {&X2AP_SeNBModificationRequest_IEs[1], &_v215},
    {&X2AP_SeNBModificationRequest_IEs[2], &_v216},
    {&X2AP_SeNBModificationRequest_IEs[3], &_v217},
    {&X2AP_SeNBModificationRequest_IEs[4], &_v218},
    {&X2AP_SeNBModificationRequest_IEs[5], &_v219},
    {&X2AP_SeNBModificationRequest_IEs[6], &_v220},
    {NULL, &_v221}
};

static ObjectSetEntry const X2AP_UE_ContextInformationSeNBModReqExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItemIEs[] = {
    {NULL, &_v222}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItemIEs[] = {
    {NULL, &_v223}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItemIEs[] = {
    {NULL, &_v224}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBModificationRequestAcknowledge_IEs[] = {
    {&X2AP_SeNBModificationRequestAcknowledge_IEs[1], &_v225},
    {&X2AP_SeNBModificationRequestAcknowledge_IEs[2], &_v226},
    {&X2AP_SeNBModificationRequestAcknowledge_IEs[3], &_v227},
    {&X2AP_SeNBModificationRequestAcknowledge_IEs[4], &_v228},
    {&X2AP_SeNBModificationRequestAcknowledge_IEs[5], &_v229},
    {&X2AP_SeNBModificationRequestAcknowledge_IEs[6], &_v230},
    {&X2AP_SeNBModificationRequestAcknowledge_IEs[7], &_v231},
    {NULL, &_v232}
};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBModificationRequestReject_IEs[] = {
    {&X2AP_SeNBModificationRequestReject_IEs[1], &_v236},
    {&X2AP_SeNBModificationRequestReject_IEs[2], &_v237},
    {&X2AP_SeNBModificationRequestReject_IEs[3], &_v238},
    {NULL, &_v239}
};

static ObjectSetEntry const X2AP_SeNBModificationRequired_IEs[] = {
    {&X2AP_SeNBModificationRequired_IEs[1], &_v240},
    {&X2AP_SeNBModificationRequired_IEs[2], &_v241},
    {&X2AP_SeNBModificationRequired_IEs[3], &_v242},
    {&X2AP_SeNBModificationRequired_IEs[4], &_v243},
    {&X2AP_SeNBModificationRequired_IEs[5], &_v244},
    {NULL, &_v245}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqdItemIEs[] = {
    {NULL, &_v246}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqdItemExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBModificationConfirm_IEs[] = {
    {&X2AP_SeNBModificationConfirm_IEs[1], &_v247},
    {&X2AP_SeNBModificationConfirm_IEs[2], &_v248},
    {&X2AP_SeNBModificationConfirm_IEs[3], &_v249},
    {NULL, &_v250}
};

static ObjectSetEntry const X2AP_SeNBModificationRefuse_IEs[] = {
    {&X2AP_SeNBModificationRefuse_IEs[1], &_v251},
    {&X2AP_SeNBModificationRefuse_IEs[2], &_v252},
    {&X2AP_SeNBModificationRefuse_IEs[3], &_v253},
    {&X2AP_SeNBModificationRefuse_IEs[4], &_v254},
    {NULL, &_v255}
};

static ObjectSetEntry const X2AP_SeNBReleaseRequest_IEs[] = {
    {&X2AP_SeNBReleaseRequest_IEs[1], &_v256},
    {&X2AP_SeNBReleaseRequest_IEs[2], &_v257},
    {&X2AP_SeNBReleaseRequest_IEs[3], &_v258},
    {NULL, &_v259}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItemIEs[] = {
    {NULL, &_v260}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBReleaseRequired_IEs[] = {
    {&X2AP_SeNBReleaseRequired_IEs[1], &_v261},
    {&X2AP_SeNBReleaseRequired_IEs[2], &_v262},
    {NULL, &_v263}
};

static ObjectSetEntry const X2AP_SeNBReleaseConfirm_IEs[] = {
    {&X2AP_SeNBReleaseConfirm_IEs[1], &_v264},
    {&X2AP_SeNBReleaseConfirm_IEs[2], &_v265},
    {&X2AP_SeNBReleaseConfirm_IEs[3], &_v266},
    {NULL, &_v267}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItemIEs[] = {
    {NULL, &_v268}
};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItem_Split_BearerExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_SeNBCounterCheckRequest_IEs[] = {
    {&X2AP_SeNBCounterCheckRequest_IEs[1], &_v269},
    {&X2AP_SeNBCounterCheckRequest_IEs[2], &_v270},
    {NULL, &_v271}
};

static ObjectSetEntry const X2AP_E_RABs_SubjectToCounterCheckItemIEs[] = {
    {NULL, &_v272}
};

static ObjectSetEntry const X2AP_E_RABs_SubjectToCounterCheckItemExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_X2RemovalRequest_IEs[] = {
    {NULL, &_v273}
};

static ObjectSetEntry const X2AP_X2RemovalResponse_IEs[] = {
    {&X2AP_X2RemovalResponse_IEs[1], &_v274},
    {NULL, &_v275}
};

static ObjectSetEntry const X2AP_X2RemovalFailure_IEs[] = {
    {&X2AP_X2RemovalFailure_IEs[1], &_v276},
    {NULL, &_v277}
};

static ObjectSetEntry const X2AP_ABSInformationFDD_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ABSInformationTDD_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ABS_Status_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_AdditionalSpecialSubframe_Info_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_AS_SecurityInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_AllocationAndRetentionPriority_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CellBasedMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CellType_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CoMPHypothesisSetItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CoMPInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CoMPInformationItem_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CoMPInformationStartTime_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CompositeAvailableCapacityGroup_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CompositeAvailableCapacity_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_COUNTvalue_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_COUNTValueExtended_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CriticalityDiagnostics_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_CriticalityDiagnostics_IE_List_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_DynamicNAICSInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_FDD_Info_ExtIEs[] = {
    {&X2AP_FDD_Info_ExtIEs[1], &_v278},
    {NULL, &_v279}
};

static ObjectSetEntry const X2AP_TDD_Info_ExtIEs[] = {
    {&X2AP_TDD_Info_ExtIEs[1], &_v280},
    {NULL, &_v281}
};

static ObjectSetEntry const X2AP_ECGI_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RAB_Level_QoS_Parameters_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RAB_ItemIEs[] = {
    {NULL, &_v282}
};

static ObjectSetEntry const X2AP_E_RAB_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ExpectedUEBehaviour_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ExpectedUEActivityBehaviour_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ExtendedULInterferenceOverloadInfo_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ForbiddenTAs_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ForbiddenLAs_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_GBR_QosInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_GlobalENB_ID_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_GTPtunnelEndpoint_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_GU_Group_ID_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_GUMMEI_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_HandoverRestrictionList_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_HWLoadIndicator_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_LastVisitedEUTRANCellInformation_ExtIEs[] = {
    {&X2AP_LastVisitedEUTRANCellInformation_ExtIEs[1], &_v283},
    {NULL, &_v284}
};

static ObjectSetEntry const X2AP_LocationReportingInformation_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_M3Configuration_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_M4Configuration_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_M5Configuration_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_MDT_Configuration_ExtIEs[] = {
    {&X2AP_MDT_Configuration_ExtIEs[1], &_v285},
    {&X2AP_MDT_Configuration_ExtIEs[2], &_v286},
    {&X2AP_MDT_Configuration_ExtIEs[3], &_v287},
    {&X2AP_MDT_Configuration_ExtIEs[4], &_v288},
    {NULL, &_v289}
};

static ObjectSetEntry const X2AP_MBSFN_Subframe_Info_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_BandInfo_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_Neighbour_Information_ExtIEs[] = {
    {&X2AP_Neighbour_Information_ExtIEs[1], &_v290},
    {NULL, &_v291}
};

static ObjectSetEntry const X2AP_M1PeriodicReporting_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_PRACH_Configuration_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ProSeAuthorized_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_RadioResourceStatus_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_RelativeNarrowbandTxPower_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_RSRPMeasurementResult_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_RSRPMRList_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_S1TNLLoadIndicator_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ServedCell_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_ServedCell_Information_ExtIEs[] = {
    {&X2AP_ServedCell_Information_ExtIEs[1], &_v292},
    {&X2AP_ServedCell_Information_ExtIEs[2], &_v293},
    {&X2AP_ServedCell_Information_ExtIEs[3], &_v294},
    {&X2AP_ServedCell_Information_ExtIEs[4], &_v295},
    {&X2AP_ServedCell_Information_ExtIEs[5], &_v296},
    {&X2AP_ServedCell_Information_ExtIEs[6], &_v297},
    {NULL, &_v298}
};

static ObjectSetEntry const X2AP_SpecialSubframe_Info_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_TABasedMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_TAIBasedMDT_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_TAI_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_M1ThresholdEventA2_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_TraceActivation_ExtIEs[] = {
    {NULL, &_v299}
};

static ObjectSetEntry const X2AP_UEAggregate_MaximumBitrate_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_UESecurityCapabilities_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_UL_HighInterferenceIndicationInfo_Item_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_UsableABSInformationFDD_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_UsableABSInformationTDD_ExtIEs[] = {
{0}};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ItemIEs[] = {
    {NULL, &_v76}
};

static ObjectSetEntry const X2AP_CompleteFailureCauseInformation_ItemIEs[] = {
    {NULL, &_v156}
};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItemIEs[] = {
    {NULL, &_v234}
};

static ObjectSetEntry const X2AP_E_RABs_SubjectToStatusTransfer_ItemIEs[] = {
    {NULL, &_v93}
};

static ObjectSetEntry const X2AP_MeasurementInitiationResult_ItemIEs[] = {
    {NULL, &_v149}
};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItemIEs[] = {
    {NULL, &_v235}
};

static ObjectSetEntry const X2AP_CellMeasurementResult_ItemIEs[] = {
    {NULL, &_v160}
};

static ObjectSetEntry const X2AP_CellInformation_ItemIEs[] = {
    {NULL, &_v119}
};

static ObjectSetEntry const X2AP_MeasurementFailureCause_ItemIEs[] = {
    {NULL, &_v150}
};

static ObjectSetEntry const X2AP_CellToReport_ItemIEs[] = {
    {NULL, &_v144}
};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ItemIEs[] = {
    {NULL, &_v207}
};

static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItemIEs[] = {
    {NULL, &_v233}
};

#else /* OSS_SPARTAN_AWARE >= 2 */

#if ((OSS_SPARTAN_AWARE + 0) > 12)
static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES[] = {
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[1], &_v1, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[2], &_v2, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[0], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[3], &_v3, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[1], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[4], &_v4, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[2], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[5], &_v5, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[3], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[6], &_v6, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[4], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[7], &_v7, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[5], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[8], &_v8, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[6], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[9], &_v9, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[7], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[10], &_v10, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[8], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[11], &_v11, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[9], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[12], &_v12, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[10], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[13], &_v13, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[11], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[14], &_v14, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[12], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[15], &_v15, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[13], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[16], &_v16, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[14], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[17], &_v17, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[15], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[18], &_v18, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[16], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[19], &_v19, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[17], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[20], &_v20, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[18], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[21], &_v21, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[19], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[22], &_v22, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[20], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[23], &_v23, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[21], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[24], &_v24, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[22], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[25], &_v25, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[23], NULL},
    {NULL, &_v26, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[24], NULL}
};
static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[] = {
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[1], &_v27, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[2], &_v28, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[0], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[3], &_v29, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[1], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[4], &_v30, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[2], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[5], &_v31, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[3], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[6], &_v32, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[4], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[7], &_v33, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[5], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[8], &_v34, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[6], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[9], &_v35, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[7], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[10], &_v36, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[8], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[11], &_v37, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[9], NULL},
    {NULL, &_v38, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[10], NULL}
};
static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[] = {
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[1], &_v39, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[2], &_v40, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[0], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[3], &_v41, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[1], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[4], &_v42, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[2], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[5], &_v43, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[3], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[6], &_v44, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[4], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[7], &_v45, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[5], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[8], &_v46, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[6], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[9], &_v47, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[7], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[10], &_v48, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[8], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[11], &_v49, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[9], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[12], &_v50, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[10], NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[13], &_v51, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[11], NULL},
    {NULL, &_v52, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[12], NULL}
};
static ObjectSetEntry const X2AP_HandoverRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[1], &_v53, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[2], &_v54, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[3], &_v55, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[4], &_v56, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[5], &_v57, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[6], &_v58, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[4], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[7], &_v59, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[5], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[8], &_v60, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[6], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[9], &_v61, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[7], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[10], &_v62, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[8], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[11], &_v63, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[9], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[12], &_v64, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[10], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[13], &_v65, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[11], NULL},
    {NULL, &_v66, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[12], NULL}
};
static ObjectSetEntry const X2AP_UE_ContextInformation_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_UE_ContextInformation_ExtIEs[1], &_v67, NULL, NULL},
    {NULL, &_v68, (ObjectSetEntry *)&X2AP_UE_ContextInformation_ExtIEs[0], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeSetup_ItemIEs[] = {
    {NULL, &_v69, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeSetup_ItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HandoverRequestAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[1], &_v70, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[2], &_v71, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[3], &_v72, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[4], &_v73, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[5], &_v74, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[3], NULL},
    {NULL, &_v75, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[4], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HandoverPreparationFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[1], &_v77, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[2], &_v78, (ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[0], NULL},
    {NULL, &_v79, (ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_HandoverReport_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[1], &_v80, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[2], &_v81, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[3], &_v82, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[4], &_v83, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[5], &_v84, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[6], &_v85, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[4], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[7], &_v86, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[5], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[8], &_v87, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[6], NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[9], &_v88, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[7], NULL},
    {NULL, &_v89, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[8], NULL}
};
static ObjectSetEntry const X2AP_SNStatusTransfer_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[1], &_v90, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[2], &_v91, (ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[0], NULL},
    {NULL, &_v92, (ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[1], &_v94, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[2], &_v95, (ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[0], NULL},
    {NULL, &_v96, (ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[1], NULL}
};
static ObjectSetEntry const X2AP_UEContextRelease_IEs[] = {
    {(ObjectSetEntry *)&X2AP_UEContextRelease_IEs[1], &_v97, NULL, NULL},
    {NULL, &_v98, (ObjectSetEntry *)&X2AP_UEContextRelease_IEs[0], NULL}
};
static ObjectSetEntry const X2AP_HandoverCancel_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverCancel_IEs[1], &_v99, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverCancel_IEs[2], &_v100, (ObjectSetEntry *)&X2AP_HandoverCancel_IEs[0], NULL},
    {NULL, &_v101, (ObjectSetEntry *)&X2AP_HandoverCancel_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_ErrorIndication_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ErrorIndication_IEs[1], &_v102, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ErrorIndication_IEs[2], &_v103, (ObjectSetEntry *)&X2AP_ErrorIndication_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_ErrorIndication_IEs[3], &_v104, (ObjectSetEntry *)&X2AP_ErrorIndication_IEs[1], NULL},
    {NULL, &_v105, (ObjectSetEntry *)&X2AP_ErrorIndication_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_ResetRequest_IEs[] = {
    {NULL, &_v106, NULL, NULL}
};
static ObjectSetEntry const X2AP_ResetResponse_IEs[] = {
    {NULL, &_v107, NULL, NULL}
};
static ObjectSetEntry const X2AP_X2SetupRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[1], &_v108, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[2], &_v109, (ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[0], NULL},
    {NULL, &_v110, (ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_X2SetupResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[1], &_v111, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[2], &_v112, (ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[3], &_v113, (ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[1], NULL},
    {NULL, &_v114, (ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_X2SetupFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[1], &_v115, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[2], &_v116, (ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[0], NULL},
    {NULL, &_v117, (ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_LoadInformation_IEs[] = {
    {NULL, &_v118, NULL, NULL}
};
static ObjectSetEntry const X2AP_CellInformation_Item_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[1], &_v120, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[2], &_v121, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[3], &_v122, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[4], &_v123, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[5], &_v124, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[3], NULL},
    {NULL, &_v125, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[4], NULL}
};
static ObjectSetEntry const X2AP_ENBConfigurationUpdate_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[1], &_v126, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[2], &_v127, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[3], &_v128, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[4], &_v129, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[2], NULL},
    {NULL, &_v130, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[3], NULL}
};
static ObjectSetEntry const X2AP_ServedCellsToModify_Item_ExtIEs[] = {
    {NULL, &_v131, NULL, NULL}
};
static ObjectSetEntry const X2AP_ENBConfigurationUpdateAcknowledge_IEs[] = {
    {NULL, &_v132, NULL, NULL}
};
static ObjectSetEntry const X2AP_ENBConfigurationUpdateFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[1], &_v133, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[2], &_v134, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[0], NULL},
    {NULL, &_v135, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_ResourceStatusRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[1], &_v136, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[2], &_v137, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[3], &_v138, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[4], &_v139, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[5], &_v140, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[6], &_v141, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[4], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[7], &_v142, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[5], NULL},
    {NULL, &_v143, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[6], NULL}
};
static ObjectSetEntry const X2AP_CellToReport_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResourceStatusResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[1], &_v145, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[2], &_v146, (ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[3], &_v147, (ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[1], NULL},
    {NULL, &_v148, (ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_MeasurementInitiationResult_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_MeasurementFailureCause_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResourceStatusFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[1], &_v151, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[2], &_v152, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[3], &_v153, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[4], &_v154, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[2], NULL},
    {NULL, &_v155, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[3], NULL}
};
static ObjectSetEntry const X2AP_CompleteFailureCauseInformation_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResourceStatusUpdate_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[1], &_v157, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[2], &_v158, (ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[0], NULL},
    {NULL, &_v159, (ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_CellMeasurementResult_Item_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[1], &_v161, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[2], &_v162, (ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[0], NULL},
    {NULL, &_v163, (ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[1], NULL}
};
static ObjectSetEntry const X2AP_PrivateMessage_IEs[] = {
{0}};
static ObjectSetEntry const X2AP_MobilityChangeRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[1], &_v164, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[2], &_v165, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[3], &_v166, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[4], &_v167, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[2], NULL},
    {NULL, &_v168, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[3], NULL}
};
static ObjectSetEntry const X2AP_MobilityChangeAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[1], &_v169, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[2], &_v170, (ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[0], NULL},
    {NULL, &_v171, (ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_MobilityChangeFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[1], &_v172, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[2], &_v173, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[3], &_v174, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[4], &_v175, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[2], NULL},
    {NULL, &_v176, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[3], NULL}
};
static ObjectSetEntry const X2AP_RLFIndication_IEs[] = {
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[1], &_v177, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[2], &_v178, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[3], &_v179, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[4], &_v180, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[5], &_v181, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[6], &_v182, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[4], NULL},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[7], &_v183, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[5], NULL},
    {NULL, &_v184, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[6], NULL}
};
static ObjectSetEntry const X2AP_CellActivationRequest_IEs[] = {
    {NULL, &_v185, NULL, NULL}
};
static ObjectSetEntry const X2AP_ServedCellsToActivate_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellActivationResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_CellActivationResponse_IEs[1], &_v186, NULL, NULL},
    {NULL, &_v187, (ObjectSetEntry *)&X2AP_CellActivationResponse_IEs[0], NULL}
};
static ObjectSetEntry const X2AP_ActivatedCellList_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellActivationFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_CellActivationFailure_IEs[1], &_v188, NULL, NULL},
    {NULL, &_v189, (ObjectSetEntry *)&X2AP_CellActivationFailure_IEs[0], NULL}
};
static ObjectSetEntry const X2AP_X2Release_IEs[] = {
    {NULL, &_v190, NULL, NULL}
};
static ObjectSetEntry const X2AP_X2APMessageTransfer_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2APMessageTransfer_IEs[1], &_v191, NULL, NULL},
    {NULL, &_v192, (ObjectSetEntry *)&X2AP_X2APMessageTransfer_IEs[0], NULL}
};
static ObjectSetEntry const X2AP_RNL_Header_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBAdditionRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[1], &_v193, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[2], &_v194, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[3], &_v195, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[4], &_v196, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[5], &_v197, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[6], &_v198, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[4], NULL},
    {NULL, &_v199, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[5], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ItemIEs[] = {
    {NULL, &_v200, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_Item_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_Item_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBAdditionRequestAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[1], &_v201, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[2], &_v202, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[3], &_v203, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[4], &_v204, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[5], &_v205, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[3], NULL},
    {NULL, &_v206, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[4], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBAdditionRequestReject_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[1], &_v208, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[2], &_v209, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[3], &_v210, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[1], NULL},
    {NULL, &_v211, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_SeNBReconfigurationComplete_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[1], &_v212, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[2], &_v213, (ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[0], NULL},
    {NULL, &_v214, (ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_ResponseInformationSeNBReconfComp_SuccessItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[1], &_v215, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[2], &_v216, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[3], &_v217, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[4], &_v218, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[5], &_v219, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[6], &_v220, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[4], NULL},
    {NULL, &_v221, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[5], NULL}
};
static ObjectSetEntry const X2AP_UE_ContextInformationSeNBModReqExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItemIEs[] = {
    {NULL, &_v222, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItemIEs[] = {
    {NULL, &_v223, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItemIEs[] = {
    {NULL, &_v224, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationRequestAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[1], &_v225, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[2], &_v226, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[3], &_v227, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[4], &_v228, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[5], &_v229, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[6], &_v230, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[4], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[7], &_v231, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[5], NULL},
    {NULL, &_v232, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[6], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationRequestReject_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[1], &_v236, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[2], &_v237, (ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[3], &_v238, (ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[1], NULL},
    {NULL, &_v239, (ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_SeNBModificationRequired_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[1], &_v240, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[2], &_v241, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[3], &_v242, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[4], &_v243, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[5], &_v244, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[3], NULL},
    {NULL, &_v245, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[4], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqdItemIEs[] = {
    {NULL, &_v246, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqdItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationConfirm_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[1], &_v247, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[2], &_v248, (ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[3], &_v249, (ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[1], NULL},
    {NULL, &_v250, (ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_SeNBModificationRefuse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[1], &_v251, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[2], &_v252, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[3], &_v253, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[4], &_v254, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[2], NULL},
    {NULL, &_v255, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[3], NULL}
};
static ObjectSetEntry const X2AP_SeNBReleaseRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[1], &_v256, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[2], &_v257, (ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[3], &_v258, (ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[1], NULL},
    {NULL, &_v259, (ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItemIEs[] = {
    {NULL, &_v260, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBReleaseRequired_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[1], &_v261, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[2], &_v262, (ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[0], NULL},
    {NULL, &_v263, (ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_SeNBReleaseConfirm_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[1], &_v264, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[2], &_v265, (ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[3], &_v266, (ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[1], NULL},
    {NULL, &_v267, (ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[2], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItemIEs[] = {
    {NULL, &_v268, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBCounterCheckRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[1], &_v269, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[2], &_v270, (ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[0], NULL},
    {NULL, &_v271, (ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[1], NULL}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToCounterCheckItemIEs[] = {
    {NULL, &_v272, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToCounterCheckItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_X2RemovalRequest_IEs[] = {
    {NULL, &_v273, NULL, NULL}
};
static ObjectSetEntry const X2AP_X2RemovalResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2RemovalResponse_IEs[1], &_v274, NULL, NULL},
    {NULL, &_v275, (ObjectSetEntry *)&X2AP_X2RemovalResponse_IEs[0], NULL}
};
static ObjectSetEntry const X2AP_X2RemovalFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2RemovalFailure_IEs[1], &_v276, NULL, NULL},
    {NULL, &_v277, (ObjectSetEntry *)&X2AP_X2RemovalFailure_IEs[0], NULL}
};
static ObjectSetEntry const X2AP_ABSInformationFDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ABSInformationTDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ABS_Status_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_AdditionalSpecialSubframe_Info_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_AS_SecurityInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_AllocationAndRetentionPriority_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellType_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPHypothesisSetItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPInformationItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPInformationStartTime_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CompositeAvailableCapacityGroup_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CompositeAvailableCapacity_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_COUNTvalue_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_COUNTValueExtended_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CriticalityDiagnostics_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CriticalityDiagnostics_IE_List_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_DynamicNAICSInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_FDD_Info_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_FDD_Info_ExtIEs[1], &_v278, NULL, NULL},
    {NULL, &_v279, (ObjectSetEntry *)&X2AP_FDD_Info_ExtIEs[0], NULL}
};
static ObjectSetEntry const X2AP_TDD_Info_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_TDD_Info_ExtIEs[1], &_v280, NULL, NULL},
    {NULL, &_v281, (ObjectSetEntry *)&X2AP_TDD_Info_ExtIEs[0], NULL}
};
static ObjectSetEntry const X2AP_ECGI_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RAB_Level_QoS_Parameters_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RAB_ItemIEs[] = {
    {NULL, &_v282, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RAB_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ExpectedUEBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ExpectedUEActivityBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ExtendedULInterferenceOverloadInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ForbiddenTAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ForbiddenLAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GBR_QosInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GlobalENB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GTPtunnelEndpoint_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GU_Group_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GUMMEI_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HandoverRestrictionList_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HWLoadIndicator_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_LastVisitedEUTRANCellInformation_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_LastVisitedEUTRANCellInformation_ExtIEs[1], &_v283, NULL, NULL},
    {NULL, &_v284, (ObjectSetEntry *)&X2AP_LastVisitedEUTRANCellInformation_ExtIEs[0], NULL}
};
static ObjectSetEntry const X2AP_LocationReportingInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M3Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M4Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M5Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_MDT_Configuration_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[1], &_v285, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[2], &_v286, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[3], &_v287, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[4], &_v288, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[2], NULL},
    {NULL, &_v289, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[3], NULL}
};
static ObjectSetEntry const X2AP_MBSFN_Subframe_Info_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_BandInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_Neighbour_Information_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_Neighbour_Information_ExtIEs[1], &_v290, NULL, NULL},
    {NULL, &_v291, (ObjectSetEntry *)&X2AP_Neighbour_Information_ExtIEs[0], NULL}
};
static ObjectSetEntry const X2AP_M1PeriodicReporting_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_PRACH_Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ProSeAuthorized_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RadioResourceStatus_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RelativeNarrowbandTxPower_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RSRPMeasurementResult_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RSRPMRList_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_S1TNLLoadIndicator_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ServedCell_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ServedCell_Information_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[1], &_v292, NULL, NULL},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[2], &_v293, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[0], NULL},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[3], &_v294, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[1], NULL},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[4], &_v295, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[2], NULL},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[5], &_v296, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[3], NULL},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[6], &_v297, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[4], NULL},
    {NULL, &_v298, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[5], NULL}
};
static ObjectSetEntry const X2AP_SpecialSubframe_Info_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TABasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TAIBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M1ThresholdEventA2_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TraceActivation_ExtIEs[] = {
    {NULL, &_v299, NULL, NULL}
};
static ObjectSetEntry const X2AP_UEAggregate_MaximumBitrate_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UESecurityCapabilities_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UL_HighInterferenceIndicationInfo_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UsableABSInformationFDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UsableABSInformationTDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ItemIEs[] = {
    {NULL, &_v76, NULL, NULL}
};
static ObjectSetEntry const X2AP_CompleteFailureCauseInformation_ItemIEs[] = {
    {NULL, &_v156, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItemIEs[] = {
    {NULL, &_v234, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToStatusTransfer_ItemIEs[] = {
    {NULL, &_v93, NULL, NULL}
};
static ObjectSetEntry const X2AP_MeasurementInitiationResult_ItemIEs[] = {
    {NULL, &_v149, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItemIEs[] = {
    {NULL, &_v235, NULL, NULL}
};
static ObjectSetEntry const X2AP_CellMeasurementResult_ItemIEs[] = {
    {NULL, &_v160, NULL, NULL}
};
static ObjectSetEntry const X2AP_CellInformation_ItemIEs[] = {
    {NULL, &_v119, NULL, NULL}
};
static ObjectSetEntry const X2AP_MeasurementFailureCause_ItemIEs[] = {
    {NULL, &_v150, NULL, NULL}
};
static ObjectSetEntry const X2AP_CellToReport_ItemIEs[] = {
    {NULL, &_v144, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ItemIEs[] = {
    {NULL, &_v207, NULL, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItemIEs[] = {
    {NULL, &_v233, NULL, NULL}
};
#else /* OSS_SPARTAN_AWARE <= 12 */
static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES[] = {
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[1], &_v1, NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[2], &_v2, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[0]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[3], &_v3, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[1]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[4], &_v4, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[2]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[5], &_v5, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[3]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[6], &_v6, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[4]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[7], &_v7, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[5]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[8], &_v8, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[6]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[9], &_v9, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[7]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[10], &_v10, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[8]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[11], &_v11, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[9]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[12], &_v12, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[10]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[13], &_v13, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[11]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[14], &_v14, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[12]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[15], &_v15, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[13]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[16], &_v16, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[14]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[17], &_v17, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[15]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[18], &_v18, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[16]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[19], &_v19, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[17]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[20], &_v20, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[18]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[21], &_v21, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[19]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[22], &_v22, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[20]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[23], &_v23, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[21]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[24], &_v24, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[22]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[25], &_v25, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[23]},
    {NULL, &_v26, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES[24]}
};
static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[] = {
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[1], &_v27, NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[2], &_v28, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[0]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[3], &_v29, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[1]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[4], &_v30, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[2]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[5], &_v31, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[3]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[6], &_v32, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[4]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[7], &_v33, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[5]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[8], &_v34, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[6]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[9], &_v35, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[7]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[10], &_v36, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[8]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[11], &_v37, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[9]},
    {NULL, &_v38, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1[10]}
};
static ObjectSetEntry const X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[] = {
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[1], &_v39, NULL},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[2], &_v40, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[0]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[3], &_v41, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[1]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[4], &_v42, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[2]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[5], &_v43, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[3]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[6], &_v44, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[4]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[7], &_v45, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[5]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[8], &_v46, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[6]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[9], &_v47, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[7]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[10], &_v48, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[8]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[11], &_v49, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[9]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[12], &_v50, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[10]},
    {(ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[13], &_v51, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[11]},
    {NULL, &_v52, (ObjectSetEntry *)&X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2[12]}
};
static ObjectSetEntry const X2AP_HandoverRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[1], &_v53, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[2], &_v54, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[0]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[3], &_v55, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[1]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[4], &_v56, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[2]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[5], &_v57, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[3]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[6], &_v58, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[4]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[7], &_v59, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[5]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[8], &_v60, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[6]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[9], &_v61, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[7]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[10], &_v62, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[8]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[11], &_v63, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[9]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[12], &_v64, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[10]},
    {(ObjectSetEntry *)&X2AP_HandoverRequest_IEs[13], &_v65, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[11]},
    {NULL, &_v66, (ObjectSetEntry *)&X2AP_HandoverRequest_IEs[12]}
};
static ObjectSetEntry const X2AP_UE_ContextInformation_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_UE_ContextInformation_ExtIEs[1], &_v67, NULL},
    {NULL, &_v68, (ObjectSetEntry *)&X2AP_UE_ContextInformation_ExtIEs[0]}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeSetup_ItemIEs[] = {
    {NULL, &_v69, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeSetup_ItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HandoverRequestAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[1], &_v70, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[2], &_v71, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[0]},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[3], &_v72, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[1]},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[4], &_v73, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[2]},
    {(ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[5], &_v74, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[3]},
    {NULL, &_v75, (ObjectSetEntry *)&X2AP_HandoverRequestAcknowledge_IEs[4]}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HandoverPreparationFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[1], &_v77, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[2], &_v78, (ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[0]},
    {NULL, &_v79, (ObjectSetEntry *)&X2AP_HandoverPreparationFailure_IEs[1]}
};
static ObjectSetEntry const X2AP_HandoverReport_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[1], &_v80, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[2], &_v81, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[0]},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[3], &_v82, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[1]},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[4], &_v83, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[2]},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[5], &_v84, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[3]},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[6], &_v85, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[4]},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[7], &_v86, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[5]},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[8], &_v87, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[6]},
    {(ObjectSetEntry *)&X2AP_HandoverReport_IEs[9], &_v88, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[7]},
    {NULL, &_v89, (ObjectSetEntry *)&X2AP_HandoverReport_IEs[8]}
};
static ObjectSetEntry const X2AP_SNStatusTransfer_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[1], &_v90, NULL},
    {(ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[2], &_v91, (ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[0]},
    {NULL, &_v92, (ObjectSetEntry *)&X2AP_SNStatusTransfer_IEs[1]}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[1], &_v94, NULL},
    {(ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[2], &_v95, (ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[0]},
    {NULL, &_v96, (ObjectSetEntry *)&X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs[1]}
};
static ObjectSetEntry const X2AP_UEContextRelease_IEs[] = {
    {(ObjectSetEntry *)&X2AP_UEContextRelease_IEs[1], &_v97, NULL},
    {NULL, &_v98, (ObjectSetEntry *)&X2AP_UEContextRelease_IEs[0]}
};
static ObjectSetEntry const X2AP_HandoverCancel_IEs[] = {
    {(ObjectSetEntry *)&X2AP_HandoverCancel_IEs[1], &_v99, NULL},
    {(ObjectSetEntry *)&X2AP_HandoverCancel_IEs[2], &_v100, (ObjectSetEntry *)&X2AP_HandoverCancel_IEs[0]},
    {NULL, &_v101, (ObjectSetEntry *)&X2AP_HandoverCancel_IEs[1]}
};
static ObjectSetEntry const X2AP_ErrorIndication_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ErrorIndication_IEs[1], &_v102, NULL},
    {(ObjectSetEntry *)&X2AP_ErrorIndication_IEs[2], &_v103, (ObjectSetEntry *)&X2AP_ErrorIndication_IEs[0]},
    {(ObjectSetEntry *)&X2AP_ErrorIndication_IEs[3], &_v104, (ObjectSetEntry *)&X2AP_ErrorIndication_IEs[1]},
    {NULL, &_v105, (ObjectSetEntry *)&X2AP_ErrorIndication_IEs[2]}
};
static ObjectSetEntry const X2AP_ResetRequest_IEs[] = {
    {NULL, &_v106, NULL}
};
static ObjectSetEntry const X2AP_ResetResponse_IEs[] = {
    {NULL, &_v107, NULL}
};
static ObjectSetEntry const X2AP_X2SetupRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[1], &_v108, NULL},
    {(ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[2], &_v109, (ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[0]},
    {NULL, &_v110, (ObjectSetEntry *)&X2AP_X2SetupRequest_IEs[1]}
};
static ObjectSetEntry const X2AP_X2SetupResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[1], &_v111, NULL},
    {(ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[2], &_v112, (ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[0]},
    {(ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[3], &_v113, (ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[1]},
    {NULL, &_v114, (ObjectSetEntry *)&X2AP_X2SetupResponse_IEs[2]}
};
static ObjectSetEntry const X2AP_X2SetupFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[1], &_v115, NULL},
    {(ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[2], &_v116, (ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[0]},
    {NULL, &_v117, (ObjectSetEntry *)&X2AP_X2SetupFailure_IEs[1]}
};
static ObjectSetEntry const X2AP_LoadInformation_IEs[] = {
    {NULL, &_v118, NULL}
};
static ObjectSetEntry const X2AP_CellInformation_Item_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[1], &_v120, NULL},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[2], &_v121, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[0]},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[3], &_v122, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[1]},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[4], &_v123, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[2]},
    {(ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[5], &_v124, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[3]},
    {NULL, &_v125, (ObjectSetEntry *)&X2AP_CellInformation_Item_ExtIEs[4]}
};
static ObjectSetEntry const X2AP_ENBConfigurationUpdate_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[1], &_v126, NULL},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[2], &_v127, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[0]},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[3], &_v128, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[1]},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[4], &_v129, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[2]},
    {NULL, &_v130, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdate_IEs[3]}
};
static ObjectSetEntry const X2AP_ServedCellsToModify_Item_ExtIEs[] = {
    {NULL, &_v131, NULL}
};
static ObjectSetEntry const X2AP_ENBConfigurationUpdateAcknowledge_IEs[] = {
    {NULL, &_v132, NULL}
};
static ObjectSetEntry const X2AP_ENBConfigurationUpdateFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[1], &_v133, NULL},
    {(ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[2], &_v134, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[0]},
    {NULL, &_v135, (ObjectSetEntry *)&X2AP_ENBConfigurationUpdateFailure_IEs[1]}
};
static ObjectSetEntry const X2AP_ResourceStatusRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[1], &_v136, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[2], &_v137, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[0]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[3], &_v138, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[1]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[4], &_v139, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[2]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[5], &_v140, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[3]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[6], &_v141, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[4]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[7], &_v142, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[5]},
    {NULL, &_v143, (ObjectSetEntry *)&X2AP_ResourceStatusRequest_IEs[6]}
};
static ObjectSetEntry const X2AP_CellToReport_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResourceStatusResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[1], &_v145, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[2], &_v146, (ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[0]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[3], &_v147, (ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[1]},
    {NULL, &_v148, (ObjectSetEntry *)&X2AP_ResourceStatusResponse_IEs[2]}
};
static ObjectSetEntry const X2AP_MeasurementInitiationResult_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_MeasurementFailureCause_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResourceStatusFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[1], &_v151, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[2], &_v152, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[0]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[3], &_v153, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[1]},
    {(ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[4], &_v154, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[2]},
    {NULL, &_v155, (ObjectSetEntry *)&X2AP_ResourceStatusFailure_IEs[3]}
};
static ObjectSetEntry const X2AP_CompleteFailureCauseInformation_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResourceStatusUpdate_IEs[] = {
    {(ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[1], &_v157, NULL},
    {(ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[2], &_v158, (ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[0]},
    {NULL, &_v159, (ObjectSetEntry *)&X2AP_ResourceStatusUpdate_IEs[1]}
};
static ObjectSetEntry const X2AP_CellMeasurementResult_Item_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[1], &_v161, NULL},
    {(ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[2], &_v162, (ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[0]},
    {NULL, &_v163, (ObjectSetEntry *)&X2AP_CellMeasurementResult_Item_ExtIEs[1]}
};
static ObjectSetEntry const X2AP_PrivateMessage_IEs[] = {
{0}};
static ObjectSetEntry const X2AP_MobilityChangeRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[1], &_v164, NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[2], &_v165, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[0]},
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[3], &_v166, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[1]},
    {(ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[4], &_v167, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[2]},
    {NULL, &_v168, (ObjectSetEntry *)&X2AP_MobilityChangeRequest_IEs[3]}
};
static ObjectSetEntry const X2AP_MobilityChangeAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[1], &_v169, NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[2], &_v170, (ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[0]},
    {NULL, &_v171, (ObjectSetEntry *)&X2AP_MobilityChangeAcknowledge_IEs[1]}
};
static ObjectSetEntry const X2AP_MobilityChangeFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[1], &_v172, NULL},
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[2], &_v173, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[0]},
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[3], &_v174, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[1]},
    {(ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[4], &_v175, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[2]},
    {NULL, &_v176, (ObjectSetEntry *)&X2AP_MobilityChangeFailure_IEs[3]}
};
static ObjectSetEntry const X2AP_RLFIndication_IEs[] = {
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[1], &_v177, NULL},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[2], &_v178, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[0]},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[3], &_v179, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[1]},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[4], &_v180, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[2]},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[5], &_v181, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[3]},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[6], &_v182, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[4]},
    {(ObjectSetEntry *)&X2AP_RLFIndication_IEs[7], &_v183, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[5]},
    {NULL, &_v184, (ObjectSetEntry *)&X2AP_RLFIndication_IEs[6]}
};
static ObjectSetEntry const X2AP_CellActivationRequest_IEs[] = {
    {NULL, &_v185, NULL}
};
static ObjectSetEntry const X2AP_ServedCellsToActivate_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellActivationResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_CellActivationResponse_IEs[1], &_v186, NULL},
    {NULL, &_v187, (ObjectSetEntry *)&X2AP_CellActivationResponse_IEs[0]}
};
static ObjectSetEntry const X2AP_ActivatedCellList_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellActivationFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_CellActivationFailure_IEs[1], &_v188, NULL},
    {NULL, &_v189, (ObjectSetEntry *)&X2AP_CellActivationFailure_IEs[0]}
};
static ObjectSetEntry const X2AP_X2Release_IEs[] = {
    {NULL, &_v190, NULL}
};
static ObjectSetEntry const X2AP_X2APMessageTransfer_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2APMessageTransfer_IEs[1], &_v191, NULL},
    {NULL, &_v192, (ObjectSetEntry *)&X2AP_X2APMessageTransfer_IEs[0]}
};
static ObjectSetEntry const X2AP_RNL_Header_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBAdditionRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[1], &_v193, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[2], &_v194, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[3], &_v195, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[1]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[4], &_v196, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[2]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[5], &_v197, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[3]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[6], &_v198, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[4]},
    {NULL, &_v199, (ObjectSetEntry *)&X2AP_SeNBAdditionRequest_IEs[5]}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ItemIEs[] = {
    {NULL, &_v200, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_Item_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_Item_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBAdditionRequestAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[1], &_v201, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[2], &_v202, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[3], &_v203, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[1]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[4], &_v204, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[2]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[5], &_v205, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[3]},
    {NULL, &_v206, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestAcknowledge_IEs[4]}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBAdditionRequestReject_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[1], &_v208, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[2], &_v209, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[3], &_v210, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[1]},
    {NULL, &_v211, (ObjectSetEntry *)&X2AP_SeNBAdditionRequestReject_IEs[2]}
};
static ObjectSetEntry const X2AP_SeNBReconfigurationComplete_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[1], &_v212, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[2], &_v213, (ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[0]},
    {NULL, &_v214, (ObjectSetEntry *)&X2AP_SeNBReconfigurationComplete_IEs[1]}
};
static ObjectSetEntry const X2AP_ResponseInformationSeNBReconfComp_SuccessItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[1], &_v215, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[2], &_v216, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[3], &_v217, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[1]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[4], &_v218, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[2]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[5], &_v219, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[3]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[6], &_v220, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[4]},
    {NULL, &_v221, (ObjectSetEntry *)&X2AP_SeNBModificationRequest_IEs[5]}
};
static ObjectSetEntry const X2AP_UE_ContextInformationSeNBModReqExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItemIEs[] = {
    {NULL, &_v222, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeAdded_ModReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItemIEs[] = {
    {NULL, &_v223, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeModified_ModReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItemIEs[] = {
    {NULL, &_v224, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationRequestAcknowledge_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[1], &_v225, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[2], &_v226, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[3], &_v227, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[1]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[4], &_v228, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[2]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[5], &_v229, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[3]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[6], &_v230, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[4]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[7], &_v231, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[5]},
    {NULL, &_v232, (ObjectSetEntry *)&X2AP_SeNBModificationRequestAcknowledge_IEs[6]}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationRequestReject_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[1], &_v236, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[2], &_v237, (ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[3], &_v238, (ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[1]},
    {NULL, &_v239, (ObjectSetEntry *)&X2AP_SeNBModificationRequestReject_IEs[2]}
};
static ObjectSetEntry const X2AP_SeNBModificationRequired_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[1], &_v240, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[2], &_v241, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[3], &_v242, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[1]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[4], &_v243, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[2]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[5], &_v244, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[3]},
    {NULL, &_v245, (ObjectSetEntry *)&X2AP_SeNBModificationRequired_IEs[4]}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqdItemIEs[] = {
    {NULL, &_v246, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_ModReqdItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBModificationConfirm_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[1], &_v247, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[2], &_v248, (ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[3], &_v249, (ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[1]},
    {NULL, &_v250, (ObjectSetEntry *)&X2AP_SeNBModificationConfirm_IEs[2]}
};
static ObjectSetEntry const X2AP_SeNBModificationRefuse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[1], &_v251, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[2], &_v252, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[3], &_v253, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[1]},
    {(ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[4], &_v254, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[2]},
    {NULL, &_v255, (ObjectSetEntry *)&X2AP_SeNBModificationRefuse_IEs[3]}
};
static ObjectSetEntry const X2AP_SeNBReleaseRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[1], &_v256, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[2], &_v257, (ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[3], &_v258, (ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[1]},
    {NULL, &_v259, (ObjectSetEntry *)&X2AP_SeNBReleaseRequest_IEs[2]}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItemIEs[] = {
    {NULL, &_v260, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelReqItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBReleaseRequired_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[1], &_v261, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[2], &_v262, (ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[0]},
    {NULL, &_v263, (ObjectSetEntry *)&X2AP_SeNBReleaseRequired_IEs[1]}
};
static ObjectSetEntry const X2AP_SeNBReleaseConfirm_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[1], &_v264, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[2], &_v265, (ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[0]},
    {(ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[3], &_v266, (ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[1]},
    {NULL, &_v267, (ObjectSetEntry *)&X2AP_SeNBReleaseConfirm_IEs[2]}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItemIEs[] = {
    {NULL, &_v268, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_ToBeReleased_RelConfItem_Split_BearerExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_SeNBCounterCheckRequest_IEs[] = {
    {(ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[1], &_v269, NULL},
    {(ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[2], &_v270, (ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[0]},
    {NULL, &_v271, (ObjectSetEntry *)&X2AP_SeNBCounterCheckRequest_IEs[1]}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToCounterCheckItemIEs[] = {
    {NULL, &_v272, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToCounterCheckItemExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_X2RemovalRequest_IEs[] = {
    {NULL, &_v273, NULL}
};
static ObjectSetEntry const X2AP_X2RemovalResponse_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2RemovalResponse_IEs[1], &_v274, NULL},
    {NULL, &_v275, (ObjectSetEntry *)&X2AP_X2RemovalResponse_IEs[0]}
};
static ObjectSetEntry const X2AP_X2RemovalFailure_IEs[] = {
    {(ObjectSetEntry *)&X2AP_X2RemovalFailure_IEs[1], &_v276, NULL},
    {NULL, &_v277, (ObjectSetEntry *)&X2AP_X2RemovalFailure_IEs[0]}
};
static ObjectSetEntry const X2AP_ABSInformationFDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ABSInformationTDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ABS_Status_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_AdditionalSpecialSubframe_Info_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_AS_SecurityInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_AllocationAndRetentionPriority_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CellType_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPHypothesisSetItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPInformationItem_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CoMPInformationStartTime_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CompositeAvailableCapacityGroup_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CompositeAvailableCapacity_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_COUNTvalue_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_COUNTValueExtended_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CriticalityDiagnostics_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_CriticalityDiagnostics_IE_List_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_DynamicNAICSInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_FDD_Info_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_FDD_Info_ExtIEs[1], &_v278, NULL},
    {NULL, &_v279, (ObjectSetEntry *)&X2AP_FDD_Info_ExtIEs[0]}
};
static ObjectSetEntry const X2AP_TDD_Info_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_TDD_Info_ExtIEs[1], &_v280, NULL},
    {NULL, &_v281, (ObjectSetEntry *)&X2AP_TDD_Info_ExtIEs[0]}
};
static ObjectSetEntry const X2AP_ECGI_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RAB_Level_QoS_Parameters_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RAB_ItemIEs[] = {
    {NULL, &_v282, NULL}
};
static ObjectSetEntry const X2AP_E_RAB_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ExpectedUEBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ExpectedUEActivityBehaviour_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ExtendedULInterferenceOverloadInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ForbiddenTAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ForbiddenLAs_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GBR_QosInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GlobalENB_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GTPtunnelEndpoint_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GU_Group_ID_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_GUMMEI_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HandoverRestrictionList_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_HWLoadIndicator_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_LastVisitedEUTRANCellInformation_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_LastVisitedEUTRANCellInformation_ExtIEs[1], &_v283, NULL},
    {NULL, &_v284, (ObjectSetEntry *)&X2AP_LastVisitedEUTRANCellInformation_ExtIEs[0]}
};
static ObjectSetEntry const X2AP_LocationReportingInformation_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M3Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M4Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M5Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_MDT_Configuration_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[1], &_v285, NULL},
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[2], &_v286, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[0]},
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[3], &_v287, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[1]},
    {(ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[4], &_v288, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[2]},
    {NULL, &_v289, (ObjectSetEntry *)&X2AP_MDT_Configuration_ExtIEs[3]}
};
static ObjectSetEntry const X2AP_MBSFN_Subframe_Info_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_BandInfo_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_Neighbour_Information_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_Neighbour_Information_ExtIEs[1], &_v290, NULL},
    {NULL, &_v291, (ObjectSetEntry *)&X2AP_Neighbour_Information_ExtIEs[0]}
};
static ObjectSetEntry const X2AP_M1PeriodicReporting_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_PRACH_Configuration_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ProSeAuthorized_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RadioResourceStatus_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RelativeNarrowbandTxPower_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RSRPMeasurementResult_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_RSRPMRList_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_S1TNLLoadIndicator_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ServedCell_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_ServedCell_Information_ExtIEs[] = {
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[1], &_v292, NULL},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[2], &_v293, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[0]},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[3], &_v294, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[1]},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[4], &_v295, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[2]},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[5], &_v296, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[3]},
    {(ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[6], &_v297, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[4]},
    {NULL, &_v298, (ObjectSetEntry *)&X2AP_ServedCell_Information_ExtIEs[5]}
};
static ObjectSetEntry const X2AP_SpecialSubframe_Info_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TABasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TAIBasedMDT_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TAI_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_M1ThresholdEventA2_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_TraceActivation_ExtIEs[] = {
    {NULL, &_v299, NULL}
};
static ObjectSetEntry const X2AP_UEAggregate_MaximumBitrate_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UESecurityCapabilities_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UL_HighInterferenceIndicationInfo_Item_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UsableABSInformationFDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_UsableABSInformationTDD_ExtIEs[] = {
{0}};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ItemIEs[] = {
    {NULL, &_v76, NULL}
};
static ObjectSetEntry const X2AP_CompleteFailureCauseInformation_ItemIEs[] = {
    {NULL, &_v156, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeModified_ModAckItemIEs[] = {
    {NULL, &_v234, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_SubjectToStatusTransfer_ItemIEs[] = {
    {NULL, &_v93, NULL}
};
static ObjectSetEntry const X2AP_MeasurementInitiationResult_ItemIEs[] = {
    {NULL, &_v149, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeReleased_ModAckItemIEs[] = {
    {NULL, &_v235, NULL}
};
static ObjectSetEntry const X2AP_CellMeasurementResult_ItemIEs[] = {
    {NULL, &_v160, NULL}
};
static ObjectSetEntry const X2AP_CellInformation_ItemIEs[] = {
    {NULL, &_v119, NULL}
};
static ObjectSetEntry const X2AP_MeasurementFailureCause_ItemIEs[] = {
    {NULL, &_v150, NULL}
};
static ObjectSetEntry const X2AP_CellToReport_ItemIEs[] = {
    {NULL, &_v144, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ItemIEs[] = {
    {NULL, &_v207, NULL}
};
static ObjectSetEntry const X2AP_E_RABs_Admitted_ToBeAdded_ModAckItemIEs[] = {
    {NULL, &_v233, NULL}
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
    4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
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

void DLL_ENTRY_FDEF _ossinit_x2ap(struct ossGlobal *world) {
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

static unsigned short const _v337 = 32;
static unsigned short const _v379 = 16;
static unsigned short const _v381 = 27;
static unsigned short const _v389 = 64;
static unsigned short const _v393 = 8;
static unsigned short const _v405 = 3;
static unsigned short const _v409 = 32;
static unsigned short const _v413 = 256;
static unsigned short const _v417 = 16;
static unsigned short const _v419 = 2;
static unsigned short const _v439 = 16;
static unsigned short const _v441 = 16;
static unsigned short const _v448 = 256;
static unsigned short const _v517 = 4;
static unsigned short const _v546 = 4096;
static unsigned short const _v590 = 28;
static unsigned short const _v749 = 20;
static unsigned short const _v751 = 28;
static unsigned short const _v934 = 40;
static unsigned short const _v936 = 40;
static unsigned short const _v950 = 40;
static unsigned short const _v1049 = 8;
static unsigned short const _v1064 = 8;
static unsigned short const _v1078 = 5;
static unsigned short const _v1099 = 2;
static unsigned short const _v1102 = 24;
static unsigned short const _v1106 = 2;
static unsigned short const _v1113 = 1;
static unsigned short const _v1119 = 8;
static unsigned short const _v1136 = 8;
static unsigned short const _v1154 = 2;
static unsigned short const _v1158 = 6;

static unsigned int const _v1291[25] = {
  22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 
  42, 43, 44, 45, INT_MAX
};
static unsigned int const _v1290[22] = {
  0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 
  20, 21
};
static unsigned short const _v1273[4] = {
  0, 1, 14, 15
};
static unsigned int const _v1268[3] = {4, 5, INT_MAX};
static unsigned int const _v1245[3] = {1, 2, INT_MAX};
static unsigned int const _v1328[2] = {2, INT_MAX};
static unsigned short const _v1232[2] = {1, 256};
static unsigned short const _v1231[2] = {1, USHRT_MAX};
static unsigned short const _v1229[2] = {0, USHRT_MAX};
static unsigned short const _v1228[2] = {1, 110};
static unsigned short const _v1221[2] = {1, 160};
static unsigned short const _v1220[2] = {8, 8};
static unsigned short const _v1216[2] = {1, 8};
static unsigned short const _v1212[2] = {3, 3};
static unsigned short const _v1211[2] = {2, 2};
static unsigned short const _v1206[2] = {0, 512};
static unsigned short const _v1202[2] = {1, 9};
static unsigned int const _v1196[2] = {0, 97};
static unsigned short const _v1188[2] = {0, 63};
static unsigned short const _v1186[2] = {0, 94};
static unsigned short const _v1184[2] = {0, 15};
static unsigned short const _v1182[2] = {0, 837};
static unsigned int const _v1175[2] = {0, 503};
static unsigned int const _v1171[2] = {1, 256};
static short const _v1170[2] = {-20, 20};
static unsigned int const _v1161[2] = {0, 7};
static unsigned short const _v1160[2] = {24, 24};
static unsigned short const _v1159[2] = {6, 6};
static unsigned short const _v1142[2] = {0, 34};
static unsigned short const _v1141[2] = {0, 97};
static unsigned short const _v1122[2] = {0, 4095};
static unsigned short const _v1114[2] = {1, 1};
static unsigned short const _v1100[2] = {1, 4096};
static unsigned short const _v1097[2] = {1, 16};
static unsigned short const _v1077[2] = {5, 5};
static unsigned int const _v1070[2] = {1, 181};
static unsigned int const _v1068[2] = {1, 30};
static unsigned int const _v1059[2] = {0, 15};
static unsigned short const _v1057[2] = {1, 15};
static unsigned short const _v1053[2] = {0, 3};
static unsigned short const _v1037[2] = {0, 255};
static unsigned int const _v1033[2] = {0, 131071};
static unsigned short const _v1032[2] = {0, SHRT_MAX};
static unsigned short const _v1021[2] = {0, 100};
static unsigned int const _v1020[2] = {1, 100};
static unsigned int const _v1015[2] = {0, 9};
static unsigned int const _v1013[2] = {0, 1023};
static int const _v1007[2] = {-101, 100};
static unsigned short const _v1006[2] = {1, 32};
static unsigned short const _v993[2] = {6, 4400};
static unsigned short const _v983[2] = {1, 6};
static unsigned short const _v955[2] = {1, 70};
static unsigned short const _v949[2] = {40, 40};
static unsigned int const _v920[2] = {0, UINT_MAX};
static unsigned short const _v798[2] = {256, 256};
static unsigned short const _v750[2] = {28, 28};
static unsigned short const _v748[2] = {20, 20};
static unsigned short const _v665[2] = {32, 32};
static unsigned int const _v601[2] = {0, 4};
static unsigned int const _v599[2] = {0, 3};
static unsigned short const _v597[2] = {6, 110};
static unsigned short const _v557[2] = {4096, 4096};
static unsigned int const _v552[2] = {0, 1048575};
static unsigned short const _v519[2] = {4, 4};
static ULONG_LONG const _v506[2] = {0ui64, 10000000000ui64};
static unsigned short const _v452[2] = {0, 7};
static unsigned short const _v443[2] = {16, 16};
static unsigned short const _v421[2] = {0, 40950U};
static unsigned short const _v411[2] = {1, 128};
static unsigned short const _v407[2] = {1, 16384};
static unsigned int const _v395[2] = {1, 4095};
static unsigned short const _v388[2] = {64, 64};
static unsigned int const _v383[2] = {65536, 262143};
static unsigned short const _v380[2] = {27, 27};
static unsigned int const _v1340[1] = {INT_MAX};

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
    0
};

static const struct etype _etypearray[] = {
    {-1, 0, 0, "_ObjectID", 8, 2, 2, 4, 40, 0, 63, 0},
    {-1, 2, 3, "X2AP-PDU", 28, 3, 2, 4, 12, 0, 13, 0},
    {-1, 12, 14, "HandoverRequest", 4, 1, 0, 0, 12, 3, 12, 0},
    {-1, 12, 18, "UE-ContextInformation", 140, 10, 1, 0, 12, 4, 12, 0},
    {-1, 12, 66, "E-RABs-ToBeSetup-Item", 104, 5, 1, 0, 12, 14, 12, 0},
    {32, 88, 0, "MobilityInformation", 8, 0, 2, 4, 216, 0, 3, 0},
    {-1, 12, 14, "HandoverRequestAcknowledge", 4, 1, 0, 0, 12, 19, 12, 0},
    {256, 12, 0, "E-RABs-Admitted-List", 4, 24, 2, 4, 216, 328, 18, 3},
    {-1, 12, 90, "E-RABs-Admitted-Item", 60, 4, 1, 0, 12, 20, 12, 0},
    {-1, 12, 14, "HandoverPreparationFailure", 4, 1, 0, 0, 12, 24, 12, 0},
    {-1, 12, 14, "HandoverReport", 4, 1, 0, 0, 12, 25, 12, 0},
    {-1, 12, 14, "SNStatusTransfer", 4, 1, 0, 0, 12, 26, 12, 0},
    {256, 12, 0, "E-RABs-SubjectToStatusTransfer-List", 4, 24, 2, 4, 216, 355, 18, 6},
    {-1, 12, 112, "E-RABs-SubjectToStatusTransfer-Item", 44, 5, 1, 0, 12, 27, 12, 0},
    {-1, 12, 14, "UEContextRelease", 4, 1, 0, 0, 12, 32, 12, 0},
    {-1, 12, 14, "HandoverCancel", 4, 1, 0, 0, 12, 33, 12, 0},
    {-1, 12, 14, "ErrorIndication", 4, 1, 0, 0, 12, 34, 12, 0},
    {-1, 12, 14, "ResetRequest", 4, 1, 0, 0, 12, 35, 12, 0},
    {-1, 12, 14, "ResetResponse", 4, 1, 0, 0, 12, 36, 12, 0},
    {-1, 12, 14, "X2SetupRequest", 4, 1, 0, 0, 12, 37, 12, 0},
    {-1, 12, 14, "X2SetupResponse", 4, 1, 0, 0, 12, 38, 12, 0},
    {-1, 12, 14, "X2SetupFailure", 4, 1, 0, 0, 12, 39, 12, 0},
    {-1, 12, 14, "LoadInformation", 4, 1, 0, 0, 12, 40, 12, 0},
    {256, 12, 0, "CellInformation-List", 4, 24, 2, 4, 216, 424, 18, 9},
    {-1, 12, 134, "CellInformation-Item", 68, 5, 1, 0, 12, 41, 12, 0},
    {-1, 12, 14, "ENBConfigurationUpdate", 4, 1, 0, 0, 12, 46, 12, 0},
    {256, 12, 0, "ServedCellsToModify", 4, 112, 2, 4, 216, 515, 18, 12},
    {256, 12, 0, "Old-ECGIs", 4, 20, 2, 4, 216, 113, 18, 15},
    {-1, 12, 14, "ENBConfigurationUpdateAcknowledge", 4, 1, 0, 0, 12, 47, 12, 0},
    {-1, 12, 14, "ENBConfigurationUpdateFailure", 4, 1, 0, 0, 12, 48, 12, 0},
    {-1, 12, 14, "ResourceStatusRequest", 4, 1, 0, 0, 12, 49, 12, 0},
    {256, 12, 0, "CellToReport-List", 4, 24, 2, 4, 216, 534, 18, 18},
    {-1, 12, 166, "CellToReport-Item", 28, 2, 1, 0, 12, 50, 12, 0},
    {INT_MAX, 174, 0, "ReportingPeriodicity", 4, 0, 4, 0, 2076, 1, 58, 0},
    {INT_MAX, 174, 0, "PartialSuccessIndicator", 4, 0, 4, 0, 2076, 10, 58, 0},
    {-1, 12, 14, "ResourceStatusResponse", 4, 1, 0, 0, 12, 52, 12, 0},
    {256, 12, 0, "MeasurementInitiationResult-List", 4, 24, 2, 4, 216, 549, 18, 21},
    {-1, 12, 176, "MeasurementInitiationResult-Item", 32, 3, 1, 0, 12, 53, 12, 0},
    {-1, 12, 190, "MeasurementFailureCause-Item", 24, 3, 1, 0, 12, 56, 12, 0},
    {-1, 12, 14, "ResourceStatusFailure", 4, 1, 0, 0, 12, 59, 12, 0},
    {256, 12, 0, "CompleteFailureCauseInformation-List", 4, 24, 2, 4, 216, 585, 18, 24},
    {-1, 12, 190, "CompleteFailureCauseInformation-Item", 32, 3, 1, 0, 12, 60, 12, 0},
    {-1, 12, 14, "ResourceStatusUpdate", 4, 1, 0, 0, 12, 63, 12, 0},
    {256, 12, 0, "CellMeasurementResult-List", 4, 24, 2, 4, 216, 601, 18, 27},
    {-1, 12, 134, "CellMeasurementResult-Item", 80, 5, 1, 0, 12, 64, 12, 0},
    {-1, 12, 14, "PrivateMessage", 4, 1, 0, 0, 12, 69, 12, 0},
    {-1, 12, 14, "MobilityChangeRequest", 4, 1, 0, 0, 12, 70, 12, 0},
    {-1, 12, 14, "MobilityChangeAcknowledge", 4, 1, 0, 0, 12, 71, 12, 0},
    {-1, 12, 14, "MobilityChangeFailure", 4, 1, 0, 0, 12, 72, 12, 0},
    {-1, 12, 14, "RLFIndication", 4, 1, 0, 0, 12, 73, 12, 0},
    {-1, 12, 14, "CellActivationRequest", 4, 1, 0, 0, 12, 74, 12, 0},
    {256, 12, 0, "ServedCellsToActivate", 4, 28, 2, 4, 216, 685, 18, 30},
    {-1, 12, 14, "CellActivationResponse", 4, 1, 0, 0, 12, 75, 12, 0},
    {256, 12, 0, "ActivatedCellList", 4, 28, 2, 4, 216, 697, 18, 33},
    {-1, 12, 14, "CellActivationFailure", 4, 1, 0, 0, 12, 76, 12, 0},
    {-1, 12, 14, "X2Release", 4, 1, 0, 0, 12, 77, 12, 0},
    {-1, 12, 14, "X2APMessageTransfer", 4, 1, 0, 0, 12, 78, 12, 0},
    {-1, 12, 176, "RNL-Header", 56, 3, 1, 0, 12, 79, 12, 0},
    {-1, 202, 0, "X2AP-Message", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 12, 14, "SeNBAdditionRequest", 4, 1, 0, 0, 12, 82, 12, 0},
    {256, 12, 0, "E-RABs-ToBeAdded-List", 4, 24, 2, 4, 216, 738, 18, 36},
    {-1, 2, 204, "E-RABs-ToBeAdded-Item", 108, 2, 2, 4, 12, 83, 13, 0},
    {-1, 12, 14, "SeNBAdditionRequestAcknowledge", 4, 1, 0, 0, 12, 85, 12, 0},
    {256, 12, 0, "E-RABs-Admitted-ToBeAdded-List", 4, 24, 2, 4, 216, 768, 18, 39},
    {-1, 2, 204, "E-RABs-Admitted-ToBeAdded-Item", 88, 2, 2, 4, 12, 86, 13, 0},
    {-1, 12, 14, "SeNBAdditionRequestReject", 4, 1, 0, 0, 12, 88, 12, 0},
    {-1, 12, 14, "SeNBReconfigurationComplete", 4, 1, 0, 0, 12, 89, 12, 0},
    {-1, 2, 204, "ResponseInformationSeNBReconfComp", 28, 2, 2, 4, 12, 90, 13, 0},
    {-1, 12, 14, "SeNBModificationRequest", 4, 1, 0, 0, 12, 92, 12, 0},
    {-1, 12, 211, "UE-ContextInformationSeNBModReq", 76, 7, 1, 0, 12, 93, 12, 0},
    {-1, 2, 204, "E-RABs-ToBeAdded-ModReqItem", 108, 2, 2, 4, 12, 100, 13, 0},
    {-1, 2, 204, "E-RABs-ToBeModified-ModReqItem", 104, 2, 2, 4, 12, 102, 13, 0},
    {-1, 2, 204, "E-RABs-ToBeReleased-ModReqItem", 64, 2, 2, 4, 12, 104, 13, 0},
    {-1, 12, 14, "SeNBModificationRequestAcknowledge", 4, 1, 0, 0, 12, 106, 12, 0},
    {256, 12, 0, "E-RABs-Admitted-ToBeAdded-ModAckList", 4, 24, 2, 4, 216, 915, 18, 42},
    {-1, 2, 204, "E-RABs-Admitted-ToBeAdded-ModAckItem", 88, 2, 2, 4, 12, 107, 13, 0},
    {256, 12, 0, "E-RABs-Admitted-ToBeModified-ModAckList", 4, 24, 2, 4, 216, 939, 18, 45},
    {-1, 2, 204, "E-RABs-Admitted-ToBeModified-ModAckItem", 40, 2, 2, 4, 12, 109, 13, 0},
    {256, 12, 0, "E-RABs-Admitted-ToBeReleased-ModAckList", 4, 24, 2, 4, 216, 961, 18, 48},
    {-1, 2, 204, "E-RABs-Admitted-ToReleased-ModAckItem", 16, 2, 2, 4, 12, 111, 13, 0},
    {-1, 12, 14, "SeNBModificationRequestReject", 4, 1, 0, 0, 12, 113, 12, 0},
    {-1, 12, 14, "SeNBModificationRequired", 4, 1, 0, 0, 12, 114, 12, 0},
    {256, 12, 0, "E-RABs-ToBeReleased-ModReqd", 4, 24, 2, 4, 216, 991, 18, 51},
    {-1, 12, 190, "E-RABs-ToBeReleased-ModReqdItem", 20, 3, 1, 0, 12, 115, 12, 0},
    {-1, 12, 14, "SeNBModificationConfirm", 4, 1, 0, 0, 12, 118, 12, 0},
    {-1, 12, 14, "SeNBModificationRefuse", 4, 1, 0, 0, 12, 119, 12, 0},
    {-1, 12, 14, "SeNBReleaseRequest", 4, 1, 0, 0, 12, 120, 12, 0},
    {256, 12, 0, "E-RABs-ToBeReleased-List-RelReq", 4, 24, 2, 4, 216, 1017, 18, 54},
    {-1, 2, 204, "E-RABs-ToBeReleased-RelReqItem", 64, 2, 2, 4, 12, 121, 13, 0},
    {-1, 12, 14, "SeNBReleaseRequired", 4, 1, 0, 0, 12, 123, 12, 0},
    {-1, 12, 14, "SeNBReleaseConfirm", 4, 1, 0, 0, 12, 124, 12, 0},
    {256, 12, 0, "E-RABs-ToBeReleased-List-RelConf", 4, 24, 2, 4, 216, 1050, 18, 57},
    {-1, 2, 204, "E-RABs-ToBeReleased-RelConfItem", 64, 2, 2, 4, 12, 125, 13, 0},
    {-1, 12, 14, "SeNBCounterCheckRequest", 4, 1, 0, 0, 12, 127, 12, 0},
    {256, 12, 0, "E-RABs-SubjectToCounterCheck-List", 4, 24, 2, 4, 216, 1078, 18, 60},
    {-1, 12, 281, "E-RABs-SubjectToCounterCheckItem", 20, 4, 1, 0, 12, 128, 12, 0},
    {-1, 12, 14, "X2RemovalRequest", 4, 1, 0, 0, 12, 132, 12, 0},
    {-1, 12, 14, "X2RemovalResponse", 4, 1, 0, 0, 12, 133, 12, 0},
    {-1, 12, 14, "X2RemovalFailure", 4, 1, 0, 0, 12, 134, 12, 0},
    {-1, 2, 3, "ABSInformation", 32, 3, 2, 4, 12, 135, 13, 0},
    {-1, 12, 190, "ABS-Status", 28, 3, 1, 0, 12, 138, 12, 0},
    {-1, 12, 281, "AdditionalSpecialSubframe-Info", 20, 4, 1, 0, 12, 141, 12, 0},
    {-1, 2, 297, "Cause", 8, 4, 2, 4, 12, 145, 13, 0},
    {-1, 12, 190, "CoMPInformation", 16, 3, 1, 0, 12, 149, 12, 0},
    {-1, 12, 190, "CompositeAvailableCapacityGroup", 40, 3, 1, 0, 12, 152, 12, 0},
    {-1, 12, 190, "COUNTValueExtended", 12, 3, 1, 0, 12, 155, 12, 0},
    {-1, 12, 308, "CriticalityDiagnostics", 20, 5, 1, 0, 12, 158, 12, 0},
    {16, 88, 0, "CRNTI", 8, 0, 2, 4, 216, 0, 3, 63},
    {1, 174, 0, "CSGMembershipStatus", 4, 0, 4, 0, 24, 16, 58, 0},
    {27, 88, 0, "CSG-Id", 8, 0, 2, 4, 216, 0, 3, 66},
    {INT_MAX, 174, 0, "DeactivationIndication", 4, 0, 4, 0, 2076, 20, 58, 0},
    {-1, 2, 204, "DynamicDLTransmissionInformation", 28, 2, 2, 4, 12, 163, 13, 0},
    {-1, 348, 0, "EARFCNExtension", 4, 0, 4, 0, 204, 0, 0, 69},
    {-1, 12, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {256, 12, 0, "E-RAB-List", 4, 24, 2, 4, 216, 1291, 18, 72},
    {-1, 12, 190, "E-RAB-Item", 20, 3, 1, 0, 12, 168, 12, 0},
    {-1, 12, 350, "ExpectedUEBehaviour", 32, 3, 1, 0, 12, 171, 12, 0},
    {-1, 12, 190, "ExtendedULInterferenceOverloadInfo", 20, 3, 1, 0, 12, 174, 12, 0},
    {INT_MAX, 174, 0, "FreqBandIndicatorPriority", 4, 0, 4, 0, 2076, 26, 58, 0},
    {-1, 12, 190, "GlobalENB-ID", 24, 3, 1, 0, 12, 177, 12, 0},
    {16, 12, 0, "GUGroupIDList", 4, 16, 2, 4, 216, 1358, 18, 75},
    {-1, 12, 190, "GUMMEI", 28, 3, 1, 0, 12, 180, 12, 0},
    {INT_MAX, 174, 0, "HandoverReportType", 4, 0, 4, 0, 2076, 33, 58, 0},
    {64, 88, 0, "Masked-IMEISV", 8, 0, 2, 4, 216, 0, 3, 78},
    {INT_MAX, 174, 0, "InvokeIndication", 4, 0, 4, 0, 2076, 41, 58, 0},
    {-1, 12, 166, "M3Configuration", 12, 2, 1, 0, 12, 183, 12, 0},
    {-1, 12, 190, "M4Configuration", 16, 3, 1, 0, 12, 185, 12, 0},
    {-1, 12, 190, "M5Configuration", 16, 3, 1, 0, 12, 188, 12, 0},
    {-1, 12, 368, "MDT-Configuration", 68, 7, 1, 0, 12, 191, 12, 0},
    {16, 12, 0, "MDTPLMNList", 4, 8, 2, 4, 216, 141, 18, 81},
    {8, 88, 0, "MDT-Location-Info", 8, 0, 2, 4, 216, 0, 3, 84},
    {-1, 202, 0, "MeNBtoSeNBContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 348, 0, "Measurement-ID", 4, 0, 4, 0, 204, 0, 0, 87},
    {256, 12, 0, "MBMS-Service-Area-Identity-List", 4, 4, 2, 4, 216, 1445, 18, 90},
    {8, 12, 0, "MBSFN-Subframe-Infolist", 4, 28, 2, 4, 216, 1460, 18, 93},
    {INT_MAX, 174, 0, "ManagementBasedMDTallowed", 4, 0, 4, 0, 2076, 49, 58, 0},
    {-1, 12, 166, "MobilityParametersModificationRange", 4, 2, 0, 0, 12, 198, 12, 0},
    {-1, 12, 14, "MobilityParametersInformation", 2, 1, 0, 0, 12, 200, 12, 0},
    {16, 12, 0, "MultibandInfoList", 4, 12, 2, 4, 216, 1470, 18, 96},
    {INT_MAX, 174, 0, "Number-of-Antennaports", 4, 0, 4, 0, 2076, 55, 58, 0},
    {-1, 348, 0, "PCI", 4, 0, 4, 0, 204, 0, 0, 99},
    {3, 202, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 102},
    {-1, 12, 402, "PRACH-Configuration", 16, 6, 1, 0, 12, 201, 12, 0},
    {-1, 12, 350, "ProSeAuthorized", 16, 3, 1, 0, 12, 207, 12, 0},
    {16384, 88, 0, "ReceiveStatusOfULPDCPSDUsExtended", 8, 0, 2, 4, 216, 0, 3, 105},
    {INT_MAX, 174, 0, "Registration-Request", 4, 0, 4, 0, 2076, 63, 58, 0},
    {INT_MAX, 174, 0, "ReportingPeriodicityRSRPMR", 4, 0, 4, 0, 2076, 70, 58, 0},
    {32, 88, 0, "ReportCharacteristics", 8, 0, 2, 4, 216, 0, 3, 108},
    {INT_MAX, 174, 0, "RRCConnReestabIndicator", 4, 0, 4, 0, 2076, 79, 58, 0},
    {INT_MAX, 174, 0, "RRCConnSetupIndicator", 4, 0, 4, 0, 2076, 87, 58, 0},
    {128, 12, 0, "RSRPMRList", 4, 12, 2, 4, 216, 1515, 18, 111},
    {INT_MAX, 174, 0, "SCGChangeIndication", 4, 0, 4, 0, 2076, 93, 58, 0},
    {256, 88, 0, "SeNBSecurityKey", 8, 0, 2, 4, 216, 0, 3, 114},
    {-1, 202, 0, "SeNBtoMeNBContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {256, 12, 0, "ServedCells", 4, 92, 2, 4, 216, 1523, 18, 117},
    {16, 88, 0, "ShortMAC-I", 8, 0, 2, 4, 216, 0, 3, 120},
    {INT_MAX, 174, 0, "SRVCCOperationPossible", 4, 0, 4, 0, 2076, 101, 58, 0},
    {INT_MAX, 174, 0, "SubframeAssignment", 4, 0, 4, 0, 2076, 107, 58, 0},
    {2, 202, 0, "TAC", 2, 0, 2, 2, 216, 0, 21, 123},
    {-1, 202, 0, "TargetCellInUTRAN", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 202, 0, "TargeteNBtoSource-eNBTransparentContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {INT_MAX, 174, 0, "TimeToWait", 4, 0, 4, 0, 2076, 119, 58, 0},
    {-1, 348, 0, "Time-UE-StayedInCell-EnhancedGranularity", 2, 0, 2, 0, 200, 0, 55, 126},
    {-1, 12, 428, "TraceActivation", 36, 5, 1, 0, 12, 210, 12, 0},
    {16, 12, 0, "UE-HistoryInformation", 4, 48, 2, 4, 216, 1384, 18, 128},
    {-1, 202, 0, "UE-HistoryInformationFromTheUE", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 348, 0, "UE-X2AP-ID", 2, 0, 2, 0, 200, 0, 55, 131},
    {-1, 12, 190, "UEAggregateMaximumBitRate", 24, 3, 1, 0, 12, 215, 12, 0},
    {-1, 12, 190, "UESecurityCapabilities", 24, 3, 1, 0, 12, 218, 12, 0},
    {-1, 202, 0, "UE-RLF-Report-Container", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 202, 0, "UE-RLF-Report-Container-for-extended-bands", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 348, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 133},
    {2, 174, 0, "Criticality", 4, 0, 4, 0, 24, 130, 58, 0},
    {-1, 2, 0, NULL, 2, 0, 0, 0, 8, 0, 50, 0},
    {-1, 2, 0, "X2AP-ELEMENTARY-PROCEDURE", 16, 5, 1, 0, 8, 221, 49, 0},
    {-1, 448, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 135},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 138},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 141},
    {-1, 12, 190, "InitiatingMessage", 24, 3, 0, 0, 264, 226, 12, 0},
    {-1, 448, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 144},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 147},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 150},
    {-1, 12, 190, "SuccessfulOutcome", 24, 3, 0, 0, 264, 229, 12, 0},
    {-1, 448, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 153},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 156},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 159},
    {-1, 12, 190, "UnsuccessfulOutcome", 24, 3, 0, 0, 264, 232, 12, 0},
    {-1, 448, 190, "InitiatingMessage", 24, 3, 0, 0, 264, 226, 12, 0},
    {-1, 450, 190, "SuccessfulOutcome", 24, 3, 0, 0, 264, 229, 12, 0},
    {-1, 452, 190, "UnsuccessfulOutcome", 24, 3, 0, 0, 264, 232, 12, 0},
    {-1, 348, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 162},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 164},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 167},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 170},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 235, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 194, 18, 173},
    {-1, 348, 0, "UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 176},
    {16, 88, 0, "EncryptionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 178},
    {16, 88, 0, "IntegrityProtectionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 182},
    {16, 448, 0, "EncryptionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 186},
    {16, 450, 0, "IntegrityProtectionAlgorithms", 8, 0, 2, 4, 220, 0, 3, 188},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 190},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 193},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 196},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 238, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 204, 18, 199},
    {256, 88, 0, "Key-eNodeB-Star", 8, 0, 2, 4, 216, 0, 3, 202},
    {-1, 348, 0, "NextHopChainingCount", 2, 0, 2, 0, 200, 0, 55, 205},
    {256, 448, 0, "Key-eNodeB-Star", 8, 0, 2, 4, 216, 0, 3, 207},
    {-1, 450, 0, "NextHopChainingCount", 2, 0, 2, 0, 200, 0, 55, 209},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 211},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 214},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 217},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 241, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 213, 18, 220},
    {-1, 12, 190, "AS-SecurityInformation", 20, 3, 1, 0, 12, 244, 12, 0},
    {-1, 348, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 223},
    {-1, 448, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 225},
    {-1, 450, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 227},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 229},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 232},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 235},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 247, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 222, 18, 238},
    {-1, 348, 0, "SubscriberProfileIDforRFP", 2, 0, 2, 0, 200, 0, 55, 241},
    {-1, 202, 0, "RRC-Context", 8, 0, 4, 4, 8, 0, 20, 0},
    {INT_MAX, 174, 0, "ForbiddenInterRATs", 4, 0, 4, 0, 2076, 135, 58, 0},
    {3, 448, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 243},
    {15, 450, 0, "EPLMNs", 4, 8, 2, 4, 216, 141, 18, 245},
    {16, 452, 0, "ForbiddenTAs", 4, 16, 2, 4, 216, 1334, 18, 248},
    {16, 454, 0, "ForbiddenLAs", 4, 16, 2, 4, 216, 1344, 18, 251},
    {INT_MAX, 456, 0, "ForbiddenInterRATs", 4, 0, 4, 0, 2076, 135, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 254},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 257},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 260},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 250, 12, 0},
    {65535, 458, 0, NULL, 4, 24, 2, 4, 216, 235, 18, 263},
    {-1, 12, 460, "HandoverRestrictionList", 28, 6, 1, 0, 12, 253, 12, 0},
    {INT_MAX, 174, 0, "EventType", 4, 0, 4, 0, 2076, 146, 58, 0},
    {INT_MAX, 174, 0, "ReportArea", 4, 0, 4, 0, 2076, 152, 58, 0},
    {INT_MAX, 448, 0, "EventType", 4, 0, 4, 0, 2076, 146, 58, 0},
    {INT_MAX, 450, 0, "ReportArea", 4, 0, 4, 0, 2076, 152, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 266},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 269},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 272},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 259, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 245, 18, 275},
    {-1, 12, 190, "LocationReportingInformation", 16, 3, 1, 0, 12, 262, 12, 0},
    {-1, 448, 0, "UE-S1AP-ID", 4, 0, 4, 0, 200, 0, 55, 278},
    {-1, 450, 190, "UESecurityCapabilities", 24, 3, 1, 0, 12, 218, 12, 0},
    {-1, 452, 190, "AS-SecurityInformation", 20, 3, 1, 0, 12, 244, 12, 0},
    {-1, 454, 190, "UEAggregateMaximumBitRate", 24, 3, 1, 0, 12, 215, 12, 0},
    {-1, 456, 0, "SubscriberProfileIDforRFP", 2, 0, 2, 0, 200, 0, 55, 280},
    {256, 458, 0, "E-RABs-ToBeSetup-List", 4, 24, 2, 4, 216, 265, 18, 282},
    {-1, 504, 0, "RRC-Context", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 506, 460, "HandoverRestrictionList", 28, 6, 1, 0, 12, 253, 12, 0},
    {-1, 508, 190, "LocationReportingInformation", 16, 3, 1, 0, 12, 262, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 285},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 288},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 291},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 265, 12, 0},
    {65535, 510, 0, NULL, 4, 24, 2, 4, 216, 260, 18, 294},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 297},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 300},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 303},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 268, 12, 0},
    {256, 12, 0, "E-RABs-ToBeSetup-List", 4, 24, 2, 4, 216, 265, 18, 306},
    {-1, 348, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 308},
    {-1, 348, 0, "QCI", 2, 0, 2, 0, 200, 0, 55, 311},
    {-1, 348, 0, "PriorityLevel", 2, 0, 2, 0, 200, 158, 55, 313},
    {1, 174, 0, "Pre-emptionCapability", 4, 0, 4, 0, 24, 164, 58, 0},
    {1, 174, 0, "Pre-emptionVulnerability", 4, 0, 4, 0, 24, 168, 58, 0},
    {-1, 448, 0, "PriorityLevel", 2, 0, 2, 0, 200, 158, 55, 315},
    {1, 450, 0, "Pre-emptionCapability", 4, 0, 4, 0, 24, 164, 58, 0},
    {1, 452, 0, "Pre-emptionVulnerability", 4, 0, 4, 0, 24, 168, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 317},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 320},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 323},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 271, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 278, 18, 326},
    {-1, 12, 281, "AllocationAndRetentionPriority", 16, 4, 1, 0, 12, 274, 12, 0},
    {-1, 448, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 329},
    {-1, 450, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 331},
    {-1, 452, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 333},
    {-1, 454, 0, "BitRate", 8, 0, 8, 0, 200, 0, 55, 335},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 337},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 340},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 343},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 278, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 288, 18, 346},
    {-1, 12, 428, "GBR-QosInformation", 40, 5, 1, 0, 12, 281, 12, 0},
    {-1, 448, 0, "QCI", 2, 0, 2, 0, 200, 0, 55, 349},
    {-1, 450, 281, "AllocationAndRetentionPriority", 16, 4, 1, 0, 12, 274, 12, 0},
    {-1, 452, 428, "GBR-QosInformation", 40, 5, 1, 0, 12, 281, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 351},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 354},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 357},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 286, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 297, 18, 360},
    {-1, 12, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {INT_MAX, 174, 0, "DL-Forwarding", 4, 0, 4, 0, 2076, 172, 58, 0},
    {160, 88, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 363},
    {4, 202, 0, "GTP-TEI", 2, 0, 2, 2, 216, 0, 21, 367},
    {160, 448, 0, "TransportLayerAddress", 8, 0, 2, 4, 220, 0, 3, 370},
    {4, 450, 0, "GTP-TEI", 2, 0, 2, 2, 216, 0, 21, 372},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 374},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 377},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 380},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 293, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 308, 18, 383},
    {-1, 12, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 386},
    {-1, 450, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {INT_MAX, 452, 0, "DL-Forwarding", 4, 0, 4, 0, 2076, 172, 58, 0},
    {-1, 454, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 388},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 391},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 394},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 299, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 318, 18, 397},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 400},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 403},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 406},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 302, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 323, 18, 409},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 412},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 415},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 418},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 305, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 421},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 423},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 426},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 429},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 308, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 335, 18, 432},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 435},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 438},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 441},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 311, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 340, 18, 444},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 447},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 450},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 453},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 314, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 345, 18, 456},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 459},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 462},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 465},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 317, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 350, 18, 468},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 471},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 474},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 477},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 320, 12, 0},
    {4096, 88, 0, "ReceiveStatusofULPDCPSDUs", 8, 0, 2, 4, 216, 0, 3, 480},
    {-1, 348, 0, "PDCP-SN", 2, 0, 2, 0, 200, 0, 55, 483},
    {-1, 348, 0, "HFN", 4, 0, 4, 0, 200, 0, 55, 485},
    {-1, 448, 0, "PDCP-SN", 2, 0, 2, 0, 200, 0, 55, 487},
    {-1, 450, 0, "HFN", 4, 0, 4, 0, 200, 0, 55, 489},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 491},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 494},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 497},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 323, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 364, 18, 500},
    {-1, 12, 190, "COUNTvalue", 12, 3, 1, 0, 12, 326, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 503},
    {4096, 450, 0, "ReceiveStatusofULPDCPSDUs", 8, 0, 2, 4, 216, 0, 3, 505},
    {-1, 452, 190, "COUNTvalue", 12, 3, 1, 0, 12, 326, 12, 0},
    {-1, 454, 190, "COUNTvalue", 12, 3, 1, 0, 12, 326, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 507},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 510},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 513},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 329, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 374, 18, 516},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 519},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 522},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 525},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 332, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 379, 18, 528},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 531},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 534},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 537},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 335, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 384, 18, 540},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 543},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 546},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 549},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 338, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 389, 18, 552},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 555},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 558},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 561},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 341, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 394, 18, 564},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 567},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 570},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 573},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 344, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 399, 18, 576},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 579},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 582},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 585},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 347, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 404, 18, 588},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 591},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 594},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 597},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 350, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 409, 18, 600},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 603},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 606},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 609},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 353, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 414, 18, 612},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 615},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 618},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 621},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 356, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 419, 18, 624},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 627},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 630},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 633},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 359, 12, 0},
    {28, 88, 0, "EUTRANCellIdentifier", 8, 0, 2, 4, 216, 0, 3, 636},
    {3, 448, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 639},
    {28, 450, 0, "EUTRANCellIdentifier", 8, 0, 2, 4, 216, 0, 3, 641},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 643},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 646},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 649},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 362, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 431, 18, 652},
    {INT_MAX, 174, 0, "RNTP-Threshold", 4, 0, 4, 0, 2076, 178, 58, 0},
    {110, 448, 0, "_bit1", 8, 0, 2, 4, 252, 0, 3, 655},
    {INT_MAX, 450, 0, "RNTP-Threshold", 4, 0, 4, 0, 2076, 178, 58, 0},
    {INT_MAX, 452, 0, "_enum1", 4, 0, 4, 0, 2108, 199, 58, 0},
    {-1, 454, 0, NULL, 4, 0, 4, 0, 204, 0, 0, 659},
    {-1, 456, 0, NULL, 4, 0, 4, 0, 204, 0, 0, 662},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 665},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 668},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 671},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 365, 12, 0},
    {65535, 458, 0, NULL, 4, 24, 2, 4, 216, 442, 18, 674},
    {-1, 12, 530, "RelativeNarrowbandTxPower", 32, 6, 1, 0, 12, 368, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {110, 450, 0, "UL-InterferenceOverloadIndication", 4, 4, 2, 4, 216, 1545, 18, 677},
    {256, 452, 0, "UL-HighInterferenceIndicationInfo", 4, 36, 2, 4, 216, 1555, 18, 680},
    {-1, 454, 530, "RelativeNarrowbandTxPower", 32, 6, 1, 0, 12, 368, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 683},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 686},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 689},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 374, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 452, 18, 692},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 695},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 698},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 701},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 377, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 457, 18, 704},
    {-1, 348, 0, "EARFCN", 2, 0, 2, 0, 200, 0, 55, 707},
    {INT_MAX, 174, 0, "Transmission-Bandwidth", 4, 0, 4, 0, 2076, 207, 58, 0},
    {-1, 448, 0, "EARFCN", 2, 0, 2, 0, 200, 0, 55, 709},
    {-1, 450, 0, "EARFCN", 2, 0, 2, 0, 200, 0, 55, 711},
    {INT_MAX, 452, 0, "Transmission-Bandwidth", 4, 0, 4, 0, 2076, 207, 58, 0},
    {INT_MAX, 454, 0, "Transmission-Bandwidth", 4, 0, 4, 0, 2076, 207, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 713},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 716},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 719},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 380, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 468, 18, 722},
    {-1, 12, 428, "FDD-Info", 20, 5, 1, 0, 12, 383, 12, 0},
    {INT_MAX, 174, 0, "SpecialSubframePatterns", 4, 0, 4, 0, 2076, 218, 58, 0},
    {INT_MAX, 174, 0, "CyclicPrefixDL", 4, 0, 4, 0, 2076, 232, 58, 0},
    {INT_MAX, 174, 0, "CyclicPrefixUL", 4, 0, 4, 0, 2076, 232, 58, 0},
    {INT_MAX, 448, 0, "SpecialSubframePatterns", 4, 0, 4, 0, 2076, 218, 58, 0},
    {INT_MAX, 450, 0, "CyclicPrefixDL", 4, 0, 4, 0, 2076, 232, 58, 0},
    {INT_MAX, 452, 0, "CyclicPrefixUL", 4, 0, 4, 0, 2076, 232, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 725},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 728},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 731},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 388, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 480, 18, 734},
    {-1, 12, 281, "SpecialSubframe-Info", 20, 4, 1, 0, 12, 391, 12, 0},
    {-1, 448, 0, "EARFCN", 2, 0, 2, 0, 200, 0, 55, 737},
    {INT_MAX, 450, 0, "Transmission-Bandwidth", 4, 0, 4, 0, 2076, 207, 58, 0},
    {INT_MAX, 452, 0, "SubframeAssignment", 4, 0, 4, 0, 2076, 107, 58, 0},
    {-1, 454, 281, "SpecialSubframe-Info", 20, 4, 1, 0, 12, 391, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 739},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 742},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 745},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 395, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 490, 18, 748},
    {-1, 12, 428, "TDD-Info", 36, 5, 1, 0, 12, 398, 12, 0},
    {-1, 448, 428, "FDD-Info", 20, 5, 1, 0, 12, 383, 12, 0},
    {-1, 450, 428, "TDD-Info", 36, 5, 1, 0, 12, 398, 12, 0},
    {-1, 2, 204, "EUTRA-Mode-Info", 40, 2, 2, 4, 12, 403, 13, 0},
    {-1, 448, 0, "PCI", 4, 0, 4, 0, 204, 0, 0, 751},
    {-1, 450, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {2, 452, 0, "TAC", 2, 0, 2, 2, 216, 0, 21, 753},
    {6, 454, 0, "BroadcastPLMNs-Item", 4, 8, 2, 4, 216, 141, 18, 755},
    {-1, 456, 204, "EUTRA-Mode-Info", 40, 2, 2, 4, 12, 403, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 758},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 761},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 764},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 405, 12, 0},
    {65535, 458, 0, NULL, 4, 24, 2, 4, 216, 504, 18, 767},
    {-1, 12, 530, "ServedCell-Information", 80, 6, 1, 0, 12, 408, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 450, 530, "ServedCell-Information", 80, 6, 1, 0, 12, 408, 12, 0},
    {512, 452, 0, "Neighbour-Information", 4, 36, 2, 4, 216, 1479, 18, 770},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 773},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 776},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 779},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 414, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 513, 18, 782},
    {-1, 12, 512, "ServedCellsToModify-Item", 112, 4, 1, 0, 12, 417, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 785},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 788},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 791},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 421, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 519, 18, 794},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 797},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 800},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 803},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 424, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 524, 18, 806},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 809},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 812},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 815},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 427, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 529, 18, 818},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 821},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 824},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 827},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 430, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 830},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 833},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 836},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 433, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 539, 18, 839},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 842},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 845},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 848},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 436, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 544, 18, 851},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 854},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 857},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 860},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 439, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {32, 450, 0, "MeasurementFailureCause-List", 4, 24, 2, 4, 216, 560, 18, 863},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 866},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 869},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 872},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 442, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 555, 18, 875},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 878},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 881},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 884},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 445, 12, 0},
    {32, 12, 0, "MeasurementFailureCause-List", 4, 24, 2, 4, 216, 560, 18, 887},
    {INT_MAX, 174, 0, "CauseRadioNetwork", 4, 0, 4, 0, 2076, 239, 58, 0},
    {INT_MAX, 174, 0, "CauseTransport", 4, 0, 4, 0, 2076, 290, 58, 0},
    {INT_MAX, 174, 0, "CauseProtocol", 4, 0, 4, 0, 2076, 297, 58, 0},
    {INT_MAX, 174, 0, "CauseMisc", 4, 0, 4, 0, 2076, 309, 58, 0},
    {INT_MAX, 448, 0, "CauseRadioNetwork", 4, 0, 4, 0, 2076, 239, 58, 0},
    {INT_MAX, 450, 0, "CauseTransport", 4, 0, 4, 0, 2076, 290, 58, 0},
    {INT_MAX, 452, 0, "CauseProtocol", 4, 0, 4, 0, 2076, 297, 58, 0},
    {INT_MAX, 454, 0, "CauseMisc", 4, 0, 4, 0, 2076, 309, 58, 0},
    {32, 448, 0, "ReportCharacteristics", 8, 0, 2, 4, 216, 0, 3, 889},
    {-1, 450, 297, "Cause", 8, 4, 2, 4, 12, 145, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 891},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 894},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 897},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 448, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 575, 18, 900},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 903},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 906},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 909},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 451, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 580, 18, 912},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 915},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 918},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 921},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 454, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {32, 450, 0, "MeasurementFailureCause-List", 4, 24, 2, 4, 216, 560, 18, 924},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 926},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 929},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 932},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 457, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 591, 18, 935},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 938},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 941},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 944},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 460, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 596, 18, 947},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 950},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 953},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 956},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 463, 12, 0},
    {INT_MAX, 174, 0, "LoadIndicator", 4, 0, 4, 0, 2076, 319, 58, 0},
    {INT_MAX, 448, 0, "LoadIndicator", 4, 0, 4, 0, 2076, 319, 58, 0},
    {INT_MAX, 450, 0, "LoadIndicator", 4, 0, 4, 0, 2076, 319, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 959},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 962},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 965},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 466, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 608, 18, 968},
    {-1, 12, 190, "HWLoadIndicator", 16, 3, 1, 0, 12, 469, 12, 0},
    {INT_MAX, 448, 0, "LoadIndicator", 4, 0, 4, 0, 2076, 319, 58, 0},
    {INT_MAX, 450, 0, "LoadIndicator", 4, 0, 4, 0, 2076, 319, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 971},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 974},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 977},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 472, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 616, 18, 980},
    {-1, 12, 190, "S1TNLLoadIndicator", 16, 3, 1, 0, 12, 475, 12, 0},
    {-1, 348, 0, "DL-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 983},
    {-1, 348, 0, "UL-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 985},
    {-1, 348, 0, "DL-non-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 987},
    {-1, 348, 0, "UL-non-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 989},
    {-1, 348, 0, "DL-Total-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 991},
    {-1, 348, 0, "UL-Total-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 993},
    {-1, 448, 0, "DL-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 995},
    {-1, 450, 0, "UL-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 997},
    {-1, 452, 0, "DL-non-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 999},
    {-1, 454, 0, "UL-non-GBR-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 1001},
    {-1, 456, 0, "DL-Total-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 1003},
    {-1, 458, 0, "UL-Total-PRB-usage", 2, 0, 2, 0, 200, 0, 55, 1005},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1007},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1010},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1013},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 478, 12, 0},
    {65535, 504, 0, NULL, 4, 24, 2, 4, 216, 634, 18, 1016},
    {-1, 12, 554, "RadioResourceStatus", 20, 7, 1, 0, 12, 481, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 450, 190, "HWLoadIndicator", 16, 3, 1, 0, 12, 469, 12, 0},
    {-1, 452, 190, "S1TNLLoadIndicator", 16, 3, 1, 0, 12, 475, 12, 0},
    {-1, 454, 554, "RadioResourceStatus", 20, 7, 1, 0, 12, 481, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1019},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1022},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1025},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 488, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 644, 18, 1028},
    {-1, 448, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 1031},
    {-1, 450, 0, "_ObjectID", 8, 2, 2, 4, 40, 0, 63, 0},
    {-1, 2, 582, "PrivateIE-ID", 12, 2, 2, 4, 8, 491, 13, 0},
    {-1, 448, 582, "PrivateIE-ID", 12, 2, 2, 4, 72, 491, 13, 1033},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1034},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1037},
    {-1, 12, 190, "PrivateIE-Field", 32, 3, 0, 0, 296, 493, 12, 0},
    {65535, 448, 0, NULL, 4, 32, 2, 4, 216, 652, 18, 1040},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1043},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1046},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1049},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 496, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 657, 18, 1052},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1055},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1058},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1061},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 499, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 662, 18, 1064},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1067},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1070},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1073},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 502, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 667, 18, 1076},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1079},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1082},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1085},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 505, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 672, 18, 1088},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1091},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1094},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1097},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 508, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 677, 18, 1100},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1103},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1106},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1109},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 511, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 683, 18, 1112},
    {-1, 12, 166, "ServedCellsToActivate-Item", 28, 2, 1, 0, 12, 514, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1115},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1118},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1121},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 516, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 689, 18, 1124},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1127},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1130},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1133},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 519, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 695, 18, 1136},
    {-1, 12, 166, "ActivatedCellList-Item", 28, 2, 1, 0, 12, 522, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1139},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1142},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1145},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 524, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 701, 18, 1148},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1151},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1154},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1157},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 527, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 706, 18, 1160},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1163},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1166},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1169},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 530, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 711, 18, 1172},
    {20, 448, 0, "", 8, 0, 2, 4, 248, 0, 3, 1175},
    {28, 450, 0, "", 8, 0, 2, 4, 248, 0, 3, 1178},
    {-1, 2, 204, "ENB-ID", 12, 2, 2, 4, 12, 533, 13, 0},
    {3, 448, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 1181},
    {-1, 450, 204, "ENB-ID", 12, 2, 2, 4, 12, 533, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1183},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1186},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1189},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 535, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 721, 18, 1192},
    {-1, 448, 190, "GlobalENB-ID", 24, 3, 1, 0, 12, 177, 12, 0},
    {-1, 450, 190, "GlobalENB-ID", 24, 3, 1, 0, 12, 177, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1195},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1198},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1201},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 538, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 728, 18, 1204},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1207},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1210},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1213},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 541, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 733, 18, 1216},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1219},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1222},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1225},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 544, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1228},
    {-1, 450, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {INT_MAX, 452, 0, "DL-Forwarding", 4, 0, 4, 0, 2076, 172, 58, 0},
    {-1, 454, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1230},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1233},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1236},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 547, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 746, 18, 1239},
    {-1, 12, 66, "E-RABs-ToBeAdded-Item-SCG-Bearer", 104, 5, 1, 0, 12, 550, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1242},
    {-1, 450, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1244},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1247},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1250},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 555, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 755, 18, 1253},
    {-1, 12, 281, "E-RABs-ToBeAdded-Item-Split-Bearer", 100, 4, 1, 0, 12, 558, 12, 0},
    {-1, 448, 66, "E-RABs-ToBeAdded-Item-SCG-Bearer", 104, 5, 1, 0, 12, 550, 12, 0},
    {-1, 450, 281, "E-RABs-ToBeAdded-Item-Split-Bearer", 100, 4, 1, 0, 12, 558, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1256},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1259},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1262},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 562, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 763, 18, 1265},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1268},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1271},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1274},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 565, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1277},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 454, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1279},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1282},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1285},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 568, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 776, 18, 1288},
    {-1, 12, 587, "E-RABs-Admitted-ToBeAdded-Item-SCG-Bearer", 84, 5, 1, 0, 12, 571, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1291},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1293},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1296},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1299},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 576, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 784, 18, 1302},
    {-1, 12, 190, "E-RABs-Admitted-ToBeAdded-Item-Split-Bearer", 36, 3, 1, 0, 12, 579, 12, 0},
    {-1, 448, 587, "E-RABs-Admitted-ToBeAdded-Item-SCG-Bearer", 84, 5, 1, 0, 12, 571, 12, 0},
    {-1, 450, 190, "E-RABs-Admitted-ToBeAdded-Item-Split-Bearer", 36, 3, 1, 0, 12, 579, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1305},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1308},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1311},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 582, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 792, 18, 1314},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1317},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1320},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1323},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 585, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 797, 18, 1326},
    {-1, 448, 0, "MeNBtoSeNBContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1329},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1332},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1335},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 588, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 803, 18, 1338},
    {-1, 12, 613, "ResponseInformationSeNBReconfComp-SuccessItem", 16, 2, 1, 0, 12, 591, 12, 0},
    {-1, 448, 297, "Cause", 8, 4, 2, 4, 12, 145, 13, 0},
    {-1, 450, 0, "MeNBtoSeNBContainer", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1341},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1344},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1347},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 593, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 811, 18, 1350},
    {-1, 12, 176, "ResponseInformationSeNBReconfComp-RejectByMeNBItem", 24, 3, 1, 0, 12, 596, 12, 0},
    {-1, 448, 613, "ResponseInformationSeNBReconfComp-SuccessItem", 16, 2, 1, 0, 12, 591, 12, 0},
    {-1, 450, 176, "ResponseInformationSeNBReconfComp-RejectByMeNBItem", 24, 3, 1, 0, 12, 596, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1353},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1356},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1359},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 599, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 819, 18, 1362},
    {-1, 448, 190, "UESecurityCapabilities", 24, 3, 1, 0, 12, 218, 12, 0},
    {256, 450, 0, "SeNBSecurityKey", 8, 0, 2, 4, 216, 0, 3, 1365},
    {-1, 452, 190, "UEAggregateMaximumBitRate", 24, 3, 1, 0, 12, 215, 12, 0},
    {256, 454, 0, "E-RABs-ToBeAdded-List-ModReq", 4, 24, 2, 4, 216, 835, 18, 1367},
    {256, 456, 0, "E-RABs-ToBeModified-List-ModReq", 4, 24, 2, 4, 216, 861, 18, 1370},
    {256, 458, 0, "E-RABs-ToBeReleased-List-ModReq", 4, 24, 2, 4, 216, 886, 18, 1373},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1376},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1379},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1382},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 602, 12, 0},
    {65535, 504, 0, NULL, 4, 24, 2, 4, 216, 830, 18, 1385},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1388},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1391},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1394},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 605, 12, 0},
    {256, 12, 0, "E-RABs-ToBeAdded-List-ModReq", 4, 24, 2, 4, 216, 835, 18, 1397},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1399},
    {-1, 450, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {INT_MAX, 452, 0, "DL-Forwarding", 4, 0, 4, 0, 2076, 172, 58, 0},
    {-1, 454, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1401},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1404},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1407},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 608, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 844, 18, 1410},
    {-1, 12, 66, "E-RABs-ToBeAdded-ModReqItem-SCG-Bearer", 104, 5, 1, 0, 12, 611, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1413},
    {-1, 450, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1415},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1418},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1421},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 616, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 853, 18, 1424},
    {-1, 12, 281, "E-RABs-ToBeAdded-ModReqItem-Split-Bearer", 100, 4, 1, 0, 12, 619, 12, 0},
    {-1, 448, 66, "E-RABs-ToBeAdded-ModReqItem-SCG-Bearer", 104, 5, 1, 0, 12, 611, 12, 0},
    {-1, 450, 281, "E-RABs-ToBeAdded-ModReqItem-Split-Bearer", 100, 4, 1, 0, 12, 619, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1427},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1430},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1433},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 623, 12, 0},
    {256, 12, 0, "E-RABs-ToBeModified-List-ModReq", 4, 24, 2, 4, 216, 861, 18, 1436},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1438},
    {-1, 450, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1440},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1443},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1446},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 626, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 869, 18, 1449},
    {-1, 12, 90, "E-RABs-ToBeModified-ModReqItem-SCG-Bearer", 100, 4, 1, 0, 12, 629, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1452},
    {-1, 450, 512, "E-RAB-Level-QoS-Parameters", 64, 4, 1, 0, 12, 289, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1454},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1457},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1460},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 633, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 878, 18, 1463},
    {-1, 12, 90, "E-RABs-ToBeModified-ModReqItem-Split-Bearer", 100, 4, 1, 0, 12, 636, 12, 0},
    {-1, 448, 90, "E-RABs-ToBeModified-ModReqItem-SCG-Bearer", 100, 4, 1, 0, 12, 629, 12, 0},
    {-1, 450, 90, "E-RABs-ToBeModified-ModReqItem-Split-Bearer", 100, 4, 1, 0, 12, 636, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1466},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1469},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1472},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 640, 12, 0},
    {256, 12, 0, "E-RABs-ToBeReleased-List-ModReq", 4, 24, 2, 4, 216, 886, 18, 1475},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1477},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1479},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1482},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1485},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 643, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 894, 18, 1488},
    {-1, 12, 90, "E-RABs-ToBeReleased-ModReqItem-SCG-Bearer", 60, 4, 1, 0, 12, 646, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1491},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1493},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1496},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1499},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 650, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 902, 18, 1502},
    {-1, 12, 176, "E-RABs-ToBeReleased-ModReqItem-Split-Bearer", 36, 3, 1, 0, 12, 653, 12, 0},
    {-1, 448, 90, "E-RABs-ToBeReleased-ModReqItem-SCG-Bearer", 60, 4, 1, 0, 12, 646, 12, 0},
    {-1, 450, 176, "E-RABs-ToBeReleased-ModReqItem-Split-Bearer", 36, 3, 1, 0, 12, 653, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1505},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1508},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1511},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 656, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 910, 18, 1514},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1517},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1520},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1523},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 659, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1526},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 454, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1528},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1531},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1534},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 662, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 923, 18, 1537},
    {-1, 12, 587, "E-RABs-Admitted-ToBeAdded-ModAckItem-SCG-Bearer", 84, 5, 1, 0, 12, 665, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1540},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1542},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1545},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1548},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 670, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 931, 18, 1551},
    {-1, 12, 190, "E-RABs-Admitted-ToBeAdded-ModAckItem-Split-Bearer", 36, 3, 1, 0, 12, 673, 12, 0},
    {-1, 448, 587, "E-RABs-Admitted-ToBeAdded-ModAckItem-SCG-Bearer", 84, 5, 1, 0, 12, 665, 12, 0},
    {-1, 450, 190, "E-RABs-Admitted-ToBeAdded-ModAckItem-Split-Bearer", 36, 3, 1, 0, 12, 673, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1554},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1557},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1560},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 676, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1563},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1565},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1568},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1571},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 679, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 945, 18, 1574},
    {-1, 12, 176, "E-RABs-Admitted-ToBeModified-ModAckItem-SCG-Bearer", 36, 3, 1, 0, 12, 682, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1577},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1579},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1582},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1585},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 685, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 953, 18, 1588},
    {-1, 12, 176, "E-RABs-Admitted-ToBeModified-ModAckItem-Split-Bearer", 36, 3, 1, 0, 12, 688, 12, 0},
    {-1, 448, 176, "E-RABs-Admitted-ToBeModified-ModAckItem-SCG-Bearer", 36, 3, 1, 0, 12, 682, 12, 0},
    {-1, 450, 176, "E-RABs-Admitted-ToBeModified-ModAckItem-Split-Bearer", 36, 3, 1, 0, 12, 688, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1591},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1594},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1597},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 691, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1600},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1602},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1605},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1608},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 694, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 966, 18, 1611},
    {-1, 12, 166, "E-RABs-Admitted-ToBeReleased-ModAckItem-SCG-Bearer", 12, 2, 1, 0, 12, 697, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1614},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1616},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1619},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1622},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 699, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 973, 18, 1625},
    {-1, 12, 166, "E-RABs-Admitted-ToBeReleased-ModAckItem-Split-Bearer", 12, 2, 1, 0, 12, 702, 12, 0},
    {-1, 448, 166, "E-RABs-Admitted-ToBeReleased-ModAckItem-SCG-Bearer", 12, 2, 1, 0, 12, 697, 12, 0},
    {-1, 450, 166, "E-RABs-Admitted-ToBeReleased-ModAckItem-Split-Bearer", 12, 2, 1, 0, 12, 702, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1628},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1631},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1634},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 704, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 981, 18, 1637},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1640},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1643},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1646},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 707, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 986, 18, 1649},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1652},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1655},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1658},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 710, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1661},
    {-1, 450, 297, "Cause", 8, 4, 2, 4, 12, 145, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1663},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1666},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1669},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 713, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 997, 18, 1672},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1675},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1678},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1681},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 716, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1002, 18, 1684},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1687},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1690},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1693},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 719, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1007, 18, 1696},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1699},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1702},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1705},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 722, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1012, 18, 1708},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1711},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1714},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1717},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 725, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1720},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1722},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1725},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1728},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 728, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1024, 18, 1731},
    {-1, 12, 90, "E-RABs-ToBeReleased-RelReqItem-SCG-Bearer", 60, 4, 1, 0, 12, 731, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1734},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1736},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1739},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1742},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 735, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1032, 18, 1745},
    {-1, 12, 176, "E-RABs-ToBeReleased-RelReqItem-Split-Bearer", 36, 3, 1, 0, 12, 738, 12, 0},
    {-1, 448, 90, "E-RABs-ToBeReleased-RelReqItem-SCG-Bearer", 60, 4, 1, 0, 12, 731, 12, 0},
    {-1, 450, 176, "E-RABs-ToBeReleased-RelReqItem-Split-Bearer", 36, 3, 1, 0, 12, 738, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1748},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1751},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1754},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 741, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1040, 18, 1757},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1760},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1763},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1766},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 744, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1045, 18, 1769},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1772},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1775},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1778},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 747, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1781},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 452, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1783},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1786},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1789},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 750, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1057, 18, 1792},
    {-1, 12, 90, "E-RABs-ToBeReleased-RelConfItem-SCG-Bearer", 60, 4, 1, 0, 12, 753, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1795},
    {-1, 450, 190, "GTPtunnelEndpoint", 24, 3, 1, 0, 12, 296, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1797},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1800},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1803},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 757, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1065, 18, 1806},
    {-1, 12, 176, "E-RABs-ToBeReleased-RelConfItem-Split-Bearer", 36, 3, 1, 0, 12, 760, 12, 0},
    {-1, 448, 90, "E-RABs-ToBeReleased-RelConfItem-SCG-Bearer", 60, 4, 1, 0, 12, 753, 12, 0},
    {-1, 450, 176, "E-RABs-ToBeReleased-RelConfItem-Split-Bearer", 36, 3, 1, 0, 12, 760, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1809},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1812},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1815},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 763, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1073, 18, 1818},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1821},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1824},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1827},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 766, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 1830},
    {-1, 450, 0, NULL, 4, 0, 4, 0, 200, 0, 55, 1832},
    {-1, 452, 0, NULL, 4, 0, 4, 0, 200, 0, 55, 1834},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1836},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1839},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1842},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 769, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1085, 18, 1845},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1848},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1851},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1854},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 772, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1090, 18, 1857},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1860},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1863},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1866},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 775, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1095, 18, 1869},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1872},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1875},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1878},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 778, 12, 0},
    {65535, 448, 0, NULL, 4, 24, 2, 4, 216, 1100, 18, 1881},
    {40, 448, 0, NULL, 8, 0, 2, 4, 216, 0, 3, 1884},
    {INT_MAX, 450, 0, NULL, 4, 0, 4, 0, 2076, 199, 58, 0},
    {40, 452, 0, NULL, 8, 0, 2, 4, 216, 0, 3, 1887},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1890},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1893},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1896},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 781, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1108, 18, 1899},
    {-1, 12, 281, "ABSInformationFDD", 28, 4, 1, 0, 12, 784, 12, 0},
    {70, 448, 0, NULL, 8, 0, 2, 4, 220, 0, 3, 1902},
    {INT_MAX, 450, 0, NULL, 4, 0, 4, 0, 2076, 199, 58, 0},
    {70, 452, 0, NULL, 8, 0, 2, 4, 220, 0, 3, 1906},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1910},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1913},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1916},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 788, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1117, 18, 1919},
    {-1, 12, 281, "ABSInformationTDD", 28, 4, 1, 0, 12, 791, 12, 0},
    {-1, 448, 281, "ABSInformationFDD", 28, 4, 1, 0, 12, 784, 12, 0},
    {-1, 450, 281, "ABSInformationTDD", 28, 4, 1, 0, 12, 791, 12, 0},
    {-1, 452, 0, NULL, 1, 0, 0, 0, 8, 0, 5, 0},
    {-1, 348, 0, "DL-ABS-status", 2, 0, 2, 0, 200, 0, 55, 1922},
    {40, 448, 0, NULL, 8, 0, 2, 4, 216, 0, 3, 1924},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1927},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1930},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1933},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 795, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1128, 18, 1936},
    {-1, 12, 166, "UsableABSInformationFDD", 16, 2, 1, 0, 12, 798, 12, 0},
    {70, 448, 0, NULL, 8, 0, 2, 4, 220, 0, 3, 1939},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1943},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1946},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1949},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 800, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1135, 18, 1952},
    {-1, 12, 166, "UsableABSInformationTDD", 16, 2, 1, 0, 12, 803, 12, 0},
    {-1, 448, 166, "UsableABSInformationFDD", 16, 2, 1, 0, 12, 798, 12, 0},
    {-1, 450, 166, "UsableABSInformationTDD", 16, 2, 1, 0, 12, 803, 12, 0},
    {-1, 2, 204, "UsableABSInformation", 20, 2, 2, 4, 12, 805, 13, 0},
    {-1, 448, 0, "DL-ABS-status", 2, 0, 2, 0, 200, 0, 55, 1955},
    {-1, 450, 204, "UsableABSInformation", 20, 2, 2, 4, 12, 805, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1957},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1960},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1963},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 807, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1146, 18, 1966},
    {INT_MAX, 174, 0, "AdditionalSpecialSubframePatterns", 4, 0, 4, 0, 2076, 328, 58, 0},
    {INT_MAX, 448, 0, "AdditionalSpecialSubframePatterns", 4, 0, 4, 0, 2076, 328, 58, 0},
    {INT_MAX, 450, 0, "CyclicPrefixDL", 4, 0, 4, 0, 2076, 232, 58, 0},
    {INT_MAX, 452, 0, "CyclicPrefixUL", 4, 0, 4, 0, 2076, 232, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1969},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1972},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1975},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 810, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1155, 18, 1978},
    {32, 448, 0, "CellIdListforMDT", 4, 20, 2, 4, 216, 113, 18, 1981},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1984},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 1987},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 1990},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 813, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1161, 18, 1993},
    {-1, 12, 166, "CellBasedMDT", 12, 2, 1, 0, 12, 816, 12, 0},
    {8, 448, 0, "TAListforMDT", 4, 4, 2, 4, 216, 158, 18, 1996},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 1999},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2002},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2005},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 818, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1168, 18, 2008},
    {-1, 12, 166, "TABasedMDT", 12, 2, 1, 0, 12, 821, 12, 0},
    {8, 448, 0, "TAIListforMDT", 4, 16, 2, 4, 216, 1532, 18, 2011},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2014},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2017},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2020},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 823, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1175, 18, 2023},
    {-1, 12, 166, "TAIBasedMDT", 12, 2, 1, 0, 12, 826, 12, 0},
    {-1, 448, 166, "CellBasedMDT", 12, 2, 1, 0, 12, 816, 12, 0},
    {-1, 450, 166, "TABasedMDT", 12, 2, 1, 0, 12, 821, 12, 0},
    {-1, 452, 0, NULL, 1, 0, 0, 0, 8, 0, 5, 0},
    {-1, 454, 166, "TAIBasedMDT", 12, 2, 1, 0, 12, 826, 12, 0},
    {1, 2, 297, "AreaScopeOfMDT", 16, 4, 2, 4, 2076, 828, 13, 0},
    {-1, 348, 0, "BenefitMetric", 4, 0, 4, 0, 204, 0, 0, 2026},
    {6, 12, 0, "BroadcastPLMNs-Item", 4, 8, 2, 4, 216, 141, 18, 2029},
    {-1, 348, 0, "CapacityValue", 2, 0, 2, 0, 200, 0, 55, 2031},
    {-1, 348, 0, "CellCapacityClassValue", 4, 0, 4, 0, 204, 0, 0, 2033},
    {32, 12, 0, "CellIdListforMDT", 4, 20, 2, 4, 216, 113, 18, 2036},
    {INT_MAX, 174, 0, "Cell-Size", 4, 0, 4, 0, 2076, 343, 58, 0},
    {INT_MAX, 448, 0, "Cell-Size", 4, 0, 4, 0, 2076, 343, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2038},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2041},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2044},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 832, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1193, 18, 2047},
    {-1, 12, 166, "CellType", 12, 2, 1, 0, 12, 835, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {4400, 450, 0, NULL, 8, 0, 2, 4, 220, 0, 3, 2050},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2054},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2057},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2060},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 837, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1201, 18, 2063},
    {-1, 12, 190, "CoMPHypothesisSetItem", 36, 3, 1, 0, 12, 840, 12, 0},
    {32, 12, 0, "CoMPHypothesisSet", 4, 36, 2, 4, 216, 1203, 18, 2066},
    {256, 448, 0, "CoMPInformationItem", 4, 16, 2, 4, 216, 1219, 18, 2069},
    {1, 450, 0, "CoMPInformationStartTime", 4, 16, 2, 4, 216, 1228, 18, 2072},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2075},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2078},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2081},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 843, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1210, 18, 2084},
    {32, 448, 0, "CoMPHypothesisSet", 4, 36, 2, 4, 216, 1203, 18, 2087},
    {-1, 450, 0, "BenefitMetric", 4, 0, 4, 0, 204, 0, 0, 2089},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2091},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2094},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2097},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 846, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1217, 18, 2100},
    {-1, 12, 190, NULL, 16, 3, 1, 0, 12, 849, 12, 0},
    {256, 12, 0, "CoMPInformationItem", 4, 16, 2, 4, 216, 1219, 18, 2103},
    {-1, 448, 0, NULL, 4, 0, 4, 0, 204, 0, 0, 2105},
    {-1, 450, 0, NULL, 4, 0, 4, 0, 204, 0, 0, 2108},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2111},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2114},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2117},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 852, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1226, 18, 2120},
    {-1, 12, 190, NULL, 16, 3, 1, 0, 12, 855, 12, 0},
    {1, 12, 0, "CoMPInformationStartTime", 4, 16, 2, 4, 216, 1228, 18, 2123},
    {-1, 448, 0, "CellCapacityClassValue", 4, 0, 4, 0, 204, 0, 0, 2125},
    {-1, 450, 0, "CapacityValue", 2, 0, 2, 0, 200, 0, 55, 2127},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2129},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2132},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2135},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 858, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1235, 18, 2138},
    {-1, 12, 623, "CompositeAvailableCapacity", 16, 3, 1, 0, 12, 861, 12, 0},
    {-1, 448, 623, "CompositeAvailableCapacity", 16, 3, 1, 0, 12, 861, 12, 0},
    {-1, 450, 623, "CompositeAvailableCapacity", 16, 3, 1, 0, 12, 861, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2141},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2144},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2147},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 864, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1243, 18, 2150},
    {-1, 348, 0, "PDCP-SNExtended", 2, 0, 2, 0, 200, 0, 55, 2153},
    {-1, 348, 0, "HFNModified", 4, 0, 4, 0, 200, 0, 55, 2155},
    {-1, 448, 0, "PDCP-SNExtended", 2, 0, 2, 0, 200, 0, 55, 2157},
    {-1, 450, 0, "HFNModified", 4, 0, 4, 0, 200, 0, 55, 2159},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2161},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2164},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2167},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 867, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1252, 18, 2170},
    {2, 174, 0, "TriggeringMessage", 4, 0, 4, 0, 24, 352, 58, 0},
    {-1, 448, 0, "ProcedureCode", 2, 0, 2, 0, 200, 0, 55, 2173},
    {2, 450, 0, "TriggeringMessage", 4, 0, 4, 0, 24, 352, 58, 0},
    {2, 452, 0, "Criticality", 4, 0, 4, 0, 24, 130, 58, 0},
    {256, 454, 0, "CriticalityDiagnostics-IE-List", 4, 20, 2, 4, 216, 1273, 18, 2175},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2178},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2181},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2184},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 870, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 1262, 18, 2187},
    {INT_MAX, 174, 0, "TypeOfError", 4, 0, 4, 0, 2076, 357, 58, 0},
    {2, 448, 0, "Criticality", 4, 0, 4, 0, 24, 130, 58, 0},
    {-1, 450, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2190},
    {INT_MAX, 452, 0, "TypeOfError", 4, 0, 4, 0, 2076, 357, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2192},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2195},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2198},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 873, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1271, 18, 2201},
    {-1, 12, 281, NULL, 20, 4, 1, 0, 12, 876, 12, 0},
    {256, 12, 0, "CriticalityDiagnostics-IE-List", 4, 20, 2, 4, 216, 1273, 18, 2204},
    {INT_MAX, 174, 0, "PA-Values", 4, 0, 4, 0, 2076, 364, 58, 0},
    {8, 448, 0, NULL, 8, 0, 2, 4, 216, 0, 3, 2206},
    {-1, 450, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 2209},
    {3, 452, 0, "_seqof127", 4, 4, 2, 4, 248, 1275, 18, 2211},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2214},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2217},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2220},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 880, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1282, 18, 2223},
    {-1, 12, 637, "DynamicNAICSInformation", 24, 4, 1, 0, 12, 883, 12, 0},
    {-1, 448, 637, "DynamicNAICSInformation", 24, 4, 1, 0, 12, 883, 12, 0},
    {-1, 450, 0, NULL, 1, 0, 0, 0, 8, 0, 5, 0},
    {15, 12, 0, "EPLMNs", 4, 8, 2, 4, 216, 141, 18, 2226},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2228},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2231},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2234},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 887, 12, 0},
    {-1, 448, 0, "E-RAB-ID", 4, 0, 4, 0, 204, 0, 0, 2237},
    {-1, 450, 297, "Cause", 8, 4, 2, 4, 12, 145, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2239},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2242},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2245},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 890, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1297, 18, 2248},
    {8, 202, 0, "EUTRANTraceID", 2, 0, 2, 2, 216, 0, 21, 2251},
    {-1, 348, 0, "ExpectedActivityPeriod", 4, 0, 4, 0, 204, 0, 0, 2254},
    {-1, 348, 0, "ExpectedIdlePeriod", 4, 0, 4, 0, 204, 0, 0, 2266},
    {INT_MAX, 174, 0, "SourceOfUEActivityBehaviourInformation", 4, 0, 4, 0, 2076, 377, 58, 0},
    {-1, 448, 0, "ExpectedActivityPeriod", 4, 0, 4, 0, 204, 0, 0, 2278},
    {-1, 450, 0, "ExpectedIdlePeriod", 4, 0, 4, 0, 204, 0, 0, 2280},
    {INT_MAX, 452, 0, "SourceOfUEActivityBehaviourInformation", 4, 0, 4, 0, 2076, 377, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2282},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2285},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2288},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 893, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1309, 18, 2291},
    {-1, 12, 659, "ExpectedUEActivityBehaviour", 20, 4, 1, 0, 12, 896, 12, 0},
    {INT_MAX, 174, 0, "ExpectedHOInterval", 4, 0, 4, 0, 2076, 384, 58, 0},
    {-1, 448, 659, "ExpectedUEActivityBehaviour", 20, 4, 1, 0, 12, 896, 12, 0},
    {INT_MAX, 450, 0, "ExpectedHOInterval", 4, 0, 4, 0, 2076, 384, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2294},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2297},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2300},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 900, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1318, 18, 2303},
    {5, 448, 0, NULL, 8, 0, 2, 4, 216, 0, 3, 2306},
    {110, 450, 0, "UL-InterferenceOverloadIndication", 4, 4, 2, 4, 216, 1545, 18, 2309},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2311},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2314},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2317},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 903, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1325, 18, 2320},
    {3, 448, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 2323},
    {4096, 450, 0, "ForbiddenTACs", 4, 4, 2, 4, 216, 158, 18, 2325},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2328},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2331},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2334},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 906, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1332, 18, 2337},
    {-1, 12, 190, "ForbiddenTAs-Item", 16, 3, 1, 0, 12, 909, 12, 0},
    {16, 12, 0, "ForbiddenTAs", 4, 16, 2, 4, 216, 1334, 18, 2340},
    {4096, 12, 0, "ForbiddenTACs", 4, 4, 2, 4, 216, 158, 18, 2342},
    {3, 448, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 2344},
    {4096, 450, 0, "ForbiddenLACs", 4, 4, 2, 4, 216, 1346, 18, 2346},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2349},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2352},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2355},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 912, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1342, 18, 2358},
    {-1, 12, 190, "ForbiddenLAs-Item", 16, 3, 1, 0, 12, 915, 12, 0},
    {16, 12, 0, "ForbiddenLAs", 4, 16, 2, 4, 216, 1344, 18, 2361},
    {2, 202, 0, "LAC", 2, 0, 2, 2, 216, 0, 21, 2363},
    {4096, 12, 0, "ForbiddenLACs", 4, 4, 2, 4, 216, 1346, 18, 2366},
    {24, 88, 0, "Fourframes", 8, 0, 2, 4, 216, 0, 3, 2368},
    {-1, 348, 0, "FreqBandIndicator", 4, 0, 4, 0, 204, 0, 0, 2371},
    {2, 202, 0, "MME-Group-ID", 2, 0, 2, 2, 216, 0, 21, 2374},
    {3, 448, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 2377},
    {2, 450, 0, "MME-Group-ID", 2, 0, 2, 2, 216, 0, 21, 2379},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2381},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2384},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2387},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 918, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1356, 18, 2390},
    {-1, 12, 190, "GU-Group-ID", 16, 3, 1, 0, 12, 921, 12, 0},
    {1, 202, 0, "MME-Code", 2, 0, 2, 2, 216, 0, 21, 2393},
    {-1, 448, 190, "GU-Group-ID", 16, 3, 1, 0, 12, 921, 12, 0},
    {1, 450, 0, "MME-Code", 2, 0, 2, 2, 216, 0, 21, 2396},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2398},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2401},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2404},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 924, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1365, 18, 2407},
    {8, 88, 0, "InterfacesToTrace", 8, 0, 2, 4, 216, 0, 3, 2410},
    {-1, 348, 0, "Time-UE-StayedInCell", 2, 0, 2, 0, 200, 0, 55, 2413},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 450, 166, "CellType", 12, 2, 1, 0, 12, 835, 12, 0},
    {-1, 452, 0, "Time-UE-StayedInCell", 2, 0, 2, 0, 200, 0, 55, 2415},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2417},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2420},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2423},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 927, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1375, 18, 2426},
    {-1, 12, 281, "LastVisitedEUTRANCellInformation", 44, 4, 1, 0, 12, 930, 12, 0},
    {-1, 202, 0, "LastVisitedUTRANCellInformation", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 448, 0, NULL, 1, 0, 0, 0, 8, 0, 5, 0},
    {-1, 2, 687, "LastVisitedGERANCellInformation", 4, 1, 2, 2, 12, 934, 13, 0},
    {-1, 448, 281, "LastVisitedEUTRANCellInformation", 44, 4, 1, 0, 12, 930, 12, 0},
    {-1, 450, 0, "LastVisitedUTRANCellInformation", 8, 0, 4, 4, 8, 0, 20, 0},
    {-1, 452, 687, "LastVisitedGERANCellInformation", 4, 1, 2, 2, 12, 934, 13, 0},
    {-1, 2, 3, "LastVisitedCell-Item", 48, 3, 2, 4, 12, 935, 13, 0},
    {INT_MAX, 174, 0, "Links-to-log", 4, 0, 4, 0, 2076, 396, 58, 0},
    {INT_MAX, 174, 0, "M3period", 4, 0, 4, 0, 2076, 404, 58, 0},
    {INT_MAX, 448, 0, "M3period", 4, 0, 4, 0, 2076, 404, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2429},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2432},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2435},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 938, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1391, 18, 2438},
    {INT_MAX, 174, 0, "M4period", 4, 0, 4, 0, 2076, 412, 58, 0},
    {INT_MAX, 448, 0, "M4period", 4, 0, 4, 0, 2076, 412, 58, 0},
    {INT_MAX, 450, 0, "Links-to-log", 4, 0, 4, 0, 2076, 396, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2441},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2444},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2447},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 941, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1399, 18, 2450},
    {INT_MAX, 174, 0, "M5period", 4, 0, 4, 0, 2076, 412, 58, 0},
    {INT_MAX, 448, 0, "M5period", 4, 0, 4, 0, 2076, 412, 58, 0},
    {INT_MAX, 450, 0, "Links-to-log", 4, 0, 4, 0, 2076, 396, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2453},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2456},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2459},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 944, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1407, 18, 2462},
    {INT_MAX, 174, 0, "MDT-Activation", 4, 0, 4, 0, 2076, 422, 58, 0},
    {8, 88, 0, "MeasurementsToActivate", 8, 0, 2, 4, 216, 0, 3, 2465},
    {INT_MAX, 174, 0, "M1ReportingTrigger", 4, 0, 4, 0, 2076, 429, 58, 0},
    {-1, 348, 0, "Threshold-RSRP", 2, 0, 2, 0, 200, 0, 55, 2468},
    {-1, 348, 0, "Threshold-RSRQ", 2, 0, 2, 0, 200, 0, 55, 2470},
    {-1, 448, 0, "Threshold-RSRP", 2, 0, 2, 0, 200, 0, 55, 2472},
    {-1, 450, 0, "Threshold-RSRQ", 2, 0, 2, 0, 200, 0, 55, 2474},
    {-1, 2, 204, "MeasurementThresholdA2", 4, 2, 2, 2, 12, 947, 13, 0},
    {-1, 448, 204, "MeasurementThresholdA2", 4, 2, 2, 2, 12, 947, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2476},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2479},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2482},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 949, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1421, 18, 2485},
    {-1, 12, 166, "M1ThresholdEventA2", 12, 2, 1, 0, 12, 952, 12, 0},
    {12, 174, 0, "ReportIntervalMDT", 4, 0, 4, 0, 24, 437, 58, 0},
    {7, 174, 0, "ReportAmountMDT", 4, 0, 4, 0, 24, 452, 58, 0},
    {12, 448, 0, "ReportIntervalMDT", 4, 0, 4, 0, 24, 437, 58, 0},
    {7, 450, 0, "ReportAmountMDT", 4, 0, 4, 0, 24, 452, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2488},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2491},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2494},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 954, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1431, 18, 2497},
    {-1, 12, 190, "M1PeriodicReporting", 16, 3, 1, 0, 12, 957, 12, 0},
    {INT_MAX, 448, 0, "MDT-Activation", 4, 0, 4, 0, 2076, 422, 58, 0},
    {1, 450, 297, "AreaScopeOfMDT", 16, 4, 2, 4, 2076, 828, 13, 0},
    {8, 452, 0, "MeasurementsToActivate", 8, 0, 2, 4, 216, 0, 3, 2500},
    {INT_MAX, 454, 0, "M1ReportingTrigger", 4, 0, 4, 0, 2076, 429, 58, 0},
    {-1, 456, 166, "M1ThresholdEventA2", 12, 2, 1, 0, 12, 952, 12, 0},
    {-1, 458, 190, "M1PeriodicReporting", 16, 3, 1, 0, 12, 957, 12, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2502},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2505},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2508},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 960, 12, 0},
    {65535, 504, 0, NULL, 4, 24, 2, 4, 216, 1443, 18, 2511},
    {2, 202, 0, "MBMS-Service-Area-Identity", 2, 0, 2, 2, 216, 0, 21, 2514},
    {INT_MAX, 174, 0, "RadioframeAllocationPeriod", 4, 0, 4, 0, 2076, 462, 58, 0},
    {-1, 348, 0, "RadioframeAllocationOffset", 4, 0, 4, 0, 204, 0, 0, 2517},
    {6, 88, 0, "Oneframe", 8, 0, 2, 4, 216, 0, 3, 2520},
    {6, 448, 0, "Oneframe", 8, 0, 2, 4, 216, 0, 3, 2523},
    {24, 450, 0, "Fourframes", 8, 0, 2, 4, 216, 0, 3, 2525},
    {-1, 2, 204, "SubframeAllocation", 12, 2, 2, 4, 12, 963, 13, 0},
    {INT_MAX, 448, 0, "RadioframeAllocationPeriod", 4, 0, 4, 0, 2076, 462, 58, 0},
    {-1, 450, 0, "RadioframeAllocationOffset", 4, 0, 4, 0, 204, 0, 0, 2527},
    {-1, 452, 204, "SubframeAllocation", 12, 2, 2, 4, 12, 963, 13, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2529},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2532},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2535},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 965, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1458, 18, 2538},
    {-1, 12, 281, "MBSFN-Subframe-Info", 28, 4, 1, 0, 12, 968, 12, 0},
    {-1, 448, 0, NULL, 2, 0, 2, 0, 200, 0, 0, 2541},
    {-1, 450, 0, NULL, 2, 0, 2, 0, 200, 0, 0, 2543},
    {-1, 448, 0, NULL, 2, 0, 2, 0, 200, 0, 0, 2545},
    {-1, 448, 0, "FreqBandIndicator", 4, 0, 4, 0, 204, 0, 0, 2547},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2549},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2552},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2555},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 972, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1468, 18, 2558},
    {-1, 12, 166, "BandInfo", 12, 2, 1, 0, 12, 975, 12, 0},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 450, 0, "PCI", 4, 0, 4, 0, 204, 0, 0, 2561},
    {-1, 452, 0, "EARFCN", 2, 0, 2, 0, 200, 0, 55, 2563},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2565},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2568},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2571},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 977, 12, 0},
    {65535, 454, 0, NULL, 4, 24, 2, 4, 216, 1477, 18, 2574},
    {-1, 12, 281, NULL, 36, 4, 1, 0, 12, 980, 12, 0},
    {512, 12, 0, "Neighbour-Information", 4, 36, 2, 4, 216, 1479, 18, 2577},
    {-1, 448, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 2579},
    {-1, 450, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 2581},
    {-1, 452, 0, NULL, 1, 0, 0, 0, 8, 0, 8, 0},
    {-1, 454, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 2583},
    {-1, 456, 0, NULL, 2, 0, 2, 0, 200, 0, 55, 2585},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2587},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2590},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2593},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 984, 12, 0},
    {65535, 458, 0, NULL, 4, 24, 2, 4, 216, 1489, 18, 2596},
    {INT_MAX, 174, 0, "ProSeDirectDiscovery", 4, 0, 4, 0, 2076, 473, 58, 0},
    {INT_MAX, 174, 0, "ProSeDirectCommunication", 4, 0, 4, 0, 2076, 473, 58, 0},
    {INT_MAX, 448, 0, "ProSeDirectDiscovery", 4, 0, 4, 0, 2076, 473, 58, 0},
    {INT_MAX, 450, 0, "ProSeDirectCommunication", 4, 0, 4, 0, 2076, 473, 58, 0},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2599},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2602},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2605},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 987, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1498, 18, 2608},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {-1, 450, 0, NULL, 4, 0, 4, 0, 204, 0, 0, 2611},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2614},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2617},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2620},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 990, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1505, 18, 2623},
    {-1, 12, 190, NULL, 32, 3, 1, 0, 12, 993, 12, 0},
    {9, 12, 0, "RSRPMeasurementResult", 4, 32, 2, 4, 216, 1507, 18, 2626},
    {9, 448, 0, "RSRPMeasurementResult", 4, 32, 2, 4, 216, 1507, 18, 2629},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2631},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2634},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2637},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 996, 12, 0},
    {65535, 450, 0, NULL, 4, 24, 2, 4, 216, 1513, 18, 2640},
    {-1, 12, 166, NULL, 12, 2, 1, 0, 12, 999, 12, 0},
    {-1, 448, 530, "ServedCell-Information", 80, 6, 1, 0, 12, 408, 12, 0},
    {512, 450, 0, "Neighbour-Information", 4, 36, 2, 4, 216, 1479, 18, 2643},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2645},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2648},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2651},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 1001, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1521, 18, 2654},
    {-1, 12, 176, NULL, 92, 3, 1, 0, 12, 1004, 12, 0},
    {8, 12, 0, "TAListforMDT", 4, 4, 2, 4, 216, 158, 18, 2657},
    {2, 448, 0, "TAC", 2, 0, 2, 2, 216, 0, 21, 2659},
    {3, 450, 0, "PLMN-Identity", 2, 0, 2, 2, 216, 0, 21, 2661},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2663},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2666},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2669},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 1007, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1530, 18, 2672},
    {-1, 12, 190, "TAI-Item", 16, 3, 1, 0, 12, 1010, 12, 0},
    {8, 12, 0, "TAIListforMDT", 4, 16, 2, 4, 216, 1532, 18, 2675},
    {INT_MAX, 174, 0, "TraceDepth", 4, 0, 4, 0, 2076, 480, 58, 0},
    {160, 88, 0, "TraceCollectionEntityIPAddress", 8, 0, 2, 4, 220, 0, 3, 2677},
    {8, 448, 0, "EUTRANTraceID", 2, 0, 2, 2, 216, 0, 21, 2681},
    {8, 450, 0, "InterfacesToTrace", 8, 0, 2, 4, 216, 0, 3, 2683},
    {INT_MAX, 452, 0, "TraceDepth", 4, 0, 4, 0, 2076, 480, 58, 0},
    {160, 454, 0, "TraceCollectionEntityIPAddress", 8, 0, 2, 4, 220, 0, 3, 2685},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2687},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2690},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2693},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 1013, 12, 0},
    {65535, 456, 0, NULL, 4, 24, 2, 4, 216, 1543, 18, 2696},
    {INT_MAX, 174, 0, "UL-InterferenceOverloadIndication-Item", 4, 0, 4, 0, 2076, 491, 58, 0},
    {110, 12, 0, "UL-InterferenceOverloadIndication", 4, 4, 2, 4, 216, 1545, 18, 2699},
    {110, 88, 0, "UL-HighInterferenceIndication", 8, 0, 2, 4, 220, 0, 3, 2701},
    {-1, 448, 190, "ECGI", 20, 3, 1, 0, 12, 165, 12, 0},
    {110, 450, 0, "UL-HighInterferenceIndication", 8, 0, 2, 4, 220, 0, 3, 2705},
    {-1, 448, 0, "ProtocolIE-ID", 2, 0, 2, 0, 200, 0, 55, 2707},
    {2, 450, 0, "Criticality", 4, 0, 4, 0, 88, 130, 58, 2710},
    {-1, 452, 0, NULL, 16, 0, 0, 0, 72, 0, 51, 2713},
    {-1, 12, 190, NULL, 24, 3, 0, 0, 264, 1016, 12, 0},
    {65535, 452, 0, NULL, 4, 24, 2, 4, 216, 1553, 18, 2716},
    {-1, 12, 190, "UL-HighInterferenceIndicationInfo-Item", 36, 3, 1, 0, 12, 1019, 12, 0},
    {256, 12, 0, "UL-HighInterferenceIndicationInfo", 4, 36, 2, 4, 216, 1555, 18, 2719},
    {2, 174, 0, "Presence", 4, 0, 4, 0, 24, 499, 58, 0},
    {-1, 2, 0, "X2AP-PROTOCOL-IES", 16, 4, 0, 0, 8, 1022, 49, 0},
    {-1, 2, 0, "X2AP-PROTOCOL-EXTENSION", 16, 4, 0, 0, 8, 1026, 49, 0},
    {-1, 348, 0, NULL, 4, 0, 4, 0, 8, 0, 0, 0},
    {-1, 2, 0, "X2AP-PRIVATE-IES", 28, 5, 0, 0, 8, 1030, 49, 0}
};

static const struct ConstraintEntry _econstraintarray[] = {
    {5, 14, (void *)_v665},
    {0, 6, (void *)2},
    {0, 1, (void *)&_v337},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)5},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)8},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)11},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)14},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)17},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)20},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)23},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)26},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)29},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)32},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)35},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)38},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)41},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)44},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)47},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)50},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)53},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)56},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)59},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)62},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v443},
    {0, 6, (void *)65},
    {0, 1, (void *)&_v379},
    {5, 14, (void *)_v380},
    {0, 6, (void *)68},
    {0, 1, (void *)&_v381},
    {5, 14, (void *)_v383},
    {0, 20, (void *)0x47},
    {0, 3, (void *)_v383},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)74},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1097},
    {0, 6, (void *)77},
    {0, 3, (void *)_v1097},
    {5, 14, (void *)_v388},
    {0, 6, (void *)80},
    {0, 1, (void *)&_v389},
    {5, 14, (void *)_v1097},
    {0, 6, (void *)83},
    {0, 3, (void *)_v1097},
    {5, 14, (void *)_v1220},
    {0, 6, (void *)86},
    {0, 1, (void *)&_v393},
    {5, 14, (void *)_v395},
    {0, 20, (void *)0x59},
    {0, 3, (void *)_v395},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)92},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1216},
    {0, 6, (void *)95},
    {0, 3, (void *)_v1216},
    {5, 14, (void *)_v1097},
    {0, 6, (void *)98},
    {0, 3, (void *)_v1097},
    {5, 14, (void *)_v1175},
    {0, 20, (void *)0x65},
    {0, 3, (void *)_v1175},
    {5, 14, (void *)_v1212},
    {0, 6, (void *)104},
    {0, 1, (void *)&_v405},
    {5, 14, (void *)_v407},
    {0, 6, (void *)107},
    {0, 3, (void *)_v407},
    {5, 14, (void *)_v665},
    {0, 6, (void *)110},
    {0, 1, (void *)&_v409},
    {5, 14, (void *)_v411},
    {0, 6, (void *)113},
    {0, 3, (void *)_v411},
    {5, 14, (void *)_v798},
    {0, 6, (void *)116},
    {0, 1, (void *)&_v413},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)119},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v443},
    {0, 6, (void *)122},
    {0, 1, (void *)&_v417},
    {5, 14, (void *)_v1211},
    {0, 6, (void *)125},
    {0, 1, (void *)&_v419},
    {5, 14, (void *)_v421},
    {0, 3, (void *)_v421},
    {5, 14, (void *)_v1097},
    {0, 6, (void *)130},
    {0, 3, (void *)_v1097},
    {5, 14, (void *)_v1122},
    {0, 3, (void *)_v1122},
    {5, 14, (void *)_v1037},
    {0, 3, (void *)_v1037},
    {5, 14, (void *)_v1037},
    {1, 11, (void *)134},
    {0, 16, (void *)0xe0},
    {0, 17, (void *)0x8c008b},
    {0, 18, (void *)0xe000e2},
    {0, 16, (void *)0xe1},
    {0, 17, (void *)0x8f008e},
    {0, 18, (void *)0xe000e2},
    {0, 16, (void *)0xdd},
    {5, 14, (void *)_v1037},
    {1, 11, (void *)134},
    {0, 16, (void *)0xe0},
    {0, 17, (void *)0x950094},
    {0, 18, (void *)0xe000e5},
    {0, 16, (void *)0xe1},
    {0, 17, (void *)0x980097},
    {0, 18, (void *)0xe000e5},
    {0, 16, (void *)0xde},
    {5, 14, (void *)_v1037},
    {1, 11, (void *)134},
    {0, 16, (void *)0xe0},
    {0, 17, (void *)0x9e009d},
    {0, 18, (void *)0xe000e8},
    {0, 16, (void *)0xe1},
    {0, 17, (void *)0xa100a0},
    {0, 18, (void *)0xe000e8},
    {0, 16, (void *)0xdf},
    {5, 14, (void *)_v1229},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x303fe},
    {0, 17, (void *)0xa900a8},
    {0, 19, (void *)0x3fe00eb},
    {0, 16, (void *)0x303ff},
    {0, 17, (void *)0xac00ab},
    {0, 19, (void *)0x3fe00eb},
    {0, 16, (void *)0x30400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)175},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v920},
    {0, 3, (void *)_v920},
    {5, 14, (void *)_v443},
    {0, 6, (void *)180},
    {0, 20, (void *)0xb5},
    {0, 1, (void *)&_v439},
    {5, 14, (void *)_v443},
    {0, 6, (void *)184},
    {0, 20, (void *)0xb9},
    {0, 1, (void *)&_v441},
    {5, 14, (void *)_v443},
    {0, 11, (void *)179},
    {5, 14, (void *)_v443},
    {0, 11, (void *)183},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa20402},
    {0, 17, (void *)0xc300c2},
    {0, 19, (void *)0x40200ee},
    {0, 16, (void *)0xa20403},
    {0, 17, (void *)0xc600c5},
    {0, 19, (void *)0x40200ee},
    {0, 16, (void *)0xa20404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)201},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v798},
    {0, 6, (void *)204},
    {0, 1, (void *)&_v448},
    {5, 14, (void *)_v452},
    {0, 3, (void *)_v452},
    {5, 14, (void *)_v798},
    {0, 11, (void *)203},
    {5, 14, (void *)_v452},
    {0, 11, (void *)206},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x670402},
    {0, 17, (void *)0xd800d7},
    {0, 19, (void *)0x40200f1},
    {0, 16, (void *)0x670403},
    {0, 17, (void *)0xdb00da},
    {0, 19, (void *)0x40200f1},
    {0, 16, (void *)0x670404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)222},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v506},
    {0, 3, (void *)_v506},
    {5, 14, (void *)_v506},
    {0, 3, (void *)_v506},
    {5, 14, (void *)_v506},
    {0, 3, (void *)_v506},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa10402},
    {0, 17, (void *)0xea00e9},
    {0, 19, (void *)0x40200f7},
    {0, 16, (void *)0xa10403},
    {0, 17, (void *)0xed00ec},
    {0, 19, (void *)0x40200f7},
    {0, 16, (void *)0xa10404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)240},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1232},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1212},
    {0, 11, (void *)103},
    {5, 14, (void *)_v1057},
    {0, 6, (void *)247},
    {0, 3, (void *)_v1057},
    {5, 14, (void *)_v1097},
    {0, 6, (void *)250},
    {0, 3, (void *)_v1097},
    {5, 14, (void *)_v1097},
    {0, 6, (void *)253},
    {0, 3, (void *)_v1097},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x860402},
    {0, 17, (void *)0x1030102},
    {0, 19, (void *)0x40200fa},
    {0, 16, (void *)0x860403},
    {0, 17, (void *)0x1060105},
    {0, 19, (void *)0x40200fa},
    {0, 16, (void *)0x860404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)265},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x890402},
    {0, 17, (void *)0x10f010e},
    {0, 19, (void *)0x4020103},
    {0, 16, (void *)0x890403},
    {0, 17, (void *)0x1120111},
    {0, 19, (void *)0x4020103},
    {0, 16, (void *)0x890404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)277},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v920},
    {0, 11, (void *)177},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)242},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)284},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x40402},
    {0, 17, (void *)0x1220121},
    {0, 19, (void *)0x4020109},
    {0, 16, (void *)0x40403},
    {0, 17, (void *)0x1250124},
    {0, 19, (void *)0x4020109},
    {0, 16, (void *)0x40404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)296},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x503fe},
    {0, 17, (void *)0x12e012d},
    {0, 19, (void *)0x3fe010c},
    {0, 16, (void *)0x503ff},
    {0, 17, (void *)0x1310130},
    {0, 19, (void *)0x3fe010c},
    {0, 16, (void *)0x50400},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)283},
    {5, 14, (void *)_v1059},
    {0, 20, (void *)0x136},
    {0, 3, (void *)_v1059},
    {5, 14, (void *)_v1037},
    {0, 3, (void *)_v1037},
    {5, 14, (void *)_v1184},
    {0, 3, (void *)_v1184},
    {5, 14, (void *)_v1184},
    {0, 11, (void *)314},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x680402},
    {0, 17, (void *)0x1420141},
    {0, 19, (void *)0x402010f},
    {0, 16, (void *)0x680403},
    {0, 17, (void *)0x1450144},
    {0, 19, (void *)0x402010f},
    {0, 16, (void *)0x680404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)328},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v506},
    {0, 3, (void *)_v506},
    {5, 14, (void *)_v506},
    {0, 3, (void *)_v506},
    {5, 14, (void *)_v506},
    {0, 3, (void *)_v506},
    {5, 14, (void *)_v506},
    {0, 3, (void *)_v506},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x810402},
    {0, 17, (void *)0x1560155},
    {0, 19, (void *)0x4020116},
    {0, 16, (void *)0x810403},
    {0, 17, (void *)0x1590158},
    {0, 19, (void *)0x4020116},
    {0, 16, (void *)0x810404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)348},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1037},
    {0, 11, (void *)312},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x790402},
    {0, 17, (void *)0x1640163},
    {0, 19, (void *)0x402011e},
    {0, 16, (void *)0x790403},
    {0, 17, (void *)0x1670166},
    {0, 19, (void *)0x402011e},
    {0, 16, (void *)0x790404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)362},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1221},
    {0, 6, (void *)365},
    {0, 20, (void *)0x16e},
    {0, 3, (void *)_v1221},
    {5, 14, (void *)_v519},
    {0, 6, (void *)369},
    {0, 1, (void *)&_v517},
    {5, 14, (void *)_v1221},
    {0, 11, (void *)364},
    {5, 14, (void *)_v519},
    {0, 11, (void *)368},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x830402},
    {0, 17, (void *)0x17b017a},
    {0, 19, (void *)0x4020125},
    {0, 16, (void *)0x830403},
    {0, 17, (void *)0x17e017d},
    {0, 19, (void *)0x4020125},
    {0, 16, (void *)0x830404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)385},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x60402},
    {0, 17, (void *)0x1890188},
    {0, 19, (void *)0x402012b},
    {0, 16, (void *)0x60403},
    {0, 17, (void *)0x18c018b},
    {0, 19, (void *)0x402012b},
    {0, 16, (void *)0x60404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)399},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x703fe},
    {0, 17, (void *)0x1950194},
    {0, 19, (void *)0x3fe012e},
    {0, 16, (void *)0x703ff},
    {0, 17, (void *)0x1980197},
    {0, 19, (void *)0x3fe012e},
    {0, 16, (void *)0x70400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)411},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa603fe},
    {0, 17, (void *)0x1a101a0},
    {0, 19, (void *)0x3fe0131},
    {0, 16, (void *)0xa603ff},
    {0, 17, (void *)0x1a401a3},
    {0, 19, (void *)0x3fe0131},
    {0, 16, (void *)0xa60400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x80402},
    {0, 17, (void *)0x1ac01ab},
    {0, 19, (void *)0x4020134},
    {0, 16, (void *)0x80403},
    {0, 17, (void *)0x1af01ae},
    {0, 19, (void *)0x4020134},
    {0, 16, (void *)0x80404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)434},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x903fe},
    {0, 17, (void *)0x1b801b7},
    {0, 19, (void *)0x3fe0137},
    {0, 16, (void *)0x903ff},
    {0, 17, (void *)0x1bb01ba},
    {0, 19, (void *)0x3fe0137},
    {0, 16, (void *)0x90400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)446},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa03fe},
    {0, 17, (void *)0x1c401c3},
    {0, 19, (void *)0x3fe013a},
    {0, 16, (void *)0xa03ff},
    {0, 17, (void *)0x1c701c6},
    {0, 19, (void *)0x3fe013a},
    {0, 16, (void *)0xa0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)458},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xb03fe},
    {0, 17, (void *)0x1d001cf},
    {0, 19, (void *)0x3fe013d},
    {0, 16, (void *)0xb03ff},
    {0, 17, (void *)0x1d301d2},
    {0, 19, (void *)0x3fe013d},
    {0, 16, (void *)0xb0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)470},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa903fe},
    {0, 17, (void *)0x1dc01db},
    {0, 19, (void *)0x3fe0140},
    {0, 16, (void *)0xa903ff},
    {0, 17, (void *)0x1df01de},
    {0, 19, (void *)0x3fe0140},
    {0, 16, (void *)0xa90400},
    {5, 14, (void *)_v557},
    {0, 6, (void *)482},
    {0, 1, (void *)&_v546},
    {5, 14, (void *)_v1122},
    {0, 3, (void *)_v1122},
    {5, 14, (void *)_v552},
    {0, 3, (void *)_v552},
    {5, 14, (void *)_v1122},
    {0, 11, (void *)484},
    {5, 14, (void *)_v552},
    {0, 11, (void *)486},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x710402},
    {0, 17, (void *)0x1f001ef},
    {0, 19, (void *)0x4020143},
    {0, 16, (void *)0x710403},
    {0, 17, (void *)0x1f301f2},
    {0, 19, (void *)0x4020143},
    {0, 16, (void *)0x710404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)502},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v557},
    {0, 11, (void *)481},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xc0402},
    {0, 17, (void *)0x20001ff},
    {0, 19, (void *)0x4020149},
    {0, 16, (void *)0xc0403},
    {0, 17, (void *)0x2030202},
    {0, 19, (void *)0x4020149},
    {0, 16, (void *)0xc0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)518},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xd03fe},
    {0, 17, (void *)0x20c020b},
    {0, 19, (void *)0x3fe014c},
    {0, 16, (void *)0xd03ff},
    {0, 17, (void *)0x20f020e},
    {0, 19, (void *)0x3fe014c},
    {0, 16, (void *)0xd0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)530},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xe03fe},
    {0, 17, (void *)0x2180217},
    {0, 19, (void *)0x3fe014f},
    {0, 16, (void *)0xe03ff},
    {0, 17, (void *)0x21b021a},
    {0, 19, (void *)0x3fe014f},
    {0, 16, (void *)0xe0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)542},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xf03fe},
    {0, 17, (void *)0x2240223},
    {0, 19, (void *)0x3fe0152},
    {0, 16, (void *)0xf03ff},
    {0, 17, (void *)0x2270226},
    {0, 19, (void *)0x3fe0152},
    {0, 16, (void *)0xf0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)554},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1003fe},
    {0, 17, (void *)0x230022f},
    {0, 19, (void *)0x3fe0155},
    {0, 16, (void *)0x1003ff},
    {0, 17, (void *)0x2330232},
    {0, 19, (void *)0x3fe0155},
    {0, 16, (void *)0x100400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)566},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1103fe},
    {0, 17, (void *)0x23c023b},
    {0, 19, (void *)0x3fe0158},
    {0, 16, (void *)0x1103ff},
    {0, 17, (void *)0x23f023e},
    {0, 19, (void *)0x3fe0158},
    {0, 16, (void *)0x110400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)578},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1203fe},
    {0, 17, (void *)0x2480247},
    {0, 19, (void *)0x3fe015b},
    {0, 16, (void *)0x1203ff},
    {0, 17, (void *)0x24b024a},
    {0, 19, (void *)0x3fe015b},
    {0, 16, (void *)0x120400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)590},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1303fe},
    {0, 17, (void *)0x2540253},
    {0, 19, (void *)0x3fe015e},
    {0, 16, (void *)0x1303ff},
    {0, 17, (void *)0x2570256},
    {0, 19, (void *)0x3fe015e},
    {0, 16, (void *)0x130400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)602},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1403fe},
    {0, 17, (void *)0x260025f},
    {0, 19, (void *)0x3fe0161},
    {0, 16, (void *)0x1403ff},
    {0, 17, (void *)0x2630262},
    {0, 19, (void *)0x3fe0161},
    {0, 16, (void *)0x140400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)614},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1503fe},
    {0, 17, (void *)0x26c026b},
    {0, 19, (void *)0x3fe0164},
    {0, 16, (void *)0x1503ff},
    {0, 17, (void *)0x26f026e},
    {0, 19, (void *)0x3fe0164},
    {0, 16, (void *)0x150400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)626},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xad03fe},
    {0, 17, (void *)0x2780277},
    {0, 19, (void *)0x3fe0167},
    {0, 16, (void *)0xad03ff},
    {0, 17, (void *)0x27b027a},
    {0, 19, (void *)0x3fe0167},
    {0, 16, (void *)0xad0400},
    {5, 14, (void *)_v750},
    {0, 6, (void *)638},
    {0, 1, (void *)&_v590},
    {5, 14, (void *)_v1212},
    {0, 11, (void *)103},
    {5, 14, (void *)_v750},
    {0, 11, (void *)637},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x780402},
    {0, 17, (void *)0x2880287},
    {0, 19, (void *)0x402016a},
    {0, 16, (void *)0x780403},
    {0, 17, (void *)0x28b028a},
    {0, 19, (void *)0x402016a},
    {0, 16, (void *)0x780404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)654},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v597},
    {0, 6, (void *)657},
    {0, 20, (void *)0x292},
    {0, 3, (void *)_v597},
    {5, 14, (void *)_v599},
    {0, 20, (void *)0x295},
    {0, 3, (void *)_v599},
    {5, 14, (void *)_v601},
    {0, 20, (void *)0x298},
    {0, 3, (void *)_v601},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x950402},
    {0, 17, (void *)0x29e029d},
    {0, 19, (void *)0x402016d},
    {0, 16, (void *)0x950403},
    {0, 17, (void *)0x2a102a0},
    {0, 19, (void *)0x402016d},
    {0, 16, (void *)0x950404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)676},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1228},
    {0, 6, (void *)679},
    {0, 3, (void *)_v1228},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)682},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x160402},
    {0, 17, (void *)0x2b002af},
    {0, 19, (void *)0x4020176},
    {0, 16, (void *)0x160403},
    {0, 17, (void *)0x2b302b2},
    {0, 19, (void *)0x4020176},
    {0, 16, (void *)0x160404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)694},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1703fe},
    {0, 17, (void *)0x2bc02bb},
    {0, 19, (void *)0x3fe0179},
    {0, 16, (void *)0x1703ff},
    {0, 17, (void *)0x2bf02be},
    {0, 19, (void *)0x3fe0179},
    {0, 16, (void *)0x170400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)706},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {0, 11, (void *)708},
    {5, 14, (void *)_v1229},
    {0, 11, (void *)708},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x760402},
    {0, 17, (void *)0x2ce02cd},
    {0, 19, (void *)0x402017c},
    {0, 16, (void *)0x760403},
    {0, 17, (void *)0x2d102d0},
    {0, 19, (void *)0x402017c},
    {0, 16, (void *)0x760404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)724},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x9b0402},
    {0, 17, (void *)0x2da02d9},
    {0, 19, (void *)0x4020184},
    {0, 16, (void *)0x9b0403},
    {0, 17, (void *)0x2dd02dc},
    {0, 19, (void *)0x4020184},
    {0, 16, (void *)0x9b0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)736},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {0, 11, (void *)708},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x770402},
    {0, 17, (void *)0x2e802e7},
    {0, 19, (void *)0x402018b},
    {0, 16, (void *)0x770403},
    {0, 17, (void *)0x2eb02ea},
    {0, 19, (void *)0x402018b},
    {0, 16, (void *)0x770404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)750},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1175},
    {0, 11, (void *)100},
    {5, 14, (void *)_v1211},
    {0, 11, (void *)124},
    {5, 14, (void *)_v983},
    {0, 6, (void *)757},
    {0, 3, (void *)_v983},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x9a0402},
    {0, 17, (void *)0x2fb02fa},
    {0, 19, (void *)0x4020195},
    {0, 16, (void *)0x9a0403},
    {0, 17, (void *)0x2fe02fd},
    {0, 19, (void *)0x4020195},
    {0, 16, (void *)0x9a0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)769},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1206},
    {0, 6, (void *)772},
    {0, 3, (void *)_v1206},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x180402},
    {0, 17, (void *)0x30a0309},
    {0, 19, (void *)0x402019e},
    {0, 16, (void *)0x180403},
    {0, 17, (void *)0x30d030c},
    {0, 19, (void *)0x402019e},
    {0, 16, (void *)0x180404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)784},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1903fe},
    {0, 17, (void *)0x3160315},
    {0, 19, (void *)0x3fe01a5},
    {0, 16, (void *)0x1903ff},
    {0, 17, (void *)0x3190318},
    {0, 19, (void *)0x3fe01a5},
    {0, 16, (void *)0x190400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)796},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1a03fe},
    {0, 17, (void *)0x3220321},
    {0, 19, (void *)0x3fe01a8},
    {0, 16, (void *)0x1a03ff},
    {0, 17, (void *)0x3250324},
    {0, 19, (void *)0x3fe01a8},
    {0, 16, (void *)0x1a0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)808},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1b03fe},
    {0, 17, (void *)0x32e032d},
    {0, 19, (void *)0x3fe01ab},
    {0, 16, (void *)0x1b03ff},
    {0, 17, (void *)0x3310330},
    {0, 19, (void *)0x3fe01ab},
    {0, 16, (void *)0x1b0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)820},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xaf03fe},
    {0, 17, (void *)0x33a0339},
    {0, 19, (void *)0x3fe01ae},
    {0, 16, (void *)0xaf03ff},
    {0, 17, (void *)0x33d033c},
    {0, 19, (void *)0x3fe01ae},
    {0, 16, (void *)0xaf0400},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1c0402},
    {0, 17, (void *)0x3430342},
    {0, 19, (void *)0x40201b1},
    {0, 16, (void *)0x1c0403},
    {0, 17, (void *)0x3460345},
    {0, 19, (void *)0x40201b1},
    {0, 16, (void *)0x1c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)841},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1d03fe},
    {0, 17, (void *)0x34f034e},
    {0, 19, (void *)0x3fe01b4},
    {0, 16, (void *)0x1d03ff},
    {0, 17, (void *)0x3520351},
    {0, 19, (void *)0x3fe01b4},
    {0, 16, (void *)0x1d0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)853},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xaa03fe},
    {0, 17, (void *)0x35b035a},
    {0, 19, (void *)0x3fe01b7},
    {0, 16, (void *)0xaa03ff},
    {0, 17, (void *)0x35e035d},
    {0, 19, (void *)0x3fe01b7},
    {0, 16, (void *)0xaa0400},
    {5, 14, (void *)_v1006},
    {0, 6, (void *)865},
    {0, 3, (void *)_v1006},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1e0402},
    {0, 17, (void *)0x3670366},
    {0, 19, (void *)0x40201ba},
    {0, 16, (void *)0x1e0403},
    {0, 17, (void *)0x36a0369},
    {0, 19, (void *)0x40201ba},
    {0, 16, (void *)0x1e0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)877},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xae03fe},
    {0, 17, (void *)0x3730372},
    {0, 19, (void *)0x3fe01bd},
    {0, 16, (void *)0xae03ff},
    {0, 17, (void *)0x3760375},
    {0, 19, (void *)0x3fe01bd},
    {0, 16, (void *)0xae0400},
    {5, 14, (void *)_v1006},
    {0, 11, (void *)864},
    {5, 14, (void *)_v665},
    {0, 11, (void *)109},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x1f0402},
    {0, 17, (void *)0x380037f},
    {0, 19, (void *)0x40201c0},
    {0, 16, (void *)0x1f0403},
    {0, 17, (void *)0x3830382},
    {0, 19, (void *)0x40201c0},
    {0, 16, (void *)0x1f0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)902},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2003fe},
    {0, 17, (void *)0x38c038b},
    {0, 19, (void *)0x3fe01c3},
    {0, 16, (void *)0x2003ff},
    {0, 17, (void *)0x38f038e},
    {0, 19, (void *)0x3fe01c3},
    {0, 16, (void *)0x200400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)914},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa703fe},
    {0, 17, (void *)0x3980397},
    {0, 19, (void *)0x3fe01c6},
    {0, 16, (void *)0xa703ff},
    {0, 17, (void *)0x39b039a},
    {0, 19, (void *)0x3fe01c6},
    {0, 16, (void *)0xa70400},
    {5, 14, (void *)_v1006},
    {0, 11, (void *)864},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x210402},
    {0, 17, (void *)0x3a303a2},
    {0, 19, (void *)0x40201c9},
    {0, 16, (void *)0x210403},
    {0, 17, (void *)0x3a603a5},
    {0, 19, (void *)0x40201c9},
    {0, 16, (void *)0x210404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)937},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2203fe},
    {0, 17, (void *)0x3af03ae},
    {0, 19, (void *)0x3fe01cc},
    {0, 16, (void *)0x2203ff},
    {0, 17, (void *)0x3b203b1},
    {0, 19, (void *)0x3fe01cc},
    {0, 16, (void *)0x220400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)949},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xac03fe},
    {0, 17, (void *)0x3bb03ba},
    {0, 19, (void *)0x3fe01cf},
    {0, 16, (void *)0xac03ff},
    {0, 17, (void *)0x3be03bd},
    {0, 19, (void *)0x3fe01cf},
    {0, 16, (void *)0xac0400},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x870402},
    {0, 17, (void *)0x3c403c3},
    {0, 19, (void *)0x40201d2},
    {0, 16, (void *)0x870403},
    {0, 17, (void *)0x3c703c6},
    {0, 19, (void *)0x40201d2},
    {0, 16, (void *)0x870404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)970},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x980402},
    {0, 17, (void *)0x3d003cf},
    {0, 19, (void *)0x40201d8},
    {0, 16, (void *)0x980403},
    {0, 17, (void *)0x3d303d2},
    {0, 19, (void *)0x40201d8},
    {0, 16, (void *)0x980404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)982},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)984},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)986},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)988},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)990},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)992},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)994},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x940402},
    {0, 17, (void *)0x3f403f3},
    {0, 19, (void *)0x40201de},
    {0, 16, (void *)0x940403},
    {0, 17, (void *)0x3f703f6},
    {0, 19, (void *)0x40201de},
    {0, 16, (void *)0x940404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1018},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x230402},
    {0, 17, (void *)0x40003ff},
    {0, 19, (void *)0x40201e8},
    {0, 16, (void *)0x230403},
    {0, 17, (void *)0x4030402},
    {0, 19, (void *)0x40201e8},
    {0, 16, (void *)0x230404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1030},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {0, 3, (void *)_v1229},
    {0, 16, (void *)0x240406},
    {0, 17, (void *)0x40c040b},
    {0, 19, (void *)0x40601ed},
    {0, 16, (void *)0x240407},
    {0, 17, (void *)0x40f040e},
    {0, 19, (void *)0x40601ed},
    {0, 16, (void *)0x240408},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1042},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2503fe},
    {0, 17, (void *)0x4180417},
    {0, 19, (void *)0x3fe01f0},
    {0, 16, (void *)0x2503ff},
    {0, 17, (void *)0x41b041a},
    {0, 19, (void *)0x3fe01f0},
    {0, 16, (void *)0x250400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1054},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2603fe},
    {0, 17, (void *)0x4240423},
    {0, 19, (void *)0x3fe01f3},
    {0, 16, (void *)0x2603ff},
    {0, 17, (void *)0x4270426},
    {0, 19, (void *)0x3fe01f3},
    {0, 16, (void *)0x260400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1066},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2703fe},
    {0, 17, (void *)0x430042f},
    {0, 19, (void *)0x3fe01f6},
    {0, 16, (void *)0x2703ff},
    {0, 17, (void *)0x4330432},
    {0, 19, (void *)0x3fe01f6},
    {0, 16, (void *)0x270400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1078},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2803fe},
    {0, 17, (void *)0x43c043b},
    {0, 19, (void *)0x3fe01f9},
    {0, 16, (void *)0x2803ff},
    {0, 17, (void *)0x43f043e},
    {0, 19, (void *)0x3fe01f9},
    {0, 16, (void *)0x280400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1090},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2903fe},
    {0, 17, (void *)0x4480447},
    {0, 19, (void *)0x3fe01fc},
    {0, 16, (void *)0x2903ff},
    {0, 17, (void *)0x44b044a},
    {0, 19, (void *)0x3fe01fc},
    {0, 16, (void *)0x290400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1102},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2a0402},
    {0, 17, (void *)0x4540453},
    {0, 19, (void *)0x40201ff},
    {0, 16, (void *)0x2a0403},
    {0, 17, (void *)0x4570456},
    {0, 19, (void *)0x40201ff},
    {0, 16, (void *)0x2a0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1114},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2b03fe},
    {0, 17, (void *)0x460045f},
    {0, 19, (void *)0x3fe0204},
    {0, 16, (void *)0x2b03ff},
    {0, 17, (void *)0x4630462},
    {0, 19, (void *)0x3fe0204},
    {0, 16, (void *)0x2b0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1126},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2c0402},
    {0, 17, (void *)0x46c046b},
    {0, 19, (void *)0x4020207},
    {0, 16, (void *)0x2c0403},
    {0, 17, (void *)0x46f046e},
    {0, 19, (void *)0x4020207},
    {0, 16, (void *)0x2c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1138},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2d03fe},
    {0, 17, (void *)0x4780477},
    {0, 19, (void *)0x3fe020c},
    {0, 16, (void *)0x2d03ff},
    {0, 17, (void *)0x47b047a},
    {0, 19, (void *)0x3fe020c},
    {0, 16, (void *)0x2d0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1150},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2e03fe},
    {0, 17, (void *)0x4840483},
    {0, 19, (void *)0x3fe020f},
    {0, 16, (void *)0x2e03ff},
    {0, 17, (void *)0x4870486},
    {0, 19, (void *)0x3fe020f},
    {0, 16, (void *)0x2e0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1162},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x2f03fe},
    {0, 17, (void *)0x490048f},
    {0, 19, (void *)0x3fe0212},
    {0, 16, (void *)0x2f03ff},
    {0, 17, (void *)0x4930492},
    {0, 19, (void *)0x3fe0212},
    {0, 16, (void *)0x2f0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1174},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v748},
    {0, 6, (void *)1177},
    {0, 1, (void *)&_v749},
    {5, 14, (void *)_v750},
    {0, 6, (void *)1180},
    {0, 1, (void *)&_v751},
    {5, 14, (void *)_v1212},
    {0, 11, (void *)103},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x820402},
    {0, 17, (void *)0x4a404a3},
    {0, 19, (void *)0x4020217},
    {0, 16, (void *)0x820403},
    {0, 17, (void *)0x4a704a6},
    {0, 19, (void *)0x4020217},
    {0, 16, (void *)0x820404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1194},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x300402},
    {0, 17, (void *)0x4b004af},
    {0, 19, (void *)0x402021a},
    {0, 16, (void *)0x300403},
    {0, 17, (void *)0x4b304b2},
    {0, 19, (void *)0x402021a},
    {0, 16, (void *)0x300404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1206},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3103fe},
    {0, 17, (void *)0x4bc04bb},
    {0, 19, (void *)0x3fe021d},
    {0, 16, (void *)0x3103ff},
    {0, 17, (void *)0x4bf04be},
    {0, 19, (void *)0x3fe021d},
    {0, 16, (void *)0x310400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1218},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3203fe},
    {0, 17, (void *)0x4c804c7},
    {0, 19, (void *)0x3fe0220},
    {0, 16, (void *)0x3203ff},
    {0, 17, (void *)0x4cb04ca},
    {0, 19, (void *)0x3fe0220},
    {0, 16, (void *)0x320400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x330402},
    {0, 17, (void *)0x4d304d2},
    {0, 19, (void *)0x4020223},
    {0, 16, (void *)0x330403},
    {0, 17, (void *)0x4d604d5},
    {0, 19, (void *)0x4020223},
    {0, 16, (void *)0x330404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1241},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x340402},
    {0, 17, (void *)0x4e104e0},
    {0, 19, (void *)0x402022b},
    {0, 16, (void *)0x340403},
    {0, 17, (void *)0x4e404e3},
    {0, 19, (void *)0x402022b},
    {0, 16, (void *)0x340404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1255},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3503fe},
    {0, 17, (void *)0x4ed04ec},
    {0, 19, (void *)0x3fe0232},
    {0, 16, (void *)0x3503ff},
    {0, 17, (void *)0x4f004ef},
    {0, 19, (void *)0x3fe0232},
    {0, 16, (void *)0x350400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1267},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xb003fe},
    {0, 17, (void *)0x4f904f8},
    {0, 19, (void *)0x3fe0235},
    {0, 16, (void *)0xb003ff},
    {0, 17, (void *)0x4fc04fb},
    {0, 19, (void *)0x3fe0235},
    {0, 16, (void *)0xb00400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x360402},
    {0, 17, (void *)0x5040503},
    {0, 19, (void *)0x4020238},
    {0, 16, (void *)0x360403},
    {0, 17, (void *)0x5070506},
    {0, 19, (void *)0x4020238},
    {0, 16, (void *)0x360404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1290},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x370402},
    {0, 17, (void *)0x5120511},
    {0, 19, (void *)0x4020240},
    {0, 16, (void *)0x370403},
    {0, 17, (void *)0x5150514},
    {0, 19, (void *)0x4020240},
    {0, 16, (void *)0x370404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1304},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3803fe},
    {0, 17, (void *)0x51e051d},
    {0, 19, (void *)0x3fe0246},
    {0, 16, (void *)0x3803ff},
    {0, 17, (void *)0x5210520},
    {0, 19, (void *)0x3fe0246},
    {0, 16, (void *)0x380400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1316},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3903fe},
    {0, 17, (void *)0x52a0529},
    {0, 19, (void *)0x3fe0249},
    {0, 16, (void *)0x3903ff},
    {0, 17, (void *)0x52d052c},
    {0, 19, (void *)0x3fe0249},
    {0, 16, (void *)0x390400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1328},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3a0402},
    {0, 17, (void *)0x5360535},
    {0, 19, (void *)0x402024c},
    {0, 16, (void *)0x3a0403},
    {0, 17, (void *)0x5390538},
    {0, 19, (void *)0x402024c},
    {0, 16, (void *)0x3a0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1340},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3b0402},
    {0, 17, (void *)0x5420541},
    {0, 19, (void *)0x4020251},
    {0, 16, (void *)0x3b0403},
    {0, 17, (void *)0x5450544},
    {0, 19, (void *)0x4020251},
    {0, 16, (void *)0x3b0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1352},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3c03fe},
    {0, 17, (void *)0x54e054d},
    {0, 19, (void *)0x3fe0257},
    {0, 16, (void *)0x3c03ff},
    {0, 17, (void *)0x5510550},
    {0, 19, (void *)0x3fe0257},
    {0, 16, (void *)0x3c0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1364},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v798},
    {0, 11, (void *)115},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)1369},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)1372},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)1375},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3d0402},
    {0, 17, (void *)0x5650564},
    {0, 19, (void *)0x402025a},
    {0, 16, (void *)0x3d0403},
    {0, 17, (void *)0x5680567},
    {0, 19, (void *)0x402025a},
    {0, 16, (void *)0x3d0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1387},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3e03fe},
    {0, 17, (void *)0x5710570},
    {0, 19, (void *)0x3fe025d},
    {0, 16, (void *)0x3e03ff},
    {0, 17, (void *)0x5740573},
    {0, 19, (void *)0x3fe025d},
    {0, 16, (void *)0x3e0400},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)1368},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x3f0402},
    {0, 17, (void *)0x57e057d},
    {0, 19, (void *)0x4020260},
    {0, 16, (void *)0x3f0403},
    {0, 17, (void *)0x5810580},
    {0, 19, (void *)0x4020260},
    {0, 16, (void *)0x3f0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1412},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x400402},
    {0, 17, (void *)0x58c058b},
    {0, 19, (void *)0x4020268},
    {0, 16, (void *)0x400403},
    {0, 17, (void *)0x58f058e},
    {0, 19, (void *)0x4020268},
    {0, 16, (void *)0x400404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1426},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4103fe},
    {0, 17, (void *)0x5980597},
    {0, 19, (void *)0x3fe026f},
    {0, 16, (void *)0x4103ff},
    {0, 17, (void *)0x59b059a},
    {0, 19, (void *)0x3fe026f},
    {0, 16, (void *)0x410400},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)1371},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x420402},
    {0, 17, (void *)0x5a505a4},
    {0, 19, (void *)0x4020272},
    {0, 16, (void *)0x420403},
    {0, 17, (void *)0x5a805a7},
    {0, 19, (void *)0x4020272},
    {0, 16, (void *)0x420404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1451},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x430402},
    {0, 17, (void *)0x5b305b2},
    {0, 19, (void *)0x4020279},
    {0, 16, (void *)0x430403},
    {0, 17, (void *)0x5b605b5},
    {0, 19, (void *)0x4020279},
    {0, 16, (void *)0x430404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1465},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4403fe},
    {0, 17, (void *)0x5bf05be},
    {0, 19, (void *)0x3fe0280},
    {0, 16, (void *)0x4403ff},
    {0, 17, (void *)0x5c205c1},
    {0, 19, (void *)0x3fe0280},
    {0, 16, (void *)0x440400},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)1374},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x450402},
    {0, 17, (void *)0x5cc05cb},
    {0, 19, (void *)0x4020283},
    {0, 16, (void *)0x450403},
    {0, 17, (void *)0x5cf05ce},
    {0, 19, (void *)0x4020283},
    {0, 16, (void *)0x450404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1490},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x460402},
    {0, 17, (void *)0x5da05d9},
    {0, 19, (void *)0x402028a},
    {0, 16, (void *)0x460403},
    {0, 17, (void *)0x5dd05dc},
    {0, 19, (void *)0x402028a},
    {0, 16, (void *)0x460404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1504},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4703fe},
    {0, 17, (void *)0x5e605e5},
    {0, 19, (void *)0x3fe0290},
    {0, 16, (void *)0x4703ff},
    {0, 17, (void *)0x5e905e8},
    {0, 19, (void *)0x3fe0290},
    {0, 16, (void *)0x470400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1516},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xb103fe},
    {0, 17, (void *)0x5f205f1},
    {0, 19, (void *)0x3fe0293},
    {0, 16, (void *)0xb103ff},
    {0, 17, (void *)0x5f505f4},
    {0, 19, (void *)0x3fe0293},
    {0, 16, (void *)0xb10400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x480402},
    {0, 17, (void *)0x5fd05fc},
    {0, 19, (void *)0x4020296},
    {0, 16, (void *)0x480403},
    {0, 17, (void *)0x60005ff},
    {0, 19, (void *)0x4020296},
    {0, 16, (void *)0x480404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1539},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x490402},
    {0, 17, (void *)0x60b060a},
    {0, 19, (void *)0x402029e},
    {0, 16, (void *)0x490403},
    {0, 17, (void *)0x60e060d},
    {0, 19, (void *)0x402029e},
    {0, 16, (void *)0x490404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1553},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa803fe},
    {0, 17, (void *)0x6170616},
    {0, 19, (void *)0x3fe02a4},
    {0, 16, (void *)0xa803ff},
    {0, 17, (void *)0x61a0619},
    {0, 19, (void *)0x3fe02a4},
    {0, 16, (void *)0xa80400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4a0402},
    {0, 17, (void *)0x6220621},
    {0, 19, (void *)0x40202a7},
    {0, 16, (void *)0x4a0403},
    {0, 17, (void *)0x6250624},
    {0, 19, (void *)0x40202a7},
    {0, 16, (void *)0x4a0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1576},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4b0402},
    {0, 17, (void *)0x630062f},
    {0, 19, (void *)0x40202ad},
    {0, 16, (void *)0x4b0403},
    {0, 17, (void *)0x6330632},
    {0, 19, (void *)0x40202ad},
    {0, 16, (void *)0x4b0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1590},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xab03fe},
    {0, 17, (void *)0x63c063b},
    {0, 19, (void *)0x3fe02b3},
    {0, 16, (void *)0xab03ff},
    {0, 17, (void *)0x63f063e},
    {0, 19, (void *)0x3fe02b3},
    {0, 16, (void *)0xab0400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4c0402},
    {0, 17, (void *)0x6470646},
    {0, 19, (void *)0x40202b6},
    {0, 16, (void *)0x4c0403},
    {0, 17, (void *)0x64a0649},
    {0, 19, (void *)0x40202b6},
    {0, 16, (void *)0x4c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1613},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4d0402},
    {0, 17, (void *)0x6550654},
    {0, 19, (void *)0x40202bb},
    {0, 16, (void *)0x4d0403},
    {0, 17, (void *)0x6580657},
    {0, 19, (void *)0x40202bb},
    {0, 16, (void *)0x4d0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1627},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4e03fe},
    {0, 17, (void *)0x6610660},
    {0, 19, (void *)0x3fe02c0},
    {0, 16, (void *)0x4e03ff},
    {0, 17, (void *)0x6640663},
    {0, 19, (void *)0x3fe02c0},
    {0, 16, (void *)0x4e0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1639},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x4f03fe},
    {0, 17, (void *)0x66d066c},
    {0, 19, (void *)0x3fe02c3},
    {0, 16, (void *)0x4f03ff},
    {0, 17, (void *)0x670066f},
    {0, 19, (void *)0x3fe02c3},
    {0, 16, (void *)0x4f0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1651},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5003fe},
    {0, 17, (void *)0x6790678},
    {0, 19, (void *)0x3fe02c6},
    {0, 16, (void *)0x5003ff},
    {0, 17, (void *)0x67c067b},
    {0, 19, (void *)0x3fe02c6},
    {0, 16, (void *)0x500400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x510402},
    {0, 17, (void *)0x6840683},
    {0, 19, (void *)0x40202c9},
    {0, 16, (void *)0x510403},
    {0, 17, (void *)0x6870686},
    {0, 19, (void *)0x40202c9},
    {0, 16, (void *)0x510404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1674},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5203fe},
    {0, 17, (void *)0x690068f},
    {0, 19, (void *)0x3fe02cc},
    {0, 16, (void *)0x5203ff},
    {0, 17, (void *)0x6930692},
    {0, 19, (void *)0x3fe02cc},
    {0, 16, (void *)0x520400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1686},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5303fe},
    {0, 17, (void *)0x69c069b},
    {0, 19, (void *)0x3fe02cf},
    {0, 16, (void *)0x5303ff},
    {0, 17, (void *)0x69f069e},
    {0, 19, (void *)0x3fe02cf},
    {0, 16, (void *)0x530400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1698},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5403fe},
    {0, 17, (void *)0x6a806a7},
    {0, 19, (void *)0x3fe02d2},
    {0, 16, (void *)0x5403ff},
    {0, 17, (void *)0x6ab06aa},
    {0, 19, (void *)0x3fe02d2},
    {0, 16, (void *)0x540400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1710},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5503fe},
    {0, 17, (void *)0x6b406b3},
    {0, 19, (void *)0x3fe02d5},
    {0, 16, (void *)0x5503ff},
    {0, 17, (void *)0x6b706b6},
    {0, 19, (void *)0x3fe02d5},
    {0, 16, (void *)0x550400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x560402},
    {0, 17, (void *)0x6bf06be},
    {0, 19, (void *)0x40202d8},
    {0, 16, (void *)0x560403},
    {0, 17, (void *)0x6c206c1},
    {0, 19, (void *)0x40202d8},
    {0, 16, (void *)0x560404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1733},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x570402},
    {0, 17, (void *)0x6cd06cc},
    {0, 19, (void *)0x40202df},
    {0, 16, (void *)0x570403},
    {0, 17, (void *)0x6d006cf},
    {0, 19, (void *)0x40202df},
    {0, 16, (void *)0x570404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1747},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5803fe},
    {0, 17, (void *)0x6d906d8},
    {0, 19, (void *)0x3fe02e5},
    {0, 16, (void *)0x5803ff},
    {0, 17, (void *)0x6dc06db},
    {0, 19, (void *)0x3fe02e5},
    {0, 16, (void *)0x580400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1759},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5903fe},
    {0, 17, (void *)0x6e506e4},
    {0, 19, (void *)0x3fe02e8},
    {0, 16, (void *)0x5903ff},
    {0, 17, (void *)0x6e806e7},
    {0, 19, (void *)0x3fe02e8},
    {0, 16, (void *)0x590400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1771},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5a03fe},
    {0, 17, (void *)0x6f106f0},
    {0, 19, (void *)0x3fe02eb},
    {0, 16, (void *)0x5a03ff},
    {0, 17, (void *)0x6f406f3},
    {0, 19, (void *)0x3fe02eb},
    {0, 16, (void *)0x5a0400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5b0402},
    {0, 17, (void *)0x6fc06fb},
    {0, 19, (void *)0x40202ee},
    {0, 16, (void *)0x5b0403},
    {0, 17, (void *)0x6ff06fe},
    {0, 19, (void *)0x40202ee},
    {0, 16, (void *)0x5b0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1794},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5c0402},
    {0, 17, (void *)0x70a0709},
    {0, 19, (void *)0x40202f5},
    {0, 16, (void *)0x5c0403},
    {0, 17, (void *)0x70d070c},
    {0, 19, (void *)0x40202f5},
    {0, 16, (void *)0x5c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1808},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5d03fe},
    {0, 17, (void *)0x7160715},
    {0, 19, (void *)0x3fe02fb},
    {0, 16, (void *)0x5d03ff},
    {0, 17, (void *)0x7190718},
    {0, 19, (void *)0x3fe02fb},
    {0, 16, (void *)0x5d0400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1820},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5e03fe},
    {0, 17, (void *)0x7220721},
    {0, 19, (void *)0x3fe02fe},
    {0, 16, (void *)0x5e03ff},
    {0, 17, (void *)0x7250724},
    {0, 19, (void *)0x3fe02fe},
    {0, 16, (void *)0x5e0400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v920},
    {0, 3, (void *)_v920},
    {5, 14, (void *)_v920},
    {0, 3, (void *)_v920},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x5f0402},
    {0, 17, (void *)0x7310730},
    {0, 19, (void *)0x4020301},
    {0, 16, (void *)0x5f0403},
    {0, 17, (void *)0x7340733},
    {0, 19, (void *)0x4020301},
    {0, 16, (void *)0x5f0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1847},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6003fe},
    {0, 17, (void *)0x73d073c},
    {0, 19, (void *)0x3fe0304},
    {0, 16, (void *)0x6003ff},
    {0, 17, (void *)0x740073f},
    {0, 19, (void *)0x3fe0304},
    {0, 16, (void *)0x600400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1859},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6103fe},
    {0, 17, (void *)0x7490748},
    {0, 19, (void *)0x3fe0307},
    {0, 16, (void *)0x6103ff},
    {0, 17, (void *)0x74c074b},
    {0, 19, (void *)0x3fe0307},
    {0, 16, (void *)0x610400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1871},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6203fe},
    {0, 17, (void *)0x7550754},
    {0, 19, (void *)0x3fe030a},
    {0, 16, (void *)0x6203ff},
    {0, 17, (void *)0x7580757},
    {0, 19, (void *)0x3fe030a},
    {0, 16, (void *)0x620400},
    {5, 14, (void *)_v1229},
    {0, 6, (void *)1883},
    {0, 3, (void *)_v1229},
    {5, 14, (void *)_v949},
    {0, 6, (void *)1886},
    {0, 1, (void *)&_v934},
    {5, 14, (void *)_v949},
    {0, 6, (void *)1889},
    {0, 1, (void *)&_v936},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x630402},
    {0, 17, (void *)0x7670766},
    {0, 19, (void *)0x402030d},
    {0, 16, (void *)0x630403},
    {0, 17, (void *)0x76a0769},
    {0, 19, (void *)0x402030d},
    {0, 16, (void *)0x630404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1901},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v955},
    {0, 6, (void *)1904},
    {0, 20, (void *)0x771},
    {0, 3, (void *)_v955},
    {5, 14, (void *)_v955},
    {0, 6, (void *)1908},
    {0, 20, (void *)0x775},
    {0, 3, (void *)_v955},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x640402},
    {0, 17, (void *)0x77b077a},
    {0, 19, (void *)0x4020314},
    {0, 16, (void *)0x640403},
    {0, 17, (void *)0x77e077d},
    {0, 19, (void *)0x4020314},
    {0, 16, (void *)0x640404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1921},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v949},
    {0, 6, (void *)1926},
    {0, 1, (void *)&_v950},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa40402},
    {0, 17, (void *)0x78c078b},
    {0, 19, (void *)0x402031b},
    {0, 16, (void *)0xa40403},
    {0, 17, (void *)0x78f078e},
    {0, 19, (void *)0x402031b},
    {0, 16, (void *)0xa40404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1938},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v955},
    {0, 6, (void *)1941},
    {0, 20, (void *)0x796},
    {0, 3, (void *)_v955},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa50402},
    {0, 17, (void *)0x79c079b},
    {0, 19, (void *)0x4020320},
    {0, 16, (void *)0xa50403},
    {0, 17, (void *)0x79f079e},
    {0, 19, (void *)0x4020320},
    {0, 16, (void *)0xa50404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1954},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)1923},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x650402},
    {0, 17, (void *)0x7aa07a9},
    {0, 19, (void *)0x4020327},
    {0, 16, (void *)0x650403},
    {0, 17, (void *)0x7ad07ac},
    {0, 19, (void *)0x4020327},
    {0, 16, (void *)0x650404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1968},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x660402},
    {0, 17, (void *)0x7b607b5},
    {0, 19, (void *)0x402032a},
    {0, 16, (void *)0x660403},
    {0, 17, (void *)0x7b907b8},
    {0, 19, (void *)0x402032a},
    {0, 16, (void *)0x660404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1980},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1006},
    {0, 6, (void *)1983},
    {0, 3, (void *)_v1006},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x690402},
    {0, 17, (void *)0x7c507c4},
    {0, 19, (void *)0x402032d},
    {0, 16, (void *)0x690403},
    {0, 17, (void *)0x7c807c7},
    {0, 19, (void *)0x402032d},
    {0, 16, (void *)0x690404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)1995},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1216},
    {0, 6, (void *)1998},
    {0, 3, (void *)_v1216},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x9c0402},
    {0, 17, (void *)0x7d407d3},
    {0, 19, (void *)0x4020332},
    {0, 16, (void *)0x9c0403},
    {0, 17, (void *)0x7d707d6},
    {0, 19, (void *)0x4020332},
    {0, 16, (void *)0x9c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2010},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1216},
    {0, 6, (void *)2013},
    {0, 3, (void *)_v1216},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x9d0402},
    {0, 17, (void *)0x7e307e2},
    {0, 19, (void *)0x4020337},
    {0, 16, (void *)0x9d0403},
    {0, 17, (void *)0x7e607e5},
    {0, 19, (void *)0x4020337},
    {0, 16, (void *)0x9d0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2025},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1007},
    {0, 20, (void *)0x7ec},
    {0, 3, (void *)_v1007},
    {5, 14, (void *)_v983},
    {0, 11, (void *)756},
    {5, 14, (void *)_v1021},
    {0, 3, (void *)_v1021},
    {5, 14, (void *)_v1020},
    {0, 20, (void *)0x7f3},
    {0, 3, (void *)_v1020},
    {5, 14, (void *)_v1006},
    {0, 11, (void *)1982},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6a0402},
    {0, 17, (void *)0x7fb07fa},
    {0, 19, (void *)0x4020340},
    {0, 16, (void *)0x6a0403},
    {0, 17, (void *)0x7fe07fd},
    {0, 19, (void *)0x4020340},
    {0, 16, (void *)0x6a0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2049},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v993},
    {0, 6, (void *)2052},
    {0, 20, (void *)0x805},
    {0, 3, (void *)_v993},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6b0402},
    {0, 17, (void *)0x80b080a},
    {0, 19, (void *)0x4020345},
    {0, 16, (void *)0x6b0403},
    {0, 17, (void *)0x80e080d},
    {0, 19, (void *)0x4020345},
    {0, 16, (void *)0x6b0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2065},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1006},
    {0, 6, (void *)2068},
    {0, 3, (void *)_v1006},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)2071},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1273},
    {0, 6, (void *)2074},
    {0, 3, (void *)_v1273},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6c0402},
    {0, 17, (void *)0x820081f},
    {0, 19, (void *)0x402034b},
    {0, 16, (void *)0x6c0403},
    {0, 17, (void *)0x8230822},
    {0, 19, (void *)0x402034b},
    {0, 16, (void *)0x6c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2086},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1006},
    {0, 11, (void *)2067},
    {5, 14, (void *)_v1007},
    {0, 11, (void *)2027},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6d0402},
    {0, 17, (void *)0x830082f},
    {0, 19, (void *)0x402034e},
    {0, 16, (void *)0x6d0403},
    {0, 17, (void *)0x8330832},
    {0, 19, (void *)0x402034e},
    {0, 16, (void *)0x6d0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2102},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)2070},
    {5, 14, (void *)_v1013},
    {0, 20, (void *)0x83b},
    {0, 3, (void *)_v1013},
    {5, 14, (void *)_v1015},
    {0, 20, (void *)0x83e},
    {0, 3, (void *)_v1015},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6e0402},
    {0, 17, (void *)0x8440843},
    {0, 19, (void *)0x4020354},
    {0, 16, (void *)0x6e0403},
    {0, 17, (void *)0x8470846},
    {0, 19, (void *)0x4020354},
    {0, 16, (void *)0x6e0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2122},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1273},
    {0, 11, (void *)2073},
    {5, 14, (void *)_v1020},
    {0, 11, (void *)2034},
    {5, 14, (void *)_v1021},
    {0, 11, (void *)2032},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x700402},
    {0, 17, (void *)0x8560855},
    {0, 19, (void *)0x402035a},
    {0, 16, (void *)0x700403},
    {0, 17, (void *)0x8590858},
    {0, 19, (void *)0x402035a},
    {0, 16, (void *)0x700404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2140},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x6f0402},
    {0, 17, (void *)0x8620861},
    {0, 19, (void *)0x4020360},
    {0, 16, (void *)0x6f0403},
    {0, 17, (void *)0x8650864},
    {0, 19, (void *)0x4020360},
    {0, 16, (void *)0x6f0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2152},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1032},
    {0, 3, (void *)_v1032},
    {5, 14, (void *)_v1033},
    {0, 3, (void *)_v1033},
    {5, 14, (void *)_v1032},
    {0, 11, (void *)2154},
    {5, 14, (void *)_v1033},
    {0, 11, (void *)2156},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x720402},
    {0, 17, (void *)0x8760875},
    {0, 19, (void *)0x4020363},
    {0, 16, (void *)0x720403},
    {0, 17, (void *)0x8790878},
    {0, 19, (void *)0x4020363},
    {0, 16, (void *)0x720404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2172},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1037},
    {0, 11, (void *)134},
    {5, 14, (void *)_v1232},
    {0, 6, (void *)2177},
    {0, 3, (void *)_v1232},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x730402},
    {0, 17, (void *)0x8870886},
    {0, 19, (void *)0x4020366},
    {0, 16, (void *)0x730403},
    {0, 17, (void *)0x88a0889},
    {0, 19, (void *)0x4020366},
    {0, 16, (void *)0x730404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2189},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {0, 11, (void *)163},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x740402},
    {0, 17, (void *)0x8950894},
    {0, 19, (void *)0x4020369},
    {0, 16, (void *)0x740403},
    {0, 17, (void *)0x8980897},
    {0, 19, (void *)0x4020369},
    {0, 16, (void *)0x740404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2203},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)2176},
    {5, 14, (void *)_v1220},
    {0, 6, (void *)2208},
    {0, 1, (void *)&_v1049},
    {5, 14, (void *)_v1053},
    {0, 3, (void *)_v1053},
    {5, 14, (void *)_v1053},
    {0, 6, (void *)2213},
    {0, 3, (void *)_v1053},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x750402},
    {0, 17, (void *)0x8ab08aa},
    {0, 19, (void *)0x4020370},
    {0, 16, (void *)0x750403},
    {0, 17, (void *)0x8ae08ad},
    {0, 19, (void *)0x4020370},
    {0, 16, (void *)0x750404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2225},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1057},
    {0, 11, (void *)246},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x7a03fe},
    {0, 17, (void *)0x8b908b8},
    {0, 19, (void *)0x3fe0377},
    {0, 16, (void *)0x7a03ff},
    {0, 17, (void *)0x8bc08bb},
    {0, 19, (void *)0x3fe0377},
    {0, 16, (void *)0x7a0400},
    {5, 14, (void *)_v1059},
    {0, 11, (void *)309},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x7b0402},
    {0, 17, (void *)0x8c408c3},
    {0, 19, (void *)0x402037a},
    {0, 16, (void *)0x7b0403},
    {0, 17, (void *)0x8c708c6},
    {0, 19, (void *)0x402037a},
    {0, 16, (void *)0x7b0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2250},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1220},
    {0, 6, (void *)2253},
    {0, 1, (void *)&_v1064},
    {5, 14, (void *)_v1070},
    {0, 20, (void *)0x8d0},
    {2, 3, (void *)_v1068},
    {2, 1, (void *)&_v327},
    {2, 1, (void *)&_v328},
    {2, 1, (void *)&_v329},
    {2, 1, (void *)&_v330},
    {2, 1, (void *)&_v331},
    {2, 1, (void *)&_v332},
    {2, 1, (void *)&_v333},
    {2, 1, (void *)&_v334},
    {0, 1, (void *)&_v335},
    {5, 14, (void *)_v1070},
    {0, 20, (void *)0x8dc},
    {2, 3, (void *)_v1068},
    {2, 1, (void *)&_v318},
    {2, 1, (void *)&_v319},
    {2, 1, (void *)&_v320},
    {2, 1, (void *)&_v321},
    {2, 1, (void *)&_v322},
    {2, 1, (void *)&_v323},
    {2, 1, (void *)&_v324},
    {2, 1, (void *)&_v325},
    {0, 1, (void *)&_v326},
    {5, 14, (void *)_v1070},
    {0, 11, (void *)2255},
    {5, 14, (void *)_v1070},
    {0, 11, (void *)2267},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x7d0402},
    {0, 17, (void *)0x8ef08ee},
    {0, 19, (void *)0x402037d},
    {0, 16, (void *)0x7d0403},
    {0, 17, (void *)0x8f208f1},
    {0, 19, (void *)0x402037d},
    {0, 16, (void *)0x7d0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2293},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x7c0402},
    {0, 17, (void *)0x8fb08fa},
    {0, 19, (void *)0x4020384},
    {0, 16, (void *)0x7c0403},
    {0, 17, (void *)0x8fe08fd},
    {0, 19, (void *)0x4020384},
    {0, 16, (void *)0x7c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2305},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1077},
    {0, 6, (void *)2308},
    {0, 1, (void *)&_v1078},
    {5, 14, (void *)_v1228},
    {0, 11, (void *)678},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x7e0402},
    {0, 17, (void *)0x90c090b},
    {0, 19, (void *)0x4020387},
    {0, 16, (void *)0x7e0403},
    {0, 17, (void *)0x90f090e},
    {0, 19, (void *)0x4020387},
    {0, 16, (void *)0x7e0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2322},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1212},
    {0, 11, (void *)103},
    {5, 14, (void *)_v1100},
    {0, 6, (void *)2327},
    {0, 3, (void *)_v1100},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x7f0402},
    {0, 17, (void *)0x91d091c},
    {0, 19, (void *)0x402038a},
    {0, 16, (void *)0x7f0403},
    {0, 17, (void *)0x920091f},
    {0, 19, (void *)0x402038a},
    {0, 16, (void *)0x7f0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2339},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1097},
    {0, 11, (void *)249},
    {5, 14, (void *)_v1100},
    {0, 11, (void *)2326},
    {5, 14, (void *)_v1212},
    {0, 11, (void *)103},
    {5, 14, (void *)_v1100},
    {0, 6, (void *)2348},
    {0, 3, (void *)_v1100},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x800402},
    {0, 17, (void *)0x9320931},
    {0, 19, (void *)0x4020390},
    {0, 16, (void *)0x800403},
    {0, 17, (void *)0x9350934},
    {0, 19, (void *)0x4020390},
    {0, 16, (void *)0x800404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2360},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1097},
    {0, 11, (void *)252},
    {5, 14, (void *)_v1211},
    {0, 6, (void *)2365},
    {0, 1, (void *)&_v1099},
    {5, 14, (void *)_v1100},
    {0, 11, (void *)2347},
    {5, 14, (void *)_v1160},
    {0, 6, (void *)2370},
    {0, 1, (void *)&_v1102},
    {5, 14, (void *)_v1171},
    {0, 20, (void *)0x945},
    {0, 3, (void *)_v1171},
    {5, 14, (void *)_v1211},
    {0, 6, (void *)2376},
    {0, 1, (void *)&_v1106},
    {5, 14, (void *)_v1212},
    {0, 11, (void *)103},
    {5, 14, (void *)_v1211},
    {0, 11, (void *)2375},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x840402},
    {0, 17, (void *)0x9520951},
    {0, 19, (void *)0x4020396},
    {0, 16, (void *)0x840403},
    {0, 17, (void *)0x9550954},
    {0, 19, (void *)0x4020396},
    {0, 16, (void *)0x840404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2392},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1114},
    {0, 6, (void *)2395},
    {0, 1, (void *)&_v1113},
    {5, 14, (void *)_v1114},
    {0, 11, (void *)2394},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x850402},
    {0, 17, (void *)0x9630962},
    {0, 19, (void *)0x402039c},
    {0, 16, (void *)0x850403},
    {0, 17, (void *)0x9660965},
    {0, 19, (void *)0x402039c},
    {0, 16, (void *)0x850404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2409},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1220},
    {0, 6, (void *)2412},
    {0, 1, (void *)&_v1119},
    {5, 14, (void *)_v1122},
    {0, 3, (void *)_v1122},
    {5, 14, (void *)_v1122},
    {0, 11, (void *)2414},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x880402},
    {0, 17, (void *)0x9760975},
    {0, 19, (void *)0x402039f},
    {0, 16, (void *)0x880403},
    {0, 17, (void *)0x9790978},
    {0, 19, (void *)0x402039f},
    {0, 16, (void *)0x880404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2428},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x8a0402},
    {0, 17, (void *)0x9820981},
    {0, 19, (void *)0x40203aa},
    {0, 16, (void *)0x8a0403},
    {0, 17, (void *)0x9850984},
    {0, 19, (void *)0x40203aa},
    {0, 16, (void *)0x8a0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2440},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x8b0402},
    {0, 17, (void *)0x98e098d},
    {0, 19, (void *)0x40203ad},
    {0, 16, (void *)0x8b0403},
    {0, 17, (void *)0x9910990},
    {0, 19, (void *)0x40203ad},
    {0, 16, (void *)0x8b0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2452},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x8c0402},
    {0, 17, (void *)0x99a0999},
    {0, 19, (void *)0x40203b0},
    {0, 16, (void *)0x8c0403},
    {0, 17, (void *)0x99d099c},
    {0, 19, (void *)0x40203b0},
    {0, 16, (void *)0x8c0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2464},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1220},
    {0, 6, (void *)2467},
    {0, 1, (void *)&_v1136},
    {5, 14, (void *)_v1141},
    {0, 3, (void *)_v1141},
    {5, 14, (void *)_v1142},
    {0, 3, (void *)_v1142},
    {5, 14, (void *)_v1141},
    {0, 11, (void *)2469},
    {5, 14, (void *)_v1142},
    {0, 11, (void *)2471},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x9f0402},
    {0, 17, (void *)0x9b109b0},
    {0, 19, (void *)0x40203b5},
    {0, 16, (void *)0x9f0403},
    {0, 17, (void *)0x9b409b3},
    {0, 19, (void *)0x40203b5},
    {0, 16, (void *)0x9f0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2487},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x910402},
    {0, 17, (void *)0x9bd09bc},
    {0, 19, (void *)0x40203ba},
    {0, 16, (void *)0x910403},
    {0, 17, (void *)0x9c009bf},
    {0, 19, (void *)0x40203ba},
    {0, 16, (void *)0x910404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2499},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1220},
    {0, 11, (void *)2466},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x8d0402},
    {0, 17, (void *)0x9cb09ca},
    {0, 19, (void *)0x40203c0},
    {0, 16, (void *)0x8d0403},
    {0, 17, (void *)0x9ce09cd},
    {0, 19, (void *)0x40203c0},
    {0, 16, (void *)0x8d0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2513},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1211},
    {0, 6, (void *)2516},
    {0, 1, (void *)&_v1154},
    {5, 14, (void *)_v1161},
    {0, 20, (void *)0x9d7},
    {0, 3, (void *)_v1161},
    {5, 14, (void *)_v1159},
    {0, 6, (void *)2522},
    {0, 1, (void *)&_v1158},
    {5, 14, (void *)_v1159},
    {0, 11, (void *)2521},
    {5, 14, (void *)_v1160},
    {0, 11, (void *)2369},
    {5, 14, (void *)_v1161},
    {0, 11, (void *)2518},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x8e0402},
    {0, 17, (void *)0x9e609e5},
    {0, 19, (void *)0x40203c5},
    {0, 16, (void *)0x8e0403},
    {0, 17, (void *)0x9e909e8},
    {0, 19, (void *)0x40203c5},
    {0, 16, (void *)0x8e0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2540},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1170},
    {0, 3, (void *)_v1170},
    {5, 14, (void *)_v1170},
    {0, 3, (void *)_v1170},
    {5, 14, (void *)_v1170},
    {0, 3, (void *)_v1170},
    {5, 14, (void *)_v1171},
    {0, 11, (void *)2372},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x8f0402},
    {0, 17, (void *)0x9fa09f9},
    {0, 19, (void *)0x40203cc},
    {0, 16, (void *)0x8f0403},
    {0, 17, (void *)0x9fd09fc},
    {0, 19, (void *)0x40203cc},
    {0, 16, (void *)0x8f0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2560},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1175},
    {0, 11, (void *)100},
    {5, 14, (void *)_v1229},
    {0, 11, (void *)708},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x900402},
    {0, 17, (void *)0xa0a0a09},
    {0, 19, (void *)0x40203d1},
    {0, 16, (void *)0x900403},
    {0, 17, (void *)0xa0d0a0c},
    {0, 19, (void *)0x40203d1},
    {0, 16, (void *)0x900404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2576},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1206},
    {0, 11, (void *)771},
    {5, 14, (void *)_v1182},
    {0, 3, (void *)_v1182},
    {5, 14, (void *)_v1184},
    {0, 3, (void *)_v1184},
    {5, 14, (void *)_v1186},
    {0, 3, (void *)_v1186},
    {5, 14, (void *)_v1188},
    {0, 3, (void *)_v1188},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x920402},
    {0, 17, (void *)0xa200a1f},
    {0, 19, (void *)0x40203d8},
    {0, 16, (void *)0x920403},
    {0, 17, (void *)0xa230a22},
    {0, 19, (void *)0x40203d8},
    {0, 16, (void *)0x920404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2598},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x930402},
    {0, 17, (void *)0xa2c0a2b},
    {0, 19, (void *)0x40203db},
    {0, 16, (void *)0x930403},
    {0, 17, (void *)0xa2f0a2e},
    {0, 19, (void *)0x40203db},
    {0, 16, (void *)0x930404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2610},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1196},
    {0, 20, (void *)0xa35},
    {0, 3, (void *)_v1196},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x960402},
    {0, 17, (void *)0xa3b0a3a},
    {0, 19, (void *)0x40203de},
    {0, 16, (void *)0x960403},
    {0, 17, (void *)0xa3e0a3d},
    {0, 19, (void *)0x40203de},
    {0, 16, (void *)0x960404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2625},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1202},
    {0, 6, (void *)2628},
    {0, 3, (void *)_v1202},
    {5, 14, (void *)_v1202},
    {0, 11, (void *)2627},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x970402},
    {0, 17, (void *)0xa4c0a4b},
    {0, 19, (void *)0x40203e4},
    {0, 16, (void *)0x970403},
    {0, 17, (void *)0xa4f0a4e},
    {0, 19, (void *)0x40203e4},
    {0, 16, (void *)0x970404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2642},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1206},
    {0, 11, (void *)771},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x990402},
    {0, 17, (void *)0xa5a0a59},
    {0, 19, (void *)0x40203e9},
    {0, 16, (void *)0x990403},
    {0, 17, (void *)0xa5d0a5c},
    {0, 19, (void *)0x40203e9},
    {0, 16, (void *)0x990404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2656},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1216},
    {0, 11, (void *)1997},
    {5, 14, (void *)_v1211},
    {0, 11, (void *)124},
    {5, 14, (void *)_v1212},
    {0, 11, (void *)103},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0x9e0402},
    {0, 17, (void *)0xa6c0a6b},
    {0, 19, (void *)0x40203ef},
    {0, 16, (void *)0x9e0403},
    {0, 17, (void *)0xa6f0a6e},
    {0, 19, (void *)0x40203ef},
    {0, 16, (void *)0x9e0404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2674},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1216},
    {0, 11, (void *)2012},
    {5, 14, (void *)_v1221},
    {0, 6, (void *)2679},
    {0, 20, (void *)0xa78},
    {0, 3, (void *)_v1221},
    {5, 14, (void *)_v1220},
    {0, 11, (void *)2252},
    {5, 14, (void *)_v1220},
    {0, 11, (void *)2411},
    {5, 14, (void *)_v1221},
    {0, 11, (void *)2678},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa00402},
    {0, 17, (void *)0xa840a83},
    {0, 19, (void *)0x40203f5},
    {0, 16, (void *)0xa00403},
    {0, 17, (void *)0xa870a86},
    {0, 19, (void *)0x40203f5},
    {0, 16, (void *)0xa00404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2698},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1228},
    {0, 11, (void *)678},
    {5, 14, (void *)_v1228},
    {0, 6, (void *)2703},
    {0, 20, (void *)0xa90},
    {0, 3, (void *)_v1228},
    {5, 14, (void *)_v1228},
    {0, 11, (void *)2702},
    {5, 14, (void *)_v1229},
    {1, 11, (void *)163},
    {0, 16, (void *)0xa30402},
    {0, 17, (void *)0xa980a97},
    {0, 19, (void *)0x40203f8},
    {0, 16, (void *)0xa30403},
    {0, 17, (void *)0xa9b0a9a},
    {0, 19, (void *)0x40203f8},
    {0, 16, (void *)0xa30404},
    {5, 14, (void *)_v1231},
    {0, 6, (void *)2718},
    {0, 3, (void *)_v1231},
    {5, 14, (void *)_v1232},
    {0, 11, (void *)681}
};

static const struct efield _efieldarray[] = {
    {4, 187, -1, 504, 2},
    {4, 188, -1, 505, 2},
    {4, 189, -1, 506, 2},
    {0, 195, -1, 507, 2},
    {4, 248, -1, 508, 2},
    {8, 249, -1, 509, 2},
    {32, 250, -1, 510, 2},
    {52, 251, -1, 511, 2},
    {76, 252, 0, 512, 3},
    {80, 253, -1, 513, 2},
    {84, 254, -1, 514, 2},
    {92, 255, 1, 515, 3},
    {120, 256, 2, 516, 3},
    {136, 261, 3, 517, 3},
    {4, 311, -1, 518, 2},
    {8, 312, -1, 519, 2},
    {72, 313, 0, 520, 3},
    {76, 314, -1, 521, 2},
    {100, 319, 1, 517, 3},
    {0, 324, -1, 507, 2},
    {4, 329, -1, 518, 2},
    {8, 330, 0, 522, 3},
    {32, 331, 1, 523, 3},
    {56, 336, 2, 517, 3},
    {0, 341, -1, 507, 2},
    {0, 346, -1, 507, 2},
    {0, 351, -1, 507, 2},
    {4, 367, -1, 518, 2},
    {8, 368, 0, 524, 3},
    {16, 369, -1, 525, 2},
    {28, 370, -1, 526, 2},
    {40, 375, 1, 517, 3},
    {0, 380, -1, 507, 2},
    {0, 385, -1, 507, 2},
    {0, 390, -1, 507, 2},
    {0, 395, -1, 507, 2},
    {0, 400, -1, 507, 2},
    {0, 405, -1, 507, 2},
    {0, 410, -1, 507, 2},
    {0, 415, -1, 507, 2},
    {0, 420, -1, 507, 2},
    {4, 445, -1, 527, 2},
    {24, 446, 0, 528, 3},
    {28, 447, 1, 529, 3},
    {32, 448, 2, 530, 3},
    {64, 453, 3, 517, 3},
    {0, 458, -1, 507, 2},
    {0, 520, -1, 507, 2},
    {0, 525, -1, 507, 2},
    {0, 530, -1, 507, 2},
    {4, 535, -1, 527, 2},
    {24, 540, 0, 517, 3},
    {0, 545, -1, 507, 2},
    {4, 550, -1, 527, 2},
    {24, 551, 0, 531, 3},
    {28, 556, 1, 517, 3},
    {4, 570, -1, 532, 2},
    {12, 571, -1, 533, 2},
    {20, 576, 0, 517, 3},
    {0, 581, -1, 507, 2},
    {4, 586, -1, 527, 2},
    {24, 587, -1, 531, 2},
    {28, 592, 0, 517, 3},
    {0, 597, -1, 507, 2},
    {4, 637, -1, 527, 2},
    {24, 638, 0, 534, 3},
    {40, 639, 1, 535, 3},
    {56, 640, 2, 536, 3},
    {76, 645, 3, 517, 3},
    {0, 653, -1, 537, 2},
    {0, 658, -1, 507, 2},
    {0, 663, -1, 507, 2},
    {0, 668, -1, 507, 2},
    {0, 673, -1, 507, 2},
    {0, 678, -1, 507, 2},
    {0, 690, -1, 507, 2},
    {0, 702, -1, 507, 2},
    {0, 707, -1, 507, 2},
    {0, 712, -1, 507, 2},
    {4, 723, -1, 538, 2},
    {28, 724, 0, 539, 3},
    {52, 729, 1, 517, 3},
    {0, 734, -1, 507, 2},
    {4, 758, -1, 540, 2},
    {4, 759, -1, 541, 2},
    {0, 764, -1, 507, 2},
    {4, 787, -1, 540, 2},
    {4, 788, -1, 541, 2},
    {0, 793, -1, 507, 2},
    {0, 798, -1, 507, 2},
    {4, 814, -1, 542, 2},
    {4, 815, -1, 543, 2},
    {0, 820, -1, 507, 2},
    {4, 821, 0, 544, 3},
    {28, 822, 1, 545, 3},
    {36, 823, 2, 546, 3},
    {60, 824, 3, 547, 3},
    {64, 825, 4, 548, 3},
    {68, 826, 5, 549, 3},
    {72, 831, 6, 517, 3},
    {4, 856, -1, 540, 2},
    {4, 857, -1, 541, 2},
    {4, 881, -1, 540, 2},
    {4, 882, -1, 541, 2},
    {4, 905, -1, 540, 2},
    {4, 906, -1, 541, 2},
    {0, 911, -1, 507, 2},
    {4, 934, -1, 540, 2},
    {4, 935, -1, 541, 2},
    {4, 956, -1, 540, 2},
    {4, 957, -1, 541, 2},
    {4, 976, -1, 540, 2},
    {4, 977, -1, 541, 2},
    {0, 982, -1, 507, 2},
    {0, 987, -1, 507, 2},
    {4, 992, -1, 518, 2},
    {8, 993, -1, 533, 2},
    {16, 998, 0, 517, 3},
    {0, 1003, -1, 507, 2},
    {0, 1008, -1, 507, 2},
    {0, 1013, -1, 507, 2},
    {4, 1035, -1, 540, 2},
    {4, 1036, -1, 541, 2},
    {0, 1041, -1, 507, 2},
    {0, 1046, -1, 507, 2},
    {4, 1068, -1, 540, 2},
    {4, 1069, -1, 541, 2},
    {0, 1074, -1, 507, 2},
    {4, 1079, -1, 518, 2},
    {8, 1080, -1, 550, 2},
    {12, 1081, -1, 551, 2},
    {16, 1086, 0, 517, 3},
    {0, 1091, -1, 507, 2},
    {0, 1096, -1, 507, 2},
    {0, 1101, -1, 507, 2},
    {4, 1120, -1, 552, 2},
    {4, 1121, -1, 553, 2},
    {4, 1122, -1, 554, 2},
    {2, 1141, -1, 555, 2},
    {4, 1142, -1, 556, 2},
    {24, 1147, 0, 517, 3},
    {4, 1149, -1, 557, 2},
    {8, 1150, -1, 558, 2},
    {12, 1151, -1, 559, 2},
    {16, 1156, 0, 517, 3},
    {4, 566, -1, 560, 2},
    {4, 567, -1, 561, 2},
    {4, 568, -1, 562, 2},
    {4, 569, -1, 563, 2},
    {4, 1205, -1, 564, 2},
    {8, 1206, -1, 565, 2},
    {12, 1211, 0, 517, 3},
    {4, 1238, -1, 566, 2},
    {20, 1239, -1, 567, 2},
    {36, 1244, 0, 517, 3},
    {2, 1247, -1, 568, 2},
    {4, 1248, -1, 569, 2},
    {8, 1253, 0, 517, 3},
    {2, 1255, 0, 570, 3},
    {4, 1256, 1, 571, 3},
    {8, 1257, 2, 572, 3},
    {12, 1258, 3, 573, 3},
    {16, 1263, 4, 517, 3},
    {4, 1285, -1, 574, 2},
    {4, 1286, -1, 575, 2},
    {2, 426, -1, 576, 2},
    {8, 427, -1, 577, 2},
    {16, 432, 0, 517, 3},
    {4, 1292, -1, 518, 2},
    {8, 1293, -1, 533, 2},
    {16, 1298, 0, 517, 3},
    {4, 1313, 0, 578, 3},
    {24, 1314, 1, 579, 3},
    {28, 1319, 2, 517, 3},
    {4, 1320, -1, 580, 2},
    {12, 1321, -1, 581, 2},
    {16, 1326, 0, 517, 3},
    {2, 716, -1, 576, 2},
    {8, 717, -1, 582, 2},
    {20, 722, 0, 517, 3},
    {4, 1360, -1, 583, 2},
    {20, 1361, -1, 584, 2},
    {24, 1366, 0, 517, 3},
    {4, 1387, -1, 585, 2},
    {8, 1392, 0, 517, 3},
    {4, 1394, -1, 586, 2},
    {8, 1395, -1, 587, 2},
    {12, 1400, 0, 517, 3},
    {4, 1402, -1, 588, 2},
    {8, 1403, -1, 589, 2},
    {12, 1408, 0, 517, 3},
    {4, 1434, -1, 590, 2},
    {8, 1435, -1, 591, 2},
    {24, 1436, -1, 592, 2},
    {32, 1437, -1, 593, 2},
    {36, 1438, 0, 594, 3},
    {48, 1439, 1, 595, 3},
    {64, 1444, 2, 517, 3},
    {0, 1461, -1, 596, 2},
    {2, 1462, -1, 597, 2},
    {0, 1463, -1, 598, 2},
    {2, 1481, -1, 599, 2},
    {4, 1482, -1, 600, 2},
    {6, 1483, -1, 601, 2},
    {8, 1484, -1, 602, 2},
    {10, 1485, 0, 603, 3},
    {12, 1490, 1, 517, 3},
    {4, 1493, 0, 604, 3},
    {8, 1494, 1, 605, 3},
    {12, 1499, 2, 517, 3},
    {2, 1536, -1, 606, 2},
    {12, 1537, -1, 607, 2},
    {20, 1538, -1, 608, 2},
    {24, 1539, -1, 609, 2},
    {32, 1544, 0, 517, 3},
    {4, 217, -1, 610, 2},
    {12, 218, -1, 611, 2},
    {20, 223, 0, 517, 3},
    {4, 199, -1, 612, 2},
    {12, 200, -1, 613, 2},
    {20, 205, 0, 517, 3},
    {2, 173, -1, 614, 2},
    {4, 173, 0, 615, 3},
    {6, 173, 1, 616, 3},
    {8, 171, -1, 570, 66},
    {12, 172, 2, 617, 7},
    {0, 175, -1, 570, 18},
    {4, 176, -1, 619, 2},
    {8, 177, -1, 620, 2},
    {0, 179, -1, 570, 18},
    {4, 180, -1, 619, 2},
    {8, 181, -1, 620, 2},
    {0, 183, -1, 570, 18},
    {4, 184, -1, 619, 2},
    {8, 185, -1, 620, 2},
    {0, 191, -1, 621, 18},
    {4, 192, -1, 619, 2},
    {8, 193, -1, 620, 2},
    {0, 201, -1, 621, 18},
    {4, 202, -1, 619, 2},
    {8, 203, -1, 622, 2},
    {0, 210, -1, 621, 18},
    {4, 211, -1, 619, 2},
    {8, 212, -1, 622, 2},
    {4, 208, -1, 623, 2},
    {12, 209, -1, 624, 2},
    {16, 214, 0, 517, 3},
    {0, 219, -1, 621, 18},
    {4, 220, -1, 619, 2},
    {8, 221, -1, 622, 2},
    {0, 232, -1, 621, 18},
    {4, 233, -1, 619, 2},
    {8, 234, -1, 622, 2},
    {2, 227, -1, 625, 2},
    {8, 228, 0, 626, 3},
    {12, 229, 1, 627, 3},
    {16, 230, 2, 628, 3},
    {20, 231, 3, 629, 3},
    {24, 236, 4, 517, 3},
    {0, 242, -1, 621, 18},
    {4, 243, -1, 619, 2},
    {8, 244, -1, 622, 2},
    {4, 240, -1, 630, 2},
    {8, 241, -1, 631, 2},
    {12, 246, 0, 517, 3},
    {0, 257, -1, 621, 18},
    {4, 258, -1, 619, 2},
    {8, 259, -1, 622, 2},
    {0, 262, -1, 621, 18},
    {4, 263, -1, 619, 2},
    {8, 264, -1, 620, 2},
    {0, 275, -1, 621, 18},
    {4, 276, -1, 619, 2},
    {8, 277, -1, 622, 2},
    {2, 272, -1, 632, 2},
    {4, 273, -1, 633, 2},
    {8, 274, -1, 634, 2},
    {12, 279, 0, 517, 3},
    {0, 285, -1, 621, 18},
    {4, 286, -1, 619, 2},
    {8, 287, -1, 622, 2},
    {4, 281, -1, 635, 2},
    {12, 282, -1, 636, 2},
    {20, 283, -1, 637, 2},
    {28, 284, -1, 638, 2},
    {36, 289, 0, 517, 3},
    {0, 294, -1, 621, 18},
    {4, 295, -1, 619, 2},
    {8, 296, -1, 622, 2},
    {2, 291, -1, 639, 2},
    {4, 292, -1, 640, 2},
    {20, 293, 0, 641, 3},
    {60, 298, 1, 517, 3},
    {0, 305, -1, 621, 18},
    {4, 306, -1, 619, 2},
    {8, 307, -1, 622, 2},
    {4, 303, -1, 642, 2},
    {12, 304, -1, 643, 2},
    {20, 309, 0, 517, 3},
    {0, 315, -1, 621, 18},
    {4, 316, -1, 619, 2},
    {8, 317, -1, 622, 2},
    {0, 320, -1, 621, 18},
    {4, 321, -1, 619, 2},
    {8, 322, -1, 620, 2},
    {0, 325, -1, 621, 18},
    {4, 326, -1, 619, 2},
    {8, 327, -1, 620, 2},
    {0, 332, -1, 621, 18},
    {4, 333, -1, 619, 2},
    {8, 334, -1, 622, 2},
    {0, 337, -1, 621, 18},
    {4, 338, -1, 619, 2},
    {8, 339, -1, 620, 2},
    {0, 342, -1, 621, 18},
    {4, 343, -1, 619, 2},
    {8, 344, -1, 620, 2},
    {0, 347, -1, 621, 18},
    {4, 348, -1, 619, 2},
    {8, 349, -1, 620, 2},
    {0, 352, -1, 621, 18},
    {4, 353, -1, 619, 2},
    {8, 354, -1, 620, 2},
    {0, 361, -1, 621, 18},
    {4, 362, -1, 619, 2},
    {8, 363, -1, 622, 2},
    {2, 359, -1, 644, 2},
    {4, 360, -1, 645, 2},
    {8, 365, 0, 517, 3},
    {0, 371, -1, 621, 18},
    {4, 372, -1, 619, 2},
    {8, 373, -1, 622, 2},
    {0, 376, -1, 621, 18},
    {4, 377, -1, 619, 2},
    {8, 378, -1, 620, 2},
    {0, 381, -1, 621, 18},
    {4, 382, -1, 619, 2},
    {8, 383, -1, 620, 2},
    {0, 386, -1, 621, 18},
    {4, 387, -1, 619, 2},
    {8, 388, -1, 620, 2},
    {0, 391, -1, 621, 18},
    {4, 392, -1, 619, 2},
    {8, 393, -1, 620, 2},
    {0, 396, -1, 621, 18},
    {4, 397, -1, 619, 2},
    {8, 398, -1, 620, 2},
    {0, 401, -1, 621, 18},
    {4, 402, -1, 619, 2},
    {8, 403, -1, 620, 2},
    {0, 406, -1, 621, 18},
    {4, 407, -1, 619, 2},
    {8, 408, -1, 620, 2},
    {0, 411, -1, 621, 18},
    {4, 412, -1, 619, 2},
    {8, 413, -1, 620, 2},
    {0, 416, -1, 621, 18},
    {4, 417, -1, 619, 2},
    {8, 418, -1, 620, 2},
    {0, 421, -1, 621, 18},
    {4, 422, -1, 619, 2},
    {8, 423, -1, 620, 2},
    {0, 428, -1, 621, 18},
    {4, 429, -1, 619, 2},
    {8, 430, -1, 622, 2},
    {0, 439, -1, 621, 18},
    {4, 440, -1, 619, 2},
    {8, 441, -1, 622, 2},
    {4, 434, -1, 646, 2},
    {12, 435, -1, 647, 2},
    {16, 436, -1, 648, 2},
    {20, 437, -1, 649, 2},
    {24, 438, -1, 650, 2},
    {28, 443, 0, 517, 3},
    {0, 449, -1, 621, 18},
    {4, 450, -1, 619, 2},
    {8, 451, -1, 622, 2},
    {0, 454, -1, 621, 18},
    {4, 455, -1, 619, 2},
    {8, 456, -1, 620, 2},
    {0, 465, -1, 621, 18},
    {4, 466, -1, 619, 2},
    {8, 467, -1, 622, 2},
    {2, 461, -1, 651, 2},
    {4, 462, -1, 652, 2},
    {8, 463, -1, 653, 2},
    {12, 464, -1, 654, 2},
    {16, 469, 0, 517, 3},
    {0, 477, -1, 621, 18},
    {4, 478, -1, 619, 2},
    {8, 479, -1, 622, 2},
    {4, 474, -1, 655, 2},
    {8, 475, -1, 558, 2},
    {12, 476, -1, 559, 2},
    {16, 481, 0, 517, 3},
    {0, 487, -1, 621, 18},
    {4, 488, -1, 619, 2},
    {8, 489, -1, 622, 2},
    {2, 483, -1, 656, 2},
    {4, 484, -1, 657, 2},
    {8, 485, -1, 658, 2},
    {12, 486, -1, 659, 2},
    {32, 491, 0, 517, 3},
    {4, 493, -1, 660, 2},
    {4, 494, -1, 661, 2},
    {0, 501, -1, 621, 18},
    {4, 502, -1, 619, 2},
    {8, 503, -1, 622, 2},
    {4, 496, -1, 662, 2},
    {8, 497, -1, 663, 2},
    {28, 498, -1, 664, 2},
    {32, 499, -1, 665, 2},
    {36, 500, -1, 666, 2},
    {76, 505, 0, 517, 3},
    {0, 510, -1, 621, 18},
    {4, 511, -1, 619, 2},
    {8, 512, -1, 622, 2},
    {4, 507, -1, 667, 2},
    {24, 508, -1, 668, 2},
    {104, 509, 0, 669, 3},
    {108, 514, 1, 517, 3},
    {0, 516, -1, 621, 18},
    {4, 517, -1, 619, 2},
    {8, 518, -1, 620, 2},
    {0, 521, -1, 621, 18},
    {4, 522, -1, 619, 2},
    {8, 523, -1, 620, 2},
    {0, 526, -1, 621, 18},
    {4, 527, -1, 619, 2},
    {8, 528, -1, 620, 2},
    {0, 531, -1, 621, 18},
    {4, 532, -1, 619, 2},
    {8, 533, -1, 620, 2},
    {0, 536, -1, 621, 18},
    {4, 537, -1, 619, 2},
    {8, 538, -1, 622, 2},
    {0, 541, -1, 621, 18},
    {4, 542, -1, 619, 2},
    {8, 543, -1, 620, 2},
    {0, 546, -1, 621, 18},
    {4, 547, -1, 619, 2},
    {8, 548, -1, 620, 2},
    {0, 552, -1, 621, 18},
    {4, 553, -1, 619, 2},
    {8, 554, -1, 622, 2},
    {0, 557, -1, 621, 18},
    {4, 558, -1, 619, 2},
    {8, 559, -1, 620, 2},
    {0, 572, -1, 621, 18},
    {4, 573, -1, 619, 2},
    {8, 574, -1, 622, 2},
    {0, 577, -1, 621, 18},
    {4, 578, -1, 619, 2},
    {8, 579, -1, 620, 2},
    {0, 582, -1, 621, 18},
    {4, 583, -1, 619, 2},
    {8, 584, -1, 620, 2},
    {0, 588, -1, 621, 18},
    {4, 589, -1, 619, 2},
    {8, 590, -1, 622, 2},
    {0, 593, -1, 621, 18},
    {4, 594, -1, 619, 2},
    {8, 595, -1, 620, 2},
    {0, 598, -1, 621, 18},
    {4, 599, -1, 619, 2},
    {8, 600, -1, 620, 2},
    {0, 605, -1, 621, 18},
    {4, 606, -1, 619, 2},
    {8, 607, -1, 622, 2},
    {4, 603, -1, 670, 2},
    {8, 604, -1, 671, 2},
    {12, 609, 0, 517, 3},
    {0, 613, -1, 621, 18},
    {4, 614, -1, 619, 2},
    {8, 615, -1, 622, 2},
    {4, 611, -1, 672, 2},
    {8, 612, -1, 673, 2},
    {12, 617, 0, 517, 3},
    {0, 631, -1, 621, 18},
    {4, 632, -1, 619, 2},
    {8, 633, -1, 622, 2},
    {2, 625, -1, 674, 2},
    {4, 626, -1, 675, 2},
    {6, 627, -1, 676, 2},
    {8, 628, -1, 677, 2},
    {10, 629, -1, 678, 2},
    {12, 630, -1, 679, 2},
    {16, 635, 0, 517, 3},
    {0, 641, -1, 621, 18},
    {4, 642, -1, 619, 2},
    {8, 643, -1, 622, 2},
    {4, 646, -1, 680, 2},
    {4, 647, -1, 681, 2},
    {0, 649, -1, 621, 18},
    {12, 650, -1, 619, 2},
    {16, 651, -1, 620, 2},
    {0, 654, -1, 621, 18},
    {4, 655, -1, 619, 2},
    {8, 656, -1, 620, 2},
    {0, 659, -1, 621, 18},
    {4, 660, -1, 619, 2},
    {8, 661, -1, 620, 2},
    {0, 664, -1, 621, 18},
    {4, 665, -1, 619, 2},
    {8, 666, -1, 620, 2},
    {0, 669, -1, 621, 18},
    {4, 670, -1, 619, 2},
    {8, 671, -1, 620, 2},
    {0, 674, -1, 621, 18},
    {4, 675, -1, 619, 2},
    {8, 676, -1, 620, 2},
    {0, 680, -1, 621, 18},
    {4, 681, -1, 619, 2},
    {8, 682, -1, 622, 2},
    {4, 679, -1, 682, 2},
    {24, 684, 0, 517, 3},
    {0, 686, -1, 621, 18},
    {4, 687, -1, 619, 2},
    {8, 688, -1, 620, 2},
    {0, 692, -1, 621, 18},
    {4, 693, -1, 619, 2},
    {8, 694, -1, 622, 2},
    {4, 691, -1, 682, 2},
    {24, 696, 0, 517, 3},
    {0, 698, -1, 621, 18},
    {4, 699, -1, 619, 2},
    {8, 700, -1, 620, 2},
    {0, 703, -1, 621, 18},
    {4, 704, -1, 619, 2},
    {8, 705, -1, 620, 2},
    {0, 708, -1, 621, 18},
    {4, 709, -1, 619, 2},
    {8, 710, -1, 620, 2},
    {4, 713, -1, 683, 2},
    {4, 714, -1, 684, 2},
    {0, 718, -1, 621, 18},
    {4, 719, -1, 619, 2},
    {8, 720, -1, 622, 2},
    {0, 725, -1, 621, 18},
    {4, 726, -1, 619, 2},
    {8, 727, -1, 622, 2},
    {0, 730, -1, 621, 18},
    {4, 731, -1, 619, 2},
    {8, 732, -1, 620, 2},
    {0, 735, -1, 621, 18},
    {4, 736, -1, 619, 2},
    {8, 737, -1, 620, 2},
    {0, 743, -1, 621, 18},
    {4, 744, -1, 619, 2},
    {8, 745, -1, 622, 2},
    {4, 739, -1, 518, 2},
    {8, 740, -1, 519, 2},
    {72, 741, 0, 520, 3},
    {76, 742, -1, 685, 2},
    {100, 747, 1, 517, 3},
    {0, 752, -1, 621, 18},
    {4, 753, -1, 619, 2},
    {8, 754, -1, 622, 2},
    {4, 749, -1, 518, 2},
    {8, 750, -1, 519, 2},
    {72, 751, -1, 686, 2},
    {96, 756, 0, 517, 3},
    {0, 760, -1, 621, 18},
    {4, 761, -1, 619, 2},
    {8, 762, -1, 620, 2},
    {0, 765, -1, 621, 18},
    {4, 766, -1, 619, 2},
    {8, 767, -1, 620, 2},
    {0, 773, -1, 621, 18},
    {4, 774, -1, 619, 2},
    {8, 775, -1, 622, 2},
    {4, 769, -1, 518, 2},
    {8, 770, -1, 687, 2},
    {32, 771, 0, 688, 3},
    {56, 772, 1, 689, 3},
    {80, 777, 2, 517, 3},
    {0, 781, -1, 621, 18},
    {4, 782, -1, 619, 2},
    {8, 783, -1, 622, 2},
    {4, 779, -1, 518, 2},
    {8, 780, -1, 690, 2},
    {32, 785, 0, 517, 3},
    {0, 789, -1, 621, 18},
    {4, 790, -1, 619, 2},
    {8, 791, -1, 620, 2},
    {0, 794, -1, 621, 18},
    {4, 795, -1, 619, 2},
    {8, 796, -1, 620, 2},
    {0, 800, -1, 621, 18},
    {4, 801, -1, 619, 2},
    {8, 802, -1, 622, 2},
    {4, 799, 0, 691, 3},
    {12, 804, 1, 517, 3},
    {0, 808, -1, 621, 18},
    {4, 809, -1, 619, 2},
    {8, 810, -1, 622, 2},
    {4, 806, -1, 533, 2},
    {12, 807, 0, 691, 3},
    {20, 812, 1, 517, 3},
    {0, 816, -1, 621, 18},
    {4, 817, -1, 619, 2},
    {8, 818, -1, 620, 2},
    {0, 827, -1, 621, 18},
    {4, 828, -1, 619, 2},
    {8, 829, -1, 622, 2},
    {0, 832, -1, 621, 18},
    {4, 833, -1, 619, 2},
    {8, 834, -1, 620, 2},
    {0, 841, -1, 621, 18},
    {4, 842, -1, 619, 2},
    {8, 843, -1, 622, 2},
    {4, 837, -1, 518, 2},
    {8, 838, -1, 519, 2},
    {72, 839, 0, 520, 3},
    {76, 840, -1, 685, 2},
    {100, 845, 1, 517, 3},
    {0, 850, -1, 621, 18},
    {4, 851, -1, 619, 2},
    {8, 852, -1, 622, 2},
    {4, 847, -1, 518, 2},
    {8, 848, -1, 519, 2},
    {72, 849, -1, 686, 2},
    {96, 854, 0, 517, 3},
    {0, 858, -1, 621, 18},
    {4, 859, -1, 619, 2},
    {8, 860, -1, 620, 2},
    {0, 866, -1, 621, 18},
    {4, 867, -1, 619, 2},
    {8, 868, -1, 622, 2},
    {4, 863, -1, 518, 2},
    {8, 864, 0, 519, 3},
    {72, 865, 1, 685, 3},
    {96, 870, 2, 517, 3},
    {0, 875, -1, 621, 18},
    {4, 876, -1, 619, 2},
    {8, 877, -1, 622, 2},
    {4, 872, -1, 518, 2},
    {8, 873, 0, 519, 3},
    {72, 874, 1, 686, 3},
    {96, 879, 2, 517, 3},
    {0, 883, -1, 621, 18},
    {4, 884, -1, 619, 2},
    {8, 885, -1, 620, 2},
    {0, 891, -1, 621, 18},
    {4, 892, -1, 619, 2},
    {8, 893, -1, 622, 2},
    {4, 888, -1, 518, 2},
    {8, 889, 0, 692, 3},
    {32, 890, 1, 521, 3},
    {56, 895, 2, 517, 3},
    {0, 899, -1, 621, 18},
    {4, 900, -1, 619, 2},
    {8, 901, -1, 622, 2},
    {4, 897, -1, 518, 2},
    {8, 898, 0, 692, 3},
    {32, 903, 1, 517, 3},
    {0, 907, -1, 621, 18},
    {4, 908, -1, 619, 2},
    {8, 909, -1, 620, 2},
    {0, 912, -1, 621, 18},
    {4, 913, -1, 619, 2},
    {8, 914, -1, 620, 2},
    {0, 920, -1, 621, 18},
    {4, 921, -1, 619, 2},
    {8, 922, -1, 622, 2},
    {4, 916, -1, 518, 2},
    {8, 917, -1, 687, 2},
    {32, 918, 0, 688, 3},
    {56, 919, 1, 689, 3},
    {80, 924, 2, 517, 3},
    {0, 928, -1, 621, 18},
    {4, 929, -1, 619, 2},
    {8, 930, -1, 622, 2},
    {4, 926, -1, 518, 2},
    {8, 927, -1, 690, 2},
    {32, 932, 0, 517, 3},
    {0, 936, -1, 621, 18},
    {4, 937, -1, 619, 2},
    {8, 938, -1, 620, 2},
    {0, 942, -1, 621, 18},
    {4, 943, -1, 619, 2},
    {8, 944, -1, 622, 2},
    {4, 940, -1, 518, 2},
    {8, 941, 0, 687, 3},
    {32, 946, 1, 517, 3},
    {0, 950, -1, 621, 18},
    {4, 951, -1, 619, 2},
    {8, 952, -1, 622, 2},
    {4, 948, -1, 518, 2},
    {8, 949, 0, 690, 3},
    {32, 954, 1, 517, 3},
    {0, 958, -1, 621, 18},
    {4, 959, -1, 619, 2},
    {8, 960, -1, 620, 2},
    {0, 963, -1, 621, 18},
    {4, 964, -1, 619, 2},
    {8, 965, -1, 622, 2},
    {4, 962, -1, 518, 2},
    {8, 967, 0, 517, 3},
    {0, 970, -1, 621, 18},
    {4, 971, -1, 619, 2},
    {8, 972, -1, 622, 2},
    {4, 969, -1, 518, 2},
    {8, 974, 0, 517, 3},
    {0, 978, -1, 621, 18},
    {4, 979, -1, 619, 2},
    {8, 980, -1, 620, 2},
    {0, 983, -1, 621, 18},
    {4, 984, -1, 619, 2},
    {8, 985, -1, 620, 2},
    {0, 988, -1, 621, 18},
    {4, 989, -1, 619, 2},
    {8, 990, -1, 620, 2},
    {0, 994, -1, 621, 18},
    {4, 995, -1, 619, 2},
    {8, 996, -1, 622, 2},
    {0, 999, -1, 621, 18},
    {4, 1000, -1, 619, 2},
    {8, 1001, -1, 620, 2},
    {0, 1004, -1, 621, 18},
    {4, 1005, -1, 619, 2},
    {8, 1006, -1, 620, 2},
    {0, 1009, -1, 621, 18},
    {4, 1010, -1, 619, 2},
    {8, 1011, -1, 620, 2},
    {0, 1014, -1, 621, 18},
    {4, 1015, -1, 619, 2},
    {8, 1016, -1, 620, 2},
    {0, 1021, -1, 621, 18},
    {4, 1022, -1, 619, 2},
    {8, 1023, -1, 622, 2},
    {4, 1018, -1, 518, 2},
    {8, 1019, 0, 521, 3},
    {32, 1020, 1, 692, 3},
    {56, 1025, 2, 517, 3},
    {0, 1029, -1, 621, 18},
    {4, 1030, -1, 619, 2},
    {8, 1031, -1, 622, 2},
    {4, 1027, -1, 518, 2},
    {8, 1028, 0, 692, 3},
    {32, 1033, 1, 517, 3},
    {0, 1037, -1, 621, 18},
    {4, 1038, -1, 619, 2},
    {8, 1039, -1, 620, 2},
    {0, 1042, -1, 621, 18},
    {4, 1043, -1, 619, 2},
    {8, 1044, -1, 620, 2},
    {0, 1047, -1, 621, 18},
    {4, 1048, -1, 619, 2},
    {8, 1049, -1, 620, 2},
    {0, 1054, -1, 621, 18},
    {4, 1055, -1, 619, 2},
    {8, 1056, -1, 622, 2},
    {4, 1051, -1, 518, 2},
    {8, 1052, 0, 521, 3},
    {32, 1053, 1, 692, 3},
    {56, 1058, 2, 517, 3},
    {0, 1062, -1, 621, 18},
    {4, 1063, -1, 619, 2},
    {8, 1064, -1, 622, 2},
    {4, 1060, -1, 518, 2},
    {8, 1061, 0, 692, 3},
    {32, 1066, 1, 517, 3},
    {0, 1070, -1, 621, 18},
    {4, 1071, -1, 619, 2},
    {8, 1072, -1, 620, 2},
    {0, 1075, -1, 621, 18},
    {4, 1076, -1, 619, 2},
    {8, 1077, -1, 620, 2},
    {0, 1082, -1, 621, 18},
    {4, 1083, -1, 619, 2},
    {8, 1084, -1, 622, 2},
    {0, 1087, -1, 621, 18},
    {4, 1088, -1, 619, 2},
    {8, 1089, -1, 620, 2},
    {0, 1092, -1, 621, 18},
    {4, 1093, -1, 619, 2},
    {8, 1094, -1, 620, 2},
    {0, 1097, -1, 621, 18},
    {4, 1098, -1, 619, 2},
    {8, 1099, -1, 620, 2},
    {0, 1105, -1, 621, 18},
    {4, 1106, -1, 619, 2},
    {8, 1107, -1, 622, 2},
    {4, 1102, -1, 693, 2},
    {12, 1103, -1, 648, 2},
    {16, 1104, -1, 694, 2},
    {24, 1109, 0, 517, 3},
    {0, 1114, -1, 621, 18},
    {4, 1115, -1, 619, 2},
    {8, 1116, -1, 622, 2},
    {4, 1111, -1, 693, 2},
    {12, 1112, -1, 648, 2},
    {16, 1113, -1, 694, 2},
    {24, 1118, 0, 517, 3},
    {0, 1125, -1, 621, 18},
    {4, 1126, -1, 619, 2},
    {8, 1127, -1, 622, 2},
    {4, 1124, -1, 695, 2},
    {12, 1129, 0, 517, 3},
    {0, 1132, -1, 621, 18},
    {4, 1133, -1, 619, 2},
    {8, 1134, -1, 622, 2},
    {4, 1131, -1, 696, 2},
    {12, 1136, 0, 517, 3},
    {4, 1138, -1, 552, 2},
    {4, 1139, -1, 553, 2},
    {0, 1143, -1, 621, 18},
    {4, 1144, -1, 619, 2},
    {8, 1145, -1, 622, 2},
    {0, 1152, -1, 621, 18},
    {4, 1153, -1, 619, 2},
    {8, 1154, -1, 622, 2},
    {0, 1158, -1, 621, 18},
    {4, 1159, -1, 619, 2},
    {8, 1160, -1, 622, 2},
    {4, 1157, -1, 697, 2},
    {8, 1162, 0, 517, 3},
    {0, 1165, -1, 621, 18},
    {4, 1166, -1, 619, 2},
    {8, 1167, -1, 622, 2},
    {4, 1164, -1, 698, 2},
    {8, 1169, 0, 517, 3},
    {0, 1172, -1, 621, 18},
    {4, 1173, -1, 619, 2},
    {8, 1174, -1, 622, 2},
    {4, 1171, -1, 699, 2},
    {8, 1176, 0, 517, 3},
    {4, 1178, -1, 700, 2},
    {4, 1179, -1, 701, 2},
    {4, 1180, -1, 702, 2},
    {4, 1181, 0, 703, 10},
    {0, 1190, -1, 621, 18},
    {4, 1191, -1, 619, 2},
    {8, 1192, -1, 622, 2},
    {4, 1189, -1, 704, 2},
    {8, 1194, 0, 517, 3},
    {0, 1198, -1, 621, 18},
    {4, 1199, -1, 619, 2},
    {8, 1200, -1, 622, 2},
    {4, 1196, -1, 705, 2},
    {24, 1197, -1, 706, 2},
    {32, 1202, 0, 517, 3},
    {0, 1207, -1, 621, 18},
    {4, 1208, -1, 619, 2},
    {8, 1209, -1, 622, 2},
    {0, 1214, -1, 621, 18},
    {4, 1215, -1, 619, 2},
    {8, 1216, -1, 622, 2},
    {4, 1212, -1, 707, 2},
    {8, 1213, -1, 708, 2},
    {12, 1218, 0, 517, 3},
    {0, 1223, -1, 621, 18},
    {4, 1224, -1, 619, 2},
    {8, 1225, -1, 622, 2},
    {4, 1221, -1, 709, 2},
    {8, 1222, -1, 710, 2},
    {12, 1227, 0, 517, 3},
    {0, 1232, -1, 621, 18},
    {4, 1233, -1, 619, 2},
    {8, 1234, -1, 622, 2},
    {4, 1230, 0, 711, 3},
    {8, 1231, -1, 712, 2},
    {12, 1236, 1, 517, 3},
    {0, 1240, -1, 621, 18},
    {4, 1241, -1, 619, 2},
    {8, 1242, -1, 622, 2},
    {0, 1249, -1, 621, 18},
    {4, 1250, -1, 619, 2},
    {8, 1251, -1, 622, 2},
    {0, 1259, -1, 621, 18},
    {4, 1260, -1, 619, 2},
    {8, 1261, -1, 622, 2},
    {0, 1268, -1, 621, 18},
    {4, 1269, -1, 619, 2},
    {8, 1270, -1, 622, 2},
    {4, 1265, -1, 713, 2},
    {8, 1266, -1, 714, 2},
    {12, 1267, -1, 715, 2},
    {16, 1272, 0, 517, 3},
    {0, 1279, -1, 621, 18},
    {4, 1280, -1, 619, 2},
    {8, 1281, -1, 622, 2},
    {4, 1276, 0, 716, 3},
    {12, 1277, 1, 717, 3},
    {16, 1278, -1, 718, 2},
    {20, 1283, 2, 517, 3},
    {0, 1288, -1, 621, 18},
    {4, 1289, -1, 619, 2},
    {8, 1290, -1, 620, 2},
    {0, 1294, -1, 621, 18},
    {4, 1295, -1, 619, 2},
    {8, 1296, -1, 622, 2},
    {0, 1306, -1, 621, 18},
    {4, 1307, -1, 619, 2},
    {8, 1308, -1, 622, 2},
    {4, 1303, 0, 719, 3},
    {8, 1304, 1, 720, 3},
    {12, 1305, 2, 721, 3},
    {16, 1310, 3, 517, 3},
    {0, 1315, -1, 621, 18},
    {4, 1316, -1, 619, 2},
    {8, 1317, -1, 622, 2},
    {0, 1322, -1, 621, 18},
    {4, 1323, -1, 619, 2},
    {8, 1324, -1, 622, 2},
    {0, 1329, -1, 621, 18},
    {4, 1330, -1, 619, 2},
    {8, 1331, -1, 622, 2},
    {2, 1327, -1, 576, 2},
    {8, 1328, -1, 722, 2},
    {12, 1333, 0, 517, 3},
    {0, 1339, -1, 621, 18},
    {4, 1340, -1, 619, 2},
    {8, 1341, -1, 622, 2},
    {2, 1337, -1, 576, 2},
    {8, 1338, -1, 723, 2},
    {12, 1343, 0, 517, 3},
    {0, 1353, -1, 621, 18},
    {4, 1354, -1, 619, 2},
    {8, 1355, -1, 622, 2},
    {2, 1351, -1, 576, 2},
    {8, 1352, -1, 724, 2},
    {12, 1357, 0, 517, 3},
    {0, 1362, -1, 621, 18},
    {4, 1363, -1, 619, 2},
    {8, 1364, -1, 622, 2},
    {0, 1372, -1, 621, 18},
    {4, 1373, -1, 619, 2},
    {8, 1374, -1, 622, 2},
    {4, 1369, -1, 725, 2},
    {24, 1370, -1, 726, 2},
    {36, 1371, -1, 727, 2},
    {40, 1376, 0, 517, 3},
    {2, 1379, -1, 728, 2},
    {4, 1381, -1, 729, 2},
    {4, 1382, -1, 730, 2},
    {4, 1383, -1, 731, 2},
    {0, 1388, -1, 621, 18},
    {4, 1389, -1, 619, 2},
    {8, 1390, -1, 622, 2},
    {0, 1396, -1, 621, 18},
    {4, 1397, -1, 619, 2},
    {8, 1398, -1, 622, 2},
    {0, 1404, -1, 621, 18},
    {4, 1405, -1, 619, 2},
    {8, 1406, -1, 622, 2},
    {2, 1414, -1, 732, 2},
    {2, 1415, -1, 733, 2},
    {0, 1418, -1, 621, 18},
    {4, 1419, -1, 619, 2},
    {8, 1420, -1, 622, 2},
    {2, 1417, -1, 734, 2},
    {8, 1422, 0, 517, 3},
    {0, 1428, -1, 621, 18},
    {4, 1429, -1, 619, 2},
    {8, 1430, -1, 622, 2},
    {4, 1426, -1, 735, 2},
    {8, 1427, -1, 736, 2},
    {12, 1432, 0, 517, 3},
    {0, 1440, -1, 621, 18},
    {4, 1441, -1, 619, 2},
    {8, 1442, -1, 622, 2},
    {4, 1449, -1, 737, 2},
    {4, 1450, -1, 738, 2},
    {0, 1455, -1, 621, 18},
    {4, 1456, -1, 619, 2},
    {8, 1457, -1, 622, 2},
    {4, 1452, -1, 739, 2},
    {8, 1453, -1, 740, 2},
    {12, 1454, -1, 741, 2},
    {24, 1459, 0, 517, 3},
    {0, 1465, -1, 621, 18},
    {4, 1466, -1, 619, 2},
    {8, 1467, -1, 622, 2},
    {4, 1464, -1, 742, 2},
    {8, 1469, 0, 517, 3},
    {0, 1474, -1, 621, 18},
    {4, 1475, -1, 619, 2},
    {8, 1476, -1, 622, 2},
    {4, 1471, -1, 743, 2},
    {24, 1472, -1, 662, 2},
    {28, 1473, -1, 656, 2},
    {32, 1478, 0, 517, 3},
    {0, 1486, -1, 621, 18},
    {4, 1487, -1, 619, 2},
    {8, 1488, -1, 622, 2},
    {0, 1495, -1, 621, 18},
    {4, 1496, -1, 619, 2},
    {8, 1497, -1, 622, 2},
    {0, 1502, -1, 621, 18},
    {4, 1503, -1, 619, 2},
    {8, 1504, -1, 622, 2},
    {4, 1500, -1, 744, 2},
    {24, 1501, -1, 745, 2},
    {28, 1506, 0, 517, 3},
    {0, 1510, -1, 621, 18},
    {4, 1511, -1, 619, 2},
    {8, 1512, -1, 622, 2},
    {4, 1509, -1, 746, 2},
    {8, 1514, 0, 517, 3},
    {0, 1518, -1, 621, 18},
    {4, 1519, -1, 619, 2},
    {8, 1520, -1, 622, 2},
    {4, 1516, -1, 668, 2},
    {84, 1517, 0, 669, 3},
    {88, 1522, 1, 517, 3},
    {0, 1527, -1, 621, 18},
    {4, 1528, -1, 619, 2},
    {8, 1529, -1, 622, 2},
    {2, 1525, -1, 664, 2},
    {6, 1526, -1, 576, 2},
    {12, 1531, 0, 517, 3},
    {0, 1540, -1, 621, 18},
    {4, 1541, -1, 619, 2},
    {8, 1542, -1, 622, 2},
    {0, 1550, -1, 621, 18},
    {4, 1551, -1, 619, 2},
    {8, 1552, -1, 622, 2},
    {4, 1548, -1, 747, 2},
    {24, 1549, -1, 748, 2},
    {32, 1554, 0, 517, 3},
    {0, 190, -1, 621, 66},
    {4, 172, -1, 619, 2},
    {8, 173, -1, 749, 2},
    {12, 1557, -1, 750, 2},
    {0, 190, -1, 621, 66},
    {4, 172, -1, 619, 2},
    {8, 173, -1, 751, 2},
    {12, 1557, -1, 750, 2},
    {0, 648, -1, 621, 2},
    {12, 172, -1, 619, 2},
    {16, 173, -1, 749, 2},
    {20, 1557, -1, 750, 2},
    {24, 1560, -1, 752, 66}
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
    (void *)0x4, (void *)_v1290,
    (void *)"one-thousand-ms", (void *)"two-thousand-ms", (void *)"five-thousand-ms", (void *)"ten-thousand-ms",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1290,
    (void *)"partial-success-allowed",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"member", (void *)"not-member",
    (void *)0x1, (void *)_v1290,
    (void *)"deactivated",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"not-broadcasted", (void *)"broadcasted",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"hoTooEarly", (void *)"hoToWrongCell",
    (void *)0x2, (void *)_v1328, (void *)"interRATpingpong", (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1290,
    (void *)"abs-information",
    (void *)0x3, (void *)_v1245, (void *)"naics-information-start", (void *)"naics-information-stop", (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1290,
    (void *)"allowed",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"an1", (void *)"an2", (void *)"an4",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"start", (void *)"stop",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1290,
    (void *)"one-hundred-20-ms", (void *)"two-hundred-40-ms", (void *)"four-hundred-80-ms", (void *)"six-hundred-40-ms",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"reconfigurationFailure", (void *)"handoverFailure", (void *)"otherFailure",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1290,
    (void *)"rrcConnSetup",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"pDCPCountWrapAround", (void *)"pSCellChange", (void *)"other",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1290,
    (void *)"possible",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x7, (void *)_v1290,
    (void *)"sa0", (void *)"sa1", (void *)"sa2", (void *)"sa3", (void *)"sa4",
    (void *)"sa5", (void *)"sa6",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x6, (void *)_v1290,
    (void *)"v1s", (void *)"v2s", (void *)"v5s", (void *)"v10s", (void *)"v20s",
    (void *)"v60s",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"reject", (void *)"ignore", (void *)"notify",
    (void *)0x4, (void *)_v1290,
    (void *)"all", (void *)"geran", (void *)"utran", (void *)"cdma2000",
    (void *)0x3, (void *)_v1268, (void *)"geranandutran",
    (void *)"cdma2000andutran", (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1290,
    (void *)"change-of-serving-cell",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x1, (void *)_v1290,
    (void *)"ecgi",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1273,
    (void *)"spare", (void *)"highest", (void *)"lowest", (void *)"no-priority",
    (void *)0x2, (void *)_v1290,
    (void *)"shall-not-trigger-pre-emption", (void *)"may-trigger-pre-emption",
    (void *)0x2, (void *)_v1290,
    (void *)"not-pre-emptable", (void *)"pre-emptable",
    (void *)0x1, (void *)_v1290,
    (void *)"dL-forwardingProposed",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x10, (void *)_v1290,
    (void *)"minusInfinity", (void *)"minusEleven", (void *)"minusTen", (void *)"minusNine", (void *)"minusEight",
    (void *)"minusSeven", (void *)"minusSix", (void *)"minusFive", (void *)"minusFour", (void *)"minusThree",
    (void *)"minusTwo", (void *)"minusOne", (void *)"zero", (void *)"one", (void *)"two",
    (void *)"three",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"one", (void *)"two", (void *)"four",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x6, (void *)_v1290,
    (void *)"bw6", (void *)"bw15", (void *)"bw25", (void *)"bw50", (void *)"bw75",
    (void *)"bw100",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x9, (void *)_v1290,
    (void *)"ssp0", (void *)"ssp1", (void *)"ssp2", (void *)"ssp3", (void *)"ssp4",
    (void *)"ssp5", (void *)"ssp6", (void *)"ssp7", (void *)"ssp8",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"normal", (void *)"extended",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x16, (void *)_v1290,
    (void *)"handover-desirable-for-radio-reasons", (void *)"time-critical-handover", (void *)"resource-optimisation-handover", (void *)"reduce-load-in-serving-cell", (void *)"partial-handover",
    (void *)"unknown-new-eNB-UE-X2AP-ID", (void *)"unknown-old-eNB-UE-X2AP-ID", (void *)"unknown-pair-of-UE-X2AP-ID", (void *)"ho-target-not-allowed", (void *)"tx2relocoverall-expiry",
    (void *)"trelocprep-expiry", (void *)"cell-not-available", (void *)"no-radio-resources-available-in-target-cell", (void *)"invalid-MME-GroupID", (void *)"unknown-MME-Code",
    (void *)"encryption-and-or-integrity-protection-algorithms-not-supported", (void *)"reportCharacteristicsEmpty", (void *)"noReportPeriodicity", (void *)"existingMeasurementID", (void *)"unknown-eNB-Measurement-ID",
    (void *)"measurement-temporarily-not-available", (void *)"unspecified",
    (void *)0x19, (void *)_v1291, (void *)"load-balancing", (void *)"handover-optimisation", (void *)"value-out-of-allowed-range",
    (void *)"multiple-E-RAB-ID-instances", (void *)"switch-off-ongoing", (void *)"not-supported-QCI-value", (void *)"measurement-not-supported-for-the-object", (void *)"tDCoverall-expiry",
    (void *)"tDCprep-expiry", (void *)"action-desirable-for-radio-reasons", (void *)"reduce-load", (void *)"resource-optimisation", (void *)"time-critical-action",
    (void *)"target-not-allowed", (void *)"no-radio-resources-available", (void *)"invalid-QoS-combination", (void *)"encryption-algorithms-not-aupported", (void *)"procedure-cancelled",
    (void *)"rRM-purpose", (void *)"improve-user-bit-rate", (void *)"user-inactivity", (void *)"radio-connection-with-UE-lost", (void *)"failure-in-the-radio-interface-procedure",
    (void *)"bearer-option-not-supported", (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"transport-resource-unavailable", (void *)"unspecified",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x7, (void *)_v1290,
    (void *)"transfer-syntax-error", (void *)"abstract-syntax-error-reject", (void *)"abstract-syntax-error-ignore-and-notify", (void *)"message-not-compatible-with-receiver-state", (void *)"semantic-error",
    (void *)"unspecified", (void *)"abstract-syntax-error-falsely-constructed-message",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x5, (void *)_v1290,
    (void *)"control-processing-overload", (void *)"hardware-failure", (void *)"om-intervention", (void *)"not-enough-user-plane-processing-resources", (void *)"unspecified",
    (void *)0x1, (void *)_v1340,
    (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1290,
    (void *)"lowLoad", (void *)"mediumLoad", (void *)"highLoad", (void *)"overLoad",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0xa, (void *)_v1290,
    (void *)"ssp0", (void *)"ssp1", (void *)"ssp2", (void *)"ssp3", (void *)"ssp4",
    (void *)"ssp5", (void *)"ssp6", (void *)"ssp7", (void *)"ssp8", (void *)"ssp9",
    (void *)0x1, (void *)_v1340,
    (void *)"oss-unknown-enumerator",
    (void *)0x4, (void *)_v1290,
    (void *)"verysmall", (void *)"small", (void *)"medium", (void *)"large",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"initiating-message", (void *)"successful-outcome", (void *)"unsuccessful-outcome",
    (void *)0x2, (void *)_v1290,
    (void *)"not-understood", (void *)"missing",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x8, (void *)_v1290,
    (void *)"dB-6", (void *)"dB-4dot77", (void *)"dB-3", (void *)"dB-1dot77", (void *)"dB0",
    (void *)"dB1", (void *)"dB2", (void *)"dB3",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"subscription-information", (void *)"statistics",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x7, (void *)_v1290,
    (void *)"sec15", (void *)"sec30", (void *)"sec60", (void *)"sec90", (void *)"sec120",
    (void *)"sec180", (void *)"long-time",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"uplink", (void *)"downlink", (void *)"both-uplink-and-downlink",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"ms100", (void *)"ms1000", (void *)"ms10000",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x5, (void *)_v1290,
    (void *)"ms1024", (void *)"ms2048", (void *)"ms5120", (void *)"ms10240", (void *)"min1",
    (void *)0x1, (void *)_v1340,
    (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"immediate-MDT-only", (void *)"immediate-MDT-and-Trace",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"periodic", (void *)"a2eventtriggered",
    (void *)0x2, (void *)_v1328, (void *)"a2eventtriggered-periodic", (void *)"oss-unknown-enumerator",
    (void *)0xd, (void *)_v1290,
    (void *)"ms120", (void *)"ms240", (void *)"ms480", (void *)"ms640", (void *)"ms1024",
    (void *)"ms2048", (void *)"ms5120", (void *)"ms10240", (void *)"min1", (void *)"min6",
    (void *)"min12", (void *)"min30", (void *)"min60",
    (void *)0x8, (void *)_v1290,
    (void *)"r1", (void *)"r2", (void *)"r4", (void *)"r8", (void *)"r16",
    (void *)"r32", (void *)"r64", (void *)"rinfinity",
    (void *)0x6, (void *)_v1290,
    (void *)"n1", (void *)"n2", (void *)"n4", (void *)"n8", (void *)"n16",
    (void *)"n32",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x2, (void *)_v1290,
    (void *)"authorized", (void *)"not-authorized",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x6, (void *)_v1290,
    (void *)"minimum", (void *)"medium", (void *)"maximum", (void *)"minimumWithoutVendorSpecificExtension", (void *)"mediumWithoutVendorSpecificExtension",
    (void *)"maximumWithoutVendorSpecificExtension",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"high-interference", (void *)"medium-interference", (void *)"low-interference",
    (void *)0x1, (void *)_v1340, (void *)"oss-unknown-enumerator",
    (void *)0x3, (void *)_v1290,
    (void *)"optional", (void *)"conditional", (void *)"mandatory",
    (void *)"initiatingMessage",
    (void *)"successfulOutcome",
    (void *)"unsuccessfulOutcome",
    (void *)"protocolIEs",
    (void *)"mME-UE-S1AP-ID",
    (void *)"uESecurityCapabilities",
    (void *)"aS-SecurityInformation",
    (void *)"uEaggregateMaximumBitRate",
    (void *)"subscriberProfileIDforRFP",
    (void *)"e-RABs-ToBeSetup-List",
    (void *)"rRC-Context",
    (void *)"handoverRestrictionList",
    (void *)"locationReportingInformation",
    (void *)"iE-Extensions",
    (void *)"e-RAB-ID",
    (void *)"e-RAB-Level-QoS-Parameters",
    (void *)"dL-Forwarding",
    (void *)"uL-GTPtunnelEndpoint",
    (void *)"uL-GTP-TunnelEndpoint",
    (void *)"dL-GTP-TunnelEndpoint",
    (void *)"receiveStatusofULPDCPSDUs",
    (void *)"uL-COUNTvalue",
    (void *)"dL-COUNTvalue",
    (void *)"cell-ID",
    (void *)"ul-InterferenceOverloadIndication",
    (void *)"ul-HighInterferenceIndicationInfo",
    (void *)"relativeNarrowbandTxPower",
    (void *)"measurementFailureCause-List",
    (void *)"measurementFailedReportCharacteristics",
    (void *)"cause",
    (void *)"hWLoadIndicator",
    (void *)"s1TNLLoadIndicator",
    (void *)"radioResourceStatus",
    (void *)"privateIEs",
    (void *)"source-GlobalENB-ID",
    (void *)"target-GlobalENB-ID",
    (void *)"sCG-Bearer",
    (void *)"split-Bearer",
    (void *)"success",
    (void *)"reject-by-MeNB",
    (void *)"uE-SecurityCapabilities",
    (void *)"seNB-SecurityKey",
    (void *)"seNBUEAggregateMaximumBitRate",
    (void *)"e-RABs-ToBeAdded",
    (void *)"e-RABs-ToBeModified",
    (void *)"e-RABs-ToBeReleased",
    (void *)"uL-Count",
    (void *)"dL-Count",
    (void *)"fdd",
    (void *)"tdd",
    (void *)"abs-inactive",
    (void *)"dL-ABS-status",
    (void *)"usableABSInformation",
    (void *)"additionalspecialSubframePatterns",
    (void *)"cyclicPrefixDL",
    (void *)"cyclicPrefixUL",
    (void *)"radioNetwork",
    (void *)"transport",
    (void *)"protocol",
    (void *)"misc",
    (void *)"coMPInformationItem",
    (void *)"coMPInformationStartTime",
    (void *)"dL-CompositeAvailableCapacity",
    (void *)"uL-CompositeAvailableCapacity",
    (void *)"pDCP-SNExtended",
    (void *)"hFNModified",
    (void *)"procedureCode",
    (void *)"triggeringMessage",
    (void *)"procedureCriticality",
    (void *)"iEsCriticalityDiagnostics",
    (void *)"naics-active",
    (void *)"naics-inactive",
    (void *)"pLMN-Identity",
    (void *)"eUTRANcellIdentifier",
    (void *)"expectedActivity",
    (void *)"expectedHOInterval",
    (void *)"associatedSubframes",
    (void *)"extended-ul-InterferenceOverloadIndication",
    (void *)"eNB-ID",
    (void *)"gU-Group-ID",
    (void *)"mME-Code",
    (void *)"m3period",
    (void *)"m4period",
    (void *)"m4-links-to-log",
    (void *)"m5period",
    (void *)"m5-links-to-log",
    (void *)"mdt-Activation",
    (void *)"areaScopeOfMDT",
    (void *)"measurementsToActivate",
    (void *)"m1reportingTrigger",
    (void *)"m1thresholdeventA2",
    (void *)"m1periodicReporting",
    (void *)"handoverTriggerChangeLowerLimit",
    (void *)"handoverTriggerChangeUpperLimit",
    (void *)"handoverTriggerChange",
    (void *)"rootSequenceIndex",
    (void *)"zeroCorrelationIndex",
    (void *)"highSpeedFlag",
    (void *)"prach-FreqOffset",
    (void *)"prach-ConfigIndex",
    (void *)"proSeDirectDiscovery",
    (void *)"proSeDirectCommunication",
    (void *)"eUTRANTraceID",
    (void *)"interfacesToTrace",
    (void *)"traceDepth",
    (void *)"traceCollectionEntityIPAddress",
    (void *)"uEaggregateMaximumBitRateDownlink",
    (void *)"uEaggregateMaximumBitRateUplink",
    (void *)"encryptionAlgorithms",
    (void *)"integrityProtectionAlgorithms",
    (void *)"InitiatingMessage",
    (void *)"SuccessfulOutcome",
    (void *)"UnsuccessfulOutcome",
    (void *)"criticality", (void *)&_v0,
    (void *)"criticality",
    (void *)"value",
    (void *)"id",
    (void *)"extensionValue",
    (void *)"key-eNodeB-star",
    (void *)"nextHopChainingCount",
    (void *)"servingPLMN",
    (void *)"equivalentPLMNs",
    (void *)"forbiddenTAs",
    (void *)"forbiddenLAs",
    (void *)"forbiddenInterRATs",
    (void *)"eventType",
    (void *)"reportArea",
    (void *)"priorityLevel",
    (void *)"pre-emptionCapability",
    (void *)"pre-emptionVulnerability",
    (void *)"e-RAB-MaximumBitrateDL",
    (void *)"e-RAB-MaximumBitrateUL",
    (void *)"e-RAB-GuaranteedBitrateDL",
    (void *)"e-RAB-GuaranteedBitrateUL",
    (void *)"qCI",
    (void *)"allocationAndRetentionPriority",
    (void *)"gbrQosInformation",
    (void *)"transportLayerAddress",
    (void *)"gTP-TEID",
    (void *)"pDCP-SN",
    (void *)"hFN",
    (void *)"rNTP-PerPRB",
    (void *)"rNTP-Threshold",
    (void *)"numberOfCellSpecificAntennaPorts",
    (void *)"p-B",
    (void *)"pDCCH-InterferenceImpact",
    (void *)"uL-EARFCN",
    (void *)"dL-EARFCN",
    (void *)"uL-Transmission-Bandwidth",
    (void *)"dL-Transmission-Bandwidth",
    (void *)"specialSubframePatterns",
    (void *)"eARFCN",
    (void *)"transmission-Bandwidth",
    (void *)"subframeAssignment",
    (void *)"specialSubframe-Info",
    (void *)"fDD",
    (void *)"tDD",
    (void *)"pCI",
    (void *)"cellId",
    (void *)"tAC",
    (void *)"broadcastPLMNs",
    (void *)"eUTRA-Mode-Info",
    (void *)"old-ecgi",
    (void *)"servedCellInfo",
    (void *)"neighbour-Info",
    (void *)"dLHWLoadIndicator",
    (void *)"uLHWLoadIndicator",
    (void *)"dLS1TNLLoadIndicator",
    (void *)"uLS1TNLLoadIndicator",
    (void *)"dL-GBR-PRB-usage",
    (void *)"uL-GBR-PRB-usage",
    (void *)"dL-non-GBR-PRB-usage",
    (void *)"uL-non-GBR-PRB-usage",
    (void *)"dL-Total-PRB-usage",
    (void *)"uL-Total-PRB-usage",
    (void *)"local",
    (void *)"global",
    (void *)"ecgi",
    (void *)"macro-eNB-ID",
    (void *)"home-eNB-ID",
    (void *)"s1-UL-GTPtunnelEndpoint",
    (void *)"meNB-GTPtunnelEndpoint",
    (void *)"s1-DL-GTPtunnelEndpoint",
    (void *)"dL-Forwarding-GTPtunnelEndpoint",
    (void *)"uL-Forwarding-GTPtunnelEndpoint",
    (void *)"seNB-GTPtunnelEndpoint",
    (void *)"meNBtoSeNBContainer",
    (void *)"dL-GTPtunnelEndpoint",
    (void *)"abs-pattern-info",
    (void *)"measurement-subset",
    (void *)"usable-abs-pattern-info",
    (void *)"usaable-abs-pattern-info",
    (void *)"cellIdListforMDT",
    (void *)"tAListforMDT",
    (void *)"tAIListforMDT",
    (void *)"cellBased",
    (void *)"tABased",
    (void *)"pLMNWide",
    (void *)"tAIBased",
    (void *)"cell-Size",
    (void *)"coMPCellID",
    (void *)"coMPHypothesis",
    (void *)"coMPHypothesisSet",
    (void *)"benefitMetric",
    (void *)"startSFN",
    (void *)"startSubframeNumber",
    (void *)"cellCapacityClassValue",
    (void *)"capacityValue",
    (void *)"iECriticality",
    (void *)"iE-ID",
    (void *)"typeOfError",
    (void *)"transmissionModes",
    (void *)"pB-information",
    (void *)"pA-list",
    (void *)"expectedActivityPeriod",
    (void *)"expectedIdlePeriod",
    (void *)"sourceofUEActivityBehaviourInformation",
    (void *)"forbiddenTACs",
    (void *)"forbiddenLACs",
    (void *)"mME-Group-ID",
    (void *)"global-Cell-ID",
    (void *)"cellType",
    (void *)"time-UE-StayedInCell",
    (void *)"undefined",
    (void *)"e-UTRAN-Cell",
    (void *)"uTRAN-Cell",
    (void *)"gERAN-Cell",
    (void *)"threshold-RSRP",
    (void *)"threshold-RSRQ",
    (void *)"measurementThreshold",
    (void *)"reportInterval",
    (void *)"reportAmount",
    (void *)"oneframe",
    (void *)"fourframes",
    (void *)"radioframeAllocationPeriod",
    (void *)"radioframeAllocationOffset",
    (void *)"subframeAllocation",
    (void *)"freqBandIndicator",
    (void *)"eCGI",
    (void *)"rSRPCellID",
    (void *)"rSRPMeasured",
    (void *)"rSRPMeasurementResult",
    (void *)"target-Cell-ID",
    (void *)"ul-interferenceindication",
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
    {(void *)X2AP_X2AP_ELEMENTARY_PROCEDURES, 174, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1, 174, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2, 174, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_HandoverRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UE_ContextInformation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeSetup_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeSetup_ItemExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_HandoverRequestAcknowledge_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_HandoverPreparationFailure_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_HandoverReport_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SNStatusTransfer_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UEContextRelease_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_HandoverCancel_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ErrorIndication_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResetRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResetResponse_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2SetupRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2SetupResponse_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2SetupFailure_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_LoadInformation_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellInformation_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ENBConfigurationUpdate_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ServedCellsToModify_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ENBConfigurationUpdateAcknowledge_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ENBConfigurationUpdateFailure_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResourceStatusRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellToReport_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResourceStatusResponse_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MeasurementInitiationResult_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MeasurementFailureCause_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResourceStatusFailure_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CompleteFailureCauseInformation_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResourceStatusUpdate_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellMeasurementResult_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_PrivateMessage_IEs, 1561, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MobilityChangeRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MobilityChangeAcknowledge_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MobilityChangeFailure_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_RLFIndication_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellActivationRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ServedCellsToActivate_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellActivationResponse_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ActivatedCellList_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellActivationFailure_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2Release_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2APMessageTransfer_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_RNL_Header_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBAdditionRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeAdded_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeAdded_Item_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeAdded_Item_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBAdditionRequestAcknowledge_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBAdditionRequestReject_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBReconfigurationComplete_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResponseInformationSeNBReconfComp_SuccessItemExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItemExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBModificationRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UE_ContextInformationSeNBModReqExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeAdded_ModReqItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeAdded_ModReqItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeModified_ModReqItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeModified_ModReqItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeModified_ModReqItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBModificationRequestAcknowledge_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBModificationRequestReject_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBModificationRequired_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqdItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqdItemExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBModificationConfirm_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBModificationRefuse_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBReleaseRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_RelReqItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_RelReqItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBReleaseRequired_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBReleaseConfirm_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_RelConfItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_ToBeReleased_RelConfItem_Split_BearerExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SeNBCounterCheckRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_SubjectToCounterCheckItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_SubjectToCounterCheckItemExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2RemovalRequest_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2RemovalResponse_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_X2RemovalFailure_IEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ABSInformationFDD_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ABSInformationTDD_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ABS_Status_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_AdditionalSpecialSubframe_Info_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_AS_SecurityInformation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_AllocationAndRetentionPriority_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellBasedMDT_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellType_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CoMPHypothesisSetItem_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CoMPInformation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CoMPInformationItem_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CoMPInformationStartTime_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CompositeAvailableCapacityGroup_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CompositeAvailableCapacity_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_COUNTvalue_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_COUNTValueExtended_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CriticalityDiagnostics_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CriticalityDiagnostics_IE_List_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_DynamicNAICSInformation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_FDD_Info_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_TDD_Info_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ECGI_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RAB_Level_QoS_Parameters_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RAB_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RAB_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ExpectedUEBehaviour_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ExpectedUEActivityBehaviour_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ExtendedULInterferenceOverloadInfo_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ForbiddenTAs_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ForbiddenLAs_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_GBR_QosInformation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_GlobalENB_ID_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_GTPtunnelEndpoint_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_GU_Group_ID_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_GUMMEI_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_HandoverRestrictionList_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_HWLoadIndicator_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_LastVisitedEUTRANCellInformation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_LocationReportingInformation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_M3Configuration_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_M4Configuration_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_M5Configuration_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MDT_Configuration_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MBSFN_Subframe_Info_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_BandInfo_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_Neighbour_Information_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_M1PeriodicReporting_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_PRACH_Configuration_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ProSeAuthorized_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_RadioResourceStatus_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_RelativeNarrowbandTxPower_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_RSRPMeasurementResult_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_RSRPMRList_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_S1TNLLoadIndicator_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ServedCell_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_ServedCell_Information_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_SpecialSubframe_Info_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_TABasedMDT_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_TAIBasedMDT_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_TAI_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_M1ThresholdEventA2_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_TraceActivation_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UEAggregate_MaximumBitrate_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UESecurityCapabilities_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UL_HighInterferenceIndicationInfo_Item_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UsableABSInformationFDD_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_UsableABSInformationTDD_ExtIEs, 1559, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CompleteFailureCauseInformation_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeModified_ModAckItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_SubjectToStatusTransfer_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MeasurementInitiationResult_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeReleased_ModAckItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellMeasurementResult_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellInformation_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_MeasurementFailureCause_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_CellToReport_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ItemIEs, 1558, NULL, 0, NULL, -1, -1},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ModAckItemIEs, 1558, NULL, 0, NULL, -1, -1}
#else
    {(void *)X2AP_X2AP_ELEMENTARY_PROCEDURES, 174, NULL, 0},
    {(void *)X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_1, 174, NULL, 0},
    {(void *)X2AP_X2AP_ELEMENTARY_PROCEDURES_CLASS_2, 174, NULL, 0},
    {(void *)X2AP_HandoverRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_UE_ContextInformation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeSetup_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeSetup_ItemExtIEs, 1559, NULL, 0},
    {(void *)X2AP_HandoverRequestAcknowledge_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_HandoverPreparationFailure_IEs, 1558, NULL, 0},
    {(void *)X2AP_HandoverReport_IEs, 1558, NULL, 0},
    {(void *)X2AP_SNStatusTransfer_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_SubjectToStatusTransfer_ItemExtIEs, 1559, NULL, 0},
    {(void *)X2AP_UEContextRelease_IEs, 1558, NULL, 0},
    {(void *)X2AP_HandoverCancel_IEs, 1558, NULL, 0},
    {(void *)X2AP_ErrorIndication_IEs, 1558, NULL, 0},
    {(void *)X2AP_ResetRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_ResetResponse_IEs, 1558, NULL, 0},
    {(void *)X2AP_X2SetupRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_X2SetupResponse_IEs, 1558, NULL, 0},
    {(void *)X2AP_X2SetupFailure_IEs, 1558, NULL, 0},
    {(void *)X2AP_LoadInformation_IEs, 1558, NULL, 0},
    {(void *)X2AP_CellInformation_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ENBConfigurationUpdate_IEs, 1558, NULL, 0},
    {(void *)X2AP_ServedCellsToModify_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ENBConfigurationUpdateAcknowledge_IEs, 1558, NULL, 0},
    {(void *)X2AP_ENBConfigurationUpdateFailure_IEs, 1558, NULL, 0},
    {(void *)X2AP_ResourceStatusRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_CellToReport_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ResourceStatusResponse_IEs, 1558, NULL, 0},
    {(void *)X2AP_MeasurementInitiationResult_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_MeasurementFailureCause_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ResourceStatusFailure_IEs, 1558, NULL, 0},
    {(void *)X2AP_CompleteFailureCauseInformation_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ResourceStatusUpdate_IEs, 1558, NULL, 0},
    {(void *)X2AP_CellMeasurementResult_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_PrivateMessage_IEs, 1561, NULL, 0},
    {(void *)X2AP_MobilityChangeRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_MobilityChangeAcknowledge_IEs, 1558, NULL, 0},
    {(void *)X2AP_MobilityChangeFailure_IEs, 1558, NULL, 0},
    {(void *)X2AP_RLFIndication_IEs, 1558, NULL, 0},
    {(void *)X2AP_CellActivationRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_ServedCellsToActivate_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CellActivationResponse_IEs, 1558, NULL, 0},
    {(void *)X2AP_ActivatedCellList_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CellActivationFailure_IEs, 1558, NULL, 0},
    {(void *)X2AP_X2Release_IEs, 1558, NULL, 0},
    {(void *)X2AP_X2APMessageTransfer_IEs, 1558, NULL, 0},
    {(void *)X2AP_RNL_Header_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBAdditionRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeAdded_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeAdded_Item_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeAdded_Item_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBAdditionRequestAcknowledge_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_Item_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_Item_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBAdditionRequestReject_IEs, 1558, NULL, 0},
    {(void *)X2AP_SeNBReconfigurationComplete_IEs, 1558, NULL, 0},
    {(void *)X2AP_ResponseInformationSeNBReconfComp_SuccessItemExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ResponseInformationSeNBReconfComp_RejectByMeNBItemExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBModificationRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_UE_ContextInformationSeNBModReqExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeAdded_ModReqItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeAdded_ModReqItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeAdded_ModReqItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeModified_ModReqItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeModified_ModReqItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeModified_ModReqItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBModificationRequestAcknowledge_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ModAckItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeModified_ModAckItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeReleased_ModAckItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBModificationRequestReject_IEs, 1558, NULL, 0},
    {(void *)X2AP_SeNBModificationRequired_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqdItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_ModReqdItemExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBModificationConfirm_IEs, 1558, NULL, 0},
    {(void *)X2AP_SeNBModificationRefuse_IEs, 1558, NULL, 0},
    {(void *)X2AP_SeNBReleaseRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_RelReqItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_RelReqItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_RelReqItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBReleaseRequired_IEs, 1558, NULL, 0},
    {(void *)X2AP_SeNBReleaseConfirm_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_RelConfItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_RelConfItem_SCG_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_ToBeReleased_RelConfItem_Split_BearerExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SeNBCounterCheckRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_SubjectToCounterCheckItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_SubjectToCounterCheckItemExtIEs, 1559, NULL, 0},
    {(void *)X2AP_X2RemovalRequest_IEs, 1558, NULL, 0},
    {(void *)X2AP_X2RemovalResponse_IEs, 1558, NULL, 0},
    {(void *)X2AP_X2RemovalFailure_IEs, 1558, NULL, 0},
    {(void *)X2AP_ABSInformationFDD_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ABSInformationTDD_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ABS_Status_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_AdditionalSpecialSubframe_Info_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_AS_SecurityInformation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_AllocationAndRetentionPriority_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CellBasedMDT_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CellType_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CoMPHypothesisSetItem_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CoMPInformation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CoMPInformationItem_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CoMPInformationStartTime_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CompositeAvailableCapacityGroup_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CompositeAvailableCapacity_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_COUNTvalue_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_COUNTValueExtended_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CriticalityDiagnostics_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_CriticalityDiagnostics_IE_List_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_DynamicNAICSInformation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_FDD_Info_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_TDD_Info_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ECGI_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RAB_Level_QoS_Parameters_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RAB_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RAB_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ExpectedUEBehaviour_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ExpectedUEActivityBehaviour_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ExtendedULInterferenceOverloadInfo_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ForbiddenTAs_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ForbiddenLAs_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_GBR_QosInformation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_GlobalENB_ID_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_GTPtunnelEndpoint_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_GU_Group_ID_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_GUMMEI_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_HandoverRestrictionList_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_HWLoadIndicator_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_LastVisitedEUTRANCellInformation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_LocationReportingInformation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_M3Configuration_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_M4Configuration_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_M5Configuration_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_MDT_Configuration_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_MBSFN_Subframe_Info_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_BandInfo_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_Neighbour_Information_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_M1PeriodicReporting_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_PRACH_Configuration_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ProSeAuthorized_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_RadioResourceStatus_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_RelativeNarrowbandTxPower_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_RSRPMeasurementResult_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_RSRPMRList_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_S1TNLLoadIndicator_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ServedCell_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_ServedCell_Information_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_SpecialSubframe_Info_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_TABasedMDT_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_TAIBasedMDT_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_TAI_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_M1ThresholdEventA2_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_TraceActivation_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_UEAggregate_MaximumBitrate_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_UESecurityCapabilities_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_UL_HighInterferenceIndicationInfo_Item_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_UsableABSInformationFDD_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_UsableABSInformationTDD_ExtIEs, 1559, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_CompleteFailureCauseInformation_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeModified_ModAckItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_SubjectToStatusTransfer_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_MeasurementInitiationResult_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeReleased_ModAckItemIEs, 1558, NULL, 0},
    {(void *)X2AP_CellMeasurementResult_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_CellInformation_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_MeasurementFailureCause_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_CellToReport_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ItemIEs, 1558, NULL, 0},
    {(void *)X2AP_E_RABs_Admitted_ToBeAdded_ModAckItemIEs, 1558, NULL, 0}
#endif /* OSS_SPARTAN_AWARE  > 12 */
};


#ifdef OSS_SPARTAN_AWARE
#if ((OSS_SPARTAN_AWARE + 0) >= 3)
static void _oss_post_init(struct ossGlobal *world) {
    static const unsigned char _oss_typeinfo[] = {
        0x00, 0xa3, 0x33, 0xb4, 0x26, 0x6b, 0xc9, 0x61, 0x9a, 0x27,
        0x04, 0x63, 0x9a, 0x24, 0x04, 0x43, 0x28, 0xc4, 0xba, 0x94,
        0x35, 0xf5, 0x31, 0xdb, 0x52, 0x15, 0x71, 0xcb, 0xf9, 0x01,
        0x2d, 0x73, 0x41, 0x37, 0x5c, 0x53, 0xf0, 0x3c, 0x44, 0x14,
        0x32, 0x8c, 0xca, 0x82, 0xc3, 0x08, 0x22, 0x5d, 0x02, 0x58,
        0x74, 0x95, 0x76, 0x45, 0xde, 0xed, 0x1a, 0xf3, 0x1a, 0xf5,
        0x4e, 0x65, 0x64, 0xf0, 0x34, 0xba, 0x2f, 0xc6, 0x5e, 0xd8,
        0xa0, 0xed, 0xdb, 0x51, 0x8f, 0x58, 0x5f, 0x7d, 0x46, 0x60,
        0x2c, 0x03, 0xca, 0x58, 0xd8, 0x1c, 0xf2, 0xfb, 0xa6, 0x7d,
        0x4c, 0xe5, 0x1b, 0x99, 0xd2, 0x49, 0x42, 0xfa, 0xb1, 0xb5,
        0xff, 0x64, 0xcf, 0xfc, 0xac, 0x14, 0x3b, 0x5d, 0xff, 0x3c,
        0xc3, 0xc8, 0x2d, 0xb3, 0x7d, 0x39, 0x1d, 0x27, 0xb8, 0xf7,
        0xb3, 0xc7, 0x16, 0x74, 0x38, 0xa1, 0xcf, 0xa4, 0x49, 0xbc,
        0xc9, 0x06, 0xa1, 0x15, 0xd8, 0x95, 0x33, 0x4d, 0x38, 0x5d,
        0x3d, 0x83, 0x67, 0xec, 0x7b, 0xb6, 0x14, 0xa0, 0xa1, 0x56,
        0x25, 0xfd, 0x78, 0xdc, 0xd8, 0xf8, 0xf7, 0x31, 0xbd, 0x51,
        0x15, 0x69, 0xef, 0xa4, 0xbb
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

static const struct eheader _head = {_ossinit_x2ap, 0, 15, 19257, 170, 1562,
    (unsigned short *)_pduarray, (struct etype *)_etypearray,
    (struct efield *)_efieldarray, (void **)_enamearray, NULL,
    (struct ConstraintEntry *)_econstraintarray, NULL, NULL, 0, (void *)_objectsettable, 178};

#ifdef _OSSGETHEADER
void *DLL_ENTRY_FDEF ossGetHeader()
{
    return (void *)&_head;
}
#endif /* _OSSGETHEADER */

void * const x2ap = (void *)&_head;
