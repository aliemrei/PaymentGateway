
using PaymentGatewayWebApi.Models;

namespace PaymentGatewayWebApi.Repositories
{
    public interface IPaymentRepository
    {
        List<PaymentModel> Get();
        PaymentModel Get(string Id);
        PaymentModel Create(PaymentModel payment);
        void Update(string Id, PaymentModel payment);
        void Remove(string Id);
    }
}
