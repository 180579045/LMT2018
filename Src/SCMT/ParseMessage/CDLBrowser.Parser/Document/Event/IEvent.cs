// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEvent.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the IEvent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.Event
{
    /// <summary>
    /// The Event interface.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets the raw data.
        /// </summary>
        byte[] RawData { get; }

        /// <summary>
        /// Gets the event id.
        /// </summary>
        int EventIdentifier { get; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        int DisplayIndex { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        string Version { get; }
    }
}
