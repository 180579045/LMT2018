#include "RRCDecoder.h"
#include "CommonFunciton.h"
#include "s1ap.h"

OssGlobal w, *world1 = &w;     /* OSS��ȫ�ֻ�������������OSS API����Ҫ */
extern char prntAsnBuf[PrintBufferLen];
extern char *p;

//************************************
// Method:    RRCOssInit
// FullName:  RRCOssInit
// Access:    public 
// Returns:   int
// Qualifier:
//************************************
int S1OssInit()
{
	// ��ʼ��״̬
	int ret = 0;

	//����ASN��RANAP��Ϣ�Ľ����ʼ������
	ret = ossinit(world1, s1ap);

	// ��ʼ��ʧ��
	if(ret)
	{
		return -1;
	}

	// �趨����״ֵ̬
	if (ossSetDecodingFlags(world1, AUTOMATIC_ENCDEC| NOCONSTRAIN | NOTRAPPING))
	{
		//printf("OSS RRC set decode flag failure, return.\n");
		return -1;
	}

	//�趨ASN�Ĵ�ӡ����Ϊ�Զ��庯��OSSTraceFunc
	world1->asn1prnt = OSSPrintFunction;
	world1->asn1chop = 0;
	//��ʼ���ɹ�
	return 0;
}

//************************************
// Method:    RRCOssDeinit
// FullName:  RRCOssDeinit
// Access:    public 
// Returns:   void
// Qualifier:
//************************************
void S1OssDeinit()
{
	ossterm (world1);
}

//************************************
// Method:    TraceRRCDecode
// FullName:  TraceRRCDecode
// Access:    public 
// Returns:   int
// Qualifier:
// Parameter: UINT8 * BitStream
// Parameter: int BitStreamlen
// Parameter: INT32 comingiPdu
//************************************
int TraceS1Decode(unsigned char *BitStream, int BitStreamlen, int PduNo, char** pErrInfo)
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
		world1,
		(int *)&iPdu,
		&stOssBuff,
		(void **)&pDecodeStr
		);

	//uiRet��Ϊ0�������ʧ��
	if(uiRet)
	{
		//����ASNϵͳ�������ؽ���ʧ����ʾ��Ϣ��pErrInfoָ���λ��
		*pErrInfo = ossGetErrMsg(world1);
		//����ʾ��Ϣת�浽ȫ������
		//sprintf(prntAsnBuf, "%s\n", pErrInfo);
		return uiRet;
	}

	//����ASN��ϵͳ��ӡ����
	p = prntAsnBuf;
	ossPrintPDU(world1,iPdu,pDecodeStr);

	//���������
	ossFreePDU(world1,iPdu,pDecodeStr);
	return 0;
}