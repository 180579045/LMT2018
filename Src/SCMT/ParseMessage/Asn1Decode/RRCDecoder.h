#pragma once
int  RRCOssInit();
void RRCOssDeinit();
int  TraceRRCDecode(unsigned char *BitStream, int BitStreamlen, int PduNo,char** pErrInfo);
