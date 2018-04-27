/*******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
********************************************************************************
* �ļ�����: nas_cbb_adapt.h 
* ��������: ����Ҫ�������ļ��Ĺ��ܡ����ݼ���Ҫģ�飩
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
#ifndef NAS_CBB_ADAPT_H
#define NAS_CBB_ADAPT_H
#include "nas_cbb_type.h"
/******************************** �����ļ����� ********************************/

/******************************** ��ͳ������� ********************************/
/* �ڴ����API���� */
#define cpss_mem_malloc (*g_pfNASCbbMalloc)
#define cpss_mem_free   (*g_pfNASCbbFree)
#define cpss_mem_memset (*g_pfNASCbbMemset)
#define cpss_mem_memcpy (*g_pfNASCbbMemcpy)

/* ��ӡ��־���API���� */
#define SWP_SUBSYS_MSPS      ((UINT8)27)
#define MSPS_MODULE_SIG       ((UINT8)2)

#define MSPS_SUCCESS         ((UINT8)0)
#define MSPS_FAIL32          ((UINT8)0xffffffff)
#define MSPS_FAIL8          ((UINT8)0xff)


#define CPSS_LOG_FATaL (CBB_PRINT_FATAL)
#define CPSS_LOG_FAIL  (CBB_PRINT_FAIL)
#define CPSS_LOG_ERROR (CBB_PRINT_ERROR)
#define CPSS_LOG_INFO  (CBB_PRINT_INFO)

#define MSPS_LOG_INFO  (CBB_PRINT_INFO)
#define MSPS_LOG_FAIL  (CBB_PRINT_FAIL)
#define MSPS_LOG_FATAL (CBB_PRINT_FATAL)

#define CBB_PRINT_FATAL       ((UINT8)(0x01))   /* ��ӡ������������ */
#define CBB_PRINT_FAIL        ((UINT8)(0x02))   /* ��ӡ����ҵ��ʧ�� */
#define CBB_PRINT_ERROR       ((UINT8)(0x03))   /* ��ӡ����һ���Դ��� */
#define CBB_PRINT_WARN        ((UINT8)(0x04))   /* ��ӡ���𣺾�����Ϣ */
#define CBB_PRINT_INFO        ((UINT8)(0x05))   /* ��ӡ����һ����Ϣ*/
#define CBB_PRINT_DETAIL      ((UINT8)(0x06))   /* ��ӡ������ϸ��Ϣ*/
#define CBB_PRINT_IMPORTANT   ((UINT8)(0x07))   /* ��ӡ����ֻ����־����ӡ*/



#define MSPS_SIG_LOG_FATAL        SWP_SUBSYS_MSPS,MSPS_MODULE_SIG,MSPS_LOG_FATAL
#define MSPS_SIG_LOG_FAIL        SWP_SUBSYS_MSPS,MSPS_MODULE_SIG,MSPS_LOG_FAIL
#define MSPS_SIG_LOG_INFO        SWP_SUBSYS_MSPS,MSPS_MODULE_SIG,MSPS_LOG_INFO

#define CBB_BIND_ERROR ("cbb_NAS_enc_dec API has not been bonnd.\n")

/* �ֽ���ת��API���� */
#define cpss_htons  (*g_pfNASCbbHton16)
#define cpss_ntohs  (*g_pfNASCbbNtoh16)
#define cpss_htonl  (*g_pfNASCbbHton32)
#define cpss_ntohl  (*g_pfNASCbbNtoh32)
#define cpss_hton16 (*g_pfNASCbbHton16)
#define cpss_ntoh16 (*g_pfNASCbbNtoh16)
#define cpss_hton32 (*g_pfNASCbbHton32)
#define cpss_ntoh32 (*g_pfNASCbbNtoh32)

/* SHELL�������API���� */
#define oams_shcmd_printf (*g_pfNASCbbShcmdPrintf)
#define oams_shcmd_reg    (*g_pfNASCbbShcmdReg)

#if (SWP_OS_TYPE != SWP_OS_LINUX)
#define __FUNCTION__    ""
#define ___FILENAME__   ""
#endif 

/******************************** ���Ͷ��� ************************************/

/******************************** ȫ�ֱ������� ********************************/
extern UINT8                    g_ucNASCbbApiBind;
extern NAS_MEM_MALLOC_FUNC_PT   g_pfNASCbbMalloc;
extern NAS_MEM_FREE_FUNC_PT     g_pfNASCbbFree;
extern NAS_MEM_SET_FUNC_PT      g_pfNASCbbMemset;
extern NAS_MEM_COPY_FUNC_PT     g_pfNASCbbMemcpy;
extern NAS_HTON_U16_FUNC_PT     g_pfNASCbbHton16;
extern NAS_NTOH_U16_FUNC_PT     g_pfNASCbbNtoh16;
extern NAS_HTON_U32_FUNC_PT     g_pfNASCbbHton32;
extern NAS_NTOH_U32_FUNC_PT     g_pfNASCbbNtoh32;
extern NAS_SHCMD_PRINTF_FUNC_PT g_pfNASCbbShcmdPrintf;
extern NAS_SHCMD_REG_FUNC_PF    g_pfNASCbbShcmdReg;

/******************************** �ⲿ����ԭ������ ****************************/
/* ��ӡ��־���API���� */
INT32 MSPS_LOG(UINT8 ucSubSystemID, UINT8 ucModuleID, UINT8 ucPrintLevel, STRING szFormat, ...);
INT32 nas_cbb_vsprintf(STRING vpBuf, const STRING vpFormat, va_list stValist);
/******************************** ͷ�ļ�������β ******************************/
#endif /* NAS_CBB_ADAPT_H */
/******************************** ͷ�ļ����� **********************************/
