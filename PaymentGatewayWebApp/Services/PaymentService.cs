using MongoDB.Driver;
using PaymentGatewayService;
using PaymentGatewayWebApp.Models;
using System.Reflection;

namespace PaymentGatewayWebApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentsDatabaseSettings settings;
        private readonly IMongoCollection<PaymentModel> _payments;

        public PaymentService(IPaymentsDatabaseSettings settings, IMongoClient mongoClient)
        {
            this.settings = settings;

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
        public List<string> GetGatewayNames()
        {
            List<string> objects = new List<string>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(GatewayBase)).GetTypes()
                   .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(GatewayBase))))
            {
                objects.Add(type.Name);
            }
            objects.Sort();
            return objects;
        }

        public GatewayBase? GetGatewayByClassName(string Classname)
        {
            var instanceType = Type.GetType($"PaymentGatewayService.{Classname}, PaymentGatewayService");

            if (instanceType != null)
            {
                var gateway = (GatewayBase?)Activator.CreateInstance(instanceType);

                return gateway;
            }

            return null;
        }
    }
}
