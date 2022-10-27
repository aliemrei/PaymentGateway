using PaymentGatewayService;
using PaymentGatewayWebApp.Models;

namespace PaymentGatewayWebApp.Services
{
    public interface IPaymentService
    {
        GatewayBase MakePayment(PaymentModel model);
        List<PaymentModel> Get();
        PaymentModel Get(string Id);
        PaymentModel Create(PaymentModel payment);
        void Update(string Id, PaymentModel payment);
        void Remove(string Id);
        List<string> GetGatewayNames();
        GatewayBase? GetGatewayByClassName(string Classname);
    }
}
