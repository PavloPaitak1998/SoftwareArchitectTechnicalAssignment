using System;
using Test.WebApplication.Common.Enums;

namespace Test.WebApplication.Common.Dtos
{
    public class TransactionDto
    {
        public string TransactionIdentificator { get; set; }
        public decimal Amount { get; set; }
        public CurrencyCode CurrencyCodeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatusValue TransactionStatusId { get; set; }
    }
}
