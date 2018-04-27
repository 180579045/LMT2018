// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialNode.cs" company="">
//   TODO: Update copyright text.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace CDLBrowser.Parser.ASN1.Implement
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The special node.
    /// </summary>
    public class SpecialNode
    {
        /// <summary>
        /// Gets or sets the decode name.
        /// </summary>
        public string DecodeName { get; set; }

        /// <summary>
        /// Gets or sets the decode port.
        /// </summary>
        public string DecodePort { get; set; }

        /// <summary>
        /// Gets or sets the decode pdu.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public int DecodePdu { get; set; }

        /// <summary>
        /// Gets or sets the decode dll.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string DecodeDll { get; set; }
      
    }
}