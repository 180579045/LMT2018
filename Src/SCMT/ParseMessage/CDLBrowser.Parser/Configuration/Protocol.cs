// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolInfo.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The protocol info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// The protocol info.
    /// </summary>
    [Serializable]
    public class Protocol : INotifyPropertyChanged
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The color.
        /// </summary>
        private string color;

        /// <summary>
        /// The id.
        /// </summary>
        private string id;

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [XmlAttribute("Name")]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.NotifyPropertyChange("Name");
            }
        }

        /// <summary>
        /// Gets or sets the Color.
        /// </summary>
        [XmlAttribute("Color")]
        public string Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
                this.NotifyPropertyChange("Color");
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [XmlAttribute("ID")]
        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.NotifyPropertyChange("Id");
            }
        }

        /// <summary>
        /// The notify property change.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        protected void NotifyPropertyChange(string propertyName)
        {
            if (null != this.PropertyChanged)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
