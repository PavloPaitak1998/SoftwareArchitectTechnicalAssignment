namespace Test.WebApplication.Commands.Models
{
    public class InvalidTransaction
    {
        public string TransactionIdentificator { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
