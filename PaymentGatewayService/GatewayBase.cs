using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayService.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PaymentGatewayService
{
    public abstract class GatewayBase : IGatewayBase, IGateway, IGatewayProvider
    {
        public delegate void GatewayLogEventHandler(object sender, GatewayLogEventArgs e);
        public string AccountId { get; set; } = string.Empty;
        public string TerminalId { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public BankRequest Request { get; set; } = new BankRequest();
        public BankResponse Response { get; set; } = new BankResponse();
        public List<ValidationResult> ValidationErrors { get; set; } = new List<ValidationResult>();
        public bool IsValid { get; private set; }

        public event GatewayLogEventHandler? OnLog = null;

        private void ToLog()
        {
            GatewayTransactionLog gatewayTransactionLog = new GatewayTransactionLog
            {
                AccountId = this.AccountId,
                TerminalId = this.TerminalId,
                TransactionId = this.TransactionId,
                Request = this.Request,
                Response = this.Response
            };
               
            string logText = JsonSerializer.Serialize(gatewayTransactionLog);

            if (OnLog != null)
                OnLog(this, new GatewayLogEventArgs { LogText = logText });
        }

        public virtual void MakePayment()
        {
            this.ToLog();
        }

        public virtual void CancelPayment()
        {
            this.ToLog();
        }

        public virtual void RefundPayment(decimal Amount)
        {
            this.ToLog();
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
