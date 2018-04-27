/*******************************************************************************
*  COPYRIGHT DaTang Mobile Communications Equipment CO.,LTD                    
********************************************************************************
* 文件名称:                                                
* 功能描述: 
* 使用说明:    
* 文件作者:                                                      
* 编写日期:                                           
* 修改历史: 
* 修改日期    修改人  BugID/CRID      修改内容
* ------------------------------------------------------------------------------
* 
*******************************************************************************/

/*******************************  头文件包含  *********************************/
#include "cdl_common.h"
#include "x2_handover_decode.h"
#include "x2ap.h"

/*******************************  局部宏定义  *********************************/

/***************************  全局变量定义/初始化  ****************************/

/*****************************  本地函数原型声明  *****************************/

/********************************  函数实现  **********************************/
/*******************************************************************************
* 函数名称: X2AP_CauseDecode
* 函数功能: 解析Cause 信息
* 相关文档: <eNB AP软件详细设计>
* 函数参数:
*   参数名              类型            输入/输出     描述
* pCause            X2AP_Cause *    输入          协议中的Cause结构头指针
* pu8CauseType          u8 *                 输出     Cause类型
* pu8CauseValue         u8 *                 输出     Cause值
* 返回值:   无
* 函数类型:
* 函数说明:
* 修改日期    版本号   修改人  修改内容
* ------------------------------------------------------------------------------
*******************************************************************************/
void X2AP_CauseDecode(const X2AP_Cause *pCause, u8 *pu8CauseType, u8 *pu8CauseValue)
{
    if (pCause->choice == X2AP_radioNetwork_chosen)
    {
        *pu8CauseType = X2AP_radioNetwork_chosen;
        *pu8CauseValue = (u8)(pCause->u.radioNetwork);
    }
    else if (pCause->choice == X2AP_transport_chosen)
    {
        *pu8CauseType = X2AP_transport_chosen;
        *pu8CauseValue = (u8)pCause->u.transport;
    }   
    else if (pCause->choice == X2AP_protocol_chosen)
    {
        *pu8CauseType = X2AP_protocol_chosen;
        *pu8CauseValue = (u8)pCause->u.protocol;
    }
    else if (pCause->choice == X2AP_misc_chosen)
    {
        *pu8CauseType = X2AP_misc_chosen;
        *pu8CauseValue = (u8)pCause->u.misc;
    }
    else
    {
        *pu8CauseType = X2AP_misc_chosen;
        *pu8CauseValue = (u8)X2AP_CauseMisc_unspecified;
    }

    return;
}

/*******************************************************************************
* 函数名称: X2AP_DecodeTargetCellId
* 函数功能: 解析Target Cell ID信息        
* 相关文档: <eNB AP软件详细设计>
* 函数参数:
* 参数名称:   类型      输入/输出     描述
* pstruTargetCellId    X2AP_ECGI *     输入    解码前Target Cell ID数据指针
* pstruPlmnId          HL_PlmnId *     输出    PDU解码后PLMN
* pu32TargetCellId     u32 *           输出    PDU解码后Cell Identity
* 返回值: 
*        SUCCESS                           成功
*        AP_MACRO_MSG_IE_NOT_UNDERSTOOD    不能识别的IE
* 函数类型:
* 函数说明:
* 1.被本函数改变的全局变量列表
* 2.被本函数改变的静态变量列表
* 3.引用但没有被改变的全局变量列表
* 4.引用但没有被改变的静态变量列表
* 修改日期    版本号   修改人  修改内容
* -----------------------------------------------------------------
*******************************************************************************/
void X2AP_DecodeTargetCellId(X2AP_ECGI *pstruTargetCellId, u32 *pu32TargetCellId)
{
    u8     u8CellId[X2AP_MACRO_MAX_EUTRAN_CELL_ID_BYTE_LEN];
    u32    u32TargetCellId;

    if (X2AP_MACRO_MAX_EUTRAN_CELL_ID_BIT_LEN != pstruTargetCellId->eUTRANcellIdentifier.length)
    {
        u32TargetCellId = 0xffffffff;
        *pu32TargetCellId = u32TargetCellId;
        return;
    }

    (void)memcpy(u8CellId, pstruTargetCellId->eUTRANcellIdentifier.value,
                                     X2AP_MACRO_MAX_EUTRAN_CELL_ID_BYTE_LEN);

    u32TargetCellId = (u8CellId[0] << 24) & 0xFF000000;/*作为32-25位*/
    u32TargetCellId |= (u8CellId[1] << 16) & 0xFF0000;/*作为24-17位*/
    u32TargetCellId |= (u8CellId[2] << 8) & 0xFF00;/*作为16-9位*/
    u32TargetCellId |= u8CellId[3];/*作为8-1位*/
    u32TargetCellId = (u32TargetCellId >> 4) & 0x0FFFFFFF;/*右移4位*/
    *pu32TargetCellId = u32TargetCellId;

    return;
}

