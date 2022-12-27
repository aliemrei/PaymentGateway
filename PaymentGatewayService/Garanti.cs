using PaymentGatewayService.Interfaces;

namespace PaymentGatewayService
{
    public sealed class Garanti : GatewayBase, IGatewayProvider
    {
        public override void MakePayment()
        {

            try
            {
                this.internalMakePayment();
            }
            catch (Exception ex)
            {
                this.Response.Errors.Add(ex.Message);
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
                this.internalCancelPayment();
            }
            catch (Exception ex)
            {
                this.Response.Errors.Add(ex.Message);
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
                this.internalRefundPayment(Amount);
            }
            catch (Exception ex)
            {
                this.Response.Errors.Add(ex.Message);
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
            this.Validate();

            if (this.IsValid)
            {
                this.Response.Result = true;

                this.Response.AuthCode = DateTime.Now.ToString();
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
