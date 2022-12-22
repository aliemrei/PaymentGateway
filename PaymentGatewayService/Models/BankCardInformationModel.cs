using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayService.Models
{
    public class BankCardInformationModel
    {
        [JsonProperty("number")]
        public BankCardInformationNumber number { get; set; }
        public string scheme { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public bool prepaid { get; set; }
        [JsonProperty("country")]
        public BankCardInformationCountry country { get; set; }

        [JsonProperty("bank")]
        public BankCardInformationBank bank { get; set; }
    }

    public class BankCardInformationNumber
    {
        public int length { get; set; }
        public bool luhn { get; set; }
    }

    public class BankCardInformationCountry
    {
        public string numeric { get; set; }
        public string alpha2 { get; set; }
        public string name { get; set; }
        public string emoji { get; set; }
        public string currency { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
    }
    
    public class BankCardInformationBank
    {
        public string name { get; set; }
        public string url { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
    }

}
