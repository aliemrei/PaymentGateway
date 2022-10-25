using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayService.Models;

namespace PaymentGatewayService.Interfaces
{
    internal interface IGatewayBase
    {
        public string AccountId { get; set; }
        public string TerminalId { get; set; }
        public string TransactionId { get; set; }
        BankRequest Request { get; set;  }
        BankResponse Response { get; set; }
        
    }
}
