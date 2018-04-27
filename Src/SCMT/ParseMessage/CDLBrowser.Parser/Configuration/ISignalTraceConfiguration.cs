// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISignalTraceConfigration.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the ISignalTraceConfigration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    /// <summary>
    /// The SignalTraceConfigration interface.
    /// </summary>
    public interface ISignalTraceConfiguration
    {
        /// <summary>
        /// Gets the get packet header config node.
        /// </summary>
        IConfigNode PacketHeaderConfigNode { get; }

        /// <summary>
        /// Gets the get message header config node.
        /// </summary>
        IConfigNode MessageHeaderConfigNode { get; }

        /// <summary>
        /// Gets the get message body config node.
        /// </summary>
        IConfigNode MessageBodyConfigNode { get; }

        /// <summary>
        /// Gets the get command config node.
        /// </summary>
        IConfigNode GetCommandConfigNode { get; }

        /// <summary>
        /// Gets the get cell command config node.
        /// </summary>
        IConfigNode GetCellCommandConfigNode { get;}

        /// <summary>
        /// Gets the add command config node.
        /// </summary>
        IConfigNode AddCommandConfigNode { get; }

        /// <summary>
        /// Gets the delete command config node.
        /// </summary>
        IConfigNode DeleteCommandConfigNode { get; }
    }
}
