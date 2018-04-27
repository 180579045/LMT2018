// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeAsnParentNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The event tree asn parent node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using CDLBrowser.Parser.Document.Event;

    /// <summary>
    /// The event tree asn parent node.
    /// </summary>
    public class EventTreeAsnParentNode : ConfigNodeWrapper
    {
        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                return "Data Asn.1 Decoder";
            }
        }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public override IConfigNodeWrapper Clone()
        {
            return new EventTreeAsnParentNode { ConfigurationNode = this.ConfigurationNode, Parent = this.Parent };
        }
    }
}
