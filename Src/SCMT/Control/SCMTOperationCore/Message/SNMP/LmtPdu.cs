using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Message.SNMP
{
    public abstract class LmtPdu
    {
        public SnmpPacket m_ReqResult;

        // 源ip
        int sourceIp { get; set; }

        // 源端口
        int sourcePort { get; set; }

        // 出错状态
        int errorStatus { get; set; }

        // 出错Vb索引
        int errorIndex { get; set; }

        // 请求类型
        int reqMsgType { get; set; }

        // 命令名称
        string cmdName { get; set; }

        // 根据索引获取Vb
        public Vb GetVbByIndex(int index)
        {
            if (index >= GetVbCount())
            {
                return null;
            }

            return m_ReqResult.Pdu.VbList[index];
        }

        // 获取vb数组数量
        public int GetVbCount()
        {
            return m_ReqResult.Pdu.VbCount;
        }

        // 添加vb
        public void addVb(Vb vb)
        {
            m_ReqResult.Pdu.VbList.Add(vb);
        }

        // 根据oid获取值
        public string GetValueByOid(string oid)
        {
            string val = null;

            foreach (Vb vb in m_ReqResult.Pdu.VbList)
            {

                if (vb.Oid.ToString().Equals(oid))
                {
                    val = vb.Value.ToString();
                    break;
                }
            }

            return val; 
        }

        // 请求id
        public int requestId { get; set; }


    }

    public class LmtPdu2c : LmtPdu
    {
       
    }

}
