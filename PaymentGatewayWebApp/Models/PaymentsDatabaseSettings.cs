namespace PaymentGatewayWebApp.Models
{
    public class PaymentsDatabaseSettings : IPaymentsDatabaseSettings
    {
        public string PaymentsCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
