using Moq;
using NUnit.Framework;
using PaymentGatewayService;
using PaymentGatewayWebApi.Services;
using System.Linq;
using PaymentGatewayService.Interfaces;

namespace PaymentGatewayUnitTest
{
    public class PaymentGatewayTests
    {
        /// <summary>
        /// Payment amount cannot be 0 or less than 0.
        /// </summary>
        [Test]
        public void MakePayment_NotAllowedAmount_ExceptionResponse()
        {
            //Arrange
            Garanti garanti = new Garanti();

            garanti.Request = new PaymentGatewayService.Models.BankRequest
            {
                Amount = -1,
                CardNumber = "4111111111111111",
                CurrecyCode = PaymentGatewayService.Models.CurrencyCodes.TRY,
                Cvv = "123",
                ExpireMonth = 12,
                ExpireYear = 2022
            };

            //Action
            garanti.MakePayment();


            //Assert
            var err = garanti.Response.Errors;

            Assert.AreEqual(true, err.Any(x => x.Contains("The payment amount must be greater than 0.")));
        }

        /// <summary>
        /// if credit card's bank is Turkish, payment currency must be TRY 
        /// </summary>
        [Test]
        public void MakePayment_NonTRYAmount_FromTurkishCreditCard_ExceptionResponse()
        {
            //Arrange
            Garanti garanti = new Garanti();

            garanti.Request = new PaymentGatewayService.Models.BankRequest
            {
                Amount = 1,
                CardNumber = "4111111111111111",
                CurrecyCode = PaymentGatewayService.Models.CurrencyCodes.EURO,
                Cvv = "123",
                ExpireMonth = 12,
                ExpireYear = 2022
            };

            //Action
            garanti.MakePayment();


            //Assert
            var err = garanti.ValidationErrors;

            Assert.AreEqual(true, err.Any(x => x.ErrorMessage.Contains("Turkish bank card's transactions must be Turkish Lira.")));

        }
    }
}