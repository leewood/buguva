using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Common
{
    public class RoadHelper
    {
        public string img(string img_name)
        {
            return "<img src=\"../../Content/Images/Icons/" + img_name + "_big.png\" alt=\"logo\" />";
        }

        public string link(string caption, string controller, string action)
        {
            if (controller != "")
                controller += "/";

            if (action != "")
                action += "/";
            
            return "<a href=\"/" + controller + action + "\">" + caption + "</a><span class=\"span\"> > </span>";
        }

    }
}
