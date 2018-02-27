// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicMessage.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The message info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DT.Tools.FlowChart
{
    using System.Windows;

    /// <summary>
    /// The message info.
    /// </summary>
    public class BasicMessage
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        public string TimeStamp { get; set; }

        public ulong TimeTicks { set; get; }
        /// <summary>
        /// Gets or sets the oid.
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        /// Gets or sets the parser id.
        /// </summary>
        public int ParserId { get; set; }

        /// <summary>
        /// Gets or sets the source element.
        /// </summary>
        public string SourceElement { get; set; }

        /// <summary>
        /// Gets or sets the destination element.
        /// </summary>
        public string DestinationElement { get; set; }

        /// <summary>
        /// Gets or sets the message start point.
        /// </summary>
        public Point MessageStartPoint { get; set; }

        /// <summary>
        /// Gets or sets the message stop point.
        /// </summary>
        public Point MessageStopPoint { get; set; }
    }
}