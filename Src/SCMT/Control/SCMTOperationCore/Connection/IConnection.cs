﻿using System;
using System.Net;

namespace SCMTOperationCore
{
    public enum ConnectionState
    {
        Connecting,
        Connencted,
        Disconnected,
    }

    public interface IConnection
    {
        event EventHandler Connected;
        event EventHandler DisConnected;
        void CreateConnection(BaseElement ele, int port);
        void Disconnect();
    }
}
