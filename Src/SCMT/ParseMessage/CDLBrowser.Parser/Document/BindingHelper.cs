// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingHelper.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the BindingHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document
{
    using System.Linq;
    using System.Text.RegularExpressions;

    using CDLBrowser.Parser.Document.Event;

    using Common.Logging;

    /// <summary>
    /// The binding helper.
    /// </summary>
    public class BindingHelper
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BindingHelper));

        /// <summary>
        /// The get binding node.
        /// </summary>
        /// <param name="currentNode">
        /// The current node.
        /// </param>
        /// <param name="bindingAttribute">
        /// The binding name.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public static IConfigNodeWrapper GetBindingNode(IConfigNodeWrapper currentNode, string bindingAttribute)
        {
            IConfigNodeWrapper rootNode = currentNode;
            while (rootNode.Parent != null)
            {
                rootNode = rootNode.Parent;
            }

            var bindingRegex = new Regex(@"{Binding (\w+)\s*}", RegexOptions.IgnoreCase);
            Match m = bindingRegex.Match(bindingAttribute);
            if (m.Success)
            {
                string bindingNodeId = m.Groups[1].Value;
                IConfigNodeWrapper bindingTreeNode = GetBindingNodeFromDescendant(rootNode, bindingNodeId);
                if (bindingTreeNode == null)
                {
                    Log.Error(
                        string.Format(
                            "没有找到DataLength的绑定节点,Id = {0},{1}",
                            currentNode.ConfigurationNode.Id,
                            bindingAttribute));
                    return null;
                }

                return bindingTreeNode;
            }
            else
            {
                if (bindingAttribute.Contains("*") && bindingAttribute.Contains("Binding"))
                {
                    int lenxx = "Binding".Length;
                    string bindingNodeId = bindingAttribute.Substring(lenxx + 1, bindingAttribute.IndexOf("*") - lenxx -1).Trim();
                    IConfigNodeWrapper bindingTreeNode = GetBindingNodeFromDescendant(rootNode, bindingNodeId);
                    if (bindingTreeNode == null)
                    {
                        Log.Error(
                            string.Format(
                                "没有找到DataLength的绑定节点,Id = {0},{1}",
                                currentNode.ConfigurationNode.Id,
                                bindingAttribute));
                        return null;
                    }

                    return bindingTreeNode;
                }
            }

            return null;
        }

        /// <summary>
        /// The get binding node from descendant.
        /// </summary>
        /// <param name="currentNode">
        /// The current Node.
        /// </param>
        /// <param name="bindingNodeId">
        /// The binding node id.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        private static IConfigNodeWrapper GetBindingNodeFromDescendant(IConfigNodeWrapper currentNode, string bindingNodeId)
        {
            if (currentNode == null)
            {
                return null;
            }

            if (null != currentNode.ConfigurationNode)
            {
                if (currentNode.ConfigurationNode.Id == bindingNodeId)
                {
                    return currentNode;
                }
            }

            return currentNode.Children.Select(child => GetBindingNodeFromDescendant(child, bindingNodeId)).FirstOrDefault(bindingNode => null != bindingNode);
        }
    }
}