/*******************************************************************************
* 函数名称: X2AP_HO_PrepReqDecodeFunc  
* 函数功能: 解析来自eNB 的切换请求消息
* 相关文档: <eNB AP软件详细设计>
* 函数参数:
* 参数名称:   类型      输入/输出     描述
* pMsg        void *                      输入     请求消息指针
* pstruDecode    X2AP_HandoverReqDecode *    输出     解码消息结构
* 返回值:   
*        SUCCESS                           成功
*        AP_MACRO_MSG_IE_WRONG_ORDER       IE不按照顺序出现或出现次数过多
*        AP_MACRO_MSG_IE_NOT_UNDERSTOOD    不能识别的IE
*        AP_MACRO_MSG_IE_MISSING           缺少 IE
* 函数类型:
* 函数说明:
* 1.被本函数改变的全局变量列表
* 2.被本函数改变的静态变量列表
* 3.引用但没有被改变的全局变量列表
* 4.引用但没有被改变的静态变量列表
* 修改日期    版本号   修改人  修改内容
* -----------------------------------------------------------------
*******************************************************************************/
void X2AP_HO_PrepReqDecodeFunc(void *pMsg, X2AP_HO_PrepReqDecode *pstruDecode)
{
    X2AP_X2AP_PDU             *pstruDecodePDU = NULLPTR;   /*解码后消息头指针*/
    X2AP_InitiatingMessage    *pstruInitMsg = NULLPTR;     /*初始化消息*/    
    X2AP_HandoverRequest      *pstruHandoverReq = NULLPTR; /*切换请求数据类型指针*/
    X2AP_Cause                *pstruCause = NULLPTR;       /*Cause*/
    X2AP_ECGI                 *pstruTargetCellId = NULLPTR;/*Target Cell ID*/
    X2AP_UE_ContextInformation          *pstruContextInfo = NULLPTR; /*UE ContextInformation*/
    struct X2AP_ProtocolIE_Container    *pIE = NULLPTR;              /*ProtocolIE_Container*/
    u32                        u32TargetCellId;


    pstruDecode->u16OldX2apUeId = 0xFFFF;

    /*变量初始化*/
    pstruDecodePDU = (X2AP_X2AP_PDU *)pMsg;    
    pstruInitMsg = &(pstruDecodePDU->u.initiatingMessage);

    /*初始化消息*/
    pstruHandoverReq = (X2AP_HandoverRequest *)(pstruInitMsg->value.decoded);
    pIE = pstruHandoverReq->protocolIEs;

    while (NULLPTR != pIE)
    {
        switch (pIE->value.id)  /*根据IE-id识别各个IE*/
        {
            case  X2AP_ID_Old_eNB_UE_X2AP_ID: /*1-M, id-Old-eNB-UE-X2AP-ID*/              
                /*X2AP_ID_Old_eNB_UE_X2AP_ID*/
                pstruDecode->u16OldX2apUeId = *(X2AP_UE_X2AP_ID *)(pIE->value.value.decoded);
                break;

            case  X2AP_ID_Cause: /*2-M, id-Cause*/              
                /*cause*/
                pstruCause = (X2AP_Cause *)(pIE->value.value.decoded);
                X2AP_CauseDecode(pstruCause, &(pstruDecode->u8CauseType), &(pstruDecode->u8CauseValue));
                break;

            case  X2AP_ID_TargetCell_ID: /*3-M, X2AP_ID_TargetCell_ID*/              
                pstruTargetCellId = (X2AP_ECGI *)(pIE->value.value.decoded);
                X2AP_DecodeTargetCellId(pstruTargetCellId, &u32TargetCellId);
                /*赋值输出*/
                pstruDecode->u8TargetCellId = (u8)(u32TargetCellId & 0xFF);
                pstruDecode->u32TargetEnbId = (u32TargetCellId >> 8) & 0xFFFFF;
                break;

            case  X2AP_ID_UE_ContextInformation:/*5-M, UE-ContextInformation*/                  
                /*解析UE_ContextInformation信息*/
                pstruContextInfo = (X2AP_UE_ContextInformation *)pIE->value.value.decoded;
                /*MME S1AP ID*/
                pstruDecode->u32SourceMmeUeId = (u32)(pstruContextInfo->mME_UE_S1AP_ID);
                /*RRC Containter*/
                pstruDecode->u16NofRrcContext = (u16)pstruContextInfo->rRC_Context.length;
                (void)memcpy(pstruDecode->u8RrcContext, pstruContextInfo->rRC_Context.value, 
                    pstruDecode->u16NofRrcContext);
                break;

            default:/*对象集内没有列出的IE*/
                break;
            }/*end of switch (pIE->value.id)*/
        pIE = pIE->next;  /*获取对象集内的下一个IE的指针*/
    }/*end of while (NULLPTR != pIE)*/
    return;
}
/*******************************  源文件结束  *********************************/

