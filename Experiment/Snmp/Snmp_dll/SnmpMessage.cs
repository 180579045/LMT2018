/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：SnmpMessage.cs
// 文件功能描述：Snmp报文类;
// 创建人：郭亮;
// 版本：V1.0
// 创建标识：创建文件;
// 时间：2017-11-2
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snmp_dll
{
    public delegate void SnmpType();

    public class SnmpMessage
    {
        private string m_IPAddr;
        private string m_Community;
        private List<string> m_PduList;

        /// <summary>
        /// 
        /// </summary>
        public void GetRequest(List<string> PduList, string Community, SnmpType SnmpType)
        {
            return;
        }

        public void Type_GetRequest()
        {

        }


    }
}
