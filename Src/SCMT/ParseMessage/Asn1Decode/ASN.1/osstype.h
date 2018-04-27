/***************************************************************************
 * Copyright (C) 1993-2008 OSS Nokalva, Inc.  All rights reserved.
 ***************************************************************************/

/* THIS FILE IS PROPRIETARY MATERIAL OF OSS NOKALVA, INC.
 * AND MAY BE USED ONLY BY DIRECT LICENSEES OF OSS NOKALVA, INC.
 * THIS FILE MAY NOT BE DISTRIBUTED. */

/*****************************************************************************/
/* FILE: @(#)osstype.hh	16.51  08/03/14                                        */
/*****************************************************************************/

/*****************************************************************************/
/* NOTE: this header file does not contain public definitions. It is used by */
/* generated control tables and code files only. Applications using OSS      */
/* tools should not include this file, neither users should be concerned     */
/* with its content.                                                         */
/*****************************************************************************/

#ifndef OSSTYPE_H
#define OSSTYPE_H

#include "ossasn1.h"
#include <stdarg.h>

/*****************************************************************************/
/*  NON-PUBLIC section: include all sections if none of them is explicitly   */
/*  specified                                                                */
/*****************************************************************************/

#ifdef _OSS_CODE_FILE
#if !defined(OSS_TOED_BER) && !defined(OSS_TOED_PER)\
	&& !defined(OSS_TOED_XER) && !defined(OSS_TOED_EXER)
#define OSS_TOED_BER
#define OSS_TOED_PER
#define OSS_TOED_XER
#define OSS_TOED_EXER
#endif /* !OSS_TOED_BER && !OSS_TOED_PER && !OSS_TOED_XER && !OSS_TOED_EXER */
#else /* _OSS_CODE_FILE */
#if !defined(OSS_SOED_BER) && !defined(OSS_SOED_PER)\
	&& !defined(OSS_SOED_XER) && !defined(OSS_SOED_EXER)
#define OSS_SOED_BER
#define OSS_SOED_PER
#define OSS_SOED_XER
#define OSS_SOED_EXER
#endif /* !OSS_SOED_BER && !OSS_SOED_PER && !OSS_SOED_XER && !OSS_SOED_EXER */
#endif /* _OSS_CODE_FILE */

/*****************************************************************************/
/*  NON-PUBLIC section: data alignment section for platforms with multiple   */
/*  alignment options (OSS data structures should use the same setting as    */
/*  OSS runtime does)                                                        */
/*****************************************************************************/

#ifdef macintosh
#pragma options align=mac68k
#endif
#if defined(_MSC_VER) && (defined(WINCE) || defined(_WIN64))
#pragma pack(push, ossPacking, 8)
#elif defined(_MSC_VER) && (defined(_WIN32) || defined(WIN32))
#pragma pack(push, ossPacking, 4)
#elif defined(_MSC_VER) && (defined(_WINDOWS) || defined(_MSDOS))
#pragma pack(1)
#elif defined(__BORLANDC__) && defined(__MSDOS__)
#ifdef _BC31
#pragma option -a-
#else
#pragma option -a1
#endif /* _BC31 */
#elif defined(__BORLANDC__) && defined(__WIN32__)
#pragma option -a4
#elif defined(__IBMC__) && (defined(__WIN32__) || defined(__OS2__))
#pragma pack(4)
#elif defined(__WATCOMC__) && defined(__NT__)
#pragma pack(push, 4)
#elif defined(__WATCOMC__) && (defined(__WINDOWS__) || defined(__DOS__))
#pragma pack(push, 1)
#endif /* _MSC_VER && (WINCE || _WIN64) */

#if defined(macintosh) && defined(__CFM68K__)
#pragma import on
#endif

#ifdef __cplusplus
extern "C"
{
#endif

/*****************************************************************************/
/*  General section                                                          */
/*****************************************************************************/

typedef struct {
  struct ObjectSetEntry *oset;
  struct ObjectSetEntry *xoset;
  unsigned char *        flags;
  unsigned int           count;
} XOSetInfoEntry;

#if defined(_OSS_CODE_FILE)
typedef struct ossDebugData {
    unsigned long flags;
    char *bufpos;
    long buflen;
#ifndef DLL_LINKAGE
    int  (DLL_ENTRY_FPTR *_System oss_print_pdu_ptr)(struct ossGlobal *,
					    int, void *);
    void (DLL_ENTRY_FPTR *_System oss_print_hex_ptr)(struct ossGlobal *,
					    char *, long);
    void (DLL_ENTRY_FPTR *_System oss_print_xml_ptr)(struct ossGlobal *,
					    char *, long, ossBoolean);
#endif /* !DLL_LINKAGE */
} ossDebugData;
#endif /* _OSS_CODE_FILE ... */

/* ossSetPresetDecFlags() flags */
#define UPDATE_INPUT_ADDRESS 0x1 /* Decoder updates the address and the length
				  * of input buffer on return */
#define USE_ETYPE_INDICES    0x2 /* All PDU numbers are treated by the runtime
				  * as etype indices */
#define USE_CPP_OPENTYPES    0x4 /* Decoder generates OpenTypeCPP instead of
				  * OpenType and OpenTypeExtendedCPP instead of
				  * OpenTypeExtended */




    /*************************************************************************/
    /*  DLL section (Windows and similar platforms only)                     */
    /*************************************************************************/
#if defined(_WINDOWS) || defined(_WIN32) || defined(WIN32) || \
	defined(__WIN32__) || defined(__OS2__) || defined(NETWARE_DLL)
struct memblock {
    struct memblock     *_prev;
    struct memblock     *_next;
    LONG                *fileOffset;
    short               *lineLength;
    short                blockNumber;
};
extern const int           ossEncoderDecoderType;
#endif /* _WINDOWS ||_WIN32 || WIN32 || __WIN32__ || __OS2__ || NETWARE_DLL */

#if defined(_WINDOWS) || defined(_WIN32) || \
	defined(__OS2__) || defined(NETWARE_DLL)


#define CHECK_UNINITIALIZED_FPTR_FIELDS(tbl)

struct cstrainTbl {
    int  (DLL_ENTRY_FPTR *_System ossConstrainp)(void *, int, void *, void *);
};

struct berTbl {
    int   (DLL_ENTRY_FPTR *_System ossBerEncodep)(void *, int, void *,
				void *, unsigned int);
    int   (DLL_ENTRY_FPTR *_System ossBerDecodep)(void *, int *, void *,
				void **, unsigned int);
#define   _dstd_parms_defx char **, long *, long
#define   _sdstd_parms_defx char **, long *
#define   _std_parms_defx char **, long *
    LONG_LONG (DLL_ENTRY_FPTR *_System _oss_dec_llintp)     (void *, _dstd_parms_defx);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_dec_ullintp)   (void *, _dstd_parms_defx);
    char (DLL_ENTRY_FPTR *_System _oss_dec_boolp)           (void *, _dstd_parms_defx);
    int  (DLL_ENTRY_FPTR *_System _oss_dec_iintp)           (void *, _dstd_parms_defx);
    long (DLL_ENTRY_FPTR *_System _oss_count_setof_itemsp)  (void *, _dstd_parms_defx);
    long (DLL_ENTRY_FPTR *_System _oss_dec_lengthp)         (void *, _sdstd_parms_defx);
    long (DLL_ENTRY_FPTR *_System _oss_dec_lintp)           (void *, _dstd_parms_defx);
    long (DLL_ENTRY_FPTR *_System _oss_encd_aiobjidp)       (void *, _std_parms_defx, void *, short);
    long (DLL_ENTRY_FPTR *_System _oss_encd_alobjidp)       (void *, _std_parms_defx, void *, short);
    long (DLL_ENTRY_FPTR *_System _oss_encd_asobjidp)       (void *, _std_parms_defx, void *, short);
    long (DLL_ENTRY_FPTR *_System _oss_encd_boolp)          (void *, _std_parms_defx, char);
    long (DLL_ENTRY_FPTR *_System _oss_encd_crealp)         (void *, _std_parms_defx, char *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_gtimep)         (void *, _std_parms_defx, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_huge_intp)      (void *, _std_parms_defx, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uhuge_intp)     (void *, _std_parms_defx, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_intp)           (void *, _std_parms_defx, LONG_LONG);
    long (DLL_ENTRY_FPTR *_System _oss_encd_lengthp)        (void *, _std_parms_defx, unsigned long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_liobjidp)       (void *, _std_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_llobjidp)       (void *, _std_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_lsobjidp)       (void *, _std_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_mrealp)         (void *, _std_parms_defx, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_nstrp)          (void *, _std_parms_defx, char *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_opentypep)      (void *, _std_parms_defx, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_pbitp)          (void *, _std_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_pstrp)          (void *, _std_parms_defx, char *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_realp)          (void *, _std_parms_defx, double);
    long (DLL_ENTRY_FPTR *_System _oss_encd_tagp)           (void *, _std_parms_defx, unsigned short, char);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uanyp)          (void *, _std_parms_defx, unsigned char *, unsigned long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ubitp)          (void *, _std_parms_defx, void *, char, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uintp)          (void *, _std_parms_defx, ULONG_LONG);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uiobjidp)       (void *, _std_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ulobjidp)       (void *, _std_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uoctp)          (void *, _std_parms_defx, void *, char, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_usobjidp)       (void *, _std_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ustrp)          (void *, _std_parms_defx, void *, char, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_utimep)         (void *, _std_parms_defx, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_vbitp)          (void *, _std_parms_defx, void *, long, char);
    long (DLL_ENTRY_FPTR *_System _oss_encd_voctp)          (void *, _std_parms_defx, void *, char, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_vstrp)          (void *, _std_parms_defx, void *, char, long);
    short (DLL_ENTRY_FPTR *_System _oss_dec_sintp)          (void *, _dstd_parms_defx);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_dec_uiintp)  (void *, _dstd_parms_defx);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_ulintp) (void *, _dstd_parms_defx);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_dec_usintp)(void *, _dstd_parms_defx);
    void *(DLL_ENTRY_FPTR *_System _oss_enc_popp)(void *);
    void (DLL_ENTRY_FPTR *_System _oss_enc_pushp)(void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_dec_alobjid_ptrp)    (void *, _dstd_parms_defx, void **, short);
    char (DLL_ENTRY_FPTR *_System _oss_dec_alobjidp_obsolete)        (void *, _dstd_parms_defx);
    void (DLL_ENTRY_FPTR *_System _oss_dec_asobjid_ptrp)    (void *, _dstd_parms_defx, void **, short);
    void (DLL_ENTRY_FPTR *_System _oss_dec_asobjidp)        (void *, _dstd_parms_defx, void *, short);
    void (DLL_ENTRY_FPTR *_System _oss_dec_crealp)          (void *, _dstd_parms_defx, char **);
    void (DLL_ENTRY_FPTR *_System _oss_dec_frealp)          (void *, _dstd_parms_defx, float *);
    void (DLL_ENTRY_FPTR *_System _oss_dec_gtimep)          (void *, _dstd_parms_defx, void *);
    void (DLL_ENTRY_FPTR *_System _oss_dec_liobjidp)        (void *, _dstd_parms_defx, void **, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_llobjidp)        (void *, _dstd_parms_defx, void **, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_lsobjidp)        (void *, _dstd_parms_defx, void **, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_mrealp)          (void *, _dstd_parms_defx, void *);
    void (DLL_ENTRY_FPTR *_System _oss_dec_nstr_ptrp)       (void *, _dstd_parms_defx, char **, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_nstrp)           (void *, _dstd_parms_defx, char *, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_opentypep)       (void *, _dstd_parms_defx, void *);
    void (DLL_ENTRY_FPTR *_System _oss_dec_pbitp)           (void *, _dstd_parms_defx, void *, long, char);
    void (DLL_ENTRY_FPTR *_System _oss_dec_pstrp)           (void *, _dstd_parms_defx, char *, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_realp)           (void *, _dstd_parms_defx, double *);
    long (DLL_ENTRY_FPTR *_System _oss_dec_tagp)            (void *, _sdstd_parms_defx);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_uanyp)  (void *, _dstd_parms_defx, void *);
    void (DLL_ENTRY_FPTR *_System _oss_dec_ubitp)           (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_uiobjidp)        (void *, _dstd_parms_defx, void *, short);
    void (DLL_ENTRY_FPTR *_System _oss_dec_ulobjidp)        (void *, _dstd_parms_defx, void *, short);
    void (DLL_ENTRY_FPTR *_System _oss_dec_uoctp)           (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_usobjidp)        (void *, _dstd_parms_defx, void *, short);
    void (DLL_ENTRY_FPTR *_System _oss_dec_ustrp)           (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_utimep)          (void *, _dstd_parms_defx, void *);
    void (DLL_ENTRY_FPTR *_System _oss_dec_vbit_ptrp)       (void *, _dstd_parms_defx, void **, long, char);
    void (DLL_ENTRY_FPTR *_System _oss_dec_vbitp)           (void *, _dstd_parms_defx, void *, long, char);
    void (DLL_ENTRY_FPTR *_System _oss_dec_voct_ptrp)       (void *, _dstd_parms_defx, void **, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_voctp)           (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_vstr_ptrp)       (void *, _dstd_parms_defx, void **, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_vstrp)           (void *, _dstd_parms_defx, void *, char, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_eobjidp)        (void *, _std_parms_defx, void *, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_eobjidp)         (void *, _dstd_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ereloidp)       (void *, _std_parms_defx, void *, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_ereloidp)        (void *, _dstd_parms_defx, void *, long);
    long (DLL_ENTRY_FPTR *_System _oss_write_bytes_dp)      (void *, _std_parms_defx, unsigned char *, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_dubitp)         (void *, _std_parms_defx, void *, char, long, char);
    long (DLL_ENTRY_FPTR *_System _oss_encd_dvbitp)         (void *, _std_parms_defx, void *, long, char, char);
    long (DLL_ENTRY_FPTR *_System _oss_encd_utf8bmpstrp)    (void *, _std_parms_defx, void *, char, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_utf8unistrp)    (void *, _std_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_utf8bmpstrp)     (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_utf8unistrp)     (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_skip_past_itemp)     (void *, _dstd_parms_defx);
    long (DLL_ENTRY_FPTR *_System _oss_encd_bmpstrp)        (void *, _std_parms_defx, void *, char, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_unistrp)        (void *, _std_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_bmpstrp)         (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_unistrp)         (void *, _dstd_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_rsf_extp)        (void *, _dstd_parms_defx, void **, char **, long *, unsigned int *);
    void (DLL_ENTRY_FPTR *_System _oss_free_rsf_extp_obsolete)(void *, _dstd_parms_defx);
    void (DLL_ENTRY_FPTR *_System _oss_dec_deferp)          (void *, _dstd_parms_defx, void *, short, ossBoolean);
    long (DLL_ENTRY_FPTR *_System _oss_encd_deferp)         (void *, _std_parms_defx, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_untimep)        (void *, _std_parms_defx, char *, long, short);
    void (DLL_ENTRY_FPTR *_System _oss_dec_untimep)         (void *, _dstd_parms_defx, char**, long, short);
    long (DLL_ENTRY_FPTR *_System _oss_encd_utf8unboundp)    (void *, _std_parms_defx, void *, char, long);
    void (DLL_ENTRY_FPTR *_System _oss_dec_utf8unboundp)     (void *, _dstd_parms_defx, void *, char, long);
	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_bmpstr_iap)       (void *, _dstd_parms_defx, void *, long);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_hint_iap)         (void *, _dstd_parms_defx, void *);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_ubit_iap)         (void *, _dstd_parms_defx, void *, long, unsigned int);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_unistr_iap) (void *, _dstd_parms_defx, void *, long);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_uoct_iap) (void *, _dstd_parms_defx, void *, long);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_ustr_iap) (void *, _dstd_parms_defx, void *, long);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_utf8bmpstr_iap) (void *, _dstd_parms_defx, void *, long);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_utf8unbound_iap) (void *, _dstd_parms_defx, void *, long);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_dec_utf8unistr_iap) (void *, _dstd_parms_defx, void *, long);

    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_vbit_iap) (void *, _dstd_parms_defx, void *, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_vbit_nzp) (void *, _dstd_parms_defx, void *, long, unsigned int);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_vbit_ptr_iap) (void *, _dstd_parms_defx, void *, unsigned int, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_vbit_ptr_nzp) (void *, _dstd_parms_defx, void *, unsigned int, long, unsigned int);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_voct_iap) (void *, _dstd_parms_defx, void *, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_voct_ptr_iap) (void *, _dstd_parms_defx, void *, unsigned int, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_vstr_iap) (void *, _dstd_parms_defx, void *, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_dec_vstr_ptr_iap) (void *, _dstd_parms_defx, void *, unsigned int, long);
    enum MixedReal_kind (DLL_ENTRY_FPTR *_System _oss_dec_mreal_iap) (void *, _dstd_parms_defx, void *);

	unsigned int
	(DLL_ENTRY_FPTR *_System _oss_dec_eobjid_iap) (void *, _dstd_parms_defx, void *
		    , long
	);

	unsigned int
	(DLL_ENTRY_FPTR *_System _oss_dec_ereloid_iap) (void *, _dstd_parms_defx, void *
		    , long
	);

    long (DLL_ENTRY_FPTR *_System _oss_encd_bmpstr_iap) (void *, _std_parms_defx, unsigned short *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_dubit_iap) (void *, _std_parms_defx, unsigned char *, unsigned long, long, ossBoolean);
    long (DLL_ENTRY_FPTR *_System _oss_encd_dvbit_iap) (void *, _std_parms_defx, unsigned char *, unsigned long, long, ossBoolean);
    long (DLL_ENTRY_FPTR *_System _oss_encd_huge_int_iap) (void *, _std_parms_defx, unsigned char *, unsigned long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ubit_iap) (void *, _std_parms_defx, unsigned char *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uhuge_int_iap) (void *, _std_parms_defx, unsigned char *, unsigned long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_unistr_iap) (void *, _std_parms_defx, OSS_INT32 *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uoct_iap) (void *, _std_parms_defx, unsigned char *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ustr_iap) (void *, _std_parms_defx, char *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_utf8bmpstr_iap) (void *, _std_parms_defx, unsigned short *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_utf8unbound_iap) (void *, _std_parms_defx, unsigned char *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_utf8unistr_iap) (void *, _std_parms_defx, OSS_INT32 *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_vbit_iap) (void *, _std_parms_defx, unsigned char *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_voct_iap) (void *, _std_parms_defx, unsigned char *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_vstr_iap) (void *, _std_parms_defx, char *, unsigned long, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_mreal_iap) (void *, _std_parms_defx, enum MixedReal_kind, void *);
    long (DLL_ENTRY_FPTR *_System _oss_encd_eobjid_iap) (void *, _std_parms_defx, unsigned char *, unsigned int, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ereloid_iap) (void *, _std_parms_defx, unsigned char *, unsigned int, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_llobjid_iap) (void *, _std_parms_defx, void *, long, unsigned int);
    void * (DLL_ENTRY_FPTR *_System _oss_dec_llobjid_iap) (void *, _dstd_parms_defx, long, unsigned int);

    long (DLL_ENTRY_FPTR *_System _oss_encd_usobjid_iap) (void *, _std_parms_defx, unsigned short *, unsigned int, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uiobjid_iap) (void *, _std_parms_defx, unsigned int *, unsigned int, long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ulobjid_iap) (void *, _std_parms_defx, unsigned long *, unsigned int, long);

    unsigned short (DLL_ENTRY_FPTR *_System _oss_dec_usobjid_iap) (void *, _dstd_parms_defx, void *, short);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_dec_uiobjid_iap) (void *, _dstd_parms_defx, void *, short);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_dec_ulobjid_iap) (void *, _dstd_parms_defx, void *, short);

    unsigned short (DLL_ENTRY_FPTR *_System _oss_dec_asobjid_iap) (void *, _dstd_parms_defx, unsigned short *, short);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_dec_aiobjid_iap) (void *, _dstd_parms_defx, unsigned int *, short);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_dec_alobjid_iap) (void *, _dstd_parms_defx, unsigned long *, short);

    void * (DLL_ENTRY_FPTR *_System _oss_dec_asobjid_ptr_iap) (void *, _dstd_parms_defx, short, unsigned int);
    void * (DLL_ENTRY_FPTR *_System _oss_dec_aiobjid_ptr_iap) (void *, _dstd_parms_defx, short, unsigned int);
    void * (DLL_ENTRY_FPTR *_System _oss_dec_alobjid_ptr_iap) (void *, _dstd_parms_defx, short, unsigned int);

    long (DLL_ENTRY_FPTR *_System _oss_encd_tag_lengthp)(void *, _sdstd_parms_defx,
	long, OSS_UINT32);
    long (DLL_ENTRY_FPTR *_System _oss_encd_tag_length2p)(void *, _sdstd_parms_defx,
	long, OSS_UINT32, OSS_UINT32);
    long (DLL_ENTRY_FPTR *_System _oss_encd_bool_tlp)(void *, _sdstd_parms_defx,
	OSS_UINT32, ossBoolean);
    long (DLL_ENTRY_FPTR *_System _oss_encd_int_tlp)(void *, _sdstd_parms_defx,
	long, OSS_UINT32);
    long (DLL_ENTRY_FPTR *_System _oss_encd_uint_tlp)(void *, _sdstd_parms_defx,
	unsigned long, OSS_UINT32);
    long (DLL_ENTRY_FPTR *_System _oss_dec_tag_lengthp) (void *, _sdstd_parms_defx,
	unsigned long);
    long (DLL_ENTRY_FPTR *_System _oss_encd_llint_tlp)(void *, _sdstd_parms_defx,
	LONG_LONG, OSS_UINT32);
    long (DLL_ENTRY_FPTR *_System _oss_encd_ullint_tlp)(void *, _sdstd_parms_defx,
	ULONG_LONG, OSS_UINT32);
    long (DLL_ENTRY_FPTR *_System _oss_encd_pbit_tlp)(void *, _sdstd_parms_defx,
	OSS_UINT32, unsigned int, OSS_UINT32);
    long (DLL_ENTRY_FPTR *_System _oss_encd_date_timep)(void *, _std_parms_defx,
						    char *, int);
    void (DLL_ENTRY_FPTR *_System _oss_dec_date_timep)(void *, _dstd_parms_defx,
						    char **, int);
};

struct perTbl {
    int  (DLL_ENTRY_FPTR *_System ossPerEncodep)(void *, int, void *,
				void *, unsigned int);
    int  (DLL_ENTRY_FPTR *_System ossPerDecodep)(void *, int *, void *,
				void **, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_intp)(void *, LONG_LONG);
    LONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_intp)(void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_ntp_kmstrp)   (void *, char **, const void *);
    void (DLL_ENTRY_FPTR *_System _oss_appendp)           (void *, unsigned char *, unsigned long, int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_int16_alignedp)  (void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_append_8bit_alignedp) (void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_octp)(void *, unsigned char *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_constr_octp)  (void *, unsigned char *, ULONG_LONG, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_link_objidsp) (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_objidsp)      (void *, unsigned short *, unsigned long);
    void (DLL_ENTRY_FPTR *_System _oss_penc_link_objidlp) (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_objidlp)      (void *, unsigned long *, unsigned long);
    void (DLL_ENTRY_FPTR *_System _oss_penc_link_objidip) (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_objidip)      (void *, unsigned int *, unsigned long);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nkmstrp)      (void *, char *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_opentypep)    (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_intp)  (void *, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_realp)        (void *, double);
    void (DLL_ENTRY_FPTR *_System _oss_penc_uenump)       (void *, unsigned long, void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_penc_lengthp)(void *, ULONG_LONG, ULONG_LONG, ULONG_LONG, char);
    void (DLL_ENTRY_FPTR *_System _oss_penc_gtimep)       (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_utimep)       (void *, void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_bitp) (void *, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_ubitp)(void *, void *, unsigned char **, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_ubitp) (void *, void *, unsigned char **, int, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_vbit_ptrp)(void *, void **, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_vbitp)(void *, void *, unsigned char *, int, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_voctp) (void *, void *, unsigned char  *, int, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_uoctp)(void *, void *, unsigned char **, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_voct_ptrp)(void *, void **, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_voctp)(void *, void *, unsigned char  *, int, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_uoctp) (void *, void *, unsigned char **value, int, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_vbitp) (void *, void *, unsigned char *, int, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_link_objidsp) (void *, void **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_link_objidip) (void *, void **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_link_objidlp) (void *, void **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_uobjidsp)     (void *, unsigned short **, unsigned short *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_uobjidip)     (void *, unsigned int **, unsigned short *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_uobjidlp)     (void *, unsigned long **, unsigned short *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_aobjidsp)     (void *, unsigned short *, unsigned short *, unsigned short);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_aobjidip)     (void *, unsigned int *, unsigned short *, unsigned short);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_aobjidlp)     (void *, unsigned long *, unsigned short *, unsigned short);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_aobjids_ptrp) (void *, void **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_aobjidi_ptrp) (void *, void **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_aobjidl_ptrp) (void *, void **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_ntp_nkmstrp)  (void *, char **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_opentypep)    (void *, void *);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_intp)(void *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_get_bitsp)         (void *, unsigned char *, unsigned long, int);
    float  (DLL_ENTRY_FPTR *_System _oss_pdec_frealp)     (void *);
    double (DLL_ENTRY_FPTR *_System _oss_pdec_realp)      (void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_uenump)(void *, void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_lsofp)        (void *, unsigned long *, ULONG_LONG, ULONG_LONG, unsigned char, char *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_usofp)        (void *, unsigned long *, unsigned char **, int, long, ULONG_LONG, ULONG_LONG, unsigned char, char *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_asofp)        (void *, unsigned long *, int, ULONG_LONG, ULONG_LONG, unsigned char, char *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_gtimep)       (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_utimep)       (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_asof_ptrp)    (void *, void **, int, long, long, char *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_nt_kmstrp)    (void *, void *, const void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_va_kmstrp)    (void *, char *, const void *);
    double (DLL_ENTRY_FPTR *_System _oss_pdec_binrealp)   (void *, unsigned char, long);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_bpbitp)(void *, unsigned char *, int, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_pbitp_obsolete) (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_vbit_ptrp)(void *, void **, int, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_constr_voct_ptrp)(void *, void **, int, ULONG_LONG, ULONG_LONG);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_eapp)(void *, unsigned char **);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_easp)         (void *, unsigned char *, unsigned long, unsigned long);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_chrrealp)     (void *, unsigned char, long, double *, unsigned char *);
    long (DLL_ENTRY_FPTR *_System _oss_pdec_enump)        (void *, void *, void *);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_indeflen_intp)(void *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_nt_nkmstrp)   (void *, char *, unsigned long);
    char *(DLL_ENTRY_FPTR *_System _oss_pdec_crealp)      (void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_lengthp) (void *, unsigned long, unsigned long, ossBoolean *);
    MixedReal (DLL_ENTRY_FPTR *_System _oss_pdec_mrealp)  (void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_popp)        (void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_pushp)       (void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_uanyp)        (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_utf8unistrp)  (void *, OSS_INT32 *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_utf8unistrp)  (void *, void *, OSS_INT32 **, int);
    LONG_LONG  (DLL_ENTRY_FPTR *_System _oss_pdec_semicon_intp)(void *, LONG_LONG);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_semicon_uintp)(void *, ULONG_LONG);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_small_intp)(void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_small_lenp)(void *);
    long (DLL_ENTRY_FPTR *_System _oss_pdec_subidp)       (void *, long, long, long *, long *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_ub_nkmstrp)   (void *, void *, char **, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_bpbitp)(void *, unsigned char *, long);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_pbitp)(void *, void *, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_hugep)(void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_vap_nkmstrp)   (void *, void **, int);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_va_nkmstrp)    (void *, void *, char *, int, unsigned long);
    void (DLL_ENTRY_FPTR *_System _oss_penc_constr_bpbitp) (void *, void *, ULONG_LONG, ULONG_LONG, char, char);
    void (DLL_ENTRY_FPTR *_System _oss_penc_constr_pbitp)  (void *, ULONG_LONG, ULONG_LONG, ULONG_LONG, ULONG_LONG, char, char);
    void (DLL_ENTRY_FPTR *_System _oss_penc_crealp)        (void *, char *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_enump)         (void *, long, void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_indeflen_intp) (void *, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_mrealp)        (void *, MixedReal);
    void (DLL_ENTRY_FPTR *_System _oss_pop_globalp_obsolete)       (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_semicon_intp)  (void *, LONG_LONG, LONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_semicon_uintp) (void *, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_small_intp)    (void *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_small_lenp)    (void *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_subidp)        (void *, int, unsigned long, unsigned long *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_uanyp)         (void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_hugep)(void *, void *);

    void (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_pbitp_obsolete)(void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_penc_semicon_hugep) (void *, void *, LONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_semicon_hugep) (void *, void *, LONG_LONG);

    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_octetp)(void *, int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_eobjidp)       (void *, void *, long);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_eobjidp)       (void *, void *, long);
    void (DLL_ENTRY_FPTR *_System _oss_penc_ereloidp)      (void *, void *, long);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_ereloidp)      (void *, void *, long);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_sotp)          (void *);
    ossBoolean (DLL_ENTRY_FPTR *_System _oss_lnchkp)       (unsigned char *, ULONG_LONG *, unsigned short, ULONG_LONG, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_penc_utf8bmpstrp)   (void *, unsigned short *, ULONG_LONG);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_utf8bmpstrp)   (void *, void *, unsigned short **, int);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_copy_preamblep)(void *, unsigned char *, unsigned long, unsigned long, void *, unsigned char **, unsigned int);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_limited_intp)(void *, ULONG_LONG, LONG_LONG, LONG_LONG);
    LONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_limited_intp)(void *, LONG_LONG, LONG_LONG);
    LONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_semicon_limited_intp)  (void *, LONG_LONG, LONG_LONG, LONG_LONG);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_pdec_indeflen_limited_intp)(void *, ULONG_LONG, LONG_LONG, LONG_LONG, LONG_LONG);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_1bit_unalignedp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_2bit_unalignedp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_3bit_unalignedp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_4bit_unalignedp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_5bit_unalignedp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_6bit_unalignedp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_7bit_unalignedp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_get_8bit_unalignedp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_1intp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_2intp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_3intp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_4intp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_5intp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_6intp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_7intp)(void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_8intp)(void *);
    void (DLL_ENTRY_FPTR *_System _oss_append_1bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_append_2bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_append_3bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_append_4bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_append_5bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_append_6bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_append_7bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_append_8bit_unalignedp)(void *, unsigned char);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_1intp)(void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_2intp)(void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_3intp)(void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_4intp)(void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_5intp)(void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_6intp)(void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_7intp)(void *, unsigned int);
    void (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_8intp)(void *, unsigned int);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_indeflen_int_lp)(void *, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_indeflen_limited_int_lp)(void *, unsigned long, long, long, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_int_lp)(void *, unsigned int);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_nonneg_limited_int_lp)(void *, unsigned int, long, long);
    long          (DLL_ENTRY_FPTR *_System _oss_pdec_semicon_int_lp)(void *, long);
    long          (DLL_ENTRY_FPTR *_System _oss_pdec_semicon_limited_int_lp)(void *, long, long, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_semicon_uint_lp)(void *, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_small_int_lp)(void *);
    long          (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_int_lp)(void *);
    long          (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_limited_int_lp)(void *, long, long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_indeflen_int_lp)(void *, unsigned long, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_nonneg_int_lp)(void *, unsigned long, unsigned int);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_semicon_int_lp)(void *, long, long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_semicon_uint_lp)(void *, unsigned long, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_small_int_lp)(void *, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_int_lp)(void *, long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_constr_oct_lp)(void *, unsigned char *, unsigned long, unsigned long, unsigned long);

    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_constr_vbit_ptr_iap) (void *, void *, unsigned int, unsigned long, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_constr_voct_ptr_iap) (void *, void *, unsigned int, unsigned long, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_vbit_ptr_iap) (void *, void *, unsigned int);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_voct_ptr_iap) (void *, void *, unsigned int);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_vap_kmstrp) (void *, void *, unsigned int, const void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_vap_nkmstr_iap) (void *, void *, unsigned int);

	unsigned int
	(DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_huge_iap) (void *, void *);

    enum MixedReal_kind (DLL_ENTRY_FPTR *_System _oss_pdec_mreal_iap) (void *, void *);

	unsigned int
	(DLL_ENTRY_FPTR *_System _oss_pdec_semicon_huge_iap) (void *, void *
	      , long lb
		       );
	unsigned int
	(DLL_ENTRY_FPTR *_System _oss_pdec_eobjid_iap) (void *, void *
					    , long
	);

	unsigned int
	(DLL_ENTRY_FPTR *_System _oss_pdec_ereloid_iap) (void *, void *
					     , long
	);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_constr_ubit_iap) (void *, unsigned char **, unsigned long, unsigned long);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_constr_uoct_iap) (void *, unsigned char **, unsigned long, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_constr_vbit_iap) (void *, unsigned char *, unsigned long, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_constr_voct_iap) (void *, unsigned char *, unsigned long, unsigned long);
	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_bmpstrp) (void *, unsigned short **, const void *);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_unistrp) (void *, OSS_INT32 **, const void *);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_utf8bmpstr_iap) (void *, unsigned short **);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_utf8unistr_iap) (void *, OSS_INT32 **);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_ub_kmstrp) (void *, char **, const void *);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_ub_nkmstr_iap) (void *, char **);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_ubit_iap) (void *, unsigned char **);

	unsigned long
	(DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_uoct_iap) (void *, unsigned char **);

    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_vbit_iap) (void *, unsigned char *, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_unconstr_voct_iap) (void *, unsigned char *, unsigned long);

    unsigned long (DLL_ENTRY_FPTR *_System _oss_pdec_va_nkmstr_iap) (void *, char *, unsigned long);

    void (DLL_ENTRY_FPTR *_System _oss_penc_mreal_iap) (void *, enum MixedReal_kind, void *);

    void (DLL_ENTRY_FPTR *_System _oss_penc_semicon_huge_iap) (void *, unsigned char *, unsigned long
    , long lb
	     );
    void (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_huge_iap) (void *, unsigned char *, unsigned long);
    void (DLL_ENTRY_FPTR *_System _oss_penc_eobjid_iap) (void *, unsigned char *, unsigned int, long);
    void (DLL_ENTRY_FPTR *_System _oss_penc_ereloid_iap) (void *, unsigned char *, unsigned int, long);

    void          (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_pbit_lp)(void *, unsigned long, unsigned long, unsigned long, char);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_oct_lp)(void *, unsigned char *, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_unconstr_bit_lp)(void *, unsigned char *, unsigned long, char);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_small_len_lp)(void *, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_nkmstr_lp)(void *, char *, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_penc_length_lp)(void *, unsigned long, unsigned long, unsigned long, char);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_kmstrp)(void *, char *, unsigned long, const void *, unsigned int);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_constr_pbit_lp)(void *, unsigned long, unsigned long, unsigned long, unsigned long, char, char);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_constr_bpbit_lp)(void *, void *, unsigned long, unsigned long, char, char);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_constr_bit_lp)(void *, unsigned char *, unsigned long, unsigned long, unsigned long, char, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_utf8bmpstr_lp)(void *, unsigned short *, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_utf8unistr_lp)(void *, OSS_INT32 *, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_bmpstrp)(void *, unsigned short *, unsigned long, const void *, unsigned int);
    void          (DLL_ENTRY_FPTR *_System _oss_penc_unistrp)(void *, OSS_INT32 *, unsigned long, const void *, unsigned int);
    void          (DLL_ENTRY_FPTR *_System _oss_pdec_pad_kmstrp)(void *, void *, const void *);
    void          (DLL_ENTRY_FPTR *_System _oss_pdec_lsof_lp)(void *, unsigned long *, unsigned long, unsigned long, unsigned char, char *);
    void          (DLL_ENTRY_FPTR *_System _oss_pdec_constr_pbit_lp)(void *, void *, int, unsigned long, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_pdec_constr_bpbit_lp)(void *, unsigned char *, int, unsigned long, unsigned long);
    void          (DLL_ENTRY_FPTR *_System _oss_pdec_asof_ptr_iap)(void *, void *, int, long, unsigned int, ossBoolean *);

    void (DLL_ENTRY_FPTR *_System _oss_penc_link_objidl_iap)(void *, void *, unsigned int);
    void * (DLL_ENTRY_FPTR *_System _oss_pdec_link_objidl_iap)(void *, unsigned int);

    void * (DLL_ENTRY_FPTR *_System _oss_pdec_aobjids_ptr_iap)(void *, unsigned int);
    void * (DLL_ENTRY_FPTR *_System _oss_pdec_aobjidi_ptr_iap)(void *, unsigned int);
    void * (DLL_ENTRY_FPTR *_System _oss_pdec_aobjidl_ptr_iap)(void *, unsigned int);


    void (DLL_ENTRY_FPTR *_System _oss_fast_push_globalp)   (void *);
    void (DLL_ENTRY_FPTR *_System _oss_fast_pop_globalp)    (void *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_pad_nkmstrp)    (void *, char *, unsigned long);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_array_frgp)(void *, void *,
				unsigned long, unsigned long, unsigned int, ossBoolean *);
    void (DLL_ENTRY_FPTR *_System _oss_pdec_unbnd_frgp)(void *, void *,
				unsigned long, unsigned long, ossBoolean *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_any_centuryp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_any_yearp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_centuryp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_duration_intervalp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_hoursp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_mixedp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_recurrencep)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_yearp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_monthp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_day_of_monthp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_day_of_yearp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_day_of_weekp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_weekp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_mins_or_secsp)(void *,
							char *, unsigned int);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_time_diffp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pe_t_time_fractionp)(void *, char *, unsigned int);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_any_centuryp)(void *, char *, unsigned int);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_any_yearp)(void *, char *, unsigned int);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_centuryp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_duration_intervalp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_hoursp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_mixedp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_recurrencep)(void *, char *, unsigned int);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_yearp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_monthp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_day_of_monthp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_day_of_yearp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_day_of_weekp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_weekp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_mins_or_secsp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_time_diffp)(void *, char *);
    char * (DLL_ENTRY_FPTR *_System _oss_pd_t_time_fractionp)(void *, char *, unsigned int);
};

struct _xerTbl {
    int  (DLL_ENTRY_FPTR *_System ossXerEncodep)(void *, int, void *,
				void *, unsigned int);
    int  (DLL_ENTRY_FPTR *_System ossXerDecodep)(void *, int *, void *,
				void **, unsigned int);
    long	(DLL_ENTRY_FPTR *_System  _oss_tex_appendp)(void *, unsigned char *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_format_tagp)(void *, char *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_indentp)(void *);
    long	(DLL_ENTRY_FPTR *_System  _oss_tex_count_bitsp)(unsigned char *, long);
    long	(DLL_ENTRY_FPTR *_System  _oss_tex_count_charsp)(char *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_lintp)(void *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_ulintp)(void *, unsigned long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_llintp)(void *, LONG_LONG);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_ullintp)(void *, ULONG_LONG);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_hintp)(void *, unsigned char *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_timep)(void *, void *, char);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_enump)(void *, void *, long, int);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_format_enump)(void *, char *, int);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_drealp)(void *, double);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_frealp)(void *, float);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_crealp)(void *, char *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_eobjidp)(void *, unsigned char *, long, long, ossBoolean);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_aobjidsp)(void *, unsigned short *, long, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_aobjidip)(void *, unsigned int *, long, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_aobjidlp)(void *, unsigned long *, long, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_lobjidsp)(void *, void *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_lobjidip)(void *, void *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_lobjidlp)(void *, void *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_format_charp)(void *, unsigned char *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_format_bitp)(void *, unsigned char *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_format_octetp)(void *, unsigned char *, unsigned long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_prologp)(void *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_opentypep)(void *, void *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_2byte_charp)(void *, unsigned short *, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_4byte_charp)(void *, OSS_UINT32 *, long);

    void (DLL_ENTRY_FPTR *_System _oss_tdx_open_tagp)(void *, char *);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_empty_tagp)(void *, char *);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_close_tagp)(void *, char *);
    ossBoolean (DLL_ENTRY_FPTR *_System _oss_tdx_open_or_empty_tagp)(void *, char *);
    ossBoolean (DLL_ENTRY_FPTR *_System _oss_tdx_open_fieldp)(void *, void *, short *);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_skip_blockp)(void *);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_prologp)(void *);
    unsigned char (DLL_ENTRY_FPTR *_System _oss_tdx_lexem_lookaheadp)(void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_allocate_array_sofp)(void *, void **, int, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_allocate_unbounded_sofp)(void *, void **, long, OSS_UINT32, OSS_UINT32);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_opentypep)(void *, void *, OSS_UINT32);
    ossBoolean (DLL_ENTRY_FPTR *_System _oss_tdx_boolp)(void *);
    OSS_INT32 (DLL_ENTRY_FPTR *_System _oss_tdx_enump)(void *, void *);
    LONG_LONG (DLL_ENTRY_FPTR *_System _oss_tdx_llintp)(void *);
    ULONG_LONG (DLL_ENTRY_FPTR *_System _oss_tdx_ullintp)(void *);
    long (DLL_ENTRY_FPTR *_System _oss_tdx_lintp)(void *, long, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_ulintp)(void *, unsigned long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_hintp)(void *, void **, OSS_UINT32);
    double (DLL_ENTRY_FPTR *_System _oss_tdx_drealp)(void *);
    float (DLL_ENTRY_FPTR *_System _oss_tdx_frealp)(void *);
    char * (DLL_ENTRY_FPTR *_System _oss_tdx_crealp)(void *);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_mrealp)(void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_char1_nulltermp)(void *, void *, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_char1_unboundedp)(void *, void **, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_char1_pvaryingp)(void *, void **, int, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_char1_varyingp)(void *, void *, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_char2p)(void *, void **, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_char4p)(void *, void **, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_octets_unboundedp)(void *, void **, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_octets_pvaryingp)(void *, void **, int, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_octets_varyingp)(void *, void *, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_bits_unboundedp)(void *, void **, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_bits_pvaryingp)(void *, void **, int, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_bits_varyingp)(void *, void *, OSS_UINT32, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_bits_paddedp)(void *, int, OSS_UINT32);
    OSS_UINT32 (DLL_ENTRY_FPTR *_System _oss_tdx_objid_encodedp)(void *, void *, OSS_UINT32, OSS_UINT32);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_objid_linkedp)(void *, void **, unsigned short, OSS_UINT32, OSS_UINT32);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_tdx_objid_unboundedp)(void *, void **, unsigned short, OSS_UINT32, OSS_UINT32);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_tdx_objid_arrayp)(void *, void *, OSS_UINT32, unsigned short);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_tdx_objid_parrayp)(void *, void **, int, unsigned short);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_gtimep)(void *, void *);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_utimep)(void *, void *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_open_tagp)(void *, char *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_close_tagp)(void *, char *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_empty_or_id_tagp)(void *, char *);
    long	(DLL_ENTRY_FPTR *_System  _oss_tex_curr_enclenp)(void *);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_indent_dltp)(void *, int);
    void (DLL_ENTRY_FPTR *_System _oss_tex_format_pbitp)(void *, OSS_UINT32, long);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_open_tagNp)(void *, char *, int);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_close_tagNp)(void *, char *, int);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_empty_or_id_tagNp)(void *, char *, int);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_format_tagNp)(void *, char *, int);
    void	(DLL_ENTRY_FPTR *_System  _oss_tex_anyopentypep)(void *, unsigned char *, long);
    void        (DLL_ENTRY_FPTR *_System  _oss_allocate_memblockp)(void *, void **, void **, long);
    void        (DLL_ENTRY_FPTR *_System  _oss_consolidate_memblockp)(void *, void **, long, long,
				void *, long, long, long, int, OSS_UINT32, OSS_UINT32);
    void        (DLL_ENTRY_FPTR *_System  _oss_tex_format_char_tp)(void *, unsigned char *, long);
    void        (DLL_ENTRY_FPTR *_System  _oss_tex_ntimep)(void *, char *, size_t, char);
    enum MixedReal_kind (DLL_ENTRY_FPTR *_System _oss_tdx_mreal_iap)(void *, void *);
    void *      (DLL_ENTRY_FPTR *_System  _oss_consolidate_memblock_fastp)(void *,
					    long, long, void *, long, long, long, int);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdx_rsf_altp)(void *, void **);
    void 	(DLL_ENTRY_FPTR *_System _oss_tdx_rsf_extp)(void *, void **, unsigned int *);
    void (DLL_ENTRY_FPTR *_System _oss_tex_date_timep)(void *, char *, int);
    void (DLL_ENTRY_FPTR *_System _oss_tdx_date_timep)(void *, char **, int);
};

struct _exerTbl {
    int  (DLL_ENTRY_FPTR *_System ossEXEREncodep)(void *, int, void *,
				void *, unsigned int);
    int  (DLL_ENTRY_FPTR *_System ossEXERDecodep)(void *, int *, void *,
				void **, unsigned int);
#ifdef OSS_TOED_EXER
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_init_envp)(void *env,
                                unsigned char *, unsigned long);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_get_lexp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_get_lex_collapsep)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_get_tagp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_errorp)(void *,
                                enum _err_index, void *, OSS_UINT32 flags);
    int         (DLL_ENTRY_FPTR *_System _oss_tlex_prolog_and_tagp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_check_close_tagp)(void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char1_listp)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char2_listp)(void *,
                                OSS_UINT16 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char4_listp)(void *,
                                OSS_INT32 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char1_collapsep)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char2_collapsep)(void *,
                                OSS_UINT16 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char4_collapsep)(void *,
                                OSS_INT32 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char1_replacep)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char2_replacep)(void *,
                                OSS_UINT16 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char4_replacep)(void *,
                                OSS_INT32 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char1_preservep)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char2_preservep)(void *,
                                OSS_UINT16 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char4_preservep)(void *,
                                OSS_INT32 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char1_64p)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char2_64p)(void *,
                                OSS_UINT16 **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_char4_64p)(void *,
                                OSS_INT32 **, void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_tdex_intp)(void *, void *);
    ULONG_LONG  (DLL_ENTRY_FPTR *_System _oss_tdex_int_llp)(void *,
                                void *);
    ossBoolean  (DLL_ENTRY_FPTR *_System _oss_tdex_boolp)(void *,
                                void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_int_hugep)(void *,
                                unsigned char **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_octp)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_oct_64p)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_otyp_64p)(void *,
                                unsigned char **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_otypp)(void *,
                                unsigned char **, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_bitp)(void  *,
                                void *, void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_tdex_enump)(void  *, void *);
    int         (DLL_ENTRY_FPTR *_System _oss_tdex_nilp)(void *);
    int                 (DLL_ENTRY_FPTR *_System _oss_tdex_find_typep)(void *,
                                void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_mark_typep)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_error_typep)(void *, void *);
    int         (DLL_ENTRY_FPTR *_System _oss_tlex_match_tagp)(void *,
                                unsigned char *, unsigned long);
    int         (DLL_ENTRY_FPTR *_System _oss_tlex_match_qtagp)(void *,
                                unsigned char *,
                                unsigned char *, unsigned long);
    int         (DLL_ENTRY_FPTR *_System _oss_tlex_match_wtagp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_match_tag_errorp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_read_wordp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_check_endp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_check_end_unionp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_skip_wsp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_check_wsp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_free_attrsp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_store_inputp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_set_attr_inputp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_restore_inputp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_set_embedp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_read_textp)(void *,
                                void *, OSS_UINT32);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tlex_resolve_prefixp)(void *,
                                void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tlex_copy_char1p)(void *,
                                unsigned char **, unsigned char *,
                                unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_free_namep)(void *);
    OSS_UINT16  (DLL_ENTRY_FPTR *_System _oss_tlex_find_idxp)(void *,
                                void *, void *);
    OSS_UINT16  (DLL_ENTRY_FPTR *_System _oss_tlex_find_tag_idxp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_start_unionp)(void *,
                                void *, unsigned long);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_retry_unionp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_finish_unionp)(void *, void *);
    int         (DLL_ENTRY_FPTR *_System _oss_tlex_wildcard_matchp)(void *,
                                unsigned char *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_anyelp)(void *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_anyattrp)(void *,
                                void *, void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_timep)(void  *,
                                void *, void *);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_tdex_oid_encp)(void *,
                                void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_oid_lnkp)(void *,
                                void **, void *);
    unsigned short (DLL_ENTRY_FPTR *_System _oss_tdex_oidp)(void  *,
                                void *, void *);
    float       (DLL_ENTRY_FPTR *_System _oss_tdex_real_floatp)(void *, void *);
    double      (DLL_ENTRY_FPTR *_System _oss_tdex_real_doublep)(void *, void *);
    char        *(DLL_ENTRY_FPTR *_System _oss_tdex_real_charp)(void *, void *);
    enum MixedReal_kind (DLL_ENTRY_FPTR *_System _oss_tdex_real_mixedp)(void *,
                                void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_skip_typep)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_skip_contentp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_push_inputp)(void *, void *,
                                unsigned char *, unsigned long);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_pop_inputp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tlex_mark_ext_attrp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_tdex_iso_timep)(void *,
                                char **, void *);

    void        (DLL_ENTRY_FPTR *_System _oss_teex_prologp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_init_envp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_appendp)(void *,
                                unsigned char *, long);
    void	(DLL_ENTRY_FPTR *_System _oss_teex_append_bytep)(void *,
                                unsigned int);
    char *	(DLL_ENTRY_FPTR *_System _oss_teex_seal_tagp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_open_qtagp)(void *,
                                void *, int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_open_tagp)(void *,
                                unsigned char *, long, int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_close_qtagp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_close_tagp)(void *,
                                unsigned char *, long);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_empty_qtagp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_id_or_empty_tagp)(void *,
                                unsigned char *, unsigned int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_end_tagp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_qattr_beginp)(void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_attr_beginp)(void *,
                                unsigned char *, long);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_indentp)(void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_type_attrp)(void *,
				unsigned int, unsigned char *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_set_dnsp)(void *,
                                unsigned int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_xmlns_attrp)(void *,
                                unsigned int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_nil_attrp)(void *,
                                unsigned int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_set_embedp)(void *,
                                void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_chklst_char1p)(void *,
                                unsigned char *, unsigned long);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char1_collapsep)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char1_replacep)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char1p)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char2p)(void *,
                                OSS_UINT16 *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char4p)(void *, OSS_INT32 *,
                                unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char1_64p)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char2_64p)(void *,
                                OSS_UINT16 *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char4_64p)(void *,
                                OSS_INT32 *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char2_collapsep)(void *,
                                OSS_UINT16 *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char2_replacep)(void *,
                                OSS_UINT16 *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_chklst_char2p)(void *,
                                OSS_UINT16 *, unsigned long);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char4_collapsep)(void *,
                                OSS_INT32 *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_char4_replacep)(void *,
                                OSS_INT32 *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_chklst_char4p)(void *,
                                OSS_INT32 *, unsigned long);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_boolp)(void *, int, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_intp)(void *,
                                unsigned int, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_int_llp)(void *,
                                ULONG_LONG, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_int_hugep)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_enump)(void *,
                                unsigned int, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_octp)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oct_64p)(void *,
                                unsigned char *, unsigned long, void *);
    void	(DLL_ENTRY_FPTR *_System _oss_teex_enc_base64_stringp)(void *,
				unsigned char *, unsigned long);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_bitp)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_bit_padp)(void *,
                                OSS_UINT32 value, unsigned int, void *);
    unsigned int (DLL_ENTRY_FPTR *_System _oss_teex_check_urip)(void *,
                                unsigned char *, unsigned int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_check_attrp)(void *,
                                unsigned char *, unsigned int,
                                unsigned char *, unsigned int, int);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_errorp)(void *,
                                enum _err_index, long, char *);
    unsigned char * (DLL_ENTRY_FPTR *_System _oss_teex_qname_prefixp)(void *,
                                unsigned char *, size_t, int *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_qname_namep)(void *,
                                unsigned char *, size_t,
                                unsigned char *, int );
    void        (DLL_ENTRY_FPTR *_System _oss_teex_anyelp)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_anyattrp)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_timep)(void *, void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_time_nulltp)(void *,
                                char *data, size_t inlen, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_iso_timep)(void *,
                                char *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_anyopentypep)(void *,
                                unsigned char *, long);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_otypp)(void *, void *,
                                void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_otyp_64p)(void *, void *,
                                void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oid_encp)(void *,
                                unsigned char *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oid_unb_sp)(void *,
                                unsigned short *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oid_unb_ip)(void *,
                                unsigned int *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oid_unb_lp)(void *,
                                unsigned long *, unsigned long, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oid_lnk_sp)(void *,
                                void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oid_lnk_ip)(void *,
                                void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_oid_lnk_lp)(void *,
                                void *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_real_charp)(void *,
                                char *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_real_decimalp)(void *,
                                char *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_real_char_modp)(void *,
                                char *, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_real_doublep)(void *,
                                double, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_real_floatp)(void *,
                                float, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_real_double_modp)(void *,
                                double, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_real_float_modp)(void *,
                                float value, void *);
    void        (DLL_ENTRY_FPTR *_System _oss_teex_db_e_prep)(void *,
                                int, void *, char *, long);
    unsigned long (DLL_ENTRY_FPTR *_System _oss_teex_curr_enclenp)(void *);
    void	(DLL_ENTRY_FPTR *_System _oss_tstk_add_blockp)(void *, size_t);
    void	(DLL_ENTRY_FPTR *_System _oss_tstk_free_blocksp)(void *, void *);
    void *	(DLL_ENTRY_FPTR *_System _oss_tstk_allocp)(void *, size_t);
    void	(DLL_ENTRY_FPTR *_System _oss_tstk_freep)(void *, void *);
    unsigned char * (DLL_ENTRY_FPTR *_System _oss_tstk_new_fragment_tagp)(void *,
				size_t);
    unsigned char * (DLL_ENTRY_FPTR *_System _oss_tstk_get_altdecmemp)(void *,
				size_t);
    void	(DLL_ENTRY_FPTR *_System _oss_tstk_consolidate_altdecmemp)(void *,
    				void *, size_t, size_t);
    void	(DLL_ENTRY_FPTR *_System _oss_tstk_consolidate_outmemp)(void *,
				void *, void *, size_t);
    void	(DLL_ENTRY_FPTR *_System _oss_tstk_consolidate_array_tagp)(void *,
        			unsigned char *, void *, size_t);
    void (DLL_ENTRY_FPTR *_System _oss_tstk_free_altdecmemp)(void *, void *);
#endif /* OSS_TOED_EXER */
};

struct cpyvalTbl {
    int (DLL_ENTRY_FPTR *_System ossCpyValuep)(void *, int, void *, void **);
    void (DLL_ENTRY_FPTR *_System exer_copy_itp)(void *, void *, void **, void*, void*);
};

struct cmpvalTbl {
    int (DLL_ENTRY_FPTR *_System ossCmpValuep)(void *, int, void *, void *);
    int (DLL_ENTRY_FPTR *_System oss_compare_themp)(void *, void *, void *, void *);
};

struct berrealTbl {
    long (DLL_ENTRY_FPTR *_System ossBerEncodeRealp)(void *, void *,
							unsigned char *);
    long (DLL_ENTRY_FPTR *_System ossBerDecodeRealp)(void *, void *,
								long, char);
    void (DLL_ENTRY_FPTR *_System ossPrintRealp)(void *, void *,
							unsigned char *);
};

struct perrealTbl {
    void (DLL_ENTRY_FPTR *_System ossPerEncodeRealp)(void *, void *);
    void (DLL_ENTRY_FPTR *_System ossPerDecodeRealp)(void *, void *);
    void (DLL_ENTRY_FPTR *_System ossPrintRealp)(void *, void *,
							unsigned char *);
};

struct zlibTbl {
    int (DLL_ENTRY_FPTR *_System ossZlibCompressEncodingp)(void *, void *, void *, 
					void *, unsigned long, void *);
    int (DLL_ENTRY_FPTR *_System ossZlibDecompressEncodingp)(void *, void *, void *, 
					void *, unsigned long, void *);
};


struct printPerTbl {
    int (DLL_ENTRY_FPTR *_System ossPrintPerp)(void *, int *,void *, void **, long, void *);
    int (DLL_ENTRY_FPTR *_System ossPrintXPerp)(void *, int *,void *, void **, long, void *);
    int (DLL_ENTRY_FPTR *_System ossPrintPerDecodingp)(void *, void *, char **, void *, void *, long *, void *, unsigned, void *, long, void *, void *, int);
};

struct oidTbl {
int (DLL_ENTRY_FPTR *_System ossEncodedOidToAsnValp)(void *,
						const void *, void *);
int (DLL_ENTRY_FPTR *_System ossEncodedOidToDotValp)(void *,
						const void *, void *);
int (DLL_ENTRY_FPTR *_System ossAsnValToEncodedOidp)(void *,
						const char *, void *);
int (DLL_ENTRY_FPTR *_System ossDotValToEncodedOidp)(void *,
						const char *, void *);
int (DLL_ENTRY_FPTR *_System ossEncodedRelOidToAsnValp)(void *,
						const void *, void *);
int (DLL_ENTRY_FPTR *_System ossEncodedRelOidToDotValp)(void *,
						const void *, void *);
int (DLL_ENTRY_FPTR *_System ossAsnValToEncodedRelOidp)(void *,
						const void *, void *);
int (DLL_ENTRY_FPTR *_System ossDotValToEncodedRelOidp)(void *,
						const void *, void *);
int (DLL_ENTRY_FPTR *_System oid_e2cp)(void *, const void *,
						void *, short );
};
#endif /* _WINDOWS || _WIN32 || __OS2__ || NETWARE_DLL */



    /*************************************************************************/
    /*      General functions section                                        */
    /*************************************************************************/

extern void *DLL_ENTRY ossGetHeader(void);
PUBLIC void DLL_ENTRY ossInitRootContext(struct ossGlobal *, unsigned char *);
PUBLIC void DLL_ENTRY ossInitRootContext1(struct ossGlobal *, unsigned char *);

#if defined(_WINDOWS) || defined(_DLL) || \
	defined(OS2_DLL) || defined(NETWARE_DLL)
PUBLIC void DLL_ENTRY ossLinkAPI(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkOid(struct ossGlobal *);
#endif /* _WINDOWS || _DLL || OS2_DLL || NETWARE_DLL */

PUBLIC void DLL_ENTRY ossLinkBer(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkDer(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkPer(struct ossGlobal *);

PUBLIC void DLL_ENTRY ossLinkXer (struct ossGlobal *);

PUBLIC void DLL_ENTRY ossLink_Exer (OssGlobal *, void *);

PUBLIC unsigned long DLL_ENTRY ossGetPresetDecFlags(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossSetPresetDecFlags(struct ossGlobal *, unsigned long);

PUBLIC void DLL_ENTRY ossPrivateSetMoreFlags(struct ossGlobal *, int,
							unsigned char *);
PUBLIC void DLL_ENTRY ossPrivateGetMoreFlags(struct ossGlobal *world, int *,
							unsigned char **);
extern int   DLL_ENTRY _oss_cmp_date_time(OssGlobal *, char *, char *);
extern char *DLL_ENTRY _oss_parse_duration(char *, ossDuration *, ossBoolean);
extern char *DLL_ENTRY _ossStrToUintUnlimited(char *, unsigned int *);
extern char *DLL_ENTRY _ossStrToUintLimited(char *, unsigned int, unsigned int *);
extern char *DLL_ENTRY _ossUintToStrUnlimited(char *, unsigned int, unsigned int);
extern char *DLL_ENTRY _ossUintToStrLimited(char *, unsigned int, unsigned int);



#if !defined(_OSS_CODE_FILE) || !defined(DLL_LINKAGE)
PUBLIC void DLL_ENTRY _oss_db_e_pre(struct ossGlobal *world,
	int pdunum, void *input, unsigned int *encFlags,
	char *bufpos, long buflen);
PUBLIC void DLL_ENTRY _oss_db_e_post(struct ossGlobal *world,
	int ret_code, char *outbuf, long outlen);
PUBLIC void DLL_ENTRY _oss_db_d_pre(struct ossGlobal *world,
	char *inbuf, long inlen, unsigned int *decFlags);
PUBLIC void DLL_ENTRY _oss_db_d_post(struct ossGlobal *world,
	int ret_code, int pdunum, void **output);
#endif /* !_OSS_CODE_FILE || !DLL_LINKAGE */


/*****************************************************************************/
/*  COMMON EXER section                                                        */
/*****************************************************************************/

typedef struct {
    OSS_UINT16 _oss_qJ;
    void *_oss_WJ;			
} exer_search;
typedef struct {
    unsigned char *_oss_QJ;
    OSS_UINT16 _oss_w;	    
} exer_table_entry;
typedef struct {
    OSS_UINT16 _oss_W;
    exer_table_entry *_oss_J;	
} exer_binary_table;
typedef struct {
    unsigned char *_oss_JJ;
    OSS_UINT16 _oss_wJ;		
    OSS_UINT16 _oss_w;
} exer_hash_table_entry;
typedef struct {
    exer_hash_table_entry *_oss_J;
    OSS_UINT32 _oss_q;
    OSS_UINT16 _oss_W;
} exer_hash_table;
typedef struct {
    OSS_UINT16 flags;
    unsigned char *uri;
    unsigned char *prefix;
} exer_namespace;

typedef struct {
    void         *_oss_Qj;
    OSS_UINT32    _oss_WjJ;
} exer_extensions_list;
typedef struct {
    OSS_UINT32 _oss_JW;
    void *_oss_jjJ;		
    unsigned char *_oss_WwJ;	
    OSS_UINT16 _oss_HWW;		
    OSS_UINT16 _oss_HQW;	
} exer_tag_decoder;
typedef struct {
    OSS_UINT32 _oss_JW;
    exer_tag_decoder *_oss_JQW;
    OSS_UINT32 _oss_QwW;		
} exer_decode_position;
typedef struct {
    unsigned char *_oss_wq;
    OSS_UINT16 _oss_Hj;
} exer_uri_test;
typedef struct {
    exer_search _oss_jJJ;		
    exer_uri_test *_oss_QqJ;	
} exer_qname_table;
typedef struct {
    unsigned char *_oss_wq;
    OSS_UINT16 _oss_wj;
    OSS_UINT16 _oss_Hj;		
} exer_uri_table_entry;
typedef struct {
    OSS_UINT32 _oss_JJJ;		    
    exer_uri_table_entry *_oss_Qj;    
    exer_search *_oss_wQW;	    
} exer_uri_table;
typedef struct {
    OSS_UINT16 _oss_wj;
    OSS_UINT16 _oss_JJJ;	
    void *_oss_wWJ;		
} exer_enum_table;
typedef struct {
    exer_enum_table _oss_WWW;	
    exer_search _oss_QjJ;	
    OSS_UINT32 _oss_QwJ;         
} exer_enum_data;
typedef struct {
    OSS_INT32 _oss_HWJ;		
    unsigned char *_oss_qW;	
} exer_enum_value;
typedef struct {
    LONG_LONG _oss_HWJ;		
    unsigned char *_oss_qW;	
} exer_long_enum_value;
typedef struct {
    OSS_UINT16 _oss_JW;
    exer_search _oss_jJJ;		
} exer_wild_data;
typedef struct {
    OSS_UINT32		_oss_JW;
    exer_enum_data	*_oss_jw;
} exer_bool;
typedef struct {
    OSS_UINT32		_oss_JW;
    exer_enum_data	*_oss_jw;
    unsigned int	_oss_jwW;
    unsigned int	_oss_WJW;
} exer_int;
typedef struct {
    OSS_UINT32		_oss_JW;
    exer_enum_data	*_oss_jw;
} exer_int_ll;
typedef struct {
    OSS_UINT32		_oss_JW;
} exer_int_huge;
typedef struct {
    OSS_UINT32		_oss_JW;
    exer_enum_data	*_oss_jw;
    exer_enum_value 	*_oss_qJW;
} exer_enum;
typedef struct {
    OSS_UINT32		_oss_JW;
    exer_enum_data	*_oss_jw;
    unsigned long	_oss_HqJ;
    unsigned long	max_length;
    unsigned int	_offset;
    unsigned long       _oss_JjJ;
} exer_bit;
typedef struct {
    OSS_UINT32		_oss_JW;
    unsigned long	max_length;
    unsigned int	_offset;
} exer_oct;
typedef struct {
    OSS_UINT32		_oss_JW;
    unsigned long	max_length;
    unsigned int  	_oss_wJW;
    unsigned int	_offset;
} exer_oid;
typedef struct {
    OSS_UINT32		_oss_JW;
    unsigned long	max_length;
    unsigned int	_offset;
} exer_char1;
typedef struct {
    OSS_UINT32		_oss_JW;
    unsigned long	max_length;
} exer_char2;
typedef struct {
    OSS_UINT32		_oss_JW;
    unsigned long	max_length;
} exer_char4;
typedef struct {
    OSS_UINT32		_oss_JW;
} exer_time;
typedef struct {
    OSS_UINT32		_oss_JW;
    int			_oss_wj;
} exer_iso_time;
typedef struct {
    OSS_UINT32		_oss_JW;
} exer_otyp;
typedef struct {
    OSS_UINT32		_oss_JW;
} exer_real;
#define OSS_EX_LIST              	0x00000001
#define OSS_EX_ATTRIBUTE 		0x00000002
#define OSS_EX_DYNAMIC_BUF 		0x00000004
#define OSS_EX_UNION	 		0x00000008
#define OSS_EX_KEEP_ATTR		0x00000010
#define OSS_EX_EXPLICIT_PDUNUM		0x00000020
#define OSS_EX_DFE			0x00000040
#define OSS_EX_OPTIMIZE_XMLNS		0x00000080
#define OSS_EX_FSA_PPTR_REF		0x00000100
#define OSS_EX_FSA_CNS_OFF		0x00000200
#define OSS_EX_NS_USED			0x00000001
#define OSS_EX_VALUE_IN_QUOTE		0x00000001
#define OSS_EX_ATTR_HANDLED		0x00000002
#define OSS_EX_ATTR_IN_EXT_TYPE		0x00000004
#define OSS_EX_TYPE_ATTR_HANDLED	0x00000008
#ifdef _OSS_CODE_FILE
#define OSS_EX_AS_LONG			0x00000000
#define OSS_EX_AS_BYTE			0x00000001
#define OSS_EX_AS_INT32			0x00000002
#define OSS_EX_AS_LLONG			0x00000004
#define OSS_EX_AS_NULLTERM		0x00000008
#define OSS_EX_AS_NAME			0x00000010
#define OSS_EX_AS_BUF			0x00000020
#define OSS_EX_NO_CHECK			0x00000040
#define OSS_EX_NAME_IS_LOCAL(name, nm, nm_len) \
    (name._uri == NULL && name._name._buflen == (nm_len) && \
	!memcmp(name._name._buf_store, nm, nm_len))
#define OSS_EX_NAME_IS_QUAL(name, uri, nm, nm_len) \
    (name._uri == uri && \
	(name._name._buflen - (name._colon_pos + 1)) == (nm_len) && \
	    !memcmp(name._name._buf_store +(name._colon_pos + 1), nm, nm_len))
#define OSS_EX_IS_PRESENT(mask, bit, erridx, edata, edtype) \
    if ((mask) & (bit)) \
	_oss_tdex_error(_env, erridx, edata, edtype); \
    (mask) |= (bit);
#define OSS_EX_FLD_IS_PRESENT(mask, bit) \
    OSS_EX_IS_PRESENT(mask, bit, _field_repeat, NULL, 0)
#define OSS_EX_ATTR_IS_PRESENT(mask, bit) \
    OSS_EX_IS_PRESENT(mask, bit, _xml_attr_repeated, &_attr->_name, OSS_EX_AS_NAME)
#define OSS_EX_IS_PRESENT_ARR(mask, n, bit, erridx, edata, edtype) \
    if ((mask)[n] & (bit)) \
	_oss_tdex_error(_env, erridx, edata, edtype); \
    (mask)[n] |= (bit);
#define OSS_EX_FLD_IS_PRESENT_ARR(mask, n, bit) \
    OSS_EX_IS_PRESENT_ARR(mask, n, bit, _field_repeat, NULL, 0)
#define OSS_EX_ATTR_IS_PRESENT_ARR(mask, n, bit) \
    OSS_EX_IS_PRESENT_ARR(mask, n, bit, _xml_attr_repeated, &_attr->_name, OSS_EX_AS_NAME)
#define OSS_EX_IS_PRESENT_UNT(mask, bit) \
    if ((mask) & (bit)) { \
	_f_idx = 0xffff; \
	break; \
    } \
    (mask) |= (bit);
#define OSS_EX_IS_PRESENT_ARR_UNT(mask, n, bit) \
    if ((mask)[n] & (bit)) { \
	_f_idx = 0xffff; \
	break; \
    } \
    (mask)[n] |= (bit);
#define OSS_EX_LOCAL_NAME(q) \
    ((q)._name._buf_store + ((q)._colon_pos + 1)), \
                              ((q)._name._buflen - ((q)._colon_pos + 1))
#define OSS_EX_IS_WS(c) \
    ((c) == ' '|| (c) == '\t' || (c) == '\n' || (c) == '\r')
#define OSS_EX_EXT_USE_TYPE_MODE	TRUE
struct st_OssToedDecmem {
    void *_oss_wWW;
    unsigned char *_oss_HwW;
    long _oss_jQW;
};
typedef struct st_OssEmbedData _oss_JWW;
#define OSS_EX_CONTENT \
    (_env->_lex_kind != EXER_END && \
	(_env->_lex_kind != EXER_TAG || _env->_tag->_kind != EXER_END_TAG))
#define OSS_EX_CONTENT_CPX \
    (_env->_lex_kind != EXER_END && _env->_tag->_kind != EXER_END_TAG)
#define OSS_EX_ADVANCE_LEX_EMP \
    if (_env->_start_tag->_kind == EXER_START_TAG) \
        _oss_tlex_get_lex(_env); \
    else \
        _env->_lex_kind = EXER_END; 
#define OSS_EX_ADVANCE_LEX_NONEMP \
    if (_env->_start_tag->_kind == EXER_START_TAG) \
        _oss_tlex_get_lex(_env); \
    else \
        _oss_tdex_error(_env, _xml_start_tag_expected, NULL, 0);
#define OSS_EX_ADVANCE_LEXC_EMP \
    if (_env->_start_tag->_kind == EXER_START_TAG) \
        _oss_tlex_get_lex_collapse(_env); \
    else \
        _env->_lex_kind = EXER_END; 
#define OSS_EX_ADVANCE_LEXC_NONEMP \
    if (_env->_start_tag->_kind == EXER_START_TAG) \
        _oss_tlex_get_lex_collapse(_env); \
    else \
        _oss_tdex_error(_env, _xml_start_tag_expected, NULL, 0);
#define OSS_EX_ADVANCE_TAG_EMP \
    if (_env->_start_tag->_kind == EXER_START_TAG) \
        _oss_tlex_get_tag(_env); \
    else \
        _env->_lex_kind = EXER_END;
#define OSS_EX_ADVANCE_TAG_NONEMP \
    if (_env->_start_tag->_kind == EXER_START_TAG) \
        _oss_tlex_get_tag(_env); \
    else \
        _oss_tdex_error(_env, _xml_start_tag_expected, NULL, 0);
#define OSS_EX_ADVANCE_LEX_DFE \
    if (_env->_start_tag->_kind == EXER_START_TAG) { \
        _oss_tlex_get_lex(_env); \
        if (_env->_lex_kind == EXER_TAG && _env->_tag->_kind == EXER_END_TAG) \
            _env->_flags |= OSS_EX_DFE; \
    } else \
        _env->_flags |= OSS_EX_DFE;
#define OSS_EX_ADVANCE_LEX_DFE_LOCAL \
    if (_env->_start_tag->_kind == EXER_START_TAG) { \
        _oss_tlex_get_lex(_env); \
        if (!(_env->_lex_kind == EXER_TAG && _env->_tag->_kind == EXER_END_TAG)) \
            _content = 1; \
    }
#define OSS_EX_FIELD_IS_PRESENT(mask, bit, fname) \
	if ((mask) & (bit)) \
		_oss_toed_error(_env->_g, _xml_dubl_val, fname); \
	(mask) |= (bit);

typedef struct st_OssToedDecmem OssToedDecmem;

/* lexeme kind */
typedef enum {
    EXER_END, EXER_TAG, EXER_UTF8, EXER_UNICODE
} exer_lex_kind;

/* tag kind */
typedef enum {
/* EXER_SKIP_TAG - is unknown tag to skip */
    EXER_SKIP_TAG, EXER_START_TAG, EXER_END_TAG, EXER_EMPTY_TAG
} exer_tag_kind;

typedef struct exer_buf {
    unsigned char	*_buf_store;
    unsigned long 	_buflen;
    unsigned long	_global_offset;
} exer_buf;

typedef struct exer_entity {
    struct exer_entity  *_next;
    exer_buf		_name;
    exer_buf      	_value;
    OSS_UINT32		_flags;
} exer_entity;

/* qualified name structure */
typedef struct {
    exer_buf		_name;
    long 		_colon_pos; /* separator position */
    unsigned char	*_uri;	    /* unique URI pointer */
} exer_name;

/* attribute structure */
typedef struct exer_attribute {
    struct exer_attribute *_next;	/* next attribute */
    exer_name 		  _name;	/* attribute name */
    exer_buf		  _value;
    OSS_UINT32 		  _flags;
} exer_attribute;

struct exer_denv;
typedef struct exer_denv exer_denv;

struct exer_embed;
typedef struct exer_embed exer_embed;

typedef void (*exer_embed_decoder)(exer_denv *, exer_embed *);

struct exer_embed {
    exer_embed_decoder  _dec;	    /* pointer to the EMBED decoding function */
    unsigned long       _count;     /* number of elements */
    void		*_data;     /* pointer to decoded data */
    void		*_data_end; /* current position - only for UNBOUNDED */
};

/* tag structure */
typedef struct exer_tag {
    exer_name		_name;      /* tag name */
    exer_attribute 	*_attrs;    /* pointer to the first attribute */
    exer_embed          *_embed;    /* if not NULL, type has EMBED-VALUES */
    unsigned char       *_mem_ns;   /* pointer to start of stack */
    unsigned char 	*_mem_attr; /* pointer to start of stack */
    exer_tag_kind 	_kind;	    /* tag kind */
} exer_tag;

/* the lexer status */
#define MAX_LEX_NESTING 	8
typedef struct {
    unsigned char       *_inbufpos;      /* current encoding */
    unsigned long       _inbuflen;	/* current length */
    unsigned long	_totallen;	/* total length */
    unsigned long	_global_offset;  /* encoding global offset */	
    OSS_UINT32 		_flags;	    	/* flag word */
    OSS_UINT32 		_nesting;    	/* current nesting level */
} exer_input;

typedef struct exer_ns {
    struct exer_ns      *_next;     /* previous namespace */
    exer_buf		_prefix;
    unsigned char	*_uri;	    /* unique URI pointer */
    unsigned long	_uri_len;
    OSS_UINT32          _flags;     /* flag word */
    OSS_UINT32 		_scope;	    /* nesting level */
} exer_ns;

/* data structure to keep a lexeme */
typedef struct exer_save_lexeme {
    exer_lex_kind	_lex_kind;	/* kind */
    unsigned long	_tag_start, 
			_tag_end;	/* start/end positions of a tag */
    union {
       exer_tag 	*_tag;		/* a tag */
       unsigned char	_utf8[5];	/* Utf8 character (4 bytes) */
       OSS_UINT32 	_unicode;	/* Unicode value */
    } _u;
} exer_save_lexeme;

typedef struct exer_save_cc_lex {
    exer_tag		_cur_tag,
			*_start_tag,
			*_tag;
    exer_save_lexeme    _lex;
    exer_ns		*_ns;
    unsigned char       *_stk_ptr;
} exer_save_cc_lex;

/* rescan data structure */
typedef struct exer_rescan {
    exer_save_lexeme	_lex;		/* current lexeme */
    exer_input		_input;		/* current encoding */
    exer_input 		_lexstack[/*MAX_LEX_NESTING*/8];
    OSS_UINT32		_lexstacksize;
    unsigned long	_num_fields;
    unsigned long	_alt_idx;
    jmp_buf		_errorBuf;
    jmp_buf		*_old_errorBuf;
    OssToedDecmem 	_outmem;
    unsigned char       *_stk_ptr;
    int			_ret_code;
} exer_rescan;     

struct exer_denv {
    OssGlobal 		*_g;
    exer_namespace 	*_ns_array;	/* points to the structure which represents
					 * an XML namespace in the E-XER control table */
    OSS_INT32		_namespace_count;
    unsigned char	*_xml_uri;	/* URI for "xml" prefix */
    unsigned char	*_ctrl_uri;	/* URI for control NS */
    unsigned char	*_asn1_uri;	/* ASN1 URI or NULL */
    exer_tag 		*_tag;		/* a pointer to the current tag */
    exer_tag            *_start_tag;	/* a pointer to the previous tag */
    exer_entity     	*_entities;		/* the list of entities */
    exer_ns  	    	*_ns;		/* the stack of NS */
    exer_input	    	_lexstack[MAX_LEX_NESTING];	/* the encoding stack */
    exer_input		_input;
    union {				
	OSS_UINT32  	_unicode;
	unsigned char   _utf8[5];		/* utf8[4]-keeps the length of UTF8 char */
    } _cur_char;
    unsigned long	_tag_start, 
			_tag_end;	/* start/end positions of a tag */
    unsigned long	_cc_pos; 
    unsigned long	*_size;		/* to error reporting */
    OSS_UINT16      	_lexstacksize;	/* stack counter */
    exer_lex_kind   	_lex_kind;	/* kind of a curremt lexeme */
    OSS_UINT32		_flags;
};
/* end of TOED EXER Decoder section */


typedef struct exer_qname {
    unsigned char	*_oss_WjW;
    unsigned int	_oss_JHH;
    OSS_UINT16        	_oss_wQj;	
} exer_qname;
#if 0
typedef struct _exer_ns {
    struct _exer_ns	*_oss_qWH;
    struct _exer_ns	*_oss_qJw;
    unsigned char	*_oss_wJH;
    unsigned char	*_oss_WjW;
    unsigned long	_oss_WQw;
    unsigned long	_oss_JHH;
} _exer_ns;
#endif
typedef struct exer_uri_list exer_uri_list;
typedef struct exer_attr_list exer_attr_list;
struct exer_attr_list {
    exer_attr_list	*_oss_qWH;
    unsigned char	*_oss_wJH;
    unsigned char	*_oss_WjW;
    unsigned int	_oss_WQw;
    unsigned int	_oss_JHH;
};
struct exer_eenv;
typedef struct exer_eenv exer_eenv;
typedef void (*exer_embed_encoder)(exer_eenv *);

struct exer_uri_list {
    exer_uri_list	*_next;
    unsigned char	*_uri;
    unsigned int	_uri_len;
};

typedef struct teex_embed {
    struct teex_embed		*_prev;
    exer_embed_encoder		_enc;
    void			*_data;
    unsigned long		_idx;
    unsigned long		_count;
    OSS_UINT32          	_nesting;
} teex_embed;

struct exer_eenv {
    /* Common encoder/decoder data */
    struct ossGlobal	*_g;
    /* Pointers to the list of declared attributes and URIs */
    exer_uri_list	*_uris;
    exer_attr_list	*_attrs;
    /* Local context */
    teex_embed	        *_embed;
    teex_embed	        *_embed_prev;
    /* Specific data */
    long                _linepos;/* start position at current output line */
    long                _taglen; /* length of last encoded 'prefix:tag' string */
    long		_curlen; /* estimated size of currently encoded string value */
    unsigned int    	_indent; /* number of indentation spaces for value */
    /* Dynamic flags */
#define OSS_EX_CANCEL_FORMATTING		0x00000010
#define OSS_EX_UNTAGGED_SET_OF_EL		0x00000020
#define OSS_EX_DISABLE_ATTRS			0x00000040
#define OSS_EX_URI_FOUND			0x80000000
#define OSS_EX_DEFAULT_NS			OSS_EX_URI_FOUND
    OSS_UINT32          _nesting;
    OSS_UINT32          _embed_nesting;
    OSS_UINT32         	_flags;
    /* --- The fields below should be moved to the OssGlobal structure --- */
    exer_namespace 	*_ns_array;	/* namespace array in the control table or  *
					 * its copy created by namespace prefix API */
    OSS_INT32		_ns_count;
    exer_search		*_ns_search;
    unsigned char	*_reserved_prefix;
    OSS_UINT16		_asn1NsIndex;
    OSS_UINT16		_dns_idx;	/* default namespace index */
/*
    OSS_UINT16		_controlNsIndex;
*/
    /* ------------------------------------------------------------------- */
};
/* end of TOED EXER Encoder section */

/*****************************************************************************/
/*  TOED general section                                                     */
/*****************************************************************************/

#ifdef EBCDIC
#include <iconv.h>
#endif
#include <setjmp.h>

#if defined(_MSC_VER) && (defined(_WIN32) || defined(WIN32) || defined(WINCE) || defined(_WIN64))
#pragma pack(push, align, 8)
#elif defined(_MSC_VER) && (defined(_WINDOWS) || defined(_MSDOS))
#pragma pack(8)
#endif /* _MSC_VER && _WIN32 */
typedef struct {
    char _oss_a;
    double _oss_b;
} _oss_amax;
#if defined(_MSC_VER) && (defined(_WIN32) || defined(WIN32) || defined(WINCE) || defined(_WIN64))
#pragma pack(pop, align)
#elif defined(_MSC_VER) && (defined(_WINDOWS) || defined(_MSDOS))
#pragma pack()
#endif /* _MSC_VER && _WIN32 */

#define _oss_asize ((unsigned)((char *)&((_oss_amax *)NULL)->_oss_b - (char *)&((_oss_amax *)NULL)->_oss_a))
#define _oss_mem_align(a) ((_oss_asize - (a)%_oss_asize) % _oss_asize)

/* The _MEM_ARRAY_SIZE size should be such that the size of the encDecVar
 * field be equal or greater than that of the world->c structure */

#ifdef __hp9000s300
#define _MEM_ARRAY_SIZE	34
#endif

#ifdef __alpha
#ifdef __osf__
#define _MEM_ARRAY_SIZE	43
#endif	/* __osf__ */
#endif	/* __alpha */

#ifdef _AIX
#define _MEM_ARRAY_SIZE	52
#endif
#ifdef __hp9000s700
#define _MEM_ARRAY_SIZE	60
#endif

#ifdef __NeXT__
#define _MEM_ARRAY_SIZE	66
#endif

#ifdef VAXC
#define _MEM_ARRAY_SIZE	78
#endif

#ifdef __mips
#	define _MEM_ARRAY_SIZE       70
#endif	/* __mips */

#ifdef _FTX	/* Stratus's Fault-Tolerant Unix */
#define _MEM_ARRAY_SIZE  84
#endif

#ifdef __HIGHC__
#define _MEM_ARRAY_SIZE       52
#endif	/* __HIGHC__ */

#ifdef __arm
#define _MEM_ARRAY_SIZE       60
#endif	/* __arm */

#if defined(_WIN32) || defined(_WINDOWS) ||\
	defined(__OS2__) || defined(NETWARE_DLL)
#define _MEM_ARRAY_SIZE 52
#endif /* _WIN32 || _WINDOWS || __OS2__ || NETWARE_DLL */

#ifdef _MCCPPC
#define _MEM_ARRAY_SIZE       72
#endif	/* _MCCPPC */

#if defined(__vms) && (__INITIAL_POINTER_SIZE + 0) == 64
#define _MEM_ARRAY_SIZE 40
#endif

#ifndef _MEM_ARRAY_SIZE
#define _MEM_ARRAY_SIZE 60
#endif

typedef struct _mem_array_ {
    short           _used;	                /* Next available entry */
    void           *_entry[_MEM_ARRAY_SIZE];	/* Pointers to allocated
						 * memory */
    struct _mem_array_ *_next;	/* Pointer to additional mem_array */
} _mem_array;

struct st_OssMemoryBlockBounds {
    unsigned char *left;
    unsigned char *right;
};

struct st_OssMemoryBlockHeader {
    struct st_OssMemoryBlockHeader *prev;
    unsigned char *block_end;
};

struct st_OssMemoryBlockHeaderEx {
    struct st_OssMemoryBlockHeaderEx *prev;
    unsigned char *block_end;
    unsigned char *data_end;
};

typedef struct st_OssMemoryBlockBounds OssMemoryBlockBounds;
typedef struct st_OssMemoryBlockHeader OssMemoryBlockHeader;
typedef struct st_OssMemoryBlockHeaderEx OssMemoryBlockHeaderEx;


#ifndef OSSDEBUG
#define OSSDEBUG 0
#endif /* OSSDEBUG */

typedef struct _encoding_ {
	       long   length;           /* length of the encoding */
	       char   *value;           /* pointer to encoding octets */
} _encoding;

typedef struct _enc_block_ {
    struct _enc_block_ *_next;        /* nested setofs form a list of these */
    long   size;                     /* size of the encodings array */
    long   used;                     /* number of items used in the array */
    _encoding       *enc;            /* pointer to array of encodings */
    _mem_array      mem;             /* previous encoding saved*/
    _mem_array      *mem_tail;
    char            *pos;
    long            max_len;
    ossBoolean           buffer_provided;
    long            _encoding_length;
    ossBoolean	    queue;	/* memory storage arrangement: stack or queue */
} _enc_block;

struct _enum_data {
	int         num;     /* number of enumerations */
	OSS_INT32   *enums;  /* pointer to sorted array of enumerations */
};

struct _char_data {
	int      num;  /* number of characters in PermittedAlphabet */
	void     *pa;  /* pointer to PermittedAlphabet char string */
	void     *ia;  /* pointer to inverted indices string */
};

struct st_tree_char_data {
    unsigned int length;
    const OSS_UINT32 *pa;
};

struct st_char_data {
    unsigned int length;
    const char *pa;		/* pointer to PermittedAlphabet char string */
    const unsigned char *inv_pa;/* pointer to inverted indices string */
};

struct st_wide_char_data {
    unsigned int length;
    const char *pa;		/* pointer to PermittedAlphabet char string */
    const unsigned char *inv_pa;/* pointer to inverted indices string */
    unsigned int base_value;
};

#define OSS_PSD_ENCODE_ALIGNED_VALUE	0x80
#define OSS_PSD_ENCODE_UNALIGNED_VALUE	0x8000
#define OSS_PSD_TIME_TYPE		0x10000
#define OSS_PSD_EXTENSIBLE		0x20000
#define OSS_PSD_MULTI_PLANE_PA		0x40000
#define OSS_PSD_ALWAYS_IN_ROOT		0x80000
#define OSS_PSD_IN_EXTENSION		0x80

struct st_PER_String_Data {   
    const void *ppa;
    unsigned long lb;
    unsigned long ub;
    unsigned long full_ub;
    unsigned int flags;
};

typedef struct st_PER_String_Data PER_String_Data;

/* The following structures are used by XER decoder to perform effective
 * search in character string array (SET/SEQUENCE/CHOICE/ENUMERATED).
 *
 * Implementation comments:
 *        - if hashes are present, it is assumed their array is sorted
 * (least value first), string array is assumed unsorted
 *        - if hashes are absent, the string data array is assumed sorted
 * (lexicographically, the 'least' string goes first)
 */
struct _string_data {
    const char * value;		/* string value */
    size_t        slen;		/* string length */
    unsigned char lmask;	/* kinds of acceptable tags */
    OSS_INT32  src;		/* return value if string matched */
};

struct _string_search_data {
    size_t              alen;	/* arrays length */
    OSS_INT32           unknown_rc; /* is returned when no match occurs */
    const struct _string_data * sa; /* string data array */
    const OSS_UINT32    * ha;	/* hashes array; may be NULL - no hashes */
};

/* The following structure are used by XER encoder to perform effective
 * search in character string array (ENUMERATED).
 * It's used as array. And for the first value of this array the field
 * 'count' contains quantity of array records, and the field 'nm' is NULL
 * for unextensible enum, or is "unknown_enum" differently.
 */
typedef struct {
     long    count; /* enum value */
     char *  nm; /* value name */
} _oss_enumInfo;

typedef struct _mem_block_ {
   void               *_data;
   struct _mem_block_ *_prev;
} _mem_block_;

typedef struct _st_print_pdu_ {
    int (*_pf)(struct ossGlobal *, void *);
    char *_name;
} _print_pdu_;

#define _oss_mem_bsize(tsize, msize) \
		(tsize > 1024 ? tsize : \
		(tsize * msize) < 1024 ? tsize * msize : 1024)

#define _oss_mem_bsize_unconstr(tsize) \
		(tsize > 1024 ? tsize : 1024)

#define _oss_mem_bcount(tsize, msize) \
		(tsize > 1024 ? 1 : \
		(tsize * msize) < 1024 ? msize : 1024 / tsize)

#define _oss_mem_bcount_unconstr(tsize) \
		(tsize > 1024 ? 1 : 1024 / tsize)

#define OSS_TOED_API_LEVEL 25

#define _dstd_parms_def char **_pos, long *_buf_len, long _length
#define _dstd_parms _pos, _buf_len, _length
#define _sdstd_parms_def char **_pos, long *_buf_len
#define _sdstd_parms _pos, _buf_len

/*
 * std_parms_def and std_parms.
 * These parameters are passed to most TOED encoding functions.
 * They are:
 *
 * char **pos:  this varies slightly depending upon the
 *   kind of encoding.  For DEFINITE encoding, this is
 *   a pointer to a pointer to the place where data
 *   was last written into memory.  For INDEFINITE encoding
 *   this is a pointer to a pointer to the place where
 *   data is next to be written.
 *
 *   Any function which writes encoded data updates *pos
 *   to stay current.
 *
 * long *max_len:  *max_len is the number of bytes left
 *   in the current memory buffer.  (The buffer into
 *   which encoded data is being written.)  If we are using
 *   memory pages, this is the number of bytes left before
 *   we need to allocate a new page.  If we are using the
 *   user's buffer, this is the number of bytes left before
 *   we'll need to complain violently.
 */
#define _std_parms_def char **_pos, long *_max_len
#define _std_parms _pos, _max_len

/* Entry Points for the Interface Routines */

#if defined(__WATCOMC__) && defined(__DOS__)
typedef    void       (*_Encode_function)
			(struct ossGlobal *world, int pdunum,
			void *inbuf);

typedef    void       (*_Decode_function)
			(struct ossGlobal *world, int *pduNum,
			void **outbuf);

typedef    int       (*_FreePDU_function)
			(struct ossGlobal *world, int pdunum,
			void *data);

typedef    void      (*_Ossinit_function)(struct ossGlobal *);

typedef    void       (*_Encode_EXER_function)
			(exer_eenv *env, int pdunum,
			void *inbuf);

typedef    void       (*_Decode_EXER_function)
			(exer_denv *env, int *pduNum,
			void **outbuf);

#else
typedef    void       (DLL_ENTRY_FPTR *_System _Encode_function)
			(struct ossGlobal *world, int pdunum,
			void *inbuf);

typedef    void      (DLL_ENTRY_FPTR *_System _Decode_function)
			(struct ossGlobal *world, int *pduNum,
			void **outbuf);

typedef    int       (DLL_ENTRY_FPTR *_System _FreePDU_function)
			(struct ossGlobal *world, int pdunum,
			void *data);

typedef    void      (DLL_ENTRY_FPTR *_System _Ossinit_function)(struct ossGlobal *);

typedef    void       (DLL_ENTRY_FPTR *_System _Encode_EXER_function)
			(exer_eenv *env, int pdunum,
			void *inbuf);

typedef    void       (DLL_ENTRY_FPTR *_System _Decode_EXER_function)
			(exer_denv *env, int *pduNum,
			void **outbuf);
#endif /* __WATCOMC__ && __DOS__ */

typedef    int	     (DLL_ENTRY_FPTR *_System _PrintPDU_function)
			(struct ossGlobal *, int pdunum,
			void *data);
typedef    int       (DLL_ENTRY_FPTR *_System _CopyPDU_function)(struct ossGlobal *, int pdunum,
			void *src, void **dst);
typedef    int       (DLL_ENTRY_FPTR *_System _CmpPDU_function)(struct ossGlobal *, int pdunum,
			void *odata, void *cdata);
typedef    void	     (DLL_ENTRY_FPTR *_System _ChkCstrPDU_function)(struct ossGlobal *, int pdunum,
			void *data);
typedef    int	     (DLL_ENTRY_FPTR *_System _GetExtAdd_function)(struct ossGlobal *, void *_ext,
			unsigned int _extnumb, OssBuf *_data);

typedef struct _entry_point_struct_ {
	_Ossinit_function	_ossinit_function;
	_Encode_function        _ber_encode_function;
	_Decode_function        _ber_decode_function;
	_FreePDU_function	_freePDU_function;
	unsigned short int      _oss_ber_version_number;
	_Encode_function        _per_encode_function;
	_Decode_function        _per_decode_function;
	unsigned short int      _oss_per_version_number;
	unsigned int            _oss_pa_count;
	struct _char_data       *_chars_ptr;
	_PrintPDU_function      _printPDU_function;
}               _entry_point_struct;

typedef struct _mixed_real_internal_ {
    double base2;
    char  *base10;
} _mixed_real_internal;

typedef struct _oss_xapi_entries_struct_ {
    unsigned short		_api_lvl;
    _CopyPDU_function		_copy_pdu_function;
    _CmpPDU_function            _cmp_pdu_function;
    _Encode_function            _xer_encode_function;
    _Decode_function            _xer_decode_function;
    unsigned short int          _oss_xer_version_number;
    _ChkCstrPDU_function         _chk_cstr_pdu_function;
    _GetExtAdd_function		_get_ext_add_function;
    _Encode_EXER_function       _exer_encode_function;
    _Decode_EXER_function       _exer_decode_function;
    unsigned short int          _oss_exer_version_number;
} _oss_xapi_entries_struct;

/* Error Handling */

/* The index to the error messages.  NOTE:  The enumerated constants here
   must match those in coderror.h. */

enum _err_index {
/*
 * FILE: @(#)coderror.h	16.19  07/08/29
# @(#)errindex.sh	14.1 04/10/11
 */
_no_msg, _small_buffer, _type_not_impl, _pdu_range, _bad_argument,
_version_mismatch_toed, _version_mismatch_soed, _out_stor,
_bad_choice, _bad_der_time, _stor_error, _int_too_long,
_uint_too_long, _bit_str_comp, _access_serialization, _constrainedBy,
_singleValue_integer, _singleValue_uinteger, _singleValue_real,
_singleValue_decreal, _singleValue_string, _singleValue_void,
_valueRange_integer, _valueRange_uinteger, _valueRange_real,
_valueRange_decreal, _durationComponentRange_string,
_sizeConstraint_length, _sizeConstraint_number, _oPA_constraint,
_absence_constraint, _presence_constraint, _openType_error,
_openType_user_field_error, _tableConstraint, _componentRelation,
_not_enumerated, _null_tbl, _null_fcn, _corrupted_tbl, _neg_uinteger,
_typeConstraint, _bit_str_too_long, _pdu_range_recursive,
_enc_oid_too_long, _more_input, _zero_bytes_requested, _size_too_big,
_out_of_memory, _invalid_object, _memmgr_undefined_err,
_bad_encrules_err, _toed_rules_not_impl_err, _unlinked_encrules_err,
_table_mismatch, _type_not_supported, _type_not_supported_decreal,
_real_dll_failed, _indef_length_not_supported, _cstrain_dll_failed,
_cmp_dll_failed, _cmp_code_not_linked, _real_code_not_linked,
_tracing_code_not_linked, _oid_dll_failed,
_type_not_supported_der_toed, _toed_rules_not_supp_err,
_load_library_failed, _unload_library_failed, _mandatory_field,
_input_file_open, _bad_objid, _bad_pointer, _bad_time,
_time_bad_month, _time_bad_day, _time_bad_hour, _time_bad_minute,
_time_bad_second, _time_bad_millisec, _too_long, _toom_field,
_zero_length_ANY, _zero_length_OpenType, _inval_dec_real,
_inval_mixed_real, _per_mandatory_field_in_group,
_ber_mandatory_field_in_group, _UTF8_bad_conversion,
_AUTOMATIC_ENCDEC_is_not_specified, _pduNum_is_not_initialized,
_length_is_not_initialized, _tag_long_nec, _tag_too_long,
_exp_long_nec, _exp_too_long, _int_long_nec, _subid_long_nec,
_subid_too_long, _length_too_long, _size_too_long, _non_std_eoc,
_unknown_size, _inconsis_len, _tag_mismatch, _pdu_mismatch,
_extra_bits, _inval_enc, _no_dec_dig, _no_exp_dig, _inval_nr1,
_inval_nr2, _inval_nr3, _real_long_zero, _undef_enc, _null_in_nlltrm,
_unknown_field, _field_repeat, _field_omit, _expec_eoc,
_indef_len_prim, _expec_constr_fnd_prim, _expec_prim_fnd_constr,
_unknown_pdu_type, _enc_too_long, _cannot_happen, _neg_not_allowed,
_expect_tag, _more_value, _real_too_large, _too_many_subids,
_too_many_optional_fields, _too_many_ext_additions,
_too_many_optional_in_group, _extension_encode_error,
_indef_length_der, _bad_UTF8_char, _ber_field_removed,
_per_field_removed, _unexpected_end_of_pdu, _input_file_eof,
_not_multiple_charwidth, _objid_is_constructed, _small_stack,
_stack_sharing, _printper_dll_failed, _small_tmp_buffer,
_bad_tmp_buf_free, _int_len_too_long, _uint_len_too_long,
_pattern_constraint_string, _pattern_constraint,
_pattern_constraint_not_linked, _nocopy_fragmented,
_xml_start_tag_expected, _xml_start_or_empty_tag_expected,
_xml_invalid_start_tag, _xml_zero_pdu, _xml_tags_mismatch,
_xml_end_tag_expected, _xml_item_mismatch, _xml_invalid_bool,
_xml_unknown_field, _xml_not_named_value, _xml_space_expected,
_xml_no_debug_info, _xml_exp_name, _xml_bad_tag, _xml_inv_name,
_xml_long_name, _xml_inv_comment, _xml_inv_ch_seq,
_xml_unexp_comment, _xml_cxer_restriction, _xml_inv_cstr_char,
_xml_invalid_element, _xml_empty_tag_expected, _xml_invalid_tag,
_xml_inv_form, _xml_exp_eq, _bad_objid_node,
_constructed_nesting_limit_exceeded, _seq_set_nesting_limit_exceeded,
_api_dll_failed, _no_valueset_cnst_support,
_segment_length_is_not_0_modulo_8, _xml_bad_char_ref,
_xml_big_char_ref, _xml_entity_not_found, _xml_bad_entity_ref,
_xml_undef_prefix, _xml_bad_cdata, _xml_cdata_exp, _xml_ill_char,
_xml_too_deeply, _xml_unsupp_entity, _xml_small_buffer,
_xml_ill_entity, _xml_ill_formed_entity, _xml_ill_start,
_xml_ill_dtd, _xml_duplicate_dtd, _xml_ill_notation_def,
_xml_ill_char_in_name, _xml_utf8_not_started, _xml_utf8_unfinished,
_xml_no_local_part, _xml_no_prefix, _xml_colon_in_local_part,
_xml_duplicate_ns, _xml_duplicate_default_ns, _xml_tag_expected,
_xml_named_value, _xml_name_forbidden, _xml_boolean_value,
_xml_attr_repeated, _xml_sign_plus_forbidden, _xml_neg_zero,
_xml_leading_zeros_forbidden, _xml_ws_found,
_xml_too_many_named_values, _xml_bad_base64, _xml_end_expected,
_xml_time_length, _xml_bad_bool, _xml_unknown_attribute,
_xml_uri_invalid, _xml_uri_wrong, _xml_prefix_wrong,
_xml_uname_wrong, _xml_embed_size, _xml_embed_size_sax,
_xml_alt_name, _xml_empty_enc, _xml_empty_list_enc, _xml_list_ws,
_xml_wrong_order, _xml_order_size, _xml_empty_real_modified,
_xml_exp_decimal, _xml_neg_zero_decimal, _xml_inf_nan_decimal,
_xml_txt_real_forbidden, _xml_leading_zeros_exponent, _xml_dubl_val,
_xml_anyattr_wrong, _xml_name_expected, _xml_collapse_ws,
_xml_inv_char, _xml_nil_forbidden, _xml_unknown_type_attr,
_xml_xml_ns, _compress_init, _compress_error, _xml_anyel_wrong,
_invalid_fragmentation, _user_fun_error, _zero_length_ContentsConst,
_char_too_big, _inval_time, _nonzero_pad_bits_1, _nonzero_pad_bits_2,
_nonzero_pad_bits_3, _xml_DTD_forbidden, _sax_not_supported,
_sax_internal, _xml_invalid_pi, _xml_pi_forbidden,
_xml_parser_not_linked, _xml_ill_xmldecl, _alternative_notfound,
_property_settings_constraint, _duration_range_constraint,
_timepoint_range_constraint, _timepoint_range_constraint_1,
_recurrence_range_constraint, _recurrence_range_constraint_s,
_canonical_restriction, _time_bad_week, _time_bad_century,
_time_bad_year, _xml_root_name, _recurrence_range_constraint_unlim,
_xml_enc_format, _hindmost_error
};

/* typedef's for the context information */

typedef enum _context_type {
    _boolean_type, _integer_type, _enumerated_type, _real_type,
    _bitstring_type, _octetstring_type, _null_type, _sequence_type,
    _sequence_of_type, _set_type, _set_of_type, _choice_type,
    _charstring_type, _object_identifier_type, _time_type, _any_type,
    _opentype, _embedded_pdv_type, _unrestr_charstring_type,
    _relative_oid_type, _contconstr_type, _defered_type, _unknown_type
}               _Context_Type;

typedef struct _context_id {
    char           *_element_name;
    char           *_element_type;
    _Context_Type   _type;
}               _Context_Id;


typedef struct _context_element {
    struct _context_element *_parent;
    _Context_Id    *_id;
    long            _occurrence;/* For SET OF or SEQUENCE OF */
}               _Context_Element;

typedef enum _context_state {
    _encoding_tag, _encoding_length, _encoding_data,
    _decoding_tag, _decoding_length, _decoding_data,
    _checking_cnstrnts
}               _Context_State;

typedef struct _context_anchor {
    _Context_Element *_last;
    int             _pdu_number;
    _Context_Id    *_pdu_id;
    _Context_State  _state;
}               _Context_Anchor;


typedef  char _Per_Inverted_Index[128];

extern _Context_Anchor _oss_context_anchor;

typedef struct _ossDTDate {
    unsigned char bit_mask;
#define _ossDTDateCenturyBit       0x80
#define _ossDTDateYearBit          0x40
#define _ossDTDateWeekBit          0x20
#define _ossDTDateMonthBit         0x10
#define _ossDTDateDayBit           0x08
#define _ossDTDateBasicYearBit     0x04
#define _ossDTDateProlepticYearBit 0x02
#define _ossDTDateNegativeYearBit  0x01
    unsigned short number_of_yc_digits;
    int            year_or_century;
    unsigned short week;
    unsigned short month;
    unsigned short day;
} _ossDTDate;

typedef struct _ossDTTimeFraction {
    unsigned short number_of_digits;
    unsigned int value;
} _ossDTTimeFraction;

typedef struct _ossDTTime {
    unsigned char bit_mask;
#define _ossDTTimeHoursBit    0x80
#define _ossDTTimeMinutesBit  0x40
#define _ossDTTimeSecondsBit  0x20
#define _ossDTTimeFractionBit 0x10
#define _ossDTTimeUtcBit      0x08
#define _ossDTTimeDiffBit     0x04
    unsigned short hours;
    unsigned short minutes;
    unsigned short seconds;
    struct _ossDTTimeFraction fraction;
    struct _ossDTTimeDifference {
#define _ossDTTimeDiffMinusBit   0x80
#define _ossDTTimeDiffMinutesBit 0x40
	unsigned char  bit_mask;
	unsigned short hours;
	unsigned short minutes;
    } diff;
} _ossDTTime;

typedef struct _ossDTPoint {
    _ossDTDate date;
    _ossDTTime time;
} _ossDTPoint;

typedef struct _ossDTDuration {
    unsigned char bit_mask;
    unsigned char accuracy_bit;
#define _ossDTDurationYearsBit    0x80
#define _ossDTDurationMonthsBit   0x40
#define _ossDTDurationWeeksBit    0x20
#define _ossDTDurationDaysBit     0x10
#define _ossDTDurationHoursBit    0x08
#define _ossDTDurationMinutesBit  0x04
#define _ossDTDurationSecondsBit  0x02
#define _ossDTDurationFractionBit 0x01
    unsigned int years;
    unsigned int months;
    unsigned int weeks;
    unsigned int days;
    unsigned int hours; 
    unsigned int minutes;
    unsigned int seconds;
    struct _ossDTTimeFraction fraction;
} _ossDTDuration;

#define _ossDuration_years_present    _ossDTDurationYearsBit
#define _ossDuration_months_present   _ossDTDurationMonthsBit
#define _ossDuration_weeks_present    _ossDTDurationWeeksBit
#define _ossDuration_days_present     _ossDTDurationDaysBit
#define _ossDuration_hours_present    _ossDTDurationHoursBit
#define _ossDuration_minutes_present  _ossDTDurationMinutesBit
#define _ossDuration_seconds_present  _ossDTDurationSecondsBit
#define _ossDuration_fraction_present _ossDTDurationFractionBit

typedef struct _ossDTInterval {
    unsigned char bit_mask;
#define _ossDTIntRecurrenceBit    0x80
#define _ossDTIntStartPointBit    0x40
#define _ossDTIntEndPointBit      0x20
#define _ossDTIntStartDurationBit 0x10
#define _ossDTIntEndDurationBit   0x08
    unsigned short number_of_r_digits;
    unsigned int recurrence;
    union _ossDTIntervalPoint {
	_ossDTPoint point;
	_ossDTDuration duration;
    } start;
    union _ossDTIntervalPoint end;
} _ossDTInterval;

#define _ossDateTimeIsEmpty(v) (!(v).bit_mask)
#define _ossDuration(v)        ((v).start.duration)

typedef _ossDTInterval _ossDateTime;


typedef struct _Xml_Enc_Env {
    struct _Xml_Enc_Env * _prev;
    /* Common encoder/decoder data */
    struct ossGlobal *_g;
    /* Specific data */
    unsigned int	indent; /* number of indentation spaces for value */
    long		linepos;/* start position at current output line */
    long		taglen;	/* length of last encoded tag */
    long		totallen; /* summary length of encoder's output memory blocks */
} _Xml_Enc_Env;

typedef struct _Xml_Dec_Env {
    /* Common encoder/decoder data */
    struct ossGlobal *_g;
    /* Specific data */
    char		* inbuf;
    unsigned char	byte;
    unsigned char	lkind;
    char		* name;
    size_t		nlen;
#   define _FSA_PPTR_REF 0x01
#   define _FSA_CNS_OFF  0x02
    OSS_UINT32		_flags;
} _Xml_Dec_Env;

struct ossExtStackElement {
    unsigned char *_bufpos;
    unsigned long _buflen;
    void *buffer;
    union {
	unsigned char *_allocated_buffer;
	unsigned int _used;
    } u;
    unsigned int _bitpos;
};

#define EXT_STACK_SIZE 4

struct ossExtStack {
    struct ossExtStack *_next;
    struct ossExtStackElement st[EXT_STACK_SIZE];
    unsigned int sp;
};

typedef struct _oss_mem_link {
    struct _oss_mem_link *_next, *_prev;
} _oss_mem_link;

/* TOED Control table header */
typedef struct {
    OSS_UINT32          table_flags;
    OSS_UINT16          table_version;
    OSS_UINT16          table_compat_version;
    OSS_INT32           namespace_count;
    exer_namespace      *namespace_array;
    exer_search         *namespace_search;
    exer_decode_position root_decoder;
    unsigned char       *reserved_prefix;
    OSS_UINT16      	control_ns;
    OSS_UINT16          asn1_ns;
    exer_search		*esa;
    exer_tag_decoder    *tda;
    exer_qname_table    *qta;
    exer_bool		*boola;
    exer_int		*inta;
    exer_int_ll		*int_lla;
    exer_int_huge	*int_hugea;
    exer_enum		*enuma;
    exer_bit		*bita;
    exer_oct		*octa;
    exer_oid		*oida;
    exer_char1		*char1a;
    exer_char2		*char2a;
    exer_char4		*char4a;
    exer_time		*timea;
    exer_iso_time	*iso_timea;
    exer_otyp		*otypa;
    exer_real		*reala;
    OSS_INT32           ext_count;
    void                **ext_array;
    OSS_INT32           ext_list_count;
    exer_extensions_list *ext_list_array;
} exer_toed_table;
                                        
typedef struct ToedExerData {
    exer_toed_table	*exer_table;
    unsigned char 	reserved_prefix[16];
    void        	*_ex_nsa;
    unsigned int 	has_default_namespace : 1;
} ToedExerData;

typedef struct ToedDataExt {
    long      bytes_read_by_decoder;
    int         (DLL_ENTRY_FPTR *ossUserVprint)(struct ossGlobal *,
			const char *format, va_list ap);
    void     *(DLL_ENTRY_FPTR *_System mallocpSaved)(size_t sz);
    void      (DLL_ENTRY_FPTR *_System freepSaved)(void *p);
    OssBuf    tmpBuffer;
    _mem_array	   _oss_stack;
    _mem_array*	   _oss_stack_tail;
    unsigned char  padding_bits;
    unsigned int    _iOsetNum;	   /* total number of objectsets */
    void	   *_piOsetInfo;   /* Inextensible objectsets info */
    unsigned int    _xOsetNum;	   /* number of extensible object sets */
    XOSetInfoEntry * _pxOsetInfo;  /* Extensible objectsets info */
    int		   (*pcmpInfoObj)(struct ossGlobal *, int , void *, void *, int);
    void           *so_handle;
    _oss_xapi_entries_struct *_xapi_ent;
    _Xml_Enc_Env   *_xee;	   /* XER encoder specific fields */
    _Xml_Dec_Env   *_xde;	   /* XER decoder specific fields */
    char           *XSL_name;      /* XSL file name to be used by encoder */
    char           *DTD_name;      /* DTD name (root element) */
    short          DTD_kind;       /* SYSTEM/PUBLUC/NONE */
    char           *DTD_ExternalID;/* dtd file name to be used by encoder */
    void	   *xtmpbuf;	   /* XER runtime temporary buffer *
				    * for error recovery purposes  */
/* For additional flags-related information */
    int            fl_count;
    unsigned char  fl_mask_default;
    unsigned char *fl_mask;
    unsigned long  compat_flags; /* Compatibility flags for TOED */
    /* limit and counter of nesting level for constructed string encodings */
    int         constructedNestingLimit;
    int         constructedNestingCounter;
    /*
     * limit and counter of nesting level of encoding of SET, SEQUENCE, SET OF,
     * SEQUENCE OF and CHOICE
     */
    int         seqSetNestingLimit;
    int         seqSetNestingCounter;
    struct ossExtStack _oss_ext_stack;
    ossBoolean  dtemp_mem;
    int         xmlEncRule; /* used by ossBinary2XML()/ossXML2Binary() */
    int		(*ossUserFieldCpyp)(struct ossGlobal *world, OpenType *src, OpenType *dest);
    int		(*ossUserFieldCmpp)(struct ossGlobal *world, OpenType *src, OpenType *dest);
    unsigned long debug_flags; /* Debug flags for use with ossSetDebugFlags */
    ossDebugData debug_data;
    void	*mem_handle;
    void	*(*ossUserMallocp_saved)(void *, size_t);
    void	*(*ossUserReallocp_saved)(void *, void *, size_t);
    void	 (*ossUserFreep_saved)(void *, void *);
    /* for Lean ASN.1/C++; needed for opentypes support */
    void	*cppPointer;
    /* to mark using of time interval start-end points */
    unsigned int timeSEPointsInUse: 2,
		timeDiffSign : 1,
		timeDiffHours : 4,
		timeDiffMinutes : 6;
    ToedExerData exd;
} ToedDataExt;

typedef struct _EncDecGlobals {

   jmp_buf	  *_oss_err_env;
   void		 (*_old_sigsegv)(int);
   void		 (*_old_sigbus)(int);
   int		   _oss_asn1err;
   char		  *_oss_err_msg;
  ossBoolean            _buffer_provided;
  ossBoolean            _tag_decoded;
  ossBoolean            _restrain_mode;
   long		   _restrain_size;
  _Context_Anchor  _oss_context_anchor;
   char		  *_oss_outbufpos;
   long		   _oss_outbuflen;
  _mem_array	   _oss_mem;
  _mem_array	  *_oss_mem_tail;
    union {
	struct {
	    _mem_array	*_oss_temp_tail;
	    _mem_array	_oss_temp_mem;
	} mem_array; /* obsolete */
	struct {
	    OssMemoryBlockHeaderEx *last_block;
	    unsigned char *first_avail;
	    OssMemoryBlockBounds bounds;
	    OssMemoryBlockHeader *last_altblock;
	} stack;
    } tmp_mem;
   long		   _oss_mem_alloc;
   long		   _oss_out_alloc;
   unsigned int     relax_ber: 1;
   unsigned int     der: 1;
   unsigned int     _cxer: 1; /* TRUE when CXER encode/decode is requested */
    OSS_UINT32	    _genflags; /* flags for usage by generated routines */
#ifdef DLL_LINKAGE
   void (DLL_ENTRY_FPTR *_System _oss_enc_error)(struct ossGlobal *g,
			enum _err_index _error_code, long _data);
#endif /* DLL_LINKAGE */
    ossBoolean            _aligned;
    int             _bitpos;
    struct ossGlobal *_prev;
    char           *_oss_inbufpos;
    long            _oss_inbuflen;
    void           *_prevbuf;
    long             encoding_length;
    void           *_blockptr;
    struct ossGlobal *_next;
    FILE           *asn1outSaved;
    int         (DLL_ENTRY_FPTR *asn1prntSaved)(FILE *stream,
			const char *format, ...);
    int         (DLL_ENTRY_FPTR *ossUserPrintp)(struct ossGlobal *,
			const char *format, ...);
    void	*(*ossUserMallocp)(void *, size_t);
    void	*(*ossUserReallocp)(void *, void *, size_t);
    void	 (*ossUserFreep)(void *, void *);

#ifdef EBCDIC
    /* The following fields contain the default permitted alphabets for
     * IA5String, NumericString, PrintableString, and VisibleString */
    char                *ia5chars;
    char                *numchars;
    char                *prichars;
    char                *vischars;
#ifdef ICONV_AVAILABLE
    /* The following fields contain the iconv() conversion tables */
    iconv_t              toAscii;
    iconv_t              toEbcdic;
#endif /* ICONV_AVAILABLE */
    int                  semaphoreId;   /* For mutex on EBCDIC machines */
#endif
    int indent_level;

    union {
	double size[4];
	void *pointer;
	struct {
	    unsigned short preset_flags;
	    ToedDataExt   *ext;
	    char  *rsf_tagpos;
	    long   rsf_taglen;
	} fields;
    } reserved;

#if defined(_WIN32) || defined(_WINDOWS) ||\
    defined(__OS2__) || defined(NETWARE_DLL)
#endif /* _WIN32 || _WINDOWS || __OS2__ || NETWARE_DLL */
} _EncDecGlobals;

#ifndef relax_per
#define relax_per relax_ber
#endif /* relax_per */

    /*************************************************************************/
    /*      TOED general functions section                                   */
    /*************************************************************************/

#if defined(_WINDOWS) || defined(_WIN32) ||\
	defined(__NETWARE_386__) || defined(__OS2__)
extern BOOL oss_globlist(struct ossGlobal *, struct globalInfo *,
		     struct globalInfo **);
extern BOOL oss_serializeGlobalAccess(struct ossGlobal *, struct globalInfo *,
				  struct globalInfo **);
#endif /* _WINDOWS || _WIN32 || __NETWARE_386__ || __OS2__ */

extern void *DLL_ENTRY _oss_enc_getmem(struct ossGlobal *g);
extern void *DLL_ENTRY _oss_dec_getmem(struct ossGlobal *g, long _size);
extern void *DLL_ENTRY _oss_dec_getmem_internal(struct ossGlobal *g, long size);

extern void *DLL_ENTRY _oss_dec_getfixmem(struct ossGlobal *g, size_t size);
#define _oss_dec_const_alloc(g,s) _oss_dec_getfixmem((g),(s)+_oss_mem_align(s))
#define _oss_dec_const_init_alloc(g,s) memset(_oss_dec_getfixmem((g),(s)+_oss_mem_align(s)),0,(s)+_oss_mem_align(s))

void *DLL_ENTRY _oss_dec_gettempmem( OssGlobal *g, size_t size );
void DLL_ENTRY _oss_dec_freetempmem( OssGlobal *g, void *ptr );
extern void *DLL_ENTRY _oss_dec_realloctempmem(struct ossGlobal *g, void *p,
					size_t old_size, size_t size);
extern void  DLL_ENTRY _oss_dec_free(struct ossGlobal *g, void *p);
extern void  DLL_ENTRY _oss_enc_push(struct ossGlobal *g,void *_p);
extern void *DLL_ENTRY _oss_enc_pop(struct ossGlobal *g);
extern void  DLL_ENTRY _oss_check_nullterm_time(struct ossGlobal *g, char *value, short kind);
extern int   DLL_ENTRY _oss_format_new_time(OssGlobal *g, char *value, char *dest);
extern int   DLL_ENTRY _oss_canonicalCheckTime(OssGlobal *g, char *str);
extern void **DLL_ENTRY _oss_load_enc_stack(struct ossGlobal *g, OSS_UINT32 *sp);
extern void DLL_ENTRY _oss_save_enc_stack(struct ossGlobal *g, OSS_UINT32 sp);
extern void **DLL_ENTRY _oss_increment_enc_stack(struct ossGlobal *g);
extern void **DLL_ENTRY _oss_decrement_enc_stack(struct ossGlobal *g);
extern void  DLL_ENTRY _oss_freeMem(struct ossGlobal *g, 
	void (*freep)(void *, void *));
extern void  DLL_ENTRY _oss_freetempMem(struct ossGlobal *g);
extern void  DLL_ENTRY _oss_releaseMem(struct ossGlobal *g);
extern void  DLL_ENTRY _ossFreeEncMemory(struct ossGlobal *world);
extern void  DLL_ENTRY _ossSetOutMemQueue(struct ossGlobal *world, long *outlen, char **outbuf, unsigned flags);
extern struct ossGlobal *  DLL_ENTRY _oss_epop_global(struct ossGlobal *world);
extern void DLL_ENTRY  _oss_set_outmem_d(struct ossGlobal *g, long _final_max_len,
					long *_totalsize, char **_outbuf);
extern void DLL_ENTRY  _oss_set_outmem_i(struct ossGlobal *g,long _final_max_len,
					long *_totalsize,char **_outbuf);
extern void DLL_ENTRY  _oss_set_outmem_p(struct ossGlobal *g,
					long *_totalsize,char **_outbuf);
extern void DLL_ENTRY  _oss_set_outmem_pb(struct ossGlobal *g,
			long *_totalsize, char **_outbuf, unsigned flags);
extern void     _oss_hdl_signal(int _signal);
extern void DLL_ENTRY _oss_free_creal(struct ossGlobal *g, char *p);
extern int  DLL_ENTRY ossMinit(struct ossGlobal *g);
extern void DLL_ENTRY _oss_beginBlock(struct ossGlobal *g, long count,
    char **pos, long *max_len);
extern void DLL_ENTRY _oss_nextItem(struct ossGlobal *g, long *max_len);
extern void DLL_ENTRY _oss_endBlock(struct ossGlobal *g, char **pos, long *max_len,
    unsigned char ct);
extern void DLL_ENTRY _oss_freeGlobals(struct ossGlobal *g, ossBoolean decoding);
extern void DLL_ENTRY _oss_tstk_add_block( OssGlobal *g, size_t size );
extern void DLL_ENTRY _oss_tstk_free_blocks( OssGlobal *g, void *ptr );

/* The error routines */
extern void DLL_ENTRY _oss_enc_error( struct ossGlobal *g,
	enum _err_index error_code, long data );
void DLL_ENTRY _oss_enc_llerror( struct ossGlobal *g,
	enum _err_index error_code, LONG_LONG data );
extern void DLL_ENTRY _oss_toed_error( struct ossGlobal *g,
	enum _err_index error_code, char *data );
extern void DLL_ENTRY _oss_toed_api_error( struct ossGlobal *g,
	enum _err_index error_code, long data );
#define _oss_dec_error _oss_enc_error
#define _oss_dec_llerror _oss_enc_llerror

/* called to perform limited check for default value */
extern ossBoolean DLL_ENTRY _oss_not_dflt(struct ossGlobal *g, void *data, void *dflt,
   long totalsize, long max_length, int kind);
extern ossBoolean DLL_ENTRY _oss_not_dflt_ia(struct ossGlobal *g,
    void *data, void *dflt, long totalsize, unsigned int offset,
    long max_length, int kind);
extern void DLL_ENTRY _oss_chk_enum(struct ossGlobal *g, long data,
	 struct _enum_data *enums);
extern void DLL_ENTRY _oss_chk_uenum(struct ossGlobal *g, unsigned long data,
	 struct _enum_data *enums);

extern void DLL_ENTRY _oss_chk_pac( struct ossGlobal *g,
    char *value, unsigned long length,
    const struct st_char_data *ppa );
extern void DLL_ENTRY _oss_chk_pac2( struct ossGlobal *g,
    unsigned short *value, unsigned long length,
    const struct st_wide_char_data *ppa );
extern void DLL_ENTRY _oss_chk_pac4( struct ossGlobal *g,
    OSS_INT32 *value, unsigned long length,
    const struct st_wide_char_data *ppa );

extern void DLL_ENTRY _oss_prt_bin(struct ossGlobal *world, char *before,
    unsigned char *s, long l, char *after);
extern void DLL_ENTRY _oss_prt_bit(struct ossGlobal *g, void *data,
    unsigned long length);
extern void DLL_ENTRY _oss_prt_char(struct ossGlobal *g, char *data,
    unsigned long length);
extern void DLL_ENTRY _oss_prt_creal(struct ossGlobal *g, char *data);
extern void DLL_ENTRY _oss_prt_gentime(struct ossGlobal *world,
    GeneralizedTime *gt);
extern void DLL_ENTRY _oss_prt_hex(struct ossGlobal *world, char *before,
    unsigned char *s, long l, char *after);
extern void DLL_ENTRY _oss_prt_nchar(struct ossGlobal *g, char *data);
extern void DLL_ENTRY _oss_prt_oct(struct ossGlobal *g, void *data,
    unsigned long length);
extern void DLL_ENTRY _oss_prt_opentype(struct ossGlobal *world,
    OpenType *data);
extern void DLL_ENTRY _oss_prt_pbit(struct ossGlobal *g, void *data,
    unsigned long totalsize, unsigned long max_length);
extern void DLL_ENTRY _oss_prt_real(struct ossGlobal *g, double d);
extern void DLL_ENTRY _oss_prt_utctime(struct ossGlobal *world,
    UTCTime *ut);
extern void DLL_ENTRY _oss_indent(struct ossGlobal *g, int change);
extern void DLL_ENTRY _oss_prt_mbchar(struct ossGlobal *world,
     unsigned char *s, long l, unsigned short width);
extern void DLL_ENTRY _oss_prt_enum(struct ossGlobal *_g, ULONG_LONG _data_ptr,
     struct _string_data const *sd, int len);
extern char* DLL_ENTRY _oss_getdisp_bin(struct ossGlobal *world, char *before,
    unsigned char *s, long l, char *after);
extern char* DLL_ENTRY _oss_getdisp_bit(struct ossGlobal *g, void *data,
    unsigned long length);
extern char* DLL_ENTRY _oss_getdisp_char(struct ossGlobal *g, char *data,
    unsigned long length);
extern char* DLL_ENTRY _oss_getdisp_creal(struct ossGlobal *g, char *data);
extern char* DLL_ENTRY _oss_getdisp_gentime(struct ossGlobal *world,
    GeneralizedTime *gt);
extern char* DLL_ENTRY _oss_getdisp_hex(struct ossGlobal *world, char *before,
    unsigned char *s, long l, char *after);
extern char* DLL_ENTRY _oss_getdisp_nchar(struct ossGlobal *g, char *data);
extern char* DLL_ENTRY _oss_getdisp_oct(struct ossGlobal *g, void *data,
    unsigned long length);
extern char* DLL_ENTRY _oss_getdisp_pbit(struct ossGlobal *g, void *data,
    unsigned long totalsize, unsigned long max_length);
extern char* DLL_ENTRY _oss_getdisp_real(struct ossGlobal *g, double d);
extern char* DLL_ENTRY _oss_getdisp_utctime(struct ossGlobal *world,
    UTCTime *ut);
extern char* DLL_ENTRY _oss_getdisp_mbchar(struct ossGlobal *world,
     unsigned char *s, long l, unsigned short width);
extern char* DLL_ENTRY _oss_getdisp_int(struct ossGlobal *world,
     LONG_LONG l);
extern char* DLL_ENTRY _oss_getdisp_uint(struct ossGlobal *world,
     ULONG_LONG l);
extern void DLL_ENTRY _oss_free_disp(struct ossGlobal *world,
     void *s);
extern ossBoolean DLL_ENTRY _ossCmpPrimitiveValue(struct ossGlobal *, void *,
                          void *, long, long, int);
extern int  DLL_ENTRY ossInitXObjectSetTable(struct ossGlobal *, const void * ,
				unsigned int , const void * , unsigned int, void *);
extern void DLL_ENTRY	 ossInitXAPIentries (struct ossGlobal *world, const void * _xent);
extern void * DLL_ENTRY	 _oss_cpy_unbnd_octet (struct ossGlobal * world, void *src, void *pdst,
						 short len_sz, long item_sz);
extern void * DLL_ENTRY	 _oss_cpy_unbnd_octet_ia (struct ossGlobal * world, void *src, void *pdst,
						 short len_sz, long item_sz, unsigned int offset);
extern void ** DLL_ENTRY _oss_cpy_dlink_obj (struct ossGlobal * world, void **psrc,
						void **pdst, long obj_sz);
extern void *  DLL_ENTRY _oss_cpy_dlink_plus (struct ossGlobal * _g, void *psrc, void *pdst,
                                              long obj_sz);
extern void ** DLL_ENTRY _oss_cpy_link_obj (struct ossGlobal * world, void **psrc,
						void **pdst, long obj_sz);
extern char * DLL_ENTRY	 _oss_cpy_char_real (struct ossGlobal * _g, char *src);
extern OpenType * DLL_ENTRY _oss_cpy_open_type (struct ossGlobal * _g, OpenType *src, OpenType *dst);
extern int  DLL_ENTRY    _oss_cmp_membits (unsigned char *data1, unsigned char *data2, long bitlen);
extern int  DLL_ENTRY    _oss_cmp_arrbits (unsigned char *data1, unsigned char *data2, long len1, long len2);
extern int  DLL_ENTRY    _oss_cmp_unbnd_octet (void *data1, void *data2, short len_sz, long item_sz);
extern int  DLL_ENTRY    _oss_cmp_unbnd_octet_ia (void *data1, void *data2,
				short len_sz, long item_sz, unsigned int offset);
extern int  DLL_ENTRY    _oss_cmp_huge_int (void *data1, void *data2, short len_sz, int sign);
extern int  DLL_ENTRY    _oss_cmp_huge_int_ia (void *data1, void *data2,
				short len_sz, int sign, unsigned int offset);
extern int  DLL_ENTRY    _oss_cmp_link_obj (void **data1, void **data2, long val_sz);
extern int  DLL_ENTRY    _oss_cmp_link_objid (void *data1, void *data2, unsigned int val_sz);
extern int  DLL_ENTRY    _oss_cmp_date_time(OssGlobal *g, char *str1, char *str2);
extern void DLL_ENTRY    _ossGetDateTimeValue(OssGlobal *g, char *data, _ossDateTime *v);
extern int  DLL_ENTRY    _ossDTIntervalsRelation(_ossDTInterval *v1, _ossDTInterval *v2);
extern int  DLL_ENTRY    _ossCmpDTIntervals(_ossDTInterval *v1, _ossDTInterval *v2);
#define _ossCmpDateTimes      _ossCmpDTIntervals
#define _ossDateTimesRelation _ossDTIntervalsRelation
extern int  DLL_ENTRY _ossChkNoBasicIntervalSettings(_ossDTInterval *v,
	unsigned int settings, unsigned short recur_digits_num, char **errmsg);
extern int  DLL_ENTRY _ossChkNoBasicDateSettings(_ossDTInterval *v,
	unsigned int settings, unsigned short year_digits_num, char **errmsg);
extern int  DLL_ENTRY _ossChkNoBasicTimeSettings(_ossDTInterval *v,
	unsigned int settings, unsigned short fract_digits_num, char **errmsg);
extern int  DLL_ENTRY _ossChkRecIntervalSettings(_ossDTInterval *v,
	unsigned int settings, unsigned short recur_digits_num, char **errmsg);
extern int  DLL_ENTRY _ossChkIntervalSettings(_ossDTInterval *v,
				    unsigned int settings, char **errmsg);
extern int  DLL_ENTRY _ossChkDateSettings(_ossDTInterval *v,
	unsigned int settings, unsigned short year_digits_num, char **errmsg);
extern int  DLL_ENTRY _ossChkTimeSettings(_ossDTInterval *v,
	unsigned int settings, unsigned short fract_digits_num, char **errmsg);
extern int  DLL_ENTRY _ossChkDateTimeSettings(_ossDTInterval *v, char **errmsg);
extern int  DLL_ENTRY _ossChkRecurrenceRange(_ossDTInterval *v,
	    unsigned short min_number, unsigned short max_number, char **errmsg);
extern int  DLL_ENTRY    _oss_cmp_open_type (struct ossGlobal * _g, OpenType *data1, OpenType *data2);
extern int  DLL_ENTRY    compare_char_real(struct ossGlobal *world, char *src1, char *src2);
extern struct ossGlobal *DLL_ENTRY _oss_push_global(struct ossGlobal *g);
extern void *DLL_ENTRY _oss_getdispmem(struct ossGlobal *g, long size);
extern void DLL_ENTRY _oss_freedispmem(struct ossGlobal *g, void *p);
extern unsigned char DLL_ENTRY charIsPermitted( const OSS_UINT32 *pat,
	OSS_UINT32 character );
extern void DLL_ENTRY _oss_free_rsf_ext(struct ossGlobal *g, void *_data);
unsigned char *DLL_ENTRY _oss_itoUTF8Char( unsigned char *value, OSS_UINT32 ch );
unsigned int DLL_ENTRY _oss_countUTF8Char( OSS_UINT32 ch );
long DLL_ENTRY _oss_count_bits(unsigned char *value, long bit_len);
long DLL_ENTRY _oss_count_chars(char *value, long length);

	/*
	 * The following macros must be #defined if you compile the ASN.1
	 * compiler generated files (.c files) and link-edit with the import
	 * library ossapit.lib, i.e the OSS DLLs ossapit.dll, toedapi.dll,
	 * and toedber.dll/toedper.dll are used.  The following must not
	 * be #defined if you link-edit with the static library toedcode.lib
	 * or toedcomd.lib.  If you use /MD and link with toedcomd.lib,
	 * #define ONE_DLL on the C-compiler command line to disable the
	 * below macros.
	 */
#ifdef DLL_LINKAGE

#define _oss_intToUTF8Char     (*_g->ft.apiTbl->_oss_intToUTF8Charp)
#define _oss_load_enc_stack    (*_g->ft.apiTbl->_oss_load_enc_stackp)
#define _oss_save_enc_stack    (*_g->ft.apiTbl->_oss_save_enc_stackp)
#define _oss_increment_enc_stack (*_g->ft.apiTbl->_oss_increment_enc_stackp)
#define _oss_decrement_enc_stack (*_g->ft.apiTbl->_oss_decrement_enc_stackp)
#define _oss_dec_free          (*_g->ft.apiTbl->_oss_dec_freep)
#define _oss_dec_getmem        (*_g->ft.apiTbl->_oss_dec_getmemp)
#define _oss_dec_getfixmem     (*_g->ft.apiTbl->_oss_dec_getfixmemp)
#define _oss_dec_getmem_internal (*_g->ft.apiTbl->_oss_dec_getmem_internalp)
#define _oss_free_creal        (*_g->ft.apiTbl->_oss_free_crealp)
#define _oss_releaseMem        (*_g->ft.apiTbl->_oss_releaseMemp)
#define _oss_set_outmem_d      (*_g->ft.apiTbl->_oss_set_outmem_dp)
#define _oss_count_bits		(*_g->ft.apiTbl->_oss_count_bitsp)
#define _oss_count_chars	(*_g->ft.apiTbl->_oss_count_charsp)
#define _oss_not_dflt	       (*_g->ft.apiTbl->_oss_not_dfltp)
#define _oss_not_dflt_ia       (*_g->ft.apiTbl->_oss_not_dflt_iap)
#define _oss_beginBlock        (*_g->ft.apiTbl->_oss_beginBlockp)
#define _oss_nextItem          (*_g->ft.apiTbl->_oss_nextItemp)
#define _oss_endBlock          (*_g->ft.apiTbl->_oss_endBlockp)
#define _ossSetOutMemQueue     (*_g->ft.apiTbl->_ossSetOutMemQueuep)
#define _ossFreeEncMemory      (*_g->ft.apiTbl->_ossFreeEncMemoryp)
#define _oss_epop_global       (*_g->ft.apiTbl->_oss_epop_globalp)
#define _oss_enc_error         (*_g->ft.apiTbl->_oss_enc_errorp)
#define _oss_toed_error        (*_g->ft.apiTbl->_oss_toed_errorp)
#define _oss_toed_api_error    (*_g->ft.apiTbl->_oss_toed_api_errorp)
#define _oss_enc_llerror       (*_g->ft.apiTbl->_oss_enc_llerrorp)
#define _oss_chk_enum          (*_g->ft.apiTbl->_oss_chk_enump)
#define _oss_chk_uenum         (*_g->ft.apiTbl->_oss_chk_uenump)
#define _oss_prt_bin           (*_g->ft.apiTbl->_oss_prt_binp)
#define _oss_prt_bit           (*_g->ft.apiTbl->_oss_prt_bitp)
#define _oss_prt_char          (*_g->ft.apiTbl->_oss_prt_charp)
#define _oss_prt_creal         (*_g->ft.apiTbl->_oss_prt_crealp)
#define _oss_prt_gentime       (*_g->ft.apiTbl->_oss_prt_gentimep)
#define _oss_prt_hex           (*_g->ft.apiTbl->_oss_prt_hexp)
#define _oss_prt_nchar         (*_g->ft.apiTbl->_oss_prt_ncharp)
#define _oss_prt_oct           (*_g->ft.apiTbl->_oss_prt_octp)
#define _oss_prt_opentype      (*_g->ft.apiTbl->_oss_prt_opentypep)
#define _oss_prt_pbit          (*_g->ft.apiTbl->_oss_prt_pbitp)
#define _oss_prt_real          (*_g->ft.apiTbl->_oss_prt_realp)
#define _oss_prt_utctime       (*_g->ft.apiTbl->_oss_prt_utctimep)
#define _oss_indent            (*_g->ft.apiTbl->_oss_indentp)
#define _oss_prt_mbchar        (*_g->ft.apiTbl->_oss_prt_mbcharp)
#define _oss_prt_enum          (*_g->ft.apiTbl->_oss_prt_enump)
#define _oss_getdisp_bin       (*_g->ft.apiTbl->_oss_getdisp_binp)
#define _oss_getdisp_bit       (*_g->ft.apiTbl->_oss_getdisp_bitp)
#define _oss_getdisp_char      (*_g->ft.apiTbl->_oss_getdisp_charp)
#define _oss_getdisp_creal     (*_g->ft.apiTbl->_oss_getdisp_crealp)
#define _oss_check_nullterm_time    (*_g->ft.apiTbl->_oss_check_nullterm_timep)
#define _oss_getdisp_gentime   (*_g->ft.apiTbl->_oss_getdisp_gentimep)
#define _oss_getdisp_hex       (*_g->ft.apiTbl->_oss_getdisp_hexp)
#define _oss_getdisp_nchar     (*_g->ft.apiTbl->_oss_getdisp_ncharp)
#define _oss_getdisp_oct       (*_g->ft.apiTbl->_oss_getdisp_octp)
#define _oss_getdisp_pbit      (*_g->ft.apiTbl->_oss_getdisp_pbitp)
#define _oss_getdisp_real      (*_g->ft.apiTbl->_oss_getdisp_realp)
#define _oss_getdisp_utctime   (*_g->ft.apiTbl->_oss_getdisp_utctimep)
#define _oss_getdisp_mbchar    (*_g->ft.apiTbl->_oss_getdisp_mbcharp)
#define _oss_push_global       (OssGlobal *)(*_g->ft.apiTbl->_oss_push_globalp)
#define _oss_getdisp_int       (*_g->ft.apiTbl->_oss_getdisp_intp)
#define _oss_getdisp_uint      (*_g->ft.apiTbl->_oss_getdisp_uintp)
#define _oss_free_disp         (*_g->ft.apiTbl->_oss_free_dispp)
#define ossInitXObjectSetTable (*world->ft.apiTbl->_oss_init_oset_tablep)
#define ossInitXAPIentries	(*world->ft.apiTbl->_oss_init_xapi_entryp)
#define _oss_cpy_unbnd_octet	(*_g->ft.apiTbl->_oss_cpy_unbnd_octetp)
#define _oss_cpy_dlink_obj	(*_g->ft.apiTbl->_oss_cpy_dlink_objp)
#define _oss_cpy_dlink_plus	(*_g->ft.apiTbl->_oss_cpy_dlink_plusp)
#define _ossCmpPrimitiveValue  (*_g->ft.apiTbl->_ossCmpPrimitiveValuep)
#define _oss_cpy_link_obj	(*_g->ft.apiTbl->_oss_cpy_link_objp)
#define _oss_cpy_unbnd_octet_ia	(*_g->ft.apiTbl->_oss_cpy_unbnd_octet_iap)
#define _oss_cpy_char_real	(*_g->ft.apiTbl->_oss_cpy_char_realp)
#define _oss_cpy_open_type	(*_g->ft.apiTbl->_oss_cpy_open_typep)
#define _oss_cmp_membits       (*_g->ft.apiTbl->_oss_cmp_membitsp)
#define _oss_cmp_arrbits       (*_g->ft.apiTbl->_oss_cmp_arrbitsp)
#define _oss_cmp_unbnd_octet_ia (*_g->ft.apiTbl->_oss_cmp_unbnd_octet_iap)
#define _oss_cmp_huge_int_ia   (*_g->ft.apiTbl->_oss_cmp_huge_int_iap)
#define _oss_cmp_unbnd_octet   (*_g->ft.apiTbl->_oss_cmp_unbnd_octetp)
#define _oss_cmp_huge_int      (*_g->ft.apiTbl->_oss_cmp_huge_intp)
#define _oss_cmp_link_obj      (*_g->ft.apiTbl->_oss_cmp_link_objp)
#define _oss_cmp_link_objid    (*_g->ft.apiTbl->_oss_cmp_link_objidp)
#define _oss_cmp_date_time     (*_g->ft.apiTbl->_oss_cmp_date_timep)
#define _ossGetDateTimeValue   (*_g->ft.apiTbl->_ossGetDateTimeValuep)
#define _ossDTIntervalsRelation (*_g->ft.apiTbl->_ossDTIntervalsRelationp)
#define _ossCmpDTIntervals     (*_g->ft.apiTbl->_ossCmpDTIntervalsp)
#define _ossChkNoBasicIntervalSettings (*_g->ft.apiTbl->_ossChkNoBasicIntervalSettingsp)
#define _ossChkNoBasicDateSettings (*_g->ft.apiTbl->_ossChkNoBasicDateSettingsp)
#define _ossChkNoBasicTimeSettings (*_g->ft.apiTbl->_ossChkNoBasicTimeSettingsp)
#define _ossChkRecIntervalSettings (*_g->ft.apiTbl->_ossChkRecIntervalSettingsp)
#define _ossChkIntervalSettings (*_g->ft.apiTbl->_ossChkIntervalSettingsp)
#define _ossChkDateSettings    (*_g->ft.apiTbl->_ossChkDateSettingsp)
#define _ossChkTimeSettings    (*_g->ft.apiTbl->_ossChkTimeSettingsp)
#define _ossChkDateTimeSettings (*_g->ft.apiTbl->_ossChkDateTimeSettingsp)
#define _ossChkRecurrenceRange (*_g->ft.apiTbl->_ossChkRecurrenceRangep)
#define _oss_cmp_open_type     (*_g->ft.apiTbl->_oss_cmp_open_typep)
#define _oss_chk_pac	       (*_g->ft.apiTbl->_oss_chk_pacp)
#define _oss_chk_pac2	       (*_g->ft.apiTbl->_oss_chk_pac2p)
#define _oss_chk_pac4	       (*_g->ft.apiTbl->_oss_chk_pac4p)
#define _oss_dec_gettempmem    (*_g->ft.apiTbl->_oss_dec_gettempmemp)
#define _oss_dec_freetempmem   (*_g->ft.apiTbl->_oss_dec_freetempmemp)
#define _oss_dec_realloctempmem (*_g->ft.apiTbl->_oss_dec_realloctempmemp)
#define compare_char_real      (*_g->ft.apiTbl->compare_char_realp)
#define _oss_db_e_pre	       (*_g->ft.apiTbl->_oss_db_e_prep)
#define _oss_db_e_post	       (*_g->ft.apiTbl->_oss_db_e_postp)
#define _oss_db_d_pre	       (*_g->ft.apiTbl->_oss_db_d_prep)
#define _oss_db_d_post	       (*_g->ft.apiTbl->_oss_db_d_postp)
#define charIsPermitted	       (*_g->ft.apiTbl->charIsPermittedp)
#define _oss_free_rsf_ext      (*_g->ft.apiTbl->_oss_free_rsf_extp)
#endif /* DLL_LINKAGE */


#ifdef OSS_TOED_BER
/*****************************************************************************/
/*  TOED BER section                                                         */
/*****************************************************************************/
    /*************************************************************************/
    /*      TOED BER functions section                                       */
    /*************************************************************************/


/* Encode Routines */

extern long DLL_ENTRY _oss_encd_length(struct ossGlobal *g, _std_parms_def,
				 unsigned long _data);
extern long DLL_ENTRY _oss_encd_tag(struct ossGlobal *g, _std_parms_def,
			      unsigned short _data, ossBoolean _constructed);
long DLL_ENTRY _oss_encd_tag_length(struct ossGlobal *g, _sdstd_parms_def,
	long length, OSS_UINT32 tag);
long DLL_ENTRY _oss_encd_tag_length2(struct ossGlobal *g, _sdstd_parms_def,
	long length, OSS_UINT32 tag1, OSS_UINT32 tag2);
extern long DLL_ENTRY _oss_encd_bool(struct ossGlobal *g, _std_parms_def,
			       ossBoolean _data);
long DLL_ENTRY _oss_encd_bool_tl(struct ossGlobal *g, _sdstd_parms_def,
    OSS_UINT32 tag, ossBoolean value);
extern long DLL_ENTRY _oss_encd_int(struct ossGlobal *g, _std_parms_def,
				LONG_LONG _data);
long DLL_ENTRY _oss_encd_int_tl(struct ossGlobal *g, _sdstd_parms_def,
	long value, OSS_UINT32 tag);
long DLL_ENTRY _oss_encd_uint_tl(struct ossGlobal *g, _sdstd_parms_def,
	unsigned long value, OSS_UINT32 tag);
long DLL_ENTRY _oss_encd_llint_tl(struct ossGlobal *g, _sdstd_parms_def,
	LONG_LONG value, OSS_UINT32 tag);
long DLL_ENTRY _oss_encd_ullint_tl(struct ossGlobal *g, _sdstd_parms_def,
	ULONG_LONG value, OSS_UINT32 tag);
extern long DLL_ENTRY _oss_encd_uint(struct ossGlobal *g, _std_parms_def,
				ULONG_LONG _data);
extern long DLL_ENTRY _oss_encd_huge_int(struct ossGlobal *g, _std_parms_def,
			       void *_data);
extern long DLL_ENTRY _oss_encd_uhuge_int(struct ossGlobal *g, _std_parms_def,
			       void *_data);

extern long DLL_ENTRY _oss_encd_huge_int_ia(struct ossGlobal *g,
    _std_parms_def, unsigned char *value, unsigned long length);
extern long DLL_ENTRY _oss_encd_uhuge_int_ia(struct ossGlobal *g,
    _std_parms_def, unsigned char *value, unsigned long length);

extern long DLL_ENTRY _oss_encd_real(struct ossGlobal *g, _std_parms_def,
			       double _data);
extern long DLL_ENTRY _oss_encd_creal(struct ossGlobal *g, _std_parms_def,
			       char *_data);
extern long DLL_ENTRY _oss_encd_mreal(struct ossGlobal *g, _std_parms_def,
			       MixedReal *_data);
extern long DLL_ENTRY _oss_encd_mreal_ia(struct ossGlobal *g, _std_parms_def,
    enum MixedReal_kind kind, void *data);
extern long DLL_ENTRY _oss_encd_alobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, short _array_size);
extern long DLL_ENTRY _oss_encd_asobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, short _array_size);
extern long DLL_ENTRY _oss_encd_aiobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, short _array_size);
extern long DLL_ENTRY _oss_encd_llobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, long _size_c);
extern long DLL_ENTRY _oss_encd_llobjid_ia(struct ossGlobal *g, _std_parms_def,
    void *data, long size_c, unsigned int offset);
extern long DLL_ENTRY _oss_encd_lsobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, long _size_c);
extern long DLL_ENTRY _oss_encd_liobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, long _size_c);
extern long DLL_ENTRY _oss_encd_ulobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, long _size_c);
extern long DLL_ENTRY _oss_encd_ulobjid_ia(struct ossGlobal *g, _std_parms_def,
    unsigned long *value, unsigned int length, long size_c);
extern long DLL_ENTRY _oss_encd_usobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, long _size_c);
extern long DLL_ENTRY _oss_encd_usobjid_ia(struct ossGlobal *g, _std_parms_def,
    unsigned short *value, unsigned int length, long size_c);
extern long DLL_ENTRY _oss_encd_uiobjid(struct ossGlobal *g, _std_parms_def,
				  void *_data, long _size_c);
extern long DLL_ENTRY _oss_encd_uiobjid_ia(struct ossGlobal *g, _std_parms_def,
    unsigned int *value, unsigned int length, long size_c);
extern long DLL_ENTRY _oss_encd_pbit(struct ossGlobal *g, _std_parms_def,
			       void *_data, long _bit_count);
extern long DLL_ENTRY _oss_encd_pbit_tl(struct ossGlobal *g, _sdstd_parms_def,
    OSS_UINT32 value, unsigned int length, OSS_UINT32 tag);
extern long DLL_ENTRY _oss_encd_ubit(struct ossGlobal *g, _std_parms_def,
			       void *_data, char _lengthsize, long _size_c);
extern long DLL_ENTRY _oss_encd_vbit(struct ossGlobal *g, _std_parms_def,
			       void *_data, long _array_size,
			       char _lengthsize);
extern long DLL_ENTRY _oss_encd_vbit_ia(struct ossGlobal *g, _std_parms_def,
    unsigned char *value, unsigned long length, long size_c);

long DLL_ENTRY _oss_encd_ubit_ia(struct ossGlobal *g, _std_parms_def,
    unsigned char *value, unsigned long length, long size_c);

extern long DLL_ENTRY _oss_encd_nstr(struct ossGlobal *g, _std_parms_def,
			       char *_data, long _size_c);
extern long DLL_ENTRY _oss_encd_pstr(struct ossGlobal *g, _std_parms_def,
			       char *_data, long _byte_count);
extern long DLL_ENTRY _oss_encd_ustr(struct ossGlobal *g, _std_parms_def,
			       void *_data, char _lengthsize, long _size_c);
extern long DLL_ENTRY _oss_encd_vstr(struct ossGlobal *g, _std_parms_def,
			       void *_data, char _lengthsize, long _size_c);
extern long DLL_ENTRY _oss_encd_uoct(struct ossGlobal *g, _std_parms_def,
			       void *_data, char _lengthsize, long _size_c);
extern long DLL_ENTRY _oss_encd_voct(struct ossGlobal *g, _std_parms_def,
			       void *_data, char _lengthsize, long _size_c);
extern long DLL_ENTRY _oss_encd_voct_ia(struct ossGlobal *g, _std_parms_def,
    unsigned char *value, unsigned long length, long size_c);
extern long DLL_ENTRY _oss_encd_vstr_ia(struct ossGlobal *g, _std_parms_def,
    char *value, unsigned long length, long size_c);

extern long DLL_ENTRY _oss_encd_gtime(struct ossGlobal *g, _std_parms_def,
				GeneralizedTime*_data);
extern long DLL_ENTRY _oss_encd_utime(struct ossGlobal *g, _std_parms_def,
				UTCTime *_data);
extern long DLL_ENTRY _oss_encd_date_time(OssGlobal *g, _std_parms_def,
				char *data, int kind);
extern void DLL_ENTRY _oss_dec_date_time(OssGlobal *g, _dstd_parms_def,
				char **data, int kind);

extern long DLL_ENTRY _oss_encd_uoct_ia(struct ossGlobal *g, _std_parms_def,
    unsigned char *value, unsigned long length, long size_c);
extern long DLL_ENTRY _oss_encd_ustr_ia(struct ossGlobal *g, _std_parms_def,
    char *value, unsigned long length, long size_c);

extern long DLL_ENTRY _oss_encd_untime(struct ossGlobal *g, _std_parms_def,
				char *_data, long _size_c, short kind);

long DLL_ENTRY _oss_encd_uany(struct ossGlobal *g, _std_parms_def,
	unsigned char *value, unsigned long length);

extern long DLL_ENTRY _oss_encd_opentype(struct ossGlobal *g, _std_parms_def,
			       void *_data);
extern long DLL_ENTRY _oss_encd_defer(struct ossGlobal *g, _std_parms_def,
				void *data);
extern long DLL_ENTRY _oss_encd_bmpstr(struct ossGlobal *g, _std_parms_def,
		    void *data, char lengthsize, long size_c);

long DLL_ENTRY _oss_encd_bmpstr_ia(struct ossGlobal *g, _std_parms_def,
	unsigned short *value, unsigned long length, long size_c);

long DLL_ENTRY _oss_encd_unistr_ia(struct ossGlobal *g, _std_parms_def,
    OSS_INT32 *value, unsigned long length, long size_c);

extern long DLL_ENTRY _oss_encd_unistr(struct ossGlobal *g, _std_parms_def,
		    void *data, char lengthsize,
		    long size_c);

extern long DLL_ENTRY _oss_encd_eobjid(struct ossGlobal *g, _std_parms_def,
				  void *data, long size_c);

extern long DLL_ENTRY _oss_encd_ereloid(struct ossGlobal *g, _std_parms_def,
				  void *data, long size_c);

extern long DLL_ENTRY _oss_encd_dubit(struct ossGlobal *g, _std_parms_def,
    void *data, char lengthsize, long size_c, ossBoolean named_bits);

long DLL_ENTRY _oss_encd_eobjid_ia(struct ossGlobal *g, _std_parms_def,
    unsigned char *value, unsigned int length, long size_c);
long DLL_ENTRY _oss_encd_ereloid_ia(struct ossGlobal *g, _std_parms_def,
    unsigned char *value, unsigned int length, long size_c);

extern long DLL_ENTRY _oss_encd_dubit_ia(struct ossGlobal *g,
    _std_parms_def, unsigned char *value, unsigned long length,
    long size_c, ossBoolean named_bits);

extern long DLL_ENTRY _oss_encd_dvbit(struct ossGlobal *g, _std_parms_def,
		    void *data, long array_size,
		  char lengthsize, ossBoolean named_bits);
extern long DLL_ENTRY _oss_encd_dvbit_ia(struct ossGlobal *g,
    _std_parms_def, unsigned char *value, unsigned long length,
    long size_c, ossBoolean named_bits);

extern long DLL_ENTRY _oss_encd_utf8unistr(struct ossGlobal *g, _std_parms_def,
		    void *data, char lengthsize,
		    long size_c);

extern long DLL_ENTRY _oss_encd_utf8bmpstr(struct ossGlobal *g, _std_parms_def,
		    void *data, char lengthsize, long size_c);

extern long DLL_ENTRY _oss_encd_utf8unbound(struct ossGlobal *g, _std_parms_def,
		    void *data, char lengthsize, long size_c);

extern long DLL_ENTRY _oss_encd_utf8unistr_ia(struct ossGlobal *g,
    _std_parms_def, OSS_INT32 *value, unsigned long length, long size_c);

extern long DLL_ENTRY _oss_encd_utf8bmpstr_ia(struct ossGlobal *g,
    _std_parms_def, unsigned short *value, unsigned long length, long size_c);

extern long DLL_ENTRY _oss_encd_utf8unbound_ia(struct ossGlobal *g,
    _std_parms_def, unsigned char *value, unsigned long length, long size_c);

/* Decode Routines */
/* these return values */
extern unsigned int DLL_ENTRY _oss_dec_tag( struct ossGlobal *g,
	_sdstd_parms_def );			     
extern long DLL_ENTRY _oss_dec_length(struct ossGlobal *g, _sdstd_parms_def);
extern long DLL_ENTRY _oss_dec_tag_length(struct ossGlobal *g,
	_sdstd_parms_def, unsigned long tag_encoding);

extern ossBoolean DLL_ENTRY _oss_dec_bool(struct ossGlobal *g, _dstd_parms_def);
extern LONG_LONG DLL_ENTRY _oss_dec_llint(struct ossGlobal *g, _dstd_parms_def);
extern long DLL_ENTRY _oss_dec_lint(struct ossGlobal *g, _dstd_parms_def);
extern int  DLL_ENTRY _oss_dec_iint(struct ossGlobal *g, _dstd_parms_def);
extern short DLL_ENTRY _oss_dec_sint(struct ossGlobal *g, _dstd_parms_def);

extern
	unsigned long
DLL_ENTRY _oss_dec_hint_ia(struct ossGlobal *g,
	_dstd_parms_def, void *data);

extern ULONG_LONG DLL_ENTRY _oss_dec_ullint(struct ossGlobal *g, _dstd_parms_def);
extern unsigned long DLL_ENTRY _oss_dec_ulint(struct ossGlobal *g, _dstd_parms_def);
extern unsigned int DLL_ENTRY _oss_dec_uiint(struct ossGlobal *g, _dstd_parms_def);
extern unsigned short DLL_ENTRY _oss_dec_usint(struct ossGlobal *g, _dstd_parms_def);

/* The code for this one is so huge that it would be criminal to
 * repeat it three times.  Caller should cast the returned value
 * to what she wants.
 */
extern void DLL_ENTRY _oss_dec_real(struct ossGlobal *g, _dstd_parms_def,
				double *data);
extern void DLL_ENTRY _oss_dec_freal(struct ossGlobal *g, _dstd_parms_def,
				float *data);
extern void DLL_ENTRY _oss_dec_creal(struct ossGlobal *g, _dstd_parms_def,
				  char **_data);
extern void DLL_ENTRY _oss_dec_mreal(struct ossGlobal *g, _dstd_parms_def,
				  MixedReal *_data);

extern enum MixedReal_kind DLL_ENTRY _oss_dec_mreal_ia
	(struct ossGlobal *g, _dstd_parms_def,
		void *data);

/* Two versions of these functions.  If the type has the pointer
 * directive, then its size is not fixed--the function will allocate
 * the memory.  Otherwise the caller allocates the memory.  (Actually,
 * the type will probably be enclosed in another type, and therefore
 * the memory has already been allocated.)
 */
extern void DLL_ENTRY _oss_dec_alobjid_ptr(struct ossGlobal *g, _dstd_parms_def,
				     void **_data,
				     short _array_size);
extern void * DLL_ENTRY _oss_dec_alobjid_ptr_ia(struct ossGlobal *g,
    _dstd_parms_def,
    short size_c, unsigned int offset);

extern unsigned short DLL_ENTRY _oss_dec_alobjid_ia(struct ossGlobal *g,
    _dstd_parms_def, unsigned long *value, short size_c);

extern void * DLL_ENTRY _oss_dec_aiobjid_ptr_ia(struct ossGlobal *g,
    _dstd_parms_def,
    short size_c, unsigned int offset);

extern unsigned short DLL_ENTRY _oss_dec_aiobjid_ia(struct ossGlobal *g,
    _dstd_parms_def, unsigned int *value, short size_c);

extern void DLL_ENTRY _oss_dec_asobjid_ptr(struct ossGlobal *g, _dstd_parms_def,
				     void **_data,
				     short _array_size);
extern void * DLL_ENTRY _oss_dec_asobjid_ptr_ia(struct ossGlobal *g,
    _dstd_parms_def,
    short size_c, unsigned int offset);

extern void DLL_ENTRY _oss_dec_asobjid(struct ossGlobal *g, _dstd_parms_def,
				 void *_data, short _array_size);
extern unsigned short DLL_ENTRY _oss_dec_asobjid_ia(struct ossGlobal *g,
    _dstd_parms_def, unsigned short *value, short size_c);

/* These allocate all their own memory.  */
extern void DLL_ENTRY _oss_dec_llobjid(struct ossGlobal *g, _dstd_parms_def,
				 void **_data, long _size_c);
extern void * DLL_ENTRY _oss_dec_llobjid_ia(struct ossGlobal *g,
    _dstd_parms_def, long size_c,
    unsigned int offset);
extern void DLL_ENTRY _oss_dec_liobjid(struct ossGlobal *g, _dstd_parms_def,
				 void **_data, long _size_c);
extern void DLL_ENTRY _oss_dec_lsobjid(struct ossGlobal *g, _dstd_parms_def,
				 void **_data, long _size_c);

/*  These are passed the structure containing the count, but allocate
  *  space for the data itself. */
extern void DLL_ENTRY _oss_dec_ulobjid(struct ossGlobal *g, _dstd_parms_def,
				 void *_data, short _size_c);
extern unsigned short DLL_ENTRY _oss_dec_ulobjid_ia(struct ossGlobal *g,
    _dstd_parms_def, void *data, short size_c);
extern void DLL_ENTRY _oss_dec_uiobjid(struct ossGlobal *g, _dstd_parms_def,
				 void *_data, short _size_c);
extern unsigned short DLL_ENTRY _oss_dec_uiobjid_ia(struct ossGlobal *g,
    _dstd_parms_def, void *data, short size_c);
extern void DLL_ENTRY _oss_dec_usobjid(struct ossGlobal *g, _dstd_parms_def,
				 void *_data, short _size_c);
extern unsigned short DLL_ENTRY _oss_dec_usobjid_ia(struct ossGlobal *g,
    _dstd_parms_def, void *data, short size_c);
/* Caller allocates memory for this one.  Call for both e_pad & e_big_pad. */
extern void DLL_ENTRY _oss_dec_pbit(struct ossGlobal *g, _dstd_parms_def,
			      void *_data, long _bit_count,
				ossBoolean _named_bits);

/* See comment before dec_ulobjid. */
extern void DLL_ENTRY _oss_dec_ubit(struct ossGlobal *g, _dstd_parms_def,
	void *_data,
	char _lengthsize, long _size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_ubit_ia(struct ossGlobal *g,
	_dstd_parms_def, void *data,
	long size_c, unsigned int flags);

/* Two versions of this one too.  */
extern void DLL_ENTRY _oss_dec_vbit_ptr(struct ossGlobal *g, _dstd_parms_def,
				  void **_data,
				  long _array_size, char _lengthsize);

extern unsigned long DLL_ENTRY _oss_dec_vbit_ptr_ia(struct ossGlobal *g,
	_dstd_parms_def,
	void *data, unsigned int offset,
	long size_c);

extern unsigned long DLL_ENTRY _oss_dec_vbit_ptr_nz(struct ossGlobal *g,
	_dstd_parms_def,
	void *data, unsigned int offset,
	long size_c, unsigned int flags);

extern void DLL_ENTRY _oss_dec_vbit(struct ossGlobal *g, _dstd_parms_def,
			      void *_data, long _array_size,
			      char _lengthsize);

extern unsigned long DLL_ENTRY _oss_dec_vbit_ia(struct ossGlobal *g,
	_dstd_parms_def, void *data,
	long size_c);

extern unsigned long DLL_ENTRY _oss_dec_vbit_nz(struct ossGlobal *g,
	_dstd_parms_def, void *data,
	long size_c, unsigned int flags);


extern void DLL_ENTRY _oss_dec_nstr_ptr(struct ossGlobal *g, _dstd_parms_def,
				  char **_data, long _size_c);
extern void DLL_ENTRY _oss_dec_nstr(struct ossGlobal *g, _dstd_parms_def,
			      char *_data, long _size_c);

extern void DLL_ENTRY _oss_dec_pstr(struct ossGlobal *g, _dstd_parms_def,
			      char *_data, long _byte_count);

extern void DLL_ENTRY _oss_dec_ustr(struct ossGlobal *g, _dstd_parms_def,
			      void *_data,
			      char _lengthsize, long _size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_ustr_ia(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    long size_c);

extern void DLL_ENTRY _oss_dec_vstr_ptr(struct ossGlobal *g, _dstd_parms_def,
				  void **_data,
				  char _lengthsize, long _size_c);

extern unsigned long DLL_ENTRY _oss_dec_vstr_ptr_ia(struct ossGlobal *g,
	_dstd_parms_def,
	void *data, unsigned int offset,
	long size_c);

extern void DLL_ENTRY _oss_dec_vstr(struct ossGlobal *g, _dstd_parms_def,
			      void *_data, char _lengthsize, long _array_size);

extern unsigned long DLL_ENTRY _oss_dec_vstr_ia(struct ossGlobal *g,
	_dstd_parms_def, void *data,
	long size_c);

extern void DLL_ENTRY _oss_dec_uoct(struct ossGlobal *g, _dstd_parms_def,
			      void *_data,
			      char _lengthsize, long size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_uoct_ia(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    long size_c);
extern void DLL_ENTRY _oss_dec_voct_ptr(struct ossGlobal *g, _dstd_parms_def,
				  void **_data,
				  char _lengthsize, long _size_c);

extern unsigned long DLL_ENTRY _oss_dec_voct_ptr_ia(struct ossGlobal *g,
	_dstd_parms_def,
	void *data, unsigned int offset,
	long size_c);

extern void DLL_ENTRY _oss_dec_voct(struct ossGlobal *g, _dstd_parms_def,
			      void *_data, char _lengthsize, long _array_size);

extern unsigned long DLL_ENTRY _oss_dec_voct_ia(struct ossGlobal *g,
	_dstd_parms_def, void *data,
	long size_c);

extern void DLL_ENTRY _oss_dec_gtime(struct ossGlobal *g, _dstd_parms_def,
			       GeneralizedTime *_data);
extern void DLL_ENTRY _oss_dec_utime(struct ossGlobal *g, _dstd_parms_def,
			       UTCTime *_data);
extern void DLL_ENTRY _oss_dec_untime(struct ossGlobal *g, _dstd_parms_def,
				  char **data,
				  long size_c, short kind);
	unsigned long
DLL_ENTRY _oss_dec_uany(struct ossGlobal *g, _dstd_parms_def,
			      void *data);

extern void DLL_ENTRY _oss_dec_opentype(struct ossGlobal *g, _dstd_parms_def,
			      void *_data);

extern void DLL_ENTRY _oss_dec_defer(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    short tag, ossBoolean constructed);

extern void DLL_ENTRY _oss_dec_bmpstr(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    char lengthsize, long size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_bmpstr_ia(struct ossGlobal *g,
	_dstd_parms_def, void *data,
	long size_c);

extern void DLL_ENTRY _oss_dec_unistr(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    char lengthsize, long size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_unistr_ia(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    long size_c);

extern void DLL_ENTRY _oss_dec_eobjid(struct ossGlobal *g, _dstd_parms_def,
	void *data, long size_c);

extern void DLL_ENTRY _oss_dec_ereloid(struct ossGlobal *g, _dstd_parms_def,
	void *data, long size_c);

extern
	unsigned int
DLL_ENTRY _oss_dec_eobjid_ia(struct ossGlobal *g, _dstd_parms_def,
			void *data
		    , long size_c
	);
extern
	unsigned int
DLL_ENTRY _oss_dec_ereloid_ia(struct ossGlobal *g, _dstd_parms_def,
			void *data
		    , long size_c
	);

extern void DLL_ENTRY _oss_dec_utf8bmpstr(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    char lengthsize, long size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_utf8bmpstr_ia(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    long size_c);

extern void DLL_ENTRY _oss_dec_utf8unbound(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    char lengthsize,
		    long size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_utf8unbound_ia(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    long size_c);

extern void DLL_ENTRY _oss_dec_utf8unistr(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    char lengthsize, long size_c);

extern
	unsigned long
DLL_ENTRY _oss_dec_utf8unistr_ia(struct ossGlobal *g, _dstd_parms_def,
		    void *data,
		    long size_c);

/* Other functions */

/* called to decode unknown extension additions in relaysafe-mode */
extern void DLL_ENTRY _oss_dec_rsf_ext(struct ossGlobal *g, _dstd_parms_def,
    void **_data, char **rsf_pos, long *rsf_len,
    unsigned int *count);

/* called to count the items for set of or sequence of. */
extern long DLL_ENTRY _oss_count_setof_items(struct ossGlobal *g, _dstd_parms_def);

/* called to skip past items for the EXTENSIBLE directive */
extern void DLL_ENTRY _oss_skip_past_item(struct ossGlobal *g, _dstd_parms_def);

    /*************************************************************************/
    /*      TOED BER DLL table defines section                               */
    /*************************************************************************/

	/*
	 * The following macros must be #defined if you compile the ASN.1
	 * compiler generated files (.c files) and link-edit with the import
	 * library ossapit.lib, i.e the OSS DLLs ossapit.dll, toedapi.dll,
	 * and toedber.dll/toedper.dll are used.  The following must not
	 * be #defined if you link-edit with the static library toedcode.lib
	 * or toedcomd.lib.  If you use /MD and link with toedcomd.lib,
	 * #define ONE_DLL on the C-compiler command line to disable the
	 * below macros.
	 */
#ifdef DLL_LINKAGE

#define _oss_count_setof_items (*_g->ft.berTbl->_oss_count_setof_itemsp)
#define _oss_dec_aiobjid_ia    (*_g->ft.berTbl->_oss_dec_aiobjid_iap)
#define _oss_dec_aiobjid_ptr_ia (*_g->ft.berTbl->_oss_dec_aiobjid_ptr_iap)
#define _oss_dec_alobjid_ia    (*_g->ft.berTbl->_oss_dec_alobjid_iap)
#define _oss_dec_alobjid_ptr   (*_g->ft.berTbl->_oss_dec_alobjid_ptrp)
#define _oss_dec_alobjid_ptr_ia (*_g->ft.berTbl->_oss_dec_alobjid_ptr_iap)
#define _oss_dec_asobjid       (*_g->ft.berTbl->_oss_dec_asobjidp)
#define _oss_dec_asobjid_ia    (*_g->ft.berTbl->_oss_dec_asobjid_iap)
#define _oss_dec_asobjid_ptr   (*_g->ft.berTbl->_oss_dec_asobjid_ptrp)
#define _oss_dec_asobjid_ptr_ia (*_g->ft.berTbl->_oss_dec_asobjid_ptr_iap)
#define _oss_dec_bool          (*_g->ft.berTbl->_oss_dec_boolp)
#define _oss_dec_creal         (*_g->ft.berTbl->_oss_dec_crealp)
#define _oss_dec_freal         (*_g->ft.berTbl->_oss_dec_frealp)
#define _oss_dec_gtime         (*_g->ft.berTbl->_oss_dec_gtimep)
#define _oss_dec_hint_ia       (*_g->ft.berTbl->_oss_dec_hint_iap)
#define _oss_dec_iint          (*_g->ft.berTbl->_oss_dec_iintp)
#define _oss_dec_length        (*_g->ft.berTbl->_oss_dec_lengthp)
#define _oss_dec_tag_length    (*_g->ft.berTbl->_oss_dec_tag_lengthp)
#define _oss_dec_lint          (*_g->ft.berTbl->_oss_dec_lintp)
#define _oss_dec_liobjid       (*_g->ft.berTbl->_oss_dec_liobjidp)
#define _oss_dec_llint         (*_g->ft.berTbl->_oss_dec_llintp)
#define _oss_dec_llobjid       (*_g->ft.berTbl->_oss_dec_llobjidp)
#define _oss_dec_llobjid_ia    (*_g->ft.berTbl->_oss_dec_llobjid_iap)
#define _oss_dec_lsobjid       (*_g->ft.berTbl->_oss_dec_lsobjidp)
#define _oss_dec_mreal         (*_g->ft.berTbl->_oss_dec_mrealp)
#define _oss_dec_mreal_ia      (*_g->ft.berTbl->_oss_dec_mreal_iap)
#define _oss_dec_nstr          (*_g->ft.berTbl->_oss_dec_nstrp)
#define _oss_dec_nstr_ptr      (*_g->ft.berTbl->_oss_dec_nstr_ptrp)
#define _oss_dec_opentype      (*_g->ft.berTbl->_oss_dec_opentypep)
#define _oss_dec_pbit          (*_g->ft.berTbl->_oss_dec_pbitp)
#define _oss_dec_pstr          (*_g->ft.berTbl->_oss_dec_pstrp)
#define _oss_dec_real          (*_g->ft.berTbl->_oss_dec_realp)
#define _oss_dec_sint          (*_g->ft.berTbl->_oss_dec_sintp)
#define _oss_dec_tag           (*_g->ft.berTbl->_oss_dec_tagp)
#define _oss_dec_uany          (*_g->ft.berTbl->_oss_dec_uanyp)
#define _oss_dec_ubit_ia       (*_g->ft.berTbl->_oss_dec_ubit_iap)
#define _oss_dec_uiint         (*_g->ft.berTbl->_oss_dec_uiintp)
#define _oss_dec_uiobjid       (*_g->ft.berTbl->_oss_dec_uiobjidp)
#define _oss_dec_uiobjid_ia    (*_g->ft.berTbl->_oss_dec_uiobjid_iap)
#define _oss_dec_ulint         (*_g->ft.berTbl->_oss_dec_ulintp)
#define _oss_dec_ullint        (*_g->ft.berTbl->_oss_dec_ullintp)
#define _oss_dec_ulobjid       (*_g->ft.berTbl->_oss_dec_ulobjidp)
#define _oss_dec_ulobjid_ia    (*_g->ft.berTbl->_oss_dec_ulobjid_iap)
#define _oss_dec_uoct_ia       (*_g->ft.berTbl->_oss_dec_uoct_iap)
#define _oss_dec_usint         (*_g->ft.berTbl->_oss_dec_usintp)
#define _oss_dec_usobjid       (*_g->ft.berTbl->_oss_dec_usobjidp)
#define _oss_dec_usobjid_ia    (*_g->ft.berTbl->_oss_dec_usobjid_iap)
#define _oss_dec_ustr_ia       (*_g->ft.berTbl->_oss_dec_ustr_iap)
#define _oss_dec_utime         (*_g->ft.berTbl->_oss_dec_utimep)
#define _oss_dec_vbit          (*_g->ft.berTbl->_oss_dec_vbitp)
#define _oss_dec_vbit_ia       (*_g->ft.berTbl->_oss_dec_vbit_iap)
#define _oss_dec_vbit_nz       (*_g->ft.berTbl->_oss_dec_vbit_nzp)
#define _oss_dec_vbit_ptr      (*_g->ft.berTbl->_oss_dec_vbit_ptrp)
#define _oss_dec_vbit_ptr_ia   (*_g->ft.berTbl->_oss_dec_vbit_ptr_iap)
#define _oss_dec_vbit_ptr_nz   (*_g->ft.berTbl->_oss_dec_vbit_ptr_nzp)
#define _oss_dec_voct          (*_g->ft.berTbl->_oss_dec_voctp)
#define _oss_dec_voct_ia       (*_g->ft.berTbl->_oss_dec_voct_iap)
#define _oss_dec_voct_ptr      (*_g->ft.berTbl->_oss_dec_voct_ptrp)
#define _oss_dec_voct_ptr_ia   (*_g->ft.berTbl->_oss_dec_voct_ptr_iap)
#define _oss_dec_vstr          (*_g->ft.berTbl->_oss_dec_vstrp)
#define _oss_dec_vstr_ia       (*_g->ft.berTbl->_oss_dec_vstr_iap)
#define _oss_dec_vstr_ptr      (*_g->ft.berTbl->_oss_dec_vstr_ptrp)
#define _oss_dec_vstr_ptr_ia   (*_g->ft.berTbl->_oss_dec_vstr_ptr_iap)
#define _oss_dec_untime        (*_g->ft.berTbl->_oss_dec_untimep)
#define _oss_dec_defer         (*_g->ft.berTbl->_oss_dec_deferp)
#define _oss_encd_tag_length   (*_g->ft.berTbl->_oss_encd_tag_lengthp)
#define _oss_encd_tag_length2  (*_g->ft.berTbl->_oss_encd_tag_length2p)
#define _oss_encd_int_tl       (*_g->ft.berTbl->_oss_encd_int_tlp)
#define _oss_encd_uint_tl      (*_g->ft.berTbl->_oss_encd_uint_tlp)
#define _oss_encd_llint_tl     (*_g->ft.berTbl->_oss_encd_llint_tlp)
#define _oss_encd_ullint_tl    (*_g->ft.berTbl->_oss_encd_ullint_tlp)
#define _oss_encd_pbit_tl      (*_g->ft.berTbl->_oss_encd_pbit_tlp)
#define _oss_encd_aiobjid      (*_g->ft.berTbl->_oss_encd_aiobjidp)
#define _oss_encd_alobjid      (*_g->ft.berTbl->_oss_encd_alobjidp)
#define _oss_encd_asobjid      (*_g->ft.berTbl->_oss_encd_asobjidp)
#define _oss_encd_bool         (*_g->ft.berTbl->_oss_encd_boolp)
#define _oss_encd_bool_tl      (*_g->ft.berTbl->_oss_encd_bool_tlp)
#define _oss_encd_creal        (*_g->ft.berTbl->_oss_encd_crealp)
#define _oss_encd_gtime        (*_g->ft.berTbl->_oss_encd_gtimep)
#define _oss_encd_huge_int_ia  (*_g->ft.berTbl->_oss_encd_huge_int_iap)
#define _oss_encd_uhuge_int_ia (*_g->ft.berTbl->_oss_encd_uhuge_int_iap)
#define _oss_encd_int          (*_g->ft.berTbl->_oss_encd_intp)
#define _oss_encd_length       (*_g->ft.berTbl->_oss_encd_lengthp)
#define _oss_encd_liobjid      (*_g->ft.berTbl->_oss_encd_liobjidp)
#define _oss_encd_llobjid      (*_g->ft.berTbl->_oss_encd_llobjidp)
#define _oss_encd_llobjid_ia   (*_g->ft.berTbl->_oss_encd_llobjid_iap)
#define _oss_encd_lsobjid      (*_g->ft.berTbl->_oss_encd_lsobjidp)
#define _oss_encd_mreal        (*_g->ft.berTbl->_oss_encd_mrealp)
#define _oss_encd_mreal_ia     (*_g->ft.berTbl->_oss_encd_mreal_iap)
#define _oss_encd_nstr         (*_g->ft.berTbl->_oss_encd_nstrp)
#define _oss_encd_opentype     (*_g->ft.berTbl->_oss_encd_opentypep)
#define _oss_encd_pbit         (*_g->ft.berTbl->_oss_encd_pbitp)
#define _oss_encd_pstr         (*_g->ft.berTbl->_oss_encd_pstrp)
#define _oss_encd_real         (*_g->ft.berTbl->_oss_encd_realp)
#define _oss_encd_tag          (*_g->ft.berTbl->_oss_encd_tagp)
#define _oss_encd_uany         (*_g->ft.berTbl->_oss_encd_uanyp)
#define _oss_encd_ubit_ia      (*_g->ft.berTbl->_oss_encd_ubit_iap)
#define _oss_encd_uint         (*_g->ft.berTbl->_oss_encd_uintp)
#define _oss_encd_uiobjid      (*_g->ft.berTbl->_oss_encd_uiobjidp)
#define _oss_encd_uiobjid_ia   (*_g->ft.berTbl->_oss_encd_uiobjid_iap)
#define _oss_encd_ulobjid      (*_g->ft.berTbl->_oss_encd_ulobjidp)
#define _oss_encd_ulobjid_ia   (*_g->ft.berTbl->_oss_encd_ulobjid_iap)
#define _oss_encd_uoct_ia      (*_g->ft.berTbl->_oss_encd_uoct_iap)
#define _oss_encd_ustr_ia      (*_g->ft.berTbl->_oss_encd_ustr_iap)
#define _oss_encd_usobjid      (*_g->ft.berTbl->_oss_encd_usobjidp)
#define _oss_encd_usobjid_ia   (*_g->ft.berTbl->_oss_encd_usobjid_iap)
#define _oss_encd_ustr         (*_g->ft.berTbl->_oss_encd_ustrp)
#define _oss_encd_untime       (*_g->ft.berTbl->_oss_encd_untimep)
#define _oss_encd_utime        (*_g->ft.berTbl->_oss_encd_utimep)
#define _oss_encd_date_time    (*_g->ft.berTbl->_oss_encd_date_timep)
#define _oss_dec_date_time     (*_g->ft.berTbl->_oss_dec_date_timep)
#define _oss_encd_vbit         (*_g->ft.berTbl->_oss_encd_vbitp)
#define _oss_encd_vbit_ia      (*_g->ft.berTbl->_oss_encd_vbit_iap)
#define _oss_encd_voct         (*_g->ft.berTbl->_oss_encd_voctp)
#define _oss_encd_voct_ia      (*_g->ft.berTbl->_oss_encd_voct_iap)
#define _oss_encd_vstr         (*_g->ft.berTbl->_oss_encd_vstrp)
#define _oss_encd_vstr_ia      (*_g->ft.berTbl->_oss_encd_vstr_iap)
#define _oss_encd_defer        (*_g->ft.berTbl->_oss_encd_deferp)
#define _oss_enci_aiobjid      (*_g->ft.berTbl->_oss_enci_aiobjidp)
#define _oss_enci_alobjid      (*_g->ft.berTbl->_oss_enci_alobjidp)
#define _oss_enci_asobjid      (*_g->ft.berTbl->_oss_enci_asobjidp)
#define _oss_enci_bool         (*_g->ft.berTbl->_oss_enci_boolp)
#define _oss_enci_creal        (*_g->ft.berTbl->_oss_enci_crealp)
#define _oss_enci_gtime        (*_g->ft.berTbl->_oss_enci_gtimep)
#define _oss_enci_int          (*_g->ft.berTbl->_oss_enci_intp)
#define _oss_enci_length       (*_g->ft.berTbl->_oss_enci_lengthp)
#define _oss_enci_liobjid      (*_g->ft.berTbl->_oss_enci_liobjidp)
#define _oss_enci_llobjid      (*_g->ft.berTbl->_oss_enci_llobjidp)
#define _oss_enci_lsobjid      (*_g->ft.berTbl->_oss_enci_lsobjidp)
#define _oss_enci_mreal        (*_g->ft.berTbl->_oss_enci_mrealp)
#define _oss_enci_nstr         (*_g->ft.berTbl->_oss_enci_nstrp)
#define _oss_enci_opentype     (*_g->ft.berTbl->_oss_enci_opentypep)
#define _oss_enci_pbit         (*_g->ft.berTbl->_oss_enci_pbitp)
#define _oss_enci_pstr         (*_g->ft.berTbl->_oss_enci_pstrp)
#define _oss_enci_real         (*_g->ft.berTbl->_oss_enci_realp)
#define _oss_enci_tag          (*_g->ft.berTbl->_oss_enci_tagp)
#define _oss_enci_uany         (*_g->ft.berTbl->_oss_enci_uanyp)
#define _oss_enci_ubit         (*_g->ft.berTbl->_oss_enci_ubitp)
#define _oss_enci_uint         (*_g->ft.berTbl->_oss_enci_uintp)
#define _oss_enci_uiobjid      (*_g->ft.berTbl->_oss_enci_uiobjidp)
#define _oss_enci_ulobjid      (*_g->ft.berTbl->_oss_enci_ulobjidp)
#define _oss_enci_uoct         (*_g->ft.berTbl->_oss_enci_uoctp)
#define _oss_enci_usobjid      (*_g->ft.berTbl->_oss_enci_usobjidp)
#define _oss_enci_ustr         (*_g->ft.berTbl->_oss_enci_ustrp)
#define _oss_enci_utime        (*_g->ft.berTbl->_oss_enci_utimep)
#define _oss_enci_vbit         (*_g->ft.berTbl->_oss_enci_vbitp)
#define _oss_enci_voct         (*_g->ft.berTbl->_oss_enci_voctp)
#define _oss_enci_vstr         (*_g->ft.berTbl->_oss_enci_vstrp)

#define _oss_encd_eobjid_ia    (*_g->ft.berTbl->_oss_encd_eobjid_iap)
#define _oss_dec_eobjid_ia     (*_g->ft.berTbl->_oss_dec_eobjid_iap)
#define _oss_encd_ereloid_ia   (*_g->ft.berTbl->_oss_encd_ereloid_iap)
#define _oss_dec_ereloid_ia    (*_g->ft.berTbl->_oss_dec_ereloid_iap)
#define _oss_encd_dubit_ia     (*_g->ft.berTbl->_oss_encd_dubit_iap)
#define _oss_encd_dvbit        (*_g->ft.berTbl->_oss_encd_dvbitp)
#define _oss_encd_dvbit_ia     (*_g->ft.berTbl->_oss_encd_dvbit_iap)

#define _oss_encd_utf8bmpstr_ia (*_g->ft.berTbl->_oss_encd_utf8bmpstr_iap)
#define _oss_encd_utf8unistr_ia (*_g->ft.berTbl->_oss_encd_utf8unistr_iap)
#define _oss_encd_utf8unbound_ia (*_g->ft.berTbl->_oss_encd_utf8unbound_iap)
#define _oss_dec_utf8bmpstr_ia (*_g->ft.berTbl->_oss_dec_utf8bmpstr_iap)
#define _oss_dec_utf8unistr_ia (*_g->ft.berTbl->_oss_dec_utf8unistr_iap)
#define _oss_dec_utf8unbound_ia (*_g->ft.berTbl->_oss_dec_utf8unbound_iap)
#define _oss_skip_past_item    (*_g->ft.berTbl->_oss_skip_past_itemp)
#define _oss_encd_bmpstr_ia    (*_g->ft.berTbl->_oss_encd_bmpstr_iap)
#define _oss_encd_unistr_ia    (*_g->ft.berTbl->_oss_encd_unistr_iap)
#define _oss_enc_push		(*_g->ft.berTbl->_oss_enc_pushp)
#define _oss_enc_pop		(*_g->ft.berTbl->_oss_enc_pop)
#define _oss_dec_bmpstr_ia     (*_g->ft.berTbl->_oss_dec_bmpstr_iap)
#define _oss_dec_unistr_ia     (*_g->ft.berTbl->_oss_dec_unistr_iap)
#define _oss_dec_rsf_ext       (*_g->ft.berTbl->_oss_dec_rsf_extp)
#define _oss_dec_bmpstr        (*_g->ft.berTbl->_oss_dec_bmpstrp)
#define _oss_dec_ubit          (*_g->ft.berTbl->_oss_dec_ubitp)
#define _oss_dec_unistr        (*_g->ft.berTbl->_oss_dec_unistrp)
#define _oss_dec_uoct          (*_g->ft.berTbl->_oss_dec_uoctp)
#define _oss_dec_ustr          (*_g->ft.berTbl->_oss_dec_ustrp)
#define _oss_dec_utf8bmpstr    (*_g->ft.berTbl->_oss_dec_utf8bmpstrp)
#define _oss_dec_utf8unbound   (*_g->ft.berTbl->_oss_dec_utf8unboundp)
#define _oss_dec_utf8unistr    (*_g->ft.berTbl->_oss_dec_utf8unistrp)
#define _oss_dec_eobjid        (*_g->ft.berTbl->_oss_dec_eobjidp)
#define _oss_dec_ereloid       (*_g->ft.berTbl->_oss_dec_ereloidp)
#define _oss_encd_bmpstr       (*_g->ft.berTbl->_oss_encd_bmpstrp)
#define _oss_encd_dubit        (*_g->ft.berTbl->_oss_encd_dubitp)
#define _oss_encd_huge_int     (*_g->ft.berTbl->_oss_encd_huge_intp)
#define _oss_encd_uhuge_int    (*_g->ft.berTbl->_oss_encd_uhuge_intp)
#define _oss_encd_ubit         (*_g->ft.berTbl->_oss_encd_ubitp)
#define _oss_encd_unistr       (*_g->ft.berTbl->_oss_encd_unistrp)
#define _oss_encd_uoct         (*_g->ft.berTbl->_oss_encd_uoctp)
#define _oss_encd_ustr         (*_g->ft.berTbl->_oss_encd_ustrp)
#define _oss_encd_utf8bmpstr   (*_g->ft.berTbl->_oss_encd_utf8bmpstrp)
#define _oss_encd_utf8unistr   (*_g->ft.berTbl->_oss_encd_utf8unistrp)
#define _oss_encd_utf8unbound  (*_g->ft.berTbl->_oss_encd_utf8unboundp)
#define _oss_encd_eobjid       (*_g->ft.berTbl->_oss_encd_eobjidp)
#define _oss_encd_ereloid      (*_g->ft.berTbl->_oss_encd_ereloidp)
#endif /* DLL_LINKAGE */


#endif /* OSS_TOED_BER */

#ifdef OSS_TOED_PER
/*****************************************************************************/
/*  TOED PER section                                                         */
/*****************************************************************************/

#define Aligned   1
#define Unaligned 0

    /*************************************************************************/
    /*      TOED PER functions section                                       */
    /*************************************************************************/

extern void DLL_ENTRY _oss_append(struct ossGlobal *g, unsigned char *field, unsigned long length,
			int align);

extern void DLL_ENTRY _oss_append_1bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_append_2bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_append_3bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_append_4bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_append_5bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_append_6bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_append_7bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_append_8bit_unaligned
	(struct ossGlobal *g, unsigned char field);

extern void DLL_ENTRY _oss_penc_unconstr_int(struct ossGlobal *g,
	    LONG_LONG value);

extern void DLL_ENTRY _oss_penc_semicon_int(struct ossGlobal *g,
	    LONG_LONG value, LONG_LONG lower_bound);

extern void DLL_ENTRY _oss_penc_semicon_uint(struct ossGlobal *g,
	    ULONG_LONG value, ULONG_LONG lower_bound);

extern void DLL_ENTRY _oss_penc_nonneg_int(struct ossGlobal *g,
	    ULONG_LONG value, ULONG_LONG range);

extern void DLL_ENTRY _oss_penc_nonneg_1int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_nonneg_2int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_nonneg_3int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_nonneg_4int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_nonneg_5int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_nonneg_6int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_nonneg_7int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_nonneg_8int
	(struct ossGlobal *g, unsigned int field);

extern void DLL_ENTRY _oss_penc_indeflen_int(struct ossGlobal *g,
	    ULONG_LONG value, ULONG_LONG range);

extern void DLL_ENTRY _oss_penc_small_int(struct ossGlobal *g, ULONG_LONG value);

extern void DLL_ENTRY _oss_penc_enum(struct ossGlobal *g, long data,
	 struct _enum_data *root,
	 struct _enum_data *extension);

extern void DLL_ENTRY _oss_penc_uenum(struct ossGlobal *g, unsigned long data,
	 struct _enum_data *root,
	 struct _enum_data *extension);

extern void DLL_ENTRY _oss_penc_real(struct ossGlobal *g, double value);
extern void DLL_ENTRY _oss_penc_creal(struct ossGlobal *g, char *value);
extern void DLL_ENTRY _oss_penc_mreal(struct ossGlobal *g, MixedReal value);
extern void DLL_ENTRY _oss_penc_mreal_ia(struct ossGlobal *g,
    enum MixedReal_kind kind, void *data);

extern void DLL_ENTRY _oss_penc_constr_bpbit(struct ossGlobal *g, void *value,
	ULONG_LONG lb, ULONG_LONG ub, ossBoolean NamedBits,
	ossBoolean Ext);

extern void DLL_ENTRY _oss_penc_constr_bpbit_l(struct ossGlobal *g,
    void *data, unsigned long lb, unsigned long ub,
    ossBoolean NamedBits, ossBoolean Ext);

extern void DLL_ENTRY _oss_penc_constr_pbit(struct ossGlobal *g, ULONG_LONG value,
	ULONG_LONG size, ULONG_LONG lb, ULONG_LONG ub, ossBoolean NamedBits,
	ossBoolean Ext);

extern void DLL_ENTRY _oss_penc_constr_pbit_l(struct ossGlobal *g,
    unsigned long value, unsigned long size, unsigned long lb,
    unsigned long ub, ossBoolean NamedBits, ossBoolean Ext);

extern void DLL_ENTRY _oss_penc_unconstr_pbit(struct ossGlobal *g, ULONG_LONG value,
	ULONG_LONG length, ULONG_LONG size, ossBoolean NamedBits);

extern unsigned long DLL_ENTRY _oss_penc_length(struct ossGlobal *g, ULONG_LONG length,
		  ULONG_LONG lb, ULONG_LONG ub, ossBoolean ext);

extern void DLL_ENTRY _oss_penc_unconstr_pbit_l(struct ossGlobal *g,
    unsigned long value, unsigned long length, unsigned long size,
    ossBoolean NamedBits);

extern void DLL_ENTRY _oss_penc_unconstr_bit_l(struct ossGlobal *g,
    unsigned char *value, unsigned long length, ossBoolean NamedBits);

extern unsigned long DLL_ENTRY _oss_penc_length_l(struct ossGlobal *g,
    unsigned long length, unsigned long lb, unsigned long ub, ossBoolean ext);

extern void DLL_ENTRY _oss_penc_constr_bit_l(struct ossGlobal *g,
    unsigned char *value, unsigned long length, unsigned long lb,
    unsigned long ub, ossBoolean NamedBits, unsigned long new_length);

extern unsigned long DLL_ENTRY _oss_pdec_small_len(struct ossGlobal *g);

extern void DLL_ENTRY _oss_penc_small_len_l(struct ossGlobal *g,
    unsigned long length);

extern void DLL_ENTRY _oss_penc_unconstr_oct_l(struct ossGlobal *g,
    unsigned char *value, unsigned long length);

void DLL_ENTRY _oss_append_8bit_aligned(struct ossGlobal *g,
    unsigned int field);
void DLL_ENTRY _oss_penc_int16_aligned(struct ossGlobal *g, unsigned int value);
extern void          DLL_ENTRY _oss_penc_small_len(struct ossGlobal *g,
							ULONG_LONG length);
extern void DLL_ENTRY _oss_penc_unconstr_oct(struct ossGlobal *g, unsigned char *value,
	 ULONG_LONG length);

extern void DLL_ENTRY _oss_penc_constr_oct(struct ossGlobal *g, unsigned char *value,
	 ULONG_LONG length, ULONG_LONG lb, ULONG_LONG ub);
extern void DLL_ENTRY _oss_fast_pop_global(struct ossGlobal *g);
extern void DLL_ENTRY _oss_fast_push_global(struct ossGlobal *g);

extern void DLL_ENTRY _oss_penc_objids(struct ossGlobal *g, unsigned short *value,
   unsigned long length);
extern void DLL_ENTRY _oss_penc_objidi(struct ossGlobal *g, unsigned int *value,
   unsigned long length);
extern void DLL_ENTRY _oss_penc_objidl(struct ossGlobal *g, unsigned long *value,
   unsigned long length);
extern void DLL_ENTRY _oss_penc_link_objids(struct ossGlobal *g, void *value);
extern void DLL_ENTRY _oss_penc_link_objidi(struct ossGlobal *g, void *value);
extern void DLL_ENTRY _oss_penc_link_objidl(struct ossGlobal *g, void *value);
extern void DLL_ENTRY _oss_penc_link_objidl_ia(struct ossGlobal *g,
    void *value, unsigned int offset);

extern void DLL_ENTRY _oss_penc_opentype(struct ossGlobal *g, void *value);
extern void DLL_ENTRY _oss_penc_nkmstr(struct ossGlobal *g, char *value, ULONG_LONG length);
extern void DLL_ENTRY _oss_penc_gtime(struct ossGlobal *g, GeneralizedTime *time);
extern void DLL_ENTRY _oss_penc_utime(struct ossGlobal *g, UTCTime *time);

extern void DLL_ENTRY _oss_penc_nkmstr_l(struct ossGlobal *g, char *value,
    unsigned long length);

extern void DLL_ENTRY _oss_penc_kmstr( struct ossGlobal *g,
    char *value,
    unsigned long length, const struct st_PER_String_Data *psd,
    unsigned int ext );
extern void DLL_ENTRY _oss_penc_bmpstr( struct ossGlobal *g,
    unsigned short *value,
    unsigned long length, const struct st_PER_String_Data *psd,
    unsigned int ext );
extern void DLL_ENTRY _oss_penc_unistr( struct ossGlobal *g,
    OSS_INT32 *value,
    unsigned long length, const struct st_PER_String_Data *psd,
    unsigned int ext );

extern void DLL_ENTRY _oss_penc_uany(struct ossGlobal *g, void *data );

extern void DLL_ENTRY _oss_penc_unconstr_huge(struct ossGlobal *g,
    void *data);
extern void DLL_ENTRY _oss_penc_semicon_huge(struct ossGlobal *g,
    void *data, LONG_LONG lb);

extern void DLL_ENTRY _oss_penc_unconstr_huge_ia(struct ossGlobal *g,
    unsigned char *value,
    unsigned long length);

extern void DLL_ENTRY _oss_penc_semicon_huge_ia(struct ossGlobal *g,
    unsigned char *value,
    unsigned long length
    , long lb
	     );

extern void DLL_ENTRY _oss_penc_eobjid(struct ossGlobal *g, void *data,
					long size_c);
extern void DLL_ENTRY _oss_penc_ereloid(struct ossGlobal *g, void *data,
					long size_c);

extern void DLL_ENTRY _oss_penc_eobjid_ia(struct ossGlobal *g,
    unsigned char *value, unsigned int length, long size_c);
extern void DLL_ENTRY _oss_penc_ereloid_ia(struct ossGlobal *g,
    unsigned char *value, unsigned int length, long size_c);

extern void DLL_ENTRY _oss_penc_utf8bmpstr(struct ossGlobal *g, unsigned short *value, ULONG_LONG length);
extern void DLL_ENTRY _oss_penc_utf8unistr(struct ossGlobal *g, OSS_INT32 *value, ULONG_LONG length);

extern void DLL_ENTRY _oss_penc_utf8bmpstr_l(struct ossGlobal *g,
    unsigned short *value, unsigned long length);
extern void DLL_ENTRY _oss_penc_utf8unistr_l(struct ossGlobal *g,
    OSS_INT32 *value, unsigned long length);
extern void DLL_ENTRY _oss_penc_indeflen_int_l(struct ossGlobal *g,
	    unsigned long value, unsigned long range);
extern void DLL_ENTRY _oss_penc_nonneg_int_l(struct ossGlobal *g,
	    unsigned long value, unsigned int range);
extern void DLL_ENTRY _oss_penc_semicon_int_l(struct ossGlobal *g,
	    long value, long lower_bound);
extern void DLL_ENTRY _oss_penc_small_int_l(struct ossGlobal *g, unsigned long value);
extern void DLL_ENTRY _oss_penc_unconstr_int_l(struct ossGlobal *g,
	    long value);
extern void DLL_ENTRY _oss_penc_constr_oct_l(struct ossGlobal *g, unsigned char *value,
	    unsigned long length, unsigned long lb, unsigned long ub);
extern void DLL_ENTRY _oss_penc_semicon_uint_l(struct ossGlobal *g,
	    unsigned long value, unsigned long lower_bound);

#define _oss_pe_t_utc(g, data, in) \
   ((*(data = in) == 'Z') ? data + 1 : (_oss_enc_error(g, _bad_time, 0L), data))
extern char * DLL_ENTRY _oss_pe_t_any_century(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pe_t_day_of_month(OssGlobal *g, char *data);
#define _oss_pe_t_any_date(g, data) \
	_oss_pe_t_day_of_month(g, _oss_pe_t_any_year_month(g, data))
extern char * DLL_ENTRY _oss_pe_t_any_year(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pe_t_day_of_year(OssGlobal *g, char *data);
#define _oss_pe_t_any_year_day(g, data) \
	_oss_pe_t_day_of_year(g, _oss_pe_t_any_year(g, data))
extern char * DLL_ENTRY _oss_pe_t_month(OssGlobal *g, char *data);
#define _oss_pe_t_any_year_month(g, data) \
	_oss_pe_t_month(g, _oss_pe_t_any_year(g, data))
extern char * DLL_ENTRY _oss_pe_t_week(OssGlobal *g, char *data);
#define _oss_pe_t_any_year_week(g, data) \
	_oss_pe_t_week(g, _oss_pe_t_any_year(g, data))
extern char * DLL_ENTRY _oss_pe_t_day_of_week(OssGlobal *g, char *data);
#define _oss_pe_t_any_year_week_day(g, data) \
	_oss_pe_t_day_of_week(g, _oss_pe_t_any_year_week(g, data))
extern char * DLL_ENTRY _oss_pe_t_century(OssGlobal *g, char *data);
#define _oss_pe_t_date(g, data) \
	_oss_pe_t_day_of_month(g, _oss_pe_t_year_month(g, data))
extern char * DLL_ENTRY _oss_pe_t_duration_interval(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pe_t_hours(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pe_t_time_diff(OssGlobal *g, char *data);
#define _oss_pe_t_hours_diff(g, data) \
	_oss_pe_t_time_diff(g, _oss_pe_t_hours(g, data))
#define _oss_pe_t_hours_diff_fraction(g, data, accuracy) \
	_oss_pe_t_time_diff(g, _oss_pe_t_hours_fraction(g, data, accuracy))
extern char * DLL_ENTRY _oss_pe_t_time_fraction(OssGlobal *g, char *data,
				unsigned int accuracy);
#define _oss_pe_t_hours_fraction(g, data, accuracy) \
	_oss_pe_t_time_fraction(g, _oss_pe_t_hours(g, data), accuracy)
#define _oss_pe_t_hours_utc(g, data) \
	_oss_pe_t_utc(g, data, _oss_pe_t_hours(g, data))
#define _oss_pe_t_hours_utc_fraction(g, data, accuracy) \
	_oss_pe_t_utc(g, data, _oss_pe_t_hours_fraction(g, data, accuracy))
extern char * DLL_ENTRY _oss_pe_t_mins_or_secs(OssGlobal *g, char *data,
				unsigned int bound);
#define _oss_pe_t_minutes(g, data) \
	_oss_pe_t_mins_or_secs(g, _oss_pe_t_hours(g, data), 59)
#define _oss_pe_t_minutes_diff(g, data) \
	_oss_pe_t_time_diff(g, _oss_pe_t_minutes(g, data))
#define _oss_pe_t_minutes_diff_fraction(g, data, accuracy) \
	_oss_pe_t_time_diff(g, _oss_pe_t_minutes_fraction(g, data, accuracy))
#define _oss_pe_t_minutes_fraction(g, data, accuracy) \
	_oss_pe_t_time_fraction(g, _oss_pe_t_minutes(g, data), accuracy)
#define _oss_pe_t_minutes_utc(g, data) \
	_oss_pe_t_utc(g, data, _oss_pe_t_minutes(g, data))
#define _oss_pe_t_minutes_utc_fraction(g, data, accuracy) \
	_oss_pe_t_utc(g, data, _oss_pe_t_minutes_fraction(g, data, accuracy))
extern char * DLL_ENTRY _oss_pe_t_mixed(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pe_t_recurrence(OssGlobal *g, char *data);
#define _oss_pe_t_daytime(g, data) \
	_oss_pe_t_mins_or_secs(g, _oss_pe_t_minutes(g, data), 60)
#define _oss_pe_t_daytime_diff(g, data) \
	_oss_pe_t_time_diff(g, _oss_pe_t_daytime(g, data))
#define _oss_pe_t_daytime_diff_fraction(g, data, accuracy) \
	_oss_pe_t_time_diff(g, _oss_pe_t_daytime_fraction(g, data, accuracy))
#define _oss_pe_t_daytime_fraction(g, data, accuracy) \
	_oss_pe_t_time_fraction(g, _oss_pe_t_daytime(g, data), accuracy)
#define _oss_pe_t_daytime_utc(g, data) \
	_oss_pe_t_utc(g, data, _oss_pe_t_daytime(g, data))
#define _oss_pe_t_daytime_utc_fraction(g, data, accuracy) \
	_oss_pe_t_utc(g, data, _oss_pe_t_daytime_fraction(g, data, accuracy))
extern char * DLL_ENTRY _oss_pe_t_year(OssGlobal *g, char *data);
#define _oss_pe_t_year_day(g, data) \
	_oss_pe_t_day_of_year(g, _oss_pe_t_year(g, data))
#define _oss_pe_t_year_month(g, data) \
	_oss_pe_t_month(g, _oss_pe_t_year(g, data))
#define _oss_pe_t_year_week(g, data) \
	_oss_pe_t_week(g, _oss_pe_t_year(g, data))
#define _oss_pe_t_year_week_day(g, data) \
	_oss_pe_t_day_of_week(g, _oss_pe_t_year_week(g, data))
#define _oss_set_t_se_points_in_use(c, v) \
	(c).reserved.fields.ext->timeSEPointsInUse = v

/* decoding functions */

extern unsigned char DLL_ENTRY _oss_get_bit(struct ossGlobal *g, int align);

extern void DLL_ENTRY _oss_get_bits(struct ossGlobal *g, unsigned char *field,
			unsigned long length, int align);

extern unsigned char DLL_ENTRY _oss_get_1bit_unaligned(struct ossGlobal *g);
extern unsigned char DLL_ENTRY _oss_get_2bit_unaligned(struct ossGlobal *g);
extern unsigned char DLL_ENTRY _oss_get_3bit_unaligned(struct ossGlobal *g);
extern unsigned char DLL_ENTRY _oss_get_4bit_unaligned(struct ossGlobal *g);
extern unsigned char DLL_ENTRY _oss_get_5bit_unaligned(struct ossGlobal *g);
extern unsigned char DLL_ENTRY _oss_get_6bit_unaligned(struct ossGlobal *g);
extern unsigned char DLL_ENTRY _oss_get_7bit_unaligned(struct ossGlobal *g);
extern unsigned char DLL_ENTRY _oss_get_8bit_unaligned(struct ossGlobal *g);

extern unsigned char DLL_ENTRY _oss_get_octet(struct ossGlobal *g, int align);

extern LONG_LONG DLL_ENTRY _oss_pdec_unconstr_int(struct ossGlobal *g);

extern LONG_LONG DLL_ENTRY _oss_pdec_semicon_int(struct ossGlobal *g,
	    LONG_LONG lower_bound);

extern ULONG_LONG DLL_ENTRY _oss_pdec_semicon_uint(struct ossGlobal *g,
	    ULONG_LONG lower_bound);

extern ULONG_LONG DLL_ENTRY _oss_pdec_nonneg_int(struct ossGlobal *g,
	    ULONG_LONG range);

extern ULONG_LONG DLL_ENTRY _oss_pdec_indeflen_int(struct ossGlobal *g,
	    ULONG_LONG range);

extern ULONG_LONG DLL_ENTRY _oss_pdec_small_int(struct ossGlobal *g);

extern LONG_LONG DLL_ENTRY _oss_pdec_unconstr_limited_int(struct ossGlobal *g,
	LONG_LONG lower_limit, LONG_LONG upper_limit);

extern LONG_LONG DLL_ENTRY _oss_pdec_semicon_limited_int(struct ossGlobal *g,
	LONG_LONG lower_bound, LONG_LONG lower_limit, LONG_LONG upper_limit);

extern ULONG_LONG DLL_ENTRY _oss_pdec_nonneg_limited_int(struct ossGlobal *g,
	ULONG_LONG range, LONG_LONG lower_limit, LONG_LONG upper_limit);

extern unsigned int DLL_ENTRY _oss_pdec_nonneg_1int(struct ossGlobal *g);
extern unsigned int DLL_ENTRY _oss_pdec_nonneg_2int(struct ossGlobal *g);
extern unsigned int DLL_ENTRY _oss_pdec_nonneg_3int(struct ossGlobal *g);
extern unsigned int DLL_ENTRY _oss_pdec_nonneg_4int(struct ossGlobal *g);
extern unsigned int DLL_ENTRY _oss_pdec_nonneg_5int(struct ossGlobal *g);
extern unsigned int DLL_ENTRY _oss_pdec_nonneg_6int(struct ossGlobal *g);
extern unsigned int DLL_ENTRY _oss_pdec_nonneg_7int(struct ossGlobal *g);
extern unsigned int DLL_ENTRY _oss_pdec_nonneg_8int(struct ossGlobal *g);

extern ULONG_LONG DLL_ENTRY _oss_pdec_indeflen_limited_int(struct ossGlobal *g,
	ULONG_LONG range, LONG_LONG lower_bound, LONG_LONG lower_limit,
	LONG_LONG upper_limit);

extern long DLL_ENTRY _oss_pdec_enum(struct ossGlobal *g,
	 struct _enum_data *root,
	 struct _enum_data *extension);

extern unsigned long DLL_ENTRY _oss_pdec_uenum(struct ossGlobal *g,
	 struct _enum_data *root,
	 struct _enum_data *extension);

extern double DLL_ENTRY _oss_pdec_binreal(struct ossGlobal *g, unsigned char s, long len);
extern void DLL_ENTRY _oss_pdec_chrreal(struct ossGlobal *g, unsigned char s, long len,
     double *num_out, unsigned char *str_out);

extern float     DLL_ENTRY _oss_pdec_freal(struct ossGlobal *g);
extern double    DLL_ENTRY _oss_pdec_real(struct ossGlobal *g);
extern char *    DLL_ENTRY _oss_pdec_creal(struct ossGlobal *g);
extern MixedReal DLL_ENTRY _oss_pdec_mreal(struct ossGlobal *g);
extern enum MixedReal_kind DLL_ENTRY _oss_pdec_mreal_ia
	(struct ossGlobal *g, void *data);

extern void DLL_ENTRY _oss_pdec_unconstr_ubit(struct ossGlobal *g, void *length,
	 unsigned char **value, int lengthsize);

extern unsigned long DLL_ENTRY _oss_pdec_length(struct ossGlobal *g,
    unsigned long lb, unsigned long ub, ossBoolean *last);

extern
	unsigned long
DLL_ENTRY _oss_pdec_unconstr_ubit_ia(struct ossGlobal *g,
	 unsigned char **value);

extern void DLL_ENTRY _oss_pdec_unconstr_vbit_ptr(struct ossGlobal *g, void **ptr,
	 int lengthsize);

extern unsigned long DLL_ENTRY _oss_pdec_unconstr_vbit_ptr_ia
	(struct ossGlobal *g, void *ptr, unsigned int offset);

extern void DLL_ENTRY _oss_pdec_unconstr_vbit(struct ossGlobal *g, void *length,
	 unsigned char *value, int lengthsize, ULONG_LONG datasize);

extern unsigned long  DLL_ENTRY _oss_pdec_unconstr_vbit_ia
    (struct ossGlobal *g, unsigned char *value, unsigned long datasize);

extern void DLL_ENTRY _oss_pdec_unconstr_pbit(struct ossGlobal *g, void *value,
	int size);

extern void DLL_ENTRY _oss_pdec_unconstr_bpbit(struct ossGlobal *g, unsigned char *value,
	long size);

extern void DLL_ENTRY _oss_pdec_constr_ubit(struct ossGlobal *g, void *length,
	 unsigned char **value, int lengthsize,
	 ULONG_LONG lb, ULONG_LONG ub);

extern
	unsigned long
DLL_ENTRY _oss_pdec_constr_ubit_ia(struct ossGlobal *g,
	unsigned char **value, unsigned long lb, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_constr_vbit(struct ossGlobal *g, void *length,
	unsigned char  *value, int lengthsize,
	ULONG_LONG lb, ULONG_LONG ub);

extern void DLL_ENTRY _oss_pdec_constr_vbit_ptr(struct ossGlobal *g, void **ptr,
	 int lengthsize, ULONG_LONG lb, ULONG_LONG ub);

extern unsigned long DLL_ENTRY _oss_pdec_constr_vbit_ia
    (struct ossGlobal *g,
	unsigned char *value, unsigned long lb, unsigned long ub);

extern unsigned long DLL_ENTRY _oss_pdec_constr_vbit_ptr_ia
	(struct ossGlobal *g, void *ptr, unsigned int offset,
		unsigned long lb, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_constr_pbit_l(struct ossGlobal *g,
	void *value, int size, unsigned long lb, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_constr_bpbit(struct ossGlobal *g, unsigned char *value,
	int size, ULONG_LONG lb, ULONG_LONG ub);

extern void DLL_ENTRY _oss_pdec_constr_bpbit_l(struct ossGlobal *g,
	unsigned char *value, int size, unsigned long lb, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_unconstr_uoct(struct ossGlobal *g, void *length,
	 unsigned char **value, int lengthsize);

extern
	unsigned long
DLL_ENTRY _oss_pdec_unconstr_uoct_ia(struct ossGlobal *g,
	 unsigned char **value);

extern void DLL_ENTRY _oss_pdec_unconstr_voct_ptr(struct ossGlobal *g, void **ptr,
	 int lengthsize);

extern unsigned long DLL_ENTRY _oss_pdec_unconstr_voct_ptr_ia
	(struct ossGlobal *g, void *ptr, unsigned int offset);

extern void DLL_ENTRY _oss_pdec_constr_voct_ptr(struct ossGlobal *g, void **ptr,
	 int lengthsize, ULONG_LONG lb, ULONG_LONG ub);

extern unsigned long DLL_ENTRY _oss_pdec_constr_voct_ptr_ia
	(struct ossGlobal *g, void *ptr, unsigned int offset,
		unsigned long lb, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_constr_uoct(struct ossGlobal *g, void *length,
	unsigned char **value, int lengthsize, ULONG_LONG lb, ULONG_LONG ub);

extern
	unsigned long
DLL_ENTRY _oss_pdec_constr_uoct_ia(struct ossGlobal *g,
	unsigned char **value, unsigned long lb, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_constr_voct(struct ossGlobal *g, void *length,
	unsigned char  *value, int lengthsize, ULONG_LONG lb, ULONG_LONG ub);

extern unsigned long DLL_ENTRY _oss_pdec_constr_voct_ia
    (struct ossGlobal *g,
	unsigned char *value, unsigned long lb, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_unconstr_voct(struct ossGlobal *g, void *length,
	unsigned char  *value, int lengthsize, ULONG_LONG ub);

extern unsigned long DLL_ENTRY _oss_pdec_unconstr_voct_ia
    (struct ossGlobal *g, unsigned char *value, unsigned long ub);

extern void DLL_ENTRY _oss_pdec_push(struct ossGlobal *g);
extern void DLL_ENTRY _oss_pdec_pop(struct ossGlobal *g);

extern unsigned long DLL_ENTRY _oss_pdec_eap(struct ossGlobal *g, unsigned char **ext);
extern void DLL_ENTRY _oss_pdec_eas(struct ossGlobal *g, unsigned char *ext,
	 unsigned long count, unsigned long ea_num);

extern unsigned int DLL_ENTRY _oss_copy_preamble(struct ossGlobal *g,
	 unsigned char *ext, unsigned long count, unsigned long ea_num,
	void *un_ea_num, unsigned char **un_ea_pr, unsigned int length_size);

extern void DLL_ENTRY _oss_pdec_lsof(struct ossGlobal *g, unsigned long *count,
    ULONG_LONG lb, ULONG_LONG ub, unsigned char ext,
    ossBoolean *last);

extern void DLL_ENTRY _oss_pdec_usof(struct ossGlobal *g, unsigned long *count,
    unsigned char **value, int lengthsize, long itemsize,
    ULONG_LONG lb, ULONG_LONG ub, unsigned char ext,
    ossBoolean *last);

extern void DLL_ENTRY _oss_pdec_lsof_l(struct ossGlobal *g,
    unsigned long *count, unsigned long lb, unsigned long ub,
			unsigned char ext, ossBoolean *last);
extern void DLL_ENTRY _oss_pdec_unbnd_frg(OssGlobal *g,
	void *ptr, unsigned long new_size, unsigned long old_size,
    ossBoolean *last);
extern void DLL_ENTRY _oss_pdec_asof(struct ossGlobal *g, unsigned long *count,
    int lengthsize,
    ULONG_LONG lb, ULONG_LONG ub, unsigned char ext,
    ossBoolean *last);

extern void DLL_ENTRY _oss_pdec_asof_ptr(struct ossGlobal *g, void **ptr,
    int lengthsize, long itemsize, long prefixsize,
    ossBoolean *last);

extern void DLL_ENTRY _oss_pdec_asof_ptr_ia(struct ossGlobal *g,
    void *ptr, int lengthsize, long itemsize, unsigned int offset,
    ossBoolean *last);

extern void DLL_ENTRY _oss_pdec_aobjids(struct ossGlobal *g, unsigned short *value,
    unsigned short *count, unsigned short array_size);

extern void DLL_ENTRY _oss_pdec_aobjidi(struct ossGlobal *g, unsigned int   *value,
    unsigned short *count, unsigned short array_size);

extern void DLL_ENTRY _oss_pdec_aobjidl(struct ossGlobal *g, unsigned long  *value,
    unsigned short *count, unsigned short array_size);

extern void DLL_ENTRY _oss_pdec_aobjids_ptr(struct ossGlobal *g, void **ptr);
extern void * DLL_ENTRY _oss_pdec_aobjids_ptr_ia(struct ossGlobal *g,
    unsigned int offset);
extern void DLL_ENTRY _oss_pdec_aobjidi_ptr(struct ossGlobal *g, void **ptr);
extern void * DLL_ENTRY _oss_pdec_aobjidi_ptr_ia(struct ossGlobal *g,
    unsigned int offset);
extern void DLL_ENTRY _oss_pdec_aobjidl_ptr(struct ossGlobal *g, void **ptr);
extern void * DLL_ENTRY _oss_pdec_aobjidl_ptr_ia(struct ossGlobal *g,
    unsigned int offset);

extern void DLL_ENTRY _oss_pdec_uobjids(struct ossGlobal *g, unsigned short **value,
	 unsigned short *count);
extern void DLL_ENTRY _oss_pdec_uobjidi(struct ossGlobal *g, unsigned int **value,
	 unsigned short *count);
extern void DLL_ENTRY _oss_pdec_uobjidl(struct ossGlobal *g, unsigned long **value,
	 unsigned short *count);

extern void DLL_ENTRY _oss_pdec_link_objids(struct ossGlobal *g, void **ptr);
extern void DLL_ENTRY _oss_pdec_link_objidi(struct ossGlobal *g, void **ptr);
extern void DLL_ENTRY _oss_pdec_link_objidl(struct ossGlobal *g, void **ptr);
extern void * DLL_ENTRY _oss_pdec_link_objidl_ia(struct ossGlobal *g,
    unsigned int offset);

extern void DLL_ENTRY _oss_pdec_nt_kmstr( struct ossGlobal *g, void *ptr,
    const struct st_PER_String_Data *psd ); 

extern unsigned long DLL_ENTRY _oss_pdec_vap_kmstr( struct ossGlobal *g,
    void *ptr, unsigned int offset,
    const struct st_PER_String_Data *psd );

extern unsigned long DLL_ENTRY _oss_pdec_va_kmstr( struct ossGlobal *g,
    char *ptr, const struct st_PER_String_Data *psd );

extern void DLL_ENTRY _oss_pdec_ntp_kmstr( struct ossGlobal *g, char **ptr,
    const struct st_PER_String_Data *psd );

extern
	unsigned long
DLL_ENTRY _oss_pdec_ub_kmstr( struct ossGlobal *g, char **ptr,
    const struct st_PER_String_Data *psd );

extern
	unsigned long
DLL_ENTRY _oss_pdec_bmpstr( struct ossGlobal *g, unsigned short **ptr,
    const struct st_PER_String_Data *psd );

extern
	unsigned long
DLL_ENTRY _oss_pdec_unistr( struct ossGlobal *g, OSS_INT32 **ptr,
    const struct st_PER_String_Data *psd );

extern void DLL_ENTRY _oss_pdec_ntp_nkmstr(struct ossGlobal *g, char **ptr);
extern void DLL_ENTRY _oss_pdec_nt_nkmstr(struct ossGlobal *g, char *value, unsigned long ub);
extern void DLL_ENTRY _oss_pdec_pad_nkmstr(struct ossGlobal *g, char *value, unsigned long ub);
extern void DLL_ENTRY _oss_pdec_vap_nkmstr(struct ossGlobal *g, void **ptr, int lengthsize);
extern unsigned long DLL_ENTRY _oss_pdec_vap_nkmstr_ia
	(struct ossGlobal *g, void *ptr, unsigned int offset);
extern void DLL_ENTRY _oss_pdec_va_nkmstr(struct ossGlobal *g, void *length, char *value,
	 int lengthsize, unsigned long ub);
extern unsigned long DLL_ENTRY _oss_pdec_va_nkmstr_ia
    (struct ossGlobal *g, char *value, unsigned long ub);
extern void DLL_ENTRY _oss_pdec_ub_nkmstr(struct ossGlobal *g, void *length, char **ptr,
     int lengthsize);

extern
	unsigned long
DLL_ENTRY _oss_pdec_ub_nkmstr_ia(struct ossGlobal *g, char **ptr);

extern void DLL_ENTRY _oss_pdec_pad_kmstr( struct ossGlobal *g, void *ptr,
    const struct st_PER_String_Data *psd );

extern void DLL_ENTRY _oss_pdec_opentype(struct ossGlobal *g, void *data );
extern void DLL_ENTRY _oss_pdec_uany(struct ossGlobal *g, void *data );

extern void DLL_ENTRY _oss_pdec_gtime(struct ossGlobal *g, GeneralizedTime *data);
extern void DLL_ENTRY _oss_pdec_utime(struct ossGlobal *g, UTCTime *data);

extern struct _char_data *_oss_get_char_data(struct ossGlobal *g, int index);

extern void DLL_ENTRY _oss_pdec_unconstr_huge(struct ossGlobal *g, void *data);
extern void DLL_ENTRY _oss_pdec_semicon_huge(struct ossGlobal *g,
    void *data, LONG_LONG lb);

extern
	unsigned int
DLL_ENTRY _oss_pdec_unconstr_huge_ia(struct ossGlobal *g, void *data);

extern
	unsigned int
DLL_ENTRY _oss_pdec_semicon_huge_ia(struct ossGlobal *g,
	void *data
	      , long lb
		       );
extern void DLL_ENTRY _oss_pdec_eobjid(struct ossGlobal *g, void *data,
				long size_c);
extern void DLL_ENTRY _oss_pdec_ereloid(struct ossGlobal *g, void *data,
				long size_c);

extern
	unsigned int
DLL_ENTRY _oss_pdec_eobjid_ia(struct ossGlobal *g, void *data
					    , long size_c
	);

extern
	unsigned int
DLL_ENTRY _oss_pdec_ereloid_ia(struct ossGlobal *g, void *data
					     , long size_c
		);

extern void DLL_ENTRY _oss_pdec_sot(struct ossGlobal *g);

extern ossBoolean DLL_ENTRY _oss_lnchk(unsigned char *value,
	ULONG_LONG *lenptr, unsigned short endpoint, ULONG_LONG lb,
	ULONG_LONG ub);

extern void DLL_ENTRY _oss_pdec_utf8bmpstr(struct ossGlobal *g, void *length, unsigned short **ptr,
     int lengthsize);
extern void DLL_ENTRY _oss_pdec_utf8unistr(struct ossGlobal *g, void *length, OSS_INT32 **ptr,
     int lengthsize);

extern
	unsigned long
DLL_ENTRY _oss_pdec_utf8bmpstr_ia
    (struct ossGlobal *g, unsigned short **ptr);

extern
	unsigned long
DLL_ENTRY _oss_pdec_utf8unistr_ia
    (struct ossGlobal *g, OSS_INT32 **ptr);

extern unsigned long DLL_ENTRY _oss_pdec_indeflen_int_l(struct ossGlobal *g,
	    unsigned long range);
extern unsigned long DLL_ENTRY _oss_pdec_indeflen_limited_int_l(struct ossGlobal *g,
	    unsigned long range, long lower_bound, long lower_limit,
	    long upper_limit);
extern unsigned long DLL_ENTRY _oss_pdec_nonneg_int_l(struct ossGlobal *g,
	    unsigned int range);
extern unsigned long DLL_ENTRY _oss_pdec_nonneg_limited_int_l(struct ossGlobal *g,
	    unsigned int range, long lower_limit, long upper_limit);
extern long DLL_ENTRY _oss_pdec_semicon_int_l(struct ossGlobal *g,
	    long lower_bound);
extern long DLL_ENTRY _oss_pdec_semicon_limited_int_l(struct ossGlobal *g,
	    long lower_bound, long lower_limit, long upper_limit);
extern unsigned long DLL_ENTRY _oss_pdec_semicon_uint_l(struct ossGlobal *g,
	    unsigned long lower_bound);
extern unsigned long DLL_ENTRY _oss_pdec_small_int_l(struct ossGlobal *g);
extern long DLL_ENTRY _oss_pdec_unconstr_int_l(struct ossGlobal *g);
extern long DLL_ENTRY _oss_pdec_unconstr_limited_int_l(struct ossGlobal *g,
	    long lower_limit, long upper_limit);
extern void DLL_ENTRY _oss_pdec_constr_uoct_l(struct ossGlobal *g, void *length,
	    unsigned char **value, int lengthsize, unsigned long lb, unsigned long ub);
extern void DLL_ENTRY _oss_pdec_array_frg (OssGlobal *g,
		 void *ptr, unsigned long frgSz, unsigned long arrSz,
				unsigned int offset, ossBoolean *last);
extern unsigned char _oss_log2table[257];

extern char * DLL_ENTRY _oss_pd_t_century(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_any_century(OssGlobal *g, char *data,
							unsigned int max_d_num);
extern char * DLL_ENTRY _oss_pd_t_year(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_any_year(OssGlobal *g, char *data,
							unsigned int max_d_num);
extern char * DLL_ENTRY _oss_pd_t_month(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_day_of_month(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_day_of_year(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_week(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_day_of_week(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_hours(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_time_diff(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_mins_or_secs(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_time_fraction(OssGlobal *g, char *data,
							unsigned int accuracy);
extern char * DLL_ENTRY _oss_pd_t_recurrence(OssGlobal *g, char *data,
							unsigned int max_r_num);
extern char * DLL_ENTRY _oss_pd_t_duration_interval(OssGlobal *g, char *data);
extern char * DLL_ENTRY _oss_pd_t_mixed(OssGlobal *g, char *data);
#define _oss_pd_t_year_month(g, data) \
	_oss_pd_t_month(g, _oss_pd_t_year(g, data))
#define _oss_pd_t_any_year_month(g, data, max_yd_num) \
	_oss_pd_t_month(g, _oss_pd_t_any_year(g, data, max_yd_num))
#define _oss_pd_t_date(g, data) \
	_oss_pd_t_day_of_month(g, _oss_pd_t_year_month(g, data))
#define _oss_pd_t_any_date(g, data, max_yd_num) \
	_oss_pd_t_day_of_month(g, _oss_pd_t_any_year_month(g, data, max_yd_num))
#define _oss_pd_t_year_day(g, data) \
	_oss_pd_t_day_of_year(g, _oss_pd_t_year(g, data))
#define _oss_pd_t_any_year_day(g, data, max_yd_num) \
	_oss_pd_t_day_of_year(g, _oss_pd_t_any_year(g, data, max_yd_num))
#define _oss_pd_t_year_week(g, data) \
	_oss_pd_t_week(g, _oss_pd_t_year(g, data))
#define _oss_pd_t_any_year_week(g, data, max_yd_num) \
	_oss_pd_t_week(g, _oss_pd_t_any_year(g, data, max_yd_num))
#define _oss_pd_t_year_week_day(g, data) \
	_oss_pd_t_day_of_week(g, _oss_pd_t_year_week(g, data))
#define _oss_pd_t_any_year_week_day(g, data, max_yd_num) \
	_oss_pd_t_day_of_week(g, _oss_pd_t_any_year_week(g, data, max_yd_num))
#define _oss_pd_t_utc(data, in) (*(data = in) = 'Z', data + 1)
#define _oss_pd_t_hours_utc(g, data) \
	_oss_pd_t_utc(data, _oss_pd_t_hours(g, data))
#define _oss_pd_t_hours_diff(g, data) \
	_oss_pd_t_time_diff(g, _oss_pd_t_hours(g, data))
#define _oss_pd_t_minutes(g, data) \
	_oss_pd_t_mins_or_secs(g, _oss_pd_t_hours(g, data))
#define _oss_pd_t_minutes_utc(g, data) \
	_oss_pd_t_utc(data, _oss_pd_t_minutes(g, data))
#define _oss_pd_t_minutes_diff(g, data) \
	_oss_pd_t_time_diff(g, _oss_pd_t_minutes(g, data))
#define _oss_pd_t_daytime(g, data) \
	_oss_pd_t_mins_or_secs(g, _oss_pd_t_minutes(g, data))
#define _oss_pd_t_daytime_utc(g, data) \
	_oss_pd_t_utc(data, _oss_pd_t_daytime(g, data))
#define _oss_pd_t_daytime_diff(g, data) \
	_oss_pd_t_time_diff(g, _oss_pd_t_daytime(g, data))
#define _oss_pd_t_hours_fraction(g, data, accuracy) \
	_oss_pd_t_time_fraction(g, _oss_pd_t_hours(g, data), accuracy)
#define _oss_pd_t_hours_utc_fraction(g, data, accuracy) \
	_oss_pd_t_utc(data, _oss_pd_t_hours_fraction(g, data, accuracy))
#define _oss_pd_t_hours_diff_fraction(g, data, accuracy) \
	_oss_pd_t_time_diff(g, _oss_pd_t_hours_fraction(g, data, accuracy))
#define _oss_pd_t_minutes_fraction(g, data, accuracy) \
	_oss_pd_t_time_fraction(g, _oss_pd_t_minutes(g, data), accuracy)
#define _oss_pd_t_minutes_utc_fraction(g, data, accuracy) \
	_oss_pd_t_utc(data, _oss_pd_t_minutes_fraction(g, data, accuracy))
#define _oss_pd_t_minutes_diff_fraction(g, data, accuracy) \
	_oss_pd_t_time_diff(g, _oss_pd_t_minutes_fraction(g, data, accuracy))
#define _oss_pd_t_daytime_fraction(g, data, accuracy) \
	_oss_pd_t_time_fraction(g, _oss_pd_t_daytime(g, data), accuracy)
#define _oss_pd_t_daytime_utc_fraction(g, data, accuracy) \
	_oss_pd_t_utc(data, _oss_pd_t_daytime_fraction(g, data, accuracy))
#define _oss_pd_t_daytime_diff_fraction(g, data, accuracy) \
	_oss_pd_t_time_diff(g, _oss_pd_t_daytime_fraction(g, data, accuracy))


    /*************************************************************************/
    /*      TOED PER DLL table defines section                               */
    /*************************************************************************/

	/*
	 * The following macros must be #defined if you compile the ASN.1
	 * compiler generated files (.c files) and link-edit with the import
	 * library ossapit.lib, i.e the OSS DLLs ossapit.dll, apit.dll,
	 * and toedber.dll/toedper.dll are used.  The following must not
	 * be #defined if you link-edit with the static library toedcode.lib
	 * or toedcomd.lib.  If you use /MD and link with toedcomd.lib,
	 * #define ONE_DLL on the C-compiler command line to disable the
	 * below macros.
	 */
#ifdef DLL_LINKAGE

#define _oss_penc_unconstr_int   (*_g->ft.perTbl->_oss_penc_unconstr_intp)
#define _oss_pdec_unconstr_int   (*_g->ft.perTbl->_oss_pdec_unconstr_intp)
#define _oss_penc_kmstr          (*_g->ft.perTbl->_oss_penc_kmstrp)
#define _oss_pdec_ub_kmstr	 (*_g->ft.perTbl->_oss_pdec_ub_kmstrp)
#define _oss_pdec_ntp_kmstr      (*_g->ft.perTbl->_oss_pdec_ntp_kmstrp)
#define _oss_append              (*_g->ft.perTbl->_oss_appendp)
#define _oss_penc_unconstr_bit_l (*_g->ft.perTbl->_oss_penc_unconstr_bit_lp)
#define _oss_penc_constr_bit_l     (*_g->ft.perTbl->_oss_penc_constr_bit_lp)
#define _oss_penc_unconstr_oct_l (*_g->ft.perTbl->_oss_penc_unconstr_oct_lp)
#define _oss_penc_constr_oct     (*_g->ft.perTbl->_oss_penc_constr_octp)
#define _oss_penc_link_objids    (*_g->ft.perTbl->_oss_penc_link_objidsp)
#define _oss_penc_objids         (*_g->ft.perTbl->_oss_penc_objidsp)
#define _oss_penc_link_objidl    (*_g->ft.perTbl->_oss_penc_link_objidlp)
#define _oss_penc_link_objidl_ia (*_g->ft.perTbl->_oss_penc_link_objidl_iap)
#define _oss_penc_objidl         (*_g->ft.perTbl->_oss_penc_objidlp)
#define _oss_penc_link_objidi    (*_g->ft.perTbl->_oss_penc_link_objidip)
#define _oss_penc_objidi         (*_g->ft.perTbl->_oss_penc_objidip)
#define _oss_penc_nkmstr_l       (*_g->ft.perTbl->_oss_penc_nkmstr_lp)
#define _oss_penc_opentype       (*_g->ft.perTbl->_oss_penc_opentypep)
#define _oss_penc_nonneg_int     (*_g->ft.perTbl->_oss_penc_nonneg_intp)
#define _oss_penc_real           (*_g->ft.perTbl->_oss_penc_realp)
#define _oss_penc_uenum          (*_g->ft.perTbl->_oss_penc_uenump)
#define _oss_penc_length_l       (*_g->ft.perTbl->_oss_penc_length_lp)
#define _oss_penc_gtime          (*_g->ft.perTbl->_oss_penc_gtimep)
#define _oss_penc_utime          (*_g->ft.perTbl->_oss_penc_utimep)
#define _oss_get_bit             (*_g->ft.perTbl->_oss_get_bitp)
#define _oss_pdec_unconstr_ubit_ia (*_g->ft.perTbl->_oss_pdec_unconstr_ubit_iap)
#define _oss_pdec_constr_ubit_ia (*_g->ft.perTbl->_oss_pdec_constr_ubit_iap)
#define _oss_pdec_constr_pbit_l  (*_g->ft.perTbl->_oss_pdec_constr_pbit_lp)
#define _oss_pdec_constr_bpbit   (*_g->ft.perTbl->_oss_pdec_constr_bpbitp)
#define _oss_pdec_constr_bpbit_l (*_g->ft.perTbl->_oss_pdec_constr_bpbit_lp)
#define _oss_pdec_unconstr_vbit_ptr (*_g->ft.perTbl->_oss_pdec_unconstr_vbit_ptrp)
#define _oss_pdec_unconstr_vbit_ptr_ia (*_g->ft.perTbl->_oss_pdec_unconstr_vbit_ptr_iap)
#define _oss_pdec_unconstr_vbit  (*_g->ft.perTbl->_oss_pdec_unconstr_vbitp)
#define _oss_pdec_unconstr_vbit_ia (*_g->ft.perTbl->_oss_pdec_unconstr_vbit_iap)
#define _oss_pdec_unconstr_uoct_ia  (*_g->ft.perTbl->_oss_pdec_unconstr_uoct_iap)
#define _oss_pdec_constr_voct    (*_g->ft.perTbl->_oss_pdec_constr_voctp)
#define _oss_pdec_constr_voct_ia (*_g->ft.perTbl->_oss_pdec_constr_voct_iap)
#define _oss_pdec_constr_voct_ptr (*_g->ft.perTbl->_oss_pdec_constr_voct_ptrp)
#define _oss_pdec_constr_voct_ptr_ia (*_g->ft.perTbl->_oss_pdec_constr_voct_ptr_iap)
#define _oss_pdec_unconstr_voct_ptr (*_g->ft.perTbl->_oss_pdec_unconstr_voct_ptrp)
#define _oss_pdec_unconstr_voct_ptr_ia (*_g->ft.perTbl->_oss_pdec_unconstr_voct_ptr_iap)
#define _oss_pdec_unconstr_voct (*_g->ft.perTbl->_oss_pdec_unconstr_voctp)
#define _oss_pdec_unconstr_voct_ia (*_g->ft.perTbl->_oss_pdec_unconstr_voct_iap)
#define _oss_pdec_constr_uoct_ia    (*_g->ft.perTbl->_oss_pdec_constr_uoct_iap)
#define _oss_pdec_constr_vbit    (*_g->ft.perTbl->_oss_pdec_constr_vbitp)
#define _oss_pdec_constr_vbit_ia (*_g->ft.perTbl->_oss_pdec_constr_vbit_iap)
#define _oss_pdec_constr_vbit_ptr (*_g->ft.perTbl->_oss_pdec_constr_vbit_ptrp)
#define _oss_pdec_constr_vbit_ptr_ia (*_g->ft.perTbl->_oss_pdec_constr_vbit_ptr_iap)
#define _oss_pdec_link_objids    (*_g->ft.perTbl->_oss_pdec_link_objidsp)
#define _oss_pdec_link_objidl    (*_g->ft.perTbl->_oss_pdec_link_objidlp)
#define _oss_pdec_link_objidl_ia (*_g->ft.perTbl->_oss_pdec_link_objidl_iap)
#define _oss_pdec_link_objidi    (*_g->ft.perTbl->_oss_pdec_link_objidip)
#define _oss_pdec_uobjids        (*_g->ft.perTbl->_oss_pdec_uobjidsp)
#define _oss_pdec_uobjidl        (*_g->ft.perTbl->_oss_pdec_uobjidlp)
#define _oss_pdec_uobjidi        (*_g->ft.perTbl->_oss_pdec_uobjidip)
#define _oss_pdec_aobjids        (*_g->ft.perTbl->_oss_pdec_aobjidsp)
#define _oss_pdec_aobjidl        (*_g->ft.perTbl->_oss_pdec_aobjidlp)
#define _oss_pdec_aobjidi        (*_g->ft.perTbl->_oss_pdec_aobjidip)
#define _oss_pdec_aobjids_ptr    (*_g->ft.perTbl->_oss_pdec_aobjids_ptrp)
#define _oss_pdec_aobjids_ptr_ia (*_g->ft.perTbl->_oss_pdec_aobjids_ptr_iap)
#define _oss_pdec_aobjidl_ptr    (*_g->ft.perTbl->_oss_pdec_aobjidl_ptrp)
#define _oss_pdec_aobjidl_ptr_ia (*_g->ft.perTbl->_oss_pdec_aobjidl_ptr_iap)
#define _oss_pdec_aobjidi_ptr    (*_g->ft.perTbl->_oss_pdec_aobjidi_ptrp)
#define _oss_pdec_aobjidi_ptr_ia (*_g->ft.perTbl->_oss_pdec_aobjidi_ptr_iap)
#define _oss_pdec_ntp_nkmstr     (*_g->ft.perTbl->_oss_pdec_ntp_nkmstrp)
#define _oss_pdec_nt_nkmstr      (*_g->ft.perTbl->_oss_pdec_nt_nkmstrp)
#define _oss_pdec_pad_nkmstr     (*_g->ft.perTbl->_oss_pdec_pad_nkmstrp)
#define _oss_pdec_opentype       (*_g->ft.perTbl->_oss_pdec_opentypep)
#define _oss_pdec_nonneg_int     (*_g->ft.perTbl->_oss_pdec_nonneg_intp)
#define _oss_get_bits            (*_g->ft.perTbl->_oss_get_bitsp)
#define _oss_pdec_freal          (*_g->ft.perTbl->_oss_pdec_frealp)
#define _oss_pdec_real           (*_g->ft.perTbl->_oss_pdec_realp)
#define _oss_pdec_uenum          (*_g->ft.perTbl->_oss_pdec_uenump)
#define _oss_pdec_asof           (*_g->ft.perTbl->_oss_pdec_asofp)
#define _oss_pdec_lsof_l         (*_g->ft.perTbl->_oss_pdec_lsof_lp)
#define _oss_pdec_array_frg	 (*_g->ft.perTbl->_oss_pdec_array_frgp)
#define _oss_pdec_unbnd_frg	 (*_g->ft.perTbl->_oss_pdec_unbnd_frgp)
#define _oss_pdec_utime          (*_g->ft.perTbl->_oss_pdec_utimep)
#define _oss_pdec_gtime          (*_g->ft.perTbl->_oss_pdec_gtimep)
#define _oss_pdec_asof_ptr       (*_g->ft.perTbl->_oss_pdec_asof_ptrp)
#define _oss_pdec_asof_ptr_ia    (*_g->ft.perTbl->_oss_pdec_asof_ptr_iap)
#define _oss_pdec_nt_kmstr       (*_g->ft.perTbl->_oss_pdec_nt_kmstrp)
#define _oss_pdec_va_kmstr       (*_g->ft.perTbl->_oss_pdec_va_kmstrp)
#define _oss_pdec_vap_kmstr      (*_g->ft.perTbl->_oss_pdec_vap_kmstrp)
#define _oss_pdec_pad_kmstr      (*_g->ft.perTbl->_oss_pdec_pad_kmstrp)
#define _oss_pdec_binreal        (*_g->ft.perTbl->_oss_pdec_binrealp)
#define _oss_pdec_eap            (*_g->ft.perTbl->_oss_pdec_eapp)
#define _oss_pdec_eas            (*_g->ft.perTbl->_oss_pdec_easp)
#define _oss_pdec_chrreal        (*_g->ft.perTbl->_oss_pdec_chrrealp)
#define _oss_pdec_enum           (*_g->ft.perTbl->_oss_pdec_enump)
#define _oss_pdec_indeflen_int   (*_g->ft.perTbl->_oss_pdec_indeflen_intp)
#define _oss_penc_indeflen_int   (*_g->ft.perTbl->_oss_penc_indeflen_intp)
#define _oss_pdec_bmpstr	 (*_g->ft.perTbl->_oss_pdec_bmpstrp)
#define _oss_pdec_creal          (*_g->ft.perTbl->_oss_pdec_crealp)
#define _oss_pdec_mreal          (*_g->ft.perTbl->_oss_pdec_mrealp)
#define _oss_pdec_mreal_ia       (*_g->ft.perTbl->_oss_pdec_mreal_iap)
#define _oss_penc_mreal          (*_g->ft.perTbl->_oss_penc_mrealp)
#define _oss_penc_mreal_ia       (*_g->ft.perTbl->_oss_penc_mreal_iap)
#define _oss_pdec_length         (*_g->ft.perTbl->_oss_pdec_lengthp)
#define _oss_pdec_pop            (*_g->ft.perTbl->_oss_pdec_popp)
#define _oss_pdec_push           (*_g->ft.perTbl->_oss_pdec_pushp)
#define _oss_pdec_uany           (*_g->ft.perTbl->_oss_pdec_uanyp)
#define _oss_penc_uany           (*_g->ft.perTbl->_oss_penc_uanyp)
#define _oss_pdec_unistr	 (*_g->ft.perTbl->_oss_pdec_unistrp)
#define _oss_penc_unistr	 (*_g->ft.perTbl->_oss_penc_unistrp)
#define _oss_pdec_semicon_int    (*_g->ft.perTbl->_oss_pdec_semicon_intp)
#define _oss_penc_semicon_int    (*_g->ft.perTbl->_oss_penc_semicon_intp)
#define _oss_pdec_semicon_uint   (*_g->ft.perTbl->_oss_pdec_semicon_uintp)
#define _oss_penc_semicon_uint   (*_g->ft.perTbl->_oss_penc_semicon_uintp)
#define _oss_pdec_small_int      (*_g->ft.perTbl->_oss_pdec_small_intp)
#define _oss_penc_small_int      (*_g->ft.perTbl->_oss_penc_small_intp)
#define _oss_pdec_small_len      (*_g->ft.perTbl->_oss_pdec_small_lenp)
#define _oss_penc_small_len_l    (*_g->ft.perTbl->_oss_penc_small_len_lp)
#define _oss_pdec_subid          (*_g->ft.perTbl->_oss_pdec_subidp)
#define _oss_penc_subid          (*_g->ft.perTbl->_oss_penc_subidp)
#define _oss_pdec_ub_nkmstr_ia   (*_g->ft.perTbl->_oss_pdec_ub_nkmstr_iap)
#define _oss_pdec_unconstr_bpbit (*_g->ft.perTbl->_oss_pdec_unconstr_bpbitp)
#define _oss_pdec_unconstr_pbit  (*_g->ft.perTbl->_oss_pdec_unconstr_pbitp)
#define _oss_penc_unconstr_pbit  (*_g->ft.perTbl->_oss_penc_unconstr_pbitp)
#define _oss_penc_unconstr_pbit_l (*_g->ft.perTbl->_oss_penc_unconstr_pbit_lp)
#define _oss_pdec_unconstr_huge_ia (*_g->ft.perTbl->_oss_pdec_unconstr_huge_iap)
#define _oss_penc_unconstr_huge_ia (*_g->ft.perTbl->_oss_penc_unconstr_huge_iap)
#define _oss_pdec_vap_nkmstr     (*_g->ft.perTbl->_oss_pdec_vap_nkmstrp)
#define _oss_pdec_vap_nkmstr_ia  (*_g->ft.perTbl->_oss_pdec_vap_nkmstr_iap)
#define _oss_pdec_va_nkmstr      (*_g->ft.perTbl->_oss_pdec_va_nkmstrp)
#define _oss_pdec_va_nkmstr_ia   (*_g->ft.perTbl->_oss_pdec_va_nkmstr_iap)
#define _oss_penc_constr_bpbit   (*_g->ft.perTbl->_oss_penc_constr_bpbitp)
#define _oss_penc_constr_bpbit_l (*_g->ft.perTbl->_oss_penc_constr_bpbit_lp)
#define _oss_penc_constr_pbit    (*_g->ft.perTbl->_oss_penc_constr_pbitp)
#define _oss_penc_constr_pbit_l  (*_g->ft.perTbl->_oss_penc_constr_pbit_lp)
#define _oss_penc_creal          (*_g->ft.perTbl->_oss_penc_crealp)
#define _oss_penc_enum           (*_g->ft.perTbl->_oss_penc_enump)
#define _oss_get_octet           (*_g->ft.perTbl->_oss_get_octetp)
#define _oss_penc_eobjid_ia      (*_g->ft.perTbl->_oss_penc_eobjid_iap)
#define _oss_pdec_eobjid_ia      (*_g->ft.perTbl->_oss_pdec_eobjid_iap)
#define _oss_penc_ereloid_ia     (*_g->ft.perTbl->_oss_penc_ereloid_iap)
#define _oss_pdec_ereloid_ia     (*_g->ft.perTbl->_oss_pdec_ereloid_iap)
#define _oss_penc_semicon_huge_ia (*_g->ft.perTbl->_oss_penc_semicon_huge_iap)
#define _oss_pdec_semicon_huge_ia (*_g->ft.perTbl->_oss_pdec_semicon_huge_iap)
#define _oss_pdec_sot            (*_g->ft.perTbl->_oss_pdec_sotp)
#define _oss_lnchk               (*_g->ft.perTbl->_oss_lnchkp)
#define _oss_penc_utf8bmpstr_l   (*_g->ft.perTbl->_oss_penc_utf8bmpstr_lp)
#define _oss_penc_utf8unistr_l   (*_g->ft.perTbl->_oss_penc_utf8unistr_lp)
#define _oss_pdec_utf8bmpstr_ia  (*_g->ft.perTbl->_oss_pdec_utf8bmpstr_iap)
#define _oss_pdec_utf8unistr_ia  (*_g->ft.perTbl->_oss_pdec_utf8unistr_iap)
#define _oss_penc_bmpstr         (*_g->ft.perTbl->_oss_penc_bmpstrp)
#define _oss_copy_preamble       (*_g->ft.perTbl->_oss_copy_preamblep)
#define _oss_pdec_semicon_limited_int    (*_g->ft.perTbl->_oss_pdec_semicon_limited_intp)
#define _oss_pdec_indeflen_limited_int   (*_g->ft.perTbl->_oss_pdec_indeflen_limited_intp)
#define _oss_pdec_unconstr_limited_int   (*_g->ft.perTbl->_oss_pdec_unconstr_limited_intp)
#define _oss_pdec_nonneg_limited_int     (*_g->ft.perTbl->_oss_pdec_nonneg_limited_intp)
#define _oss_set_outmem_p                (*_g->ft.apiTbl->_oss_set_outmem_pp)
#define _oss_get_1bit_unaligned          (*_g->ft.perTbl->_oss_get_1bit_unalignedp)
#define _oss_get_2bit_unaligned          (*_g->ft.perTbl->_oss_get_2bit_unalignedp)
#define _oss_get_3bit_unaligned          (*_g->ft.perTbl->_oss_get_3bit_unalignedp)
#define _oss_get_4bit_unaligned          (*_g->ft.perTbl->_oss_get_4bit_unalignedp)
#define _oss_get_5bit_unaligned          (*_g->ft.perTbl->_oss_get_5bit_unalignedp)
#define _oss_get_6bit_unaligned          (*_g->ft.perTbl->_oss_get_6bit_unalignedp)
#define _oss_get_7bit_unaligned          (*_g->ft.perTbl->_oss_get_7bit_unalignedp)
#define _oss_get_8bit_unaligned          (*_g->ft.perTbl->_oss_get_8bit_unalignedp)
#define _oss_pdec_nonneg_1int		 (*_g->ft.perTbl->_oss_pdec_nonneg_1intp)
#define _oss_pdec_nonneg_2int            (*_g->ft.perTbl->_oss_pdec_nonneg_2intp)
#define _oss_pdec_nonneg_3int            (*_g->ft.perTbl->_oss_pdec_nonneg_3intp)
#define _oss_pdec_nonneg_4int            (*_g->ft.perTbl->_oss_pdec_nonneg_4intp)
#define _oss_pdec_nonneg_5int            (*_g->ft.perTbl->_oss_pdec_nonneg_5intp)
#define _oss_pdec_nonneg_6int            (*_g->ft.perTbl->_oss_pdec_nonneg_6intp)
#define _oss_pdec_nonneg_7int            (*_g->ft.perTbl->_oss_pdec_nonneg_7intp)
#define _oss_pdec_nonneg_8int            (*_g->ft.perTbl->_oss_pdec_nonneg_8intp)
#define _oss_append_1bit_unaligned	 (*_g->ft.perTbl->_oss_append_1bit_unalignedp)
#define _oss_append_2bit_unaligned	 (*_g->ft.perTbl->_oss_append_2bit_unalignedp)
#define _oss_append_3bit_unaligned	 (*_g->ft.perTbl->_oss_append_3bit_unalignedp)
#define _oss_append_4bit_unaligned	 (*_g->ft.perTbl->_oss_append_4bit_unalignedp)
#define _oss_append_5bit_unaligned	 (*_g->ft.perTbl->_oss_append_5bit_unalignedp)
#define _oss_append_6bit_unaligned	 (*_g->ft.perTbl->_oss_append_6bit_unalignedp)
#define _oss_append_7bit_unaligned	 (*_g->ft.perTbl->_oss_append_7bit_unalignedp)
#define _oss_append_8bit_unaligned	 (*_g->ft.perTbl->_oss_append_8bit_unalignedp)
#define _oss_penc_nonneg_1int		 (*_g->ft.perTbl->_oss_penc_nonneg_1intp)
#define _oss_penc_nonneg_2int		 (*_g->ft.perTbl->_oss_penc_nonneg_2intp)
#define _oss_penc_nonneg_3int		 (*_g->ft.perTbl->_oss_penc_nonneg_3intp)
#define _oss_penc_nonneg_4int		 (*_g->ft.perTbl->_oss_penc_nonneg_4intp)
#define _oss_penc_nonneg_5int		 (*_g->ft.perTbl->_oss_penc_nonneg_5intp)
#define _oss_penc_nonneg_6int		 (*_g->ft.perTbl->_oss_penc_nonneg_6intp)
#define _oss_penc_nonneg_7int		 (*_g->ft.perTbl->_oss_penc_nonneg_7intp)
#define _oss_penc_nonneg_8int		 (*_g->ft.perTbl->_oss_penc_nonneg_8intp)
#define _oss_pdec_indeflen_int_l         (*_g->ft.perTbl->_oss_pdec_indeflen_int_lp)
#define _oss_pdec_indeflen_limited_int_l (*_g->ft.perTbl->_oss_pdec_indeflen_limited_int_lp)
#define _oss_pdec_nonneg_int_l           (*_g->ft.perTbl->_oss_pdec_nonneg_int_lp)
#define _oss_pdec_nonneg_limited_int_l   (*_g->ft.perTbl->_oss_pdec_nonneg_limited_int_lp)
#define _oss_pdec_semicon_int_l          (*_g->ft.perTbl->_oss_pdec_semicon_int_lp)
#define _oss_pdec_semicon_limited_int_l  (*_g->ft.perTbl->_oss_pdec_semicon_limited_int_lp)
#define _oss_pdec_semicon_uint_l         (*_g->ft.perTbl->_oss_pdec_semicon_uint_lp)
#define _oss_pdec_small_int_l            (*_g->ft.perTbl->_oss_pdec_small_int_lp)
#define _oss_pdec_unconstr_int_l         (*_g->ft.perTbl->_oss_pdec_unconstr_int_lp)
#define _oss_pdec_unconstr_limited_int_l (*_g->ft.perTbl->_oss_pdec_unconstr_limited_int_lp)
#define _oss_pdec_constr_uoct_l          (*_g->ft.perTbl->_oss_pdec_constr_uoct_lp)
#define _oss_penc_indeflen_int_l         (*_g->ft.perTbl->_oss_penc_indeflen_int_lp)
#define _oss_penc_nonneg_int_l           (*_g->ft.perTbl->_oss_penc_nonneg_int_lp)
#define _oss_penc_semicon_int_l          (*_g->ft.perTbl->_oss_penc_semicon_int_lp)
#define _oss_penc_semicon_uint_l         (*_g->ft.perTbl->_oss_penc_semicon_uint_lp)
#define _oss_penc_small_int_l            (*_g->ft.perTbl->_oss_penc_small_int_lp)
#define _oss_penc_unconstr_int_l         (*_g->ft.perTbl->_oss_penc_unconstr_int_lp)
#define _oss_penc_constr_oct_l           (*_g->ft.perTbl->_oss_penc_constr_oct_lp)
#define _oss_fast_push_global    	 (*_g->ft.perTbl->_oss_fast_push_globalp)
#define _oss_fast_pop_global    	 (*_g->ft.perTbl->_oss_fast_pop_globalp)

#define _oss_pdec_semicon_huge   (*_g->ft.perTbl->_oss_pdec_semicon_hugep)
#define _oss_pdec_unconstr_huge  (*_g->ft.perTbl->_oss_pdec_unconstr_hugep)
#define _oss_pdec_eobjid         (*_g->ft.perTbl->_oss_pdec_eobjidp)
#define _oss_pdec_ereloid        (*_g->ft.perTbl->_oss_pdec_ereloidp)
#define _oss_pdec_constr_ubit    (*_g->ft.perTbl->_oss_pdec_constr_ubitp)
#define _oss_pdec_constr_uoct    (*_g->ft.perTbl->_oss_pdec_constr_uoctp)
#define _oss_pdec_utf8bmpstr     (*_g->ft.perTbl->_oss_pdec_utf8bmpstrp)
#define _oss_pdec_utf8unistr     (*_g->ft.perTbl->_oss_pdec_utf8unistrp)
#define _oss_pdec_ub_nkmstr      (*_g->ft.perTbl->_oss_pdec_ub_nkmstrp)
#define _oss_pdec_unconstr_ubit  (*_g->ft.perTbl->_oss_pdec_unconstr_ubitp)
#define _oss_pdec_unconstr_uoct  (*_g->ft.perTbl->_oss_pdec_unconstr_uoctp)
#define _oss_pdec_usof           (*_g->ft.perTbl->_oss_pdec_usofp)
#define _oss_penc_semicon_huge   (*_g->ft.perTbl->_oss_penc_semicon_hugep)
#define _oss_penc_unconstr_huge  (*_g->ft.perTbl->_oss_penc_unconstr_hugep)
#define _oss_penc_eobjid         (*_g->ft.perTbl->_oss_penc_eobjidp)
#define _oss_penc_ereloid        (*_g->ft.perTbl->_oss_penc_ereloidp)
#define _oss_pdec_lsof           (*_g->ft.perTbl->_oss_pdec_lsofp)
#define _oss_append_8bit_aligned (*_g->ft.perTbl->_oss_append_8bit_alignedp)
#define _oss_penc_length         (*_g->ft.perTbl->_oss_penc_lengthp)
#define _oss_penc_nkmstr         (*_g->ft.perTbl->_oss_penc_nkmstrp)
#define _oss_penc_utf8bmpstr     (*_g->ft.perTbl->_oss_penc_utf8bmpstrp)
#define _oss_penc_utf8unistr     (*_g->ft.perTbl->_oss_penc_utf8unistrp)
#define _oss_penc_small_len      (*_g->ft.perTbl->_oss_penc_small_lenp)
#define _oss_penc_int16_aligned  (*_g->ft.perTbl->_oss_penc_int16_alignedp)
#define _oss_penc_unconstr_oct   (*_g->ft.perTbl->_oss_penc_unconstr_octp)
#define _oss_pe_t_century        (*_g->ft.perTbl->_oss_pe_t_centuryp)
#define _oss_pe_t_any_century    (*_g->ft.perTbl->_oss_pe_t_any_centuryp)
#define _oss_pe_t_year           (*_g->ft.perTbl->_oss_pe_t_yearp)
#define _oss_pe_t_any_year       (*_g->ft.perTbl->_oss_pe_t_any_yearp)
#define _oss_pe_t_month          (*_g->ft.perTbl->_oss_pe_t_monthp)
#define _oss_pe_t_day_of_month   (*_g->ft.perTbl->_oss_pe_t_day_of_monthp)
#define _oss_pe_t_week           (*_g->ft.perTbl->_oss_pe_t_weekp)
#define _oss_pe_t_day_of_week    (*_g->ft.perTbl->_oss_pe_t_day_of_weekp)
#define _oss_pe_t_day_of_year    (*_g->ft.perTbl->_oss_pe_t_day_of_yearp)
#define _oss_pe_t_duration_interval (*_g->ft.perTbl->_oss_pe_t_duration_intervalp)
#define _oss_pe_t_hours          (*_g->ft.perTbl->_oss_pe_t_hoursp)
#define _oss_pe_t_time_diff      (*_g->ft.perTbl->_oss_pe_t_time_diffp)
#define _oss_pe_t_time_fraction  (*_g->ft.perTbl->_oss_pe_t_time_fractionp)
#define _oss_pe_t_mins_or_secs   (*_g->ft.perTbl->_oss_pe_t_mins_or_secsp)
#define _oss_pe_t_mixed          (*_g->ft.perTbl->_oss_pe_t_mixedp)
#define _oss_pe_t_recurrence     (*_g->ft.perTbl->_oss_pe_t_recurrencep)
#define _oss_pd_t_century        (*_g->ft.perTbl->_oss_pd_t_centuryp)
#define _oss_pd_t_any_century    (*_g->ft.perTbl->_oss_pd_t_any_centuryp)
#define _oss_pd_t_year           (*_g->ft.perTbl->_oss_pd_t_yearp)
#define _oss_pd_t_any_year       (*_g->ft.perTbl->_oss_pd_t_any_yearp)
#define _oss_pd_t_month          (*_g->ft.perTbl->_oss_pd_t_monthp)
#define _oss_pd_t_day_of_month   (*_g->ft.perTbl->_oss_pd_t_day_of_monthp)
#define _oss_pd_t_week           (*_g->ft.perTbl->_oss_pd_t_weekp)
#define _oss_pd_t_day_of_week    (*_g->ft.perTbl->_oss_pd_t_day_of_weekp)
#define _oss_pd_t_day_of_year    (*_g->ft.perTbl->_oss_pd_t_day_of_yearp)
#define _oss_pd_t_duration_interval (*_g->ft.perTbl->_oss_pd_t_duration_intervalp)
#define _oss_pd_t_hours          (*_g->ft.perTbl->_oss_pd_t_hoursp)
#define _oss_pd_t_time_diff      (*_g->ft.perTbl->_oss_pd_t_time_diffp)
#define _oss_pd_t_time_fraction  (*_g->ft.perTbl->_oss_pd_t_time_fractionp)
#define _oss_pd_t_mixed          (*_g->ft.perTbl->_oss_pd_t_mixedp)
#define _oss_pd_t_recurrence     (*_g->ft.perTbl->_oss_pd_t_recurrencep)
#define _oss_pd_t_mins_or_secs   (*_g->ft.perTbl->_oss_pd_t_mins_or_secsp)

#endif /* DLL_LINKAGE */


#endif /* OSS_TOED_PER */

#if defined(OSS_TOED_XER) || defined(OSS_TOED_EXER)
long	DLL_ENTRY _oss_tex_count_bits (unsigned char *value, long bit_len);
long	DLL_ENTRY _oss_tex_count_chars (char *value, long max_len);
#endif

#ifdef OSS_TOED_XER
/*****************************************************************************/
/*  TOED XER section                                                         */
/*****************************************************************************/
    /*************************************************************************/
    /*      TOED XER functions section                                       */
    /*************************************************************************/

extern void DLL_ENTRY _oss_allocate_memblock(struct ossGlobal *g,
			struct _mem_block_ **tmp_mem, void **value, long used);
extern void DLL_ENTRY _oss_consolidate_memblock(struct ossGlobal *g,
			void **value, long itemsize, long ecount,
			struct _mem_block_ *tmp_mem,long bcnt,long bcount,
			long bindex, int offset, OSS_UINT32 ubound,
			OSS_UINT32 flags);
extern void * DLL_ENTRY _oss_consolidate_memblock_fast(struct ossGlobal *g,
			long itemsize, long ecount, struct _mem_block_ *tmp_mem,
			long bcnt, long bcount, long bindex, int offset);

/* TOED XER encoder definitions and declarations */
void	DLL_ENTRY _oss_tex_open_tag (OssGlobal * _g, char * tag);
void	DLL_ENTRY _oss_tex_close_tag (OssGlobal * _g, char * tag);
void	DLL_ENTRY _oss_tex_empty_or_id_tag (OssGlobal * _g, char * tag);
void	DLL_ENTRY _oss_tex_open_tagN (OssGlobal * _g, char * tag, int taglen);
void	DLL_ENTRY _oss_tex_close_tagN (OssGlobal * _g, char * tag, int taglen);
void	DLL_ENTRY _oss_tex_empty_or_id_tagN (OssGlobal * _g, char * tag, int taglen);
void	DLL_ENTRY _oss_tex_format_tagN (OssGlobal *g, char *tag, int taglen);
long	DLL_ENTRY _oss_tex_curr_enclen (OssGlobal * _g);
void	DLL_ENTRY _oss_tex_format_tag (OssGlobal *g, char *tag);
void	DLL_ENTRY _oss_tex_indent_dlt (OssGlobal * _g, int dlt);
void	DLL_ENTRY _oss_tex_indent (OssGlobal * _g);
void	DLL_ENTRY _oss_tex_lint (OssGlobal *_g, long value);
void	DLL_ENTRY _oss_tex_ulint (OssGlobal *_g, unsigned long value);
void	DLL_ENTRY _oss_tex_llint (OssGlobal *_g, LONG_LONG value);
void	DLL_ENTRY _oss_tex_ullint (OssGlobal *_g, unsigned LONG_LONG value);
void	DLL_ENTRY _oss_tex_hint (OssGlobal *g, unsigned char *value,
		long length);
void	DLL_ENTRY _oss_tex_ntime (OssGlobal *_g, char *data, size_t inlen, ossBoolean is_utc);

void	DLL_ENTRY _oss_tex_time (OssGlobal *_g, GeneralizedTime *data,
		ossBoolean is_utc);
void	DLL_ENTRY _oss_tex_date_time(OssGlobal *_g, char *data, int kind);
void	DLL_ENTRY _oss_tex_enum(OssGlobal *_g, _oss_enumInfo *_vN, long value, int mode);
void	DLL_ENTRY _oss_tex_format_enum(OssGlobal *_g, char * name, int mode);

void	DLL_ENTRY _oss_tex_dreal (OssGlobal *_g, double value);
void	DLL_ENTRY _oss_tex_freal (OssGlobal *_g, float value);
void	DLL_ENTRY _oss_tex_creal (OssGlobal *_g, char *value);

void	DLL_ENTRY _oss_tex_eobjid (OssGlobal *_g, unsigned char * value, long length,
		long max_count, ossBoolean relative);
void	DLL_ENTRY _oss_tex_aobjids(OssGlobal *_g, unsigned short * value, long count,
		long max_count);
void	DLL_ENTRY _oss_tex_aobjidi(OssGlobal *_g, unsigned int * value, long count,
		long max_count);
void	DLL_ENTRY _oss_tex_aobjidl(OssGlobal *_g, unsigned long * value, long count,
		long max_count);
void	DLL_ENTRY _oss_tex_lobjids(OssGlobal *_g, void * value, long max_count);
void	DLL_ENTRY _oss_tex_lobjidi(OssGlobal *_g, void * value, long max_count);
void	DLL_ENTRY _oss_tex_lobjidl(OssGlobal *_g, void * value, long max_count);

void	DLL_ENTRY _oss_tex_format_char (OssGlobal *_g, unsigned char *value, long len);
void	DLL_ENTRY _oss_tex_format_char_t (OssGlobal *_g, unsigned char *value, long len);
void	DLL_ENTRY _oss_tex_format_bit (OssGlobal *_g, unsigned char *value, long bit_len);
void	DLL_ENTRY _oss_tex_format_pbit (OssGlobal *g, OSS_UINT32 value, long bit_len);
void	DLL_ENTRY _oss_tex_format_octet (OssGlobal *_g, unsigned char *value, unsigned long length);
void	DLL_ENTRY _oss_tex_prolog (OssGlobal *_g);
void	DLL_ENTRY _oss_tex_opentype (OssGlobal *_g, void *data);
void	DLL_ENTRY _oss_tex_anyopentype (OssGlobal *_g, unsigned char *data, long length);

void	DLL_ENTRY _oss_tex_2byte_char (OssGlobal *_g, unsigned short *value, long length);
void	DLL_ENTRY _oss_tex_4byte_char (OssGlobal *_g, OSS_UINT32 *value, long length);

/* TOED XER decoder definitions and declarations */

/* kinds of lexem */
#define L_BYTE		0
#define L_START		1
#define L_STARTEND	2
#define L_END		4
#define L_COMMENT	5
#define L_HEX_BYTE	0x10
#define L_KEEP		0x80

#define _oss_tdx_present_field(mask)	\
		if (_present_fields & mask) \
		    _oss_dec_error(_g, _field_repeat, 0L); \
		_present_fields |= mask;

#define _oss_tdx_present_field_arr(n, mask)	\
		if (_present_fields[n] & mask) \
		    _oss_dec_error(_g, _field_repeat, 0L); \
		_present_fields[n] |= mask;

long		DLL_ENTRY _oss_tex_append (OssGlobal *g, unsigned char * data, long num);
void 		DLL_ENTRY _oss_tdx_open_tag(_Xml_Dec_Env *env, char *tagname);
    /* read open tag with the specified name */
void 		DLL_ENTRY _oss_tdx_empty_tag(_Xml_Dec_Env *env, char *tagname);
    /* read empty tag with the specified name */
void 		DLL_ENTRY _oss_tdx_close_tag(_Xml_Dec_Env *env, char *tagname);
    /* read close tag with the specified name */
ossBoolean	DLL_ENTRY _oss_tdx_open_or_empty_tag(_Xml_Dec_Env *env,
			char *tagname);
    /* read open or empty tag with the specified name (TRUE if empty) */
ossBoolean	DLL_ENTRY _oss_tdx_open_field(_Xml_Dec_Env *env,
			struct _string_search_data *f,	short *f_idx);
void		DLL_ENTRY _oss_tdx_skip_block(_Xml_Dec_Env  *env);
unsigned long 	DLL_ENTRY _oss_tdx_rsf_alt(_Xml_Dec_Env  *env, void **_data);
void            DLL_ENTRY _oss_tdx_rsf_ext(_Xml_Dec_Env  *env, 
			void **_data, unsigned int *count);
void		DLL_ENTRY _oss_tdx_prolog(_Xml_Dec_Env  *env);
unsigned char	DLL_ENTRY _oss_tdx_lexem_lookahead(_Xml_Dec_Env  *env);
unsigned long	DLL_ENTRY _oss_tdx_allocate_array_sof(_Xml_Dec_Env  *_env,
			void **data, int offset, long itemsize);
unsigned long	DLL_ENTRY _oss_tdx_allocate_unbounded_sof(_Xml_Dec_Env  *_env,
			void **value, long itemsize, OSS_UINT32 ubound,
			OSS_UINT32 flags);
void		DLL_ENTRY _oss_tdx_opentype(_Xml_Dec_Env  *_env, void *data,
			OSS_UINT32 flags);

ossBoolean 	DLL_ENTRY _oss_tdx_bool(_Xml_Dec_Env *env);
OSS_INT32 	DLL_ENTRY _oss_tdx_enum(_Xml_Dec_Env *env,
			struct _string_search_data * data);

LONG_LONG	DLL_ENTRY _oss_tdx_llint(_Xml_Dec_Env *env);
ULONG_LONG	DLL_ENTRY _oss_tdx_ullint(_Xml_Dec_Env *env);
long		DLL_ENTRY _oss_tdx_lint(_Xml_Dec_Env *env, long lb, long ub);
unsigned long	DLL_ENTRY _oss_tdx_ulint(_Xml_Dec_Env *env, unsigned long lb);
unsigned long	DLL_ENTRY _oss_tdx_hint(_Xml_Dec_Env *env, void **value,
			OSS_UINT32 flags);

double		DLL_ENTRY _oss_tdx_dreal(_Xml_Dec_Env *env);
float		DLL_ENTRY _oss_tdx_freal(_Xml_Dec_Env *env);
char *		DLL_ENTRY _oss_tdx_creal(_Xml_Dec_Env *env);
void		DLL_ENTRY _oss_tdx_mreal(_Xml_Dec_Env *env, MixedReal *data);
enum MixedReal_kind DLL_ENTRY _oss_tdx_mreal_ia(_Xml_Dec_Env *env, void *data);

void		DLL_ENTRY _oss_tdx_char1_nullterm(_Xml_Dec_Env *env,
			void *storage, OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_char1_unbounded(_Xml_Dec_Env *env,
			void **value, OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_char1_pvarying(_Xml_Dec_Env *env,
			void **data, int offset, OSS_UINT32 ubound,
			OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_char1_varying(_Xml_Dec_Env *env,
			void *value, OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_char2(_Xml_Dec_Env *env, void **value,
			OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_char4(_Xml_Dec_Env *env, void **value,
			OSS_UINT32 ubound, OSS_UINT32 flags);

unsigned long	DLL_ENTRY _oss_tdx_octets_unbounded(_Xml_Dec_Env *env,
			void **value, OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_octets_pvarying(_Xml_Dec_Env *env,
			void **data, int offset, OSS_UINT32 ubound,
			OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_octets_varying(_Xml_Dec_Env *env,
			void *value, OSS_UINT32 ubound, OSS_UINT32 flags);

unsigned long	DLL_ENTRY _oss_tdx_bits_unbounded(_Xml_Dec_Env *env,
			void **value, OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_bits_pvarying(_Xml_Dec_Env *env,
			void **data, int offset, OSS_UINT32 ubound,
			OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_bits_varying(_Xml_Dec_Env *env,
			void *value, OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned long	DLL_ENTRY _oss_tdx_bits_padded(_Xml_Dec_Env *env,
			int lengthsize, OSS_UINT32 flags);

OSS_UINT32	DLL_ENTRY _oss_tdx_objid_encoded(_Xml_Dec_Env *env,
			void *data, OSS_UINT32 ubound, OSS_UINT32 flags);
void		DLL_ENTRY _oss_tdx_objid_linked(_Xml_Dec_Env *env,
			void **data, unsigned short nodesize,
			OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned short	DLL_ENTRY _oss_tdx_objid_unbounded(_Xml_Dec_Env *env,
			void **value, unsigned short nodesize,
			OSS_UINT32 ubound, OSS_UINT32 flags);
unsigned short	DLL_ENTRY _oss_tdx_objid_array(_Xml_Dec_Env *env, void *value,
			OSS_UINT32 ubound, unsigned short nodesize);
unsigned short	DLL_ENTRY _oss_tdx_objid_parray(_Xml_Dec_Env *env, void **data,
			int offset, unsigned short nodesize);

void		DLL_ENTRY _oss_tdx_gtime(_Xml_Dec_Env *env, void *storage);
void		DLL_ENTRY _oss_tdx_utime(_Xml_Dec_Env *env, void *storage);
void	        DLL_ENTRY _oss_tdx_date_time(_Xml_Dec_Env *env, char **data,
			int kind);

    /*************************************************************************/
    /*      TOED XER DLL table defines section                               */
    /*************************************************************************/

	/*
	 * The following macros must be #defined if you compile the ASN.1
	 * compiler generated files (.c files) and link-edit with the import
	 * library ossapit.lib or ossapi.lib, i.e the OSS DLLs ossapit.dll,
	 * toedapi.dll and toedxer.dll are used with your TOED application,
	 * or ossapi.dll, soedapi.dll soedxer.dll or soedxers.dll are used.
	 * The following must not be #defined if you link-edit with
	 * the static library toedcode.lib, toedcomd.lib, soeddefa.lib,
	 * soeddemd.lib, soeddsmd.lib, soeddspa.lib. If you use /MD and
	 * link with toedcomd.lib, soeddemd.lib or soeddsmd.lib
	 * #define ONE_DLL on the C-compiler command line to disable the
	 * below macros.
	 */
#if defined(DLL_LINKAGE)

#define _oss_tex_append (*_g->ft.xerTbl->_oss_tex_appendp)
#define _oss_tex_open_tag (*_g->ft.xerTbl->_oss_tex_open_tagp)
#define _oss_tex_close_tag (*_g->ft.xerTbl->_oss_tex_close_tagp)
#define _oss_tex_empty_or_id_tag (*_g->ft.xerTbl->_oss_tex_empty_or_id_tagp)
#define _oss_tex_open_tagN (*_g->ft.xerTbl->_oss_tex_open_tagNp)
#define _oss_tex_close_tagN (*_g->ft.xerTbl->_oss_tex_close_tagNp)
#define _oss_tex_empty_or_id_tagN (*_g->ft.xerTbl->_oss_tex_empty_or_id_tagNp)
#define _oss_tex_format_tagN (*_g->ft.xerTbl->_oss_tex_format_tagNp)
#define _oss_tex_curr_enclen (*_g->ft.xerTbl->_oss_tex_curr_enclenp)
#define _oss_tex_ntime (*_g->ft.xerTbl->_oss_tex_ntimep)
#define _oss_tex_format_tag (*_g->ft.xerTbl->_oss_tex_format_tagp)
#define _oss_tex_date_time (*_g->ft.xerTbl->_oss_tex_date_timep)
#define _oss_tdx_date_time (*_g->ft.xerTbl->_oss_tdx_date_timep)
#define _oss_tex_indent (*_g->ft.xerTbl->_oss_tex_indentp)
#define _oss_tex_indent_dlt (*_g->ft.xerTbl->_oss_tex_indent_dltp)
#define _oss_tex_count_bits (*_g->ft.xerTbl->_oss_tex_count_bitsp)
#define _oss_tex_count_chars (*_g->ft.xerTbl->_oss_tex_count_charsp)
#define _oss_tex_lint (*_g->ft.xerTbl->_oss_tex_lintp)
#define _oss_tex_ulint (*_g->ft.xerTbl->_oss_tex_ulintp)
#define _oss_tex_llint (*_g->ft.xerTbl->_oss_tex_llintp)
#define _oss_tex_ullint (*_g->ft.xerTbl->_oss_tex_ullintp)
#define _oss_tex_hint (*_g->ft.xerTbl->_oss_tex_hintp)
#define _oss_tex_time (*_g->ft.xerTbl->_oss_tex_timep)
#define _oss_tex_enum (*_g->ft.xerTbl->_oss_tex_enump)
#define _oss_tex_format_enum (*_g->ft.xerTbl->_oss_tex_format_enump)
#define _oss_tex_dreal (*_g->ft.xerTbl->_oss_tex_drealp)
#define _oss_tex_freal (*_g->ft.xerTbl->_oss_tex_frealp)
#define _oss_tex_creal (*_g->ft.xerTbl->_oss_tex_crealp)
#define _oss_tex_eobjid (*_g->ft.xerTbl->_oss_tex_eobjidp)
#define _oss_tex_aobjids (*_g->ft.xerTbl->_oss_tex_aobjidsp)
#define _oss_tex_aobjidi (*_g->ft.xerTbl->_oss_tex_aobjidip)
#define _oss_tex_aobjidl (*_g->ft.xerTbl->_oss_tex_aobjidlp)
#define _oss_tex_lobjids (*_g->ft.xerTbl->_oss_tex_lobjidsp)
#define _oss_tex_lobjidi (*_g->ft.xerTbl->_oss_tex_lobjidip)
#define _oss_tex_lobjidl (*_g->ft.xerTbl->_oss_tex_lobjidlp)
#define _oss_tex_format_char (*_g->ft.xerTbl->_oss_tex_format_charp)
#define _oss_tex_format_char_t (*_g->ft.xerTbl->_oss_tex_format_char_tp)
#define _oss_tex_format_bit (*_g->ft.xerTbl->_oss_tex_format_bitp)
#define _oss_tex_format_octet (*_g->ft.xerTbl->_oss_tex_format_octetp)
#define _oss_tex_prolog (*_g->ft.xerTbl->_oss_tex_prologp)
#define _oss_tex_opentype (*_g->ft.xerTbl->_oss_tex_opentypep)
#define _oss_tex_anyopentype (*_g->ft.xerTbl->_oss_tex_anyopentypep)
#define _oss_tex_2byte_char (*_g->ft.xerTbl->_oss_tex_2byte_charp)
#define _oss_tex_4byte_char (*_g->ft.xerTbl->_oss_tex_4byte_charp)

#define _oss_tdx_open_tag (*_g->ft.xerTbl->_oss_tdx_open_tagp)
#define _oss_tdx_empty_tag (*_g->ft.xerTbl->_oss_tdx_empty_tagp)
#define _oss_tdx_close_tag (*_g->ft.xerTbl->_oss_tdx_close_tagp)
#define _oss_tdx_open_or_empty_tag (*_g->ft.xerTbl->_oss_tdx_open_or_empty_tagp)
#define _oss_tdx_open_field (*_g->ft.xerTbl->_oss_tdx_open_fieldp)
#define _oss_tdx_skip_block (*_g->ft.xerTbl->_oss_tdx_skip_blockp)
#define _oss_tdx_rsf_alt    (*_g->ft.xerTbl->_oss_tdx_rsf_altp)
#define _oss_tdx_rsf_ext    (*_g->ft.xerTbl->_oss_tdx_rsf_extp)
#define _oss_tdx_prolog (*_g->ft.xerTbl->_oss_tdx_prologp)
#define _oss_tdx_lexem_lookahead (*_g->ft.xerTbl->_oss_tdx_lexem_lookaheadp)
#define _oss_tdx_allocate_array_sof (*_g->ft.xerTbl->_oss_tdx_allocate_array_sofp)
#define _oss_tdx_allocate_unbounded_sof (*_g->ft.xerTbl->_oss_tdx_allocate_unbounded_sofp)
#define _oss_tdx_opentype (*_g->ft.xerTbl->_oss_tdx_opentypep)
#define _oss_tdx_bool (*_g->ft.xerTbl->_oss_tdx_boolp)
#define _oss_tdx_enum (*_g->ft.xerTbl->_oss_tdx_enump)
#define _oss_tdx_llint (*_g->ft.xerTbl->_oss_tdx_llintp)
#define _oss_tdx_ullint (*_g->ft.xerTbl->_oss_tdx_ullintp)
#define _oss_tdx_lint (*_g->ft.xerTbl->_oss_tdx_lintp)
#define _oss_tdx_ulint (*_g->ft.xerTbl->_oss_tdx_ulintp)
#define _oss_tdx_hint (*_g->ft.xerTbl->_oss_tdx_hintp)
#define _oss_tdx_dreal (*_g->ft.xerTbl->_oss_tdx_drealp)
#define _oss_tdx_freal (*_g->ft.xerTbl->_oss_tdx_frealp)
#define _oss_tdx_creal (*_g->ft.xerTbl->_oss_tdx_crealp)
#define _oss_tdx_mreal (*_g->ft.xerTbl->_oss_tdx_mrealp)
#define _oss_tdx_mreal_ia (*_g->ft.xerTbl->_oss_tdx_mreal_iap)
#define _oss_tdx_char1_nullterm (*_g->ft.xerTbl->_oss_tdx_char1_nulltermp)
#define _oss_tdx_char1_unbounded (*_g->ft.xerTbl->_oss_tdx_char1_unboundedp)
#define _oss_tdx_char1_pvarying (*_g->ft.xerTbl->_oss_tdx_char1_pvaryingp)
#define _oss_tdx_char1_varying (*_g->ft.xerTbl->_oss_tdx_char1_varyingp)
#define _oss_tdx_char2 (*_g->ft.xerTbl->_oss_tdx_char2p)
#define _oss_tdx_char4 (*_g->ft.xerTbl->_oss_tdx_char4p)
#define _oss_tdx_octets_unbounded (*_g->ft.xerTbl->_oss_tdx_octets_unboundedp)
#define _oss_tdx_octets_pvarying (*_g->ft.xerTbl->_oss_tdx_octets_pvaryingp)
#define _oss_tdx_octets_varying (*_g->ft.xerTbl->_oss_tdx_octets_varyingp)
#define _oss_tdx_bits_unbounded (*_g->ft.xerTbl->_oss_tdx_bits_unboundedp)
#define _oss_tdx_bits_pvarying (*_g->ft.xerTbl->_oss_tdx_bits_pvaryingp)
#define _oss_tdx_bits_varying (*_g->ft.xerTbl->_oss_tdx_bits_varyingp)
#define _oss_tdx_bits_padded (*_g->ft.xerTbl->_oss_tdx_bits_paddedp)
#define _oss_tdx_objid_encoded (*_g->ft.xerTbl->_oss_tdx_objid_encodedp)
#define _oss_tdx_objid_linked (*_g->ft.xerTbl->_oss_tdx_objid_linkedp)
#define _oss_tdx_objid_unbounded (*_g->ft.xerTbl->_oss_tdx_objid_unboundedp)
#define _oss_tdx_objid_array (*_g->ft.xerTbl->_oss_tdx_objid_arrayp)
#define _oss_tdx_objid_parray (*_g->ft.xerTbl->_oss_tdx_objid_parrayp)
#define _oss_tdx_gtime (*_g->ft.xerTbl->_oss_tdx_gtimep)
#define _oss_tdx_utime (*_g->ft.xerTbl->_oss_tdx_utimep)
#define _oss_tex_format_pbit (*_g->ft.xerTbl->_oss_tex_format_pbitp)

#define _oss_allocate_memblock    (*_g->ft.xerTbl->_oss_allocate_memblockp)
#define _oss_consolidate_memblock (*_g->ft.xerTbl->_oss_consolidate_memblockp)
#define _oss_consolidate_memblock_fast (*_g->ft.xerTbl->_oss_consolidate_memblock_fastp)
#endif /* DLL_LINKAGE && !BUILD */

#endif /* OSS_TOED_XER */

#ifdef OSS_TOED_EXER
/*****************************************************************************/
/*  TOED EXER section                                                        */
/*****************************************************************************/

#define OSS_SOF_LENGTH 64

#define OSS_TSTK_NEW_FRAGMENT(g, c, size) \
  { if ((c)->tmp_mem.stack.bounds.left + (size) > (c)->tmp_mem.stack.bounds.right) \
	_oss_tstk_add_block(g, size); \
    _buf = (unsigned char *)(c)->tmp_mem.stack.bounds.left; \
    (c)->tmp_mem.stack.bounds.left += (size); }

#define OSS_TSTK_CONSOLIDATE_ARRAY(g, c, out, size) \
  { if ((unsigned char *)(c)->tmp_mem.stack.last_block < \
	    _first_buf && _first_buf <= (c)->tmp_mem.stack.bounds.left) \
	memcpy( out, _first_buf, size ); \
    else _oss_tstk_consolidate_outmem(g, out, _first_buf, size); \
    (c)->tmp_mem.stack.bounds.left = _first_buf; }

#define _oss_tstk_tell(g) (((_EncDecGlobals *)(g)->encDecVar)->tmp_mem.stack.bounds.left)

void DLL_ENTRY _oss_tstk_consolidate_outmem( OssGlobal *g, void *out, void *ptr, size_t size );
unsigned char *DLL_ENTRY _oss_tstk_get_altdecmem( OssGlobal *g, size_t size );
void DLL_ENTRY _oss_tstk_consolidate_altdecmem( OssGlobal *g,
    void *out, size_t size, size_t block_size );
void DLL_ENTRY _oss_tstk_free_altdecmem( OssGlobal *g, void *ptr );
unsigned char *DLL_ENTRY _oss_tstk_new_fragment_tag( exer_denv *env,
    size_t size );
void DLL_ENTRY _oss_tstk_consolidate_array_tag( exer_denv *env,
    unsigned char *first_buf, void *out, size_t size );

void DLL_ENTRY _oss_tell_dec_outmem( OssGlobal *g, OssToedDecmem *dm );
void DLL_ENTRY _oss_free_dec_outmem( OssGlobal *g, OssToedDecmem *dm );

void *DLL_ENTRY _oss_tstk_alloc( OssGlobal *g, size_t size );
void DLL_ENTRY _oss_tstk_free( OssGlobal *g, void *p );

    /*************************************************************************/
    /*      TOED EXER functions section                                      */
    /*************************************************************************/
int		DLL_ENTRY find_PDU_by_exer_tag_name(OssGlobal *world,
				exer_toed_table *etable,
        			unsigned char *uri, OSS_LENGTH ulen,
				unsigned char *name, OSS_LENGTH len);
void 		DLL_ENTRY _oss_tdex_init_env(exer_denv *env, 
				unsigned char *buf, unsigned long length);
void 		DLL_ENTRY _oss_tlex_get_lex(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_get_lex_collapse(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_get_tag(exer_denv *env);
void 		DLL_ENTRY _oss_tdex_error(exer_denv *env, 
				enum _err_index error_code, void *data, 
				OSS_UINT32 flags);
int 		DLL_ENTRY _oss_tlex_prolog_and_tag(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_check_close_tag(exer_denv *env);
unsigned long 	DLL_ENTRY _oss_tdex_char1_list(exer_denv *env, 
				void *value, exer_char1 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char2_list(exer_denv *env, 
				OSS_UINT16 **value, exer_char2 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char4_list(exer_denv *env, 
				OSS_INT32 **value, exer_char4 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char1_collapse(exer_denv *env, 
				void *value, exer_char1 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char2_collapse(exer_denv *env, 
				OSS_UINT16 **value, exer_char2 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char4_collapse(exer_denv *env, 
				OSS_INT32 **value, exer_char4 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char1_replace(exer_denv *env, 
				void *value, exer_char1 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char2_replace(exer_denv *env, 
				OSS_UINT16 **value, exer_char2 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char4_replace(exer_denv *env, 
				OSS_INT32 **value, exer_char4 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char1_preserve(exer_denv *env, 
				void *value, exer_char1 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char2_preserve(exer_denv *env, 
				OSS_UINT16 **value, exer_char2 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char4_preserve(exer_denv *env, 
				OSS_INT32 **value, exer_char4 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char1_64(exer_denv *env, 
				void *value, exer_char1 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char2_64(exer_denv *env, 
				OSS_UINT16 **value, exer_char2 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_char4_64(exer_denv *env, 
				OSS_INT32 **value, exer_char4 *ctx);
unsigned int 	DLL_ENTRY _oss_tdex_int(exer_denv *env, exer_int *ctx);
ULONG_LONG 	DLL_ENTRY _oss_tdex_int_ll(exer_denv *env, exer_int_ll *ctx);
ossBoolean 	DLL_ENTRY _oss_tdex_bool(exer_denv *env, exer_bool *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_int_huge(exer_denv *env,
				unsigned char **value, exer_int_huge *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_oct(exer_denv *env, 
				void *value, exer_oct *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_oct_64(exer_denv *env, 
				void  *value, exer_oct *ctx);
unsigned long	DLL_ENTRY _oss_tdex_otyp_64(exer_denv *env, 
				unsigned char **data, exer_otyp *ctx);
unsigned long	DLL_ENTRY _oss_tdex_otyp(exer_denv *env, 
				unsigned char **data, exer_otyp *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_bit(exer_denv  *env, 
				void *value, exer_bit *ctx);
unsigned int 	DLL_ENTRY _oss_tdex_enum(exer_denv  *env, exer_enum *ctx);
int 		DLL_ENTRY _oss_tdex_nil(exer_denv *env);
int 		DLL_ENTRY _oss_tdex_find_type(exer_denv *env, exer_name *qname);
void		DLL_ENTRY _oss_tdex_mark_type(exer_denv *env);
void		DLL_ENTRY _oss_tdex_error_type(exer_denv *env, exer_name *qname);
int 		DLL_ENTRY _oss_tlex_match_tag(exer_denv *env, 
				unsigned char *nm, unsigned long nm_len);
int 		DLL_ENTRY _oss_tlex_match_qtag(exer_denv *env, 
				unsigned char *uri, 
				unsigned char *nm, unsigned long nm_len);
int 		DLL_ENTRY _oss_tlex_match_wtag(exer_denv *env, 
				exer_wild_data *ctx);
void 		DLL_ENTRY _oss_tdex_match_tag_error(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_read_word(exer_denv *env, exer_name *name);
void 		DLL_ENTRY _oss_tlex_check_end(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_check_end_union(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_skip_ws(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_check_ws(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_free_attrs(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_store_input(exer_denv *env, 
				exer_save_lexeme *lex);
void 		DLL_ENTRY _oss_tlex_set_attr_input(exer_denv *env, 
				exer_attribute *attr);
void 		DLL_ENTRY _oss_tlex_restore_input(exer_denv *env, 
				exer_save_lexeme *lex);
void 		DLL_ENTRY _oss_tlex_set_embed(exer_denv *env,
				exer_embed *embed);
void 		DLL_ENTRY _oss_tlex_read_text(exer_denv *env,
				exer_name *name, OSS_UINT32 flags);
unsigned long 	DLL_ENTRY _oss_tlex_resolve_prefix(exer_denv *env, 
				exer_name *name);
unsigned long 	DLL_ENTRY _oss_tlex_copy_char1(exer_denv *env, 
				unsigned char **value,
				unsigned char *src, unsigned long src_length, 
				exer_char1 *ctx);
void 		DLL_ENTRY _oss_tlex_free_name(exer_denv *env);
OSS_UINT16 	DLL_ENTRY _oss_tlex_find_idx(exer_denv *env, exer_name *ename, 
				exer_tag_decoder *td);
OSS_UINT16 	DLL_ENTRY _oss_tlex_find_tag_idx(exer_denv *env, 
				exer_tag_decoder *td);
void 		DLL_ENTRY _oss_tlex_start_union(exer_denv *env, 
				exer_rescan *lex, unsigned long num);
void 		DLL_ENTRY _oss_tlex_retry_union(exer_denv *env, 
				exer_rescan *lex);
void 		DLL_ENTRY _oss_tlex_finish_union(exer_denv *env, 
				exer_rescan *lex);
int 		DLL_ENTRY _oss_tlex_wildcard_match(exer_denv *env, 
				unsigned char *uri, exer_wild_data *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_anyel(exer_denv *env, void *data, exer_char1 *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_anyattr(exer_denv *env, void *data,
				exer_attribute *attr, exer_char1 *ctx);
void		DLL_ENTRY _oss_tdex_time(exer_denv  *env, void *storage,
				exer_time *ctx);
unsigned long 	DLL_ENTRY _oss_tdex_oid_enc(exer_denv  *env, 
				void *value, exer_oid *ctx);
void		DLL_ENTRY _oss_tdex_oid_lnk(exer_denv  *env, 
				void **value, exer_oid *ctx);
unsigned short 	DLL_ENTRY _oss_tdex_oid(exer_denv  *env, 
				void *value, exer_oid *ctx);
float  		DLL_ENTRY _oss_tdex_real_float(exer_denv *env, exer_real *ctx);
double  	DLL_ENTRY _oss_tdex_real_double(exer_denv *env, exer_real *ctx);
char 	       *DLL_ENTRY _oss_tdex_real_char(exer_denv *env, exer_real *ctx);
enum MixedReal_kind DLL_ENTRY _oss_tdex_real_mixed(exer_denv *env, 
				_mixed_real_internal *data, exer_real *ctx);
void 		DLL_ENTRY _oss_tlex_skip_type(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_skip_content(exer_denv *env);
void 		DLL_ENTRY _oss_tlex_push_input(exer_denv *env, 
				exer_save_cc_lex *lex,
				unsigned char *enc, unsigned long len);
void 		DLL_ENTRY _oss_tlex_pop_input(exer_denv *env, 
				exer_save_cc_lex *lex);
void 		DLL_ENTRY _oss_tlex_mark_ext_attr(exer_denv *env);
void		DLL_ENTRY _oss_tdex_iso_time(exer_denv *env, 
				char **value, exer_iso_time *ctx);

void    	DLL_ENTRY _oss_teex_prolog(exer_eenv *env);
void		DLL_ENTRY _oss_teex_init_env(OssGlobal *world, exer_eenv *env);
void		DLL_ENTRY _oss_teex_append(exer_eenv *env, unsigned char *data,
				long num);
void		DLL_ENTRY _oss_teex_append_byte(exer_eenv *env,
				unsigned int byte);
char *		DLL_ENTRY _oss_teex_seal_tag(exer_eenv *env);
void		DLL_ENTRY _oss_teex_open_qtag(exer_eenv *env, exer_qname *qn,
				int right_bracket);
void		DLL_ENTRY _oss_teex_open_tag(exer_eenv *env,
				unsigned char *name, long name_len,
				int right_bracket);
void		DLL_ENTRY _oss_teex_close_qtag(exer_eenv *env, exer_qname *qn);
void		DLL_ENTRY _oss_teex_close_tag(exer_eenv *env,
				unsigned char *name, long name_len);
void		DLL_ENTRY _oss_teex_empty_qtag(exer_eenv *env, exer_qname *qn);
void		DLL_ENTRY _oss_teex_id_or_empty_tag(exer_eenv *env,
				unsigned char *name, unsigned int name_len);
void		DLL_ENTRY _oss_teex_end_tag(exer_eenv *env);
void		DLL_ENTRY _oss_teex_qattr_begin(exer_eenv *env, exer_qname *qn);
void		DLL_ENTRY _oss_teex_attr_begin(exer_eenv *env,
				unsigned char *name, long name_len);
void		DLL_ENTRY _oss_teex_indent(exer_eenv *env);
void		DLL_ENTRY _oss_teex_type_attr(exer_eenv *env,
			unsigned int control_idx,
			unsigned char *type_name,
				exer_qname *qn);
void		DLL_ENTRY _oss_teex_set_dns(exer_eenv *env,
				unsigned int ns_idx);
void		DLL_ENTRY _oss_teex_xmlns_attr(exer_eenv *env,
				unsigned int ns_idx);
void		DLL_ENTRY _oss_teex_nil_attr(exer_eenv *env,
				unsigned int control_idx);
void		DLL_ENTRY _oss_teex_set_embed(exer_eenv *env,
				teex_embed *embed, exer_embed_encoder enc);
void		DLL_ENTRY _oss_teex_chklst_char1(exer_eenv *env, 
				unsigned char *value, unsigned long length);
void 		DLL_ENTRY _oss_teex_char1_collapse(exer_eenv *env, 
				unsigned char *value, unsigned long length,
				exer_char1 *sdata);
void 		DLL_ENTRY _oss_teex_char1_replace(exer_eenv *env, 
				unsigned char *value, unsigned long length,
				exer_char1 *sdata);
void 		DLL_ENTRY _oss_teex_char1(exer_eenv *env, 
				unsigned char *value, unsigned long length,
				exer_char1 *sdata);
void		DLL_ENTRY _oss_teex_char2(exer_eenv *env, OSS_UINT16 *value,
				unsigned long length, exer_char2 *sdata);
void		DLL_ENTRY _oss_teex_char4(exer_eenv *env, OSS_INT32 *value,
				unsigned long length, exer_char4 *sdata);
void		DLL_ENTRY _oss_teex_char1_64(exer_eenv *env,
				unsigned char *value, unsigned long length,
				exer_char1 *sdata);
void		DLL_ENTRY _oss_teex_char2_64(exer_eenv *env,
				OSS_UINT16 *value, unsigned long length,
				exer_char2 *sdata);
void		DLL_ENTRY _oss_teex_char4_64(exer_eenv *env,
				OSS_INT32 *value, unsigned long length,
				exer_char4 *sdata);
void	DLL_ENTRY _oss_teex_char2_collapse(exer_eenv *env,
		OSS_UINT16 *value, unsigned long length, exer_char2 *sdata);
void	DLL_ENTRY _oss_teex_char2_replace(exer_eenv *env,
		OSS_UINT16 *value, unsigned long length, exer_char2 *sdata);
void	DLL_ENTRY _oss_teex_chklst_char2(exer_eenv *env,
		OSS_UINT16 *value, unsigned long length);
void	DLL_ENTRY _oss_teex_char4_collapse(exer_eenv *env,
		OSS_INT32 *value, unsigned long length, exer_char4 *sdata);
void	DLL_ENTRY _oss_teex_char4_replace(exer_eenv *env,
		OSS_INT32 *value, unsigned long length, exer_char4 *sdata);
void	DLL_ENTRY _oss_teex_chklst_char4(exer_eenv *env,
		OSS_INT32 *value, unsigned long length);
void		DLL_ENTRY _oss_teex_bool(exer_eenv *env, int value,
				exer_bool *sdata);
void		DLL_ENTRY _oss_teex_int(exer_eenv *env, unsigned int value,
				exer_int *sdata);
void		DLL_ENTRY _oss_teex_int_ll(exer_eenv *env, ULONG_LONG value,
				exer_int_ll *sdata);
void 		DLL_ENTRY _oss_teex_int_huge(exer_eenv *env,
				unsigned char *value, unsigned long length,
				exer_int_huge *sdata);
void		DLL_ENTRY _oss_teex_enum(exer_eenv *env, unsigned int value,
				exer_enum *sdata);
void		DLL_ENTRY _oss_teex_oct(exer_eenv *env, unsigned char *value,
				unsigned long length, exer_oct *sdata);
void		DLL_ENTRY _oss_teex_oct_64(exer_eenv *env,
				unsigned char *value, unsigned long length,
				exer_oct *sdata);
void		DLL_ENTRY _oss_teex_enc_base64_string(exer_eenv *env,
				unsigned char *value, unsigned long length);
void		DLL_ENTRY _oss_teex_bit(exer_eenv *env,  unsigned char *value,
				unsigned long bit_len, exer_bit *sdata);
void		DLL_ENTRY _oss_teex_bit_pad(exer_eenv *env,  OSS_UINT32 value,
					unsigned int bit_len, exer_bit *sdata);
unsigned int	DLL_ENTRY _oss_teex_check_uri(exer_eenv *env,
				unsigned char *value, unsigned int len);
void		DLL_ENTRY _oss_teex_check_attr(exer_eenv *env,
				unsigned char *uri, unsigned int uri_len,
				unsigned char *name, unsigned int name_len,
				int check);
void		DLL_ENTRY _oss_teex_error(exer_eenv *env,
				enum _err_index error_code,
				long num, char *data);
unsigned char *	DLL_ENTRY _oss_teex_qname_prefix(exer_eenv *env,
				unsigned char *value, size_t length,
				int *num);
void		DLL_ENTRY _oss_teex_qname_name(exer_eenv *env,
				unsigned char *value, size_t length,
				unsigned char *prefix, int num);
void		DLL_ENTRY _oss_teex_anyel(exer_eenv *env,
				unsigned char * value, unsigned long length,
				exer_wild_data *w_data);
void		DLL_ENTRY _oss_teex_anyattr(exer_eenv *env,
				unsigned char *value, unsigned long length,
				exer_wild_data *w_data);
void 		DLL_ENTRY _oss_teex_time(exer_eenv *env, void *data,
				exer_time *sdata);
void		DLL_ENTRY _oss_teex_time_nullt(exer_eenv *env, char *data,
				size_t inlen, exer_time *sdata);
void		DLL_ENTRY _oss_teex_iso_time(exer_eenv *env, char *data,
				exer_iso_time *sdata);
void		DLL_ENTRY _oss_teex_anyopentype(exer_eenv *env,
				unsigned char *data, long length);
void		DLL_ENTRY _oss_teex_otyp(exer_eenv *env, void *data,
				exer_otyp *sdata);
void		DLL_ENTRY _oss_teex_otyp_64(exer_eenv *env, void *data,
				exer_otyp *sdata);
void		DLL_ENTRY _oss_teex_oid_enc(exer_eenv *env, unsigned char *value,
				unsigned long length, exer_oid *sdata);
void		DLL_ENTRY _oss_teex_oid_unb_s(exer_eenv *env,
				unsigned short *value, unsigned long count,
				exer_oid *sdata);
void		DLL_ENTRY _oss_teex_oid_unb_i(exer_eenv *env,
				unsigned int *value, unsigned long count,
				exer_oid *sdata);
void		DLL_ENTRY _oss_teex_oid_unb_l(exer_eenv *env,
				unsigned long *value, unsigned long count,
				exer_oid *sdata);
void		DLL_ENTRY _oss_teex_oid_lnk_s(exer_eenv *env, void *value,
				exer_oid *sdata);
void		DLL_ENTRY _oss_teex_oid_lnk_i(exer_eenv *env, void *value,
				exer_oid *sdata);
void		DLL_ENTRY _oss_teex_oid_lnk_l(exer_eenv *env, void *value,
				exer_oid *sdata);
void		DLL_ENTRY _oss_teex_real_char(exer_eenv *env, char *value,
				exer_real *sdata);
void		DLL_ENTRY _oss_teex_real_decimal(exer_eenv *env,
				char *value, exer_real *sdata);
void		DLL_ENTRY _oss_teex_real_char_mod(exer_eenv *env,
				char *value, exer_real *sdata);
void		DLL_ENTRY _oss_teex_real_double(exer_eenv *env, double value,
				exer_real *sdata);
void		DLL_ENTRY _oss_teex_real_float(exer_eenv *env, float value,
				exer_real *sdata);
void		DLL_ENTRY _oss_teex_real_double_mod(exer_eenv *env, double value,
				exer_real *sdata);
void		DLL_ENTRY _oss_teex_real_float_mod(exer_eenv *env, float value,
				exer_real *sdata);
unsigned long	DLL_ENTRY _oss_teex_curr_enclen(exer_eenv *env);
void		DLL_ENTRY _oss_teex_db_e_pre(exer_eenv *env,int pdunum, void *input,
				char *bufpos, long buflen );

    /*************************************************************************/
    /*      TOED EXER DLL table defines section                               */
    /*************************************************************************/

        /*
         * The following macros must be #defined if you compile the ASN.1
         * compiler generated files (.c files) and link-edit with the import
         * library ossapit.lib or ossapi.lib, i.e the OSS DLLs ossapit.dll,
         * toedapi.dll and toedexer.dll are used with your TOED application,
         * or ossapi.dll, soedapi.dll soedxer.dll or soedxers.dll are used.
         * The following must not be #defined if you link-edit with
         * the static library toedcode.lib, toedcomd.lib, soeddefa.lib,
         * soeddemd.lib, soeddsmd.lib, soeddspa.lib. If you use /MD and
         * link with toedcomd.lib, soeddemd.lib or soeddsmd.lib
         * #define ONE_DLL on the C-compiler command line to disable the
         * below macros.
         */
#if defined(DLL_LINKAGE)

#define _oss_tdex_init_env (*_g->ft.exerTbl->_oss_tdex_init_envp)
#define _oss_tlex_get_lex (*_g->ft.exerTbl->_oss_tlex_get_lexp)
#define _oss_tlex_get_lex_collapse (*_g->ft.exerTbl->_oss_tlex_get_lex_collapsep)
#define _oss_tlex_get_tag (*_g->ft.exerTbl->_oss_tlex_get_tagp)
#define _oss_tdex_error (*_g->ft.exerTbl->_oss_tdex_errorp)
#define _oss_tlex_prolog_and_tag (*_g->ft.exerTbl->_oss_tlex_prolog_and_tagp)
#define _oss_tlex_check_close_tag (*_g->ft.exerTbl->_oss_tlex_check_close_tagp)
#define _oss_tdex_char1_list (*_g->ft.exerTbl->_oss_tdex_char1_listp)
#define _oss_tdex_char2_list (*_g->ft.exerTbl->_oss_tdex_char2_listp)
#define _oss_tdex_char4_list (*_g->ft.exerTbl->_oss_tdex_char4_listp)
#define _oss_tdex_char1_collapse (*_g->ft.exerTbl->_oss_tdex_char1_collapsep)
#define _oss_tdex_char2_collapse (*_g->ft.exerTbl->_oss_tdex_char2_collapsep)
#define _oss_tdex_char4_collapse (*_g->ft.exerTbl->_oss_tdex_char4_collapsep)
#define _oss_tdex_char1_replace (*_g->ft.exerTbl->_oss_tdex_char1_replacep)
#define _oss_tdex_char2_replace (*_g->ft.exerTbl->_oss_tdex_char2_replacep)
#define _oss_tdex_char4_replace (*_g->ft.exerTbl->_oss_tdex_char4_replacep)
#define _oss_tdex_char1_preserve (*_g->ft.exerTbl->_oss_tdex_char1_preservep)
#define _oss_tdex_char2_preserve (*_g->ft.exerTbl->_oss_tdex_char2_preservep)
#define _oss_tdex_char4_preserve (*_g->ft.exerTbl->_oss_tdex_char4_preservep)
#define _oss_tdex_char1_64 (*_g->ft.exerTbl->_oss_tdex_char1_64p)
#define _oss_tdex_char2_64 (*_g->ft.exerTbl->_oss_tdex_char2_64p)
#define _oss_tdex_char4_64 (*_g->ft.exerTbl->_oss_tdex_char4_64p)
#define _oss_tdex_int (*_g->ft.exerTbl->_oss_tdex_intp)
#define _oss_tdex_int_ll (*_g->ft.exerTbl->_oss_tdex_int_llp)
#define _oss_tdex_bool (*_g->ft.exerTbl->_oss_tdex_boolp)
#define _oss_tdex_int_huge (*_g->ft.exerTbl->_oss_tdex_int_hugep)
#define _oss_tdex_oct (*_g->ft.exerTbl->_oss_tdex_octp)
#define _oss_tdex_oct_64 (*_g->ft.exerTbl->_oss_tdex_oct_64p)
#define _oss_tdex_otyp_64 (*_g->ft.exerTbl->_oss_tdex_otyp_64p)
#define _oss_tdex_otyp (*_g->ft.exerTbl->_oss_tdex_otypp)
#define _oss_tdex_bit (*_g->ft.exerTbl->_oss_tdex_bitp)
#define _oss_tdex_enum (*_g->ft.exerTbl->_oss_tdex_enump)
#define _oss_tdex_nil (*_g->ft.exerTbl->_oss_tdex_nilp)
#define _oss_tdex_find_type (*_g->ft.exerTbl->_oss_tdex_find_typep)
#define _oss_tdex_mark_type (*_g->ft.exerTbl->_oss_tdex_mark_typep)
#define _oss_tdex_error_type (*_g->ft.exerTbl->_oss_tdex_error_typep)
#define _oss_tlex_match_tag (*_g->ft.exerTbl->_oss_tlex_match_tagp)
#define _oss_tlex_match_qtag (*_g->ft.exerTbl->_oss_tlex_match_qtagp)
#define _oss_tlex_match_wtag (*_g->ft.exerTbl->_oss_tlex_match_wtagp)
#define _oss_tdex_match_tag_error (*_g->ft.exerTbl->_oss_tdex_match_tag_errorp)
#define _oss_tlex_read_word (*_g->ft.exerTbl->_oss_tlex_read_wordp)
#define _oss_tlex_check_end (*_g->ft.exerTbl->_oss_tlex_check_endp)
#define _oss_tlex_check_end_union (*_g->ft.exerTbl->_oss_tlex_check_end_unionp)
#define _oss_tlex_skip_ws (*_g->ft.exerTbl->_oss_tlex_skip_wsp)
#define _oss_tlex_check_ws (*_g->ft.exerTbl->_oss_tlex_check_wsp)
#define _oss_tlex_free_attrs (*_g->ft.exerTbl->_oss_tlex_free_attrsp)
#define _oss_tlex_store_input (*_g->ft.exerTbl->_oss_tlex_store_inputp)
#define _oss_tlex_set_attr_input (*_g->ft.exerTbl->_oss_tlex_set_attr_inputp)
#define _oss_tlex_restore_input (*_g->ft.exerTbl->_oss_tlex_restore_inputp)
#define _oss_tlex_set_embed (*_g->ft.exerTbl->_oss_tlex_set_embedp)
#define _oss_tlex_read_text (*_g->ft.exerTbl->_oss_tlex_read_textp)
#define _oss_tlex_resolve_prefix (*_g->ft.exerTbl->_oss_tlex_resolve_prefixp)
#define _oss_tlex_copy_char1 (*_g->ft.exerTbl->_oss_tlex_copy_char1p)
#define _oss_tlex_free_name (*_g->ft.exerTbl->_oss_tlex_free_namep)
#define _oss_tlex_find_idx (*_g->ft.exerTbl->_oss_tlex_find_idxp)
#define _oss_tlex_find_tag_idx (*_g->ft.exerTbl->_oss_tlex_find_tag_idxp)
#define _oss_tlex_start_union (*_g->ft.exerTbl->_oss_tlex_start_unionp)
#define _oss_tlex_retry_union (*_g->ft.exerTbl->_oss_tlex_retry_unionp)
#define _oss_tlex_finish_union (*_g->ft.exerTbl->_oss_tlex_finish_unionp)
#define _oss_tlex_wildcard_match (*_g->ft.exerTbl->_oss_tlex_wildcard_matchp)
#define _oss_tdex_anyel (*_g->ft.exerTbl->_oss_tdex_anyelp)
#define _oss_tdex_anyattr (*_g->ft.exerTbl->_oss_tdex_anyattrp)
#define _oss_tdex_time (*_g->ft.exerTbl->_oss_tdex_timep)
#define _oss_tdex_oid_enc (*_g->ft.exerTbl->_oss_tdex_oid_encp)
#define _oss_tdex_oid_lnk (*_g->ft.exerTbl->_oss_tdex_oid_lnkp)
#define _oss_tdex_oid (*_g->ft.exerTbl->_oss_tdex_oidp)
#define _oss_tdex_real_float (*_g->ft.exerTbl->_oss_tdex_real_floatp)
#define _oss_tdex_real_double (*_g->ft.exerTbl->_oss_tdex_real_doublep)
#define _oss_tdex_real_char (*_g->ft.exerTbl->_oss_tdex_real_charp)
#define _oss_tdex_real_mixed (*_g->ft.exerTbl->_oss_tdex_real_mixedp)
#define _oss_tlex_skip_type (*_g->ft.exerTbl->_oss_tlex_skip_typep)
#define _oss_tlex_skip_content (*_g->ft.exerTbl->_oss_tlex_skip_contentp)
#define _oss_tlex_push_input (*_g->ft.exerTbl->_oss_tlex_push_inputp)
#define _oss_tlex_pop_input (*_g->ft.exerTbl->_oss_tlex_pop_inputp)
#define _oss_tlex_mark_ext_attr (*_g->ft.exerTbl->_oss_tlex_mark_ext_attrp)
#define _oss_tdex_iso_time (*_g->ft.exerTbl->_oss_tdex_iso_timep)
#define _oss_teex_prolog (*_g->ft.exerTbl->_oss_teex_prologp)
#define _oss_teex_init_env (*_g->ft.exerTbl->_oss_teex_init_envp)
#define _oss_teex_append (*_g->ft.exerTbl->_oss_teex_appendp)
#define _oss_teex_append_byte (*_g->ft.exerTbl->_oss_teex_append_bytep)
#define _oss_teex_seal_tag (*_g->ft.exerTbl->_oss_teex_seal_tagp)
#define _oss_teex_open_qtag (*_g->ft.exerTbl->_oss_teex_open_qtagp)
#define _oss_teex_open_tag (*_g->ft.exerTbl->_oss_teex_open_tagp)
#define _oss_teex_close_qtag (*_g->ft.exerTbl->_oss_teex_close_qtagp)
#define _oss_teex_close_tag (*_g->ft.exerTbl->_oss_teex_close_tagp)
#define _oss_teex_empty_qtag (*_g->ft.exerTbl->_oss_teex_empty_qtagp)
#define _oss_teex_id_or_empty_tag (*_g->ft.exerTbl->_oss_teex_id_or_empty_tagp)
#define _oss_teex_end_tag (*_g->ft.exerTbl->_oss_teex_end_tagp)
#define _oss_teex_qattr_begin (*_g->ft.exerTbl->_oss_teex_qattr_beginp)
#define _oss_teex_attr_begin (*_g->ft.exerTbl->_oss_teex_attr_beginp)
#define _oss_teex_indent (*_g->ft.exerTbl->_oss_teex_indentp)
#define _oss_teex_type_attr (*_g->ft.exerTbl->_oss_teex_type_attrp)
#define _oss_teex_set_dns (*_g->ft.exerTbl->_oss_teex_set_dnsp)
#define _oss_teex_xmlns_attr (*_g->ft.exerTbl->_oss_teex_xmlns_attrp)
#define _oss_teex_nil_attr (*_g->ft.exerTbl->_oss_teex_nil_attrp)
#define _oss_teex_set_embed (*_g->ft.exerTbl->_oss_teex_set_embedp)
#define _oss_teex_chklst_char1 (*_g->ft.exerTbl->_oss_teex_chklst_char1p)
#define _oss_teex_char1_collapse (*_g->ft.exerTbl->_oss_teex_char1_collapsep)
#define _oss_teex_char1_replace (*_g->ft.exerTbl->_oss_teex_char1_replacep)
#define _oss_teex_char1 (*_g->ft.exerTbl->_oss_teex_char1p)
#define _oss_teex_char2 (*_g->ft.exerTbl->_oss_teex_char2p)
#define _oss_teex_char4 (*_g->ft.exerTbl->_oss_teex_char4p)
#define _oss_teex_char1_64 (*_g->ft.exerTbl->_oss_teex_char1_64p)
#define _oss_teex_char2_64 (*_g->ft.exerTbl->_oss_teex_char2_64p)
#define _oss_teex_char4_64 (*_g->ft.exerTbl->_oss_teex_char4_64p)
#define _oss_teex_char2_collapse (*_g->ft.exerTbl->_oss_teex_char2_collapsep)
#define _oss_teex_char2_replace (*_g->ft.exerTbl->_oss_teex_char2_replacep)
#define _oss_teex_chklst_char2 (*_g->ft.exerTbl->_oss_teex_chklst_char2p)
#define _oss_teex_char4_collapse (*_g->ft.exerTbl->_oss_teex_char4_collapsep)
#define _oss_teex_char4_replace (*_g->ft.exerTbl->_oss_teex_char4_replacep)
#define _oss_teex_chklst_char4 (*_g->ft.exerTbl->_oss_teex_chklst_char4p)
#define _oss_teex_bool (*_g->ft.exerTbl->_oss_teex_boolp)
#define _oss_teex_int (*_g->ft.exerTbl->_oss_teex_intp)
#define _oss_teex_int_ll (*_g->ft.exerTbl->_oss_teex_int_llp)
#define _oss_teex_int_huge (*_g->ft.exerTbl->_oss_teex_int_hugep)
#define _oss_teex_enum (*_g->ft.exerTbl->_oss_teex_enump)
#define _oss_teex_oct (*_g->ft.exerTbl->_oss_teex_octp)
#define _oss_teex_oct_64 (*_g->ft.exerTbl->_oss_teex_oct_64p)
#define _oss_teex_enc_base64_string (*_g->ft.exerTbl->_oss_teex_enc_base64_stringp)
#define _oss_teex_bit (*_g->ft.exerTbl->_oss_teex_bitp)
#define _oss_teex_bit_pad (*_g->ft.exerTbl->_oss_teex_bit_padp)
#define _oss_teex_check_uri (*_g->ft.exerTbl->_oss_teex_check_urip)
#define _oss_teex_check_attr (*_g->ft.exerTbl->_oss_teex_check_attrp)
#define _oss_teex_error (*_g->ft.exerTbl->_oss_teex_errorp)
#define _oss_teex_qname_prefix (*_g->ft.exerTbl->_oss_teex_qname_prefixp)
#define _oss_teex_qname_name (*_g->ft.exerTbl->_oss_teex_qname_namep)
#define _oss_teex_anyel (*_g->ft.exerTbl->_oss_teex_anyelp)
#define _oss_teex_anyattr (*_g->ft.exerTbl->_oss_teex_anyattrp)
#define _oss_teex_time (*_g->ft.exerTbl->_oss_teex_timep)
#define _oss_teex_time_nullt (*_g->ft.exerTbl->_oss_teex_time_nulltp)
#define _oss_teex_iso_time (*_g->ft.exerTbl->_oss_teex_iso_timep)
#define _oss_teex_anyopentype (*_g->ft.exerTbl->_oss_teex_anyopentypep)
#define _oss_teex_otyp (*_g->ft.exerTbl->_oss_teex_otypp)
#define _oss_teex_otyp_64 (*_g->ft.exerTbl->_oss_teex_otyp_64p)
#define _oss_teex_oid_enc (*_g->ft.exerTbl->_oss_teex_oid_encp)
#define _oss_teex_oid_unb_s (*_g->ft.exerTbl->_oss_teex_oid_unb_sp)
#define _oss_teex_oid_unb_i (*_g->ft.exerTbl->_oss_teex_oid_unb_ip)
#define _oss_teex_oid_unb_l (*_g->ft.exerTbl->_oss_teex_oid_unb_lp)
#define _oss_teex_oid_lnk_s (*_g->ft.exerTbl->_oss_teex_oid_lnk_sp)
#define _oss_teex_oid_lnk_i (*_g->ft.exerTbl->_oss_teex_oid_lnk_ip)
#define _oss_teex_oid_lnk_l (*_g->ft.exerTbl->_oss_teex_oid_lnk_lp)
#define _oss_teex_real_char (*_g->ft.exerTbl->_oss_teex_real_charp)
#define _oss_teex_real_decimal (*_g->ft.exerTbl->_oss_teex_real_decimalp)
#define _oss_teex_real_char_mod (*_g->ft.exerTbl->_oss_teex_real_char_modp)
#define _oss_teex_real_double (*_g->ft.exerTbl->_oss_teex_real_doublep)
#define _oss_teex_real_float (*_g->ft.exerTbl->_oss_teex_real_floatp)
#define _oss_teex_real_double_mod (*_g->ft.exerTbl->_oss_teex_real_double_modp)
#define _oss_teex_real_float_mod (*_g->ft.exerTbl->_oss_teex_real_float_modp)
#define _oss_teex_db_e_pre (*_g->ft.exerTbl->_oss_teex_db_e_prep)
#define _oss_teex_curr_enclen (*_g->ft.exerTbl->_oss_teex_curr_enclenp)
#define _oss_tstk_alloc (*_g->ft.exerTbl->_oss_tstk_allocp)
#define _oss_tstk_add_block (*_g->ft.exerTbl->_oss_tstk_add_blockp)
#define _oss_tstk_free_blocks  (*_g->ft.exerTbl->_oss_tstk_free_blocksp)
#define _oss_tstk_free (*_g->ft.exerTbl->_oss_tstk_freep)
#define _oss_tstk_new_fragment_tag (*_g->ft.exerTbl->_oss_tstk_new_fragment_tagp)
#define _oss_tstk_get_altdecmem (*_g->ft.exerTbl->_oss_tstk_get_altdecmemp)
#define _oss_tstk_consolidate_altdecmem (*_g->ft.exerTbl->_oss_tstk_consolidate_altdecmemp)
#define _oss_tstk_consolidate_outmem (*_g->ft.exerTbl->_oss_tstk_consolidate_outmemp)
#define _oss_tstk_consolidate_array_tag (*_g->ft.exerTbl->_oss_tstk_consolidate_array_tagp)
#define _oss_tstk_free_altdecmem (*_g->ft.exerTbl->_oss_tstk_free_altdecmemp)

#endif /* DLL_LINKAGE && !BUILD */

#endif /* OSS_TOED_EXER */

#else /* _OSS_CODE_FILE */

/*****************************************************************************/
/*  SOED general section                                                     */
/*****************************************************************************/


#ifndef OSS_SPARTAN_AWARE
#define OSS_SPARTAN_AWARE 18
#endif
typedef unsigned short Etag;
typedef struct efield *_oss_QQQJ;
typedef struct etype *_oss_qHq;
typedef struct eheader *Eheader;
struct etype {
    long            max_length;
    size_t          _oss_QJq;
    size_t          _oss_JHQJ;
    char           *_oss_WWj;
    size_t          _oss_WJq;
    size_t          _oss_wJJJ;
    unsigned short int _oss_qwJJ;
    unsigned short int _oss_WjQJ;
    unsigned short int _oss_QqQJ;
    unsigned short int _oss_HJJJ;
    int                _oss_Hwj;	    
    unsigned short int _oss_wwq;
};
struct efield {
    size_t          _oss_jqq;	
    unsigned short int _oss_Qwj;	
    short int       _oss_jwQJ;	
    unsigned short int _oss_QJQJ;	
    unsigned char   _oss_jHQJ;	
};
struct ConstraintEntry {
    char            _oss_wHJJ;
    char            _oss_WHJJ;
    void           *_oss_HJq;
};
struct InnerSubtypeEntry {
    char		_oss_WQQJ;
    unsigned char	_oss_jJj;
    unsigned short	_oss_wQq;	     
    unsigned short	_oss_HJq;  
};
struct eValRef {
    char           *_oss_WWj;
    const void     *_oss_qWq;
    unsigned short  _oss_Qwj;	
};
typedef struct OSetTableEntry {
    void		*_oss_qWq;
    unsigned short	_oss_Qwj;
    void		*_oss_qJJJ;
    unsigned short	_oss_wJQJ;
    char		*_oss_WQJJ;
    int			_oss_HHJJ;
    int			_oss_WJJJ;
} OSetTableEntry;
typedef struct WideAlphabet {
    OSS_UINT32  _oss_jqq; 
    struct {
	short _oss_QHQJ;   
	char  *_oss_JQq;   
    } _oss_wQQJ;
    void *        _oss_qHJJ; 
} WideAlphabet;
typedef struct eda_table {
    unsigned int _oss_JWq;
    unsigned char *_oss_jJj;
} eda_table;
typedef struct midRelation {
    unsigned short _oss_HqQJ;
    unsigned short _oss_QwJJ;
    unsigned short _oss_JqJJ;
    unsigned short _oss_jQJJ;
} MidRelation;
struct eheader {
#if defined(__WATCOMC__) && defined(__DOS__)
    void (*_oss_JHq)(struct ossGlobal *); 
#else
    void (DLL_ENTRY_FPTR *_System _oss_JHq)(struct ossGlobal *); 
#endif /* __WATCOMC__ && __DOS__ */
    long            pdudecoder;	
    unsigned short int _oss_JWq;	
    unsigned short int _oss_jJj;	
    unsigned short int _oss_QWQJ,
                    _oss_JJJJ;	
    unsigned short *pduarray;	
    _oss_qHq           etypes;	
    _oss_QQQJ          _oss_wjQJ;	
    void          **_oss_QjJJ;	
    unsigned short *_oss_QJq;	
    struct ConstraintEntry *_oss_wwq;
    struct InnerSubtypeEntry *_oss_wHQJ;	
    void           *_oss_WqQJ;	
    unsigned short int _oss_qHQJ; 
    void           *_oss_QqJJ;
    unsigned short  _oss_qwQJ;
};


    /*************************************************************************/
    /*      SOED general functions section                                   */
    /*************************************************************************/

PUBLIC void DLL_ENTRY ossLinkConstraint(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossEDLinkPer(struct ossGlobal *world, void *eda_table);
PUBLIC void DLL_ENTRY ossLinkSAX(struct ossGlobal *world, void *exer_table, 
	void *sax_data);
PUBLIC void DLL_ENTRY ossLinkSAXParser(struct ossGlobal *world);
PUBLIC void DLL_ENTRY ossLinkUserConstraint(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkPrintPer(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkPerPDV(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossInitObjectSetInfo(struct ossGlobal *,
					    unsigned short *);
PUBLIC void DLL_ENTRY ossLinkUserConstraintSpartanAware8(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossSetExtParms(struct ossGlobal *world, void  *arg);
PUBLIC void DLL_ENTRY ossLinkConstraintSpartanAware8(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkCmpValue(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkCpyValue(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkZlib(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkPropertySettingsConstraint(struct ossGlobal *,
					    unsigned int *);

PUBLIC void DLL_ENTRY ossInitPrintFunctions(struct ossGlobal *, void *);
PUBLIC void DLL_ENTRY ossInitUserFunctions(struct ossGlobal *, void *);

PUBLIC void DLL_ENTRY ossLinkBerReal(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkConstraintReal(struct ossGlobal *);
PUBLIC void DLL_ENTRY ossLinkPatternConstraint(struct ossGlobal *);

#ifdef OSS_SOED_BER
/*****************************************************************************/
/*  SOED BER section                                                         */
/*****************************************************************************/
    /*************************************************************************/
    /*      SOED BER functions section                                       */
    /*************************************************************************/

PUBLIC void DLL_ENTRY ossLinkCer(struct ossGlobal *);

#endif /* OSS_SOED_BER */

#ifdef OSS_SOED_PER
/*****************************************************************************/
/*  SOED PER section                                                         */
/*****************************************************************************/
    /*************************************************************************/
    /*      SOED PER functions section                                       */
    /*************************************************************************/

PUBLIC void DLL_ENTRY ossLinkPerReal(struct ossGlobal *);

	/********************************************************************/
	/*      SOED PER time functions section                             */
	/********************************************************************/

typedef void (*ossPERTimeEncodefp)(struct ossGlobal  *);
typedef void (*ossPERTimeDecodefp)(struct ossGlobal  *);

#if defined(DLL_LINKAGE)
extern ossPERTimeEncodefp *_oss_getPERTimeEncodefp(void);
extern ossPERTimeDecodefp *_oss_getPERTimeDecodefp(void);
#endif /* DLL_LINKAGE || PRINT_PER */
 
/* ossLink_[E|D]PERTime routines should never be used with DLLs */
PUBLIC void DLL_ENTRY ossLink_EPerTime (struct ossGlobal *,
 	unsigned short , int const * , ossPERTimeEncodefp *);
PUBLIC void DLL_ENTRY ossLink_DPerTime (struct ossGlobal *,
 	unsigned short , int const * , ossPERTimeDecodefp *);

#ifdef __vms
#define _oss_ept_rec_duration_end_date_time_interval _oss_ept_rec_duration_end_dt_time_interval
#define _oss_dpt_rec_duration_end_date_time_interval _oss_dpt_rec_duration_end_dt_time_interval
#endif

/* ************************************************************* *
 * PER TIME Encoding functions:				         *
 * ************************************************************* */
extern void _oss_ept_century(struct ossGlobal  *);
extern void _oss_ept_any_century(struct ossGlobal  *);
extern void _oss_ept_year(struct ossGlobal  *);
extern void _oss_ept_any_year(struct ossGlobal  *);
extern void _oss_ept_year_month(struct ossGlobal  *);
extern void _oss_ept_any_year_month(struct ossGlobal  *);
extern void _oss_ept_date(struct ossGlobal  *);
extern void _oss_ept_any_date(struct ossGlobal  *);
extern void _oss_ept_year_day(struct ossGlobal  *);
extern void _oss_ept_any_year_day(struct ossGlobal  *);
extern void _oss_ept_year_week(struct ossGlobal  *);
extern void _oss_ept_any_year_week(struct ossGlobal  *);
extern void _oss_ept_year_week_day(struct ossGlobal  *);
extern void _oss_ept_any_year_week_day(struct ossGlobal  *);
extern void _oss_ept_hours(struct ossGlobal  *);
extern void _oss_ept_hours_utc(struct ossGlobal  *);
extern void _oss_ept_hours_and_diff(struct ossGlobal  *);
extern void _oss_ept_minutes(struct ossGlobal  *);
extern void _oss_ept_minutes_utc(struct ossGlobal  *);
extern void _oss_ept_minutes_and_diff(struct ossGlobal  *);
extern void _oss_ept_minutes_and_diff_and_fraction(struct ossGlobal  *);
extern void _oss_ept_time_of_day(struct ossGlobal  *);
extern void _oss_ept_time_of_day_utc(struct ossGlobal  *);
extern void _oss_ept_time_of_day_and_diff(struct ossGlobal  *);
extern void _oss_ept_hours_and_fraction(struct ossGlobal  *);
extern void _oss_ept_hours_utc_and_fraction(OssGlobal *world);
extern void _oss_ept_hours_and_diff_and_fraction(struct ossGlobal  *);
extern void _oss_ept_minutes_and_fraction(struct ossGlobal  *);
extern void _oss_ept_minutes_utc_and_fraction(struct ossGlobal  *);
extern void _oss_ept_time_of_day_and_fraction(struct ossGlobal  *);
extern void _oss_ept_time_of_day_utc_and_fraction(struct ossGlobal  *);
extern void _oss_ept_time_of_day_and_diff_and_fraction(struct ossGlobal  *);
extern void _oss_ept_date_time(struct ossGlobal  *);
extern void _oss_ept_start_end_date_interval(struct ossGlobal  *);
extern void _oss_ept_start_end_time_interval(struct ossGlobal  *);
extern void _oss_ept_start_end_date_time_interval(struct ossGlobal  *);
extern void _oss_ept_duration_interval(struct ossGlobal  *);
extern void _oss_ept_start_date_duration_interval(struct ossGlobal  *);
extern void _oss_ept_start_time_duration_interval(struct ossGlobal  *);
extern void _oss_ept_start_date_time_duration_interval(struct ossGlobal  *);
extern void _oss_ept_duration_end_date_interval(struct ossGlobal  *);
extern void _oss_ept_duration_end_time_interval(struct ossGlobal  *);
extern void _oss_ept_duration_end_date_time_interval(struct ossGlobal  *);
extern void _oss_ept_rec_start_end_date_interval(struct ossGlobal  *);
extern void _oss_ept_rec_start_end_time_interval(struct ossGlobal  *);
extern void _oss_ept_rec_start_end_date_time_interval(struct ossGlobal *);
extern void _oss_ept_rec_duration_interval(struct ossGlobal  *);
extern void _oss_ept_rec_start_date_duration_interval(struct ossGlobal  *);
extern void _oss_ept_rec_start_time_duration_interval(struct ossGlobal  *);
extern void _oss_ept_rec_start_date_time_duration_interval(struct ossGlobal  *);
extern void _oss_ept_rec_duration_end_date_interval(struct ossGlobal  *);
extern void _oss_ept_rec_duration_end_time_interval(struct ossGlobal  *);
extern void _oss_ept_rec_duration_end_date_time_interval(struct ossGlobal  *);
extern void _oss_ept_mixed(struct ossGlobal  *);
extern void _oss_ept_not_supported (struct ossGlobal  *);

/* ************************************************************* *
 * PER TIME decoding functions:					 *
 * ************************************************************* */
extern void _oss_dpt_century(struct ossGlobal  *);
extern void _oss_dpt_any_century(struct ossGlobal  *);
extern void _oss_dpt_year(struct ossGlobal  *);
extern void _oss_dpt_any_year(struct ossGlobal  *);
extern void _oss_dpt_year_month(struct ossGlobal  *);
extern void _oss_dpt_any_year_month(struct ossGlobal  *);
extern void _oss_dpt_date(struct ossGlobal  *);
extern void _oss_dpt_any_date(struct ossGlobal  *);
extern void _oss_dpt_year_day(struct ossGlobal  *);
extern void _oss_dpt_any_year_day(struct ossGlobal  *);
extern void _oss_dpt_year_week(struct ossGlobal  *);
extern void _oss_dpt_any_year_week(struct ossGlobal  *);
extern void _oss_dpt_year_week_day(struct ossGlobal  *);
extern void _oss_dpt_any_year_week_day(struct ossGlobal  *);
extern void _oss_dpt_hours(struct ossGlobal  *);
extern void _oss_dpt_hours_utc(struct ossGlobal  *);
extern void _oss_dpt_hours_and_diff(struct ossGlobal  *);
extern void _oss_dpt_minutes(struct ossGlobal  *);
extern void _oss_dpt_minutes_utc(struct ossGlobal  *);
extern void _oss_dpt_minutes_and_diff(struct ossGlobal  *);
extern void _oss_dpt_minutes_and_diff_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_time_of_day(struct ossGlobal  *);
extern void _oss_dpt_time_of_day_utc(struct ossGlobal  *);
extern void _oss_dpt_time_of_day_and_diff(struct ossGlobal  *);
extern void _oss_dpt_hours_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_hours_utc_and_fraction(OssGlobal *world);
extern void _oss_dpt_hours_and_diff_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_minutes_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_minutes_utc_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_time_of_day_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_time_of_day_utc_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_time_of_day_and_diff_and_fraction(struct ossGlobal  *);
extern void _oss_dpt_date_time(struct ossGlobal  *);
extern void _oss_dpt_start_end_date_interval(struct ossGlobal  *);
extern void _oss_dpt_start_end_time_interval(struct ossGlobal  *);
extern void _oss_dpt_start_end_date_time_interval(struct ossGlobal  *);
extern void _oss_dpt_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_start_date_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_start_time_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_start_date_time_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_duration_end_date_interval(struct ossGlobal  *);
extern void _oss_dpt_duration_end_time_interval(struct ossGlobal  *);
extern void _oss_dpt_duration_end_date_time_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_start_end_date_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_start_end_time_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_start_end_date_time_interval(struct ossGlobal *);
extern void _oss_dpt_rec_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_start_date_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_start_time_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_start_date_time_duration_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_duration_end_date_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_duration_end_time_interval(struct ossGlobal  *);
extern void _oss_dpt_rec_duration_end_date_time_interval(struct ossGlobal  *);
extern void _oss_dpt_mixed(struct ossGlobal  *);
extern void _oss_dpt_not_supported (struct ossGlobal  *);

#endif /* OSS_SOED_PER */


#if defined(OSS_SOED_XER)
/*****************************************************************************/
/*  SOED XER section                                                         */
/*****************************************************************************/
#ifndef OSS_VOIDE
#define OSS_VOIDE void
#endif
#ifndef OSS_VOIDD
#define OSS_VOIDD void
#endif

    /*************************************************************************/
    /*      SOED XER functions section                                       */
    /*************************************************************************/

typedef void	(* ossXERencodefp)(void *);
typedef void	(* ossXERdecodefp)(void *);

/* DLL-related functions defined at xerfp.c; also used for ASN1Step */
#if defined(DLL_LINKAGE)
extern ossXERencodefp *_oss_getXERencodefp(void);
extern ossXERdecodefp *_oss_getXERdecodefp(void);
#endif /* DLL_LINKAGE */

/* ossLink[E|D]Xer routines should never be used with DLLs */
PUBLIC void DLL_ENTRY ossLinkEXer (OssGlobal *, unsigned short,
					int const *, ossXERencodefp *);
PUBLIC void DLL_ENTRY ossLinkDXer (OssGlobal *, unsigned short,
					int const *, ossXERdecodefp *);
PUBLIC void DLL_ENTRY ossLinkXerReal(OssGlobal *);
/* In contrast, ossLinkXer handles DLL SOED XER linkage */
PUBLIC void DLL_ENTRY ossLinkXer (OssGlobal *);
#endif /* (OSS_SOED_XER || !SHIPPING) && !OSS_XSD */

#if defined(OSS_SOED_XER)
/* ************************************************************* *
 * Encoding functions:						 *
 * ************************************************************* */
extern void			_oss_ex_int (OSS_VOIDE * ctx);
#define _oss_ex_uint		_oss_ex_int
extern void			_oss_ex_enum (OSS_VOIDE * ctx);
#define _oss_ex_uenum		_oss_ex_enum
extern void			_oss_ex_pad_bit (OSS_VOIDE * ctx);
#define _oss_ex_big_pad_bit	_oss_ex_pad_bit
extern void			_oss_ex_unbnd_bit (OSS_VOIDE * ctx);
#define _oss_ex_vary_bit	_oss_ex_unbnd_bit
/* there is no separate encoding function for NULL type */
#define _oss_ex_null		_oss_ex_not_supported
extern void			_oss_ex_real (OSS_VOIDE * ctx);
extern void			_oss_ex_char_real (OSS_VOIDE * ctx);
extern void			_oss_ex_mixed_real (OSS_VOIDE * ctx);
extern void			_oss_ex_bool (OSS_VOIDE * ctx);
extern void			_oss_ex_unbnd_any (OSS_VOIDE * ctx);
extern void			_oss_ex_seq (OSS_VOIDE * ctx);
#define _oss_ex_set		_oss_ex_seq
#define _oss_ex_embedded_pdv	_oss_ex_seq
#define _oss_ex_char_string	_oss_ex_seq
extern void			_oss_ex_choice (OSS_VOIDE * ctx);
extern void			_oss_ex_link_seqof (OSS_VOIDE * ctx);
#define _oss_ex_link_setof	_oss_ex_link_seqof
extern void			_oss_ex_dlink_seqof (OSS_VOIDE * ctx);
#define _oss_ex_dlink_setof	_oss_ex_dlink_seqof
#define _oss_ex_dlink_plus_seqof	_oss_ex_link_seqof
#define _oss_ex_dlink_plus_setof	_oss_ex_dlink_plus_seqof
extern void			_oss_ex_unbnd_seqof (OSS_VOIDE * ctx);
#define _oss_ex_unbnd_setof	_oss_ex_unbnd_seqof
#define _oss_ex_array_seqof	_oss_ex_unbnd_seqof
#define _oss_ex_array_setof	_oss_ex_unbnd_seqof
extern void			_oss_ex_unbnd_octet (OSS_VOIDE * ctx);
#define _oss_ex_vary_octet	_oss_ex_unbnd_octet
extern void                     _oss_ex_unbnd_char (OSS_VOIDE * ctx);
#define _oss_ex_vary_char	_oss_ex_unbnd_char
extern void                     _oss_ex_nlltrm_char (OSS_VOIDE * ctx);
extern void			_oss_ex_pad_char (OSS_VOIDE * ctx);
extern void			_oss_ex_link_objid (OSS_VOIDE * ctx);
extern void			_oss_ex_unbnd_objid (OSS_VOIDE * ctx);
#define _oss_ex_array_objid	_oss_ex_unbnd_objid
extern void			_oss_ex_utc_time (OSS_VOIDE * ctx);
#define _oss_ex_gen_time	_oss_ex_utc_time
extern void			_oss_ex_opentype (OSS_VOIDE * ctx);
extern void			_oss_ex_4byte_char (OSS_VOIDE * ctx);
#define _oss_ex_2byte_char	_oss_ex_4byte_char
extern void			_oss_ex_huge_int (OSS_VOIDE * ctx);
extern void			_oss_ex_encoded_objid (OSS_VOIDE * ctx);
#define _oss_ex_encoded_reloid	_oss_ex_encoded_objid
extern void                     _oss_ex_defer (OSS_VOIDE * ctx);
#define _oss_ex_defer_obj	_oss_ex_defer
extern void			_oss_ex_containing (OSS_VOIDE * ctx);
extern void _oss_ex_not_supported (OSS_VOIDE * ctx);

#define _oss_ex_nlltrm_date_time	_oss_ex_nlltrm_char

/* No support for the below C representations */
#define _oss_ex_vary_any		_oss_ex_not_supported
#define _oss_ex_unbnd_link_octet	_oss_ex_not_supported
#define _oss_ex_vary_link_octet		_oss_ex_not_supported
#define _oss_ex_unbnd_link_bit		_oss_ex_not_supported
#define _oss_ex_vary_link_bit		_oss_ex_not_supported
#define _oss_ex_vary_link_char		_oss_ex_not_supported
#define _oss_ex_nlltrm_link_char	_oss_ex_not_supported
#define _oss_ex_unbnd_link_char		_oss_ex_not_supported
#define _oss_ex_unbnd_dlink_octet	_oss_ex_not_supported
#define _oss_ex_vary_dlink_octet	_oss_ex_not_supported
#define _oss_ex_unbnd_dlink_bit		_oss_ex_not_supported
#define _oss_ex_vary_dlink_bit		_oss_ex_not_supported
#define _oss_ex_vary_dlink_char		_oss_ex_not_supported
#define _oss_ex_nlltrm_dlink_char	_oss_ex_not_supported
#define _oss_ex_unbnd_dlink_char	_oss_ex_not_supported
#define _oss_ex_unbnd_link_any		_oss_ex_not_supported
#define _oss_ex_unbnd_dlink_any		_oss_ex_not_supported
#define _oss_ex_one_char		_oss_ex_not_supported

/* ************************************************************* *
 * Decoding functions:						 *
 * ************************************************************* */
extern void			_oss_dx_int (OSS_VOIDD * ctx);
#define _oss_dx_uint		_oss_dx_int
extern void			_oss_dx_enum (OSS_VOIDD * ctx);
#define _oss_dx_uenum		_oss_dx_enum
extern void			_oss_dx_unbnd_bit (OSS_VOIDD * ctx);
#define _oss_dx_vary_bit	_oss_dx_unbnd_bit
#define _oss_dx_pad_bit		_oss_dx_unbnd_bit
#define _oss_dx_big_pad_bit	_oss_dx_unbnd_bit
#define _oss_dx_unbnd_octet	_oss_dx_unbnd_bit
#define _oss_dx_vary_octet	_oss_dx_unbnd_bit
extern void			_oss_dx_null (OSS_VOIDD * ctx);
extern void			_oss_dx_real (OSS_VOIDD * ctx);
extern void			_oss_dx_bool (OSS_VOIDD * ctx);
extern void			_oss_dx_seq (OSS_VOIDD * ctx);
#define _oss_dx_set		_oss_dx_seq
extern void			_oss_dx_choice (OSS_VOIDD * ctx);
extern void			_oss_dx_link_seqof (OSS_VOIDD * ctx);
#define _oss_dx_link_setof	_oss_dx_link_seqof
extern void			_oss_dx_dlink_seqof (OSS_VOIDD * ctx);
#define _oss_dx_dlink_setof	_oss_dx_dlink_seqof
#define _oss_dx_dlink_plus_seqof	_oss_dx_link_seqof
#define _oss_dx_dlink_plus_setof	_oss_dx_dlink_plus_seqof
extern void			_oss_dx_unbnd_seqof (OSS_VOIDD * ctx);
#define _oss_dx_unbnd_setof	_oss_dx_unbnd_seqof
#define _oss_dx_array_seqof	_oss_dx_unbnd_seqof
#define _oss_dx_array_setof	_oss_dx_unbnd_seqof
extern void			_oss_dx_unbnd_char (OSS_VOIDD * ctx);
extern void			_oss_dx_4byte_char (OSS_VOIDD * ctx);
#define _oss_dx_2byte_char	_oss_dx_4byte_char
extern void			_oss_dx_pad_char (OSS_VOIDD * ctx);
extern void			_oss_dx_nlltrm_char (OSS_VOIDD * ctx);
#define	_oss_dx_vary_char	_oss_dx_nlltrm_char
extern void			_oss_dx_array_objid (OSS_VOIDD * ctx);
extern void			_oss_dx_link_objid (OSS_VOIDD * ctx);
extern void			_oss_dx_unbnd_objid (OSS_VOIDD * ctx);
extern void			_oss_dx_utc_time (OSS_VOIDD * ctx);
#define _oss_dx_gen_time	_oss_dx_utc_time
extern void			_oss_dx_opentype (OSS_VOIDD * ctx);
#define _oss_dx_unbnd_any	_oss_dx_opentype
#define _oss_dx_embedded_pdv	_oss_dx_seq
#define _oss_dx_char_string	_oss_dx_seq
extern void			_oss_dx_char_real (OSS_VOIDD * ctx);
extern void			_oss_dx_mixed_real (OSS_VOIDD * ctx);
extern void			_oss_dx_huge_int (OSS_VOIDD * ctx);
extern void			_oss_dx_encoded_objid (OSS_VOIDD * ctx);
#define _oss_dx_encoded_reloid	_oss_dx_encoded_objid
extern void			_oss_dx_containing (OSS_VOIDD * ctx);
#define _oss_dx_defer		_oss_dx_opentype
#define _oss_dx_defer_obj	_oss_dx_opentype
extern void			_oss_dx_not_supported (OSS_VOIDD * ctx);

#define _oss_dx_nlltrm_date_time	_oss_dx_nlltrm_char

/* No support for the below C representations */
#define _oss_dx_vary_any		_oss_dx_not_supported
#define _oss_dx_unbnd_link_octet	_oss_dx_not_supported
#define _oss_dx_vary_link_octet		_oss_dx_not_supported
#define _oss_dx_unbnd_link_bit		_oss_dx_not_supported
#define _oss_dx_vary_link_bit		_oss_dx_not_supported
#define _oss_dx_vary_link_char		_oss_dx_not_supported
#define _oss_dx_nlltrm_link_char	_oss_dx_not_supported
#define _oss_dx_unbnd_link_char		_oss_dx_not_supported
#define _oss_dx_unbnd_dlink_octet	_oss_dx_not_supported
#define _oss_dx_vary_dlink_octet	_oss_dx_not_supported
#define _oss_dx_unbnd_dlink_bit		_oss_dx_not_supported
#define _oss_dx_vary_dlink_bit		_oss_dx_not_supported
#define _oss_dx_vary_dlink_char		_oss_dx_not_supported
#define _oss_dx_nlltrm_dlink_char	_oss_dx_not_supported
#define _oss_dx_unbnd_dlink_char	_oss_dx_not_supported
#define _oss_dx_unbnd_link_any		_oss_dx_not_supported
#define _oss_dx_unbnd_dlink_any		_oss_dx_not_supported
#define _oss_dx_one_char		_oss_dx_not_supported

#endif /* OSS_SOED_XER ... */

#if defined(OSS_SOED_EXER)

typedef struct {
    unsigned char *_oss_WWwJ;	    
    unsigned char *_oss_HWjJ;    
    unsigned char *_oss_wJjJ;	    
    unsigned char *_oss_JqjJ;	    
    void *_oss_QqHJ;	    
    void *_oss_HjwJ;	    
    OSS_UINT16 _oss_wWwJ;   
    OSS_UINT16 _oss_qjHJ;       
    OSS_UINT16 *_oss_WjwJ;        
} exer_extra_data;
typedef struct {
    OSS_INT32 _oss_JqWJ;	
    void ** _oss_wjwJ;	
} exer_ext;
typedef struct {
    OSS_UINT32 _oss_jWwJ;
    OSS_UINT32 _oss_wwjJ;
    unsigned char *_oss_HqJJ;	    
    OSS_UINT16 _oss_jjjJ;		    
    void *_oss_QWjJ;		    
    exer_extra_data *_oss_qjjJ;    
    exer_ext _oss_QQwJ;	    
} exer_type;

/* EXER-specific control table header */

/* Control table header */
typedef struct {
    OSS_UINT32             table_flags;
    OSS_UINT16             table_version;
    OSS_UINT16             table_compat_version;
    OSS_INT32              type_count;
    exer_type             *type_array;
    OSS_INT32              namespace_count;
    exer_namespace        *namespace_array;
    exer_search		  *namespace_search;
    exer_decode_position   root_decoder;
    unsigned char	  *reserved_prefix;
    OSS_UINT16		   control_ns;
    OSS_UINT16		   asn1_ns;
    OSS_UINT16             main_ns;
    OSS_INT32              ext_count;
    void                 **ext_array;
    OSS_INT32              ext_list_count;
    exer_extensions_list  *ext_list_array;
} exer_table;


typedef struct {
    OSS_UINT32 _oss_wjJW;	    
    OSS_UINT16 *_oss_jJJW;    
} exer_union_data;
typedef struct {
    OSS_UINT16 _oss_qWqJ;		
    OSS_UINT16 _oss_QJJW;	
    OSS_UINT32 _oss_WjQW;	
    OSS_UINT32 *_oss_JHWW;		
} exer_order_data;
typedef struct {
    exer_tag_decoder *_oss_QwqJ; 
    exer_decode_position _oss_QQQW;   
} exer_set_data;
typedef struct {
    exer_tag_decoder *_oss_QwqJ; 
    exer_decode_position *_oss_jwJW; 
} exer_sequence_data;

/* ekind- and ikind- specific functions for smart linking */

struct _EXEREncContext;
struct _EXERDecContext;

typedef void (*ossEXERencodefp)(struct _EXEREncContext *);
typedef void (*ossEXERencodeIfp)(struct _EXEREncContext *);
typedef void (*ossEXERdecodefp)(struct _EXERDecContext *);
typedef void (*ossEXERdecodeIfp)(struct _EXERDecContext *);

    /*************************************************************************/
    /*      SOED EXER functions section                                      */
    /*************************************************************************/
#if defined(DLL_LINKAGE)
extern ossEXERencodefp *_oss_getEXERencode_efp(void);
extern ossEXERdecodefp *_oss_getEXERdecode_efp(void);
extern ossEXERencodeIfp *_oss_getEXERencode_ifp(void);
extern ossEXERdecodeIfp *_oss_getEXERdecode_ifp(void);
#endif /* DLL_LINKAGE */

/* ossLink_[E|D]Exer routines should never be used with DLLs */
PUBLIC void DLL_ENTRY ossLink_EExer (OssGlobal *, void *,
	unsigned short , int const * , ossEXERencodefp *,
	unsigned short , int const * , ossEXERencodeIfp *);
PUBLIC void DLL_ENTRY ossLink_DExer (OssGlobal *, void *,
	unsigned short , int const * , ossEXERdecodefp *,
	unsigned short , int const * , ossEXERdecodeIfp *);

PUBLIC void DLL_ENTRY ossLink_ExerReal(OssGlobal *);

#endif /* OSS_SOED_EXER || !SHIPPING */

#if defined(OSS_SOED_EXER)

/* ************************************************************* *
 * Encoding functions:						 *
 * ************************************************************* */
extern void			_oss_eex_int (struct _EXEREncContext * ctx);
#define _oss_eex_uint		_oss_eex_int
extern void			_oss_eex_enum (struct _EXEREncContext * ctx);
#define _oss_eex_uenum		_oss_eex_enum
extern void			_oss_eex_pad_bit (struct _EXEREncContext * ctx);
#define _oss_eex_big_pad_bit	_oss_eex_pad_bit
extern void			_oss_eex_unbnd_bit (struct _EXEREncContext * ctx);
#define _oss_eex_vary_bit	_oss_eex_unbnd_bit
extern void			_oss_eex_null (struct _EXEREncContext * ctx);
extern void			_oss_eex_real (struct _EXEREncContext * ctx);
extern void			_oss_eex_char_real (struct _EXEREncContext * ctx);
extern void			_oss_eex_mixed_real (struct _EXEREncContext * ctx);
extern void			_oss_eex_bool (struct _EXEREncContext * ctx);
extern void			_oss_eex_unbnd_any (struct _EXEREncContext * ctx);
extern void			_oss_eex_seq (struct _EXEREncContext * ctx);
#define _oss_eex_set		_oss_eex_seq
#define _oss_eex_embedded_pdv	_oss_eex_seq
#define _oss_eex_char_string	_oss_eex_seq
extern void			_oss_eex_choice (struct _EXEREncContext * ctx);
extern void			_oss_eex_link_seqof (struct _EXEREncContext * ctx);
#define _oss_eex_dlink_plus_seqof	_oss_eex_link_seqof
#define _oss_eex_link_setof	_oss_eex_link_seqof
#define _oss_eex_dlink_plus_setof	_oss_eex_dlink_plus_seqof
extern void			_oss_eex_dlink_seqof (struct _EXEREncContext * ctx);
#define _oss_eex_dlink_setof	_oss_eex_dlink_seqof
extern void			_oss_eex_unbnd_seqof (struct _EXEREncContext * ctx);
#define _oss_eex_unbnd_setof	_oss_eex_unbnd_seqof
#define _oss_eex_array_seqof	_oss_eex_unbnd_seqof
#define _oss_eex_array_setof	_oss_eex_unbnd_seqof
extern void			_oss_eex_unbnd_octet (struct _EXEREncContext * ctx);
#define _oss_eex_vary_octet	_oss_eex_unbnd_octet
extern void                     _oss_eex_unbnd_char (struct _EXEREncContext * ctx);
#define _oss_eex_vary_char	_oss_eex_unbnd_char
#define _oss_eex_nlltrm_char	_oss_eex_unbnd_char
extern void			_oss_eex_pad_char (struct _EXEREncContext * ctx);
extern void			_oss_eex_link_objid (struct _EXEREncContext * ctx);
extern void			_oss_eex_unbnd_objid (struct _EXEREncContext * ctx);
#define _oss_eex_array_objid	_oss_eex_unbnd_objid
extern void			_oss_eex_utc_time (struct _EXEREncContext * ctx);
#define _oss_eex_gen_time	_oss_eex_utc_time
extern void			_oss_eex_opentype (struct _EXEREncContext * ctx);
extern void			_oss_eex_4byte_char (struct _EXEREncContext * ctx);
#define _oss_eex_2byte_char	_oss_eex_4byte_char
extern void			_oss_eex_huge_int (struct _EXEREncContext * ctx);
extern void			_oss_eex_encoded_objid (struct _EXEREncContext * ctx);
#define _oss_eex_encoded_reloid	_oss_eex_encoded_objid
extern void			_oss_eex_containing (struct _EXEREncContext * ctx);
extern void _oss_eex_not_supported (struct _EXEREncContext * ctx);
#define _oss_eex_defer		_oss_eex_not_supported
#define _oss_eex_defer_obj	_oss_eex_not_supported

#define _oss_eex_nlltrm_date_time	_oss_eex_nlltrm_char

/* ************************************************************* *
 * Encoding i_kind functions:					 *
 * ************************************************************* */
extern void    			_oss_eex_anyattr_nlltrm_char(struct _EXEREncContext * ctx);

extern void  			_oss_eex_normal_int(struct _EXEREncContext * ctx);
extern void 			_oss_eex_normal_enum(struct _EXEREncContext * ctx);
extern void    			_oss_eex_base64_unbnd_char(struct _EXEREncContext * ctx);
extern void    			_oss_eex_normal_unbnd_char(struct _EXEREncContext * ctx);
extern void    			_oss_eex_base64_unbnd_octet(struct _EXEREncContext * ctx);
extern void    			_oss_eex_normal_unbnd_octet(struct _EXEREncContext * ctx);
#define _oss_eex_base64_pad_char	_oss_eex_base64_unbnd_char
#define	_oss_eex_normal_pad_char	_oss_eex_normal_unbnd_char
#define	_oss_eex_base64_nlltrm_char	_oss_eex_base64_unbnd_char
#define _oss_eex_normal_nlltrm_char	_oss_eex_normal_unbnd_char
extern void			_oss_eex_anyel_nlltrm_char(struct _EXEREncContext * ctx);
extern void    			_oss_eex_base64_4byte_char(struct _EXEREncContext * ctx);
extern void    			_oss_eex_normal_4byte_char(struct _EXEREncContext * ctx);
#define _oss_eex_useorder_seq		_oss_eex_not_supported
extern void    			_oss_eex_qname_seq(struct _EXEREncContext * ctx);
#define _oss_eex_normal_seq		_oss_eex_not_supported
#define _oss_eex_anyattr_link_sof	_oss_eex_anyattr_nlltrm_char
#define _oss_eex_normal_link_sof	_oss_eex_not_supported
#define _oss_eex_base64_opentype	_oss_eex_base64_unbnd_octet
#define _oss_eex_normal_opentype	_oss_eex_normal_unbnd_octet
extern void			_oss_eex_normal_real(struct _EXEREncContext * ctx);
extern void			_oss_eex_modified_real(struct _EXEREncContext * ctx);
extern void			_oss_eex_normal_char_real(struct _EXEREncContext * ctx);
extern void			_oss_eex_modified_char_real(struct _EXEREncContext * ctx);
extern void			_oss_eex_decimal_char_real(struct _EXEREncContext * ctx);

/* No support for the below C representations */
#define _oss_eex_vary_any		_oss_eex_not_supported
#define _oss_eex_unbnd_link_octet	_oss_eex_not_supported
#define _oss_eex_vary_link_octet	_oss_eex_not_supported
#define _oss_eex_unbnd_link_bit		_oss_eex_not_supported
#define _oss_eex_vary_link_bit		_oss_eex_not_supported
#define _oss_eex_vary_link_char		_oss_eex_not_supported
#define _oss_eex_nlltrm_link_char	_oss_eex_not_supported
#define _oss_eex_unbnd_link_char	_oss_eex_not_supported
#define _oss_eex_unbnd_dlink_octet	_oss_eex_not_supported
#define _oss_eex_vary_dlink_octet	_oss_eex_not_supported
#define _oss_eex_unbnd_dlink_bit	_oss_eex_not_supported
#define _oss_eex_vary_dlink_bit		_oss_eex_not_supported
#define _oss_eex_vary_dlink_char	_oss_eex_not_supported
#define _oss_eex_nlltrm_dlink_char	_oss_eex_not_supported
#define _oss_eex_unbnd_dlink_char	_oss_eex_not_supported
#define _oss_eex_unbnd_link_any		_oss_eex_not_supported
#define _oss_eex_unbnd_dlink_any	_oss_eex_not_supported
#define _oss_eex_one_char		_oss_eex_not_supported

/* ************************************************************* *
 * Decoding functions:						 *
 * ************************************************************* */
extern void			_oss_dex_int (struct _EXERDecContext * ctx);
#define _oss_dex_uint		_oss_dex_int
#define _oss_dex_enum		_oss_dex_int
#define _oss_dex_uenum		_oss_dex_int
extern void			_oss_dex_unbnd_bit (struct _EXERDecContext * ctx);
#define _oss_dex_vary_bit	_oss_dex_unbnd_bit
#define _oss_dex_pad_bit	_oss_dex_unbnd_bit
#define _oss_dex_big_pad_bit	_oss_dex_unbnd_bit
extern void			_oss_dex_unbnd_octet (struct _EXERDecContext * ctx);
#define _oss_dex_vary_octet	_oss_dex_unbnd_octet
extern void			_oss_dex_null (struct _EXERDecContext * ctx);
extern void			_oss_dex_real (struct _EXERDecContext * ctx);
extern void			_oss_dex_bool (struct _EXERDecContext * ctx);
extern void			_oss_dex_seq (struct _EXERDecContext * ctx);
extern void			_oss_dex_set (struct _EXERDecContext * ctx);
extern void			_oss_dex_choice (struct _EXERDecContext * ctx);
extern void			_oss_dex_link_seqof (struct _EXERDecContext * ctx);
#define _oss_dex_dlink_plus_seqof	_oss_dex_link_seqof
#define _oss_dex_link_setof	_oss_dex_link_seqof
#define _oss_dex_dlink_plus_setof	_oss_dex_dlink_plus_seqof
extern void    			_oss_dex_dlink_seqof (struct _EXERDecContext * ctx);
#define _oss_dex_dlink_setof	_oss_dex_dlink_seqof
extern void			_oss_dex_unbnd_seqof (struct _EXERDecContext * ctx);
#define _oss_dex_unbnd_setof	_oss_dex_unbnd_seqof
#define _oss_dex_array_seqof	_oss_dex_unbnd_seqof
#define _oss_dex_array_setof	_oss_dex_unbnd_seqof
extern void			_oss_dex_unbnd_char (struct _EXERDecContext * ctx);
extern void			_oss_dex_4byte_char (struct _EXERDecContext * ctx);
#define _oss_dex_2byte_char	_oss_dex_4byte_char
extern void			_oss_dex_pad_char (struct _EXERDecContext * ctx);
extern void			_oss_dex_nlltrm_char (struct _EXERDecContext * ctx);
#define	_oss_dex_vary_char	_oss_dex_nlltrm_char
extern void			_oss_dex_array_objid (struct _EXERDecContext * ctx);
extern void			_oss_dex_link_objid (struct _EXERDecContext * ctx);
extern void			_oss_dex_unbnd_objid (struct _EXERDecContext * ctx);
extern void			_oss_dex_utc_time (struct _EXERDecContext * ctx);
#define _oss_dex_gen_time	_oss_dex_utc_time
extern void			_oss_dex_opentype (struct _EXERDecContext * ctx);
#define _oss_dex_unbnd_any	_oss_dex_opentype
#define _oss_dex_embedded_pdv	_oss_dex_seq
#define _oss_dex_char_string	_oss_dex_seq
extern void			_oss_dex_char_real (struct _EXERDecContext * ctx);
extern void			_oss_dex_mixed_real (struct _EXERDecContext * ctx);
extern void			_oss_dex_huge_int (struct _EXERDecContext * ctx);
extern void			_oss_dex_encoded_objid (struct _EXERDecContext * ctx);
#define _oss_dex_encoded_reloid	_oss_dex_encoded_objid
extern void			_oss_dex_containing (struct _EXERDecContext * ctx);
extern void			_oss_dex_not_supported (struct _EXERDecContext * ctx);
#define _oss_dex_defer		_oss_dex_not_supported
#define _oss_dex_defer_obj	_oss_dex_not_supported

#define _oss_dex_nlltrm_date_time	_oss_dex_nlltrm_char

/* ************************************************************* *
 * Decoding i_kind functions:					 *
 * ************************************************************* */
extern void    			_oss_dex_anyattr_nlltrm_char(struct _EXERDecContext *ctx);

#define _oss_dex_normal_int	_oss_dex_not_supported
#define _oss_dex_normal_enum	_oss_dex_not_supported
extern void    			_oss_dex_base64_unbnd_char(struct _EXERDecContext * ctx);
extern void    			_oss_dex_normal_unbnd_char(struct _EXERDecContext * ctx);
extern void                     _oss_dex_base64_unbnd_octet(struct _EXERDecContext * ctx);
extern void                     _oss_dex_normal_unbnd_octet(struct _EXERDecContext * ctx);
extern void    			_oss_dex_base64_pad_char(struct _EXERDecContext * ctx);
extern void    			_oss_dex_normal_pad_char(struct _EXERDecContext * ctx);
extern void    			_oss_dex_base64_nlltrm_char(struct _EXERDecContext * ctx);
extern void			_oss_dex_anyel_nlltrm_char(struct _EXERDecContext *ctx);
extern void    			_oss_dex_normal_nlltrm_char(struct _EXERDecContext * ctx);
extern void    			_oss_dex_base64_4byte_char(struct _EXERDecContext * ctx);
extern void    			_oss_dex_normal_4byte_char(struct _EXERDecContext * ctx);
#define _oss_dex_useorder_seq	_oss_dex_set
extern void			_oss_dex_qname_seq(struct _EXERDecContext *ctx);
extern void			_oss_dex_normal_seq(struct _EXERDecContext *ctx);
#define _oss_dex_anyattr_link_sof _oss_dex_anyattr_nlltrm_char
extern void			_oss_dex_normal_link_sof(struct _EXERDecContext *ctx);
extern void    			_oss_dex_base64_opentype(struct _EXERDecContext * ctx);
extern void    			_oss_dex_normal_opentype(struct _EXERDecContext * ctx);
extern void			_oss_dex_normal_real(struct _EXERDecContext * ctx);
extern void			_oss_dex_modified_real(struct _EXERDecContext * ctx);
extern void			_oss_dex_normal_char_real(struct _EXERDecContext * ctx);
extern void			_oss_dex_modified_char_real(struct _EXERDecContext * ctx);
extern void			_oss_dex_decimal_char_real(struct _EXERDecContext * ctx);

extern ossBoolean DLL_ENTRY ossEXValidateAnyUri(unsigned char *string, long length);

/* No support for the below C representations */
#define _oss_dex_vary_any		_oss_dex_not_supported
#define _oss_dex_unbnd_link_octet	_oss_dex_not_supported
#define _oss_dex_vary_link_octet	_oss_dex_not_supported
#define _oss_dex_unbnd_link_bit		_oss_dex_not_supported
#define _oss_dex_vary_link_bit		_oss_dex_not_supported
#define _oss_dex_vary_link_char		_oss_dex_not_supported
#define _oss_dex_nlltrm_link_char	_oss_dex_not_supported
#define _oss_dex_unbnd_link_char	_oss_dex_not_supported
#define _oss_dex_unbnd_dlink_octet	_oss_dex_not_supported
#define _oss_dex_vary_dlink_octet	_oss_dex_not_supported
#define _oss_dex_unbnd_dlink_bit	_oss_dex_not_supported
#define _oss_dex_vary_dlink_bit		_oss_dex_not_supported
#define _oss_dex_vary_dlink_char	_oss_dex_not_supported
#define _oss_dex_nlltrm_dlink_char	_oss_dex_not_supported
#define _oss_dex_unbnd_dlink_char	_oss_dex_not_supported
#define _oss_dex_unbnd_link_any		_oss_dex_not_supported
#define _oss_dex_unbnd_dlink_any	_oss_dex_not_supported
#define _oss_dex_one_char		_oss_dex_not_supported

#endif /* OSS_SOED_EXER && !LED_SOURCECODE_SHIPPING */


#endif /* _OSS_CODE_FILE */

/*****************************************************************************/
/*  NON-PUBLIC section: data alignment section - revert to user-defined data */
/*  alignment                                                                */
/*****************************************************************************/

#ifdef __cplusplus
}
#endif

#if defined(macintosh) && defined(__CFM68K__)
#pragma import reset
#endif

#if defined(_MSC_VER) && (defined(_WIN32) || defined(WIN32) \
			|| defined(WINCE) || defined(_WIN64))
#pragma pack(pop, ossPacking)
#elif defined(_MSC_VER) && (defined(_WINDOWS) || defined(_MSDOS))
#pragma pack()
#elif defined(__BORLANDC__) && (defined(__WIN32__) || defined(__MSDOS__))
#pragma option -a.
#elif defined(__IBMC__) && (defined(__WIN32__) || defined(__OS2__))
#pragma pack()
#elif defined(__WATCOMC__) && (defined(__NT__) || defined(__WINDOWS__) \
			|| defined(__DOS__))
#pragma pack(pop)
#endif /* _MSC_VER && _WIN32 */

#endif /* !OSSTYPE_H */

