#pragma once

#include "Asn1Decoder.h"
#include "CommonFunciton.h"


#include "RRCDecoder.h"
#include "S1Decoder.h"
#include "X2Decoder.h"
#include "NasDecoder.h"

#include "nas_cbb_public.h"
#include "nas_cbb_tag.h"

#include "x2_handover_decode.h"
#include "x2_handover_rrc_decode.h"

extern char prntAsnBuf[PrintBufferLen];

//************************************
// Method:    RRCDecode
// FullName:  RRCDecode
// Access:    public 
// Returns:   int
// Qualifier:
// Parameter: char * BitStream
// Parameter: int BitStreamlen
// Parameter: int PduNo
// Parameter: char * OutPduString
//************************************
int RRCDecode(unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString)
{
	char* errorInfo = 0;

	if(RRCOssInit())
	{
		return -1;
	}
	//memset(prntAsnBuf,"\n",PrintBufferLen);
	if(TraceRRCDecode(BitStream,BitStreamlen,PduNo,&errorInfo))
	{
		if(0 != errorInfo)
		{
			strcpy(OutPduString,errorInfo);
		}
		RRCOssDeinit();
		return -1;
	}

	strcpy(OutPduString,prntAsnBuf);

	RRCOssDeinit();

	return 0;
}

//************************************
// Method:    S1Decode
// FullName:  S1Decode
// Access:    public 
// Returns:   int
// Qualifier:
// Parameter: char * BitStream
// Parameter: int BitStreamlen
// Parameter: int PduNo
// Parameter: char * OutPduString
//************************************
int S1Decode(unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString)
{
	char* errorInfo = 0;

	if(S1OssInit())
	{
		return -1;
	}
	//memset(prntAsnBuf,"\n",PrintBufferLen);
	if(TraceS1Decode(BitStream,BitStreamlen,PduNo,&errorInfo))
	{
		if(0 != errorInfo)
		{
			strcpy(OutPduString,errorInfo);
		}
		S1OssDeinit();
		return -1;
	}

	strcpy(OutPduString,prntAsnBuf);

	S1OssDeinit();

	return 0;
}

//************************************
// Method:    X2Decode
// FullName:  X2Decode
// Access:    public 
// Returns:   int
// Qualifier:
// Parameter: char * BitStream
// Parameter: int BitStreamlen
// Parameter: int PduNo
// Parameter: char * OutPduString
//************************************
int X2Decode( unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString)
{	
	char* errorInfo = 0;

	if(X2OssInit())
	{
		return -1;
	}
	
	//memset(prntAsnBuf,"\n",PrintBufferLen);
	if(TraceX2Decode(BitStream,BitStreamlen,PduNo,&errorInfo))
	{
		if(0 != errorInfo)
		{
			strcpy(OutPduString,errorInfo);
		}
		X2OssDeinit();
		return -1;
	}

	strcpy(OutPduString,prntAsnBuf);

	X2OssDeinit();

	return 0;
}

//************************************
// Method:    NasDecode
// FullName:  NasDecode
// Access:    public 
// Returns:   int
// Qualifier:
// Parameter: char * BitStream
// Parameter: int BitStreamlen
// Parameter: int PduNo
// Parameter: char * OutPduString
//************************************
int NasDecode(unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString)
{	
	int errorCode = 0;		
	
	NasCBBInit();
	errorCode = TraceNasDecode(BitStream, BitStreamlen, 100, OutPduString);
	NasCBBDeinit();	
	
	if (errorCode == NAS_CODEC_SUCCESS)
	{		
		return 0;
	}

	return -1;
}