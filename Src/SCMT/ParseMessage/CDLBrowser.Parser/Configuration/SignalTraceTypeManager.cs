// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalTraceTypeManager.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the SignalTraceProtocolInfoManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System.Windows.Media;

    /// <summary>
    /// The signal trace protocol info manager.
    /// </summary>
    public class SignalTraceTypeManager
    {
        /// <summary>
        /// The manager.
        /// </summary>
        private static SignalTraceTypeManager manager;

        /// <summary>
        /// The protocols info impl.
        /// </summary>
        private readonly ProtocolsInfo protocolsInfoImpl;

        /// <summary>
        /// Prevents a default instance of the <see cref="SignalTraceTypeManager"/> class from being created. 
        /// </summary>
        private SignalTraceTypeManager()
        {
            this.protocolsInfoImpl = new ProtocolsInfo(@"/Document/Resources/TraceInterfaceProtocols/Protocol", true);
        }

        /// <summary>
        /// The get singleton.
        /// </summary>
        /// <returns>
        /// The <see cref="OffLineProtocolInfoManager"/>.
        /// </returns>
        public static SignalTraceTypeManager GetSingleton()
        {
            return manager ?? (manager = new SignalTraceTypeManager());
        }

        /// <summary>
        /// The get protocol name.
        /// </summary>
        /// <param name="traceType">
        /// The protocol id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTraceTypeName(uint traceType)
        {
            return this.protocolsInfoImpl.GetProtocolName(traceType);
        }

        /// <summary>
        /// The get protocol color.
        /// </summary>
        /// <param name="traceTypeName">
        /// The protocol name.
        /// </param>
        /// <returns>
        /// The <see cref="Color"/>.
        /// </returns>
        public string GetTraceTypeColor(string traceTypeName)
        {
            return this.protocolsInfoImpl.GetProtocolColor(traceTypeName);
        }
    }
}
