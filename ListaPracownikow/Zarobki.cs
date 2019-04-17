using System;
using System.Globalization;
using System.Windows.Controls;
namespace laby
{
    public class Zarobki : ValidationRule
    {
        public int Minimum{get;set;}
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pom=(string)value;
            if (int.TryParse(pom, out int ile)==false)
            {
                return new ValidationResult(false, "Integer!");
            }
            if (ile >= Minimum)
            {
                return new ValidationResult(true, "");
            }
            string a = "Min " + Minimum.ToString() + "!";
            return new ValidationResult(false, a);
        }
    }
    
}