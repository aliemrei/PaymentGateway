using PaymentGatewayService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService
{
    public sealed class YapiKredi : GatewayBase, IGatewayProvider
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
            return "Yapı Kredi Bankası";
        }

        public void internalMakePayment()
        {
            this.Validate();

            if (this.IsValid)
            {
                //valid;
            }
        }

        public void internalCancelPayment()
        {
            
        }

        public void internalRefundPayment(decimal Amount)
        {
           
        }
    }
}
