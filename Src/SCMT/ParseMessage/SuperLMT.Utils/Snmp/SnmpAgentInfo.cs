//===============================================================================
// SnmpAgentInfo
//      --Snmp Agent的相关信息
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
//   2012/2/14 created by pengqiang
//===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using SnmpSharpNet;

namespace SuperLMT.Utils
{
    /// <summary>
    /// 远端代理的信息
    /// </summary>
    public class SnmpAgentInfo
    {
        /// <summary>
        /// 对端地址
        /// </summary>
        public IPAddress PeerAddress { get; set; }

        /// <summary>
        /// 对端端口
        /// </summary>
        public int Port 
        {
            get { return _nPort; }
            set { _nPort = value; } 
        }

        /// <summary>
        /// 超时时长
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 重复操作次数
        /// </summary>
        public int RetryTime { get; set; }

        /// <summary>
        /// 共同体名
        /// </summary>
        public string Community { get; set; }

        /// <summary>
        /// 协议版本
        /// </summary>
        public SnmpVersion Version { get; set; }

        /// <summary>
        /// GetBulk中的不需要重复读取的变量
        /// </summary>
        public int NonRepeaters
        {
            get { return _nonRepeaters; }
            set { _nonRepeaters = value; }
        }

        /// <summary>
        /// GetBulk中的最大重复次数
        /// </summary>
        public int MaxRepetitions
        {
            get { return _maxRepetitions; }
            set { _maxRepetitions = value; }
        }

        /// <summary>
        /// Non repeaters value used in SNMP GET-BULK requests，VB列表中不需要重复读取的变量
        /// </summary>
        private int _nonRepeaters = 0;

        /// <summary>
        /// Maximum repetitions value used in SNMP GET-BULK requests，重复取值的最大重复次数
        /// </summary>
        private int _maxRepetitions = 50;

        /// <summary>
        /// 对端接收Get/Set报文的端口
        /// </summary>
        private int _nPort = 161;
    }
}
