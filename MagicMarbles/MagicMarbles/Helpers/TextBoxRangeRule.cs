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

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (Int32.Parse((String) value) > Min)
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
