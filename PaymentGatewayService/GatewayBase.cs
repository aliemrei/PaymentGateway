using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayService.Models;

namespace PaymentGatewayService
{
    public abstract class GatewayBase : IGatewayBase
    {
        public string AccountId { get; set; } = string.Empty;
        public string TerminalId { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public BankRequest Request { get; set; } = new BankRequest();
        public BankResponse Response { get; set; } = new BankResponse();

        private void ToLog()
        {
            //Log
        }

        public virtual void CancelPayment()
        {
            ToLog();
        }

        public virtual void MakePayment()
        {
            ToLog();
        }
        public virtual void RefundPayment(decimal Amount)
        {
            ToLog();
        }
    }
}
