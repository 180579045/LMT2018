// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the INode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Node interface.
    /// </summary>
    public interface IConfigNode : IDisposable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        IList<IConfigNode> Children { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        IConfigNode Parent { get; }

        /// <summary>
        /// The get attribute.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetAttribute(string name);

        /// <summary>
        /// The get value description.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetValueDescription(string value);
    }
}
