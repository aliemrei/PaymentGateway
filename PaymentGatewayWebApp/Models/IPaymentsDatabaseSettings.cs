namespace PaymentGatewayWebApp.Models
{
    public interface IPaymentsDatabaseSettings
    {
        string PaymentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
