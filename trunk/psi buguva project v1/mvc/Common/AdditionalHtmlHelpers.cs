using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;


namespace mvc.Common
{
    public static class AdditionalHtmlHelpers
    {
        public static string Label(this HtmlHelper helper, string target, string text)
        { 
            return String.Format("<label for='{0}'>{1}</label>", target, text); 
        }

        public static string Path(this HtmlHelper helper)
        {
            return "";
        }

        public static string ActionImageLink(this HtmlHelper helper, string imagePath, string alt, string action, object values)
        {
            string returnActionLink = helper.ActionLink("insert_place_for_img", action, values);
            string replaceWith = String.Format("<img src=\"{0}\" alt = \"{1}\"/>", imagePath, HttpUtility.HtmlEncode(alt));
            return returnActionLink.Replace("insert_place_for_img", replaceWith);
        }

    }

    
}
