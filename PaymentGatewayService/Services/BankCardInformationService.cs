using Newtonsoft.Json.Linq;
using PaymentGatewayService.Interfaces;
using PaymentGatewayService.Models;
using System.Net.Http.Json;

namespace PaymentGatewayService.Services
{
    public class BankCardInformationService : IBankCardInformationService
    {
        private const string _url = "http://lookup.binlist.net/";

        public async Task<BankCardInformationModel?> GetBankCardInformation(string CardNumber)
        {
            using (var _httpClient = new HttpClient())
            {
                var result = await _httpClient.GetFromJsonAsync<BankCardInformationModel>(_url + CardNumber);

                return result;
            }
        }
    }
}
