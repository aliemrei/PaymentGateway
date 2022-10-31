using PaymentGatewayService;
using PaymentGatewayWebApi.Models;

namespace PaymentGatewayWebApp.Clients
{
    public class PaymentGatewayApiClient : IPaymentGatewayApiClient
    {
        private readonly HttpClient _httpClient;

        public PaymentGatewayApiClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<PaymentModel?> Create(PaymentModel payment)
        {
            HttpResponseMessage response = _httpClient.PostAsJsonAsync("Payment", payment).Result;

            if (response.IsSuccessStatusCode)
            {
                Task<PaymentModel?> result = response.Content.ReadFromJsonAsync<PaymentModel>();

                return result.Result;
            }

            return null;
        }
        public async Task<List<PaymentModel>?> Get()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PaymentModel>>("Payment");

            return result;
        }

        public async Task<PaymentModel?> Get(string Id) 
        {
            var result = await _httpClient.GetFromJsonAsync<PaymentModel>(
                $"Payment/{Id}");

            return result;
        }

        public void Remove(string Id)
        {
            throw new NotImplementedException();
        }

        public void Update(string Id, PaymentModel payment)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentModel?> MakePayment(PaymentModel model)
        {
            HttpResponseMessage response = _httpClient.PostAsJsonAsync("Payment", model).Result;

            if (response.IsSuccessStatusCode)
            {
                Task<PaymentModel?> result = response.Content.ReadFromJsonAsync<PaymentModel>();

                return result.Result;
            }

            return null;
        }

    }
}
