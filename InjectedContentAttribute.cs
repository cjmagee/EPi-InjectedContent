using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Core.CMS.Attributes
{
    public class InjectedContentAttribute : Attribute
    {
        public string RootPageName { get; set; }

        public string RootPageGuid { get; set; }
    }
}
