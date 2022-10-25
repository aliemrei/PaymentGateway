using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Interfaces
{
    internal interface IGatewayBase
    {
        public string AccountId { get; set; }
        public string TerminalId { get; set; }
        public string TransactionId { get; set; }
        BankResponse Response { get; set; }
        abstract void MakePayment();
        abstract void CancelPayment();
        abstract void RefundPayment(decimal Amount);
    }
}
