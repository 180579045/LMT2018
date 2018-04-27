using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SuperLMT.Utils
{
    /// <summary>
    /// 增加数据的有效性判断
    /// </summary>
    public class ParameterCheck
    {
        #region 单例
        private static readonly ParameterCheck g_instance;
        public static ParameterCheck Instance
        {
            get { return g_instance; }
        }
        private ParameterCheck() { }

        static ParameterCheck()
        {
            g_instance = new ParameterCheck();
        }
        #endregion

        #region 公共函数

        /// <summary>
        /// IP信息匹配
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns>该IP符合就返回IP（删除前0，空格等），不符合就返回空</returns>
        public string IPV4Check(string strIP)
        {
            //检查分段
            string[] IPSection = strIP.Split('.');//a.b.c.d
            if (4 != IPSection.Count())
            {
                return "";
            }
            //是否全由数字组成
            int[] nSec = new int[4];
            string strPattern = @"[0-9]+";
            for (int i = 0; i < 4; i++)
            {
                IPSection[i] = IPSection[i].Trim();
                if ("" == IPSection[i])
                {
                    nSec[i] = 0;
                }
                else
                {
                    Regex regex = new Regex(strPattern);
                    if (!regex.IsMatch(IPSection[i]))
                    {
                        return "";
                    }
                    else
                    {
                        nSec[i] = Convert.ToInt32(IPSection[i]);
                        //数字范围
                        if (nSec[i] > 255)
                        {
                            return "";
                        }
                        
                    }
                }
            }//for
            string strResultIP = string.Format("{0}.{1}.{2}.{3}", nSec[0], nSec[1], nSec[2], nSec[3]);
            return strResultIP;
        }

        /// <summary>
        /// XML读取信息
        /// </summary>
        /// <param name="strXml"></param>
        /// <param name="strName"></param>
        /// <returns>以@分隔的信息串</returns>
        public IList<string> ReadXml(string strXml, string strName)
        {
            IList<string> infoList = new List<String>();
            //
            return infoList;
        }


        #endregion
    }
}
