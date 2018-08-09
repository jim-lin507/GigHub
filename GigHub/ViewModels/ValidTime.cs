using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class ValidTime:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime time;
            CultureInfo culture = CultureInfo.GetCultureInfo("en-us");
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                culture,
                DateTimeStyles.None,
                out time);
            return isValid;
        }
    }
}