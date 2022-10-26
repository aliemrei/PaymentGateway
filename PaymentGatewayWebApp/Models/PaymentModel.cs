using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentGatewayService.Models;

namespace PaymentGatewayWebApp.Models
{
    public class PaymentModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        public string TransactionId { get; set; } = String.Empty;
        public string Gateway { get; set; } = string.Empty;
        public BankRequest Request { get; set; } = new BankRequest();
        public List<BankResponse> Response { get; internal set; } = new List<BankResponse>();
    }
}
