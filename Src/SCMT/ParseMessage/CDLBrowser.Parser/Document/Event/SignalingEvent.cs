// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalingEvent.cs" company="dtmobile">
//  dtmobile
// </copyright>
// <summary>
//   Defines the SignalingEvent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.Event
{
    using DevExpress.Xpo;

    /// <summary>
    /// The signaling message.
    /// </summary>
    public class SignalingEvent //yanjiewa: XPObject
    {
        /// <summary>
        /// The message data.
        /// </summary>
        private byte[] messageData;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalingEvent"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        //public SignalingEvent(Session session)
        //    : base(session)
        //{ yanjiewa
        //}

        /// <summary>
        /// Gets or sets a value indicating whether is marked.
        /// </summary>
        public string LogName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is marked.
        /// </summary>
        public bool IsMarked { get; set; }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the trace ne global id.
        /// </summary>
        public uint TrcNEGlobalId { get; set; }

        /// <summary>
        /// Gets or sets the remote ip address.
        /// </summary>
        public string RemoteIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the task id.
        /// </summary>
        public uint TaskId { get; set; }

        /// <summary>
        /// Gets or sets the task set id.
        /// </summary>
        public uint TaskSetId { get; set; }

        /// <summary>
        /// Gets or sets the trace type.跟踪类型，对应TraceInterfaceProtocols
        /// </summary>
        public string TraceType { get; set; }

        /// <summary>
        /// Gets or sets the sequence id.
        /// </summary>
        public uint SequenceId { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the trace message interface type. 用于ASN解码的类型
        /// </summary>
        public uint TrcMsgInterfaceType { get; set; }

        /// <summary>
        /// Gets or sets the trace message  direction.
        /// </summary>
        public string TrcMsgDirect { get; set; }

        /// <summary>
        /// Gets or sets the cell id.
        /// </summary>
        public uint CellId { get; set; }

        /// <summary>
        /// Gets or sets the sctp index.
        /// </summary>
        public ushort SctpIndex { get; set; }

        /// <summary>
        /// Gets or sets the message name.
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// Gets or sets the source net.
        /// </summary>
        public string SourceNet { get; set; }

        /// <summary>
        /// Gets or sets the destination net.
        /// </summary>
        public string DestinationNet { get; set; }

        /// <summary>
        /// Gets or sets the message data.
        /// </summary>
        [Delayed]
        public byte[] MessageData
        {
            set; get;
            //get
            //{
            //    if (null != this.messageData)
            //    {
            //        return this.messageData;
            //    }

            //    return this.GetDelayedPropertyValue<byte[]>("MessageData");
            //}

            //set
            //{
            //    this.SetPropertyValue("MessageData", ref this.messageData, value);
            //}
        }
    }
}
