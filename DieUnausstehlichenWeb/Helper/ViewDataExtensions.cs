using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DieUnausstehlichenWeb.Helper
{
    public static class ViewDataExtensions
    {
        public static ViewDataDictionary NoSidePadding(this ViewDataDictionary dictionary)
        {
            dictionary["NoSidePadding"] = true;
            return dictionary;
        }
    }
}