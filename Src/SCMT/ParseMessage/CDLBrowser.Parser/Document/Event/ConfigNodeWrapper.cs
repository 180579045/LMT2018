// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigNodeWrapper.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.Event
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using CDLBrowser.Parser.Configuration;
    using CDLBrowser.Parser.Document.DataLength;

    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The event node.
    /// </summary>
    public class ConfigNodeWrapper : IConfigNodeWrapper
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigNodeWrapper));

        /// <summary>
        /// The children.
        /// </summary>
        private readonly IList<IConfigNodeWrapper> children = new List<IConfigNodeWrapper>();

        /// <summary>
        /// The data length.(if(dataLength == null),represent using binding)
        /// </summary>
        private int dataLength;

        /// <summary>
        /// The is data length using binding.
        /// </summary>
        private bool isDataLengthUsingBinding = true;

        /// <summary>
        /// Gets the children.
        /// </summary>
        public IList<IConfigNodeWrapper> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public IConfigNodeWrapper Parent { get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public virtual string Id
        {
            get
            {
                if (null != this.ConfigurationNode)
                {
                    return this.ConfigurationNode.Id;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets the value description.
        /// </summary>
        public string ValueDescription
        {
            get
            {
                try
                {
                    return this.ConfigurationNode.GetValueDescription(Convert.ToString(this.Value));
                }
                catch (Exception ex)
                {
                    Log.Error("ValueDescription error,message = " + ex.Message);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the display content.
        /// </summary>
        public virtual string DisplayContent
        {
            get
            {
                return string.Format("{0} : {1}", this.ConfigurationNode.GetAttribute("Describe"), this.ValueDescription);
            }
        }

        /// <summary>
        /// Gets or sets the configuration node.
        /// </summary>
        public IConfigNode ConfigurationNode { get; set; }

        /// <summary>
        /// Gets or sets the data length.
        /// </summary>
        public int DataLength
        {
            get
            {
                return this.dataLength;
            }

            protected set
            {
                this.dataLength = value;
            }
        }

        /// <summary>
        /// The initialize value.
        /// </summary>
        /// <param name="memoryStream">
        /// The memory Stream.
        /// </param>
//         public virtual void InitializeValue(Stream memoryStream)
//         {
//             try
//             {
//                 //Debug.Assert(null != this.ConfigurationNode || this.Children.Count > 0, "event tree node ,must have a configuration node");
//                 if (this.ConfigurationNode == null || ConfigurationNode.Children.Count == 0)
//                 {
//                     Log.Error("node id = " + this.Id + ", ConfigurationNode == null.");
//                 }
//                 // not a leaf node
//                 if (this.children.Count > 0)
//                 {
//                     foreach (var child in this.Children)
//                     {
//                         if (null != child)
//                         {
//                             child.InitializeValue(memoryStream);
//                         }
//                     }
//                 }
//                 else
//                 {
//                     if (this.Id == "CellInfo")
//                     {
//                         IConfigNodeWrapper FileVer = this.Parent.GetChildNodeById("FileVer");
//                         if (FileVer != null)
//                         {
//                             string strfileVer = FileVer.Value.ToString();
// 
//                             if (strfileVer != "2")
//                             {
//                                 Log.Info("This is used for special version!");
//                                 return;
//                             }
// 
//                         }
// 
//                     }
// 
//                     if (this.isDataLengthUsingBinding)
//                     {
//                         
//                         this.dataLength = new DataLengthParser().GetDataLength(this, ref this.isDataLengthUsingBinding);
//                         if (this.dataLength < 1)
//                         {
//                             Log.Error("InitializeValue data length get error!");
//                             return;
//                         }
//                     }
// 
//                     var dataBuffer = new byte[this.dataLength];
//                     memoryStream.Read(dataBuffer, 0, this.dataLength);
//                     var configurationNode = this.ConfigurationNode;
//                     if (configurationNode != null)
//                     {
// 
//                          this.Value = ConvertUtil.Singleton.ConvertByteArrayToDataTypeObject(
//                                 configurationNode.GetAttribute("DataType"), dataBuffer);
// 
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 Log.Error(string.Format("InitializeValue erro msg ={0}", ex.Message));
//             }
//         }
//         public virtual void InitializeValue(FileStream stream)
//         {
//             InitializeValue(stream, "");
//         }
//         public virtual void InitializeValue(FileStream stream, string btsVersion)
//         {
//             Debug.Assert(null != this.ConfigurationNode, "event tree node ,must have a configuration node");
//             if (this.ConfigurationNode == null)
//             {
//                 Log.Error("event tree node ,must have a configuration node");
//                 return;
//             }
// 
//             // not a leaf node
//             if (this.children.Count > 0)
//             {
//                 foreach (var child in this.Children)
//                 {
//                     if (null != child)
//                     {
//                         child.InitializeValue(stream, btsVersion);
//                     }
//                 }
//             }
//             else
//             {
// 
//                 if (this.Id == "CellInfo")
//                 {
//                     IConfigNodeWrapper FileVer = this.Parent.GetChildNodeById("FileVer");
//                     if (FileVer != null)
//                     {
//                         string strfileVer = FileVer.Value.ToString();
//                         //如果等于2，表示电信专项版本，需要截取
//                         if (strfileVer != "2")
//                         {
//                             Log.Info("This is used for special version!");
//                             return;
//                         }
//                     }
//                 }
//                 if (this.isDataLengthUsingBinding)
//                 {
//                     this.dataLength = new DataLengthParser().GetDataLength(this, ref this.isDataLengthUsingBinding);
//                     if (this.dataLength < 1)
//                     {
//                         Log.Error("InitializeValue data length get error!");
//                         return;
//                     }
//                 }
// 
//                 var dataBuffer = new byte[this.dataLength];
//                 stream.Read(dataBuffer, 0, this.dataLength);
//                 //this.Value = ConvertUtil.Singleton.ConvertByteArrayToDataTypeObject(
//                        // this.ConfigurationNode.GetAttribute("DataType"), dataBuffer);
// 
//                 var configurationNode = this.ConfigurationNode;
//                 if (configurationNode != null)
//                 {
//                     if (BTSVersionsManager.HasVersion(btsVersion))
//                     {
//                         this.Value = ConvertUtilLittleEnd.Singleton.ConvertByteArrayToDataTypeObject(
//                             configurationNode.GetAttribute("DataType"), dataBuffer);
//                     }
//                     else
//                     {
//                         this.Value = ConvertUtil.Singleton.ConvertByteArrayToDataTypeObject(
//                             configurationNode.GetAttribute("DataType"), dataBuffer);
//                     }
//                 }
//             }
//         }


        /// <summary>
        /// The initialize value.
        /// </summary>
        /// <param name="memoryStream">
        /// The memory Stream.
        /// </param>
        public virtual void InitializeValue(Stream memoryStream, string btsVersion)
        {
            try
            {
                 Debug.Assert(null != this.ConfigurationNode || this.Children.Count > 0, "event tree node ,must have a configuration node");
                // not a leaf node
                if (this.children.Count > 0)
                {
                    foreach (var child in this.Children)
                    {
                        if (null != child)
                        {
                            child.InitializeValue(memoryStream, btsVersion);
                        }
                    }
                }
                else
                {
                    if (this.Id == "CellInfo")
                    {
                        IConfigNodeWrapper FileVer = this.Parent.GetChildNodeById("FileVer");
                        if (FileVer != null)
                        {
                            string strfileVer = FileVer.Value.ToString();

                            if (strfileVer != "2")
                            {
                                Log.Info("This is used for special version!");
                                return;
                            }

                        }

                    }

                    if (this.isDataLengthUsingBinding)
                    {
                        this.dataLength = new DataLengthParser().GetDataLength(this, ref this.isDataLengthUsingBinding);
                        if (this.dataLength < 1)
                        {
                            Log.Error("InitializeValue data length get error!");
                            return;
                        }
                    }

                    var dataBuffer = new byte[this.dataLength];
                    memoryStream.Read(dataBuffer, 0, this.dataLength);
                    var configurationNode = this.ConfigurationNode;
                    if (configurationNode != null)
                    {
                        if (BTSVersionsManager.HasVersion(btsVersion))
                        {
                            this.Value = ConvertUtilLittleEnd.Singleton.ConvertByteArrayToDataTypeObject(
                                configurationNode.GetAttribute("DataType"), dataBuffer);
                        }
                        else
                        {
                            this.Value = ConvertUtil.Singleton.ConvertByteArrayToDataTypeObject(
                                configurationNode.GetAttribute("DataType"), dataBuffer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("InitializeValue erro msg ={0}", ex.Message));
            }
        }
      
        /// <summary>
        /// The get child node by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public IConfigNodeWrapper GetChildNodeById(string id)
        {
            if (this.Id == id)
            {
                return this;
            }

            return this.children.Select(child => child.GetChildNodeById(id)).FirstOrDefault(nodeFinded => null != nodeFinded);
        }

        /// <summary>
        /// The get child node by Describe.
        /// </summary>
        /// <param name="des">
        /// The Describe.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public IConfigNodeWrapper GetChildNodeByDescribe(string des)
        {
            // 匹配
            string desstring = des + " : ";
            if (0 == this.DisplayContent.IndexOf(desstring)) 
            {
                return this;
            }

            return this.children.Select(child => child.GetChildNodeByDescribe(des)).FirstOrDefault(nodeFinded => null != nodeFinded);
        }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public virtual IConfigNodeWrapper Clone()
        {
            return new ConfigNodeWrapper { ConfigurationNode = this.ConfigurationNode, Parent = this.Parent };
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Parent = null;
            foreach (var eventTreeNode in this.Children)
            {
                eventTreeNode.Dispose();
            }
        }

        /// <summary>
        /// The get event tree node by config id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        protected IConfigNodeWrapper GetChildNodeByConfigId(string id)
        {
            return this.Children.FirstOrDefault(eventTreeNode => eventTreeNode.ConfigurationNode.Id == id);
        }
    }
}
