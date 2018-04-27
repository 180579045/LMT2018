/*******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
********************************************************************************
* 文件名称: 
* 功能描述: 
* 其它说明:
* 编写日期: 
* 修改历史:
* 修改日期    修改人  BugID/CRID      修改内容
* ------------------------------------------------------------------------------
* 
*******************************************************************************/

/******************************** 头文件保护开头 ******************************/
#ifndef CDL_COMMON_H
#define CDL_COMMON_H

/******************************** 包含文件声明 ********************************/

/******************************** 基本类型定义 ************************************/
typedef signed   char                       s8;                                 /* 有符号8位整数别名定义              */
typedef signed   short                      s16;                                /* 有符号16位整数别名定义             */
typedef signed   int                        s32;                                /* 有符号32位整数别名定义             */
typedef unsigned char                       u8;                                 /* 无符号8位整数别名定义              */
typedef unsigned short                      u16;                                /* 无符号16位整数别名定义             */
typedef unsigned int                        u32;                                /* 无符号32位整数别名定义             */

#ifdef OSP_SUPPORT_VXWORKS                                                               
typedef unsigned long long int              u64;                                /* 无符号64位整数别名定义             */
typedef signed long long int                s64;                                /* 有符号64位整数别名定义             */
#else /* VXWORKS */
#ifdef OSP_SUPPORT_WINDOWS                                                    
typedef unsigned __int64                    u64;                                /* 无符号64位整数别名定义             */
typedef __int64                             s64;                                /* 有符号64位整数别名定义             */
#endif /* WINDOWS */
#endif
#ifdef OSP_SUPPORT_LINUX                                                               
typedef unsigned long long int              u64;                                /* 无符号64位整数别名定义             */
typedef signed long long int                s64;                                /* 有符号64位整数别名定义             */
#endif
#ifdef OSP_SUPPORT_RTOS                                                               
typedef unsigned long long int              u64;                                /* 无符号64位整数别名定义             */
typedef signed long long int                s64;                                /* 有符号64位整数别名定义             */
#endif
#ifdef OSP_SUPPORT_PHAROS
typedef unsigned long long int              u64;                                /* 无符号64位整数别名定义             */
typedef signed long long int                s64;                                /* 有符号64位整数别名定义             */
#endif

/******************************** 宏和常量定义 ********************************/
#ifndef NULLPTR
#define NULLPTR         0
#endif

/********************************  类型定义  **********************************/

/******************************  全局变量声明  ********************************/

/****************************  外部函数原型声明  ******************************/

/******************************** 头文件保护结尾 ******************************/
#endif
/******************************  头文件结束  **********************************/

