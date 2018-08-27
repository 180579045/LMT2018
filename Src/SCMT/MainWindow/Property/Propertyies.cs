using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow.Property
{
    public class Propertyies
    {
        private string property = null;
        private string value = null;
        public Propertyies(string property, string value)
        {

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
    }
}
