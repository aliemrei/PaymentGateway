using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Models
{
    public enum CurrencyCodes
    {
        TRY,
        EURO,
        USD,
        GBP
    }

    public enum CardBrands
    {
        Undefined,
        VISA,
        MASTER,
        AMEX
    }
}
