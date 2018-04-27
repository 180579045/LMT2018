#include "RRCDecoder.h"
#include "CommonFunciton.h"
#include "x2ap.h"

OssGlobal w, *world2 = &w;     /* OSS��ȫ�ֻ�������������OSS API����Ҫ */
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
	// ��ʼ��״̬
	int ret = 0;

	//����ASN��RANAP��Ϣ�Ľ����ʼ������
	ret = ossinit(world2, x2ap);

	// ��ʼ��ʧ��
	if(ret)
	{
		return -1;
	}

	// �趨����״ֵ̬
	if (ossSetDecodingFlags(world2, AUTOMATIC_ENCDEC| NOCONSTRAIN | NOTRAPPING))
	{
		//printf("OSS RRC set decode flag failure, return.\n");
		return -1;
	}

	//�趨ASN�Ĵ�ӡ����Ϊ�Զ��庯��OSSTraceFunc
	world2->asn1prnt = OSSPrintFunction;
	world2->asn1chop = 0;

	//��ʼ���ɹ�
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
	//ASN�������õĹ�������
	OssBuf              stOssBuff;
	//����Ϣ������PDU����
	INT32               iPdu = PduNo;
	//����״̬
	UINT32     uiRet;
	//��Ž���������ʱָ��
	unsigned char *pDecodeStr = NULL;
	//��Ž���ʧ��ʱ����ʾ��Ϣ
	//char *pErrInfo = NULL;

	stOssBuff.length = BitStreamlen;
	stOssBuff.value = BitStream;

	//����ASN�Ľ��뺯���������Ϣ���������pDecodeStrָ���λ��
	uiRet = ossDecode
		(
		world2,
		(int *)&iPdu,
		&stOssBuff,
		(void **)&pDecodeStr
		);

	//uiRet��Ϊ0�������ʧ��
	if(uiRet)
	{
		//����ASNϵͳ�������ؽ���ʧ����ʾ��Ϣ��pErrInfoָ���λ��
		*pErrInfo = ossGetErrMsg(world2);
		//����ʾ��Ϣת�浽ȫ������
		//sprintf(prntAsnBuf, "%s\n", pErrInfo);
		return uiRet;
	}

	//����ASN��ϵͳ��ӡ����
	p = prntAsnBuf;
	ossPrintPDU(world2,iPdu,pDecodeStr);

	//���������
	ossFreePDU(world2,iPdu,pDecodeStr);
	return 0;
}