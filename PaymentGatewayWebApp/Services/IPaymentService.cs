using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentGatewayService;
using PaymentGatewayWebApi.Models;

namespace PaymentGatewayWebApp.Services
{
    public interface IPaymentService
    {
        Task<List<PaymentModel>?> Get();
        Task<PaymentModel?> Get(string Id);
        Task<PaymentModel?> Create(PaymentModel payment);
        void Update(string Id, PaymentModel payment);
        void Remove(string Id);
        Task<PaymentModel?> MakePayment(PaymentModel model);
        List<SelectListItem> GatewayNamesForDropdown();
    }
}
