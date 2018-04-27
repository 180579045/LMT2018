// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TraceMessageReceiver.cs" company="datang mobile">
//   datangmobile
// </copyright>
// <summary>
//   The handle message delegate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.Udp
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// The handle message delegate.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    public delegate void HandleMessageDelegate(object sender, EventArgs e);

    /// <summary>
    /// The trace listen message.
    /// </summary>
    public class TraceMessageReceiver
    {
        /// <summary>
        /// The local port.
        /// </summary>
        private const int LocalPort = 10001;

        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly TraceMessageReceiver Instance;

        /// <summary>
        /// The udp client.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private UdpClient udpClient;

        /// <summary>
        /// Initializes static members of the <see cref="TraceMessageReceiver"/> class.
        /// </summary>
        static TraceMessageReceiver()
        {
            Instance = new TraceMessageReceiver();
        }
 
        /// <summary>
        /// Prevents a default instance of the <see cref="TraceMessageReceiver"/> class from being created.
        /// </summary>
        private TraceMessageReceiver()
        {
        }

        /// <summary>
        /// The message received event.
        /// </summary>
        public event HandleMessageDelegate MessageReceivedEvent;

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static TraceMessageReceiver Singleton
        {
            get
            {
                return Instance;
            }
        }

        #region 公共函数

        /// <summary>
        /// The register message handler.
        /// </summary>
        /// <param name="messageHandler">
        /// The message handler.
        /// </param>
        public void RegisterMessageHandler(HandleMessageDelegate messageHandler)
        {
            this.MessageReceivedEvent += messageHandler;
        }

        /// <summary>
        /// The unregister message handler.
        /// </summary>
        /// <param name="messageHandler">
        /// The message handler.
        /// </param>
        public void UnregisterMessageHandler(HandleMessageDelegate messageHandler)
        {
            this.MessageReceivedEvent -= messageHandler;
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public void Initialize()
        {
            var receiveMessageThread = new Thread(this.ReceiveEventMessage);
            receiveMessageThread.Start();
        }

        /// <summary>
        /// The receive event message.
        /// </summary>
        private void ReceiveEventMessage()
        {
            this.udpClient = new UdpClient(LocalPort);
            IPEndPoint remote = null;
            while (true)
            {
                try
                {
                    // 关闭udpClient时此句会产生异常
                    byte[] bytes = this.udpClient.Receive(ref remote);

                    if (null != this.MessageReceivedEvent)
                    {
                        var e = new EventArgs();
                        this.MessageReceivedEvent(bytes, e);
                    }
                }
                catch
                {
                    break;
                }
                finally
                {
                    this.udpClient.Close();
                }
            }
        }

        #endregion
    }
}
