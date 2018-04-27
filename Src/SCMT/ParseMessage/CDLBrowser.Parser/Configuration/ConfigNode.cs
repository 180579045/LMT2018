// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigNode.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The abstract node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Xml;

    using Common.Logging;

    /// <summary>
    /// The node.
    /// </summary>
    public class ConfigNode : IConfigNode
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigNode));

        /// <summary>
        /// The attributes.
        /// </summary>
        private readonly IDictionary<string, string> attributes = new Dictionary<string, string>();

        /// <summary>
        /// The descriptions.
        /// </summary>
        private readonly IDictionary<string, string> descriptions = new Dictionary<string, string>();

        /// <summary>
        /// The children.
        /// </summary>
        private readonly IList<IConfigNode> children = new List<IConfigNode>();

        /// <summary>
        /// The id.
        /// </summary>
        private string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigNode"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public ConfigNode(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id
        {
            get
            {
                try
                {
                    if (this.id == null)
                    {
                        string idAddribute;
                        this.id = this.attributes.TryGetValue("ID", out idAddribute) ? idAddribute : string.Empty;
                    }

                    return this.id;
                }
                catch (System.Exception ex)
                {
                    Log.Error("没有ID字段！Name = " + this.Name + "  Error Message :" + ex.Message);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public IList<IConfigNode> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public IConfigNode Parent { get; set; }

        /// <summary>
        /// The get attribute.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAttribute(string name)
        {
            string attribute;
            if (this.attributes.TryGetValue(name, out attribute))
            {
                return attribute;
            }

            return null;
        }

        /// <summary>
        /// The get value description.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetValueDescription(string value)
        {
            if (this.descriptions.Count == 0)
            {
                if (0 == string.Compare("Enum", this.GetAttribute("DisplayType"), System.StringComparison.OrdinalIgnoreCase))
                {
                    string valueRange = this.GetAttribute("ValueRange");
                    Debug.Assert(!string.IsNullOrEmpty(valueRange), "enum type,but value range is empty!");

                    string[] enumValues = valueRange.Split('|');
                    foreach (var enumValue in enumValues)
                    {
                        var position = enumValue.IndexOf('~');
                        var index = enumValue.Substring(0, position);
                        var content = enumValue.Substring(position + 1);
                        this.descriptions.Add(index, content);
                    }
                }

                if (0 == string.Compare("IPv4", this.GetAttribute("DisplayType"), System.StringComparison.OrdinalIgnoreCase))
                {
                    return IPAddress.Parse(value).ToString();
                }

            }

            if (this.descriptions.ContainsKey(value))
            {
                return this.descriptions[value];
            }

            return value;
        }

        /// <summary>
        /// The initialize attributes.
        /// </summary>
        /// <param name="xmlNode">
        /// The xml node.
        /// </param>
        /// <returns>
        /// The <see cref="xmlNode"/>.
        /// </returns>
        public ConfigNode InitializeAttributes(XmlNode xmlNode)
        {
            if (xmlNode.Attributes != null)
            {
                foreach (var attribute in xmlNode.Attributes)
                {
                    var attri = (XmlAttribute)attribute;
                    this.attributes.Add(attri.Name, attri.Value);
                }
            }

            return this;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Parent = null;
            foreach (var child in this.Children)
            {
                child.Dispose();
            }

            this.Children.Clear();
        }
    }
}
