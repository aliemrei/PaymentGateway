using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentGatewayService;
using PaymentGatewayWebApi.Models;
using PaymentGatewayWebApp.Clients;
using System.Reflection;

namespace PaymentGatewayWebApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentGatewayApiClient _paymentGatewayApiClient;

        public PaymentService(IPaymentGatewayApiClient paymentGatewayApiClient)
        {
            this._paymentGatewayApiClient = paymentGatewayApiClient;
        }

        public async Task<PaymentModel?> Create(PaymentModel payment)
        {
            return await _paymentGatewayApiClient.Create(payment);
        }

        public async Task<List<PaymentModel>?> Get()
        {
            return await _paymentGatewayApiClient.Get();
        }

        public async Task<PaymentModel?> Get(string Id)
        {
            return await _paymentGatewayApiClient.Get(Id);
        }

        public Task<PaymentModel?> MakePayment(PaymentModel model)
        {
            return _paymentGatewayApiClient.MakePayment(model);
        }

        public void Remove(string Id)
        {
            _paymentGatewayApiClient.Remove(Id);
        }

        public void Update(string Id, PaymentModel payment)
        {
            _paymentGatewayApiClient.Update(Id, payment);
        }

        private List<string> getGatewayNames()
        {
            List<string> objects = new List<string>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(GatewayBase)).GetTypes()
                   .Where(_type => _type.IsClass && !_type.IsAbstract && _type.IsSubclassOf(typeof(GatewayBase))))
            {
                objects.Add(type.Name);
            }
            objects.Sort();
            return objects;
        }

        public List<SelectListItem> GatewayNamesForDropdown()
        {
            return getGatewayNames()
                  .Select(x => new SelectListItem(x, x))
                  .ToList();
        }
    }
}
