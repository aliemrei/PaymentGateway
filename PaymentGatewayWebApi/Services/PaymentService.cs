using Microsoft.AspNetCore.Mvc.Rendering;
using PaymentGatewayService;
using PaymentGatewayWebApi.Models;
using PaymentGatewayWebApi.Repositories;
using System.Reflection;

namespace PaymentGatewayWebApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger _logger;

        public PaymentService(IPaymentRepository paymentRepository, ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;

            _logger = logger;
        }

        public PaymentModel? MakePayment(PaymentModel model)
        {
            var gateway = GetGatewayByClassName(model.Gateway);

            if (gateway != null)
            {
                try
                {
                    var payment = _paymentRepository.Create(model);

                    gateway.TransactionId = model.TransactionId;

                    gateway.Request = model.Request;

                    gateway.MakePayment();

                    payment.Response.Add(gateway.Response);

                    _paymentRepository.Update(payment.Id, payment);

                    return payment;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                throw new Exception($"{model.Gateway} did not find.");

            return null;
        }

        public GatewayBase? GetGatewayByClassName(string Classname)
        {
            var instanceType = Type.GetType($"PaymentGatewayService.{Classname}, PaymentGatewayService");

            if (instanceType != null)
            {
                var gateway = (GatewayBase?)Activator.CreateInstance(instanceType);

                if (gateway != null)
                    gateway.OnLog += gateway_OnLog;

                return gateway;
            }

            _logger.LogCritical($"PaymentGatewayService.{Classname} did not find!");

            return null;
        }

        private void gateway_OnLog(object sender, PaymentGatewayService.Models.GatewayLogEventArgs e)
        {
            _logger.LogInformation(e.LogText);
        }
    }
}
