using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Models
{
    public class GatewayLogEventArgs : EventArgs
    {
        public string LogText { get; set; } = string.Empty;
    }
}
