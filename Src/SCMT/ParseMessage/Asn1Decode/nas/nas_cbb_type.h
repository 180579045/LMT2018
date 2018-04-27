/******************************************************************************
* COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD
*******************************************************************************
* �ļ�����: nas_cbb_type.h
* ��    ��: ͳһ���ƽ̨�ṩ�Ļ����궨�塢�������Ͷ��塢���ݽṹ����ȡ�
* ˵    ��: 
* �޸���ʷ:
* 2008/11/06 ��С�� ��־ǿ��ʼ�汾
******************************************************************************/

/******************************** ͷ�ļ�������ͷ *****************************/
#ifndef NAS_CBB_TYPE_H
#define NAS_CBB_TYPE_H

/********************************* ͷ�ļ����� ********************************/
#ifdef  __cplusplus
extern "C" {
#endif
/******************************** ��ͳ������� *******************************/
    
/* ����ϵͳ���ͺ궨�� */
/*                             0       ���� */
#define SWP_OS_WINDOWS         1    /* Windows */
#define SWP_OS_VXWORKS         2    /* VxWorks */
#define SWP_OS_LINUX           3    /* Ƕ��ʽLinux */
#define SWP_OS_NONE            4    /* �޲���ϵͳ */
/*                         ��255       ���� */
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
/******************************** ���Ͷ��� ***********************************/
/* �����������Ͷ��� */

/*
 *VXWORKS��ϵͳ�ļ�������ͬ����
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

/* ������ϵͳ�ļ���ͻ�������ּ�����*/
/* yuexh, 2009.10.23 */
#ifndef DT_DUX_USING
#undef STATUS
#define STATUS  int
#endif

#endif

#if (SWP_OS_TYPE == SWP_OS_WINDOWS) || (SWP_OS_TYPE == SWP_OS_LINUX)
/* BITS8��BITS16��BITS32��CHAR�����ͣ����ڼ�������Ҫ����ʱ���� */

typedef   UINT8                BITS8;          /* bt */
typedef   UINT16               BITS16;         /* bt */
typedef   UINT32               BITS32;         /* bt */

typedef   char                 CHAR;        
#endif

/* WINDOWS�»����������Ͷ��� */
#if (SWP_OS_TYPE == SWP_OS_WINDOWS)
typedef __int64 INT64;
typedef unsigned __int64 UINT64;
#endif 

/* LINUX�»����������Ͷ��� */
#if (SWP_OS_TYPE == SWP_OS_LINUX)
typedef long long INT64;
typedef unsigned long long UINT64;
#endif 

/* ����ָ�����Ͷ��� */
typedef void    (*VOID_FUNC_PTR) (); /* pointer to function returning void */

/* ��ȡ�汾���ƺ���ָ�� */
typedef char * (*GET_VERSION_NAME_FUNC_PF)();

/******************************** ȫ�ֱ������� *******************************/
/*extern */
/******************************** �ⲿ����ԭ������ ***************************/
/*extern */
/******************************** ͷ�ļ�������β *****************************/
#ifdef  __cplusplus
}
#endif /* End "#ifdef  __cplusplus" */

#endif /* SWP_TYPE_H */
/******************************** ͷ�ļ����� *********************************/























