// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeBodyStructNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventTreeBodyStructNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using CDLBrowser.Parser.Document.Event;

    /// <summary>
    /// The event tree body struct node.
    /// </summary>
    public class EventTreeBodyStructNode : ConfigNodeWrapper
    {
        /// <summary>
        /// The struct name.
        /// </summary>
        private readonly string structName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTreeBodyStructNode"/> class.
        /// </summary>
        /// <summary>
        /// The id.
        /// </summary>
        private readonly string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTreeBodyStructNode"/> class.
        /// </summary>
        /// <param name="structName">
        /// The struct name.
        /// </param>
        /// <param name="structId">
        /// The struct id.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        public EventTreeBodyStructNode(string structName, string structId, int index)
        {
            this.id = structId + index;
            this.structName = structName + " " + index;
        }

        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                return this.structName;
            }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public override string Id
        {
            get
            {
                return this.id;
            }
        }
    }
}
