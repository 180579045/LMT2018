#include "RRCDecoder.h"
#include "CommonFunciton.h"
#include "rrc.h"

OssGlobal w, *world = &w;     /* OSS��ȫ�ֻ�������������OSS API����Ҫ */
extern char prntAsnBuf[PrintBufferLen];
extern char *p;

//************************************
// Method:    RRCOssInit
// FullName:  RRCOssInit
// Access:    public 
// Returns:   int
// Qualifier:
//************************************
int RRCOssInit()
{
	// ��ʼ��״̬
	int ret = 0;

	//����ASN��RANAP��Ϣ�Ľ����ʼ������
	ret = ossinit(world, rrc);

	// ��ʼ��ʧ��
	if(ret)
	{
		return -1;
	}

	// �趨����״ֵ̬
	if (ossSetDecodingFlags(world, AUTOMATIC_ENCDEC| NOCONSTRAIN | NOTRAPPING))
	{
		//printf("OSS RRC set decode flag failure, return.\n");
		return -1;
	}

	//�趨ASN�Ĵ�ӡ����Ϊ�Զ��庯��OSSTraceFunc
	world->asn1prnt = OSSPrintFunction;
	world->asn1chop = 0;
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
void RRCOssDeinit()
{
	ossterm (world);
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
int TraceRRCDecode(unsigned char *BitStream, int BitStreamlen, int PduNo,char** pErrInfo)
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
		world,
		(int *)&iPdu,
		&stOssBuff,
		(void **)&pDecodeStr
		);

	//uiRet��Ϊ0�������ʧ��
	if(uiRet)
	{
		//����ASNϵͳ�������ؽ���ʧ����ʾ��Ϣ��pErrInfoָ���λ��
		*pErrInfo = ossGetErrMsg(world);
		//����ʾ��Ϣת�浽ȫ������
		//sprintf(prntAsnBuf, "%s\n", pErrInfo);
		return uiRet;
	}

	//����ASN��ϵͳ��ӡ����
	p = prntAsnBuf;
	ossPrintPDU(world,iPdu,pDecodeStr);

	//���������
	ossFreePDU(world,iPdu,pDecodeStr);
	return 0;
}

