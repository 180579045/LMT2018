using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LmtbSnmp;
using LogManager;
using MsgQueue;

namespace DataSync
{
    class AlterationPduParser
    {
        /*验证变更PDU的完整性，验证不过，不再进行后续的处理*/
        static bool CheckAlterationPDUConsistency(CDTLmtbPdu lmtPdu)
        {
            //PDU中的VB个数小于等于5的话,就没有绑定变更节点
            int vbCount = lmtPdu.VbCount();
            if (vbCount <= 5)
            {
                Log.Error("参数lmtPdu没有绑定变更节点！");
                return false;
            }
            return true;
        }

        static void PrintAlterationInformation(CDTLmtbPdu lmtPdu)
        {
            // 网元IP
            var strNodeBIp = lmtPdu.m_SourceIp;
            //获取变更的类型
            string alterationNotiType;
            if (!lmtPdu.GetValueByMibName(strNodeBIp, "alterationNotiType", out alterationNotiType))
            {
                Log.Error(strNodeBIp + " get alterationNotiType fail");
                return;
            }
            //alterationNotiType转换成字符串
            if (alterationNotiType.Equals("0"))
            {
                alterationNotiType = CommString.IDS_ALTERATION_TYPE_CREATION;
            }
            else if (alterationNotiType.Equals("1"))
            {
                alterationNotiType = CommString.IDS_ALTERATION_TYPE_DELETE;
            }
            else if (alterationNotiType.Equals("2"))
            {
                alterationNotiType = CommString.IDS_ALTERATION_TYPE_CHANGED;
            }
            //将各变更的节点名称与值解析

            // 输出信息
            //string strEventInfo = $"({CommString.IDS_RECEIVE}):{strEventInfo}";
            //ShowLogHelper.Show(strEventInfo, lmtPdu.m_SourceIp, InfoTypeEnum.OM_EVENT_NOTIFY_INFO);

        }

        bool GenerateAlterationMibContent(CDTLmtbPdu lmtPdu)
        {
            int vbCount = lmtPdu.VbCount();
            for (int loop = 0; loop < vbCount; loop++)
            {
                var cDTLmtbVb = lmtPdu.GetVbByIndexEx(loop);
                string oid = cDTLmtbVb.Oid;
                string vbValue = cDTLmtbVb.Value;
                var snmpSyntax = cDTLmtbVb.SnmpSyntax;
                //转换bit类型的显示
                if (snmpSyntax == SNMP_SYNTAX_TYPE.SNMP_SYNTAX_BITS)
                {
                    string strDesVal = null;
                    //SnmpMibUtil.GenerateBitsTypeDesc(vbValue, , out strDesVal);
                }
            }
            return true;
        }
    }
}
