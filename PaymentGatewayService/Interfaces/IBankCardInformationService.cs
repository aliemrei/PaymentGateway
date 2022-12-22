using Newtonsoft.Json.Linq;
using PaymentGatewayService.Models;

namespace PaymentGatewayService.Interfaces
{
    public interface IBankCardInformationService
    {
        public Task<BankCardInformationModel?> GetBankCardInformation(string CardNumber);
    }
}
