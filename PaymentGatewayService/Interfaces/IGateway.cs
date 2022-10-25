using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Interfaces
{
    internal interface IGateway
    {
        abstract void MakePayment();
        abstract void CancelPayment();
        abstract void RefundPayment(decimal Amount);
    }
}
