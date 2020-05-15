using System;

namespace Test.WebApplication.Dal.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionIdentificator { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyCodeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionStatusId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public CurrencyCode CurrencyCode { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
