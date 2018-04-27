// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigNodeWrapper.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the IEventTreeNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.Event
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using CDLBrowser.Parser.Configuration;

    /// <summary>
    /// The EventTreeNode interface.
    /// </summary>
    public interface IConfigNodeWrapper : IDisposable
    {
        /// <summary>
        /// Gets the children.
        /// </summary>
        IList<IConfigNodeWrapper> Children { get; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        IConfigNodeWrapper Parent { get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Gets the value description.
        /// </summary>
        string ValueDescription { get; }

        /// <summary>
        /// Gets the display content.
        /// </summary>
        string DisplayContent { get; }

        /// <summary>
        /// Gets or sets the configuration node.
        /// </summary>
        IConfigNode ConfigurationNode { get; set; }

        /// <summary>
        /// Gets the data length.
        /// </summary>
        int DataLength { get; }

        /// <summary>
        /// The initialize value.
        /// </summary>
        /// <param name="memoryStream">
        /// The memory stream.
        /// </param>
        //void InitializeValue(Stream memoryStream);
        void InitializeValue(Stream memoryStream, string btsVersion);
        /// <summary>
        /// The initialize value.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
      //  void InitializeValue(FileStream stream);

        /// <summary>
        /// The get child node by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        IConfigNodeWrapper GetChildNodeById(string id);

        /// <summary>
        /// The get child node by describe.
        /// </summary>
        /// <param name="des">
        /// The Describe.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        IConfigNodeWrapper GetChildNodeByDescribe(string des);

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        IConfigNodeWrapper Clone();
    }
}
