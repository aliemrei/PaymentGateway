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
    public abstract class GatewayBase : IGatewayBase, IGateway
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

        private void ToLog(string ActionMethod)
        {
            if (OnLog != null)
            {
                GatewayTransactionLog gatewayTransactionLog = new GatewayTransactionLog
                {
                    Action = ActionMethod,
                    AccountId = this.AccountId,
                    TerminalId = this.TerminalId,
                    TransactionId = this.TransactionId,
                    Request = this.Request,
                    Response = this.Response
                };

                string logText = JsonSerializer.Serialize(gatewayTransactionLog);

                OnLog(this, new GatewayLogEventArgs { LogText = logText });
            }
        }

        protected void Validate()
        {
            this.ValidationErrors.Clear();

            var context = new ValidationContext(this.Request);

            this.IsValid = Validator.TryValidateObject(this.Request, context, this.ValidationErrors, true);
        }

        public virtual void MakePayment()
        {
            this.ToLog("MakePayment");
        }

        public virtual void CancelPayment()
        {
            this.ToLog("CancelPayment");
        }

        public virtual void RefundPayment(decimal Amount)
        {
            this.ToLog($"RefundPayment {Amount}");
        }

        public static GatewayBase? GetGatewayByClassName(string Classname)
        {
            var instanceType = Type.GetType($"PaymentGatewayService.{Classname}");

            if (instanceType != null)
            {
                return (GatewayBase?)Activator.CreateInstance(instanceType);
            }

            return null;
        }
    }
}
