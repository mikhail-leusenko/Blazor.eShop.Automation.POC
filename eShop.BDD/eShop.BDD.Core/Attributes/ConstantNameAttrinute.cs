using System;

namespace eShop.BDD.Core.Attributes
{
    public class ConstantNameAttribute : Attribute
    {
        public string ConstantName
        {
            get;
        }

        public ConstantNameAttribute(string constantName)
        {
            this.ConstantName = constantName;
        }
    }
}
