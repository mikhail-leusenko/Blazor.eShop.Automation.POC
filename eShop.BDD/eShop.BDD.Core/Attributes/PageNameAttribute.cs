using System;

namespace eShop.BDD.Core.Attributes
{
    public class PageNameAttribute : Attribute
    {
        public string PageName
        {
            get;
        }

        public PageNameAttribute(string pageName)
        {
            this.PageName = pageName;
        }
    }
}
