// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationInfo.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the ApplicationInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The application info.
    /// </summary>
    public class ApplicationInfo
    {
        /// <summary>
        /// The product name.
        /// </summary>
        private static string productName;

        /// <summary>
        /// The product name cached.
        /// </summary>
        private static bool productNameCached;

        /// <summary>
        /// The version.
        /// </summary>
        private static string version;

        /// <summary>
        /// The version cached.
        /// </summary>
        private static bool versionCached;

        /// <summary>
        /// The company.
        /// </summary>
        private static string company;

        /// <summary>
        /// The company cached.
        /// </summary>
        private static bool companyCached;

        /// <summary>
        /// The copyright.
        /// </summary>
        private static string copyright;

        /// <summary>
        /// The copyright cached.
        /// </summary>
        private static bool copyrightCached;

        /// <summary>
        /// Gets the product name of the application.
        /// </summary>
        public static string ProductName
        {
            get
            {
                if (!productNameCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        var attribute = (AssemblyProductAttribute)Attribute.GetCustomAttribute(
                            entryAssembly, typeof(AssemblyProductAttribute));
                        productName = (attribute != null) ? attribute.Product : string.Empty;
                    }
                    else
                    {
                        productName = string.Empty;
                    }

                    productNameCached = true;
                }

                return productName;
            }
        }

        /// <summary>
        /// Gets the version number of the application.
        /// </summary>
        public static string Version
        {
            get
            {
                if (!versionCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        version = entryAssembly.GetName().Version.ToString();
                    }
                    else
                    {
                        version = string.Empty;
                    }

                    versionCached = true;
                }

                return version;
            }
        }

        /// <summary>
        /// Gets the company of the application.
        /// </summary>
        public static string Company
        {
            get
            {
                if (!companyCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        var attribute = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(
                            entryAssembly, typeof(AssemblyCompanyAttribute));
                        company = (attribute != null) ? attribute.Company : string.Empty;
                    }
                    else
                    {
                        company = string.Empty;
                    }

                    companyCached = true;
                }

                return company;
            }
        }

        /// <summary>
        /// Gets the copyright information of the application.
        /// </summary>
        public static string Copyright
        {
            get
            {
                if (!copyrightCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        var attribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                            entryAssembly, typeof(AssemblyCopyrightAttribute));
                        copyright = attribute != null ? attribute.Copyright : string.Empty;
                    }
                    else
                    {
                        copyright = string.Empty;
                    }

                    copyrightCached = true;
                }

                return copyright;
            }
        }

        /// <summary>
        /// Gets the path for the executable file that started the application, not including the executable name.
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                return AppPathUtiliy.Singleton.GetAppPath();
            }
        }
    }
}
