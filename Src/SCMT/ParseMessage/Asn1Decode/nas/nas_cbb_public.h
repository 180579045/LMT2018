/*******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
********************************************************************************
* 文件名称: nas_cbb_public.h 
* 功能描述: nas编解码CBB对外公布头文件
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
#ifndef NAS_CBB_PUBLIC_H
#define NAS_CBB_PUBLIC_H

/******************************** 包含文件声明 ********************************/
#include <stdarg.h>

#include "nas_cbb_type.h"

/******************************** 宏和常量定义 ********************************/
#ifdef WIN32
#define NAS_API __stdcall
#else
#define NAS_API
#endif
/******************************** 类型定义 ************************************/
/* 内存操作相关API */
typedef VOID * (* NAS_MEM_MALLOC_FUNC_PT)(UINT32 ulSize);
typedef VOID   (* NAS_MEM_FREE_FUNC_PT)(VOID *pvBuf);
typedef VOID * (* NAS_MEM_SET_FUNC_PT)(VOID *pvDst, UINT8 ucByte, UINT32 ulSize);
typedef VOID * (* NAS_MEM_COPY_FUNC_PT)(VOID *pvDst, const VOID *pvSrc, UINT32 ulSize);
/* 日志打印相关API */
typedef INT32  (* NAS_LOG_FUNC_PT)(UINT8 ucLogLevel, const STRING szFormat, ...);
typedef INT32  (* NAS_SPRINTF_FUNC_PT)(STRING szBuf, const STRING szFormat, ...);
typedef INT32  (* NAS_VSPRINTF_FUNC_PT)(STRING szBuf, const STRING szFormat, va_list args);
/* 字节序转换相关API */
typedef UINT16 (NAS_API * NAS_HTON_U16_FUNC_PT)(UINT16 usValue);
typedef UINT16 (NAS_API * NAS_NTOH_U16_FUNC_PT)(UINT16 usValue);
typedef UINT32 (NAS_API * NAS_HTON_U32_FUNC_PT)(UINT32 ulValue);
typedef UINT32 (NAS_API * NAS_NTOH_U32_FUNC_PT)(UINT32 ulValue);
/* SHELL命令相关API */
typedef INT32  (* NAS_SHCMD_FUNC_PT)(UINT32 ulPara0, UINT32 ulPara1, UINT32 ulPara2, UINT32 ulPara3);
typedef VOID   (* NAS_SHCMD_PRINTF_FUNC_PT)(const STRING szFormat, ...);
typedef VOID   (* NAS_SHCMD_REG_FUNC_PF)(STRING szFuncName, STRING szFuncHelp, STRING szArgFmt, NAS_SHCMD_FUNC_PT pfShcmdFunc);

/* API集 */
typedef struct
{
    /* 内存操作相关API */
    NAS_MEM_MALLOC_FUNC_PT pfuncMemMalloc;
    NAS_MEM_FREE_FUNC_PT   pfuncMemFree;
    NAS_MEM_SET_FUNC_PT    pfuncMemSet;
    NAS_MEM_COPY_FUNC_PT   pfuncMemCopy;
    /* 日志打印相关API */
    NAS_LOG_FUNC_PT        pfuncLog;
    NAS_SPRINTF_FUNC_PT    pfuncSprintf;
    NAS_VSPRINTF_FUNC_PT   pfuncVsprintf;
    /* 字节序转换相关API */
    NAS_HTON_U16_FUNC_PT   pfuncHtonU16;
    NAS_NTOH_U16_FUNC_PT   pfuncNtohU16;
    NAS_HTON_U32_FUNC_PT   pfuncHtonU32;
    NAS_NTOH_U32_FUNC_PT   pfuncNtohU32;
    /* SHELL命令相关API */
    NAS_SHCMD_PRINTF_FUNC_PT pfuncShPrintf;
    NAS_SHCMD_REG_FUNC_PF    pfuncShReg;
}NAS_REFERENCE_APIS_T, *NAS_REFERENCE_APIS_PT;
/******************************** 全局变量声明 ********************************/

/******************************** 外部函数原形声明 ****************************/
/* 绑定API集 */
UINT32 nas_api_bind(NAS_REFERENCE_APIS_PT pstApis);
/* 去绑定API集 */
UINT32 nas_api_unbind();
/* Nas编码 */
UINT32 nas_encode( void *pvInMsg,  UINT8 *pucOutBufAddr, UINT32 *puiMsgLen, UINT32 uiInMaxLen);
/* Nas解码 */
UINT32 nas_decode(UINT8 *pucInBufAddr,  UINT32 ulInMsgLen, void *pvOutMsg, UINT32 ulInMaxLen, UINT8 *pucMsgType);
/* 打印解码后消息结构，输出打印缓存 */
UINT32 nas_message_decode_print(UINT8 ucMsgType, UINT8 *pvInMsg,UINT8 *pucOutBuf,UINT32 ulLen);
/******************************** 头文件保护结尾 ******************************/
#endif /* NAS_PUBLIC_H */
/******************************** 头文件结束 **********************************/
