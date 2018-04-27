using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SuperLMT.Utils.TraceManager
{
    public class TraceDocMgr
    {
        #region 构造函数
        public TraceDocMgr()
        {
            GetTraceXmlInfo();
        }
        #endregion

        #region 公共函数

        /// <summary>
        /// 初始话xml文件
        /// </summary>
        public void GetTraceXmlInfo()
        {
            if(null == TraceDoc)
            {
                TraceDoc = new XmlDocument();
                string path = GetCommonPath();
                try
                {
                    TraceDoc.Load(path);
                }
                catch (SystemException ex)
                {
                    return ;
                }
            }
            return;    
        }


        /// <summary>
        /// 获取解析get命令相关信息
        /// </summary>
        /// <returns></returns>
        public List<TraceItem> CmdGetInfo()
        {
            List<TraceItem> returnList = new List<TraceItem>();
            if (null == TraceDoc)
            {
                return null;
            }

            XmlNodeList typeNodeList = null;
            try
            {
                typeNodeList = TraceDoc.SelectNodes(@"/TraceInfo/GetTrace/ParaNode");
            }
            catch (SystemException ex)
            {
                return null;
            }

            if (null == typeNodeList)
                return null;

            foreach (XmlNode curNode in typeNodeList)
            {
                XmlAttributeCollection attributes = curNode.Attributes;
                XmlAttribute typeNameAttr = attributes["Oid"];
                string strOid = typeNameAttr.Value;
                TraceItem newItem = new TraceItem();
                newItem.Oid = strOid;
                returnList.Add(newItem);
                
            }


            return returnList;
        }


        /// <summary>
        /// 获取解析Add命令的参数
        /// </summary>
        /// <returns></returns>
        public List<TraceItem> CmdAddInfo()
        {
            List<TraceItem> returnList = new List<TraceItem>();
            if (null == TraceDoc)
            {
                return null;
            }

            XmlNodeList typeNodeList = null;
            try
            {
                typeNodeList = TraceDoc.SelectNodes(@"/TraceInfo/AddTrace/ParaNode");
            }
            catch (SystemException ex)
            {
                return null;
            }

            if (null == typeNodeList)
                return null;

            foreach (XmlNode curNode in typeNodeList)
            {
                XmlAttributeCollection attributes = curNode.Attributes;
                XmlAttribute typeNameAttr = attributes["Oid"];
                string strOid = typeNameAttr.Value;

                XmlAttribute typeNameAttr1 = attributes["DisplayName"];
                string displayName = typeNameAttr1.Value;

                XmlAttribute typeNameAttr2 = attributes["Range"];
                string range = typeNameAttr2.Value;

                XmlAttribute typeNameAttr3 = attributes["Value"];
                string value = typeNameAttr3.Value;

                XmlAttribute typeNameAttr4 = attributes["Type"];
                string type = typeNameAttr4.Value;
                TraceItem newItem = new TraceItem(strOid,displayName,range,value,type);
                returnList.Add(newItem);

            }


            return returnList;
        }


        /// <summary>
        /// 将获取解析Del命令参数
        /// </summary>
        /// <returns></returns>
        public List<TraceItem> CmdDelInfo()
        {
            List<TraceItem> returnList = new List<TraceItem>();
            if (null == TraceDoc)
            {
                return null;
            }

            XmlNodeList typeNodeList = null;
            try
            {
                typeNodeList = TraceDoc.SelectNodes(@"/TraceInfo/DelTrace/ParaNode");
            }
            catch (SystemException ex)
            {
                return null;
            }

            if (null == typeNodeList)
                return null;

            foreach (XmlNode curNode in typeNodeList)
            {
                XmlAttributeCollection attributes = curNode.Attributes;
                XmlAttribute typeNameAttr = attributes["Oid"];
                string strOid = typeNameAttr.Value;
                TraceItem newItem = new TraceItem();
                newItem.Oid = strOid;
                returnList.Add(newItem);

            }


            return returnList;
        }

        /// <summary>
        /// 获取OID前面公共部分
        /// </summary>
        /// <returns></returns>
        public string CmdCommonOid()
        {
            string strOid = "";
            if (null == TraceDoc)
            {
                return null;
            }

            XmlNodeList typeNodeList = null;
            try
            {
                typeNodeList = TraceDoc.SelectNodes(@"/TraceInfo/CommonHead");
            }
            catch (SystemException ex)
            {
                return null;
            }

            if (null == typeNodeList)
                return null;
            foreach (XmlNode curNode in typeNodeList)
            {
                XmlAttributeCollection attributes = curNode.Attributes;
                XmlAttribute typeNameAttr = attributes["Oid"];
                strOid = typeNameAttr.Value;
            }

            return strOid;
        }

        /// <summary>
        /// 获取Oid公共头
        /// </summary>
        /// <returns></returns>
        public string GetCommonPath()
        {
            string path = AppPathUtiliy.Instance.GetAppPath() + @"\config\TraceInfo.xml"; 

            return path;
        }

        #endregion


        #region 属性
        XmlDocument _traceDoc;
        public XmlDocument TraceDoc
        {
            get
            {
            	return _traceDoc;
            }
            set
            {
                _traceDoc = value;
            }
        }

        #endregion
    }
}
