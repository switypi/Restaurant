using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestaurantDesk.ValidationRules
{
   public  class RequireCmbValidation: ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
                    return ValidationResult.ValidResult;
                
        }
    }
}
