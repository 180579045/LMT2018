// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeBodyNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventTreeBodyNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using CDLBrowser.Parser.Document;
    using CDLBrowser.Parser.Document.Event;

    using Common.Logging;

    /// <summary>
    /// The event tree body node.
    /// </summary>
    public class EventTreeBodyNode : ConfigNodeWrapper
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(EventTreeBodyNode));

        /// <summary>
        /// Gets the display content.（Description + direction）
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                string description = this.ConfigurationNode.GetAttribute("Describe");
                string messageDirection = this.ConfigurationNode.GetAttribute("MsgDirection");
                if (messageDirection.IndexOf("Binding", System.StringComparison.Ordinal) > 0)
                {
                    IConfigNodeWrapper bindingDirectionNode = BindingHelper.GetBindingNode(this, messageDirection);
                    if (bindingDirectionNode == null)
                    {
                        Log.Error("cant find the binding node,attribute is " + messageDirection);
                        messageDirection = "unknown direction";
                    }
                    else
                    {
                        messageDirection = bindingDirectionNode.ValueDescription;
                    }
                }

                return string.Format("{0} ({1})", description, messageDirection);
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
            return new EventTreeBodyNode { ConfigurationNode = this.ConfigurationNode, Parent = this.Parent };
        }
    }
}
