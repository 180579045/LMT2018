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
#include "x2_handover_rrc_decode.h"
#include "rrc.h"

/*******************************  局部宏定义  *********************************/

/***************************  全局变量定义/初始化  ****************************/

/*****************************  本地函数原型声明  *****************************/

/********************************  函数实现  **********************************/
/*******************************************************************************
* 函数名称: RRC_HO_PrepInfoDecodeFunc  
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
void RRC_HO_PrepInfoDecodeFunc(void *pMsg, RRC_HO_PrepInfoDecode *pstruDecode)
{
    HandoverPreparationInformation        *pHandoverPreparationInfo = NULLPTR;           /*内部节点消息指针*/
    HandoverPreparationInformation_r8_IEs *phandoverPreparationInformation_r8 = NULLPTR; /*切换准备信息    */
    u32                                    u32SourceCellId;


    /*指向解码的数据    */
    pHandoverPreparationInfo = (HandoverPreparationInformation *)pMsg;
    phandoverPreparationInformation_r8 
        = &pHandoverPreparationInfo->criticalExtensions.u.c1.u.handoverPreparationInformation_r8;
    /*sourceUE_Identity*/
    pstruDecode->u16SourceCrnti = (u16)phandoverPreparationInformation_r8->as_Config.sourceUE_Identity.value[0];
    pstruDecode->u16SourceCrnti <<= 8;
    pstruDecode->u16SourceCrnti |= (u16)phandoverPreparationInformation_r8->as_Config.sourceUE_Identity.value[1];
    /*sourceDl_CarrierFreq*/
    pstruDecode->u16SourceDlEarfcn = phandoverPreparationInformation_r8->as_Config.sourceDl_CarrierFreq;
    /*sourcePhysCellId*/
    pstruDecode->u16SourcePci = phandoverPreparationInformation_r8->as_Context.reestablishmentInfo.sourcePhysCellId;
    /*中间变量*/
    u32SourceCellId = (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[0];
    u32SourceCellId <<= 8;
    u32SourceCellId |= (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[1];
    u32SourceCellId <<= 8;
    u32SourceCellId |= (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[2];
    u32SourceCellId <<= 8;
    u32SourceCellId |= (u32)phandoverPreparationInformation_r8->as_Config.sourceSystemInformationBlockType1.cellAccessRelatedInfo.cellIdentity.value[3];
    /*赋值输出*/
    pstruDecode->u8SourceCellId = (u8)(u32SourceCellId & 0xFF);
    pstruDecode->u32SourceEnbId = (u32SourceCellId >> 8) & 0xFFFFF;
    return;
}
/*******************************  源文件结束  *********************************/

