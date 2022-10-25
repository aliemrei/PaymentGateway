using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CreditCardExpireYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value != null && (int?)value >= DateTime.Today.Year)
                return true;

            return false;
        }
         
    }
}
