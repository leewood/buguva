using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using Castle.Components.Validator;
using MvcValidation;

// (c) 2008 Steven Sanderson
// http://blog.codeville.net/
// License: MIT (http://www.opensource.org/licenses/mit-license.php)


namespace System.Web.Mvc // Ahem...
{
    public static class ValidationScriptsHelper
    {
        /// <summary>
        /// Emits Javascript code to validate form elements
        /// </summary>
        /// <param name="modelToValidate">Your data object whose properties have [ValidateXXX] attributes, defining validation rules</param>
        /// <returns>A <script>...</script> block. Put it *after* any HTML elements you want to validate.</returns>
        public static string ClientSideValidation(this HtmlHelper html, object modelToValidate)
        {
            return ClientSideValidation(html, modelToValidate, s => s);
        }

        /// <summary>
        /// Emits Javascript code to validate form elements
        /// </summary>
        /// <param name="modelToValidate">Your data object whose properties have [ValidateXXX] attributes, defining validation rules</param>
        /// <param name="elementIDs">A function to map property names to HTML element IDs, e.g. (prop => "Prefix." + prop)</param>
        /// <returns>A <script>...</script> block. Put it *after* any HTML elements you want to validate.</returns>
        public static string ClientSideValidation(this HtmlHelper html, object modelToValidate, Func<string, string> elementIDs)
        {
            return ClientSideValidation(html, modelToValidate, elementIDs, " ", 500);
        }

        /// <summary>
        /// Emits Javascript code to validate form elements
        /// </summary>
        /// <param name="modelToValidate">Your data object whose properties have [ValidateXXX] attributes, defining validation rules</param>
        /// <param name="elementIDs">A function to map property names to HTML element IDs, e.g. (prop => "Prefix." + prop)</param>
        /// <param name="validMessage">A message to be displayed when valid data is detected</param>
        /// <param name="waitMilliseconds">How long to pause after the user stops typing before performing validation</param>
        /// <returns>A <script>...</script> block. Put it *after* any HTML elements you want to validate.</returns>
        public static string ClientSideValidation(this HtmlHelper html, object modelToValidate, Func<string, string> elementIDs, string validMessage, int waitMilliseconds)
        {
            StringBuilder results = new StringBuilder();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(modelToValidate.GetType());
            foreach (PropertyDescriptor prop in props)
            {
                LiveValidation val = new LiveValidation(prop.Name, elementIDs, validMessage, waitMilliseconds);
                IBrowserValidationGenerator generator = new LiveValidationProvider(val);
                foreach (AbstractValidationAttribute attrib in prop.Attributes.OfType<AbstractValidationAttribute>())
                {
                    IValidator v = attrib.Build();
                    v.ErrorMessage = v.ErrorMessage ?? LiveValidation.UnspecifiedMessage; // Castle Validator is annoying here - it won't let you have a null message, so we put in a fake one
                    v.ApplyBrowserValidation(null, InputElementType.Undefined, generator, null, null);
                }

                if (val.Rules.Count > 0)
                    results.AppendLine(val.ToJavascript());
            }

            return string.Format("<script type='text/javascript'><!--{1}{0}{1}--></script>", results.ToString(), Environment.NewLine);
        }

        public static string ErrorSummary(this HtmlHelper html, string caption, string last, object modelToValidate)
        {
            List<string> results = new List<string>();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(modelToValidate.GetType());
            foreach (PropertyDescriptor prop in props)
            {
                Func<string, string> elementIDs = (s => s);
                LiveValidation val = new LiveValidation(prop.Name, elementIDs, " ", 500);
                IBrowserValidationGenerator generator = new LiveValidationProvider(val);
                foreach (AbstractValidationAttribute attrib in prop.Attributes.OfType<AbstractValidationAttribute>())
                {
                    IValidator v = attrib.Build();
                    v.ErrorMessage = v.ErrorMessage ?? LiveValidation.UnspecifiedMessage; // Castle Validator is annoying here - it won't let you have a null message, so we put in a fake one
                    v.ApplyBrowserValidation(null, InputElementType.Undefined, generator, null, null);
                    
                }

                
                if (val.Rules.Count > 0)
                    foreach (ValidationRule rule in val.Rules)
                    {
                        results.Add(rule.Properties["failureMessage"].ToString());
                    }

            }
            return html.ErrorSummary(caption, results.ToArray());
        }
    }
}
