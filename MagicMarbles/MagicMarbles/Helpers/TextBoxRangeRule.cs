using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MagicMarbles.Helpers
{
    public class TextBoxRangeRule : ValidationRule
    {

        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (int.Parse((string) value) > Min && int.Parse((string) value) < Max)
                {
                    return new ValidationResult(true, null);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return new ValidationResult(false, null);
        }
    }
}
