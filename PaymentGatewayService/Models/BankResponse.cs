using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Models
{
    public sealed class BankResponse
    {
        public string TransactionId { get; set; } = string.Empty;
        public bool Result { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string AuthCode { get; set; } = string.Empty;
        public string BankMessage { get; set; } = string.Empty;
    }
}
