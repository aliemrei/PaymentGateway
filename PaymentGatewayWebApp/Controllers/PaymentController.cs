using Microsoft.AspNetCore.Mvc;
using PaymentGatewayWebApp.Services;
using PaymentGatewayService;
using PaymentGatewayWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PaymentGatewayWebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<HomeController> logger,
            IPaymentService paymentService)
        {
            _logger = logger;

            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            var model = new PaymentModel()
            {
                TransactionId = Guid.NewGuid().ToString(),
            };

            ViewBag.Gateways = _paymentService.GetGatewayNames()
                .Select(x => new SelectListItem(x, x))
                .ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Index(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                var gateway = _paymentService.GetGatewayByClassName(model.Gateway);

                if (gateway != null)
                {
                    try
                    {
                        var payment = _paymentService.Create(model);

                        gateway.OnLog += Gateway_OnLog;

                        gateway.TransactionId = model.TransactionId;

                        gateway.Request = model.Request;

                        gateway.MakePayment();

                        payment.Response.Add(gateway.Response);

                        _paymentService.Update(payment.Id, payment);

                        if (gateway.Response.Result)
                        {
                            return View("Success", gateway);
                        }
                        else
                        {
                            foreach (var error in gateway.Response.Errors)
                            {
                                ModelState.TryAddModelException("", error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.TryAddModelException("", ex);
                    }
                }
                else
                    ModelState.AddModelError("", $"{model.Gateway} did not find.");
            }

            ViewBag.Gateways = _paymentService.GetGatewayNames()
                    .Select(x => new SelectListItem(x, x))
                    .ToList();

            return View(model);
        }

        private void Gateway_OnLog(object sender, PaymentGatewayService.Models.GatewayLogEventArgs e)
        {

        }
    }
}
