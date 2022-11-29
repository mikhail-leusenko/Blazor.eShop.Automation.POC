using eShop.BDD.Core.Attributes;
using eShop.BDD.Core.Pages.Base;
using System;
using TechTalk.SpecFlow;

namespace eShop.BDD.Core.Helpers
{
    public static class PageNavigationHelper
    {
        /// <summary>
        /// Gets the instance of page from the PageStorage class according to the specified value.
        /// </summary>
        /// <param name="scenariContext">Storage of helper instances.</param>
        /// <param name="value">The name and type of page to get.</param>
        /// <returns>The insance of appropriate page class.</returns>
        public static BasePage GetPage(this ScenarioContext scenariContext, FeatureContext featureContext, string value)
        {
            var page = ConvertToObject(scenariContext, featureContext, value);
            try
            {
                return (BasePage)page;
            }
            catch
            {
                throw;
            }
        }

        private static object ConvertToObject(this ScenarioContext scenarioContext, FeatureContext featureContext, string value)
        {
            var storage = new PageStorage(scenarioContext, featureContext);
            value = value.Trim();
            var properties = storage.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var pageNameAttribute = (PageNameAttribute[])properties[i].GetCustomAttributes(typeof(PageNameAttribute), false);
                if (pageNameAttribute.Length == 0)
                {
                    continue;
                }

                if (string.Equals(pageNameAttribute[0].PageName.Trim(), value.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return properties[i].GetValue(storage);
                }
            }

            return null;
        }
    }
}
