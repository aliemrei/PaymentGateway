using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayService.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using PaymentGatewayService.Services;

namespace PaymentGatewayService
{
    public abstract class GatewayBase : IGatewayBase, IGateway
    {
        private BankCardInformationModel? bankCardInformations = null;

        public delegate void GatewayLogEventHandler(object sender, GatewayLogEventArgs e);
        public string AccountId { get; set; } = string.Empty;
        public string TerminalId { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public BankRequest Request { get; set; } = new BankRequest();
        public BankResponse Response { get; set; } = new BankResponse();
        public List<ValidationResult> ValidationErrors { get; set; } = new List<ValidationResult>();
        public bool IsValid { get; private set; }

        public event GatewayLogEventHandler? OnLog = null;

        private void toLog(string ActionMethod)
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

            this.CheckRules();

            var context = new ValidationContext(this.Request);

            this.IsValid = Validator.TryValidateObject(this.Request, context, this.ValidationErrors, true);
        }

        private BankCardInformationModel? GetBankCardInformations(IBankCardInformationService? bankCardInformationService)
        {
            if (bankCardInformationService == null)
                bankCardInformationService = new BankCardInformationService();

            return bankCardInformationService.GetBankCardInformation(Request.CardNumber).Result;
        }

        private void CheckRules()
        {
            if (Request.Amount <= 0)
            {
                throw new Exception("The payment amount must be greater than 0.");
            }

            if (bankCardInformations == null)
                bankCardInformations = GetBankCardInformations(null);

            if (bankCardInformations != null)
            {
                if (bankCardInformations.country?.alpha2 == "TR" && Request.CurrecyCode != CurrencyCodes.TRY)
                    ValidationErrors.Add(new ValidationResult("Turkish bank card's transactions must be Turkish Lira.", new string[] { "CurCode" }) );
            }
        }

        public virtual void MakePayment()
        {
            this.toLog("MakePayment");
        }

        public virtual void CancelPayment()
        {
            this.toLog("CancelPayment");
        }

        public virtual void RefundPayment(decimal Amount)
        {
            this.toLog($"RefundPayment {Amount}");
        }
    }
}
