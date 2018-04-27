// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCoUse.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   common use.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///  user control.
    /// </summary>
    public class ControlCoUse
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly ControlCoUse Instance = new ControlCoUse();

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static ControlCoUse Singleton
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        ///  is 0~9.
        /// </summary>
        /// <param name="textvalue">text value.</param>
        /// <returns>true or false.</returns>
        public bool IsNumber(string textvalue)
        {
            if (string.Empty == textvalue)
            {
                return true;
            }

            if (textvalue.IndexOf('.') >= 0)
            {
                return false;
            }

            const string Pattern = @"[0-9]+";
            var regex = new Regex(Pattern);
            Match match = regex.Match(textvalue);
            if (match.Success)
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// number or comma.
        /// </summary>
        /// <param name="textvalue">text value.</param>
        /// <returns>true or false.</returns>
        public bool IsNumberOrDecimal(string textvalue)
        {
            if (string.Empty == textvalue)
            {
                return true;
            }

            const string Pattern = @"[0-9.]+";
            var regex = new Regex(Pattern);
            Match match = regex.Match(textvalue);
            if (match.Success)
            {
                return true;
            }
            
           return false;
        }

        /// <summary>
        ///  version 4.
        /// </summary>
        /// <param name="ip">address value.</param>
        /// <returns>null or address.</returns>
        public string Ipv4Check(string ip)
        {
            /*检查分段 a.b.c.d*/
            string[] addressSection = ip.Split('.');
            if (4 != addressSection.Count())
            {
                return string.Empty;
            }

            /*是否全由数字组成*/
            var sec = new int[4];
            const string Pattern = @"[0-9]+";
            for (int i = 0; i < 4; i++)
            {
                addressSection[i] = addressSection[i].Trim();
                if (string.Empty == addressSection[i])
                {
                    sec[i] = 0;
                }
                else
                {
                    var regex = new Regex(Pattern);
                    if (!regex.IsMatch(addressSection[i]))
                    {
                        return string.Empty;
                    }
                    
                    sec[i] = Convert.ToInt32(addressSection[i]);
                    /*数字范围*/
                    if (sec[i] > 255)
                    {
                        return string.Empty;
                    } 
                }
            }

            string strResultIp = string.Format("{0}.{1}.{2}.{3}", sec[0], sec[1], sec[2], sec[3]);
            return strResultIp;
        }
    }
}
