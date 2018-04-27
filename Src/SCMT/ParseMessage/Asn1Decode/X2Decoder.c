#include "RRCDecoder.h"
#include "CommonFunciton.h"
#include "x2ap.h"

OssGlobal w, *world2 = &w;     /* OSS的全局环境变量，所有OSS API都需要 */
extern char prntAsnBuf[PrintBufferLen];
extern char *p;

//************************************
// Method:    X2OssInit
// FullName:  X2OssInit
// Access:    public 
// Returns:   int
// Qualifier:
//************************************
int X2OssInit()
{
	// 初始化状态
	int ret = 0;

	//调用ASN的RANAP消息的解码初始化函数
	ret = ossinit(world2, x2ap);

	// 初始化失败
	if(ret)
	{
		return -1;
	}

	// 设定解码状态值
	if (ossSetDecodingFlags(world2, AUTOMATIC_ENCDEC| NOCONSTRAIN | NOTRAPPING))
	{
		//printf("OSS RRC set decode flag failure, return.\n");
		return -1;
	}

	//设定ASN的打印函数为自定义函数OSSTraceFunc
	world2->asn1prnt = OSSPrintFunction;
	world2->asn1chop = 0;

	//初始化成功
	return 0;
}

//************************************
// Method:    X2OssDeinit
// FullName:  X2OssDeinit
// Access:    public 
// Returns:   void
// Qualifier:
//************************************
void X2OssDeinit()
{
	ossterm (world2);
}

//************************************
// Method:    TraceX2Decode
// FullName:  TraceX2Decode
// Access:    public 
// Returns:   int
// Qualifier:
// Parameter: UINT8 * BitStream
// Parameter: int BitStreamlen
// Parameter: INT32 comingiPdu
//************************************
int TraceX2Decode(unsigned char *BitStream, int BitStreamlen, int PduNo,char** pErrInfo)
{
	//ASN解码所用的公共变量
	OssBuf              stOssBuff;
	//该消息所属的PDU种类
	INT32               iPdu = PduNo;
	//解码状态
	UINT32     uiRet;
	//存放解码结果的临时指针
	unsigned char *pDecodeStr = NULL;
	//存放解码失败时的提示信息
	//char *pErrInfo = NULL;

	stOssBuff.length = BitStreamlen;
	stOssBuff.value = BitStream;

	//调用ASN的解码函数解码该消息，结果存入pDecodeStr指向的位置
	uiRet = ossDecode
		(
		world2,
		(int *)&iPdu,
		&stOssBuff,
		(void **)&pDecodeStr
		);

	//uiRet不为0，则解码失败
	if(uiRet)
	{
		//调用ASN系统函数返回解码失败提示信息到pErrInfo指向的位置
		*pErrInfo = ossGetErrMsg(world2);
		//将提示信息转存到全局数组
		//sprintf(prntAsnBuf, "%s\n", pErrInfo);
		return uiRet;
	}

	//调用ASN的系统打印函数
	p = prntAsnBuf;
	ossPrintPDU(world2,iPdu,pDecodeStr);

	//清理解码结果
	ossFreePDU(world2,iPdu,pDecodeStr);
	return 0;
}