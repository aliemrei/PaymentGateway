using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService
{
    public class Garanti : GatewayBase, IGatewayProvider
    {
        public override void MakePayment()
        {
            try
            {
                try
                {
                    this.internalMakePayment();
                }
                catch (Exception ex)
                {
                    this.Response.Errors.Add(ex);
                }
            }
            finally
            {
                base.MakePayment();
            }
        }
        public override void CancelPayment()
        {
            try
            {
                try
                {
                    this.internalCancelPayment();
                }
                catch (Exception ex)
                {
                    this.Response.Errors.Add(ex);
                }
            }
            finally
            {
                base.CancelPayment();
            }
        }

        public override void RefundPayment(decimal Amount)
        {
            try
            {
                try
                {
                    this.internalRefundPayment(Amount);
                }
                catch (Exception ex)
                {
                    this.Response.Errors.Add(ex);
                }
            }
            finally
            {
                base.RefundPayment(Amount);
            }
        }

        public override string? ToString()
        {
            return "Garanti Bankası";
        }

        public void internalMakePayment()
        {
            throw new NotImplementedException();
        }

        public void internalCancelPayment()
        {
            throw new NotImplementedException();
        }

        public void internalRefundPayment(decimal Amount)
        {
            throw new NotImplementedException();
        }
    }
}
