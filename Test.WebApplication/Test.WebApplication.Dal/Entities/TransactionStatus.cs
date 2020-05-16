using Test.WebApplication.Common.Enums;

namespace Test.WebApplication.Dal.Entities
{
    public class TransactionStatus
    {
        public TransactionStatusValue TransactionStatusId { get; set; }
        public TransactionStatusValue Status { get; set; }
        public string UnifiedFormat { get; set; }
    }
}
