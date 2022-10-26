using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Interfaces
{
    internal interface IGateway
    {
        void MakePayment();
        void CancelPayment();
        void RefundPayment(decimal Amount);
    }
}
