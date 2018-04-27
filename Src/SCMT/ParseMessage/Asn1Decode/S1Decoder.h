#pragma once
int  S1OssInit();
void S1OssDeinit();
int  TraceS1Decode(unsigned char *BitStream, int BitStreamlen, int PduNo,char** pErrInfo);