// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeBitSectionNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventTreeBitSectionNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using System;

    using CDLBrowser.Parser.Document.Event;

    /// <summary>
    /// The event tree bit section node.
    /// </summary>
    public class EventTreeBitSectionNode : ConfigNodeWrapper
    {
        /// <summary>
        /// The section value.
        /// </summary>
        private readonly uint sectionValue;

        /// <summary>
        /// The section name.
        /// </summary>
        private readonly string sectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTreeBitSectionNode"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="startPosition">
        /// The start position.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        public EventTreeBitSectionNode(string name, int startPosition, int length, uint parentValue)
        {
            this.sectionName = name;
            var mask = (int)(Math.Pow(2, length) - 1);
            this.sectionValue = (uint)((parentValue >> startPosition) & mask);
        }

        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                return string.Format("{0} : {1}", this.sectionName, this.sectionValue);
            }
        }
    }
}
