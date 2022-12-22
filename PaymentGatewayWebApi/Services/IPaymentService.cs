using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentGatewayService;
using PaymentGatewayWebApi.Models;

namespace PaymentGatewayWebApi.Services
{
    public interface IPaymentService
    {
        PaymentModel? MakePayment(PaymentModel model);
        GatewayBase? GetGatewayByClassName(string Classname);
        GatewayBase? GetGateway(string Classname);
    }
}
