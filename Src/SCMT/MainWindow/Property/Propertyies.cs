using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow.Property
{
    public class Propertyies
    {
        private string property = null;
        private string value = null;
        private string name = null;
        private List<string> ls = new List<string>();
        private en enlt = en.a1;
        public enum en
        {
            a1=1,
            a2,
            a3,
            a4,
            a5
        }
        public en Enlt {
            get {
                return enlt;
            }
            set {
                this.enlt=value;
            }

        }
        public Propertyies(string name, string property, string value)
        {
            this.name = name;
            this.property = property;
            this.value = value;

        }
        public string Property
        {

            get
            {
                return this.property;
            }

            set
            {
                this.property = value;
            }
        }
        public string Value
        {

            get
            {

                return this.value;
            }

            set
            {
                this.value = value;
            }
        }


        [CategoryAttribute("基本")]
        [DisplayName("名称")]
        [ReadOnly(true)]
        [Browsable(true)]
        [Description("wwwwwwwwwwwwwww")]
        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;

            }
        }
    }
}
