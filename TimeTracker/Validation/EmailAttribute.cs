using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TimeTracker.Validation
{
    public class EmailAttribute : ValidationAttribute
    {
        private Regex Regex { get; set; }

        public string Pattern
        {
            get
            {
                return
                    @"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$";
            }
        }


        public EmailAttribute()
        {
            this.Regex = new Regex(this.Pattern);
        }

        public override bool IsValid(object value)
        {
            var stringValue = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (string.IsNullOrWhiteSpace(stringValue)) return true;

            var m = Regex.Match(stringValue);

            return (m.Success && (m.Index == 0) && (m.Length == stringValue.Length));
        }
    }
}