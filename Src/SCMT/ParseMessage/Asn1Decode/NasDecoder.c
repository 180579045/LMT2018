#include <stdio.h>
#include <string.h>
#include <WinSock.h>

#include "CommonFunciton.h"
#include "nas_cbb_public.h" 


//UINT8 g_aucMspsPrintBuf[PrintBufferLen];		/* Used for print Nas Message  */

VOID test_log()
{
}

VOID oams_shcmd_printf()
{

}

VOID oams_shcmd_reg()
{

}

int  NasCBBInit()
{
	UINT32 ulRt = 1; 

	NAS_REFERENCE_APIS_T stApis = {0};
	/* 内存操作相关API */
	stApis.pfuncMemMalloc = malloc;
	stApis.pfuncMemFree = free;
	stApis.pfuncMemSet = memset;
	stApis.pfuncMemCopy = memcpy;
	/* 日志打印相关API */
	stApis.pfuncLog = test_log;
	stApis.pfuncSprintf = sprintf;
	stApis.pfuncVsprintf = vsprintf;
	/* 字节序转换相关API */
	stApis.pfuncHtonU16 = htons;
	stApis.pfuncNtohU16 = ntohs;
	stApis.pfuncHtonU32 = htonl;
	stApis.pfuncNtohU32 = ntohl;
	/* SHELL命令相关API */
	//stApis.pfuncShPrintf = oams_shcmd_printf;
	//stApis.pfuncShReg = oams_shcmd_reg;

	/* 绑定API集 */
	ulRt = nas_api_bind(&stApis);

	if (ulRt == 0)
	{
		return 1;
	}
	
	return -1;	
}

void NasCBBDeinit()
{
	UINT32 ulRt = 1;
	
	ulRt =nas_api_unbind();
}

int TraceNasDecode(unsigned char *BitStream, int BitStreamlen, int PduNo, char* pErrInfo)
{  	
	UINT8 ucMsgType = 0;

	UINT32	uiRet = 0;
	UINT8 aucBuffTemp[10000] = {0};
	void* pucOutMsg = malloc(PrintBufferLen);
	
	uiRet = nas_decode((UINT8*)BitStream,  BitStreamlen, pucOutMsg, PrintBufferLen, &ucMsgType);
	if (0 != uiRet)
	{
		free(pucOutMsg);
		return -1;
	}
		
	nas_message_decode_print(ucMsgType, (UINT8*)pucOutMsg, aucBuffTemp, sizeof(aucBuffTemp));

	memcpy(pErrInfo,aucBuffTemp,sizeof(aucBuffTemp));
	free(pucOutMsg);

	return 0;
}

