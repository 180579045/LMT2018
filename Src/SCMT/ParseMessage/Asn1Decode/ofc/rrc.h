/*************************************************************/
/* Copyright (C) 2011 OSS Nokalva, Inc.  All rights reserved.*/
/*************************************************************/

/* THIS FILE IS PROPRIETARY MATERIAL OF OSS NOKALVA, INC.
 * AND MAY BE USED ONLY BY DIRECT LICENSEES OF OSS NOKALVA, INC.
 * THIS FILE MAY NOT BE DISTRIBUTED. */

/* Generated for: Datang Mobile Communications Equipment CO.,LTD, Beijing, China - Windows license 9319 (C) & 9652 (ASN-1Step) */
/* Abstract syntax: rrc */
/* Created: Fri Mar 18 09:45:09 2011 */
/* ASN.1 compiler version: 8.4 */
/* Code generated for runtime version 8.4 or later */
/* Compiler operating system: Windows */
/* Compiler machine type: Intel x86 */
/* Target operating system: Windows */
/* Target machine type: Intel x86 */
/* C compiler options required: -Zp4 (Microsoft) */
/* ASN.1 compiler options and file names specified:
 * -controlfile rrc.c -headerfile rrc.h -errorfile asn_error.txt -externalname
 * rrc -constraints -debug -uper -root -autoencdec -compat nosharedtypes
 * asn1dflt.ms.zp4 36331-c70.asn
 */

#ifndef OSS_rrc
#define OSS_rrc

#include "ossasn1.h"

#define          BCCH_BCH_Message_PDU 1
#define          BCCH_DL_SCH_Message_PDU 2
#define          MCCH_Message_PDU 3
#define          PCCH_Message_PDU 4
#define          DL_CCCH_Message_PDU 5
#define          DL_DCCH_Message_PDU 6
#define          UL_CCCH_Message_PDU 7
#define          UL_DCCH_Message_PDU 8
#define          InDeviceCoexIndication_r11_PDU 9
#define          MBMSInterestIndication_r11_PDU 10
#define          RRCConnectionReconfiguration_v8m0_IEs_PDU 11
#define          RRCConnectionRelease_v9e0_IEs_PDU 12
#define          SidelinkUEInformation_r12_PDU 13
#define          SystemInformationBlockType1_PDU 14
#define          SystemInformationBlockType1_v890_IEs_PDU 15
#define          SystemInformationBlockType1_v8h0_IEs_PDU 16
#define          UEAssistanceInformation_r11_PDU 17
#define          UECapabilityInformation_PDU 18
#define          UEInformationResponse_v9e0_IEs_PDU 19
#define          SystemInformationBlockType2_v8h0_IEs_PDU 20
#define          SystemInformationBlockType5_v8h0_IEs_PDU 21
#define          SystemInformationBlockType6_v8h0_IEs_PDU 22
#define          UE_EUTRA_Capability_PDU 23
#define          UE_EUTRA_Capability_v9a0_IEs_PDU 24
#define          UE_RadioPagingInfo_r12_PDU 25
#define          SBCCH_SL_BCH_Message_PDU 26
#define          VarConnEstFailReport_r11_PDU 27
#define          VarLogMeasConfig_r10_PDU 28
#define          VarLogMeasConfig_r11_PDU 29
#define          VarLogMeasConfig_r12_PDU 30
#define          VarLogMeasReport_r10_PDU 31
#define          VarLogMeasReport_r11_PDU 32
#define          VarMeasConfig_PDU 33
#define          VarMeasReportList_PDU 34
#define          VarMeasReportList_r12_PDU 35
#define          VarMobilityHistoryReport_r12_PDU 36
#define          VarRLF_Report_r10_PDU 37
#define          VarRLF_Report_r11_PDU 38
#define          VarShortMAC_Input_PDU 39
#define          SL_Preconfiguration_r12_PDU 40
#define          HandoverCommand_PDU 41
#define          HandoverPreparationInformation_PDU 42
#define          SCG_ConfigInfo_r12_PDU 43
#define          UERadioAccessCapabilityInformation_PDU 44
#define          UERadioPagingInformation_PDU 45

typedef struct PHICH_Config {
    enum {
        phich_Duration_normal = 0,
        phich_Duration_extended = 1
    } phich_Duration;
    enum {
        oneSixth = 0,
        half = 1,
        one = 2,
        two = 3
    } phich_Resource;
} PHICH_Config;

typedef enum _enum1 {
    MasterInformationBlock_dl_Bandwidth_n6 = 0,
    MasterInformationBlock_dl_Bandwidth_n15 = 1,
    MasterInformationBlock_dl_Bandwidth_n25 = 2,
    MasterInformationBlock_dl_Bandwidth_n50 = 3,
    MasterInformationBlock_dl_Bandwidth_n75 = 4,
    MasterInformationBlock_dl_Bandwidth_n100 = 5
} _enum1;

typedef struct _bit1 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} _bit1;

typedef struct MasterInformationBlock {
    _enum1          dl_Bandwidth;
    PHICH_Config    phich_Config;
    _bit1           systemFrameNumber;
    _bit1           spare;
} MasterInformationBlock;

typedef MasterInformationBlock BCCH_BCH_MessageType;

typedef struct BCCH_BCH_Message {
    BCCH_BCH_MessageType message;
} BCCH_BCH_Message;

typedef struct AC_BarringConfig {
    enum {
        p00 = 0,
        p05 = 1,
        p10 = 2,
        p15 = 3,
        p20 = 4,
        ac_BarringFactor_p25 = 5,
        p30 = 6,
        p40 = 7,
        ac_BarringFactor_p50 = 8,
        p60 = 9,
        p70 = 10,
        ac_BarringFactor_p75 = 11,
        p80 = 12,
        p85 = 13,
        p90 = 14,
        p95 = 15
    } ac_BarringFactor;
    enum {
        s4 = 0,
        ac_Barring_s8 = 1,
        ac_Barring_s16 = 2,
        ac_Barring_s32 = 3,
        ac_Barring_s64 = 4,
        s128 = 5,
        s256 = 6,
        s512 = 7
    } ac_BarringTime;
    _bit1           ac_BarringForSpecialAC;
} AC_BarringConfig;

typedef struct PowerRampingParameters {
    enum {
        powerRampingStep_dB0 = 0,
        powerRampingStep_dB2 = 1,
        powerRampingStep_dB4 = 2,
        powerRampingStep_dB6 = 3
    } powerRampingStep;
    enum {
        dBm_120 = 0,
        dBm_118 = 1,
        dBm_116 = 2,
        dBm_114 = 3,
        dBm_112 = 4,
        dBm_110 = 5,
        dBm_108 = 6,
        dBm_106 = 7,
        dBm_104 = 8,
        dBm_102 = 9,
        dBm_100 = 10,
        dBm_98 = 11,
        dBm_96 = 12,
        dBm_94 = 13,
        dBm_92 = 14,
        dBm_90 = 15
    } preambleInitialReceivedTargetPower;
} PowerRampingParameters;

typedef enum PreambleTransMax {
    PreambleTransMax_n3 = 0,
    PreambleTransMax_n4 = 1,
    PreambleTransMax_n5 = 2,
    PreambleTransMax_n6 = 3,
    PreambleTransMax_n7 = 4,
    PreambleTransMax_n8 = 5,
    PreambleTransMax_n10 = 6,
    PreambleTransMax_n20 = 7,
    PreambleTransMax_n50 = 8,
    PreambleTransMax_n100 = 9,
    PreambleTransMax_n200 = 10
} PreambleTransMax;

typedef struct RACH_ConfigCommon {
    struct {
        unsigned char   bit_mask;
#           define      preamblesGroupAConfig_present 0x80
        enum {
            numberOfRA_Preambles_n4 = 0,
            numberOfRA_Preambles_n8 = 1,
            numberOfRA_Preambles_n12 = 2,
            numberOfRA_Preambles_n16 = 3,
            numberOfRA_Preambles_n20 = 4,
            numberOfRA_Preambles_n24 = 5,
            numberOfRA_Preambles_n28 = 6,
            numberOfRA_Preambles_n32 = 7,
            numberOfRA_Preambles_n36 = 8,
            numberOfRA_Preambles_n40 = 9,
            numberOfRA_Preambles_n44 = 10,
            numberOfRA_Preambles_n48 = 11,
            numberOfRA_Preambles_n52 = 12,
            numberOfRA_Preambles_n56 = 13,
            numberOfRA_Preambles_n60 = 14,
            numberOfRA_Preambles_n64 = 15
        } numberOfRA_Preambles;
        struct {
            enum {
                sizeOfRA_PreamblesGroupA_n4 = 0,
                sizeOfRA_PreamblesGroupA_n8 = 1,
                sizeOfRA_PreamblesGroupA_n12 = 2,
                sizeOfRA_PreamblesGroupA_n16 = 3,
                sizeOfRA_PreamblesGroupA_n20 = 4,
                sizeOfRA_PreamblesGroupA_n24 = 5,
                sizeOfRA_PreamblesGroupA_n28 = 6,
                sizeOfRA_PreamblesGroupA_n32 = 7,
                sizeOfRA_PreamblesGroupA_n36 = 8,
                sizeOfRA_PreamblesGroupA_n40 = 9,
                sizeOfRA_PreamblesGroupA_n44 = 10,
                sizeOfRA_PreamblesGroupA_n48 = 11,
                sizeOfRA_PreamblesGroupA_n52 = 12,
                sizeOfRA_PreamblesGroupA_n56 = 13,
                sizeOfRA_PreamblesGroupA_n60 = 14
            } sizeOfRA_PreamblesGroupA;
            enum {
                b56 = 0,
                b144 = 1,
                b208 = 2,
                b256 = 3
            } messageSizeGroupA;
            enum {
                minusinfinity = 0,
                messagePowerOffsetGroupB_dB0 = 1,
                messagePowerOffsetGroupB_dB5 = 2,
                messagePowerOffsetGroupB_dB8 = 3,
                messagePowerOffsetGroupB_dB10 = 4,
                messagePowerOffsetGroupB_dB12 = 5,
                dB15 = 6,
                messagePowerOffsetGroupB_dB18 = 7
            } messagePowerOffsetGroupB;
        } preamblesGroupAConfig;  /* optional; set in bit_mask
                                   * preamblesGroupAConfig_present if present */
                                  /* Need OP */
    } preambleInfo;
    PowerRampingParameters powerRampingParameters;
    struct {
        PreambleTransMax preambleTransMax;
        enum {
            ra_ResponseWindowSize_sf2 = 0,
            sf3 = 1,
            sf4 = 2,
            ra_ResponseWindowSize_sf5 = 3,
            sf6 = 4,
            sf7 = 5,
            ra_ResponseWindowSize_sf8 = 6,
            ra_ResponseWindowSize_sf10 = 7
        } ra_ResponseWindowSize;
        enum {
            mac_ContentionResolutionTimer_sf8 = 0,
            mac_ContentionResolutionTimer_sf16 = 1,
            sf24 = 2,
            mac_ContentionResolutionTimer_sf32 = 3,
            mac_ContentionResolutionTimer_sf40 = 4,
            sf48 = 5,
            sf56 = 6,
            mac_ContentionResolutionTimer_sf64 = 7
        } mac_ContentionResolutionTimer;
    } ra_SupervisionInfo;
    unsigned short  maxHARQ_Msg3Tx;
} RACH_ConfigCommon;

typedef struct BCCH_Config {
    enum {
        modificationPeriodCoeff_n2 = 0,
        modificationPeriodCoeff_n4 = 1,
        modificationPeriodCoeff_n8 = 2,
        modificationPeriodCoeff_n16 = 3
    } modificationPeriodCoeff;
} BCCH_Config;

typedef enum _enum2 {
    defaultPagingCycle_rf32 = 0,
    defaultPagingCycle_rf64 = 1,
    defaultPagingCycle_rf128 = 2,
    defaultPagingCycle_rf256 = 3
} _enum2;

typedef struct PCCH_Config {
    _enum2          defaultPagingCycle;
    enum {
        fourT = 0,
        twoT = 1,
        oneT = 2,
        halfT = 3,
        quarterT = 4,
        oneEighthT = 5,
        oneSixteenthT = 6,
        oneThirtySecondT = 7
    } nB;
} PCCH_Config;

typedef struct PRACH_ConfigInfo {
    unsigned short  prach_ConfigIndex;
    ossBoolean      highSpeedFlag;
    unsigned short  zeroCorrelationZoneConfig;
    unsigned short  prach_FreqOffset;
} PRACH_ConfigInfo;

typedef struct PRACH_ConfigSIB {
    unsigned short  rootSequenceIndex;
    PRACH_ConfigInfo prach_ConfigInfo;
} PRACH_ConfigSIB;

typedef struct PDSCH_ConfigCommon {
    short           referenceSignalPower;
    unsigned short  p_b;
} PDSCH_ConfigCommon;

typedef struct UL_ReferenceSignalsPUSCH {
    ossBoolean      groupHoppingEnabled;
    unsigned short  groupAssignmentPUSCH;
    ossBoolean      sequenceHoppingEnabled;
    unsigned short  cyclicShift;
} UL_ReferenceSignalsPUSCH;

typedef struct PUSCH_ConfigCommon {
    struct {
        unsigned short  n_SB;
        enum {
            interSubFrame = 0,
            intraAndInterSubFrame = 1
        } hoppingMode;
        unsigned short  pusch_HoppingOffset;
        ossBoolean      enable64QAM;
    } pusch_ConfigBasic;
    UL_ReferenceSignalsPUSCH ul_ReferenceSignalsPUSCH;
} PUSCH_ConfigCommon;

typedef struct PUCCH_ConfigCommon {
    enum {
        ds1 = 0,
        ds2 = 1,
        ds3 = 2
    } deltaPUCCH_Shift;
    unsigned short  nRB_CQI;
    unsigned short  nCS_AN;
    unsigned short  n1PUCCH_AN;
} PUCCH_ConfigCommon;

typedef enum _enum3 {
    true = 0
} _enum3;

typedef struct SoundingRS_UL_ConfigCommon {
    unsigned short  choice;
#       define      SoundingRS_UL_ConfigCommon_release_chosen 1
#       define      SoundingRS_UL_ConfigCommon_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                 * SoundingRS_UL_ConfigCommon_release_chosen */
        struct _seq1 {
            unsigned char   bit_mask;
#               define      srs_MaxUpPts_present 0x80
            enum {
                srs_BandwidthConfig_bw0 = 0,
                srs_BandwidthConfig_bw1 = 1,
                srs_BandwidthConfig_bw2 = 2,
                srs_BandwidthConfig_bw3 = 3,
                bw4 = 4,
                bw5 = 5,
                bw6 = 6,
                bw7 = 7
            } srs_BandwidthConfig;
            enum {
                sc0 = 0,
                sc1 = 1,
                sc2 = 2,
                sc3 = 3,
                sc4 = 4,
                sc5 = 5,
                sc6 = 6,
                sc7 = 7,
                sc8 = 8,
                sc9 = 9,
                sc10 = 10,
                sc11 = 11,
                sc12 = 12,
                sc13 = 13,
                sc14 = 14,
                sc15 = 15
            } srs_SubframeConfig;
            ossBoolean      ackNackSRS_SimultaneousTransmission;
            _enum3          srs_MaxUpPts;  /* optional; set in bit_mask
                                            * srs_MaxUpPts_present if present */
                                           /* Cond TDD */
        } setup;  /* to choose, set choice to
                   * SoundingRS_UL_ConfigCommon_setup_chosen */
    } u;
} SoundingRS_UL_ConfigCommon;

typedef enum Alpha_r12 {
    al0 = 0,
    al04 = 1,
    al05 = 2,
    al06 = 3,
    al07 = 4,
    al08 = 5,
    al09 = 6,
    al1 = 7
} Alpha_r12;

typedef enum _enum4 {
    deltaF_PUCCH_Format1_deltaF_2 = 0,
    deltaF_PUCCH_Format1_deltaF0 = 1,
    deltaF_PUCCH_Format1_deltaF2 = 2
} _enum4;

typedef struct DeltaFList_PUCCH {
    _enum4          deltaF_PUCCH_Format1;
    enum {
        deltaF_PUCCH_Format1b_deltaF1 = 0,
        deltaF_PUCCH_Format1b_deltaF3 = 1,
        deltaF_PUCCH_Format1b_deltaF5 = 2
    } deltaF_PUCCH_Format1b;
    enum {
        deltaF_PUCCH_Format2_deltaF_2 = 0,
        deltaF_PUCCH_Format2_deltaF0 = 1,
        deltaF_PUCCH_Format2_deltaF1 = 2,
        deltaF_PUCCH_Format2_deltaF2 = 3
    } deltaF_PUCCH_Format2;
    _enum4          deltaF_PUCCH_Format2a;
    _enum4          deltaF_PUCCH_Format2b;
} DeltaFList_PUCCH;

typedef struct UplinkPowerControlCommon {
    short           p0_NominalPUSCH;
    Alpha_r12       alpha;
    short           p0_NominalPUCCH;
    DeltaFList_PUCCH deltaFList_PUCCH;
    short           deltaPreambleMsg3;
} UplinkPowerControlCommon;

typedef enum UL_CyclicPrefixLength {
    len1 = 0,
    len2 = 1
} UL_CyclicPrefixLength;

typedef enum _enum5 {
    deltaF_1 = 0,
    deltaF_PUCCH_Format3_r10_deltaF0 = 1,
    deltaF_PUCCH_Format3_r10_deltaF1 = 2,
    deltaF_PUCCH_Format3_r10_deltaF2 = 3,
    deltaF_PUCCH_Format3_r10_deltaF3 = 4,
    deltaF4 = 5,
    deltaF_PUCCH_Format3_r10_deltaF5 = 6,
    deltaF6 = 7
} _enum5;

typedef enum _enum6 {
    deltaF_PUCCH_Format1bCS_r10_deltaF1 = 0,
    deltaF_PUCCH_Format1bCS_r10_deltaF2 = 1,
    deltaF_PUCCH_Format1bCS_r10_spare2 = 2,
    deltaF_PUCCH_Format1bCS_r10_spare1 = 3
} _enum6;

typedef struct UplinkPowerControlCommon_v1020 {
    _enum5          deltaF_PUCCH_Format3_r10;
    _enum6          deltaF_PUCCH_Format1bCS_r10;
} UplinkPowerControlCommon_v1020;

typedef struct RACH_ConfigCommon_v1250 {
    struct {
        unsigned char   bit_mask;
#           define      connEstFailOffset_r12_present 0x80
        enum {
            connEstFailCount_r12_n1 = 0,
            connEstFailCount_r12_n2 = 1,
            connEstFailCount_r12_n3 = 2,
            connEstFailCount_r12_n4 = 3
        } connEstFailCount_r12;
        enum {
            connEstFailOffsetValidity_r12_s30 = 0,
            connEstFailOffsetValidity_r12_s60 = 1,
            connEstFailOffsetValidity_r12_s120 = 2,
            connEstFailOffsetValidity_r12_s240 = 3,
            connEstFailOffsetValidity_r12_s300 = 4,
            s420 = 5,
            connEstFailOffsetValidity_r12_s600 = 6,
            s900 = 7
        } connEstFailOffsetValidity_r12;
        unsigned short  connEstFailOffset_r12;  /* optional; set in bit_mask
                                                 * connEstFailOffset_r12_present
                                                 * if present */
                                                /* Need OP */
    } txFailParams_r12;
} RACH_ConfigCommon_v1250;

typedef struct PUSCH_ConfigCommon_v1270 {
    _enum3          enable64QAM_v1270;
} PUSCH_ConfigCommon_v1270;

typedef struct RadioResourceConfigCommonSIB {
    unsigned char   bit_mask;
#       define      RadioResourceConfigCommonSIB_uplinkPowerControlCommon_v1020_present 0x80
#       define      rach_ConfigCommon_v1250_present 0x40
#       define      RadioResourceConfigCommonSIB_pusch_ConfigCommon_v1270_present 0x20
    RACH_ConfigCommon rach_ConfigCommon;
    BCCH_Config     bcch_Config;
    PCCH_Config     pcch_Config;
    PRACH_ConfigSIB prach_Config;
    PDSCH_ConfigCommon pdsch_ConfigCommon;
    PUSCH_ConfigCommon pusch_ConfigCommon;
    PUCCH_ConfigCommon pucch_ConfigCommon;
    SoundingRS_UL_ConfigCommon soundingRS_UL_ConfigCommon;
    UplinkPowerControlCommon uplinkPowerControlCommon;
    UL_CyclicPrefixLength ul_CyclicPrefixLength;
    UplinkPowerControlCommon_v1020 uplinkPowerControlCommon_v1020;  
                                  /* extension #1; optional; set in bit_mask
       * RadioResourceConfigCommonSIB_uplinkPowerControlCommon_v1020_present if
       * present */
                                                                    /* Need OR */
    RACH_ConfigCommon_v1250 rach_ConfigCommon_v1250;  /* extension #2; optional;
                                   * set in bit_mask
                                   * rach_ConfigCommon_v1250_present if
                                   * present */
                                                      /* Need OR */
    PUSCH_ConfigCommon_v1270 pusch_ConfigCommon_v1270;  /* extension #3;
                                   * optional; set in bit_mask
             * RadioResourceConfigCommonSIB_pusch_ConfigCommon_v1270_present if
             * present */
                                                        /* Need OR */
} RadioResourceConfigCommonSIB;

typedef enum _enum7 {
    t300_ms100 = 0,
    t300_ms200 = 1,
    t300_ms300 = 2,
    t300_ms400 = 3,
    ms600 = 4,
    t300_ms1000 = 5,
    t300_ms1500 = 6,
    t300_ms2000 = 7
} _enum7;

typedef enum _enum8 {
    t310_ms0 = 0,
    t310_ms50 = 1,
    t310_ms100 = 2,
    t310_ms200 = 3,
    t310_ms500 = 4,
    t310_ms1000 = 5,
    t310_ms2000 = 6
} _enum8;

typedef enum _enum9 {
    n310_n1 = 0,
    n310_n2 = 1,
    n310_n3 = 2,
    n310_n4 = 3,
    n310_n6 = 4,
    n310_n8 = 5,
    n310_n10 = 6,
    n310_n20 = 7
} _enum9;

typedef enum _enum10 {
    t311_ms1000 = 0,
    ms3000 = 1,
    ms5000 = 2,
    ms10000 = 3,
    ms15000 = 4,
    ms20000 = 5,
    ms30000 = 6
} _enum10;

typedef enum _enum11 {
    n311_n1 = 0,
    n311_n2 = 1,
    n311_n3 = 2,
    n311_n4 = 3,
    n311_n5 = 4,
    n311_n6 = 5,
    n311_n8 = 6,
    n311_n10 = 7
} _enum11;

typedef struct UE_TimersAndConstants {
    _enum7          t300;
    _enum7          t301;
    _enum8          t310;
    _enum9          n310;
    _enum10         t311;
    _enum11         n311;
} UE_TimersAndConstants;

typedef unsigned short  ARFCN_ValueEUTRA;

typedef unsigned short  AdditionalSpectrumEmission;

typedef enum TimeAlignmentTimer {
    TimeAlignmentTimer_sf500 = 0,
    sf750 = 1,
    TimeAlignmentTimer_sf1280 = 2,
    sf1920 = 3,
    TimeAlignmentTimer_sf2560 = 4,
    TimeAlignmentTimer_sf5120 = 5,
    TimeAlignmentTimer_sf10240 = 6,
    TimeAlignmentTimer_infinity = 7
} TimeAlignmentTimer;

typedef unsigned int    ARFCN_ValueEUTRA_v9e0;

typedef struct _seq2 {
    char            placeholder;
} _seq2;

typedef struct SystemInformationBlockType2_v9e0_IEs {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType2_v9e0_IEs_ul_CarrierFreq_v9e0_present 0x80
#       define      SystemInformationBlockType2_v9e0_IEs_nonCriticalExtension_present 0x40
    ARFCN_ValueEUTRA_v9e0 ul_CarrierFreq_v9e0;  /* optional; set in bit_mask
          * SystemInformationBlockType2_v9e0_IEs_ul_CarrierFreq_v9e0_present if
          * present */
                                                /* Cond ul-FreqMax */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType2_v9e0_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType2_v9e0_IEs;

typedef struct SystemInformationBlockType2_v8h0_IEs {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType2_v8h0_IEs_multiBandInfoList_present 0x80
#       define      SystemInformationBlockType2_v8h0_IEs_nonCriticalExtension_present 0x40
    struct _seqof1 {
        struct _seqof1  *next;
        AdditionalSpectrumEmission value;
    } *multiBandInfoList;  /* optional; set in bit_mask
            * SystemInformationBlockType2_v8h0_IEs_multiBandInfoList_present if
            * present */
                           /* Need OR */
    SystemInformationBlockType2_v9e0_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
         * SystemInformationBlockType2_v8h0_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType2_v8h0_IEs;

typedef struct _octet1 {
    unsigned int    length;
    unsigned char   *value;
} _octet1;

typedef struct SystemInformationBlockType2 {
    unsigned short  bit_mask;
#       define      ac_BarringInfo_present 0x8000
#       define      mbsfn_SubframeConfigList_present 0x4000
#       define      SystemInformationBlockType2_lateNonCriticalExtension_present 0x2000
#       define      ssac_BarringForMMTEL_Voice_r9_present 0x1000
#       define      ssac_BarringForMMTEL_Video_r9_present 0x0800
#       define      ac_BarringForCSFB_r10_present 0x0400
#       define      SystemInformationBlockType2_ac_BarringSkipForMMTELVoice_r12_present 0x0200
#       define      SystemInformationBlockType2_ac_BarringSkipForMMTELVideo_r12_present 0x0100
#       define      SystemInformationBlockType2_ac_BarringSkipForSMS_r12_present 0x0080
#       define      ac_BarringPerPLMN_List_r12_present 0x0040
    struct {
        unsigned char   bit_mask;
#           define      ac_BarringForMO_Signalling_present 0x80
#           define      ac_BarringForMO_Data_present 0x40
        ossBoolean      ac_BarringForEmergency;
        AC_BarringConfig ac_BarringForMO_Signalling;  /* optional; set in
                                   * bit_mask ac_BarringForMO_Signalling_present
                                   * if present */
                                                      /* Need OP */
        AC_BarringConfig ac_BarringForMO_Data;  /* optional; set in bit_mask
                                                 * ac_BarringForMO_Data_present
                                                 * if present */
                                                /* Need OP */
    } ac_BarringInfo;  /* optional; set in bit_mask ac_BarringInfo_present if
                        * present */
       /* Need OP */
    RadioResourceConfigCommonSIB radioResourceConfigCommon;
    UE_TimersAndConstants ue_TimersAndConstants;
    struct {
        unsigned char   bit_mask;
#           define      freqInfo_ul_CarrierFreq_present 0x80
#           define      freqInfo_ul_Bandwidth_present 0x40
        ARFCN_ValueEUTRA ul_CarrierFreq;  /* optional; set in bit_mask
                                           * freqInfo_ul_CarrierFreq_present if
                                           * present */
                                          /* Need OP */
        _enum1          ul_Bandwidth;  /* optional; set in bit_mask
                                        * freqInfo_ul_Bandwidth_present if
                                        * present */
       /* Need OP */
        AdditionalSpectrumEmission additionalSpectrumEmission;
    } freqInfo;
    struct MBSFN_SubframeConfigList *mbsfn_SubframeConfigList;  /* optional; set
                                   * in bit_mask
                                   * mbsfn_SubframeConfigList_present if
                                   * present */
                                                                /* Need OR */
    TimeAlignmentTimer timeAlignmentTimerCommon;
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        SystemInformationBlockType2_v8h0_IEs *decoded;
    } lateNonCriticalExtension;  /* extension #1; optional; set in bit_mask
              * SystemInformationBlockType2_lateNonCriticalExtension_present if
              * present */
    AC_BarringConfig ssac_BarringForMMTEL_Voice_r9;  /* extension #2; optional;
                                   * set in bit_mask
                                   * ssac_BarringForMMTEL_Voice_r9_present if
                                   * present */
                                                     /* Need OP */
    AC_BarringConfig ssac_BarringForMMTEL_Video_r9;  /* extension #2; optional;
                                   * set in bit_mask
                                   * ssac_BarringForMMTEL_Video_r9_present if
                                   * present */
                                                     /* Need OP */
    AC_BarringConfig ac_BarringForCSFB_r10;  /* extension #3; optional; set in
                                   * bit_mask ac_BarringForCSFB_r10_present if
                                   * present */
                                             /* Need OP */
    _enum3          ac_BarringSkipForMMTELVoice_r12;  /* extension #4; optional;
                                   * set in bit_mask
       * SystemInformationBlockType2_ac_BarringSkipForMMTELVoice_r12_present if
       * present */
        /* Need OP */
    _enum3          ac_BarringSkipForMMTELVideo_r12;  /* extension #4; optional;
                                   * set in bit_mask
       * SystemInformationBlockType2_ac_BarringSkipForMMTELVideo_r12_present if
       * present */
        /* Need OP */
    _enum3          ac_BarringSkipForSMS_r12;  /* extension #4; optional; set in
                                   * bit_mask
              * SystemInformationBlockType2_ac_BarringSkipForSMS_r12_present if
              * present */
        /* Need OP */
    struct AC_BarringPerPLMN_List_r12 *ac_BarringPerPLMN_List_r12;  
                                  /* extension #4; optional; set in bit_mask
                                   * ac_BarringPerPLMN_List_r12_present if
                                   * present */
                                                                    /* Need OP */
} SystemInformationBlockType2;

typedef enum _enum12 {
    t_Evaluation_s30 = 0,
    t_Evaluation_s60 = 1,
    t_Evaluation_s120 = 2,
    s180 = 3,
    t_Evaluation_s240 = 4,
    t_Evaluation_spare3 = 5,
    t_Evaluation_spare2 = 6,
    t_Evaluation_spare1 = 7
} _enum12;

typedef struct MobilityStateParameters {
    _enum12         t_Evaluation;
    _enum12         t_HystNormal;
    unsigned short  n_CellChangeMedium;
    unsigned short  n_CellChangeHigh;
} MobilityStateParameters;

typedef unsigned short  ReselectionThreshold;

typedef unsigned short  CellReselectionPriority;

typedef short           Q_RxLevMin;

typedef short           P_Max;

typedef enum AllowedMeasBandwidth {
    mbw6 = 0,
    mbw15 = 1,
    mbw25 = 2,
    mbw50 = 3,
    mbw75 = 4,
    mbw100 = 5
} AllowedMeasBandwidth;

typedef ossBoolean      PresenceAntennaPort1;

typedef struct NeighCellConfig {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} NeighCellConfig;

typedef unsigned short  T_Reselection;

typedef enum _enum13 {
    oDot25 = 0,
    oDot5 = 1,
    oDot75 = 2,
    lDot0 = 3
} _enum13;

typedef struct SpeedStateScaleFactors {
    _enum13         sf_Medium;
    _enum13         sf_High;
} SpeedStateScaleFactors;

typedef unsigned short  ReselectionThresholdQ_r9;

typedef short           Q_QualMin_r9;

typedef enum _enum14 {
    sf_Medium_dB_6 = 0,
    sf_Medium_dB_4 = 1,
    sf_Medium_dB_2 = 2,
    sf_Medium_dB0 = 3
} _enum14;

typedef struct SystemInformationBlockType3 {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType3_lateNonCriticalExtension_present 0x80
#       define      s_IntraSearch_v920_present 0x40
#       define      s_NonIntraSearch_v920_present 0x20
#       define      SystemInformationBlockType3_q_QualMin_r9_present 0x10
#       define      threshServingLowQ_r9_present 0x08
#       define      SystemInformationBlockType3_q_QualMinWB_r11_present 0x04
#       define      SystemInformationBlockType3_q_QualMinRSRQ_OnAllSymbols_r12_present 0x02
    struct {
        unsigned char   bit_mask;
#           define      speedStateReselectionPars_present 0x80
        enum {
            q_Hyst_dB0 = 0,
            q_Hyst_dB1 = 1,
            q_Hyst_dB2 = 2,
            q_Hyst_dB3 = 3,
            q_Hyst_dB4 = 4,
            q_Hyst_dB5 = 5,
            q_Hyst_dB6 = 6,
            q_Hyst_dB8 = 7,
            q_Hyst_dB10 = 8,
            q_Hyst_dB12 = 9,
            q_Hyst_dB14 = 10,
            q_Hyst_dB16 = 11,
            q_Hyst_dB18 = 12,
            q_Hyst_dB20 = 13,
            q_Hyst_dB22 = 14,
            q_Hyst_dB24 = 15
        } q_Hyst;
        struct {
            MobilityStateParameters mobilityStateParameters;
            struct {
                _enum14         sf_Medium;
                _enum14         sf_High;
            } q_HystSF;
        } speedStateReselectionPars;  /* optional; set in bit_mask
                                       * speedStateReselectionPars_present if
                                       * present */
                                      /* Need OP */
    } cellReselectionInfoCommon;
    struct {
        unsigned char   bit_mask;
#           define      s_NonIntraSearch_present 0x80
        ReselectionThreshold s_NonIntraSearch;  /* optional; set in bit_mask
                                                 * s_NonIntraSearch_present if
                                                 * present */
                                                /* Need OP */
        ReselectionThreshold threshServingLow;
        CellReselectionPriority cellReselectionPriority;
    } cellReselectionServingFreqInfo;
    struct {
        unsigned char   bit_mask;
#           define      intraFreqCellReselectionInfo_p_Max_present 0x80
#           define      s_IntraSearch_present 0x40
#           define      allowedMeasBandwidth_present 0x20
#           define      intraFreqCellReselectionInfo_t_ReselectionEUTRA_SF_present 0x10
        Q_RxLevMin      q_RxLevMin;
        P_Max           p_Max;  /* optional; set in bit_mask
                                 * intraFreqCellReselectionInfo_p_Max_present if
                                 * present */
                                /* Need OP */
        ReselectionThreshold s_IntraSearch;  /* optional; set in bit_mask
                                              * s_IntraSearch_present if
                                              * present */
                                             /* Need OP */
        AllowedMeasBandwidth allowedMeasBandwidth;  /* optional; set in bit_mask
                                              * allowedMeasBandwidth_present if
                                              * present */
                                                    /* Need OP */
        PresenceAntennaPort1 presenceAntennaPort1;
        NeighCellConfig neighCellConfig;
        T_Reselection   t_ReselectionEUTRA;
        SpeedStateScaleFactors t_ReselectionEUTRA_SF;  /* optional; set in
                                   * bit_mask
                * intraFreqCellReselectionInfo_t_ReselectionEUTRA_SF_present if
                * present */
                                                       /* Need OP */
    } intraFreqCellReselectionInfo;
    _octet1         lateNonCriticalExtension;  /* extension #1; optional; set in
                                   * bit_mask
              * SystemInformationBlockType3_lateNonCriticalExtension_present if
              * present */
    struct {
        ReselectionThreshold s_IntraSearchP_r9;
        ReselectionThresholdQ_r9 s_IntraSearchQ_r9;
    } s_IntraSearch_v920;  /* extension #2; optional; set in bit_mask
                            * s_IntraSearch_v920_present if present */
               /* Need OP */
    struct {
        ReselectionThreshold s_NonIntraSearchP_r9;
        ReselectionThresholdQ_r9 s_NonIntraSearchQ_r9;
    } s_NonIntraSearch_v920;  /* extension #2; optional; set in bit_mask
                               * s_NonIntraSearch_v920_present if present */
               /* Need OP */
    Q_QualMin_r9    q_QualMin_r9;  /* extension #2; optional; set in bit_mask
                          * SystemInformationBlockType3_q_QualMin_r9_present if
                          * present */
                                   /* Need OP */
    ReselectionThresholdQ_r9 threshServingLowQ_r9;  /* extension #2; optional;
                                   * set in bit_mask
                                   * threshServingLowQ_r9_present if present */
                                                    /* Need OP */
    Q_QualMin_r9    q_QualMinWB_r11;  /* extension #3; optional; set in bit_mask
                       * SystemInformationBlockType3_q_QualMinWB_r11_present if
                       * present */
                                      /* Cond WB-RSRQ */
    Q_QualMin_r9    q_QualMinRSRQ_OnAllSymbols_r12;  /* extension #4; optional;
                                   * set in bit_mask
        * SystemInformationBlockType3_q_QualMinRSRQ_OnAllSymbols_r12_present if
        * present */
                                                     /* Cond RSRQ */
} SystemInformationBlockType3;

typedef unsigned short  PhysCellId;

typedef struct PhysCellIdRange {
    unsigned char   bit_mask;
#       define      range_present 0x80
    PhysCellId      start;
    enum {
        range_n4 = 0,
        range_n8 = 1,
        range_n12 = 2,
        range_n16 = 3,
        range_n24 = 4,
        range_n32 = 5,
        range_n48 = 6,
        range_n64 = 7,
        n84 = 8,
        n96 = 9,
        n128 = 10,
        n168 = 11,
        n252 = 12,
        n504 = 13,
        range_spare2 = 14,
        range_spare1 = 15
    } range;  /* optional; set in bit_mask range_present if present */
              /* Need OP */
} PhysCellIdRange;

typedef struct SystemInformationBlockType4 {
    unsigned char   bit_mask;
#       define      intraFreqNeighCellList_present 0x80
#       define      intraFreqBlackCellList_present 0x40
#       define      csg_PhysCellIdRange_present 0x20
#       define      SystemInformationBlockType4_lateNonCriticalExtension_present 0x10
    struct IntraFreqNeighCellList *intraFreqNeighCellList;  /* optional; set in
                                   * bit_mask intraFreqNeighCellList_present if
                                   * present */
                                                            /* Need OR */
    struct IntraFreqBlackCellList *intraFreqBlackCellList;  /* optional; set in
                                   * bit_mask intraFreqBlackCellList_present if
                                   * present */
                                                            /* Need OR */
    PhysCellIdRange csg_PhysCellIdRange;  /* optional; set in bit_mask
                                           * csg_PhysCellIdRange_present if
                                           * present */
                                          /* Cond CSG */
    _octet1         lateNonCriticalExtension;  /* extension #1; optional; set in
                                   * bit_mask
              * SystemInformationBlockType4_lateNonCriticalExtension_present if
              * present */
} SystemInformationBlockType4;

typedef struct InterFreqCarrierFreqInfo_v8h0 {
    unsigned char   bit_mask;
#       define      InterFreqCarrierFreqInfo_v8h0_multiBandInfoList_present 0x80
    struct MultiBandInfoList *multiBandInfoList;  /* optional; set in bit_mask
                   * InterFreqCarrierFreqInfo_v8h0_multiBandInfoList_present if
                   * present */
                                                  /* Need OR */
} InterFreqCarrierFreqInfo_v8h0;

typedef struct InterFreqCarrierFreqInfo_v9e0 {
    unsigned char   bit_mask;
#       define      dl_CarrierFreq_v9e0_present 0x80
#       define      InterFreqCarrierFreqInfo_v9e0_multiBandInfoList_v9e0_present 0x40
    ARFCN_ValueEUTRA_v9e0 dl_CarrierFreq_v9e0;  /* optional; set in bit_mask
                                                 * dl_CarrierFreq_v9e0_present
                                                 * if present */
                                                /* Cond dl-FreqMax */
    struct MultiBandInfoList_v9e0 *multiBandInfoList_v9e0;  /* optional; set in
                                   * bit_mask
              * InterFreqCarrierFreqInfo_v9e0_multiBandInfoList_v9e0_present if
              * present */
                                                            /* Need OR */
} InterFreqCarrierFreqInfo_v9e0;

typedef struct SystemInformationBlockType5_v9e0_IEs {
    unsigned char   bit_mask;
#       define      interFreqCarrierFreqList_v9e0_present 0x80
#       define      SystemInformationBlockType5_v9e0_IEs_nonCriticalExtension_present 0x40
    struct _seqof2 {
        struct _seqof2  *next;
        InterFreqCarrierFreqInfo_v9e0 value;
    } *interFreqCarrierFreqList_v9e0;  /* optional; set in bit_mask
                                        * interFreqCarrierFreqList_v9e0_present
                                        * if present */
                                       /* Need OR */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType5_v9e0_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType5_v9e0_IEs;

typedef struct SystemInformationBlockType5_v8h0_IEs {
    unsigned char   bit_mask;
#       define      interFreqCarrierFreqList_v8h0_present 0x80
#       define      SystemInformationBlockType5_v8h0_IEs_nonCriticalExtension_present 0x40
    struct _seqof3 {
        struct _seqof3  *next;
        InterFreqCarrierFreqInfo_v8h0 value;
    } *interFreqCarrierFreqList_v8h0;  /* optional; set in bit_mask
                                        * interFreqCarrierFreqList_v8h0_present
                                        * if present */
                                       /* Need OP */
    SystemInformationBlockType5_v9e0_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
         * SystemInformationBlockType5_v8h0_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType5_v8h0_IEs;

typedef struct SystemInformationBlockType5 {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType5_lateNonCriticalExtension_present 0x80
#       define      interFreqCarrierFreqList_v1250_present 0x40
#       define      interFreqCarrierFreqListExt_r12_present 0x20
    struct InterFreqCarrierFreqList *interFreqCarrierFreqList;
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        SystemInformationBlockType5_v8h0_IEs *decoded;
    } lateNonCriticalExtension;  /* extension #1; optional; set in bit_mask
              * SystemInformationBlockType5_lateNonCriticalExtension_present if
              * present */
    struct InterFreqCarrierFreqList_v1250 *interFreqCarrierFreqList_v1250;  
                                        /* extension #2; optional; set in
                                   * bit_mask
                                   * interFreqCarrierFreqList_v1250_present if
                                   * present */
                                                                            /* Need OR */
    struct InterFreqCarrierFreqListExt_r12 *interFreqCarrierFreqListExt_r12;  
                                        /* extension #2; optional; set in
                                   * bit_mask
                                   * interFreqCarrierFreqListExt_r12_present if
                                   * present */
                                                                              /* Need OR */
} SystemInformationBlockType5;

typedef unsigned short  FreqBandIndicator_UTRA_FDD;

typedef struct _seqof4 {
    struct _seqof4  *next;
    FreqBandIndicator_UTRA_FDD value;
} *_seqof4;

typedef struct CarrierFreqInfoUTRA_FDD_v8h0 {
    unsigned char   bit_mask;
#       define      CarrierFreqInfoUTRA_FDD_v8h0_multiBandInfoList_present 0x80
    struct _seqof4  *multiBandInfoList;  /* optional; set in bit_mask
                    * CarrierFreqInfoUTRA_FDD_v8h0_multiBandInfoList_present if
                    * present */
                                         /* Need OR */
} CarrierFreqInfoUTRA_FDD_v8h0;

typedef struct SystemInformationBlockType6_v8h0_IEs {
    unsigned char   bit_mask;
#       define      carrierFreqListUTRA_FDD_v8h0_present 0x80
#       define      SystemInformationBlockType6_v8h0_IEs_nonCriticalExtension_present 0x40
    struct _seqof5 {
        struct _seqof5  *next;
        CarrierFreqInfoUTRA_FDD_v8h0 value;
    } *carrierFreqListUTRA_FDD_v8h0;  /* optional; set in bit_mask
                                       * carrierFreqListUTRA_FDD_v8h0_present if
                                       * present */
                                      /* Cond UTRA-FDD */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType6_v8h0_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType6_v8h0_IEs;

typedef struct CarrierFreqInfoUTRA_v1250 {
    unsigned char   bit_mask;
#       define      CarrierFreqInfoUTRA_v1250_reducedMeasPerformance_r12_present 0x80
    _enum3          reducedMeasPerformance_r12;  /* optional; set in bit_mask
              * CarrierFreqInfoUTRA_v1250_reducedMeasPerformance_r12_present if
              * present */
                                                 /* Need OP */
} CarrierFreqInfoUTRA_v1250;

typedef struct _seqof6 {
    struct _seqof6  *next;
    CarrierFreqInfoUTRA_v1250 value;
} *_seqof6;

typedef struct SystemInformationBlockType6 {
    unsigned char   bit_mask;
#       define      carrierFreqListUTRA_FDD_present 0x80
#       define      carrierFreqListUTRA_TDD_present 0x40
#       define      t_ReselectionUTRA_SF_present 0x20
#       define      SystemInformationBlockType6_lateNonCriticalExtension_present 0x10
#       define      carrierFreqListUTRA_FDD_v1250_present 0x08
#       define      carrierFreqListUTRA_TDD_v1250_present 0x04
#       define      carrierFreqListUTRA_FDD_Ext_r12_present 0x02
#       define      carrierFreqListUTRA_TDD_Ext_r12_present 0x01
    struct CarrierFreqListUTRA_FDD *carrierFreqListUTRA_FDD;  /* optional; set
                                   * in bit_mask carrierFreqListUTRA_FDD_present
                                   * if present */
                                                              /* Need OR */
    struct CarrierFreqListUTRA_TDD *carrierFreqListUTRA_TDD;  /* optional; set
                                   * in bit_mask carrierFreqListUTRA_TDD_present
                                   * if present */
                                                              /* Need OR */
    T_Reselection   t_ReselectionUTRA;
    SpeedStateScaleFactors t_ReselectionUTRA_SF;  /* optional; set in bit_mask
                                              * t_ReselectionUTRA_SF_present if
                                              * present */
                                                  /* Need OP */
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        SystemInformationBlockType6_v8h0_IEs *decoded;
    } lateNonCriticalExtension;  /* extension #1; optional; set in bit_mask
              * SystemInformationBlockType6_lateNonCriticalExtension_present if
              * present */
    struct _seqof6  *carrierFreqListUTRA_FDD_v1250;  /* extension #2; optional;
                                   * set in bit_mask
                                   * carrierFreqListUTRA_FDD_v1250_present if
                                   * present */
                                                     /* Cond UTRA-FDD */
    struct _seqof6  *carrierFreqListUTRA_TDD_v1250;  /* extension #2; optional;
                                   * set in bit_mask
                                   * carrierFreqListUTRA_TDD_v1250_present if
                                   * present */
                                                     /* Cond UTRA-TDD */
    struct CarrierFreqListUTRA_FDD_Ext_r12 *carrierFreqListUTRA_FDD_Ext_r12;  
                                        /* extension #2; optional; set in
                                   * bit_mask
                                   * carrierFreqListUTRA_FDD_Ext_r12_present if
                                   * present */
                                                                              /* Cond UTRA-FDD */
    struct CarrierFreqListUTRA_TDD_Ext_r12 *carrierFreqListUTRA_TDD_Ext_r12;  
                                        /* extension #2; optional; set in
                                   * bit_mask
                                   * carrierFreqListUTRA_TDD_Ext_r12_present if
                                   * present */
                                                                              /* Cond UTRA-TDD */
} SystemInformationBlockType6;

typedef struct SystemInformationBlockType7 {
    unsigned char   bit_mask;
#       define      t_ReselectionGERAN_SF_present 0x80
#       define      carrierFreqsInfoList_present 0x40
#       define      SystemInformationBlockType7_lateNonCriticalExtension_present 0x20
    T_Reselection   t_ReselectionGERAN;
    SpeedStateScaleFactors t_ReselectionGERAN_SF;  /* optional; set in bit_mask
                                             * t_ReselectionGERAN_SF_present if
                                             * present */
                                                   /* Need OR */
    struct CarrierFreqsInfoListGERAN *carrierFreqsInfoList;  /* optional; set in
                                   * bit_mask carrierFreqsInfoList_present if
                                   * present */
                                                             /* Need OR */
    _octet1         lateNonCriticalExtension;  /* extension #1; optional; set in
                                   * bit_mask
              * SystemInformationBlockType7_lateNonCriticalExtension_present if
              * present */
} SystemInformationBlockType7;

typedef struct SystemTimeInfoCDMA2000 {
    ossBoolean      cdma_EUTRA_Synchronisation;
    struct {
        unsigned short  choice;
#           define      synchronousSystemTime_chosen 1
#           define      asynchronousSystemTime_chosen 2
        union {
            _bit1           synchronousSystemTime;  /* to choose, set choice to
                                              * synchronousSystemTime_chosen */
            _bit1           asynchronousSystemTime;  /* to choose, set choice to
                                             * asynchronousSystemTime_chosen */
        } u;
    } cdma_SystemTime;
} SystemTimeInfoCDMA2000;

typedef unsigned short  PreRegistrationZoneIdHRPD;

typedef struct PreRegistrationInfoHRPD {
    unsigned char   bit_mask;
#       define      preRegistrationZoneId_present 0x80
#       define      secondaryPreRegistrationZoneIdList_present 0x40
    ossBoolean      preRegistrationAllowed;
    PreRegistrationZoneIdHRPD preRegistrationZoneId;  /* optional; set in
                                   * bit_mask preRegistrationZoneId_present if
                                   * present */
                                                      /* cond PreRegAllowed */
    struct SecondaryPreRegistrationZoneIdListHRPD *secondaryPreRegistrationZoneIdList;                                  /* optional; set in bit_mask
                                * secondaryPreRegistrationZoneIdList_present if
                                * present */
                                                                                        /* Need OR */
} PreRegistrationInfoHRPD;

typedef struct CellReselectionParametersCDMA2000 {
    unsigned char   bit_mask;
#       define      CellReselectionParametersCDMA2000_t_ReselectionCDMA2000_SF_present 0x80
    struct BandClassListCDMA2000 *bandClassList;
    struct NeighCellListCDMA2000 *neighCellList;
    T_Reselection   t_ReselectionCDMA2000;
    SpeedStateScaleFactors t_ReselectionCDMA2000_SF;  /* optional; set in
                                   * bit_mask
        * CellReselectionParametersCDMA2000_t_ReselectionCDMA2000_SF_present if
        * present */
                                                      /* Need OP */
} CellReselectionParametersCDMA2000;

typedef struct CSFB_RegistrationParam1XRTT {
    _bit1           sid;
    _bit1           nid;
    ossBoolean      multipleSID;
    ossBoolean      multipleNID;
    ossBoolean      homeReg;
    ossBoolean      foreignSIDReg;
    ossBoolean      foreignNIDReg;
    ossBoolean      parameterReg;
    ossBoolean      powerUpReg;
    _bit1           registrationPeriod;
    _bit1           registrationZone;
    _bit1           totalZone;
    _bit1           zoneTimer;
} CSFB_RegistrationParam1XRTT;

typedef struct CellReselectionParametersCDMA2000_v920 {
    struct NeighCellListCDMA2000_v920 *neighCellList_v920;
} CellReselectionParametersCDMA2000_v920;

typedef struct CSFB_RegistrationParam1XRTT_v920 {
    _enum3          powerDownReg_r9;
} CSFB_RegistrationParam1XRTT_v920;

typedef struct AC_BarringConfig1XRTT_r9 {
    unsigned short  ac_Barring0to9_r9;
    unsigned short  ac_Barring10_r9;
    unsigned short  ac_Barring11_r9;
    unsigned short  ac_Barring12_r9;
    unsigned short  ac_Barring13_r9;
    unsigned short  ac_Barring14_r9;
    unsigned short  ac_Barring15_r9;
    unsigned short  ac_BarringMsg_r9;
    unsigned short  ac_BarringReg_r9;
    unsigned short  ac_BarringEmg_r9;
} AC_BarringConfig1XRTT_r9;

typedef struct SystemInformationBlockType8 {
    unsigned short  bit_mask;
#       define      systemTimeInfo_present 0x8000
#       define      SystemInformationBlockType8_searchWindowSize_present 0x4000
#       define      parametersHRPD_present 0x2000
#       define      parameters1XRTT_present 0x1000
#       define      SystemInformationBlockType8_lateNonCriticalExtension_present 0x0800
#       define      csfb_SupportForDualRxUEs_r9_present 0x0400
#       define      cellReselectionParametersHRPD_v920_present 0x0200
#       define      cellReselectionParameters1XRTT_v920_present 0x0100
#       define      csfb_RegistrationParam1XRTT_v920_present 0x0080
#       define      ac_BarringConfig1XRTT_r9_present 0x0040
#       define      csfb_DualRxTxSupport_r10_present 0x0020
#       define      sib8_PerPLMN_List_r11_present 0x0010
    SystemTimeInfoCDMA2000 systemTimeInfo;  /* optional; set in bit_mask
                                             * systemTimeInfo_present if
                                             * present */
                                            /* Need OR */
    unsigned short  searchWindowSize;  /* optional; set in bit_mask
                      * SystemInformationBlockType8_searchWindowSize_present if
                      * present */
                                       /* Need OR */
    struct {
        unsigned char   bit_mask;
#           define      cellReselectionParametersHRPD_present 0x80
        PreRegistrationInfoHRPD preRegistrationInfoHRPD;
        CellReselectionParametersCDMA2000 cellReselectionParametersHRPD;  
                                        /* optional; set in bit_mask
                                         * cellReselectionParametersHRPD_present
                                         * if present */
                                                                          /* Need OR */
    } parametersHRPD;  /* optional; set in bit_mask parametersHRPD_present if
                        * present */
       /* Need OR */
    struct {
        unsigned char   bit_mask;
#           define      csfb_RegistrationParam1XRTT_present 0x80
#           define      longCodeState1XRTT_present 0x40
#           define      cellReselectionParameters1XRTT_present 0x20
        CSFB_RegistrationParam1XRTT csfb_RegistrationParam1XRTT;  /* optional;
                                   * set in bit_mask
                                   * csfb_RegistrationParam1XRTT_present if
                                   * present */
                                                                  /* Need OP */
        _bit1           longCodeState1XRTT;  /* optional; set in bit_mask
                                              * longCodeState1XRTT_present if
                                              * present */
                                             /* Need OR */
        CellReselectionParametersCDMA2000 cellReselectionParameters1XRTT;  
                                        /* optional; set in bit_mask
                                    * cellReselectionParameters1XRTT_present if
                                    * present */
                                                                           /* Need OR */
    } parameters1XRTT;  /* optional; set in bit_mask parameters1XRTT_present if
                         * present */
       /* Need OR */
    _octet1         lateNonCriticalExtension;  /* extension #1; optional; set in
                                   * bit_mask
              * SystemInformationBlockType8_lateNonCriticalExtension_present if
              * present */
    ossBoolean      csfb_SupportForDualRxUEs_r9;  /* extension #2; optional; set
                                   * in bit_mask
                                   * csfb_SupportForDualRxUEs_r9_present if
                                   * present */
                                                  /* Need OR */
    CellReselectionParametersCDMA2000_v920 cellReselectionParametersHRPD_v920;                                          /* extension #2; optional; set in
                                   * bit_mask
                                   * cellReselectionParametersHRPD_v920_present
                                   * if present */
                                                                                /* Cond NCL-HRPD */
    CellReselectionParametersCDMA2000_v920 cellReselectionParameters1XRTT_v920;                                         /* extension #2; optional; set in
                                   * bit_mask
                                   * cellReselectionParameters1XRTT_v920_present
                                   * if present */
                                                                                 /* Cond NCL-1XRTT */
    CSFB_RegistrationParam1XRTT_v920 csfb_RegistrationParam1XRTT_v920;  
                                  /* extension #2; optional; set in bit_mask
                                   * csfb_RegistrationParam1XRTT_v920_present if
                                   * present */
                                                                        /* Cond REG-1XRTT */
    AC_BarringConfig1XRTT_r9 ac_BarringConfig1XRTT_r9;  /* extension #2;
                                   * optional; set in bit_mask
                                   * ac_BarringConfig1XRTT_r9_present if
                                   * present */
                                                        /* Cond REG-1XRTT */
    _enum3          csfb_DualRxTxSupport_r10;  /* extension #3; optional; set in
                                   * bit_mask csfb_DualRxTxSupport_r10_present
                                   * if present */
                                               /* Cond REG-1XRTT */
    struct SIB8_PerPLMN_List_r11 *sib8_PerPLMN_List_r11;  /* extension #4;
                                   * optional; set in bit_mask
                                   * sib8_PerPLMN_List_r11_present if present */
                                                          /* Need OR */
} SystemInformationBlockType8;

typedef struct SystemInformationBlockType9 {
    unsigned char   bit_mask;
#       define      hnb_Name_present 0x80
#       define      SystemInformationBlockType9_lateNonCriticalExtension_present 0x40
    struct {
        unsigned short  length;
        unsigned char   value[48];
    } hnb_Name;  /* optional; set in bit_mask hnb_Name_present if present */
                 /* Need OR */
    _octet1         lateNonCriticalExtension;  /* extension #1; optional; set in
                                   * bit_mask
              * SystemInformationBlockType9_lateNonCriticalExtension_present if
              * present */
} SystemInformationBlockType9;

typedef struct _octet2 {
    unsigned short  length;
    unsigned char   value[2];
} _octet2;

typedef struct SystemInformationBlockType10 {
    unsigned char   bit_mask;
#       define      dummy_present 0x80
#       define      SystemInformationBlockType10_lateNonCriticalExtension_present 0x40
    _bit1           messageIdentifier;
    _bit1           serialNumber;
    _octet2         warningType;
    struct {
        unsigned short  length;
        unsigned char   value[50];
    } dummy;  /* optional; set in bit_mask dummy_present if present */
              /* Need OP */
    _octet1         lateNonCriticalExtension;  /* extension #1; optional; set in
                                   * bit_mask
             * SystemInformationBlockType10_lateNonCriticalExtension_present if
             * present */
} SystemInformationBlockType10;

typedef enum _enum15 {
    notLastSegment = 0,
    lastSegment = 1
} _enum15;

typedef struct _octet3 {
    unsigned short  length;
    unsigned char   value[1];
} _octet3;

typedef struct SystemInformationBlockType11 {
    unsigned char   bit_mask;
#       define      dataCodingScheme_present 0x80
#       define      SystemInformationBlockType11_lateNonCriticalExtension_present 0x40
    _bit1           messageIdentifier;
    _bit1           serialNumber;
    _enum15         warningMessageSegmentType;
    unsigned short  warningMessageSegmentNumber;
    _octet1         warningMessageSegment;
    _octet3         dataCodingScheme;  /* optional; set in bit_mask
                                        * dataCodingScheme_present if present */
                                       /* Cond Segment1 */
    _octet1         lateNonCriticalExtension;  /* extension #1; optional; set in
                                   * bit_mask
             * SystemInformationBlockType11_lateNonCriticalExtension_present if
             * present */
} SystemInformationBlockType11;

typedef struct SystemInformationBlockType12_r9 {
    unsigned char   bit_mask;
#       define      dataCodingScheme_r9_present 0x80
#       define      SystemInformationBlockType12_r9_lateNonCriticalExtension_present 0x40
    _bit1           messageIdentifier_r9;
    _bit1           serialNumber_r9;
    _enum15         warningMessageSegmentType_r9;
    unsigned short  warningMessageSegmentNumber_r9;
    _octet1         warningMessageSegment_r9;
    _octet3         dataCodingScheme_r9;  /* optional; set in bit_mask
                                           * dataCodingScheme_r9_present if
                                           * present */
                                          /* Cond Segment1 */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
          * SystemInformationBlockType12_r9_lateNonCriticalExtension_present if
          * present */
} SystemInformationBlockType12_r9;

typedef enum _enum16 {
    notificationRepetitionCoeff_r9_n2 = 0,
    notificationRepetitionCoeff_r9_n4 = 1
} _enum16;

typedef struct MBMS_NotificationConfig_r9 {
    _enum16         notificationRepetitionCoeff_r9;
    unsigned short  notificationOffset_r9;
    unsigned short  notificationSF_Index_r9;
} MBMS_NotificationConfig_r9;

typedef struct SystemInformationBlockType13_r9 {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType13_r9_lateNonCriticalExtension_present 0x80
    struct MBSFN_AreaInfoList_r9 *mbsfn_AreaInfoList_r9;
    MBMS_NotificationConfig_r9 notificationConfig_r9;
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
          * SystemInformationBlockType13_r9_lateNonCriticalExtension_present if
          * present */
} SystemInformationBlockType13_r9;

typedef struct EAB_Config_r11 {
    enum {
        eab_Category_r11_a = 0,
        eab_Category_r11_b = 1,
        eab_Category_r11_c = 2
    } eab_Category_r11;
    _bit1           eab_BarringBitmap_r11;
} EAB_Config_r11;

typedef struct EAB_ConfigPLMN_r11 {
    unsigned char   bit_mask;
#       define      eab_Config_r11_present 0x80
    EAB_Config_r11  eab_Config_r11;  /* optional; set in bit_mask
                                      * eab_Config_r11_present if present */
                                     /* Need OR */
} EAB_ConfigPLMN_r11;

typedef struct SystemInformationBlockType14_r11 {
    unsigned char   bit_mask;
#       define      eab_Param_r11_present 0x80
#       define      SystemInformationBlockType14_r11_lateNonCriticalExtension_present 0x40
    struct {
        unsigned short  choice;
#           define      eab_Common_r11_chosen 1
#           define      eab_PerPLMN_List_r11_chosen 2
        union {
            EAB_Config_r11  eab_Common_r11;  /* to choose, set choice to
                                              * eab_Common_r11_chosen */
            struct _seqof7 {
                struct _seqof7  *next;
                EAB_ConfigPLMN_r11 value;
            } *eab_PerPLMN_List_r11;  /* to choose, set choice to
                                       * eab_PerPLMN_List_r11_chosen */
        } u;
    } eab_Param_r11;  /* optional; set in bit_mask eab_Param_r11_present if
                       * present */
 /* Need OR */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType14_r11_lateNonCriticalExtension_present if
         * present */
} SystemInformationBlockType14_r11;

typedef struct SystemInformationBlockType15_r11 {
    unsigned char   bit_mask;
#       define      mbms_SAI_IntraFreq_r11_present 0x80
#       define      mbms_SAI_InterFreqList_r11_present 0x40
#       define      SystemInformationBlockType15_r11_lateNonCriticalExtension_present 0x20
#       define      mbms_SAI_InterFreqList_v1140_present 0x10
    struct MBMS_SAI_List_r11 *mbms_SAI_IntraFreq_r11;  /* optional; set in
                                   * bit_mask mbms_SAI_IntraFreq_r11_present if
                                   * present */
                                                       /* Need OR */
    struct MBMS_SAI_InterFreqList_r11 *mbms_SAI_InterFreqList_r11;  
                                  /* optional; set in bit_mask
                                   * mbms_SAI_InterFreqList_r11_present if
                                   * present */
                                                                    /* Need OR */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType15_r11_lateNonCriticalExtension_present if
         * present */
    struct MBMS_SAI_InterFreqList_v1140 *mbms_SAI_InterFreqList_v1140;  
                                  /* extension #1; optional; set in bit_mask
                                   * mbms_SAI_InterFreqList_v1140_present if
                                   * present */
                                                                        /* Cond InterFreq */
} SystemInformationBlockType15_r11;

typedef struct SystemInformationBlockType16_r11 {
    unsigned char   bit_mask;
#       define      timeInfo_r11_present 0x80
#       define      SystemInformationBlockType16_r11_lateNonCriticalExtension_present 0x40
    struct {
        unsigned char   bit_mask;
#           define      dayLightSavingTime_r11_present 0x80
#           define      leapSeconds_r11_present 0x40
#           define      localTimeOffset_r11_present 0x20
        ULONG_LONG      timeInfoUTC_r11;
        _bit1           dayLightSavingTime_r11;  /* optional; set in bit_mask
                                            * dayLightSavingTime_r11_present if
                                            * present */
                                                 /* Need OR */
        short           leapSeconds_r11;  /* optional; set in bit_mask
                                           * leapSeconds_r11_present if
                                           * present */
                                          /* Need OR */
        short           localTimeOffset_r11;  /* optional; set in bit_mask
                                               * localTimeOffset_r11_present if
                                               * present */
                                              /* Need OR */
    } timeInfo_r11;  /* optional; set in bit_mask timeInfo_r11_present if
                      * present */
       /* Need OR */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType16_r11_lateNonCriticalExtension_present if
         * present */
} SystemInformationBlockType16_r11;

typedef unsigned short  RSRP_Range;

typedef unsigned short  RSRQ_Range;

typedef enum WLAN_backhaulRate_r12 {
    r0 = 0,
    WLAN_backhaulRate_r12_r4 = 1,
    WLAN_backhaulRate_r12_r8 = 2,
    WLAN_backhaulRate_r12_r16 = 3,
    WLAN_backhaulRate_r12_r32 = 4,
    WLAN_backhaulRate_r12_r64 = 5,
    r128 = 6,
    r256 = 7,
    r512 = 8,
    r1024 = 9,
    r2048 = 10,
    r4096 = 11,
    r8192 = 12,
    r16384 = 13,
    r32768 = 14,
    r65536 = 15,
    r131072 = 16,
    r262144 = 17,
    r524288 = 18,
    r1048576 = 19,
    r2097152 = 20,
    r4194304 = 21,
    r8388608 = 22,
    r16777216 = 23,
    r33554432 = 24,
    r67108864 = 25,
    r134217728 = 26,
    r268435456 = 27,
    r536870912 = 28,
    r1073741824 = 29,
    r2147483648 = 30,
    r4294967296 = 31
} WLAN_backhaulRate_r12;

typedef struct WLAN_OffloadConfig_r12 {
    unsigned short  bit_mask;
#       define      thresholdRSRP_r12_present 0x8000
#       define      thresholdRSRQ_r12_present 0x4000
#       define      thresholdRSRQ_OnAllSymbolsWithWB_r12_present 0x2000
#       define      thresholdRSRQ_OnAllSymbols_r12_present 0x1000
#       define      thresholdRSRQ_WB_r12_present 0x0800
#       define      thresholdChannelUtilization_r12_present 0x0400
#       define      thresholdBackhaul_Bandwidth_r12_present 0x0200
#       define      thresholdBeaconRSSI_r12_present 0x0100
#       define      offloadPreferenceIndicator_r12_present 0x0080
#       define      t_SteeringWLAN_r12_present 0x0040
    struct {
        RSRP_Range      thresholdRSRP_Low_r12;
        RSRP_Range      thresholdRSRP_High_r12;
    } thresholdRSRP_r12;  /* optional; set in bit_mask thresholdRSRP_r12_present
                           * if present */
 /* Need OR */
    struct {
        RSRQ_Range      thresholdRSRQ_Low_r12;
        RSRQ_Range      thresholdRSRQ_High_r12;
    } thresholdRSRQ_r12;  /* optional; set in bit_mask thresholdRSRQ_r12_present
                           * if present */
 /* Need OR */
    struct {
        RSRQ_Range      thresholdRSRQ_OnAllSymbolsWithWB_Low_r12;
        RSRQ_Range      thresholdRSRQ_OnAllSymbolsWithWB_High_r12;
    } thresholdRSRQ_OnAllSymbolsWithWB_r12;  /* optional; set in bit_mask
                              * thresholdRSRQ_OnAllSymbolsWithWB_r12_present if
                              * present */
 /* Need OP */
    struct {
        RSRQ_Range      thresholdRSRQ_OnAllSymbolsLow_r12;
        RSRQ_Range      thresholdRSRQ_OnAllSymbolsHigh_r12;
    } thresholdRSRQ_OnAllSymbols_r12;  /* optional; set in bit_mask
                                        * thresholdRSRQ_OnAllSymbols_r12_present
                                        * if present */
 /* Need OP */
    struct {
        RSRQ_Range      thresholdRSRQ_WB_Low_r12;
        RSRQ_Range      thresholdRSRQ_WB_High_r12;
    } thresholdRSRQ_WB_r12;  /* optional; set in bit_mask
                              * thresholdRSRQ_WB_r12_present if present */
 /* Need OP */
    struct {
        unsigned short  thresholdChannelUtilizationLow_r12;
        unsigned short  thresholdChannelUtilizationHigh_r12;
    } thresholdChannelUtilization_r12;  /* optional; set in bit_mask
                                   * thresholdChannelUtilization_r12_present if
                                   * present */
 /* Need OR */
    struct {
        WLAN_backhaulRate_r12 thresholdBackhaulDL_BandwidthLow_r12;
        WLAN_backhaulRate_r12 thresholdBackhaulDL_BandwidthHigh_r12;
        WLAN_backhaulRate_r12 thresholdBackhaulUL_BandwidthLow_r12;
        WLAN_backhaulRate_r12 thresholdBackhaulUL_BandwidthHigh_r12;
    } thresholdBackhaul_Bandwidth_r12;  /* optional; set in bit_mask
                                   * thresholdBackhaul_Bandwidth_r12_present if
                                   * present */
 /* Need OR */
    struct {
        unsigned short  thresholdBeaconRSSI_Low_r12;
        unsigned short  thresholdBeaconRSSI_High_r12;
    } thresholdBeaconRSSI_r12;  /* optional; set in bit_mask
                                 * thresholdBeaconRSSI_r12_present if present */
 /* Need OR */
    _bit1           offloadPreferenceIndicator_r12;  /* optional; set in
                                   * bit_mask
                                   * offloadPreferenceIndicator_r12_present if
                                   * present */
                                                     /* Need OR */
    T_Reselection   t_SteeringWLAN_r12;  /* optional; set in bit_mask
                                          * t_SteeringWLAN_r12_present if
                                          * present */
                                         /* Need OR */
} WLAN_OffloadConfig_r12;

typedef struct WLAN_OffloadInfoPerPLMN_r12 {
    unsigned char   bit_mask;
#       define      wlan_OffloadConfigCommon_r12_present 0x80
#       define      wlan_Id_List_r12_present 0x40
    WLAN_OffloadConfig_r12 wlan_OffloadConfigCommon_r12;  /* optional; set in
                                   * bit_mask
                                   * wlan_OffloadConfigCommon_r12_present if
                                   * present */
                                                          /* Need OR */
    struct WLAN_Id_List_r12 *wlan_Id_List_r12;  /* optional; set in bit_mask
                                                 * wlan_Id_List_r12_present if
                                                 * present */
                                                /* Need OR */
} WLAN_OffloadInfoPerPLMN_r12;

typedef struct SystemInformationBlockType17_r12 {
    unsigned char   bit_mask;
#       define      wlan_OffloadInfoPerPLMN_List_r12_present 0x80
#       define      SystemInformationBlockType17_r12_lateNonCriticalExtension_present 0x40
    struct _seqof8 {
        struct _seqof8  *next;
        WLAN_OffloadInfoPerPLMN_r12 value;
    } *wlan_OffloadInfoPerPLMN_List_r12;  /* optional; set in bit_mask
                                  * wlan_OffloadInfoPerPLMN_List_r12_present if
                                  * present */
                                          /* Need OR */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType17_r12_lateNonCriticalExtension_present if
         * present */
} SystemInformationBlockType17_r12;

typedef struct SystemInformationBlockType18_r12 {
    unsigned char   bit_mask;
#       define      commConfig_r12_present 0x80
#       define      SystemInformationBlockType18_r12_lateNonCriticalExtension_present 0x40
    struct {
        unsigned char   bit_mask;
#           define      commTxPoolNormalCommon_r12_present 0x80
#           define      commTxPoolExceptional_r12_present 0x40
#           define      commSyncConfig_r12_present 0x20
        struct SL_CommRxPoolList_r12 *commRxPool_r12;
        struct SL_CommTxPoolList_r12 *commTxPoolNormalCommon_r12;  /* optional;
                                   * set in bit_mask
                                   * commTxPoolNormalCommon_r12_present if
                                   * present */
                                                                   /* Need OR */
        struct SL_CommTxPoolList_r12 *commTxPoolExceptional_r12;  /* optional;
                                   * set in bit_mask
                                   * commTxPoolExceptional_r12_present if
                                   * present */
                                                                  /* Need OR */
        struct SL_SyncConfigList_r12 *commSyncConfig_r12;  /* optional; set in
                                   * bit_mask commSyncConfig_r12_present if
                                   * present */
                                                           /* Need OR */
    } commConfig_r12;  /* optional; set in bit_mask commConfig_r12_present if
                        * present */
       /* Need OR */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType18_r12_lateNonCriticalExtension_present if
         * present */
} SystemInformationBlockType18_r12;

typedef struct SystemInformationBlockType19_r12 {
    unsigned char   bit_mask;
#       define      discConfig_r12_present 0x80
#       define      discInterFreqList_r12_present 0x40
#       define      SystemInformationBlockType19_r12_lateNonCriticalExtension_present 0x20
    struct {
        unsigned char   bit_mask;
#           define      discTxPoolCommon_r12_present 0x80
#           define      discTxPowerInfo_r12_present 0x40
#           define      discSyncConfig_r12_present 0x20
        struct SL_DiscRxPoolList_r12 *discRxPool_r12;
        struct SL_DiscTxPoolList_r12 *discTxPoolCommon_r12;  /* optional; set in
                                   * bit_mask discTxPoolCommon_r12_present if
                                   * present */
                                                             /* Need OR */
        struct SL_DiscTxPowerInfoList_r12 *discTxPowerInfo_r12;  /* optional;
                                   * set in bit_mask discTxPowerInfo_r12_present
                                   * if present */
                                                                 /* Cond Tx */
        struct SL_SyncConfigList_r12 *discSyncConfig_r12;  /* optional; set in
                                   * bit_mask discSyncConfig_r12_present if
                                   * present */
                                                           /* Need OR */
    } discConfig_r12;  /* optional; set in bit_mask discConfig_r12_present if
                        * present */
       /* Need OR */
    struct SL_CarrierFreqInfoList_r12 *discInterFreqList_r12;  /* optional; set
                                   * in bit_mask discInterFreqList_r12_present
                                   * if present */
                                                               /* Need OR */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType19_r12_lateNonCriticalExtension_present if
         * present */
} SystemInformationBlockType19_r12;

typedef struct SystemInformation_v8a0_IEs {
    unsigned char   bit_mask;
#       define      SystemInformation_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      SystemInformation_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
               * SystemInformation_v8a0_IEs_lateNonCriticalExtension_present if
               * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                   * SystemInformation_v8a0_IEs_nonCriticalExtension_present if
                   * present */
} SystemInformation_v8a0_IEs;

typedef struct SystemInformation_r8_IEs {
    unsigned char   bit_mask;
#       define      SystemInformation_r8_IEs_nonCriticalExtension_present 0x80
    struct _seqof9 {
        struct _seqof9  *next;
        struct {
            unsigned short  choice;
#               define      sib2_chosen 1
#               define      sib3_chosen 2
#               define      sib4_chosen 3
#               define      sib5_chosen 4
#               define      sib6_chosen 5
#               define      sib7_chosen 6
#               define      sib8_chosen 7
#               define      sib9_chosen 8
#               define      sib10_chosen 9
#               define      sib11_chosen 10
#               define      sib12_v920_chosen 11
#               define      sib13_v920_chosen 12
#               define      sib14_v1130_chosen 13
#               define      sib15_v1130_chosen 14
#               define      sib16_v1130_chosen 15
#               define      sib17_v1250_chosen 16
#               define      sib18_v1250_chosen 17
#               define      sib19_v1250_chosen 18
            union {
                SystemInformationBlockType2 sib2;  /* to choose, set choice to
                                                    * sib2_chosen */
                SystemInformationBlockType3 sib3;  /* to choose, set choice to
                                                    * sib3_chosen */
                SystemInformationBlockType4 sib4;  /* to choose, set choice to
                                                    * sib4_chosen */
                SystemInformationBlockType5 sib5;  /* to choose, set choice to
                                                    * sib5_chosen */
                SystemInformationBlockType6 sib6;  /* to choose, set choice to
                                                    * sib6_chosen */
                SystemInformationBlockType7 sib7;  /* to choose, set choice to
                                                    * sib7_chosen */
                SystemInformationBlockType8 sib8;  /* to choose, set choice to
                                                    * sib8_chosen */
                SystemInformationBlockType9 sib9;  /* to choose, set choice to
                                                    * sib9_chosen */
                SystemInformationBlockType10 sib10;  /* to choose, set choice to
                                                      * sib10_chosen */
                SystemInformationBlockType11 sib11;  /* to choose, set choice to
                                                      * sib11_chosen */
                SystemInformationBlockType12_r9 sib12_v920;  /* extension #1; to
                                   * choose, set choice to sib12_v920_chosen */
                SystemInformationBlockType13_r9 sib13_v920;  /* extension #2; to
                                   * choose, set choice to sib13_v920_chosen */
                SystemInformationBlockType14_r11 sib14_v1130;  /* extension #3;
                                   * to choose, set choice to
                                   * sib14_v1130_chosen */
                SystemInformationBlockType15_r11 sib15_v1130;  /* extension #4;
                                   * to choose, set choice to
                                   * sib15_v1130_chosen */
                SystemInformationBlockType16_r11 sib16_v1130;  /* extension #5;
                                   * to choose, set choice to
                                   * sib16_v1130_chosen */
                SystemInformationBlockType17_r12 sib17_v1250;  /* extension #6;
                                   * to choose, set choice to
                                   * sib17_v1250_chosen */
                SystemInformationBlockType18_r12 sib18_v1250;  /* extension #7;
                                   * to choose, set choice to
                                   * sib18_v1250_chosen */
                SystemInformationBlockType19_r12 sib19_v1250;  /* extension #8;
                                   * to choose, set choice to
                                   * sib19_v1250_chosen */
            } u;
        } value;
    } *sib_TypeAndInfo;
    SystemInformation_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                     * SystemInformation_r8_IEs_nonCriticalExtension_present if
                     * present */
} SystemInformation_r8_IEs;

typedef struct SystemInformation {
    struct {
        unsigned short  choice;
#           define      systemInformation_r8_chosen 1
#           define      SystemInformation_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            SystemInformation_r8_IEs systemInformation_r8;  /* to choose, set
                                     * choice to systemInformation_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
      * SystemInformation_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SystemInformation;

typedef struct TrackingAreaCode {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} TrackingAreaCode;

typedef struct CellIdentity {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} CellIdentity;

typedef struct CSG_Identity {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} CSG_Identity;

typedef unsigned short  FreqBandIndicator;

typedef struct TDD_Config {
    enum {
        subframeAssignment_sa0 = 0,
        subframeAssignment_sa1 = 1,
        subframeAssignment_sa2 = 2,
        subframeAssignment_sa3 = 3,
        subframeAssignment_sa4 = 4,
        subframeAssignment_sa5 = 5,
        subframeAssignment_sa6 = 6
    } subframeAssignment;
    enum {
        ssp0 = 0,
        ssp1 = 1,
        ssp2 = 2,
        ssp3 = 3,
        ssp4 = 4,
        ssp5 = 5,
        ssp6 = 6,
        specialSubframePatterns_ssp7 = 7,
        ssp8 = 8
    } specialSubframePatterns;
} TDD_Config;

typedef unsigned short  FreqBandIndicator_v9e0;

typedef struct SystemInformationBlockType1_v9e0_IEs {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType1_v9e0_IEs_freqBandIndicator_v9e0_present 0x80
#       define      SystemInformationBlockType1_v9e0_IEs_multiBandInfoList_v9e0_present 0x40
#       define      SystemInformationBlockType1_v9e0_IEs_nonCriticalExtension_present 0x20
    FreqBandIndicator_v9e0 freqBandIndicator_v9e0;  /* optional; set in bit_mask
       * SystemInformationBlockType1_v9e0_IEs_freqBandIndicator_v9e0_present if
       * present */
                                                    /* Cond FBI-max */
    struct MultiBandInfoList_v9e0 *multiBandInfoList_v9e0;  /* optional; set in
                                   * bit_mask
       * SystemInformationBlockType1_v9e0_IEs_multiBandInfoList_v9e0_present if
       * present */
                                                            /* Cond mFBI-max */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
         * SystemInformationBlockType1_v9e0_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType1_v9e0_IEs;

/* Late non critical extensions */
typedef struct SystemInformationBlockType1_v8h0_IEs {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType1_v8h0_IEs_multiBandInfoList_present 0x80
#       define      SystemInformationBlockType1_v8h0_IEs_nonCriticalExtension_present 0x40
    struct MultiBandInfoList *multiBandInfoList;  /* optional; set in bit_mask
            * SystemInformationBlockType1_v8h0_IEs_multiBandInfoList_present if
            * present */
                                                  /* Need OR */
    SystemInformationBlockType1_v9e0_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
         * SystemInformationBlockType1_v8h0_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType1_v8h0_IEs;

typedef struct CellSelectionInfo_v920 {
    unsigned char   bit_mask;
#       define      q_QualMinOffset_r9_present 0x80
    Q_QualMin_r9    q_QualMin_r9;
    unsigned short  q_QualMinOffset_r9;  /* optional; set in bit_mask
                                          * q_QualMinOffset_r9_present if
                                          * present */
                                         /* Need OP */
} CellSelectionInfo_v920;

typedef struct TDD_Config_v1130 {
    enum {
        specialSubframePatterns_v1130_ssp7 = 0,
        ssp9 = 1
    } specialSubframePatterns_v1130;
} TDD_Config_v1130;

typedef struct CellSelectionInfo_v1130 {
    Q_QualMin_r9    q_QualMinWB_r11;
} CellSelectionInfo_v1130;

typedef struct CellSelectionInfo_v1250 {
    Q_QualMin_r9    q_QualMinRSRQ_OnAllSymbols_r12;
} CellSelectionInfo_v1250;

typedef struct SystemInformationBlockType1_v1250_IEs {
    unsigned char   bit_mask;
#       define      cellSelectionInfo_v1250_present 0x80
#       define      freqBandIndicatorPriority_r12_present 0x40
#       define      SystemInformationBlockType1_v1250_IEs_nonCriticalExtension_present 0x20
    struct {
        unsigned char   bit_mask;
#           define      category0Allowed_r12_present 0x80
        _enum3          category0Allowed_r12;  /* optional; set in bit_mask
                                                * category0Allowed_r12_present
                                                * if present */
                                               /* Need OP */
    } cellAccessRelatedInfo_v1250;
    CellSelectionInfo_v1250 cellSelectionInfo_v1250;  /* optional; set in
                                   * bit_mask cellSelectionInfo_v1250_present if
                                   * present */
                                                      /* Cond RSRQ2 */
    _enum3          freqBandIndicatorPriority_r12;  /* optional; set in bit_mask
                                     * freqBandIndicatorPriority_r12_present if
                                     * present */
       /* Cond mFBI */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
        * SystemInformationBlockType1_v1250_IEs_nonCriticalExtension_present if
        * present */
} SystemInformationBlockType1_v1250_IEs;

typedef struct SystemInformationBlockType1_v1130_IEs {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType1_v1130_IEs_tdd_Config_v1130_present 0x80
#       define      cellSelectionInfo_v1130_present 0x40
#       define      SystemInformationBlockType1_v1130_IEs_nonCriticalExtension_present 0x20
    TDD_Config_v1130 tdd_Config_v1130;  /* optional; set in bit_mask
            * SystemInformationBlockType1_v1130_IEs_tdd_Config_v1130_present if
            * present */
                                        /* Cond TDD-OR */
    CellSelectionInfo_v1130 cellSelectionInfo_v1130;  /* optional; set in
                                   * bit_mask cellSelectionInfo_v1130_present if
                                   * present */
                                                      /* Cond WB-RSRQ */
    SystemInformationBlockType1_v1250_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
        * SystemInformationBlockType1_v1130_IEs_nonCriticalExtension_present if
        * present */
} SystemInformationBlockType1_v1130_IEs;

/* Regular non critical extensions */
typedef struct SystemInformationBlockType1_v920_IEs {
    unsigned char   bit_mask;
#       define      ims_EmergencySupport_r9_present 0x80
#       define      cellSelectionInfo_v920_present 0x40
#       define      SystemInformationBlockType1_v920_IEs_nonCriticalExtension_present 0x20
    _enum3          ims_EmergencySupport_r9;  /* optional; set in bit_mask
                                               * ims_EmergencySupport_r9_present
                                               * if present */
       /* Need OR */
    CellSelectionInfo_v920 cellSelectionInfo_v920;  /* optional; set in bit_mask
                                            * cellSelectionInfo_v920_present if
                                            * present */
                                                    /* Cond RSRQ */
    SystemInformationBlockType1_v1130_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
         * SystemInformationBlockType1_v920_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType1_v920_IEs;

typedef struct SystemInformationBlockType1_v890_IEs {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType1_v890_IEs_lateNonCriticalExtension_present 0x80
#       define      SystemInformationBlockType1_v890_IEs_nonCriticalExtension_present 0x40
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        SystemInformationBlockType1_v8h0_IEs *decoded;
    } lateNonCriticalExtension;  /* optional; set in bit_mask
     * SystemInformationBlockType1_v890_IEs_lateNonCriticalExtension_present if
     * present */
    SystemInformationBlockType1_v920_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
         * SystemInformationBlockType1_v890_IEs_nonCriticalExtension_present if
         * present */
} SystemInformationBlockType1_v890_IEs;

typedef struct SystemInformationBlockType1 {
    unsigned char   bit_mask;
#       define      SystemInformationBlockType1_p_Max_present 0x80
#       define      SystemInformationBlockType1_tdd_Config_present 0x40
#       define      SystemInformationBlockType1_nonCriticalExtension_present 0x20
    struct {
        unsigned char   bit_mask;
#           define      csg_Identity_present 0x80
        struct PLMN_IdentityList *plmn_IdentityList;
        TrackingAreaCode trackingAreaCode;
        CellIdentity    cellIdentity;
        enum {
            barred = 0,
            notBarred = 1
        } cellBarred;
        enum {
            allowed = 0,
            notAllowed = 1
        } intraFreqReselection;
        ossBoolean      csg_Indication;
        CSG_Identity    csg_Identity;  /* optional; set in bit_mask
                                        * csg_Identity_present if present */
                                       /* Need OR */
    } cellAccessRelatedInfo;
    struct {
        unsigned char   bit_mask;
#           define      q_RxLevMinOffset_present 0x80
        Q_RxLevMin      q_RxLevMin;
        unsigned short  q_RxLevMinOffset;  /* optional; set in bit_mask
                                            * q_RxLevMinOffset_present if
                                            * present */
                                           /* Need OP */
    } cellSelectionInfo;
    P_Max           p_Max;  /* optional; set in bit_mask
                             * SystemInformationBlockType1_p_Max_present if
                             * present */
                            /* Need OP */
    FreqBandIndicator freqBandIndicator;
    struct SchedulingInfoList *schedulingInfoList;
    TDD_Config      tdd_Config;  /* optional; set in bit_mask
                            * SystemInformationBlockType1_tdd_Config_present if
                            * present */
                                 /* Cond TDD */
    enum {
        ms1 = 0,
        ms2 = 1,
        si_WindowLength_ms5 = 2,
        si_WindowLength_ms10 = 3,
        si_WindowLength_ms15 = 4,
        si_WindowLength_ms20 = 5,
        si_WindowLength_ms40 = 6
    } si_WindowLength;
    unsigned short  systemInfoValueTag;
    SystemInformationBlockType1_v890_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
                  * SystemInformationBlockType1_nonCriticalExtension_present if
                  * present */
} SystemInformationBlockType1;

typedef struct BCCH_DL_SCH_MessageType {
    unsigned short  choice;
#       define      BCCH_DL_SCH_MessageType_c1_chosen 1
#       define      BCCH_DL_SCH_MessageType_messageClassExtension_chosen 2
    union {
        struct _choice1 {
            unsigned short  choice;
#               define      systemInformation_chosen 1
#               define      systemInformationBlockType1_chosen 2
            union {
                SystemInformation systemInformation;  /* to choose, set choice
                                               * to systemInformation_chosen */
                SystemInformationBlockType1 systemInformationBlockType1;  
                                        /* to choose, set choice to
                                        * systemInformationBlockType1_chosen */
            } u;
        } c1;  /* to choose, set choice to BCCH_DL_SCH_MessageType_c1_chosen */
        _seq2           messageClassExtension;  /* to choose, set choice to
                      * BCCH_DL_SCH_MessageType_messageClassExtension_chosen */
    } u;
} BCCH_DL_SCH_MessageType;

typedef struct BCCH_DL_SCH_Message {
    BCCH_DL_SCH_MessageType message;
} BCCH_DL_SCH_Message;

typedef struct MBSFNAreaConfiguration_v1250_IEs {
    unsigned char   bit_mask;
#       define      pmch_InfoListExt_r12_present 0x80
#       define      MBSFNAreaConfiguration_v1250_IEs_nonCriticalExtension_present 0x40
    struct PMCH_InfoListExt_r12 *pmch_InfoListExt_r12;  /* optional; set in
                                   * bit_mask pmch_InfoListExt_r12_present if
                                   * present */
                                                        /* Need OR */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
             * MBSFNAreaConfiguration_v1250_IEs_nonCriticalExtension_present if
             * present */
} MBSFNAreaConfiguration_v1250_IEs;

typedef struct MBSFNAreaConfiguration_v930_IEs {
    unsigned char   bit_mask;
#       define      MBSFNAreaConfiguration_v930_IEs_lateNonCriticalExtension_present 0x80
#       define      MBSFNAreaConfiguration_v930_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
          * MBSFNAreaConfiguration_v930_IEs_lateNonCriticalExtension_present if
          * present */
    MBSFNAreaConfiguration_v1250_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
              * MBSFNAreaConfiguration_v930_IEs_nonCriticalExtension_present if
              * present */
} MBSFNAreaConfiguration_v930_IEs;

typedef struct MBSFNAreaConfiguration_r9 {
    unsigned char   bit_mask;
#       define      MBSFNAreaConfiguration_r9_nonCriticalExtension_present 0x80
    struct CommonSF_AllocPatternList_r9 *commonSF_Alloc_r9;
    enum {
        commonSF_AllocPeriod_r9_rf4 = 0,
        commonSF_AllocPeriod_r9_rf8 = 1,
        commonSF_AllocPeriod_r9_rf16 = 2,
        commonSF_AllocPeriod_r9_rf32 = 3,
        commonSF_AllocPeriod_r9_rf64 = 4,
        commonSF_AllocPeriod_r9_rf128 = 5,
        commonSF_AllocPeriod_r9_rf256 = 6
    } commonSF_AllocPeriod_r9;
    struct PMCH_InfoList_r9 *pmch_InfoList_r9;
    MBSFNAreaConfiguration_v930_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                    * MBSFNAreaConfiguration_r9_nonCriticalExtension_present if
                    * present */
} MBSFNAreaConfiguration_r9;

typedef struct MBMSCountingRequest_r10 {
    unsigned char   bit_mask;
#       define      MBMSCountingRequest_r10_lateNonCriticalExtension_present 0x80
#       define      MBMSCountingRequest_r10_nonCriticalExtension_present 0x40
    struct CountingRequestList_r10 *countingRequestList_r10;
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
                  * MBMSCountingRequest_r10_lateNonCriticalExtension_present if
                  * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                      * MBMSCountingRequest_r10_nonCriticalExtension_present if
                      * present */
} MBMSCountingRequest_r10;

typedef struct MCCH_MessageType {
    unsigned short  choice;
#       define      MCCH_MessageType_c1_chosen 1
#       define      later_chosen 2
    union {
        struct _choice3 {
            unsigned short  choice;
#               define      mbsfnAreaConfiguration_r9_chosen 1
            union {
                MBSFNAreaConfiguration_r9 mbsfnAreaConfiguration_r9;  /* to
                                   * choose, set choice to
                                   * mbsfnAreaConfiguration_r9_chosen */
            } u;
        } c1;  /* to choose, set choice to MCCH_MessageType_c1_chosen */
        struct _choice4 {
            unsigned short  choice;
#               define      later_c2_chosen 1
#               define      later_messageClassExtension_chosen 2
            union {
                struct _choice2 {
                    unsigned short  choice;
#                       define      mbmsCountingRequest_r10_chosen 1
                    union {
                        MBMSCountingRequest_r10 mbmsCountingRequest_r10;  
                                        /* to choose, set choice to
                                         * mbmsCountingRequest_r10_chosen */
                    } u;
                } c2;  /* to choose, set choice to later_c2_chosen */
                _seq2           messageClassExtension;  /* to choose, set choice
                                     * to later_messageClassExtension_chosen */
            } u;
        } later;  /* to choose, set choice to later_chosen */
    } u;
} MCCH_MessageType;

typedef struct MCCH_Message {
    MCCH_MessageType message;
} MCCH_Message;

typedef struct Paging_v1130_IEs {
    unsigned char   bit_mask;
#       define      eab_ParamModification_r11_present 0x80
#       define      Paging_v1130_IEs_nonCriticalExtension_present 0x40
    _enum3          eab_ParamModification_r11;  /* optional; set in bit_mask
                                         * eab_ParamModification_r11_present if
                                         * present */
       /* Need ON */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                             * Paging_v1130_IEs_nonCriticalExtension_present if
                             * present */
} Paging_v1130_IEs;

typedef struct Paging_v920_IEs {
    unsigned char   bit_mask;
#       define      cmas_Indication_r9_present 0x80
#       define      Paging_v920_IEs_nonCriticalExtension_present 0x40
    _enum3          cmas_Indication_r9;  /* optional; set in bit_mask
                                          * cmas_Indication_r9_present if
                                          * present */
       /* Need ON */
    Paging_v1130_IEs nonCriticalExtension;  /* optional; set in bit_mask
                              * Paging_v920_IEs_nonCriticalExtension_present if
                              * present */
} Paging_v920_IEs;

typedef struct Paging_v890_IEs {
    unsigned char   bit_mask;
#       define      Paging_v890_IEs_lateNonCriticalExtension_present 0x80
#       define      Paging_v890_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
                          * Paging_v890_IEs_lateNonCriticalExtension_present if
                          * present */
    Paging_v920_IEs nonCriticalExtension;  /* optional; set in bit_mask
                              * Paging_v890_IEs_nonCriticalExtension_present if
                              * present */
} Paging_v890_IEs;

typedef struct Paging {
    unsigned char   bit_mask;
#       define      pagingRecordList_present 0x80
#       define      systemInfoModification_present 0x40
#       define      etws_Indication_present 0x20
#       define      Paging_nonCriticalExtension_present 0x10
    struct PagingRecordList *pagingRecordList;  /* optional; set in bit_mask
                                                 * pagingRecordList_present if
                                                 * present */
                                                /* Need ON */
    _enum3          systemInfoModification;  /* optional; set in bit_mask
                                              * systemInfoModification_present
                                              * if present */
       /* Need ON */
    _enum3          etws_Indication;  /* optional; set in bit_mask
                                       * etws_Indication_present if present */
       /* Need ON */
    Paging_v890_IEs nonCriticalExtension;  /* optional; set in bit_mask
                                       * Paging_nonCriticalExtension_present if
                                       * present */
} Paging;

typedef struct PCCH_MessageType {
    unsigned short  choice;
#       define      PCCH_MessageType_c1_chosen 1
#       define      PCCH_MessageType_messageClassExtension_chosen 2
    union {
        struct _choice5 {
            unsigned short  choice;
#               define      paging_chosen 1
            union {
                Paging          paging;  /* to choose, set choice to
                                          * paging_chosen */
            } u;
        } c1;  /* to choose, set choice to PCCH_MessageType_c1_chosen */
        _seq2           messageClassExtension;  /* to choose, set choice to
                             * PCCH_MessageType_messageClassExtension_chosen */
    } u;
} PCCH_MessageType;

typedef struct PCCH_Message {
    PCCH_MessageType message;
} PCCH_Message;

typedef unsigned short  RRC_TransactionIdentifier;

typedef enum PeriodicBSR_Timer_r12 {
    PeriodicBSR_Timer_r12_sf5 = 0,
    PeriodicBSR_Timer_r12_sf10 = 1,
    PeriodicBSR_Timer_r12_sf16 = 2,
    PeriodicBSR_Timer_r12_sf20 = 3,
    PeriodicBSR_Timer_r12_sf32 = 4,
    PeriodicBSR_Timer_r12_sf40 = 5,
    PeriodicBSR_Timer_r12_sf64 = 6,
    PeriodicBSR_Timer_r12_sf80 = 7,
    PeriodicBSR_Timer_r12_sf128 = 8,
    PeriodicBSR_Timer_r12_sf160 = 9,
    PeriodicBSR_Timer_r12_sf320 = 10,
    PeriodicBSR_Timer_r12_sf640 = 11,
    PeriodicBSR_Timer_r12_sf1280 = 12,
    PeriodicBSR_Timer_r12_sf2560 = 13,
    PeriodicBSR_Timer_r12_infinity = 14,
    PeriodicBSR_Timer_r12_spare1 = 15
} PeriodicBSR_Timer_r12;

typedef enum RetxBSR_Timer_r12 {
    RetxBSR_Timer_r12_sf320 = 0,
    RetxBSR_Timer_r12_sf640 = 1,
    RetxBSR_Timer_r12_sf1280 = 2,
    RetxBSR_Timer_r12_sf2560 = 3,
    RetxBSR_Timer_r12_sf5120 = 4,
    RetxBSR_Timer_r12_sf10240 = 5,
    RetxBSR_Timer_r12_spare2 = 6,
    RetxBSR_Timer_r12_spare1 = 7
} RetxBSR_Timer_r12;

typedef struct DRX_Config {
    unsigned short  choice;
#       define      DRX_Config_release_chosen 1
#       define      DRX_Config_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * DRX_Config_release_chosen */
        struct _seq3 {
            unsigned char   bit_mask;
#               define      shortDRX_present 0x80
            enum {
                onDurationTimer_psf1 = 0,
                onDurationTimer_psf2 = 1,
                onDurationTimer_psf3 = 2,
                onDurationTimer_psf4 = 3,
                onDurationTimer_psf5 = 4,
                onDurationTimer_psf6 = 5,
                onDurationTimer_psf8 = 6,
                onDurationTimer_psf10 = 7,
                onDurationTimer_psf20 = 8,
                onDurationTimer_psf30 = 9,
                onDurationTimer_psf40 = 10,
                onDurationTimer_psf50 = 11,
                onDurationTimer_psf60 = 12,
                onDurationTimer_psf80 = 13,
                onDurationTimer_psf100 = 14,
                onDurationTimer_psf200 = 15
            } onDurationTimer;
            enum {
                drx_InactivityTimer_psf1 = 0,
                drx_InactivityTimer_psf2 = 1,
                drx_InactivityTimer_psf3 = 2,
                drx_InactivityTimer_psf4 = 3,
                drx_InactivityTimer_psf5 = 4,
                drx_InactivityTimer_psf6 = 5,
                drx_InactivityTimer_psf8 = 6,
                drx_InactivityTimer_psf10 = 7,
                drx_InactivityTimer_psf20 = 8,
                drx_InactivityTimer_psf30 = 9,
                drx_InactivityTimer_psf40 = 10,
                drx_InactivityTimer_psf50 = 11,
                drx_InactivityTimer_psf60 = 12,
                drx_InactivityTimer_psf80 = 13,
                drx_InactivityTimer_psf100 = 14,
                drx_InactivityTimer_psf200 = 15,
                psf300 = 16,
                psf500 = 17,
                psf750 = 18,
                psf1280 = 19,
                psf1920 = 20,
                psf2560 = 21,
                psf0_v1020 = 22,
                drx_InactivityTimer_spare9 = 23,
                drx_InactivityTimer_spare8 = 24,
                drx_InactivityTimer_spare7 = 25,
                drx_InactivityTimer_spare6 = 26,
                drx_InactivityTimer_spare5 = 27,
                drx_InactivityTimer_spare4 = 28,
                drx_InactivityTimer_spare3 = 29,
                drx_InactivityTimer_spare2 = 30,
                drx_InactivityTimer_spare1 = 31
            } drx_InactivityTimer;
            enum {
                drx_RetransmissionTimer_psf1 = 0,
                drx_RetransmissionTimer_psf2 = 1,
                drx_RetransmissionTimer_psf4 = 2,
                drx_RetransmissionTimer_psf6 = 3,
                drx_RetransmissionTimer_psf8 = 4,
                psf16 = 5,
                psf24 = 6,
                psf33 = 7
            } drx_RetransmissionTimer;
            struct {
                unsigned short  choice;
#                   define      sf10_chosen 1
#                   define      sf20_chosen 2
#                   define      sf32_chosen 3
#                   define      sf40_chosen 4
#                   define      sf64_chosen 5
#                   define      sf80_chosen 6
#                   define      sf128_chosen 7
#                   define      sf160_chosen 8
#                   define      sf256_chosen 9
#                   define      sf320_chosen 10
#                   define      sf512_chosen 11
#                   define      sf640_chosen 12
#                   define      sf1024_chosen 13
#                   define      sf1280_chosen 14
#                   define      sf2048_chosen 15
#                   define      sf2560_chosen 16
                union {
                    unsigned short  sf10;  /* to choose, set choice to
                                            * sf10_chosen */
                    unsigned short  sf20;  /* to choose, set choice to
                                            * sf20_chosen */
                    unsigned short  sf32;  /* to choose, set choice to
                                            * sf32_chosen */
                    unsigned short  sf40;  /* to choose, set choice to
                                            * sf40_chosen */
                    unsigned short  sf64;  /* to choose, set choice to
                                            * sf64_chosen */
                    unsigned short  sf80;  /* to choose, set choice to
                                            * sf80_chosen */
                    unsigned short  sf128;  /* to choose, set choice to
                                             * sf128_chosen */
                    unsigned short  sf160;  /* to choose, set choice to
                                             * sf160_chosen */
                    unsigned short  sf256;  /* to choose, set choice to
                                             * sf256_chosen */
                    unsigned short  sf320;  /* to choose, set choice to
                                             * sf320_chosen */
                    unsigned short  sf512;  /* to choose, set choice to
                                             * sf512_chosen */
                    unsigned short  sf640;  /* to choose, set choice to
                                             * sf640_chosen */
                    unsigned short  sf1024;  /* to choose, set choice to
                                              * sf1024_chosen */
                    unsigned short  sf1280;  /* to choose, set choice to
                                              * sf1280_chosen */
                    unsigned short  sf2048;  /* to choose, set choice to
                                              * sf2048_chosen */
                    unsigned short  sf2560;  /* to choose, set choice to
                                              * sf2560_chosen */
                } u;
            } longDRX_CycleStartOffset;
            struct {
                enum {
                    shortDRX_Cycle_sf2 = 0,
                    shortDRX_Cycle_sf5 = 1,
                    shortDRX_Cycle_sf8 = 2,
                    shortDRX_Cycle_sf10 = 3,
                    shortDRX_Cycle_sf16 = 4,
                    shortDRX_Cycle_sf20 = 5,
                    shortDRX_Cycle_sf32 = 6,
                    shortDRX_Cycle_sf40 = 7,
                    shortDRX_Cycle_sf64 = 8,
                    shortDRX_Cycle_sf80 = 9,
                    shortDRX_Cycle_sf128 = 10,
                    shortDRX_Cycle_sf160 = 11,
                    shortDRX_Cycle_sf256 = 12,
                    shortDRX_Cycle_sf320 = 13,
                    shortDRX_Cycle_sf512 = 14,
                    shortDRX_Cycle_sf640 = 15
                } shortDRX_Cycle;
                unsigned short  drxShortCycleTimer;
            } shortDRX;  /* optional; set in bit_mask shortDRX_present if
                          * present */
                         /* Need OR */
        } setup;  /* to choose, set choice to DRX_Config_setup_chosen */
    } u;
} DRX_Config;

typedef struct DRX_Config_v1130 {
    unsigned char   bit_mask;
#       define      drx_RetransmissionTimer_v1130_present 0x80
#       define      longDRX_CycleStartOffset_v1130_present 0x40
#       define      shortDRX_Cycle_v1130_present 0x20
    enum {
        psf0_v1130 = 0
    } drx_RetransmissionTimer_v1130;  /* optional; set in bit_mask
                                       * drx_RetransmissionTimer_v1130_present
                                       * if present */
       /*Need OR */
    struct {
        unsigned short  choice;
#           define      sf60_v1130_chosen 1
#           define      sf70_v1130_chosen 2
        union {
            unsigned short  sf60_v1130;  /* to choose, set choice to
                                          * sf60_v1130_chosen */
            unsigned short  sf70_v1130;  /* to choose, set choice to
                                          * sf70_v1130_chosen */
        } u;
    } longDRX_CycleStartOffset_v1130;  /* optional; set in bit_mask
                                        * longDRX_CycleStartOffset_v1130_present
                                        * if present */
       /*Need OR */
    enum {
        sf4_v1130 = 0
    } shortDRX_Cycle_v1130;  /* optional; set in bit_mask
                              * shortDRX_Cycle_v1130_present if present */
                             /*Need OR */
} DRX_Config_v1130;

typedef enum _enum17 {
    setup = 0
} _enum17;

typedef struct MAC_MainConfig {
    unsigned short  bit_mask;
#       define      ul_SCH_Config_present 0x8000
#       define      drx_Config_present 0x4000
#       define      phr_Config_present 0x2000
#       define      sr_ProhibitTimer_r9_present 0x1000
#       define      mac_MainConfig_v1020_present 0x0800
#       define      stag_ToReleaseList_r11_present 0x0400
#       define      stag_ToAddModList_r11_present 0x0200
#       define      drx_Config_v1130_present 0x0100
#       define      e_HARQ_Pattern_r12_present 0x0080
#       define      dualConnectivityPHR_present 0x0040
#       define      logicalChannelSR_Config_r12_present 0x0020
    struct {
        unsigned char   bit_mask;
#           define      maxHARQ_Tx_present 0x80
#           define      periodicBSR_Timer_present 0x40
        enum {
            maxHARQ_Tx_n1 = 0,
            maxHARQ_Tx_n2 = 1,
            maxHARQ_Tx_n3 = 2,
            maxHARQ_Tx_n4 = 3,
            maxHARQ_Tx_n5 = 4,
            maxHARQ_Tx_n6 = 5,
            maxHARQ_Tx_n7 = 6,
            maxHARQ_Tx_n8 = 7,
            maxHARQ_Tx_n10 = 8,
            maxHARQ_Tx_n12 = 9,
            maxHARQ_Tx_n16 = 10,
            maxHARQ_Tx_n20 = 11,
            maxHARQ_Tx_n24 = 12,
            maxHARQ_Tx_n28 = 13,
            maxHARQ_Tx_spare2 = 14,
            maxHARQ_Tx_spare1 = 15
        } maxHARQ_Tx;  /* optional; set in bit_mask maxHARQ_Tx_present if
                        * present */
       /* Need ON */
        PeriodicBSR_Timer_r12 periodicBSR_Timer;  /* optional; set in bit_mask
                                                   * periodicBSR_Timer_present
                                                   * if present */
                                                  /* Need ON */
        RetxBSR_Timer_r12 retxBSR_Timer;
        ossBoolean      ttiBundling;
    } ul_SCH_Config;  /* optional; set in bit_mask ul_SCH_Config_present if
                       * present */
       /* Need ON */
    DRX_Config      drx_Config;  /* optional; set in bit_mask drx_Config_present
                                  * if present */
                                 /* Need ON */
    TimeAlignmentTimer timeAlignmentTimerDedicated;
    struct {
        unsigned short  choice;
#           define      phr_Config_release_chosen 1
#           define      phr_Config_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * phr_Config_release_chosen */
            struct _seq4 {
                enum {
                    periodicPHR_Timer_sf10 = 0,
                    periodicPHR_Timer_sf20 = 1,
                    periodicPHR_Timer_sf50 = 2,
                    periodicPHR_Timer_sf100 = 3,
                    periodicPHR_Timer_sf200 = 4,
                    periodicPHR_Timer_sf500 = 5,
                    periodicPHR_Timer_sf1000 = 6,
                    periodicPHR_Timer_infinity = 7
                } periodicPHR_Timer;
                enum {
                    sf0 = 0,
                    prohibitPHR_Timer_sf10 = 1,
                    prohibitPHR_Timer_sf20 = 2,
                    prohibitPHR_Timer_sf50 = 3,
                    prohibitPHR_Timer_sf100 = 4,
                    prohibitPHR_Timer_sf200 = 5,
                    prohibitPHR_Timer_sf500 = 6,
                    prohibitPHR_Timer_sf1000 = 7
                } prohibitPHR_Timer;
                enum {
                    dl_PathlossChange_dB1 = 0,
                    dl_PathlossChange_dB3 = 1,
                    dl_PathlossChange_dB6 = 2,
                    dl_PathlossChange_infinity = 3
                } dl_PathlossChange;
            } setup;  /* to choose, set choice to phr_Config_setup_chosen */
        } u;
    } phr_Config;  /* optional; set in bit_mask phr_Config_present if present */
       /* Need ON */
    unsigned short  sr_ProhibitTimer_r9;  /* extension #1; optional; set in
                                   * bit_mask sr_ProhibitTimer_r9_present if
                                   * present */
                                          /* Need ON */
    struct {
        unsigned char   bit_mask;
#           define      sCellDeactivationTimer_r10_present 0x80
#           define      extendedBSR_Sizes_r10_present 0x40
#           define      extendedPHR_r10_present 0x20
        enum {
            rf2 = 0,
            sCellDeactivationTimer_r10_rf4 = 1,
            sCellDeactivationTimer_r10_rf8 = 2,
            sCellDeactivationTimer_r10_rf16 = 3,
            sCellDeactivationTimer_r10_rf32 = 4,
            sCellDeactivationTimer_r10_rf64 = 5,
            sCellDeactivationTimer_r10_rf128 = 6,
            sCellDeactivationTimer_r10_spare = 7
        } sCellDeactivationTimer_r10;  /* optional; set in bit_mask
                                        * sCellDeactivationTimer_r10_present if
                                        * present */
       /* Need OP */
        _enum17         extendedBSR_Sizes_r10;  /* optional; set in bit_mask
                                                 * extendedBSR_Sizes_r10_present
                                                 * if present */
       /* Need OR */
        _enum17         extendedPHR_r10;  /* optional; set in bit_mask
                                           * extendedPHR_r10_present if
                                           * present */
                                          /* Need OR */
    } mac_MainConfig_v1020;  /* extension #2; optional; set in bit_mask
                              * mac_MainConfig_v1020_present if present */
    /* Need ON */
    struct STAG_ToReleaseList_r11 *stag_ToReleaseList_r11;  /* extension #3;
                                   * optional; set in bit_mask
                                   * stag_ToReleaseList_r11_present if
                                   * present */
                                                            /* Need ON */
    struct STAG_ToAddModList_r11 *stag_ToAddModList_r11;  /* extension #3;
                                   * optional; set in bit_mask
                                   * stag_ToAddModList_r11_present if present */
                                                          /* Need ON */
    DRX_Config_v1130 drx_Config_v1130;  /* extension #3; optional; set in
                                   * bit_mask drx_Config_v1130_present if
                                   * present */
                                        /* Need ON */
    ossBoolean      e_HARQ_Pattern_r12;  /* extension #4; optional; set in
                                   * bit_mask e_HARQ_Pattern_r12_present if
                                   * present */
                                         /* Need ON */
    struct {
        unsigned short  choice;
#           define      dualConnectivityPHR_release_chosen 1
#           define      dualConnectivityPHR_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * dualConnectivityPHR_release_chosen */
            struct _seq5 {
                enum {
                    real = 0,
                    virtual = 1
                } phr_ModeOtherCG_r12;
            } setup;  /* to choose, set choice to
                       * dualConnectivityPHR_setup_chosen */
        } u;
    } dualConnectivityPHR;  /* extension #4; optional; set in bit_mask
                             * dualConnectivityPHR_present if present */
       /* Need ON */
    struct {
        unsigned short  choice;
#           define      logicalChannelSR_Config_r12_release_chosen 1
#           define      logicalChannelSR_Config_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                * logicalChannelSR_Config_r12_release_chosen */
            struct _seq6 {
                enum {
                    logicalChannelSR_ProhibitTimer_r12_sf20 = 0,
                    logicalChannelSR_ProhibitTimer_r12_sf40 = 1,
                    logicalChannelSR_ProhibitTimer_r12_sf64 = 2,
                    logicalChannelSR_ProhibitTimer_r12_sf128 = 3,
                    logicalChannelSR_ProhibitTimer_r12_sf512 = 4,
                    logicalChannelSR_ProhibitTimer_r12_sf1024 = 5,
                    logicalChannelSR_ProhibitTimer_r12_sf2560 = 6,
                    logicalChannelSR_ProhibitTimer_r12_spare1 = 7
                } logicalChannelSR_ProhibitTimer_r12;
            } setup;  /* to choose, set choice to
                       * logicalChannelSR_Config_r12_setup_chosen */
        } u;
    } logicalChannelSR_Config_r12;  /* extension #4; optional; set in bit_mask
                                     * logicalChannelSR_Config_r12_present if
                                     * present */
                                    /* Need ON */
} MAC_MainConfig;

typedef struct C_RNTI {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} C_RNTI;

typedef enum _enum18 {
    semiPersistSchedIntervalDL_sf10 = 0,
    semiPersistSchedIntervalDL_sf20 = 1,
    semiPersistSchedIntervalDL_sf32 = 2,
    semiPersistSchedIntervalDL_sf40 = 3,
    semiPersistSchedIntervalDL_sf64 = 4,
    semiPersistSchedIntervalDL_sf80 = 5,
    semiPersistSchedIntervalDL_sf128 = 6,
    semiPersistSchedIntervalDL_sf160 = 7,
    semiPersistSchedIntervalDL_sf320 = 8,
    semiPersistSchedIntervalDL_sf640 = 9,
    semiPersistSchedIntervalDL_spare6 = 10,
    semiPersistSchedIntervalDL_spare5 = 11,
    semiPersistSchedIntervalDL_spare4 = 12,
    semiPersistSchedIntervalDL_spare3 = 13,
    semiPersistSchedIntervalDL_spare2 = 14,
    semiPersistSchedIntervalDL_spare1 = 15
} _enum18;

typedef struct SPS_ConfigDL {
    unsigned short  choice;
#       define      SPS_ConfigDL_release_chosen 1
#       define      SPS_ConfigDL_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * SPS_ConfigDL_release_chosen */
        struct _seq8 {
            unsigned char   bit_mask;
#               define      twoAntennaPortActivated_r10_present 0x80
            _enum18         semiPersistSchedIntervalDL;
            unsigned short  numberOfConfSPS_Processes;
            struct N1PUCCH_AN_PersistentList *n1PUCCH_AN_PersistentList;
            struct {
                unsigned short  choice;
#                   define      twoAntennaPortActivated_r10_release_chosen 1
#                   define      twoAntennaPortActivated_r10_setup_chosen 2
                union {
                    Nulltype        release;  /* to choose, set choice to
                                * twoAntennaPortActivated_r10_release_chosen */
                    struct _seq7 {
                        struct N1PUCCH_AN_PersistentList *n1PUCCH_AN_PersistentListP1_r10;
                    } setup;  /* to choose, set choice to
                               * twoAntennaPortActivated_r10_setup_chosen */
                } u;
            } twoAntennaPortActivated_r10;  /* extension #1; optional; set in
                                   * bit_mask
                                   * twoAntennaPortActivated_r10_present if
                                   * present */
                                            /* Need ON */
        } setup;  /* to choose, set choice to SPS_ConfigDL_setup_chosen */
    } u;
} SPS_ConfigDL;

typedef struct SPS_ConfigUL {
    unsigned short  choice;
#       define      SPS_ConfigUL_release_chosen 1
#       define      SPS_ConfigUL_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * SPS_ConfigUL_release_chosen */
        struct _seq10 {
            unsigned char   bit_mask;
#               define      p0_Persistent_present 0x80
#               define      twoIntervalsConfig_present 0x40
#               define      p0_PersistentSubframeSet2_r12_present 0x20
            _enum18         semiPersistSchedIntervalUL;
            enum {
                e2 = 0,
                e3 = 1,
                e4 = 2,
                e8 = 3
            } implicitReleaseAfter;
            struct {
                short           p0_NominalPUSCH_Persistent;
                short           p0_UE_PUSCH_Persistent;
            } p0_Persistent;  /* optional; set in bit_mask p0_Persistent_present
                               * if present */
                                                                                               /* Need OP */
            _enum3          twoIntervalsConfig;  /* optional; set in bit_mask
                                                  * twoIntervalsConfig_present
                                                  * if present */
       /* Cond TDD */
            struct {
                unsigned short  choice;
#                   define      p0_PersistentSubframeSet2_r12_release_chosen 1
#                   define      p0_PersistentSubframeSet2_r12_setup_chosen 2
                union {
                    Nulltype        release;  /* to choose, set choice to
                              * p0_PersistentSubframeSet2_r12_release_chosen */
                    struct _seq9 {
                        short           p0_NominalPUSCH_PersistentSubframeSet2_r12;
                        short           p0_UE_PUSCH_PersistentSubframeSet2_r12;
                    } setup;  /* to choose, set choice to
                               * p0_PersistentSubframeSet2_r12_setup_chosen */
                } u;
            } p0_PersistentSubframeSet2_r12;  /* extension #1; optional; set in
                                   * bit_mask
                                   * p0_PersistentSubframeSet2_r12_present if
                                   * present */
                                              /* Need ON */
        } setup;  /* to choose, set choice to SPS_ConfigUL_setup_chosen */
    } u;
} SPS_ConfigUL;

typedef struct SPS_Config {
    unsigned char   bit_mask;
#       define      semiPersistSchedC_RNTI_present 0x80
#       define      sps_ConfigDL_present 0x40
#       define      sps_ConfigUL_present 0x20
    C_RNTI          semiPersistSchedC_RNTI;  /* optional; set in bit_mask
                                              * semiPersistSchedC_RNTI_present
                                              * if present */
                                             /* Need OR */
    SPS_ConfigDL    sps_ConfigDL;  /* optional; set in bit_mask
                                    * sps_ConfigDL_present if present */
                                   /* Need ON */
    SPS_ConfigUL    sps_ConfigUL;  /* optional; set in bit_mask
                                    * sps_ConfigUL_present if present */
                                   /* Need ON */
} SPS_Config;

typedef struct PDSCH_ConfigDedicated {
    enum {
        p_a_dB_6 = 0,
        p_a_dB_4dot77 = 1,
        p_a_dB_3 = 2,
        p_a_dB_1dot77 = 3,
        p_a_dB0 = 4,
        p_a_dB1 = 5,
        p_a_dB2 = 6,
        p_a_dB3 = 7
    } p_a;
} PDSCH_ConfigDedicated;

typedef struct PUCCH_ConfigDedicated {
    unsigned char   bit_mask;
#       define      tdd_AckNackFeedbackMode_present 0x80
    struct {
        unsigned short  choice;
#           define      ackNackRepetition_release_chosen 1
#           define      ackNackRepetition_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * ackNackRepetition_release_chosen */
            struct _seq11 {
                enum {
                    repetitionFactor_n2 = 0,
                    repetitionFactor_n4 = 1,
                    repetitionFactor_n6 = 2,
                    repetitionFactor_spare1 = 3
                } repetitionFactor;
                unsigned short  n1PUCCH_AN_Rep;
            } setup;  /* to choose, set choice to
                       * ackNackRepetition_setup_chosen */
        } u;
    } ackNackRepetition;
    enum {
        bundling = 0,
        multiplexing = 1
    } tdd_AckNackFeedbackMode;  /* optional; set in bit_mask
                                 * tdd_AckNackFeedbackMode_present if present */
                                /* Cond TDD */
} PUCCH_ConfigDedicated;

typedef struct PUSCH_ConfigDedicated {
    unsigned short  betaOffset_ACK_Index;
    unsigned short  betaOffset_RI_Index;
    unsigned short  betaOffset_CQI_Index;
} PUSCH_ConfigDedicated;

typedef enum FilterCoefficient {
    fc0 = 0,
    fc1 = 1,
    fc2 = 2,
    fc3 = 3,
    fc4 = 4,
    fc5 = 5,
    fc6 = 6,
    fc7 = 7,
    fc8 = 8,
    fc9 = 9,
    fc11 = 10,
    fc13 = 11,
    fc15 = 12,
    fc17 = 13,
    fc19 = 14,
    FilterCoefficient_spare1 = 15
} FilterCoefficient;

typedef enum _enum19 {
    en0 = 0,
    en1 = 1
} _enum19;

typedef struct UplinkPowerControlDedicated {
    unsigned char   bit_mask;
#       define      UplinkPowerControlDedicated_filterCoefficient_present 0x80
    short           p0_UE_PUSCH;
    _enum19         deltaMCS_Enabled;
    ossBoolean      accumulationEnabled;
    short           p0_UE_PUCCH;
    unsigned short  pSRS_Offset;
    FilterCoefficient filterCoefficient;  
                    /* UplinkPowerControlDedicated_filterCoefficient_present not
                     * set in bit_mask implies value is fc4 */
} UplinkPowerControlDedicated;

typedef struct TPC_Index {
    unsigned short  choice;
#       define      indexOfFormat3_chosen 1
#       define      indexOfFormat3A_chosen 2
    union {
        unsigned short  indexOfFormat3;  /* to choose, set choice to
                                          * indexOfFormat3_chosen */
        unsigned short  indexOfFormat3A;  /* to choose, set choice to
                                           * indexOfFormat3A_chosen */
    } u;
} TPC_Index;

typedef struct TPC_PDCCH_Config {
    unsigned short  choice;
#       define      TPC_PDCCH_Config_release_chosen 1
#       define      TPC_PDCCH_Config_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * TPC_PDCCH_Config_release_chosen */
        struct _seq12 {
            _bit1           tpc_RNTI;
            TPC_Index       tpc_Index;
        } setup;  /* to choose, set choice to TPC_PDCCH_Config_setup_chosen */
    } u;
} TPC_PDCCH_Config;

typedef enum CQI_ReportModeAperiodic {
    rm12 = 0,
    rm20 = 1,
    rm22 = 2,
    rm30 = 3,
    rm31 = 4,
    rm32_v1250 = 5,
    CQI_ReportModeAperiodic_spare2 = 6,
    CQI_ReportModeAperiodic_spare1 = 7
} CQI_ReportModeAperiodic;

typedef struct CQI_ReportPeriodic {
    unsigned short  choice;
#       define      CQI_ReportPeriodic_release_chosen 1
#       define      CQI_ReportPeriodic_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * CQI_ReportPeriodic_release_chosen */
        struct _seq14 {
            unsigned char   bit_mask;
#               define      CQI_ReportPeriodic_setup_ri_ConfigIndex_present 0x80
            unsigned short  cqi_PUCCH_ResourceIndex;
            unsigned short  cqi_pmi_ConfigIndex;
            struct {
                unsigned short  choice;
#                   define      widebandCQI_chosen 1
#                   define      subbandCQI_chosen 2
                union {
                    Nulltype        widebandCQI;  /* to choose, set choice to
                                                   * widebandCQI_chosen */
                    struct _seq13 {
                        unsigned short  k;
                    } subbandCQI;  /* to choose, set choice to
                                    * subbandCQI_chosen */
                } u;
            } cqi_FormatIndicatorPeriodic;
            unsigned short  ri_ConfigIndex;  /* optional; set in bit_mask
                           * CQI_ReportPeriodic_setup_ri_ConfigIndex_present if
                           * present */
                                             /* Need OR */
            ossBoolean      simultaneousAckNackAndCQI;
        } setup;  /* to choose, set choice to CQI_ReportPeriodic_setup_chosen */
    } u;
} CQI_ReportPeriodic;

typedef struct CQI_ReportConfig {
    unsigned char   bit_mask;
#       define      cqi_ReportModeAperiodic_present 0x80
#       define      cqi_ReportPeriodic_present 0x40
    CQI_ReportModeAperiodic cqi_ReportModeAperiodic;  /* optional; set in
                                   * bit_mask cqi_ReportModeAperiodic_present if
                                   * present */
                                                      /* Need OR */
    short           nomPDSCH_RS_EPRE_Offset;
    CQI_ReportPeriodic cqi_ReportPeriodic;  /* optional; set in bit_mask
                                             * cqi_ReportPeriodic_present if
                                             * present */
                                            /* Need ON */
} CQI_ReportConfig;

typedef enum _enum20 {
    srs_Bandwidth_bw0 = 0,
    srs_Bandwidth_bw1 = 1,
    srs_Bandwidth_bw2 = 2,
    srs_Bandwidth_bw3 = 3
} _enum20;

typedef enum _enum21 {
    cs0 = 0,
    cs1 = 1,
    cyclicShift_cs2 = 2,
    cs3 = 3,
    cyclicShift_cs4 = 4,
    cs5 = 5,
    cs6 = 6,
    cs7 = 7
} _enum21;

typedef struct SoundingRS_UL_ConfigDedicated {
    unsigned short  choice;
#       define      SoundingRS_UL_ConfigDedicated_release_chosen 1
#       define      SoundingRS_UL_ConfigDedicated_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                              * SoundingRS_UL_ConfigDedicated_release_chosen */
        struct _seq15 {
            _enum20         srs_Bandwidth;
            enum {
                hbw0 = 0,
                hbw1 = 1,
                hbw2 = 2,
                hbw3 = 3
            } srs_HoppingBandwidth;
            unsigned short  freqDomainPosition;
            ossBoolean      duration;
            unsigned short  srs_ConfigIndex;
            unsigned short  transmissionComb;
            _enum21         cyclicShift;
        } setup;  /* to choose, set choice to
                   * SoundingRS_UL_ConfigDedicated_setup_chosen */
    } u;
} SoundingRS_UL_ConfigDedicated;

typedef enum _enum22 {
    closedLoop = 0,
    openLoop = 1
} _enum22;

typedef struct _choice6 {
    unsigned short  choice;
#       define      ue_TransmitAntennaSelection_release_chosen 1
#       define      ue_TransmitAntennaSelection_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                * ue_TransmitAntennaSelection_release_chosen */
        _enum22         setup;  /* to choose, set choice to
                                 * ue_TransmitAntennaSelection_setup_chosen */
    } u;
} _choice6;

typedef struct AntennaInfoDedicated {
    unsigned char   bit_mask;
#       define      codebookSubsetRestriction_present 0x80
    enum {
        transmissionMode_tm1 = 0,
        transmissionMode_tm2 = 1,
        transmissionMode_tm3 = 2,
        transmissionMode_tm4 = 3,
        transmissionMode_tm5 = 4,
        transmissionMode_tm6 = 5,
        transmissionMode_tm7 = 6,
        transmissionMode_tm8_v920 = 7
    } transmissionMode;
    struct {
        unsigned short  choice;
#           define      n2TxAntenna_tm3_chosen 1
#           define      n4TxAntenna_tm3_chosen 2
#           define      n2TxAntenna_tm4_chosen 3
#           define      n4TxAntenna_tm4_chosen 4
#           define      n2TxAntenna_tm5_chosen 5
#           define      n4TxAntenna_tm5_chosen 6
#           define      n2TxAntenna_tm6_chosen 7
#           define      n4TxAntenna_tm6_chosen 8
        union {
            _bit1           n2TxAntenna_tm3;  /* to choose, set choice to
                                               * n2TxAntenna_tm3_chosen */
            _bit1           n4TxAntenna_tm3;  /* to choose, set choice to
                                               * n4TxAntenna_tm3_chosen */
            _bit1           n2TxAntenna_tm4;  /* to choose, set choice to
                                               * n2TxAntenna_tm4_chosen */
            _bit1           n4TxAntenna_tm4;  /* to choose, set choice to
                                               * n4TxAntenna_tm4_chosen */
            _bit1           n2TxAntenna_tm5;  /* to choose, set choice to
                                               * n2TxAntenna_tm5_chosen */
            _bit1           n4TxAntenna_tm5;  /* to choose, set choice to
                                               * n4TxAntenna_tm5_chosen */
            _bit1           n2TxAntenna_tm6;  /* to choose, set choice to
                                               * n2TxAntenna_tm6_chosen */
            _bit1           n4TxAntenna_tm6;  /* to choose, set choice to
                                               * n4TxAntenna_tm6_chosen */
        } u;
    } codebookSubsetRestriction;  /* optional; set in bit_mask
                                   * codebookSubsetRestriction_present if
                                   * present */
                                                                                                                       /* Cond TM */
    _choice6        ue_TransmitAntennaSelection;
} AntennaInfoDedicated;

typedef struct SchedulingRequestConfig {
    unsigned short  choice;
#       define      SchedulingRequestConfig_release_chosen 1
#       define      SchedulingRequestConfig_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * SchedulingRequestConfig_release_chosen */
        struct _seq16 {
            unsigned short  sr_PUCCH_ResourceIndex;
            unsigned short  sr_ConfigIndex;
            enum {
                dsr_TransMax_n4 = 0,
                dsr_TransMax_n8 = 1,
                dsr_TransMax_n16 = 2,
                dsr_TransMax_n32 = 3,
                dsr_TransMax_n64 = 4,
                dsr_TransMax_spare3 = 5,
                dsr_TransMax_spare2 = 6,
                dsr_TransMax_spare1 = 7
            } dsr_TransMax;
        } setup;  /* to choose, set choice to
                   * SchedulingRequestConfig_setup_chosen */
    } u;
} SchedulingRequestConfig;

typedef struct CQI_ReportConfig_v920 {
    unsigned char   bit_mask;
#       define      CQI_ReportConfig_v920_cqi_Mask_r9_present 0x80
#       define      CQI_ReportConfig_v920_pmi_RI_Report_r9_present 0x40
    _enum17         cqi_Mask_r9;  /* optional; set in bit_mask
                                   * CQI_ReportConfig_v920_cqi_Mask_r9_present
                                   * if present */
               /* Cond cqi-Setup */
    _enum17         pmi_RI_Report_r9;  /* optional; set in bit_mask
                            * CQI_ReportConfig_v920_pmi_RI_Report_r9_present if
                            * present */
                                       /* Cond PMIRI */
} CQI_ReportConfig_v920;

typedef struct AntennaInfoDedicated_v920 {
    unsigned char   bit_mask;
#       define      codebookSubsetRestriction_v920_present 0x80
    struct {
        unsigned short  choice;
#           define      n2TxAntenna_tm8_r9_chosen 1
#           define      n4TxAntenna_tm8_r9_chosen 2
        union {
            _bit1           n2TxAntenna_tm8_r9;  /* to choose, set choice to
                                                 * n2TxAntenna_tm8_r9_chosen */
            _bit1           n4TxAntenna_tm8_r9;  /* to choose, set choice to
                                                 * n4TxAntenna_tm8_r9_chosen */
        } u;
    } codebookSubsetRestriction_v920;  /* optional; set in bit_mask
                                        * codebookSubsetRestriction_v920_present
                                        * if present */
                                       /* Cond TM8 */
} AntennaInfoDedicated_v920;

typedef struct _bit2 {
    unsigned int    length;  /* number of significant bits */
    unsigned char   *value;
} _bit2;

typedef struct AntennaInfoDedicated_r10 {
    unsigned char   bit_mask;
#       define      codebookSubsetRestriction_r10_present 0x80
    enum {
        transmissionMode_r10_tm1 = 0,
        transmissionMode_r10_tm2 = 1,
        transmissionMode_r10_tm3 = 2,
        transmissionMode_r10_tm4 = 3,
        transmissionMode_r10_tm5 = 4,
        transmissionMode_r10_tm6 = 5,
        transmissionMode_r10_tm7 = 6,
        transmissionMode_r10_tm8_v920 = 7,
        tm9_v1020 = 8,
        tm10_v1130 = 9,
        transmissionMode_r10_spare6 = 10,
        transmissionMode_r10_spare5 = 11,
        transmissionMode_r10_spare4 = 12,
        transmissionMode_r10_spare3 = 13,
        transmissionMode_r10_spare2 = 14,
        transmissionMode_r10_spare1 = 15
    } transmissionMode_r10;
    _bit2           codebookSubsetRestriction_r10;  /* optional; set in bit_mask
                                     * codebookSubsetRestriction_r10_present if
                                     * present */
                                                    /* Cond TMX */
    _choice6        ue_TransmitAntennaSelection;
} AntennaInfoDedicated_r10;

typedef struct AntennaInfoUL_r10 {
    unsigned char   bit_mask;
#       define      transmissionModeUL_r10_present 0x80
#       define      fourAntennaPortActivated_r10_present 0x40
    enum {
        transmissionModeUL_r10_tm1 = 0,
        transmissionModeUL_r10_tm2 = 1,
        transmissionModeUL_r10_spare6 = 2,
        transmissionModeUL_r10_spare5 = 3,
        transmissionModeUL_r10_spare4 = 4,
        transmissionModeUL_r10_spare3 = 5,
        transmissionModeUL_r10_spare2 = 6,
        transmissionModeUL_r10_spare1 = 7
    } transmissionModeUL_r10;  /* optional; set in bit_mask
                                * transmissionModeUL_r10_present if present */
       /* Need OR */
    _enum17         fourAntennaPortActivated_r10;  /* optional; set in bit_mask
                                      * fourAntennaPortActivated_r10_present if
                                      * present */
                                                   /* Need OR */
} AntennaInfoUL_r10;

typedef struct CQI_ReportAperiodic_r10 {
    unsigned short  choice;
#       define      CQI_ReportAperiodic_r10_release_chosen 1
#       define      CQI_ReportAperiodic_r10_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * CQI_ReportAperiodic_r10_release_chosen */
        struct _seq17 {
            unsigned char   bit_mask;
#               define      aperiodicCSI_Trigger_r10_present 0x80
            CQI_ReportModeAperiodic cqi_ReportModeAperiodic_r10;
            struct {
                _bit1           trigger1_r10;
                _bit1           trigger2_r10;
            } aperiodicCSI_Trigger_r10;  /* optional; set in bit_mask
                                          * aperiodicCSI_Trigger_r10_present if
                                          * present */
                                         /* Need OR */
        } setup;  /* to choose, set choice to
                   * CQI_ReportAperiodic_r10_setup_chosen */
    } u;
} CQI_ReportAperiodic_r10;

typedef enum _enum23 {
    submode1 = 0,
    submode2 = 1
} _enum23;

typedef struct CQI_ReportPeriodic_r10 {
    unsigned short  choice;
#       define      CQI_ReportPeriodic_r10_release_chosen 1
#       define      CQI_ReportPeriodic_r10_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * CQI_ReportPeriodic_r10_release_chosen */
        struct _seq21 {
            unsigned char   bit_mask;
#               define      cqi_PUCCH_ResourceIndexP1_r10_present 0x80
#               define      CQI_ReportPeriodic_r10_setup_ri_ConfigIndex_present 0x40
#               define      setup_cqi_Mask_r9_present 0x20
#               define      csi_ConfigIndex_r10_present 0x10
            unsigned short  cqi_PUCCH_ResourceIndex_r10;
            unsigned short  cqi_PUCCH_ResourceIndexP1_r10;  /* optional; set in
                                   * bit_mask
                                   * cqi_PUCCH_ResourceIndexP1_r10_present if
                                   * present */
                                                            /* Need OR */
            unsigned short  cqi_pmi_ConfigIndex;
            struct {
                unsigned short  choice;
#                   define      widebandCQI_r10_chosen 1
#                   define      subbandCQI_r10_chosen 2
                union {
                    struct _seq18 {
                        unsigned char   bit_mask;
#                           define      csi_ReportMode_r10_present 0x80
                        _enum23         csi_ReportMode_r10;  /* optional; set in
                                   * bit_mask csi_ReportMode_r10_present if
                                   * present */
                                                             /* Need OR */
                    } widebandCQI_r10;  /* to choose, set choice to
                                         * widebandCQI_r10_chosen */
                    struct _seq19 {
                        unsigned short  k;
                        _enum16         periodicityFactor_r10;
                    } subbandCQI_r10;  /* to choose, set choice to
                                        * subbandCQI_r10_chosen */
                } u;
            } cqi_FormatIndicatorPeriodic_r10;
            unsigned short  ri_ConfigIndex;  /* optional; set in bit_mask
                       * CQI_ReportPeriodic_r10_setup_ri_ConfigIndex_present if
                       * present */
                                             /* Need OR */
            ossBoolean      simultaneousAckNackAndCQI;
            _enum17         cqi_Mask_r9;  /* optional; set in bit_mask
                                           * setup_cqi_Mask_r9_present if
                                           * present */
                               /* Need OR */
            struct {
                unsigned short  choice;
#                   define      csi_ConfigIndex_r10_release_chosen 1
#                   define      csi_ConfigIndex_r10_setup_chosen 2
                union {
                    Nulltype        release;  /* to choose, set choice to
                                        * csi_ConfigIndex_r10_release_chosen */
                    struct _seq20 {
                        unsigned char   bit_mask;
#                           define      ri_ConfigIndex2_r10_present 0x80
                        unsigned short  cqi_pmi_ConfigIndex2_r10;
                        unsigned short  ri_ConfigIndex2_r10;  /* optional; set
                                   * in bit_mask ri_ConfigIndex2_r10_present if
                                   * present */
                                                              /* Need OR */
                    } setup;  /* to choose, set choice to
                               * csi_ConfigIndex_r10_setup_chosen */
                } u;
            } csi_ConfigIndex_r10;  /* optional; set in bit_mask
                                     * csi_ConfigIndex_r10_present if present */
                                    /* Need ON */
        } setup;  /* to choose, set choice to
                   * CQI_ReportPeriodic_r10_setup_chosen */
    } u;
} CQI_ReportPeriodic_r10;

typedef struct MeasSubframePattern_r10 {
    unsigned short  choice;
#       define      subframePatternFDD_r10_chosen 1
#       define      subframePatternTDD_r10_chosen 2
    union {
        _bit1           subframePatternFDD_r10;  /* to choose, set choice to
                                             * subframePatternFDD_r10_chosen */
        struct _choice7 {
            unsigned short  choice;
#               define      subframeConfig1_5_r10_chosen 1
#               define      subframeConfig0_r10_chosen 2
#               define      subframeConfig6_r10_chosen 3
            union {
                _bit1           subframeConfig1_5_r10;  /* to choose, set choice
                                           * to subframeConfig1_5_r10_chosen */
                _bit1           subframeConfig0_r10;  /* to choose, set choice
                                             * to subframeConfig0_r10_chosen */
                _bit1           subframeConfig6_r10;  /* to choose, set choice
                                             * to subframeConfig6_r10_chosen */
            } u;
        } subframePatternTDD_r10;  /* to choose, set choice to
                                    * subframePatternTDD_r10_chosen */
    } u;
} MeasSubframePattern_r10;

typedef struct CQI_ReportConfig_r10 {
    unsigned char   bit_mask;
#       define      cqi_ReportAperiodic_r10_present 0x80
#       define      cqi_ReportPeriodic_r10_present 0x40
#       define      CQI_ReportConfig_r10_pmi_RI_Report_r9_present 0x20
#       define      csi_SubframePatternConfig_r10_present 0x10
    CQI_ReportAperiodic_r10 cqi_ReportAperiodic_r10;  /* optional; set in
                                   * bit_mask cqi_ReportAperiodic_r10_present if
                                   * present */
                                                      /* Need ON */
    short           nomPDSCH_RS_EPRE_Offset;
    CQI_ReportPeriodic_r10 cqi_ReportPeriodic_r10;  /* optional; set in bit_mask
                                            * cqi_ReportPeriodic_r10_present if
                                            * present */
                                                    /* Need ON */
    _enum17         pmi_RI_Report_r9;  /* optional; set in bit_mask
                             * CQI_ReportConfig_r10_pmi_RI_Report_r9_present if
                             * present */
       /* Cond PMIRIPCell */
    struct {
        unsigned short  choice;
#           define      csi_SubframePatternConfig_r10_release_chosen 1
#           define      csi_SubframePatternConfig_r10_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                              * csi_SubframePatternConfig_r10_release_chosen */
            struct _seq22 {
                MeasSubframePattern_r10 csi_MeasSubframeSet1_r10;
                MeasSubframePattern_r10 csi_MeasSubframeSet2_r10;
            } setup;  /* to choose, set choice to
                       * csi_SubframePatternConfig_r10_setup_chosen */
        } u;
    } csi_SubframePatternConfig_r10;  /* optional; set in bit_mask
                                       * csi_SubframePatternConfig_r10_present
                                       * if present */
                                      /* Need ON */
} CQI_ReportConfig_r10;

typedef struct ZeroTxPowerCSI_RS_r12 {
    _bit1           zeroTxPowerResourceConfigList_r12;
    unsigned short  zeroTxPowerSubframeConfig_r12;
} ZeroTxPowerCSI_RS_r12;

typedef struct ZeroTxPowerCSI_RS_Conf_r12 {
    unsigned short  choice;
#       define      ZeroTxPowerCSI_RS_Conf_r12_release_chosen 1
#       define      ZeroTxPowerCSI_RS_Conf_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                 * ZeroTxPowerCSI_RS_Conf_r12_release_chosen */
        ZeroTxPowerCSI_RS_r12 setup;  /* to choose, set choice to
                                   * ZeroTxPowerCSI_RS_Conf_r12_setup_chosen */
    } u;
} ZeroTxPowerCSI_RS_Conf_r12;

typedef enum _enum24 {
    antennaPortsCount_r10_an1 = 0,
    antennaPortsCount_r10_an2 = 1,
    antennaPortsCount_r10_an4 = 2,
    an8 = 3
} _enum24;

typedef struct CSI_RS_Config_r10 {
    unsigned char   bit_mask;
#       define      csi_RS_r10_present 0x80
#       define      zeroTxPowerCSI_RS_r10_present 0x40
    struct {
        unsigned short  choice;
#           define      csi_RS_r10_release_chosen 1
#           define      csi_RS_r10_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * csi_RS_r10_release_chosen */
            struct _seq23 {
                _enum24         antennaPortsCount_r10;
                unsigned short  resourceConfig_r10;
                unsigned short  subframeConfig_r10;
                short           p_C_r10;
            } setup;  /* to choose, set choice to csi_RS_r10_setup_chosen */
        } u;
    } csi_RS_r10;  /* optional; set in bit_mask csi_RS_r10_present if present */
                       /* Need ON */
    ZeroTxPowerCSI_RS_Conf_r12 zeroTxPowerCSI_RS_r10;  /* optional; set in
                                   * bit_mask zeroTxPowerCSI_RS_r10_present if
                                   * present */
                                                       /* Need ON */
} CSI_RS_Config_r10;

typedef struct _seqof10 {
    struct _seqof10 *next;
    unsigned short  value;
} *_seqof10;

typedef struct PUCCH_ConfigDedicated_v1020 {
    unsigned char   bit_mask;
#       define      pucch_Format_r10_present 0x80
#       define      twoAntennaPortActivatedPUCCH_Format1a1b_r10_present 0x40
#       define      PUCCH_ConfigDedicated_v1020_simultaneousPUCCH_PUSCH_r10_present 0x20
#       define      n1PUCCH_AN_RepP1_r10_present 0x10
    struct {
        unsigned short  choice;
#           define      format3_r10_chosen 1
#           define      channelSelection_r10_chosen 2
        union {
            struct _seq26 {
                unsigned char   bit_mask;
#                   define      n3PUCCH_AN_List_r10_present 0x80
#                   define      twoAntennaPortActivatedPUCCH_Format3_r10_present 0x40
                struct _seqof10 *n3PUCCH_AN_List_r10;  /* optional; set in
                                   * bit_mask n3PUCCH_AN_List_r10_present if
                                   * present */
                                                       /* Need ON */
                struct {
                    unsigned short  choice;
#                       define      twoAntennaPortActivatedPUCCH_Format3_r10_release_chosen 1
#                       define      twoAntennaPortActivatedPUCCH_Format3_r10_setup_chosen 2
                    union {
                        Nulltype        release;  /* to choose, set choice to
                   * twoAntennaPortActivatedPUCCH_Format3_r10_release_chosen */
                        struct _seq24 {
                            struct _seqof10 *n3PUCCH_AN_ListP1_r10;
                        } setup;  /* to choose, set choice to
                     * twoAntennaPortActivatedPUCCH_Format3_r10_setup_chosen */
                    } u;
                } twoAntennaPortActivatedPUCCH_Format3_r10;  /* optional; set in
                                   * bit_mask
                          * twoAntennaPortActivatedPUCCH_Format3_r10_present if
                          * present */
                                                             /* Need ON */
            } format3_r10;  /* to choose, set choice to format3_r10_chosen */
            struct _seq27 {
                unsigned char   bit_mask;
#                   define      n1PUCCH_AN_CS_r10_present 0x80
                struct {
                    unsigned short  choice;
#                       define      n1PUCCH_AN_CS_r10_release_chosen 1
#                       define      n1PUCCH_AN_CS_r10_setup_chosen 2
                    union {
                        Nulltype        release;  /* to choose, set choice to
                                          * n1PUCCH_AN_CS_r10_release_chosen */
                        struct _seq25 {
                            struct _seqof11 {
                                struct _seqof11 *next;
                                struct N1PUCCH_AN_CS_r10 *value;
                            } *n1PUCCH_AN_CS_List_r10;
                        } setup;  /* to choose, set choice to
                                   * n1PUCCH_AN_CS_r10_setup_chosen */
                    } u;
                } n1PUCCH_AN_CS_r10;  /* optional; set in bit_mask
                                       * n1PUCCH_AN_CS_r10_present if present */
                                      /* Need ON */
            } channelSelection_r10;  /* to choose, set choice to
                                      * channelSelection_r10_chosen */
        } u;
    } pucch_Format_r10;  /* optional; set in bit_mask pucch_Format_r10_present
                          * if present */
       /* Need OR */
    _enum3          twoAntennaPortActivatedPUCCH_Format1a1b_r10;  /* optional;
                                   * set in bit_mask
                       * twoAntennaPortActivatedPUCCH_Format1a1b_r10_present if
                       * present */
       /* Need OR */
    _enum3          simultaneousPUCCH_PUSCH_r10;  /* optional; set in bit_mask
           * PUCCH_ConfigDedicated_v1020_simultaneousPUCCH_PUSCH_r10_present if
           * present */
       /* Need OR */
    unsigned short  n1PUCCH_AN_RepP1_r10;  /* optional; set in bit_mask
                                            * n1PUCCH_AN_RepP1_r10_present if
                                            * present */
                                           /* Need OR */
} PUCCH_ConfigDedicated_v1020;

typedef struct PUSCH_ConfigDedicated_v1020 {
    unsigned char   bit_mask;
#       define      betaOffsetMC_r10_present 0x80
#       define      PUSCH_ConfigDedicated_v1020_groupHoppingDisabled_r10_present 0x40
#       define      PUSCH_ConfigDedicated_v1020_dmrs_WithOCC_Activated_r10_present 0x20
    struct {
        unsigned short  betaOffset_ACK_Index_MC_r10;
        unsigned short  betaOffset_RI_Index_MC_r10;
        unsigned short  betaOffset_CQI_Index_MC_r10;
    } betaOffsetMC_r10;  /* optional; set in bit_mask betaOffsetMC_r10_present
                          * if present */
       /* Need OR */
    _enum3          groupHoppingDisabled_r10;  /* optional; set in bit_mask
              * PUSCH_ConfigDedicated_v1020_groupHoppingDisabled_r10_present if
              * present */
       /* Need OR */
    _enum3          dmrs_WithOCC_Activated_r10;  /* optional; set in bit_mask
            * PUSCH_ConfigDedicated_v1020_dmrs_WithOCC_Activated_r10_present if
            * present */
                                                 /* Need OR */
} PUSCH_ConfigDedicated_v1020;

typedef struct SchedulingRequestConfig_v1020 {
    unsigned char   bit_mask;
#       define      sr_PUCCH_ResourceIndexP1_r10_present 0x80
    unsigned short  sr_PUCCH_ResourceIndexP1_r10;  /* optional; set in bit_mask
                                      * sr_PUCCH_ResourceIndexP1_r10_present if
                                      * present */
                                                   /* Need OR */
} SchedulingRequestConfig_v1020;

typedef enum SRS_AntennaPort {
    SRS_AntennaPort_an1 = 0,
    SRS_AntennaPort_an2 = 1,
    SRS_AntennaPort_an4 = 2,
    SRS_AntennaPort_spare1 = 3
} SRS_AntennaPort;

typedef struct SoundingRS_UL_ConfigDedicated_v1020 {
    SRS_AntennaPort srs_AntennaPort_r10;
} SoundingRS_UL_ConfigDedicated_v1020;

typedef struct SRS_ConfigAp_r10 {
    SRS_AntennaPort srs_AntennaPortAp_r10;
    _enum20         srs_BandwidthAp_r10;
    unsigned short  freqDomainPositionAp_r10;
    unsigned short  transmissionCombAp_r10;
    _enum21         cyclicShiftAp_r10;
} SRS_ConfigAp_r10;

typedef struct SoundingRS_UL_ConfigDedicatedAperiodic_r10 {
    unsigned short  choice;
#       define      SoundingRS_UL_ConfigDedicatedAperiodic_r10_release_chosen 1
#       define      SoundingRS_UL_ConfigDedicatedAperiodic_r10_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                 * SoundingRS_UL_ConfigDedicatedAperiodic_r10_release_chosen */
        struct _seq29 {
            unsigned char   bit_mask;
#               define      srs_ConfigApDCI_Format4_r10_present 0x80
#               define      srs_ActivateAp_r10_present 0x40
            unsigned short  srs_ConfigIndexAp_r10;
            struct _seqof12 {
                struct _seqof12 *next;
                SRS_ConfigAp_r10 value;
            } *srs_ConfigApDCI_Format4_r10;  /* optional; set in bit_mask
                                       * srs_ConfigApDCI_Format4_r10_present if
                                       * present */
                                             /*Need ON */
            struct {
                unsigned short  choice;
#                   define      srs_ActivateAp_r10_release_chosen 1
#                   define      srs_ActivateAp_r10_setup_chosen 2
                union {
                    Nulltype        release;  /* to choose, set choice to
                                         * srs_ActivateAp_r10_release_chosen */
                    struct _seq28 {
                        SRS_ConfigAp_r10 srs_ConfigApDCI_Format0_r10;
                        SRS_ConfigAp_r10 srs_ConfigApDCI_Format1a2b2c_r10;
                    } setup;  /* to choose, set choice to
                               * srs_ActivateAp_r10_setup_chosen */
                } u;
            } srs_ActivateAp_r10;  /* optional; set in bit_mask
                                    * srs_ActivateAp_r10_present if present */
                                   /* Need ON */
        } setup;  /* to choose, set choice to
                   * SoundingRS_UL_ConfigDedicatedAperiodic_r10_setup_chosen */
    } u;
} SoundingRS_UL_ConfigDedicatedAperiodic_r10;

typedef enum _enum25 {
    deltaTxD_OffsetPUCCH_Format1_r10_dB0 = 0,
    deltaTxD_OffsetPUCCH_Format1_r10_dB_2 = 1
} _enum25;

typedef struct DeltaTxD_OffsetListPUCCH_r10 {
    _enum25         deltaTxD_OffsetPUCCH_Format1_r10;
    _enum25         deltaTxD_OffsetPUCCH_Format1a1b_r10;
    _enum25         deltaTxD_OffsetPUCCH_Format22a2b_r10;
    _enum25         deltaTxD_OffsetPUCCH_Format3_r10;
} DeltaTxD_OffsetListPUCCH_r10;

typedef struct UplinkPowerControlDedicated_v1020 {
    unsigned char   bit_mask;
#       define      deltaTxD_OffsetListPUCCH_r10_present 0x80
#       define      UplinkPowerControlDedicated_v1020_pSRS_OffsetAp_r10_present 0x40
    DeltaTxD_OffsetListPUCCH_r10 deltaTxD_OffsetListPUCCH_r10;  /* optional; set
                                   * in bit_mask
                                   * deltaTxD_OffsetListPUCCH_r10_present if
                                   * present */
                                                                /* Need OR */
    unsigned short  pSRS_OffsetAp_r10;  /* optional; set in bit_mask
               * UplinkPowerControlDedicated_v1020_pSRS_OffsetAp_r10_present if
               * present */
                                        /* Need OR */
} UplinkPowerControlDedicated_v1020;

typedef struct EPDCCH_Config_r11 {
    struct {
        unsigned short  choice;
#           define      config_r11_release_chosen 1
#           define      config_r11_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * config_r11_release_chosen */
            struct _seq31 {
                unsigned char   bit_mask;
#                   define      subframePatternConfig_r11_present 0x80
#                   define      startSymbol_r11_present 0x40
#                   define      setConfigToReleaseList_r11_present 0x20
#                   define      setConfigToAddModList_r11_present 0x10
                struct {
                    unsigned short  choice;
#                       define      subframePatternConfig_r11_release_chosen 1
#                       define      subframePatternConfig_r11_setup_chosen 2
                    union {
                        Nulltype        release;  /* to choose, set choice to
                                  * subframePatternConfig_r11_release_chosen */
                        struct _seq30 {
                            MeasSubframePattern_r10 subframePattern_r11;
                        } setup;  /* to choose, set choice to
                                   * subframePatternConfig_r11_setup_chosen */
                    } u;
                } subframePatternConfig_r11;  /* optional; set in bit_mask
                                         * subframePatternConfig_r11_present if
                                         * present */
 /* Need ON */
                unsigned short  startSymbol_r11;  /* optional; set in bit_mask
                                                   * startSymbol_r11_present if
                                                   * present */
                                                  /* Need OP */
                struct EPDCCH_SetConfigToReleaseList_r11 *setConfigToReleaseList_r11;                                   /* optional; set in bit_mask
                                         * setConfigToReleaseList_r11_present if
                                         * present */
                                                                                       /* Need ON */
                struct EPDCCH_SetConfigToAddModList_r11 *setConfigToAddModList_r11;                                     /* optional; set in bit_mask
                                         * setConfigToAddModList_r11_present if
                                         * present */
                                                                                     /* Need ON */
            } setup;  /* to choose, set choice to config_r11_setup_chosen */
        } u;
    } config_r11;
} EPDCCH_Config_r11;

typedef struct DMRS_Config_r11 {
    unsigned short  choice;
#       define      DMRS_Config_r11_release_chosen 1
#       define      DMRS_Config_r11_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * DMRS_Config_r11_release_chosen */
        struct _seq32 {
            unsigned short  scramblingIdentity_r11;
            unsigned short  scramblingIdentity2_r11;
        } setup;  /* to choose, set choice to DMRS_Config_r11_setup_chosen */
    } u;
} DMRS_Config_r11;

typedef struct PDSCH_ConfigDedicated_v1130 {
    unsigned char   bit_mask;
#       define      dmrs_ConfigPDSCH_r11_present 0x80
#       define      qcl_Operation_present 0x40
#       define      re_MappingQCLConfigToReleaseList_r11_present 0x20
#       define      re_MappingQCLConfigToAddModList_r11_present 0x10
    DMRS_Config_r11 dmrs_ConfigPDSCH_r11;  /* optional; set in bit_mask
                                            * dmrs_ConfigPDSCH_r11_present if
                                            * present */
                                           /* Need ON */
    enum {
        typeA = 0,
        typeB = 1
    } qcl_Operation;  /* optional; set in bit_mask qcl_Operation_present if
                       * present */
       /* Need OR */
    struct RE_MappingQCLConfigToReleaseList_r11 *re_MappingQCLConfigToReleaseList_r11;                                  /* optional; set in bit_mask
                              * re_MappingQCLConfigToReleaseList_r11_present if
                              * present */
                                                                                        /* Need ON */
    struct RE_MappingQCLConfigToAddModList_r11 *re_MappingQCLConfigToAddModList_r11;                                    /* optional; set in bit_mask
                               * re_MappingQCLConfigToAddModList_r11_present if
                               * present */
                                                                                      /* Need ON */
} PDSCH_ConfigDedicated_v1130;

typedef struct CQI_ReportPeriodic_v1130 {
    unsigned char   bit_mask;
#       define      simultaneousAckNackAndCQI_Format3_r11_present 0x80
#       define      cqi_ReportPeriodicProcExtToReleaseList_r11_present 0x40
#       define      cqi_ReportPeriodicProcExtToAddModList_r11_present 0x20
    _enum17         simultaneousAckNackAndCQI_Format3_r11;  /* optional; set in
                                   * bit_mask
                             * simultaneousAckNackAndCQI_Format3_r11_present if
                             * present */
       /* Need OR */
    struct CQI_ReportPeriodicProcExtToReleaseList_r11 *cqi_ReportPeriodicProcExtToReleaseList_r11;                      /* optional; set in bit_mask
                        * cqi_ReportPeriodicProcExtToReleaseList_r11_present if
                        * present */
                                                                                                    /* Need ON */
    struct CQI_ReportPeriodicProcExtToAddModList_r11 *cqi_ReportPeriodicProcExtToAddModList_r11;                        /* optional; set in bit_mask
                         * cqi_ReportPeriodicProcExtToAddModList_r11_present if
                         * present */
                                                                                                  /* Need ON */
} CQI_ReportPeriodic_v1130;

typedef struct CQI_ReportBoth_r11 {
    unsigned char   bit_mask;
#       define      csi_IM_ConfigToReleaseList_r11_present 0x80
#       define      csi_IM_ConfigToAddModList_r11_present 0x40
#       define      csi_ProcessToReleaseList_r11_present 0x20
#       define      csi_ProcessToAddModList_r11_present 0x10
    struct CSI_IM_ConfigToReleaseList_r11 *csi_IM_ConfigToReleaseList_r11;  
                                        /* optional; set in bit_mask
                                    * csi_IM_ConfigToReleaseList_r11_present if
                                    * present */
                                                                            /* Need ON */
    struct CSI_IM_ConfigToAddModList_r11 *csi_IM_ConfigToAddModList_r11;  
                                        /* optional; set in bit_mask
                                         * csi_IM_ConfigToAddModList_r11_present
                                         * if present */
                                                                          /* Need ON */
    struct CSI_ProcessToReleaseList_r11 *csi_ProcessToReleaseList_r11;  
                                  /* optional; set in bit_mask
                                   * csi_ProcessToReleaseList_r11_present if
                                   * present */
                                                                        /* Need ON */
    struct CSI_ProcessToAddModList_r11 *csi_ProcessToAddModList_r11;  
                                  /* optional; set in bit_mask
                                   * csi_ProcessToAddModList_r11_present if
                                   * present */
                                                                      /* Need ON */
} CQI_ReportBoth_r11;

typedef struct CQI_ReportConfig_v1130 {
    CQI_ReportPeriodic_v1130 cqi_ReportPeriodic_v1130;
    CQI_ReportBoth_r11 cqi_ReportBoth_r11;
} CQI_ReportConfig_v1130;

typedef struct PUCCH_ConfigDedicated_v1130 {
    unsigned char   bit_mask;
#       define      n1PUCCH_AN_CS_v1130_present 0x80
#       define      nPUCCH_Param_r11_present 0x40
    struct {
        unsigned short  choice;
#           define      n1PUCCH_AN_CS_v1130_release_chosen 1
#           define      n1PUCCH_AN_CS_v1130_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * n1PUCCH_AN_CS_v1130_release_chosen */
            struct _seq33 {
                struct _seqof10 *n1PUCCH_AN_CS_ListP1_r11;
            } setup;  /* to choose, set choice to
                       * n1PUCCH_AN_CS_v1130_setup_chosen */
        } u;
    } n1PUCCH_AN_CS_v1130;  /* optional; set in bit_mask
                             * n1PUCCH_AN_CS_v1130_present if present */
       /* Need ON */
    struct {
        unsigned short  choice;
#           define      nPUCCH_Param_r11_release_chosen 1
#           define      nPUCCH_Param_r11_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * nPUCCH_Param_r11_release_chosen */
            struct _seq34 {
                unsigned short  nPUCCH_Identity_r11;
                unsigned short  n1PUCCH_AN_r11;
            } setup;  /* to choose, set choice to
                       * nPUCCH_Param_r11_setup_chosen */
        } u;
    } nPUCCH_Param_r11;  /* optional; set in bit_mask nPUCCH_Param_r11_present
                          * if present */
                         /* Need ON */
} PUCCH_ConfigDedicated_v1130;

typedef struct PUSCH_ConfigDedicated_v1130 {
    struct {
        unsigned short  choice;
#           define      pusch_DMRS_r11_release_chosen 1
#           define      pusch_DMRS_r11_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * pusch_DMRS_r11_release_chosen */
            struct _seq35 {
                unsigned short  nPUSCH_Identity_r11;
                unsigned short  nDMRS_CSH_Identity_r11;
            } setup;  /* to choose, set choice to pusch_DMRS_r11_setup_chosen */
        } u;
    } pusch_DMRS_r11;
} PUSCH_ConfigDedicated_v1130;

typedef struct DeltaTxD_OffsetListPUCCH_v1130 {
    enum {
        deltaTxD_OffsetPUCCH_Format1bCS_r11_dB0 = 0,
        deltaTxD_OffsetPUCCH_Format1bCS_r11_dB_1 = 1
    } deltaTxD_OffsetPUCCH_Format1bCS_r11;
} DeltaTxD_OffsetListPUCCH_v1130;

typedef struct UplinkPowerControlDedicated_v1130 {
    unsigned char   bit_mask;
#       define      pSRS_Offset_v1130_present 0x80
#       define      pSRS_OffsetAp_v1130_present 0x40
#       define      deltaTxD_OffsetListPUCCH_v1130_present 0x20
    unsigned short  pSRS_Offset_v1130;  /* optional; set in bit_mask
                                         * pSRS_Offset_v1130_present if
                                         * present */
                                        /* Need OR */
    unsigned short  pSRS_OffsetAp_v1130;  /* optional; set in bit_mask
                                           * pSRS_OffsetAp_v1130_present if
                                           * present */
                                          /* Need OR */
    DeltaTxD_OffsetListPUCCH_v1130 deltaTxD_OffsetListPUCCH_v1130;  
                                  /* optional; set in bit_mask
                                   * deltaTxD_OffsetListPUCCH_v1130_present if
                                   * present */
                                                                    /* Need OR */
} UplinkPowerControlDedicated_v1130;

typedef struct AntennaInfoDedicated_v1250 {
    ossBoolean      alternativeCodebookEnabledFor4TX_r12;
} AntennaInfoDedicated_v1250;

typedef struct EIMTA_MainConfig_r12 {
    unsigned short  choice;
#       define      EIMTA_MainConfig_r12_release_chosen 1
#       define      EIMTA_MainConfig_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * EIMTA_MainConfig_r12_release_chosen */
        struct _seq36 {
            C_RNTI          eimta_RNTI_r12;
            enum {
                eimta_CommandPeriodicity_r12_sf10 = 0,
                eimta_CommandPeriodicity_r12_sf20 = 1,
                eimta_CommandPeriodicity_r12_sf40 = 2,
                eimta_CommandPeriodicity_r12_sf80 = 3
            } eimta_CommandPeriodicity_r12;
            _bit1           eimta_CommandSubframeSet_r12;
        } setup;  /* to choose, set choice to
                   * EIMTA_MainConfig_r12_setup_chosen */
    } u;
} EIMTA_MainConfig_r12;

typedef struct EIMTA_MainConfigServCell_r12 {
    unsigned short  choice;
#       define      EIMTA_MainConfigServCell_r12_release_chosen 1
#       define      EIMTA_MainConfigServCell_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                               * EIMTA_MainConfigServCell_r12_release_chosen */
        struct _seq38 {
            unsigned short  eimta_UL_DL_ConfigIndex_r12;
            enum {
                eimta_HARQ_ReferenceConfig_r12_sa2 = 0,
                eimta_HARQ_ReferenceConfig_r12_sa4 = 1,
                eimta_HARQ_ReferenceConfig_r12_sa5 = 2
            } eimta_HARQ_ReferenceConfig_r12;
            struct {
                unsigned short  choice;
#                   define      mbsfn_SubframeConfigList_v1250_release_chosen 1
#                   define      mbsfn_SubframeConfigList_v1250_setup_chosen 2
                union {
                    Nulltype        release;  /* to choose, set choice to
                             * mbsfn_SubframeConfigList_v1250_release_chosen */
                    struct _seq37 {
                        struct MBSFN_SubframeConfigList *subframeConfigList_r12;
                    } setup;  /* to choose, set choice to
                               * mbsfn_SubframeConfigList_v1250_setup_chosen */
                } u;
            } mbsfn_SubframeConfigList_v1250;
        } setup;  /* to choose, set choice to
                   * EIMTA_MainConfigServCell_r12_setup_chosen */
    } u;
} EIMTA_MainConfigServCell_r12;

typedef struct PUCCH_ConfigDedicated_v1250 {
    struct {
        unsigned short  choice;
#           define      nkaPUCCH_Param_r12_release_chosen 1
#           define      nkaPUCCH_Param_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * nkaPUCCH_Param_r12_release_chosen */
            struct _seq39 {
                unsigned short  nkaPUCCH_AN_r12;
            } setup;  /* to choose, set choice to
                       * nkaPUCCH_Param_r12_setup_chosen */
        } u;
    } nkaPUCCH_Param_r12;
} PUCCH_ConfigDedicated_v1250;

typedef unsigned short  CSI_IM_ConfigId_v1250;

typedef struct CSI_IM_ConfigExt_r12 {
    CSI_IM_ConfigId_v1250 csi_IM_ConfigId_v1250;
    unsigned short  resourceConfig_r12;
    unsigned short  subframeConfig_r12;
} CSI_IM_ConfigExt_r12;

typedef struct CQI_ReportBoth_v1250 {
    unsigned char   bit_mask;
#       define      csi_IM_ConfigToReleaseListExt_r12_present 0x80
#       define      csi_IM_ConfigToAddModListExt_r12_present 0x40
    CSI_IM_ConfigId_v1250 csi_IM_ConfigToReleaseListExt_r12;  /* optional; set
                                   * in bit_mask
                                   * csi_IM_ConfigToReleaseListExt_r12_present
                                   * if present */
                                                              /* Need ON */
    CSI_IM_ConfigExt_r12 csi_IM_ConfigToAddModListExt_r12;  /* optional; set in
                                   * bit_mask
                                   * csi_IM_ConfigToAddModListExt_r12_present if
                                   * present */
                                                            /* Need ON */
} CQI_ReportBoth_v1250;

typedef enum _enum26 {
    trigger_SubframeSetIndicator_r12_s1 = 0,
    trigger_SubframeSetIndicator_r12_s2 = 1
} _enum26;

typedef struct CQI_ReportAperiodic_v1250 {
    unsigned short  choice;
#       define      CQI_ReportAperiodic_v1250_release_chosen 1
#       define      CQI_ReportAperiodic_v1250_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                  * CQI_ReportAperiodic_v1250_release_chosen */
        struct _seq40 {
            struct {
                _enum26         trigger_SubframeSetIndicator_r12;
                _bit1           trigger1_SubframeSetIndicator_r12;
                _bit1           trigger2_SubframeSetIndicator_r12;
            } aperiodicCSI_Trigger_v1250;
        } setup;  /* to choose, set choice to
                   * CQI_ReportAperiodic_v1250_setup_chosen */
    } u;
} CQI_ReportAperiodic_v1250;

typedef struct CQI_ReportConfig_v1250 {
    unsigned char   bit_mask;
#       define      csi_SubframePatternConfig_r12_present 0x80
#       define      cqi_ReportBoth_v1250_present 0x40
#       define      cqi_ReportAperiodic_v1250_present 0x20
#       define      altCQI_Table_r12_present 0x10
    struct {
        unsigned short  choice;
#           define      csi_SubframePatternConfig_r12_release_chosen 1
#           define      csi_SubframePatternConfig_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                              * csi_SubframePatternConfig_r12_release_chosen */
            struct _seq41 {
                _bit1           csi_MeasSubframeSets_r12;
            } setup;  /* to choose, set choice to
                       * csi_SubframePatternConfig_r12_setup_chosen */
        } u;
    } csi_SubframePatternConfig_r12;  /* optional; set in bit_mask
                                       * csi_SubframePatternConfig_r12_present
                                       * if present */
       /* Need ON */
    CQI_ReportBoth_v1250 cqi_ReportBoth_v1250;  /* optional; set in bit_mask
                                                 * cqi_ReportBoth_v1250_present
                                                 * if present */
                                                /* Need ON */
    CQI_ReportAperiodic_v1250 cqi_ReportAperiodic_v1250;  /* optional; set in
                                   * bit_mask cqi_ReportAperiodic_v1250_present
                                   * if present */
                                                          /* Need ON */
    enum {
        allSubframes = 0,
        csi_SubframeSet1 = 1,
        csi_SubframeSet2 = 2,
        altCQI_Table_r12_spare1 = 3
    } altCQI_Table_r12;  /* optional; set in bit_mask altCQI_Table_r12_present
                          * if present */
                         /* Need OP */
} CQI_ReportConfig_v1250;

typedef struct UplinkPowerControlDedicated_v1250 {
    struct {
        unsigned short  choice;
#           define      set2PowerControlParameter_release_chosen 1
#           define      set2PowerControlParameter_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                  * set2PowerControlParameter_release_chosen */
            struct _seq42 {
                _bit1           tpc_SubframeSet_r12;
                short           p0_NominalPUSCH_SubframeSet2_r12;
                Alpha_r12       alpha_SubframeSet2_r12;
                short           p0_UE_PUSCH_SubframeSet2_r12;
            } setup;  /* to choose, set choice to
                       * set2PowerControlParameter_setup_chosen */
        } u;
    } set2PowerControlParameter;
} UplinkPowerControlDedicated_v1250;

typedef struct PUSCH_ConfigDedicated_v1250 {
    struct {
        unsigned short  choice;
#           define      uciOnPUSCH_release_chosen 1
#           define      uciOnPUSCH_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * uciOnPUSCH_release_chosen */
            struct _seq43 {
                unsigned char   bit_mask;
#                   define      betaOffsetMC_r12_present 0x80
                unsigned short  betaOffset_ACK_Index_SubframeSet2_r12;
                unsigned short  betaOffset_RI_Index_SubframeSet2_r12;
                unsigned short  betaOffset_CQI_Index_SubframeSet2_r12;
                struct {
                    unsigned short  betaOffset_ACK_Index_MC_SubframeSet2_r12;
                    unsigned short  betaOffset_RI_Index_MC_SubframeSet2_r12;
                    unsigned short  betaOffset_CQI_Index_MC_SubframeSet2_r12;
                } betaOffsetMC_r12;  /* optional; set in bit_mask
                                      * betaOffsetMC_r12_present if present */
                                     /* Need OR */
            } setup;  /* to choose, set choice to uciOnPUSCH_setup_chosen */
        } u;
    } uciOnPUSCH;
} PUSCH_ConfigDedicated_v1250;

typedef struct CSI_RS_Config_v1250 {
    unsigned char   bit_mask;
#       define      zeroTxPowerCSI_RS2_r12_present 0x80
#       define      ds_ZeroTxPowerCSI_RS_r12_present 0x40
    ZeroTxPowerCSI_RS_Conf_r12 zeroTxPowerCSI_RS2_r12;  /* optional; set in
                                   * bit_mask zeroTxPowerCSI_RS2_r12_present if
                                   * present */
                                                        /* Need ON */
    struct {
        unsigned short  choice;
#           define      ds_ZeroTxPowerCSI_RS_r12_release_chosen 1
#           define      ds_ZeroTxPowerCSI_RS_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                   * ds_ZeroTxPowerCSI_RS_r12_release_chosen */
            struct _seq44 {
                struct _seqof13 {
                    struct _seqof13 *next;
                    ZeroTxPowerCSI_RS_r12 value;
                } *zeroTxPowerCSI_RS_List_r12;
            } setup;  /* to choose, set choice to
                       * ds_ZeroTxPowerCSI_RS_r12_setup_chosen */
        } u;
    } ds_ZeroTxPowerCSI_RS_r12;  /* optional; set in bit_mask
                                  * ds_ZeroTxPowerCSI_RS_r12_present if
                                  * present */
                                 /* Need ON */
} CSI_RS_Config_v1250;

typedef struct PhysicalConfigDedicated {
    unsigned char   bit_mask[6];
#       define      pdsch_ConfigDedicated_present 0x80
#       define      pdsch_ConfigDedicated_present_byte 0
#       define      pucch_ConfigDedicated_present 0x40
#       define      pucch_ConfigDedicated_present_byte 0
#       define      pusch_ConfigDedicated_present 0x20
#       define      pusch_ConfigDedicated_present_byte 0
#       define      uplinkPowerControlDedicated_present 0x10
#       define      uplinkPowerControlDedicated_present_byte 0
#       define      tpc_PDCCH_ConfigPUCCH_present 0x08
#       define      tpc_PDCCH_ConfigPUCCH_present_byte 0
#       define      tpc_PDCCH_ConfigPUSCH_present 0x04
#       define      tpc_PDCCH_ConfigPUSCH_present_byte 0
#       define      cqi_ReportConfig_present 0x02
#       define      cqi_ReportConfig_present_byte 0
#       define      soundingRS_UL_ConfigDedicated_present 0x01
#       define      soundingRS_UL_ConfigDedicated_present_byte 0
#       define      antennaInfo_present 0x80
#       define      antennaInfo_present_byte 1
#       define      schedulingRequestConfig_present 0x40
#       define      schedulingRequestConfig_present_byte 1
#       define      cqi_ReportConfig_v920_present 0x20
#       define      cqi_ReportConfig_v920_present_byte 1
#       define      antennaInfo_v920_present 0x10
#       define      antennaInfo_v920_present_byte 1
#       define      PhysicalConfigDedicated_antennaInfo_r10_present 0x08
#       define      PhysicalConfigDedicated_antennaInfo_r10_present_byte 1
#       define      PhysicalConfigDedicated_antennaInfoUL_r10_present 0x04
#       define      PhysicalConfigDedicated_antennaInfoUL_r10_present_byte 1
#       define      cif_Presence_r10_present 0x02
#       define      cif_Presence_r10_present_byte 1
#       define      cqi_ReportConfig_r10_present 0x01
#       define      cqi_ReportConfig_r10_present_byte 1
#       define      PhysicalConfigDedicated_csi_RS_Config_r10_present 0x80
#       define      PhysicalConfigDedicated_csi_RS_Config_r10_present_byte 2
#       define      pucch_ConfigDedicated_v1020_present 0x40
#       define      pucch_ConfigDedicated_v1020_present_byte 2
#       define      pusch_ConfigDedicated_v1020_present 0x20
#       define      pusch_ConfigDedicated_v1020_present_byte 2
#       define      schedulingRequestConfig_v1020_present 0x10
#       define      schedulingRequestConfig_v1020_present_byte 2
#       define      PhysicalConfigDedicated_soundingRS_UL_ConfigDedicated_v1020_present 0x08
#       define      PhysicalConfigDedicated_soundingRS_UL_ConfigDedicated_v1020_present_byte 2
#       define      PhysicalConfigDedicated_soundingRS_UL_ConfigDedicatedAperiodic_r10_present 0x04
#       define      PhysicalConfigDedicated_soundingRS_UL_ConfigDedicatedAperiodic_r10_present_byte 2
#       define      uplinkPowerControlDedicated_v1020_present 0x02
#       define      uplinkPowerControlDedicated_v1020_present_byte 2
#       define      additionalSpectrumEmissionCA_r10_present 0x01
#       define      additionalSpectrumEmissionCA_r10_present_byte 2
#       define      PhysicalConfigDedicated_csi_RS_ConfigNZPToReleaseList_r11_present 0x80
#       define      PhysicalConfigDedicated_csi_RS_ConfigNZPToReleaseList_r11_present_byte 3
#       define      PhysicalConfigDedicated_csi_RS_ConfigNZPToAddModList_r11_present 0x40
#       define      PhysicalConfigDedicated_csi_RS_ConfigNZPToAddModList_r11_present_byte 3
#       define      PhysicalConfigDedicated_csi_RS_ConfigZPToReleaseList_r11_present 0x20
#       define      PhysicalConfigDedicated_csi_RS_ConfigZPToReleaseList_r11_present_byte 3
#       define      PhysicalConfigDedicated_csi_RS_ConfigZPToAddModList_r11_present 0x10
#       define      PhysicalConfigDedicated_csi_RS_ConfigZPToAddModList_r11_present_byte 3
#       define      PhysicalConfigDedicated_epdcch_Config_r11_present 0x08
#       define      PhysicalConfigDedicated_epdcch_Config_r11_present_byte 3
#       define      PhysicalConfigDedicated_pdsch_ConfigDedicated_v1130_present 0x04
#       define      PhysicalConfigDedicated_pdsch_ConfigDedicated_v1130_present_byte 3
#       define      PhysicalConfigDedicated_cqi_ReportConfig_v1130_present 0x02
#       define      PhysicalConfigDedicated_cqi_ReportConfig_v1130_present_byte 3
#       define      pucch_ConfigDedicated_v1130_present 0x01
#       define      pucch_ConfigDedicated_v1130_present_byte 3
#       define      PhysicalConfigDedicated_pusch_ConfigDedicated_v1130_present 0x80
#       define      PhysicalConfigDedicated_pusch_ConfigDedicated_v1130_present_byte 4
#       define      uplinkPowerControlDedicated_v1130_present 0x40
#       define      uplinkPowerControlDedicated_v1130_present_byte 4
#       define      PhysicalConfigDedicated_antennaInfo_v1250_present 0x20
#       define      PhysicalConfigDedicated_antennaInfo_v1250_present_byte 4
#       define      eimta_MainConfig_r12_present 0x10
#       define      eimta_MainConfig_r12_present_byte 4
#       define      eimta_MainConfigPCell_r12_present 0x08
#       define      eimta_MainConfigPCell_r12_present_byte 4
#       define      pucch_ConfigDedicated_v1250_present 0x04
#       define      pucch_ConfigDedicated_v1250_present_byte 4
#       define      cqi_ReportConfigPCell_v1250_present 0x02
#       define      cqi_ReportConfigPCell_v1250_present_byte 4
#       define      uplinkPowerControlDedicated_v1250_present 0x01
#       define      uplinkPowerControlDedicated_v1250_present_byte 4
#       define      pusch_ConfigDedicated_v1250_present 0x80
#       define      pusch_ConfigDedicated_v1250_present_byte 5
#       define      PhysicalConfigDedicated_csi_RS_Config_v1250_present 0x40
#       define      PhysicalConfigDedicated_csi_RS_Config_v1250_present_byte 5
    PDSCH_ConfigDedicated pdsch_ConfigDedicated;  /* optional; set in bit_mask
                                             * pdsch_ConfigDedicated_present if
                                             * present */
                                                  /* Need ON */
    PUCCH_ConfigDedicated pucch_ConfigDedicated;  /* optional; set in bit_mask
                                             * pucch_ConfigDedicated_present if
                                             * present */
                                                  /* Need ON */
    PUSCH_ConfigDedicated pusch_ConfigDedicated;  /* optional; set in bit_mask
                                             * pusch_ConfigDedicated_present if
                                             * present */
                                                  /* Need ON */
    UplinkPowerControlDedicated uplinkPowerControlDedicated;  /* optional; set
                                   * in bit_mask
                                   * uplinkPowerControlDedicated_present if
                                   * present */
                                                              /* Need ON */
    TPC_PDCCH_Config tpc_PDCCH_ConfigPUCCH;  /* optional; set in bit_mask
                                              * tpc_PDCCH_ConfigPUCCH_present if
                                              * present */
                                             /* Need ON */
    TPC_PDCCH_Config tpc_PDCCH_ConfigPUSCH;  /* optional; set in bit_mask
                                              * tpc_PDCCH_ConfigPUSCH_present if
                                              * present */
                                             /* Need ON */
    CQI_ReportConfig cqi_ReportConfig;  /* optional; set in bit_mask
                                         * cqi_ReportConfig_present if
                                         * present */
                                        /* Cond CQI-r8 */
    SoundingRS_UL_ConfigDedicated soundingRS_UL_ConfigDedicated;  /* optional;
                                   * set in bit_mask
                                   * soundingRS_UL_ConfigDedicated_present if
                                   * present */
                                                                  /* Need ON */
    struct {
        unsigned short  choice;
#           define      antennaInfo_explicitValue_chosen 1
#           define      antennaInfo_defaultValue_chosen 2
        union {
            AntennaInfoDedicated explicitValue;  /* to choose, set choice to
                                          * antennaInfo_explicitValue_chosen */
            Nulltype        defaultValue;  /* to choose, set choice to
                                           * antennaInfo_defaultValue_chosen */
        } u;
    } antennaInfo;  /* optional; set in bit_mask antennaInfo_present if
                     * present */
                                                                                                                               /* Cond AI-r8 */
    SchedulingRequestConfig schedulingRequestConfig;  /* optional; set in
                                   * bit_mask schedulingRequestConfig_present if
                                   * present */
                                                      /* Need ON */
    CQI_ReportConfig_v920 cqi_ReportConfig_v920;  /* extension #1; optional; set
                                   * in bit_mask cqi_ReportConfig_v920_present
                                   * if present */
                                                  /* Cond CQI-r8 */
    AntennaInfoDedicated_v920 antennaInfo_v920;  /* extension #1; optional; set
                                   * in bit_mask antennaInfo_v920_present if
                                   * present */
                                                 /* Cond AI-r8 */
    struct {
        unsigned short  choice;
#           define      explicitValue_r10_chosen 1
#           define      antennaInfo_r10_defaultValue_chosen 2
        union {
            AntennaInfoDedicated_r10 explicitValue_r10;  /* to choose, set
                                        * choice to explicitValue_r10_chosen */
            Nulltype        defaultValue;  /* to choose, set choice to
                                       * antennaInfo_r10_defaultValue_chosen */
        } u;
    } antennaInfo_r10;  /* extension #2; optional; set in bit_mask
                         * PhysicalConfigDedicated_antennaInfo_r10_present if
                         * present */
                                                                                                                       /* Cond AI-r10 */
    AntennaInfoUL_r10 antennaInfoUL_r10;  /* extension #2; optional; set in
                                   * bit_mask
                         * PhysicalConfigDedicated_antennaInfoUL_r10_present if
                         * present */
                                          /* Need ON */
    ossBoolean      cif_Presence_r10;  /* extension #2; optional; set in
                                   * bit_mask cif_Presence_r10_present if
                                   * present */
                                       /* Need ON */
    CQI_ReportConfig_r10 cqi_ReportConfig_r10;  /* extension #2; optional; set
                                   * in bit_mask cqi_ReportConfig_r10_present if
                                   * present */
                                                /* Cond CQI-r10 */
    CSI_RS_Config_r10 csi_RS_Config_r10;  /* extension #2; optional; set in
                                   * bit_mask
                         * PhysicalConfigDedicated_csi_RS_Config_r10_present if
                         * present */
                                          /* Need ON */
    PUCCH_ConfigDedicated_v1020 pucch_ConfigDedicated_v1020;  /* extension #2;
                                   * optional; set in bit_mask
                                   * pucch_ConfigDedicated_v1020_present if
                                   * present */
                                                              /* Need ON */
    PUSCH_ConfigDedicated_v1020 pusch_ConfigDedicated_v1020;  /* extension #2;
                                   * optional; set in bit_mask
                                   * pusch_ConfigDedicated_v1020_present if
                                   * present */
                                                              /* Need ON */
    SchedulingRequestConfig_v1020 schedulingRequestConfig_v1020;  /* extension
                                   * #2; optional; set in bit_mask
                                   * schedulingRequestConfig_v1020_present if
                                   * present */
                                                                  /* Need ON */
    SoundingRS_UL_ConfigDedicated_v1020 soundingRS_UL_ConfigDedicated_v1020;  
                                        /* extension #2; optional; set in
                                   * bit_mask
       * PhysicalConfigDedicated_soundingRS_UL_ConfigDedicated_v1020_present if
       * present */
                                                                              /* Need ON */
    SoundingRS_UL_ConfigDedicatedAperiodic_r10 soundingRS_UL_ConfigDedicatedAperiodic_r10;                              /* extension #2; optional; set in
                                   * bit_mask
  * PhysicalConfigDedicated_soundingRS_UL_ConfigDedicatedAperiodic_r10_present if
  * present */
                                                                                            /* Need ON */
    UplinkPowerControlDedicated_v1020 uplinkPowerControlDedicated_v1020;  
                                        /* extension #2; optional; set in
                                   * bit_mask
                                   * uplinkPowerControlDedicated_v1020_present
                                   * if present */
                                                                          /* Need ON */
    struct {
        unsigned short  choice;
#           define      additionalSpectrumEmissionCA_r10_release_chosen 1
#           define      additionalSpectrumEmissionCA_r10_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                           * additionalSpectrumEmissionCA_r10_release_chosen */
            struct _seq45 {
                AdditionalSpectrumEmission additionalSpectrumEmissionPCell_r10;
            } setup;  /* to choose, set choice to
                       * additionalSpectrumEmissionCA_r10_setup_chosen */
        } u;
    } additionalSpectrumEmissionCA_r10;  /* extension #3; optional; set in
                                   * bit_mask
                                   * additionalSpectrumEmissionCA_r10_present if
                                   * present */
    /* Need ON */
      /* DL configuration as well as configuration applicable for DL and UL */
    struct CSI_RS_ConfigNZPToReleaseList_r11 *csi_RS_ConfigNZPToReleaseList_r11;                                        /* extension #4; optional; set in
                                   * bit_mask
         * PhysicalConfigDedicated_csi_RS_ConfigNZPToReleaseList_r11_present if
         * present */
                                                                                  /* Need ON */
    struct CSI_RS_ConfigNZPToAddModList_r11 *csi_RS_ConfigNZPToAddModList_r11;                                          /* extension #4; optional; set in
                                   * bit_mask
          * PhysicalConfigDedicated_csi_RS_ConfigNZPToAddModList_r11_present if
          * present */
                                                                                /* Need ON */
    struct CSI_RS_ConfigZPToReleaseList_r11 *csi_RS_ConfigZPToReleaseList_r11;                                          /* extension #4; optional; set in
                                   * bit_mask
          * PhysicalConfigDedicated_csi_RS_ConfigZPToReleaseList_r11_present if
          * present */
                                                                                /* Need ON */
    struct CSI_RS_ConfigZPToAddModList_r11 *csi_RS_ConfigZPToAddModList_r11;  
                                        /* extension #4; optional; set in
                                   * bit_mask
           * PhysicalConfigDedicated_csi_RS_ConfigZPToAddModList_r11_present if
           * present */
                                                                              /* Need ON */
    EPDCCH_Config_r11 epdcch_Config_r11;  /* extension #4; optional; set in
                                   * bit_mask
                         * PhysicalConfigDedicated_epdcch_Config_r11_present if
                         * present */
                                          /* Need ON */
    PDSCH_ConfigDedicated_v1130 pdsch_ConfigDedicated_v1130;  /* extension #4;
                                   * optional; set in bit_mask
               * PhysicalConfigDedicated_pdsch_ConfigDedicated_v1130_present if
               * present */
                                                              /* Need ON */
	/* UL configuration */
    CQI_ReportConfig_v1130 cqi_ReportConfig_v1130;  /* extension #4; optional;
                                   * set in bit_mask
                    * PhysicalConfigDedicated_cqi_ReportConfig_v1130_present if
                    * present */
                                                    /* Need ON */
    PUCCH_ConfigDedicated_v1130 pucch_ConfigDedicated_v1130;  /* extension #4;
                                   * optional; set in bit_mask
                                   * pucch_ConfigDedicated_v1130_present if
                                   * present */
                                                              /* Need ON */
    PUSCH_ConfigDedicated_v1130 pusch_ConfigDedicated_v1130;  /* extension #4;
                                   * optional; set in bit_mask
               * PhysicalConfigDedicated_pusch_ConfigDedicated_v1130_present if
               * present */
                                                              /* Need ON */
    UplinkPowerControlDedicated_v1130 uplinkPowerControlDedicated_v1130;  
                                        /* extension #4; optional; set in
                                   * bit_mask
                                   * uplinkPowerControlDedicated_v1130_present
                                   * if present */
                                                                          /* Need ON */
    AntennaInfoDedicated_v1250 antennaInfo_v1250;  /* extension #5; optional;
                                   * set in bit_mask
                         * PhysicalConfigDedicated_antennaInfo_v1250_present if
                         * present */
                                                   /* Cond AI-r10 */
    EIMTA_MainConfig_r12 eimta_MainConfig_r12;  /* extension #5; optional; set
                                   * in bit_mask eimta_MainConfig_r12_present if
                                   * present */
                                                /* Need ON */
    EIMTA_MainConfigServCell_r12 eimta_MainConfigPCell_r12;  /* extension #5;
                                   * optional; set in bit_mask
                                   * eimta_MainConfigPCell_r12_present if
                                   * present */
                                                             /* Need ON */
    PUCCH_ConfigDedicated_v1250 pucch_ConfigDedicated_v1250;  /* extension #5;
                                   * optional; set in bit_mask
                                   * pucch_ConfigDedicated_v1250_present if
                                   * present */
                                                              /* Need ON */
    CQI_ReportConfig_v1250 cqi_ReportConfigPCell_v1250;  /* extension #5;
                                   * optional; set in bit_mask
                                   * cqi_ReportConfigPCell_v1250_present if
                                   * present */
                                                         /* Need ON */
    UplinkPowerControlDedicated_v1250 uplinkPowerControlDedicated_v1250;  
                                        /* extension #5; optional; set in
                                   * bit_mask
                                   * uplinkPowerControlDedicated_v1250_present
                                   * if present */
                                                                          /* Need ON */
    PUSCH_ConfigDedicated_v1250 pusch_ConfigDedicated_v1250;  /* extension #5;
                                   * optional; set in bit_mask
                                   * pusch_ConfigDedicated_v1250_present if
                                   * present */
                                                              /* Need ON */
    CSI_RS_Config_v1250 csi_RS_Config_v1250;  /* extension #5; optional; set in
                                   * bit_mask
                       * PhysicalConfigDedicated_csi_RS_Config_v1250_present if
                       * present */
                                              /* Need ON */
} PhysicalConfigDedicated;

typedef struct RLF_TimersAndConstants_r9 {
    unsigned short  choice;
#       define      RLF_TimersAndConstants_r9_release_chosen 1
#       define      RLF_TimersAndConstants_r9_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                  * RLF_TimersAndConstants_r9_release_chosen */
        struct _seq46 {
            _enum7          t301_r9;
            _enum8          t310_r9;
            _enum9          n310_r9;
            _enum10         t311_r9;
            _enum11         n311_r9;
        } setup;  /* to choose, set choice to
                   * RLF_TimersAndConstants_r9_setup_chosen */
    } u;
} RLF_TimersAndConstants_r9;

typedef struct MeasSubframePatternPCell_r10 {
    unsigned short  choice;
#       define      MeasSubframePatternPCell_r10_release_chosen 1
#       define      MeasSubframePatternPCell_r10_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                               * MeasSubframePatternPCell_r10_release_chosen */
        MeasSubframePattern_r10 setup;  /* to choose, set choice to
                                 * MeasSubframePatternPCell_r10_setup_chosen */
    } u;
} MeasSubframePatternPCell_r10;

typedef struct NeighCellsCRS_Info_r11 {
    unsigned short  choice;
#       define      NeighCellsCRS_Info_r11_release_chosen 1
#       define      NeighCellsCRS_Info_r11_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * NeighCellsCRS_Info_r11_release_chosen */
        struct CRS_AssistanceInfoList_r11 *setup;  /* to choose, set choice to
                                       * NeighCellsCRS_Info_r11_setup_chosen */
    } u;
} NeighCellsCRS_Info_r11;

typedef enum P_a {
    P_a_dB_6 = 0,
    P_a_dB_4dot77 = 1,
    P_a_dB_3 = 2,
    P_a_dB_1dot77 = 3,
    P_a_dB0 = 4,
    P_a_dB1 = 5,
    P_a_dB2 = 6,
    P_a_dB3 = 7
} P_a;

typedef struct NAICS_AssistanceInfo_r12 {
    unsigned short  choice;
#       define      NAICS_AssistanceInfo_r12_release_chosen 1
#       define      NAICS_AssistanceInfo_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * NAICS_AssistanceInfo_r12_release_chosen */
        struct _seq47 {
            unsigned char   bit_mask;
#               define      neighCellsToReleaseList_r12_present 0x80
#               define      neighCellsToAddModList_r12_present 0x40
#               define      servCellp_a_r12_present 0x20
            struct NeighCellsToReleaseList_r12 *neighCellsToReleaseList_r12;  
                                        /* optional; set in bit_mask
                                         * neighCellsToReleaseList_r12_present
                                         * if present */
                                                                              /* Need ON */
            struct NeighCellsToAddModList_r12 *neighCellsToAddModList_r12;  
                                        /* optional; set in bit_mask
                                         * neighCellsToAddModList_r12_present if
                                         * present */
                                                                            /* Need ON */
            P_a             servCellp_a_r12;  /* optional; set in bit_mask
                                               * servCellp_a_r12_present if
                                               * present */
                                              /* Need ON */
        } setup;  /* to choose, set choice to
                   * NAICS_AssistanceInfo_r12_setup_chosen */
    } u;
} NAICS_AssistanceInfo_r12;

typedef struct RadioResourceConfigDedicated {
    unsigned short  bit_mask;
#       define      srb_ToAddModList_present 0x8000
#       define      drb_ToAddModList_present 0x4000
#       define      drb_ToReleaseList_present 0x2000
#       define      mac_MainConfig_present 0x1000
#       define      sps_Config_present 0x0800
#       define      physicalConfigDedicated_present 0x0400
#       define      rlf_TimersAndConstants_r9_present 0x0200
#       define      measSubframePatternPCell_r10_present 0x0100
#       define      neighCellsCRS_Info_r11_present 0x0080
#       define      RadioResourceConfigDedicated_naics_Info_r12_present 0x0040
    struct SRB_ToAddModList *srb_ToAddModList;  /* optional; set in bit_mask
                                                 * srb_ToAddModList_present if
                                                 * present */
                                                /* Cond HO-Conn */
    struct DRB_ToAddModList *drb_ToAddModList;  /* optional; set in bit_mask
                                                 * drb_ToAddModList_present if
                                                 * present */
                                                /* Cond HO-toEUTRA */
    struct DRB_ToReleaseList *drb_ToReleaseList;  /* optional; set in bit_mask
                                                   * drb_ToReleaseList_present
                                                   * if present */
                                                  /* Need ON */
    struct {
        unsigned short  choice;
#           define      mac_MainConfig_explicitValue_chosen 1
#           define      mac_MainConfig_defaultValue_chosen 2
        union {
            MAC_MainConfig  explicitValue;  /* to choose, set choice to
                                       * mac_MainConfig_explicitValue_chosen */
            Nulltype        defaultValue;  /* to choose, set choice to
                                        * mac_MainConfig_defaultValue_chosen */
        } u;
    } mac_MainConfig;  /* optional; set in bit_mask mac_MainConfig_present if
                        * present */
                                                                                                                               /* Cond HO-toEUTRA2 */
    SPS_Config      sps_Config;  /* optional; set in bit_mask sps_Config_present
                                  * if present */
                                 /* Need ON */
    PhysicalConfigDedicated physicalConfigDedicated;  /* optional; set in
                                   * bit_mask physicalConfigDedicated_present if
                                   * present */
                                                      /* Need ON */
    RLF_TimersAndConstants_r9 rlf_TimersAndConstants_r9;  /* extension #1;
                                   * optional; set in bit_mask
                                   * rlf_TimersAndConstants_r9_present if
                                   * present */
                                                          /* Need ON */
    MeasSubframePatternPCell_r10 measSubframePatternPCell_r10;  /* extension #2;
                                   * optional; set in bit_mask
                                   * measSubframePatternPCell_r10_present if
                                   * present */
                                                                /* Need ON */
    NeighCellsCRS_Info_r11 neighCellsCRS_Info_r11;  /* extension #3; optional;
                                   * set in bit_mask
                                   * neighCellsCRS_Info_r11_present if
                                   * present */
                                                    /* Need ON */
    NAICS_AssistanceInfo_r12 naics_Info_r12;  /* extension #4; optional; set in
                                   * bit_mask
                       * RadioResourceConfigDedicated_naics_Info_r12_present if
                       * present */
                                              /* Need ON */
} RadioResourceConfigDedicated;

typedef unsigned short  NextHopChainingCount;

typedef struct RRCConnectionReestablishment_v8a0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishment_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionReestablishment_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
    * RRCConnectionReestablishment_v8a0_IEs_lateNonCriticalExtension_present if
    * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
        * RRCConnectionReestablishment_v8a0_IEs_nonCriticalExtension_present if
        * present */
} RRCConnectionReestablishment_v8a0_IEs;

typedef struct RRCConnectionReestablishment_r8_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishment_r8_IEs_nonCriticalExtension_present 0x80
    RadioResourceConfigDedicated radioResourceConfigDedicated;
    NextHopChainingCount nextHopChainingCount;
    RRCConnectionReestablishment_v8a0_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
          * RRCConnectionReestablishment_r8_IEs_nonCriticalExtension_present if
          * present */
} RRCConnectionReestablishment_r8_IEs;

typedef struct RRCConnectionReestablishment {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      RRCConnectionReestablishment_criticalExtensions_c1_chosen 1
#           define      RRCConnectionReestablishment_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice8 {
                unsigned short  choice;
#                   define      rrcConnectionReestablishment_r8_chosen 1
#                   define      RRCConnectionReestablishment_criticalExtensions_c1_spare7_chosen 2
#                   define      RRCConnectionReestablishment_criticalExtensions_c1_spare6_chosen 3
#                   define      RRCConnectionReestablishment_criticalExtensions_c1_spare5_chosen 4
#                   define      RRCConnectionReestablishment_criticalExtensions_c1_spare4_chosen 5
#                   define      RRCConnectionReestablishment_criticalExtensions_c1_spare3_chosen 6
#                   define      RRCConnectionReestablishment_criticalExtensions_c1_spare2_chosen 7
#                   define      RRCConnectionReestablishment_criticalExtensions_c1_spare1_chosen 8
                union {
                    RRCConnectionReestablishment_r8_IEs rrcConnectionReestablishment_r8;                                /* to choose, set choice to
                                    * rrcConnectionReestablishment_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
          * RRCConnectionReestablishment_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
          * RRCConnectionReestablishment_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
          * RRCConnectionReestablishment_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
          * RRCConnectionReestablishment_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
          * RRCConnectionReestablishment_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
          * RRCConnectionReestablishment_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
          * RRCConnectionReestablishment_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                 * RRCConnectionReestablishment_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
       * RRCConnectionReestablishment_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionReestablishment;

typedef struct RRCConnectionReestablishmentReject_v8a0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishmentReject_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionReestablishmentReject_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
    * RRCConnectionReestablishmentReject_v8a0_IEs_lateNonCriticalExtension_present if
    * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
  * RRCConnectionReestablishmentReject_v8a0_IEs_nonCriticalExtension_present if
  * present */
} RRCConnectionReestablishmentReject_v8a0_IEs;

typedef struct RRCConnectionReestablishmentReject_r8_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishmentReject_r8_IEs_nonCriticalExtension_present 0x80
    RRCConnectionReestablishmentReject_v8a0_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
    * RRCConnectionReestablishmentReject_r8_IEs_nonCriticalExtension_present if
    * present */
} RRCConnectionReestablishmentReject_r8_IEs;

typedef struct RRCConnectionReestablishmentReject {
    struct {
        unsigned short  choice;
#           define      rrcConnectionReestablishmentReject_r8_chosen 1
#           define      RRCConnectionReestablishmentReject_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            RRCConnectionReestablishmentReject_r8_IEs rrcConnectionReestablishmentReject_r8;                            /* to choose, set choice to
                              * rrcConnectionReestablishmentReject_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
             * RRCConnectionReestablishmentReject_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionReestablishmentReject;

typedef struct RRCConnectionReject_v1130_IEs {
    unsigned char   bit_mask;
#       define      deprioritisationReq_r11_present 0x80
#       define      RRCConnectionReject_v1130_IEs_nonCriticalExtension_present 0x40
    struct {
        enum {
            frequency = 0,
            e_utra = 1
        } deprioritisationType_r11;
        enum {
            deprioritisationTimer_r11_min5 = 0,
            deprioritisationTimer_r11_min10 = 1,
            min15 = 2,
            deprioritisationTimer_r11_min30 = 3
        } deprioritisationTimer_r11;
    } deprioritisationReq_r11;  /* optional; set in bit_mask
                                 * deprioritisationReq_r11_present if present */
       /* Need ON */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * RRCConnectionReject_v1130_IEs_nonCriticalExtension_present if
                * present */
} RRCConnectionReject_v1130_IEs;

typedef struct RRCConnectionReject_v1020_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReject_v1020_IEs_extendedWaitTime_r10_present 0x80
#       define      RRCConnectionReject_v1020_IEs_nonCriticalExtension_present 0x40
    unsigned short  extendedWaitTime_r10;  /* optional; set in bit_mask
                * RRCConnectionReject_v1020_IEs_extendedWaitTime_r10_present if
                * present */
                                           /* Need ON */
    RRCConnectionReject_v1130_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * RRCConnectionReject_v1020_IEs_nonCriticalExtension_present if
                * present */
} RRCConnectionReject_v1020_IEs;

typedef struct RRCConnectionReject_v8a0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReject_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionReject_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
             * RRCConnectionReject_v8a0_IEs_lateNonCriticalExtension_present if
             * present */
    RRCConnectionReject_v1020_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * RRCConnectionReject_v8a0_IEs_nonCriticalExtension_present if
                 * present */
} RRCConnectionReject_v8a0_IEs;

typedef struct RRCConnectionReject_r8_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReject_r8_IEs_nonCriticalExtension_present 0x80
    unsigned short  waitTime;
    RRCConnectionReject_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                   * RRCConnectionReject_r8_IEs_nonCriticalExtension_present if
                   * present */
} RRCConnectionReject_r8_IEs;

typedef struct RRCConnectionReject {
    struct {
        unsigned short  choice;
#           define      RRCConnectionReject_criticalExtensions_c1_chosen 1
#           define      RRCConnectionReject_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice9 {
                unsigned short  choice;
#                   define      rrcConnectionReject_r8_chosen 1
#                   define      RRCConnectionReject_criticalExtensions_c1_spare3_chosen 2
#                   define      RRCConnectionReject_criticalExtensions_c1_spare2_chosen 3
#                   define      RRCConnectionReject_criticalExtensions_c1_spare1_chosen 4
                union {
                    RRCConnectionReject_r8_IEs rrcConnectionReject_r8;  /* to
                                   * choose, set choice to
                                   * rrcConnectionReject_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                   * RRCConnectionReject_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                   * RRCConnectionReject_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                   * RRCConnectionReject_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * RRCConnectionReject_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
    * RRCConnectionReject_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionReject;

typedef struct RRCConnectionSetup_v8a0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionSetup_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionSetup_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
              * RRCConnectionSetup_v8a0_IEs_lateNonCriticalExtension_present if
              * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                  * RRCConnectionSetup_v8a0_IEs_nonCriticalExtension_present if
                  * present */
} RRCConnectionSetup_v8a0_IEs;

typedef struct RRCConnectionSetup_r8_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionSetup_r8_IEs_nonCriticalExtension_present 0x80
    RadioResourceConfigDedicated radioResourceConfigDedicated;
    RRCConnectionSetup_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                    * RRCConnectionSetup_r8_IEs_nonCriticalExtension_present if
                    * present */
} RRCConnectionSetup_r8_IEs;

typedef struct RRCConnectionSetup {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      RRCConnectionSetup_criticalExtensions_c1_chosen 1
#           define      RRCConnectionSetup_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice10 {
                unsigned short  choice;
#                   define      rrcConnectionSetup_r8_chosen 1
#                   define      RRCConnectionSetup_criticalExtensions_c1_spare7_chosen 2
#                   define      RRCConnectionSetup_criticalExtensions_c1_spare6_chosen 3
#                   define      RRCConnectionSetup_criticalExtensions_c1_spare5_chosen 4
#                   define      RRCConnectionSetup_criticalExtensions_c1_spare4_chosen 5
#                   define      RRCConnectionSetup_criticalExtensions_c1_spare3_chosen 6
#                   define      RRCConnectionSetup_criticalExtensions_c1_spare2_chosen 7
#                   define      RRCConnectionSetup_criticalExtensions_c1_spare1_chosen 8
                union {
                    RRCConnectionSetup_r8_IEs rrcConnectionSetup_r8;  /* to
                                   * choose, set choice to
                                   * rrcConnectionSetup_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * RRCConnectionSetup_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
     * RRCConnectionSetup_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionSetup;

typedef struct DL_CCCH_MessageType {
    unsigned short  choice;
#       define      DL_CCCH_MessageType_c1_chosen 1
#       define      DL_CCCH_MessageType_messageClassExtension_chosen 2
    union {
        struct _choice11 {
            unsigned short  choice;
#               define      rrcConnectionReestablishment_chosen 1
#               define      rrcConnectionReestablishmentReject_chosen 2
#               define      rrcConnectionReject_chosen 3
#               define      rrcConnectionSetup_chosen 4
            union {
                RRCConnectionReestablishment rrcConnectionReestablishment;  
                                        /* to choose, set choice to
                                       * rrcConnectionReestablishment_chosen */
                RRCConnectionReestablishmentReject rrcConnectionReestablishmentReject;                                  /* to choose, set choice to
                                 * rrcConnectionReestablishmentReject_chosen */
                RRCConnectionReject rrcConnectionReject;  /* to choose, set
                                      * choice to rrcConnectionReject_chosen */
                RRCConnectionSetup rrcConnectionSetup;  /* to choose, set choice
                                              * to rrcConnectionSetup_chosen */
            } u;
        } c1;  /* to choose, set choice to DL_CCCH_MessageType_c1_chosen */
        _seq2           messageClassExtension;  /* to choose, set choice to
                          * DL_CCCH_MessageType_messageClassExtension_chosen */
    } u;
} DL_CCCH_MessageType;

typedef struct DL_CCCH_Message {
    DL_CCCH_MessageType message;
} DL_CCCH_Message;

typedef struct RAND_CDMA2000 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} RAND_CDMA2000;

typedef struct MobilityParametersCDMA2000 {
    unsigned int    length;
    unsigned char   *value;
} MobilityParametersCDMA2000;

typedef struct CSFBParametersResponseCDMA2000_v8a0_IEs {
    unsigned char   bit_mask;
#       define      CSFBParametersResponseCDMA2000_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      CSFBParametersResponseCDMA2000_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
  * CSFBParametersResponseCDMA2000_v8a0_IEs_lateNonCriticalExtension_present if
  * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
      * CSFBParametersResponseCDMA2000_v8a0_IEs_nonCriticalExtension_present if
      * present */
} CSFBParametersResponseCDMA2000_v8a0_IEs;

typedef struct CSFBParametersResponseCDMA2000_r8_IEs {
    unsigned char   bit_mask;
#       define      CSFBParametersResponseCDMA2000_r8_IEs_nonCriticalExtension_present 0x80
    RAND_CDMA2000   rand;
    MobilityParametersCDMA2000 mobilityParameters;
    CSFBParametersResponseCDMA2000_v8a0_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
        * CSFBParametersResponseCDMA2000_r8_IEs_nonCriticalExtension_present if
        * present */
} CSFBParametersResponseCDMA2000_r8_IEs;

typedef struct CSFBParametersResponseCDMA2000 {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      csfbParametersResponseCDMA2000_r8_chosen 1
#           define      CSFBParametersResponseCDMA2000_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            CSFBParametersResponseCDMA2000_r8_IEs csfbParametersResponseCDMA2000_r8;                                    /* to choose, set choice to
                                  * csfbParametersResponseCDMA2000_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
         * CSFBParametersResponseCDMA2000_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} CSFBParametersResponseCDMA2000;

typedef struct DedicatedInfoNAS {
    unsigned int    length;
    unsigned char   *value;
} DedicatedInfoNAS;

typedef struct DedicatedInfoCDMA2000 {
    unsigned int    length;
    unsigned char   *value;
} DedicatedInfoCDMA2000;

typedef struct DLInformationTransfer_v8a0_IEs {
    unsigned char   bit_mask;
#       define      DLInformationTransfer_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      DLInformationTransfer_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
           * DLInformationTransfer_v8a0_IEs_lateNonCriticalExtension_present if
           * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
               * DLInformationTransfer_v8a0_IEs_nonCriticalExtension_present if
               * present */
} DLInformationTransfer_v8a0_IEs;

typedef struct _choice12 {
    unsigned short  choice;
#       define      dedicatedInfoNAS_chosen 1
#       define      dedicatedInfoCDMA2000_1XRTT_chosen 2
#       define      dedicatedInfoCDMA2000_HRPD_chosen 3
    union {
        DedicatedInfoNAS dedicatedInfoNAS;  /* to choose, set choice to
                                             * dedicatedInfoNAS_chosen */
        DedicatedInfoCDMA2000 dedicatedInfoCDMA2000_1XRTT;  /* to choose, set
                                   * choice to
                                   * dedicatedInfoCDMA2000_1XRTT_chosen */
        DedicatedInfoCDMA2000 dedicatedInfoCDMA2000_HRPD;  /* to choose, set
                                   * choice to
                                   * dedicatedInfoCDMA2000_HRPD_chosen */
    } u;
} _choice12;

typedef struct DLInformationTransfer_r8_IEs {
    unsigned char   bit_mask;
#       define      DLInformationTransfer_r8_IEs_nonCriticalExtension_present 0x80
    _choice12       dedicatedInfoType;
    DLInformationTransfer_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * DLInformationTransfer_r8_IEs_nonCriticalExtension_present if
                 * present */
} DLInformationTransfer_r8_IEs;

typedef struct DLInformationTransfer {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      DLInformationTransfer_criticalExtensions_c1_chosen 1
#           define      DLInformationTransfer_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice13 {
                unsigned short  choice;
#                   define      dlInformationTransfer_r8_chosen 1
#                   define      DLInformationTransfer_criticalExtensions_c1_spare3_chosen 2
#                   define      DLInformationTransfer_criticalExtensions_c1_spare2_chosen 3
#                   define      DLInformationTransfer_criticalExtensions_c1_spare1_chosen 4
                union {
                    DLInformationTransfer_r8_IEs dlInformationTransfer_r8;  
                                        /* to choose, set choice to
                                         * dlInformationTransfer_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                 * DLInformationTransfer_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                 * DLInformationTransfer_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                 * DLInformationTransfer_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * DLInformationTransfer_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
  * DLInformationTransfer_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} DLInformationTransfer;

typedef enum CDMA2000_Type {
    type1XRTT = 0,
    typeHRPD = 1
} CDMA2000_Type;

typedef enum BandclassCDMA2000 {
    bc0 = 0,
    bc1 = 1,
    bc2 = 2,
    bc3 = 3,
    bc4 = 4,
    bc5 = 5,
    bc6 = 6,
    bc7 = 7,
    bc8 = 8,
    bc9 = 9,
    bc10 = 10,
    bc11 = 11,
    bc12 = 12,
    bc13 = 13,
    bc14 = 14,
    bc15 = 15,
    bc16 = 16,
    bc17 = 17,
    bc18_v9a0 = 18,
    bc19_v9a0 = 19,
    bc20_v9a0 = 20,
    bc21_v9a0 = 21,
    BandclassCDMA2000_spare10 = 22,
    BandclassCDMA2000_spare9 = 23,
    BandclassCDMA2000_spare8 = 24,
    BandclassCDMA2000_spare7 = 25,
    BandclassCDMA2000_spare6 = 26,
    BandclassCDMA2000_spare5 = 27,
    BandclassCDMA2000_spare4 = 28,
    BandclassCDMA2000_spare3 = 29,
    BandclassCDMA2000_spare2 = 30,
    BandclassCDMA2000_spare1 = 31
} BandclassCDMA2000;

typedef unsigned short  ARFCN_ValueCDMA2000;

typedef struct CarrierFreqCDMA2000 {
    BandclassCDMA2000 bandClass;
    ARFCN_ValueCDMA2000 arfcn;
} CarrierFreqCDMA2000;

typedef struct HandoverFromEUTRAPreparationRequest_v1020_IEs {
    unsigned char   bit_mask;
#       define      dualRxTxRedirectIndicator_r10_present 0x80
#       define      redirectCarrierCDMA2000_1XRTT_r10_present 0x40
#       define      HandoverFromEUTRAPreparationRequest_v1020_IEs_nonCriticalExtension_present 0x20
    _enum3          dualRxTxRedirectIndicator_r10;  /* optional; set in bit_mask
                                     * dualRxTxRedirectIndicator_r10_present if
                                     * present */
       /* Cond cdma2000-1XRTT */
    CarrierFreqCDMA2000 redirectCarrierCDMA2000_1XRTT_r10;  /* optional; set in
                                   * bit_mask
                                   * redirectCarrierCDMA2000_1XRTT_r10_present
                                   * if present */
                                                            /* Cond dualRxTxRedirect */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
  * HandoverFromEUTRAPreparationRequest_v1020_IEs_nonCriticalExtension_present if
  * present */
} HandoverFromEUTRAPreparationRequest_v1020_IEs;

typedef struct HandoverFromEUTRAPreparationRequest_v920_IEs {
    unsigned char   bit_mask;
#       define      concurrPrepCDMA2000_HRPD_r9_present 0x80
#       define      HandoverFromEUTRAPreparationRequest_v920_IEs_nonCriticalExtension_present 0x40
    ossBoolean      concurrPrepCDMA2000_HRPD_r9;  /* optional; set in bit_mask
                                       * concurrPrepCDMA2000_HRPD_r9_present if
                                       * present */
                                                  /* Cond cdma2000-Type */
    HandoverFromEUTRAPreparationRequest_v1020_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
 * HandoverFromEUTRAPreparationRequest_v920_IEs_nonCriticalExtension_present if
 * present */
} HandoverFromEUTRAPreparationRequest_v920_IEs;

typedef struct HandoverFromEUTRAPreparationRequest_v890_IEs {
    unsigned char   bit_mask;
#       define      HandoverFromEUTRAPreparationRequest_v890_IEs_lateNonCriticalExtension_present 0x80
#       define      HandoverFromEUTRAPreparationRequest_v890_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
     * HandoverFromEUTRAPreparationRequest_v890_IEs_lateNonCriticalExtension_present if
     * present */
    HandoverFromEUTRAPreparationRequest_v920_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
 * HandoverFromEUTRAPreparationRequest_v890_IEs_nonCriticalExtension_present if
 * present */
} HandoverFromEUTRAPreparationRequest_v890_IEs;

typedef struct HandoverFromEUTRAPreparationRequest_r8_IEs {
    unsigned char   bit_mask;
#       define      rand_present 0x80
#       define      mobilityParameters_present 0x40
#       define      HandoverFromEUTRAPreparationRequest_r8_IEs_nonCriticalExtension_present 0x20
    CDMA2000_Type   cdma2000_Type;
    RAND_CDMA2000   rand;  /* optional; set in bit_mask rand_present if
                            * present */
                           /* Cond cdma2000-Type */
    MobilityParametersCDMA2000 mobilityParameters;  /* optional; set in bit_mask
                                                * mobilityParameters_present if
                                                * present */
                                                    /* Cond cdma2000-Type */
    HandoverFromEUTRAPreparationRequest_v890_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
   * HandoverFromEUTRAPreparationRequest_r8_IEs_nonCriticalExtension_present if
   * present */
} HandoverFromEUTRAPreparationRequest_r8_IEs;

typedef struct HandoverFromEUTRAPreparationRequest {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_chosen 1
#           define      HandoverFromEUTRAPreparationRequest_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice14 {
                unsigned short  choice;
#                   define      handoverFromEUTRAPreparationRequest_r8_chosen 1
#                   define      HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_spare3_chosen 2
#                   define      HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_spare2_chosen 3
#                   define      HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_spare1_chosen 4
                union {
                    HandoverFromEUTRAPreparationRequest_r8_IEs handoverFromEUTRAPreparationRequest_r8;                  /* to choose, set choice to
                             * handoverFromEUTRAPreparationRequest_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
   * HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
   * HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
   * HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
          * HandoverFromEUTRAPreparationRequest_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
              * HandoverFromEUTRAPreparationRequest_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} HandoverFromEUTRAPreparationRequest;

typedef struct SI_OrPSI_GERAN {
    unsigned short  choice;
#       define      si_chosen 1
#       define      psi_chosen 2
    union {
        struct SystemInfoListGERAN *si;  /* to choose, set choice to
                                          * si_chosen */
        struct SystemInfoListGERAN *psi;  /* to choose, set choice to
                                           * psi_chosen */
    } u;
} SI_OrPSI_GERAN;

typedef struct Handover {
    unsigned char   bit_mask;
#       define      nas_SecurityParamFromEUTRA_present 0x80
#       define      Handover_systemInformation_present 0x40
    enum {
        targetRAT_Type_utra = 0,
        geran = 1,
        targetRAT_Type_cdma2000_1XRTT = 2,
        cdma2000_HRPD = 3,
        targetRAT_Type_spare4 = 4,
        targetRAT_Type_spare3 = 5,
        targetRAT_Type_spare2 = 6,
        targetRAT_Type_spare1 = 7
    } targetRAT_Type;
    _octet1         targetRAT_MessageContainer;
    _octet3         nas_SecurityParamFromEUTRA;  /* optional; set in bit_mask
                                        * nas_SecurityParamFromEUTRA_present if
                                        * present */
                                                 /* Cond UTRAGERAN */
    SI_OrPSI_GERAN  systemInformation;  /* optional; set in bit_mask
                                         * Handover_systemInformation_present if
                                         * present */
                                        /* Cond PSHO */
} Handover;

typedef struct PhysCellIdGERAN {
    _bit1           networkColourCode;
    _bit1           baseStationColourCode;
} PhysCellIdGERAN;

typedef unsigned short  ARFCN_ValueGERAN;

typedef enum BandIndicatorGERAN {
    dcs1800 = 0,
    pcs1900 = 1
} BandIndicatorGERAN;

typedef struct CarrierFreqGERAN {
    ARFCN_ValueGERAN arfcn;
    BandIndicatorGERAN bandIndicator;
} CarrierFreqGERAN;

typedef struct CellChangeOrder {
    enum {
        CellChangeOrder_t304_ms100 = 0,
        CellChangeOrder_t304_ms200 = 1,
        CellChangeOrder_t304_ms500 = 2,
        CellChangeOrder_t304_ms1000 = 3,
        CellChangeOrder_t304_ms2000 = 4,
        ms4000 = 5,
        ms8000 = 6,
        CellChangeOrder_t304_spare1 = 7
    } t304;
    struct {
        unsigned short  choice;
#           define      targetRAT_Type_geran_chosen 1
        union {
            struct _seq48 {
                unsigned char   bit_mask;
#                   define      networkControlOrder_present 0x80
#                   define      geran_systemInformation_present 0x40
                PhysCellIdGERAN physCellId;
                CarrierFreqGERAN carrierFreq;
                _bit1           networkControlOrder;  /* optional; set in
                                   * bit_mask networkControlOrder_present if
                                   * present */
                                                      /* Need OP */
                SI_OrPSI_GERAN  systemInformation;  /* optional; set in bit_mask
                                           * geran_systemInformation_present if
                                           * present */
                                                    /* Need OP */
            } geran;  /* to choose, set choice to targetRAT_Type_geran_chosen */
        } u;
    } targetRAT_Type;
} CellChangeOrder;

typedef struct MobilityFromEUTRACommand_v8d0_IEs {
    unsigned char   bit_mask;
#       define      MobilityFromEUTRACommand_v8d0_IEs_bandIndicator_present 0x80
#       define      MobilityFromEUTRACommand_v8d0_IEs_nonCriticalExtension_present 0x40
    BandIndicatorGERAN bandIndicator;  /* optional; set in bit_mask
                   * MobilityFromEUTRACommand_v8d0_IEs_bandIndicator_present if
                   * present */
                                       /* Cond GERAN */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
            * MobilityFromEUTRACommand_v8d0_IEs_nonCriticalExtension_present if
            * present */
} MobilityFromEUTRACommand_v8d0_IEs;

typedef struct MobilityFromEUTRACommand_v8a0_IEs {
    unsigned char   bit_mask;
#       define      MobilityFromEUTRACommand_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      MobilityFromEUTRACommand_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
        * MobilityFromEUTRACommand_v8a0_IEs_lateNonCriticalExtension_present if
        * present */
    MobilityFromEUTRACommand_v8d0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
            * MobilityFromEUTRACommand_v8a0_IEs_nonCriticalExtension_present if
            * present */
} MobilityFromEUTRACommand_v8a0_IEs;

typedef struct MobilityFromEUTRACommand_r8_IEs {
    unsigned char   bit_mask;
#       define      MobilityFromEUTRACommand_r8_IEs_nonCriticalExtension_present 0x80
    ossBoolean      cs_FallbackIndicator;
    struct {
        unsigned short  choice;
#           define      MobilityFromEUTRACommand_r8_IEs_purpose_handover_chosen 1
#           define      MobilityFromEUTRACommand_r8_IEs_purpose_cellChangeOrder_chosen 2
        union {
            Handover        handover;  /* to choose, set choice to
                   * MobilityFromEUTRACommand_r8_IEs_purpose_handover_chosen */
            CellChangeOrder cellChangeOrder;  /* to choose, set choice to
            * MobilityFromEUTRACommand_r8_IEs_purpose_cellChangeOrder_chosen */
        } u;
    } purpose;
    MobilityFromEUTRACommand_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
              * MobilityFromEUTRACommand_r8_IEs_nonCriticalExtension_present if
              * present */
} MobilityFromEUTRACommand_r8_IEs;

typedef struct E_CSFB_r9 {
    unsigned char   bit_mask;
#       define      messageContCDMA2000_1XRTT_r9_present 0x80
#       define      mobilityCDMA2000_HRPD_r9_present 0x40
#       define      messageContCDMA2000_HRPD_r9_present 0x20
#       define      redirectCarrierCDMA2000_HRPD_r9_present 0x10
    _octet1         messageContCDMA2000_1XRTT_r9;  /* optional; set in bit_mask
                                      * messageContCDMA2000_1XRTT_r9_present if
                                      * present */
                                                   /* Need ON */
    enum {
        handover = 0,
        redirection = 1
    } mobilityCDMA2000_HRPD_r9;  /* optional; set in bit_mask
                                  * mobilityCDMA2000_HRPD_r9_present if
                                  * present */
       /* Need OP */
    _octet1         messageContCDMA2000_HRPD_r9;  /* optional; set in bit_mask
                                       * messageContCDMA2000_HRPD_r9_present if
                                       * present */
                                                  /* Cond concHO */
    CarrierFreqCDMA2000 redirectCarrierCDMA2000_HRPD_r9;  /* optional; set in
                                   * bit_mask
                                   * redirectCarrierCDMA2000_HRPD_r9_present if
                                   * present */
                                                          /* Cond concRedir */
} E_CSFB_r9;

typedef struct MobilityFromEUTRACommand_v960_IEs {
    unsigned char   bit_mask;
#       define      MobilityFromEUTRACommand_v960_IEs_bandIndicator_present 0x80
#       define      MobilityFromEUTRACommand_v960_IEs_nonCriticalExtension_present 0x40
    BandIndicatorGERAN bandIndicator;  /* optional; set in bit_mask
                   * MobilityFromEUTRACommand_v960_IEs_bandIndicator_present if
                   * present */
                                       /* Cond GERAN */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
            * MobilityFromEUTRACommand_v960_IEs_nonCriticalExtension_present if
            * present */
} MobilityFromEUTRACommand_v960_IEs;

typedef struct MobilityFromEUTRACommand_v930_IEs {
    unsigned char   bit_mask;
#       define      MobilityFromEUTRACommand_v930_IEs_lateNonCriticalExtension_present 0x80
#       define      MobilityFromEUTRACommand_v930_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
        * MobilityFromEUTRACommand_v930_IEs_lateNonCriticalExtension_present if
        * present */
    MobilityFromEUTRACommand_v960_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
            * MobilityFromEUTRACommand_v930_IEs_nonCriticalExtension_present if
            * present */
} MobilityFromEUTRACommand_v930_IEs;

typedef struct MobilityFromEUTRACommand_r9_IEs {
    unsigned char   bit_mask;
#       define      MobilityFromEUTRACommand_r9_IEs_nonCriticalExtension_present 0x80
    ossBoolean      cs_FallbackIndicator;
    struct {
        unsigned short  choice;
#           define      MobilityFromEUTRACommand_r9_IEs_purpose_handover_chosen 1
#           define      MobilityFromEUTRACommand_r9_IEs_purpose_cellChangeOrder_chosen 2
#           define      e_CSFB_r9_chosen 3
        union {
            Handover        handover;  /* to choose, set choice to
                   * MobilityFromEUTRACommand_r9_IEs_purpose_handover_chosen */
            CellChangeOrder cellChangeOrder;  /* to choose, set choice to
            * MobilityFromEUTRACommand_r9_IEs_purpose_cellChangeOrder_chosen */
            E_CSFB_r9       e_CSFB_r9;  /* to choose, set choice to
                                         * e_CSFB_r9_chosen */
        } u;
    } purpose;
    MobilityFromEUTRACommand_v930_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
              * MobilityFromEUTRACommand_r9_IEs_nonCriticalExtension_present if
              * present */
} MobilityFromEUTRACommand_r9_IEs;

typedef struct MobilityFromEUTRACommand {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      MobilityFromEUTRACommand_criticalExtensions_c1_chosen 1
#           define      MobilityFromEUTRACommand_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice15 {
                unsigned short  choice;
#                   define      mobilityFromEUTRACommand_r8_chosen 1
#                   define      mobilityFromEUTRACommand_r9_chosen 2
#                   define      MobilityFromEUTRACommand_criticalExtensions_c1_spare2_chosen 3
#                   define      MobilityFromEUTRACommand_criticalExtensions_c1_spare1_chosen 4
                union {
                    MobilityFromEUTRACommand_r8_IEs mobilityFromEUTRACommand_r8;                                        /* to choose, set choice to
                                        * mobilityFromEUTRACommand_r8_chosen */
                    MobilityFromEUTRACommand_r9_IEs mobilityFromEUTRACommand_r9;                                        /* to choose, set choice to
                                        * mobilityFromEUTRACommand_r9_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
              * MobilityFromEUTRACommand_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
              * MobilityFromEUTRACommand_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * MobilityFromEUTRACommand_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * MobilityFromEUTRACommand_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} MobilityFromEUTRACommand;

typedef struct QuantityConfigEUTRA {
    unsigned char   bit_mask;
#       define      filterCoefficientRSRP_present 0x80
#       define      filterCoefficientRSRQ_present 0x40
    FilterCoefficient filterCoefficientRSRP;  /* filterCoefficientRSRP_present
                                   * not set in bit_mask implies value is fc4 */
    FilterCoefficient filterCoefficientRSRQ;  /* filterCoefficientRSRQ_present
                                   * not set in bit_mask implies value is fc4 */
} QuantityConfigEUTRA;

typedef struct QuantityConfigUTRA {
    unsigned char   bit_mask;
#       define      QuantityConfigUTRA_filterCoefficient_present 0x80
    enum {
        cpich_RSCP = 0,
        cpich_EcN0 = 1
    } measQuantityUTRA_FDD;
    enum {
        pccpch_RSCP = 0
    } measQuantityUTRA_TDD;
    FilterCoefficient filterCoefficient;  
                             /* QuantityConfigUTRA_filterCoefficient_present not
                              * set in bit_mask implies value is fc4 */
} QuantityConfigUTRA;

typedef struct QuantityConfigGERAN {
    unsigned char   bit_mask;
#       define      QuantityConfigGERAN_filterCoefficient_present 0x80
    enum {
        rssi = 0
    } measQuantityGERAN;
    FilterCoefficient filterCoefficient;  
                            /* QuantityConfigGERAN_filterCoefficient_present not
                             * set in bit_mask implies value is fc2 */
} QuantityConfigGERAN;

typedef struct QuantityConfigCDMA2000 {
    enum {
        pilotStrength = 0,
        pilotPnPhaseAndPilotStrength = 1
    } measQuantityCDMA2000;
} QuantityConfigCDMA2000;

typedef struct QuantityConfigUTRA_v1020 {
    unsigned char   bit_mask;
#       define      filterCoefficient2_FDD_r10_present 0x80
    FilterCoefficient filterCoefficient2_FDD_r10;  
                                  /* filterCoefficient2_FDD_r10_present not set
                                   * in bit_mask implies value is fc4 */
} QuantityConfigUTRA_v1020;

typedef struct QuantityConfigEUTRA_v1250 {
    unsigned char   bit_mask;
#       define      filterCoefficientCSI_RSRP_r12_present 0x80
    FilterCoefficient filterCoefficientCSI_RSRP_r12;  /* optional; set in
                                   * bit_mask
                                   * filterCoefficientCSI_RSRP_r12_present if
                                   * present */
                                                      /* Need OR */
} QuantityConfigEUTRA_v1250;

typedef struct QuantityConfig {
    unsigned char   bit_mask;
#       define      quantityConfigEUTRA_present 0x80
#       define      quantityConfigUTRA_present 0x40
#       define      quantityConfigGERAN_present 0x20
#       define      quantityConfigCDMA2000_present 0x10
#       define      quantityConfigUTRA_v1020_present 0x08
#       define      quantityConfigEUTRA_v1250_present 0x04
    QuantityConfigEUTRA quantityConfigEUTRA;  /* optional; set in bit_mask
                                               * quantityConfigEUTRA_present if
                                               * present */
                                              /* Need ON */
    QuantityConfigUTRA quantityConfigUTRA;  /* optional; set in bit_mask
                                             * quantityConfigUTRA_present if
                                             * present */
                                            /* Need ON */
    QuantityConfigGERAN quantityConfigGERAN;  /* optional; set in bit_mask
                                               * quantityConfigGERAN_present if
                                               * present */
                                              /* Need ON */
    QuantityConfigCDMA2000 quantityConfigCDMA2000;  /* optional; set in bit_mask
                                            * quantityConfigCDMA2000_present if
                                            * present */
                                                    /* Need ON */
    QuantityConfigUTRA_v1020 quantityConfigUTRA_v1020;  /* extension #1;
                                   * optional; set in bit_mask
                                   * quantityConfigUTRA_v1020_present if
                                   * present */
                                                        /* Need ON */
    QuantityConfigEUTRA_v1250 quantityConfigEUTRA_v1250;  /* extension #2;
                                   * optional; set in bit_mask
                                   * quantityConfigEUTRA_v1250_present if
                                   * present */
                                                          /* Need ON */
} QuantityConfig;

typedef struct MeasGapConfig {
    unsigned short  choice;
#       define      MeasGapConfig_release_chosen 1
#       define      MeasGapConfig_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * MeasGapConfig_release_chosen */
        struct _seq49 {
            struct {
                unsigned short  choice;
#                   define      gp0_chosen 1
#                   define      gp1_chosen 2
                union {
                    unsigned short  gp0;  /* to choose, set choice to
                                           * gp0_chosen */
                    unsigned short  gp1;  /* to choose, set choice to
                                           * gp1_chosen */
                } u;
            } gapOffset;
        } setup;  /* to choose, set choice to MeasGapConfig_setup_chosen */
    } u;
} MeasGapConfig;

typedef enum MeasScaleFactor_r12 {
    sf_EUTRA_cf1 = 0,
    sf_EUTRA_cf2 = 1
} MeasScaleFactor_r12;

typedef struct _seq50 {
    MobilityStateParameters mobilityStateParameters;
    SpeedStateScaleFactors timeToTrigger_SF;
} _seq50;

typedef struct _choice16 {
    unsigned short  choice;
#       define      speedStatePars_release_chosen 1
#       define      speedStatePars_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * speedStatePars_release_chosen */
        _seq50          setup;  /* to choose, set choice to
                                 * speedStatePars_setup_chosen */
    } u;
} _choice16;

typedef struct MeasConfig {
    unsigned int    bit_mask;
#       define      measObjectToRemoveList_present 0x80000000
#       define      measObjectToAddModList_present 0x40000000
#       define      reportConfigToRemoveList_present 0x20000000
#       define      reportConfigToAddModList_present 0x10000000
#       define      measIdToRemoveList_present 0x08000000
#       define      measIdToAddModList_present 0x04000000
#       define      MeasConfig_quantityConfig_present 0x02000000
#       define      measGapConfig_present 0x01000000
#       define      MeasConfig_s_Measure_present 0x00800000
#       define      preRegistrationInfoHRPD_present 0x00400000
#       define      MeasConfig_speedStatePars_present 0x00200000
#       define      measObjectToAddModList_v9e0_present 0x00100000
#       define      MeasConfig_allowInterruptions_r11_present 0x00080000
#       define      MeasConfig_measScaleFactor_r12_present 0x00040000
#       define      measIdToRemoveListExt_r12_present 0x00020000
#       define      measIdToAddModListExt_r12_present 0x00010000
#       define      measRSRQ_OnAllSymbols_r12_present 0x00008000
	/* Measurement objects */
    struct MeasObjectToRemoveList *measObjectToRemoveList;  /* optional; set in
                                   * bit_mask measObjectToRemoveList_present if
                                   * present */
                                                            /* Need ON */
    struct MeasObjectToAddModList *measObjectToAddModList;  /* optional; set in
                                   * bit_mask measObjectToAddModList_present if
                                   * present */
                                                            /* Need ON */
	/* Reporting configurations */
    struct ReportConfigToRemoveList *reportConfigToRemoveList;  /* optional; set
                                   * in bit_mask
                                   * reportConfigToRemoveList_present if
                                   * present */
                                                                /* Need ON */
    struct ReportConfigToAddModList *reportConfigToAddModList;  /* optional; set
                                   * in bit_mask
                                   * reportConfigToAddModList_present if
                                   * present */
                                                                /* Need ON */
	/* Measurement identities */
    struct MeasIdToRemoveList *measIdToRemoveList;  /* optional; set in bit_mask
                                                * measIdToRemoveList_present if
                                                * present */
                                                    /* Need ON */
    struct MeasIdToAddModList *measIdToAddModList;  /* optional; set in bit_mask
                                                * measIdToAddModList_present if
                                                * present */
                                                    /* Need ON */
	/* Other parameters */
    QuantityConfig  quantityConfig;  /* optional; set in bit_mask
                                      * MeasConfig_quantityConfig_present if
                                      * present */
                                     /* Need ON */
    MeasGapConfig   measGapConfig;  /* optional; set in bit_mask
                                     * measGapConfig_present if present */
                                    /* Need ON */
    RSRP_Range      s_Measure;  /* optional; set in bit_mask
                                 * MeasConfig_s_Measure_present if present */
                                /* Need ON */
    PreRegistrationInfoHRPD preRegistrationInfoHRPD;  /* optional; set in
                                   * bit_mask preRegistrationInfoHRPD_present if
                                   * present */
                                                      /* Need OP */
    _choice16       speedStatePars;  /* optional; set in bit_mask
                                      * MeasConfig_speedStatePars_present if
                                      * present */
       /* Need ON */
    struct MeasObjectToAddModList_v9e0 *measObjectToAddModList_v9e0;  
                                  /* extension #1; optional; set in bit_mask
                                   * measObjectToAddModList_v9e0_present if
                                   * present */
                                                                      /* Need ON */
    ossBoolean      allowInterruptions_r11;  /* extension #2; optional; set in
                                   * bit_mask
                                   * MeasConfig_allowInterruptions_r11_present
                                   * if present */
                                             /* Need ON */
    struct {
        unsigned short  choice;
#           define      measScaleFactor_r12_release_chosen 1
#           define      measScaleFactor_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * measScaleFactor_r12_release_chosen */
            MeasScaleFactor_r12 setup;  /* to choose, set choice to
                                         * measScaleFactor_r12_setup_chosen */
        } u;
    } measScaleFactor_r12;  /* extension #3; optional; set in bit_mask
                             * MeasConfig_measScaleFactor_r12_present if
                             * present */
       /* Need ON */
    struct MeasIdToRemoveListExt_r12 *measIdToRemoveListExt_r12;  /* extension
                                   * #3; optional; set in bit_mask
                                   * measIdToRemoveListExt_r12_present if
                                   * present */
                                                                  /* Need ON */
    struct MeasIdToAddModListExt_r12 *measIdToAddModListExt_r12;  /* extension
                                   * #3; optional; set in bit_mask
                                   * measIdToAddModListExt_r12_present if
                                   * present */
                                                                  /* Need ON */
    ossBoolean      measRSRQ_OnAllSymbols_r12;  /* extension #3; optional; set
                                   * in bit_mask
                                   * measRSRQ_OnAllSymbols_r12_present if
                                   * present */
                                                /* Need ON */
} MeasConfig;

typedef struct CarrierFreqEUTRA {
    unsigned char   bit_mask;
#       define      CarrierFreqEUTRA_ul_CarrierFreq_present 0x80
    ARFCN_ValueEUTRA dl_CarrierFreq;
    ARFCN_ValueEUTRA ul_CarrierFreq;  /* optional; set in bit_mask
                                       * CarrierFreqEUTRA_ul_CarrierFreq_present
                                       * if present */
                                      /* Cond FDD */
} CarrierFreqEUTRA;

typedef enum _enum27 {
    CarrierBandwidthEUTRA_dl_Bandwidth_n6 = 0,
    CarrierBandwidthEUTRA_dl_Bandwidth_n15 = 1,
    CarrierBandwidthEUTRA_dl_Bandwidth_n25 = 2,
    CarrierBandwidthEUTRA_dl_Bandwidth_n50 = 3,
    CarrierBandwidthEUTRA_dl_Bandwidth_n75 = 4,
    CarrierBandwidthEUTRA_dl_Bandwidth_n100 = 5,
    dl_Bandwidth_spare10 = 6,
    dl_Bandwidth_spare9 = 7,
    dl_Bandwidth_spare8 = 8,
    dl_Bandwidth_spare7 = 9,
    dl_Bandwidth_spare6 = 10,
    dl_Bandwidth_spare5 = 11,
    dl_Bandwidth_spare4 = 12,
    dl_Bandwidth_spare3 = 13,
    dl_Bandwidth_spare2 = 14,
    dl_Bandwidth_spare1 = 15
} _enum27;

typedef struct CarrierBandwidthEUTRA {
    unsigned char   bit_mask;
#       define      CarrierBandwidthEUTRA_ul_Bandwidth_present 0x80
    _enum27         dl_Bandwidth;
    _enum27         ul_Bandwidth;  /* optional; set in bit_mask
                                    * CarrierBandwidthEUTRA_ul_Bandwidth_present
                                    * if present */
                                   /* Need OP */
} CarrierBandwidthEUTRA;

typedef struct PRACH_Config {
    unsigned char   bit_mask;
#       define      prach_ConfigInfo_present 0x80
    unsigned short  rootSequenceIndex;
    PRACH_ConfigInfo prach_ConfigInfo;  /* optional; set in bit_mask
                                         * prach_ConfigInfo_present if
                                         * present */
                                        /* Need ON */
} PRACH_Config;

typedef enum _enum28 {
    antennaPortsCount_an1 = 0,
    antennaPortsCount_an2 = 1,
    antennaPortsCount_an4 = 2,
    antennaPortsCount_spare1 = 3
} _enum28;

typedef struct AntennaInfoCommon {
    _enum28         antennaPortsCount;
} AntennaInfoCommon;

typedef struct RadioResourceConfigCommon {
    unsigned short  bit_mask;
#       define      rach_ConfigCommon_present 0x8000
#       define      pdsch_ConfigCommon_present 0x4000
#       define      phich_Config_present 0x2000
#       define      pucch_ConfigCommon_present 0x1000
#       define      soundingRS_UL_ConfigCommon_present 0x0800
#       define      uplinkPowerControlCommon_present 0x0400
#       define      antennaInfoCommon_present 0x0200
#       define      RadioResourceConfigCommon_p_Max_present 0x0100
#       define      RadioResourceConfigCommon_tdd_Config_present 0x0080
#       define      RadioResourceConfigCommon_uplinkPowerControlCommon_v1020_present 0x0040
#       define      RadioResourceConfigCommon_tdd_Config_v1130_present 0x0020
#       define      RadioResourceConfigCommon_pusch_ConfigCommon_v1270_present 0x0010
    RACH_ConfigCommon rach_ConfigCommon;  /* optional; set in bit_mask
                                           * rach_ConfigCommon_present if
                                           * present */
                                          /* Need ON */
    PRACH_Config    prach_Config;
    PDSCH_ConfigCommon pdsch_ConfigCommon;  /* optional; set in bit_mask
                                             * pdsch_ConfigCommon_present if
                                             * present */
                                            /* Need ON */
    PUSCH_ConfigCommon pusch_ConfigCommon;
    PHICH_Config    phich_Config;  /* optional; set in bit_mask
                                    * phich_Config_present if present */
                                   /* Need ON */
    PUCCH_ConfigCommon pucch_ConfigCommon;  /* optional; set in bit_mask
                                             * pucch_ConfigCommon_present if
                                             * present */
                                            /* Need ON */
    SoundingRS_UL_ConfigCommon soundingRS_UL_ConfigCommon;  /* optional; set in
                                   * bit_mask soundingRS_UL_ConfigCommon_present
                                   * if present */
                                                            /* Need ON */
    UplinkPowerControlCommon uplinkPowerControlCommon;  /* optional; set in
                                   * bit_mask uplinkPowerControlCommon_present
                                   * if present */
                                                        /* Need ON */
    AntennaInfoCommon antennaInfoCommon;  /* optional; set in bit_mask
                                           * antennaInfoCommon_present if
                                           * present */
                                          /* Need ON */
    P_Max           p_Max;  /* optional; set in bit_mask
                             * RadioResourceConfigCommon_p_Max_present if
                             * present */
                            /* Need OP */
    TDD_Config      tdd_Config;  /* optional; set in bit_mask
                                  * RadioResourceConfigCommon_tdd_Config_present
                                  * if present */
                                 /* Cond TDD */
    UL_CyclicPrefixLength ul_CyclicPrefixLength;
    UplinkPowerControlCommon_v1020 uplinkPowerControlCommon_v1020;  
                                  /* extension #1; optional; set in bit_mask
          * RadioResourceConfigCommon_uplinkPowerControlCommon_v1020_present if
          * present */
                                                                    /* Need ON */
    TDD_Config_v1130 tdd_Config_v1130;  /* extension #2; optional; set in
                                   * bit_mask
                        * RadioResourceConfigCommon_tdd_Config_v1130_present if
                        * present */
                                        /* Cond TDD3 */
    PUSCH_ConfigCommon_v1270 pusch_ConfigCommon_v1270;  /* extension #3;
                                   * optional; set in bit_mask
                * RadioResourceConfigCommon_pusch_ConfigCommon_v1270_present if
                * present */
                                                        /* Need OR */
} RadioResourceConfigCommon;

typedef struct RACH_ConfigDedicated {
    unsigned short  ra_PreambleIndex;
    unsigned short  ra_PRACH_MaskIndex;
} RACH_ConfigDedicated;

typedef unsigned int    ARFCN_ValueEUTRA_r9;

typedef struct CarrierFreqEUTRA_v9e0 {
    unsigned char   bit_mask;
#       define      CarrierFreqEUTRA_v9e0_ul_CarrierFreq_v9e0_present 0x80
    ARFCN_ValueEUTRA_r9 dl_CarrierFreq_v9e0;
    ARFCN_ValueEUTRA_r9 ul_CarrierFreq_v9e0;  /* optional; set in bit_mask
                         * CarrierFreqEUTRA_v9e0_ul_CarrierFreq_v9e0_present if
                         * present */
                                              /* Cond FDD */
} CarrierFreqEUTRA_v9e0;

typedef enum _enum29 {
    t304_ms50 = 0,
    MobilityControlInfo_t304_ms100 = 1,
    t304_ms150 = 2,
    MobilityControlInfo_t304_ms200 = 3,
    MobilityControlInfo_t304_ms500 = 4,
    MobilityControlInfo_t304_ms1000 = 5,
    MobilityControlInfo_t304_ms2000 = 6,
    MobilityControlInfo_t304_spare1 = 7
} _enum29;

typedef struct MobilityControlInfo {
    unsigned char   bit_mask;
#       define      carrierFreq_present 0x80
#       define      carrierBandwidth_present 0x40
#       define      additionalSpectrumEmission_present 0x20
#       define      rach_ConfigDedicated_present 0x10
#       define      MobilityControlInfo_carrierFreq_v9e0_present 0x08
#       define      drb_ContinueROHC_r11_present 0x04
    PhysCellId      targetPhysCellId;
    CarrierFreqEUTRA carrierFreq;  /* optional; set in bit_mask
                                    * carrierFreq_present if present */
                                   /* Cond HO-toEUTRA2 */
    CarrierBandwidthEUTRA carrierBandwidth;  /* optional; set in bit_mask
                                              * carrierBandwidth_present if
                                              * present */
                                             /* Cond HO-toEUTRA */
    AdditionalSpectrumEmission additionalSpectrumEmission;  /* optional; set in
                                   * bit_mask additionalSpectrumEmission_present
                                   * if present */
                                                            /* Cond HO-toEUTRA */
    _enum29         t304;
    C_RNTI          newUE_Identity;
    RadioResourceConfigCommon radioResourceConfigCommon;
    RACH_ConfigDedicated rach_ConfigDedicated;  /* optional; set in bit_mask
                                                 * rach_ConfigDedicated_present
                                                 * if present */
                                                /* Need OP */
    CarrierFreqEUTRA_v9e0 carrierFreq_v9e0;  /* extension #1; optional; set in
                                   * bit_mask
                              * MobilityControlInfo_carrierFreq_v9e0_present if
                              * present */
                                             /* Need ON */
    _enum3          drb_ContinueROHC_r11;  /* extension #2; optional; set in
                                   * bit_mask drb_ContinueROHC_r11_present if
                                   * present */
                                           /* Cond HO */
} MobilityControlInfo;

typedef enum CipheringAlgorithm_r12 {
    eea0 = 0,
    eea1 = 1,
    eea2 = 2,
    eea3_v1130 = 3,
    CipheringAlgorithm_r12_spare4 = 4,
    CipheringAlgorithm_r12_spare3 = 5,
    CipheringAlgorithm_r12_spare2 = 6,
    CipheringAlgorithm_r12_spare1 = 7
} CipheringAlgorithm_r12;

typedef struct SecurityAlgorithmConfig {
    CipheringAlgorithm_r12 cipheringAlgorithm;
    enum {
        eia0_v920 = 0,
        eia1 = 1,
        eia2 = 2,
        eia3_v1130 = 3,
        integrityProtAlgorithm_spare4 = 4,
        integrityProtAlgorithm_spare3 = 5,
        integrityProtAlgorithm_spare2 = 6,
        integrityProtAlgorithm_spare1 = 7
    } integrityProtAlgorithm;
} SecurityAlgorithmConfig;

typedef struct _octet4 {
    unsigned short  length;
    unsigned char   value[6];
} _octet4;

typedef struct SecurityConfigHO {
    struct {
        unsigned short  choice;
#           define      intraLTE_chosen 1
#           define      interRAT_chosen 2
        union {
            struct _seq51 {
                unsigned char   bit_mask;
#                   define      securityAlgorithmConfig_present 0x80
                SecurityAlgorithmConfig securityAlgorithmConfig;  /* optional;
                                   * set in bit_mask
                                   * securityAlgorithmConfig_present if
                                   * present */
                                                                  /* Cond fullConfig */
                ossBoolean      keyChangeIndicator;
                NextHopChainingCount nextHopChainingCount;
            } intraLTE;  /* to choose, set choice to intraLTE_chosen */
            struct _seq52 {
                SecurityAlgorithmConfig securityAlgorithmConfig;
                _octet4         nas_SecurityParamToEUTRA;
            } interRAT;  /* to choose, set choice to interRAT_chosen */
        } u;
    } handoverType;
} SecurityConfigHO;

typedef struct AntennaInfoDedicated_v10i0 {
    unsigned char   bit_mask;
#       define      maxLayersMIMO_r10_present 0x80
    enum {
        maxLayersMIMO_r10_twoLayers = 0,
        maxLayersMIMO_r10_fourLayers = 1,
        maxLayersMIMO_r10_eightLayers = 2
    } maxLayersMIMO_r10;  /* optional; set in bit_mask maxLayersMIMO_r10_present
                           * if present */
                          /* Need OR */
} AntennaInfoDedicated_v10i0;

typedef struct RRCConnectionReconfiguration_v10i0_IEs {
    unsigned char   bit_mask;
#       define      antennaInfoDedicatedPCell_v10i0_present 0x80
#       define      RRCConnectionReconfiguration_v10i0_IEs_nonCriticalExtension_present 0x40
    AntennaInfoDedicated_v10i0 antennaInfoDedicatedPCell_v10i0;  /* optional;
                                   * set in bit_mask
                                   * antennaInfoDedicatedPCell_v10i0_present if
                                   * present */
                                                                 /* Need ON */
	/* Following field is only for late non-critical extensions from REL-10 */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
       * RRCConnectionReconfiguration_v10i0_IEs_nonCriticalExtension_present if
       * present */
} RRCConnectionReconfiguration_v10i0_IEs;

/* Late non-critical extensions: */
typedef struct RRCConnectionReconfiguration_v8m0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReconfiguration_v8m0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionReconfiguration_v8m0_IEs_nonCriticalExtension_present 0x40
	/* Following field is only for pre REL-10 late non-critical extensions */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
    * RRCConnectionReconfiguration_v8m0_IEs_lateNonCriticalExtension_present if
    * present */
    RRCConnectionReconfiguration_v10i0_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
        * RRCConnectionReconfiguration_v8m0_IEs_nonCriticalExtension_present if
        * present */
} RRCConnectionReconfiguration_v8m0_IEs;

typedef enum _enum30 {
    enabled = 0
} _enum30;

typedef struct ReportProximityConfig_r9 {
    unsigned char   bit_mask;
#       define      proximityIndicationEUTRA_r9_present 0x80
#       define      proximityIndicationUTRA_r9_present 0x40
    _enum30         proximityIndicationEUTRA_r9;  /* optional; set in bit_mask
                                       * proximityIndicationEUTRA_r9_present if
                                       * present */
               /* Need OR */
    _enum30         proximityIndicationUTRA_r9;  /* optional; set in bit_mask
                                        * proximityIndicationUTRA_r9_present if
                                        * present */
                                                 /* Need OR */
} ReportProximityConfig_r9;

typedef struct IDC_Config_r11 {
    unsigned char   bit_mask;
#       define      IDC_Config_r11_idc_Indication_r11_present 0x80
#       define      autonomousDenialParameters_r11_present 0x40
#       define      idc_Indication_UL_CA_r11_present 0x20
    _enum17         idc_Indication_r11;  /* optional; set in bit_mask
                                 * IDC_Config_r11_idc_Indication_r11_present if
                                 * present */
               /* Need OR */
    struct {
        enum {
            autonomousDenialSubframes_r11_n2 = 0,
            autonomousDenialSubframes_r11_n5 = 1,
            autonomousDenialSubframes_r11_n10 = 2,
            autonomousDenialSubframes_r11_n15 = 3,
            autonomousDenialSubframes_r11_n20 = 4,
            n30 = 5,
            autonomousDenialSubframes_r11_spare2 = 6,
            autonomousDenialSubframes_r11_spare1 = 7
        } autonomousDenialSubframes_r11;
        enum {
            autonomousDenialValidity_r11_sf200 = 0,
            autonomousDenialValidity_r11_sf500 = 1,
            autonomousDenialValidity_r11_sf1000 = 2,
            sf2000 = 3,
            autonomousDenialValidity_r11_spare4 = 4,
            autonomousDenialValidity_r11_spare3 = 5,
            autonomousDenialValidity_r11_spare2 = 6,
            autonomousDenialValidity_r11_spare1 = 7
        } autonomousDenialValidity_r11;
    } autonomousDenialParameters_r11;  /* optional; set in bit_mask
                                        * autonomousDenialParameters_r11_present
                                        * if present */
               /* Need OR */
    _enum17         idc_Indication_UL_CA_r11;  /* extension #1; optional; set in
                                   * bit_mask idc_Indication_UL_CA_r11_present
                                   * if present */
                                               /* Cond idc-Ind */
} IDC_Config_r11;

typedef struct PowerPrefIndicationConfig_r11 {
    unsigned short  choice;
#       define      PowerPrefIndicationConfig_r11_release_chosen 1
#       define      PowerPrefIndicationConfig_r11_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                              * PowerPrefIndicationConfig_r11_release_chosen */
        struct _seq53 {
            enum {
                s0 = 0,
                s0dot5 = 1,
                powerPrefIndicationTimer_r11_s1 = 2,
                powerPrefIndicationTimer_r11_s2 = 3,
                powerPrefIndicationTimer_r11_s5 = 4,
                powerPrefIndicationTimer_r11_s10 = 5,
                powerPrefIndicationTimer_r11_s20 = 6,
                powerPrefIndicationTimer_r11_s30 = 7,
                powerPrefIndicationTimer_r11_s60 = 8,
                s90 = 9,
                powerPrefIndicationTimer_r11_s120 = 10,
                powerPrefIndicationTimer_r11_s300 = 11,
                powerPrefIndicationTimer_r11_s600 = 12,
                powerPrefIndicationTimer_r11_spare3 = 13,
                powerPrefIndicationTimer_r11_spare2 = 14,
                powerPrefIndicationTimer_r11_spare1 = 15
            } powerPrefIndicationTimer_r11;
        } setup;  /* to choose, set choice to
                   * PowerPrefIndicationConfig_r11_setup_chosen */
    } u;
} PowerPrefIndicationConfig_r11;

typedef struct ObtainLocationConfig_r11 {
    unsigned char   bit_mask;
#       define      obtainLocation_r11_present 0x80
    _enum17         obtainLocation_r11;  /* optional; set in bit_mask
                                          * obtainLocation_r11_present if
                                          * present */
                                         /* Need OR */
} ObtainLocationConfig_r11;

typedef struct OtherConfig_r9 {
    unsigned char   bit_mask;
#       define      reportProximityConfig_r9_present 0x80
#       define      idc_Config_r11_present 0x40
#       define      powerPrefIndicationConfig_r11_present 0x20
#       define      obtainLocationConfig_r11_present 0x10
    ReportProximityConfig_r9 reportProximityConfig_r9;  /* optional; set in
                                   * bit_mask reportProximityConfig_r9_present
                                   * if present */
                                                        /* Need ON */
    IDC_Config_r11  idc_Config_r11;  /* extension #1; optional; set in bit_mask
                                      * idc_Config_r11_present if present */
                                     /* Need ON */
    PowerPrefIndicationConfig_r11 powerPrefIndicationConfig_r11;  /* extension
                                   * #1; optional; set in bit_mask
                                   * powerPrefIndicationConfig_r11_present if
                                   * present */
                                                                  /* Need ON */
    ObtainLocationConfig_r11 obtainLocationConfig_r11;  /* extension #1;
                                   * optional; set in bit_mask
                                   * obtainLocationConfig_r11_present if
                                   * present */
                                                        /* Need ON */
} OtherConfig_r9;

typedef struct PowerCoordinationInfo_r12 {
    unsigned short  p_MeNB_r12;
    unsigned short  p_SeNB_r12;
    unsigned short  powerControlMode_r12;
} PowerCoordinationInfo_r12;

typedef struct RLF_TimersAndConstantsSCG_r12 {
    unsigned short  choice;
#       define      RLF_TimersAndConstantsSCG_r12_release_chosen 1
#       define      RLF_TimersAndConstantsSCG_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                              * RLF_TimersAndConstantsSCG_r12_release_chosen */
        struct _seq54 {
            _enum8          t313_r12;
            _enum9          n313_r12;
            _enum11         n314_r12;
        } setup;  /* to choose, set choice to
                   * RLF_TimersAndConstantsSCG_r12_setup_chosen */
    } u;
} RLF_TimersAndConstantsSCG_r12;

typedef struct RadioResourceConfigDedicatedSCG_r12 {
    unsigned char   bit_mask;
#       define      RadioResourceConfigDedicatedSCG_r12_drb_ToAddModListSCG_r12_present 0x80
#       define      mac_MainConfigSCG_r12_present 0x40
#       define      rlf_TimersAndConstantsSCG_r12_present 0x20
    struct DRB_ToAddModListSCG_r12 *drb_ToAddModListSCG_r12;  /* optional; set
                                   * in bit_mask
       * RadioResourceConfigDedicatedSCG_r12_drb_ToAddModListSCG_r12_present if
       * present */
                                                              /* Need ON */
    MAC_MainConfig  mac_MainConfigSCG_r12;  /* optional; set in bit_mask
                                             * mac_MainConfigSCG_r12_present if
                                             * present */
                                            /* Need ON */
    RLF_TimersAndConstantsSCG_r12 rlf_TimersAndConstantsSCG_r12;  /* optional;
                                   * set in bit_mask
                                   * rlf_TimersAndConstantsSCG_r12_present if
                                   * present */
                                                                  /* Need ON */
} RadioResourceConfigDedicatedSCG_r12;

typedef unsigned short  SCellIndex_r10;

typedef struct UplinkPowerControlCommonSCell_r10 {
    short           p0_NominalPUSCH_r10;
    Alpha_r12       alpha_r10;
} UplinkPowerControlCommonSCell_r10;

typedef struct PRACH_ConfigSCell_r10 {
    unsigned short  prach_ConfigIndex_r10;
} PRACH_ConfigSCell_r10;

typedef struct RACH_ConfigCommonSCell_r11 {
    PowerRampingParameters powerRampingParameters_r11;
    struct {
        PreambleTransMax preambleTransMax_r11;
    } ra_SupervisionInfo_r11;
} RACH_ConfigCommonSCell_r11;

typedef struct UplinkPowerControlCommonSCell_v1130 {
    short           deltaPreambleMsg3_r11;
} UplinkPowerControlCommonSCell_v1130;

typedef struct RadioResourceConfigCommonSCell_r10 {
    unsigned char   bit_mask;
#       define      RadioResourceConfigCommonSCell_r10_ul_Configuration_r10_present 0x80
#       define      ul_CarrierFreq_v1090_present 0x40
#       define      rach_ConfigCommonSCell_r11_present 0x20
#       define      prach_ConfigSCell_r11_present 0x10
#       define      RadioResourceConfigCommonSCell_r10_tdd_Config_v1130_present 0x08
#       define      uplinkPowerControlCommonSCell_v1130_present 0x04
#       define      RadioResourceConfigCommonSCell_r10_pusch_ConfigCommon_v1270_present 0x02
	/* DL configuration as well as configuration applicable for DL and UL */
    struct {
        unsigned char   bit_mask;
#           define      mbsfn_SubframeConfigList_r10_present 0x80
#           define      tdd_Config_r10_present 0x40
		/* 1: Cell characteristics */
        _enum1          dl_Bandwidth_r10;
		/* 2: Physical configuration, general */
        AntennaInfoCommon antennaInfoCommon_r10;
        struct MBSFN_SubframeConfigList *mbsfn_SubframeConfigList_r10;  
                                  /* optional; set in bit_mask
                                   * mbsfn_SubframeConfigList_r10_present if
                                   * present */
                                                                        /* Need OR */
		/* 3: Physical configuration, control */
        PHICH_Config    phich_Config_r10;
		/* 4: Physical configuration, physical channels */
        PDSCH_ConfigCommon pdsch_ConfigCommon_r10;
        TDD_Config      tdd_Config_r10;  /* optional; set in bit_mask
                                          * tdd_Config_r10_present if present */
                                         /* Cond TDDSCell */
    } nonUL_Configuration_r10;
	/* UL configuration */
    struct {
        unsigned char   bit_mask;
#           define      p_Max_r10_present 0x80
#           define      prach_ConfigSCell_r10_present 0x40
        struct {
            unsigned char   bit_mask;
#               define      ul_CarrierFreq_r10_present 0x80
#               define      ul_Bandwidth_r10_present 0x40
            ARFCN_ValueEUTRA ul_CarrierFreq_r10;  /* optional; set in bit_mask
                                                   * ul_CarrierFreq_r10_present
                                                   * if present */
                                                  /* Need OP */
            _enum1          ul_Bandwidth_r10;  /* optional; set in bit_mask
                                                * ul_Bandwidth_r10_present if
                                                * present */
       /* Need OP */
            AdditionalSpectrumEmission additionalSpectrumEmissionSCell_r10;
        } ul_FreqInfo_r10;
        P_Max           p_Max_r10;  /* optional; set in bit_mask
                                     * p_Max_r10_present if present */
                                    /* Need OP */
        UplinkPowerControlCommonSCell_r10 uplinkPowerControlCommonSCell_r10;
		/* A special version of IE UplinkPowerControlCommon may be introduced */
		/* 3: Physical configuration, control */
        SoundingRS_UL_ConfigCommon soundingRS_UL_ConfigCommon_r10;
        UL_CyclicPrefixLength ul_CyclicPrefixLength_r10;
		/* 4: Physical configuration, physical channels */
        PRACH_ConfigSCell_r10 prach_ConfigSCell_r10;  /* optional; set in
                                   * bit_mask prach_ConfigSCell_r10_present if
                                   * present */
                                                      /* Cond TDD-OR-NoR11 */
        PUSCH_ConfigCommon pusch_ConfigCommon_r10;
    } ul_Configuration_r10;  /* optional; set in bit_mask
           * RadioResourceConfigCommonSCell_r10_ul_Configuration_r10_present if
           * present */
       /* Need OR */
    ARFCN_ValueEUTRA_v9e0 ul_CarrierFreq_v1090;  /* extension #1; optional; set
                                   * in bit_mask ul_CarrierFreq_v1090_present if
                                   * present */
                                                 /* Need OP */
    RACH_ConfigCommonSCell_r11 rach_ConfigCommonSCell_r11;  /* extension #2;
                                   * optional; set in bit_mask
                                   * rach_ConfigCommonSCell_r11_present if
                                   * present */
                                                            /* Cond ULSCell */
    PRACH_Config    prach_ConfigSCell_r11;  /* extension #2; optional; set in
                                   * bit_mask prach_ConfigSCell_r11_present if
                                   * present */
                                            /* Cond UL */
    TDD_Config_v1130 tdd_Config_v1130;  /* extension #2; optional; set in
                                   * bit_mask
               * RadioResourceConfigCommonSCell_r10_tdd_Config_v1130_present if
               * present */
                                        /* Cond TDD2 */
    UplinkPowerControlCommonSCell_v1130 uplinkPowerControlCommonSCell_v1130;  
                                        /* extension #2; optional; set in
                                   * bit_mask
                                   * uplinkPowerControlCommonSCell_v1130_present
                                   * if present */
    /* Cond UL */
    PUSCH_ConfigCommon_v1270 pusch_ConfigCommon_v1270;  /* extension #3;
                                   * optional; set in bit_mask
       * RadioResourceConfigCommonSCell_r10_pusch_ConfigCommon_v1270_present if
       * present */
                                                        /* Need OR */
} RadioResourceConfigCommonSCell_r10;

typedef struct UplinkPowerControlCommonPSCell_r12 {
/* For uplink power control the additional/ missing fields are signalled (compared to SCell) */
    _enum5          deltaF_PUCCH_Format3_r12;
    _enum6          deltaF_PUCCH_Format1bCS_r12;
    short           p0_NominalPUCCH_r12;
    DeltaFList_PUCCH deltaFList_PUCCH_r12;
} UplinkPowerControlCommonPSCell_r12;

typedef struct RadioResourceConfigCommonPSCell_r12 {
    RadioResourceConfigCommonSCell_r10 basicFields_r12;
    PUCCH_ConfigCommon pucch_ConfigCommon_r12;
    RACH_ConfigCommon rach_ConfigCommon_r12;
    UplinkPowerControlCommonPSCell_r12 uplinkPowerControlCommonPSCell_r12;
} RadioResourceConfigCommonPSCell_r12;

typedef struct RadioResourceConfigDedicatedPSCell_r12 {
    unsigned char   bit_mask;
#       define      physicalConfigDedicatedPSCell_r12_present 0x80
#       define      sps_Config_r12_present 0x40
#       define      RadioResourceConfigDedicatedPSCell_r12_naics_Info_r12_present 0x20
	/* UE specific configuration extensions applicable for an PSCell */
    PhysicalConfigDedicated physicalConfigDedicatedPSCell_r12;  /* optional; set
                                   * in bit_mask
                                   * physicalConfigDedicatedPSCell_r12_present
                                   * if present */
                                                                /* Need ON */
    SPS_Config      sps_Config_r12;  /* optional; set in bit_mask
                                      * sps_Config_r12_present if present */
                                     /* Need ON */
    NAICS_AssistanceInfo_r12 naics_Info_r12;  /* optional; set in bit_mask
             * RadioResourceConfigDedicatedPSCell_r12_naics_Info_r12_present if
             * present */
                                              /* Need ON */
} RadioResourceConfigDedicatedPSCell_r12;

typedef struct _seq55 {
    PhysCellId      physCellId_r12;
    ARFCN_ValueEUTRA_r9 dl_CarrierFreq_r12;
} _seq55;

typedef struct PSCellToAddMod_r12 {
    unsigned char   bit_mask;
#       define      PSCellToAddMod_r12_cellIdentification_r12_present 0x80
#       define      radioResourceConfigCommonPSCell_r12_present 0x40
#       define      radioResourceConfigDedicatedPSCell_r12_present 0x20
#       define      antennaInfoDedicatedPSCell_v10i0_present 0x10
    SCellIndex_r10  sCellIndex_r12;
    _seq55          cellIdentification_r12;  /* optional; set in bit_mask
                         * PSCellToAddMod_r12_cellIdentification_r12_present if
                         * present */
       /* Cond SCellAdd */
    RadioResourceConfigCommonPSCell_r12 radioResourceConfigCommonPSCell_r12;  
                                        /* optional; set in bit_mask
                               * radioResourceConfigCommonPSCell_r12_present if
                               * present */
                                                                              /* Cond SCellAdd */
    RadioResourceConfigDedicatedPSCell_r12 radioResourceConfigDedicatedPSCell_r12;                                      /* optional; set in bit_mask
                            * radioResourceConfigDedicatedPSCell_r12_present if
                            * present */
                                                                                    /* Cond SCellAdd2 */
    AntennaInfoDedicated_v10i0 antennaInfoDedicatedPSCell_v10i0;  /* extension
                                   * #1; optional; set in bit_mask
                                   * antennaInfoDedicatedPSCell_v10i0_present if
                                   * present */
                                                                  /* Need ON */
} PSCellToAddMod_r12;

typedef struct MobilityControlInfoSCG_r12 {
    unsigned char   bit_mask;
#       define      ue_IdentitySCG_r12_present 0x80
#       define      rach_ConfigDedicated_r12_present 0x40
#       define      cipheringAlgorithmSCG_r12_present 0x20
    _enum29         t307_r12;
    C_RNTI          ue_IdentitySCG_r12;  /* optional; set in bit_mask
                                          * ue_IdentitySCG_r12_present if
                                          * present */
                                         /* Cond SCGEst, */
    RACH_ConfigDedicated rach_ConfigDedicated_r12;  /* optional; set in bit_mask
                                          * rach_ConfigDedicated_r12_present if
                                          * present */
                                                    /* Need OP */
    CipheringAlgorithm_r12 cipheringAlgorithmSCG_r12;  /* optional; set in
                                   * bit_mask cipheringAlgorithmSCG_r12_present
                                   * if present */
                                                       /* Need ON */
} MobilityControlInfoSCG_r12;

typedef struct SCG_ConfigPartSCG_r12 {
    unsigned char   bit_mask;
#       define      radioResourceConfigDedicatedSCG_r12_present 0x80
#       define      SCG_ConfigPartSCG_r12_sCellToReleaseListSCG_r12_present 0x40
#       define      pSCellToAddMod_r12_present 0x20
#       define      SCG_ConfigPartSCG_r12_sCellToAddModListSCG_r12_present 0x10
#       define      mobilityControlInfoSCG_r12_present 0x08
    RadioResourceConfigDedicatedSCG_r12 radioResourceConfigDedicatedSCG_r12;  
                                        /* optional; set in bit_mask
                               * radioResourceConfigDedicatedSCG_r12_present if
                               * present */
                                                                              /* Need ON */
    struct SCellToReleaseList_r10 *sCellToReleaseListSCG_r12;  /* optional; set
                                   * in bit_mask
                   * SCG_ConfigPartSCG_r12_sCellToReleaseListSCG_r12_present if
                   * present */
                                                               /* Need ON */
    PSCellToAddMod_r12 pSCellToAddMod_r12;  /* optional; set in bit_mask
                                             * pSCellToAddMod_r12_present if
                                             * present */
                                            /* Need ON */
    struct SCellToAddModList_r10 *sCellToAddModListSCG_r12;  /* optional; set in
                                   * bit_mask
                    * SCG_ConfigPartSCG_r12_sCellToAddModListSCG_r12_present if
                    * present */
                                                             /* Need ON */
    MobilityControlInfoSCG_r12 mobilityControlInfoSCG_r12;  /* optional; set in
                                   * bit_mask mobilityControlInfoSCG_r12_present
                                   * if present */
                                                            /* Need ON */
} SCG_ConfigPartSCG_r12;

typedef struct SCG_Configuration_r12 {
    unsigned short  choice;
#       define      SCG_Configuration_r12_release_chosen 1
#       define      SCG_Configuration_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * SCG_Configuration_r12_release_chosen */
        struct _seq56 {
            unsigned char   bit_mask;
#               define      scg_ConfigPartMCG_r12_present 0x80
#               define      scg_ConfigPartSCG_r12_present 0x40
            struct {
                unsigned char   bit_mask;
#                   define      scg_Counter_r12_present 0x80
#                   define      scg_ConfigPartMCG_r12_powerCoordinationInfo_r12_present 0x40
                unsigned short  scg_Counter_r12;  /* optional; set in bit_mask
                                                   * scg_Counter_r12_present if
                                                   * present */
                                                  /* Need ON */
                PowerCoordinationInfo_r12 powerCoordinationInfo_r12;  
                                  /* optional; set in bit_mask
                   * scg_ConfigPartMCG_r12_powerCoordinationInfo_r12_present if
                   * present */
                                                                      /* Need ON */
            } scg_ConfigPartMCG_r12;  /* optional; set in bit_mask
                                       * scg_ConfigPartMCG_r12_present if
                                       * present */
       /* Need ON */
            SCG_ConfigPartSCG_r12 scg_ConfigPartSCG_r12;  /* optional; set in
                                   * bit_mask scg_ConfigPartSCG_r12_present if
                                   * present */
                                                          /* Need ON */
        } setup;  /* to choose, set choice to
                   * SCG_Configuration_r12_setup_chosen */
    } u;
} SCG_Configuration_r12;

typedef struct SL_SyncTxControl_r12 {
    unsigned char   bit_mask;
#       define      networkControlledSyncTx_r12_present 0x80
    enum {
        on = 0,
        off = 1
    } networkControlledSyncTx_r12;  /* optional; set in bit_mask
                                     * networkControlledSyncTx_r12_present if
                                     * present */
                                    /* Need OP */
} SL_SyncTxControl_r12;

typedef enum SL_CP_Len_r12 {
    SL_CP_Len_r12_normal = 0,
    SL_CP_Len_r12_extended = 1
} SL_CP_Len_r12;

typedef struct SL_OffsetIndicator_r12 {
    unsigned short  choice;
#       define      small_r12_chosen 1
#       define      large_r12_chosen 2
    union {
        unsigned short  small_r12;  /* to choose, set choice to
                                     * small_r12_chosen */
        unsigned short  large_r12;  /* to choose, set choice to
                                     * large_r12_chosen */
    } u;
} SL_OffsetIndicator_r12;

typedef struct SubframeBitmapSL_r12 {
    unsigned short  choice;
#       define      bs4_r12_chosen 1
#       define      bs8_r12_chosen 2
#       define      bs12_r12_chosen 3
#       define      bs16_r12_chosen 4
#       define      bs30_r12_chosen 5
#       define      bs40_r12_chosen 6
#       define      bs42_r12_chosen 7
    union {
        _bit1           bs4_r12;  /* to choose, set choice to bs4_r12_chosen */
        _bit1           bs8_r12;  /* to choose, set choice to bs8_r12_chosen */
        _bit1           bs12_r12;  /* to choose, set choice to
                                    * bs12_r12_chosen */
        _bit1           bs16_r12;  /* to choose, set choice to
                                    * bs16_r12_chosen */
        _bit1           bs30_r12;  /* to choose, set choice to
                                    * bs30_r12_chosen */
        _bit1           bs40_r12;  /* to choose, set choice to
                                    * bs40_r12_chosen */
        _bit1           bs42_r12;  /* to choose, set choice to
                                    * bs42_r12_chosen */
    } u;
} SubframeBitmapSL_r12;

typedef struct SL_TF_ResourceConfig_r12 {
    unsigned short  prb_Num_r12;
    unsigned short  prb_Start_r12;
    unsigned short  prb_End_r12;
    SL_OffsetIndicator_r12 offsetIndicator_r12;
    SubframeBitmapSL_r12 subframeBitmap_r12;
} SL_TF_ResourceConfig_r12;

typedef short           P0_SL_r12;

typedef struct SL_TxParameters_r12 {
    Alpha_r12       alpha_r12;
    P0_SL_r12       p0_r12;
} SL_TxParameters_r12;

typedef unsigned short  RSRP_RangeSL2_r12;

typedef struct SL_PoolSelectionConfig_r12 {
    RSRP_RangeSL2_r12 threshLow_r12;
    RSRP_RangeSL2_r12 threshHigh_r12;
} SL_PoolSelectionConfig_r12;

typedef struct _seq57 {
    unsigned char   bit_mask;
#       define      tdd_Config_r12_present 0x80
    TDD_Config      tdd_Config_r12;  /* optional; set in bit_mask
                                      * tdd_Config_r12_present if present */
                                     /* Need OR */
    unsigned short  syncConfigIndex_r12;
} _seq57;

typedef struct SL_DiscResourcePool_r12 {
    unsigned char   bit_mask;
#       define      SL_DiscResourcePool_r12_txParameters_r12_present 0x80
#       define      rxParameters_r12_present 0x40
    SL_CP_Len_r12   cp_Len_r12;
    enum {
        discPeriod_r12_rf32 = 0,
        discPeriod_r12_rf64 = 1,
        discPeriod_r12_rf128 = 2,
        discPeriod_r12_rf256 = 3,
        discPeriod_r12_rf512 = 4,
        discPeriod_r12_rf1024 = 5,
        discPeriod_r12_spare2 = 6,
        discPeriod_r12_spare = 7
    } discPeriod_r12;
    unsigned short  numRetx_r12;
    unsigned short  numRepetition_r12;
    SL_TF_ResourceConfig_r12 tf_ResourceConfig_r12;
    struct {
        unsigned char   bit_mask;
#           define      txParameters_r12_ue_SelectedResourceConfig_r12_present 0x80
        SL_TxParameters_r12 txParametersGeneral_r12;
        struct {
            struct {
                unsigned short  choice;
#                   define      rsrpBased_r12_chosen 1
#                   define      random_r12_chosen 2
                union {
                    SL_PoolSelectionConfig_r12 rsrpBased_r12;  /* to choose, set
                                            * choice to rsrpBased_r12_chosen */
                    Nulltype        random_r12;  /* to choose, set choice to
                                                  * random_r12_chosen */
                } u;
            } poolSelection_r12;
            enum {
                txProbability_r12_p25 = 0,
                txProbability_r12_p50 = 1,
                txProbability_r12_p75 = 2,
                p100 = 3
            } txProbability_r12;
        } ue_SelectedResourceConfig_r12;  /* optional; set in bit_mask
                    * txParameters_r12_ue_SelectedResourceConfig_r12_present if
                    * present */
                                          /* Need OR */
    } txParameters_r12;  /* optional; set in bit_mask
                          * SL_DiscResourcePool_r12_txParameters_r12_present if
                          * present */
       /* Cond Tx */
    _seq57          rxParameters_r12;  /* optional; set in bit_mask
                                        * rxParameters_r12_present if present */
       /* Need OR */
} SL_DiscResourcePool_r12;

typedef struct SL_HoppingConfigDisc_r12 {
    unsigned short  a_r12;
    unsigned short  b_r12;
    enum {
        c_r12_n1 = 0,
        c_r12_n5 = 1
    } c_r12;
} SL_HoppingConfigDisc_r12;

typedef struct SL_DiscConfig_r12 {
    unsigned char   bit_mask;
#       define      discTxResources_r12_present 0x80
#       define      discTF_IndexList_v1260_present 0x40
    struct {
        unsigned short  choice;
#           define      discTxResources_r12_release_chosen 1
#           define      discTxResources_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * discTxResources_r12_release_chosen */
            struct _choice17 {
                unsigned short  choice;
#                   define      discTxResources_r12_setup_scheduled_r12_chosen 1
#                   define      discTxResources_r12_setup_ue_Selected_r12_chosen 2
                union {
                    struct _seq58 {
                        unsigned char   bit_mask;
#                           define      discTxConfig_r12_present 0x80
#                           define      discTF_IndexList_r12_present 0x40
#                           define      discHoppingConfig_r12_present 0x20
                        SL_DiscResourcePool_r12 discTxConfig_r12;  /* optional;
                                   * set in bit_mask discTxConfig_r12_present if
                                   * present */
                                                                   /* Need ON */
                        struct SL_TF_IndexPairList_r12 *discTF_IndexList_r12;                                           /* optional; set in bit_mask
                                         * discTF_IndexList_r12_present if
                                         * present */
                                                                               /* Need ON */
                        SL_HoppingConfigDisc_r12 discHoppingConfig_r12;  
                                  /* optional; set in bit_mask
                                   * discHoppingConfig_r12_present if present */
                                                                         /* Need ON */
                    } scheduled_r12;  /* to choose, set choice to
                            * discTxResources_r12_setup_scheduled_r12_chosen */
                    struct _seq59 {
                        unsigned char   bit_mask;
#                           define      discTxPoolDedicated_r12_present 0x80
                        struct {
                            unsigned char   bit_mask;
#                               define      discTxPoolDedicated_r12_poolToReleaseList_r12_present 0x80
#                               define      discTxPoolDedicated_r12_poolToAddModList_r12_present 0x40
                            struct SL_TxPoolToReleaseList_r12 *poolToReleaseList_r12;                                   /* optional; set in bit_mask
                     * discTxPoolDedicated_r12_poolToReleaseList_r12_present if
                     * present */
                                                                                       /* Need ON */
                            struct SL_DiscTxPoolToAddModList_r12 *poolToAddModList_r12;                                 /* optional; set in bit_mask
                      * discTxPoolDedicated_r12_poolToAddModList_r12_present if
                      * present */
                                                                                         /* Need ON */
                        } discTxPoolDedicated_r12;  /* optional; set in bit_mask
                                           * discTxPoolDedicated_r12_present if
                                           * present */
                                                    /* Need ON */
                    } ue_Selected_r12;  /* to choose, set choice to
                          * discTxResources_r12_setup_ue_Selected_r12_chosen */
                } u;
            } setup;  /* to choose, set choice to
                       * discTxResources_r12_setup_chosen */
        } u;
    } discTxResources_r12;  /* optional; set in bit_mask
                             * discTxResources_r12_present if present */
       /* Need ON */
    struct {
        unsigned short  choice;
#           define      discTF_IndexList_v1260_release_chosen 1
#           define      discTF_IndexList_v1260_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                     * discTF_IndexList_v1260_release_chosen */
            struct _seq60 {
                struct SL_TF_IndexPairList_r12b *discTF_IndexList_r12b;
            } setup;  /* to choose, set choice to
                       * discTF_IndexList_v1260_setup_chosen */
        } u;
    } discTF_IndexList_v1260;  /* extension #1; optional; set in bit_mask
                                * discTF_IndexList_v1260_present if present */
                               /* Need ON */
} SL_DiscConfig_r12;

typedef struct MAC_MainConfigSL_r12 {
    unsigned char   bit_mask;
#       define      periodic_BSR_TimerSL_present 0x80
    PeriodicBSR_Timer_r12 periodic_BSR_TimerSL;  /* optional; set in bit_mask
                                                  * periodic_BSR_TimerSL_present
                                                  * if present */
                                                 /* Need ON */
    RetxBSR_Timer_r12 retx_BSR_TimerSL;
} MAC_MainConfigSL_r12;

typedef enum SL_PeriodComm_r12 {
    SL_PeriodComm_r12_sf40 = 0,
    SL_PeriodComm_r12_sf60 = 1,
    sf70 = 2,
    SL_PeriodComm_r12_sf80 = 3,
    sf120 = 4,
    sf140 = 5,
    SL_PeriodComm_r12_sf160 = 6,
    sf240 = 7,
    sf280 = 8,
    SL_PeriodComm_r12_sf320 = 9,
    SL_PeriodComm_r12_spare6 = 10,
    SL_PeriodComm_r12_spare5 = 11,
    SL_PeriodComm_r12_spare4 = 12,
    SL_PeriodComm_r12_spare3 = 13,
    SL_PeriodComm_r12_spare2 = 14,
    SL_PeriodComm_r12_spare = 15
} SL_PeriodComm_r12;

typedef struct SL_HoppingConfigComm_r12 {
    unsigned short  hoppingParameter_r12;
    enum {
        ns1 = 0,
        ns2 = 1,
        ns4 = 2
    } numSubbands_r12;
    unsigned short  rb_Offset_r12;
} SL_HoppingConfigComm_r12;

typedef struct SL_TRPT_Subset_r12 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} SL_TRPT_Subset_r12;

typedef struct SL_CommResourcePool_r12 {
    unsigned char   bit_mask;
#       define      SL_CommResourcePool_r12_ue_SelectedResourceConfig_r12_present 0x80
#       define      rxParametersNCell_r12_present 0x40
#       define      SL_CommResourcePool_r12_txParameters_r12_present 0x20
    SL_CP_Len_r12   sc_CP_Len_r12;
    SL_PeriodComm_r12 sc_Period_r12;
    SL_TF_ResourceConfig_r12 sc_TF_ResourceConfig_r12;
    SL_CP_Len_r12   data_CP_Len_r12;
    SL_HoppingConfigComm_r12 dataHoppingConfig_r12;
    struct {
        unsigned char   bit_mask;
#           define      trpt_Subset_r12_present 0x80
        SL_TF_ResourceConfig_r12 data_TF_ResourceConfig_r12;
        SL_TRPT_Subset_r12 trpt_Subset_r12;  /* optional; set in bit_mask
                                              * trpt_Subset_r12_present if
                                              * present */
                                             /* Need OP */
    } ue_SelectedResourceConfig_r12;  /* optional; set in bit_mask
             * SL_CommResourcePool_r12_ue_SelectedResourceConfig_r12_present if
             * present */
       /* Need OR */
    _seq57          rxParametersNCell_r12;  /* optional; set in bit_mask
                                             * rxParametersNCell_r12_present if
                                             * present */
       /* Need OR */
    struct {
        SL_TxParameters_r12 sc_TxParameters_r12;
        SL_TxParameters_r12 dataTxParameters_r12;
    } txParameters_r12;  /* optional; set in bit_mask
                          * SL_CommResourcePool_r12_txParameters_r12_present if
                          * present */
       /* Cond Tx */
} SL_CommResourcePool_r12;

typedef struct SL_CommConfig_r12 {
    unsigned char   bit_mask;
#       define      commTxResources_r12_present 0x80
    struct {
        unsigned short  choice;
#           define      commTxResources_r12_release_chosen 1
#           define      commTxResources_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * commTxResources_r12_release_chosen */
            struct _choice18 {
                unsigned short  choice;
#                   define      commTxResources_r12_setup_scheduled_r12_chosen 1
#                   define      commTxResources_r12_setup_ue_Selected_r12_chosen 2
                union {
                    struct _seq61 {
                        unsigned char   bit_mask;
#                           define      mcs_r12_present 0x80
                        C_RNTI          sl_RNTI_r12;
                        MAC_MainConfigSL_r12 mac_MainConfig_r12;
                        SL_CommResourcePool_r12 sc_CommTxConfig_r12;
                        unsigned short  mcs_r12;  /* optional; set in bit_mask
                                                   * mcs_r12_present if
                                                   * present */
                                                  /* Need OP */
                    } scheduled_r12;  /* to choose, set choice to
                            * commTxResources_r12_setup_scheduled_r12_chosen */
                    struct _seq62 {
				/* Pool for normal usage */
                        struct {
                            unsigned char   bit_mask;
#                               define      commTxPoolNormalDedicated_r12_poolToReleaseList_r12_present 0x80
#                               define      commTxPoolNormalDedicated_r12_poolToAddModList_r12_present 0x40
                            struct SL_TxPoolToReleaseList_r12 *poolToReleaseList_r12;                                   /* optional; set in bit_mask
               * commTxPoolNormalDedicated_r12_poolToReleaseList_r12_present if
               * present */
                                                                                       /* Need ON */
                            struct SL_CommTxPoolToAddModList_r12 *poolToAddModList_r12;                                 /* optional; set in bit_mask
                * commTxPoolNormalDedicated_r12_poolToAddModList_r12_present if
                * present */
                                                                                         /* Need ON */
                        } commTxPoolNormalDedicated_r12;
                    } ue_Selected_r12;  /* to choose, set choice to
                          * commTxResources_r12_setup_ue_Selected_r12_chosen */
                } u;
            } setup;  /* to choose, set choice to
                       * commTxResources_r12_setup_chosen */
        } u;
    } commTxResources_r12;  /* optional; set in bit_mask
                             * commTxResources_r12_present if present */
       /* Need ON */
} SL_CommConfig_r12;

typedef enum _enum31 {
    t350_r12_min5 = 0,
    t350_r12_min10 = 1,
    t350_r12_min20 = 2,
    t350_r12_min30 = 3,
    t350_r12_min60 = 4,
    t350_r12_min120 = 5,
    min180 = 6,
    t350_r12_spare1 = 7
} _enum31;

typedef struct RRCConnectionReconfiguration_v1250_IEs {
    unsigned char   bit_mask;
#       define      wlan_OffloadInfo_r12_present 0x80
#       define      scg_Configuration_r12_present 0x40
#       define      sl_SyncTxControl_r12_present 0x20
#       define      sl_DiscConfig_r12_present 0x10
#       define      sl_CommConfig_r12_present 0x08
#       define      RRCConnectionReconfiguration_v1250_IEs_nonCriticalExtension_present 0x04
    struct {
        unsigned short  choice;
#           define      wlan_OffloadInfo_r12_release_chosen 1
#           define      wlan_OffloadInfo_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * wlan_OffloadInfo_r12_release_chosen */
            struct _seq63 {
                unsigned char   bit_mask;
#                   define      t350_r12_present 0x80
                WLAN_OffloadConfig_r12 wlan_OffloadConfigDedicated_r12;
                _enum31         t350_r12;  /* optional; set in bit_mask
                                            * t350_r12_present if present */
                                           /* Need OR */
            } setup;  /* to choose, set choice to
                       * wlan_OffloadInfo_r12_setup_chosen */
        } u;
    } wlan_OffloadInfo_r12;  /* optional; set in bit_mask
                              * wlan_OffloadInfo_r12_present if present */
               /* Need ON */
    SCG_Configuration_r12 scg_Configuration_r12;  /* optional; set in bit_mask
                                             * scg_Configuration_r12_present if
                                             * present */
                                                  /* Cond nonFullConfig */
    SL_SyncTxControl_r12 sl_SyncTxControl_r12;  /* optional; set in bit_mask
                                                 * sl_SyncTxControl_r12_present
                                                 * if present */
                                                /* Need ON */
    SL_DiscConfig_r12 sl_DiscConfig_r12;  /* optional; set in bit_mask
                                           * sl_DiscConfig_r12_present if
                                           * present */
                                          /* Need ON */
    SL_CommConfig_r12 sl_CommConfig_r12;  /* optional; set in bit_mask
                                           * sl_CommConfig_r12_present if
                                           * present */
                                          /* Need ON */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
       * RRCConnectionReconfiguration_v1250_IEs_nonCriticalExtension_present if
       * present */
} RRCConnectionReconfiguration_v1250_IEs;

typedef struct _seq64 {
    /* ContentsConstraint is applied to _seq64 */
    _octet1         encoded;
    SystemInformationBlockType1 *decoded;
} _seq64;

typedef struct RRCConnectionReconfiguration_v1130_IEs {
    unsigned char   bit_mask;
#       define      systemInfomationBlockType1Dedicated_r11_present 0x80
#       define      RRCConnectionReconfiguration_v1130_IEs_nonCriticalExtension_present 0x40
    _seq64          systemInfomationBlockType1Dedicated_r11;  /* optional; set
                                   * in bit_mask
                           * systemInfomationBlockType1Dedicated_r11_present if
                           * present */
                                                              /* Need ON */
    RRCConnectionReconfiguration_v1250_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
       * RRCConnectionReconfiguration_v1130_IEs_nonCriticalExtension_present if
       * present */
} RRCConnectionReconfiguration_v1130_IEs;

typedef struct RRCConnectionReconfiguration_v1020_IEs {
    unsigned char   bit_mask;
#       define      sCellToReleaseList_r10_present 0x80
#       define      sCellToAddModList_r10_present 0x40
#       define      RRCConnectionReconfiguration_v1020_IEs_nonCriticalExtension_present 0x20
    struct SCellToReleaseList_r10 *sCellToReleaseList_r10;  /* optional; set in
                                   * bit_mask sCellToReleaseList_r10_present if
                                   * present */
                                                            /* Need ON */
    struct SCellToAddModList_r10 *sCellToAddModList_r10;  /* optional; set in
                                   * bit_mask sCellToAddModList_r10_present if
                                   * present */
                                                          /* Need ON */
    RRCConnectionReconfiguration_v1130_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
       * RRCConnectionReconfiguration_v1020_IEs_nonCriticalExtension_present if
       * present */
} RRCConnectionReconfiguration_v1020_IEs;

/* Regular non-critical extensions: */
typedef struct RRCConnectionReconfiguration_v920_IEs {
    unsigned char   bit_mask;
#       define      otherConfig_r9_present 0x80
#       define      fullConfig_r9_present 0x40
#       define      RRCConnectionReconfiguration_v920_IEs_nonCriticalExtension_present 0x20
    OtherConfig_r9  otherConfig_r9;  /* optional; set in bit_mask
                                      * otherConfig_r9_present if present */
                                     /* Need ON */
    _enum3          fullConfig_r9;  /* optional; set in bit_mask
                                     * fullConfig_r9_present if present */
       /* Cond HO-Reestab */
    RRCConnectionReconfiguration_v1020_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
        * RRCConnectionReconfiguration_v920_IEs_nonCriticalExtension_present if
        * present */
} RRCConnectionReconfiguration_v920_IEs;

typedef struct RRCConnectionReconfiguration_v890_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReconfiguration_v890_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionReconfiguration_v890_IEs_nonCriticalExtension_present 0x40
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        RRCConnectionReconfiguration_v8m0_IEs *decoded;
    } lateNonCriticalExtension;  /* optional; set in bit_mask
    * RRCConnectionReconfiguration_v890_IEs_lateNonCriticalExtension_present if
    * present */
    RRCConnectionReconfiguration_v920_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
        * RRCConnectionReconfiguration_v890_IEs_nonCriticalExtension_present if
        * present */
} RRCConnectionReconfiguration_v890_IEs;

typedef struct RRCConnectionReconfiguration_r8_IEs {
    unsigned char   bit_mask;
#       define      measConfig_present 0x80
#       define      mobilityControlInfo_present 0x40
#       define      dedicatedInfoNASList_present 0x20
#       define      radioResourceConfigDedicated_present 0x10
#       define      securityConfigHO_present 0x08
#       define      RRCConnectionReconfiguration_r8_IEs_nonCriticalExtension_present 0x04
    MeasConfig      measConfig;  /* optional; set in bit_mask measConfig_present
                                  * if present */
                                 /* Need ON */
    MobilityControlInfo mobilityControlInfo;  /* optional; set in bit_mask
                                               * mobilityControlInfo_present if
                                               * present */
                                              /* Cond HO */
    struct _seqof14 {
        struct _seqof14 *next;
        DedicatedInfoNAS value;
    } *dedicatedInfoNASList;  /* optional; set in bit_mask
                               * dedicatedInfoNASList_present if present */
                              /* Cond nonHO */
    RadioResourceConfigDedicated radioResourceConfigDedicated;  /* optional; set
                                   * in bit_mask
                                   * radioResourceConfigDedicated_present if
                                   * present */
                                                                /* Cond HO-toEUTRA */
    SecurityConfigHO securityConfigHO;  /* optional; set in bit_mask
                                         * securityConfigHO_present if
                                         * present */
                                        /* Cond HO */
    RRCConnectionReconfiguration_v890_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
          * RRCConnectionReconfiguration_r8_IEs_nonCriticalExtension_present if
          * present */
} RRCConnectionReconfiguration_r8_IEs;

typedef struct RRCConnectionReconfiguration {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      RRCConnectionReconfiguration_criticalExtensions_c1_chosen 1
#           define      RRCConnectionReconfiguration_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice19 {
                unsigned short  choice;
#                   define      rrcConnectionReconfiguration_r8_chosen 1
#                   define      RRCConnectionReconfiguration_criticalExtensions_c1_spare7_chosen 2
#                   define      RRCConnectionReconfiguration_criticalExtensions_c1_spare6_chosen 3
#                   define      RRCConnectionReconfiguration_criticalExtensions_c1_spare5_chosen 4
#                   define      RRCConnectionReconfiguration_criticalExtensions_c1_spare4_chosen 5
#                   define      RRCConnectionReconfiguration_criticalExtensions_c1_spare3_chosen 6
#                   define      RRCConnectionReconfiguration_criticalExtensions_c1_spare2_chosen 7
#                   define      RRCConnectionReconfiguration_criticalExtensions_c1_spare1_chosen 8
                union {
                    RRCConnectionReconfiguration_r8_IEs rrcConnectionReconfiguration_r8;                                /* to choose, set choice to
                                    * rrcConnectionReconfiguration_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
          * RRCConnectionReconfiguration_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
          * RRCConnectionReconfiguration_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
          * RRCConnectionReconfiguration_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
          * RRCConnectionReconfiguration_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
          * RRCConnectionReconfiguration_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
          * RRCConnectionReconfiguration_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
          * RRCConnectionReconfiguration_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                 * RRCConnectionReconfiguration_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
       * RRCConnectionReconfiguration_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionReconfiguration;

typedef enum ReleaseCause {
    loadBalancingTAUrequired = 0,
    ReleaseCause_other = 1,
    cs_FallbackHighPriority_v1020 = 2,
    ReleaseCause_spare1 = 3
} ReleaseCause;

typedef struct CarrierFreqsGERAN {
    ARFCN_ValueGERAN startingARFCN;
    BandIndicatorGERAN bandIndicator;
    struct {
        unsigned short  choice;
#           define      explicitListOfARFCNs_chosen 1
#           define      equallySpacedARFCNs_chosen 2
#           define      variableBitMapOfARFCNs_chosen 3
        union {
            struct ExplicitListOfARFCNs *explicitListOfARFCNs;  /* to choose,
                                   * set choice to
                                   * explicitListOfARFCNs_chosen */
            struct _seq65 {
                unsigned short  arfcn_Spacing;
                unsigned short  numberOfFollowingARFCNs;
            } equallySpacedARFCNs;  /* to choose, set choice to
                                     * equallySpacedARFCNs_chosen */
            struct _octet5 {
                unsigned short  length;
                unsigned char   value[16];
            } variableBitMapOfARFCNs;  /* to choose, set choice to
                                        * variableBitMapOfARFCNs_chosen */
        } u;
    } followingARFCNs;
} CarrierFreqsGERAN;

typedef unsigned short  ARFCN_ValueUTRA;

typedef struct RedirectedCarrierInfo {
    unsigned short  choice;
#       define      eutra_chosen 1
#       define      RedirectedCarrierInfo_geran_chosen 2
#       define      RedirectedCarrierInfo_utra_FDD_chosen 3
#       define      RedirectedCarrierInfo_utra_TDD_chosen 4
#       define      cdma2000_HRPD_chosen 5
#       define      cdma2000_1xRTT_chosen 6
#       define      RedirectedCarrierInfo_utra_TDD_r10_chosen 7
    union {
        ARFCN_ValueEUTRA eutra;  /* to choose, set choice to eutra_chosen */
        CarrierFreqsGERAN geran;  /* to choose, set choice to
                                   * RedirectedCarrierInfo_geran_chosen */
        ARFCN_ValueUTRA utra_FDD;  /* to choose, set choice to
                                    * RedirectedCarrierInfo_utra_FDD_chosen */
        ARFCN_ValueUTRA utra_TDD;  /* to choose, set choice to
                                    * RedirectedCarrierInfo_utra_TDD_chosen */
        CarrierFreqCDMA2000 cdma2000_HRPD;  /* to choose, set choice to
                                             * cdma2000_HRPD_chosen */
        CarrierFreqCDMA2000 cdma2000_1xRTT;  /* to choose, set choice to
                                              * cdma2000_1xRTT_chosen */
        struct CarrierFreqListUTRA_TDD_r10 *utra_TDD_r10;  /* extension #1; to
                                   * choose, set choice to
                                 * RedirectedCarrierInfo_utra_TDD_r10_chosen */
    } u;
} RedirectedCarrierInfo;

typedef struct IdleModeMobilityControlInfo {
    unsigned char   bit_mask;
#       define      freqPriorityListEUTRA_present 0x80
#       define      freqPriorityListGERAN_present 0x40
#       define      freqPriorityListUTRA_FDD_present 0x20
#       define      freqPriorityListUTRA_TDD_present 0x10
#       define      bandClassPriorityListHRPD_present 0x08
#       define      bandClassPriorityList1XRTT_present 0x04
#       define      t320_present 0x02
#       define      freqPriorityListExtEUTRA_r12_present 0x01
    struct FreqPriorityListEUTRA *freqPriorityListEUTRA;  /* optional; set in
                                   * bit_mask freqPriorityListEUTRA_present if
                                   * present */
                                                          /* Need ON */
    struct FreqsPriorityListGERAN *freqPriorityListGERAN;  /* optional; set in
                                   * bit_mask freqPriorityListGERAN_present if
                                   * present */
                                                           /* Need ON */
    struct FreqPriorityListUTRA_FDD *freqPriorityListUTRA_FDD;  /* optional; set
                                   * in bit_mask
                                   * freqPriorityListUTRA_FDD_present if
                                   * present */
                                                                /* Need ON */
    struct FreqPriorityListUTRA_TDD *freqPriorityListUTRA_TDD;  /* optional; set
                                   * in bit_mask
                                   * freqPriorityListUTRA_TDD_present if
                                   * present */
                                                                /* Need ON */
    struct BandClassPriorityListHRPD *bandClassPriorityListHRPD;  /* optional;
                                   * set in bit_mask
                                   * bandClassPriorityListHRPD_present if
                                   * present */
                                                                  /* Need ON */
    struct BandClassPriorityList1XRTT *bandClassPriorityList1XRTT;  
                                  /* optional; set in bit_mask
                                   * bandClassPriorityList1XRTT_present if
                                   * present */
                                                                    /* Need ON */
    _enum31         t320;  /* optional; set in bit_mask t320_present if
                            * present */
               /* Need OR */
    struct FreqPriorityListExtEUTRA_r12 *freqPriorityListExtEUTRA_r12;  
                                  /* extension #1; optional; set in bit_mask
                                   * freqPriorityListExtEUTRA_r12_present if
                                   * present */
                                                                        /* Need ON */
} IdleModeMobilityControlInfo;

typedef struct RedirectedCarrierInfo_v9e0 {
    ARFCN_ValueEUTRA_v9e0 eutra_v9e0;
} RedirectedCarrierInfo_v9e0;

typedef struct FreqPriorityEUTRA_v9e0 {
    unsigned char   bit_mask;
#       define      FreqPriorityEUTRA_v9e0_carrierFreq_v9e0_present 0x80
    ARFCN_ValueEUTRA_v9e0 carrierFreq_v9e0;  /* optional; set in bit_mask
                           * FreqPriorityEUTRA_v9e0_carrierFreq_v9e0_present if
                           * present */
                                             /* Cond EARFCN-max */
} FreqPriorityEUTRA_v9e0;

typedef struct IdleModeMobilityControlInfo_v9e0 {
    struct _seqof15 {
        struct _seqof15 *next;
        FreqPriorityEUTRA_v9e0 value;
    } *freqPriorityListEUTRA_v9e0;
} IdleModeMobilityControlInfo_v9e0;

/* Late non critical extensions */
typedef struct RRCConnectionRelease_v9e0_IEs {
    unsigned char   bit_mask;
#       define      redirectedCarrierInfo_v9e0_present 0x80
#       define      idleModeMobilityControlInfo_v9e0_present 0x40
#       define      RRCConnectionRelease_v9e0_IEs_nonCriticalExtension_present 0x20
    RedirectedCarrierInfo_v9e0 redirectedCarrierInfo_v9e0;  /* optional; set in
                                   * bit_mask redirectedCarrierInfo_v9e0_present
                                   * if present */
                                                            /* Cond NoRedirect-r8 */
    IdleModeMobilityControlInfo_v9e0 idleModeMobilityControlInfo_v9e0;  
                                  /* optional; set in bit_mask
                                   * idleModeMobilityControlInfo_v9e0_present if
                                   * present */
                                                                        /* Cond IdleInfoEUTRA */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * RRCConnectionRelease_v9e0_IEs_nonCriticalExtension_present if
                * present */
} RRCConnectionRelease_v9e0_IEs;

typedef struct RRCConnectionRelease_v1020_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionRelease_v1020_IEs_extendedWaitTime_r10_present 0x80
#       define      RRCConnectionRelease_v1020_IEs_nonCriticalExtension_present 0x40
    unsigned short  extendedWaitTime_r10;  /* optional; set in bit_mask
               * RRCConnectionRelease_v1020_IEs_extendedWaitTime_r10_present if
               * present */
                                           /* Need ON */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
               * RRCConnectionRelease_v1020_IEs_nonCriticalExtension_present if
               * present */
} RRCConnectionRelease_v1020_IEs;

/* Regular non critical extensions */
typedef struct RRCConnectionRelease_v920_IEs {
    unsigned char   bit_mask;
#       define      cellInfoList_r9_present 0x80
#       define      RRCConnectionRelease_v920_IEs_nonCriticalExtension_present 0x40
    struct {
        unsigned short  choice;
#           define      geran_r9_chosen 1
#           define      utra_FDD_r9_chosen 2
#           define      utra_TDD_r9_chosen 3
#           define      cellInfoList_r9_utra_TDD_r10_chosen 4
        union {
            struct CellInfoListGERAN_r9 *geran_r9;  /* to choose, set choice to
                                                     * geran_r9_chosen */
            struct CellInfoListUTRA_FDD_r9 *utra_FDD_r9;  /* to choose, set
                                              * choice to utra_FDD_r9_chosen */
            struct CellInfoListUTRA_TDD_r9 *utra_TDD_r9;  /* to choose, set
                                              * choice to utra_TDD_r9_chosen */
            struct CellInfoListUTRA_TDD_r10 *utra_TDD_r10;  /* extension #1; to
                                   * choose, set choice to
                                   * cellInfoList_r9_utra_TDD_r10_chosen */
        } u;
    } cellInfoList_r9;  /* optional; set in bit_mask cellInfoList_r9_present if
                         * present */
       /* Cond Redirection */
    RRCConnectionRelease_v1020_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * RRCConnectionRelease_v920_IEs_nonCriticalExtension_present if
                * present */
} RRCConnectionRelease_v920_IEs;

typedef struct RRCConnectionRelease_v890_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionRelease_v890_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionRelease_v890_IEs_nonCriticalExtension_present 0x40
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        RRCConnectionRelease_v9e0_IEs *decoded;
    } lateNonCriticalExtension;  /* optional; set in bit_mask
            * RRCConnectionRelease_v890_IEs_lateNonCriticalExtension_present if
            * present */
    RRCConnectionRelease_v920_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * RRCConnectionRelease_v890_IEs_nonCriticalExtension_present if
                * present */
} RRCConnectionRelease_v890_IEs;

typedef struct RRCConnectionRelease_r8_IEs {
    unsigned char   bit_mask;
#       define      redirectedCarrierInfo_present 0x80
#       define      idleModeMobilityControlInfo_present 0x40
#       define      RRCConnectionRelease_r8_IEs_nonCriticalExtension_present 0x20
    ReleaseCause    releaseCause;
    RedirectedCarrierInfo redirectedCarrierInfo;  /* optional; set in bit_mask
                                             * redirectedCarrierInfo_present if
                                             * present */
                                                  /* Need ON */
    IdleModeMobilityControlInfo idleModeMobilityControlInfo;  /* optional; set
                                   * in bit_mask
                                   * idleModeMobilityControlInfo_present if
                                   * present */
                                                              /* Need OP */
    RRCConnectionRelease_v890_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                  * RRCConnectionRelease_r8_IEs_nonCriticalExtension_present if
                  * present */
} RRCConnectionRelease_r8_IEs;

typedef struct RRCConnectionRelease {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      RRCConnectionRelease_criticalExtensions_c1_chosen 1
#           define      RRCConnectionRelease_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice20 {
                unsigned short  choice;
#                   define      rrcConnectionRelease_r8_chosen 1
#                   define      RRCConnectionRelease_criticalExtensions_c1_spare3_chosen 2
#                   define      RRCConnectionRelease_criticalExtensions_c1_spare2_chosen 3
#                   define      RRCConnectionRelease_criticalExtensions_c1_spare1_chosen 4
                union {
                    RRCConnectionRelease_r8_IEs rrcConnectionRelease_r8;  
                                        /* to choose, set choice to
                                         * rrcConnectionRelease_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                  * RRCConnectionRelease_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                  * RRCConnectionRelease_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                  * RRCConnectionRelease_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * RRCConnectionRelease_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * RRCConnectionRelease_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionRelease;

typedef struct SecurityConfigSMC {
    SecurityAlgorithmConfig securityAlgorithmConfig;
} SecurityConfigSMC;

typedef struct SecurityModeCommand_v8a0_IEs {
    unsigned char   bit_mask;
#       define      SecurityModeCommand_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      SecurityModeCommand_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
             * SecurityModeCommand_v8a0_IEs_lateNonCriticalExtension_present if
             * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                 * SecurityModeCommand_v8a0_IEs_nonCriticalExtension_present if
                 * present */
} SecurityModeCommand_v8a0_IEs;

typedef struct SecurityModeCommand_r8_IEs {
    unsigned char   bit_mask;
#       define      SecurityModeCommand_r8_IEs_nonCriticalExtension_present 0x80
    SecurityConfigSMC securityConfigSMC;
    SecurityModeCommand_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                   * SecurityModeCommand_r8_IEs_nonCriticalExtension_present if
                   * present */
} SecurityModeCommand_r8_IEs;

typedef struct SecurityModeCommand {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      SecurityModeCommand_criticalExtensions_c1_chosen 1
#           define      SecurityModeCommand_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice21 {
                unsigned short  choice;
#                   define      securityModeCommand_r8_chosen 1
#                   define      SecurityModeCommand_criticalExtensions_c1_spare3_chosen 2
#                   define      SecurityModeCommand_criticalExtensions_c1_spare2_chosen 3
#                   define      SecurityModeCommand_criticalExtensions_c1_spare1_chosen 4
                union {
                    SecurityModeCommand_r8_IEs securityModeCommand_r8;  /* to
                                   * choose, set choice to
                                   * securityModeCommand_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                   * SecurityModeCommand_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                   * SecurityModeCommand_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                   * SecurityModeCommand_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * SecurityModeCommand_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
    * SecurityModeCommand_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SecurityModeCommand;

typedef unsigned short  FreqBandIndicator_r11;

typedef struct _seqof16 {
    struct _seqof16 *next;
    FreqBandIndicator_r11 value;
} *_seqof16;

typedef struct UECapabilityEnquiry_v1180_IEs {
    unsigned char   bit_mask;
#       define      requestedFrequencyBands_r11_present 0x80
#       define      UECapabilityEnquiry_v1180_IEs_nonCriticalExtension_present 0x40
    struct _seqof16 *requestedFrequencyBands_r11;  /* optional; set in bit_mask
                                       * requestedFrequencyBands_r11_present if
                                       * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * UECapabilityEnquiry_v1180_IEs_nonCriticalExtension_present if
                * present */
} UECapabilityEnquiry_v1180_IEs;

typedef struct UECapabilityEnquiry_v8a0_IEs {
    unsigned char   bit_mask;
#       define      UECapabilityEnquiry_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      UECapabilityEnquiry_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
             * UECapabilityEnquiry_v8a0_IEs_lateNonCriticalExtension_present if
             * present */
    UECapabilityEnquiry_v1180_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UECapabilityEnquiry_v8a0_IEs_nonCriticalExtension_present if
                 * present */
} UECapabilityEnquiry_v8a0_IEs;

typedef struct UECapabilityEnquiry_r8_IEs {
    unsigned char   bit_mask;
#       define      UECapabilityEnquiry_r8_IEs_nonCriticalExtension_present 0x80
    struct UE_CapabilityRequest *ue_CapabilityRequest;
    UECapabilityEnquiry_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                   * UECapabilityEnquiry_r8_IEs_nonCriticalExtension_present if
                   * present */
} UECapabilityEnquiry_r8_IEs;

typedef struct UECapabilityEnquiry {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      UECapabilityEnquiry_criticalExtensions_c1_chosen 1
#           define      UECapabilityEnquiry_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice22 {
                unsigned short  choice;
#                   define      ueCapabilityEnquiry_r8_chosen 1
#                   define      UECapabilityEnquiry_criticalExtensions_c1_spare3_chosen 2
#                   define      UECapabilityEnquiry_criticalExtensions_c1_spare2_chosen 3
#                   define      UECapabilityEnquiry_criticalExtensions_c1_spare1_chosen 4
                union {
                    UECapabilityEnquiry_r8_IEs ueCapabilityEnquiry_r8;  /* to
                                   * choose, set choice to
                                   * ueCapabilityEnquiry_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                   * UECapabilityEnquiry_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                   * UECapabilityEnquiry_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                   * UECapabilityEnquiry_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * UECapabilityEnquiry_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
    * UECapabilityEnquiry_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} UECapabilityEnquiry;

typedef struct CounterCheck_v8a0_IEs {
    unsigned char   bit_mask;
#       define      CounterCheck_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      CounterCheck_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
                    * CounterCheck_v8a0_IEs_lateNonCriticalExtension_present if
                    * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                        * CounterCheck_v8a0_IEs_nonCriticalExtension_present if
                        * present */
} CounterCheck_v8a0_IEs;

typedef struct CounterCheck_r8_IEs {
    unsigned char   bit_mask;
#       define      CounterCheck_r8_IEs_nonCriticalExtension_present 0x80
    struct DRB_CountMSB_InfoList *drb_CountMSB_InfoList;
    CounterCheck_v8a0_IEs nonCriticalExtension;  /* optional; set in bit_mask
                          * CounterCheck_r8_IEs_nonCriticalExtension_present if
                          * present */
} CounterCheck_r8_IEs;

typedef struct CounterCheck {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      CounterCheck_criticalExtensions_c1_chosen 1
#           define      CounterCheck_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice23 {
                unsigned short  choice;
#                   define      counterCheck_r8_chosen 1
#                   define      CounterCheck_criticalExtensions_c1_spare3_chosen 2
#                   define      CounterCheck_criticalExtensions_c1_spare2_chosen 3
#                   define      CounterCheck_criticalExtensions_c1_spare1_chosen 4
                union {
                    CounterCheck_r8_IEs counterCheck_r8;  /* to choose, set
                                          * choice to counterCheck_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                          * CounterCheck_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                          * CounterCheck_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                          * CounterCheck_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * CounterCheck_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
           * CounterCheck_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} CounterCheck;

typedef struct UEInformationRequest_v1250_IEs {
    unsigned char   bit_mask;
#       define      mobilityHistoryReportReq_r12_present 0x80
#       define      UEInformationRequest_v1250_IEs_nonCriticalExtension_present 0x40
    _enum3          mobilityHistoryReportReq_r12;  /* optional; set in bit_mask
                                      * mobilityHistoryReportReq_r12_present if
                                      * present */
       /* Need ON */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
               * UEInformationRequest_v1250_IEs_nonCriticalExtension_present if
               * present */
} UEInformationRequest_v1250_IEs;

typedef struct UEInformationRequest_v1130_IEs {
    unsigned char   bit_mask;
#       define      connEstFailReportReq_r11_present 0x80
#       define      UEInformationRequest_v1130_IEs_nonCriticalExtension_present 0x40
    _enum3          connEstFailReportReq_r11;  /* optional; set in bit_mask
                                          * connEstFailReportReq_r11_present if
                                          * present */
       /* Need ON */
    UEInformationRequest_v1250_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
               * UEInformationRequest_v1130_IEs_nonCriticalExtension_present if
               * present */
} UEInformationRequest_v1130_IEs;

typedef struct UEInformationRequest_v1020_IEs {
    unsigned char   bit_mask;
#       define      logMeasReportReq_r10_present 0x80
#       define      UEInformationRequest_v1020_IEs_nonCriticalExtension_present 0x40
    _enum3          logMeasReportReq_r10;  /* optional; set in bit_mask
                                            * logMeasReportReq_r10_present if
                                            * present */
       /* Need ON */
    UEInformationRequest_v1130_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
               * UEInformationRequest_v1020_IEs_nonCriticalExtension_present if
               * present */
} UEInformationRequest_v1020_IEs;

typedef struct UEInformationRequest_v930_IEs {
    unsigned char   bit_mask;
#       define      UEInformationRequest_v930_IEs_lateNonCriticalExtension_present 0x80
#       define      UEInformationRequest_v930_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
            * UEInformationRequest_v930_IEs_lateNonCriticalExtension_present if
            * present */
    UEInformationRequest_v1020_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UEInformationRequest_v930_IEs_nonCriticalExtension_present if
                * present */
} UEInformationRequest_v930_IEs;

typedef struct UEInformationRequest_r9_IEs {
    unsigned char   bit_mask;
#       define      UEInformationRequest_r9_IEs_nonCriticalExtension_present 0x80
    ossBoolean      rach_ReportReq_r9;
    ossBoolean      rlf_ReportReq_r9;
    UEInformationRequest_v930_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                  * UEInformationRequest_r9_IEs_nonCriticalExtension_present if
                  * present */
} UEInformationRequest_r9_IEs;

typedef struct UEInformationRequest_r9 {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      UEInformationRequest_r9_criticalExtensions_c1_chosen 1
#           define      UEInformationRequest_r9_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice24 {
                unsigned short  choice;
#                   define      criticalExtensions_c1_ueInformationRequest_r9_chosen 1
#                   define      UEInformationRequest_r9_criticalExtensions_c1_spare3_chosen 2
#                   define      UEInformationRequest_r9_criticalExtensions_c1_spare2_chosen 3
#                   define      UEInformationRequest_r9_criticalExtensions_c1_spare1_chosen 4
                union {
                    UEInformationRequest_r9_IEs ueInformationRequest_r9;  
                                        /* to choose, set choice to
                      * criticalExtensions_c1_ueInformationRequest_r9_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
               * UEInformationRequest_r9_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
               * UEInformationRequest_r9_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
               * UEInformationRequest_r9_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * UEInformationRequest_r9_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
  * UEInformationRequest_r9_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} UEInformationRequest_r9;

typedef struct PLMN_Identity {
    unsigned char   bit_mask;
#       define      mcc_present 0x80
    struct MCC      *mcc;  /* optional; set in bit_mask mcc_present if
                            * present */
                           /* Cond MCC */
    struct MNC      *mnc;
} PLMN_Identity;

typedef struct _octet6 {
    unsigned short  length;
    unsigned char   value[3];
} _octet6;

typedef struct TraceReference_r10 {
    PLMN_Identity   plmn_Identity_r10;
    _octet6         traceId_r10;
} TraceReference_r10;

typedef struct AbsoluteTimeInfo_r10 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} AbsoluteTimeInfo_r10;

typedef struct AreaConfiguration_r10 {
    unsigned short  choice;
#       define      cellGlobalIdList_r10_chosen 1
#       define      trackingAreaCodeList_r10_chosen 2
    union {
        struct CellGlobalIdList_r10 *cellGlobalIdList_r10;  /* to choose, set
                                     * choice to cellGlobalIdList_r10_chosen */
        struct TrackingAreaCodeList_r10 *trackingAreaCodeList_r10;  /* to
                                   * choose, set choice to
                                   * trackingAreaCodeList_r10_chosen */
    } u;
} AreaConfiguration_r10;

typedef enum LoggingDuration_r10 {
    LoggingDuration_r10_min10 = 0,
    LoggingDuration_r10_min20 = 1,
    min40 = 2,
    LoggingDuration_r10_min60 = 3,
    min90 = 4,
    LoggingDuration_r10_min120 = 5,
    LoggingDuration_r10_spare2 = 6,
    LoggingDuration_r10_spare1 = 7
} LoggingDuration_r10;

typedef enum LoggingInterval_r10 {
    LoggingInterval_r10_ms1280 = 0,
    LoggingInterval_r10_ms2560 = 1,
    LoggingInterval_r10_ms5120 = 2,
    LoggingInterval_r10_ms10240 = 3,
    ms20480 = 4,
    ms30720 = 5,
    ms40960 = 6,
    ms61440 = 7
} LoggingInterval_r10;

typedef struct TrackingAreaCodeList_v1130 {
    struct _seqof17 {
        struct _seqof17 *next;
        PLMN_Identity   value;
    } *plmn_Identity_perTAC_List_r11;
} TrackingAreaCodeList_v1130;

typedef struct AreaConfiguration_v1130 {
    TrackingAreaCodeList_v1130 trackingAreaCodeList_v1130;
} AreaConfiguration_v1130;

typedef struct LoggedMeasurementConfiguration_v1250_IEs {
    unsigned char   bit_mask;
#       define      LoggedMeasurementConfiguration_v1250_IEs_targetMBSFN_AreaList_r12_present 0x80
#       define      LoggedMeasurementConfiguration_v1250_IEs_nonCriticalExtension_present 0x40
    struct TargetMBSFN_AreaList_r12 *targetMBSFN_AreaList_r12;  /* optional; set
                                   * in bit_mask
 * LoggedMeasurementConfiguration_v1250_IEs_targetMBSFN_AreaList_r12_present if
 * present */
                                                                /* Need OP */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
     * LoggedMeasurementConfiguration_v1250_IEs_nonCriticalExtension_present if
     * present */
} LoggedMeasurementConfiguration_v1250_IEs;

typedef struct LoggedMeasurementConfiguration_v1130_IEs {
    unsigned char   bit_mask;
#       define      plmn_IdentityList_r11_present 0x80
#       define      LoggedMeasurementConfiguration_v1130_IEs_areaConfiguration_v1130_present 0x40
#       define      LoggedMeasurementConfiguration_v1130_IEs_nonCriticalExtension_present 0x20
    struct PLMN_IdentityList3_r11 *plmn_IdentityList_r11;  /* optional; set in
                                   * bit_mask plmn_IdentityList_r11_present if
                                   * present */
                                                           /* Need OR */
    AreaConfiguration_v1130 areaConfiguration_v1130;  /* optional; set in
                                   * bit_mask
  * LoggedMeasurementConfiguration_v1130_IEs_areaConfiguration_v1130_present if
  * present */
                                                      /* Need OR */
    LoggedMeasurementConfiguration_v1250_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
     * LoggedMeasurementConfiguration_v1130_IEs_nonCriticalExtension_present if
     * present */
} LoggedMeasurementConfiguration_v1130_IEs;

typedef struct LoggedMeasurementConfiguration_v1080_IEs {
    unsigned char   bit_mask;
#       define      lateNonCriticalExtension_r10_present 0x80
#       define      LoggedMeasurementConfiguration_v1080_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension_r10;  /* optional; set in bit_mask
                                      * lateNonCriticalExtension_r10_present if
                                      * present */
    LoggedMeasurementConfiguration_v1130_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
     * LoggedMeasurementConfiguration_v1080_IEs_nonCriticalExtension_present if
     * present */
} LoggedMeasurementConfiguration_v1080_IEs;

typedef struct LoggedMeasurementConfiguration_r10_IEs {
    unsigned char   bit_mask;
#       define      LoggedMeasurementConfiguration_r10_IEs_areaConfiguration_r10_present 0x80
#       define      LoggedMeasurementConfiguration_r10_IEs_nonCriticalExtension_present 0x40
    TraceReference_r10 traceReference_r10;
    _octet2         traceRecordingSessionRef_r10;
    _octet3         tce_Id_r10;
    AbsoluteTimeInfo_r10 absoluteTimeInfo_r10;
    AreaConfiguration_r10 areaConfiguration_r10;  /* optional; set in bit_mask
      * LoggedMeasurementConfiguration_r10_IEs_areaConfiguration_r10_present if
      * present */
                                                  /* Need OR */
    LoggingDuration_r10 loggingDuration_r10;
    LoggingInterval_r10 loggingInterval_r10;
    LoggedMeasurementConfiguration_v1080_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
       * LoggedMeasurementConfiguration_r10_IEs_nonCriticalExtension_present if
       * present */
} LoggedMeasurementConfiguration_r10_IEs;

typedef struct LoggedMeasurementConfiguration_r10 {
    struct {
        unsigned short  choice;
#           define      LoggedMeasurementConfiguration_r10_criticalExtensions_c1_chosen 1
#           define      LoggedMeasurementConfiguration_r10_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice25 {
                unsigned short  choice;
#                   define      criticalExtensions_c1_loggedMeasurementConfiguration_r10_chosen 1
#                   define      LoggedMeasurementConfiguration_r10_criticalExtensions_c1_spare3_chosen 2
#                   define      LoggedMeasurementConfiguration_r10_criticalExtensions_c1_spare2_chosen 3
#                   define      LoggedMeasurementConfiguration_r10_criticalExtensions_c1_spare1_chosen 4
                union {
                    LoggedMeasurementConfiguration_r10_IEs loggedMeasurementConfiguration_r10;                          /* to choose, set choice to
           * criticalExtensions_c1_loggedMeasurementConfiguration_r10_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
    * LoggedMeasurementConfiguration_r10_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
    * LoggedMeasurementConfiguration_r10_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
    * LoggedMeasurementConfiguration_r10_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
           * LoggedMeasurementConfiguration_r10_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
             * LoggedMeasurementConfiguration_r10_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} LoggedMeasurementConfiguration_r10;

typedef struct RN_SystemInfo_r10 {
    unsigned char   bit_mask;
#       define      systemInformationBlockType1_r10_present 0x80
#       define      systemInformationBlockType2_r10_present 0x40
    _seq64          systemInformationBlockType1_r10;  /* optional; set in
                                   * bit_mask
                                   * systemInformationBlockType1_r10_present if
                                   * present */
                                                      /* Need ON */
    SystemInformationBlockType2 systemInformationBlockType2_r10;  /* optional;
                                   * set in bit_mask
                                   * systemInformationBlockType2_r10_present if
                                   * present */
                                                                  /* Need ON */
} RN_SystemInfo_r10;

typedef struct _choice26 {
    unsigned short  choice;
#       define      nrb6_r10_chosen 1
#       define      nrb15_r10_chosen 2
#       define      nrb25_r10_chosen 3
#       define      nrb50_r10_chosen 4
#       define      nrb75_r10_chosen 5
#       define      nrb100_r10_chosen 6
    union {
        _bit1           nrb6_r10;  /* to choose, set choice to
                                    * nrb6_r10_chosen */
        _bit1           nrb15_r10;  /* to choose, set choice to
                                     * nrb15_r10_chosen */
        _bit1           nrb25_r10;  /* to choose, set choice to
                                     * nrb25_r10_chosen */
        _bit1           nrb50_r10;  /* to choose, set choice to
                                     * nrb50_r10_chosen */
        _bit1           nrb75_r10;  /* to choose, set choice to
                                     * nrb75_r10_chosen */
        _bit1           nrb100_r10;  /* to choose, set choice to
                                      * nrb100_r10_chosen */
    } u;
} _choice26;

typedef struct _seq67 {
    unsigned char   bit_mask;
#       define      n1PUCCH_AN_P1_r10_present 0x80
    unsigned short  n1PUCCH_AN_P0_r10;
    unsigned short  n1PUCCH_AN_P1_r10;  /* optional; set in bit_mask
                                         * n1PUCCH_AN_P1_r10_present if
                                         * present */
                                        /* Need OR */
} _seq67;

typedef struct RN_SubframeConfig_r10 {
    unsigned char   bit_mask;
#       define      subframeConfigPattern_r10_present 0x80
#       define      rpdcch_Config_r10_present 0x40
    struct {
        unsigned short  choice;
#           define      subframeConfigPatternFDD_r10_chosen 1
#           define      subframeConfigPatternTDD_r10_chosen 2
        union {
            _bit1           subframeConfigPatternFDD_r10;  /* to choose, set
                                   * choice to
                                   * subframeConfigPatternFDD_r10_chosen */
            unsigned short  subframeConfigPatternTDD_r10;  /* to choose, set
                                   * choice to
                                   * subframeConfigPatternTDD_r10_chosen */
        } u;
    } subframeConfigPattern_r10;  /* optional; set in bit_mask
                                   * subframeConfigPattern_r10_present if
                                   * present */
       /* Need ON */
    struct {
        enum {
            type0 = 0,
            type1 = 1,
            type2Localized = 2,
            type2Distributed = 3,
            resourceAllocationType_r10_spare4 = 4,
            resourceAllocationType_r10_spare3 = 5,
            resourceAllocationType_r10_spare2 = 6,
            resourceAllocationType_r10_spare1 = 7
        } resourceAllocationType_r10;
        struct {
            unsigned short  choice;
#               define      type01_r10_chosen 1
#               define      type2_r10_chosen 2
            union {
                _choice26       type01_r10;  /* to choose, set choice to
                                              * type01_r10_chosen */
                _choice26       type2_r10;  /* to choose, set choice to
                                             * type2_r10_chosen */
            } u;
        } resourceBlockAssignment_r10;
        struct {
            unsigned short  choice;
#               define      interleaving_r10_chosen 1
#               define      noInterleaving_r10_chosen 2
            union {
                enum _enum32 {
                    interleaving_r10_crs = 0
                } interleaving_r10;  /* to choose, set choice to
                                      * interleaving_r10_chosen */
                enum _enum33 {
                    noInterleaving_r10_crs = 0,
                    dmrs = 1
                } noInterleaving_r10;  /* to choose, set choice to
                                        * noInterleaving_r10_chosen */
            } u;
        } demodulationRS_r10;
        unsigned short  pdsch_Start_r10;
        struct {
            unsigned short  choice;
#               define      pucch_Config_r10_tdd_chosen 1
#               define      pucch_Config_r10_fdd_chosen 2
            union {
                struct _choice27 {
                    unsigned short  choice;
#                       define      channelSelectionMultiplexingBundling_chosen 1
#                       define      fallbackForFormat3_chosen 2
                    union {
                        struct _seq66 {
                            struct _seqof10 *n1PUCCH_AN_List_r10;
                        } channelSelectionMultiplexingBundling;  /* to choose,
                                   * set choice to
                               * channelSelectionMultiplexingBundling_chosen */
                        _seq67          fallbackForFormat3;  /* to choose, set
                                       * choice to fallbackForFormat3_chosen */
                    } u;
                } tdd;  /* to choose, set choice to
                         * pucch_Config_r10_tdd_chosen */
                _seq67          fdd;  /* to choose, set choice to
                                       * pucch_Config_r10_fdd_chosen */
            } u;
        } pucch_Config_r10;
    } rpdcch_Config_r10;  /* optional; set in bit_mask rpdcch_Config_r10_present
                           * if present */
       /* Need ON */
} RN_SubframeConfig_r10;

typedef struct RNReconfiguration_r10_IEs {
    unsigned char   bit_mask;
#       define      rn_SystemInfo_r10_present 0x80
#       define      rn_SubframeConfig_r10_present 0x40
#       define      RNReconfiguration_r10_IEs_lateNonCriticalExtension_present 0x20
#       define      RNReconfiguration_r10_IEs_nonCriticalExtension_present 0x10
    RN_SystemInfo_r10 rn_SystemInfo_r10;  /* optional; set in bit_mask
                                           * rn_SystemInfo_r10_present if
                                           * present */
                                          /* Need ON */
    RN_SubframeConfig_r10 rn_SubframeConfig_r10;  /* optional; set in bit_mask
                                             * rn_SubframeConfig_r10_present if
                                             * present */
                                                  /* Need ON */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
                * RNReconfiguration_r10_IEs_lateNonCriticalExtension_present if
                * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                    * RNReconfiguration_r10_IEs_nonCriticalExtension_present if
                    * present */
} RNReconfiguration_r10_IEs;

typedef struct RNReconfiguration_r10 {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      RNReconfiguration_r10_criticalExtensions_c1_chosen 1
#           define      RNReconfiguration_r10_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice28 {
                unsigned short  choice;
#                   define      criticalExtensions_c1_rnReconfiguration_r10_chosen 1
#                   define      RNReconfiguration_r10_criticalExtensions_c1_spare3_chosen 2
#                   define      RNReconfiguration_r10_criticalExtensions_c1_spare2_chosen 3
#                   define      RNReconfiguration_r10_criticalExtensions_c1_spare1_chosen 4
                union {
                    RNReconfiguration_r10_IEs rnReconfiguration_r10;  /* to
                                   * choose, set choice to
                        * criticalExtensions_c1_rnReconfiguration_r10_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                 * RNReconfiguration_r10_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                 * RNReconfiguration_r10_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                 * RNReconfiguration_r10_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * RNReconfiguration_r10_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
  * RNReconfiguration_r10_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RNReconfiguration_r10;

typedef struct DL_DCCH_MessageType {
    unsigned short  choice;
#       define      DL_DCCH_MessageType_c1_chosen 1
#       define      DL_DCCH_MessageType_messageClassExtension_chosen 2
    union {
        struct _choice29 {
            unsigned short  choice;
#               define      csfbParametersResponseCDMA2000_chosen 1
#               define      dlInformationTransfer_chosen 2
#               define      handoverFromEUTRAPreparationRequest_chosen 3
#               define      mobilityFromEUTRACommand_chosen 4
#               define      rrcConnectionReconfiguration_chosen 5
#               define      rrcConnectionRelease_chosen 6
#               define      securityModeCommand_chosen 7
#               define      ueCapabilityEnquiry_chosen 8
#               define      counterCheck_chosen 9
#               define      DL_DCCH_MessageType_c1_ueInformationRequest_r9_chosen 10
#               define      DL_DCCH_MessageType_c1_loggedMeasurementConfiguration_r10_chosen 11
#               define      DL_DCCH_MessageType_c1_rnReconfiguration_r10_chosen 12
#               define      DL_DCCH_MessageType_c1_spare4_chosen 13
#               define      DL_DCCH_MessageType_c1_spare3_chosen 14
#               define      DL_DCCH_MessageType_c1_spare2_chosen 15
#               define      DL_DCCH_MessageType_c1_spare1_chosen 16
            union {
                CSFBParametersResponseCDMA2000 csfbParametersResponseCDMA2000;                                          /* to choose, set choice to
                                     * csfbParametersResponseCDMA2000_chosen */
                DLInformationTransfer dlInformationTransfer;  /* to choose, set
                                    * choice to dlInformationTransfer_chosen */
                HandoverFromEUTRAPreparationRequest handoverFromEUTRAPreparationRequest;                                /* to choose, set choice to
                                * handoverFromEUTRAPreparationRequest_chosen */
                MobilityFromEUTRACommand mobilityFromEUTRACommand;  /* to
                                   * choose, set choice to
                                   * mobilityFromEUTRACommand_chosen */
                RRCConnectionReconfiguration rrcConnectionReconfiguration;  
                                        /* to choose, set choice to
                                       * rrcConnectionReconfiguration_chosen */
                RRCConnectionRelease rrcConnectionRelease;  /* to choose, set
                                     * choice to rrcConnectionRelease_chosen */
                SecurityModeCommand securityModeCommand;  /* to choose, set
                                      * choice to securityModeCommand_chosen */
                UECapabilityEnquiry ueCapabilityEnquiry;  /* to choose, set
                                      * choice to ueCapabilityEnquiry_chosen */
                CounterCheck    counterCheck;  /* to choose, set choice to
                                                * counterCheck_chosen */
                UEInformationRequest_r9 ueInformationRequest_r9;  /* to choose,
                                   * set choice to
                     * DL_DCCH_MessageType_c1_ueInformationRequest_r9_chosen */
                LoggedMeasurementConfiguration_r10 loggedMeasurementConfiguration_r10;                                  /* to choose, set choice to
          * DL_DCCH_MessageType_c1_loggedMeasurementConfiguration_r10_chosen */
                RNReconfiguration_r10 rnReconfiguration_r10;  /* to choose, set
                                   * choice to
                       * DL_DCCH_MessageType_c1_rnReconfiguration_r10_chosen */
                Nulltype        spare4;  /* to choose, set choice to
                                      * DL_DCCH_MessageType_c1_spare4_chosen */
                Nulltype        spare3;  /* to choose, set choice to
                                      * DL_DCCH_MessageType_c1_spare3_chosen */
                Nulltype        spare2;  /* to choose, set choice to
                                      * DL_DCCH_MessageType_c1_spare2_chosen */
                Nulltype        spare1;  /* to choose, set choice to
                                      * DL_DCCH_MessageType_c1_spare1_chosen */
            } u;
        } c1;  /* to choose, set choice to DL_DCCH_MessageType_c1_chosen */
        _seq2           messageClassExtension;  /* to choose, set choice to
                          * DL_DCCH_MessageType_messageClassExtension_chosen */
    } u;
} DL_DCCH_MessageType;

typedef struct DL_DCCH_Message {
    DL_DCCH_MessageType message;
} DL_DCCH_Message;

typedef struct ShortMAC_I {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} ShortMAC_I;

typedef struct ReestabUE_Identity {
    C_RNTI          c_RNTI;
    PhysCellId      physCellId;
    ShortMAC_I      shortMAC_I;
} ReestabUE_Identity;

typedef enum ReestablishmentCause {
    reconfigurationFailure = 0,
    handoverFailure = 1,
    otherFailure = 2,
    ReestablishmentCause_spare1 = 3
} ReestablishmentCause;

typedef struct RRCConnectionReestablishmentRequest_r8_IEs {
    ReestabUE_Identity ue_Identity;
    ReestablishmentCause reestablishmentCause;
    _bit1           spare;
} RRCConnectionReestablishmentRequest_r8_IEs;

typedef struct RRCConnectionReestablishmentRequest {
    struct {
        unsigned short  choice;
#           define      rrcConnectionReestablishmentRequest_r8_chosen 1
#           define      RRCConnectionReestablishmentRequest_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            RRCConnectionReestablishmentRequest_r8_IEs rrcConnectionReestablishmentRequest_r8;                          /* to choose, set choice to
                             * rrcConnectionReestablishmentRequest_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
              * RRCConnectionReestablishmentRequest_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionReestablishmentRequest;

typedef struct MMEC {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} MMEC;

typedef struct S_TMSI {
    MMEC            mmec;
    _bit1           m_TMSI;
} S_TMSI;

typedef struct InitialUE_Identity {
    unsigned short  choice;
#       define      InitialUE_Identity_s_TMSI_chosen 1
#       define      randomValue_chosen 2
    union {
        S_TMSI          s_TMSI;  /* to choose, set choice to
                                  * InitialUE_Identity_s_TMSI_chosen */
        _bit1           randomValue;  /* to choose, set choice to
                                       * randomValue_chosen */
    } u;
} InitialUE_Identity;

typedef enum EstablishmentCause {
    emergency = 0,
    highPriorityAccess = 1,
    mt_Access = 2,
    mo_Signalling = 3,
    mo_Data = 4,
    delayTolerantAccess_v1020 = 5,
    EstablishmentCause_spare2 = 6,
    EstablishmentCause_spare1 = 7
} EstablishmentCause;

typedef struct RRCConnectionRequest_r8_IEs {
    InitialUE_Identity ue_Identity;
    EstablishmentCause establishmentCause;
    _bit1           spare;
} RRCConnectionRequest_r8_IEs;

typedef struct RRCConnectionRequest {
    struct {
        unsigned short  choice;
#           define      rrcConnectionRequest_r8_chosen 1
#           define      RRCConnectionRequest_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            RRCConnectionRequest_r8_IEs rrcConnectionRequest_r8;  /* to choose,
                                   * set choice to
                                   * rrcConnectionRequest_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * RRCConnectionRequest_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionRequest;

typedef struct UL_CCCH_MessageType {
    unsigned short  choice;
#       define      UL_CCCH_MessageType_c1_chosen 1
#       define      UL_CCCH_MessageType_messageClassExtension_chosen 2
    union {
        struct _choice30 {
            unsigned short  choice;
#               define      rrcConnectionReestablishmentRequest_chosen 1
#               define      rrcConnectionRequest_chosen 2
            union {
                RRCConnectionReestablishmentRequest rrcConnectionReestablishmentRequest;                                /* to choose, set choice to
                                * rrcConnectionReestablishmentRequest_chosen */
                RRCConnectionRequest rrcConnectionRequest;  /* to choose, set
                                     * choice to rrcConnectionRequest_chosen */
            } u;
        } c1;  /* to choose, set choice to UL_CCCH_MessageType_c1_chosen */
        _seq2           messageClassExtension;  /* to choose, set choice to
                          * UL_CCCH_MessageType_messageClassExtension_chosen */
    } u;
} UL_CCCH_MessageType;

typedef struct UL_CCCH_Message {
    UL_CCCH_MessageType message;
} UL_CCCH_Message;

typedef struct CSFBParametersRequestCDMA2000_v8a0_IEs {
    unsigned char   bit_mask;
#       define      CSFBParametersRequestCDMA2000_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      CSFBParametersRequestCDMA2000_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
   * CSFBParametersRequestCDMA2000_v8a0_IEs_lateNonCriticalExtension_present if
   * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
       * CSFBParametersRequestCDMA2000_v8a0_IEs_nonCriticalExtension_present if
       * present */
} CSFBParametersRequestCDMA2000_v8a0_IEs;

typedef struct CSFBParametersRequestCDMA2000_r8_IEs {
    unsigned char   bit_mask;
#       define      CSFBParametersRequestCDMA2000_r8_IEs_nonCriticalExtension_present 0x80
    CSFBParametersRequestCDMA2000_v8a0_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
         * CSFBParametersRequestCDMA2000_r8_IEs_nonCriticalExtension_present if
         * present */
} CSFBParametersRequestCDMA2000_r8_IEs;

typedef struct CSFBParametersRequestCDMA2000 {
    struct {
        unsigned short  choice;
#           define      csfbParametersRequestCDMA2000_r8_chosen 1
#           define      CSFBParametersRequestCDMA2000_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            CSFBParametersRequestCDMA2000_r8_IEs csfbParametersRequestCDMA2000_r8;                                      /* to choose, set choice to
                                   * csfbParametersRequestCDMA2000_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
        * CSFBParametersRequestCDMA2000_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} CSFBParametersRequestCDMA2000;

typedef unsigned short  MeasId;

typedef struct MeasResultsCDMA2000 {
    ossBoolean      preRegistrationStatusHRPD;
    struct MeasResultListCDMA2000 *measResultListCDMA2000;
} MeasResultsCDMA2000;

typedef struct MeasResultForECID_r9 {
    unsigned short  ue_RxTxTimeDiffResult_r9;
    _bit1           currentSFN_r9;
} MeasResultForECID_r9;

typedef struct LocationInfo_r10 {
    unsigned char   bit_mask;
#       define      horizontalVelocity_r10_present 0x80
#       define      gnss_TOD_msec_r10_present 0x40
    struct {
        unsigned short  choice;
#           define      ellipsoid_Point_r10_chosen 1
#           define      ellipsoidPointWithAltitude_r10_chosen 2
#           define      ellipsoidPointWithUncertaintyCircle_r11_chosen 3
#           define      ellipsoidPointWithUncertaintyEllipse_r11_chosen 4
#           define      ellipsoidPointWithAltitudeAndUncertaintyEllipsoid_r11_chosen 5
#           define      ellipsoidArc_r11_chosen 6
#           define      polygon_r11_chosen 7
        union {
            _octet1         ellipsoid_Point_r10;  /* to choose, set choice to
                                                * ellipsoid_Point_r10_chosen */
            _octet1         ellipsoidPointWithAltitude_r10;  /* to choose, set
                                   * choice to
                                   * ellipsoidPointWithAltitude_r10_chosen */
            _octet1         ellipsoidPointWithUncertaintyCircle_r11;  
                                  /* extension #1; to choose, set choice to
                            * ellipsoidPointWithUncertaintyCircle_r11_chosen */
            _octet1         ellipsoidPointWithUncertaintyEllipse_r11;  
                                  /* extension #2; to choose, set choice to
                           * ellipsoidPointWithUncertaintyEllipse_r11_chosen */
            _octet1         ellipsoidPointWithAltitudeAndUncertaintyEllipsoid_r11;                                      /* extension #3; to choose, set choice
                                   * to
              * ellipsoidPointWithAltitudeAndUncertaintyEllipsoid_r11_chosen */
            _octet1         ellipsoidArc_r11;  /* extension #4; to choose, set
                                                * choice to
                                                * ellipsoidArc_r11_chosen */
            _octet1         polygon_r11;  /* extension #5; to choose, set choice
                                           * to polygon_r11_chosen */
        } u;
    } locationCoordinates_r10;
    _octet1         horizontalVelocity_r10;  /* optional; set in bit_mask
                                              * horizontalVelocity_r10_present
                                              * if present */
    _octet1         gnss_TOD_msec_r10;  /* optional; set in bit_mask
                                         * gnss_TOD_msec_r10_present if
                                         * present */
} LocationInfo_r10;

typedef unsigned short  MeasId_v1250;

typedef short           RSRQ_Range_v1250;

typedef struct MeasResults {
    unsigned char   bit_mask;
#       define      measResultNeighCells_present 0x80
#       define      measResultForECID_r9_present 0x40
#       define      MeasResults_locationInfo_r10_present 0x20
#       define      measResultServFreqList_r10_present 0x10
#       define      MeasResults_measId_v1250_present 0x08
#       define      measResultPCell_v1250_present 0x04
#       define      measResultCSI_RS_List_r12_present 0x02
    MeasId          measId;
    struct {
        RSRP_Range      rsrpResult;
        RSRQ_Range      rsrqResult;
    } measResultPCell;
    struct {
        unsigned short  choice;
#           define      measResultListEUTRA_chosen 1
#           define      measResultListUTRA_chosen 2
#           define      measResultListGERAN_chosen 3
#           define      measResultsCDMA2000_chosen 4
        union {
            struct MeasResultListEUTRA *measResultListEUTRA;  /* to choose, set
                                      * choice to measResultListEUTRA_chosen */
            struct MeasResultListUTRA *measResultListUTRA;  /* to choose, set
                                       * choice to measResultListUTRA_chosen */
            struct MeasResultListGERAN *measResultListGERAN;  /* to choose, set
                                      * choice to measResultListGERAN_chosen */
            MeasResultsCDMA2000 measResultsCDMA2000;  /* to choose, set choice
                                             * to measResultsCDMA2000_chosen */
        } u;
    } measResultNeighCells;  /* optional; set in bit_mask
                              * measResultNeighCells_present if present */
    MeasResultForECID_r9 measResultForECID_r9;  /* extension #1; optional; set
                                   * in bit_mask measResultForECID_r9_present if
                                   * present */
    LocationInfo_r10 locationInfo_r10;  /* extension #2; optional; set in
                                   * bit_mask
                                   * MeasResults_locationInfo_r10_present if
                                   * present */
    struct MeasResultServFreqList_r10 *measResultServFreqList_r10;  
                                  /* extension #2; optional; set in bit_mask
                                   * measResultServFreqList_r10_present if
                                   * present */
    MeasId_v1250    measId_v1250;  /* extension #3; optional; set in bit_mask
                                    * MeasResults_measId_v1250_present if
                                    * present */
    RSRQ_Range_v1250 measResultPCell_v1250;  /* extension #3; optional; set in
                                   * bit_mask measResultPCell_v1250_present if
                                   * present */
    struct MeasResultCSI_RS_List_r12 *measResultCSI_RS_List_r12;  /* extension
                                   * #3; optional; set in bit_mask
                                   * measResultCSI_RS_List_r12_present if
                                   * present */
} MeasResults;

typedef struct MeasurementReport_v8a0_IEs {
    unsigned char   bit_mask;
#       define      MeasurementReport_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      MeasurementReport_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
               * MeasurementReport_v8a0_IEs_lateNonCriticalExtension_present if
               * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                   * MeasurementReport_v8a0_IEs_nonCriticalExtension_present if
                   * present */
} MeasurementReport_v8a0_IEs;

typedef struct MeasurementReport_r8_IEs {
    unsigned char   bit_mask;
#       define      MeasurementReport_r8_IEs_nonCriticalExtension_present 0x80
    MeasResults     measResults;
    MeasurementReport_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                     * MeasurementReport_r8_IEs_nonCriticalExtension_present if
                     * present */
} MeasurementReport_r8_IEs;

typedef struct MeasurementReport {
    struct {
        unsigned short  choice;
#           define      MeasurementReport_criticalExtensions_c1_chosen 1
#           define      MeasurementReport_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice31 {
                unsigned short  choice;
#                   define      measurementReport_r8_chosen 1
#                   define      MeasurementReport_criticalExtensions_c1_spare7_chosen 2
#                   define      MeasurementReport_criticalExtensions_c1_spare6_chosen 3
#                   define      MeasurementReport_criticalExtensions_c1_spare5_chosen 4
#                   define      MeasurementReport_criticalExtensions_c1_spare4_chosen 5
#                   define      MeasurementReport_criticalExtensions_c1_spare3_chosen 6
#                   define      MeasurementReport_criticalExtensions_c1_spare2_chosen 7
#                   define      MeasurementReport_criticalExtensions_c1_spare1_chosen 8
                union {
                    MeasurementReport_r8_IEs measurementReport_r8;  /* to
                                   * choose, set choice to
                                   * measurementReport_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
                     * MeasurementReport_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
                     * MeasurementReport_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
                     * MeasurementReport_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
                     * MeasurementReport_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                     * MeasurementReport_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                     * MeasurementReport_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                     * MeasurementReport_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * MeasurementReport_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
      * MeasurementReport_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} MeasurementReport;

typedef struct RRCConnectionReconfigurationComplete_v1250_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReconfigurationComplete_v1250_IEs_logMeasAvailableMBSFN_r12_present 0x80
#       define      RRCConnectionReconfigurationComplete_v1250_IEs_nonCriticalExtension_present 0x40
    _enum3          logMeasAvailableMBSFN_r12;  /* optional; set in bit_mask
        * RRCConnectionReconfigurationComplete_v1250_IEs_logMeasAvailableMBSFN_r12_present if
        * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
   * RRCConnectionReconfigurationComplete_v1250_IEs_nonCriticalExtension_present if
   * present */
} RRCConnectionReconfigurationComplete_v1250_IEs;

typedef struct RRCConnectionReconfigurationComplete_v1130_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReconfigurationComplete_v1130_IEs_connEstFailInfoAvailable_r11_present 0x80
#       define      RRCConnectionReconfigurationComplete_v1130_IEs_nonCriticalExtension_present 0x40
    _enum3          connEstFailInfoAvailable_r11;  /* optional; set in bit_mask
           * RRCConnectionReconfigurationComplete_v1130_IEs_connEstFailInfoAvailable_r11_present if
           * present */
    RRCConnectionReconfigurationComplete_v1250_IEs nonCriticalExtension;  
                                        /* optional; set in bit_mask
   * RRCConnectionReconfigurationComplete_v1130_IEs_nonCriticalExtension_present if
   * present */
} RRCConnectionReconfigurationComplete_v1130_IEs;

typedef struct RRCConnectionReconfigurationComplete_v1020_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReconfigurationComplete_v1020_IEs_rlf_InfoAvailable_r10_present 0x80
#       define      RRCConnectionReconfigurationComplete_v1020_IEs_logMeasAvailable_r10_present 0x40
#       define      RRCConnectionReconfigurationComplete_v1020_IEs_nonCriticalExtension_present 0x20
    _enum3          rlf_InfoAvailable_r10;  /* optional; set in bit_mask
    * RRCConnectionReconfigurationComplete_v1020_IEs_rlf_InfoAvailable_r10_present if
    * present */
    _enum3          logMeasAvailable_r10;  /* optional; set in bit_mask
   * RRCConnectionReconfigurationComplete_v1020_IEs_logMeasAvailable_r10_present if
   * present */
    RRCConnectionReconfigurationComplete_v1130_IEs nonCriticalExtension;  
                                        /* optional; set in bit_mask
   * RRCConnectionReconfigurationComplete_v1020_IEs_nonCriticalExtension_present if
   * present */
} RRCConnectionReconfigurationComplete_v1020_IEs;

typedef struct RRCConnectionReconfigurationComplete_v8a0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReconfigurationComplete_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionReconfigurationComplete_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
      * RRCConnectionReconfigurationComplete_v8a0_IEs_lateNonCriticalExtension_present if
      * present */
    RRCConnectionReconfigurationComplete_v1020_IEs nonCriticalExtension;  
                                        /* optional; set in bit_mask
  * RRCConnectionReconfigurationComplete_v8a0_IEs_nonCriticalExtension_present if
  * present */
} RRCConnectionReconfigurationComplete_v8a0_IEs;

typedef struct RRCConnectionReconfigurationComplete_r8_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReconfigurationComplete_r8_IEs_nonCriticalExtension_present 0x80
    RRCConnectionReconfigurationComplete_v8a0_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
  * RRCConnectionReconfigurationComplete_r8_IEs_nonCriticalExtension_present if
  * present */
} RRCConnectionReconfigurationComplete_r8_IEs;

typedef struct RRCConnectionReconfigurationComplete {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      rrcConnectionReconfigurationComplete_r8_chosen 1
#           define      RRCConnectionReconfigurationComplete_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            RRCConnectionReconfigurationComplete_r8_IEs rrcConnectionReconfigurationComplete_r8;                        /* to choose, set choice to
                            * rrcConnectionReconfigurationComplete_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
               * RRCConnectionReconfigurationComplete_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionReconfigurationComplete;

typedef struct RRCConnectionReestablishmentComplete_v1250_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishmentComplete_v1250_IEs_logMeasAvailableMBSFN_r12_present 0x80
#       define      RRCConnectionReestablishmentComplete_v1250_IEs_nonCriticalExtension_present 0x40
    _enum3          logMeasAvailableMBSFN_r12;  /* optional; set in bit_mask
        * RRCConnectionReestablishmentComplete_v1250_IEs_logMeasAvailableMBSFN_r12_present if
        * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
   * RRCConnectionReestablishmentComplete_v1250_IEs_nonCriticalExtension_present if
   * present */
} RRCConnectionReestablishmentComplete_v1250_IEs;

typedef struct RRCConnectionReestablishmentComplete_v1130_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishmentComplete_v1130_IEs_connEstFailInfoAvailable_r11_present 0x80
#       define      RRCConnectionReestablishmentComplete_v1130_IEs_nonCriticalExtension_present 0x40
    _enum3          connEstFailInfoAvailable_r11;  /* optional; set in bit_mask
           * RRCConnectionReestablishmentComplete_v1130_IEs_connEstFailInfoAvailable_r11_present if
           * present */
    RRCConnectionReestablishmentComplete_v1250_IEs nonCriticalExtension;  
                                        /* optional; set in bit_mask
   * RRCConnectionReestablishmentComplete_v1130_IEs_nonCriticalExtension_present if
   * present */
} RRCConnectionReestablishmentComplete_v1130_IEs;

typedef struct RRCConnectionReestablishmentComplete_v1020_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishmentComplete_v1020_IEs_logMeasAvailable_r10_present 0x80
#       define      RRCConnectionReestablishmentComplete_v1020_IEs_nonCriticalExtension_present 0x40
    _enum3          logMeasAvailable_r10;  /* optional; set in bit_mask
   * RRCConnectionReestablishmentComplete_v1020_IEs_logMeasAvailable_r10_present if
   * present */
    RRCConnectionReestablishmentComplete_v1130_IEs nonCriticalExtension;  
                                        /* optional; set in bit_mask
   * RRCConnectionReestablishmentComplete_v1020_IEs_nonCriticalExtension_present if
   * present */
} RRCConnectionReestablishmentComplete_v1020_IEs;

typedef struct RRCConnectionReestablishmentComplete_v8a0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishmentComplete_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionReestablishmentComplete_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
      * RRCConnectionReestablishmentComplete_v8a0_IEs_lateNonCriticalExtension_present if
      * present */
    RRCConnectionReestablishmentComplete_v1020_IEs nonCriticalExtension;  
                                        /* optional; set in bit_mask
  * RRCConnectionReestablishmentComplete_v8a0_IEs_nonCriticalExtension_present if
  * present */
} RRCConnectionReestablishmentComplete_v8a0_IEs;

typedef struct RRCConnectionReestablishmentComplete_v920_IEs {
    unsigned char   bit_mask;
#       define      rlf_InfoAvailable_r9_present 0x80
#       define      RRCConnectionReestablishmentComplete_v920_IEs_nonCriticalExtension_present 0x40
    _enum3          rlf_InfoAvailable_r9;  /* optional; set in bit_mask
                                            * rlf_InfoAvailable_r9_present if
                                            * present */
    RRCConnectionReestablishmentComplete_v8a0_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
  * RRCConnectionReestablishmentComplete_v920_IEs_nonCriticalExtension_present if
  * present */
} RRCConnectionReestablishmentComplete_v920_IEs;

typedef struct RRCConnectionReestablishmentComplete_r8_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionReestablishmentComplete_r8_IEs_nonCriticalExtension_present 0x80
    RRCConnectionReestablishmentComplete_v920_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
  * RRCConnectionReestablishmentComplete_r8_IEs_nonCriticalExtension_present if
  * present */
} RRCConnectionReestablishmentComplete_r8_IEs;

typedef struct RRCConnectionReestablishmentComplete {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      rrcConnectionReestablishmentComplete_r8_chosen 1
#           define      RRCConnectionReestablishmentComplete_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            RRCConnectionReestablishmentComplete_r8_IEs rrcConnectionReestablishmentComplete_r8;                        /* to choose, set choice to
                            * rrcConnectionReestablishmentComplete_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
               * RRCConnectionReestablishmentComplete_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionReestablishmentComplete;

typedef struct RegisteredMME {
    unsigned char   bit_mask;
#       define      plmn_Identity_present 0x80
    PLMN_Identity   plmn_Identity;  /* optional; set in bit_mask
                                     * plmn_Identity_present if present */
    _bit1           mmegi;
    MMEC            mmec;
} RegisteredMME;

typedef struct RRCConnectionSetupComplete_v1250_IEs {
    unsigned char   bit_mask;
#       define      mobilityState_r12_present 0x80
#       define      mobilityHistoryAvail_r12_present 0x40
#       define      RRCConnectionSetupComplete_v1250_IEs_logMeasAvailableMBSFN_r12_present 0x20
#       define      RRCConnectionSetupComplete_v1250_IEs_nonCriticalExtension_present 0x10
    enum {
        mobilityState_r12_normal = 0,
        medium = 1,
        high = 2,
        mobilityState_r12_spare = 3
    } mobilityState_r12;  /* optional; set in bit_mask mobilityState_r12_present
                           * if present */
    _enum3          mobilityHistoryAvail_r12;  /* optional; set in bit_mask
                                          * mobilityHistoryAvail_r12_present if
                                          * present */
    _enum3          logMeasAvailableMBSFN_r12;  /* optional; set in bit_mask
    * RRCConnectionSetupComplete_v1250_IEs_logMeasAvailableMBSFN_r12_present if
    * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
         * RRCConnectionSetupComplete_v1250_IEs_nonCriticalExtension_present if
         * present */
} RRCConnectionSetupComplete_v1250_IEs;

typedef struct RRCConnectionSetupComplete_v1130_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionSetupComplete_v1130_IEs_connEstFailInfoAvailable_r11_present 0x80
#       define      RRCConnectionSetupComplete_v1130_IEs_nonCriticalExtension_present 0x40
    _enum3          connEstFailInfoAvailable_r11;  /* optional; set in bit_mask
 * RRCConnectionSetupComplete_v1130_IEs_connEstFailInfoAvailable_r11_present if
 * present */
    RRCConnectionSetupComplete_v1250_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
         * RRCConnectionSetupComplete_v1130_IEs_nonCriticalExtension_present if
         * present */
} RRCConnectionSetupComplete_v1130_IEs;

typedef struct RRCConnectionSetupComplete_v1020_IEs {
    unsigned char   bit_mask;
#       define      gummei_Type_r10_present 0x80
#       define      RRCConnectionSetupComplete_v1020_IEs_rlf_InfoAvailable_r10_present 0x40
#       define      RRCConnectionSetupComplete_v1020_IEs_logMeasAvailable_r10_present 0x20
#       define      rn_SubframeConfigReq_r10_present 0x10
#       define      RRCConnectionSetupComplete_v1020_IEs_nonCriticalExtension_present 0x08
    enum {
        native = 0,
        mapped = 1
    } gummei_Type_r10;  /* optional; set in bit_mask gummei_Type_r10_present if
                         * present */
    _enum3          rlf_InfoAvailable_r10;  /* optional; set in bit_mask
        * RRCConnectionSetupComplete_v1020_IEs_rlf_InfoAvailable_r10_present if
        * present */
    _enum3          logMeasAvailable_r10;  /* optional; set in bit_mask
         * RRCConnectionSetupComplete_v1020_IEs_logMeasAvailable_r10_present if
         * present */
    enum {
        required = 0,
        notRequired = 1
    } rn_SubframeConfigReq_r10;  /* optional; set in bit_mask
                                  * rn_SubframeConfigReq_r10_present if
                                  * present */
    RRCConnectionSetupComplete_v1130_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
         * RRCConnectionSetupComplete_v1020_IEs_nonCriticalExtension_present if
         * present */
} RRCConnectionSetupComplete_v1020_IEs;

typedef struct RRCConnectionSetupComplete_v8a0_IEs {
    unsigned char   bit_mask;
#       define      RRCConnectionSetupComplete_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      RRCConnectionSetupComplete_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
      * RRCConnectionSetupComplete_v8a0_IEs_lateNonCriticalExtension_present if
      * present */
    RRCConnectionSetupComplete_v1020_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
          * RRCConnectionSetupComplete_v8a0_IEs_nonCriticalExtension_present if
          * present */
} RRCConnectionSetupComplete_v8a0_IEs;

typedef struct RRCConnectionSetupComplete_r8_IEs {
    unsigned char   bit_mask;
#       define      registeredMME_present 0x80
#       define      RRCConnectionSetupComplete_r8_IEs_nonCriticalExtension_present 0x40
    unsigned short  selectedPLMN_Identity;
    RegisteredMME   registeredMME;  /* optional; set in bit_mask
                                     * registeredMME_present if present */
    DedicatedInfoNAS dedicatedInfoNAS;
    RRCConnectionSetupComplete_v8a0_IEs nonCriticalExtension;  /* optional; set
                                   * in bit_mask
            * RRCConnectionSetupComplete_r8_IEs_nonCriticalExtension_present if
            * present */
} RRCConnectionSetupComplete_r8_IEs;

typedef struct RRCConnectionSetupComplete {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      RRCConnectionSetupComplete_criticalExtensions_c1_chosen 1
#           define      RRCConnectionSetupComplete_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice32 {
                unsigned short  choice;
#                   define      rrcConnectionSetupComplete_r8_chosen 1
#                   define      RRCConnectionSetupComplete_criticalExtensions_c1_spare3_chosen 2
#                   define      RRCConnectionSetupComplete_criticalExtensions_c1_spare2_chosen 3
#                   define      RRCConnectionSetupComplete_criticalExtensions_c1_spare1_chosen 4
                union {
                    RRCConnectionSetupComplete_r8_IEs rrcConnectionSetupComplete_r8;                                    /* to choose, set choice to
                                      * rrcConnectionSetupComplete_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
            * RRCConnectionSetupComplete_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
            * RRCConnectionSetupComplete_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
            * RRCConnectionSetupComplete_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                   * RRCConnectionSetupComplete_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
     * RRCConnectionSetupComplete_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RRCConnectionSetupComplete;

typedef struct SecurityModeComplete_v8a0_IEs {
    unsigned char   bit_mask;
#       define      SecurityModeComplete_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      SecurityModeComplete_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
            * SecurityModeComplete_v8a0_IEs_lateNonCriticalExtension_present if
            * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * SecurityModeComplete_v8a0_IEs_nonCriticalExtension_present if
                * present */
} SecurityModeComplete_v8a0_IEs;

typedef struct SecurityModeComplete_r8_IEs {
    unsigned char   bit_mask;
#       define      SecurityModeComplete_r8_IEs_nonCriticalExtension_present 0x80
    SecurityModeComplete_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                  * SecurityModeComplete_r8_IEs_nonCriticalExtension_present if
                  * present */
} SecurityModeComplete_r8_IEs;

typedef struct SecurityModeComplete {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      securityModeComplete_r8_chosen 1
#           define      SecurityModeComplete_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            SecurityModeComplete_r8_IEs securityModeComplete_r8;  /* to choose,
                                   * set choice to
                                   * securityModeComplete_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * SecurityModeComplete_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SecurityModeComplete;

typedef struct SecurityModeFailure_v8a0_IEs {
    unsigned char   bit_mask;
#       define      SecurityModeFailure_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      SecurityModeFailure_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
             * SecurityModeFailure_v8a0_IEs_lateNonCriticalExtension_present if
             * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                 * SecurityModeFailure_v8a0_IEs_nonCriticalExtension_present if
                 * present */
} SecurityModeFailure_v8a0_IEs;

typedef struct SecurityModeFailure_r8_IEs {
    unsigned char   bit_mask;
#       define      SecurityModeFailure_r8_IEs_nonCriticalExtension_present 0x80
    SecurityModeFailure_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                   * SecurityModeFailure_r8_IEs_nonCriticalExtension_present if
                   * present */
} SecurityModeFailure_r8_IEs;

typedef struct SecurityModeFailure {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      securityModeFailure_r8_chosen 1
#           define      SecurityModeFailure_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            SecurityModeFailure_r8_IEs securityModeFailure_r8;  /* to choose,
                                   * set choice to
                                   * securityModeFailure_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
    * SecurityModeFailure_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SecurityModeFailure;

typedef struct UE_RadioPagingInfo_r12 {
    unsigned char   bit_mask;
#       define      ue_Category_v1250_present 0x80
    unsigned short  ue_Category_v1250;  /* optional; set in bit_mask
                                         * ue_Category_v1250_present if
                                         * present */
} UE_RadioPagingInfo_r12;

typedef struct UECapabilityInformation_v1250_IEs {
    unsigned char   bit_mask;
#       define      ue_RadioPagingInfo_r12_present 0x80
#       define      UECapabilityInformation_v1250_IEs_nonCriticalExtension_present 0x40
    UE_RadioPagingInfo_r12 ue_RadioPagingInfo_r12;  /* optional; set in bit_mask
                                            * ue_RadioPagingInfo_r12_present if
                                            * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
            * UECapabilityInformation_v1250_IEs_nonCriticalExtension_present if
            * present */
} UECapabilityInformation_v1250_IEs;

typedef struct UECapabilityInformation_v8a0_IEs {
    unsigned char   bit_mask;
#       define      UECapabilityInformation_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      UECapabilityInformation_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
         * UECapabilityInformation_v8a0_IEs_lateNonCriticalExtension_present if
         * present */
    UECapabilityInformation_v1250_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
             * UECapabilityInformation_v8a0_IEs_nonCriticalExtension_present if
             * present */
} UECapabilityInformation_v8a0_IEs;

typedef struct UECapabilityInformation_r8_IEs {
    unsigned char   bit_mask;
#       define      UECapabilityInformation_r8_IEs_nonCriticalExtension_present 0x80
    struct UE_CapabilityRAT_ContainerList *ue_CapabilityRAT_ContainerList;
    UECapabilityInformation_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
               * UECapabilityInformation_r8_IEs_nonCriticalExtension_present if
               * present */
} UECapabilityInformation_r8_IEs;

typedef struct UECapabilityInformation {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      UECapabilityInformation_criticalExtensions_c1_chosen 1
#           define      UECapabilityInformation_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice33 {
                unsigned short  choice;
#                   define      ueCapabilityInformation_r8_chosen 1
#                   define      UECapabilityInformation_criticalExtensions_c1_spare7_chosen 2
#                   define      UECapabilityInformation_criticalExtensions_c1_spare6_chosen 3
#                   define      UECapabilityInformation_criticalExtensions_c1_spare5_chosen 4
#                   define      UECapabilityInformation_criticalExtensions_c1_spare4_chosen 5
#                   define      UECapabilityInformation_criticalExtensions_c1_spare3_chosen 6
#                   define      UECapabilityInformation_criticalExtensions_c1_spare2_chosen 7
#                   define      UECapabilityInformation_criticalExtensions_c1_spare1_chosen 8
                union {
                    UECapabilityInformation_r8_IEs ueCapabilityInformation_r8;                                          /* to choose, set choice to
                                         * ueCapabilityInformation_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
               * UECapabilityInformation_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
               * UECapabilityInformation_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
               * UECapabilityInformation_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
               * UECapabilityInformation_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
               * UECapabilityInformation_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
               * UECapabilityInformation_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
               * UECapabilityInformation_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * UECapabilityInformation_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
  * UECapabilityInformation_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} UECapabilityInformation;

typedef struct ULHandoverPreparationTransfer_v8a0_IEs {
    unsigned char   bit_mask;
#       define      ULHandoverPreparationTransfer_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      ULHandoverPreparationTransfer_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
   * ULHandoverPreparationTransfer_v8a0_IEs_lateNonCriticalExtension_present if
   * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
       * ULHandoverPreparationTransfer_v8a0_IEs_nonCriticalExtension_present if
       * present */
} ULHandoverPreparationTransfer_v8a0_IEs;

typedef struct ULHandoverPreparationTransfer_r8_IEs {
    unsigned char   bit_mask;
#       define      meid_present 0x80
#       define      ULHandoverPreparationTransfer_r8_IEs_nonCriticalExtension_present 0x40
    CDMA2000_Type   cdma2000_Type;
    _bit1           meid;  /* optional; set in bit_mask meid_present if
                            * present */
    DedicatedInfoCDMA2000 dedicatedInfo;
    ULHandoverPreparationTransfer_v8a0_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
         * ULHandoverPreparationTransfer_r8_IEs_nonCriticalExtension_present if
         * present */
} ULHandoverPreparationTransfer_r8_IEs;

typedef struct ULHandoverPreparationTransfer {
    struct {
        unsigned short  choice;
#           define      ULHandoverPreparationTransfer_criticalExtensions_c1_chosen 1
#           define      ULHandoverPreparationTransfer_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice34 {
                unsigned short  choice;
#                   define      ulHandoverPreparationTransfer_r8_chosen 1
#                   define      ULHandoverPreparationTransfer_criticalExtensions_c1_spare3_chosen 2
#                   define      ULHandoverPreparationTransfer_criticalExtensions_c1_spare2_chosen 3
#                   define      ULHandoverPreparationTransfer_criticalExtensions_c1_spare1_chosen 4
                union {
                    ULHandoverPreparationTransfer_r8_IEs ulHandoverPreparationTransfer_r8;                              /* to choose, set choice to
                                   * ulHandoverPreparationTransfer_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
         * ULHandoverPreparationTransfer_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
         * ULHandoverPreparationTransfer_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
         * ULHandoverPreparationTransfer_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                * ULHandoverPreparationTransfer_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
        * ULHandoverPreparationTransfer_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} ULHandoverPreparationTransfer;

typedef struct ULInformationTransfer_v8a0_IEs {
    unsigned char   bit_mask;
#       define      ULInformationTransfer_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      ULInformationTransfer_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
           * ULInformationTransfer_v8a0_IEs_lateNonCriticalExtension_present if
           * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
               * ULInformationTransfer_v8a0_IEs_nonCriticalExtension_present if
               * present */
} ULInformationTransfer_v8a0_IEs;

typedef struct ULInformationTransfer_r8_IEs {
    unsigned char   bit_mask;
#       define      ULInformationTransfer_r8_IEs_nonCriticalExtension_present 0x80
    _choice12       dedicatedInfoType;
    ULInformationTransfer_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * ULInformationTransfer_r8_IEs_nonCriticalExtension_present if
                 * present */
} ULInformationTransfer_r8_IEs;

typedef struct ULInformationTransfer {
    struct {
        unsigned short  choice;
#           define      ULInformationTransfer_criticalExtensions_c1_chosen 1
#           define      ULInformationTransfer_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice35 {
                unsigned short  choice;
#                   define      ulInformationTransfer_r8_chosen 1
#                   define      ULInformationTransfer_criticalExtensions_c1_spare3_chosen 2
#                   define      ULInformationTransfer_criticalExtensions_c1_spare2_chosen 3
#                   define      ULInformationTransfer_criticalExtensions_c1_spare1_chosen 4
                union {
                    ULInformationTransfer_r8_IEs ulInformationTransfer_r8;  
                                        /* to choose, set choice to
                                         * ulInformationTransfer_r8_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                 * ULInformationTransfer_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                 * ULInformationTransfer_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                 * ULInformationTransfer_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * ULInformationTransfer_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
  * ULInformationTransfer_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} ULInformationTransfer;

typedef struct CounterCheckResponse_v8a0_IEs {
    unsigned char   bit_mask;
#       define      CounterCheckResponse_v8a0_IEs_lateNonCriticalExtension_present 0x80
#       define      CounterCheckResponse_v8a0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
            * CounterCheckResponse_v8a0_IEs_lateNonCriticalExtension_present if
            * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * CounterCheckResponse_v8a0_IEs_nonCriticalExtension_present if
                * present */
} CounterCheckResponse_v8a0_IEs;

typedef struct CounterCheckResponse_r8_IEs {
    unsigned char   bit_mask;
#       define      CounterCheckResponse_r8_IEs_nonCriticalExtension_present 0x80
    struct DRB_CountInfoList *drb_CountInfoList;
    CounterCheckResponse_v8a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                  * CounterCheckResponse_r8_IEs_nonCriticalExtension_present if
                  * present */
} CounterCheckResponse_r8_IEs;

typedef struct CounterCheckResponse {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      counterCheckResponse_r8_chosen 1
#           define      CounterCheckResponse_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            CounterCheckResponse_r8_IEs counterCheckResponse_r8;  /* to choose,
                                   * set choice to
                                   * counterCheckResponse_r8_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * CounterCheckResponse_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} CounterCheckResponse;

typedef unsigned short  NumberOfPreamblesSent_r11;

typedef struct CellGlobalIdEUTRA {
    PLMN_Identity   plmn_Identity;
    CellIdentity    cellIdentity;
} CellGlobalIdEUTRA;

typedef unsigned int    TimeSinceFailure_r11;

typedef unsigned short  PhysCellIdUTRA_FDD;

typedef unsigned short  PhysCellIdUTRA_TDD;

typedef struct CellGlobalIdUTRA {
    PLMN_Identity   plmn_Identity;
    _bit1           cellIdentity;
} CellGlobalIdUTRA;

typedef struct RSRQ_Type_r12 {
    ossBoolean      allSymbols_r12;
    ossBoolean      wideBand_r12;
} RSRQ_Type_r12;

typedef struct _choice36 {
    unsigned short  choice;
#       define      fdd_r11_chosen 1
#       define      tdd_r11_chosen 2
    union {
        PhysCellIdUTRA_FDD fdd_r11;  /* to choose, set choice to
                                      * fdd_r11_chosen */
        PhysCellIdUTRA_TDD tdd_r11;  /* to choose, set choice to
                                      * tdd_r11_chosen */
    } u;
} _choice36;

typedef struct RLF_Report_r9 {
    unsigned short  bit_mask;
#       define      measResultNeighCells_r9_present 0x8000
#       define      RLF_Report_r9_locationInfo_r10_present 0x4000
#       define      failedPCellId_r10_present 0x2000
#       define      reestablishmentCellId_r10_present 0x1000
#       define      timeConnFailure_r10_present 0x0800
#       define      connectionFailureType_r10_present 0x0400
#       define      previousPCellId_r10_present 0x0200
#       define      failedPCellId_v1090_present 0x0100
#       define      basicFields_r11_present 0x0080
#       define      previousUTRA_CellId_r11_present 0x0040
#       define      selectedUTRA_CellId_r11_present 0x0020
#       define      failedPCellId_v1250_present 0x0010
#       define      measResultLastServCell_v1250_present 0x0008
#       define      lastServCellRSRQ_Type_r12_present 0x0004
#       define      RLF_Report_r9_measResultListEUTRA_v1250_present 0x0002
    struct {
        unsigned char   bit_mask;
#           define      rsrqResult_r9_present 0x80
        RSRP_Range      rsrpResult_r9;
        RSRQ_Range      rsrqResult_r9;  /* optional; set in bit_mask
                                         * rsrqResult_r9_present if present */
    } measResultLastServCell_r9;
    struct {
        unsigned char   bit_mask;
#           define      measResultListEUTRA_r9_present 0x80
#           define      measResultListUTRA_r9_present 0x40
#           define      measResultListGERAN_r9_present 0x20
#           define      measResultsCDMA2000_r9_present 0x10
        struct MeasResultList2EUTRA_r9 *measResultListEUTRA_r9;  /* optional;
                                   * set in bit_mask
                                   * measResultListEUTRA_r9_present if
                                   * present */
        struct MeasResultList2UTRA_r9 *measResultListUTRA_r9;  /* optional; set
                                   * in bit_mask measResultListUTRA_r9_present
                                   * if present */
        struct MeasResultListGERAN *measResultListGERAN_r9;  /* optional; set in
                                   * bit_mask measResultListGERAN_r9_present if
                                   * present */
        struct MeasResultList2CDMA2000_r9 *measResultsCDMA2000_r9;  
                                  /* optional; set in bit_mask
                                   * measResultsCDMA2000_r9_present if
                                   * present */
    } measResultNeighCells_r9;  /* optional; set in bit_mask
                                 * measResultNeighCells_r9_present if present */
    LocationInfo_r10 locationInfo_r10;  /* extension #1; optional; set in
                                   * bit_mask
                                   * RLF_Report_r9_locationInfo_r10_present if
                                   * present */
    struct {
        unsigned short  choice;
#           define      cellGlobalId_r10_chosen 1
#           define      pci_arfcn_r10_chosen 2
        union {
            CellGlobalIdEUTRA cellGlobalId_r10;  /* to choose, set choice to
                                                  * cellGlobalId_r10_chosen */
            struct _seq68 {
                PhysCellId      physCellId_r10;
                ARFCN_ValueEUTRA carrierFreq_r10;
            } pci_arfcn_r10;  /* to choose, set choice to
                               * pci_arfcn_r10_chosen */
        } u;
    } failedPCellId_r10;  /* extension #1; optional; set in bit_mask
                           * failedPCellId_r10_present if present */
    CellGlobalIdEUTRA reestablishmentCellId_r10;  /* extension #1; optional; set
                                   * in bit_mask
                                   * reestablishmentCellId_r10_present if
                                   * present */
    unsigned short  timeConnFailure_r10;  /* extension #1; optional; set in
                                   * bit_mask timeConnFailure_r10_present if
                                   * present */
    enum {
        rlf = 0,
        hof = 1
    } connectionFailureType_r10;  /* extension #1; optional; set in bit_mask
                                   * connectionFailureType_r10_present if
                                   * present */
    CellGlobalIdEUTRA previousPCellId_r10;  /* extension #1; optional; set in
                                   * bit_mask previousPCellId_r10_present if
                                   * present */
    struct {
        ARFCN_ValueEUTRA_v9e0 carrierFreq_v1090;
    } failedPCellId_v1090;  /* extension #2; optional; set in bit_mask
                             * failedPCellId_v1090_present if present */
    struct {
        C_RNTI          c_RNTI_r11;
        enum {
            t310_Expiry = 0,
            rlf_Cause_r11_randomAccessProblem = 1,
            rlf_Cause_r11_rlc_MaxNumRetx = 2,
            t312_Expiry_r12 = 3
        } rlf_Cause_r11;
        TimeSinceFailure_r11 timeSinceFailure_r11;
    } basicFields_r11;  /* extension #3; optional; set in bit_mask
                         * basicFields_r11_present if present */
    struct {
        unsigned char   bit_mask;
#           define      cellGlobalId_r11_present 0x80
        ARFCN_ValueUTRA carrierFreq_r11;
        _choice36       physCellId_r11;
        CellGlobalIdUTRA cellGlobalId_r11;  /* optional; set in bit_mask
                                             * cellGlobalId_r11_present if
                                             * present */
    } previousUTRA_CellId_r11;  /* extension #3; optional; set in bit_mask
                                 * previousUTRA_CellId_r11_present if present */
    struct {
        ARFCN_ValueUTRA carrierFreq_r11;
        _choice36       physCellId_r11;
    } selectedUTRA_CellId_r11;  /* extension #3; optional; set in bit_mask
                                 * selectedUTRA_CellId_r11_present if present */
    struct {
        TrackingAreaCode tac_FailedPCell_r12;
    } failedPCellId_v1250;  /* extension #4; optional; set in bit_mask
                             * failedPCellId_v1250_present if present */
    RSRQ_Range_v1250 measResultLastServCell_v1250;  /* extension #4; optional;
                                   * set in bit_mask
                                   * measResultLastServCell_v1250_present if
                                   * present */
    RSRQ_Type_r12   lastServCellRSRQ_Type_r12;  /* extension #4; optional; set
                                   * in bit_mask
                                   * lastServCellRSRQ_Type_r12_present if
                                   * present */
    struct MeasResultList2EUTRA_v1250 *measResultListEUTRA_v1250;  /* extension
                                   * #4; optional; set in bit_mask
                           * RLF_Report_r9_measResultListEUTRA_v1250_present if
                           * present */
} RLF_Report_r9;

typedef struct RLF_Report_v9e0 {
    struct MeasResultList2EUTRA_v9e0 *measResultListEUTRA_v9e0;
} RLF_Report_v9e0;

/* Late non critical extensions */
typedef struct UEInformationResponse_v9e0_IEs {
    unsigned char   bit_mask;
#       define      rlf_Report_v9e0_present 0x80
#       define      UEInformationResponse_v9e0_IEs_nonCriticalExtension_present 0x40
    RLF_Report_v9e0 rlf_Report_v9e0;  /* optional; set in bit_mask
                                       * rlf_Report_v9e0_present if present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
               * UEInformationResponse_v9e0_IEs_nonCriticalExtension_present if
               * present */
} UEInformationResponse_v9e0_IEs;

typedef struct LogMeasReport_r10 {
    unsigned char   bit_mask;
#       define      LogMeasReport_r10_logMeasAvailable_r10_present 0x80
    AbsoluteTimeInfo_r10 absoluteTimeStamp_r10;
    TraceReference_r10 traceReference_r10;
    _octet2         traceRecordingSessionRef_r10;
    _octet3         tce_Id_r10;
    struct LogMeasInfoList_r10 *logMeasInfoList_r10;
    _enum3          logMeasAvailable_r10;  /* optional; set in bit_mask
                            * LogMeasReport_r10_logMeasAvailable_r10_present if
                            * present */
} LogMeasReport_r10;

typedef struct ConnEstFailReport_r11 {
    unsigned char   bit_mask;
#       define      locationInfo_r11_present 0x80
#       define      measResultNeighCells_r11_present 0x40
#       define      measResultListEUTRA_v1130_present 0x20
#       define      measResultFailedCell_v1250_present 0x10
#       define      failedCellRSRQ_Type_r12_present 0x08
#       define      ConnEstFailReport_r11_measResultListEUTRA_v1250_present 0x04
    CellGlobalIdEUTRA failedCellId_r11;
    LocationInfo_r10 locationInfo_r11;  /* optional; set in bit_mask
                                         * locationInfo_r11_present if
                                         * present */
    struct {
        unsigned char   bit_mask;
#           define      rsrqResult_r11_present 0x80
        RSRP_Range      rsrpResult_r11;
        RSRQ_Range      rsrqResult_r11;  /* optional; set in bit_mask
                                          * rsrqResult_r11_present if present */
    } measResultFailedCell_r11;
    struct {
        unsigned char   bit_mask;
#           define      measResultListEUTRA_r11_present 0x80
#           define      measResultListUTRA_r11_present 0x40
#           define      measResultListGERAN_r11_present 0x20
#           define      measResultsCDMA2000_r11_present 0x10
        struct MeasResultList2EUTRA_r9 *measResultListEUTRA_r11;  /* optional;
                                   * set in bit_mask
                                   * measResultListEUTRA_r11_present if
                                   * present */
        struct MeasResultList2UTRA_r9 *measResultListUTRA_r11;  /* optional; set
                                   * in bit_mask measResultListUTRA_r11_present
                                   * if present */
        struct MeasResultListGERAN *measResultListGERAN_r11;  /* optional; set
                                   * in bit_mask measResultListGERAN_r11_present
                                   * if present */
        struct MeasResultList2CDMA2000_r9 *measResultsCDMA2000_r11;  
                                  /* optional; set in bit_mask
                                   * measResultsCDMA2000_r11_present if
                                   * present */
    } measResultNeighCells_r11;  /* optional; set in bit_mask
                                  * measResultNeighCells_r11_present if
                                  * present */
    NumberOfPreamblesSent_r11 numberOfPreamblesSent_r11;
    ossBoolean      contentionDetected_r11;
    ossBoolean      maxTxPowerReached_r11;
    TimeSinceFailure_r11 timeSinceFailure_r11;
    struct MeasResultList2EUTRA_v9e0 *measResultListEUTRA_v1130;  /* optional;
                                   * set in bit_mask
                                   * measResultListEUTRA_v1130_present if
                                   * present */
    RSRQ_Range_v1250 measResultFailedCell_v1250;  /* extension #1; optional; set
                                   * in bit_mask
                                   * measResultFailedCell_v1250_present if
                                   * present */
    RSRQ_Type_r12   failedCellRSRQ_Type_r12;  /* extension #1; optional; set in
                                   * bit_mask failedCellRSRQ_Type_r12_present if
                                   * present */
    struct MeasResultList2EUTRA_v1250 *measResultListEUTRA_v1250;  /* extension
                                   * #1; optional; set in bit_mask
                   * ConnEstFailReport_r11_measResultListEUTRA_v1250_present if
                   * present */
} ConnEstFailReport_r11;

typedef struct VisitedCellInfoList_r12 *MobilityHistoryReport_r12;

typedef struct UEInformationResponse_v1250_IEs {
    unsigned char   bit_mask;
#       define      mobilityHistoryReport_r12_present 0x80
#       define      UEInformationResponse_v1250_IEs_nonCriticalExtension_present 0x40
    MobilityHistoryReport_r12 mobilityHistoryReport_r12;  /* optional; set in
                                   * bit_mask mobilityHistoryReport_r12_present
                                   * if present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
              * UEInformationResponse_v1250_IEs_nonCriticalExtension_present if
              * present */
} UEInformationResponse_v1250_IEs;

typedef struct UEInformationResponse_v1130_IEs {
    unsigned char   bit_mask;
#       define      connEstFailReport_r11_present 0x80
#       define      UEInformationResponse_v1130_IEs_nonCriticalExtension_present 0x40
    ConnEstFailReport_r11 connEstFailReport_r11;  /* optional; set in bit_mask
                                             * connEstFailReport_r11_present if
                                             * present */
    UEInformationResponse_v1250_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
              * UEInformationResponse_v1130_IEs_nonCriticalExtension_present if
              * present */
} UEInformationResponse_v1130_IEs;

typedef struct UEInformationResponse_v1020_IEs {
    unsigned char   bit_mask;
#       define      logMeasReport_r10_present 0x80
#       define      UEInformationResponse_v1020_IEs_nonCriticalExtension_present 0x40
    LogMeasReport_r10 logMeasReport_r10;  /* optional; set in bit_mask
                                           * logMeasReport_r10_present if
                                           * present */
    UEInformationResponse_v1130_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
              * UEInformationResponse_v1020_IEs_nonCriticalExtension_present if
              * present */
} UEInformationResponse_v1020_IEs;

/* Regular non critical extensions */
typedef struct UEInformationResponse_v930_IEs {
    unsigned char   bit_mask;
#       define      UEInformationResponse_v930_IEs_lateNonCriticalExtension_present 0x80
#       define      UEInformationResponse_v930_IEs_nonCriticalExtension_present 0x40
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        UEInformationResponse_v9e0_IEs *decoded;
    } lateNonCriticalExtension;  /* optional; set in bit_mask
           * UEInformationResponse_v930_IEs_lateNonCriticalExtension_present if
           * present */
    UEInformationResponse_v1020_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
               * UEInformationResponse_v930_IEs_nonCriticalExtension_present if
               * present */
} UEInformationResponse_v930_IEs;

typedef struct UEInformationResponse_r9_IEs {
    unsigned char   bit_mask;
#       define      UEInformationResponse_r9_IEs_rach_Report_r9_present 0x80
#       define      rlf_Report_r9_present 0x40
#       define      UEInformationResponse_r9_IEs_nonCriticalExtension_present 0x20
    struct {
        NumberOfPreamblesSent_r11 numberOfPreamblesSent_r9;
        ossBoolean      contentionDetected_r9;
    } rach_Report_r9;  /* optional; set in bit_mask
                        * UEInformationResponse_r9_IEs_rach_Report_r9_present if
                        * present */
    RLF_Report_r9   rlf_Report_r9;  /* optional; set in bit_mask
                                     * rlf_Report_r9_present if present */
    UEInformationResponse_v930_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UEInformationResponse_r9_IEs_nonCriticalExtension_present if
                 * present */
} UEInformationResponse_r9_IEs;

typedef struct UEInformationResponse_r9 {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      UEInformationResponse_r9_criticalExtensions_c1_chosen 1
#           define      UEInformationResponse_r9_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice37 {
                unsigned short  choice;
#                   define      criticalExtensions_c1_ueInformationResponse_r9_chosen 1
#                   define      UEInformationResponse_r9_criticalExtensions_c1_spare3_chosen 2
#                   define      UEInformationResponse_r9_criticalExtensions_c1_spare2_chosen 3
#                   define      UEInformationResponse_r9_criticalExtensions_c1_spare1_chosen 4
                union {
                    UEInformationResponse_r9_IEs ueInformationResponse_r9;  
                                        /* to choose, set choice to
                     * criticalExtensions_c1_ueInformationResponse_r9_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
              * UEInformationResponse_r9_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
              * UEInformationResponse_r9_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
              * UEInformationResponse_r9_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * UEInformationResponse_r9_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * UEInformationResponse_r9_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} UEInformationResponse_r9;

typedef struct ProximityIndication_v930_IEs {
    unsigned char   bit_mask;
#       define      ProximityIndication_v930_IEs_lateNonCriticalExtension_present 0x80
#       define      ProximityIndication_v930_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
             * ProximityIndication_v930_IEs_lateNonCriticalExtension_present if
             * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                 * ProximityIndication_v930_IEs_nonCriticalExtension_present if
                 * present */
} ProximityIndication_v930_IEs;

typedef struct ProximityIndication_r9_IEs {
    unsigned char   bit_mask;
#       define      ProximityIndication_r9_IEs_nonCriticalExtension_present 0x80
    enum {
        entering = 0,
        leaving = 1
    } type_r9;
    struct {
        unsigned short  choice;
#           define      eutra_r9_chosen 1
#           define      utra_r9_chosen 2
#           define      eutra2_v9e0_chosen 3
        union {
            ARFCN_ValueEUTRA eutra_r9;  /* to choose, set choice to
                                         * eutra_r9_chosen */
            ARFCN_ValueUTRA utra_r9;  /* to choose, set choice to
                                       * utra_r9_chosen */
            ARFCN_ValueEUTRA_v9e0 eutra2_v9e0;  /* extension #1; to choose, set
                                                 * choice to
                                                 * eutra2_v9e0_chosen */
        } u;
    } carrierFreq_r9;
    ProximityIndication_v930_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                   * ProximityIndication_r9_IEs_nonCriticalExtension_present if
                   * present */
} ProximityIndication_r9_IEs;

typedef struct ProximityIndication_r9 {
    struct {
        unsigned short  choice;
#           define      ProximityIndication_r9_criticalExtensions_c1_chosen 1
#           define      ProximityIndication_r9_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice38 {
                unsigned short  choice;
#                   define      criticalExtensions_c1_proximityIndication_r9_chosen 1
#                   define      ProximityIndication_r9_criticalExtensions_c1_spare3_chosen 2
#                   define      ProximityIndication_r9_criticalExtensions_c1_spare2_chosen 3
#                   define      ProximityIndication_r9_criticalExtensions_c1_spare1_chosen 4
                union {
                    ProximityIndication_r9_IEs proximityIndication_r9;  /* to
                                   * choose, set choice to
                       * criticalExtensions_c1_proximityIndication_r9_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                * ProximityIndication_r9_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                * ProximityIndication_r9_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                * ProximityIndication_r9_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * ProximityIndication_r9_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
 * ProximityIndication_r9_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} ProximityIndication_r9;

typedef struct RNReconfigurationComplete_r10_IEs {
    unsigned char   bit_mask;
#       define      RNReconfigurationComplete_r10_IEs_lateNonCriticalExtension_present 0x80
#       define      RNReconfigurationComplete_r10_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
        * RNReconfigurationComplete_r10_IEs_lateNonCriticalExtension_present if
        * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
            * RNReconfigurationComplete_r10_IEs_nonCriticalExtension_present if
            * present */
} RNReconfigurationComplete_r10_IEs;

typedef struct RNReconfigurationComplete_r10 {
    RRC_TransactionIdentifier rrc_TransactionIdentifier;
    struct {
        unsigned short  choice;
#           define      RNReconfigurationComplete_r10_criticalExtensions_c1_chosen 1
#           define      RNReconfigurationComplete_r10_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice39 {
                unsigned short  choice;
#                   define      criticalExtensions_c1_rnReconfigurationComplete_r10_chosen 1
#                   define      RNReconfigurationComplete_r10_criticalExtensions_c1_spare3_chosen 2
#                   define      RNReconfigurationComplete_r10_criticalExtensions_c1_spare2_chosen 3
#                   define      RNReconfigurationComplete_r10_criticalExtensions_c1_spare1_chosen 4
                union {
                    RNReconfigurationComplete_r10_IEs rnReconfigurationComplete_r10;                                    /* to choose, set choice to
                * criticalExtensions_c1_rnReconfigurationComplete_r10_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
         * RNReconfigurationComplete_r10_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
         * RNReconfigurationComplete_r10_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
         * RNReconfigurationComplete_r10_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                * RNReconfigurationComplete_r10_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
        * RNReconfigurationComplete_r10_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} RNReconfigurationComplete_r10;

typedef struct MBMSCountingResponse_r10_IEs {
    unsigned char   bit_mask;
#       define      mbsfn_AreaIndex_r10_present 0x80
#       define      countingResponseList_r10_present 0x40
#       define      MBMSCountingResponse_r10_IEs_lateNonCriticalExtension_present 0x20
#       define      MBMSCountingResponse_r10_IEs_nonCriticalExtension_present 0x10
    unsigned short  mbsfn_AreaIndex_r10;  /* optional; set in bit_mask
                                           * mbsfn_AreaIndex_r10_present if
                                           * present */
    struct CountingResponseList_r10 *countingResponseList_r10;  /* optional; set
                                   * in bit_mask
                                   * countingResponseList_r10_present if
                                   * present */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
             * MBMSCountingResponse_r10_IEs_lateNonCriticalExtension_present if
             * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                 * MBMSCountingResponse_r10_IEs_nonCriticalExtension_present if
                 * present */
} MBMSCountingResponse_r10_IEs;

typedef struct MBMSCountingResponse_r10 {
    struct {
        unsigned short  choice;
#           define      MBMSCountingResponse_r10_criticalExtensions_c1_chosen 1
#           define      MBMSCountingResponse_r10_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice40 {
                unsigned short  choice;
#                   define      countingResponse_r10_chosen 1
#                   define      MBMSCountingResponse_r10_criticalExtensions_c1_spare3_chosen 2
#                   define      MBMSCountingResponse_r10_criticalExtensions_c1_spare2_chosen 3
#                   define      MBMSCountingResponse_r10_criticalExtensions_c1_spare1_chosen 4
                union {
                    MBMSCountingResponse_r10_IEs countingResponse_r10;  /* to
                                   * choose, set choice to
                                   * countingResponse_r10_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
              * MBMSCountingResponse_r10_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
              * MBMSCountingResponse_r10_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
              * MBMSCountingResponse_r10_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * MBMSCountingResponse_r10_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * MBMSCountingResponse_r10_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} MBMSCountingResponse_r10;

typedef struct InterFreqRSTDMeasurementIndication_r10_IEs {
    unsigned char   bit_mask;
#       define      InterFreqRSTDMeasurementIndication_r10_IEs_lateNonCriticalExtension_present 0x80
#       define      InterFreqRSTDMeasurementIndication_r10_IEs_nonCriticalExtension_present 0x40
    struct {
        unsigned short  choice;
#           define      start_chosen 1
#           define      stop_chosen 2
        union {
            struct _seq69 {
                struct RSTD_InterFreqInfoList_r10 *rstd_InterFreqInfoList_r10;
            } start;  /* to choose, set choice to start_chosen */
            Nulltype        stop;  /* to choose, set choice to stop_chosen */
        } u;
    } rstd_InterFreqIndication_r10;
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
   * InterFreqRSTDMeasurementIndication_r10_IEs_lateNonCriticalExtension_present if
   * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
   * InterFreqRSTDMeasurementIndication_r10_IEs_nonCriticalExtension_present if
   * present */
} InterFreqRSTDMeasurementIndication_r10_IEs;

typedef struct InterFreqRSTDMeasurementIndication_r10 {
    struct {
        unsigned short  choice;
#           define      InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_chosen 1
#           define      InterFreqRSTDMeasurementIndication_r10_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice41 {
                unsigned short  choice;
#                   define      criticalExtensions_c1_interFreqRSTDMeasurementIndication_r10_chosen 1
#                   define      InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_spare3_chosen 2
#                   define      InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_spare2_chosen 3
#                   define      InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_spare1_chosen 4
                union {
                    InterFreqRSTDMeasurementIndication_r10_IEs interFreqRSTDMeasurementIndication_r10;                  /* to choose, set choice to
       * criticalExtensions_c1_interFreqRSTDMeasurementIndication_r10_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
  * InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
  * InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
  * InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
       * InterFreqRSTDMeasurementIndication_r10_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
                 * InterFreqRSTDMeasurementIndication_r10_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} InterFreqRSTDMeasurementIndication_r10;

typedef struct UEAssistanceInformation_r11_IEs {
    unsigned char   bit_mask;
#       define      UEAssistanceInformation_r11_IEs_powerPrefIndication_r11_present 0x80
#       define      UEAssistanceInformation_r11_IEs_lateNonCriticalExtension_present 0x40
#       define      UEAssistanceInformation_r11_IEs_nonCriticalExtension_present 0x20
    enum {
        powerPrefIndication_r11_normal = 0,
        lowPowerConsumption = 1
    } powerPrefIndication_r11;  /* optional; set in bit_mask
           * UEAssistanceInformation_r11_IEs_powerPrefIndication_r11_present if
           * present */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
          * UEAssistanceInformation_r11_IEs_lateNonCriticalExtension_present if
          * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
              * UEAssistanceInformation_r11_IEs_nonCriticalExtension_present if
              * present */
} UEAssistanceInformation_r11_IEs;

typedef struct UEAssistanceInformation_r11 {
    struct {
        unsigned short  choice;
#           define      UEAssistanceInformation_r11_criticalExtensions_c1_chosen 1
#           define      UEAssistanceInformation_r11_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice42 {
                unsigned short  choice;
#                   define      c1_ueAssistanceInformation_r11_chosen 1
#                   define      UEAssistanceInformation_r11_criticalExtensions_c1_spare3_chosen 2
#                   define      UEAssistanceInformation_r11_criticalExtensions_c1_spare2_chosen 3
#                   define      UEAssistanceInformation_r11_criticalExtensions_c1_spare1_chosen 4
                union {
                    UEAssistanceInformation_r11_IEs ueAssistanceInformation_r11;                                        /* to choose, set choice to
                                     * c1_ueAssistanceInformation_r11_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
           * UEAssistanceInformation_r11_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
           * UEAssistanceInformation_r11_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
           * UEAssistanceInformation_r11_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                  * UEAssistanceInformation_r11_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
      * UEAssistanceInformation_r11_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} UEAssistanceInformation_r11;

typedef struct TDM_AssistanceInfo_r11 {
    unsigned short  choice;
#       define      drx_AssistanceInfo_r11_chosen 1
#       define      idc_SubframePatternList_r11_chosen 2
    union {
        struct _seq70 {
            unsigned char   bit_mask;
#               define      drx_Offset_r11_present 0x80
            enum {
                drx_CycleLength_r11_sf40 = 0,
                drx_CycleLength_r11_sf64 = 1,
                drx_CycleLength_r11_sf80 = 2,
                drx_CycleLength_r11_sf128 = 3,
                drx_CycleLength_r11_sf160 = 4,
                drx_CycleLength_r11_sf256 = 5,
                drx_CycleLength_r11_spare2 = 6,
                drx_CycleLength_r11_spare1 = 7
            } drx_CycleLength_r11;
            unsigned short  drx_Offset_r11;  /* optional; set in bit_mask
                                              * drx_Offset_r11_present if
                                              * present */
            enum {
                drx_ActiveTime_r11_sf20 = 0,
                sf30 = 1,
                drx_ActiveTime_r11_sf40 = 2,
                drx_ActiveTime_r11_sf60 = 3,
                drx_ActiveTime_r11_sf80 = 4,
                drx_ActiveTime_r11_sf100 = 5,
                drx_ActiveTime_r11_spare2 = 6,
                drx_ActiveTime_r11_spare1 = 7
            } drx_ActiveTime_r11;
        } drx_AssistanceInfo_r11;  /* to choose, set choice to
                                    * drx_AssistanceInfo_r11_chosen */
        struct IDC_SubframePatternList_r11 *idc_SubframePatternList_r11;  
                                        /* to choose, set choice to
                                        * idc_SubframePatternList_r11_chosen */
    } u;
} TDM_AssistanceInfo_r11;

typedef struct VictimSystemType_r11 {
    unsigned char   bit_mask;
#       define      gps_r11_present 0x80
#       define      glonass_r11_present 0x40
#       define      bds_r11_present 0x20
#       define      galileo_r11_present 0x10
#       define      wlan_r11_present 0x08
#       define      bluetooth_r11_present 0x04
    _enum3          gps_r11;  /* optional; set in bit_mask gps_r11_present if
                               * present */
    _enum3          glonass_r11;  /* optional; set in bit_mask
                                   * glonass_r11_present if present */
    _enum3          bds_r11;  /* optional; set in bit_mask bds_r11_present if
                               * present */
    _enum3          galileo_r11;  /* optional; set in bit_mask
                                   * galileo_r11_present if present */
    _enum3          wlan_r11;  /* optional; set in bit_mask wlan_r11_present if
                                * present */
    _enum3          bluetooth_r11;  /* optional; set in bit_mask
                                     * bluetooth_r11_present if present */
} VictimSystemType_r11;

typedef struct InDeviceCoexIndication_v11d0_IEs {
    unsigned char   bit_mask;
#       define      ul_CA_AssistanceInfo_r11_present 0x80
#       define      InDeviceCoexIndication_v11d0_IEs_nonCriticalExtension_present 0x40
    struct {
        unsigned char   bit_mask;
#           define      affectedCarrierFreqCombList_r11_present 0x80
        struct AffectedCarrierFreqCombList_r11 *affectedCarrierFreqCombList_r11;                                        /* optional; set in bit_mask
                                   * affectedCarrierFreqCombList_r11_present if
                                   * present */
        VictimSystemType_r11 victimSystemType_r11;
    } ul_CA_AssistanceInfo_r11;  /* optional; set in bit_mask
                                  * ul_CA_AssistanceInfo_r11_present if
                                  * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
             * InDeviceCoexIndication_v11d0_IEs_nonCriticalExtension_present if
             * present */
} InDeviceCoexIndication_v11d0_IEs;

typedef struct InDeviceCoexIndication_r11_IEs {
    unsigned char   bit_mask;
#       define      affectedCarrierFreqList_r11_present 0x80
#       define      tdm_AssistanceInfo_r11_present 0x40
#       define      InDeviceCoexIndication_r11_IEs_lateNonCriticalExtension_present 0x20
#       define      InDeviceCoexIndication_r11_IEs_nonCriticalExtension_present 0x10
    struct AffectedCarrierFreqList_r11 *affectedCarrierFreqList_r11;  
                                  /* optional; set in bit_mask
                                   * affectedCarrierFreqList_r11_present if
                                   * present */
    TDM_AssistanceInfo_r11 tdm_AssistanceInfo_r11;  /* optional; set in bit_mask
                                            * tdm_AssistanceInfo_r11_present if
                                            * present */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
           * InDeviceCoexIndication_r11_IEs_lateNonCriticalExtension_present if
           * present */
    InDeviceCoexIndication_v11d0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
               * InDeviceCoexIndication_r11_IEs_nonCriticalExtension_present if
               * present */
} InDeviceCoexIndication_r11_IEs;

typedef struct InDeviceCoexIndication_r11 {
    struct {
        unsigned short  choice;
#           define      InDeviceCoexIndication_r11_criticalExtensions_c1_chosen 1
#           define      InDeviceCoexIndication_r11_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice43 {
                unsigned short  choice;
#                   define      c1_inDeviceCoexIndication_r11_chosen 1
#                   define      InDeviceCoexIndication_r11_criticalExtensions_c1_spare3_chosen 2
#                   define      InDeviceCoexIndication_r11_criticalExtensions_c1_spare2_chosen 3
#                   define      InDeviceCoexIndication_r11_criticalExtensions_c1_spare1_chosen 4
                union {
                    InDeviceCoexIndication_r11_IEs inDeviceCoexIndication_r11;                                          /* to choose, set choice to
                                      * c1_inDeviceCoexIndication_r11_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
            * InDeviceCoexIndication_r11_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
            * InDeviceCoexIndication_r11_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
            * InDeviceCoexIndication_r11_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                   * InDeviceCoexIndication_r11_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
     * InDeviceCoexIndication_r11_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} InDeviceCoexIndication_r11;

typedef struct MBMSInterestIndication_r11_IEs {
    unsigned char   bit_mask;
#       define      mbms_FreqList_r11_present 0x80
#       define      mbms_Priority_r11_present 0x40
#       define      MBMSInterestIndication_r11_IEs_lateNonCriticalExtension_present 0x20
#       define      MBMSInterestIndication_r11_IEs_nonCriticalExtension_present 0x10
    struct CarrierFreqListMBMS_r11 *mbms_FreqList_r11;  /* optional; set in
                                   * bit_mask mbms_FreqList_r11_present if
                                   * present */
    _enum3          mbms_Priority_r11;  /* optional; set in bit_mask
                                         * mbms_Priority_r11_present if
                                         * present */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
           * MBMSInterestIndication_r11_IEs_lateNonCriticalExtension_present if
           * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
               * MBMSInterestIndication_r11_IEs_nonCriticalExtension_present if
               * present */
} MBMSInterestIndication_r11_IEs;

typedef struct MBMSInterestIndication_r11 {
    struct {
        unsigned short  choice;
#           define      MBMSInterestIndication_r11_criticalExtensions_c1_chosen 1
#           define      MBMSInterestIndication_r11_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice44 {
                unsigned short  choice;
#                   define      interestIndication_r11_chosen 1
#                   define      MBMSInterestIndication_r11_criticalExtensions_c1_spare3_chosen 2
#                   define      MBMSInterestIndication_r11_criticalExtensions_c1_spare2_chosen 3
#                   define      MBMSInterestIndication_r11_criticalExtensions_c1_spare1_chosen 4
                union {
                    MBMSInterestIndication_r11_IEs interestIndication_r11;  
                                        /* to choose, set choice to
                                         * interestIndication_r11_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
            * MBMSInterestIndication_r11_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
            * MBMSInterestIndication_r11_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
            * MBMSInterestIndication_r11_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                   * MBMSInterestIndication_r11_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
     * MBMSInterestIndication_r11_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} MBMSInterestIndication_r11;

typedef struct FailureReportSCG_r12 {
    unsigned char   bit_mask;
#       define      measResultServFreqList_r12_present 0x80
#       define      measResultNeighCells_r12_present 0x40
    enum {
        t313_Expiry = 0,
        failureType_r12_randomAccessProblem = 1,
        failureType_r12_rlc_MaxNumRetx = 2,
        scg_ChangeFailure = 3
    } failureType_r12;
    struct MeasResultServFreqList_r10 *measResultServFreqList_r12;  
                                  /* optional; set in bit_mask
                                   * measResultServFreqList_r12_present if
                                   * present */
    struct MeasResultList2EUTRA_r9 *measResultNeighCells_r12;  /* optional; set
                                   * in bit_mask
                                   * measResultNeighCells_r12_present if
                                   * present */
} FailureReportSCG_r12;

typedef struct SCGFailureInformation_r12_IEs {
    unsigned char   bit_mask;
#       define      failureReportSCG_r12_present 0x80
#       define      SCGFailureInformation_r12_IEs_nonCriticalExtension_present 0x40
    FailureReportSCG_r12 failureReportSCG_r12;  /* optional; set in bit_mask
                                                 * failureReportSCG_r12_present
                                                 * if present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * SCGFailureInformation_r12_IEs_nonCriticalExtension_present if
                * present */
} SCGFailureInformation_r12_IEs;

typedef struct SCGFailureInformation_r12 {
    struct {
        unsigned short  choice;
#           define      SCGFailureInformation_r12_criticalExtensions_c1_chosen 1
#           define      SCGFailureInformation_r12_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice45 {
                unsigned short  choice;
#                   define      c1_scgFailureInformation_r12_chosen 1
#                   define      SCGFailureInformation_r12_criticalExtensions_c1_spare3_chosen 2
#                   define      SCGFailureInformation_r12_criticalExtensions_c1_spare2_chosen 3
#                   define      SCGFailureInformation_r12_criticalExtensions_c1_spare1_chosen 4
                union {
                    SCGFailureInformation_r12_IEs scgFailureInformation_r12;  
                                        /* to choose, set choice to
                                       * c1_scgFailureInformation_r12_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
             * SCGFailureInformation_r12_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
             * SCGFailureInformation_r12_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
             * SCGFailureInformation_r12_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * SCGFailureInformation_r12_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
    * SCGFailureInformation_r12_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SCGFailureInformation_r12;

typedef struct SL_CommTxResourceReq_r12 {
    unsigned char   bit_mask;
#       define      carrierFreq_r12_present 0x80
    ARFCN_ValueEUTRA_r9 carrierFreq_r12;  /* optional; set in bit_mask
                                           * carrierFreq_r12_present if
                                           * present */
    struct SL_DestinationInfoList_r12 *destinationInfoList_r12;
} SL_CommTxResourceReq_r12;

typedef struct SidelinkUEInformation_r12_IEs {
    unsigned char   bit_mask;
#       define      commRxInterestedFreq_r12_present 0x80
#       define      commTxResourceReq_r12_present 0x40
#       define      discRxInterest_r12_present 0x20
#       define      discTxResourceReq_r12_present 0x10
#       define      SidelinkUEInformation_r12_IEs_lateNonCriticalExtension_present 0x08
#       define      SidelinkUEInformation_r12_IEs_nonCriticalExtension_present 0x04
    ARFCN_ValueEUTRA_r9 commRxInterestedFreq_r12;  /* optional; set in bit_mask
                                          * commRxInterestedFreq_r12_present if
                                          * present */
    SL_CommTxResourceReq_r12 commTxResourceReq_r12;  /* optional; set in
                                   * bit_mask commTxResourceReq_r12_present if
                                   * present */
    _enum3          discRxInterest_r12;  /* optional; set in bit_mask
                                          * discRxInterest_r12_present if
                                          * present */
    unsigned short  discTxResourceReq_r12;  /* optional; set in bit_mask
                                             * discTxResourceReq_r12_present if
                                             * present */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
            * SidelinkUEInformation_r12_IEs_lateNonCriticalExtension_present if
            * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * SidelinkUEInformation_r12_IEs_nonCriticalExtension_present if
                * present */
} SidelinkUEInformation_r12_IEs;

typedef struct SidelinkUEInformation_r12 {
    struct {
        unsigned short  choice;
#           define      SidelinkUEInformation_r12_criticalExtensions_c1_chosen 1
#           define      SidelinkUEInformation_r12_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice46 {
                unsigned short  choice;
#                   define      c1_sidelinkUEInformation_r12_chosen 1
#                   define      SidelinkUEInformation_r12_criticalExtensions_c1_spare3_chosen 2
#                   define      SidelinkUEInformation_r12_criticalExtensions_c1_spare2_chosen 3
#                   define      SidelinkUEInformation_r12_criticalExtensions_c1_spare1_chosen 4
                union {
                    SidelinkUEInformation_r12_IEs sidelinkUEInformation_r12;  
                                        /* to choose, set choice to
                                       * c1_sidelinkUEInformation_r12_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
             * SidelinkUEInformation_r12_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
             * SidelinkUEInformation_r12_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
             * SidelinkUEInformation_r12_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * SidelinkUEInformation_r12_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
    * SidelinkUEInformation_r12_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SidelinkUEInformation_r12;

typedef struct UL_DCCH_MessageType {
    unsigned short  choice;
#       define      UL_DCCH_MessageType_c1_chosen 1
#       define      UL_DCCH_MessageType_messageClassExtension_chosen 2
    union {
        struct _choice48 {
            unsigned short  choice;
#               define      csfbParametersRequestCDMA2000_chosen 1
#               define      measurementReport_chosen 2
#               define      rrcConnectionReconfigurationComplete_chosen 3
#               define      rrcConnectionReestablishmentComplete_chosen 4
#               define      rrcConnectionSetupComplete_chosen 5
#               define      securityModeComplete_chosen 6
#               define      securityModeFailure_chosen 7
#               define      ueCapabilityInformation_chosen 8
#               define      ulHandoverPreparationTransfer_chosen 9
#               define      ulInformationTransfer_chosen 10
#               define      counterCheckResponse_chosen 11
#               define      UL_DCCH_MessageType_c1_ueInformationResponse_r9_chosen 12
#               define      UL_DCCH_MessageType_c1_proximityIndication_r9_chosen 13
#               define      UL_DCCH_MessageType_c1_rnReconfigurationComplete_r10_chosen 14
#               define      mbmsCountingResponse_r10_chosen 15
#               define      UL_DCCH_MessageType_c1_interFreqRSTDMeasurementIndication_r10_chosen 16
            union {
                CSFBParametersRequestCDMA2000 csfbParametersRequestCDMA2000;  
                                        /* to choose, set choice to
                                      * csfbParametersRequestCDMA2000_chosen */
                MeasurementReport measurementReport;  /* to choose, set choice
                                               * to measurementReport_chosen */
                RRCConnectionReconfigurationComplete rrcConnectionReconfigurationComplete;                              /* to choose, set choice to
                               * rrcConnectionReconfigurationComplete_chosen */
                RRCConnectionReestablishmentComplete rrcConnectionReestablishmentComplete;                              /* to choose, set choice to
                               * rrcConnectionReestablishmentComplete_chosen */
                RRCConnectionSetupComplete rrcConnectionSetupComplete;  /* to
                                   * choose, set choice to
                                   * rrcConnectionSetupComplete_chosen */
                SecurityModeComplete securityModeComplete;  /* to choose, set
                                     * choice to securityModeComplete_chosen */
                SecurityModeFailure securityModeFailure;  /* to choose, set
                                      * choice to securityModeFailure_chosen */
                UECapabilityInformation ueCapabilityInformation;  /* to choose,
                                   * set choice to
                                   * ueCapabilityInformation_chosen */
                ULHandoverPreparationTransfer ulHandoverPreparationTransfer;  
                                        /* to choose, set choice to
                                      * ulHandoverPreparationTransfer_chosen */
                ULInformationTransfer ulInformationTransfer;  /* to choose, set
                                    * choice to ulInformationTransfer_chosen */
                CounterCheckResponse counterCheckResponse;  /* to choose, set
                                     * choice to counterCheckResponse_chosen */
                UEInformationResponse_r9 ueInformationResponse_r9;  /* to
                                   * choose, set choice to
                    * UL_DCCH_MessageType_c1_ueInformationResponse_r9_chosen */
                ProximityIndication_r9 proximityIndication_r9;  /* to choose,
                                   * set choice to
                      * UL_DCCH_MessageType_c1_proximityIndication_r9_chosen */
                RNReconfigurationComplete_r10 rnReconfigurationComplete_r10;  
                                        /* to choose, set choice to
               * UL_DCCH_MessageType_c1_rnReconfigurationComplete_r10_chosen */
                MBMSCountingResponse_r10 mbmsCountingResponse_r10;  /* to
                                   * choose, set choice to
                                   * mbmsCountingResponse_r10_chosen */
                InterFreqRSTDMeasurementIndication_r10 interFreqRSTDMeasurementIndication_r10;                          /* to choose, set choice to
      * UL_DCCH_MessageType_c1_interFreqRSTDMeasurementIndication_r10_chosen */
            } u;
        } c1;  /* to choose, set choice to UL_DCCH_MessageType_c1_chosen */
        struct _choice49 {
            unsigned short  choice;
#               define      messageClassExtension_c2_chosen 1
#               define      messageClassExtensionFuture_r11_chosen 2
            union {
                struct _choice47 {
                    unsigned short  choice;
#                       define      c2_ueAssistanceInformation_r11_chosen 1
#                       define      c2_inDeviceCoexIndication_r11_chosen 2
#                       define      mbmsInterestIndication_r11_chosen 3
#                       define      c2_scgFailureInformation_r12_chosen 4
#                       define      c2_sidelinkUEInformation_r12_chosen 5
#                       define      spare11_chosen 6
#                       define      spare10_chosen 7
#                       define      spare9_chosen 8
#                       define      spare8_chosen 9
#                       define      c2_spare7_chosen 10
#                       define      c2_spare6_chosen 11
#                       define      c2_spare5_chosen 12
#                       define      c2_spare4_chosen 13
#                       define      c2_spare3_chosen 14
#                       define      c2_spare2_chosen 15
#                       define      c2_spare1_chosen 16
                    union {
                        UEAssistanceInformation_r11 ueAssistanceInformation_r11;                                        /* to choose, set choice to
                                     * c2_ueAssistanceInformation_r11_chosen */
                        InDeviceCoexIndication_r11 inDeviceCoexIndication_r11;                                          /* to choose, set choice to
                                      * c2_inDeviceCoexIndication_r11_chosen */
                        MBMSInterestIndication_r11 mbmsInterestIndication_r11;                                          /* to choose, set choice to
                                         * mbmsInterestIndication_r11_chosen */
                        SCGFailureInformation_r12 scgFailureInformation_r12;  
                                        /* to choose, set choice to
                                       * c2_scgFailureInformation_r12_chosen */
                        SidelinkUEInformation_r12 sidelinkUEInformation_r12;  
                                        /* to choose, set choice to
                                       * c2_sidelinkUEInformation_r12_chosen */
                        Nulltype        spare11;  /* to choose, set choice to
                                                   * spare11_chosen */
                        Nulltype        spare10;  /* to choose, set choice to
                                                   * spare10_chosen */
                        Nulltype        spare9;  /* to choose, set choice to
                                                  * spare9_chosen */
                        Nulltype        spare8;  /* to choose, set choice to
                                                  * spare8_chosen */
                        Nulltype        spare7;  /* to choose, set choice to
                                                  * c2_spare7_chosen */
                        Nulltype        spare6;  /* to choose, set choice to
                                                  * c2_spare6_chosen */
                        Nulltype        spare5;  /* to choose, set choice to
                                                  * c2_spare5_chosen */
                        Nulltype        spare4;  /* to choose, set choice to
                                                  * c2_spare4_chosen */
                        Nulltype        spare3;  /* to choose, set choice to
                                                  * c2_spare3_chosen */
                        Nulltype        spare2;  /* to choose, set choice to
                                                  * c2_spare2_chosen */
                        Nulltype        spare1;  /* to choose, set choice to
                                                  * c2_spare1_chosen */
                    } u;
                } c2;  /* to choose, set choice to
                        * messageClassExtension_c2_chosen */
                _seq2           messageClassExtensionFuture_r11;  /* to choose,
                                   * set choice to
                                   * messageClassExtensionFuture_r11_chosen */
            } u;
        } messageClassExtension;  /* to choose, set choice to
                          * UL_DCCH_MessageType_messageClassExtension_chosen */
    } u;
} UL_DCCH_MessageType;

typedef struct UL_DCCH_Message {
    UL_DCCH_MessageType message;
} UL_DCCH_Message;

typedef unsigned short  DRB_Identity;

typedef struct DRB_CountMSB_Info {
    DRB_Identity    drb_Identity;
    unsigned int    countMSB_Uplink;
    unsigned int    countMSB_Downlink;
} DRB_CountMSB_Info;

typedef struct DRB_CountMSB_InfoList {
    struct DRB_CountMSB_InfoList *next;
    DRB_CountMSB_Info value;
} *DRB_CountMSB_InfoList;

typedef struct DRB_CountInfo {
    DRB_Identity    drb_Identity;
    unsigned int    count_Uplink;
    unsigned int    count_Downlink;
} DRB_CountInfo;

typedef struct DRB_CountInfoList {
    struct DRB_CountInfoList *next;
    DRB_CountInfo   value;
} *DRB_CountInfoList;

typedef unsigned short  MeasObjectId;

typedef struct AffectedCarrierFreq_r11 {
    MeasObjectId    carrierFreq_r11;
    enum {
        interferenceDirection_r11_eutra = 0,
        interferenceDirection_r11_other = 1,
        interferenceDirection_r11_both = 2,
        interferenceDirection_r11_spare = 3
    } interferenceDirection_r11;
} AffectedCarrierFreq_r11;

typedef struct AffectedCarrierFreqList_r11 {
    struct AffectedCarrierFreqList_r11 *next;
    AffectedCarrierFreq_r11 value;
} *AffectedCarrierFreqList_r11;

typedef struct AffectedCarrierFreqCombList_r11 {
    struct AffectedCarrierFreqCombList_r11 *next;
    struct AffectedCarrierFreqComb_r11 *value;
} *AffectedCarrierFreqCombList_r11;

typedef struct AffectedCarrierFreqComb_r11 {
    struct AffectedCarrierFreqComb_r11 *next;
    MeasObjectId    value;
} *AffectedCarrierFreqComb_r11;

typedef struct IDC_SubframePattern_r11 {
    unsigned short  choice;
#       define      subframePatternFDD_r11_chosen 1
#       define      subframePatternTDD_r11_chosen 2
    union {
        _bit1           subframePatternFDD_r11;  /* to choose, set choice to
                                             * subframePatternFDD_r11_chosen */
        struct _choice50 {
            unsigned short  choice;
#               define      subframeConfig0_r11_chosen 1
#               define      subframeConfig1_5_r11_chosen 2
#               define      subframeConfig6_r11_chosen 3
            union {
                _bit1           subframeConfig0_r11;  /* to choose, set choice
                                             * to subframeConfig0_r11_chosen */
                _bit1           subframeConfig1_5_r11;  /* to choose, set choice
                                           * to subframeConfig1_5_r11_chosen */
                _bit1           subframeConfig6_r11;  /* to choose, set choice
                                             * to subframeConfig6_r11_chosen */
            } u;
        } subframePatternTDD_r11;  /* to choose, set choice to
                                    * subframePatternTDD_r11_chosen */
    } u;
} IDC_SubframePattern_r11;

typedef struct IDC_SubframePatternList_r11 {
    struct IDC_SubframePatternList_r11 *next;
    IDC_SubframePattern_r11 value;
} *IDC_SubframePatternList_r11;

typedef struct RSTD_InterFreqInfo_r10 {
    unsigned char   bit_mask;
#       define      carrierFreq_v1090_present 0x80
    ARFCN_ValueEUTRA carrierFreq_r10;
    unsigned short  measPRS_Offset_r10;
    ARFCN_ValueEUTRA_v9e0 carrierFreq_v1090;  /* extension #1; optional; set in
                                   * bit_mask carrierFreq_v1090_present if
                                   * present */
} RSTD_InterFreqInfo_r10;

typedef struct RSTD_InterFreqInfoList_r10 {
    struct RSTD_InterFreqInfoList_r10 *next;
    RSTD_InterFreqInfo_r10 value;
} *RSTD_InterFreqInfoList_r10;

typedef unsigned short  MBSFN_AreaId_r12;

typedef struct TargetMBSFN_Area_r12 {
    unsigned char   bit_mask;
#       define      mbsfn_AreaId_r12_present 0x80
    MBSFN_AreaId_r12 mbsfn_AreaId_r12;  /* optional; set in bit_mask
                                         * mbsfn_AreaId_r12_present if
                                         * present */
                                        /* Need OR */
    ARFCN_ValueEUTRA_r9 carrierFreq_r12;
} TargetMBSFN_Area_r12;

typedef struct TargetMBSFN_AreaList_r12 {
    struct TargetMBSFN_AreaList_r12 *next;
    TargetMBSFN_Area_r12 value;
} *TargetMBSFN_AreaList_r12;

typedef struct TMGI_r9 {
    struct {
        unsigned short  choice;
#           define      plmn_Index_r9_chosen 1
#           define      explicitValue_r9_chosen 2
        union {
            unsigned short  plmn_Index_r9;  /* to choose, set choice to
                                             * plmn_Index_r9_chosen */
            PLMN_Identity   explicitValue_r9;  /* to choose, set choice to
                                                * explicitValue_r9_chosen */
        } u;
    } plmn_Id_r9;
    _octet6         serviceId_r9;
} TMGI_r9;

typedef struct CountingRequestInfo_r10 {
    TMGI_r9         tmgi_r10;
} CountingRequestInfo_r10;

typedef struct CountingRequestList_r10 {
    struct CountingRequestList_r10 *next;
    CountingRequestInfo_r10 value;
} *CountingRequestList_r10;

typedef struct CountingResponseInfo_r10 {
    unsigned short  countingResponseService_r10;
} CountingResponseInfo_r10;

typedef struct CountingResponseList_r10 {
    struct CountingResponseList_r10 *next;
    CountingResponseInfo_r10 value;
} *CountingResponseList_r10;

typedef struct MBSFN_SubframeConfig {
    enum {
        radioframeAllocationPeriod_n1 = 0,
        radioframeAllocationPeriod_n2 = 1,
        radioframeAllocationPeriod_n4 = 2,
        radioframeAllocationPeriod_n8 = 3,
        radioframeAllocationPeriod_n16 = 4,
        radioframeAllocationPeriod_n32 = 5
    } radioframeAllocationPeriod;
    unsigned short  radioframeAllocationOffset;
    struct {
        unsigned short  choice;
#           define      oneFrame_chosen 1
#           define      fourFrames_chosen 2
        union {
            _bit1           oneFrame;  /* to choose, set choice to
                                        * oneFrame_chosen */
            _bit1           fourFrames;  /* to choose, set choice to
                                          * fourFrames_chosen */
        } u;
    } subframeAllocation;
} MBSFN_SubframeConfig;

typedef struct CommonSF_AllocPatternList_r9 {
    struct CommonSF_AllocPatternList_r9 *next;
    MBSFN_SubframeConfig value;
} *CommonSF_AllocPatternList_r9;

typedef struct PagingUE_Identity {
    unsigned short  choice;
#       define      PagingUE_Identity_s_TMSI_chosen 1
#       define      imsi_chosen 2
    union {
        S_TMSI          s_TMSI;  /* to choose, set choice to
                                  * PagingUE_Identity_s_TMSI_chosen */
        struct IMSI     *imsi;  /* to choose, set choice to imsi_chosen */
    } u;
} PagingUE_Identity;

typedef struct PagingRecord {
    PagingUE_Identity ue_Identity;
    enum {
        ps = 0,
        cs = 1
    } cn_Domain;
} PagingRecord;

typedef struct PagingRecordList {
    struct PagingRecordList *next;
    PagingRecord    value;
} *PagingRecordList;

typedef unsigned short  IMSI_Digit;

typedef struct IMSI {
    struct IMSI     *next;
    IMSI_Digit      value;
} *IMSI;

typedef unsigned short  ServCellIndex_r10;

typedef struct CrossCarrierSchedulingConfig_r10 {
    struct {
        unsigned short  choice;
#           define      own_r10_chosen 1
#           define      other_r10_chosen 2
        union {
            struct _seq71 {                                      /* No cross carrier scheduling */
                ossBoolean      cif_Presence_r10;
            } own_r10;  /* to choose, set choice to own_r10_chosen */
            struct _seq72 {                                      /* Cross carrier scheduling */
                ServCellIndex_r10 schedulingCellId_r10;
                unsigned short  pdsch_Start_r10;
            } other_r10;  /* to choose, set choice to other_r10_chosen */
        } u;
    } schedulingCellInfo_r10;
} CrossCarrierSchedulingConfig_r10;

typedef struct PUSCH_ConfigDedicatedSCell_r10 {
    unsigned char   bit_mask;
#       define      PUSCH_ConfigDedicatedSCell_r10_groupHoppingDisabled_r10_present 0x80
#       define      PUSCH_ConfigDedicatedSCell_r10_dmrs_WithOCC_Activated_r10_present 0x40
    _enum3          groupHoppingDisabled_r10;  /* optional; set in bit_mask
           * PUSCH_ConfigDedicatedSCell_r10_groupHoppingDisabled_r10_present if
           * present */
       /* Need OR */
    _enum3          dmrs_WithOCC_Activated_r10;  /* optional; set in bit_mask
         * PUSCH_ConfigDedicatedSCell_r10_dmrs_WithOCC_Activated_r10_present if
         * present */
                                                 /* Need OR */
} PUSCH_ConfigDedicatedSCell_r10;

typedef struct UplinkPowerControlDedicatedSCell_r10 {
    unsigned char   bit_mask;
#       define      UplinkPowerControlDedicatedSCell_r10_pSRS_OffsetAp_r10_present 0x80
#       define      filterCoefficient_r10_present 0x40
    short           p0_UE_PUSCH_r10;
    _enum19         deltaMCS_Enabled_r10;
    ossBoolean      accumulationEnabled_r10;
    unsigned short  pSRS_Offset_r10;
    unsigned short  pSRS_OffsetAp_r10;  /* optional; set in bit_mask
            * UplinkPowerControlDedicatedSCell_r10_pSRS_OffsetAp_r10_present if
            * present */
                                        /* Need OR */
    FilterCoefficient filterCoefficient_r10;  /* filterCoefficient_r10_present
                                   * not set in bit_mask implies value is fc4 */
    enum {
        pCell = 0,
        sCell = 1
    } pathlossReferenceLinking_r10;
} UplinkPowerControlDedicatedSCell_r10;

typedef struct CQI_ReportConfigSCell_r10 {
    unsigned char   bit_mask;
#       define      cqi_ReportModeAperiodic_r10_present 0x80
#       define      cqi_ReportPeriodicSCell_r10_present 0x40
#       define      pmi_RI_Report_r10_present 0x20
    CQI_ReportModeAperiodic cqi_ReportModeAperiodic_r10;  /* optional; set in
                                   * bit_mask
                                   * cqi_ReportModeAperiodic_r10_present if
                                   * present */
                                                          /* Need OR */
    short           nomPDSCH_RS_EPRE_Offset_r10;
    CQI_ReportPeriodic_r10 cqi_ReportPeriodicSCell_r10;  /* optional; set in
                                   * bit_mask
                                   * cqi_ReportPeriodicSCell_r10_present if
                                   * present */
                                                         /* Need ON */
    _enum17         pmi_RI_Report_r10;  /* optional; set in bit_mask
                                         * pmi_RI_Report_r10_present if
                                         * present */
                                        /* Cond PMIRISCell */
} CQI_ReportConfigSCell_r10;

typedef struct PhysicalConfigDedicatedSCell_r10 {
    unsigned short  bit_mask;
#       define      nonUL_Configuration_r10_present 0x8000
#       define      PhysicalConfigDedicatedSCell_r10_ul_Configuration_r10_present 0x4000
#       define      PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigNZPToReleaseList_r11_present 0x2000
#       define      PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigNZPToAddModList_r11_present 0x1000
#       define      PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigZPToReleaseList_r11_present 0x0800
#       define      PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigZPToAddModList_r11_present 0x0400
#       define      PhysicalConfigDedicatedSCell_r10_epdcch_Config_r11_present 0x0200
#       define      PhysicalConfigDedicatedSCell_r10_pdsch_ConfigDedicated_v1130_present 0x0100
#       define      PhysicalConfigDedicatedSCell_r10_cqi_ReportConfig_v1130_present 0x0080
#       define      PhysicalConfigDedicatedSCell_r10_pusch_ConfigDedicated_v1130_present 0x0040
#       define      uplinkPowerControlDedicatedSCell_v1130_present 0x0020
#       define      PhysicalConfigDedicatedSCell_r10_antennaInfo_v1250_present 0x0010
#       define      eimta_MainConfigSCell_r12_present 0x0008
#       define      cqi_ReportConfigSCell_v1250_present 0x0004
#       define      uplinkPowerControlDedicatedSCell_v1250_present 0x0002
#       define      PhysicalConfigDedicatedSCell_r10_csi_RS_Config_v1250_present 0x0001
	/* DL configuration as well as configuration applicable for DL and UL */
    struct {
        unsigned char   bit_mask;
#           define      nonUL_Configuration_r10_antennaInfo_r10_present 0x80
#           define      crossCarrierSchedulingConfig_r10_present 0x40
#           define      nonUL_Configuration_r10_csi_RS_Config_r10_present 0x20
#           define      pdsch_ConfigDedicated_r10_present 0x10
        AntennaInfoDedicated_r10 antennaInfo_r10;  /* optional; set in bit_mask
                           * nonUL_Configuration_r10_antennaInfo_r10_present if
                           * present */
                                                   /* Need ON */
        CrossCarrierSchedulingConfig_r10 crossCarrierSchedulingConfig_r10;  
                                        /* optional; set in bit_mask
                                  * crossCarrierSchedulingConfig_r10_present if
                                  * present */
                                                                            /* Need ON */
        CSI_RS_Config_r10 csi_RS_Config_r10;  /* optional; set in bit_mask
                         * nonUL_Configuration_r10_csi_RS_Config_r10_present if
                         * present */
                                              /* Need ON */
        PDSCH_ConfigDedicated pdsch_ConfigDedicated_r10;  /* optional; set in
                                   * bit_mask pdsch_ConfigDedicated_r10_present
                                   * if present */
                                                          /* Need ON */
    } nonUL_Configuration_r10;  /* optional; set in bit_mask
                                 * nonUL_Configuration_r10_present if present */
       /* Cond SCellAdd */
	/* UL configuration */
    struct {
        unsigned char   bit_mask;
#           define      ul_Configuration_r10_antennaInfoUL_r10_present 0x80
#           define      pusch_ConfigDedicatedSCell_r10_present 0x40
#           define      uplinkPowerControlDedicatedSCell_r10_present 0x20
#           define      cqi_ReportConfigSCell_r10_present 0x10
#           define      soundingRS_UL_ConfigDedicated_r10_present 0x08
#           define      ul_Configuration_r10_soundingRS_UL_ConfigDedicated_v1020_present 0x04
#           define      ul_Configuration_r10_soundingRS_UL_ConfigDedicatedAperiodic_r10_present 0x02
        AntennaInfoUL_r10 antennaInfoUL_r10;  /* optional; set in bit_mask
                            * ul_Configuration_r10_antennaInfoUL_r10_present if
                            * present */
                                              /* Need ON */
        PUSCH_ConfigDedicatedSCell_r10 pusch_ConfigDedicatedSCell_r10;  
                                  /* optional; set in bit_mask
                                   * pusch_ConfigDedicatedSCell_r10_present if
                                   * present */
                                                                        /* Need ON */
        UplinkPowerControlDedicatedSCell_r10 uplinkPowerControlDedicatedSCell_r10;                                      /* optional; set in bit_mask
                              * uplinkPowerControlDedicatedSCell_r10_present if
                              * present */
                                                                                    /* Need ON */
        CQI_ReportConfigSCell_r10 cqi_ReportConfigSCell_r10;  /* optional; set
                                   * in bit_mask
                                   * cqi_ReportConfigSCell_r10_present if
                                   * present */
                                                              /* Need ON */
        SoundingRS_UL_ConfigDedicated soundingRS_UL_ConfigDedicated_r10;  
                                        /* optional; set in bit_mask
                                 * soundingRS_UL_ConfigDedicated_r10_present if
                                 * present */
                                                                          /* Need ON */
        SoundingRS_UL_ConfigDedicated_v1020 soundingRS_UL_ConfigDedicated_v1020;                                        /* optional; set in bit_mask
          * ul_Configuration_r10_soundingRS_UL_ConfigDedicated_v1020_present if
          * present */
                                                                                  /* Need ON */
        SoundingRS_UL_ConfigDedicatedAperiodic_r10 soundingRS_UL_ConfigDedicatedAperiodic_r10;                          /* optional; set in bit_mask
   * ul_Configuration_r10_soundingRS_UL_ConfigDedicatedAperiodic_r10_present if
   * present */
                                                                                                /* Need ON */
    } ul_Configuration_r10;  /* optional; set in bit_mask
             * PhysicalConfigDedicatedSCell_r10_ul_Configuration_r10_present if
             * present */
       /* Cond CommonUL */
    /* DL configuration as well as configuration applicable for DL and UL */
    struct CSI_RS_ConfigNZPToReleaseList_r11 *csi_RS_ConfigNZPToReleaseList_r11;                                        /* extension #1; optional; set in
                                   * bit_mask
  * PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigNZPToReleaseList_r11_present if
  * present */
                                                                                  /* Need ON */
    struct CSI_RS_ConfigNZPToAddModList_r11 *csi_RS_ConfigNZPToAddModList_r11;                                          /* extension #1; optional; set in
                                   * bit_mask
 * PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigNZPToAddModList_r11_present if
 * present */
                                                                                /* Need ON */
    struct CSI_RS_ConfigZPToReleaseList_r11 *csi_RS_ConfigZPToReleaseList_r11;                                          /* extension #1; optional; set in
                                   * bit_mask
 * PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigZPToReleaseList_r11_present if
 * present */
                                                                                /* Need ON */
    struct CSI_RS_ConfigZPToAddModList_r11 *csi_RS_ConfigZPToAddModList_r11;  
                                        /* extension #1; optional; set in
                                   * bit_mask
  * PhysicalConfigDedicatedSCell_r10_csi_RS_ConfigZPToAddModList_r11_present if
  * present */
                                                                              /* Need ON */
    EPDCCH_Config_r11 epdcch_Config_r11;  /* extension #1; optional; set in
                                   * bit_mask
                * PhysicalConfigDedicatedSCell_r10_epdcch_Config_r11_present if
                * present */
                                          /* Need ON */
    PDSCH_ConfigDedicated_v1130 pdsch_ConfigDedicated_v1130;  /* extension #1;
                                   * optional; set in bit_mask
      * PhysicalConfigDedicatedSCell_r10_pdsch_ConfigDedicated_v1130_present if
      * present */
                                                              /* Need ON */
	/* UL configuration */
    CQI_ReportConfig_v1130 cqi_ReportConfig_v1130;  /* extension #1; optional;
                                   * set in bit_mask
           * PhysicalConfigDedicatedSCell_r10_cqi_ReportConfig_v1130_present if
           * present */
                                                    /* Need ON */
    PUSCH_ConfigDedicated_v1130 pusch_ConfigDedicated_v1130;  /* extension #1;
                                   * optional; set in bit_mask
      * PhysicalConfigDedicatedSCell_r10_pusch_ConfigDedicated_v1130_present if
      * present */
                                                              /* Need ON */
    UplinkPowerControlDedicated_v1130 uplinkPowerControlDedicatedSCell_v1130;                                           /* extension #1; optional; set in
                                   * bit_mask
                            * uplinkPowerControlDedicatedSCell_v1130_present if
                            * present */
                                                                               /* Need ON */
    AntennaInfoDedicated_v1250 antennaInfo_v1250;  /* extension #2; optional;
                                   * set in bit_mask
                * PhysicalConfigDedicatedSCell_r10_antennaInfo_v1250_present if
                * present */
                                                   /* Need ON */
    EIMTA_MainConfigServCell_r12 eimta_MainConfigSCell_r12;  /* extension #2;
                                   * optional; set in bit_mask
                                   * eimta_MainConfigSCell_r12_present if
                                   * present */
                                                             /* Need ON */
    CQI_ReportConfig_v1250 cqi_ReportConfigSCell_v1250;  /* extension #2;
                                   * optional; set in bit_mask
                                   * cqi_ReportConfigSCell_v1250_present if
                                   * present */
                                                         /* Need ON */
    UplinkPowerControlDedicated_v1250 uplinkPowerControlDedicatedSCell_v1250;                                           /* extension #2; optional; set in
                                   * bit_mask
                            * uplinkPowerControlDedicatedSCell_v1250_present if
                            * present */
                                                                               /* Need ON */
    CSI_RS_Config_v1250 csi_RS_Config_v1250;  /* extension #2; optional; set in
                                   * bit_mask
              * PhysicalConfigDedicatedSCell_r10_csi_RS_Config_v1250_present if
              * present */
                                              /* Need ON */
} PhysicalConfigDedicatedSCell_r10;

typedef unsigned short  STAG_Id_r11;

typedef struct MAC_MainConfigSCell_r11 {
    unsigned char   bit_mask;
#       define      stag_Id_r11_present 0x80
    STAG_Id_r11     stag_Id_r11;  /* optional; set in bit_mask
                                   * stag_Id_r11_present if present */
                                  /* Need OP */
} MAC_MainConfigSCell_r11;

typedef struct RadioResourceConfigDedicatedSCell_r10 {
    unsigned char   bit_mask;
#       define      physicalConfigDedicatedSCell_r10_present 0x80
#       define      mac_MainConfigSCell_r11_present 0x40
#       define      RadioResourceConfigDedicatedSCell_r10_naics_Info_r12_present 0x20
	/* UE specific configuration extensions applicable for an SCell */
    PhysicalConfigDedicatedSCell_r10 physicalConfigDedicatedSCell_r10;  
                                  /* optional; set in bit_mask
                                   * physicalConfigDedicatedSCell_r10_present if
                                   * present */
                                                                        /* Need ON */
    MAC_MainConfigSCell_r11 mac_MainConfigSCell_r11;  /* extension #1; optional;
                                   * set in bit_mask
                                   * mac_MainConfigSCell_r11_present if
                                   * present */
                                                      /* Cond SCellAdd */
    NAICS_AssistanceInfo_r12 naics_Info_r12;  /* extension #2; optional; set in
                                   * bit_mask
              * RadioResourceConfigDedicatedSCell_r10_naics_Info_r12_present if
              * present */
                                              /* Need ON */
} RadioResourceConfigDedicatedSCell_r10;

typedef struct SCellToAddMod_r10 {
    unsigned char   bit_mask;
#       define      cellIdentification_r10_present 0x80
#       define      radioResourceConfigCommonSCell_r10_present 0x40
#       define      radioResourceConfigDedicatedSCell_r10_present 0x20
#       define      SCellToAddMod_r10_dl_CarrierFreq_v1090_present 0x10
#       define      antennaInfoDedicatedSCell_v10i0_present 0x08
    SCellIndex_r10  sCellIndex_r10;
    struct {
        PhysCellId      physCellId_r10;
        ARFCN_ValueEUTRA dl_CarrierFreq_r10;
    } cellIdentification_r10;  /* optional; set in bit_mask
                                * cellIdentification_r10_present if present */
       /* Cond SCellAdd */
    RadioResourceConfigCommonSCell_r10 radioResourceConfigCommonSCell_r10;  
                                        /* optional; set in bit_mask
                                * radioResourceConfigCommonSCell_r10_present if
                                * present */
                                                                            /* Cond SCellAdd */
    RadioResourceConfigDedicatedSCell_r10 radioResourceConfigDedicatedSCell_r10;                                        /* optional; set in bit_mask
                             * radioResourceConfigDedicatedSCell_r10_present if
                             * present */
                                                                                  /* Cond SCellAdd2 */
    ARFCN_ValueEUTRA_v9e0 dl_CarrierFreq_v1090;  /* extension #1; optional; set
                                   * in bit_mask
                            * SCellToAddMod_r10_dl_CarrierFreq_v1090_present if
                            * present */
                                                 /* Cond EARFCN-max */
    AntennaInfoDedicated_v10i0 antennaInfoDedicatedSCell_v10i0;  /* extension
                                   * #2; optional; set in bit_mask
                                   * antennaInfoDedicatedSCell_v10i0_present if
                                   * present */
                                                                 /* Need ON */
} SCellToAddMod_r10;

typedef struct SCellToAddModList_r10 {
    struct SCellToAddModList_r10 *next;
    SCellToAddMod_r10 value;
} *SCellToAddModList_r10;

typedef struct SCellToReleaseList_r10 {
    struct SCellToReleaseList_r10 *next;
    SCellIndex_r10  value;
} *SCellToReleaseList_r10;

typedef struct CarrierFreqListUTRA_TDD_r10 {
    struct CarrierFreqListUTRA_TDD_r10 *next;
    ARFCN_ValueUTRA value;
} *CarrierFreqListUTRA_TDD_r10;

typedef struct FreqPriorityEUTRA {
    ARFCN_ValueEUTRA carrierFreq;
    CellReselectionPriority cellReselectionPriority;
} FreqPriorityEUTRA;

typedef struct FreqPriorityListEUTRA {
    struct FreqPriorityListEUTRA *next;
    FreqPriorityEUTRA value;
} *FreqPriorityListEUTRA;

typedef struct FreqPriorityEUTRA_r12 {
    ARFCN_ValueEUTRA_r9 carrierFreq_r12;
    CellReselectionPriority cellReselectionPriority_r12;
} FreqPriorityEUTRA_r12;

typedef struct FreqPriorityListExtEUTRA_r12 {
    struct FreqPriorityListExtEUTRA_r12 *next;
    FreqPriorityEUTRA_r12 value;
} *FreqPriorityListExtEUTRA_r12;

typedef struct FreqsPriorityGERAN {
    CarrierFreqsGERAN carrierFreqs;
    CellReselectionPriority cellReselectionPriority;
} FreqsPriorityGERAN;

typedef struct FreqsPriorityListGERAN {
    struct FreqsPriorityListGERAN *next;
    FreqsPriorityGERAN value;
} *FreqsPriorityListGERAN;

typedef struct FreqPriorityUTRA_FDD {
    ARFCN_ValueUTRA carrierFreq;
    CellReselectionPriority cellReselectionPriority;
} FreqPriorityUTRA_FDD;

typedef struct FreqPriorityListUTRA_FDD {
    struct FreqPriorityListUTRA_FDD *next;
    FreqPriorityUTRA_FDD value;
} *FreqPriorityListUTRA_FDD;

typedef struct FreqPriorityUTRA_TDD {
    ARFCN_ValueUTRA carrierFreq;
    CellReselectionPriority cellReselectionPriority;
} FreqPriorityUTRA_TDD;

typedef struct FreqPriorityListUTRA_TDD {
    struct FreqPriorityListUTRA_TDD *next;
    FreqPriorityUTRA_TDD value;
} *FreqPriorityListUTRA_TDD;

typedef struct BandClassPriorityHRPD {
    BandclassCDMA2000 bandClass;
    CellReselectionPriority cellReselectionPriority;
} BandClassPriorityHRPD;

typedef struct BandClassPriorityListHRPD {
    struct BandClassPriorityListHRPD *next;
    BandClassPriorityHRPD value;
} *BandClassPriorityListHRPD;

typedef struct BandClassPriority1XRTT {
    BandclassCDMA2000 bandClass;
    CellReselectionPriority cellReselectionPriority;
} BandClassPriority1XRTT;

typedef struct BandClassPriorityList1XRTT {
    struct BandClassPriorityList1XRTT *next;
    BandClassPriority1XRTT value;
} *BandClassPriorityList1XRTT;

typedef struct CellInfoGERAN_r9 {
    PhysCellIdGERAN physCellId_r9;
    CarrierFreqGERAN carrierFreq_r9;
    struct SystemInfoListGERAN *systemInformation_r9;
} CellInfoGERAN_r9;

typedef struct CellInfoListGERAN_r9 {
    struct CellInfoListGERAN_r9 *next;
    CellInfoGERAN_r9 value;
} *CellInfoListGERAN_r9;

typedef struct CellInfoUTRA_FDD_r9 {
    PhysCellIdUTRA_FDD physCellId_r9;
    _octet1         utra_BCCH_Container_r9;
} CellInfoUTRA_FDD_r9;

typedef struct CellInfoListUTRA_FDD_r9 {
    struct CellInfoListUTRA_FDD_r9 *next;
    CellInfoUTRA_FDD_r9 value;
} *CellInfoListUTRA_FDD_r9;

typedef struct CellInfoUTRA_TDD_r9 {
    PhysCellIdUTRA_TDD physCellId_r9;
    _octet1         utra_BCCH_Container_r9;
} CellInfoUTRA_TDD_r9;

typedef struct CellInfoListUTRA_TDD_r9 {
    struct CellInfoListUTRA_TDD_r9 *next;
    CellInfoUTRA_TDD_r9 value;
} *CellInfoListUTRA_TDD_r9;

typedef struct CellInfoUTRA_TDD_r10 {
    PhysCellIdUTRA_TDD physCellId_r10;
    ARFCN_ValueUTRA carrierFreq_r10;
    _octet1         utra_BCCH_Container_r10;
} CellInfoUTRA_TDD_r10;

typedef struct CellInfoListUTRA_TDD_r10 {
    struct CellInfoListUTRA_TDD_r10 *next;
    CellInfoUTRA_TDD_r10 value;
} *CellInfoListUTRA_TDD_r10;

typedef struct SL_DestinationIdentity_r12 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} SL_DestinationIdentity_r12;

typedef struct SL_DestinationInfoList_r12 {
    struct SL_DestinationInfoList_r12 *next;
    SL_DestinationIdentity_r12 value;
} *SL_DestinationInfoList_r12;

typedef struct PLMN_IdentityInfo {
    PLMN_Identity   plmn_Identity;
    enum {
        cellReservedForOperatorUse_reserved = 0,
        notReserved = 1
    } cellReservedForOperatorUse;
} PLMN_IdentityInfo;

typedef struct PLMN_IdentityList {
    struct PLMN_IdentityList *next;
    PLMN_IdentityInfo value;
} *PLMN_IdentityList;

typedef struct SchedulingInfo {
    enum {
        si_Periodicity_rf8 = 0,
        si_Periodicity_rf16 = 1,
        si_Periodicity_rf32 = 2,
        si_Periodicity_rf64 = 3,
        si_Periodicity_rf128 = 4,
        si_Periodicity_rf256 = 5,
        si_Periodicity_rf512 = 6
    } si_Periodicity;
    struct SIB_MappingInfo *sib_MappingInfo;
} SchedulingInfo;

typedef struct SchedulingInfoList {
    struct SchedulingInfoList *next;
    SchedulingInfo  value;
} *SchedulingInfoList;

typedef enum SIB_Type {
    sibType3 = 0,
    sibType4 = 1,
    sibType5 = 2,
    sibType6 = 3,
    sibType7 = 4,
    sibType8 = 5,
    sibType9 = 6,
    sibType10 = 7,
    sibType11 = 8,
    sibType12_v920 = 9,
    sibType13_v920 = 10,
    sibType14_v1130 = 11,
    sibType15_v1130 = 12,
    sibType16_v1130 = 13,
    sibType17_v1250 = 14,
    sibType18_v1250 = 15,
    sibType19_v1250 = 16
} SIB_Type;

typedef struct SIB_MappingInfo {
    struct SIB_MappingInfo *next;
    SIB_Type        value;
} *SIB_MappingInfo;

typedef enum RAT_Type {
    RAT_Type_eutra = 0,
    RAT_Type_utra = 1,
    geran_cs = 2,
    geran_ps = 3,
    RAT_Type_cdma2000_1XRTT = 4,
    RAT_Type_spare3 = 5,
    RAT_Type_spare2 = 6,
    RAT_Type_spare1 = 7
} RAT_Type;

typedef struct UE_CapabilityRequest {
    struct UE_CapabilityRequest *next;
    RAT_Type        value;
} *UE_CapabilityRequest;

typedef struct MeasResult2EUTRA_r9 {
    ARFCN_ValueEUTRA carrierFreq_r9;
    struct MeasResultListEUTRA *measResultList_r9;
} MeasResult2EUTRA_r9;

typedef struct MeasResultList2EUTRA_r9 {
    struct MeasResultList2EUTRA_r9 *next;
    MeasResult2EUTRA_r9 value;
} *MeasResultList2EUTRA_r9;

typedef struct MeasResult2EUTRA_v9e0 {
    unsigned char   bit_mask;
#       define      MeasResult2EUTRA_v9e0_carrierFreq_v9e0_present 0x80
    ARFCN_ValueEUTRA_v9e0 carrierFreq_v9e0;  /* optional; set in bit_mask
                            * MeasResult2EUTRA_v9e0_carrierFreq_v9e0_present if
                            * present */
} MeasResult2EUTRA_v9e0;

typedef struct MeasResultList2EUTRA_v9e0 {
    struct MeasResultList2EUTRA_v9e0 *next;
    MeasResult2EUTRA_v9e0 value;
} *MeasResultList2EUTRA_v9e0;

typedef struct MeasResult2EUTRA_v1250 {
    unsigned char   bit_mask;
#       define      rsrq_Type_r12_present 0x80
    RSRQ_Type_r12   rsrq_Type_r12;  /* optional; set in bit_mask
                                     * rsrq_Type_r12_present if present */
} MeasResult2EUTRA_v1250;

typedef struct MeasResultList2EUTRA_v1250 {
    struct MeasResultList2EUTRA_v1250 *next;
    MeasResult2EUTRA_v1250 value;
} *MeasResultList2EUTRA_v1250;

typedef struct MeasResult2UTRA_r9 {
    ARFCN_ValueUTRA carrierFreq_r9;
    struct MeasResultListUTRA *measResultList_r9;
} MeasResult2UTRA_r9;

typedef struct MeasResultList2UTRA_r9 {
    struct MeasResultList2UTRA_r9 *next;
    MeasResult2UTRA_r9 value;
} *MeasResultList2UTRA_r9;

typedef struct MeasResult2CDMA2000_r9 {
    CarrierFreqCDMA2000 carrierFreq_r9;
    MeasResultsCDMA2000 measResultList_r9;
} MeasResult2CDMA2000_r9;

typedef struct MeasResultList2CDMA2000_r9 {
    struct MeasResultList2CDMA2000_r9 *next;
    MeasResult2CDMA2000_r9 value;
} *MeasResultList2CDMA2000_r9;

typedef struct LogMeasInfo_r10 {
    unsigned char   bit_mask;
#       define      LogMeasInfo_r10_locationInfo_r10_present 0x80
#       define      measResultNeighCells_r10_present 0x40
#       define      measResultListEUTRA_v1090_present 0x20
#       define      measResultListMBSFN_r12_present 0x10
#       define      measResultServCell_v1250_present 0x08
#       define      servCellRSRQ_Type_r12_present 0x04
#       define      LogMeasInfo_r10_measResultListEUTRA_v1250_present 0x02
    LocationInfo_r10 locationInfo_r10;  /* optional; set in bit_mask
                                  * LogMeasInfo_r10_locationInfo_r10_present if
                                  * present */
    unsigned short  relativeTimeStamp_r10;
    CellGlobalIdEUTRA servCellIdentity_r10;
    struct {
        RSRP_Range      rsrpResult_r10;
        RSRQ_Range      rsrqResult_r10;
    } measResultServCell_r10;
    struct {
        unsigned char   bit_mask;
#           define      measResultListEUTRA_r10_present 0x80
#           define      measResultListUTRA_r10_present 0x40
#           define      measResultListGERAN_r10_present 0x20
#           define      measResultListCDMA2000_r10_present 0x10
        struct MeasResultList2EUTRA_r9 *measResultListEUTRA_r10;  /* optional;
                                   * set in bit_mask
                                   * measResultListEUTRA_r10_present if
                                   * present */
        struct MeasResultList2UTRA_r9 *measResultListUTRA_r10;  /* optional; set
                                   * in bit_mask measResultListUTRA_r10_present
                                   * if present */
        struct MeasResultList2GERAN_r10 *measResultListGERAN_r10;  /* optional;
                                   * set in bit_mask
                                   * measResultListGERAN_r10_present if
                                   * present */
        struct MeasResultList2CDMA2000_r9 *measResultListCDMA2000_r10;  
                                  /* optional; set in bit_mask
                                   * measResultListCDMA2000_r10_present if
                                   * present */
    } measResultNeighCells_r10;  /* optional; set in bit_mask
                                  * measResultNeighCells_r10_present if
                                  * present */
    struct MeasResultList2EUTRA_v9e0 *measResultListEUTRA_v1090;  /* extension
                                   * #1; optional; set in bit_mask
                                   * measResultListEUTRA_v1090_present if
                                   * present */
    struct MeasResultListMBSFN_r12 *measResultListMBSFN_r12;  /* extension #2;
                                   * optional; set in bit_mask
                                   * measResultListMBSFN_r12_present if
                                   * present */
    RSRQ_Range_v1250 measResultServCell_v1250;  /* extension #2; optional; set
                                   * in bit_mask
                                   * measResultServCell_v1250_present if
                                   * present */
    RSRQ_Type_r12   servCellRSRQ_Type_r12;  /* extension #2; optional; set in
                                   * bit_mask servCellRSRQ_Type_r12_present if
                                   * present */
    struct MeasResultList2EUTRA_v1250 *measResultListEUTRA_v1250;  /* extension
                                   * #2; optional; set in bit_mask
                         * LogMeasInfo_r10_measResultListEUTRA_v1250_present if
                         * present */
} LogMeasInfo_r10;

typedef struct LogMeasInfoList_r10 {
    struct LogMeasInfoList_r10 *next;
    LogMeasInfo_r10 value;
} *LogMeasInfoList_r10;

typedef unsigned short  MBSFN_RSRQ_Range_r12;

typedef unsigned short  BLER_Range_r12;

typedef struct BLER_Result_r12 {
    BLER_Range_r12  bler_r12;
    struct {
        _bit1           n_r12;
        _bit1           m_r12;
    } blocksReceived_r12;
} BLER_Result_r12;

typedef struct MeasResultMBSFN_r12 {
    unsigned char   bit_mask;
#       define      signallingBLER_Result_r12_present 0x80
#       define      dataBLER_MCH_ResultList_r12_present 0x40
    struct {
        MBSFN_AreaId_r12 mbsfn_AreaId_r12;
        ARFCN_ValueEUTRA_r9 carrierFreq_r12;
    } mbsfn_Area_r12;
    RSRP_Range      rsrpResultMBSFN_r12;
    MBSFN_RSRQ_Range_r12 rsrqResultMBSFN_r12;
    BLER_Result_r12 signallingBLER_Result_r12;  /* optional; set in bit_mask
                                         * signallingBLER_Result_r12_present if
                                         * present */
    struct DataBLER_MCH_ResultList_r12 *dataBLER_MCH_ResultList_r12;  
                                  /* optional; set in bit_mask
                                   * dataBLER_MCH_ResultList_r12_present if
                                   * present */
} MeasResultMBSFN_r12;

typedef struct MeasResultListMBSFN_r12 {
    struct MeasResultListMBSFN_r12 *next;
    MeasResultMBSFN_r12 value;
} *MeasResultListMBSFN_r12;

typedef struct DataBLER_MCH_Result_r12 {
    unsigned short  mch_Index_r12;
    BLER_Result_r12 dataBLER_Result_r12;
} DataBLER_MCH_Result_r12;

typedef struct DataBLER_MCH_ResultList_r12 {
    struct DataBLER_MCH_ResultList_r12 *next;
    DataBLER_MCH_Result_r12 value;
} *DataBLER_MCH_ResultList_r12;

typedef struct MeasResultList2GERAN_r10 {
    struct MeasResultList2GERAN_r10 *next;
    struct MeasResultListGERAN *value;
} *MeasResultList2GERAN_r10;

typedef struct MBSFN_SubframeConfigList {
    struct MBSFN_SubframeConfigList *next;
    MBSFN_SubframeConfig value;
} *MBSFN_SubframeConfigList;

typedef struct AC_BarringPerPLMN_r12 {
    unsigned char   bit_mask;
#       define      ac_BarringInfo_r12_present 0x80
#       define      AC_BarringPerPLMN_r12_ac_BarringSkipForMMTELVoice_r12_present 0x40
#       define      AC_BarringPerPLMN_r12_ac_BarringSkipForMMTELVideo_r12_present 0x20
#       define      AC_BarringPerPLMN_r12_ac_BarringSkipForSMS_r12_present 0x10
#       define      ac_BarringForCSFB_r12_present 0x08
#       define      ssac_BarringForMMTEL_Voice_r12_present 0x04
#       define      ssac_BarringForMMTEL_Video_r12_present 0x02
    unsigned short  plmn_IdentityIndex_r12;
    struct {
        unsigned char   bit_mask;
#           define      ac_BarringForMO_Signalling_r12_present 0x80
#           define      ac_BarringForMO_Data_r12_present 0x40
        ossBoolean      ac_BarringForEmergency_r12;
        AC_BarringConfig ac_BarringForMO_Signalling_r12;  /* optional; set in
                                   * bit_mask
                                   * ac_BarringForMO_Signalling_r12_present if
                                   * present */
                                                          /* Need OP */
        AC_BarringConfig ac_BarringForMO_Data_r12;  /* optional; set in bit_mask
                                          * ac_BarringForMO_Data_r12_present if
                                          * present */
                                                    /* Need OP */
    } ac_BarringInfo_r12;  /* optional; set in bit_mask
                            * ac_BarringInfo_r12_present if present */
       /* Need OP */
    _enum3          ac_BarringSkipForMMTELVoice_r12;  /* optional; set in
                                   * bit_mask
             * AC_BarringPerPLMN_r12_ac_BarringSkipForMMTELVoice_r12_present if
             * present */
       /* Need OP */
    _enum3          ac_BarringSkipForMMTELVideo_r12;  /* optional; set in
                                   * bit_mask
             * AC_BarringPerPLMN_r12_ac_BarringSkipForMMTELVideo_r12_present if
             * present */
       /* Need OP */
    _enum3          ac_BarringSkipForSMS_r12;  /* optional; set in bit_mask
                    * AC_BarringPerPLMN_r12_ac_BarringSkipForSMS_r12_present if
                    * present */
       /* Need OP */
    AC_BarringConfig ac_BarringForCSFB_r12;  /* optional; set in bit_mask
                                              * ac_BarringForCSFB_r12_present if
                                              * present */
                                             /* Need OP */
    AC_BarringConfig ssac_BarringForMMTEL_Voice_r12;  /* optional; set in
                                   * bit_mask
                                   * ssac_BarringForMMTEL_Voice_r12_present if
                                   * present */
                                                      /* Need OP */
    AC_BarringConfig ssac_BarringForMMTEL_Video_r12;  /* optional; set in
                                   * bit_mask
                                   * ssac_BarringForMMTEL_Video_r12_present if
                                   * present */
                                                      /* Need OP */
} AC_BarringPerPLMN_r12;

typedef struct AC_BarringPerPLMN_List_r12 {
    struct AC_BarringPerPLMN_List_r12 *next;
    AC_BarringPerPLMN_r12 value;
} *AC_BarringPerPLMN_List_r12;

typedef enum Q_OffsetRange {
    dB_24 = 0,
    dB_22 = 1,
    dB_20 = 2,
    dB_18 = 3,
    dB_16 = 4,
    dB_14 = 5,
    dB_12 = 6,
    dB_10 = 7,
    dB_8 = 8,
    Q_OffsetRange_dB_6 = 9,
    dB_5 = 10,
    Q_OffsetRange_dB_4 = 11,
    Q_OffsetRange_dB_3 = 12,
    Q_OffsetRange_dB_2 = 13,
    Q_OffsetRange_dB_1 = 14,
    Q_OffsetRange_dB0 = 15,
    Q_OffsetRange_dB1 = 16,
    Q_OffsetRange_dB2 = 17,
    Q_OffsetRange_dB3 = 18,
    Q_OffsetRange_dB4 = 19,
    Q_OffsetRange_dB5 = 20,
    Q_OffsetRange_dB6 = 21,
    Q_OffsetRange_dB8 = 22,
    Q_OffsetRange_dB10 = 23,
    Q_OffsetRange_dB12 = 24,
    Q_OffsetRange_dB14 = 25,
    Q_OffsetRange_dB16 = 26,
    Q_OffsetRange_dB18 = 27,
    Q_OffsetRange_dB20 = 28,
    Q_OffsetRange_dB22 = 29,
    Q_OffsetRange_dB24 = 30
} Q_OffsetRange;

typedef struct IntraFreqNeighCellInfo {
    PhysCellId      physCellId;
    Q_OffsetRange   q_OffsetCell;
} IntraFreqNeighCellInfo;

typedef struct IntraFreqNeighCellList {
    struct IntraFreqNeighCellList *next;
    IntraFreqNeighCellInfo value;
} *IntraFreqNeighCellList;

typedef struct IntraFreqBlackCellList {
    struct IntraFreqBlackCellList *next;
    PhysCellIdRange value;
} *IntraFreqBlackCellList;

typedef struct _seq73 {
    ReselectionThresholdQ_r9 threshX_HighQ_r9;
    ReselectionThresholdQ_r9 threshX_LowQ_r9;
} _seq73;

typedef struct InterFreqCarrierFreqInfo {
    unsigned short  bit_mask;
#       define      InterFreqCarrierFreqInfo_p_Max_present 0x8000
#       define      InterFreqCarrierFreqInfo_t_ReselectionEUTRA_SF_present 0x4000
#       define      InterFreqCarrierFreqInfo_cellReselectionPriority_present 0x2000
#       define      q_OffsetFreq_present 0x1000
#       define      interFreqNeighCellList_present 0x0800
#       define      interFreqBlackCellList_present 0x0400
#       define      InterFreqCarrierFreqInfo_q_QualMin_r9_present 0x0200
#       define      InterFreqCarrierFreqInfo_threshX_Q_r9_present 0x0100
#       define      InterFreqCarrierFreqInfo_q_QualMinWB_r11_present 0x0080
    ARFCN_ValueEUTRA dl_CarrierFreq;
    Q_RxLevMin      q_RxLevMin;
    P_Max           p_Max;  /* optional; set in bit_mask
                             * InterFreqCarrierFreqInfo_p_Max_present if
                             * present */
                            /* Need OP */
    T_Reselection   t_ReselectionEUTRA;
    SpeedStateScaleFactors t_ReselectionEUTRA_SF;  /* optional; set in bit_mask
                    * InterFreqCarrierFreqInfo_t_ReselectionEUTRA_SF_present if
                    * present */
                                                   /* Need OP */
    ReselectionThreshold threshX_High;
    ReselectionThreshold threshX_Low;
    AllowedMeasBandwidth allowedMeasBandwidth;
    PresenceAntennaPort1 presenceAntennaPort1;
    CellReselectionPriority cellReselectionPriority;  /* optional; set in
                                   * bit_mask
                  * InterFreqCarrierFreqInfo_cellReselectionPriority_present if
                  * present */
                                                      /* Need OP */
    NeighCellConfig neighCellConfig;
    Q_OffsetRange   q_OffsetFreq;  /* q_OffsetFreq_present not set in bit_mask
                                    * implies value is dB0 */
    struct InterFreqNeighCellList *interFreqNeighCellList;  /* optional; set in
                                   * bit_mask interFreqNeighCellList_present if
                                   * present */
                                                            /* Need OR */
    struct InterFreqBlackCellList *interFreqBlackCellList;  /* optional; set in
                                   * bit_mask interFreqBlackCellList_present if
                                   * present */
                                                            /* Need OR */
    Q_QualMin_r9    q_QualMin_r9;  /* extension #1; optional; set in bit_mask
                             * InterFreqCarrierFreqInfo_q_QualMin_r9_present if
                             * present */
                                   /* Need OP */
    _seq73          threshX_Q_r9;  /* extension #1; optional; set in bit_mask
                             * InterFreqCarrierFreqInfo_threshX_Q_r9_present if
                             * present */
    /* Cond RSRQ */
    Q_QualMin_r9    q_QualMinWB_r11;  /* extension #2; optional; set in bit_mask
                          * InterFreqCarrierFreqInfo_q_QualMinWB_r11_present if
                          * present */
                                      /* Cond WB-RSRQ */
} InterFreqCarrierFreqInfo;

typedef struct InterFreqCarrierFreqList {
    struct InterFreqCarrierFreqList *next;
    InterFreqCarrierFreqInfo value;
} *InterFreqCarrierFreqList;

typedef struct InterFreqCarrierFreqInfo_v1250 {
    unsigned char   bit_mask;
#       define      InterFreqCarrierFreqInfo_v1250_reducedMeasPerformance_r12_present 0x80
#       define      InterFreqCarrierFreqInfo_v1250_q_QualMinRSRQ_OnAllSymbols_r12_present 0x40
    _enum3          reducedMeasPerformance_r12;  /* optional; set in bit_mask
         * InterFreqCarrierFreqInfo_v1250_reducedMeasPerformance_r12_present if
         * present */
               /* Need OP */
    Q_QualMin_r9    q_QualMinRSRQ_OnAllSymbols_r12;  /* optional; set in
                                   * bit_mask
     * InterFreqCarrierFreqInfo_v1250_q_QualMinRSRQ_OnAllSymbols_r12_present if
     * present */
                                                     /* Cond RSRQ2 */
} InterFreqCarrierFreqInfo_v1250;

typedef struct InterFreqCarrierFreqList_v1250 {
    struct InterFreqCarrierFreqList_v1250 *next;
    InterFreqCarrierFreqInfo_v1250 value;
} *InterFreqCarrierFreqList_v1250;

typedef struct _seq74 {
    ReselectionThresholdQ_r9 threshX_HighQ_r12;
    ReselectionThresholdQ_r9 threshX_LowQ_r12;
} _seq74;

typedef struct InterFreqCarrierFreqInfo_r12 {
    unsigned short  bit_mask;
#       define      InterFreqCarrierFreqInfo_r12_p_Max_r12_present 0x8000
#       define      t_ReselectionEUTRA_SF_r12_present 0x4000
#       define      InterFreqCarrierFreqInfo_r12_cellReselectionPriority_r12_present 0x2000
#       define      q_OffsetFreq_r12_present 0x1000
#       define      interFreqNeighCellList_r12_present 0x0800
#       define      interFreqBlackCellList_r12_present 0x0400
#       define      q_QualMin_r12_present 0x0200
#       define      InterFreqCarrierFreqInfo_r12_threshX_Q_r12_present 0x0100
#       define      q_QualMinWB_r12_present 0x0080
#       define      InterFreqCarrierFreqInfo_r12_multiBandInfoList_r12_present 0x0040
#       define      InterFreqCarrierFreqInfo_r12_reducedMeasPerformance_r12_present 0x0020
#       define      InterFreqCarrierFreqInfo_r12_q_QualMinRSRQ_OnAllSymbols_r12_present 0x0010
    ARFCN_ValueEUTRA_r9 dl_CarrierFreq_r12;
    Q_RxLevMin      q_RxLevMin_r12;
    P_Max           p_Max_r12;  /* optional; set in bit_mask
                            * InterFreqCarrierFreqInfo_r12_p_Max_r12_present if
                            * present */
                                /* Need OP */
    T_Reselection   t_ReselectionEUTRA_r12;
    SpeedStateScaleFactors t_ReselectionEUTRA_SF_r12;  /* optional; set in
                                   * bit_mask t_ReselectionEUTRA_SF_r12_present
                                   * if present */
                                                       /* Need OP */
    ReselectionThreshold threshX_High_r12;
    ReselectionThreshold threshX_Low_r12;
    AllowedMeasBandwidth allowedMeasBandwidth_r12;
    PresenceAntennaPort1 presenceAntennaPort1_r12;
    CellReselectionPriority cellReselectionPriority_r12;  /* optional; set in
                                   * bit_mask
          * InterFreqCarrierFreqInfo_r12_cellReselectionPriority_r12_present if
          * present */
                                                          /* Need OP */
    NeighCellConfig neighCellConfig_r12;
    Q_OffsetRange   q_OffsetFreq_r12;  /* q_OffsetFreq_r12_present not set in
                                        * bit_mask implies value is dB0 */
    struct InterFreqNeighCellList *interFreqNeighCellList_r12;  /* optional; set
                                   * in bit_mask
                                   * interFreqNeighCellList_r12_present if
                                   * present */
                                                                /* Need OR */
    struct InterFreqBlackCellList *interFreqBlackCellList_r12;  /* optional; set
                                   * in bit_mask
                                   * interFreqBlackCellList_r12_present if
                                   * present */
                                                                /* Need OR */
    Q_QualMin_r9    q_QualMin_r12;  /* optional; set in bit_mask
                                     * q_QualMin_r12_present if present */
                                    /* Need OP */
    _seq74          threshX_Q_r12;  /* optional; set in bit_mask
                        * InterFreqCarrierFreqInfo_r12_threshX_Q_r12_present if
                        * present */
       /* Cond RSRQ */
    Q_QualMin_r9    q_QualMinWB_r12;  /* optional; set in bit_mask
                                       * q_QualMinWB_r12_present if present */
                                      /* Cond WB-RSRQ */
    struct MultiBandInfoList_r11 *multiBandInfoList_r12;  /* optional; set in
                                   * bit_mask
                * InterFreqCarrierFreqInfo_r12_multiBandInfoList_r12_present if
                * present */
                                                          /* Need OR */
    _enum3          reducedMeasPerformance_r12;  /* optional; set in bit_mask
           * InterFreqCarrierFreqInfo_r12_reducedMeasPerformance_r12_present if
           * present */
       /* Need OP */
    Q_QualMin_r9    q_QualMinRSRQ_OnAllSymbols_r12;  /* optional; set in
                                   * bit_mask
       * InterFreqCarrierFreqInfo_r12_q_QualMinRSRQ_OnAllSymbols_r12_present if
       * present */
                                                     /* Cond RSRQ2 */
} InterFreqCarrierFreqInfo_r12;

typedef struct InterFreqCarrierFreqListExt_r12 {
    struct InterFreqCarrierFreqListExt_r12 *next;
    InterFreqCarrierFreqInfo_r12 value;
} *InterFreqCarrierFreqListExt_r12;

typedef struct InterFreqNeighCellInfo {
    PhysCellId      physCellId;
    Q_OffsetRange   q_OffsetCell;
} InterFreqNeighCellInfo;

typedef struct InterFreqNeighCellList {
    struct InterFreqNeighCellList *next;
    InterFreqNeighCellInfo value;
} *InterFreqNeighCellList;

typedef struct InterFreqBlackCellList {
    struct InterFreqBlackCellList *next;
    PhysCellIdRange value;
} *InterFreqBlackCellList;

typedef struct CarrierFreqUTRA_FDD {
    unsigned char   bit_mask;
#       define      CarrierFreqUTRA_FDD_cellReselectionPriority_present 0x80
#       define      CarrierFreqUTRA_FDD_threshX_Q_r9_present 0x40
    ARFCN_ValueUTRA carrierFreq;
    CellReselectionPriority cellReselectionPriority;  /* optional; set in
                                   * bit_mask
                       * CarrierFreqUTRA_FDD_cellReselectionPriority_present if
                       * present */
                                                      /* Need OP */
    ReselectionThreshold threshX_High;
    ReselectionThreshold threshX_Low;
    short           q_RxLevMin;
    short           p_MaxUTRA;
    short           q_QualMin;
    _seq73          threshX_Q_r9;  /* extension #1; optional; set in bit_mask
                                    * CarrierFreqUTRA_FDD_threshX_Q_r9_present
                                    * if present */
                                   /* Cond RSRQ */
} CarrierFreqUTRA_FDD;

typedef struct CarrierFreqListUTRA_FDD {
    struct CarrierFreqListUTRA_FDD *next;
    CarrierFreqUTRA_FDD value;
} *CarrierFreqListUTRA_FDD;

typedef struct CarrierFreqUTRA_FDD_Ext_r12 {
    unsigned char   bit_mask;
#       define      CarrierFreqUTRA_FDD_Ext_r12_cellReselectionPriority_r12_present 0x80
#       define      CarrierFreqUTRA_FDD_Ext_r12_threshX_Q_r12_present 0x40
#       define      CarrierFreqUTRA_FDD_Ext_r12_multiBandInfoList_r12_present 0x20
#       define      CarrierFreqUTRA_FDD_Ext_r12_reducedMeasPerformance_r12_present 0x10
    ARFCN_ValueUTRA carrierFreq_r12;
    CellReselectionPriority cellReselectionPriority_r12;  /* optional; set in
                                   * bit_mask
           * CarrierFreqUTRA_FDD_Ext_r12_cellReselectionPriority_r12_present if
           * present */
                                                          /* Need OP */
    ReselectionThreshold threshX_High_r12;
    ReselectionThreshold threshX_Low_r12;
    short           q_RxLevMin_r12;
    short           p_MaxUTRA_r12;
    short           q_QualMin_r12;
    _seq74          threshX_Q_r12;  /* optional; set in bit_mask
                         * CarrierFreqUTRA_FDD_Ext_r12_threshX_Q_r12_present if
                         * present */
               /* Cond RSRQ */
    struct _seqof4  *multiBandInfoList_r12;  /* optional; set in bit_mask
                 * CarrierFreqUTRA_FDD_Ext_r12_multiBandInfoList_r12_present if
                 * present */
                                             /* Need OR */
    _enum3          reducedMeasPerformance_r12;  /* optional; set in bit_mask
            * CarrierFreqUTRA_FDD_Ext_r12_reducedMeasPerformance_r12_present if
            * present */
       /* Need OP */
} CarrierFreqUTRA_FDD_Ext_r12;

typedef struct CarrierFreqListUTRA_FDD_Ext_r12 {
    struct CarrierFreqListUTRA_FDD_Ext_r12 *next;
    CarrierFreqUTRA_FDD_Ext_r12 value;
} *CarrierFreqListUTRA_FDD_Ext_r12;

typedef struct CarrierFreqUTRA_TDD {
    unsigned char   bit_mask;
#       define      CarrierFreqUTRA_TDD_cellReselectionPriority_present 0x80
    ARFCN_ValueUTRA carrierFreq;
    CellReselectionPriority cellReselectionPriority;  /* optional; set in
                                   * bit_mask
                       * CarrierFreqUTRA_TDD_cellReselectionPriority_present if
                       * present */
                                                      /* Need OP */
    ReselectionThreshold threshX_High;
    ReselectionThreshold threshX_Low;
    short           q_RxLevMin;
    short           p_MaxUTRA;
} CarrierFreqUTRA_TDD;

typedef struct CarrierFreqListUTRA_TDD {
    struct CarrierFreqListUTRA_TDD *next;
    CarrierFreqUTRA_TDD value;
} *CarrierFreqListUTRA_TDD;

typedef struct CarrierFreqUTRA_TDD_r12 {
    unsigned char   bit_mask;
#       define      CarrierFreqUTRA_TDD_r12_cellReselectionPriority_r12_present 0x80
#       define      CarrierFreqUTRA_TDD_r12_reducedMeasPerformance_r12_present 0x40
    ARFCN_ValueUTRA carrierFreq_r12;
    CellReselectionPriority cellReselectionPriority_r12;  /* optional; set in
                                   * bit_mask
               * CarrierFreqUTRA_TDD_r12_cellReselectionPriority_r12_present if
               * present */
                                                          /* Need OP */
    ReselectionThreshold threshX_High_r12;
    ReselectionThreshold threshX_Low_r12;
    short           q_RxLevMin_r12;
    short           p_MaxUTRA_r12;
    _enum3          reducedMeasPerformance_r12;  /* optional; set in bit_mask
                * CarrierFreqUTRA_TDD_r12_reducedMeasPerformance_r12_present if
                * present */
       /* Need OP */
} CarrierFreqUTRA_TDD_r12;

typedef struct CarrierFreqListUTRA_TDD_Ext_r12 {
    struct CarrierFreqListUTRA_TDD_Ext_r12 *next;
    CarrierFreqUTRA_TDD_r12 value;
} *CarrierFreqListUTRA_TDD_Ext_r12;

typedef struct CarrierFreqsInfoGERAN {
    CarrierFreqsGERAN carrierFreqs;
    struct {
        unsigned char   bit_mask;
#           define      commonInfo_cellReselectionPriority_present 0x80
#           define      p_MaxGERAN_present 0x40
        CellReselectionPriority cellReselectionPriority;  /* optional; set in
                                   * bit_mask
                                   * commonInfo_cellReselectionPriority_present
                                   * if present */
                                                          /* Need OP */
        _bit1           ncc_Permitted;
        unsigned short  q_RxLevMin;
        unsigned short  p_MaxGERAN;  /* optional; set in bit_mask
                                      * p_MaxGERAN_present if present */
                                     /* Need OP */
        ReselectionThreshold threshX_High;
        ReselectionThreshold threshX_Low;
    } commonInfo;
} CarrierFreqsInfoGERAN;

typedef struct CarrierFreqsInfoListGERAN {
    struct CarrierFreqsInfoListGERAN *next;
    CarrierFreqsInfoGERAN value;
} *CarrierFreqsInfoListGERAN;

typedef unsigned short  PhysCellIdCDMA2000;

typedef struct NeighCellsPerBandclassCDMA2000_r11 {
    ARFCN_ValueCDMA2000 arfcn;
    struct _seqof18 {
        struct _seqof18 *next;
        PhysCellIdCDMA2000 value;
    } *physCellIdList_r11;
} NeighCellsPerBandclassCDMA2000_r11;

typedef struct NeighCellCDMA2000_r11 {
    BandclassCDMA2000 bandClass;
    struct _seqof19 {
        struct _seqof19 *next;
        NeighCellsPerBandclassCDMA2000_r11 value;
    } *neighFreqInfoList_r11;
} NeighCellCDMA2000_r11;

typedef struct CellReselectionParametersCDMA2000_r11 {
    unsigned char   bit_mask;
#       define      CellReselectionParametersCDMA2000_r11_t_ReselectionCDMA2000_SF_present 0x80
    struct BandClassListCDMA2000 *bandClassList;
    struct _seqof20 {
        struct _seqof20 *next;
        NeighCellCDMA2000_r11 value;
    } *neighCellList_r11;
    T_Reselection   t_ReselectionCDMA2000;
    SpeedStateScaleFactors t_ReselectionCDMA2000_SF;  /* optional; set in
                                   * bit_mask
    * CellReselectionParametersCDMA2000_r11_t_ReselectionCDMA2000_SF_present if
    * present */
                                                      /* Need OP */
} CellReselectionParametersCDMA2000_r11;

typedef struct NeighCellCDMA2000 {
    BandclassCDMA2000 bandClass;
    struct NeighCellsPerBandclassListCDMA2000 *neighCellsPerFreqList;
} NeighCellCDMA2000;

typedef struct NeighCellListCDMA2000 {
    struct NeighCellListCDMA2000 *next;
    NeighCellCDMA2000 value;
} *NeighCellListCDMA2000;

typedef struct NeighCellsPerBandclassCDMA2000 {
    ARFCN_ValueCDMA2000 arfcn;
    struct PhysCellIdListCDMA2000 *physCellIdList;
} NeighCellsPerBandclassCDMA2000;

typedef struct NeighCellsPerBandclassListCDMA2000 {
    struct NeighCellsPerBandclassListCDMA2000 *next;
    NeighCellsPerBandclassCDMA2000 value;
} *NeighCellsPerBandclassListCDMA2000;

typedef struct NeighCellCDMA2000_v920 {
    struct NeighCellsPerBandclassListCDMA2000_v920 *neighCellsPerFreqList_v920;
} NeighCellCDMA2000_v920;

typedef struct NeighCellListCDMA2000_v920 {
    struct NeighCellListCDMA2000_v920 *next;
    NeighCellCDMA2000_v920 value;
} *NeighCellListCDMA2000_v920;

typedef struct NeighCellsPerBandclassCDMA2000_v920 {
    struct PhysCellIdListCDMA2000_v920 *physCellIdList_v920;
} NeighCellsPerBandclassCDMA2000_v920;

typedef struct NeighCellsPerBandclassListCDMA2000_v920 {
    struct NeighCellsPerBandclassListCDMA2000_v920 *next;
    NeighCellsPerBandclassCDMA2000_v920 value;
} *NeighCellsPerBandclassListCDMA2000_v920;

typedef struct PhysCellIdListCDMA2000 {
    struct PhysCellIdListCDMA2000 *next;
    PhysCellIdCDMA2000 value;
} *PhysCellIdListCDMA2000;

typedef struct PhysCellIdListCDMA2000_v920 {
    struct PhysCellIdListCDMA2000_v920 *next;
    PhysCellIdCDMA2000 value;
} *PhysCellIdListCDMA2000_v920;

typedef struct BandClassInfoCDMA2000 {
    unsigned char   bit_mask;
#       define      BandClassInfoCDMA2000_cellReselectionPriority_present 0x80
    BandclassCDMA2000 bandClass;
    CellReselectionPriority cellReselectionPriority;  /* optional; set in
                                   * bit_mask
                     * BandClassInfoCDMA2000_cellReselectionPriority_present if
                     * present */
                                                      /* Need OP */
    unsigned short  threshX_High;
    unsigned short  threshX_Low;
} BandClassInfoCDMA2000;

typedef struct BandClassListCDMA2000 {
    struct BandClassListCDMA2000 *next;
    BandClassInfoCDMA2000 value;
} *BandClassListCDMA2000;

typedef struct ParametersCDMA2000_r11 {
    unsigned char   bit_mask;
#       define      systemTimeInfo_r11_present 0x80
#       define      parametersHRPD_r11_present 0x40
#       define      parameters1XRTT_r11_present 0x20
    struct {
        unsigned short  choice;
#           define      systemTimeInfo_r11_explicitValue_chosen 1
#           define      systemTimeInfo_r11_defaultValue_chosen 2
        union {
            SystemTimeInfoCDMA2000 explicitValue;  /* to choose, set choice to
                                   * systemTimeInfo_r11_explicitValue_chosen */
            Nulltype        defaultValue;  /* to choose, set choice to
                                    * systemTimeInfo_r11_defaultValue_chosen */
        } u;
    } systemTimeInfo_r11;  /* optional; set in bit_mask
                            * systemTimeInfo_r11_present if present */
       /* Need OR */
    unsigned short  searchWindowSize_r11;
    struct {
        unsigned char   bit_mask;
#           define      cellReselectionParametersHRPD_r11_present 0x80
        PreRegistrationInfoHRPD preRegistrationInfoHRPD_r11;
        CellReselectionParametersCDMA2000_r11 cellReselectionParametersHRPD_r11;                                        /* optional; set in bit_mask
                                 * cellReselectionParametersHRPD_r11_present if
                                 * present */
                                                                                  /* Need OR */
    } parametersHRPD_r11;  /* optional; set in bit_mask
                            * parametersHRPD_r11_present if present */
       /* Need OR */
    struct {
        unsigned char   bit_mask;
#           define      csfb_RegistrationParam1XRTT_r11_present 0x80
#           define      csfb_RegistrationParam1XRTT_Ext_r11_present 0x40
#           define      longCodeState1XRTT_r11_present 0x20
#           define      cellReselectionParameters1XRTT_r11_present 0x10
#           define      ac_BarringConfig1XRTT_r11_present 0x08
#           define      csfb_SupportForDualRxUEs_r11_present 0x04
#           define      csfb_DualRxTxSupport_r11_present 0x02
        CSFB_RegistrationParam1XRTT csfb_RegistrationParam1XRTT_r11;  
                                  /* optional; set in bit_mask
                                   * csfb_RegistrationParam1XRTT_r11_present if
                                   * present */
                                                                      /* Need OP */
        CSFB_RegistrationParam1XRTT_v920 csfb_RegistrationParam1XRTT_Ext_r11;                                           /* optional; set in bit_mask
                               * csfb_RegistrationParam1XRTT_Ext_r11_present if
                               * present */
                                                                               /* Cond REG-1XRTT-PerPLMN */
        _bit1           longCodeState1XRTT_r11;  /* optional; set in bit_mask
                                            * longCodeState1XRTT_r11_present if
                                            * present */
                                                 /* Cond PerPLMN-LC */
        CellReselectionParametersCDMA2000_r11 cellReselectionParameters1XRTT_r11;                                       /* optional; set in bit_mask
                                * cellReselectionParameters1XRTT_r11_present if
                                * present */
                                                                                   /* Need OR */
        AC_BarringConfig1XRTT_r9 ac_BarringConfig1XRTT_r11;  /* optional; set in
                                   * bit_mask ac_BarringConfig1XRTT_r11_present
                                   * if present */
                                                             /* Cond REG-1XRTT-PerPLMN */
        ossBoolean      csfb_SupportForDualRxUEs_r11;  /* optional; set in
                                   * bit_mask
                                   * csfb_SupportForDualRxUEs_r11_present if
                                   * present */
                                                       /* Need OR */
        _enum3          csfb_DualRxTxSupport_r11;  /* optional; set in bit_mask
                                          * csfb_DualRxTxSupport_r11_present if
                                          * present */
                                                   /* Cond REG-1XRTT-PerPLMN */
    } parameters1XRTT_r11;  /* optional; set in bit_mask
                             * parameters1XRTT_r11_present if present */
       /* Need OR */
} ParametersCDMA2000_r11;

typedef struct SIB8_PerPLMN_r11 {
    unsigned short  plmn_Identity_r11;
    struct {
        unsigned short  choice;
#           define      parametersCDMA2000_r11_explicitValue_chosen 1
#           define      parametersCDMA2000_r11_defaultValue_chosen 2
        union {
            ParametersCDMA2000_r11 explicitValue;  /* to choose, set choice to
                               * parametersCDMA2000_r11_explicitValue_chosen */
            Nulltype        defaultValue;  /* to choose, set choice to
                                * parametersCDMA2000_r11_defaultValue_chosen */
        } u;
    } parametersCDMA2000_r11;
} SIB8_PerPLMN_r11;

typedef struct SIB8_PerPLMN_List_r11 {
    struct SIB8_PerPLMN_List_r11 *next;
    SIB8_PerPLMN_r11 value;
} *SIB8_PerPLMN_List_r11;

typedef unsigned short  MBMS_SAI_r11;

typedef struct MBMS_SAI_List_r11 {
    struct MBMS_SAI_List_r11 *next;
    MBMS_SAI_r11    value;
} *MBMS_SAI_List_r11;

typedef struct MBMS_SAI_InterFreq_r11 {
    ARFCN_ValueEUTRA_r9 dl_CarrierFreq_r11;
    struct MBMS_SAI_List_r11 *mbms_SAI_List_r11;
} MBMS_SAI_InterFreq_r11;

typedef struct MBMS_SAI_InterFreqList_r11 {
    struct MBMS_SAI_InterFreqList_r11 *next;
    MBMS_SAI_InterFreq_r11 value;
} *MBMS_SAI_InterFreqList_r11;

typedef struct MBMS_SAI_InterFreq_v1140 {
    unsigned char   bit_mask;
#       define      multiBandInfoList_r11_present 0x80
    struct MultiBandInfoList_r11 *multiBandInfoList_r11;  /* optional; set in
                                   * bit_mask multiBandInfoList_r11_present if
                                   * present */
                                                          /* Need OR */
} MBMS_SAI_InterFreq_v1140;

typedef struct MBMS_SAI_InterFreqList_v1140 {
    struct MBMS_SAI_InterFreqList_v1140 *next;
    MBMS_SAI_InterFreq_v1140 value;
} *MBMS_SAI_InterFreqList_v1140;

typedef struct WLAN_Identifiers_r12 {
    unsigned char   bit_mask;
#       define      ssid_r12_present 0x80
#       define      bssid_r12_present 0x40
#       define      hessid_r12_present 0x20
    struct {
        unsigned short  length;
        unsigned char   value[32];
    } ssid_r12;  /* optional; set in bit_mask ssid_r12_present if present */
                 /* Need OR */
    _octet4         bssid_r12;  /* optional; set in bit_mask bssid_r12_present
                                 * if present */
                                /* Need OR */
    _octet4         hessid_r12;  /* optional; set in bit_mask hessid_r12_present
                                  * if present */
                                 /* Need OR */
} WLAN_Identifiers_r12;

typedef struct WLAN_Id_List_r12 {
    struct WLAN_Id_List_r12 *next;
    WLAN_Identifiers_r12 value;
} *WLAN_Id_List_r12;

typedef struct SL_CarrierFreqInfo_r12 {
    unsigned char   bit_mask;
#       define      plmn_IdentityList_r12_present 0x80
    ARFCN_ValueEUTRA_r9 carrierFreq_r12;
    struct PLMN_IdentityList4_r12 *plmn_IdentityList_r12;  /* optional; set in
                                   * bit_mask plmn_IdentityList_r12_present if
                                   * present */
                                                           /* Need OP */
} SL_CarrierFreqInfo_r12;

typedef struct SL_CarrierFreqInfoList_r12 {
    struct SL_CarrierFreqInfoList_r12 *next;
    SL_CarrierFreqInfo_r12 value;
} *SL_CarrierFreqInfoList_r12;

typedef struct PLMN_IdentityInfo2_r12 {
    unsigned short  choice;
#       define      plmn_Index_r12_chosen 1
#       define      plmnIdentity_r12_chosen 2
    union {
        unsigned short  plmn_Index_r12;  /* to choose, set choice to
                                          * plmn_Index_r12_chosen */
        PLMN_Identity   plmnIdentity_r12;  /* to choose, set choice to
                                            * plmnIdentity_r12_chosen */
    } u;
} PLMN_IdentityInfo2_r12;

typedef struct PLMN_IdentityList4_r12 {
    struct PLMN_IdentityList4_r12 *next;
    PLMN_IdentityInfo2_r12 value;
} *PLMN_IdentityList4_r12;

typedef unsigned short  CQI_ReportPeriodicProcExtId_r11;

typedef struct CQI_ReportPeriodicProcExt_r11 {
    unsigned char   bit_mask;
#       define      ri_ConfigIndex_r11_present 0x80
#       define      csi_ConfigIndex_r11_present 0x40
    CQI_ReportPeriodicProcExtId_r11 cqi_ReportPeriodicProcExtId_r11;
    unsigned short  cqi_pmi_ConfigIndex_r11;
    struct {
        unsigned short  choice;
#           define      widebandCQI_r11_chosen 1
#           define      subbandCQI_r11_chosen 2
        union {
            struct _seq75 {
                unsigned char   bit_mask;
#                   define      csi_ReportMode_r11_present 0x80
                _enum23         csi_ReportMode_r11;  /* optional; set in
                                   * bit_mask csi_ReportMode_r11_present if
                                   * present */
                                                     /* Need OR */
            } widebandCQI_r11;  /* to choose, set choice to
                                 * widebandCQI_r11_chosen */
            struct _seq76 {
                unsigned short  k;
                _enum16         periodicityFactor_r11;
            } subbandCQI_r11;  /* to choose, set choice to
                                * subbandCQI_r11_chosen */
        } u;
    } cqi_FormatIndicatorPeriodic_r11;
    unsigned short  ri_ConfigIndex_r11;  /* optional; set in bit_mask
                                          * ri_ConfigIndex_r11_present if
                                          * present */
                                         /* Need OR */
    struct {
        unsigned short  choice;
#           define      csi_ConfigIndex_r11_release_chosen 1
#           define      csi_ConfigIndex_r11_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * csi_ConfigIndex_r11_release_chosen */
            struct _seq77 {
                unsigned char   bit_mask;
#                   define      ri_ConfigIndex2_r11_present 0x80
                unsigned short  cqi_pmi_ConfigIndex2_r11;
                unsigned short  ri_ConfigIndex2_r11;  /* optional; set in
                                   * bit_mask ri_ConfigIndex2_r11_present if
                                   * present */
                                                      /* Need OR */
            } setup;  /* to choose, set choice to
                       * csi_ConfigIndex_r11_setup_chosen */
        } u;
    } csi_ConfigIndex_r11;  /* optional; set in bit_mask
                             * csi_ConfigIndex_r11_present if present */
               /* Need ON */
} CQI_ReportPeriodicProcExt_r11;

typedef struct CQI_ReportPeriodicProcExtToAddModList_r11 {
    struct CQI_ReportPeriodicProcExtToAddModList_r11 *next;
    CQI_ReportPeriodicProcExt_r11 value;
} *CQI_ReportPeriodicProcExtToAddModList_r11;

typedef struct CQI_ReportPeriodicProcExtToReleaseList_r11 {
    struct CQI_ReportPeriodicProcExtToReleaseList_r11 *next;
    CQI_ReportPeriodicProcExtId_r11 value;
} *CQI_ReportPeriodicProcExtToReleaseList_r11;

typedef struct CQI_ReportAperiodicProc_r11 {
    CQI_ReportModeAperiodic cqi_ReportModeAperiodic_r11;
    ossBoolean      trigger01_r11;
    ossBoolean      trigger10_r11;
    ossBoolean      trigger11_r11;
} CQI_ReportAperiodicProc_r11;

typedef unsigned short  CSI_IM_ConfigId_r11;

typedef struct CSI_IM_Config_r11 {
    CSI_IM_ConfigId_r11 csi_IM_ConfigId_r11;
    unsigned short  resourceConfig_r11;
    unsigned short  subframeConfig_r11;
} CSI_IM_Config_r11;

typedef struct CSI_IM_ConfigToAddModList_r11 {
    struct CSI_IM_ConfigToAddModList_r11 *next;
    CSI_IM_Config_r11 value;
} *CSI_IM_ConfigToAddModList_r11;

typedef struct CSI_IM_ConfigToReleaseList_r11 {
    struct CSI_IM_ConfigToReleaseList_r11 *next;
    CSI_IM_ConfigId_r11 value;
} *CSI_IM_ConfigToReleaseList_r11;

typedef unsigned short  CSI_ProcessId_r11;

typedef unsigned short  CSI_RS_ConfigNZPId_r11;

typedef struct P_C_AndCBSR_r11 {
    short           p_C_r11;
    _bit2           codebookSubsetRestriction_r11;
} P_C_AndCBSR_r11;

typedef struct CQI_ReportBothProc_r11 {
    unsigned char   bit_mask;
#       define      ri_Ref_CSI_ProcessId_r11_present 0x80
#       define      pmi_RI_Report_r11_present 0x40
    CSI_ProcessId_r11 ri_Ref_CSI_ProcessId_r11;  /* optional; set in bit_mask
                                          * ri_Ref_CSI_ProcessId_r11_present if
                                          * present */
                                                 /* Need OR */
    _enum17         pmi_RI_Report_r11;  /* optional; set in bit_mask
                                         * pmi_RI_Report_r11_present if
                                         * present */
                                        /* Need OR */
} CQI_ReportBothProc_r11;

typedef unsigned short  CSI_IM_ConfigId_r12;

typedef struct CSI_Process_r11 {
    unsigned char   bit_mask;
#       define      cqi_ReportBothProc_r11_present 0x80
#       define      cqi_ReportPeriodicProcId_r11_present 0x40
#       define      cqi_ReportAperiodicProc_r11_present 0x20
#       define      alternativeCodebookEnabledFor4TXProc_r12_present 0x10
#       define      csi_IM_ConfigIdList_r12_present 0x08
#       define      cqi_ReportAperiodicProc2_r12_present 0x04
    CSI_ProcessId_r11 csi_ProcessId_r11;
    CSI_RS_ConfigNZPId_r11 csi_RS_ConfigNZPId_r11;
    CSI_IM_ConfigId_r11 csi_IM_ConfigId_r11;
    struct _seqof22 {
        struct _seqof22 *next;
        P_C_AndCBSR_r11 value;
    } *p_C_AndCBSRList_r11;
    CQI_ReportBothProc_r11 cqi_ReportBothProc_r11;  /* optional; set in bit_mask
                                            * cqi_ReportBothProc_r11_present if
                                            * present */
                                                    /* Need OR */
    unsigned short  cqi_ReportPeriodicProcId_r11;  /* optional; set in bit_mask
                                      * cqi_ReportPeriodicProcId_r11_present if
                                      * present */
                                                   /* Need OR */
    CQI_ReportAperiodicProc_r11 cqi_ReportAperiodicProc_r11;  /* optional; set
                                   * in bit_mask
                                   * cqi_ReportAperiodicProc_r11_present if
                                   * present */
                                                              /* Need OR */
    _enum3          alternativeCodebookEnabledFor4TXProc_r12;  /* extension #1;
                                   * optional; set in bit_mask
                          * alternativeCodebookEnabledFor4TXProc_r12_present if
                          * present */
       /* Need ON */
    struct {
        unsigned short  choice;
#           define      csi_IM_ConfigIdList_r12_release_chosen 1
#           define      csi_IM_ConfigIdList_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                    * csi_IM_ConfigIdList_r12_release_chosen */
            struct _seqof21 {
                struct _seqof21 *next;
                CSI_IM_ConfigId_r12 value;
            } *setup;  /* to choose, set choice to
                        * csi_IM_ConfigIdList_r12_setup_chosen */
        } u;
    } csi_IM_ConfigIdList_r12;  /* extension #1; optional; set in bit_mask
                                 * csi_IM_ConfigIdList_r12_present if present */
       /* Need ON */
    struct {
        unsigned short  choice;
#           define      cqi_ReportAperiodicProc2_r12_release_chosen 1
#           define      cqi_ReportAperiodicProc2_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                               * cqi_ReportAperiodicProc2_r12_release_chosen */
            CQI_ReportAperiodicProc_r11 setup;  /* to choose, set choice to
                                 * cqi_ReportAperiodicProc2_r12_setup_chosen */
        } u;
    } cqi_ReportAperiodicProc2_r12;  /* extension #1; optional; set in bit_mask
                                      * cqi_ReportAperiodicProc2_r12_present if
                                      * present */
                                     /* Need ON */
} CSI_Process_r11;

typedef struct CSI_ProcessToAddModList_r11 {
    struct CSI_ProcessToAddModList_r11 *next;
    CSI_Process_r11 value;
} *CSI_ProcessToAddModList_r11;

typedef struct CSI_ProcessToReleaseList_r11 {
    struct CSI_ProcessToReleaseList_r11 *next;
    CSI_ProcessId_r11 value;
} *CSI_ProcessToReleaseList_r11;

typedef enum _enum34 {
    crs_PortsCount_r11_n1 = 0,
    crs_PortsCount_r11_n2 = 1,
    crs_PortsCount_r11_n4 = 2,
    crs_PortsCount_r11_spare1 = 3
} _enum34;

typedef struct _seq78 {
    struct MBSFN_SubframeConfigList *subframeConfigList;
} _seq78;

typedef struct _choice51 {
    unsigned short  choice;
#       define      mbsfn_SubframeConfigList_r11_release_chosen 1
#       define      mbsfn_SubframeConfigList_r11_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                               * mbsfn_SubframeConfigList_r11_release_chosen */
        _seq78          setup;  /* to choose, set choice to
                                 * mbsfn_SubframeConfigList_r11_setup_chosen */
    } u;
} _choice51;

typedef struct CSI_RS_ConfigNZP_r11 {
    unsigned char   bit_mask;
#       define      qcl_CRS_Info_r11_present 0x80
    CSI_RS_ConfigNZPId_r11 csi_RS_ConfigNZPId_r11;
    _enum24         antennaPortsCount_r11;
    unsigned short  resourceConfig_r11;
    unsigned short  subframeConfig_r11;
    unsigned short  scramblingIdentity_r11;
    struct {
        unsigned char   bit_mask;
#           define      qcl_CRS_Info_r11_mbsfn_SubframeConfigList_r11_present 0x80
        unsigned short  qcl_ScramblingIdentity_r11;
        _enum34         crs_PortsCount_r11;
        _choice51       mbsfn_SubframeConfigList_r11;  /* optional; set in
                                   * bit_mask
                     * qcl_CRS_Info_r11_mbsfn_SubframeConfigList_r11_present if
                     * present */
                                                       /* Need ON */
    } qcl_CRS_Info_r11;  /* optional; set in bit_mask qcl_CRS_Info_r11_present
                          * if present */
       /* Need OR */
} CSI_RS_ConfigNZP_r11;

typedef unsigned short  CSI_RS_ConfigZPId_r11;

typedef struct CSI_RS_ConfigZP_r11 {
    CSI_RS_ConfigZPId_r11 csi_RS_ConfigZPId_r11;
    _bit1           resourceConfigList_r11;
    unsigned short  subframeConfig_r11;
} CSI_RS_ConfigZP_r11;

typedef unsigned short  EPDCCH_SetConfigId_r11;

typedef unsigned short  PDSCH_RE_MappingQCL_ConfigId_r11;

typedef struct EPDCCH_SetConfig_r11 {
    unsigned char   bit_mask;
#       define      re_MappingQCL_ConfigId_r11_present 0x80
#       define      csi_RS_ConfigZPId2_r12_present 0x40
    EPDCCH_SetConfigId_r11 setConfigId_r11;
    enum {
        localised = 0,
        distributed = 1
    } transmissionType_r11;
    struct {
        enum {
            numberPRB_Pairs_r11_n2 = 0,
            numberPRB_Pairs_r11_n4 = 1,
            numberPRB_Pairs_r11_n8 = 2
        } numberPRB_Pairs_r11;
        _bit1           resourceBlockAssignment_r11;
    } resourceBlockAssignment_r11;
    unsigned short  dmrs_ScramblingSequenceInt_r11;
    unsigned short  pucch_ResourceStartOffset_r11;
    PDSCH_RE_MappingQCL_ConfigId_r11 re_MappingQCL_ConfigId_r11;  /* optional;
                                   * set in bit_mask
                                   * re_MappingQCL_ConfigId_r11_present if
                                   * present */
                                                                  /* Need OR */
    struct {
        unsigned short  choice;
#           define      csi_RS_ConfigZPId2_r12_release_chosen 1
#           define      csi_RS_ConfigZPId2_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                     * csi_RS_ConfigZPId2_r12_release_chosen */
            CSI_RS_ConfigZPId_r11 setup;  /* to choose, set choice to
                                       * csi_RS_ConfigZPId2_r12_setup_chosen */
        } u;
    } csi_RS_ConfigZPId2_r12;  /* extension #1; optional; set in bit_mask
                                * csi_RS_ConfigZPId2_r12_present if present */
                               /* Need ON */
} EPDCCH_SetConfig_r11;

typedef struct EPDCCH_SetConfigToAddModList_r11 {
    struct EPDCCH_SetConfigToAddModList_r11 *next;
    EPDCCH_SetConfig_r11 value;
} *EPDCCH_SetConfigToAddModList_r11;

typedef struct EPDCCH_SetConfigToReleaseList_r11 {
    struct EPDCCH_SetConfigToReleaseList_r11 *next;
    EPDCCH_SetConfigId_r11 value;
} *EPDCCH_SetConfigToReleaseList_r11;

typedef struct LogicalChannelConfig {
    unsigned char   bit_mask;
#       define      ul_SpecificParameters_present 0x80
#       define      logicalChannelSR_Mask_r9_present 0x40
#       define      logicalChannelSR_Prohibit_r12_present 0x20
    struct {
        unsigned char   bit_mask;
#           define      logicalChannelGroup_present 0x80
        unsigned short  priority;
        enum {
            kBps0 = 0,
            kBps8 = 1,
            kBps16 = 2,
            kBps32 = 3,
            kBps64 = 4,
            kBps128 = 5,
            kBps256 = 6,
            prioritisedBitRate_infinity = 7,
            kBps512_v1020 = 8,
            kBps1024_v1020 = 9,
            kBps2048_v1020 = 10,
            prioritisedBitRate_spare5 = 11,
            prioritisedBitRate_spare4 = 12,
            prioritisedBitRate_spare3 = 13,
            prioritisedBitRate_spare2 = 14,
            prioritisedBitRate_spare1 = 15
        } prioritisedBitRate;
        enum {
            bucketSizeDuration_ms50 = 0,
            bucketSizeDuration_ms100 = 1,
            bucketSizeDuration_ms150 = 2,
            bucketSizeDuration_ms300 = 3,
            bucketSizeDuration_ms500 = 4,
            bucketSizeDuration_ms1000 = 5,
            bucketSizeDuration_spare2 = 6,
            bucketSizeDuration_spare1 = 7
        } bucketSizeDuration;
        unsigned short  logicalChannelGroup;  /* optional; set in bit_mask
                                               * logicalChannelGroup_present if
                                               * present */
                                              /* Need OR */
    } ul_SpecificParameters;  /* optional; set in bit_mask
                               * ul_SpecificParameters_present if present */
                                                                                                                                       /* Cond UL */
    _enum17         logicalChannelSR_Mask_r9;  /* extension #1; optional; set in
                                   * bit_mask logicalChannelSR_Mask_r9_present
                                   * if present */
                                               /* Cond SRmask */
    ossBoolean      logicalChannelSR_Prohibit_r12;  /* extension #2; optional;
                                   * set in bit_mask
                                   * logicalChannelSR_Prohibit_r12_present if
                                   * present */
                                                    /* Need ON */
} LogicalChannelConfig;

typedef struct STAG_ToReleaseList_r11 {
    struct STAG_ToReleaseList_r11 *next;
    STAG_Id_r11     value;
} *STAG_ToReleaseList_r11;

typedef struct STAG_ToAddMod_r11 {
    STAG_Id_r11     stag_Id_r11;
    TimeAlignmentTimer timeAlignmentTimerSTAG_r11;
} STAG_ToAddMod_r11;

typedef struct STAG_ToAddModList_r11 {
    struct STAG_ToAddModList_r11 *next;
    STAG_ToAddMod_r11 value;
} *STAG_ToAddModList_r11;

typedef struct _seq79 {
    ossBoolean      profile0x0001;
    ossBoolean      profile0x0002;
    ossBoolean      profile0x0003;
    ossBoolean      profile0x0004;
    ossBoolean      profile0x0006;
    ossBoolean      profile0x0101;
    ossBoolean      profile0x0102;
    ossBoolean      profile0x0103;
    ossBoolean      profile0x0104;
} _seq79;

typedef struct PDCP_Config {
    unsigned char   bit_mask;
#       define      discardTimer_present 0x80
#       define      rlc_AM_present 0x40
#       define      rlc_UM_present 0x20
#       define      rn_IntegrityProtection_r10_present 0x10
#       define      pdcp_SN_Size_v1130_present 0x08
#       define      ul_DataSplitDRB_ViaSCG_r12_present 0x04
#       define      t_Reordering_r12_present 0x02
    enum {
        discardTimer_ms50 = 0,
        discardTimer_ms100 = 1,
        discardTimer_ms150 = 2,
        discardTimer_ms300 = 3,
        discardTimer_ms500 = 4,
        discardTimer_ms750 = 5,
        discardTimer_ms1500 = 6,
        discardTimer_infinity = 7
    } discardTimer;  /* optional; set in bit_mask discardTimer_present if
                      * present */
                       /* Cond Setup */
    struct {
        ossBoolean      statusReportRequired;
    } rlc_AM;  /* optional; set in bit_mask rlc_AM_present if present */
                       /* Cond Rlc-AM */
    struct {
        enum {
            len7bits = 0,
            len12bits = 1
        } pdcp_SN_Size;
    } rlc_UM;  /* optional; set in bit_mask rlc_UM_present if present */
                       /* Cond Rlc-UM */
    struct {
        unsigned short  choice;
#           define      notUsed_chosen 1
#           define      rohc_chosen 2
        union {
            Nulltype        notUsed;  /* to choose, set choice to
                                       * notUsed_chosen */
            struct _seq80 {
                unsigned char   bit_mask;
#                   define      maxCID_present 0x80
                unsigned short  maxCID;  /* maxCID_present not set in bit_mask
                                          * implies value is 15 */
                _seq79          profiles;
            } rohc;  /* to choose, set choice to rohc_chosen */
        } u;
    } headerCompression;
    _enum30         rn_IntegrityProtection_r10;  /* extension #1; optional; set
                                   * in bit_mask
                                   * rn_IntegrityProtection_r10_present if
                                   * present */
                                                 /* Cond RN */
    enum {
        len15bits = 0
    } pdcp_SN_Size_v1130;  /* extension #2; optional; set in bit_mask
                            * pdcp_SN_Size_v1130_present if present */
                           /* Cond Rlc-AM2 */
    ossBoolean      ul_DataSplitDRB_ViaSCG_r12;  /* extension #3; optional; set
                                   * in bit_mask
                                   * ul_DataSplitDRB_ViaSCG_r12_present if
                                   * present */
                                                 /* Need ON */
    enum {
        t_Reordering_r12_ms0 = 0,
        t_Reordering_r12_ms20 = 1,
        t_Reordering_r12_ms40 = 2,
        t_Reordering_r12_ms60 = 3,
        t_Reordering_r12_ms80 = 4,
        t_Reordering_r12_ms100 = 5,
        t_Reordering_r12_ms120 = 6,
        t_Reordering_r12_ms140 = 7,
        t_Reordering_r12_ms160 = 8,
        t_Reordering_r12_ms180 = 9,
        t_Reordering_r12_ms200 = 10,
        t_Reordering_r12_ms220 = 11,
        t_Reordering_r12_ms240 = 12,
        ms260 = 13,
        ms280 = 14,
        t_Reordering_r12_ms300 = 15,
        t_Reordering_r12_ms500 = 16,
        t_Reordering_r12_ms750 = 17,
        spare14 = 18,
        spare13 = 19,
        spare12 = 20,
        spare11 = 21,
        t_Reordering_r12_spare10 = 22,
        t_Reordering_r12_spare9 = 23,
        t_Reordering_r12_spare8 = 24,
        t_Reordering_r12_spare7 = 25,
        t_Reordering_r12_spare6 = 26,
        t_Reordering_r12_spare5 = 27,
        t_Reordering_r12_spare4 = 28,
        t_Reordering_r12_spare3 = 29,
        t_Reordering_r12_spare2 = 30,
        t_Reordering_r12_spare1 = 31
    } t_Reordering_r12;  /* extension #3; optional; set in bit_mask
                          * t_Reordering_r12_present if present */
                         /* Cond SetupS */
} PDCP_Config;

typedef struct PDSCH_RE_MappingQCL_Config_r11 {
    unsigned char   bit_mask;
#       define      optionalSetOfFields_r11_present 0x80
#       define      qcl_CSI_RS_ConfigNZPId_r11_present 0x40
    PDSCH_RE_MappingQCL_ConfigId_r11 pdsch_RE_MappingQCL_ConfigId_r11;
    struct {
        unsigned char   bit_mask;
#           define      optionalSetOfFields_r11_mbsfn_SubframeConfigList_r11_present 0x80
        _enum34         crs_PortsCount_r11;
        unsigned short  crs_FreqShift_r11;
        _choice51       mbsfn_SubframeConfigList_r11;  /* optional; set in
                                   * bit_mask
              * optionalSetOfFields_r11_mbsfn_SubframeConfigList_r11_present if
              * present */
       /* Need ON */
        enum {
            pdsch_Start_r11_reserved = 0,
            pdsch_Start_r11_n1 = 1,
            pdsch_Start_r11_n2 = 2,
            pdsch_Start_r11_n3 = 3,
            pdsch_Start_r11_n4 = 4,
            assigned = 5
        } pdsch_Start_r11;
    } optionalSetOfFields_r11;  /* optional; set in bit_mask
                                 * optionalSetOfFields_r11_present if present */
       /* Need OP */
    CSI_RS_ConfigZPId_r11 csi_RS_ConfigZPId_r11;
    CSI_RS_ConfigNZPId_r11 qcl_CSI_RS_ConfigNZPId_r11;  /* optional; set in
                                   * bit_mask qcl_CSI_RS_ConfigNZPId_r11_present
                                   * if present */
                                                        /* Need OR */
} PDSCH_RE_MappingQCL_Config_r11;

typedef struct RE_MappingQCLConfigToAddModList_r11 {
    struct RE_MappingQCLConfigToAddModList_r11 *next;
    PDSCH_RE_MappingQCL_Config_r11 value;
} *RE_MappingQCLConfigToAddModList_r11;

typedef struct RE_MappingQCLConfigToReleaseList_r11 {
    struct RE_MappingQCLConfigToReleaseList_r11 *next;
    PDSCH_RE_MappingQCL_ConfigId_r11 value;
} *RE_MappingQCLConfigToReleaseList_r11;

typedef struct CSI_RS_ConfigNZPToAddModList_r11 {
    struct CSI_RS_ConfigNZPToAddModList_r11 *next;
    CSI_RS_ConfigNZP_r11 value;
} *CSI_RS_ConfigNZPToAddModList_r11;

typedef struct CSI_RS_ConfigNZPToReleaseList_r11 {
    struct CSI_RS_ConfigNZPToReleaseList_r11 *next;
    CSI_RS_ConfigNZPId_r11 value;
} *CSI_RS_ConfigNZPToReleaseList_r11;

typedef struct CSI_RS_ConfigZPToAddModList_r11 {
    struct CSI_RS_ConfigZPToAddModList_r11 *next;
    CSI_RS_ConfigZP_r11 value;
} *CSI_RS_ConfigZPToAddModList_r11;

typedef struct CSI_RS_ConfigZPToReleaseList_r11 {
    struct CSI_RS_ConfigZPToReleaseList_r11 *next;
    CSI_RS_ConfigZPId_r11 value;
} *CSI_RS_ConfigZPToReleaseList_r11;

typedef struct N1PUCCH_AN_CS_r10 {
    struct N1PUCCH_AN_CS_r10 *next;
    unsigned short  value;
} *N1PUCCH_AN_CS_r10;

typedef enum T_PollRetransmit {
    T_PollRetransmit_ms5 = 0,
    T_PollRetransmit_ms10 = 1,
    T_PollRetransmit_ms15 = 2,
    T_PollRetransmit_ms20 = 3,
    T_PollRetransmit_ms25 = 4,
    T_PollRetransmit_ms30 = 5,
    T_PollRetransmit_ms35 = 6,
    T_PollRetransmit_ms40 = 7,
    T_PollRetransmit_ms45 = 8,
    T_PollRetransmit_ms50 = 9,
    T_PollRetransmit_ms55 = 10,
    T_PollRetransmit_ms60 = 11,
    T_PollRetransmit_ms65 = 12,
    T_PollRetransmit_ms70 = 13,
    T_PollRetransmit_ms75 = 14,
    T_PollRetransmit_ms80 = 15,
    T_PollRetransmit_ms85 = 16,
    T_PollRetransmit_ms90 = 17,
    T_PollRetransmit_ms95 = 18,
    T_PollRetransmit_ms100 = 19,
    T_PollRetransmit_ms105 = 20,
    T_PollRetransmit_ms110 = 21,
    T_PollRetransmit_ms115 = 22,
    T_PollRetransmit_ms120 = 23,
    T_PollRetransmit_ms125 = 24,
    T_PollRetransmit_ms130 = 25,
    T_PollRetransmit_ms135 = 26,
    T_PollRetransmit_ms140 = 27,
    T_PollRetransmit_ms145 = 28,
    T_PollRetransmit_ms150 = 29,
    T_PollRetransmit_ms155 = 30,
    T_PollRetransmit_ms160 = 31,
    T_PollRetransmit_ms165 = 32,
    T_PollRetransmit_ms170 = 33,
    T_PollRetransmit_ms175 = 34,
    T_PollRetransmit_ms180 = 35,
    T_PollRetransmit_ms185 = 36,
    T_PollRetransmit_ms190 = 37,
    T_PollRetransmit_ms195 = 38,
    T_PollRetransmit_ms200 = 39,
    T_PollRetransmit_ms205 = 40,
    T_PollRetransmit_ms210 = 41,
    T_PollRetransmit_ms215 = 42,
    T_PollRetransmit_ms220 = 43,
    T_PollRetransmit_ms225 = 44,
    T_PollRetransmit_ms230 = 45,
    T_PollRetransmit_ms235 = 46,
    T_PollRetransmit_ms240 = 47,
    T_PollRetransmit_ms245 = 48,
    T_PollRetransmit_ms250 = 49,
    T_PollRetransmit_ms300 = 50,
    T_PollRetransmit_ms350 = 51,
    T_PollRetransmit_ms400 = 52,
    T_PollRetransmit_ms450 = 53,
    T_PollRetransmit_ms500 = 54,
    T_PollRetransmit_spare9 = 55,
    T_PollRetransmit_spare8 = 56,
    T_PollRetransmit_spare7 = 57,
    T_PollRetransmit_spare6 = 58,
    T_PollRetransmit_spare5 = 59,
    T_PollRetransmit_spare4 = 60,
    T_PollRetransmit_spare3 = 61,
    T_PollRetransmit_spare2 = 62,
    T_PollRetransmit_spare1 = 63
} T_PollRetransmit;

typedef enum PollPDU {
    p4 = 0,
    p8 = 1,
    p16 = 2,
    p32 = 3,
    p64 = 4,
    p128 = 5,
    p256 = 6,
    pInfinity = 7
} PollPDU;

typedef enum PollByte {
    kB25 = 0,
    kB50 = 1,
    kB75 = 2,
    kB100 = 3,
    kB125 = 4,
    kB250 = 5,
    kB375 = 6,
    kB500 = 7,
    kB750 = 8,
    kB1000 = 9,
    kB1250 = 10,
    kB1500 = 11,
    kB2000 = 12,
    kB3000 = 13,
    kBinfinity = 14,
    PollByte_spare1 = 15
} PollByte;

typedef struct UL_AM_RLC {
    T_PollRetransmit t_PollRetransmit;
    PollPDU         pollPDU;
    PollByte        pollByte;
    enum {
        t1 = 0,
        t2 = 1,
        t3 = 2,
        t4 = 3,
        t6 = 4,
        t8 = 5,
        t16 = 6,
        t32 = 7
    } maxRetxThreshold;
} UL_AM_RLC;

typedef enum T_Reordering {
    T_Reordering_ms0 = 0,
    T_Reordering_ms5 = 1,
    T_Reordering_ms10 = 2,
    T_Reordering_ms15 = 3,
    T_Reordering_ms20 = 4,
    T_Reordering_ms25 = 5,
    T_Reordering_ms30 = 6,
    T_Reordering_ms35 = 7,
    T_Reordering_ms40 = 8,
    T_Reordering_ms45 = 9,
    T_Reordering_ms50 = 10,
    T_Reordering_ms55 = 11,
    T_Reordering_ms60 = 12,
    T_Reordering_ms65 = 13,
    T_Reordering_ms70 = 14,
    T_Reordering_ms75 = 15,
    T_Reordering_ms80 = 16,
    T_Reordering_ms85 = 17,
    T_Reordering_ms90 = 18,
    T_Reordering_ms95 = 19,
    T_Reordering_ms100 = 20,
    T_Reordering_ms110 = 21,
    T_Reordering_ms120 = 22,
    T_Reordering_ms130 = 23,
    T_Reordering_ms140 = 24,
    T_Reordering_ms150 = 25,
    T_Reordering_ms160 = 26,
    T_Reordering_ms170 = 27,
    T_Reordering_ms180 = 28,
    T_Reordering_ms190 = 29,
    T_Reordering_ms200 = 30,
    T_Reordering_spare1 = 31
} T_Reordering;

typedef enum T_StatusProhibit {
    T_StatusProhibit_ms0 = 0,
    T_StatusProhibit_ms5 = 1,
    T_StatusProhibit_ms10 = 2,
    T_StatusProhibit_ms15 = 3,
    T_StatusProhibit_ms20 = 4,
    T_StatusProhibit_ms25 = 5,
    T_StatusProhibit_ms30 = 6,
    T_StatusProhibit_ms35 = 7,
    T_StatusProhibit_ms40 = 8,
    T_StatusProhibit_ms45 = 9,
    T_StatusProhibit_ms50 = 10,
    T_StatusProhibit_ms55 = 11,
    T_StatusProhibit_ms60 = 12,
    T_StatusProhibit_ms65 = 13,
    T_StatusProhibit_ms70 = 14,
    T_StatusProhibit_ms75 = 15,
    T_StatusProhibit_ms80 = 16,
    T_StatusProhibit_ms85 = 17,
    T_StatusProhibit_ms90 = 18,
    T_StatusProhibit_ms95 = 19,
    T_StatusProhibit_ms100 = 20,
    T_StatusProhibit_ms105 = 21,
    T_StatusProhibit_ms110 = 22,
    T_StatusProhibit_ms115 = 23,
    T_StatusProhibit_ms120 = 24,
    T_StatusProhibit_ms125 = 25,
    T_StatusProhibit_ms130 = 26,
    T_StatusProhibit_ms135 = 27,
    T_StatusProhibit_ms140 = 28,
    T_StatusProhibit_ms145 = 29,
    T_StatusProhibit_ms150 = 30,
    T_StatusProhibit_ms155 = 31,
    T_StatusProhibit_ms160 = 32,
    T_StatusProhibit_ms165 = 33,
    T_StatusProhibit_ms170 = 34,
    T_StatusProhibit_ms175 = 35,
    T_StatusProhibit_ms180 = 36,
    T_StatusProhibit_ms185 = 37,
    T_StatusProhibit_ms190 = 38,
    T_StatusProhibit_ms195 = 39,
    T_StatusProhibit_ms200 = 40,
    T_StatusProhibit_ms205 = 41,
    T_StatusProhibit_ms210 = 42,
    T_StatusProhibit_ms215 = 43,
    T_StatusProhibit_ms220 = 44,
    T_StatusProhibit_ms225 = 45,
    T_StatusProhibit_ms230 = 46,
    T_StatusProhibit_ms235 = 47,
    T_StatusProhibit_ms240 = 48,
    T_StatusProhibit_ms245 = 49,
    T_StatusProhibit_ms250 = 50,
    T_StatusProhibit_ms300 = 51,
    T_StatusProhibit_ms350 = 52,
    T_StatusProhibit_ms400 = 53,
    T_StatusProhibit_ms450 = 54,
    T_StatusProhibit_ms500 = 55,
    T_StatusProhibit_spare8 = 56,
    T_StatusProhibit_spare7 = 57,
    T_StatusProhibit_spare6 = 58,
    T_StatusProhibit_spare5 = 59,
    T_StatusProhibit_spare4 = 60,
    T_StatusProhibit_spare3 = 61,
    T_StatusProhibit_spare2 = 62,
    T_StatusProhibit_spare1 = 63
} T_StatusProhibit;

typedef struct DL_AM_RLC {
    T_Reordering    t_Reordering;
    T_StatusProhibit t_StatusProhibit;
} DL_AM_RLC;

typedef enum SN_FieldLength {
    size5 = 0,
    size10 = 1
} SN_FieldLength;

typedef struct UL_UM_RLC {
    SN_FieldLength  sn_FieldLength;
} UL_UM_RLC;

typedef struct DL_UM_RLC {
    SN_FieldLength  sn_FieldLength;
    T_Reordering    t_Reordering;
} DL_UM_RLC;

typedef struct RLC_Config {
    unsigned short  choice;
#       define      am_chosen 1
#       define      um_Bi_Directional_chosen 2
#       define      um_Uni_Directional_UL_chosen 3
#       define      um_Uni_Directional_DL_chosen 4
    union {
        struct _seq81 {
            UL_AM_RLC       ul_AM_RLC;
            DL_AM_RLC       dl_AM_RLC;
        } am;  /* to choose, set choice to am_chosen */
        struct _seq82 {
            UL_UM_RLC       ul_UM_RLC;
            DL_UM_RLC       dl_UM_RLC;
        } um_Bi_Directional;  /* to choose, set choice to
                               * um_Bi_Directional_chosen */
        struct _seq83 {
            UL_UM_RLC       ul_UM_RLC;
        } um_Uni_Directional_UL;  /* to choose, set choice to
                                   * um_Uni_Directional_UL_chosen */
        struct _seq84 {
            DL_UM_RLC       dl_UM_RLC;
        } um_Uni_Directional_DL;  /* to choose, set choice to
                                   * um_Uni_Directional_DL_chosen */
    } u;
} RLC_Config;

typedef struct SRB_ToAddMod {
    unsigned char   bit_mask;
#       define      SRB_ToAddMod_rlc_Config_present 0x80
#       define      SRB_ToAddMod_logicalChannelConfig_present 0x40
    unsigned short  srb_Identity;
    struct {
        unsigned short  choice;
#           define      rlc_Config_explicitValue_chosen 1
#           define      rlc_Config_defaultValue_chosen 2
        union {
            RLC_Config      explicitValue;  /* to choose, set choice to
                                           * rlc_Config_explicitValue_chosen */
            Nulltype        defaultValue;  /* to choose, set choice to
                                            * rlc_Config_defaultValue_chosen */
        } u;
    } rlc_Config;  /* optional; set in bit_mask SRB_ToAddMod_rlc_Config_present
                    * if present */
                                                                                                                               /* Cond Setup */
    struct {
        unsigned short  choice;
#           define      logicalChannelConfig_explicitValue_chosen 1
#           define      logicalChannelConfig_defaultValue_chosen 2
        union {
            LogicalChannelConfig explicitValue;  /* to choose, set choice to
                                 * logicalChannelConfig_explicitValue_chosen */
            Nulltype        defaultValue;  /* to choose, set choice to
                                  * logicalChannelConfig_defaultValue_chosen */
        } u;
    } logicalChannelConfig;  /* optional; set in bit_mask
                              * SRB_ToAddMod_logicalChannelConfig_present if
                              * present */
                                                                                                                               /* Cond Setup */
} SRB_ToAddMod;

typedef struct SRB_ToAddModList {
    struct SRB_ToAddModList *next;
    SRB_ToAddMod    value;
} *SRB_ToAddModList;

typedef struct RLC_Config_v1250 {
    ossBoolean      ul_extended_RLC_LI_Field_r12;
    ossBoolean      dl_extended_RLC_LI_Field_r12;
} RLC_Config_v1250;

typedef struct DRB_ToAddMod {
    unsigned char   bit_mask;
#       define      eps_BearerIdentity_present 0x80
#       define      pdcp_Config_present 0x40
#       define      DRB_ToAddMod_rlc_Config_present 0x20
#       define      logicalChannelIdentity_present 0x10
#       define      DRB_ToAddMod_logicalChannelConfig_present 0x08
#       define      drb_TypeChange_r12_present 0x04
#       define      DRB_ToAddMod_rlc_Config_v1250_present 0x02
    unsigned short  eps_BearerIdentity;  /* optional; set in bit_mask
                                          * eps_BearerIdentity_present if
                                          * present */
                                         /* Cond DRB-Setup */
    DRB_Identity    drb_Identity;
    PDCP_Config     pdcp_Config;  /* optional; set in bit_mask
                                   * pdcp_Config_present if present */
                                  /* Cond PDCP */
    RLC_Config      rlc_Config;  /* optional; set in bit_mask
                                  * DRB_ToAddMod_rlc_Config_present if
                                  * present */
                                 /* Cond SetupM */
    unsigned short  logicalChannelIdentity;  /* optional; set in bit_mask
                                              * logicalChannelIdentity_present
                                              * if present */
                                             /* Cond DRB-SetupM */
    LogicalChannelConfig logicalChannelConfig;  /* optional; set in bit_mask
                                 * DRB_ToAddMod_logicalChannelConfig_present if
                                 * present */
                                                /* Cond SetupM */
    enum {
        toMCG = 0
    } drb_TypeChange_r12;  /* extension #1; optional; set in bit_mask
                            * drb_TypeChange_r12_present if present */
               /* Need OP */
    RLC_Config_v1250 rlc_Config_v1250;  /* extension #1; optional; set in
                                   * bit_mask
                                   * DRB_ToAddMod_rlc_Config_v1250_present if
                                   * present */
                                        /* Need ON */
} DRB_ToAddMod;

typedef struct DRB_ToAddModList {
    struct DRB_ToAddModList *next;
    DRB_ToAddMod    value;
} *DRB_ToAddModList;

typedef struct DRB_ToAddModSCG_r12 {
    unsigned char   bit_mask;
#       define      DRB_ToAddModSCG_r12_drb_Type_r12_present 0x80
#       define      rlc_ConfigSCG_r12_present 0x40
#       define      DRB_ToAddModSCG_r12_rlc_Config_v1250_present 0x20
#       define      logicalChannelIdentitySCG_r12_present 0x10
#       define      logicalChannelConfigSCG_r12_present 0x08
    DRB_Identity    drb_Identity_r12;
    struct {
        unsigned short  choice;
#           define      split_r12_chosen 1
#           define      scg_r12_chosen 2
        union {
            Nulltype        split_r12;  /* to choose, set choice to
                                         * split_r12_chosen */
            struct _seq85 {
                unsigned char   bit_mask;
#                   define      scg_r12_eps_BearerIdentity_r12_present 0x80
#                   define      pdcp_Config_r12_present 0x40
                unsigned short  eps_BearerIdentity_r12;  /* optional; set in
                                   * bit_mask
                                   * scg_r12_eps_BearerIdentity_r12_present if
                                   * present */
                                                         /* Cond DRB-Setup */
                PDCP_Config     pdcp_Config_r12;  /* optional; set in bit_mask
                                                   * pdcp_Config_r12_present if
                                                   * present */
                                                  /* Cond PDCP-S */
            } scg_r12;  /* to choose, set choice to scg_r12_chosen */
        } u;
    } drb_Type_r12;  /* optional; set in bit_mask
                      * DRB_ToAddModSCG_r12_drb_Type_r12_present if present */
       /* Cond SetupS2 */
    RLC_Config      rlc_ConfigSCG_r12;  /* optional; set in bit_mask
                                         * rlc_ConfigSCG_r12_present if
                                         * present */
                                        /* Cond SetupS */
    RLC_Config_v1250 rlc_Config_v1250;  /* optional; set in bit_mask
                              * DRB_ToAddModSCG_r12_rlc_Config_v1250_present if
                              * present */
                                        /* Need ON */
    unsigned short  logicalChannelIdentitySCG_r12;  /* optional; set in bit_mask
                                     * logicalChannelIdentitySCG_r12_present if
                                     * present */
                                                    /* Cond DRB-SetupS */
    LogicalChannelConfig logicalChannelConfigSCG_r12;  /* optional; set in
                                   * bit_mask
                                   * logicalChannelConfigSCG_r12_present if
                                   * present */
                                                       /* Cond SetupS */
} DRB_ToAddModSCG_r12;

typedef struct DRB_ToAddModListSCG_r12 {
    struct DRB_ToAddModListSCG_r12 *next;
    DRB_ToAddModSCG_r12 value;
} *DRB_ToAddModListSCG_r12;

typedef struct DRB_ToReleaseList {
    struct DRB_ToReleaseList *next;
    DRB_Identity    value;
} *DRB_ToReleaseList;

typedef struct CRS_AssistanceInfo_r11 {
    PhysCellId      physCellId_r11;
    _enum28         antennaPortsCount_r11;
    struct MBSFN_SubframeConfigList *mbsfn_SubframeConfigList_r11;
} CRS_AssistanceInfo_r11;

typedef struct CRS_AssistanceInfoList_r11 {
    struct CRS_AssistanceInfoList_r11 *next;
    CRS_AssistanceInfo_r11 value;
} *CRS_AssistanceInfoList_r11;

typedef struct NeighCellsToReleaseList_r12 {
    struct NeighCellsToReleaseList_r12 *next;
    PhysCellId      value;
} *NeighCellsToReleaseList_r12;

typedef struct NeighCellsInfo_r12 {
    unsigned char   bit_mask;
#       define      mbsfn_SubframeConfig_r12_present 0x80
    PhysCellId      physCellId_r12;
    unsigned short  p_b_r12;
    enum {
        crs_PortsCount_r12_n1 = 0,
        crs_PortsCount_r12_n2 = 1,
        crs_PortsCount_r12_n4 = 2,
        crs_PortsCount_r12_spare = 3
    } crs_PortsCount_r12;
    struct MBSFN_SubframeConfigList *mbsfn_SubframeConfig_r12;  /* optional; set
                                   * in bit_mask
                                   * mbsfn_SubframeConfig_r12_present if
                                   * present */
                                                                /* Need ON */
    struct _seqof23 {
        struct _seqof23 *next;
        P_a             value;
    } *p_aList_r12;
    _bit1           transmissionModeList_r12;
    unsigned short  resAllocGranularity_r12;
} NeighCellsInfo_r12;

typedef struct NeighCellsToAddModList_r12 {
    struct NeighCellsToAddModList_r12 *next;
    NeighCellsInfo_r12 value;
} *NeighCellsToAddModList_r12;

typedef struct N1PUCCH_AN_PersistentList {
    struct N1PUCCH_AN_PersistentList *next;
    unsigned short  value;
} *N1PUCCH_AN_PersistentList;

typedef struct TDD_ConfigSL_r12 {
    enum {
        none = 0,
        subframeAssignmentSL_r12_sa0 = 1,
        subframeAssignmentSL_r12_sa1 = 2,
        subframeAssignmentSL_r12_sa2 = 3,
        subframeAssignmentSL_r12_sa3 = 4,
        subframeAssignmentSL_r12_sa4 = 5,
        subframeAssignmentSL_r12_sa5 = 6,
        subframeAssignmentSL_r12_sa6 = 7
    } subframeAssignmentSL_r12;
} TDD_ConfigSL_r12;

typedef struct ExplicitListOfARFCNs {
    struct ExplicitListOfARFCNs *next;
    ARFCN_ValueGERAN value;
} *ExplicitListOfARFCNs;

typedef struct CarrierFreqListMBMS_r11 {
    struct CarrierFreqListMBMS_r11 *next;
    ARFCN_ValueEUTRA_r9 value;
} *CarrierFreqListMBMS_r11;

typedef unsigned short  CellIndex;

typedef struct CellIndexList {
    struct CellIndexList *next;
    CellIndex       value;
} *CellIndexList;

typedef struct CellGlobalIdGERAN {
    PLMN_Identity   plmn_Identity;
    _bit1           locationAreaCode;
    _bit1           cellIdentity;
} CellGlobalIdGERAN;

typedef struct CellGlobalIdCDMA2000 {
    unsigned short  choice;
#       define      cellGlobalId1XRTT_chosen 1
#       define      cellGlobalIdHRPD_chosen 2
    union {
        _bit1           cellGlobalId1XRTT;  /* to choose, set choice to
                                             * cellGlobalId1XRTT_chosen */
        _bit1           cellGlobalIdHRPD;  /* to choose, set choice to
                                            * cellGlobalIdHRPD_chosen */
    } u;
} CellGlobalIdCDMA2000;

typedef struct MultiBandInfoList {
    struct MultiBandInfoList *next;
    FreqBandIndicator value;
} *MultiBandInfoList;

typedef struct MultiBandInfo_v9e0 {
    unsigned char   bit_mask;
#       define      MultiBandInfo_v9e0_freqBandIndicator_v9e0_present 0x80
    FreqBandIndicator_v9e0 freqBandIndicator_v9e0;  /* optional; set in bit_mask
                         * MultiBandInfo_v9e0_freqBandIndicator_v9e0_present if
                         * present */
                                                    /* Need OP */
} MultiBandInfo_v9e0;

typedef struct MultiBandInfoList_v9e0 {
    struct MultiBandInfoList_v9e0 *next;
    MultiBandInfo_v9e0 value;
} *MultiBandInfoList_v9e0;

typedef struct MultiBandInfoList_r11 {
    struct MultiBandInfoList_r11 *next;
    FreqBandIndicator_r11 value;
} *MultiBandInfoList_r11;

typedef struct PhysCellIdRangeUTRA_FDD_r9 {
    unsigned char   bit_mask;
#       define      range_r9_present 0x80
    PhysCellIdUTRA_FDD start_r9;
    unsigned short  range_r9;  /* optional; set in bit_mask range_r9_present if
                                * present */
                               /* Need OP */
} PhysCellIdRangeUTRA_FDD_r9;

typedef struct PhysCellIdRangeUTRA_FDDList_r9 {
    struct PhysCellIdRangeUTRA_FDDList_r9 *next;
    PhysCellIdRangeUTRA_FDD_r9 value;
} *PhysCellIdRangeUTRA_FDDList_r9;

typedef unsigned short  MCC_MNC_Digit;

typedef struct MCC {
    struct MCC      *next;
    MCC_MNC_Digit   value;
} *MCC;

typedef struct MNC {
    struct MNC      *next;
    MCC_MNC_Digit   value;
} *MNC;

typedef struct PLMN_IdentityList3_r11 {
    struct PLMN_IdentityList3_r11 *next;
    PLMN_Identity   value;
} *PLMN_IdentityList3_r11;

typedef struct SecondaryPreRegistrationZoneIdListHRPD {
    struct SecondaryPreRegistrationZoneIdListHRPD *next;
    PreRegistrationZoneIdHRPD value;
} *SecondaryPreRegistrationZoneIdListHRPD;

typedef short           Q_OffsetRangeInterRAT;

typedef struct SystemInfoListGERAN {
    struct SystemInfoListGERAN *next;
    struct {
        unsigned short  length;
        unsigned char   value[23];
    } value;
} *SystemInfoListGERAN;

typedef unsigned short  CSI_RSRP_Range_r12;

typedef unsigned short  Hysteresis;

typedef struct MeasIdToRemoveList {
    struct MeasIdToRemoveList *next;
    MeasId          value;
} *MeasIdToRemoveList;

typedef struct MeasIdToRemoveListExt_r12 {
    struct MeasIdToRemoveListExt_r12 *next;
    MeasId_v1250    value;
} *MeasIdToRemoveListExt_r12;

typedef struct MeasObjectToRemoveList {
    struct MeasObjectToRemoveList *next;
    MeasObjectId    value;
} *MeasObjectToRemoveList;

typedef unsigned short  ReportConfigId;

typedef struct ReportConfigToRemoveList {
    struct ReportConfigToRemoveList *next;
    ReportConfigId  value;
} *ReportConfigToRemoveList;

typedef struct MeasDS_Config_r12 {
    unsigned short  choice;
#       define      MeasDS_Config_r12_release_chosen 1
#       define      MeasDS_Config_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * MeasDS_Config_r12_release_chosen */
        struct _seq86 {
            unsigned char   bit_mask;
#               define      measCSI_RS_ToRemoveList_r12_present 0x80
#               define      measCSI_RS_ToAddModList_r12_present 0x40
            struct {
                unsigned short  choice;
#                   define      ms40_r12_chosen 1
#                   define      ms80_r12_chosen 2
#                   define      ms160_r12_chosen 3
                union {
                    unsigned short  ms40_r12;  /* to choose, set choice to
                                                * ms40_r12_chosen */
                    unsigned short  ms80_r12;  /* to choose, set choice to
                                                * ms80_r12_chosen */
                    unsigned short  ms160_r12;  /* to choose, set choice to
                                                 * ms160_r12_chosen */
                } u;
            } dmtc_PeriodOffset_r12;
            struct {
                unsigned short  choice;
#                   define      durationFDD_r12_chosen 1
#                   define      durationTDD_r12_chosen 2
                union {
                    unsigned short  durationFDD_r12;  /* to choose, set choice
                                                 * to durationFDD_r12_chosen */
                    unsigned short  durationTDD_r12;  /* to choose, set choice
                                                 * to durationTDD_r12_chosen */
                } u;
            } ds_OccasionDuration_r12;
            struct MeasCSI_RS_ToRemoveList_r12 *measCSI_RS_ToRemoveList_r12;  
                                        /* optional; set in bit_mask
                                         * measCSI_RS_ToRemoveList_r12_present
                                         * if present */
                                                                              /* Need ON */
            struct MeasCSI_RS_ToAddModList_r12 *measCSI_RS_ToAddModList_r12;  
                                        /* optional; set in bit_mask
                                         * measCSI_RS_ToAddModList_r12_present
                                         * if present */
                                                                              /* Need ON */
        } setup;  /* to choose, set choice to MeasDS_Config_r12_setup_chosen */
    } u;
} MeasDS_Config_r12;

typedef unsigned short  MeasCSI_RS_Id_r12;

typedef struct MeasCSI_RS_ToRemoveList_r12 {
    struct MeasCSI_RS_ToRemoveList_r12 *next;
    MeasCSI_RS_Id_r12 value;
} *MeasCSI_RS_ToRemoveList_r12;

typedef struct MeasCSI_RS_Config_r12 {
    MeasCSI_RS_Id_r12 measCSI_RS_Id_r12;
    unsigned short  physCellId_r12;
    unsigned short  scramblingIdentity_r12;
    unsigned short  resourceConfig_r12;
    unsigned short  subframeOffset_r12;
    Q_OffsetRange   csi_RS_IndividualOffset_r12;
} MeasCSI_RS_Config_r12;

typedef struct MeasCSI_RS_ToAddModList_r12 {
    struct MeasCSI_RS_ToAddModList_r12 *next;
    MeasCSI_RS_Config_r12 value;
} *MeasCSI_RS_ToAddModList_r12;

typedef struct MeasIdToAddMod {
    MeasId          measId;
    MeasObjectId    measObjectId;
    ReportConfigId  reportConfigId;
} MeasIdToAddMod;

typedef struct MeasIdToAddModList {
    struct MeasIdToAddModList *next;
    MeasIdToAddMod  value;
} *MeasIdToAddModList;

typedef struct MeasIdToAddModExt_r12 {
    MeasId_v1250    measId_v1250;
    MeasObjectId    measObjectId_r12;
    ReportConfigId  reportConfigId_r12;
} MeasIdToAddModExt_r12;

typedef struct MeasIdToAddModListExt_r12 {
    struct MeasIdToAddModListExt_r12 *next;
    MeasIdToAddModExt_r12 value;
} *MeasIdToAddModListExt_r12;

typedef struct MeasObjectCDMA2000 {
    unsigned char   bit_mask;
#       define      MeasObjectCDMA2000_searchWindowSize_present 0x80
#       define      MeasObjectCDMA2000_offsetFreq_present 0x40
#       define      MeasObjectCDMA2000_cellsToRemoveList_present 0x20
#       define      MeasObjectCDMA2000_cellsToAddModList_present 0x10
#       define      MeasObjectCDMA2000_cellForWhichToReportCGI_present 0x08
    CDMA2000_Type   cdma2000_Type;
    CarrierFreqCDMA2000 carrierFreq;
    unsigned short  searchWindowSize;  /* optional; set in bit_mask
                               * MeasObjectCDMA2000_searchWindowSize_present if
                               * present */
                                       /* Need ON */
    Q_OffsetRangeInterRAT offsetFreq;  /* MeasObjectCDMA2000_offsetFreq_present
                                        * not set in bit_mask implies value is
                                        * 0 */
    struct CellIndexList *cellsToRemoveList;  /* optional; set in bit_mask
                              * MeasObjectCDMA2000_cellsToRemoveList_present if
                              * present */
                                              /* Need ON */
    struct CellsToAddModListCDMA2000 *cellsToAddModList;  /* optional; set in
                                   * bit_mask
                              * MeasObjectCDMA2000_cellsToAddModList_present if
                              * present */
                                                          /* Need ON */
    PhysCellIdCDMA2000 cellForWhichToReportCGI;  /* optional; set in bit_mask
                        * MeasObjectCDMA2000_cellForWhichToReportCGI_present if
                        * present */
                                                 /* Need ON */
} MeasObjectCDMA2000;

typedef struct CellsToAddModCDMA2000 {
    unsigned short  cellIndex;
    PhysCellIdCDMA2000 physCellId;
} CellsToAddModCDMA2000;

typedef struct CellsToAddModListCDMA2000 {
    struct CellsToAddModListCDMA2000 *next;
    CellsToAddModCDMA2000 value;
} *CellsToAddModListCDMA2000;

typedef enum MeasCycleSCell_r10 {
    MeasCycleSCell_r10_sf160 = 0,
    MeasCycleSCell_r10_sf256 = 1,
    MeasCycleSCell_r10_sf320 = 2,
    MeasCycleSCell_r10_sf512 = 3,
    MeasCycleSCell_r10_sf640 = 4,
    MeasCycleSCell_r10_sf1024 = 5,
    MeasCycleSCell_r10_sf1280 = 6,
    MeasCycleSCell_r10_spare1 = 7
} MeasCycleSCell_r10;

typedef struct MeasSubframePatternConfigNeigh_r10 {
    unsigned short  choice;
#       define      MeasSubframePatternConfigNeigh_r10_release_chosen 1
#       define      MeasSubframePatternConfigNeigh_r10_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                         * MeasSubframePatternConfigNeigh_r10_release_chosen */
        struct _seq87 {
            unsigned char   bit_mask;
#               define      measSubframeCellList_r10_present 0x80
            MeasSubframePattern_r10 measSubframePatternNeigh_r10;
            struct MeasSubframeCellList_r10 *measSubframeCellList_r10;  
                                  /* optional; set in bit_mask
                                   * measSubframeCellList_r10_present if
                                   * present */
                                                                        /* Cond always */
        } setup;  /* to choose, set choice to
                   * MeasSubframePatternConfigNeigh_r10_setup_chosen */
    } u;
} MeasSubframePatternConfigNeigh_r10;

typedef struct MeasObjectEUTRA {
    unsigned short  bit_mask;
#       define      MeasObjectEUTRA_offsetFreq_present 0x8000
#       define      MeasObjectEUTRA_cellsToRemoveList_present 0x4000
#       define      MeasObjectEUTRA_cellsToAddModList_present 0x2000
#       define      blackCellsToRemoveList_present 0x1000
#       define      blackCellsToAddModList_present 0x0800
#       define      MeasObjectEUTRA_cellForWhichToReportCGI_present 0x0400
#       define      measCycleSCell_r10_present 0x0200
#       define      measSubframePatternConfigNeigh_r10_present 0x0100
#       define      widebandRSRQ_Meas_r11_present 0x0080
#       define      altTTT_CellsToRemoveList_r12_present 0x0040
#       define      altTTT_CellsToAddModList_r12_present 0x0020
#       define      t312_r12_present 0x0010
#       define      MeasObjectEUTRA_reducedMeasPerformance_r12_present 0x0008
#       define      measDS_Config_r12_present 0x0004
    ARFCN_ValueEUTRA carrierFreq;
    AllowedMeasBandwidth allowedMeasBandwidth;
    PresenceAntennaPort1 presenceAntennaPort1;
    NeighCellConfig neighCellConfig;
    Q_OffsetRange   offsetFreq;  /* MeasObjectEUTRA_offsetFreq_present not set
                                  * in bit_mask implies value is dB0 */
	/* Cell list */
    struct CellIndexList *cellsToRemoveList;  /* optional; set in bit_mask
                                 * MeasObjectEUTRA_cellsToRemoveList_present if
                                 * present */
                                              /* Need ON */
    struct CellsToAddModList *cellsToAddModList;  /* optional; set in bit_mask
                                 * MeasObjectEUTRA_cellsToAddModList_present if
                                 * present */
                                                  /* Need ON */
	/* Black list */
    struct CellIndexList *blackCellsToRemoveList;  /* optional; set in bit_mask
                                            * blackCellsToRemoveList_present if
                                            * present */
                                                   /* Need ON */
    struct BlackCellsToAddModList *blackCellsToAddModList;  /* optional; set in
                                   * bit_mask blackCellsToAddModList_present if
                                   * present */
                                                            /* Need ON */
    PhysCellId      cellForWhichToReportCGI;  /* optional; set in bit_mask
                           * MeasObjectEUTRA_cellForWhichToReportCGI_present if
                           * present */
                                              /* Need ON */
    MeasCycleSCell_r10 measCycleSCell_r10;  /* extension #1; optional; set in
                                   * bit_mask measCycleSCell_r10_present if
                                   * present */
                                            /* Need ON */
    MeasSubframePatternConfigNeigh_r10 measSubframePatternConfigNeigh_r10;  
                                        /* extension #1; optional; set in
                                   * bit_mask
                                   * measSubframePatternConfigNeigh_r10_present
                                   * if present */
                                                                            /* Need ON */
    ossBoolean      widebandRSRQ_Meas_r11;  /* extension #2; optional; set in
                                   * bit_mask widebandRSRQ_Meas_r11_present if
                                   * present */
                                            /* Cond WB-RSRQ */
    struct CellIndexList *altTTT_CellsToRemoveList_r12;  /* extension #3;
                                   * optional; set in bit_mask
                                   * altTTT_CellsToRemoveList_r12_present if
                                   * present */
                                                         /* Need ON */
    struct AltTTT_CellsToAddModList_r12 *altTTT_CellsToAddModList_r12;  
                                  /* extension #3; optional; set in bit_mask
                                   * altTTT_CellsToAddModList_r12_present if
                                   * present */
                                                                        /* Need ON */
    struct {
        unsigned short  choice;
#           define      t312_r12_release_chosen 1
#           define      t312_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * t312_r12_release_chosen */
            enum _enum35 {
                setup_ms0 = 0,
                setup_ms50 = 1,
                setup_ms100 = 2,
                setup_ms200 = 3,
                setup_ms300 = 4,
                setup_ms400 = 5,
                setup_ms500 = 6,
                setup_ms1000 = 7
            } setup;  /* to choose, set choice to t312_r12_setup_chosen */
        } u;
    } t312_r12;  /* extension #3; optional; set in bit_mask t312_r12_present if
                  * present */
               /* Need ON */
    ossBoolean      reducedMeasPerformance_r12;  /* extension #3; optional; set
                                   * in bit_mask
                        * MeasObjectEUTRA_reducedMeasPerformance_r12_present if
                        * present */
                                                 /* Need ON */
    MeasDS_Config_r12 measDS_Config_r12;  /* extension #3; optional; set in
                                   * bit_mask measDS_Config_r12_present if
                                   * present */
                                          /* Need ON */
} MeasObjectEUTRA;

typedef struct MeasObjectEUTRA_v9e0 {
    ARFCN_ValueEUTRA_v9e0 carrierFreq_v9e0;
} MeasObjectEUTRA_v9e0;

typedef struct CellsToAddMod {
    unsigned short  cellIndex;
    PhysCellId      physCellId;
    Q_OffsetRange   cellIndividualOffset;
} CellsToAddMod;

typedef struct CellsToAddModList {
    struct CellsToAddModList *next;
    CellsToAddMod   value;
} *CellsToAddModList;

typedef struct BlackCellsToAddMod {
    unsigned short  cellIndex;
    PhysCellIdRange physCellIdRange;
} BlackCellsToAddMod;

typedef struct BlackCellsToAddModList {
    struct BlackCellsToAddModList *next;
    BlackCellsToAddMod value;
} *BlackCellsToAddModList;

typedef struct MeasSubframeCellList_r10 {
    struct MeasSubframeCellList_r10 *next;
    PhysCellIdRange value;
} *MeasSubframeCellList_r10;

typedef struct AltTTT_CellsToAddMod_r12 {
    unsigned short  cellIndex_r12;
    PhysCellIdRange physCellIdRange_r12;
} AltTTT_CellsToAddMod_r12;

typedef struct AltTTT_CellsToAddModList_r12 {
    struct AltTTT_CellsToAddModList_r12 *next;
    AltTTT_CellsToAddMod_r12 value;
} *AltTTT_CellsToAddModList_r12;

typedef struct MeasObjectGERAN {
    unsigned char   bit_mask;
#       define      MeasObjectGERAN_offsetFreq_present 0x80
#       define      ncc_Permitted_present 0x40
#       define      MeasObjectGERAN_cellForWhichToReportCGI_present 0x20
    CarrierFreqsGERAN carrierFreqs;
    Q_OffsetRangeInterRAT offsetFreq;  /* MeasObjectGERAN_offsetFreq_present not
                                        * set in bit_mask implies value is 0 */
    _bit1           ncc_Permitted;  /* ncc_Permitted_present not set in bit_mask
                                     * implies value is '11111111'B */
    PhysCellIdGERAN cellForWhichToReportCGI;  /* optional; set in bit_mask
                           * MeasObjectGERAN_cellForWhichToReportCGI_present if
                           * present */
                                              /* Need ON */
} MeasObjectGERAN;

typedef struct CSG_AllowedReportingCells_r9 {
    unsigned char   bit_mask;
#       define      physCellIdRangeUTRA_FDDList_r9_present 0x80
    struct PhysCellIdRangeUTRA_FDDList_r9 *physCellIdRangeUTRA_FDDList_r9;  
                                        /* optional; set in bit_mask
                                    * physCellIdRangeUTRA_FDDList_r9_present if
                                    * present */
                                                                            /* Need OR */
} CSG_AllowedReportingCells_r9;

typedef struct MeasObjectUTRA {
    unsigned char   bit_mask;
#       define      MeasObjectUTRA_offsetFreq_present 0x80
#       define      MeasObjectUTRA_cellsToRemoveList_present 0x40
#       define      MeasObjectUTRA_cellsToAddModList_present 0x20
#       define      MeasObjectUTRA_cellForWhichToReportCGI_present 0x10
#       define      csg_allowedReportingCells_v930_present 0x08
#       define      MeasObjectUTRA_reducedMeasPerformance_r12_present 0x04
    ARFCN_ValueUTRA carrierFreq;
    Q_OffsetRangeInterRAT offsetFreq;  /* MeasObjectUTRA_offsetFreq_present not
                                        * set in bit_mask implies value is 0 */
    struct CellIndexList *cellsToRemoveList;  /* optional; set in bit_mask
                                  * MeasObjectUTRA_cellsToRemoveList_present if
                                  * present */
                                              /* Need ON */
    struct {
        unsigned short  choice;
#           define      cellsToAddModListUTRA_FDD_chosen 1
#           define      cellsToAddModListUTRA_TDD_chosen 2
        union {
            struct CellsToAddModListUTRA_FDD *cellsToAddModListUTRA_FDD;  
                                        /* to choose, set choice to
                                         * cellsToAddModListUTRA_FDD_chosen */
            struct CellsToAddModListUTRA_TDD *cellsToAddModListUTRA_TDD;  
                                        /* to choose, set choice to
                                         * cellsToAddModListUTRA_TDD_chosen */
        } u;
    } cellsToAddModList;  /* optional; set in bit_mask
                           * MeasObjectUTRA_cellsToAddModList_present if
                           * present */
                       /* Need ON */
    struct {
        unsigned short  choice;
#           define      cellForWhichToReportCGI_utra_FDD_chosen 1
#           define      cellForWhichToReportCGI_utra_TDD_chosen 2
        union {
            PhysCellIdUTRA_FDD utra_FDD;  /* to choose, set choice to
                                   * cellForWhichToReportCGI_utra_FDD_chosen */
            PhysCellIdUTRA_TDD utra_TDD;  /* to choose, set choice to
                                   * cellForWhichToReportCGI_utra_TDD_chosen */
        } u;
    } cellForWhichToReportCGI;  /* optional; set in bit_mask
                            * MeasObjectUTRA_cellForWhichToReportCGI_present if
                            * present */
       /* Need ON */
    CSG_AllowedReportingCells_r9 csg_allowedReportingCells_v930;  /* extension
                                   * #1; optional; set in bit_mask
                                   * csg_allowedReportingCells_v930_present if
                                   * present */
                                                                  /* Need ON */
    ossBoolean      reducedMeasPerformance_r12;  /* extension #2; optional; set
                                   * in bit_mask
                         * MeasObjectUTRA_reducedMeasPerformance_r12_present if
                         * present */
                                                 /* Need ON */
} MeasObjectUTRA;

typedef struct MeasObjectToAddMod {
    MeasObjectId    measObjectId;
    struct {
        unsigned short  choice;
#           define      measObjectEUTRA_chosen 1
#           define      measObjectUTRA_chosen 2
#           define      measObjectGERAN_chosen 3
#           define      measObjectCDMA2000_chosen 4
        union {
            MeasObjectEUTRA measObjectEUTRA;  /* to choose, set choice to
                                               * measObjectEUTRA_chosen */
            MeasObjectUTRA  measObjectUTRA;  /* to choose, set choice to
                                              * measObjectUTRA_chosen */
            MeasObjectGERAN measObjectGERAN;  /* to choose, set choice to
                                               * measObjectGERAN_chosen */
            MeasObjectCDMA2000 measObjectCDMA2000;  /* to choose, set choice to
                                                 * measObjectCDMA2000_chosen */
        } u;
    } measObject;
} MeasObjectToAddMod;

typedef struct MeasObjectToAddModList {
    struct MeasObjectToAddModList *next;
    MeasObjectToAddMod value;
} *MeasObjectToAddModList;

typedef struct MeasObjectToAddMod_v9e0 {
    unsigned char   bit_mask;
#       define      measObjectEUTRA_v9e0_present 0x80
    MeasObjectEUTRA_v9e0 measObjectEUTRA_v9e0;  /* optional; set in bit_mask
                                                 * measObjectEUTRA_v9e0_present
                                                 * if present */
                                                /* Cond eutra */
} MeasObjectToAddMod_v9e0;

typedef struct MeasObjectToAddModList_v9e0 {
    struct MeasObjectToAddModList_v9e0 *next;
    MeasObjectToAddMod_v9e0 value;
} *MeasObjectToAddModList_v9e0;

typedef struct CellsToAddModUTRA_FDD {
    unsigned short  cellIndex;
    PhysCellIdUTRA_FDD physCellId;
} CellsToAddModUTRA_FDD;

typedef struct CellsToAddModListUTRA_FDD {
    struct CellsToAddModListUTRA_FDD *next;
    CellsToAddModUTRA_FDD value;
} *CellsToAddModListUTRA_FDD;

typedef struct CellsToAddModUTRA_TDD {
    unsigned short  cellIndex;
    PhysCellIdUTRA_TDD physCellId;
} CellsToAddModUTRA_TDD;

typedef struct CellsToAddModListUTRA_TDD {
    struct CellsToAddModListUTRA_TDD *next;
    CellsToAddModUTRA_TDD value;
} *CellsToAddModListUTRA_TDD;

typedef struct AdditionalSI_Info_r9 {
    unsigned char   bit_mask;
#       define      csg_MemberStatus_r9_present 0x80
#       define      csg_Identity_r9_present 0x40
    enum {
        member = 0
    } csg_MemberStatus_r9;  /* optional; set in bit_mask
                             * csg_MemberStatus_r9_present if present */
    CSG_Identity    csg_Identity_r9;  /* optional; set in bit_mask
                                       * csg_Identity_r9_present if present */
} AdditionalSI_Info_r9;

typedef struct MeasResultEUTRA {
    unsigned char   bit_mask;
#       define      MeasResultEUTRA_cgi_Info_present 0x80
    PhysCellId      physCellId;
    struct {
        unsigned char   bit_mask;
#           define      MeasResultEUTRA_cgi_Info_plmn_IdentityList_present 0x80
        CellGlobalIdEUTRA cellGlobalId;
        TrackingAreaCode trackingAreaCode;
        struct PLMN_IdentityList2 *plmn_IdentityList;  /* optional; set in
                                   * bit_mask
                        * MeasResultEUTRA_cgi_Info_plmn_IdentityList_present if
                        * present */
    } cgi_Info;  /* optional; set in bit_mask MeasResultEUTRA_cgi_Info_present
                  * if present */
    struct {
        unsigned char   bit_mask;
#           define      rsrpResult_present 0x80
#           define      rsrqResult_present 0x40
#           define      MeasResultEUTRA_measResult_additionalSI_Info_r9_present 0x20
#           define      MeasResultEUTRA_measResult_primaryPLMN_Suitable_r12_present 0x10
#           define      measResult_v1250_present 0x08
        RSRP_Range      rsrpResult;  /* optional; set in bit_mask
                                      * rsrpResult_present if present */
        RSRQ_Range      rsrqResult;  /* optional; set in bit_mask
                                      * rsrqResult_present if present */
        AdditionalSI_Info_r9 additionalSI_Info_r9;  /* extension #1; optional;
                                   * set in bit_mask
                   * MeasResultEUTRA_measResult_additionalSI_Info_r9_present if
                   * present */
        _enum3          primaryPLMN_Suitable_r12;  /* extension #2; optional;
                                   * set in bit_mask
               * MeasResultEUTRA_measResult_primaryPLMN_Suitable_r12_present if
               * present */
        RSRQ_Range_v1250 measResult_v1250;  /* extension #2; optional; set in
                                   * bit_mask measResult_v1250_present if
                                   * present */
    } measResult;
} MeasResultEUTRA;

typedef struct MeasResultListEUTRA {
    struct MeasResultListEUTRA *next;
    MeasResultEUTRA value;
} *MeasResultListEUTRA;

typedef struct MeasResultServFreq_r10 {
    unsigned char   bit_mask;
#       define      measResultSCell_r10_present 0x80
#       define      measResultBestNeighCell_r10_present 0x40
#       define      measResultSCell_v1250_present 0x20
#       define      measResultBestNeighCell_v1250_present 0x10
    ServCellIndex_r10 servFreqId_r10;
    struct {
        RSRP_Range      rsrpResultSCell_r10;
        RSRQ_Range      rsrqResultSCell_r10;
    } measResultSCell_r10;  /* optional; set in bit_mask
                             * measResultSCell_r10_present if present */
    struct {
        PhysCellId      physCellId_r10;
        RSRP_Range      rsrpResultNCell_r10;
        RSRQ_Range      rsrqResultNCell_r10;
    } measResultBestNeighCell_r10;  /* optional; set in bit_mask
                                     * measResultBestNeighCell_r10_present if
                                     * present */
    RSRQ_Range_v1250 measResultSCell_v1250;  /* extension #1; optional; set in
                                   * bit_mask measResultSCell_v1250_present if
                                   * present */
    RSRQ_Range_v1250 measResultBestNeighCell_v1250;  /* extension #1; optional;
                                   * set in bit_mask
                                   * measResultBestNeighCell_v1250_present if
                                   * present */
} MeasResultServFreq_r10;

typedef struct MeasResultServFreqList_r10 {
    struct MeasResultServFreqList_r10 *next;
    MeasResultServFreq_r10 value;
} *MeasResultServFreqList_r10;

typedef struct MeasResultCSI_RS_r12 {
    MeasCSI_RS_Id_r12 measCSI_RS_Id_r12;
    CSI_RSRP_Range_r12 csi_RSRP_Result_r12;
} MeasResultCSI_RS_r12;

typedef struct MeasResultCSI_RS_List_r12 {
    struct MeasResultCSI_RS_List_r12 *next;
    MeasResultCSI_RS_r12 value;
} *MeasResultCSI_RS_List_r12;

typedef struct _choice52 {
    unsigned short  choice;
#       define      physCellId_fdd_chosen 1
#       define      physCellId_tdd_chosen 2
    union {
        PhysCellIdUTRA_FDD fdd;  /* to choose, set choice to
                                  * physCellId_fdd_chosen */
        PhysCellIdUTRA_TDD tdd;  /* to choose, set choice to
                                  * physCellId_tdd_chosen */
    } u;
} _choice52;

typedef struct MeasResultUTRA {
    unsigned char   bit_mask;
#       define      MeasResultUTRA_cgi_Info_present 0x80
    _choice52       physCellId;
    struct {
        unsigned char   bit_mask;
#           define      locationAreaCode_present 0x80
#           define      MeasResultUTRA_cgi_Info_routingAreaCode_present 0x40
#           define      MeasResultUTRA_cgi_Info_plmn_IdentityList_present 0x20
        CellGlobalIdUTRA cellGlobalId;
        _bit1           locationAreaCode;  /* optional; set in bit_mask
                                            * locationAreaCode_present if
                                            * present */
        _bit1           routingAreaCode;  /* optional; set in bit_mask
                           * MeasResultUTRA_cgi_Info_routingAreaCode_present if
                           * present */
        struct PLMN_IdentityList2 *plmn_IdentityList;  /* optional; set in
                                   * bit_mask
                         * MeasResultUTRA_cgi_Info_plmn_IdentityList_present if
                         * present */
    } cgi_Info;  /* optional; set in bit_mask MeasResultUTRA_cgi_Info_present if
                  * present */
    struct {
        unsigned char   bit_mask;
#           define      utra_RSCP_present 0x80
#           define      utra_EcN0_present 0x40
#           define      MeasResultUTRA_measResult_additionalSI_Info_r9_present 0x20
#           define      MeasResultUTRA_measResult_primaryPLMN_Suitable_r12_present 0x10
        short           utra_RSCP;  /* optional; set in bit_mask
                                     * utra_RSCP_present if present */
        unsigned short  utra_EcN0;  /* optional; set in bit_mask
                                     * utra_EcN0_present if present */
        AdditionalSI_Info_r9 additionalSI_Info_r9;  /* extension #1; optional;
                                   * set in bit_mask
                    * MeasResultUTRA_measResult_additionalSI_Info_r9_present if
                    * present */
        _enum3          primaryPLMN_Suitable_r12;  /* extension #2; optional;
                                   * set in bit_mask
                * MeasResultUTRA_measResult_primaryPLMN_Suitable_r12_present if
                * present */
    } measResult;
} MeasResultUTRA;

typedef struct MeasResultListUTRA {
    struct MeasResultListUTRA *next;
    MeasResultUTRA  value;
} *MeasResultListUTRA;

typedef struct MeasResultGERAN {
    unsigned char   bit_mask;
#       define      MeasResultGERAN_cgi_Info_present 0x80
    CarrierFreqGERAN carrierFreq;
    PhysCellIdGERAN physCellId;
    struct {
        unsigned char   bit_mask;
#           define      MeasResultGERAN_cgi_Info_routingAreaCode_present 0x80
        CellGlobalIdGERAN cellGlobalId;
        _bit1           routingAreaCode;  /* optional; set in bit_mask
                          * MeasResultGERAN_cgi_Info_routingAreaCode_present if
                          * present */
    } cgi_Info;  /* optional; set in bit_mask MeasResultGERAN_cgi_Info_present
                  * if present */
    struct {
        unsigned short  rssi;
    } measResult;
} MeasResultGERAN;

typedef struct MeasResultListGERAN {
    struct MeasResultListGERAN *next;
    MeasResultGERAN value;
} *MeasResultListGERAN;

typedef struct MeasResultCDMA2000 {
    unsigned char   bit_mask;
#       define      MeasResultCDMA2000_cgi_Info_present 0x80
    PhysCellIdCDMA2000 physCellId;
    CellGlobalIdCDMA2000 cgi_Info;  /* optional; set in bit_mask
                                     * MeasResultCDMA2000_cgi_Info_present if
                                     * present */
    struct {
        unsigned char   bit_mask;
#           define      pilotPnPhase_present 0x80
        unsigned short  pilotPnPhase;  /* optional; set in bit_mask
                                        * pilotPnPhase_present if present */
        unsigned short  pilotStrength;
    } measResult;
} MeasResultCDMA2000;

typedef struct MeasResultListCDMA2000 {
    struct MeasResultListCDMA2000 *next;
    MeasResultCDMA2000 value;
} *MeasResultListCDMA2000;

typedef struct PLMN_IdentityList2 {
    struct PLMN_IdentityList2 *next;
    PLMN_Identity   value;
} *PLMN_IdentityList2;

typedef struct ThresholdEUTRA {
    unsigned short  choice;
#       define      threshold_RSRP_chosen 1
#       define      threshold_RSRQ_chosen 2
    union {
        RSRP_Range      threshold_RSRP;  /* to choose, set choice to
                                          * threshold_RSRP_chosen */
        RSRQ_Range      threshold_RSRQ;  /* to choose, set choice to
                                          * threshold_RSRQ_chosen */
    } u;
} ThresholdEUTRA;

typedef CSI_RSRP_Range_r12 ThresholdEUTRA_v1250;

typedef enum TimeToTrigger {
    TimeToTrigger_ms0 = 0,
    TimeToTrigger_ms40 = 1,
    ms64 = 2,
    TimeToTrigger_ms80 = 3,
    TimeToTrigger_ms100 = 4,
    ms128 = 5,
    TimeToTrigger_ms160 = 6,
    ms256 = 7,
    ms320 = 8,
    TimeToTrigger_ms480 = 9,
    ms512 = 10,
    TimeToTrigger_ms640 = 11,
    TimeToTrigger_ms1024 = 12,
    TimeToTrigger_ms1280 = 13,
    TimeToTrigger_ms2560 = 14,
    TimeToTrigger_ms5120 = 15
} TimeToTrigger;

typedef enum ReportInterval {
    ReportInterval_ms120 = 0,
    ReportInterval_ms240 = 1,
    ReportInterval_ms480 = 2,
    ReportInterval_ms640 = 3,
    ReportInterval_ms1024 = 4,
    ms2048 = 5,
    ReportInterval_ms5120 = 6,
    ReportInterval_ms10240 = 7,
    ReportInterval_min1 = 8,
    ReportInterval_min6 = 9,
    ReportInterval_min12 = 10,
    ReportInterval_min30 = 11,
    ReportInterval_min60 = 12,
    ReportInterval_spare3 = 13,
    ReportInterval_spare2 = 14,
    ReportInterval_spare1 = 15
} ReportInterval;

typedef struct RSRQ_RangeConfig_r12 {
    unsigned short  choice;
#       define      RSRQ_RangeConfig_r12_release_chosen 1
#       define      RSRQ_RangeConfig_r12_setup_chosen 2
    union {
        Nulltype        release;  /* to choose, set choice to
                                   * RSRQ_RangeConfig_r12_release_chosen */
        RSRQ_Range_v1250 setup;  /* to choose, set choice to
                                  * RSRQ_RangeConfig_r12_setup_chosen */
    } u;
} RSRQ_RangeConfig_r12;

typedef enum _enum36 {
    r1 = 0,
    r2 = 1,
    reportAmount_r4 = 2,
    reportAmount_r8 = 3,
    reportAmount_r16 = 4,
    reportAmount_r32 = 5,
    reportAmount_r64 = 6,
    reportAmount_infinity = 7
} _enum36;

typedef struct ReportConfigEUTRA {
    unsigned short  bit_mask;
#       define      ReportConfigEUTRA_si_RequestForHO_r9_present 0x8000
#       define      ue_RxTxTimeDiffPeriodical_r9_present 0x4000
#       define      includeLocationInfo_r10_present 0x2000
#       define      reportAddNeighMeas_r10_present 0x1000
#       define      ReportConfigEUTRA_alternativeTimeToTrigger_r12_present 0x0800
#       define      useT312_r12_present 0x0400
#       define      usePSCell_r12_present 0x0200
#       define      aN_Threshold1_v1250_present 0x0100
#       define      a5_Threshold2_v1250_present 0x0080
#       define      reportStrongestCSI_RSs_r12_present 0x0040
#       define      reportCRS_Meas_r12_present 0x0020
#       define      triggerQuantityCSI_RS_r12_present 0x0010
    struct {
        unsigned short  choice;
#           define      ReportConfigEUTRA_triggerType_event_chosen 1
#           define      ReportConfigEUTRA_triggerType_periodical_chosen 2
        union {
            struct _seq96 {
                struct {
                    unsigned short  choice;
#                       define      eventA1_chosen 1
#                       define      eventA2_chosen 2
#                       define      eventA3_chosen 3
#                       define      eventA4_chosen 4
#                       define      eventA5_chosen 5
#                       define      eventA6_r10_chosen 6
#                       define      eventC1_r12_chosen 7
#                       define      eventC2_r12_chosen 8
                    union {
                        struct _seq88 {
                            ThresholdEUTRA  a1_Threshold;
                        } eventA1;  /* to choose, set choice to
                                     * eventA1_chosen */
                        struct _seq89 {
                            ThresholdEUTRA  a2_Threshold;
                        } eventA2;  /* to choose, set choice to
                                     * eventA2_chosen */
                        struct _seq90 {
                            short           a3_Offset;
                            ossBoolean      reportOnLeave;
                        } eventA3;  /* to choose, set choice to
                                     * eventA3_chosen */
                        struct _seq91 {
                            ThresholdEUTRA  a4_Threshold;
                        } eventA4;  /* to choose, set choice to
                                     * eventA4_chosen */
                        struct _seq92 {
                            ThresholdEUTRA  a5_Threshold1;
                            ThresholdEUTRA  a5_Threshold2;
                        } eventA5;  /* to choose, set choice to
                                     * eventA5_chosen */
                        struct _seq93 {
                            short           a6_Offset_r10;
                            ossBoolean      a6_ReportOnLeave_r10;
                        } eventA6_r10;  /* extension #1; to choose, set choice
                                         * to eventA6_r10_chosen */
                        struct _seq94 {
                            ThresholdEUTRA_v1250 c1_Threshold_r12;
                            ossBoolean      c1_ReportOnLeave_r12;
                        } eventC1_r12;  /* extension #2; to choose, set choice
                                         * to eventC1_r12_chosen */
                        struct _seq95 {
                            MeasCSI_RS_Id_r12 c2_RefCSI_RS_r12;
                            short           c2_Offset_r12;
                            ossBoolean      c2_ReportOnLeave_r12;
                        } eventC2_r12;  /* extension #3; to choose, set choice
                                         * to eventC2_r12_chosen */
                    } u;
                } eventId;
                Hysteresis      hysteresis;
                TimeToTrigger   timeToTrigger;
            } event;  /* to choose, set choice to
                       * ReportConfigEUTRA_triggerType_event_chosen */
            struct _seq97 {
                enum {
                    ReportConfigEUTRA_triggerType_periodical_purpose_reportStrongestCells = 0,
                    ReportConfigEUTRA_triggerType_periodical_purpose_reportCGI = 1
                } purpose;
            } periodical;  /* to choose, set choice to
                           * ReportConfigEUTRA_triggerType_periodical_chosen */
        } u;
    } triggerType;
    enum {
        rsrp = 0,
        rsrq = 1
    } triggerQuantity;
    enum {
        sameAsTriggerQuantity = 0,
        reportQuantity_both = 1
    } reportQuantity;
    unsigned short  maxReportCells;
    ReportInterval  reportInterval;
    _enum36         reportAmount;
    _enum17         si_RequestForHO_r9;  /* extension #1; optional; set in
                                   * bit_mask
                              * ReportConfigEUTRA_si_RequestForHO_r9_present if
                              * present */
       /* Cond reportCGI */
    _enum17         ue_RxTxTimeDiffPeriodical_r9;  /* extension #1; optional;
                                   * set in bit_mask
                                   * ue_RxTxTimeDiffPeriodical_r9_present if
                                   * present */
                                                   /* Need OR */
    _enum3          includeLocationInfo_r10;  /* extension #2; optional; set in
                                   * bit_mask includeLocationInfo_r10_present if
                                   * present */
       /* Need OR */
    _enum17         reportAddNeighMeas_r10;  /* extension #2; optional; set in
                                   * bit_mask reportAddNeighMeas_r10_present if
                                   * present */
                                             /* Need OR */
    struct {
        unsigned short  choice;
#           define      alternativeTimeToTrigger_r12_release_chosen 1
#           define      alternativeTimeToTrigger_r12_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                               * alternativeTimeToTrigger_r12_release_chosen */
            TimeToTrigger   setup;  /* to choose, set choice to
                                 * alternativeTimeToTrigger_r12_setup_chosen */
        } u;
    } alternativeTimeToTrigger_r12;  /* extension #3; optional; set in bit_mask
                    * ReportConfigEUTRA_alternativeTimeToTrigger_r12_present if
                    * present */
       /* Need ON */
    ossBoolean      useT312_r12;  /* extension #3; optional; set in bit_mask
                                   * useT312_r12_present if present */
                                  /* Need ON */
    ossBoolean      usePSCell_r12;  /* extension #3; optional; set in bit_mask
                                     * usePSCell_r12_present if present */
                                    /* Need ON */
    RSRQ_RangeConfig_r12 aN_Threshold1_v1250;  /* extension #3; optional; set in
                                   * bit_mask aN_Threshold1_v1250_present if
                                   * present */
                                               /* Need ON */
    RSRQ_RangeConfig_r12 a5_Threshold2_v1250;  /* extension #3; optional; set in
                                   * bit_mask a5_Threshold2_v1250_present if
                                   * present */
                                               /* Need ON */
    ossBoolean      reportStrongestCSI_RSs_r12;  /* extension #3; optional; set
                                   * in bit_mask
                                   * reportStrongestCSI_RSs_r12_present if
                                   * present */
                                                 /* Need ON */
    ossBoolean      reportCRS_Meas_r12;  /* extension #3; optional; set in
                                   * bit_mask reportCRS_Meas_r12_present if
                                   * present */
                                         /* Need ON */
    ossBoolean      triggerQuantityCSI_RS_r12;  /* extension #3; optional; set
                                   * in bit_mask
                                   * triggerQuantityCSI_RS_r12_present if
                                   * present */
                                                /* Need ON */
} ReportConfigEUTRA;

typedef struct ThresholdUTRA {
    unsigned short  choice;
#       define      utra_RSCP_chosen 1
#       define      utra_EcN0_chosen 2
    union {
        short           utra_RSCP;  /* to choose, set choice to
                                     * utra_RSCP_chosen */
        unsigned short  utra_EcN0;  /* to choose, set choice to
                                     * utra_EcN0_chosen */
    } u;
} ThresholdUTRA;

typedef unsigned short  ThresholdGERAN;

typedef unsigned short  ThresholdCDMA2000;

typedef struct ReportConfigInterRAT {
    unsigned char   bit_mask;
#       define      ReportConfigInterRAT_si_RequestForHO_r9_present 0x80
#       define      reportQuantityUTRA_FDD_r10_present 0x40
#       define      includeLocationInfo_r11_present 0x20
#       define      b2_Threshold1_v1250_present 0x10
    struct {
        unsigned short  choice;
#           define      ReportConfigInterRAT_triggerType_event_chosen 1
#           define      ReportConfigInterRAT_triggerType_periodical_chosen 2
        union {
            struct _seq100 {
                struct {
                    unsigned short  choice;
#                       define      eventB1_chosen 1
#                       define      eventB2_chosen 2
                    union {
                        struct _seq98 {
                            struct {
                                unsigned short  choice;
#                                   define      b1_ThresholdUTRA_chosen 1
#                                   define      b1_ThresholdGERAN_chosen 2
#                                   define      b1_ThresholdCDMA2000_chosen 3
                                union {
                                    ThresholdUTRA   b1_ThresholdUTRA;  /* to
                                   * choose, set choice to
                                   * b1_ThresholdUTRA_chosen */
                                    ThresholdGERAN  b1_ThresholdGERAN;  /* to
                                   * choose, set choice to
                                   * b1_ThresholdGERAN_chosen */
                                    ThresholdCDMA2000 b1_ThresholdCDMA2000;  
                                        /* to choose, set choice to
                                         * b1_ThresholdCDMA2000_chosen */
                                } u;
                            } b1_Threshold;
                        } eventB1;  /* to choose, set choice to
                                     * eventB1_chosen */
                        struct _seq99 {
                            ThresholdEUTRA  b2_Threshold1;
                            struct {
                                unsigned short  choice;
#                                   define      b2_Threshold2UTRA_chosen 1
#                                   define      b2_Threshold2GERAN_chosen 2
#                                   define      b2_Threshold2CDMA2000_chosen 3
                                union {
                                    ThresholdUTRA   b2_Threshold2UTRA;  /* to
                                   * choose, set choice to
                                   * b2_Threshold2UTRA_chosen */
                                    ThresholdGERAN  b2_Threshold2GERAN;  /* to
                                   * choose, set choice to
                                   * b2_Threshold2GERAN_chosen */
                                    ThresholdCDMA2000 b2_Threshold2CDMA2000;  
                                        /* to choose, set choice to
                                         * b2_Threshold2CDMA2000_chosen */
                                } u;
                            } b2_Threshold2;
                        } eventB2;  /* to choose, set choice to
                                     * eventB2_chosen */
                    } u;
                } eventId;
                Hysteresis      hysteresis;
                TimeToTrigger   timeToTrigger;
            } event;  /* to choose, set choice to
                       * ReportConfigInterRAT_triggerType_event_chosen */
            struct _seq101 {
                enum {
                    ReportConfigInterRAT_triggerType_periodical_purpose_reportStrongestCells = 0,
                    reportStrongestCellsForSON = 1,
                    ReportConfigInterRAT_triggerType_periodical_purpose_reportCGI = 2
                } purpose;
            } periodical;  /* to choose, set choice to
                        * ReportConfigInterRAT_triggerType_periodical_chosen */
        } u;
    } triggerType;
    unsigned short  maxReportCells;
    ReportInterval  reportInterval;
    _enum36         reportAmount;
    _enum17         si_RequestForHO_r9;  /* extension #1; optional; set in
                                   * bit_mask
                           * ReportConfigInterRAT_si_RequestForHO_r9_present if
                           * present */
                                         /* Cond reportCGI */
    enum {
        reportQuantityUTRA_FDD_r10_both = 0
    } reportQuantityUTRA_FDD_r10;  /* extension #2; optional; set in bit_mask
                                    * reportQuantityUTRA_FDD_r10_present if
                                    * present */
                                   /* Need OR */
    ossBoolean      includeLocationInfo_r11;  /* extension #3; optional; set in
                                   * bit_mask includeLocationInfo_r11_present if
                                   * present */
                                              /* Need ON */
    struct {
        unsigned short  choice;
#           define      b2_Threshold1_v1250_release_chosen 1
#           define      b2_Threshold1_v1250_setup_chosen 2
        union {
            Nulltype        release;  /* to choose, set choice to
                                       * b2_Threshold1_v1250_release_chosen */
            RSRQ_Range_v1250 setup;  /* to choose, set choice to
                                      * b2_Threshold1_v1250_setup_chosen */
        } u;
    } b2_Threshold1_v1250;  /* extension #4; optional; set in bit_mask
                             * b2_Threshold1_v1250_present if present */
                            /* Need ON */
} ReportConfigInterRAT;

typedef struct ReportConfigToAddMod {
    ReportConfigId  reportConfigId;
    struct {
        unsigned short  choice;
#           define      reportConfigEUTRA_chosen 1
#           define      reportConfigInterRAT_chosen 2
        union {
            ReportConfigEUTRA reportConfigEUTRA;  /* to choose, set choice to
                                                  * reportConfigEUTRA_chosen */
            ReportConfigInterRAT reportConfigInterRAT;  /* to choose, set choice
                                            * to reportConfigInterRAT_chosen */
        } u;
    } reportConfig;
} ReportConfigToAddMod;

typedef struct ReportConfigToAddModList {
    struct ReportConfigToAddModList *next;
    ReportConfigToAddMod value;
} *ReportConfigToAddModList;

typedef unsigned short  RSRP_RangeSL_r12;

typedef unsigned short  RSRP_RangeSL3_r12;

typedef struct CellGlobalIdList_r10 {
    struct CellGlobalIdList_r10 *next;
    CellGlobalIdEUTRA value;
} *CellGlobalIdList_r10;

typedef struct TrackingAreaCodeList_r10 {
    struct TrackingAreaCodeList_r10 *next;
    TrackingAreaCode value;
} *TrackingAreaCodeList_r10;

typedef struct UE_CapabilityRAT_Container {
    RAT_Type        rat_Type;
    _octet1         ueCapabilityRAT_Container;
} UE_CapabilityRAT_Container;

typedef struct UE_CapabilityRAT_ContainerList {
    struct UE_CapabilityRAT_ContainerList *next;
    UE_CapabilityRAT_Container value;
} *UE_CapabilityRAT_ContainerList;

typedef enum AccessStratumRelease {
    rel8 = 0,
    AccessStratumRelease_rel9 = 1,
    AccessStratumRelease_rel10 = 2,
    AccessStratumRelease_rel11 = 3,
    AccessStratumRelease_rel12 = 4,
    AccessStratumRelease_spare3 = 5,
    AccessStratumRelease_spare2 = 6,
    AccessStratumRelease_spare1 = 7
} AccessStratumRelease;

typedef struct PDCP_Parameters {
    unsigned char   bit_mask;
#       define      maxNumberROHC_ContextSessions_present 0x80
    _seq79          supportedROHC_Profiles;
    enum _enum37 {
        maxNumberROHC_ContextSessions_cs2 = 0,
        maxNumberROHC_ContextSessions_cs4 = 1,
        cs8 = 2,
        cs12 = 3,
        cs16 = 4,
        cs24 = 5,
        cs32 = 6,
        cs48 = 7,
        cs64 = 8,
        cs128 = 9,
        cs256 = 10,
        cs512 = 11,
        cs1024 = 12,
        cs16384 = 13,
        maxNumberROHC_ContextSessions_spare2 = 14,
        maxNumberROHC_ContextSessions_spare1 = 15
    } maxNumberROHC_ContextSessions;  /* maxNumberROHC_ContextSessions_present
                                   * not set in bit_mask implies value is
                                   * cs16 */
} PDCP_Parameters;

typedef struct PhyLayerParameters {
    ossBoolean      ue_TxAntennaSelectionSupported;
    ossBoolean      ue_SpecificRefSigsSupported;
} PhyLayerParameters;

typedef struct RF_Parameters {
    struct SupportedBandListEUTRA *supportedBandListEUTRA;
} RF_Parameters;

typedef struct MeasParameters {
    struct BandListEUTRA *bandListEUTRA;
} MeasParameters;

typedef struct IRAT_ParametersUTRA_FDD {
    struct SupportedBandListUTRA_FDD *supportedBandListUTRA_FDD;
} IRAT_ParametersUTRA_FDD;

typedef struct IRAT_ParametersUTRA_TDD128 {
    struct SupportedBandListUTRA_TDD128 *supportedBandListUTRA_TDD128;
} IRAT_ParametersUTRA_TDD128;

typedef struct IRAT_ParametersUTRA_TDD384 {
    struct SupportedBandListUTRA_TDD384 *supportedBandListUTRA_TDD384;
} IRAT_ParametersUTRA_TDD384;

typedef struct IRAT_ParametersUTRA_TDD768 {
    struct SupportedBandListUTRA_TDD768 *supportedBandListUTRA_TDD768;
} IRAT_ParametersUTRA_TDD768;

typedef struct IRAT_ParametersGERAN {
    struct SupportedBandListGERAN *supportedBandListGERAN;
    ossBoolean      interRAT_PS_HO_ToGERAN;
} IRAT_ParametersGERAN;

typedef enum _enum38 {
    single = 0,
    dual = 1
} _enum38;

typedef struct IRAT_ParametersCDMA2000_HRPD {
    struct SupportedBandListHRPD *supportedBandListHRPD;
    _enum38         tx_ConfigHRPD;
    _enum38         rx_ConfigHRPD;
} IRAT_ParametersCDMA2000_HRPD;

typedef struct IRAT_ParametersCDMA2000_1XRTT {
    struct SupportedBandList1XRTT *supportedBandList1XRTT;
    _enum38         tx_Config1XRTT;
    _enum38         rx_Config1XRTT;
} IRAT_ParametersCDMA2000_1XRTT;

typedef enum _enum39 {
    supported = 0
} _enum39;

typedef struct PhyLayerParameters_v920 {
    unsigned char   bit_mask;
#       define      enhancedDualLayerFDD_r9_present 0x80
#       define      enhancedDualLayerTDD_r9_present 0x40
    _enum39         enhancedDualLayerFDD_r9;  /* optional; set in bit_mask
                                               * enhancedDualLayerFDD_r9_present
                                               * if present */
    _enum39         enhancedDualLayerTDD_r9;  /* optional; set in bit_mask
                                               * enhancedDualLayerTDD_r9_present
                                               * if present */
} PhyLayerParameters_v920;

typedef struct IRAT_ParametersGERAN_v920 {
    unsigned char   bit_mask;
#       define      dtm_r9_present 0x80
#       define      e_RedirectionGERAN_r9_present 0x40
    _enum39         dtm_r9;  /* optional; set in bit_mask dtm_r9_present if
                              * present */
    _enum39         e_RedirectionGERAN_r9;  /* optional; set in bit_mask
                                             * e_RedirectionGERAN_r9_present if
                                             * present */
} IRAT_ParametersGERAN_v920;

typedef struct IRAT_ParametersUTRA_v920 {
    _enum39         e_RedirectionUTRA_r9;
} IRAT_ParametersUTRA_v920;

typedef struct IRAT_ParametersCDMA2000_1XRTT_v920 {
    unsigned char   bit_mask;
#       define      e_CSFB_ConcPS_Mob1XRTT_r9_present 0x80
    _enum39         e_CSFB_1XRTT_r9;
    _enum39         e_CSFB_ConcPS_Mob1XRTT_r9;  /* optional; set in bit_mask
                                         * e_CSFB_ConcPS_Mob1XRTT_r9_present if
                                         * present */
} IRAT_ParametersCDMA2000_1XRTT_v920;

typedef struct CSG_ProximityIndicationParameters_r9 {
    unsigned char   bit_mask;
#       define      intraFreqProximityIndication_r9_present 0x80
#       define      interFreqProximityIndication_r9_present 0x40
#       define      utran_ProximityIndication_r9_present 0x20
    _enum39         intraFreqProximityIndication_r9;  /* optional; set in
                                   * bit_mask
                                   * intraFreqProximityIndication_r9_present if
                                   * present */
    _enum39         interFreqProximityIndication_r9;  /* optional; set in
                                   * bit_mask
                                   * interFreqProximityIndication_r9_present if
                                   * present */
    _enum39         utran_ProximityIndication_r9;  /* optional; set in bit_mask
                                      * utran_ProximityIndication_r9_present if
                                      * present */
} CSG_ProximityIndicationParameters_r9;

typedef struct NeighCellSI_AcquisitionParameters_r9 {
    unsigned char   bit_mask;
#       define      intraFreqSI_AcquisitionForHO_r9_present 0x80
#       define      interFreqSI_AcquisitionForHO_r9_present 0x40
#       define      utran_SI_AcquisitionForHO_r9_present 0x20
    _enum39         intraFreqSI_AcquisitionForHO_r9;  /* optional; set in
                                   * bit_mask
                                   * intraFreqSI_AcquisitionForHO_r9_present if
                                   * present */
    _enum39         interFreqSI_AcquisitionForHO_r9;  /* optional; set in
                                   * bit_mask
                                   * interFreqSI_AcquisitionForHO_r9_present if
                                   * present */
    _enum39         utran_SI_AcquisitionForHO_r9;  /* optional; set in bit_mask
                                      * utran_SI_AcquisitionForHO_r9_present if
                                      * present */
} NeighCellSI_AcquisitionParameters_r9;

typedef struct SON_Parameters_r9 {
    unsigned char   bit_mask;
#       define      SON_Parameters_r9_rach_Report_r9_present 0x80
    _enum39         rach_Report_r9;  /* optional; set in bit_mask
                                      * SON_Parameters_r9_rach_Report_r9_present
                                      * if present */
} SON_Parameters_r9;

typedef struct UE_EUTRA_CapabilityAddXDD_Mode_r9 {
    unsigned char   bit_mask;
#       define      phyLayerParameters_r9_present 0x80
#       define      featureGroupIndicators_r9_present 0x40
#       define      UE_EUTRA_CapabilityAddXDD_Mode_r9_featureGroupIndRel9Add_r9_present 0x20
#       define      interRAT_ParametersGERAN_r9_present 0x10
#       define      interRAT_ParametersUTRA_r9_present 0x08
#       define      interRAT_ParametersCDMA2000_r9_present 0x04
#       define      neighCellSI_AcquisitionParameters_r9_present 0x02
    PhyLayerParameters phyLayerParameters_r9;  /* optional; set in bit_mask
                                                * phyLayerParameters_r9_present
                                                * if present */
    _bit1           featureGroupIndicators_r9;  /* optional; set in bit_mask
                                         * featureGroupIndicators_r9_present if
                                         * present */
    _bit1           featureGroupIndRel9Add_r9;  /* optional; set in bit_mask
       * UE_EUTRA_CapabilityAddXDD_Mode_r9_featureGroupIndRel9Add_r9_present if
       * present */
    IRAT_ParametersGERAN interRAT_ParametersGERAN_r9;  /* optional; set in
                                   * bit_mask
                                   * interRAT_ParametersGERAN_r9_present if
                                   * present */
    IRAT_ParametersUTRA_v920 interRAT_ParametersUTRA_r9;  /* optional; set in
                                   * bit_mask interRAT_ParametersUTRA_r9_present
                                   * if present */
    IRAT_ParametersCDMA2000_1XRTT_v920 interRAT_ParametersCDMA2000_r9;  
                                  /* optional; set in bit_mask
                                   * interRAT_ParametersCDMA2000_r9_present if
                                   * present */
    NeighCellSI_AcquisitionParameters_r9 neighCellSI_AcquisitionParameters_r9;                                          /* optional; set in bit_mask
                              * neighCellSI_AcquisitionParameters_r9_present if
                              * present */
} UE_EUTRA_CapabilityAddXDD_Mode_r9;

typedef struct IRAT_ParametersUTRA_v9c0 {
    unsigned char   bit_mask;
#       define      voiceOverPS_HS_UTRA_FDD_r9_present 0x80
#       define      voiceOverPS_HS_UTRA_TDD128_r9_present 0x40
#       define      srvcc_FromUTRA_FDD_ToUTRA_FDD_r9_present 0x20
#       define      srvcc_FromUTRA_FDD_ToGERAN_r9_present 0x10
#       define      srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9_present 0x08
#       define      srvcc_FromUTRA_TDD128_ToGERAN_r9_present 0x04
    _enum39         voiceOverPS_HS_UTRA_FDD_r9;  /* optional; set in bit_mask
                                        * voiceOverPS_HS_UTRA_FDD_r9_present if
                                        * present */
    _enum39         voiceOverPS_HS_UTRA_TDD128_r9;  /* optional; set in bit_mask
                                     * voiceOverPS_HS_UTRA_TDD128_r9_present if
                                     * present */
    _enum39         srvcc_FromUTRA_FDD_ToUTRA_FDD_r9;  /* optional; set in
                                   * bit_mask
                                   * srvcc_FromUTRA_FDD_ToUTRA_FDD_r9_present if
                                   * present */
    _enum39         srvcc_FromUTRA_FDD_ToGERAN_r9;  /* optional; set in bit_mask
                                     * srvcc_FromUTRA_FDD_ToGERAN_r9_present if
                                     * present */
    _enum39         srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9;  /* optional; set in
                                   * bit_mask
                            * srvcc_FromUTRA_TDD128_ToUTRA_TDD128_r9_present if
                            * present */
    _enum39         srvcc_FromUTRA_TDD128_ToGERAN_r9;  /* optional; set in
                                   * bit_mask
                                   * srvcc_FromUTRA_TDD128_ToGERAN_r9_present if
                                   * present */
} IRAT_ParametersUTRA_v9c0;

typedef struct PhyLayerParameters_v9d0 {
    unsigned char   bit_mask;
#       define      tm5_FDD_r9_present 0x80
#       define      tm5_TDD_r9_present 0x40
    _enum39         tm5_FDD_r9;  /* optional; set in bit_mask tm5_FDD_r9_present
                                  * if present */
    _enum39         tm5_TDD_r9;  /* optional; set in bit_mask tm5_TDD_r9_present
                                  * if present */
} PhyLayerParameters_v9d0;

typedef struct RF_Parameters_v9e0 {
    unsigned char   bit_mask;
#       define      supportedBandListEUTRA_v9e0_present 0x80
    struct SupportedBandListEUTRA_v9e0 *supportedBandListEUTRA_v9e0;  
                                  /* optional; set in bit_mask
                                   * supportedBandListEUTRA_v9e0_present if
                                   * present */
} RF_Parameters_v9e0;

typedef struct IRAT_ParametersUTRA_v9h0 {
    _enum39         mfbi_UTRA_r9;
} IRAT_ParametersUTRA_v9h0;

typedef struct OTDOA_PositioningCapabilities_r10 {
    unsigned char   bit_mask;
#       define      interFreqRSTD_Measurement_r10_present 0x80
    _enum39         otdoa_UE_Assisted_r10;
    _enum39         interFreqRSTD_Measurement_r10;  /* optional; set in bit_mask
                                     * interFreqRSTD_Measurement_r10_present if
                                     * present */
} OTDOA_PositioningCapabilities_r10;

typedef struct RF_Parameters_v10f0 {
    unsigned char   bit_mask;
#       define      modifiedMPR_Behavior_r10_present 0x80
    _bit1           modifiedMPR_Behavior_r10;  /* optional; set in bit_mask
                                          * modifiedMPR_Behavior_r10_present if
                                          * present */
} RF_Parameters_v10f0;

typedef struct RF_Parameters_v10i0 {
    unsigned char   bit_mask;
#       define      supportedBandCombination_v10i0_present 0x80
    struct SupportedBandCombination_v10i0 *supportedBandCombination_v10i0;  
                                        /* optional; set in bit_mask
                                    * supportedBandCombination_v10i0_present if
                                    * present */
} RF_Parameters_v10i0;

typedef struct RF_Parameters_v11d0 {
    unsigned char   bit_mask;
#       define      supportedBandCombinationAdd_v11d0_present 0x80
    struct SupportedBandCombinationAdd_v11d0 *supportedBandCombinationAdd_v11d0;                                        /* optional; set in bit_mask
                                 * supportedBandCombinationAdd_v11d0_present if
                                 * present */
} RF_Parameters_v11d0;

typedef struct Other_Parameters_v11d0 {
    unsigned char   bit_mask;
#       define      inDeviceCoexInd_UL_CA_r11_present 0x80
    _enum39         inDeviceCoexInd_UL_CA_r11;  /* optional; set in bit_mask
                                         * inDeviceCoexInd_UL_CA_r11_present if
                                         * present */
} Other_Parameters_v11d0;

typedef struct UE_EUTRA_Capability_v11d0_IEs {
    unsigned char   bit_mask;
#       define      rf_Parameters_v11d0_present 0x80
#       define      otherParameters_v11d0_present 0x40
#       define      UE_EUTRA_Capability_v11d0_IEs_nonCriticalExtension_present 0x20
    RF_Parameters_v11d0 rf_Parameters_v11d0;  /* optional; set in bit_mask
                                               * rf_Parameters_v11d0_present if
                                               * present */
    Other_Parameters_v11d0 otherParameters_v11d0;  /* optional; set in bit_mask
                                             * otherParameters_v11d0_present if
                                             * present */
	/* Following field is only to be used for late REL-11 extensions */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * UE_EUTRA_Capability_v11d0_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v11d0_IEs;

typedef struct UE_EUTRA_Capability_v10i0_IEs {
    unsigned char   bit_mask;
#       define      rf_Parameters_v10i0_present 0x80
#       define      UE_EUTRA_Capability_v10i0_IEs_lateNonCriticalExtension_present 0x40
#       define      UE_EUTRA_Capability_v10i0_IEs_nonCriticalExtension_present 0x20
    RF_Parameters_v10i0 rf_Parameters_v10i0;  /* optional; set in bit_mask
                                               * rf_Parameters_v10i0_present if
                                               * present */
	/* Following field is only to be used for late REL-10 extensions */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
            * UE_EUTRA_Capability_v10i0_IEs_lateNonCriticalExtension_present if
            * present */
    UE_EUTRA_Capability_v11d0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v10i0_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v10i0_IEs;

typedef struct UE_EUTRA_Capability_v10f0_IEs {
    unsigned char   bit_mask;
#       define      rf_Parameters_v10f0_present 0x80
#       define      UE_EUTRA_Capability_v10f0_IEs_nonCriticalExtension_present 0x40
    RF_Parameters_v10f0 rf_Parameters_v10f0;  /* optional; set in bit_mask
                                               * rf_Parameters_v10f0_present if
                                               * present */
    UE_EUTRA_Capability_v10i0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v10f0_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v10f0_IEs;

typedef struct UE_EUTRA_Capability_v10c0_IEs {
    unsigned char   bit_mask;
#       define      UE_EUTRA_Capability_v10c0_IEs_otdoa_PositioningCapabilities_r10_present 0x80
#       define      UE_EUTRA_Capability_v10c0_IEs_nonCriticalExtension_present 0x40
    OTDOA_PositioningCapabilities_r10 otdoa_PositioningCapabilities_r10;  
                                        /* optional; set in bit_mask
   * UE_EUTRA_Capability_v10c0_IEs_otdoa_PositioningCapabilities_r10_present if
   * present */
    UE_EUTRA_Capability_v10f0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v10c0_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v10c0_IEs;

typedef struct UE_EUTRA_Capability_v9h0_IEs {
    unsigned char   bit_mask;
#       define      interRAT_ParametersUTRA_v9h0_present 0x80
#       define      UE_EUTRA_Capability_v9h0_IEs_lateNonCriticalExtension_present 0x40
#       define      UE_EUTRA_Capability_v9h0_IEs_nonCriticalExtension_present 0x20
    IRAT_ParametersUTRA_v9h0 interRAT_ParametersUTRA_v9h0;  /* optional; set in
                                   * bit_mask
                                   * interRAT_ParametersUTRA_v9h0_present if
                                   * present */
	/* Following field is only to be used for late REL-9 extensions */
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
             * UE_EUTRA_Capability_v9h0_IEs_lateNonCriticalExtension_present if
             * present */
    UE_EUTRA_Capability_v10c0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UE_EUTRA_Capability_v9h0_IEs_nonCriticalExtension_present if
                 * present */
} UE_EUTRA_Capability_v9h0_IEs;

typedef struct UE_EUTRA_Capability_v9e0_IEs {
    unsigned char   bit_mask;
#       define      rf_Parameters_v9e0_present 0x80
#       define      UE_EUTRA_Capability_v9e0_IEs_nonCriticalExtension_present 0x40
    RF_Parameters_v9e0 rf_Parameters_v9e0;  /* optional; set in bit_mask
                                             * rf_Parameters_v9e0_present if
                                             * present */
    UE_EUTRA_Capability_v9h0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UE_EUTRA_Capability_v9e0_IEs_nonCriticalExtension_present if
                 * present */
} UE_EUTRA_Capability_v9e0_IEs;

typedef struct UE_EUTRA_Capability_v9d0_IEs {
    unsigned char   bit_mask;
#       define      phyLayerParameters_v9d0_present 0x80
#       define      UE_EUTRA_Capability_v9d0_IEs_nonCriticalExtension_present 0x40
    PhyLayerParameters_v9d0 phyLayerParameters_v9d0;  /* optional; set in
                                   * bit_mask phyLayerParameters_v9d0_present if
                                   * present */
    UE_EUTRA_Capability_v9e0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UE_EUTRA_Capability_v9d0_IEs_nonCriticalExtension_present if
                 * present */
} UE_EUTRA_Capability_v9d0_IEs;

typedef struct UE_EUTRA_Capability_v9c0_IEs {
    unsigned char   bit_mask;
#       define      interRAT_ParametersUTRA_v9c0_present 0x80
#       define      UE_EUTRA_Capability_v9c0_IEs_nonCriticalExtension_present 0x40
    IRAT_ParametersUTRA_v9c0 interRAT_ParametersUTRA_v9c0;  /* optional; set in
                                   * bit_mask
                                   * interRAT_ParametersUTRA_v9c0_present if
                                   * present */
    UE_EUTRA_Capability_v9d0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UE_EUTRA_Capability_v9c0_IEs_nonCriticalExtension_present if
                 * present */
} UE_EUTRA_Capability_v9c0_IEs;

/* Late non critical extensions */
typedef struct UE_EUTRA_Capability_v9a0_IEs {
    unsigned char   bit_mask;
#       define      UE_EUTRA_Capability_v9a0_IEs_featureGroupIndRel9Add_r9_present 0x80
#       define      fdd_Add_UE_EUTRA_Capabilities_r9_present 0x40
#       define      tdd_Add_UE_EUTRA_Capabilities_r9_present 0x20
#       define      UE_EUTRA_Capability_v9a0_IEs_nonCriticalExtension_present 0x10
    _bit1           featureGroupIndRel9Add_r9;  /* optional; set in bit_mask
            * UE_EUTRA_Capability_v9a0_IEs_featureGroupIndRel9Add_r9_present if
            * present */
    UE_EUTRA_CapabilityAddXDD_Mode_r9 fdd_Add_UE_EUTRA_Capabilities_r9;  
                                  /* optional; set in bit_mask
                                   * fdd_Add_UE_EUTRA_Capabilities_r9_present if
                                   * present */
    UE_EUTRA_CapabilityAddXDD_Mode_r9 tdd_Add_UE_EUTRA_Capabilities_r9;  
                                  /* optional; set in bit_mask
                                   * tdd_Add_UE_EUTRA_Capabilities_r9_present if
                                   * present */
    UE_EUTRA_Capability_v9c0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UE_EUTRA_Capability_v9a0_IEs_nonCriticalExtension_present if
                 * present */
} UE_EUTRA_Capability_v9a0_IEs;

typedef struct PhyLayerParameters_v1020 {
    unsigned char   bit_mask;
#       define      twoAntennaPortsForPUCCH_r10_present 0x80
#       define      tm9_With_8Tx_FDD_r10_present 0x40
#       define      pmi_Disabling_r10_present 0x20
#       define      crossCarrierScheduling_r10_present 0x10
#       define      PhyLayerParameters_v1020_simultaneousPUCCH_PUSCH_r10_present 0x08
#       define      multiClusterPUSCH_WithinCC_r10_present 0x04
#       define      nonContiguousUL_RA_WithinCC_List_r10_present 0x02
    _enum39         twoAntennaPortsForPUCCH_r10;  /* optional; set in bit_mask
                                       * twoAntennaPortsForPUCCH_r10_present if
                                       * present */
    _enum39         tm9_With_8Tx_FDD_r10;  /* optional; set in bit_mask
                                            * tm9_With_8Tx_FDD_r10_present if
                                            * present */
    _enum39         pmi_Disabling_r10;  /* optional; set in bit_mask
                                         * pmi_Disabling_r10_present if
                                         * present */
    _enum39         crossCarrierScheduling_r10;  /* optional; set in bit_mask
                                        * crossCarrierScheduling_r10_present if
                                        * present */
    _enum39         simultaneousPUCCH_PUSCH_r10;  /* optional; set in bit_mask
              * PhyLayerParameters_v1020_simultaneousPUCCH_PUSCH_r10_present if
              * present */
    _enum39         multiClusterPUSCH_WithinCC_r10;  /* optional; set in
                                   * bit_mask
                                   * multiClusterPUSCH_WithinCC_r10_present if
                                   * present */
    struct NonContiguousUL_RA_WithinCC_List_r10 *nonContiguousUL_RA_WithinCC_List_r10;                                  /* optional; set in bit_mask
                              * nonContiguousUL_RA_WithinCC_List_r10_present if
                              * present */
} PhyLayerParameters_v1020;

typedef struct RF_Parameters_v1020 {
    struct SupportedBandCombination_r10 *supportedBandCombination_r10;
} RF_Parameters_v1020;

typedef struct MeasParameters_v1020 {
    struct BandCombinationListEUTRA_r10 *bandCombinationListEUTRA_r10;
} MeasParameters_v1020;

typedef struct IRAT_ParametersCDMA2000_1XRTT_v1020 {
    _enum39         e_CSFB_dual_1XRTT_r10;
} IRAT_ParametersCDMA2000_1XRTT_v1020;

typedef struct UE_BasedNetwPerfMeasParameters_r10 {
    unsigned char   bit_mask;
#       define      loggedMeasurementsIdle_r10_present 0x80
#       define      standaloneGNSS_Location_r10_present 0x40
    _enum39         loggedMeasurementsIdle_r10;  /* optional; set in bit_mask
                                        * loggedMeasurementsIdle_r10_present if
                                        * present */
    _enum39         standaloneGNSS_Location_r10;  /* optional; set in bit_mask
                                       * standaloneGNSS_Location_r10_present if
                                       * present */
} UE_BasedNetwPerfMeasParameters_r10;

typedef struct IRAT_ParametersUTRA_TDD_v1020 {
    _enum39         e_RedirectionUTRA_TDD_r10;
} IRAT_ParametersUTRA_TDD_v1020;

typedef struct UE_EUTRA_CapabilityAddXDD_Mode_v1060 {
    unsigned char   bit_mask;
#       define      phyLayerParameters_v1060_present 0x80
#       define      featureGroupIndRel10_v1060_present 0x40
#       define      interRAT_ParametersCDMA2000_v1060_present 0x20
#       define      interRAT_ParametersUTRA_TDD_v1060_present 0x10
#       define      UE_EUTRA_CapabilityAddXDD_Mode_v1060_otdoa_PositioningCapabilities_r10_present 0x08
    PhyLayerParameters_v1020 phyLayerParameters_v1060;  /* optional; set in
                                   * bit_mask phyLayerParameters_v1060_present
                                   * if present */
    _bit1           featureGroupIndRel10_v1060;  /* optional; set in bit_mask
                                        * featureGroupIndRel10_v1060_present if
                                        * present */
    IRAT_ParametersCDMA2000_1XRTT_v1020 interRAT_ParametersCDMA2000_v1060;  
                                        /* optional; set in bit_mask
                                 * interRAT_ParametersCDMA2000_v1060_present if
                                 * present */
    IRAT_ParametersUTRA_TDD_v1020 interRAT_ParametersUTRA_TDD_v1060;  
                                  /* optional; set in bit_mask
                                   * interRAT_ParametersUTRA_TDD_v1060_present
                                   * if present */
    OTDOA_PositioningCapabilities_r10 otdoa_PositioningCapabilities_r10;  
                                        /* extension #1; optional; set in
                                   * bit_mask
      * UE_EUTRA_CapabilityAddXDD_Mode_v1060_otdoa_PositioningCapabilities_r10_present if
      * present */
} UE_EUTRA_CapabilityAddXDD_Mode_v1060;

typedef struct RF_Parameters_v1060 {
    struct SupportedBandCombinationExt_r10 *supportedBandCombinationExt_r10;
} RF_Parameters_v1060;

typedef struct RF_Parameters_v1090 {
    unsigned char   bit_mask;
#       define      supportedBandCombination_v1090_present 0x80
    struct SupportedBandCombination_v1090 *supportedBandCombination_v1090;  
                                        /* optional; set in bit_mask
                                    * supportedBandCombination_v1090_present if
                                    * present */
} RF_Parameters_v1090;

typedef struct PDCP_Parameters_v1130 {
    unsigned char   bit_mask;
#       define      pdcp_SN_Extension_r11_present 0x80
#       define      supportRohcContextContinue_r11_present 0x40
    _enum39         pdcp_SN_Extension_r11;  /* optional; set in bit_mask
                                             * pdcp_SN_Extension_r11_present if
                                             * present */
    _enum39         supportRohcContextContinue_r11;  /* optional; set in
                                   * bit_mask
                                   * supportRohcContextContinue_r11_present if
                                   * present */
} PDCP_Parameters_v1130;

typedef struct PhyLayerParameters_v1130 {
    unsigned char   bit_mask;
#       define      crs_InterfHandl_r11_present 0x80
#       define      ePDCCH_r11_present 0x40
#       define      multiACK_CSI_Reporting_r11_present 0x20
#       define      ss_CCH_InterfHandl_r11_present 0x10
#       define      tdd_SpecialSubframe_r11_present 0x08
#       define      txDiv_PUCCH1b_ChSelect_r11_present 0x04
#       define      ul_CoMP_r11_present 0x02
    _enum39         crs_InterfHandl_r11;  /* optional; set in bit_mask
                                           * crs_InterfHandl_r11_present if
                                           * present */
    _enum39         ePDCCH_r11;  /* optional; set in bit_mask ePDCCH_r11_present
                                  * if present */
    _enum39         multiACK_CSI_Reporting_r11;  /* optional; set in bit_mask
                                        * multiACK_CSI_Reporting_r11_present if
                                        * present */
    _enum39         ss_CCH_InterfHandl_r11;  /* optional; set in bit_mask
                                              * ss_CCH_InterfHandl_r11_present
                                              * if present */
    _enum39         tdd_SpecialSubframe_r11;  /* optional; set in bit_mask
                                               * tdd_SpecialSubframe_r11_present
                                               * if present */
    _enum39         txDiv_PUCCH1b_ChSelect_r11;  /* optional; set in bit_mask
                                        * txDiv_PUCCH1b_ChSelect_r11_present if
                                        * present */
    _enum39         ul_CoMP_r11;  /* optional; set in bit_mask
                                   * ul_CoMP_r11_present if present */
} PhyLayerParameters_v1130;

typedef struct RF_Parameters_v1130 {
    unsigned char   bit_mask;
#       define      supportedBandCombination_v1130_present 0x80
    struct SupportedBandCombination_v1130 *supportedBandCombination_v1130;  
                                        /* optional; set in bit_mask
                                    * supportedBandCombination_v1130_present if
                                    * present */
} RF_Parameters_v1130;

typedef struct MeasParameters_v1130 {
    unsigned char   bit_mask;
#       define      rsrqMeasWideband_r11_present 0x80
    _enum39         rsrqMeasWideband_r11;  /* optional; set in bit_mask
                                            * rsrqMeasWideband_r11_present if
                                            * present */
} MeasParameters_v1130;

typedef struct IRAT_ParametersCDMA2000_v1130 {
    unsigned char   bit_mask;
#       define      cdma2000_NW_Sharing_r11_present 0x80
    _enum39         cdma2000_NW_Sharing_r11;  /* optional; set in bit_mask
                                               * cdma2000_NW_Sharing_r11_present
                                               * if present */
} IRAT_ParametersCDMA2000_v1130;

typedef struct Other_Parameters_r11 {
    unsigned char   bit_mask;
#       define      inDeviceCoexInd_r11_present 0x80
#       define      powerPrefInd_r11_present 0x40
#       define      ue_Rx_TxTimeDiffMeasurements_r11_present 0x20
    _enum39         inDeviceCoexInd_r11;  /* optional; set in bit_mask
                                           * inDeviceCoexInd_r11_present if
                                           * present */
    _enum39         powerPrefInd_r11;  /* optional; set in bit_mask
                                        * powerPrefInd_r11_present if present */
    _enum39         ue_Rx_TxTimeDiffMeasurements_r11;  /* optional; set in
                                   * bit_mask
                                   * ue_Rx_TxTimeDiffMeasurements_r11_present if
                                   * present */
} Other_Parameters_r11;

typedef struct UE_EUTRA_CapabilityAddXDD_Mode_v1130 {
    unsigned char   bit_mask;
#       define      UE_EUTRA_CapabilityAddXDD_Mode_v1130_phyLayerParameters_v1130_present 0x80
#       define      measParameters_v1130_present 0x40
#       define      otherParameters_r11_present 0x20
    PhyLayerParameters_v1130 phyLayerParameters_v1130;  /* optional; set in
                                   * bit_mask
     * UE_EUTRA_CapabilityAddXDD_Mode_v1130_phyLayerParameters_v1130_present if
     * present */
    MeasParameters_v1130 measParameters_v1130;  /* optional; set in bit_mask
                                                 * measParameters_v1130_present
                                                 * if present */
    Other_Parameters_r11 otherParameters_r11;  /* optional; set in bit_mask
                                                * otherParameters_r11_present if
                                                * present */
} UE_EUTRA_CapabilityAddXDD_Mode_v1130;

typedef struct PhyLayerParameters_v1170 {
    unsigned char   bit_mask;
#       define      interBandTDD_CA_WithDifferentConfig_r11_present 0x80
    _bit1           interBandTDD_CA_WithDifferentConfig_r11;  /* optional; set
                                   * in bit_mask
                           * interBandTDD_CA_WithDifferentConfig_r11_present if
                           * present */
} PhyLayerParameters_v1170;

typedef struct RF_Parameters_v1180 {
    unsigned char   bit_mask;
#       define      freqBandRetrieval_r11_present 0x80
#       define      requestedBands_r11_present 0x40
#       define      supportedBandCombinationAdd_r11_present 0x20
    _enum39         freqBandRetrieval_r11;  /* optional; set in bit_mask
                                             * freqBandRetrieval_r11_present if
                                             * present */
    struct _seqof16 *requestedBands_r11;  /* optional; set in bit_mask
                                           * requestedBands_r11_present if
                                           * present */
    struct SupportedBandCombinationAdd_r11 *supportedBandCombinationAdd_r11;  
                                        /* optional; set in bit_mask
                                   * supportedBandCombinationAdd_r11_present if
                                   * present */
} RF_Parameters_v1180;

typedef struct MBMS_Parameters_r11 {
    unsigned char   bit_mask;
#       define      mbms_SCell_r11_present 0x80
#       define      mbms_NonServingCell_r11_present 0x40
    _enum39         mbms_SCell_r11;  /* optional; set in bit_mask
                                      * mbms_SCell_r11_present if present */
    _enum39         mbms_NonServingCell_r11;  /* optional; set in bit_mask
                                               * mbms_NonServingCell_r11_present
                                               * if present */
} MBMS_Parameters_r11;

typedef struct UE_EUTRA_CapabilityAddXDD_Mode_v1180 {
    MBMS_Parameters_r11 mbms_Parameters_r11;
} UE_EUTRA_CapabilityAddXDD_Mode_v1180;

typedef struct MeasParameters_v11a0 {
    unsigned char   bit_mask;
#       define      benefitsFromInterruption_r11_present 0x80
    _enum3          benefitsFromInterruption_r11;  /* optional; set in bit_mask
                                      * benefitsFromInterruption_r11_present if
                                      * present */
} MeasParameters_v11a0;

typedef struct PhyLayerParameters_v1250 {
    unsigned short  bit_mask;
#       define      e_HARQ_Pattern_FDD_r12_present 0x8000
#       define      enhanced_4TxCodebook_r12_present 0x4000
#       define      tdd_FDD_CA_PCellDuplex_r12_present 0x2000
#       define      phy_TDD_ReConfig_TDD_PCell_r12_present 0x1000
#       define      phy_TDD_ReConfig_FDD_PCell_r12_present 0x0800
#       define      pusch_FeedbackMode_r12_present 0x0400
#       define      pusch_SRS_PowerControl_SubframeSet_r12_present 0x0200
#       define      csi_SubframeSet_r12_present 0x0100
#       define      noResourceRestrictionForTTIBundling_r12_present 0x0080
#       define      discoverySignalsInDeactSCell_r12_present 0x0040
#       define      naics_Capability_List_r12_present 0x0020
    _enum39         e_HARQ_Pattern_FDD_r12;  /* optional; set in bit_mask
                                              * e_HARQ_Pattern_FDD_r12_present
                                              * if present */
    _enum39         enhanced_4TxCodebook_r12;  /* optional; set in bit_mask
                                          * enhanced_4TxCodebook_r12_present if
                                          * present */
    _bit1           tdd_FDD_CA_PCellDuplex_r12;  /* optional; set in bit_mask
                                        * tdd_FDD_CA_PCellDuplex_r12_present if
                                        * present */
    _enum39         phy_TDD_ReConfig_TDD_PCell_r12;  /* optional; set in
                                   * bit_mask
                                   * phy_TDD_ReConfig_TDD_PCell_r12_present if
                                   * present */
    _enum39         phy_TDD_ReConfig_FDD_PCell_r12;  /* optional; set in
                                   * bit_mask
                                   * phy_TDD_ReConfig_FDD_PCell_r12_present if
                                   * present */
    _enum39         pusch_FeedbackMode_r12;  /* optional; set in bit_mask
                                              * pusch_FeedbackMode_r12_present
                                              * if present */
    _enum39         pusch_SRS_PowerControl_SubframeSet_r12;  /* optional; set in
                                   * bit_mask
                            * pusch_SRS_PowerControl_SubframeSet_r12_present if
                            * present */
    _enum39         csi_SubframeSet_r12;  /* optional; set in bit_mask
                                           * csi_SubframeSet_r12_present if
                                           * present */
    _enum39         noResourceRestrictionForTTIBundling_r12;  /* optional; set
                                   * in bit_mask
                           * noResourceRestrictionForTTIBundling_r12_present if
                           * present */
    _enum39         discoverySignalsInDeactSCell_r12;  /* optional; set in
                                   * bit_mask
                                   * discoverySignalsInDeactSCell_r12_present if
                                   * present */
    struct NAICS_Capability_List_r12 *naics_Capability_List_r12;  /* optional;
                                   * set in bit_mask
                                   * naics_Capability_List_r12_present if
                                   * present */
} PhyLayerParameters_v1250;

typedef struct RF_Parameters_v1250 {
    unsigned char   bit_mask;
#       define      supportedBandListEUTRA_v1250_present 0x80
#       define      supportedBandCombination_v1250_present 0x40
#       define      supportedBandCombinationAdd_v1250_present 0x20
#       define      freqBandPriorityAdjustment_r12_present 0x10
    struct SupportedBandListEUTRA_v1250 *supportedBandListEUTRA_v1250;  
                                  /* optional; set in bit_mask
                                   * supportedBandListEUTRA_v1250_present if
                                   * present */
    struct SupportedBandCombination_v1250 *supportedBandCombination_v1250;  
                                        /* optional; set in bit_mask
                                    * supportedBandCombination_v1250_present if
                                    * present */
    struct SupportedBandCombinationAdd_v1250 *supportedBandCombinationAdd_v1250;                                        /* optional; set in bit_mask
                                 * supportedBandCombinationAdd_v1250_present if
                                 * present */
    _enum39         freqBandPriorityAdjustment_r12;  /* optional; set in
                                   * bit_mask
                                   * freqBandPriorityAdjustment_r12_present if
                                   * present */
} RF_Parameters_v1250;

typedef struct RLC_Parameters_r12 {
    _enum39         extended_RLC_LI_Field_r12;
} RLC_Parameters_r12;

typedef struct UE_BasedNetwPerfMeasParameters_v1250 {
    _enum39         loggedMBSFNMeasurements_r12;
} UE_BasedNetwPerfMeasParameters_v1250;

typedef struct WLAN_IW_Parameters_r12 {
    unsigned char   bit_mask;
#       define      wlan_IW_RAN_Rules_r12_present 0x80
#       define      wlan_IW_ANDSF_Policies_r12_present 0x40
    _enum39         wlan_IW_RAN_Rules_r12;  /* optional; set in bit_mask
                                             * wlan_IW_RAN_Rules_r12_present if
                                             * present */
    _enum39         wlan_IW_ANDSF_Policies_r12;  /* optional; set in bit_mask
                                        * wlan_IW_ANDSF_Policies_r12_present if
                                        * present */
} WLAN_IW_Parameters_r12;

typedef struct MeasParameters_v1250 {
    unsigned short  bit_mask;
#       define      timerT312_r12_present 0x8000
#       define      MeasParameters_v1250_alternativeTimeToTrigger_r12_present 0x4000
#       define      incMonEUTRA_r12_present 0x2000
#       define      incMonUTRA_r12_present 0x1000
#       define      extendedMaxMeasId_r12_present 0x0800
#       define      extendedRSRQ_LowerRange_r12_present 0x0400
#       define      rsrq_OnAllSymbols_r12_present 0x0200
#       define      crs_DiscoverySignalsMeas_r12_present 0x0100
#       define      csi_RS_DiscoverySignalsMeas_r12_present 0x0080
    _enum39         timerT312_r12;  /* optional; set in bit_mask
                                     * timerT312_r12_present if present */
    _enum39         alternativeTimeToTrigger_r12;  /* optional; set in bit_mask
                 * MeasParameters_v1250_alternativeTimeToTrigger_r12_present if
                 * present */
    _enum39         incMonEUTRA_r12;  /* optional; set in bit_mask
                                       * incMonEUTRA_r12_present if present */
    _enum39         incMonUTRA_r12;  /* optional; set in bit_mask
                                      * incMonUTRA_r12_present if present */
    _enum39         extendedMaxMeasId_r12;  /* optional; set in bit_mask
                                             * extendedMaxMeasId_r12_present if
                                             * present */
    _enum39         extendedRSRQ_LowerRange_r12;  /* optional; set in bit_mask
                                       * extendedRSRQ_LowerRange_r12_present if
                                       * present */
    _enum39         rsrq_OnAllSymbols_r12;  /* optional; set in bit_mask
                                             * rsrq_OnAllSymbols_r12_present if
                                             * present */
    _enum39         crs_DiscoverySignalsMeas_r12;  /* optional; set in bit_mask
                                      * crs_DiscoverySignalsMeas_r12_present if
                                      * present */
    _enum39         csi_RS_DiscoverySignalsMeas_r12;  /* optional; set in
                                   * bit_mask
                                   * csi_RS_DiscoverySignalsMeas_r12_present if
                                   * present */
} MeasParameters_v1250;

typedef struct DC_Parameters_r12 {
    unsigned char   bit_mask;
#       define      drb_TypeSplit_r12_present 0x80
#       define      drb_TypeSCG_r12_present 0x40
    _enum39         drb_TypeSplit_r12;  /* optional; set in bit_mask
                                         * drb_TypeSplit_r12_present if
                                         * present */
    _enum39         drb_TypeSCG_r12;  /* optional; set in bit_mask
                                       * drb_TypeSCG_r12_present if present */
} DC_Parameters_r12;

typedef struct MBMS_Parameters_v1250 {
    unsigned char   bit_mask;
#       define      mbms_AsyncDC_r12_present 0x80
    _enum39         mbms_AsyncDC_r12;  /* optional; set in bit_mask
                                        * mbms_AsyncDC_r12_present if present */
} MBMS_Parameters_v1250;

typedef struct MAC_Parameters_r12 {
    unsigned char   bit_mask;
#       define      logicalChannelSR_ProhibitTimer_r12_present 0x80
#       define      longDRX_Command_r12_present 0x40
    _enum39         logicalChannelSR_ProhibitTimer_r12;  /* optional; set in
                                   * bit_mask
                                   * logicalChannelSR_ProhibitTimer_r12_present
                                   * if present */
    _enum39         longDRX_Command_r12;  /* optional; set in bit_mask
                                           * longDRX_Command_r12_present if
                                           * present */
} MAC_Parameters_r12;

typedef struct UE_EUTRA_CapabilityAddXDD_Mode_v1250 {
    unsigned char   bit_mask;
#       define      UE_EUTRA_CapabilityAddXDD_Mode_v1250_phyLayerParameters_v1250_present 0x80
#       define      UE_EUTRA_CapabilityAddXDD_Mode_v1250_measParameters_v1250_present 0x40
    PhyLayerParameters_v1250 phyLayerParameters_v1250;  /* optional; set in
                                   * bit_mask
     * UE_EUTRA_CapabilityAddXDD_Mode_v1250_phyLayerParameters_v1250_present if
     * present */
    MeasParameters_v1250 measParameters_v1250;  /* optional; set in bit_mask
         * UE_EUTRA_CapabilityAddXDD_Mode_v1250_measParameters_v1250_present if
         * present */
} UE_EUTRA_CapabilityAddXDD_Mode_v1250;

typedef struct SL_Parameters_r12 {
    unsigned char   bit_mask;
#       define      commSimultaneousTx_r12_present 0x80
#       define      commSupportedBands_r12_present 0x40
#       define      discSupportedBands_r12_present 0x20
#       define      discScheduledResourceAlloc_r12_present 0x10
#       define      disc_UE_SelectedResourceAlloc_r12_present 0x08
#       define      disc_SLSS_r12_present 0x04
#       define      discSupportedProc_r12_present 0x02
    _enum39         commSimultaneousTx_r12;  /* optional; set in bit_mask
                                              * commSimultaneousTx_r12_present
                                              * if present */
    struct FreqBandIndicatorListEUTRA_r12 *commSupportedBands_r12;  
                                  /* optional; set in bit_mask
                                   * commSupportedBands_r12_present if
                                   * present */
    struct SupportedBandInfoList_r12 *discSupportedBands_r12;  /* optional; set
                                   * in bit_mask discSupportedBands_r12_present
                                   * if present */
    _enum39         discScheduledResourceAlloc_r12;  /* optional; set in
                                   * bit_mask
                                   * discScheduledResourceAlloc_r12_present if
                                   * present */
    _enum39         disc_UE_SelectedResourceAlloc_r12;  /* optional; set in
                                   * bit_mask
                                   * disc_UE_SelectedResourceAlloc_r12_present
                                   * if present */
    _enum39         disc_SLSS_r12;  /* optional; set in bit_mask
                                     * disc_SLSS_r12_present if present */
    enum {
        discSupportedProc_r12_n50 = 0,
        discSupportedProc_r12_n400 = 1
    } discSupportedProc_r12;  /* optional; set in bit_mask
                               * discSupportedProc_r12_present if present */
} SL_Parameters_r12;

typedef struct RF_Parameters_v1270 {
    unsigned char   bit_mask;
#       define      supportedBandCombination_v1270_present 0x80
#       define      supportedBandCombinationAdd_v1270_present 0x40
    struct SupportedBandCombination_v1270 *supportedBandCombination_v1270;  
                                        /* optional; set in bit_mask
                                    * supportedBandCombination_v1270_present if
                                    * present */
    struct SupportedBandCombinationAdd_v1270 *supportedBandCombinationAdd_v1270;                                        /* optional; set in bit_mask
                                 * supportedBandCombinationAdd_v1270_present if
                                 * present */
} RF_Parameters_v1270;

typedef struct UE_EUTRA_Capability_v1270_IEs {
    unsigned char   bit_mask;
#       define      rf_Parameters_v1270_present 0x80
#       define      UE_EUTRA_Capability_v1270_IEs_nonCriticalExtension_present 0x40
    RF_Parameters_v1270 rf_Parameters_v1270;  /* optional; set in bit_mask
                                               * rf_Parameters_v1270_present if
                                               * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                * UE_EUTRA_Capability_v1270_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1270_IEs;

typedef struct UE_EUTRA_Capability_v1260_IEs {
    unsigned char   bit_mask;
#       define      ue_CategoryDL_v1260_present 0x80
#       define      UE_EUTRA_Capability_v1260_IEs_nonCriticalExtension_present 0x40
    unsigned short  ue_CategoryDL_v1260;  /* optional; set in bit_mask
                                           * ue_CategoryDL_v1260_present if
                                           * present */
    UE_EUTRA_Capability_v1270_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1260_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1260_IEs;

typedef struct UE_EUTRA_Capability_v1250_IEs {
    unsigned short  bit_mask;
#       define      UE_EUTRA_Capability_v1250_IEs_phyLayerParameters_v1250_present 0x8000
#       define      rf_Parameters_v1250_present 0x4000
#       define      rlc_Parameters_r12_present 0x2000
#       define      ue_BasedNetwPerfMeasParameters_v1250_present 0x1000
#       define      ue_CategoryDL_r12_present 0x0800
#       define      ue_CategoryUL_r12_present 0x0400
#       define      wlan_IW_Parameters_r12_present 0x0200
#       define      UE_EUTRA_Capability_v1250_IEs_measParameters_v1250_present 0x0100
#       define      dc_Parameters_r12_present 0x0080
#       define      mbms_Parameters_v1250_present 0x0040
#       define      mac_Parameters_r12_present 0x0020
#       define      fdd_Add_UE_EUTRA_Capabilities_v1250_present 0x0010
#       define      tdd_Add_UE_EUTRA_Capabilities_v1250_present 0x0008
#       define      sl_Parameters_r12_present 0x0004
#       define      UE_EUTRA_Capability_v1250_IEs_nonCriticalExtension_present 0x0002
    PhyLayerParameters_v1250 phyLayerParameters_v1250;  /* optional; set in
                                   * bit_mask
            * UE_EUTRA_Capability_v1250_IEs_phyLayerParameters_v1250_present if
            * present */
    RF_Parameters_v1250 rf_Parameters_v1250;  /* optional; set in bit_mask
                                               * rf_Parameters_v1250_present if
                                               * present */
    RLC_Parameters_r12 rlc_Parameters_r12;  /* optional; set in bit_mask
                                             * rlc_Parameters_r12_present if
                                             * present */
    UE_BasedNetwPerfMeasParameters_v1250 ue_BasedNetwPerfMeasParameters_v1250;                                          /* optional; set in bit_mask
                              * ue_BasedNetwPerfMeasParameters_v1250_present if
                              * present */
    unsigned short  ue_CategoryDL_r12;  /* optional; set in bit_mask
                                         * ue_CategoryDL_r12_present if
                                         * present */
    unsigned short  ue_CategoryUL_r12;  /* optional; set in bit_mask
                                         * ue_CategoryUL_r12_present if
                                         * present */
    WLAN_IW_Parameters_r12 wlan_IW_Parameters_r12;  /* optional; set in bit_mask
                                            * wlan_IW_Parameters_r12_present if
                                            * present */
    MeasParameters_v1250 measParameters_v1250;  /* optional; set in bit_mask
                * UE_EUTRA_Capability_v1250_IEs_measParameters_v1250_present if
                * present */
    DC_Parameters_r12 dc_Parameters_r12;  /* optional; set in bit_mask
                                           * dc_Parameters_r12_present if
                                           * present */
    MBMS_Parameters_v1250 mbms_Parameters_v1250;  /* optional; set in bit_mask
                                             * mbms_Parameters_v1250_present if
                                             * present */
    MAC_Parameters_r12 mac_Parameters_r12;  /* optional; set in bit_mask
                                             * mac_Parameters_r12_present if
                                             * present */
    UE_EUTRA_CapabilityAddXDD_Mode_v1250 fdd_Add_UE_EUTRA_Capabilities_v1250;                                           /* optional; set in bit_mask
                               * fdd_Add_UE_EUTRA_Capabilities_v1250_present if
                               * present */
    UE_EUTRA_CapabilityAddXDD_Mode_v1250 tdd_Add_UE_EUTRA_Capabilities_v1250;                                           /* optional; set in bit_mask
                               * tdd_Add_UE_EUTRA_Capabilities_v1250_present if
                               * present */
    SL_Parameters_r12 sl_Parameters_r12;  /* optional; set in bit_mask
                                           * sl_Parameters_r12_present if
                                           * present */
    UE_EUTRA_Capability_v1260_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1250_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1250_IEs;

typedef struct UE_EUTRA_Capability_v11a0_IEs {
    unsigned char   bit_mask;
#       define      ue_Category_v11a0_present 0x80
#       define      measParameters_v11a0_present 0x40
#       define      UE_EUTRA_Capability_v11a0_IEs_nonCriticalExtension_present 0x20
    unsigned short  ue_Category_v11a0;  /* optional; set in bit_mask
                                         * ue_Category_v11a0_present if
                                         * present */
    MeasParameters_v11a0 measParameters_v11a0;  /* optional; set in bit_mask
                                                 * measParameters_v11a0_present
                                                 * if present */
    UE_EUTRA_Capability_v1250_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v11a0_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v11a0_IEs;

typedef struct UE_EUTRA_Capability_v1180_IEs {
    unsigned char   bit_mask;
#       define      rf_Parameters_v1180_present 0x80
#       define      mbms_Parameters_r11_present 0x40
#       define      fdd_Add_UE_EUTRA_Capabilities_v1180_present 0x20
#       define      tdd_Add_UE_EUTRA_Capabilities_v1180_present 0x10
#       define      UE_EUTRA_Capability_v1180_IEs_nonCriticalExtension_present 0x08
    RF_Parameters_v1180 rf_Parameters_v1180;  /* optional; set in bit_mask
                                               * rf_Parameters_v1180_present if
                                               * present */
    MBMS_Parameters_r11 mbms_Parameters_r11;  /* optional; set in bit_mask
                                               * mbms_Parameters_r11_present if
                                               * present */
    UE_EUTRA_CapabilityAddXDD_Mode_v1180 fdd_Add_UE_EUTRA_Capabilities_v1180;                                           /* optional; set in bit_mask
                               * fdd_Add_UE_EUTRA_Capabilities_v1180_present if
                               * present */
    UE_EUTRA_CapabilityAddXDD_Mode_v1180 tdd_Add_UE_EUTRA_Capabilities_v1180;                                           /* optional; set in bit_mask
                               * tdd_Add_UE_EUTRA_Capabilities_v1180_present if
                               * present */
    UE_EUTRA_Capability_v11a0_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1180_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1180_IEs;

typedef struct UE_EUTRA_Capability_v1170_IEs {
    unsigned char   bit_mask;
#       define      phyLayerParameters_v1170_present 0x80
#       define      ue_Category_v1170_present 0x40
#       define      UE_EUTRA_Capability_v1170_IEs_nonCriticalExtension_present 0x20
    PhyLayerParameters_v1170 phyLayerParameters_v1170;  /* optional; set in
                                   * bit_mask phyLayerParameters_v1170_present
                                   * if present */
    unsigned short  ue_Category_v1170;  /* optional; set in bit_mask
                                         * ue_Category_v1170_present if
                                         * present */
    UE_EUTRA_Capability_v1180_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1170_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1170_IEs;

typedef struct UE_EUTRA_Capability_v1130_IEs {
    unsigned char   bit_mask;
#       define      UE_EUTRA_Capability_v1130_IEs_phyLayerParameters_v1130_present 0x80
#       define      fdd_Add_UE_EUTRA_Capabilities_v1130_present 0x40
#       define      tdd_Add_UE_EUTRA_Capabilities_v1130_present 0x20
#       define      UE_EUTRA_Capability_v1130_IEs_nonCriticalExtension_present 0x10
    PDCP_Parameters_v1130 pdcp_Parameters_v1130;
    PhyLayerParameters_v1130 phyLayerParameters_v1130;  /* optional; set in
                                   * bit_mask
            * UE_EUTRA_Capability_v1130_IEs_phyLayerParameters_v1130_present if
            * present */
    RF_Parameters_v1130 rf_Parameters_v1130;
    MeasParameters_v1130 measParameters_v1130;
    IRAT_ParametersCDMA2000_v1130 interRAT_ParametersCDMA2000_v1130;
    Other_Parameters_r11 otherParameters_r11;
    UE_EUTRA_CapabilityAddXDD_Mode_v1130 fdd_Add_UE_EUTRA_Capabilities_v1130;                                           /* optional; set in bit_mask
                               * fdd_Add_UE_EUTRA_Capabilities_v1130_present if
                               * present */
    UE_EUTRA_CapabilityAddXDD_Mode_v1130 tdd_Add_UE_EUTRA_Capabilities_v1130;                                           /* optional; set in bit_mask
                               * tdd_Add_UE_EUTRA_Capabilities_v1130_present if
                               * present */
    UE_EUTRA_Capability_v1170_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1130_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1130_IEs;

typedef struct UE_EUTRA_Capability_v1090_IEs {
    unsigned char   bit_mask;
#       define      rf_Parameters_v1090_present 0x80
#       define      UE_EUTRA_Capability_v1090_IEs_nonCriticalExtension_present 0x40
    RF_Parameters_v1090 rf_Parameters_v1090;  /* optional; set in bit_mask
                                               * rf_Parameters_v1090_present if
                                               * present */
    UE_EUTRA_Capability_v1130_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1090_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1090_IEs;

typedef struct UE_EUTRA_Capability_v1060_IEs {
    unsigned char   bit_mask;
#       define      fdd_Add_UE_EUTRA_Capabilities_v1060_present 0x80
#       define      tdd_Add_UE_EUTRA_Capabilities_v1060_present 0x40
#       define      rf_Parameters_v1060_present 0x20
#       define      UE_EUTRA_Capability_v1060_IEs_nonCriticalExtension_present 0x10
    UE_EUTRA_CapabilityAddXDD_Mode_v1060 fdd_Add_UE_EUTRA_Capabilities_v1060;                                           /* optional; set in bit_mask
                               * fdd_Add_UE_EUTRA_Capabilities_v1060_present if
                               * present */
    UE_EUTRA_CapabilityAddXDD_Mode_v1060 tdd_Add_UE_EUTRA_Capabilities_v1060;                                           /* optional; set in bit_mask
                               * tdd_Add_UE_EUTRA_Capabilities_v1060_present if
                               * present */
    RF_Parameters_v1060 rf_Parameters_v1060;  /* optional; set in bit_mask
                                               * rf_Parameters_v1060_present if
                                               * present */
    UE_EUTRA_Capability_v1090_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1060_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1060_IEs;

typedef struct UE_EUTRA_Capability_v1020_IEs {
    unsigned short  bit_mask;
#       define      ue_Category_v1020_present 0x8000
#       define      phyLayerParameters_v1020_present 0x4000
#       define      rf_Parameters_v1020_present 0x2000
#       define      measParameters_v1020_present 0x1000
#       define      featureGroupIndRel10_r10_present 0x0800
#       define      interRAT_ParametersCDMA2000_v1020_present 0x0400
#       define      ue_BasedNetwPerfMeasParameters_r10_present 0x0200
#       define      interRAT_ParametersUTRA_TDD_v1020_present 0x0100
#       define      UE_EUTRA_Capability_v1020_IEs_nonCriticalExtension_present 0x0080
    unsigned short  ue_Category_v1020;  /* optional; set in bit_mask
                                         * ue_Category_v1020_present if
                                         * present */
    PhyLayerParameters_v1020 phyLayerParameters_v1020;  /* optional; set in
                                   * bit_mask phyLayerParameters_v1020_present
                                   * if present */
    RF_Parameters_v1020 rf_Parameters_v1020;  /* optional; set in bit_mask
                                               * rf_Parameters_v1020_present if
                                               * present */
    MeasParameters_v1020 measParameters_v1020;  /* optional; set in bit_mask
                                                 * measParameters_v1020_present
                                                 * if present */
    _bit1           featureGroupIndRel10_r10;  /* optional; set in bit_mask
                                          * featureGroupIndRel10_r10_present if
                                          * present */
    IRAT_ParametersCDMA2000_1XRTT_v1020 interRAT_ParametersCDMA2000_v1020;  
                                        /* optional; set in bit_mask
                                 * interRAT_ParametersCDMA2000_v1020_present if
                                 * present */
    UE_BasedNetwPerfMeasParameters_r10 ue_BasedNetwPerfMeasParameters_r10;  
                                        /* optional; set in bit_mask
                                * ue_BasedNetwPerfMeasParameters_r10_present if
                                * present */
    IRAT_ParametersUTRA_TDD_v1020 interRAT_ParametersUTRA_TDD_v1020;  
                                  /* optional; set in bit_mask
                                   * interRAT_ParametersUTRA_TDD_v1020_present
                                   * if present */
    UE_EUTRA_Capability_v1060_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                * UE_EUTRA_Capability_v1020_IEs_nonCriticalExtension_present if
                * present */
} UE_EUTRA_Capability_v1020_IEs;

typedef struct UE_EUTRA_Capability_v940_IEs {
    unsigned char   bit_mask;
#       define      UE_EUTRA_Capability_v940_IEs_lateNonCriticalExtension_present 0x80
#       define      UE_EUTRA_Capability_v940_IEs_nonCriticalExtension_present 0x40
    struct {
        /* ContentsConstraint is applied to lateNonCriticalExtension */
        _octet1         encoded;
        UE_EUTRA_Capability_v9a0_IEs *decoded;
    } lateNonCriticalExtension;  /* optional; set in bit_mask
             * UE_EUTRA_Capability_v940_IEs_lateNonCriticalExtension_present if
             * present */
    UE_EUTRA_Capability_v1020_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UE_EUTRA_Capability_v940_IEs_nonCriticalExtension_present if
                 * present */
} UE_EUTRA_Capability_v940_IEs;

/* Regular non critical extensions */
typedef struct UE_EUTRA_Capability_v920_IEs {
    unsigned char   bit_mask;
#       define      interRAT_ParametersUTRA_v920_present 0x80
#       define      interRAT_ParametersCDMA2000_v920_present 0x40
#       define      deviceType_r9_present 0x20
#       define      UE_EUTRA_Capability_v920_IEs_nonCriticalExtension_present 0x10
    PhyLayerParameters_v920 phyLayerParameters_v920;
    IRAT_ParametersGERAN_v920 interRAT_ParametersGERAN_v920;
    IRAT_ParametersUTRA_v920 interRAT_ParametersUTRA_v920;  /* optional; set in
                                   * bit_mask
                                   * interRAT_ParametersUTRA_v920_present if
                                   * present */
    IRAT_ParametersCDMA2000_1XRTT_v920 interRAT_ParametersCDMA2000_v920;  
                                        /* optional; set in bit_mask
                                  * interRAT_ParametersCDMA2000_v920_present if
                                  * present */
    enum {
        noBenFromBatConsumpOpt = 0
    } deviceType_r9;  /* optional; set in bit_mask deviceType_r9_present if
                       * present */
    CSG_ProximityIndicationParameters_r9 csg_ProximityIndicationParameters_r9;
    NeighCellSI_AcquisitionParameters_r9 neighCellSI_AcquisitionParameters_r9;
    SON_Parameters_r9 son_Parameters_r9;
    UE_EUTRA_Capability_v940_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                 * UE_EUTRA_Capability_v920_IEs_nonCriticalExtension_present if
                 * present */
} UE_EUTRA_Capability_v920_IEs;

typedef struct UE_EUTRA_Capability {
    unsigned char   bit_mask;
#       define      featureGroupIndicators_present 0x80
#       define      UE_EUTRA_Capability_nonCriticalExtension_present 0x40
    AccessStratumRelease accessStratumRelease;
    unsigned short  ue_Category;
    PDCP_Parameters pdcp_Parameters;
    PhyLayerParameters phyLayerParameters;
    RF_Parameters   rf_Parameters;
    MeasParameters  measParameters;
    _bit1           featureGroupIndicators;  /* optional; set in bit_mask
                                              * featureGroupIndicators_present
                                              * if present */
    struct {
        unsigned char   bit_mask;
#           define      utraFDD_present 0x80
#           define      utraTDD128_present 0x40
#           define      utraTDD384_present 0x20
#           define      utraTDD768_present 0x10
#           define      geran_present 0x08
#           define      cdma2000_HRPD_present 0x04
#           define      cdma2000_1xRTT_present 0x02
        IRAT_ParametersUTRA_FDD utraFDD;  /* optional; set in bit_mask
                                           * utraFDD_present if present */
        IRAT_ParametersUTRA_TDD128 utraTDD128;  /* optional; set in bit_mask
                                                 * utraTDD128_present if
                                                 * present */
        IRAT_ParametersUTRA_TDD384 utraTDD384;  /* optional; set in bit_mask
                                                 * utraTDD384_present if
                                                 * present */
        IRAT_ParametersUTRA_TDD768 utraTDD768;  /* optional; set in bit_mask
                                                 * utraTDD768_present if
                                                 * present */
        IRAT_ParametersGERAN geran;  /* optional; set in bit_mask geran_present
                                      * if present */
        IRAT_ParametersCDMA2000_HRPD cdma2000_HRPD;  /* optional; set in
                                   * bit_mask cdma2000_HRPD_present if
                                   * present */
        IRAT_ParametersCDMA2000_1XRTT cdma2000_1xRTT;  /* optional; set in
                                   * bit_mask cdma2000_1xRTT_present if
                                   * present */
    } interRAT_Parameters;
    UE_EUTRA_Capability_v920_IEs nonCriticalExtension;  /* optional; set in
                                   * bit_mask
                          * UE_EUTRA_Capability_nonCriticalExtension_present if
                          * present */
} UE_EUTRA_Capability;

typedef struct NonContiguousUL_RA_WithinCC_r10 {
    unsigned char   bit_mask;
#       define      nonContiguousUL_RA_WithinCC_Info_r10_present 0x80
    _enum39         nonContiguousUL_RA_WithinCC_Info_r10;  /* optional; set in
                                   * bit_mask
                              * nonContiguousUL_RA_WithinCC_Info_r10_present if
                              * present */
} NonContiguousUL_RA_WithinCC_r10;

typedef struct NonContiguousUL_RA_WithinCC_List_r10 {
    struct NonContiguousUL_RA_WithinCC_List_r10 *next;
    NonContiguousUL_RA_WithinCC_r10 value;
} *NonContiguousUL_RA_WithinCC_List_r10;

typedef struct SupportedBandCombination_r10 {
    struct SupportedBandCombination_r10 *next;
    struct BandCombinationParameters_r10 *value;
} *SupportedBandCombination_r10;

typedef struct SupportedBandwidthCombinationSet_r10 {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} SupportedBandwidthCombinationSet_r10;

typedef struct BandCombinationParametersExt_r10 {
    unsigned char   bit_mask;
#       define      supportedBandwidthCombinationSet_r10_present 0x80
    SupportedBandwidthCombinationSet_r10 supportedBandwidthCombinationSet_r10;                                          /* optional; set in bit_mask
                              * supportedBandwidthCombinationSet_r10_present if
                              * present */
} BandCombinationParametersExt_r10;

typedef struct SupportedBandCombinationExt_r10 {
    struct SupportedBandCombinationExt_r10 *next;
    BandCombinationParametersExt_r10 value;
} *SupportedBandCombinationExt_r10;

typedef struct SupportedBandCombination_v1090 {
    struct SupportedBandCombination_v1090 *next;
    struct BandCombinationParameters_v1090 *value;
} *SupportedBandCombination_v1090;

typedef struct CA_MIMO_ParametersDL_v10i0 {
    unsigned char   bit_mask;
#       define      fourLayerTM3_TM4_r10_present 0x80
    _enum39         fourLayerTM3_TM4_r10;  /* optional; set in bit_mask
                                            * fourLayerTM3_TM4_r10_present if
                                            * present */
} CA_MIMO_ParametersDL_v10i0;

typedef struct BandParameters_v10i0 {
    struct _seqof24 {
        struct _seqof24 *next;
        CA_MIMO_ParametersDL_v10i0 value;
    } *bandParametersDL_v10i0;
} BandParameters_v10i0;

typedef struct BandCombinationParameters_v10i0 {
    unsigned char   bit_mask;
#       define      bandParameterList_v10i0_present 0x80
    struct _seqof25 {
        struct _seqof25 *next;
        BandParameters_v10i0 value;
    } *bandParameterList_v10i0;  /* optional; set in bit_mask
                                  * bandParameterList_v10i0_present if
                                  * present */
} BandCombinationParameters_v10i0;

typedef struct SupportedBandCombination_v10i0 {
    struct SupportedBandCombination_v10i0 *next;
    BandCombinationParameters_v10i0 value;
} *SupportedBandCombination_v10i0;

typedef enum _enum40 {
    supportedCSI_Proc_r11_n1 = 0,
    supportedCSI_Proc_r11_n3 = 1,
    supportedCSI_Proc_r11_n4 = 2
} _enum40;

typedef struct BandParameters_v1130 {
    _enum40         supportedCSI_Proc_r11;
} BandParameters_v1130;

typedef struct BandCombinationParameters_v1130 {
    unsigned char   bit_mask;
#       define      BandCombinationParameters_v1130_multipleTimingAdvance_r11_present 0x80
#       define      BandCombinationParameters_v1130_simultaneousRx_Tx_r11_present 0x40
#       define      bandParameterList_r11_present 0x20
    _enum39         multipleTimingAdvance_r11;  /* optional; set in bit_mask
         * BandCombinationParameters_v1130_multipleTimingAdvance_r11_present if
         * present */
    _enum39         simultaneousRx_Tx_r11;  /* optional; set in bit_mask
             * BandCombinationParameters_v1130_simultaneousRx_Tx_r11_present if
             * present */
    struct _seqof26 {
        struct _seqof26 *next;
        BandParameters_v1130 value;
    } *bandParameterList_r11;  /* optional; set in bit_mask
                                * bandParameterList_r11_present if present */
} BandCombinationParameters_v1130;

typedef struct SupportedBandCombination_v1130 {
    struct SupportedBandCombination_v1130 *next;
    BandCombinationParameters_v1130 value;
} *SupportedBandCombination_v1130;

typedef struct BandCombinationParameters_v1250 {
    unsigned char   bit_mask;
#       define      dc_Support_r12_present 0x80
#       define      supportedNAICS_2CRS_AP_r12_present 0x40
#       define      commSupportedBandsPerBC_r12_present 0x20
    struct {
        unsigned char   bit_mask;
#           define      asynchronous_r12_present 0x80
#           define      supportedCellGrouping_r12_present 0x40
        _enum39         asynchronous_r12;  /* optional; set in bit_mask
                                            * asynchronous_r12_present if
                                            * present */
        struct {
            unsigned short  choice;
#               define      threeEntries_r12_chosen 1
#               define      fourEntries_r12_chosen 2
#               define      fiveEntries_r12_chosen 3
            union {
                _bit1           threeEntries_r12;  /* to choose, set choice to
                                                   * threeEntries_r12_chosen */
                _bit1           fourEntries_r12;  /* to choose, set choice to
                                                   * fourEntries_r12_chosen */
                _bit1           fiveEntries_r12;  /* to choose, set choice to
                                                   * fiveEntries_r12_chosen */
            } u;
        } supportedCellGrouping_r12;  /* optional; set in bit_mask
                                       * supportedCellGrouping_r12_present if
                                       * present */
    } dc_Support_r12;  /* optional; set in bit_mask dc_Support_r12_present if
                        * present */
    _bit1           supportedNAICS_2CRS_AP_r12;  /* optional; set in bit_mask
                                        * supportedNAICS_2CRS_AP_r12_present if
                                        * present */
    _bit1           commSupportedBandsPerBC_r12;  /* optional; set in bit_mask
                                       * commSupportedBandsPerBC_r12_present if
                                       * present */
} BandCombinationParameters_v1250;

typedef struct SupportedBandCombination_v1250 {
    struct SupportedBandCombination_v1250 *next;
    BandCombinationParameters_v1250 value;
} *SupportedBandCombination_v1250;

typedef enum MIMO_CapabilityDL_r10 {
    MIMO_CapabilityDL_r10_twoLayers = 0,
    MIMO_CapabilityDL_r10_fourLayers = 1,
    MIMO_CapabilityDL_r10_eightLayers = 2
} MIMO_CapabilityDL_r10;

typedef struct IntraBandContiguousCC_Info_r12 {
    unsigned char   bit_mask;
#       define      fourLayerTM3_TM4_perCC_r12_present 0x80
#       define      supportedMIMO_CapabilityDL_r12_present 0x40
#       define      supportedCSI_Proc_r12_present 0x20
    _enum39         fourLayerTM3_TM4_perCC_r12;  /* optional; set in bit_mask
                                        * fourLayerTM3_TM4_perCC_r12_present if
                                        * present */
    MIMO_CapabilityDL_r10 supportedMIMO_CapabilityDL_r12;  /* optional; set in
                                   * bit_mask
                                   * supportedMIMO_CapabilityDL_r12_present if
                                   * present */
    _enum40         supportedCSI_Proc_r12;  /* optional; set in bit_mask
                                             * supportedCSI_Proc_r12_present if
                                             * present */
} IntraBandContiguousCC_Info_r12;

typedef struct CA_MIMO_ParametersDL_v1270 {
    struct _seqof27 {
        struct _seqof27 *next;
        IntraBandContiguousCC_Info_r12 value;
    } *intraBandContiguousCC_InfoList_r12;
} CA_MIMO_ParametersDL_v1270;

typedef struct BandParameters_v1270 {
    struct _seqof28 {
        struct _seqof28 *next;
        CA_MIMO_ParametersDL_v1270 value;
    } *bandParametersDL_v1270;
} BandParameters_v1270;

typedef struct BandCombinationParameters_v1270 {
    unsigned char   bit_mask;
#       define      bandParameterList_v1270_present 0x80
    struct _seqof29 {
        struct _seqof29 *next;
        BandParameters_v1270 value;
    } *bandParameterList_v1270;  /* optional; set in bit_mask
                                  * bandParameterList_v1270_present if
                                  * present */
} BandCombinationParameters_v1270;

typedef struct SupportedBandCombination_v1270 {
    struct SupportedBandCombination_v1270 *next;
    BandCombinationParameters_v1270 value;
} *SupportedBandCombination_v1270;

typedef struct BandParameters_r11 {
    unsigned char   bit_mask;
#       define      bandParametersUL_r11_present 0x80
#       define      bandParametersDL_r11_present 0x40
#       define      supportedCSI_Proc_r11_present 0x20
    FreqBandIndicator_r11 bandEUTRA_r11;
    struct BandParametersUL_r10 *bandParametersUL_r11;  /* optional; set in
                                   * bit_mask bandParametersUL_r11_present if
                                   * present */
    struct BandParametersDL_r10 *bandParametersDL_r11;  /* optional; set in
                                   * bit_mask bandParametersDL_r11_present if
                                   * present */
    _enum40         supportedCSI_Proc_r11;  /* optional; set in bit_mask
                                             * supportedCSI_Proc_r11_present if
                                             * present */
} BandParameters_r11;

typedef struct BandInfoEUTRA {
    unsigned char   bit_mask;
#       define      interRAT_BandList_present 0x80
    struct InterFreqBandList *interFreqBandList;
    struct InterRAT_BandList *interRAT_BandList;  /* optional; set in bit_mask
                                                   * interRAT_BandList_present
                                                   * if present */
} BandInfoEUTRA;

typedef struct BandCombinationParameters_r11 {
    unsigned char   bit_mask;
#       define      supportedBandwidthCombinationSet_r11_present 0x80
#       define      BandCombinationParameters_r11_multipleTimingAdvance_r11_present 0x40
#       define      BandCombinationParameters_r11_simultaneousRx_Tx_r11_present 0x20
    struct _seqof30 {
        struct _seqof30 *next;
        BandParameters_r11 value;
    } *bandParameterList_r11;
    SupportedBandwidthCombinationSet_r10 supportedBandwidthCombinationSet_r11;                                          /* optional; set in bit_mask
                              * supportedBandwidthCombinationSet_r11_present if
                              * present */
    _enum39         multipleTimingAdvance_r11;  /* optional; set in bit_mask
           * BandCombinationParameters_r11_multipleTimingAdvance_r11_present if
           * present */
    _enum39         simultaneousRx_Tx_r11;  /* optional; set in bit_mask
               * BandCombinationParameters_r11_simultaneousRx_Tx_r11_present if
               * present */
    BandInfoEUTRA   bandInfoEUTRA_r11;
} BandCombinationParameters_r11;

typedef struct SupportedBandCombinationAdd_r11 {
    struct SupportedBandCombinationAdd_r11 *next;
    BandCombinationParameters_r11 value;
} *SupportedBandCombinationAdd_r11;

typedef struct SupportedBandCombinationAdd_v11d0 {
    struct SupportedBandCombinationAdd_v11d0 *next;
    BandCombinationParameters_v10i0 value;
} *SupportedBandCombinationAdd_v11d0;

typedef struct SupportedBandCombinationAdd_v1250 {
    struct SupportedBandCombinationAdd_v1250 *next;
    BandCombinationParameters_v1250 value;
} *SupportedBandCombinationAdd_v1250;

typedef struct SupportedBandCombinationAdd_v1270 {
    struct SupportedBandCombinationAdd_v1270 *next;
    BandCombinationParameters_v1270 value;
} *SupportedBandCombinationAdd_v1270;

typedef struct BandParameters_r10 {
    unsigned char   bit_mask;
#       define      bandParametersUL_r10_present 0x80
#       define      bandParametersDL_r10_present 0x40
    FreqBandIndicator bandEUTRA_r10;
    struct BandParametersUL_r10 *bandParametersUL_r10;  /* optional; set in
                                   * bit_mask bandParametersUL_r10_present if
                                   * present */
    struct BandParametersDL_r10 *bandParametersDL_r10;  /* optional; set in
                                   * bit_mask bandParametersDL_r10_present if
                                   * present */
} BandParameters_r10;

typedef struct BandCombinationParameters_r10 {
    struct BandCombinationParameters_r10 *next;
    BandParameters_r10 value;
} *BandCombinationParameters_r10;

typedef struct BandParameters_v1090 {
    unsigned char   bit_mask;
#       define      bandEUTRA_v1090_present 0x80
    FreqBandIndicator_v9e0 bandEUTRA_v1090;  /* optional; set in bit_mask
                                              * bandEUTRA_v1090_present if
                                              * present */
} BandParameters_v1090;

typedef struct BandCombinationParameters_v1090 {
    struct BandCombinationParameters_v1090 *next;
    BandParameters_v1090 value;
} *BandCombinationParameters_v1090;

typedef enum CA_BandwidthClass_r10 {
    CA_BandwidthClass_r10_a = 0,
    CA_BandwidthClass_r10_b = 1,
    CA_BandwidthClass_r10_c = 2,
    CA_BandwidthClass_r10_d = 3,
    CA_BandwidthClass_r10_e = 4,
    CA_BandwidthClass_r10_f = 5
} CA_BandwidthClass_r10;

typedef enum MIMO_CapabilityUL_r10 {
    MIMO_CapabilityUL_r10_twoLayers = 0,
    MIMO_CapabilityUL_r10_fourLayers = 1
} MIMO_CapabilityUL_r10;

typedef struct CA_MIMO_ParametersUL_r10 {
    unsigned char   bit_mask;
#       define      supportedMIMO_CapabilityUL_r10_present 0x80
    CA_BandwidthClass_r10 ca_BandwidthClassUL_r10;
    MIMO_CapabilityUL_r10 supportedMIMO_CapabilityUL_r10;  /* optional; set in
                                   * bit_mask
                                   * supportedMIMO_CapabilityUL_r10_present if
                                   * present */
} CA_MIMO_ParametersUL_r10;

typedef struct BandParametersUL_r10 {
    struct BandParametersUL_r10 *next;
    CA_MIMO_ParametersUL_r10 value;
} *BandParametersUL_r10;

typedef struct CA_MIMO_ParametersDL_r10 {
    unsigned char   bit_mask;
#       define      supportedMIMO_CapabilityDL_r10_present 0x80
    CA_BandwidthClass_r10 ca_BandwidthClassDL_r10;
    MIMO_CapabilityDL_r10 supportedMIMO_CapabilityDL_r10;  /* optional; set in
                                   * bit_mask
                                   * supportedMIMO_CapabilityDL_r10_present if
                                   * present */
} CA_MIMO_ParametersDL_r10;

typedef struct BandParametersDL_r10 {
    struct BandParametersDL_r10 *next;
    CA_MIMO_ParametersDL_r10 value;
} *BandParametersDL_r10;

typedef struct SupportedBandEUTRA {
    FreqBandIndicator bandEUTRA;
    ossBoolean      halfDuplex;
} SupportedBandEUTRA;

typedef struct SupportedBandListEUTRA {
    struct SupportedBandListEUTRA *next;
    SupportedBandEUTRA value;
} *SupportedBandListEUTRA;

typedef struct SupportedBandEUTRA_v9e0 {
    unsigned char   bit_mask;
#       define      bandEUTRA_v9e0_present 0x80
    FreqBandIndicator_v9e0 bandEUTRA_v9e0;  /* optional; set in bit_mask
                                             * bandEUTRA_v9e0_present if
                                             * present */
} SupportedBandEUTRA_v9e0;

typedef struct SupportedBandListEUTRA_v9e0 {
    struct SupportedBandListEUTRA_v9e0 *next;
    SupportedBandEUTRA_v9e0 value;
} *SupportedBandListEUTRA_v9e0;

typedef struct SupportedBandEUTRA_v1250 {
    unsigned char   bit_mask;
#       define      dl_256QAM_r12_present 0x80
#       define      ul_64QAM_r12_present 0x40
    _enum39         dl_256QAM_r12;  /* optional; set in bit_mask
                                     * dl_256QAM_r12_present if present */
    _enum39         ul_64QAM_r12;  /* optional; set in bit_mask
                                    * ul_64QAM_r12_present if present */
} SupportedBandEUTRA_v1250;

typedef struct SupportedBandListEUTRA_v1250 {
    struct SupportedBandListEUTRA_v1250 *next;
    SupportedBandEUTRA_v1250 value;
} *SupportedBandListEUTRA_v1250;

typedef struct BandListEUTRA {
    struct BandListEUTRA *next;
    BandInfoEUTRA   value;
} *BandListEUTRA;

typedef struct BandCombinationListEUTRA_r10 {
    struct BandCombinationListEUTRA_r10 *next;
    BandInfoEUTRA   value;
} *BandCombinationListEUTRA_r10;

typedef struct InterFreqBandInfo {
    ossBoolean      interFreqNeedForGaps;
} InterFreqBandInfo;

typedef struct InterFreqBandList {
    struct InterFreqBandList *next;
    InterFreqBandInfo value;
} *InterFreqBandList;

typedef struct InterRAT_BandInfo {
    ossBoolean      interRAT_NeedForGaps;
} InterRAT_BandInfo;

typedef struct InterRAT_BandList {
    struct InterRAT_BandList *next;
    InterRAT_BandInfo value;
} *InterRAT_BandList;

typedef enum SupportedBandUTRA_FDD {
    bandI = 0,
    bandII = 1,
    bandIII = 2,
    bandIV = 3,
    bandV = 4,
    bandVI = 5,
    bandVII = 6,
    bandVIII = 7,
    bandIX = 8,
    bandX = 9,
    bandXI = 10,
    bandXII = 11,
    bandXIII = 12,
    bandXIV = 13,
    bandXV = 14,
    bandXVI = 15,
    bandXVII_8a0 = 16,
    bandXVIII_8a0 = 17,
    bandXIX_8a0 = 18,
    bandXX_8a0 = 19,
    bandXXI_8a0 = 20,
    bandXXII_8a0 = 21,
    bandXXIII_8a0 = 22,
    bandXXIV_8a0 = 23,
    bandXXV_8a0 = 24,
    bandXXVI_8a0 = 25,
    bandXXVII_8a0 = 26,
    bandXXVIII_8a0 = 27,
    bandXXIX_8a0 = 28,
    bandXXX_8a0 = 29,
    bandXXXI_8a0 = 30,
    bandXXXII_8a0 = 31
} SupportedBandUTRA_FDD;

typedef struct SupportedBandListUTRA_FDD {
    struct SupportedBandListUTRA_FDD *next;
    SupportedBandUTRA_FDD value;
} *SupportedBandListUTRA_FDD;

typedef enum SupportedBandUTRA_TDD128 {
    SupportedBandUTRA_TDD128_a = 0,
    SupportedBandUTRA_TDD128_b = 1,
    SupportedBandUTRA_TDD128_c = 2,
    SupportedBandUTRA_TDD128_d = 3,
    SupportedBandUTRA_TDD128_e = 4,
    SupportedBandUTRA_TDD128_f = 5,
    SupportedBandUTRA_TDD128_g = 6,
    SupportedBandUTRA_TDD128_h = 7,
    SupportedBandUTRA_TDD128_i = 8,
    SupportedBandUTRA_TDD128_j = 9,
    SupportedBandUTRA_TDD128_k = 10,
    SupportedBandUTRA_TDD128_l = 11,
    SupportedBandUTRA_TDD128_m = 12,
    SupportedBandUTRA_TDD128_n = 13,
    SupportedBandUTRA_TDD128_o = 14,
    SupportedBandUTRA_TDD128_p = 15
} SupportedBandUTRA_TDD128;

typedef struct SupportedBandListUTRA_TDD128 {
    struct SupportedBandListUTRA_TDD128 *next;
    SupportedBandUTRA_TDD128 value;
} *SupportedBandListUTRA_TDD128;

typedef enum SupportedBandUTRA_TDD384 {
    SupportedBandUTRA_TDD384_a = 0,
    SupportedBandUTRA_TDD384_b = 1,
    SupportedBandUTRA_TDD384_c = 2,
    SupportedBandUTRA_TDD384_d = 3,
    SupportedBandUTRA_TDD384_e = 4,
    SupportedBandUTRA_TDD384_f = 5,
    SupportedBandUTRA_TDD384_g = 6,
    SupportedBandUTRA_TDD384_h = 7,
    SupportedBandUTRA_TDD384_i = 8,
    SupportedBandUTRA_TDD384_j = 9,
    SupportedBandUTRA_TDD384_k = 10,
    SupportedBandUTRA_TDD384_l = 11,
    SupportedBandUTRA_TDD384_m = 12,
    SupportedBandUTRA_TDD384_n = 13,
    SupportedBandUTRA_TDD384_o = 14,
    SupportedBandUTRA_TDD384_p = 15
} SupportedBandUTRA_TDD384;

typedef struct SupportedBandListUTRA_TDD384 {
    struct SupportedBandListUTRA_TDD384 *next;
    SupportedBandUTRA_TDD384 value;
} *SupportedBandListUTRA_TDD384;

typedef enum SupportedBandUTRA_TDD768 {
    SupportedBandUTRA_TDD768_a = 0,
    SupportedBandUTRA_TDD768_b = 1,
    SupportedBandUTRA_TDD768_c = 2,
    SupportedBandUTRA_TDD768_d = 3,
    SupportedBandUTRA_TDD768_e = 4,
    SupportedBandUTRA_TDD768_f = 5,
    SupportedBandUTRA_TDD768_g = 6,
    SupportedBandUTRA_TDD768_h = 7,
    SupportedBandUTRA_TDD768_i = 8,
    SupportedBandUTRA_TDD768_j = 9,
    SupportedBandUTRA_TDD768_k = 10,
    SupportedBandUTRA_TDD768_l = 11,
    SupportedBandUTRA_TDD768_m = 12,
    SupportedBandUTRA_TDD768_n = 13,
    SupportedBandUTRA_TDD768_o = 14,
    SupportedBandUTRA_TDD768_p = 15
} SupportedBandUTRA_TDD768;

typedef struct SupportedBandListUTRA_TDD768 {
    struct SupportedBandListUTRA_TDD768 *next;
    SupportedBandUTRA_TDD768 value;
} *SupportedBandListUTRA_TDD768;

typedef enum SupportedBandGERAN {
    gsm450 = 0,
    gsm480 = 1,
    gsm710 = 2,
    gsm750 = 3,
    gsm810 = 4,
    gsm850 = 5,
    gsm900P = 6,
    gsm900E = 7,
    gsm900R = 8,
    gsm1800 = 9,
    gsm1900 = 10,
    SupportedBandGERAN_spare5 = 11,
    SupportedBandGERAN_spare4 = 12,
    SupportedBandGERAN_spare3 = 13,
    SupportedBandGERAN_spare2 = 14,
    SupportedBandGERAN_spare1 = 15
} SupportedBandGERAN;

typedef struct SupportedBandListGERAN {
    struct SupportedBandListGERAN *next;
    SupportedBandGERAN value;
} *SupportedBandListGERAN;

typedef struct SupportedBandListHRPD {
    struct SupportedBandListHRPD *next;
    BandclassCDMA2000 value;
} *SupportedBandListHRPD;

typedef struct SupportedBandList1XRTT {
    struct SupportedBandList1XRTT *next;
    BandclassCDMA2000 value;
} *SupportedBandList1XRTT;

typedef struct NAICS_Capability_Entry_r12 {
    unsigned short  numberOfNAICS_CapableCC_r12;
    enum {
        numberOfAggregatedPRB_r12_n50 = 0,
        numberOfAggregatedPRB_r12_n75 = 1,
        numberOfAggregatedPRB_r12_n100 = 2,
        n125 = 3,
        n150 = 4,
        n175 = 5,
        numberOfAggregatedPRB_r12_n200 = 6,
        n225 = 7,
        n250 = 8,
        n275 = 9,
        n300 = 10,
        n350 = 11,
        numberOfAggregatedPRB_r12_n400 = 12,
        n450 = 13,
        n500 = 14,
        numberOfAggregatedPRB_r12_spare = 15
    } numberOfAggregatedPRB_r12;
} NAICS_Capability_Entry_r12;

typedef struct NAICS_Capability_List_r12 {
    struct NAICS_Capability_List_r12 *next;
    NAICS_Capability_Entry_r12 value;
} *NAICS_Capability_List_r12;

typedef struct SupportedBandInfo_r12 {
    unsigned char   bit_mask;
#       define      support_r12_present 0x80
    _enum39         support_r12;  /* optional; set in bit_mask
                                   * support_r12_present if present */
} SupportedBandInfo_r12;

typedef struct SupportedBandInfoList_r12 {
    struct SupportedBandInfoList_r12 *next;
    SupportedBandInfo_r12 value;
} *SupportedBandInfoList_r12;

typedef struct FreqBandIndicatorListEUTRA_r12 {
    struct FreqBandIndicatorListEUTRA_r12 *next;
    FreqBandIndicator_r11 value;
} *FreqBandIndicatorListEUTRA_r12;

typedef struct VisitedCellInfo_r12 {
    unsigned char   bit_mask;
#       define      visitedCellId_r12_present 0x80
    struct {
        unsigned short  choice;
#           define      cellGlobalId_r12_chosen 1
#           define      pci_arfcn_r12_chosen 2
        union {
            CellGlobalIdEUTRA cellGlobalId_r12;  /* to choose, set choice to
                                                  * cellGlobalId_r12_chosen */
            struct _seq102 {
                PhysCellId      physCellId_r12;
                ARFCN_ValueEUTRA_r9 carrierFreq_r12;
            } pci_arfcn_r12;  /* to choose, set choice to
                               * pci_arfcn_r12_chosen */
        } u;
    } visitedCellId_r12;  /* optional; set in bit_mask visitedCellId_r12_present
                           * if present */
    unsigned short  timeSpent_r12;
} VisitedCellInfo_r12;

typedef struct VisitedCellInfoList_r12 {
    struct VisitedCellInfoList_r12 *next;
    VisitedCellInfo_r12 value;
} *VisitedCellInfoList_r12;

typedef struct MBSFN_AreaInfo_r9 {
    MBSFN_AreaId_r12 mbsfn_AreaId_r9;
    _enum26         non_MBSFNregionLength;
    unsigned short  notificationIndicator_r9;
    struct {
        _enum2          mcch_RepetitionPeriod_r9;
        unsigned short  mcch_Offset_r9;
        enum {
            mcch_ModificationPeriod_r9_rf512 = 0,
            mcch_ModificationPeriod_r9_rf1024 = 1
        } mcch_ModificationPeriod_r9;
        _bit1           sf_AllocInfo_r9;
        enum {
            signallingMCS_r9_n2 = 0,
            signallingMCS_r9_n7 = 1,
            n13 = 2,
            n19 = 3
        } signallingMCS_r9;
    } mcch_Config_r9;
} MBSFN_AreaInfo_r9;

typedef struct MBSFN_AreaInfoList_r9 {
    struct MBSFN_AreaInfoList_r9 *next;
    MBSFN_AreaInfo_r9 value;
} *MBSFN_AreaInfoList_r9;

typedef struct PMCH_Config_r9 {
    unsigned short  sf_AllocEnd_r9;
    unsigned short  dataMCS_r9;
    enum {
        mch_SchedulingPeriod_r9_rf8 = 0,
        mch_SchedulingPeriod_r9_rf16 = 1,
        mch_SchedulingPeriod_r9_rf32 = 2,
        mch_SchedulingPeriod_r9_rf64 = 3,
        mch_SchedulingPeriod_r9_rf128 = 4,
        mch_SchedulingPeriod_r9_rf256 = 5,
        mch_SchedulingPeriod_r9_rf512 = 6,
        mch_SchedulingPeriod_r9_rf1024 = 7
    } mch_SchedulingPeriod_r9;
} PMCH_Config_r9;

typedef struct PMCH_Info_r9 {
    PMCH_Config_r9  pmch_Config_r9;
    struct MBMS_SessionInfoList_r9 *mbms_SessionInfoList_r9;
} PMCH_Info_r9;

typedef struct PMCH_InfoList_r9 {
    struct PMCH_InfoList_r9 *next;
    PMCH_Info_r9    value;
} *PMCH_InfoList_r9;

typedef struct PMCH_Config_r12 {
    unsigned short  sf_AllocEnd_r12;
    struct {
        unsigned short  choice;
#           define      normal_r12_chosen 1
#           define      higerOrder_r12_chosen 2
        union {
            unsigned short  normal_r12;  /* to choose, set choice to
                                          * normal_r12_chosen */
            unsigned short  higerOrder_r12;  /* to choose, set choice to
                                              * higerOrder_r12_chosen */
        } u;
    } dataMCS_r12;
    enum {
        mch_SchedulingPeriod_r12_rf4 = 0,
        mch_SchedulingPeriod_r12_rf8 = 1,
        mch_SchedulingPeriod_r12_rf16 = 2,
        mch_SchedulingPeriod_r12_rf32 = 3,
        mch_SchedulingPeriod_r12_rf64 = 4,
        mch_SchedulingPeriod_r12_rf128 = 5,
        mch_SchedulingPeriod_r12_rf256 = 6,
        mch_SchedulingPeriod_r12_rf512 = 7,
        mch_SchedulingPeriod_r12_rf1024 = 8
    } mch_SchedulingPeriod_r12;
} PMCH_Config_r12;

typedef struct PMCH_InfoExt_r12 {
    PMCH_Config_r12 pmch_Config_r12;
    struct MBMS_SessionInfoList_r9 *mbms_SessionInfoList_r12;
} PMCH_InfoExt_r12;

typedef struct PMCH_InfoListExt_r12 {
    struct PMCH_InfoListExt_r12 *next;
    PMCH_InfoExt_r12 value;
} *PMCH_InfoListExt_r12;

typedef struct MBMS_SessionInfo_r9 {
    unsigned char   bit_mask;
#       define      sessionId_r9_present 0x80
    TMGI_r9         tmgi_r9;
    _octet3         sessionId_r9;  /* optional; set in bit_mask
                                    * sessionId_r9_present if present */
                                   /* Need OR */
    unsigned short  logicalChannelIdentity_r9;
} MBMS_SessionInfo_r9;

typedef struct MBMS_SessionInfoList_r9 {
    struct MBMS_SessionInfoList_r9 *next;
    MBMS_SessionInfo_r9 value;
} *MBMS_SessionInfoList_r9;

typedef unsigned short  SL_TxPoolIdentity_r12;

typedef struct SL_CommTxPoolToAddMod_r12 {
    SL_TxPoolIdentity_r12 poolIdentity_r12;
    SL_CommResourcePool_r12 pool_r12;
} SL_CommTxPoolToAddMod_r12;

typedef struct SL_CommTxPoolToAddModList_r12 {
    struct SL_CommTxPoolToAddModList_r12 *next;
    SL_CommTxPoolToAddMod_r12 value;
} *SL_CommTxPoolToAddModList_r12;

typedef struct SL_CommTxPoolList_r12 {
    struct SL_CommTxPoolList_r12 *next;
    SL_CommResourcePool_r12 value;
} *SL_CommTxPoolList_r12;

typedef struct SL_CommRxPoolList_r12 {
    struct SL_CommRxPoolList_r12 *next;
    SL_CommResourcePool_r12 value;
} *SL_CommRxPoolList_r12;

typedef struct SL_DiscTxPoolToAddMod_r12 {
    SL_TxPoolIdentity_r12 poolIdentity_r12;
    SL_DiscResourcePool_r12 pool_r12;
} SL_DiscTxPoolToAddMod_r12;

typedef struct SL_DiscTxPoolToAddModList_r12 {
    struct SL_DiscTxPoolToAddModList_r12 *next;
    SL_DiscTxPoolToAddMod_r12 value;
} *SL_DiscTxPoolToAddModList_r12;

typedef struct SL_TF_IndexPair_r12 {
    unsigned char   bit_mask;
#       define      discSF_Index_r12_present 0x80
#       define      discPRB_Index_r12_present 0x40
    unsigned short  discSF_Index_r12;  /* optional; set in bit_mask
                                        * discSF_Index_r12_present if present */
                                       /* Need ON */
    unsigned short  discPRB_Index_r12;  /* optional; set in bit_mask
                                         * discPRB_Index_r12_present if
                                         * present */
                                        /* Need ON */
} SL_TF_IndexPair_r12;

typedef struct SL_TF_IndexPairList_r12 {
    struct SL_TF_IndexPairList_r12 *next;
    SL_TF_IndexPair_r12 value;
} *SL_TF_IndexPairList_r12;

typedef struct SL_TF_IndexPair_r12b {
    unsigned char   bit_mask;
#       define      discSF_Index_r12b_present 0x80
#       define      discPRB_Index_r12b_present 0x40
    unsigned short  discSF_Index_r12b;  /* optional; set in bit_mask
                                         * discSF_Index_r12b_present if
                                         * present */
                                        /* Need ON */
    unsigned short  discPRB_Index_r12b;  /* optional; set in bit_mask
                                          * discPRB_Index_r12b_present if
                                          * present */
                                         /* Need ON */
} SL_TF_IndexPair_r12b;

typedef struct SL_TF_IndexPairList_r12b {
    struct SL_TF_IndexPairList_r12b *next;
    SL_TF_IndexPair_r12b value;
} *SL_TF_IndexPairList_r12b;

typedef struct SL_DiscTxPoolList_r12 {
    struct SL_DiscTxPoolList_r12 *next;
    SL_DiscResourcePool_r12 value;
} *SL_DiscTxPoolList_r12;

typedef struct SL_DiscRxPoolList_r12 {
    struct SL_DiscRxPoolList_r12 *next;
    SL_DiscResourcePool_r12 value;
} *SL_DiscRxPoolList_r12;

typedef struct SL_DiscTxPowerInfo_r12 {
    P_Max           discMaxTxPower_r12;
} SL_DiscTxPowerInfo_r12;

typedef struct SL_DiscTxPowerInfoList_r12 {
    struct SL_DiscTxPowerInfoList_r12 *next;
    SL_DiscTxPowerInfo_r12 value;
} *SL_DiscTxPowerInfoList_r12;

typedef unsigned short  SL_OffsetIndicatorSync_r12;

typedef unsigned short  SLSSID_r12;

typedef struct SL_SyncConfig_r12 {
    unsigned char   bit_mask;
#       define      SL_SyncConfig_r12_txParameters_r12_present 0x80
#       define      rxParamsNCell_r12_present 0x40
    SL_CP_Len_r12   syncCP_Len_r12;
    SL_OffsetIndicatorSync_r12 syncOffsetIndicator_r12;
    SLSSID_r12      slssid_r12;
    struct {
        unsigned char   bit_mask;
#           define      syncInfoReserved_r12_present 0x80
        SL_TxParameters_r12 syncTxParameters_r12;
        RSRP_RangeSL_r12 syncTxThreshIC_r12;
        _bit1           syncInfoReserved_r12;  /* optional; set in bit_mask
                                                * syncInfoReserved_r12_present
                                                * if present */
                                               /* Need OR */
    } txParameters_r12;  /* optional; set in bit_mask
                          * SL_SyncConfig_r12_txParameters_r12_present if
                          * present */
       /* Need OR */
    struct {
        PhysCellId      physCellId_r12;
        enum {
            w1 = 0,
            w2 = 1
        } discSyncWindow_r12;
    } rxParamsNCell_r12;  /* optional; set in bit_mask rxParamsNCell_r12_present
                           * if present */
       /* Need OR */
} SL_SyncConfig_r12;

typedef struct SL_SyncConfigList_r12 {
    struct SL_SyncConfigList_r12 *next;
    SL_SyncConfig_r12 value;
} *SL_SyncConfigList_r12;

typedef struct SL_TxPoolToReleaseList_r12 {
    struct SL_TxPoolToReleaseList_r12 *next;
    SL_TxPoolIdentity_r12 value;
} *SL_TxPoolToReleaseList_r12;

typedef struct MasterInformationBlock_SL {
    _enum1          sl_Bandwidth_r12;
    TDD_ConfigSL_r12 tdd_ConfigSL_r12;
    _bit1           directFrameNumber_r12;
    unsigned short  directSubframeNumber_r12;
    ossBoolean      inCoverage_r12;
    _bit1           reserved_r12;
} MasterInformationBlock_SL;

typedef MasterInformationBlock_SL SBCCH_SL_BCH_MessageType;

typedef struct SBCCH_SL_BCH_Message {
    SBCCH_SL_BCH_MessageType message;
} SBCCH_SL_BCH_Message;

typedef struct VarConnEstFailReport_r11 {
    ConnEstFailReport_r11 connEstFailReport_r11;
    PLMN_Identity   plmn_Identity_r11;
} VarConnEstFailReport_r11;

typedef struct VarLogMeasConfig_r10 {
    unsigned char   bit_mask;
#       define      VarLogMeasConfig_r10_areaConfiguration_r10_present 0x80
    AreaConfiguration_r10 areaConfiguration_r10;  /* optional; set in bit_mask
                        * VarLogMeasConfig_r10_areaConfiguration_r10_present if
                        * present */
    LoggingDuration_r10 loggingDuration_r10;
    LoggingInterval_r10 loggingInterval_r10;
} VarLogMeasConfig_r10;

typedef struct VarLogMeasConfig_r11 {
    unsigned char   bit_mask;
#       define      VarLogMeasConfig_r11_areaConfiguration_r10_present 0x80
#       define      VarLogMeasConfig_r11_areaConfiguration_v1130_present 0x40
    AreaConfiguration_r10 areaConfiguration_r10;  /* optional; set in bit_mask
                        * VarLogMeasConfig_r11_areaConfiguration_r10_present if
                        * present */
    AreaConfiguration_v1130 areaConfiguration_v1130;  /* optional; set in
                                   * bit_mask
                      * VarLogMeasConfig_r11_areaConfiguration_v1130_present if
                      * present */
    LoggingDuration_r10 loggingDuration_r10;
    LoggingInterval_r10 loggingInterval_r10;
} VarLogMeasConfig_r11;

typedef struct VarLogMeasConfig_r12 {
    unsigned char   bit_mask;
#       define      VarLogMeasConfig_r12_areaConfiguration_r10_present 0x80
#       define      VarLogMeasConfig_r12_areaConfiguration_v1130_present 0x40
#       define      VarLogMeasConfig_r12_targetMBSFN_AreaList_r12_present 0x20
    AreaConfiguration_r10 areaConfiguration_r10;  /* optional; set in bit_mask
                        * VarLogMeasConfig_r12_areaConfiguration_r10_present if
                        * present */
    AreaConfiguration_v1130 areaConfiguration_v1130;  /* optional; set in
                                   * bit_mask
                      * VarLogMeasConfig_r12_areaConfiguration_v1130_present if
                      * present */
    LoggingDuration_r10 loggingDuration_r10;
    LoggingInterval_r10 loggingInterval_r10;
    struct TargetMBSFN_AreaList_r12 *targetMBSFN_AreaList_r12;  /* optional; set
                                   * in bit_mask
                     * VarLogMeasConfig_r12_targetMBSFN_AreaList_r12_present if
                     * present */
} VarLogMeasConfig_r12;

typedef struct VarLogMeasReport_r10 {
    TraceReference_r10 traceReference_r10;
    _octet2         traceRecordingSessionRef_r10;
    _octet3         tce_Id_r10;
    PLMN_Identity   plmn_Identity_r10;
    AbsoluteTimeInfo_r10 absoluteTimeInfo_r10;
    struct LogMeasInfoList2_r10 *logMeasInfoList_r10;
} VarLogMeasReport_r10;

typedef struct VarLogMeasReport_r11 {
    TraceReference_r10 traceReference_r10;
    _octet2         traceRecordingSessionRef_r10;
    _octet3         tce_Id_r10;
    struct PLMN_IdentityList3_r11 *plmn_IdentityList_r11;
    AbsoluteTimeInfo_r10 absoluteTimeInfo_r10;
    struct LogMeasInfoList2_r10 *logMeasInfoList_r10;
} VarLogMeasReport_r11;

typedef struct LogMeasInfoList2_r10 {
    struct LogMeasInfoList2_r10 *next;
    LogMeasInfo_r10 value;
} *LogMeasInfoList2_r10;

typedef struct VarMeasConfig {
    unsigned short  bit_mask;
#       define      measIdList_present 0x8000
#       define      measIdListExt_r12_present 0x4000
#       define      measObjectList_present 0x2000
#       define      measObjectList_v9i0_present 0x1000
#       define      reportConfigList_present 0x0800
#       define      VarMeasConfig_quantityConfig_present 0x0400
#       define      VarMeasConfig_measScaleFactor_r12_present 0x0200
#       define      VarMeasConfig_s_Measure_present 0x0100
#       define      VarMeasConfig_speedStatePars_present 0x0080
#       define      VarMeasConfig_allowInterruptions_r11_present 0x0040
	/* Measurement identities */
    struct MeasIdToAddModList *measIdList;  /* optional; set in bit_mask
                                             * measIdList_present if present */
    struct MeasIdToAddModListExt_r12 *measIdListExt_r12;  /* optional; set in
                                   * bit_mask measIdListExt_r12_present if
                                   * present */
	/* Measurement objects */
    struct MeasObjectToAddModList *measObjectList;  /* optional; set in bit_mask
                                                     * measObjectList_present if
                                                     * present */
    struct MeasObjectToAddModList_v9e0 *measObjectList_v9i0;  /* optional; set
                                   * in bit_mask measObjectList_v9i0_present if
                                   * present */
	/* Reporting configurations */
    struct ReportConfigToAddModList *reportConfigList;  /* optional; set in
                                   * bit_mask reportConfigList_present if
                                   * present */
	/* Other parameters */
    QuantityConfig  quantityConfig;  /* optional; set in bit_mask
                                      * VarMeasConfig_quantityConfig_present if
                                      * present */
    MeasScaleFactor_r12 measScaleFactor_r12;  /* optional; set in bit_mask
                                 * VarMeasConfig_measScaleFactor_r12_present if
                                 * present */
    short           s_Measure;  /* optional; set in bit_mask
                                 * VarMeasConfig_s_Measure_present if present */
    _choice16       speedStatePars;  /* optional; set in bit_mask
                                      * VarMeasConfig_speedStatePars_present if
                                      * present */
    ossBoolean      allowInterruptions_r11;  /* optional; set in bit_mask
                              * VarMeasConfig_allowInterruptions_r11_present if
                              * present */
} VarMeasConfig;

typedef struct VarMeasReport {
    unsigned char   bit_mask;
#       define      VarMeasReport_measId_v1250_present 0x80
#       define      cellsTriggeredList_present 0x40
#       define      csi_RS_TriggeredList_r12_present 0x20
	/* List of measurement that have been triggered */
    MeasId          measId;
    MeasId_v1250    measId_v1250;  /* optional; set in bit_mask
                                    * VarMeasReport_measId_v1250_present if
                                    * present */
    struct CellsTriggeredList *cellsTriggeredList;  /* optional; set in bit_mask
                                                * cellsTriggeredList_present if
                                                * present */
    struct CSI_RS_TriggeredList_r12 *csi_RS_TriggeredList_r12;  /* optional; set
                                   * in bit_mask
                                   * csi_RS_TriggeredList_r12_present if
                                   * present */
    int             numberOfReportsSent;
} VarMeasReport;

typedef struct VarMeasReportList {
    struct VarMeasReportList *next;
    VarMeasReport   value;
} *VarMeasReportList;

typedef struct VarMeasReportList_r12 {
    struct VarMeasReportList_r12 *next;
    VarMeasReport   value;
} *VarMeasReportList_r12;

typedef struct CellsTriggeredList {
    struct CellsTriggeredList *next;
    struct {
        unsigned short  choice;
#           define      physCellIdEUTRA_chosen 1
#           define      physCellIdUTRA_chosen 2
#           define      physCellIdGERAN_chosen 3
#           define      physCellIdCDMA2000_chosen 4
        union {
            PhysCellId      physCellIdEUTRA;  /* to choose, set choice to
                                               * physCellIdEUTRA_chosen */
            _choice52       physCellIdUTRA;  /* to choose, set choice to
                                              * physCellIdUTRA_chosen */
            struct _seq103 {
                CarrierFreqGERAN carrierFreq;
                PhysCellIdGERAN physCellId;
            } physCellIdGERAN;  /* to choose, set choice to
                                 * physCellIdGERAN_chosen */
            PhysCellIdCDMA2000 physCellIdCDMA2000;  /* to choose, set choice to
                                                 * physCellIdCDMA2000_chosen */
        } u;
    } value;
} *CellsTriggeredList;

typedef struct CSI_RS_TriggeredList_r12 {
    struct CSI_RS_TriggeredList_r12 *next;
    MeasCSI_RS_Id_r12 value;
} *CSI_RS_TriggeredList_r12;

typedef struct VisitedCellInfoList_r12 *VarMobilityHistoryReport_r12;

typedef struct VarRLF_Report_r10 {
    RLF_Report_r9   rlf_Report_r10;
    PLMN_Identity   plmn_Identity_r10;
} VarRLF_Report_r10;

typedef struct VarRLF_Report_r11 {
    RLF_Report_r9   rlf_Report_r10;
    struct PLMN_IdentityList3_r11 *plmn_IdentityList_r11;
} VarRLF_Report_r11;

typedef struct VarShortMAC_Input {
    CellIdentity    cellIdentity;
    PhysCellId      physCellId;
    C_RNTI          c_RNTI;
} VarShortMAC_Input;

typedef struct SL_PreconfigGeneral_r12 {
	/* PDCP configuration */
    struct {
        ossBoolean      profile0x0001_r12;
        ossBoolean      profile0x0002_r12;
        ossBoolean      profile0x0004_r12;
        ossBoolean      profile0x0006_r12;
        ossBoolean      profile0x0101_r12;
        ossBoolean      profile0x0102_r12;
        ossBoolean      profile0x0104_r12;
    } rohc_Profiles_r12;
	/* Physical configuration */
    ARFCN_ValueEUTRA_r9 carrierFreq_r12;
    P_Max           maxTxPower_r12;
    AdditionalSpectrumEmission additionalSpectrumEmission_r12;
    _enum1          sl_bandwidth_r12;
    TDD_ConfigSL_r12 tdd_ConfigSL_r12;
    _bit1           reserved_r12;
} SL_PreconfigGeneral_r12;

typedef struct SL_PreconfigSync_r12 {
    SL_CP_Len_r12   syncCP_Len_r12;
    SL_OffsetIndicatorSync_r12 syncOffsetIndicator1_r12;
    SL_OffsetIndicatorSync_r12 syncOffsetIndicator2_r12;
    P0_SL_r12       syncTxParameters_r12;
    RSRP_RangeSL3_r12 syncTxThreshOoC_r12;
    FilterCoefficient filterCoefficient_r12;
    enum {
        syncRefMinHyst_r12_dB0 = 0,
        syncRefMinHyst_r12_dB3 = 1,
        syncRefMinHyst_r12_dB6 = 2,
        syncRefMinHyst_r12_dB9 = 3,
        syncRefMinHyst_r12_dB12 = 4
    } syncRefMinHyst_r12;
    enum {
        syncRefDiffHyst_r12_dB0 = 0,
        syncRefDiffHyst_r12_dB3 = 1,
        syncRefDiffHyst_r12_dB6 = 2,
        syncRefDiffHyst_r12_dB9 = 3,
        syncRefDiffHyst_r12_dB12 = 4,
        dBinf = 5
    } syncRefDiffHyst_r12;
} SL_PreconfigSync_r12;

typedef struct SL_Preconfiguration_r12 {
    SL_PreconfigGeneral_r12 preconfigGeneral_r12;
    SL_PreconfigSync_r12 preconfigSync_r12;
    struct SL_PreconfigCommPoolList4_r12 *preconfigComm_r12;
} SL_Preconfiguration_r12;

typedef struct SL_PreconfigCommPool_r12 {
/* This IE is same as SL-CommResourcePool with rxParametersNCell absent */
    SL_CP_Len_r12   sc_CP_Len_r12;
    SL_PeriodComm_r12 sc_Period_r12;
    SL_TF_ResourceConfig_r12 sc_TF_ResourceConfig_r12;
    P0_SL_r12       sc_TxParameters_r12;
    SL_CP_Len_r12   data_CP_Len_r12;
    SL_TF_ResourceConfig_r12 data_TF_ResourceConfig_r12;
    SL_HoppingConfigComm_r12 dataHoppingConfig_r12;
    P0_SL_r12       dataTxParameters_r12;
    SL_TRPT_Subset_r12 trpt_Subset_r12;
} SL_PreconfigCommPool_r12;

typedef struct SL_PreconfigCommPoolList4_r12 {
    struct SL_PreconfigCommPoolList4_r12 *next;
    SL_PreconfigCommPool_r12 value;
} *SL_PreconfigCommPoolList4_r12;

typedef struct HandoverCommand_r8_IEs {
    unsigned char   bit_mask;
#       define      HandoverCommand_r8_IEs_nonCriticalExtension_present 0x80
    struct {
        /* ContentsConstraint is applied to handoverCommandMessage */
        _octet1         encoded;
        DL_DCCH_Message *decoded;
    } handoverCommandMessage;
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                       * HandoverCommand_r8_IEs_nonCriticalExtension_present if
                       * present */
} HandoverCommand_r8_IEs;

typedef struct HandoverCommand {
    struct {
        unsigned short  choice;
#           define      HandoverCommand_criticalExtensions_c1_chosen 1
#           define      HandoverCommand_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice53 {
                unsigned short  choice;
#                   define      handoverCommand_r8_chosen 1
#                   define      HandoverCommand_criticalExtensions_c1_spare7_chosen 2
#                   define      HandoverCommand_criticalExtensions_c1_spare6_chosen 3
#                   define      HandoverCommand_criticalExtensions_c1_spare5_chosen 4
#                   define      HandoverCommand_criticalExtensions_c1_spare4_chosen 5
#                   define      HandoverCommand_criticalExtensions_c1_spare3_chosen 6
#                   define      HandoverCommand_criticalExtensions_c1_spare2_chosen 7
#                   define      HandoverCommand_criticalExtensions_c1_spare1_chosen 8
                union {
                    HandoverCommand_r8_IEs handoverCommand_r8;  /* to choose,
                                   * set choice to handoverCommand_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
                       * HandoverCommand_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
                       * HandoverCommand_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
                       * HandoverCommand_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
                       * HandoverCommand_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                       * HandoverCommand_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                       * HandoverCommand_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                       * HandoverCommand_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * HandoverCommand_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
        * HandoverCommand_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} HandoverCommand;

typedef struct SCG_Config_r12_IEs {
    unsigned char   bit_mask;
#       define      SCG_Config_r12_IEs_scg_RadioConfig_r12_present 0x80
#       define      SCG_Config_r12_IEs_nonCriticalExtension_present 0x40
    SCG_ConfigPartSCG_r12 scg_RadioConfig_r12;  /* optional; set in bit_mask
                            * SCG_Config_r12_IEs_scg_RadioConfig_r12_present if
                            * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                           * SCG_Config_r12_IEs_nonCriticalExtension_present if
                           * present */
} SCG_Config_r12_IEs;

typedef struct SCG_Config_r12 {
    struct {
        unsigned short  choice;
#           define      SCG_Config_r12_criticalExtensions_c1_chosen 1
#           define      SCG_Config_r12_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice54 {
                unsigned short  choice;
#                   define      scg_Config_r12_chosen 1
#                   define      SCG_Config_r12_criticalExtensions_c1_spare7_chosen 2
#                   define      SCG_Config_r12_criticalExtensions_c1_spare6_chosen 3
#                   define      SCG_Config_r12_criticalExtensions_c1_spare5_chosen 4
#                   define      SCG_Config_r12_criticalExtensions_c1_spare4_chosen 5
#                   define      SCG_Config_r12_criticalExtensions_c1_spare3_chosen 6
#                   define      SCG_Config_r12_criticalExtensions_c1_spare2_chosen 7
#                   define      SCG_Config_r12_criticalExtensions_c1_spare1_chosen 8
                union {
                    SCG_Config_r12_IEs scg_Config_r12;  /* to choose, set choice
                                                  * to scg_Config_r12_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
                        * SCG_Config_r12_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
                        * SCG_Config_r12_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
                        * SCG_Config_r12_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
                        * SCG_Config_r12_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                        * SCG_Config_r12_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                        * SCG_Config_r12_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                        * SCG_Config_r12_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * SCG_Config_r12_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
         * SCG_Config_r12_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SCG_Config_r12;

typedef struct AS_Config {
    unsigned char   bit_mask;
#       define      sourceSystemInformationBlockType1Ext_present 0x80
#       define      sourceOtherConfig_r9_present 0x40
#       define      sourceSCellConfigList_r10_present 0x20
#       define      sourceConfigSCG_r12_present 0x10
    MeasConfig      sourceMeasConfig;
    RadioResourceConfigDedicated sourceRadioResourceConfig;
    SecurityAlgorithmConfig sourceSecurityAlgorithmConfig;
    C_RNTI          sourceUE_Identity;
    MasterInformationBlock sourceMasterInformationBlock;
    SystemInformationBlockType1 sourceSystemInformationBlockType1;
    SystemInformationBlockType2 sourceSystemInformationBlockType2;
    AntennaInfoCommon antennaInfoCommon;
    ARFCN_ValueEUTRA sourceDl_CarrierFreq;
    struct {
        /* ContentsConstraint is applied to
         * sourceSystemInformationBlockType1Ext */
        _octet1         encoded;
        SystemInformationBlockType1_v890_IEs *decoded;
    } sourceSystemInformationBlockType1Ext;  /* extension #1; optional; set in
                                   * bit_mask
                              * sourceSystemInformationBlockType1Ext_present if
                              * present */
    OtherConfig_r9  sourceOtherConfig_r9;  /* extension #1; set in bit_mask
                                            * sourceOtherConfig_r9_present if
                                            * present */
	/* sourceOtherConfig-r9 should have been optional. A target eNB compliant with this transfer */
	/* syntax should support receiving an AS-Config not including this extension addition group */
	/* e.g. from a legacy source eNB */
    struct SCellToAddModList_r10 *sourceSCellConfigList_r10;  /* extension #2;
                                   * optional; set in bit_mask
                                   * sourceSCellConfigList_r10_present if
                                   * present */
    SCG_Config_r12  sourceConfigSCG_r12;  /* extension #3; optional; set in
                                   * bit_mask sourceConfigSCG_r12_present if
                                   * present */
} AS_Config;

typedef struct RRM_Config {
    unsigned char   bit_mask;
#       define      ue_InactiveTime_present 0x80
#       define      candidateCellInfoList_r10_present 0x40
    enum {
        ue_InactiveTime_s1 = 0,
        ue_InactiveTime_s2 = 1,
        s3 = 2,
        ue_InactiveTime_s5 = 3,
        s7 = 4,
        ue_InactiveTime_s10 = 5,
        s15 = 6,
        ue_InactiveTime_s20 = 7,
        s25 = 8,
        ue_InactiveTime_s30 = 9,
        s40 = 10,
        s50 = 11,
        ue_InactiveTime_min1 = 12,
        min1s20c = 13,
        min1s40 = 14,
        min2 = 15,
        min2s30 = 16,
        min3 = 17,
        min3s30 = 18,
        min4 = 19,
        ue_InactiveTime_min5 = 20,
        ue_InactiveTime_min6 = 21,
        min7 = 22,
        min8 = 23,
        min9 = 24,
        ue_InactiveTime_min10 = 25,
        ue_InactiveTime_min12 = 26,
        min14 = 27,
        min17 = 28,
        ue_InactiveTime_min20 = 29,
        min24 = 30,
        min28 = 31,
        min33 = 32,
        min38 = 33,
        min44 = 34,
        min50 = 35,
        hr1 = 36,
        hr1min30 = 37,
        hr2 = 38,
        hr2min30 = 39,
        hr3 = 40,
        hr3min30 = 41,
        hr4 = 42,
        hr5 = 43,
        hr6 = 44,
        hr8 = 45,
        hr10 = 46,
        hr13 = 47,
        hr16 = 48,
        hr20 = 49,
        day1 = 50,
        day1hr12 = 51,
        day2 = 52,
        day2hr12 = 53,
        day3 = 54,
        day4 = 55,
        day5 = 56,
        day7 = 57,
        day10 = 58,
        day14 = 59,
        day19 = 60,
        day24 = 61,
        day30 = 62,
        dayMoreThan30 = 63
    } ue_InactiveTime;  /* optional; set in bit_mask ue_InactiveTime_present if
                         * present */
    struct CandidateCellInfoList_r10 *candidateCellInfoList_r10;  /* extension
                                   * #1; optional; set in bit_mask
                                   * candidateCellInfoList_r10_present if
                                   * present */
} RRM_Config;

typedef struct ReestablishmentInfo {
    unsigned char   bit_mask;
#       define      additionalReestabInfoList_present 0x80
    PhysCellId      sourcePhysCellId;
    ShortMAC_I      targetCellShortMAC_I;
    struct AdditionalReestabInfoList *additionalReestabInfoList;  /* optional;
                                   * set in bit_mask
                                   * additionalReestabInfoList_present if
                                   * present */
} ReestablishmentInfo;

typedef struct AS_Context {
    unsigned char   bit_mask;
#       define      reestablishmentInfo_present 0x80
    ReestablishmentInfo reestablishmentInfo;  /* optional; set in bit_mask
                                               * reestablishmentInfo_present if
                                               * present */
                                              /* Cond HO */
} AS_Context;

typedef struct AS_Config_v9e0 {
    ARFCN_ValueEUTRA_v9e0 sourceDl_CarrierFreq_v9e0;
} AS_Config_v9e0;

typedef struct _seq104 {
    /* ContentsConstraint is applied to _seq104 */
    _octet1         encoded;
    MBMSInterestIndication_r11 *decoded;
} _seq104;

typedef struct AS_Context_v1130 {
    unsigned char   bit_mask;
#       define      AS_Context_v1130_idc_Indication_r11_present 0x80
#       define      mbmsInterestIndication_r11_present 0x40
#       define      AS_Context_v1130_powerPrefIndication_r11_present 0x20
#       define      sidelinkUEInformation_r12_present 0x10
    struct {
        /* ContentsConstraint is applied to idc_Indication_r11 */
        _octet1         encoded;
        InDeviceCoexIndication_r11 *decoded;
    } idc_Indication_r11;  /* optional; set in bit_mask
                            * AS_Context_v1130_idc_Indication_r11_present if
                            * present */
                           /* Cond HO2 */
    _seq104         mbmsInterestIndication_r11;  /* optional; set in bit_mask
                                        * mbmsInterestIndication_r11_present if
                                        * present */
                                                 /* Cond HO2 */
    struct {
        /* ContentsConstraint is applied to powerPrefIndication_r11 */
        _octet1         encoded;
        UEAssistanceInformation_r11 *decoded;
    } powerPrefIndication_r11;  /* optional; set in bit_mask
                          * AS_Context_v1130_powerPrefIndication_r11_present if
                          * present */
                                /* Cond HO2 */
    struct {
        /* ContentsConstraint is applied to sidelinkUEInformation_r12 */
        _octet1         encoded;
        SidelinkUEInformation_r12 *decoded;
    } sidelinkUEInformation_r12;  /* extension #1; optional; set in bit_mask
                                   * sidelinkUEInformation_r12_present if
                                   * present */
                                  /* Cond HO2 */
} AS_Context_v1130;

typedef struct AS_Config_v1250 {
    unsigned char   bit_mask;
#       define      sourceWlan_OffloadConfig_r12_present 0x80
#       define      sourceSL_CommConfig_r12_present 0x40
#       define      sourceSL_DiscConfig_r12_present 0x20
    WLAN_OffloadConfig_r12 sourceWlan_OffloadConfig_r12;  /* optional; set in
                                   * bit_mask
                                   * sourceWlan_OffloadConfig_r12_present if
                                   * present */
    SL_CommConfig_r12 sourceSL_CommConfig_r12;  /* optional; set in bit_mask
                                           * sourceSL_CommConfig_r12_present if
                                           * present */
    SL_DiscConfig_r12 sourceSL_DiscConfig_r12;  /* optional; set in bit_mask
                                           * sourceSL_DiscConfig_r12_present if
                                           * present */
} AS_Config_v1250;

typedef struct HandoverPreparationInformation_v1250_IEs {
    unsigned char   bit_mask;
#       define      ue_SupportedEARFCN_r12_present 0x80
#       define      as_Config_v1250_present 0x40
#       define      HandoverPreparationInformation_v1250_IEs_nonCriticalExtension_present 0x20
    ARFCN_ValueEUTRA_r9 ue_SupportedEARFCN_r12;  /* optional; set in bit_mask
                                            * ue_SupportedEARFCN_r12_present if
                                            * present */
                                                 /* Cond HO3 */
    AS_Config_v1250 as_Config_v1250;  /* optional; set in bit_mask
                                       * as_Config_v1250_present if present */
                                      /* Cond HO2 */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
     * HandoverPreparationInformation_v1250_IEs_nonCriticalExtension_present if
     * present */
} HandoverPreparationInformation_v1250_IEs;

typedef struct HandoverPreparationInformation_v1130_IEs {
    unsigned char   bit_mask;
#       define      as_Context_v1130_present 0x80
#       define      HandoverPreparationInformation_v1130_IEs_nonCriticalExtension_present 0x40
    AS_Context_v1130 as_Context_v1130;  /* optional; set in bit_mask
                                         * as_Context_v1130_present if
                                         * present */
                                        /* Cond HO2 */
    HandoverPreparationInformation_v1250_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
     * HandoverPreparationInformation_v1130_IEs_nonCriticalExtension_present if
     * present */
} HandoverPreparationInformation_v1130_IEs;

typedef struct HandoverPreparationInformation_v9e0_IEs {
    unsigned char   bit_mask;
#       define      as_Config_v9e0_present 0x80
#       define      HandoverPreparationInformation_v9e0_IEs_nonCriticalExtension_present 0x40
    AS_Config_v9e0  as_Config_v9e0;  /* optional; set in bit_mask
                                      * as_Config_v9e0_present if present */
                                     /* Cond HO2 */
    HandoverPreparationInformation_v1130_IEs nonCriticalExtension;  
                                  /* optional; set in bit_mask
      * HandoverPreparationInformation_v9e0_IEs_nonCriticalExtension_present if
      * present */
} HandoverPreparationInformation_v9e0_IEs;

typedef struct HandoverPreparationInformation_v9d0_IEs {
    unsigned char   bit_mask;
#       define      HandoverPreparationInformation_v9d0_IEs_lateNonCriticalExtension_present 0x80
#       define      HandoverPreparationInformation_v9d0_IEs_nonCriticalExtension_present 0x40
    _octet1         lateNonCriticalExtension;  /* optional; set in bit_mask
  * HandoverPreparationInformation_v9d0_IEs_lateNonCriticalExtension_present if
  * present */
    HandoverPreparationInformation_v9e0_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
      * HandoverPreparationInformation_v9d0_IEs_nonCriticalExtension_present if
      * present */
} HandoverPreparationInformation_v9d0_IEs;

typedef struct HandoverPreparationInformation_v920_IEs {
    unsigned char   bit_mask;
#       define      ue_ConfigRelease_r9_present 0x80
#       define      HandoverPreparationInformation_v920_IEs_nonCriticalExtension_present 0x40
    enum {
        ue_ConfigRelease_r9_rel9 = 0,
        ue_ConfigRelease_r9_rel10 = 1,
        ue_ConfigRelease_r9_rel11 = 2,
        ue_ConfigRelease_r9_rel12 = 3,
        ue_ConfigRelease_r9_spare4 = 4,
        ue_ConfigRelease_r9_spare3 = 5,
        ue_ConfigRelease_r9_spare2 = 6,
        ue_ConfigRelease_r9_spare1 = 7
    } ue_ConfigRelease_r9;  /* optional; set in bit_mask
                             * ue_ConfigRelease_r9_present if present */
       /* Cond HO2 */
    HandoverPreparationInformation_v9d0_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
      * HandoverPreparationInformation_v920_IEs_nonCriticalExtension_present if
      * present */
} HandoverPreparationInformation_v920_IEs;

typedef struct HandoverPreparationInformation_r8_IEs {
    unsigned char   bit_mask;
#       define      as_Config_present 0x80
#       define      rrm_Config_present 0x40
#       define      as_Context_present 0x20
#       define      HandoverPreparationInformation_r8_IEs_nonCriticalExtension_present 0x10
    struct UE_CapabilityRAT_ContainerList *ue_RadioAccessCapabilityInfo;
    AS_Config       as_Config;  /* optional; set in bit_mask as_Config_present
                                 * if present */
                                /* Cond HO */
    RRM_Config      rrm_Config;  /* optional; set in bit_mask rrm_Config_present
                                  * if present */
    AS_Context      as_Context;  /* optional; set in bit_mask as_Context_present
                                  * if present */
                                 /* Cond HO */
    HandoverPreparationInformation_v920_IEs nonCriticalExtension;  /* optional;
                                   * set in bit_mask
        * HandoverPreparationInformation_r8_IEs_nonCriticalExtension_present if
        * present */
} HandoverPreparationInformation_r8_IEs;

typedef struct HandoverPreparationInformation {
    struct {
        unsigned short  choice;
#           define      HandoverPreparationInformation_criticalExtensions_c1_chosen 1
#           define      HandoverPreparationInformation_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice55 {
                unsigned short  choice;
#                   define      handoverPreparationInformation_r8_chosen 1
#                   define      HandoverPreparationInformation_criticalExtensions_c1_spare7_chosen 2
#                   define      HandoverPreparationInformation_criticalExtensions_c1_spare6_chosen 3
#                   define      HandoverPreparationInformation_criticalExtensions_c1_spare5_chosen 4
#                   define      HandoverPreparationInformation_criticalExtensions_c1_spare4_chosen 5
#                   define      HandoverPreparationInformation_criticalExtensions_c1_spare3_chosen 6
#                   define      HandoverPreparationInformation_criticalExtensions_c1_spare2_chosen 7
#                   define      HandoverPreparationInformation_criticalExtensions_c1_spare1_chosen 8
                union {
                    HandoverPreparationInformation_r8_IEs handoverPreparationInformation_r8;                            /* to choose, set choice to
                                  * handoverPreparationInformation_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
        * HandoverPreparationInformation_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
        * HandoverPreparationInformation_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
        * HandoverPreparationInformation_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
        * HandoverPreparationInformation_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
        * HandoverPreparationInformation_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
        * HandoverPreparationInformation_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
        * HandoverPreparationInformation_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
               * HandoverPreparationInformation_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
         * HandoverPreparationInformation_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} HandoverPreparationInformation;

typedef struct SCG_ConfigRestrictInfo_r12 {
    unsigned short  maxSCH_TB_BitsDL_r12;
    unsigned short  maxSCH_TB_BitsUL_r12;
} SCG_ConfigRestrictInfo_r12;

typedef struct _seq105 {
    /* ContentsConstraint is applied to _seq105 */
    _octet1         encoded;
    UECapabilityInformation *decoded;
} _seq105;

typedef struct SCG_ConfigInfo_r12_IEs {
    unsigned short  bit_mask;
#       define      radioResourceConfigDedMCG_r12_present 0x8000
#       define      sCellToAddModListMCG_r12_present 0x4000
#       define      measGapConfig_r12_present 0x2000
#       define      SCG_ConfigInfo_r12_IEs_powerCoordinationInfo_r12_present 0x1000
#       define      SCG_ConfigInfo_r12_IEs_scg_RadioConfig_r12_present 0x0800
#       define      eutra_CapabilityInfo_r12_present 0x0400
#       define      scg_ConfigRestrictInfo_r12_present 0x0200
#       define      mbmsInterestIndication_r12_present 0x0100
#       define      measResultServCellListSCG_r12_present 0x0080
#       define      SCG_ConfigInfo_r12_IEs_drb_ToAddModListSCG_r12_present 0x0040
#       define      drb_ToReleaseListSCG_r12_present 0x0020
#       define      SCG_ConfigInfo_r12_IEs_sCellToAddModListSCG_r12_present 0x0010
#       define      SCG_ConfigInfo_r12_IEs_sCellToReleaseListSCG_r12_present 0x0008
#       define      SCG_ConfigInfo_r12_IEs_p_Max_r12_present 0x0004
#       define      SCG_ConfigInfo_r12_IEs_nonCriticalExtension_present 0x0002
    RadioResourceConfigDedicated radioResourceConfigDedMCG_r12;  /* optional;
                                   * set in bit_mask
                                   * radioResourceConfigDedMCG_r12_present if
                                   * present */
    struct SCellToAddModList_r10 *sCellToAddModListMCG_r12;  /* optional; set in
                                   * bit_mask sCellToAddModListMCG_r12_present
                                   * if present */
    MeasGapConfig   measGapConfig_r12;  /* optional; set in bit_mask
                                         * measGapConfig_r12_present if
                                         * present */
    PowerCoordinationInfo_r12 powerCoordinationInfo_r12;  /* optional; set in
                                   * bit_mask
                  * SCG_ConfigInfo_r12_IEs_powerCoordinationInfo_r12_present if
                  * present */
    SCG_ConfigPartSCG_r12 scg_RadioConfig_r12;  /* optional; set in bit_mask
                        * SCG_ConfigInfo_r12_IEs_scg_RadioConfig_r12_present if
                        * present */
    _seq105         eutra_CapabilityInfo_r12;  /* optional; set in bit_mask
                                          * eutra_CapabilityInfo_r12_present if
                                          * present */
    SCG_ConfigRestrictInfo_r12 scg_ConfigRestrictInfo_r12;  /* optional; set in
                                   * bit_mask scg_ConfigRestrictInfo_r12_present
                                   * if present */
    _seq104         mbmsInterestIndication_r12;  /* optional; set in bit_mask
                                        * mbmsInterestIndication_r12_present if
                                        * present */
    struct MeasResultServCellListSCG_r12 *measResultServCellListSCG_r12;  
                                        /* optional; set in bit_mask
                                         * measResultServCellListSCG_r12_present
                                         * if present */
    struct DRB_InfoListSCG_r12 *drb_ToAddModListSCG_r12;  /* optional; set in
                                   * bit_mask
                    * SCG_ConfigInfo_r12_IEs_drb_ToAddModListSCG_r12_present if
                    * present */
    struct DRB_ToReleaseList *drb_ToReleaseListSCG_r12;  /* optional; set in
                                   * bit_mask drb_ToReleaseListSCG_r12_present
                                   * if present */
    struct SCellToAddModListSCG_r12 *sCellToAddModListSCG_r12;  /* optional; set
                                   * in bit_mask
                   * SCG_ConfigInfo_r12_IEs_sCellToAddModListSCG_r12_present if
                   * present */
    struct SCellToReleaseList_r10 *sCellToReleaseListSCG_r12;  /* optional; set
                                   * in bit_mask
                  * SCG_ConfigInfo_r12_IEs_sCellToReleaseListSCG_r12_present if
                  * present */
    P_Max           p_Max_r12;  /* optional; set in bit_mask
                                 * SCG_ConfigInfo_r12_IEs_p_Max_r12_present if
                                 * present */
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
                       * SCG_ConfigInfo_r12_IEs_nonCriticalExtension_present if
                       * present */
} SCG_ConfigInfo_r12_IEs;

typedef struct SCG_ConfigInfo_r12 {
    struct {
        unsigned short  choice;
#           define      SCG_ConfigInfo_r12_criticalExtensions_c1_chosen 1
#           define      SCG_ConfigInfo_r12_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice56 {
                unsigned short  choice;
#                   define      scg_ConfigInfo_r12_chosen 1
#                   define      SCG_ConfigInfo_r12_criticalExtensions_c1_spare7_chosen 2
#                   define      SCG_ConfigInfo_r12_criticalExtensions_c1_spare6_chosen 3
#                   define      SCG_ConfigInfo_r12_criticalExtensions_c1_spare5_chosen 4
#                   define      SCG_ConfigInfo_r12_criticalExtensions_c1_spare4_chosen 5
#                   define      SCG_ConfigInfo_r12_criticalExtensions_c1_spare3_chosen 6
#                   define      SCG_ConfigInfo_r12_criticalExtensions_c1_spare2_chosen 7
#                   define      SCG_ConfigInfo_r12_criticalExtensions_c1_spare1_chosen 8
                union {
                    SCG_ConfigInfo_r12_IEs scg_ConfigInfo_r12;  /* to choose,
                                   * set choice to scg_ConfigInfo_r12_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * SCG_ConfigInfo_r12_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
     * SCG_ConfigInfo_r12_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} SCG_ConfigInfo_r12;

typedef struct DRB_InfoSCG_r12 {
    unsigned char   bit_mask;
#       define      DRB_InfoSCG_r12_eps_BearerIdentity_r12_present 0x80
#       define      DRB_InfoSCG_r12_drb_Type_r12_present 0x40
    unsigned short  eps_BearerIdentity_r12;  /* optional; set in bit_mask
                            * DRB_InfoSCG_r12_eps_BearerIdentity_r12_present if
                            * present */
                                             /* Cond DRB-Setup */
    DRB_Identity    drb_Identity_r12;
    enum {
        split = 0,
        scg = 1
    } drb_Type_r12;  /* optional; set in bit_mask
                      * DRB_InfoSCG_r12_drb_Type_r12_present if present */
       /* Cond DRB-Setup */
} DRB_InfoSCG_r12;

typedef struct DRB_InfoListSCG_r12 {
    struct DRB_InfoListSCG_r12 *next;
    DRB_InfoSCG_r12 value;
} *DRB_InfoListSCG_r12;

typedef struct Cell_ToAddMod_r12 {
    unsigned char   bit_mask;
#       define      Cell_ToAddMod_r12_cellIdentification_r12_present 0x80
#       define      measResultCellToAdd_r12_present 0x40
    SCellIndex_r10  sCellIndex_r12;
    _seq55          cellIdentification_r12;  /* optional; set in bit_mask
                          * Cell_ToAddMod_r12_cellIdentification_r12_present if
                          * present */
       /* Cond SCellAdd */
    struct {
        RSRP_Range      rsrpResult_r12;
        RSRQ_Range      rsrqResult_r12;
    } measResultCellToAdd_r12;  /* optional; set in bit_mask
                                 * measResultCellToAdd_r12_present if present */
       /* Cond SCellAdd2 */
} Cell_ToAddMod_r12;

typedef struct SCellToAddModListSCG_r12 {
    struct SCellToAddModListSCG_r12 *next;
    Cell_ToAddMod_r12 value;
} *SCellToAddModListSCG_r12;

typedef struct MeasResultServCellSCG_r12 {
    ServCellIndex_r10 servCellId_r12;
    struct {
        RSRP_Range      rsrpResultSCell_r12;
        RSRQ_Range      rsrqResultSCell_r12;
    } measResultSCell_r12;
} MeasResultServCellSCG_r12;

typedef struct MeasResultServCellListSCG_r12 {
    struct MeasResultServCellListSCG_r12 *next;
    MeasResultServCellSCG_r12 value;
} *MeasResultServCellListSCG_r12;

typedef struct UERadioAccessCapabilityInformation_r8_IEs {
    unsigned char   bit_mask;
#       define      UERadioAccessCapabilityInformation_r8_IEs_nonCriticalExtension_present 0x80
    _seq105         ue_RadioAccessCapabilityInfo;
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
    * UERadioAccessCapabilityInformation_r8_IEs_nonCriticalExtension_present if
    * present */
} UERadioAccessCapabilityInformation_r8_IEs;

typedef struct UERadioAccessCapabilityInformation {
    struct {
        unsigned short  choice;
#           define      UERadioAccessCapabilityInformation_criticalExtensions_c1_chosen 1
#           define      UERadioAccessCapabilityInformation_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice57 {
                unsigned short  choice;
#                   define      ueRadioAccessCapabilityInformation_r8_chosen 1
#                   define      UERadioAccessCapabilityInformation_criticalExtensions_c1_spare7_chosen 2
#                   define      UERadioAccessCapabilityInformation_criticalExtensions_c1_spare6_chosen 3
#                   define      UERadioAccessCapabilityInformation_criticalExtensions_c1_spare5_chosen 4
#                   define      UERadioAccessCapabilityInformation_criticalExtensions_c1_spare4_chosen 5
#                   define      UERadioAccessCapabilityInformation_criticalExtensions_c1_spare3_chosen 6
#                   define      UERadioAccessCapabilityInformation_criticalExtensions_c1_spare2_chosen 7
#                   define      UERadioAccessCapabilityInformation_criticalExtensions_c1_spare1_chosen 8
                union {
                    UERadioAccessCapabilityInformation_r8_IEs ueRadioAccessCapabilityInformation_r8;                    /* to choose, set choice to
                              * ueRadioAccessCapabilityInformation_r8_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
    * UERadioAccessCapabilityInformation_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
    * UERadioAccessCapabilityInformation_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
    * UERadioAccessCapabilityInformation_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
    * UERadioAccessCapabilityInformation_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
    * UERadioAccessCapabilityInformation_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
    * UERadioAccessCapabilityInformation_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
    * UERadioAccessCapabilityInformation_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
           * UERadioAccessCapabilityInformation_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
             * UERadioAccessCapabilityInformation_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} UERadioAccessCapabilityInformation;

typedef struct UERadioPagingInformation_r12_IEs {
    unsigned char   bit_mask;
#       define      UERadioPagingInformation_r12_IEs_nonCriticalExtension_present 0x80
    struct {
        /* ContentsConstraint is applied to ue_RadioPagingInfo_r12 */
        _octet1         encoded;
        UE_RadioPagingInfo_r12 *decoded;
    } ue_RadioPagingInfo_r12;
    _seq2           nonCriticalExtension;  /* optional; set in bit_mask
             * UERadioPagingInformation_r12_IEs_nonCriticalExtension_present if
             * present */
} UERadioPagingInformation_r12_IEs;

typedef struct UERadioPagingInformation {
    struct {
        unsigned short  choice;
#           define      UERadioPagingInformation_criticalExtensions_c1_chosen 1
#           define      UERadioPagingInformation_criticalExtensions_criticalExtensionsFuture_chosen 2
        union {
            struct _choice58 {
                unsigned short  choice;
#                   define      ueRadioPagingInformation_r12_chosen 1
#                   define      UERadioPagingInformation_criticalExtensions_c1_spare7_chosen 2
#                   define      UERadioPagingInformation_criticalExtensions_c1_spare6_chosen 3
#                   define      UERadioPagingInformation_criticalExtensions_c1_spare5_chosen 4
#                   define      UERadioPagingInformation_criticalExtensions_c1_spare4_chosen 5
#                   define      UERadioPagingInformation_criticalExtensions_c1_spare3_chosen 6
#                   define      UERadioPagingInformation_criticalExtensions_c1_spare2_chosen 7
#                   define      UERadioPagingInformation_criticalExtensions_c1_spare1_chosen 8
                union {
                    UERadioPagingInformation_r12_IEs ueRadioPagingInformation_r12;                                      /* to choose, set choice to
                                       * ueRadioPagingInformation_r12_chosen */
                    Nulltype        spare7;  /* to choose, set choice to
              * UERadioPagingInformation_criticalExtensions_c1_spare7_chosen */
                    Nulltype        spare6;  /* to choose, set choice to
              * UERadioPagingInformation_criticalExtensions_c1_spare6_chosen */
                    Nulltype        spare5;  /* to choose, set choice to
              * UERadioPagingInformation_criticalExtensions_c1_spare5_chosen */
                    Nulltype        spare4;  /* to choose, set choice to
              * UERadioPagingInformation_criticalExtensions_c1_spare4_chosen */
                    Nulltype        spare3;  /* to choose, set choice to
              * UERadioPagingInformation_criticalExtensions_c1_spare3_chosen */
                    Nulltype        spare2;  /* to choose, set choice to
              * UERadioPagingInformation_criticalExtensions_c1_spare2_chosen */
                    Nulltype        spare1;  /* to choose, set choice to
              * UERadioPagingInformation_criticalExtensions_c1_spare1_chosen */
                } u;
            } c1;  /* to choose, set choice to
                    * UERadioPagingInformation_criticalExtensions_c1_chosen */
            _seq2           criticalExtensionsFuture;  /* to choose, set choice
                                   * to
   * UERadioPagingInformation_criticalExtensions_criticalExtensionsFuture_chosen */
        } u;
    } criticalExtensions;
} UERadioPagingInformation;

typedef struct Key_eNodeB_Star {
    unsigned short  length;  /* number of significant bits */
    unsigned char   *value;
} Key_eNodeB_Star;

typedef struct AdditionalReestabInfo {
    CellIdentity    cellIdentity;
    Key_eNodeB_Star key_eNodeB_Star;
    ShortMAC_I      shortMAC_I;
} AdditionalReestabInfo;

typedef struct AdditionalReestabInfoList {
    struct AdditionalReestabInfoList *next;
    AdditionalReestabInfo value;
} *AdditionalReestabInfoList;

typedef struct CandidateCellInfo_r10 {
    unsigned char   bit_mask;
#       define      rsrpResult_r10_present 0x80
#       define      rsrqResult_r10_present 0x40
#       define      CandidateCellInfo_r10_dl_CarrierFreq_v1090_present 0x20
#       define      rsrqResult_v1250_present 0x10
	/* cellIdentification */
    PhysCellId      physCellId_r10;
    ARFCN_ValueEUTRA dl_CarrierFreq_r10;
	/* available measurement results */
    RSRP_Range      rsrpResult_r10;  /* optional; set in bit_mask
                                      * rsrpResult_r10_present if present */
    RSRQ_Range      rsrqResult_r10;  /* optional; set in bit_mask
                                      * rsrqResult_r10_present if present */
    ARFCN_ValueEUTRA_v9e0 dl_CarrierFreq_v1090;  /* extension #1; optional; set
                                   * in bit_mask
                        * CandidateCellInfo_r10_dl_CarrierFreq_v1090_present if
                        * present */
    RSRQ_Range_v1250 rsrqResult_v1250;  /* extension #2; optional; set in
                                   * bit_mask rsrqResult_v1250_present if
                                   * present */
} CandidateCellInfo_r10;

typedef struct CandidateCellInfoList_r10 {
    struct CandidateCellInfoList_r10 *next;
    CandidateCellInfo_r10 value;
} *CandidateCellInfoList_r10;

#ifndef _OSSNOVALUES

extern const int maxBandComb_r10;     /* Maximum number of band combinations. */

extern const int maxBandComb_r11;     /* Maximum number of additional band combinations. */

extern const int maxBands;  /* Maximum number of bands listed in EUTRA UE caps */

extern const int maxBandwidthClass_r10;      /* Maximum number of supported CA BW classes per band */

extern const int maxBandwidthCombSet_r10;      /* Maximum number of bandwidth combination sets per */

											/* supported band combination */
extern const int maxCDMA_BandClass;  /* Maximum value of the CDMA band classes */

extern const int maxCellBlack;  /* Maximum number of blacklisted physical cell identity */

											/* ranges listed in SIB type 4 and 5 */
extern const int maxCellHistory_r12;  /* Maximum number of visited EUTRA cells reported */

extern const int maxCellInfoGERAN_r9;      /* Maximum number of GERAN cells for which system in- */

											/* formation can be provided as redirection assistance */
extern const int maxCellInfoUTRA_r9;      /* Maximum number of UTRA cells for which system */

											/* information can be provided as redirection */
											/* assistance */
extern const int maxCombIDC_r11; /* Maximum number of reported UL CA combinations */

extern const int maxCSI_IM_r11;   /* Maximum number of CSI-IM configurations */

											/* (per carrier frequency) */
extern const unsigned short maxCSI_IM_r12;   /* Maximum number of CSI-IM configurations */

											/* (per carrier frequency) */
extern const int maxCSI_Proc_r11;   /* Maximum number of CSI processes (per carrier */

											/*  frequency) */
extern const int maxCSI_RS_NZP_r11;   /* Maximum number of CSI RS resource */

											/*  configurations using non-zero Tx power */
											/*  (per carrier frequency) */
extern const int maxCSI_RS_ZP_r11;   /* Maximum number of CSI RS resource */

											/*  configurations using zero Tx power(per carrier */
											/*  frequency) */
extern const int maxCQI_ProcExt_r11;   /* Maximum number of additional periodic CQI */

											/* configurations (per carrier frequency) */
extern const int maxFreqUTRA_TDD_r10;       /* Maximum number of UTRA TDD carrier frequencies for */

											/* which system information can be provided as */
											/* redirection assistance */
extern const int maxCellInter;  /* Maximum number of neighbouring inter-frequency */

											/* cells listed in SIB type 5 */
extern const int maxCellIntra;  /* Maximum number of neighbouring intra-frequency */

											/* cells listed in SIB type 4 */
extern const int maxCellListGERAN;   /* Maximum number of lists of GERAN cells */

extern const int maxCellMeas;  /* Maximum number of entries in each of the */

											/* cell lists in a measurement object */
extern const int maxCellReport;   /* Maximum number of reported cells/CSI-RS resources */

extern const int maxCSI_RS_Meas_r12;  /* Maximum number of entries in the CSI-RS list */

											/* in a measurement object */
extern const int maxDRB;  /* Maximum number of Data Radio Bearers */

extern const int maxDS_Duration_r12;   /* Maximum number of subframes in a discovery signals */

											/* occasion */
extern const int maxDS_ZTP_CSI_RS_r12;   /* Maximum number of zero transmission power CSI-RS for */

											/* a serving cell concerning discovery signals */
extern const int maxEARFCN;       /* Maximum value of EUTRA carrier frequency */

extern const int maxEARFCN_Plus1;       /* Lowest value extended EARFCN range */

extern const int maxEARFCN2;      /* Highest value extended EARFCN range */

extern const int maxEPDCCH_Set_r11;   /* Maximum number of EPDCCH sets */

extern const int maxFBI;  /* Maximum value of fequency band indicator */

extern const int maxFBI_Plus1;  /* Lowest value extended FBI range */

extern const int maxFBI2; /* Highest value extended FBI range */

extern const int maxFreq;   /* Maximum number of carrier frequencies */

extern const int maxFreqIDC_r11;  /* Maximum number of carrier frequencies that are */

											/* affected by the IDC problems */
extern const int maxFreqMBMS_r11;   /* Maximum number of carrier frequencies for which an */

											/* MBMS capable UE may indicate an interest */
extern const int maxGERAN_SI;  /* Maximum number of GERAN SI blocks that can be */

											/* provided as part of NACC information */
extern const int maxGNFG;  /* Maximum number of GERAN neighbour freq groups */

extern const int maxLogMeasReport_r10; /* Maximum number of logged measurement entries */

											/*  that can be reported by the UE in one message */
extern const int maxMBSFN_Allocations;   /* Maximum number of MBSFN frame allocations with */

											/* different offset */
extern const int maxMBSFN_Area;

extern const int maxMBSFN_Area_1;

extern const int maxMeasId;

extern const int maxMeasId_Plus1;

extern const int maxMeasId_r12;

extern const int maxMultiBands;   /* Maximum number of additional frequency bands */

											/* that a cell belongs to */
extern const int maxNAICS_Entries_r12;   /* Maximum number of supported NAICS combination(s) */

extern const int maxNeighCell_r12;   /* Maximum number of neighbouring cells in NAICS */

											/* configuration (per carrier frequency) */
extern const int maxObjectId;

extern const int maxP_a_PerNeighCell_r12;   /* Maximum number of power offsets for a neighbour cell */

											/* in NAICS configuration */
extern const int maxPageRec;  /* */

extern const int maxPhysCellIdRange_r9;   /* Maximum number of physical cell identity ranges */

extern const int maxPLMN_r11;       /* Maximum number of PLMNs */

extern const int maxPNOffset;     /* Maximum number of CDMA2000 PNOffsets */

extern const int maxPMCH_PerMBSFN;

extern const int maxRAT_Capabilities;   /* Maximum number of interworking RATs (incl EUTRA) */

extern const int maxRE_MapQCL_r11;   /* Maximum number of PDSCH RE Mapping configurations */

											/*  (per carrier frequency) */
extern const int maxReportConfigId;

extern const int maxRSTD_Freq_r10;   /* Maximum number of frequency layers for RSTD */

											/* measurement */
extern const int maxSAI_MBMS_r11;  /* Maximum number of MBMS service area identities */

											/* broadcast per carrier frequency */
extern const int maxSCell_r10;   /* Maximum number of SCells */

extern const int maxSL_Dest_r12;  /* Maximum number of sidelink destinations */

extern const int maxSL_DiscPowerClass_r12;   /* Maximum number of sidelink power classes */

extern const int maxSL_RxPool_r12;  /* Maximum number of individual sidelink Rx resource pools */

extern const int maxSL_SyncConfig_r12;  /* Maximum number of sidelink Sync configurations */

extern const int maxSL_TF_IndexPair_r12;  /* Maximum number of sidelink Time Freq resource index */

											/*  pairs */
extern const int maxSL_TxPool_r12;   /* Maximum number of individual sidelink Tx resource pools */

extern const int maxSTAG_r11;   /* Maximum number of STAGs */

extern const int maxServCell_r10;   /* Maximum number of Serving cells */

extern const int maxServiceCount;  /* Maximum number of MBMS services that can be included */

											/*  in an MBMS counting request and response */
extern const int maxServiceCount_1;

extern const int maxSessionPerPMCH;

extern const int maxSessionPerPMCH_1;

extern const int maxSIB;  /* Maximum number of SIBs */

extern const int maxSIB_1;

extern const int maxSI_Message;  /* Maximum number of SI messages */

extern const int maxSimultaneousBands_r10;  /* Maximum number of simultaneously aggregated bands */

extern const int maxSubframePatternIDC_r11;   /* Maximum number of subframe reservation patterns */

											/* that the UE can simultaneously recommend to the */
											/* E-UTRAN for use. */
extern const int maxUTRA_FDD_Carrier;  /* Maximum number of UTRA FDD carrier frequencies */

extern const int maxUTRA_TDD_Carrier;  /* Maximum number of UTRA TDD carrier frequencies */

extern const int maxWLAN_Id_r12;      /* Maximum number of WLAN identifiers */

extern const int maxLogMeas_r10;/* Maximum number of logged measurement entries */
											/*  that can be stored by the UE */

extern const int maxReestabInfo;  /* Maximum number of KeNB* and shortMAC-I forwarded */
											/* at handover for re-establishment preparation */

#endif  /* #ifndef _OSSNOVALUES */


extern void * const rrc;    /* encoder-decoder control table */
#endif /* OSS_rrc */
