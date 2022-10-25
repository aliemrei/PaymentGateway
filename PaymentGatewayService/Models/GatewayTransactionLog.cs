using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Models
{
    internal sealed class GatewayTransactionLog : IGatewayBase
    {
        public string Action { get; set; } = string.Empty;
        public string AccountId { get; set; } = string.Empty;
        public string TerminalId { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public BankRequest Request { get; set; }
        public BankResponse Response { get; set; }
    }
}
