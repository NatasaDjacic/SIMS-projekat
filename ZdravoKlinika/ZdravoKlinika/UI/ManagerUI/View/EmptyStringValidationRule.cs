using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ZdravoKlinika.UI.ManagerUI.View
{
    
    public class EmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (s.Length > 0)
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a value.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}

