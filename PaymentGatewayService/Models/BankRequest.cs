using PaymentGatewayService.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentGatewayService.Models
{
    public class BankRequest
    {
        [CreditCard]
        [Required]
        public string CardNumber { get; set; } = string.Empty;
        [Required]
        public string CardHolder { get; set; } = string.Empty;
        [CreditCardExpireYear(ErrorMessage = "Invalid Year Number.")]
        public int ExpireYear { get; set; }
        [Required]
        [Range(1, 12, ErrorMessage = "Invalid Month Number.")]
        public int ExpireMonth { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Cvv must be minimum 3 length")]
        public int Cvv { get; set; }
        [Required]
        [Range(0.1, Double.MaxValue, ErrorMessage = "Amount {0} must be greater than {1}.")]
        public decimal Amount { get; set; }
        public CurrencyCodes CurrecyCode { get; set; } = CurrencyCodes.TRY;
        public CardBrands CardBrand { 
            get 
            {
                if (this.CardNumber != null)
                {
                    Regex regexVisa = new Regex("^4[0-9]{12}(?:[0-9]{3})?$");
                    Regex regexMaster = new Regex("^5[1-5][0-9]{14}$");
                    Regex regexAmex = new Regex("^3[47][0-9]{13}$");

                    if (regexVisa.IsMatch(this.CardNumber))
                        return CardBrands.VISA;
                    else if (regexMaster.IsMatch(this.CardNumber))
                        return CardBrands.MASTER;
                    else if (regexAmex.IsMatch(this.CardNumber))
                        return CardBrands.AMEX;
                }
                
                return CardBrands.Undefined;
            } 
        }
    }
}
