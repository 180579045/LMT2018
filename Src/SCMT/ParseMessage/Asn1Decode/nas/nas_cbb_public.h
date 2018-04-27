/*******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
********************************************************************************
* �ļ�����: nas_cbb_public.h 
* ��������: nas�����CBB���⹫��ͷ�ļ�
* ����˵��: ������ʹ���ļ�����ʱ����Լ������                            
*      				                    
* ��д����: ��YYYY/MM/DD��
* �޸���ʷ: 
* [��ʽҪ���޸İ汾ʹ����λ�ַ���ʾ, ����ǰ2λΪ����, ��ʾ�ϴ��޸�; ��1λΪ 
*  ��ĸ������, ��ʾ��С�޸ģ��޸����ڲ���"yyyy/mm/dd"�ĸ�ʽ���޸��˱���������
*  ȫ�����̶�ռ3�������ֵĴ�С�޸�����������Ҫ����80�У���������֮������룻
*  �汾�����ڡ��޸��ˡ��޸�����֮��ʹ��1���ո�ָ�]
*  
* �޸İ汾  �޸�����   �޸���  �޸�����
* ------------------------------------------------------------------------------
* 01a      2006/04/24  ������ ���޸�����������
*							
*******************************************************************************/

/******************************** ͷ�ļ�������ͷ ******************************/
#ifndef NAS_CBB_PUBLIC_H
#define NAS_CBB_PUBLIC_H

/******************************** �����ļ����� ********************************/
#include <stdarg.h>

#include "nas_cbb_type.h"

/******************************** ��ͳ������� ********************************/
#ifdef WIN32
#define NAS_API __stdcall
#else
#define NAS_API
#endif
/******************************** ���Ͷ��� ************************************/
/* �ڴ�������API */
typedef VOID * (* NAS_MEM_MALLOC_FUNC_PT)(UINT32 ulSize);
typedef VOID   (* NAS_MEM_FREE_FUNC_PT)(VOID *pvBuf);
typedef VOID * (* NAS_MEM_SET_FUNC_PT)(VOID *pvDst, UINT8 ucByte, UINT32 ulSize);
typedef VOID * (* NAS_MEM_COPY_FUNC_PT)(VOID *pvDst, const VOID *pvSrc, UINT32 ulSize);
/* ��־��ӡ���API */
typedef INT32  (* NAS_LOG_FUNC_PT)(UINT8 ucLogLevel, const STRING szFormat, ...);
typedef INT32  (* NAS_SPRINTF_FUNC_PT)(STRING szBuf, const STRING szFormat, ...);
typedef INT32  (* NAS_VSPRINTF_FUNC_PT)(STRING szBuf, const STRING szFormat, va_list args);
/* �ֽ���ת�����API */
typedef UINT16 (NAS_API * NAS_HTON_U16_FUNC_PT)(UINT16 usValue);
typedef UINT16 (NAS_API * NAS_NTOH_U16_FUNC_PT)(UINT16 usValue);
typedef UINT32 (NAS_API * NAS_HTON_U32_FUNC_PT)(UINT32 ulValue);
typedef UINT32 (NAS_API * NAS_NTOH_U32_FUNC_PT)(UINT32 ulValue);
/* SHELL�������API */
typedef INT32  (* NAS_SHCMD_FUNC_PT)(UINT32 ulPara0, UINT32 ulPara1, UINT32 ulPara2, UINT32 ulPara3);
typedef VOID   (* NAS_SHCMD_PRINTF_FUNC_PT)(const STRING szFormat, ...);
typedef VOID   (* NAS_SHCMD_REG_FUNC_PF)(STRING szFuncName, STRING szFuncHelp, STRING szArgFmt, NAS_SHCMD_FUNC_PT pfShcmdFunc);

/* API�� */
typedef struct
{
    /* �ڴ�������API */
    NAS_MEM_MALLOC_FUNC_PT pfuncMemMalloc;
    NAS_MEM_FREE_FUNC_PT   pfuncMemFree;
    NAS_MEM_SET_FUNC_PT    pfuncMemSet;
    NAS_MEM_COPY_FUNC_PT   pfuncMemCopy;
    /* ��־��ӡ���API */
    NAS_LOG_FUNC_PT        pfuncLog;
    NAS_SPRINTF_FUNC_PT    pfuncSprintf;
    NAS_VSPRINTF_FUNC_PT   pfuncVsprintf;
    /* �ֽ���ת�����API */
    NAS_HTON_U16_FUNC_PT   pfuncHtonU16;
    NAS_NTOH_U16_FUNC_PT   pfuncNtohU16;
    NAS_HTON_U32_FUNC_PT   pfuncHtonU32;
    NAS_NTOH_U32_FUNC_PT   pfuncNtohU32;
    /* SHELL�������API */
    NAS_SHCMD_PRINTF_FUNC_PT pfuncShPrintf;
    NAS_SHCMD_REG_FUNC_PF    pfuncShReg;
}NAS_REFERENCE_APIS_T, *NAS_REFERENCE_APIS_PT;
/******************************** ȫ�ֱ������� ********************************/

/******************************** �ⲿ����ԭ������ ****************************/
/* ��API�� */
UINT32 nas_api_bind(NAS_REFERENCE_APIS_PT pstApis);
/* ȥ��API�� */
UINT32 nas_api_unbind();
/* Nas���� */
UINT32 nas_encode( void *pvInMsg,  UINT8 *pucOutBufAddr, UINT32 *puiMsgLen, UINT32 uiInMaxLen);
/* Nas���� */
UINT32 nas_decode(UINT8 *pucInBufAddr,  UINT32 ulInMsgLen, void *pvOutMsg, UINT32 ulInMaxLen, UINT8 *pucMsgType);
/* ��ӡ�������Ϣ�ṹ�������ӡ���� */
UINT32 nas_message_decode_print(UINT8 ucMsgType, UINT8 *pvInMsg,UINT8 *pucOutBuf,UINT32 ulLen);
/******************************** ͷ�ļ�������β ******************************/
#endif /* NAS_PUBLIC_H */
/******************************** ͷ�ļ����� **********************************/
