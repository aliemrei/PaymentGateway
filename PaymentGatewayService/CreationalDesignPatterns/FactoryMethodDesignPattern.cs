using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.CreationalDesignPatterns
{
    public class FactoryMethodDesignPattern
    {
        public GatewayBase? Creator(BankEnum bank)
        {
            GatewayBase result = null;

            switch (bank)
            {
                case BankEnum.Garanti:
                    result = new Garanti();

                    break;
                case BankEnum.YapiKredi:
                    result = new YapiKredi();

                    break;
            }

            return result;
        }
    }
}
