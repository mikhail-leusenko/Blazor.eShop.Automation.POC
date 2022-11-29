using System;

namespace eShop.BDD.Core.Attributes
{
    public class ElementNameAttribute : Attribute
    {
        public string ElementName
        {
            get;
        }

        public ElementNameAttribute(string elementName)
        {
            this.ElementName = elementName;
        }
    }
}
