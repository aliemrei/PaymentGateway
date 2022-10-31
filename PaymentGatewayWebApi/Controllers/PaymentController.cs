using Microsoft.AspNetCore.Mvc;
using PaymentGatewayService;
using PaymentGatewayWebApi.Models;
using PaymentGatewayWebApi.Repositories;
using PaymentGatewayWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentGatewayWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentService paymentService, IPaymentRepository paymentRepository)
        {
            this._paymentService = paymentService;
            this._paymentRepository = paymentRepository;
        }
        // GET: api/<PaymentController>
        [HttpGet]
        public ActionResult<List<PaymentModel>> Get()
        {
            return _paymentRepository.Get();
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public ActionResult<PaymentModel> Get(string id)
        {
            var payment = _paymentRepository.Get(id);

            if (payment == null)
                return NotFound($"Payment with id = {id} not found");

            return payment;
        }

        // POST api/<PaymentController>

        [HttpPost]
        public ActionResult<PaymentModel?> Post([FromBody] PaymentModel value)
        {
            var result = _paymentService.MakePayment(value);

            return result;
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public ActionResult<PaymentModel> Put(string id, [FromBody] PaymentModel value)
        {
            var payment = _paymentRepository.Get(id);

            if (payment == null)
                return NotFound($"Payment with id = {id} not found");

            _paymentRepository.Update(id, value);

            return NoContent();
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var payment = _paymentRepository.Get(id);

            if (payment == null)
                return NotFound($"Payment with id = {id} not found");

            _paymentRepository.Remove(id);

            return NoContent();
        }
    }
}
