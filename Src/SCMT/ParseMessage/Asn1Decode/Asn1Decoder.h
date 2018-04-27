#pragma once

#include "x2_handover_rrc_decode.h"
#include "x2_handover_decode.h"


#ifdef DECODER_DLL_EXPORT
#ifdef __cplusplus
#define DLL_EXPORT extern "C" __declspec (dllexport)
#else
#define DLL_EXPORT __declspec (dllexport)
#endif
#else  
#ifdef __cplusplus
#define DLL_EXPORT extern "C" __declspec (dllimport)
#else
#define DLL_EXPORT __declspec (dllimport)
#endif
#endif 

//½âÎö³ö×Ö·û´®--------------->
DLL_EXPORT int RRCDecode(unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString);
DLL_EXPORT int S1Decode(unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString);
DLL_EXPORT int X2Decode(unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString);
DLL_EXPORT int NasDecode(unsigned char *BitStream, int BitStreamlen, int PduNo,char* OutPduString);
//½âÎö³ö×Ö·û´®<---------------
