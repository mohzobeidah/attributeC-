using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingReflection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReportItem2Attribute:Attribute
    {


        public ReportItem2Attribute(int order )
        {
            Order = order;
        }
        public int Order { get; set; }
        public string Heading { get; set; }
        public string  Format { get; set; }


    }
}
