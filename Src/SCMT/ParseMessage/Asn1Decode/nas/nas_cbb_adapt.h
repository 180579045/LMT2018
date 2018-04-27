/*******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
********************************************************************************
* 文件名称: nas_cbb_adapt.h 
* 功能描述: （简要描述本文件的功能、内容及主要模块）
* 其它说明: （描述使用文件功能时的制约条件）                            
*      				                    
* 编写日期: （YYYY/MM/DD）
* 修改历史: 
* [格式要求：修改版本使用三位字符表示, 其中前2位为数字, 表示较大修改; 后1位为 
*  字母或数字, 表示较小修改；修改日期采用"yyyy/mm/dd"的格式；修改人必须用中文
*  全名，固定占3个中文字的大小修改内容描述不要超过80列，多行描述之间左对齐；
*  版本、日期、修改人、修改描述之间使用1个空格分割]
*  
* 修改版本  修改日期   修改人  修改内容
* ------------------------------------------------------------------------------
* 01a      2006/04/24  王老五 （修改内容描述）
*							
*******************************************************************************/

/******************************** 头文件保护开头 ******************************/
#ifndef NAS_CBB_ADAPT_H
#define NAS_CBB_ADAPT_H
#include "nas_cbb_type.h"
/******************************** 包含文件声明 ********************************/

/******************************** 宏和常量定义 ********************************/
/* 内存操作API适配 */
#define cpss_mem_malloc (*g_pfNASCbbMalloc)
#define cpss_mem_free   (*g_pfNASCbbFree)
#define cpss_mem_memset (*g_pfNASCbbMemset)
#define cpss_mem_memcpy (*g_pfNASCbbMemcpy)

/* 打印日志相关API适配 */
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

#define CBB_PRINT_FATAL       ((UINT8)(0x01))   /* 打印级别：致命错误 */
#define CBB_PRINT_FAIL        ((UINT8)(0x02))   /* 打印级别：业务失败 */
#define CBB_PRINT_ERROR       ((UINT8)(0x03))   /* 打印级别：一般性错误 */
#define CBB_PRINT_WARN        ((UINT8)(0x04))   /* 打印级别：警告信息 */
#define CBB_PRINT_INFO        ((UINT8)(0x05))   /* 打印级别：一般信息*/
#define CBB_PRINT_DETAIL      ((UINT8)(0x06))   /* 打印级别：详细信息*/
#define CBB_PRINT_IMPORTANT   ((UINT8)(0x07))   /* 打印级别：只记日志不打印*/



#define MSPS_SIG_LOG_FATAL        SWP_SUBSYS_MSPS,MSPS_MODULE_SIG,MSPS_LOG_FATAL
#define MSPS_SIG_LOG_FAIL        SWP_SUBSYS_MSPS,MSPS_MODULE_SIG,MSPS_LOG_FAIL
#define MSPS_SIG_LOG_INFO        SWP_SUBSYS_MSPS,MSPS_MODULE_SIG,MSPS_LOG_INFO

#define CBB_BIND_ERROR ("cbb_NAS_enc_dec API has not been bonnd.\n")

/* 字节序转换API适配 */
#define cpss_htons  (*g_pfNASCbbHton16)
#define cpss_ntohs  (*g_pfNASCbbNtoh16)
#define cpss_htonl  (*g_pfNASCbbHton32)
#define cpss_ntohl  (*g_pfNASCbbNtoh32)
#define cpss_hton16 (*g_pfNASCbbHton16)
#define cpss_ntoh16 (*g_pfNASCbbNtoh16)
#define cpss_hton32 (*g_pfNASCbbHton32)
#define cpss_ntoh32 (*g_pfNASCbbNtoh32)

/* SHELL命令相关API适配 */
#define oams_shcmd_printf (*g_pfNASCbbShcmdPrintf)
#define oams_shcmd_reg    (*g_pfNASCbbShcmdReg)

#if (SWP_OS_TYPE != SWP_OS_LINUX)
#define __FUNCTION__    ""
#define ___FILENAME__   ""
#endif 

/******************************** 类型定义 ************************************/

/******************************** 全局变量声明 ********************************/
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

/******************************** 外部函数原形声明 ****************************/
/* 打印日志相关API适配 */
INT32 MSPS_LOG(UINT8 ucSubSystemID, UINT8 ucModuleID, UINT8 ucPrintLevel, STRING szFormat, ...);
INT32 nas_cbb_vsprintf(STRING vpBuf, const STRING vpFormat, va_list stValist);
/******************************** 头文件保护结尾 ******************************/
#endif /* NAS_CBB_ADAPT_H */
/******************************** 头文件结束 **********************************/
