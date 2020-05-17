using System;
using System.Xml.Serialization;

namespace Test.WebApplication.Commands.Models
{
    [Serializable]
    [XmlType("Transaction")]
    public class SerializableTransaction
    {
        [XmlAttribute("id")]
        public string TransactionIdentificator { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }
    }

    [Serializable]
    [XmlType("PaymentDetails")]
    public class PaymentDetails
    {
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
