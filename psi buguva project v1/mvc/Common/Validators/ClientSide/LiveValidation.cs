using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Components.Validator;
using System.Web.Script.Serialization;

namespace MvcValidation
{
    internal enum Validate
    {
        Presence, Format, Email, Numericality, Confirmation, Length, Custom
    }

    /// <summary>
    /// This is a C# wrapper around the LiveValidation JS library, so you can build up
    /// a validation config then render it as Javascript
    /// --
    /// It supports (most of) the Castle Validator API, i.e. IBrowserValidationGenerator
    /// Note that the two validation frameworks don't mesh perfectly; there are some awkward bits.
    /// If I develop this further, I'll get rid of Castle Validator altogether and make a
    /// server-side equivalent for Live Validator - it has a much nicer API
    /// --
    /// (c) 2008 Steven Sanderson
    /// http://blog.codeville.net/
    /// License: MIT (http://www.opensource.org/licenses/mit-license.php)
    /// </summary>
    internal class LiveValidation
    {
        public const string UnspecifiedMessage = "__unspecifiedMessage";

        public LiveValidation(string propertyName, Func<string, string> propertyNamesToElementIDs, string validMessage, int waitMilliseconds)
        {
            PropertyName = propertyName;
            PropertyNamesToElementIDs = propertyNamesToElementIDs;
            ValidMessage = validMessage;
            WaitMilliseconds = waitMilliseconds;
        }

        public readonly string PropertyName;
        public readonly Func<string, string> PropertyNamesToElementIDs;
        public readonly string ValidMessage;
        public readonly int WaitMilliseconds;
        public readonly IList<ValidationRule> Rules = new List<ValidationRule>();

        public void Add(ValidationRule rule)
        {
            Rules.Add(rule);
        }

        // Slightly hacky, but we want to ignore exceptions thrown just because the HTML form doesn't have a field corresponding to a certain model property
        private const string IgnoreExceptionContaining = "LiveValidation::initialize - No element with";
        private const string IgnoreMissingElementsBlock1 = "try {";
        private const string IgnoreMissingElementsBlock2 = "} catch(ex) { if(ex.message.indexOf('" + IgnoreExceptionContaining + "') < 0) throw ex; }";

        public string ToJavascript()
        {
            StringBuilder sb = new StringBuilder();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string elem = string.Format("document.getElementById(\"{0}\") || document.getElementById(\"{1}\") || \"{0}\"", PropertyNamesToElementIDs(PropertyName), PropertyNamesToElementIDs(PropertyName).ToLower());
            sb.AppendFormat("var __val = new LiveValidation({2}, {0} validMessage : \"{3}\", wait: {4} {1});", "{", "}", elem, ValidMessage, WaitMilliseconds);

            foreach (var rule in Rules)
            {
                sb.AppendFormat("__val.add(Validate.{0}", rule.Validate.ToString());
                if (rule.Properties.Count > 0)
                {
                    sb.Append(", ");
                    serializer.Serialize(rule.Properties, sb);
                }
                sb.Append(");");
            }
            return IgnoreMissingElementsBlock1 + sb.ToString() + IgnoreMissingElementsBlock2;
        }
    }

    internal class ValidationRule
    {
        public ValidationRule(Validate validate) : this(validate, null, null) { }
        public ValidationRule(Validate validate, string failureMessage) : this(validate, failureMessage, null) { }
        public ValidationRule(Validate validate, string failureMessage, IDictionary<string, object> properties)
        {
            this.Validate = validate;
            this.Properties = properties ?? new Dictionary<string, object>();
            if ((!string.IsNullOrEmpty(failureMessage)) && (failureMessage != LiveValidation.UnspecifiedMessage))
                this.Properties.Add("failureMessage", failureMessage);
        }

        public readonly Validate Validate;
        public readonly IDictionary<string, object> Properties;
    }

    internal class LiveValidationProvider : IBrowserValidationGenerator
    {
        private LiveValidation liveValidation;
        public LiveValidationProvider(LiveValidation liveValidation)
        {
            this.liveValidation = liveValidation;
        }

        public void SetAsNotSameAs(string target, string comparisonFieldName, string violationMessage)
        {
            // Unfortunately not supported by LiveValidation
            throw new NotImplementedException();
        }

        public void SetAsRequired(string target, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Presence, violationMessage));
        }

        public void SetAsSameAs(string target, string comparisonFieldName, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Confirmation, violationMessage,
                new Dictionary<string, object> { { "match", liveValidation.PropertyNamesToElementIDs(comparisonFieldName) } }
            ));
        }

        public void SetDate(string target, string violationMessage)
        {
            // Unfortunately not supported by LiveValidation
            // Could write a custom validator, though
            throw new NotImplementedException();
        }

        public void SetDigitsOnly(string target, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Numericality, violationMessage));
        }

        public void SetEmail(string target, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Email, violationMessage));
        }

        public void SetExactLength(string target, int length, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, violationMessage,
                new Dictionary<string, object> { { "is", length } }
            ));
        }

        public void SetExactLength(string target, int length)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, null,
                new Dictionary<string, object> { { "is", length } }
            ));
        }

        public void SetLengthRange(string target, int minLength, int maxLength, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, violationMessage,
                new Dictionary<string, object> { { "minimum", minLength }, { "maximum", maxLength } }
            ));
        }

        public void SetLengthRange(string target, int minLength, int maxLength)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, null,
                new Dictionary<string, object> { { "minimum", minLength }, { "maximum", maxLength } }
            ));
        }

        public void SetMaxLength(string target, int maxLength, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, violationMessage,
                new Dictionary<string, object> { { "maximum", maxLength } }
            ));
        }

        public void SetMaxLength(string target, int maxLength)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, null,
                new Dictionary<string, object> { { "maximum", maxLength } }
            ));
        }

        public void SetMinLength(string target, int minLength, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, violationMessage,
                new Dictionary<string, object> { { "minimum", minLength } }
            ));
        }

        public void SetMinLength(string target, int minLength)
        {
            liveValidation.Add(new ValidationRule(Validate.Length, null,
                new Dictionary<string, object> { { "minimum", minLength } }
            ));
        }

        public void SetNumberOnly(string target, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Numericality, violationMessage,
                new Dictionary<string, object> { { "onlyInteger", true } }
            ));
        }

        public void SetRegExp(string target, string regExp, string violationMessage)
        {
            // Haven't bothered to implement this properly yet because
            // you have to do special JSON formatting
            throw new NotImplementedException();
        }

        public void SetValueRange(string target, string minValue, string maxValue, string violationMessage)
        {
            string fn = string.Format("function(value, args) { return (value >= args.minimum) && (value <= args.maximum); }");
            var args = new Dictionary<string, object> { { "minimum", minValue }, { "maximum", maxValue } };
            liveValidation.Add(new ValidationRule(Validate.Custom, violationMessage,
                new Dictionary<string, object> { { "against", fn }, { "args", args } }
            ));
        }

        public void SetValueRange(string target, DateTime minValue, DateTime maxValue, string violationMessage)
        {
            // Unfortunately not supported by LiveValidation
            throw new NotImplementedException();
        }

        public void SetValueRange(string target, decimal minValue, decimal maxValue, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Numericality, violationMessage,
                new Dictionary<string, object> { { "minimum", minValue }, { "maximum", maxValue } }
            ));
        }

        public void SetValueRange(string target, int minValue, int maxValue, string violationMessage)
        {
            liveValidation.Add(new ValidationRule(Validate.Numericality, violationMessage,
                new Dictionary<string, object> { { "minimum", minValue }, { "maximum", maxValue }, { "onlyInteger", true } }
            ));
        }
    }

}
