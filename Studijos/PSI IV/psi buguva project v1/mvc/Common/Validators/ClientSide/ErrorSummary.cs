using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using mvc.Common;

namespace System.Web.Mvc // Honest!
{
    public static class ErrorSummaryHelper
    {
        public static string ErrorSummary(this HtmlHelper html, string caption, TempDataDictionary dict)
        {
            string[] errors = (string[])dict.getAndRemove("errors");
            return ErrorSummary(html, caption, errors);
        }

        public static string ErrorSummary(this HtmlHelper html, string caption, string[] errors)
        {
            if (errors != null)
            {
                StringBuilder result = new StringBuilder();
                using (HtmlTextWriter writer = new HtmlTextWriter(new StringWriter(result)))
                {
                    writer.WriteLine(caption);
                    writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                    foreach (string msg in errors)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        writer.Write(msg);
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                }
                return result.ToString();
            }
            else
                return null;
        }
    }
}
