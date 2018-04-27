using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDLBrowser.Parser.Document.Event
{
    public class DataBaseIngoredAttribute:Attribute
    {
        bool ignoredFlag = false;
        public DataBaseIngoredAttribute(bool isIgnored)
        {
            ignoredFlag = isIgnored;
        }

        public bool IgnoredFlag
        {
            get
            {
                return ignoredFlag;
            }
        }
    }
}
