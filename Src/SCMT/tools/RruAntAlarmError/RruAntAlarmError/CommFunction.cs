using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RruAntAlarmError
{
    public static class CommFunction
    {
        public static bool splitKeyValue(string excelValue, string sign, out string key, out string value)
        {
            key = "";
            value = "";
            if (-1 == excelValue.IndexOf(sign))
            {
                return false;
            }
            key = excelValue.Substring(0, excelValue.IndexOf(sign));
            value = excelValue.Substring(excelValue.IndexOf(sign) + sign.Length);

            return true;
        }

        public static bool splitKeyLanguageValue(string content, string language, out string key, out string value)
        {
            key = "";
            value = "";
            string sign = ":";
            if (-1 == content.IndexOf(sign))
            {
                return false;
            }
            key = content.Substring(0, content.IndexOf(sign));
            string languageEng;
            string languageChi;
            if (!splitKeyValue(content.Substring(content.IndexOf(sign) + sign.Length), "|", out languageEng,
                out languageChi))
            {
                return false;
            }
            if (language.Equals("Chinese"))
            {
                value = languageChi;
            }
            else
            {
                value = languageEng;
            }

            return true;
        }
    }
}
