#include "CommonFunciton.h"
#include "ossasn1.h"
#include "rrc.h"


char prntAsnBuf[PrintBufferLen];


char *pTail = prntAsnBuf + PrintBufferLen;
char *p = prntAsnBuf;

//************************************
// Method:    OSSPrintFunction
// FullName:  OSSPrintFunction
// Access:    public 
// Returns:   int
// Qualifier:
// Parameter: FILE * pFile
// Parameter: const char * format
// Parameter: ...
//************************************
int OSSPrintFunction(FILE *pFile, const char *format, ...)
{	
	va_list args;
	int nRC = 0;


	if(p == NULL)
		return nRC; 


	va_start(args, format);

	nRC = vsprintf(p, format, args);
	va_end(args);


	p = p + nRC;

	if(pTail - p < 80)
		p = NULL;


	return nRC;
}
