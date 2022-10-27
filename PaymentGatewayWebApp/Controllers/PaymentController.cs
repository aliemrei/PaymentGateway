using Microsoft.AspNetCore.Mvc;
using PaymentGatewayWebApp.Services;
using PaymentGatewayWebApp.Models;
using System.Diagnostics;

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
            var model = new PaymentModel()
            {
                TransactionId = Guid.NewGuid().ToString(),
            };

            ViewBag.Gateways = _paymentService.GatewayNamesForDropdown();

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var gateway = _paymentService.MakePayment(model);
                     
                    if (gateway.Response.Result)
                    {
                        return View("Success", gateway);
                    }
                    else
                    {
                        foreach (var error in gateway.Response.Errors)
                        {
                            ModelState.AddModelError("", error.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Gateways = _paymentService.GatewayNamesForDropdown();

            return View(model);
        }
    }
}
