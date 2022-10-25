using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Interfaces
{
    internal interface IGatewayProvider
    {
        void internalMakePayment();
        void internalCancelPayment();
        void internalRefundPayment(decimal Amount);
    }
}
