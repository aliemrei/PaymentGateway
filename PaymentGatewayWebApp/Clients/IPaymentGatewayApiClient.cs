using PaymentGatewayService;
using PaymentGatewayWebApi.Models;

namespace PaymentGatewayWebApp.Clients
{
    public interface IPaymentGatewayApiClient
    {
        Task<List<PaymentModel>?> Get();
        Task<PaymentModel?> Get(string Id);
        Task<PaymentModel?> Create(PaymentModel payment);
        void Update(string Id, PaymentModel payment);
        void Remove(string Id);

        Task<PaymentModel?> MakePayment(PaymentModel model);
    }
}
