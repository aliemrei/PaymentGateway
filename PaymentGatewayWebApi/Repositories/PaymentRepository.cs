using MongoDB.Driver;
using PaymentGatewayWebApi.Models;

namespace PaymentGatewayWebApi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<PaymentModel> _payments;

        public PaymentRepository(IMongoClient mongoClient, IPaymentsDatabaseSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _payments = database.GetCollection<PaymentModel>(settings.PaymentsCollectionName);
        }

        public PaymentModel Create(PaymentModel payment)
        {
            _payments.InsertOne(payment);

            return payment;
        }

        public List<PaymentModel> Get()
        {
            return _payments.Find(payment => true).ToList();
        }

        public PaymentModel Get(string Id)
        {
            return _payments.Find(payment => payment.Id == Id).FirstOrDefault();

        }

        public void Remove(string Id)
        {
            _payments.DeleteOne(payment => payment.Id == Id);
        }

        public void Update(string Id, PaymentModel payment)
        {
            _payments.ReplaceOne(payment => payment.Id == Id, payment);
        }
    }
}
