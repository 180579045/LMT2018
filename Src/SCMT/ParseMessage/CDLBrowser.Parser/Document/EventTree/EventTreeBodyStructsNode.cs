// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeBodyStructsNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventTreeBodyStructNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using System;

    using CDLBrowser.Parser.Document.Event;

    /// <summary>
    /// The event tree body struct node.
    /// </summary>
    public class EventTreeBodyStructsNode : ConfigNodeWrapper
    {
        /// <summary>
        /// The children backup.
        /// </summary>
        private IConfigNodeWrapper[] childrenBackup;

        /// <summary>
        /// The display content.
        /// </summary>
        private string displayContent;

        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                if (this.displayContent == null)
                {
                    return base.DisplayContent;
                }

                return this.displayContent;
            }
        }

        /// <summary>
        /// The initialize value.
        /// </summary>
        /// <param name="memoryStream">
        /// The memory stream.
        /// </param>
        public override void InitializeValue(System.IO.Stream memoryStream , string btsversion)
        {
            if (this.childrenBackup == null)
            {
                this.childrenBackup = new IConfigNodeWrapper[this.Children.Count];
                this.Children.CopyTo(this.childrenBackup, 0);
            }

            // 需要重新生成符合要求的树
            IConfigNodeWrapper rootNode = this;
            while (null != rootNode.Parent)
            {
                rootNode = rootNode.Parent;
            }

            string totalNumId = this.ConfigurationNode.GetAttribute("TotalNum");
            int struNum;

            if (Int32.TryParse(totalNumId,out struNum) == false)
            {
                IConfigNodeWrapper totalNumNode = rootNode.GetChildNodeById(totalNumId);
                struNum = Convert.ToInt32(totalNumNode.Value);
            }

            this.Children.Clear();

            // remove this node from parent
            if (struNum == 0)
            {
                this.displayContent = string.Empty;
            }
            else
            {
                string tempContent = this.ConfigurationNode.GetAttribute("Describe");
                for (int i = 0; i < struNum; i++)
                {
                    IConfigNodeWrapper singleStructRootNode = new EventTreeBodyStructNode(
                        tempContent, this.Id, i);

                    foreach (var childBackup in this.childrenBackup)
                    {
                        IConfigNodeWrapper childNode = childBackup.Clone();
                        singleStructRootNode.Children.Add(childNode);
                        childNode.Parent = singleStructRootNode;
                    }

                    this.Children.Add(singleStructRootNode);
                }

                base.InitializeValue(memoryStream, btsversion);
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
            var cloneNode = new EventTreeBodyStructsNode { ConfigurationNode = this.ConfigurationNode, Parent = this.Parent };
            if (null != this.Children)
            {
                foreach (var configNodeWrapper in this.Children)
                {
                    if (null != configNodeWrapper)
                    {
                        cloneNode.Children.Add(configNodeWrapper.Clone());
                    }
                }
            }

            return cloneNode;
        }
    }
}
