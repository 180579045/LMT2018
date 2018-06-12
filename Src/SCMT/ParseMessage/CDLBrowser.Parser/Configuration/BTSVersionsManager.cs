using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SuperLMT.Utils;
using System.Xml;
using Common.Logging;
using System.Collections;

namespace CDLBrowser.Parser.Configuration
{
    public class BTSVersionsManager
    {

        private static Hashtable _versions;
        private static ILog _log = Common.Logging.LogManager.GetLogger(typeof(BTSVersionsManager));
        static BTSVersionsManager()
        {
            InitVersionList();
        }

        public static bool HasVersion(string version)
        {
            if (_versions != null && _versions[version] != null)
            {
                return true;
            }
            return false;
        }
        private static void InitVersionList()
        {

            string xmlpath = string.Format("{0}\\Configuration\\Files\\SMCVersions.xml", AppPathUtiliy.Singleton.GetAppPath());
            try
            {
                _versions = new Hashtable();
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlpath);
                XmlNodeList typeNodeList = xmlDoc.SelectNodes("//SmcVersionList//Smc");
               if (typeNodeList == null)
               {
                   _log.Info("SMCVersions.xml 为空。");
                   return;
               }
                
                foreach (XmlNode curNode in typeNodeList)
                {
                    XmlAttributeCollection attributes = curNode.Attributes;
                    if (attributes == null || attributes["Version"] == null)
                    {
                        continue;
                    }

                    string version = attributes["Version"].Value;
                    _versions[version] = version;
                } 
            }
            catch (Exception exp)
            {
               _log.Error(exp.Message);
            }

        }

    }
}
