/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ ICDTLinkPathMgr $
* 机器名称：       $ machinename $
* 命名空间：       $ LinkPath $
* 文 件 名：       $ ICDTLinkPathMgr.cs $
* 创建时间：       $ 2018.09.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     LMT-eNB与eNB的通信链路的管理器。
* 修改时间     修 改 人         修改内容：
* 2018.09.xx  XXXX            XXXXX
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPath
{
    /// <summary>
    /// LMT-eNB与eNB的通信链路的管理器
    /// </summary>
    public interface ICDTLinkPathMgr
    {
        /*启动SNMP模块*/
        bool StartSnmp(string commnuity, string destIpAddr);
    }
}
