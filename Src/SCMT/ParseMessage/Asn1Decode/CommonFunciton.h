#pragma once
#include "ossasn1.h"




#define PrintBufferLen 204800

//************************************
// Method:    OSSPrintFunction
// FullName:  OSSPrintFunction
// Access:    public 
// Returns:   extern int
// Qualifier:
// Parameter: FILE * pFile
// Parameter: const char * format
// Parameter: ...
//************************************
extern int OSSPrintFunction(FILE *pFile, const char *format, ...);

