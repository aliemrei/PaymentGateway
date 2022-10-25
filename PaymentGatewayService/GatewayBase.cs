using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayService.Models;
using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayService
{
    public abstract class GatewayBase : IGatewayBase, IGatewayProvider
    {
        public string AccountId { get; set; } = string.Empty;
        public string TerminalId { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public BankRequest Request { get; set; } = new BankRequest();
        public BankResponse Response { get; set; } = new BankResponse();
        public List<ValidationResult> ValidationErrors { get; set; } = new List<ValidationResult>();
        public bool IsValid { get; private set; }

        private void ToLog()
        {
            //Log
        }

        public virtual void MakePayment()
        {
            ToLog();
        }

        public virtual void CancelPayment()
        {
            ToLog();
        }

        public virtual void RefundPayment(decimal Amount)
        {
            ToLog();
        }

        public virtual void internalMakePayment()
        {
            this.Validate();
        }

        private void Validate()
        {
            this.ValidationErrors.Clear();

            var context = new ValidationContext(this.Request);

            this.IsValid = Validator.TryValidateObject(this.Request, context, this.ValidationErrors, true);
        }

        public virtual void internalCancelPayment()
        {
            throw new NotImplementedException();
        }

        public virtual void internalRefundPayment(decimal Amount)
        {
            throw new NotImplementedException();
        }
    }
}
