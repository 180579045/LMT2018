/******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
*******************************************************************************
* 文件名称: nas_cbb_type.h
* 功    能: 统一软件平台提供的基本宏定义、数据类型定义、数据结构定义等。
* 说    明: 
* 修改历史:
* 2008/11/06 岳小海 齐志强初始版本
******************************************************************************/

/******************************** 头文件保护开头 *****************************/
#ifndef NAS_CBB_TYPE_H
#define NAS_CBB_TYPE_H

/********************************* 头文件包含 ********************************/
#ifdef  __cplusplus
extern "C" {
#endif
/******************************** 宏和常量定义 *******************************/
    
/* 操作系统类型宏定义 */
/*                             0       保留 */
#define SWP_OS_WINDOWS         1    /* Windows */
#define SWP_OS_VXWORKS         2    /* VxWorks */
#define SWP_OS_LINUX           3    /* 嵌入式Linux */
#define SWP_OS_NONE            4    /* 无操作系统 */
/*                         ～255       保留 */
#if (!defined(SWP_OS_TYPE))
#define SWP_OS_TYPE SWP_OS_WINDOWS
#endif

#ifndef NULL
#ifdef __cplusplus
#define NULL    (0)
#else
#define NULL    ((void *) 0)
#endif
#endif

#if (SWP_OS_TYPE == SWP_OS_VXWORKS)
#define SOCKET_ERROR ERROR
#define INVALID_SOCKET ERROR
#endif

#ifndef FALSE
#define FALSE   (0)
#endif

#ifndef TRUE
#define TRUE    (1)
#endif

#ifndef CPSS_OK
#define CPSS_OK (0)
#endif

#ifndef CPSS_ERROR
#define CPSS_ERROR  (-1)
#endif

#ifndef NO_WAIT
#define NO_WAIT (0)
#endif
#ifndef WAIT_FOREVER
#define WAIT_FOREVER 0xffffffff
#endif

#ifndef VOS_SEM_Q_FIFO
#define VOS_SEM_Q_FIFO  0x00
#endif
#ifndef VOS_SEM_Q_PRI
#define VOS_SEM_Q_PRI   0x01
#endif

#define VOID void
#define NUM_ENTS(array) (sizeof (array) / sizeof ((array) [0]))
/******************************** 类型定义 ***********************************/
/* 公共数据类型定义 */

/*
 *VXWORKS下系统文件中有相同定义
 */
#if (SWP_OS_VXWORKS != SWP_OS_TYPE)
typedef    char*  STRING;

typedef	char    INT8;
typedef	short   INT16;
typedef	int     INT32;

typedef	unsigned char   UINT8;
typedef	unsigned short  UINT16;
typedef	unsigned int    UINT32;

typedef	int     BOOL;

/* 避免与系统文件冲突，并保持兼容性*/
/* yuexh, 2009.10.23 */
#ifndef DT_DUX_USING
#undef STATUS
#define STATUS  int
#endif

#endif

#if (SWP_OS_TYPE == SWP_OS_WINDOWS) || (SWP_OS_TYPE == SWP_OS_LINUX)
/* BITS8，BITS16，BITS32，CHAR等类型，由于兼容性需要，暂时保留 */

typedef   UINT8                BITS8;          /* bt */
typedef   UINT16               BITS16;         /* bt */
typedef   UINT32               BITS32;         /* bt */

typedef   char                 CHAR;        
#endif

/* WINDOWS下基本数据类型定义 */
#if (SWP_OS_TYPE == SWP_OS_WINDOWS)
typedef __int64 INT64;
typedef unsigned __int64 UINT64;
#endif 

/* LINUX下基本数据类型定义 */
#if (SWP_OS_TYPE == SWP_OS_LINUX)
typedef long long INT64;
typedef unsigned long long UINT64;
#endif 

/* 函数指针类型定义 */
typedef void    (*VOID_FUNC_PTR) (); /* pointer to function returning void */

/* 获取版本名称函数指针 */
typedef char * (*GET_VERSION_NAME_FUNC_PF)();

/******************************** 全局变量声明 *******************************/
/*extern */
/******************************** 外部函数原形声明 ***************************/
/*extern */
/******************************** 头文件保护结尾 *****************************/
#ifdef  __cplusplus
}
#endif /* End "#ifdef  __cplusplus" */

#endif /* SWP_TYPE_H */
/******************************** 头文件结束 *********************************/























