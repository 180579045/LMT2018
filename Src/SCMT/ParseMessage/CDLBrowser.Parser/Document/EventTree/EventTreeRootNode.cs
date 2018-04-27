// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeRootNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventTreeRootNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using CDLBrowser.Parser.Document.Event;

    /// <summary>
    /// The event tree root node.
    /// </summary>
    public class EventTreeRootNode : ConfigNodeWrapper
    {
        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                return string.Format(
                    "Event {0} ({1} Bytes)", this.EventIndex, this.GetChildNodeByConfigId("DataLength").Value);
            }
        }

        /// <summary>
        /// Gets or sets the event index.
        /// </summary>
        public int EventIndex { get; set; }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public override IConfigNodeWrapper Clone()
        {
            return new EventTreeRootNode
                {
                    EventIndex = this.EventIndex, ConfigurationNode = this.ConfigurationNode, Parent = this.Parent 
                };
        }
    }
}
