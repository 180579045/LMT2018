#pragma once
int  X2OssInit();
void X2OssDeinit();
int TraceX2Decode(unsigned char *BitStream, int BitStreamlen, int PduNo,char** pErrInfo);