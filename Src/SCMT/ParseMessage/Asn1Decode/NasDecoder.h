#pragma once
int  NasCBBInit();
int  NasCBBDeinit();
int TraceNasDecode(unsigned char *BitStream, int BitStreamlen, int PduNo, char* pErrInfo);