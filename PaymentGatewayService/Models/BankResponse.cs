using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Models
{
    public class BankResponse
    {
        public string TransactionId { get; set; } = string.Empty;
        public bool Result { get; set; }
        public List<Exception> Errors { get; set; } = new List<Exception>();
        public string AuthCode { get; set; } = string.Empty;
        public string BankMessage { get; set; } = string.Empty;
    }
}
