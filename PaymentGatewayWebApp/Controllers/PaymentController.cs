using Microsoft.AspNetCore.Mvc;
using PaymentGatewayWebApp.Services;
using PaymentGatewayWebApp.Models;
using System.Diagnostics;
using PaymentGatewayWebApi.Models;
using PaymentGatewayService.Models;

namespace PaymentGatewayWebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger,
            IPaymentService paymentService)
        {
            _logger = logger;

            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            var payments = _paymentService.Get().Result;
            
            return View(payments);
        }

        [IgnoreAntiforgeryToken]
        public IActionResult New()
        {
            var model = new PaymentModel()
            {
                TransactionId = Guid.NewGuid().ToString(),
            };

            ViewBag.Gateways = _paymentService.GatewayNamesForDropdown();

            return View(model);
        }

        [HttpPost]
        public IActionResult New(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = _paymentService.MakePayment(model).Result;

                    if (payment != null)
                    {
                        var result = payment.Response.LastOrDefault();

                        if (result?.Result ?? false)
                        {
                            
                            return RedirectToAction("Success", new { id = payment.Id });
                        }
                        else if (result != null)
                        {
                            foreach (var error in ((BankResponse)result).Errors)
                            {
                                ModelState.AddModelError("", error);
                            }
                        }
                    }
                }
                catch (AggregateException ex)
                {
                    Exception e = ex.InnerException;
                    do
                        e = e.InnerException;
                    while (e.InnerException != null);

                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ModelState.AddModelError("", "Unexpected error!");

            ViewBag.Gateways = _paymentService.GatewayNamesForDropdown();

            return View(model);
        }

        public IActionResult Success(string id)
        {
            var model = _paymentService.Get(id).Result;

            return View(model);
        }
    }
}
