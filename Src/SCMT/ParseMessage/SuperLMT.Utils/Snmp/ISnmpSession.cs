//===============================================================================
// ISnmpSession
//      --Snmp会话类接口
//        
//===============================================================================
// Copyright ? Datang Mobile Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// Author:  pengqiang
// History: 
//   2012/2/15 created by pengqiang
//===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SnmpSharpNet;

namespace SuperLMT.Utils
{
    public interface ISnmpSession
    {
        /// <summary>
        /// 同步读取对象实例值
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="oidList"></param>
        /// <returns></returns>
        IEnumerable<Vb> Get(SnmpAgentInfo peer, IEnumerable<Vb> vbs);

        /// <summary>
        /// 同步读取给定对象的下一个可用实例值
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="vbs"></param>
        /// <returns></returns>
        IEnumerable<Vb> GetNext(SnmpAgentInfo peer, Vb[] vbs);

        /// <summary>
        /// 同步读取表中若干行
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="vbs"></param>
        /// <returns></returns>
        IEnumerable<Vb> GetBulk(SnmpAgentInfo peer, Vb[] vbs);

        /// <summary>
        /// 同步设置操作
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="vbs"></param>
        /// <returns></returns>
        IEnumerable<Vb> Set(SnmpAgentInfo peer, IEnumerable<Vb> vbs);
    }
}
