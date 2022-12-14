using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Data;
using System;

namespace eShop.BDD.Core.Helpers
{
    public class ConstantsHelper
    {
        /// <summary>
        /// Gets the appropriate Constant message value from Constants.cs.
        /// </summary>
        /// <param name="context">Storage of the instance of page class, which should contain required element. </param>
        /// <param name="value">The name and type of element to be found. </param>
        /// <returns>The appropriate Constant message. </returns>
        public static string GetConstantMessage(string value)
        {
            var constantObject = ConvertMessageToObject(value);

            if (constantObject is string constant)
            {
                return constant;
            }
            else
            {
                throw new InvalidCastException($"The {value} constant is not represented in Constants class. Check the cast.");
            }
        }

        /// <summary>
        /// Gets the appropriate Constant page defaulter value from Constants.cs.
        /// </summary>
        /// <param name="context">Storage of the instance of page class, which should contain required element. </param>
        /// <param name="value">The name and type of element to be found. </param>
        /// <returns>The appropriate Constant page defaulter. </returns>
        public static string GetConstantPageDefaulterValue(string value)
        {
            var constantObject = ConvertPageConstNameToObject(value);

            if (constantObject is string constant)
            {
                return constant;
            }
            else
            {
                throw new InvalidCastException($"The {value} constant is not represented in Constants class. Check the cast.");
            }
        }

        private static object ConvertPageConstNameToObject(string value)
        {
            value = value.Trim();

            var constants = new Constants.PageNames();
            var properties = constants.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var constantNameAttribute = (ConstantNameAttribute[])properties[i].GetCustomAttributes(typeof(ConstantNameAttribute), false);
                if (constantNameAttribute.Length == 0)
                {
                    continue;
                }

                if (string.Equals(constantNameAttribute[0].ConstantName, value, StringComparison.CurrentCultureIgnoreCase))
                {
                    return properties[i].GetValue(constants);
                }
            }
            return null;
        }

        private static object ConvertMessageToObject(string value)
        {
            value = value.Trim();

            var constants = new Constants.ValidationMessages();
            var properties = constants.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var constantNameAttribute = (ConstantNameAttribute[])properties[i].GetCustomAttributes(typeof(ConstantNameAttribute), false);
                if (constantNameAttribute.Length == 0)
                {
                    continue;
                }

                if (string.Equals(constantNameAttribute[0].ConstantName, value, StringComparison.CurrentCultureIgnoreCase))
                {
                    return properties[i].GetValue(constants);
                }
            }
            return null;
        }
    }
}
