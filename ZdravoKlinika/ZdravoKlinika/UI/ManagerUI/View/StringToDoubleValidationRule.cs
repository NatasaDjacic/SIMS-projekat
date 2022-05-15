using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ZdravoKlinika.UI.ManagerUI.View
{
    public class StringToDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                double r;
                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a valid number.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }

    public class MinMaxValidationRule : ValidationRule
    {
        public int Min
        {
            get;
            set;
        }

        public int Max
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is int || value is string)
            {
                double d = Convert.ToDouble(value);
                if (d < Min) return new ValidationResult(false, "Value cannot be negative or 0.");
                if (d > Max) return new ValidationResult(false, "Maximum value is 1000.");
                return new ValidationResult(true, null);
            }
            else
            {
                Console.WriteLine(value.GetType());
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
